using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Web.Script.Serialization;
using System.Linq;

namespace Forwarder
{
    public partial class mainform : Form
    {
        private List<ForwardRuleState> ruleStates = new List<ForwardRuleState>();
        private string configPath = "forwarder-config.json";
        private JavaScriptSerializer serializer = new JavaScriptSerializer();
        private int bufferSize = 8192;

        public mainform()
        {
            InitializeComponent();
        }

        private void mainform_Load(object sender, EventArgs e)
        {
            LoadConfig();
            UpdateGrid();
        }

        private void mainform_FormClosing(object sender, FormClosingEventArgs e)
        {
            foreach (var state in ruleStates)
            {
                if (state.Rule.Enabled)
                {
                    StopRule(state);
                }
            }
        }

        private void Log(string msg)
        {
            if (InvokeRequired)
            {
                Invoke(new Action(() => Log(msg)));
                return;
            }
            lblStatus.Text = string.Format("{0:HH:mm:ss} - {1}", DateTime.Now, msg);
        }

        private void LoadConfig()
        {
            try
            {
                if (File.Exists(configPath))
                {
                    string json = File.ReadAllText(configPath);
                    var config = serializer.Deserialize<ForwarderConfig>(json);
                    if (config != null)
                    {
                        bufferSize = config.BufferSize > 0 ? config.BufferSize : 8192;
                        
                        // stop existing
                        foreach (var s in ruleStates)
                        {
                            StopRule(s);
                        }
                        ruleStates.Clear();

                        if (config.Rules != null)
                        {
                            foreach (var r in config.Rules)
                            {
                                var state = new ForwardRuleState { Rule = r };
                                ruleStates.Add(state);
                                if (r.Enabled)
                                {
                                    StartRule(state);
                                }
                            }
                        }
                        Log("Config loaded.");
                    }
                }
            }
            catch (Exception ex)
            {
                Log("Error loading config: " + ex.Message);
            }
            UpdateGrid();
        }

        private void SaveConfig()
        {
            try
            {
                var config = new ForwarderConfig
                {
                    BufferSize = bufferSize,
                    Rules = ruleStates.Select(s => s.Rule).ToList()
                };
                string json = serializer.Serialize(config);
                // Prettify JSON manually since JavaScriptSerializer doesn't have it built in easily
                File.WriteAllText(configPath, json);
                Log("Config saved.");
            }
            catch (Exception ex)
            {
                Log("Error saving config: " + ex.Message);
            }
        }

        private void StartRule(ForwardRuleState state)
        {
            if (state.CancellationTokenSource != null) return;

            try
            {
                state.CancellationTokenSource = new CancellationTokenSource();
                state.Listener = new TcpListener(IPAddress.Parse(state.Rule.ListenIP), state.Rule.ListenPort);
                state.Listener.Start();
                state.Rule.Enabled = true;

                Task.Run(() => AcceptClientsAsync(state, state.CancellationTokenSource.Token));
                Log(string.Format("Started rule {0} on {1}:{2}", state.Rule.Name, state.Rule.ListenIP, state.Rule.ListenPort));
            }
            catch (Exception ex)
            {
                Log(string.Format("Failed to start rule {0}: {1}", state.Rule.Name, ex.Message));
                state.Rule.Enabled = false;
                if (state.CancellationTokenSource != null) state.CancellationTokenSource.Cancel();
                state.CancellationTokenSource = null;
                if (state.Listener != null) state.Listener.Stop();
                state.Listener = null;
            }
        }

        private void StopRule(ForwardRuleState state)
        {
            if (state.CancellationTokenSource == null) return;
            state.Rule.Enabled = false;
            state.CancellationTokenSource.Cancel();
            try { if (state.Listener != null) state.Listener.Stop(); } catch { }
            state.Listener = null;
            state.CancellationTokenSource = null;
            Log(string.Format("Stopped rule {0}", state.Rule.Name));
        }

        private async Task AcceptClientsAsync(ForwardRuleState state, CancellationToken token)
        {
            while (!token.IsCancellationRequested)
            {
                try
                {
                    var client = await state.Listener.AcceptTcpClientAsync();
                    Interlocked.Increment(ref state.ActiveConnections);
                    var ignore = HandleClientAsync(state, client, token).ContinueWith(t => 
                    {
                        Interlocked.Decrement(ref state.ActiveConnections);
                    });
                }
                catch (ObjectDisposedException) { break; }
                catch (Exception ex)
                {
                    if (!token.IsCancellationRequested)
                        Log(string.Format("Accept error on {0}: {1}", state.Rule.Name, ex.Message));
                }
            }
        }

        private async Task HandleClientAsync(ForwardRuleState state, TcpClient sourceClient, CancellationToken token)
        {
            using (sourceClient)
            using (var targetClient = new TcpClient())
            {
                try
                {
                    await targetClient.ConnectAsync(state.Rule.TargetIP, state.Rule.TargetPort);
                    
                    using (var sourceStream = sourceClient.GetStream())
                    using (var targetStream = targetClient.GetStream())
                    {
                        var task1 = CopyStreamAsync(sourceStream, targetStream, state, token);
                        var task2 = CopyStreamAsync(targetStream, sourceStream, state, token);
                        await Task.WhenAny(task1, task2);
                    }
                }
                catch (Exception ex)
                {
                    // Don't log normal disconnects
                    if (!(ex is IOException || ex is SocketException))
                    {
                        Log(string.Format("Connection error on {0}: {1}", state.Rule.Name, ex.Message));
                    }
                }
            }
        }

        private async Task CopyStreamAsync(NetworkStream input, NetworkStream output, ForwardRuleState state, CancellationToken token)
        {
            byte[] buffer = new byte[bufferSize];
            int bytesRead;
            while (!token.IsCancellationRequested && (bytesRead = await input.ReadAsync(buffer, 0, buffer.Length, token)) > 0)
            {
                await output.WriteAsync(buffer, 0, bytesRead, token);
                Interlocked.Add(ref state.TotalBytesForwarded, bytesRead);
            }
        }

        private void UpdateGrid()
        {
            dgvRules.Rows.Clear();
            foreach (var state in ruleStates)
            {
                dgvRules.Rows.Add(
                    state.Rule.Name,
                    string.Format("{0}:{1}", state.Rule.ListenIP, state.Rule.ListenPort),
                    string.Format("{0}:{1}", state.Rule.TargetIP, state.Rule.TargetPort),
                    state.Rule.Enabled,
                    state.ActiveConnections,
                    FormatBytes(state.TotalBytesForwarded)
                );
            }
        }

        private string FormatBytes(long bytes)
        {
            string[] suf = { "B", "KB", "MB", "GB", "TB", "PB", "EB" };
            if (bytes == 0)
                return "0 B";
            long bytes2 = Math.Abs(bytes);
            int place = Convert.ToInt32(Math.Floor(Math.Log(bytes2, 1024)));
            double num = Math.Round(bytes2 / Math.Pow(1024, place), 1);
            return (Math.Sign(bytes) * num).ToString() + " " + suf[place];
        }

        private void timerUpdate_Tick(object sender, EventArgs e)
        {
            for (int i = 0; i < ruleStates.Count; i++)
            {
                if (i < dgvRules.Rows.Count)
                {
                    var row = dgvRules.Rows[i];
                    row.Cells[3].Value = ruleStates[i].Rule.Enabled;
                    row.Cells[4].Value = ruleStates[i].ActiveConnections;
                    row.Cells[5].Value = FormatBytes(ruleStates[i].TotalBytesForwarded);
                }
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            var rule = new ForwardRule
            {
                Name = txtName.Text,
                ListenIP = txtListenIP.Text,
                ListenPort = (int)numListenPort.Value,
                TargetIP = txtTargetIP.Text,
                TargetPort = (int)numTargetPort.Value,
                Enabled = false
            };
            ruleStates.Add(new ForwardRuleState { Rule = rule });
            UpdateGrid();
            Log("Rule added.");
        }

        private void btnRemove_Click(object sender, EventArgs e)
        {
            if (dgvRules.SelectedRows.Count > 0)
            {
                int idx = dgvRules.SelectedRows[0].Index;
                var state = ruleStates[idx];
                if (state.Rule.Enabled)
                {
                    StopRule(state);
                }
                ruleStates.RemoveAt(idx);
                UpdateGrid();
                Log("Rule removed.");
            }
        }

        private void btnToggle_Click(object sender, EventArgs e)
        {
            if (dgvRules.SelectedRows.Count > 0)
            {
                int idx = dgvRules.SelectedRows[0].Index;
                var state = ruleStates[idx];
                if (state.Rule.Enabled)
                {
                    StopRule(state);
                }
                else
                {
                    StartRule(state);
                }
                UpdateGrid();
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            SaveConfig();
        }

        private void btnLoad_Click(object sender, EventArgs e)
        {
            LoadConfig();
        }
    }

    public class ForwardRule
    {
        public string Name { get; set; }
        public string ListenIP { get; set; }
        public int ListenPort { get; set; }
        public string TargetIP { get; set; }
        public int TargetPort { get; set; }
        public bool Enabled { get; set; }
    }

    public class ForwarderConfig
    {
        public List<ForwardRule> Rules { get; set; }
        public int BufferSize { get; set; }

        public ForwarderConfig()
        {
            Rules = new List<ForwardRule>();
            BufferSize = 8192;
        }
    }

    public class ForwardRuleState
    {
        public ForwardRule Rule { get; set; }
        public int ActiveConnections;
        public long TotalBytesForwarded;
        public TcpListener Listener;
        public CancellationTokenSource CancellationTokenSource;
    }
}

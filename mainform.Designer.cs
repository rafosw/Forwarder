namespace Forwarder
{
    partial class mainform
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.dgvRules = new System.Windows.Forms.DataGridView();
            this.colName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colListen = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colTarget = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colEnabled = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.colConn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colBytes = new System.Windows.Forms.DataGridViewTextBoxColumn();
            
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btnAdd = new System.Windows.Forms.Button();
            this.numTargetPort = new System.Windows.Forms.NumericUpDown();
            this.txtTargetIP = new System.Windows.Forms.TextBox();
            this.numListenPort = new System.Windows.Forms.NumericUpDown();
            this.txtListenIP = new System.Windows.Forms.TextBox();
            this.txtName = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            
            this.btnRemove = new System.Windows.Forms.Button();
            this.btnToggle = new System.Windows.Forms.Button();
            this.btnSave = new System.Windows.Forms.Button();
            this.btnLoad = new System.Windows.Forms.Button();
            this.lblStatus = new System.Windows.Forms.Label();
            this.timerUpdate = new System.Windows.Forms.Timer(this.components);
            
            ((System.ComponentModel.ISupportInitialize)(this.dgvRules)).BeginInit();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numTargetPort)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numListenPort)).BeginInit();
            this.SuspendLayout();
            
            // dgvRules
            this.dgvRules.AllowUserToAddRows = false;
            this.dgvRules.AllowUserToDeleteRows = false;
            this.dgvRules.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvRules.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colName,
            this.colListen,
            this.colTarget,
            this.colEnabled,
            this.colConn,
            this.colBytes});
            this.dgvRules.Location = new System.Drawing.Point(12, 12);
            this.dgvRules.Name = "dgvRules";
            this.dgvRules.ReadOnly = true;
            this.dgvRules.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvRules.Size = new System.Drawing.Size(760, 200);
            this.dgvRules.TabIndex = 0;
            
            // colName
            this.colName.HeaderText = "Name";
            this.colName.Name = "colName";
            this.colName.ReadOnly = true;
            // colListen
            this.colListen.HeaderText = "Listen";
            this.colListen.Name = "colListen";
            this.colListen.ReadOnly = true;
            // colTarget
            this.colTarget.HeaderText = "Target";
            this.colTarget.Name = "colTarget";
            this.colTarget.ReadOnly = true;
            // colEnabled
            this.colEnabled.HeaderText = "Active";
            this.colEnabled.Name = "colEnabled";
            this.colEnabled.ReadOnly = true;
            this.colEnabled.Width = 60;
            // colConn
            this.colConn.HeaderText = "Connections";
            this.colConn.Name = "colConn";
            this.colConn.ReadOnly = true;
            // colBytes
            this.colBytes.HeaderText = "Bytes";
            this.colBytes.Name = "colBytes";
            this.colBytes.ReadOnly = true;
            
            // groupBox1
            this.groupBox1.Controls.Add(this.btnAdd);
            this.groupBox1.Controls.Add(this.numTargetPort);
            this.groupBox1.Controls.Add(this.txtTargetIP);
            this.groupBox1.Controls.Add(this.numListenPort);
            this.groupBox1.Controls.Add(this.txtListenIP);
            this.groupBox1.Controls.Add(this.txtName);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Location = new System.Drawing.Point(12, 220);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(760, 80);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Add New Rule";
            
            // label1
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(10, 25);
            this.label1.Name = "label1";
            this.label1.Text = "Name:";
            
            // txtName
            this.txtName.Location = new System.Drawing.Point(13, 45);
            this.txtName.Name = "txtName";
            this.txtName.Size = new System.Drawing.Size(100, 20);
            
            // label2
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(120, 25);
            this.label2.Text = "Listen IP:";
            // txtListenIP
            this.txtListenIP.Location = new System.Drawing.Point(123, 45);
            this.txtListenIP.Size = new System.Drawing.Size(100, 20);
            this.txtListenIP.Text = "0.0.0.0";
            
            // label3
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(230, 25);
            this.label3.Text = "Port:";
            // numListenPort
            this.numListenPort.Location = new System.Drawing.Point(233, 45);
            this.numListenPort.Maximum = new decimal(new int[] { 65535, 0, 0, 0 });
            this.numListenPort.Size = new System.Drawing.Size(60, 20);
            this.numListenPort.Value = new decimal(new int[] { 8080, 0, 0, 0 });
            
            // label4
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(310, 25);
            this.label4.Text = "Target IP:";
            // txtTargetIP
            this.txtTargetIP.Location = new System.Drawing.Point(313, 45);
            this.txtTargetIP.Size = new System.Drawing.Size(100, 20);
            this.txtTargetIP.Text = "127.0.0.1";
            
            // label5
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(420, 25);
            this.label5.Text = "Port:";
            // numTargetPort
            this.numTargetPort.Location = new System.Drawing.Point(423, 45);
            this.numTargetPort.Maximum = new decimal(new int[] { 65535, 0, 0, 0 });
            this.numTargetPort.Size = new System.Drawing.Size(60, 20);
            this.numTargetPort.Value = new decimal(new int[] { 80, 0, 0, 0 });
            
            // btnAdd
            this.btnAdd.Location = new System.Drawing.Point(500, 43);
            this.btnAdd.Size = new System.Drawing.Size(75, 23);
            this.btnAdd.Text = "Add Rule";
            this.btnAdd.UseVisualStyleBackColor = true;
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            
            // btnToggle
            this.btnToggle.Location = new System.Drawing.Point(12, 310);
            this.btnToggle.Size = new System.Drawing.Size(120, 30);
            this.btnToggle.Text = "Start/Stop Rule";
            this.btnToggle.Click += new System.EventHandler(this.btnToggle_Click);
            
            // btnRemove
            this.btnRemove.Location = new System.Drawing.Point(140, 310);
            this.btnRemove.Size = new System.Drawing.Size(120, 30);
            this.btnRemove.Text = "Remove Rule";
            this.btnRemove.Click += new System.EventHandler(this.btnRemove_Click);
            
            // btnSave
            this.btnSave.Location = new System.Drawing.Point(520, 310);
            this.btnSave.Size = new System.Drawing.Size(120, 30);
            this.btnSave.Text = "Save Config";
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            
            // btnLoad
            this.btnLoad.Location = new System.Drawing.Point(650, 310);
            this.btnLoad.Size = new System.Drawing.Size(120, 30);
            this.btnLoad.Text = "Load Config";
            this.btnLoad.Click += new System.EventHandler(this.btnLoad_Click);
            
            // lblStatus
            this.lblStatus.AutoSize = true;
            this.lblStatus.Location = new System.Drawing.Point(12, 360);
            this.lblStatus.Text = "Ready.";
            
            // timerUpdate
            this.timerUpdate.Enabled = true;
            this.timerUpdate.Interval = 1000;
            this.timerUpdate.Tick += new System.EventHandler(this.timerUpdate_Tick);
            
            // mainform
            this.ClientSize = new System.Drawing.Size(784, 391);
            this.Controls.Add(this.lblStatus);
            this.Controls.Add(this.btnLoad);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.btnRemove);
            this.Controls.Add(this.btnToggle);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.dgvRules);
            this.Name = "mainform";
            this.Text = "TCP/IP Forwarder";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.mainform_FormClosing);
            this.Load += new System.EventHandler(this.mainform_Load);
            
            ((System.ComponentModel.ISupportInitialize)(this.dgvRules)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numTargetPort)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numListenPort)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        private System.Windows.Forms.DataGridView dgvRules;
        private System.Windows.Forms.DataGridViewTextBoxColumn colName;
        private System.Windows.Forms.DataGridViewTextBoxColumn colListen;
        private System.Windows.Forms.DataGridViewTextBoxColumn colTarget;
        private System.Windows.Forms.DataGridViewCheckBoxColumn colEnabled;
        private System.Windows.Forms.DataGridViewTextBoxColumn colConn;
        private System.Windows.Forms.DataGridViewTextBoxColumn colBytes;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button btnAdd;
        private System.Windows.Forms.NumericUpDown numTargetPort;
        private System.Windows.Forms.TextBox txtTargetIP;
        private System.Windows.Forms.NumericUpDown numListenPort;
        private System.Windows.Forms.TextBox txtListenIP;
        private System.Windows.Forms.TextBox txtName;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnRemove;
        private System.Windows.Forms.Button btnToggle;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btnLoad;
        private System.Windows.Forms.Label lblStatus;
        private System.Windows.Forms.Timer timerUpdate;
    }
}

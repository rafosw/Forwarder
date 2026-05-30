# Forwarder

Forwarder is a lightweight TCP port forwarding utility built with C#.

<img src="https://github.com/rafosw/Forwarder/blob/main/ss/forwarder.png?raw=true" width="600" height="350" />

## Features

* Multiple forwarding rules
* Custom listen and target IP/port pairs
* Start and stop rules individually
* Live connection statistics
* Traffic usage tracking
* Config save and load support
* Adjustable buffer size
* Asynchronous TCP forwarding

## How It Works

A forwarding rule listens on a specified IP address and port, then forwards all incoming TCP traffic to a configured destination IP and port.

Example:

```text
0.0.0.0:8080 -> 192.168.1.100:80
```

Connections are handled asynchronously and support bidirectional traffic forwarding.

## Configuration

Rules and application settings are stored in:

```text
forwarder-config.json
```

Each rule contains:

* Rule name
* Listen IP
* Listen port
* Target IP
* Target port
* Enabled state

## Statistics

The application displays:

* Active connections
* Total forwarded traffic
* Rule status

Traffic counters are automatically updated while forwarding is active.

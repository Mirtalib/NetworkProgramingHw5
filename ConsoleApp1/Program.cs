using System.Net.Sockets;
using System.Net;

var ipString = "127.0.0.1";
var port = 12;

using var client = new TcpClient(ipString, port);

var serverStream = client.GetStream();
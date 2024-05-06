using RsLib.TCP.Common;
using System;
// for Socket
using System.Net;
using System.Net.Sockets;
// for Muti-Thread
using System.Threading;
namespace RsLib.TCP.Client
{
    public class TCPClient
    {
        TCP_ClientBase _client = null;
        public event Action<string, string> DataReceived;
        public string Name => _client.Name;
        public string Msg => _client.Name;
        public bool IsConnect => _client.StateObject != null ? _client.StateObject.IsConnect : false;

        public TCPClient(string clientName, string ipAddress, int port)
        {
            _client = new TCP_ClientBase(clientName, ipAddress,port);
            _client.StateObject.DataReceived += StateObject_DataReceived;
        }

        private void StateObject_DataReceived(string name, string obj)
        {
            DataReceived?.Invoke(name, obj);
            msgHandle();
        }

        void msgHandle()
        {
            string[] splitData = Msg.Split(',');
            if (splitData.Length < 2) return;
            string requestName = splitData[0];
            string act = splitData[1];
            switch (act)
            {
                case Command.ServerStop:
                    Send(Command.ByeBye);
                    _client.StateObject.DataReceived -= StateObject_DataReceived;
                    _client.StateObject.Disconnect();
                    break;
                case Command.ByeBye:
                    _client.StateObject.DataReceived -= StateObject_DataReceived;
                    _client.StateObject.Disconnect();
                    break;

                case Command.Welcome:
                    _client.StateObject.SendData($"{Name},{Command.Connect}");
                    break;
                default:

                    break;
            }
        }

        // 中斷連線
        public void Disconnect()
        {
            _client.StateObject.SendData($"{Name},{Command.Disconnect}");
        }
        public void Connect()
        {
            _client.Connect();
        }
        public void Send(string data)
        {
            if (IsConnect)
                _client.StateObject.SendData(data);
        }
    }
}

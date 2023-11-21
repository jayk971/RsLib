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
        IPEndPoint ipe = null;
        IPAddress ipa = null;
        StateObject stateObject;

        ManualResetEvent connectDone = new ManualResetEvent(false);
        public event Action<string, string> DataReceived;
        public string Name { get; private set; }
        public string Msg { get; private set; }
        public bool IsConnect => stateObject != null ? stateObject.IsConnect : false;
        ManualResetEvent ReceiveDone = new ManualResetEvent(false);

        public TCPClient(string clientName, string ipAddress, int port)
        {
            Name = clientName;
            stateObject = new StateObject(Name);
            ipa = IPAddress.Parse(ipAddress);
            ipe = new IPEndPoint(ipa, port);
            stateObject.DataReceived += StateObject_DataReceived;
        }

        private void StateObject_DataReceived(string name, string obj)
        {
            DataReceived?.Invoke(name, obj);
            Msg = obj;
            ReceiveDone.Set();
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
                    stateObject.DataReceived -= StateObject_DataReceived;
                    stateObject.Disconnect();
                    break;
                case Command.ByeBye:
                    stateObject.DataReceived -= StateObject_DataReceived;
                    stateObject.Disconnect();
                    break;
                default:

                    break;
            }
        }

        // 中斷連線
        public void Disconnect()
        {
            stateObject.SendClientDisconnect();
        }
        public void Connect()
        {
            try
            {
                Socket client = new Socket(ipa.AddressFamily,
                    SocketType.Stream, ProtocolType.Tcp);

                client.BeginConnect(ipe,
                    new AsyncCallback(connectCallback), client);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }

        }
        void connectCallback(IAsyncResult ar)
        {
            try
            {
                Socket client = (Socket)ar.AsyncState;
                client.EndConnect(ar);
                stateObject.WorkSocket = client;

                ReceiveDone.Reset();
                stateObject.Receive();
                bool isInTime = ReceiveDone.WaitOne(5000);
                if (Msg.Contains(Command.Welcome) && isInTime)
                {
                    stateObject.SendClientConnect();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
        }
        public void Send(string data)
        {
            if (IsConnect)
                stateObject.SendData(data);
        }
    }
}

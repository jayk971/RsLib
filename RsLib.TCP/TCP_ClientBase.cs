using RsLib.TCP.Common;
using System;
// for Socket
using System.Net;
using System.Net.Sockets;
// for Muti-Thread
using System.Threading;
namespace RsLib.TCP.Client
{
    public class TCP_ClientBase
    {
        protected IPEndPoint ipe = null;
        protected IPAddress ipa = null;
        public StateObject StateObject = null;

        ManualResetEvent connectDone = new ManualResetEvent(false);
        public event Action<string, string> DataReceived;
        public string Name { get; protected set; }
        public string Msg { get; protected set; }
        public bool IsConnect => StateObject != null ? StateObject.IsConnect : false;
        ManualResetEvent ReceiveDone = new ManualResetEvent(false);

        public TCP_ClientBase(string clientName, string ipAddress, int port)
        {
            Name = clientName;
            StateObject = new StateObject(Name);
            ipa = IPAddress.Parse(ipAddress);
            ipe = new IPEndPoint(ipa, port);
            StateObject.DataReceived += StateObject_DataReceived;
        }

        private void StateObject_DataReceived(string name, string obj)
        {
            DataReceived?.Invoke(name, obj);
            Msg = obj;
            ReceiveDone.Set();
        }


        // 中斷連線
        public void Disconnect()
        {
            StateObject.DataReceived -= StateObject_DataReceived;
            StateObject.Disconnect();
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
                StateObject.WorkSocket = client;

                ReceiveDone.Reset();
                StateObject.Receive();
                bool isInTime = ReceiveDone.WaitOne(5000);
                //if (Msg.Contains(Command.Welcome) && isInTime)
                //{
                //    stateObject.SendClientConnect();
                //}
                if (isInTime)
                {
                    //stateObject.SendClientConnect();
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
                StateObject.SendData(data);
        }
    }
}

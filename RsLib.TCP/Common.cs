using System;
// for Socket
using System.Net;
using System.Net.Sockets;
using System.Text;
// for Muti-Thread
using System.Threading;
namespace RsLib.TCP.Common
{
    public class StateObject
    {
        public Socket WorkSocket = null;
        public string Name { get; private set; } = "";
        const int _bufferSize = 1024;
        byte[] _buffer = new byte[_bufferSize];
        StringBuilder sb = new StringBuilder();
        public event Action<string,string> DataReceived;
        public bool IsConnect => WorkSocket != null ? WorkSocket.Connected : false;
        public StateObject(string name)
        {
            Name = name;
        }
        public void SendClientConnect()
        {
            SendData($"{Command.Connect}");
        }
        public void SendClientDisconnect()
        {
            SendData($"{Command.Disconnect}");
        }
        public void SendServerStop()
        {
            SendData(Command.Stop);
        }
        public void SendData(string data)
        {
            string sendData = $"{Name},{data}";
            byte[] byteData = Encoding.ASCII.GetBytes(sendData);
            WorkSocket.BeginSend(byteData, 0, byteData.Length, 0,
                new AsyncCallback(SendCallback), WorkSocket);
        }
        private void SendCallback(IAsyncResult ar)
        {
            try
            {
                
                Socket client = (Socket)ar.AsyncState;
                int bytesSent = client.EndSend(ar);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
        }
        public void Receive()
        {
            try
            {
                WorkSocket.BeginReceive(_buffer, 0, _bufferSize, 0,
                    new AsyncCallback(ReceiveCallback), this);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
        }

        private void ReceiveCallback(IAsyncResult ar)
        {
            try
            {
                int bytesRead = WorkSocket.EndReceive(ar);

                if (bytesRead > 0)
                {
                    sb.Append(Encoding.ASCII.GetString(_buffer, 0, bytesRead));
                    if (sb.Length > 1)
                    {
                        string getData = sb.ToString();
                        DataReceived?.Invoke(Name, getData);
                    }
                    sb.Clear();
                }
                if (WorkSocket != null)
                {
                    WorkSocket.BeginReceive(_buffer, 0, _bufferSize, 0,
                      new AsyncCallback(ReceiveCallback), this);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
        }
        public void Disconnect()
        {
            if (WorkSocket == null) return;
            if(WorkSocket.Connected)
                WorkSocket.Shutdown(SocketShutdown.Both);
           
            WorkSocket.Close();
            WorkSocket.Dispose();
            WorkSocket = null;
        }


    }

    public struct Command
    {
        public const string Welcome = "Welcome";
        public const string Connect = "Connect";
        public const string Disconnect = "Disconnect";
        public const string ByeBye = "ByeBye";
        public const string Stop = "Stop";
        public const string ack = "ack";
    }

}

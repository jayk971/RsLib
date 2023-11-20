using RsLib.Common;
using RsLib.LogMgr;
// for File I/O

using RsLib.TCP.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
// for Socket
using System.Net;
using System.Net.Sockets;
using System.Text;
// for Muti-Thread
using System.Threading;
using YamlDotNet.Serialization;
namespace RsLib.TCP.Server
{
    public class TCPServer
    {
        public TCPServerOption Option = new TCPServerOption();
        string _opeionFilePath = AppFolderHandle.Folder_Config + "\\TCPServer.yaml";
        public bool IsRun { get; private set; } = false;
        public int ClientCount => _clientObj.Count;
        IPEndPoint ipe = null;
        Socket mainSocket;
        Dictionary<string, StateObject> _clientObj = new Dictionary<string, StateObject>();
        public List<string> ClientsName => _clientObj.Keys.ToList();
        public string Msg { get; private set; }
        //StateObject[] stateObject;
        public event Action<string, string> DataReceived;
        public event Action<string> ClientAdded;
        ManualResetEvent ReceiveDone = new ManualResetEvent(false);
        public TCPServer()
        {
            LoadYaml();
            IsRun = false;
            ipe = new IPEndPoint(IPAddress.Any, Option.Port);
        }
        public void Send(string data)
        {
            if (_clientObj.Count == 0) return;
            foreach (var item in _clientObj)
            {
                Send(item.Key, data);
            }
        }
        public void Send(string name, string data)
        {
            if (_clientObj.Count == 0) return;
            if (_clientObj.ContainsKey(name) == false) return;

            if (_clientObj[name].WorkSocket != null)
                if (_clientObj[name].WorkSocket.Connected == true)
                {
                    //Log.Add($"TCP server send name : {name} msg : {data}", MsgLevel.Trace);
                    _clientObj[name].SendData(data);
                }
        }
        private void StateObject_DataReceived(string name, string obj)
        {
            //Log.Add($"TCP server receive name : {name} msg : {obj}", MsgLevel.Trace);

            DataReceived?.Invoke(name, obj);
            Msg = obj;
            msgHandle();
            ReceiveDone.Set();

        }
        void msgHandle()
        {
            string[] splitData = Msg.Split(',');
            if (splitData.Length < 2) return;
            string requestName = splitData[0];
            string act = splitData[1];
            if (_clientObj.ContainsKey(requestName))
            {
                switch (act)
                {
                    case Command.Disconnect:
                        _clientObj[requestName].SendData(Command.ByeBye);
                        _clientObj[requestName].Disconnect();
                        _clientObj[requestName].DataReceived -= StateObject_DataReceived;

                        _clientObj.Remove(requestName);
                        ClientAdded?.Invoke("");
                        break;
                    case Command.ByeBye:
                        _clientObj[requestName].Disconnect();
                        _clientObj[requestName].DataReceived -= StateObject_DataReceived;

                        _clientObj.Remove(requestName);
                        ClientAdded?.Invoke("");
                        break;
                    default:

                        break;
                }
            }
        }
        // 啟動Server
        public void Start()
        {
            try
            {
                mainSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);


                mainSocket.Bind(ipe);
                mainSocket.Listen(4);
                Log.Add("TCP server start. Wait clients...", MsgLevel.Info);
                IsRun = true;
                mainSocket.BeginAccept(
                    new AsyncCallback(clientConnected),
                    null);

            }
            catch (Exception ex)
            {
                Log.Add("Start TCP server exceptioon.", MsgLevel.Alarm, ex);
            }
        }
        void clientConnected(IAsyncResult ar)
        {
            if (mainSocket == null) return;
            Socket handler = mainSocket.EndAccept(ar);

            int currentClientCount = _clientObj.Count;
            if (currentClientCount >= Option.MaxClientCount)
            {
                Log.Add($"Clients count is over limit {Option.MaxClientCount}", MsgLevel.Alarm);
                return;
            }
            StateObject temp = new StateObject($"{Option.Name}_{currentClientCount}");
            Log.Add($"{handler.RemoteEndPoint} is connecting to {temp.Name}", MsgLevel.Info);

            temp.DataReceived += StateObject_DataReceived;
            temp.WorkSocket = handler;
            temp.SendData(Command.Welcome);
            ReceiveDone.Reset();
            temp.Receive();
            bool isInTime = ReceiveDone.WaitOne(5000);

            if (isInTime && Msg.Contains(Command.Connect))
            {
                string[] splitData = Msg.Split(',');
                if (splitData.Length < 2)
                {
                    Log.Add($"Msg format is wrong. Receive Msg : {Msg}", MsgLevel.Alarm);
                }
                else
                {
                    string requestName = splitData[0];
                    if (_clientObj.ContainsKey(requestName) == false)
                    {
                        _clientObj.Add(requestName, temp);
                        Log.Add($"{handler.RemoteEndPoint} connects to {temp.Name} successfully.", MsgLevel.Info);
                        ClientAdded?.Invoke(requestName);
                    }
                    else
                    {
                        _clientObj[requestName].DataReceived -= StateObject_DataReceived;
                        _clientObj[requestName].Disconnect();
                        _clientObj[requestName] = temp;
                        Log.Add($"{handler.RemoteEndPoint} reconnected.", MsgLevel.Warn);
                    }
                }
            }
            else
            {
                Log.Add($"{handler.RemoteEndPoint} connect fail. Timeout : {!isInTime}. Msg : {Msg}", MsgLevel.Alarm);
            }
            // 等待下一個使用者連接
            mainSocket.BeginAccept(
                new AsyncCallback(clientConnected),
                null);
        }

        // 停止Server
        public void Stop()
        {
            try
            {
                foreach (var item in _clientObj)
                {
                    ReceiveDone.Reset();
                    item.Value.SendServerStop();
                    bool isInTime = ReceiveDone.WaitOne(5000);
                }
                if (mainSocket.Connected)
                    mainSocket.Shutdown(SocketShutdown.Both);
                mainSocket.Close();
                mainSocket.Dispose();
                mainSocket = null;
                IsRun = false;
                _clientObj.Clear();

                Log.Add($"TCP server stopped.", MsgLevel.Info);
            }
            catch (Exception ex)
            {
                Log.Add("TCP server stop exception.", MsgLevel.Alarm, ex);
            }
        }
        public void SaveYaml()
        {
            Option.SaveYaml(_opeionFilePath);
        }

        public void LoadYaml()
        {
            if (!File.Exists(_opeionFilePath))
            {
                SaveYaml();
            }
            string ReadData = "";
            using (StreamReader sr = new StreamReader(_opeionFilePath, Encoding.Default))
            {
                ReadData = sr.ReadToEnd();
            }
            var deserializer = new DeserializerBuilder().
                IgnoreUnmatchedProperties()
                .Build();

            //yml contains a string containing your YAML
            var p = deserializer.Deserialize<TCPServerOption>(ReadData);
            Option = p.DeepClone();
        }

    }
    [Serializable]
    public class TCPServerOption
    {
        [Category("Server")]
        [DisplayName("Name")]
        public string Name { get; set; } = "TCPServer";
        [Category("Server")]
        [DisplayName("Max Client Count")]
        public ushort MaxClientCount { get; set; } = 4;
        [Category("Server")]
        [DisplayName("Port")]
        public ushort Port { get; set; } = 1000;
        internal void SaveYaml(string filePath)
        {
            using (StreamWriter sw = new StreamWriter(filePath, false, Encoding.Default))
            {
                var serializer = new SerializerBuilder()
                    .Build();
                var yaml = serializer.Serialize(this);
                sw.WriteLine(yaml);
                sw.Flush();
            }
        }
    }

}

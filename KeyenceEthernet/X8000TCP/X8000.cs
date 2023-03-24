using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using YamlDotNet.Serialization;
using YamlDotNet.Serialization.NamingConventions;
using RsLib.LogMgr;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using RsLib.Common;
namespace RsLib.X8000TCP
{
    public class X8000
    {
        public string IP = "192.168.83.2";
        public int Port = 8500;

        [YamlIgnore]
        public string config_file_name = "X8000IP.cfg";
        bool isConnected = false;
        [YamlIgnore]
        public bool IsConnected 
        {
            get
            {
                if(!isConnected) Log.Add($"X8000 Disconnected", MsgLevel.Alarm);
                return isConnected;
            }
        }
        TcpClient client;

        public event Action<string> UpdateCommand;
        string status = "Disconnected";
        [YamlIgnore]
        public string Status => status;
        public X8000()
        {
        }

        public bool Ping(int pingByteSize) =>FT_Functions.PingOK(IP, pingByteSize);
        
        public bool CheckRunMode()
        {
            Log.Add($"Check X8000 run mode", MsgLevel.Trace);
            if (IsConnected)
            {
                string[] data = sendCommand("RM");
                bool result = data[1] == "1";
                Log.Add($"X8000 is run mode : {result}", MsgLevel.Info);
                return result;
            }
            else return false;
        }

        public bool Trigger()
        {
            Log.Add("X8000 Trigger", MsgLevel.Trace);
            if (!IsConnected) return false;

            string[] data = sendCommand("T1");
            return data[0] == "T1";
        }
        public bool StopTrigger()
        {
            Log.Add("X8000 Stop Trigger", MsgLevel.Trace);
            if (!IsConnected) return false;

            SwitchToSetting();
            SpinWait.SpinUntil(()=>false, 1000);
            return SwitchToRun();
        }
        public bool TriggerEnable()
        {
            Log.Add("X8000 Trigger Enable", MsgLevel.Trace);
            if (!IsConnected) return false;

            string[] data = sendCommand("TE,1");
            return data[0] == "TE";
        }
        public bool TriggerDisable()
        {
            Log.Add("X8000 Trigger Disable", MsgLevel.Trace);
            if (!IsConnected) return false;

            string[] data = sendCommand("TE,0");
            return data[0] == "TE";
        }
        public bool SwitchToRun()
        {
            Log.Add("X8000 Switch to Run Mode", MsgLevel.Trace);
            if (!IsConnected) return false;

            string[] data = sendCommand("R0");
            return data[0] == "R0";
        }
        public bool SwitchToSetting()
        {
            Log.Add("X8000 Switch to Setting Mode", MsgLevel.Trace);
            if (!IsConnected) return false;

            string[] data = sendCommand("S0");
            return data[0] == "S0";
        }
        public bool Reset()
        {
            Log.Add("X8000 Reset", MsgLevel.Trace);
            if (!IsConnected) return false;

            string[] data = sendCommand("RS");
            return data[0] == "RS";
        }
        public bool Reboot()
        {
            Log.Add("X8000 Reboot", MsgLevel.Trace);
            if (!IsConnected) return false;

            string[] data = sendCommand("RB");
            return data[0] == "RB";
        }
        public bool SaveSetting()
        {
            Log.Add("X8000 Save Setting", MsgLevel.Trace);
            if (!IsConnected) return false;

            string[] data = sendCommand("SS");
            return data[0] == "SS";
        }
        public bool ClearError()
        {
            Log.Add("X8000 clear error", MsgLevel.Trace);
            if (!IsConnected) return false;

            string[] data = sendCommand("CE");
            return data[0] == "CE";
        }

        public bool SwitchSettingNumber(int settingNum)
        {
            Log.Add($"X8000 change recipe {settingNum}", MsgLevel.Info);
            if (!IsConnected) return false;
            if (settingNum < 0)
            {
                Log.Add($"X8000 recipe number {settingNum} < 0", MsgLevel.Warn);
                return false;
            }

            string[] data = sendCommand($"PW,1,{settingNum}");
            return data[0] == "PW";
        }
        public int ReadCurrentSettingNumber()
        {
            Log.Add($"X8000 read current recipe", MsgLevel.Info);
            if (!IsConnected) return -1;

            string[] data = sendCommand("PR");
            if (data[0] == "PR")
            {
                int recipeNum = int.Parse(data[2]);
                Log.Add($"X8000 current recipe {recipeNum}", MsgLevel.Info);
                return recipeNum;
            }
            else return -1;
        }

        public bool ClearHistory()
        {
            string[] data = sendCommand("HC");
            return data[0] == "HC";
        }
        public bool SettingDelayTime(int ms)
        {
            Log.Add($"X8000 set trigger delay {ms} ms", MsgLevel.Trace);
            if (!IsConnected) return false;

            string[] data = sendCommand($"CTD,1,{ms}");
            return data[0] == "CTD";
        }
        public bool SaveX8000ScreenPrint()
        {
            Log.Add("X8000 save screen print", MsgLevel.Trace);
            if (!IsConnected) return false;

            string[] data = sendCommand($"BC,1");
            return data[0] == "BC";
        }
        public bool Echo(string msg,bool addLog = false)
        {
            if(addLog)  Log.Add($"X8000 Echo {msg}", MsgLevel.Trace);
            if (!IsConnected) return false;

            string[] data = sendCommand($"EC,{msg}");
            if(data[0] == "EC")
            {
                return data[1] == msg;
            }
            else
            {
                return false;
            }
        }
        public DateTime ReadX8000Time()
        {
            Log.Add($"Read X8000 current time", MsgLevel.Trace);
            if (!IsConnected) return DateTime.MinValue;

            string[] data = sendCommand($"TR");
            if (data[0] == "TR")
            {
                int yy = int.Parse(data[1]) + 2000;
                int mo = int.Parse(data[2]);
                int dd = int.Parse(data[3]);
                int hh = int.Parse(data[4]);
                int mm = int.Parse(data[5]);
                int ss = int.Parse(data[6]);
                DateTime dt = new DateTime(yy, mo, dd, hh, mm, ss);
                Log.Add($"X8000 current time {dt:yyyy-MM-dd HH:mm:ss}", MsgLevel.Trace);

                return dt;
            }
            else
            {
                return DateTime.MinValue;
            }
        }
        public bool WriteX8000Time(int yy, int mo, int dd, int hh, int mi, int ss)
        {
            Log.Add($"Set X8000 time {yy}-{mo}-{dd} {hh}:{mi}:{ss}", MsgLevel.Trace);
            if (!IsConnected) return false;

            string[] data = sendCommand($"TW,{yy},{mo},{dd},{hh},{mi},{ss}");
            return data[0] == "TW";
        }
        public string[] ReadX8000Version()
        {
            Log.Add($"Read X8000 version", MsgLevel.Trace);

            if (!IsConnected) return new string[] { "-1","-1" };

            string[] data = sendCommand($"VI");
            if (data[0] == "VI")
            {
                Log.Add($"X8000 version {data[1]} \\ {data[2]}", MsgLevel.Trace);
                return new string[] { data[1], data[2] };
            }
            else return null;
        }




        string[] ParseRecieveData(string data)
        {
            string removeCR = data.Remove(data.Length - 1);
            string[] splitData = removeCR.Split(',');
            if (splitData[0] == "ER")
            {
                int lastIndex = splitData.Length - 1;
                string command = splitData[1];
                switch (splitData[lastIndex])
                {
                    case "02":
                        //命令錯誤 (符合的命令不存在)
                        Log.Add($"x8k command {command} do not exist!", MsgLevel.Alarm);
                        break;

                    case "03":
                        //命令動作禁止 (接收的命令不能動作)
                        Log.Add($"x8k {command} reject!", MsgLevel.Warn);

                        break;

                    case "22":
                        //參數錯誤 (數據的值, 數量在範圍外)
                        Log.Add($"x8k {command} wrong parameter !", MsgLevel.Warn);

                        break;

                    case "91":
                        //超時錯誤
                        Log.Add($"x8k {command} time out!", MsgLevel.Warn);

                        break;

                    default:

                        break;
                }
            }
            return splitData;
        }

        public bool Connect()
        {
            if (client == null)
            {
                client = new TcpClient();
            }
            if (!client.Connected)
            {
                Log.Add("Connecting X8000.", MsgLevel.Info);
                status = "Connecting...";
                try
                {
                    IPAddress ip = IPAddress.Parse(IP);
                    
                    client.Connect(ip, Port);
                    Log.Add("X8000 connected.", MsgLevel.Info);
                    status = "Connected";
                    isConnected = true;
                }
                catch (Exception ex)
                {
                    isConnected = false;
                    status = "Connect Exception";
                    Log.Add("Cannot connect X8000.", MsgLevel.Warn,ex);
                }
            }
            else
            {
                isConnected = true;
                status = "Connected";

                Log.Add("X8000 has been connected!", MsgLevel.Trace);
            }
            return isConnected;
        }
        public void Disconnect()
        {
            client.Close();
            GC.Collect();
            status = "Disconnect";
            isConnected = false;
        }

        string[] sendCommand(string origCommand)
        {
            string command = $"{origCommand}\r";
            NetworkStream stream = client.GetStream();
            writeStream(stream, command);
            string recieveData = readStream(stream);
            UpdateCommand(recieveData);
            string[] splitData = ParseRecieveData(recieveData);
            return splitData;
        }
        void writeStream(NetworkStream stream,string command)
        {
            byte[] bytResponse = Encoding.ASCII.GetBytes(command);
            if (stream != null)
            {
                if (stream.CanWrite)
                {
                    Log.Add($"Write {command.Replace("\r","\\r")}", MsgLevel.Trace);
                    stream.Write(bytResponse, 0, bytResponse.Length);
                }
            }
        }
        string readStream(NetworkStream stream)
        {
            while (!stream.DataAvailable)
            {
                SpinWait.SpinUntil(() => false, 2);
            }
            byte[] bytBuffer = new byte[256];
            if (stream.CanRead)
            {
                int intCount = stream.Read(bytBuffer, 0, bytBuffer.Length);
                string strData = System.Text.Encoding.ASCII.GetString(bytBuffer, 0, intCount);
                Log.Add($"Recieve {strData.Replace("\r","\\r")}", MsgLevel.Trace);
                return strData;
            }
            else return "";
        }
        public void LoadYaml()
        {
            Log.Add("X8000 TCP Module Load Config.", MsgLevel.Trace);
            string folder = System.Environment.CurrentDirectory;
            string config_folder = $"{folder}\\Config";
            string file_path = $"{config_folder}\\{config_file_name}";

            if (!File.Exists(file_path))
            {
                Log.Add($"X8000 TCP  Module Config {file_path} Not Found.", MsgLevel.Warn);
                SaveYaml();
            }
            string ReadData = "";
            using (StreamReader sr = new StreamReader(file_path, Encoding.Default))
            {
                ReadData = sr.ReadToEnd();
            }
            var deserializer = new DeserializerBuilder()
                .WithNamingConvention(UnderscoredNamingConvention.Instance)  // see height_in_inches in sample yml 
                .Build();

            //yml contains a string containing your YAML
            var p = deserializer.Deserialize<X8000>(ReadData);
            this.IP = p.IP;
            this.Port = p.Port;

            Log.Add($"X8000 TCP  Module Config {file_path} Loaded.", MsgLevel.Trace);

        }
        public void SaveYaml()
        {
            string folder = System.Environment.CurrentDirectory;
            string config_folder = $"{folder}\\Config";
            string file_path = $"{config_folder}\\{config_file_name}";

            if (!Directory.Exists(config_folder)) Directory.CreateDirectory(config_folder);
            using (StreamWriter sw = new StreamWriter(file_path, false, Encoding.Default))
            {
                var serializer = new SerializerBuilder().WithNamingConvention(UnderscoredNamingConvention.Instance).Build();
                var yaml = serializer.Serialize(this);
                sw.WriteLine(yaml);
                sw.Flush();
            }
            Log.Add($"X8000 TCP  Module Config {file_path} Saved.", MsgLevel.Trace);

        }
    }

}

using RsLib.Common;
using RsLib.LogMgr;
using System;
using System.ComponentModel;
using System.IO;
using System.IO.Ports;
using System.Text;
using System.Threading;
using YamlDotNet.Serialization;
namespace RsLib.SerialPortLib
{
    public class EJ1500
    {
        public event Action<int, double,bool> WeightMeasured;
        public event Action<bool> Connected;
        RS232 _rs232;
        public EJ1500Setting Setting { get; private set; } = new EJ1500Setting();
        public bool IsConnected
        {
            get
            {
                if (_rs232 == null) return false;
                else return _rs232.IsConnected;
            }
        }

        //public int StableTime { get; private set; } = 1;
        public int Index => Setting.Index;
        int _stableCount = 0;
        double _weightSum = 0;
        bool _enableGetWeight = false;
        public bool IsSettingLoaded { get; private set; } = false;
        bool _isRaiseEvent = false;

        public string Status { get; private set; } = "Not Connect";
        public EJ1500()
        {

        }
        public EJ1500(int index)
        {
            Setting.Index = index;
        }
        public void SaveYaml(string filePath)
        {
            Setting.SaveYaml(filePath);
        }
        public void LoadYaml(string filePath)
        {
            if (!File.Exists(filePath))
            {
                SaveYaml(filePath);
            }
            string ReadData = "";
            using (StreamReader sr = new StreamReader(filePath, Encoding.Default))
            {
                ReadData = sr.ReadToEnd();
            }
            var deserializer = new DeserializerBuilder().
                IgnoreUnmatchedProperties()
                .Build();

            //yml contains a string containing your YAML
            var p = deserializer.Deserialize<EJ1500Setting>(ReadData);
            Setting = p.DeepClone();
            IsSettingLoaded = true;
        }

        private void _rs232_DataUpdated(string obj)
        {
            //"ST,+000105.2  g\r""

            if (obj.Contains("ST,"))
            {
                string[] split1 = obj.Split(',');
                if (split1.Length == 2)
                {
                    string[] split2 = split1[1].Split(' ');
                    string str_weight = split2[0];
                    double weight = 0;
                    if (double.TryParse(str_weight, out weight))
                    {
                        _stableCount++;
                        _weightSum += weight;
                        Log.Add($"Measure {_stableCount}/{Setting.MeasureCount} Get {weight:F2}", MsgLevel.Trace);

                        if (_stableCount >= Setting.MeasureCount)
                        {
                            double avgWeight = Math.Round(_weightSum / (double)Setting.MeasureCount, 1);
                            WeightMeasured?.Invoke(Setting.Index, avgWeight,_isRaiseEvent);
                            _stableCount = 0;
                            _weightSum = 0;
                            _enableGetWeight = false;
                            Status = "Weight measured";

                        }
                    }
                }
            }
        }
        public bool Connect()
        {
            if (Setting.Index <= 0) return false;
            if (Setting.PortName == "") return false;
            if (Setting.DataBits == 0) return false;
            if (IsSettingLoaded == false) return false;
            Status = $"{Setting.PortName} Connecting....";
            _rs232 = new RS232($"EJ1500_{Setting.Index}",
                Setting.PortName,
                Setting.BaudrateOption,
                Setting.ParityOption,
                Setting.DataBits,
                Setting.StopBitsOption);

            Log.Add($"EJ1500 {Setting.Index} {Setting.PortName} {Setting.BaudrateOption} {Setting.ParityOption} {Setting.DataBits} {Setting.StopBitsOption} is connecting...", MsgLevel.Info);
            _rs232.DataUpdated += _rs232_DataUpdated;
            _isRaiseEvent = false;
            _rs232.Start("Q\r",500);
            Connected?.Invoke(IsConnected);
            if (IsConnected)
            {
                Log.Add($"EJ1500 {Setting.Index} is connected.", MsgLevel.Info);
                Status = "Connected";
            }
            else
            {
                Log.Add($"EJ1500 {Setting.Index} cannot connect.", MsgLevel.Alarm);
                Status = "Disconnected";
            }
            return IsConnected;
        }
        public void Disconnect()
        {
            _rs232.Stop();
            Connected?.Invoke(IsConnected);
            Log.Add($"EJ1500 {Setting.Index} disconnect.", MsgLevel.Info);
            Status = "Disconnected";

        }
        public void Measure(bool isRaiseEventl)
        {
            if (_rs232 == null) return;
            if (_rs232.IsConnected == false) return;
            _isRaiseEvent = isRaiseEventl;
            _enableGetWeight = true;
            Status = "Weight measuring...";

            ThreadPool.QueueUserWorkItem(getWeight);
        }

        void getWeight(object obj)
        {

            while (_enableGetWeight)
            {
                _rs232.Send("Q\r");
                int waitTime = FT_Functions.IsTimeOut(2000, () => _rs232.ReadData != "", true);
                if(waitTime ==(int)eTimeOutType.TimeOut)
                {
                    Log.Add($"{Setting.PortName} read data time out.", MsgLevel.Warn);
                    _enableGetWeight = false;
                    Status = "Read data time out";
                }
                else
                {

                }
                SpinWait.SpinUntil(() => false, 150);
            }
        }
        public void SetSero()
        {
            if (_rs232 == null) return;
            if (_rs232.IsConnected == false) return;
            Status = "Zero setting...";

            Log.Add("Set Zero", MsgLevel.Info);
            _rs232.Send("Z\r");
        }
    }
    [Serializable]
    public class EJ1500Setting
    {
        [Category("1. Setting")]
        [DisplayName("Index")]
        public int Index { get; set; } = -1;

        [Category("1. Setting")]
        [DisplayName("Port Name")]
        public string PortName { get; set; } = "";

        [Category("1. Setting")]
        [DisplayName("Baud Rate")]
        public BaudRate BaudrateOption { get; set; } = BaudRate._9600;

        [Category("1. Setting")]
        [DisplayName("Parity")]
        public Parity ParityOption { get; set; } = Parity.None;

        [Category("1. Setting")]
        [DisplayName("Data Bits")]
        public int DataBits { get; set; } = 8;

        [Category("1. Setting")]
        [DisplayName("Stop Bits")]
        public StopBits StopBitsOption { get; set; } = StopBits.One;
        [Category("2. Measure")]
        [DisplayName("Measure Count")]
        public int MeasureCount { get; set; } = 1;

        public void SaveYaml(string filePath)
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

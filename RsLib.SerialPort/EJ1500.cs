using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO.Ports;
using RsLib.LogMgr;
using System.Threading;
using System.IO;
using YamlDotNet.Serialization;
using RsLib.Common;
using System.ComponentModel;
namespace RsLib.SerialPortLib
{
    public class EJ1500
    {
        public event Action<int,double> WeightMeasured;
        public event Action<bool> Connected;
        RS232 _rs232;
        public EJ1500Setting Setting { get;private set; } = new EJ1500Setting();
        public bool IsConnected
        {
            get
            {
                if (_rs232 == null) return false;
                else
                {
                    return _rs232.IsConnected;
                }
            }
        }
        public int StableTime { get; private set; } = 1;
        public int Index => Setting.Index;
        int _stableCount = 0;
        double _weightSum = 0;
        bool _enableGetWeight = false;
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
        }

        private void _rs232_DataUpdated(string obj)
        {
            //"ST,+000105.2  g\r""

            if(obj.Contains("ST,"))
            {
                string[] split1 = obj.Split(',');
                if(split1.Length == 2)
                {
                    string[] split2 = split1[1].Split(' ');
                    string str_weight = split2[0];
                    double weight = 0;
                    if(double.TryParse(str_weight,out weight))
                    {
                        _stableCount++;
                        _weightSum += weight;
                        Log.Add($"Weight {_stableCount} Get {weight:F2}", MsgLevel.Info);

                        if (_stableCount >= StableTime)
                        {
                            double avgWeight = Math.Round(_weightSum / (double)StableTime,1);
                            WeightMeasured?.Invoke(Setting.Index,avgWeight);
                            _stableCount = 0;
                            _weightSum = 0;
                            _enableGetWeight = false;
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

            _rs232 = new RS232($"EJ1500_{Setting.Index}",
                Setting.PortName,
                Setting.BaudrateOption,
                Setting.ParityOption,
                Setting.DataBits,
                Setting.StopBitsOption);

            _rs232.DataUpdated += _rs232_DataUpdated;

            _rs232.Start();
            Connected?.Invoke(IsConnected);
            return IsConnected;
        }
        public void Disconnect()
        {
            _rs232.Stop();
            Connected?.Invoke(IsConnected);

        }
        public void Measure(int time = 1)
        {
            if (_rs232 == null) return;
            if (_rs232.IsConnected == false) return;
            if (time <= 0) StableTime = 1;
            else StableTime = time;
            _enableGetWeight = true;
            ThreadPool.QueueUserWorkItem(getWeight);
        }

        void getWeight(object obj)
        {
            while (_enableGetWeight)
            {
                _rs232.Send("Q\r");
                SpinWait.SpinUntil(() => false, 150);
            }
        }
        public void SetSero()
        {
            if (_rs232 == null) return;
            if (_rs232.IsConnected == false) return;
            Log.Add("Set Zero", MsgLevel.Info);
            _rs232.Send("Z\r");
        }
    }
    [Serializable]
    public class EJ1500Setting
    {
        [DisplayName("Index")]
        public int Index { get; set; } = -1;
        [DisplayName("Port Name")]
        public string PortName { get; set; } = "";
        [DisplayName("Baud Rate")]
        public BaudRate BaudrateOption { get; set; } = BaudRate._9600;
        [DisplayName("Parity")]
        public Parity ParityOption { get; set; } = Parity.None;
        [DisplayName("Data Bits")]
        public int DataBits { get; set; } = 8;
        [DisplayName("Stop Bits")]
        public StopBits StopBitsOption { get; set; } = StopBits.One;
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

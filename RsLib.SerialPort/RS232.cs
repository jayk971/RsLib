using System;
using System.IO.Ports;
using RsLib.Common;
using RsLib.LogMgr;
namespace RsLib.SerialPortLib
{
    public class RS232
    {
        SerialPort _serialPort;
        public bool IsConnected { get; private set; } = false;
        public event Action<string> DataUpdated;
        public string ReadData { get; private set; } = "";
        public RS232()
        {
        }

        public RS232(string deviceName, string portName, BaudRate baudRate, Parity parity, int dataBits, StopBits stopBits)
        {

            _serialPort = new SerialPort(portName, (int)baudRate, parity, dataBits, stopBits);
        }
        public static string[] DetectSerialPort()
        {
            string[] portNames = SerialPort.GetPortNames();
            return portNames;
        }

        //public void Start()
        //{
        //    _serialPort.Open();
        //    _serialPort.DataReceived += _serialPort_DataReceived;
        //}
        public void Start(string testCommand,double waitTime)
        {
            _serialPort.Open();
            _serialPort.DataReceived += _serialPort_DataReceived;
            for (int i = 0; i < 5; i++)
            {
                Send(testCommand);
                int isTimeOut = FT_Functions.IsTimeOut(waitTime, () => ReadData != "", true);
                if (isTimeOut == (int)eTimeOutType.TimeOut)
                {
                    if (i == 4)
                    {
                        IsConnected = false;
                        Stop();
                        Log.Add($"Serial port {_serialPort.PortName} connect fail.", MsgLevel.Warn);
                    }
                    else
                    {
                        Log.Add($"Serial port {_serialPort.PortName} connect fail. Retry {i+1} / 5", MsgLevel.Warn);
                    }
                }
                else
                {
                    IsConnected = true;
                    break;
                }
            }
        }
        public void Stop()
        {
            _serialPort.DataReceived -= _serialPort_DataReceived;
            _serialPort.Close();
            IsConnected = false;

        }
        public void Send(string data)
        {
            ReadData = "";
            _serialPort.WriteLine(data);
        }
        private void _serialPort_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            SerialPort s = (SerialPort)sender;
            ReadData = s.ReadLine();
            DataUpdated?.Invoke(ReadData);
        }
    }


    public enum BaudRate : int
    {
        _110 = 110,
        _300 = 300,
        _600 = 600,
        _1200 = 1200,
        _2400 = 2400,
        _4800 = 4800,
        _9600 = 9600,
        _14400 = 14400,
        _19200 = 19200,
        _38400 = 38400,
        _57600 = 57600,
        _115200 = 115200,
        _128000 = 128000,
        _256000 = 256000,
    }

}

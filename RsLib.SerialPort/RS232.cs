using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.IO.Ports;
using RsLib.LogMgr;
namespace RsLib.SerialPortLib
{
    public class RS232
    {
        public string DeviceName { get; private set; } = "";
        public string PortName { get; private set; } = "";
        SerialPort _serialPort;
        
        bool _enableTd = false;
        bool _TdRunning = false;

        public event Action<string> DataUpdated;
        public RS232()
        {

        }

        public RS232(string deviceName,string portName,BaudRate baudRate, Parity parity,int dataBits, StopBits stopBits)
        {
            DeviceName = deviceName;
            PortName = portName;
            Log.Add($"RS232 {deviceName} {portName} {baudRate} {parity} {dataBits} {stopBits}",MsgLevel.Info);
            _serialPort = new SerialPort(portName, (int)baudRate, parity, dataBits, stopBits);
        }

        public static string[] DetectSerialPort()
        {
            string[] portNames = SerialPort.GetPortNames();
            return portNames;
        }

        public void Start()
        {
            _enableTd = true;

            ThreadPool.QueueUserWorkItem(run);
        }
        public void Stop()
        {
            _enableTd = false;
        }
        void run(object obj)
        {
            _TdRunning = true;
            _serialPort.Open();
            while(_enableTd)
            {
                if (_serialPort.IsOpen)
                {
                    if (_serialPort.BytesToRead > 0)
                    {
                        string data = _serialPort.ReadLine();
                        DataUpdated?.Invoke(data);
                    }
                }
                SpinWait.SpinUntil(() => false, 100);
            }
            _serialPort.Close();
            _TdRunning = false;

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

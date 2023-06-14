using System;
using System.IO.Ports;
namespace RsLib.SerialPortLib
{
    public class RS232
    {
        SerialPort _serialPort;
        public bool IsConnected
        {
            get
            {
                if (_serialPort == null) return false;
                else
                {
                    return _serialPort.IsOpen;
                }
            }
        }
        public event Action<string> DataUpdated;
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

        public void Start()
        {
            _serialPort.Open();
            _serialPort.DataReceived += _serialPort_DataReceived;
        }
        public void Stop()
        {
            _serialPort.DataReceived -= _serialPort_DataReceived;
            _serialPort.Close();
        }
        public void Send(string data)
        {
            _serialPort.WriteLine(data);
        }
        private void _serialPort_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            SerialPort s = (SerialPort)sender;
            string data = s.ReadLine();
            DataUpdated?.Invoke(data);
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

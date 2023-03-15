using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.IO.Ports;
namespace RsLib.SerialPortLib
{
    public class RS232
    {
        SerialPort _serialPort;
        bool _enableTd = false;
        bool _TdRunning = false;

        public event Action<string> DataUpdated;
        public RS232()
        {
            _serialPort = new SerialPort("COM1", 9600, Parity.Even, 8, StopBits.One);
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
}

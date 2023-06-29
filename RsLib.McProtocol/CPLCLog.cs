using System;
using System.IO;

namespace RsLib.McProtocol
{
    internal class CPLCLog
    {
        public CPLCLog()
        {
        }

        private string GetDeviceString(Device device, int iAddress)
        {
            string str;
            string[] strArrays;
            if ((device == Device.B || device == Device.W || device == Device.X ? false : device != Device.Y))
            {
                strArrays = new string[] { "[", device.ToString(), " ", iAddress.ToString("d6"), "]" };
                str = string.Concat(strArrays);
            }
            else
            {
                strArrays = new string[] { "[", device.ToString(), " ", iAddress.ToString("X6"), "]" };
                str = string.Concat(strArrays);
            }
            return str;
        }

        public void MakeLog(int[] iaSend, Device device, int iAddress, int iCount, bool bSuccess)
        {
            string str = "";
            string str1 = (bSuccess ? "  Completed" : "  Fault");
            string deviceString = this.GetDeviceString(device, iAddress);
            string[] strArrays = new string[] { DateTime.Now.ToString("HH:mm:ss  "), "Write To => PLC ", deviceString, " Count:", iCount.ToString(), str1, "\r\n" };
            string str2 = string.Concat(strArrays);
            for (int i = 0; i < (int)iaSend.Length; i++)
            {
                if (i == 0)
                {
                    str = "[ Write Data ] \t";
                }
                str = string.Concat(str, " ", iaSend[i].ToString("X4"));
                if (((i + 1) % 20 != 0 ? false : i > 0))
                {
                    str = string.Concat(str, "\r\n  \t\t");
                }
            }
            str2 = string.Concat(str2, str, "\r\n\r\n");
            this.SaveLog(str2);
        }

        public void MakeLog(int iSend, Device device, int iAddress, bool bSuccess)
        {
            string str = (bSuccess ? "  Completed" : "  Fault");
            string deviceString = this.GetDeviceString(device, iAddress);
            string[] strArrays = new string[] { DateTime.Now.ToString("HH:mm:ss  "), "Write To => PLC ", deviceString, str, "\r\n" };
            string str1 = string.Concat(string.Concat(strArrays), "[ Write Data ] \t");
            str1 = string.Concat(str1, " ", iSend.ToString("X4"), "\r\n\r\n");
            this.SaveLog(str1);
        }

        public void MakeLog(bool[] baSend, int iAddress, Device device, int iCount, bool bSuccess)
        {
            string str = "";
            string str1 = (bSuccess ? "  Completed" : "  Fault");
            string deviceString = this.GetDeviceString(device, iAddress);
            string[] strArrays = new string[] { DateTime.Now.ToString("HH:mm:ss  "), "Write To => PLC ", deviceString, " Count:", iCount.ToString(), str1, "\r\n" };
            string str2 = string.Concat(strArrays);
            for (int i = 0; i < (int)baSend.Length; i++)
            {
                string str3 = (baSend[i] ? "1" : "0");
                if (i == 0)
                {
                    str = "[ Write Data ] \t";
                }
                str = string.Concat(str, " ", str3);
                if (((i + 1) % 20 != 0 ? false : i > 0))
                {
                    str = string.Concat(str, "\r\n  \t\t");
                }
            }
            str2 = string.Concat(str2, str, "\r\n\r\n");
            this.SaveLog(str2);
        }

        public void MakeLog(bool bBit, Device device, int iAddress, bool bSuccess)
        {
            string str = (bSuccess ? "  Completed" : "  Fault");
            string deviceString = this.GetDeviceString(device, iAddress);
            string str1 = (bBit ? "1" : "0");
            string[] strArrays = new string[] { DateTime.Now.ToString("HH:mm:ss  "), "Write To => PLC ", deviceString, " ", str, "\r\n" };
            string str2 = string.Concat(strArrays);
            this.SaveLog(string.Concat(str2, "[ Write Data ] \t", str1, "\r\n\r\n"));
        }

        private void SaveLog(string sLog)
        {
            lock (this)
            {
                DateTime now = DateTime.Now;
                string str = string.Concat("c:\\\\log\\", now.ToString("yyMMdd"));
                int hour = DateTime.Now.Hour;
                string str1 = string.Concat(hour.ToString("D2"), "_PLClog.txt");
                string str2 = string.Concat(str, "\\", str1);
                if (!Directory.Exists(str))
                {
                    Directory.CreateDirectory(str);
                }
                StreamWriter streamWriter = File.AppendText(str2);
                if (!File.Exists(str2))
                {
                    File.CreateText(str2);
                }
                streamWriter.Write(sLog);
                streamWriter.Close();
            }
        }
    }
}

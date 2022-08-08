using System;
using RsLib.LogMgr;
namespace RsLib.McProtocol
{
    public class PLC_Binary : CTCPIP
    {
        private CPLCLog m_Logger = new CPLCLog();
        private bool m_bLogEnable;
        public PLC_Binary(string PLCName)
        {
            Name = PLCName;
        }
        public bool LogWritePLC
        {
            get
            {
                return this.m_bLogEnable;
            }
            set
            {
                this.m_bLogEnable = value;
            }
        }
        public void Close()
        {
            this.m_bStop = true;
        }
        private bool IsBitDevice(Device device)
        {
            bool flag = false;
            if ((device == Device.B || device == Device.M || device == Device.X ? true : device == Device.Y))
            {
                flag = true;
            }
            return flag;
        }
        private bool IsWordDevice(Device device)
        {
            bool flag = false;
            if ((device == Device.D || device == Device.W || device == Device.R ? true : device == Device.ZR))
            {
                flag = true;
            }
            return flag;
        }
        private int MakeSendByteArr(int[] NumArr, ref byte[] ByteArr, int Count)
        {
            ByteArr = new byte[Count * 2];
            for (int i = 0; i < Count; i++)
            {
                ByteArr[i * 2] = (byte)(NumArr[i] % 256);
                ByteArr[i * 2 + 1] = (byte)(NumArr[i] / 256);
            }
            return (int)NumArr.Length * 2;
        }
        private int MakeSendByteArr(bool[] BoolArr, ref byte[] ByteArr, int Count)
        {
            ByteArr = new byte[Count / 2];
            for (int i = 0; i < Count; i++)
            {
                int num = Convert.ToByte(BoolArr[i * 2]) * 16;
                int num1 = Convert.ToByte(BoolArr[i * 2 + 1]);
                ByteArr[i] = (byte)(num + num1);
            }
            return (int)ByteArr.Length;
        }
        private bool[] PLCDataToBoolArr(byte[] ByteArr, int Count)
        {
            if (((int)ByteArr.Length - 2) * 2 < Count)
            {
                Count = ((int)ByteArr.Length - 2) * 2;
            }
            bool[] flag = new bool[Count];
            for (int i = 0; i < Count; i++)
            {
                int num = i / 2;
                int num1 = i % 2;
                if (num1 == 0)
                {
                    flag[i] = Convert.ToBoolean(ByteArr[num + 2] & 16);
                }
                else if (num1 == 1)
                {
                    flag[i] = Convert.ToBoolean(ByteArr[num + 2] & 1);
                }
            }
            return flag;
        }
        private int[] PLCDataToNumArr(byte[] ByteArr)
        {
            int[] byteArr = new int[((int)ByteArr.Length - 2) / 2];
            for (int i = 0; i < (int)byteArr.Length; i++)
            {
                byteArr[i] = ByteArr[i * 2 + 2] + ByteArr[i * 2 + 3] * 256;
            }
            return byteArr;
        }
        public bool ReadBit(int StartAddress, Device device, ref bool[] ReadBit, short Count)
        {
            bool flag = false;
            byte num = (byte)device;
            int num1 = 21;
            short[] numArray = new short[0];
            byte[] numArray1 = new byte[0];
            byte[] numArray2 = new byte[0];
            byte[] startAddress = new byte[21];
            lock (this)
            {
                if (this.m_enConState == ConState.Connected & this.IsBitDevice(device))
                {
                    this.WordReadIniFormat(ref startAddress);
                    num = (byte)device;
                    startAddress[13] = 1;
                    startAddress[14] = 0;
                    startAddress[16] = (byte)(StartAddress / 256);
                    startAddress[15] = (byte)(StartAddress % 256);
                    startAddress[18] = num;
                    startAddress[19] = (byte)(Count % 256);
                    startAddress[20] = (byte)(Count / 256);
                    Array.Copy(numArray1, 0, startAddress, 21, (int)numArray1.Length);
                    base.SendSocket(startAddress, num1);
                    if (base.ReadSocket(ref numArray2, 9) == 9)
                    {
                        int num2 = numArray2[7] + numArray2[8] * 256;
                        base.ReadSocket(ref numArray2, num2);
                        if ((numArray2[0] != 0 ? false : numArray2[1] == 0))
                        {
                            ReadBit = this.PLCDataToBoolArr(numArray2, Count);
                            flag = true;
                        }
                    }
                }
            }
            return flag;
        }
        public bool ReadWord(int StartAddress, Device device, ref int[] ReadWord, short Count)
        {
            bool flag = false;
            byte num = (byte)device;
            int num1 = 21;
            short[] numArray = new short[0];
            byte[] numArray1 = new byte[0];
            byte[] numArray2 = new byte[0];
            byte[] startAddress = new byte[21];
            try
            {
                lock (this)
                {
                    if (this.m_enConState == ConState.Connected & this.IsWordDevice(device))
                    {
                        this.WordReadIniFormat(ref startAddress);
                        num = (byte)device;
                        startAddress[13] = 0;
                        startAddress[14] = 0;
                        startAddress[16] = (byte)(StartAddress / 256);
                        startAddress[15] = (byte)(StartAddress % 256);
                        startAddress[18] = num;
                        startAddress[19] = (byte)(Count % 256);
                        startAddress[20] = (byte)(Count / 256);
                        Array.Copy(numArray1, 0, startAddress, 21, (int)numArray1.Length);
                        base.SendSocket(startAddress, num1);
                        if (base.ReadSocket(ref numArray2, 9) == 9)
                        {
                            int num2 = numArray2[7] + numArray2[8] * 256;
                            base.ReadSocket(ref numArray2, num2);
                            if ((numArray2[0] != 0 ? false : numArray2[1] == 0))
                            {
                                ReadWord = this.PLCDataToNumArr(numArray2);
                                flag = true;
                            }
                        }
                    }
                }
            }
            catch(Exception e)
            {
                Log.Add($"{logger_ip} ReadWord exception", MsgLevel.Alarm, e);
                //logger.ErrorException(logger_ip, e);
                //Write_Log(e.Message, "ReadWord");
            }
            return flag;
        }
        private void WordReadIniFormat(ref byte[] WordRead)
        {
            WordRead[0] = 80;
            WordRead[1] = 0;
            WordRead[2] = 0;
            WordRead[3] = 255;
            WordRead[4] = 255;
            WordRead[5] = 3;
            WordRead[6] = 0;
            WordRead[7] = 12;
            WordRead[8] = 0;
            WordRead[9] = 16;
            WordRead[10] = 0;
            WordRead[11] = 1;
            WordRead[12] = 4;
            WordRead[15] = 1;
            WordRead[16] = 0;
            WordRead[17] = 0;
            WordRead[18] = 168;
            WordRead[19] = 12;
            WordRead[20] = 3;
        }
        private void WordWriteIniFormat(ref byte[] WordWrite)
        {
            WordWrite[0] = 80;
            WordWrite[1] = 0;
            WordWrite[2] = 0;
            WordWrite[3] = 255;
            WordWrite[4] = 255;
            WordWrite[5] = 3;
            WordWrite[6] = 0;
            WordWrite[9] = 16;
            WordWrite[10] = 0;
            WordWrite[11] = 1;
            WordWrite[12] = 20;
            WordWrite[17] = 0;
            WordWrite[18] = 168;
        }
        public bool WriteBit(bool[] WriteBitAy, int StartAddress, Device device, short Count)
        {
            bool flag = false;
            byte[] numArray = new byte[0];
            byte[] numArray1 = new byte[0];
            int count = Count / 2 + Count % 2;
            byte[] startAddress = new byte[count + 21];
            lock (this)
            {
                if (this.m_enConState == ConState.Connected & this.IsBitDevice(device))
                {
                    this.WordWriteIniFormat(ref startAddress);
                    byte num = (byte)device;
                    this.MakeSendByteArr(WriteBitAy, ref numArray, Count);
                    startAddress[7] = (byte)((12 + count) % 256);
                    startAddress[8] = (byte)((12 + count) / 256);
                    startAddress[13] = 1;
                    startAddress[14] = 0;
                    startAddress[15] = (byte)(StartAddress % 256);
                    startAddress[16] = (byte)(StartAddress / 256);
                    startAddress[18] = num;
                    startAddress[19] = (byte)(Count % 256);
                    startAddress[20] = (byte)(Count / 256);
                    Array.Copy(numArray, 0, startAddress, 21, (int)numArray.Length);
                    base.SendSocket(startAddress, count + 21);
                    if (base.ReadSocket(ref numArray1, 9) == 9)
                    {
                        int num1 = numArray1[7] + numArray1[8] * 256;
                        base.ReadSocket(ref numArray1, num1);
                        if ((numArray1[0] != 0 ? false : numArray1[1] == 0))
                        {
                            flag = true;
                        }
                    }
                }
                if (this.m_bLogEnable)
                {
                    this.m_Logger.MakeLog(WriteBitAy, StartAddress, device, Count, flag);
                }
            }
            return flag;
        }
        public bool WriteBit(bool WriteBit, int StartAddress, Device device)
        {
            bool flag = false;
            byte[] numArray = new byte[0];
            byte[] numArray1 = new byte[0];
            byte[] startAddress = new byte[22];
            lock (this)
            {
                if (this.m_enConState == ConState.Connected & this.IsBitDevice(device))
                {
                    this.WordWriteIniFormat(ref startAddress);
                    byte num = (byte)device;
                    startAddress[7] = 13;
                    startAddress[8] = 0;
                    startAddress[13] = 1;
                    startAddress[14] = 0;
                    startAddress[15] = (byte)(StartAddress % 256);
                    startAddress[16] = (byte)(StartAddress / 256);
                    startAddress[18] = num;
                    startAddress[19] = 1;
                    startAddress[20] = 0;
                    startAddress[21] = (byte)(Convert.ToByte(WriteBit) * 16);
                    base.SendSocket(startAddress, 22);
                    if (base.ReadSocket(ref numArray1, 9) == 9)
                    {
                        int num1 = numArray1[7] + numArray1[8] * 256;
                        base.ReadSocket(ref numArray1, num1);
                        if ((numArray1[0] != 0 ? false : numArray1[1] == 0))
                        {
                            flag = true;
                        }
                    }
                }
                if (this.m_bLogEnable)
                {
                    this.m_Logger.MakeLog(WriteBit, device, StartAddress, flag);
                }
            }
            return flag;
        }
        public bool WriteWord(int[] aWriteWord, int StartAddress, Device device, short Count)
        {
            bool flag = false;
            byte[] numArray = new byte[0];
            byte[] numArray1 = new byte[0];
            int count = 21 + Count * 2;
            byte[] startAddress = new byte[count];
            lock (this)
            {
                if (this.m_enConState == ConState.Connected & this.IsWordDevice(device))
                {
                    this.WordWriteIniFormat(ref startAddress);
                    byte num = (byte)device;
                    this.MakeSendByteArr(aWriteWord, ref numArray, Count);
                    startAddress[7] = (byte)((12 + Count * 2) % 256);
                    startAddress[8] = (byte)((12 + Count * 2) / 256);
                    startAddress[13] = 0;
                    startAddress[14] = 0;
                    startAddress[15] = (byte)(StartAddress % 256);
                    startAddress[16] = (byte)(StartAddress / 256);
                    startAddress[18] = num;
                    startAddress[19] = (byte)(Count % 256);
                    startAddress[20] = (byte)(Count / 256);
                    Array.Copy(numArray, 0, startAddress, 21, (int)numArray.Length);
                    base.SendSocket(startAddress, count);
                    if (base.ReadSocket(ref numArray1, 9) == 9)
                    {
                        int num1 = numArray1[7] + numArray1[8] * 256;
                        base.ReadSocket(ref numArray1, num1);
                        if ((numArray1[0] != 0 ? false : numArray1[1] == 0))
                        {
                            flag = true;
                        }
                    }
                }
                if (this.m_bLogEnable)
                {
                    this.m_Logger.MakeLog(aWriteWord, device, StartAddress, Count, flag);
                }
            }
            return flag;
        }
        public bool WriteWord(int WriteWord, int StartAddress, Device device)
        {
            bool flag = false;
            byte[] numArray = new byte[0];
            byte[] startAddress = new byte[23];
            lock (this)
            {
                if (this.m_enConState == ConState.Connected & this.IsWordDevice(device))
                {
                    this.WordWriteIniFormat(ref startAddress);
                    byte num = (byte)device;
                    startAddress[7] = 14;
                    startAddress[8] = 0;
                    startAddress[13] = 0;
                    startAddress[14] = 0;
                    startAddress[15] = (byte)(StartAddress % 256);
                    startAddress[16] = (byte)(StartAddress / 256);
                    startAddress[18] = num;
                    startAddress[19] = 1;
                    startAddress[20] = 0;
                    startAddress[21] = (byte)(WriteWord % 256);
                    startAddress[22] = (byte)(WriteWord / 256);
                    base.SendSocket(startAddress, 23);
                    if (base.ReadSocket(ref numArray, 9) == 9)
                    {
                        int num1 = numArray[7] + numArray[8] * 256;
                        base.ReadSocket(ref numArray, num1);
                        if ((numArray[0] != 0 ? false : numArray[1] == 0))
                        {
                            flag = true;
                        }
                    }
                }
                if (this.m_bLogEnable)
                {
                    this.m_Logger.MakeLog(WriteWord, device, StartAddress, flag);
                }
            }
            return flag;
        }
    }
}

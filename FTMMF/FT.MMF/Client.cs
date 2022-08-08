using System;
using System.IO;
using System.IO.MemoryMappedFiles;
using System.Text;
using System.Threading;

namespace RsLib.MMF
{
    public class MMFClient
    {
        private MemoryMappedFile mmfW;
        private MemoryMappedFile mmfR;
        private Thread td;
        public bool IsRun = false;

        public int DO = 0;
        public int DI = 0;

        private int LastDi = -1;
        private int LastDo = -1;

        private string SendMsg = "";
        private string LastSendMsg = "";

        private string ReceiveMsg = "";
        private string LastRMsg = "";

        public long MemoryCapacity = 1024;

        private bool IsStop = true;
        private bool IsTdStop = true;

        public delegate void DICallBack(int DI);
        public event DICallBack GetDiValue;

        public delegate void DOCallBack(int DO);
        public event DOCallBack GetDOValue;

        public delegate void MsgCallBack(string Msg);
        public event MsgCallBack GetMsg;

        public int LoopInterval = 500;

        public MMFClient(string ReadMapName, string WriteMapName, long l_MemoryCapacity)
        {
            MemoryCapacity = l_MemoryCapacity;
            mmfW = MemoryMappedFile.CreateOrOpen(WriteMapName, MemoryCapacity, MemoryMappedFileAccess.ReadWrite);
            mmfR = MemoryMappedFile.CreateOrOpen(ReadMapName, MemoryCapacity, MemoryMappedFileAccess.ReadWrite);
            GetDOValue += new DOCallBack(MMFClient_GetDOValue);
            GetDiValue += new DICallBack(MMFClient_GetDiValue);
            GetMsg += new MsgCallBack(MMFClient_GetMsg);
        }

        public MMFClient()
        {
            mmfW = MemoryMappedFile.CreateOrOpen(Common.str_Client2Server, MemoryCapacity, MemoryMappedFileAccess.ReadWrite);
            mmfR = MemoryMappedFile.CreateOrOpen(Common.str_Server2Client, MemoryCapacity, MemoryMappedFileAccess.ReadWrite);
            GetDOValue += new DOCallBack(MMFClient_GetDOValue);
            GetDiValue += new DICallBack(MMFClient_GetDiValue);
            GetMsg += new MsgCallBack(MMFClient_GetMsg);
        }
        public void Start()
        {

            IsStop = false;
            IsTdStop = false;

            td = new Thread(new ThreadStart(Run));
            td.IsBackground = true;
            td.Start();
        }

        public void Stop()
        {
            DO = 0;
            GetDOValue(DO);

            DI = 0;
            GetDiValue(DI);

            IsStop = true;

            //td.Abort();
            while (!IsTdStop)
            {
                SpinWait.SpinUntil(() => false, 500);
            }

        }
        private void Run()
        {
            IsRun = true;
            try
            {
                while (!IsStop)
                {
                    if (LastDo != DO || LastSendMsg != SendMsg)
                    {
                        MemoryMappedViewStream mmvsW = mmfW.CreateViewStream();

                        if (mmvsW.CanWrite)
                        {
                            byte[] msg = Encoding.UTF8.GetBytes(SendMsg);
                            using (BinaryWriter bw = new BinaryWriter(mmvsW))
                            {
                                bw.Write(DO);
                                bw.Write(msg.Length);
                                bw.Write(msg);

                                LastDo = DO;
                                GetDOValue(DO);
                                LastSendMsg = SendMsg;
                            }
                        }
                        mmvsW.Close();
                        //SendMsg = "";
                    }

                    MemoryMappedViewStream mmvsR = mmfR.CreateViewStream();

                    if (mmvsR.CanRead)
                    {
                        using (var br = new BinaryReader(mmvsR))
                        {
                            DI = br.ReadInt32();
                            int ReadMsgLen = br.ReadInt32();
                            ReceiveMsg = Encoding.UTF8.GetString(br.ReadBytes(ReadMsgLen), 0, ReadMsgLen);
                            if (LastDi != DI)
                            {
                                LastDi = DI;
                                GetDiValue(DI);
                            }
                            if (ReceiveMsg != "")
                            {
                                if (ReceiveMsg != LastRMsg)
                                {
                                    LastRMsg = ReceiveMsg;
                                    GetMsg(ReceiveMsg);
                                }
                            }
                        }

                        mmvsR.Close();
                        ReceiveMsg = "";

                    }
                    SpinWait.SpinUntil(() => false, LoopInterval);
                }//end while
            }
            catch (Exception ex)
            {
                IsRun = false;
            }
            IsRun = false;
            IsTdStop = true;
        }
        public void SendMessage(string Text)
        {
            SendMsg = Text;
        }
        private void MMFClient_GetDOValue(int Do)
        {

        }
        private void MMFClient_GetDiValue(int Di)
        {

        }
        private void MMFClient_GetMsg(string Msg)
        {

        }

    }

}

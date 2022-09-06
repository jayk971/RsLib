using System;
using System.IO;
using System.IO.MemoryMappedFiles;
using System.Text;
using System.Threading;
using RsLib.LogMgr;
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

        public event Action<int> DiValueUpdated;

        public event Action<int> DoValueUpdated;

        public event Action<string> MsgUpdated;

        public int LoopInterval = 500;

        public MMFClient(string ReadMapName, string WriteMapName, long l_MemoryCapacity)
        {
            MemoryCapacity = l_MemoryCapacity;
            mmfW = MemoryMappedFile.CreateOrOpen(WriteMapName, MemoryCapacity, MemoryMappedFileAccess.ReadWrite);
            mmfR = MemoryMappedFile.CreateOrOpen(ReadMapName, MemoryCapacity, MemoryMappedFileAccess.ReadWrite);
        }

        public MMFClient()
        {
            mmfW = MemoryMappedFile.CreateOrOpen(Common.str_Client2Server, MemoryCapacity, MemoryMappedFileAccess.ReadWrite);
            mmfR = MemoryMappedFile.CreateOrOpen(Common.str_Server2Client, MemoryCapacity, MemoryMappedFileAccess.ReadWrite);
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
            DoValueUpdated?.Invoke(DO);

            DI = 0;
            DiValueUpdated?.Invoke(DI);

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
                Log.Add("MMF client thread running.", MsgLevel.Info);
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
                                DoValueUpdated?.Invoke(DO);
                                LastSendMsg = SendMsg;
                                Log.Add($"MMF client send {DO} & {SendMsg}.", MsgLevel.Trace);

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
                                DiValueUpdated?.Invoke(DI);
                                Log.Add($"MMF client receive int {DI}.", MsgLevel.Trace);
                            }
                            if (ReceiveMsg != "")
                            {
                                if (ReceiveMsg != LastRMsg)
                                {
                                    LastRMsg = ReceiveMsg;
                                    MsgUpdated?.Invoke(ReceiveMsg);
                                    Log.Add($"MMF client receive string {ReceiveMsg}.", MsgLevel.Trace);
                                }
                            }
                        }

                        ReceiveMsg = "";

                    }
                    mmvsR.Close();
                    SpinWait.SpinUntil(() => false, LoopInterval);
                }//end while
                Log.Add("MMF client thread stopped.", MsgLevel.Info);
            }
            catch (Exception ex)
            {
                Log.Add("MMF client exception", MsgLevel.Alarm, ex);
            }
            finally
            {
                IsRun = false;
                IsTdStop = true;
            }
        }
        public void SendMessage(string Text)
        {
            SendMsg = Text;
        }
    }

}

using System.IO;
using System.IO.MemoryMappedFiles;
using System.Text;

namespace RsLib.MMF
{
    public class MMFReadWrite
    {
        public string MapName = "tmpCom";
        public long MapCapacity = 128;

        private MemoryMappedFile mmf;

        public MMFReadWrite()
        {
            mmf = MemoryMappedFile.CreateOrOpen(MapName, MapCapacity, MemoryMappedFileAccess.ReadWrite);
        }

        public void Send(string Sendmsg)
        {
            MemoryMappedViewStream mmvs = mmf.CreateViewStream();

            if (mmvs.CanWrite)
            {
                byte[] msg = Encoding.UTF8.GetBytes(Sendmsg);

                using (BinaryWriter bw = new BinaryWriter(mmvs))
                {
                    bw.Write(msg.Length);
                    bw.Write(msg);
                }
            }
            mmvs.Close();
        }
        public string Receive()
        {
            MemoryMappedViewStream mmvs = mmf.CreateViewStream();
            string ReceiveMsg = "";
            if (mmvs.CanRead)
            {
                using (var br = new BinaryReader(mmvs))
                {
                    int ReadMsgLen = br.ReadInt32();
                    ReceiveMsg = Encoding.UTF8.GetString(br.ReadBytes(ReadMsgLen), 0, ReadMsgLen);

                }

            }
            mmvs.Close();
            return ReceiveMsg;
        }

    }
}

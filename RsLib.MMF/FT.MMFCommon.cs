namespace RsLib.MMF
{
    public class Common
    {
        public const string str_Server2Client = "S2C";
        public const string str_Client2Server = "C2S";


        public static int GetIntSomeBit(int Source, int BitNum)
        {
            return Source >> BitNum & 1;
        }
        public static bool GetboolSomeBit(int Source, int BitNum)
        {
            return ((Source >> BitNum & 1) == 1) ? true : false;
        }

        public static int SetIntSomeBit(int Source, int BitNum, bool OnOff)
        {
            if (OnOff)
                Source |= (0x1 << BitNum);
            else
                Source &= ~(0x1 << BitNum);

            return Source;
        }
    }
}

//                       _oo0oo_
//                      o8888888o
//                      88" . "88
//                      (| -_- |)
//                      0\  =  /0
//                    ___/`---'\___
//                  .' \\|     |// '.
//                 / \\|||  :  |||// \
//                / _||||| -:- |||||- \
//               |   | \\\  -  /// |   |
//               | \_|  ''\---/''  |_/ |
//               \  .-\__  '-'  ___/-. /
//             ___'. .'  /--.--\  `. .'___
//          ."" '<  `.___\_<|>_/___.' >' "".
//         | | :  `- \`.;`\ _ /`;.`/ - ` : | |
//         \  \ `_.   \_ __\ /__ _/   .-` /  /
//     =====`-.____`.___ \_____/___.-`___.-'=====
//                       `=---='
//     ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
//               佛祖保佑         永無bug
//***************************************************
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Sockets;
using System.Net;
using System.Threading.Tasks;
using System.IO;

namespace RsLib.McProtocol
{
    public class PLC_ASCII
    {
        TcpClient client;
        NetworkStream Stream;
        public void PLC_Connect(string PLC_IP, int PLC_Port)
        {
            client = new TcpClient();
            try
            {
                IPEndPoint PLCIP = new IPEndPoint(IPAddress.Parse(PLC_IP), PLC_Port);
                client.Connect(PLCIP);
                delay(500);
            }
            catch { }
        }
        public void PLC_Disconnect()//PLC 連線中斷
        {
            try
            {
                if (client != null)
                {
                    client.GetStream().Close();
                    client.Close();
                    client = null;
                }
            }
            catch { }
        }
        public bool Check_Connection_Status(string PLC_IP, int PLC_Port)
        {
            bool status_code = true;
            try
            {
                //byte[] tmp = new byte[1];
                byte[] tmp = Encoding.ASCII.GetBytes("tx"); //這邊
                // clientSocket.Blocking = false;                
                client.Client.Send(tmp); //主要是這邊   
            }
            catch (SocketException e)
            {
                // 10035 == WSAEWOULDBLOCK
                if (e.NativeErrorCode.Equals(10035))
                    status_code = true;
                else
                {
                    status_code = false;
                    PLC_Disconnect();
                    PLC_Connect(PLC_IP, PLC_Port);
                }
            }
            return status_code;
        }
        public int[] ReadPLC(Device_ASCII DeviceName, string StartAddress, int intIndex)
        {
            string strCmd = "";
            List<string> str_List = new List<string>();
            int[] Response_Array = new int[intIndex];
            try
            {
                str_List = listPLCSetting(str_List);//載入前面相同的 Common Code          
                str_List.Add("0018");//Request data length, 計算以下所有的位元數(Hex)  ~  第六筆:str_List(5)
                str_List.Add("0005");//CPU monitoring timer(0005)
                str_List.Add("0401");//Command
                str_List.Add(device_enum((int)DeviceName));//Subcommand Word = "0000"  Bit = "0001"
                str_List.Add(DeviceName + "*");//Device code  ( M* or D* or ~~)
                str_List.Add(StartAddress.PadLeft(6, '0'));
                str_List.Add(Convert.ToString(intIndex, 16).PadLeft(4, '0'));
                str_List[5] = strDateLenght(str_List); //Request data length, 重新計算以下所有的位元數(Dec->Hex)  ~  第六筆:str_List(5)
                strCmd = strCommonCode(str_List); // 組合命令碼
                Response_Array = Read_writetoPLC(strCmd, intIndex);
            }
            catch
            {
            }
            return Response_Array;
        }
        public void WritePLC(Device_ASCII DeviceName, string StartAddress, int[] inputvalue)
        {
            string str_device = device_enum((int)DeviceName);
            string strCmd = "";
            List<string> str_List = new List<string>();
            str_List = listPLCSetting(str_List);             // 載入前面相同的 Common Code 
            str_List.Add("0018");                            //Request data length, 計算以下所有的位元數(Hex)  ~  第六筆:str_List(5)
            str_List.Add("0010");                            //CPU monitoring timer(0005)
            str_List.Add("1401");                            //Command: Batch write.
            str_List.Add(str_device);
            str_List.Add(DeviceName + "*");          //Device code  ( M* or D* or ~~)
            str_List.Add(StartAddress.PadLeft(6, '0'));
            //str_List.Add(Convert.ToString(strlength).PadLeft(4, '0'));                            //Number of device points (資料長度) 寫入時限制長度為 1
            str_List.Add(Convert.ToString(inputvalue.Length).PadLeft(4, '0'));
            str_List.Add(strvalue(inputvalue, str_device));                   //Data for the number of device points (寫入值)
            str_List[5] = strDateLenght(str_List);           //Request data length, 重新計算以下所有的位元數(Dec->Hex)  ~  第六筆:str_List(5)
            strCmd = strCommonCode(str_List);                // 組合所有  @@命令碼@@
            Write_writetoPLC(strCmd);
        }
        private List<string> listPLCSetting(List<string> list_common)//載入前面相同的 Common Code 
        {
            list_common.Add("5000");                           //Sub header
            list_common.Add("00");                             //Network No.
            list_common.Add("FF");                             //PC No.
            list_common.Add("03FF");                           //Request destination module I/O No.
            list_common.Add("00");
            return list_common;
        }
        private string strDateLenght(List<string> strList)//計算 Request Data Length : 第六筆:str_List(5)
        {
            string str_1, strCmd = null;
            int intLength;
            for (int i = 6; i <= strList.Count - 1; i++)  // 組合第六筆以後的位元
            {
                strCmd = strCmd + strList[i];
            }
            intLength = strCmd.Length;   //取出長度        
            str_1 = Convert.ToString(intLength, 16);  // 將 10進制 轉為 16進制                       
            str_1 = str_1.PadLeft(4, '0');
            str_1 = str_1.ToUpper();  //大小寫轉換
            return str_1;

        }
        private string strCommonCode(List<string> strList)//組合所有命令碼
        {
            string str1 = "";
            for (int i = 0; i <= (strList.Count - 1); i = i + 1)
            {
                str1 = str1 + strList[i];
            }
            return str1;
        }
        private int[] Read_writetoPLC(string strToPLC, int intIndex)//讀取 PLC 的寫入
        {
            NetworkStream Stream = client.GetStream();
            byte[] bytes = Encoding.ASCII.GetBytes(strToPLC);
            byte[] read_bytes = new byte[1024];
            string data;
            int i;
            int[] Response_Array = new int[intIndex];
            Stream.Write(bytes, 0, bytes.Length);
            i = Stream.Read(read_bytes, 0, read_bytes.Length);
            data = System.Text.Encoding.ASCII.GetString(read_bytes, 0, i);
            string Response_data = data.Remove(0, 22);
            for (int j = 0; j < intIndex; j++)
            {
                Response_Array[j] = Convert.ToInt32(Response_data.Substring(j * 4, 4), 16);
            }
            return Response_Array;
        }
        private void Write_writetoPLC(string strToPLC)
        {
            Stream = client.GetStream();
            byte[] bytes = Encoding.ASCII.GetBytes(strToPLC);
            Stream.Write(bytes, 0, bytes.Length);
        }
        private string strvalue(int[] Value, string device)
        {
            string str = null;
            switch (device)
            {
                case "0000":
                    for (int i = 0; i < Value.Length; i++)
                        str = str + Convert.ToString(Value[i], 16).PadLeft(4, '0').ToUpper();
                    break;
                case "0001":
                    for (int i = 0; i < Value.Length; i++)
                        str = str + Convert.ToString(Value[i]).PadLeft(10, '0');
                    break;
            }
            return str;
        }
        private void delay(int delay_milliseconds) //Delay
        {
            DateTime time_before = DateTime.Now;
            while (((TimeSpan)(DateTime.Now - time_before)).TotalMilliseconds < delay_milliseconds)
            {
                return;
            }
        }
        public string device_enum(int value)
        {
            string Device = string.Empty;
            switch (value)
            {
                case 0:
                case 1:
                    Device = "0000";
                    break;
                case 2:
                case 3:
                    Device = "0001";
                    break;
            }
            return Device;
        }
    }
    public enum Device_ASCII : int
    {
        D = 0,
        W
    }
}

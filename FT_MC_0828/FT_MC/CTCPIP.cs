using System;
using System.Text;
using System.Net;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using System.Threading;
using System.IO;
using ThreadingTimer = System.Threading.Timer;
using RsLib.LogMgr;

namespace RsLib.McProtocol
{
    public class CTCPIP
    {
        protected string logger_ip = string.Empty; 
        public string Name { get; set; }
        static object lockMe = new object();
        protected bool m_bPassive;
        protected int m_iPortNum = 0;
        protected string m_sIPAddress;
        protected ConState m_enConState = ConState.None;
        protected Socket m_ServerSocket;
        protected Socket m_ClientSocket;
        protected Thread TCPThread;
        protected bool m_bStop = false;
        private IPGlobalProperties properties;
        private TcpConnectionInformation[] connections;
        public ThreadingTimer tdt_PLC_Connect = null;
        bool tdt_busy = false;
        bool isPLCAlarm = false;
        public string ConnectStatus
        {
            get
            {
                return this.m_enConState.ToString();
            }
        }
        public CTCPIP()
        {
        }
        protected bool ClientConnect()
        {
            bool flag;
            try
            {
                this.m_enConState = ConState.Opening;
                this.m_ClientSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                this.m_ClientSocket.Connect(IPAddress.Parse(this.m_sIPAddress), this.m_iPortNum);
                flag = true;
                isPLCAlarm = false;
            }
            catch(Exception e)
            {
                Log.Add($"Connect exception {logger_ip}", MsgLevel.Alarm,e);
                this.m_ClientSocket = null;
                SpinWait.SpinUntil(() => false, 500);
                flag = false;
                isPLCAlarm = true;
            }
            return flag;
        }
        protected void CloseSocket()
        {
            try
            {
                this.m_ClientSocket.Shutdown(SocketShutdown.Both);
                this.m_ClientSocket.Close();
                Log.Add($"{logger_ip} - Connection is already closed", MsgLevel.Trace);
                SpinWait.SpinUntil(() => false, 100);
                if (!this.m_bPassive)
                    this.m_enConState = ConState.Opening;
                else
                    this.m_enConState = ConState.listen;
                isPLCAlarm = false;
            }
            catch (Exception e)
            {
                Log.Add($"Disconnect exception {logger_ip}", MsgLevel.Alarm,e);
            }
        }
        protected bool IsConnect(int PortNO)
        {
            bool flag = false;
            this.properties = IPGlobalProperties.GetIPGlobalProperties();
            this.connections = this.properties.GetActiveTcpConnections();
            if ((int)this.connections.Length > 0)
            {
                TcpConnectionInformation[] tcpConnectionInformationArray = this.connections;
                for (int i = 0; i < (int)tcpConnectionInformationArray.Length; i++)
                {
                    TcpConnectionInformation tcpConnectionInformation = tcpConnectionInformationArray[i];
                    if (tcpConnectionInformation.State == TcpState.Established & tcpConnectionInformation.RemoteEndPoint.Port == PortNO)
                    {
                        flag = true;
                    }
                }
            }
            return flag;
        }
        protected int ReadSocket(ref byte[] Buffer)
        {
            int num = -1;
            byte[] numArray = new byte[1941];
            if (this.m_enConState == ConState.Connected)
            {
                try
                {
                    num = this.m_ClientSocket.Receive(numArray);
                    Buffer = new byte[num];
                    Array.Copy(numArray, Buffer, num);
                    if (num == 0)
                    {
                        num = -1;
                        Log.Add($"{logger_ip} - Close socket", MsgLevel.Trace);
                        if(this.m_ClientSocket != null)
                            this.CloseSocket();

                        Log.Add($"{logger_ip} - Socket is already closed", MsgLevel.Trace);
                    }
                }
                catch (Exception e)
                {
                    Log.Add($"{logger_ip} read socket exception.", MsgLevel.Alarm, e);
                    num = -1;
                    isPLCAlarm = true;
                }
            }
            return num;
        }
        protected int ReadSocket(ref byte[] Buffer, int iReqLen)
        {
            int num;
            int num1 = 0;
            int num2 = 0;
            int num3 = iReqLen;
            byte[] numArray = new byte[iReqLen];
            Buffer = new byte[iReqLen];
            do
            {
                try
                {
                    num2 = this.m_ClientSocket.Receive(numArray, num3, SocketFlags.None);
                    if (num2 > 0)
                    {
                        Array.Copy(numArray, 0, Buffer, num1, num2);
                        num1 = num1 + num2;
                        num3 = iReqLen - num1;
                    }
                    else if (num2 == 0)
                    {
                        num1 = -1;
                        this.CloseSocket();
                        num = num1;
                        return num;
                    }
                }
                catch (Exception e)
                {
                    Log.Add($"{logger_ip} read socket exception.", MsgLevel.Alarm, e);
                    num1 = -1;
                    isPLCAlarm = true;
                    break;
                }
            }
            while (num1 < iReqLen);
            num = num1;
            return num;
        }
        protected int SendSocket(byte[] SendByte, int size)
        {
            int num = 0;
            try
            {
                num = this.m_ClientSocket.Send(SendByte, size, SocketFlags.None);
            }
            catch (Exception e)
            {
                Log.Add($"{logger_ip} send socket exception.", MsgLevel.Alarm, e);
                num = -1;
            }
            return num;
        }
        protected bool ServerListen()
        {
            bool flag;
            try
            {
                this.m_ServerSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                this.m_ServerSocket.Bind(new IPEndPoint(IPAddress.Parse(this.m_sIPAddress), this.m_iPortNum));
                this.m_ServerSocket.Listen(10);
                flag = true;
            }
            catch(Exception e)
            {
                Log.Add($"{logger_ip} server listen exception.", MsgLevel.Alarm, e);

                this.m_ServerSocket = null;
                flag = false;
            }
            return flag;
        }
        private void test_plc()
        {
            try
            {
                this.m_enConState = ConState.Opening;
                this.m_ClientSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                this.m_ClientSocket.Connect(IPAddress.Parse(this.m_sIPAddress), this.m_iPortNum);
            }
            catch (Exception e)
            {
                Log.Add($"{logger_ip} test plc exception.", MsgLevel.Alarm, e);
            }
            finally
            {
                tdt_busy = false;
            }
        }
        private void Check_PLC(object objectkey)
        {
            try
            {
                if (!tdt_busy)
                {
                    tdt_busy = true;
                    if (!this.m_bPassive)
                    {
                        if (this.m_enConState != ConState.Connected)
                        {
                            if (!this.ClientConnect())
                            {
                                Log.Add($"{logger_ip} - PLC is disconnected", MsgLevel.Trace);
                                this.m_enConState = ConState.Opening;
                            }
                            else
                            {
                                Log.Add($"{logger_ip} - PLC is already connected", MsgLevel.Trace);
                                this.m_enConState = ConState.Connected;
                            }
                        }
                    }
                    if (this.m_enConState == ConState.Connected)
                    {
                        if (!this.IsConnect(this.m_iPortNum))
                        {
                            Log.Add($"{logger_ip} - Close Socket", MsgLevel.Trace);
                            if (this.m_ClientSocket != null) //確認 Socket 是有被宣告的
                                this.CloseSocket();
                            Log.Add($"{logger_ip} - Socket is already closed", MsgLevel.Trace);
                        }
                    }
                }
            }
            catch(Exception e)
            {
                Log.Add($"{logger_ip} check plc exception.", MsgLevel.Alarm, e);
            }
            finally
            {
                tdt_busy = false;
            }
        }
        public void PLC_Connect(string IPAddress, int Port)
        {
            try
            {
                //Initial_Log(); //初始化 Log File 檔案位置
                logger_ip = IPAddress;
                this.m_sIPAddress = IPAddress;
                this.m_iPortNum = Port;
                this.m_bPassive = false;
                CTCPIP cTCPIP = this;
                tdt_PLC_Connect = new ThreadingTimer(new System.Threading.TimerCallback(Check_PLC), null, 0, 5000);
                Log.Add($"{logger_ip} -Connect to plc", MsgLevel.Info);
                SpinWait.SpinUntil(() => false, 100);
            }
            catch(Exception e)
            {
                Log.Add($"{logger_ip} plc connect exception.", MsgLevel.Alarm, e);
            }
        }
    }
    public enum ConState
    {
        None,
        listen,
        Opening,
        Opened,
        Connected
    }
}

using RsLib.LogMgr;
using System;
using System.Net;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using System.Threading;
using ThreadingTimer = System.Threading.Timer;

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
        //protected Thread TCPThread;
        protected bool m_bStop = false;
        private IPGlobalProperties properties;
        private TcpConnectionInformation[] connections;
        public ThreadingTimer tdt_PLC_Connect = null;
        bool tdt_busy = false;
        //bool isPLCAlarm = false;
        public event Action PLCAlarm;
        public string ConnectStatus => m_enConState.ToString();

        public CTCPIP()
        {
        }
        protected bool ClientConnect()
        {
            bool flag;
            try
            {
                m_enConState = ConState.Opening;
                m_ClientSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                m_ClientSocket.Connect(IPAddress.Parse(m_sIPAddress), m_iPortNum);
                flag = true;
            }
            catch (Exception e)
            {
                m_ClientSocket = null;
                SpinWait.SpinUntil(() => false, 500);
                flag = false;

                Log.Add($"PLC connect exception {logger_ip}", MsgLevel.Alarm, e);
                PLCAlarm?.Invoke();
            }
            return flag;
        }
        protected void CloseSocket()
        {
            try
            {
                m_ClientSocket.Shutdown(SocketShutdown.Both);
                m_ClientSocket.Close();
                Log.Add($"PLC {logger_ip} - Connection is already closed", MsgLevel.Trace);
                SpinWait.SpinUntil(() => false, 100);
                if (!m_bPassive)
                    m_enConState = ConState.Opening;
                else
                    m_enConState = ConState.listen;
            }
            catch (Exception e)
            {
                Log.Add($"PLC disconnect exception {logger_ip}", MsgLevel.Alarm, e);
                PLCAlarm?.Invoke();

            }
        }
        protected bool IsConnect(int PortNO)
        {
            bool flag = false;
            properties = IPGlobalProperties.GetIPGlobalProperties();
            connections = properties.GetActiveTcpConnections();
            if (connections.Length > 0)
            {
                TcpConnectionInformation[] tcpConnectionInformationArray = connections;
                for (int i = 0; i < tcpConnectionInformationArray.Length; i++)
                {
                    TcpConnectionInformation tcpConnectionInformation = tcpConnectionInformationArray[i];
                    if (tcpConnectionInformation.State == TcpState.Established & 
                        tcpConnectionInformation.RemoteEndPoint.Port == PortNO)
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
            if (m_enConState == ConState.Connected)
            {
                try
                {
                    num = m_ClientSocket.Receive(numArray);
                    Buffer = new byte[num];
                    Array.Copy(numArray, Buffer, num);
                    if (num == 0)
                    {
                        num = -1;
                        Log.Add($"PLC {logger_ip} - Close socket", MsgLevel.Trace);
                        if (m_ClientSocket != null)  CloseSocket();
                    }
                }
                catch (Exception e)
                {
                    num = -1;
                    Log.Add($"PLC {logger_ip} read socket exception.", MsgLevel.Alarm, e);
                    PLCAlarm?.Invoke();
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
                    num2 = m_ClientSocket.Receive(numArray, num3, SocketFlags.None);
                    if (num2 > 0)
                    {
                        Array.Copy(numArray, 0, Buffer, num1, num2);
                        num1 += num2;
                        num3 = iReqLen - num1;
                    }
                    else if (num2 == 0)
                    {
                        num1 = -1;
                        CloseSocket();
                        num = num1;
                        return num;
                    }
                }
                catch (Exception e)
                {
                    num1 = -1;
                    Log.Add($"PLC {logger_ip} read socket exception.", MsgLevel.Alarm, e);
                    PLCAlarm?.Invoke();
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
                num = m_ClientSocket.Send(SendByte, size, SocketFlags.None);
            }
            catch (Exception e)
            {
                num = -1;
                Log.Add($"{logger_ip} send socket exception.", MsgLevel.Alarm, e);
                PLCAlarm?.Invoke();
            }
            return num;
        }
        protected bool ServerListen()
        {
            bool flag;
            try
            {
                m_ServerSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                m_ServerSocket.Bind(new IPEndPoint(IPAddress.Parse(m_sIPAddress), m_iPortNum));
                m_ServerSocket.Listen(10);
                flag = true;
            }
            catch (Exception e)
            {
                m_ServerSocket = null;
                flag = false;
                Log.Add($"{logger_ip} server listen exception.", MsgLevel.Alarm, e);
                PLCAlarm?.Invoke();
            }
            return flag;
        }
        private void test_plc()
        {
            try
            {
                m_enConState = ConState.Opening;
                m_ClientSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                m_ClientSocket.Connect(IPAddress.Parse(m_sIPAddress), m_iPortNum);
            }
            catch (Exception e)
            {
                Log.Add($"{logger_ip} test plc exception.", MsgLevel.Alarm, e);
                PLCAlarm?.Invoke();
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
                    if (!m_bPassive)
                    {
                        if (m_enConState != ConState.Connected)
                        {
                            if (!ClientConnect())
                            {
                                Log.Add($"PLC {logger_ip} disconnected", MsgLevel.Trace);
                                m_enConState = ConState.Opening;
                            }
                            else
                            {
                                Log.Add($"PLC {logger_ip} already connected", MsgLevel.Trace);
                                m_enConState = ConState.Connected;
                            }
                        }
                    }
                    if (m_enConState == ConState.Connected)
                    {
                        if (!IsConnect(this.m_iPortNum))
                        {
                            Log.Add($"PLC {logger_ip} - Close Socket", MsgLevel.Trace);
                            if (m_ClientSocket != null) CloseSocket();//確認 Socket 是有被宣告的
                        }
                    }
                }
            }
            catch (Exception e)
            {
                Log.Add($"PLC {logger_ip} check plc exception.", MsgLevel.Alarm, e);
                PLCAlarm?.Invoke();
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
                logger_ip = IPAddress;
                m_sIPAddress = IPAddress;
                m_iPortNum = Port;
                m_bPassive = false;
                CTCPIP cTCPIP = this;
                tdt_PLC_Connect = new ThreadingTimer(new TimerCallback(Check_PLC), null, 0, 5000);
                Log.Add($"{logger_ip} -Connect to plc", MsgLevel.Info);
                SpinWait.SpinUntil(() => false, 100);
            }
            catch (Exception e)
            {
                Log.Add($"{logger_ip} plc connect exception.", MsgLevel.Alarm, e);
                PLCAlarm?.Invoke();
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

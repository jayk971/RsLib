using System;
using System.Net;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using System.Threading;

namespace TCPBase
{
    public class CTCPIP
    {
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
            }
            catch (Exception exception)
            {
                this.m_ClientSocket = null;
                Thread.Sleep(500);
                flag = false;
            }
            return flag;
        }

        protected void CloseSocket()
        {
            this.m_ClientSocket.Shutdown(SocketShutdown.Both);
            this.m_ClientSocket.Close();
            Thread.Sleep(100);
            if (!this.m_bPassive)
            {
                this.m_enConState = ConState.Opening;
            }
            else
            {
                this.m_enConState = ConState.listen;
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
                        this.CloseSocket();
                    }
                }
                catch (Exception exception)
                {
                    num = -1;
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
                    if (num2 == 0)
                    {
                        num1 = -1;
                        this.CloseSocket();
                        num = num1;
                        return num;
                    }
                }
                catch (SocketException socketException)
                {
                    num1 = -1;
                    break;
                }
                catch (Exception exception)
                {
                    num1 = -1;
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
            catch (Exception exception)
            {
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
            catch
            {
                this.m_ServerSocket = null;
                flag = false;
            }
            return flag;
        }

        public void TCPInitial(string IPAddress, int Port, bool Mode)
        {
            this.m_sIPAddress = IPAddress;
            this.m_iPortNum = Port;
            this.m_bPassive = Mode;
            CTCPIP cTCPIP = this;
            this.TCPThread = new Thread(new ThreadStart(cTCPIP.TCPLoop));
            this.TCPThread.Start();
        }

        public void TCPInitial()
        {
            CTCPIP cTCPIP = this;
            this.TCPThread = new Thread(new ThreadStart(cTCPIP.TCPLoop));
            this.TCPThread.Start();
        }

        public virtual void TCPLoop()
        {
            while (!this.m_bStop)
            {
                if (this.m_enConState < ConState.Connected)
                {
                    if (!this.m_bPassive)
                    {
                        if (this.m_enConState < ConState.Connected)
                        {
                            if (!this.ClientConnect())
                            {
                                this.m_enConState = ConState.Opening;
                            }
                            else
                            {
                                this.m_enConState = ConState.Connected;
                            }
                        }
                    }
                    else if (this.m_enConState == ConState.listen)
                    {
                        this.m_ClientSocket = this.m_ServerSocket.Accept();
                        this.m_enConState = ConState.Connected;
                    }
                    else if (this.ServerListen())
                    {
                        this.m_enConState = ConState.listen;
                    }
                }
                if (this.m_enConState == ConState.Connected)
                {
                    if (!this.IsConnect(this.m_iPortNum))
                    {
                        this.CloseSocket();
                    }
                }
                Thread.Sleep(10);
            }
        }
    }

}

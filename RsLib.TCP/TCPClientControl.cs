using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using RsLib.TCP.Client;
using RsLib.TCP.Properties;
namespace RsLib.TCP.Control
{
    public partial class TCPClientControl : UserControl
    {
        TCPClient _client;
        public TCPClientControl()
        {
            InitializeComponent();
        }

        private void btn_ConnectServer_Click(object sender, EventArgs e)
        {
            string name = tbx_ClientName.Text;
            string ip = tbx_IP.Text;
            int port = int.Parse(tbx_Port.Text);
            _client = new TCPClient(name, ip, port);
            _client.DataReceived += _client_DataReceived;
            _client.Connect();
        }

        private void _client_DataReceived(string arg1, string arg2)
        {
            if(this.InvokeRequired)
            {
                Action<string, string> action = new Action<string, string>(_client_DataReceived);
                this.Invoke(action, arg1, arg2);
            }
            else
            {
                if (rtbx_ExchangeMsg.Lines.Length > 10) rtbx_ExchangeMsg.Clear();

                string displayMsg = $"{DateTime.Now:HH:mm:ss.fff}\t{arg1} > {arg2}\n";
                rtbx_ExchangeMsg.AppendText(displayMsg);

            }
        }

        private void btn_DisconnectServer_Click(object sender, EventArgs e)
        {
            if(_client != null)
            {
                if(_client.IsConnect)
                {
                    _client.Disconnect();
                }
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if(_client == null)
            {
                lbl_ConnectStatus.Text = "Disconnect";
                lbl_ConnectStatus.Image = Resources.Disconnect;
                btn_DisconnectServer.Enabled = false;
                btn_ConnectServer.Enabled = true;
            }
            else
            {
                if(_client.IsConnect)
                {
                    lbl_ConnectStatus.Text = "Connect";
                    lbl_ConnectStatus.Image = Resources.Connect;
                    btn_DisconnectServer.Enabled = true;
                    btn_ConnectServer.Enabled = false;
                }
                else
                {
                    lbl_ConnectStatus.Text = "Disconnect";
                    lbl_ConnectStatus.Image = Resources.Disconnect;
                    btn_DisconnectServer.Enabled = false;
                    btn_ConnectServer.Enabled = true;

                }
            }
        }

        private void btn_SendToServer_Click(object sender, EventArgs e)
        {
            if(_client!= null)
            {
                if(_client.IsConnect)
                {
                    string data = rtbx_SendData.Text;
                    _client.Send(data);
                    string displayMsg = $"{DateTime.Now:HH:mm:ss.fff}\t> {data}\n";
                    rtbx_ExchangeMsg.AppendText(displayMsg);
                    rtbx_SendData.Clear();
                }
            }
        }
    }
}

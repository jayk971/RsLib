using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using RsLib.TCP.Control;
using RsLib.LogMgr;
using RsLib.Common;
using RsLib.AlarmMgr;
namespace RsLib.DemoForm
{
    public partial class Form1 : Form
    {
        TCPServerControl serverControl = new TCPServerControl();
        TCPClientControl clientControl = new TCPClientControl();
        LogControl logControl = new LogControl();
        public Form1()
        {
            InitializeComponent();
            Log.EnableUpdateUI = false;
            Log.Start();
            serverControl.Dock = DockStyle.Fill;
            pnl_TCPServer.Controls.Add(serverControl);
            clientControl.Dock = DockStyle.Fill;
            pnl_TCPClient.Controls.Add(clientControl);


            logControl.Dock = DockStyle.Fill;
            tableLayoutPanel1.Controls.Add(logControl, 0, 1);

            double test = 70;
            double tt = Math.Round(test / 100, 2);
            label1.Text = tt.ToString("F2");
            Log.EnableUpdateUI = true;
            comboBox1.AddEnumItems(typeof(LogControl));
        }

        private void button1_Click(object sender, EventArgs e)
        {
            bool ds = false;
            ds = FT_Functions.PingOK("192.168.170.120",4);
            //AlarmHistory.Add(3001);
            //string s = "Ab12 #_.@$-()*&^";
            //int[] arr = s.ConvertToWordArray(10);

            //listBox1.Items.Clear();

            //for (int i = 0; i < arr.Length; i++)
            //{
            //    listBox1.Items.Add(arr[i]);
            //}

            //listBox1.Items.Add(arr.ConvertToString());
        }
    }
}

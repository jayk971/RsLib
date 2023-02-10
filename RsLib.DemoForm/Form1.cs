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
using System.IO;
using System.Threading;
using RsLib.Display3D;
namespace RsLib.DemoForm
{
    public partial class Form1 : Form
    {
        TCPServerControl serverControl = new TCPServerControl();
        TCPClientControl clientControl = new TCPClientControl();
        LogControl logControl = new LogControl();
        Display3DControl displayControl = new Display3DControl(4);
        public Form1()
        {
            InitializeComponent();
            Log.EnableUpdateUI = false;
            Log.Start();
            serverControl.Dock = DockStyle.Fill;
            pnl_TCPServer.Controls.Add(serverControl);
            clientControl.Dock = DockStyle.Fill;
            pnl_TCPClient.Controls.Add(clientControl);

            displayControl.Dock = DockStyle.Fill;
            tabPage2.Controls.Add(displayControl);
            logControl.Dock = DockStyle.Fill;
            tableLayoutPanel1.Controls.Add(logControl, 0, 1);

            double test = 70;
            double tt = Math.Round(test / 100, 2);
            label1.Text = tt.ToString("F2");
            Log.EnableUpdateUI = true;
            comboBox1.AddEnumItems(typeof(LogControl));
            //ThreadPool.QueueUserWorkItem(new WaitCallback(writeTxt));
            button1.Text = Properties.Settings.Default.TestSetting;
        }

        void writeTxt(object obj)
        {
            using (StreamWriter sw = new StreamWriter("d:\\test.txt",true,Encoding.Default))
            {
                while(true)
                {
                    sw.WriteLine($"{DateTime.Now.ToShortTimeString()}\n");
                    sw.Flush();
                    SpinWait.SpinUntil(() => false, 2000);
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string aaa = "321";
            Properties.Settings.Default.TestSetting = DateTime.Now.ToString("HH:mm:ss.fff");
            Properties.Settings.Default.Save();


            PointCloud.PointCloud t = new PointCloud.PointCloud();
            Random rd = new Random();
            for (int i = 0; i < 100; i++)
            {
                
                double x = rd.NextDouble() * 100;
                double y = rd.NextDouble() * 100;
                double z = rd.NextDouble() * 100;

                //t.Add(x, y, z, true);
            }
            t.Add(100, 100, 100, true);
            t.Add(10, 10, -100, true);
            t.Add(-100, 97, 32, true);
            t.Add(150, -35, 19, true);

            PointCloud.Polyline line = new PointCloud.Polyline();
            line.LoadFromOPTFile("d:\\test.opt",true);


            displayControl.BuildPointCloud(t, Color.White, 5.0, 2,"random cloud");
            displayControl.BuildPath(line, Color.Red, 2.0, 3,"test opt path");
            displayControl.BuildVector(line, Color.LimeGreen, 2.0, 4,"test opt vector");
            //bool ds = false;
            //listBox1.Items.Clear();
            ////ds = FT_Functions.PingOK("192.168.170.120",4);

            //try
            //{
            //    //using (StreamReader sr = new StreamReader("d:\\test.txt"))
            //    //{
            //    //    sr.ReadToEnd();
            //    //}
            //    string filePath = "d:\\test.txt";
            //    string finalFilePath = filePath;
            //    if (FT_Functions.IsFileLocked(filePath))
            //    {

            //        finalFilePath = Path.GetTempFileName();
            //        File.Copy(filePath, finalFilePath, true);
            //        listBox1.Items.Add("File locked");
            //        listBox1.Items.Add(finalFilePath);

            //    }
            //    using (StreamReader sr = new StreamReader(finalFilePath))
            //    {
            //       string dd = sr.ReadToEnd();
            //        listBox1.Items.Add(dd);
            //    }


            //}
            //catch(Exception ex)
            //{
            //    listBox1.Items.Add(ex.Message);
            //}
            ////AlarmHistory.Add(3001);
            ////string s = "Ab12 #_.@$-()*&^";
            ////int[] arr = s.ConvertToWordArray(10);

            ////listBox1.Items.Clear();

            ////for (int i = 0; i < arr.Length; i++)
            ////{
            ////    listBox1.Items.Add(arr[i]);
            ////}

            ////listBox1.Items.Add(arr.ConvertToString());
        }
    }
}

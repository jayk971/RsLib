﻿using System;
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
        ZoomImageControl zoomCtrl = new ZoomImageControl();
        ZoomImageControl zoom1Ctrl = new ZoomImageControl();
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
            zoomCtrl.Dock = DockStyle.Fill;
            zoom1Ctrl.Dock = DockStyle.Fill;

            tableLayoutPanel2.Controls.Add(zoomCtrl, 0, 0);
            tableLayoutPanel2.Controls.Add(zoom1Ctrl, 1, 0);

            double test = 70;
            double tt = Math.Round(test / 100, 2);
            label1.Text = tt.ToString("F2");
            Log.EnableUpdateUI = true;
            comboBox1.AddEnumItems(typeof(LogControl));
            //ThreadPool.QueueUserWorkItem(new WaitCallback(writeTxt));
            button1.Text = Properties.Settings.Default.TestSetting;
            this.MouseMove += Form1_MouseMove;
            Bitmap bitmap = new Bitmap( Image.FromFile("d:\\test.png"));


            zoomCtrl.SetImage(bitmap.GetHbitmap());
            zoom1Ctrl.SetImage("d:\\test.bmp");
            zoomCtrl.MouseMove += ZoomCtrl_MouseMove;
            zoomCtrl.MouseWheel += ZoomCtrl_MouseWheel;
            zoomCtrl.MouseDoubleClick += ZoomCtrl_MouseDoubleClick;
            zoom1Ctrl.MouseMove += Zoom1Ctrl_MouseMove;
            zoom1Ctrl.MouseWheel += Zoom1Ctrl_MouseWheel;
            zoom1Ctrl.MouseDoubleClick += Zoom1Ctrl_MouseDoubleClick;

            Log.Add("Trace", MsgLevel.Trace);
            Log.Add("Info", MsgLevel.Info);
            Log.Add("Warn", MsgLevel.Warn);
            Log.Add("Alarm", MsgLevel.Alarm);
            Log.Add("Exception", MsgLevel.Alarm,new Exception("Test Exception"));

        }

        private void Zoom1Ctrl_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if(e.Button == MouseButtons.Middle)
            {
                zoomCtrl.ResetView();
            }
        }

        private void ZoomCtrl_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Middle)
            {
                zoom1Ctrl.ResetView();
            }
        }

        private void Zoom1Ctrl_MouseWheel(object sender, MouseEventArgs e)
        {
            zoomCtrl.Zoom = zoom1Ctrl.Zoom;
            zoomCtrl.Pan = zoom1Ctrl.Pan;
        }

        private void Zoom1Ctrl_MouseMove(object sender, MouseEventArgs e)
        {
          if(e.Button == MouseButtons.Left)
            {
                zoomCtrl.Zoom = zoom1Ctrl.Zoom;
                zoomCtrl.Pan = zoom1Ctrl.Pan;
            }
        }

        private void ZoomCtrl_MouseWheel(object sender, MouseEventArgs e)
        {
            zoom1Ctrl.Zoom = zoomCtrl.Zoom;
            zoom1Ctrl.Pan = zoomCtrl.Pan;
        }

        private void ZoomCtrl_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                zoom1Ctrl.Zoom = zoomCtrl.Zoom;
                zoom1Ctrl.Pan = zoomCtrl.Pan;
            }
        }



        bool _isDragTool = false;
        private void Form1_MouseMove(object sender, MouseEventArgs e)
        {
            if(_isDragTool)
            {
            }
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

            DisplayObjectOption cloudOption = new DisplayObjectOption()
            {
                DisplayType = DisplayObjectType.PointCloud,
                DrawColor = Color.White,
                DrawSize = 5.0f,
                ID = 2,
                Name = "Random Cloud"
            };
            displayControl.AddDisplayOption(cloudOption);
            displayControl.BuildPointCloud(t, cloudOption.ID,true,true);
            DisplayObjectOption pathOption = new DisplayObjectOption()
            {
                DisplayType = DisplayObjectType.Path,
                DrawColor = Color.Red,
                DrawSize = 2.0f,
                ID = 3,
                Name = "test opt path"
            };
            displayControl.AddDisplayOption(pathOption);

            displayControl.BuildPath(line, pathOption.ID,true,true);
            DisplayObjectOption vectorOption = new DisplayObjectOption()
            {
                DisplayType = DisplayObjectType.Vector_z,
                DrawColor = Color.LimeGreen,
                DrawSize = 2.0f,
                ID = 4,
                Name = "test opt vector"
            };
            displayControl.AddDisplayOption(vectorOption);
            displayControl.BuildVector(line, vectorOption.ID,false,true);
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

        private void timer1_Tick(object sender, EventArgs e)
        {
            int threadCount = System.Diagnostics.Process.GetCurrentProcess().Threads.Count;
            this.Text = threadCount.ToString();
        }
    }
}

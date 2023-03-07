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
using RsLib.PointCloud;

using System.Drawing.Imaging;
using System.Runtime.InteropServices;
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

            ThreadPool.QueueUserWorkItem(traceTd);
            ThreadPool.QueueUserWorkItem(infoTd);
            ThreadPool.QueueUserWorkItem(warnTd);
            ThreadPool.QueueUserWorkItem(alarmTd);


        }
        void traceTd(object obj)
        {
            while(true)
            {
                Log.Add($" {Thread.CurrentThread.ManagedThreadId} - Trace", MsgLevel.Trace);
                SpinWait.SpinUntil(() => false, 500);
            }
        }
        void infoTd(object obj)
        {
            while (true)
            {
                Log.Add($" {Thread.CurrentThread.ManagedThreadId} - Info", MsgLevel.Info);
                SpinWait.SpinUntil(() => false, 600);
            }
        }
        void warnTd(object obj)
        {
            while (true)
            {
                Log.Add($" {Thread.CurrentThread.ManagedThreadId} - warn", MsgLevel.Warn);
                SpinWait.SpinUntil(() => false, 250);
            }
        }
        void alarmTd(object obj)
        {
            while (true)
            {
                
                Log.Add($" {Thread.CurrentThread.ManagedThreadId} - alarm", MsgLevel.Alarm);
                SpinWait.SpinUntil(() => false, 300);
            }
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

            string filePath = "";
            using (OpenFileDialog op = new OpenFileDialog())
            {
                op.Filter = "XYZ|*.xyz";
                if(op.ShowDialog() == DialogResult.OK)
                {
                    filePath = op.FileName;
                }
            }
            if (File.Exists(filePath) == false) return;

            string bmpFilePath = filePath.Replace(".xyz", ".bmp");
            PointCloud.PointCloud cloud = new PointCloud.PointCloud();
            int width = 2000;
            int height = 800;

            Rectangle rec = new Rectangle(0, 0, width, height);
            Bitmap bmp = new Bitmap(width, height, PixelFormat.Format24bppRgb);
            BitmapData bitmapData = bmp.LockBits(rec, ImageLockMode.WriteOnly, bmp.PixelFormat);
            List<byte> byteList = new List<byte>();
            using (StreamReader sr = new StreamReader(filePath))
            {
                while(!sr.EndOfStream)
                {
                    string readData = sr.ReadLine();
                    string[] splitData = readData.Split('\t');

                    if(splitData.Length == 6)
                    {
                        double x = double.Parse(splitData[0]);
                        double y = double.Parse(splitData[1]);
                        double z = double.Parse(splitData[2]);

                        int r = int.Parse(splitData[3]);
                        int g = int.Parse(splitData[4]);
                        int b = int.Parse(splitData[5]);

                        Point3D p = new Point3D(x, y, z);
                        DisplayOption display = new DisplayOption();
                        display.Color = Color.FromArgb(r, g, b);
                        p.AddOption(display);
                        cloud.Add(p);
                    }
                }
            }
            cloud.BuildIndexKDtree(true);

            for (int y = 799; y >=0; y--)
            {
                double dY = -80 + y * 0.2;
                for (int x =0; x <2000; x++)
                {
                    double dX = -200 + x * 0.2;
                    Point3D target = new Point3D(dX, dY, 0);
                    int findI = cloud.FindClosePointIndex(target, 0.2);
                    if(findI >= 0)
                    {
                        Point3D tmpP = cloud.Points[findI];
                        DisplayOption o =  tmpP.GetOption(typeof(DisplayOption)) as DisplayOption;
                        if(o != null)
                        {
                            byteList.Add(o.Color.B);
                            byteList.Add(o.Color.G);
                            byteList.Add(o.Color.R);
                        }
                        else
                        {
                            byteList.Add(0);
                            byteList.Add(0);
                            byteList.Add(0);
                        }
                    }
                    else
                    {
                        byteList.Add(0);
                        byteList.Add(0);
                        byteList.Add(0);
                    }
                }
            }
            byte[] byteArr = byteList.ToArray();

            Marshal.Copy(byteArr, 0, bitmapData.Scan0, byteArr.Length);
            
            bmp.UnlockBits(bitmapData);
            bmp.Save(bmpFilePath);
            Log.Add("Convert to BMP done.", MsgLevel.Info, Color.White, Color.LimeGreen);
                //cloud.LoadFromFile(filePath,DigitFormat.XYZ,'\t');

                //Rotate r = new Rotate();
                //r.AddRotateSeq(RefAxis.Z, 95.0250352976598);
                //r.AddRotateSeq(RefAxis.Y, 70.3304675358202);
                //r.AddRotateSeq(RefAxis.X, 355.815193115526);

                //Shift s = new Shift(16.0686951767622, 121.255767387646, 178.63285793135);

                //CoordMatrix cm = new CoordMatrix();
                //cm.AddSeq(r);
                //cm.AddSeq(s);

                //PointCloud.PointCloud finalCloud =  cloud.Multiply(cm.FinalMatrix4);
                //finalCloud.Save("D:\\20230221_AF1大底線\\test.xyz");
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            int threadCount = System.Diagnostics.Process.GetCurrentProcess().Threads.Count;
            this.Text = threadCount.ToString();
        }
    }
}

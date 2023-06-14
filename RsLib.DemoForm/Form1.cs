using RsLib.Common;
using RsLib.Display3D;
using RsLib.LogMgr;
using RsLib.PointCloud;
using RsLib.SerialPortLib;
using RsLib.TCP.Control;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Text;
using System.Threading;
using System.Windows.Forms;
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

        EJ1500 _EJ1500 = new EJ1500(0);
        EJ1500Control eJ1500Ctrl;
        public Form1()
        {
            InitializeComponent();
            _EJ1500.LoadYaml("d:\\testEj1500.yaml");
            eJ1500Ctrl = new EJ1500Control(_EJ1500);
            eJ1500Ctrl.Dock = DockStyle.Fill;
            tabPage4.Controls.Add(eJ1500Ctrl);

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

            //ThreadPool.QueueUserWorkItem(traceTd);
            //ThreadPool.QueueUserWorkItem(infoTd);
            //ThreadPool.QueueUserWorkItem(warnTd);
            //ThreadPool.QueueUserWorkItem(alarmTd);


        }
        void traceTd(object obj)
        {
            while (true)
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
            if (e.Button == MouseButtons.Middle)
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
            if (e.Button == MouseButtons.Left)
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
            if (_isDragTool)
            {
            }
        }



        void writeTxt(object obj)
        {
            using (StreamWriter sw = new StreamWriter("d:\\test.txt", true, Encoding.Default))
            {
                while (true)
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
                if (op.ShowDialog() == DialogResult.OK)
                {
                    filePath = op.FileName;
                }
            }
            if (File.Exists(filePath) == false) return;


            PointCloud.PointCloud cloud = new PointCloud.PointCloud();

            using (StreamReader sr = new StreamReader(filePath))
            {
                while (!sr.EndOfStream)
                {
                    string readData = sr.ReadLine();
                    string[] splitData = readData.Split('\t');

                    if (splitData.Length == 6)
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
#if !ConvertPxStitchPath

            PointCloud.PointCloud findStitch3D = new PointCloud.PointCloud();
            PointCloud.PointCloud finalStitch = new PointCloud.PointCloud();

            string pxFilePath = filePath.Replace(".xyz", ".txt");
            string stitchRealPath = filePath.Replace(".xyz", "stitch.xyz");
            string findRealPath = filePath.Replace(".xyz", "find.xyz");

            using (StreamReader sr = new StreamReader(pxFilePath))
            {
                while (!sr.EndOfStream)
                {
                    string readData = sr.ReadLine();
                    string[] splitData = readData.Split(',');
                    double pxX = double.Parse(splitData[0]);
                    double pxY = double.Parse(splitData[1]);

                    double realX = -200 + pxX * 0.2;
                    double realY = -80 + (799 - pxY) * 0.2;



                    List<int> nearIntCloud = cloud.GetNearestCloudIndex(new Point3D(realX, realY, 0), 1.0);
                    if (nearIntCloud.Count == 0) continue;

                    PointCloud.PointCloud nearCloud = new PointCloud.PointCloud();
                    for (int i = 0; i < nearIntCloud.Count; i++)
                    {
                        nearCloud.Add(cloud.Points[nearIntCloud[i]]);
                    }
                    PointCloud.PointCloud tmpCloud = nearCloud.DeepClone();
                    tmpCloud.ReduceAboveY(realY);
                    Point3D tmpMaxZ = tmpCloud.GetMaxZPoint();
                    nearCloud.ReduceBelowY(tmpMaxZ.Y);
                    Point3D findMinZ = nearCloud.GetMinZPoint();

                    finalStitch.Add(findMinZ);

                }
                finalStitch.Save(findRealPath);

                Log.Add(findRealPath, MsgLevel.Info, Color.Black, Color.LimeGreen);
            }

            string dataFilePath = "";
            using (OpenFileDialog op = new OpenFileDialog())
            {
                op.Filter = "Halcon data|*.dat";
                if (op.ShowDialog() == DialogResult.OK)
                {
                    dataFilePath = op.FileName;
                }
            }

            if (File.Exists(dataFilePath))
            {
                CoordMatrix cm = new CoordMatrix();


                using (StreamReader sr = new StreamReader(dataFilePath))
                {
                    while (!sr.EndOfStream)
                    {
                        string readData = sr.ReadLine();
                        if (readData.Contains("Rodriguez"))
                        {
                            readData = sr.ReadLine();
                            string[] splitData = readData.Split(' ');
                            Rotate r = new Rotate();
                            double rz = double.Parse(splitData[3]);
                            double ry = double.Parse(splitData[2]);
                            double rx = double.Parse(splitData[1]);

                            r.AddRotateSeq(RefAxis.Z, rz);
                            r.AddRotateSeq(RefAxis.Y, ry);
                            r.AddRotateSeq(RefAxis.X, rx);
                            cm.AddSeq(r);

                        }
                        if (readData.Contains("Translation vector"))
                        {
                            readData = sr.ReadLine();
                            string[] splitData = readData.Split(' ');
                            double tx = double.Parse(splitData[1]);
                            double ty = double.Parse(splitData[2]);
                            double tz = double.Parse(splitData[3]);

                            Shift s = new Shift(tx, ty, tz);
                            cm.AddSeq(s);

                        }
                    }
                }

                PointCloud.PointCloud finalCloud = finalStitch.Multiply(cm.FinalMatrix4);
                finalCloud.Save(stitchRealPath);
            }
#endif
#if convertBMP
            int width = 2000;
            int height = 800;
            string bmpFilePath = filePath.Replace(".xyz", ".bmp");

            Rectangle rec = new Rectangle(0, 0, width, height);
            Bitmap bmp = new Bitmap(width, height, PixelFormat.Format24bppRgb);
            BitmapData bitmapData = bmp.LockBits(rec, ImageLockMode.WriteOnly, bmp.PixelFormat);
            List<byte> byteList = new List<byte>();

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

#endif

#if transformMatrix
            cloud.LoadFromFile(filePath, DigitFormat.XYZ, '\t');

            Rotate r = new Rotate();
            r.AddRotateSeq(RefAxis.Z, 95.0250352976598);
            r.AddRotateSeq(RefAxis.Y, 70.3304675358202);
            r.AddRotateSeq(RefAxis.X, 355.815193115526);

            Shift s = new Shift(16.0686951767622, 121.255767387646, 178.63285793135);

            CoordMatrix cm = new CoordMatrix();
            cm.AddSeq(r);
            cm.AddSeq(s);

            PointCloud.PointCloud finalCloud = cloud.Multiply(cm.FinalMatrix4);
            finalCloud.Save("D:\\20230221_AF1大底線\\test.xyz");
#endif
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (eJ1500 == null)
            {
                button2.BackColor = Color.Red;

                return;
            }
            button2.BackColor = eJ1500.IsConnected ? Color.LimeGreen : Color.Red;
        }
        EJ1500 eJ1500;
        private void button2_Click(object sender, EventArgs e)
        {
            eJ1500 = new EJ1500(1);
            eJ1500.WeightMeasured += EJ1500_WeightMeasured;
            eJ1500.Connect();
            listBox1.Items.Clear();
        }

        private void EJ1500_WeightMeasured(int index, double obj)
        {
            if (this.InvokeRequired)
            {
                Action<int, double> action = new Action<int, double>(EJ1500_WeightMeasured);
                this.Invoke(action, index, obj);
            }
            else
            {
                listBox1.Items.Add(obj);
            }
        }



        private void button3_Click(object sender, EventArgs e)
        {
            eJ1500.Disconnect();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            eJ1500.Measure();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            eJ1500.SetSero();

        }
    }
}

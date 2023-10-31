using Accord.Math;
using RsLib.Common;
using RsLib.Display3D;
using RsLib.LogMgr;
using RsLib.PointCloudLib;
using RsLib.PointCloudLib.CalculateMatrix;
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
        TransMatrixControl transMatrixControl = new TransMatrixControl();
        public Form1()
        {
            InitializeComponent();
            //_EJ1500.LoadYaml("d:\\testEj1500.yaml");
            eJ1500Ctrl = new EJ1500Control(_EJ1500);
            eJ1500Ctrl.Dock = DockStyle.Fill;
            tabPage_EJ1500.Controls.Add(eJ1500Ctrl);

            Log.EnableUpdateUI = false;
            Log.Start();
            serverControl.Dock = DockStyle.Fill;
            pnl_TCPServer.Controls.Add(serverControl);
            clientControl.Dock = DockStyle.Fill;
            pnl_TCPClient.Controls.Add(clientControl);

            displayControl.Dock = DockStyle.Fill;
            tabPage_XYZView.Controls.Add(displayControl);
            logControl.Dock = DockStyle.Fill;
            tableLayoutPanel1.Controls.Add(logControl, 0, 1);
            zoomCtrl.Dock = DockStyle.Fill;
            zoom1Ctrl.Dock = DockStyle.Fill;

            tableLayoutPanel2.Controls.Add(zoomCtrl, 0, 0);
            tableLayoutPanel2.Controls.Add(zoom1Ctrl, 1, 0);
            transMatrixControl.Dock = DockStyle.Fill;
            panel1.Controls.Add(transMatrixControl);
            double test = 70;
            double tt = Math.Round(test / 100, 2);
            label1.Text = tt.ToString("F2");
            Log.EnableUpdateUI = true;
            comboBox1.AddEnumItems(typeof(LogControl));
            //ThreadPool.QueueUserWorkItem(new WaitCallback(writeTxt));
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

        private void EJ1500_WeightMeasured(int index, double obj,bool isRaiseEvent)
        {
            if (this.InvokeRequired)
            {
                Action<int, double,bool> action = new Action<int, double,bool>(EJ1500_WeightMeasured);
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
            eJ1500.Measure(false);
        }

        private void button5_Click(object sender, EventArgs e)
        {
            eJ1500.SetSero();

        }

        private void button6_Click(object sender, EventArgs e)
        {

            PointCloud p = new PointCloud();
            PointCloud p2 = new PointCloud();

            p = p2.DeepClone();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog op = new OpenFileDialog())
            {
                op.Filter = "Json file|*.json";
                if(op.ShowDialog() == DialogResult.OK)
                {
                    string filePath = op.FileName;
                    NikePath nike =  NikePath.Parse(filePath);

                    List<ObjectGroup> oo = nike.ToObjectGroups();
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            // Y 90 X -45

            Vector3 vx = new Vector3(0f, 0f, -1f);
            Vector3 vy = new Vector3(-0.707f, 0.707f, 0f);
            Vector3 vz = new Vector3(0.707f, 0.707f, 0f);

            RotateAxis r = new RotateAxis(vx,vy,vz);

            //Y -90 X -45

            Vector3 vx2 = new Vector3(0f, 0f, 1f);
            Vector3 vy2 = new Vector3(0.707f, 0.707f, 0f);
            Vector3 vz2 = new Vector3(-0.707f, 0.707f, 0f);

            RotateAxis r2 = new RotateAxis(vx2, vy2, vz2);


        }
    }
}

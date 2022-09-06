using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using RsLib.LogMgr;
using System.Threading;
namespace TestForm
{
    public partial class Form1 : Form
    {
        LogControl logControl = new LogControl();
        bool EnableTd = false;
        public Form1()
        {
            InitializeComponent();
            logControl.Dock = DockStyle.Fill;
            panel1.Controls.Add(logControl);
            ThreadPool.QueueUserWorkItem(testThread);
            ThreadPool.QueueUserWorkItem(testOtherThread);

        }

        private void button1_Click(object sender, EventArgs e)
        {
            Log.Add("Test Info", MsgLevel.Info);

            Log.Add("Test Trace", MsgLevel.Trace);
            Log.Add("Test Warning", MsgLevel.Warning);

            Log.Add("Test Fatal", MsgLevel.Alarm);
            //Log.Add("Test Exception", MsgLevel.Alarm,new Exception("Test"));
            EnableTd = !EnableTd;

        }

        void testThread(object obj)
        {
            try
            {
                while (true)
                {
                    if (EnableTd)
                    {
                        Random rd = new Random();
                        int rdNum = rd.Next(1, 100) * 10;
                        DateTime dt = DateTime.Now;
                        Log.Add($"{rdNum} ,td  {dt:HH:mm:ss.fff}", MsgLevel.Warning);
                        SpinWait.SpinUntil(() => false, 150);
                    }
                    else
                    {
                        SpinWait.SpinUntil(() => false, 1000);
                    }
                }
            }
            catch (Exception ex)
            {
                string ss = "";
                ss = ex.StackTrace;
            }
        }
        void testOtherThread(object obj)
        {
            try
            {
                while (true)
                {
                    if (EnableTd)
                    {
                        Random rd = new Random();
                        int rdNum = rd.Next(1, 100) * 10;
                        DateTime dt = DateTime.Now;
                        Log.Add($"{rdNum} , {dt:HH:mm:ss.fff} <<<<<<<<<<<", MsgLevel.Info);
                        SpinWait.SpinUntil(() => false, 200);
                    }
                    else
                    {
                        SpinWait.SpinUntil(() => false, 1000);
                    }
                }
            }
            catch(Exception ex)
            {
                string ss = "";
                ss = ex.StackTrace;
            }
        }

    }
}

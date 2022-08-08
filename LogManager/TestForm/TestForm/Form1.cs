using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using LogMgr;
namespace TestForm
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Log.Add("Test Info", MsgLevel.Info);

            Log.Add("Test Trace", MsgLevel.Trace);
            Log.Add("Test Warning", MsgLevel.Warning);

            Log.Add("Test Fatal", MsgLevel.Alarm);
            Log.Add("Test Exception", MsgLevel.Alarm,new Exception("Test"));

        }
    }
}

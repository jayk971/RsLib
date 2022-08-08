using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using AlarmMgr;
using System.Threading;
namespace AlarmManager
{
    public partial class Form1 : Form
    {
        AlarmControl ac = new AlarmControl();
        AlarmBriefInfoControl abc = new AlarmBriefInfoControl();
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            ac.Dock = DockStyle.Fill;
            tableLayoutPanel1.Controls.Add(ac, 0, 0);
            abc.Dock = DockStyle.Fill;
            tableLayoutPanel1.Controls.Add(abc, 0, 1);

            AlarmHistory.Add(10);
        }

        bool iii = false;
        private void button1_Click(object sender, EventArgs e)
        {
            bool tt = false;
            tt = SpinWait.SpinUntil(() => true, 10000);
        }

        private void button2_Click(object sender, EventArgs e)
        {

        }
    }
}

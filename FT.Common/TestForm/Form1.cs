using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using FT.Common;
using System.Threading;
namespace TestForm
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            shoeIndexControl1.ShoeSizeChanged += ShoeIndexControl1_ShoeSizeChanged;
        }

        private void ShoeIndexControl1_ShoeSizeChanged(FT.Common.ShoeSizeIndex sizeIndex)
        {
            MessageBox.Show(sizeIndex.ToString());
        }
        FT_StopWatch ff = new FT_StopWatch();

        private void button1_Click(object sender, EventArgs e)
        {
            ff.Start();
            Thread td = new Thread(ss);
            td.IsBackground = true;
            td.Start();
            
        }

        void ss()
        {
            while(true)
            {
                updateButton();
                SpinWait.SpinUntil(() => false, 100);
            }
        }

        void updateButton()
        {
            if(this.InvokeRequired)
            {
                Action action = new Action(updateButton);
                this.Invoke(action);
            }
            else
            {
                string fff = ff.ToString_HHmmss();
                button1.Text = fff;
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            GC.Collect();
        }
    }
}

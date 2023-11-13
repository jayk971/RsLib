using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Windows;
//using System.Drawing;

namespace RsLib.Common
{
    public partial class ColorGradientControl : System.Windows.Forms.UserControl
    {
        public ColorGradientControl()
        {
            InitializeComponent();
            pnl_5.BackColor = Color.FromArgb(255, 0, 0);
            pnl_4.BackColor = Color.FromArgb(255, 255, 0);
            pnl_3.BackColor = Color.FromArgb(0, 255, 0);
            pnl_2.BackColor = Color.FromArgb(0, 255, 255);
            pnl_1.BackColor = Color.FromArgb(0, 0, 255);
            
        }
        public void SetMaxMin(double max,double min)
        {
            double d100 = max;
            double d75 = (max - min) * 0.75;
            double d50 = (max - min) * 0.5;
            double d25 = (max - min) * 0.25;
            double d0 = min;

            lbl_100.Text = d100.ToString("F1");
            lbl_75.Text = d75.ToString("F1");
            lbl_50.Text = d50.ToString("F1");
            lbl_25.Text = d25.ToString("F1");
            lbl_0.Text = d0.ToString("F1");
        }
    }
}

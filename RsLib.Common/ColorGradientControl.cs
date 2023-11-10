using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace RsLib.Common
{
    public partial class ColorGradientControl : UserControl
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
            lbl_Max.Text = max.ToString();
            lbl_Min.Text = min.ToString();
        }
    }
}

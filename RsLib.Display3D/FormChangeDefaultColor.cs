using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Windows.Forms;

using RsLib.Display3D.Properties;
namespace RsLib.Display3D
{
    public partial class FormChangeDefaultColor : Form
    {
        bool _isColorDialogOpen = false;
        public FormChangeDefaultColor()
        {
            InitializeComponent();
            btn_SelectPointColor.BackColor = Settings.Default.SelectPoint;
            btn_SelectRangeColor.BackColor = Settings.Default.SelectRange;

            
        }

        private void btn_SelectPointColor_Click(object sender, EventArgs e)
        {
            if (_isColorDialogOpen == false)
            {
                ThreadPool.QueueUserWorkItem(new WaitCallback(showColorDialogTd), 1);
            }
            else
            {
                MessageBox.Show("Color dialog has been opened. Please close the color dialog first.", "Color Dialog Opened!!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void btn_SelectRangeColor_Click(object sender, EventArgs e)
        {
            if (_isColorDialogOpen == false)
            {
                ThreadPool.QueueUserWorkItem(new WaitCallback(showColorDialogTd), 2);
            }
            else
            {
                MessageBox.Show("Color dialog has been opened. Please close the color dialog first.", "Color Dialog Opened!!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
        void showColorDialogTd(object obj)
        {
            _isColorDialogOpen = true;
            int i = (int)obj
;            using (ColorDialog cd = new ColorDialog())
            {
                if (cd.ShowDialog() == DialogResult.OK)
                {
                    if(i == 1)
                    {
                        Settings.Default.SelectPoint = cd.Color;
                        Settings.Default.Save();
                        btn_SelectPointColor.Invoke((MethodInvoker)(() => btn_SelectPointColor.BackColor = cd.Color));

                    }
                    else if(i == 2)
                    {
                        Settings.Default.SelectRange = cd.Color;
                        Settings.Default.Save();
                        btn_SelectPointColor.Invoke((MethodInvoker)(() => btn_SelectRangeColor.BackColor = cd.Color));
                    }
                }
            }
            _isColorDialogOpen = false;
        }
        private void FormChangeDefaultColor_FormClosing(object sender, FormClosingEventArgs e)
        {
        }
    }
}

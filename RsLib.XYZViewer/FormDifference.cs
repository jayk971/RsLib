using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using RsLib.Common;
namespace RsLib.XYZViewer
{
    public partial class FormDifference : Form
    {
        public event Action<int, int, double, double> AfterShowPressed;
        public FormDifference()
        {
            InitializeComponent();
        }

        private void btn_Show_Click(object sender, EventArgs e)
        {
            double min = 0;
            double max = 1;
            double.TryParse(tbx_Min.Text, out  min);
            double.TryParse(tbx_Max.Text, out max);
            double realMin = min < max ? min : max;
            double realMax = max > min ? max : min;

            int cloudBase = 0;
            int cloudCompare = 0;

            ColorGradient cg = new ColorGradient(realMin,realMax);
            panel1.Controls.Clear();
            cg.ColorControl.Dock = DockStyle.Fill;
            panel1.Controls.Add(cg.ColorControl);

            if (rbn_Cloud1.Checked) cloudBase = 1;
            else if (rbn_Cloud2.Checked) cloudBase = 2;
            else if (rbn_Cloud3.Checked) cloudBase = 3;
            else if (rbn_Cloud4.Checked) cloudBase = 4;
            else if (rbn_Cloud5.Checked) cloudBase = 5;

            if (rbn_Compare1.Checked) cloudCompare = 1;
            else if (rbn_Compare2.Checked) cloudCompare = 2;
            else if (rbn_Compare3.Checked) cloudCompare = 3;
            else if (rbn_Compare4.Checked) cloudCompare = 4;
            else if (rbn_Compare5.Checked) cloudCompare = 5;

            AfterShowPressed?.Invoke(cloudBase, cloudCompare, realMin, realMax);
            Hide();
        }
        public void SetFileName(string[] fileNames)
        {
            for (int i = 0; i < 5; i++)
            {
                string fileName = fileNames[i];
                switch (i)
                {
                    case 0:
                        rbn_Cloud1.Text = fileName;
                        rbn_Compare1.Text = fileName;
                        break;
                    case 1:
                        rbn_Cloud2.Text = fileName;
                        rbn_Compare2.Text = fileName;
                        break;
                    case 2:
                        rbn_Cloud3.Text = fileName;
                        rbn_Compare3.Text = fileName;
                        break;
                    case 3:
                        rbn_Cloud4.Text = fileName;
                        rbn_Compare4.Text = fileName;
                        break;
                    case 4:
                        rbn_Cloud5.Text = fileName;
                        rbn_Compare5.Text = fileName;
                        break;
                    default:
                        break;
                }

            }
        }
        private void tbx_Min_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = FT_Functions.double_Positive_KeyPress(e.KeyChar);
        }

        private void tbx_Max_TextChanged(object sender, EventArgs e)
        {
            
        }

        private void tbx_Min_TextChanged(object sender, EventArgs e)
        {

        }

        private void FormDifference_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = true;
            Hide();
        }
    }
}

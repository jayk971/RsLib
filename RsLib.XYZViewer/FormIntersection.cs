using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RsLib.XYZViewer
{
    public partial class FormIntersection : Form
    {
        public event Action<int, int, double, double, int,double> AfterPressShowIntersect;
        public FormIntersection()
        {
            InitializeComponent();
        }

        private void btn_Show_Click(object sender, EventArgs e)
        {
            int cloudIndex = 0;
            int pathIndex = 0;
            double extendLength = 0;
            double searchR = 0;
            int searchRange = 0;
            double reduceR = 0;
            if(rbn_Cloud1.Checked) cloudIndex = 1;
            else if (rbn_Cloud2.Checked) cloudIndex = 2;
            else if (rbn_Cloud3.Checked) cloudIndex = 3;
            else if (rbn_Cloud4.Checked) cloudIndex = 4;
            else if (rbn_Cloud5.Checked) cloudIndex = 5;

            if (rbn_Path1.Checked) pathIndex = 1;
            else if (rbn_Path2.Checked) pathIndex = 2;
            else if (rbn_Path3.Checked) pathIndex = 3;

            double.TryParse(tbx_ExtendLength.Text, out extendLength);
            double.TryParse(tbx_SearchRadius.Text, out searchR);
            int.TryParse(tbx_SearchRange.Text, out searchRange);
            double.TryParse(tbx_ReduceR.Text, out reduceR);
            if (extendLength <= 0) extendLength = 10;
            if (searchR <= 0) searchR = 1.0;
            if (searchRange <= 0) searchRange = 10;
            if (reduceR <= 0) reduceR = 1.5;
            AfterPressShowIntersect?.Invoke(cloudIndex, pathIndex, extendLength, searchR, searchRange,reduceR);
        }
    }
}

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
    using ColorTuple = Tuple<double, float, float, float>;
    using ColorTupleList = List<Tuple<double, float, float, float>>;

    public partial class ColorGradientControl : System.Windows.Forms.UserControl
    {
        ColorTupleList _colorList = new ColorTupleList();
        public ColorGradientControl()
        {
            InitializeComponent();            
        }
        internal void SetRatio(ColorTupleList colorTupleList)
        {
            _colorList = colorTupleList;
        }
        public void SetMaxMin(double max,double min)
        {
            double d100 = max;
            double d75 = (max - min) * 0.75;
            double d50 = (max - min) * 0.5;
            double d25 = (max - min) * 0.25;
            double d0 = min;

            lbl_100.Text = d100.ToString("F1");
            lbl_50.Text = d50.ToString("F1");
            lbl_0.Text = d0.ToString("F1");
            panel1.Paint += Panel1_Paint;
        }

        private void Panel1_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            Rectangle rect = panel1.ClientRectangle;

            for (int i = 0; i < panel1.Height; i++)
            {
                float blend = (float)i / (float)panel1.Height;
                Color c = InterpolationColor(blend);

                using (SolidBrush s = new SolidBrush(c))
                {
                    g.FillRectangle(s, 0, panel1.Height- i, rect.Width, 1);
                }
            }
        }
        private Color InterpolationColor(float TargetValue)
        {
            Color Output = Color.FromArgb(255, 255, 255);
            int PrevI = -1;
            for (int i = 1; i < _colorList.Count; i++)
            {
                PrevI = i - 1;

                ColorTuple CurrC = _colorList[i];
                double lastRatio = _colorList[_colorList.Count - 1].Item1;
                double firstRation = _colorList[0].Item1;
                double currentRatio = CurrC.Item1;
                if (TargetValue == lastRatio)
                {
                    Output = Color.FromArgb(255, 0, 0);
                    return Output;

                }
                else if (TargetValue == firstRation)
                {
                    Output = Color.FromArgb(0, 0, 255);
                    return Output;
                }
                else if (TargetValue < currentRatio)
                {
                    ColorTuple PrevC = _colorList[PrevI];
                    double prevRatio = PrevC.Item1;
                    double ValDiff = (prevRatio - currentRatio);
                    double FractBetween = (ValDiff == 0) ? 0 : (TargetValue - currentRatio) / ValDiff;

                    double r = (PrevC.Item2 - CurrC.Item2) * FractBetween + CurrC.Item2;
                    double g = (PrevC.Item3 - CurrC.Item3) * FractBetween + CurrC.Item3;
                    double b = (PrevC.Item4 - CurrC.Item4) * FractBetween + CurrC.Item4;

                    int iR = double2Int(r);
                    int iG = double2Int(g);
                    int iB = double2Int(b);
                    Output = Color.FromArgb(iR, iG, iB);
                    return Output;
                }
            }
            return Output;

        }
        private int double2Int(double f)
        {
            return (int)(f * 255);
        }
    }
}

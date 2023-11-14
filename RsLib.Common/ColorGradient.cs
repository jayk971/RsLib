using System;
using System.Collections.Generic;

using System.Drawing;
namespace RsLib.Common
{
    using ColorTuple = Tuple<double, float, float, float>;
    using ColorTupleList = List<Tuple<double, float, float, float>>;
    public class ColorGradient
    {
        public double Max { get; private set; } = 0;
        public double Min { get; private set; } = 0;

        ColorTupleList _gradient = new ColorTupleList();
        public ColorGradientControl ColorControl = null;

        public ColorGradient(double min, double max)
        {
            Max = max;
            Min = min;
            ColorControl = new ColorGradientControl();
            ColorControl.SetMaxMin(max, min);
            _gradient.Clear();
            _gradient.Add(new ColorTuple(0.0, 0f, 0f, 1f));  //blue
            _gradient.Add(new ColorTuple(0.25, 0f, 1f, 1f)); // cyan
            _gradient.Add(new ColorTuple(0.5, 0f, 1f, 0f)); // green
            _gradient.Add(new ColorTuple(0.75, 1f, 1f, 0f)); // yellow
            _gradient.Add(new ColorTuple(1.0, 1f, 0f, 0f)); // red
            ColorControl.SetRatio(_gradient);
        }

        public Color GetColorFromGradient(double testValue)
        {
            double r = 0.0;
            double g = 0.0;
            double b = 1.0;
            double TargetValue = 0;

            if (testValue < Min)
            {
                TargetValue = Min;
            }
            else if (testValue > Max)
            {
                TargetValue = 1.0;
            }
            else
            {
                TargetValue = (testValue - Min) / (Max - Min);
            }
            int iR = double2Int(r);
            int iG = double2Int(g);
            int iB = double2Int(b);

            Color Output = Color.FromArgb(iR, iG, iB);
            int PrevI = -1;
            for (int i = 1; i < _gradient.Count; i++)
            {
                PrevI = i - 1;

                ColorTuple CurrC = _gradient[i];
                double lastRatio = _gradient[_gradient.Count - 1].Item1;
                double firstRation = _gradient[0].Item1;
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
                    ColorTuple PrevC = _gradient[PrevI];
                    double prevRatio = PrevC.Item1;
                    double ValDiff = (prevRatio - currentRatio);
                    double FractBetween = (ValDiff == 0) ? 0 : (TargetValue - currentRatio) / ValDiff;

                    r = (PrevC.Item2 - CurrC.Item2) * FractBetween + CurrC.Item2;
                    g = (PrevC.Item3 - CurrC.Item3) * FractBetween + CurrC.Item3;
                    b = (PrevC.Item4 - CurrC.Item4) * FractBetween + CurrC.Item4;

                    iR = double2Int(r);
                    iG = double2Int(g);
                    iB = double2Int(b);
                    Output = Color.FromArgb(iR, iG, iB);
                    return Output;
                }
            }
            return Output;

        }
        private int float2Int(float f)
        {
            return (int)(f * 255f);
        }
        private int double2Int(double f)
        {
            return (int)(f * 255);
        }
    }

}

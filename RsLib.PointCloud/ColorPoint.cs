using System;
using System.Collections.Generic;

using System.Drawing;
namespace RsLib.PointCloudLib
{
    [Serializable]
    public partial class ColorPoint : Object3D
    {
        public float R = 0.0f;
        public float G = 0.0f;
        public float B = 0.0f;
        public float Value = 0.0f;
        public override uint DataCount => 1;


        public List<ColorPoint> Gradient = new List<ColorPoint>();

        public ColorPoint()
        {
        }
        public ColorPoint(float r, float g, float b, float val)
        {
            R = r;
            G = g;
            B = b;
            Value = val;
        }


        public void CreateColorGradient()
        {
            Gradient.Clear();


            Gradient.Add(new ColorPoint(0f, 0f, 1f, 0.0f));  //blue
            Gradient.Add(new ColorPoint(0f, 1f, 1f, 0.25f)); // cyan
            Gradient.Add(new ColorPoint(0f, 1f, 0f, 0.5f)); // green
            Gradient.Add(new ColorPoint(1f, 1f, 0f, 0.75f)); // yellow
            Gradient.Add(new ColorPoint(1f, 0f, 0f, 1.0f)); // red

            //Gradient.Add(new ColorPoint(1f, 0f, 0f, 0.0f));  //red
            //Gradient.Add(new ColorPoint(1f, 1f, 0f, 0.25f)); // yellow
            //Gradient.Add(new ColorPoint(0f, 1f, 0f, 0.5f)); // green
            //Gradient.Add(new ColorPoint(0f, 1f, 1f, 0.75f)); // cyan
            //Gradient.Add(new ColorPoint(0f, 0f, 1f, 1.0f)); // blue
        }
        public void GetColorFromGradient(float TargetVal, out float r, out float g, out float b)
        {
            r = 0.0f;
            g = 0.0f;
            b = 1.0f;
            if (Gradient.Count == 0) return;

            for (int i = 0; i < Gradient.Count; i++)
            {
                ColorPoint CurrC = Gradient[i];
                if (TargetVal < CurrC.Value)
                {
                    ColorPoint PrevC = Gradient[Math.Max(0, i - 1)];
                    float ValDiff = (PrevC.Value - CurrC.Value);
                    float FractBetween = (ValDiff == 0) ? 0 : (TargetVal - CurrC.Value) / ValDiff;

                    r = (PrevC.R - CurrC.R) * FractBetween + CurrC.R;
                    g = (PrevC.G - CurrC.G) * FractBetween + CurrC.G;
                    b = (PrevC.B - CurrC.B) * FractBetween + CurrC.B;
                    return;
                }
            }
            return;

        }
#if backup
        public Color GetColorFromGradient(float TargetVal)
        {
            float r = 0.0f;
            float g = 0.0f;
            float b = 1.0f;

            int iR = Float2Int(r);
            int iG = Float2Int(g);
            int iB = Float2Int(b);

            Color Output = Color.FromArgb(iR, iG, iB);

            for (int i = 0; i <Gradient.Count ; i++)
            {
                ColorPoint CurrC = Gradient[i];
                if (TargetVal == Gradient[Gradient.Count - 1].Value)
                {
                    Output = Color.FromArgb(255, 0, 0);
                    return Output;

                }
                else if (TargetVal < CurrC.Value)
                {
                    ColorPoint PrevC = Gradient[Math.Max(0, i - 1)];
                    float ValDiff = (PrevC.Value - CurrC.Value);
                    float FractBetween = (ValDiff == 0) ? 0 : (TargetVal - CurrC.Value) / ValDiff;

                    r = (PrevC.R - CurrC.R) * FractBetween + CurrC.R;
                    g = (PrevC.G - CurrC.G) * FractBetween + CurrC.G;
                    b = (PrevC.B - CurrC.B) * FractBetween + CurrC.B;

                    iR = Float2Int(r);
                    iG = Float2Int(g);
                    iB = Float2Int(b);
                    Output = Color.FromArgb(iR, iG, iB);
                    return Output;
                }
            }
            return Output;

        }
#else
        public Color GetColorFromGradient(float TargetVal)
        {
            float r = 0.0f;
            float g = 0.0f;
            float b = 1.0f;

            int iR = Float2Int(r);
            int iG = Float2Int(g);
            int iB = Float2Int(b);

            Color Output = Color.FromArgb(iR, iG, iB);
            int PrevI = -1;
            for (int i = 1; i < Gradient.Count; i++)
            {
                PrevI = i - 1;

                ColorPoint CurrC = Gradient[i];
                if (TargetVal == Gradient[Gradient.Count - 1].Value)
                {
                    Output = Color.FromArgb(255, 0, 0);
                    return Output;

                }
                else if (TargetVal == Gradient[0].Value)
                {
                    Output = Color.FromArgb(0, 0, 255);
                    return Output;
                }
                else if (TargetVal < CurrC.Value)
                {
                    ColorPoint PrevC = Gradient[PrevI];
                    float ValDiff = (PrevC.Value - CurrC.Value);
                    float FractBetween = (ValDiff == 0) ? 0 : (TargetVal - CurrC.Value) / ValDiff;

                    r = (PrevC.R - CurrC.R) * FractBetween + CurrC.R;
                    g = (PrevC.G - CurrC.G) * FractBetween + CurrC.G;
                    b = (PrevC.B - CurrC.B) * FractBetween + CurrC.B;

                    iR = Float2Int(r);
                    iG = Float2Int(g);
                    iB = Float2Int(b);
                    Output = Color.FromArgb(iR, iG, iB);
                    return Output;
                }
            }
            return Output;

        }
#endif
        private int Float2Int(float f)
        {
            return (int)(f * 255f);
        }
    }
}

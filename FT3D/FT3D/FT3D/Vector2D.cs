using System;
using System.ComponentModel;

namespace RsLib.PointCloud
{
    public partial class Vector2D
    {
        [DefaultValue(0.0)]
        public double X { get; set; } = 0.0;
        [DefaultValue(0.0)]
        public double Y { get; set; } = 0.0;

        public double L
        {
            get
            {
                double Sum = Math.Pow(X, 2) + Math.Pow(Y, 2);
                double Sqrt = Math.Sqrt(Sum);
                return Math.Round(Sqrt, 2);
            }
        }

        public Vector2D()
        {
            X = 0.0;
            Y = 0.0;
        }

        public Vector2D(Point2D StartPoint, Point2D EndPoint)
        {
            X = EndPoint.X - StartPoint.X;
            Y = EndPoint.Y - StartPoint.Y;
        }
        public Vector2D(double XDiff, double YDiff)
        {
            X = XDiff;
            Y = YDiff;
        }

        public Vector2D GetUnitVector()
        {
            return new Vector2D(X / L, Y / L);
        }

        public void Add(Vector2D vector)
        {
            X += vector.X;
            Y += vector.Y;
        }


    }
}

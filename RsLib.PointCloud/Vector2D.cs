using System;
using System.ComponentModel;

namespace RsLib.PointCloudLib
{
    public partial class Vector2D : Object3D
    {
        [DefaultValue(0.0)]
        public double X { get; set; } = 0.0;
        [DefaultValue(0.0)]
        public double Y { get; set; } = 0.0;
        public override uint DataCount => 1;
        public double L
        {
            get
            {
                double Sum = Math.Pow(X, 2) + Math.Pow(Y, 2);
                double Sqrt = Math.Sqrt(Sum);
                return Math.Round(Sqrt, 2);
            }
        }

        public static Vector2D operator *(Vector2D v, double d)
        {
            return new Vector2D(v.X * d, v.Y * d);
        }
        public static Vector2D operator /(Vector2D v, double d)
        {
            return new Vector2D(v.X / d, v.Y / d);
        }
        public static Vector2D operator +(Vector2D v1, Vector2D v2)
        {
            return new Vector2D(v1.X+v2.X,v1.Y+v2.Y);
        }
        public static Vector2D operator -(Vector2D v1, Vector2D v2)
        {
            return new Vector2D(v1.X - v2.X, v1.Y - v2.Y);
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
        public bool GetRadianAngle(out double rad)
        {
            rad = 0;
            if (X == 0 && Y == 0) return false;
            else
            {
                rad = Math.Atan2(Y, X);
                return true;
            }
        }
    }
}

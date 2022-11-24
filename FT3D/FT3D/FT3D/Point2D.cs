using System;

namespace RsLib.PointCloud
{
    [Serializable]
    public partial class Point2D:Object3D
    {
        public double X = 0;
        public double Y = 0;

        public double R = 0;
        public double A = 0;

        public override uint DataCount => 1;

        public Point2D()
        {
        }
        public Point2D(double x, double y)
        {
            X = x;
            Y = y;
        }

        public Point2D(double x, double y, double r)
        {
            X = x;
            Y = y;
            R = r;
            A = 0;
        }
        public Point2D(double x, double y, double r, double a)
        {
            X = x;
            Y = y;
            R = r;
            A = a;
        }

        public static Point2D operator -(Point2D A, Point2D B)
        {
            return new Point2D(A.X - B.X, A.Y - B.Y);
        }
        public static Point2D operator +(Point2D A, Point2D B)
        {
            return new Point2D(A.X + B.X, A.Y + B.Y);
        }

        public double Length()
        {
            return Math.Sqrt(X * X + Y * Y);
        }
        public void Rotate(double AngleTheta)
        {
            double Rad = AngleTheta / 180 * Math.PI;

            double xx = Math.Round(X * Math.Cos(Rad) - Y * Math.Sin(Rad), 1);
            double yy = Math.Round(X * Math.Sin(Rad) + Y * Math.Cos(Rad), 1);

            X = xx;
            Y = yy;
        }
        public Point2D GetRotate(double AngleTheta)
        {
            double Rad = AngleTheta / 180 * Math.PI;

            double xx = Math.Round(X * Math.Cos(Rad) - Y * Math.Sin(Rad), 1);
            double yy = Math.Round(X * Math.Sin(Rad) + Y * Math.Cos(Rad), 1);

            return new Point2D(xx, yy);
        }
        public void MirrorXAxis()
        {
            Y *= -1;
        }
        public Point2D GetMirrorXAxis()
        {
            return new Point2D(X, -1 * Y);
        }
        public void MirrorYAxis()
        {
            X *= -1;
        }
        public Point2D GetMirrorYAxis()
        {
            return new Point2D(-1 * X, Y);
        }


    }


}

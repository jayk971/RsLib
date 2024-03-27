using System;

using System.ComponentModel;
namespace RsLib.PointCloudLib
{
    [Serializable]
    public partial class Ball : Point3D
    {
        [DefaultValue(0.0)]
        public double Radius { get; set; }
        public Point3D Center => new Point3D(X, Y, Z);
        /// <summary>
        /// 初始化 ball 類別, 球心為(0,0),半徑為 0
        /// </summary>
        public Ball()
        {
            Radius = 0.0;
        }
        /// <summary>
        /// 初始化 ball 類別
        /// </summary>
        /// <param name="center">球心</param>
        /// <param name="radius">半徑</param>
        public Ball(Point3D center, double radius)
        {
            X = center.X;
            Y = center.Y;
            Z = center.Z;

            Radius = radius;
        }
        public Ball(Pose center, double radius)
        {
            X = center.X;
            Y = center.Y;
            Z = center.Z;

            Radius = radius;
        }
        public Ball(double x ,double y ,double z, double radius)
        {
            X = x;
            Y = y;
            Z = z;

            Radius = radius;
        }
        /// <summary>
        /// 求球與線段交點
        /// </summary>
        /// <param name="L">線段</param>
        /// <param name="IntersectPoint">交點</param>
        /// <returns>False : 沒有交點</returns>
        public bool Intersect(Line L, out Point3D IntersectPoint)
        {
            IntersectPoint = null;
            Vector3D StartV = new Vector3D(this, L.StartPoint);
            Vector3D EndV = new Vector3D(this, L.EndPoint);
            if (StartV.L > Radius)
            {
                return false;
            }
            else
            {
                if (EndV.L < Radius)
                {
                    return false;
                }
                else
                {
                    double a1 = L.Direction.X;
                    double a2 = L.X - this.X;

                    double b1 = L.Direction.Y;
                    double b2 = L.Y - this.Y;

                    double c1 = L.Direction.Z;
                    double c2 = L.Z - this.Z;

                    double A = Math.Pow(a1, 2) + Math.Pow(b1, 2) + Math.Pow(c1, 2);
                    double B = 2 * (a1 * a2 + b1 * b2 + c1 * c2);
                    double C = Math.Pow(a2, 2) + Math.Pow(b2, 2) + Math.Pow(c2, 2);

                    double s = Math.Pow(Radius, 2) / A + Math.Pow(B / 2 / A, 2) - (C / A);
                    double m = Math.Sqrt(s) - (B / A / 2);

                    IntersectPoint = new Point3D(L, L.Direction, m);

                    return true;
                }
            }
        }

        public double GetDistanceFromCenter(double x, double y ,double z)
        {
            return Distance(this, new Point3D(x, y, z));
        }
        public PointCloud ToPointCloud()
        {
            PointCloud output = new PointCloud();
            for (int i = -90; i <= 90; i++)
            {
                double radZ = ((double)i) / 180.0 * Math.PI;
                for (int j = 0; j <= 360; j++)
                {
                    double radXY = ((double)j) / 180.0 * Math.PI;
                    double z = Radius * Math.Sin(radZ) + Z;
                    double y = Radius * Math.Cos(radZ) * Math.Sin(radXY) + Y;
                    double x = Radius * Math.Cos(radZ) * Math.Cos(radXY) + X;

                    output.Add(x, y, z, true);
                }
            }
            return output;
        }
        public bool IsInside(double x,double y ,double z)
        {
            double r = Math.Pow(x - X, 2) + Math.Pow(y - Y, 2) + Math.Pow(z - Z, 2);
            return r <= Math.Pow(Radius, 2);
        }
    }
}

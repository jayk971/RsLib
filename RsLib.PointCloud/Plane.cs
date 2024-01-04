using Newtonsoft.Json;
using System;
using System.ComponentModel;
using AccPlane = Accord.Math.Plane;
namespace RsLib.PointCloudLib
{
    [Serializable]
    public partial class RsPlane : Object3D
    {
        public override uint DataCount => 1;

        AccPlane m_Plane;
        public AccPlane PL { get => m_Plane; }

        [DefaultValue(0.0)]
        public double A
        {
            get => m_Plane.A;
            set
            {
                m_Plane.A = (float)value;
            }
        }

        [DefaultValue(0.0)]
        public double B
        {
            get => m_Plane.B;
            set
            {
                m_Plane.B = (float)value;
            }
        }
        [DefaultValue(1.0)]
        public double C
        {
            get => m_Plane.C;
            set
            {
                m_Plane.C = (float)value;
            }
        }

        [DefaultValue(0.0)]
        public double D
        {
            get => m_Plane.Offset;
            set
            {
                m_Plane.Offset = (float)value;
            }
        }

        [JsonIgnore]
        public Vector3D Normal { get => new Vector3D(m_Plane.Normal.X, m_Plane.Normal.Y, m_Plane.Normal.Z); }


        public RsPlane()
        {
            A = 0.0;
            B = 0.0;
            C = 1.0;
            D = 0.0;
        }

        public RsPlane(Vector3D n, Point3D p)
        {
            m_Plane = new AccPlane(n.V);
            m_Plane.Offset = (float)(-m_Plane.A * p.X - m_Plane.B * p.Y - m_Plane.C * p.Z);
            m_Plane.Normalize();
        }
        public RsPlane(Point3D P0, Point3D P1, Point3D P2)
        {
            m_Plane = AccPlane.FromPoints(P0.P, P1.P, P2.P);
            m_Plane.Normalize();
        }
        public RsPlane(Point3D P0, Point3D P1, Point3D P2, Vector3D RefV)
        {
            AccPlane plane = AccPlane.FromPoints(P0.P, P1.P, P2.P);
            double dotValue = Vector3D.Dot(plane.Normal, RefV);

            if (dotValue < 0)
            {
                A = -plane.Normal.X;
                B = -plane.Normal.Y;
                C = -plane.Normal.Z;
            }
            else
            {
                A = plane.Normal.X;
                B = plane.Normal.Y;
                C = plane.Normal.Z;
            }
            D = -A * P0.X - B * P0.Y - C * P0.Z;
            m_Plane.Normalize();
        }

        public RsPlane(Point3D P0, Point3D P1, Point3D P2, Point3D RefPoint)
        {
            AccPlane plane = AccPlane.FromPoints(P0.P, P1.P, P2.P);
            Vector3D RefV = new Vector3D(P0, RefPoint);
            double dotValue = Vector3D.Dot(plane.Normal, RefV);

            if (dotValue < 0)
            {
                A = -plane.Normal.X;
                B = -plane.Normal.Y;
                C = -plane.Normal.Z;
            }
            else
            {
                A = plane.Normal.X;
                B = plane.Normal.Y;
                C = plane.Normal.Z;
            }
            D = -A * P0.X - B * P0.Y - C * P0.Z;
            m_Plane.Normalize();

        }
        /// <summary>
        /// 求點P到平面 ax + by + cz + d = 0 的距離
        /// </summary>
        public double Distance(Point3D P, bool IsAbsDis = true)
        {
            if (A == 0 && B == 0 && C == 0)
                return double.MaxValue;

            double H = A * P.X + B * P.Y + C * P.Z + D;
            if (H == 0)
                return 0.0;

            if (IsAbsDis) return Math.Abs(H) / Math.Sqrt(A * A + B * B + C * C);
            else return H / Math.Sqrt(A * A + B * B + C * C);
        }
        /// <summary>
        /// 求點P到平面 ax + by + cz + d = 0 的投影點
        /// </summary>
        public Point3D ProjectPOnPlane(Point3D P)
        {
            if (A == 0 && B == 0 && C == 0)
                return null;
            if (P == null)
                return null;
            Point3D projP = new Point3D();
            double H = A * P.X + B * P.Y + C * P.Z + D;
            if (H == 0)
            {
                projP.X = Math.Round(P.X, 2);
                projP.Y = Math.Round(P.Y, 2);
                projP.Z = Math.Round(P.Z, 2);
                return projP;
            }

            double t = -H / (A * A + B * B + C * C);

            projP.X = Math.Round(P.X + A * t, 2);
            projP.Y = Math.Round(P.Y + B * t, 2);
            projP.Z = Math.Round(P.Z + C * t, 2);

            return projP;
        }

        public bool VectorAndPlaneIntersecPoint(Point3D start, Vector3D vec, out Point3D intersecpoint)
        {
            double scale = -(A * start.X + B * start.Y + C * start.Z + D) / (A * vec.X + B * vec.Y + C * vec.Z);

            intersecpoint = new Point3D();

            if (scale >= 0 && scale <= 1)
            {
                intersecpoint.X = start.X + vec.X * scale;
                intersecpoint.Y = start.Y + vec.Y * scale;
                intersecpoint.Z = start.Z + vec.Z * scale;

                return true;
            }
            else
                return false;
        }



        //public void computeCoeByPointAndNormal(Vector3D n, Point3D p)
        //{
        //    A = n.X;
        //    B = n.Y;
        //    C = n.Z;
        //    D = -A * p.X - B * p.Y - C * p.Z;
        //}


    }


}

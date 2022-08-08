using System;

using System.ComponentModel;
using Accord.Math.Geometry;
namespace RsLib.PointCloud
{
    [Serializable]
    public partial class Line : Point3D
    {
        [DefaultValue(null)]
        public Vector3D Direction { get; set; }
        [DefaultValue(0.0)]
        public double Length { get; set; }

        public Point3D EndPoint
        {
            get
            {
                Point3D P = new Point3D(X, Y, Z);
                Point3D E = new Point3D(P, Direction, Length);

                return E;
            }
        }
        public Point3D StartPoint
        {
            get
            {
                return new Point3D(X, Y, Z);
            }
        }

        public Line()
        {
            Direction = new Vector3D();
            Length = 0.0;
        }
        public Line(Point3D pos, Vector3D dir, double length)
        {
            this.X = pos.X;
            this.Y = pos.Y;
            this.Z = pos.Z;
            Direction = dir;
            Length = length;
        }
        public void Intersect2DLine(double L1x1, double L1y1, double L1x2, double L1y2, double L2x1, double L2y1, double L2x2, double L2y2)
        {
            Accord.Point l1p1 = new Accord.Point((float)L1x1, (float)L1y1);
            Accord.Point l1p2 = new Accord.Point((float)L1x2, (float)L1y2);
            Accord.Point l2p1 = new Accord.Point((float)L2x1, (float)L2y1);
            Accord.Point l2p2 = new Accord.Point((float)L2x2, (float)L2y2);

            Accord.Math.Geometry.Line line1 = Accord.Math.Geometry.Line.FromPoints(l1p1, l1p2);
            Accord.Math.Geometry.Line line2 = Accord.Math.Geometry.Line.FromPoints(l2p1, l2p2);

            Accord.Point? intersectP =  line1.GetIntersectionWith(line2);
            if(intersectP != null)
            {
                
            }
        }

    }
}

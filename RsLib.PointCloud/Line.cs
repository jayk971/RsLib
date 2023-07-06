using System;

using System.ComponentModel;
namespace RsLib.PointCloudLib
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
        public Line(Point3D startPoint, Point3D endPoint)
        {
            this.X = startPoint.X;
            this.Y = startPoint.Y;
            this.Z = startPoint.Z;

            Direction = new Vector3D(startPoint, endPoint);
            Length = Direction.L;
        }
        public Point3D Intersect2DLine(Line line2D)
        {
            Accord.Point l1p1 = new Accord.Point((float)StartPoint.X, (float)StartPoint.Y);
            Accord.Point l1p2 = new Accord.Point((float)EndPoint.X, (float)EndPoint.Y);
            Accord.Point l2p1 = new Accord.Point((float)line2D.StartPoint.X, (float)line2D.StartPoint.Y);
            Accord.Point l2p2 = new Accord.Point((float)line2D.EndPoint.X, (float)line2D.EndPoint.Y);

            Accord.Math.Geometry.Line line1 = Accord.Math.Geometry.Line.FromPoints(l1p1, l1p2);
            Accord.Math.Geometry.Line line2 = Accord.Math.Geometry.Line.FromPoints(l2p1, l2p2);

            Accord.Point? intersectP = line1.GetIntersectionWith(line2);
            if (intersectP.HasValue)
            {
                return new Point3D(intersectP.Value.X, intersectP.Value.Y, 0.0);
            }
            else return null;
        }

    }
}

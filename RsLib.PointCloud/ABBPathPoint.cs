using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RsLib.PointCloudLib
{
    [Serializable]
    public partial class ABBPathPoint:Point3D
    {
        public double Rx { get; set; } = 0;
        public double Ry { get; set; } = 0;
        public double Rz { get; set; } = 0;

        public int LapIndex { get; set; } = 0;
        public int SegmentIndex { get; set; } = 0;

        public ABBPathPoint() 
        {

        }
        public ABBPathPoint(PointV3D p)
        {
            X = p.X;
            Y = p.Y;
            Z = p.Z;

            Rotate r = new Rotate(p);
            Rx = r.Rx;
            Ry= r.Ry; 
            Rz = r.Rz;
        }
        public string ToString_XYZRxRyRz() => $"[{X:F2},{Y:F2},{Z:F2},{Rx:F2},{Ry:F2},{Rz:F2}]";
        public string ToString_XYZRxRyRzLapSegment() => $"[{X:F2},{Y:F2},{Z:F2},{Rx:F2},{Ry:F2},{Rz:F2},{LapIndex},{SegmentIndex}]";
        public string ToString_XYZRxRyRzSegment() => $"[{X:F2},{Y:F2},{Z:F2},{Rx:F2},{Ry:F2},{Rz:F2},{SegmentIndex}]";
    }

    [Serializable]
    public partial class ABBPath
    {
        public List<ABBPathPoint> Pts { get;private  set; } = new List<ABBPathPoint>();
        public int Count => Pts.Count;
        public ABBPath() { }

        public void Add(ABBPathPoint p)
        {
            Pts.Add(p);
               
        }
        public void Clear() 
        {
            Pts.Clear();
        }
    }

}

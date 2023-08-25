using System;
using System.Collections.Generic;
using System.IO;
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
        public int PtIndex { get; set; } = 0;
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
        public string ToString_RobTarget(string targetName) => $"CONST robtarget {targetName} :=[[{X:F2},{Y:F2},{Z:F2}],[0.198562,0.633744,0.709806,0.23477],[-1,0,0,0],[9E+09,9E+09,9E+09,9E+09,9E+09,9E+09]];";
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
        public void SaveABBModPath(string filePath)
        {
            string fileName = "ABB_" + Path.GetFileNameWithoutExtension(filePath);
            using (StreamWriter sw = new StreamWriter(filePath, false, System.Text.Encoding.Default))
            {
                sw.WriteLine($"MODULE {fileName}");
                sw.WriteLine($"! File Generate Time : {DateTime.Now:yyMMdd_HHmmss}");
                sw.WriteLine("");
                sw.WriteLine($"VAR num {fileName}_Pose{{{Count} ,7}} := [");
                for (int i = 0; i < Count; i++)
                {
                    ABBPathPoint abbPt = Pts[i];
                    if (i == Count - 1)
                    {
                        sw.WriteLine($"{abbPt.ToString_XYZRxRyRzSegment()}];");
                    }
                    else
                    {
                        sw.WriteLine($"{abbPt.ToString_XYZRxRyRzSegment()},");
                    }
                }
                sw.WriteLine("");
                sw.WriteLine($"ENDMODULE");
            }
        }
        public void SaveABBModPathWithRobTarget(string filePath)
        {
            string fileName = "ABB_" + Path.GetFileNameWithoutExtension(filePath);
            using (StreamWriter sw = new StreamWriter(filePath, false, System.Text.Encoding.Default))
            {
                sw.WriteLine($"MODULE {fileName}");
                sw.WriteLine($"! File Generate Time : {DateTime.Now:yyMMdd_HHmmss}");
                sw.WriteLine("");
                for (int i = 0; i < Count; i++)
                {
                    ABBPathPoint abbPt = Pts[i];
                    sw.WriteLine(abbPt.ToString_RobTarget($"t_{abbPt.SegmentIndex}_{i}"));
                }
                sw.WriteLine("");
                sw.WriteLine($"ENDMODULE");
            }
        }

    }

}

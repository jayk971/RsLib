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
        public Quaternion Q { get; set; } = new Quaternion();

        public ABBPathPoint() 
        {

        }
        public ABBPathPoint(PointV3D p)
        {
            X = p.X;
            Y = p.Y;
            Z = p.Z;

            RotateAxis r = new RotateAxis(p);
            Rx = r.Rx;
            Ry= r.Ry; 
            Rz = r.Rz;
            Q = r.Q;
        }
        public string ToString_XYZRxRyRz() => $"[{X:F2},{Y:F2},{Z:F2},{Rx:F2},{Ry:F2},{Rz:F2}]";
        public string ToString_XYZRxRyRzLapSegment() => $"[{X:F2},{Y:F2},{Z:F2},{Rx:F2},{Ry:F2},{Rz:F2},{LapIndex},{SegmentIndex}]";
        public string ToString_XYZRxRyRzSegment() => $"[{X:F2},{Y:F2},{Z:F2},{Rx:F2},{Ry:F2},{Rz:F2},{SegmentIndex}]";
        public string ToString_RobTarget(string targetName) => $"CONST robtarget {targetName} :=[[{X:F2},{Y:F2},{Z:F2}],[{Q.W:F6},{Q.V.X:F6},{Q.V.Y:F6},{Q.V.Z:F6}],[-1,0,0,0],[9E+09,9E+09,9E+09,9E+09,9E+09,9E+09]];";
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
            string fileName = Path.GetFileNameWithoutExtension(filePath).Replace(" ","_");
            string moduleName = "ABB_" + fileName;
            using (StreamWriter sw = new StreamWriter(filePath, false, Encoding.Default,65535))
            {
                sw.WriteLine($"MODULE {moduleName}");
                sw.WriteLine($"! File Generate Time : {DateTime.Now:yyMMdd_HHmmss}");
                sw.WriteLine("");
                sw.WriteLine($"VAR num {moduleName}_Pose{{{Count} ,7}} := [");
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
            string fileName = Path.GetFileNameWithoutExtension(filePath).Replace(" ","_");
            string moduleName = "ABB_" + fileName;
            using (StreamWriter sw = new StreamWriter(filePath, false, Encoding.Default,65535))
            {
                sw.WriteLine($"MODULE {moduleName}");
                sw.WriteLine($"! File Generate Time : {DateTime.Now:yyMMdd_HHmmss}");
                sw.WriteLine("");
                for (int i = 0; i < Count; i++)
                {
                    ABBPathPoint abbPt = Pts[i];
                    sw.WriteLine(abbPt.ToString_RobTarget($"{fileName}_{abbPt.SegmentIndex}_{i}"));
                }
                sw.WriteLine("");

                sw.WriteLine("PERS tooldata LocalTool:=[TRUE,[[0,0,0],[1,0,0,0]],[1,[0,0,0],[1,0,0,0],0,0,0]];");
                sw.WriteLine("PERS wobjdata LocalWork:=[FALSE,TRUE,\"\",[[0,0,0],[1,0,0,0]],[[0,0,0],[1,0,0,0]]]; ");

                sw.WriteLine("");
                sw.WriteLine($"PROC Path{moduleName}()");
                sw.WriteLine("");
                sw.WriteLine("\tVAR speeddata LocalSpeed := v100;");
                sw.WriteLine("");
                for (int i = 0; i < Count; i++)
                {
                    ABBPathPoint abbPt = Pts[i];
                    sw.WriteLine($"\tMOVEL {fileName}_{abbPt.SegmentIndex}_{i}, LocalSpeed, z1, LocalTool\\WObj:=LocalWork;");
                }
                sw.WriteLine("");
                sw.WriteLine($"ENDPROC");
                sw.WriteLine("");
                sw.WriteLine($"ENDMODULE");
            }
        }

    }

}

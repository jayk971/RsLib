using RsLib.Common;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace RsLib.PointCloudLib
{
    [Serializable]
    public partial class ABBPoint:Point3D
    {
        public double Rx { get; set; } = 0;
        public double Ry { get; set; } = 0;
        public double Rz { get; set; } = 0;
        public int PtIndex { get; set; } = 0;
        public int LapIndex { get; set; } = 0;
        public int SegmentIndex { get; set; } = 0;
        public Quaternion Q { get; set; } = new Quaternion();

        public ABBPoint() 
        {

        }
        public ABBPoint(PointV3D p)
        {
            X = p.X;
            Y = p.Y;
            Z = p.Z;

            RotateAxis r = new RotateAxis(p);
            Rx = r.Rx;
            Ry= r.Ry;
            if (r.Rz == 180) Rz = -180.0;
            else if (r.Rz == -180) Rz = 180.0;
            else Rz = r.Rz;
            Q = r.Q;
        }

        public void ReCaculateQ()
        {
            RotateAxis r = new RotateAxis();
            r.AddRotateSeq(eRefAxis.Z, Rz);
            r.AddRotateSeq(eRefAxis.Y, Ry);
            r.AddRotateSeq(eRefAxis.X, Rx);
            Q = r.Q.DeepClone();
        }
        public string ToString_XYZRxRyRz() => $"[{X:F2},{Y:F2},{Z:F2},{Rx:F3},{Ry:F3},{Rz:F3}]";
        public string ToString_XYZRxRyRzLapSegment() => $"[{X:F2},{Y:F2},{Z:F2},{Rx:F3},{Ry:F3},{Rz:F3},{LapIndex},{SegmentIndex}]";
        public string ToString_XYZRxRyRzSegment() => $"[{X:F2},{Y:F2},{Z:F2},{Rx:F3},{Ry:F3},{Rz:F3},{SegmentIndex}]";
        public string ToString_RobTarget(string targetName) => $"CONST robtarget {targetName} :=[[{X:F2},{Y:F2},{Z:F2}],[{Q.W:F6},{Q.V.X:F6},{Q.V.Y:F6},{Q.V.Z:F6}],[-1,0,0,0],[9E+09,9E+09,9E+09,9E+09,9E+09,9E+09]];";
    }

    [Serializable]
    public partial class ABBPath
    {
        public Dictionary<int,ABBSegment> Segments { get;private  set; } = new Dictionary<int, ABBSegment>();
        public int Count => Segments.Count;
        public int PtCount
        {
            get
            {
                int count = 0;
                foreach (var segment in Segments)
                {
                    count += segment.Value.Count;
                }
                return count;
            }
        }
        public ABBPath() { }
        public void Add(ABBPoint abbPt)
        {
            int index = abbPt.SegmentIndex;
            if(Segments.ContainsKey(index))
            {
                Segments[index].Add(abbPt);
            }
            else
            {
                Segments.Add(index, new ABBSegment());
                Segments[index].Add(abbPt);
            }
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
                sw.WriteLine($"VAR num {moduleName}_Pose{{{PtCount} ,7}} := [");

                foreach (var item in Segments)
                {
                    for (int i = 0; i < item.Value.Count; i++)
                    {

                        ABBPoint abbPt = item.Value.Pts[i];
                        if (i == Count - 1)
                        {
                            sw.WriteLine($"{abbPt.ToString_XYZRxRyRzSegment()}];");
                        }
                        else
                        {
                            sw.WriteLine($"{abbPt.ToString_XYZRxRyRzSegment()},");
                        }
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
                foreach (var item in Segments)
                {
                    for (int i = 0; i < item.Value.Count; i++)
                    {
                        ABBPoint abbPt = item.Value.Pts[i];
                        sw.WriteLine(abbPt.ToString_RobTarget($"{fileName}_{abbPt.SegmentIndex}_{i}"));

                    }
                }
                sw.WriteLine("");

                sw.WriteLine("PERS tooldata LocalTool:=[TRUE,[[0,0,0],[1,0,0,0]],[1,[0,0,0],[1,0,0,0],0,0,0]];");
                sw.WriteLine("PERS wobjdata LocalWork:=[FALSE,TRUE,\"\",[[0,0,0],[1,0,0,0]],[[0,0,0],[1,0,0,0]]]; ");

                sw.WriteLine("");
                sw.WriteLine($"PROC Path{moduleName}()");
                sw.WriteLine("");
                sw.WriteLine("\tVAR speeddata LocalSpeed := v100;");
                sw.WriteLine("");

                foreach (var item in Segments)
                {
                    for (int i = 0; i < item.Value.Count; i++)
                    {
                        ABBPoint abbPt = item.Value.Pts[i];
                        sw.WriteLine($"\tMOVEL {fileName}_{abbPt.SegmentIndex}_{i}, LocalSpeed, z1, LocalTool\\WObj:=LocalWork;");

                    }
                }
 
                sw.WriteLine("");
                sw.WriteLine($"ENDPROC");
                sw.WriteLine("");
                sw.WriteLine($"ENDMODULE");
            }
        }
        public List<string> ToString(string arrayName)
        {
            List<string> output = new List<string>();
            output.Add($"LOCAL VAR num {arrayName}{{{PtCount} ,7}} := [");

            foreach (var item in Segments)
            {
                for (int i = 0; i < item.Value.Count; i++)
                {
                    ABBPoint abbPt = item.Value.Pts[i];
                    if (i == Count - 1)
                    {
                        output.Add($"{abbPt.ToString_XYZRxRyRzSegment()}];");
                    }
                    else
                    {
                        output.Add($"{abbPt.ToString_XYZRxRyRzSegment()},");
                    }
                }
            }

            output.Add($"");
            return output;
        }
        public void SmoothEulerAngle_5P(bool enableSmoothRX, bool enableSmoothRY, bool enableSmoothRZ, double ratioP1, double ratioP2, double ratioP3, double ratioP4, double ratioP5)
        {
            foreach (var item in Segments)
            {
                item.Value.SmoothEulerAngle_5P(enableSmoothRX, enableSmoothRY, enableSmoothRZ, ratioP1, ratioP2, ratioP3,ratioP4,ratioP5);
            }
        }
        public void SmoothEulerAngle_4P(bool enableSmoothRX, bool enableSmoothRY, bool enableSmoothRZ, double ratioP1, double ratioP2, double ratioP3, double ratioP4)
        {
            foreach (var item in Segments)
            {
                item.Value.SmoothEulerAngle_4P(enableSmoothRX, enableSmoothRY, enableSmoothRZ, ratioP1, ratioP2, ratioP3,ratioP4);
            }
        }
        public void SmoothEulerAngle_3P(bool enableSmoothRX, bool enableSmoothRY, bool enableSmoothRZ, double ratioP1, double ratioP2, double ratioP3)
        {
            foreach (var item in Segments)
            {
                item.Value.SmoothEulerAngle_3P(enableSmoothRX, enableSmoothRY, enableSmoothRZ, ratioP1, ratioP2, ratioP3);
            }
        }

    }
    [Serializable]
    public partial class ABBSegment
    {
        public List<ABBPoint> Pts = new List<ABBPoint>();
        public int Count => Pts.Count;
        public void Add(ABBPoint p)
        {
            Pts.Add(p);

        }
        public void Clear()
        {
            Pts.Clear();
        }
        public void SmoothEulerAngle_5P(bool enableSmoothRX, bool enableSmoothRY, bool enableSmoothRZ, double ratioP1, double ratioP2, double ratioP3, double ratioP4, double ratioP5)
        {
            List<ABBPoint> output = new List<ABBPoint>();

            double p1r = ratioP1 < 0 ? 0 : ratioP1;
            double p2r = ratioP2 < 0 ? 0 : ratioP2;
            double p3r = ratioP3 < 0 ? 0 : ratioP3;
            double p4r = ratioP4 < 0 ? 0 : ratioP4;
            double p5r = ratioP5 < 0 ? 0 : ratioP5;

            double sum = p1r + p2r + p3r + p4r + p5r;


            for (int i = 0; i < Pts.Count; i++)
            {

                int index1 = i - 2;
                int index2 = i - 1;
                int index3 = i;
                int index4 = i + 1;
                int index5 = i + 2;

                if (index5 >= Pts.Count)
                    index5 = i;

                if (index4 >= Pts.Count)
                    index4 = i;

                if (index1 < 0)
                    index1 = i;

                if (index2 < 0)
                    index2 = i;
                ABBPoint outP = Pts[i].DeepClone();

                if (sum > 0)
                {
                    if (enableSmoothRX) outP.Rx = (Pts[index1].Rx * p1r + Pts[index2].Rx * p2r + Pts[index3].Rx * p3r + Pts[index4].Rx * p4r + Pts[index5].Rx * p5r) / (sum);
                    if (enableSmoothRY) outP.Ry = (Pts[index1].Ry * p1r + Pts[index2].Ry * p2r + Pts[index3].Ry * p3r + Pts[index4].Ry * p4r + Pts[index5].Ry * p5r) / (sum);
                    if (enableSmoothRZ) outP.Rz = (Pts[index1].Rz * p1r + Pts[index2].Rz * p2r + Pts[index3].Rz * p3r + Pts[index4].Rz * p4r + Pts[index5].Rz * p5r) / (sum);
                }
                outP.ReCaculateQ();
                output.Add(outP);
            }
            Pts.Clear();
            Pts.AddRange(output);
        }
        public void SmoothEulerAngle_4P(bool enableSmoothRX, bool enableSmoothRY, bool enableSmoothRZ, double ratioP1, double ratioP2, double ratioP3, double ratioP4)
        {
            List<ABBPoint> output = new List<ABBPoint>();

            double p1r = ratioP1 < 0 ? 0 : ratioP1;
            double p2r = ratioP2 < 0 ? 0 : ratioP2;
            double p3r = ratioP3 < 0 ? 0 : ratioP3;
            double p4r = ratioP4 < 0 ? 0 : ratioP4;

            double sum = p1r + p2r + p3r + p4r;



            for (int i = 0; i < Pts.Count; i++)
            {

                int index1 = i - 2;
                int index2 = i - 1;
                int index3 = i + 1;
                int index4 = i + 2;

                if (index4 >= Pts.Count)
                    index4 = i;

                if (index3 >= Pts.Count)
                    index3 = i;

                if (index1 < 0)
                    index1 = i;

                if (index2 < 0)
                    index2 = i;
                ABBPoint outP = Pts[i].DeepClone();

                if (sum > 0)
                {
                    if (enableSmoothRX) outP.Rx = (Pts[index1].Rx * p1r + Pts[index2].Rx * p2r + Pts[index3].Rx * p3r + Pts[index4].Rx * p4r) / (sum);
                    if (enableSmoothRY) outP.Ry = (Pts[index1].Ry * p1r + Pts[index2].Ry * p2r + Pts[index3].Ry * p3r + Pts[index4].Ry * p4r) / (sum);
                    if (enableSmoothRZ) outP.Rz = (Pts[index1].Rz * p1r + Pts[index2].Rz * p2r + Pts[index3].Rz * p3r + Pts[index4].Rz * p4r) / (sum);
                }
                outP.ReCaculateQ();

                output.Add(outP);
            }
            Pts.Clear();
            Pts.AddRange(output);
        }
        public void SmoothEulerAngle_3P(bool enableSmoothRX, bool enableSmoothRY, bool enableSmoothRZ, double ratioP1, double ratioP2, double ratioP3)
        {
            List<ABBPoint> output = new List<ABBPoint>();

            double p1r = ratioP1 < 0 ? 0 : ratioP1;
            double p2r = ratioP2 < 0 ? 0 : ratioP2;
            double p3r = ratioP3 < 0 ? 0 : ratioP3;

            double sum = p1r + p2r + p3r;



            for (int i = 0; i < Pts.Count; i++)
            {

                int index1 = i - 1;
                int index2 = i;
                int index3 = i + 1;

                if (index1 < 0)
                    index1 = i;

                if (index3 >= Pts.Count)
                    index3 = i;

                ABBPoint outP = Pts[i].DeepClone();

                if (sum > 0)
                {
                    if (enableSmoothRX) outP.Rx = (Pts[index1].Rx * p1r + Pts[index2].Rx * p2r + Pts[index3].Rx * p3r) / (sum);
                    if (enableSmoothRY) outP.Ry = (Pts[index1].Ry * p1r + Pts[index2].Ry * p2r + Pts[index3].Ry * p3r) / (sum);
                    if (enableSmoothRZ) outP.Rz = (Pts[index1].Rz * p1r + Pts[index2].Rz * p2r + Pts[index3].Rz * p3r) / (sum);
                }
                outP.ReCaculateQ();

                output.Add(outP);
            }
            Pts.Clear();
            Pts.AddRange(output);
        }

    }

}

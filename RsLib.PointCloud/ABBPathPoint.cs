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
        public double Rx_Rad => Rx/180.0*Math.PI;
        public double Ry { get; set; } = 0;
        public double Ry_Rad => Ry / 180.0 * Math.PI;

        public double Rz { get; set; } = 0;
        public double Rz_Rad => Rz / 180.0 * Math.PI;

        public int PtIndex { get; set; } = 0;
        public int LapIndex { get; set; } = 0;
        public int SegmentIndex { get; set; } = 0;

        public Vector2D RzVec => new Vector2D(Math.Cos(Rz_Rad), Math.Sin(Rz_Rad));
        public Vector2D RyVec => new Vector2D(Math.Cos(Ry_Rad), Math.Sin(Ry_Rad));
        public Vector2D RxVec => new Vector2D(Math.Cos(Rx_Rad), Math.Sin(Rx_Rad));

        public Quaternion Q { get; set; } = new Quaternion();

        public ABBPoint() 
        {

        }

        public ABBPoint(ABBPoint otherPoint)
        {

            X = otherPoint.X;
            Y = otherPoint.Y;
            Z = otherPoint.Z;
            Rx = otherPoint.Rx;
            Ry = otherPoint.Ry;
            Rz = otherPoint.Rz;
            PtIndex = otherPoint.PtIndex;
            SegmentIndex = otherPoint.SegmentIndex;
            LapIndex = otherPoint.LapIndex;
        }
        public ABBPoint(PointV3D p)
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
            int countSegment = 0;
            using (StreamWriter sw = new StreamWriter(filePath, false, Encoding.Default,65535))
            {
                sw.WriteLine($"MODULE {moduleName}");
                sw.WriteLine($"! File Generate Time : {DateTime.Now:yyMMdd_HHmmss}");
                sw.WriteLine("");
                sw.WriteLine($"VAR num {moduleName}_Pose{{{PtCount} ,7}} := [");

                foreach (var item in Segments)
                {
                    countSegment++;
                    for (int i = 0; i < item.Value.Count; i++)
                    {

                        ABBPoint abbPt = item.Value.Pts[i];
                        if (countSegment == Count)
                        {
                            if (i == item.Value.Count - 1)
                            {
                                sw.WriteLine($"{abbPt.ToString_XYZRxRyRzSegment()}];");
                            }
                            else
                            {
                                sw.WriteLine($"{abbPt.ToString_XYZRxRyRzSegment()},");
                            }
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
            int countSegment = 0;
            foreach (var item in Segments)
            {
                countSegment++;
                for (int i = 0; i < item.Value.Count; i++)
                {
                    ABBPoint abbPt = item.Value.Pts[i];
                    if (countSegment == Count)//最後一條線
                    {
                        if (i == item.Value.Count - 1)//最後一條線的最後一點
                        {
                            output.Add($"{abbPt.ToString_XYZRxRyRzSegment()}];");
                        }
                        else
                        {
                            output.Add($"{abbPt.ToString_XYZRxRyRzSegment()},");
                        }
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
        public void SmoothEulerAngle_5P(bool enableSmoothRX, bool enableSmoothRY, bool enableSmoothRZ, double ratioP1, double ratioP2, double ratioP3, double ratioP4, double ratioP5,bool reCalculateQ = false)
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
                ABBPoint outP =new ABBPoint(Pts[i]);
                Vector2D vRx = new Vector2D();
                Vector2D vRy = new Vector2D();
                Vector2D vRz = new Vector2D();

                if (sum > 0)
                {
                    if (enableSmoothRX) vRx = (Pts[index1].RxVec * p1r + Pts[index2].RxVec * p2r + Pts[index3].RxVec * p3r + Pts[index4].RxVec * p4r + Pts[index5].RxVec * p5r) / (sum);
                    if (enableSmoothRY) vRy = (Pts[index1].RyVec * p1r + Pts[index2].RyVec * p2r + Pts[index3].RyVec * p3r + Pts[index4].RyVec * p4r + Pts[index5].RyVec * p5r) / (sum);
                    if (enableSmoothRZ) vRz = (Pts[index1].RzVec * p1r + Pts[index2].RzVec * p2r + Pts[index3].RzVec * p3r + Pts[index4].RzVec * p4r + Pts[index5].RzVec * p5r) / (sum);
                    if (vRx.GetRadianAngle(out double radX)) outP.Rx = radX / Math.PI * 180;
                    if (vRy.GetRadianAngle(out double radY)) outP.Ry = radY / Math.PI * 180;
                    if (vRz.GetRadianAngle(out double radZ)) outP.Rz = radZ / Math.PI * 180;

                    if(reCalculateQ) outP.ReCaculateQ();
                }
                output.Add(outP);
            }
            Pts.Clear();
            Pts.AddRange(output);
        }
        public void SmoothEulerAngle_4P(bool enableSmoothRX, bool enableSmoothRY, bool enableSmoothRZ, double ratioP1, double ratioP2, double ratioP3, double ratioP4,bool reCalculateQ = false)
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
                ABBPoint outP = new ABBPoint(Pts[i]);
                Vector2D vRx = new Vector2D();
                Vector2D vRy = new Vector2D();
                Vector2D vRz = new Vector2D();

                if (sum > 0)
                {
                    if (enableSmoothRX) vRx = (Pts[index1].RxVec * p1r + Pts[index2].RxVec * p2r + Pts[index3].RxVec * p3r + Pts[index4].RxVec * p4r) / (sum);
                    if (enableSmoothRY) vRy = (Pts[index1].RyVec * p1r + Pts[index2].RyVec * p2r + Pts[index3].RyVec * p3r + Pts[index4].RyVec * p4r) / (sum);
                    if (enableSmoothRZ) vRz = (Pts[index1].RzVec * p1r + Pts[index2].RzVec * p2r + Pts[index3].RzVec * p3r + Pts[index4].RzVec * p4r) / (sum);
                    if (vRx.GetRadianAngle(out double radX)) outP.Rx = radX / Math.PI * 180;
                    if (vRy.GetRadianAngle(out double radY)) outP.Ry = radY / Math.PI * 180;
                    if (vRz.GetRadianAngle(out double radZ)) outP.Rz = radZ / Math.PI * 180;

                   if(reCalculateQ) outP.ReCaculateQ();
                }
                output.Add(outP);
            }
            Pts.Clear();
            Pts.AddRange(output);
        }
        public void SmoothEulerAngle_3P(bool enableSmoothRX, bool enableSmoothRY, bool enableSmoothRZ, double ratioP1, double ratioP2, double ratioP3,bool reCalculateQ = false)
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

                ABBPoint outP = new ABBPoint(Pts[i]);
                Vector2D vRx = new Vector2D();
                Vector2D vRy = new Vector2D();
                Vector2D vRz = new Vector2D();
                if (sum > 0)
                {
                    if (enableSmoothRX) vRx = (Pts[index1].RxVec * p1r + Pts[index2].RxVec * p2r + Pts[index3].RxVec * p3r) / (sum);
                    if (enableSmoothRY) vRy = (Pts[index1].RyVec * p1r + Pts[index2].RyVec * p2r + Pts[index3].RyVec * p3r) / (sum);
                    if (enableSmoothRZ) vRz = (Pts[index1].RzVec * p1r + Pts[index2].RzVec * p2r + Pts[index3].RzVec * p3r) / (sum);
                    if(vRx.GetRadianAngle(out double radX)) outP.Rx = radX/Math.PI*180;
                    if (vRy.GetRadianAngle(out double radY)) outP.Ry = radY / Math.PI * 180;
                    if (vRz.GetRadianAngle(out double radZ)) outP.Rz = radZ / Math.PI * 180;

                     if(reCalculateQ)  outP.ReCaculateQ();
                }
                output.Add(outP);
            }
            Pts.Clear();
            Pts.AddRange(output);
        }

    }

}

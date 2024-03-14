using Accord.Collections;
using Accord.Math;
using RsLib.Common;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace RsLib.PointCloudLib
{
    [Serializable]
    public class Polyline : PointCloud
    {
        public List<double> Percent { get; set; }

        public double Length { get { return CalTotalLength(); } }

        public double RangeMax { get; set; }
        public double RangeMin { get; set; }
        
        public Polyline()
        {
            RangeMax = 0;
            RangeMin = 0;
            Percent = new List<double>();

        }
        public Polyline(int index)
        {
            RangeMax = 0;
            RangeMin = 0;
            Percent = new List<double>();
            Options.Add(new LineOption() { LineIndex = index });
        }
        public Polyline(double[] x,double[]y,double[]z)
        {
            Points.Clear();
            kdTree.Clear();
            if (x.Length == y.Length && x.Length == z.Length)
            {
                for (int i = 0; i < x.Length; i++)
                {
                    Point3D point = new Point3D(Math.Round(x[i], 2), Math.Round(y[i], 2), Math.Round(z[i], 2));

                    Points.Add(point);
                }
            }
        }
        public bool LoadFromStringList(List<string> fileContent, bool IsAddKdTree, int ResampleCount = 0)
        {
            Points.Clear();
            kdTree.Clear();

            if (ResampleCount <= 0) ResampleCount = 1;
            if (fileContent.Count == 0) return false;

            for (int i = 0; i < fileContent.Count; i++)
            {
                if (fileContent[i] != "")
                {
                    string[] SplitData = fileContent[i].Split(',');
                    if (SplitData.Length == 6)
                    {
                        if (i % ResampleCount != 0) continue;

                        if (!double.TryParse(SplitData[0], out double x)) return false;
                        if (!double.TryParse(SplitData[1], out double y)) return false;
                        if (!double.TryParse(SplitData[2], out double z)) return false;
                        if (!double.TryParse(SplitData[3], out double vz_x)) return false;
                        if (!double.TryParse(SplitData[4], out double vz_y)) return false;
                        if (!double.TryParse(SplitData[5], out double vz_z)) return false;

                        Point3D point = new Point3D(Math.Round(x, 3), Math.Round(y, 3), Math.Round(z, 3));
                        Vector3D vec = new Vector3D(Math.Round(vz_x, 3), Math.Round(vz_y, 3), Math.Round(vz_z, 3));

                        LocateIndexOption index = new LocateIndexOption()
                        {
                            Index = Count
                        };
                        point.Options.AddRange(Options);
                        point.Options.Add(index);
                        PointV3D point_V = new PointV3D(point, vec);
                        Points.Add(point_V);
                        if (IsAddKdTree) kdTree.Add(new double[] { point.X, point.Y, point.Z }, Points.Count - 1);
                    }
                    else if(SplitData.Length == 12)
                    {
                        if (i % ResampleCount != 0) continue;

                        if (!double.TryParse(SplitData[0], out double x)) return false;
                        if (!double.TryParse(SplitData[1], out double y)) return false;
                        if (!double.TryParse(SplitData[2], out double z)) return false;

                        if (!double.TryParse(SplitData[3], out double vz_x)) return false;
                        if (!double.TryParse(SplitData[4], out double vz_y)) return false;
                        if (!double.TryParse(SplitData[5], out double vz_z)) return false;

                        if (!double.TryParse(SplitData[6], out double vy_x)) return false;
                        if (!double.TryParse(SplitData[7], out double vy_y)) return false;
                        if (!double.TryParse(SplitData[8], out double vy_z)) return false;

                        if (!double.TryParse(SplitData[9], out double vx_x)) return false;
                        if (!double.TryParse(SplitData[10], out double vx_y)) return false;
                        if (!double.TryParse(SplitData[11], out double vx_z)) return false;

                        Point3D point = new Point3D(Math.Round(x, 3), Math.Round(y, 3), Math.Round(z, 3));
                        Vector3D vz = new Vector3D(Math.Round(vz_x, 3), Math.Round(vz_y, 3), Math.Round(vz_z, 3));
                        Vector3D vy = new Vector3D(Math.Round(vy_x, 3), Math.Round(vy_y, 3), Math.Round(vy_z, 3));
                        Vector3D vx = new Vector3D(Math.Round(vx_x, 3), Math.Round(vx_y, 3), Math.Round(vx_z, 3));

                        LocateIndexOption index = new LocateIndexOption()
                        {
                            Index = Count
                        };
                        point.Options.AddRange(Options);
                        point.Options.Add(index);
                        PointV3D point_V = new PointV3D(point, vx, vy, vz);

                        Points.Add(point_V);
                        if (IsAddKdTree) kdTree.Add(new double[] { point.X, point.Y, point.Z }, Points.Count - 1);

                    }
                    else
                    {

                    }
                }
            }
            return true;

        }

        public bool LoadFromOPTFile(string FilePath, bool IsAddKdTree, int ResampleCount = 0)
        {
            Points.Clear();
            kdTree.Clear();
            if (!File.Exists(FilePath)) return false;
            string Ext = Path.GetExtension(FilePath).ToUpper();
            if (Ext != ".OPT" && Ext != ".TXT") return false;
            List<string> stringList = ReadAllLines(FilePath);

            if (ResampleCount <= 0) ResampleCount = 1;
            if (stringList.Count == 0) return false;

            for (int i = 0; i < stringList.Count; i++)
            {
                if (stringList[i] != "")
                {
                    string[] SplitData = stringList[i].Split(',');
                    if (SplitData.Length == 6)
                    {
                        if (i % ResampleCount != 0) continue;

                        if (!double.TryParse(SplitData[0], out double x)) return false;
                        if (!double.TryParse(SplitData[1], out double y)) return false;
                        if (!double.TryParse(SplitData[2], out double z)) return false;
                        if (!double.TryParse(SplitData[3], out double vz_x)) return false;
                        if (!double.TryParse(SplitData[4], out double vz_y)) return false;
                        if (!double.TryParse(SplitData[5], out double vz_z)) return false;

                        Point3D point = new Point3D(Math.Round(x, 2), Math.Round(y, 2), Math.Round(z, 2));
                        Vector3D vec = new Vector3D(Math.Round(vz_x, 2), Math.Round(vz_y, 2), Math.Round(vz_z, 2));
                        LocateIndexOption index = new LocateIndexOption()
                        {
                            Index = Count
                        };
                        point.Options.AddRange(Options);
                        point.Options.Add(index);

                        PointV3D point_V = new PointV3D(point, vec);
                        Points.Add(point_V);
                        if (IsAddKdTree) kdTree.Add(new double[] { point_V.X, point_V.Y, point_V.Z }, Points.Count - 1);
                    }
                }
            }
            return true;

        }
        public PointCloud GetRangePoints(double startPercent, double endPercent, bool isContainStartIndex, bool isContainEndIndex)
        {
            PointCloud output = new PointCloud();
            if (Percent.Count == 0)
            {
                CalculatePercent();
            }
            if (startPercent > endPercent)
            {
                double tempStart = startPercent;
                double tempEnd = endPercent;

                startPercent = tempEnd;
                endPercent = tempStart;
            }
            for (int i = 0; i < Percent.Count; i++)
            {
                double d = Percent[i];
                if (isContainStartIndex)
                {
                    if (isContainEndIndex)
                    {
                        if (d >= startPercent && d <= endPercent) output.Add(Points[i]);
                    }
                    else
                    {
                        if (d >= startPercent && d < endPercent) output.Add(Points[i]);
                    }
                }
                else
                {
                    if (isContainEndIndex)
                    {
                        if (d > startPercent && d <= endPercent) output.Add(Points[i]);
                    }
                    else
                    {
                        if (d > startPercent && d < endPercent) output.Add(Points[i]);
                    }
                }
            }
            return output;
        }
        public new Polyline Multiply(Matrix4x4 matrix)
        {
            Polyline output = new Polyline();
            for (int i = 0; i < Points.Count; i++)
            {
                Type type = Points[i].GetType();
                if (type == typeof(Point3D))
                {
                    Point3D pt = Points[i];
                    Point3D p = Point3D.Multiply(pt, matrix);
                    output.Add(p);

                }
                else if (type == typeof(PointV3D))
                {
                    PointV3D pt = Points[i] as PointV3D;
                    PointV3D p = PointV3D.Multiply(pt, matrix);
                    output.Add(p);

                }
            }
            return output;
        }

        public new Polyline Multiply(double[,] matrixArr)
        {
            Polyline output = new Polyline();
            Matrix4x4 matrix = PointCloudCommon.ArrayToMatrix4x4(matrixArr);
            for (int i = 0; i < Points.Count; i++)
            {
                Type type = Points[i].GetType();
                if (type == typeof(Point3D))
                {
                    Point3D pt = Points[i];
                    Point3D p = Point3D.Multiply(pt, matrix);
                    output.Add(p);

                }
                else if (type == typeof(PointV3D))
                {
                    PointV3D pt = Points[i] as PointV3D;
                    PointV3D p = PointV3D.Multiply(pt, matrix);
                    output.Add(p);

                }
            }
            return output;
        }
        public PointCloud GetNearPointCloud(KDTree<int> Tree, double Radius)
        {
            Dictionary<string, Point3D> CollectCloud = new Dictionary<string, Point3D>();
            for (int i = 0; i < Points.Count; i++)
            {
                PointCloud tmp = PointCloudCommon.GetNearestPointCloud(Tree, Points[i], Radius);

                for (int j = 0; j < tmp.Count; j++)
                {
                    string Key = string.Format("{0:F2},{1:F2},{2:F2}", tmp.Points[j].X, tmp.Points[j].Y, tmp.Points[j].Z);
                    if (!CollectCloud.ContainsKey(Key)) CollectCloud.Add(Key, tmp.Points[j]);
                }
            }
            PointCloud Output = new PointCloud();
            Output.Points = CollectCloud.Values.ToList();
            return Output;
        }
        /// <summary>
        /// 補點, keep 原始點
        /// </summary>
        public void PatchLine(double smallestDis)
        {
            List<Point3D> patchLine = new List<Point3D>();

            for (int i = 0; i < Points.Count; ++i)
            {
                int index = i + 1;

                if (i == Points.Count - 1)
                {
                    index = 0;
                }

                Point3D p = Points[i];
                Point3D p1 = Points[index];

                if (i == 0)
                    patchLine.Add(Points[i]);


                //double length = Math.Abs(p1.Y-p.Y);
                double length = Point3D.Distance(p, p1);


                if (length > smallestDis)
                {

                    int pathNum = (int)(length / smallestDis);

                    Vector3D v = new Vector3D();
                    v.X = p1.X - p.X;
                    v.Y = p1.Y - p.Y;
                    v.Z = p1.Z - p.Z;
                    v = v.GetUnitVector();

                    for (int j = 0; j < pathNum; j++)
                    {
                        Point3D ppath = new Point3D(p.X + v.X * smallestDis * j, p.Y + v.Y * smallestDis * j, p.Z + v.Z * smallestDis * j);
                        patchLine.Add(ppath);
                    }

                }

                patchLine.Add(p1);
            }
            Points.Clear();
            Points = patchLine.DeepClone();
        }
        /// <summary>
        /// 輸入排序後的點(線段) 進行濾點, 保留首點, 使得兩點間距離不小於shortestDis
        /// </summary>

        public void FilterLineByDis(double shortestDis)
        {
            List<Point3D> output = new List<Point3D>();
            double accLength = 0;

            for (int i = 0; i < Points.Count - 1; i++)
            {
                int index1 = i;
                int index2 = i + 1;

                if (i == 0)
                    output.Add(Points[0]);

                accLength += Point3D.Distance(Points[index1], Points[index2]);

                if (accLength > shortestDis)
                {
                    output.Add(Points[index2]);
                    accLength = 0;
                }

            }

            //檢查尾點 // for ring
            if (output.Count > 2)
            {
                double length = Point3D.Distance(output[0], output[output.Count - 1]);
                if (length < shortestDis)
                {
                    Point3D lastPoint = output[output.Count - 1];
                    output.RemoveAt(output.Count - 1);
                    /*
                    output[output.Count - 1].X = (lastPoint.X + output[output.Count - 1].X) / 2;
                    output[output.Count - 1].Y = (lastPoint.Y + output[output.Count - 1].Y) / 2;
                    output[output.Count - 1].Z = (lastPoint.Z + output[output.Count - 1].Z) / 2;
                    */
                }
            }
            Points.Clear();
            Points = output.DeepClone();
        }
        public void Reverse()
        {
            Points.Reverse();
        }

        public Polyline GetReverse()
        {
            Polyline Output = new Polyline();
            Points.Reverse();
            Output.Points.AddRange(Points);
            return Output;
        }

        public void ReduceSortedPointByXYDis(Point3D refPoint, Vector3D refVec, double MaxDis)
        {
            Vector3D CompareVec = new Vector3D(Points[0], Points[Points.Count - 1]);
            double dotValue = Vector3D.Dot(refVec, CompareVec);
            Polyline output = new Polyline();
            if (dotValue > 0) Points.Reverse();
            bool IsLastPointGreatDis = false;
            for (int i = 0; i < Points.Count; i++)
            {
                Vector3D refV = new Vector3D(refPoint, Points[i]);
                refV.Z = 0;

                if (refV.L > MaxDis)
                {
                    IsLastPointGreatDis = true;
                }
                else if (refV.L < MaxDis)
                {
                    if (IsLastPointGreatDis)
                    {
                        Point3D CurrentP = Points[i];
                        Point3D LastP = Points[i - 1];

                        double t = MaxDis - refV.L;
                        Point3D NewP = new Point3D();
                        NewP.X = refPoint.X + refV.GetUnitVector().X * MaxDis;
                        NewP.Y = refPoint.Y + refV.GetUnitVector().Y * MaxDis;
                        NewP.Z = CurrentP.Z;

                        output.Add(NewP);
                    }

                    output.Add(Points[i].DeepClone());
                    IsLastPointGreatDis = false;
                }
                else
                {
                    output.Add(Points[i].DeepClone());
                    IsLastPointGreatDis = false;
                }
            }

            Points.Clear();
            Points = output.Points.DeepClone();
            CalculatePercent();
        }
        public void ReduceByKDTree(double Radius)
        {
            Accord.Collections.KDTree<int> temp = GetIndexKDtree();
            Point3D refP = Points[0];
            List<Point3D> Output = new List<Point3D>();
            Output.Add(refP);
            while (true)
            {
                double SetRadius = Radius;
                List<int> FoundIndex = new List<int>();
                PointCloud NearestCloud = PointCloudCommon.GetNearestPointCloud(temp, refP, SetRadius, out FoundIndex);
                List<Point3D> NewCloud = new List<Point3D>();
                while (NearestCloud.Count == 0)
                {
                    SetRadius += 1;
                    NearestCloud = PointCloudCommon.GetNearestPointCloud(temp, refP, SetRadius, out FoundIndex);
                }

                for (int i = 0; i < Points.Count; i++)
                {
                    if (!FoundIndex.Contains(i)) NewCloud.Add(Points[i]);
                }
                double MaxDis = double.MinValue;
                for (int i = 0; i < NearestCloud.Points.Count; i++)
                {
                    Vector3D tempV = new Vector3D(refP, NearestCloud.Points[i]);
                    if (tempV.L > MaxDis)
                    {
                        MaxDis = tempV.L;
                        refP = NearestCloud.Points[i].DeepClone();
                    }
                }
                Output.Add(refP);
                if (NewCloud.Count == 0) break;
                Points = NewCloud.DeepClone();
                temp = GetIndexKDtree();
            }
            Points = Output.DeepClone();
        }
        public void ReduceByKDTree(double Radius, Point3D refP)
        {
            Accord.Collections.KDTree<int> temp = GetIndexKDtree();
            List<Point3D> Output = new List<Point3D>();
            Output.Add(refP);
            while (true)
            {
                double SetRadius = Radius;
                List<int> FoundIndex = new List<int>();
                PointCloud NearestCloud = PointCloudCommon.GetNearestPointCloud(temp, refP, SetRadius, out FoundIndex);
                List<Point3D> NewCloud = new List<Point3D>();
                while (NearestCloud.Count == 0)
                {
                    SetRadius += 1;
                    NearestCloud = PointCloudCommon.GetNearestPointCloud(temp, refP, SetRadius, out FoundIndex);
                }

                for (int i = 0; i < Points.Count; i++)
                {
                    if (!FoundIndex.Contains(i)) NewCloud.Add(Points[i]);
                }
                double MaxDis = double.MinValue;
                for (int i = 0; i < NearestCloud.Points.Count; i++)
                {
                    Vector3D tempV = new Vector3D(refP, NearestCloud.Points[i]);
                    if (tempV.L > MaxDis)
                    {
                        MaxDis = tempV.L;
                        refP = NearestCloud.Points[i].DeepClone();
                    }
                }
                Output.Add(refP);
                if (NewCloud.Count == 0) break;
                Points = NewCloud.DeepClone();
                temp = GetIndexKDtree();
            }
            Points = Output.DeepClone();
        }
        public void ReduceByKDTree(double Radius, Point3D P_Start, Point3D P_End)
        {
            Accord.Collections.KDTree<int> temp = GetIndexKDtree();
            List<Point3D> Output = new List<Point3D>();
            Output.Add(P_Start);
            while (true)
            {
                double SetRadius = Radius;
                List<int> FoundIndex = new List<int>();
                PointCloud NearestCloud = PointCloudCommon.GetNearestPointCloud(temp, P_Start, SetRadius, out FoundIndex);
                List<Point3D> NewCloud = new List<Point3D>();
                while (NearestCloud.Count == 0)
                {
                    SetRadius += 1;
                    NearestCloud = PointCloudCommon.GetNearestPointCloud(temp, P_Start, SetRadius, out FoundIndex);
                }

                for (int i = 0; i < Points.Count; i++)
                {
                    if (!FoundIndex.Contains(i)) NewCloud.Add(Points[i]);
                }
                double MaxDis = double.MinValue;
                for (int i = 0; i < NearestCloud.Points.Count; i++)
                {
                    Vector3D tempV = new Vector3D(P_Start, NearestCloud.Points[i]);
                    if (tempV.L > MaxDis)
                    {
                        MaxDis = tempV.L;
                        P_Start = NearestCloud.Points[i].DeepClone();
                    }
                }
                Output.Add(P_Start);
                if (NewCloud.Count == 0) break;
                Points = NewCloud.DeepClone();
                temp = GetIndexKDtree();
            }

            Points = Output.DeepClone();
            //Point3D CalLastP = this.LastPoint();
            double LastDis = Point3D.Distance(LastPoint, P_End);
            if (LastDis > 0.5) Points.Add(P_End);
        }
        public void SmoothByCatmullRom(int subdivisions = 10)
        {
            if (Count <= 1) return;

            List<Point3D> output = new List<Point3D>();
            for (int i = 0; i < Points.Count - 1; i++)
            {
                for (int j = 0; j < subdivisions; j++)
                {
                    float t = (float)j / subdivisions;
                    Point3D pt = CatmullRom(
                        Points[Math.Max(i - 1, 0)],
                        Points[i],
                        Points[(i + 1) % Points.Count],
                        Points[Math.Min(i + 2, Points.Count - 1)],
                        t
                    );

                    if (Points[i] is PointV3D pv3d)
                    {
                        PointV3D NewPoint = new PointV3D(pv3d);

                        NewPoint.X = pt.X;
                        NewPoint.Y = pt.Y;
                        NewPoint.Z = pt.Z;
                        output.Add(NewPoint);
                    }
                    else
                    {
                        Point3D NewPoint = new Point3D(Points[i]);
                        NewPoint.X = pt.X;
                        NewPoint.Y = pt.Y;
                        NewPoint.Z = pt.Z;
                        output.Add(NewPoint);
                    }
                }
            }
            Points.Clear();
            Points.AddRange(output);
        }
        private Point3D CatmullRom(Point3D p0, Point3D p1, Point3D p2, Point3D p3, float t)
        {
            float t2 = t * t;
            float t3 = t2 * t;

            Point3D point = 0.5f * (
                (2.0f * p1) +
                (-1.0*p0 + p2) * t +
                (2.0f * p0 - 5.0f * p1 + 4.0f * p2 - p3) * t2 +
                (-1*p0 + 3.0f * p1 - 3.0f * p2 + p3) * t3
            );

            return point;
        }
        public void SmoothByCubicBezier(int subdivisions = 10)
        {
            if (Count <= 3) return;
            List<Point3D> output = new List<Point3D>();
            for (int i = 0; i < Points.Count - 3; i++)
            {
                for (int j = 0; j < subdivisions; j++)
                {
                    float t = (float)j / subdivisions;
                    Point3D pt = CubicBezier(
                        Points[i],
                        Points[i + 1],
                        Points[i + 2],
                        Points[i + 3],
                        t
                    );

                    if (Points[i] is PointV3D pv3d)
                    {
                        PointV3D NewPoint = new PointV3D(pv3d);

                        NewPoint.X = pt.X;
                        NewPoint.Y = pt.Y;
                        NewPoint.Z = pt.Z;
                        output.Add(NewPoint);
                    }
                    else
                    {
                        Point3D NewPoint = new Point3D(Points[i]);
                        NewPoint.X = pt.X;
                        NewPoint.Y = pt.Y;
                        NewPoint.Z = pt.Z;
                        output.Add(NewPoint);
                    }
                }
            }
            Points.Clear();
            Points.AddRange(output);
        }
        private Point3D CubicBezier(Point3D p0, Point3D p1, Point3D p2, Point3D p3, float t)
        {
            float u = 1 - t;
            float tt = t * t;
            float uu = u * u;
            float uuu = uu * u;
            float ttt = tt * t;

            Point3D p = uuu * p0; // (1-t)^3 * P0
            p += 3 * uu * t * p1; // 3(1-t)^2 * t * P1
            p += 3 * u * tt * p2; // 3(1-t) * t^2 * P2
            p += ttt * p3; // t^3 * P3

            return p;
        }

        public void SmoothByRDP(float tolerance)
        {
            if (Points == null || Points.Count < 3) return;

            List<Point3D> simplifiedPoints = new List<Point3D>();
            Simplify(Points, tolerance, 0, Points.Count - 1, simplifiedPoints);
            simplifiedPoints.Add(Points[Points.Count - 1]);  // Ensure the last point is included

            Points.Clear();
            Points.AddRange(simplifiedPoints);
        }
        private  void Simplify(List<Point3D> polyline, float tolerance, int start, int end, List<Point3D> result)
        {
            double maxDistance = 0;
            int index = 0;

            for (int i = start + 1; i < end; i++)
            {
                double distance = DistanceFromPointToLine(polyline[i], polyline[start], polyline[end]);

                if (distance > maxDistance)
                {
                    maxDistance = distance;
                    index = i;
                }
            }

            if (maxDistance > tolerance)
            {
                Simplify(polyline, tolerance, start, index, result);
                result.Add(polyline[index]);
                Simplify(polyline, tolerance, index, end, result);
            }
        }

        private double DistanceFromPointToLine(Point3D point, Point3D lineStart, Point3D lineEnd)
        {
            double a = point.X - lineStart.X;
            double b = point.Y - lineStart.Y;
            double c = lineEnd.X - lineStart.X;
            double d = lineEnd.Y - lineStart.Y;

            double dot = a * c + b * d;
            double lenSq = c * c + d * d;
            double param = (lenSq != 0) ? dot / lenSq : -1;

            double xx, yy;

            if (param < 0)
            {
                xx = lineStart.X;
                yy = lineStart.Y;
            }
            else if (param > 1)
            {
                xx = lineEnd.X;
                yy = lineEnd.Y;
            }
            else
            {
                xx = lineStart.X + param * c;
                yy = lineStart.Y + param * d;
            }

            double dx = point.X - xx;
            double dy = point.Y - yy;

            return Math.Sqrt(dx * dx + dy * dy);
        }
        public void SmoothByMoveAve(int count, bool SmoothX, bool SmoothY, bool SmoothZ)
        {

            if (Points.Count < count) return;
            Queue<double> FIFO_X = new Queue<double>();
            Queue<double> FIFO_Y = new Queue<double>();
            Queue<double> FIFO_Z = new Queue<double>();

            List<Point3D> AvePoints = new List<Point3D>();
            int QueueCount = 0;
            for (int i = 0; i < Points.Count - 1; i++)
            {
                int InternalCount = 0;
                if (i < Points.Count)
                {
                    if (SmoothX)
                    {
                        FIFO_X.Enqueue(Points[i].X);
                        InternalCount++;
                    }
                    if (SmoothY)
                    {
                        FIFO_Y.Enqueue(Points[i].Y);
                        InternalCount++;
                    }
                    if (SmoothZ)
                    {
                        FIFO_Z.Enqueue(Points[i].Z);
                        InternalCount++;
                    }
                    if (SmoothX && SmoothY && SmoothZ) InternalCount /= 3;
                    else
                    {
                        if (SmoothX && SmoothY) InternalCount /= 2;
                        if (SmoothX && SmoothZ) InternalCount /= 2;
                        if (SmoothY && SmoothZ) InternalCount /= 2;
                    }
                    QueueCount += InternalCount;

                    if (QueueCount > count)
                    {
                        if (SmoothX) FIFO_X.Dequeue();
                        if (SmoothY) FIFO_Y.Dequeue();
                        if (SmoothZ) FIFO_Z.Dequeue();
                    }
                }
                //else
                //{
                //    FIFO_X.Dequeue();
                //    FIFO_Y.Dequeue();
                //    FIFO_Z.Dequeue();
                //}

                double x = Points[i].X;
                double y = Points[i].Y;
                double z = Points[i].Z;

                if (SmoothX) x = FIFO_X.Average();
                if (SmoothY) y = FIFO_Y.Average();
                if (SmoothZ) z = FIFO_Z.Average();

                if(Points[i] is PointV3D d2)
                {
                    PointV3D NewPoint = new PointV3D(d2);
                    NewPoint.X = x;
                    NewPoint.Y = y;
                    NewPoint.Z = z;

                    AvePoints.Add(NewPoint);
                }
                else
                {
                    Point3D NewPoint = new Point3D(Points[i]);
                    NewPoint.X = x;
                    NewPoint.Y = y;
                    NewPoint.Z = z;

                    AvePoints.Add(NewPoint);
                }

            }
            int LastCount = AvePoints.Count - 1;
            AvePoints.RemoveAt(LastCount);

            if(Points[Points.Count - 1] is PointV3D d)
            {
                PointV3D lastPoint = new PointV3D(d);
                AvePoints.Add(lastPoint);

            }
            else
            {
                Point3D lastPoint = new Point3D(Points[Points.Count - 1]);
                AvePoints.Add(lastPoint);
            }
            Points.Clear();
            Points.AddRange(AvePoints);

            CalculatePercent();

        }

        public void FixHole(double HoleSize)
        {
            List<Point3D> NewPoints = new List<Point3D>();
            for (int i = 0; i < Points.Count - 1; i++)
            {
                int index = i;
                int index1 = i + 1;
                NewPoints.Add(Points[index]);
                Vector3D Diff = new Vector3D(Points[index], Points[index1]);

                if (Diff.L > HoleSize)
                {
                    int t = (int)Diff.L / (int)HoleSize;
                    for (int j = 0; j <= t; j++)
                    {
                        double dd = (double)j * HoleSize;

                        Point3D point = new Point3D();
                        point.X = Points[index].X + Diff.GetUnitVector().X * dd;
                        point.Y = Points[index].Y + Diff.GetUnitVector().Y * dd;
                        point.Z = Points[index].Z + Diff.GetUnitVector().Z * dd;

                        NewPoints.Add(point);
                    }
                }
                if (index == Points.Count - 2)
                {
                    NewPoints.Add(Points[index1]);
                }
            }
            Points.Clear();

            Points = NewPoints.DeepClone();

        }

        public void Insert(int Index, Point3D point)
        {
            Points.Insert(Index, point);
        }
        public void AddLine(List<Point3D> Line, double resampleDis, bool IsClose = true)
        {
            Resampling.ReSample_SkipOriginalPoint(Line, Line, resampleDis, true);
            int Count = Line.Count - 1;
            Points = Line.DeepClone();
            if (Line[0].X == Line[Count].X && Line[0].Y == Line[Count].Y && Line[0].Z == Line[Count].Z)
            {

            }
            else
            {
                if (IsClose) Points.Add(Line[0]);
            }
            CalculatePercent();

        }
        public void AddLine(List<Point3D> Line, bool IsClose = true)
        {
            int Count = Line.Count - 1;
            Points.AddRange(Line);
            if (Line[0].X == Line[Count].X && Line[0].Y == Line[Count].Y && Line[0].Z == Line[Count].Z)
            {

            }
            else
            {
                if (IsClose) Points.Add(Line[0]);
            }
            CalculatePercent();

        }

        public void CalculatePercent(int roundDigit = 4)
        {
            Percent = CalTotalPercent();
        }

        public double CalTotalLength()
        {
            double L = 0;
            for (int i = 0; i < Points.Count - 1; i++)
            {
                int index = i;
                int index1 = i + 1;

                Vector3D vector = new Vector3D(Points[index], Points[index1]);

                L += vector.L;
            }
            return L;
        }

        public List<double> CalTotalPercent(int roundDigit = 4)
        {
            List<double> output = new List<double>();
            double L = 0;
            output.Add(0.0);
            double LineLength = Length;
            LocatePercentOption loc = new LocatePercentOption();
            Points[0].Options.Add(loc);

            for (int i = 0; i < Points.Count - 1; i++)
            {
                int index = i;
                int index1 = i + 1;

                Vector3D vector = new Vector3D(Points[index], Points[index1]);
                L += vector.L;
                double P = Math.Round(L / LineLength, roundDigit);
                loc = new LocatePercentOption()
                {
                    Percent = P
                };
                Points[index1].Options.Add(loc);
                output.Add(P);
            }
            return output;
        }
        public void CalculateNormalVector(object tree, double searchRadius, Vector3D positiveDir)
        {
            if (tree.GetType() != typeof(KDTree<int>)) return;

            var output = Points.DeepClone();
            Points.Clear();
            for (int i = 0; i < output.Count; i++)
            {
                Point3D target = output[i];
                PointV3D p = new PointV3D(target);

                //Vector3D vX = new Vector3D();
                //Vector3D vY = new Vector3D();
                //Vector3D vZ = new Vector3D();
                //Point3D center = new Point3D();

                PointCloud surfaceCloud = PointCloudCommon.GetNearestPointCloud((KDTree<int>)tree, target, searchRadius);
                if (surfaceCloud.Count > 0)
                {
                    PointCloudCommon.PCA(surfaceCloud, out Vector3D vX, out Vector3D vY, out Vector3D vZ, out Point3D center);
                    double dot = Vector3D.Dot(vZ, positiveDir);
                    if (dot < 0) vZ.Reverse();
                    p.Vz = vZ.GetUnitVector();
                }
                else p.Vz = positiveDir.DeepClone();
                p.SetXYZ(target.X, target.Y, target.Z);
                Points.Add(p);
            }
        }
        public void SmoothVx()
        {
            for (int i = 0; i < Points.Count; i++)
            {
                int prev = i - 1;
                int curr = i;
                int next = i + 1;
                if(prev <0) prev = 0;
                if (next > Points.Count - 1) next = Points.Count - 1;

                PointV3D prevP = Points[prev] as PointV3D;
                PointV3D currP = Points[curr] as PointV3D;
                PointV3D nextP = Points[next] as PointV3D;

                currP.Vz.UnitVector();
                Vector3D tempVx = (prevP.Vx + currP.Vx + nextP.Vx) * (1.0 / 3.0);
                currP.Vy = Vector3D.Cross(currP.Vz, tempVx);
                currP.Vy.UnitVector();
                currP.Vx = Vector3D.Cross(currP.Vy,currP.Vz);
                currP.Vx.UnitVector();
            }
        }
        public void SmoothVy()
        {
            for (int i = 0; i < Points.Count; i++)
            {
                int prev = i - 1;
                int curr = i;
                int next = i + 1;
                if (prev < 0) prev = 0;
                if (next > Points.Count - 1) next = Points.Count - 1;

                PointV3D prevP = Points[prev] as PointV3D;
                PointV3D currP = Points[curr] as PointV3D;
                PointV3D nextP = Points[next] as PointV3D;

                currP.Vz.UnitVector();
                Vector3D tempVy = (prevP.Vy + currP.Vy + nextP.Vy) * (1.0 / 3.0);
                currP.Vx = Vector3D.Cross(tempVy, currP.Vz);
                currP.Vx.UnitVector();
                currP.Vy = Vector3D.Cross(currP.Vz, currP.Vx);
                currP.Vy.UnitVector();
            }
        }
        public void SmoothVz()
        {
            for (int i = 0; i < Points.Count; i++)
            {
                int prev = i - 1;
                int curr = i;
                int next = i + 1;
                if (prev < 0) prev = 0;
                if (next > Points.Count - 1) next = Points.Count - 1;

                PointV3D prevP = Points[prev] as PointV3D;
                PointV3D currP = Points[curr] as PointV3D;
                PointV3D nextP = Points[next] as PointV3D;

                currP.Vz = (prevP.Vz + currP.Vz + nextP.Vz) * (1.0 / 3.0);
                currP.Vx = Vector3D.Cross(currP.Vy, currP.Vz);
                currP.Vx.UnitVector();
                currP.Vy = Vector3D.Cross(currP.Vz, currP.Vx);
                currP.Vy.UnitVector();
            }
        }
        public void CalculatePathDirectionAsVy()
        {
            for (int i = 0; i < Points.Count -1; i++)
            {
                int curr = i;
                int next = i + 1;
                PointV3D currP = Points[curr] as PointV3D;
                PointV3D nextP = Points[next] as PointV3D;

                Vector3D Vpath = new Vector3D(currP, nextP);
                currP.Vz.UnitVector();
                Vpath.UnitVector();
                currP.Vx = Vector3D.Cross(Vpath, currP.Vz);
                currP.Vx.UnitVector();
                currP.Vy = Vector3D.Cross(currP.Vz, currP.Vx);
                currP.Vy.UnitVector();

                if(i == Points.Count-2)
                {
                    nextP.Vz.UnitVector();
                    nextP.Vx = Vector3D.Cross(Vpath, nextP.Vz);
                    nextP.Vx.UnitVector();
                    nextP.Vy = Vector3D.Cross(nextP.Vz, nextP.Vx);
                    nextP.Vy.UnitVector();
                }
            }
        }
        public void RemoveLast()
        {
            if (Points.Count == 0) return;
            Points.RemoveAt(Count - 1);
            CalculatePercent();
        }
        public void RemovePoint(double percent)
        {
            if (percent < 0 || percent > 1) return;
            if (Points.Count == 0) return;
            else
            {
                CalculatePercent();
            }

            int RemoveIndex = 0;
            for (int i = 0; i < Percent.Count - 1; i++)
            {
                int index = i;
                int index1 = i + 1;

                double Percent0 = Percent[index];
                double Percent1 = Percent[index1];

                if (percent > Percent0 && percent <= Percent1)
                {
                    RemoveIndex = index;
                    break;
                }
            }

            Points.RemoveAt(RemoveIndex);
            CalculatePercent();
        }

        public void RemovePoint(List<int> Indices)
        {
            if (Indices.Count == 0) return;
            if (Points.Count == 0) return;

            List<Point3D> tmp = new List<Point3D>();

            for (int i = 0; i < Points.Count; i++)
            {
                if (Indices.Contains(i)) continue;
                else tmp.Add(Points[i].DeepClone());
            }

            Points.Clear();
            Points = tmp.DeepClone();

            CalculatePercent();
        }
        /// <summary>
        /// 取得線段的百分比點位，並回傳百分比點前後一個點位資料
        /// </summary>
        /// <param name="percent">點位百分比</param>
        /// <param name="P1">前一個點</param>
        /// <param name="P2">後一個點</param>
        /// <returns></returns>
        public Point3D GetPercentPoint(double percent, out Point3D P1, out Point3D P2)
        {
            P1 = new Point3D();
            P2 = new Point3D();

            if (percent < 0 || percent > 1) return null;
            if (Points.Count == 0) return null;
            else
            {
                CalculatePercent();
            }

            if (percent == 0.0)
            {
                P1 = Points[0];
                P2 = Points[1];

                return Points[0];
            }

            if (percent == 1.0)
            {
                P1 = Points[Points.Count - 1];
                P2 = Points[1];
                return Points[0];
            }
            double diff = 0;



            for (int i = 0; i < Percent.Count - 1; i++)
            {
                int index = i;
                int index1 = i + 1;

                double Percent0 = Percent[index];
                double Percent1 = Percent[index1];

                if (percent > Percent0 && percent <= Percent1)
                {
                    P1 = Points[index];
                    P2 = Points[index1];
                    diff = (percent - Percent0) / (Percent1 - Percent0);
                    break;
                }
            }
            Vector3D vector3 = new Vector3D(P1, P2);
            Point3D CalPoint = new Point3D();
            CalPoint.X = P1.X + vector3.X * diff;
            CalPoint.Y = P1.Y + vector3.Y * diff;
            CalPoint.Z = P1.Z + vector3.Z * diff;


            return CalPoint;
        }
        public Point3D GetPercentPoint(double percent, out Vector3D V1)
        {
            Point3D P1 = new Point3D();
            Point3D P2 = new Point3D();

            V1 = new Vector3D();

            if (percent < 0 || percent > 1) return null;
            if (Points.Count == 0) return null;
            else
            {
                CalculatePercent();
            }

            if (percent == 0.0)
            {
                P1 = Points[Points.Count - 2];
                P2 = Points[1];

                V1 = new Vector3D(P1, P2);

                return Points[0];
            }

            if (percent == 1.0)
            {
                P1 = Points[Points.Count - 2];
                P2 = Points[1];

                V1 = new Vector3D(P1, P2);
                return Points[0];
            }
            double diff = 0;



            for (int i = 0; i < Percent.Count - 1; i++)
            {
                int index = i;
                int index1 = i + 1;

                double Percent0 = Percent[index];
                double Percent1 = Percent[index1];

                if (percent > Percent0 && percent <= Percent1)
                {
                    P1 = Points[index];
                    P2 = Points[index1];
                    diff = (percent - Percent0) / (Percent1 - Percent0);
                    break;
                }
            }
            Vector3D vector3 = new Vector3D(P1, P2);
            Point3D CalPoint = new Point3D();
            CalPoint.X = P1.X + vector3.X * diff;
            CalPoint.Y = P1.Y + vector3.Y * diff;
            CalPoint.Z = P1.Z + vector3.Z * diff;

            V1 = new Vector3D(P1, P2);
            return CalPoint;
        }
        public Point3D GetPercentPoint(double percent)
        {
            Point3D P1 = new Point3D();
            Point3D P2 = new Point3D();

            if (percent < 0 || percent > 1) return null;
            if (Points.Count == 0) return null;
            else
            {
                CalculatePercent();
            }

            if (percent == 0.0)
            {
                return Points[0];
            }

            if (percent == 1.0)
            {
                return Points[Points.Count - 1];
            }
            double diff = 0;



            for (int i = 0; i < Percent.Count - 1; i++)
            {
                int index = i;
                int index1 = i + 1;

                double Percent0 = Percent[index];
                double Percent1 = Percent[index1];

                if (percent > Percent0 && percent <= Percent1)
                {
                    P1 = Points[index];
                    P2 = Points[index1];
                    diff = (percent - Percent0) / (Percent1 - Percent0);
                    break;
                }
            }
            Vector3D vector3 = new Vector3D(P1, P2);
            Point3D CalPoint = new Point3D();
            CalPoint.X = P1.X + vector3.X * diff;
            CalPoint.Y = P1.Y + vector3.Y * diff;
            CalPoint.Z = P1.Z + vector3.Z * diff;


            return CalPoint;
        }

        public void RemoveAboveY(double YValue)
        {
            List<Point3D> temp = new List<Point3D>();
            for (int i = 0; i < Points.Count; i++)
            {
                if (Points[i].Y < YValue) temp.Add(Points[i].DeepClone());
            }

            Points.Clear();
            Points = temp.DeepClone();
        }
        public void RemoveBelowY(double YValue)
        {
            List<Point3D> temp = new List<Point3D>();
            for (int i = 0; i < Points.Count; i++)
            {
                if (Points[i].Y > YValue) temp.Add(Points[i].DeepClone());
            }

            Points.Clear();
            Points = temp.DeepClone();
        }
        public void SmoothPath_5P(bool enableSmoothX, bool enableSmoothY, bool enableSmoothZ, double ratioP1, double ratioP2, double ratioP3, double ratioP4,double ratioP5)
        {
            List<Point3D> output = new List<Point3D>();

            double p1r = ratioP1 < 0 ? 0 : ratioP1;
            double p2r = ratioP2 < 0 ? 0 : ratioP2;
            double p3r = ratioP3 < 0 ? 0 : ratioP3;
            double p4r = ratioP4 < 0 ? 0 : ratioP4;
            double p5r = ratioP5 < 0 ? 0 : ratioP5;

            double sum = p1r + p2r + p3r + p4r + p5r;



            for (int i = 0; i < Points.Count; i++)
            {

                int index1 = i - 2;
                int index2 = i - 1;
                int index3 = i;
                int index4 = i + 1;
                int index5 = i + 2;

                if (index5 >= Points.Count)
                    index5 = i;

                if (index4 >= Points.Count)
                    index4 = i;

                if (index1 < 0)
                    index1 = i;

                if (index2 < 0)
                    index2 = i;

                Point3D outP;
                if (Points[i] is PointV3D p1)
                {
                    outP = new PointV3D(p1);
                }
                else
                {
                    outP = new Point3D(Points[i]);
                }

                if(i == 0)
                {
                    output.Add(outP);

                }
                else if (i == Points.Count-1)
                {
                    output.Add(outP);
                }
                else
                {
                    if (sum > 0)
                    {
                        if (enableSmoothX) outP.X = (Points[index1].X * p1r + Points[index2].X * p2r + Points[index3].X * p3r + Points[index4].X * p4r + Points[index5].X * p5r) / (sum);
                        if (enableSmoothY) outP.Y = (Points[index1].Y * p1r + Points[index2].Y * p2r + Points[index3].Y * p3r + Points[index4].Y * p4r + Points[index5].Y * p5r) / (sum);
                        if (enableSmoothZ) outP.Z = (Points[index1].Z * p1r + Points[index2].Z * p2r + Points[index3].Z * p3r + Points[index4].Z * p4r + Points[index5].Z * p5r) / (sum);
                    }
                    output.Add(outP);
                }
            }
            Points.Clear();
            Points.AddRange(output);
        }
        public void SmoothPath_4P(bool enableSmoothX,bool enableSmoothY,bool enableSmoothZ,double ratioP1, double ratioP2, double ratioP3, double ratioP4)
        {
            List<Point3D> output = new List<Point3D>();

            double p1r = ratioP1 < 0 ? 0 : ratioP1;
            double p2r = ratioP2 < 0 ? 0 : ratioP2;
            double p3r = ratioP3 < 0 ? 0 : ratioP3;
            double p4r = ratioP4 < 0 ? 0 : ratioP4;
            double sum = p1r + p2r + p3r + p4r;



            for (int i = 0; i < Points.Count; i++)
            {

                int index1 = i - 2;
                int index2 = i - 1;
                int index3 = i + 1;
                int index4 = i + 2;

                if (index3 >= Points.Count)
                    index3 = i;

                if (index4 >= Points.Count)
                    index4 = i;

                if (index1 < 0)
                    index1 = i;

                if (index2 < 0)
                    index2 = i;

                Point3D outP;
                if (Points[i] is PointV3D p1)
                {
                    outP = new PointV3D(p1);
                }
                else
                {
                    outP = new Point3D(Points[i]);
                }

                if (i == 0)
                {
                    output.Add(outP);

                }
                else if (i == Points.Count - 1)
                {
                    output.Add(outP);
                }
                else
                {
                    if (sum > 0)
                    {
                        if (enableSmoothX) outP.X = (Points[index1].X * p1r + Points[index2].X * p2r + Points[index3].X * p3r + Points[index4].X * p4r) / (sum);
                        if (enableSmoothY) outP.Y = (Points[index1].Y * p1r + Points[index2].Y * p2r + Points[index3].Y * p3r + Points[index4].Y * p4r) / (sum);
                        if (enableSmoothZ) outP.Z = (Points[index1].Z * p1r + Points[index2].Z * p2r + Points[index3].Z * p3r + Points[index4].Z * p4r) / (sum);
                    }
                    output.Add(outP);
                }
            }
            Points.Clear();
            Points.AddRange(output);
        }
        public void SmoothPath_3P(bool enableSmoothX, bool enableSmoothY, bool enableSmoothZ, double ratioP1, double ratioP2, double ratioP3)
        {
            List<Point3D> output = new List<Point3D>();

            double p1r = ratioP1 < 0 ? 0 : ratioP1;
            double p2r = ratioP2 < 0 ? 0 : ratioP2;
            double p3r = ratioP3 < 0 ? 0 : ratioP3;
            double sum = p1r + p2r + p3r;

            for (int i = 0; i < Points.Count; i++)
            {
                int index1 = i - 1;
                int index2 = i ;
                int index3 = i + 1;

                Point3D outP;
                if (Points[i] is PointV3D p1)
                {
                    outP = new PointV3D(p1);
                }
                else
                {
                    outP = new Point3D(Points[i]);
                }

                if (i == 0)
                {
                    output.Add(outP);

                }
                else if(i  == Points.Count-1)
                {
                    output.Add(outP);
                }
                else
                {
                    if (sum > 0)
                    {
                        if (enableSmoothX) outP.X = (Points[index1].X * p1r + Points[index2].X * p2r + Points[index3].X * p3r) / (sum);
                        if (enableSmoothY) outP.Y = (Points[index1].Y * p1r + Points[index2].Y * p2r + Points[index3].Y * p3r) / (sum);
                        if (enableSmoothZ) outP.Z = (Points[index1].Z * p1r + Points[index2].Z * p2r + Points[index3].Z * p3r) / (sum);
                    }
                    output.Add(outP);
                }
            }
            Points.Clear();
            Points.AddRange(output);
        }
        public void SmoothPathZ_4P(double ratioP1,double ratioP2,double ratioP3,double ratioP4)
        {
            List<Point3D> output = new List<Point3D>();

            double p1r = ratioP1 < 0 ? 0 : ratioP1;
            double p2r = ratioP2 < 0 ? 0 : ratioP2;
            double p3r = ratioP3 < 0 ? 0 : ratioP3;
            double p4r = ratioP4 < 0 ? 0 : ratioP4;
            double sum = p1r + p2r + p3r + p4r;



            for (int i = 0; i < Points.Count; i++)
            {

                int index1 = i - 2;
                int index2 = i - 1;
                int index3 = i + 1;
                int index4 = i + 2;

                if (index3 >= Points.Count)
                    index3 -= Points.Count;

                if (index4 >= Points.Count)
                    index4 -= Points.Count;

                if (index1 < 0)
                    index1 += Points.Count;

                if (index2 < 0)
                    index2 += Points.Count;

                Point3D outP;
                if (Points[i] is PointV3D p1)
                {
                    outP = new PointV3D(p1);
                }
                else
                {
                    outP = new Point3D(Points[i]);
                }

                if (sum > 0)
                {
                    outP.Z = (Points[index1].Z * p1r + Points[index2].Z * p2r + Points[index3].Z * p3r + Points[index4].Z * p4r) / (sum);
                }

                output.Add(outP);
            }
            Points.Clear();
            Points.AddRange(output);
        }
        public void SmoothPathY_4P(double ratioP1, double ratioP2, double ratioP3, double ratioP4)
        {
            List<Point3D> output = new List<Point3D>();

            double p1r = ratioP1 < 0 ? 0 : ratioP1;
            double p2r = ratioP2 < 0 ? 0 : ratioP2;
            double p3r = ratioP3 < 0 ? 0 : ratioP3;
            double p4r = ratioP4 < 0 ? 0 : ratioP4;
            double sum = p1r + p2r + p3r + p4r;

            for (int i = 0; i < Points.Count; i++)
            {

                int index1 = i - 2;
                int index2 = i - 1;
                int index3 = i + 1;
                int index4 = i + 2;

                if (index3 >= Points.Count)
                    index3 -= Points.Count;

                if (index4 >= Points.Count)
                    index4 -= Points.Count;

                if (index1 < 0)
                    index1 += Points.Count;

                if (index2 < 0)
                    index2 += Points.Count;

                Point3D outP;
                if (Points[i] is PointV3D p1)
                {
                    outP = new PointV3D(p1);
                }

                else
                {
                    outP = new Point3D(Points[i]);
                }

                if (sum > 0)
                {
                    outP.Y = (Points[index1].Y * p1r + Points[index2].Y * p2r + Points[index3].Y * p3r + Points[index4].Y * p4r) / sum;
                }
                output.Add(outP);
            }
            Points.Clear();
            Points.AddRange(output);

        }
        public void SmoothPathX_4P(double ratioP1, double ratioP2, double ratioP3, double ratioP4)
        {
            List<Point3D> output = new List<Point3D>();

            double p1r = ratioP1 < 0 ? 0 : ratioP1;
            double p2r = ratioP2 < 0 ? 0 : ratioP2;
            double p3r = ratioP3 < 0 ? 0 : ratioP3;
            double p4r = ratioP4 < 0 ? 0 : ratioP4;
            double sum = p1r + p2r + p3r + p4r;

            for (int i = 0; i < Points.Count; i++)
            {

                int index1 = i - 2;
                int index2 = i - 1;
                int index3 = i + 1;
                int index4 = i + 2;

                if (index3 >= Points.Count)
                    index3 -= Points.Count;

                if (index4 >= Points.Count)
                    index4 -= Points.Count;

                if (index1 < 0)
                    index1 += Points.Count;

                if (index2 < 0)
                    index2 += Points.Count;

                Point3D outP;
                if (Points[i] is PointV3D p1)
                {
                    outP = new PointV3D(p1);
                }
                else
                {
                    outP = new Point3D(Points[i]);
                }
                if (sum > 0)
                {
                    outP.X = (Points[index1].X * p1r + Points[index2].X * p2r + Points[index3].X * p3r + Points[index4].X * p4r) / sum;
                }
                output.Add(outP);
            }
            Points.Clear();
            Points.AddRange(output);

        }
        public void SetLineIndexForAllPts(int lineIndex)
        {
            for (int i = 0; i < Points.Count;i++)
            {
                if (Points[i].GetOption(typeof(LineOption)) is LineOption lo)
                {
                    lo.LineIndex = lineIndex;
                }
                else
                {
                    LineOption newlo = new LineOption() { LineIndex = lineIndex };
                    Points[i].AddOption(newlo);

                }
            }
        }
        public void ReduceSamePoint()
        {
            List<Point3D> Output = new List<Point3D>();
            Output.Add(Points[0]);
            for (int i = 0; i < Points.Count - 1; i++)
            {
                int i0 = i;
                int i1 = i + 1;
                Point3D P0 = Points[i0].DeepClone();
                Point3D P1 = Points[i1].DeepClone();

                Vector3D After = new Vector3D(P0, P1);
                if (After.L > 0.5)
                {
                    Output.Add(P1);
                }
            }

            Points.Clear();
            Points = Output.DeepClone();
        }

        /// <summary>
        /// 將線段中太相近的點消除
        /// </summary>
        public void FilterLineDupicatePts()
        {
            List<Point3D> output = new List<Point3D>();

            for (int i = 0; i < Points.Count - 1; i++)
            {

                Point3D p1 = Points[i];
                Point3D p2 = Points[i + 1];

                output.Add(p1);

                if (Point3D.Distance(p1, p2) > 0.01)
                    i++;

            }
            Points.Clear();
            Points = output.DeepClone();
        }

        public void SmoothLineKeepOriginAndLastXY()
        {
            List<Point3D> output = new List<Point3D>();

            for (int i = 0; i < Points.Count; i++)
            {
                if (i == 0 || i == (Points.Count - 1))
                {
                    output.Add(Points[i]);
                    continue;
                }


                int index2 = i - 1;
                int index3 = i + 1;


                Point3D outP = new Point3D(
                    (Points[index2].X * 0.25 + Points[i].X * 0.5 + Points[index3].X * 0.25),
                    (Points[index2].Y * 0.25 + Points[i].Y * 0.5 + Points[index3].Y * 0.25),
                     Points[i].Z
                );

                outP.tag = Points[i].tag;
                output.Add(outP);
            }
            Points.Clear();
            Points = output.DeepClone();
        }

        public void SmoothLineKeepOriginAndLast()
        {
            List<Point3D> output = new List<Point3D>();

            for (int i = 0; i < Points.Count; i++)
            {
                if (i == 0 || i == (Points.Count - 1))
                {
                    output.Add(Points[i]);
                    continue;
                }


                int index2 = i - 1;
                int index3 = i + 1;


                Point3D outP = new Point3D(
                    (Points[index2].X * 0.25 + Points[i].X * 0.5 + Points[index3].X * 0.25),
                    (Points[index2].Y * 0.25 + Points[i].Y * 0.5 + Points[index3].Y * 0.25),
                    (Points[index2].Z * 0.25 + Points[i].Z * 0.5 + Points[index3].Z * 0.25)
                );

                outP.tag = Points[i].tag;
                output.Add(outP);
            }
            Points.Clear();
            Points = output.DeepClone();
        }
        public PointCloud ToPointCloud()
        {
            return new PointCloud(Points);
        }
        public void SaveAsOpt(string FilePath, bool Append = false)
        {
            if (Path.GetExtension(FilePath).ToUpper() == ".XYZ") FilePath = FilePath.Replace(".xyz", ".opt");
            if (Path.GetExtension(FilePath).ToUpper() == ".TXT") FilePath = FilePath.Replace(".txt", ".opt");

            using (StreamWriter sw = new StreamWriter(FilePath, Append, Encoding.Default))
            {
                for (int i = 0; i < Points.Count; i++)
                {
                    string WriteData = "";
                    if (Points[i].GetType() == typeof(PointV3D))
                    {
                        PointV3D p = Points[i] as PointV3D;
                        WriteData = string.Format("{0:F2},{1:F2},{2:F2},{3:F2},{4:F2},{5:F2}", p.X, p.Y, p.Z, p.Vz.X, p.Vz.Y, p.Vz.Z);
                    }
                    else
                    {
                        Point3D p = Points[i];
                        WriteData = string.Format("{0:F2},{1:F2},{2:F2},0,0,0", p.X, p.Y, p.Z);
                    }
                    sw.WriteLine(WriteData);
                }
                if (Append) sw.WriteLine("");

                sw.Flush();
            }
        }
        public void SaveAsOpt2(string filePath)
        {

            if (Path.GetExtension(filePath).ToUpper() == ".XYZ") filePath = filePath.Replace(".xyz", ".opt2");
            if (Path.GetExtension(filePath).ToUpper() == ".TXT") filePath = filePath.Replace(".txt", ".opt2");

            using (StreamWriter sw = new StreamWriter(filePath, false, Encoding.Default,65535))
            {
                List<string> finalString = new List<string>();
                finalString = GetOpt2PathStringList();
                for (int i = 0; i < finalString.Count; i++)
                {
                    sw.WriteLine(finalString[i]);
                }

                sw.Flush();
            }
        }
        public List<string> GetOptPathStringList()
        {
            List<string> output = new List<string>();
            for (int i = 0; i < Points.Count; i++)
            {
                if (Points[i].GetType() == typeof(PointV3D))
                {
                    PointV3D p = Points[i] as PointV3D;
                    output.Add(string.Format("{0:F2},{1:F2},{2:F2},{3:F2},{4:F2},{5:F2}", p.X, p.Y, p.Z, p.Vz.X, p.Vz.Y, p.Vz.Z));
                }
                else
                {
                    Point3D p = Points[i];
                    output.Add(string.Format("{0:F2},{1:F2},{2:F2},0,0,0", p.X, p.Y, p.Z));
                }
            }
            output.Add("");
            return output;
        }
        public List<string> GetOpt2PathStringList()
        {
            List<string> output = new List<string>();
            for (int i = 0; i < Points.Count; i++)
            {
                if (Points[i].GetType() == typeof(PointV3D))
                {
                    PointV3D p = Points[i] as PointV3D;
                    output.Add(string.Format("{0:F3},{1:F3},{2:F3},{3:F3},{4:F3},{5:F3},{6:F3},{7:F3},{8:F3},{9:F3},{10:F3},{11:F3}", p.X, p.Y, p.Z, p.Vz.X, p.Vz.Y, p.Vz.Z, p.Vy.X, p.Vy.Y, p.Vy.Z, p.Vx.X, p.Vx.Y, p.Vx.Z));
                }
                else
                {
                    Point3D p = Points[i];
                    output.Add(string.Format("{0:F2},{1:F2},{2:F2},0,0,0,0,0,0,0,0,0", p.X, p.Y, p.Z));
                }
            }
            output.Add("");
            return output;
        }

        public void SaveAsTxt(string FilePath, bool Append = false)
        {
            if (Path.GetExtension(FilePath).ToUpper() == ".XYZ") FilePath = FilePath.Replace(".xyz", ".txt");
            if (Path.GetExtension(FilePath).ToUpper() == ".TXT") FilePath = FilePath.Replace(".opt", ".txt");

            using (StreamWriter sw = new StreamWriter(FilePath, Append, Encoding.Default))
            {
                for (int i = 0; i < Points.Count; i++)
                {
                    string WriteData = string.Format("{0:F2},{1:F2},{2:F2},0,0,0", Points[i].X, Points[i].Y, Points[i].Z);
                    sw.WriteLine(WriteData);
                }
                if (Append) sw.WriteLine("");

                sw.Flush();
                sw.Close();
            }
        }

#if Backup
        public Polyline ReSample_SkipOriginalPoint(double ExpectedSampleDistance, double LimitDegree)
        {

            List<double> ChangedAngles = new List<double>();                   // 紀錄線段間 "角度" 的變化
            Polyline keptPoints = new Polyline();     //'保留大轉折點
            double pointInterval = ExpectedSampleDistance;                                  // 2.54              'mm      針距
            Point3D temp = new Point3D();
            Polyline sPoints = new Polyline();

            ChangedAngles = CalculateAngles();       //'   計算線段間的角度變化

            int ttmp = 1; //ttmp =0;

            for (int i = ttmp; i <= ChangedAngles.Count - 1; i++)
            {
                if (ChangedAngles[i] < LimitDegree)
                {
                    keptPoints.Add(Points[i + 1]);         // 把大轉折點保留起來
                }
            }

            sPoints.Add(Points[0]);   //' 第一點一定要車

            temp = sPoints.Points[0];        //  ' 第一點存入 temp

            for (int i = 1; i <= Points.Count - 1; i++)
            {
                Vector3D tmpV = new Vector3D(temp, Points[i]);
                double distance2NextImagePoint = tmpV.L;    //'''計算暫存點與下個影像點的距離

                while (distance2NextImagePoint > pointInterval)
                {
                    Point3D midPoint = new Point3D();

                    if (i == 1)
                    {
                        midPoint.X = temp.X + (Points[i].X - temp.X) * (pointInterval / distance2NextImagePoint);  //  ' 內插法
                        midPoint.Y = temp.Y + (Points[i].Y - temp.Y) * (pointInterval / distance2NextImagePoint);

                        midPoint.X = Math.Round(midPoint.X, 1);
                        midPoint.Y = Math.Round(midPoint.Y, 1);

                        //midPoint.speed = points[i].speed;
                        //midPoint.tension = points[i].tension;
                        //midPoint.presserFootHight = points[i].presserFootHight;

                        sPoints.Add(midPoint);
                        temp = midPoint;
                        tmpV = new Vector3D(temp, Points[i]);
                        distance2NextImagePoint = tmpV.L;                                     //' 計算暫存點與下個影像點的距離
                    }
                    else
                    {

                        double checkIfAligned = Math.Abs((temp.X - Points[i - 1].X) * (Points[i].Y - Points[i - 1].Y) - (temp.Y - Points[i - 1].Y) * (Points[i].X - Points[i - 1].X));

                        if (checkIfAligned < 0.1)
                        {
                            midPoint.X = temp.X + (Points[i].X - temp.X) * (pointInterval / distance2NextImagePoint);   //' 內插法
                            midPoint.Y = temp.Y + (Points[i].Y - temp.Y) * (pointInterval / distance2NextImagePoint);

                            midPoint.X = Math.Round(midPoint.X, 1);
                            midPoint.Y = Math.Round(midPoint.Y, 1);

                            //midPoint.speed = points[i].speed;
                            //midPoint.tension = points[i].tension;
                            //midPoint.presserFootHight = points[i].presserFootHight;

                            sPoints.Add(midPoint);
                            temp = midPoint;
                            tmpV = new Vector3D(temp, Points[i]);
                            distance2NextImagePoint = tmpV.L;                                      //' 計算暫存點與下個影像點的距離
                        }
                        else
                        {
                            Point3D frontPoint = Points[i - 1];
                            Point3D backPoint = Points[i];

                            midPoint.X = (frontPoint.X + backPoint.X) / 2; //' 二分逼近法
                            midPoint.Y = (frontPoint.Y + backPoint.Y) / 2;

                            tmpV = new Vector3D(temp, midPoint);
                            double distance2MidPoint = tmpV.L;

                            while (Math.Abs(distance2MidPoint - pointInterval) > 0.03)
                            {
                                if (distance2MidPoint > pointInterval)             //   '    不會有等於的狀況, 因為 while (... > 0.03)
                                {
                                    backPoint = midPoint;
                                }
                                else
                                {
                                    frontPoint = midPoint;
                                }

                                midPoint.X = (frontPoint.X + backPoint.X) / 2;
                                midPoint.Y = (frontPoint.Y + backPoint.Y) / 2;
                                tmpV = new Vector3D(temp, midPoint);
                                distance2MidPoint = tmpV.L;                                     //' 計算暫存點與下個影像點的距離
                            }

                            midPoint.X = Math.Round(midPoint.X, 1);
                            midPoint.Y = Math.Round(midPoint.Y, 1);
                            //midPoint.speed = points[i].speed;
                            //midPoint.tension = points[i].tension;
                            //midPoint.presserFootHight = points[i].presserFootHight;

                            sPoints.Add(midPoint);
                            temp = midPoint;
                            tmpV = new Vector3D(temp, Points[i]);
                            distance2NextImagePoint = tmpV.L; //   ' 計算暫存點與下個影像點的距離
                        }
                    }
                }//while
                //if (i == 59)
                //{
                //  //i = i
                //}

                if (i == Points.Count - 1 && distance2NextImagePoint < pointInterval)
                {
                    if (distance2NextImagePoint > 0.5)
                    {
                        sPoints.Add(Points[i]);
                    }
                }
            }//for

            return sPoints;
        }
#else
        public Polyline ReSample_SkipOriginalPoint(double ExpectedSampleDistance, double LimitDegree)
        {

            List<double> ChangedAngles = new List<double>();                   // 紀錄線段間 "角度" 的變化
            List<int> KeyIndex = new List<int>();  //'保留大轉折點
            //double pointInterval = ExpectedSampleDistance;                                  // 2.54              'mm      針距
            Point3D temp = new Point3D();
            Polyline sPoints = new Polyline();

            ChangedAngles = CalculateAngles();       //'   計算線段間的角度變化

            int ttmp = 1; //ttmp =0;

            for (int i = ttmp; i <= ChangedAngles.Count - 1; i++)
            {
                if (ChangedAngles[i] < LimitDegree)
                {
                    KeyIndex.Add(i);         // 把大轉折點保留起來
                }
            }

            sPoints.Add(Points[0]);   //' 第一點一定要車

            temp = Points[0];        //  ' 第一點存入 temp

            for (int i = 1; i <= Points.Count - 2; i++)
            {
                if (KeyIndex.Contains(i))
                {
                    sPoints.Add(Points[i]);
                    temp = Points[i];
                    continue;
                }
                Vector3D tmpV = new Vector3D();
                double distance2NextImagePoint = Point3D.Distance(temp, Points[i]);    //'''計算暫存點與下個影像點的距離
                bool IsOverLimit = distance2NextImagePoint > ExpectedSampleDistance;
                bool IsLessLimit = distance2NextImagePoint < ExpectedSampleDistance;
                Ball ball = new Ball(temp, ExpectedSampleDistance);
                Point3D TargetP = Points[i];
                while (IsOverLimit)
                {
                    // 內插
                    tmpV = new Vector3D(temp, TargetP);
                    Line L = new Line(temp, tmpV, tmpV.L);
                    ball = new Ball(temp, ExpectedSampleDistance);
                    Point3D InterP = new Point3D();
                    bool IsIntersect = ball.Intersect(L, out InterP);
                    if (IsIntersect)
                    {
                        temp = InterP;
                        sPoints.Add(InterP);
                    }
                    distance2NextImagePoint = Point3D.Distance(temp, TargetP);    //'''計算暫存點與下個影像點的距離
                    IsOverLimit = distance2NextImagePoint > ExpectedSampleDistance;
                    IsLessLimit = distance2NextImagePoint < ExpectedSampleDistance;

                }


                if (IsLessLimit)
                {

                    tmpV = new Vector3D(Points[i], Points[i + 1]);
                    Line L = new Line(Points[i], tmpV, tmpV.L);
                    Point3D InterP = new Point3D();
                    bool IsIntersect = ball.Intersect(L, out InterP);
                    if (IsIntersect)
                    {

                        temp = InterP;
                        sPoints.Add(InterP);
                    }

                }
                else
                {
                    sPoints.Add(new Point3D(temp));
                }
                if (i == Points.Count - 2)
                {
                    Point3D LastP = sPoints.Points[sPoints.Count - 1];
                    Point3D EndP = Points[i + 1];

                    double CalDis = Point3D.Distance(LastP, EndP);
                    if (CalDis > 1.0) sPoints.Add(EndP);
                }
            }//for

            return sPoints;
        }
#endif
        private List<double> CalculateAngles()
        {
            List<double> temp = new List<double>();
            double CalAngle = 0.0;
            for (int i = 0; i <= Points.Count - 3; i++)
            {
                Point3D n = Points[i];
                Point3D n1 = Points[i + 1];
                Point3D n2 = Points[i + 2];

                Vector3D v1 = new Vector3D(n1, n);
                Vector3D v2 = new Vector3D(n1, n2);
                CalAngle = Vector3D.Degree(v1, v2);
                temp.Add(CalAngle);
            }
            return temp;
        }

        /// <summary>
        /// 有序3D點重取樣插補，忽略原有的點以完全重新取樣
        /// </summary>
        public Polyline ReSample_SkipOriginalPoint(double ExpectedSampleDistance, bool IsClosed, bool isKeepLast = false)
        {
            // 資料點不足
            if (Points.Count < 3) return null;

            // 備份Pin
            Polyline Ptemp = this.DeepClone();
            Polyline Output = new Polyline();
            //Pout.Clear();

            Point3D p = new Point3D();
            Point3D p_prev = new Point3D();
            Point3D p_next = new Point3D();

            double TotalLength = 0.0;       // 曲線總長
            int estimated_sample_num = 0;   // 估計的取樣數
            double sample_distance = 0.0;   // 取樣間隔                      

            // 估計曲線總長
            //Ptemp[0].Dt = Point3D.Distance(Ptemp[0], Ptemp[Ptemp.Count - 1]);
            TotalLength = Ptemp.Length;
            for (int i = 0; i < Ptemp.Count - 1; i++)
            {
                p_prev = Ptemp.Points[i];
                p_next = Ptemp.Points[i + 1];
                p_next.Dt = Point3D.Distance(p_prev, p_next);    //將計算到的分段長度存入每一點以備後續使用
                TotalLength += p_next.Dt;
            }

            // 重新計算適合的取樣間隔
            // 經過計算後，sample_distance <= ExpectedSampleDistance
            estimated_sample_num = (int)Math.Ceiling(TotalLength / ExpectedSampleDistance);
            sample_distance = TotalLength / estimated_sample_num;

            // ==================================================== 等距離取樣演算法 +
            double accumulative_length = 0.0;   //累加的計算長度
            Vector3D uV = new Vector3D();         //單位方向向量



            //先加入首點
            Output.Add(new Point3D(Ptemp.Points[0]));

            int MaxCount = 0;
            if (IsClosed)
                MaxCount = Ptemp.Count;       //如果要完成封閉曲線，則跑滿全部的點
            else
                MaxCount = Ptemp.Count - 1;   //反之只跑到N-1點

            //逐點累進計算
            for (int i = 0; i < MaxCount; i++)
            {
                //拆點啟始點
                p_prev = Ptemp.Points[i];

                //拆點結束點
                if (i == Ptemp.Count - 1)
                    p_next = Ptemp.Points[0];    //欲完成封閉曲線，所以最後一次拆點的結束點是P[0]
                else
                    p_next = Ptemp.Points[i + 1];

                // 當累進距離超過取樣距離，進行取樣
                if (accumulative_length + p_next.Dt > sample_distance)
                {
                    // 先計算從這點到下點之方向向量
                    uV.X = p_next.X - p_prev.X;
                    uV.Y = p_next.Y - p_prev.Y;
                    uV.Z = p_next.Z - p_prev.Z;
                    uV.X /= p_next.Dt;
                    uV.Y /= p_next.Dt;
                    uV.Z /= p_next.Dt;

                    // 計算拆點長度
                    double cut_length = sample_distance - accumulative_length;
                    // 從這點出發，在朝向下一點的方向，拆點長度的位置上插入一個新取樣點
                    p = new Point3D();
                    p.X = p_prev.X + cut_length * uV.X;
                    p.Y = p_prev.Y + cut_length * uV.Y;
                    p.Z = p_prev.Z + cut_length * uV.Z;
                    p.flag = p_prev.flag;
                    Output.Add(p);

                    // 重複檢驗在這點到下點的線段上還可以再插入幾個取樣點
                    double remain_length = p_next.Dt - cut_length;
                    int remain_num = (int)Math.Ceiling(remain_length / sample_distance);
                    // 如果還可以繼續取樣，則進入此迴圈
                    for (int j = 1; j < remain_num; j++)
                    {
                        // 計算拆點長度
                        cut_length += sample_distance;
                        // 從這點出發，在朝向下一點的方向，拆點長度的位置上插入一個新取樣點
                        p = new Point3D();
                        p.X = p_prev.X + cut_length * uV.X;
                        p.Y = p_prev.Y + cut_length * uV.Y;
                        p.Z = p_prev.Z + cut_length * uV.Z;
                        p.flag = p_prev.flag;
                        Output.Add(p);
                    }

                    // 更新累進距離，但是已經取樣拆點過的長度要扣掉
                    accumulative_length = p_next.Dt - cut_length;
                }
                else
                {
                    // 當累進距離沒有達到取樣距離，不取樣
                    // 僅將這點到下點之長度加入累進距離
                    accumulative_length += p_next.Dt;
                }




                if (IsClosed)
                {
                    //關於封閉曲線的尾點處理
                    Point3D p_start = Ptemp.Points[0];
                    Point3D p_end = Ptemp.Points[Ptemp.Count - 1];
                    if (Point3D.Distance(p_start, p_end) < 0.25 * sample_distance)
                    {
                        // 當最後一點與第一點的距離太近（小於預期的取樣距離的0.25倍）
                        // 則將最後一點刪掉
                        Output.Points.RemoveAt(Ptemp.Count - 1);
                    }
                }
                else if (isKeepLast)
                {
                    Output.Add(Ptemp.Points[Ptemp.Count - 1]);

                    Point3D p_end = Ptemp.Points[Ptemp.Count - 1];
                    Point3D p_before_end = Ptemp.Points[Ptemp.Count - 2];

                    if (Point3D.Distance(p_before_end, p_end) < 0.25 * sample_distance)
                    {
                        // 當最後一點與第一點的距離太近（小於預期的取樣距離的0.25倍）
                        // 則將最後一點刪掉
                        Output.Points.RemoveAt(Ptemp.Count - 2);
                    }
                }

            }
            // ==================================================== 等距離取樣演算法 -

            return Output;
        }
        /// <summary>
        /// 有序3D點重取樣插補，忽略原有的點以完全重新取樣
        /// </summary>
        public void ReSample_SkipOriginalPointAndTestLast(double ExpectedSampleDistance,double lastPtMinDis)
        {
            // 資料點不足
            if (Points.Count < 3) return;
            Polyline output = new Polyline();
            output.Add(Points[0]);
            int i = 1;
            while(i <Points.Count-1)
            {
                var lastPt = output.Points[output.Count - 1];

                var testPt = Points[i];
                Vector3D testV = new Vector3D(lastPt, testPt);

                var nextTestPt = Points[i + 1];
                Vector3D nextTestV = new Vector3D(testPt, nextTestPt);

                if (testV.L < ExpectedSampleDistance)
                {
                    double t = 0.0;
                    while (t <= 1)
                    {
                        double testL = Math.Pow(testV.X + nextTestV.X * t,2) + Math.Pow(testV.Y + nextTestV.Y * t,2) + Math.Pow(testV.Z + nextTestV.Z * t,2);
                        double powSampleDis = Math.Pow(ExpectedSampleDistance, 2);
                        if (testL >= powSampleDis)
                        {
                            var insertPt = nextTestPt.DeepClone();
                            insertPt = testPt + nextTestV * t;
                            if (insertPt.GetType() == typeof(PointV3D))
                            {
                                ((PointV3D)insertPt).Vx = new Vector3D(((PointV3D)testPt).Vx);
                                ((PointV3D)insertPt).Vy = new Vector3D(((PointV3D)testPt).Vy);
                                ((PointV3D)insertPt).Vz = new Vector3D(((PointV3D)testPt).Vz);

                            }
                            output.Add(insertPt);
                            i++;
                            break;
                        }
                        else t += 0.001;
                        if(t>1)
                        {
                            i++;
                        }
                    }
                }
                else if(testV.L == ExpectedSampleDistance)
                {
                    output.Add(testPt);
                    i++;
                }
                else
                {
                    var insertPt = testPt.DeepClone();
                    insertPt  = lastPt +  testV.GetUnitVector() * ExpectedSampleDistance;
                    if(insertPt.GetType() == typeof(PointV3D))
                    {
                        ((PointV3D)insertPt).Vx = new Vector3D(((PointV3D)testPt).Vx);
                        ((PointV3D)insertPt).Vy = new Vector3D(((PointV3D)testPt).Vy);
                        ((PointV3D)insertPt).Vz = new Vector3D(((PointV3D)testPt).Vz);

                    }
                    output.Add(insertPt);
                }
            }

            Vector3D endV = new Vector3D(output.LastPoint, LastPoint);
            if (endV.L <= lastPtMinDis)
            {
                output.RemoveLast();
            }
            output.Add(LastPoint);

            Points.Clear();
            Points.AddRange(output);
        }
        public void ReSample_SkipOriginalPointAndTestLast(double ExpectedSampleDistance, double lastPtMinDis,double tstep)
        {
            // 資料點不足
            if (Points.Count < 3) return;
            Polyline output = new Polyline();
            output.Add(Points[0]);
            int i = 1;
            double powSampleDis = Math.Pow(ExpectedSampleDistance, 2);
            if (tstep <= 0) tstep = 0.01;
            while (i < Points.Count - 1)
            {
                var lastPt = output.Points[output.Count - 1];

                var testPt = Points[i];
                Vector3D testV = new Vector3D(lastPt, testPt);

                var nextTestPt = Points[i + 1];
                Vector3D nextTestV = new Vector3D(testPt, nextTestPt);

                Vector3D nextFinalV = new Vector3D(lastPt, nextTestPt);

                if (testV.L < ExpectedSampleDistance)
                {
                    double t = 0.0;
                    if (nextFinalV.L <= ExpectedSampleDistance)
                    {
                        i++;
                        continue;
                    }
                    else
                    {
                        while (t <= 1)
                        {
                            double testL = Math.Pow(testV.X + nextTestV.X * t, 2) + Math.Pow(testV.Y + nextTestV.Y * t, 2) + Math.Pow(testV.Z + nextTestV.Z * t, 2);
                            if (testL >= powSampleDis)
                            {
                                var insertPt = nextTestPt.DeepClone();
                                insertPt = testPt + nextTestV * t;
                                if (insertPt.GetType() == typeof(PointV3D))
                                {
                                    ((PointV3D)insertPt).Vx = new Vector3D(((PointV3D)testPt).Vx);
                                    ((PointV3D)insertPt).Vy = new Vector3D(((PointV3D)testPt).Vy);
                                    ((PointV3D)insertPt).Vz = new Vector3D(((PointV3D)testPt).Vz);

                                }
                                output.Add(insertPt);
                                i++;
                                break;
                            }
                            else t += tstep;
                            if (t > 1)
                            {
                                i++;
                            }
                        }
                    }
                }
                else if (testV.L == ExpectedSampleDistance)
                {
                    output.Add(testPt);
                    i++;
                }
                else
                {
                    var insertPt = testPt.DeepClone();
                    insertPt = lastPt + testV.GetUnitVector() * ExpectedSampleDistance;
                    if (insertPt.GetType() == typeof(PointV3D))
                    {
                        ((PointV3D)insertPt).Vx = new Vector3D(((PointV3D)testPt).Vx);
                        ((PointV3D)insertPt).Vy = new Vector3D(((PointV3D)testPt).Vy);
                        ((PointV3D)insertPt).Vz = new Vector3D(((PointV3D)testPt).Vz);

                    }
                    output.Add(insertPt);
                }
            }

            Vector3D endV = new Vector3D(output.LastPoint, LastPoint);
            if (endV.L <= lastPtMinDis)
            {
                output.RemoveLast();
            }
            output.Add(LastPoint);

            Points.Clear();
            Points.AddRange(output);
        }

        /// <summary>
        /// 有序3D點重取樣插補。只有距離夠大需要插補的地方才插補，並保留原始點
        /// </summary>
        public Polyline ReSample_KeepOriginalPoint(double MinDistance, double MaxDistance, bool IsClosed)
        {
            // 資料點不足
            if (this.Count < 20) return null;

            Polyline Output = new Polyline();

            Point3D p = new Point3D();
            Point3D p_prev = new Point3D();
            Point3D p_next = new Point3D();
            Vector3D uV = new Vector3D();           //單位方向向量
            //將計算到的分段長度存入每一點以備後續使用
            for (int i = 0; i < Points.Count - 1; i++)
            {
                p_prev = Points[i];
                p_next = Points[i + 1];
                p_next.Dt = Point3D.Distance(p_prev, p_next);
            }

            // ==================================================== 等距離取樣演算法 +         

            //先加入首點
            Output.Add(new Point3D(Points[0]));

            int MaxCount = 0;
            if (IsClosed)
                MaxCount = this.Count;       //如果要完成封閉曲線，則跑滿全部的點
            else
                MaxCount = this.Count - 1;   //反之只跑到N-1點


            for (int i = 0; i < MaxCount; i++)
            {
                //拆點啟始點
                p_prev = Points[i];

                //拆點結束點
                if (i == Points.Count - 1)
                {
                    p_next = Points[0];    //欲完成封閉曲線，所以最後一次拆點的結束點是P[0]
                    p_next.Dt = Point3D.Distance(p_prev, p_next);
                }
                else
                    p_next = Points[i + 1];

                // 當prev點跟next點之間的距離在適合取樣的範圍內，進行取樣
                if (p_next.Dt > MinDistance && p_next.Dt < MaxDistance)
                {
                    // 先計算從這點到下點之方向向量
                    uV.X = p_next.X - p_prev.X;
                    uV.Y = p_next.Y - p_prev.Y;
                    uV.Z = p_next.Z - p_prev.Z;
                    uV.X /= p_next.Dt;
                    uV.Y /= p_next.Dt;
                    uV.Z /= p_next.Dt;

                    double cut_length = MinDistance;                    // prev到拆點位置的長度
                    double remain_length = p_next.Dt - cut_length;      // 拆點位置到next的長度

                    if (remain_length < MinDistance / 3.0)
                    {
                        //如果拆點位置已經離下一點很近，則放棄拆點，直接將next點加入
                        Output.Add(new Point3D(p_next));
                    }
                    else
                    {
                        // 從prev出發，在朝向下一點的方向，cut_length長度的位置上插入一個新取樣點
                        p = new Point3D();
                        p.X = p_prev.X + cut_length * uV.X;
                        p.Y = p_prev.Y + cut_length * uV.Y;
                        p.Z = p_prev.Z + cut_length * uV.Z;
                        Output.Add(p);

                        // 檢驗在這點到下點的線段上還可以再插入幾個取樣點
                        int remain_num = (int)Math.Ceiling(remain_length / MinDistance);

                        // 如果還可以繼續取樣，則進入此迴圈
                        for (int j = 1; j < remain_num; j++)
                        {
                            // 計算拆點長度
                            cut_length += MinDistance;
                            remain_length = p_next.Dt - cut_length;

                            if (remain_length < MinDistance / 3.0)
                            {
                                //如果拆點位置已經離下一點很近，則放棄拆點，直接將next點加入
                                Output.Add(new Point3D(p_next));
                                break;
                            }

                            // 從prev出發，在朝向下一點的方向，cut_length長度的位置上插入一個新取樣點
                            p = new Point3D();
                            p.X = p_prev.X + cut_length * uV.X;
                            p.Y = p_prev.Y + cut_length * uV.Y;
                            p.Z = p_prev.Z + cut_length * uV.Z;
                            Output.Add(p);
                        }
                    }
                }
                else
                {
                    // 不取樣直接將next點加入
                    Output.Add(new Point3D(p_next));
                }
            }

            // ==================================================== 等距離取樣演算法 -

            return Output;
        }
        public Polyline GetIntersectVz(KDTree<int> tree,double vzExtendLength,int searchRange,double searchR,double reduceR,bool ignoreZLowerTarget)
        {
            Polyline output = new Polyline();
            for (int i = 0; i < Points.Count; i++)
            {
                if(searchRange <=0) searchRange = 10;
                if (Points[i] is PointV3D pt)
                {
                    double increasement = vzExtendLength / (double)searchRange;
                    for (int j = 0; j < searchRange; j++)
                    {
                       Point3D ptVz =   pt.GetVzExtendPoint(j * increasement);
                      PointCloud nearCloud =   PointCloudCommon.GetNearestPointCloud(tree, ptVz, searchR);
                        if (nearCloud != null)
                        {
                            if (nearCloud.Count > 0)
                            {
                                Point3D ptAvg = nearCloud.Average;
                                double t = (ptAvg.Z - pt.Z) / pt.Vz.Z;
                                Point3D ptAtVector = pt.GetVzExtendPoint(t);
                                Vector3D testResultV = new Vector3D(pt, ptAtVector);
                                double dot = Vector3D.Dot(pt.Vz, testResultV);
                                if (ignoreZLowerTarget)
                                {
                                    if(dot > 0)
                                    {
                                        output.Add(ptAtVector);
                                        break;
                                    }
                                }
                                else
                                {
                                    output.Add(ptAtVector);
                                    break;
                                }
                            }
                        }
                    }
                }
            }
            output.ReduceByKDTree(reduceR);
            output.SmoothByKDTree(reduceR, true, true, false);
            return output;
        }
        public Polyline GetIntersectVz(KDTree<int> targetTree,
            double vzExtendLength, 
            int searchRange, 
            double startSearchR, 
            double EndSearchR, 
            double stepSearchR,
            bool ignoreZLowerTarget) 
        {
            Polyline output = new Polyline();

            for (int i = 0; i < Points.Count; i++)
            {
                if (Points[i] is PointV3D pt)
                {
                    List<Vector3D> candidateV = new List<Vector3D>() { pt.Vz };
                    Point3D point3D = PointCloudCommon.ProjectToSurface(pt.X, pt.Y, pt.Z, candidateV, targetTree, 15, startSearchR, EndSearchR, 0.1, vzExtendLength, searchRange);
                    //PointV3D pointV3D = PointCloudCommon.ProjectToSurface(pt.X, pt.Y,pt.Z, candidateV, targetTree, 5, startSearchR, EndSearchR);
                    output.Add(point3D);
                }
            }
            return output;
        }

        public Polyline GetIntersectVz(KDTree<int> tree, double vzExtendLength, int searchRange, double startSearchR,double EndSearchR, double stepSearchR,double reduceR, bool ignoreZLowerTarget)
        {
            Polyline output = new Polyline();
            for (int i = 0; i < Points.Count; i++)
            {
                if (searchRange <= 0) searchRange = 10;
                if (Points[i] is PointV3D pt)
                {
                    double increasement = vzExtendLength / (double)searchRange;
                    double tempSearchR = startSearchR;
                    bool isSearchOK = false;
                    while (tempSearchR <= EndSearchR)
                    {
                        for (int j = 0; j < searchRange; j++)
                        {
                            Point3D ptVz = pt.GetVzExtendPoint(j * increasement);
                            PointCloud nearCloud = PointCloudCommon.GetNearestPointCloud(tree, ptVz, tempSearchR);
                            if (nearCloud != null)
                            {
                                if (nearCloud.Count > 3)
                                {
                                    Point3D ptAvg = nearCloud.Average;
                                    double t = (ptAvg.Z - pt.Z) / pt.Vz.Z;
                                    Point3D ptAtVector = pt.GetVzExtendPoint(t);
                                    Vector3D testResultV = new Vector3D(pt, ptAtVector);
                                    double dot = Vector3D.Dot(pt.Vz, testResultV);
                                    if (ignoreZLowerTarget)
                                    {
                                        if (dot > 0)
                                        {
                                            output.Add(ptAtVector);
                                            isSearchOK = true;
                                            break;
                                        }
                                    }
                                    else
                                    {
                                        output.Add(ptAtVector);
                                        isSearchOK = true;
                                        break;
                                    }
                                }
                            }
                        }
                        if (isSearchOK) break;
                        else
                        {
                            tempSearchR += stepSearchR;
                        }
                    }
                }
            }
            //output.ReduceByKDTree(reduceR);
            //output.SmoothByKDTree(reduceR, true, true, false);
            return output;
        }


        public List<Pose> ToJsonPoses()
        {
            List<Pose> output = new List<Pose>();
            for (int i = 0; i < Count; i++)
            {
                if(Points[i] is PointV3D pV3D)
                {
                    Pose pose = new Pose()
                    {
                        X = pV3D.X,
                        Y = pV3D.Y,
                        Z = pV3D.Z,

                        XAxis = new double[] { pV3D.Vx.X, pV3D.Vx.Y, pV3D.Vx.Z },
                        YAxis = new double[] { pV3D.Vy.X, pV3D.Vy.Y, pV3D.Vy.Z }
                    };
                    output.Add(pose);
                }
                else if(Points[i] is Point3D p3D)
                {
                    Pose pose = new Pose()
                    {
                        X = p3D.X,
                        Y = p3D.Y,
                        Z = p3D.Z,

                    };
                    output.Add(pose);
                }
            }
            return output;
        }
    }
    [Serializable]
    public class LocatePercentOption : ObjectOption
    {
        public double Percent { get; set; } = 0.0;

        public LocatePercentOption()
        {

        }
    }
    [Serializable]
    public class LocateIndexOption : ObjectOption
    {
        public int Index { get; set; } = 0;

        public LocateIndexOption()
        {

        }
    }
    [Serializable]
    public class LineOption : ObjectOption
    {
        public int LineIndex { get; set; } = 0;

        public LineOption()
        {

        }
    }
}

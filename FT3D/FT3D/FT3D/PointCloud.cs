using Accord.Collections;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using RsLib.Common;
using Accord.Math;
namespace RsLib.PointCloud
{

    [Serializable]
    public partial class PointCloud:Object3D,IEnumerable<Point3D>
    {
        #region Main Property
        public List<Point3D> Points = new List<Point3D>();

        public int Count => (int)DataCount;
        public Point3D Median { get { return GetMedian(); } }
        public Point3D Q1 { get { return GetOneQuartile(); } }
        public Point3D Q3 { get { return GetThirdQuartile(); } }
        public Point3D Max { get { return GetMax(); } }
        public Point3D Min { get { return GetMin(); } }
        public Point3D Average { get { return GetAverage(); } }

        public Point3D FirstPoint { get => GetFirstPoint(); }
        public Point3D LastPoint { get => GetLastPoint(); }

        #endregion
        //public DisplayUnit Display = new DisplayUnit();
        public KDTree<int> kdTree = new KDTree<int>(3);
        public object KdTreeObj { get => kdTree; }

        public bool IsKdTreeBuilded => kdTree.Count > 0;
        public override uint DataCount => (uint)Points.Count;

        #region Private
        private Dictionary<string, Point3D> DuplicatePoints = new Dictionary<string, Point3D>();
        private bool IsAddNoDuplicate = false;
        #endregion

        public PointCloud()
        {
        }
        public PointCloud(double[] x,double[] y,double[] z)
        {
            if(x.Length == y.Length && x.Length == z.Length)
            {
                for(int i = 0; i < x.Length;i++)
                {
                    Point3D p = new Point3D(x[i], y[i], z[i]);
                    Points.Add(p);
                }
            }
        }
        public PointCloud(List<Point3D> Cloud)
        {
            Points = Cloud.DeepClone();
        }

        public Point3D GetMaxZPoint()
        {
            List<Point3D> SortedZ = Points.OrderByDescending(T => T.Z).ToList();

            return new Point3D(SortedZ[0]);
        }
        public Point3D GetMaxZPoint(Point3D ClosePoint)
        {
            List<Point3D> SortedZ = Points.OrderByDescending(T => T.Z).ToList();
            double Z = SortedZ[0].Z;
            double MinD = double.MaxValue;
            Point3D Output = new Point3D();
            for (int i = 0; i < SortedZ.Count; i++)
            {
                if (SortedZ[i].Z == Z)
                {
                    double CalMinD = Point3D.Distance(SortedZ[i], ClosePoint);
                    if (CalMinD < MinD)
                    {
                        MinD = CalMinD;
                        Output = SortedZ[i];
                    }
                }
            }
            return Output;
        }
        public List<Point3D> GetZValuesMax2Min()
        {
            List<Point3D> SortedZ = Points.OrderByDescending(T => T.Z).ToList();
            return SortedZ;
        }
        public Point3D GetMinZPoint()
        {
            List<Point3D> SortedZ = Points.OrderBy(T => T.Z).ToList();

            return new Point3D(SortedZ[0]);
        }
        public Point3D GetMinZPoint(Point3D ClosePoint)
        {
            List<Point3D> SortedZ = Points.OrderBy(T => T.Z).ToList();
            double Z = SortedZ[0].Z;
            double MinD = double.MaxValue;
            Point3D Output = new Point3D();
            for (int i = 0; i < SortedZ.Count; i++)
            {
                if (SortedZ[i].Z == Z)
                {
                    double CalMinD = Point3D.Distance(SortedZ[i], ClosePoint);
                    if (CalMinD < MinD)
                    {
                        MinD = CalMinD;
                        Output = SortedZ[i];
                    }
                }
            }
            return Output;
        }
        public List<Point3D> GetZValuesMin2Max()
        {
            List<Point3D> SortedZ = Points.OrderBy(T => T.Z).ToList();
            return SortedZ;
        }
        public Point3D GetMaxYPoint()
        {
            List<Point3D> SortedZ = Points.OrderByDescending(T => T.Y).ToList();

            return new Point3D(SortedZ[0]);
        }
        public Point3D GetMaxYPoint(Point3D ClosePoint)
        {
            List<Point3D> SortedY = Points.OrderByDescending(T => T.Y).ToList();

            double Y = SortedY[0].Y;
            double MinD = double.MaxValue;
            Point3D Output = new Point3D();
            for (int i = 0; i < SortedY.Count; i++)
            {
                if (SortedY[i].Y == Y)
                {
                    double CalMinD = Point3D.Distance(SortedY[i], ClosePoint);
                    if (CalMinD < MinD)
                    {
                        MinD = CalMinD;
                        Output = SortedY[i];
                    }
                }
            }
            return Output;
        }
        public List<Point3D> GetYValuesMax2Min()
        {
            List<Point3D> SortedY = Points.OrderByDescending(T => T.Y).ToList();
            return SortedY;
        }
        public Point3D GetMinYPoint()
        {
            List<Point3D> SortedY = Points.OrderBy(T => T.Y).ToList();

            return new Point3D(SortedY[0]);
        }
        public Point3D GetMinYPoint(Point3D ClosePoint)
        {
            List<Point3D> SortedY = Points.OrderBy(T => T.Y).ToList();
            double Y = SortedY[0].Y;
            double MinD = double.MaxValue;
            Point3D Output = new Point3D();
            for (int i = 0; i < SortedY.Count; i++)
            {
                if (SortedY[i].Y == Y)
                {
                    double CalMinD = Point3D.Distance(SortedY[i], ClosePoint);
                    if (CalMinD < MinD)
                    {
                        MinD = CalMinD;
                        Output = SortedY[i];
                    }
                }
            }
            return Output;
        }
        public PointCloud Multiply(Matrix4x4 matrix)
        {
            PointCloud output = new PointCloud();
            for(int i = 0; i < Points.Count;i++)
            {
                Type type = Points[i].GetType();
                if(type == typeof(Point3D))
                {
                    Point3D pt = Points[i];
                    Point3D p = Point3D.Multiply(pt,matrix);
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
        public PointCloud Multiply(double[,] matrixArr)
        {
            PointCloud output = new PointCloud();
            Matrix4x4 matrix = m_Func.ArrayToMatrix4x4(matrixArr);
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
        
        public List<Point3D> GetYValuesMin2Max()
        {
            List<Point3D> SortedY = Points.OrderBy(T => T.Y).ToList();
            return SortedY;
        }
        public Point3D GetMaxXPoint()
        {
            List<Point3D> SortedX = Points.OrderByDescending(T => T.X).ToList();

            return new Point3D(SortedX[0]);
        }
        public Point3D GetMaxXPoint(Point3D ClosePoint)
        {
            List<Point3D> SortedX = Points.OrderByDescending(T => T.X).ToList();

            double X = SortedX[0].X;
            double MinD = double.MaxValue;
            Point3D Output = new Point3D();
            for (int i = 0; i < SortedX.Count; i++)
            {
                if (SortedX[i].X == X)
                {
                    double CalMinD = Point3D.Distance(SortedX[i], ClosePoint);
                    if (CalMinD < MinD)
                    {
                        MinD = CalMinD;
                        Output = SortedX[i];
                    }
                }
            }
            return Output;
        }
        public List<Point3D> GetXValuesMax2Min()
        {
            List<Point3D> SortedX = Points.OrderByDescending(T => T.X).ToList();
            return SortedX;
        }
        public Point3D GetMinXPoint()
        {
            List<Point3D> SortedZ = Points.OrderBy(T => T.X).ToList();

            return new Point3D(SortedZ[0]);
        }
        public Point3D GetMinXPoint(Point3D ClosePoint)
        {
            List<Point3D> SortedX = Points.OrderBy(T => T.X).ToList();
            double X = SortedX[0].X;
            double MinD = double.MaxValue;
            Point3D Output = new Point3D();
            for (int i = 0; i < SortedX.Count; i++)
            {
                if (SortedX[i].X == X)
                {
                    double CalMinD = Point3D.Distance(SortedX[i], ClosePoint);
                    if (CalMinD < MinD)
                    {
                        MinD = CalMinD;
                        Output = SortedX[i];
                    }
                }
            }
            return Output;
        }
        public List<Point3D> GetXValuesMin2Max()
        {
            List<Point3D> SortedX = Points.OrderBy(T => T.X).ToList();
            return SortedX;
        }
        public void SmoothByKDTree(double Radius, bool SmoothX, bool SmoothY, bool SmoothZ)
        {
            if (Points.Count == 0) return;

            if(IsKdTreeBuilded == false)  BuildIndexKDtree();
            for (int i = 0; i < Points.Count; i++)
            {
                PointCloud temp = m_Func.GetNearestPointCloud(kdTree, Points[i], Radius);

                if (SmoothX) Points[i].X = temp.Average.X;
                if (SmoothY) Points[i].Y = temp.Average.Y;
                if (SmoothZ) Points[i].Z = temp.Average.Z;


            }
        }
        public void SmoothByKDTree(int PointCount, bool SmoothX, bool SmoothY, bool SmoothZ)
        {
            if (Points.Count == 0) return;

            if (IsKdTreeBuilded == false) BuildIndexKDtree();
            for (int i = 0; i < Points.Count; i++)
            {
                PointCloud temp = m_Func.GetNearestPointCloud(kdTree, Points[i], PointCount);
                Point3D avg = temp.Average;

                if (SmoothX) Points[i].X = avg.X;
                if (SmoothY) Points[i].Y = avg.Y;
                if (SmoothZ) Points[i].Z = avg.Z;


            }
        }

        public Polyline SortCloud(Point3D SortBasePoint, Point3D SortRefPoint)
        {
            Polyline output = new Polyline();

            Vector3D RefVec = new Vector3D(SortBasePoint, SortRefPoint);
            double RefL = RefVec.L;

            Dictionary<double, Point3D> AnglePointPair = new Dictionary<double, Point3D>();

            ParallelLoopResult PResult1 = Parallel.For(0, Points.Count, (int i, ParallelLoopState PLS) =>
            {
                Point3D CurrentPoint = Points[i].DeepClone();

                Vector3D CurrentV = new Vector3D(SortBasePoint, CurrentPoint);

                double CurrentL = CurrentV.L;
                double DotV = Vector3D.Dot(CurrentV, RefVec);

                double Theta = Math.Acos(DotV / (CurrentL * RefL)) * 180 / Math.PI;

                lock (AnglePointPair)
                {
                    if (!AnglePointPair.ContainsKey(Theta)) AnglePointPair.Add(Theta, CurrentPoint);
                }
            });
            Dictionary<double, Point3D> SortedAnglePointPair = new Dictionary<double, Point3D>();
            SortedAnglePointPair = AnglePointPair.OrderBy(t => t.Key).ToDictionary(t => t.Key, t => t.Value);

            foreach (KeyValuePair<double, Point3D> kvp in SortedAnglePointPair)
            {
                output.Add(kvp.Value.DeepClone());
            }

            return output;
        }
        //public Polyline SortCloudByKDTree(double Radius, Point3D P_Start,bool IsClosed = true)
        //{
        //    Polyline output = new Polyline();
        //    Point3D StartP = new Point3D(P_Start);
        //    KDTree<int> temp = BuildIndexKDtree();
        //    output.Add(P_Start);

        //    //bool IsFirstRound = true;
        //    while (true)
        //    {
        //        double SetRadius = Radius;
        //        List<int> FoundIndex = new List<int>();
        //        PointCloud NearestCloud = m_Func.GetNearestPointCloud(temp, P_Start, SetRadius, out FoundIndex);
        //        List<Point3D> NewCloud = new List<Point3D>();
        //        while (NearestCloud.Count == 0)
        //        {
        //            SetRadius += 0.5;

        //            NearestCloud = m_Func.GetNearestPointCloud(temp, P_Start, SetRadius, out FoundIndex);
        //        }
        //        for (int i = 0; i < Points.Count; i++)
        //        {
        //            if (!FoundIndex.Contains(i)) NewCloud.Add(Points[i].DeepClone());
        //        }
        //        double MaxDis = double.MinValue;
        //        Point3D MaxP = new Point3D();
        //        //bool IsContainEndP = false;
        //        for (int i = 0; i < NearestCloud.Points.Count; i++)
        //        {
        //            double CalDis = Math.Round(Point3D.Distance(P_Start, NearestCloud.Points[i]), 2);


        //            //double CalD_EndP = Math.Round(Point3D.Distance(StartP, NearestCloud.Points[i]), 2);
        //            //if (CalD_EndP < 0.1 && !IsFirstRound) IsContainEndP = true;

        //            if (CalDis > MaxDis)
        //            {
        //                MaxDis = CalDis;
        //                MaxP = NearestCloud.Points[i];
        //            }
        //        }
        //        double tempD = Math.Round(Point3D.Distance(StartP, P_Start), 2);
        //        if(NewCloud.Count < 10) 
        //            if (SetRadius > tempD)  break;

        //        P_Start = MaxP;

        //        output.Add(P_Start);

        //        if (NewCloud.Count == 0) break;
        //        Points.Clear();

        //        Points = NewCloud;
        //        temp = BuildIndexKDtree();
        //    }
        //    if (IsClosed) output.Add(StartP);
        //    temp = output.BuildIndexKDtree();

        //    Polyline FinalOutput = new Polyline();
        //    for (int i = 0; i < output.Count; i++)
        //    {
        //        PointCloud N = m_Func.GetNearestPointCloud(temp, output.Points[i], 2.0 * Radius);

        //        if (N.Count > 1)
        //        {
        //            FinalOutput.Add(output.Points[i]);
        //        }
        //    }
        //    FinalOutput.Cal();
        //    return FinalOutput;
        //}
        //public Polyline SortCloudByKDTree(double Radius, Point3D P_Start, double SmoothRad,bool IsClosed = true)
        //{
        //    Polyline output = new Polyline();
        //    Point3D StartP = new Point3D(P_Start);

        //    KDTree<int> temp = BuildIndexKDtree();
        //    KDTree<int> temp2 = BuildIndexKDtree();
        //    output.Add(P_Start);
        //    while (true)
        //    {
        //        double SetRadius = Radius;
        //        List<int> FoundIndex = new List<int>();
        //        PointCloud NearestCloud = m_Func.GetNearestPointCloud(temp, P_Start, SetRadius, out FoundIndex);
        //        List<Point3D> NewCloud = new List<Point3D>();
        //        while (NearestCloud.Count == 0)
        //        {
        //            SetRadius += 0.5;
        //            NearestCloud = m_Func.GetNearestPointCloud(temp, P_Start, SetRadius, out FoundIndex);
        //        }
        //        for (int i = 0; i < Points.Count; i++)
        //        {
        //            if (!FoundIndex.Contains(i)) NewCloud.Add(Points[i].DeepClone());
        //        }
        //        double MaxDis = double.MinValue;
        //        Point3D MaxP = new Point3D();

        //        for (int i = 0; i < NearestCloud.Points.Count; i++)
        //        {
        //            double CalDis = Math.Round(Point3D.Distance(P_Start, NearestCloud.Points[i]), 2);
        //            if (CalDis > MaxDis)
        //            {
        //                MaxDis = CalDis;
        //                MaxP = NearestCloud.Points[i];
        //            }
        //        }
        //        if (SmoothRad > 0)
        //        {
        //            if (Point3D.Distance(MaxP, StartP) < 0.1)
        //            {
        //                P_Start = MaxP;
        //            }
        //            else
        //            {
        //                PointCloud MaxPNear = m_Func.GetNearestPointCloud(temp2, MaxP, SmoothRad, out FoundIndex);
        //                Point3D AvgMapP = MaxPNear.Average;

        //                double tempD = Math.Round(Point3D.Distance(StartP, AvgMapP), 2);

        //                if (NewCloud.Count < 10)
        //                {
        //                    if (SetRadius > tempD) break;
        //                }
        //                P_Start = AvgMapP;
        //            }
        //        }
        //        else
        //        {
        //            P_Start = MaxP;
        //        }
        //        if (NewCloud.Count == 0)
        //        {
        //            //here is problem!!!!!!
        //        }
        //        output.Add(P_Start);
        //        if (NewCloud.Count == 0) break;

        //        Points.Clear();
        //        Points = NewCloud;
        //        temp = BuildIndexKDtree();
        //    }
        //    if(IsClosed) output.Add(StartP);
        //    temp = output.BuildIndexKDtree();

        //    Polyline FinalOutput = new Polyline();
        //    for (int i = 0; i < output.Count; i++)
        //    {
        //        PointCloud N = m_Func.GetNearestPointCloud(temp, output.Points[i], 2.0 * Radius);

        //        if (N.Count > 1)
        //        {
        //            FinalOutput.Add(output.Points[i]);
        //        }
        //    }
        //    FinalOutput.Cal();
        //    return FinalOutput;
        //}
        public Polyline SortCloudByKDTree(double Radius, Point3D P_Start, Point3D P_End, double SmoothRad, bool IsClosed = true)
        {
            Polyline output = new Polyline();
            Point3D StartP = new Point3D(P_Start);

            KDTree<int> temp = GetIndexKDtree();
            KDTree<int> temp2 = GetIndexKDtree();
            output.Add(P_Start);
            while (true)
            {
                double SetRadius = Radius;
                List<int> FoundIndex = new List<int>();
                PointCloud NearestCloud = m_Func.GetNearestPointCloud(temp, P_Start, SetRadius, out FoundIndex);
                List<Point3D> NewCloud = new List<Point3D>();
                while (NearestCloud.Count == 0)
                {
                    SetRadius += 0.5;
                    NearestCloud = m_Func.GetNearestPointCloud(temp, P_Start, SetRadius, out FoundIndex);
                }
                for (int i = 0; i < Points.Count; i++)
                {
                    if (!FoundIndex.Contains(i)) NewCloud.Add(Points[i]);
                }
                double MaxDis = double.MinValue;
                Point3D MaxP = new Point3D();

                for (int i = 0; i < NearestCloud.Points.Count; i++)
                {
                    double CalDis = Math.Round(Point3D.Distance(P_Start, NearestCloud.Points[i]), 2);
                    if (CalDis > MaxDis)
                    {
                        MaxDis = CalDis;
                        MaxP = NearestCloud.Points[i];
                    }
                }
                if (SmoothRad > 0)
                {
                    if (Point3D.Distance(MaxP, StartP) < 0.1)
                    {
                        P_Start = MaxP;
                    }
                    else
                    {
                        PointCloud MaxPNear = m_Func.GetNearestPointCloud(temp2, MaxP, SmoothRad, out FoundIndex);
                        Point3D AvgMapP = MaxPNear.Average;

                        double tempD = Math.Round(Point3D.Distance(StartP, AvgMapP), 2);

                        if (NewCloud.Count < 10)
                        {
                            if (SetRadius > tempD) break;
                        }
                        P_Start = AvgMapP;
                    }
                }
                else
                {
                    P_Start = MaxP;
                }
                int Count_1 = output.Count - 1;
                int Count_2 = output.Count - 2;
                if (output.Count > 1)
                {
                    Point3D p_1 = output.Points[Count_1];
                    Point3D p_2 = output.Points[Count_2];

                    Vector3D CalV1 = new Vector3D(p_1, MaxP);
                    Vector3D CalRefV = new Vector3D(p_1, P_End);

                    if (CalRefV.L < 5.0)
                    {
                        double DotValue = Vector3D.Dot(CalRefV, CalV1);
                        if (DotValue < 0) break;
                    }
                }


                output.Add(P_Start);
                if (NewCloud.Count == 0) break;

                Points.Clear();
                Points = NewCloud;
                temp = GetIndexKDtree();
            }
            if (IsClosed) output.Add(StartP);
            temp = output.GetIndexKDtree();

            Polyline FinalOutput = new Polyline();
            for (int i = 0; i < output.Count; i++)
            {
                PointCloud N = m_Func.GetNearestPointCloud(temp, output.Points[i], 2.0 * Radius);

                if (N.Count > 1)
                {
                    FinalOutput.Add(output.Points[i]);
                }
            }
            FinalOutput.CalculatePercent();
            return FinalOutput;
        }
        //public Polyline SortCloudByKDTree(double Radius,int ClosePointCount, Point3D P_Start, double SmoothRad, bool IsClosed = true)
        //{
        //    Polyline output = new Polyline();
        //    Point3D StartP = new Point3D(P_Start);

        //    KDTree<int> temp = BuildIndexKDtree();
        //    KDTree<int> temp2 = BuildIndexKDtree();
        //    output.Add(P_Start);
        //    while (true)
        //    {
        //        double SetRadius = Radius;
        //        List<int> FoundIndex = new List<int>();
        //        PointCloud NearestCloud = m_Func.GetNearestPointCloud(temp, P_Start, ClosePointCount, out FoundIndex);
        //        List<Point3D> NewCloud = new List<Point3D>();
        //        //while (NearestCloud.Count == 0)
        //        //{
        //        //    SetRadius += 0.5;
        //        //    NearestCloud = m_Func.GetNearestPointCloud(temp, P_Start, SetRadius, out FoundIndex);
        //        //}
        //        for (int i = 0; i < Points.Count; i++)
        //        {
        //            if (!FoundIndex.Contains(i)) NewCloud.Add(Points[i]);
        //        }
        //        double MaxDis = double.MinValue;
        //        Point3D MaxP = new Point3D();

        //        for (int i = 0; i < NearestCloud.Points.Count; i++)
        //        {
        //            double CalDis = Math.Round(Point3D.Distance(P_Start, NearestCloud.Points[i]), 2);
        //            if (CalDis > MaxDis)
        //            {

        //                MaxDis = CalDis;
        //                MaxP = NearestCloud.Points[i];

        //            }
        //        }

        //        if (SmoothRad > 0)
        //        {
        //            if (Point3D.Distance(MaxP, StartP) < 0.1)
        //            {
        //                P_Start = MaxP;
        //            }
        //            else
        //            {
        //                PointCloud MaxPNear = m_Func.GetNearestPointCloud(temp2, MaxP, SmoothRad, out FoundIndex);
        //                Point3D AvgMapP = MaxPNear.Average;

        //                double tempD = Math.Round(Point3D.Distance(StartP, AvgMapP), 2);

        //                if (NewCloud.Count < 10)
        //                {
        //                    if (SetRadius > tempD) break;
        //                }
        //                P_Start = AvgMapP;
        //            }
        //        }//(SmoothRad > 0)
        //        else
        //        {
        //            P_Start = MaxP;
        //        }//(SmoothRad > 0)

        //        output.Add(P_Start);
        //        if (NewCloud.Count == 0) break;

        //        Points.Clear();
        //        Points = NewCloud;
        //        temp = BuildIndexKDtree();
        //    }
        //    if (IsClosed) output.Add(StartP);

        //    temp = output.BuildIndexKDtree();

        //    Polyline FinalOutput = new Polyline();
        //    for (int i = 0; i < output.Count; i++)
        //    {
        //        PointCloud N = m_Func.GetNearestPointCloud(temp, output.Points[i], 2.0 * Radius);

        //        if (N.Count > 1)
        //        {
        //            FinalOutput.Add(output.Points[i]);
        //        }
        //    }
        //    FinalOutput.Cal();
        //    return FinalOutput;
        //}
        public Polyline SortCloudByKDTree(int ClosePointCount, Point3D P_Start, Point3D P_End, double SmoothRad, bool IsClosed = true)
        {
            Polyline output = new Polyline();
            Point3D StartP = new Point3D(P_Start);

            KDTree<int> temp = GetIndexKDtree();
            KDTree<int> temp2 = GetIndexKDtree();
            output.Add(P_Start);
            while (true)
            {
                List<int> FoundIndex = new List<int>();
                PointCloud NearestCloud = m_Func.GetNearestPointCloud(temp, P_Start, ClosePointCount, out FoundIndex);
                List<Point3D> NewCloud = new List<Point3D>();
                //while (NearestCloud.Count == 0)
                //{
                //    SetRadius += 0.5;
                //    NearestCloud = m_Func.GetNearestPointCloud(temp, P_Start, SetRadius, out FoundIndex);
                //}
                for (int i = 0; i < Points.Count; i++)
                {
                    if (!FoundIndex.Contains(i)) NewCloud.Add(Points[i]);
                }
                double MaxDis = double.MinValue;
                Point3D MaxP = new Point3D();

                for (int i = 0; i < NearestCloud.Points.Count; i++)
                {
                    double CalDis = Math.Round(Point3D.Distance(P_Start, NearestCloud.Points[i]), 2);
                    if (CalDis > MaxDis)
                    {

                        MaxDis = CalDis;
                        MaxP = NearestCloud.Points[i];

                    }
                }

                if (SmoothRad > 0)
                {
                    if (Point3D.Distance(MaxP, StartP) < 0.1)
                    {
                        P_Start = MaxP;
                    }
                    else
                    {
                        PointCloud MaxPNear = m_Func.GetNearestPointCloud(temp2, MaxP, SmoothRad, out FoundIndex);
                        Point3D AvgMapP = MaxPNear.Average;
                        P_Start = AvgMapP;
                    }
                }//(SmoothRad > 0)
                else
                {
                    P_Start = MaxP;
                }//(SmoothRad > 0)

                int Count_1 = output.Count - 1;
                int Count_2 = output.Count - 2;
                if (output.Count > 1)
                {
                    Point3D p_1 = output.Points[Count_1];
                    Point3D p_2 = output.Points[Count_2];

                    Vector3D CalV1 = new Vector3D(p_1, MaxP);
                    Vector3D CalRefV = new Vector3D(p_1, P_End);

                    if (CalRefV.L < 5.0)
                    {
                        double DotValue = Vector3D.Dot(CalRefV, CalV1);
                        if (DotValue < 0) break;
                    }
                }

                output.Add(P_Start);
                if (NewCloud.Count == 0) break;

                Points.Clear();
                Points = NewCloud;
                temp = GetIndexKDtree();
            }
            if (IsClosed) output.Add(StartP);

            temp = output.GetIndexKDtree();

            //Polyline FinalOutput = new Polyline();
            //for (int i = 0; i < output.Count; i++)
            //{
            //    PointCloud N = m_Func.GetNearestPointCloud(temp, output.Points[i], 2.0 * Radius);

            //    if (N.Count > 1)
            //    {
            //        FinalOutput.Add(output.Points[i]);
            //    }
            //}
            //FinalOutput.Cal();
            return output;
        }
        public Polyline SortCloudByKDTree(double Radius, Point3D P_Start, Point3D P_End)
        {
            Polyline output = new Polyline();
            Points.Add(P_Start);
            Points.Add(P_End);
            KDTree<int> temp = GetIndexKDtree();
            output.Add(P_Start);

            while (true)
            {
                double SetRadius = Radius;
                List<int> FoundIndex = new List<int>();
                PointCloud NearestCloud = m_Func.GetNearestPointCloud(temp, P_Start, SetRadius, out FoundIndex);
                List<Point3D> NewCloud = new List<Point3D>();
                while (NearestCloud.Count == 0)
                {
                    SetRadius += 0.5;
                    NearestCloud = m_Func.GetNearestPointCloud(temp, P_Start, SetRadius, out FoundIndex);
                }
                for (int i = 0; i < Points.Count; i++)
                {
                    if (!FoundIndex.Contains(i)) NewCloud.Add(Points[i]);
                }
                double MaxDis = double.MinValue;
                Point3D MaxP = new Point3D();
                bool IsContainEndP = false;
                for (int i = 0; i < NearestCloud.Points.Count; i++)
                {
                    double CalDis = Math.Round(Point3D.Distance(P_Start, NearestCloud.Points[i]), 2);

                    double CalD_EndP = Math.Round(Point3D.Distance(P_End, NearestCloud.Points[i]), 2);
                    if (CalD_EndP < 0.1) IsContainEndP = true;

                    if (CalDis > MaxDis)
                    {
                        MaxDis = CalDis;
                        MaxP = NearestCloud.Points[i];
                    }
                }
                P_Start = MaxP;
                output.Add(P_Start);

                double tempD = Math.Round(Point3D.Distance(P_End, P_Start), 2);
                if (IsContainEndP)
                {
                    if (tempD > 0.5)
                    {
                        output.Add(P_End);
                    }
                    break;
                }

                if (NewCloud.Count == 0) break;
                Points.Clear();
                Points = NewCloud;
                temp = GetIndexKDtree();
            }
            temp = output.GetIndexKDtree();

            Polyline FinalOutput = new Polyline();
            for (int i = 0; i < output.Count; i++)
            {
                PointCloud N = m_Func.GetNearestPointCloud(temp, output.Points[i], 2.0 * Radius);

                if (N.Count > 1)
                {
                    FinalOutput.Add(output.Points[i]);
                }
            }
            FinalOutput.CalculatePercent();
            return FinalOutput;
        }
        //public Polyline SortCloudByKDTree(double Radius, Point3D P_Start, Point3D P_End,double SmoothRad)
        //{
        //    Polyline output = new Polyline();
        //    Points.Add(P_Start);
        //    Points.Add(P_End);
        //    KDTree<int> temp = BuildIndexKDtree();
        //    KDTree<int> temp2 = BuildIndexKDtree();
        //    output.Add(P_Start);
        //    while (true)
        //    {
        //        double SetRadius = Radius;
        //        List<int> FoundIndex = new List<int>();
        //        PointCloud NearestCloud = m_Func.GetNearestPointCloud(temp, P_Start, SetRadius, out FoundIndex);
        //        List<Point3D> NewCloud = new List<Point3D>();
        //        while (NearestCloud.Count == 0)
        //        {
        //            SetRadius += 0.5;
        //            NearestCloud = m_Func.GetNearestPointCloud(temp, P_Start, SetRadius, out FoundIndex);
        //        }
        //        for (int i = 0; i < Points.Count; i++)
        //        {
        //            if (!FoundIndex.Contains(i)) NewCloud.Add(Points[i].DeepClone());
        //        }
        //        double MaxDis = double.MinValue;
        //        Point3D MaxP = new Point3D();

        //        for (int i = 0; i < NearestCloud.Points.Count; i++)
        //        {
        //            double CalDis = Math.Round(Point3D.Distance(P_Start, NearestCloud.Points[i]), 2);
        //            if (CalDis > MaxDis)
        //            {
        //                MaxDis = CalDis;
        //                MaxP = NearestCloud.Points[i];
        //            }
        //        }
        //        if(SmoothRad > 0)
        //        {
        //            if (Point3D.Distance(MaxP, P_End) < 0.1)
        //            {
        //                P_Start = MaxP;
        //            }
        //            else
        //            {
        //                PointCloud MaxPNear = m_Func.GetNearestPointCloud(temp2, MaxP, SmoothRad, out FoundIndex);
        //                Point3D AvgMapP = MaxPNear.Average;
        //                P_Start = AvgMapP;
        //            }
        //        }
        //        else
        //        {
        //            P_Start = MaxP;
        //        }
        //        output.Add(P_Start);
        //        if (NewCloud.Count == 0) break;
        //        double tempD = Point3D.Distance(P_Start, P_End);
        //        if (tempD < 0.1) break;
        //        Points.Clear();
        //        Points = NewCloud;
        //        temp = BuildIndexKDtree();
        //    }
        //    output.Cal();
        //    return output;
        //}

        public void GetNearPlanePoints(RsLib.PointCloud.RsPlane plane, double Dis)
        {
            PointCloud cloud = new PointCloud();

#if !Parallel
            for (int i = 0; i < Points.Count; i++)
            {
                Point3D point = Points[i];
                double CalDis = Math.Abs(plane.A * point.X + plane.B * point.Y + plane.C * point.Z + plane.D) / (Math.Sqrt(plane.A * plane.A + plane.B * plane.B + plane.C * plane.C));

                if (CalDis < Dis)
                {
                    cloud.Add(point);

                }
            }
#else
            ParallelLoopResult PResult1 = Parallel.For(0, Points.Count, (int i, ParallelLoopState PLS) =>
            {
                Point3D point = Points[i].DeepClone();
                double CalDis = Math.Abs(plane.A * point.X + plane.B * point.Y + plane.C * point.Z + plane.D) / (Math.Sqrt(plane.A * plane.A + plane.B * plane.B + plane.C * plane.C));
                if (CalDis < Dis)
                {
                    lock (cloud)
                    {
                        cloud.Add(point);
                    }
                }
            });
#endif
            Points.Clear();
            Points = cloud.Points;
        }
        public PointCloud ReturnNearPlanePoints(RsLib.PointCloud.RsPlane plane, double Dis)
        {
            PointCloud cloud = new PointCloud();

            for (int i = 0; i < Points.Count; i++)
            {
                Point3D point = Points[i];
                double CalDis = Math.Abs(plane.A * point.X + plane.B * point.Y + plane.C * point.Z + plane.D) / (Math.Sqrt(plane.A * plane.A + plane.B * plane.B + plane.C * plane.C));

                if (CalDis < Dis)
                {
                    cloud.Add(point);

                }
            }
            return cloud;


        }
        public void Project2Plane(RsLib.PointCloud.RsPlane plane)
        {
            PointCloud cloud = new PointCloud();

            for (int i = 0; i < Points.Count; i++)
            {
                Point3D point = Points[i];
                double t = (plane.A * point.X + plane.B * point.Y + plane.C * point.Z + plane.D) / (plane.A * plane.A + plane.B * plane.B + plane.C * plane.C);
                Point3D ProjP = new Point3D();

                ProjP.X = Math.Round(point.X - plane.A * t, 2);
                ProjP.Y = Math.Round(point.Y - plane.B * t, 2);
                ProjP.Z = Math.Round(point.Z - plane.C * t, 2);

                cloud.Add(ProjP);

            }

            Points.Clear();
            Points = cloud.Points;

        }
        public PointCloud GetProject2Plane(RsLib.PointCloud.RsPlane plane)
        {
            PointCloud cloud = new PointCloud();

            for (int i = 0; i < Points.Count; i++)
            {
                Point3D point = Points[i];
                double t = (plane.A * point.X + plane.B * point.Y + plane.C * point.Z + plane.D) / (plane.A * plane.A + plane.B * plane.B + plane.C * plane.C);
                Point3D ProjP = new Point3D();

                ProjP.X = Math.Round(point.X - plane.A * t, 2);
                ProjP.Y = Math.Round(point.Y - plane.B * t, 2);
                ProjP.Z = Math.Round(point.Z - plane.C * t, 2);

                cloud.Add(ProjP);

            }

            return cloud;
        }

        public PointCloud GetInsideTiltBox(TiltBox Boundary)
        {
            PointCloud cloud = new PointCloud();
            RsPlane plane = Boundary.BasePlane;
#if !Parallel
            Point3D point, ProjP;

            Vector3D V0, V1, V2, V3, Vc0, Vc1, Vc2, Vc3;
            double Dot1, Dot2, Dot3, Dot4, Dot5, Dot6;

            if (IsKdTreeBuilded == false) BuildIndexKDtree();
            double KdTreeR = Boundary.GetFarestCornerFromCenter();

            PointCloud Collection = m_Func.GetNearestPointCloud(kdTree, Boundary.BaseCenter, KdTreeR);


            for (int i = 0; i < Collection.Points.Count; i++)
            {
                point = Collection.Points[i];
                ProjP = plane.ProjectPOnPlane(point);

                V0 = new Vector3D(Boundary.BaseP0, ProjP);
                V1 = new Vector3D(Boundary.BaseP1, ProjP);
                V2 = new Vector3D(Boundary.BaseP2, ProjP);
                V3 = new Vector3D(Boundary.BaseP3, ProjP);

                Vc0 = Vector3D.Cross(V0, Boundary.V01);
                Vc1 = Vector3D.Cross(V1, Boundary.V13);
                Vc2 = Vector3D.Cross(V2, Boundary.V20);
                Vc3 = Vector3D.Cross(V3, Boundary.V32);

                Dot1 = Vector3D.Dot(Vc0, Vc1);
                Dot2 = Vector3D.Dot(Vc0, Vc2);
                Dot3 = Vector3D.Dot(Vc0, Vc3);
                Dot4 = Vector3D.Dot(Vc1, Vc2);
                Dot5 = Vector3D.Dot(Vc1, Vc3);
                Dot6 = Vector3D.Dot(Vc2, Vc3);



                if ((Dot1 > 0 && Dot2 > 0 && Dot3 > 0 && Dot4 > 0 && Dot5 > 0 && Dot6 > 0) || (Dot1 < 0 && Dot2 < 0 && Dot3 < 0 && Dot4 < 0 && Dot5 < 0 && Dot6 < 0))
                {
                    Vector3D RefV = new Vector3D(ProjP, point);
                    double Dis = RefV.L;

                    double DotDir = Vector3D.Dot(plane.Normal, RefV);

                    if (DotDir > 0)
                    {
                        if (Dis < Math.Abs(Boundary.MaxDis)) cloud.Add(point, true);
                    }
                    else
                    {
                        if (Dis < Math.Abs(Boundary.MinDis)) cloud.Add(point, true);
                    }

                }
            }
#else
            ParallelLoopResult PResult1 = Parallel.For(0, Points.Count, (int i, ParallelLoopState PLS) =>
            {

                    lock (cloud)
                    {
                        Point3D point = Points[i];
                        Point3D ProjP = plane.ProjectPOnPlane(point);

                        Vector3D V0 = new Vector3D(Boundary.BaseP0, ProjP);
                        Vector3D V1 = new Vector3D(Boundary.BaseP1, ProjP);
                        Vector3D V2 = new Vector3D(Boundary.BaseP2, ProjP);
                        Vector3D V3 = new Vector3D(Boundary.BaseP3, ProjP);

                        Vector3D Vc0 = Vector3D.Cross(V0, Boundary.V01);
                        Vector3D Vc1 = Vector3D.Cross(V1, Boundary.V13);
                        Vector3D Vc2 = Vector3D.Cross(V2, Boundary.V20);
                        Vector3D Vc3 = Vector3D.Cross(V3, Boundary.V32);

                        double Dot1 = Vector3D.Dot(Vc0, Vc1);
                        double Dot2 = Vector3D.Dot(Vc0, Vc2);
                        double Dot3 = Vector3D.Dot(Vc0, Vc3);
                        double Dot4 = Vector3D.Dot(Vc1, Vc2);
                        double Dot5 = Vector3D.Dot(Vc1, Vc3);
                        double Dot6 = Vector3D.Dot(Vc2, Vc3);



                        if ((Dot1 > 0 && Dot2 > 0 && Dot3 > 0 && Dot4 > 0 && Dot5 > 0 && Dot6 > 0) || (Dot1 < 0 && Dot2 < 0 && Dot3 < 0 && Dot4 < 0 && Dot5 < 0 && Dot6 < 0))
                        {
                            Vector3D RefV = new Vector3D(ProjP, point);
                            double Dis = RefV.L;

                            double DotDir = Vector3D.Dot(plane.Normal, RefV);

                            if (DotDir > 0)
                            {
                                if (Dis < Math.Abs(Boundary.MaxDis)) cloud.Add(point);
                            }
                            else
                            {
                                if (Dis < Math.Abs(Boundary.MinDis)) cloud.Add(point);
                            }

                        }
                    }
                

            });


#endif
            return cloud;
        }

        public LayerPointCloud GetInsideTiltBox(List<TiltBox> Boundary)
        {
            LayerPointCloud clouds = new LayerPointCloud(Boundary.Count);
            //ParallelLoopResult PResult1 = Parallel.For(0, Boundary.Count, (int j, ParallelLoopState PLS) =>
            //{
            for (int i = 0; i < Points.Count; i++)
            {
                //ParallelLoopResult PResult1 = Parallel.For(0, Boundary.Count, (int j, ParallelLoopState PLS) =>
                //{

                for (int j = 0; j < Boundary.Count; j++)
                {
                    if (Boundary[j].IsInsideTiltBox(Points[i]))
                    {
                        clouds.Layers[j].Add(new Point3D(Points[i]));
                        break;
                    }
                }
                //});
            }
            //});
            return clouds;
        }

        public Point3D GetFarestPoint(Point3D refPoint)
        {
            Point3D Output = new Point3D();
            double MaxDis = double.MinValue;
            for (int i = 0; i < Points.Count; i++)
            {
                Vector3D CalVec = new Vector3D(refPoint, Points[i]);
                if (CalVec.L > MaxDis)
                {
                    MaxDis = CalVec.L;
                    Output = Points[i];
                }
            }
            return Output;
        }

        public void ReduceOutsideXY(double MaxX, double MaxY, double MinX, double MinY)
        {
            PointCloud cloud = new PointCloud();

            for (int i = 0; i < Points.Count; i++)
            {
                Point3D point = Points[i];

                if (point.X >= MinX && point.X <= MaxX && point.Y >= MinY && point.Y <= MaxY)
                {

                    cloud.Add(point);

                }
            }

            Points.Clear();
            Points = cloud.Points;
        }
        public void ReducePointEvery(int PointsCount)
        {
            PointCloud cloud = new PointCloud();
            for (int i = 0; i < Points.Count; i++)
            {
                Point3D point = Points[i];
                lock (cloud)
                {
                    if (i == 0 || i == Points.Count - 1)
                    {
                        cloud.Add(point);
                    }
                    else
                    {
                        if (i % PointsCount == 0)
                        {
                            cloud.Add(point);
                        }
                    }
                }
            }
            Points.Clear();
            Points = cloud.Points;
        }
        //public void ReducePointEveryLength(double PointsLength)
        //{
        //    PointCloud cloud = new PointCloud();
        //    for (int i = 0; i < Points.Count; i++)
        //    {
        //        Point3D point = Points[i].DeepClone();
        //        if (i == 0 || i == Points.Count - 1)
        //        {
        //            cloud.Add(point);
        //        }
        //        else
        //        {
        //            Point3D LastP = cloud.Points[cloud.Count - 1].DeepClone();
        //            Point3D point1 = Points[i].DeepClone();

        //            Vector3D CalV = new Vector3D(LastP, point1);
        //            if (CalV.L >= PointsLength)
        //            {
        //                cloud.Add(point1);
        //                continue;
        //            }
        //            while (CalV.L < PointsLength)
        //            {
        //                i += 1;
        //                point1 = Points[i].DeepClone();
        //                if (i == Points.Count - 1)
        //                {
        //                    cloud.Add(point1);
        //                    break;
        //                }
        //                CalV = new Vector3D(LastP, point1);
        //                if (CalV.L >= PointsLength)
        //                {
        //                    cloud.Add(point1);
        //                    break;
        //                }
        //            }
        //        }
        //    }
        //    Points.Clear();
        //    Points = cloud.Points;
        //}

        public void ReducePointByXYDis(double MaxDis, Point3D refPoint)
        {
            PointCloud cloud = new PointCloud();
            for (int i = 0; i < Points.Count; i++)
            {
                Vector3D refV = new Vector3D(refPoint, Points[i]);
                refV.Z = 0;

                if (refV.L <= MaxDis)
                {

                    cloud.Add(Points[i]);

                }
            }
            Points.Clear();
            Points = cloud.Points;
        }
        public void ReducePointByDir(Vector3D refVec, Point3D refPoint)
        {
            PointCloud cloud = new PointCloud();
            for (int i = 0; i < Points.Count; i++)
            {
                Vector3D refV = new Vector3D(refPoint, Points[i]);
                refV.Z = 0;
                double DotValue = Vector3D.Dot(refV, refVec);

                if (DotValue >= 0)
                {

                    cloud.Add(Points[i]);

                }
            }
            Points.Clear();
            Points = cloud.Points;
        }

        public Point3D GetXPercentPoint(double PercentValue, Point3D BasePoint, Point3D FarestPoint)
        {
            List<Point3D> temp = Points.DeepClone();
            List<Point3D> Sorted = new List<Point3D>();
            Vector3D tempV = new Vector3D(BasePoint, FarestPoint);
            if (FarestPoint.X > BasePoint.X)
                Sorted = temp.OrderBy(t => t.X).ToList();
            else
                Sorted = temp.OrderByDescending(t => t.X).ToList();

            double RatioLength = tempV.L * PercentValue;

            Point3D CalP = new Point3D();

            CalP.X = BasePoint.X + tempV.GetUnitVector().X * RatioLength;
            CalP.Y = BasePoint.Y + tempV.GetUnitVector().Y * RatioLength;

            double LaseValue = 0;
            for (int i = 0; i < Sorted.Count; i++)
            {
                double VecX = Sorted[i].X - BasePoint.X;
                double VecY = Sorted[i].Y - BasePoint.Y;

                double VecL = Math.Sqrt(Math.Pow(VecX, 2) + Math.Pow(VecY, 2));

                if (LaseValue < RatioLength && VecL >= RatioLength)
                {
                    CalP.Z = Sorted[i].Z;
                    break;
                }

                LaseValue = VecL;

            }
            return CalP;
        }
        public void RemoveOutsideBox(Box bound)
        {
            PointCloud cloud = new PointCloud();
            for (int i = 0; i < Points.Count; i++)
            {
                if (Points[i].Z >= bound.MinP.Z && Points[i].Z <= bound.MaxP.Z &&
                Points[i].Y >= bound.MinP.Y && Points[i].Y <= bound.MaxP.Y &&
                Points[i].X >= bound.MinP.X && Points[i].X <= bound.MaxP.X)
                {

                    cloud.Add(Points[i]);

                }
            }
            Points.Clear();
            Points = cloud.Points;
        }
        public PointCloud GetInsideBox(Box bound)
        {
            PointCloud cloud = new PointCloud();

            for (int i = 0; i < Points.Count; i++)
            {
                if (Points[i].Z >= bound.MinP.Z && Points[i].Z <= bound.MaxP.Z &&
                Points[i].Y >= bound.MinP.Y && Points[i].Y <= bound.MaxP.Y &&
                Points[i].X >= bound.MinP.X && Points[i].X <= bound.MaxP.X)
                {

                    cloud.Add(Points[i]);

                }
            }
            return cloud;
        }
        public void ReduceBelowMedian(double BelowDis = 1)
        {
            PointCloud cloud = new PointCloud();
            Point3D MedianPoint = GetMedian();

            for (int i = 0; i < Points.Count; i++)
            {
                if (Points[i].Z >= MedianPoint.Z - BelowDis)
                {

                    cloud.Add(Points[i]);

                }
            }
            Points.Clear();
            Points = cloud.Points;
        }
        #region Reduce Point
        public void ReduceAboveZ(double ZValue)
        {
            PointCloud cloud = new PointCloud();
            for (int i = 0; i < Points.Count; i++)
            {
                if (Points[i].Z <= ZValue)
                {

                    cloud.Add(Points[i]);

                }
            }
            Points.Clear();
            Points = cloud.Points;
        }
        public void ReduceAboveY(double YValue)
        {
            PointCloud cloud = new PointCloud();
            for (int i = 0; i < Points.Count; i++)
            {
                if (Points[i].Y <= YValue)
                {

                    cloud.Add(Points[i]);

                }
            }
            Points.Clear();
            Points = cloud.Points;
        }
        public void ReduceAboveX(double XValue)
        {
            PointCloud cloud = new PointCloud();
            for (int i = 0; i < Points.Count; i++)
            {
                if (Points[i].X <= XValue)
                {

                    cloud.Add(Points[i]);

                }
            }
            Points.Clear();
            Points = cloud.Points;
        }
        public void ReduceBelowX(double XValue)
        {
            PointCloud cloud = new PointCloud();
            for (int i = 0; i < Points.Count; i++)
            {
                if (Points[i].X >= XValue)
                {

                    cloud.Add(Points[i]);

                }
            }
            Points.Clear();
            Points = cloud.Points;
        }
        public void ReduceBelowY(double YValue)
        {
            PointCloud cloud = new PointCloud();
            for (int i = 0; i < Points.Count; i++)
            {
                if (Points[i].Y >= YValue)
                {

                    cloud.Add(Points[i]);

                }
            }
            Points.Clear();
            Points = cloud.Points;
        }
        public void ReduceBelowZ(double ZValue)
        {
            //PointCloud cloud = new PointCloud();
            Dictionary<int, Point3D> Orig = new Dictionary<int, Point3D>();
            for (int i = 0; i < Points.Count; i++)
            {
                if (Points[i].Z >= ZValue)
                {

                    Orig.Add(i, Points[i]);

                }
            }
            Dictionary<int, Point3D> Sorted = Orig.OrderBy(t => t.Key).ToDictionary(t => t.Key, tt => tt.Value);
            
            Points.Clear();
            Points = Sorted.Values.ToList();
        }

        public void ReduceInsideX(double Min, double Max)
        {
            double min = Math.Min(Min, Max);
            double max = Math.Max(Min, Max);
            PointCloud cloud = new PointCloud();
            for (int i = 0; i < Points.Count; i++)
            {
                double d = Points[i].X;
                if (d <= min && d >= max)
                {
                    cloud.Add(Points[i]);
                }
            }
            Points.Clear();
            Points = cloud.Points;
        }
        public void ReduceInsideY(double Min, double Max)
        {
            double min = Math.Min(Min, Max);
            double max = Math.Max(Min, Max);
            PointCloud cloud = new PointCloud();
            for (int i = 0; i < Points.Count; i++)
            {
                double d = Points[i].Y;
                if (d <= min && d >= max)
                {
                    cloud.Add(Points[i]);
                }
            }
            Points.Clear();
            Points = cloud.Points;
        }
        public void ReduceInsideZ(double Min, double Max)
        {
            double min = Math.Min(Min, Max);
            double max = Math.Max(Min, Max);

            PointCloud cloud = new PointCloud();
            for (int i = 0; i < Points.Count; i++)
            {
                double d = Points[i].Z;
                if (d <= min && d >= max)
                {
                    cloud.Add(Points[i]);
                }
            }
            Points.Clear();
            Points = cloud.Points;
        }

        public void ReduceOutsideX(double Min, double Max)
        {
            double min = Math.Min(Min, Max);
            double max = Math.Max(Min, Max);
            PointCloud cloud = new PointCloud();
            for (int i = 0; i < Points.Count; i++)
            {
                double d = Points[i].X;
                if (d >= min && d <= max)
                {
                    cloud.Add(Points[i]);
                }
            }
            Points.Clear();
            Points = cloud.Points;
        }
        public void ReduceOutsideY(double Min, double Max)
        {
            double min = Math.Min(Min, Max);
            double max = Math.Max(Min, Max);

            PointCloud cloud = new PointCloud();
            for (int i = 0; i < Points.Count; i++)
            {
                double d = Points[i].Y;
                if (d >= min && d <= max)
                {
                    cloud.Add(Points[i]);
                }
            }
            Points.Clear();
            Points = cloud.Points;
        }
        public void ReduceOutsideZ(double Min, double Max)
        {
            double min = Math.Min(Min, Max);
            double max = Math.Max(Min, Max);

            PointCloud cloud = new PointCloud();
            for (int i = 0; i < Points.Count; i++)
            {
                double d = Points[i].Z;
                if (d >= min && d <= max)
                {
                    cloud.Add(Points[i]);
                }
            }
            Points.Clear();
            Points = cloud.Points;
        }

        #endregion
        #region Get Point
        public PointCloud GetPointBelowZ(double ZValue)
        {
            PointCloud cloud = new PointCloud();

            for (int i = 0; i < Points.Count; i++)
            {
                if (Points[i].Z <= ZValue)
                {

                    cloud.Add(Points[i]);

                }
            }

            return cloud;
        }
        public PointCloud GetPointBelowY(double YValue)
        {
            PointCloud cloud = new PointCloud();

            for (int i = 0; i < Points.Count; i++)
            {
                if (Points[i].Y <= YValue)
                {

                    cloud.Add(Points[i]);

                }
            }
            return cloud;
        }
        public PointCloud GetPointBelowX(double XValue)
        {
            PointCloud cloud = new PointCloud();

            for (int i = 0; i < Points.Count; i++)
            {
                if (Points[i].X <= XValue)
                {

                    cloud.Add(Points[i]);

                }
            }
            return cloud;
        }
        public PointCloud GetPointAboveX(double XValue)
        {
            PointCloud cloud = new PointCloud();

            for (int i = 0; i < Points.Count; i++)
            {
                if (Points[i].X >= XValue)
                {

                    cloud.Add(Points[i]);

                }
            }
            return cloud;
        }
        public PointCloud GetPointAboveY(double YValue)
        {
            PointCloud cloud = new PointCloud();

            for (int i = 0; i < Points.Count; i++)
            {
                if (Points[i].Y >= YValue)
                {

                    cloud.Add(Points[i]);

                }
            }
            return cloud;
        }
        public PointCloud GetPointAboveZ(double ZValue)
        {
            PointCloud cloud = new PointCloud();

            for (int i = 0; i < Points.Count; i++)
            {
                if (Points[i].Z >= ZValue)
                {

                    cloud.Add(Points[i]);

                }
            }
            return cloud;
        }

        #endregion
        public void ReducePointOutside2Point(Point3D P1, Point3D P2, bool IsConsiderZ)
        {
            PointCloud cloud = new PointCloud();
            double Diff, Diff1, Diff2 = 0;
            if (IsConsiderZ)
            {
                Diff = Point3D.Distance(P1, P2);
            }
            else
            {
                Diff = Point3D.Distance(new Point3D(P1.X, P1.Y, 0), new Point3D(P2.X, P2.Y, 0));
            }
            for (int i = 0; i < Points.Count; i++)
            {
                if (IsConsiderZ)
                {
                    Diff1 = Point3D.Distance(P1, Points[i]);
                    Diff2 = Point3D.Distance(Points[i], P2);

                }
                else
                {
                    Diff1 = Point3D.Distance(new Point3D(P1.X, P1.Y, 0), new Point3D(Points[i].X, Points[i].Y, 0));
                    Diff2 = Point3D.Distance(new Point3D(Points[i].X, Points[i].Y, 0), new Point3D(P2.X, P2.Y, 0));

                }
                if (Diff1 <= Diff && Diff2 <= Diff)
                {

                    cloud.Add(Points[i]);

                }
            }


            Points.Clear();
            Points = cloud.Points;
        }

        public void Clear()
        {
            Points.Clear();
            kdTree.Clear();
        }
        public void Add(LayerPointCloud Clouds)
        {
            for (int i = 0; i < Clouds.Layers.Count; i++)
            {
                this.Add(Clouds.Layers[i]);
            }
        }
        public void Add(double x,double y,double z, bool IsAddKdTree = false)
        {
            Points.Add(new Point3D(x,y,z));
            if (IsAddKdTree) kdTree.Add(new double[] { x, y, z}, Points.Count - 1);
        }
        public void Add(Point3D point, bool IsAddKdTree = false)
        {
            Points.Add(point);
            if (IsAddKdTree) kdTree.Add(new double[] { point.X, point.Y, point.Z }, Points.Count - 1);
        }
        public void StartAdd()
        {
            DuplicatePoints.Clear();
            IsAddNoDuplicate = true;
        }

        public void AddNotDuplicate(Point3D Input)
        {
            string PointName = string.Format("{0},{1},{2}", Input.X.ToString("F2"), Input.Y.ToString("F2"), Input.Z.ToString("F2"));

            if (!IsAddNoDuplicate)
                return;
            if (!DuplicatePoints.ContainsKey(PointName)) DuplicatePoints.Add(PointName, Input);
        }
        public void AddNotDuplicate(Point3D Input, double MinDis)
        {
            string PointName = string.Format("{0},{1},{2}", Input.X.ToString("F2"), Input.Y.ToString("F2"), Input.Z.ToString("F2"));

            if (!IsAddNoDuplicate)
                return;

            if (DuplicatePoints.Count == 0) DuplicatePoints.Add(PointName, Input);
            if (!DuplicatePoints.ContainsKey(PointName))
            {
                List<string> Keys = DuplicatePoints.Keys.ToList();
                int LastIndex = Keys.Count - 1;
                string LastKey = Keys[LastIndex];
                Point3D LastPoint = DuplicatePoints[LastKey];
                Vector3D CalV = new Vector3D(LastPoint, Input);
                if (CalV.L > MinDis) DuplicatePoints.Add(PointName, Input);
            }
        }
        public void AddNotDuplicate(PointCloud Input)
        {
            for (int i = 0; i < Input.Points.Count; i++)
            {
                string PointName = string.Format("{0},{1},{2}", Input.Points[i].X.ToString("F2"), Input.Points[i].Y.ToString("F2"), Input.Points[i].Z.ToString("F2"));

                if (!IsAddNoDuplicate) return;
                if (!DuplicatePoints.ContainsKey(PointName)) DuplicatePoints.Add(PointName, Input.Points[i]);
            }
        }
        public void EndAdd()
        {
            Points = DuplicatePoints.Values.ToList();
            DuplicatePoints.Clear();
            IsAddNoDuplicate = false;
        }
        public void Add(double[] x, double[] y, double[] z)
        {
            if (x.Length != y.Length && x.Length != z.Length) return;

            for (int i = 0; i < x.Length; i++)
            {
                Points.Add(new Point3D(x[i], y[i], z[i]));
            }
        }
        public void Add(PointCloud cloud)
        {
            Points.AddRange(cloud.Points);
        }
        public void Add(PointCloud cloud, System.Drawing.Color DisplayColor)
        {
            for (int i = 0; i < cloud.Points.Count; i++)
            {
                cloud.Points[i].Color = DisplayColor;
                Points.Add(cloud.Points[i]);
            }
        }
        public void Add(Polyline cloud, bool IsAddNoDuplicate = false)
        {
            if (!IsAddNoDuplicate) Points.AddRange(cloud.Points);
            else
            {
                for (int i = 0; i < cloud.Count; i++)
                {
                    AddNotDuplicate(cloud.Points[i]);
                }
            }
        }

        private Point3D GetAverage()
        {
            double x = 0;
            double y = 0;
            double z = 0;

            for (int i = 0; i < Points.Count; i++)
            {
                x += Points[i].X;
                y += Points[i].Y;
                z += Points[i].Z;

            }

            return new Point3D(x / Points.Count, y / Points.Count, z / Points.Count);
        }

        public Point3D GetPoint(int Index)
        {
            if (Index > Points.Count - 1) return null;

            return Points[Index];


        }
        public Accord.Collections.KDTree<Point3D> GetKDtree()
        {
            Accord.Collections.KDTree<Point3D> kD = new Accord.Collections.KDTree<Point3D>(3);
            for (int i = 0; i < Points.Count; i++)
            {
                kD.Add(new double[] { Points[i].X, Points[i].Y, Points[i].Z }, Points[i]);
            }
            return kD;
        }

        public void BuildKDtree()
        {
            kdTree = new KDTree<int>(3);
            for (int i = 0; i < Points.Count; i++)
            {
                kdTree.Add(new double[] { Points[i].X, Points[i].Y, Points[i].Z }, i);
            }
        }
        public Point3D FindClosePoint(PointCloud Target, out int TargetIndex)
        {
            if(IsKdTreeBuilded == false) BuildIndexKDtree();
            double minDis = double.MaxValue;
            Point3D MinPoint = new Point3D();
            TargetIndex = -1;
            for (int i = 0; i < Target.Count; i++)
            {
                double tmpMinDis = double.MaxValue;
                Point3D tmp = m_Func.GetNearestPoint(kdTree, Target.Points[i], out tmpMinDis);
                if (tmpMinDis < minDis)
                {
                    TargetIndex = i;
                    MinPoint = new Point3D(tmp);
                    minDis = tmpMinDis;
                }
            }
            return MinPoint;
        }
        public int FindClosePointIndex(PointCloud Target)
        {
            if(IsKdTreeBuilded == false) BuildIndexKDtree();
            double minDis = double.MaxValue;
            int MinPointIndex = -1;
            for (int i = 0; i < Target.Count; i++)
            {
                double tmpMinDis = double.MaxValue;
                int tmp = m_Func.GetNearestPointIndex(kdTree, Target.Points[i], out tmpMinDis);
                if (tmpMinDis < minDis)
                {
                    MinPointIndex = tmp;
                    minDis = tmpMinDis;
                }
            }
            return MinPointIndex;
        }
        public int FindClosePointIndex(PointCloud Target,double radius)
        {
            if (IsKdTreeBuilded == false) BuildIndexKDtree();
            double minDis = double.MaxValue;
            int MinPointIndex = -1;
            for (int i = 0; i < Target.Count; i++)
            {
                double tmpMinDis = double.MaxValue;
                int tmp = m_Func.GetNearestPointIndex(kdTree, Target.Points[i], out tmpMinDis);
                if (tmpMinDis < minDis)
                {
                    MinPointIndex = tmp;
                    minDis = tmpMinDis;
                }
            }
            return MinPointIndex;
        }
        public int FindClosePointIndex(Point3D Target, double radius)
        {
            if (IsKdTreeBuilded == false) BuildIndexKDtree();
            return m_Func.GetInRadiusPoint(kdTree, Target,radius);
        }

        public Accord.Collections.KDTree<int> GetIndexKDtree(bool MakeZequal0 = false)
        {
            Accord.Collections.KDTree<int> kD = new Accord.Collections.KDTree<int>(3);
            //ParallelLoopResult PResult1 = Parallel.For(0, Points.Count, (int i, ParallelLoopState PLS) =>
            //{
            //    if (Points.Count > 0)
            //    {
            //        lock (kD)
            //        {
            //            kD.Add(new double[] { Points[i].X, Points[i].Y, Points[i].Z }, i);
            //        }
            //    }

            //});
            if (Points.Count > 0)
            {
                for (int i = 0; i < Points.Count; i++)
                {
                    if (MakeZequal0) kD.Add(new double[] { Points[i].X, Points[i].Y, 0.0 }, i);
                    else kD.Add(new double[] { Points[i].X, Points[i].Y, Points[i].Z }, i);
                }
            }
            return kD;
        }
        public void BuildIndexKDtree(bool MakeZequal0 = false)
        {
            kdTree = new KDTree<int>(3);
            //ParallelLoopResult PResult1 = Parallel.For(0, Points.Count, (int i, ParallelLoopState PLS) =>
            //{
            //    if (Points.Count > 0)
            //    {
            //        lock (kD)
            //        {
            //            kD.Add(new double[] { Points[i].X, Points[i].Y, Points[i].Z }, i);
            //        }
            //    }

            //});
            if (Points.Count > 0)
            {
                for (int i = 0; i < Points.Count; i++)
                {
                    if (MakeZequal0) kdTree.Add(new double[] { Points[i].X, Points[i].Y, 0.0 }, i);
                    else kdTree.Add(new double[] { Points[i].X, Points[i].Y, Points[i].Z }, i);
                }
            }
        }

        private Point3D GetMedian()
        {


            //List<Point3D> SortedZ = new List<Point3D>();
            //List<Point3D> SortedY = new List<Point3D>();
            //List<Point3D> SortedX = new List<Point3D>();
            //ParallelLoopResult PResult1 = Parallel.For(0, 3, (int i, ParallelLoopState PLS) =>
            //{
            //    if (i == 0)
            //    {
            //        SortedZ = Points.OrderBy(t => t.Z).ToList();
            //    }
            //    else if (i == 1)
            //    {
            //        SortedY = Points.OrderBy(t => t.Y).ToList();
            //    }
            //    else if (i == 2)
            //    {
            //        SortedX = Points.OrderBy(t => t.X).ToList();
            //    }
            //    else
            //    {

            //    }
            //});

            List<Point3D> SortedZ = Points.OrderBy(t => t.Z).ToList();
            List<Point3D> SortedY = Points.OrderBy(t => t.Y).ToList();
            List<Point3D> SortedX = Points.OrderBy(t => t.X).ToList();

            double MedianX = 0;
            double MedianY = 0;
            double MedianZ = 0;

            if (SortedZ.Count % 2 == 0)
            {
                int MedianIndex0 = SortedZ.Count / 2 - 1;
                int MedianIndex1 = SortedZ.Count / 2;
                MedianX = (SortedX[MedianIndex0].X + SortedX[MedianIndex1].X) / 2;
                MedianY = (SortedY[MedianIndex0].Y + SortedY[MedianIndex1].Y) / 2;
                MedianZ = (SortedZ[MedianIndex0].Z + SortedZ[MedianIndex1].Z) / 2;
            }
            else
            {
                int MedianIndex = SortedZ.Count / 2;
                MedianX = SortedX[MedianIndex].X;
                MedianY = SortedY[MedianIndex].Y;
                MedianZ = SortedZ[MedianIndex].Z;
            }



            return new Point3D(MedianX, MedianY, MedianZ);

        }
        public Point3D GetMedianZ()
        {
            List<Point3D> SortedZ = Points.OrderBy(t => t.Z).ToList();

            double MedianX = 0;
            double MedianY = 0;
            double MedianZ = 0;

            if (SortedZ.Count % 2 == 0)
            {
                int MedianIndex0 = SortedZ.Count / 2 - 1;
                int MedianIndex1 = SortedZ.Count / 2;
                MedianX = (SortedZ[MedianIndex0].X + SortedZ[MedianIndex1].X) / 2;
                MedianY = (SortedZ[MedianIndex0].Y + SortedZ[MedianIndex1].Y) / 2;
                MedianZ = (SortedZ[MedianIndex0].Z + SortedZ[MedianIndex1].Z) / 2;
            }
            else
            {
                int MedianIndex = SortedZ.Count / 2;
                MedianX = SortedZ[MedianIndex].X;
                MedianY = SortedZ[MedianIndex].Y;
                MedianZ = SortedZ[MedianIndex].Z;
            }
            return new Point3D(MedianX, MedianY, MedianZ);

        }
        public Point3D GetMedianY()
        {
            List<Point3D> SortedY = Points.OrderBy(t => t.Y).ToList();

            double MedianX = 0;
            double MedianY = 0;
            double MedianZ = 0;

            if (SortedY.Count % 2 == 0)
            {
                int MedianIndex0 = SortedY.Count / 2 - 1;
                int MedianIndex1 = SortedY.Count / 2;
                MedianX = (SortedY[MedianIndex0].X + SortedY[MedianIndex1].X) / 2;
                MedianY = (SortedY[MedianIndex0].Y + SortedY[MedianIndex1].Y) / 2;
                MedianZ = (SortedY[MedianIndex0].Z + SortedY[MedianIndex1].Z) / 2;
            }
            else
            {
                int MedianIndex = SortedY.Count / 2;
                MedianX = SortedY[MedianIndex].X;
                MedianY = SortedY[MedianIndex].Y;
                MedianZ = SortedY[MedianIndex].Z;
            }
            return new Point3D(MedianX, MedianY, MedianZ);

        }
        public Point3D GetMedianX()
        {
            List<Point3D> SortedX = Points.OrderBy(t => t.X).ToList();

            double MedianX = 0;
            double MedianY = 0;
            double MedianZ = 0;

            if (SortedX.Count % 2 == 0)
            {
                int MedianIndex0 = SortedX.Count / 2 - 1;
                int MedianIndex1 = SortedX.Count / 2;
                MedianX = (SortedX[MedianIndex0].X + SortedX[MedianIndex1].X) / 2;
                MedianY = (SortedX[MedianIndex0].Y + SortedX[MedianIndex1].Y) / 2;
                MedianZ = (SortedX[MedianIndex0].Z + SortedX[MedianIndex1].Z) / 2;
            }
            else
            {
                int MedianIndex = SortedX.Count / 2;
                MedianX = SortedX[MedianIndex].X;
                MedianY = SortedX[MedianIndex].Y;
                MedianZ = SortedX[MedianIndex].Z;
            }
            return new Point3D(MedianX, MedianY, MedianZ);

        }

        private Point3D GetOneQuartile()
        {
            List<Point3D> SortedZ = Points.OrderBy(t => t.Z).ToList();
            List<Point3D> SortedY = Points.OrderBy(t => t.Y).ToList();
            List<Point3D> SortedX = Points.OrderBy(t => t.X).ToList();

            List<Point3D> BelowZ = SortedZ.Where(t => t.Z < Median.Z).ToList();
            List<Point3D> BelowY = SortedY.Where(t => t.Y < Median.Y).ToList();
            List<Point3D> BelowX = SortedX.Where(t => t.X < Median.X).ToList();

            BelowZ = BelowZ.OrderBy(t => t.Z).ToList();
            BelowY = BelowY.OrderBy(t => t.Y).ToList();
            BelowX = BelowX.OrderBy(t => t.X).ToList();

            double MedianX = 0;
            double MedianY = 0;
            double MedianZ = 0;

            if (BelowZ.Count % 2 == 0)
            {
                if (BelowZ.Count == 0)
                {
                    MedianZ = SortedZ[0].Z;
                }
                else
                {
                    int MedianIndex0 = BelowZ.Count / 2 - 1;
                    int MedianIndex1 = BelowZ.Count / 2;
                    MedianZ = (BelowZ[MedianIndex0].Z + BelowZ[MedianIndex1].Z) / 2;
                }
            }
            else
            {
                int MedianIndex = BelowZ.Count / 2;
                MedianZ = BelowZ[MedianIndex].Z;
            }

            if (BelowY.Count % 2 == 0)
            {
                if (BelowY.Count == 0)
                {
                    MedianY = SortedY[0].Y;
                }
                else
                {
                    int MedianIndex0 = BelowY.Count / 2 - 1;
                    int MedianIndex1 = BelowY.Count / 2;
                    MedianY = (BelowY[MedianIndex0].Y + BelowY[MedianIndex1].Y) / 2;
                }
            }
            else
            {
                int MedianIndex = BelowY.Count / 2;
                MedianY = BelowY[MedianIndex].Y;
            }

            if (BelowX.Count % 2 == 0)
            {
                if (BelowX.Count == 0)
                {
                    MedianX = SortedX[0].X;
                }
                else
                {
                    int MedianIndex0 = BelowX.Count / 2 - 1;
                    int MedianIndex1 = BelowX.Count / 2;
                    MedianX = (BelowX[MedianIndex0].X + BelowX[MedianIndex1].X) / 2;
                }
            }
            else
            {
                int MedianIndex = BelowX.Count / 2;
                MedianX = BelowX[MedianIndex].X;
            }
            return new Point3D(MedianX, MedianY, MedianZ);

        }
        private Point3D GetThirdQuartile()
        {


            List<Point3D> SortedZ = Points.OrderBy(t => t.Z).ToList();
            List<Point3D> SortedY = Points.OrderBy(t => t.Y).ToList();
            List<Point3D> SortedX = Points.OrderBy(t => t.X).ToList();

            List<Point3D> BelowZ = SortedZ.Where(t => t.Z > Median.Z).ToList();
            List<Point3D> BelowY = SortedY.Where(t => t.Y > Median.Y).ToList();
            List<Point3D> BelowX = SortedX.Where(t => t.X > Median.X).ToList();

            BelowZ = BelowZ.OrderBy(t => t.Z).ToList();
            BelowY = BelowY.OrderBy(t => t.Y).ToList();
            BelowX = BelowX.OrderBy(t => t.X).ToList();

            double MedianX = 0;
            double MedianY = 0;
            double MedianZ = 0;

            if (BelowZ.Count % 2 == 0)
            {
                if (BelowZ.Count == 0)
                {
                    MedianZ = SortedZ[0].Z;
                }
                else
                {
                    int MedianIndex0 = BelowZ.Count / 2 - 1;
                    int MedianIndex1 = BelowZ.Count / 2;
                    MedianZ = (BelowZ[MedianIndex0].Z + BelowZ[MedianIndex1].Z) / 2;
                }
            }
            else
            {
                int MedianIndex = BelowZ.Count / 2;
                MedianZ = BelowZ[MedianIndex].Z;
            }

            if (BelowY.Count % 2 == 0)
            {
                if (BelowY.Count == 0)
                {
                    MedianY = SortedY[0].Y;
                }
                else
                {
                    int MedianIndex0 = BelowY.Count / 2 - 1;
                    int MedianIndex1 = BelowY.Count / 2;
                    MedianY = (BelowY[MedianIndex0].Y + BelowY[MedianIndex1].Y) / 2;
                }
            }
            else
            {
                int MedianIndex = BelowY.Count / 2;
                MedianY = BelowY[MedianIndex].Y;
            }

            if (BelowX.Count % 2 == 0)
            {
                if (BelowX.Count == 0)
                {
                    MedianX = SortedX[0].X;
                }
                else
                {
                    int MedianIndex0 = BelowX.Count / 2 - 1;
                    int MedianIndex1 = BelowX.Count / 2;
                    MedianX = (BelowX[MedianIndex0].X + BelowX[MedianIndex1].X) / 2;
                }
            }
            else
            {
                int MedianIndex = BelowX.Count / 2;
                MedianX = BelowX[MedianIndex].X;
            }
            return new Point3D(MedianX, MedianY, MedianZ);

        }
        private Point3D GetMax()
        {
            double MaxX = double.MinValue;
            double MaxY = double.MinValue;
            double MaxZ = double.MinValue;

            for (int i = 0; i < Points.Count; i++)
            {
                if (Points[i].X >= MaxX) MaxX = Points[i].X;
                if (Points[i].Y >= MaxY) MaxY = Points[i].Y;
                if (Points[i].Z >= MaxZ) MaxZ = Points[i].Z;

            }

            return new Point3D(MaxX, MaxY, MaxZ);

        }
        private Point3D GetMin()
        {
            double MinX = double.MaxValue;
            double MinY = double.MaxValue;
            double MinZ = double.MaxValue;

            for (int i = 0; i < Points.Count; i++)
            {
                if (Points[i].X <= MinX) MinX = Points[i].X;
                if (Points[i].Y <= MinY) MinY = Points[i].Y;
                if (Points[i].Z <= MinZ) MinZ = Points[i].Z;

            }

            return new Point3D(MinX, MinY, MinZ);

        }
        public Point3D GetStandardDeviation()
        {
            Point3D C = Average;
            double X_sigma = 0;
            double Y_sigma = 0;
            double Z_sigma = 0;


            for (int i = 0; i < Points.Count; i++)
            {
                X_sigma += Math.Pow(Points[i].X - C.X, 2);
                Y_sigma += Math.Pow(Points[i].Y - C.Y, 2);
                Z_sigma += Math.Pow(Points[i].Z - C.Z, 2);
            }
            X_sigma = Math.Sqrt(X_sigma / Points.Count);
            Y_sigma = Math.Sqrt(Y_sigma / Points.Count);
            Z_sigma = Math.Sqrt(Z_sigma / Points.Count);

            return new Point3D(X_sigma, Y_sigma, Z_sigma);
        }
#if backup
        public void ReduceOutside(Polyline OuterLine)
        {
            KDTree<int> tmp = OuterLine.BuildIndexKDtree(true);
            Point3D Center = OuterLine.Average;
            Center.Z = 0;

            PointCloud cloud = new PointCloud();
            for (int i = 0; i < Points.Count; i++)
            {
                Point3D tempP = new Point3D(Points[i]);
                tempP.Z = 0;
                PointCloud NearCloud = m_Func.GetNearestPointCloud(tmp, tempP,2);
                Vector3D tmpV = new Vector3D(NearCloud.Points[1], NearCloud.Points[0]);

                Point3D CalP = tmpV.ShortestPoint(tempP, NearCloud.Points[1]);
                using (StreamWriter sw = new StreamWriter("d:\\Outer.xyz", true, Encoding.Default))
                {

                    sw.WriteLine(string.Format("{0} {1} {2}", CalP.X, CalP.Y, CalP.Z));
                    sw.Flush();
                    sw.Close();
                }
                Vector3D RefV = new Vector3D(CalP,tempP );
                Vector3D CenterV = new Vector3D(CalP, Center);

                if (RefV.L != 0.0)
                {
                    double Dot = Vector3D.Dot(RefV, CenterV);
                    if (Dot >= 0)
                    {
                        cloud.Add(Points[i]);
                    }
                }
                else
                {
                    cloud.Add(Points[i]);
                }
                
            }
            Points.Clear();
            Points = cloud.Points;
        }
#else
        public void ReduceOutside(Polyline OuterLine)
        {
            if (IsKdTreeBuilded == false) BuildIndexKDtree(true);
            Point3D Center = OuterLine.Average;
            Center.Z = 0;
            //OuterLine.Save("D:\\OuterLine.xyz");
            //StreamWriter sw = new StreamWriter("d:\\Outer.xyz", false, Encoding.Default);
            PointCloud cloud = new PointCloud();
            for (int i = 0; i < Points.Count; i++)
            {
                Point3D tempP = new Point3D(Points[i]);
                tempP.Z = 0;
                Point3D NearP1 = m_Func.GetNearestPoint(kdTree, tempP);
                PointCloud NearCloud = m_Func.GetNearestPointCloud(kdTree, NearP1, 2);
                Point3D NearP2 = NearCloud.Points[1];

                Vector3D tmpV = new Vector3D(NearP2, NearP1);

                Point3D CalP = tmpV.ShortestPoint(tempP, NearP2);


                //sw.WriteLine(string.Format("{0} {1} {2}", CalP.X, CalP.Y, CalP.Z));


                Vector3D RefV = new Vector3D(CalP, tempP);
                Vector3D CenterV = new Vector3D(CalP, Center);

                //if (CalP.Y > -66.1 && CalP.Y < -66.0 && CalP.X > -34.9 && CalP.X < -34.8)
                //{
                //    string gg = "";
                //}
                if (RefV.L != 0.0)
                {
                    double Dot = Vector3D.Dot(RefV, CenterV);
                    if (Dot >= 0)
                    {
                        cloud.Add(Points[i]);
                    }

                }
                else
                {
                    cloud.Add(Points[i]);
                }

            }
            Points.Clear();
            Points = cloud.Points;
            //sw.Flush();
            //sw.Close();
        }
#endif
        public bool LoadFromFile(string FilePath, bool IsBuildKDTree, int ResampleCount = 0)
        {
            Points.Clear();
            kdTree.Clear();
            if (!File.Exists(FilePath)) return false;
            using (StreamReader sr = new StreamReader(FilePath, Encoding.Default))
            {
                int i = 0;
                while (!sr.EndOfStream)
                {
                    string s = sr.ReadLine();
                    if (s != "")
                    {
                        string[] SplitData = s.Split(' ');
                        if (SplitData.Length == 1) SplitData = s.Split('\t');
                        if (SplitData.Length == 3)
                        {
                            double x = 0;
                            double y = 0;
                            double z = 0;

                            if (ResampleCount != 0)
                                if (i % ResampleCount != 0) continue;

                            if (!double.TryParse(SplitData[0], out x)) return false;
                            if (!double.TryParse(SplitData[1], out y)) return false;
                            if (!double.TryParse(SplitData[2], out z)) return false;

                            Point3D point = new Point3D(Math.Round(x, 2), Math.Round(y, 2), Math.Round(z, 2));
                            Points.Add(point);
                            kdTree.Add(new double[] { point.X, point.Y, point.Z }, Points.Count - 1);
                            i++;
                        }
                        else if(SplitData.Length == 6)
                        {
                            double x = 0;
                            double y = 0;
                            double z = 0;

                            int r = 0;
                            int g = 0;
                            int b = 0;

                            if (ResampleCount != 0)
                                if (i % ResampleCount != 0) continue;

                            if (!double.TryParse(SplitData[0], out x)) return false;
                            if (!double.TryParse(SplitData[1], out y)) return false;
                            if (!double.TryParse(SplitData[2], out z)) return false;
                            if (!int.TryParse(SplitData[3], out r)) return false;
                            if (!int.TryParse(SplitData[4], out g)) return false;
                            if (!int.TryParse(SplitData[5], out b)) return false;


                            Point3D point = new Point3D(Math.Round(x, 2), Math.Round(y, 2), Math.Round(z, 2));
                            DisplayOption displayOption = new DisplayOption();
                            displayOption.Color = Color.FromArgb(r, g, b);
                            point.AddOption(displayOption);
                            Points.Add(point);
                            kdTree.Add(new double[] { point.X, point.Y, point.Z }, Points.Count - 1);
                            i++;
                        }
                    }
                }
                sr.Close();
            }
            return true;

        }
        public bool LoadFromFile(string FilePath, DigitFormat Format, char SplitSymbol, int ResampleCount = 0)
        {
            Points.Clear();
            if (!File.Exists(FilePath)) return false;
            //List<string> stringList = ReadAllLines(FilePath);
            using (StreamReader sr = new StreamReader(FilePath, Encoding.Default))
            {
                int i = 0;
                while (!sr.EndOfStream)
                {
                    string s = sr.ReadLine();
                    if (s != "")
                    {
                        string[] SplitData = s.Split(SplitSymbol);
                        if (SplitData.Length >= 3)
                        {
                            double x = 0;
                            double y = 0;
                            double z = 0;

                            if (ResampleCount != 0)
                                if (i % ResampleCount != 0) continue;

                            ConvertXYZ(Format, SplitData[0], SplitData[1], SplitData[2], out x, out y, out z);

                            Point3D point = new Point3D(x, y, z);
                            Points.Add(point);
                            i++;
                        }
                    }
                }
                sr.Close();
            }
            return true;

        }
        public bool LoadFromPLY(string FilePath, int ResampleCount = 0)
        {
            Points.Clear();
            if (!File.Exists(FilePath)) return false;
            //List<string> stringList = ReadAllLines(FilePath);
            using (StreamReader sr = new StreamReader(FilePath, Encoding.Default))
            {
                string readData = "";
                int pointCount = 0;
                int i = 0;
                while (!sr.EndOfStream)
                {
                    readData = sr.ReadLine();
                    if (readData.Contains("element vertex"))
                    {
                        string[] splitData = readData.Split(' ');
                        pointCount = int.Parse(splitData[2]);
                    }
                    if (readData == "end_header") break;
                }
                while (!sr.EndOfStream)
                {
                    readData = sr.ReadLine();

                    if (readData != "")
                    {
                        string[] SplitData = readData.Split(' ');
                        if (SplitData.Length >= 3)
                        {
                            double x = 0;
                            double y = 0;
                            double z = 0;

                            if (ResampleCount != 0)
                                if (i % ResampleCount != 0) continue;

                            ConvertXYZ(DigitFormat.XYZ, SplitData[0], SplitData[1], SplitData[2], out x, out y, out z);

                            Point3D point = new Point3D(x, y, z);
                            Points.Add(point);
                            i++;
                        }
                    }
                }
                sr.Close();
            }
            return true;
        }
        private bool ConvertXYZ(DigitFormat Format, string str1, string str2, string str3, out double x, out double y, out double z, int RoundDigit = 2)
        {
            string strX, strY, strZ = "";
            x = -9999;
            y = -9999;
            z = -9999;
            if (RoundDigit < 0) RoundDigit = 0;
            switch (Format)
            {
                case DigitFormat.XYZ:
                    strX = str1;
                    strY = str2;
                    strZ = str3;

                    break;

                case DigitFormat.YXZ:
                    strX = str2;
                    strY = str1;
                    strZ = str3;

                    break;

                default:
                    strX = str1;
                    strY = str2;
                    strZ = str3;

                    break;
            }
            if (!double.TryParse(strX, out x)) return false;
            if (!double.TryParse(strY, out y)) return false;
            if (!double.TryParse(strZ, out z)) return false;

            x = Math.Round(x, RoundDigit);
            y = Math.Round(y, RoundDigit);
            z = Math.Round(z, RoundDigit);


            return true;
        }
        public bool LoadFromOFFFile(string FilePath)
        {
            Points.Clear();
            if (!File.Exists(FilePath)) return false;
            string Ext = Path.GetExtension(FilePath).ToUpper();
            if (Ext != ".OFF") return false;
            //List<string> stringList = ReadAllLines(FilePath);
            using (StreamReader sr = new StreamReader(FilePath, Encoding.Default))
            {
                sr.ReadLine();
                sr.ReadLine();
                while (!sr.EndOfStream)
                {
                    string s = sr.ReadLine();
                    if (s != "")
                    {
                        string[] SplitData = s.Split(' ');
                        if (SplitData.Length == 3)
                        {
                            double x = 0;
                            double y = 0;
                            double z = 0;

                            if (!double.TryParse(SplitData[0], out x)) return false;
                            if (!double.TryParse(SplitData[1], out y)) return false;
                            if (!double.TryParse(SplitData[2], out z)) return false;

                            Point3D point = new Point3D(Math.Round(x, 2), Math.Round(y, 2), Math.Round(z, 2));
                            Points.Add(point);
                        }
                    }
                }
                sr.Close();
            }
            return true;

        }

        public void Save(string FilePath)
        {

            using (StreamWriter sw = new StreamWriter(FilePath, false, Encoding.Default))
            {
                string WriteData = "";
                for (int i = 0; i < Points.Count; i++)
                {
                    WriteData = Points[i].ToString(false, false, false);//string.Format("{0:F2} {1:F2} {2:F2}", Points[i].X, Points[i].Y, Points[i].Z);
                    sw.WriteLine(WriteData);
                }

                sw.Flush();
                sw.Close();
            }
        }
        public void Save(string FilePath,bool WritePointTag,bool WritePointFlag,bool WritePointDt)
        {

            using (StreamWriter sw = new StreamWriter(FilePath, false, Encoding.Default))
            {
                string WriteData = "";
                for (int i = 0; i < Points.Count; i++)
                {
                    WriteData = Points[i].ToString(WritePointTag, WritePointFlag, WritePointDt);//string.Format("{0:F2} {1:F2} {2:F2}", Points[i].X, Points[i].Y, Points[i].Z);
                    sw.WriteLine(WriteData);
                }

                sw.Flush();
                sw.Close();
            }
        }

        public Point3D GetLastPoint()
        {
            return Points[Points.Count - 1];
        }

        public Point3D GetFirstPoint()
        {
            return Points[0];
        }
        public void SaveOffModel(string FilePath)
        {
            using (StreamWriter sw = new StreamWriter(FilePath, false, Encoding.Default))
            {
                sw.WriteLine("OFF");
                sw.WriteLine(string.Format("{0} 0 0", Points.Count));
                for (int i = 0; i < Points.Count; i++)
                {
                    string WriteData = Points[i].ToString(false, false, false);//string.Format("{0:F2} {1:F2} {2:F2}", Points[i].X, Points[i].Y, Points[i].Z);
                    sw.WriteLine(WriteData);
                }

                sw.Flush();
                sw.Close();
            }
        }
        protected List<string> ReadAllLines(string filePath)
        {
            FileStream fs;
            StreamReader sr;
            List<string> stringList = new List<string>();

            if (!File.Exists(filePath))
                return stringList;

            fs = File.OpenRead(filePath);
            sr = new StreamReader(fs);

            while (sr.Peek() >= 0)
            {
                stringList.Add(sr.ReadLine());
            }

            fs.Flush();
            fs.Close();
            fs.Dispose();
            fs = null;

            sr.Close();
            sr.Dispose();
            sr = null;



            return stringList;
        }
        public void OrderX()
        {
            List<Point3D> tempCollection = new List<Point3D>();
            tempCollection = Points.OrderBy(t => t.X).ToList().DeepClone();
            Points.Clear();
            Points = tempCollection.DeepClone();
        }
        public void OrderY()
        {
            List<Point3D> tempCollection = new List<Point3D>();
            tempCollection = Points.OrderBy(t => t.Y).ToList().DeepClone();
            Points.Clear();
            Points = tempCollection.DeepClone();
        }
        public void OrderZ()
        {
            List<Point3D> tempCollection = new List<Point3D>();
            tempCollection = Points.OrderBy(t => t.Z).ToList().DeepClone();
            Points.Clear();
            Points = tempCollection.DeepClone();
        }

        public void ResortBySameX()
        {
            List<Point3D> tempCollect = new List<Point3D>();

            tempCollect = Points.OrderBy(t => t.X).ThenByDescending(t => t.Y).ToList().DeepClone();

            Points.Clear();
            Points = tempCollect.DeepClone();
        }
        public void ResortBySameY()
        {
            List<Point3D> tempCollect = new List<Point3D>();

            tempCollect = Points.OrderBy(t => t.Y).ThenByDescending(t => t.X).ToList().DeepClone();

            Points.Clear();
            Points = tempCollect.DeepClone();
        }
        public void ReduceByZLayer(double LayerRange, int LayerPointCount)
        {

            for (int i = 0; i < Points.Count; i++)
            {

            }

            List<Point3D> tempCollect = new List<Point3D>();

            tempCollect = Points.OrderBy(t => t.Y).ThenByDescending(t => t.X).ToList().DeepClone();

            Points.Clear();
            Points = tempCollect.DeepClone();
        }
        public void ResortHeadBodyTail(double MinX, double MaxX, double HeadTailRatio, out PointCloud Head, out PointCloud Body, out PointCloud Tail)
        {
            double DiffX = (MaxX - MinX) * HeadTailRatio;
            double ResampleMaxX = Math.Round(MaxX - DiffX, 1);
            double ResampleMinX = Math.Round(MinX + DiffX, 1);

            Head = new PointCloud();
            Body = new PointCloud();
            Tail = new PointCloud();

            for (int i = 0; i < Points.Count; i++)
            {
                if (Points[i].X >= ResampleMaxX) Head.Add(Points[i]);
                else if (Points[i].X <= ResampleMinX) Tail.Add(Points[i]);
                else Body.Add(Points[i]);
            }

        }

        public void FillPoints(double Dis = 2.0)
        {
            List<Point3D> temp = new List<Point3D>();
            for (int i = 0; i < Points.Count - 1; i++)
            {
                int index0 = i;
                int index1 = i + 1;

                Point3D P0 = Points[index0];
                Point3D P1 = Points[index1];

                if (P0.Y == P1.Y)
                {
                    temp.Add(P0);
                    Vector3D V0 = new Vector3D(P0, P1);
                    Vector3D Unit = V0.GetUnitVector();
                    if (V0.L > Dis)
                    {
                        double Count = V0.L / Dis;
                        if (Count > 1.0)
                        {
                            int i_Count = (int)Count + 1;
                            for (int j = 1; j < i_Count; j++)
                            {
                                double X = Math.Round(P0.X + Unit.X * j, 1);
                                double Y = Math.Round(P0.Y + Unit.Y * j, 1);
                                double Z = Math.Round(P0.Z + Unit.Z * j, 1);

                                Point3D InsertP = new Point3D(X, Y, Z);
                                temp.Add(InsertP);
                            }
                        }
                    }
                }
                else
                {
                    temp.Add(P0);
                }
            }
            Points.Clear();
            Points = temp.DeepClone();
        }
        public void MoveCoordinate2Point(Point3D OrigP)
        {
            for (int i = 0; i < Points.Count; i++)
            {
                Points[i].X -= OrigP.X;
                Points[i].Y -= OrigP.Y;
                Points[i].Z -= OrigP.Z;
            }
        }
        public Vector3D GetNormalVector()
        {
            Vector3D vx, vy, vz = new Vector3D();
            PCA(out vx, out vy, out vz);
            return vz;
        }
        public void Norm()
        {
            Vector3D Vx, Vy, Vz;
            m_Func.PCA(this, out Vx, out Vy, out Vz);

            PointCloud After = m_Func.ShiftRotate(this, Average.X, Average.Y, Average.Z, Vx, Vy, Vz, m_Func.VoX, m_Func.VoY, m_Func.VoZ);

            Points.Clear();
            Points = After.Points;
        }
        public void Norm(Point3D PBefore, Vector3D VxBefore, Vector3D VyBefore, Vector3D VzBefore, Point3D PAfter, Vector3D VxAfter, Vector3D VyAfter, Vector3D VzAfter)
        {
            Vector3D v = new Vector3D(PBefore, PAfter);


            PointCloud After = m_Func.ShiftRotate(this, v.X, v.Y, v.Z, VxBefore, VyBefore, VzBefore, VxAfter, VyAfter, VzAfter);

            Points.Clear();
            Points = After.Points;
        }

        public void PCA(out Vector3D Vx, out Vector3D Vy, out Vector3D Vz)
        {
            m_Func.PCA(this, out Vx, out Vy, out Vz);
        }
        public void PCA(out Vector3D Vx, out Vector3D Vy, out Vector3D Vz, out Point3D Center)
        {
            m_Func.PCA(this, out Vx, out Vy, out Vz, out Center);
        }
        public void RotateRz90()
        {

            PointCloud After = m_Func.Rotate(this, Vector3D.ZAxis, 90);

            Points.Clear();
            Points = After.Points;
        }
        public void RotateRy90()
        {
            PointCloud After = m_Func.Rotate(this, Vector3D.YAxis, 90);

            Points.Clear();
            Points = After.Points;

        }
        public void RotateRx90()
        {
            PointCloud After = m_Func.Rotate(this, Vector3D.XAxis, 90);

            Points.Clear();
            Points = After.Points;

        }

        public Dictionary<int, double> GetEveryDistance(Point3D refPoint)
        {
            Dictionary<int, double> output = new Dictionary<int, double>();

            for (int i = 0; i < Points.Count; i++)
            {
                output.Add(i, Point3D.Distance(refPoint, Points[i]));
            }

            return output;
        }

        public Dictionary<int, double> GetEveryDistance(Point3D refPoint, int StartIndex, int EndIndex)
        {
            Dictionary<int, double> output = new Dictionary<int, double>();

            for (int i = StartIndex; i <= EndIndex; i++)
            {
                output.Add(i, Point3D.Distance(refPoint, Points[i]));
            }

            return output;
        }
        public void SortingPoint3DCW()
        {
            Point3D cog = Average; ;

            // bubble sort
            for (int i = 0; i < Points.Count; i++)
            {
                for (int j = 0; j < Points.Count - i - 1; j++)
                {
                    if (!Point3D.CompareCWValue(Points[j], Points[j + 1], cog))
                    {
                        Point3D temp = Points[j];
                        Points[j] = Points[j + 1];
                        Points[j + 1] = temp;
                    }
                }
            }
        }

        public void SortingPoint3DCCW()
        {
            Point3D cog = Average;

            // bubble sort
            for (int i = 0; i < Points.Count; i++)
            {
                for (int j = 0; j < Points.Count - i - 1; j++)
                {
                    if (Point3D.CompareCWValue(Points[j], Points[j + 1], cog))
                    {
                        Point3D temp = Points[j];
                        Points[j] = Points[j + 1];
                        Points[j + 1] = temp;
                    }

                }
            }
        }
        public void SortingPoint3DCW(Point3D refP)
        {

            // bubble sort
            for (int i = 0; i < Points.Count; i++)
            {
                for (int j = 0; j < Points.Count - i - 1; j++)
                {
                    if (!Point3D.CompareCWValue(Points[j], Points[j + 1], refP))
                    {
                        Point3D temp = Points[j];
                        Points[j] = Points[j + 1];
                        Points[j + 1] = temp;
                    }

                }
            }
        }


        public void SortingPoint3DCCW(Point3D refP)
        {
            // bubble sort
            for (int i = 0; i < Points.Count; i++)
            {
                for (int j = 0; j < Points.Count - i - 1; j++)
                {
                    if (Point3D.CompareCWValue(Points[j], Points[j + 1], refP))
                    {
                        Point3D temp = Points[j];
                        Points[j] = Points[j + 1];
                        Points[j + 1] = temp;
                    }

                }
            }
        }

        public Point3D FindClosetPointFromPlane(RsPlane find, Point3D refPoint = null)
        {
            double length = double.MaxValue;
            int index = 0;

            double dist = 0;

            List<Point3D> canidates = new List<Point3D>();

            for (int i = 0; i < Points.Count - 1; i++)
            {
                if (refPoint != null)
                {

                    dist = Point3D.Distance(refPoint, Points[i]);

                    if (dist < length)
                    {
                        length = dist;
                        index = i;
                    }
                }
                else
                {
                    dist = find.Distance(Points[i]);

                    if (dist < length)
                    {
                        length = dist;
                        index = i;
                    }
                }

            }

            Point3D returnP = new Point3D();


            returnP = Points[index];


            return returnP;

        }


        /// <summary>
        /// 計算xy平面的主軸, 將雲點座標轉換使得其主軸與Y軸重和, Z值不變
        /// </summary>
        public void PrincipalAxisCalibration(out Vector3D v1, out Vector3D v2)
        {
            // 計算重心座標
            Point3D com = Average;

            // 移至原點
            List<Point3D> temp = new List<Point3D>();

            foreach (Point3D pt in Points)
            {
                temp.Add(new Point3D(pt.X - com.X, pt.Y - com.Y, pt.Z - com.Z));
            }

            // 計算特徵值 lamda
            double sigmaX = 0;
            double sigmaY = 0;
            double sigmaXY = 0;

            foreach (Point3D pt in temp)
            {
                sigmaX += Math.Pow(pt.X, 2);
                sigmaY += Math.Pow(pt.Y, 2);
                sigmaXY += pt.X * pt.Y;
            }

            sigmaX /= temp.Count;
            sigmaXY /= temp.Count;
            sigmaY /= temp.Count;

            double sumvars = sigmaX + sigmaY;
            double diffvars = sigmaX - sigmaY;
            double discriminant = diffvars * diffvars + 4 * sigmaXY * sigmaXY;
            double sqrtdiscr = Math.Sqrt(discriminant);

            double plamda = (sumvars + sqrtdiscr) / 2;
            double nlamda = (sumvars - sqrtdiscr) / 2;

            v1 = new Vector3D();
            v2 = new Vector3D();

            v1.X = sigmaX + sigmaXY - nlamda;
            v2.Y = sigmaY + sigmaXY - nlamda;

            v1.X = sigmaX + sigmaXY - plamda;
            v2.Y = sigmaY + sigmaXY - plamda;

            v1 = v1.GetUnitVector();
            v2 = v2.GetUnitVector();

            List<Point3D> transformPts = new List<Point3D>();

            foreach (Point3D pt in temp)
            {
                Point3D tmpPt = new Point3D();
                tmpPt.X = -v2.X * pt.X - v2.Y * pt.Y;
                tmpPt.Y = v1.X * pt.X + v1.Y * pt.Y;
                tmpPt.Z = pt.Z;
                transformPts.Add(tmpPt);
            }
            Points.Clear();
            Points = transformPts.DeepClone();
        }
        public void PCA(Point3D refP, out Vector3D v1, out Vector3D v2)
        {
            // 計算重心座標
            Point3D com = refP;

            // 移至原點
            List<Point3D> temp = new List<Point3D>();

            foreach (Point3D pt in Points)
            {
                temp.Add(new Point3D(pt.X - com.X, pt.Y - com.Y, pt.Z - com.Z));
            }


            // 計算特徵值 lamda
            double sigmaX = 0;
            double sigmaY = 0;
            double sigmaXY = 0;

            foreach (Point3D pt in temp)
            {
                sigmaX += Math.Pow(pt.X, 2);
                sigmaY += Math.Pow(pt.Y, 2);
                sigmaXY += pt.X * pt.Y;
            }

            sigmaX /= temp.Count;
            sigmaXY /= temp.Count;
            sigmaY /= temp.Count;

            double sumvars = sigmaX + sigmaY;
            double diffvars = sigmaX - sigmaY;
            double discriminant = diffvars * diffvars + 4 * sigmaXY * sigmaXY;
            double sqrtdiscr = Math.Sqrt(discriminant);

            double plamda = (sumvars + sqrtdiscr) / 2;
            double nlamda = (sumvars - sqrtdiscr) / 2;

            v1 = new Vector3D();
            v2 = new Vector3D();

            v1.X = sigmaX + sigmaXY - nlamda;
            v1.Y = sigmaY + sigmaXY - nlamda;

            v2.X = sigmaX + sigmaXY - plamda;
            v2.Y = sigmaY + sigmaXY - plamda;

            v1 = v1.GetUnitVector();
            v2 = v2.GetUnitVector();
        }
        public Point3D GetNearest(Point3D refP)
        {
            if(IsKdTreeBuilded == false)   BuildIndexKDtree();
            PointCloud tree = m_Func.GetNearestPointCloud(kdTree, refP, 1);
            if (tree.Count == 1)
            {
                return tree.Points[0];
            }
            else return null;
        }
        public PointCloud GetNearestCloud(Point3D refP,double radius)
        {
            if (IsKdTreeBuilded == false) BuildIndexKDtree();
            PointCloud tree = m_Func.GetNearestPointCloud(kdTree, refP,radius);

            return tree;
        }
        public List<int> GetNearestCloudIndex(Point3D refP, double radius)
        {
            if (IsKdTreeBuilded == false) BuildIndexKDtree();
            List<int> output = new List<int>();
            m_Func.GetNearestPointCloud(kdTree, refP, radius,out output);

            return output;
        }
        public PointCloud GetEdge(Point3D BoxMin, Point3D BoxMax, bool BoxSplitX, double BoxSplitRange = 0.5)
        {
            if (IsKdTreeBuilded == false) BuildIndexKDtree();
            Box b = new Box(BoxMin.X, BoxMax.X, BoxMin.Y, BoxMax.Y, BoxMin.Z, BoxMax.Z);

            List<Box> SplitBoxs = new List<Box>();
            if (BoxSplitX) SplitBoxs = b.SplitByX(BoxSplitRange);
            else SplitBoxs = b.SplitByY(BoxSplitRange);

            PointCloud Output = new PointCloud();
            ParallelLoopResult PResult1 = Parallel.For(0, SplitBoxs.Count, (int i, ParallelLoopState PLS) =>
            {
                PointCloud temp = m_Func.GetNearestPointCloud(kdTree, SplitBoxs[i].Center, SplitBoxs[i].ContactBallR);
                temp.RemoveOutsideBox(SplitBoxs[i]);
                if (temp.Count > 0)
                {
                    lock (Output)
                    {
                        Output.Add(temp.GetMaxZPoint());
                    }
                }

            });
            return Output;
        }
        public PointCloud GetEdge(Point3D BoxMin, Point3D BoxMax, bool BoxSplitX, double MinCount, double BoxSplitRange = 0.5)
        {
            if (IsKdTreeBuilded == false) BuildIndexKDtree();
            Box b = new Box(BoxMin.X, BoxMax.X, BoxMin.Y, BoxMax.Y, BoxMin.Z, BoxMax.Z);

            List<Box> SplitBoxs = new List<Box>();
            if (BoxSplitX) SplitBoxs = b.SplitByX(BoxSplitRange);
            else SplitBoxs = b.SplitByY(BoxSplitRange);

            PointCloud Output = new PointCloud();
            ParallelLoopResult PResult1 = Parallel.For(0, SplitBoxs.Count, (int i, ParallelLoopState PLS) =>
            {
                PointCloud temp = m_Func.GetNearestPointCloud(kdTree, SplitBoxs[i].Center, SplitBoxs[i].ContactBallR);
                temp.RemoveOutsideBox(SplitBoxs[i]);
                if (temp.Count > MinCount)
                {
                    lock (Output)
                    {
                        Output.Add(temp.GetMaxZPoint());
                    }
                }

            });
            return Output;
        }
        public PointCloud GetEdge(Point3D BoxMin, Point3D BoxMax, bool BoxSplitX, double MinCount, bool UseClosePoint, double BoxSplitRange = 0.5, bool IsUseParallel = true)
        {
            if (IsKdTreeBuilded == false) BuildIndexKDtree();
            Box b = new Box(BoxMin.X, BoxMax.X, BoxMin.Y, BoxMax.Y, BoxMin.Z, BoxMax.Z);

            List<Box> SplitBoxs = new List<Box>();
            if (BoxSplitX) SplitBoxs = b.SplitByX(BoxSplitRange);
            else SplitBoxs = b.SplitByY(BoxSplitRange);

            PointCloud Output = new PointCloud();
            if (IsUseParallel)
            {
                ParallelLoopResult PResult1 = Parallel.For(0, SplitBoxs.Count, (int i, ParallelLoopState PLS) =>
                {
                    PointCloud temp = m_Func.GetNearestPointCloud(kdTree, SplitBoxs[i].Center, SplitBoxs[i].ContactBallR);
                    temp.RemoveOutsideBox(SplitBoxs[i]);
                    Point3D CloseP = temp.Average;
                    //temp.Save($"d:\\test\\{DateTime.Now:HHmmss}_{i}.xyz");
                    if (temp.Count > MinCount)
                    {
                        lock (Output)
                        {
                            Point3D GotPoint = new Point3D();

                            if (UseClosePoint) GotPoint = temp.GetMaxZPoint(CloseP);
                            else GotPoint = temp.GetMaxZPoint();

                            Output.Add(GotPoint);
                        }
                    }

                });
            }
            else
            {
                for (int i = 0; i < SplitBoxs.Count; i++)
                {
                    PointCloud temp = m_Func.GetNearestPointCloud(kdTree, SplitBoxs[i].Center, SplitBoxs[i].ContactBallR);
                    temp.RemoveOutsideBox(SplitBoxs[i]);
                    Point3D CloseP = temp.Average;

                    if (temp.Count > MinCount)
                    {
                        lock (Output)
                        {
                            Point3D GotPoint = new Point3D();

                            if (UseClosePoint) GotPoint = temp.GetMaxZPoint(CloseP);
                            else GotPoint = temp.GetMaxZPoint();

                            Output.Add(GotPoint);
                        }
                    }
                }
            }
            return Output;
        }

        public PointCloud GetSectionCloud(Point3D P1, Point3D P2, double NearDis = 0.6, double ProfileSearchRadius = 4)
        {

            Vector3D V_H = new Vector3D(P1.X - P2.X, P1.Y - P2.Y, 0);
            Vector3D V_V = new Vector3D(0, 0, 1);

            Vector3D V_N = Vector3D.Cross(V_H, V_V);
            RsPlane CrossSectionPlane = new RsPlane(V_N, P1);

            PointCloud tempCloud = ReturnNearPlanePoints(CrossSectionPlane, NearDis);
            tempCloud.Project2Plane(CrossSectionPlane);

            double BigX = Math.Max(P1.X, P2.X);
            double SmallX = Math.Min(P1.X, P2.X);

            tempCloud.ReduceAboveX(BigX + 2);
            tempCloud.ReduceBelowX(SmallX);

            return tempCloud;
        }
        public Point3D FindPoint3DByTag(int tag)
        {
            for(int i = 0; i < Points.Count; i++)
            {
                int tmpTag = Points[i].tag;
                if (tmpTag == tag) return Points[i];
            }
            return null;
        }

        public new IEnumerator<Point3D> GetEnumerator()
        {
            for (int i = 0; i < Points.Count; i++)
            {
                yield return Points[i];
            }
        }
    }

    public enum DigitFormat : int
    {
        XYZ = 0,
        YXZ,
    }

}

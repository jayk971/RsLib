using Newtonsoft.Json;
using RsLib.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel;
namespace RsLib.PointCloudLib
{
    [Serializable]
    public partial class Box : Object3D
    {
        [DefaultValue(null)]
        public Point3D MinP { get; set; }

        [DefaultValue(null)]
        public Point3D MaxP { get; set; }

        [JsonIgnore]
        public double Volumn { get { return GetVolume(); } }

        [JsonIgnore]
        public Point3D Center { get { return GetCenter(); } }

        [JsonIgnore]
        public double ContactBallR { get { return Point3D.Distance(Center, MinP); } }

        public override uint DataCount => 1;

        /// <summary>
        /// 初始化 box 類別, 最大及最小角點皆為 (0,0)
        /// </summary>
        public Box()
        {
            MinP = new Point3D();
            MaxP = new Point3D();
        }
        /// <summary>
        /// 初始化 box 類別, 以兩點拉出長方體
        /// </summary>
        /// <param name="P1">第 1 點</param>
        /// <param name="P2">第 2 點</param>
        public Box(Point3D P1, Point3D P2)
        {
            double MaxX = Math.Max(P1.X, P2.X);
            double MaxY = Math.Max(P1.Y, P2.Y);
            double MaxZ = Math.Max(P1.Z, P2.Z);

            double MinX = Math.Min(P1.X, P2.X);
            double MinY = Math.Min(P1.Y, P2.Y);
            double MinZ = Math.Min(P1.Z, P2.Z);

            MinP = new Point3D(MinX, MinY, MinZ);
            MaxP = new Point3D(MaxX, MaxY, MaxZ);
        }
        /// <summary>
        /// 初始化 box 類別, 以兩點拉出長方體
        /// </summary>
        /// <param name="x1">第 1 點 X 座標</param>
        /// <param name="x2">第 2 點 X 座標</param>
        /// <param name="y1">第 1 點 Y 座標</param>
        /// <param name="y2">第 2 點 Y 座標</param>
        /// <param name="z1">第 1 點 Z 座標</param>
        /// <param name="z2">第 2 點 Z 座標</param>
        public Box(double x1, double x2, double y1, double y2, double z1, double z2)
        {
            double MaxX = Math.Max(x1, x2);
            double MaxY = Math.Max(y1, y2);
            double MaxZ = Math.Max(z1, z2);

            double MinX = Math.Min(x1, x2);
            double MinY = Math.Min(y1, y2);
            double MinZ = Math.Min(z1, z2);

            MinP = new Point3D(MinX, MinY, MinZ);
            MaxP = new Point3D(MaxX, MaxY, MaxZ);
        }
        /// <summary>
        /// 將長方體依照 X 方向等距切分成若干長方體
        /// </summary>
        /// <param name="SplitDis">切分距離</param>
        /// <returns></returns>
        public List<Box> SplitByX(double SplitDis)
        {
            List<Box> Output = new List<Box>();
            double diff = MaxP.X - MinP.X;
            if (diff == 0) return null;
            int t = (int)(diff / SplitDis);
            if (t <= 1)
                Output.Add(this);
            else
            {
                for (int i = 0; i < t; i++)
                {
                    int i0 = i;
                    int i1 = i + 1;

                    double x0 = MinP.X + i0 * SplitDis;
                    double x1 = MinP.X + i1 * SplitDis;

                    Box SplitBox = new Box(x0, x1, MinP.Y, MaxP.Y, MinP.Z, MaxP.Z);
                    Output.Add(SplitBox);
                }
            }
            return Output;
        }
        /// <summary>
        /// 將長方體依照 Y 方向等距切分成若干長方體
        /// </summary>
        /// <param name="SplitDis">切分距離</param>
        /// <returns></returns>
        public List<Box> SplitByY(double SplitDis)
        {
            List<Box> Output = new List<Box>();
            double diff = MaxP.Y - MinP.Y;
            if (diff == 0) return null;
            int t = (int)(diff / SplitDis);
            if (t <= 1)
                Output.Add(this);
            else
            {
                for (int i = 0; i < t; i++)
                {
                    int i0 = i;
                    int i1 = i + 1;

                    double y0 = MinP.Y + i0 * SplitDis;
                    double y1 = MinP.Y + i1 * SplitDis;

                    Box SplitBox = new Box(MinP.X, MaxP.X, y0, y1, MinP.Z, MaxP.Z);
                    Output.Add(SplitBox);
                }
            }
            return Output;
        }
        /// <summary>
        /// 計算長方體體積
        /// </summary>
        /// <returns>體積 mm^3</returns>
        private double GetVolume()
        {
            double x = MaxP.X - MinP.X;
            double y = MaxP.Y - MinP.Y;
            double z = MaxP.Z - MinP.Z;

            return x * y * z;

        }
        /// <summary>
        /// 取得長方體中心點
        /// </summary>
        /// <returns>中心點位</returns>
        private Point3D GetCenter()
        {
            double x = (MaxP.X + MinP.X) / 2;
            double y = (MaxP.Y + MinP.Y) / 2;
            double z = (MaxP.Z + MinP.Z) / 2;
            return new Point3D(x, y, z);

        }
        /// <summary>
        /// 取得長方體 8 個角點
        /// </summary>
        /// <returns>8 個角點</returns>
        public List<Point3D> GetCorners()
        {
            List<Point3D> Output = new List<Point3D>();
            Output.Add(new Point3D(MinP.X, MinP.Y, MinP.Z));
            Output.Add(new Point3D(MinP.X, MaxP.Y, MinP.Z));
            Output.Add(new Point3D(MaxP.X, MaxP.Y, MinP.Z));
            Output.Add(new Point3D(MaxP.X, MinP.Y, MinP.Z));

            Output.Add(new Point3D(MinP.X, MinP.Y, MaxP.Z));
            Output.Add(new Point3D(MinP.X, MaxP.Y, MaxP.Z));
            Output.Add(new Point3D(MaxP.X, MaxP.Y, MaxP.Z));
            Output.Add(new Point3D(MaxP.X, MinP.Y, MaxP.Z));

            return Output;
        }
    }

    [Serializable]
    public partial class TiltBox
    {
        [DefaultValue(null)]
        public RsPlane BasePlane { get; set; }

        [DefaultValue(null)]
        public Point3D BaseP0 { get; set; }

        [DefaultValue(null)]
        public Point3D BaseP1 { get; set; }

        [DefaultValue(null)]
        public Point3D BaseP2 { get; set; }

        [DefaultValue(null)]
        public Point3D BaseP3 { get; set; }
        [DefaultValue(null)]
        public Vector3D RefVec { get; set; }
        [DefaultValue(0.0)]
        public double MinDis { get; set; }

        [DefaultValue(1.0)]
        public double MaxDis { get; set; }

        [DefaultValue(null)]
        public List<Point3D> Corners { get; set; }

        [JsonIgnore]
        public Point3D BaseCenter { get { return GetPlaneCenter(); } }
        [JsonIgnore]
        public Vector3D V01 { get { return new Vector3D(BaseP0, BaseP1); } }
        [JsonIgnore]
        public Vector3D V13 { get { return new Vector3D(BaseP1, BaseP3); } }
        [JsonIgnore]
        public Vector3D V32 { get { return new Vector3D(BaseP3, BaseP2); } }
        [JsonIgnore]
        public Vector3D V20 { get { return new Vector3D(BaseP2, BaseP0); } }

        public TiltBox()
        {
            BasePlane = new RsPlane();

            BaseP0 = new Point3D();
            BaseP1 = new Point3D();
            BaseP2 = new Point3D();
            BaseP3 = new Point3D();

            Corners = new List<Point3D>();

            MinDis = 0.0;
            MaxDis = 1.0;
        }

        //public TiltBox(Point3D P0,Point3D P1,Point3D P2, double Min,double Max)
        //{
        //    Corners = new List<Point3D>();

        //    BasePlane = new Plane(P0,P1,P2);

        //    BaseP0 = P0.DeepClone();
        //    BaseP1 = P1.DeepClone();
        //    BaseP2 = P2.DeepClone();

        //    MinDis = Math.Min(Min, Max);
        //    MaxDis = Math.Max(Min, Max);

        //    Vector3D V1 = new Vector3D(P0, P1);
        //    Vector3D V2 = new Vector3D(P0, P2);
        //    Vector3D V3 = V1 + V2;

        //    BaseP3 =  BaseP0 + V3;

        //    Point3D Corner1 = new Point3D(BaseP0, BasePlane.Normal, Max);
        //    Corners.Add(Corner1);
        //    Point3D Corner2 = new Point3D(BaseP1, BasePlane.Normal, Max);
        //    Corners.Add(Corner2);
        //    Point3D Corner3 = new Point3D(BaseP3, BasePlane.Normal, Max);
        //    Corners.Add(Corner3);
        //    Point3D Corner4 = new Point3D(BaseP2, BasePlane.Normal, Max);
        //    Corners.Add(Corner4);
        //    Point3D Corner5 = new Point3D(BaseP0, BasePlane.Normal, Min);
        //    Corners.Add(Corner5);
        //    Point3D Corner6 = new Point3D(BaseP1, BasePlane.Normal, Min);
        //    Corners.Add(Corner6);
        //    Point3D Corner7 = new Point3D(BaseP3, BasePlane.Normal, Min);
        //    Corners.Add(Corner7);
        //    Point3D Corner8 = new Point3D(BaseP2, BasePlane.Normal, Min);
        //    Corners.Add(Corner8);

        //}
        public TiltBox(Point3D P0, Point3D P1, Point3D P2, Vector3D RefV, double Min, double Max)
        {
            Corners = new List<Point3D>();

            RefVec = RefV;

            BasePlane = new RsPlane(P0, P1, P2, RefVec);

            BaseP0 = P0.DeepClone();
            BaseP1 = P1.DeepClone();
            BaseP2 = P2.DeepClone();

            MinDis = Math.Min(Min, Max);
            MaxDis = Math.Max(Min, Max);

            Vector3D V1 = new Vector3D(P0, P1);
            Vector3D V2 = new Vector3D(P0, P2);
            Vector3D V3 = V1 + V2;

            BaseP3 = BaseP0 + V3;

            Point3D Corner1 = new Point3D(BaseP0, BasePlane.Normal, Max);
            Corners.Add(Corner1);
            Point3D Corner2 = new Point3D(BaseP1, BasePlane.Normal, Max);
            Corners.Add(Corner2);
            Point3D Corner3 = new Point3D(BaseP3, BasePlane.Normal, Max);
            Corners.Add(Corner3);
            Point3D Corner4 = new Point3D(BaseP2, BasePlane.Normal, Max);
            Corners.Add(Corner4);
            Point3D Corner5 = new Point3D(BaseP0, BasePlane.Normal, Min);
            Corners.Add(Corner5);
            Point3D Corner6 = new Point3D(BaseP1, BasePlane.Normal, Min);
            Corners.Add(Corner6);
            Point3D Corner7 = new Point3D(BaseP3, BasePlane.Normal, Min);
            Corners.Add(Corner7);
            Point3D Corner8 = new Point3D(BaseP2, BasePlane.Normal, Min);
            Corners.Add(Corner8);

        }
        public TiltBox(Point3D P0, Point3D P1, Point3D P2, Point3D RefP, double Min, double Max)
        {
            Corners = new List<Point3D>();

            RefVec = new Vector3D(P0, RefP);

            BasePlane = new RsPlane(P0, P1, P2, RefP);

            BaseP0 = P0.DeepClone();
            BaseP1 = P1.DeepClone();
            BaseP2 = P2.DeepClone();

            MinDis = Math.Min(Min, Max);
            MaxDis = Math.Max(Min, Max);

            Vector3D V1 = new Vector3D(P0, P1);
            Vector3D V2 = new Vector3D(P0, P2);
            Vector3D V3 = V1 + V2;

            BaseP3 = BaseP0 + V3;

            Point3D Corner1 = new Point3D(BaseP0, BasePlane.Normal, Max);
            Corners.Add(Corner1);
            Point3D Corner2 = new Point3D(BaseP1, BasePlane.Normal, Max);
            Corners.Add(Corner2);
            Point3D Corner3 = new Point3D(BaseP3, BasePlane.Normal, Max);
            Corners.Add(Corner3);
            Point3D Corner4 = new Point3D(BaseP2, BasePlane.Normal, Max);
            Corners.Add(Corner4);
            Point3D Corner5 = new Point3D(BaseP0, BasePlane.Normal, Min);
            Corners.Add(Corner5);
            Point3D Corner6 = new Point3D(BaseP1, BasePlane.Normal, Min);
            Corners.Add(Corner6);
            Point3D Corner7 = new Point3D(BaseP3, BasePlane.Normal, Min);
            Corners.Add(Corner7);
            Point3D Corner8 = new Point3D(BaseP2, BasePlane.Normal, Min);
            Corners.Add(Corner8);

        }
        public double GetFarestCornerFromCenter()
        {
            double Max = double.MinValue;
            double Cal = 0;
            Point3D Center = BaseCenter;
            for (int i = 0; i < Corners.Count; i++)
            {
                Cal = Corners[i].Distance(Center);
                if (Cal > Max) Max = Cal;
            }
            return Max;
        }
        public void Clear()
        {
            BasePlane = new RsPlane();

            BaseP0 = new Point3D();
            BaseP1 = new Point3D();
            BaseP2 = new Point3D();
            BaseP3 = new Point3D();

            Corners = new List<Point3D>();

            MinDis = 0.0;
            MaxDis = 1.0;
        }
        public List<TiltBox> Split(int Count)
        {
            List<TiltBox> Output = new List<TiltBox>();
            Vector3D V01 = new Vector3D(BaseP0, BaseP1);
            Vector3D V02 = new Vector3D(BaseP0, BaseP2);

            V01.X /= Count;
            V01.Y /= Count;
            V01.Z /= Count;

            V02.X /= Count;
            V02.Y /= Count;
            V02.Z /= Count;

            for (int i = 0; i < Count; i++)
            {

                for (int j = 0; j < Count; j++)
                {
                    Point3D NewP0 = new Point3D();
                    NewP0.X = BaseP0.X + V01.X * i + V02.X * j;
                    NewP0.Y = BaseP0.Y + V01.Y * i + V02.Y * j;
                    NewP0.Z = BaseP0.Z + V01.Z * i + V02.Z * j;

                    Point3D NewP1 = new Point3D();
                    NewP1.X = NewP0.X + V01.X;
                    NewP1.Y = NewP0.Y + V01.Y;
                    NewP1.Z = NewP0.Z + V01.Z;

                    Point3D NewP2 = new Point3D();
                    NewP2.X = NewP0.X + V02.X;
                    NewP2.Y = NewP0.Y + V02.Y;
                    NewP2.Z = NewP0.Z + V02.Z;

                    TiltBox NewBox = new TiltBox(NewP0, NewP1, NewP2, RefVec, MinDis, MaxDis);

                    Output.Add(NewBox);
                }


            }



            return Output;
        }
        private Point3D GetPlaneCenter()
        {
            return new Point3D((BaseP1.X + BaseP2.X) / 2, (BaseP1.Y + BaseP2.Y) / 2, (BaseP1.Z + BaseP2.Z) / 2);
        }
        public bool IsInsideTiltBox(Point3D TargetP)
        {
            RsPlane plane = BasePlane;
            Point3D point, ProjP;

            Vector3D V0, V1, V2, V3, Vc0, Vc1, Vc2, Vc3;
            double Dot1, Dot2, Dot3, Dot4, Dot5, Dot6;


            point = new Point3D(TargetP);
            ProjP = plane.ProjectPOnPlane(point);

            V0 = new Vector3D(BaseP0, ProjP);
            V1 = new Vector3D(BaseP1, ProjP);
            V2 = new Vector3D(BaseP2, ProjP);
            V3 = new Vector3D(BaseP3, ProjP);

            Vc0 = Vector3D.Cross(V0, V01);
            Vc1 = Vector3D.Cross(V1, V13);
            Vc2 = Vector3D.Cross(V2, V20);
            Vc3 = Vector3D.Cross(V3, V32);

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
                    if (Dis < Math.Abs(MaxDis)) return true;
                    else return false;
                }
                else
                {
                    if (Dis < Math.Abs(MinDis)) return true;
                    else return false;
                }

            }
            else return false;
        }

    }
}

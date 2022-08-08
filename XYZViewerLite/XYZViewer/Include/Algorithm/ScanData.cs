using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Runtime.InteropServices;

// for File I/O
using System.IO;

namespace Automation
{
    #region 2D運算
    public class Point2D
    {
        public const double InvalidX = -1;   //定義：無效之X 2D座標
        public const double InvalidY = -1;   //定義：無效之Y 2D座標
        public double X;    //X座標
        public double Y;    //Y座標
        public double Dt;   //記錄與上一點之距離
        public Point2D()
        {
            X = InvalidX;
            Y = InvalidY;
            Dt = 0.0;
        }

        public Point2D(Point2D p)
        {
            X = p.X;
            Y = p.Y;
            Dt = p.Dt;
        }
        public Point2D(double x, double y)
        {
            X = x;
            Y = y;
            Dt = 0.0;
        }
        public Point2D(double x, double y, double dt)
        {
            X = x;
            Y = y;
            Dt = dt;
        }

        /// <summary>
        /// Description:    static method, calculate the distance from A(x,y) to B(x,y)
        /// Date:           2013.8.12
        /// </summary>
        /// <returns>distance</returns>
        public static double Distance(Point2D A, Point2D B)
        {
            double d = Math.Sqrt(Math.Pow(A.X - B.X, 2) + Math.Pow(A.Y - B.Y, 2));
            return d;
        }
    };

    public class Vector2D
    {
        private double x;
        //X分量
        public double X
        {
            get { return x; }
            set { x = value; }
        }

        private double y;
        //Y分量
        public double Y
        {
            get { return y; }
            set { y = value; }
        }

        private double l;
        //向量長度
        public double L
        {
            get
            {
                l = Length();
                return l;
            }
        }

        // 計算向量長度
        private double Length()
        {
            double value = Math.Sqrt(Math.Pow(x, 2) + Math.Pow(y, 2));
            return value;
        }

        // 回傳單位向量
        public Vector2D UnitVector()
        {
            Vector2D v_unit = new Vector2D();
            if (L != 0)
            {
                v_unit.X = x / l;
                v_unit.Y = y / l;
            }
            return v_unit;
        }

        // 0向量建構
        public Vector2D()
        {
            x = 0.0;
            y = 0.0;
        }
        // 直接指定分量建構
        public Vector2D(double x, double y)
        {
            this.x = x;
            this.y = y;
        }
        // 指定起點與終點建構
        public Vector2D(Point2D startP, Point2D endP)
        {
            x = endP.X - startP.X;
            y = endP.Y - startP.Y;
        }

        /// <summary>
        /// 計算向量A+B
        /// </summary>
        /// <returns>向量</returns>
        public static Vector2D Add(Vector2D A, Vector2D B)
        {
            Vector2D v = new Vector2D();
            v.X = A.X + B.X;
            v.Y = A.Y + B.Y;
            return v;
        }

        /// <summary>
        /// 計算向量A-B
        /// </summary>
        /// <returns>向量</returns>
        public static Vector2D Sub(Vector2D A, Vector2D B)
        {
            Vector2D v = new Vector2D();
            v.X = A.X - B.X;
            v.Y = A.Y - B.Y;
            return v;
        }

        /// <summary>
        /// 計算 純量t * 向量A
        /// </summary>
        /// <returns>向量</returns>
        public static Vector2D Multiple(Vector2D A, double t)
        {
            Vector2D v = new Vector2D();
            v.X = t * A.X;
            v.Y = t * A.Y;
            return v;
        }

        /// <summary>
        /// Description:    static method, calculate the dot product of vector A and vector B
        /// Date:           2013.9.18
        /// </summary>
        /// <returns>distance</returns>
        public static double Dot(Vector2D A, Vector2D B)
        {
            double value = A.X * B.X + A.Y * B.Y;
            return value;
        }

        /// <summary>
        /// Description:    static method, calculate the angle degree of vector A to vector B
        /// Date:           2013.9.18
        /// </summary>
        /// <returns>distance</returns>
        public static double Degree(Vector2D A, Vector2D B)
        {
            double AB = A.L * B.L;
            double AdotB = Dot(A, B);
            double q = AdotB / AB;

            // 有任何一向量為0，無需計算，回傳0
            if (AdotB == 0)
                return 0.0;

            // 兩向量幾乎成同方向的情形，直接回傳0
            if (Math.Abs(q - 1) < 0.000001)
                return 0.0;

            // 兩向量幾乎成反方向的情形，直接回傳180
            if (Math.Abs(q + 1) < 0.000001)
                return 180.0;

            double value = Math.Acos(q);
            value = MyFunc.RadianToDegree(value);
            return value;
        }
    };
    #endregion


    #region 3D點
    public class Point3D:IComparable<Point3D>

    {
        public const double InvalidX = 0.0;  //定義：無效之X 3D座標
        public const double InvalidY = 0.0;  //定義：無效之Y 3D座標
        public const double InvalidZ = 0.0;  //定義：無效之Z 3D座標

        public double X;
        public double Y;
        public double Z;

        public double R;
        public double G;
        public double B;

        public double Dt;   //記錄與上一點之距離
        public bool flag;   //備用的flag(default=false)
        public Point3D()
        {
            X = InvalidX;
            Y = InvalidY;
            Z = InvalidZ;
            Dt = 0.0;
            flag = false;
        }
        public Point3D(Point3D P)
        {
            X = P.X;
            Y = P.Y;
            Z = P.Z;
            R = P.R;
            G = P.G;
            B = P.B;
            Dt = P.Dt;
            flag = P.flag;
        }
        public Point3D(double x, double y, double z)
        {
            X = x;
            Y = y;
            Z = z;
            Dt = 0.0;
            flag = false;
        }

        public Point3D(double x, double y, double z, double r, double g, double b)
        {
            X = x;
            Y = y;
            Z = z;
            R = r;
            G = g;
            B = b;
        }

        public Point3D(double x, double y, double z, double dt)
        {
            X = x;
            Y = y;
            Z = z;
            Dt = dt;
            flag = false;
        }
        public Point3D(double x, double y, double z, double dt, bool flag)
        {
            X = x;
            Y = y;
            Z = z;
            Dt = dt;
            this.flag = flag;
        }

        /// <summary>
        /// 實作介面之座標比較。比較自己與其他點，判斷先後順序。先比較X值，再比較Y值，最後比較Z值。
        /// </summary>
        /// <returns>1:自己較大 -1:其他點較大 0:相等</returns>
        int IComparable<Point3D>.CompareTo(Point3D other)
        {
            if (this.X - other.X > double.Epsilon)
            {
                return 1;
            }
            else if (other.X - this.X > double.Epsilon)
            {
                return -1;
            }
            else
            {
                if (this.Y - other.Y > double.Epsilon)
                {
                    return 1;
                }
                else if (other.Y - this.Y > double.Epsilon)
                {
                    return -1;
                }
                else
                {
                    if (this.Z - other.Z > double.Epsilon)
                    {
                        return 1;
                    }
                    else if (other.Z - this.Z > double.Epsilon)
                    {
                        return -1;
                    }
                    return 0;
                }
            }
        }

        /// <summary>
        /// 座標比較子。比較兩個點，判斷先後順序。先比較Y值，再比較X值，最後比較Z值。
        /// </summary>
        /// <param name="A">輸入點A</param>
        /// <param name="B">輸入點B</param>
        /// <returns>1:A較大 -1:B較大 0:相等</returns>
        public static int ComparitorPoint3D(Point3D A, Point3D B)
        {
            if (A.Y - B.Y > double.Epsilon)
            {
                return 1;
            }
            else if (B.Y - A.Y > double.Epsilon)
            {
                return -1;
            }
            else
            {
                if (A.X - B.X > double.Epsilon)
                {
                    return 1;
                }
                else if (B.X - A.X > double.Epsilon)
                {
                    return -1;
                }
                else
                {
                    if (A.Z - B.Z > double.Epsilon)
                    {
                        return 1;
                    }
                    else if (B.Z - A.Z > double.Epsilon)
                    {
                        return -1;
                    }
                    return 0;
                }
            }
        }


        /// <summary>
        /// 座標比較子。比較兩個點的Z值，判斷先後順序。使用P.Sort()後，遞增排序
        /// </summary>
        /// <param name="A">輸入點A</param>
        /// <param name="B">輸入點B</param>
        /// <returns>1:A較大 -1:B較大 0:相等</returns>
        public static int ComparitorPoint3DZ(Point3D A, Point3D B)
        {
            if (A.Z - B.Z > double.Epsilon)
            {
                return 1;
            }
            else if (B.Z - A.Z > double.Epsilon)
            {
                return -1;
            }
            else
            {
                return 0;
            }
        }

        /// <summary>
        /// 座標比較子。比較兩個點的dt值，判斷先後順序。使用P.Sort()後，遞增排序
        /// </summary>
        /// <param name="A">輸入點A</param>
        /// <param name="B">輸入點B</param>
        /// <returns>1:A較大 -1:B較大 0:相等</returns>
        public static int ComparitorPoint3Ddt(Point3D A, Point3D B)
        {
            if (A.Dt - B.Dt > double.Epsilon)
            {
                return 1;
            }
            else if (B.Dt - A.Dt > double.Epsilon)
            {
                return -1;
            }
            else
            {
                return 0;
            }
        }

        /// <summary>
        /// 在點雲PCloud中搜尋與P點近似點的index
        /// </summary>
        /// <returns>P在PCloud中的index</returns>
        public static int FindIndex(List<Point3D> PCloud, Point3D P, double d_near_precision)
        {
            int index = 0;
            for (int i = 0; i < PCloud.Count; i++)
            {
                if (d_near_precision - Distance(PCloud[i], P) > double.Epsilon)
                {
                    index = i;
                    break;
                }

                if (i == PCloud.Count - 1)
                    index = -1;
            }
            return index;
        }

        /// <summary>
        /// 在點雲PCloud中找尋是否有與P點近似的點
        /// </summary>
        public static bool Contains(List<Point3D> PCloud, Point3D P, double d_near_precision)
        {
            bool blnContains = false;
            for (int i = 0; i < PCloud.Count; i++)
            {
                if (d_near_precision - Distance(PCloud[i], P) > double.Epsilon)
                {
                    blnContains = true;
                    break;
                }

                if (i == PCloud.Count - 1)
                    blnContains = false;
            }
            return blnContains;
        }

        /// <summary>
        /// 求兩點距離
        /// </summary>
        public static double Distance(Point3D A, Point3D B)
        {
            if (A.X == B.X && A.Y == B.Y && A.Z == B.Z)
                return 0.0;

            return Math.Sqrt(
                                (A.X - B.X) * (A.X - B.X) +
                                (A.Y - B.Y) * (A.Y - B.Y) +
                                (A.Z - B.Z) * (A.Z - B.Z)
                );
        }

        /// <summary>
        /// 求點P到平面 ax + by + cz + d = 0 的距離
        /// </summary>
        public static double Distance(Point3D P, double a, double b, double c, double d)
        {
            if (a == 0 && b == 0 && c == 0)
                return double.MaxValue;

            double H = a * P.X + b * P.Y + c * P.Z + d;
            if (H == 0)
                return 0.0;

            return Math.Abs(H) / Math.Sqrt(a * a + b * b + c * c);
        }

        /// <summary>
        /// 求點P到線段AB的距離
        /// </summary>
        public static double Distance(Point3D P, Point3D A, Point3D B)
        {
            Point3D PP = Point3D.ProjectPOnSegmentAB(P, A, B);
            double d = Distance(P, PP);
            return d;
        }

        /// <summary>
        /// 給一堆點，計算重心座標
        /// </summary>
        public static Point3D CenterOfGravity(List<Point3D> P)
        {
            if (P.Count == 0 || P == null)
                return null;

            Point3D COG = new Point3D();

            double SumX = 0.0;
            double SumY = 0.0;
            double SumZ = 0.0;
            for (int i = 0; i < P.Count; i++)
            {
                SumX += P[i].X;
                SumY += P[i].Y;
                SumZ += P[i].Z;
            }
            COG.X = SumX / P.Count;
            COG.Y = SumY / P.Count;
            COG.Z = SumZ / P.Count;

            return COG;
        }

        public static Point3D CenterOfGravity_25(List<Point3D> P, List<Point3D> P1, List<Point3D> P2, List<Point3D> P3, List<Point3D> P4, List<Point3D> P5, List<Point3D> P6, List<Point3D> P7, List<Point3D> P8, List<Point3D> P9, List<Point3D> P10, List<Point3D> P11, List<Point3D> P12, List<Point3D> P13, List<Point3D> P14, List<Point3D> P15, List<Point3D> P16, List<Point3D> P17, List<Point3D> P18, List<Point3D> P19, List<Point3D> P20, List<Point3D> P21, List<Point3D> P22, List<Point3D> P23, List<Point3D> P24)
        {
            if (P.Count == 0 || P == null)
                return null;

            Point3D COG = new Point3D();

            double SumX = 0.0;
            double SumY = 0.0;
            double SumZ = 0.0;

            double tempZ = 0.0;
            double maxZ = 0.0;

            for (int i = 0; i < P.Count; i++)
            {
                SumX += P[i].X;
                SumY += P[i].Y;
                SumZ += P[i].Z;
                //tempZ = P[i].Z;

                //if (tempZ > maxZ) 
                //{
                //    maxZ = tempZ;
                //}

            }
            COG.X = SumX / P.Count;
            COG.Y = SumY / P.Count;
            COG.Z = SumZ / P.Count;

            //COG.Z = maxZ;

            SumZ = 0.0;
            for (int i = 0; i < P1.Count; i++)
            {
                SumZ += P1[i].Z;
            }
            SumZ = SumZ / P1.Count;
            if (SumZ > COG.Z)
            {
                COG.Z = SumZ;
            }

            SumZ = 0.0;
            for (int i = 0; i < P2.Count; i++)
            {
                SumZ += P2[i].Z;
            }
            SumZ = SumZ / P2.Count;
            if (SumZ > COG.Z)
            {
                COG.Z = SumZ;
            }

            SumZ = 0.0;
            for (int i = 0; i < P3.Count; i++)
            {
                SumZ += P3[i].Z;
            }
            SumZ = SumZ / P3.Count;
            if (SumZ > COG.Z)
            {
                COG.Z = SumZ;
            }

            SumZ = 0.0;
            for (int i = 0; i < P4.Count; i++)
            {
                SumZ += P4[i].Z;
            }
            SumZ = SumZ / P4.Count;
            if (SumZ > COG.Z)
            {
                COG.Z = SumZ;
            }

            SumZ = 0.0;
            for (int i = 0; i < P5.Count; i++)
            {
                SumZ += P5[i].Z;
            }
            SumZ = SumZ / P5.Count;
            if (SumZ > COG.Z)
            {
                COG.Z = SumZ;
            }

            SumZ = 0.0;
            for (int i = 0; i < P6.Count; i++)
            {
                SumZ += P6[i].Z;
            }
            SumZ = SumZ / P6.Count;
            if (SumZ > COG.Z)
            {
                COG.Z = SumZ;
            }

            SumZ = 0.0;
            for (int i = 0; i < P7.Count; i++)
            {
                SumZ += P7[i].Z;
            }
            SumZ = SumZ / P7.Count;
            if (SumZ > COG.Z)
            {
                COG.Z = SumZ;
            }

            SumZ = 0.0;
            for (int i = 0; i < P8.Count; i++)
            {
                SumZ += P8[i].Z;
            }
            SumZ = SumZ / P8.Count;
            if (SumZ > COG.Z)
            {
                COG.Z = SumZ;
            }

            SumZ = 0.0;
            for (int i = 0; i < P9.Count; i++)
            {
                SumZ += P9[i].Z;
            }
            SumZ = SumZ / P9.Count;
            if (SumZ > COG.Z)
            {
                COG.Z = SumZ;
            }

            SumZ = 0.0;
            for (int i = 0; i < P10.Count; i++)
            {
                SumZ += P10[i].Z;
            }
            SumZ = SumZ / P10.Count;
            if (SumZ > COG.Z)
            {
                COG.Z = SumZ;
            }

            SumZ = 0.0;
            for (int i = 0; i < P11.Count; i++)
            {
                SumZ += P11[i].Z;
            }
            SumZ = SumZ / P11.Count;
            if (SumZ > COG.Z)
            {
                COG.Z = SumZ;
            }

            SumZ = 0.0;
            for (int i = 0; i < P12.Count; i++)
            {
                SumZ += P12[i].Z;
            }
            SumZ = SumZ / P12.Count;
            if (SumZ > COG.Z)
            {
                COG.Z = SumZ;
            }

            SumZ = 0.0;
            for (int i = 0; i < P13.Count; i++)
            {
                SumZ += P13[i].Z;
            }
            SumZ = SumZ / P13.Count;
            if (SumZ > COG.Z)
            {
                COG.Z = SumZ;
            }

            SumZ = 0.0;
            for (int i = 0; i < P14.Count; i++)
            {
                SumZ += P14[i].Z;
            }
            SumZ = SumZ / P14.Count;
            if (SumZ > COG.Z)
            {
                COG.Z = SumZ;
            }

            SumZ = 0.0;
            for (int i = 0; i < P15.Count; i++)
            {
                SumZ += P15[i].Z;
            }
            SumZ = SumZ / P15.Count;
            if (SumZ > COG.Z)
            {
                COG.Z = SumZ;
            }

            SumZ = 0.0;
            for (int i = 0; i < P16.Count; i++)
            {
                SumZ += P16[i].Z;
            }
            SumZ = SumZ / P16.Count;
            if (SumZ > COG.Z)
            {
                COG.Z = SumZ;
            }

            SumZ = 0.0;
            for (int i = 0; i < P17.Count; i++)
            {
                SumZ += P17[i].Z;
            }
            SumZ = SumZ / P17.Count;
            if (SumZ > COG.Z)
            {
                COG.Z = SumZ;
            }

            SumZ = 0.0;
            for (int i = 0; i < P18.Count; i++)
            {
                SumZ += P18[i].Z;
            }
            SumZ = SumZ / P18.Count;
            if (SumZ > COG.Z)
            {
                COG.Z = SumZ;
            }

            SumZ = 0.0;
            for (int i = 0; i < P19.Count; i++)
            {
                SumZ += P19[i].Z;
            }
            SumZ = SumZ / P19.Count;
            if (SumZ > COG.Z)
            {
                COG.Z = SumZ;
            }

            SumZ = 0.0;
            for (int i = 0; i < P20.Count; i++)
            {
                SumZ += P20[i].Z;
            }
            SumZ = SumZ / P20.Count;
            if (SumZ > COG.Z)
            {
                COG.Z = SumZ;
            }

            SumZ = 0.0;
            for (int i = 0; i < P21.Count; i++)
            {
                SumZ += P21[i].Z;
            }
            SumZ = SumZ / P21.Count;
            if (SumZ > COG.Z)
            {
                COG.Z = SumZ;
            }

            SumZ = 0.0;
            for (int i = 0; i < P22.Count; i++)
            {
                SumZ += P22[i].Z;
            }
            SumZ = SumZ / P22.Count;
            if (SumZ > COG.Z)
            {
                COG.Z = SumZ;
            }

            SumZ = 0.0;
            for (int i = 0; i < P23.Count; i++)
            {
                SumZ += P23[i].Z;
            }
            SumZ = SumZ / P23.Count;
            if (SumZ > COG.Z)
            {
                COG.Z = SumZ;
            }

            SumZ = 0.0;
            for (int i = 0; i < P24.Count; i++)
            {
                SumZ += P24[i].Z;
            }
            SumZ = SumZ / P24.Count;
            if (SumZ > COG.Z)
            {
                COG.Z = SumZ;
            }

            return COG;
        }

        /// <summary>
        /// 給一堆點，計算重心座標
        /// </summary>
        public static Point3D CenterOfGravity_9(List<Point3D> P, List<Point3D> P1, List<Point3D> P2, List<Point3D> P3, List<Point3D> P4, List<Point3D> P5, List<Point3D> P6, List<Point3D> P7, List<Point3D> P8)
        {
            if (P.Count == 0 || P == null)
                return null;

            Point3D COG = new Point3D();

            double SumX = 0.0;
            double SumY = 0.0;
            double SumZ = 0.0;
            for (int i = 0; i < P.Count; i++)
            {
                SumX += P[i].X;
                SumY += P[i].Y;
                SumZ += P[i].Z;
            }
            COG.X = SumX / P.Count;
            COG.Y = SumY / P.Count;
            COG.Z = SumZ / P.Count;

            SumZ = 0.0;
            for (int i = 0; i < P1.Count; i++)
            {
                SumZ += P1[i].Z;
            }
            SumZ = SumZ / P1.Count;
            if (SumZ > COG.Z) 
            {
                COG.Z = SumZ;
            }

            SumZ = 0.0;
            for (int i = 0; i < P2.Count; i++)
            {
                SumZ += P2[i].Z;
            }
            SumZ = SumZ / P2.Count;
            if (SumZ > COG.Z)
            {
                COG.Z = SumZ;
            }

            SumZ = 0.0;
            for (int i = 0; i < P3.Count; i++)
            {
                SumZ += P3[i].Z;
            }
            SumZ = SumZ / P3.Count;
            if (SumZ > COG.Z)
            {
                COG.Z = SumZ;
            }

            SumZ = 0.0;
            for (int i = 0; i < P4.Count; i++)
            {
                SumZ += P4[i].Z;
            }
            SumZ = SumZ / P4.Count;
            if (SumZ > COG.Z)
            {
                COG.Z = SumZ;
            }

            SumZ = 0.0;
            for (int i = 0; i < P5.Count; i++)
            {
                SumZ += P5[i].Z;
            }
            SumZ = SumZ / P5.Count;
            if (SumZ > COG.Z)
            {
                COG.Z = SumZ;
            }

            SumZ = 0.0;
            for (int i = 0; i < P6.Count; i++)
            {
                SumZ += P6[i].Z;
            }
            SumZ = SumZ / P6.Count;
            if (SumZ > COG.Z)
            {
                COG.Z = SumZ;
            }

            SumZ = 0.0;
            for (int i = 0; i < P7.Count; i++)
            {
                SumZ += P7[i].Z;
            }
            SumZ = SumZ / P7.Count;
            if (SumZ > COG.Z)
            {
                COG.Z = SumZ;
            }

            SumZ = 0.0;
            for (int i = 0; i < P8.Count; i++)
            {
                SumZ += P8[i].Z;
            }
            SumZ = SumZ / P8.Count;
            if (SumZ > COG.Z)
            {
                COG.Z = SumZ;
            }

            return COG;
        }

        /// <summary>
        /// 計算點P在線段AB上的投影點
        /// </summary>
        public static Point3D ProjectPOnSegmentAB(Point3D P, Point3D A, Point3D B)
        {
            Point3D PP = new Point3D();

            Vector3D AP = new Vector3D(A, P);
            Vector3D AB = new Vector3D(A, B);

            double AP_dot_AB = Vector3D.Dot(AP, AB);
            if (Math.Abs(AP_dot_AB) <= double.Epsilon)
            {
                PP.X = P.X;
                PP.Y = P.Y;
                PP.Z = P.Z;
            }
            else
            {
                double l = AP_dot_AB / AB.L;
                Vector3D uAB = AB.UnitVector();
                PP.X = A.X + l * uAB.X;
                PP.Y = A.Y + l * uAB.Y;
                PP.Z = A.Z + l * uAB.Z;
            }

            return PP;
        }

        /// <summary>
        /// 求點P到平面 ax + by + cz + d = 0 的投影點
        /// </summary>
        public static Point3D ProjectPOnPlan(Point3D P, double a, double b, double c, double d)
        {
            if (a == 0 && b == 0 && c == 0)
                return null;
            if (P == null)
                return null;

            Point3D projP = new Point3D();
            double H = a * P.X + b * P.Y + c * P.Z + d;
            if (H == 0)
            {
                projP.X = P.X;
                projP.Y = P.Y;
                projP.Z = P.Z;
                return projP;
            }

            double t = H / (a * a + b * b + c * c);

            projP.X = P.X - a * t;
            projP.Y = P.Y - b * t;
            projP.Z = P.Z - c * t;

            return projP;
        }

        /// <summary>
        /// 給一堆點，計算近似平面的單位法向量
        /// </summary>
        public static Vector3D FitPlanNormalUnitVector(List<Point3D> P)
        {
            if (P.Count < 4 || P == null)
                return null;

            Vector3D vNormal = new Vector3D();


            // ********************************************
            //              Least-Square Method
            // ********************************************
            double[,] arrLMethod = new double[3, 4];
            arrLMethod[0, 0] = P.Count;      // total points

            arrLMethod[0, 1] = 0;            // summation of x
            for (int i = 0; i < P.Count; i++)
                arrLMethod[0, 1] += P[i].X;

            arrLMethod[0, 2] = 0;            // summation of y
            for (int i = 0; i < P.Count; i++)
                arrLMethod[0, 2] += P[i].Y;

            arrLMethod[0, 3] = 0;            // summation of z
            for (int i = 0; i < P.Count; i++)
                arrLMethod[0, 3] += P[i].Z;

            arrLMethod[1, 0] = arrLMethod[0, 1];

            arrLMethod[1, 1] = 0;            // summation of x^2
            for (int i = 0; i < P.Count; i++)
                arrLMethod[1, 1] += P[i].X * P[i].X;

            arrLMethod[1, 2] = 0;            // summation of x*y
            for (int i = 0; i < P.Count; i++)
                arrLMethod[1, 2] += P[i].X * P[i].Y;

            arrLMethod[1, 3] = 0;            // summation of x*z
            for (int i = 0; i < P.Count; i++)
                arrLMethod[1, 3] += P[i].X * P[i].Z;

            arrLMethod[2, 0] = arrLMethod[0, 2];
            arrLMethod[2, 1] = arrLMethod[1, 2];

            arrLMethod[2, 2] = 0;            // summation of y^2
            for (int i = 0; i < P.Count; i++)
                arrLMethod[2, 2] += P[i].Y * P[i].Y;

            arrLMethod[2, 3] = 0;            // summation of y*z
            for (int i = 0; i < P.Count; i++)
                arrLMethod[2, 3] += P[i].Y * P[i].Z;

            double[] arrXYZ = MyFunc.Solve3x3Equation(arrLMethod);

            //3D Least Square 最佳化的 平面是 Z = arrXYZ[0] + arrXYZ[1]*X + arrXYZ[2]*Y
            //                         也就是 arrXYZ[1]*X + arrXYZ[2]*Y - Z = - arrXYZ[0]
            // now we want to normalize {arrayXYZ[1], arrayXYZ[2], -1} as our transformation angle
            double dblLength = Math.Sqrt(arrXYZ[1] * arrXYZ[1] + arrXYZ[2] * arrXYZ[2] + 1);
            if (arrXYZ[1] > 0)
            {
                arrXYZ[0] = arrXYZ[1] / dblLength;
                arrXYZ[1] = arrXYZ[2] / dblLength;
                arrXYZ[2] = -1 / dblLength;
            }
            else
            {
                arrXYZ[0] = -arrXYZ[1] / dblLength;
                arrXYZ[1] = -arrXYZ[2] / dblLength;
                arrXYZ[2] = 1 / dblLength;
            }

            vNormal.X = arrXYZ[0];
            vNormal.Y = arrXYZ[1];
            vNormal.Z = arrXYZ[2];
            return vNormal;
        }

    };

    #endregion

    #region 3D向量

    public class Vector3D
    {
        private double x;
        //X分量
        public double X
        {
            get { return x; }
            set { x = value; }
        }

        private double y;
        //Y分量
        public double Y
        {
            get { return y; }
            set { y = value; }
        }

        private double z;
        //Z分量
        public double Z
        {
            get { return z; }
            set { z = value; }
        }

        private double l;
        //向量長度
        public double L
        {
            get
            {
                l = Length();
                return l;
            }
        }

        // 計算向量長度
        private double Length()
        {
            double value = Math.Sqrt(Math.Pow(x, 2) + Math.Pow(y, 2) + Math.Pow(z, 2));
            return value;
        }

        // 回傳單位向量
        public Vector3D UnitVector()
        {
            Vector3D v_unit = new Vector3D();
            if (L != 0)
            {
                v_unit.X = x / l;
                v_unit.Y = y / l;
                v_unit.Z = z / l;
            }
            return v_unit;
        }

        // 0向量建構
        public Vector3D()
        {
            x = 0.0;
            y = 0.0;
            z = 0.0;
        }
        // 直接指定分量建構
        public Vector3D(double x, double y, double z)
        {
            this.x = x;
            this.y = y;
            this.z = z;
        }
        // 指定起點與終點建構
        public Vector3D(Point3D startP, Point3D endP)
        {
            x = endP.X - startP.X;
            y = endP.Y - startP.Y;
            z = endP.Z - startP.Z;
        }

        /// <summary>
        /// 計算向量A+B
        /// </summary>
        /// <returns>向量</returns>
        public static Vector3D Add(Vector3D A, Vector3D B)
        {
            Vector3D v = new Vector3D();
            v.X = A.X + B.X;
            v.Y = A.Y + B.Y;
            v.Z = A.Z + B.Z;
            return v;
        }

        /// <summary>
        /// 計算向量A-B
        /// </summary>
        /// <returns>向量</returns>
        public static Vector3D Sub(Vector3D A, Vector3D B)
        {
            Vector3D v = new Vector3D();
            v.X = A.X - B.X;
            v.Y = A.Y - B.Y;
            v.Z = A.Z - B.Z;
            return v;
        }

        /// <summary>
        /// 計算 純量t * 向量A
        /// </summary>
        /// <returns>向量</returns>
        public static Vector3D Multiple(Vector3D A, double t)
        {
            Vector3D v = new Vector3D();
            v.X = t * A.X;
            v.Y = t * A.Y;
            v.Z = t * A.Z;
            return v;
        }

        /// <summary>
        /// Description:    static method, calculate the dot product of vector A and vector B
        /// Date:           2013.9.18
        /// </summary>
        /// <returns>distance</returns>
        public static double Dot(Vector3D A, Vector3D B)
        {
            double value = A.X * B.X + A.Y * B.Y + A.Z * B.Z;
            return value;
        }

        public static Vector3D Cross(Vector3D A, Vector3D B)
        {
            Vector3D v = new Vector3D();
            v.X = A.Y * B.Z - A.Z * B.Y;
            v.Y = A.Z * B.X - A.X * B.Z;
            v.Z = A.X * B.Y - A.Y * B.X;
            return v;
        }

        /// <summary>
        /// Description:    static method, calculate the angle degree of vector A to vector B
        /// Date:           2013.9.18
        /// </summary>
        /// <returns>distance</returns>
        public static double Degree(Vector3D A, Vector3D B)
        {
            double AB = A.L * B.L;
            double AdotB = Dot(A, B);
            double q = AdotB / AB;

            // 有任何一向量為0，無需計算，回傳0
            if (AdotB == 0)
                return 0.0;

            // 避免計算兩向量幾乎平行的情形，直接回傳0
            if ((1 - Math.Abs(q)) < 0.000001)
                return 0.0;

            double value = Math.Acos(q);
            value = MyFunc.RadianToDegree(value);
            return value;
        }

    };
    #endregion

    #region 3D機台路徑應用

    /// <summary>
    /// 定義X Y Z Vx Vy Vz 六軸運動路徑之點座標及方位
    /// </summary>
    public class Position3D
    {
        public double X = 0.0;
        public double Y = 0.0;
        public double Z = 0.0;
        public double Vx = 0.0;
        public double Vy = 0.0;
        public double Vz = 0.0;
        public Position3D()
        {
            X = 0.0;
            Y = 0.0;
            Z = 0.0;
            Vx = 0.0;
            Vy = 0.0;
            Vz = 0.0;
        }

        public Position3D(Position3D p)
        {
            X = p.X;
            Y = p.Y;
            Z = p.Z;
            Vx = p.Vx;
            Vy = p.Vy;
            Vz = p.Vz;
        }
        public Position3D(double x, double y, double z, double vx, double vy, double vz)
        {
            X = x;
            Y = y;
            Z = z;
            Vx = vx;
            Vy = vy;
            Vz = vz;
        }
    };
    

    /// <summary>
    /// 定義ScanTable X Y Z R 四軸運動平台之各軸位置
    /// </summary>
    public class ScanTablePos
    {
        public double X = 0.0;
        public double Y = 0.0;
        public double Z = 0.0;
        public double R = 0.0;
        public bool blnTrigger;
        public ScanTablePos()
        {
            X = 0.0;
            Y = 0.0;
            Z = 0.0;
            R = 0.0;
            blnTrigger = false;
        }
        public ScanTablePos(double x, double y, double z, double r, bool trigger)
        {
            X = x;
            Y = y;
            Z = z;
            R = r;
            blnTrigger = trigger;
        }
    };
    #endregion
}
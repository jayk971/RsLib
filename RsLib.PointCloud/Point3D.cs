using Accord.Math;
using RsLib.Common;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Text;
namespace RsLib.PointCloudLib
{
    [Serializable]
    public partial class Point3D : Object3D, IComparable<Point3D>

    {

        public Point3 P => new Point3((float)X, (float)Y, (float)Z);
        public double[] PArray => new double[] { X, Y, Z };
        public override uint DataCount => 1;

        public double X = 0.0;

        public double Y = 0.0;
        public double Z = 0.0;


        public static Point3D Orig => new Point3D(0, 0, 0);
        public static Point3D MaxValue => new Point3D(double.MaxValue, double.MaxValue, double.MaxValue);
        public static Point3D MinValue => new Point3D(double.MinValue, double.MinValue, double.MinValue);

        public Color Color = Color.DimGray;
        public double Dt;   //記錄與上一點之距離或備用於其他演算法        
        public bool flag;           //備用的flag(default=false)
        public int tag = 0;
        public int tag1 = 0;

        public Point3D()
        {
            Dt = 0.0;
            flag = false;
            tag = 0;
            Color = Color.DimGray;
        }
        public Point3D(Point3D P)
        {
            X = P.X;
            Y = P.Y;
            Z = P.Z;
            Dt = P.Dt;
            flag = P.flag;
            tag = P.tag;
            tag1 = P.tag1;
            Color = P.Color;
        }

        public Point3D(Point3D BasePoint, Vector3D ExtendVector, double Dis)
        {
            ExtendVector.UnitVector();
            X = BasePoint.X + ExtendVector.X * Dis;
            Y = BasePoint.Y + ExtendVector.Y * Dis;
            Z = BasePoint.Z + ExtendVector.Z * Dis;
            Dt = 0.0;
            flag = false;
            tag = BasePoint.tag;
            tag1 = BasePoint.tag1;
            Color = BasePoint.Color;
        }
        public Point3D(double x, double y, double z)
        {
            X = x;
            Y = y;
            Z = z;
            Dt = 0.0;
            flag = false;
            tag = 0;
            tag1 = 0;
            Color = Color.DimGray;
        }
        public Point3D(double x, double y, double z, double dt)
        {
            X = x;
            Y = y;
            Z = z;
            Dt = dt;
            flag = false;
            tag = 0;
            tag1 = 0;

            Color = Color.DimGray;
        }
        public Point3D(double[] data, double dt)
        {
            X = data[0];
            Y = data[1];
            Z = data[2];
            Dt = dt;
            flag = false;
            tag = 0;
            tag1 = 0;

            Color = Color.DimGray;
        }
        public Point3D(double[] data)
        {
            X = data[0];
            Y = data[1];
            Z = data[2];
            Dt = 0;
            flag = false;
            tag = 0;
            tag1 = 0;

            Color = Color.DimGray;
        }

        //public Point3D(double x, double y, double z, double dt, bool flagModified, bool flag)
        //{
        //    X = x;
        //    Y = y;
        //    Z = z;
        //    Dt = dt;
        //    this.flag = flag;
        //}
        public Point3D(double x, double y, double z, Point3D CopyData)
        {
            X = x;
            Y = y;
            Z = z;
            Dt = CopyData.Dt;
            flag = CopyData.flag;
            tag = CopyData.tag;
            tag1 = CopyData.tag1;

            Color = CopyData.Color;
        }
        public string ToString(string spliter = ",")
        {
            return $"{X:F3}{spliter}{Y:F3}{spliter}{Z:F3}";
        }
        public void SetXYZ(double x, double y, double z)
        {
            X = x;
            Y = y;
            Z = z;
        }
        public static Point3D operator *(double t , Point3D p)
        {
            return new Point3D(t * p.X, t * p.Y, t * p.Z);
        }
        public static Point3D operator *(Point3D p, double t)
        {
            return new Point3D(t * p.X, t * p.Y, t * p.Z);
        }
        public static Point3D operator *(float t, Point3D p)
        {
            return new Point3D(t * p.X, t * p.Y, t * p.Z);
        }
        public static Point3D operator *(Point3D p, float t)
        {
            return new Point3D(t * p.X, t * p.Y, t * p.Z);
        }
        public static Point3D operator -(Point3D p1, Point3D p2)
        {
            return new Point3D(p1.X - p2.X, p1.Y - p2.Y, p1.Z - p2.Z);
        }
        public static Point3D operator +(Point3D p1, Point3D p2)
        {
            return new Point3D(p1.X + p2.X, p1.Y + p2.Y, p1.Z + p2.Z);
        }
        public static Point3D operator /(Point3D p, double t)
        {
            return new Point3D(p.X/t,  p.Y/t, p.Z/t);
        }
        public static Point3D operator /(Point3D p, float t)
        {
            return new Point3D(p.X / t, p.Y / t, p.Z / t);
        }
        public void SetXYZ(Point3D p)
        {
            X = p.X;
            Y = p.Y;
            Z = p.Z;
        }
        public Point3D PositionXY()
        {
            return new Point3D(X, Y, 0.0);
        }
        public void Save(string filePath)
        {
            using (StreamWriter sw = new StreamWriter(filePath, false, Encoding.Default))
            {
                sw.WriteLine($"{X} {Y} {Z}");
                sw.Flush();
            }
        }
        public double Distance(Point3D Target)
        {
            double x = X - Target.X;
            double y = Y - Target.Y;
            double z = Z - Target.Z;
            return Math.Sqrt(Math.Pow(x, 2) + Math.Pow(y, 2) + Math.Pow(z, 2));
        }
        /// <summary>
        /// 實作介面之座標比較。比較自己與其他點，判斷先後順序。先比較Y值，再比較X值，最後比較Z值。
        /// </summary>
        /// <returns>1:自己較大 -1:其他點較大 0:相等</returns>
        int IComparable<Point3D>.CompareTo(Point3D other)
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

        public static bool IsEqualInt(Point3D A, Point3D B)
        {
            int ax = (int)A.X;
            int ay = (int)A.Y;
            int az = (int)A.Z;
            int bx = (int)B.X;
            int by = (int)B.Y;
            int bz = (int)B.Z;

            if (ax == bx && ay == by && az == bz)
                return true;

            return false;

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
        /// 座標比較子。比較平面兩個點根據中心點的順時針先後順序
        /// </summary>
        /// <param name="A">輸入點A</param>
        /// <param name="B">輸入點B</param>
        /// <returns> true若A小於B </returns>
        public static bool CompareCWValue(Point3D A, Point3D B, Point3D C)
        {
            if (A.X - C.X >= 0 && B.X - C.X < 0)
                return true;
            if (A.X - C.X < 0 && B.X - C.X >= 0)
                return false;
            if (A.X - C.X == 0 && B.X - C.X == 0)
            {
                if (A.Y - C.Y >= 0 || B.Y - C.Y >= 0)
                    return A.Y > B.Y;
                return B.Y > A.Y;
            }

            // compute the cross product of vectors (center -> a) x (center -> b)
            double det = (A.X - C.X) * (B.Y - C.Y) - (B.X - C.X) * (A.Y - C.Y);
            if (det < 0)
                return true;
            if (det > 0)
                return false;

            // points a and b are on the same line from the center
            // check which point is closer to the center
            double d1 = (A.X - C.X) * (A.X - C.X) + (A.Y - C.Y) * (A.Y - C.Y);
            double d2 = (B.X - C.X) * (B.X - C.X) + (B.Y - C.Y) * (B.Y - C.Y);
            return d1 > d2;
        }

        public static Point3D Center(Point3D p1, Point3D p2)
        {
            return new Point3D((p1.X + p2.X) / 2, (p1.Y + p2.Y) / 2, (p1.Z + p2.Z) / 2);
        }

        public static Point3D Multiply(Point3D pt, List<CoordMatrix> matrics)
        {
            Point3D outPt = new Point3D(pt);
            Matrix4x4 m = Matrix4x4.Identity;
            for (int i = 0; i < matrics.Count; i++)
            {
                matrics[i].EndAddMatrix();
                m = Matrix4x4.Multiply(matrics[i].FinalMatrix4,m);
            }
            Vector4 output = Matrix4x4.Multiply(m, new Vector4((float)pt.X, (float)pt.Y, (float)pt.Z, 1f));
            outPt.X = output.X;
            outPt.Y = output.Y;
            outPt.Z = output.Z;

            return outPt;
        }
        public static Point3D Multiply(Point3D pt, Matrix4x4 matrix)
        {
            Point3D outPt = new Point3D(pt);

            Vector4 output = Matrix4x4.Multiply(matrix, new Vector4((float)pt.X, (float)pt.Y, (float)pt.Z, 1f));
            outPt.X = output.X;
            outPt.Y = output.Y;
            outPt.Z = output.Z;

            return outPt;
        }
        public static Point3D Multiply(Point3D pt, double[,] matrixArr)
        {
            Point3D outPt = new Point3D(pt);

            Matrix4x4 matrix = PointCloudCommon.ArrayToMatrix4x4(matrixArr);

            Vector4 output = Matrix4x4.Multiply(matrix, new Vector4((float)pt.X, (float)pt.Y, (float)pt.Z, 1f));
            outPt.X = output.X;
            outPt.Y = output.Y;
            outPt.Z = output.Z;

            return outPt;
        }
        public Point3D Multiply(Matrix4x4 matrix)
        {
            Point3D outPt = new Point3D(this);
            Vector4 v = new Vector4((float)X, (float)Y, (float)Z, 1f);
            Vector4 output = matrix * v;

           
            outPt.X = output.X;
            outPt.Y = output.Y;
            outPt.Z = output.Z;

            return outPt;
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
        /// 求線段p1-p2-p3的最小夾角
        /// </summary>
        public static double Degree(Point3D p1, Point3D p2, Point3D p3)
        {
            Vector3D v_p2_to_p1 = new Vector3D(p2, p1);
            Vector3D v_p2_to_p3 = new Vector3D(p2, p3);
            double degree = Vector3D.Degree(v_p2_to_p1, v_p2_to_p3);

            return degree;
        }

        /// <summary>
        /// 求點P到線段AB的距離
        /// </summary>
        public static double Distance(Point3D P, Point3D A, Point3D B)
        {
            Point3D PP = ProjectPOnSegmentAB(P, A, B);
            double d = Distance(P, PP);
            return d;
        }

        public static bool SameSide(Point3D p1, Point3D p2, Point3D a, Point3D b)
        {
            Vector3D v1 = new Vector3D(b.X - a.X, b.Y - a.Y, b.Z - a.Z);
            Vector3D v2 = new Vector3D(p1.X - a.X, p1.Y - a.Y, p1.Z - a.Z);
            Vector3D v3 = new Vector3D(p2.X - a.X, p2.Y - a.Y, p2.Z - a.Z);

            Vector3D cp1 = Vector3D.Cross(v1, v2);
            Vector3D cp2 = Vector3D.Cross(v1, v3);
            double dot = Vector3D.Dot(cp1, cp2);
            if (dot >= 0)
                return true;
            else
                return false;
        }

        public static bool PointInTriangle(Point3D p, Point3D a, Point3D b, Point3D c)
        {
            if (SameSide(p, a, b, c) && SameSide(p, b, a, c) && SameSide(p, c, a, b))
                return true;
            else
                return false;
        }


        // Given three colinear points p, q, r, the function checks if
        // point q lies on line segment 'pr'
        public static bool OnSegment(Point3D p, Point3D q, Point3D r)
        {
            if (q.X <= Math.Max(p.X, r.X) && q.X >= Math.Min(p.X, r.X) &&
                q.Y <= Math.Max(p.Y, r.Y) && q.Y >= Math.Min(p.Y, r.Y))
                return true;

            return false;
        }

        // To find orientation of ordered triplet (p, q, r).
        // The function returns following values
        // 0 ‐‐> p, q and r are colinear
        // 1 ‐‐> Clockwise
        // 2 ‐‐> Counterclockwise
        public static int Orientation(Point3D p, Point3D q, Point3D r)
        {
            // See http://www.geeksforgeeks.org/orientation‐3‐ordered‐points/
            // for details of below formula.

            double y1 = q.Y - p.Y;
            double x1 = r.X - q.X;
            double x2 = q.X - p.X;
            double y2 = r.Y - q.Y;

            int val = (int)(y1 * x1 - x2 * y2);

            if (val == 0)
                return 0; // colinear

            return (val > 0) ? 1 : 2; // clock or counterclock wise
        }

        // The main function that returns true if line segment 'p1q1'
        // and 'p2q2' intersect.
        public static bool DoIntersect(Point3D p1, Point3D q1, Point3D p2, Point3D q2)
        {
            // Find the four orientations needed for general and
            // special cases
            int o1 = Orientation(p1, q1, p2);
            int o2 = Orientation(p1, q1, q2);
            int o3 = Orientation(p2, q2, p1);
            int o4 = Orientation(p2, q2, q1);
            // General case
            if (o1 != o2 && o3 != o4)
                return true;
            // Special Cases
            // p1, q1 and p2 are colinear and p2 lies on segment p1q1
            if (o1 == 0 && OnSegment(p1, p2, q1))
                return true;
            // p1, q1 and p2 are colinear and q2 lies on segment p1q1
            if (o2 == 0 && OnSegment(p1, q2, q1))
                return true;
            // p2, q2 and p1 are colinear and p1 lies on segment p2q2
            if (o3 == 0 && OnSegment(p2, p1, q2))
                return true;
            // p2, q2 and q1 are colinear and q1 lies on segment p2q2
            if (o4 == 0 && OnSegment(p2, q1, q2))
                return true;

            return false; // Doesn't fall in any of the above cases
        }



        /// <summary>
        /// 計算兩線段是否有交點
        /// </summary>
	    public static bool LineSegmentIntersec(KeyValuePair<Point3D, Point3D> line1, KeyValuePair<Point3D, Point3D> line2, out Point3D p)
        {
            Point3D p0 = line1.Key;
            Point3D p1 = line1.Value;

            Point3D p2 = line2.Key;
            Point3D p3 = line2.Value;

            double s02_x, s02_y, s10_x, s10_y, s32_x, s32_y, s_numer, t_numer, denom, t;
            s10_x = p1.X - p0.X;
            s10_y = p1.Y - p0.Y;
            s32_x = p3.X - p2.X;
            s32_y = p3.Y - p2.Y;

            denom = s10_x * s32_y - s32_x * s10_y;

            p = new Point3D();

            if (denom == 0)
            {
                p = new Point3D((p1.X + p0.X) / 2, (p1.Y + p0.Y) / 2, 0);
                return true; // Collinear
            }

            bool denomPositive = denom > 0;

            s02_x = p0.X - p2.X;
            s02_y = p0.Y - p2.Y;

            s_numer = s10_x * s02_y - s10_y * s02_x;

            if ((s_numer < 0) == denomPositive)
                return false; // No collision

            t_numer = s32_x * s02_y - s32_y * s02_x;
            if ((t_numer < 0) == denomPositive)
                return false; // No collision

            if (((s_numer > denom) == denomPositive) || ((t_numer > denom) == denomPositive))
                return false; // No collision
            // Collision detected
            t = t_numer / denom;

            p.X = p0.X + (t * s10_x);

            p.Y = p0.Y + (t * s10_y);

            return true;
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
                Vector3D uAB = AB.GetUnitVector();
                PP.X = A.X + l * uAB.X;
                PP.Y = A.Y + l * uAB.Y;
                PP.Z = A.Z + l * uAB.Z;
            }

            return PP;
        }


        /// <summary>
        /// 給三個頂點，計算平面的單位法向量(方向為P0->P1->P2右手定則)
        /// </summary>
        public static Vector3D PlaneNormalUnitVector(Point3D P0, Point3D P1, Point3D P2)
        {
            Vector3D vNormal = new Vector3D();
            vNormal.X = 0.0;
            vNormal.Y = 0.0;
            vNormal.Z = 0.0;
            if (P0 == null || P1 == null || P2 == null)
                return vNormal;

            Vector3D vA = new Vector3D(P0, P1);
            Vector3D vB = new Vector3D(P1, P2);
            Vector3D vAxB = Vector3D.Cross(vA, vB);

            if (vAxB.L < 0.00000001) return vNormal;

            vNormal.X = vAxB.X;
            vNormal.Y = vAxB.Y;
            vNormal.Z = vAxB.Z;
            return vNormal.GetUnitVector();
        }

        /// <summary>
        /// 給三個點，計算中間那點的曲率
        /// </summary>
        public static double CurvatureOfPoint(Point3D p0, Point3D p1, Point3D p2)
        {
            Vector3D normal = PlaneNormalUnitVector(p0, p1, p2);

            RsPlane plane = new RsPlane(normal, p1);

            Vector3D x_axis = new Vector3D(p1, p0);
            Vector3D y_axis = new Vector3D();

            y_axis = Vector3D.Cross(x_axis, normal).GetUnitVector();

            Point3D projected_p0 = new Point3D();
            Point3D projected_p2 = new Point3D();

            Point3D final_projected_p0 = new Point3D();
            Point3D final_projected_p2 = new Point3D();

            projected_p0.X = p0.X - p1.X;
            projected_p0.Y = p0.Y - p1.Y;
            projected_p0.Z = p0.Z - p1.Z;

            projected_p2.X = p2.X - p0.X;
            projected_p2.Y = p2.Y - p0.Y;
            projected_p2.Z = p2.Z - p0.Z;

            final_projected_p0.X = projected_p0.X * x_axis.X + projected_p0.Y * x_axis.Y + projected_p0.Z * x_axis.Z;
            final_projected_p0.Y = projected_p0.X * y_axis.X + projected_p0.Y * y_axis.Y + projected_p0.Z * y_axis.Z;
            final_projected_p0.Z = 0;

            final_projected_p2.X = projected_p2.X * x_axis.X + projected_p2.Y * x_axis.Y + projected_p2.Z * x_axis.Z;
            final_projected_p2.Y = projected_p2.X * y_axis.X + projected_p2.Y * y_axis.Y + projected_p2.Z * y_axis.Z;
            final_projected_p2.Z = 0;

            // 算出 origin, final p1, final p2 的外接圓半徑
            double a = Point3D.Distance(new Point3D(0, 0, 0), final_projected_p0);

            double b = Point3D.Distance(new Point3D(0, 0, 0), final_projected_p2);

            double c = Point3D.Distance(final_projected_p2, final_projected_p0);

            double s = (a + b + c) / 2;

            double r = a * b * c / 4 / Math.Sqrt(s * (s - a) * (s - b) * (s - c));

            double curvature = 1 / r;

            return curvature;
        }

        public static Point3D FindClosetPointFromOtherLine2D(Point3D find, ref List<Point3D> toFind)
        {
            double length = double.MaxValue;
            int index = 0;

            for (int i = 0; i < toFind.Count; i++)
            {

                double dist = Point3D.Distance(
                    new Point3D(toFind[i].X, toFind[i].Y, 0), new Point3D(find.X, find.Y, 0)
                    );

                if (dist < length)
                {
                    length = dist;
                    index = i;
                }
            }

            return toFind[index];

        }


        public static Point3D FindClosetPointFromOtherLine(Point3D find, ref List<Point3D> toFind)
        {
            double length = double.MaxValue;
            int index = 0;

            for (int i = 0; i < toFind.Count; i++)
            {

                double dist = Point3D.Distance(toFind[i], find);

                if (dist < length)
                {
                    length = dist;
                    index = i;
                }
            }

            return toFind[index];

        }

        public static List<KeyValuePair<Point3D, double>> GetPathCurvatureList(List<Point3D> profile)
        {
            List<KeyValuePair<Point3D, double>> curvatures = new List<KeyValuePair<Point3D, double>>();

            if (profile.Count >= 3)
            {
                for (int i = 1; i < profile.Count - 1; i++)
                {
                    int index1 = i - 1;
                    int index2 = i + 1;

                    Point3D p0 = profile[index1];
                    Point3D p1 = profile[i];
                    Point3D p2 = profile[index2];

                    double curvature = Point3D.CurvatureOfPoint(p0, p1, p2);

                    if (i == 1)
                    {
                        curvatures.Add(new KeyValuePair<Point3D, double>(p0, curvature));
                    }

                    curvatures.Add(new KeyValuePair<Point3D, double>(p1, curvature));

                    if (i == profile.Count - 2)
                    {
                        curvatures.Add(new KeyValuePair<Point3D, double>(p2, curvature));
                    }

                }
            }

            return curvatures;
        }

        public static Point3D FindBitelineCloestPoint(List<Point3D> toFind, RsPlane find, Point3D refPoint)
        {
            List<KeyValuePair<int, double>> cannidate = new List<KeyValuePair<int, double>>();

            // bubble sort
            for (int i = 0; i < toFind.Count; i++)
            {
                double dis1 = find.Distance(toFind[i]);
                if (dis1 < 0.1)
                    cannidate.Add(new KeyValuePair<int, double>(i, dis1));
            }

            int index = 0;

            double length = double.MaxValue;

            for (int i = 0; i < cannidate.Count; i++)
            {
                double dist = Point3D.Distance(refPoint, toFind[cannidate[i].Key]);
                if (dist < length)
                {
                    length = dist;
                    index = cannidate[i].Key;
                }
            }

            return toFind[index];
        }
        public string ToString(bool WriteTag, bool WriteFlag, bool WriteDt)
        {
            string Coord = string.Format("{0:F2} {1:F2} {2:F2}", X, Y, Z);
            if (WriteTag) Coord += string.Format(" {0}", tag);
            if (WriteFlag) Coord += string.Format(" {0}", flag);
            if (WriteDt) Coord += string.Format(" {0}", Dt);

            return Coord;
        }
        public string ToStringWithColor(bool WriteTag, bool WriteFlag, bool WriteDt)
        {
            string Coord = string.Format("{0:F2} {1:F2} {2:F2} {3} {4} {5}", X, Y, Z,Color.R,Color.G,Color.B);
            if (WriteTag) Coord += string.Format(" {0}", tag);
            if (WriteFlag) Coord += string.Format(" {0}", flag);
            if (WriteDt) Coord += string.Format(" {0}", Dt);

            return Coord;
        }

    }
    [Serializable]
    public partial class PointV3D : Point3D
    {
        public Vector3D Vx { get; set; }
        public Vector3D Vy { get; set; }
        public Vector3D Vz { get; set; }
        public Point3D Position
        {
            get
            {
                return new Point3D(X, Y, Z);
            }
        }
        public new Point3D PositionXY
        {
            get
            {
                return new Point3D(X, Y, 0.0);
            }
        }
        public PointV3D()
        {
            Vx = Vector3D.XAxis;
            Vy = Vector3D.YAxis;
            Vz = Vector3D.ZAxis;

            X = 0.0;
            Y = 0.0;
            Z = 0.0;
            Dt = 0.0;
            flag = false;
        }

        public PointV3D(Point3D src)
        {
            Vx = new Vector3D(1, 0, 0);
            Vy = new Vector3D(0, 1, 0);
            Vz = new Vector3D(0, 0, 1);

            X = src.X;
            Y = src.Y;
            Z = src.Z;
            Dt = src.Dt;
            flag = src.flag;

            AddOption(src.Options);
        }
        public PointV3D(Point3D src, Vector3D zVecSrc)
        {
            Vx = new Vector3D(0, 0, 0);
            Vy = new Vector3D(0, 0, 0);
            Vz = zVecSrc.DeepClone();

            X = src.X;
            Y = src.Y;
            Z = src.Z;
            Dt = src.Dt;
            flag = src.flag;

            AddOption(src.Options);
        }
        public PointV3D(Point3D src, Vector3D vx, Vector3D vy, Vector3D vz)
        {
            Vx = vx.DeepClone();
            Vy = vy.DeepClone();
            Vz = vz.DeepClone();

            X = src.X;
            Y = src.Y;
            Z = src.Z;
            Dt = src.Dt;
            flag = src.flag;

            AddOption(src.Options);
        }

        public PointV3D(PointV3D src)
        {
            Vx = new Vector3D(src.Vx.X, src.Vx.Y, src.Vx.Z);
            Vy = new Vector3D(src.Vy.X, src.Vy.Y, src.Vy.Z);
            Vz = new Vector3D(src.Vz.X, src.Vz.Y, src.Vz.Z);

            X = src.X;
            Y = src.Y;
            Z = src.Z;
            Dt = src.Dt;
            flag = src.flag;

            AddOption(src.Options);

        }

        public static PointV3D Multiply(PointV3D pt, List<CoordMatrix> matrics)
        {
            PointV3D outPt = new PointV3D(pt);
            Matrix4x4 m = Matrix4x4.Identity;
            for (int i = 0; i < matrics.Count; i++)
            {
                matrics[i].EndAddMatrix();
                m =Matrix4x4.Multiply( matrics[i].FinalMatrix4,m);
            }
            Vector4 output = Matrix4x4.Multiply(m, new Vector4((float)pt.X, (float)pt.Y, (float)pt.Z, 1f));
            outPt.X = output.X;
            outPt.Y = output.Y;
            outPt.Z = output.Z;

            return outPt;
        }
        public static PointV3D Multiply(PointV3D pt, Matrix4x4 matrix)
        {
            PointV3D outPt = new PointV3D(pt);

            Vector4 output = Matrix4x4.Multiply(matrix, new Vector4((float)pt.X, (float)pt.Y, (float)pt.Z, 1f));
            outPt.X = output.X;
            outPt.Y = output.Y;
            outPt.Z = output.Z;

            return outPt;
        }
        public static PointV3D Multiply(PointV3D pt, double[,] matrixArr)
        {
            PointV3D outPt = new PointV3D(pt);

            Matrix4x4 matrix = PointCloudCommon.ArrayToMatrix4x4(matrixArr);
            Vector4 output = Matrix4x4.Multiply(matrix, new Vector4((float)pt.X, (float)pt.Y, (float)pt.Z, 1f));
            outPt.X = output.X;
            outPt.Y = output.Y;
            outPt.Z = output.Z;

            return outPt;
        }
        public void Save(string FilePath, bool IsAppend)
        {
            using (StreamWriter sw = new StreamWriter(FilePath, IsAppend, Encoding.Default))
            {
                string WriteData = string.Format("{0} {1} {2} {3} {4} {5} {6} {7} {8} {9} {10} {11}", X, Y, Z, Vx.X, Vx.Y, Vx.Z, Vy.X, Vy.Y, Vy.Z, Vz.X, Vz.Y, Vz.Z);

                sw.WriteLine(WriteData);
                sw.Flush();
                sw.Close();
            }
        }
        public void Load(string RowData)
        {
            string[] SplitData = RowData.Split(' ');
            if (SplitData.Length != 12) return;
            double tmp = -99;

            tmp = -99;
            double.TryParse(SplitData[0], out tmp);
            X = tmp;
            tmp = -99;
            double.TryParse(SplitData[1], out tmp);
            Y = tmp;
            tmp = -99;
            double.TryParse(SplitData[2], out tmp);
            Z = tmp;

            tmp = -99;
            double.TryParse(SplitData[3], out tmp);
            Vx.X = tmp;
            tmp = -99;
            double.TryParse(SplitData[4], out tmp);
            Vx.Y = tmp;
            tmp = -99;
            double.TryParse(SplitData[5], out tmp);
            Vx.Z = tmp;

            tmp = -99;
            double.TryParse(SplitData[6], out tmp);
            Vy.X = tmp;
            tmp = -99;
            double.TryParse(SplitData[7], out tmp);
            Vy.Y = tmp;
            tmp = -99;
            double.TryParse(SplitData[8], out tmp);
            Vy.Z = tmp;

            tmp = -99;
            double.TryParse(SplitData[9], out tmp);
            Vz.X = tmp;
            tmp = -99;
            double.TryParse(SplitData[10], out tmp);
            Vz.Y = tmp;
            tmp = -99;
            double.TryParse(SplitData[11], out tmp);
            Vz.Z = tmp;

        }
        public Point3D GetVxExtendPoint(double Distance)
        {
            Point3D ExP = new Point3D();
            ExP.X = GetVxExtendX(Distance);
            ExP.Y = GetVxExtendY(Distance);
            ExP.Z = GetVxExtendZ(Distance);

            return ExP;
        }
        public double GetVxExtendX(double Distance)
        {
            Vector3D Unit = Vx.GetUnitVector();
            return X + Unit.X * Distance;
        }
        public double GetVxExtendY(double Distance)
        {
            Vector3D Unit = Vx.GetUnitVector();
            return Y + Unit.Y * Distance;
        }
        public double GetVxExtendZ(double Distance)
        {
            Vector3D Unit = Vx.GetUnitVector();
            return Z + Unit.Z * Distance;
        }

        public Point3D GetVyExtendPoint(double Distance)
        {
            Point3D ExP = new Point3D();
            ExP.X = GetVyExtendX(Distance);
            ExP.Y = GetVyExtendY(Distance);
            ExP.Z = GetVyExtendZ(Distance);

            return ExP;
        }
        public double GetVyExtendX(double Distance)
        {
            Vector3D Unit = Vy.GetUnitVector();
            return X + Unit.X * Distance;
        }
        public double GetVyExtendY(double Distance)
        {
            Vector3D Unit = Vy.GetUnitVector();
            return Y + Unit.Y * Distance;
        }
        public double GetVyExtendZ(double Distance)
        {
            Vector3D Unit = Vy.GetUnitVector();
            return Z + Unit.Z * Distance;
        }

        public Point3D GetVzExtendPoint(double Distance)
        {
            Point3D ExP = new Point3D();
            ExP.X = GetVzExtendX(Distance);
            ExP.Y = GetVzExtendY(Distance);
            ExP.Z = GetVzExtendZ(Distance);

            return ExP;
        }
        public double GetVzExtendX(double Distance)
        {
            Vector3D Unit = Vz.GetUnitVector();
            return X + Unit.X * Distance;
        }
        public double GetVzExtendY(double Distance)
        {
            Vector3D Unit = Vz.GetUnitVector();
            return Y + Unit.Y * Distance;
        }
        public double GetVzExtendZ(double Distance)
        {
            Vector3D Unit = Vz.GetUnitVector();
            return Z + Unit.Z * Distance;
        }
    }
    [Serializable]
    public partial class Point3DI : Point3D
    {
        public double Intensity;
        public Point3DI()
        {
            X = 0;
            Y = 0;
            Z = 0;
            Intensity = 0.0;
        }
        public Point3DI(double x, double y, double z)
        {
            X = x;
            Y = y;
            Z = z;
            Intensity = 0.0;
        }
        public Point3DI(double x, double y, double z, double intensity)
        {
            X = x;
            Y = y;
            Z = z;
            Intensity = intensity;
        }
    }
}

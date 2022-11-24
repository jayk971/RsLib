
using System;
using System.Collections.Generic;

using Accord.Math;
namespace RsLib.PointCloud
{
    [Serializable]
    public partial class Vector3D:Object3D
    {
        public readonly static Vector3D XAxis = new Vector3D(1, 0, 0);
        public readonly static Vector3D YAxis = new Vector3D(0, 1, 0);
        public readonly static Vector3D ZAxis = new Vector3D(0, 0, 1);
        public override uint DataCount => 1;
        //Vector3 vector;
        public Vector3 V => new Vector3((float)X, (float)Y, (float)(Z));
        public double X = 0.0;

        public double Y = 0.0;
        public double Z = 0.0;

        public double L{ get => V.Norm;}


        public double R = 0;
        public double T = 0;
        public double P = 0;
        // 0向量建構
        public Vector3D()
        {
            X = 0.0;
            Y = 0.0;
            Z = 0.0;
        }
        // 直接指定分量建構
        public Vector3D(double x, double y, double z)
        {
            this.X = x;
            this.Y = y;
            this.Z = z;
        }
        // 指定起點與終點建構
        public Vector3D(Point3D startP, Point3D endP)
        {
            X = Math.Round(endP.X - startP.X, 2);
            Y = Math.Round(endP.Y - startP.Y, 2);
            Z = Math.Round(endP.Z - startP.Z, 2);
        }
        public void CalculateEulerAngle()
        {
            if (!(X == 0 && Y == 0 && Z == 0))
            {
                R = V.Square;
                T = Math.Atan2(Y, X);
                P = Math.Acos(Z / R);
            }
        }

        public static Vector3D VectorFromEulerAngle(double r, double t, double p)
        {
            Vector3D vec = new Vector3D();
            vec.X = r * Math.Sin(p) * Math.Cos(t);
            vec.Y = r * Math.Sin(p) * Math.Sin(t);
            vec.Z = r * Math.Cos(p);
            return vec;
        }

        public void Reverse()
        {
            X *= -1;
            Y *= -1;
            Z *= -1;
        }
        public Vector3D GetReverse()
        {
            return new Vector3D(-1 * X, -1 * Y, -1 * Z);
        }
        // 計算向量長度
        public Vector3D ShortestVector(Point3D Target, Point3D RefPoint)
        {
            double CalD = 0.0;
            Vector3D tmpUnit = GetUnitVector();
            Vector3D RefV = new Vector3D(Target, RefPoint);

            Vector3D CrossV = Vector3D.Cross(RefV, tmpUnit);
            CalD = CrossV.L;

            Vector3D dCross = Vector3D.Cross(tmpUnit, CrossV);
            dCross.UnitVector();

            Vector3D Output = new Vector3D();
            Output.X = dCross.X * CalD;
            Output.Y = dCross.Y * CalD;
            Output.Z = dCross.Z * CalD;


            return Output;
        }
        public Point3D ShortestPoint(Point3D Target, Point3D RefPoint)
        {
            double CalD = 0.0;
            Vector3D tmpUnit = GetUnitVector();
            Vector3D RefV = new Vector3D(Target, RefPoint);

            Vector3D CrossV = Vector3D.Cross(RefV, tmpUnit);
            CalD = CrossV.L;

            Vector3D dCross = Vector3D.Cross(tmpUnit, CrossV);
            dCross.UnitVector();

            Point3D Output = new Point3D();
            Output.X = Target.X + dCross.X * CalD;
            Output.Y = Target.Y + dCross.Y * CalD;
            Output.Z = Target.Z + dCross.Z * CalD;


            return Output;
        }
        public double ShortestDistance(Point3D Target, Point3D RefPoint)
        {
            double CalD = 0.0;
            Vector3D tmpUnit = GetUnitVector();
            Vector3D RefV = new Vector3D(Target, RefPoint);

            Vector3D CrossV = Vector3D.Cross(RefV, tmpUnit);
            CalD = CrossV.L;

            return CalD;
        }
        public double ShortestDistanceWithSign(Point3D Target, Point3D RefPoint, Vector3D RefDir)
        {
            double CalD = 0.0;
            Vector3D tmpUnit = GetUnitVector();
            Vector3D RefV = new Vector3D(Target, RefPoint);

            Vector3D CrossV = Vector3D.Cross(RefV, tmpUnit);
            CalD = CrossV.L;

            double DotValue = Vector3D.Dot(RefV, RefDir);
            if (DotValue < 0)
            {
                CalD *= -1;
            }


            return CalD;
        }
        // 回傳單位向量
        public Vector3D GetUnitVector()
        {
            Vector3 temp = new Vector3((float)X, (float)Y, (float)Z);
            temp.Normalize();
            Vector3D v_unit = new Vector3D(temp.X, temp.Y, temp.Z);
            return v_unit;
        }
        public void UnitVector()
        {
            V.Normalize();
            X = V.X;
            Y = V.Y;
            Z = V.Z;
        }

        public static Vector3D operator +(Vector3D A, Vector3D B)
        {
            return new Vector3D(A.X + B.X, A.Y + B.Y, A.Z + B.Z);
        }
        public static Point3D operator +(Point3D P, Vector3D A)
        {
            return new Point3D(A.X + P.X, A.Y + P.Y, A.Z + P.Z);
        }
        public static Vector3D operator -(Vector3D A, Vector3D B)
        {
            return new Vector3D(A.X - B.X, A.Y - B.Y, A.Z - B.Z);
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
        public static Vector3D Multiply(Vector3D A, double t)
        {
            Vector3D v = new Vector3D();
            v.X = t * A.X;
            v.Y = t * A.Y;
            v.Z = t * A.Z;
            return v;
        }
        public Vector3D Multiply(Matrix4x4 matrix)
        {
            Vector4 output = Matrix4x4.Multiply(matrix, new Vector4((float)X, (float)Y, (float)Z, 1f));
            return new Vector3D(output.X, output.Y, output.Z);
        }
        /// <summary>
        /// 向量內積
        /// </summary>
        public static double Dot(Vector3D A, Vector3D B) => Vector3.Dot(A.V, B.V);
        public static double Dot(Vector3 A, Vector3D B) =>Vector3.Dot(A, B.V);
        public static double Dot(Vector3 A, Vector3 B) => Vector3.Dot(A, B);
        public static double Dot(Vector3D A, Vector3 B) => Vector3.Dot(A.V, B);


        /// <summary>
        /// 向量外積
        /// </summary>
        public static Vector3D Cross(Vector3D A, Vector3D B)
        {
            Vector3 output = Vector3.Cross(A.V, B.V);
            Vector3D v = new Vector3D(output.X, output.Y, output.Z);
            return v;
        }
        public static Vector3D Cross(Vector3 A, Vector3D B)
        {
            Vector3 output = Vector3.Cross(A, B.V);
            Vector3D v = new Vector3D(output.X, output.Y, output.Z);
            return v;
        }
        public static Vector3D Cross(Vector3D A, Vector3 B)
        {
            Vector3 output = Vector3.Cross(A.V, B);
            Vector3D v = new Vector3D(output.X, output.Y, output.Z);
            return v;
        }
        public static Vector3D Cross(Vector3 A, Vector3 B)
        {
            Vector3 output = Vector3.Cross(A, B);
            Vector3D v = new Vector3D(output.X, output.Y, output.Z);
            return v;
        }
        /// <summary>
        /// 向量內插
        /// </summary>
        public static List<Vector3D> Interpolation(Vector3D A, Vector3D B, int n)
        {
            Vector3 v = B.V - A.V;
            //double X = B.X - A.X;
            //double Y = B.Y - A.Y;
            //double Z = B.Z - A.Z;

            double x_pitch = v.X / n;
            double y_pitch = v.Y / n;
            double z_pitch = v.Z / n;

            List<Vector3D> result = new List<Vector3D>();

            for (int i = 1; i < n; i++)
            {
                Vector3D v2 = new Vector3D(A.X + i * x_pitch, A.Y + i * y_pitch, A.Z + i * z_pitch);
                result.Add(v2);
            }

            return result;
        }

        /// <summary>
        /// Description:    static method, calculate the angle degree of vector A to vector B
        /// Date:           2013.9.18
        /// </summary>
        /// <returns>distance</returns>
        public static double Degree(Vector3D A, Vector3D B)
        {
            Vector3D cross = Cross(A, B);
            double CalL = cross.L / (A.L * B.L);
            double CalAngle = Math.Round(Math.Asin(CalL) * 180 / Math.PI, 2);
            double dot = Dot(A, B);
            if (dot < 0 && CalAngle < 90) CalAngle += 180;

            return CalAngle;
        }
        public static double Radius(Vector3D A, Vector3D B)
        {
            double AB = A.L * B.L;
            double AdotB = Dot(A, B);
            double q = AdotB / AB;

            // 有任何一向量為0，無需計算，回傳0
            if (AdotB == 0)
                return 0.0;

            // 避免計算兩向量幾乎同向(q接近1)的情形，直接回傳0
            if (Math.Abs(1 - q) < 0.000001)
                return 0.0;
            // 避免計算兩向量幾乎反向(q接近-1)的情形，直接回傳180
            else if (Math.Abs(1 + q) < 0.000001)
                return 1.0;
            else
            {
                double value = Math.Acos(q);
                return value;
            }
        }
        public static void SmoothLine(ref List<Vector3D> input, out List<Vector3D> output, int times)
        {
            List<Vector3D> currentIn = input;
            List<Vector3D> currentOut = new List<Vector3D>();

            for (int i = 0; i < times; i++)
            {
                SmoothLine(ref currentIn, out currentOut);
                currentIn = currentOut;
            }

            output = currentOut;
        }

        public static void SmoothLine(ref List<Vector3D> input, out List<Vector3D> output)
        {
            output = new List<Vector3D>();

            for (int i = 0; i < input.Count; i++)
            {

                int index1 = i - 2;
                int index2 = i - 1;
                int index3 = i + 1;
                int index4 = i + 2;

                if (index3 >= input.Count)
                    index3 -= input.Count;

                if (index4 >= input.Count)
                    index4 -= input.Count;

                if (index1 < 0)
                    index1 += input.Count;

                if (index2 < 0)
                    index2 += input.Count;

                Vector3D outP = new Vector3D(
                    (input[index1].X * 0.25 + input[index2].X * 0.5 + input[index3].X * 0.5 + input[index4].X * 0.25) / 1.5,
                    (input[index1].Y * 0.25 + input[index2].Y * 0.5 + input[index3].Y * 0.5 + input[index4].Y * 0.25) / 1.5,
                    (input[index1].Z * 0.25 + input[index2].Z * 0.5 + input[index3].Z * 0.5 + input[index4].Z * 0.25) / 1.5
                );

                output.Add(outP);
            }

        }

        public static Vector3D AsignElevationAngle(Vector3D ori_vec, double assign_angle_in_degree)
        {
            Vector3D adjusted_vector = new Vector3D();

            ori_vec = ori_vec.GetUnitVector();

            double alpha = Math.Atan2(ori_vec.Y, ori_vec.X);

            adjusted_vector.X = Math.Abs(Math.Cos(assign_angle_in_degree / 180.0 * Math.PI)) * Math.Cos(alpha);
            adjusted_vector.Y = Math.Abs(Math.Cos(assign_angle_in_degree / 180.0 * Math.PI)) * Math.Sin(alpha);
            adjusted_vector.Z = Math.Sin(assign_angle_in_degree / 180.0 * Math.PI);

            return adjusted_vector;
        }

        public static Vector3D AjustElevationAngle(Vector3D ori_vec, double diff_angle_in_degree)
        {
            Vector3D adjusted_vector = new Vector3D();

            ori_vec = ori_vec.GetUnitVector();

            double angleWithXYPlaneRadian = GetAngleWithXYPlane(ori_vec);

            angleWithXYPlaneRadian += (diff_angle_in_degree / 180.0 * Math.PI);

            double alpha = Math.Atan2(ori_vec.Y, ori_vec.X);

            adjusted_vector.X = Math.Abs(Math.Cos(angleWithXYPlaneRadian)) * Math.Cos(alpha);
            adjusted_vector.Y = Math.Abs(Math.Cos(angleWithXYPlaneRadian)) * Math.Sin(alpha);
            adjusted_vector.Z = Math.Sin(angleWithXYPlaneRadian);

            return adjusted_vector;
        }

        public static double GetAngleWithXYPlane(Vector3D vec)
        {
            vec = vec.GetUnitVector();

            double angleWithXYPlaneRadian = Math.Asin(vec.Z);

            return angleWithXYPlaneRadian;
        }

    }
}
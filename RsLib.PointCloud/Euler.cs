using Accord.Math;
using Accord.Statistics.Distributions.Reflection;
using Accord.Statistics.Kernels;
using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Drawing.Drawing2D;
using System.IO;
using System.Text;
using System.Windows.Forms;

namespace RsLib.PointCloudLib
{
    public enum MatrixType : int
    {
        Translation = 0,
        Rotate,
        Scale,
        //Reflection,
        //Shearing,
    }
    public enum RefAxis : int
    {
        X,
        Y,
        Z
    }

    public class MatrixUnit
    {

        RefAxis refAxis = RefAxis.X;
        public RefAxis RefAxis
        {
            get => refAxis;
            set
            {
                refAxis = value;
                calculateMatrix();
            }
        }
        MatrixType matrixType = MatrixType.Translation;
        public MatrixType Type
        {
            get => matrixType;
            set
            {
                matrixType = value;
                calculateMatrix();
            }
        }
        public double setValue = 0;
        /// <summary>
        /// if matrix is rotation, value is radian;
        /// if matrix is translate, value is millimeter.
        /// </summary>
        public double Value
        {
            get => setValue;
            set
            {
                setValue = value;
                calculateMatrix();
            }
        }

        internal Matrix4x4 matrix4 = Matrix4x4.Identity;
        public Matrix4x4 Matrix4 { get => matrix4; }


        void calculateMatrix()
        {
            switch (Type)
            {
                case MatrixType.Translation:

                    calculateTranslationMatrix();

                    break;

                case MatrixType.Rotate:
                    calculateRotateMatrix();
                    break;
                case MatrixType.Scale:

                    calculateScaleMatrix();
                    break;

                default:

                    break;
            }
        }
        void calculateTranslationMatrix()
        {
            matrix4 = Matrix4x4.Identity;
            switch (RefAxis)
            {
                case RefAxis.X:
                    matrix4.V03 = (float)Value;
                    break;

                case RefAxis.Y:
                    matrix4.V13 = (float)Value;
                    break;

                case RefAxis.Z:
                    matrix4.V23 = (float)Value;
                    break;

                default:

                    break;
            }
        }
        void calculateRotateMatrix()
        {
            matrix4 = Matrix4x4.Identity;
            switch (RefAxis)
            {
                case RefAxis.X:
                    matrix4 = Matrix4x4.CreateRotationX((float)Value);
                    break;

                case RefAxis.Y:
                    matrix4 = Matrix4x4.CreateRotationY((float)Value);
                    break;

                case RefAxis.Z:
                    matrix4 = Matrix4x4.CreateRotationZ((float)Value);
                    break;

                default:

                    break;
            }
        }
        void calculateScaleMatrix()
        {
            matrix4 = Matrix4x4.Identity;
            matrix4.V00 = (float)Value;
            matrix4.V11 = (float)Value;
            matrix4.V22 = (float)Value;
        }
        public Point3D Multiply(Point3D target)
        {
            Vector4 output = Matrix4x4.Multiply(matrix4, new Vector4((float)target.X, (float)target.Y, (float)target.Z, 1f));
            return new Point3D(output.X, output.Y, output.Z);
        }
        public Vector3D Multiply(Vector3D target)
        {
            Vector4 output = Matrix4x4.Multiply(matrix4, new Vector4((float)target.X, (float)target.Y, (float)target.Z, 1f));
            return new Vector3D(output.X, output.Y, output.Z);
        }

        public override string ToString()
        {
            return $"{Type} {RefAxis} Axis {Value} Unit";
        }
    }
    public class TranslationUnit : MatrixUnit
    {
        public double ShiftDistance
        {
            get => Value;
            set
            {
                Value = value;
            }
        }
        public Matrix4x4 ShiftMatrix4 { get => matrix4; }
        public Matrix4x4 ShiftMatrix4Inverse
        {
            get
            {
                Matrix4x4 m = Matrix4x4.Identity;
                m.V03 = -matrix4.V03;
                m.V13 = -matrix4.V13;
                m.V23 = -matrix4.V23;
                return m;
            }
        }



        public TranslationUnit()
        {
            Type = MatrixType.Translation;

        }
        public TranslationUnit(RefAxis inRefAxis)
        {
            Type = MatrixType.Translation;
            RefAxis = inRefAxis;
            Value = 0;
        }
        public TranslationUnit(RefAxis inRefAxis, double shiftValue)
        {
            Type = MatrixType.Translation;
            RefAxis = inRefAxis;
            Value = shiftValue;
        }

        Point3D shift(Point3D target)
        {
            Vector4 output = Matrix4x4.Multiply(matrix4, new Vector4((float)target.X, (float)target.Y, (float)target.Z, 1f));
            return new Point3D(output.X, output.Y, output.Z);
        }
        PointCloud shift(PointCloud target)
        {
            PointCloud output = new PointCloud();
            for (int i = 0; i < target.Count; i++)
            {
                output.Add(shift(target.Points[i]));
            }
            return output;
        }
        public override string ToString()
        {
            return $"{Type} {RefAxis} Axis {Value} mm";
        }
    }
    public class RotateUnit : MatrixUnit
    {
        public double RotateAngle { get => Math.Round(Value / Math.PI * 180.0,2); }
        public double RotateRadian { get => Value; }
        public Matrix4x4 RotateMatrix4 { get => matrix4; }
        Matrix3x3 RotateMatrix3
        {
            get
            {
                Matrix3x3 m = Matrix3x3.Identity;
                m.V00 = matrix4.V00;
                m.V01 = matrix4.V01;
                m.V02 = matrix4.V02;

                m.V10 = matrix4.V10;
                m.V11 = matrix4.V11;
                m.V12 = matrix4.V12;

                m.V20 = matrix4.V20;
                m.V21 = matrix4.V21;
                m.V22 = matrix4.V22;
                return m;
            }
        }
        public Matrix4x4 RotateMatrix4Inverse { get => Transpose(); }

        public RotateUnit()
        {
            Type = MatrixType.Rotate;
        }
        public RotateUnit(RefAxis inRefAxis)
        {
            Type = MatrixType.Rotate;
            RefAxis = inRefAxis;
            Value = 0;
        }
        public RotateUnit(RefAxis inRefAxis, double inRotValue, bool ValueIsAngleUnit = true)
        {
            Type = MatrixType.Rotate;
            RefAxis = inRefAxis;
            if (ValueIsAngleUnit) Value = inRotValue / 180 * Math.PI;
            else Value = inRotValue;
        }
        public Matrix4x4 Transpose()
        {
            Matrix4x4 output = new Matrix4x4();
            output.V00 = matrix4.V00;
            output.V01 = matrix4.V10;
            output.V02 = matrix4.V20;
            output.V03 = matrix4.V30;
            output.V10 = matrix4.V01;
            output.V11 = matrix4.V11;
            output.V12 = matrix4.V21;
            output.V13 = matrix4.V31;
            output.V20 = matrix4.V02;
            output.V21 = matrix4.V12;
            output.V22 = matrix4.V22;
            output.V23 = matrix4.V32;
            output.V30 = matrix4.V03;
            output.V31 = matrix4.V13;
            output.V32 = matrix4.V23;
            output.V33 = matrix4.V33;
            return output;
        }

        public void Rotate(ref PointV3D Coord)
        {
            switch (RefAxis)
            {
                case RefAxis.X:
                    Coord.Vy = PointCloudCommon.Rotate(Coord.Vy, Coord.Vx, RotateAngle);
                    Coord.Vz = PointCloudCommon.Rotate(Coord.Vz, Coord.Vx, RotateAngle);
                    break;
                case RefAxis.Y:
                    Coord.Vx = PointCloudCommon.Rotate(Coord.Vx, Coord.Vy, RotateAngle);
                    Coord.Vz = PointCloudCommon.Rotate(Coord.Vz, Coord.Vy, RotateAngle);
                    break;
                case RefAxis.Z:
                    Coord.Vx = PointCloudCommon.Rotate(Coord.Vx, Coord.Vz, RotateAngle);
                    Coord.Vy = PointCloudCommon.Rotate(Coord.Vy, Coord.Vz, RotateAngle);
                    break;
                default:
                    break;
            }
        }
        public override string ToString()
        {
            return $"{Type} {RefAxis} Axis {RotateAngle} deg {RotateRadian} rad";
        }
    }
    public class CoordMatrix
    {
        internal string Name = "";
        internal List<MatrixUnit> seq = new List<MatrixUnit>();
        public List<MatrixUnit> MatrixSequnce { get => seq; }
        protected Matrix4x4 finalMatrix4 = Matrix4x4.Identity;
        public Matrix4x4 FinalMatrix4
        {
            get
            {
                if (!isMatrixCalculated)
                {
                    EndAddMatrix();
                }
                return finalMatrix4;
            }

        }

        public Matrix4x4 FinalMatrix4Inverse
        {
            get
            {
                Matrix4x4 m = Matrix4x4.Identity;
                for (int i = 0; i < seq.Count; i++)
                {
                    switch (seq[i].Type)
                    {
                        case MatrixType.Rotate:
                            RotateUnit rUnit = seq[i] as RotateUnit;
                            m = Matrix4x4.Multiply(rUnit.RotateMatrix4Inverse, m);
                            break;

                        case MatrixType.Translation:
                            TranslationUnit sUnit = seq[i] as TranslationUnit;
                            m = Matrix4x4.Multiply(sUnit.ShiftMatrix4Inverse, m);
                            break;

                        default:

                            break;
                    }
                }
                return m;
            }
        }
        internal bool isMatrixCalculated = false;
        public bool IsMatrixCalculated { get => isMatrixCalculated; }

        public CoordMatrix()
        {

        }

        /// <summary>
        /// 加入平移或旋轉矩陣
        /// </summary>
        /// <param name="axis">參考軸</param>
        /// <param name="matrixType">矩陣類型</param>
        /// <param name="setValue">平移矩陣為平移距離 ; 旋轉矩陣為旋轉角度, 不是弧度</param>
        public void AddSeq(RefAxis axis, MatrixType matrixType, double setValue)
        {
            switch (matrixType)
            {
                case MatrixType.Translation:
                    TranslationUnit sUnit = new TranslationUnit(axis, setValue);
                    seq.Add(sUnit);
                    break;

                case MatrixType.Rotate:
                    RotateUnit rUnit = new RotateUnit(axis, setValue);
                    seq.Add(rUnit);
                    break;

                default:

                    break;
            }
            isMatrixCalculated = false;
        }
        public void AddSeq(List<MatrixUnit> otherSeq)
        {
            seq.AddRange(otherSeq);
            isMatrixCalculated = false;
        }
        public void AddSeq(CoordMatrix otherSeq)
        {
            seq.AddRange(otherSeq.seq);
            isMatrixCalculated = false;
        }

        public void Clear()
        {
            seq.Clear();
            finalMatrix4 = Matrix4x4.Identity;
            isMatrixCalculated = false;
        }
        public void EndAddMatrix()
        {
            finalMatrix4 = Matrix4x4.Identity;
            for (int i = 0; i < seq.Count; i++)
            {
                finalMatrix4 = Matrix4x4.Multiply( seq[i].matrix4,finalMatrix4);
            }
            isMatrixCalculated = true;
        }
        public Matrix4x4 Transpose()
        {
            Matrix4x4 output = new Matrix4x4();
            output.V00 = finalMatrix4.V00;
            output.V01 = finalMatrix4.V10;
            output.V02 = finalMatrix4.V20;
            output.V03 = finalMatrix4.V30;
            output.V10 = finalMatrix4.V01;
            output.V11 = finalMatrix4.V11;
            output.V12 = finalMatrix4.V21;
            output.V13 = finalMatrix4.V31;
            output.V20 = finalMatrix4.V02;
            output.V21 = finalMatrix4.V12;
            output.V22 = finalMatrix4.V22;
            output.V23 = finalMatrix4.V32;
            output.V30 = finalMatrix4.V03;
            output.V31 = finalMatrix4.V13;
            output.V32 = finalMatrix4.V23;
            output.V33 = finalMatrix4.V33;
            return output;
        }

        public static void SolveMatrixRzRyRxShift(Matrix4x4 m,out Rotate r, out Shift s)
        {
            s = new Shift(m.V03,m.V13,m.V23);
            Vector3D vx = new Vector3D(m.V00,m.V10,m.V20);
            Vector3D vy = new Vector3D(m.V01, m.V11, m.V21);
            Vector3D vz = new Vector3D(m.V02, m.V12, m.V22);

            vx.UnitVector();
            vy.UnitVector();
            vz.UnitVector();

            r = new Rotate(vx, vy, vz);

        }
    }
    public class Rotate : CoordMatrix
    {
        public Matrix3x3 RotateMatrix3
        {
            get
            {
                Matrix3x3 output = new Matrix3x3();
                output.V00 = finalMatrix4.V00;
                output.V01 = finalMatrix4.V01;
                output.V02 = finalMatrix4.V02;

                output.V10 = finalMatrix4.V10;
                output.V11 = finalMatrix4.V11;
                output.V12 = finalMatrix4.V12;

                output.V20 = finalMatrix4.V20;
                output.V21 = finalMatrix4.V21;
                output.V22 = finalMatrix4.V22;
                return output;
            }
        }
        public double Rz
        {
            get
            {
                if (seq.Count == 3)
                {
                    return seq[0].Value / Math.PI * 180;
                }
                else return 0.0;
            }
        }
        public double Ry
        {
            get
            {
                if (seq.Count == 3)
                {
                    return seq[1].Value / Math.PI * 180;
                }
                else return 0.0;
            }
        }
        public double Rx
        {
            get
            {
                if (seq.Count == 3)
                {
                    return seq[2].Value / Math.PI * 180;
                }
                else return 0.0;
            }
        }

        public float[,] FloatArray2d
        {
            get
            {
                return PointCloudCommon.Matrix4x4ToFloatArray(finalMatrix4);
            }
        }
        public float[,] FloatArray2dTranspose
        {
            get
            {
                return PointCloudCommon.Matrix4x4ToFloatArray(Transpose());
            }
        }

        public Quaternion Q = new Quaternion();
        public Rotate()
        {

        }
        /// <summary>
        /// 輸入旋轉後坐標系, 計算出世界座標->旋轉後坐標系的矩陣
        /// </summary>
        /// <param name="vx">旋轉後坐標系 x 相對於世界坐標系</param>
        /// <param name="vy">旋轉後坐標系 y 相對於世界坐標系</param>
        /// <param name="vz">旋轉後坐標系 z 相對於世界坐標系</param>
        public Rotate(Vector3D vx, Vector3D vy, Vector3D vz)
        {
            finalMatrix4 = collectMatricElement(vx, vy, vz);
            solveRzRyRx();
            SolveQ();
            //List<RotateUnit> rSeq = solveRzRyRx(finalMatrix4);
            //seq.AddRange(rSeq);
            isMatrixCalculated = true;

        }
        public Rotate(PointV3D p)
        {
            finalMatrix4 = collectMatricElement(p.Vx, p.Vy, p.Vz);
            solveRzRyRx();
            SolveQ();
            //List<RotateUnit> rSeq = solveRzRyRx(finalMatrix4);
            //seq.AddRange(rSeq);
            isMatrixCalculated = true;

        }
        /// <summary>
        /// Solve Rz -> Ry -> Rx
        /// </summary>
        /// <param name="p"></param>
        /// <returns>[0] : Rz,[1] : Ry,[2] : Rx</returns>
        public static double[] SolveRzRyRx(PointV3D p)
        {
            Rotate r = new Rotate(p);
            return new double[] { r.Rz, r.Ry, r.Rx };
        }
        /// <summary>
        /// 輸入旋轉後坐標系, 計算出世界座標->旋轉後坐標系的矩陣
        /// </summary>
        /// <param name="vx">旋轉後坐標系 x 相對於世界坐標系</param>
        /// <param name="vy">旋轉後坐標系 y 相對於世界坐標系</param>
        /// <param name="vz">旋轉後坐標系 z 相對於世界坐標系</param>
        public Rotate(Vector3 vx, Vector3 vy, Vector3 vz)
        {
            finalMatrix4 = collectMatricElement(vx, vy, vz);
            solveRzRyRx();
            SolveQ();
            //List<RotateUnit> rSeq = solveRzRyRx(finalMatrix4);
            //seq.AddRange(rSeq);
            isMatrixCalculated = true;

        }
        public void AddRotateSeq(RefAxis rotSeq, double rotateAngle)
        {
            RotateUnit rotateEulerUnit = new RotateUnit(rotSeq, rotateAngle);
            seq.Add(rotateEulerUnit);
            Q*=new Quaternion(rotateEulerUnit);
            isMatrixCalculated = false;
        }
        /// <summary>
        /// 取得旋轉 Rz -> Ry -> Rx 順序的旋轉角
        /// </summary>
        /// <param name="vx">姿態的 x 向量</param>
        /// <param name="vy">姿態的 y 向量</param>
        /// <param name="vz">姿態的 z 向量</param>
        /// <returns>旋轉 Rz -> Ry -> Rx 順序的旋轉角</returns>
        public static List<RotateUnit> SolveRzRyRxFromVector3DZYX(Vector3D vx, Vector3D vy, Vector3D vz)
        {
            /*
            V00,V01,V02,V03
            V10,V11,V12,V13
            V20,V21,V22,V23
            V30,V31,V32,V33
            */
            Matrix4x4 after = collectMatricElement(vx, vy, vz);
            return SolveRzRyRx(after);
        }
        static Matrix4x4 collectMatricElement(Vector3D vx, Vector3D vy, Vector3D vz)
        {
            vx.UnitVector();
            vy.UnitVector();
            vz.UnitVector();
            Matrix4x4 matrix = Matrix4x4.Identity;
            /*
             Row : Fix body coordinate relate to world coordinate
            Column : World coordinate relate to fix body coordinate
             */
            matrix.V00 = (float)vx.X;
            matrix.V01 = (float)vx.Y;
            matrix.V02 = (float)vx.Z;

            matrix.V10 = (float)vy.X;
            matrix.V11 = (float)vy.Y;
            matrix.V12 = (float)vy.Z;

            matrix.V20 = (float)vz.X;
            matrix.V21 = (float)vz.Y;
            matrix.V22 = (float)vz.Z;
            return matrix;
        }
        static Matrix4x4 collectMatricElement(Vector3 vx, Vector3 vy, Vector3 vz)
        {
            vx.Normalize();
            vy.Normalize();
            vz.Normalize();
            Matrix4x4 matrix = Matrix4x4.Identity;
            /*
             Row : Fix body coordinate relate to world coordinate
            Column : World coordinate relate to fix body coordinate
             */
            matrix.V00 = (float)vx.X;
            matrix.V01 = (float)vx.Y;
            matrix.V02 = (float)vx.Z;

            matrix.V10 = (float)vy.X;
            matrix.V11 = (float)vy.Y;
            matrix.V12 = (float)vy.Z;

            matrix.V20 = (float)vz.X;
            matrix.V21 = (float)vz.Y;
            matrix.V22 = (float)vz.Z;
            return matrix;
        }

#if m
        public static double[] SolveQ(Matrix4x4 inMatrix)
        {
            double q00 = Math.Sqrt(1 + inMatrix.V00 + inMatrix.V11 + inMatrix.V22);

            double q11 = Math.Sqrt(1 + inMatrix.V00 - inMatrix.V11 - inMatrix.V22);

            double q22 = Math.Sqrt(1 - inMatrix.V00 + inMatrix.V11 - inMatrix.V22);

            double q33 = Math.Sqrt(1 - inMatrix.V00 - inMatrix.V11 + inMatrix.V22);


            double[] q0R = new double[]
            {
                Math.Round(q00/2,7),
                Math.Round((inMatrix.V21-inMatrix.V12)/q00/2,7),
                Math.Round((inMatrix.V02 - inMatrix.V20) / q00 / 2, 7),
                Math.Round((inMatrix.V10 - inMatrix.V01) / q00 / 2, 7)
            };
            double[] q1R = new double[]
            {
                Math.Round((inMatrix.V21 - inMatrix.V12) / q11 / 2, 7),
                Math.Round(q11 / 2, 7),
                Math.Round((inMatrix.V10 + inMatrix.V01) / q11 / 2, 7),
                Math.Round((inMatrix.V02 + inMatrix.V20) / q11 / 2, 7)
               };
            double[] q2R = new double[]
            {
                Math.Round((inMatrix.V02 - inMatrix.V20) / q22 / 2, 7),
                Math.Round((inMatrix.V10 + inMatrix.V01) / q22 / 2, 7),
                Math.Round(q22 / 2, 7),
                Math.Round((inMatrix.V21 + inMatrix.V12) / q22 / 2, 7)
            };
            double[] q3R = new double[]
            {
                Math.Round((inMatrix.V10 - inMatrix.V01) / q33 / 2, 7),
                Math.Round((inMatrix.V02 + inMatrix.V20) / q33 / 2, 7),
                Math.Round((inMatrix.V21 + inMatrix.V12) / q33 / 2, 7),
               Math.Round(q33 / 2, 7)
            };

            if ((inMatrix.V11 > -1 * inMatrix.V22) &&
                (inMatrix.V00 > -1 * inMatrix.V11) &&
                (inMatrix.V00 > -1 * inMatrix.V22))
            {
                return q0R;
            }
            else if ((inMatrix.V11 < -1 * inMatrix.V22) &&
                (inMatrix.V00 > inMatrix.V11) &&
                (inMatrix.V00 > inMatrix.V22))
            {
                return q1R;
            }
            else if ((inMatrix.V11 > inMatrix.V22) &&
                (inMatrix.V00 < inMatrix.V11) &&
                (inMatrix.V00 < -1 * inMatrix.V22))
            {
                return q2R;
            }
            else if ((inMatrix.V11 < inMatrix.V22) &&
                (inMatrix.V00 < -1 * inMatrix.V11) &&
                (inMatrix.V00 < inMatrix.V22))
            {
                return q3R;
            }
            else
            {
                return new double[] { 1, 0, 0, 0 };
            }

        }
#endif
        public void SolveQ()
        {
            Q = new Quaternion();
            for (int i = 0; i < seq.Count; i++)
            {
                 if(seq[i].Type == MatrixType.Rotate)
                {
                    Quaternion tempQ = new Quaternion(seq[i]);
                    Q *= tempQ;
                }
            }
        }
        public static List<RotateUnit> SolveRzRyRx(Matrix4x4 inMatrix)
        {
            RotateUnit rx = new RotateUnit(RefAxis.X);
            RotateUnit ry = new RotateUnit(RefAxis.Y);
            RotateUnit rz = new RotateUnit(RefAxis.Z);

            if (inMatrix.V02 < 1)
            {
                if (inMatrix.V02 > -1)
                {
                    rx.Value = Math.Atan2(inMatrix.V12, inMatrix.V22);
                    ry.Value = -Math.Asin(inMatrix.V02);
                    rz.Value = Math.Atan2(inMatrix.V01, inMatrix.V00);
                }
                else // inMatrix.V02 == -1
                {
                    rx.Value = Math.Atan2(inMatrix.V10, inMatrix.V11);
                    ry.Value = Math.PI / 2;
                    rz.Value = 0;
                }
            }
            else // inMatrix.V02 == 1
            {
                rx.Value = -Math.Atan2(inMatrix.V10, inMatrix.V11);
                ry.Value = -Math.PI / 2;
                rz.Value = 0;
            }
            List<RotateUnit> output = new List<RotateUnit>();
            output.Add(rz);
            output.Add(ry);
            output.Add(rx);
            return output;
        }
        void solveRzRyRx()
        {
            seq.AddRange(SolveRzRyRx(finalMatrix4));
        }

    }
    public class Shift : CoordMatrix
    {
        public double X
        {
            get
            {
                foreach (var item in seq)
                {
                    if (item.RefAxis == RefAxis.X) return item.Value;
                }
                return 0;
            }
        }
        public double Y
        {
            get
            {
                foreach (var item in seq)
                {
                    if (item.RefAxis == RefAxis.Y) return item.Value;
                }
                return 0;
            }
        }
        public double Z
        {
            get
            {
                foreach (var item in seq)
                {
                    if (item.RefAxis == RefAxis.Z) return item.Value;
                }
                return 0;
            }
        }
        public Shift()
        {

        }
        public Shift(double x, double y, double z)
        {
            seq.Add(new TranslationUnit(RefAxis.X, x));
            seq.Add(new TranslationUnit(RefAxis.Y, y));
            seq.Add(new TranslationUnit(RefAxis.Z, z));
            EndAddMatrix();
        }
    }

    public class Quaternion
    {
        public double W { get; set; } = 1;
        public Vector3D V { get; set; } = new Vector3D();

        public double[] QArray => new double[] { Q0, Q1, Q2, Q3 };
        public double Q0 => Math.Round(W,7);
        public double Q1 => Math.Round(V.X,7);
        public double Q2 => Math.Round(V.Y,7);
        public double Q3 => Math.Round(V.Z,7);
        public double Determin => Math.Round(Math.Sqrt(Math.Pow(Q0, 2) + Math.Pow(Q1, 2) + Math.Pow(Q2, 2) + Math.Pow(Q3, 2)),7);

        //public Matrix4x4 Matrix44
        //{
        //    get
        //    {
        //        Normalize();
        //        Matrix4x4 output = Matrix4x4.Identity;
        //        output.V00 = (float)(Math.Pow(Q0,2)+Math.Pow(Q1,2)-Math.Pow(Q2,2)-Math.Pow(Q3,2));
        //        output.V01 = (float)(2 * (Q1 * Q2 - Q0 * Q3));
        //        output.V02 = (float)(2 * (Q1 * Q3 + Q0 * Q2));
                
        //        output.V10 = (float)(2 * (Q1 * Q2 + Q0 * Q3));
        //        output.V11 = (float)(Math.Pow(Q0, 2) - Math.Pow(Q1, 2) + Math.Pow(Q2, 2) - Math.Pow(Q3, 2));
        //        output.V12 = (float)(2 * (Q2 * Q3 - Q0 * Q1));

        //        output.V20 = (float)(2 * (Q3 * Q1 - Q0 * Q2));
        //        output.V21 = (float)(2 * (Q3 * Q2 + Q0 * Q1));
        //        output.V22 = (float)(Math.Pow(Q0, 2) - Math.Pow(Q1, 2) - Math.Pow(Q2, 2) + Math.Pow(Q3, 2));
        //        return output;
        //    }
        //}
        public Quaternion()
        {

        }
        public Quaternion(MatrixUnit matrixUnit)
        {
            if(matrixUnit.Type == MatrixType.Rotate)
            {
                RotateUnit rotateUnit = matrixUnit as RotateUnit;
                if(rotateUnit != null)
                {
                    calculateQ(rotateUnit.RotateRadian, rotateUnit.RefAxis);
                }
            }
        }
        public Quaternion(double rotateRad,RefAxis refAxis)
        {
            calculateQ(rotateRad, refAxis);
        }
        
        private void calculateQ(double rotateRad,RefAxis refAxis)
        {
            W = Math.Cos(rotateRad/2);
            V = Math.Sin(rotateRad/2) * Vector3D.GetRefAxis(refAxis);
            Normalize();
        }
        /// <summary>
        /// output = left * right
        /// </summary>
        /// <param name="left">left side of the *</param>
        /// <param name="right">right side of the *</param>
        /// <returns>result Q</returns>
        public static Quaternion operator *(Quaternion left, Quaternion right) 
        {
            Quaternion q = new Quaternion();
            double resultW = left.W * right.W - Vector3D.Dot(left.V,right.V);
            q.W = resultW;

            Vector3D result1 = left.W * right.V;
            Vector3D result2 = right.W * left.V;
            Vector3D result3 = Vector3D.Cross(left.V, right.V);
            q.V = result1 + result2 + result3;
            q.Normalize();
            return q;
        }
        public void Normalize()
        {
            double sum = Math.Sqrt(Math.Pow(W, 2) + Math.Pow(V.X, 2) + Math.Pow(V.Y, 2) + Math.Pow(V.Z, 2));
            W /= sum;
            V.X /= sum;
            V.Y /= sum;
            V.Z /= sum;
        }

    }


}

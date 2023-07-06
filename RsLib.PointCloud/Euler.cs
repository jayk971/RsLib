using Accord.Math;
using System;
using System.Collections.Generic;

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
        public double RotateAngle { get => Value / Math.PI * 180.0; }
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
                    Coord.Vy = m_Func.Rotate(Coord.Vy, Coord.Vx, RotateAngle);
                    Coord.Vz = m_Func.Rotate(Coord.Vz, Coord.Vx, RotateAngle);
                    break;
                case RefAxis.Y:
                    Coord.Vx = m_Func.Rotate(Coord.Vx, Coord.Vy, RotateAngle);
                    Coord.Vz = m_Func.Rotate(Coord.Vz, Coord.Vy, RotateAngle);
                    break;
                case RefAxis.Z:
                    Coord.Vx = m_Func.Rotate(Coord.Vx, Coord.Vz, RotateAngle);
                    Coord.Vy = m_Func.Rotate(Coord.Vy, Coord.Vz, RotateAngle);
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
                    CalculateFinalMatrix();
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
        void calculateQ()
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
        public void CalculateFinalMatrix()
        {
            finalMatrix4 = Matrix4x4.Identity;
            for (int i = 0; i < seq.Count; i++)
            {
                finalMatrix4 = Matrix4x4.Multiply(seq[i].matrix4, finalMatrix4);
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
                return m_Func.Matrix4x4ToFloatArray(finalMatrix4);
            }
        }
        public float[,] FloatArray2dTranspose
        {
            get
            {
                return m_Func.Matrix4x4ToFloatArray(Transpose());
            }
        }


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
            List<RotateUnit> rSeq = solveRzRyRx(finalMatrix4);
            seq.AddRange(rSeq);
            isMatrixCalculated = true;

        }
        public Rotate(PointV3D p)
        {
            finalMatrix4 = collectMatricElement(p.Vx, p.Vy, p.Vz);
            List<RotateUnit> rSeq = solveRzRyRx(finalMatrix4);
            seq.AddRange(rSeq);
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
            List<RotateUnit> rSeq = solveRzRyRx(finalMatrix4);
            seq.AddRange(rSeq);
            isMatrixCalculated = true;

        }
        public void AddRotateSeq(RefAxis rotSeq, double rotateAngle)
        {
            RotateUnit rotateEulerUnit = new RotateUnit(rotSeq, rotateAngle);
            seq.Add(rotateEulerUnit);
            isMatrixCalculated = false;
        }
        /// <summary>
        /// 取得旋轉 Rz -> Ry -> Rx 順序的旋轉角
        /// </summary>
        /// <param name="vx">姿態的 x 向量</param>
        /// <param name="vy">姿態的 y 向量</param>
        /// <param name="vz">姿態的 z 向量</param>
        /// <returns>旋轉 Rz -> Ry -> Rx 順序的旋轉角</returns>
        public static List<RotateUnit> FromVector3DZYX(Vector3D vx, Vector3D vy, Vector3D vz)
        {
            /*
            V00,V01,V02,V03
            V10,V11,V12,V13
            V20,V21,V22,V23
            V30,V31,V32,V33
            */
            Matrix4x4 after = collectMatricElement(vx, vy, vz);
            return solveRzRyRx(after);
        }
        static Matrix4x4 collectMatricElement(Vector3D vx, Vector3D vy, Vector3D vz)
        {
            vx.UnitVector();
            vy.UnitVector();
            vz.UnitVector();
            Matrix4x4 matrix = Matrix4x4.Identity;

            matrix.V00 = (float)vx.X;
            matrix.V10 = (float)vx.Y;
            matrix.V20 = (float)vx.Z;

            matrix.V01 = (float)vy.X;
            matrix.V11 = (float)vy.Y;
            matrix.V21 = (float)vy.Z;

            matrix.V02 = (float)vz.X;
            matrix.V12 = (float)vz.Y;
            matrix.V22 = (float)vz.Z;
            return matrix;
        }
        static Matrix4x4 collectMatricElement(Vector3 vx, Vector3 vy, Vector3 vz)
        {
            vx.Normalize();
            vy.Normalize();
            vz.Normalize();
            Matrix4x4 matrix = Matrix4x4.Identity;

            matrix.V00 = (float)vx.X;
            matrix.V10 = (float)vx.Y;
            matrix.V20 = (float)vx.Z;

            matrix.V01 = (float)vy.X;
            matrix.V11 = (float)vy.Y;
            matrix.V21 = (float)vy.Z;

            matrix.V02 = (float)vz.X;
            matrix.V12 = (float)vz.Y;
            matrix.V22 = (float)vz.Z;
            return matrix;
        }
        static List<RotateUnit> solveRzRyRx(Matrix4x4 inMatrix)
        {
            RotateUnit rx = new RotateUnit(RefAxis.X);
            RotateUnit ry = new RotateUnit(RefAxis.Y);
            RotateUnit rz = new RotateUnit(RefAxis.Z);

            if (inMatrix.V20 < 1)
            {
                if (inMatrix.V20 > -1)
                {
                    ry.Value = Math.Asin(-inMatrix.V20);
                    rz.Value = Math.Atan2(inMatrix.V10, inMatrix.V00);
                    rx.Value = Math.Atan2(inMatrix.V21, inMatrix.V22);
                }
                else
                {
                    ry.Value = Math.PI / 2;
                    rz.Value = -Math.Atan2(-inMatrix.V12, inMatrix.V11);
                    rx.Value = 0;
                }
            }
            else
            {
                ry.Value = -Math.PI / 2;
                rz.Value = Math.Atan2(-inMatrix.V12, inMatrix.V11);
                rx.Value = 0;
            }
            List<RotateUnit> output = new List<RotateUnit>();
            output.Add(rz);
            output.Add(ry);
            output.Add(rx);
            return output;
        }
    }
    public class Shift : CoordMatrix
    {
        public Shift()
        {

        }
        public Shift(double x, double y, double z)
        {
            seq.Add(new TranslationUnit(RefAxis.X, x));
            seq.Add(new TranslationUnit(RefAxis.Y, y));
            seq.Add(new TranslationUnit(RefAxis.Z, z));
            CalculateFinalMatrix();
        }
    }


}

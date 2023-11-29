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
    public enum eMatrixType : int
    {
        Translation = 0,
        Rotate,
        Scale,
        //Reflection,
        //Shearing,
    }
    public enum eRefAxis : int
    {
        X,
        Y,
        Z
    }
    public enum eRotateType : int 
    {
        RigidBody = 0,
        Axis,
    }

    public abstract class MatrixUnit
    {
        public eRefAxis RefAxis = eRefAxis.X;
        public eMatrixType MatrixType = eMatrixType.Translation;
        public eRotateType RotateType = eRotateType.RigidBody;
        /// <summary>
        /// if matrix is rotation, value is radian;
        /// if matrix is translate, value is millimeter.
        /// </summary>
        public double Value = 0;
        public Matrix4x4 Matrix4 = Matrix4x4.Identity;


        public abstract Matrix4x4 GetMatrixInverse();
        public Matrix4x4 GetMatrixTranspose()
        {
            Matrix4x4 output = new Matrix4x4();
            output.V00 = Matrix4.V00;
            output.V01 = Matrix4.V10;
            output.V02 = Matrix4.V20;
            output.V03 = Matrix4.V30;
            output.V10 = Matrix4.V01;
            output.V11 = Matrix4.V11;
            output.V12 = Matrix4.V21;
            output.V13 = Matrix4.V31;
            output.V20 = Matrix4.V02;
            output.V21 = Matrix4.V12;
            output.V22 = Matrix4.V22;
            output.V23 = Matrix4.V32;
            output.V30 = Matrix4.V03;
            output.V31 = Matrix4.V13;
            output.V32 = Matrix4.V23;
            output.V33 = Matrix4.V33;
            return output;

        }
        public abstract void CalculateMatrix();

        public abstract string GetMatrixInfo();
        //void calculateMatrix()
        //{
        //    switch (Type)
        //    {
        //        case eMatrixType.Translation:

        //            calculateTranslationMatrix();

        //            break;

        //        case eMatrixType.Rotate:
        //            calculateRotateMatrix();
        //            break;
        //        case eMatrixType.Scale:

        //            calculateScaleMatrix();
        //            break;

        //        default:

        //            break;
        //    }
        //}
        //void calculateTranslationMatrix()
        //{
        //    matrix4 = Matrix4x4.Identity;
        //    switch (RefAxis)
        //    {
        //        case eRefAxis.X:
        //            matrix4.V03 = (float)Value;
        //            break;

        //        case eRefAxis.Y:
        //            matrix4.V13 = (float)Value;
        //            break;

        //        case eRefAxis.Z:
        //            matrix4.V23 = (float)Value;
        //            break;

        //        default:

        //            break;
        //    }
        //}
        //void calculateRotateMatrix()
        //{
        //    matrix4 = Matrix4x4.Identity;
        //    switch (RefAxis)
        //    {
        //        case eRefAxis.X:
        //            matrix4 = Matrix4x4.CreateRotationX((float)Value);
        //            break;

        //        case eRefAxis.Y:
        //            matrix4 = Matrix4x4.CreateRotationY((float)Value);
        //            break;

        //        case eRefAxis.Z:
        //            matrix4 = Matrix4x4.CreateRotationZ((float)Value);
        //            break;

        //        default:

        //            break;
        //    }
        //}
        //void calculateScaleMatrix()
        //{
        //    matrix4 = Matrix4x4.Identity;
        //    matrix4.V00 = (float)Value;
        //    matrix4.V11 = (float)Value;
        //    matrix4.V22 = (float)Value;
        //}
        //public Point3D Multiply(Point3D target)
        //{
        //    Vector4 output = Matrix4x4.Multiply(matrix4, new Vector4((float)target.X, (float)target.Y, (float)target.Z, 1f));
        //    return new Point3D(output.X, output.Y, output.Z);
        //}
        //public Vector3D Multiply(Vector3D target)
        //{
        //    Vector4 output = Matrix4x4.Multiply(matrix4, new Vector4((float)target.X, (float)target.Y, (float)target.Z, 1f));
        //    return new Vector3D(output.X, output.Y, output.Z);
        //}

        //public override string ToString()
        //{
        //    return $"{Type} {RefAxis} Axis {Value} Unit";
        //}
    }
    public class TranslationUnit : MatrixUnit
    {
         public TranslationUnit()
        {
            MatrixType = eMatrixType.Translation;
        }
        public TranslationUnit(eRefAxis inRefAxis, double shiftValue)
        {
            MatrixType = eMatrixType.Translation;
            RefAxis = inRefAxis;
            Value = shiftValue;
            CalculateMatrix();
        }

        public override Matrix4x4 GetMatrixInverse()
        {
            Matrix4x4 output = Matrix4;
            output.V03 = Matrix4.V03 * -1;
            output.V13 = Matrix4.V13 * -1;
            output.V23 = Matrix4.V23 * -1;
            return output;
        }
        public override void CalculateMatrix()
        {
            Matrix4 = Matrix4x4.Identity;
            switch (RefAxis)
            {
                case eRefAxis.X:
                    Matrix4.V03 = (float)Value;
                    break;

                case eRefAxis.Y:
                    Matrix4.V13 = (float)Value;
                    break;

                case eRefAxis.Z:
                    Matrix4.V23 = (float)Value;
                    break;

                default:

                    break;
            }
        }

        public override string GetMatrixInfo() => $"{MatrixType} {RefAxis}-Axis {Value} mm";
    }
    public class RotateUnit : MatrixUnit
    {
        public double RotateAngle => Math.Round(Value / Math.PI * 180.0,2);

        public double RotateRadian =>  Value; 
        public Matrix3x3 RotateMatrix3
        {
            get
            {
                Matrix3x3 m = Matrix3x3.Identity;
                m.V00 = Matrix4.V00;
                m.V01 = Matrix4.V01;
                m.V02 = Matrix4.V02;

                m.V10 = Matrix4.V10;
                m.V11 = Matrix4.V11;
                m.V12 = Matrix4.V12;

                m.V20 = Matrix4.V20;
                m.V21 = Matrix4.V21;
                m.V22 = Matrix4.V22;
                return m;
            }
        }

        public RotateUnit()
        {
            MatrixType = eMatrixType.Rotate;
            RotateType = eRotateType.RigidBody;
        }
        /// <summary>
        /// 加入對應軸旋轉弧度, 套用的旋轉矩陣為旋轉點雲的狀態, 而不是旋轉軸的狀態, 會根據 Rotate type 自動加正負號
        /// </summary>
        /// <param name="inRefAxis"></param>
        /// <param name="rotateType"></param>
        /// <param name="inRotateRad"></param>
        public RotateUnit(eRefAxis inRefAxis, eRotateType rotateType,double inRotateRad)
        {
            MatrixType = eMatrixType.Rotate;
            RotateType = rotateType;
            RefAxis = inRefAxis;

            Value =inRotateRad;
            CalculateMatrix();
        }

        public void Rotate(ref PointV3D Coord)
        {
            switch (RefAxis)
            {
                case eRefAxis.X:
                    Coord.Vy = PointCloudCommon.Rotate(Coord.Vy, Coord.Vx, RotateAngle);
                    Coord.Vz = PointCloudCommon.Rotate(Coord.Vz, Coord.Vx, RotateAngle);
                    break;
                case eRefAxis.Y:
                    Coord.Vx = PointCloudCommon.Rotate(Coord.Vx, Coord.Vy, RotateAngle);
                    Coord.Vz = PointCloudCommon.Rotate(Coord.Vz, Coord.Vy, RotateAngle);
                    break;
                case eRefAxis.Z:
                    Coord.Vx = PointCloudCommon.Rotate(Coord.Vx, Coord.Vz, RotateAngle);
                    Coord.Vy = PointCloudCommon.Rotate(Coord.Vy, Coord.Vz, RotateAngle);
                    break;
                default:
                    break;
            }
        }


        public override Matrix4x4 GetMatrixInverse() => GetMatrixTranspose();

        public override void CalculateMatrix()
        {
            Matrix4 = Matrix4x4.Identity;
            float finalValue = RotateType == eRotateType.RigidBody ? (float) Value : (float)-Value;
            switch (RefAxis)
            {
                case eRefAxis.X:
                    Matrix4 = Matrix4x4.CreateRotationX((float)finalValue);
                    break;

                case eRefAxis.Y:
                    Matrix4 = Matrix4x4.CreateRotationY((float)finalValue);
                    break;

                case eRefAxis.Z:
                    Matrix4 = Matrix4x4.CreateRotationZ((float)finalValue);
                    break;

                default:

                    break;
            }
        }
        public override string GetMatrixInfo() => $"{MatrixType} {RotateType} {RefAxis}-Axis : {RotateAngle} deg ({RotateRadian} rad)";
    }
    public class CoordMatrix
    {
        internal string Name = "";
        public List<MatrixUnit> MatrixSequnce { get; private set; } = new List<MatrixUnit>();
        protected Matrix4x4 finalMatrix4 = Matrix4x4.Identity;
        public Matrix4x4 FinalMatrix4
        {
            get
            {
                if (IsMatrixCalculated == false)
                {
                    EndAddMatrix();
                }
                return finalMatrix4;
            }

        }

        public bool IsMatrixCalculated { get; internal set; } = false;

        public CoordMatrix()
        {

        }

        /// <summary>
        /// 加入平移或旋轉矩陣
        /// </summary>
        /// <param name="axis">參考軸</param>
        /// <param name="matrixType">矩陣類型</param>
        /// <param name="setValue">平移矩陣為平移距離 ; 旋轉矩陣為旋轉弧度, 不是角度</param>
        public void AddSeq(eRefAxis axis, eMatrixType matrixType, double setValue)
        {
            switch (matrixType)
            {
                case eMatrixType.Translation:
                    TranslationUnit sUnit = new TranslationUnit(axis, setValue);
                    MatrixSequnce.Add(sUnit);
                    break;

                case eMatrixType.Rotate:
                    RotateUnit rUnit = new RotateUnit(axis, eRotateType.RigidBody,setValue);
                    MatrixSequnce.Add(rUnit);
                    break;

                default:

                    break;
            }
            IsMatrixCalculated = false;
        }
        public void AddSeq(MatrixUnit otherUnit)
        {
            MatrixSequnce.Add(otherUnit);
            IsMatrixCalculated = false;
        }
        public void AddSeq(List<MatrixUnit> otherSeq)
        {
            MatrixSequnce.AddRange(otherSeq);
            IsMatrixCalculated = false;
        }
        public void AddSeq(CoordMatrix otherSeq)
        {
            MatrixSequnce.AddRange(otherSeq.MatrixSequnce);
            IsMatrixCalculated = false;
        }

        public void Clear()
        {
            MatrixSequnce.Clear();
            finalMatrix4 = Matrix4x4.Identity;
            IsMatrixCalculated = false;
        }
        public void EndAddMatrix()
        {
            finalMatrix4 = Matrix4x4.Identity;
            for (int i = 0; i < MatrixSequnce.Count; i++)
            {
                finalMatrix4 = Matrix4x4.Multiply(MatrixSequnce[i].Matrix4,finalMatrix4);
            }
            IsMatrixCalculated = true;
        }
        public Matrix4x4 GetMatrixInverse()
        {
            Matrix4x4 m = Matrix4x4.Identity;
            for (int i = 0; i < MatrixSequnce.Count; i++)
            {
                switch (MatrixSequnce[i].MatrixType)
                {
                    case eMatrixType.Rotate:
                        RotateUnit rUnit = MatrixSequnce[i] as RotateUnit;
                        m = Matrix4x4.Multiply(rUnit.GetMatrixInverse(), m);
                        break;

                    case eMatrixType.Translation:
                        TranslationUnit sUnit = MatrixSequnce[i] as TranslationUnit;
                        m = Matrix4x4.Multiply(sUnit.GetMatrixInverse(), m);
                        break;

                    default:

                        break;
                }
            }
            return m;

        }

        public Matrix4x4 GetMatrixTranspose()
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

        //public static void SolveMatrixRzRyRxShift(Matrix4x4 m,out Rotate r, out Shift s)
        //{
        //    s = new Shift(m.V03,m.V13,m.V23);
        //    Vector3D vx = new Vector3D(m.V00,m.V10,m.V20);
        //    Vector3D vy = new Vector3D(m.V01, m.V11, m.V21);
        //    Vector3D vz = new Vector3D(m.V02, m.V12, m.V22);

        //    vx.UnitVector();
        //    vy.UnitVector();
        //    vz.UnitVector();

        //    r = new Rotate(vx, vy, vz);

        //}

        public static void SolveTzTyTx(Matrix4x4 inMatrix,out Shift s)
        {
            s = new Shift(inMatrix.V03, inMatrix.V13, inMatrix.V23);
        }
    }
    public class RotateAxis : CoordMatrix
    {
        public eRotateType RotationType => eRotateType.Axis;
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
                if (MatrixSequnce.Count == 3)
                {
                    return MatrixSequnce[0].Value / Math.PI * 180;
                }
                else return 0.0;
            }
        }
        public double Ry
        {
            get
            {
                if (MatrixSequnce.Count == 3)
                {
                    return MatrixSequnce[1].Value / Math.PI * 180;
                }
                else return 0.0;
            }
        }
        public double Rx
        {
            get
            {
                if (MatrixSequnce.Count == 3)
                {
                    return MatrixSequnce[2].Value / Math.PI * 180;
                }
                else return 0.0;
            }
        }

        public float[,] FloatArray2d =>PointCloudCommon.Matrix4x4ToFloatArray(finalMatrix4);
        
        public float[,] FloatArray2dTranspose =>PointCloudCommon.Matrix4x4ToFloatArray(GetMatrixTranspose());
            

        public Quaternion Q = new Quaternion();
        public RotateAxis()
        {
        }
        /// <summary>
        /// 輸入旋轉後坐標系, 計算出世界座標->旋轉後坐標系的矩陣
        /// </summary>
        /// <param name="vx">旋轉後坐標系 x 相對於世界坐標系</param>
        /// <param name="vy">旋轉後坐標系 y 相對於世界坐標系</param>
        /// <param name="vz">旋轉後坐標系 z 相對於世界坐標系</param>
        public RotateAxis(Vector3D vx, Vector3D vy, Vector3D vz)
        {
            finalMatrix4 = collectMatricElement(vx, vy, vz);
            SolveRzRyRx(finalMatrix4,out RotateUnit rx,out RotateUnit ry,out RotateUnit rz);
            MatrixSequnce.Add(rz);
            MatrixSequnce.Add(ry);
            MatrixSequnce.Add(rx);
            SolveQ();
            IsMatrixCalculated = true;

        }
        public RotateAxis(PointV3D p)
        {
            finalMatrix4 = collectMatricElement(p.Vx, p.Vy, p.Vz);
            SolveRzRyRx(finalMatrix4, out RotateUnit rx, out RotateUnit ry, out RotateUnit rz);
            MatrixSequnce.Add(rz);
            MatrixSequnce.Add(ry);
            MatrixSequnce.Add(rx);
            SolveQ();
            IsMatrixCalculated = true;

        }
        /// <summary>
        /// 輸入旋轉後坐標系, 計算出世界座標->旋轉後坐標系的矩陣
        /// </summary>
        /// <param name="vx">旋轉後坐標系 x 相對於世界坐標系</param>
        /// <param name="vy">旋轉後坐標系 y 相對於世界坐標系</param>
        /// <param name="vz">旋轉後坐標系 z 相對於世界坐標系</param>
        public RotateAxis(Vector3 vx, Vector3 vy, Vector3 vz)
        {
            finalMatrix4 = collectMatricElement(vx, vy, vz);
            SolveRzRyRx(finalMatrix4,out RotateUnit rx, out RotateUnit ry, out RotateUnit rz);
            MatrixSequnce.Add(rz);
            MatrixSequnce.Add(ry);
            MatrixSequnce.Add(rx);
            SolveQ();
            IsMatrixCalculated = true;
        }

        Matrix4x4 collectMatricElement(Vector3D vx, Vector3D vy, Vector3D vz)
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
        Matrix4x4 collectMatricElement(Vector3 vx, Vector3 vy, Vector3 vz)
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
        public void AddRotateSeq(eRefAxis rotSeq, double rotateAngle)
        {
            RotateUnit rotateEulerUnit = new RotateUnit(rotSeq, eRotateType.Axis, rotateAngle);
            MatrixSequnce.Add(rotateEulerUnit);
            Q *= new Quaternion(rotateEulerUnit);
            IsMatrixCalculated = false;
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
        public static void SolveRzRyRx(Matrix4x4 inMatrix,out RotateUnit Rx, out RotateUnit Ry, out RotateUnit Rz)
        {
            double rx = 0, ry = 0, rz = 0;
            if (inMatrix.V02 < 1)
            {
                if (inMatrix.V02 > -1)
                {
                    rx = Math.Atan2(inMatrix.V12, inMatrix.V22);
                    ry = -Math.Asin(inMatrix.V02);
                    rz = Math.Atan2(inMatrix.V01, inMatrix.V00);
                }
                else // inMatrix.V02 == -1
                {
                    rx = Math.Atan2(inMatrix.V10, inMatrix.V11);
                    ry = Math.PI / 2;
                    rz = 0;
                }
            }
            else // inMatrix.V02 == 1
            {
                rx = -Math.Atan2(inMatrix.V10, inMatrix.V11);
                ry = -Math.PI / 2;
                rz = 0;
            }

            Rx = new RotateUnit(eRefAxis.X, eRotateType.Axis, rx);
            Ry = new RotateUnit(eRefAxis.Y, eRotateType.Axis, ry);
            Rz = new RotateUnit(eRefAxis.Z, eRotateType.Axis, rz);

        }

        public void SolveQ()
        {
            Q = new Quaternion();
            for (int i = 0; i < MatrixSequnce.Count; i++)
            {
                 if(MatrixSequnce[i].MatrixType == eMatrixType.Rotate)
                {
                    Quaternion tempQ = new Quaternion(MatrixSequnce[i]);
                    Q *= tempQ;
                }
            }
        }
    }


    public class RotateRigidBody : CoordMatrix
    {
        public eRotateType RotationType => eRotateType.RigidBody;
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
                if (MatrixSequnce.Count == 3)
                {
                    return MatrixSequnce[0].Value / Math.PI * 180;
                }
                else return 0.0;
            }
        }
        public double Ry
        {
            get
            {
                if (MatrixSequnce.Count == 3)
                {
                    return MatrixSequnce[1].Value / Math.PI * 180;
                }
                else return 0.0;
            }
        }
        public double Rx
        {
            get
            {
                if (MatrixSequnce.Count == 3)
                {
                    return MatrixSequnce[2].Value / Math.PI * 180;
                }
                else return 0.0;
            }
        }

        public float[,] FloatArray2d =>PointCloudCommon.Matrix4x4ToFloatArray(finalMatrix4);
        
        public float[,] FloatArray2dTranspose =>PointCloudCommon.Matrix4x4ToFloatArray(GetMatrixTranspose());
        public Quaternion Q = new Quaternion();
        public RotateRigidBody()
        {
        }

        public void AddRotateSeq(eRefAxis rotSeq, double rotateRad)
        {
            RotateUnit rotateEulerUnit = new RotateUnit(rotSeq, eRotateType.RigidBody,rotateRad);
            MatrixSequnce.Add(rotateEulerUnit);
            Q*=new Quaternion(rotateEulerUnit);
            IsMatrixCalculated = false;
        }
        public static void SolveRzRyRx(Matrix4x4 inMatrix, out RotateUnit Rx, out RotateUnit Ry, out RotateUnit Rz)
        {
            double rx = 0, ry = 0, rz = 0;
            if (inMatrix.V02 < 1)
            {
                if (inMatrix.V02 > -1)
                {
                    rx = Math.Atan2(inMatrix.V12, inMatrix.V22);
                    ry = -Math.Asin(inMatrix.V02);
                    rz = Math.Atan2(inMatrix.V01, inMatrix.V00);
                }
                else // inMatrix.V02 == -1
                {
                    rx = Math.Atan2(inMatrix.V10, inMatrix.V11);
                    ry = Math.PI / 2;
                    rz = 0;
                }
            }
            else // inMatrix.V02 == 1
            {
                rx = -Math.Atan2(inMatrix.V10, inMatrix.V11);
                ry = -Math.PI / 2;
                rz = 0;
            }
            rx *= -1;
            ry *= -1;
            rz *= -1;

            Rx = new RotateUnit(eRefAxis.X, eRotateType.RigidBody, rx);
            Ry = new RotateUnit(eRefAxis.Y, eRotateType.RigidBody, ry);
            Rz = new RotateUnit(eRefAxis.Z, eRotateType.RigidBody, rz);

        }

    }
    public class Shift : CoordMatrix
    {
        public double X
        {
            get
            {
                double output = 0;
                foreach (var item in MatrixSequnce)
                {
                    if (item.RefAxis == eRefAxis.X) output+= item.Value;
                }
                return output;
            }
        }
        public double Y
        {
            get
            {
                double output = 0;

                foreach (var item in MatrixSequnce)
                {
                    if (item.RefAxis == eRefAxis.Y) output+= item.Value;
                }
                return output;
            }
        }
        public double Z
        {
            get
            {
                double output = 0;

                foreach (var item in MatrixSequnce)
                {
                    if (item.RefAxis == eRefAxis.Z) output+= item.Value;
                }
                return output;
            }
        }
        public Shift()
        {

        }
        public Shift(double x, double y, double z)
        {
            MatrixSequnce.Add(new TranslationUnit(eRefAxis.X, x));
            MatrixSequnce.Add(new TranslationUnit(eRefAxis.Y, y));
            MatrixSequnce.Add(new TranslationUnit(eRefAxis.Z, z));
            EndAddMatrix();
        }
    }
    [Serializable]
    public class Quaternion
    {
        public double W { get; set; } = 1;
        public Vector3D V { get; set; } = new Vector3D();

        public double[] QArray => new double[] { Q0, Q1, Q2, Q3 };
        public double Q0 => Math.Round(W, 7);
        public double Q1 => Math.Round(V.X, 7);
        public double Q2 => Math.Round(V.Y, 7);
        public double Q3 => Math.Round(V.Z, 7);
        public double Determin => Math.Round(Math.Sqrt(Math.Pow(Q0, 2) + Math.Pow(Q1, 2) + Math.Pow(Q2, 2) + Math.Pow(Q3, 2)), 7);

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
            if (matrixUnit.MatrixType == eMatrixType.Rotate)
            {
                RotateUnit rotateUnit = matrixUnit as RotateUnit;
                if (rotateUnit != null)
                {
                    calculateQ(rotateUnit.RotateRadian, rotateUnit.RefAxis);
                }
            }
        }
        public Quaternion(double rotateRad, eRefAxis refAxis)
        {
            calculateQ(rotateRad, refAxis);
        }
        public override string ToString()
        {
            return $"{W:F6},{Q1:F6},{Q2:F6},{Q3:F6}";
        }
        private void calculateQ(double rotateRad,eRefAxis refAxis)
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

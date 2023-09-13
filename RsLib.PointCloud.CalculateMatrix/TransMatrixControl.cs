using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Accord.Math;
using RsLib.Common;

namespace RsLib.PointCloudLib.CalculateMatrix
{
    public partial class TransMatrixControl : UserControl
    {
        public TransMatrixControl()
        {
            InitializeComponent();
        }

        private void btn_EulerToMatrix_Click(object sender, EventArgs e)
        {

            calculateEuler(out CoordMatrix c, out Rotate r, out Shift s);
            updateMatrixTextbox(c.FinalMatrix4);
            updateQ(r.Q);

        }
        void calculateEuler(out CoordMatrix c,out Rotate r, out Shift s)
        {
            c = new CoordMatrix();
            r = new Rotate();
            r.AddRotateSeq(RefAxis.Z, double.Parse(tbx_RZ.Text));
            r.AddRotateSeq(RefAxis.Y, double.Parse(tbx_RY.Text));
            r.AddRotateSeq(RefAxis.X, double.Parse(tbx_RX.Text));
            r.EndAddMatrix();


            s = new Shift();
            s.AddSeq(RefAxis.X, MatrixType.Translation, double.Parse(tbx_SX.Text));
            s.AddSeq(RefAxis.Y, MatrixType.Translation, double.Parse(tbx_SY.Text));
            s.AddSeq(RefAxis.Z, MatrixType.Translation, double.Parse(tbx_SZ.Text));
            s.EndAddMatrix();

            c.AddSeq(r);
            c.AddSeq(s);
            c.EndAddMatrix();
        }
        void updateQ(Quaternion q)
        {
            lbl_Q0.Text = q.Q0.ToString();
            lbl_Q1.Text = q.Q1.ToString();
            lbl_Q2.Text = q.Q2.ToString();
            lbl_Q3.Text = q.Q3.ToString();
        }
        void updateMatrixTextbox(Matrix4x4 m)
        {
            tbx_M00.Text = m.V00.ToString();
            tbx_M01.Text = m.V01.ToString();
            tbx_M02.Text = m.V02.ToString();
            tbx_M03.Text = m.V03.ToString();
            tbx_M10.Text = m.V10.ToString();
            tbx_M11.Text = m.V11.ToString();
            tbx_M12.Text = m.V12.ToString();
            tbx_M13.Text = m.V13.ToString();
            tbx_M20.Text = m.V20.ToString();
            tbx_M21.Text = m.V21.ToString();
            tbx_M22.Text = m.V22.ToString();
            tbx_M23.Text = m.V23.ToString();
            tbx_M30.Text = m.V30.ToString();
            tbx_M31.Text = m.V31.ToString();
            tbx_M32.Text = m.V32.ToString();
            tbx_M33.Text = m.V33.ToString();
        }

        void getMatrix(out Matrix4x4 m)
        {
            m = Matrix4x4.Identity;
            m.V00 = float.Parse(tbx_M00.Text);
            m.V01 = float.Parse(tbx_M01.Text);
            m.V02 = float.Parse (tbx_M02.Text);
            m.V03 = float.Parse(tbx_M03.Text);

            m.V10 = float.Parse(tbx_M10.Text);
            m.V11 = float.Parse(tbx_M11.Text);
            m.V12 = float.Parse(tbx_M12.Text);
            m.V13 = float.Parse(tbx_M13.Text);

            m.V20 = float.Parse(tbx_M20.Text);
            m.V21 = float.Parse(tbx_M21.Text);
            m.V22 = float.Parse(tbx_M22.Text);
            m.V23 = float.Parse(tbx_M23.Text);

            m.V30 = float.Parse(tbx_M30.Text);
            m.V31 = float.Parse(tbx_M31.Text);
            m.V32 = float.Parse(tbx_M32.Text);
            m.V33 = float.Parse(tbx_M33.Text);
        }
        private void btn_MatrixToEuler_Click(object sender, EventArgs e)
        {
            getMatrix(out Matrix4x4 m);
            CoordMatrix.SolveMatrixRzRyRxShift(m, out Rotate r, out Shift s);
            updateEuler(r, s);
        }
        void updateEuler(Rotate r,Shift s)
        {
            tbx_RX.Text = r.Rx.ToString();
            tbx_RY.Text = r.Ry.ToString();
            tbx_RZ.Text = r.Rz.ToString();

            tbx_SX.Text = s.X.ToString();
            tbx_SY.Text = s.Y.ToString();
            tbx_SZ.Text = s.Z.ToString();

        }
        private void btn_ResetIdentity_Click(object sender, EventArgs e)
        {
            updateMatrixTextbox(Matrix4x4.Identity);
            updateQ(new Quaternion());
        }

        private void tbx_RX_KeyPress(object sender, KeyPressEventArgs e)
        {
             e.Handled =  FT_Functions.double_Positive_Negative_KeyPress(e.KeyChar);
        }

        private void btn_ResetEuler_Click(object sender, EventArgs e)
        {
            tbx_RX.Text = "0";
            tbx_RY.Text = "0";
            tbx_RZ.Text = "0";

            tbx_SX.Text = "0";
            tbx_SY.Text = "0";
            tbx_SZ.Text = "0";
        }
    }
}

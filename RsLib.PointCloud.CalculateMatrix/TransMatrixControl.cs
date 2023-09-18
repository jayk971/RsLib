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
            if (rbn_RigidBody.Checked)
            {
                calculateEuler_RigidBody(out CoordMatrix c, out RotateRigidBody r, out Shift s);
                updateMatrixTextbox(c.FinalMatrix4);
                updateQ(r.Q);
            }
            else
            {
                calculateEuler_Axis(out CoordMatrix c, out RotateAxis r, out Shift s);
                updateMatrixTextbox(c.FinalMatrix4);
                updateQ(r.Q);
            }
        }
        void calculateEuler_RigidBody(out CoordMatrix c,out RotateRigidBody r, out Shift s)
        {
            c = new CoordMatrix();
            r = new RotateRigidBody();
            r.AddRotateSeq(eRefAxis.Z, double.Parse(tbx_RZ.Text)/180*Math.PI);
            r.AddRotateSeq(eRefAxis.Y, double.Parse(tbx_RY.Text) / 180 * Math.PI);
            r.AddRotateSeq(eRefAxis.X, double.Parse(tbx_RX.Text) / 180 * Math.PI);
            r.EndAddMatrix();


            s = new Shift();
            s.AddSeq(eRefAxis.X, eMatrixType.Translation, double.Parse(tbx_SX.Text));
            s.AddSeq(eRefAxis.Y, eMatrixType.Translation, double.Parse(tbx_SY.Text));
            s.AddSeq(eRefAxis.Z, eMatrixType.Translation, double.Parse(tbx_SZ.Text));
            s.EndAddMatrix();

            c.AddSeq(r);
            c.AddSeq(s);
            c.EndAddMatrix();
        }
        void calculateEuler_Axis(out CoordMatrix c, out RotateAxis r, out Shift s)
        {
            c = new CoordMatrix();
            r = new RotateAxis();
            r.AddRotateSeq(eRefAxis.Z, double.Parse(tbx_RZ.Text) / 180 * Math.PI);
            r.AddRotateSeq(eRefAxis.Y, double.Parse(tbx_RY.Text) / 180 * Math.PI);
            r.AddRotateSeq(eRefAxis.X, double.Parse(tbx_RX.Text) / 180 * Math.PI);
            r.EndAddMatrix();


            s = new Shift();
            s.AddSeq(eRefAxis.X, eMatrixType.Translation, double.Parse(tbx_SX.Text));
            s.AddSeq(eRefAxis.Y, eMatrixType.Translation, double.Parse(tbx_SY.Text));
            s.AddSeq(eRefAxis.Z, eMatrixType.Translation, double.Parse(tbx_SZ.Text));
            s.EndAddMatrix();

            c.AddSeq(r);
            c.AddSeq(s);
            c.EndAddMatrix();
        }
        void updateQ(Quaternion q)
        {
            rtbx_Q.Clear();
            rtbx_Q.AppendText(q.ToString());
        }
        void updateMatrixTextbox(Matrix4x4 m)
        {
            string mStr = PointCloudCommon.Matrix4x4ToString(m);
            rtbx_M.Clear();
            rtbx_M.AppendText(mStr);
        }

        void getMatrix(char splitChar, out Matrix4x4 m)
        {
          m =  PointCloudCommon.ParseMatrix4x4(rtbx_M.Text, splitChar);
        }
        private void btn_MatrixToEuler_Click(object sender, EventArgs e)
        {
            getMatrix(',' ,out Matrix4x4 m);
            RotateUnit rx = new RotateUnit();
            RotateUnit ry = new RotateUnit();
            RotateUnit rz = new RotateUnit();

            Shift s = new Shift();
            if(rbn_RigidBody.Checked)
            {
                RotateRigidBody.SolveRzRyRx(m, out rx, out ry, out rz);
            }
            else
            {
                RotateAxis.SolveRzRyRx(m, out rx, out ry, out rz);
            }

            CoordMatrix.SolveTzTyTx(m,out s);
            updateEulerTextBox(rx,ry,rz, s);
            updateQ(new Quaternion());
        }
        void updateEulerTextBox(RotateUnit rx, RotateUnit ry,RotateUnit rz,Shift s)
        {
            updateEulerTextBox(s.X, s.Y, s.Z, rx.RotateAngle, ry.RotateAngle, rz.RotateAngle);
        }
        void updateEulerTextBox(double tx,double ty,double tz,double rx,double ry,double rz)
        {
            tbx_RX.Text = rx.ToString();
            tbx_RY.Text = ry.ToString();
            tbx_RZ.Text = rz.ToString();

            tbx_SX.Text = tx.ToString();
            tbx_SY.Text = ty.ToString();
            tbx_SZ.Text = tz.ToString();

        }

        private void btn_ResetIdentity_Click(object sender, EventArgs e)
        {
            rbn_RigidBody.Checked = true;
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

        private void btn_SaveMatrix_Click(object sender, EventArgs e)
        {
            using (SaveFileDialog sf = new SaveFileDialog())
            {
                sf.Filter = "Matrix 4x4 File|*.m44";
                if(sf.ShowDialog() == DialogResult.OK)
                {
                    string filePath = sf.FileName;
                    getMatrix(',', out Matrix4x4 m);
                    PointCloudCommon.SaveMatrix4x4(m, filePath,',');
                }
            }
        }

        private void btn_LoadEulerData_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog op = new OpenFileDialog())
            {
                op.Filter = "Halcon Transform Dat File|*.dat";
                if (op.ShowDialog() == DialogResult.OK)
                {
                    string filePath = op.FileName;
                    PointCloudCommon.LoadMatrix4x4ArrayFromHalconDatFile(filePath, out Matrix4x4 m, out Tuple<double, double, double, double, double, double> t);
                    updateMatrixTextbox(m);
                    updateEulerTextBox(t.Item1, t.Item2, t.Item3, t.Item4, t.Item5, t.Item6);
                }

            }
        }
    }
}

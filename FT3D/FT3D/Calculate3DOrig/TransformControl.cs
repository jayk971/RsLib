using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using RsLib.PointCloud;
using Accord.Math;
namespace RsLib.PointCloud.CalculateMatrix
{
    public partial class TransformControl : UserControl
    {
        public TransformControl()
        {
            InitializeComponent();
        }

        private void btn_OpenXYZ_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog op = new OpenFileDialog())
            {
                op.Filter = "XYZ File|*.xyz";
                if(op.ShowDialog() == DialogResult.OK)
                {
                    lbl_XYZFilePath.Text = op.FileName;
                }
            }
        }

        private void btn_openM44_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog op = new OpenFileDialog())
            {
                op.Filter = "Matrix4x4 File|*.m44";
                if (op.ShowDialog() == DialogResult.OK)
                {
                    lbl_M44FilePath.Text = op.FileName;
                }
            }
        }

        private void btn_Calculate_Click(object sender, EventArgs e)
        {
            PointCloud pc = new PointCloud();
            pc.LoadFromFile(lbl_XYZFilePath.Text,false);

            double[,] mArr = m_Func.LoadMatrix4x4ArrayFromFile(lbl_M44FilePath.Text);

            PointCloud p = pc.Multiply(mArr);

            p.Save(lbl_XYZFilePath.Text.Replace(".xyz", "_T.xyz"));
        }
    }
}

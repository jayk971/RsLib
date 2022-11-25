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
using System.IO;
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
                op.Filter = "Matrix 4x4 with data|*.m44d|Matrix4x4 File|*.m44";
                if (op.ShowDialog() == DialogResult.OK)
                {
                    lbl_M44FilePath.Text = op.FileName;
                }
            }
        }

        private void btn_Calculate_Click(object sender, EventArgs e)
        {
            if (File.Exists(lbl_XYZFilePath.Text))
            {
                PointCloud pc = new PointCloud();
                pc.LoadFromFile(lbl_XYZFilePath.Text, false);

                if(pc.Count ==0)
                {
                    MessageBox.Show($"Test cloud file : {lbl_XYZFilePath.Text} load fail. Points count = 0");
                    return;
                }

                if (File.Exists(lbl_M44FilePath.Text))
                {
                    double[,] mArr = m_Func.LoadMatrix4x4ArrayFromFile(lbl_M44FilePath.Text);

                    PointCloud p = pc.Multiply(mArr);

                    string filePath = lbl_XYZFilePath.Text.Replace(".xyz", $"_{DateTime.Now:yyMMddHHmmss}.xyz");
                    p.Save(filePath);
                    MessageBox.Show($"{filePath} saved");
                }
                else
                {
                    MessageBox.Show($"Transform matrix file : {lbl_M44FilePath.Text} not exist.");
                }
            }
            else
            {
                MessageBox.Show($"Test cloud file : {lbl_XYZFilePath.Text} not exist.");
            }
        }
    }
}

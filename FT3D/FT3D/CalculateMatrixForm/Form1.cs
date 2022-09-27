using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using RsLib.PointCloud;
using RsLib.PointCloud.CalculateMatrix;
namespace CalculateMatrixForm
{
    public partial class Form1 : Form
    {
        CalculateMatrixControl cmc = new CalculateMatrixControl();
        TransformControl tc = new TransformControl();
        public Form1()
        {
            InitializeComponent();
            cmc.Dock = DockStyle.Fill;
            tabPage1.Controls.Add(cmc);
            tc.Dock = DockStyle.Fill;
            tabPage2.Controls.Add(tc);
        }
    }
}

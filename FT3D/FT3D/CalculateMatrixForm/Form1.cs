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
        public Form1()
        {
            InitializeComponent();
            cmc.Dock = DockStyle.Fill;
            this.Controls.Add(cmc);
        }
    }
}

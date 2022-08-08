using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using FT.DXF;
namespace TestForm
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        DXFReader dr = new DXFReader();
        private void button1_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog op = new OpenFileDialog())
            {
                op.Filter = "DXF File|*.dxf";
                if(op.ShowDialog() == DialogResult.OK)
                {
                    dr.LoadDXF(op.FileName);

                    dr.Scale(5.0);
                    
                    using (SaveFileDialog sf = new SaveFileDialog())
                    {
                        sf.Filter = "DXF File|*.dxf";
                        if(sf.ShowDialog() == DialogResult.OK)
                        {
                            dr.Save(sf.FileName);
                        }
                    }
                }
            }
        }
    }
}

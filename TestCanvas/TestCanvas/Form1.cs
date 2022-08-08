using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using System.Windows.Controls;
using FT.Display;
using FT.DXF;
using FT.DXF2Display;
namespace TestCanvas
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            ftDisplay1.GetSelectedItem += FtDisplay1_SelectedItem;
            ftDisplay1.MouseDoubleClickPos += FtDisplay1_MouseDoubleClickPos;
            ftDisplay1.SetCoordinate(CoordDir.X_InvertY);
            //FtRectangle ftRectangle = new FtRectangle();
            //ftRectangle.SetXYWidthHeight(0, 0, 100, 200);
            //ftDisplay1.AddInteractiveGraphics("TestRectangle", ftRectangle);
            //FtCircle ftCircle = new FtCircle();
            //ftCircle.SetCenterRadius(100, 50, 40);
            //ftCircle.IsFill = true;

            //ftDisplay1.UpdateDisplay(false);

        }

        private void FtDisplay1_MouseDoubleClickPos(double x, double y)
        {
            //MessageBox.Show(string.Format("{0:F1} , {1:F1}", x, y));

            FtPoint DoubleClickPoint = new FtPoint(x, y);
            DoubleClickPoint.Name = string.Format("{0}-{1}", x, y);
            DoubleClickPoint.DisplayPara.PointSize = 1;
            DoubleClickPoint.SelectedDisplayPara.PointSize = 1;
            DoubleClickPoint.ToolTipText = DoubleClickPoint.Name;
            ftDisplay1.AddInteractiveGraphics(DoubleClickPoint.Name, DoubleClickPoint);


            //ftDisplay1.UpdateDisplay(false);
        }

        private void FtDisplay1_SelectedItem(string ItemName)
        {
            label1.Text = ItemName;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            OpenFileDialog op = new OpenFileDialog();
            op.Filter = "DXF File|*.dxf";
            if(op.ShowDialog() == DialogResult.OK)
            {
                comboBox1.Items.Clear();
                string FilePath = op.FileName;
                DXFReader dr = new DXFReader();
                dr.LoadDXF(FilePath);
                ftDisplay1.Clear();


                ftDisplay1.DrawingEnabled = false;
                FtPoint CenterP = new FtPoint(dr.Avg.X, dr.Avg.Y);
                CenterP.Name = "Center";
                CenterP.DisplayPara.PointSize = 1;
                CenterP.SelectedDisplayPara.PointSize = 1;
                CenterP.ToolTipText = CenterP.Name;
                ftDisplay1.AddInteractiveGraphics(CenterP.Name, CenterP);

                int count = 0;
                foreach (KeyValuePair<string, DXFItem> kvp in dr._Items)
                {
                    comboBox1.Items.Add(kvp.Key);
                    FtCompositeShape fc1 = new FtCompositeShape();
                    fc1 = ToDisplay.Convert(kvp.Value);
                    fc1.Name = kvp.Key;
                    fc1.ToolTipText = kvp.Key;
                    fc1.DOF = FtObjectDOF.Shift;

                    ftDisplay1.AddInteractiveGraphics(kvp.Key, fc1);
                    count++;
                }
                //ftDisplay1.UpdateDisplay();
                ftDisplay1.DrawingEnabled = true;
                System.Drawing.Image img = ftDisplay1.CreateContentBitmap();
                pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;
                pictureBox1.Image = img;
                img.Save("d:\\tetetetet.png", System.Drawing.Imaging.ImageFormat.Png);
                GC.Collect();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            ftDisplay1.SaveAllRangeImage("d:\\test.png", 3.2);
            //System.Drawing.Image img = ftDisplay1.CreateContentBitmap(1562,954);
            //img.Save("d:\\tetetetet.png", System.Drawing.Imaging.ImageFormat.Png);
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox1.Items.Count == 0) return;
            if (comboBox1.SelectedIndex == -1) return;

            string SelectedItemName = comboBox1.SelectedItem.ToString();

            ftDisplay1.SelectItem(SelectedItemName);
        }
    }
}

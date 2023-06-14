using RsLib.PointCloud.CalculateMatrix.Properties;
using System;
using System.IO;
using System.Text;
using System.Windows.Forms;
namespace RsLib.PointCloud.CalculateMatrix
{
    public partial class CalculateMatrixControl : UserControl
    {
        Point3DControl x1 = new Point3DControl("X1");
        Point3DControl x2 = new Point3DControl("X2");
        Point3DControl y1 = new Point3DControl("Y1");
        Point3DControl y2 = new Point3DControl("Y2");

        Point3DControl vx = new Point3DControl("Vx'");
        Point3DControl vy = new Point3DControl("Vy'");
        Point3DControl vz = new Point3DControl("Vz'");
        Point3DControl shift = new Point3DControl("Shift");

        TransformControl transformControl = new TransformControl();

        public CalculateMatrixControl()
        {
            InitializeComponent();

            x1.Dock = DockStyle.Fill;
            x2.Dock = DockStyle.Fill;
            y1.Dock = DockStyle.Fill;
            y2.Dock = DockStyle.Fill;

            table_ImageBase.Controls.Add(x1, 1, 0);
            table_ImageBase.Controls.Add(x2, 1, 1);
            table_ImageBase.Controls.Add(y1, 1, 2);
            table_ImageBase.Controls.Add(y2, 1, 3);

            vx.Dock = DockStyle.Fill;
            vy.Dock = DockStyle.Fill;
            vz.Dock = DockStyle.Fill;
            shift.Dock = DockStyle.Fill;

            table_RobotBase.Controls.Add(vx, 1, 0);
            table_RobotBase.Controls.Add(vy, 1, 1);
            table_RobotBase.Controls.Add(vz, 1, 2);
            table_RobotBase.Controls.Add(shift, 1, 3);

            transformControl.Dock = DockStyle.Fill;
            gbx_Test.Controls.Add(transformControl);
            loadLastValue();
        }

        private void btn_CalculateMatrix_Click(object sender, EventArgs e)
        {
            Point3D intersectP = new Point3D();
            double[,] m = m_Func.CalculateTransformMatrix(
                x1.P,
                x2.P,
                y1.P,
                y2.P,
                shift.P,
                vx.V,
                vy.V,
                vz.V,
                ref intersectP
                );
            richTextBox1.Clear();
            richTextBox1.AppendText("Calculate matrix done\n");
            richTextBox1.AppendText($"Intersect : {intersectP.X:F2} {intersectP.Y:F2} {intersectP.Z:F2}\n");
            richTextBox1.AppendText(m_Func.Matrix4x4ToString(m));
            using (SaveFileDialog sf = new SaveFileDialog())
            {
                sf.Filter = "Matrix 4x4 with data|*.m44d|Matrix 4x4|*.m44";
                if (sf.ShowDialog() == DialogResult.OK)
                {
                    string filePath = sf.FileName;
                    m_Func.SaveMatrix4x4(m, filePath);
                    if (sf.FilterIndex == 1)
                    {
                        using (StreamWriter sw = new StreamWriter(filePath, true, Encoding.Default))
                        {
                            sw.WriteLine($"");

                            sw.WriteLine($"GenerateTime:{DateTime.Now:yyyy-MM-dd HH:mm:ss}");

                            sw.WriteLine($"x1:{x1.X},{x1.Y},{x1.Z}");
                            sw.WriteLine($"x2:{x2.X},{x2.Y},{x2.Z}");
                            sw.WriteLine($"y1:{y1.X},{y1.Y},{y1.Z}");
                            sw.WriteLine($"y2:{y2.X},{y2.Y},{y2.Z}");

                            sw.WriteLine($"vx:{vx.X},{vx.Y},{vx.Z}");
                            sw.WriteLine($"vy:{vy.X},{vy.Y},{vy.Z}");
                            sw.WriteLine($"vz:{vz.X},{vz.Y},{vz.Z}");
                            sw.WriteLine($"shift:{shift.X},{shift.Y},{shift.Z}");

                            sw.Flush();
                        }
                        richTextBox1.AppendText($"Save matrix {filePath}");

                    }

                    Settings s = Settings.Default;
                    s.x1x = x1.X;
                    s.x1y = x1.Y;
                    s.x1z = x1.Z;

                    s.x2x = x2.X;
                    s.x2y = x2.Y;
                    s.x2z = x2.Z;

                    s.y1x = y1.X;
                    s.y1y = y1.Y;
                    s.y1z = y1.Z;

                    s.y2x = y2.X;
                    s.y2y = y2.Y;
                    s.y2z = y2.Z;


                    s.vxx = vx.X;
                    s.vxy = vx.Y;
                    s.vxz = vx.Z;

                    s.vyx = vy.X;
                    s.vyy = vy.Y;
                    s.vyz = vy.Z;

                    s.vzx = vz.X;
                    s.vzy = vz.Y;
                    s.vzz = vz.Z;

                    s.ox = shift.X;
                    s.oy = shift.Y;
                    s.oz = shift.Z;

                    s.Save();
                }
            }
        }

        private void loadLastValue()
        {
            Settings s = Settings.Default;
            x1.SetXYZ(s.x1x, s.x1y, s.x1z);
            x2.SetXYZ(s.x2x, s.x2y, s.x2z);
            y1.SetXYZ(s.y1x, s.y1y, s.y1z);
            y2.SetXYZ(s.y2x, s.y2y, s.y2z);

            vx.SetXYZ(s.vxx, s.vxy, s.vxz);
            vy.SetXYZ(s.vyx, s.vyy, s.vyz);
            vz.SetXYZ(s.vzx, s.vzy, s.vzz);

            shift.SetXYZ(s.ox, s.oy, s.oz);
        }

        private void btn_LoadMatrix_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog op = new OpenFileDialog())
            {
                op.Filter = "Matrix 4x4 with data|*.m44d";
                if (op.ShowDialog() == DialogResult.OK)
                {
                    string filepath = op.FileName;
                    double[,] mArr = m_Func.LoadMatrix4x4ArrayFromFile(filepath);
                    richTextBox1.Clear();
                    richTextBox1.AppendText("Load matrix done.\n");
                    richTextBox1.AppendText(m_Func.Matrix4x4ToString(mArr));

                    using (StreamReader sr = new StreamReader(filepath))
                    {

                        while (!sr.EndOfStream)
                        {
                            string readData = sr.ReadLine();
                            if (readData.Contains(":"))
                            {
                                string[] splitType = readData.Split(':');
                                if (splitType.Length == 2)
                                {
                                    string typeStr = splitType[0];
                                    string[] splitData = splitType[1].Split(',');
                                    switch (typeStr)
                                    {
                                        case "x1":
                                            x1.SetXYZ(splitData[0], splitData[1], splitData[2]);

                                            break;
                                        case "x2":
                                            x2.SetXYZ(splitData[0], splitData[1], splitData[2]);

                                            break;
                                        case "y1":
                                            y1.SetXYZ(splitData[0], splitData[1], splitData[2]);

                                            break;
                                        case "y2":
                                            y2.SetXYZ(splitData[0], splitData[1], splitData[2]);

                                            break;
                                        case "vx":
                                            vx.SetXYZ(splitData[0], splitData[1], splitData[2]);

                                            break;
                                        case "vy":
                                            vy.SetXYZ(splitData[0], splitData[1], splitData[2]);

                                            break;
                                        case "vz":
                                            vz.SetXYZ(splitData[0], splitData[1], splitData[2]);

                                            break;
                                        case "shift":
                                            shift.SetXYZ(splitData[0], splitData[1], splitData[2]);

                                            break;
                                        default:

                                            break;
                                    }

                                }
                            }
                        }
                    }
                }
            }
        }

        private void btn_ClearData_Click(object sender, EventArgs e)
        {
            richTextBox1.Clear();
            richTextBox1.AppendText("Clear\n");
            x1.Clear();
            x2.Clear();
            y1.Clear();
            y2.Clear();

            vx.Clear();
            vy.Clear();
            vz.Clear();

            shift.Clear();
        }
    }
}

using System;
using System.Drawing;
using System.Windows.Forms;

namespace RsLib.PointCloud
{
    public partial class Point3DControl : UserControl
    {
        Point3D p = new Point3D();
        /// <summary>
        /// Get X value
        /// </summary>
        public double X { get => p.X; }
        /// <summary>
        /// Get Y value
        /// </summary>
        public double Y { get => p.Y; }
        /// <summary>
        /// Get Z value
        /// </summary>
        public double Z { get => p.Z; }
        /// <summary>
        /// Get instance Point3D
        /// </summary>
        public Point3D P { get => p; }
        /// <summary>
        /// Get instance Vector3D
        /// </summary>
        public Vector3D V { get => new Vector3D(p.X, p.Y, p.Z); }
        /// <summary>
        /// Get X Y Z double array
        /// </summary>
        public double[] Arr { get => new double[] { p.X, p.Y, p.Z }; }
        /// <summary>
        /// After x, y or z value changed
        /// </summary>
        public event Action<CoordinateType, double, double, double> ValueChanged;
        bool handled = false;
        public Point3DControl(string name)
        {
            InitializeComponent();

            this.Name = name;
            if (name == "") lbl_PointName.Text = "--";
            else lbl_PointName.Text = name;
        }

        public void SetXYZ(double x, double y, double z, int digit = 2)
        {
            p = new Point3D(x, y, z);
            updateBox(digit);
        }
        public void SetXYZ(string xStr, string yStr, string zStr, int digit = 2)
        {
            double x, y, z;
            double.TryParse(xStr, out x);
            double.TryParse(yStr, out y);
            double.TryParse(zStr, out z);

            p = new Point3D(x, y, z);
            updateBox(digit);
        }
        private void updateBox(int digit = 1)
        {
            tbx_X.Text = Math.Round(p.X, digit).ToString();
            tbx_Y.Text = Math.Round(p.Y, digit).ToString();
            tbx_Z.Text = Math.Round(p.Z, digit).ToString();

        }
        private void tbx_X_KeyUp(object sender, KeyEventArgs e)
        {
            TextBox box = sender as TextBox;
            double tmpValue = 0.0;
            bool parseOK = double.TryParse(box.Text, out tmpValue);
            if (parseOK)
            {
                if (e.KeyCode == Keys.Enter)
                {
                    p.X = tmpValue;
                    box.ForeColor = Color.Black;
                    if (ValueChanged != null) ValueChanged(CoordinateType.X, X, Y, Z);
                }
                else
                {
                    if (!handled)
                    {
                        if (p.X == tmpValue) box.ForeColor = Color.Black;
                        else box.ForeColor = Color.Red;
                    }
                }
            }
            else
            {
                box.ForeColor = Color.Red;
            }
        }

        private void tbx_Y_KeyUp(object sender, KeyEventArgs e)
        {
            TextBox box = sender as TextBox;
            double tmpValue = 0.0;
            bool parseOK = double.TryParse(box.Text, out tmpValue);
            if (parseOK)
            {
                if (e.KeyCode == Keys.Enter)
                {
                    p.Y = tmpValue;
                    box.ForeColor = Color.Black;
                    if (ValueChanged != null) ValueChanged(CoordinateType.Y, X, Y, Z);

                }
                else
                {
                    if (!handled)
                    {
                        if (p.Y == tmpValue) box.ForeColor = Color.Black;
                        else box.ForeColor = Color.Red;
                    }
                }
            }
            else
            {
                box.ForeColor = Color.Red;
            }
        }

        private void tbx_Z_KeyUp(object sender, KeyEventArgs e)
        {
            TextBox box = sender as TextBox;
            double tmpValue = 0.0;
            bool parseOK = double.TryParse(box.Text, out tmpValue);
            if (parseOK)
            {
                if (e.KeyCode == Keys.Enter)
                {
                    p.Z = tmpValue;
                    box.ForeColor = Color.Black;
                    if (ValueChanged != null) ValueChanged(CoordinateType.Z, X, Y, Z);

                }
                else
                {
                    if (!handled)
                    {
                        if (p.Z == tmpValue) box.ForeColor = Color.Black;
                        else box.ForeColor = Color.Red;
                    }
                }
            }
            else
            {
                box.ForeColor = Color.Red;
            }

        }
        private void tbx_X_KeyPress(object sender, KeyPressEventArgs e)
        {
            handled = RsLib.Common.FT_Functions.double_Positive_Negative_KeyPress(e.KeyChar);
            e.Handled = handled;
        }
        private void tbx_Y_KeyPress(object sender, KeyPressEventArgs e)
        {
            handled = RsLib.Common.FT_Functions.double_Positive_Negative_KeyPress(e.KeyChar);
            e.Handled = handled;
        }

        private void tbx_Z_KeyPress(object sender, KeyPressEventArgs e)
        {
            handled = RsLib.Common.FT_Functions.double_Positive_Negative_KeyPress(e.KeyChar);
            e.Handled = handled;
        }
        public void Clear()
        {
            p = new Point3D();
            updateBox();
        }
    }
}

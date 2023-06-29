using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
namespace RsLib.Common
{
    public partial class ZoomImageControl : UserControl
    {
        public float Zoom { get; set; } = 1.0f;
        public Point Pan;
        Point _panStart;

        public int UpdateInterval
        {
            get => timer1.Interval;
            set
            {
                if (value < 10) timer1.Interval = 10;
                else if (value > 150) timer1.Interval = 150;
                else timer1.Interval = value;
            }
        }

        bool _isUpdateNow = false;
        public Color BackgroundColor = Color.Silver;
        Image _img;
        public ZoomImageControl()
        {
            InitializeComponent();
            pictureBox1.MouseWheel += PictureBox1_MouseWheel;
            pictureBox1.MouseDown += PictureBox1_MouseDown;
            pictureBox1.MouseUp += PictureBox1_MouseUp;
            pictureBox1.MouseMove += PictureBox1_MouseMove;
            pictureBox1.MouseDoubleClick += PictureBox1_MouseDoubleClick;
            pictureBox1.Paint += PictureBox1_Paint;
            pictureBox1.SizeChanged += PictureBox1_SizeChanged;
        }

        private void PictureBox1_SizeChanged(object sender, EventArgs e)
        {
            ResetView();
        }

        private void PictureBox1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            base.OnMouseDoubleClick(e);
            if (e.Button == MouseButtons.Middle) ResetView();
        }

        private void PictureBox1_Paint(object sender, PaintEventArgs e)
        {
            try
            {
                _isUpdateNow = true;
                e.Graphics.Clear(BackgroundColor);

                // Translate the coordinate system to implement panning
                e.Graphics.TranslateTransform(Pan.X, Pan.Y);
                // Scale the coordinate system to implement zooming
                e.Graphics.ScaleTransform(Zoom, Zoom);

                if (pictureBox1.Image != null)
                {
                    // Draw the image at the origin of the coordinate system
                    e.Graphics.DrawImage(pictureBox1.Image, new Point(0, 0));

                }
                _isUpdateNow = false;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void ResetView()
        {

            if (pictureBox1.Image != null)
            {
                double w = (double)Width / (double)pictureBox1.Image.Width;
                double h = (double)Height / (double)pictureBox1.Image.Height;
                Zoom = w < h ? (float)w : (float)h;

                double zoomW = Zoom * pictureBox1.Image.Width;
                double zoomH = Zoom * pictureBox1.Image.Height;

                Pan.X = (int)(Width - zoomW) / 2;
                Pan.Y = (int)(Height - zoomH) / 2;
            }
            else
            {
                Zoom = 1.0f;
                Pan = new Point();
            }
        }

        public void SetImage(string filePath)
        {
            if (filePath != "")
            {
                if (File.Exists(filePath))
                {
                    Image tmpImg = Image.FromFile(filePath);
                    pictureBox1.Image = tmpImg;
                    _img = tmpImg;
                }
                else
                {
                    pictureBox1.Image = null;
                    _img = null;
                    throw new Exception($"File not exist! File Path : {filePath}.");
                }
            }
            else
            {
                pictureBox1.Image = null;
                _img = null;
            }
            ResetView();
        }
        public void SetImage(Bitmap bitmap)
        {
            if (bitmap != null)
            {
                Image tmpImg = new Bitmap(bitmap);
                pictureBox1.Image = tmpImg;
                _img = tmpImg;
            }
            else
            {
                pictureBox1.Image = null;
                _img = null;

            }
            ResetView();
        }
        public void SetImage(IntPtr bitmapPtr)
        {
            if (bitmapPtr != IntPtr.Zero)
            {
                Image tmpImg = Image.FromHbitmap(bitmapPtr);
                pictureBox1.Image = tmpImg;
                _img = tmpImg;

            }
            else
            {
                pictureBox1.Image = null;
                _img = null;

            }
            ResetView();
        }
        private void PictureBox1_MouseMove(object sender, MouseEventArgs e)
        {
            base.OnMouseMove(e);
            if (e.Button == MouseButtons.Left)
            {
                int deltaX = e.Location.X - _panStart.X;
                int deltaY = e.Location.Y - _panStart.Y;
                Pan.X += deltaX;
                Pan.Y += deltaY;
                _panStart = e.Location;
            }
        }

        private void PictureBox1_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                _panStart = e.Location;
                Cursor = Cursors.Hand;
            }
        }
        private void PictureBox1_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                Cursor = Cursors.Default;
            }
        }
        private Point GetImagePointFromMousePosition(Point mousePosition)
        {
            // Convert the mouse position to the coordinates of the image
            Point imagePoint = new Point(
                (int)((mousePosition.X - Pan.X) / Zoom),
                (int)((mousePosition.Y - Pan.Y) / Zoom));
            int w = Width;
            int h = Height;

            if (pictureBox1.Image != null)
            {
                w = pictureBox1.Image.Width;
                h = pictureBox1.Image.Height;
            }
            // Clamp the image point to the bounds of the image
            imagePoint.X = Math.Max(0, Math.Min(imagePoint.X, w - 1));
            imagePoint.Y = Math.Max(0, Math.Min(imagePoint.Y, h - 1));

            return imagePoint;
        }
        private void PictureBox1_MouseWheel(object sender, MouseEventArgs e)
        {
            // Determine the zoom factor based on the mouse wheel delta
            float zoomChange = e.Delta > 0 ? 1.1f : 0.9f;
            float newZoom = Zoom * zoomChange;
            if (newZoom < 0.1f)
            {
                newZoom = 0.1f;
            }
            if (newZoom > 10.0f)
            {
                newZoom = 10.0f;
            }

            // Determine the image point that corresponds to the mouse position
            Point imagePoint = GetImagePointFromMousePosition(e.Location);

            // Adjust the pan offset to maintain the image point at the same position on the screen
            Pan.X += (int)(imagePoint.X * Zoom - imagePoint.X * newZoom);
            Pan.Y += (int)(imagePoint.Y * Zoom - imagePoint.Y * newZoom);
            // Update the zoom and repaint the picture box
            Zoom = newZoom;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (_isUpdateNow == false)
                pictureBox1.Invalidate();
        }
    }
}

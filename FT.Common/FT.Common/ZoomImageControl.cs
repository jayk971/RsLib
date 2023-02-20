using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using System.Drawing.Drawing2D;
namespace RsLib.Common
{
    public partial class ZoomImageControl : UserControl
    {
        private float _zoom = 1.0f;
        private Point _panStart;
        private Point _panOffset;

        public Point Pan
        {
            get => _panOffset;
            set
            {
                _panOffset = value;
            }
        }
        public float Zoom
        {
            get => _zoom;
            set
            {
                _zoom = value;
            }
        }



        public ZoomImageControl()
        {
            InitializeComponent();
            pictureBox1.MouseWheel += PictureBox1_MouseWheel;
            pictureBox1.MouseDown += PictureBox1_MouseDown;
            pictureBox1.MouseUp += PictureBox1_MouseUp;
            pictureBox1.MouseMove += PictureBox1_MouseMove;
            pictureBox1.MouseDoubleClick += PictureBox1_MouseDoubleClick;
            pictureBox1.Paint += PictureBox1_Paint;
            
        }

        private void PictureBox1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            base.OnMouseDoubleClick(e);

        }

        private void PictureBox1_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.Clear(Color.Silver);

            // Translate the coordinate system to implement panning
            e.Graphics.TranslateTransform(_panOffset.X, _panOffset.Y);
            // Scale the coordinate system to implement zooming
            e.Graphics.ScaleTransform(_zoom, _zoom);

            // Draw the image at the origin of the coordinate system
            e.Graphics.DrawImage(pictureBox1.Image, new Point(0, 0));

        }
        public void ResetView()
        {
            double w = (double)Width / (double)pictureBox1.Image.Width;
            double h = (double)Height / (double)pictureBox1.Image.Height;
            _zoom = w < h ? (float)w : (float)h;

            double zoomW = _zoom * pictureBox1.Image.Width;
            double zoomH = _zoom * pictureBox1.Image.Height;

            _panOffset.X = (int)(Width - zoomW) / 2;
            _panOffset.Y = (int)(Height - zoomH) / 2;
        }
        public void SetImage(string filePath)
        {
            pictureBox1.Image = Image.FromFile(filePath);
            ResetView();
        }
        private void PictureBox1_MouseMove(object sender, MouseEventArgs e)
        {
            base.OnMouseMove(e);
            if (e.Button == MouseButtons.Left)
            {
                int deltaX = e.Location.X - _panStart.X;
                int deltaY = e.Location.Y - _panStart.Y;
                _panOffset.X += deltaX;
                _panOffset.Y += deltaY;
                _panStart = e.Location;
            }
        }

        private void PictureBox1_MouseDown(object sender, MouseEventArgs e)
        {
            if(e.Button == MouseButtons.Left)
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
                (int)((mousePosition.X - _panOffset.X) / _zoom),
                (int)((mousePosition.Y - _panOffset.Y) / _zoom));

            // Clamp the image point to the bounds of the image
            imagePoint.X = Math.Max(0, Math.Min(imagePoint.X, pictureBox1.Image.Width - 1));
            imagePoint.Y = Math.Max(0, Math.Min(imagePoint.Y, pictureBox1.Image.Height - 1));
            
            return imagePoint;
        }
        private void PictureBox1_MouseWheel(object sender, MouseEventArgs e)
        {

            // Determine the zoom factor based on the mouse wheel delta
            float zoomChange = e.Delta > 0 ? 0.1f : -0.1f;
            float newZoom = _zoom + zoomChange;
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
            _panOffset.X += (int)(imagePoint.X * _zoom - imagePoint.X * newZoom);
            _panOffset.Y += (int)(imagePoint.Y * _zoom - imagePoint.Y * newZoom);
            // Update the zoom and repaint the picture box
            _zoom = newZoom;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            pictureBox1.Invalidate();
        }
    }
}

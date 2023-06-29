
using System.Windows;
using System.Windows.Media;
using DrawingColor = System.Drawing.Color;
using MediaColor = System.Windows.Media.Color;
namespace RsLib.Display
{
    class ComFunc
    {
        public static void TextAlignmentMatrix(VerticalAlignment va, HorizontalAlignment ha, Size TextBoxSize, out double x, out double y)
        {
            x = 0.0;
            y = 0.0;
            if (va == VerticalAlignment.Top && ha == HorizontalAlignment.Left)
            {
                x = 0.0;
                y = 0.0;
            }
            else if (va == VerticalAlignment.Top && ha == HorizontalAlignment.Center)
            {
                x = -TextBoxSize.Width / 2;
                y = 0.0;
            }
            else if (va == VerticalAlignment.Top && ha == HorizontalAlignment.Right)
            {
                x = -TextBoxSize.Width;
                y = 0.0;
            }
            else if (va == VerticalAlignment.Center && ha == HorizontalAlignment.Left)
            {
                x = 0.0;
                y = -TextBoxSize.Height / 2;
            }
            else if (va == VerticalAlignment.Center && ha == HorizontalAlignment.Center)
            {
                x = -TextBoxSize.Width / 2;
                y = -TextBoxSize.Height / 2;
            }
            else if (va == VerticalAlignment.Center && ha == HorizontalAlignment.Right)
            {
                x = -TextBoxSize.Width;
                y = -TextBoxSize.Height / 2;
            }
            else if (va == VerticalAlignment.Bottom && ha == HorizontalAlignment.Left)
            {
                x = 0.0;
                y = -TextBoxSize.Height;
            }
            else if (va == VerticalAlignment.Bottom && ha == HorizontalAlignment.Center)
            {
                x = -TextBoxSize.Width / 2;
                y = -TextBoxSize.Height;
            }
            else if (va == VerticalAlignment.Bottom && ha == HorizontalAlignment.Right)
            {
                x = -TextBoxSize.Width;
                y = -TextBoxSize.Height;
            }
            else
            {
                x = 0.0;
                y = 0.0;
            }

        }
        public static SolidColorBrush ToSolidBrush(DrawingColor color)
        {
            return new SolidColorBrush(ToMediaColor(color));
        }
        public static MediaColor ToMediaColor(DrawingColor color)
        {
            return MediaColor.FromArgb(color.A, color.R, color.G, color.B);
        }
        public static DrawingColor ToDrawingColor(MediaColor color)
        {
            return DrawingColor.FromArgb(color.A, color.R, color.G, color.B);
        }
    }
}

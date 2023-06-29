using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Shapes;
using DrawingColor = System.Drawing.Color;
using WinPoint = System.Windows.Point;

namespace RsLib.Display
{
    public enum FtObjectDOF : int
    {
        None = 0,
        Shift,
        //Rotate,
        //Scale,
        //Skew,
    }
    public interface IFtObject
    {
        UIElement element { get; }
        bool IsSelected { get; set; }
        string Name { get; set; }
        FtObjectDOF DOF { get; set; }
        bool Interactive { get; set; }
    }
    public class DisplayParamter
    {
        #region Line Width
        public delegate void delegateLineWidthChange(double lineWidth);
        public event delegateLineWidthChange LineWidthChanged;
        private double lineWidth = 2.0;
        public double LineWidth
        {
            get { return lineWidth; }
            set
            {
                lineWidth = value;
                LineWidthChanged(lineWidth);
            }
        }
        #endregion
        #region Point Size
        public delegate void delegatePoinrSizeChange(double pointSize);
        public event delegatePoinrSizeChange PointSizeChanged;

        private double pointSize = 1.0;
        public double PointSize
        {
            get { return pointSize; }
            set
            {
                pointSize = value;
                PointSizeChanged(pointSize);
            }
        }
        #endregion
        #region Fill Color
        public delegate void delegateFillBrushChange(SolidColorBrush solidColorBrush);
        public event delegateFillBrushChange FillBrushChanged;
        private SolidColorBrush fillBrush = Brushes.Cyan;
        public SolidColorBrush FillBrush
        {
            get { return fillBrush; }
        }
        public DrawingColor FillColor
        {
            get
            {
                return DrawingColor.FromArgb(fillBrush.Color.A, fillBrush.Color.R, fillBrush.Color.G, fillBrush.Color.B);
            }
            set
            {
                fillBrush = ComFunc.ToSolidBrush(value);
                FillBrushChanged(fillBrush);
            }
        }
        #endregion
        #region Stroke Color
        public delegate void delegateStrokeBrushChange(SolidColorBrush solidColorBrush);
        public event delegateStrokeBrushChange StrokeBrushChanged;
        private SolidColorBrush strokeBrush = Brushes.Cyan;
        public SolidColorBrush StrokeBrush
        {
            get { return strokeBrush; }
        }
        public DrawingColor StrokeColor
        {
            get
            {
                return DrawingColor.FromArgb(strokeBrush.Color.A, strokeBrush.Color.R, strokeBrush.Color.G, strokeBrush.Color.B);
            }
            set
            {
                strokeBrush = ComFunc.ToSolidBrush(value);
                StrokeBrushChanged(strokeBrush);
            }
        }
        #endregion
        public PenLineCap EndLineCap = PenLineCap.Round;

        public DisplayParamter()
        {
            FillBrushChanged += DisplayParamter_FillBrushChanged;
            StrokeBrushChanged += DisplayParamter_StrokeBrushChanged;
            LineWidthChanged += DisplayParamter_LineWidthChanged;
            PointSizeChanged += DisplayParamter_PointSizeChanged;
        }

        private void DisplayParamter_PointSizeChanged(double pointSize)
        {
        }
        private void DisplayParamter_LineWidthChanged(double lineWidth)
        {
        }
        private void DisplayParamter_StrokeBrushChanged(SolidColorBrush solidColorBrush)
        {
        }
        private void DisplayParamter_FillBrushChanged(SolidColorBrush solidColorBrush)
        {
        }
    }

    public class FtObject : IFtObject
    {
        public delegate void delegateNameChanged(string name);
        public event delegateNameChanged NameChanged;
        private string name = "";
        public string Name
        {
            get { return name; }
            set
            {
                name = value;
                NameChanged(name);
            }
        }



        private Path geometry = new Path();
        public UIElement element
        {
            get { return (Path)geometry; }

            //set{ geometry = value as Path; } 
        }
        public DisplayParamter DisplayPara = new DisplayParamter();
        public DisplayParamter SelectedDisplayPara = new DisplayParamter();

        public delegate void delegateIsSelectedChanged(bool IsSelected);
        public event delegateIsSelectedChanged IsSelectedChanged;
        private bool isSelected = false;
        public bool IsSelected
        {
            get { return isSelected; }
            set
            {
                isSelected = value;
                IsSelectedChanged(isSelected);
            }
        }
        public delegate void delegateIsStaticChanged(bool IsStatic);
        public event delegateIsStaticChanged IsStaticChanged;
        private bool interactive = false;
        public bool Interactive
        {
            get { return interactive; }
            set
            {
                interactive = value;
                IsStaticChanged(interactive);
            }
        }

        public delegate void delegateToolTipTextChanged(string Text);
        public event delegateToolTipTextChanged ToolTipTextChanged;
        private string toolTipText = "";
        public string ToolTipText
        {
            get { return toolTipText; }
            set
            {
                toolTipText = value;
                ToolTipTextChanged(toolTipText);
            }
        }
        public FtObjectDOF DOF { get; set; } = FtObjectDOF.None;
        public Matrix M_Offset = new Matrix();


        public FtObject()
        {
            DisplayPara.FillBrushChanged += DisplayPara_FillBrushChanged;
            DisplayPara.StrokeBrushChanged += DisplayPara_StrokeBrushChanged;
            DisplayPara.LineWidthChanged += DisplayPara_LineWidthChanged;
            DisplayPara.PointSizeChanged += DisplayPara_PointSizeChanged;
            IsSelectedChanged += FtObject_IsSelectedChanged;
            IsStaticChanged += FtObject_IsStaticChanged;
            ToolTipTextChanged += FtObject_ToolTipTextChanged;
            NameChanged += FtObject_NameChanged;
            if (!interactive) geometry.Cursor = Cursors.Arrow;
            else geometry.Cursor = Cursors.Hand;

            DisplayPara.StrokeColor = DrawingColor.White;
            DisplayPara.LineWidth = 3;
            SelectedDisplayPara.StrokeColor = DrawingColor.Red;
            SelectedDisplayPara.LineWidth = 5;
        }
        private void FtObject_NameChanged(string name)
        {
            geometry.Uid = name;
        }
        private void FtObject_ToolTipTextChanged(string Text)
        {
            geometry.ToolTip = Text;
        }

        private void FtObject_IsStaticChanged(bool Interactive)
        {
            if (!Interactive) geometry.Cursor = Cursors.Arrow;
            else geometry.Cursor = Cursors.Hand;
        }

        private void FtObject_IsSelectedChanged(bool IsSelected)
        {
            if (IsSelected)
            {
                geometry.Stroke = SelectedDisplayPara.StrokeBrush;
                geometry.StrokeThickness = SelectedDisplayPara.LineWidth;
                geometry.Fill = SelectedDisplayPara.FillBrush;
            }
            else
            {
                geometry.Stroke = DisplayPara.StrokeBrush;
                geometry.StrokeThickness = DisplayPara.LineWidth;
                geometry.Fill = DisplayPara.FillBrush;

            }
        }

        private void DisplayPara_PointSizeChanged(double pointSize)
        {
            //geometry.StrokeThickness = pointSize;
        }

        private void DisplayPara_LineWidthChanged(double lineWidth)
        {
            geometry.StrokeThickness = lineWidth;
        }

        private void DisplayPara_StrokeBrushChanged(SolidColorBrush solidColorBrush)
        {
            geometry.Stroke = solidColorBrush;
        }

        private void DisplayPara_FillBrushChanged(SolidColorBrush solidColorBrush)
        {
            geometry.Fill = solidColorBrush;
        }

        public void GetGeometryBound(out double MinX, out double MinY, out double MaxX, out double MaxY)
        {
            MinX = 0.0;
            MinY = 0.0;
            MaxX = 0.0;
            MaxY = 0.0;
            Rect rect = geometry.Data.Bounds;
            WinPoint NewTopLeft = M_Offset.Transform(rect.TopLeft);
            WinPoint NewBottomRight = M_Offset.Transform(rect.BottomRight);
            MinX = Math.Min(NewTopLeft.X, NewBottomRight.X);
            MaxX = Math.Max(NewTopLeft.X, NewBottomRight.X);
            MinY = Math.Min(NewTopLeft.Y, NewBottomRight.Y);
            MaxY = Math.Max(NewTopLeft.Y, NewBottomRight.Y);

        }
        public Size GetGeometrySize()
        {
            Size size = new Size(double.PositiveInfinity, double.PositiveInfinity);
            geometry.Measure(size);
            return geometry.DesiredSize;
        }
        public void GetGeometrySize(out double Width, out double Height)
        {
            Width = 0.0;
            Height = 0.0;
            Size size = new Size(double.PositiveInfinity, double.PositiveInfinity);
            geometry.Measure(size);
            Width = geometry.DesiredSize.Width;
            Height = geometry.DesiredSize.Height;
        }
    }
    public class FtPoint : FtObject
    {
        private EllipseGeometry EG = new EllipseGeometry();
        public double X
        {
            get
            {
                return EG.Center.X;
            }
        }
        public double Y
        {
            get
            {
                return EG.Center.Y;
            }
        }
        public FtPoint()
        {
            EG.Center = new WinPoint(0, 0);
            Init();
        }
        public FtPoint(double x, double y)
        {
            EG.Center = new WinPoint(x, y);
            Init();
        }
        private void Init()
        {
            base.DisplayPara.PointSizeChanged += DisplayPara_PointSizeChanged;
            base.DisplayPara.StrokeBrushChanged += DisplayPara_StrokeBrushChanged;
            ((Path)element).Data = EG;
            ((Path)element).Fill = DisplayPara.StrokeBrush;
            ((Path)element).Stroke = DisplayPara.StrokeBrush;
        }

        private void DisplayPara_StrokeBrushChanged(SolidColorBrush solidColorBrush)
        {
            ((Path)element).Fill = DisplayPara.StrokeBrush;
        }

        private void DisplayPara_PointSizeChanged(double pointSize)
        {
            EG.RadiusX = pointSize;
            EG.RadiusY = pointSize;
            ((Path)element).Data = EG;
            ((Path)element).Fill = DisplayPara.StrokeBrush;
            ((Path)element).Stroke = DisplayPara.StrokeBrush;
        }
    }
    public class FtLine : FtObject
    {
        private LineGeometry LG = new LineGeometry();

        public FtLine()
        {
            Init();
        }
        public FtLine(WinPoint StartPoint, WinPoint EndPoint)
        {
            LG.StartPoint = StartPoint;
            LG.EndPoint = EndPoint;
            Init();
        }
        public void SetStartEnd(double StartX, double StartY, double EndX, double EndY)
        {
            LG.StartPoint = new WinPoint(StartX, StartY);
            LG.EndPoint = new WinPoint(EndX, EndY);
        }
        private void Init()
        {
            ((Path)element).Data = LG;
            ((Path)element).Fill = DisplayPara.FillBrush;
            ((Path)element).Stroke = DisplayPara.StrokeBrush;
            ((Path)element).StrokeThickness = DisplayPara.LineWidth;
            ((Path)element).StrokeEndLineCap = DisplayPara.EndLineCap;
        }
    }
    public class FtPolygon : FtObject
    {
        private PathGeometry PG = new PathGeometry();
        private bool Is1stPoint = true;
        public FtPolygon()
        {
            Init();
        }
        public void Add(WinPoint point)
        {
            PathFigure PF = PG.Figures[0];
            PolyLineSegment PLS = PF.Segments[0] as PolyLineSegment;
            if (Is1stPoint)
            {
                Is1stPoint = false;
                PF.StartPoint = point;
            }
            else
                PLS.Points.Add(point);

        }
        public void Add(double x, double y)
        {
            WinPoint point = new WinPoint(x, y);
            PathFigure PF = PG.Figures[0];
            PolyLineSegment PLS = PF.Segments[0] as PolyLineSegment;
            if (Is1stPoint)
            {
                Is1stPoint = false;
                PF.StartPoint = point;
            }
            else
            {
                if (IsSameStartEnd(PF, point))
                {

                }
                else
                {
                    PLS.Points.Add(point);
                }
            }
        }
        private bool IsSameStartEnd(PathFigure PF, WinPoint CheckPoint)
        {
            PolyLineSegment PLS = PF.Segments[0] as PolyLineSegment;
            if (Is1stPoint) return false;
            else
            {
                if (PLS.Points.Count == 0) return false;
                else
                {
                    Vector v = PF.StartPoint - CheckPoint;
                    if (Math.Abs(v.X) < 0.01 && Math.Abs(v.Y) < 0.01) return true;
                    else return false;
                }
            }
        }
        private void Init()
        {
            PolyLineSegment PLS = new PolyLineSegment();
            PathFigure PF = new PathFigure();
            PF.Segments.Add(PLS);
            PG.Figures.Add(PF);

            PF.IsClosed = true;
            PF.IsFilled = false;

            ((Path)element).Data = PG;
            ((Path)element).Stroke = DisplayPara.StrokeBrush;
            ((Path)element).StrokeThickness = DisplayPara.LineWidth;
        }
    }
    public class FtPolyline : FtObject
    {
        private PathGeometry PG = new PathGeometry();
        private bool Is1stPoint = true;
        public FtPolyline()
        {
            Init();
        }
        public void Add(WinPoint point)
        {
            PathFigure PF = PG.Figures[0];
            PolyLineSegment PLS = PF.Segments[0] as PolyLineSegment;
            if (Is1stPoint)
            {
                Is1stPoint = false;
                PF.StartPoint = point;
            }
            else
                PLS.Points.Add(point);

        }
        public void Add(double x, double y)
        {
            PathFigure PF = PG.Figures[0];
            PolyLineSegment PLS = PF.Segments[0] as PolyLineSegment;
            if (Is1stPoint)
            {
                Is1stPoint = false;
                PF.StartPoint = new WinPoint(x, y);
            }
            else
                PLS.Points.Add(new WinPoint(x, y));

        }
        private void Init()
        {
            PolyLineSegment PLS = new PolyLineSegment();
            PathFigure PF = new PathFigure();
            PF.Segments.Add(PLS);
            PG.Figures.Add(PF);

            PF.IsClosed = false;
            PF.IsFilled = false;

            ((Path)element).Data = PG;
            ((Path)element).Stroke = DisplayPara.StrokeBrush;
            ((Path)element).StrokeThickness = DisplayPara.LineWidth;
        }
    }

    public class FtCircle : FtObject
    {
        private EllipseGeometry EG = new EllipseGeometry();

        private bool isFill = false;
        public bool IsFill
        {
            get
            {
                return isFill;
            }
            set
            {
                isFill = value;
                Fill();
            }
        }
        public FtCircle()
        {
            EG.Center = new WinPoint(0, 0);
            EG.RadiusX = 1;
            EG.RadiusY = 1;
            Init();
        }
        public FtCircle(double x, double y, double R)
        {
            EG.Center = new WinPoint(x, y);
            EG.RadiusX = R;
            EG.RadiusY = R;
            Init();
        }
        public void SetCenterRadius(double x, double y, double R)
        {
            EG.Center = new WinPoint(x, y);
            EG.RadiusX = R;
            EG.RadiusY = R;
        }
        private void Init()
        {
            ((Path)element).Data = EG;
            ((Path)element).Stroke = DisplayPara.StrokeBrush;
            ((Path)element).StrokeThickness = DisplayPara.LineWidth;
        }
        private void Fill()
        {
            if (isFill) ((Path)element).Fill = DisplayPara.StrokeBrush;
            else ((Path)element).Fill = null;

        }
    }
    public class FtRectangleAffine : FtObject
    {
        private PathGeometry PG = new PathGeometry();
        public FtRectangleAffine()
        {
            Init();
        }
        public void SetOriginCornerXCornerY(double Ox, double Oy, double Xx, double Xy, double Yx, double Yy)
        {
            PathFigure PF = PG.Figures[0];
            PolyLineSegment PLS = PF.Segments[0] as PolyLineSegment;
            PF.StartPoint = new WinPoint(Ox, Oy);
            PLS.Points.Add(new WinPoint(Xx, Xy));
            PLS.Points.Add(new WinPoint(Xx + Yx - Ox, Xy + Yy - Oy));
            PLS.Points.Add(new WinPoint(Yx, Yy));
        }
        private void Init()
        {
            PolyLineSegment PLS = new PolyLineSegment();
            PathFigure PF = new PathFigure();
            PF.Segments.Add(PLS);
            PG.Figures.Add(PF);

            PF.IsClosed = true;
            PF.IsFilled = false;

            ((Path)element).Data = PG;
            ((Path)element).Stroke = DisplayPara.StrokeBrush;
            ((Path)element).StrokeThickness = DisplayPara.LineWidth;
        }
    }

    public class FtRectangle : FtObject
    {
        private RectangleGeometry RG = new RectangleGeometry();
        public FtRectangle()
        {
            Init();
        }
        public FtRectangle(WinPoint StartP, WinPoint EndP)
        {
            Rect rect = new Rect(StartP, EndP);
            RG = new RectangleGeometry(rect);
            Init();
        }
        private void Init()
        {
            ((Path)element).Data = RG;
            ((Path)element).Stroke = DisplayPara.StrokeBrush;
            ((Path)element).StrokeThickness = DisplayPara.LineWidth;
        }
        public void SetXYWidthHeight(double x, double y, double Width, double Height)
        {
            Rect rect = new Rect(x, y, Width, Height);
            RG.Rect = rect;
        }
        public void SetCenterWidthHeight(double CenterX, double CenterY, double Width, double Height)
        {
            Rect rect = new Rect(CenterX - Width / 2, CenterY - Height / 2, Width, Height);
            RG.Rect = rect;
        }
    }
    public class FtCompositeShape : FtObject
    {
        private GeometryGroup GG = new GeometryGroup();
        public FtCompositeShape()
        {
            Init();
        }
        public void Add(IFtObject obj)
        {
            Path path = (Path)obj.element;
            GG.Children.Add(path.Data);
        }
        public void Clear()
        {
            GG.Children.Clear();
        }

        private void Init()
        {
            ((Path)element).Data = GG;
            ((Path)element).Stroke = DisplayPara.StrokeBrush;
            ((Path)element).StrokeThickness = DisplayPara.LineWidth;
        }

    }

    public class FtText : IFtObject
    {
        public FtObjectDOF DOF { get; set; } = FtObjectDOF.None;
        public bool Interactive { get; set; } = true;
        private bool isSelected = false;
        public bool IsSelected
        {
            get { return isSelected; }
            set
            {
                isSelected = false;
            }
        }
        private int zIndex = 0;
        public int ZIndex
        {
            get
            {
                return zIndex;
            }
            set
            {
                zIndex = value;
            }
        }
        public delegate void delegateNameChanged(string name);
        public event delegateNameChanged NameChanged;
        private string name = "";
        public string Name
        {
            get { return name; }
            set
            {
                name = value;
                NameChanged(name);
            }
        }
        private TextBlock tb = new TextBlock();
        public UIElement element
        {
            get
            {
                return tb;
            }
            set
            {
                tb = value as TextBlock;
            }
        }
        public double FontSize = 12;
        public DrawingColor FontColor = DrawingColor.White;
        public VerticalAlignment verticalAlignment = VerticalAlignment.Top;
        public HorizontalAlignment horizontalAlignment = HorizontalAlignment.Left;
        public WinPoint Pos = new WinPoint(0, 0);
        public FtText()
        {
            TextBlock.SetFontFamily(tb, new FontFamily("Arial"));
            NameChanged += FtText_NameChanged;
        }

        private void FtText_NameChanged(string name)
        {
            element.Uid = name;
        }

        public void SetAlignment(VerticalAlignment VA, HorizontalAlignment HA)
        {
            verticalAlignment = VA;
            horizontalAlignment = HA;
        }
        public void SetFontSizeColor(double fontSize, DrawingColor color)
        {
            TextBlock.SetForeground(tb, ComFunc.ToSolidBrush(color));
            TextBlock.SetFontSize(tb, fontSize);
        }
        public void SetXYText(double x, double y, string text)
        {
            tb.Text = text;
            Pos = new WinPoint(x, y);
        }

    }
    public class FTGraphics2D : IEnumerable
    {
        private List<string> GroupName = new List<string>();
        private Dictionary<string, IFtObject> Group = new Dictionary<string, IFtObject>();
        public delegate void delegateRemoveItem(string ItemName);
        public event delegateRemoveItem AfterRemoveItem;

        public int Count
        {
            get { return Group.Count; }
        }

        public ICollection<string> Keys
        {
            get { return Group.Keys; }
        }


        public ICollection<IFtObject> Values
        {
            get
            {
                return Group.Values;
            }
        }

        public IFtObject this[string key]
        {
            get => Group[key];
            set => Group[key] = value;
        }
        public FTGraphics2D()
        {
            AfterRemoveItem += FTGraphics2D_AfterRemoveItem;
        }

        private void FTGraphics2D_AfterRemoveItem(string ItemName)
        {
            //throw new NotImplementedException();
        }

        public void Clear()
        {
            foreach (string s in GroupName)
            {
                AfterRemoveItem(s);
            }
            GroupName.Clear();
            Group.Clear();
        }
        public void SelectedShape(string Uid)
        {
            if (ContainsKey(Uid))
            {
                Group[Uid].IsSelected = true;
            }
        }
        public void UnSelectAll()
        {
            foreach (KeyValuePair<string, IFtObject> kvp in Group)
            {
                kvp.Value.IsSelected = false;
            }
        }

        public bool ContainsKey(string Name)
        {
            return Group.ContainsKey(Name);
        }
        public bool FindItem(string Name)
        {
            return ContainsKey(Name);
        }

        public bool TryGetValue(string key, out IFtObject value)
        {
            value = null;
            return Group.TryGetValue(key, out value);
        }

        public void Add(string Name, IFtObject Shape)
        {
            if (!ContainsKey(Name))
            {
                GroupName.Add(Name);
                Group.Add(Name, Shape);
            }
        }
        public bool Contains(KeyValuePair<string, IFtObject> item)
        {
            return Group.Contains(item);
        }
        public bool Remove(string key)
        {
            if (Group.ContainsKey(key))
            {
                //先移除畫面上的物件
                AfterRemoveItem(key);

                //再移除資料
                GroupName.Remove(key);
                Group.Remove(key);
                return true;
            }
            else return false;
        }

        public bool Remove(KeyValuePair<string, IFtObject> item)
        {
            if (Group.ContainsKey(item.Key))
            {
                GroupName.Remove(item.Key);
                Group.Remove(item.Key);
                AfterRemoveItem(item.Key);

                return true;
            }
            else return false;
        }

        public IEnumerator GetEnumerator()
        {
            foreach (KeyValuePair<string, IFtObject> kvp in Group)
            {
                yield return kvp;
            }
        }
        public bool ContainString(string NeedFindStr, out List<string> FoundStr)
        {
            FoundStr = new List<string>();
            foreach (string s in GroupName)
            {
                if (s.Contains(NeedFindStr)) FoundStr.Add(s);
            }
            if (FoundStr.Count > 0) return true;
            else return false;
        }
    }

    public class FtAxis
    {
        public FtPoint Orig = new FtPoint();
        public FtLine XAxis, YAxis;
        public FtText XAxisT = new FtText();
        public FtText YAxisT = new FtText();
        public const string UID_XAxis = "Coord_XAxis";
        public const string UID_YAxis = "Coord_YAxis";
        public const string UID_Orig = "Coord_Orig";
        public const string UID_XAxisT = "Coord_XAxisT";
        public const string UID_YAxisT = "Coord_YAxisT";

        public FtAxis()
        {
            XAxis = new FtLine(new WinPoint(0, 0), new WinPoint(10, 0));
            XAxis.Name = UID_XAxis;
            XAxis.Interactive = false;
            XAxis.DisplayPara.StrokeColor = DrawingColor.Red;
            XAxis.element.SetValue(Canvas.ZIndexProperty, -3);
            XAxis.element.SetValue(Path.StrokeEndLineCapProperty, PenLineCap.Triangle);

            YAxis = new FtLine(new WinPoint(0, 0), new WinPoint(0, 10));
            YAxis.Name = UID_YAxis;
            YAxis.DisplayPara.StrokeColor = DrawingColor.Lime;
            YAxis.Interactive = false;
            YAxis.element.SetValue(Canvas.ZIndexProperty, -2);
            YAxis.element.SetValue(Path.StrokeEndLineCapProperty, PenLineCap.Triangle);

            Orig.DisplayPara.StrokeColor = DrawingColor.White;
            Orig.DisplayPara.PointSize = 2.0;
            Orig.Interactive = false;
            Orig.element.SetValue(Canvas.ZIndexProperty, -1);
            Orig.Name = UID_Orig;

            XAxisT.SetXYText(10.5, 0, "X");
            XAxisT.SetFontSizeColor(14, DrawingColor.Red);
            XAxisT.SetAlignment(VerticalAlignment.Center, System.Windows.HorizontalAlignment.Left);
            XAxisT.Name = UID_XAxisT;

            YAxisT.SetXYText(0, 10.5, "Y");
            YAxisT.SetFontSizeColor(14, DrawingColor.Lime);
            YAxisT.SetAlignment(VerticalAlignment.Bottom, System.Windows.HorizontalAlignment.Center);
            YAxisT.Name = UID_YAxisT;

        }
        public void SetCoordDir(CoordDir coord)
        {
            if (coord == CoordDir.X_Y)
            {
                XAxisT.SetAlignment(VerticalAlignment.Center, System.Windows.HorizontalAlignment.Left);

                YAxisT.SetAlignment(VerticalAlignment.Top, System.Windows.HorizontalAlignment.Center);
            }
            else if (coord == CoordDir.X_InvertY)
            {
                XAxisT.SetAlignment(VerticalAlignment.Center, System.Windows.HorizontalAlignment.Left);

                YAxisT.SetAlignment(VerticalAlignment.Bottom, System.Windows.HorizontalAlignment.Center);
            }
            else if (coord == CoordDir.InvertX_Y)
            {
                XAxisT.SetAlignment(VerticalAlignment.Center, System.Windows.HorizontalAlignment.Right);

                YAxisT.SetAlignment(VerticalAlignment.Top, System.Windows.HorizontalAlignment.Center);
            }
            else
            {
                XAxisT.SetAlignment(VerticalAlignment.Center, System.Windows.HorizontalAlignment.Right);

                YAxisT.SetAlignment(VerticalAlignment.Bottom, System.Windows.HorizontalAlignment.Center);
            }
        }
    }

}

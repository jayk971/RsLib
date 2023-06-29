using System;
using System.Collections.Generic;
using System.Windows;
//using System.Drawing;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using WinPoint = System.Windows.Point;
namespace RsLib.Display
{
    public partial class FtDisplay : System.Windows.Forms.UserControl
    {
        public delegate void delegateMouseDoubleClick(double x, double y);
        public event delegateMouseDoubleClick MouseDoubleClickPos;

        public delegate void delegateSelectItem(string ItemName);
        public event delegateSelectItem GetSelectedItem;

        public delegate void delegateItemShift(string Item, double x, double y);
        public event delegateItemShift GetItemShift;


        public FTGraphics2D StaticGraphics = new FTGraphics2D();
        public FTGraphics2D InteractiveGraphics = new FTGraphics2D();
        public List<string> Selection = new List<string>();
        private bool drawingEnabled = true;
        public bool DrawingEnabled
        {
            get
            {
                return drawingEnabled;
            }
            set
            {
                drawingEnabled = value;
                if (drawingEnabled)
                {
                    UpdateDisplay();
                }
            }
        }

        public bool ShowAxis = false;
        public bool ShowCoordinateValue
        {
            set
            {
                statusStrip1.Visible = value;
            }
        }
        Canvas canvas = new Canvas();
        WinPoint MinP = new WinPoint(double.MaxValue, double.MaxValue);
        WinPoint MaxP = new WinPoint(double.MinValue, double.MinValue);
        WinPoint MedP = new WinPoint(0, 0);
        new double Width = 0;
        new double Height = 0;
        double XScale, YScale, AxisRotate;
        Matrix M_basic = new Matrix();
        readonly MatrixTransform _GlobalView = new MatrixTransform();
        WinPoint _initialMousePosition;
        bool _dragging;
        UIElement _selectedElement;
        Matrix ObjectOffset = new Matrix();
        FtAxis ftAxis = new FtAxis();
        CoordDir CurrentDir = CoordDir.X_Y;
        public FtDisplay()
        {
            InitializeComponent();

            canvas.Background = Brushes.Black;
            canvas.SetValue(Canvas.FocusableProperty, true);
            elementHost1.Child = canvas;

            canvas.MouseUp += Canvas_MouseUp;
            canvas.MouseWheel += Canvas_MouseWheel;
            canvas.MouseDown += Canvas_MouseDown;
            canvas.MouseEnter += Canvas_MouseEnter;
            canvas.MouseMove += Canvas_MouseMove;
            canvas.SizeChanged += Canvas_SizeChanged;
            MouseDoubleClickPos += FtDisplay_MouseDoubleClickPos;
            GetSelectedItem += FtDisplay_SelectedItem;
            GetItemShift += FtDisplay_GetItemShift;
            SetCoordinate(CoordDir.X_InvertY, CoordRotate.Rotate0);
            StaticGraphics.AfterRemoveItem += StaticGraphics_AfterRemoveItem;
            InteractiveGraphics.AfterRemoveItem += InteractiveGraphics_AfterRemoveItem;
        }

        private void InteractiveGraphics_AfterRemoveItem(string ItemName)
        {
            if (drawingEnabled)
            {
                int ItemI = -1;
                UIElement e = InteractiveGraphics[ItemName].element;
                ItemI = canvas.Children.IndexOf(e);
                canvas.Children.RemoveAt(ItemI);
            }
        }

        private void StaticGraphics_AfterRemoveItem(string ItemName)
        {
            if (drawingEnabled)
            {
                int ItemI = -1;
                UIElement e = StaticGraphics[ItemName].element;
                ItemI = canvas.Children.IndexOf(e);
                canvas.Children.RemoveAt(ItemI);
            }
        }

        private void FtDisplay_GetItemShift(string Item, double x, double y)
        {
            //dummy function
        }

        public void Clear()
        {
            StaticGraphics.Clear();
            InteractiveGraphics.Clear();
            ObjectOffset.SetIdentity();
            MinP = new WinPoint(double.MaxValue, double.MaxValue);
            MaxP = new WinPoint(double.MinValue, double.MinValue);
            MedP = new WinPoint(0, 0);
            Width = 0;
            Height = 0;
            Selection.Clear();
        }
        private void FtDisplay_SelectedItem(string ItemName)
        {
            //dummy function
        }

        private void FtDisplay_MouseDoubleClickPos(double x, double y)
        {
            //dummy function
        }

        private void FTDisplay_Load(object sender, EventArgs e)
        {
            UpdateDisplay();
        }

        private void CalMaxMin()
        {
            #region Display Interactive Graphic
            MinP = new WinPoint(double.MaxValue, double.MaxValue);
            MaxP = new WinPoint(double.MinValue, double.MinValue);
            foreach (KeyValuePair<string, IFtObject> kvp in StaticGraphics)
            {
                UIElement element = kvp.Value.element;
                Type type = kvp.Value.GetType();
                if (type.BaseType == typeof(FtObject))
                {
                    FtObject ftObject = kvp.Value as FtObject;
                    double minX, minY, maxX, maxY = 0;
                    ftObject.GetGeometryBound(out minX, out minY, out maxX, out maxY);

                    if (MinP.X > minX) MinP.X = minX;
                    if (MinP.Y > minY) MinP.Y = minY;
                    if (MaxP.X < maxX) MaxP.X = maxX;
                    if (MaxP.Y < maxY) MaxP.Y = maxY;
                }
            }
            #endregion

            #region Display Interactive Graphic
            foreach (KeyValuePair<string, IFtObject> kvp in InteractiveGraphics)
            {
                UIElement element = kvp.Value.element;
                Type type = kvp.Value.GetType();
                if (type.BaseType == typeof(FtObject))
                {
                    FtObject ftObject = kvp.Value as FtObject;
                    double minX, minY, maxX, maxY = 0;
                    ftObject.GetGeometryBound(out minX, out minY, out maxX, out maxY);

                    if (MinP.X > minX) MinP.X = minX;
                    if (MinP.Y > minY) MinP.Y = minY;
                    if (MaxP.X < maxX) MaxP.X = maxX;
                    if (MaxP.Y < maxY) MaxP.Y = maxY;
                }
            }
            #endregion
            #region Cal Median Point, Width, Height
            MedP.X = (MinP.X + MaxP.X) / 2;
            MedP.Y = (MinP.Y + MaxP.Y) / 2;
            Width = MaxP.X - MinP.X;
            Height = MaxP.Y - MinP.Y;
            #endregion
        }
        private void Canvas_SizeChanged(object sender, System.Windows.SizeChangedEventArgs e)
        {
            try
            {
                Fit();
            }
            catch (Exception ex)
            { throw ex; }
        }
        private void GetXScaleYScale(CoordDir coordDir, out double xScale, out double yScale)
        {
            xScale = 1.0;
            yScale = 1.0;
            switch (coordDir)
            {
                case CoordDir.X_Y:
                    xScale = 1.0;
                    yScale = 1.0;
                    break;
                case CoordDir.X_InvertY:
                    xScale = 1.0;
                    yScale = -1.0;
                    break;
                case CoordDir.InvertX_Y:
                    xScale = -1.0;
                    yScale = 1.0;
                    break;
                case CoordDir.InvertX_InvertY:
                    xScale = -1.0;
                    yScale = -1.0;
                    break;
                default:
                    xScale = 1.0;
                    yScale = -1.0;
                    break;

            }

        }
        private void SetCoordinate(CoordDir coordDir, CoordRotate coordRotate)
        {
            CurrentDir = coordDir;
            GetXScaleYScale(CurrentDir, out XScale, out YScale);
            ftAxis.SetCoordDir(CurrentDir);

            switch (coordRotate)
            {
                case CoordRotate.Rotate0:
                    AxisRotate = 0;
                    break;
                case CoordRotate.Rotate90:
                    AxisRotate = 90;
                    break;
                case CoordRotate.Rotate180:
                    AxisRotate = 180;

                    break;
                case CoordRotate.Rotate270:
                    AxisRotate = 270;

                    break;
                default:
                    AxisRotate = 0;

                    break;

            }
            Fit();

            //ResetMatrix(elementHost1.Width/2,elementHost1.Height/2);
        }//暫時不考慮旋轉坐標系

        public void SetCoordinate(CoordDir coordDir)
        {
            CurrentDir = coordDir;
            GetXScaleYScale(CurrentDir, out XScale, out YScale);
            ftAxis.SetCoordDir(CurrentDir);
            if (ShowAxis) AddCoordinateAxis();
            Fit();

            //ResetMatrix(elementHost1.Width/2,elementHost1.Height/2);
        }
        private void ResetMatrix(double X, double Y)
        {
            //MedPosX = elementHost1.Width / 2;
            //MedPosY = elementHost1.Height / 2;

            M_basic.SetIdentity();
            M_basic.ScalePrepend(XScale, YScale);
            //M_basic.ScalePrepend(1.5, -1.5);
            M_basic.TranslatePrepend(XScale * X, YScale * Y);
            M_basic.RotatePrepend(AxisRotate);

            _GlobalView.Matrix = M_basic;
        }
        private void Canvas_MouseUp(object sender, MouseButtonEventArgs e)
        {
            try
            {
                canvas.Cursor = System.Windows.Input.Cursors.Arrow;
                if (e.ChangedButton == MouseButton.Left)
                {
                    if (_selectedElement != null)
                    {
                        string UID = _selectedElement.Uid;
                        if (_dragging)
                        {
                            if (InteractiveGraphics.ContainsKey(UID))
                            {
                                FtObject ftObject = InteractiveGraphics[UID] as FtObject;
                                ftObject.M_Offset = ObjectOffset * ftObject.M_Offset;
                                GetItemShift(UID, ObjectOffset.OffsetX, ObjectOffset.OffsetY);

                                ObjectOffset.SetIdentity();
                                CalMaxMin();
                            }
                            _dragging = false;
                        }
                        else
                        {

                        }
                    }
                    _dragging = false;
                }
                //if(e.ChangedButton == MouseButton.Right)
                //{
                //    GetSelectedItem("");
                //}
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        private void Canvas_MouseMove(object sender, System.Windows.Input.MouseEventArgs e)
        {
            try
            {
                //Matrix M_FinalInvert = _GlobalView.Matrix;
                //M_FinalInvert.Invert();
                WinPoint MousePos = e.GetPosition(canvas);
                //WinPoint mousePosition = M_FinalInvert.Transform(MousePos);
                WinPoint mousePosition = CanvasCoord2ViewCoord(MousePos);

                double PosX = Math.Round(mousePosition.X, 2);
                double PosY = Math.Round(mousePosition.Y, 2);

                UpdateToolStripStatusLabel(toolStripStatusLabel_X, PosX.ToString("F2"));
                UpdateToolStripStatusLabel(toolStripStatusLabel_Y, PosY.ToString("F2"));


                if (e.RightButton == MouseButtonState.Pressed)
                {
                    canvas.Cursor = System.Windows.Input.Cursors.Cross;
                    Vector delta = Point.Subtract(mousePosition, _initialMousePosition);
                    var translate = new TranslateTransform(delta.X, delta.Y);
                    _GlobalView.Matrix = translate.Value * _GlobalView.Matrix;
                    UpdateTransform();
                }


                #region Drag Item
                if (e.LeftButton == MouseButtonState.Pressed)
                {
                    double x = Mouse.GetPosition(canvas).X;
                    double y = Mouse.GetPosition(canvas).Y;

                    if (_selectedElement != null)
                    {
                        if (InteractiveGraphics.ContainsKey(_selectedElement.Uid))
                        {
                            if (InteractiveGraphics[_selectedElement.Uid].DOF == FtObjectDOF.Shift)
                            {
                                _dragging = true;
                                Vector delta = Point.Subtract(mousePosition, _initialMousePosition);
                                UpdateSelectTransform(delta.X, delta.Y);
                            }
                        }
                    }
                }
                #endregion
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void Canvas_MouseEnter(object sender, System.Windows.Input.MouseEventArgs e)
        {
            elementHost1.Child.Focus();
        }

        private void Canvas_MouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            try
            {
                if (e.ClickCount == 2)
                {
                    if (e.ChangedButton == MouseButton.Left)
                    {
                        _initialMousePosition = CanvasCoord2ViewCoord(e.GetPosition(canvas));

                        double x = Math.Round(_initialMousePosition.X, 2);
                        double y = Math.Round(_initialMousePosition.Y, 2);
                        MouseDoubleClickPos(x, y);
                    }
                }
                else if (e.ClickCount == 1)
                {
                    WinPoint CanvasP = e.GetPosition(canvas);
                    _initialMousePosition = CanvasCoord2ViewCoord(CanvasP);

                    #region Drag Item
                    if (e.ChangedButton == MouseButton.Left)
                    {
                        if (canvas.Children.Contains((UIElement)e.Source))
                        {
                            ResetSelectedElement();
                            _selectedElement = (UIElement)e.Source;
                            string UID = _selectedElement.Uid;
                            if (InteractiveGraphics.ContainsKey(UID))
                            {
                                if (InteractiveGraphics[UID].Interactive)
                                {
                                    Selection.Add(UID);
                                    SelectElement(UID);
                                    GetSelectedItem(UID);
                                }
                                else
                                {
                                    ResetSelectedElement();
                                    GetSelectedItem("");
                                }
                            }
                            else
                            {
                                ResetSelectedElement();
                                GetSelectedItem("");
                            }

                        }
                        else
                        {
                            ResetSelectedElement();
                            GetSelectedItem("");
                        }

                    }
                    else if (e.ChangedButton == MouseButton.Middle)
                    {
                        Fit();
                    }
                    #endregion
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void Canvas_MouseWheel(object sender, System.Windows.Input.MouseWheelEventArgs e)
        {
            try
            {
                float scaleFactor = 1.1f;
                if (e.Delta < 0)
                {
                    scaleFactor = 1f / scaleFactor;
                }
                Point mousePostion = e.GetPosition(canvas);

                Matrix scaleMatrix = _GlobalView.Matrix;
                scaleMatrix.ScaleAt(scaleFactor, scaleFactor, mousePostion.X, mousePostion.Y);
                _GlobalView.Matrix = scaleMatrix;
                UpdateTransform();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private void SelectElement(string elementUID)
        {
            if (InteractiveGraphics.ContainsKey(elementUID))
            {
                InteractiveGraphics[elementUID].IsSelected = true;
                _selectedElement = InteractiveGraphics[elementUID].element;
                //foreach (UIElement uI in canvas.Children)
                //{
                //    if (uI.Uid == elementUID)
                //    {
                //        FtObject ftObject = InteractiveGraphics[elementUID] as FtObject;

                //        uI.SetValue(Polyline.StrokeProperty, ftObject.SelectedDisplayPara.StrokeBrush);
                //        uI.SetValue(Polyline.StrokeThicknessProperty, ftObject.SelectedDisplayPara.LineWidth);
                //        _selectedElement = uI;
                //    }
                //}
            }
        }
        private void ResetSelectedElement()
        {

            //foreach (UIElement uI in canvas.Children)
            //{
            //    if (InteractiveGraphics.ContainsKey(uI.Uid))
            //    {
            //        FtObject ftObject = InteractiveGraphics[uI.Uid] as FtObject;
            //        uI.SetValue(Polyline.StrokeProperty, ftObject.DisplayPara.StrokeBrush);
            //        uI.SetValue(Polyline.StrokeThicknessProperty, ftObject.DisplayPara.LineWidth);
            //    }
            //    if(_selectedElement != null) _selectedElement = null;
            //}
            if (_selectedElement != null)
            {
                if (InteractiveGraphics.ContainsKey(_selectedElement.Uid))
                {
                    InteractiveGraphics[_selectedElement.Uid].IsSelected = false;

                    //FtObject ftObject = InteractiveGraphics[_selectedElement.Uid] as FtObject;
                    //_selectedElement.SetValue(Polyline.StrokeProperty, ftObject.DisplayPara.StrokeBrush);
                    //_selectedElement.SetValue(Polyline.StrokeThicknessProperty, ftObject.DisplayPara.LineWidth);
                }
                _selectedElement = null;
                Selection.Clear();
            }
        }
        private void UpdateTransform()
        {
            try
            {
                foreach (UIElement child in canvas.Children)
                {
                    if (InteractiveGraphics.ContainsKey(child.Uid))//interactive 可選 可移動
                    {
                        if (typeof(TextBlock) == child.GetType())//字型要翻正
                        {
                            FtText ftText = InteractiveGraphics[child.Uid] as FtText;
                            Matrix matrix = new Matrix();
                            matrix = GetTextAlignMatrix(child, ftText, CurrentDir);
                            MatrixTransform scaleBack = new MatrixTransform();
                            scaleBack.Matrix = matrix * _GlobalView.Value;
                            child.RenderTransform = scaleBack;
                        }
                        else
                        {
                            MatrixTransform mt = new MatrixTransform();
                            FtObject ftObject = InteractiveGraphics[child.Uid] as FtObject;
                            mt.Matrix = ObjectOffset * ftObject.M_Offset * _GlobalView.Matrix;
                            child.RenderTransform = mt;
                        }
                    }
                    else if (StaticGraphics.ContainsKey(child.Uid))//static 不可選 不可移動
                    {
                        if (typeof(TextBlock) == child.GetType())//字型要翻正
                        {
                            FtText ftText = StaticGraphics[child.Uid] as FtText;
                            Matrix matrix = new Matrix();
                            matrix = GetTextAlignMatrix(child, ftText, CurrentDir);
                            MatrixTransform scaleBack = new MatrixTransform();
                            scaleBack.Matrix = matrix * _GlobalView.Value;
                            child.RenderTransform = scaleBack;
                        }
                        else
                        {
                            child.RenderTransform = _GlobalView;
                        }
                    }
                    else//原點軸
                    {
                        if (child.Uid == FtAxis.UID_XAxisT)
                        {
                            Matrix matrix = new Matrix();
                            matrix = GetTextAlignMatrix(child, ftAxis.XAxisT, CurrentDir);
                            MatrixTransform scaleBack = new MatrixTransform();
                            scaleBack.Matrix = matrix * _GlobalView.Value;

                            child.RenderTransform = scaleBack;
                        }
                        else if (child.Uid == FtAxis.UID_YAxisT)
                        {
                            Matrix matrix = new Matrix();
                            matrix = GetTextAlignMatrix(child, ftAxis.YAxisT, CurrentDir);
                            MatrixTransform scaleBack = new MatrixTransform();
                            scaleBack.Matrix = matrix * _GlobalView.Value;

                            child.RenderTransform = scaleBack;
                        }
                        else
                        {

                            child.RenderTransform = _GlobalView;
                            UpdateToolStripStatusLabel(toolStripStatusLabel_Scale, _GlobalView.Matrix.M11.ToString("F2"));

                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private void UpdateSelectTransform(double x, double y)
        {
            if (_selectedElement != null)
            {
                if (InteractiveGraphics.ContainsKey(_selectedElement.Uid))
                {
                    ObjectOffset = new Matrix();
                    ObjectOffset.TranslatePrepend(x, y);

                    MatrixTransform mt = new MatrixTransform();
                    FtObject ftObject = InteractiveGraphics[_selectedElement.Uid] as FtObject;
                    mt.Matrix = ObjectOffset * ftObject.M_Offset * _GlobalView.Matrix;
                    _selectedElement.RenderTransform = mt;

                    Path path = _selectedElement as Path;
                    Rect rect = path.Data.Bounds;
                    if (MinP.X > rect.TopLeft.X) MinP.X = rect.TopLeft.X;
                    if (MinP.Y > rect.TopLeft.Y) MinP.Y = rect.TopLeft.Y;
                    if (MaxP.X < rect.BottomRight.X) MaxP.X = rect.BottomRight.X;
                    if (MaxP.Y < rect.BottomRight.Y) MaxP.Y = rect.BottomRight.Y;
                }
            }
        }
        private Matrix GetTextAlignMatrix(UIElement element, FtText text, CoordDir coord)
        {
            Matrix matrix = new Matrix();
            element.Measure(new System.Windows.Size(double.PositiveInfinity, double.PositiveInfinity));
            Size tt = element.DesiredSize;

            double x = 0.0;
            double y = 0.0;
            double xs = 1.0;
            double ys = 1.0;
            GetXScaleYScale(coord, out xs, out ys);
            ComFunc.TextAlignmentMatrix(text.verticalAlignment, text.horizontalAlignment, tt, out x, out y);
            //matrix.ScalePrepend(1, -1);
            matrix.ScalePrepend(xs, ys);
            matrix.TranslatePrepend(x, y);//對齊角點
            matrix.TranslatePrepend(xs * text.Pos.X, ys * text.Pos.Y);
            return matrix;
        }
        private void AddCoordinateAxis()
        {
            canvas.Children.Add(ftAxis.Orig.element);
            canvas.Children.Add(ftAxis.XAxis.element);
            canvas.Children.Add(ftAxis.YAxis.element);
            canvas.Children.Add(ftAxis.XAxisT.element);
            canvas.Children.Add(ftAxis.YAxisT.element);
        }
        private void UpdateDisplay(bool AutoFit = true)
        {
            try
            {
                canvas.Children.Clear();
                if (ShowAxis)
                    AddCoordinateAxis();
                #region Display Static Graphic
                //AddCoordinateAxis();
                foreach (KeyValuePair<string, IFtObject> kvp in StaticGraphics)
                {
                    UIElement element = kvp.Value.element;

                    canvas.Children.Add(element);
                    Type type = kvp.Value.GetType();
                    if (type.BaseType == typeof(FtObject))
                    {
                        FtObject ftObject = kvp.Value as FtObject;
                        double minX, minY, maxX, maxY = 0;
                        ftObject.GetGeometryBound(out minX, out minY, out maxX, out maxY);

                        if (MinP.X > minX) MinP.X = minX;
                        if (MinP.Y > minY) MinP.Y = minY;
                        if (MaxP.X < maxX) MaxP.X = maxX;
                        if (MaxP.Y < maxY) MaxP.Y = maxY;
                    }
                }

                #endregion
                #region Display Interactive Graphic
                foreach (KeyValuePair<string, IFtObject> kvp in InteractiveGraphics)
                {
                    UIElement element = kvp.Value.element;

                    canvas.Children.Add(element);
                    Type type = kvp.Value.GetType();
                    if (type.BaseType == typeof(FtObject))
                    {
                        FtObject ftObject = kvp.Value as FtObject;
                        double minX, minY, maxX, maxY = 0;
                        ftObject.GetGeometryBound(out minX, out minY, out maxX, out maxY);

                        if (MinP.X > minX) MinP.X = minX;
                        if (MinP.Y > minY) MinP.Y = minY;
                        if (MaxP.X < maxX) MaxP.X = maxX;
                        if (MaxP.Y < maxY) MaxP.Y = maxY;
                    }
                }
                #endregion
                CalMedWidthHeight();
                Fit(AutoFit);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private void CalMedWidthHeight()
        {
            #region Cal Median Point, Width, Height
            MedP.X = (MinP.X + MaxP.X) / 2;
            MedP.Y = (MinP.Y + MaxP.Y) / 2;
            Width = MaxP.X - MinP.X;
            Height = MaxP.Y - MinP.Y;
            #endregion
        }
        public void Fit(bool AutoFit = true)
        {
            if (InteractiveGraphics.Count == 0 && StaticGraphics.Count == 0)
                ResetMatrix(elementHost1.Width / 2, elementHost1.Height / 2);
            else
            {
                if (AutoFit)
                {
                    double x = 0;
                    double y = 0;
                    double xScale = 1.0;
                    double yScale = 1.0;
                    GetXScaleYScale(CurrentDir, out xScale, out yScale);

                    x = elementHost1.Width / 2 - xScale * MedP.X;
                    y = elementHost1.Height / 2 - yScale * MedP.Y;
                    WinPoint tttt = ViewCoord2CanvasCoord(MedP);
                    double xx = elementHost1.Width / 2 - tttt.X;
                    double yy = elementHost1.Height / 2 - tttt.Y;
                    ResetMatrix(x, y);

                    double ScaleX = elementHost1.Width / Width;
                    double ScaleY = elementHost1.Height / Height;
                    double scaleFactor = Math.Min(ScaleX, ScaleY);
                    Matrix scaleMatrix = _GlobalView.Matrix;
                    scaleMatrix.ScaleAt(scaleFactor, scaleFactor, elementHost1.Width / 2, elementHost1.Height / 2);
                    _GlobalView.Matrix = scaleMatrix;
                }
            }
            UpdateTransform();
        }

        private delegate void delegateToolStripStatusLabel(System.Windows.Forms.ToolStripStatusLabel ctrl, string text);
        private void UpdateToolStripStatusLabel(System.Windows.Forms.ToolStripStatusLabel ctrl, string text)
        {
            if (this.InvokeRequired)
            {
                delegateToolStripStatusLabel uI = new delegateToolStripStatusLabel(UpdateToolStripStatusLabel);
                this.Invoke(uI, ctrl, text);
            }
            else
            {
                ctrl.Text = text;
            }
        }
        public void UnSelectItem()
        {
            ResetSelectedElement();
        }
        public void SelectItem(string ItemName)
        {
            ResetSelectedElement();
            SelectElement(ItemName);

            if (InteractiveGraphics.ContainsKey(ItemName))
            {
                UIElement e = InteractiveGraphics[ItemName].element;
                int i = canvas.Children.IndexOf(e);
            }
            //GetSelectedItem(ItemName);
        }
        public void AddStaticGraphics(string Name, IFtObject Shape)
        {
            if (StaticGraphics.ContainsKey(Name) || InteractiveGraphics.ContainsKey(Name))
            {
                if (StaticGraphics.ContainsKey(Name))
                    throw new Exception($"StaticGraphics contain same key - \"{Name}\"");
                else
                    throw new Exception($"InteractiveGraphics contain same key - \"{Name}\"");
            }
            else
            {
                Shape.Interactive = false;
                Canvas.SetZIndex(Shape.element, StaticGraphics.Count);
                StaticGraphics.Add(Name, Shape);
                if (drawingEnabled)
                {
                    UIElement element = Shape.element;
                    canvas.Children.Add(element);
                    Type type = Shape.GetType();
                    if (type.BaseType == typeof(FtObject))
                    {
                        FtObject ftObject = Shape as FtObject;
                        double minX, minY, maxX, maxY = 0;
                        ftObject.GetGeometryBound(out minX, out minY, out maxX, out maxY);

                        if (MinP.X > minX) MinP.X = minX;
                        if (MinP.Y > minY) MinP.Y = minY;
                        if (MaxP.X < maxX) MaxP.X = maxX;
                        if (MaxP.Y < maxY) MaxP.Y = maxY;
                    }
                    //CalMedWidthHeight();
                    UpdateTransform();
                    //Fit();
                }
            }
        }
        public bool AddInteractiveGraphics(string Name, IFtObject Shape)
        {
            if (StaticGraphics.ContainsKey(Name) || InteractiveGraphics.ContainsKey(Name))
            {
                if (StaticGraphics.ContainsKey(Name))
                    throw new Exception($"StaticGraphics contain same key - \"{Name}\"");
                else
                    throw new Exception($"InteractiveGraphics contain same key - \"{Name}\"");
            }
            else
            {
                Shape.Interactive = true;
                Canvas.SetZIndex(Shape.element, 1000 + InteractiveGraphics.Count);
                InteractiveGraphics.Add(Name, Shape);
                if (drawingEnabled)
                {
                    UIElement element = Shape.element;
                    canvas.Children.Add(element);
                    Type type = Shape.GetType();
                    if (type.BaseType == typeof(FtObject))
                    {
                        FtObject ftObject = Shape as FtObject;
                        double minX, minY, maxX, maxY = 0;
                        ftObject.GetGeometryBound(out minX, out minY, out maxX, out maxY);

                        if (MinP.X > minX) MinP.X = minX;
                        if (MinP.Y > minY) MinP.Y = minY;
                        if (MaxP.X < maxX) MaxP.X = maxX;
                        if (MaxP.Y < maxY) MaxP.Y = maxY;
                    }
                    //CalMedWidthHeight();
                    UpdateTransform();
                    //Fit();
                }
                return true;
            }
        }

        public void ViewCoord2CanvasCoord(double InX, double InY, out double OutX, out double OutY)
        {
            OutX = 0;
            OutY = 0;
            WinPoint CanvasP = _GlobalView.Matrix.Transform(new WinPoint(InX, InY));
            OutX = CanvasP.X;
            OutY = CanvasP.Y;
        }
        public WinPoint ViewCoord2CanvasCoord(WinPoint InPoint)
        {
            WinPoint CanvasP = _GlobalView.Matrix.Transform(InPoint);
            return CanvasP;
        }
        public void CanvasCoord2ViewCoord(double InX, double InY, out double OutX, out double OutY)
        {
            OutX = 0;
            OutY = 0;
            Matrix M_FinalInvert = _GlobalView.Matrix;
            M_FinalInvert.Invert();
            WinPoint CanvasP = M_FinalInvert.Transform(new WinPoint(InX, InY));
            OutX = CanvasP.X;
            OutY = CanvasP.Y;
        }
        public WinPoint CanvasCoord2ViewCoord(WinPoint InPoint)
        {
            Matrix M_FinalInvert = _GlobalView.Matrix;
            M_FinalInvert.Invert();
            WinPoint CanvasP = M_FinalInvert.Transform(InPoint);
            return CanvasP;
        }

        public void SaveAllRangeImage(string FilePath, double Scale = 1.0)
        {
            try
            {
                canvas.UpdateLayout();
                canvas.Measure(new System.Windows.Size(double.PositiveInfinity, double.PositiveInfinity));
                RenderTargetBitmap rtb = new RenderTargetBitmap((int)(canvas.ActualWidth * Scale),
                 (int)(canvas.ActualHeight * Scale), 96 * Scale, 96 * Scale, PixelFormats.Default);
                //放大倍率 = (圖片寬度 / Canvas 畫圖寬度)
                //RenderTargetBitmap rtb = new RenderTargetBitmap(圖片寬度,圖片高度, 96 *放大倍率 , 96 * 放大倍率, PixelFormats.Default);
                rtb.Render(canvas);

                //WinPoint CanvasMinP = _GlobalView.Matrix.Transform(MinP);
                //WinPoint CanvasMaxP = _GlobalView.Matrix.Transform(MaxP);
                WinPoint CanvasMinP = ViewCoord2CanvasCoord(MinP);
                WinPoint CanvasMaxP = ViewCoord2CanvasCoord(MaxP);
                WinPoint FinalMinP = new WinPoint();
                WinPoint FinalMaxP = new WinPoint();

                FinalMinP.X = Math.Min(CanvasMinP.X, CanvasMaxP.X);
                FinalMinP.Y = Math.Min(CanvasMinP.Y, CanvasMaxP.Y);
                FinalMaxP.X = Math.Max(CanvasMinP.X, CanvasMaxP.X);
                FinalMaxP.Y = Math.Max(CanvasMinP.Y, CanvasMaxP.Y);
                int CalWidth = (int)Math.Ceiling((FinalMaxP.X - FinalMinP.X) * Scale);
                int CalHeight = (int)Math.Ceiling((FinalMaxP.Y - FinalMinP.Y) * Scale);

                if (CalWidth <= 0 || CalHeight <= 0) return;

                Int32Rect rect = new Int32Rect();
                rect.X = ((int)Math.Floor(FinalMinP.X) < 0) ? 0 : (int)Math.Floor(FinalMinP.X * Scale);
                rect.Y = ((int)Math.Floor(FinalMinP.Y) < 0) ? 0 : (int)Math.Floor(FinalMinP.Y * Scale);
                rect.Width = (CalWidth * Scale) > (canvas.ActualWidth * Scale) ? (int)(canvas.ActualWidth * Scale) : (int)(CalWidth * Scale);
                rect.Height = (CalHeight * Scale) > (canvas.ActualHeight * Scale) ? (int)(canvas.ActualHeight * Scale) : (int)(CalHeight * Scale);


                var crop = new CroppedBitmap(rtb, rect);

                BitmapEncoder pngEncoder = new PngBitmapEncoder();
                pngEncoder.Frames.Add(BitmapFrame.Create(crop));
                using (var fs = System.IO.File.OpenWrite(FilePath))
                {
                    pngEncoder.Save(fs);

                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public System.Drawing.Image CreateContentBitmap(double Scale = 1.0)
        {
            try
            {
                canvas.UpdateLayout();
                canvas.Measure(new System.Windows.Size(double.PositiveInfinity, double.PositiveInfinity));
                RenderTargetBitmap rtb = new RenderTargetBitmap((int)(canvas.ActualWidth * Scale),
                    (int)(canvas.ActualHeight * Scale), 96 * Scale, 96 * Scale, PixelFormats.Default);
                rtb.Render(canvas);

                WinPoint CanvasMinP = ViewCoord2CanvasCoord(MinP);
                WinPoint CanvasMaxP = ViewCoord2CanvasCoord(MaxP);
                WinPoint FinalMinP = new WinPoint();
                WinPoint FinalMaxP = new WinPoint();

                FinalMinP.X = Math.Min(CanvasMinP.X, CanvasMaxP.X);
                FinalMinP.Y = Math.Min(CanvasMinP.Y, CanvasMaxP.Y);
                FinalMaxP.X = Math.Max(CanvasMinP.X, CanvasMaxP.X);
                FinalMaxP.Y = Math.Max(CanvasMinP.Y, CanvasMaxP.Y);
                int CalWidth = (int)Math.Ceiling((FinalMaxP.X - FinalMinP.X));
                int CalHeight = (int)Math.Ceiling((FinalMaxP.Y - FinalMinP.Y));

                if (CalWidth <= 0 || CalHeight <= 0) return null;

                Int32Rect rect = new Int32Rect();
                rect.X = ((int)Math.Floor(FinalMinP.X) < 0) ? 0 : (int)Math.Floor(FinalMinP.X * Scale);
                rect.Y = ((int)Math.Floor(FinalMinP.Y) < 0) ? 0 : (int)Math.Floor(FinalMinP.Y * Scale);
                rect.Width = (CalWidth * Scale) > (canvas.ActualWidth * Scale) ? (int)(canvas.ActualWidth * Scale) : (int)(CalWidth * Scale);
                rect.Height = (CalHeight * Scale) > (canvas.ActualHeight * Scale) ? (int)(canvas.ActualHeight * Scale) : (int)(CalHeight * Scale);


                var crop = new CroppedBitmap(rtb, rect);

                BitmapEncoder pngEncoder = new PngBitmapEncoder();
                pngEncoder.Frames.Add(BitmapFrame.Create(crop));
                System.IO.MemoryStream ms = new System.IO.MemoryStream();
                //BitmapImage bitmap = new BitmapImage();
                pngEncoder.Save(ms);
                System.Drawing.Image image = System.Drawing.Image.FromStream(ms);
                ms.Flush();
                ms.Close();
                return image;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }

    public enum CoordRotate : int
    {
        Rotate0 = 0,
        Rotate90,
        Rotate180,
        Rotate270
    }
    public enum CoordDir : int
    {
        X_Y = 0,
        X_InvertY,
        InvertX_Y,
        InvertX_InvertY
    }
}

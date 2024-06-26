﻿using OpenTK;
using OpenTK.Graphics.OpenGL;
using OpenTK.Input;
using RsLib.Common;
using RsLib.Display3D.Properties;
using RsLib.PointCloudLib;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Reflection;
using System.Windows.Forms;
using System.Linq;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ProgressBar;

namespace RsLib.Display3D
{
    public partial class Display3DControl : UserControl
    {
        GLControl _glControl;
        private float _scale = 1.0f;
        private Vector3 _translation = Vector3.Zero;
        bool _leftButtonPressed = false;
        bool _rightButtonPressed = false;

        int _middleMouseCount = 0;
        Matrix4 _rotationMatrix = Matrix4.Identity;
        int _lastX, _lastY;
        int _lastRotX, _lastRotY;
        int _headList;
        int _maxDisplayList;
        PointPickMode _pickMode = PointPickMode.None;
        bool _haveClosestPoint = false;
        bool _haveSelectPath = false;
        int _CurrentSelectObjectIndex = -1;
        Dictionary<int, Object3D> _displayObject = new Dictionary<int, Object3D>();
        Dictionary<int, DisplayObjectOption> _displayOption = new Dictionary<int, DisplayObjectOption>();
        const float _closetDisLimit = 1.0f;
        bool _GLUpdateDone = true;
        public DisplayObjectOption CurrentSelectObjOption => GetDisplayObjectOption(_CurrentSelectObjectIndex);
        public Object3D CurrentSelectObj => GetDisplayObject(_CurrentSelectObjectIndex);
        Point3D _closetPoint = new Point3D();
        PointCloud _multiSelectPoints = new PointCloud();
        PointCloud _tempMultiSelectPoints = new PointCloud();

        Polyline _SelectPath = new Polyline();
        Polyline _DrawPath = new Polyline();


        Vector3 _maxPoint = new Vector3(float.MinValue, float.MinValue, float.MinValue);
        Vector3 _minPoint = new Vector3(float.MaxValue, float.MaxValue, float.MaxValue);
        Vector3 _oldAvgPoint = new Vector3();
        Vector3 _avgPoint
        {
            get
            {
                float avgX = (_maxPoint.X + _minPoint.X) / 2;
                float avgY = (_maxPoint.Y + _minPoint.Y) / 2;
                float avgZ = (_maxPoint.Z + _minPoint.Z) / 2;

                return new Vector3(avgX, avgY, avgZ);
            }
        }
        bool _isCheckedMaxMin = false;

        int _MultiSelectLineIndex_Start = -1;
        int _MultiSelectLineIndex_End = -1;

        int _MultiSelectPtIndex_Start = -1;
        int _MultiSelectPtIndex_End = -1;
        double _VectorLength = 10.0;
        #region event

        private void _glControl_SizeChanged(object sender, EventArgs e)
        {
        }
        private void GlControl_Load(object sender, EventArgs e)
        {
            // Initialize the OpenGL context
            GL.ShadeModel(ShadingModel.Smooth);
            GL.ClearColor(0.0f, 0.0f, 0.0f, 0.0f);
            GL.ClearDepth(1.0f);

            GL.Enable(EnableCap.PointSmooth);
            GL.Enable(EnableCap.LineSmooth);
            GL.BlendFunc(BlendingFactor.SrcAlpha, BlendingFactor.OneMinusSrcAlpha);
            GL.Enable(EnableCap.Blend);
            GL.Hint(HintTarget.PerspectiveCorrectionHint, HintMode.Nicest);
            GL.Enable(EnableCap.DepthTest);
            //GL.Disable(EnableCap.Lighting);
            _headList = GL.GenLists(_maxDisplayList);
            BuildAxis();
            timer1.Enabled = true;
        }
        private void GlControl_Paint(object sender, PaintEventArgs e)
        {
            _GLUpdateDone = false;
            GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);

            GL.MatrixMode(MatrixMode.Projection);
            GL.LoadIdentity();
            double view_range = 300;
            double aspect = (double)_glControl.Width / _glControl.Height;
            if (aspect >= 1.0)
                GL.Ortho(-view_range * aspect, view_range * aspect, -view_range, view_range, -8 * view_range, 8 * view_range);
            else
                GL.Ortho(-view_range, view_range, -view_range / aspect, view_range / aspect, -8 * view_range, 8 * view_range);

            GL.MatrixMode(MatrixMode.Modelview);
            GL.LoadIdentity();
            GL.PushMatrix();
            multiMatrix();

            GL.Viewport(0, 0, _glControl.Width, _glControl.Height);

            GL.CallList(_headList);
            foreach (var item in _displayOption)
            {
                DisplayObjectOption option = item.Value;
                if (option.IsDisplay) GL.CallList(option.ID);
            }
            if(_haveSelectPath)
            {
                drawSelectPath();
            }
            if (_pickMode == PointPickMode.One)
            {
                if (_haveClosestPoint)
                    drawSelectedPoint();
            }
            else if (_pickMode == PointPickMode.Two)
            {
                if (_multiSelectPoints.Count <= 2)
                    drawMeasureLine();
            }
            else if (_pickMode == PointPickMode.Multiple)
            {
                drawTempMultipleSelect();
                drawMultipleSelect();
            }
            else if(_pickMode == PointPickMode.Draw)
            {
                drawDrawPath();
            }
            if (_CurrentSelectObjectIndex > 1)
                //drawSelectRangeFrame();

            GL.MatrixMode(MatrixMode.Modelview);
            GL.PopMatrix();
            _glControl.SwapBuffers();
            _GLUpdateDone = true;
            //SpinWait.SpinUntil(() => false, 100);
            //_glControl.Invalidate();
        }
        private void GlControl_MouseDoubleClick(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Middle)
            {
                ResetView();
            }
        }
        private void GlControl_MouseClick(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Middle)
            {
                MiddleMouseButtonClick?.Invoke();
                if (_pickMode == PointPickMode.None) return;

                int viewportX = 0;
                int viewportY = 0;
                int viewportWidth = _glControl.Width;
                int viewportHeight = _glControl.Height;

                GL.MatrixMode(MatrixMode.Modelview);
                GL.LoadIdentity();
                GL.PushMatrix();
                multiMatrix();
                // Get the world coordinates of the mouse click
                Vector3 near = new Vector3(e.X, _glControl.Height - e.Y, 0f);
                Vector3 far = new Vector3(e.X, _glControl.Height - e.Y, 1f);
                Vector3 nearWorld = unProject(near, viewportX, viewportY, viewportWidth, viewportHeight);
                Vector3 farWorld = unProject(far, viewportX, viewportY, viewportWidth, viewportHeight);
                GL.PopMatrix();
                _haveClosestPoint = closestPoint(nearWorld, farWorld, out _closetPoint);

                AfterMediemButtonClick?.Invoke(
                    new double[] { nearWorld.X, nearWorld.Y, nearWorld.Z },
                    new double[] { farWorld.X, farWorld.Y, farWorld.Z });

                switch (_pickMode)
                {
                    case PointPickMode.One:
                        _middleMouseCount = 0;
                        _multiSelectPoints.Clear();
                        _multiSelectPoints.Add(_closetPoint);
                        showSelectPointData(_haveClosestPoint, _closetPoint);
                        if(_haveClosestPoint) AfterPointSelected?.Invoke(_closetPoint);
                        //AfterPointsSelected?.Invoke(_multiSelectPoints);
                        break;

                    case PointPickMode.Two:
                        if (_haveClosestPoint)
                        {
                            if (_middleMouseCount % 2 == 0)
                            {
                                _multiSelectPoints.Clear();
                                _multiSelectPoints.Add(_closetPoint);
                                lbl_PickPointMode.Text = "Pick 2nd Point - Middle Click";
                                _middleMouseCount++;

                            }
                            else
                            {
                                _multiSelectPoints.Add(_closetPoint);
                                lbl_PickPointMode.Text = "Measure";
                                _middleMouseCount++;
                                showSelectMeasurePointData(_multiSelectPoints);
                                //AfterPointsSelected?.Invoke(_multiSelectPoints);
                            }
                        }
                        else
                        {
                            _middleMouseCount = 0;
                            _multiSelectPoints.Clear();
                            lbl_PickPointMode.Text = "Pick 1st Point - Middle Click";
                            showSelectMeasurePointData(_multiSelectPoints);
                        }
                        if (_middleMouseCount >= 2)
                        {
                            _middleMouseCount = 0;
                            lbl_PickPointMode.Text = "Pick 1st Point - Middle Click";
                        }

                        break;
                    case PointPickMode.Multiple:
                        if (_haveClosestPoint)
                        {
                            if (Control.ModifierKeys == Keys.Shift)
                            {
                                if (_middleMouseCount % 2 == 0)
                                {
                                    _MultiSelectLineIndex_Start = -1;
                                    _MultiSelectLineIndex_End = -1;
                                    _MultiSelectPtIndex_Start = -1;
                                    _MultiSelectPtIndex_End = -1;
                                    _tempMultiSelectPoints.Clear();

                                    if (_closetPoint.GetOption(typeof(LineOption)) is LineOption lo) _MultiSelectLineIndex_Start = lo.LineIndex;
                                    if (_closetPoint.GetOption(typeof(LocateIndexOption)) is LocateIndexOption lio) _MultiSelectPtIndex_Start = lio.Index;
                                    if (_MultiSelectLineIndex_Start >= 0) // 選到的是線
                                    {
                                        if (_haveSelectPath) // 指定特定線
                                        {
                                            _tempMultiSelectPoints.Add(_SelectPath.Points[_MultiSelectPtIndex_Start]);

                                        }
                                        else
                                        {
                                            if (_displayObject[_CurrentSelectObjectIndex] is ObjectGroup obj)
                                            {
                                                foreach (var item in obj.Objects)
                                                {
                                                    if (item.Value is Polyline pl)
                                                    {
                                                        if (pl.GetOption(typeof(LineOption)) is LineOption pllo)
                                                        {
                                                            if (pllo.LineIndex == _MultiSelectLineIndex_Start)
                                                            {

                                                                _tempMultiSelectPoints.Add(pl.Points[_MultiSelectPtIndex_Start]);

                                                                break;
                                                            }
                                                        }
                                                    }
                                                }
                                            }
                                        }
                                    }

                                    _middleMouseCount++;
                                }
                                else
                                {
                                    if (_closetPoint.GetOption(typeof(LineOption)) is LineOption lo) _MultiSelectLineIndex_End= lo.LineIndex;
                                    if (_closetPoint.GetOption(typeof(LocateIndexOption)) is LocateIndexOption lio) _MultiSelectPtIndex_End = lio.Index;
                                    int minIndex = _MultiSelectPtIndex_Start <= _MultiSelectPtIndex_End ? _MultiSelectPtIndex_Start : _MultiSelectPtIndex_End;
                                    int maxIndex = _MultiSelectPtIndex_End >= _MultiSelectPtIndex_Start ? _MultiSelectPtIndex_End : _MultiSelectPtIndex_Start;

                                    if (_MultiSelectLineIndex_Start == _MultiSelectLineIndex_End ) 
                                    {
                                        if (_MultiSelectLineIndex_End >= 0) // 選到的是線
                                        {
                                            if (_haveSelectPath) // 指定特定線
                                            {
                                                for (int i = minIndex; i <= maxIndex; i++)
                                                {
                                                    _multiSelectPoints.Add(_SelectPath.Points[i]);
                                                }
                                            }
                                            else
                                            {
                                                if (_displayObject[_CurrentSelectObjectIndex] is ObjectGroup obj)
                                                {
                                                    foreach (var item in obj.Objects)
                                                    {
                                                        if (item.Value is Polyline pl)
                                                        {
                                                            if(pl.GetOption(typeof(LineOption)) is LineOption pllo)
                                                            {
                                                                if(pllo.LineIndex == _MultiSelectLineIndex_End)
                                                                {
                                                                    for (int i = minIndex; i <= maxIndex; i++)
                                                                    {
                                                                        _multiSelectPoints.Add(pl.Points[i]);
                                                                    }
                                                                    break;
                                                                }
                                                            }
                                                        }
                                                    }
                                                }
                                            }
                                            _tempMultiSelectPoints.Clear();
                                        }
                                    }
                                    else
                                    {
                                        _tempMultiSelectPoints.Clear();
                                    }
                                    _middleMouseCount = 0;

                                }
                            }
                            else if (Control.ModifierKeys == Keys.Control)
                            {
                                if (_closetPoint.GetOption(typeof(LineOption)) is LineOption lo) _MultiSelectLineIndex_Start = lo.LineIndex;
                                if (_closetPoint.GetOption(typeof(LocateIndexOption)) is LocateIndexOption lio) _MultiSelectPtIndex_Start = lio.Index;

                                if(_MultiSelectLineIndex_Start >=0)
                                {
                                    if (_haveSelectPath) // 指定特定線
                                    {
                                        if(_SelectPath.Count > _MultiSelectPtIndex_Start)
                                            _multiSelectPoints.Add(_SelectPath.Points[_MultiSelectPtIndex_Start]);
                                    }
                                    else
                                    {
                                        if (_displayObject[_CurrentSelectObjectIndex] is ObjectGroup obj)
                                        {
                                            foreach (var item in obj.Objects)
                                            {
                                                if (item.Value is Polyline pl)
                                                {
                                                    if (pl.GetOption(typeof(LineOption)) is LineOption pllo)
                                                    {
                                                        if (pllo.LineIndex == _MultiSelectLineIndex_Start)
                                                        {
                                                            if (pl.Count > _MultiSelectPtIndex_Start)
                                                                _multiSelectPoints.Add(pl.Points[_MultiSelectPtIndex_Start]);

                                                            break;
                                                        }
                                                    }
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }
                        else
                        {
                            _tempMultiSelectPoints.Clear();
                        }
                        break;
                    case PointPickMode.Draw:
                        addToDrawPath(nearWorld, farWorld);
                        break;
                    default:
                        _middleMouseCount = 0;

                        break;

                }

            }
        }
        private void addToDrawPath(Vector3 near,Vector3 far)
        {
            switch (currentPlane)
            {
                case eCoordPlane.XY:
                    _DrawPath.Add(near.X, near.Y, 0.0);
                    break;

                case eCoordPlane.XZ:
                    _DrawPath.Add(near.X, 0.0, near.Z);
                    break;

                case eCoordPlane.YZ:
                    _DrawPath.Add(0.0, near.Y, near.Z);
                    break;

                case eCoordPlane.YX:
                    _DrawPath.Add(near.X, near.Y, 0.0);
                    break;

                case eCoordPlane.ZX:
                    _DrawPath.Add(near.X, 0.0, near.Z);
                    break;

                case eCoordPlane.ZY:
                    _DrawPath.Add(0.0, near.Y, near.Z);
                    break;
                default:
                    break;
            }


        }

        private void GlControl_MouseMove(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            if (_leftButtonPressed)
            {
                rotate(e.X, e.Y);
            }
            else if (_rightButtonPressed)
            {
                shift(e.X, e.Y);
            }
        }
        private void GlControl_MouseUp(object sender, System.Windows.Forms.MouseEventArgs e)
        {

            if (e.Button == MouseButtons.Left)
            {
                _leftButtonPressed = false;
            }
            else if (e.Button == MouseButtons.Right)
            {
                _rightButtonPressed = false;
            }
        }
        private void GlControl_MouseWheel(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            int step = e.Delta / SystemInformation.MouseWheelScrollDelta;
            _scale *= (float)(1.0 + step * 0.1);

            //_scale += (float)e.Delta / SystemInformation.MouseWheelScrollDelta / 10.0f;
            if (_scale <= 0.1f) _scale = 0.1f;
        }
        private Vector3 ScreenToTrackball(int mousePositionX,int mousePositionY)
        {
            float radius = Math.Min(Width, Height) * 0.5f;
            Vector3 point = new Vector3(
                (mousePositionX - Width * 0.5f) / radius,
                (Height * 0.5f - mousePositionY) / radius,
                0.0f
            );

            float lengthSquared = point.LengthSquared;
            if (lengthSquared <= 1.0f)
                point.Z = (float)Math.Sqrt(1.0f - lengthSquared);
            else
                point.Normalize();

            return point;
        }
        private void GlControl_MouseDown(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                _leftButtonPressed = true;
                _lastRotX = e.X;
                _lastRotY = e.Y;
            }
            else if (e.Button == MouseButtons.Right)
            {
                _rightButtonPressed = true;
                _lastX = e.X;
                _lastY = e.Y;
            }
        }
        #endregion

        #region method


        void multiMatrix()
        {
            Matrix4 localTranslate = Matrix4.CreateTranslation(-1 * _oldAvgPoint);
            localTranslate *= _rotationMatrix;
            localTranslate *= Matrix4.CreateScale(_scale, _scale, _scale);
            localTranslate *= Matrix4.CreateTranslation(_avgPoint);
            localTranslate *= Matrix4.CreateTranslation(_translation.X, _translation.Y, 0.0f);
            localTranslate *= Matrix4.CreateTranslation(-1 * _avgPoint);
            if(_isCheckedMaxMin)
            {
                _oldAvgPoint = _avgPoint;
                _isCheckedMaxMin = false;
            }
            GL.MultMatrix(ref localTranslate);
        }
        void showSelectPointData(bool haveClosetPoint, Point3D closetP)
        {
            treeView1.Nodes.Clear();
            PointV3D ptv3D = closetP as PointV3D;

            if (_haveClosestPoint)
            {
                treeView1.Nodes.Add("Location", "Location");

                treeView1.Nodes["Location"].Nodes.Add($"X : {closetP.X:F2}");
                treeView1.Nodes["Location"].Nodes.Add($"Y : {closetP.Y:F2}");
                treeView1.Nodes["Location"].Nodes.Add($"Z : {closetP.Z:F2}");

                if(ptv3D != null)
                {
                    treeView1.Nodes.Add("Vector Z", "Vector Z");
                    treeView1.Nodes["Vector Z"].Nodes.Add($"X : {ptv3D.Vz.X:F2}");
                    treeView1.Nodes["Vector Z"].Nodes.Add($"Y : {ptv3D.Vz.Y:F2}");
                    treeView1.Nodes["Vector Z"].Nodes.Add($"Z : {ptv3D.Vz.Z:F2}");

                    treeView1.Nodes.Add("Vector Y", "Vector Y");
                    treeView1.Nodes["Vector Y"].Nodes.Add($"X : {ptv3D.Vy.X:F2}");
                    treeView1.Nodes["Vector Y"].Nodes.Add($"Y : {ptv3D.Vy.Y:F2}");
                    treeView1.Nodes["Vector Y"].Nodes.Add($"Z : {ptv3D.Vy.Z:F2}");

                    treeView1.Nodes.Add("Vector X", "Vector X");
                    treeView1.Nodes["Vector X"].Nodes.Add($"X : {ptv3D.Vx.X:F2}");
                    treeView1.Nodes["Vector X"].Nodes.Add($"Y : {ptv3D.Vx.Y:F2}");
                    treeView1.Nodes["Vector X"].Nodes.Add($"Z : {ptv3D.Vx.Z:F2}");
                }

                treeView1.Nodes.Add("Property", "Property");

                List<string> propString = getPointProperty(closetP);
                for (int i = 0; i < propString.Count; i++)
                {
                    treeView1.Nodes["Property"].Nodes.Add(propString[i]);
                }
                LineOption lineOption =  closetP.GetOption(typeof(LineOption)) as LineOption;
                if(lineOption != null)
                {
                    _CurrentSelectLineIndex = lineOption.LineIndex;
                }
                else
                {
                    _CurrentSelectLineIndex = -1;
                }
            }
            else
            {
                _CurrentSelectLineIndex = -1;
            }

            treeView1.ExpandAll();
        }
        List<string> getPointProperty(Point3D p)
        {
            List<string> output = new List<string>();
            for (int i = 0; i < p.Options.Count; i++)
            {
                Type optionType = p.Options[i].GetType();

                PropertyInfo[] properties = optionType.GetProperties();

                for (int j = 0; j < properties.Length; j++)
                {

                    output.Add($"{properties[j].Name} : {properties[j].GetValue(p.Options[i])}");
                }
            }
            return output;
        }

        void showSelectMeasurePointData(PointCloud measureLine)
        {
            treeView1.Nodes.Clear();

            if (measureLine.Count == 2)
            {
                Vector3D v = new Vector3D(measureLine.Points[0], measureLine.Points[1]);
                List<string> startPtProp = getPointProperty(measureLine.Points[0]);
                List<string> endPtProp = getPointProperty(measureLine.Points[1]);

                treeView1.Nodes.Add($"Start Point", "Start Point");
                treeView1.Nodes["Start Point"].BackColor = Settings.Default.Color_StartPoint;

                treeView1.Nodes.Add($"End Point", "End Point");
                treeView1.Nodes["End Point"].BackColor = Settings.Default.Color_EndPoint;

                treeView1.Nodes.Add($"Difference", "Difference");

                treeView1.Nodes.Add($"Length", $"Length : {v.L:F2}");
                treeView1.Nodes["Length"].BackColor = Settings.Default.Color_MeasureLine;

                treeView1.Nodes["Start Point"].Nodes.Add($" X : {measureLine.Points[0].X:F2}");
                treeView1.Nodes["Start Point"].Nodes.Add($" Y : {measureLine.Points[0].Y:F2}");
                treeView1.Nodes["Start Point"].Nodes.Add($" Z : {measureLine.Points[0].Z:F2}");
                treeView1.Nodes["Start Point"].Nodes.Add("Property", "Property");
                for (int i = 0; i < startPtProp.Count; i++)
                {
                    treeView1.Nodes["Start Point"].Nodes["Property"].Nodes.Add(startPtProp[i]);
                }

                treeView1.Nodes["End Point"].Nodes.Add($" X : {measureLine.Points[1].X:F2}");
                treeView1.Nodes["End Point"].Nodes.Add($" Y : {measureLine.Points[1].Y:F2}");
                treeView1.Nodes["End Point"].Nodes.Add($" Z : {measureLine.Points[1].Z:F2}");
                treeView1.Nodes["End Point"].Nodes.Add("Property", "Property");
                for (int i = 0; i < endPtProp.Count; i++)
                {
                    treeView1.Nodes["End Point"].Nodes["Property"].Nodes.Add(endPtProp[i]);
                }

                treeView1.Nodes["Difference"].Nodes.Add($" X : {v.X:F3}");
                treeView1.Nodes["Difference"].Nodes.Add($" Y : {v.Y:F3}");
                treeView1.Nodes["Difference"].Nodes.Add($" Z : {v.Z:F3}");

            }
            treeView1.ExpandAll();
        }

        void shift(int mouseX, int mouseY)
        {
            _translation.X += (mouseX - _lastX);
            _translation.Y -= (mouseY - _lastY);

            _glControl.Invalidate();
            _lastX = mouseX;
            _lastY = mouseY;
        }
        float _angle = 0.0f;
        Vector3 _rotateAxis;
        void rotate(int mouseX, int mouseY)
        {
            if (LockRotate) return;

            Vector3 trackballStart = ScreenToTrackball(_lastRotX, _lastRotY);
            Vector3 trackballCurrent = ScreenToTrackball(mouseX, mouseY);
            _rotateAxis = Vector3.Cross(trackballStart, trackballCurrent);
            _angle = (float)Math.Acos(Vector3.Dot(trackballStart, trackballCurrent));
            _angle *= Settings.Default.Sensitivity;
            if (float.IsNaN(_angle)) return;
            if (float.IsNaN(_rotateAxis.Length)) return;
            if (_rotateAxis.Length <= float.Epsilon) return;
            if (_rotateAxis.Length == 0) return;

            _lastRotX = mouseX;
            _lastRotY = mouseY;



            Matrix4 deltaRotation = Matrix4.CreateFromAxisAngle(_rotateAxis, _angle);

            _rotationMatrix *= deltaRotation;

            _glControl.Invalidate();

        }
        void checkMaxMinPoint(Point3D p)
        {
            _oldAvgPoint = _avgPoint;
            if (p.X >= _maxPoint.X) _maxPoint.X = (float)p.X;
            if (p.Y >= _maxPoint.Y) _maxPoint.Y = (float)p.Y;
            if (p.Z >= _maxPoint.Z) _maxPoint.Z = (float)p.Z;

            if (p.X <= _minPoint.X) _minPoint.X = (float)p.X;
            if (p.Y <= _minPoint.Y) _minPoint.Y = (float)p.Y;
            if (p.Z <= _minPoint.Z) _minPoint.Z = (float)p.Z;
            _isCheckedMaxMin = true;
        }
        bool closestPoint(Vector3 nearWorld, Vector3 farWorld, out Point3D closestPoint)
        {
            Vector3 rayDir, rayOrig;
            closestPoint = new Point3D(0, 0, 0);
            rayOrig = nearWorld;
            rayDir = Vector3.Subtract(farWorld, nearWorld).Normalized();
            float closestDistance = float.MaxValue;
            bool hasClosetPoint = false;
            if (_displayObject.Count == 0) return false;
            if (_CurrentSelectObjectIndex == -1) return false;
            if (_displayObject.ContainsKey(_CurrentSelectObjectIndex) == false) return false;
            if (_displayOption.ContainsKey(_CurrentSelectObjectIndex) == false) return false;

            DisplayObjectType displayObjectType = _displayOption[_CurrentSelectObjectIndex].DisplayType;
            if (displayObjectType == DisplayObjectType.None) return false;
            //DisplayObjectOption objectOption = _displayOption[_CurrentSelectObjectIndex];
            //if (objectOption.IsDisplay == false) return false;

            //Type objectType = _displayObject[_CurrentSelectObjectIndex].GetType();

            if (_haveSelectPath == false)
            {
                if (_displayObject[_CurrentSelectObjectIndex] is Point3D pt)
                {
                    //var obj = _displayObject[_CurrentSelectObjectIndex] as Point3D;
                    float result = calculatePointMinDistance(rayOrig, rayDir, pt);
                    closestDistance = result;
                    closestPoint = pt;
                    hasClosetPoint = closestDistance <= _closetDisLimit;
                }
                else if (_displayObject[_CurrentSelectObjectIndex] is ObjectGroup obj)
                {
                    foreach (var item in obj.Objects)
                    {
                        var subObj = item.Value as PointCloud;
                        Tuple<float, Point3D> result = calculateNearestPoint(rayOrig, rayDir, closestDistance, subObj.Points);
                        if (result.Item1 < closestDistance)
                        {
                            closestDistance = result.Item1;
                            closestPoint = result.Item2;
                            hasClosetPoint = closestDistance <= _closetDisLimit;
                        }
                    }
                }
                else if (_displayObject[_CurrentSelectObjectIndex] is PointCloud cloud)
                {
                    //var obj = _displayObject[_CurrentSelectObjectIndex] as PointCloud;
                    Tuple<float, Point3D> result = calculateNearestPoint(rayOrig, rayDir, closestDistance, cloud.Points);
                    closestDistance = result.Item1;
                    closestPoint = result.Item2;
                    hasClosetPoint = closestDistance <= _closetDisLimit;

                }
            }
            else
            {
                Tuple<float, Point3D> result = calculateNearestPoint(rayOrig, rayDir, closestDistance, _SelectPath.Points);
                closestDistance = result.Item1;
                closestPoint = result.Item2;
                hasClosetPoint = closestDistance <= _closetDisLimit;
            }
            return hasClosetPoint;
        }
        Tuple<float, Point3D> calculateNearestPoint(Vector3 rayOrig, Vector3 rayDir, float closestDistance, List<Point3D> points)
        {
            Point3D closestPoint = new Point3D();
            foreach (Point3D point in points)
            {
                float result = calculatePointMinDistance(rayOrig, rayDir, point);
                if (result <= closestDistance)
                {
                    closestDistance = result;
                    closestPoint = point;
                }
            }
            return new Tuple<float, Point3D>(closestDistance, closestPoint);
        }
        float calculatePointMinDistance(Vector3 rayOrig, Vector3 rayDir, Point3D point)
        {
            Vector3 vecPoint = new Vector3((float)point.X, (float)point.Y, (float)point.Z);
            float distance = Vector3.Distance(rayOrig, vecPoint);
            Vector3 displacement = vecPoint - rayOrig;
            float projection = Vector3.Dot(displacement, rayDir);
            float verticalDis = (float)Math.Sqrt(Math.Pow(distance, 2) - Math.Pow(projection, 2));
            return verticalDis;
        }

        Vector3 unProject(Vector3 winCoord, int viewportX, int viewportY, int viewportWidth, int viewportHeight)
        {
            Matrix4 modelview;
            Matrix4 projection;
            GL.GetFloat(GetPName.ModelviewMatrix, out modelview);
            GL.GetFloat(GetPName.ProjectionMatrix, out projection);
            Vector4 vec;

            vec.X = 2.0f * (winCoord.X - viewportX) / viewportWidth - 1;
            vec.Y = 2.0f * (winCoord.Y - viewportY) / viewportHeight - 1;
            vec.Z = 2.0f * winCoord.Z - 1;
            vec.W = 1.0f;
            Matrix4 inverted = Matrix4.Invert(modelview * projection);
            Vector4 vec_trans = Vector4.Transform(vec, inverted);
            if (vec_trans.W > float.Epsilon || vec_trans.W < float.Epsilon)
            {
                vec_trans.X /= vec_trans.W;
                vec_trans.Y /= vec_trans.W;
                vec_trans.Z /= vec_trans.W;
            }

            return new Vector3(vec_trans.X, vec_trans.Y, vec_trans.Z);
        }
        #endregion
        #region build object
        void drawCone(Vector3 baseCenter, float height, Vector3 normalDir, float baseRadius, Color drawColor)
        {
            GL.PushMatrix();
            Matrix4 rotate = getRotationMatrix(normalDir);
            rotate *= Matrix4.CreateTranslation(baseCenter);
            GL.MultMatrix(ref rotate);
            GL.Color3(drawColor);
            GL.Begin(PrimitiveType.TriangleFan);
            GL.Vertex3(0, 0, height);
            int numSegments = 45;
            for (int i = 0; i <= numSegments; i++)
            {
                float angle = i * 2.0f * (float)Math.PI / numSegments;
                GL.Vertex3(baseRadius * (float)Math.Cos(angle), baseRadius * (float)Math.Sin(angle), 0.0f);
            }
            GL.End();
            GL.PopMatrix();
        }
        void drawCone(Point3D baseCenter, float height, Vector3D normalDir, float baseRadius, Color drawColor)
        {
            Vector3 center = new Vector3((float)baseCenter.X, (float)baseCenter.Y, (float)baseCenter.Z);
            Vector3 normal = new Vector3((float)normalDir.X, (float)normalDir.Y, (float)normalDir.Z);
            drawCone(center, height, normal, baseRadius, drawColor);
        }

        Matrix4 getRotationMatrix(Vector3 normal)
        {
            normal.Normalize();
            Vector3 axis = Vector3.UnitZ;
            float angle;
            if (normal == Vector3.UnitZ)
            {
                angle = 0f;
            }
            else
            {
                axis = Vector3.Cross(normal, new Vector3(0, 0, 1));
                angle = -1 * (float)Math.Acos(Vector3.Dot(normal, new Vector3(0, 0, 1)));
            }
            Matrix3 mat3 = Matrix3.CreateFromAxisAngle(axis, angle);
            Matrix4 mat4 = new Matrix4(mat3.M11, mat3.M12, mat3.M13, 0,
                           mat3.M21, mat3.M22, mat3.M23, 0,
                           mat3.M31, mat3.M32, mat3.M33, 0,
                           0, 0, 0, 1);
            return mat4;
        }
        void drawSelectedPoint()
        {
            GL.PointSize(Settings.Default.Size_SelectPoint);
            GL.Begin(PrimitiveType.Points);
            GL.Color3(Settings.Default.Color_SelectPoint);
            GL.Vertex3(_closetPoint.PArray);
            GL.End();
            drawVector(_closetPoint);
        }
        void drawDrawPath()
        {
            int count = _DrawPath.Count;
            if (count == 0) return;
            for (int i = 0; i < _DrawPath.Count; i++)
            {
                Point3D pt = _DrawPath.Points[i];
                switch (currentPlane)
                {
                    case eCoordPlane.XY:
                        pt.Z =  _maxPoint.Z == float.MinValue ? 0f : _maxPoint.Z;
                        break;

                    case eCoordPlane.XZ:
                        pt.Y = _minPoint.Y == float.MaxValue ? 0f : _minPoint.Y;
                        break;

                    case eCoordPlane.YZ:
                        pt.X = _maxPoint.X == float.MinValue ? 0f : _maxPoint.X;
                        break;

                    case eCoordPlane.YX:
                        pt.Z =_minPoint.Z == float.MaxValue ? 0f : _minPoint.Z;
                        break;

                    case eCoordPlane.ZX:
                        pt.Y =_maxPoint.Y == float.MinValue ? 0f : _maxPoint.Y;
                        break;

                    case eCoordPlane.ZY:
                        pt.X = _minPoint.X == float.MaxValue ? 0f : _minPoint.X;
                        break;
                    default:
                        break;
                }

            }

            if (count >= 2)
                drawPolyline(_DrawPath, Settings.Default.Size_DrawLine, Settings.Default.Color_DrawLine, false);
            drawPointCloud(_DrawPath, Settings.Default.Size_DrawLine * 3, Settings.Default.Color_DrawLine, false);

        }

        void drawSelectPath()
        {
            int count = _SelectPath.Count;
            if (count < 2) return;
            Point3D pLast = _SelectPath.Points[count - 1];
            Point3D pBeforeLast = _SelectPath.Points[count - 2];
            Vector3 pos = new Vector3()
            { 
                X = (float)pLast.X,
                Y = (float)pLast.Y,
                Z = (float)pLast.Z,
            };

            Vector3 dir = new Vector3()
            {
                X = (float)(pLast.X - pBeforeLast.X),
                Y = (float)(pLast.Y - pBeforeLast.Y),
                Z = (float)(pLast.Z - pBeforeLast.Z),
            };

            drawPolyline(_SelectPath, Settings.Default.Size_SelectPath, Settings.Default.Color_SelectPath, false);
            drawPointCloud(_SelectPath, Settings.Default.Size_SelectPath*2, Settings.Default.Color_SelectPath, false);
            drawVector(_SelectPath, Settings.Default.Size_SelectPath, Color.Blue, false, DisplayObjectType.Vector_z);
            drawCone(pos, 7, dir, 2, Settings.Default.Color_SelectPath);

        }
        void drawMeasureLine()
        {
            GL.PointSize(Settings.Default.Size_SelectPoint);
            GL.Begin(PrimitiveType.Points);
            for (int i = 0; i < _multiSelectPoints.Count; i++)
            {
                Point3D p = _multiSelectPoints.Points[i];

                if (i == 0) GL.Color4(Settings.Default.Color_StartPoint);
                else GL.Color4(Settings.Default.Color_EndPoint);
                GL.Vertex3(p.PArray);
            }
            GL.End();


            if (_multiSelectPoints.Count >= 2)
            {
                GL.LineWidth(Settings.Default.Size_MeasureLine);
                GL.Begin(PrimitiveType.Lines);
                GL.Color4(Settings.Default.Color_MeasureLine);
                for (int i = 0; i < _multiSelectPoints.Count; i++)
                {
                    Point3D p = _multiSelectPoints.Points[i];
                    GL.Vertex3(p.PArray);
                }
                GL.End();
            }

        }
        void drawMultipleSelect()
        {
            GL.PointSize(Settings.Default.Size_SelectPoint);
            GL.Begin(PrimitiveType.Points);
            GL.Color4(Settings.Default.Color_StartPoint);
            for (int i = 0; i < _multiSelectPoints.Count; i++)
            {
                Point3D p = _multiSelectPoints.Points[i];

                GL.Vertex3(p.PArray);
            }
            GL.End();
        }
        void drawTempMultipleSelect()
        {
            GL.PointSize(Settings.Default.Size_SelectPoint);
            GL.Begin(PrimitiveType.Points);
            GL.Color4(Settings.Default.Color_EndPoint);
            for (int i = 0; i < _tempMultiSelectPoints.Count; i++)
            {
                Point3D p = _tempMultiSelectPoints.Points[i];

                GL.Vertex3(p.PArray);
            }
            GL.End();
        }

        void drawSelectRangeFrame()
        {
            if (_CurrentSelectObjectIndex <= 1) return;
            if (_displayObject.Count == 0) return;
            if (_displayObject.ContainsKey(_CurrentSelectObjectIndex) == false) return;

            Point3D min = new Point3D();
            Point3D max = new Point3D();

            Type objectType = _displayObject[_CurrentSelectObjectIndex].GetType();
            if (objectType == typeof(PointCloud))
            {
                PointCloud selectCandidate = _displayObject[_CurrentSelectObjectIndex] as PointCloud;
                min = selectCandidate.Min;
                max = selectCandidate.Max;
            }
            else if (objectType == typeof(Polyline))
            {
                Polyline selectCandidate = _displayObject[_CurrentSelectObjectIndex] as Polyline;
                min = selectCandidate.Min;
                max = selectCandidate.Max;
            }
            else if (objectType == typeof(ObjectGroup))
            {
                ObjectGroup selectCandidate = _displayObject[_CurrentSelectObjectIndex] as ObjectGroup;
                min = selectCandidate.Min;
                max = selectCandidate.Max;
            }


            drawFrame(new Vector3((float)min.X, (float)min.Y, (float)min.Z),
                new Vector3((float)max.X, (float)max.Y, (float)max.Z),
                Settings.Default.Size_SelectRange,
                Settings.Default.Color_SelectRange,
                true);
        }
        void drawCircle(Vector3 center, Vector3 normalDir, float radius, Color drawColor)
        {
            GL.PushMatrix();
            Matrix4 rotate = getRotationMatrix(normalDir);
            rotate *= Matrix4.CreateTranslation(center);
            GL.MultMatrix(ref rotate);
            GL.Begin(PrimitiveType.TriangleFan);
            GL.Color3(drawColor);
            for (int i = 0; i <= 32; i++)
            {
                double angle = 2 * Math.PI * i / 32;
                GL.Vertex3(radius * Math.Cos(angle), radius * Math.Sin(angle), 0);
            }
            GL.End();
            GL.PopMatrix();
        }
        void drawCylinder(Vector3 center, Vector3 normalDir, float radius, float length, Color drawColor)
        {
            GL.PushMatrix();
            Matrix4 rotate = getRotationMatrix(normalDir);
            rotate *= Matrix4.CreateTranslation(center);
            GL.MultMatrix(ref rotate);
            // 畫圓柱
            GL.Begin(PrimitiveType.QuadStrip);
            GL.Color3(drawColor);
            for (int i = 0; i <= 360; i++)
            {
                double angle = i * Math.PI / 180;
                GL.Vertex3(radius * Math.Cos(angle), radius * Math.Sin(angle), 0);
                GL.Vertex3(radius * Math.Cos(angle), radius * Math.Sin(angle), length);
            }
            GL.End();
            // 畫下圓形
            GL.Begin(PrimitiveType.TriangleFan);
            GL.Color3(drawColor);
            for (int i = 0; i <= 32; i++)
            {
                double angle = 2 * Math.PI * i / 32;
                GL.Vertex3(radius * Math.Cos(angle), radius * Math.Sin(angle), 0);
            }
            GL.End();
            // 畫上圓形
            GL.Begin(PrimitiveType.TriangleFan);
            GL.Color3(drawColor);
            for (int i = 0; i <= 32; i++)
            {
                double angle = 2 * Math.PI * i / 32;
                GL.Vertex3(radius * Math.Cos(angle), radius * Math.Sin(angle), length);
            }
            GL.End();
            GL.PopMatrix();
        }
        void drawArrow(Point3D pStart,Point3D pEnd, float cylinderHeight, float cylynderRadius, float topCapHeight, float topCapRadius, Vector3 normalDir, Color drawColor)
        {
            GL.PushMatrix();
            Vector3 dir = new Vector3()
            {
                X = (float)(pEnd.X - pStart.X),
                Y = (float)(pEnd.Y - pStart.Y),
                Z = (float)(pEnd.Z - pStart.Z),
            };
            Matrix4 rotate = getRotationMatrix(dir);
            rotate *= Matrix4.CreateTranslation(dir);

            GL.MultMatrix(ref rotate);
            // 畫圓柱
            GL.Begin(PrimitiveType.QuadStrip);
            GL.Color3(drawColor);
            for (int i = 0; i <= 360; i++)
            {
                double angle = i * Math.PI / 180;
                GL.Vertex3(cylynderRadius * Math.Cos(angle), cylynderRadius * Math.Sin(angle), 0);
                GL.Vertex3(cylynderRadius * Math.Cos(angle), cylynderRadius * Math.Sin(angle), cylinderHeight);
            }
            GL.End();
            // 畫下圓形
            GL.Begin(PrimitiveType.TriangleFan);
            GL.Color3(drawColor);
            for (int i = 0; i <= 32; i++)
            {
                double angle = 2 * Math.PI * i / 32;
                GL.Vertex3(cylynderRadius * Math.Cos(angle), cylynderRadius * Math.Sin(angle), 0);
            }
            GL.End();

            GL.Begin(PrimitiveType.TriangleFan);
            GL.Vertex3(0, 0, cylinderHeight + topCapHeight);
            int numSegments = 45;
            for (int i = 0; i <= numSegments; i++)
            {
                float angle = i * 2.0f * (float)Math.PI / numSegments;
                GL.Vertex3(topCapRadius * (float)Math.Cos(angle), topCapRadius * (float)Math.Sin(angle), cylinderHeight);
            }
            GL.End();
            GL.PopMatrix();

        }

        void drawArrow(Vector3 baseCenter, float cylinderHeight, float cylynderRadius, float topCapHeight, float topCapRadius, Vector3 normalDir, Color drawColor)
        {
            GL.PushMatrix();
            Matrix4 rotate = getRotationMatrix(normalDir);
            rotate *= Matrix4.CreateTranslation(baseCenter);
            GL.MultMatrix(ref rotate);
            // 畫圓柱
            GL.Begin(PrimitiveType.QuadStrip);
            GL.Color3(drawColor);
            for (int i = 0; i <= 360; i++)
            {
                double angle = i * Math.PI / 180;
                GL.Vertex3(cylynderRadius * Math.Cos(angle), cylynderRadius * Math.Sin(angle), 0);
                GL.Vertex3(cylynderRadius * Math.Cos(angle), cylynderRadius * Math.Sin(angle), cylinderHeight);
            }
            GL.End();
            // 畫下圓形
            GL.Begin(PrimitiveType.TriangleFan);
            GL.Color3(drawColor);
            for (int i = 0; i <= 32; i++)
            {
                double angle = 2 * Math.PI * i / 32;
                GL.Vertex3(cylynderRadius * Math.Cos(angle), cylynderRadius * Math.Sin(angle), 0);
            }
            GL.End();

            GL.Begin(PrimitiveType.TriangleFan);
            GL.Vertex3(0, 0, cylinderHeight + topCapHeight);
            int numSegments = 45;
            for (int i = 0; i <= numSegments; i++)
            {
                float angle = i * 2.0f * (float)Math.PI / numSegments;
                GL.Vertex3(topCapRadius * (float)Math.Cos(angle), topCapRadius * (float)Math.Sin(angle), cylinderHeight);
            }
            GL.End();
            GL.PopMatrix();

        }
        void drawSphere(Vector3 center, float radius, Color drawColor)
        {
            // Draw the sphere
            GL.PushMatrix();
            Matrix4 translate = Matrix4.CreateTranslation(center);
            GL.MultMatrix(ref translate);
            GL.Color3(drawColor);
            float theta, phi;
            float thetaNext, phiNext;
            Vector3 normal;
            int stacks = 30;
            int slices = 30;
            Vector3 pos = Vector3.Zero;

            for (int i = 0; i < stacks; i++)
            {
                phi = (float)(Math.PI / 2 - i * Math.PI / stacks);
                phiNext = (float)(Math.PI / 2 - (i + 1) * Math.PI / stacks);

                GL.Begin(PrimitiveType.TriangleStrip);

                for (int j = 0; j <= slices; j++)
                {
                    theta = (float)(j * 2 * Math.PI / slices);
                    thetaNext = (float)((j + 1) * 2 * Math.PI / slices);

                    normal.X = (float)(Math.Cos(phi) * Math.Cos(theta));
                    normal.Y = (float)Math.Sin(phi);
                    normal.Z = (float)(Math.Cos(phi) * Math.Sin(theta));
                    pos = normal * radius;

                    GL.Normal3(normal);
                    GL.Vertex3(pos);

                    normal.X = (float)(Math.Cos(phiNext) * Math.Cos(theta));
                    normal.Y = (float)Math.Sin(phiNext);
                    normal.Z = (float)(Math.Cos(phiNext) * Math.Sin(theta));
                    pos = normal * radius;

                    GL.Normal3(normal);
                    GL.Vertex3(pos);
                }

                GL.End();
            }
            GL.PopMatrix();
        }
        void drawFrame(Vector3 minPoint, Vector3 maxPoint, float lineWidth, Color drawColor, bool useDashedLine)
        {
            /*                                8                           7
             *                                  ________________
             *                                /                          /|
             *                        4    /                           / | 3
             *                        5   ---------------- 6  /
             *                             |_______________|
             *                             1                        2
             */
            Vector3 p1 = new Vector3(minPoint.X, minPoint.Y, minPoint.Z);
            Vector3 p2 = new Vector3(maxPoint.X, minPoint.Y, minPoint.Z);
            Vector3 p3 = new Vector3(maxPoint.X, maxPoint.Y, minPoint.Z);
            Vector3 p4 = new Vector3(minPoint.X, maxPoint.Y, minPoint.Z);
            Vector3 p5 = new Vector3(minPoint.X, minPoint.Y, maxPoint.Z);
            Vector3 p6 = new Vector3(maxPoint.X, minPoint.Y, maxPoint.Z);
            Vector3 p7 = new Vector3(maxPoint.X, maxPoint.Y, maxPoint.Z);
            Vector3 p8 = new Vector3(minPoint.X, maxPoint.Y, maxPoint.Z);

            if (lineWidth <= 0f) return;
            if (useDashedLine)
            {
                #region dashed line
                // down surface
                drawDashedLine(p1, p2, lineWidth, drawColor, 5f);
                drawDashedLine(p2, p3, lineWidth, drawColor, 5f);
                drawDashedLine(p3, p4, lineWidth, drawColor, 5f);
                drawDashedLine(p4, p1, lineWidth, drawColor, 5f);

                //up surface
                drawDashedLine(p5, p6, lineWidth, drawColor, 5f);
                drawDashedLine(p6, p7, lineWidth, drawColor, 5f);
                drawDashedLine(p7, p8, lineWidth, drawColor, 5f);
                drawDashedLine(p8, p5, lineWidth, drawColor, 5f);

                // side wall
                drawDashedLine(p1, p5, lineWidth, drawColor, 5f);
                drawDashedLine(p2, p6, lineWidth, drawColor, 5f);
                drawDashedLine(p3, p7, lineWidth, drawColor, 5f);
                drawDashedLine(p4, p8, lineWidth, drawColor, 5f);
                #endregion
            }
            else
            {
                #region solid line
                // down surface
                GL.Begin(PrimitiveType.LineLoop);
                GL.Color3(drawColor);
                GL.LineWidth(lineWidth);
                GL.Vertex3(minPoint.X, minPoint.Y, minPoint.Z);
                GL.Vertex3(maxPoint.X, minPoint.Y, minPoint.Z);
                GL.Vertex3(maxPoint.X, maxPoint.Y, minPoint.Z);
                GL.Vertex3(minPoint.X, maxPoint.Y, minPoint.Z);
                GL.End();

                //up surface
                GL.Begin(PrimitiveType.LineLoop);
                GL.Color3(drawColor);
                GL.LineWidth(lineWidth);
                GL.Vertex3(minPoint.X, minPoint.Y, maxPoint.Z);
                GL.Vertex3(maxPoint.X, minPoint.Y, maxPoint.Z);
                GL.Vertex3(maxPoint.X, maxPoint.Y, maxPoint.Z);
                GL.Vertex3(minPoint.X, maxPoint.Y, maxPoint.Z);
                GL.End();

                // side wall
                GL.Begin(PrimitiveType.Lines);
                GL.Color3(drawColor);
                GL.LineWidth(lineWidth);
                GL.Vertex3(minPoint.X, minPoint.Y, minPoint.Z);
                GL.Vertex3(minPoint.X, minPoint.Y, maxPoint.Z);

                GL.Vertex3(maxPoint.X, minPoint.Y, minPoint.Z);
                GL.Vertex3(maxPoint.X, minPoint.Y, maxPoint.Z);

                GL.Vertex3(minPoint.X, maxPoint.Y, minPoint.Z);
                GL.Vertex3(minPoint.X, maxPoint.Y, maxPoint.Z);

                GL.Vertex3(maxPoint.X, maxPoint.Y, minPoint.Z);
                GL.Vertex3(maxPoint.X, maxPoint.Y, maxPoint.Z);
                GL.End();
                #endregion
            }
        }
        void drawDashedLine(Point3D start, Point3D end, float lineWidth, Color drawColor, float dashLength)
        {
            Vector3 startV = new Vector3((float)start.X, (float)start.Y, (float)start.Z);
            Vector3 endV = new Vector3((float)end.X, (float)end.Y, (float)end.Z);
            drawDashedLine(startV, endV, lineWidth, drawColor, dashLength);

        }

        void drawDashedLine(Vector3 start, Vector3 end, float lineWidth, Color drawColor, float dashLength)
        {
            float distance = Vector3.Distance(start, end);
            float dashCount = distance / dashLength;

            GL.LineWidth(lineWidth);
            GL.Begin(PrimitiveType.Lines);
            GL.Color4(drawColor);

            if (dashCount >= 3)
            {
                for (int i = 0; i < dashCount; i++)
                {
                    if (i % 2 == 0)
                    {
                        GL.Vertex3(start + (end - start) * (i / dashCount));
                    }
                }
            }
            else
            {
                GL.Vertex3(start);
                GL.Vertex3(end);
            }
            GL.End();
        }
        private void BuildAxis()
        {
            GL.NewList(_headList, ListMode.Compile);
            float cylinderHeight = 20f;
            float capHeight = 10f;
            float cylinderRadius = 0.5f;
            float capRadius = 2f;
            drawCylinder(Vector3.Zero, Vector3.UnitX, cylinderRadius, cylinderHeight, Color.Red);
            drawCylinder(Vector3.Zero, Vector3.UnitY, cylinderRadius, cylinderHeight, Color.Green);
            drawCylinder(Vector3.Zero, Vector3.UnitZ, cylinderRadius, cylinderHeight, Color.Blue);
            drawSphere(Vector3.Zero, 3.0f, Color.White);
            drawCone(new Vector3(cylinderHeight, 0, 0), capHeight, Vector3.UnitX, capRadius, Color.Red);
            drawCone(new Vector3(0, cylinderHeight, 0), capHeight, Vector3.UnitY, capRadius, Color.Green);
            drawCone(new Vector3(0, 0, cylinderHeight), capHeight, Vector3.UnitZ, capRadius, Color.Blue);
            GL.EndList();
        }
        void drawPoint(Point3D pt, float drawSize, Color drawColor, bool checkMaxMin)
        {
            GL.PointSize(drawSize);
            GL.Color4(drawColor);
            GL.Begin(PrimitiveType.Points);

            if (checkMaxMin)
            {
                checkMaxMinPoint(pt);
            }
            GL.Vertex3(pt.PArray);

            GL.End();
        }
        public Bitmap CaptureScreenShot()
        {
            Bitmap bmp = new Bitmap(pnl_GLControl.Width, pnl_GLControl.Height);
            BitmapData bitmapData = bmp.LockBits(
                new Rectangle(0, 0, pnl_GLControl.Width, pnl_GLControl.Height),
                ImageLockMode.WriteOnly,
                System.Drawing.Imaging.PixelFormat.Format24bppRgb);
            GL.ReadPixels(0, 0, pnl_GLControl.Width, pnl_GLControl.Height,
                OpenTK.Graphics.OpenGL.PixelFormat.Bgr,
                PixelType.UnsignedByte,
                bitmapData.Scan0);
            bmp.UnlockBits(bitmapData);
            bmp.RotateFlip(RotateFlipType.RotateNoneFlipY);
            return bmp;
        }
        public void CaptureScreenShotIntoClipboard()
        {
            Bitmap bmp = CaptureScreenShot();
            Clipboard.SetImage(bmp);
        }
        public void SaveScreenShot(string filePath)
        {
            Bitmap bmp = CaptureScreenShot();
            bmp.Save(filePath, ImageFormat.Png);
            bmp.Dispose();
        }
        public void BuildPoint(Point3D pt, int id, bool checkMaxMin, bool isUpdateObject)
        {
            if (id > _maxDisplayList) return;
            if (_displayOption.ContainsKey(id) == false)
            {
                throw new Exception($"Build point fail. Display option doesn't contain ID {id}.");
            }
            DisplayObjectOption option = _displayOption[id];
            if (option.DisplayType != DisplayObjectType.Point) return;

            if (_displayObject.ContainsKey(option.ID))
            {
                if (isUpdateObject)
                {
                    _displayObject.Remove(option.ID);
                    _displayObject.Add(option.ID, pt);
                }
            }
            else
            {
                _displayObject.Add(option.ID, pt);
            }
            GL.NewList(id, ListMode.Compile);
            drawPoint(pt, option.DrawSize, option.DrawColor, checkMaxMin);
            GL.EndList();
            //updateDataGridView();
        }
        void drawPointCloud(PointCloud cloud, float drawSize, Color drawColor, bool checkMaxMin)
        {
            GL.PointSize(drawSize);
            GL.Begin(PrimitiveType.Points);
            foreach (var item in cloud.Points)
            {
                if (checkMaxMin)
                {
                    checkMaxMinPoint(item);
                }
                DisplayOption option = item.GetOption(typeof(DisplayOption)) as DisplayOption;
                if (option == null) GL.Color4(drawColor);
                else GL.Color3(option.Color);

                GL.Vertex3(item.PArray);
            }
            GL.End();
        }
        public void BuildPointCloud(Tuple<double[],double[],double[]> cloud, int id, bool checkMaxMin, bool isUpdateObject)
        {
            PointCloud p = new PointCloud(cloud);
            BuildPointCloud(p, id, checkMaxMin, isUpdateObject);
        }

        public void BuildPointCloud(PointCloud cloud, int id, bool checkMaxMin, bool isUpdateObject)
        {
            if (id > _maxDisplayList) return;
            if (_displayOption.ContainsKey(id) == false)
            {
                throw new Exception($"Build point cloud fail. Display option doesn't contain ID {id}.");
            }

            DisplayObjectOption option = _displayOption[id];
            if (option.DisplayType != DisplayObjectType.PointCloud) return;
            if (_displayObject.ContainsKey(option.ID))
            {
                if (isUpdateObject)
                {
                    _displayObject.Remove(option.ID);
                    _displayObject.Add(option.ID, cloud);
                }
            }
            else
            {
                _displayObject.Add(option.ID, cloud);
            }
            GL.NewList(id, ListMode.Compile);
            drawPointCloud(cloud, option.DrawSize, option.DrawColor, checkMaxMin);
            GL.EndList();
            //updateDataGridView();

        }
        public void BuildPointCloud(ObjectGroup group, int id, bool checkMaxMin, bool isUpdateObject)
        {
            if (id > _maxDisplayList) return;
            if (_displayOption.ContainsKey(id) == false)
            {
                throw new Exception($"Build point cloud fail. Display option doesn't contain ID {id}.");
            }

            DisplayObjectOption option = _displayOption[id];
            if (option.DisplayType != DisplayObjectType.PointCloud) return;

            if (_displayObject.ContainsKey(option.ID))
            {
                if (isUpdateObject)
                {
                    _displayObject.Remove(option.ID);
                    _displayObject.Add(option.ID, group);
                }
            }
            else
            {
                _displayObject.Add(option.ID, group);
            }
            GL.NewList(id, ListMode.Compile);
            foreach (var item in group.Objects)
            {
                Polyline line = item.Value as Polyline;
                if (line != null) drawPointCloud(line, option.DrawSize, option.DrawColor, checkMaxMin);
            }
            GL.EndList();
            //updateDataGridView();
        }
        void drawPolyline(Polyline polyLine, float drawSize, Color drawColor, bool checkMaxMin)
        {
            GL.LineWidth(drawSize);
            GL.Color4(drawColor);
            GL.Begin(PrimitiveType.LineStrip);

            GL.Vertex3(polyLine.Points[0].PArray);
            if (checkMaxMin)  checkMaxMinPoint(polyLine.Points[0]);
            GL.Vertex3(polyLine.Points[1].PArray);
            if (checkMaxMin) checkMaxMinPoint(polyLine.Points[1]);
            for (int i = 2; i < polyLine.Count; i++)
            {

                GL.Vertex3(polyLine.Points[i].PArray);
                if (checkMaxMin) checkMaxMinPoint(polyLine.Points[i]);
                
            }

            GL.End();
        }
        public void BuildPath(Polyline polyLine, int id, bool checkMaxMin, bool isUpdateObject)
        {
            if (id > _maxDisplayList) return;
            if (_displayOption.ContainsKey(id) == false)
            {
                throw new Exception($"Build path fail. Display option doesn't contain ID {id}.");
            }
            DisplayObjectOption option = _displayOption[id];
            if (option.DisplayType != DisplayObjectType.Path) return;
            if (polyLine.Count < 2) return;

            if (_displayObject.ContainsKey(option.ID))
            {
                if (isUpdateObject)
                {
                    _displayObject.Remove(option.ID);
                    _displayObject.Add(option.ID, polyLine);
                }
            }
            else
            {
                _displayObject.Add(option.ID, polyLine);
            }
            GL.NewList(id, ListMode.Compile);
            drawPolyline(polyLine, option.DrawSize, option.DrawColor, checkMaxMin);
            GL.EndList();
            //updateDataGridView();
        }
        public void BuildPath(ObjectGroup polyLines, int id, bool checkMaxMin, bool isUpdateObject)
        {
            if (id > _maxDisplayList) return;
            if (polyLines.DataCount == 0) return;
            if (_displayOption.ContainsKey(id) == false)
            {
                throw new Exception($"Build path fail. Display option doesn't contain ID {id}.");
            }
            DisplayObjectOption option = _displayOption[id];
            if (option.DisplayType != DisplayObjectType.Path) return;

            if (_displayObject.ContainsKey(option.ID))
            {
                if (isUpdateObject)
                {
                    _displayObject.Remove(option.ID);
                    _displayObject.Add(option.ID, polyLines);
                }
            }
            else
            {
                _displayObject.Add(option.ID, polyLines);
            }
            GL.NewList(id, ListMode.Compile);
            foreach (var item in polyLines.Objects)
            {
                Polyline line = item.Value as Polyline;
                if (line != null)
                {
                    drawPolyline(line, option.DrawSize, option.DrawColor, checkMaxMin);
                }
            }
            GL.EndList();
            //updateDataGridView();
        }
        public void BuildPath(ObjectGroup polyLines, int id, bool checkMaxMin, bool isUpdateObject,bool ConnectEachOther)
        {
            if (id > _maxDisplayList) return;
            if (polyLines.DataCount == 0) return;
            if (_displayOption.ContainsKey(id) == false)
            {
                throw new Exception($"Build path fail. Display option doesn't contain ID {id}.");
            }
            DisplayObjectOption option = _displayOption[id];
            if (option.DisplayType != DisplayObjectType.Path) return;

            if (_displayObject.ContainsKey(option.ID))
            {
                if (isUpdateObject)
                {
                    _displayObject.Remove(option.ID);
                    _displayObject.Add(option.ID, polyLines);
                }
            }
            else
            {
                _displayObject.Add(option.ID, polyLines);
            }
            GL.NewList(id, ListMode.Compile);

            foreach (var item in polyLines.Objects)
            {
                Polyline line = item.Value as Polyline;
                if (line != null)
                {
                    drawPolyline(line, option.DrawSize, option.DrawColor, checkMaxMin);
                }
            }
            if (ConnectEachOther)
            {
                List<string> keys = polyLines.Objects.Keys.ToList();

                double coneHeight = 4.0;
                double baseRad = 2.0;
                for (int i = 1; i < keys.Count; i++)
                {
                    int lastI = i - 1;
                    int currentI = i;

                    string nameLast = keys[lastI];
                    string nameCurrent = keys[currentI];


                    Polyline lastLine = polyLines.Objects[nameLast] as Polyline;
                    Polyline currentLine = polyLines.Objects[nameCurrent] as Polyline;
                    Point3D lastP = lastLine.LastPoint as Point3D;
                    Point3D startP = currentLine.FirstPoint as Point3D;
                    Vector3D v = new Vector3D(lastP, startP);
                    v.UnitVector();
                    Point3D startPCone = startP - v * coneHeight;
                    drawDashedLine(lastP, startP, option.DrawSize, option.DrawColor, (float)baseRad);
                    drawCone(lastP, (float)coneHeight, v, (float)baseRad, option.DrawColor);
                    drawCone(startPCone, (float)coneHeight, v, (float)baseRad, option.DrawColor);
                }
            }
            GL.EndList();
            //updateDataGridView();
        }

        void drawVector(Point3D pt)
        {
            PointV3D ptV3D = pt as PointV3D;
            if (ptV3D != null)
            {
                GL.LineWidth(3f);

                GL.Begin(PrimitiveType.Lines);


                GL.Color4(Color.DodgerBlue);
                GL.Vertex3(ptV3D.PArray);
                GL.Vertex3(ptV3D.GetVzExtendPoint(_VectorLength).PArray);

                GL.Color4(Color.LimeGreen);
                GL.Vertex3(ptV3D.PArray);
                GL.Vertex3(ptV3D.GetVyExtendPoint(_VectorLength).PArray);

                GL.Color4(Color.Red);
                GL.Vertex3(ptV3D.PArray);
                GL.Vertex3(ptV3D.GetVxExtendPoint(_VectorLength).PArray);


                GL.End();
            }
        }

        void drawVector(Polyline polyLine, float drawSize, Color drawColor, bool checkMaxMin, DisplayObjectType objectType)
        {
            GL.LineWidth(drawSize);
            GL.Color4(drawColor);

            GL.Begin(PrimitiveType.Lines);

            for (int i = 0; i < polyLine.Count; i++)
            {
                PointV3D p = polyLine.Points[i] as PointV3D;
                if (checkMaxMin)
                {
                    checkMaxMinPoint(p);
                }
                if (objectType == DisplayObjectType.Vector_z)
                {
                    GL.Vertex3(p.PArray);
                    GL.Vertex3(p.GetVzExtendPoint(_VectorLength).PArray);
                }
                else if (objectType == DisplayObjectType.Vector_y)
                {
                    GL.Vertex3(p.PArray);
                    GL.Vertex3(p.GetVyExtendPoint(_VectorLength).PArray);
                }
                else if (objectType == DisplayObjectType.Vector_x)
                {
                    GL.Vertex3(p.PArray);
                    GL.Vertex3(p.GetVxExtendPoint(_VectorLength).PArray);
                }
            }
            GL.End();
        }
        public void BuildVector(Polyline polyLine, int id, bool checkMaxMin, bool isUpdateObject)
        {
            if (id > _maxDisplayList) return;
            if (_displayOption.ContainsKey(id) == false)
            {
                throw new Exception($"Build vector fail. Display option doesn't contain ID {id}.");
            }

            DisplayObjectOption option = _displayOption[id];

            if (option.DisplayType < DisplayObjectType.Vector_z || option.DisplayType > DisplayObjectType.Vector_x) return;
            if (polyLine.Count == 0) return;
            if (polyLine.Points[0].GetType() != typeof(PointV3D)) return;

            if (_displayObject.ContainsKey(option.ID))
            {
                if (isUpdateObject)
                {
                    _displayObject.Remove(option.ID);
                    _displayObject.Add(option.ID, polyLine);
                }
            }
            else
            {
                _displayObject.Add(option.ID, polyLine);
            }
            GL.NewList(id, ListMode.Compile);
            drawVector(polyLine, option.DrawSize, option.DrawColor, checkMaxMin, option.DisplayType);
            GL.EndList();
            //updateDataGridView();
        }
        public void BuildVector(ObjectGroup polyLines, int id, bool checkMaxMin, bool isUpdateObject)
        {
            if (id > _maxDisplayList) return;
            if (_displayOption.ContainsKey(id) == false)
            {
                throw new Exception($"Build vector fail. Display option doesn't contain ID {id}.");
            }

            DisplayObjectOption option = _displayOption[id];

            if (option.DisplayType < DisplayObjectType.Vector_z || option.DisplayType > DisplayObjectType.Vector_x) return;
            if (polyLines.DataCount == 0) return;

            if (_displayObject.ContainsKey(option.ID))
            {
                if (isUpdateObject)
                {
                    _displayObject.Remove(option.ID);
                    _displayObject.Add(option.ID, polyLines);
                }
            }
            else
            {
                _displayObject.Add(option.ID, polyLines);
            }
            GL.NewList(id, ListMode.Compile);
            foreach (var item in polyLines.Objects)
            {
                Polyline line = item.Value as Polyline;
                if (line != null)
                    drawVector(line, option.DrawSize, option.DrawColor, checkMaxMin, option.DisplayType);
            }
            GL.EndList();
            //updateDataGridView();
        }
        void drawQuad(PointCloud cloud, float drawSize, Color drawColor, bool checkMaxMin)
        {
            GL.PointSize(drawSize);
            GL.Color4(drawColor);
            GL.Begin(PrimitiveType.Quads);

            int count = cloud.Count / 4;
            for (int i = 0; i < count; i++)
            {
                Point3D p0 = cloud.Points[i * 4];
                Point3D p1 = cloud.Points[i * 4 + 1];
                Point3D p2 = cloud.Points[i * 4 + 2];
                Point3D p3 = cloud.Points[i * 4 + 3];

                if (checkMaxMin)
                {
                    checkMaxMinPoint(p0);
                    checkMaxMinPoint(p1);
                    checkMaxMinPoint(p2);
                    checkMaxMinPoint(p3);
                }
                GL.Vertex3(p0.PArray);
                GL.Vertex3(p1.PArray);
                GL.Vertex3(p2.PArray);
                GL.Vertex3(p3.PArray);
            }
            GL.End();
        }
        public void BuildQuad(PointCloud cloud, int id, bool checkMaxMin, bool isUpdateObject)
        {
            if (id > _maxDisplayList) return;
            if (_displayOption.ContainsKey(id) == false)
            {
                throw new Exception($"Build quad fail. Display option doesn't contain ID {id}.");
            }
            if (cloud.Count < 4) return;
            DisplayObjectOption option = _displayOption[id];
            if (option.DisplayType != DisplayObjectType.PointCloud) return;

            if (_displayObject.ContainsKey(option.ID))
            {
                if (isUpdateObject)
                {
                    _displayObject.Remove(option.ID);
                    _displayObject.Add(option.ID, cloud);
                }
            }
            else
            {
                _displayObject.Add(option.ID, cloud);
            }
            GL.NewList(id, ListMode.Compile);
            drawQuad(cloud, option.DrawSize, option.DrawColor, checkMaxMin);
            GL.EndList();
            //updateDataGridView();
        }
        void drawGroup(ObjectGroup group, DisplayObjectOption option, bool checkMaxMin)
        {
            foreach (var item in group.Objects)
            {
                string subOptionName = item.Key;
                if (option.SubOptions.ContainsKey(subOptionName))
                {
                    DisplayObjectOption subOption = option.SubOptions[subOptionName];
                    switch (subOption.DisplayType)
                    {
                        case DisplayObjectType.PointCloud:
                            PointCloud cloud = item.Value as PointCloud;
                            if (cloud != null) drawPointCloud(cloud, subOption.DrawSize, subOption.DrawColor, checkMaxMin);
                            break;

                        case DisplayObjectType.Path:
                            Polyline line = item.Value as Polyline;
                            if (line != null) drawPolyline(line, subOption.DrawSize, subOption.DrawColor, checkMaxMin);
                            break;

                        case DisplayObjectType.Vector_z:
                            Polyline vector = item.Value as Polyline;
                            if (vector != null)
                            {
                                drawVector(vector, subOption.DrawSize, subOption.DrawColor, false, option.DisplayType);
                            }
                            break;

                        case DisplayObjectType.Point:
                            Point3D pt = item.Value as Point3D;
                            if (pt != null) drawPoint(pt, subOption.DrawSize, subOption.DrawColor, checkMaxMin);
                            break;

                        case DisplayObjectType.Quad:

                            break;

                        case DisplayObjectType.Composite:
                            ObjectGroup subGroup = item.Value as ObjectGroup;
                            if (subGroup != null) drawGroup(subGroup, subOption, checkMaxMin);
                            break;

                        default:

                            break;
                    }
                }
                else
                {
                    throw new Exception($"Display option - sub option doesn't contain Key {subOptionName}.");
                }
            }

        }
        public void BuildGroup(ObjectGroup group, int id, bool checkMaxMin, bool isUpdateObject)
        {
            if (id > _maxDisplayList) return;
            if (_displayOption.ContainsKey(id) == false)
            {
                throw new Exception($"Build group fail. Display option doesn't contain ID {id}.");
            }

            DisplayObjectOption option = _displayOption[id];
            if (option.DisplayType != DisplayObjectType.Composite) return;

            if (_displayObject.ContainsKey(option.ID))
            {
                if (isUpdateObject)
                {
                    _displayObject.Remove(option.ID);
                    _displayObject.Add(option.ID, group);
                }
            }
            else
            {
                _displayObject.Add(option.ID, group);
            }
            GL.NewList(option.ID, ListMode.Compile);
            drawGroup(group, option, checkMaxMin);
            GL.EndList();
            //updateDataGridView();
        }

        #endregion
    }

    public class DisplayObjectOption
    {
        public bool IsDisplay = true;
        public int ID = 0;
        public string Name = "";
        public Color DrawColor = Color.White;
        public DisplayObjectType DisplayType = DisplayObjectType.None;
        float _drawSize = 5f;
        public float DrawSize
        {
            get => _drawSize;
            set
            {
                if (value < 1.0f) _drawSize = 1.0f;
                else _drawSize = value;
            }
        }
        public bool IsShowAtDataGrid = true;
        public bool IsSelectable = false;
        public Dictionary<string, DisplayObjectOption> SubOptions = new Dictionary<string, DisplayObjectOption>();
        public DisplayObjectOption()
        {

        }
        public DisplayObjectOption(int id, string name, Color drawColor, DisplayObjectType displayType, float drawSize, bool isShowDataGrid = true)
        {
            ID = id;
            Name = name;
            DrawColor = drawColor;
            DisplayType = displayType;
            DrawSize = drawSize;
            IsShowAtDataGrid = isShowDataGrid;
        }
        public static DisplayObjectOption[] CreateDisplayOptionArray(int startID, int count, DisplayObjectType defaultType, float defaultSize, bool isSelectable)
        {
            DisplayObjectOption[] output = new DisplayObjectOption[count];
            for (int i = 0; i < output.Length; i++)
            {
                output[i] = new DisplayObjectOption();
                output[i].ID = startID + i;
                output[i].DisplayType = defaultType;
                output[i].DrawSize = defaultSize;
                output[i].IsSelectable = isSelectable;
            }
            return output;
        }
        public object[] ToDataGridRowObject()
        {
            return new object[] { DisplayType, IsDisplay, Name, DrawSize,"", ID, };
        }


    }
    public enum DisplayObjectType : int
    {
        None = 0,
        PointCloud,
        Path,
        Vector_z,
        Vector_y,
        Vector_x,
        Point,
        Quad,
        Composite,
    }
    public enum PointPickMode : int
    {
        None = 0,
        One,
        Two,
        Multiple,
        Draw
    }
}

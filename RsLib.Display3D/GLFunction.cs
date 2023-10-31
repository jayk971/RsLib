using OpenTK;
using OpenTK.Graphics.OpenGL;
using OpenTK.Input;
using RsLib.Common;
using RsLib.Display3D.Properties;
using RsLib.PointCloudLib;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Reflection;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ProgressBar;

namespace RsLib.Display3D
{
    public partial class Display3DControl : UserControl
    {
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
            buildAxis();
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
            buildAxis();
            GL.CallList(_headList);
            foreach (var item in _displayOption)
            {
                DisplayObjectOption option = item.Value;
                if (option.Visible) GL.CallList(option.ID);
            }
            if(_haveSelectPath)
            {
                drawSelectPath();
            }
            if (_pickMode == ePointPickMode.One)
            {
                if (_haveClosestPoint)
                    drawSelectedPoint();
            }
            else if (_pickMode == ePointPickMode.Two)
            {
                if (_multiSelectPoints.Count <= 2)
                    drawMeasureLine();
            }
            else if (_pickMode == ePointPickMode.Multiple)
            {
                drawTempMultipleSelect();
                drawMultipleSelect();
            }
            else if(_pickMode == ePointPickMode.Draw)
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
                if (_pickMode == ePointPickMode.None) return;

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
                    case ePointPickMode.One:
                        _middleMouseCount = 0;
                        _multiSelectPoints.Clear();
                        _multiSelectPoints.Add(_closetPoint);
                        showSelectPointData(_haveClosestPoint, _closetPoint);
                        if(_haveClosestPoint) AfterPointSelected?.Invoke(_closetPoint);
                        //AfterPointsSelected?.Invoke(_multiSelectPoints);
                        break;

                    case ePointPickMode.Two:
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
                    case ePointPickMode.Multiple:
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
                    case ePointPickMode.Draw:
                        addToDrawPath(nearWorld, farWorld);
                        break;
                    default:
                        _middleMouseCount = 0;

                        break;

                }

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


    }



}

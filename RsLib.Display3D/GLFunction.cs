using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Text;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using OpenTK;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL;

using RsLib.PointCloud;
using RsLib.Display3D.Properties;
using RsLib.Common;
namespace RsLib.Display3D
{
    using RPointCloud = RsLib.PointCloud.PointCloud;
    public partial class Display3DControl : UserControl
    {
        GLControl _glControl;
        private float _scale = 1.0f;
        private Vector3 _translation = Vector3.Zero;
        private Vector3 _rotation = Vector3.Zero;
        bool _leftButtonPressed = false;
        bool _rightButtonPressed = false;

        int _lastX, _lastY;
        int _lastRotX, _lastRotY;
        int _headList;
        int _maxDisplayList;
        bool _enableSelectPoint = false;
        bool _haveClosestPoint = false;
        Point3D _closetPoint = new Point3D();
        int _selectIndex = -1;
        Dictionary<int, Object3D> _selectCandidate = new Dictionary<int, Object3D>();
        Dictionary<int,DisplayObjectOption> _displayObject = new Dictionary<int, DisplayObjectOption>();

        Vector3 _maxPoint = new Vector3(float.MinValue, float.MinValue, float.MinValue);
        Vector3 _minPoint = new Vector3(float.MaxValue, float.MaxValue, float.MaxValue);
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
            GL.Disable(EnableCap.Lighting);
            _headList = GL.GenLists(_maxDisplayList);
            BuildAxis();
        }
        private void GlControl_Paint(object sender, PaintEventArgs e)
        {
            GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);

            GL.MatrixMode(MatrixMode.Projection);
            GL.LoadIdentity();
            double view_range = 300;
            double aspect = (double)_glControl.Width / _glControl.Height;
            if (aspect >= 1.0)
                GL.Ortho(-view_range * aspect, view_range * aspect, -view_range, view_range, -8 * view_range, 8 * view_range);
            else
                GL.Ortho(-view_range, view_range, -view_range / aspect, view_range / aspect, -8 * view_range, 8 * view_range);

            //GL.Ortho(-100.0, 100.0, -100.0, 100.0, -100.0, 100.0);

            GL.MatrixMode(MatrixMode.Modelview);
            GL.LoadIdentity();
            GL.PushMatrix();
            //Matrix4 localTranslate = Matrix4.CreateTranslation(-1 * _avgPoint);
            //localTranslate *= Matrix4.CreateRotationX(_rotation.Y);
            //localTranslate *= Matrix4.CreateRotationY(_rotation.X);
            //localTranslate *= Matrix4.CreateRotationZ(_rotation.Z);
            //localTranslate *= Matrix4.CreateTranslation(_avgPoint);
            //localTranslate *= Matrix4.CreateScale(_scale, _scale, _scale);
            //localTranslate *= Matrix4.CreateTranslation(_translation.X, _translation.Y, 0.0f);
            //GL.MultMatrix(ref localTranslate);
            multiMatrix();

            GL.Viewport(0, 0, _glControl.Width, _glControl.Height);
            
            GL.CallList(_headList);
            foreach (var item in _displayObject)
            {
                DisplayObjectOption option = item.Value;
                if (option.IsDisplay) GL.CallList(option.ID);
            }
            if (_enableSelectPoint && _haveClosestPoint) drawSelectedPoint(Settings.Default.SelectPoint);

            if (_enableSelectPoint && _selectIndex > 1) drawSelectRangeFrame(Settings.Default.SelectRange);

            GL.MatrixMode(MatrixMode.Modelview);
            GL.PopMatrix();
            _glControl.SwapBuffers();
            _glControl.Invalidate();
        }
        private void GlControl_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Middle)
            {
                ResetView();
            }
        }
        private void GlControl_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Middle)
            {
                if (_enableSelectPoint == false) return;

                int viewportX = 0;
                int viewportY = 0;
                int viewportWidth = _glControl.Width;
                int viewportHeight = _glControl.Height;

                GL.MatrixMode(MatrixMode.Modelview);
                GL.LoadIdentity();
                GL.PushMatrix();
                //Matrix4 localTranslate = Matrix4.CreateTranslation(-1 * _avgPoint);
                //localTranslate *= Matrix4.CreateRotationX(_rotation.Y);
                //localTranslate *= Matrix4.CreateRotationY(_rotation.X);
                //localTranslate *= Matrix4.CreateRotationZ(_rotation.Z);
                //localTranslate *= Matrix4.CreateTranslation(_avgPoint);
                //localTranslate *= Matrix4.CreateScale(_scale, _scale, _scale);
                //localTranslate *= Matrix4.CreateTranslation(_translation.X, _translation.Y, 0.0f);
                //GL.MultMatrix(ref localTranslate);
                multiMatrix();
                // Get the world coordinates of the mouse click
                Vector3 near = new Vector3(e.X, _glControl.Height - e.Y, 0f);
                Vector3 far = new Vector3(e.X, _glControl.Height - e.Y, 1f);
                Vector3 nearWorld = unProject(near, viewportX, viewportY, viewportWidth, viewportHeight);
                Vector3 farWorld = unProject(far, viewportX, viewportY, viewportWidth, viewportHeight);
                GL.PopMatrix();
                _haveClosestPoint = closestPoint(nearWorld, farWorld, out _closetPoint);
                AfterPointSelected?.Invoke(_haveClosestPoint, _closetPoint);
                showSelectPointData(_haveClosestPoint, _closetPoint);
            }
        }


        private void GlControl_MouseMove(object sender, MouseEventArgs e)
        {
            if (_leftButtonPressed)
            {
                rotate(e.X, e.Y);
                //_translation.X += (e.X - _lastX) / _scale;
                //_translation.Y -= (e.Y - _lastY) / _scale;

                ////_glControl.Invalidate();
                //_lastX = e.X;
                //_lastY = e.Y;
            }
            else if (_rightButtonPressed)
            {
                shift(e.X, e.Y);
                //_rotation.X += (e.X - _lastRotX) / (float)_glControl.Width * 180.0f;
                //_rotation.Y += 1 * (e.Y - _lastRotY) / (float)_glControl.Height * 180.0f;
                ////GL.Rotate(dx * 180.0f, Vector3.UnitY);
                ////GL.Rotate(-dy * 180.0f, Vector3.UnitX);
                ////_glControl.Invalidate();
                //_lastRotX = e.X;
                //_lastRotY = e.Y;
            }
        }
        private void GlControl_MouseUp(object sender, MouseEventArgs e)
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
        private void GlControl_MouseWheel(object sender, MouseEventArgs e)
        {
            _scale += (float)e.Delta / SystemInformation.MouseWheelScrollDelta / 10.0f;
            if (_scale <= 0.1f) _scale = 0.1f;
        }
        private void GlControl_MouseDown(object sender, MouseEventArgs e)
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
        public void Clear()
        {
            foreach (var item in _displayObject)
            {
                DisplayObjectOption objOption = item.Value;
                GL.NewList(objOption.ID, ListMode.Compile);
                GL.EndList();
            }
            _selectCandidate.Clear();
            _displayObject.Clear();
            _maxPoint = new Vector3(float.MinValue, float.MinValue, float.MinValue);
            _minPoint = new Vector3(float.MaxValue, float.MaxValue, float.MaxValue);
            ResetView();
            updateUI();
        }
        public void ChangeDisplayOption(DisplayObjectOption newOption)
        {
            if(_displayObject.ContainsKey(newOption.ID))
            {
                _displayObject[newOption.ID].DisplayColor = newOption.DisplayColor;
                _displayObject[newOption.ID].IsDisplay = newOption.IsDisplay;
                ReBuild(newOption.ID);
            }
        }

        public void ReBuildAll()
        {
            foreach (var item in _displayObject)
            {
                ReBuild(item.Key);
            }
        }
        public void ReBuild(int id)
        {
            if (this.InvokeRequired)
            {
                Action<int> action = new Action<int>(ReBuild);
                this.Invoke(action, id);
            }
            else
            {
                if (_displayObject.ContainsKey(id))
                {
                    DisplayObjectOption option = _displayObject[id];
                    switch (option.DisplayType)
                    {
                        case DisplayObjectType.PointCloud:
                            BuildPointCloud((RPointCloud)_selectCandidate[id], option.DisplayColor, option.DrawSize, id, option.Name, true);
                            break;
                        case DisplayObjectType.Vector:
                            BuildVector((Polyline)_selectCandidate[id], option.DisplayColor, option.DrawSize, id, option.Name, true);
                            break;
                        case DisplayObjectType.Path:
                            BuildPath((Polyline)_selectCandidate[id], option.DisplayColor, option.DrawSize, id, option.Name, true);
                            break;
                        case DisplayObjectType.Point:

                            break;
                        default:

                            break;
                    }
                }
            }
        }
        public void SetObjectVisible(int id,bool visible)
        {
            if(_displayObject.ContainsKey(id))
            {
                _displayObject[id].IsDisplay = visible;
            }
        }
        public void ResetView()
        {
            _translation = new Vector3();
            _rotation = new Vector3();
            _scale = 1.0f;
        }
        void multiMatrix()
        {
            Matrix4 localTranslate = Matrix4.CreateTranslation(-1 * _avgPoint);
            localTranslate *= Matrix4.CreateRotationX(_rotation.Y);
            localTranslate *= Matrix4.CreateRotationY(_rotation.X);
            localTranslate *= Matrix4.CreateRotationZ(_rotation.Z);
            localTranslate *= Matrix4.CreateScale(_scale, _scale, _scale);
            localTranslate *= Matrix4.CreateTranslation(_avgPoint);
            localTranslate *= Matrix4.CreateTranslation(_translation.X, _translation.Y, 0.0f);
            localTranslate *= Matrix4.CreateTranslation(-1*_avgPoint);

            GL.MultMatrix(ref localTranslate);
        }
        void showSelectPointData(bool haveClosetPoint, Point3D closetPoint)
        {
            if (_haveClosestPoint)
            {
                lbl_SelectPoint.Text = $"{_closetPoint.ToString()}";
            }
            else
            {
                lbl_SelectPoint.Text = "--";

            }
        }
        void shift(int mouseX, int mouseY)
        {
            _translation.X += (mouseX - _lastX);
            _translation.Y -= (mouseY - _lastY);

            _glControl.Invalidate();
            _lastX = mouseX;
            _lastY = mouseY;
        }
        void rotate(int mouseX, int mouseY)
        {
            //_rotation.X += (mouseX - _lastRotX) / (float)_glControl.Width * 180.0f;
            //_rotation.Y += 1 * (mouseY - _lastRotY) / (float)_glControl.Height * 180.0f;
            _rotation.X += (mouseX - _lastRotX) / (float)_glControl.Width * 10.0f;
            _rotation.Y += 1 * (mouseY - _lastRotY) / (float)_glControl.Height * 10.0f;
            //GL.Rotate(dx * 180.0f, Vector3.UnitY);
            //GL.Rotate(-dy * 180.0f, Vector3.UnitX);
            _glControl.Invalidate();
            _lastRotX = mouseX;
            _lastRotY = mouseY;
        }
        void checkMaxMinPoint(Point3D p)
        {
            if (p.X >= _maxPoint.X) _maxPoint.X = (float)p.X;
            if (p.Y >= _maxPoint.Y) _maxPoint.Y = (float)p.Y;
            if (p.Z >= _maxPoint.Z) _maxPoint.Z = (float)p.Z;

            if (p.X <= _minPoint.X) _minPoint.X = (float)p.X;
            if (p.Y <= _minPoint.Y) _minPoint.Y = (float)p.Y;
            if (p.Z <= _minPoint.Z) _minPoint.Z = (float)p.Z;

        }
        bool closestPoint(Vector3 nearWorld, Vector3 farWorld, out Point3D closestPoint)
        {
            Vector3 rayDir, rayOrig;
            closestPoint = new Point3D(0, 0, 0);
            rayOrig = nearWorld;
            rayDir = Vector3.Subtract(farWorld, nearWorld).Normalized();
            float closestDistance = float.MaxValue;
            bool hasClosetPoint = false;
            if (_selectCandidate.Count == 0) return false;
            if (_selectIndex == -1) return false;
            if (_selectCandidate.ContainsKey(_selectIndex) == false) return false;

            var selectCandidateCloud = _selectCandidate[_selectIndex] as PointCloud.PointCloud;
            foreach (Point3D point in selectCandidateCloud.Points)
            {
                Vector3 vecPoint = new Vector3((float)point.X, (float)point.Y, (float)point.Z);
                float distance = Vector3.Distance(rayOrig, vecPoint);
                Vector3 displacement = vecPoint - rayOrig;
                float projection = Vector3.Dot(displacement, rayDir);
                float verticalDis = (float)Math.Sqrt(Math.Pow(distance, 2) - Math.Pow(projection, 2));
                if (verticalDis < closestDistance)
                {
                    closestDistance = verticalDis;
                    if (verticalDis < 1.0)
                    {
                        hasClosetPoint = true;
                        closestPoint = point;
                    }
                }
            }
            return hasClosetPoint;
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

            //Matrix4 viewport = Matrix4.Identity;
            //viewport.M11 = viewportWidth / 2.0f;
            //viewport.M22 = viewportHeight / 2.0f;
            //viewport.M33 = 1.0f;
            //viewport.M41 = viewportX + viewport.M11;
            //viewport.M42 = viewportY + viewport.M22;

            //Vector4.Transform(ref vec, ref projection, out vec);
            //Vector4.Transform(ref vec, ref modelview, out vec);

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
        void drawSelectedPoint(Color drawColor)
        {
            GL.PointSize(10);
            GL.Begin(PrimitiveType.Points);
            GL.Color3(drawColor);
            GL.Vertex3(_closetPoint.PArray);
            GL.End();
        }
        void drawSelectRangeFrame(Color drawColor)
        {
            if (_selectIndex <= 1) return;
            if (_selectCandidate.Count == 0) return;
            if (_selectCandidate.ContainsKey(_selectIndex) == false) return;
            var selectCandidate = _selectCandidate[_selectIndex] as PointCloud.PointCloud;
            Point3D min = selectCandidate.Min;
            Point3D max = selectCandidate.Max;

            drawFrame(new Vector3((float)min.X, (float)min.Y, (float)min.Z), new Vector3((float)max.X, (float)max.Y, (float)max.Z), 5f, drawColor);
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
        void drawFrame(Vector3 minPoint,Vector3 maxPoint,float lineWidth,Color drawColor)
        {
            /*
             *                                  ________________
             *                                /                          /|
             *                              /                           / |
             *                              ----------------  /
             *                             |_______________|
             */

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
        }
        private void BuildAxis()
        {
            GL.NewList(_headList, ListMode.Compile);
            float cylinderHeight = 20f;
            float capHeight = 10f;
            float cylinderRadius = 1f;
            float capRadius = 4f;
            drawCylinder(Vector3.Zero, Vector3.UnitX, cylinderRadius, cylinderHeight, Color.Red);
            drawCylinder(Vector3.Zero, Vector3.UnitY, cylinderRadius, cylinderHeight, Color.Green);
            drawCylinder(Vector3.Zero, Vector3.UnitZ, cylinderRadius, cylinderHeight, Color.Blue);
            drawSphere(Vector3.Zero, 3.0f, Color.White);
            drawCone(new Vector3(cylinderHeight, 0, 0), capHeight, Vector3.UnitX, capRadius, Color.Red);
            drawCone(new Vector3(0, cylinderHeight, 0), capHeight, Vector3.UnitY, capRadius, Color.Green);
            drawCone(new Vector3(0, 0, cylinderHeight), capHeight, Vector3.UnitZ, capRadius, Color.Blue);
            GL.EndList();
        }
        public void BuildPointCloud(RPointCloud cloud, Color color, double pointSize, int callListIndex,string name,bool isReBuild = false)
        {
            if (callListIndex > _maxDisplayList) return;

            if (isReBuild == false)
            {
                if (_selectCandidate.ContainsKey(callListIndex))
                {
                    throw new Exception($"callListIndex {callListIndex} is already in the Dictionary");
                }
                _selectCandidate.Add(callListIndex, cloud);
                _displayObject.Add(callListIndex, new DisplayObjectOption(callListIndex, name, color,DisplayObjectType.PointCloud,(float)pointSize));
            }
            updateUI();
            GL.NewList(callListIndex, ListMode.Compile);
            GL.PointSize((float)pointSize);
            GL.Color4(color);
            GL.Begin(PrimitiveType.Points);
            foreach (var item in cloud.Points)
            {
                checkMaxMinPoint(item);
                GL.Vertex3(item.PArray);
            }
            GL.End();
            GL.EndList();

        }
        public void BuildPath(Polyline polyLine, Color color, double lineWidth, int callListIndex, string name, bool isReBuild = false)
        {
            if (callListIndex > _maxDisplayList) return;
            if (polyLine.Count < 2) return;
            if (isReBuild == false)
            {
                if (_selectCandidate.ContainsKey(callListIndex))
                {
                    throw new Exception($"callListIndex {callListIndex} is already in the Dictionary");
                }
                _selectCandidate.Add(callListIndex, polyLine);
                _displayObject.Add(callListIndex, new DisplayObjectOption(callListIndex, name, color,DisplayObjectType.Path,(float)lineWidth));
            }
            updateUI();

            GL.NewList(callListIndex, ListMode.Compile);
            GL.LineWidth((float)lineWidth);
            GL.Color4(color);
            GL.Begin(PrimitiveType.LineStrip);

            GL.Vertex3(polyLine.Points[0].PArray);
            checkMaxMinPoint(polyLine.Points[0]);
            GL.Vertex3(polyLine.Points[1].PArray);
            checkMaxMinPoint(polyLine.Points[1]);
            for (int i = 2; i < polyLine.Count; i++)
            {
                GL.Vertex3(polyLine.Points[i].PArray);
                checkMaxMinPoint(polyLine.Points[i]);
            }

            GL.End();
            GL.EndList();

        }
        public void BuildVector(Polyline polyLine, Color color, double lineWidth, int callListIndex,string name, bool isReBuild = false)
        {
            if (callListIndex > _maxDisplayList) return;
            if (polyLine.Count == 0) return;
            if (polyLine.Points[0].GetType() != typeof(PointV3D)) return;

            if (isReBuild == false)
            {
                if (_selectCandidate.ContainsKey(callListIndex))
                {
                    throw new Exception($"callListIndex {callListIndex} is already in the Dictionary");
                }
                _selectCandidate.Add(callListIndex, polyLine);
                _displayObject.Add(callListIndex, new DisplayObjectOption(callListIndex, name, color,DisplayObjectType.Vector,(float)lineWidth));
            }
            updateUI();

            GL.NewList(callListIndex, ListMode.Compile);
            GL.LineWidth((float)lineWidth);
            GL.Color4(color);
            GL.Begin(PrimitiveType.Lines);

            for (int i = 0; i < polyLine.Count; i++)
            {
                PointV3D p = polyLine.Points[i] as PointV3D;
                GL.Vertex3(p.PArray);
                GL.Vertex3(p.GetVzExtendPoint(10).PArray);
            }
            GL.End();

            GL.EndList();

        }
        #endregion
    }

    public class DisplayObjectOption
    {
        public bool IsDisplay = true;
        public int ID = 0;
        public string Name = "";
        public Color DisplayColor = Color.White;
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


        public DisplayObjectOption()
        {

        }
        public DisplayObjectOption(int id,string name,Color drawColor,DisplayObjectType displayType,float drawSize)
        {
            ID = id;
            Name = name;
            DisplayColor = drawColor;
            DisplayType = displayType;
            DrawSize =  drawSize;
        }

        public object[] ToDataGridRowObject()
        {
            return new object[] { IsDisplay, Name, ID, "",DrawSize};
        }


    }
    public enum DisplayObjectType : int
    {
        None = 0,
        PointCloud,
        Path,
        Vector,
        Point,
    }

}

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
        #region private method
        private void addToDrawPath(Vector3 near, Vector3 far)
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

        private void multiMatrix()
        {
            Matrix4 localTranslate = Matrix4.CreateTranslation(-1 * _oldAvgPoint);
            localTranslate *= _rotationMatrix;
            localTranslate *= Matrix4.CreateScale(_scale, _scale, _scale);
            localTranslate *= Matrix4.CreateTranslation(_avgPoint);
            localTranslate *= Matrix4.CreateTranslation(_translation.X, _translation.Y, 0.0f);
            localTranslate *= Matrix4.CreateTranslation(-1 * _avgPoint);
            if (_isCheckedMaxMin)
            {
                _oldAvgPoint = _avgPoint;
                _isCheckedMaxMin = false;
            }
            GL.MultMatrix(ref localTranslate);
        }
        private void showSelectPointData(bool haveClosetPoint, Point3D closetP)
        {
            treeView1.Nodes.Clear();
            PointV3D ptv3D = closetP as PointV3D;

            if (_haveClosestPoint)
            {
                treeView1.Nodes.Add("Location", "Location");

                treeView1.Nodes["Location"].Nodes.Add($"X : {closetP.X:F2}");
                treeView1.Nodes["Location"].Nodes.Add($"Y : {closetP.Y:F2}");
                treeView1.Nodes["Location"].Nodes.Add($"Z : {closetP.Z:F2}");

                if (ptv3D != null)
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
                LineOption lineOption = closetP.GetOption(typeof(LineOption)) as LineOption;
                if (lineOption != null)
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
        private List<string> getPointProperty(Point3D p)
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
        private void showSelectMeasurePointData(PointCloud measureLine)
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
        private void shift(int mouseX, int mouseY)
        {
            _translation.X += (mouseX - _lastX);
            _translation.Y -= (mouseY - _lastY);

            _glControl.Invalidate();
            _lastX = mouseX;
            _lastY = mouseY;
        }
        private void rotate(int mouseX, int mouseY)
        {
            if (LockRotate) return;

            Vector3 trackballStart = ScreenToTrackball(_lastRotX, _lastRotY);
            Vector3 trackballCurrent = ScreenToTrackball(mouseX, mouseY);
            Vector3 axis = Vector3.Cross(trackballStart, trackballCurrent);
            float angle = (float)Math.Acos(Vector3.Dot(trackballStart, trackballCurrent));
            angle *= Settings.Default.Sensitivity;
            _lastRotX = mouseX;
            _lastRotY = mouseY;
            if (axis.Length == 0) return;
            Matrix4 deltaRotation = Matrix4.CreateFromAxisAngle(axis, angle);

            _rotationMatrix *= deltaRotation;

            _glControl.Invalidate();

        }
        private void checkMaxMinPoint(Point3D p)
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
        private bool closestPoint(Vector3 nearWorld, Vector3 farWorld, out Point3D closestPoint)
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

            eDisplayObjectType displayObjectType = _displayOption[_CurrentSelectObjectIndex].DisplayType;
            if (displayObjectType == eDisplayObjectType.None) return false;
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
        private Tuple<float, Point3D> calculateNearestPoint(Vector3 rayOrig, Vector3 rayDir, float closestDistance, List<Point3D> points)
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
        private float calculatePointMinDistance(Vector3 rayOrig, Vector3 rayDir, Point3D point)
        {
            Vector3 vecPoint = new Vector3((float)point.X, (float)point.Y, (float)point.Z);
            float distance = Vector3.Distance(rayOrig, vecPoint);
            Vector3 displacement = vecPoint - rayOrig;
            float projection = Vector3.Dot(displacement, rayDir);
            float verticalDis = (float)Math.Sqrt(Math.Pow(distance, 2) - Math.Pow(projection, 2));
            return verticalDis;
        }
        private Vector3 unProject(Vector3 winCoord, int viewportX, int viewportY, int viewportWidth, int viewportHeight)
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
        private void drawCone(Vector3 baseCenter, float height, Vector3 normalDir, float baseRadius, Color drawColor)
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
        private Matrix4 getRotationMatrix(Vector3 normal)
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
        private void drawSelectedPoint()
        {
            GL.PointSize(Settings.Default.Size_SelectPoint);
            GL.Begin(PrimitiveType.Points);
            GL.Color3(Settings.Default.Color_SelectPoint);
            GL.Vertex3(_closetPoint.PArray);
            GL.End();
            drawVector(_closetPoint);
        }
        private void drawDrawPath()
        {
            int count = _DrawPath.Count;
            if (count == 0) return;
            for (int i = 0; i < _DrawPath.Count; i++)
            {
                Point3D pt = _DrawPath.Points[i];
                switch (currentPlane)
                {
                    case eCoordPlane.XY:
                        pt.Z = _maxPoint.Z == float.MinValue ? 0f : _maxPoint.Z;
                        break;

                    case eCoordPlane.XZ:
                        pt.Y = _minPoint.Y == float.MaxValue ? 0f : _minPoint.Y;
                        break;

                    case eCoordPlane.YZ:
                        pt.X = _maxPoint.X == float.MinValue ? 0f : _maxPoint.X;
                        break;

                    case eCoordPlane.YX:
                        pt.Z = _minPoint.Z == float.MaxValue ? 0f : _minPoint.Z;
                        break;

                    case eCoordPlane.ZX:
                        pt.Y = _maxPoint.Y == float.MinValue ? 0f : _maxPoint.Y;
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
        private void drawSelectPath()
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
            drawPointCloud(_SelectPath, Settings.Default.Size_SelectPath * 2, Settings.Default.Color_SelectPath, false);
            drawVector(_SelectPath, Settings.Default.Size_SelectPath, Color.Blue, false, eDisplayObjectType.Vector_z);
            drawCone(pos, 7, dir, 2, Settings.Default.Color_SelectPath);

        }
        private void drawMeasureLine()
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
        private void drawMultipleSelect()
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
        private void drawTempMultipleSelect()
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
        private void drawSelectRangeFrame()
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
        private void drawCircle(Vector3 center, Vector3 normalDir, float radius, Color drawColor)
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
        private void drawCylinder(Vector3 center, Vector3 normalDir, float radius, float length, Color drawColor)
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
        private void drawArrow(Point3D pStart, Point3D pEnd, float cylinderHeight, float cylynderRadius, float topCapHeight, float topCapRadius, Vector3 normalDir, Color drawColor)
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
        private void drawArrow(Vector3 baseCenter, float cylinderHeight, float cylynderRadius, float topCapHeight, float topCapRadius, Vector3 normalDir, Color drawColor)
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
        private void drawSphere(Vector3 center, float radius, Color drawColor)
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
        private void drawFrame(Vector3 minPoint, Vector3 maxPoint, float lineWidth, Color drawColor, bool useDashedLine)
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
        private void drawDashedLine(Vector3 start, Vector3 end, float lineWidth, Color drawColor, float dashLength)
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
        private void buildAxis()
        {
            //GL.NewList(_headList, ListMode.Compile);
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
            //GL.EndList();
        }
        private void drawPoint(Point3D pt, float drawSize, Color drawColor, bool checkMaxMin)
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
        private void drawPointCloud(PointCloud cloud, float drawSize, Color drawColor, bool checkMaxMin)
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
        private void drawPolyline(Polyline polyLine, float drawSize, Color drawColor, bool checkMaxMin)
        {
            GL.LineWidth(drawSize);
            GL.Color4(drawColor);
            GL.Begin(PrimitiveType.LineStrip);

            GL.Vertex3(polyLine.Points[0].PArray);
            checkMaxMinPoint(polyLine.Points[0]);
            GL.Vertex3(polyLine.Points[1].PArray);
            checkMaxMinPoint(polyLine.Points[1]);
            for (int i = 2; i < polyLine.Count; i++)
            {

                GL.Vertex3(polyLine.Points[i].PArray);
                if (checkMaxMin)
                {
                    checkMaxMinPoint(polyLine.Points[i]);
                }
            }

            GL.End();
        }
        private void drawVector(Point3D pt)
        {
            PointV3D ptV3D = pt as PointV3D;
            if (ptV3D != null)
            {
                GL.LineWidth(3f);

                GL.Begin(PrimitiveType.Lines);


                GL.Color4(Color.DodgerBlue);
                GL.Vertex3(ptV3D.PArray);
                GL.Vertex3(ptV3D.GetVzExtendPoint(10).PArray);

                GL.Color4(Color.LimeGreen);
                GL.Vertex3(ptV3D.PArray);
                GL.Vertex3(ptV3D.GetVyExtendPoint(10).PArray);

                GL.Color4(Color.Red);
                GL.Vertex3(ptV3D.PArray);
                GL.Vertex3(ptV3D.GetVxExtendPoint(10).PArray);


                GL.End();
            }
        }
        private void drawVector(Polyline polyLine, float drawSize, Color drawColor, bool checkMaxMin, eDisplayObjectType objectType)
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
                if (objectType == eDisplayObjectType.Vector_z)
                {
                    GL.Vertex3(p.PArray);
                    GL.Vertex3(p.GetVzExtendPoint(10).PArray);
                }
                else if (objectType == eDisplayObjectType.Vector_y)
                {
                    GL.Vertex3(p.PArray);
                    GL.Vertex3(p.GetVyExtendPoint(10).PArray);
                }
                else if (objectType == eDisplayObjectType.Vector_x)
                {
                    GL.Vertex3(p.PArray);
                    GL.Vertex3(p.GetVxExtendPoint(10).PArray);
                }
            }
            GL.End();
        }
        private void drawQuad(PointCloud cloud, float drawSize, Color drawColor, bool checkMaxMin)
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
        private void drawGroup(ObjectGroup group, DisplayObjectOption option, bool checkMaxMin)
        {
            foreach (var item in group.Objects)
            {
                string subOptionName = item.Key;
                if (option.SubItemOptions.ContainsKey(subOptionName))
                {
                    DisplayObjectOption subOption = option.SubItemOptions[subOptionName];
                    switch (subOption.DisplayType)
                    {
                        case eDisplayObjectType.PointCloud:
                            PointCloud cloud = item.Value as PointCloud;
                            if (cloud != null) drawPointCloud(cloud, subOption.DrawSize, subOption.DrawColor, checkMaxMin);
                            break;

                        case eDisplayObjectType.Path:
                            Polyline line = item.Value as Polyline;
                            if (line != null) drawPolyline(line, subOption.DrawSize, subOption.DrawColor, checkMaxMin);
                            break;

                        case eDisplayObjectType.Vector_z:
                            Polyline vector = item.Value as Polyline;
                            if (vector != null)
                            {
                                drawVector(vector, subOption.DrawSize, subOption.DrawColor, false, option.DisplayType);
                            }
                            break;

                        case eDisplayObjectType.Point:
                            Point3D pt = item.Value as Point3D;
                            if (pt != null) drawPoint(pt, subOption.DrawSize, subOption.DrawColor, checkMaxMin);
                            break;

                        case eDisplayObjectType.Quad:

                            break;

                        case eDisplayObjectType.Composite:
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

        #endregion
        #region public Method
        public void BuildPoint(Point3D pt, int id, bool checkMaxMin, bool isUpdateObject)
        {
            if (id > _maxDisplayList) return;
            if (_displayOption.ContainsKey(id) == false)
            {
                throw new Exception($"Build point fail. Display option doesn't contain ID {id}.");
            }
            DisplayObjectOption option = _displayOption[id];
            if (option.DisplayType != eDisplayObjectType.Point) return;

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

        public void BuildPointCloud(PointCloud cloud, int id, bool checkMaxMin, bool isUpdateObject)
        {
            if (id > _maxDisplayList) return;
            if (_displayOption.ContainsKey(id) == false)
            {
                throw new Exception($"Build point cloud fail. Display option doesn't contain ID {id}.");
            }

            DisplayObjectOption option = _displayOption[id];
            if (option.DisplayType != eDisplayObjectType.PointCloud) return;
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
            if (option.DisplayType != eDisplayObjectType.PointCloud) return;

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

        public void BuildPath(Polyline polyLine, int id, bool checkMaxMin, bool isUpdateObject)
        {
            if (id > _maxDisplayList) return;
            if (_displayOption.ContainsKey(id) == false)
            {
                throw new Exception($"Build path fail. Display option doesn't contain ID {id}.");
            }
            DisplayObjectOption option = _displayOption[id];
            if (option.DisplayType != eDisplayObjectType.Path) return;
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
            if (option.DisplayType != eDisplayObjectType.Path) return;

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
            option.ClearSubItems();
            GL.NewList(id, ListMode.Compile);
            foreach (var item in polyLines.Objects)
            {
                if (item.Value is Polyline line)
                {
                    if(line.GetOption(typeof(LineOption)) is LineOption lo)
                    {
                        option.AddSubItemOption(lo.LineIndex);
                    }
                    drawPolyline(line, option.DrawSize, option.DrawColor, checkMaxMin);
                }
            }
            GL.EndList();
            //updateDataGridView();
        }

        public void BuildVector(Polyline polyLine, int id, bool checkMaxMin, bool isUpdateObject)
        {
            if (id > _maxDisplayList) return;
            if (_displayOption.ContainsKey(id) == false)
            {
                throw new Exception($"Build vector fail. Display option doesn't contain ID {id}.");
            }

            DisplayObjectOption option = _displayOption[id];

            if (option.DisplayType < eDisplayObjectType.Vector_z || option.DisplayType > eDisplayObjectType.Vector_x) return;
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

            if (option.DisplayType < eDisplayObjectType.Vector_z || option.DisplayType > eDisplayObjectType.Vector_x) return;
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

        public void BuildQuad(PointCloud cloud, int id, bool checkMaxMin, bool isUpdateObject)
        {
            if (id > _maxDisplayList) return;
            if (_displayOption.ContainsKey(id) == false)
            {
                throw new Exception($"Build quad fail. Display option doesn't contain ID {id}.");
            }
            if (cloud.Count < 4) return;
            DisplayObjectOption option = _displayOption[id];
            if (option.DisplayType != eDisplayObjectType.PointCloud) return;

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

        public void BuildGroup(ObjectGroup group, int id, bool checkMaxMin, bool isUpdateObject)
        {
            if (id > _maxDisplayList) return;
            if (_displayOption.ContainsKey(id) == false)
            {
                throw new Exception($"Build group fail. Display option doesn't contain ID {id}.");
            }

            DisplayObjectOption option = _displayOption[id];
            if (option.DisplayType != eDisplayObjectType.Composite) return;

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
}

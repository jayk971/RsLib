using Accord.Math;
using RsLib.Display3D;
using RsLib.PointCloudLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RsLib.DemoForm
{
    public class Head2ndAlign : ISipingPlugin
    {
        ICPMatch icp = new ICPMatch();
        PointCloud _adjustModel = null;
        ObjectGroup _adjustPath = null;
        Head2ndAlignParameter Para = new Head2ndAlignParameter();
        PropertyGrid _propertyGrid = new PropertyGrid();
        public Head2ndAlign() 
        {
            _propertyGrid.SelectedObject = Para;
        }
        public bool AdjustPath(PointCloud scanCloud, PointCloud modelCloud, ObjectGroup sipingPath)
        {
            //scanCloud.CompareOtherCloud(modelCloud.kdTree, 0, 2.5, true);
            icp.SetModel(scanCloud);

            LayerPointCloud lptCloud = new LayerPointCloud(modelCloud, false, 0.3);
            Point3D maxPt = lptCloud.Max;
            Point3D minPt = lptCloud.Min;
            double xDiff = (maxPt.X + Para.MapExtendLength) - (minPt.X - Para.MapExtendLength);
            double yDiff = (maxPt.Y + Para.MapExtendLength) - (minPt.Y - Para.MapExtendLength);
            double zDiff = (maxPt.Z + Para.MapExtendLength) - (minPt.Z - Para.MapExtendLength);
            double step = Para.MapStep;

            int xStep = (int)(xDiff / step) + 1;
            int yStep = (int)(yDiff / step) + 1;
            int zStep = (int)(zDiff / step) + 1;


            double limit = 0.75 * (maxPt.Y - minPt.Y) + minPt.Y;

            PointCloud splitY = lptCloud.GetAboveY(limit).ToPointCloud();
            Point3D avgPt = splitY.Average;

            PointCloud splitRight = splitY.GetPointAboveX(avgPt.X - Para.HeadSplitOverlap);
            PointCloud splitLeft = splitY.GetPointBelowX(avgPt.X + Para.HeadSplitOverlap);

            icp.Match(splitRight);
            Matrix4x4 alignRight = icp.AlignMatrix;
            Point3D minRight = splitRight.Min;
            Point3D maxRight = splitRight.Max;
            double circleRight = maxRight.X - minRight.X >= maxRight.Y - minRight.Y ? maxRight.Y - minRight.Y : maxRight.X - minRight.X;
            double maxDsRight = splitRight.GetMaxDistanceAtXY(minRight);

            icp.Match(splitLeft);
            Matrix4x4 alignLeft = icp.AlignMatrix;
            Point3D maxLeft = splitLeft.Max;
            Point3D minLeft = splitLeft.Min;
            double circleLeft = maxLeft.X - minLeft.X >= maxLeft.Y - minLeft.Y ? maxLeft.Y - minLeft.Y : maxLeft.X - minLeft.X;

            Point3D rightBottom = new Point3D(maxLeft.X, minLeft.Y, 0.0);
            double maxDsLeft = splitLeft.GetMaxDistanceAtXY(maxLeft);

            double[,] mapRx = new double[xStep, yStep];
            double[,] mapRy = new double[xStep, yStep];
            double[,] mapRz = new double[xStep, yStep];

            double[,] mapTx = new double[xStep, yStep];
            double[,] mapTy = new double[xStep, yStep];
            double[,] mapTz = new double[xStep, yStep];

            double[,] mapSx = new double[xStep, yStep];
            double[,] mapSy = new double[xStep, yStep];
            double[,] mapSz = new double[xStep, yStep];

#if m
                            Vector3D[,,] volumnVector = new Vector3D[xStep, yStep, zStep];
#endif

            for (int i = 0; i < xStep; i++)
            {
                for (int j = 0; j < yStep; j++)
                {
                    double x = i * step + (minPt.X - Para.MapExtendLength);
                    double y = j * step + (minPt.Y - Para.MapExtendLength);
                    if (y >= limit)
                    {
                        if (x >= avgPt.X - Para.HeadSplitOverlap)
                        {
                            Point3D tempPt = new Point3D(x, y, 0);
                            double tempDis = Point3D.DistanceXY(tempPt, minRight);
                            double ratio = tempDis / maxDsRight;

                            if (ratio > 1) ratio = 1;

                            RotateRigidBody.SolveRzRyRx(alignRight, out RotateUnit rx, out RotateUnit ry, out RotateUnit rz);
                            CoordMatrix.SolveTzTyTx(alignRight, out Shift shift);
                            mapRx[i, j] = ratio * rx.RotateAngle;
                            mapRy[i, j] = ratio * ry.RotateAngle;
                            mapRz[i, j] = ratio * rz.RotateAngle;

                            mapTx[i, j] = ratio * shift.X;
                            mapTy[i, j] = ratio * shift.Y;
                            mapTz[i, j] = ratio * shift.Z;

                        }
                        else
                        {
                            Point3D tempPt = new Point3D(x, y, 0);
                            double tempDis = Point3D.DistanceXY(tempPt, maxLeft);
                            double ratio = tempDis / maxDsLeft;

                            if (ratio > 1) ratio = 1;

                            RotateRigidBody.SolveRzRyRx(alignLeft, out RotateUnit rx, out RotateUnit ry, out RotateUnit rz);
                            CoordMatrix.SolveTzTyTx(alignLeft, out Shift shift);
                            mapRx[i, j] = ratio * rx.RotateAngle;
                            mapRy[i, j] = ratio * ry.RotateAngle;
                            mapRz[i, j] = ratio * rz.RotateAngle;

                            mapTx[i, j] = ratio * shift.X;
                            mapTy[i, j] = ratio * shift.Y;
                            mapTz[i, j] = ratio * shift.Z;
                        }
                    }
                    else
                    {
                        mapRx[i, j] = 0;
                        mapRy[i, j] = 0;
                        mapRz[i, j] = 0;
                        mapTx[i, j] = 0;
                        mapTy[i, j] = 0;
                        mapTz[i, j] = 0;

                    }
#if m
                                    for (int k = 0; k < zStep; k++)
                                    {
                                        double z = k * step + (minPt.Z - 10);
                                        Point3D targetPt = new Point3D(x + step / 2, y + step / 2, z + step / 2);
                                        PointCloud pc = PointCloudCommon.GetNearestPointCloud(scanCloud.kdTree, targetPt, step);
                                        if(pc.Count == 0)
                                        {
                                            volumnVector[i, j, k] = new Vector3D();
                                        }
                                        else
                                        {
                                            Point3D nearest = PointCloudCommon.GetNearestPoint(scanCloud.kdTree, targetPt);
                                            if( nearest.GetOption(typeof(DiffOption)) is DiffOption DiffO)
                                            {
                                                volumnVector[i, j, k] = DiffO.DiffVector.DeepClone();

                                            }
                                            else
                                            {
                                                volumnVector[i, j, k] = new Vector3D();
                                            }
                                        }
                                    }
#endif
                }
            }



            double[,] mask9 = new double[,]
            {
                                { 1,1,1},
                                { 1,1,1},
                                { 0.5,0.5,0.5}
            };
            double[,] mask25 = new double[,]
            {
                                { 1.0,1.0,1.0,1.0,1.0},
                                { 1.0,1.0,1.0,1.0,1.0},
                                { 1.0,1.0,1.0,1.0,1.0},
                                { 1.0,1.0,1.0,1.0,1.0},
                                { 1.0,1.0,1.0,1.0,1.0},
            };
            int testLength = mask25.GetTotalLength();
            for (int i = 2; i < xStep - 2; i++)
            {
                for (int j = 2; j < yStep - 2; j++)
                {
                    mapRx[i, j] = calAvg25(mapRx, i, j, mask25);
                    mapRy[i, j] = calAvg25(mapRy, i, j, mask25);
                    mapRz[i, j] = calAvg25(mapRz, i, j, mask25);
                    mapTx[i, j] = calAvg25(mapTx, i, j, mask25);
                    mapTy[i, j] = calAvg25(mapTy, i, j, mask25);
                    mapTz[i, j] = calAvg25(mapTz, i, j, mask25);
                }
            }
#if m
                            PointCloud output = new PointCloud();
                            for (int i = 0; i < loadedPCloud.Count; i++)
                            {
                                Point3D pt = loadedPCloud.Points[i];
                                int x =(int)( (pt.X - minPt.X + 20) / step);
                                int y = (int)( (pt.Y - minPt.Y + 20) / step);

                                CoordMatrix cm = new CoordMatrix();
                                cm.AddSeq(eRefAxis.Z, eMatrixType.Rotate, mapRz[x, y]/180*Math.PI);
                                cm.AddSeq(eRefAxis.Y, eMatrixType.Rotate, mapRy[x, y] / 180 * Math.PI);
                                cm.AddSeq(eRefAxis.X, eMatrixType.Rotate, mapRx[x, y] / 180 * Math.PI);

                                cm.AddSeq(eRefAxis.Z, eMatrixType.Translation, mapTz[x, y]);
                                cm.AddSeq(eRefAxis.Y, eMatrixType.Translation, mapTy[x, y]);
                                cm.AddSeq(eRefAxis.X, eMatrixType.Translation, mapTx[x, y]);

                                cm.EndAddMatrix();

                                Point3D newPt = pt.Multiply(cm.FinalMatrix4);
                                output.Add(newPt);
                            }
                            output.Save(modelAfterAdjust1);
#endif
            //og.SaveOPT(pathBeforeAdjust);
#if m
                            for (int i = 0; i < output.Count; i++)
                            {
                                Point3D pt = output.Points[i];
                                PointCloud nearCloud=  PointCloudCommon.GetNearestPointCloud(scanCloud.kdTree, pt, 5.0);
                                if(nearCloud != null)
                                {
                                    if(nearCloud.Count > 0)
                                    {
                                        double HighZ = nearCloud.Max.Z;
                                        double diffZ = HighZ - pt.Z;

                                        int x = (int)((pt.X - minPt.X + 20) / step);
                                        int y = (int)((pt.Y - minPt.Y + 20) / step);
                                        if(Math.Abs(diffZ) > mapTz2[x, y])
                                            mapTz2[x, y] = diffZ;
                                        //mapTz[x, y] += diffZ;
                                        //mapTz[x, y] /= 2;
                                    }
                                }
                            }
#endif
            _adjustModel = new PointCloud();
            for (int i = 0; i < modelCloud.Count; i++)
            {
                Point3D pt = modelCloud.Points[i];
                int x = (int)((pt.X - minPt.X + Para.MapExtendLength) / step);
                int y = (int)((pt.Y - minPt.Y + Para.MapExtendLength) / step);

                CoordMatrix cm = new CoordMatrix();
                cm.AddSeq(eRefAxis.Z, eMatrixType.Rotate, mapRz[x, y] / 180 * Math.PI);
                cm.AddSeq(eRefAxis.Y, eMatrixType.Rotate, mapRy[x, y] / 180 * Math.PI);
                cm.AddSeq(eRefAxis.X, eMatrixType.Rotate, mapRx[x, y] / 180 * Math.PI);

                cm.AddSeq(eRefAxis.Z, eMatrixType.Translation, mapTz[x, y]);
                cm.AddSeq(eRefAxis.Y, eMatrixType.Translation, mapTy[x, y]);
                cm.AddSeq(eRefAxis.X, eMatrixType.Translation, mapTx[x, y]);

                cm.EndAddMatrix();

                Point3D newPt = pt.Multiply(cm.FinalMatrix4);
                _adjustModel.Add(newPt, true);
            }
            _adjustPath = new ObjectGroup("AdjustPath");
            foreach (var item in sipingPath.Objects)
            {
                if (item.Value is Polyline pl)
                {
                    Polyline smoothPL = new Polyline();
                    Point3D pStart = pl.GetFirstPoint();
                    Point3D pEnd = pl.GetLastPoint();

                    int xStart = (int)((pStart.X - minPt.X + Para.MapExtendLength) / step);
                    int yStart = (int)((pStart.Y - minPt.Y + Para.MapExtendLength) / step);

                    int xEnd = (int)((pEnd.X - minPt.X + Para.MapExtendLength) / step);
                    int yEnd = (int)((pEnd.Y - minPt.Y + Para.MapExtendLength) / step);

                    double maxLengthXY = Point3D.DistanceXY(pStart, pEnd);
                    for (int i = 0; i < pl.Count; i++)
                    {
                        Point3D pt = pl.Points[i];  
                        int x = (int)((pt.X - minPt.X + Para.MapExtendLength) / step);
                        int y = (int)((pt.Y - minPt.Y + Para.MapExtendLength) / step);
                        double tempLengthXY = Point3D.DistanceXY(pStart, pt);

                        double rz = tempLengthXY / maxLengthXY * (mapRz[xEnd, yEnd] - mapRz[xStart, yStart]) + mapRz[xStart, yStart];
                        double ry = tempLengthXY / maxLengthXY * (mapRy[xEnd, yEnd] - mapRy[xStart, yStart]) + mapRy[xStart, yStart];
                        double rx = tempLengthXY / maxLengthXY * (mapRx[xEnd, yEnd] - mapRx[xStart, yStart]) + mapRx[xStart, yStart];
                        double tz = tempLengthXY / maxLengthXY * (mapTz[xEnd, yEnd] - mapTz[xStart, yStart]) + mapTz[xStart, yStart];
                        double ty = tempLengthXY / maxLengthXY * (mapTy[xEnd, yEnd] - mapTy[xStart, yStart]) + mapTy[xStart, yStart];
                        double tx = tempLengthXY / maxLengthXY * (mapTx[xEnd, yEnd] - mapTx[xStart, yStart]) + mapTx[xStart, yStart];
                        CoordMatrix cm = new CoordMatrix();
                        cm.AddSeq(eRefAxis.Z, eMatrixType.Rotate, rz / 180 * Math.PI);
                        cm.AddSeq(eRefAxis.Y, eMatrixType.Rotate, ry / 180 * Math.PI);
                        cm.AddSeq(eRefAxis.X, eMatrixType.Rotate, rx / 180 * Math.PI);

                        cm.AddSeq(eRefAxis.Z, eMatrixType.Translation, tz);
                        cm.AddSeq(eRefAxis.Y, eMatrixType.Translation, ty);
                        cm.AddSeq(eRefAxis.X, eMatrixType.Translation, tx);
                        cm.EndAddMatrix();

                        Point3D newPt = pt.Multiply(cm.FinalMatrix4);
                        smoothPL.Add(newPt);

                    }
                    smoothPL.SmoothPath_3P(true, true, false, 2.0, 1.0, 2.0);
                    _adjustPath.Add($"Adjust{item.Key}", smoothPL);
                }
            }
            return true;
        }

        public PointCloud GetAdjustModel() => _adjustModel;

        public ObjectGroup GetAdjustPath() => _adjustPath;
        double calAvg9(double[,] target, int i, int j, double[,] mask)
        {

            double avg = (target[i - 1, j - 1] * mask[0, 0] +
                target[i, j - 1] * mask[0, 1] +
                target[i + 1, j - 1] * mask[0, 2] +
                target[i - 1, j] * mask[1, 0] +
                target[i, j] * mask[1, 1] +
                target[i + 1, j] * mask[1, 2] +
                target[i - 1, j + 1] * mask[2, 0] +
                target[i, j + 1] * mask[2, 1] +
                target[i + 1, j + 1] * mask[2, 2]) /
                (mask[0, 0] +
                mask[0, 1] +
                mask[0, 2] +
                mask[1, 0] +
                mask[1, 1] +
                mask[1, 2] +
                mask[2, 0] +
                mask[2, 1] +
                mask[2, 2]);
            return avg;
        }
        double calAvg25(double[,] target, int i, int j, double[,] mask)
        {

            double avg = (target[i - 2, j - 2] * mask[0, 0] +
                target[i - 2, j - 1] * mask[0, 1] +
                target[i - 2, j] * mask[0, 2] +
                target[i - 2, j + 1] * mask[0, 3] +
                target[i - 2, j + 2] * mask[0, 4] +

                 target[i - 1, j - 2] * mask[1, 0] +
                target[i - 1, j - 1] * mask[1, 1] +
                target[i - 1, j] * mask[1, 2] +
                target[i - 1, j + 1] * mask[1, 3] +
                target[i - 1, j + 2] * mask[1, 4] +

                 target[i, j - 2] * mask[2, 0] +
                target[i, j - 1] * mask[2, 1] +
                target[i, j] * mask[2, 2] +
                target[i, j + 1] * mask[2, 3] +
                target[i, j + 2] * mask[2, 4] +

                target[i + 1, j - 2] * mask[3, 0] +
                target[i + 1, j - 1] * mask[3, 1] +
                target[i + 1, j] * mask[3, 2] +
                target[i + 1, j + 1] * mask[3, 3] +
                target[i + 1, j + 2] * mask[3, 3] +

                target[i + 2, j - 2] * mask[4, 0] +
                target[i + 2, j - 1] * mask[4, 1] +
                target[i + 2, j] * mask[4, 2] +
                target[i + 2, j + 1] * mask[4, 3] +
                target[i + 2, j + 2] * mask[4, 4]) /
                (
                mask[0, 0] +
                mask[0, 1] +
                mask[0, 2] +
                mask[0, 3] +
                mask[0, 4] +
                mask[1, 0] +
                mask[1, 1] +
                mask[1, 2] +
                mask[1, 3] +
                mask[1, 4] +
                mask[2, 0] +
                mask[2, 1] +
                mask[2, 2] +
                mask[2, 3] +
                mask[2, 4] +
                mask[3, 0] +
                mask[3, 1] +
                mask[3, 2] +
                mask[3, 3] +
                mask[3, 4] +
                mask[4, 0] +
                mask[4, 1] +
                mask[4, 2] +
                mask[4, 3] +
                mask[4, 4]
                );
            return avg;
        }

        public Control GetParameterControl() => _propertyGrid;
    }
    public class Head2ndAlignParameter
    {
        double _MapExtendLength = 10;
        public double MapExtendLength 
        {
            get => _MapExtendLength;
            set
            {
                _MapExtendLength = Math.Abs(value);
            }
        }
        double _MapStep = 1.0;
        public double MapStep
        {
            get => _MapStep;
            set
            {
                if(value <=0.0)
                {
                    _MapStep = 0.1;
                }
                else
                {
                    _MapStep = value;
                }
            }
        }
        double _HeadSplitOverlap = 10.0;
        public double HeadSplitOverlap
        {
            get => _HeadSplitOverlap;
            set
            {
                if(value <=0.0)
                {
                    _HeadSplitOverlap = 0.1;
                }
                else
                {
                    _HeadSplitOverlap = value;
                }
            }
        }

    }

}

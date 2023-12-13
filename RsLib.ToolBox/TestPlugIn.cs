using Accord.Math;
using RsLib.Display3D;
using RsLib.PointCloudLib;
using System;
using System.Collections.Generic;
using System.IO;
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
        public Head2ndAlignParameter Para = new Head2ndAlignParameter();
        public Head2ndAlign() 
        {
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

            PointCloud splitRight = splitY.GetPointAboveX(avgPt.X);
            PointCloud splitLeft = splitY.GetPointBelowX(avgPt.X);

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
            double maxDsLeft = splitLeft.GetMaxDistanceAtXY(rightBottom);

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

            double minRatioR = double.MaxValue;
            double minRatioL = double.MaxValue;
            for (int i = 0; i < xStep; i++)
            {
                for (int j = 0; j < yStep; j++)
                {
                    double x = i * step + (minPt.X - Para.MapExtendLength);
                    double y = j * step + (minPt.Y - Para.MapExtendLength);
                    if (y >= limit)
                    {
                        if (x >= avgPt.X)
                        {
                            Point3D tempPt = new Point3D(x, y, 0);
                            double tempDis = Point3D.DistanceXY(tempPt, minRight);
                            double ratio = tempDis / maxDsRight;

                            if (ratio > 1) ratio = 1;
                            if(minRatioR >ratio) 
                                minRatioR = ratio;
                            RotateRigidBody.SolveRzRyRx(alignRight, out RotateUnit rx, out RotateUnit ry, out RotateUnit rz);
                            CoordMatrix.SolveTzTyTx(alignRight, out Shift shift);
                            mapRx[i, j] =Math.Round( ratio * rx.RotateAngle,2);
                            mapRy[i, j] = Math.Round(ratio * ry.RotateAngle,2);
                            mapRz[i, j] = Math.Round(ratio * rz.RotateAngle,2);

                            mapTx[i, j] = Math.Round(ratio * shift.X, 2);
                            mapTy[i, j] = Math.Round(ratio * shift.Y, 2);
                            mapTz[i, j] = Math.Round(ratio * shift.Z, 2);

                        }
                        else
                        {
                            Point3D tempPt = new Point3D(x, y, 0);
                            double tempDis = Point3D.DistanceXY(tempPt, rightBottom);
                            double ratio = tempDis / maxDsLeft;

                            if (ratio > 1) ratio = 1;
                            if (minRatioL > ratio) 
                                minRatioL = ratio;

                            RotateRigidBody.SolveRzRyRx(alignLeft, out RotateUnit rx, out RotateUnit ry, out RotateUnit rz);
                            CoordMatrix.SolveTzTyTx(alignLeft, out Shift shift);
                            mapRx[i, j] = Math.Round(ratio * rx.RotateAngle, 2);
                            mapRy[i, j] = Math.Round(ratio * ry.RotateAngle, 2);
                            mapRz[i, j] = Math.Round(ratio * rz.RotateAngle, 2);

                            mapTx[i, j] = Math.Round(ratio * shift.X, 2);
                            mapTy[i, j] = Math.Round(ratio * shift.Y, 2);
                            mapTz[i, j] = Math.Round(ratio * shift.Z, 2);
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
            WriteMap("d:\\Rx.txt", mapRx);
            WriteMap("d:\\Ry.txt", mapRy);
            WriteMap("d:\\Rz.txt", mapRz);
            WriteMap("d:\\Tx.txt", mapTx);
            WriteMap("d:\\Ty.txt", mapTy);
            WriteMap("d:\\Tz.txt", mapTz);


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

            for (int k = 0; k < Para.MapSmoothTime; k++)
            {
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
            }
            WriteMap("d:\\Rx2.txt", mapRx);
            WriteMap("d:\\Ry2.txt", mapRy);
            WriteMap("d:\\Rz2.txt", mapRz);
            WriteMap("d:\\Tx2.txt", mapTx);
            WriteMap("d:\\Ty2.txt", mapTy);
            WriteMap("d:\\Tz2.txt", mapTz);
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

                    double diffRz = mapRz[xEnd, yEnd] - mapRz[xStart, yStart];
                    double diffRy = mapRy[xEnd, yEnd] - mapRy[xStart, yStart];
                    double diffRx = mapRx[xEnd, yEnd] - mapRx[xStart, yStart];
                    double diffTz = mapTz[xEnd, yEnd] - mapTz[xStart, yStart];
                    double diffTy = mapTy[xEnd, yEnd] - mapTy[xStart, yStart];
                    double diffTx = mapTx[xEnd, yEnd] - mapTx[xStart, yStart];
                    double startRz =  mapRz[xStart, yStart];
                    double startRy = mapRy[xStart, yStart];
                    double startRx = mapRx[xStart, yStart];
                    double startTz = mapTz[xStart, yStart];
                    double startTy = mapTy[xStart, yStart];
                    double startTx = mapTx[xStart, yStart];
                    for (int i = 0; i < pl.Count; i++)
                    {
                        Point3D pt = pl.Points[i];  
                        int x = (int)Math.Round(((pt.X - minPt.X + Para.MapExtendLength) / step),0);
                        int y = (int)Math.Round(((pt.Y - minPt.Y + Para.MapExtendLength) / step),0);
                        double tempLengthXY = Point3D.DistanceXY(pStart, pt);
                        double lengthRatio = tempLengthXY / maxLengthXY;
                        //double rz = lengthRatio * diffRz + startRz;
                        //double ry = lengthRatio * diffRy + startRy;
                        //double rx = lengthRatio * diffRx + startRx;
                        //double tz = lengthRatio * diffTz + startTz;
                        //double ty = lengthRatio * diffTy + startTy;
                        //double tx = lengthRatio * diffTx + startTx;

                        double rz = mapRz[x, y];
                        double ry = mapRy[x, y];
                        double rx = mapRx[x, y];
                        double tz = mapTz[x, y];
                        double ty = mapTy[x, y];
                        double tx = mapTx[x, y];

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
                    smoothPL.SmoothPath_3P(true, true, true, 1.0, 1.0, 1.0);
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
            return Math.Round(avg,2);
        }
        void WriteMap(string filePath, double[,] map)
        {
            using (StreamWriter sw = new StreamWriter(filePath,false,Encoding.Default))
            {
                for (int i = 0; i < map.GetLength(0); i++)
                {
                    string row = "";
                    for (int j = 0; j < map.GetLength(1); j++)
                    {
                        if (j != map.GetLength(1) - 1)
                        {
                            row += (map[i, j].ToString() + ",");
                        }
                        else
                        {
                            row += (map[i, j].ToString());
                        }
                    }
                    sw.WriteLine(row);
                }
            }

        }
    }
    public class Head2ndAlignParameter
    {

        uint _MapSmoothTime = 10;
        public uint MapSmoothTime
        {
            get => _MapSmoothTime;
            set
            {
                if (value <= 0) _MapSmoothTime = 1;
                else _MapSmoothTime = value;
            }
        }
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

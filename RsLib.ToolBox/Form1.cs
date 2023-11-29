using Accord.Math;
using RsLib.Common;
using RsLib.Display3D;
using RsLib.LogMgr;
using RsLib.PointCloudLib;
using RsLib.PointCloudLib.CalculateMatrix;
using RsLib.SerialPortLib;
using RsLib.TCP.Control;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Tab;

namespace RsLib.DemoForm
{
    public partial class Form1 : Form
    {
        TCPServerControl serverControl = new TCPServerControl();
        TCPClientControl clientControl = new TCPClientControl();
        LogControl logControl = new LogControl();
        Display3DControl displayControl = new Display3DControl(4);
        ZoomImageControl zoomCtrl = new ZoomImageControl();
        ZoomImageControl zoom1Ctrl = new ZoomImageControl();

        EJ1500 _EJ1500 = new EJ1500(0);
        EJ1500Control eJ1500Ctrl;
        TransMatrixControl transMatrixControl = new TransMatrixControl();
        public Form1()
        {
            InitializeComponent();
            //_EJ1500.LoadYaml("d:\\testEj1500.yaml");
            eJ1500Ctrl = new EJ1500Control(_EJ1500);
            eJ1500Ctrl.Dock = DockStyle.Fill;
            tabPage_EJ1500.Controls.Add(eJ1500Ctrl);

            Log.EnableUpdateUI = false;
            Log.Start();
            serverControl.Dock = DockStyle.Fill;
            pnl_TCPServer.Controls.Add(serverControl);
            clientControl.Dock = DockStyle.Fill;
            pnl_TCPClient.Controls.Add(clientControl);

            displayControl.Dock = DockStyle.Fill;
            tabPage_XYZView.Controls.Add(displayControl);
            logControl.Dock = DockStyle.Fill;
            tableLayoutPanel1.Controls.Add(logControl, 0, 1);
            zoomCtrl.Dock = DockStyle.Fill;
            zoom1Ctrl.Dock = DockStyle.Fill;

            tableLayoutPanel2.Controls.Add(zoomCtrl, 0, 0);
            tableLayoutPanel2.Controls.Add(zoom1Ctrl, 1, 0);
            transMatrixControl.Dock = DockStyle.Fill;
            panel1.Controls.Add(transMatrixControl);
            double test = 70;
            double tt = Math.Round(test / 100, 2);
            label1.Text = tt.ToString("F2");
            Log.EnableUpdateUI = true;
            comboBox1.AddEnumItems(typeof(LogControl));
            //ThreadPool.QueueUserWorkItem(new WaitCallback(writeTxt));
            this.MouseMove += Form1_MouseMove;

            //ThreadPool.QueueUserWorkItem(traceTd);
            //ThreadPool.QueueUserWorkItem(infoTd);
            //ThreadPool.QueueUserWorkItem(warnTd);
            //ThreadPool.QueueUserWorkItem(alarmTd);

            ColorGradient cg = new ColorGradient(0, 3);
            cg.ColorControl.Dock = DockStyle.Fill;
            panel2.Controls.Add(cg.ColorControl);
            Log.Add("Start", MsgLevel.Info);
        }
        void traceTd(object obj)
        {
            while (true)
            {
                Log.Add($" {Thread.CurrentThread.ManagedThreadId} - Trace", MsgLevel.Trace);
                SpinWait.SpinUntil(() => false, 500);
            }
        }
        void infoTd(object obj)
        {
            while (true)
            {
                Log.Add($" {Thread.CurrentThread.ManagedThreadId} - Info", MsgLevel.Info);
                SpinWait.SpinUntil(() => false, 600);
            }
        }
        void warnTd(object obj)
        {
            while (true)
            {
                Log.Add($" {Thread.CurrentThread.ManagedThreadId} - warn", MsgLevel.Warn);
                SpinWait.SpinUntil(() => false, 250);
            }
        }
        void alarmTd(object obj)
        {
            while (true)
            {

                Log.Add($" {Thread.CurrentThread.ManagedThreadId} - alarm", MsgLevel.Alarm);
                SpinWait.SpinUntil(() => false, 300);
            }
        }

        private void Zoom1Ctrl_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Middle)
            {
                zoomCtrl.ResetView();
            }
        }

        private void ZoomCtrl_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Middle)
            {
                zoom1Ctrl.ResetView();
            }
        }

        private void Zoom1Ctrl_MouseWheel(object sender, MouseEventArgs e)
        {
            zoomCtrl.Zoom = zoom1Ctrl.Zoom;
            zoomCtrl.Pan = zoom1Ctrl.Pan;
        }

        private void Zoom1Ctrl_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                zoomCtrl.Zoom = zoom1Ctrl.Zoom;
                zoomCtrl.Pan = zoom1Ctrl.Pan;
            }
        }

        private void ZoomCtrl_MouseWheel(object sender, MouseEventArgs e)
        {
            zoom1Ctrl.Zoom = zoomCtrl.Zoom;
            zoom1Ctrl.Pan = zoomCtrl.Pan;
        }

        private void ZoomCtrl_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                zoom1Ctrl.Zoom = zoomCtrl.Zoom;
                zoom1Ctrl.Pan = zoomCtrl.Pan;
            }
        }



        bool _isDragTool = false;
        private void Form1_MouseMove(object sender, MouseEventArgs e)
        {
            if (_isDragTool)
            {
            }
        }



        void writeTxt(object obj)
        {
            using (StreamWriter sw = new StreamWriter("d:\\test.txt", true, Encoding.Default))
            {
                while (true)
                {
                    sw.WriteLine($"{DateTime.Now.ToShortTimeString()}\n");
                    sw.Flush();
                    SpinWait.SpinUntil(() => false, 2000);
                }
            }
        }


        private void timer1_Tick(object sender, EventArgs e)
        {
            if (eJ1500 == null)
            {
                button2.BackColor = Color.Red;

                return;
            }
            button2.BackColor = eJ1500.IsConnected ? Color.LimeGreen : Color.Red;
        }
        EJ1500 eJ1500;
        private void button2_Click(object sender, EventArgs e)
        {
            eJ1500 = new EJ1500(1);
            eJ1500.WeightMeasured += EJ1500_WeightMeasured;
            eJ1500.Connect();
            listBox1.Items.Clear();
        }

        private void EJ1500_WeightMeasured(int index, double obj,bool isRaiseEvent)
        {
            if (this.InvokeRequired)
            {
                Action<int, double,bool> action = new Action<int, double,bool>(EJ1500_WeightMeasured);
                this.Invoke(action, index, obj, isRaiseEvent);
            }
            else
            {
                listBox1.Items.Add(obj);
            }
        }



        private void button3_Click(object sender, EventArgs e)
        {
            eJ1500.Disconnect();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            eJ1500.Measure(false);
        }

        private void button5_Click(object sender, EventArgs e)
        {


        }

        private void button6_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog op = new OpenFileDialog())
            {
                op.Filter = "XYZ file|*.xyz";
                if(op.ShowDialog() == DialogResult.OK)
                {

                    Log.Add("Load Start", MsgLevel.Info);
                    PointCloud pCloud = new PointCloud();
                    pCloud.LoadFromFile(op.FileName,true);
                    LayerPointCloud lPt = new LayerPointCloud(pCloud, false, 1.0);
                    Log.Add("Load End", MsgLevel.Info);

                    lPt.Save("D:\\testSlice.xyz");
                    Log.Add("Save End", MsgLevel.Info);
                    Log.Add("Find Start", MsgLevel.Info);

                    Test(lPt);
                    Log.Add("Find END", MsgLevel.Info);

                }
            }



        }
        private PointCloud Head = new PointCloud();
        private LayerPointCloud Bodys = new LayerPointCloud();
        private PointCloud Tail = new PointCloud();
        private Polyline Edge = new Polyline();
        private int ResampleCount = 2;
        public void Test(LayerPointCloud Target)
        {
            Point3D MaxY = Target.Max;
            double TargetMaxY = MaxY.Y - 15;
            double TargetMinY = Target.Min.Y + 15;
            for (int i = 0; i < Target.LayerCount; i++)
            {
                PointCloud ReduceCloud = new PointCloud();
                Point3D tempp = Target.Layers[i].Points[0];
                if (tempp.Y >= TargetMaxY || tempp.Y <= TargetMinY) continue;

                for (int j = 0; j < Target.Layers[i].Count; j++)
                {
                    if (j > 10 && j < Target.Layers[i].Count - 10)
                    {
                        if (j % (ResampleCount + 1) == 0)
                        {
                            ReduceCloud.Add(Target.Layers[i].Points[j]);
                        }
                    }
                    else
                    {
                        ReduceCloud.Add(Target.Layers[i].Points[j]);
                    }
                }

                //Target.Layers[i].Clear();
                Target.Layers[i] = ReduceCloud;
            }


            Head = Target.GetAboveYtoPointCloud(TargetMaxY);
            Tail = Target.GetBelowYtoPointCloud(TargetMinY);
            Bodys = Target.GetInsideRangeY(TargetMinY, TargetMaxY);

            Target.Clear();
            Target.Add(Tail);
            Target.Add(Bodys);

            LayerPointCloud LHead = new LayerPointCloud(Head, 3, 1.0, true);
            PointCloud tmpHead = new PointCloud();
            int ChangeMax = 0;
            for (int i = 0; i < LHead.LayerCount; i++)
            {
                if (LHead.Layers[i].Count > 50)
                {
                    ChangeMax = i;
                    break;
                }
            }
            for (int i = ChangeMax; i < LHead.LayerCount; i++)
            {
                tmpHead.Add(LHead.Layers[i]);
                Target.Add(LHead.Layers[i]);
            }

            Head = tmpHead;

            FindBiteline();
        }
        public void FindBiteline()
        {
            Edge.Clear();
            #region Find Biteline

            #region Body
            PointCloud FoundBody1 = new PointCloud();
            PointCloud FoundBody2 = new PointCloud();
            for (int i = 0; i < Bodys.Layers.Count; i++)
            {
                PointCloud Body1 = new PointCloud();
                PointCloud Body2 = new PointCloud();

                for (int j = 0; j < Bodys.Layers[i].Count / 3; j++)
                {
                    Body1.Add(Bodys.Layers[i].Points[j]);
                }
                for (int j = Bodys.Layers[i].Count - 1; j >= Bodys.Layers[i].Count / 3 * 2; j--)
                {
                    Body2.Add(Bodys.Layers[i].Points[j]);
                }
                FoundBody1.Add(Body1.GetMaxZPoint(Bodys.Layers[i].Points[Bodys.Layers[i].Count - 1]));
                FoundBody2.Add(Body2.GetMaxZPoint(Bodys.Layers[i].Points[0]));

            }
            FoundBody1.Save("d:\\B1.xyz");
            FoundBody2.Save("d:\\B2.xyz");

            #endregion
            #region Tail
            PointCloud FoundTail = Tail.GetEdge(Tail.Min, Tail.Max, true, 10, true);
            Point3D BD1MinY = FoundBody1.GetMinYPoint();
            Point3D BD2MinY = FoundBody2.GetMinYPoint();

            double X1 = Math.Max(BD1MinY.X, BD2MinY.X);
            double X2 = Math.Min(BD1MinY.X, BD2MinY.X);
            FoundTail.ReduceOutsideX(X1, X2);
            //FoundTail.ReduceAboveX(X1);
            //FoundTail.ReduceBelowX(X2);

            #endregion
            #region Head


            PointCloud FoundHead = Head.GetEdge(Head.Min, Head.Max, true, 10, true);
            Point3D BD1MaxY = FoundBody1.GetMaxYPoint();
            Point3D BD2MaxY = FoundBody2.GetMaxYPoint();

            X1 = Math.Max(BD1MaxY.X, BD2MaxY.X);
            X2 = Math.Min(BD1MaxY.X, BD2MaxY.X);
            FoundHead.ReduceOutsideX(X1, X2);
            //FoundHead.ReduceAboveX(X1);
            //FoundHead.ReduceBelowX(X2);
            #endregion
            //FoundBody.Save(op.FileName.Replace(".txt", "_Body.xyz"));

            Polyline LineBody1 = new Polyline();
            Polyline LineBody2 = new Polyline();
            Polyline LineHead = new Polyline();
            Polyline LineTail = new Polyline();

            double SmoothRadius = 4.0;
            double SearchRadius = 3.0;
            LineBody1 = FoundBody1.SortCloudByKDTree(SearchRadius, BD1MaxY, BD1MinY, SmoothRadius, false);
            LineBody2 = FoundBody2.SortCloudByKDTree(SearchRadius, BD2MinY, BD2MaxY, SmoothRadius, false);
            LineHead = FoundHead.SortCloudByKDTree(SearchRadius, FoundHead.GetMaxXPoint(), FoundHead.GetMinXPoint(), SmoothRadius, false);
            LineTail = FoundTail.SortCloudByKDTree(SearchRadius, FoundTail.GetMinXPoint(), FoundTail.GetMaxXPoint(), SmoothRadius, false);
            //LineBody1 = FoundBody1.SortCloudByKDTree(SearchRadius, FoundBody1.GetMaxYPoint(), SmoothRadius, false);
            //LineBody2 = FoundBody2.SortCloudByKDTree(SearchRadius, FoundBody2.GetMinYPoint(), SmoothRadius, false);
            //LineHead = FoundHead.SortCloudByKDTree(SearchRadius, FoundHead.GetMaxXPoint(), SmoothRadius, false);
            //LineTail = FoundTail.SortCloudByKDTree(SearchRadius, FoundTail.GetMinXPoint(), SmoothRadius, false);
            Polyline Output = new Polyline();

            Output.Add(LineBody1);
            Output.Add(LineTail);
            Output.Add(LineBody2);
            Output.Add(LineHead);
            Output.Add(LineBody1.Points[0]);

            Edge.StartAddNotDuplicate();
            for (int i = 0; i < Output.Count; i++)
            {
                Edge.AddNotDuplicate(Output.Points[i], 2.0);
            }
            Edge.EndAddNotDuplicate();

            Edge.SaveAsOpt("D:\\TestSlice.opt");
            #endregion
        }

        private void button7_Click(object sender, EventArgs e)
        {
            ICPMatch icp = new ICPMatch();
            using (OpenFileDialog op = new OpenFileDialog())
            {
                op.Filter = "PLY file|*.ply";
                if(op.ShowDialog() == DialogResult.OK)
                {
                    string scandataPath = op.FileName;
                    icp.SetModel(scandataPath);
                    if(op.ShowDialog() == DialogResult.OK)
                    {
                        string modelPath = op.FileName;
                        op.Filter = "OPT file|*.opt";
                        if(op.ShowDialog() == DialogResult.OK)
                        {
                            string optFilePath = op.FileName;

                            ObjectGroup og = new ObjectGroup("LoadedPath");
                            og.LoadMultiPathOPT(optFilePath,false);


                            PointCloud loadedPCloud = new PointCloud();
                            loadedPCloud.LoadFromPLY(modelPath, false);


                            icp.Match(loadedPCloud);
                            icp.SaveAlignTarget("d:\\model.xyz");
                            Matrix4x4 alignModel = icp.AlignMatrix;
                            icp.SaveTransformMatrix("d:\\AlignModel.m44");
                            ObjectGroup transformOg = og.Multiply(alignModel);
                            transformOg.SaveOPT("d:\\alignPath.txt");
                            PointCloud alignPCloud = icp.GetAlignedPointCloud();
                            LayerPointCloud lptCloud = new LayerPointCloud(alignPCloud, false, 0.3);
                            Point3D maxPt = lptCloud.Max;
                            Point3D minPt = lptCloud.Min;
                            double xDiff = (maxPt.X + 20) - (minPt.X-20);
                            double yDiff = (maxPt.Y + 20) - (minPt.Y-20);
                            double step = 5.0;

                            int xStep = (int)(xDiff/ step)+1;
                            int yStep = (int)(yDiff / step)+1;

                            double limit = 0.75 * (maxPt.Y - minPt.Y) + minPt.Y;

                            PointCloud splitY = lptCloud.GetAboveY(limit).ToPointCloud();
                            Point3D avgPt = splitY.Average;

                            PointCloud splitRight = splitY.GetPointAboveX(avgPt.X-10);
                            PointCloud splitLeft = splitY.GetPointBelowX(avgPt.X-10);

                            icp.Match(splitRight);
                            icp.SaveAlignTarget("d:\\splitRight.xyz");
                            Matrix4x4 alignRight = icp.AlignMatrix;
                            icp.SaveTransformMatrix("d:\\AlignSplitRight.m44");
                            Point3D minRight = splitRight.Min;
                            double maxDsRight = splitRight.GetMaxDistanceAtXY(avgPt);
                            PointCloud finalRight = CalGradientTransform(splitRight, avgPt, icp.AlignMatrix);
                            finalRight.Save("d:\\finalRight.xyz");

                            icp.Match(splitLeft);
                            icp.SaveAlignTarget("d:\\splitLeft.xyz");
                            Matrix4x4 alignLeft = icp.AlignMatrix;
                            icp.SaveTransformMatrix("d:\\AlignSplitLeft.m44");
                            Point3D maxLeft = splitRight.Max;
                            Point3D minLeft = splitRight.Min;
                            Point3D rightBottom = new Point3D(maxLeft.X, minLeft.Y, 0.0);
                            double maxDsLeft = splitLeft.GetMaxDistanceAtXY(avgPt);

                            PointCloud finalLeft = CalGradientTransform(splitLeft, avgPt, icp.AlignMatrix);
                            finalLeft.Save("d:\\finalLeft.xyz");


                            double[,] mapRx = new double[xStep, yStep];
                            double[,] mapRy = new double[xStep, yStep];
                            double[,] mapRz = new double[xStep, yStep];

                            double[,] mapTx = new double[xStep, yStep];
                            double[,] mapTy = new double[xStep, yStep];
                            double[,] mapTz = new double[xStep, yStep];


                            for (int i = 0; i < xStep; i++)
                            {
                                for (int j = 0; j < yStep; j++)
                                {
                                    double x = i * step + (minPt.X - 20);
                                    double y = j * step + (minPt.Y - 20);
                                    if (y >= limit)
                                    {
                                        if(x>= avgPt.X)
                                        {
                                            Point3D tempPt = new Point3D(x, y, 0);
                                            double tempDis =  Point3D.DistanceXY(tempPt, avgPt);
                                            double ratio = tempDis / maxDsRight;
                                            if (ratio > 1) ratio = 1;
                                            RotateRigidBody.SolveRzRyRx(alignRight, out RotateUnit rx, out RotateUnit ry, out RotateUnit rz);
                                            CoordMatrix.SolveTzTyTx(alignRight, out Shift shift);
                                            mapRx[i, j] = ratio*rx.RotateAngle;
                                            mapRy[i, j]= ratio * ry.RotateAngle;
                                            mapRz[i,j]= ratio * rz.RotateAngle;
                                            mapTx[i, j] = ratio*shift.X;
                                            mapTy[i,j]= ratio * shift.Y;
                                            mapTz[i,j]= ratio * shift.Z;

                                        }
                                        else
                                        {
                                            Point3D tempPt = new Point3D(x, y, 0);
                                            double tempDis = Point3D.DistanceXY(tempPt, avgPt);
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
                                        mapTy[i, j] =0;
                                        mapTz[i, j] = 0;
                                    }
                                }
                            }

                            for (int i = 1; i < xStep-1; i++)
                            {
                                for (int j = 1; j < yStep-1; j++)
                                {
                                    mapRx[i, j] = calAvg9(mapRx, i, j);
                                    mapRy[i, j] = calAvg9(mapRy, i, j);
                                    mapRz[i, j] = calAvg9(mapRz, i, j);
                                    mapTx[i, j] = calAvg9(mapTx, i, j);
                                    mapTy[i, j] = calAvg9(mapTy, i, j);
                                    mapTz[i, j] = calAvg9(mapTz, i, j);
                                }
                            }
                            PointCloud output = new PointCloud();
                            for (int i = 0; i < alignPCloud.Count; i++)
                            {
                                Point3D pt = alignPCloud.Points[i];
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
                            output.Save("d:\\testSmoothOutput.xyz");


                            ObjectGroup smoothOg = new ObjectGroup("Smooth111OG");
                            foreach (var item in transformOg.Objects)
                            {
                                if(item.Value is Polyline pl)
                                {
                                    Polyline smoothPL = new Polyline();
                                    for (int i = 0; i < pl.Count; i++)
                                    {
                                        Point3D pt = pl.Points[i];
                                        int x = (int)((pt.X - minPt.X + 20) / step);
                                        int y = (int)((pt.Y - minPt.Y + 20) / step);

                                        CoordMatrix cm = new CoordMatrix();
                                        cm.AddSeq(eRefAxis.Z, eMatrixType.Rotate, mapRz[x, y] / 180 * Math.PI);
                                        cm.AddSeq(eRefAxis.Y, eMatrixType.Rotate, mapRy[x, y] / 180 * Math.PI);
                                        cm.AddSeq(eRefAxis.X, eMatrixType.Rotate, mapRx[x, y] / 180 * Math.PI);

                                        cm.AddSeq(eRefAxis.Z, eMatrixType.Translation, mapTz[x, y]);
                                        cm.AddSeq(eRefAxis.Y, eMatrixType.Translation, mapTy[x, y]);
                                        cm.AddSeq(eRefAxis.X, eMatrixType.Translation, mapTx[x, y]);

                                        cm.EndAddMatrix();

                                        Point3D newPt = pt.Multiply(cm.FinalMatrix4);
                                        smoothPL.Add(newPt);

                                    }
                                    smoothOg.Add($"SmoothOg{item.Key}", smoothPL);
                                }
                            }
                            smoothOg.SaveOPT("d:\\testSmoothOPT.txt");
                        }
                    }
                }
            }
            Log.Add("Done", MsgLevel.Info);

        }

        double calAvg9(double[,] target,int i ,int j)
        {

            double avg = (target[i - 1, j - 1] +
                target[i, j - 1] +
                target[i + 1, j - 1] +
                target[i - 1, j] +
                target[i, j] +
                target[i + 1, j] +
                target[i - 1, j + 1] +
                target[i, j + 1] +
                target[i + 1, j + 1]) / 9;
            return avg;
        }
        private PointCloud CalGradientTransform(PointCloud testCloud,Point3D basePt,Matrix4x4 alignMatrix)
        {
            RotateRigidBody.SolveRzRyRx(alignMatrix, out RotateUnit rx, out RotateUnit ry, out RotateUnit rz);
            CoordMatrix.SolveTzTyTx(alignMatrix, out Shift shift);

            double maxRight = testCloud.GetMaxDistanceAtXY(basePt);
            PointCloud finalCloud = new PointCloud();
            for (int i = 0; i < testCloud.Count; i++)
            {
                Point3D pt = testCloud.Points[i];
                double calD = Point3D.DistanceXY(pt, basePt);
                double ratio = calD / maxRight;

                if (ratio > 1) ratio = 1.0;

                RotateRigidBody rg = new RotateRigidBody();
                rg.AddRotateSeq(eRefAxis.Z, ratio * rz.RotateRadian);
                rg.AddRotateSeq(eRefAxis.Y, ratio * ry.RotateRadian);
                rg.AddRotateSeq(eRefAxis.X, ratio * rx.RotateRadian);

                rg.AddSeq(eRefAxis.X, eMatrixType.Translation, ratio * shift.X);
                rg.AddSeq(eRefAxis.Y, eMatrixType.Translation, ratio * shift.Y);
                rg.AddSeq(eRefAxis.Z, eMatrixType.Translation, ratio * shift.Z);

                rg.EndAddMatrix();

                Point3D finalPt = pt.Multiply(rg.FinalMatrix4);
                finalCloud.Add(finalPt);
            }
            return finalCloud;
        }
        private void button1_Click(object sender, EventArgs e)
        {
            // Y 90 X -45

            Vector3 vx = new Vector3(0f, 0f, -1f);
            Vector3 vy = new Vector3(-0.707f, 0.707f, 0f);
            Vector3 vz = new Vector3(0.707f, 0.707f, 0f);

            RotateAxis r = new RotateAxis(vx,vy,vz);

            //Y -90 X -45

            Vector3 vx2 = new Vector3(0f, 0f, 1f);
            Vector3 vy2 = new Vector3(0.707f, 0.707f, 0f);
            Vector3 vz2 = new Vector3(-0.707f, 0.707f, 0f);

            RotateAxis r2 = new RotateAxis(vx2, vy2, vz2);


        }
    }
}

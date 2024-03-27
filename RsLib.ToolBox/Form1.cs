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
using System.Threading.Tasks;
using System.Windows.Forms;

using System.Reflection;
using System.Linq;

namespace RsLib.DemoForm
{
    public partial class Form1 : Form
    {
        TCPServerControl serverControl = new TCPServerControl();
        TCPClientControl clientControl = new TCPClientControl();
        LogControl logControl = new LogControl();
        Display3DControl displayControl = new Display3DControl(5);
        ZoomImageControl zoomCtrl = new ZoomImageControl();
        ZoomImageControl zoom1Ctrl = new ZoomImageControl();

        EJ1500 _EJ1500 = new EJ1500(0);
        EJ1500Control eJ1500Ctrl;
        TransMatrixControl transMatrixControl = new TransMatrixControl();
        ICPAlignControl icpCtrl = new ICPAlignControl();
        Head2ndAlign icpAlign = new Head2ndAlign();

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
            splitContainer1.Panel2.Controls.Add(displayControl);
            displayControl.AddDisplayOption(new DisplayObjectOption((int)eDrawItem.ScanData, "Scan", Color.White, DisplayObjectType.PointCloud, 2.0f) );
            displayControl.AddDisplayOption(new DisplayObjectOption((int)eDrawItem.AlignModel, "AlignModel", Color.Red, DisplayObjectType.PointCloud, 2.0f));
            displayControl.AddDisplayOption(new DisplayObjectOption((int)eDrawItem.AlignPath, "AlignPath", Color.Red, DisplayObjectType.Path, 2.0f));
            displayControl.AddDisplayOption(new DisplayObjectOption((int)eDrawItem.AdjustModel, "AdjustModel", Color.Lime, DisplayObjectType.PointCloud, 2.0f));
            displayControl.AddDisplayOption(new DisplayObjectOption((int)eDrawItem.AdjustPath, "AdjustPath", Color.Lime, DisplayObjectType.Path, 2.0f));



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
            icpCtrl.Dock = DockStyle.Fill;
            tabPage_ICP.Controls.Add(icpCtrl);

            icpCtrl.AfterAligned += IcpCtrl_AfterAligned;

            ColorGradient cg = new ColorGradient(0, 3);
            cg.ColorControl.Dock = DockStyle.Fill;
            panel2.Controls.Add(cg.ColorControl);
            Log.Add("Start", MsgLevel.Info);
            propertyGrid1.SelectedObject = icpAlign.Para;
        }

        private void IcpCtrl_AfterAligned(PointCloud model, PointCloud aligned)
        {
            if (InvokeRequired)
            {
                Action<PointCloud, PointCloud> action = new Action<PointCloud, PointCloud>(IcpCtrl_AfterAligned);
                Invoke(action, model, aligned);
            }
            else
            {
                displayControl.BuildPointCloud(model, (int)eDrawItem.ScanData, false, true);
                displayControl.BuildPointCloud(aligned, (int)eDrawItem.AdjustModel, false, true);
                displayControl.UpdateDataGridView();
            }
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
            Ball b = new Ball(new Point3D(100, 50, 100), 50);
            b.ToPointCloud().Save("d:\\testBall.xyz");
            RsPlane pl = new RsPlane(new Vector3D(50, 50, 50), new Point3D(100, 50, 100));
            PointCloud pp = pl.ToPointCloud(0, 200, 0, 100, 50, 150, 0.5);
            pp.Save("D:\\testPlane.xyz");
            PointCloud pppp =  pl.Intersect(b,0.1);

            pppp.SortingPoint3DCW(b.Center);
            pppp.Save("D:\\intersect.xyz");

            Polyline ppl = new Polyline();
            for (int i = 0; i < pppp.Points.Count; i++)
            {
                PointV3D pt = new PointV3D(pppp.Points[i]);
                pt.Vz = pl.Normal;
                ppl.Add(pt);
            }
            PointV3D pt2 = new PointV3D(pppp.Points[0]);
            pt2.Vz = pl.Normal;

            ppl.Add(pt2);

            ppl.ReSample_SkipOriginalPointAndTestLast(5.0, 0.01);
            ppl.CalculatePathDirectionAsVy();

            ppl.SaveAsOpt2("d:\\Polyline.opt2");
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
                    string scanDataRight_Before = scandataPath.Replace(".ply", "_RB.xyz");
                    string scanDataLeft_Before = scandataPath.Replace(".ply", "_LB.xyz");
                    string modelAfterAdjust1 = scandataPath.Replace(".ply", "_ModelAdjust1.xyz");
                    string modelAfterAdjust2= scandataPath.Replace(".ply", "_ModelAdjust2.xyz");

                    string pathAfterAdjust = scandataPath.Replace(".ply", "_PathAdjust.txt");
                    string cloudAfterAdjust = scandataPath.Replace(".ply", "_CloudAdjust.xyz");
                    PointCloud scanCloud = new PointCloud();
                    scanCloud.LoadFromPLY(scandataPath, true);
                    if (op.ShowDialog() == DialogResult.OK)
                    {
                        string modelPath = op.FileName;
                        op.Filter = "OPT2 file|*.opt2";
                        if(op.ShowDialog() == DialogResult.OK)
                        {
                            string optFilePath = op.FileName;
                            displayControl.BuildPointCloud(scanCloud, (int)eDrawItem.ScanData, false, true);

                            ObjectGroup og = new ObjectGroup("LoadedPath");
                            og.LoadMultiPathOPT2(optFilePath,false);
                            displayControl.BuildPath(og, (int)eDrawItem.AlignPath, false, true);

                            PointCloud modelCloud = new PointCloud();
                            modelCloud.LoadFromPLY(modelPath, true);
                            displayControl.BuildPointCloud(modelCloud, (int)eDrawItem.AlignModel, false, true);


                            string mainAppDomainName = AppDomain.CurrentDomain.FriendlyName;
                            Console.WriteLine(mainAppDomainName);


                            icpAlign.AdjustPath(scanCloud, modelCloud, og);
                            ObjectGroup adjustPath = icpAlign.GetAdjustPath();
                            PointCloud adjustCloud = icpAlign.GetAdjustModel();
                            adjustPath.SaveOPT2(pathAfterAdjust);
                            adjustCloud.Save(cloudAfterAdjust);
                            displayControl.BuildPath(adjustPath, (int)eDrawItem.AdjustPath, false, true);
                            displayControl.BuildPointCloud(adjustCloud, (int)eDrawItem.AdjustModel, false, true);

                            displayControl.UpdateDataGridView();
                        }
                    }
                }
            }
            Log.Add("Done", MsgLevel.Info);

        }

        private void button1_Click(object sender, EventArgs e)
        {
            CoordMatrix cm = new CoordMatrix();
            cm.AddSeq(eRefAxis.Z,eMatrixType.Rotate,60.0/180.0*Math.PI);
            cm.AddSeq(eRefAxis.Y, eMatrixType.Rotate, 30.0 / 180.0 * Math.PI);
            cm.AddSeq(eRefAxis.X, eMatrixType.Rotate, 40.0 / 180.0 * Math.PI);
            cm.EndAddMatrix();
            Matrix4x4 m = cm.FinalMatrix4;
            Matrix4x4 m_Inverse =  cm.GetMatrixInverse();

            Matrix4x4 m1 = m * m_Inverse;
            Matrix4x4 m2 = m_Inverse * m;

            Matrix4x4 mm = Matrix4x4.CreateRotationZ(0.5f);
        }

        private void button8_Click(object sender, EventArgs e)
        {
            AppDomain ad = AppDomain.CreateDomain("DLL unload Test");

            ProxyObject obj = (ProxyObject)ad.CreateInstanceFromAndUnwrap(@"RsLib.ToolBox.exe", "RsLib.DemoForm.ProxyObject");
            obj.LoadAssembly("D:\\RLib\\bin\\x64\\Debug\\RsLib.ToolDll.dll");
            obj.Invoke("RsLib.Common.IPlugIn", "run", "Test1");
            AppDomain.Unload(ad);
            obj = null;
        }

        private void button9_Click(object sender, EventArgs e)
        {
            Log.Add("Test", MsgLevel.Alarm,new Exception("123456789"));
        }
    }
    class ProxyObject : MarshalByRefObject
    {
        Assembly assembly = null;
        public void LoadAssembly(string dllFilePath)
        {         
            AssemblyName an = AssemblyName.GetAssemblyName(dllFilePath);
             assembly = Assembly.Load(an);
        }
        public bool Invoke(string fullClassName,string methodName,params object[] args)
        {
            if (assembly == null) return false;
            Type tp = null;
            Type[] tps = assembly.GetTypes();
            foreach (Type type in tps)
            {
                if (type.IsInterface || type.IsAbstract)
                    continue;

                else if (type.GetInterfaces().Contains(typeof(RsLib.Common.IPlugIn)))
                    tp = type;
            }
            if (tp == null) return false;
            MethodInfo method = tp.GetMethod(methodName);
            if (method == null) return false;
            object obj = Activator.CreateInstance(tp);
            method.Invoke(obj, args);
            return true;
        }
    }
    public enum eDrawItem : int 
    {
        ScanData = 2,
        AlignModel,
        AlignPath,
        AdjustModel,
        AdjustPath,
    }

}

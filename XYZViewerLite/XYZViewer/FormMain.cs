using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading;
using System.IO;
using CsGL.OpenGL;
using RsLib.WatchFolder;
using RsLib.LogMgr;
using RsLib.Common;
namespace Automation
{
    public partial class FormMain : Form
    {
        public AutoSearchKernel autoSearch;         // AutoSearch核心
        public AutoSearchData scanData;     // 掃描資料及檔案處理
        OGL gl3DGraph;                       // 3D繪圖物件
        static string Folder_App = System.Environment.CurrentDirectory;
        string Folder_temp = Folder_App + "\\temp";
        FolderWatchControl plyWatch = new FolderWatchControl("PLY");
        FolderWatchControl ply2Watch = new FolderWatchControl("PLY2");
        FolderWatchControl opt1Watch = new FolderWatchControl("OPT1");
        FolderWatchControl opt2Watch = new FolderWatchControl("OPT2");
        FolderWatchControl opt3Watch = new FolderWatchControl("OPT3");

        FormWatchFolder form_watchFolder;
        
        // 資料夾路徑
        public string strStartupPath;

        // XYZ檔案
        public string strPolyline;
        public string strPoints;

        // Opt檔案
        public string strOptFile;
        string[] _arg;

        public FormMain(string[] arg)
        {
            InitializeComponent();
            _arg = arg;
            

            // 執行檔路徑
            strStartupPath = Application.StartupPath;

            // 讓執行緒可以互相跨越存取內容
            Control.CheckForIllegalCrossThreadCalls = false;

            if (!Directory.Exists(Folder_temp)) Directory.CreateDirectory(Folder_temp);
        }

        private void FormMain_Load(object sender, EventArgs e)
        {
            autoSearch = new AutoSearchKernel();
            scanData = new AutoSearchData();
            scanData.autoSearch = autoSearch;

            lblPoyline1Count.Text = "";
            lblPoyline2Count.Text = "";
            lblPoyline3Count.Text = "";
            lblPoyline4Count.Text = "";
            lblPoyline5Count.Text = "";
            lblPoints1Count.Text = "";
            lblPoints2Count.Text = "";
            lblOpt1Count.Text = "";
            lblOpt2Count.Text = "";

            // 建立3D面板
            gl3DGraph = new OGL(this);
            gl3DGraph.Parent = pnl3DGraph;
            gl3DGraph.Dock = DockStyle.Fill;

            //gl3DGraph.Draw();
            tmrRender.Enabled = true;

            plyWatch.FileUpdated += PlyWatch_FileUpdated;
            ply2Watch.FileUpdated += Ply2Watch_FileUpdated;
            opt1Watch.FileUpdated += Opt1Watch_FileUpdated;
            opt2Watch.FileUpdated += Opt2Watch_FileUpdated;
            opt3Watch.FileUpdated += Opt3Watch_FileUpdated;
            chkShowPolyline1.Checked = gl3DGraph.blnShowPointCloud1;
            chkShowPolyline2.Checked = gl3DGraph.blnShowPointCloud2;
            chkShowPolyline3.Checked = gl3DGraph.blnShowPointCloud3;
            chkShowPolyline4.Checked = gl3DGraph.blnShowPointCloud4;
            chkShowPolyline5.Checked = gl3DGraph.blnShowPointCloud5;
            chkShowPoints1.Checked = gl3DGraph.blnShowPoints1;
            chkShowPoints2.Checked = gl3DGraph.blnShowPoints2;
            chkShowOptPath1.Checked = gl3DGraph.blnShowOptPath1;
            chkShowOptPath2.Checked = gl3DGraph.blnShowOptPath2;

            //int[] color;
            //color = OGL.GetRGB8((uint)GLColor.BrightGray); chkShowPolyline1.ForeColor = Color.FromArgb(color[0], color[1], color[2]);
            //color = OGL.GetRGB8((uint)GLColor.Blue); chkShowPolyline2.ForeColor = Color.FromArgb(color[0], color[1], color[2]);
            //color = OGL.GetRGB8((uint)GLColor.Yellow); chkShowPolyline3.ForeColor = Color.FromArgb(color[0], color[1], color[2]);
            //color = OGL.GetRGB8((uint)GLColor.BrightOrange); chkShowPolyline4.ForeColor = Color.FromArgb(color[0], color[1], color[2]);
            //color = OGL.GetRGB8((uint)GLColor.BrightRed); chkShowPolyline5.ForeColor = Color.FromArgb(color[0], color[1], color[2]);
            //color = OGL.GetRGB8((uint)GLColor.Magenta); chkShowPoints1.ForeColor = Color.FromArgb(color[0], color[1], color[2]);
            //color = OGL.GetRGB8((uint)GLColor.Purple); chkShowPoints2.ForeColor = Color.FromArgb(color[0], color[1], color[2]);
            //color = OGL.GetRGB8((uint)GLColor.Green); chkShowOptPath1.ForeColor = Color.FromArgb(color[0], color[1], color[2]);
            //color = OGL.GetRGB8((uint)GLColor.Cyan); chkShowOptPath2.ForeColor = Color.FromArgb(color[0], color[1], color[2]);

            if (_arg != null)
            {
                if (_arg.Length == 2)
                {
                    bool enableWatchFolder = false;
                    bool.TryParse(_arg[0], out enableWatchFolder);
                    if(enableWatchFolder)
                    {
                        if (Directory.Exists(_arg[1]))
                        {
                            plyWatch.SetFolder(_arg[1]);
                            opt1Watch.SetFolder(_arg[1]);
                            opt2Watch.SetFolder(_arg[1]);
                            opt3Watch.SetFolder(_arg[1]);
                        }
                        plyWatch.StartMonitor();
                        opt1Watch.StartMonitor();
                        opt2Watch.StartMonitor();
                        opt3Watch.StartMonitor();

                        splitContainer1.Panel1Collapsed = true;
                    }
                    else
                    {
                        plyWatch.StopMonitor();
                        opt1Watch.StopMonitor();
                        opt2Watch.StopMonitor();
                        opt3Watch.StopMonitor();

                        splitContainer1.Panel1Collapsed = false;
                    }

                }
            }
        }

        private void Ply2Watch_FileUpdated(string obj)
        {
            bool isTimeOut = FT_Functions.IsTimeOut(5000, () => !FT_Functions.IsFileLocked(obj));
            if (!isTimeOut)
            {
                showPoint2(obj);
            }
        }

        private void Opt3Watch_FileUpdated(string obj)
        {
            bool isTimeOut = FT_Functions.IsTimeOut(5000, () => !FT_Functions.IsFileLocked(obj));
            if (!isTimeOut)
            {
                showOPT3(obj);
            }
        }

        private void Opt2Watch_FileUpdated(string obj)
        {
            bool isTimeOut = FT_Functions.IsTimeOut(5000, () => !FT_Functions.IsFileLocked(obj));
            if (!isTimeOut)
            {
                showOPT2(obj);
            }
        }

        private void Opt1Watch_FileUpdated(string obj)
        {
            bool isTimeOut = FT_Functions.IsTimeOut(5000, () => !FT_Functions.IsFileLocked(obj));
            if (!isTimeOut)
            {
                showOPT1(obj);
            }
        }

        private void PlyWatch_FileUpdated(string obj)
        {
            bool isTimeOut = FT_Functions.IsTimeOut(5000, () => !FT_Functions.IsFileLocked(obj));
            if(!isTimeOut)
            {
                showPoint1(obj);
            }
        }
        #region CheckBox Change
        private void chkShowPolyline1_CheckedChanged(object sender, EventArgs e)
        {
            gl3DGraph.blnShowPointCloud1 = chkShowPolyline1.Checked;
            UpdateMouseHitData();
        }
        private void chkShowPolyline2_CheckedChanged(object sender, EventArgs e)
        {
            gl3DGraph.blnShowPointCloud2 = chkShowPolyline2.Checked;
            UpdateMouseHitData();
        }
        private void chkShowPolyline3_CheckedChanged(object sender, EventArgs e)
        {
            gl3DGraph.blnShowPointCloud3 = chkShowPolyline3.Checked;
            UpdateMouseHitData();
        }
        private void chkShowPolyline4_CheckedChanged(object sender, EventArgs e)
        {
            gl3DGraph.blnShowPointCloud4 = chkShowPolyline4.Checked;
            UpdateMouseHitData();
        }
        private void chkShowPolyline5_CheckedChanged(object sender, EventArgs e)
        {
            gl3DGraph.blnShowPointCloud5 = chkShowPolyline5.Checked;
            UpdateMouseHitData();
        }
        private void chkShowPoints1_CheckedChanged(object sender, EventArgs e)
        {
            gl3DGraph.blnShowPoints1 = chkShowPoints1.Checked;
            UpdateMouseHitData();
        }
        private void chkShowPoints2_CheckedChanged(object sender, EventArgs e)
        {
            gl3DGraph.blnShowPoints2 = chkShowPoints2.Checked;
            UpdateMouseHitData();
        }
        private void chkShowOptPath1_CheckedChanged(object sender, EventArgs e)
        {
            gl3DGraph.blnShowOptPath1 = chkShowOptPath1.Checked;
            UpdateMouseHitData();
        }
        private void chkShowOptPath2_CheckedChanged(object sender, EventArgs e)
        {
            gl3DGraph.blnShowOptPath2 = chkShowOptPath2.Checked;
            UpdateMouseHitData();
        }
        private void chkMouseMeasure_CheckedChanged(object sender, EventArgs e)
        {
            if (chkMouseMeasure.Checked)
                gl3DGraph.blnShowHitPoint = true;
            else
                gl3DGraph.blnShowHitPoint = false;
        }
        #endregion CheckBox Change
        #region Render

        private void Update3DModel()
        {
            if (this.InvokeRequired)
            {
                Action action = new Action(Update3DModel);
                this.Invoke(action);
            }
            else
            {
                gl3DGraph.BuildPointCloud1(chkShowPolyline1.ForeColor);
                gl3DGraph.BuildPointCloud2(chkShowPolyline2.ForeColor);
                gl3DGraph.BuildPointCloud3(chkShowPolyline3.ForeColor);
                gl3DGraph.BuildPointCloud4(chkShowPolyline4.ForeColor);
                gl3DGraph.BuildPointCloud5(chkShowPolyline5.ForeColor);
                gl3DGraph.BuildPoints1(chkShowPoints1.ForeColor);
                gl3DGraph.BuildPoints2(chkShowPoints2.ForeColor);
                gl3DGraph.BuildUser();
                gl3DGraph.BuildOptPath1(chkShowOptPath1.ForeColor, chbx_ShowOPT1NormalV.Checked);
                gl3DGraph.BuildOptPath2(chkShowOptPath2.ForeColor, chbx_ShowOPT2NormalV.Checked);
                gl3DGraph.BuildOptPath3(chkShowOptPath3.ForeColor, chbx_ShowOPT3NormalV.Checked);

            }
        }
        private void UpdateMouseHitData()
        {
            gl3DGraph.UpdateMouseHitData();
        }
        private void tmrRender_Tick(object sender, EventArgs e)
        {
            gl3DGraph.Draw();
        }
        #endregion Render
        #region Read File
        private void chkShowPolyline1_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop, false) == true)
            {
                e.Effect = DragDropEffects.All;
            }
        }
        private void chkShowPolyline1_DragDrop(object sender, DragEventArgs e)
        {
            string[] files = (string[])e.Data.GetData(DataFormats.FileDrop, false);
            strPolyline = files[0];
            try
            {
                autoSearch.polyline1.Clear();
                scanData.Read3DProfileDataFromFile(strPolyline, out autoSearch.polyline1);
                chkShowPolyline1.Checked = true;

                int L = autoSearch.polyline1.Count;
                int P = scanData.Get3DProfilePointCount(autoSearch.polyline1);
                lblPoyline1Count.Text = string.Format("{0}L\t{1}P", L, P);

                Update3DModel();
                UpdateMouseHitData();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void chkShowPolyline2_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop, false) == true)
            {
                e.Effect = DragDropEffects.All;
            }
        }
        private void chkShowPolyline2_DragDrop(object sender, DragEventArgs e)
        {
            string[] files = (string[])e.Data.GetData(DataFormats.FileDrop, false);
            strPolyline = files[0];
            try
            {
                autoSearch.polyline2.Clear();
                scanData.Read3DProfileDataFromFile(strPolyline, out autoSearch.polyline2);
                chkShowPolyline2.Checked = true;

                int L = autoSearch.polyline2.Count;
                int P = scanData.Get3DProfilePointCount(autoSearch.polyline2);
                lblPoyline2Count.Text = string.Format("{0}L\t{1}P", L, P);

                Update3DModel();
                UpdateMouseHitData();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void chkShowPolyline3_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop, false) == true)
            {
                e.Effect = DragDropEffects.All;
            }
        }
        private void chkShowPolyline3_DragDrop(object sender, DragEventArgs e)
        {
            string[] files = (string[])e.Data.GetData(DataFormats.FileDrop, false);
            strPolyline = files[0];
            try
            {
                autoSearch.polyline3.Clear();
                scanData.Read3DProfileDataFromFile(strPolyline, out autoSearch.polyline3);
                chkShowPolyline3.Checked = true;

                int L = autoSearch.polyline3.Count;
                int P = scanData.Get3DProfilePointCount(autoSearch.polyline3);
                lblPoyline3Count.Text = string.Format("{0}L\t{1}P", L, P);

                Update3DModel();
                UpdateMouseHitData();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void chkShowPolyline4_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop, false) == true)
            {
                e.Effect = DragDropEffects.All;
            }
        }
        private void chkShowPolyline4_DragDrop(object sender, DragEventArgs e)
        {
            string[] files = (string[])e.Data.GetData(DataFormats.FileDrop, false);
            strPolyline = files[0];
            try
            {
                autoSearch.polyline4.Clear();
                scanData.Read3DProfileDataFromFile(strPolyline, out autoSearch.polyline4);
                chkShowPolyline4.Checked = true;

                int L = autoSearch.polyline4.Count;
                int P = scanData.Get3DProfilePointCount(autoSearch.polyline4);
                lblPoyline4Count.Text = string.Format("{0}L\t{1}P", L, P);

                Update3DModel();
                UpdateMouseHitData();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void chkShowPolyline5_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop, false) == true)
            {
                e.Effect = DragDropEffects.All;
            }
        }
        private void chkShowPolyline5_DragDrop(object sender, DragEventArgs e)
        {
            string[] files = (string[])e.Data.GetData(DataFormats.FileDrop, false);
            strPolyline = files[0];
            try
            {
                autoSearch.polyline5.Clear();
                scanData.Read3DProfileDataFromFile(strPolyline, out autoSearch.polyline5);
                chkShowPolyline5.Checked = true;

                int L = autoSearch.polyline5.Count;
                int P = scanData.Get3DProfilePointCount(autoSearch.polyline5);
                lblPoyline5Count.Text = string.Format("{0}L\t{1}P", L, P);

                Update3DModel();
                UpdateMouseHitData();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void chkShowPoints1_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop, false) == true)
            {
                e.Effect = DragDropEffects.All;
            }
        }
        //private void chkShowPoints1_DragDrop(object sender, DragEventArgs e)
        //{
        //    string[] files = (string[])e.Data.GetData(DataFormats.FileDrop, false);
        //    strPoints = files[0];
        //    try
        //    {
        //        autoSearch.points1.Clear();
        //        scanData.Read3DPointDataFromFile(strPoints, out autoSearch.points1);
        //        chkShowPoints1.Checked = true;

        //        int P = autoSearch.points1.Count;
        //        lblPoints1Count.Text = string.Format("{0}P", P);

        //        Update3DModel();
        //        UpdateMouseHitData();
        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show(ex.Message);
        //    }
        //}
        private void chkShowPoints1_DragDrop(object sender, DragEventArgs e)
        {
            string[] array = (string[])e.Data.GetData(DataFormats.FileDrop, autoConvert: false);
            strPoints = array[0];
            showPoint1(strPoints);
        }
        void showPoint1(string filePath)
        {
            try
            {
                autoSearch.points1.Clear();
                scanData.Read3DPointDataFromFile(filePath, out autoSearch.points1);
                chkShowPoints1.Checked = true;
                gl3DGraph.blnShowPoints1 = true;
                int count = autoSearch.points1.Count;
                lblPoints1Count.Text = $"{count}P";
                Update3DModel();
                UpdateMouseHitData();
            }
            catch (Exception ex)
            {
                Log.Add("Show Point 1 Exception", MsgLevel.Alarm, ex);

                //MessageBox.Show(ex.StackTrace);
            }
        }

        private void chkShowPoints2_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop, false) == true)
            {
                e.Effect = DragDropEffects.All;
            }
        }
        private void chkShowPoints2_DragDrop(object sender, DragEventArgs e)
        {
            string[] files = (string[])e.Data.GetData(DataFormats.FileDrop, false);
            strPoints = files[0];
            showPoint2(strPoints);

        }
        void showPoint2(string filePath)
        {
            try
            {
                autoSearch.points2.Clear();
                scanData.Read3DPointDataFromFile(strPoints, out autoSearch.points2);
                chkShowPoints2.Checked = true;

                int P = autoSearch.points2.Count;
                lblPoints2Count.Text = string.Format("{0}P", P);

                Update3DModel();
                UpdateMouseHitData();
            }
            catch (Exception ex)
            {
                Log.Add("Show Point 2 Exception", MsgLevel.Alarm, ex);

                //MessageBox.Show(ex.Message);
            }
        }

        private void chkShowOptPath1_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop, false) == true)
            {
                e.Effect = DragDropEffects.All;
            }
        }
        //private void chkShowOptPath1_DragDrop(object sender, DragEventArgs e)
        //{
        //    string[] files = (string[])e.Data.GetData(DataFormats.FileDrop, false);
        //    strOptFile = files[0];
        //    try
        //    {
        //        autoSearch.opt1.Clear();
        //        scanData.Read3DOptDataFromFile(strOptFile, out autoSearch.opt1);
        //        chkShowOptPath1.Checked = true;

        //        int P = autoSearch.opt1[0].Count;
        //        lblOpt1Count.Text = string.Format("{0}P", P);

        //        Update3DModel();
        //        UpdateMouseHitData();
        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show(ex.Message);
        //    }
        //}
        private void chkShowOptPath1_DragDrop(object sender, DragEventArgs e)
        {
            string[] array = (string[])e.Data.GetData(DataFormats.FileDrop, autoConvert: false);
            strOptFile = array[0];
            showOPT1(strOptFile);

        }
        void showOPT1(string filePath)
        {
            try
            {
                autoSearch.opt1.Clear();
                scanData.Read3DOptDataFromFile(filePath, out autoSearch.opt1);
                chkShowOptPath1.Checked = true;
                gl3DGraph.blnShowOptPath1 = true;
                int count = autoSearch.opt1[0].Count;
                lblOpt1Count.Text = $"{count}P";
                Update3DModel();
                UpdateMouseHitData();
            }
            catch (Exception ex)
            {
                Log.Add("Show OPT1 Exception", MsgLevel.Alarm, ex);
                //MessageBox.Show(ex.StackTrace);
            }
        }

        private void chkShowOptPath2_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop, false) == true)
            {
                e.Effect = DragDropEffects.All;
            }
        }
        private void chkShowOptPath2_DragDrop(object sender, DragEventArgs e)
        {
            string[] files = (string[])e.Data.GetData(DataFormats.FileDrop, false);
            strOptFile = files[0];
            showOPT2(strOptFile);
        }
        void showOPT2(string filePath)
        {
            try
            {
                autoSearch.opt2.Clear();
                scanData.Read3DOptDataFromFile(filePath, out autoSearch.opt2);
                chkShowOptPath2.Checked = true;
                gl3DGraph.blnShowOptPath2 = true;

                int count = autoSearch.opt2[0].Count;
                lblOpt2Count.Text = $"{count}P";
                Update3DModel();
                UpdateMouseHitData();
            }
            catch (Exception ex)
            {
                Log.Add("Show OPT2 Exception", MsgLevel.Alarm, ex);

                //MessageBox.Show(ex.StackTrace);
            }
        }

        private void chkShowOptPath3_DragDrop(object sender, DragEventArgs e)
        {
            string[] files = (string[])e.Data.GetData(DataFormats.FileDrop, false);
            strOptFile = files[0];
            showOPT3(strOptFile);
        }

        private void chkShowOptPath3_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop, false) == true)
            {
                e.Effect = DragDropEffects.All;
            }
        }
        void showOPT3(string filePath)
        {
            try
            {
                autoSearch.opt3.Clear();
                scanData.Read3DOptDataFromFile(filePath, out autoSearch.opt3);
                chkShowOptPath3.Checked = true;
                gl3DGraph.blnShowOptPath3 = true;

                int count = autoSearch.opt3[0].Count;
                lblOpt3Count.Text = $"{count}P";
                Update3DModel();
                UpdateMouseHitData();
            }
            catch (Exception ex)
            {
                Log.Add("Show OPT3 Exception", MsgLevel.Alarm,ex);

                //MessageBox.Show(ex.StackTrace);
            }
        }
        #endregion Read File

        private void button1_Click(object sender, EventArgs e)
        {
            List<Point3D> points = new List<Point3D>();

            for (int i = 0; i < autoSearch.polyline2.Count; i++)
            {
                for (int j = 0; j < autoSearch.polyline2[i].Count; j++)
                {
                    points.Add(new Point3D(autoSearch.polyline2[i][j]));
                }
            }

            scanData.Write3DPointDataToOFFFile("D:\\02_LMI\\orig.off", points);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            System.Windows.Forms.OpenFileDialog openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            openFileDialog1.InitialDirectory = Application.StartupPath;
            openFileDialog1.Filter = "xyz file |*.xyz";
            openFileDialog1.FileName = "";

            if (openFileDialog1.ShowDialog() != DialogResult.OK)
            {
                return;
            }

            string strBmpFile = openFileDialog1.FileName.Replace(".xyz", ".bmp");
            string strTxtFile = openFileDialog1.FileName.Replace(".xyz", ".txt");
            string strBitelineFile = openFileDialog1.FileName.Replace(".xyz", "_biteline.xyz");
            string strBitelineFile_9 = openFileDialog1.FileName.Replace(".xyz", "_biteline9.xyz");
            string strBitelineFile_25 = openFileDialog1.FileName.Replace(".xyz", "_biteline25.xyz");


            List<List<Point3D>> orig = new List<List<Point3D>>();
            scanData.Read3DProfileDataFromFile(openFileDialog1.FileName, out orig);

            double dblXmin = 0;
            double dblXmax = 0;
            double dblYmin = 0;
            double dblYmax = 0;

            for (int i = 0; i < orig.Count; i++)
            {
                for (int j = 0; j < orig[i].Count; j++)
                {
                    dblXmin = Math.Min(dblXmin, orig[i][j].X);
                    dblXmax = Math.Max(dblXmax, orig[i][j].X);
                    dblYmin = Math.Min(dblYmin, orig[i][j].Y);
                    dblYmax = Math.Max(dblYmax, orig[i][j].Y);
                }
            }

            int k = 0;

            int mapWidth = 1001;
            int mapHeight = 601;

            List<Point3D>[,] buffer = new List<Point3D>[mapWidth, mapHeight];

            for (int h = 0; h < mapHeight; h++)
            {
                for (int w = 0; w < mapWidth; w++)
                {
                    buffer[w, h] = new List<Point3D>();
                }
            }

            for (int i = 0; i < orig.Count; i++)
            {
                for (int j = 0; j < orig[i].Count; j++)
                {
                    int w = (int)Math.Round((orig[i][j].X - (-250)) * 2);
                    int h = -(int)Math.Round((orig[i][j].Y - (150)) * 2);

                    if (h < 0 || h >= mapHeight || w < 0 || w >= mapWidth)
                    {
                        continue;
                    }

                    buffer[w, h].Add(new Point3D(orig[i][j]));
                }
            }


            Point3D[,] result = new Point3D[mapWidth, mapHeight];

            for (int h = 0; h < mapHeight; h++)
            {
                for (int w = 0; w < mapWidth; w++)
                {
                    if (buffer[w, h].Count >= 1)
                    {
                        List<Point3D> sortZ = buffer[w, h].OrderByDescending(t => t.Z).ToList();
                        result[w, h] = new Point3D(sortZ[0]);
                    }
                }
            }



            Bitmap bmp = new Bitmap(mapWidth, mapHeight, System.Drawing.Imaging.PixelFormat.Format24bppRgb);

            for (int h = 0; h < mapHeight; h++)
            {
                for (int w = 0; w < mapWidth; w++)
                {
                    Color color = new Color();

                    if (buffer[w, h].Count >= 1)
                    {
                        color = Color.FromArgb((int)result[w, h].R, (int)result[w, h].G, (int)result[w, h].B);
                        bmp.SetPixel(w, h, color);
                    }
                    else
                    {
                        color = Color.FromArgb(0, 0, 0);
                        bmp.SetPixel(w, h, color);
                    }
                }
            }

            //bmp.RotateFlip(RotateFlipType.RotateNoneFlipY);
            bmp.Save(strBmpFile);


            // AI 2D->3D
            if (File.Exists(strTxtFile))
            {
                List<string> allLines = new List<string>();
                allLines = MyFile.ReadAllLines(strTxtFile);

                List<Point3D> biteline = new List<Point3D>();
                List<Point3D> biteline1 = new List<Point3D>();
                List<Point3D> biteline2 = new List<Point3D>();
                List<Point3D> biteline3 = new List<Point3D>();

                for (int i = 0; i < allLines.Count; i++)
                {
                    string[] split;
                    split = allLines[i].Split(',');

                    int intTargetX = int.Parse(split[0]);
                    int intTargetY = int.Parse(split[1]);

                    int XP2 = intTargetX + 2;
                    int XP1 = intTargetX + 1;
                    int XS1 = intTargetX - 1;
                    int XS2 = intTargetX - 2;
                    int YS2 = intTargetY - 2;
                    int YS1 = intTargetY - 1;
                    int YP2 = intTargetY + 2;
                    int YP1 = intTargetY + 1;

                    if (buffer[intTargetX, intTargetY].Count >= 1)
                    {
                        biteline.Add(new Point3D(result[intTargetX, intTargetY]));
                        biteline1.Add(new Point3D(Point3D.CenterOfGravity(buffer[intTargetX, intTargetY])));
                        biteline2.Add(new Point3D(Point3D.CenterOfGravity_9(buffer[intTargetX, intTargetY], buffer[intTargetX-1, intTargetY+1], buffer[intTargetX, intTargetY+1], buffer[intTargetX+1, intTargetY-1], buffer[intTargetX-1, intTargetY], buffer[intTargetX+1, intTargetY], buffer[intTargetX-1, intTargetY-1], buffer[intTargetX, intTargetY-1], buffer[intTargetX+1, intTargetY-1])));
                        biteline3.Add(new Point3D(Point3D.CenterOfGravity_25(buffer[intTargetX, intTargetY], buffer[XS2, YP2], buffer[XS1, YP2], buffer[intTargetX, YP2], buffer[XP1, YP2], buffer[XP2, YP2], buffer[XS2, YP1], buffer[XS1, YP1], buffer[intTargetX, YP1], buffer[XP1, YP1], buffer[XP2, YP1], buffer[XS2, intTargetY], buffer[XS1, intTargetY], buffer[XP1, intTargetY], buffer[XP2, intTargetY], buffer[XS2, YS1], buffer[XS1, YS1], buffer[intTargetX, YS1], buffer[XP1, YS1], buffer[XP2, YS1], buffer[XS2, YS2], buffer[XS1, YS2], buffer[intTargetX, YS2], buffer[XP1, YS2], buffer[XP2, YS2])));
                    }
                }

                //scanData.Write3DPointDataToFile(Application.StartupPath + "\\Biteline.xyz", biteline);
                scanData.Write3DPointDataToFile(strBitelineFile, biteline1);
                scanData.Write3DPointDataToFile(strBitelineFile_9, biteline2);
                scanData.Write3DPointDataToFile(strBitelineFile_25, biteline3);
            }

        }

        private void btn_ClearDisplay_Click(object sender, EventArgs e)
        {
            gl3DGraph.Clear();
            gl3DGraph.SizeReset();
        }

        private void tableLayoutPanel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void chkShowOptPath3_CheckedChanged(object sender, EventArgs e)
        {
            gl3DGraph.blnShowOptPath3 = chkShowOptPath3.Checked;
            UpdateMouseHitData();

        }

        private void chbx_ShowOPT3NormalV_CheckedChanged(object sender, EventArgs e)
        {
            Update3DModel();
        }

        private void btn_WatchFolderSet_Click(object sender, EventArgs e)
        {
            form_watchFolder = new FormWatchFolder(plyWatch, opt1Watch, opt2Watch, opt3Watch);
            form_watchFolder.Show();
        }

        private void btn_CollapsePanel_Click(object sender, EventArgs e)
        {
            splitContainer1.Panel1Collapsed = !splitContainer1.Panel1Collapsed;
        }

        private void chbx_ShowOPT1NormalV_CheckedChanged(object sender, EventArgs e)
        {
            Update3DModel();

        }

        private void chbx_ShowOPT2NormalV_CheckedChanged(object sender, EventArgs e)
        {
            Update3DModel();

        }
    }
}

using RsLib.Common;
using RsLib.ConvertKeyBMP;
using RsLib.Display3D;
using RsLib.PointCloudLib;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Threading;
using System.Windows.Forms;
using RsLib.LogMgr;
using System.Threading;
using System.Threading.Tasks;
using Accord.Statistics.Filters;

namespace RsLib.XYZViewer
{
    public partial class Form1 : Form
    {
        const int MaxXYZCount = 5;
        const int MaxOPTCount = 3;

        Display3DControl _displayCtrl = new Display3DControl(MaxXYZCount + MaxOPTCount * 5 +1);

        Dictionary<DrawItem, Button> xyzButtons = new Dictionary<DrawItem, Button>();
        Dictionary<DrawItem, Button> optButtons = new Dictionary<DrawItem, Button>();
        Dictionary<DrawItem, string> loadedFiles = new Dictionary<DrawItem, string>();

        FormProcessing _processForm;
        FormIntersection fi = new FormIntersection();
        FormDifference fd = new FormDifference();

        public Form1()
        {
            Log.Start();
            InitializeComponent();
            KeyBMP.Init();
            _displayCtrl.Dock = DockStyle.Fill;
            _displayCtrl.EnableMultipleSelect = false;
            tableLayoutPanel2.Controls.Add(_displayCtrl, 1, 0);
            _displayCtrl.AfterClearButtonPressed += _displayCtrl_AfterCleared;
            init();
            SizeChanged += Form1_SizeChanged;
            this.Text += " " + FT_Functions.GetFileVersion("RsLib.XYZViewer.exe");
            fi.AfterPressShowIntersect += Fi_AfterPressShowIntersect;
            fd.AfterShowPressed += Fd_AfterShowPressed;

        }


        private void Form1_SizeChanged(object sender, EventArgs e)
        {
        }

        private void _displayCtrl_AfterCleared()
        {
            xyzButtons.Clear();
            optButtons.Clear();
            panel1.Controls.Clear();
            loadedFiles.Clear();
            init();
        }

        void init()
        {
            DisplayObjectOption[] XYZOptions = DisplayObjectOption.CreateDisplayOptionArray((int)DrawItem.XYZ1, 5, DisplayObjectType.PointCloud, 2.0f, true);
            DisplayObjectOption[] PathOptions = DisplayObjectOption.CreateDisplayOptionArray((int)DrawItem.OPT1Path, 3, DisplayObjectType.Path, 2.0f, false);
            DisplayObjectOption[] PointOptions = DisplayObjectOption.CreateDisplayOptionArray((int)DrawItem.OPT1Point, 3, DisplayObjectType.PointCloud, 5.0f, true);
            DisplayObjectOption[] vzVectorOptions = DisplayObjectOption.CreateDisplayOptionArray((int)DrawItem.OPT1vzVector, 3, DisplayObjectType.Vector_z, 1.0f, false);
            DisplayObjectOption[] vyVectorOptions = DisplayObjectOption.CreateDisplayOptionArray((int)DrawItem.OPT1vyVector, 3, DisplayObjectType.Vector_y, 1.0f, false);
            DisplayObjectOption[] vxVectorOptions = DisplayObjectOption.CreateDisplayOptionArray((int)DrawItem.OPT1vxVector, 3, DisplayObjectType.Vector_x, 1.0f, false);

            XYZOptions[0].DrawColor = Color.Gray;
            XYZOptions[1].DrawColor = Color.DarkGray;
            XYZOptions[2].DrawColor = Color.Silver;
            XYZOptions[3].DrawColor = Color.LightGray;
            XYZOptions[4].DrawColor = Color.Gainsboro;

            PathOptions[0].DrawColor = Color.DarkOrange;
            PathOptions[1].DrawColor = Color.SteelBlue;
            PathOptions[2].DrawColor = Color.DarkOrchid;

            PointOptions[0].DrawColor = Color.DarkOrange;
            PointOptions[1].DrawColor = Color.SteelBlue;
            PointOptions[2].DrawColor = Color.DarkOrchid;

            vzVectorOptions[0].DrawColor = Color.Blue;
            vzVectorOptions[1].DrawColor = Color.Blue;
            vzVectorOptions[2].DrawColor = Color.Blue;

            vyVectorOptions[0].DrawColor = Color.LimeGreen;
            vyVectorOptions[1].DrawColor = Color.LimeGreen;
            vyVectorOptions[2].DrawColor = Color.LimeGreen;

            vxVectorOptions[0].DrawColor = Color.Red;
            vxVectorOptions[1].DrawColor = Color.Red;
            vxVectorOptions[2].DrawColor = Color.Red;

            _displayCtrl.AddDisplayOption(XYZOptions);
            _displayCtrl.AddDisplayOption(PathOptions);
            _displayCtrl.AddDisplayOption(PointOptions);
            _displayCtrl.AddDisplayOption(vzVectorOptions);
            _displayCtrl.AddDisplayOption(vyVectorOptions);
            _displayCtrl.AddDisplayOption(vxVectorOptions);
            _displayCtrl.AddDisplayOption(new DisplayObjectOption((int)DrawItem.VzIntersection, "IntersectVz", Color.Cyan, DisplayObjectType.Path, 2.0f) { IsShowAtDataGrid = true});

            createButton(optButtons, DrawItem.OPT3Path, PathOptions[2].DrawColor,"Path 3");
            createButton(optButtons, DrawItem.OPT2Path, PathOptions[1].DrawColor,"Path 2");
            createButton(optButtons, DrawItem.OPT1Path, PathOptions[0].DrawColor,"Path 1");

            createButton(xyzButtons, DrawItem.XYZ5, XYZOptions[4].DrawColor,"Point Cloud 5");
            createButton(xyzButtons, DrawItem.XYZ4, XYZOptions[3].DrawColor, "Point Cloud 4");
            createButton(xyzButtons, DrawItem.XYZ3, XYZOptions[2].DrawColor, "Point Cloud 3");
            createButton(xyzButtons, DrawItem.XYZ2, XYZOptions[1].DrawColor, "Point Cloud 2");
            createButton(xyzButtons, DrawItem.XYZ1, XYZOptions[0].DrawColor, "Point Cloud 1");

            loadedFiles.Add(DrawItem.XYZ1, "");
            loadedFiles.Add(DrawItem.XYZ2, "");
            loadedFiles.Add(DrawItem.XYZ3, "");
            loadedFiles.Add(DrawItem.XYZ4, "");
            loadedFiles.Add(DrawItem.XYZ5, "");
            loadedFiles.Add(DrawItem.OPT1Path, "");
            loadedFiles.Add(DrawItem.OPT2Path, "");
            loadedFiles.Add(DrawItem.OPT3Path, "");

        }

        void createButton(Dictionary<DrawItem, Button> dic, DrawItem drawItem, Color backColor,string displayText)
        {
            Button btn = new Button();
            btn.BackColor = backColor;
            btn.Text = displayText;
            btn.Dock = DockStyle.Top;
            btn.Height = 50;
            btn.AllowDrop = true;
            btn.MouseClick += Btn_MouseClick;
            btn.DragEnter += Btn_DragEnter;
            btn.DragDrop += Btn_DragDrop;
            dic.Add(drawItem, btn);
            panel1.Controls.Add(btn);
        }

        void showProcessForm_td(object obj)
        {
            if (_processForm == null)
            {
                _processForm = new FormProcessing("Loading...");
                _processForm.SetMode(ProgressBarStyle.Marquee);
                _processForm.SetProgress(100);
                _processForm.ShowDialog();
            }
        }

        void loadFile(DrawItem drawItem, string filePath)
        {
            ThreadPool.QueueUserWorkItem(new WaitCallback(showProcessForm_td));
            string ext = Path.GetExtension(filePath).ToLower();
            string fileName = Path.GetFileNameWithoutExtension(filePath);
            _displayCtrl.ShowColorGradientControl(false);
            Log.Add($"Load {filePath}.", MsgLevel.Info);
            try
            {
                _displayCtrl.ClearSelectedObjectList((int)drawItem);
                switch (ext)
                {
                    case ".xyz":
                        PointCloud cloud = new PointCloud();
                        cloud.LoadFromFile(filePath, true);
                        _displayCtrl.GetDisplayObjectOption((int)drawItem).Name = fileName;
                        _displayCtrl.BuildPointCloud(cloud, (int)drawItem, true, true);
                        break;
                    case ".ply":
                        PointCloud cloudPLY = new PointCloud();
                        cloudPLY.LoadFromPLY(filePath, true);
                        _displayCtrl.GetDisplayObjectOption((int)drawItem).Name = fileName;
                        _displayCtrl.BuildPointCloud(cloudPLY, (int)drawItem, true, true);
                        break;
                    case ".opt":
                        ObjectGroup group = new ObjectGroup(fileName);
                        group.LoadMultiPathOPT(filePath, true);
                        drawObjectGroup(group, drawItem,fileName);
                        //Polyline line = new Polyline();
                        //line.LoadFromOPTFile(filePath, true);
                        //_displayCtrl.GetDisplayObjectOption((int)drawItem).Name = fileName;
                        //_displayCtrl.BuildPath(group, (int)drawItem, true, true);

                        //_displayCtrl.GetDisplayObjectOption((int)drawItem + 3).Name = fileName;
                        //_displayCtrl.BuildPointCloud(group, (int)drawItem + 3, false, true);

                        //_displayCtrl.GetDisplayObjectOption((int)drawItem + 6).Name = fileName;
                        //_displayCtrl.GetDisplayObjectOption((int)drawItem + 6).IsDisplay = false;
                        //_displayCtrl.BuildVector(group, (int)drawItem + 6, false, true);

                        //_displayCtrl.GetDisplayObjectOption((int)drawItem + 9).Name = fileName;
                        //_displayCtrl.GetDisplayObjectOption((int)drawItem + 9).IsDisplay = false;
                        //_displayCtrl.BuildVector(group, (int)drawItem + 9, false, true);

                        //_displayCtrl.GetDisplayObjectOption((int)drawItem + 12).Name = fileName;
                        //_displayCtrl.GetDisplayObjectOption((int)drawItem + 12).IsDisplay = false;
                        //_displayCtrl.BuildVector(group, (int)drawItem + 12, false, true);


                        break;
                    case ".opt2":
                        ObjectGroup group2 = new ObjectGroup(fileName);
                        group2.LoadMultiPathOPT2(filePath, true);
                        drawObjectGroup(group2, drawItem, fileName);
                        //Polyline line = new Polyline();
                        //line.LoadFromOPTFile(filePath, true);
                        //_displayCtrl.GetDisplayObjectOption((int)drawItem).Name = fileName;
                        //_displayCtrl.BuildPath(group, (int)drawItem, true, true);

                        //_displayCtrl.GetDisplayObjectOption((int)drawItem + 3).Name = fileName;
                        //_displayCtrl.BuildPointCloud(group, (int)drawItem + 3, false, true);

                        //_displayCtrl.GetDisplayObjectOption((int)drawItem + 6).Name = fileName;
                        //_displayCtrl.GetDisplayObjectOption((int)drawItem + 6).IsDisplay = false;
                        //_displayCtrl.BuildVector(group, (int)drawItem + 6, false, true);

                        //_displayCtrl.GetDisplayObjectOption((int)drawItem + 9).Name = fileName;
                        //_displayCtrl.GetDisplayObjectOption((int)drawItem + 9).IsDisplay = false;
                        //_displayCtrl.BuildVector(group, (int)drawItem + 9, false, true);

                        //_displayCtrl.GetDisplayObjectOption((int)drawItem + 12).Name = fileName;
                        //_displayCtrl.GetDisplayObjectOption((int)drawItem + 12).IsDisplay = false;
                        //_displayCtrl.BuildVector(group, (int)drawItem + 12, false, true);


                        break;

                    case ".csv":
                        PointCloud cloud2 = KeyRawCSV.LoadHeightRawData(filePath, 1, 1);
                        _displayCtrl.GetDisplayObjectOption((int)drawItem).Name = fileName;
                        _displayCtrl.BuildPointCloud(cloud2, (int)drawItem, true, true);

                        break;
                    case ".bmp":
                        KeyBMP.Load(filePath);
                        PointCloud cloud3 = KeyBMP.ConvertToXYZ();
                        _displayCtrl.GetDisplayObjectOption((int)drawItem).Name = fileName;
                        _displayCtrl.BuildPointCloud(cloud3, (int)drawItem, true, true);

                        break;

                    case ".json":
                        NikePath nike = NikePath.Parse(filePath);
                        List<ObjectGroup> groups = nike.ToObjectGroups();
                        if (groups.Count > 0)
                        {
                            drawObjectGroup(groups[0], drawItem, fileName);
                        }
                        break;
                    default:

                        break;
                }

            }
            catch(Exception ex)
            {
                Log.Add("Load file exception.", MsgLevel.Alarm, ex);
            }
            if (_processForm != null)
            {
                _processForm.BeginInvoke(new Action(
                    () =>
                    {
                        _processForm.Close();
                        _processForm = null;
                    }));
            }
            _displayCtrl.UpdateDataGridView();
        }

        private void drawObjectGroup(ObjectGroup group,DrawItem drawItem,string fileName)
        {
            _displayCtrl.GetDisplayObjectOption((int)drawItem).Name = fileName;
            _displayCtrl.BuildPath(group, (int)drawItem, true, true);

            _displayCtrl.GetDisplayObjectOption((int)drawItem + 3).Name = fileName;
            _displayCtrl.BuildPointCloud(group, (int)drawItem + 3, false, true);

            _displayCtrl.GetDisplayObjectOption((int)drawItem + 6).Name = fileName;
            _displayCtrl.GetDisplayObjectOption((int)drawItem + 6).IsDisplay = false;
            _displayCtrl.BuildVector(group, (int)drawItem + 6, false, true);

            _displayCtrl.GetDisplayObjectOption((int)drawItem + 9).Name = fileName;
            _displayCtrl.GetDisplayObjectOption((int)drawItem + 9).IsDisplay = false;
            _displayCtrl.BuildVector(group, (int)drawItem + 9, false, true);

            _displayCtrl.GetDisplayObjectOption((int)drawItem + 12).Name = fileName;
            _displayCtrl.GetDisplayObjectOption((int)drawItem + 12).IsDisplay = false;
            _displayCtrl.BuildVector(group, (int)drawItem + 12, false, true);

        }
        DrawItem getPressedButton(Button btn)
        {
            string text = btn.Text;
            foreach (var item in xyzButtons)
            {
                if (item.Value.Text == text) return item.Key;
            }
            foreach (var item in optButtons)
            {
                if (item.Value.Text == text) return item.Key;
            }
            return DrawItem.None;
        }

        private void Btn_DragDrop(object sender, DragEventArgs e)
        {
            Button btn = (Button)sender;
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                string[] files = (string[])e.Data.GetData(DataFormats.FileDrop);
                DrawItem dropedBtn = getPressedButton((Button)sender);

                loadFile(dropedBtn, files[0]);
                ((Button)sender).Text = Path.GetFileNameWithoutExtension(files[0]);
                if (loadedFiles.ContainsKey(dropedBtn) == false)
                {
                    loadedFiles.Add(dropedBtn, files[0]);
                }
                else
                {
                    loadedFiles[dropedBtn] = files[0];
                }
            }
        }

        private void Btn_DragEnter(object sender, DragEventArgs e)
        {

            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                bool canDrop = false;

                string[] files = (string[])e.Data.GetData(DataFormats.FileDrop);
                if (files.Length > 0)
                {
                    string ext = Path.GetExtension(files[0]).ToLower();
                    string fileName = Path.GetFileName(files[0]).ToLower();
                    DrawItem dropedBtn = getPressedButton((Button)sender);

                    if (dropedBtn >= DrawItem.XYZ1 && dropedBtn <= DrawItem.XYZ5)
                    {
                        if (ext == ".xyz")
                        {
                            canDrop = true;
                        }
                        else if (ext == ".csv")
                        {
                            if (fileName.Contains(KeyRawCSV.Extension.ToLower())) canDrop = true;
                        }
                        else if (ext == ".bmp")
                        {
                            if (fileName.Contains(KeyBMP.HeightExt.ToLower())) canDrop = true;
                        }
                        else if(ext == ".ply")
                        {
                            canDrop = true;
                        }
                        else
                        {
                            canDrop = false;
                        }
                    }
                    if (dropedBtn >= DrawItem.OPT1Path && dropedBtn <= DrawItem.OPT3Path)
                    {
                        if(ext == ".opt") canDrop = true;
                        else if(ext == ".json")canDrop = true;
                        else if(ext == ".opt2") canDrop =true;
                        else canDrop = false;
                    }

                    if (canDrop) e.Effect = DragDropEffects.Move;
                }
            }
        }

        private void Btn_MouseClick(object sender, MouseEventArgs e)
        {
            using (OpenFileDialog op = new OpenFileDialog())
            {
                DrawItem dropedBtn = getPressedButton((Button)sender);
                if (dropedBtn >= DrawItem.XYZ1 && dropedBtn <= DrawItem.XYZ5) op.Filter = "XYZ cloud file|*.xyz|PLY file|*.ply|Keyence CSV Raw File|*_HRaw.csv|Keyence BMP Raw File|*_Height.bmp";
                if (dropedBtn >= DrawItem.OPT1Path && dropedBtn <= DrawItem.OPT3Path) op.Filter = "OPT path file|*.opt|OPT2 path file|*.opt2";

                if (op.ShowDialog() == DialogResult.OK)
                {
                    ((Button)sender).Text = Path.GetFileNameWithoutExtension(op.FileName);
                    loadFile(dropedBtn, op.FileName);
                }
            }
        }

        private void reloadToolStripMenuItem_Click(object sender, EventArgs e)
        {
            foreach (var item in loadedFiles)
            {
                DrawItem drawItem = item.Key;
                string filePath = item.Value;
                if (File.Exists(filePath)) 
                {
                    loadFile(drawItem, filePath);
                }
            }
        }

        private void showINtersectionToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string[] fileNames = new string[5];
            for (int i = 2; i < 7; i++)
            {
                if (loadedFiles.ContainsKey((DrawItem)i))
                {
                    fileNames[i - 2] = Path.GetFileNameWithoutExtension(loadedFiles[(DrawItem)i]);
                }
            }
            string[] optFileNames = new string[3];
            for (int i = 7; i < 10; i++)
            {
                if (loadedFiles.ContainsKey((DrawItem)i))
                {
                    optFileNames[i - 7] = Path.GetFileNameWithoutExtension(loadedFiles[(DrawItem)i]);
                }
            }
            fi.SetFileName(fileNames,optFileNames);
            fi.Show();
        }

        private void Fi_AfterPressShowIntersect(int cloudIndex, int pathIndex, double extendLength, double searchR, int searchRange,double reduceR)
        {
            string cloudFile = loadedFiles[(DrawItem)(cloudIndex + 1)];
            string cloudFileName = Path.GetFileNameWithoutExtension(cloudFile);
            string cloudExt = Path.GetExtension(cloudFile).ToUpper();

            string pathFile = loadedFiles[(DrawItem)(6 + pathIndex)];
            string pathFileName = Path.GetFileNameWithoutExtension(pathFile);
            string pathExt  = Path.GetExtension(pathFile).ToUpper();
            try
            {
                _displayCtrl.ClearSelectedObjectList((int)DrawItem.VzIntersection);
                PointCloud cloud = new PointCloud();
                switch (cloudExt)
                {
                    case ".XYZ":
                        cloud.LoadFromFile(cloudFile, true);
                        break;
                    case ".PLY":
                        cloud.LoadFromPLY(cloudFile, true);
                        break;
                    case ".CSV":
                        cloud = KeyRawCSV.LoadHeightRawData(cloudFile, 1, 1);

                        break;
                    case ".BMP":
                        KeyBMP.Load(cloudFile);
                        cloud = KeyBMP.ConvertToXYZ();

                        break;

                    default:

                        break;
                }
                ObjectGroup group = new ObjectGroup(pathFileName);

                switch (pathExt)
                {
                    case ".OPT":
                        group.LoadMultiPathOPT(pathFile, true);
                        break;
                    case ".OPT2":
                        group.LoadMultiPathOPT2(pathFile, true);
                        break;
                    default:

                        break;
                }
                //PointCloud cloudIntersect = new PointCloud();
                ObjectGroup groupIntersect = new ObjectGroup("IntersecPath");
                object lockobj = new object();

#if parallel
                foreach (var item in group.Objects)
                {
                    string objName = item.Key;
                    Object3D obj = item.Value;
                    if (obj is Polyline pl)
                    {
                        PointCloud pCloud = pl.GetIntersectVz(cloud.kdTree, extendLength, searchRange, searchR, reduceR);
                        lock (lockobj)
                        {
                            cloudIntersect.Add(pCloud);
                        }
                    }
                }


#else
                Parallel.ForEach(group.Objects, (KeyValuePair<string, Object3D> o) =>
                {
                    string objName = o.Key;
                    Object3D obj = o.Value;
                    if (obj is Polyline pl)
                    {
                        Polyline pCloud = pl.GetIntersectVz(cloud.kdTree, extendLength, searchRange,0.1, searchR,0.1, reduceR,true);
                        lock (lockobj)
                        {
                            //cloudIntersect.Add(pCloud);
                            groupIntersect.Add($"{objName}Intersect", pCloud);
                        }
                    }
                });
#endif
                _displayCtrl.GetDisplayObjectOption((int)DrawItem.VzIntersection).Name = "IntersectVz";
                _displayCtrl.BuildPath(groupIntersect, (int)DrawItem.VzIntersection, false, true);
                _displayCtrl.UpdateDataGridView();
            }
            catch (Exception ex)
            {
                Log.Add("Load file exception.", MsgLevel.Alarm, ex);
            }

        }

        private void showDifferenceToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string[] fileNames = new string[5];
            for(int i = 2; i < 7; i++)
            {
                if(loadedFiles.ContainsKey((DrawItem)i))
                {
                    fileNames[i - 2] = Path.GetFileNameWithoutExtension(loadedFiles[(DrawItem)i]);
                }
            }
            fd.SetFileName(fileNames);
            fd.Show();
            
        }

        private void Fd_AfterShowPressed(int baseIndex, int compareIndex, double min, double max,bool absMode)
        {
            ColorGradient cg = new ColorGradient(min, max);
            string baseFile = loadedFiles[(DrawItem)(baseIndex + 1)];
            string baseFileName = Path.GetFileNameWithoutExtension(baseFile);
            string baseFileext = Path.GetExtension(baseFile).ToUpper();
            string compareFile = loadedFiles[(DrawItem)(compareIndex + 1)];
            string compareFileName = Path.GetFileNameWithoutExtension(compareFile);
            string compareFileext = Path.GetExtension(compareFile).ToUpper();
            try
            {
                PointCloud cloudBase = new PointCloud();

                switch (baseFileext)
                {
                    case ".XYZ":
                        cloudBase.LoadFromFile(baseFile, true);
                        break;
                    case ".PLY":
                        cloudBase.LoadFromPLY(baseFile, true);
                        break;
                    case ".CSV":
                        cloudBase = KeyRawCSV.LoadHeightRawData(baseFile, 1, 1);

                        break;
                    case ".BMP":
                        KeyBMP.Load(baseFile);
                        cloudBase = KeyBMP.ConvertToXYZ();

                        break;

                    default:

                        break;
                }
                PointCloud cloudCompare = new PointCloud();
                switch (compareFileext)
                {
                    case ".XYZ":
                        cloudCompare.LoadFromFile(compareFile, true);
                        break;
                    case ".PLY":
                        cloudCompare.LoadFromPLY(compareFile, true);
                        break;
                    case ".CSV":
                        cloudCompare = KeyRawCSV.LoadHeightRawData(compareFile, 1, 1);

                        break;
                    case ".BMP":
                        KeyBMP.Load(compareFile);
                        cloudCompare = KeyBMP.ConvertToXYZ();

                        break;

                    default:

                        break;
                }
                _displayCtrl.SetColorGradientCtrl(cg.ColorControl);
                _displayCtrl.ShowColorGradientControl(true);
                cloudBase.CompareOtherCloud(cloudCompare.kdTree, min, max, absMode);
                _displayCtrl.BuildPointCloud(cloudBase, baseIndex + 1, false, true);
            }
            catch (Exception ex)
            {
                Log.Add("Load file exception.", MsgLevel.Alarm, ex);
            }
        }
    }
    public enum DrawItem : int
    {
        None = 0,
        XYZ1 = 2,
        XYZ2,
        XYZ3,
        XYZ4,
        XYZ5,

        OPT1Path,
        OPT2Path,
        OPT3Path,

        OPT1Point,
        OPT2Point,
        OPT3Point,

        OPT1vzVector,
        OPT2vzVector,
        OPT3vzVector,

        OPT1vyVector,
        OPT2vyVector,
        OPT3vyVector,

        OPT1vxVector,
        OPT2vxVector,
        OPT3vxVector,

        VzIntersection,
    }
}

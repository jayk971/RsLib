﻿using RsLib.Common;
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
namespace RsLib.XYZViewer
{
    public partial class Form1 : Form
    {
        const int MaxXYZCount = 5;
        const int MaxOPTCount = 3;

        Display3DControl _displayCtrl = new Display3DControl(MaxXYZCount + MaxOPTCount * 5);

        Dictionary<DrawItem, Button> xyzButtons = new Dictionary<DrawItem, Button>();
        Dictionary<DrawItem, Button> optButtons = new Dictionary<DrawItem, Button>();
        Dictionary<DrawItem, string> loadedFiles = new Dictionary<DrawItem, string>();

        FormProcessing _processForm;
        public Form1()
        {
            Log.Start();
            InitializeComponent();
            KeyBMP.Init();
            _displayCtrl.Dock = DockStyle.Fill;
            tableLayoutPanel2.Controls.Add(_displayCtrl, 1, 0);
            _displayCtrl.AfterClearButtonPressed += _displayCtrl_AfterCleared;
            init();
            SizeChanged += Form1_SizeChanged;
            this.Text += " " + FT_Functions.GetFileVersion("RsLib.XYZViewer.exe");
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
            PathOptions[1].DrawColor = Color.Cyan;
            PathOptions[2].DrawColor = Color.Gold;

            PointOptions[0].DrawColor = Color.DarkOrange;
            PointOptions[1].DrawColor = Color.Cyan;
            PointOptions[2].DrawColor = Color.Gold;

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
                if(loadedFiles.ContainsKey(dropedBtn) == false)
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


                        else
                        {
                            canDrop = false;
                        }
                    }
                    if (dropedBtn >= DrawItem.OPT1Path && dropedBtn <= DrawItem.OPT3Path)
                    {
                        if(ext == ".opt") canDrop = true;
                        else if(ext == ".json")canDrop = true;
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
                if (dropedBtn >= DrawItem.XYZ1 && dropedBtn <= DrawItem.XYZ5) op.Filter = "XYZ cloud file|*.xyz|Keyence CSV Raw File|*_HRaw.csv|Keyence BMP Raw File|*_Height.bmp";
                if (dropedBtn >= DrawItem.OPT1Path && dropedBtn <= DrawItem.OPT3Path) op.Filter = "OPT path file|*.opt";

                if (op.ShowDialog() == DialogResult.OK)
                {
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

    }
}

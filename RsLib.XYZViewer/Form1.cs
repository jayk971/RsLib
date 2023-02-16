﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

using RsLib.Common;
using RsLib.Display3D;
using RsLib.PointCloud;
namespace RsLib.XYZViewer
{
    using RPointCloud = RsLib.PointCloud.PointCloud;
    public partial class Form1 : Form
    {
        const int MaxXYZCount = 5;
        const int MaxOPTCount = 3;

        Display3DControl _displayCtrl = new Display3DControl(MaxXYZCount+ MaxOPTCount*3);

        Dictionary<DrawItem, Button> xyzButtons = new Dictionary<DrawItem, Button>();
        Dictionary<DrawItem, Button> optButtons = new Dictionary<DrawItem, Button>();


        public Form1()
        {
            InitializeComponent();
            _displayCtrl.Dock = DockStyle.Fill;
            tableLayoutPanel2.Controls.Add(_displayCtrl, 1, 0);
            _displayCtrl.AfterClearButtonPressed += _displayCtrl_AfterCleared;
            init();
        }

        private void _displayCtrl_AfterCleared()
        {
            xyzButtons.Clear();
            optButtons.Clear();
            panel1.Controls.Clear();
            init();
        }

        void init()
        {
            DisplayObjectOption[] XYZOptions = DisplayObjectOption.CreateDisplayOptionArray((int)DrawItem.XYZ1, 5, DisplayObjectType.PointCloud, 1.0f,true);
            DisplayObjectOption[] PathOptions = DisplayObjectOption.CreateDisplayOptionArray((int)DrawItem.OPT1Path, 3, DisplayObjectType.Path, 3.0f,false);
            DisplayObjectOption[] PointOptions = DisplayObjectOption.CreateDisplayOptionArray((int)DrawItem.OPT1Point, 3, DisplayObjectType.PointCloud, 9.0f,true);
            DisplayObjectOption[] vzVectorOptions = DisplayObjectOption.CreateDisplayOptionArray((int)DrawItem.OPT1vzVector, 3, DisplayObjectType.Vector_z, 1.0f,false);

            XYZOptions[0].DrawColor = Color.Gray;
            XYZOptions[1].DrawColor = Color.DarkGray;
            XYZOptions[2].DrawColor = Color.Silver;
            XYZOptions[3].DrawColor = Color.LightGray;
            XYZOptions[4].DrawColor = Color.Gainsboro;

            PathOptions[0].DrawColor = Color.LimeGreen;
            PathOptions[1].DrawColor = Color.Cyan;
            PathOptions[2].DrawColor = Color.Gold;

            PointOptions[0].DrawColor = Color.LimeGreen;
            PointOptions[1].DrawColor = Color.Cyan;
            PointOptions[2].DrawColor = Color.Gold;

            vzVectorOptions[0].DrawColor = Color.Blue;
            vzVectorOptions[1].DrawColor = Color.Blue;
            vzVectorOptions[2].DrawColor = Color.Blue;

            _displayCtrl.AddDisplayOption(XYZOptions);
            _displayCtrl.AddDisplayOption(PathOptions);
            _displayCtrl.AddDisplayOption(PointOptions);
            _displayCtrl.AddDisplayOption(vzVectorOptions);

            createButton(optButtons, DrawItem.OPT3Path, PathOptions[2].DrawColor);
            createButton(optButtons, DrawItem.OPT2Path, PathOptions[1].DrawColor);
            createButton(optButtons, DrawItem.OPT1Path, PathOptions[0].DrawColor);
            
            createButton(xyzButtons, DrawItem.XYZ5, XYZOptions[4].DrawColor);
            createButton(xyzButtons, DrawItem.XYZ4, XYZOptions[3].DrawColor);
            createButton(xyzButtons, DrawItem.XYZ3, XYZOptions[2].DrawColor);
            createButton(xyzButtons, DrawItem.XYZ2, XYZOptions[1].DrawColor);
            createButton(xyzButtons, DrawItem.XYZ1, XYZOptions[0].DrawColor);

        }

        void createButton(Dictionary<DrawItem, Button> dic,DrawItem drawItem,Color backColor)
        {
            Button btn = new Button();
            btn.BackColor = backColor;
            btn.Text = drawItem.ToString();
            btn.Dock = DockStyle.Top;
            btn.Height = 50;
            btn.AllowDrop = true;
            btn.MouseClick += Btn_MouseClick;
            btn.DragEnter += Btn_DragEnter;
            btn.DragDrop += Btn_DragDrop;
            dic.Add(drawItem, btn);
            panel1.Controls.Add(btn);
        }

        void loadFile(DrawItem drawItem,string filePath)
        {
            string ext = Path.GetExtension(filePath).ToLower();
            string fileName = Path.GetFileNameWithoutExtension(filePath);
            switch (ext)
            {
                case ".xyz":
                    RPointCloud cloud = new RPointCloud();
                    cloud.LoadFromFile(filePath, true);
                    _displayCtrl.GetDisplayObjectOption((int)drawItem).Name = fileName;
                    _displayCtrl.BuildPointCloud(cloud, (int)drawItem, true, true);
                    break;

                case ".opt":
                    ObjectGroup group = new ObjectGroup(fileName);
                    group.LoadMultiPathOPT(filePath,true);

                    //Polyline line = new Polyline();
                    //line.LoadFromOPTFile(filePath, true);
                    _displayCtrl.GetDisplayObjectOption((int)drawItem).Name = fileName;
                    _displayCtrl.BuildMultiPath(group, (int)drawItem, true, true);

                    _displayCtrl.GetDisplayObjectOption((int)drawItem+3).Name = fileName;
                    _displayCtrl.BuildPointCloud(group, (int)drawItem+3, false, true);

                    _displayCtrl.GetDisplayObjectOption((int)drawItem + 6).Name = fileName;
                    _displayCtrl.GetDisplayObjectOption((int)drawItem + 6).IsDisplay = false;
                    _displayCtrl.BuildMultiPathVector(group, (int)drawItem+6, false, true);

                    break;

                default:

                    break;
            }
            _displayCtrl.UpdateDataGridView();
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

                loadFile(dropedBtn,files[0]);
            }
        }

        private void Btn_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                string[] files = (string[])e.Data.GetData(DataFormats.FileDrop);
                if (files.Length > 0)
                {
                    string ext = Path.GetExtension(files[0]).ToLower();
                    DrawItem dropedBtn = getPressedButton((Button)sender);

                    if(dropedBtn>=DrawItem.XYZ1 && dropedBtn <= DrawItem.XYZ5 && ext == ".xyz") e.Effect = DragDropEffects.Move;
                    if (dropedBtn >= DrawItem.OPT1Path && dropedBtn <= DrawItem.OPT3Path && ext == ".opt") e.Effect = DragDropEffects.Move;
                }
            }
        }

        private void Btn_MouseClick(object sender, MouseEventArgs e)
        {
            using (OpenFileDialog op = new OpenFileDialog())
            {
                DrawItem dropedBtn = getPressedButton((Button)sender);
                if (dropedBtn >= DrawItem.XYZ1 && dropedBtn <= DrawItem.XYZ5) op.Filter = "XYZ cloud file|*.xyz";
                if (dropedBtn >= DrawItem.OPT1Path && dropedBtn <= DrawItem.OPT3Path) op.Filter = "OPT path file|*.opt";

                if (op.ShowDialog() == DialogResult.OK)
                {
                    loadFile(dropedBtn,op.FileName);
                }
            }
        }
    }
    public enum DrawItem:int
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
    }
}

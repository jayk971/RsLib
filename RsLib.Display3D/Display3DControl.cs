﻿using OpenTK;
using OpenTK.Graphics.OpenGL;
using RsLib.Common;
using RsLib.Display3D.Properties;
using RsLib.LogMgr;
using RsLib.PointCloudLib;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Linq.Expressions;
using System.Security.AccessControl;
using System.Windows.Forms;
namespace RsLib.Display3D
{
    public partial class Display3DControl : UserControl
    {
        public event Action<PointCloud> AfterPointsSelected;
        public event Action<Point3D> AfterPointSelected;

        public event Action AfterClearButtonPressed;
        public event Action MiddleMouseButtonClick;
        public event Action<int> AfterPolylineSelected;
        public event Action<double[], double[]> AfterMediemButtonClick;
        public event Action<Polyline> AfterLineDrawn;
        public bool LockRotate = false;
        bool _isColorDialogOpen = false;
        bool _isMouseOnCell = false;
        const int splitContainerPanel1MinSize = 300;
        const int _typeIndex = 0;
        const int _visibleIndex = 1;
        const int _nameIndex = 2;
        const int _idIndex = 5;
        const int _colorIndex = 4;
        const int _sizeIndex = 3;
        FormAddSelectPath formAdd = new FormAddSelectPath();
        // key : object ID, value : list of select paths;
        Dictionary<int, List<int>> _SelectedPathIndex = new Dictionary<int, List<int>>();
        int _CurrentSelectLineIndex = -1;
        bool enableMultipleSelect = false;
        public bool EnableMultipleSelect
        {
            get => enableMultipleSelect;
            set
            {
                enableMultipleSelect = value;
                MultipleSelectToolStripMenuItem.Visible = value;
            }
        }

        eCoordPlane currentPlane = eCoordPlane.None;
        public Display3DControl(int listNum = 1)
        {
            InitializeComponent();
            _maxDisplayList = listNum + 1;

            _glControl = new GLControl();
            _glControl.Load += GlControl_Load;
            _glControl.Paint += GlControl_Paint;
            _glControl.Dock = DockStyle.Fill;
            _glControl.MouseDown += GlControl_MouseDown;
            _glControl.MouseUp += GlControl_MouseUp;
            _glControl.MouseMove += GlControl_MouseMove;
            _glControl.MouseWheel += GlControl_MouseWheel;
            _glControl.MouseClick += GlControl_MouseClick;
            _glControl.MouseDoubleClick += GlControl_MouseDoubleClick;
            _glControl.SizeChanged += _glControl_SizeChanged;

            //_glControl.Parent = splitContainer1.Panel2;
            pnl_GLControl.Controls.Add(_glControl);

            dataGridView1.CellValueChanged += DataGridView1_CellValueChanged;
            dataGridView1.CellContentClick += DataGridView1_CellContentClick;
            dataGridView1.CellClick += DataGridView1_CellClick;
            dataGridView1.MouseClick += DataGridView1_MouseClick;
            dataGridView1.CellMouseMove += DataGridView1_CellMouseMove;
            dataGridView1.CellMouseLeave += DataGridView1_CellMouseLeave;
            dataGridView1.CellValidating += DataGridView1_CellValidating;
            //GlControl_Load(null, null);
            splitContainer1.Panel1Collapsed = true;
            splitContainer2.Panel2Collapsed = true;

            formAdd.SaveSelectPathMOD += FormAdd_SaveSelectPathMod;
            formAdd.SaveSelectPathOPT += FormAdd_SaveSelectPathOPT;
            formAdd.ClearSelectPath += FormAdd_ClearSelectPath;

            Log.Start();
            trackBar_RotateSensitivity.Value = (int)(Settings.Default.Sensitivity * 10);
            pnl_ColorGradient.Visible = false;
        }

        public void ShowColorGradientControl(bool show)
        {
            pnl_ColorGradient.Visible = show;
        }
        public void SetColorGradientCtrl(ColorGradientControl colorCtrl)
        {
            pnl_ColorGradient.Controls.Clear();
            colorCtrl.Dock = DockStyle.Fill;
            pnl_ColorGradient.Controls.Add(colorCtrl);
        }
        public bool AddDisplayOption(DisplayObjectOption option)
        {
            if (_displayOption.ContainsKey(option.ID))
            {
                return false;
            }
            else
            {
                _displayOption.Add(option.ID, option);
                return true;
            }
        }
        public void AddDisplayOption(DisplayObjectOption[] option)
        {
            for (int i = 0; i < option.Length; i++)
            {
                if (_displayOption.ContainsKey(option[i].ID))
                {
                }
                else
                {
                    _displayOption.Add(option[i].ID, option[i]);
                }
            }
        }
        public void Clear(bool clearOptions = false)
        {
            _displayObject.Clear();
            treeView1.Nodes.Clear();
            ShowColorGradientControl(false);
            foreach (var item in _displayOption)
            {
                DisplayObjectOption objOption = item.Value;
                GL.NewList(objOption.ID, ListMode.Compile);
                GL.EndList();
            }
            if (clearOptions)
            {
                _displayOption.Clear();
            }
            _maxPoint = new Vector3(float.MinValue, float.MinValue, float.MinValue);
            _minPoint = new Vector3(float.MaxValue, float.MaxValue, float.MaxValue);
            ResetView();
            UpdateDataGridView();
            AfterClearButtonPressed?.Invoke();
        }
        public void Clear(bool resetView ,bool clearOptions = false)
        {
            _displayObject.Clear();
            treeView1.Nodes.Clear();
            ShowColorGradientControl(false);
            foreach (var item in _displayOption)
            {
                DisplayObjectOption objOption = item.Value;
                GL.NewList(objOption.ID, ListMode.Compile);
                GL.EndList();
            }
            if (clearOptions)
            {
                _displayOption.Clear();
            }
            _maxPoint = new Vector3(float.MinValue, float.MinValue, float.MinValue);
            _minPoint = new Vector3(float.MaxValue, float.MaxValue, float.MaxValue);
           if(resetView) ResetView();
            UpdateDataGridView();
            AfterClearButtonPressed?.Invoke();
        }

        public void ReBuildAll()
        {
            foreach (var item in _displayOption)
            {
                ReBuild_ChangeColorSize(item.Key);
            }
        }
        public void ReBuild_ChangeColorSize(int id)
        {
            if (this.InvokeRequired)
            {
                Action<int> action = new Action<int>(ReBuild_ChangeColorSize);
                this.Invoke(action, id);
            }
            else
            {
                if (_displayOption.ContainsKey(id))
                {
                    DisplayObjectOption option = _displayOption[id];
                    if (_displayObject.ContainsKey(id) == false) return;
                    Type objectType = _displayObject[id].GetType();

                    switch (option.DisplayType)
                    {
                        case DisplayObjectType.PointCloud:
                            if (objectType == typeof(ObjectGroup))
                                BuildPointCloud((ObjectGroup)_displayObject[id], id, false, false);
                            else if (objectType == typeof(PointCloud))
                                BuildPointCloud((PointCloud)_displayObject[id], id, false, false);
                            break;
                        case DisplayObjectType.Vector_z:
                            if (objectType == typeof(ObjectGroup))
                                BuildVector((ObjectGroup)_displayObject[id], id, false, false);
                            else if (objectType == typeof(Polyline))
                                BuildVector((Polyline)_displayObject[id], id, false, false);
                            break;
                        case DisplayObjectType.Vector_y:
                            if (objectType == typeof(ObjectGroup))
                                BuildVector((ObjectGroup)_displayObject[id], id, false, false);
                            else if (objectType == typeof(Polyline))
                                BuildVector((Polyline)_displayObject[id], id, false, false);
                            break;
                        case DisplayObjectType.Vector_x:
                            if (objectType == typeof(ObjectGroup))
                                BuildVector((ObjectGroup)_displayObject[id], id, false, false);
                            else if (objectType == typeof(Polyline))
                                BuildVector((Polyline)_displayObject[id], id, false, false);
                            break;
                        case DisplayObjectType.Path:
                            if (objectType == typeof(ObjectGroup))
                                BuildPath((ObjectGroup)_displayObject[id], id, false, false);

                            else if (objectType == typeof(Polyline))
                                BuildPath((Polyline)_displayObject[id], id, false, false);
                            break;
                        case DisplayObjectType.Point:
                            BuildPoint((Point3D)_displayObject[id], id, false, false);
                            break;
                        default:

                            break;
                    }
                }
            }
        }
        public Object3D GetDisplayObject(int index)
        {
            if (index > 0)
            {
                if (_displayObject.ContainsKey(index)) return _displayObject[index];
                else return null;
            }
            else return null;
        }
        public DisplayObjectOption GetDisplayObjectOption(int index)
        {

            if (index > 0)
            {
                if (_displayOption.ContainsKey(index)) return _displayOption[index];
                else return null;
            }
            else return null;

        }
        //public void SetObjectVisible(int id, bool visible)
        //{
        //    if (_displayOption.ContainsKey(id))
        //    {
        //        _displayOption[id].IsDisplay = visible;
        //        //updateDataGridView();
        //    }
        //}
        public void ResetView()
        {
            _rotationMatrix = Matrix4.Identity;
            _translation = new Vector3();
            //_rotation = new Vector3();
            _scale = 1.0f;
        }
        private void DataGridView1_CellValidating(object sender, DataGridViewCellValidatingEventArgs e)
        {
            int c = e.ColumnIndex;
            int r = e.RowIndex;
            if (r == -1 || c == -1) return;

            int id = (int)dataGridView1.Rows[r].Cells[_idIndex].Value;
            if (c == _sizeIndex)
            {
                float newValue;
                if (float.TryParse(dataGridView1.Rows[r].Cells[_sizeIndex].Value.ToString(), out newValue) == false)
                {
                    e.Cancel = true;
                }
                else
                {

                }

            }
        }

        private void DataGridView1_CellMouseLeave(object sender, DataGridViewCellEventArgs e)
        {
            _isMouseOnCell = false;
        }

        private void DataGridView1_CellMouseMove(object sender, DataGridViewCellMouseEventArgs e)
        {
            _isMouseOnCell = true;
            int c = e.ColumnIndex;
            int r = e.RowIndex;
            if (r == -1 || c == -1) return;
            dataGridView1.Rows[r].Cells[c].ToolTipText = dataGridView1.Rows[r].Cells[c].Value.ToString();
        }


        private void DataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int c = e.ColumnIndex;
            int r = e.RowIndex;
            if (r == -1 || c == -1) return;
            int id = (int)dataGridView1.Rows[r].Cells[_idIndex].Value;

            if (_displayOption.ContainsKey(id))
            {
                if (_displayOption[id].IsSelectable)
                {
                    if (_CurrentSelectObjectIndex != id) _haveClosestPoint = false;
                    _CurrentSelectObjectIndex = id;
                    lbl_Selectable.Visible = false;
                }
                else
                {
                    lbl_Selectable.Visible = true;
                    clearDataGridSelection();
                }
            }
            else
            {
                lbl_Selectable.Visible = false;
                clearDataGridSelection();
            }
            UpdateLineIndexComboBox();
        }
        public void UpdateLineIndexComboBox()
        {
            toolCmb_LineIndex.Items.Clear();
            toolCmb_LineIndex.Items.Add("None");
            _haveSelectPath = false;
            if (_CurrentSelectObjectIndex < 0) return;
            var obj = _displayObject[_CurrentSelectObjectIndex] as ObjectGroup;
            if (obj != null)
            {
                foreach (var item in obj.Objects)
                {
                    string name = item.Key;
                    Polyline p = item.Value as Polyline;
                    if(p != null)
                    {
                        LineOption lineOption = p.GetOption(typeof(LineOption)) as LineOption;
                        if(lineOption != null)
                        {
                            if(toolCmb_LineIndex.Items.Contains(lineOption.LineIndex) == false)
                            {
                                toolCmb_LineIndex.Items.Add(lineOption.LineIndex);
                            }
                        }
                    }
                }
            }



        }
        void clearDataGridSelection()
        {
            _CurrentSelectObjectIndex = -1;
            _CurrentSelectLineIndex = -1;
            _haveClosestPoint = false;
            dataGridView1.ClearSelection();
        }
        private void DataGridView1_MouseClick(object sender, MouseEventArgs e)
        {
            if (_isMouseOnCell == false)
            {
                clearDataGridSelection();

                lbl_Selectable.Visible = false;
                UpdateLineIndexComboBox();
            }
        }

        private void DataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            int c = e.ColumnIndex;
            int r = e.RowIndex;
            if (r == -1 || c == -1) return;
            int id = (int)dataGridView1.Rows[r].Cells[_idIndex].Value;

            if (c == _visibleIndex)
                dataGridView1.CommitEdit(DataGridViewDataErrorContexts.Commit);

            else if (c == _colorIndex)
            {
                if (_isColorDialogOpen == false)
                {
                    //ThreadPool.QueueUserWorkItem(new WaitCallback(showColorDialogTd), id);
                    showColorDialogTd(id);
                }
                else
                {
                    MessageBox.Show("Color dialog has been opened. Please close the color dialog first.", "Color Dialog Opened!!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
        }

        private void DataGridView1_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            int c = e.ColumnIndex;
            int r = e.RowIndex;
            if (r == -1 || c == -1) return;

            int id = (int)dataGridView1.Rows[r].Cells[_idIndex].Value;

            if (c == _visibleIndex)
            {
                bool isCheck = (bool)dataGridView1.Rows[r].Cells[_visibleIndex].Value;
                if (_displayOption.ContainsKey(id))
                {
                    bool oldCheck = _displayOption[id].IsDisplay;
                    if (oldCheck != isCheck)
                    {
                        Log.Add($"Change ID {id} is visible. {isCheck}.", MsgLevel.Trace);

                        _displayOption[id].IsDisplay = isCheck;
                    }
                }

            }
            else if (c == _sizeIndex)
            {
                if (_displayOption.ContainsKey(id))
                {
                    float newValue = float.Parse(dataGridView1.Rows[r].Cells[_sizeIndex].Value.ToString());
                    float oldValue = _displayOption[id].DrawSize;
                    if (oldValue != newValue)
                    {
                        _displayOption[id].DrawSize = newValue;
                        Log.Add($"Change ID {id} 's size. {newValue}.", MsgLevel.Trace);

                        ReBuild_ChangeColorSize(id);
                    }
                }
            }

        }
        public void ClearSelectedObjectList(int id)
        {
            if (id == -1) return;
            if (_displayObject.ContainsKey(id) == false) return;
            if (_displayOption.ContainsKey(id) == false) return;
            if (_SelectedPathIndex.ContainsKey(id) == false) return;

            _SelectedPathIndex[id].Clear();

        }
        void showColorDialogTd(object obj)
        {
            _isColorDialogOpen = true;

            int id = (int)obj;
            using (ColorDialog cd = new ColorDialog())
            {
                if (cd.ShowDialog() == DialogResult.OK)
                {
                    if (_displayOption.ContainsKey(id))
                    {
                        Color selectColor = cd.Color;
                        Color oldColor = _displayOption[id].DrawColor;

                        if (selectColor != oldColor)
                        {
                            _displayOption[id].DrawColor = selectColor;
                            Log.Add($"Change ID {id} 's color. {selectColor.A} {selectColor.R} {selectColor.G} {selectColor.B}.", MsgLevel.Trace);
                            ReBuild_ChangeColorSize(id);
                            UpdateDataGridView();
                        }

                    }
                }
            }
            _isColorDialogOpen = false;
        }

        public void UpdateDataGridView()
        {
            if (this.InvokeRequired)
            {
                Action action = new Action(UpdateDataGridView);
                this.Invoke(action);
            }
            else
            {

                dataGridView1.Rows.Clear();
                foreach (var item in _displayOption)
                {
                    DisplayObjectOption option = item.Value;
                    if (_displayObject.ContainsKey(option.ID) == false) continue;
                    if (option.IsShowAtDataGrid)
                    {
                        if (option.Name != "")
                        {
                            dataGridView1.Rows.Add(option.ToDataGridRowObject());
                            int currentIndex = dataGridView1.Rows.Count - 1;
                            DataGridViewButtonCell btn = dataGridView1.Rows[currentIndex].Cells[_colorIndex] as DataGridViewButtonCell;
                            btn.FlatStyle = FlatStyle.Flat;
                            btn.Style.BackColor = option.DrawColor;

                            if (option.IsSelectable == false)
                            {
                                dataGridView1.Rows[currentIndex].DefaultCellStyle.BackColor = Color.Gray;
                            }
                        }
                    }
                }
                _CurrentSelectObjectIndex = -1;
                _CurrentSelectLineIndex = -1;
                UpdateLineIndexComboBox();
            }
        }


        private void Form_FormClosed(object sender, FormClosedEventArgs e)
        {
        }

        private void btn_Edit_Click(object sender, EventArgs e)
        {
            splitContainer1.Panel1Collapsed = !splitContainer1.Panel1Collapsed;
            if (splitContainer1.Panel1Collapsed == false) splitContainer1.SplitterDistance = splitContainerPanel1MinSize;
        }

        private void btn_ResizeView_Click(object sender, EventArgs e)
        {
            ResetView();
        }

        private void btn_ClearObject_Click(object sender, EventArgs e)
        {
            Clear();
        }

        private void btn_Color_Click(object sender, EventArgs e)
        {
            FormChangeDefaultColor form = new FormChangeDefaultColor();
            form.FormClosed += Form_FormClosed;
            form.StartPosition = FormStartPosition.CenterParent;
            form.ShowDialog();
        }

        private void btn_PickPoint_Click(object sender, EventArgs e)
        {
            if (_pickMode == PointPickMode.One)
            {
                pickMode_None();
            }
            else
            {
                pickMode_Single();
            }
        }

        private void Display3DControl_SizeChanged(object sender, EventArgs e)
        {
            if (splitContainer1.Width > splitContainerPanel1MinSize)
            {
                splitContainer1.SplitterDistance = splitContainerPanel1MinSize;
            }
        }

        private void btn_Update_Click(object sender, EventArgs e)
        {
            ReBuildAll();
            UpdateDataGridView();
        }

        private void measureDistanceToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (_pickMode == PointPickMode.Two)
            {
                pickMode_None();
            }
            else
            {
                pickMode_Measure();
            }
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            pickMode_None();
        }
        void pickMode_None()
        {
            _pickMode = PointPickMode.None;
            lbl_PickPointMode.Text = "None";
            lbl_PickPointMode.Image = Resources.unavailable_48px;
            treeView1.Nodes.Clear();
            splitContainer2.Panel2Collapsed = true;
            toolStrip_MultipleSelect.Visible = false;

        }
        void pickMode_Single()
        {
            _pickMode = PointPickMode.One;
            lbl_PickPointMode.Text = "Pick Point - Middle Click";
            lbl_PickPointMode.Image = Resources.place_marker_30px;
            treeView1.Nodes.Clear();
            splitContainer2.Panel2Collapsed = false;

        }
        void pickMode_Measure()
        {
            _pickMode = PointPickMode.Two;
            lbl_PickPointMode.Text = "Pick 1st Point - Middle Click";
            lbl_PickPointMode.Image = Resources.width_30px;
            treeView1.Nodes.Clear();
            splitContainer2.Panel2Collapsed = false;

        }
        void pickMode_Multiple()
        {
            _pickMode = PointPickMode.Multiple;
            lbl_PickPointMode.Text = "Select Range : Shift + Middle Click\tSelect Each : Ctrl + Middle Click";
            lbl_PickPointMode.Image = Resources.map_pinpoint_30px;
            treeView1.Nodes.Clear();
            splitContainer2.Panel2Collapsed = false;

        }
        void pickMode_Draw()
        {
            _pickMode = PointPickMode.Draw;
            lbl_PickPointMode.Text = "Draw Polyline";
            lbl_PickPointMode.Image = Resources.autograph_48px;
            treeView1.Nodes.Clear();
            splitContainer2.Panel2Collapsed = false;

        }
        private void btn_SaveAs_Click(object sender, EventArgs e)
        {
            if (_CurrentSelectObjectIndex <= 1)
            {
                Log.Add("Select index = -1. Didn't select object.", MsgLevel.Warn);
                MessageBox.Show("Please select object first.", "Save file as xyz fail.", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (_displayObject.ContainsKey(_CurrentSelectObjectIndex) == false)
            {
                Log.Add($"Select index = {_CurrentSelectObjectIndex}. Display objects didn't contain {_CurrentSelectObjectIndex}.", MsgLevel.Warn);
                MessageBox.Show("No data to be saved.", "Save file as xyz fail.", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            try
            {
                Type objectType = _displayObject[_CurrentSelectObjectIndex].GetType();

                using (SaveFileDialog sf = new SaveFileDialog())
                {
                    sf.Filter = "XYZ point cloud|*.xyz";
                    if (sf.ShowDialog() == DialogResult.OK)
                    {
                        string filePath = sf.FileName;
                        try
                        {
                            if (objectType == typeof(Point3D))
                            {
                                if (_displayObject[_CurrentSelectObjectIndex] is Point3D obj)
                                {
                                    obj.Save(filePath);
                                }
                            }
                            else if (objectType == typeof(ObjectGroup))
                            {
                                if (_displayObject[_CurrentSelectObjectIndex] is ObjectGroup obj)
                                {
                                    obj.SaveXYZ(filePath);
                                }
                            }
                            else
                            {
                                if (_displayObject[_CurrentSelectObjectIndex] is PointCloud obj)
                                {
                                    obj.Save(filePath);
                                    //LayerPointCloud layer = new LayerPointCloud(obj, false, 0.3);
                                    //layer.Save(filePath);
                                }
                            }
                            Log.Add($"Save xyz cloud.{filePath}", MsgLevel.Info);
                            MessageBox.Show($"Save xyz point cloud done.\n{filePath}", "Save file done.", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        catch (Exception ex)
                        {
                            Log.Add($"Save xyz cloud exception.", MsgLevel.Alarm, ex);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Log.Add($"Save XYZ cloud exception.", MsgLevel.Alarm, ex);
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (_GLUpdateDone)
                _glControl.Invalidate();
            toolLbl_CurrentPlane.Text = currentPlane.ToString();
            toolStatusLbl_CurrentSelectLineIndex.Text = _CurrentSelectLineIndex.ToString();
            toolStatusLbl_SelectObjectIndex.Text = _CurrentSelectObjectIndex.ToString();
            toolLblScale.Text = _scale.ToString("F1");
            GC.Collect();
        }

        private void saveABBModFileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ObjectGroup selectGroup = selectObjectGroup(_CurrentSelectObjectIndex);
            if (selectGroup == null) return;
            saveABBMod(selectGroup, false);
        }
        private ObjectGroup selectObjectGroup(int selectIndex)
        {
            try
            {
                ObjectGroup output = null;
                if (selectIndex == -1)
                {
                    Log.Add("Select index = -1. Didn't select path.", MsgLevel.Warn);
                    return null;
                }
                if (_displayObject.ContainsKey(selectIndex) == false)
                {
                    //Log.Add($"Select index = {selectIndex}. Display objects didn't contain {selectIndex}.", MsgLevel.Warn);
                    return null;

                }
                output = _displayObject[selectIndex] as ObjectGroup;

                if (output == null)
                {
                    Log.Add($"Select index = {selectIndex}. Selected object  type is not ObjectGroup.", MsgLevel.Warn);
                }
                return output;
            }
            catch (Exception ex)
            {
                Log.Add($"Select object group exception.", MsgLevel.Alarm, ex);
                return null;
            }
        }
        private void saveABBMod(ObjectGroup selectObjGroup, bool isSaveRobTarget)
        {
            try
            {
                ObjectGroup obj = selectObjGroup;
                if (obj == null) return;
                using (SaveFileDialog sf = new SaveFileDialog())
                {
                    sf.Filter = "ABB mod File|*.mod";
                    if (sf.ShowDialog() == DialogResult.OK)
                    {
                        string outputFilePath = sf.FileName;
                        obj.SaveABBModPath(outputFilePath, isSaveRobTarget);
                        Log.Add($"Save mod. {outputFilePath}", MsgLevel.Info);
                        MessageBox.Show($"ABB Path Mod is saved.\n{outputFilePath}", "MOD file saved", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
            catch (Exception ex)
            {
                Log.Add($"Save ABB Path Mod exception.", MsgLevel.Alarm, ex);
            }
        }

        private void treeView1_AfterSelect(object sender, TreeViewEventArgs e)
        {
            //TreeNode selectNode = e.Node;
            //if (selectNode.Text.Contains("LineIndex"))
            //{
            //    string nodeText = selectNode.Text;
            //    string[] splitData = nodeText.Split(':');
            //    if (splitData.Length == 2)
            //    {
            //        if (int.TryParse(splitData[1], out _CurrentSelectLineIndex) == false)
            //        {
            //            _CurrentSelectLineIndex = -1;
            //        }
            //    }
            //    else _CurrentSelectLineIndex = -1;
            //}
            //else _CurrentSelectLineIndex = -1;
        }
        private void addSelectLine(int selectIndex)
        {
            if (_CurrentSelectObjectIndex < 0) return;
            if (selectIndex < 0) return;
            if (_SelectedPathIndex.ContainsKey(_CurrentSelectObjectIndex) == false)
            {
                _SelectedPathIndex.Add(_CurrentSelectObjectIndex, new List<int>() { selectIndex });
            }
            else
            {
                if (_SelectedPathIndex[_CurrentSelectObjectIndex].Contains(selectIndex) == false) 
                {
                    _SelectedPathIndex[_CurrentSelectObjectIndex].Add (selectIndex);
                }
            }
            formAdd.UpdateSelectPath(_SelectedPathIndex);

        }
        private void addToolStripMenuItem_Click(object sender, EventArgs e)
        {
            addSelectLine(_CurrentSelectLineIndex);
            if(formAdd.Visible == false)
                formAdd.Show();
        }

        private void FormAdd_SaveSelectPathOPT()
        {
            try
            {
                ObjectGroup output = selectPartPaths();
                if (output != null)
                    SaveOPT(output);

            }
            catch (Exception ex)
            {
                Log.Add($"Save ABB Path OPT exception.", MsgLevel.Alarm, ex);
            }
        }

        private void SaveOPT(ObjectGroup obj)
        {
            using (SaveFileDialog sf = new SaveFileDialog())
            {
                sf.Filter = "OPT file|*.opt";
                if(sf.ShowDialog() == DialogResult.OK)
                {
                    obj.SaveOPT(sf.FileName);
                }
            }
        }
        private void FormAdd_ClearSelectPath()
        {
            clearSelectPathIndex();
        }

        private void FormAdd_SaveSelectPathMod(bool isSaveRobtarget)
        {


            try
            {
                ObjectGroup output = selectPartPaths();
                if (output != null)
                    saveABBMod(output, isSaveRobtarget);

            }
            catch (Exception ex)
            {
                Log.Add($"Save ABB Path Mod exception.", MsgLevel.Alarm, ex);
            }

        }
        private ObjectGroup selectPartPaths()
        {
            try
            {
                ObjectGroup output = new ObjectGroup("SelectPath");
                if (output == null) return null;

                foreach (var item in _SelectedPathIndex)
                {
                    int selectObjIndex = item.Key;
                    List<int> selectPartPath = item.Value;
                    ObjectGroup selectObj = selectObjectGroup(selectObjIndex);
                    if (selectObj == null) return null;
                    int outputLineIndex = 0;
                    for (int i = 0; i < selectPartPath.Count; i++)
                    {
                        foreach (var item2 in selectObj.Objects)
                        {
                            string name = item2.Key;
                            if (item2.Value is Polyline p)
                            {
                                if (p.GetOption(typeof(LineOption)) is LineOption lineOption)
                                {
                                    if (lineOption.LineIndex == selectPartPath[i])
                                    {
                                        Polyline newPl = p.DeepClone();
                                        newPl.SetLineIndexForAllPts(outputLineIndex);
                                        if (newPl.GetOption(typeof(LineOption)) is LineOption lineOption2)
                                        {
                                            lineOption2.LineIndex = outputLineIndex;
                                            output.Add($"{selectObjIndex}_{outputLineIndex}", newPl);
                                            outputLineIndex++;
                                        }
                                    }
                                }
                            }
                        }
                    }

                    //foreach (var item2 in selectObj.Objects)
                    //{
                    //    string name = item2.Key;
                    //    Polyline p = item2.Value as Polyline;
                    //    if (p != null)
                    //    {
                    //        LineOption lineOption = p.GetOption(typeof(LineOption)) as LineOption;
                    //        if (lineOption != null)
                    //        {
                    //            if (selectPartPath.Contains(lineOption.LineIndex))
                    //            {
                    //                output.Add($"{selectObjIndex}_{outputLineIndex}", p);
                    //                outputLineIndex++;
                    //            }
                    //        }
                    //    }
                    //}
                }

                if (output.DataCount <= 0)
                {
                    MessageBox.Show($"Selected path data is empty. Cannot be saved as OPT", "OPT file save fail", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return null;
                }
                return output;
            }
            catch (Exception ex)
            {
                Log.Add($"Select part of paths exception.", MsgLevel.Alarm, ex);
                return null;
            }
        }
        private void clearSelectPathIndex()
        {
            _SelectedPathIndex.Clear();
        }
        private void saveABBModFileWithRobTargetToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ObjectGroup selectGroup = selectObjectGroup(_CurrentSelectObjectIndex);
            if (selectGroup == null) return;
            saveABBMod(selectGroup, true);
        }

        private void clearCollectionToolStripMenuItem_Click(object sender, EventArgs e)
        {
            clearSelectPathIndex();
        }

        private void reversePathToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ObjectGroup selectObj = selectObjectGroup(_CurrentSelectObjectIndex);
            if (selectObj == null) return;
            Polyline p = selectObj.SelectPolyine(_CurrentSelectLineIndex);
            if (p != null)
            {
                p.Reverse();
                p.CalculatePathDirectionAsVy();
                ReBuildAll();
            }
        }

        private void treeView1_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {

            }
        }

        private void saveOPTFileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ObjectGroup selectGroup = selectObjectGroup(_CurrentSelectObjectIndex);
            bool tempSaveAll = false;
            if (selectGroup == null) return;
            if (_SelectedPathIndex.Count == 0)
            {
                DialogResult dr =  MessageBox.Show("Save all path of the selected item to opt file?", "Save all path?", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (dr == DialogResult.No) return;
                else
                {
                    tempSaveAll = true;
                    addAllToCollectionToolStripMenuItem_Click(null, null);
                }
            }
            ObjectGroup selects = selectPartPaths();
            if (selects != null)
            {
                using (SaveFileDialog sf = new SaveFileDialog())
                {
                    sf.Filter = "OPT file|*.opt|OPT2 file|*.opt2";
                    if (sf.ShowDialog() == DialogResult.OK)
                    {
                        string filePath = sf.FileName;
                        int s = sf.FilterIndex;
                        try
                        {
                            if(s == 1) selects.SaveOPT(filePath);
                            else selects.SaveOPT2(filePath);

                            if(tempSaveAll)
                            {
                                _SelectedPathIndex.Clear();
                            }
                            Log.Add($"Save opt path. {filePath}", MsgLevel.Info);
                            MessageBox.Show($"Save opt path done.\n{filePath}", "Save file done.", MessageBoxButtons.OK, MessageBoxIcon.Information);

                        }
                        catch (Exception ex)
                        {
                            Log.Add($"Save opt path exception.", MsgLevel.Alarm, ex);

                        }
                    }
                }
            }
        }

        private void saveSelectedXYZPointCloudToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ObjectGroup selects = selectPartPaths();
            if (selects != null)
            {
                using (SaveFileDialog sf = new SaveFileDialog())
                {
                    sf.Filter = "XYZ point cloud|*.xyz";
                    if (sf.ShowDialog() == DialogResult.OK)
                    {
                        string filePath = sf.FileName;
                        try
                        {
                            selects.SaveXYZ(filePath);
                            Log.Add($"Save xyz cloud.{filePath}", MsgLevel.Info);
                            MessageBox.Show($"Save xyz point cloud done.\n{filePath}", "Save file done.", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        catch (Exception ex)
                        {
                            Log.Add($"Save xyz cloud exception.", MsgLevel.Alarm, ex);
                        }
                    }
                }
            }
        }

        private void toolBtn_ShowAddPathForm_Click(object sender, EventArgs e)
        {
            formAdd.Show();
        }

        public void SelectSegment(int selectObj,int segmentIndex)
        {
            _CurrentSelectLineIndex = segmentIndex;
            _CurrentSelectObjectIndex = selectObj;

            ObjectGroup obj = selectObjectGroup(_CurrentSelectObjectIndex);
            if (obj != null)
            {
                Polyline p = obj.SelectPolyine(_CurrentSelectLineIndex);
                _SelectPath = p;

                if (p != null)
                {
                    _haveSelectPath = true;
                    AfterPolylineSelected?.Invoke(_CurrentSelectLineIndex);
                }
                else
                {
                    _haveSelectPath = false;
                }
            }

        }

        private void toolCmb_LineIndex_SelectedIndexChanged(object sender, EventArgs e)
        {
            _haveSelectPath = false;
            _haveClosestPoint = false;
            if (toolCmb_LineIndex.Items.Count == 0) return;
            if (toolCmb_LineIndex.SelectedIndex <= 0) return;

            SelectSegment(_CurrentSelectObjectIndex, toolCmb_LineIndex.SelectedIndex - 1);
            //_CurrentSelectLineIndex = toolCmb_LineIndex.SelectedIndex - 1;
            //ObjectGroup obj = selectObjectGroup(_CurrentSelectObjectIndex);
            //if (obj != null)
            //{
            //    Polyline p = obj.SelectPolyine(_CurrentSelectLineIndex);
            //    if (p != null)
            //    {
            //        _SelectPath = p;
            //        _haveSelectPath = true;
            //        AfterPolylineSelected?.Invoke(_CurrentSelectLineIndex);
            //    }
            //}
            

        }

        private void addAllToCollectionToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ObjectGroup obj = selectObjectGroup(_CurrentSelectObjectIndex);
            if (obj == null) return;
            foreach (var item in obj.Objects)
            {
                Polyline p = item.Value as Polyline;
                if(p != null)
                {
                    LineOption lineOption = p.GetOption(typeof(LineOption)) as LineOption;
                    if(lineOption != null)
                    {
                        addSelectLine(lineOption.LineIndex);
                    }
                }
            }
        }
        public void SetView(eCoordPlane coordPlane,bool lockRotate)
        {
            LockRotate = lockRotate;
            if (currentPlane != coordPlane) _DrawPath.Clear();
            currentPlane = coordPlane;
            switch (currentPlane)
            {
                case eCoordPlane.XY:
                    SetXYView();
                    break;

                case eCoordPlane.XZ:
                    SetXZView();
                    break;
                
                case eCoordPlane.YZ:
                    SetYZView();
                    break;

                case eCoordPlane.YX:
                    SetYXView();
                    break;

                case eCoordPlane.ZX: 
                    SetZXView();
                    break;

                case eCoordPlane.ZY:
                    SetZYView();
                    break;
                default:
                    LockRotate = false;
                    break;
            }
            
            changeLockViewIcon();
        }
        private void trackBar_RotateSensitivity_Scroll(object sender, EventArgs e)
        {

        }

        private void changeRotateSensitivityToolStripMenuItem_Click(object sender, EventArgs e)
        {
            trackBar_RotateSensitivity.Visible = !trackBar_RotateSensitivity.Visible;
        }

        private void trackBar_RotateSensitivity_MouseUp(object sender, MouseEventArgs e)
        {
            int sensitivity = trackBar_RotateSensitivity.Value;
            float fSensitivity = (float)sensitivity / 10f;
            Settings.Default.Sensitivity = fSensitivity;
            Settings.Default.Save();
        }



        private void nud_ShiftX_ValueChanged(object sender, EventArgs e)
        {

        }
        private void manualShiftRotateItem()
        {

        }
        private void manualShiftRotateToolStripMenuItem_Click(object sender, EventArgs e)
        {
            tlp_LocalTransform.Visible = !tlp_LocalTransform.Visible;
        }
        public void SetXYView()
        {
            _rotationMatrix = Matrix4.Identity;

        }
        private void upToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SetView(eCoordPlane.XY, true);
        }
        public void SetBottomView()
        {
            _rotationMatrix = Matrix4.Identity;
            _rotationMatrix *= Matrix4.CreateFromAxisAngle(new Vector3(0, 1, 0), (float)Math.PI);
        }
        private void bottomViewToolStripMenuItem_Click(object sender, EventArgs e)
        {
            
            currentPlane = eCoordPlane.None;
            _DrawPath.Clear();
            SetBottomView();
        }
        public void SetYZView()
        {
            _rotationMatrix = Matrix4.Identity;
            _rotationMatrix *= Matrix4.CreateFromAxisAngle(new Vector3(0, 0, 1), -1 * (float)Math.PI / 2);
            _rotationMatrix *= Matrix4.CreateFromAxisAngle(new Vector3(1, 0, 0), -1 * (float)Math.PI / 2);
        }
        private void rightViewToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SetView(eCoordPlane.YZ, true);
        }
        public void SetLeftView()
        {
            _rotationMatrix = Matrix4.Identity;
            _rotationMatrix *= Matrix4.CreateFromAxisAngle(new Vector3(0, 0, 1), (float)Math.PI / 2);
            _rotationMatrix *= Matrix4.CreateFromAxisAngle(new Vector3(1, 0, 0), -1 * (float)Math.PI / 2);
        }

        private void leftViewToolStripMenuItem_Click(object sender, EventArgs e)
        {
            currentPlane = eCoordPlane.None;
            _DrawPath.Clear();
            SetLeftView();
        }
        public void SetFrontView()
        {
            _rotationMatrix = Matrix4.Identity;
            _rotationMatrix *= Matrix4.CreateFromAxisAngle(new Vector3(0, 0, 1), (float)Math.PI);
            _rotationMatrix *= Matrix4.CreateFromAxisAngle(new Vector3(1, 0, 0), -1 * (float)Math.PI / 2);
        }
        private void frontViewToolStripMenuItem_Click(object sender, EventArgs e)
        {
            currentPlane = eCoordPlane.None;
            _DrawPath.Clear();
            SetFrontView();
        }
        public void SetXZView()
        {
            _rotationMatrix = Matrix4.Identity;
            _rotationMatrix *= Matrix4.CreateFromAxisAngle(new Vector3(1, 0, 0), -1 * (float)Math.PI / 2);
        }

        private void backViewToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SetView(eCoordPlane.XZ, true);
        }

        private void smoothVxToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ObjectGroup selectObj = selectObjectGroup(_CurrentSelectObjectIndex);
            if (selectObj == null) return;
            Polyline p = selectObj.SelectPolyine(_CurrentSelectLineIndex);
            if (p != null)
            {
                p.SmoothVx();
                ReBuildAll();
            }
        }

        private void smoothVyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ObjectGroup selectObj = selectObjectGroup(_CurrentSelectObjectIndex);
            if (selectObj == null) return;
            Polyline p = selectObj.SelectPolyine(_CurrentSelectLineIndex);
            if (p != null)
            {
                p.SmoothVy();
                ReBuildAll();
            }
        }

        private void smoothVzToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            ObjectGroup selectObj = selectObjectGroup(_CurrentSelectObjectIndex);
            if (selectObj == null) return;
            Polyline p = selectObj.SelectPolyine(_CurrentSelectLineIndex);
            if (p != null)
            {
                p.SmoothVz();
                ReBuildAll();
            }
        }

        private void toolCmb_LineIndex_Click(object sender, EventArgs e)
        {

        }

        private void MultipleSelectToolStripMenuItem_Click(object sender, EventArgs e)
        {
            toolStrip_MultipleSelect.Visible = !toolStrip_MultipleSelect.Visible;
        }


        private void toolBtn_StartMultipleSelect_Click(object sender, EventArgs e)
        {
            if (_pickMode == PointPickMode.Multiple)
            {
                pickMode_None();
            }
            else
            {
                pickMode_Multiple();
            }
        }

        private void toolBtn_EndMultipleSelect_Click(object sender, EventArgs e)
        {
            pickMode_None();
            AfterPointsSelected?.Invoke(_multiSelectPoints);
        }

        private void toolBtn_ClearMultipleSelect_Click(object sender, EventArgs e)
        {
            _multiSelectPoints.Clear();

        }

        private void logToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FT_Functions.OpenFolder($"{System.Environment.CurrentDirectory}\\Log");
        }
        public void SetYXView()
        {
            _rotationMatrix = Matrix4.Identity;
            _rotationMatrix *= Matrix4.CreateFromAxisAngle(new Vector3(0, 0, 1), -1*(float)Math.PI / 2);
            _rotationMatrix *= Matrix4.CreateFromAxisAngle(new Vector3(1, 0, 0), -1 * (float)Math.PI);
        }

        private void yXToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SetView(eCoordPlane.YX, true);
        }
        public void SetZYView()
        {
            _rotationMatrix = Matrix4.Identity;
            _rotationMatrix *= Matrix4.CreateFromAxisAngle(new Vector3(0, 1,0), (float)Math.PI / 2);
        }

        private void zYToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SetView(eCoordPlane.ZY, true);
        }
        public void SetZXView()
        {
            _rotationMatrix = Matrix4.Identity;
            _rotationMatrix *= Matrix4.CreateFromAxisAngle(new Vector3(0, 1, 0), (float)Math.PI / 2);
            _rotationMatrix *= Matrix4.CreateFromAxisAngle(new Vector3(1, 0, 0), (float)Math.PI / 2);

        }
        public void SetDrawPath(Polyline pl)
        {
            _DrawPath = pl;
        }
        private void zXToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SetView(eCoordPlane.ZX, true);
        }

        private void toolBtn_LockView_Click(object sender, EventArgs e)
        {
            switchLockViewStatus();
            changeLockViewIcon();
        }
        private void switchLockViewStatus()
        {
            LockRotate = !LockRotate;
        }
        private void changeLockViewIcon()
        {
            toolLbl_Notify.Visible = LockRotate;
            toolBtn_LockView.Image = LockRotate ? Resources.lock_48px : Resources.padlock_48px;
            if (LockRotate == false)
            {
                currentPlane = eCoordPlane.None;
            }
        }

        private void drawSegmentToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void startDrawToolStripMenuItem_Click(object sender, EventArgs e)
        {
            pickMode_Draw();
        }

        private void endDrawToolStripMenuItem_Click(object sender, EventArgs e)
        {
            pickMode_None();
            if (_DrawPath.Count >= 2)
                AfterLineDrawn?.Invoke(_DrawPath);
            _DrawPath.Clear();

        }

        private void clearDrawToolStripMenuItem_Click(object sender, EventArgs e)
        {
            _DrawPath.Clear();
        }

        private void undoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            _DrawPath.RemoveLast();
        }

        private void saToolStripMenuItem_Click(object sender, EventArgs e)
        {

            if (_CurrentSelectObjectIndex <= 1)
            {
                Log.Add("Select index = -1. Didn't select object.", MsgLevel.Warn);
                MessageBox.Show("Please select object first.", "Save file as xyz fail.", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (_displayObject.ContainsKey(_CurrentSelectObjectIndex) == false)
            {
                Log.Add($"Select index = {_CurrentSelectObjectIndex}. Display objects didn't contain {_CurrentSelectObjectIndex}.", MsgLevel.Warn);
                MessageBox.Show("No data to be saved.", "Save file as xyz fail.", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            try
            {
                Type objectType = _displayObject[_CurrentSelectObjectIndex].GetType();

                using (SaveFileDialog sf = new SaveFileDialog())
                {
                    sf.Filter = "XYZ point cloud|*.xyz";
                    if (sf.ShowDialog() == DialogResult.OK)
                    {
                        string filePath = sf.FileName;
                        try
                        {
                            if (objectType == typeof(Point3D))
                            {
                                if (_displayObject[_CurrentSelectObjectIndex] is Point3D obj)
                                {
                                    obj.Save(filePath);
                                }
                            }
                            else if (objectType == typeof(ObjectGroup))
                            {
                                if (_displayObject[_CurrentSelectObjectIndex] is ObjectGroup obj)
                                {
                                    obj.SaveXYZ(filePath);
                                }
                            }
                            else
                            {
                                if (_displayObject[_CurrentSelectObjectIndex] is PointCloud obj)
                                {
                                    LayerPointCloud layer = new LayerPointCloud(obj, false, 0.3);
                                    layer.Save(filePath);
                                }
                            }
                            Log.Add($"Save xyz cloud.{filePath}", MsgLevel.Info);
                            MessageBox.Show($"Save xyz point cloud done.\n{filePath}", "Save file done.", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        catch (Exception ex)
                        {
                            Log.Add($"Save xyz cloud exception.", MsgLevel.Alarm, ex);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Log.Add($"Save XYZ cloud exception.", MsgLevel.Alarm, ex);
            }


        }

        private void saveImageToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (SaveFileDialog sf = new SaveFileDialog())
            {
                sf.Filter = "Display 3D Screen Shot Image|*.png";
                if(sf.ShowDialog() == DialogResult.OK)
                {
                    string filePath = sf.FileName;
                    SaveScreenShot(filePath);
                }
            }
        }

        private void copyImageIntoClipboardToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CaptureScreenShotIntoClipboard();
        }

        private void toolTbx_VectorLength_KeyPress(object sender, System.Windows.Forms.KeyPressEventArgs e)
        {
            e.Handled = FT_Functions.double_Positive_KeyPress(e.KeyChar);
        }

        private void toolTbx_VectorLength_KeyUp(object sender, KeyEventArgs e)
        {
            if(e.KeyCode == Keys.Enter)
            {
                _VectorLength = double.Parse(toolTbx_VectorLength.Text);
                ReBuildAll();
            }
        }
    }
}

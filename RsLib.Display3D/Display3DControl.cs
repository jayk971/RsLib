using OpenTK;
using OpenTK.Graphics.OpenGL;
using RsLib.Display3D.Properties;
using RsLib.LogMgr;
using RsLib.PointCloudLib;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
namespace RsLib.Display3D
{
    public partial class Display3DControl : UserControl
    {
        public event Action<Polyline> AfterPointsSelected;
        public event Action AfterClearButtonPressed;
        public event Action MiddleMouseButtonClick;

        bool _isColorDialogOpen = false;
        bool _isMouseOnCell = false;
        const int splitContainerPanel1MinSize = 300;
        const int _typeIndex = 0;
        const int _visibleIndex = 1;
        const int _nameIndex = 2;
        const int _idIndex = 3;
        const int _colorIndex = 4;
        const int _sizeIndex = 5;
        FormAddSelectPath formAdd;
        List<int> selectSegmentIndex = new List<int>();
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
            splitContainer1.Panel2.Controls.Add(_glControl);

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

            Log.Start();

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
            _translation = new Vector3();
            _rotation = new Vector3();
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
                    if (_selectIndex != id) _haveClosestPoint = false;
                    _selectIndex = id;
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
        }
        void clearDataGridSelection()
        {
            _selectIndex = 0;
            _haveClosestPoint = false;
            dataGridView1.ClearSelection();
        }
        private void DataGridView1_MouseClick(object sender, MouseEventArgs e)
        {
            if (_isMouseOnCell == false)
            {
                clearDataGridSelection();
                lbl_Selectable.Visible = false;

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
            if (_pickMode == PointPickMode.Single)
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
            if (_pickMode == PointPickMode.Multiple)
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
            lbl_PickPointMode.Image = Resources.shutdown_30px;
            treeView1.Nodes.Clear();
            splitContainer2.Panel2Collapsed = true;

        }
        void pickMode_Single()
        {
            _pickMode = PointPickMode.Single;
            lbl_PickPointMode.Text = "Pick Point - Middle Click";
            lbl_PickPointMode.Image = Resources.place_marker_30px;
            treeView1.Nodes.Clear();
            splitContainer2.Panel2Collapsed = false;

        }
        void pickMode_Measure()
        {
            _pickMode = PointPickMode.Multiple;
            lbl_PickPointMode.Text = "Pick 1st Point - Middle Click";
            lbl_PickPointMode.Image = Resources.width_30px;
            treeView1.Nodes.Clear();
            splitContainer2.Panel2Collapsed = false;

        }

        private void btn_SaveAs_Click(object sender, EventArgs e)
        {
            if (_selectIndex <= 1)
            {
                Log.Add("Select index = -1. Didn't select object.", MsgLevel.Warn);
                MessageBox.Show("Please select object first.", "Save file as xyz fail.", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (_displayObject.ContainsKey(_selectIndex) == false)
            {
                Log.Add($"Select index = {_selectIndex}. Display objects didn't contain {_selectIndex}.", MsgLevel.Warn);
                MessageBox.Show("No data to be saved.", "Save file as xyz fail.", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            try
            {
                Type objectType = _displayObject[_selectIndex].GetType();

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
                                var obj = _displayObject[_selectIndex] as Point3D;
                                obj.Save(filePath);
                            }
                            else if (objectType == typeof(ObjectGroup))
                            {
                                var obj = _displayObject[_selectIndex] as ObjectGroup;
                                obj.Save(filePath);
                            }
                            else
                            {
                                var obj = _displayObject[_selectIndex] as PointCloud;
                                obj.Save(filePath);
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
            GC.Collect();
        }

        private void saveABBModFileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            saveABBMod(false);
        }

        private void saveABBMod(bool isSaveRobTarget)
        {
            if (_selectIndex == -1)
            {
                Log.Add("Select index = -1. Didn't select path.", MsgLevel.Warn);
                MessageBox.Show($"Didn't select any path yet.", "MOD file save fail", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                return;
            }
            if (_displayObject.ContainsKey(_selectIndex) == false)
            {
                Log.Add($"Select index = {_selectIndex}. Display objects didn't contain {_selectIndex}.", MsgLevel.Warn);
                MessageBox.Show($"Display object didn't conatin {_selectIndex} object", "MOD file save fail", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            try
            {
                var obj = _displayObject[_selectIndex] as ObjectGroup;

                if (obj == null)
                {
                    Log.Add($"Select index = {_selectIndex}. Selected object  type is not ObjectGroup.", MsgLevel.Warn);
                    MessageBox.Show($"Selected objec cannot be saved as MOD", "MOD file save fail", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
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

        }

        private void addToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if(formAdd == null)
            {
                formAdd = new FormAddSelectPath();
                formAdd.UpdateSelectPath(selectSegmentIndex);
                formAdd.SaveSelectPath += FormAdd_SaveSelectPath;
                formAdd.ClearSelectPath += FormAdd_ClearSelectPath;
                formAdd.Show();
            }
            else
            {
                formAdd.Show();
            }

        }

        private void FormAdd_ClearSelectPath()
        {
            clearSelectPathIndex();
        }

        private void FormAdd_SaveSelectPath()
        {
            if (_selectIndex == -1)
            {
                Log.Add("Select index = -1. Didn't select path.", MsgLevel.Warn);
                MessageBox.Show($"Didn't select any path yet.", "MOD file save fail", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (_displayObject.ContainsKey(_selectIndex) == false)
            {
                Log.Add($"Select index = {_selectIndex}. Display objects didn't contain {_selectIndex}.", MsgLevel.Warn);
                MessageBox.Show($"Display object didn't conatin {_selectIndex} object", "MOD file save fail", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            try
            {
                var obj = _displayObject[_selectIndex] as ObjectGroup;

                if (obj == null)
                {
                    Log.Add($"Select index = {_selectIndex}. Selected object  type is not ObjectGroup.", MsgLevel.Warn);
                    MessageBox.Show($"Selected object cannot be saved as MOD", "MOD file save fail", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                ObjectGroup output = new ObjectGroup("SelectPath");
                int outputLineIndex = 0;
                foreach (var item in obj.Objects)
                {
                    string name = item.Key;
                    Polyline p = item.Value as Polyline; 
                    if(p != null)
                    {
                        LineOption lineOption = p.GetOption(typeof(LineOption)) as LineOption;
                        if(lineOption != null)
                        {
                            if (selectSegmentIndex.Contains(lineOption.LineIndex))
                            {
                                output.Add($"SelectPath{outputLineIndex}", p);
                                outputLineIndex++;
                            }
                        }
                    }
                }

                if (output.DataCount <= 0)
                {
                    MessageBox.Show($"No selected path cannot be saved as MOD", "MOD file save fail", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                using (SaveFileDialog sf = new SaveFileDialog())
                {
                    sf.Filter = "ABB mod File|*.mod";
                    if (sf.ShowDialog() == DialogResult.OK)
                    {
                        string outputFilePath = sf.FileName;
                        output.SaveABBModPath(outputFilePath,false);
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

        private void clearSelectPathIndex()
        {
            selectSegmentIndex.Clear();
        }
        private void saveABBModFileWithRobTargetToolStripMenuItem_Click(object sender, EventArgs e)
        {
            saveABBMod(true);
        }

        private void clearCollectionToolStripMenuItem_Click(object sender, EventArgs e)
        {
            clearSelectPathIndex();
        }
    }
}

﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Windows.Forms;
using OpenTK;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL;

using RsLib.PointCloud;
using RsLib.Display3D.Properties;
namespace RsLib.Display3D
{
    using RPointCloud = RsLib.PointCloud.PointCloud;

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

        public Display3DControl(int listNum = 1)
        {
            InitializeComponent();
            _maxDisplayList = listNum +1;
            
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
        public void Clear(bool clearOptions = true)
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
                    switch (option.DisplayType)
                    {
                        case DisplayObjectType.PointCloud:
                            BuildPointCloud((RPointCloud)_displayObject[id], id, false, false);
                            break;
                        case DisplayObjectType.Vector_z:
                            BuildVector((Polyline)_displayObject[id], id, false, false);
                            break;
                        case DisplayObjectType.Path:
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
                if(_displayOption.ContainsKey(id))
                {
                    bool oldCheck = _displayOption[id].IsDisplay;
                    if(oldCheck != isCheck)
                    {
                        _displayOption[id].IsDisplay = isCheck;
                    }
                }

            }
            else if(c == _sizeIndex)
            {
                if (_displayOption.ContainsKey(id))
                {
                    float newValue = float.Parse(dataGridView1.Rows[r].Cells[_sizeIndex].Value.ToString());
                    float oldValue = _displayOption[id].DrawSize;
                    if (oldValue != newValue)
                    {
                        _displayOption[id].DrawSize = newValue;
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

                        if(selectColor != oldColor)
                        {
                            _displayOption[id].DrawColor = selectColor;
                            ReBuild_ChangeColorSize(id);
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

                            if(option.IsSelectable == false)
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
            string ss = "";
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
            splitContainer1.SplitterDistance = splitContainerPanel1MinSize;
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
            lbl_PickPointMode.Text = "Pick Point";
            lbl_PickPointMode.Image = Resources.place_marker_30px;
            treeView1.Nodes.Clear();
            splitContainer2.Panel2Collapsed = false;

        }
        void pickMode_Measure()
        {
            _pickMode = PointPickMode.Multiple;
            lbl_PickPointMode.Text = "Pick 1st Point";
            lbl_PickPointMode.Image = Resources.width_30px;
            treeView1.Nodes.Clear();
            splitContainer2.Panel2Collapsed = false;

        }

        private void btn_SaveAs_Click(object sender, EventArgs e)
        {
            if (_selectIndex <= 1)
            {
                MessageBox.Show("Please select object first.", "Save file as xyz fail.", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (_displayObject.ContainsKey(_selectIndex) == false)
            {
                MessageBox.Show("No data to be saved.", "Save file as xyz fail.", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            Type objectType = _displayObject[_selectIndex].GetType();

            using (SaveFileDialog sf = new SaveFileDialog())
            {
                sf.Filter = "XYZ point cloud|*.xyz";
                if(sf.ShowDialog() == DialogResult.OK)
                {
                    string filePath = sf.FileName;
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
                        var obj = _displayObject[_selectIndex] as RPointCloud;
                        obj.Save(filePath);
                    }

                }
            }

            

        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if(_GLUpdateDone)
                _glControl.Invalidate();
            GC.Collect();
        }
    }
}
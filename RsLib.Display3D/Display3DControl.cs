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
        public event Action<bool,int,Point3D> AfterPointSelected;
        public event Action AfterCleared;
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
                //updateDataGridView();
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
            //updateDataGridView();
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
            updateDataGridView();
            AfterCleared?.Invoke();
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
                        case DisplayObjectType.Vector:
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
                if (_selectIndex != id) _haveClosestPoint = false;
                _selectIndex = id;
            }
        }

        private void DataGridView1_MouseClick(object sender, MouseEventArgs e)
        {
            if (_isMouseOnCell == false)
            {
                _selectIndex = 0;
                _haveClosestPoint = false;
                dataGridView1.ClearSelection();
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
                    ThreadPool.QueueUserWorkItem(new WaitCallback(showColorDialogTd), id);
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

        void updateDataGridView()
        {
            if (this.InvokeRequired)
            {
                Action action = new Action(updateDataGridView);
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
            updateDataGridView();
        }

        private void measureDistanceToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (_pickMode == PointPickMode.Measure)
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
            lbl_SelectPoint.Text = "--";
        }
        void pickMode_Single()
        {
            _pickMode = PointPickMode.Single;
            lbl_PickPointMode.Text = "Pick Point";
            lbl_PickPointMode.Image = Resources.place_marker_30px;
            lbl_SelectPoint.Text = "--";
        }
        void pickMode_Measure()
        {
            _pickMode = PointPickMode.Measure;
            lbl_PickPointMode.Text = "Measure";
            lbl_PickPointMode.Image = Resources.width_30px;
            lbl_SelectPoint.Text = "--";
        }
    }
}

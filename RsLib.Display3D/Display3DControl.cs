using System;
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
    public partial class Display3DControl : UserControl
    {
        public event Action<bool,int,Point3D> AfterPointSelected;
        bool _isColorDialogOpen = false;
        bool _isMouseOnCell = false;
        const int splitContainerPanel1MinSize = 300;
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

        private void DataGridView1_CellValidating(object sender, DataGridViewCellValidatingEventArgs e)
        {
            int columnIndex = e.ColumnIndex;
            int rowIndex = e.RowIndex;
            int id = (int)dataGridView1.Rows[rowIndex].Cells[2].Value;
            if (columnIndex == 4)
            {
                float newValue;
                if (float.TryParse(dataGridView1.Rows[rowIndex].Cells[4].Value.ToString(), out newValue) == false)
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
        }


        private void DataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int columnIndex = e.ColumnIndex;
            int rowIndex = e.RowIndex;
            int id = (int)dataGridView1.Rows[rowIndex].Cells[2].Value;

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
            if (r == -1) return;
            int id = (int)dataGridView1.Rows[r].Cells[2].Value;
            
            if (c == 0)
                dataGridView1.CommitEdit(DataGridViewDataErrorContexts.Commit);

            else if (c == 3)
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
            int columnIndex = e.ColumnIndex;
            int rowIndex = e.RowIndex;
            int id = (int)dataGridView1.Rows[rowIndex].Cells[2].Value;

            if (columnIndex == 0)
            {
                bool isCheck = (bool)dataGridView1.Rows[rowIndex].Cells[0].Value;
                if(_displayOption.ContainsKey(id))
                {
                    bool oldCheck = _displayOption[id].IsDisplay;
                    if(oldCheck != isCheck)
                    {
                        _displayOption[id].IsDisplay = isCheck;
                    }
                }

            }
            else if(columnIndex == 4)
            {
                if (_displayOption.ContainsKey(id))
                {
                    float newValue = float.Parse(dataGridView1.Rows[rowIndex].Cells[4].Value.ToString());
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
                    dataGridView1.Rows.Add(option.ToDataGridRowObject());
                    int currentIndex = dataGridView1.Rows.Count - 1;
                    DataGridViewButtonCell btn = dataGridView1.Rows[currentIndex].Cells[3] as DataGridViewButtonCell;
                    btn.FlatStyle = FlatStyle.Flat;
                    btn.Style.BackColor = option.DrawColor;
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

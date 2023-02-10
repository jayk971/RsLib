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
        public event Action<bool,Point3D> AfterPointSelected;
        bool _isColorDialogOpen = false;
        bool _isMouseOnCell = false;
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

            if (_displayObject.ContainsKey(id))
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
            int columnIndex = e.ColumnIndex;
            int rowIndex = e.RowIndex;
            int id = (int)dataGridView1.Rows[rowIndex].Cells[2].Value;
            
            if (columnIndex == 0)
                dataGridView1.CommitEdit(DataGridViewDataErrorContexts.Commit);

            else if (columnIndex == 3)
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
                if(_displayObject.ContainsKey(id))
                {
                    bool oldCheck = _displayObject[id].IsDisplay;
                    if(oldCheck != isCheck)
                    {
                        _displayObject[id].IsDisplay = isCheck;
                    }
                }

            }
            else if(columnIndex == 4)
            {
                if (_displayObject.ContainsKey(id))
                {
                    float newValue = float.Parse(dataGridView1.Rows[rowIndex].Cells[4].Value.ToString());
                    float oldValue = _displayObject[id].DrawSize;
                    if (oldValue != newValue)
                    {
                        _displayObject[id].DrawSize = newValue;
                        ReBuild(id);
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
                    if (_displayObject.ContainsKey(id))
                    {
                        Color selectColor = cd.Color;
                        Color oldColor = _displayObject[id].DisplayColor;

                        if(selectColor != oldColor)
                        {
                            _displayObject[id].DisplayColor = selectColor;
                            ReBuild(id);
                        }

                    }
                }
            }
            _isColorDialogOpen = false;
        }

        void updateUI()
        {
            if (this.InvokeRequired)
            {
                Action action = new Action(updateUI);
                this.Invoke(action);
            }
            else
            {
                dataGridView1.Rows.Clear();
                foreach (var item in _displayObject)
                {
                    DisplayObjectOption option = item.Value;
                    dataGridView1.Rows.Add(option.ToDataGridRowObject());
                    int currentIndex = dataGridView1.Rows.Count - 1;
                    DataGridViewButtonCell btn = dataGridView1.Rows[currentIndex].Cells[3] as DataGridViewButtonCell;
                    btn.FlatStyle = FlatStyle.Popup;
                    btn.Style.BackColor = option.DisplayColor;
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
            splitContainer1.SplitterDistance = splitContainer1.Panel1MinSize;
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
            _enableSelectPoint = !_enableSelectPoint;
            btn_PickPoint.BackColor = _enableSelectPoint ? Color.Red : Color.FromKnownColor(KnownColor.Control);
        }

        private void Display3DControl_SizeChanged(object sender, EventArgs e)
        {

            splitContainer1.SplitterDistance = splitContainer1.Panel1MinSize;
        }

        private void btn_Update_Click(object sender, EventArgs e)
        {
            ReBuildAll();
            updateUI();
        }
    }
}

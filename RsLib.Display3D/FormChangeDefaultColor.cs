using RsLib.Display3D.Properties;
using System;
using System.Drawing;
using System.Threading;
using System.Windows.Forms;
namespace RsLib.Display3D
{
    public partial class FormChangeDefaultColor : Form
    {
        bool _isColorDialogOpen = false;
        public FormChangeDefaultColor()
        {
            InitializeComponent();
            dataGridView1.CellValidating += DataGridView1_CellValidating;
            dataGridView1.CellValueChanged += DataGridView1_CellValueChanged;
            dataGridView1.CellContentClick += DataGridView1_CellContentClick;
            dataGridView1.Rows.Clear();
            dataGridView1.Rows.Add((int)ColorItem.SelectPoint, "Select Point", "", getSettingSize(ColorItem.SelectPoint));
            dataGridView1.Rows.Add((int)ColorItem.SelectFrame, "Select Range", "", getSettingSize(ColorItem.SelectFrame));
            dataGridView1.Rows.Add((int)ColorItem.MeasureStartP, "Measure Start Point", "", getSettingSize(ColorItem.MeasureStartP));
            dataGridView1.Rows.Add((int)ColorItem.MeasureEndP, "Measure End Point", "", getSettingSize(ColorItem.MeasureEndP));
            dataGridView1.Rows.Add((int)ColorItem.MeasureLine, "Measure Line", "", getSettingSize(ColorItem.MeasureLine));

            string[] names = Enum.GetNames(typeof(ColorItem));
            for (int i = 0; i < names.Length; i++)
            {
                if (names[i] == "None") continue;
                ColorItem item = (ColorItem)Enum.Parse(typeof(ColorItem), names[i]);
                int id = (int)item;
                int rowIndex = id - 1;
                DataGridViewButtonCell btn = dataGridView1.Rows[rowIndex].Cells[2] as DataGridViewButtonCell;
                btn.FlatStyle = FlatStyle.Popup;
                btn.Style.BackColor = getSettingColor(item);
            }
        }

        private void DataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            int r = e.RowIndex;
            int c = e.ColumnIndex;
            if (r == -1) return;
            int id = (int)dataGridView1.Rows[r].Cells[0].Value;

            if (c == 3)
                dataGridView1.CommitEdit(DataGridViewDataErrorContexts.Commit);
            else if (c == 2)
            {
                openColorDialog((ColorItem)id);
            }
        }

        private void DataGridView1_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            int r = e.RowIndex;
            int c = e.ColumnIndex;
            int id = (int)dataGridView1.Rows[r].Cells[0].Value;

            if (c == 3)
            {
                float newValue;
                if (float.TryParse(dataGridView1.Rows[r].Cells[c].Value.ToString(), out newValue))
                {
                    float oldValue = getSettingSize((ColorItem)id);
                    if (newValue < 1)
                    {
                        dataGridView1.Rows[r].Cells[c].Value = 1;
                        return;
                    }
                    if (oldValue != newValue)
                        setSettingSize((ColorItem)id, newValue);
                }
            }
        }

        private void DataGridView1_CellValidating(object sender, DataGridViewCellValidatingEventArgs e)
        {
            int r = e.RowIndex;
            int c = e.ColumnIndex;

            if (c == 3)
            {
                float f;
                if (float.TryParse(dataGridView1.Rows[r].Cells[c].Value.ToString(), out f) == false)
                {
                    e.Cancel = true;
                }
            }
        }



        Color getSettingColor(ColorItem colorItem)
        {
            switch (colorItem)
            {
                case ColorItem.SelectPoint:
                    return Settings.Default.Color_SelectPoint;
                case ColorItem.SelectFrame:
                    return Settings.Default.Color_SelectRange;
                case ColorItem.MeasureStartP:
                    return Settings.Default.Color_StartPoint;
                case ColorItem.MeasureEndP:
                    return Settings.Default.Color_EndPoint;
                case ColorItem.MeasureLine:
                    return Settings.Default.Color_MeasureLine;
                default:
                    return Color.White;
            }
        }
        float getSettingSize(ColorItem colorItem)
        {
            switch (colorItem)
            {
                case ColorItem.SelectPoint:
                    return Settings.Default.Size_SelectPoint;
                case ColorItem.SelectFrame:
                    return Settings.Default.Size_SelectRange;
                case ColorItem.MeasureStartP:
                    return Settings.Default.Size_StartPoint;
                case ColorItem.MeasureEndP:
                    return Settings.Default.Size_EndPoint;
                case ColorItem.MeasureLine:
                    return Settings.Default.Size_MeasureLine;
                default:
                    return 5f;
            }
        }
        void setSettingColor(ColorItem colorItem, Color setColor)
        {
            switch (colorItem)
            {
                case ColorItem.SelectPoint:
                    Settings.Default.Color_SelectPoint = setColor;
                    break;
                case ColorItem.SelectFrame:
                    Settings.Default.Color_SelectRange = setColor;
                    break;
                case ColorItem.MeasureStartP:
                    Settings.Default.Color_StartPoint = setColor;
                    break;
                case ColorItem.MeasureEndP:
                    Settings.Default.Color_EndPoint = setColor;
                    break;
                case ColorItem.MeasureLine:
                    Settings.Default.Color_MeasureLine = setColor;
                    break;
                default:
                    break;
            }
            Settings.Default.Save();
        }
        void setSettingSize(ColorItem colorItem, float setSize)
        {
            switch (colorItem)
            {
                case ColorItem.SelectPoint:
                    Settings.Default.Size_SelectPoint = setSize;
                    break;
                case ColorItem.SelectFrame:
                    Settings.Default.Size_SelectRange = setSize;
                    break;
                case ColorItem.MeasureStartP:
                    Settings.Default.Size_StartPoint = setSize;
                    break;
                case ColorItem.MeasureEndP:
                    Settings.Default.Size_EndPoint = setSize;
                    break;
                case ColorItem.MeasureLine:
                    Settings.Default.Size_MeasureLine = setSize;
                    break;
                default:
                    break;
            }
            Settings.Default.Save();
        }
        void updateBtnColor(ColorItem id, Color drawColor)
        {
            if (this.InvokeRequired)
            {
                Action<ColorItem, Color> action = new Action<ColorItem, Color>(updateBtnColor);
                this.Invoke(action, id, drawColor);
            }
            else
            {
                int r = (int)id - 1;
                DataGridViewButtonCell btn = dataGridView1.Rows[r].Cells[2] as DataGridViewButtonCell;
                btn.FlatStyle = FlatStyle.Popup;
                btn.Style.BackColor = drawColor;
                dataGridView1.ClearSelection();
            }
        }

        void showColorDialogTd(object obj)
        {
            _isColorDialogOpen = true;
            ColorItem i = (ColorItem)obj
; using (ColorDialog cd = new ColorDialog())
            {
                if (cd.ShowDialog() == DialogResult.OK)
                {
                    setSettingColor(i, cd.Color);
                    updateBtnColor(i, cd.Color);
                }
            }
            _isColorDialogOpen = false;
        }
        private void FormChangeDefaultColor_FormClosing(object sender, FormClosingEventArgs e)
        {
        }

        void openColorDialog(ColorItem index)
        {
            if (_isColorDialogOpen == false)
            {
                ThreadPool.QueueUserWorkItem(new WaitCallback(showColorDialogTd), index);
            }
            else
            {
                MessageBox.Show("Color dialog has been opened. Please close the color dialog first.", "Color Dialog Opened!!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

    }

    public enum ColorItem : int
    {
        None = 0,
        SelectPoint,
        SelectFrame,
        MeasureStartP,
        MeasureEndP,
        MeasureLine,
    }

}

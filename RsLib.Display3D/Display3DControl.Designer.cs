
namespace RsLib.Display3D
{
    partial class Display3DControl
    {
        /// <summary> 
        /// 設計工具所需的變數。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// 清除任何使用中的資源。
        /// </summary>
        /// <param name="disposing">如果應該處置受控資源則為 true，否則為 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region 元件設計工具產生的程式碼

        /// <summary> 
        /// 此為設計工具支援所需的方法 - 請勿使用程式碼編輯器修改
        /// 這個方法的內容。
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle9 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle10 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle11 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle12 = new System.Windows.Forms.DataGridViewCellStyle();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.splitContainer2 = new System.Windows.Forms.SplitContainer();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.Column_Type = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column_Display = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.Column_Name = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column_Color = new System.Windows.Forms.DataGridViewButtonColumn();
            this.Column2_Size = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.treeView1 = new System.Windows.Forms.TreeView();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.btn_Edit = new System.Windows.Forms.ToolStripButton();
            this.btn_ResizeView = new System.Windows.Forms.ToolStripButton();
            this.btn_ClearObject = new System.Windows.Forms.ToolStripButton();
            this.btn_Update = new System.Windows.Forms.ToolStripButton();
            this.btn_Color = new System.Windows.Forms.ToolStripButton();
            this.btn_PickPoint = new System.Windows.Forms.ToolStripSplitButton();
            this.measureDistanceToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.lbl_Selectable = new System.Windows.Forms.ToolStripLabel();
            this.lbl_PickPointMode = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStrip1.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).BeginInit();
            this.splitContainer2.Panel1.SuspendLayout();
            this.splitContainer2.Panel2.SuspendLayout();
            this.splitContainer2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // toolStrip1
            // 
            this.toolStrip1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.toolStrip1.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btn_Edit,
            this.btn_ResizeView,
            this.btn_ClearObject,
            this.btn_Update,
            this.btn_Color,
            this.btn_PickPoint,
            this.toolStripSeparator1,
            this.lbl_Selectable});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(518, 35);
            this.toolStrip1.TabIndex = 0;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // statusStrip1
            // 
            this.statusStrip1.BackColor = System.Drawing.Color.Gainsboro;
            this.statusStrip1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.lbl_PickPointMode});
            this.statusStrip1.Location = new System.Drawing.Point(0, 410);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(518, 30);
            this.statusStrip1.TabIndex = 1;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.toolStrip1, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.statusStrip1, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.splitContainer1, 0, 1);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 3;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 35F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(518, 440);
            this.tableLayoutPanel1.TabIndex = 2;
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(3, 38);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.splitContainer2);
            this.splitContainer1.Panel1MinSize = 250;
            this.splitContainer1.Size = new System.Drawing.Size(512, 369);
            this.splitContainer1.SplitterDistance = 250;
            this.splitContainer1.TabIndex = 2;
            // 
            // splitContainer2
            // 
            this.splitContainer2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer2.Location = new System.Drawing.Point(0, 0);
            this.splitContainer2.Name = "splitContainer2";
            this.splitContainer2.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer2.Panel1
            // 
            this.splitContainer2.Panel1.Controls.Add(this.dataGridView1);
            // 
            // splitContainer2.Panel2
            // 
            this.splitContainer2.Panel2.Controls.Add(this.treeView1);
            this.splitContainer2.Size = new System.Drawing.Size(250, 369);
            this.splitContainer2.SplitterDistance = 236;
            this.splitContainer2.TabIndex = 0;
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToDeleteRows = false;
            dataGridViewCellStyle9.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle9.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle9.Font = new System.Drawing.Font("新細明體", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            dataGridViewCellStyle9.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle9.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle9.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle9.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridView1.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle9;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Column_Type,
            this.Column_Display,
            this.Column_Name,
            this.Column1,
            this.Column_Color,
            this.Column2_Size});
            this.dataGridView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridView1.Location = new System.Drawing.Point(0, 0);
            this.dataGridView1.MultiSelect = false;
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowHeadersVisible = false;
            this.dataGridView1.RowTemplate.Height = 24;
            this.dataGridView1.ShowEditingIcon = false;
            this.dataGridView1.Size = new System.Drawing.Size(250, 236);
            this.dataGridView1.TabIndex = 0;
            // 
            // Column_Type
            // 
            dataGridViewCellStyle10.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.Column_Type.DefaultCellStyle = dataGridViewCellStyle10;
            this.Column_Type.HeaderText = "Type";
            this.Column_Type.Name = "Column_Type";
            this.Column_Type.ReadOnly = true;
            this.Column_Type.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.Column_Type.Width = 70;
            // 
            // Column_Display
            // 
            this.Column_Display.HeaderText = "Display";
            this.Column_Display.Name = "Column_Display";
            this.Column_Display.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.Column_Display.Width = 50;
            // 
            // Column_Name
            // 
            this.Column_Name.HeaderText = "Name";
            this.Column_Name.Name = "Column_Name";
            this.Column_Name.ReadOnly = true;
            this.Column_Name.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // Column1
            // 
            dataGridViewCellStyle11.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.Column1.DefaultCellStyle = dataGridViewCellStyle11;
            this.Column1.HeaderText = "ID";
            this.Column1.Name = "Column1";
            this.Column1.ReadOnly = true;
            this.Column1.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.Column1.Width = 30;
            // 
            // Column_Color
            // 
            this.Column_Color.HeaderText = "Color";
            this.Column_Color.Name = "Column_Color";
            this.Column_Color.ReadOnly = true;
            this.Column_Color.Width = 50;
            // 
            // Column2_Size
            // 
            dataGridViewCellStyle12.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.Column2_Size.DefaultCellStyle = dataGridViewCellStyle12;
            this.Column2_Size.HeaderText = "Size";
            this.Column2_Size.Name = "Column2_Size";
            this.Column2_Size.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.Column2_Size.Width = 60;
            // 
            // treeView1
            // 
            this.treeView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.treeView1.Location = new System.Drawing.Point(0, 0);
            this.treeView1.Name = "treeView1";
            this.treeView1.Size = new System.Drawing.Size(250, 129);
            this.treeView1.TabIndex = 0;
            // 
            // btn_Edit
            // 
            this.btn_Edit.AutoSize = false;
            this.btn_Edit.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btn_Edit.Image = global::RsLib.Display3D.Properties.Resources.edit_30px;
            this.btn_Edit.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btn_Edit.Name = "btn_Edit";
            this.btn_Edit.Size = new System.Drawing.Size(32, 32);
            this.btn_Edit.Text = "Edit";
            this.btn_Edit.Click += new System.EventHandler(this.btn_Edit_Click);
            // 
            // btn_ResizeView
            // 
            this.btn_ResizeView.AutoSize = false;
            this.btn_ResizeView.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btn_ResizeView.Image = global::RsLib.Display3D.Properties.Resources.resize_30px;
            this.btn_ResizeView.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btn_ResizeView.Name = "btn_ResizeView";
            this.btn_ResizeView.Size = new System.Drawing.Size(32, 32);
            this.btn_ResizeView.Text = "Reset View";
            this.btn_ResizeView.Click += new System.EventHandler(this.btn_ResizeView_Click);
            // 
            // btn_ClearObject
            // 
            this.btn_ClearObject.AutoSize = false;
            this.btn_ClearObject.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btn_ClearObject.Image = global::RsLib.Display3D.Properties.Resources.broom_30px;
            this.btn_ClearObject.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btn_ClearObject.Name = "btn_ClearObject";
            this.btn_ClearObject.Size = new System.Drawing.Size(32, 32);
            this.btn_ClearObject.Text = "Clear Objecs";
            this.btn_ClearObject.Click += new System.EventHandler(this.btn_ClearObject_Click);
            // 
            // btn_Update
            // 
            this.btn_Update.AutoSize = false;
            this.btn_Update.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btn_Update.Image = global::RsLib.Display3D.Properties.Resources.available_updates_30px;
            this.btn_Update.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btn_Update.Name = "btn_Update";
            this.btn_Update.Size = new System.Drawing.Size(32, 32);
            this.btn_Update.Text = "Update";
            this.btn_Update.Click += new System.EventHandler(this.btn_Update_Click);
            // 
            // btn_Color
            // 
            this.btn_Color.AutoSize = false;
            this.btn_Color.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btn_Color.Image = global::RsLib.Display3D.Properties.Resources.paint_palette_30px;
            this.btn_Color.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btn_Color.Name = "btn_Color";
            this.btn_Color.Size = new System.Drawing.Size(32, 32);
            this.btn_Color.Text = "Change Color";
            this.btn_Color.Click += new System.EventHandler(this.btn_Color_Click);
            // 
            // btn_PickPoint
            // 
            this.btn_PickPoint.AutoSize = false;
            this.btn_PickPoint.BackColor = System.Drawing.SystemColors.Control;
            this.btn_PickPoint.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btn_PickPoint.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.measureDistanceToolStripMenuItem,
            this.exitToolStripMenuItem});
            this.btn_PickPoint.Image = global::RsLib.Display3D.Properties.Resources.place_marker_30px;
            this.btn_PickPoint.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btn_PickPoint.Name = "btn_PickPoint";
            this.btn_PickPoint.Size = new System.Drawing.Size(32, 32);
            this.btn_PickPoint.Text = "Pick Point";
            this.btn_PickPoint.ButtonClick += new System.EventHandler(this.btn_PickPoint_Click);
            // 
            // measureDistanceToolStripMenuItem
            // 
            this.measureDistanceToolStripMenuItem.Image = global::RsLib.Display3D.Properties.Resources.width_30px;
            this.measureDistanceToolStripMenuItem.Name = "measureDistanceToolStripMenuItem";
            this.measureDistanceToolStripMenuItem.Size = new System.Drawing.Size(169, 22);
            this.measureDistanceToolStripMenuItem.Text = "Multiple Select";
            this.measureDistanceToolStripMenuItem.Click += new System.EventHandler(this.measureDistanceToolStripMenuItem_Click);
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Image = global::RsLib.Display3D.Properties.Resources.shutdown_30px;
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(169, 22);
            this.exitToolStripMenuItem.Text = "Exit Select Mode";
            this.exitToolStripMenuItem.Click += new System.EventHandler(this.exitToolStripMenuItem_Click);
            // 
            // lbl_Selectable
            // 
            this.lbl_Selectable.Image = global::RsLib.Display3D.Properties.Resources.disclaimer_30px;
            this.lbl_Selectable.Name = "lbl_Selectable";
            this.lbl_Selectable.Size = new System.Drawing.Size(107, 32);
            this.lbl_Selectable.Text = "Not Selectable";
            this.lbl_Selectable.Visible = false;
            // 
            // lbl_PickPointMode
            // 
            this.lbl_PickPointMode.BackColor = System.Drawing.Color.Gainsboro;
            this.lbl_PickPointMode.Image = global::RsLib.Display3D.Properties.Resources.shutdown_30px;
            this.lbl_PickPointMode.Name = "lbl_PickPointMode";
            this.lbl_PickPointMode.Size = new System.Drawing.Size(55, 16);
            this.lbl_PickPointMode.Text = "None";
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 35);
            // 
            // Display3DControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tableLayoutPanel1);
            this.Name = "Display3DControl";
            this.Size = new System.Drawing.Size(518, 440);
            this.SizeChanged += new System.EventHandler(this.Display3DControl_SizeChanged);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.splitContainer1.Panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.splitContainer2.Panel1.ResumeLayout(false);
            this.splitContainer2.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).EndInit();
            this.splitContainer2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.ToolStripButton btn_ResizeView;
        private System.Windows.Forms.ToolStripButton btn_Edit;
        private System.Windows.Forms.ToolStripButton btn_ClearObject;
        private System.Windows.Forms.ToolStripButton btn_Update;
        private System.Windows.Forms.ToolStripButton btn_Color;
        private System.Windows.Forms.ToolStripSplitButton btn_PickPoint;
        private System.Windows.Forms.ToolStripStatusLabel lbl_PickPointMode;
        private System.Windows.Forms.ToolStripMenuItem measureDistanceToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column_Type;
        private System.Windows.Forms.DataGridViewCheckBoxColumn Column_Display;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column_Name;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column1;
        private System.Windows.Forms.DataGridViewButtonColumn Column_Color;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column2_Size;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.SplitContainer splitContainer2;
        private System.Windows.Forms.TreeView treeView1;
        private System.Windows.Forms.ToolStripLabel lbl_Selectable;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
    }
}

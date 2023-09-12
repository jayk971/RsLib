
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.btn_Edit = new System.Windows.Forms.ToolStripButton();
            this.btn_ResizeView = new System.Windows.Forms.ToolStripButton();
            this.btn_ClearObject = new System.Windows.Forms.ToolStripButton();
            this.btn_Update = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.toolBtn_ShowAddPathForm = new System.Windows.Forms.ToolStripButton();
            this.toolBtn_Setting = new System.Windows.Forms.ToolStripDropDownButton();
            this.changeColorSizeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.changeRotateSensitivityToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.manualShiftRotateToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripDropDownButton1 = new System.Windows.Forms.ToolStripDropDownButton();
            this.saveXYZPointCloudToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveSelectedXYZPointCloudToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.saveABBModFileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveABBModFileWithRobTargetToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.btn_PickPoint = new System.Windows.Forms.ToolStripSplitButton();
            this.measureDistanceToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.lbl_Selectable = new System.Windows.Forms.ToolStripLabel();
            this.toolStripDropDownButton2 = new System.Windows.Forms.ToolStripDropDownButton();
            this.upToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.bottomViewToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.rightViewToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.lefeViewToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.frontViewToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.backViewToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.lbl_PickPointMode = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStatusLbl_SelectObjectIndex = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabel2 = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStatusLbl_CurrentSelectLineIndex = new System.Windows.Forms.ToolStripStatusLabel();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.splitContainer2 = new System.Windows.Forms.SplitContainer();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.Column_Type = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column_Display = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.Column_Name = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column_Color = new System.Windows.Forms.DataGridViewButtonColumn();
            this.Column2_Size = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.toolStrip2 = new System.Windows.Forms.ToolStrip();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.addToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.addAllToCollectionToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.clearCollectionToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.reversePathToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripLabel1 = new System.Windows.Forms.ToolStripLabel();
            this.toolCmb_LineIndex = new System.Windows.Forms.ToolStripComboBox();
            this.treeView1 = new System.Windows.Forms.TreeView();
            this.tlp_LocalTransform = new System.Windows.Forms.TableLayoutPanel();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.nud_ShiftX = new System.Windows.Forms.NumericUpDown();
            this.nud_ShiftY = new System.Windows.Forms.NumericUpDown();
            this.nud_ShiftZ = new System.Windows.Forms.NumericUpDown();
            this.nud_RotateX = new System.Windows.Forms.NumericUpDown();
            this.nud_RotateY = new System.Windows.Forms.NumericUpDown();
            this.nud_RotateZ = new System.Windows.Forms.NumericUpDown();
            this.trackBar_RotateSensitivity = new System.Windows.Forms.TrackBar();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.toolStrip1.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).BeginInit();
            this.splitContainer2.Panel1.SuspendLayout();
            this.splitContainer2.Panel2.SuspendLayout();
            this.splitContainer2.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.toolStrip2.SuspendLayout();
            this.contextMenuStrip1.SuspendLayout();
            this.tlp_LocalTransform.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nud_ShiftX)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nud_ShiftY)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nud_ShiftZ)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nud_RotateX)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nud_RotateY)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nud_RotateZ)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar_RotateSensitivity)).BeginInit();
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
            this.toolStripSeparator1,
            this.toolBtn_ShowAddPathForm,
            this.toolBtn_Setting,
            this.toolStripSeparator4,
            this.toolStripDropDownButton1,
            this.btn_PickPoint,
            this.lbl_Selectable,
            this.toolStripDropDownButton2});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(748, 38);
            this.toolStrip1.TabIndex = 0;
            this.toolStrip1.Text = "toolStrip1";
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
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 38);
            // 
            // toolBtn_ShowAddPathForm
            // 
            this.toolBtn_ShowAddPathForm.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolBtn_ShowAddPathForm.Image = global::RsLib.Display3D.Properties.Resources.list_48px;
            this.toolBtn_ShowAddPathForm.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolBtn_ShowAddPathForm.Name = "toolBtn_ShowAddPathForm";
            this.toolBtn_ShowAddPathForm.Size = new System.Drawing.Size(23, 35);
            this.toolBtn_ShowAddPathForm.Text = "Show Add List";
            this.toolBtn_ShowAddPathForm.Click += new System.EventHandler(this.toolBtn_ShowAddPathForm_Click);
            // 
            // toolBtn_Setting
            // 
            this.toolBtn_Setting.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.toolBtn_Setting.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolBtn_Setting.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.changeColorSizeToolStripMenuItem,
            this.changeRotateSensitivityToolStripMenuItem,
            this.manualShiftRotateToolStripMenuItem});
            this.toolBtn_Setting.Image = global::RsLib.Display3D.Properties.Resources.settings_48px;
            this.toolBtn_Setting.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolBtn_Setting.Name = "toolBtn_Setting";
            this.toolBtn_Setting.Size = new System.Drawing.Size(29, 35);
            this.toolBtn_Setting.Text = "Setting";
            // 
            // changeColorSizeToolStripMenuItem
            // 
            this.changeColorSizeToolStripMenuItem.Image = global::RsLib.Display3D.Properties.Resources.paint_palette_30px;
            this.changeColorSizeToolStripMenuItem.Name = "changeColorSizeToolStripMenuItem";
            this.changeColorSizeToolStripMenuItem.Size = new System.Drawing.Size(211, 22);
            this.changeColorSizeToolStripMenuItem.Text = "Change color and size";
            this.changeColorSizeToolStripMenuItem.Click += new System.EventHandler(this.btn_Color_Click);
            // 
            // changeRotateSensitivityToolStripMenuItem
            // 
            this.changeRotateSensitivityToolStripMenuItem.Image = global::RsLib.Display3D.Properties.Resources.settings_48px;
            this.changeRotateSensitivityToolStripMenuItem.Name = "changeRotateSensitivityToolStripMenuItem";
            this.changeRotateSensitivityToolStripMenuItem.Size = new System.Drawing.Size(211, 22);
            this.changeRotateSensitivityToolStripMenuItem.Text = "Change rotate sensitivity";
            this.changeRotateSensitivityToolStripMenuItem.Click += new System.EventHandler(this.changeRotateSensitivityToolStripMenuItem_Click);
            // 
            // manualShiftRotateToolStripMenuItem
            // 
            this.manualShiftRotateToolStripMenuItem.Image = global::RsLib.Display3D.Properties.Resources.settings_48px;
            this.manualShiftRotateToolStripMenuItem.Name = "manualShiftRotateToolStripMenuItem";
            this.manualShiftRotateToolStripMenuItem.Size = new System.Drawing.Size(211, 22);
            this.manualShiftRotateToolStripMenuItem.Text = "Manual shift \\ rotate";
            this.manualShiftRotateToolStripMenuItem.Visible = false;
            this.manualShiftRotateToolStripMenuItem.Click += new System.EventHandler(this.manualShiftRotateToolStripMenuItem_Click);
            // 
            // toolStripSeparator4
            // 
            this.toolStripSeparator4.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.toolStripSeparator4.Name = "toolStripSeparator4";
            this.toolStripSeparator4.Size = new System.Drawing.Size(6, 38);
            // 
            // toolStripDropDownButton1
            // 
            this.toolStripDropDownButton1.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.toolStripDropDownButton1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripDropDownButton1.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.saveXYZPointCloudToolStripMenuItem,
            this.saveSelectedXYZPointCloudToolStripMenuItem,
            this.toolStripSeparator2,
            this.toolStripMenuItem1,
            this.toolStripSeparator3,
            this.saveABBModFileToolStripMenuItem,
            this.saveABBModFileWithRobTargetToolStripMenuItem});
            this.toolStripDropDownButton1.Image = global::RsLib.Display3D.Properties.Resources.save_30px;
            this.toolStripDropDownButton1.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripDropDownButton1.Name = "toolStripDropDownButton1";
            this.toolStripDropDownButton1.Size = new System.Drawing.Size(29, 35);
            this.toolStripDropDownButton1.Text = "toolStripDropDownButton1";
            // 
            // saveXYZPointCloudToolStripMenuItem
            // 
            this.saveXYZPointCloudToolStripMenuItem.Image = global::RsLib.Display3D.Properties.Resources.save_30px;
            this.saveXYZPointCloudToolStripMenuItem.Name = "saveXYZPointCloudToolStripMenuItem";
            this.saveXYZPointCloudToolStripMenuItem.Size = new System.Drawing.Size(272, 22);
            this.saveXYZPointCloudToolStripMenuItem.Text = "Save XYZ point cloud";
            this.saveXYZPointCloudToolStripMenuItem.Click += new System.EventHandler(this.btn_SaveAs_Click);
            // 
            // saveSelectedXYZPointCloudToolStripMenuItem
            // 
            this.saveSelectedXYZPointCloudToolStripMenuItem.Image = global::RsLib.Display3D.Properties.Resources.save_30px;
            this.saveSelectedXYZPointCloudToolStripMenuItem.Name = "saveSelectedXYZPointCloudToolStripMenuItem";
            this.saveSelectedXYZPointCloudToolStripMenuItem.Size = new System.Drawing.Size(272, 22);
            this.saveSelectedXYZPointCloudToolStripMenuItem.Text = "Save selected XYZ point cloud";
            this.saveSelectedXYZPointCloudToolStripMenuItem.Click += new System.EventHandler(this.saveSelectedXYZPointCloudToolStripMenuItem_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(269, 6);
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Image = global::RsLib.Display3D.Properties.Resources.save_30px;
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(272, 22);
            this.toolStripMenuItem1.Text = "Save OPT path file";
            this.toolStripMenuItem1.Click += new System.EventHandler(this.saveOPTFileToolStripMenuItem_Click);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(269, 6);
            // 
            // saveABBModFileToolStripMenuItem
            // 
            this.saveABBModFileToolStripMenuItem.Image = global::RsLib.Display3D.Properties.Resources.save_30px;
            this.saveABBModFileToolStripMenuItem.Name = "saveABBModFileToolStripMenuItem";
            this.saveABBModFileToolStripMenuItem.Size = new System.Drawing.Size(272, 22);
            this.saveABBModFileToolStripMenuItem.Text = "Save ABB Mod File";
            this.saveABBModFileToolStripMenuItem.ToolTipText = "Save ABB Mod File";
            this.saveABBModFileToolStripMenuItem.Click += new System.EventHandler(this.saveABBModFileToolStripMenuItem_Click);
            // 
            // saveABBModFileWithRobTargetToolStripMenuItem
            // 
            this.saveABBModFileWithRobTargetToolStripMenuItem.Image = global::RsLib.Display3D.Properties.Resources.save_30px;
            this.saveABBModFileWithRobTargetToolStripMenuItem.Name = "saveABBModFileWithRobTargetToolStripMenuItem";
            this.saveABBModFileWithRobTargetToolStripMenuItem.Size = new System.Drawing.Size(272, 22);
            this.saveABBModFileWithRobTargetToolStripMenuItem.Text = "Save ABB Mod File With RobTarget";
            this.saveABBModFileWithRobTargetToolStripMenuItem.Click += new System.EventHandler(this.saveABBModFileWithRobTargetToolStripMenuItem_Click);
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
            this.lbl_Selectable.Image = global::RsLib.Display3D.Properties.Resources.unavailable_48px;
            this.lbl_Selectable.Name = "lbl_Selectable";
            this.lbl_Selectable.Size = new System.Drawing.Size(107, 35);
            this.lbl_Selectable.Text = "Not Selectable";
            this.lbl_Selectable.Visible = false;
            // 
            // toolStripDropDownButton2
            // 
            this.toolStripDropDownButton2.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.toolStripDropDownButton2.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripDropDownButton2.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.upToolStripMenuItem,
            this.bottomViewToolStripMenuItem,
            this.rightViewToolStripMenuItem,
            this.lefeViewToolStripMenuItem,
            this.frontViewToolStripMenuItem,
            this.backViewToolStripMenuItem});
            this.toolStripDropDownButton2.Image = global::RsLib.Display3D.Properties.Resources.orthogonal_view_48px;
            this.toolStripDropDownButton2.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripDropDownButton2.Name = "toolStripDropDownButton2";
            this.toolStripDropDownButton2.Size = new System.Drawing.Size(29, 35);
            this.toolStripDropDownButton2.Text = "toolStripDropDownButton2";
            // 
            // upToolStripMenuItem
            // 
            this.upToolStripMenuItem.Name = "upToolStripMenuItem";
            this.upToolStripMenuItem.Size = new System.Drawing.Size(146, 22);
            this.upToolStripMenuItem.Text = "Top View";
            this.upToolStripMenuItem.Click += new System.EventHandler(this.upToolStripMenuItem_Click);
            // 
            // bottomViewToolStripMenuItem
            // 
            this.bottomViewToolStripMenuItem.Name = "bottomViewToolStripMenuItem";
            this.bottomViewToolStripMenuItem.Size = new System.Drawing.Size(146, 22);
            this.bottomViewToolStripMenuItem.Text = "Bottom View";
            this.bottomViewToolStripMenuItem.Click += new System.EventHandler(this.bottomViewToolStripMenuItem_Click);
            // 
            // rightViewToolStripMenuItem
            // 
            this.rightViewToolStripMenuItem.Name = "rightViewToolStripMenuItem";
            this.rightViewToolStripMenuItem.Size = new System.Drawing.Size(146, 22);
            this.rightViewToolStripMenuItem.Text = "Right View";
            this.rightViewToolStripMenuItem.Click += new System.EventHandler(this.rightViewToolStripMenuItem_Click);
            // 
            // lefeViewToolStripMenuItem
            // 
            this.lefeViewToolStripMenuItem.Name = "lefeViewToolStripMenuItem";
            this.lefeViewToolStripMenuItem.Size = new System.Drawing.Size(146, 22);
            this.lefeViewToolStripMenuItem.Text = "Left View";
            this.lefeViewToolStripMenuItem.Click += new System.EventHandler(this.leftViewToolStripMenuItem_Click);
            // 
            // frontViewToolStripMenuItem
            // 
            this.frontViewToolStripMenuItem.Name = "frontViewToolStripMenuItem";
            this.frontViewToolStripMenuItem.Size = new System.Drawing.Size(146, 22);
            this.frontViewToolStripMenuItem.Text = "Front View";
            this.frontViewToolStripMenuItem.Click += new System.EventHandler(this.frontViewToolStripMenuItem_Click);
            // 
            // backViewToolStripMenuItem
            // 
            this.backViewToolStripMenuItem.Name = "backViewToolStripMenuItem";
            this.backViewToolStripMenuItem.Size = new System.Drawing.Size(146, 22);
            this.backViewToolStripMenuItem.Text = "Back View";
            this.backViewToolStripMenuItem.Click += new System.EventHandler(this.backViewToolStripMenuItem_Click);
            // 
            // statusStrip1
            // 
            this.statusStrip1.BackColor = System.Drawing.Color.Gainsboro;
            this.statusStrip1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.lbl_PickPointMode,
            this.toolStripStatusLabel1,
            this.toolStatusLbl_SelectObjectIndex,
            this.toolStripStatusLabel2,
            this.toolStatusLbl_CurrentSelectLineIndex});
            this.statusStrip1.Location = new System.Drawing.Point(0, 603);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(748, 30);
            this.statusStrip1.TabIndex = 1;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // lbl_PickPointMode
            // 
            this.lbl_PickPointMode.BackColor = System.Drawing.Color.Gainsboro;
            this.lbl_PickPointMode.Image = global::RsLib.Display3D.Properties.Resources.unavailable_48px;
            this.lbl_PickPointMode.Name = "lbl_PickPointMode";
            this.lbl_PickPointMode.Size = new System.Drawing.Size(55, 25);
            this.lbl_PickPointMode.Text = "None";
            // 
            // toolStripStatusLabel1
            // 
            this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            this.toolStripStatusLabel1.Size = new System.Drawing.Size(43, 25);
            this.toolStripStatusLabel1.Text = "Group";
            // 
            // toolStatusLbl_SelectObjectIndex
            // 
            this.toolStatusLbl_SelectObjectIndex.Name = "toolStatusLbl_SelectObjectIndex";
            this.toolStatusLbl_SelectObjectIndex.Size = new System.Drawing.Size(19, 25);
            this.toolStatusLbl_SelectObjectIndex.Text = "-1";
            // 
            // toolStripStatusLabel2
            // 
            this.toolStripStatusLabel2.Name = "toolStripStatusLabel2";
            this.toolStripStatusLabel2.Size = new System.Drawing.Size(30, 25);
            this.toolStripStatusLabel2.Text = "Line";
            // 
            // toolStatusLbl_CurrentSelectLineIndex
            // 
            this.toolStatusLbl_CurrentSelectLineIndex.Name = "toolStatusLbl_CurrentSelectLineIndex";
            this.toolStatusLbl_CurrentSelectLineIndex.Size = new System.Drawing.Size(22, 25);
            this.toolStatusLbl_CurrentSelectLineIndex.Text = " -1";
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
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 38F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(748, 633);
            this.tableLayoutPanel1.TabIndex = 2;
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(3, 41);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.splitContainer2);
            this.splitContainer1.Panel1MinSize = 250;
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.tlp_LocalTransform);
            this.splitContainer1.Panel2.Controls.Add(this.trackBar_RotateSensitivity);
            this.splitContainer1.Size = new System.Drawing.Size(742, 559);
            this.splitContainer1.SplitterDistance = 296;
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
            this.splitContainer2.Panel1.Controls.Add(this.tableLayoutPanel2);
            // 
            // splitContainer2.Panel2
            // 
            this.splitContainer2.Panel2.Controls.Add(this.treeView1);
            this.splitContainer2.Size = new System.Drawing.Size(296, 559);
            this.splitContainer2.SplitterDistance = 358;
            this.splitContainer2.TabIndex = 0;
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.ColumnCount = 1;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel2.Controls.Add(this.dataGridView1, 0, 0);
            this.tableLayoutPanel2.Controls.Add(this.toolStrip2, 0, 1);
            this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel2.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 2;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(296, 358);
            this.tableLayoutPanel2.TabIndex = 1;
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToDeleteRows = false;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("新細明體", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridView1.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Column_Type,
            this.Column_Display,
            this.Column_Name,
            this.Column1,
            this.Column_Color,
            this.Column2_Size});
            this.dataGridView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridView1.Location = new System.Drawing.Point(3, 3);
            this.dataGridView1.MultiSelect = false;
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowHeadersVisible = false;
            this.dataGridView1.RowTemplate.Height = 24;
            this.dataGridView1.ShowEditingIcon = false;
            this.dataGridView1.Size = new System.Drawing.Size(290, 322);
            this.dataGridView1.TabIndex = 0;
            // 
            // Column_Type
            // 
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.Column_Type.DefaultCellStyle = dataGridViewCellStyle2;
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
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.Column1.DefaultCellStyle = dataGridViewCellStyle3;
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
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.Column2_Size.DefaultCellStyle = dataGridViewCellStyle4;
            this.Column2_Size.HeaderText = "Size";
            this.Column2_Size.Name = "Column2_Size";
            this.Column2_Size.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.Column2_Size.Width = 60;
            // 
            // toolStrip2
            // 
            this.toolStrip2.ContextMenuStrip = this.contextMenuStrip1;
            this.toolStrip2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.toolStrip2.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.toolStrip2.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripLabel1,
            this.toolCmb_LineIndex});
            this.toolStrip2.Location = new System.Drawing.Point(0, 328);
            this.toolStrip2.Name = "toolStrip2";
            this.toolStrip2.Size = new System.Drawing.Size(296, 30);
            this.toolStrip2.TabIndex = 0;
            this.toolStrip2.Text = "toolStrip2";
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.addToolStripMenuItem,
            this.clearCollectionToolStripMenuItem,
            this.reversePathToolStripMenuItem});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(177, 70);
            // 
            // addToolStripMenuItem
            // 
            this.addToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.addAllToCollectionToolStripMenuItem});
            this.addToolStripMenuItem.Image = global::RsLib.Display3D.Properties.Resources.add_48px;
            this.addToolStripMenuItem.Name = "addToolStripMenuItem";
            this.addToolStripMenuItem.Size = new System.Drawing.Size(176, 22);
            this.addToolStripMenuItem.Text = "Add To Collection";
            this.addToolStripMenuItem.Click += new System.EventHandler(this.addToolStripMenuItem_Click);
            // 
            // addAllToCollectionToolStripMenuItem
            // 
            this.addAllToCollectionToolStripMenuItem.Image = global::RsLib.Display3D.Properties.Resources.add_48px;
            this.addAllToCollectionToolStripMenuItem.Name = "addAllToCollectionToolStripMenuItem";
            this.addAllToCollectionToolStripMenuItem.Size = new System.Drawing.Size(190, 22);
            this.addAllToCollectionToolStripMenuItem.Text = "Add All to Collection";
            this.addAllToCollectionToolStripMenuItem.Click += new System.EventHandler(this.addAllToCollectionToolStripMenuItem_Click);
            // 
            // clearCollectionToolStripMenuItem
            // 
            this.clearCollectionToolStripMenuItem.Image = global::RsLib.Display3D.Properties.Resources.broom_30px;
            this.clearCollectionToolStripMenuItem.Name = "clearCollectionToolStripMenuItem";
            this.clearCollectionToolStripMenuItem.Size = new System.Drawing.Size(176, 22);
            this.clearCollectionToolStripMenuItem.Text = "Clear Collection";
            this.clearCollectionToolStripMenuItem.Click += new System.EventHandler(this.clearCollectionToolStripMenuItem_Click);
            // 
            // reversePathToolStripMenuItem
            // 
            this.reversePathToolStripMenuItem.Image = global::RsLib.Display3D.Properties.Resources.reversed_numerical_sorting_48px;
            this.reversePathToolStripMenuItem.Name = "reversePathToolStripMenuItem";
            this.reversePathToolStripMenuItem.Size = new System.Drawing.Size(176, 22);
            this.reversePathToolStripMenuItem.Text = "Reverse Path";
            this.reversePathToolStripMenuItem.Click += new System.EventHandler(this.reversePathToolStripMenuItem_Click);
            // 
            // toolStripLabel1
            // 
            this.toolStripLabel1.Name = "toolStripLabel1";
            this.toolStripLabel1.Size = new System.Drawing.Size(64, 27);
            this.toolStripLabel1.Text = "Line Index";
            // 
            // toolCmb_LineIndex
            // 
            this.toolCmb_LineIndex.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.toolCmb_LineIndex.Name = "toolCmb_LineIndex";
            this.toolCmb_LineIndex.Size = new System.Drawing.Size(75, 30);
            this.toolCmb_LineIndex.SelectedIndexChanged += new System.EventHandler(this.toolCmb_LineIndex_SelectedIndexChanged);
            // 
            // treeView1
            // 
            this.treeView1.ContextMenuStrip = this.contextMenuStrip1;
            this.treeView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.treeView1.Location = new System.Drawing.Point(0, 0);
            this.treeView1.Name = "treeView1";
            this.treeView1.Size = new System.Drawing.Size(296, 197);
            this.treeView1.TabIndex = 0;
            this.treeView1.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.treeView1_AfterSelect);
            this.treeView1.MouseClick += new System.Windows.Forms.MouseEventHandler(this.treeView1_MouseClick);
            // 
            // tlp_LocalTransform
            // 
            this.tlp_LocalTransform.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.tlp_LocalTransform.BackColor = System.Drawing.Color.Black;
            this.tlp_LocalTransform.ColumnCount = 4;
            this.tlp_LocalTransform.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tlp_LocalTransform.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tlp_LocalTransform.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tlp_LocalTransform.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tlp_LocalTransform.Controls.Add(this.label1, 0, 1);
            this.tlp_LocalTransform.Controls.Add(this.label2, 0, 2);
            this.tlp_LocalTransform.Controls.Add(this.label3, 1, 0);
            this.tlp_LocalTransform.Controls.Add(this.label4, 2, 0);
            this.tlp_LocalTransform.Controls.Add(this.label5, 3, 0);
            this.tlp_LocalTransform.Controls.Add(this.nud_ShiftX, 1, 1);
            this.tlp_LocalTransform.Controls.Add(this.nud_ShiftY, 2, 1);
            this.tlp_LocalTransform.Controls.Add(this.nud_ShiftZ, 3, 1);
            this.tlp_LocalTransform.Controls.Add(this.nud_RotateX, 1, 2);
            this.tlp_LocalTransform.Controls.Add(this.nud_RotateY, 2, 2);
            this.tlp_LocalTransform.Controls.Add(this.nud_RotateZ, 3, 2);
            this.tlp_LocalTransform.Location = new System.Drawing.Point(3, 487);
            this.tlp_LocalTransform.Name = "tlp_LocalTransform";
            this.tlp_LocalTransform.RowCount = 3;
            this.tlp_LocalTransform.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tlp_LocalTransform.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tlp_LocalTransform.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tlp_LocalTransform.Size = new System.Drawing.Size(254, 69);
            this.tlp_LocalTransform.TabIndex = 1;
            this.tlp_LocalTransform.Visible = false;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.SystemColors.ControlDark;
            this.label1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label1.Location = new System.Drawing.Point(0, 23);
            this.label1.Margin = new System.Windows.Forms.Padding(0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(63, 23);
            this.label1.TabIndex = 0;
            this.label1.Text = "Shift";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.LightGray;
            this.label2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label2.Location = new System.Drawing.Point(0, 46);
            this.label2.Margin = new System.Windows.Forms.Padding(0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(63, 23);
            this.label2.TabIndex = 1;
            this.label2.Text = "Rotate";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.BackColor = System.Drawing.Color.LightCoral;
            this.label3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label3.Location = new System.Drawing.Point(63, 0);
            this.label3.Margin = new System.Windows.Forms.Padding(0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(63, 23);
            this.label3.TabIndex = 2;
            this.label3.Text = "X";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.BackColor = System.Drawing.Color.LightGreen;
            this.label4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label4.Location = new System.Drawing.Point(126, 0);
            this.label4.Margin = new System.Windows.Forms.Padding(0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(63, 23);
            this.label4.TabIndex = 3;
            this.label4.Text = "Y";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.BackColor = System.Drawing.Color.LightBlue;
            this.label5.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label5.Location = new System.Drawing.Point(189, 0);
            this.label5.Margin = new System.Windows.Forms.Padding(0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(65, 23);
            this.label5.TabIndex = 4;
            this.label5.Text = "Z";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // nud_ShiftX
            // 
            this.nud_ShiftX.Dock = System.Windows.Forms.DockStyle.Fill;
            this.nud_ShiftX.InterceptArrowKeys = false;
            this.nud_ShiftX.Location = new System.Drawing.Point(63, 23);
            this.nud_ShiftX.Margin = new System.Windows.Forms.Padding(0);
            this.nud_ShiftX.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            this.nud_ShiftX.Minimum = new decimal(new int[] {
            10000,
            0,
            0,
            -2147483648});
            this.nud_ShiftX.Name = "nud_ShiftX";
            this.nud_ShiftX.Size = new System.Drawing.Size(63, 22);
            this.nud_ShiftX.TabIndex = 5;
            this.nud_ShiftX.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.nud_ShiftX.ValueChanged += new System.EventHandler(this.nud_ShiftX_ValueChanged);
            // 
            // nud_ShiftY
            // 
            this.nud_ShiftY.Dock = System.Windows.Forms.DockStyle.Fill;
            this.nud_ShiftY.InterceptArrowKeys = false;
            this.nud_ShiftY.Location = new System.Drawing.Point(126, 23);
            this.nud_ShiftY.Margin = new System.Windows.Forms.Padding(0);
            this.nud_ShiftY.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            this.nud_ShiftY.Minimum = new decimal(new int[] {
            10000,
            0,
            0,
            -2147483648});
            this.nud_ShiftY.Name = "nud_ShiftY";
            this.nud_ShiftY.Size = new System.Drawing.Size(63, 22);
            this.nud_ShiftY.TabIndex = 6;
            this.nud_ShiftY.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.nud_ShiftY.ValueChanged += new System.EventHandler(this.nud_ShiftX_ValueChanged);
            // 
            // nud_ShiftZ
            // 
            this.nud_ShiftZ.Dock = System.Windows.Forms.DockStyle.Fill;
            this.nud_ShiftZ.InterceptArrowKeys = false;
            this.nud_ShiftZ.Location = new System.Drawing.Point(189, 23);
            this.nud_ShiftZ.Margin = new System.Windows.Forms.Padding(0);
            this.nud_ShiftZ.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            this.nud_ShiftZ.Minimum = new decimal(new int[] {
            10000,
            0,
            0,
            -2147483648});
            this.nud_ShiftZ.Name = "nud_ShiftZ";
            this.nud_ShiftZ.Size = new System.Drawing.Size(65, 22);
            this.nud_ShiftZ.TabIndex = 7;
            this.nud_ShiftZ.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.nud_ShiftZ.ValueChanged += new System.EventHandler(this.nud_ShiftX_ValueChanged);
            // 
            // nud_RotateX
            // 
            this.nud_RotateX.Dock = System.Windows.Forms.DockStyle.Fill;
            this.nud_RotateX.InterceptArrowKeys = false;
            this.nud_RotateX.Location = new System.Drawing.Point(63, 46);
            this.nud_RotateX.Margin = new System.Windows.Forms.Padding(0);
            this.nud_RotateX.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            this.nud_RotateX.Minimum = new decimal(new int[] {
            10000,
            0,
            0,
            -2147483648});
            this.nud_RotateX.Name = "nud_RotateX";
            this.nud_RotateX.Size = new System.Drawing.Size(63, 22);
            this.nud_RotateX.TabIndex = 8;
            this.nud_RotateX.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.nud_RotateX.ValueChanged += new System.EventHandler(this.nud_ShiftX_ValueChanged);
            // 
            // nud_RotateY
            // 
            this.nud_RotateY.Dock = System.Windows.Forms.DockStyle.Fill;
            this.nud_RotateY.InterceptArrowKeys = false;
            this.nud_RotateY.Location = new System.Drawing.Point(126, 46);
            this.nud_RotateY.Margin = new System.Windows.Forms.Padding(0);
            this.nud_RotateY.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            this.nud_RotateY.Minimum = new decimal(new int[] {
            10000,
            0,
            0,
            -2147483648});
            this.nud_RotateY.Name = "nud_RotateY";
            this.nud_RotateY.Size = new System.Drawing.Size(63, 22);
            this.nud_RotateY.TabIndex = 9;
            this.nud_RotateY.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.nud_RotateY.ValueChanged += new System.EventHandler(this.nud_ShiftX_ValueChanged);
            // 
            // nud_RotateZ
            // 
            this.nud_RotateZ.Dock = System.Windows.Forms.DockStyle.Fill;
            this.nud_RotateZ.Location = new System.Drawing.Point(189, 46);
            this.nud_RotateZ.Margin = new System.Windows.Forms.Padding(0);
            this.nud_RotateZ.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            this.nud_RotateZ.Minimum = new decimal(new int[] {
            10000,
            0,
            0,
            -2147483648});
            this.nud_RotateZ.Name = "nud_RotateZ";
            this.nud_RotateZ.Size = new System.Drawing.Size(65, 22);
            this.nud_RotateZ.TabIndex = 10;
            this.nud_RotateZ.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.nud_RotateZ.ValueChanged += new System.EventHandler(this.nud_ShiftX_ValueChanged);
            // 
            // trackBar_RotateSensitivity
            // 
            this.trackBar_RotateSensitivity.AutoSize = false;
            this.trackBar_RotateSensitivity.Location = new System.Drawing.Point(3, 3);
            this.trackBar_RotateSensitivity.Maximum = 20;
            this.trackBar_RotateSensitivity.Minimum = 1;
            this.trackBar_RotateSensitivity.Name = "trackBar_RotateSensitivity";
            this.trackBar_RotateSensitivity.Size = new System.Drawing.Size(104, 24);
            this.trackBar_RotateSensitivity.TabIndex = 0;
            this.trackBar_RotateSensitivity.TickStyle = System.Windows.Forms.TickStyle.None;
            this.trackBar_RotateSensitivity.Value = 10;
            this.trackBar_RotateSensitivity.Visible = false;
            this.trackBar_RotateSensitivity.Scroll += new System.EventHandler(this.trackBar_RotateSensitivity_Scroll);
            this.trackBar_RotateSensitivity.MouseUp += new System.Windows.Forms.MouseEventHandler(this.trackBar_RotateSensitivity_MouseUp);
            // 
            // timer1
            // 
            this.timer1.Interval = 50;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // Display3DControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tableLayoutPanel1);
            this.Name = "Display3DControl";
            this.Size = new System.Drawing.Size(748, 633);
            this.SizeChanged += new System.EventHandler(this.Display3DControl_SizeChanged);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.splitContainer2.Panel1.ResumeLayout(false);
            this.splitContainer2.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).EndInit();
            this.splitContainer2.ResumeLayout(false);
            this.tableLayoutPanel2.ResumeLayout(false);
            this.tableLayoutPanel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.toolStrip2.ResumeLayout(false);
            this.toolStrip2.PerformLayout();
            this.contextMenuStrip1.ResumeLayout(false);
            this.tlp_LocalTransform.ResumeLayout(false);
            this.tlp_LocalTransform.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nud_ShiftX)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nud_ShiftY)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nud_ShiftZ)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nud_RotateX)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nud_RotateY)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nud_RotateZ)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar_RotateSensitivity)).EndInit();
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
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.ToolStripDropDownButton toolStripDropDownButton1;
        private System.Windows.Forms.ToolStripMenuItem saveABBModFileToolStripMenuItem;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem addToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saveABBModFileWithRobTargetToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem clearCollectionToolStripMenuItem;
        private System.Windows.Forms.ToolStripStatusLabel toolStatusLbl_CurrentSelectLineIndex;
        private System.Windows.Forms.ToolStripMenuItem reversePathToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saveXYZPointCloudToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem1;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripMenuItem saveSelectedXYZPointCloudToolStripMenuItem;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private System.Windows.Forms.ToolStrip toolStrip2;
        private System.Windows.Forms.ToolStripComboBox toolCmb_LineIndex;
        private System.Windows.Forms.ToolStripStatusLabel toolStatusLbl_SelectObjectIndex;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel2;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
        private System.Windows.Forms.ToolStripButton toolBtn_ShowAddPathForm;
        private System.Windows.Forms.ToolStripMenuItem addAllToCollectionToolStripMenuItem;
        private System.Windows.Forms.ToolStripLabel toolStripLabel1;
        private System.Windows.Forms.ToolStripDropDownButton toolBtn_Setting;
        private System.Windows.Forms.ToolStripMenuItem changeColorSizeToolStripMenuItem;
        private System.Windows.Forms.TrackBar trackBar_RotateSensitivity;
        private System.Windows.Forms.ToolStripMenuItem changeRotateSensitivityToolStripMenuItem;
        private System.Windows.Forms.TableLayoutPanel tlp_LocalTransform;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.NumericUpDown nud_ShiftX;
        private System.Windows.Forms.NumericUpDown nud_ShiftY;
        private System.Windows.Forms.NumericUpDown nud_ShiftZ;
        private System.Windows.Forms.NumericUpDown nud_RotateX;
        private System.Windows.Forms.NumericUpDown nud_RotateY;
        private System.Windows.Forms.NumericUpDown nud_RotateZ;
        private System.Windows.Forms.ToolStripMenuItem manualShiftRotateToolStripMenuItem;
        private System.Windows.Forms.ToolStripDropDownButton toolStripDropDownButton2;
        private System.Windows.Forms.ToolStripMenuItem upToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem bottomViewToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem rightViewToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem lefeViewToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem frontViewToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem backViewToolStripMenuItem;
    }
}

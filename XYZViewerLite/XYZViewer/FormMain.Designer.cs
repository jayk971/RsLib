namespace Automation
{
    partial class FormMain
    {
        /// <summary>
        /// 設計工具所需的變數。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清除任何使用中的資源。
        /// </summary>
        /// <param name="disposing">如果應該處置 Managed 資源則為 true，否則為 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form 設計工具產生的程式碼

        /// <summary>
        /// 此為設計工具支援所需的方法 - 請勿使用程式碼編輯器
        /// 修改這個方法的內容。
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormMain));
            this.pnl3DGraph = new System.Windows.Forms.Panel();
            this.btn_ClearDisplay = new System.Windows.Forms.Button();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.chkShowPolyline1 = new System.Windows.Forms.CheckBox();
            this.lblPoyline1Count = new System.Windows.Forms.Label();
            this.chkShowPolyline2 = new System.Windows.Forms.CheckBox();
            this.lblPoyline2Count = new System.Windows.Forms.Label();
            this.chkShowPolyline3 = new System.Windows.Forms.CheckBox();
            this.lblPoyline3Count = new System.Windows.Forms.Label();
            this.chkShowPolyline4 = new System.Windows.Forms.CheckBox();
            this.lblPoyline4Count = new System.Windows.Forms.Label();
            this.chkShowPolyline5 = new System.Windows.Forms.CheckBox();
            this.lblPoyline5Count = new System.Windows.Forms.Label();
            this.chkShowPoints1 = new System.Windows.Forms.CheckBox();
            this.lblPoints1Count = new System.Windows.Forms.Label();
            this.chkShowPoints2 = new System.Windows.Forms.CheckBox();
            this.lblPoints2Count = new System.Windows.Forms.Label();
            this.chkShowOptPath1 = new System.Windows.Forms.CheckBox();
            this.lblOpt1Count = new System.Windows.Forms.Label();
            this.chkShowOptPath2 = new System.Windows.Forms.CheckBox();
            this.lblOpt2Count = new System.Windows.Forms.Label();
            this.chkShowOptPath3 = new System.Windows.Forms.CheckBox();
            this.lblOpt3Count = new System.Windows.Forms.Label();
            this.chbx_ShowOPT1NormalV = new System.Windows.Forms.CheckBox();
            this.chbx_ShowOPT2NormalV = new System.Windows.Forms.CheckBox();
            this.chbx_ShowOPT3NormalV = new System.Windows.Forms.CheckBox();
            this.tableLayoutPanel3 = new System.Windows.Forms.TableLayoutPanel();
            this.chkMouseMeasure = new System.Windows.Forms.CheckBox();
            this.lblXYZ1 = new System.Windows.Forms.Label();
            this.txtDistance = new System.Windows.Forms.TextBox();
            this.lblDistance = new System.Windows.Forms.Label();
            this.txtX1 = new System.Windows.Forms.TextBox();
            this.txtY1 = new System.Windows.Forms.TextBox();
            this.txtZ2 = new System.Windows.Forms.TextBox();
            this.lblXYZ2 = new System.Windows.Forms.Label();
            this.txtY2 = new System.Windows.Forms.TextBox();
            this.txtZ1 = new System.Windows.Forms.TextBox();
            this.txtX2 = new System.Windows.Forms.TextBox();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.btn_WatchFolderSet = new System.Windows.Forms.Button();
            this.tmrRender = new System.Windows.Forms.Timer(this.components);
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.tableLayoutPanel4 = new System.Windows.Forms.TableLayoutPanel();
            this.btn_CollapsePanel = new System.Windows.Forms.Button();
            this.pnl3DGraph.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            this.tableLayoutPanel3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.tableLayoutPanel4.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnl3DGraph
            // 
            this.pnl3DGraph.BackColor = System.Drawing.Color.Black;
            this.pnl3DGraph.Controls.Add(this.btn_ClearDisplay);
            this.pnl3DGraph.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnl3DGraph.Location = new System.Drawing.Point(3, 13);
            this.pnl3DGraph.Name = "pnl3DGraph";
            this.pnl3DGraph.Size = new System.Drawing.Size(1120, 554);
            this.pnl3DGraph.TabIndex = 0;
            // 
            // btn_ClearDisplay
            // 
            this.btn_ClearDisplay.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_ClearDisplay.ForeColor = System.Drawing.Color.White;
            this.btn_ClearDisplay.Location = new System.Drawing.Point(3, 3);
            this.btn_ClearDisplay.Name = "btn_ClearDisplay";
            this.btn_ClearDisplay.Size = new System.Drawing.Size(75, 23);
            this.btn_ClearDisplay.TabIndex = 0;
            this.btn_ClearDisplay.Text = "Clear";
            this.btn_ClearDisplay.UseVisualStyleBackColor = true;
            this.btn_ClearDisplay.Click += new System.EventHandler(this.btn_ClearDisplay_Click);
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.BackColor = System.Drawing.Color.Black;
            this.tableLayoutPanel2.ColumnCount = 9;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 23.33333F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 10F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 23.33333F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 10F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 23.33333F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 10F));
            this.tableLayoutPanel2.Controls.Add(this.chkShowPolyline1, 0, 0);
            this.tableLayoutPanel2.Controls.Add(this.lblPoyline1Count, 2, 0);
            this.tableLayoutPanel2.Controls.Add(this.chkShowPolyline2, 3, 0);
            this.tableLayoutPanel2.Controls.Add(this.lblPoyline2Count, 5, 0);
            this.tableLayoutPanel2.Controls.Add(this.chkShowPolyline3, 6, 0);
            this.tableLayoutPanel2.Controls.Add(this.lblPoyline3Count, 8, 0);
            this.tableLayoutPanel2.Controls.Add(this.chkShowPolyline4, 0, 1);
            this.tableLayoutPanel2.Controls.Add(this.lblPoyline4Count, 2, 1);
            this.tableLayoutPanel2.Controls.Add(this.chkShowPolyline5, 3, 1);
            this.tableLayoutPanel2.Controls.Add(this.lblPoyline5Count, 5, 1);
            this.tableLayoutPanel2.Controls.Add(this.chkShowPoints1, 0, 2);
            this.tableLayoutPanel2.Controls.Add(this.lblPoints1Count, 2, 2);
            this.tableLayoutPanel2.Controls.Add(this.chkShowPoints2, 3, 2);
            this.tableLayoutPanel2.Controls.Add(this.lblPoints2Count, 5, 2);
            this.tableLayoutPanel2.Controls.Add(this.chkShowOptPath1, 0, 3);
            this.tableLayoutPanel2.Controls.Add(this.lblOpt1Count, 2, 3);
            this.tableLayoutPanel2.Controls.Add(this.chkShowOptPath2, 3, 3);
            this.tableLayoutPanel2.Controls.Add(this.lblOpt2Count, 5, 3);
            this.tableLayoutPanel2.Controls.Add(this.chkShowOptPath3, 6, 3);
            this.tableLayoutPanel2.Controls.Add(this.lblOpt3Count, 8, 3);
            this.tableLayoutPanel2.Controls.Add(this.chbx_ShowOPT1NormalV, 1, 3);
            this.tableLayoutPanel2.Controls.Add(this.chbx_ShowOPT2NormalV, 4, 3);
            this.tableLayoutPanel2.Controls.Add(this.chbx_ShowOPT3NormalV, 7, 3);
            this.tableLayoutPanel2.Controls.Add(this.tableLayoutPanel3, 0, 4);
            this.tableLayoutPanel2.Controls.Add(this.button1, 6, 4);
            this.tableLayoutPanel2.Controls.Add(this.button2, 6, 5);
            this.tableLayoutPanel2.Controls.Add(this.btn_WatchFolderSet, 3, 4);
            this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel2.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 9;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 35F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 35F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 35F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 35F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 35F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 35F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 35F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 35F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 35F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(1126, 293);
            this.tableLayoutPanel2.TabIndex = 9;
            this.tableLayoutPanel2.Paint += new System.Windows.Forms.PaintEventHandler(this.tableLayoutPanel2_Paint);
            // 
            // chkShowPolyline1
            // 
            this.chkShowPolyline1.AllowDrop = true;
            this.chkShowPolyline1.AutoSize = true;
            this.chkShowPolyline1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.chkShowPolyline1.Font = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.chkShowPolyline1.ForeColor = System.Drawing.Color.LightGray;
            this.chkShowPolyline1.Location = new System.Drawing.Point(3, 3);
            this.chkShowPolyline1.Name = "chkShowPolyline1";
            this.chkShowPolyline1.Size = new System.Drawing.Size(242, 29);
            this.chkShowPolyline1.TabIndex = 1;
            this.chkShowPolyline1.Text = "Point Cloud 1";
            this.chkShowPolyline1.UseVisualStyleBackColor = true;
            this.chkShowPolyline1.CheckedChanged += new System.EventHandler(this.chkShowPolyline1_CheckedChanged);
            this.chkShowPolyline1.DragDrop += new System.Windows.Forms.DragEventHandler(this.chkShowPolyline1_DragDrop);
            this.chkShowPolyline1.DragEnter += new System.Windows.Forms.DragEventHandler(this.chkShowPolyline1_DragEnter);
            // 
            // lblPoyline1Count
            // 
            this.lblPoyline1Count.AutoSize = true;
            this.lblPoyline1Count.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPoyline1Count.Location = new System.Drawing.Point(271, 0);
            this.lblPoyline1Count.Name = "lblPoyline1Count";
            this.lblPoyline1Count.Size = new System.Drawing.Size(84, 16);
            this.lblPoyline1Count.TabIndex = 8;
            this.lblPoyline1Count.Text = "999L 99999P";
            // 
            // chkShowPolyline2
            // 
            this.chkShowPolyline2.AllowDrop = true;
            this.chkShowPolyline2.AutoSize = true;
            this.chkShowPolyline2.Font = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.chkShowPolyline2.ForeColor = System.Drawing.Color.CornflowerBlue;
            this.chkShowPolyline2.Location = new System.Drawing.Point(377, 3);
            this.chkShowPolyline2.Name = "chkShowPolyline2";
            this.chkShowPolyline2.Size = new System.Drawing.Size(132, 25);
            this.chkShowPolyline2.TabIndex = 2;
            this.chkShowPolyline2.Text = "Point Cloud 2";
            this.chkShowPolyline2.UseVisualStyleBackColor = true;
            this.chkShowPolyline2.CheckedChanged += new System.EventHandler(this.chkShowPolyline2_CheckedChanged);
            this.chkShowPolyline2.DragDrop += new System.Windows.Forms.DragEventHandler(this.chkShowPolyline2_DragDrop);
            this.chkShowPolyline2.DragEnter += new System.Windows.Forms.DragEventHandler(this.chkShowPolyline2_DragEnter);
            // 
            // lblPoyline2Count
            // 
            this.lblPoyline2Count.AutoSize = true;
            this.lblPoyline2Count.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPoyline2Count.Location = new System.Drawing.Point(645, 0);
            this.lblPoyline2Count.Name = "lblPoyline2Count";
            this.lblPoyline2Count.Size = new System.Drawing.Size(84, 16);
            this.lblPoyline2Count.TabIndex = 15;
            this.lblPoyline2Count.Text = "999L 99999P";
            // 
            // chkShowPolyline3
            // 
            this.chkShowPolyline3.AllowDrop = true;
            this.chkShowPolyline3.AutoSize = true;
            this.chkShowPolyline3.Font = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.chkShowPolyline3.ForeColor = System.Drawing.Color.DarkKhaki;
            this.chkShowPolyline3.Location = new System.Drawing.Point(751, 3);
            this.chkShowPolyline3.Name = "chkShowPolyline3";
            this.chkShowPolyline3.Size = new System.Drawing.Size(132, 25);
            this.chkShowPolyline3.TabIndex = 3;
            this.chkShowPolyline3.Text = "Point Cloud 3";
            this.chkShowPolyline3.UseVisualStyleBackColor = true;
            this.chkShowPolyline3.CheckedChanged += new System.EventHandler(this.chkShowPolyline3_CheckedChanged);
            this.chkShowPolyline3.DragDrop += new System.Windows.Forms.DragEventHandler(this.chkShowPolyline3_DragDrop);
            this.chkShowPolyline3.DragEnter += new System.Windows.Forms.DragEventHandler(this.chkShowPolyline3_DragEnter);
            // 
            // lblPoyline3Count
            // 
            this.lblPoyline3Count.AutoSize = true;
            this.lblPoyline3Count.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPoyline3Count.Location = new System.Drawing.Point(1019, 0);
            this.lblPoyline3Count.Name = "lblPoyline3Count";
            this.lblPoyline3Count.Size = new System.Drawing.Size(84, 16);
            this.lblPoyline3Count.TabIndex = 16;
            this.lblPoyline3Count.Text = "999L 99999P";
            // 
            // chkShowPolyline4
            // 
            this.chkShowPolyline4.AllowDrop = true;
            this.chkShowPolyline4.AutoSize = true;
            this.chkShowPolyline4.Font = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.chkShowPolyline4.ForeColor = System.Drawing.Color.DarkOrange;
            this.chkShowPolyline4.Location = new System.Drawing.Point(3, 38);
            this.chkShowPolyline4.Name = "chkShowPolyline4";
            this.chkShowPolyline4.Size = new System.Drawing.Size(132, 25);
            this.chkShowPolyline4.TabIndex = 19;
            this.chkShowPolyline4.Text = "Point Cloud 4";
            this.chkShowPolyline4.UseVisualStyleBackColor = true;
            this.chkShowPolyline4.CheckedChanged += new System.EventHandler(this.chkShowPolyline4_CheckedChanged);
            this.chkShowPolyline4.DragDrop += new System.Windows.Forms.DragEventHandler(this.chkShowPolyline4_DragDrop);
            this.chkShowPolyline4.DragEnter += new System.Windows.Forms.DragEventHandler(this.chkShowPolyline4_DragEnter);
            // 
            // lblPoyline4Count
            // 
            this.lblPoyline4Count.AutoSize = true;
            this.lblPoyline4Count.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPoyline4Count.Location = new System.Drawing.Point(271, 35);
            this.lblPoyline4Count.Name = "lblPoyline4Count";
            this.lblPoyline4Count.Size = new System.Drawing.Size(84, 16);
            this.lblPoyline4Count.TabIndex = 20;
            this.lblPoyline4Count.Text = "999L 99999P";
            // 
            // chkShowPolyline5
            // 
            this.chkShowPolyline5.AllowDrop = true;
            this.chkShowPolyline5.AutoSize = true;
            this.chkShowPolyline5.Font = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.chkShowPolyline5.ForeColor = System.Drawing.Color.OrangeRed;
            this.chkShowPolyline5.Location = new System.Drawing.Point(377, 38);
            this.chkShowPolyline5.Name = "chkShowPolyline5";
            this.chkShowPolyline5.Size = new System.Drawing.Size(132, 25);
            this.chkShowPolyline5.TabIndex = 21;
            this.chkShowPolyline5.Text = "Point Cloud 5";
            this.chkShowPolyline5.UseVisualStyleBackColor = true;
            this.chkShowPolyline5.CheckedChanged += new System.EventHandler(this.chkShowPolyline5_CheckedChanged);
            this.chkShowPolyline5.DragDrop += new System.Windows.Forms.DragEventHandler(this.chkShowPolyline5_DragDrop);
            this.chkShowPolyline5.DragEnter += new System.Windows.Forms.DragEventHandler(this.chkShowPolyline5_DragEnter);
            // 
            // lblPoyline5Count
            // 
            this.lblPoyline5Count.AutoSize = true;
            this.lblPoyline5Count.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPoyline5Count.Location = new System.Drawing.Point(645, 35);
            this.lblPoyline5Count.Name = "lblPoyline5Count";
            this.lblPoyline5Count.Size = new System.Drawing.Size(84, 16);
            this.lblPoyline5Count.TabIndex = 22;
            this.lblPoyline5Count.Text = "999L 99999P";
            // 
            // chkShowPoints1
            // 
            this.chkShowPoints1.AllowDrop = true;
            this.chkShowPoints1.AutoSize = true;
            this.chkShowPoints1.Font = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.chkShowPoints1.ForeColor = System.Drawing.Color.Magenta;
            this.chkShowPoints1.Location = new System.Drawing.Point(3, 73);
            this.chkShowPoints1.Name = "chkShowPoints1";
            this.chkShowPoints1.Size = new System.Drawing.Size(91, 25);
            this.chkShowPoints1.TabIndex = 4;
            this.chkShowPoints1.Text = "Points 1";
            this.chkShowPoints1.UseVisualStyleBackColor = true;
            this.chkShowPoints1.CheckedChanged += new System.EventHandler(this.chkShowPoints1_CheckedChanged);
            this.chkShowPoints1.DragDrop += new System.Windows.Forms.DragEventHandler(this.chkShowPoints1_DragDrop);
            this.chkShowPoints1.DragEnter += new System.Windows.Forms.DragEventHandler(this.chkShowPoints1_DragEnter);
            // 
            // lblPoints1Count
            // 
            this.lblPoints1Count.AutoSize = true;
            this.lblPoints1Count.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPoints1Count.Location = new System.Drawing.Point(271, 70);
            this.lblPoints1Count.Name = "lblPoints1Count";
            this.lblPoints1Count.Size = new System.Drawing.Size(52, 16);
            this.lblPoints1Count.TabIndex = 17;
            this.lblPoints1Count.Text = "99999P";
            // 
            // chkShowPoints2
            // 
            this.chkShowPoints2.AllowDrop = true;
            this.chkShowPoints2.AutoSize = true;
            this.chkShowPoints2.Font = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.chkShowPoints2.ForeColor = System.Drawing.Color.MediumSlateBlue;
            this.chkShowPoints2.Location = new System.Drawing.Point(377, 73);
            this.chkShowPoints2.Name = "chkShowPoints2";
            this.chkShowPoints2.Size = new System.Drawing.Size(91, 25);
            this.chkShowPoints2.TabIndex = 23;
            this.chkShowPoints2.Text = "Points 2";
            this.chkShowPoints2.UseVisualStyleBackColor = true;
            this.chkShowPoints2.CheckedChanged += new System.EventHandler(this.chkShowPoints2_CheckedChanged);
            this.chkShowPoints2.DragDrop += new System.Windows.Forms.DragEventHandler(this.chkShowPoints2_DragDrop);
            this.chkShowPoints2.DragEnter += new System.Windows.Forms.DragEventHandler(this.chkShowPoints2_DragEnter);
            // 
            // lblPoints2Count
            // 
            this.lblPoints2Count.AutoSize = true;
            this.lblPoints2Count.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPoints2Count.Location = new System.Drawing.Point(645, 70);
            this.lblPoints2Count.Name = "lblPoints2Count";
            this.lblPoints2Count.Size = new System.Drawing.Size(52, 16);
            this.lblPoints2Count.TabIndex = 24;
            this.lblPoints2Count.Text = "99999P";
            // 
            // chkShowOptPath1
            // 
            this.chkShowOptPath1.AllowDrop = true;
            this.chkShowOptPath1.AutoSize = true;
            this.chkShowOptPath1.Font = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.chkShowOptPath1.ForeColor = System.Drawing.Color.Lime;
            this.chkShowOptPath1.Location = new System.Drawing.Point(3, 108);
            this.chkShowOptPath1.Name = "chkShowOptPath1";
            this.chkShowOptPath1.Size = new System.Drawing.Size(111, 25);
            this.chkShowOptPath1.TabIndex = 5;
            this.chkShowOptPath1.Text = "Opt Path 1";
            this.chkShowOptPath1.UseVisualStyleBackColor = true;
            this.chkShowOptPath1.CheckedChanged += new System.EventHandler(this.chkShowOptPath1_CheckedChanged);
            this.chkShowOptPath1.DragDrop += new System.Windows.Forms.DragEventHandler(this.chkShowOptPath1_DragDrop);
            this.chkShowOptPath1.DragEnter += new System.Windows.Forms.DragEventHandler(this.chkShowOptPath1_DragEnter);
            // 
            // lblOpt1Count
            // 
            this.lblOpt1Count.AutoSize = true;
            this.lblOpt1Count.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblOpt1Count.Location = new System.Drawing.Point(271, 105);
            this.lblOpt1Count.Name = "lblOpt1Count";
            this.lblOpt1Count.Size = new System.Drawing.Size(52, 16);
            this.lblOpt1Count.TabIndex = 18;
            this.lblOpt1Count.Text = "99999P";
            // 
            // chkShowOptPath2
            // 
            this.chkShowOptPath2.AllowDrop = true;
            this.chkShowOptPath2.AutoSize = true;
            this.chkShowOptPath2.Font = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.chkShowOptPath2.ForeColor = System.Drawing.Color.Cyan;
            this.chkShowOptPath2.Location = new System.Drawing.Point(377, 108);
            this.chkShowOptPath2.Name = "chkShowOptPath2";
            this.chkShowOptPath2.Size = new System.Drawing.Size(111, 25);
            this.chkShowOptPath2.TabIndex = 25;
            this.chkShowOptPath2.Text = "Opt Path 2";
            this.chkShowOptPath2.UseVisualStyleBackColor = true;
            this.chkShowOptPath2.CheckedChanged += new System.EventHandler(this.chkShowOptPath2_CheckedChanged);
            this.chkShowOptPath2.DragDrop += new System.Windows.Forms.DragEventHandler(this.chkShowOptPath2_DragDrop);
            this.chkShowOptPath2.DragEnter += new System.Windows.Forms.DragEventHandler(this.chkShowOptPath2_DragEnter);
            // 
            // lblOpt2Count
            // 
            this.lblOpt2Count.AutoSize = true;
            this.lblOpt2Count.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblOpt2Count.Location = new System.Drawing.Point(645, 105);
            this.lblOpt2Count.Name = "lblOpt2Count";
            this.lblOpt2Count.Size = new System.Drawing.Size(52, 16);
            this.lblOpt2Count.TabIndex = 26;
            this.lblOpt2Count.Text = "99999P";
            // 
            // chkShowOptPath3
            // 
            this.chkShowOptPath3.AllowDrop = true;
            this.chkShowOptPath3.AutoSize = true;
            this.chkShowOptPath3.Font = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.chkShowOptPath3.ForeColor = System.Drawing.Color.Gold;
            this.chkShowOptPath3.Location = new System.Drawing.Point(751, 108);
            this.chkShowOptPath3.Name = "chkShowOptPath3";
            this.chkShowOptPath3.Size = new System.Drawing.Size(111, 25);
            this.chkShowOptPath3.TabIndex = 31;
            this.chkShowOptPath3.Text = "Opt Path 3";
            this.chkShowOptPath3.UseVisualStyleBackColor = true;
            this.chkShowOptPath3.CheckedChanged += new System.EventHandler(this.chkShowOptPath3_CheckedChanged);
            this.chkShowOptPath3.DragDrop += new System.Windows.Forms.DragEventHandler(this.chkShowOptPath3_DragDrop);
            this.chkShowOptPath3.DragEnter += new System.Windows.Forms.DragEventHandler(this.chkShowOptPath3_DragEnter);
            // 
            // lblOpt3Count
            // 
            this.lblOpt3Count.AutoSize = true;
            this.lblOpt3Count.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblOpt3Count.Location = new System.Drawing.Point(1019, 105);
            this.lblOpt3Count.Name = "lblOpt3Count";
            this.lblOpt3Count.Size = new System.Drawing.Size(52, 16);
            this.lblOpt3Count.TabIndex = 27;
            this.lblOpt3Count.Text = "99999P";
            // 
            // chbx_ShowOPT1NormalV
            // 
            this.chbx_ShowOPT1NormalV.Appearance = System.Windows.Forms.Appearance.Button;
            this.chbx_ShowOPT1NormalV.AutoSize = true;
            this.chbx_ShowOPT1NormalV.Font = new System.Drawing.Font("新細明體", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.chbx_ShowOPT1NormalV.ForeColor = System.Drawing.Color.Black;
            this.chbx_ShowOPT1NormalV.Location = new System.Drawing.Point(251, 108);
            this.chbx_ShowOPT1NormalV.Name = "chbx_ShowOPT1NormalV";
            this.chbx_ShowOPT1NormalV.Size = new System.Drawing.Size(14, 23);
            this.chbx_ShowOPT1NormalV.TabIndex = 27;
            this.chbx_ShowOPT1NormalV.Text = "Normal Vector";
            this.chbx_ShowOPT1NormalV.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.chbx_ShowOPT1NormalV.UseVisualStyleBackColor = true;
            this.chbx_ShowOPT1NormalV.CheckedChanged += new System.EventHandler(this.chbx_ShowOPT1NormalV_CheckedChanged);
            // 
            // chbx_ShowOPT2NormalV
            // 
            this.chbx_ShowOPT2NormalV.Appearance = System.Windows.Forms.Appearance.Button;
            this.chbx_ShowOPT2NormalV.AutoSize = true;
            this.chbx_ShowOPT2NormalV.Font = new System.Drawing.Font("新細明體", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.chbx_ShowOPT2NormalV.ForeColor = System.Drawing.Color.Black;
            this.chbx_ShowOPT2NormalV.Location = new System.Drawing.Point(625, 108);
            this.chbx_ShowOPT2NormalV.Name = "chbx_ShowOPT2NormalV";
            this.chbx_ShowOPT2NormalV.Size = new System.Drawing.Size(14, 23);
            this.chbx_ShowOPT2NormalV.TabIndex = 28;
            this.chbx_ShowOPT2NormalV.Text = "Normal Vector";
            this.chbx_ShowOPT2NormalV.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.chbx_ShowOPT2NormalV.UseVisualStyleBackColor = true;
            this.chbx_ShowOPT2NormalV.CheckedChanged += new System.EventHandler(this.chbx_ShowOPT2NormalV_CheckedChanged);
            // 
            // chbx_ShowOPT3NormalV
            // 
            this.chbx_ShowOPT3NormalV.Appearance = System.Windows.Forms.Appearance.Button;
            this.chbx_ShowOPT3NormalV.AutoSize = true;
            this.chbx_ShowOPT3NormalV.Font = new System.Drawing.Font("新細明體", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.chbx_ShowOPT3NormalV.ForeColor = System.Drawing.Color.Black;
            this.chbx_ShowOPT3NormalV.Location = new System.Drawing.Point(999, 108);
            this.chbx_ShowOPT3NormalV.Name = "chbx_ShowOPT3NormalV";
            this.chbx_ShowOPT3NormalV.Size = new System.Drawing.Size(14, 23);
            this.chbx_ShowOPT3NormalV.TabIndex = 29;
            this.chbx_ShowOPT3NormalV.Text = "Normal Vector";
            this.chbx_ShowOPT3NormalV.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.chbx_ShowOPT3NormalV.UseVisualStyleBackColor = true;
            this.chbx_ShowOPT3NormalV.CheckedChanged += new System.EventHandler(this.chbx_ShowOPT3NormalV_CheckedChanged);
            // 
            // tableLayoutPanel3
            // 
            this.tableLayoutPanel3.ColumnCount = 4;
            this.tableLayoutPanel2.SetColumnSpan(this.tableLayoutPanel3, 3);
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel3.Controls.Add(this.chkMouseMeasure, 0, 0);
            this.tableLayoutPanel3.Controls.Add(this.lblXYZ1, 0, 1);
            this.tableLayoutPanel3.Controls.Add(this.txtDistance, 2, 3);
            this.tableLayoutPanel3.Controls.Add(this.lblDistance, 0, 3);
            this.tableLayoutPanel3.Controls.Add(this.txtX1, 1, 1);
            this.tableLayoutPanel3.Controls.Add(this.txtY1, 2, 1);
            this.tableLayoutPanel3.Controls.Add(this.txtZ2, 3, 2);
            this.tableLayoutPanel3.Controls.Add(this.lblXYZ2, 0, 2);
            this.tableLayoutPanel3.Controls.Add(this.txtY2, 2, 2);
            this.tableLayoutPanel3.Controls.Add(this.txtZ1, 3, 1);
            this.tableLayoutPanel3.Controls.Add(this.txtX2, 1, 2);
            this.tableLayoutPanel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel3.ForeColor = System.Drawing.Color.White;
            this.tableLayoutPanel3.Location = new System.Drawing.Point(3, 143);
            this.tableLayoutPanel3.Name = "tableLayoutPanel3";
            this.tableLayoutPanel3.RowCount = 4;
            this.tableLayoutPanel2.SetRowSpan(this.tableLayoutPanel3, 4);
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel3.Size = new System.Drawing.Size(368, 134);
            this.tableLayoutPanel3.TabIndex = 9;
            // 
            // chkMouseMeasure
            // 
            this.chkMouseMeasure.AutoSize = true;
            this.tableLayoutPanel3.SetColumnSpan(this.chkMouseMeasure, 4);
            this.chkMouseMeasure.Dock = System.Windows.Forms.DockStyle.Fill;
            this.chkMouseMeasure.Font = new System.Drawing.Font("新細明體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.chkMouseMeasure.Location = new System.Drawing.Point(3, 3);
            this.chkMouseMeasure.Name = "chkMouseMeasure";
            this.chkMouseMeasure.Size = new System.Drawing.Size(362, 27);
            this.chkMouseMeasure.TabIndex = 8;
            this.chkMouseMeasure.Text = "Measure";
            this.chkMouseMeasure.UseVisualStyleBackColor = true;
            this.chkMouseMeasure.CheckedChanged += new System.EventHandler(this.chkMouseMeasure_CheckedChanged);
            // 
            // lblXYZ1
            // 
            this.lblXYZ1.AutoSize = true;
            this.lblXYZ1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblXYZ1.Font = new System.Drawing.Font("Verdana", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblXYZ1.Location = new System.Drawing.Point(3, 33);
            this.lblXYZ1.Name = "lblXYZ1";
            this.lblXYZ1.Size = new System.Drawing.Size(86, 33);
            this.lblXYZ1.TabIndex = 8;
            this.lblXYZ1.Text = "P1";
            // 
            // txtDistance
            // 
            this.txtDistance.BackColor = System.Drawing.Color.Black;
            this.txtDistance.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.tableLayoutPanel3.SetColumnSpan(this.txtDistance, 2);
            this.txtDistance.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtDistance.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtDistance.ForeColor = System.Drawing.Color.White;
            this.txtDistance.Location = new System.Drawing.Point(187, 102);
            this.txtDistance.Name = "txtDistance";
            this.txtDistance.ReadOnly = true;
            this.txtDistance.Size = new System.Drawing.Size(178, 20);
            this.txtDistance.TabIndex = 13;
            this.txtDistance.Text = "0.0";
            this.txtDistance.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // lblDistance
            // 
            this.lblDistance.AutoSize = true;
            this.tableLayoutPanel3.SetColumnSpan(this.lblDistance, 2);
            this.lblDistance.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblDistance.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDistance.Location = new System.Drawing.Point(3, 99);
            this.lblDistance.Name = "lblDistance";
            this.lblDistance.Size = new System.Drawing.Size(178, 35);
            this.lblDistance.TabIndex = 14;
            this.lblDistance.Text = "Distance";
            this.lblDistance.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtX1
            // 
            this.txtX1.BackColor = System.Drawing.Color.Black;
            this.txtX1.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtX1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtX1.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtX1.ForeColor = System.Drawing.Color.White;
            this.txtX1.Location = new System.Drawing.Point(95, 36);
            this.txtX1.Name = "txtX1";
            this.txtX1.ReadOnly = true;
            this.txtX1.Size = new System.Drawing.Size(86, 20);
            this.txtX1.TabIndex = 6;
            this.txtX1.Text = "0.0";
            this.txtX1.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // txtY1
            // 
            this.txtY1.BackColor = System.Drawing.Color.Black;
            this.txtY1.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtY1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtY1.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtY1.ForeColor = System.Drawing.Color.White;
            this.txtY1.Location = new System.Drawing.Point(187, 36);
            this.txtY1.Name = "txtY1";
            this.txtY1.ReadOnly = true;
            this.txtY1.Size = new System.Drawing.Size(86, 20);
            this.txtY1.TabIndex = 7;
            this.txtY1.Text = "0.0";
            this.txtY1.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // txtZ2
            // 
            this.txtZ2.BackColor = System.Drawing.Color.Black;
            this.txtZ2.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtZ2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtZ2.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtZ2.ForeColor = System.Drawing.Color.White;
            this.txtZ2.Location = new System.Drawing.Point(279, 69);
            this.txtZ2.Name = "txtZ2";
            this.txtZ2.ReadOnly = true;
            this.txtZ2.Size = new System.Drawing.Size(86, 20);
            this.txtZ2.TabIndex = 12;
            this.txtZ2.Text = "0.0";
            this.txtZ2.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // lblXYZ2
            // 
            this.lblXYZ2.AutoSize = true;
            this.lblXYZ2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblXYZ2.Font = new System.Drawing.Font("Verdana", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblXYZ2.Location = new System.Drawing.Point(3, 66);
            this.lblXYZ2.Name = "lblXYZ2";
            this.lblXYZ2.Size = new System.Drawing.Size(86, 33);
            this.lblXYZ2.TabIndex = 11;
            this.lblXYZ2.Text = "P2";
            // 
            // txtY2
            // 
            this.txtY2.BackColor = System.Drawing.Color.Black;
            this.txtY2.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtY2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtY2.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtY2.ForeColor = System.Drawing.Color.White;
            this.txtY2.Location = new System.Drawing.Point(187, 69);
            this.txtY2.Name = "txtY2";
            this.txtY2.ReadOnly = true;
            this.txtY2.Size = new System.Drawing.Size(86, 20);
            this.txtY2.TabIndex = 10;
            this.txtY2.Text = "0.0";
            this.txtY2.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // txtZ1
            // 
            this.txtZ1.BackColor = System.Drawing.Color.Black;
            this.txtZ1.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtZ1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtZ1.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtZ1.ForeColor = System.Drawing.Color.White;
            this.txtZ1.Location = new System.Drawing.Point(279, 36);
            this.txtZ1.Name = "txtZ1";
            this.txtZ1.ReadOnly = true;
            this.txtZ1.Size = new System.Drawing.Size(86, 20);
            this.txtZ1.TabIndex = 8;
            this.txtZ1.Text = "0.0";
            this.txtZ1.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // txtX2
            // 
            this.txtX2.BackColor = System.Drawing.Color.Black;
            this.txtX2.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtX2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtX2.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtX2.ForeColor = System.Drawing.Color.White;
            this.txtX2.Location = new System.Drawing.Point(95, 69);
            this.txtX2.Name = "txtX2";
            this.txtX2.ReadOnly = true;
            this.txtX2.Size = new System.Drawing.Size(86, 20);
            this.txtX2.TabIndex = 9;
            this.txtX2.Text = "0.0";
            this.txtX2.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(751, 143);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(96, 29);
            this.button1.TabIndex = 8;
            this.button1.Text = "button1";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Visible = false;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(751, 178);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(96, 29);
            this.button2.TabIndex = 27;
            this.button2.Text = "button2";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Visible = false;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // btn_WatchFolderSet
            // 
            this.btn_WatchFolderSet.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btn_WatchFolderSet.Location = new System.Drawing.Point(377, 143);
            this.btn_WatchFolderSet.Name = "btn_WatchFolderSet";
            this.btn_WatchFolderSet.Size = new System.Drawing.Size(242, 29);
            this.btn_WatchFolderSet.TabIndex = 32;
            this.btn_WatchFolderSet.Text = "Watch Folder";
            this.btn_WatchFolderSet.UseVisualStyleBackColor = true;
            this.btn_WatchFolderSet.Click += new System.EventHandler(this.btn_WatchFolderSet_Click);
            // 
            // tmrRender
            // 
            this.tmrRender.Interval = 60;
            this.tmrRender.Tick += new System.EventHandler(this.tmrRender_Tick);
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.tableLayoutPanel2);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.tableLayoutPanel4);
            this.splitContainer1.Size = new System.Drawing.Size(1126, 867);
            this.splitContainer1.SplitterDistance = 293;
            this.splitContainer1.TabIndex = 10;
            // 
            // tableLayoutPanel4
            // 
            this.tableLayoutPanel4.ColumnCount = 1;
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel4.Controls.Add(this.pnl3DGraph, 0, 1);
            this.tableLayoutPanel4.Controls.Add(this.btn_CollapsePanel, 0, 0);
            this.tableLayoutPanel4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel4.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel4.Name = "tableLayoutPanel4";
            this.tableLayoutPanel4.RowCount = 2;
            this.tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 10F));
            this.tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel4.Size = new System.Drawing.Size(1126, 570);
            this.tableLayoutPanel4.TabIndex = 0;
            // 
            // btn_CollapsePanel
            // 
            this.btn_CollapsePanel.BackColor = System.Drawing.Color.DeepSkyBlue;
            this.btn_CollapsePanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btn_CollapsePanel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_CollapsePanel.Location = new System.Drawing.Point(0, 0);
            this.btn_CollapsePanel.Margin = new System.Windows.Forms.Padding(0);
            this.btn_CollapsePanel.Name = "btn_CollapsePanel";
            this.btn_CollapsePanel.Size = new System.Drawing.Size(1126, 10);
            this.btn_CollapsePanel.TabIndex = 1;
            this.btn_CollapsePanel.Text = "button3";
            this.btn_CollapsePanel.UseVisualStyleBackColor = false;
            this.btn_CollapsePanel.Click += new System.EventHandler(this.btn_CollapsePanel_Click);
            // 
            // FormMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1126, 867);
            this.Controls.Add(this.splitContainer1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "FormMain";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "XYZViewerLite v1.3";
            this.Load += new System.EventHandler(this.FormMain_Load);
            this.pnl3DGraph.ResumeLayout(false);
            this.tableLayoutPanel2.ResumeLayout(false);
            this.tableLayoutPanel2.PerformLayout();
            this.tableLayoutPanel3.ResumeLayout(false);
            this.tableLayoutPanel3.PerformLayout();
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.tableLayoutPanel4.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        public System.Windows.Forms.Panel pnl3DGraph;
        private System.Windows.Forms.CheckBox chkShowPolyline1;
        private System.Windows.Forms.CheckBox chkShowPolyline2;
        private System.Windows.Forms.CheckBox chkShowPolyline3;
        private System.Windows.Forms.CheckBox chkShowPoints1;
        private System.Windows.Forms.CheckBox chkShowOptPath1;
        public System.Windows.Forms.TextBox txtZ1;
        public System.Windows.Forms.TextBox txtY1;
        public System.Windows.Forms.TextBox txtX1;
        public System.Windows.Forms.TextBox txtZ2;
        public System.Windows.Forms.TextBox txtY2;
        public System.Windows.Forms.TextBox txtX2;
        private System.Windows.Forms.Timer tmrRender;
        private System.Windows.Forms.Label lblDistance;
        public System.Windows.Forms.TextBox txtDistance;
        public System.Windows.Forms.Label lblXYZ1;
        public System.Windows.Forms.Label lblXYZ2;
        private System.Windows.Forms.CheckBox chkMouseMeasure;
        private System.Windows.Forms.Label lblOpt1Count;
        private System.Windows.Forms.Label lblPoints1Count;
        private System.Windows.Forms.Label lblPoyline3Count;
        private System.Windows.Forms.Label lblPoyline2Count;
        private System.Windows.Forms.Label lblPoyline1Count;
        private System.Windows.Forms.Label lblPoyline5Count;
        private System.Windows.Forms.CheckBox chkShowPolyline5;
        private System.Windows.Forms.Label lblPoyline4Count;
        private System.Windows.Forms.CheckBox chkShowPolyline4;
        private System.Windows.Forms.Label lblOpt2Count;
        private System.Windows.Forms.CheckBox chkShowOptPath2;
        private System.Windows.Forms.Label lblPoints2Count;
        private System.Windows.Forms.CheckBox chkShowPoints2;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel3;
        private System.Windows.Forms.CheckBox chbx_ShowOPT1NormalV;
        private System.Windows.Forms.CheckBox chbx_ShowOPT2NormalV;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.CheckBox chkShowOptPath3;
        private System.Windows.Forms.Label lblOpt3Count;
        private System.Windows.Forms.CheckBox chbx_ShowOPT3NormalV;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel4;
        private System.Windows.Forms.Button btn_CollapsePanel;
        private System.Windows.Forms.Button btn_WatchFolderSet;
        private System.Windows.Forms.Button btn_ClearDisplay;
    }
}



namespace RsLib.WatchFolder
{
    partial class FolderWatchControl
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
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.label3 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.tbx_WatchFilter = new System.Windows.Forms.TextBox();
            this.lbl_WatchedFolder = new System.Windows.Forms.Label();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.progressBar_RunStatus = new System.Windows.Forms.ToolStripProgressBar();
            this.tbx_TimeOut = new System.Windows.Forms.TextBox();
            this.btn_ApplyFilter = new System.Windows.Forms.Button();
            this.btn_OpenFolder = new System.Windows.Forms.Button();
            this.btn_StartMonitor = new System.Windows.Forms.ToolStripButton();
            this.btn_StopMonitor = new System.Windows.Forms.ToolStripButton();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.btn_ApplyTimeOut = new System.Windows.Forms.Button();
            this.tableLayoutPanel1.SuspendLayout();
            this.toolStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 4;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 98F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 75F));
            this.tableLayoutPanel1.Controls.Add(this.label3, 0, 3);
            this.tableLayoutPanel1.Controls.Add(this.label1, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.label2, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.tbx_WatchFilter, 1, 1);
            this.tableLayoutPanel1.Controls.Add(this.lbl_WatchedFolder, 1, 2);
            this.tableLayoutPanel1.Controls.Add(this.btn_ApplyFilter, 3, 1);
            this.tableLayoutPanel1.Controls.Add(this.btn_OpenFolder, 3, 2);
            this.tableLayoutPanel1.Controls.Add(this.toolStrip1, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.pictureBox1, 2, 2);
            this.tableLayoutPanel1.Controls.Add(this.tbx_TimeOut, 1, 3);
            this.tableLayoutPanel1.Controls.Add(this.btn_ApplyTimeOut, 3, 3);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Margin = new System.Windows.Forms.Padding(4);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 5;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 38F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 45F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 45F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 45F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(606, 207);
            this.tableLayoutPanel1.TabIndex = 0;
            this.tableLayoutPanel1.Paint += new System.Windows.Forms.PaintEventHandler(this.tableLayoutPanel1_Paint);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label3.Location = new System.Drawing.Point(4, 128);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(90, 45);
            this.label3.TabIndex = 11;
            this.label3.Text = "Time out";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label1.Location = new System.Drawing.Point(4, 38);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(90, 45);
            this.label1.TabIndex = 2;
            this.label1.Text = "Filter";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label2.Location = new System.Drawing.Point(4, 83);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(90, 45);
            this.label2.TabIndex = 3;
            this.label2.Text = "Folder";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // tbx_WatchFilter
            // 
            this.tbx_WatchFilter.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tbx_WatchFilter.Location = new System.Drawing.Point(102, 42);
            this.tbx_WatchFilter.Margin = new System.Windows.Forms.Padding(4);
            this.tbx_WatchFilter.Name = "tbx_WatchFilter";
            this.tbx_WatchFilter.Size = new System.Drawing.Size(395, 29);
            this.tbx_WatchFilter.TabIndex = 4;
            // 
            // lbl_WatchedFolder
            // 
            this.lbl_WatchedFolder.AutoSize = true;
            this.lbl_WatchedFolder.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lbl_WatchedFolder.Location = new System.Drawing.Point(102, 83);
            this.lbl_WatchedFolder.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lbl_WatchedFolder.Name = "lbl_WatchedFolder";
            this.lbl_WatchedFolder.Size = new System.Drawing.Size(395, 45);
            this.lbl_WatchedFolder.TabIndex = 5;
            this.lbl_WatchedFolder.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // toolStrip1
            // 
            this.tableLayoutPanel1.SetColumnSpan(this.toolStrip1, 4);
            this.toolStrip1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.toolStrip1.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.toolStrip1.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btn_StartMonitor,
            this.btn_StopMonitor,
            this.toolStripSeparator1,
            this.progressBar_RunStatus});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Padding = new System.Windows.Forms.Padding(0, 0, 3, 0);
            this.toolStrip1.Size = new System.Drawing.Size(606, 38);
            this.toolStrip1.TabIndex = 9;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 38);
            // 
            // progressBar_RunStatus
            // 
            this.progressBar_RunStatus.Name = "progressBar_RunStatus";
            this.progressBar_RunStatus.Size = new System.Drawing.Size(150, 33);
            // 
            // tbx_TimeOut
            // 
            this.tbx_TimeOut.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tbx_TimeOut.Location = new System.Drawing.Point(102, 132);
            this.tbx_TimeOut.Margin = new System.Windows.Forms.Padding(4);
            this.tbx_TimeOut.Name = "tbx_TimeOut";
            this.tbx_TimeOut.Size = new System.Drawing.Size(395, 29);
            this.tbx_TimeOut.TabIndex = 12;
            this.tbx_TimeOut.Text = "5000";
            this.tbx_TimeOut.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.tbx_TimeOut.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.tbx_TimeOut_KeyPress);
            // 
            // btn_ApplyFilter
            // 
            this.btn_ApplyFilter.BackgroundImage = global::RsLib.WatchFolder.Properties.Resources.success;
            this.btn_ApplyFilter.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btn_ApplyFilter.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btn_ApplyFilter.Location = new System.Drawing.Point(535, 42);
            this.btn_ApplyFilter.Margin = new System.Windows.Forms.Padding(4);
            this.btn_ApplyFilter.Name = "btn_ApplyFilter";
            this.btn_ApplyFilter.Size = new System.Drawing.Size(67, 37);
            this.btn_ApplyFilter.TabIndex = 6;
            this.btn_ApplyFilter.UseVisualStyleBackColor = true;
            this.btn_ApplyFilter.Click += new System.EventHandler(this.btn_ApplyFilter_Click);
            // 
            // btn_OpenFolder
            // 
            this.btn_OpenFolder.BackgroundImage = global::RsLib.WatchFolder.Properties.Resources.icons8_opened_folder_64;
            this.btn_OpenFolder.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btn_OpenFolder.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btn_OpenFolder.Location = new System.Drawing.Point(535, 87);
            this.btn_OpenFolder.Margin = new System.Windows.Forms.Padding(4);
            this.btn_OpenFolder.Name = "btn_OpenFolder";
            this.btn_OpenFolder.Size = new System.Drawing.Size(67, 37);
            this.btn_OpenFolder.TabIndex = 7;
            this.btn_OpenFolder.UseVisualStyleBackColor = true;
            this.btn_OpenFolder.Click += new System.EventHandler(this.btn_OpenFolder_Click);
            // 
            // btn_StartMonitor
            // 
            this.btn_StartMonitor.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btn_StartMonitor.Image = global::RsLib.WatchFolder.Properties.Resources.icons8_go_64;
            this.btn_StartMonitor.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btn_StartMonitor.Name = "btn_StartMonitor";
            this.btn_StartMonitor.Size = new System.Drawing.Size(34, 33);
            this.btn_StartMonitor.Text = "Start Monitor Folder";
            this.btn_StartMonitor.Click += new System.EventHandler(this.btn_StartMonitor_Click);
            // 
            // btn_StopMonitor
            // 
            this.btn_StopMonitor.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btn_StopMonitor.Image = global::RsLib.WatchFolder.Properties.Resources.icons8_stop_sign_64;
            this.btn_StopMonitor.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btn_StopMonitor.Name = "btn_StopMonitor";
            this.btn_StopMonitor.Size = new System.Drawing.Size(34, 33);
            this.btn_StopMonitor.Text = "Stop Monitor Folder";
            this.btn_StopMonitor.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btn_StopMonitor.Click += new System.EventHandler(this.btn_StopMonitor_Click);
            // 
            // pictureBox1
            // 
            this.pictureBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pictureBox1.Image = global::RsLib.WatchFolder.Properties.Resources.icons8_warning_64;
            this.pictureBox1.Location = new System.Drawing.Point(501, 83);
            this.pictureBox1.Margin = new System.Windows.Forms.Padding(0);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(30, 45);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox1.TabIndex = 10;
            this.pictureBox1.TabStop = false;
            this.pictureBox1.Visible = false;
            // 
            // btn_ApplyTimeOut
            // 
            this.btn_ApplyTimeOut.BackgroundImage = global::RsLib.WatchFolder.Properties.Resources.success;
            this.btn_ApplyTimeOut.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btn_ApplyTimeOut.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btn_ApplyTimeOut.Location = new System.Drawing.Point(535, 132);
            this.btn_ApplyTimeOut.Margin = new System.Windows.Forms.Padding(4);
            this.btn_ApplyTimeOut.Name = "btn_ApplyTimeOut";
            this.btn_ApplyTimeOut.Size = new System.Drawing.Size(67, 37);
            this.btn_ApplyTimeOut.TabIndex = 13;
            this.btn_ApplyTimeOut.UseVisualStyleBackColor = true;
            this.btn_ApplyTimeOut.Click += new System.EventHandler(this.btn_ApplyTimeOut_Click);
            // 
            // FolderWatchControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tableLayoutPanel1);
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "FolderWatchControl";
            this.Size = new System.Drawing.Size(606, 207);
            this.Load += new System.EventHandler(this.FolderWatchControl_Load);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox tbx_WatchFilter;
        private System.Windows.Forms.Label lbl_WatchedFolder;
        private System.Windows.Forms.Button btn_ApplyFilter;
        private System.Windows.Forms.Button btn_OpenFolder;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton btn_StartMonitor;
        private System.Windows.Forms.ToolStripProgressBar progressBar_RunStatus;
        private System.Windows.Forms.ToolStripButton btn_StopMonitor;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox tbx_TimeOut;
        private System.Windows.Forms.Button btn_ApplyTimeOut;
    }
}

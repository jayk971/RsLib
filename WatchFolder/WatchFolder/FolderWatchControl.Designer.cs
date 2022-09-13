
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FolderWatchControl));
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.label3 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.tbx_WatchFilter = new System.Windows.Forms.TextBox();
            this.lbl_WatchedFolder = new System.Windows.Forms.Label();
            this.btn_ApplyFilter = new System.Windows.Forms.Button();
            this.btn_OpenFolder = new System.Windows.Forms.Button();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.btn_StartMonitor = new System.Windows.Forms.ToolStripButton();
            this.btn_StopMonitor = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.progressBar_RunStatus = new System.Windows.Forms.ToolStripProgressBar();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.tbx_TimeOut = new System.Windows.Forms.TextBox();
            this.btn_ApplyTimeOut = new System.Windows.Forms.Button();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.tableLayoutPanel1.SuspendLayout();
            this.toolStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            resources.ApplyResources(this.tableLayoutPanel1, "tableLayoutPanel1");
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
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.Paint += new System.Windows.Forms.PaintEventHandler(this.tableLayoutPanel1_Paint);
            // 
            // label3
            // 
            resources.ApplyResources(this.label3, "label3");
            this.label3.Name = "label3";
            // 
            // label1
            // 
            resources.ApplyResources(this.label1, "label1");
            this.label1.Name = "label1";
            // 
            // label2
            // 
            resources.ApplyResources(this.label2, "label2");
            this.label2.Name = "label2";
            // 
            // tbx_WatchFilter
            // 
            resources.ApplyResources(this.tbx_WatchFilter, "tbx_WatchFilter");
            this.tbx_WatchFilter.Name = "tbx_WatchFilter";
            // 
            // lbl_WatchedFolder
            // 
            resources.ApplyResources(this.lbl_WatchedFolder, "lbl_WatchedFolder");
            this.lbl_WatchedFolder.Name = "lbl_WatchedFolder";
            // 
            // btn_ApplyFilter
            // 
            this.btn_ApplyFilter.BackgroundImage = global::RsLib.WatchFolder.Properties.Resources.success;
            resources.ApplyResources(this.btn_ApplyFilter, "btn_ApplyFilter");
            this.btn_ApplyFilter.Name = "btn_ApplyFilter";
            this.btn_ApplyFilter.UseVisualStyleBackColor = true;
            this.btn_ApplyFilter.Click += new System.EventHandler(this.btn_ApplyFilter_Click);
            // 
            // btn_OpenFolder
            // 
            this.btn_OpenFolder.BackgroundImage = global::RsLib.WatchFolder.Properties.Resources.icons8_opened_folder_64;
            resources.ApplyResources(this.btn_OpenFolder, "btn_OpenFolder");
            this.btn_OpenFolder.Name = "btn_OpenFolder";
            this.btn_OpenFolder.UseVisualStyleBackColor = true;
            this.btn_OpenFolder.Click += new System.EventHandler(this.btn_OpenFolder_Click);
            // 
            // toolStrip1
            // 
            this.tableLayoutPanel1.SetColumnSpan(this.toolStrip1, 4);
            resources.ApplyResources(this.toolStrip1, "toolStrip1");
            this.toolStrip1.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.toolStrip1.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btn_StartMonitor,
            this.btn_StopMonitor,
            this.toolStripSeparator1,
            this.progressBar_RunStatus});
            this.toolStrip1.Name = "toolStrip1";
            // 
            // btn_StartMonitor
            // 
            this.btn_StartMonitor.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btn_StartMonitor.Image = global::RsLib.WatchFolder.Properties.Resources.icons8_go_64;
            resources.ApplyResources(this.btn_StartMonitor, "btn_StartMonitor");
            this.btn_StartMonitor.Name = "btn_StartMonitor";
            this.btn_StartMonitor.Click += new System.EventHandler(this.btn_StartMonitor_Click);
            // 
            // btn_StopMonitor
            // 
            this.btn_StopMonitor.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btn_StopMonitor.Image = global::RsLib.WatchFolder.Properties.Resources.icons8_stop_sign_64;
            resources.ApplyResources(this.btn_StopMonitor, "btn_StopMonitor");
            this.btn_StopMonitor.Name = "btn_StopMonitor";
            this.btn_StopMonitor.Click += new System.EventHandler(this.btn_StopMonitor_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            resources.ApplyResources(this.toolStripSeparator1, "toolStripSeparator1");
            // 
            // progressBar_RunStatus
            // 
            this.progressBar_RunStatus.Name = "progressBar_RunStatus";
            resources.ApplyResources(this.progressBar_RunStatus, "progressBar_RunStatus");
            // 
            // pictureBox1
            // 
            resources.ApplyResources(this.pictureBox1, "pictureBox1");
            this.pictureBox1.Image = global::RsLib.WatchFolder.Properties.Resources.icons8_warning_64;
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.TabStop = false;
            // 
            // tbx_TimeOut
            // 
            resources.ApplyResources(this.tbx_TimeOut, "tbx_TimeOut");
            this.tbx_TimeOut.Name = "tbx_TimeOut";
            this.tbx_TimeOut.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.tbx_TimeOut_KeyPress);
            // 
            // btn_ApplyTimeOut
            // 
            this.btn_ApplyTimeOut.BackgroundImage = global::RsLib.WatchFolder.Properties.Resources.success;
            resources.ApplyResources(this.btn_ApplyTimeOut, "btn_ApplyTimeOut");
            this.btn_ApplyTimeOut.Name = "btn_ApplyTimeOut";
            this.btn_ApplyTimeOut.UseVisualStyleBackColor = true;
            this.btn_ApplyTimeOut.Click += new System.EventHandler(this.btn_ApplyTimeOut_Click);
            // 
            // timer1
            // 
            this.timer1.Enabled = true;
            this.timer1.Interval = 2000;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // FolderWatchControl
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tableLayoutPanel1);
            this.Name = "FolderWatchControl";
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
        private System.Windows.Forms.Timer timer1;
    }
}

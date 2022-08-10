
namespace RsLib.LogMgr
{
    partial class LogControl
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(LogControl));
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripLabel1 = new System.Windows.Forms.ToolStripLabel();
            this.cmb_LevelFilter = new System.Windows.Forms.ToolStripComboBox();
            this.rtbx_Log = new System.Windows.Forms.RichTextBox();
            this.btn_ClearMsg = new System.Windows.Forms.ToolStripButton();
            this.toolStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // toolStrip1
            // 
            resources.ApplyResources(this.toolStrip1, "toolStrip1");
            this.toolStrip1.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.toolStrip1.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btn_ClearMsg,
            this.toolStripSeparator1,
            this.toolStripLabel1,
            this.cmb_LevelFilter});
            this.toolStrip1.Name = "toolStrip1";
            // 
            // toolStripSeparator1
            // 
            resources.ApplyResources(this.toolStripSeparator1, "toolStripSeparator1");
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            // 
            // toolStripLabel1
            // 
            resources.ApplyResources(this.toolStripLabel1, "toolStripLabel1");
            this.toolStripLabel1.BackColor = System.Drawing.Color.Silver;
            this.toolStripLabel1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.toolStripLabel1.Name = "toolStripLabel1";
            // 
            // cmb_LevelFilter
            // 
            resources.ApplyResources(this.cmb_LevelFilter, "cmb_LevelFilter");
            this.cmb_LevelFilter.BackColor = System.Drawing.Color.PeachPuff;
            this.cmb_LevelFilter.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmb_LevelFilter.Name = "cmb_LevelFilter";
            this.cmb_LevelFilter.Click += new System.EventHandler(this.cmb_LevelFilter_Click);
            // 
            // rtbx_Log
            // 
            resources.ApplyResources(this.rtbx_Log, "rtbx_Log");
            this.rtbx_Log.BackColor = System.Drawing.Color.White;
            this.rtbx_Log.Name = "rtbx_Log";
            this.rtbx_Log.ReadOnly = true;
            // 
            // btn_ClearMsg
            // 
            resources.ApplyResources(this.btn_ClearMsg, "btn_ClearMsg");
            this.btn_ClearMsg.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btn_ClearMsg.Image = global::RsLib.LogMgr.Properties.Resources.icons8_broom_64;
            this.btn_ClearMsg.Name = "btn_ClearMsg";
            this.btn_ClearMsg.Click += new System.EventHandler(this.btn_ClearMsg_Click);
            // 
            // LogControl
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.rtbx_Log);
            this.Controls.Add(this.toolStrip1);
            this.Name = "LogControl";
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.RichTextBox rtbx_Log;
        private System.Windows.Forms.ToolStripButton btn_ClearMsg;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripLabel toolStripLabel1;
        private System.Windows.Forms.ToolStripComboBox cmb_LevelFilter;
    }
}


namespace RsLib.Display
{
    partial class FtDisplay
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
            this.elementHost1 = new System.Windows.Forms.Integration.ElementHost();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabel_X = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabel_Y = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabel_Scale = new System.Windows.Forms.ToolStripStatusLabel();
            this.statusStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // elementHost1
            // 
            this.elementHost1.BackColor = System.Drawing.Color.Gray;
            this.elementHost1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.elementHost1.Location = new System.Drawing.Point(0, 0);
            this.elementHost1.Name = "elementHost1";
            this.elementHost1.Size = new System.Drawing.Size(466, 324);
            this.elementHost1.TabIndex = 0;
            this.elementHost1.Text = "elementHost1";
            this.elementHost1.Child = null;
            // 
            // statusStrip1
            // 
            this.statusStrip1.BackColor = System.Drawing.Color.Black;
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel_X,
            this.toolStripStatusLabel_Y,
            this.toolStripStatusLabel_Scale});
            this.statusStrip1.Location = new System.Drawing.Point(0, 324);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(466, 22);
            this.statusStrip1.TabIndex = 1;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // toolStripStatusLabel_X
            // 
            this.toolStripStatusLabel_X.ForeColor = System.Drawing.Color.Red;
            this.toolStripStatusLabel_X.Name = "toolStripStatusLabel_X";
            this.toolStripStatusLabel_X.Size = new System.Drawing.Size(25, 17);
            this.toolStripStatusLabel_X.Text = "0.0";
            // 
            // toolStripStatusLabel_Y
            // 
            this.toolStripStatusLabel_Y.ForeColor = System.Drawing.Color.Lime;
            this.toolStripStatusLabel_Y.Name = "toolStripStatusLabel_Y";
            this.toolStripStatusLabel_Y.Size = new System.Drawing.Size(25, 17);
            this.toolStripStatusLabel_Y.Text = "0.0";
            // 
            // toolStripStatusLabel_Scale
            // 
            this.toolStripStatusLabel_Scale.ForeColor = System.Drawing.Color.Cyan;
            this.toolStripStatusLabel_Scale.Name = "toolStripStatusLabel_Scale";
            this.toolStripStatusLabel_Scale.Size = new System.Drawing.Size(25, 17);
            this.toolStripStatusLabel_Scale.Text = "0.0";
            // 
            // FtDisplay
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.elementHost1);
            this.Controls.Add(this.statusStrip1);
            this.Name = "FtDisplay";
            this.Size = new System.Drawing.Size(466, 346);
            this.Load += new System.EventHandler(this.FTDisplay_Load);
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Integration.ElementHost elementHost1;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel_X;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel_Y;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel_Scale;
    }
}

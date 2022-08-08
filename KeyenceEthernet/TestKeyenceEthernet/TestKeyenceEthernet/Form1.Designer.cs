
namespace TestKeyenceEthernet
{
    partial class Form1
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

        #region Windows Form 設計工具產生的程式碼

        /// <summary>
        /// 此為設計工具支援所需的方法 - 請勿使用程式碼編輯器修改
        /// 這個方法的內容。
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.x8000Control1 = new X8000TCP.X8000Control();
            this.SuspendLayout();
            // 
            // x8000Control1
            // 
            this.x8000Control1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.x8000Control1.Location = new System.Drawing.Point(0, 0);
            this.x8000Control1.Name = "x8000Control1";
            this.x8000Control1.Size = new System.Drawing.Size(550, 154);
            this.x8000Control1.TabIndex = 0;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(550, 154);
            this.Controls.Add(this.x8000Control1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Form1";
            this.Text = "Keyence Ethernet";
            this.ResumeLayout(false);

        }

        #endregion

        private X8000TCP.X8000Control x8000Control1;
    }
}


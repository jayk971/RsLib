namespace RsLib.Common
{
    partial class ColorGradientControl
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
            this.lbl_50 = new System.Windows.Forms.Label();
            this.lbl_0 = new System.Windows.Forms.Label();
            this.lbl_100 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.tableLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.BackColor = System.Drawing.Color.Black;
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 58.21597F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 41.78403F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.Controls.Add(this.lbl_50, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.lbl_0, 0, 4);
            this.tableLayoutPanel1.Controls.Add(this.lbl_100, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.panel1, 1, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 5;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(86, 149);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // lbl_50
            // 
            this.lbl_50.AutoSize = true;
            this.lbl_50.BackColor = System.Drawing.Color.Black;
            this.lbl_50.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lbl_50.Font = new System.Drawing.Font("Arial", 9F);
            this.lbl_50.ForeColor = System.Drawing.Color.Lime;
            this.lbl_50.Location = new System.Drawing.Point(0, 58);
            this.lbl_50.Margin = new System.Windows.Forms.Padding(0);
            this.lbl_50.Name = "lbl_50";
            this.lbl_50.Size = new System.Drawing.Size(50, 29);
            this.lbl_50.TabIndex = 6;
            this.lbl_50.Text = "50.0";
            this.lbl_50.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lbl_0
            // 
            this.lbl_0.AutoSize = true;
            this.lbl_0.BackColor = System.Drawing.Color.Black;
            this.lbl_0.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lbl_0.Font = new System.Drawing.Font("Arial", 9F);
            this.lbl_0.ForeColor = System.Drawing.Color.Blue;
            this.lbl_0.Location = new System.Drawing.Point(0, 116);
            this.lbl_0.Margin = new System.Windows.Forms.Padding(0);
            this.lbl_0.Name = "lbl_0";
            this.lbl_0.Size = new System.Drawing.Size(50, 33);
            this.lbl_0.TabIndex = 2;
            this.lbl_0.Text = "0.0";
            this.lbl_0.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lbl_100
            // 
            this.lbl_100.AutoSize = true;
            this.lbl_100.BackColor = System.Drawing.Color.Black;
            this.lbl_100.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lbl_100.Font = new System.Drawing.Font("Arial", 9F);
            this.lbl_100.ForeColor = System.Drawing.Color.Red;
            this.lbl_100.Location = new System.Drawing.Point(0, 0);
            this.lbl_100.Margin = new System.Windows.Forms.Padding(0);
            this.lbl_100.Name = "lbl_100";
            this.lbl_100.Size = new System.Drawing.Size(50, 29);
            this.lbl_100.TabIndex = 3;
            this.lbl_100.Text = "100.0";
            this.lbl_100.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.Black;
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(50, 0);
            this.panel1.Margin = new System.Windows.Forms.Padding(0);
            this.panel1.Name = "panel1";
            this.tableLayoutPanel1.SetRowSpan(this.panel1, 5);
            this.panel1.Size = new System.Drawing.Size(36, 149);
            this.panel1.TabIndex = 7;
            // 
            // ColorGradientControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tableLayoutPanel1);
            this.Name = "ColorGradientControl";
            this.Size = new System.Drawing.Size(86, 149);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Label lbl_0;
        private System.Windows.Forms.Label lbl_100;
        private System.Windows.Forms.Label lbl_50;
        private System.Windows.Forms.Panel panel1;
    }
}

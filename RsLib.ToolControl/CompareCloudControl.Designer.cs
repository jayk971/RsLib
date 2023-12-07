namespace RsLib.ToolControl
{
    partial class CompareCloudControl
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
            this.pnl_ComparePlot = new System.Windows.Forms.Panel();
            this.lbl_Similarity = new System.Windows.Forms.Label();
            this.tableLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 116F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.Controls.Add(this.pnl_ComparePlot, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.lbl_Similarity, 1, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 1;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(564, 197);
            this.tableLayoutPanel1.TabIndex = 1;
            // 
            // pnl_ComparePlot
            // 
            this.pnl_ComparePlot.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnl_ComparePlot.Location = new System.Drawing.Point(3, 3);
            this.pnl_ComparePlot.Name = "pnl_ComparePlot";
            this.pnl_ComparePlot.Size = new System.Drawing.Size(442, 191);
            this.pnl_ComparePlot.TabIndex = 1;
            // 
            // lbl_Similarity
            // 
            this.lbl_Similarity.AutoSize = true;
            this.lbl_Similarity.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lbl_Similarity.Font = new System.Drawing.Font("Arial", 26.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_Similarity.Location = new System.Drawing.Point(451, 0);
            this.lbl_Similarity.Name = "lbl_Similarity";
            this.lbl_Similarity.Size = new System.Drawing.Size(110, 197);
            this.lbl_Similarity.TabIndex = 2;
            this.lbl_Similarity.Text = "100.0";
            this.lbl_Similarity.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // CompareCloudControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tableLayoutPanel1);
            this.Name = "CompareCloudControl";
            this.Size = new System.Drawing.Size(564, 197);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Panel pnl_ComparePlot;
        private System.Windows.Forms.Label lbl_Similarity;
    }
}

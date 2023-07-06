
namespace RsLib.PointCloudLib
{
    partial class Point3DControl
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
            this.tbx_Z = new System.Windows.Forms.TextBox();
            this.tbx_Y = new System.Windows.Forms.TextBox();
            this.lbl_PointName = new System.Windows.Forms.Label();
            this.tbx_X = new System.Windows.Forms.TextBox();
            this.tableLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 5;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 105F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 66F));
            this.tableLayoutPanel1.Controls.Add(this.tbx_Z, 3, 0);
            this.tableLayoutPanel1.Controls.Add(this.tbx_Y, 2, 0);
            this.tableLayoutPanel1.Controls.Add(this.lbl_PointName, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.tbx_X, 1, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 1;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(508, 50);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // tbx_Z
            // 
            this.tbx_Z.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tbx_Z.Font = new System.Drawing.Font("新細明體", 12F);
            this.tbx_Z.Location = new System.Drawing.Point(333, 4);
            this.tbx_Z.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.tbx_Z.Name = "tbx_Z";
            this.tbx_Z.Size = new System.Drawing.Size(104, 36);
            this.tbx_Z.TabIndex = 3;
            this.tbx_Z.Text = "0.0";
            this.tbx_Z.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.tbx_Z.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.tbx_Z_KeyPress);
            this.tbx_Z.KeyUp += new System.Windows.Forms.KeyEventHandler(this.tbx_Z_KeyUp);
            // 
            // tbx_Y
            // 
            this.tbx_Y.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tbx_Y.Font = new System.Drawing.Font("新細明體", 12F);
            this.tbx_Y.Location = new System.Drawing.Point(221, 4);
            this.tbx_Y.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.tbx_Y.Name = "tbx_Y";
            this.tbx_Y.Size = new System.Drawing.Size(104, 36);
            this.tbx_Y.TabIndex = 2;
            this.tbx_Y.Text = "0.0";
            this.tbx_Y.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.tbx_Y.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.tbx_Y_KeyPress);
            this.tbx_Y.KeyUp += new System.Windows.Forms.KeyEventHandler(this.tbx_Y_KeyUp);
            // 
            // lbl_PointName
            // 
            this.lbl_PointName.AutoSize = true;
            this.lbl_PointName.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lbl_PointName.Location = new System.Drawing.Point(4, 0);
            this.lbl_PointName.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lbl_PointName.Name = "lbl_PointName";
            this.lbl_PointName.Size = new System.Drawing.Size(97, 50);
            this.lbl_PointName.TabIndex = 0;
            this.lbl_PointName.Text = "Name";
            this.lbl_PointName.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // tbx_X
            // 
            this.tbx_X.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tbx_X.Font = new System.Drawing.Font("新細明體", 12F);
            this.tbx_X.Location = new System.Drawing.Point(109, 4);
            this.tbx_X.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.tbx_X.Name = "tbx_X";
            this.tbx_X.Size = new System.Drawing.Size(104, 36);
            this.tbx_X.TabIndex = 1;
            this.tbx_X.Text = "0.0";
            this.tbx_X.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.tbx_X.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.tbx_X_KeyPress);
            this.tbx_X.KeyUp += new System.Windows.Forms.KeyEventHandler(this.tbx_X_KeyUp);
            // 
            // Point3DControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tableLayoutPanel1);
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.Name = "Point3DControl";
            this.Size = new System.Drawing.Size(508, 50);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.TextBox tbx_Z;
        private System.Windows.Forms.TextBox tbx_Y;
        private System.Windows.Forms.Label lbl_PointName;
        private System.Windows.Forms.TextBox tbx_X;
    }
}

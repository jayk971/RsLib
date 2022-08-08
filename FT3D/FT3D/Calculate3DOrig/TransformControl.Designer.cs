
namespace RsLib.PointCloud.CalculateMatrix
{
    partial class TransformControl
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
            this.lbl_XYZFilePath = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.lbl_M44FilePath = new System.Windows.Forms.Label();
            this.btn_OpenXYZ = new System.Windows.Forms.Button();
            this.btn_openM44 = new System.Windows.Forms.Button();
            this.btn_Calculate = new System.Windows.Forms.Button();
            this.tableLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 4;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 150F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 524F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 106F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.lbl_XYZFilePath, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.label1, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.label2, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.lbl_M44FilePath, 1, 1);
            this.tableLayoutPanel1.Controls.Add(this.btn_OpenXYZ, 2, 0);
            this.tableLayoutPanel1.Controls.Add(this.btn_openM44, 2, 1);
            this.tableLayoutPanel1.Controls.Add(this.btn_Calculate, 0, 2);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 4;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 52F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 52F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 110F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 12F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(837, 248);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // lbl_XYZFilePath
            // 
            this.lbl_XYZFilePath.AutoSize = true;
            this.lbl_XYZFilePath.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lbl_XYZFilePath.Location = new System.Drawing.Point(154, 0);
            this.lbl_XYZFilePath.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lbl_XYZFilePath.Name = "lbl_XYZFilePath";
            this.lbl_XYZFilePath.Size = new System.Drawing.Size(516, 52);
            this.lbl_XYZFilePath.TabIndex = 2;
            this.lbl_XYZFilePath.Text = "--";
            this.lbl_XYZFilePath.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label1.Location = new System.Drawing.Point(4, 0);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(142, 52);
            this.label1.TabIndex = 0;
            this.label1.Text = "XYZ File";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label2.Location = new System.Drawing.Point(4, 52);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(142, 52);
            this.label2.TabIndex = 1;
            this.label2.Text = "Matrix File";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lbl_M44FilePath
            // 
            this.lbl_M44FilePath.AutoSize = true;
            this.lbl_M44FilePath.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lbl_M44FilePath.Location = new System.Drawing.Point(154, 52);
            this.lbl_M44FilePath.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lbl_M44FilePath.Name = "lbl_M44FilePath";
            this.lbl_M44FilePath.Size = new System.Drawing.Size(516, 52);
            this.lbl_M44FilePath.TabIndex = 3;
            this.lbl_M44FilePath.Text = "--";
            this.lbl_M44FilePath.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // btn_OpenXYZ
            // 
            this.btn_OpenXYZ.BackgroundImage = global::RsLib.PointCloud.CalculateMatrix.Properties.Resources.icons8_opened_folder_64;
            this.btn_OpenXYZ.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btn_OpenXYZ.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btn_OpenXYZ.Location = new System.Drawing.Point(678, 4);
            this.btn_OpenXYZ.Margin = new System.Windows.Forms.Padding(4);
            this.btn_OpenXYZ.Name = "btn_OpenXYZ";
            this.btn_OpenXYZ.Size = new System.Drawing.Size(98, 44);
            this.btn_OpenXYZ.TabIndex = 4;
            this.btn_OpenXYZ.UseVisualStyleBackColor = true;
            this.btn_OpenXYZ.Click += new System.EventHandler(this.btn_OpenXYZ_Click);
            // 
            // btn_openM44
            // 
            this.btn_openM44.BackgroundImage = global::RsLib.PointCloud.CalculateMatrix.Properties.Resources.icons8_opened_folder_64;
            this.btn_openM44.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btn_openM44.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btn_openM44.Location = new System.Drawing.Point(678, 56);
            this.btn_openM44.Margin = new System.Windows.Forms.Padding(4);
            this.btn_openM44.Name = "btn_openM44";
            this.btn_openM44.Size = new System.Drawing.Size(98, 44);
            this.btn_openM44.TabIndex = 5;
            this.btn_openM44.UseVisualStyleBackColor = true;
            this.btn_openM44.Click += new System.EventHandler(this.btn_openM44_Click);
            // 
            // btn_Calculate
            // 
            this.btn_Calculate.BackgroundImage = global::RsLib.PointCloud.CalculateMatrix.Properties.Resources.icons8_math_64;
            this.btn_Calculate.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.tableLayoutPanel1.SetColumnSpan(this.btn_Calculate, 3);
            this.btn_Calculate.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btn_Calculate.Location = new System.Drawing.Point(4, 108);
            this.btn_Calculate.Margin = new System.Windows.Forms.Padding(4);
            this.btn_Calculate.Name = "btn_Calculate";
            this.btn_Calculate.Size = new System.Drawing.Size(772, 102);
            this.btn_Calculate.TabIndex = 6;
            this.btn_Calculate.UseVisualStyleBackColor = true;
            this.btn_Calculate.Click += new System.EventHandler(this.btn_Calculate_Click);
            // 
            // TransformControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tableLayoutPanel1);
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.Name = "TransformControl";
            this.Size = new System.Drawing.Size(837, 248);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Label lbl_XYZFilePath;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label lbl_M44FilePath;
        private System.Windows.Forms.Button btn_OpenXYZ;
        private System.Windows.Forms.Button btn_openM44;
        private System.Windows.Forms.Button btn_Calculate;
    }
}

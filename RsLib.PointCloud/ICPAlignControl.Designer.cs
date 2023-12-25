namespace RsLib.PointCloudLib
{
    partial class ICPAlignControl
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
            this.linkLbl_ToBeAligned = new System.Windows.Forms.LinkLabel();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.linkLbl_Model = new System.Windows.Forms.LinkLabel();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayoutPanel3 = new System.Windows.Forms.TableLayoutPanel();
            this.lbl_RMS = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.lbl_Fitness = new System.Windows.Forms.Label();
            this.btn_Align = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.btn_SaveMatrix = new System.Windows.Forms.Button();
            this.btn_SaveAligned = new System.Windows.Forms.Button();
            this.tableLayoutPanel4 = new System.Windows.Forms.TableLayoutPanel();
            this.propertyGrid1 = new System.Windows.Forms.PropertyGrid();
            this.richTextBox1 = new System.Windows.Forms.RichTextBox();
            this.tableLayoutPanel1.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            this.tableLayoutPanel3.SuspendLayout();
            this.tableLayoutPanel4.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 125F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.linkLbl_ToBeAligned, 1, 1);
            this.tableLayoutPanel1.Controls.Add(this.label1, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.label2, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.linkLbl_Model, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.tableLayoutPanel2, 0, 2);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 3;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 35F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 35F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(485, 436);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // linkLbl_ToBeAligned
            // 
            this.linkLbl_ToBeAligned.AutoSize = true;
            this.linkLbl_ToBeAligned.Dock = System.Windows.Forms.DockStyle.Fill;
            this.linkLbl_ToBeAligned.Location = new System.Drawing.Point(128, 35);
            this.linkLbl_ToBeAligned.Name = "linkLbl_ToBeAligned";
            this.linkLbl_ToBeAligned.Size = new System.Drawing.Size(354, 35);
            this.linkLbl_ToBeAligned.TabIndex = 3;
            this.linkLbl_ToBeAligned.TabStop = true;
            this.linkLbl_ToBeAligned.Text = "--";
            this.linkLbl_ToBeAligned.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.linkLbl_ToBeAligned.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLbl_ToBeAligned_LinkClicked);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label1.Location = new System.Drawing.Point(3, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(119, 35);
            this.label1.TabIndex = 0;
            this.label1.Text = "Model";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label2.Location = new System.Drawing.Point(3, 35);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(119, 35);
            this.label2.TabIndex = 1;
            this.label2.Text = "To be aligned";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // linkLbl_Model
            // 
            this.linkLbl_Model.AutoSize = true;
            this.linkLbl_Model.Dock = System.Windows.Forms.DockStyle.Fill;
            this.linkLbl_Model.Location = new System.Drawing.Point(128, 0);
            this.linkLbl_Model.Name = "linkLbl_Model";
            this.linkLbl_Model.Size = new System.Drawing.Size(354, 35);
            this.linkLbl_Model.TabIndex = 2;
            this.linkLbl_Model.TabStop = true;
            this.linkLbl_Model.Text = "--";
            this.linkLbl_Model.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.linkLbl_Model.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLbl_Model_LinkClicked);
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.ColumnCount = 2;
            this.tableLayoutPanel1.SetColumnSpan(this.tableLayoutPanel2, 2);
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 67.01461F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 32.98539F));
            this.tableLayoutPanel2.Controls.Add(this.tableLayoutPanel3, 1, 0);
            this.tableLayoutPanel2.Controls.Add(this.tableLayoutPanel4, 0, 0);
            this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel2.Location = new System.Drawing.Point(3, 73);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 1;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(479, 360);
            this.tableLayoutPanel2.TabIndex = 4;
            // 
            // tableLayoutPanel3
            // 
            this.tableLayoutPanel3.ColumnCount = 2;
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 60F));
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel3.Controls.Add(this.lbl_RMS, 1, 2);
            this.tableLayoutPanel3.Controls.Add(this.label5, 0, 2);
            this.tableLayoutPanel3.Controls.Add(this.lbl_Fitness, 1, 1);
            this.tableLayoutPanel3.Controls.Add(this.btn_Align, 0, 0);
            this.tableLayoutPanel3.Controls.Add(this.label3, 0, 1);
            this.tableLayoutPanel3.Controls.Add(this.btn_SaveMatrix, 0, 4);
            this.tableLayoutPanel3.Controls.Add(this.btn_SaveAligned, 0, 5);
            this.tableLayoutPanel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel3.Location = new System.Drawing.Point(323, 3);
            this.tableLayoutPanel3.Name = "tableLayoutPanel3";
            this.tableLayoutPanel3.RowCount = 6;
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 35F));
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 35F));
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 35F));
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 35F));
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 35F));
            this.tableLayoutPanel3.Size = new System.Drawing.Size(153, 354);
            this.tableLayoutPanel3.TabIndex = 1;
            // 
            // lbl_RMS
            // 
            this.lbl_RMS.AutoSize = true;
            this.lbl_RMS.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lbl_RMS.Location = new System.Drawing.Point(63, 70);
            this.lbl_RMS.Name = "lbl_RMS";
            this.lbl_RMS.Size = new System.Drawing.Size(87, 35);
            this.lbl_RMS.TabIndex = 4;
            this.lbl_RMS.Text = "0.0";
            this.lbl_RMS.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label5.Location = new System.Drawing.Point(3, 70);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(54, 35);
            this.label5.TabIndex = 3;
            this.label5.Text = "RMS";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lbl_Fitness
            // 
            this.lbl_Fitness.AutoSize = true;
            this.lbl_Fitness.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lbl_Fitness.Location = new System.Drawing.Point(63, 35);
            this.lbl_Fitness.Name = "lbl_Fitness";
            this.lbl_Fitness.Size = new System.Drawing.Size(87, 35);
            this.lbl_Fitness.TabIndex = 2;
            this.lbl_Fitness.Text = "0.0";
            this.lbl_Fitness.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // btn_Align
            // 
            this.tableLayoutPanel3.SetColumnSpan(this.btn_Align, 2);
            this.btn_Align.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btn_Align.Location = new System.Drawing.Point(3, 3);
            this.btn_Align.Name = "btn_Align";
            this.btn_Align.Size = new System.Drawing.Size(147, 29);
            this.btn_Align.TabIndex = 0;
            this.btn_Align.Text = "Align";
            this.btn_Align.UseVisualStyleBackColor = true;
            this.btn_Align.Click += new System.EventHandler(this.btn_Align_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label3.Location = new System.Drawing.Point(3, 35);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(54, 35);
            this.label3.TabIndex = 1;
            this.label3.Text = "Fitness";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // btn_SaveMatrix
            // 
            this.tableLayoutPanel3.SetColumnSpan(this.btn_SaveMatrix, 2);
            this.btn_SaveMatrix.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btn_SaveMatrix.Location = new System.Drawing.Point(3, 287);
            this.btn_SaveMatrix.Name = "btn_SaveMatrix";
            this.btn_SaveMatrix.Size = new System.Drawing.Size(147, 29);
            this.btn_SaveMatrix.TabIndex = 5;
            this.btn_SaveMatrix.Text = "Save Matrix";
            this.btn_SaveMatrix.UseVisualStyleBackColor = true;
            this.btn_SaveMatrix.Click += new System.EventHandler(this.btn_SaveMatrix_Click);
            // 
            // btn_SaveAligned
            // 
            this.tableLayoutPanel3.SetColumnSpan(this.btn_SaveAligned, 2);
            this.btn_SaveAligned.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btn_SaveAligned.Location = new System.Drawing.Point(3, 322);
            this.btn_SaveAligned.Name = "btn_SaveAligned";
            this.btn_SaveAligned.Size = new System.Drawing.Size(147, 29);
            this.btn_SaveAligned.TabIndex = 6;
            this.btn_SaveAligned.Text = "Save Aligned Cloud";
            this.btn_SaveAligned.UseVisualStyleBackColor = true;
            this.btn_SaveAligned.Click += new System.EventHandler(this.btn_SaveAligned_Click);
            // 
            // tableLayoutPanel4
            // 
            this.tableLayoutPanel4.ColumnCount = 1;
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel4.Controls.Add(this.propertyGrid1, 0, 0);
            this.tableLayoutPanel4.Controls.Add(this.richTextBox1, 0, 1);
            this.tableLayoutPanel4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel4.Location = new System.Drawing.Point(3, 3);
            this.tableLayoutPanel4.Name = "tableLayoutPanel4";
            this.tableLayoutPanel4.RowCount = 2;
            this.tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 61.58192F));
            this.tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 38.41808F));
            this.tableLayoutPanel4.Size = new System.Drawing.Size(314, 354);
            this.tableLayoutPanel4.TabIndex = 2;
            // 
            // propertyGrid1
            // 
            this.propertyGrid1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.propertyGrid1.HelpVisible = false;
            this.propertyGrid1.Location = new System.Drawing.Point(3, 3);
            this.propertyGrid1.Name = "propertyGrid1";
            this.propertyGrid1.Size = new System.Drawing.Size(308, 212);
            this.propertyGrid1.TabIndex = 0;
            this.propertyGrid1.ToolbarVisible = false;
            // 
            // richTextBox1
            // 
            this.richTextBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.richTextBox1.Location = new System.Drawing.Point(3, 221);
            this.richTextBox1.Name = "richTextBox1";
            this.richTextBox1.ReadOnly = true;
            this.richTextBox1.Size = new System.Drawing.Size(308, 130);
            this.richTextBox1.TabIndex = 1;
            this.richTextBox1.Text = "";
            // 
            // ICPAlignControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tableLayoutPanel1);
            this.Name = "ICPAlignControl";
            this.Size = new System.Drawing.Size(485, 436);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.tableLayoutPanel2.ResumeLayout(false);
            this.tableLayoutPanel3.ResumeLayout(false);
            this.tableLayoutPanel3.PerformLayout();
            this.tableLayoutPanel4.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.LinkLabel linkLbl_ToBeAligned;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.LinkLabel linkLbl_Model;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private System.Windows.Forms.PropertyGrid propertyGrid1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel3;
        private System.Windows.Forms.Button btn_Align;
        private System.Windows.Forms.Label lbl_RMS;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label lbl_Fitness;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button btn_SaveMatrix;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel4;
        private System.Windows.Forms.RichTextBox richTextBox1;
        private System.Windows.Forms.Button btn_SaveAligned;
    }
}

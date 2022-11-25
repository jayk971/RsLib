
namespace RsLib.PointCloud.CalculateMatrix
{
    partial class CalculateMatrixControl
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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.table_ImageBase = new System.Windows.Forms.TableLayoutPanel();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.table_RobotBase = new System.Windows.Forms.TableLayoutPanel();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.richTextBox1 = new System.Windows.Forms.RichTextBox();
            this.gbx_Test = new System.Windows.Forms.GroupBox();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.btn_LoadMatrix = new System.Windows.Forms.ToolStripButton();
            this.btn_CalMatrix = new System.Windows.Forms.ToolStripButton();
            this.btn_ClearData = new System.Windows.Forms.ToolStripButton();
            this.tableLayoutPanel1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.table_ImageBase.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.table_RobotBase.SuspendLayout();
            this.toolStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Controls.Add(this.groupBox1, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.groupBox2, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.toolStrip1, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.richTextBox1, 0, 3);
            this.tableLayoutPanel1.Controls.Add(this.gbx_Test, 1, 3);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 4;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 200F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 200F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 50F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(500, 533);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // groupBox1
            // 
            this.tableLayoutPanel1.SetColumnSpan(this.groupBox1, 2);
            this.groupBox1.Controls.Add(this.table_ImageBase);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox1.Location = new System.Drawing.Point(3, 33);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(494, 194);
            this.groupBox1.TabIndex = 3;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Image Base";
            // 
            // table_ImageBase
            // 
            this.table_ImageBase.ColumnCount = 2;
            this.table_ImageBase.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 250F));
            this.table_ImageBase.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.table_ImageBase.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.table_ImageBase.Controls.Add(this.pictureBox2, 0, 0);
            this.table_ImageBase.Dock = System.Windows.Forms.DockStyle.Fill;
            this.table_ImageBase.Location = new System.Drawing.Point(3, 18);
            this.table_ImageBase.Margin = new System.Windows.Forms.Padding(0);
            this.table_ImageBase.Name = "table_ImageBase";
            this.table_ImageBase.RowCount = 4;
            this.table_ImageBase.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.table_ImageBase.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.table_ImageBase.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.table_ImageBase.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.table_ImageBase.Size = new System.Drawing.Size(488, 173);
            this.table_ImageBase.TabIndex = 0;
            // 
            // groupBox2
            // 
            this.tableLayoutPanel1.SetColumnSpan(this.groupBox2, 2);
            this.groupBox2.Controls.Add(this.table_RobotBase);
            this.groupBox2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox2.Location = new System.Drawing.Point(3, 233);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(494, 194);
            this.groupBox2.TabIndex = 4;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Robot base";
            // 
            // table_RobotBase
            // 
            this.table_RobotBase.ColumnCount = 2;
            this.table_RobotBase.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 250F));
            this.table_RobotBase.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.table_RobotBase.Controls.Add(this.pictureBox1, 0, 0);
            this.table_RobotBase.Dock = System.Windows.Forms.DockStyle.Fill;
            this.table_RobotBase.Location = new System.Drawing.Point(3, 18);
            this.table_RobotBase.Margin = new System.Windows.Forms.Padding(0);
            this.table_RobotBase.Name = "table_RobotBase";
            this.table_RobotBase.RowCount = 4;
            this.table_RobotBase.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.table_RobotBase.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.table_RobotBase.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.table_RobotBase.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.table_RobotBase.Size = new System.Drawing.Size(488, 173);
            this.table_RobotBase.TabIndex = 1;
            // 
            // toolStrip1
            // 
            this.tableLayoutPanel1.SetColumnSpan(this.toolStrip1, 2);
            this.toolStrip1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.toolStrip1.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btn_LoadMatrix,
            this.btn_CalMatrix,
            this.btn_ClearData});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(500, 30);
            this.toolStrip1.TabIndex = 5;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // richTextBox1
            // 
            this.richTextBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.richTextBox1.Location = new System.Drawing.Point(0, 430);
            this.richTextBox1.Margin = new System.Windows.Forms.Padding(0);
            this.richTextBox1.Name = "richTextBox1";
            this.richTextBox1.ReadOnly = true;
            this.richTextBox1.Size = new System.Drawing.Size(250, 103);
            this.richTextBox1.TabIndex = 2;
            this.richTextBox1.Text = "";
            // 
            // gbx_Test
            // 
            this.gbx_Test.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gbx_Test.Location = new System.Drawing.Point(253, 433);
            this.gbx_Test.Name = "gbx_Test";
            this.gbx_Test.Size = new System.Drawing.Size(244, 97);
            this.gbx_Test.TabIndex = 6;
            this.gbx_Test.TabStop = false;
            this.gbx_Test.Text = "Test";
            // 
            // pictureBox2
            // 
            this.pictureBox2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pictureBox2.Image = global::RsLib.PointCloud.CalculateMatrix.Properties.Resources.ImageBase;
            this.pictureBox2.Location = new System.Drawing.Point(3, 3);
            this.pictureBox2.Name = "pictureBox2";
            this.table_ImageBase.SetRowSpan(this.pictureBox2, 4);
            this.pictureBox2.Size = new System.Drawing.Size(244, 167);
            this.pictureBox2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox2.TabIndex = 0;
            this.pictureBox2.TabStop = false;
            // 
            // pictureBox1
            // 
            this.pictureBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pictureBox1.Image = global::RsLib.PointCloud.CalculateMatrix.Properties.Resources.RobotBase;
            this.pictureBox1.Location = new System.Drawing.Point(3, 3);
            this.pictureBox1.Name = "pictureBox1";
            this.table_RobotBase.SetRowSpan(this.pictureBox1, 4);
            this.pictureBox1.Size = new System.Drawing.Size(244, 167);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            // 
            // btn_LoadMatrix
            // 
            this.btn_LoadMatrix.BackgroundImage = global::RsLib.PointCloud.CalculateMatrix.Properties.Resources.icons8_opened_folder_1_64;
            this.btn_LoadMatrix.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btn_LoadMatrix.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btn_LoadMatrix.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btn_LoadMatrix.Name = "btn_LoadMatrix";
            this.btn_LoadMatrix.Size = new System.Drawing.Size(23, 27);
            this.btn_LoadMatrix.Text = "Load Matrix";
            this.btn_LoadMatrix.Click += new System.EventHandler(this.btn_LoadMatrix_Click);
            // 
            // btn_CalMatrix
            // 
            this.btn_CalMatrix.BackgroundImage = global::RsLib.PointCloud.CalculateMatrix.Properties.Resources.icons8_save_64;
            this.btn_CalMatrix.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btn_CalMatrix.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btn_CalMatrix.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btn_CalMatrix.Name = "btn_CalMatrix";
            this.btn_CalMatrix.Size = new System.Drawing.Size(23, 27);
            this.btn_CalMatrix.Text = "Calculate Matrix";
            this.btn_CalMatrix.Click += new System.EventHandler(this.btn_CalculateMatrix_Click);
            // 
            // btn_ClearData
            // 
            this.btn_ClearData.BackgroundImage = global::RsLib.PointCloud.CalculateMatrix.Properties.Resources.icons8_broom_64;
            this.btn_ClearData.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btn_ClearData.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btn_ClearData.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btn_ClearData.Name = "btn_ClearData";
            this.btn_ClearData.Size = new System.Drawing.Size(23, 27);
            this.btn_ClearData.Text = "Clear";
            this.btn_ClearData.Click += new System.EventHandler(this.btn_ClearData_Click);
            // 
            // CalculateMatrixControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tableLayoutPanel1);
            this.Name = "CalculateMatrixControl";
            this.Size = new System.Drawing.Size(500, 533);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.table_ImageBase.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.table_RobotBase.ResumeLayout(false);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.TableLayoutPanel table_ImageBase;
        private System.Windows.Forms.RichTextBox richTextBox1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.TableLayoutPanel table_RobotBase;
        private System.Windows.Forms.PictureBox pictureBox2;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton btn_LoadMatrix;
        private System.Windows.Forms.ToolStripButton btn_CalMatrix;
        private System.Windows.Forms.ToolStripButton btn_ClearData;
        private System.Windows.Forms.GroupBox gbx_Test;
    }
}

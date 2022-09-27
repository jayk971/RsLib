
namespace TestForm
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
            this.button1 = new System.Windows.Forms.Button();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.lbl_RealX = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.lbl_RealY = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.lbl_RealZ = new System.Windows.Forms.Label();
            this.tbx_PxX = new System.Windows.Forms.TextBox();
            this.tbx_PxY = new System.Windows.Forms.TextBox();
            this.btn_CalPixel = new System.Windows.Forms.Button();
            this.tableLayoutPanel1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.BackgroundImage = global::TestForm.Properties.Resources.icons8_bmp_64;
            this.button1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.button1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.button1.Location = new System.Drawing.Point(3, 3);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(215, 147);
            this.button1.TabIndex = 0;
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 3;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanel1.Controls.Add(this.button1, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.groupBox1, 0, 1);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(663, 306);
            this.tableLayoutPanel1.TabIndex = 1;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.tableLayoutPanel2);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox1.Location = new System.Drawing.Point(3, 156);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(215, 147);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Get Pixel Height Data";
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.ColumnCount = 3;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 50F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel2.Controls.Add(this.label1, 0, 0);
            this.tableLayoutPanel2.Controls.Add(this.label2, 0, 1);
            this.tableLayoutPanel2.Controls.Add(this.label3, 0, 2);
            this.tableLayoutPanel2.Controls.Add(this.lbl_RealX, 1, 2);
            this.tableLayoutPanel2.Controls.Add(this.label5, 0, 3);
            this.tableLayoutPanel2.Controls.Add(this.lbl_RealY, 1, 3);
            this.tableLayoutPanel2.Controls.Add(this.label7, 0, 4);
            this.tableLayoutPanel2.Controls.Add(this.lbl_RealZ, 1, 4);
            this.tableLayoutPanel2.Controls.Add(this.tbx_PxX, 1, 0);
            this.tableLayoutPanel2.Controls.Add(this.tbx_PxY, 1, 1);
            this.tableLayoutPanel2.Controls.Add(this.btn_CalPixel, 2, 0);
            this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel2.Location = new System.Drawing.Point(3, 18);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 5;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(209, 126);
            this.tableLayoutPanel2.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label1.Location = new System.Drawing.Point(3, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(44, 25);
            this.label1.TabIndex = 0;
            this.label1.Text = "Pixel X";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label2.Location = new System.Drawing.Point(3, 25);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(44, 25);
            this.label2.TabIndex = 1;
            this.label2.Text = "Pixel Y";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label3.Location = new System.Drawing.Point(3, 50);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(44, 25);
            this.label3.TabIndex = 2;
            this.label3.Text = "Real X";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lbl_RealX
            // 
            this.lbl_RealX.AutoSize = true;
            this.lbl_RealX.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lbl_RealX.Location = new System.Drawing.Point(53, 50);
            this.lbl_RealX.Name = "lbl_RealX";
            this.lbl_RealX.Size = new System.Drawing.Size(73, 25);
            this.lbl_RealX.TabIndex = 3;
            this.lbl_RealX.Text = "0.0";
            this.lbl_RealX.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label5.Location = new System.Drawing.Point(3, 75);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(44, 25);
            this.label5.TabIndex = 4;
            this.label5.Text = "Real Y";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lbl_RealY
            // 
            this.lbl_RealY.AutoSize = true;
            this.lbl_RealY.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lbl_RealY.Location = new System.Drawing.Point(53, 75);
            this.lbl_RealY.Name = "lbl_RealY";
            this.lbl_RealY.Size = new System.Drawing.Size(73, 25);
            this.lbl_RealY.TabIndex = 5;
            this.lbl_RealY.Text = "0.0";
            this.lbl_RealY.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label7.Location = new System.Drawing.Point(3, 100);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(44, 26);
            this.label7.TabIndex = 6;
            this.label7.Text = "Real Z";
            this.label7.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lbl_RealZ
            // 
            this.lbl_RealZ.AutoSize = true;
            this.lbl_RealZ.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lbl_RealZ.Location = new System.Drawing.Point(53, 100);
            this.lbl_RealZ.Name = "lbl_RealZ";
            this.lbl_RealZ.Size = new System.Drawing.Size(73, 26);
            this.lbl_RealZ.TabIndex = 7;
            this.lbl_RealZ.Text = "0.0";
            this.lbl_RealZ.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // tbx_PxX
            // 
            this.tbx_PxX.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tbx_PxX.Location = new System.Drawing.Point(53, 3);
            this.tbx_PxX.Name = "tbx_PxX";
            this.tbx_PxX.Size = new System.Drawing.Size(73, 22);
            this.tbx_PxX.TabIndex = 8;
            this.tbx_PxX.Text = "0";
            this.tbx_PxX.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.tbx_PxX.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.tbx_PxX_KeyPress);
            // 
            // tbx_PxY
            // 
            this.tbx_PxY.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tbx_PxY.Location = new System.Drawing.Point(53, 28);
            this.tbx_PxY.Name = "tbx_PxY";
            this.tbx_PxY.Size = new System.Drawing.Size(73, 22);
            this.tbx_PxY.TabIndex = 9;
            this.tbx_PxY.Text = "0";
            this.tbx_PxY.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.tbx_PxY.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.tbx_PxY_KeyPress);
            // 
            // btn_CalPixel
            // 
            this.btn_CalPixel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btn_CalPixel.Location = new System.Drawing.Point(132, 3);
            this.btn_CalPixel.Name = "btn_CalPixel";
            this.tableLayoutPanel2.SetRowSpan(this.btn_CalPixel, 2);
            this.btn_CalPixel.Size = new System.Drawing.Size(74, 44);
            this.btn_CalPixel.TabIndex = 10;
            this.btn_CalPixel.Text = "Get Pixel Data";
            this.btn_CalPixel.UseVisualStyleBackColor = true;
            this.btn_CalPixel.Click += new System.EventHandler(this.btn_CalPixel_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(663, 306);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Form1";
            this.Text = "Convert BMP To XYZ";
            this.tableLayoutPanel1.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.tableLayoutPanel2.ResumeLayout(false);
            this.tableLayoutPanel2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label lbl_RealX;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label lbl_RealY;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label lbl_RealZ;
        private System.Windows.Forms.TextBox tbx_PxX;
        private System.Windows.Forms.TextBox tbx_PxY;
        private System.Windows.Forms.Button btn_CalPixel;
    }
}


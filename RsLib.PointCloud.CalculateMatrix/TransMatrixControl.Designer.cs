namespace RsLib.PointCloudLib.CalculateMatrix
{
    partial class TransMatrixControl
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
            this.btn_ResetEuler = new System.Windows.Forms.Button();
            this.tbx_RX = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.tbx_RY = new System.Windows.Forms.TextBox();
            this.tbx_RZ = new System.Windows.Forms.TextBox();
            this.tbx_SY = new System.Windows.Forms.TextBox();
            this.tbx_SZ = new System.Windows.Forms.TextBox();
            this.tbx_SX = new System.Windows.Forms.TextBox();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayoutPanel3 = new System.Windows.Forms.TableLayoutPanel();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.tbx_M00 = new System.Windows.Forms.TextBox();
            this.tbx_M10 = new System.Windows.Forms.TextBox();
            this.tbx_M20 = new System.Windows.Forms.TextBox();
            this.tbx_M30 = new System.Windows.Forms.TextBox();
            this.tbx_M01 = new System.Windows.Forms.TextBox();
            this.tbx_M11 = new System.Windows.Forms.TextBox();
            this.tbx_M21 = new System.Windows.Forms.TextBox();
            this.tbx_M31 = new System.Windows.Forms.TextBox();
            this.tbx_M02 = new System.Windows.Forms.TextBox();
            this.tbx_M12 = new System.Windows.Forms.TextBox();
            this.tbx_M22 = new System.Windows.Forms.TextBox();
            this.tbx_M32 = new System.Windows.Forms.TextBox();
            this.tbx_M03 = new System.Windows.Forms.TextBox();
            this.tbx_M13 = new System.Windows.Forms.TextBox();
            this.tbx_M23 = new System.Windows.Forms.TextBox();
            this.tbx_M33 = new System.Windows.Forms.TextBox();
            this.lbl_Q0 = new System.Windows.Forms.Label();
            this.lbl_Q1 = new System.Windows.Forms.Label();
            this.lbl_Q2 = new System.Windows.Forms.Label();
            this.btn_ResetIdentity = new System.Windows.Forms.Button();
            this.lbl_Q3 = new System.Windows.Forms.Label();
            this.btn_EulerToMatrix = new System.Windows.Forms.Button();
            this.btn_MatrixToEuler = new System.Windows.Forms.Button();
            this.btn_SaveMatrix = new System.Windows.Forms.Button();
            this.btn_LoadEulerData = new System.Windows.Forms.Button();
            this.tableLayoutPanel1.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            this.tableLayoutPanel3.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 4;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 50F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanel1.Controls.Add(this.btn_ResetEuler, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.tbx_RX, 1, 2);
            this.tableLayoutPanel1.Controls.Add(this.label9, 1, 1);
            this.tableLayoutPanel1.Controls.Add(this.label10, 2, 1);
            this.tableLayoutPanel1.Controls.Add(this.label11, 3, 1);
            this.tableLayoutPanel1.Controls.Add(this.label12, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.label13, 0, 3);
            this.tableLayoutPanel1.Controls.Add(this.tbx_RY, 2, 2);
            this.tableLayoutPanel1.Controls.Add(this.tbx_RZ, 3, 2);
            this.tableLayoutPanel1.Controls.Add(this.tbx_SY, 2, 3);
            this.tableLayoutPanel1.Controls.Add(this.tbx_SZ, 3, 3);
            this.tableLayoutPanel1.Controls.Add(this.tbx_SX, 1, 3);
            this.tableLayoutPanel1.Controls.Add(this.btn_LoadEulerData, 2, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(3, 3);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 5;
            this.tableLayoutPanel2.SetRowSpan(this.tableLayoutPanel1, 4);
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 35F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 35F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 35F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(248, 338);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // btn_ResetEuler
            // 
            this.btn_ResetEuler.Location = new System.Drawing.Point(53, 3);
            this.btn_ResetEuler.Name = "btn_ResetEuler";
            this.btn_ResetEuler.Size = new System.Drawing.Size(46, 23);
            this.btn_ResetEuler.TabIndex = 29;
            this.btn_ResetEuler.Text = "Reset";
            this.btn_ResetEuler.UseVisualStyleBackColor = true;
            this.btn_ResetEuler.Click += new System.EventHandler(this.btn_ResetEuler_Click);
            // 
            // tbx_RX
            // 
            this.tbx_RX.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tbx_RX.Location = new System.Drawing.Point(53, 154);
            this.tbx_RX.Name = "tbx_RX";
            this.tbx_RX.Size = new System.Drawing.Size(60, 22);
            this.tbx_RX.TabIndex = 24;
            this.tbx_RX.Text = "0";
            this.tbx_RX.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.tbx_RX.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.tbx_RX_KeyPress);
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label9.Location = new System.Drawing.Point(53, 116);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(60, 35);
            this.label9.TabIndex = 8;
            this.label9.Text = "X";
            this.label9.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label10.Location = new System.Drawing.Point(119, 116);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(60, 35);
            this.label10.TabIndex = 9;
            this.label10.Text = "Y";
            this.label10.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label11.Location = new System.Drawing.Point(185, 116);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(60, 35);
            this.label11.TabIndex = 10;
            this.label11.Text = "Z";
            this.label11.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label12.Location = new System.Drawing.Point(3, 151);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(44, 35);
            this.label12.TabIndex = 11;
            this.label12.Text = "Rotate";
            this.label12.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label13.Location = new System.Drawing.Point(3, 186);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(44, 35);
            this.label13.TabIndex = 12;
            this.label13.Text = "Shift";
            this.label13.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // tbx_RY
            // 
            this.tbx_RY.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tbx_RY.Location = new System.Drawing.Point(119, 154);
            this.tbx_RY.Name = "tbx_RY";
            this.tbx_RY.Size = new System.Drawing.Size(60, 22);
            this.tbx_RY.TabIndex = 25;
            this.tbx_RY.Text = "0";
            this.tbx_RY.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.tbx_RY.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.tbx_RX_KeyPress);
            // 
            // tbx_RZ
            // 
            this.tbx_RZ.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tbx_RZ.Location = new System.Drawing.Point(185, 154);
            this.tbx_RZ.Name = "tbx_RZ";
            this.tbx_RZ.Size = new System.Drawing.Size(60, 22);
            this.tbx_RZ.TabIndex = 26;
            this.tbx_RZ.Text = "0";
            this.tbx_RZ.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.tbx_RZ.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.tbx_RX_KeyPress);
            // 
            // tbx_SY
            // 
            this.tbx_SY.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tbx_SY.Location = new System.Drawing.Point(119, 189);
            this.tbx_SY.Name = "tbx_SY";
            this.tbx_SY.Size = new System.Drawing.Size(60, 22);
            this.tbx_SY.TabIndex = 28;
            this.tbx_SY.Text = "0";
            this.tbx_SY.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.tbx_SY.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.tbx_RX_KeyPress);
            // 
            // tbx_SZ
            // 
            this.tbx_SZ.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tbx_SZ.Location = new System.Drawing.Point(185, 189);
            this.tbx_SZ.Name = "tbx_SZ";
            this.tbx_SZ.Size = new System.Drawing.Size(60, 22);
            this.tbx_SZ.TabIndex = 29;
            this.tbx_SZ.Text = "0";
            this.tbx_SZ.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.tbx_SZ.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.tbx_RX_KeyPress);
            // 
            // tbx_SX
            // 
            this.tbx_SX.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tbx_SX.Location = new System.Drawing.Point(53, 189);
            this.tbx_SX.Name = "tbx_SX";
            this.tbx_SX.Size = new System.Drawing.Size(60, 22);
            this.tbx_SX.TabIndex = 27;
            this.tbx_SX.Text = "0";
            this.tbx_SX.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.tbx_SX.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.tbx_RX_KeyPress);
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.ColumnCount = 3;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 58F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel2.Controls.Add(this.tableLayoutPanel1, 0, 0);
            this.tableLayoutPanel2.Controls.Add(this.tableLayoutPanel3, 2, 0);
            this.tableLayoutPanel2.Controls.Add(this.btn_EulerToMatrix, 1, 1);
            this.tableLayoutPanel2.Controls.Add(this.btn_MatrixToEuler, 1, 2);
            this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel2.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 4;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 40F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 40F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(567, 344);
            this.tableLayoutPanel2.TabIndex = 1;
            // 
            // tableLayoutPanel3
            // 
            this.tableLayoutPanel3.ColumnCount = 5;
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 38F));
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel3.Controls.Add(this.label1, 1, 1);
            this.tableLayoutPanel3.Controls.Add(this.label2, 2, 1);
            this.tableLayoutPanel3.Controls.Add(this.label3, 3, 1);
            this.tableLayoutPanel3.Controls.Add(this.label4, 4, 1);
            this.tableLayoutPanel3.Controls.Add(this.label5, 0, 2);
            this.tableLayoutPanel3.Controls.Add(this.label6, 0, 3);
            this.tableLayoutPanel3.Controls.Add(this.label7, 0, 4);
            this.tableLayoutPanel3.Controls.Add(this.label8, 0, 5);
            this.tableLayoutPanel3.Controls.Add(this.tbx_M00, 1, 2);
            this.tableLayoutPanel3.Controls.Add(this.tbx_M10, 1, 3);
            this.tableLayoutPanel3.Controls.Add(this.tbx_M20, 1, 4);
            this.tableLayoutPanel3.Controls.Add(this.tbx_M30, 1, 5);
            this.tableLayoutPanel3.Controls.Add(this.tbx_M01, 2, 2);
            this.tableLayoutPanel3.Controls.Add(this.tbx_M11, 2, 3);
            this.tableLayoutPanel3.Controls.Add(this.tbx_M21, 2, 4);
            this.tableLayoutPanel3.Controls.Add(this.tbx_M31, 2, 5);
            this.tableLayoutPanel3.Controls.Add(this.tbx_M02, 3, 2);
            this.tableLayoutPanel3.Controls.Add(this.tbx_M12, 3, 3);
            this.tableLayoutPanel3.Controls.Add(this.tbx_M22, 3, 4);
            this.tableLayoutPanel3.Controls.Add(this.tbx_M32, 3, 5);
            this.tableLayoutPanel3.Controls.Add(this.tbx_M03, 4, 2);
            this.tableLayoutPanel3.Controls.Add(this.tbx_M13, 4, 3);
            this.tableLayoutPanel3.Controls.Add(this.tbx_M23, 4, 4);
            this.tableLayoutPanel3.Controls.Add(this.tbx_M33, 4, 5);
            this.tableLayoutPanel3.Controls.Add(this.lbl_Q0, 1, 6);
            this.tableLayoutPanel3.Controls.Add(this.lbl_Q1, 2, 6);
            this.tableLayoutPanel3.Controls.Add(this.lbl_Q2, 3, 6);
            this.tableLayoutPanel3.Controls.Add(this.btn_ResetIdentity, 1, 0);
            this.tableLayoutPanel3.Controls.Add(this.lbl_Q3, 4, 6);
            this.tableLayoutPanel3.Controls.Add(this.btn_SaveMatrix, 4, 0);
            this.tableLayoutPanel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel3.Location = new System.Drawing.Point(315, 3);
            this.tableLayoutPanel3.Name = "tableLayoutPanel3";
            this.tableLayoutPanel3.RowCount = 7;
            this.tableLayoutPanel2.SetRowSpan(this.tableLayoutPanel3, 4);
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 35F));
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 35F));
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 35F));
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 35F));
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel3.Size = new System.Drawing.Size(249, 338);
            this.tableLayoutPanel3.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label1.Location = new System.Drawing.Point(41, 89);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(46, 20);
            this.label1.TabIndex = 0;
            this.label1.Text = "0";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label2.Location = new System.Drawing.Point(93, 89);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(46, 20);
            this.label2.TabIndex = 1;
            this.label2.Text = "1";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label3.Location = new System.Drawing.Point(145, 89);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(46, 20);
            this.label3.TabIndex = 2;
            this.label3.Text = "2";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label4.Location = new System.Drawing.Point(197, 89);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(49, 20);
            this.label4.TabIndex = 3;
            this.label4.Text = "3";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label5.Location = new System.Drawing.Point(3, 109);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(32, 35);
            this.label5.TabIndex = 4;
            this.label5.Text = "0";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label6.Location = new System.Drawing.Point(3, 144);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(32, 35);
            this.label6.TabIndex = 5;
            this.label6.Text = "1";
            this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label7.Location = new System.Drawing.Point(3, 179);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(32, 35);
            this.label7.TabIndex = 6;
            this.label7.Text = "2";
            this.label7.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label8.Location = new System.Drawing.Point(3, 214);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(32, 35);
            this.label8.TabIndex = 7;
            this.label8.Text = "3";
            this.label8.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // tbx_M00
            // 
            this.tbx_M00.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tbx_M00.Location = new System.Drawing.Point(41, 112);
            this.tbx_M00.Name = "tbx_M00";
            this.tbx_M00.Size = new System.Drawing.Size(46, 22);
            this.tbx_M00.TabIndex = 8;
            this.tbx_M00.Text = "1";
            this.tbx_M00.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.tbx_M00.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.tbx_RX_KeyPress);
            // 
            // tbx_M10
            // 
            this.tbx_M10.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tbx_M10.Location = new System.Drawing.Point(41, 147);
            this.tbx_M10.Name = "tbx_M10";
            this.tbx_M10.Size = new System.Drawing.Size(46, 22);
            this.tbx_M10.TabIndex = 9;
            this.tbx_M10.Text = "0";
            this.tbx_M10.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.tbx_M10.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.tbx_RX_KeyPress);
            // 
            // tbx_M20
            // 
            this.tbx_M20.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tbx_M20.Location = new System.Drawing.Point(41, 182);
            this.tbx_M20.Name = "tbx_M20";
            this.tbx_M20.Size = new System.Drawing.Size(46, 22);
            this.tbx_M20.TabIndex = 10;
            this.tbx_M20.Text = "0";
            this.tbx_M20.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.tbx_M20.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.tbx_RX_KeyPress);
            // 
            // tbx_M30
            // 
            this.tbx_M30.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tbx_M30.Location = new System.Drawing.Point(41, 217);
            this.tbx_M30.Name = "tbx_M30";
            this.tbx_M30.Size = new System.Drawing.Size(46, 22);
            this.tbx_M30.TabIndex = 11;
            this.tbx_M30.Text = "0";
            this.tbx_M30.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.tbx_M30.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.tbx_RX_KeyPress);
            // 
            // tbx_M01
            // 
            this.tbx_M01.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tbx_M01.Location = new System.Drawing.Point(93, 112);
            this.tbx_M01.Name = "tbx_M01";
            this.tbx_M01.Size = new System.Drawing.Size(46, 22);
            this.tbx_M01.TabIndex = 12;
            this.tbx_M01.Text = "0";
            this.tbx_M01.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.tbx_M01.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.tbx_RX_KeyPress);
            // 
            // tbx_M11
            // 
            this.tbx_M11.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tbx_M11.Location = new System.Drawing.Point(93, 147);
            this.tbx_M11.Name = "tbx_M11";
            this.tbx_M11.Size = new System.Drawing.Size(46, 22);
            this.tbx_M11.TabIndex = 13;
            this.tbx_M11.Text = "1";
            this.tbx_M11.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.tbx_M11.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.tbx_RX_KeyPress);
            // 
            // tbx_M21
            // 
            this.tbx_M21.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tbx_M21.Location = new System.Drawing.Point(93, 182);
            this.tbx_M21.Name = "tbx_M21";
            this.tbx_M21.Size = new System.Drawing.Size(46, 22);
            this.tbx_M21.TabIndex = 14;
            this.tbx_M21.Text = "0";
            this.tbx_M21.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.tbx_M21.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.tbx_RX_KeyPress);
            // 
            // tbx_M31
            // 
            this.tbx_M31.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tbx_M31.Location = new System.Drawing.Point(93, 217);
            this.tbx_M31.Name = "tbx_M31";
            this.tbx_M31.Size = new System.Drawing.Size(46, 22);
            this.tbx_M31.TabIndex = 15;
            this.tbx_M31.Text = "0";
            this.tbx_M31.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.tbx_M31.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.tbx_RX_KeyPress);
            // 
            // tbx_M02
            // 
            this.tbx_M02.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tbx_M02.Location = new System.Drawing.Point(145, 112);
            this.tbx_M02.Name = "tbx_M02";
            this.tbx_M02.Size = new System.Drawing.Size(46, 22);
            this.tbx_M02.TabIndex = 16;
            this.tbx_M02.Text = "0";
            this.tbx_M02.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.tbx_M02.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.tbx_RX_KeyPress);
            // 
            // tbx_M12
            // 
            this.tbx_M12.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tbx_M12.Location = new System.Drawing.Point(145, 147);
            this.tbx_M12.Name = "tbx_M12";
            this.tbx_M12.Size = new System.Drawing.Size(46, 22);
            this.tbx_M12.TabIndex = 17;
            this.tbx_M12.Text = "0";
            this.tbx_M12.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.tbx_M12.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.tbx_RX_KeyPress);
            // 
            // tbx_M22
            // 
            this.tbx_M22.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tbx_M22.Location = new System.Drawing.Point(145, 182);
            this.tbx_M22.Name = "tbx_M22";
            this.tbx_M22.Size = new System.Drawing.Size(46, 22);
            this.tbx_M22.TabIndex = 18;
            this.tbx_M22.Text = "1";
            this.tbx_M22.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.tbx_M22.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.tbx_RX_KeyPress);
            // 
            // tbx_M32
            // 
            this.tbx_M32.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tbx_M32.Location = new System.Drawing.Point(145, 217);
            this.tbx_M32.Name = "tbx_M32";
            this.tbx_M32.Size = new System.Drawing.Size(46, 22);
            this.tbx_M32.TabIndex = 19;
            this.tbx_M32.Text = "0";
            this.tbx_M32.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.tbx_M32.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.tbx_RX_KeyPress);
            // 
            // tbx_M03
            // 
            this.tbx_M03.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tbx_M03.Location = new System.Drawing.Point(197, 112);
            this.tbx_M03.Name = "tbx_M03";
            this.tbx_M03.Size = new System.Drawing.Size(49, 22);
            this.tbx_M03.TabIndex = 20;
            this.tbx_M03.Text = "0";
            this.tbx_M03.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.tbx_M03.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.tbx_RX_KeyPress);
            // 
            // tbx_M13
            // 
            this.tbx_M13.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tbx_M13.Location = new System.Drawing.Point(197, 147);
            this.tbx_M13.Name = "tbx_M13";
            this.tbx_M13.Size = new System.Drawing.Size(49, 22);
            this.tbx_M13.TabIndex = 21;
            this.tbx_M13.Text = "0";
            this.tbx_M13.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.tbx_M13.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.tbx_RX_KeyPress);
            // 
            // tbx_M23
            // 
            this.tbx_M23.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tbx_M23.Location = new System.Drawing.Point(197, 182);
            this.tbx_M23.Name = "tbx_M23";
            this.tbx_M23.Size = new System.Drawing.Size(49, 22);
            this.tbx_M23.TabIndex = 22;
            this.tbx_M23.Text = "0";
            this.tbx_M23.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.tbx_M23.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.tbx_RX_KeyPress);
            // 
            // tbx_M33
            // 
            this.tbx_M33.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tbx_M33.Location = new System.Drawing.Point(197, 217);
            this.tbx_M33.Name = "tbx_M33";
            this.tbx_M33.Size = new System.Drawing.Size(49, 22);
            this.tbx_M33.TabIndex = 23;
            this.tbx_M33.Text = "1";
            this.tbx_M33.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.tbx_M33.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.tbx_RX_KeyPress);
            // 
            // lbl_Q0
            // 
            this.lbl_Q0.AutoSize = true;
            this.lbl_Q0.Dock = System.Windows.Forms.DockStyle.Top;
            this.lbl_Q0.Location = new System.Drawing.Point(41, 249);
            this.lbl_Q0.Name = "lbl_Q0";
            this.lbl_Q0.Size = new System.Drawing.Size(46, 12);
            this.lbl_Q0.TabIndex = 25;
            this.lbl_Q0.Text = "1";
            this.lbl_Q0.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lbl_Q1
            // 
            this.lbl_Q1.AutoSize = true;
            this.lbl_Q1.Dock = System.Windows.Forms.DockStyle.Top;
            this.lbl_Q1.Location = new System.Drawing.Point(93, 249);
            this.lbl_Q1.Name = "lbl_Q1";
            this.lbl_Q1.Size = new System.Drawing.Size(46, 12);
            this.lbl_Q1.TabIndex = 26;
            this.lbl_Q1.Text = "0";
            this.lbl_Q1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lbl_Q2
            // 
            this.lbl_Q2.AutoSize = true;
            this.lbl_Q2.Dock = System.Windows.Forms.DockStyle.Top;
            this.lbl_Q2.Location = new System.Drawing.Point(145, 249);
            this.lbl_Q2.Name = "lbl_Q2";
            this.lbl_Q2.Size = new System.Drawing.Size(46, 12);
            this.lbl_Q2.TabIndex = 27;
            this.lbl_Q2.Text = "0";
            this.lbl_Q2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // btn_ResetIdentity
            // 
            this.btn_ResetIdentity.Location = new System.Drawing.Point(41, 3);
            this.btn_ResetIdentity.Name = "btn_ResetIdentity";
            this.btn_ResetIdentity.Size = new System.Drawing.Size(46, 23);
            this.btn_ResetIdentity.TabIndex = 24;
            this.btn_ResetIdentity.Text = "Reset";
            this.btn_ResetIdentity.UseVisualStyleBackColor = true;
            this.btn_ResetIdentity.Click += new System.EventHandler(this.btn_ResetIdentity_Click);
            // 
            // lbl_Q3
            // 
            this.lbl_Q3.AutoSize = true;
            this.lbl_Q3.Dock = System.Windows.Forms.DockStyle.Top;
            this.lbl_Q3.Location = new System.Drawing.Point(197, 249);
            this.lbl_Q3.Name = "lbl_Q3";
            this.lbl_Q3.Size = new System.Drawing.Size(49, 12);
            this.lbl_Q3.TabIndex = 28;
            this.lbl_Q3.Text = "0";
            this.lbl_Q3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // btn_EulerToMatrix
            // 
            this.btn_EulerToMatrix.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btn_EulerToMatrix.Image = global::RsLib.PointCloudLib.CalculateMatrix.Properties.Resources.right_48px;
            this.btn_EulerToMatrix.Location = new System.Drawing.Point(257, 135);
            this.btn_EulerToMatrix.Name = "btn_EulerToMatrix";
            this.btn_EulerToMatrix.Size = new System.Drawing.Size(52, 34);
            this.btn_EulerToMatrix.TabIndex = 3;
            this.btn_EulerToMatrix.UseVisualStyleBackColor = true;
            this.btn_EulerToMatrix.Click += new System.EventHandler(this.btn_EulerToMatrix_Click);
            // 
            // btn_MatrixToEuler
            // 
            this.btn_MatrixToEuler.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btn_MatrixToEuler.Image = global::RsLib.PointCloudLib.CalculateMatrix.Properties.Resources.left_48px;
            this.btn_MatrixToEuler.Location = new System.Drawing.Point(257, 175);
            this.btn_MatrixToEuler.Name = "btn_MatrixToEuler";
            this.btn_MatrixToEuler.Size = new System.Drawing.Size(52, 34);
            this.btn_MatrixToEuler.TabIndex = 4;
            this.btn_MatrixToEuler.UseVisualStyleBackColor = true;
            this.btn_MatrixToEuler.Click += new System.EventHandler(this.btn_MatrixToEuler_Click);
            // 
            // btn_SaveMatrix
            // 
            this.btn_SaveMatrix.Dock = System.Windows.Forms.DockStyle.Top;
            this.btn_SaveMatrix.Location = new System.Drawing.Point(197, 3);
            this.btn_SaveMatrix.Name = "btn_SaveMatrix";
            this.btn_SaveMatrix.Size = new System.Drawing.Size(49, 23);
            this.btn_SaveMatrix.TabIndex = 29;
            this.btn_SaveMatrix.Text = "Save";
            this.btn_SaveMatrix.UseVisualStyleBackColor = true;
            this.btn_SaveMatrix.Click += new System.EventHandler(this.btn_SaveMatrix_Click);
            // 
            // btn_LoadEulerData
            // 
            this.btn_LoadEulerData.Location = new System.Drawing.Point(119, 3);
            this.btn_LoadEulerData.Name = "btn_LoadEulerData";
            this.btn_LoadEulerData.Size = new System.Drawing.Size(46, 23);
            this.btn_LoadEulerData.TabIndex = 30;
            this.btn_LoadEulerData.Text = "Load";
            this.btn_LoadEulerData.UseVisualStyleBackColor = true;
            this.btn_LoadEulerData.Click += new System.EventHandler(this.btn_LoadEulerData_Click);
            // 
            // TransMatrixControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tableLayoutPanel2);
            this.Name = "TransMatrixControl";
            this.Size = new System.Drawing.Size(567, 344);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.tableLayoutPanel2.ResumeLayout(false);
            this.tableLayoutPanel3.ResumeLayout(false);
            this.tableLayoutPanel3.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel3;
        private System.Windows.Forms.Button btn_EulerToMatrix;
        private System.Windows.Forms.Button btn_MatrixToEuler;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox tbx_RX;
        private System.Windows.Forms.TextBox tbx_RY;
        private System.Windows.Forms.TextBox tbx_RZ;
        private System.Windows.Forms.TextBox tbx_SY;
        private System.Windows.Forms.TextBox tbx_SZ;
        private System.Windows.Forms.TextBox tbx_SX;
        private System.Windows.Forms.TextBox tbx_M00;
        private System.Windows.Forms.TextBox tbx_M10;
        private System.Windows.Forms.TextBox tbx_M20;
        private System.Windows.Forms.TextBox tbx_M30;
        private System.Windows.Forms.TextBox tbx_M01;
        private System.Windows.Forms.TextBox tbx_M11;
        private System.Windows.Forms.TextBox tbx_M21;
        private System.Windows.Forms.TextBox tbx_M31;
        private System.Windows.Forms.TextBox tbx_M02;
        private System.Windows.Forms.TextBox tbx_M12;
        private System.Windows.Forms.TextBox tbx_M22;
        private System.Windows.Forms.TextBox tbx_M32;
        private System.Windows.Forms.TextBox tbx_M03;
        private System.Windows.Forms.TextBox tbx_M13;
        private System.Windows.Forms.TextBox tbx_M23;
        private System.Windows.Forms.TextBox tbx_M33;
        private System.Windows.Forms.Button btn_ResetIdentity;
        private System.Windows.Forms.Label lbl_Q0;
        private System.Windows.Forms.Label lbl_Q1;
        private System.Windows.Forms.Label lbl_Q2;
        private System.Windows.Forms.Label lbl_Q3;
        private System.Windows.Forms.Button btn_ResetEuler;
        private System.Windows.Forms.Button btn_SaveMatrix;
        private System.Windows.Forms.Button btn_LoadEulerData;
    }
}

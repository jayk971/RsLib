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
            this.btn_EulerToMatrix = new System.Windows.Forms.Button();
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
            this.btn_LoadEulerData = new System.Windows.Forms.Button();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.tableLayoutPanel3 = new System.Windows.Forms.TableLayoutPanel();
            this.rtbx_M = new System.Windows.Forms.RichTextBox();
            this.btn_MatrixToEuler = new System.Windows.Forms.Button();
            this.btn_ResetIdentity = new System.Windows.Forms.Button();
            this.btn_SaveMatrix = new System.Windows.Forms.Button();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.tableLayoutPanel5 = new System.Windows.Forms.TableLayoutPanel();
            this.rtbx_Q = new System.Windows.Forms.RichTextBox();
            this.btn_QToEuler = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.cmb_QSplitChar = new System.Windows.Forms.ComboBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.tableLayoutPanel4 = new System.Windows.Forms.TableLayoutPanel();
            this.rbn_RigidBody = new System.Windows.Forms.RadioButton();
            this.rbn_Axis = new System.Windows.Forms.RadioButton();
            this.tableLayoutPanel1.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tableLayoutPanel3.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.tableLayoutPanel5.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.tableLayoutPanel4.SuspendLayout();
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
            this.tableLayoutPanel1.Controls.Add(this.btn_EulerToMatrix, 3, 4);
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
            this.tableLayoutPanel2.SetRowSpan(this.tableLayoutPanel1, 3);
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 35F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 35F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 35F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(215, 338);
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
            this.tbx_RX.Size = new System.Drawing.Size(49, 22);
            this.tbx_RX.TabIndex = 24;
            this.tbx_RX.Text = "0";
            this.tbx_RX.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.tbx_RX.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.tbx_RX_KeyPress);
            // 
            // btn_EulerToMatrix
            // 
            this.btn_EulerToMatrix.Dock = System.Windows.Forms.DockStyle.Top;
            this.btn_EulerToMatrix.Image = global::RsLib.PointCloudLib.CalculateMatrix.Properties.Resources.right_48px;
            this.btn_EulerToMatrix.Location = new System.Drawing.Point(163, 224);
            this.btn_EulerToMatrix.Name = "btn_EulerToMatrix";
            this.btn_EulerToMatrix.Size = new System.Drawing.Size(49, 34);
            this.btn_EulerToMatrix.TabIndex = 3;
            this.btn_EulerToMatrix.UseVisualStyleBackColor = true;
            this.btn_EulerToMatrix.Click += new System.EventHandler(this.btn_EulerToMatrix_Click);
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label9.Location = new System.Drawing.Point(53, 116);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(49, 35);
            this.label9.TabIndex = 8;
            this.label9.Text = "X";
            this.label9.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label10.Location = new System.Drawing.Point(108, 116);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(49, 35);
            this.label10.TabIndex = 9;
            this.label10.Text = "Y";
            this.label10.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label11.Location = new System.Drawing.Point(163, 116);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(49, 35);
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
            this.tbx_RY.Location = new System.Drawing.Point(108, 154);
            this.tbx_RY.Name = "tbx_RY";
            this.tbx_RY.Size = new System.Drawing.Size(49, 22);
            this.tbx_RY.TabIndex = 25;
            this.tbx_RY.Text = "0";
            this.tbx_RY.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.tbx_RY.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.tbx_RX_KeyPress);
            // 
            // tbx_RZ
            // 
            this.tbx_RZ.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tbx_RZ.Location = new System.Drawing.Point(163, 154);
            this.tbx_RZ.Name = "tbx_RZ";
            this.tbx_RZ.Size = new System.Drawing.Size(49, 22);
            this.tbx_RZ.TabIndex = 26;
            this.tbx_RZ.Text = "0";
            this.tbx_RZ.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.tbx_RZ.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.tbx_RX_KeyPress);
            // 
            // tbx_SY
            // 
            this.tbx_SY.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tbx_SY.Location = new System.Drawing.Point(108, 189);
            this.tbx_SY.Name = "tbx_SY";
            this.tbx_SY.Size = new System.Drawing.Size(49, 22);
            this.tbx_SY.TabIndex = 28;
            this.tbx_SY.Text = "0";
            this.tbx_SY.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.tbx_SY.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.tbx_RX_KeyPress);
            // 
            // tbx_SZ
            // 
            this.tbx_SZ.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tbx_SZ.Location = new System.Drawing.Point(163, 189);
            this.tbx_SZ.Name = "tbx_SZ";
            this.tbx_SZ.Size = new System.Drawing.Size(49, 22);
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
            this.tbx_SX.Size = new System.Drawing.Size(49, 22);
            this.tbx_SX.TabIndex = 27;
            this.tbx_SX.Text = "0";
            this.tbx_SX.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.tbx_SX.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.tbx_RX_KeyPress);
            // 
            // btn_LoadEulerData
            // 
            this.btn_LoadEulerData.Location = new System.Drawing.Point(108, 3);
            this.btn_LoadEulerData.Name = "btn_LoadEulerData";
            this.btn_LoadEulerData.Size = new System.Drawing.Size(46, 23);
            this.btn_LoadEulerData.TabIndex = 30;
            this.btn_LoadEulerData.Text = "Load";
            this.btn_LoadEulerData.UseVisualStyleBackColor = true;
            this.btn_LoadEulerData.Click += new System.EventHandler(this.btn_LoadEulerData_Click);
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.ColumnCount = 3;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 45F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 75F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 55F));
            this.tableLayoutPanel2.Controls.Add(this.tableLayoutPanel1, 0, 0);
            this.tableLayoutPanel2.Controls.Add(this.tabControl1, 2, 0);
            this.tableLayoutPanel2.Controls.Add(this.groupBox1, 1, 1);
            this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel2.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 3;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 145F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(567, 344);
            this.tableLayoutPanel2.TabIndex = 1;
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(299, 3);
            this.tabControl1.Name = "tabControl1";
            this.tableLayoutPanel2.SetRowSpan(this.tabControl1, 3);
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(265, 338);
            this.tabControl1.TabIndex = 32;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.tableLayoutPanel3);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(257, 312);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Matrix 4x4";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // tableLayoutPanel3
            // 
            this.tableLayoutPanel3.ColumnCount = 5;
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 72F));
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel3.Controls.Add(this.rtbx_M, 0, 1);
            this.tableLayoutPanel3.Controls.Add(this.btn_MatrixToEuler, 0, 2);
            this.tableLayoutPanel3.Controls.Add(this.btn_ResetIdentity, 1, 2);
            this.tableLayoutPanel3.Controls.Add(this.btn_SaveMatrix, 2, 2);
            this.tableLayoutPanel3.Controls.Add(this.comboBox1, 3, 2);
            this.tableLayoutPanel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel3.Location = new System.Drawing.Point(3, 3);
            this.tableLayoutPanel3.Name = "tableLayoutPanel3";
            this.tableLayoutPanel3.RowCount = 3;
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 236F));
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel3.Size = new System.Drawing.Size(251, 306);
            this.tableLayoutPanel3.TabIndex = 1;
            // 
            // rtbx_M
            // 
            this.tableLayoutPanel3.SetColumnSpan(this.rtbx_M, 5);
            this.rtbx_M.Dock = System.Windows.Forms.DockStyle.Fill;
            this.rtbx_M.Location = new System.Drawing.Point(3, 38);
            this.rtbx_M.Name = "rtbx_M";
            this.rtbx_M.Size = new System.Drawing.Size(245, 230);
            this.rtbx_M.TabIndex = 30;
            this.rtbx_M.Text = "";
            // 
            // btn_MatrixToEuler
            // 
            this.btn_MatrixToEuler.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btn_MatrixToEuler.Image = global::RsLib.PointCloudLib.CalculateMatrix.Properties.Resources.left_48px;
            this.btn_MatrixToEuler.Location = new System.Drawing.Point(3, 274);
            this.btn_MatrixToEuler.Name = "btn_MatrixToEuler";
            this.btn_MatrixToEuler.Size = new System.Drawing.Size(66, 29);
            this.btn_MatrixToEuler.TabIndex = 4;
            this.btn_MatrixToEuler.UseVisualStyleBackColor = true;
            this.btn_MatrixToEuler.Click += new System.EventHandler(this.btn_MatrixToEuler_Click);
            // 
            // btn_ResetIdentity
            // 
            this.btn_ResetIdentity.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btn_ResetIdentity.Location = new System.Drawing.Point(75, 274);
            this.btn_ResetIdentity.Name = "btn_ResetIdentity";
            this.btn_ResetIdentity.Size = new System.Drawing.Size(38, 29);
            this.btn_ResetIdentity.TabIndex = 24;
            this.btn_ResetIdentity.Text = "Reset";
            this.btn_ResetIdentity.UseVisualStyleBackColor = true;
            this.btn_ResetIdentity.Click += new System.EventHandler(this.btn_ResetIdentity_Click);
            // 
            // btn_SaveMatrix
            // 
            this.btn_SaveMatrix.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btn_SaveMatrix.Location = new System.Drawing.Point(119, 274);
            this.btn_SaveMatrix.Name = "btn_SaveMatrix";
            this.btn_SaveMatrix.Size = new System.Drawing.Size(38, 29);
            this.btn_SaveMatrix.TabIndex = 29;
            this.btn_SaveMatrix.Text = "Save";
            this.btn_SaveMatrix.UseVisualStyleBackColor = true;
            this.btn_SaveMatrix.Click += new System.EventHandler(this.btn_SaveMatrix_Click);
            // 
            // comboBox1
            // 
            this.tableLayoutPanel3.SetColumnSpan(this.comboBox1, 2);
            this.comboBox1.Dock = System.Windows.Forms.DockStyle.Top;
            this.comboBox1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Items.AddRange(new object[] {
            "Space",
            ","});
            this.comboBox1.Location = new System.Drawing.Point(163, 274);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(85, 20);
            this.comboBox1.TabIndex = 35;
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.tableLayoutPanel5);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(257, 312);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Quaternion";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // tableLayoutPanel5
            // 
            this.tableLayoutPanel5.ColumnCount = 3;
            this.tableLayoutPanel5.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 26.29482F));
            this.tableLayoutPanel5.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 39.84064F));
            this.tableLayoutPanel5.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanel5.Controls.Add(this.rtbx_Q, 1, 1);
            this.tableLayoutPanel5.Controls.Add(this.btn_QToEuler, 0, 1);
            this.tableLayoutPanel5.Controls.Add(this.button2, 1, 2);
            this.tableLayoutPanel5.Controls.Add(this.cmb_QSplitChar, 2, 2);
            this.tableLayoutPanel5.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel5.Location = new System.Drawing.Point(3, 3);
            this.tableLayoutPanel5.Name = "tableLayoutPanel5";
            this.tableLayoutPanel5.RowCount = 3;
            this.tableLayoutPanel5.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel5.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 64F));
            this.tableLayoutPanel5.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel5.Size = new System.Drawing.Size(251, 306);
            this.tableLayoutPanel5.TabIndex = 0;
            // 
            // rtbx_Q
            // 
            this.tableLayoutPanel5.SetColumnSpan(this.rtbx_Q, 2);
            this.rtbx_Q.Dock = System.Windows.Forms.DockStyle.Fill;
            this.rtbx_Q.Location = new System.Drawing.Point(66, 121);
            this.rtbx_Q.Margin = new System.Windows.Forms.Padding(0);
            this.rtbx_Q.Name = "rtbx_Q";
            this.rtbx_Q.Size = new System.Drawing.Size(185, 64);
            this.rtbx_Q.TabIndex = 31;
            this.rtbx_Q.Text = "";
            // 
            // btn_QToEuler
            // 
            this.btn_QToEuler.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btn_QToEuler.Image = global::RsLib.PointCloudLib.CalculateMatrix.Properties.Resources.left_48px;
            this.btn_QToEuler.Location = new System.Drawing.Point(3, 124);
            this.btn_QToEuler.Name = "btn_QToEuler";
            this.btn_QToEuler.Size = new System.Drawing.Size(60, 58);
            this.btn_QToEuler.TabIndex = 32;
            this.btn_QToEuler.UseVisualStyleBackColor = true;
            // 
            // button2
            // 
            this.button2.Dock = System.Windows.Forms.DockStyle.Top;
            this.button2.Location = new System.Drawing.Point(69, 188);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(94, 23);
            this.button2.TabIndex = 33;
            this.button2.Text = "Reset";
            this.button2.UseVisualStyleBackColor = true;
            // 
            // cmb_QSplitChar
            // 
            this.cmb_QSplitChar.Dock = System.Windows.Forms.DockStyle.Top;
            this.cmb_QSplitChar.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmb_QSplitChar.FormattingEnabled = true;
            this.cmb_QSplitChar.Items.AddRange(new object[] {
            "Space",
            ","});
            this.cmb_QSplitChar.Location = new System.Drawing.Point(169, 188);
            this.cmb_QSplitChar.Name = "cmb_QSplitChar";
            this.cmb_QSplitChar.Size = new System.Drawing.Size(79, 20);
            this.cmb_QSplitChar.TabIndex = 34;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.tableLayoutPanel4);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox1.Location = new System.Drawing.Point(224, 102);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(69, 139);
            this.groupBox1.TabIndex = 30;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Type";
            // 
            // tableLayoutPanel4
            // 
            this.tableLayoutPanel4.ColumnCount = 1;
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel4.Controls.Add(this.rbn_RigidBody, 0, 0);
            this.tableLayoutPanel4.Controls.Add(this.rbn_Axis, 0, 1);
            this.tableLayoutPanel4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel4.Location = new System.Drawing.Point(3, 18);
            this.tableLayoutPanel4.Name = "tableLayoutPanel4";
            this.tableLayoutPanel4.RowCount = 2;
            this.tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel4.Size = new System.Drawing.Size(63, 118);
            this.tableLayoutPanel4.TabIndex = 0;
            // 
            // rbn_RigidBody
            // 
            this.rbn_RigidBody.Appearance = System.Windows.Forms.Appearance.Button;
            this.rbn_RigidBody.AutoSize = true;
            this.rbn_RigidBody.Checked = true;
            this.rbn_RigidBody.Dock = System.Windows.Forms.DockStyle.Fill;
            this.rbn_RigidBody.Location = new System.Drawing.Point(3, 3);
            this.rbn_RigidBody.Name = "rbn_RigidBody";
            this.rbn_RigidBody.Size = new System.Drawing.Size(57, 53);
            this.rbn_RigidBody.TabIndex = 0;
            this.rbn_RigidBody.TabStop = true;
            this.rbn_RigidBody.Text = "Rigid Body";
            this.rbn_RigidBody.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.rbn_RigidBody.UseVisualStyleBackColor = true;
            // 
            // rbn_Axis
            // 
            this.rbn_Axis.Appearance = System.Windows.Forms.Appearance.Button;
            this.rbn_Axis.AutoSize = true;
            this.rbn_Axis.Dock = System.Windows.Forms.DockStyle.Fill;
            this.rbn_Axis.Location = new System.Drawing.Point(3, 62);
            this.rbn_Axis.Name = "rbn_Axis";
            this.rbn_Axis.Size = new System.Drawing.Size(57, 53);
            this.rbn_Axis.TabIndex = 1;
            this.rbn_Axis.TabStop = true;
            this.rbn_Axis.Text = "Axis";
            this.rbn_Axis.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.rbn_Axis.UseVisualStyleBackColor = true;
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
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tableLayoutPanel3.ResumeLayout(false);
            this.tabPage2.ResumeLayout(false);
            this.tableLayoutPanel5.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.tableLayoutPanel4.ResumeLayout(false);
            this.tableLayoutPanel4.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private System.Windows.Forms.Button btn_EulerToMatrix;
        private System.Windows.Forms.Button btn_MatrixToEuler;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.TextBox tbx_RX;
        private System.Windows.Forms.TextBox tbx_RY;
        private System.Windows.Forms.TextBox tbx_RZ;
        private System.Windows.Forms.TextBox tbx_SY;
        private System.Windows.Forms.TextBox tbx_SZ;
        private System.Windows.Forms.TextBox tbx_SX;
        private System.Windows.Forms.Button btn_ResetEuler;
        private System.Windows.Forms.Button btn_LoadEulerData;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel4;
        private System.Windows.Forms.RadioButton rbn_RigidBody;
        private System.Windows.Forms.RadioButton rbn_Axis;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel3;
        private System.Windows.Forms.Button btn_ResetIdentity;
        private System.Windows.Forms.Button btn_SaveMatrix;
        private System.Windows.Forms.RichTextBox rtbx_M;
        private System.Windows.Forms.RichTextBox rtbx_Q;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel5;
        private System.Windows.Forms.Button btn_QToEuler;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.ComboBox comboBox1;
        private System.Windows.Forms.ComboBox cmb_QSplitChar;
    }
}

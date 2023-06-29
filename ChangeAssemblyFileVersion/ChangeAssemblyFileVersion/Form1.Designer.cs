
namespace ChangeAssemblyFileVersion
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
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.label1 = new System.Windows.Forms.Label();
            this.treeView1 = new System.Windows.Forms.TreeView();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.btn_ClearSln = new System.Windows.Forms.Button();
            this.btn_SelectSLN = new System.Windows.Forms.Button();
            this.btn_DeleteSln = new System.Windows.Forms.Button();
            this.btn_UpdateAll = new System.Windows.Forms.Button();
            this.btn_Update = new System.Windows.Forms.Button();
            this.btn_ManualUpdateAll = new System.Windows.Forms.Button();
            this.lbl_Today = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.tableLayoutPanel1.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 3;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 100F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 100F));
            this.tableLayoutPanel1.Controls.Add(this.label1, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.treeView1, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.comboBox1, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.tableLayoutPanel2, 2, 0);
            this.tableLayoutPanel1.Controls.Add(this.panel1, 0, 2);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 3;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 146F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(724, 671);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label1.Location = new System.Drawing.Point(3, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(94, 30);
            this.label1.TabIndex = 0;
            this.label1.Text = "sln File";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // treeView1
            // 
            this.tableLayoutPanel1.SetColumnSpan(this.treeView1, 2);
            this.treeView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.treeView1.Font = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.treeView1.Location = new System.Drawing.Point(3, 33);
            this.treeView1.Name = "treeView1";
            this.treeView1.Size = new System.Drawing.Size(618, 489);
            this.treeView1.TabIndex = 3;
            this.treeView1.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.treeView1_MouseDoubleClick);
            // 
            // comboBox1
            // 
            this.comboBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.comboBox1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox1.Font = new System.Drawing.Font("新細明體", 11F);
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Location = new System.Drawing.Point(103, 3);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(518, 23);
            this.comboBox1.TabIndex = 6;
            this.comboBox1.SelectedIndexChanged += new System.EventHandler(this.comboBox1_SelectedIndexChanged);
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.ColumnCount = 1;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel2.Controls.Add(this.btn_ClearSln, 0, 2);
            this.tableLayoutPanel2.Controls.Add(this.btn_SelectSLN, 0, 0);
            this.tableLayoutPanel2.Controls.Add(this.btn_DeleteSln, 0, 1);
            this.tableLayoutPanel2.Controls.Add(this.btn_UpdateAll, 0, 6);
            this.tableLayoutPanel2.Controls.Add(this.btn_Update, 0, 5);
            this.tableLayoutPanel2.Controls.Add(this.btn_ManualUpdateAll, 0, 7);
            this.tableLayoutPanel2.Controls.Add(this.lbl_Today, 0, 4);
            this.tableLayoutPanel2.Controls.Add(this.label2, 0, 3);
            this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel2.Location = new System.Drawing.Point(627, 3);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 8;
            this.tableLayoutPanel1.SetRowSpan(this.tableLayoutPanel2, 2);
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 50F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 50F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 50F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 35F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(94, 519);
            this.tableLayoutPanel2.TabIndex = 7;
            // 
            // btn_ClearSln
            // 
            this.btn_ClearSln.Dock = System.Windows.Forms.DockStyle.Top;
            this.btn_ClearSln.Location = new System.Drawing.Point(3, 103);
            this.btn_ClearSln.Name = "btn_ClearSln";
            this.btn_ClearSln.Size = new System.Drawing.Size(88, 24);
            this.btn_ClearSln.TabIndex = 7;
            this.btn_ClearSln.Text = "Clear sln";
            this.btn_ClearSln.UseVisualStyleBackColor = true;
            this.btn_ClearSln.Click += new System.EventHandler(this.btn_ClearSln_Click);
            // 
            // btn_SelectSLN
            // 
            this.btn_SelectSLN.Dock = System.Windows.Forms.DockStyle.Top;
            this.btn_SelectSLN.Location = new System.Drawing.Point(3, 3);
            this.btn_SelectSLN.Name = "btn_SelectSLN";
            this.btn_SelectSLN.Size = new System.Drawing.Size(88, 24);
            this.btn_SelectSLN.TabIndex = 2;
            this.btn_SelectSLN.Text = "Select sln";
            this.btn_SelectSLN.UseVisualStyleBackColor = true;
            this.btn_SelectSLN.Click += new System.EventHandler(this.btn_SelectSLN_Click);
            // 
            // btn_DeleteSln
            // 
            this.btn_DeleteSln.Dock = System.Windows.Forms.DockStyle.Top;
            this.btn_DeleteSln.Location = new System.Drawing.Point(3, 53);
            this.btn_DeleteSln.Name = "btn_DeleteSln";
            this.btn_DeleteSln.Size = new System.Drawing.Size(88, 24);
            this.btn_DeleteSln.TabIndex = 6;
            this.btn_DeleteSln.Text = "Delete sln";
            this.btn_DeleteSln.UseVisualStyleBackColor = true;
            this.btn_DeleteSln.Click += new System.EventHandler(this.btn_DeleteSln_Click);
            // 
            // btn_UpdateAll
            // 
            this.btn_UpdateAll.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btn_UpdateAll.Location = new System.Drawing.Point(3, 354);
            this.btn_UpdateAll.Name = "btn_UpdateAll";
            this.btn_UpdateAll.Size = new System.Drawing.Size(88, 77);
            this.btn_UpdateAll.TabIndex = 5;
            this.btn_UpdateAll.Text = "Auto Update All";
            this.btn_UpdateAll.UseVisualStyleBackColor = true;
            this.btn_UpdateAll.Click += new System.EventHandler(this.btn_UpdateAll_Click);
            // 
            // btn_Update
            // 
            this.btn_Update.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btn_Update.Location = new System.Drawing.Point(3, 271);
            this.btn_Update.Name = "btn_Update";
            this.btn_Update.Size = new System.Drawing.Size(88, 77);
            this.btn_Update.TabIndex = 4;
            this.btn_Update.Text = "Auto Update";
            this.btn_Update.UseVisualStyleBackColor = true;
            this.btn_Update.Click += new System.EventHandler(this.btn_Update_Click);
            // 
            // btn_ManualUpdateAll
            // 
            this.btn_ManualUpdateAll.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btn_ManualUpdateAll.Location = new System.Drawing.Point(3, 437);
            this.btn_ManualUpdateAll.Name = "btn_ManualUpdateAll";
            this.btn_ManualUpdateAll.Size = new System.Drawing.Size(88, 79);
            this.btn_ManualUpdateAll.TabIndex = 8;
            this.btn_ManualUpdateAll.Text = "Manual Update All";
            this.btn_ManualUpdateAll.UseVisualStyleBackColor = true;
            this.btn_ManualUpdateAll.Click += new System.EventHandler(this.btn_ManualUpdateAll_Click);
            // 
            // lbl_Today
            // 
            this.lbl_Today.AutoSize = true;
            this.lbl_Today.BackColor = System.Drawing.Color.Bisque;
            this.lbl_Today.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lbl_Today.Font = new System.Drawing.Font("Arial", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_Today.Location = new System.Drawing.Point(3, 185);
            this.lbl_Today.Name = "lbl_Today";
            this.lbl_Today.Size = new System.Drawing.Size(88, 83);
            this.lbl_Today.TabIndex = 9;
            this.lbl_Today.Text = "23001";
            this.lbl_Today.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label2.Font = new System.Drawing.Font("Arial", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(3, 150);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(88, 35);
            this.label2.TabIndex = 10;
            this.label2.Text = "Today :";
            this.label2.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
            // 
            // panel1
            // 
            this.tableLayoutPanel1.SetColumnSpan(this.panel1, 3);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(3, 528);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(718, 140);
            this.panel1.TabIndex = 8;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(724, 671);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Form1";
            this.Text = "Change Assembly Version";
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.tableLayoutPanel2.ResumeLayout(false);
            this.tableLayoutPanel2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btn_SelectSLN;
        private System.Windows.Forms.TreeView treeView1;
        private System.Windows.Forms.Button btn_Update;
        private System.Windows.Forms.Button btn_UpdateAll;
        private System.Windows.Forms.ComboBox comboBox1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private System.Windows.Forms.Button btn_DeleteSln;
        private System.Windows.Forms.Button btn_ClearSln;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button btn_ManualUpdateAll;
        private System.Windows.Forms.Label lbl_Today;
        private System.Windows.Forms.Label label2;
    }
}


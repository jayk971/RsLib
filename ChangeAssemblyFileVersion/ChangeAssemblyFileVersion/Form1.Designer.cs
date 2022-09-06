
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
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
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
            this.treeView1.Size = new System.Drawing.Size(618, 635);
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
            this.tableLayoutPanel2.Controls.Add(this.btn_UpdateAll, 0, 5);
            this.tableLayoutPanel2.Controls.Add(this.btn_Update, 0, 4);
            this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel2.Location = new System.Drawing.Point(627, 3);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 6;
            this.tableLayoutPanel1.SetRowSpan(this.tableLayoutPanel2, 2);
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 16.66667F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 16.66667F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 16.66667F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 16.66667F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 16.66667F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 16.66667F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(94, 665);
            this.tableLayoutPanel2.TabIndex = 7;
            // 
            // btn_ClearSln
            // 
            this.btn_ClearSln.Dock = System.Windows.Forms.DockStyle.Top;
            this.btn_ClearSln.Location = new System.Drawing.Point(3, 223);
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
            this.btn_DeleteSln.Location = new System.Drawing.Point(3, 113);
            this.btn_DeleteSln.Name = "btn_DeleteSln";
            this.btn_DeleteSln.Size = new System.Drawing.Size(88, 24);
            this.btn_DeleteSln.TabIndex = 6;
            this.btn_DeleteSln.Text = "Delete sln";
            this.btn_DeleteSln.UseVisualStyleBackColor = true;
            this.btn_DeleteSln.Click += new System.EventHandler(this.btn_DeleteSln_Click);
            // 
            // btn_UpdateAll
            // 
            this.btn_UpdateAll.Dock = System.Windows.Forms.DockStyle.Top;
            this.btn_UpdateAll.Location = new System.Drawing.Point(3, 553);
            this.btn_UpdateAll.Name = "btn_UpdateAll";
            this.btn_UpdateAll.Size = new System.Drawing.Size(88, 29);
            this.btn_UpdateAll.TabIndex = 5;
            this.btn_UpdateAll.Text = "Update All";
            this.btn_UpdateAll.UseVisualStyleBackColor = true;
            this.btn_UpdateAll.Click += new System.EventHandler(this.btn_UpdateAll_Click);
            // 
            // btn_Update
            // 
            this.btn_Update.Dock = System.Windows.Forms.DockStyle.Top;
            this.btn_Update.Location = new System.Drawing.Point(3, 443);
            this.btn_Update.Name = "btn_Update";
            this.btn_Update.Size = new System.Drawing.Size(88, 29);
            this.btn_Update.TabIndex = 4;
            this.btn_Update.Text = "Update";
            this.btn_Update.UseVisualStyleBackColor = true;
            this.btn_Update.Click += new System.EventHandler(this.btn_Update_Click);
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
    }
}


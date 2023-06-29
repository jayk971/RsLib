
namespace ChangeAssemblyFileVersion
{
    partial class Form2
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.tbx_Main = new System.Windows.Forms.TextBox();
            this.tbx_Sub = new System.Windows.Forms.TextBox();
            this.tbx_Build = new System.Windows.Forms.TextBox();
            this.tbx_Revise = new System.Windows.Forms.TextBox();
            this.btn_Update = new System.Windows.Forms.Button();
            this.btn_Exit = new System.Windows.Forms.Button();
            this.tableLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 6;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 16.66667F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 16.66667F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 16.66667F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 16.66667F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 16.66667F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 16.66667F));
            this.tableLayoutPanel1.Controls.Add(this.tbx_Revise, 3, 0);
            this.tableLayoutPanel1.Controls.Add(this.tbx_Build, 2, 0);
            this.tableLayoutPanel1.Controls.Add(this.tbx_Main, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.tbx_Sub, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.btn_Update, 4, 0);
            this.tableLayoutPanel1.Controls.Add(this.btn_Exit, 5, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 1;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(638, 38);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // tbx_Main
            // 
            this.tbx_Main.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tbx_Main.Font = new System.Drawing.Font("新細明體", 12F);
            this.tbx_Main.Location = new System.Drawing.Point(3, 3);
            this.tbx_Main.Name = "tbx_Main";
            this.tbx_Main.Size = new System.Drawing.Size(100, 27);
            this.tbx_Main.TabIndex = 0;
            this.tbx_Main.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.tbx_Main.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.tbx_Main_KeyPress);
            // 
            // tbx_Sub
            // 
            this.tbx_Sub.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tbx_Sub.Font = new System.Drawing.Font("新細明體", 12F);
            this.tbx_Sub.Location = new System.Drawing.Point(109, 3);
            this.tbx_Sub.Name = "tbx_Sub";
            this.tbx_Sub.Size = new System.Drawing.Size(100, 27);
            this.tbx_Sub.TabIndex = 1;
            this.tbx_Sub.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.tbx_Sub.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.tbx_Main_KeyPress);
            // 
            // tbx_Build
            // 
            this.tbx_Build.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tbx_Build.Font = new System.Drawing.Font("新細明體", 12F);
            this.tbx_Build.Location = new System.Drawing.Point(215, 3);
            this.tbx_Build.Name = "tbx_Build";
            this.tbx_Build.Size = new System.Drawing.Size(100, 27);
            this.tbx_Build.TabIndex = 2;
            this.tbx_Build.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.tbx_Build.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.tbx_Main_KeyPress);
            // 
            // tbx_Revise
            // 
            this.tbx_Revise.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tbx_Revise.Font = new System.Drawing.Font("新細明體", 12F);
            this.tbx_Revise.Location = new System.Drawing.Point(321, 3);
            this.tbx_Revise.Name = "tbx_Revise";
            this.tbx_Revise.Size = new System.Drawing.Size(100, 27);
            this.tbx_Revise.TabIndex = 3;
            this.tbx_Revise.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.tbx_Revise.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.tbx_Main_KeyPress);
            // 
            // btn_Update
            // 
            this.btn_Update.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btn_Update.Location = new System.Drawing.Point(427, 3);
            this.btn_Update.Name = "btn_Update";
            this.btn_Update.Size = new System.Drawing.Size(100, 32);
            this.btn_Update.TabIndex = 4;
            this.btn_Update.Text = "Update";
            this.btn_Update.UseVisualStyleBackColor = true;
            this.btn_Update.Click += new System.EventHandler(this.btn_Update_Click);
            // 
            // btn_Exit
            // 
            this.btn_Exit.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btn_Exit.Location = new System.Drawing.Point(533, 3);
            this.btn_Exit.Name = "btn_Exit";
            this.btn_Exit.Size = new System.Drawing.Size(102, 32);
            this.btn_Exit.TabIndex = 5;
            this.btn_Exit.Text = "Exit";
            this.btn_Exit.UseVisualStyleBackColor = true;
            this.btn_Exit.Click += new System.EventHandler(this.btn_Exit_Click);
            // 
            // Form2
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(638, 38);
            this.ControlBox = false;
            this.Controls.Add(this.tableLayoutPanel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "Form2";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Form2";
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.TextBox tbx_Revise;
        private System.Windows.Forms.TextBox tbx_Build;
        private System.Windows.Forms.TextBox tbx_Main;
        private System.Windows.Forms.TextBox tbx_Sub;
        private System.Windows.Forms.Button btn_Update;
        private System.Windows.Forms.Button btn_Exit;
    }
}
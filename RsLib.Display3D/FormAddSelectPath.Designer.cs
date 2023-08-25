namespace RsLib.Display3D
{
    partial class FormAddSelectPath
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormAddSelectPath));
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.toolBtn_Clear = new System.Windows.Forms.ToolStripButton();
            this.toolDropDownBtn_Save = new System.Windows.Forms.ToolStripDropDownButton();
            this.saveOPTToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveMODToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveMODWithRobtargetToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.treeView1 = new System.Windows.Forms.TreeView();
            this.tableLayoutPanel1.SuspendLayout();
            this.toolStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.Controls.Add(this.toolStrip1, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.treeView1, 0, 1);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 35F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(556, 465);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // toolStrip1
            // 
            this.toolStrip1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.toolStrip1.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.toolStrip1.ImageScalingSize = new System.Drawing.Size(32, 32);
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolBtn_Clear,
            this.toolDropDownBtn_Save});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(556, 35);
            this.toolStrip1.TabIndex = 0;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // toolBtn_Clear
            // 
            this.toolBtn_Clear.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.toolBtn_Clear.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolBtn_Clear.Image = global::RsLib.Display3D.Properties.Resources.broom_30px;
            this.toolBtn_Clear.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolBtn_Clear.Name = "toolBtn_Clear";
            this.toolBtn_Clear.Size = new System.Drawing.Size(36, 32);
            this.toolBtn_Clear.Text = "Clear";
            this.toolBtn_Clear.Click += new System.EventHandler(this.toolBtn_Clear_Click);
            // 
            // toolDropDownBtn_Save
            // 
            this.toolDropDownBtn_Save.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolDropDownBtn_Save.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.saveOPTToolStripMenuItem,
            this.saveMODToolStripMenuItem,
            this.saveMODWithRobtargetToolStripMenuItem});
            this.toolDropDownBtn_Save.Image = global::RsLib.Display3D.Properties.Resources.save_30px;
            this.toolDropDownBtn_Save.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolDropDownBtn_Save.Name = "toolDropDownBtn_Save";
            this.toolDropDownBtn_Save.Size = new System.Drawing.Size(45, 32);
            this.toolDropDownBtn_Save.Text = "Save";
            this.toolDropDownBtn_Save.Click += new System.EventHandler(this.toolDropDownBtn_Save_Click);
            // 
            // saveOPTToolStripMenuItem
            // 
            this.saveOPTToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("saveOPTToolStripMenuItem.Image")));
            this.saveOPTToolStripMenuItem.Name = "saveOPTToolStripMenuItem";
            this.saveOPTToolStripMenuItem.Size = new System.Drawing.Size(222, 22);
            this.saveOPTToolStripMenuItem.Text = "Save OPT";
            this.saveOPTToolStripMenuItem.Click += new System.EventHandler(this.saveOPTToolStripMenuItem_Click);
            // 
            // saveMODToolStripMenuItem
            // 
            this.saveMODToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("saveMODToolStripMenuItem.Image")));
            this.saveMODToolStripMenuItem.Name = "saveMODToolStripMenuItem";
            this.saveMODToolStripMenuItem.Size = new System.Drawing.Size(222, 22);
            this.saveMODToolStripMenuItem.Text = "Save MOD";
            this.saveMODToolStripMenuItem.Click += new System.EventHandler(this.saveMODToolStripMenuItem_Click);
            // 
            // saveMODWithRobtargetToolStripMenuItem
            // 
            this.saveMODWithRobtargetToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("saveMODWithRobtargetToolStripMenuItem.Image")));
            this.saveMODWithRobtargetToolStripMenuItem.Name = "saveMODWithRobtargetToolStripMenuItem";
            this.saveMODWithRobtargetToolStripMenuItem.Size = new System.Drawing.Size(222, 22);
            this.saveMODWithRobtargetToolStripMenuItem.Text = "Save MOD with Robtarget";
            this.saveMODWithRobtargetToolStripMenuItem.Click += new System.EventHandler(this.saveMODWithRobtargetToolStripMenuItem_Click);
            // 
            // treeView1
            // 
            this.treeView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.treeView1.Location = new System.Drawing.Point(3, 38);
            this.treeView1.Name = "treeView1";
            this.treeView1.Size = new System.Drawing.Size(550, 424);
            this.treeView1.TabIndex = 1;
            // 
            // FormAddSelectPath
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(556, 465);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FormAddSelectPath";
            this.Text = "Add Select Path";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FormAddSelectPath_FormClosing);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton toolBtn_Clear;
        private System.Windows.Forms.TreeView treeView1;
        private System.Windows.Forms.ToolStripDropDownButton toolDropDownBtn_Save;
        private System.Windows.Forms.ToolStripMenuItem saveOPTToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saveMODToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saveMODWithRobtargetToolStripMenuItem;
    }
}
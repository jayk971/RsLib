
namespace RsLib.X8000TCP
{
    partial class X8000Control
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(X8000Control));
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.toolStripLabel1 = new System.Windows.Forms.ToolStripLabel();
            this.tbx_IP = new System.Windows.Forms.ToolStripTextBox();
            this.toolStripLabel2 = new System.Windows.Forms.ToolStripLabel();
            this.tbx_Port = new System.Windows.Forms.ToolStripTextBox();
            this.btn_Connect = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripDropDownButton1 = new System.Windows.Forms.ToolStripDropDownButton();
            this.mim_Reset = new System.Windows.Forms.ToolStripMenuItem();
            this.mim_Reboot = new System.Windows.Forms.ToolStripMenuItem();
            this.mim_ClearError = new System.Windows.Forms.ToolStripMenuItem();
            this.mim_SaveScreenPrint = new System.Windows.Forms.ToolStripMenuItem();
            this.mim_X8kVersion = new System.Windows.Forms.ToolStripMenuItem();
            this.mim_ReadX8000Time = new System.Windows.Forms.ToolStripMenuItem();
            this.setX8000TimeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayoutPanel3 = new System.Windows.Forms.TableLayoutPanel();
            this.btn_Trigger = new System.Windows.Forms.Button();
            this.chbx_TriggerEnable = new System.Windows.Forms.CheckBox();
            this.tableLayoutPanel4 = new System.Windows.Forms.TableLayoutPanel();
            this.rbn_SetMode = new System.Windows.Forms.RadioButton();
            this.rbn_RunMode = new System.Windows.Forms.RadioButton();
            this.label1 = new System.Windows.Forms.Label();
            this.cmb_x8000Number = new System.Windows.Forms.ComboBox();
            this.btn_ReadX8000SettingNum = new System.Windows.Forms.Button();
            this.btn_WriteX8000SettingNum = new System.Windows.Forms.Button();
            this.richTextBox1 = new System.Windows.Forms.RichTextBox();
            this.tableLayoutPanel1.SuspendLayout();
            this.toolStrip1.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            this.tableLayoutPanel3.SuspendLayout();
            this.tableLayoutPanel4.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Controls.Add(this.toolStrip1, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.tableLayoutPanel2, 0, 1);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 45F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(718, 240);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // toolStrip1
            // 
            this.toolStrip1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.toolStrip1.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.toolStrip1.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripLabel1,
            this.tbx_IP,
            this.toolStripLabel2,
            this.tbx_Port,
            this.btn_Connect,
            this.toolStripSeparator1,
            this.toolStripDropDownButton1});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Padding = new System.Windows.Forms.Padding(0, 0, 3, 0);
            this.toolStrip1.Size = new System.Drawing.Size(718, 45);
            this.toolStrip1.TabIndex = 1;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // toolStripLabel1
            // 
            this.toolStripLabel1.Name = "toolStripLabel1";
            this.toolStripLabel1.Size = new System.Drawing.Size(26, 40);
            this.toolStripLabel1.Text = "IP";
            // 
            // tbx_IP
            // 
            this.tbx_IP.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.tbx_IP.Name = "tbx_IP";
            this.tbx_IP.Size = new System.Drawing.Size(148, 45);
            this.tbx_IP.Text = "192.168.83.2";
            // 
            // toolStripLabel2
            // 
            this.toolStripLabel2.Name = "toolStripLabel2";
            this.toolStripLabel2.Size = new System.Drawing.Size(46, 40);
            this.toolStripLabel2.Text = "Port";
            // 
            // tbx_Port
            // 
            this.tbx_Port.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.tbx_Port.Name = "tbx_Port";
            this.tbx_Port.Size = new System.Drawing.Size(73, 45);
            this.tbx_Port.Text = "8500";
            // 
            // btn_Connect
            // 
            this.btn_Connect.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btn_Connect.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btn_Connect.Image = global::RsLib.X8000TCP.Properties.Resources.icons8_link_64;
            this.btn_Connect.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btn_Connect.Name = "btn_Connect";
            this.btn_Connect.Size = new System.Drawing.Size(34, 40);
            this.btn_Connect.Text = "Connect";
            this.btn_Connect.Click += new System.EventHandler(this.btn_Connect_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 45);
            // 
            // toolStripDropDownButton1
            // 
            this.toolStripDropDownButton1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.toolStripDropDownButton1.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mim_Reset,
            this.mim_Reboot,
            this.mim_ClearError,
            this.mim_SaveScreenPrint,
            this.mim_X8kVersion,
            this.mim_ReadX8000Time,
            this.setX8000TimeToolStripMenuItem});
            this.toolStripDropDownButton1.Image = ((System.Drawing.Image)(resources.GetObject("toolStripDropDownButton1.Image")));
            this.toolStripDropDownButton1.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripDropDownButton1.Name = "toolStripDropDownButton1";
            this.toolStripDropDownButton1.Size = new System.Drawing.Size(88, 40);
            this.toolStripDropDownButton1.Text = "Option";
            this.toolStripDropDownButton1.Click += new System.EventHandler(this.toolStripDropDownButton1_Click);
            // 
            // mim_Reset
            // 
            this.mim_Reset.Name = "mim_Reset";
            this.mim_Reset.Size = new System.Drawing.Size(257, 34);
            this.mim_Reset.Text = "Reset";
            this.mim_Reset.Click += new System.EventHandler(this.mim_Reset_Click);
            // 
            // mim_Reboot
            // 
            this.mim_Reboot.Name = "mim_Reboot";
            this.mim_Reboot.Size = new System.Drawing.Size(257, 34);
            this.mim_Reboot.Text = "Reboot";
            this.mim_Reboot.Click += new System.EventHandler(this.mim_Reboot_Click);
            // 
            // mim_ClearError
            // 
            this.mim_ClearError.Name = "mim_ClearError";
            this.mim_ClearError.Size = new System.Drawing.Size(257, 34);
            this.mim_ClearError.Text = "Clear Error";
            this.mim_ClearError.Click += new System.EventHandler(this.mim_ClearError_Click);
            // 
            // mim_SaveScreenPrint
            // 
            this.mim_SaveScreenPrint.Name = "mim_SaveScreenPrint";
            this.mim_SaveScreenPrint.Size = new System.Drawing.Size(257, 34);
            this.mim_SaveScreenPrint.Text = "Save Screen Print";
            this.mim_SaveScreenPrint.Click += new System.EventHandler(this.mim_SaveScreenPrint_Click);
            // 
            // mim_X8kVersion
            // 
            this.mim_X8kVersion.Name = "mim_X8kVersion";
            this.mim_X8kVersion.Size = new System.Drawing.Size(257, 34);
            this.mim_X8kVersion.Text = "X8000 Version";
            this.mim_X8kVersion.Click += new System.EventHandler(this.mim_X8kVersion_Click);
            // 
            // mim_ReadX8000Time
            // 
            this.mim_ReadX8000Time.Name = "mim_ReadX8000Time";
            this.mim_ReadX8000Time.Size = new System.Drawing.Size(257, 34);
            this.mim_ReadX8000Time.Text = "Read X8000 Time";
            this.mim_ReadX8000Time.Click += new System.EventHandler(this.mim_ReadX8000Time_Click);
            // 
            // setX8000TimeToolStripMenuItem
            // 
            this.setX8000TimeToolStripMenuItem.Name = "setX8000TimeToolStripMenuItem";
            this.setX8000TimeToolStripMenuItem.Size = new System.Drawing.Size(257, 34);
            this.setX8000TimeToolStripMenuItem.Text = "Set X8000 Time";
            this.setX8000TimeToolStripMenuItem.Click += new System.EventHandler(this.setX8000TimeToolStripMenuItem_Click);
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.ColumnCount = 4;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel2.Controls.Add(this.tableLayoutPanel3, 0, 0);
            this.tableLayoutPanel2.Controls.Add(this.tableLayoutPanel4, 0, 1);
            this.tableLayoutPanel2.Controls.Add(this.label1, 0, 2);
            this.tableLayoutPanel2.Controls.Add(this.cmb_x8000Number, 1, 2);
            this.tableLayoutPanel2.Controls.Add(this.btn_ReadX8000SettingNum, 2, 2);
            this.tableLayoutPanel2.Controls.Add(this.btn_WriteX8000SettingNum, 3, 2);
            this.tableLayoutPanel2.Controls.Add(this.richTextBox1, 2, 0);
            this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel2.Location = new System.Drawing.Point(4, 49);
            this.tableLayoutPanel2.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 4;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 52F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 52F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 52F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(710, 187);
            this.tableLayoutPanel2.TabIndex = 2;
            // 
            // tableLayoutPanel3
            // 
            this.tableLayoutPanel3.ColumnCount = 2;
            this.tableLayoutPanel2.SetColumnSpan(this.tableLayoutPanel3, 2);
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tableLayoutPanel3.Controls.Add(this.btn_Trigger, 1, 0);
            this.tableLayoutPanel3.Controls.Add(this.chbx_TriggerEnable, 0, 0);
            this.tableLayoutPanel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel3.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel3.Margin = new System.Windows.Forms.Padding(0);
            this.tableLayoutPanel3.Name = "tableLayoutPanel3";
            this.tableLayoutPanel3.RowCount = 1;
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel3.Size = new System.Drawing.Size(354, 52);
            this.tableLayoutPanel3.TabIndex = 17;
            // 
            // btn_Trigger
            // 
            this.btn_Trigger.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btn_Trigger.Location = new System.Drawing.Point(181, 4);
            this.btn_Trigger.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btn_Trigger.Name = "btn_Trigger";
            this.btn_Trigger.Size = new System.Drawing.Size(169, 44);
            this.btn_Trigger.TabIndex = 16;
            this.btn_Trigger.Text = "Trigger";
            this.btn_Trigger.UseVisualStyleBackColor = true;
            this.btn_Trigger.Click += new System.EventHandler(this.btn_Trigger_Click);
            // 
            // chbx_TriggerEnable
            // 
            this.chbx_TriggerEnable.Appearance = System.Windows.Forms.Appearance.Button;
            this.chbx_TriggerEnable.AutoSize = true;
            this.chbx_TriggerEnable.Checked = true;
            this.chbx_TriggerEnable.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chbx_TriggerEnable.Dock = System.Windows.Forms.DockStyle.Fill;
            this.chbx_TriggerEnable.Location = new System.Drawing.Point(4, 4);
            this.chbx_TriggerEnable.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.chbx_TriggerEnable.Name = "chbx_TriggerEnable";
            this.chbx_TriggerEnable.Size = new System.Drawing.Size(169, 44);
            this.chbx_TriggerEnable.TabIndex = 17;
            this.chbx_TriggerEnable.Text = "Trigger Enable";
            this.chbx_TriggerEnable.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.chbx_TriggerEnable.UseVisualStyleBackColor = true;
            this.chbx_TriggerEnable.CheckedChanged += new System.EventHandler(this.chbx_TriggerEnable_CheckedChanged);
            // 
            // tableLayoutPanel4
            // 
            this.tableLayoutPanel4.ColumnCount = 2;
            this.tableLayoutPanel2.SetColumnSpan(this.tableLayoutPanel4, 2);
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel4.Controls.Add(this.rbn_SetMode, 1, 0);
            this.tableLayoutPanel4.Controls.Add(this.rbn_RunMode, 0, 0);
            this.tableLayoutPanel4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel4.Location = new System.Drawing.Point(0, 52);
            this.tableLayoutPanel4.Margin = new System.Windows.Forms.Padding(0);
            this.tableLayoutPanel4.Name = "tableLayoutPanel4";
            this.tableLayoutPanel4.RowCount = 1;
            this.tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 52F));
            this.tableLayoutPanel4.Size = new System.Drawing.Size(354, 52);
            this.tableLayoutPanel4.TabIndex = 18;
            // 
            // rbn_SetMode
            // 
            this.rbn_SetMode.Appearance = System.Windows.Forms.Appearance.Button;
            this.rbn_SetMode.AutoSize = true;
            this.rbn_SetMode.Dock = System.Windows.Forms.DockStyle.Fill;
            this.rbn_SetMode.Location = new System.Drawing.Point(181, 4);
            this.rbn_SetMode.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.rbn_SetMode.Name = "rbn_SetMode";
            this.rbn_SetMode.Size = new System.Drawing.Size(169, 44);
            this.rbn_SetMode.TabIndex = 1;
            this.rbn_SetMode.Text = "Setting Mode";
            this.rbn_SetMode.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.rbn_SetMode.UseVisualStyleBackColor = true;
            // 
            // rbn_RunMode
            // 
            this.rbn_RunMode.Appearance = System.Windows.Forms.Appearance.Button;
            this.rbn_RunMode.AutoSize = true;
            this.rbn_RunMode.Checked = true;
            this.rbn_RunMode.Dock = System.Windows.Forms.DockStyle.Fill;
            this.rbn_RunMode.Location = new System.Drawing.Point(4, 4);
            this.rbn_RunMode.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.rbn_RunMode.Name = "rbn_RunMode";
            this.rbn_RunMode.Size = new System.Drawing.Size(169, 44);
            this.rbn_RunMode.TabIndex = 0;
            this.rbn_RunMode.TabStop = true;
            this.rbn_RunMode.Text = "Run Mode";
            this.rbn_RunMode.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.rbn_RunMode.UseVisualStyleBackColor = true;
            this.rbn_RunMode.CheckedChanged += new System.EventHandler(this.rbn_RunMode_CheckedChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label1.Location = new System.Drawing.Point(4, 104);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(169, 52);
            this.label1.TabIndex = 19;
            this.label1.Text = "x8000 Setting #";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // cmb_x8000Number
            // 
            this.cmb_x8000Number.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cmb_x8000Number.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmb_x8000Number.Font = new System.Drawing.Font("新細明體", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.cmb_x8000Number.FormattingEnabled = true;
            this.cmb_x8000Number.Location = new System.Drawing.Point(181, 108);
            this.cmb_x8000Number.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.cmb_x8000Number.Name = "cmb_x8000Number";
            this.cmb_x8000Number.Size = new System.Drawing.Size(169, 37);
            this.cmb_x8000Number.TabIndex = 20;
            // 
            // btn_ReadX8000SettingNum
            // 
            this.btn_ReadX8000SettingNum.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btn_ReadX8000SettingNum.Location = new System.Drawing.Point(358, 108);
            this.btn_ReadX8000SettingNum.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btn_ReadX8000SettingNum.Name = "btn_ReadX8000SettingNum";
            this.btn_ReadX8000SettingNum.Size = new System.Drawing.Size(169, 44);
            this.btn_ReadX8000SettingNum.TabIndex = 21;
            this.btn_ReadX8000SettingNum.Text = "Read";
            this.btn_ReadX8000SettingNum.UseVisualStyleBackColor = true;
            this.btn_ReadX8000SettingNum.Click += new System.EventHandler(this.btn_ReadX8000SettingNum_Click);
            // 
            // btn_WriteX8000SettingNum
            // 
            this.btn_WriteX8000SettingNum.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btn_WriteX8000SettingNum.Location = new System.Drawing.Point(535, 108);
            this.btn_WriteX8000SettingNum.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btn_WriteX8000SettingNum.Name = "btn_WriteX8000SettingNum";
            this.btn_WriteX8000SettingNum.Size = new System.Drawing.Size(171, 44);
            this.btn_WriteX8000SettingNum.TabIndex = 22;
            this.btn_WriteX8000SettingNum.Text = "Write";
            this.btn_WriteX8000SettingNum.UseVisualStyleBackColor = true;
            this.btn_WriteX8000SettingNum.Click += new System.EventHandler(this.btn_WriteX8000SettingNum_Click);
            // 
            // richTextBox1
            // 
            this.tableLayoutPanel2.SetColumnSpan(this.richTextBox1, 2);
            this.richTextBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.richTextBox1.Location = new System.Drawing.Point(358, 4);
            this.richTextBox1.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.richTextBox1.Name = "richTextBox1";
            this.tableLayoutPanel2.SetRowSpan(this.richTextBox1, 2);
            this.richTextBox1.Size = new System.Drawing.Size(348, 96);
            this.richTextBox1.TabIndex = 24;
            this.richTextBox1.Text = "";
            // 
            // X8000Control
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tableLayoutPanel1);
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.Name = "X8000Control";
            this.Size = new System.Drawing.Size(718, 240);
            this.Load += new System.EventHandler(this.X8000Control_Load);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.tableLayoutPanel2.ResumeLayout(false);
            this.tableLayoutPanel2.PerformLayout();
            this.tableLayoutPanel3.ResumeLayout(false);
            this.tableLayoutPanel3.PerformLayout();
            this.tableLayoutPanel4.ResumeLayout(false);
            this.tableLayoutPanel4.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripLabel toolStripLabel1;
        private System.Windows.Forms.ToolStripTextBox tbx_IP;
        private System.Windows.Forms.ToolStripLabel toolStripLabel2;
        private System.Windows.Forms.ToolStripTextBox tbx_Port;
        private System.Windows.Forms.ToolStripButton btn_Connect;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel3;
        private System.Windows.Forms.Button btn_Trigger;
        private System.Windows.Forms.CheckBox chbx_TriggerEnable;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel4;
        private System.Windows.Forms.RadioButton rbn_SetMode;
        private System.Windows.Forms.RadioButton rbn_RunMode;
        private System.Windows.Forms.ToolStripDropDownButton toolStripDropDownButton1;
        private System.Windows.Forms.ToolStripMenuItem mim_Reset;
        private System.Windows.Forms.ToolStripMenuItem mim_Reboot;
        private System.Windows.Forms.ToolStripMenuItem mim_ClearError;
        private System.Windows.Forms.ToolStripMenuItem mim_SaveScreenPrint;
        private System.Windows.Forms.ToolStripMenuItem mim_X8kVersion;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cmb_x8000Number;
        private System.Windows.Forms.Button btn_ReadX8000SettingNum;
        private System.Windows.Forms.Button btn_WriteX8000SettingNum;
        private System.Windows.Forms.ToolStripMenuItem mim_ReadX8000Time;
        private System.Windows.Forms.RichTextBox richTextBox1;
        private System.Windows.Forms.ToolStripMenuItem setX8000TimeToolStripMenuItem;
    }
}

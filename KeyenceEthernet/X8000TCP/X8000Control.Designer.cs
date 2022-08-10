
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
            resources.ApplyResources(this.tableLayoutPanel1, "tableLayoutPanel1");
            this.tableLayoutPanel1.Controls.Add(this.toolStrip1, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.tableLayoutPanel2, 0, 1);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            // 
            // toolStrip1
            // 
            resources.ApplyResources(this.toolStrip1, "toolStrip1");
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
            this.toolStrip1.Name = "toolStrip1";
            // 
            // toolStripLabel1
            // 
            this.toolStripLabel1.Name = "toolStripLabel1";
            resources.ApplyResources(this.toolStripLabel1, "toolStripLabel1");
            // 
            // tbx_IP
            // 
            resources.ApplyResources(this.tbx_IP, "tbx_IP");
            this.tbx_IP.Name = "tbx_IP";
            // 
            // toolStripLabel2
            // 
            this.toolStripLabel2.Name = "toolStripLabel2";
            resources.ApplyResources(this.toolStripLabel2, "toolStripLabel2");
            // 
            // tbx_Port
            // 
            resources.ApplyResources(this.tbx_Port, "tbx_Port");
            this.tbx_Port.Name = "tbx_Port";
            // 
            // btn_Connect
            // 
            resources.ApplyResources(this.btn_Connect, "btn_Connect");
            this.btn_Connect.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btn_Connect.Image = global::RsLib.X8000TCP.Properties.Resources.icons8_link_64;
            this.btn_Connect.Name = "btn_Connect";
            this.btn_Connect.Click += new System.EventHandler(this.btn_Connect_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            resources.ApplyResources(this.toolStripSeparator1, "toolStripSeparator1");
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
            resources.ApplyResources(this.toolStripDropDownButton1, "toolStripDropDownButton1");
            this.toolStripDropDownButton1.Name = "toolStripDropDownButton1";
            this.toolStripDropDownButton1.Click += new System.EventHandler(this.toolStripDropDownButton1_Click);
            // 
            // mim_Reset
            // 
            this.mim_Reset.Name = "mim_Reset";
            resources.ApplyResources(this.mim_Reset, "mim_Reset");
            this.mim_Reset.Click += new System.EventHandler(this.mim_Reset_Click);
            // 
            // mim_Reboot
            // 
            this.mim_Reboot.Name = "mim_Reboot";
            resources.ApplyResources(this.mim_Reboot, "mim_Reboot");
            this.mim_Reboot.Click += new System.EventHandler(this.mim_Reboot_Click);
            // 
            // mim_ClearError
            // 
            this.mim_ClearError.Name = "mim_ClearError";
            resources.ApplyResources(this.mim_ClearError, "mim_ClearError");
            this.mim_ClearError.Click += new System.EventHandler(this.mim_ClearError_Click);
            // 
            // mim_SaveScreenPrint
            // 
            this.mim_SaveScreenPrint.Name = "mim_SaveScreenPrint";
            resources.ApplyResources(this.mim_SaveScreenPrint, "mim_SaveScreenPrint");
            this.mim_SaveScreenPrint.Click += new System.EventHandler(this.mim_SaveScreenPrint_Click);
            // 
            // mim_X8kVersion
            // 
            this.mim_X8kVersion.Name = "mim_X8kVersion";
            resources.ApplyResources(this.mim_X8kVersion, "mim_X8kVersion");
            this.mim_X8kVersion.Click += new System.EventHandler(this.mim_X8kVersion_Click);
            // 
            // mim_ReadX8000Time
            // 
            this.mim_ReadX8000Time.Name = "mim_ReadX8000Time";
            resources.ApplyResources(this.mim_ReadX8000Time, "mim_ReadX8000Time");
            this.mim_ReadX8000Time.Click += new System.EventHandler(this.mim_ReadX8000Time_Click);
            // 
            // setX8000TimeToolStripMenuItem
            // 
            this.setX8000TimeToolStripMenuItem.Name = "setX8000TimeToolStripMenuItem";
            resources.ApplyResources(this.setX8000TimeToolStripMenuItem, "setX8000TimeToolStripMenuItem");
            this.setX8000TimeToolStripMenuItem.Click += new System.EventHandler(this.setX8000TimeToolStripMenuItem_Click);
            // 
            // tableLayoutPanel2
            // 
            resources.ApplyResources(this.tableLayoutPanel2, "tableLayoutPanel2");
            this.tableLayoutPanel2.Controls.Add(this.tableLayoutPanel3, 0, 0);
            this.tableLayoutPanel2.Controls.Add(this.tableLayoutPanel4, 0, 1);
            this.tableLayoutPanel2.Controls.Add(this.label1, 0, 2);
            this.tableLayoutPanel2.Controls.Add(this.cmb_x8000Number, 1, 2);
            this.tableLayoutPanel2.Controls.Add(this.btn_ReadX8000SettingNum, 2, 2);
            this.tableLayoutPanel2.Controls.Add(this.btn_WriteX8000SettingNum, 3, 2);
            this.tableLayoutPanel2.Controls.Add(this.richTextBox1, 2, 0);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            // 
            // tableLayoutPanel3
            // 
            resources.ApplyResources(this.tableLayoutPanel3, "tableLayoutPanel3");
            this.tableLayoutPanel2.SetColumnSpan(this.tableLayoutPanel3, 2);
            this.tableLayoutPanel3.Controls.Add(this.btn_Trigger, 1, 0);
            this.tableLayoutPanel3.Controls.Add(this.chbx_TriggerEnable, 0, 0);
            this.tableLayoutPanel3.Name = "tableLayoutPanel3";
            // 
            // btn_Trigger
            // 
            resources.ApplyResources(this.btn_Trigger, "btn_Trigger");
            this.btn_Trigger.Name = "btn_Trigger";
            this.btn_Trigger.UseVisualStyleBackColor = true;
            this.btn_Trigger.Click += new System.EventHandler(this.btn_Trigger_Click);
            // 
            // chbx_TriggerEnable
            // 
            resources.ApplyResources(this.chbx_TriggerEnable, "chbx_TriggerEnable");
            this.chbx_TriggerEnable.Checked = true;
            this.chbx_TriggerEnable.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chbx_TriggerEnable.Name = "chbx_TriggerEnable";
            this.chbx_TriggerEnable.UseVisualStyleBackColor = true;
            this.chbx_TriggerEnable.CheckedChanged += new System.EventHandler(this.chbx_TriggerEnable_CheckedChanged);
            // 
            // tableLayoutPanel4
            // 
            resources.ApplyResources(this.tableLayoutPanel4, "tableLayoutPanel4");
            this.tableLayoutPanel2.SetColumnSpan(this.tableLayoutPanel4, 2);
            this.tableLayoutPanel4.Controls.Add(this.rbn_SetMode, 1, 0);
            this.tableLayoutPanel4.Controls.Add(this.rbn_RunMode, 0, 0);
            this.tableLayoutPanel4.Name = "tableLayoutPanel4";
            // 
            // rbn_SetMode
            // 
            resources.ApplyResources(this.rbn_SetMode, "rbn_SetMode");
            this.rbn_SetMode.Name = "rbn_SetMode";
            this.rbn_SetMode.UseVisualStyleBackColor = true;
            // 
            // rbn_RunMode
            // 
            resources.ApplyResources(this.rbn_RunMode, "rbn_RunMode");
            this.rbn_RunMode.Checked = true;
            this.rbn_RunMode.Name = "rbn_RunMode";
            this.rbn_RunMode.TabStop = true;
            this.rbn_RunMode.UseVisualStyleBackColor = true;
            this.rbn_RunMode.CheckedChanged += new System.EventHandler(this.rbn_RunMode_CheckedChanged);
            // 
            // label1
            // 
            resources.ApplyResources(this.label1, "label1");
            this.label1.Name = "label1";
            // 
            // cmb_x8000Number
            // 
            resources.ApplyResources(this.cmb_x8000Number, "cmb_x8000Number");
            this.cmb_x8000Number.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmb_x8000Number.FormattingEnabled = true;
            this.cmb_x8000Number.Name = "cmb_x8000Number";
            // 
            // btn_ReadX8000SettingNum
            // 
            resources.ApplyResources(this.btn_ReadX8000SettingNum, "btn_ReadX8000SettingNum");
            this.btn_ReadX8000SettingNum.Name = "btn_ReadX8000SettingNum";
            this.btn_ReadX8000SettingNum.UseVisualStyleBackColor = true;
            this.btn_ReadX8000SettingNum.Click += new System.EventHandler(this.btn_ReadX8000SettingNum_Click);
            // 
            // btn_WriteX8000SettingNum
            // 
            resources.ApplyResources(this.btn_WriteX8000SettingNum, "btn_WriteX8000SettingNum");
            this.btn_WriteX8000SettingNum.Name = "btn_WriteX8000SettingNum";
            this.btn_WriteX8000SettingNum.UseVisualStyleBackColor = true;
            this.btn_WriteX8000SettingNum.Click += new System.EventHandler(this.btn_WriteX8000SettingNum_Click);
            // 
            // richTextBox1
            // 
            this.tableLayoutPanel2.SetColumnSpan(this.richTextBox1, 2);
            resources.ApplyResources(this.richTextBox1, "richTextBox1");
            this.richTextBox1.Name = "richTextBox1";
            this.tableLayoutPanel2.SetRowSpan(this.richTextBox1, 2);
            // 
            // X8000Control
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tableLayoutPanel1);
            this.Name = "X8000Control";
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

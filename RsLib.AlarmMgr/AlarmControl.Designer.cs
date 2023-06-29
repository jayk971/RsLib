
namespace RsLib.AlarmMgr
{
    partial class AlarmControl
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AlarmControl));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle9 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle7 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle8 = new System.Windows.Forms.DataGridViewCellStyle();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.dgvAlarmRealTime = new System.Windows.Forms.DataGridView();
            this.dgvAlarmRealTimeColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgvAlarmRealTimeColumn2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgvAlarmRealTimeColumn6 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgvAlarmRealTimeColumn3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgvAlarmRealTimeColumn4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgvAlarmRealTimeColumn5 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgvAlarmRealTimeColumn7 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.btn_ResetAlarm = new System.Windows.Forms.ToolStripButton();
            this.tableLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvAlarmRealTime)).BeginInit();
            this.toolStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            resources.ApplyResources(this.tableLayoutPanel1, "tableLayoutPanel1");
            this.tableLayoutPanel1.Controls.Add(this.dgvAlarmRealTime, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.toolStrip1, 0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            // 
            // dgvAlarmRealTime
            // 
            resources.ApplyResources(this.dgvAlarmRealTime, "dgvAlarmRealTime");
            this.dgvAlarmRealTime.AllowUserToAddRows = false;
            this.dgvAlarmRealTime.AllowUserToDeleteRows = false;
            this.dgvAlarmRealTime.AllowUserToResizeRows = false;
            this.dgvAlarmRealTime.BackgroundColor = System.Drawing.Color.White;
            this.dgvAlarmRealTime.BorderStyle = System.Windows.Forms.BorderStyle.None;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("微軟正黑體", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvAlarmRealTime.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dgvAlarmRealTime.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvAlarmRealTime.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dgvAlarmRealTimeColumn1,
            this.dgvAlarmRealTimeColumn2,
            this.dgvAlarmRealTimeColumn6,
            this.dgvAlarmRealTimeColumn3,
            this.dgvAlarmRealTimeColumn4,
            this.dgvAlarmRealTimeColumn5,
            this.dgvAlarmRealTimeColumn7});
            dataGridViewCellStyle9.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle9.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle9.Font = new System.Drawing.Font("微軟正黑體", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            dataGridViewCellStyle9.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle9.SelectionBackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle9.SelectionForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle9.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvAlarmRealTime.DefaultCellStyle = dataGridViewCellStyle9;
            this.dgvAlarmRealTime.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically;
            this.dgvAlarmRealTime.Name = "dgvAlarmRealTime";
            this.dgvAlarmRealTime.ReadOnly = true;
            this.dgvAlarmRealTime.RowHeadersVisible = false;
            this.dgvAlarmRealTime.RowTemplate.Height = 24;
            this.dgvAlarmRealTime.TabStop = false;
            // 
            // dgvAlarmRealTimeColumn1
            // 
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.dgvAlarmRealTimeColumn1.DefaultCellStyle = dataGridViewCellStyle2;
            resources.ApplyResources(this.dgvAlarmRealTimeColumn1, "dgvAlarmRealTimeColumn1");
            this.dgvAlarmRealTimeColumn1.Name = "dgvAlarmRealTimeColumn1";
            this.dgvAlarmRealTimeColumn1.ReadOnly = true;
            // 
            // dgvAlarmRealTimeColumn2
            // 
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.dgvAlarmRealTimeColumn2.DefaultCellStyle = dataGridViewCellStyle3;
            resources.ApplyResources(this.dgvAlarmRealTimeColumn2, "dgvAlarmRealTimeColumn2");
            this.dgvAlarmRealTimeColumn2.Name = "dgvAlarmRealTimeColumn2";
            this.dgvAlarmRealTimeColumn2.ReadOnly = true;
            // 
            // dgvAlarmRealTimeColumn6
            // 
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.dgvAlarmRealTimeColumn6.DefaultCellStyle = dataGridViewCellStyle4;
            resources.ApplyResources(this.dgvAlarmRealTimeColumn6, "dgvAlarmRealTimeColumn6");
            this.dgvAlarmRealTimeColumn6.Name = "dgvAlarmRealTimeColumn6";
            this.dgvAlarmRealTimeColumn6.ReadOnly = true;
            // 
            // dgvAlarmRealTimeColumn3
            // 
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.dgvAlarmRealTimeColumn3.DefaultCellStyle = dataGridViewCellStyle5;
            resources.ApplyResources(this.dgvAlarmRealTimeColumn3, "dgvAlarmRealTimeColumn3");
            this.dgvAlarmRealTimeColumn3.Name = "dgvAlarmRealTimeColumn3";
            this.dgvAlarmRealTimeColumn3.ReadOnly = true;
            // 
            // dgvAlarmRealTimeColumn4
            // 
            dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            this.dgvAlarmRealTimeColumn4.DefaultCellStyle = dataGridViewCellStyle6;
            resources.ApplyResources(this.dgvAlarmRealTimeColumn4, "dgvAlarmRealTimeColumn4");
            this.dgvAlarmRealTimeColumn4.Name = "dgvAlarmRealTimeColumn4";
            this.dgvAlarmRealTimeColumn4.ReadOnly = true;
            // 
            // dgvAlarmRealTimeColumn5
            // 
            dataGridViewCellStyle7.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            this.dgvAlarmRealTimeColumn5.DefaultCellStyle = dataGridViewCellStyle7;
            resources.ApplyResources(this.dgvAlarmRealTimeColumn5, "dgvAlarmRealTimeColumn5");
            this.dgvAlarmRealTimeColumn5.Name = "dgvAlarmRealTimeColumn5";
            this.dgvAlarmRealTimeColumn5.ReadOnly = true;
            // 
            // dgvAlarmRealTimeColumn7
            // 
            dataGridViewCellStyle8.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            this.dgvAlarmRealTimeColumn7.DefaultCellStyle = dataGridViewCellStyle8;
            resources.ApplyResources(this.dgvAlarmRealTimeColumn7, "dgvAlarmRealTimeColumn7");
            this.dgvAlarmRealTimeColumn7.Name = "dgvAlarmRealTimeColumn7";
            this.dgvAlarmRealTimeColumn7.ReadOnly = true;
            // 
            // toolStrip1
            // 
            resources.ApplyResources(this.toolStrip1, "toolStrip1");
            this.toolStrip1.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.toolStrip1.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btn_ResetAlarm,
            this.toolStripSeparator1});
            this.toolStrip1.Name = "toolStrip1";
            // 
            // toolStripSeparator1
            // 
            resources.ApplyResources(this.toolStripSeparator1, "toolStripSeparator1");
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            // 
            // btn_ResetAlarm
            // 
            resources.ApplyResources(this.btn_ResetAlarm, "btn_ResetAlarm");
            this.btn_ResetAlarm.BackgroundImage = global::RsLib.AlarmMgr.Properties.Resources.icons8_broom_64;
            this.btn_ResetAlarm.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.btn_ResetAlarm.Name = "btn_ResetAlarm";
            this.btn_ResetAlarm.Click += new System.EventHandler(this.btn_ResetAlarm_Click);
            // 
            // AlarmControl
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tableLayoutPanel1);
            this.Name = "AlarmControl";
            this.Load += new System.EventHandler(this.AlarmControl_Load);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvAlarmRealTime)).EndInit();
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.DataGridView dgvAlarmRealTime;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton btn_ResetAlarm;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgvAlarmRealTimeColumn1;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgvAlarmRealTimeColumn2;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgvAlarmRealTimeColumn6;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgvAlarmRealTimeColumn3;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgvAlarmRealTimeColumn4;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgvAlarmRealTimeColumn5;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgvAlarmRealTimeColumn7;
    }
}

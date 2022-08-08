
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle10 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle18 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle11 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle12 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle13 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle14 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle15 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle16 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle17 = new System.Windows.Forms.DataGridViewCellStyle();
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
            this.btn_ResetAlarm = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.tableLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvAlarmRealTime)).BeginInit();
            this.toolStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 91.79936F));
            this.tableLayoutPanel1.Controls.Add(this.dgvAlarmRealTime, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.toolStrip1, 0, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Margin = new System.Windows.Forms.Padding(4, 6, 4, 6);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 45F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(1500, 698);
            this.tableLayoutPanel1.TabIndex = 23;
            // 
            // dgvAlarmRealTime
            // 
            this.dgvAlarmRealTime.AllowUserToAddRows = false;
            this.dgvAlarmRealTime.AllowUserToDeleteRows = false;
            this.dgvAlarmRealTime.AllowUserToResizeRows = false;
            this.dgvAlarmRealTime.BackgroundColor = System.Drawing.Color.White;
            this.dgvAlarmRealTime.BorderStyle = System.Windows.Forms.BorderStyle.None;
            dataGridViewCellStyle10.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle10.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle10.Font = new System.Drawing.Font("微軟正黑體", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            dataGridViewCellStyle10.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle10.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle10.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle10.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvAlarmRealTime.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle10;
            this.dgvAlarmRealTime.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvAlarmRealTime.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dgvAlarmRealTimeColumn1,
            this.dgvAlarmRealTimeColumn2,
            this.dgvAlarmRealTimeColumn6,
            this.dgvAlarmRealTimeColumn3,
            this.dgvAlarmRealTimeColumn4,
            this.dgvAlarmRealTimeColumn5,
            this.dgvAlarmRealTimeColumn7});
            dataGridViewCellStyle18.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle18.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle18.Font = new System.Drawing.Font("微軟正黑體", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            dataGridViewCellStyle18.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle18.SelectionBackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle18.SelectionForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle18.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvAlarmRealTime.DefaultCellStyle = dataGridViewCellStyle18;
            this.dgvAlarmRealTime.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvAlarmRealTime.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically;
            this.dgvAlarmRealTime.Location = new System.Drawing.Point(4, 49);
            this.dgvAlarmRealTime.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.dgvAlarmRealTime.Name = "dgvAlarmRealTime";
            this.dgvAlarmRealTime.ReadOnly = true;
            this.dgvAlarmRealTime.RowHeadersVisible = false;
            this.dgvAlarmRealTime.RowHeadersWidth = 62;
            this.dgvAlarmRealTime.RowTemplate.Height = 24;
            this.dgvAlarmRealTime.Size = new System.Drawing.Size(1492, 645);
            this.dgvAlarmRealTime.TabIndex = 19;
            this.dgvAlarmRealTime.TabStop = false;
            // 
            // dgvAlarmRealTimeColumn1
            // 
            dataGridViewCellStyle11.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.dgvAlarmRealTimeColumn1.DefaultCellStyle = dataGridViewCellStyle11;
            this.dgvAlarmRealTimeColumn1.HeaderText = "Time";
            this.dgvAlarmRealTimeColumn1.MinimumWidth = 8;
            this.dgvAlarmRealTimeColumn1.Name = "dgvAlarmRealTimeColumn1";
            this.dgvAlarmRealTimeColumn1.ReadOnly = true;
            this.dgvAlarmRealTimeColumn1.Width = 150;
            // 
            // dgvAlarmRealTimeColumn2
            // 
            dataGridViewCellStyle12.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.dgvAlarmRealTimeColumn2.DefaultCellStyle = dataGridViewCellStyle12;
            this.dgvAlarmRealTimeColumn2.HeaderText = "Level";
            this.dgvAlarmRealTimeColumn2.MinimumWidth = 8;
            this.dgvAlarmRealTimeColumn2.Name = "dgvAlarmRealTimeColumn2";
            this.dgvAlarmRealTimeColumn2.ReadOnly = true;
            this.dgvAlarmRealTimeColumn2.Width = 150;
            // 
            // dgvAlarmRealTimeColumn6
            // 
            dataGridViewCellStyle13.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.dgvAlarmRealTimeColumn6.DefaultCellStyle = dataGridViewCellStyle13;
            this.dgvAlarmRealTimeColumn6.HeaderText = "Code";
            this.dgvAlarmRealTimeColumn6.MinimumWidth = 8;
            this.dgvAlarmRealTimeColumn6.Name = "dgvAlarmRealTimeColumn6";
            this.dgvAlarmRealTimeColumn6.ReadOnly = true;
            this.dgvAlarmRealTimeColumn6.Width = 70;
            // 
            // dgvAlarmRealTimeColumn3
            // 
            dataGridViewCellStyle14.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.dgvAlarmRealTimeColumn3.DefaultCellStyle = dataGridViewCellStyle14;
            this.dgvAlarmRealTimeColumn3.HeaderText = "Description";
            this.dgvAlarmRealTimeColumn3.MinimumWidth = 8;
            this.dgvAlarmRealTimeColumn3.Name = "dgvAlarmRealTimeColumn3";
            this.dgvAlarmRealTimeColumn3.ReadOnly = true;
            this.dgvAlarmRealTimeColumn3.Width = 200;
            // 
            // dgvAlarmRealTimeColumn4
            // 
            dataGridViewCellStyle15.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            this.dgvAlarmRealTimeColumn4.DefaultCellStyle = dataGridViewCellStyle15;
            this.dgvAlarmRealTimeColumn4.HeaderText = "Reason";
            this.dgvAlarmRealTimeColumn4.MinimumWidth = 8;
            this.dgvAlarmRealTimeColumn4.Name = "dgvAlarmRealTimeColumn4";
            this.dgvAlarmRealTimeColumn4.ReadOnly = true;
            this.dgvAlarmRealTimeColumn4.Width = 200;
            // 
            // dgvAlarmRealTimeColumn5
            // 
            dataGridViewCellStyle16.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            this.dgvAlarmRealTimeColumn5.DefaultCellStyle = dataGridViewCellStyle16;
            this.dgvAlarmRealTimeColumn5.HeaderText = "Remedy";
            this.dgvAlarmRealTimeColumn5.MinimumWidth = 8;
            this.dgvAlarmRealTimeColumn5.Name = "dgvAlarmRealTimeColumn5";
            this.dgvAlarmRealTimeColumn5.ReadOnly = true;
            this.dgvAlarmRealTimeColumn5.Width = 300;
            // 
            // dgvAlarmRealTimeColumn7
            // 
            dataGridViewCellStyle17.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            this.dgvAlarmRealTimeColumn7.DefaultCellStyle = dataGridViewCellStyle17;
            this.dgvAlarmRealTimeColumn7.HeaderText = "Note";
            this.dgvAlarmRealTimeColumn7.MinimumWidth = 8;
            this.dgvAlarmRealTimeColumn7.Name = "dgvAlarmRealTimeColumn7";
            this.dgvAlarmRealTimeColumn7.ReadOnly = true;
            this.dgvAlarmRealTimeColumn7.Width = 470;
            // 
            // toolStrip1
            // 
            this.toolStrip1.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.toolStrip1.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btn_ResetAlarm,
            this.toolStripSeparator1});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Padding = new System.Windows.Forms.Padding(0, 0, 3, 0);
            this.toolStrip1.Size = new System.Drawing.Size(1500, 38);
            this.toolStrip1.TabIndex = 20;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // btn_ResetAlarm
            // 
            this.btn_ResetAlarm.BackgroundImage = global::RsLib.AlarmMgr.Properties.Resources.icons8_broom_64;
            this.btn_ResetAlarm.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btn_ResetAlarm.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.btn_ResetAlarm.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btn_ResetAlarm.Name = "btn_ResetAlarm";
            this.btn_ResetAlarm.Size = new System.Drawing.Size(34, 33);
            this.btn_ResetAlarm.Click += new System.EventHandler(this.btn_ResetAlarm_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 38);
            // 
            // AlarmControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tableLayoutPanel1);
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.Name = "AlarmControl";
            this.Size = new System.Drawing.Size(1500, 698);
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

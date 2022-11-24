
namespace RsLib.AlarmMgr
{
    partial class AlarmBriefInfoControl
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AlarmBriefInfoControl));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle7 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            this.dgvAlarmBriefInfo = new System.Windows.Forms.DataGridView();
            this.dgvSystemInfoColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgvSystemInfoColumn2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgvSystemInfoColumn3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgvSystemInfoColumn4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.dgvAlarmBriefInfo)).BeginInit();
            this.SuspendLayout();
            // 
            // dgvAlarmBriefInfo
            // 
            resources.ApplyResources(this.dgvAlarmBriefInfo, "dgvAlarmBriefInfo");
            this.dgvAlarmBriefInfo.AllowUserToAddRows = false;
            this.dgvAlarmBriefInfo.AllowUserToDeleteRows = false;
            this.dgvAlarmBriefInfo.AllowUserToResizeRows = false;
            this.dgvAlarmBriefInfo.BackgroundColor = System.Drawing.Color.White;
            this.dgvAlarmBriefInfo.BorderStyle = System.Windows.Forms.BorderStyle.None;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("微軟正黑體", 9F, System.Drawing.FontStyle.Bold);
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvAlarmBriefInfo.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dgvAlarmBriefInfo.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvAlarmBriefInfo.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dgvSystemInfoColumn1,
            this.dgvSystemInfoColumn2,
            this.Column1,
            this.dgvSystemInfoColumn3,
            this.dgvSystemInfoColumn4,
            this.Column2});
            dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle6.BackColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle6.Font = new System.Drawing.Font("微軟正黑體", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            dataGridViewCellStyle6.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle6.SelectionBackColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle6.SelectionForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle6.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvAlarmBriefInfo.DefaultCellStyle = dataGridViewCellStyle6;
            this.dgvAlarmBriefInfo.EnableHeadersVisualStyles = false;
            this.dgvAlarmBriefInfo.Name = "dgvAlarmBriefInfo";
            this.dgvAlarmBriefInfo.ReadOnly = true;
            dataGridViewCellStyle7.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle7.BackColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle7.Font = new System.Drawing.Font("微軟正黑體", 9F, System.Drawing.FontStyle.Bold);
            dataGridViewCellStyle7.ForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle7.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle7.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle7.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvAlarmBriefInfo.RowHeadersDefaultCellStyle = dataGridViewCellStyle7;
            this.dgvAlarmBriefInfo.RowHeadersVisible = false;
            this.dgvAlarmBriefInfo.RowTemplate.Height = 24;
            this.dgvAlarmBriefInfo.TabStop = false;
            this.dgvAlarmBriefInfo.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvAlarmBriefInfo_CellContentClick);
            // 
            // dgvSystemInfoColumn1
            // 
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.dgvSystemInfoColumn1.DefaultCellStyle = dataGridViewCellStyle2;
            this.dgvSystemInfoColumn1.Frozen = true;
            resources.ApplyResources(this.dgvSystemInfoColumn1, "dgvSystemInfoColumn1");
            this.dgvSystemInfoColumn1.Name = "dgvSystemInfoColumn1";
            this.dgvSystemInfoColumn1.ReadOnly = true;
            // 
            // dgvSystemInfoColumn2
            // 
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.dgvSystemInfoColumn2.DefaultCellStyle = dataGridViewCellStyle3;
            resources.ApplyResources(this.dgvSystemInfoColumn2, "dgvSystemInfoColumn2");
            this.dgvSystemInfoColumn2.Name = "dgvSystemInfoColumn2";
            this.dgvSystemInfoColumn2.ReadOnly = true;
            // 
            // Column1
            // 
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.Column1.DefaultCellStyle = dataGridViewCellStyle4;
            resources.ApplyResources(this.Column1, "Column1");
            this.Column1.Name = "Column1";
            this.Column1.ReadOnly = true;
            // 
            // dgvSystemInfoColumn3
            // 
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            this.dgvSystemInfoColumn3.DefaultCellStyle = dataGridViewCellStyle5;
            resources.ApplyResources(this.dgvSystemInfoColumn3, "dgvSystemInfoColumn3");
            this.dgvSystemInfoColumn3.Name = "dgvSystemInfoColumn3";
            this.dgvSystemInfoColumn3.ReadOnly = true;
            // 
            // dgvSystemInfoColumn4
            // 
            resources.ApplyResources(this.dgvSystemInfoColumn4, "dgvSystemInfoColumn4");
            this.dgvSystemInfoColumn4.Name = "dgvSystemInfoColumn4";
            this.dgvSystemInfoColumn4.ReadOnly = true;
            // 
            // Column2
            // 
            resources.ApplyResources(this.Column2, "Column2");
            this.Column2.Name = "Column2";
            this.Column2.ReadOnly = true;
            // 
            // AlarmBriefInfoControl
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.dgvAlarmBriefInfo);
            this.Name = "AlarmBriefInfoControl";
            this.Load += new System.EventHandler(this.AlarmBriefInfoControl_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvAlarmBriefInfo)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dgvAlarmBriefInfo;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgvSystemInfoColumn1;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgvSystemInfoColumn2;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column1;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgvSystemInfoColumn3;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgvSystemInfoColumn4;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column2;
    }
}

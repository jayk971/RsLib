
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle8 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle13 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle14 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle9 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle10 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle11 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle12 = new System.Windows.Forms.DataGridViewCellStyle();
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
            dataGridViewCellStyle8.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle8.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle8.Font = new System.Drawing.Font("微軟正黑體", 9F, System.Drawing.FontStyle.Bold);
            dataGridViewCellStyle8.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle8.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle8.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle8.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvAlarmBriefInfo.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle8;
            this.dgvAlarmBriefInfo.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvAlarmBriefInfo.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dgvSystemInfoColumn1,
            this.dgvSystemInfoColumn2,
            this.Column1,
            this.dgvSystemInfoColumn3,
            this.dgvSystemInfoColumn4,
            this.Column2});
            dataGridViewCellStyle13.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle13.BackColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle13.Font = new System.Drawing.Font("微軟正黑體", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            dataGridViewCellStyle13.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle13.SelectionBackColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle13.SelectionForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle13.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvAlarmBriefInfo.DefaultCellStyle = dataGridViewCellStyle13;
            this.dgvAlarmBriefInfo.EnableHeadersVisualStyles = false;
            this.dgvAlarmBriefInfo.Name = "dgvAlarmBriefInfo";
            this.dgvAlarmBriefInfo.ReadOnly = true;
            dataGridViewCellStyle14.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle14.BackColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle14.Font = new System.Drawing.Font("微軟正黑體", 9F, System.Drawing.FontStyle.Bold);
            dataGridViewCellStyle14.ForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle14.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle14.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle14.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvAlarmBriefInfo.RowHeadersDefaultCellStyle = dataGridViewCellStyle14;
            this.dgvAlarmBriefInfo.RowHeadersVisible = false;
            this.dgvAlarmBriefInfo.RowTemplate.Height = 24;
            this.dgvAlarmBriefInfo.TabStop = false;
            this.dgvAlarmBriefInfo.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvAlarmBriefInfo_CellContentClick);
            // 
            // dgvSystemInfoColumn1
            // 
            dataGridViewCellStyle9.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.dgvSystemInfoColumn1.DefaultCellStyle = dataGridViewCellStyle9;
            this.dgvSystemInfoColumn1.Frozen = true;
            resources.ApplyResources(this.dgvSystemInfoColumn1, "dgvSystemInfoColumn1");
            this.dgvSystemInfoColumn1.Name = "dgvSystemInfoColumn1";
            this.dgvSystemInfoColumn1.ReadOnly = true;
            // 
            // dgvSystemInfoColumn2
            // 
            dataGridViewCellStyle10.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.dgvSystemInfoColumn2.DefaultCellStyle = dataGridViewCellStyle10;
            resources.ApplyResources(this.dgvSystemInfoColumn2, "dgvSystemInfoColumn2");
            this.dgvSystemInfoColumn2.Name = "dgvSystemInfoColumn2";
            this.dgvSystemInfoColumn2.ReadOnly = true;
            // 
            // Column1
            // 
            dataGridViewCellStyle11.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.Column1.DefaultCellStyle = dataGridViewCellStyle11;
            resources.ApplyResources(this.Column1, "Column1");
            this.Column1.Name = "Column1";
            this.Column1.ReadOnly = true;
            // 
            // dgvSystemInfoColumn3
            // 
            dataGridViewCellStyle12.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            this.dgvSystemInfoColumn3.DefaultCellStyle = dataGridViewCellStyle12;
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

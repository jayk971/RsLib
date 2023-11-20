
namespace RsLib.TCP.Control
{
    partial class TCPServerControl
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
            this.components = new System.ComponentModel.Container();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.btn_ServerStart = new System.Windows.Forms.ToolStripButton();
            this.btn_ServerStop = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripLabel1 = new System.Windows.Forms.ToolStripLabel();
            this.tbx_SendData = new System.Windows.Forms.ToolStripTextBox();
            this.cmb_ClientLIst = new System.Windows.Forms.ToolStripComboBox();
            this.btn_SendData = new System.Windows.Forms.ToolStripButton();
            this.btn_SendDataAll = new System.Windows.Forms.ToolStripButton();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.richTextBox1 = new System.Windows.Forms.RichTextBox();
            this.propertyGrid1 = new System.Windows.Forms.PropertyGrid();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.toolStripProgressBar1 = new System.Windows.Forms.ToolStripProgressBar();
            this.lbl_ClientCount = new System.Windows.Forms.ToolStripStatusLabel();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.pnl_OuterButton = new System.Windows.Forms.Panel();
            this.toolStrip1.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // toolStrip1
            // 
            this.tableLayoutPanel1.SetColumnSpan(this.toolStrip1, 2);
            this.toolStrip1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.toolStrip1.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btn_ServerStart,
            this.btn_ServerStop,
            this.toolStripSeparator1,
            this.toolStripLabel1,
            this.tbx_SendData,
            this.cmb_ClientLIst,
            this.btn_SendData,
            this.btn_SendDataAll});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(482, 25);
            this.toolStrip1.TabIndex = 0;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // btn_ServerStart
            // 
            this.btn_ServerStart.BackgroundImage = global::RsLib.TCP.Properties.Resources.play_64px;
            this.btn_ServerStart.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btn_ServerStart.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btn_ServerStart.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btn_ServerStart.Name = "btn_ServerStart";
            this.btn_ServerStart.Size = new System.Drawing.Size(23, 22);
            this.btn_ServerStart.Text = "Start TCP Server";
            this.btn_ServerStart.Click += new System.EventHandler(this.btn_ServerStart_Click);
            // 
            // btn_ServerStop
            // 
            this.btn_ServerStop.BackgroundImage = global::RsLib.TCP.Properties.Resources.stop_64px;
            this.btn_ServerStop.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btn_ServerStop.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btn_ServerStop.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btn_ServerStop.Name = "btn_ServerStop";
            this.btn_ServerStop.Size = new System.Drawing.Size(23, 22);
            this.btn_ServerStop.Text = "Stop TCP Server";
            this.btn_ServerStop.Click += new System.EventHandler(this.btn_ServerStop_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
            // 
            // toolStripLabel1
            // 
            this.toolStripLabel1.Name = "toolStripLabel1";
            this.toolStripLabel1.Size = new System.Drawing.Size(58, 22);
            this.toolStripLabel1.Text = "Message";
            // 
            // tbx_SendData
            // 
            this.tbx_SendData.BackColor = System.Drawing.Color.Silver;
            this.tbx_SendData.Font = new System.Drawing.Font("Microsoft JhengHei UI", 9F);
            this.tbx_SendData.Name = "tbx_SendData";
            this.tbx_SendData.Size = new System.Drawing.Size(100, 25);
            this.tbx_SendData.ToolTipText = "Message";
            // 
            // cmb_ClientLIst
            // 
            this.cmb_ClientLIst.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmb_ClientLIst.DropDownWidth = 80;
            this.cmb_ClientLIst.Name = "cmb_ClientLIst";
            this.cmb_ClientLIst.Size = new System.Drawing.Size(80, 25);
            this.cmb_ClientLIst.ToolTipText = "Client list";
            // 
            // btn_SendData
            // 
            this.btn_SendData.BackgroundImage = global::RsLib.TCP.Properties.Resources.send_64px;
            this.btn_SendData.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btn_SendData.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btn_SendData.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btn_SendData.Name = "btn_SendData";
            this.btn_SendData.Size = new System.Drawing.Size(23, 22);
            this.btn_SendData.Text = "Send data to client";
            this.btn_SendData.Click += new System.EventHandler(this.btn_SendData_Click);
            // 
            // btn_SendDataAll
            // 
            this.btn_SendDataAll.BackgroundImage = global::RsLib.TCP.Properties.Resources.send_email_64px;
            this.btn_SendDataAll.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btn_SendDataAll.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btn_SendDataAll.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btn_SendDataAll.Name = "btn_SendDataAll";
            this.btn_SendDataAll.Size = new System.Drawing.Size(23, 22);
            this.btn_SendDataAll.Text = "Send data to all client";
            this.btn_SendDataAll.Click += new System.EventHandler(this.btn_SendDataAll_Click);
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Controls.Add(this.richTextBox1, 1, 1);
            this.tableLayoutPanel1.Controls.Add(this.toolStrip1, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.statusStrip1, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.tableLayoutPanel2, 0, 1);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 3;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(482, 512);
            this.tableLayoutPanel1.TabIndex = 1;
            // 
            // richTextBox1
            // 
            this.richTextBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.richTextBox1.Location = new System.Drawing.Point(244, 28);
            this.richTextBox1.Name = "richTextBox1";
            this.richTextBox1.ReadOnly = true;
            this.richTextBox1.Size = new System.Drawing.Size(235, 461);
            this.richTextBox1.TabIndex = 0;
            this.richTextBox1.Text = "";
            // 
            // propertyGrid1
            // 
            this.propertyGrid1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.propertyGrid1.HelpVisible = false;
            this.propertyGrid1.Location = new System.Drawing.Point(3, 3);
            this.propertyGrid1.Name = "propertyGrid1";
            this.propertyGrid1.Size = new System.Drawing.Size(229, 165);
            this.propertyGrid1.TabIndex = 0;
            this.propertyGrid1.ToolbarVisible = false;
            this.propertyGrid1.PropertyValueChanged += new System.Windows.Forms.PropertyValueChangedEventHandler(this.propertyGrid1_PropertyValueChanged);
            // 
            // statusStrip1
            // 
            this.tableLayoutPanel1.SetColumnSpan(this.statusStrip1, 2);
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripProgressBar1,
            this.lbl_ClientCount});
            this.statusStrip1.Location = new System.Drawing.Point(0, 492);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(482, 20);
            this.statusStrip1.TabIndex = 2;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // toolStripProgressBar1
            // 
            this.toolStripProgressBar1.Name = "toolStripProgressBar1";
            this.toolStripProgressBar1.Size = new System.Drawing.Size(100, 14);
            // 
            // lbl_ClientCount
            // 
            this.lbl_ClientCount.Name = "lbl_ClientCount";
            this.lbl_ClientCount.Size = new System.Drawing.Size(14, 15);
            this.lbl_ClientCount.Text = "0";
            // 
            // timer1
            // 
            this.timer1.Enabled = true;
            this.timer1.Interval = 2000;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.ColumnCount = 1;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel2.Controls.Add(this.propertyGrid1, 0, 0);
            this.tableLayoutPanel2.Controls.Add(this.pnl_OuterButton, 0, 1);
            this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel2.Location = new System.Drawing.Point(3, 28);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 2;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 37.09328F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 62.90672F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(235, 461);
            this.tableLayoutPanel2.TabIndex = 1;
            // 
            // pnl_OuterButton
            // 
            this.pnl_OuterButton.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnl_OuterButton.Location = new System.Drawing.Point(3, 174);
            this.pnl_OuterButton.Name = "pnl_OuterButton";
            this.pnl_OuterButton.Size = new System.Drawing.Size(229, 284);
            this.pnl_OuterButton.TabIndex = 1;
            // 
            // TCPServerControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tableLayoutPanel1);
            this.Name = "TCPServerControl";
            this.Size = new System.Drawing.Size(482, 512);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.tableLayoutPanel2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton btn_ServerStart;
        private System.Windows.Forms.ToolStripButton btn_ServerStop;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.RichTextBox richTextBox1;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripProgressBar toolStripProgressBar1;
        private System.Windows.Forms.ToolStripStatusLabel lbl_ClientCount;
        private System.Windows.Forms.ToolStripLabel toolStripLabel1;
        private System.Windows.Forms.ToolStripTextBox tbx_SendData;
        private System.Windows.Forms.ToolStripComboBox cmb_ClientLIst;
        private System.Windows.Forms.ToolStripButton btn_SendData;
        private System.Windows.Forms.ToolStripButton btn_SendDataAll;
        private System.Windows.Forms.PropertyGrid propertyGrid1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private System.Windows.Forms.Panel pnl_OuterButton;
    }
}


namespace RsLib.TCP.Control
{
    partial class TCPClientControl
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
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.toolStripLabel3 = new System.Windows.Forms.ToolStripLabel();
            this.tbx_ClientName = new System.Windows.Forms.ToolStripTextBox();
            this.toolStripLabel1 = new System.Windows.Forms.ToolStripLabel();
            this.tbx_IP = new System.Windows.Forms.ToolStripTextBox();
            this.toolStripLabel2 = new System.Windows.Forms.ToolStripLabel();
            this.tbx_Port = new System.Windows.Forms.ToolStripTextBox();
            this.btn_ConnectServer = new System.Windows.Forms.ToolStripButton();
            this.btn_DisconnectServer = new System.Windows.Forms.ToolStripButton();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.lbl_ConnectStatus = new System.Windows.Forms.ToolStripStatusLabel();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.btn_SendToServer = new System.Windows.Forms.Button();
            this.rtbx_SendData = new System.Windows.Forms.RichTextBox();
            this.rtbx_ExchangeMsg = new System.Windows.Forms.RichTextBox();
            this.tableLayoutPanel1.SuspendLayout();
            this.toolStrip1.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.toolStrip1, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.statusStrip1, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.tableLayoutPanel2, 0, 1);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 3;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(427, 362);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // toolStrip1
            // 
            this.toolStrip1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.toolStrip1.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripLabel3,
            this.tbx_ClientName,
            this.toolStripLabel1,
            this.tbx_IP,
            this.toolStripLabel2,
            this.tbx_Port,
            this.btn_ConnectServer,
            this.btn_DisconnectServer});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(427, 30);
            this.toolStrip1.TabIndex = 0;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // toolStripLabel3
            // 
            this.toolStripLabel3.Name = "toolStripLabel3";
            this.toolStripLabel3.Size = new System.Drawing.Size(42, 27);
            this.toolStripLabel3.Text = "Name";
            // 
            // tbx_ClientName
            // 
            this.tbx_ClientName.Font = new System.Drawing.Font("Microsoft JhengHei UI", 9F);
            this.tbx_ClientName.Name = "tbx_ClientName";
            this.tbx_ClientName.Size = new System.Drawing.Size(100, 30);
            this.tbx_ClientName.Text = "Client";
            // 
            // toolStripLabel1
            // 
            this.toolStripLabel1.Name = "toolStripLabel1";
            this.toolStripLabel1.Size = new System.Drawing.Size(17, 27);
            this.toolStripLabel1.Text = "IP";
            // 
            // tbx_IP
            // 
            this.tbx_IP.Font = new System.Drawing.Font("Microsoft JhengHei UI", 9F);
            this.tbx_IP.Name = "tbx_IP";
            this.tbx_IP.Size = new System.Drawing.Size(100, 30);
            this.tbx_IP.Text = "127.0.0.1";
            this.tbx_IP.TextBoxTextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // toolStripLabel2
            // 
            this.toolStripLabel2.Name = "toolStripLabel2";
            this.toolStripLabel2.Size = new System.Drawing.Size(30, 27);
            this.toolStripLabel2.Text = "Port";
            // 
            // tbx_Port
            // 
            this.tbx_Port.Font = new System.Drawing.Font("Microsoft JhengHei UI", 9F);
            this.tbx_Port.Name = "tbx_Port";
            this.tbx_Port.Size = new System.Drawing.Size(50, 30);
            this.tbx_Port.Text = "1000";
            this.tbx_Port.TextBoxTextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // btn_ConnectServer
            // 
            this.btn_ConnectServer.BackgroundImage = global::RsLib.TCP.Properties.Resources.play_64px1;
            this.btn_ConnectServer.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btn_ConnectServer.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btn_ConnectServer.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btn_ConnectServer.Name = "btn_ConnectServer";
            this.btn_ConnectServer.Size = new System.Drawing.Size(23, 27);
            this.btn_ConnectServer.Text = "toolStripButton1";
            this.btn_ConnectServer.Click += new System.EventHandler(this.btn_ConnectServer_Click);
            // 
            // btn_DisconnectServer
            // 
            this.btn_DisconnectServer.BackgroundImage = global::RsLib.TCP.Properties.Resources.stop_64px;
            this.btn_DisconnectServer.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btn_DisconnectServer.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btn_DisconnectServer.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btn_DisconnectServer.Name = "btn_DisconnectServer";
            this.btn_DisconnectServer.Size = new System.Drawing.Size(23, 27);
            this.btn_DisconnectServer.Text = "toolStripButton2";
            this.btn_DisconnectServer.Click += new System.EventHandler(this.btn_DisconnectServer_Click);
            // 
            // statusStrip1
            // 
            this.statusStrip1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.lbl_ConnectStatus});
            this.statusStrip1.Location = new System.Drawing.Point(0, 332);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(427, 30);
            this.statusStrip1.TabIndex = 1;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // lbl_ConnectStatus
            // 
            this.lbl_ConnectStatus.Image = global::RsLib.TCP.Properties.Resources.Disconnect;
            this.lbl_ConnectStatus.Name = "lbl_ConnectStatus";
            this.lbl_ConnectStatus.Size = new System.Drawing.Size(85, 25);
            this.lbl_ConnectStatus.Text = "Disconnect";
            // 
            // timer1
            // 
            this.timer1.Enabled = true;
            this.timer1.Interval = 2000;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.ColumnCount = 2;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 85F));
            this.tableLayoutPanel2.Controls.Add(this.btn_SendToServer, 1, 0);
            this.tableLayoutPanel2.Controls.Add(this.rtbx_SendData, 0, 0);
            this.tableLayoutPanel2.Controls.Add(this.rtbx_ExchangeMsg, 0, 1);
            this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel2.Location = new System.Drawing.Point(3, 33);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 2;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 44.59459F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 55.40541F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(421, 296);
            this.tableLayoutPanel2.TabIndex = 2;
            // 
            // btn_SendToServer
            // 
            this.btn_SendToServer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btn_SendToServer.Image = global::RsLib.TCP.Properties.Resources.paper_plane_64px;
            this.btn_SendToServer.Location = new System.Drawing.Point(339, 3);
            this.btn_SendToServer.Name = "btn_SendToServer";
            this.btn_SendToServer.Size = new System.Drawing.Size(79, 125);
            this.btn_SendToServer.TabIndex = 0;
            this.btn_SendToServer.UseVisualStyleBackColor = true;
            this.btn_SendToServer.Click += new System.EventHandler(this.btn_SendToServer_Click);
            // 
            // rtbx_SendData
            // 
            this.rtbx_SendData.Dock = System.Windows.Forms.DockStyle.Fill;
            this.rtbx_SendData.Location = new System.Drawing.Point(3, 3);
            this.rtbx_SendData.Name = "rtbx_SendData";
            this.rtbx_SendData.Size = new System.Drawing.Size(330, 125);
            this.rtbx_SendData.TabIndex = 1;
            this.rtbx_SendData.Text = "";
            // 
            // rtbx_ExchangeMsg
            // 
            this.tableLayoutPanel2.SetColumnSpan(this.rtbx_ExchangeMsg, 2);
            this.rtbx_ExchangeMsg.Dock = System.Windows.Forms.DockStyle.Fill;
            this.rtbx_ExchangeMsg.Location = new System.Drawing.Point(3, 134);
            this.rtbx_ExchangeMsg.Name = "rtbx_ExchangeMsg";
            this.rtbx_ExchangeMsg.ReadOnly = true;
            this.rtbx_ExchangeMsg.Size = new System.Drawing.Size(415, 159);
            this.rtbx_ExchangeMsg.TabIndex = 2;
            this.rtbx_ExchangeMsg.Text = "";
            // 
            // TCPClientControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tableLayoutPanel1);
            this.Name = "TCPClientControl";
            this.Size = new System.Drawing.Size(427, 362);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.tableLayoutPanel2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripButton btn_ConnectServer;
        private System.Windows.Forms.ToolStripButton btn_DisconnectServer;
        private System.Windows.Forms.ToolStripTextBox tbx_IP;
        private System.Windows.Forms.ToolStripLabel toolStripLabel1;
        private System.Windows.Forms.ToolStripLabel toolStripLabel2;
        private System.Windows.Forms.ToolStripTextBox tbx_Port;
        private System.Windows.Forms.ToolStripStatusLabel lbl_ConnectStatus;
        private System.Windows.Forms.ToolStripLabel toolStripLabel3;
        private System.Windows.Forms.ToolStripTextBox tbx_ClientName;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private System.Windows.Forms.Button btn_SendToServer;
        private System.Windows.Forms.RichTextBox rtbx_SendData;
        private System.Windows.Forms.RichTextBox rtbx_ExchangeMsg;
    }
}

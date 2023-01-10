
namespace RsLib.DemoForm
{
    partial class Form1
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

        #region Windows Form 設計工具產生的程式碼

        /// <summary>
        /// 此為設計工具支援所需的方法 - 請勿使用程式碼編輯器修改
        /// 這個方法的內容。
        /// </summary>
        private void InitializeComponent()
        {
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage_TCPServer = new System.Windows.Forms.TabPage();
            this.pnl_TCPServer = new System.Windows.Forms.Panel();
            this.tabPage_TCPClient = new System.Windows.Forms.TabPage();
            this.pnl_TCPClient = new System.Windows.Forms.Panel();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.listBox1 = new System.Windows.Forms.ListBox();
            this.button1 = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.tabControl1.SuspendLayout();
            this.tabPage_TCPServer.SuspendLayout();
            this.tabPage_TCPClient.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage_TCPServer);
            this.tabControl1.Controls.Add(this.tabPage_TCPClient);
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(3, 3);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(764, 476);
            this.tabControl1.TabIndex = 0;
            // 
            // tabPage_TCPServer
            // 
            this.tabPage_TCPServer.Controls.Add(this.pnl_TCPServer);
            this.tabPage_TCPServer.Location = new System.Drawing.Point(4, 22);
            this.tabPage_TCPServer.Name = "tabPage_TCPServer";
            this.tabPage_TCPServer.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage_TCPServer.Size = new System.Drawing.Size(756, 450);
            this.tabPage_TCPServer.TabIndex = 0;
            this.tabPage_TCPServer.Text = "TCP Server";
            this.tabPage_TCPServer.UseVisualStyleBackColor = true;
            // 
            // pnl_TCPServer
            // 
            this.pnl_TCPServer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnl_TCPServer.Location = new System.Drawing.Point(3, 3);
            this.pnl_TCPServer.Name = "pnl_TCPServer";
            this.pnl_TCPServer.Size = new System.Drawing.Size(750, 444);
            this.pnl_TCPServer.TabIndex = 0;
            // 
            // tabPage_TCPClient
            // 
            this.tabPage_TCPClient.Controls.Add(this.pnl_TCPClient);
            this.tabPage_TCPClient.Location = new System.Drawing.Point(4, 22);
            this.tabPage_TCPClient.Name = "tabPage_TCPClient";
            this.tabPage_TCPClient.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage_TCPClient.Size = new System.Drawing.Size(756, 450);
            this.tabPage_TCPClient.TabIndex = 1;
            this.tabPage_TCPClient.Text = "TCP Client";
            this.tabPage_TCPClient.UseVisualStyleBackColor = true;
            // 
            // pnl_TCPClient
            // 
            this.pnl_TCPClient.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnl_TCPClient.Location = new System.Drawing.Point(3, 3);
            this.pnl_TCPClient.Name = "pnl_TCPClient";
            this.pnl_TCPClient.Size = new System.Drawing.Size(750, 444);
            this.pnl_TCPClient.TabIndex = 0;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.comboBox1);
            this.tabPage1.Controls.Add(this.listBox1);
            this.tabPage1.Controls.Add(this.button1);
            this.tabPage1.Controls.Add(this.label1);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(756, 450);
            this.tabPage1.TabIndex = 2;
            this.tabPage1.Text = "tabPage1";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // listBox1
            // 
            this.listBox1.FormattingEnabled = true;
            this.listBox1.ItemHeight = 12;
            this.listBox1.Location = new System.Drawing.Point(346, 6);
            this.listBox1.Name = "listBox1";
            this.listBox1.Size = new System.Drawing.Size(290, 436);
            this.listBox1.TabIndex = 2;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(92, 263);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 1;
            this.button1.Text = "button1";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(90, 187);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(33, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "label1";
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Controls.Add(this.tabControl1, 0, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 70.22222F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 29.77778F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(770, 687);
            this.tableLayoutPanel1.TabIndex = 1;
            // 
            // comboBox1
            // 
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Location = new System.Drawing.Point(136, 89);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(121, 20);
            this.comboBox1.TabIndex = 3;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(770, 687);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Name = "Form1";
            this.Text = "Form1";
            this.tabControl1.ResumeLayout(false);
            this.tabPage_TCPServer.ResumeLayout(false);
            this.tabPage_TCPClient.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage_TCPServer;
        private System.Windows.Forms.TabPage tabPage_TCPClient;
        private System.Windows.Forms.Panel pnl_TCPServer;
        private System.Windows.Forms.Panel pnl_TCPClient;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.ListBox listBox1;
        private System.Windows.Forms.ComboBox comboBox1;
    }
}


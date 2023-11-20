using RsLib.TCP.Server;
using System;
using System.Windows.Forms;
namespace RsLib.TCP.Control
{
    public partial class TCPServerControl : UserControl
    {
        TCPServer _server = new TCPServer();
        public event Action<string, string> DataReceived;
        public TCPServerControl()
        {
            InitializeComponent();
            _server.DataReceived += _server_DataReceived;
            _server.ClientAdded += _server_ClientAdded;
            propertyGrid1.SelectedObject = _server.Option;
        }
        public void SetOuterButton(System.Windows.Forms.Control ctrl)
        {
            pnl_OuterButton.Controls.Clear();
            pnl_OuterButton.Controls.Add(ctrl);
        }
        private void _server_ClientAdded(string obj)
        {
            if (this.InvokeRequired)
            {
                Action<string> action = new Action<string>(_server_ClientAdded);
                this.Invoke(action, obj);
            }
            else
            {
                cmb_ClientLIst.Items.Clear();
                for (int i = 0; i < _server.ClientCount; i++)
                {
                    string clientName = _server.ClientsName[i];
                    cmb_ClientLIst.Items.Add(clientName);
                }
            }
        }

        private void _server_DataReceived(string name, string msg)
        {
            if (this.InvokeRequired)
            {
                Action<string, string> action = new Action<string, string>(_server_DataReceived);
                this.Invoke(action, name, msg);
            }
            else
            {
                if (richTextBox1.Lines.Length > 50) richTextBox1.Clear();
                DataReceived?.Invoke(name, msg);

                string displayMsg = $"{DateTime.Now:HH:mm:ss.fff}\t{name}\t<< {msg}\n";
                richTextBox1.AppendText(displayMsg);
            }
        }

        private void btn_ServerStart_Click(object sender, EventArgs e)
        {
            _server.Start();
        }

        private void btn_ServerStop_Click(object sender, EventArgs e)
        {
            _server.Stop();
            _server_ClientAdded("");
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            toolStripProgressBar1.Style = _server.IsRun ? ProgressBarStyle.Marquee : ProgressBarStyle.Blocks;
            toolStripProgressBar1.Value = _server.IsRun ? 100 : 0;
            lbl_ClientCount.Text = _server.ClientCount.ToString();

            btn_ServerStop.Enabled = _server.IsRun;
            btn_ServerStart.Enabled = !_server.IsRun;
        }
        public void SendData(string clientName, string data)
        {
            if (data == "") return;
            if (_server.IsRun == false) return;
            if (_server.ClientCount == 0) return;
            _server.Send(clientName, data);
            string displayMsg = $"{DateTime.Now:HH:mm:ss.fff}\t>> {data}\n";
            richTextBox1.Invoke((Action)(() =>
            {
                richTextBox1.AppendText(displayMsg);
                richTextBox1.ScrollToCaret();
            }));

        }
        private void btn_SendData_Click(object sender, EventArgs e)
        {

            string data = tbx_SendData.Text;
            if (data == "") return;
            if (_server.IsRun == false) return;
            if (cmb_ClientLIst.Items.Count == 0) return;
            int selectIndex = cmb_ClientLIst.SelectedIndex;
            if (selectIndex == -1) return;
            string clientName = cmb_ClientLIst.SelectedItem.ToString();
            SendData(clientName, data);
        }

        private void btn_SendDataAll_Click(object sender, EventArgs e)
        {
            string data = tbx_SendData.Text;
            if (data == "") return;
            if (_server.IsRun == false) return;
            if (cmb_ClientLIst.Items.Count == 0) return;

            _server.Send(data);

        }

        private void propertyGrid1_PropertyValueChanged(object s, PropertyValueChangedEventArgs e)
        {
            _server.SaveYaml();
        }
    }
}

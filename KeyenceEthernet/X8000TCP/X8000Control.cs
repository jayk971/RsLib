using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading;
using RsLib.LogMgr;
namespace RsLib.X8000TCP
{
    public partial class X8000Control : UserControl
    {
        X8000 x8k = new X8000();
        public bool IsConnect => x8k.IsConnected;
        public bool IsRunMode => x8k.CheckRunMode();
        public int CurrentSettingNum => x8k.ReadCurrentSettingNumber();

        public string Status
        {
            get
            {
                if (x8k == null) return "Disconnected";
                else return x8k.Status;
            }
        }

        public X8000Control()
        {
            InitializeComponent();
            cmb_x8000Number.Items.Clear();
            for (int i = 0; i <= 999; i++)
            {
                cmb_x8000Number.Items.Add(i);
            }
            x8k.LoadYaml();
            tbx_IP.Text = x8k.IP;
            tbx_Port.Text = x8k.Port.ToString();
        }

        public void SetX8000(X8000 x8000)
        {
            x8k = x8000;
        }
        public bool ConnectX8000()
        {
            if (x8k == null)
            {
                x8k = new X8000();
            }
            x8k.UpdateCommand += X8k_UpdateStatus;
            x8k.IP = tbx_IP.Text;
            x8k.Port = int.Parse(tbx_Port.Text);
            //x8k.SaveYaml();
            bool isConnected =  x8k.Connect();
            if (isConnected)
            {
                SwitchRunMode();
                ResetAlarm();
                updateUI();
            }
            return isConnected;
        }

        void updateUI()
        {
            if(this.InvokeRequired)
            {
                Action action = new Action(updateUI);
                this.Invoke(action);
            }
            else
            {
                if (IsRunMode)
                {
                    rbn_RunMode.Checked = true;
                    cmb_x8000Number.SelectedIndex = CurrentSettingNum;

                }
                else
                {
                    rbn_SetMode.Checked = true;
                }
            }
        }
        private void btn_Connect_Click(object sender, EventArgs e)
        {
            ConnectX8000();
            x8k.SaveYaml();
        }

        private void X8k_UpdateStatus(string obj)
        {
            richTextBox1.Text = obj;
        }

        private void chbx_TriggerEnable_CheckedChanged(object sender, EventArgs e)
        {
            if (x8k == null) return;
            if (!x8k.IsConnected) return;
            if (chbx_TriggerEnable.Checked)
            {
                chbx_TriggerEnable.Text = "Trigger Enable";
                x8k.TriggerEnable();
            }
            else
            {
                chbx_TriggerEnable.Text = "Trigger Disable";
                x8k.TriggerDisable();
            }
        }

        private void btn_Trigger_Click(object sender, EventArgs e)
        {
            if (x8k == null) return;
            x8k.Trigger();
        }
        public void SwitchRunMode()
        {
            if (x8k == null) return;
            x8k.SwitchToRun();
        }
        public void SwitchSettingMode()
        {
            if (x8k == null) return;
            x8k.SwitchToSetting();
        }
        public void StartScan()
        {
            if (x8k == null) return;
            x8k.Trigger();
        }
        public void StopScan()
        {
            if (x8k == null) return;
            x8k.StopTrigger();

        }
        public void ResetAlarm()
        {
            if (x8k == null) return;
            x8k.ClearError();
        }
        private void rbn_RunMode_CheckedChanged(object sender, EventArgs e)
        {
            if (rbn_RunMode.Checked) SwitchRunMode();
            else SwitchSettingMode();
        }

        private void toolStripDropDownButton1_Click(object sender, EventArgs e)
        {

        }

        private void mim_Reset_Click(object sender, EventArgs e)
        {
            if (x8k == null) return;
            x8k.Reset();
        }

        private void mim_Reboot_Click(object sender, EventArgs e)
        {
            if (x8k == null) return;
            x8k.Reboot();
        }

        private void mim_ClearError_Click(object sender, EventArgs e)
        {
            if (x8k == null) return;
            x8k.ClearError();
        }

        private void mim_SaveScreenPrint_Click(object sender, EventArgs e)
        {
            if (x8k == null) return;
            x8k.SaveX8000ScreenPrint();
        }

        private void mim_X8kVersion_Click(object sender, EventArgs e)
        {
            if (x8k == null) return;
            string[] data =  x8k.ReadX8000Version();
            MessageBox.Show($"{data[0]},{data[1]}","x8000 Version",MessageBoxButtons.OK,MessageBoxIcon.Information);
        }

        private void X8000Control_Load(object sender, EventArgs e)
        {

        }

        private void btn_ReadX8000SettingNum_Click(object sender, EventArgs e)
        {
            cmb_x8000Number.SelectedIndex = CurrentSettingNum;
        }
        public void ChangeSettingNum(int num)
        {
            if (x8k == null) return;
            x8k.SwitchSettingNumber(num);
        }
        private void btn_WriteX8000SettingNum_Click(object sender, EventArgs e)
        {
            ChangeSettingNum(cmb_x8000Number.SelectedIndex);
        }

        private void mim_ReadX8000Time_Click(object sender, EventArgs e)
        {
            if (x8k == null) return;

            DateTime dt = x8k.ReadX8000Time();
            MessageBox.Show($"Read {dt:yyyy-MM-dd HH:mm:ss}", "X8000 DateTime", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void setX8000TimeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (x8k == null) return;

            DateTime dt = DateTime.Now;
            bool result = x8k.WriteX8000Time(dt.Year, dt.Month, dt.Day, dt.Hour, dt.Minute, dt.Second);
            if(result) MessageBox.Show($"Write {dt:yyyy-MM-dd HH:mm:ss}", "X8000 DateTime", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }
}

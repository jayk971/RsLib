using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace RsLib.AlarmMgr
{
    public partial class AlarmControl : UserControl
    {
        public AlarmControl()
        {
            InitializeComponent();
            AlarmHistory.AlarmQueueUpdated += AlarmHistory_AlarmQueueUpdated;

        }
        public void SetAlarmStyle()
        {
            btn_ResetAlarm.Enabled = true;
            btn_ResetAlarm.BackColor = Color.Orange;
        }
        public void SetWarningStyle()
        {
            btn_ResetAlarm.Enabled = true;
            btn_ResetAlarm.BackColor = Color.Gold;
        }
        public void SetNormalStyle()
        {
            btn_ResetAlarm.Enabled = false;
            btn_ResetAlarm.BackColor = Color.Transparent;
        }
        private void AlarmControl_Load(object sender, EventArgs e)
        {
        }

        private void AlarmHistory_AlarmQueueUpdated(Queue<AlarmItem> obj)
        {
            if (this.InvokeRequired)
            {
                Action< Queue < AlarmItem >> updateErrorUI = new Action<Queue<AlarmItem>>(AlarmHistory_AlarmQueueUpdated);
                this?.Invoke(updateErrorUI, obj);
            }
            else
            {
                dgvAlarmRealTime.Rows.Clear();
                List<AlarmItem> ErrorList = obj.ToList();
                for (int i = 0; i < ErrorList.Count; i++)
                {
                    AlarmItem item = ErrorList[i];
                    dgvAlarmRealTime.Rows.Add(item.ToObj());
                }
            }

        }

        private void btn_ResetAlarm_Click(object sender, EventArgs e)
        {
            AlarmHistory.ResetAllAlarm();
        }
    }
}

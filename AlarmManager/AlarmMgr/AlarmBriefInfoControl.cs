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
    public partial class AlarmBriefInfoControl : UserControl
    {
        public AlarmBriefInfoControl()
        {
            InitializeComponent();
            AlarmHistory.AlarmQueueUpdated += AlarmHistory_AlarmQueueUpdated;
        }



        private void AlarmBriefInfoControl_Load(object sender, EventArgs e)
        {
        }

        private void AlarmHistory_AlarmQueueUpdated(Queue<AlarmItem> obj)
        {
            if (this.InvokeRequired)
            {
                Action<Queue<AlarmItem>> updateErrorUI = new Action<Queue<AlarmItem>>(AlarmHistory_AlarmQueueUpdated);
                this?.Invoke(updateErrorUI, obj);
            }
            else
            {
                dgvAlarmBriefInfo.Rows.Clear();
                List<AlarmItem> ErrorList = obj.ToList();
                for (int i = 0; i < ErrorList.Count; i++)
                {
                    AlarmItem item = ErrorList[i];
                    dgvAlarmBriefInfo.Rows.Add(item.ToBriefObj());
                }
            }
        }

        private void dgvAlarmBriefInfo_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}

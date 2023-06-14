using RsLib.BaseType;
using System;
using System.Drawing;
using System.Windows.Forms;
namespace RsLib.LogMgr
{
    public partial class LogControl : UserControl
    {
        //Queue<LogMsg> logQ = new Queue<LogMsg>();
        LockQueue<LogMsg> logQ = new LockQueue<LogMsg>();
        public int Count => logQ.Count;
        public int TextLength => rtbx_Log.TextLength;
        public LogControl()
        {
            InitializeComponent();
            Log.UiUpdated += Log_UiUpdated;
            cmb_LevelFilter.Items.Clear();
            string[] levels = Enum.GetNames(typeof(MsgLevel));
            for (int i = 0; i < levels.Length; i++)
            {
                cmb_LevelFilter.Items.Add(levels[i]);
            }
            cmb_LevelFilter.Items.Add("All");

            cmb_LevelFilter.SelectedIndexChanged += Cmb_LevelFilter_SelectedIndexChanged;
            cmb_LevelFilter.SelectedIndex = 4;
        }
        public void SetDisplayLevel(MsgLevel level)
        {
            cmb_LevelFilter.SelectedIndex = (int)level;
        }

        private void Cmb_LevelFilter_SelectedIndexChanged(object sender, EventArgs e)
        {
            show(true);
        }
        //delegate void delegateUpdateUI(string msg, MsgLevel level);
        private void Log_UiUpdated(LogMsg msg)
        {
            if (logQ.Count >= 100)
            {
                for (int i = 0; i < 75; i++)
                {
                    logQ.Dequeue();
                }
                logQ.Enqueue(msg);
                show(true);
            }
            else
            {
                logQ.Enqueue(msg);
                show(false);
            }
        }
        //void show(bool loadLast)
        //{
        //    if (this.InvokeRequired)
        //    {
        //        Action<bool> action = new Action<bool>(show);
        //        this.Invoke(action, loadLast);
        //    }
        //    else
        //    {
        //        LogMsg[] lines = logQ.ToArray();
        //        int levelFilterIndex = cmb_LevelFilter.SelectedIndex;

        //        if (loadLast)
        //        {
        //            LogMsg lastMsg = lines[lines.Length - 1];
        //            if ((int)lastMsg.Level == levelFilterIndex || levelFilterIndex == 4)
        //            {
        //                renderRichTexyBox(lastMsg.ToString(), lastMsg.Level);
        //            }
        //        }
        //        else
        //        {
        //            rtbx_Log.Clear();
        //            for (int i = 0; i < lines.Length; i++)
        //            {
        //                LogMsg msg = lines[i];
        //                if ((int)msg.Level == levelFilterIndex || levelFilterIndex == 4)
        //                {
        //                    renderRichTexyBox(msg.ToString(), msg.Level);
        //                }
        //            }
        //        }
        //        rtbx_Log.ScrollToCaret();
        //    }
        //}
        void show(bool clearAll)
        {
            if (this.InvokeRequired)
            {
                Action<bool> action = new Action<bool>(show);
                this.Invoke(action, clearAll);
            }
            else
            {
                try
                {
                    LogMsg[] lines = logQ.ToArray();
                    if (cmb_LevelFilter == null) return;

                    int levelFilterIndex = cmb_LevelFilter.SelectedIndex;

                    if (!clearAll)
                    {
                        LogMsg lastMsg = lines[lines.Length - 1];
                        if (lastMsg != null)
                        {
                            if ((int)lastMsg.Level >= levelFilterIndex || levelFilterIndex == 4)
                            {
                                renderRichTexyBox(lastMsg);
                            }
                        }
                    }
                    else
                    {
                        if (rtbx_Log == null) return;
                        rtbx_Log.Clear();
                        for (int i = 0; i < lines.Length; i++)
                        {
                            LogMsg msg = lines[i];
                            if (msg != null)
                            {
                                if ((int)msg.Level >= levelFilterIndex || levelFilterIndex == 4)
                                {
                                    renderRichTexyBox(msg);
                                }
                            }
                        }
                    }
                    if (rtbx_Log == null) return;
                    rtbx_Log.ScrollToCaret();
                }
                catch (Exception ex)
                {
                    Log.m_Log.Error(ex.Message);
                    Log.m_FatalLog.Fatal(ex);

                }
            }
        }
        void renderRichTexyBox(LogMsg msg)
        {
            int textLength = rtbx_Log.Text.Length;
            rtbx_Log.AppendText(msg.ToString());
            rtbx_Log.SelectionStart = textLength;
            rtbx_Log.SelectionLength = rtbx_Log.Text.Length - textLength;
            if (msg.EnableSpecialColor)
            {
                rtbx_Log.SelectionBackColor = msg.BackColor;
                rtbx_Log.SelectionColor = msg.ForeColor;
            }
            else
            {
                switch (msg.Level)
                {
                    case MsgLevel.Trace:
                        rtbx_Log.SelectionBackColor = Color.White;
                        break;
                    case MsgLevel.Info:
                        rtbx_Log.SelectionBackColor = Color.Silver;
                        break;
                    case MsgLevel.Warn:
                        rtbx_Log.SelectionBackColor = Color.Gold;
                        break;
                    case MsgLevel.Alarm:
                        rtbx_Log.SelectionBackColor = Color.Coral;

                        break;
                    default:
                        rtbx_Log.SelectionBackColor = Color.White;
                        break;
                }
            }
            rtbx_Log.AppendText("\n");
        }
        void renderRichTexyBox(string msg, MsgLevel level)
        {
            int textLength = rtbx_Log.Text.Length;
            rtbx_Log.AppendText(msg);
            rtbx_Log.SelectionStart = textLength;
            rtbx_Log.SelectionLength = rtbx_Log.Text.Length - textLength;
            switch (level)
            {
                case MsgLevel.Trace:
                    rtbx_Log.SelectionBackColor = Color.White;
                    break;
                case MsgLevel.Info:
                    rtbx_Log.SelectionBackColor = Color.Silver;
                    break;
                case MsgLevel.Warn:
                    rtbx_Log.SelectionBackColor = Color.Gold;
                    break;
                case MsgLevel.Alarm:
                    rtbx_Log.SelectionBackColor = Color.Coral;

                    break;
                default:
                    rtbx_Log.SelectionBackColor = Color.White;
                    break;
            }
            rtbx_Log.AppendText("\n");
        }

        private void btn_ClearMsg_Click(object sender, EventArgs e)
        {
            rtbx_Log.Clear();
            logQ.Clear();
        }

        private void cmb_LevelFilter_Click(object sender, EventArgs e)
        {

        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using NLog;
using RsLib.BaseType;
using System.Threading;
namespace RsLib.LogMgr
{
    public enum MsgLevel : int
    {
        Trace,
        Info,
        Warning,
        Alarm,
    }
    public static class Log
    {
        private static Logger m_Log = LogManager.GetLogger("Lib");
        private static Logger m_FatalLog = LogManager.GetLogger("Lib_Fatal");
        //public delegate void delegateUpdateUI(string msg, MsgLevel level);
        public static event Action<LogMsg> UiUpdated;
        private static readonly object _lock = new object();
        private static LockQueue<LogMsg> logQ = new LockQueue<LogMsg>();
        public static bool EnableUpdateUI = true;
        public static void Add(string Msg, MsgLevel errLevel, Exception ex = null)
        {
            lock (_lock)
            {
                LogMsg msg = new LogMsg(errLevel, Msg,ex);
                logQ.Enqueue(msg);
            }
        }
        public static void Add(string Msg, MsgLevel errLevel, bool updateUI,Exception ex = null)
        {
            lock (_lock)
            {
                LogMsg msg = new LogMsg(errLevel, Msg,ex, updateUI);
                logQ.Enqueue(msg);
            }
        }
        public static void Start()
        {
            string LogFolder = string.Format("{0}\\Log", System.Environment.CurrentDirectory);
            if (!Directory.Exists(LogFolder)) Directory.CreateDirectory(LogFolder);

            ThreadPool.QueueUserWorkItem(run);
        }
        static void run(object obj)
        {
            while(true)
            {
                SpinWait.SpinUntil(() => false, 5);
                if (logQ.Count == 0) continue;

                LogMsg tempMsg = logQ.Dequeue();
                switch (tempMsg.Level)
                {
                    case MsgLevel.Trace:
                        m_Log.Trace(tempMsg.Text);

                        break;
                    case MsgLevel.Info:
                        m_Log.Info(tempMsg.Text);

                        break;

                    case MsgLevel.Warning:

                        m_Log.Warn(tempMsg.Text);
                        break;

                    case MsgLevel.Alarm:
                        if (tempMsg.Ex == null)
                        {
                            m_Log.Error(tempMsg.Text);
                            m_FatalLog.Fatal(tempMsg.Text);
                            //MessageBox.Show(Msg, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

                        }
                        else
                        {
                            m_Log.Error(tempMsg.Text + "\t" + tempMsg.Ex.Message);
                            m_FatalLog.Fatal(tempMsg.Ex);
                            MessageBox.Show(tempMsg.Text + "\n" + tempMsg.Ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

                        }
                        break;


                    default:

                        break;
                }
                if (EnableUpdateUI)
                {
                    if (tempMsg.UpdateUI) UiUpdated?.Invoke(tempMsg);
                }
            }
        }
    }

    public class LogMsg
    {
        public DateTime Time;
        public MsgLevel Level;
        public string Text;
        public Exception Ex;
        public bool UpdateUI = true;

        public LogMsg(MsgLevel level,string text,Exception ex)
        {
            Time = DateTime.Now;
            Level = level;
            Text = text;
            Ex = ex;
        }
        public LogMsg(MsgLevel level, string text, Exception ex,bool updateUI)
        {
            Time = DateTime.Now;
            Level = level;
            Text = text;
            Ex = ex;
            UpdateUI = updateUI;
        }


        public override string ToString()
        {
            return $"{Time:HH:mm:ss}\t{Level}\t{Text}";
        }
    }
}

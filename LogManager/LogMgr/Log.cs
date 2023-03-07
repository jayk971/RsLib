using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using NLog;
using RsLib.BaseType;
using System.Threading;
using System.Drawing;
using System.Collections.Concurrent;

namespace RsLib.LogMgr
{
    public enum MsgLevel : int
    {
        Trace,
        Info,
        Warn,
        Alarm,
    }
    public static class Log
    {
        internal static Logger m_Log = LogManager.GetLogger("Lib");
        internal static Logger m_FatalLog = LogManager.GetLogger("Lib_Fatal");
        static bool _enableTd = false;
        static bool _isTdRunning = false;
        //public delegate void delegateUpdateUI(string msg, MsgLevel level);
        public static event Action<LogMsg> UiUpdated;
        private static readonly object _lock = new object();
        private static ConcurrentQueue<LogMsg> _logQ = new ConcurrentQueue<LogMsg>();
        //private static LockQueue<LogMsg> logQ = new LockQueue<LogMsg>();
        public static bool EnableUpdateUI = true;
        public static void Add(string Msg, MsgLevel errLevel, Exception ex = null)
        {
            //lock (_lock)
            //{
            //    LogMsg msg = new LogMsg(errLevel, Msg,ex);
            //    logQ.Enqueue(msg);
            //}
            LogMsg msg = new LogMsg(errLevel, Msg, ex);
            _logQ.Enqueue(msg);

        }
        public static void Add(string Msg, MsgLevel errLevel,Color backColor,Color foreColor ,Exception ex = null)
        {
            //lock (_lock)
            //{
            //    LogMsg msg = new LogMsg(errLevel, Msg,backColor,foreColor, ex);
            //    logQ.Enqueue(msg);
            //}
            LogMsg msg = new LogMsg(errLevel, Msg, backColor, foreColor, ex);
            _logQ.Enqueue(msg);

        }

        public static void Add(string Msg, MsgLevel errLevel, bool updateUI,Exception ex = null)
        {
            //lock (_lock)
            //{
            //    LogMsg msg = new LogMsg(errLevel, Msg,ex, updateUI);
            //    logQ.Enqueue(msg);
            //}
            LogMsg msg = new LogMsg(errLevel, Msg, ex, updateUI);
            _logQ.Enqueue(msg);
        }
        public static void Start()
        {
            string LogFolder = string.Format("{0}\\Log", System.Environment.CurrentDirectory);
            if (!Directory.Exists(LogFolder)) Directory.CreateDirectory(LogFolder);

            _enableTd = true;
            ThreadPool.QueueUserWorkItem(run);
        }
        public static void Stop()
        {
            EnableUpdateUI = false;
            _enableTd = false;
        }
        static void run(object obj)
        {
            while(_enableTd)
            {
                _isTdRunning = true;
                SpinWait.SpinUntil(() => false, 5);
                if (_logQ.Count == 0) continue;
                LogMsg tempMsg;
                bool isDequeueOK = _logQ.TryDequeue(out tempMsg);
                //LogMsg tempMsg = logQ.Dequeue();
                if (isDequeueOK)
                {
                    switch (tempMsg.Level)
                    {
                        case MsgLevel.Trace:
                            m_Log.Trace(tempMsg.ToString());

                            break;
                        case MsgLevel.Info:
                            m_Log.Info(tempMsg.ToString());

                            break;

                        case MsgLevel.Warn:

                            m_Log.Warn(tempMsg.ToString());
                            break;

                        case MsgLevel.Alarm:
                            if (tempMsg.Ex == null)
                            {
                                m_Log.Error(tempMsg.ToString());
                                m_FatalLog.Fatal(tempMsg.Text);
                                //MessageBox.Show(Msg, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

                            }
                            else
                            {
                                m_Log.Error(tempMsg.ToString() + "\t" + tempMsg.Ex.Message);
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
            _isTdRunning = false;
        }
    }

    public class LogMsg
    {
        public DateTime Time;
        public MsgLevel Level;
        public string Text;
        public Exception Ex;
        public bool UpdateUI = true;
        public Color BackColor { get; private set; } = Color.FromKnownColor(KnownColor.Control);
        public Color ForeColor { get; private set; } = Color.Black;
        public bool EnableSpecialColor { get; private set; } = false;
        public LogMsg(MsgLevel level, string text, Exception ex)
        {
            Time = DateTime.Now;
            Level = level;
            Text = text;
            Ex = ex;
        }
        public LogMsg(MsgLevel level,string text,Color backColor,Color foreColor,Exception ex)
        {
            Time = DateTime.Now;
            Level = level;
            Text = text;
            Ex = ex;
            BackColor = backColor;
            ForeColor = foreColor;
            EnableSpecialColor = true;
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
            return $"{Time:HH:mm:ss.fff}\t{Level}\t{Text}";
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using NLog;
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
        public static void Add(string Msg, MsgLevel errLevel, Exception ex = null)
        {
            string LogFolder = string.Format("{0}\\Log", System.Environment.CurrentDirectory);
            if (!Directory.Exists(LogFolder)) Directory.CreateDirectory(LogFolder);
            LogMsg msg = new LogMsg(errLevel, Msg);
            switch (errLevel)
            {
                case MsgLevel.Trace:
                    m_Log.Trace(Msg);

                    break;
                case MsgLevel.Info:

                    m_Log.Info(Msg);
                    //MessageBox.Show(Msg, "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    break;

                case MsgLevel.Warning:

                    m_Log.Warn(Msg);
                    //MessageBox.Show(Msg, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                    break;

                case MsgLevel.Alarm:
                    if (ex == null)
                    {
                        m_Log.Error(Msg);
                        m_FatalLog.Fatal(Msg);
                        //MessageBox.Show(Msg, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

                    }
                    else
                    {
                        m_Log.Error(Msg + "\t" + ex.Message);
                        m_FatalLog.Fatal(ex);
                        MessageBox.Show(Msg + "\n" + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

                    }
                    break;


                default:

                    break;
            }
            if (UiUpdated != null)
            {
                UiUpdated(msg);
            }
        }

    }

    public class LogMsg
    {
        public DateTime Time;
        public MsgLevel Level;
        public string Text;

        public LogMsg(MsgLevel level,string text)
        {
            Time = DateTime.Now;
            Level = level;
            Text = text;
        }

        public override string ToString()
        {
            return $"{Time:HH:mm:ss}\t{Level}\t{Text}";
        }
    }
}

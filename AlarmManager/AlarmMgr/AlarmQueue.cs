using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using RsLib.LogMgr;
using RsLib.Common;
using System.IO;
namespace RsLib.AlarmMgr
{
    public static class AlarmHistory
    {
        public static event Action<Queue<AlarmItem>> AlarmQueueUpdated;
        static Queue<AlarmItem> _Q = new Queue<AlarmItem>();
        public static void ResetAlarm()
        {
            if (_Q.Count != 0)
            {
                AlarmItem ErrorItem = _Q.Dequeue();
                Log.Add(string.Format("Error Reset : {0}", ErrorItem.Code), MsgLevel.Trace);
            }
            if(AlarmQueueUpdated!= null) AlarmQueueUpdated(_Q);
        }
        public static void ResetAllAlarm()
        {
            if (_Q.Count != 0) _Q.Clear();
            Log.Add("Error Reset All", MsgLevel.Trace);
            AlarmQueueUpdated?.Invoke(_Q);
        }
        public static void Add(int code, Exception ex = null)
        {
            if (!Contain(code))
            {
                if (ex != null)
                {
                    Add(code, ex.Message, ex);
                }
                else
                {
                    AlarmItem error = new AlarmItem(code, ex);
                    _Q.Enqueue(error);
                    AlarmQueueUpdated?.Invoke(_Q);
                }
            }
        }
        public static void Add(int code, string msg, Exception ex = null)
        {
            AlarmItem error = new AlarmItem(code, msg, ex);
            _Q.Enqueue(error);
            AlarmQueueUpdated?.Invoke(_Q);
        }
        public static void Add(object objCode, Exception ex = null)
        {
            int code = (int)objCode;
            if (!Contain(code))
            {
                if (ex != null)
                {
                    Add(code, ex.Message, ex);
                }
                else
                {
                    AlarmItem error = new AlarmItem(code, ex);
                    _Q.Enqueue(error);
                    AlarmQueueUpdated?.Invoke(_Q);
                }
            }
        }
        public static void Add(object objCode, string msg, Exception ex = null)
        {
            int code = (int)objCode;
            AlarmItem error = new AlarmItem(code, msg, ex);
            _Q.Enqueue(error);
            AlarmQueueUpdated?.Invoke(_Q);
        }
        public static bool Contain(int code)
        {
            List<AlarmItem> list = _Q.ToList();
            for (int i = 0; i < list.Count; i++)
            {
                AlarmItem item = list[i];
                if (item.Code == code) return true;
            }
            return false;
        }
    }


    public class AlarmItem
    {
        DateTime time;
        int code = 0;
        public int Code {get => code; }

        string name = "";
        public string Name { get => name; }
        MsgLevel level = MsgLevel.Warning;
        public MsgLevel Level { get => level; }
        string description = "";
        public string Description { get => description; }
        string reason = "";
        public string Reason { get => reason; }
        string remedy = "";
        public string Remedy = "";
        string note = "";
        public string Note { get => note; }

        string mainFolder { get => System.Environment.CurrentDirectory; }
        string configFolder { get => $"{mainFolder}\\Config"; }
        string alarmMsgIniFile { get => $"{configFolder}\\AlarmMsg.ini"; }

        public AlarmItem(int alarmCode, Exception ex = null)
        {
            time = DateTime.Now;
            code = alarmCode;
            if (((int)code % 1000) < 500) level = MsgLevel.Alarm;
            else level = MsgLevel.Warning;
            GetErrorData();
            if (note == "") Log.Add($"Error Code : {code} - {name}", level, ex);
            else Log.Add($"Error Code : {code} -{name}, Note : {note}", level, ex);

        }
        public AlarmItem(int alarmCode, string msg, Exception ex = null)
        {
            time = DateTime.Now;
            code = alarmCode;
            if (((int)code % 1000) < 500) level = MsgLevel.Alarm;
            else level = MsgLevel.Warning;
            GetErrorData();
            note = msg;
            if (note == "") Log.Add($"Error Code : {code} - {name}", level, ex);
            else Log.Add($"Error Code : {code} - {name}, Note : {note}",level, ex);

        }
        void GetErrorData()
        {
            if(!File.Exists(alarmMsgIniFile)) return;
            
            object OutVal;
            INI.Read(alarmMsgIniFile, code.ToString(), "Name", typeof(string), "Please Add New Error Name", out OutVal);
            name = (string)OutVal;
            INI.Read(alarmMsgIniFile,code.ToString() , "Reason", typeof(string), "Please Add New Error Reason", out OutVal);
            reason = (string)OutVal;
            INI.Read(alarmMsgIniFile, code.ToString(), "Remedy", typeof(string), "Please Add New Error Remedy", out OutVal);
            remedy = (string)OutVal;
        }
        public object[] ToObj()
        {
            object[] objs = new object[] { time.ToString("MM/dd HH:mm:ss"), level, code, name, reason, remedy, note };
            return objs;
        }
        public object[] ToShortObj()
        {
            object[] objs = new object[] { time.ToString("HH:mm:ss"), level, code, name, reason };
            return objs;
        }
    }

}

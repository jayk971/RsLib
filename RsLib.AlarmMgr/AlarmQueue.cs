using RsLib.BaseType;
using RsLib.Common;
using RsLib.LogMgr;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
namespace RsLib.AlarmMgr
{
    public static class AlarmHistory
    {
        public static event Action<LockQueue<AlarmItem>> AlarmQueueUpdated;
        static LockQueue<AlarmItem> _Q = new LockQueue<AlarmItem>();
        static bool isInit = false;
        public static bool IsInit => isInit;
        public static AlarmTable alarmTable = new AlarmTable();
        public static void Initial(LangCode lang)
        {
            isInit = false;
            bool isLoadOK = alarmTable.Load(lang, '\t');
            isInit = isLoadOK;
        }
        public static void ResetAlarm()
        {
            if (_Q.Count != 0)
            {
                AlarmItem ErrorItem = _Q.Dequeue();
                Log.Add(string.Format("Error Reset : {0}", ErrorItem.Code), MsgLevel.Trace);
            }
            if (AlarmQueueUpdated != null) AlarmQueueUpdated(_Q);
        }
        public static void ResetAllAlarm()
        {
            if (_Q.Count != 0) _Q.Clear();
            Log.Add("Error Reset All", MsgLevel.Trace);
            AlarmQueueUpdated?.Invoke(_Q);
        }
        //public static void Add(int code, Exception ex = null)
        //{
        //    if (!Contain(code))
        //    {
        //        if (ex != null)
        //        {
        //            Add(code, ex.Message, ex);
        //        }
        //        else
        //        {
        //            AlarmItem error = new AlarmItem(code, ex);
        //            _Q.Enqueue(error);
        //            AlarmQueueUpdated?.Invoke(_Q);
        //        }
        //    }
        //}
        //public static void Add(int code, string msg, Exception ex = null)
        //{
        //    AlarmItem error = new AlarmItem(code, msg, ex);
        //    _Q.Enqueue(error);
        //    AlarmQueueUpdated?.Invoke(_Q);
        //}
        public static void Add(object objCode, Exception ex = null)
        {
            Add(objCode, "", ex);
        }
        public static void Add(object objCode, string note, Exception ex = null)
        {
            int code = (int)objCode;

            AlarmInfo info = alarmTable.GetInfo(code);
            AlarmItem error = new AlarmItem(code, info.Name, note, info.Reason, info.Remedy, ex);
            if (error.Level == MsgLevel.Alarm)
            {
                if (contain(code) == false)
                {
                    _Q.Enqueue(error);
                    if (error.Note == "") Log.Add($"Error Code : {error.Code} - {error.Name}", error.Level, ex);
                    else Log.Add($"Error Code : {error.Code} - {error.Name}, Note : {error.Note}", error.Level, ex);

                    AlarmQueueUpdated?.Invoke(_Q);
                }
            }
            else
            {
                _Q.Enqueue(error);
                if (error.Note == "") Log.Add($"Error Code : {error.Code} - {error.Name}", error.Level, ex);
                else Log.Add($"Error Code : {error.Code} - {error.Name}, Note : {error.Note}", error.Level, ex);

                AlarmQueueUpdated?.Invoke(_Q);
            }

        }
        static bool contain(int code)
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
        public int Code { get => code; }

        string name = "";
        public string Name { get => name; }
        MsgLevel level = MsgLevel.Warn;
        public MsgLevel Level { get => level; }
        string description = "";
        public string Description { get => description; }
        string reason = "";
        public string Reason { get => reason; }
        string remedy = "";
        public string Remedy = "";
        string note = "";
        public string Note { get => note; }
        public AlarmItem(int alarmCode, string _name, string _reason, string _remedy, Exception ex = null)
        {
            time = DateTime.Now;
            code = alarmCode;
            name = _name;
            reason = _reason;
            remedy = _remedy;

            if (((int)code % 1000) < 500) level = MsgLevel.Alarm;
            else level = MsgLevel.Warn;
            //if (note == "") Log.Add($"Error Code : {code} - {name}", level, ex);
            //else Log.Add($"Error Code : {code} -{name}, Note : {note}", level, ex);

        }
        //public AlarmItem(int alarmCode, string msg, Exception ex = null)
        //{
        //    time = DateTime.Now;
        //    code = alarmCode;
        //    if (((int)code % 1000) < 500) level = MsgLevel.Alarm;
        //    else level = MsgLevel.Warn;
        //    GetErrorData();
        //    note = msg;
        //    if (note == "") Log.Add($"Error Code : {code} - {name}", level, ex);
        //    else Log.Add($"Error Code : {code} - {name}, Note : {note}",level, ex);

        //}
        public AlarmItem(int alarmCode, string _name, string _note, string _reason, string _remedy, Exception ex = null)
        {
            time = DateTime.Now;
            code = alarmCode;
            name = _name;
            reason = _reason;
            remedy = _remedy;

            if (((int)code % 1000) < 500) level = MsgLevel.Alarm;
            else level = MsgLevel.Warn;
            note = _note;

        }

        public object[] ToObj()
        {
            object[] objs = new object[] { time.ToString("MM/dd HH:mm:ss"), level, code, name, reason, remedy, note };
            return objs;
        }
        public object[] ToBriefObj()
        {
            object[] objs = new object[] { time.ToString("HH:mm:ss"), level, code, name, reason, note };
            return objs;
        }
        public object[] ToShortObj()
        {
            object[] objs = new object[] { time.ToString("HH:mm:ss"), level, code, name, reason };
            return objs;
        }
    }

    [Serializable]
    public class AlarmTable
    {
        string mainFolder { get => System.Environment.CurrentDirectory; }
        string configFolder { get => $"{mainFolder}\\Config"; }
        string alarmMsgFile { get => $"{configFolder}\\AlarmMsg.txt"; }

        public Dictionary<int, AlarmInfo> Table = new Dictionary<int, AlarmInfo>();

        public AlarmInfo GetInfo(int code)
        {
            if (Table.ContainsKey(code))
            {
                return Table[code];
            }
            else
            {
                return new AlarmInfo(code);
            }
        }
        public void Clear()
        {
            Table.Clear();
        }
        public bool Load(LangCode lang, char splitChar)
        {
            Table.Clear();
            int langInt = (int)lang;
            int reasonI = (langInt + 1) * 2;
            int remedyI = reasonI + 1;
            if (!File.Exists(alarmMsgFile)) return false;
            using (StreamReader sr = new StreamReader(alarmMsgFile, Encoding.Default))
            {
                while (!sr.EndOfStream)
                {
                    string[] splitData = sr.ReadLine().Split(splitChar);
                    if (splitData.Length <= 0) continue;
                    string code = splitData[0];
                    int codeI = -999;
                    if (int.TryParse(code, out codeI))
                    {
                        if (splitData.Length > remedyI)
                        {
                            string name = splitData[1];
                            string reason = splitData[reasonI];
                            string remedy = splitData[remedyI];
                            Table.Add(codeI, new AlarmInfo(codeI, name, reason, remedy));
                        }
                        else
                        {
                            Table.Add(codeI, new AlarmInfo(codeI));
                        }
                    }
                    else
                    {
                        //not integer do nothing
                    }
                }
            }
            if (Table.Count > 0) return true;
            else return false;
        }
    }
    [Serializable]
    public class AlarmInfo
    {
        public int Code { get; set; } = -999;
        public string Name { get; set; } = "";
        public string Reason { get; set; } = "";
        public string Remedy { get; set; } = "";

        public AlarmInfo(int code)
        {
            Code = code;
            Name = "add new name";
            Reason = "add new reason";
            Remedy = "add new remedy";
        }
        public AlarmInfo(int code, string name, string reason, string remedy)
        {
            Code = code;
            Name = name;
            Reason = reason;
            Remedy = remedy;
        }
    }
}

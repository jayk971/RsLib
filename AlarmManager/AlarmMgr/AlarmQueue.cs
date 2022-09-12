using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using RsLib.LogMgr;
using RsLib.Common;
using RsLib.BaseType;
using System.IO;
using YamlDotNet.Serialization;
namespace RsLib.AlarmMgr
{
    public static class AlarmHistory
    {
        public static event Action<LockQueue<AlarmItem>> AlarmQueueUpdated;
        static LockQueue<AlarmItem> _Q = new LockQueue<AlarmItem>();
        static bool isInit = false;
        public static bool IsInit => isInit;
        static AlarmTable alarmTable = new AlarmTable();

        public static void CreateNewTableFile(List<int> errorCodes)
        {
            isInit = false;
            alarmTable.CreateNewTable(errorCodes);
            isInit = true;
        }
        public static void Initial()
        {
            isInit = false;
            alarmTable.Load();
            isInit = true;
        }
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
            if(error.Level == MsgLevel.Alarm)
            {
                if (contain(code) == false)
                {
                    _Q.Enqueue(error);
                    AlarmQueueUpdated?.Invoke(_Q);
                }
            }
            else
            {
                _Q.Enqueue(error);
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
        public AlarmItem(int alarmCode, string _name,string _reason, string _remedy,Exception ex = null)
        {
            time = DateTime.Now;
            code = alarmCode;
            name = _name;
            reason = _reason;
            remedy = _remedy;

            if (((int)code % 1000) < 500) level = MsgLevel.Alarm;
            else level = MsgLevel.Warning;
            if (note == "") Log.Add($"Error Code : {code} - {name}", level, ex);
            else Log.Add($"Error Code : {code} -{name}, Note : {note}", level, ex);

        }
        //public AlarmItem(int alarmCode, string msg, Exception ex = null)
        //{
        //    time = DateTime.Now;
        //    code = alarmCode;
        //    if (((int)code % 1000) < 500) level = MsgLevel.Alarm;
        //    else level = MsgLevel.Warning;
        //    GetErrorData();
        //    note = msg;
        //    if (note == "") Log.Add($"Error Code : {code} - {name}", level, ex);
        //    else Log.Add($"Error Code : {code} - {name}, Note : {note}",level, ex);

        //}
        public AlarmItem(int alarmCode, string _name, string _note,string _reason,string _remedy ,Exception ex = null)
        {
            time = DateTime.Now;
            code = alarmCode;
            name = _name;
            reason = _reason;
            remedy = _remedy;

            if (((int)code % 1000) < 500) level = MsgLevel.Alarm;
            else level = MsgLevel.Warning;
            note = _note;
            if (note == "") Log.Add($"Error Code : {code} - {name}", level, ex);
            else Log.Add($"Error Code : {code} - {name}, Note : {note}", level, ex);

        }

        public object[] ToObj()
        {
            object[] objs = new object[] { time.ToString("MM/dd HH:mm:ss"), level, code, name, reason, remedy, note };
            return objs;
        }
        public object[] ToBriefObj()
        {
            object[] objs = new object[] { time.ToString("HH:mm:ss"), level, code, name, reason,note };
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
        string alarmMsgIniFile { get => $"{configFolder}\\AlarmMsg.yaml"; }

        public Dictionary<int, AlarmInfo> Table = new Dictionary<int, AlarmInfo>();

        public AlarmInfo GetInfo(int code)
        {
            if(Table.ContainsKey(code))
            {
                return Table[code];
            }
            else
            {
                return new AlarmInfo(code);
            }
        }

        public void CreateNewTable(List<int> codes)
        {
            Table.Clear();
            for(int i = 0; i < codes.Count; i++)
            {
                Table.Add(codes[i], new AlarmInfo(codes[i]));
            }
            saveYaml();
        }
        public void Clear()
        {
            Table.Clear();
        }
        public void Load()
        {
            Table.Clear();
            loadYaml();
        }
        void loadYaml()
        {
            if (!File.Exists(alarmMsgIniFile)) saveYaml();

            string ReadData = "";
            using (StreamReader sr = new StreamReader(alarmMsgIniFile, Encoding.Default))
            {
                ReadData = sr.ReadToEnd();
            }
            var deserializer = new DeserializerBuilder()
                .Build();

            //yml contains a string containing your YAML
            var p = deserializer.Deserialize<AlarmTable>(ReadData);
            this.Table = p.Table.DeepClone();
        }
        void saveYaml()
        {
            if (!Directory.Exists(configFolder)) Directory.CreateDirectory(configFolder);
            using (StreamWriter sw = new StreamWriter(alarmMsgIniFile, false, Encoding.Default))
            {
                var serializer = new SerializerBuilder().Build();
                var yaml = serializer.Serialize(this);
                sw.WriteLine(yaml);
                sw.Flush();
            }
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
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.IO;
using YamlDotNet.Serialization;
using YamlDotNet.Serialization.NamingConventions;
using System.Threading;
using RsLib.LogMgr;
using RsLib.Common;
namespace RsLib.WatchFolder
{
    internal class Watcher
    {
        WatcherConfig config = new WatcherConfig();    
        FileSystemWatcher watcher = new FileSystemWatcher();

        //public delegate void delegateFileAdded(string filePath);
        internal event Action<string> AfterFileAdded;
        internal string Filter 
        { 
            get => config.FilterString;
            set
            {
                config.FilterString = value;
                config.SaveYaml();
                watcher.Filter = $"{config.FilterString}";
            }
        }
        internal string Folder
        {
            get => config.Folder;
            set
            {
                config.Folder = value;
                config.SaveYaml();
                watcher.Path = config.Folder;
            }
        }
        internal int TimeOutMs
        {
            get => config.TimeOutMilliSec;
            set
            {
                config.TimeOutMilliSec = value;
                config.SaveYaml();
            }
        }
        internal string CfgName
        {
            set
            {
                config.CfgName = value;
            }
        }
        bool isInitial = false;
        string detectFilePath = "";
        internal bool Init()
        {
            config.LoadYaml();
            if (!Directory.Exists(config.Folder))
            {
                isInitial = false;
                Log.Add($"Folder {config.Folder} Not Exist.", MsgLevel.Warning);
            }
            else
            {
                watcher.Path = config.Folder;
                watcher.Filter = $"{config.FilterString}";
                watcher.NotifyFilter = NotifyFilters.LastWrite;
                watcher.EnableRaisingEvents = false;
                watcher.Changed += Watcher_Changed;
                isInitial = true;
            }
            return isInitial;
        }
        private void Watcher_Changed(object sender, FileSystemEventArgs e)
        {
            watcher.EnableRaisingEvents = false;
            detectFilePath = e.FullPath;
            Log.Add($"File Detected. {detectFilePath}", MsgLevel.Info);
            bool isTimeout = FT_Functions.IsTimeOut(config.TimeOutMilliSec, isFileDone);
            if (!isTimeout) AfterFileAdded(detectFilePath);
            else
            {
                Log.Add($"File unlock time out. > {config.TimeOutMilliSec} ms", MsgLevel.Warning);
            }
            watcher.EnableRaisingEvents = true;
        }
        bool isFileDone()
        {
            return !FT_Functions.IsFileLocked(detectFilePath);
        }

        internal bool Start()
        {
            if (isInitial)
            {
                watcher.EnableRaisingEvents = true;
                return true;
            }
            else
            {
                if (Init())
                {
                    watcher.EnableRaisingEvents = true;
                    return true;
                }
                else
                {
                    Log.Add("Log service was not initialized.", MsgLevel.Warning);
                    return false;
                }
            }
        }
        internal void Stop()
        {
            watcher.EnableRaisingEvents = false;
        }
    }
    [Serializable]
    internal class WatcherConfig
    {
        public string FilterString = @"*_IMG_HEIGHT.bmp";
        public string Folder = @"C:\FTP\lj-x3d\image\MSInspect";
        public int TimeOutMilliSec = 5000; 
        bool isInit = false;
        [YamlIgnore]
        internal bool IsInit { get => isInit; }
        string config_file_name = "folder.watch";
        [YamlIgnore]
        internal string CfgFileName { get => config_file_name; }

        internal string CfgName
        {
            set
            {
                config_file_name = value + ".watch";
            }
        }
        internal void LoadYaml()
        {
            string folder = System.Environment.CurrentDirectory;
            string config_folder = $"{folder}\\Config";
            string file_path = $"{config_folder}\\{config_file_name}";

            if (!File.Exists(file_path)) SaveYaml();

            string ReadData = "";
            using (StreamReader sr = new StreamReader(file_path, Encoding.Default))
            {
                ReadData = sr.ReadToEnd();
            }
            var deserializer = new DeserializerBuilder()
                .WithNamingConvention(UnderscoredNamingConvention.Instance)  // see height_in_inches in sample yml 
                .Build();

            //yml contains a string containing your YAML
            var p = deserializer.Deserialize<WatcherConfig>(ReadData);
            this.FilterString = p.FilterString;
            this.Folder = p.Folder;
            this.TimeOutMilliSec = p.TimeOutMilliSec;
            isInit = true;
        }
        internal void SaveYaml()
        {
            string folder = System.Environment.CurrentDirectory;
            string config_folder = $"{folder}\\Config";
            string file_path = $"{config_folder}\\{config_file_name}";

            if (!Directory.Exists(config_folder)) Directory.CreateDirectory(config_folder);
            using (StreamWriter sw = new StreamWriter(file_path, false, Encoding.Default))
            {
                var serializer = new SerializerBuilder().WithNamingConvention(UnderscoredNamingConvention.Instance).Build();
                var yaml = serializer.Serialize(this);
                sw.WriteLine(yaml);
                sw.Flush();
            }
        }

    }
}

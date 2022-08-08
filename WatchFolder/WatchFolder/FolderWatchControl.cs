using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using RsLib.LogMgr;
using RsLib.Common;
namespace RsLib.WatchFolder
{
    public partial class FolderWatchControl : UserControl
    {
        Watcher watcher;
        public event Action<string> FileUpdated;
        public event Action<string> WatchedFolderChanged;
        public event Action<string> WatchedFilterChanged;

        public FolderWatchControl(string cfgName)
        {
            InitializeComponent();
            init(cfgName);
        }
        public void SetFilter(string filter)
        {
            watcher.Filter = filter;
        }
        public void SetFolder(string folder)
        {
            watcher.Folder = folder;
        }
        void init(string cfgName)
        {
            watcher = new Watcher();
            if (cfgName != "")
            {
                watcher.CfgName  = cfgName;
            }
            if (!watcher.Init())
            {
                pictureBox1.Visible = true;
            }
            watcher.AfterFileAdded += Watcher_AfterFileAdded; ;
            tbx_WatchFilter.Text = watcher.Filter;
            lbl_WatchedFolder.Text = watcher.Folder;
        }
        private void btn_ApplyFilter_Click(object sender, EventArgs e)
        {
            watcher.Filter = tbx_WatchFilter.Text;
            if (WatchedFilterChanged != null) WatchedFilterChanged(watcher.Filter);
        }

        private void btn_OpenFolder_Click(object sender, EventArgs e)
        {
            using (FolderBrowserDialog fbd = new FolderBrowserDialog())
            {
                fbd.SelectedPath = "d:\\";
                if(fbd.ShowDialog() == DialogResult.OK)
                {
                    watcher.Folder = fbd.SelectedPath;
                    lbl_WatchedFolder.Text = fbd.SelectedPath;
                    pictureBox1.Visible = false;
                    if (WatchedFolderChanged != null) WatchedFolderChanged(watcher.Folder);
                }
            }
        }


        private void FolderWatchControl_Load(object sender, EventArgs e)
        {

        }
        private void Watcher_AfterFileAdded(string filePath)
        {
            FileUpdated(filePath);
        }

        private void btn_StartMonitor_Click(object sender, EventArgs e)
        {
            StartMonitor();
        }

        private void btn_StopMonitor_Click(object sender, EventArgs e)
        {
            StopMonitor();
        }

        public void StartMonitor()
        {
            Log.Add($"Start Monitor Folder : {watcher.Folder}", MsgLevel.Trace);
            Log.Add($"Start Monitor Filter : {watcher.Filter}", MsgLevel.Trace);

            if(watcher.Start()) progressBar_RunStatus.Style = ProgressBarStyle.Marquee;

        }

        public void StopMonitor()
        {
            Log.Add($"Stop Monitor Folder : {watcher.Folder}", MsgLevel.Trace);

            progressBar_RunStatus.Style = ProgressBarStyle.Blocks;
            progressBar_RunStatus.Value = 0;
            watcher.Stop();
        }

        private void tbx_TimeOut_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = FT_Functions.int_Positive_KeyPress(e.KeyChar);
        }

        private void tableLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void btn_ApplyTimeOut_Click(object sender, EventArgs e)
        {
            watcher.TimeOutMs = int.Parse(tbx_TimeOut.Text);

        }
    }
}

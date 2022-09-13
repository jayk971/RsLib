using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using RsLib.WatchFolder;
using RsLib.LogMgr;
namespace TestWatchFolder
{
    using ArrayTuple = Tuple<double[], double[], double[]>;
    public partial class Form1 : Form
    {
        FolderWatchControl ftpWatch = new FolderWatchControl("FTP");
        //FolderWatchControl recipeWatch = new FolderWatchControl("Recipe");
        LogControl lc = new LogControl();
        public Form1()
        {
            InitializeComponent();

            tableLayoutPanel1.Controls.Add(ftpWatch, 0, 0);
            ftpWatch.Dock = DockStyle.Fill;
            ftpWatch.FileUpdated += FTPWatcher_AfterFileAdded;
            tableLayoutPanel1.SetRowSpan(lc, 2);
            tableLayoutPanel1.Controls.Add(lc, 1, 0);
            lc.Dock = DockStyle.Fill;
            Log.Start();
        }

        private void RecipeWatch_FileUpdated(string obj)
        {

        }

        private void FTPWatcher_AfterFileAdded(string filePath)
        {
            Log.Add(filePath, MsgLevel.Warn);
        }
    }
}

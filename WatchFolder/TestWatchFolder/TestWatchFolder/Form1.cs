using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using WatchFolder;
namespace TestWatchFolder
{
    using ArrayTuple = Tuple<double[], double[], double[]>;
    public partial class Form1 : Form
    {
        FolderWatchControl ftpWatch = new FolderWatchControl("FTP");
        FolderWatchControl recipeWatch = new FolderWatchControl("Recipe");

        public Form1()
        {
            InitializeComponent();
            tableLayoutPanel1.Controls.Add(ftpWatch, 0, 0);
            ftpWatch.Dock = DockStyle.Fill;
            ftpWatch.FileUpdated += FTPWatcher_AfterFileAdded;

            tableLayoutPanel1.Controls.Add(recipeWatch, 0, 1);
            recipeWatch.Dock = DockStyle.Fill;
            recipeWatch.FileUpdated += RecipeWatch_FileUpdated;

        }

        private void RecipeWatch_FileUpdated(string obj)
        {

        }

        private void FTPWatcher_AfterFileAdded(string filePath)
        {

        }
    }
}

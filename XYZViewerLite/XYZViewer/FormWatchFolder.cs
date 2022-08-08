using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using RsLib.WatchFolder;
namespace Automation
{
    public partial class FormWatchFolder : Form
    {
        FolderWatchControl _ply;
        FolderWatchControl _opt1;
        FolderWatchControl _opt2;
        FolderWatchControl _opt3;

        public FormWatchFolder(FolderWatchControl ply,
            FolderWatchControl opt1,
            FolderWatchControl opt2,
            FolderWatchControl opt3)
        {
            InitializeComponent();

            _ply = ply;
            _opt1 = opt1;
            _opt2 = opt2;
            _opt3 = opt3;

            _ply.Dock = DockStyle.Fill;
            tabPage_WatchPLY.Controls.Add(_ply);
            _opt1.Dock = DockStyle.Fill;
            tabPage_WatchOPT1.Controls.Add(_opt1);
            _opt2.Dock = DockStyle.Fill;
            tabPage_WatchOPT2.Controls.Add(_opt2);
            _opt3.Dock = DockStyle.Fill;
            tabPage_WatchOPT3.Controls.Add(_opt3);

        }
    }
}

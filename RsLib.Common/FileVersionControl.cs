using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using System.IO;
namespace RsLib.Common
{
    public partial class FileVersionControl : UserControl
    {
        public FileVersionControl(string folder)
        {
            InitializeComponent();
            showVersionInFolder(folder);
        }
        void showVersionInFolder(string folder)
        {
            dataGridView1.Rows.Clear();

            string[] files = Directory.GetFiles(folder);
            for (int i = 0; i < files.Length; i++)
            {
                string file = files[i];
                string fileName = Path.GetFileName(file);
                string ext = Path.GetExtension(file);
                if (ext.ToUpper() == ".DLL" || ext.ToUpper() == ".EXE")
                {
                    string fileVersion = FT_Functions.GetFileVersion(fileName);
                    dataGridView1.Rows.Add(fileName, fileVersion);
                }
            }
        }

    }
}

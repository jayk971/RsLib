using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using ConvertKeyBMP;
using System.IO;
using WatchFolder;
using LogMgr;
namespace TestForm
{
    public partial class Form1 : Form
    {
        LogControl lc = new LogControl();
        FolderWatchControl fwc = new FolderWatchControl("FTP");

        public Form1()
        {
            InitializeComponent();
            KeyBMP.Init();
            fwc.Dock = DockStyle.Fill;
            tableLayoutPanel1.SetColumnSpan(fwc, 2);
            tableLayoutPanel1.Controls.Add(fwc, 1, 0);
            lc.Dock = DockStyle.Fill;
            tableLayoutPanel1.SetColumnSpan(lc, 2);
            tableLayoutPanel1.Controls.Add(lc, 1, 1);

            fwc.FileUpdated += Fwc_FileUpdated;
        }

        private void Fwc_FileUpdated(string obj)
        {
            string xyzFile = obj.Replace(".bmp", ".xyz");
            KeyBMP.Load(obj);
            KeyBMP.SaveData(xyzFile);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            
            using (OpenFileDialog op = new OpenFileDialog())
            {
                op.Filter = $"Keyence Height BMP|*{KeyBMP.HeightExt}";
                if(op.ShowDialog() == DialogResult.OK)
                {
                    string file_path = op.FileName;
                    DialogResult dr =  MessageBox.Show("Load As XYZ Data ?", "Load Format", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
                    if(dr == DialogResult.Yes)
                    {
                        KeyBMP.Load(file_path);
                    }
                    else
                    {
                        KeyBMP.LoadInt(file_path);
                    }
                    using (SaveFileDialog sf = new SaveFileDialog())
                    {
                        sf.Filter = "XYZ File|*.xyz|CSV File|*.csv";
                        sf.FileName = Path.GetFileNameWithoutExtension(file_path);
                        if(sf.ShowDialog() == DialogResult.OK)
                        {
                            switch(sf.FilterIndex)
                            {
                                case 1:
                                    KeyBMP.SaveData(sf.FileName);
                                    MessageBox.Show($"XYZ File Saved.\n{sf.FileName}");
                                    break;
                                case 2:
                                    KeyBMP.SaveCSVData(sf.FileName);
                                    MessageBox.Show($"CSV File Saved.\n{sf.FileName}");
                                    break;
                                default:
                                    break;
                            }
                        }
                    }
                }
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using RsLib.ConvertKeyBMP;
using System.IO;
using RsLib.WatchFolder;
using RsLib.LogMgr;
using RsLib.Common;
using System.Diagnostics;
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
            Log.Start();
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
                    long normal = 0;
                    long ptr = 0;
                    long getPixel = 0;

                    DialogResult dr =  MessageBox.Show("Load As XYZ Data ?", "Load Format", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
                    if(dr == DialogResult.Yes)
                    {
                        Stopwatch stopwatch = new Stopwatch();

                        //stopwatch.Restart();
                        //KeyBMP.Load(file_path);
                        //stopwatch.Stop();
                        //normal = stopwatch.ElapsedMilliseconds;

                        stopwatch.Restart();
                        KeyBMP.Load_ptr(file_path);
                        stopwatch.Stop();
                        ptr = stopwatch.ElapsedMilliseconds;

                        //stopwatch.Restart();
                        //KeyBMP.Load_GetPixel(file_path);
                        //stopwatch.Stop();
                        //getPixel = stopwatch.ElapsedMilliseconds;

                        Log.Add($"{normal},{ptr},{getPixel}", MsgLevel.Info);
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

        private void btn_CalPixel_Click(object sender, EventArgs e)
        {
            int x = -1;
            int y = -1;
            bool parseX = int.TryParse(tbx_PxX.Text, out x);
            bool parseY = int.TryParse(tbx_PxY.Text, out y);
            if (parseX && parseY)
            {
                double[] xyz = KeyBMP.FindXYZ(x, y);
                lbl_RealX.Text = xyz[0].ToString("F3");
                lbl_RealY.Text = xyz[1].ToString("F3");
                lbl_RealZ.Text = xyz[2].ToString("F3");
            }
        }

        private void tbx_PxX_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = FT_Functions.double_Positive_KeyPress(e.KeyChar);
        }

        private void tbx_PxY_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = FT_Functions.double_Positive_KeyPress(e.KeyChar);
        }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

using RsLib.Common;
namespace RsLib.Encrypt
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void btn_Encrypt_Click(object sender, EventArgs e)
        {
            try
            {
                string encrypted = "";
                using (OpenFileDialog op = new OpenFileDialog())
                {
                    op.Filter = "Any text file|*.*";

                    if (op.ShowDialog() == DialogResult.OK)
                    {
                        using (StreamReader sr = new StreamReader(op.FileName))
                        {
                            string readToEnd = sr.ReadToEnd();
                            encrypted = FT_Functions.EncryptString(readToEnd);
                        }
                        using (SaveFileDialog sf = new SaveFileDialog())
                        {
                            sf.Filter = "Text file|*.txt";
                            if (sf.ShowDialog() == DialogResult.OK)
                            {
                                using (StreamWriter sw = new StreamWriter(sf.FileName, false, Encoding.Default))
                                {
                                    sw.Write(encrypted);
                                    sw.Flush();
                                }
                            }
                        }
                    }
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btn_Decrypt_Click(object sender, EventArgs e)
        {
            string decrypted = "";
            bool decryptOK = false;
            try
            {
                using (OpenFileDialog op = new OpenFileDialog())
                {
                    op.Filter = "Any text file|*.*";
                    if (op.ShowDialog() == DialogResult.OK)
                    {
                        using (StreamReader sr = new StreamReader(op.FileName))
                        {
                            string readToEnd = sr.ReadToEnd();
                            decryptOK = FT_Functions.DecryptString(readToEnd, out decrypted);
                        }
                        if (decryptOK)
                        {
                            using (SaveFileDialog sf = new SaveFileDialog())
                            {
                                sf.Filter = "Text file|*.txt";
                                if (sf.ShowDialog() == DialogResult.OK)
                                {
                                    using (StreamWriter sw = new StreamWriter(sf.FileName, false, Encoding.Default))
                                    {
                                        sw.Write(decrypted);
                                        sw.Flush();
                                    }
                                }
                            }
                        }
                        else
                        {
                            MessageBox.Show("Decrypt fail.", "Decrypt Fail", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}

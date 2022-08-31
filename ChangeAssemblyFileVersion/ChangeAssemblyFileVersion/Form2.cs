using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using RsLib.Common;
namespace ChangeAssemblyFileVersion
{
    public partial class Form2 : Form
    {
        string assemblyName = "";
        public event Action<string,int, int, int, int> VersionUpdated;
        public Form2()
        {
            InitializeComponent();
        }
        public void SetTextbox(string name, int main, int sub, int build, int revise)
        {
            assemblyName = name;
            tbx_Main.Text = main.ToString();
            tbx_Sub.Text = sub.ToString();
            tbx_Build.Text = build.ToString();
            tbx_Revise.Text = revise.ToString();

        }
        private void btn_Update_Click(object sender, EventArgs e)
        {
            int main = int.Parse(tbx_Main.Text);
            int sub = int.Parse(tbx_Sub.Text);
            int build = int.Parse(tbx_Build.Text);
            int revise = int.Parse(tbx_Revise.Text);

            VersionUpdated?.Invoke(assemblyName,main, sub, build, revise);
            this.Hide();
        }

        private void tbx_Main_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = FT_Functions.int_Positive_KeyPress(e.KeyChar);
        }

        private void btn_Exit_Click(object sender, EventArgs e)
        {
            this.Hide();
        }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace RsLib.Common
{
    public partial class FormProcessing : Form
    {
        public FormProcessing(string title)
        {
            InitializeComponent();
            this.Text = title;
            this.StartPosition = FormStartPosition.CenterParent;
        }
        public void SetMode(ProgressBarStyle progressBarStyle)
        {
            progressBar1.Style = progressBarStyle;
        }
        public void SetProgress(int value)
        {
            if (InvokeRequired)
            {
                Action<int> action = new Action<int>(SetProgress);
                Invoke(action, value);
            }
            else
            {
                int v = value;
                if (value < 0) v = 0;
                else if (value > 100) v = 100;
                else v = value;

                progressBar1.Value = v;
            }
        }

    }
}

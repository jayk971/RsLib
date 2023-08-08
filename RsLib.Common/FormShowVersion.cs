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
    public partial class FormShowVersion : Form
    {
        FileVersionControl _FileVerCtrl = null;
        public FormShowVersion(string folder)
        {
            InitializeComponent();
            _FileVerCtrl = new FileVersionControl(folder)
            { Dock = DockStyle.Fill };
            Controls.Add(_FileVerCtrl);
        }
    }
}

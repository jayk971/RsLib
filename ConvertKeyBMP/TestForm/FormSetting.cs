using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using ConvertKeyBMP;
namespace TestForm
{
    public partial class FormSetting : Form
    {
        public FormSetting(KeyBMPConfig cfg)
        {
            InitializeComponent();
            propertyGrid1.SelectedObject = cfg;
            propertyGrid1.Refresh();
        }

        private void FormSetting_Load(object sender, EventArgs e)
        {

        }

        private void propertyGrid1_Click(object sender, EventArgs e)
        {

        }

        private void propertyGrid1_PropertyValueChanged(object s, PropertyValueChangedEventArgs e)
        {

        }

        private void FormSetting_FormClosing(object sender, FormClosingEventArgs e)
        {
            KeyBMPConfig cfg = propertyGrid1.SelectedObject as KeyBMPConfig;
            cfg.SaveYaml();
        }
    }
}

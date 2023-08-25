using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RsLib.Display3D
{
    public partial class FormAddSelectPath : Form
    {
        public event Action SaveSelectPath;
        public event Action ClearSelectPath;
        public FormAddSelectPath()
        {
            InitializeComponent();
        }
        public void UpdateSelectPath(List<int> segmentIndex)
        {
            treeView1.Nodes.Clear();
            for (int i = 0; i < segmentIndex.Count; i++)
            {
                int sIndex = segmentIndex[i];
                treeView1.Nodes.Add("Select Path","Select Path");
                treeView1.Nodes["Select Path"].Nodes.Add(sIndex.ToString());
            }
        }
    }
}

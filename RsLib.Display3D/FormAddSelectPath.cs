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
        public event Action<bool> SaveSelectPathMOD;
        public event Action SaveSelectPathOPT;
        public event Action ClearSelectPath;
        public FormAddSelectPath()
        {
            InitializeComponent();
        }
        public void UpdateSelectPath(Dictionary<int, List<int>> segmentIndex)
        {
            treeView1.Nodes.Clear();
            foreach (var item in segmentIndex)
            {
                int selectObject = item.Key;
                List<int> selectLineIndex = item.Value;
                for (int i = 0; i < selectLineIndex.Count; i++)
                {
                    int sIndex = selectLineIndex[i];
                    if (treeView1.Nodes.ContainsKey(selectObject.ToString()) == false)
                        treeView1.Nodes.Add(selectObject.ToString(), selectObject.ToString());

                    treeView1.Nodes[selectObject.ToString()].Nodes.Add(sIndex.ToString());
                }
            }
            treeView1.ExpandAll();
        }

        private void toolBtn_Clear_Click(object sender, EventArgs e)
        {
            treeView1.Nodes.Clear();
            ClearSelectPath?.Invoke();
        }

        private void toolDropDownBtn_Save_Click(object sender, EventArgs e)
        {

        }

        private void saveOPTToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveSelectPathOPT?.Invoke();
        }

        private void saveMODToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveSelectPathMOD?.Invoke(false);
        }

        private void saveMODWithRobtargetToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveSelectPathMOD?.Invoke(true);
        }

        private void FormAddSelectPath_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = true;
            Hide();
        }
    }
}

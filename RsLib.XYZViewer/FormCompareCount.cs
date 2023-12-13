using System;
using System.Drawing;
using System.Windows.Forms;


using RsLib.PointCloudLib;
using RsLib.ToolControl;
namespace RsLib.XYZViewer
{
    public partial class FormCompareCount : Form
    {
        CompareCloudControl compareCtrl = new CompareCloudControl();
        ShoeSectionSimilarityControl sectionCtrl = new ShoeSectionSimilarityControl();
        public FormCompareCount()
        {
            InitializeComponent();
            compareCtrl.Dock = DockStyle.Fill;
            tableLayoutPanel1.Controls.Add(compareCtrl,0,0);
            sectionCtrl.Dock = DockStyle.Fill;
            tableLayoutPanel1.Controls.Add(sectionCtrl,1,0);
        }
        public void SetCompareResult(CompareCloudOption option)
        {
            compareCtrl.SetCompareResult(option);
        }

        public void SetCompare10Result(CompareSection10Option option)
        {
            sectionCtrl.SetSimilarity(option);
        }
    }
}

using System;
using System.Drawing;
using System.Windows.Forms;


using RsLib.PointCloudLib;
using RsLib.ToolControl;
namespace RsLib.XYZViewer
{
    public partial class FormCompareCount : Form
    {
        CompareCloudControl compareControl = new CompareCloudControl();
        public FormCompareCount()
        {
            InitializeComponent();
            compareControl.Dock = DockStyle.Fill;
            Controls.Add(compareControl);
        }
        public void SetCompareResult(CompareCloudOption option)
        {
            compareControl.SetCompareResult(option);
        }


    }
}

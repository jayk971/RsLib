using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using RsLib.Common;
using RsLib.PointCloudLib;

namespace RsLib.ToolControl
{
    public partial class ShoeSectionSimilarityControl : UserControl
    {
        ColorGradient cg = new ColorGradient(0, 100,true);
        List<Label> _lableList = new List<Label>();
        public ShoeSectionSimilarityControl()
        {
            InitializeComponent();
            _lableList.Add(lbl_1_1);
            _lableList.Add(lbl_1_2);
            _lableList.Add(lbl_1_3);
            _lableList.Add(lbl_1_4);
            _lableList.Add(lbl_1_5);
            _lableList.Add(lbl_2_1);
            _lableList.Add(lbl_2_2);
            _lableList.Add(lbl_2_3);
            _lableList.Add(lbl_2_4);
            _lableList.Add(lbl_2_5);


        }
        public void SetSimilarity(CompareSection10Option option)
        {
            if(InvokeRequired)
            {
                Action<CompareSection10Option> action = new Action<CompareSection10Option>(SetSimilarity);
                Invoke(action, option);
            }
            else
            {
                for (int i = 0; i < _lableList.Count; i++)
                {
                    _lableList[i].Text = option.GetPercent(i).ToString();
                    _lableList[i].BackColor = cg.GetColorFromGradient(option.GetPercent(i));
                }
            }

        }
    }
}

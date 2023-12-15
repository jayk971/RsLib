using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading;

using System.IO;
using RsLib.Common;
using System.Timers;

namespace RsLib.PointCloudLib
{
    public partial class ICPAlignControl : UserControl
    {
        ICPMatch  icpObj = new ICPMatch();
        System.Windows.Forms.Timer waitAlign = new System.Windows.Forms.Timer();
        bool isTimerRunng = false;
        int count = 0;
        /// <summary>
        /// Model cloud, Aligned Cloud
        /// </summary>
        public event Action<PointCloud, PointCloud> AfterAligned;
        public ICPAlignControl()
        {
            InitializeComponent();
            propertyGrid1.SelectedObject = icpObj.Setting;
            waitAlign.Tick += WaitAlign_Tick;
        }

        private void WaitAlign_Tick(object sender, EventArgs e)
        {
            isTimerRunng = true;
            if (count %5 == 0 )
            {
                lbl_Fitness.Text = "";
                lbl_RMS.Text = "";
            }
            lbl_Fitness.Text += "-";
            lbl_RMS.Text += "-";
            count++;
            isTimerRunng = false;
        }

        private void linkLbl_Model_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            linkLbl_Model.Text = openFile();
            if(File.Exists(linkLbl_Model.Text))
                icpObj.SetModel(linkLbl_Model.Text);
            else
            {
                linkLbl_Model.Text = "--";
            }
        }

        private void linkLbl_ToBeAligned_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            linkLbl_ToBeAligned.Text = openFile();
        }
        private string openFile()
        {
            using (OpenFileDialog op = new OpenFileDialog())
            {
                op.Filter = "XYZ cloud file|*.xyz|PLY file|*.ply";
                if (op.ShowDialog() == DialogResult.OK)
                {
                    return op.FileName;
                }
                else return "--";
            }
        }

        private void btn_SaveMatrix_Click(object sender, EventArgs e)
        {
            using (SaveFileDialog sf = new SaveFileDialog())
            {
                sf.Filter = "Matrix 4x4 file|*.m44";
                if(sf.ShowDialog() == DialogResult.OK)
                {
                    string saveFilePath = sf.FileName;
                    icpObj.SaveTransformMatrix(saveFilePath);
                }
            }
        }

        private void btn_Align_Click(object sender, EventArgs e)
        {
            waitAlign.Interval = 500;
            waitAlign.Enabled = true;
            count = 0;
            ThreadPool.QueueUserWorkItem(new WaitCallback(TdAlign), linkLbl_ToBeAligned.Text);
        }
        private void TdAlign(object obj)
        {
            string beAlignedFilePath = (string)obj;
            if (File.Exists(beAlignedFilePath))
            {
                icpObj.Match(beAlignedFilePath);
                waitAlign.Enabled = false;
                SpinWait.SpinUntil(() => isTimerRunng == false, 500);
                updateUI();
                AfterAligned?.Invoke(icpObj.GetModelCloud(), icpObj.GetAlignedPointCloud());
            }
        }
        private void updateUI()
        {
            if(this.InvokeRequired)
            {
                Action action = new Action(updateUI);
                this.Invoke(action);
            }
            else
            {
                lbl_Fitness.Text = icpObj.Fitness.ToString();
                lbl_RMS.Text = icpObj.RMS.ToString();
                richTextBox1.Text = PointCloudCommon.Matrix4x4ToString(icpObj.AlignMatrix, ' ');
            }
        }

        private void btn_SaveAligned_Click(object sender, EventArgs e)
        {
            using (SaveFileDialog sf = new SaveFileDialog())
            {
                sf.Filter = "XYZ file|*.xyz";
                if(sf.ShowDialog() == DialogResult.OK)
                {
                    string filePath = sf.FileName;
                    icpObj.SaveAlignTarget(filePath);
                }
            }
        }
    }
}

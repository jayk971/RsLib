using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using FT.MMF;
namespace FTMMF
{
    public partial class FormClient : Form
    {
        MMFClient m_Client = new MMFClient();
        List<CheckBox> DiList = new List<CheckBox>();
        List<CheckBox> DoList = new List<CheckBox>();
        public FormClient()
        {
            InitializeComponent();
            m_Client.GetDiValue += new MMFClient.DICallBack(m_Client_GetDiValue);
            m_Client.GetMsg += new MMFClient.MsgCallBack(m_Client_GetMsg);
            m_Client.GetDOValue +=new MMFClient.DOCallBack(m_Client_GetDOValue);
            DiList.Add(DI_0);
            DiList.Add(DI_1);
            DiList.Add(DI_2);
            DiList.Add(DI_3);
            DiList.Add(DI_4);
            DiList.Add(DI_5);
            DiList.Add(DI_6);
            DiList.Add(DI_7);


            DoList.Add(DO_0);
            DoList.Add(DO_1);
            DoList.Add(DO_2);
            DoList.Add(DO_3);
            DoList.Add(DO_4);
            DoList.Add(DO_5);
            DoList.Add(DO_6);
            DoList.Add(DO_7);
        }

        private void DO_0_CheckedChanged(object sender, EventArgs e)
        {
            CheckBox tmp = (CheckBox)sender;
            string str_Num = tmp.Name.Split('_')[1];
            int i_Num = int.Parse(str_Num);

            if (tmp.Checked) tmp.BackColor = Color.Lime;
            else tmp.BackColor = Color.Red;

            m_Client.DO = Common.SetIntSomeBit(m_Client.DO, i_Num, tmp.Checked);
        }
        private delegate void SetDi(int Di);
        private void m_Client_GetDiValue(int Di)
        {
            BeginInvoke(new SetDi(SetDIUI), Di);
        }
        private delegate void SetDo(int Do);
        private void m_Client_GetDOValue(int Do)
        {
            BeginInvoke(new SetDo(SetDOUI), Do);
        }
        private delegate void SetMsg(string Msg);
        private void m_Client_GetMsg(string Msg)
        {
            BeginInvoke(new SetMsg(SetMsgUI), Msg);
        }
        private void SetDOUI(int Do)
        {
            for (int i = 0; i < 8; i++)
            {
                DoList[i].Checked = Common.GetboolSomeBit(Do, i);
                if (DoList[i].Checked) DoList[i].BackColor = Color.LimeGreen;
                else DoList[i].BackColor = Color.Red;
            }
        }
        private void SetDIUI(int Di)
        {
            for (int i = 0; i < 8; i++)
            {
                DiList[i].Checked = Common.GetboolSomeBit(Di, i);
                if (DiList[i].Checked) DiList[i].BackColor = Color.LimeGreen;
                else DiList[i].BackColor = Color.Red;
            }
        }
        private void SetMsgUI(string Msg)
        {
            if (Msg != "")
            {
                string tmp = string.Format("{0 : HHmmss} {1}", DateTime.Now, Msg);
                listBox2.Items.Add(tmp);
                listBox2.SelectedIndex = listBox2.Items.Count - 1;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            m_Client.SendMessage(textBox1.Text);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            m_Client.Start();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            m_Client.Stop();
        }

        private void FormClient_FormClosing(object sender, FormClosingEventArgs e)
        {
            m_Client.Stop();
        }
    }
}

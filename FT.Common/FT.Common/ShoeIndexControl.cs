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
    public partial class ShoeIndexControl : UserControl
    {
        public ShoeSizeIndex CurrentIndex;
        public event Action<ShoeSizeIndex> ShoeSizeChanged;

        bool needRL = true;
        public bool NeedRL 
        {
            get => needRL;
            set
            {
                needRL = value;
                rbn_RL.Visible = value;
            }
        }
        List<ShoeSizeIndex> ExistedSize = new List<ShoeSizeIndex>();
        public ShoeIndexControl()
        {
            InitializeComponent();
            ChangeRadioButtonColor();

            rbn_L.Checked = true;
            rbn_90.Checked = true;
        }

        public void ResetExistSize()
        {
            ExistedSize.Clear();
            ChangeRadioButtonColor();
        }

        private void rbn_100_CheckedChanged(object sender, EventArgs e)
        {
            if (((RadioButton)sender).Checked)
            {
                if (FindSize())
                    ShoeSizeChanged?.Invoke(CurrentIndex);
            }
        }

        private void rbtnShoeSide_L_CheckedChanged(object sender, EventArgs e)
        {
            if (((RadioButton)sender).Checked)
            {
                ChangeRadioButtonColor();
                if (FindSize())
                    ShoeSizeChanged?.Invoke(CurrentIndex);
            }
        }
        public void SetSelectSize(ShoeSizeIndex sizeIndex)
        {
            bool? isRight = FT_Functions.SizeIndex2Side(sizeIndex);
            ShoeSize shoeSize = FT_Functions.SizeIndex2Size(sizeIndex);
            if (isRight == null)
            {
                rbn_RL.Checked = true;
            }
            else
            {
                if ((bool)isRight) rbn_R.Checked = true;
                else rbn_L.Checked = true;
            }
            foreach (Control c in tlp_Size.Controls)
            {
                if (c.GetType() == typeof(RadioButton))
                {
                    RadioButton rb = (RadioButton)c;
                    string sizeString = rb.Name.Remove(0, 3);
                    if(sizeString == shoeSize.ToString())
                    {
                        rb.Checked = true;
                        break;
                    }
                }
            }
        }
    
        bool FindSize()
        {
            foreach (Control c in tlp_Size.Controls)
            {
                if (c.GetType() == typeof(RadioButton))
                {
                    RadioButton rb = (RadioButton)c;
                    if (rb.Checked)
                    {
                        string c_Name = rb.Name;
                        string[] SplitData = c_Name.Split('_');
                        if (SplitData.Length == 2)
                        {
                            ShoeSize ss = ShoeSize._100;
                            if (Enum.TryParse<ShoeSize>(SplitData[1], out ss))
                            {
                                bool? IsRight;
                                if (rbn_L.Checked) IsRight = false;
                                else if (rbn_R.Checked) IsRight = true;
                                else IsRight = null;

                                CurrentIndex = FT_Functions.SizeSide2Index(ss, IsRight);
                                return true;
                            }
                            else
                            {
                                return false;
                            }
                        }
                    }                   
                }
            }
            return false;
        }

        private void chbx_ShowAllSize_CheckedChanged(object sender, EventArgs e)
        {
            rbn_30.Visible = chbx_ShowAllSize.Checked;
            rbn_35.Visible = chbx_ShowAllSize.Checked;
            rbn_40.Visible = chbx_ShowAllSize.Checked;
            rbn_45.Visible = chbx_ShowAllSize.Checked;
            rbn_50.Visible = chbx_ShowAllSize.Checked;
            rbn_55.Visible = chbx_ShowAllSize.Checked;
            rbn_60.Visible = chbx_ShowAllSize.Checked;
            rbn_65.Visible = chbx_ShowAllSize.Checked;
            rbn_160.Visible = chbx_ShowAllSize.Checked;
            rbn_170.Visible = chbx_ShowAllSize.Checked;
            rbn_180.Visible = chbx_ShowAllSize.Checked;

            if (chbx_ShowAllSize.Checked)
            {
                chbx_ShowAllSize.Text = "Show Common Size";
                gbx_Size.Text = "All Size";
            }
            else
            {
                chbx_ShowAllSize.Text = "Show All Size";
                gbx_Size.Text = "Common Size";
            }
        }
        public void SetExistSize(List<ShoeSizeIndex> ssis)
        {
            ExistedSize = ssis;
            ChangeRadioButtonColor();
        }
        public void AddExistSize(ShoeSizeIndex sizeIndex)
        {
            if (!ExistedSize.Contains(sizeIndex))
            {
                ExistedSize.Add(sizeIndex);
                ChangeRadioButtonColor();
            }
        }
        public void RemoveExistSize(ShoeSizeIndex sizeIndex)
        {
            if (ExistedSize.Contains(sizeIndex))
            {
                ExistedSize.Remove(sizeIndex);
                ChangeRadioButtonColor();
            }
        }
        void ChangeRadioButtonColor()
        {
            foreach (Control c in tlp_Size.Controls)
            {
                if (c.GetType() == typeof(RadioButton))
                {
                    RadioButton rb = (RadioButton)c;
                    rb.BackColor = Color.Red;
                    string c_Name = rb.Name;
                    string[] SplitData = c_Name.Split('_');
                    if (SplitData.Length == 2)
                    {
                        for (int i = 0; i < ExistedSize.Count; i++)
                        {
                            bool? IsRight = FT_Functions.SizeIndex2Side(ExistedSize[i]);
                            ShoeSize ss = FT_Functions.SizeIndex2Size(ExistedSize[i]);

                            if (rbn_RL.Checked)
                            {
                                if (IsRight == null)
                                {
                                    if (SplitData[1] == ((int)ss).ToString())
                                    {
                                        rb.BackColor = Color.Transparent;
                                        break;
                                    }
                                }
                            }
                            else if (rbn_R.Checked)
                            {
                                if (IsRight != null && (bool)IsRight == true)
                                {
                                    if (SplitData[1] == ((int)ss).ToString())
                                    {
                                        rb.BackColor = Color.Transparent;
                                        break;
                                    }
                                }
                            }
                            else
                            {
                                if (IsRight != null && (bool)IsRight == false)
                                {
                                    if (SplitData[1] == ((int)ss).ToString())
                                    {
                                        rb.BackColor = Color.Transparent;
                                        break;
                                    }
                                }
                            }
                        }
                    }
                }
            }

        }
    }
}

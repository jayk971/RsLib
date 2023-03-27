using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using RsLib.SerialPortLib.Properties;
namespace RsLib.SerialPortLib
{
    public partial class EJ1500Control : UserControl
    {
        EJ1500 _ej1500;
        public event Action PropertyUpdated;
        //public EJ1500Control()
        //{
        //    InitializeComponent();
        //}
        public EJ1500Control(EJ1500 eJ1500)
        {
            InitializeComponent();
            _ej1500 = eJ1500;
            _ej1500.WeightMeasured += _ej1500_WeightMeasured;
            propertyGrid1.SelectedObject = _ej1500.Setting;
            
        }

        private void _ej1500_WeightMeasured(int scaleIndex, double measuredWeight)
        {
            if(this.InvokeRequired)
            {
                Action<int, double> action = new Action<int, double>(_ej1500_WeightMeasured);
                this.Invoke(action, scaleIndex, measuredWeight);
            }
            else
            {
                lbl_MeasuredWeight.Text = measuredWeight.ToString();
            }
        }

        private void btn_Start_Click(object sender, EventArgs e)
        {
            if (_ej1500 == null) return;

            if (_ej1500.IsConnected == false)
            {
                _ej1500.Connect();
            }
            else
            {
                _ej1500.Disconnect();
            }
            btn_Start.Image = _ej1500.IsConnected ? Resources.toggle_on_96px : Resources.toggle_off_96px;
        }

        private void btn_Measure_Click(object sender, EventArgs e)
        {
            if (_ej1500 == null) return;
            _ej1500.Measure();
        }

        private void btn_Zero_Click(object sender, EventArgs e)
        {
            if (_ej1500 == null) return;
            _ej1500.SetSero();
        }

        private void propertyGrid1_PropertyValueChanged(object s, PropertyValueChangedEventArgs e)
        {
            PropertyUpdated?.Invoke();
        }
    }
}

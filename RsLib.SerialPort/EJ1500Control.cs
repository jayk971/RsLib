using RsLib.SerialPortLib.Properties;
using System;
using System.Windows.Forms;
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
            _ej1500.Connected += _ej1500_Connected;
            propertyGrid1.SelectedObject = _ej1500.Setting;

        }

        private void _ej1500_Connected(bool obj)
        {
            if (this.InvokeRequired)
            {
                Action<bool> action = new Action<bool>(_ej1500_Connected);
                this.Invoke(action, obj);
            }
            else
            {
                btn_Start.Image = obj ? Resources.toggle_on_96px : Resources.toggle_off_96px;
            }
        }

        private void _ej1500_WeightMeasured(int scaleIndex, double measuredWeight)
        {
            if (this.InvokeRequired)
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
        }
        private void btn_Zero_Click(object sender, EventArgs e)
        {

        }

        private void propertyGrid1_PropertyValueChanged(object s, PropertyValueChangedEventArgs e)
        {
            PropertyUpdated?.Invoke();
        }

        private void btn_GetWeight_Click(object sender, EventArgs e)
        {
            if (_ej1500 == null) return;
            _ej1500.Measure(false);
        }

        private void btn_ReZero_Click(object sender, EventArgs e)
        {
            if (_ej1500 == null) return;
            _ej1500.SetSero();
        }
    }
}

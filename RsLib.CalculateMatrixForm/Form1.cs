using RsLib.PointCloud.CalculateMatrix;
using System.Windows.Forms;
namespace RsLib.CalculateMatrixForm
{
    public partial class Form1 : Form
    {
        CalculateMatrixControl cmc = new CalculateMatrixControl();
        public Form1()
        {
            InitializeComponent();
            cmc.Dock = DockStyle.Fill;
            Controls.Add(cmc);
        }
    }
}

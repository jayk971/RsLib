using RsLib.PointCloudLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RsLib.DemoForm
{
    public interface ISipingPlugin
    {
        PointCloud GetAdjustModel();
        ObjectGroup GetAdjustPath();
        bool AdjustPath(PointCloud scanCloud, PointCloud modelCloud, ObjectGroup sipingPath);

        Control GetParameterControl();
    }
}

using OpenTK;
using OpenTK.Graphics.OpenGL;
using OpenTK.Input;
using RsLib.Common;
using RsLib.Display3D.Properties;
using RsLib.PointCloudLib;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Reflection;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ProgressBar;

namespace RsLib.Display3D
{

    public enum eDisplayObjectType : int
    {
        None = 0,
        PointCloud,
        Path,
        Vector_z,
        Vector_y,
        Vector_x,
        Point,
        Quad,
        Composite,
    }
    public enum ePointPickMode : int
    {
        None = 0,
        One,
        Two,
        Multiple,
        Draw
    }

}

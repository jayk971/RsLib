using System;

using System.Drawing;
using System.ComponentModel;
namespace RsLib.PointCloud
{
    [Serializable]
    public partial class DisplayOption:ObjectOption
    {
        public Color Color = Color.Gray;
        public float Size = 1.0f;
        public int Fine = 0;
        public bool IsDisplay = true;
        
    }
}

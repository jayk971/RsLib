using System;

using System.Drawing;
namespace RsLib.PointCloud
{
    [Serializable]
    public partial class DisplayUnit
    {
        public Color Color = Color.Gray;
        public float Size = 1.0f;
        public int Fine = 0;
        public bool IsDisplay = true;
        public DisplayUnit()
        {

        }
    }
}

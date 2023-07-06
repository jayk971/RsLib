using System;

using System.Drawing;
namespace RsLib.PointCloudLib
{
    [Serializable]
    public partial class DisplayOption : ObjectOption
    {
        public Color Color { get; set; } = Color.Gray;
        public float Size { get; set; } = 1.0f;
        public int Fine { get; set; } = 0;
        public bool IsDisplay { get; set; } = true;

    }
}

using OpenTK;
using OpenTK.Graphics.OpenGL;
using RsLib.Common;
using RsLib.Display3D.Properties;
using RsLib.LogMgr;
using RsLib.PointCloudLib;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Linq.Expressions;
using System.Security.AccessControl;
using System.Windows.Forms;
namespace RsLib.Display3D
{
    public partial class Display3DControl : UserControl
    {
        GLControl _glControl;
        private float _scale = 1.0f;
        private Vector3 _translation = Vector3.Zero;
        bool _leftButtonPressed = false;
        bool _rightButtonPressed = false;

        int _middleMouseCount = 0;
        Matrix4 _rotationMatrix = Matrix4.Identity;
        int _lastX, _lastY;
        int _lastRotX, _lastRotY;
        int _headList;
        int _maxDisplayList;
        ePointPickMode _pickMode = ePointPickMode.None;
        bool _haveClosestPoint = false;
        bool _haveSelectPath = false;
        int _CurrentSelectObjectIndex = -1;
        Dictionary<int, Object3D> _displayObject = new Dictionary<int, Object3D>();
        Dictionary<int, DisplayObjectOption> _displayOption = new Dictionary<int, DisplayObjectOption>();
        const float _closetDisLimit = 1.0f;
        bool _GLUpdateDone = true;
        public DisplayObjectOption CurrentSelectObjOption => GetDisplayObjectOption(_CurrentSelectObjectIndex);
        public Object3D CurrentSelectObj => GetDisplayObject(_CurrentSelectObjectIndex);
        Point3D _closetPoint = new Point3D();
        PointCloud _multiSelectPoints = new PointCloud();
        PointCloud _tempMultiSelectPoints = new PointCloud();

        Polyline _SelectPath = new Polyline();
        Polyline _DrawPath = new Polyline();


        Vector3 _maxPoint = new Vector3(float.MinValue, float.MinValue, float.MinValue);
        Vector3 _minPoint = new Vector3(float.MaxValue, float.MaxValue, float.MaxValue);
        Vector3 _oldAvgPoint = new Vector3();
        Vector3 _avgPoint
        {
            get
            {
                float avgX = (_maxPoint.X + _minPoint.X) / 2;
                float avgY = (_maxPoint.Y + _minPoint.Y) / 2;
                float avgZ = (_maxPoint.Z + _minPoint.Z) / 2;

                return new Vector3(avgX, avgY, avgZ);
            }
        }
        bool _isCheckedMaxMin = false;

        int _MultiSelectLineIndex_Start = -1;
        int _MultiSelectLineIndex_End = -1;

        int _MultiSelectPtIndex_Start = -1;
        int _MultiSelectPtIndex_End = -1;
        public bool LockRotate = false;
        bool _isColorDialogOpen = false;
        bool _isMouseOnCell = false;
        const int splitContainerPanel1MinSize = 300;
        const int _typeIndex = 0;
        const int _visibleIndex = 1;
        const int _nameIndex = 2;
        const int _idIndex = 3;
        const int _colorIndex = 4;
        const int _sizeIndex = 5;
        FormAddSelectPath formAdd = new FormAddSelectPath();
        // key : object ID, value : list of select paths;
        Dictionary<int, List<int>> _SelectedPathIndex = new Dictionary<int, List<int>>();
        int _CurrentSelectLineIndex = -1;
        bool enableMultipleSelect = false;
        public bool EnableMultipleSelect
        {
            get => enableMultipleSelect;
            set
            {
                enableMultipleSelect = value;
                MultipleSelectToolStripMenuItem.Visible = value;
            }
        }

        eCoordPlane currentPlane = eCoordPlane.None;

    }
}

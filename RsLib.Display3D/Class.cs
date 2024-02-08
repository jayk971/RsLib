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
using static RsLib.Display3D.Display3DControl;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ProgressBar;

namespace RsLib.Display3D
{
    public class DisplayObjectOption
    {
        public bool Visible = true;
        public int ID = 0;
        public string Name = "";
        public Color DrawColor = Color.White;
        public eDisplayObjectType DisplayType = eDisplayObjectType.None;
        float _drawSize = 5f;
        public float DrawSize
        {
            get => _drawSize;
            set
            {
                if (value < 1.0f) _drawSize = 1.0f;
                else _drawSize = value;
            }
        }
        public bool IsShowAtDataGrid = true;
        public bool IsSelectable = false;
        public Dictionary<eDisplayObjectType, DisplayObjectOption> SubOptions = new Dictionary<eDisplayObjectType, DisplayObjectOption>();
        public Dictionary<string, DisplayObjectOption> SubItemOptions = new Dictionary<string, DisplayObjectOption>();
        public DisplayObjectOption()
        {

        }
        public DisplayObjectOption(int id, eDisplayObjectType displayType, float drawSize, Color drawColor, bool isSelectable, bool isShowDataGrid = true)
        {
            ID = id;
            DrawColor = drawColor;
            DisplayType = displayType;
            DrawSize = drawSize;
            IsShowAtDataGrid = isShowDataGrid;
            IsSelectable = isSelectable;
            if (displayType == eDisplayObjectType.Path)
            {
                DisplayObjectOption op_Cloud = new DisplayObjectOption()
                {
                    ID = (int)eDisplayObjectType.PointCloud,
                    Name = eDisplayObjectType.PointCloud.ToString(),
                    DrawColor = drawColor,
                    DisplayType = eDisplayObjectType.PointCloud,
                    DrawSize = drawSize,
                    IsSelectable = isSelectable,
                    IsShowAtDataGrid = isShowDataGrid,
                    Visible = true
                };
                DisplayObjectOption op_Path = new DisplayObjectOption()
                {
                    ID = (int)eDisplayObjectType.Path,
                    Name = eDisplayObjectType.Path.ToString(),
                    DrawColor = drawColor,
                    DisplayType = eDisplayObjectType.Path,
                    DrawSize = drawSize,
                    IsSelectable = isSelectable,
                    IsShowAtDataGrid = isShowDataGrid,
                    Visible = true
                };
                DisplayObjectOption op_Vx = new DisplayObjectOption()
                {
                    ID = (int)eDisplayObjectType.Vector_x,
                    Name = eDisplayObjectType.Vector_x.ToString(),
                    DrawColor = Color.Red,
                    DisplayType = eDisplayObjectType.Vector_x,
                    DrawSize = 1.0f,
                    IsSelectable = false,
                    IsShowAtDataGrid = isShowDataGrid,
                    Visible = false
                };
                DisplayObjectOption op_Vy = new DisplayObjectOption()
                {
                    ID = (int)eDisplayObjectType.Vector_y,
                    Name = eDisplayObjectType.Vector_y.ToString(),
                    DrawColor = Color.LimeGreen,
                    DisplayType = eDisplayObjectType.Vector_y,
                    DrawSize = 1.0f,
                    IsSelectable = false,
                    IsShowAtDataGrid = isShowDataGrid,
                    Visible = false
                };
                DisplayObjectOption op_Vz = new DisplayObjectOption()
                {
                    ID = (int)eDisplayObjectType.Vector_z,
                    Name = eDisplayObjectType.Vector_z.ToString(),
                    DrawColor = Color.Blue,
                    DisplayType = eDisplayObjectType.Vector_z,
                    DrawSize = 1.0f,
                    IsSelectable = false,
                    IsShowAtDataGrid = isShowDataGrid,
                    Visible = false
                };
                SubOptions.Add(eDisplayObjectType.PointCloud, op_Cloud);
                SubOptions.Add(eDisplayObjectType.Path, op_Path);
                SubOptions.Add(eDisplayObjectType.Vector_x, op_Vx);
                SubOptions.Add(eDisplayObjectType.Vector_y, op_Vy);
                SubOptions.Add(eDisplayObjectType.Vector_z, op_Vz);
            }
        }

        public static DisplayObjectOption[] CreateDisplayOptionArray(int startID, int count, eDisplayObjectType defaultType, float defaultSize, bool isSelectable)
        {
            DisplayObjectOption[] output = new DisplayObjectOption[count];
            for (int i = 0; i < output.Length; i++)
            {
                output[i] = new DisplayObjectOption(startID+i, defaultType, defaultSize, Color.White, isSelectable);
            }
            return output;
        }
        public object[] ToDataGridRowObject()
        {
            return new object[] { DisplayType, Visible, Name, ID, "", DrawSize };
        }

        public TreeNode ToTreeNode()
        {
            TreeNode name = new TreeNode()
            {
                Name = Name,
                Text = Name,
                Checked = true
            };
            if(Name.Contains(":"))
            {
                string[] splitData = Name.Split(':');
                name.Text = splitData[splitData.Length - 1];
            }

            foreach (var item in SubOptions)
            {

                eDisplayObjectType subType = item.Key;
                DisplayObjectOption option = item.Value;
                TreeNode subTreeNode = new TreeNode()
                {
                    Name = subType.ToString(),
                    Text = subType.ToString()

                };
                if (subType == eDisplayObjectType.PointCloud || subType == eDisplayObjectType.Path)
                    subTreeNode.Checked = true;
                name.Nodes.Add(subTreeNode);
                //name.Nodes.Add(subType.ToString(), subType.ToString());
            }
            if (SubItemOptions.Count > 0)
            {
                TreeNode subItem = new TreeNode()
                {
                    Name = "SubItems",
                    Text = "SubItems",
                    Checked = true
                };
                foreach (var item in SubItemOptions)
                {
                    string subItemName = item.Key;
                    DisplayObjectOption option = item.Value;
                    subItem.Nodes.Add(option.ToTreeNode());
                }
                name.Nodes.Add(subItem);
            }
            return name;
        }
        public void ClearSubItems()
        {
            SubItemOptions.Clear();
        }
        public void AddSubItemOption(int id)
        {
            string subItemName = $"{Name}:{id}";
            DisplayObjectOption doo = new DisplayObjectOption(id, DisplayType, DrawSize, DrawColor, IsSelectable, IsShowAtDataGrid);
            doo.Name = subItemName;
            SubItemOptions.Add(subItemName, doo);
        }

        public void AddSubItemOption(int id, eDisplayObjectType displayType, float drawSize, Color drawColor, bool isSelectable, bool isShowDataGrid = true)
        {
            string subItemName = $"{Name}:{id}";
            DisplayObjectOption doo = new DisplayObjectOption(id, displayType, drawSize, drawColor, isSelectable, isShowDataGrid);
            doo.Name = subItemName;

            SubItemOptions.Add(subItemName,doo );
        }
    }

}

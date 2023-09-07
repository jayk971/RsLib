using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

using Newtonsoft.Json;
namespace RsLib.PointCloudLib
{
    public class JsonParser
    {

    }

    public class NikePath
    {
        public string SchemaVersion { get; set; }
        public string UID { get; set; }
        public CADInfo CAD { get; set; }
        public List<MachinesInfo> Machines { get; set; }
        public ProjectInfo Project { get; set; }
        public List<SelectionInfo> Selections { get; set; }

        public static NikePath Parse(string path)
        {
            string readData = "";
            using (StreamReader sr = new StreamReader(path))
            {
                readData = sr.ReadToEnd();
            }

            try
            {
                NikePath temp = JsonConvert.DeserializeObject<NikePath>(readData, new JsonSerializerSettings() { DefaultValueHandling = DefaultValueHandling.Ignore, NullValueHandling = NullValueHandling.Ignore });
                return temp;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<ObjectGroup> ToObjectGroups()
        {
            try
            {
                List<ObjectGroup> output = new List<ObjectGroup>();
                for (int i = 0; i < Selections.Count; i++)
                {
                    SelectionInfo selection = Selections[i];
                    output.Add(selection.ToObjectGroup(i));
                }

                return output;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
    [Serializable]
    public class CADInfo
    {
        public string Size { get; set; } = "";
        public string UID { get; set; } = "";
    }
    [Serializable]
    public class MachinesInfo
    {
        public string Label { get; set; } = "";
        public string Type { get; set; } = "";
    }
    [Serializable]
    public class ProjectInfo
    {
        public string Name { get; set; }
        public string Season { get; set; }
    }
    [Serializable]
    public class SelectionInfo
    {
        public string Machine { get; set; }
        public List<ToolPathInfo> toolPaths { get; set; }

        public ObjectGroup ToObjectGroup(int selectIndex)
        {
            ObjectGroup objects = new ObjectGroup($"Nike_{selectIndex}");
            for(int i = 0; i < toolPaths.Count;i++)
            {
                ToolPathInfo toolPathInfo = toolPaths[i];
                objects.Add($"Nike_{selectIndex}_{i}", toolPathInfo.ToPolyline(i));
            }
            return objects;
        }
    }
    [Serializable]
    public class ToolPathInfo
    { 
        public string StartType { get; set; }
        public string StopType { get; set; }
        public string Texture { get; set; }
        public List<Pose> Poses { get; set; }
        public Polyline ToPolyline(int lineIndex)
        {
            Polyline poly = new Polyline();
            LineOption lineOption = new LineOption();
            lineOption.LineIndex = lineIndex;
            poly.AddOption(lineOption);
            for(int i = 0;i  < Poses.Count; i++)
            {
                Pose p = Poses[i];
                poly.Add(p.Pt);
            }

            return poly;
        }
    }
    [Serializable]
    public class Pose
    {
        public double X { get; set; }
        public double Y { get; set; }
        public double Z { get; set; }
        public double[] XAxis { get; set; }
        public double[] YAxis { get; set;}
        public PointV3D Pt
        { 
            get
            {
                PointV3D p = new PointV3D();
                p.X = X;
                p.Y = Y;
                p.Z = Z;
                p.Vx = new Vector3D(XAxis[0], XAxis[1], XAxis[2]);
                p.Vy = new Vector3D(YAxis[0], YAxis[1], YAxis[2]);
                p.Vz = Vector3D.Cross(p.Vx, p.Vy);

                return p;
            } 
        }
    }



}





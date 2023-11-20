using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

using Newtonsoft.Json;
namespace RsLib.PointCloudLib
{
    //using CoordTuple = Tuple<List<double>, List<double>, List<double>>;
    //using SelectionTuple = Tuple<Tuple<List<double>, List<double>, List<double>>, Tuple<List<double>, List<double>, List<double>>, Tuple<List<double>, List<double>, List<double>>, Tuple<List<double>, List<double>, List<double>>,List<int>>;
    [Serializable]
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
        //public SelectionTuple ToArrayTuple()
        //{
        //    SelectionInfo selection = Selections[0];

        //    return selection.ToArrayTuple();
        //}
        public PoseList ToPoseList()
        {
            SelectionInfo selection = Selections[0];

            return selection.ToPoseList();
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
            for (int i = 0; i < toolPaths.Count; i++)
            {
                ToolPathInfo toolPathInfo = toolPaths[i];
                objects.Add($"Nike_{selectIndex}_{i}", toolPathInfo.ToPolyline(i));
            }
            return objects;
        }
        //public SelectionTuple ToArrayTuple()
        //{
        //    List<double> x = new List<double>();
        //    List<double> y = new List<double>();
        //    List<double> z = new List<double>();

        //    List<double> xx = new List<double>();
        //    List<double> xy = new List<double>();
        //    List<double> xz = new List<double>();

        //    List<double> yx = new List<double>();
        //    List<double> yy = new List<double>();
        //    List<double> yz = new List<double>();

        //    List<double> zx = new List<double>();
        //    List<double> zy = new List<double>();
        //    List<double> zz = new List<double>();

        //    List<int> iLi = new List<int>();

        //    for (int i = 0; i < toolPaths.Count; i++)
        //    {
        //        ToolPathInfo toolPathInfo = toolPaths[i];
        //        SelectionTuple tuple = toolPathInfo.ToArrayTuple(i);
        //        x.AddRange(tuple.Item1.Item1);
        //        y.AddRange(tuple.Item1.Item2);
        //        z.AddRange(tuple.Item1.Item3);

        //        xx.AddRange(tuple.Item2.Item1);
        //        xy.AddRange(tuple.Item2.Item2);
        //        xz.AddRange(tuple.Item2.Item3);

        //        yx.AddRange(tuple.Item3.Item1);
        //        yy.AddRange(tuple.Item3.Item2);
        //        yz.AddRange(tuple.Item3.Item3);

        //        zx.AddRange(tuple.Item4.Item1);
        //        zy.AddRange(tuple.Item4.Item2);
        //        zz.AddRange(tuple.Item4.Item3);

        //        iLi.AddRange(tuple.Item5);

        //    }
        //    CoordTuple tuplePos = new CoordTuple(x, y, z);
        //    CoordTuple tupleVX = new CoordTuple(xx, xy, xz);
        //    CoordTuple tupleVY = new CoordTuple(yx, yy, yz);
        //    CoordTuple tupleVZ = new CoordTuple(zx, zy, zz);

        //    return new SelectionTuple(tuplePos, tupleVX, tupleVY, tupleVZ, iLi); ;
        //}
        public PoseList ToPoseList()
        {
            PoseList pl = new PoseList();
            for (int i = 0; i < toolPaths.Count; i++)
            {
                ToolPathInfo toolPathInfo = toolPaths[i];
                PoseList tuple = toolPathInfo.ToPoseList(i);
                pl.Add(tuple);

            }
            return pl;
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
            for (int i = 0; i < Poses.Count; i++)
            {
                Pose p = Poses[i];
                poly.Add(p.ToPoint3D(lineIndex,i));
            }

            return poly;
        }
        //public SelectionTuple ToArrayTuple(int lineIndex)
        //{
        //    List<double> x = new List<double>();
        //    List<double> y = new List<double>();
        //    List<double> z = new List<double>();

        //    List<double> xx = new List<double>();
        //    List<double> xy = new List<double>();
        //    List<double> xz = new List<double>();

        //    List<double> yx = new List<double>();
        //    List<double> yy = new List<double>();
        //    List<double> yz = new List<double>();

        //    List<double> zx = new List<double>();
        //    List<double> zy = new List<double>();
        //    List<double> zz = new List<double>();

        //    List<int> iLi = new List<int>();
        //    for (int i = 0; i < Poses.Count; i++)
        //    {
        //        Pose p = Poses[i];
        //        x.Add(p.X);
        //        y.Add(p.Y);
        //        z.Add(p.Z);

        //        Vector3D vx = p.VectorX;
        //        Vector3D vy = p.VectorY;
        //        Vector3D vz = p.VectorZ;

        //        xx.Add(vx.X);
        //        xy.Add(vx.Y);
        //        xz.Add(vx.Z);

        //        yx.Add(vy.X);
        //        yy.Add(vy.Y);
        //        yz.Add(vy.Z);

        //        zx.Add(vz.X);
        //        zy.Add(vz.Y);
        //        zz.Add(vz.Z);

        //        iLi.Add(lineIndex);
        //    }
        //    CoordTuple tuplePos = new CoordTuple(x, y, z);
        //    CoordTuple tupleVX = new CoordTuple(xx, xy, xz);
        //    CoordTuple tupleVY = new CoordTuple(yx, yy, yz);
        //    CoordTuple tupleVZ = new CoordTuple(zx, zy, zz);

        //    return new SelectionTuple(tuplePos, tupleVX, tupleVY, tupleVZ,iLi);;
        //}

        public PoseList ToPoseList(int lineIndex)
        {
            PoseList poseList = new PoseList();
            for (int i = 0; i < Poses.Count; i++)
            {
                Pose p = Poses[i];

                poseList.Add(p, lineIndex);
            }
            return poseList;
        }

    }
    [Serializable]
    public class Pose
    {
        public double X { get; set; }
        public double Y { get; set; }
        public double Z { get; set; }
        public double[] XAxis { get; set; }
        [JsonIgnore]
        public Vector3D VectorX => new Vector3D(XAxis[0], XAxis[1], XAxis[2]);
        public double[] YAxis { get; set; }
        [JsonIgnore]
        public Vector3D VectorY => new Vector3D(YAxis[0], YAxis[1], YAxis[2]);
        [JsonIgnore]
        public Vector3D VectorZ => Vector3D.Cross(VectorX, VectorY);
        [JsonIgnore]
        public double[] ZAxis => new double[] { VectorZ.X, VectorZ.Y, VectorZ.Z };
        public PointV3D ToPoint3D(int lineIndex, int ptIndex)
        {

            PointV3D p = new PointV3D();
            p.X = X;
            p.Y = Y;
            p.Z = Z;
            p.Vx = new Vector3D(XAxis[0], XAxis[1], XAxis[2]);
            p.Vy = new Vector3D(YAxis[0], YAxis[1], YAxis[2]);
            p.Vz = Vector3D.Cross(p.Vx, p.Vy);
            LineOption lineOption = new LineOption();
            lineOption.LineIndex = lineIndex;
            LocateIndexOption locateIndexOption = new LocateIndexOption();
            locateIndexOption.Index = ptIndex;
            p.AddOption(lineOption);
            p.AddOption(locateIndexOption);

            return p;
        }
    }

    [Serializable]
    public class PoseList
    {
        public List<double> X = new List<double>();
        public double[] ArrayX => X.ToArray();
        public List<double> Y = new List<double>();
        public double[] ArrayY => Y.ToArray();
        public List<double> Z = new List<double>();
        public double[] ArrayZ => Z.ToArray();

        public List<double> Xx = new List<double>();
        public double[] ArrayXx => Xx.ToArray();
        public List<double> Xy = new List<double>();
        public double[] ArrayXy => Xy.ToArray();
        public List<double> Xz = new List<double>();
        public double[] ArrayXz => Xz.ToArray();

        public List<double> Yx = new List<double>();
        public double[] ArrayYx => Yx.ToArray();

        public List<double> Yy = new List<double>();
        public double[] ArrayYy => Yy.ToArray();

        public List<double> Yz = new List<double>();
        public double[] ArrayYz => Yz.ToArray();

        public List<double> Zx = new List<double>();
        public double[] ArrayZx => Zx.ToArray();

        public List<double> Zy = new List<double>();
        public double[] ArrayZy => Zy.ToArray();

        public List<double> Zz = new List<double>();
        public double[] ArrayZz => Zz.ToArray();

        public List<int> Index = new List<int>();
        public int[] ArrayIndex => Index.ToArray();
        public int Count => Index.Count;
        public PoseList()
        {

        }
        public PoseList(List<double> x,
    List<double> y,
    List<double> z,
    List<double> zx,
    List<double> zy,
    List<double> zz,
    List<int> i)
        {
            Add(x, y, z, zx, zy, zz, i);
        }
        public void Add(Point3D pt,int index)
        {
            X.Add(pt.X);
            Y.Add(pt.Y);
            Z.Add(pt.Z);
            Index.Add(index);
        }
        public void Add(Pose pt,int index)
        {
            X.Add(pt.X);
            Y.Add(pt.Y);
            Z.Add(pt.Z);
            AddVector(pt.VectorX, eRefAxis.X);
            AddVector(pt.VectorY, eRefAxis.Y);
            AddVector(pt.VectorZ, eRefAxis.Z);
            Index.Add(index);
        }
        public void Add(PointV3D pt, int index)
        {
            X.Add(pt.X);
            Y.Add(pt.Y);
            Z.Add(pt.Z);
            AddVector(pt.Vx, eRefAxis.X);
            AddVector(pt.Vy, eRefAxis.Y);
            AddVector(pt.Vz, eRefAxis.Z);
            Index.Add(index);
        }
        public void Add(PoseList otherPoseList)
        {
            X.AddRange(otherPoseList.X);
            Y.AddRange(otherPoseList.Y);
            Z.AddRange(otherPoseList.Z);

            Xx.AddRange(otherPoseList.Xx);
            Xy.AddRange(otherPoseList.Xy);
            Xz.AddRange(otherPoseList.Xz);

            Yx.AddRange(otherPoseList.Yx);
            Yy.AddRange(otherPoseList.Yy);
            Yz.AddRange(otherPoseList.Yz);

            Zx.AddRange(otherPoseList.Zx);
            Zy.AddRange(otherPoseList.Zy);
            Zz.AddRange(otherPoseList.Zz);

            Index.AddRange(otherPoseList.Index);

        }
        public void Add(List<double>x, 
            List<double> y, 
            List<double> z, 
            List<double> xx, 
            List<double> xy, 
            List<double> xz, 
            List<double> yx, 
            List<double> yy, 
            List<double> yz, 
            List<double> zx,
            List<double> zy,
            List<double> zz, 
            List<int> i)
        {
            X.AddRange(x);
            Y.AddRange(y);
            Z.AddRange(z);

            Xx.AddRange(xx);
            Xy.AddRange(xy);
            Xz.AddRange(xz);

            Yx.AddRange(yx);
            Yy.AddRange(yy);
            Yz.AddRange(yz);

            Zx.AddRange(zx);
            Zy.AddRange(zy);
            Zz.AddRange(zz);

            Index.AddRange(i);

        }
        public void Add(List<double> x,
    List<double> y,
    List<double> z,
    List<double> zx,
    List<double> zy,
    List<double> zz,
    List<int> i)
        {
            X.AddRange(x);
            Y.AddRange(y);
            Z.AddRange(z);

            Zx.AddRange(zx);
            Zy.AddRange(zy);
            Zz.AddRange(zz);

            Index.AddRange(i);

        }

        public void Add(double[] x,double[] y,double[] z)
        {
            X.AddRange(x);
            Y.AddRange(y);
            Z.AddRange(z);
        }
        public void AddVector(Vector3D v,eRefAxis refAxis)
        {
            if(refAxis== eRefAxis.X)
            {
                Xx.Add(v.X);
                Xy.Add(v.Y);
                Xz.Add(v.Z);
            }
            else if(refAxis == eRefAxis.Y)
            {
                Yx.Add(v.X);
                Yy.Add(v.Y);
                Yz.Add(v.Z);
            }
            else if(refAxis == eRefAxis.Z)
            {
                Zx.Add(v.X);
                Zy.Add(v.Y);
                Zz.Add(v.Z);
            }

        }
        public Vector3D GetVectorZ(int Ptindex)
        {
            if (Count > Ptindex)
            {
                return new Vector3D(Zx[Ptindex], Zy[Ptindex], Zz[Ptindex]);
            }
            else
                return null;
        }
        public Vector3D GetVectorY(int Ptindex)
        {
            if (Count > Ptindex)
            {
                return new Vector3D(Yx[Ptindex], Yy[Ptindex], Yz[Ptindex]);
            }
            else
                return null;
        }

        public Vector3D GetVectorX(int Ptindex)
        {
            if (Count > Ptindex)
            {
                return new Vector3D(Xx[Ptindex], Xy[Ptindex], Xz[Ptindex]);
            }
            else
                return null;
        }

    }
}









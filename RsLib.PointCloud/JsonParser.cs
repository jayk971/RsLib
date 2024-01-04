﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using Accord.Collections;
using Newtonsoft.Json;
using RsLib.Common;

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

        public NikePath() 
        {
            
        }
        public NikePath(NikePath src)
        {
            SchemaVersion = src.SchemaVersion;
            UID = src.UID;
            CAD = src.CAD.DeepClone();
            Machines = src.Machines.DeepClone();
            Project = src.Project.DeepClone();
            Selections = src.Selections.DeepClone();
        }
        public NikePath(NikePath src,bool enableClonePath)
        {
            SchemaVersion = src.SchemaVersion;
            UID = src.UID;
            CAD = src.CAD.DeepClone();
            Machines = src.Machines.DeepClone();
            Project = src.Project.DeepClone();
            if (enableClonePath) Selections = src.Selections.DeepClone();
            else Selections = new List<SelectionInfo>();
        }

        public static NikePath Parse(string path)
        {
            string readData = "";
            using (StreamReader sr = new StreamReader(path))
            {
                readData = sr.ReadToEnd();
            }

            try
            {
                NikePath temp = JsonConvert.DeserializeObject<NikePath>(readData,
                    new JsonSerializerSettings() 
                    { 
                        DefaultValueHandling = DefaultValueHandling.Ignore, 
                        NullValueHandling = NullValueHandling.Ignore,
                        MissingMemberHandling = MissingMemberHandling.Ignore,
                    });
                return temp;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void SaveJson(string filePath)
        {
            try
            {
                string writeData = JsonConvert.SerializeObject(this,
                    new JsonSerializerSettings() 
                    { 
                        DefaultValueHandling= DefaultValueHandling.Ignore,
                        NullValueHandling = NullValueHandling.Ignore, 
                        MissingMemberHandling = MissingMemberHandling.Ignore
                    });
                using (StreamWriter sw = new StreamWriter(filePath,false,Encoding.Default,65535))
                {
                    sw.WriteLine(writeData);
                    sw.Flush();
                }
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
        public string Size { get; set; } = "1.0";
        public string UID { get; set; } = "UID";
    }
    [Serializable]
    public class MachinesInfo
    {
        public string Label { get; set; } = "Cutter";
        public string Type { get; set; } = "Cutter_Z69";
    }
    [Serializable]
    public class ProjectInfo
    {
        public string Name { get; set; } = "Test";
        public string Season { get; set; } = "TBD";
    }
    [Serializable]
    public class SelectionInfo
    {
        public string Machine { get; set; } = "Cutter_Z69";
        public List<ToolPathInfo> toolPaths { get; set; }
        [JsonIgnore]
        public int PoseCount
        {
            get
            {
                if (toolPaths == null) return 0;
                else
                {
                    int count = 0;
                    for (int i = 0; i < toolPaths.Count; i++)
                    {
                        count += toolPaths[i].PoseCount;
                    }
                    return count;
                }
            }
        }


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

        [JsonIgnore]
        public int PoseCount => Poses.Count;
        public Polyline ToPolyline(int lineIndex)
        {
            Polyline poly = new Polyline();
            LineOption lineOption = new LineOption
            {
                LineIndex = lineIndex
            };
            poly.AddOption(lineOption);
            for (int i = 0; i < Poses.Count; i++)
            {
                Pose p = Poses[i];
                PointV3D pt = p.ToPoint3D(lineIndex, i);
                poly.Add(pt);
            }

            for (int i = 0; i < poly.Count; i++)
            {
                if (poly.Points[i] is PointV3D pt)
                {
                    if(i == poly.Count-1)
                    {
                        if (pt.Vx != null)
                        {
                            if (pt.Vx.L == 0) pt.Vx = (poly.Points[i - 1] as PointV3D).Vx.DeepClone();
                        }
                        if (pt.Vy != null)
                        {
                            if (pt.Vy.L == 0) pt.Vy = (poly.Points[i - 1] as PointV3D).Vy.DeepClone();
                        }
                        if (pt.Vz != null)
                        {
                            if (pt.Vz.L == 0) pt.Vz = (poly.Points[i - 1] as PointV3D).Vz.DeepClone();
                        }
                    }
                    else
                    {
                        if (pt.Vx != null)
                        {
                            if (pt.Vx.L == 0) pt.Vx = (poly.Points[i + 1] as PointV3D).Vx.DeepClone();
                        }
                        if (pt.Vy != null)
                        {
                            if (pt.Vy.L == 0) pt.Vy = (poly.Points[i + 1] as PointV3D).Vy.DeepClone();
                        }
                        if (pt.Vz != null)
                        {
                            if (pt.Vz.L == 0) pt.Vz = (poly.Points[i + 1] as PointV3D).Vz.DeepClone();
                        }
                    }
                }
            }
            for (int i = 0; i < poly.Count; i++)
            {
                if (poly.Points[i] is PointV3D pt)
                {
                    if(pt.Vx == null) pt.Vx = Vector3D.Cross(pt.Vy, pt.Vz);
                    if(pt.Vy == null) pt.Vy = Vector3D.Cross(pt.Vz,pt.Vx);
                    if (pt.Vz == null) pt.Vz = Vector3D.Cross(pt.Vx, pt.Vy);
                }
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
        public  List<double> ToXVectorLength()
        {
            List<double> output = new List<double>();
            for (int i = 0; i < Poses.Count; i++)
            {
                output.Add(Poses[i].XLength);
            }
            return output;
        }
        public List<double> ToYVectorLength()
        {
            List<double> output = new List<double>();
            for (int i = 0; i < Poses.Count; i++)
            {
                output.Add(Poses[i].YLength);
            }
            return output;
        }
        public List<double> ToZVectorLength()
        {
            List<double> output = new List<double>();
            for (int i = 0; i < Poses.Count; i++)
            {
                output.Add(Poses[i].ZLength);
            }
            return output;
        }
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
        public double XLength => GetVectorLength(XAxis);
        [JsonIgnore]
        public Vector3D VectorX
        {
            get
            {
                if (XAxis != null) return new Vector3D(XAxis[0], XAxis[1], XAxis[2]);
                else return new Vector3D();
            }
        }

        public double[] YAxis { get; set; }
        [JsonIgnore]
        public double YLength => GetVectorLength(YAxis);

        [JsonIgnore]
        public Vector3D VectorY
        {
            get
            {
                if (YAxis != null) return new Vector3D(YAxis[0], YAxis[1], YAxis[2]);
                else return new Vector3D();
            }
        }
        public double[] ZAxis { get; set; }
        [JsonIgnore]
        public double ZLength => GetVectorLength(ZAxis);

        [JsonIgnore]
        public Vector3D VectorZ
        {
            get
            {
                if (ZAxis != null) return new Vector3D(ZAxis[0], ZAxis[1], ZAxis[2]);
                else return new Vector3D();
            }
        }
        public Pose()
        {

        }
        public Pose(Pose src)
        {
            X = src.X;
            Y = src.Y;
            Z = src.Z;
            XAxis = src.XAxis;
            YAxis = src.YAxis;
            ZAxis = src.ZAxis;
        }
        public PointV3D ToPoint3D(int lineIndex, int ptIndex)
        {            
            PointV3D p = new PointV3D
            {
                X = X,
                Y = Y,
                Z = Z
            };
            if (XAxis != null) p.Vx = VectorX;
            else p.Vx = null;

            if (YAxis != null) p.Vy = VectorY;
            else p.Vy = null;

            if (ZAxis != null)p.Vz = VectorZ;
            else p.Vz = null;

            LineOption lineOption = new LineOption
            {
                LineIndex = lineIndex
            };
            LocateIndexOption locateIndexOption = new LocateIndexOption
            {
                Index = ptIndex
            };
            p.AddOption(lineOption);
            p.AddOption(locateIndexOption);

            return p;
        }
        public static double GetVectorLength(double[] arr)
        {
            if (arr == null) return 0;
            else  return Math.Sqrt(Math.Pow(arr[0], 2) + Math.Pow(arr[1], 2) + Math.Pow(arr[2], 2));
        }
        public Pose ProjectToSurface(KDTree<int> targetTree,bool useSurfaceNormal = false)
        {
            PointV3D projectedP = PointCloudCommon.ProjectToSurface(X, Y, Z, targetTree,2);
            Pose p = new Pose()
            {
                X = projectedP.X,
                Y = projectedP.Y,
                Z = projectedP.Z,

            };
            if (useSurfaceNormal)
            {
                p.XAxis = projectedP.Vx.ToArray();
                p.YAxis = projectedP.Vy.ToArray();
                p.ZAxis = projectedP.Vz.ToArray();
            }
            else
            {
                p.XAxis = XAxis;
                p.YAxis = YAxis;
                p.ZAxis = ZAxis;
            }
            return p;
        }
        public static Pose operator +(Pose pose1,Pose pose2)
        {
            Pose p = new Pose(pose1);
            p.X = pose1.X + pose2.X;
            p.Y = pose1.Y + pose2.Y;
            p.Z = pose1.Z + pose2.Z;
            return p;
        }
        public static Pose operator +(Pose pose1, Vector3D vector)
        {
            Pose p = new Pose(pose1);
            p.X = pose1.X + vector.X;
            p.Y = pose1.Y + vector.Y;
            p.Z = pose1.Z + vector.Z;
            return p;
        }
        public Pose GetExtendPose(eRefAxis refAxis, double extendLength)
        {
            Vector3D targetV = new Vector3D();
            switch (refAxis)
            {
                case eRefAxis.X:
                    if (XAxis == null) targetV = new Vector3D();
                    else
                    {
                        if (XLength != 0) targetV = VectorX.GetUnitVector();
                        else targetV = VectorX;
                    }
                    break;

                case eRefAxis.Y:
                    if (YAxis == null) targetV = new Vector3D();
                    else
                    {
                        if (YLength != 0) targetV = VectorY.GetUnitVector();
                        else targetV = VectorY;
                    }
                    break;

                case eRefAxis.Z:
                    if (ZAxis == null) targetV = new Vector3D();
                    else
                    {
                        if (ZLength != 0) targetV = VectorZ.GetUnitVector();
                        else targetV = VectorZ;
                    }
                    break;

                default:

                    break;
            }
            Pose output = new Pose(this);
            output= this + targetV * extendLength;
            return output;
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
        public Pose ToPose(int index)
        {
            Pose output = new Pose
            {
                X = X[index],
                Y = Y[index],
                Z = Z[index],
                XAxis = new double[] { Xx[index], Xy[index], Xz[index] },
                YAxis = new double[] { Yx[index], Yy[index], Yz[index] },
                ZAxis = new double[] { Zx[index], Zy[index], Zz[index] },

            };
            return output;
        }
    }
}









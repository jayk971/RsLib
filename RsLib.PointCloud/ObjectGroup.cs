﻿using Accord.Math;
using Accord.Statistics.Kernels;
using Newtonsoft.Json.Linq;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Threading;
namespace RsLib.PointCloudLib
{
    [Serializable]
    public class ObjectGroup : Object3D
    {
        public Dictionary<string, Object3D> Objects { get; private set; } = new Dictionary<string, Object3D>();
        public List<string> Sequence { get; private set; } = new List<string>();

        public override uint DataCount => (uint)Objects.Count;
        public uint TotalDataCount
        {
            get
            {
                uint sum = 0;
                foreach (KeyValuePair<string, Object3D> kvp in Objects)
                {
                    sum += kvp.Value.DataCount;
                }

                return sum;
            }
        }
        public double TotalPathLength
        {
            get
            {
                double sum = 0.0;
                foreach (KeyValuePair<string, Object3D> kvp in Objects)
                {
                    if(kvp.Value is Polyline pl)
                    {
                        sum += pl.Length;
                    }
                }
                return sum;
            }
        }


        public Point3D Min
        {
            get
            {
                Point3D output = Point3D.MaxValue;
                foreach (var item in Objects)
                {
                    PointCloud line = item.Value as PointCloud;
                    if (line != null)
                    {
                        Point3D lineMin = line.Min;
                        if (output.X >= lineMin.X) output.X = lineMin.X;
                        if (output.Y >= lineMin.Y) output.Y = lineMin.Y;
                        if (output.Z >= lineMin.Z) output.Z = lineMin.Z;
                    }
                }
                return output;
            }
        }
        public Point3D Max
        {
            get
            {
                Point3D output = Point3D.MinValue;
                foreach (var item in Objects)
                {
                    PointCloud line = item.Value as PointCloud;
                    if (line != null)
                    {
                        Point3D lineMax = line.Max;
                        if (output.X <= lineMax.X) output.X = lineMax.X;
                        if (output.Y <= lineMax.Y) output.Y = lineMax.Y;
                        if (output.Z <= lineMax.Z) output.Z = lineMax.Z;
                    }
                }
                return output;
            }
        }
        public ObjectGroup(string groupName)
        {
            Name = groupName;
        }
        public ObjectGroup(string groupName, Tuple<double[], double[], double[], double[], double[], double[], int[]> dataTuple)
        {
            Name = groupName;
            parseArray(dataTuple.Item1,
                dataTuple.Item2,
                dataTuple.Item3,
                dataTuple.Item4,
                dataTuple.Item5,
                dataTuple.Item6,
                dataTuple.Item7);
        }
        //public ObjectGroup(string groupName, SelectionTuple dataTuple,bool calculateVy)
        //{
        //    Name = groupName;
        //    parseArray(dataTuple, calculateVy);
        //}
        public ObjectGroup(string groupName, PoseList dataTuple, bool calculateVy)
        {
            Name = groupName;
            parseArray(dataTuple, calculateVy);
        }
        public ObjectGroup(string groupName, double[] xArray, double[] yArray, double[] zArray, int[] objIndexArray, double[] nXArr, double[] nYArr, double[] nZArr)
        {
            Name = groupName;
            parseArray(xArray, yArray, zArray, nXArr, nYArr, nZArr, objIndexArray);
        }
        void parseArray(double[] xArray, double[] yArray, double[] zArray, double[] nXArr, double[] nYArr, double[] nZArr, int[] objIndexArray)
        {
            int lastIndex = 0;
            Polyline p = new Polyline();
            LineOption lineOption = new LineOption() { LineIndex = lastIndex };
            p.AddOption(lineOption);
            for (int i = 0; i < objIndexArray.Length; i++)
            {
                double x = xArray[i];
                double y = yArray[i];
                double z = zArray[i];
                int index = objIndexArray[i];
                double nx = nXArr[i];
                double ny = nYArr[i];
                double nz = nZArr[i];


                if (lastIndex == index)
                {
                    p.Add(new PointV3D()
                    {
                        X = x,
                        Y = y,
                        Z = z,
                        Vz = new Vector3D(nx, ny, nz)
                    });
                }
                else
                {
                    p.CalculatePathDirectionAsVy();
                    Add($"{Name}_{lastIndex}", p);
                    lastIndex = index;
                    p = new Polyline();
                    lineOption = new LineOption() { LineIndex = lastIndex };
                    p.AddOption(lineOption);
                    p.Add(new PointV3D()
                    {
                        X = x,
                        Y = y,
                        Z = z,
                        Vz = new Vector3D(nx, ny, nz)
                    });
                }
            }
            p.CalculatePathDirectionAsVy();
            Add($"{Name}_{lastIndex}", p);
        }
        //void parseArray(SelectionTuple selectionTuple, bool calculateVy)
        //{
        //    int lastIndex = 0;
        //    Polyline p = new Polyline();
        //    LineOption lineOption = new LineOption() { LineIndex = lastIndex };
        //    p.AddOption(lineOption);
        //    if (calculateVy == false)
        //        if (selectionTuple.Item2.Item1.Count == 0)
        //            calculateVy = true;
        //    for (int i = 0; i < selectionTuple.Item5.Count; i++)
        //    {
        //        double x = selectionTuple.Item1.Item1[i];
        //        double y = selectionTuple.Item1.Item2[i];
        //        double z = selectionTuple.Item1.Item3[i];
        //        int index = selectionTuple.Item5[i];

        //        double zx = selectionTuple.Item4.Item1[i];
        //        double zy = selectionTuple.Item4.Item2[i];
        //        double zz = selectionTuple.Item4.Item3[i];


        //        if (lastIndex == index)
        //        {
        //            PointV3D pt = new PointV3D()
        //            {
        //                X = x,
        //                Y = y,
        //                Z = z,
        //                Vz = new Vector3D(zx, zy, zz)
        //            };

        //            if(calculateVy == false)
        //            {
        //                double xx = selectionTuple.Item2.Item1[i];
        //                double xy = selectionTuple.Item2.Item2[i];
        //                double xz = selectionTuple.Item2.Item3[i];

        //                double yx = selectionTuple.Item3.Item1[i];
        //                double yy = selectionTuple.Item3.Item2[i];
        //                double yz = selectionTuple.Item3.Item3[i];
        //                pt.Vx = new Vector3D(xx, xy, xz);
        //                pt.Vy = new Vector3D(yx, yy, yz);

        //            }
        //            p.Add(pt);
        //        }
        //        else
        //        {
        //            if(calculateVy) p.CalculatePathDirectionAsVy();

        //            Add($"{Name}_{lastIndex}", p);
        //            lastIndex = index;
        //            p = new Polyline();
        //            lineOption = new LineOption() { LineIndex = lastIndex };
        //            p.AddOption(lineOption);
        //            PointV3D pt = new PointV3D()
        //            {
        //                X = x,
        //                Y = y,
        //                Z = z,
        //                Vz = new Vector3D(zx, zy, zz)
        //            };

        //            if (calculateVy == false)
        //            {
        //                double xx = selectionTuple.Item2.Item1[i];
        //                double xy = selectionTuple.Item2.Item2[i];
        //                double xz = selectionTuple.Item2.Item3[i];

        //                double yx = selectionTuple.Item3.Item1[i];
        //                double yy = selectionTuple.Item3.Item2[i];
        //                double yz = selectionTuple.Item3.Item3[i];
        //                pt.Vx = new Vector3D(xx, xy, xz);
        //                pt.Vy = new Vector3D(yx, yy, yz);

        //            }
        //            p.Add(pt);
        //        }
        //    }
        //    if (calculateVy) p.CalculatePathDirectionAsVy();
        //    Add($"{Name}_{lastIndex}", p);
        //}
        void parseArray(PoseList selectionTuple, bool calculateVy)
        {
            int lastIndex = 0;
            Polyline p = new Polyline();
            LineOption lineOption = new LineOption() { LineIndex = lastIndex };
            p.AddOption(lineOption);
            if (calculateVy == false)
                if (selectionTuple.Xx.Count == 0)
                    calculateVy = true;
            for (int i = 0; i < selectionTuple.Index.Count; i++)
            {
                double x = selectionTuple.X[i];
                double y = selectionTuple.Y[i];
                double z = selectionTuple.Z[i];
                int index = selectionTuple.Index[i];

                double zx = selectionTuple.Zx[i];
                double zy = selectionTuple.Zy[i];
                double zz = selectionTuple.Zz[i];


                if (lastIndex == index)
                {
                    PointV3D pt = new PointV3D()
                    {
                        X = x,
                        Y = y,
                        Z = z,
                        Vz = new Vector3D(zx, zy, zz)
                    };

                    if (calculateVy == false)
                    {
                        pt.Vx = selectionTuple.GetVectorX(i);
                        pt.Vy = selectionTuple.GetVectorY(i);

                    }
                    p.Add(pt);
                }
                else
                {
                    if (calculateVy) p.CalculatePathDirectionAsVy();

                    Add($"{Name}_{lastIndex}", p);
                    lastIndex = index;
                    p = new Polyline();
                    lineOption = new LineOption() { LineIndex = lastIndex };
                    p.AddOption(lineOption);
                    PointV3D pt = new PointV3D()
                    {
                        X = x,
                        Y = y,
                        Z = z,
                        Vz = new Vector3D(zx, zy, zz)
                    };

                    if (calculateVy == false)
                    {
                        pt.Vx = selectionTuple.GetVectorX(i);
                        pt.Vy = selectionTuple.GetVectorY(i);
                    }
                    p.Add(pt);
                }
            }
            if (calculateVy) p.CalculatePathDirectionAsVy();
            Add($"{Name}_{lastIndex}", p);
        }

        public void Add(string objName, Object3D object3D)
        {
            if (!Objects.ContainsKey(objName))
            {
                Sequence.Add(objName);
                Objects.Add(objName, object3D);
            }
            else
                throw new Exception($"object name : {objName} is already in the group.");
        }
        public void AddRange(List<Object3D> object3D)
        {
            int count = Sequence.Count;
            for (int i = 0; i < object3D.Count; i++)
            {
                string name = $"{Name}_{count + i}";
                Sequence.Add(name);
                Objects.Add(name, object3D[i]);
            }
        }

        public void Remove(string objName)
        {
            if (Objects.ContainsKey(objName))
            {
                Sequence.Remove(objName);
                Objects.Remove(objName);
            }
            else
                throw new Exception($"object name : {objName} is not in the group.");
        }

        public ObjectGroup Multiply(Matrix4x4 matrix)
        {
            ObjectGroup outputOg = new ObjectGroup("TransformOG");
            foreach (var item in Objects)
            {
                if (item.Value is Polyline pl)
                {
                    Polyline plTransform = pl.Multiply(matrix) as Polyline;
                    outputOg.Add($"Trans{item.Key}", plTransform);
                }
                else if (item.Value is PointCloud pc)
                {
                    PointCloud pcTransform = pc.Multiply(matrix);
                    outputOg.Add($"Trans{item.Key}", pcTransform);
                }
            }
            return outputOg;
        }
        public Tuple<double[], double[], double[], int[]> ToXYZIArrayTuple()
        {
            List<double> xList = new List<double>();
            List<double> yList = new List<double>();
            List<double> zLIst = new List<double>();
            List<int> iLIst = new List<int>();

            foreach (var item in Objects)
            {
                string name = item.Key;
                Object3D obj = item.Value;
                Type objType = obj.GetType();
                if (objType == typeof(Polyline))
                {
                    Polyline pLine = obj as Polyline;
                    LineOption lineOption = pLine.GetOption(typeof(LineOption)) as LineOption;

                    if (pLine != null)
                    {
                        for (int i = 0; i < pLine.Count; i++)
                        {
                            Point3D pt = pLine.Points[i];
                            xList.Add(pt.X);
                            yList.Add(pt.Y);
                            zLIst.Add(pt.Z);
                            if (lineOption != null)
                            {
                                iLIst.Add(lineOption.LineIndex);
                            }
                            else iLIst.Add(0);
                        }
                    }
                }
                else if (objType == typeof(PointCloud))
                {
                    PointCloud pCloud = obj as PointCloud;
                    if (pCloud != null)
                    {
                        for (int i = 0; i < pCloud.Count; i++)
                        {
                            Point3D pt = pCloud.Points[i];
                            xList.Add(pt.X);
                            yList.Add(pt.Y);
                            zLIst.Add(pt.Z);
                            iLIst.Add(0);
                        }
                    }
                }
                else
                {

                }
            }

            return new Tuple<double[], double[], double[], int[]>(xList.ToArray(), yList.ToArray(), zLIst.ToArray(), iLIst.ToArray());
        }

        public void Clear()
        {
            Objects.Clear();
            Sequence.Clear();
        }
        public void SaveXYZ(string filePath, bool useOtherThread = false)
        {
            if(useOtherThread)
            {
                ThreadPool.QueueUserWorkItem(new WaitCallback(saveXYZ), filePath);
            }
            else
            {
                saveXYZ(filePath);
            }
        }
        private void saveXYZ(object obj)
        {
            string filePath = (string)obj;
            PointCloud total = new PointCloud();
            foreach (var item in Objects)
            {
                var subObj = item.Value as PointCloud;
                if (subObj != null)
                    total.Add(subObj);
            }
            if (total.DataCount > 0)
                total.Save(filePath);
            else
            {
                throw new Exception("Output cloud data count = 0");
            }
        }
        public void SaveOPT(string filePath, bool useOtherThread = false)
        {
            if(useOtherThread)
            {
                ThreadPool.QueueUserWorkItem(new WaitCallback(saveOPT), filePath);
            }
            else
            {
                saveOPT(filePath);
            }
        }
        private void saveOPT(object obj)
        {
            string filePath = (string)obj;
            List<string> finalString = new List<string>();
            foreach (var item in Objects)
            {
                var subObj = item.Value as Polyline;
                if (subObj != null)
                {
                    finalString.AddRange(subObj.GetOptPathStringList());
                }
            }
            using (StreamWriter sw = new StreamWriter(filePath, false, System.Text.Encoding.Default, 65535))
            {
                for (int i = 0; i < finalString.Count; i++)
                {
                    sw.WriteLine(finalString[i]);
                }
            }
        }
        public void SaveOPT2(string filePath,bool useOtherThread = false)
        {
            if(useOtherThread)
            {
                ThreadPool.QueueUserWorkItem(new WaitCallback(saveOPT2), filePath);
            }
            else
            {
                saveOPT2(filePath);
            }
        }
        private void saveOPT2(object obj)
        {
            string filePath = (string)obj;
            List<string> finalString = new List<string>();
            foreach (var item in Objects)
            {
                var subObj = item.Value as Polyline;
                if (subObj != null)
                {
                    finalString.AddRange(subObj.GetOpt2PathStringList());
                }
            }
            using (StreamWriter sw = new StreamWriter(filePath, false, System.Text.Encoding.Default, 65535))
            {
                for (int i = 0; i < finalString.Count; i++)
                {
                    sw.WriteLine(finalString[i]);
                }
            }
        }

        public void LoadMultiPathOPT(string filePath, bool buildKDTree)
        {
            List<string> temp = new List<string>();
            using (StreamReader sr = new StreamReader(filePath))
            {
                while (!sr.EndOfStream)
                {
                    string readData = sr.ReadLine();
                    if (readData != "")
                    {
                        temp.Add(readData);
                    }
                    else
                    {
                        if (temp.Count > 0)
                        {
                            Polyline p = new Polyline();
                            LineOption lineProperty = new LineOption()
                            {
                                LineIndex = Objects.Count,
                            };

                            p.Options.Add(lineProperty);
                            p.LoadFromStringList(temp, buildKDTree);
                            p.CalculatePathDirectionAsVy();

                            Objects.Add(lineProperty.LineIndex.ToString(), p);
                            Sequence.Add(lineProperty.LineIndex.ToString());
                            temp.Clear();
                        }
                    }
                }
            }
            if (temp.Count > 0)
            {
                Polyline p = new Polyline();
                LineOption lineProperty = new LineOption()
                {
                    LineIndex = Objects.Count
                };
                p.Options.Add(lineProperty);
                p.LoadFromStringList(temp, buildKDTree);
                p.CalculatePathDirectionAsVy();

                Objects.Add(lineProperty.LineIndex.ToString(), p);
                Sequence.Add(lineProperty.LineIndex.ToString());
                temp.Clear();
            }
        }
        public void LoadMultiPathOPT2(string filePath, bool buildKDTree)
        {
            List<string> temp = new List<string>();
            using (StreamReader sr = new StreamReader(filePath))
            {
                while (!sr.EndOfStream)
                {
                    string readData = sr.ReadLine();
                    if (readData != "")
                    {
                        temp.Add(readData);
                    }
                    else
                    {
                        if (temp.Count > 0)
                        {
                            Polyline p = new Polyline();
                            LineOption lineProperty = new LineOption()
                            {
                                LineIndex = Objects.Count,
                            };

                            p.Options.Add(lineProperty);
                            p.LoadFromStringList(temp, buildKDTree);

                            Objects.Add(lineProperty.LineIndex.ToString(), p);
                            Sequence.Add(lineProperty.LineIndex.ToString());
                            temp.Clear();
                        }
                    }
                }
            }
            if (temp.Count > 0)
            {
                Polyline p = new Polyline();
                LineOption lineProperty = new LineOption()
                {
                    LineIndex = Objects.Count
                };
                p.Options.Add(lineProperty);
                p.LoadFromStringList(temp, buildKDTree);

                Objects.Add(lineProperty.LineIndex.ToString(), p);
                Sequence.Add(lineProperty.LineIndex.ToString());
                temp.Clear();
            }
        }

        public void SaveABBModPath(string filePath, bool isRobTargetMode)
        {
            ABBPath aBBPath = ConvertABBModPath();
            aBBPath.SmoothEulerAngle_3P(true, true, true, 1.0, 1.0, 1.0);
            if (isRobTargetMode)
                aBBPath.SaveABBModPathWithRobTarget(filePath);
            else
                aBBPath.SaveABBModPath(filePath);
        }
        public ABBPath ConvertABBModPath()
        {
            int segmentIndex = 0;
            ABBPath aBBPath = new ABBPath();
            foreach (var item in Objects)
            {
                string name = item.Key;
                string segmentName = "";
                Object3D obj = item.Value;
                if (obj is Polyline pLine)
                {
                    if (pLine.GetOption(typeof(LineOption)) is LineOption lo) segmentIndex = lo.LineIndex;
                    if (pLine.GetOption(typeof(NameOption)) is NameOption no) segmentName = no.Name;

                    for (int i = 0; i < pLine.Count; i++)
                    {
                        if(pLine.Points[i] is PointV3D pV3D)
                        {

                            ABBPoint abbPt = new ABBPoint(pV3D)
                            {
                                PtIndex = i,
                                LapIndex = segmentIndex,
                                SegmentIndex = segmentIndex,
                            };
                            aBBPath.Add(abbPt, segmentName);
                        }
                    }
                    segmentIndex++;
                }

            }
            return aBBPath;
        }
        public Polyline SelectPolyine(int selectLineIndex)
        {
            if (selectLineIndex < 0) return null;
            foreach (var item in Objects)
            {
                string name = item.Key;
                Object3D obj = item.Value;
                Polyline pLine = obj as Polyline;
                if (pLine != null)
                {
                    LineOption lineOption = pLine.GetOption(typeof(LineOption)) as LineOption;
                    if (lineOption.LineIndex == selectLineIndex) return pLine;
                }
            }
            return null;
        }
    }
}

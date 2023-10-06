using Accord.Statistics.Kernels;
using Newtonsoft.Json.Linq;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
namespace RsLib.PointCloudLib
{
    [Serializable]
    public abstract class ObjectOption
    {

    }
    [Serializable]
    public abstract class Object3D : IEnumerable<ObjectOption>
    {
        public string Name = "";

        List<ObjectOption> _Options = new List<ObjectOption>();
        public List<ObjectOption> Options => _Options;
        public abstract uint DataCount { get; }

        public IEnumerator<ObjectOption> GetEnumerator()
        {
            foreach (var item in _Options)
            {
                yield return item;
            }
        }
        public void AddOption(ObjectOption option)
        {
            bool addNew = true;
            for (int i = 0; i < _Options.Count; i++)
            {
                Type t = option.GetType();
                if (t == _Options[i].GetType())
                {
                    _Options[i] = option;
                    addNew = false;
                }
            }
            if (addNew) _Options.Add(option);
        }
        public void AddOption(List<ObjectOption> options)
        {
            bool addNew = true;
            for (int h = 0; h < options.Count; h++)
            {
                ObjectOption option = options[h];
                for (int i = 0; i < _Options.Count; i++)
                {
                    Type t = option.GetType();
                    if (t == _Options[i].GetType())
                    {
                        _Options[i] = option;
                        addNew = false;
                    }
                }
                if (addNew) _Options.Add(option);
            }
        }


        public object GetOption(Type optionType)
        {
            object o = null;
            for (int i = 0; i < _Options.Count; i++)
            {
                Type t = _Options[i].GetType();
                if (t == optionType)
                {
                    o = _Options[i];
                    break;
                }
            }

            return o;
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            foreach (var item in _Options)
            {
                yield return item;
            }
        }
    }
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
        public ObjectGroup(string groupName, Tuple<double[] , double[] , double[], double[] , double[] , double[], int[]> dataTuple)
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

        public ObjectGroup(string groupName,double[] xArray,double[] yArray,double[] zArray,int[] objIndexArray, double[] nXArr, double[] nYArr, double[] nZArr)
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
        public Tuple<double[],double[],double[],int[]> ToXYZIArrayTuple()
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
                if(objType == typeof(Polyline))
                {
                    Polyline pLine = obj as Polyline;
                    LineOption lineOption =  pLine.GetOption(typeof(LineOption)) as LineOption;
   
                    if(pLine!= null)
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
                else if(objType == typeof(PointCloud))
                {
                    PointCloud pCloud = obj as PointCloud;
                    if(pCloud != null)
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
        public void SaveXYZ(string filePath)
        {
            PointCloud total = new PointCloud();
            foreach (var item in Objects)
            {
                var subObj = item.Value as PointCloud;
                if(subObj != null)
                    total.Add(subObj);
            }
            if(total.DataCount >0)
                total.Save(filePath);
            else
            {
                throw new Exception("Output cloud data count = 0");
            }
        }
        public void SaveOPT(string filePath)
        {
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
        public void SaveOPT2(string filePath)
        {
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

        public void SaveABBModPath(string filePath,bool isRobTargetMode)
        {
            ABBPath aBBPath = ConvertABBModPath();
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
                Object3D obj = item.Value;
                Polyline pLine = obj as Polyline;
                if (pLine != null)
                {
                    for (int i = 0; i < pLine.Count; i++)
                    {
                        PointV3D pV3D = pLine.Points[i] as PointV3D;
                        if(pV3D != null)
                        {
                            ABBPathPoint abbPt = new ABBPathPoint(pV3D)
                            {
                                PtIndex = i,
                                LapIndex = segmentIndex,
                                SegmentIndex = segmentIndex,
                            };
                            aBBPath.Add(abbPt);
                        }
                    }
                    segmentIndex++;
                }

            }
            return aBBPath;
        }
        public Polyline SelectPolyine(int selectLineIndex)
        {
            foreach (var item in Objects)
            {
                string name = item.Key;
                Object3D obj = item.Value;
                Polyline pLine = obj as Polyline;
                if(pLine != null)
                {
                    LineOption lineOption = pLine.GetOption(typeof(LineOption)) as LineOption;
                    if (lineOption.LineIndex == selectLineIndex) return pLine;
                }
            }
            return null;
        }
    }

}

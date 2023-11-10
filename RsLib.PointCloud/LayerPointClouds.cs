using Accord.Collections;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
namespace RsLib.PointCloudLib
{
    [Serializable]
    public partial class LayerPointCloud : Object3D
    {
        public List<PointCloud> Layers = new List<PointCloud>();
        public int LayerCount { get { return Layers.Count; } }
        public int PointCount { get { return TotalPointCount(); } }
        public Point3D Max { get { return GetMaxPoint(); } }
        public Point3D Min { get { return GetMinPoint(); } }

        public override uint DataCount => (uint)LayerCount;

        public LayerPointCloud()
        {
            Layers = new List<PointCloud>();
        }

        public LayerPointCloud(int LayerCount)
        {
            for (int i = 0; i < LayerCount; i++)
            {
                Layers.Add(new PointCloud());
            }
        }
        public LayerPointCloud(LayerPointCloud OtherClouds)
        {
            Layers = new List<PointCloud>();
            Layers = OtherClouds.Layers;
        }
        public LayerPointCloud(PointCloud Input, bool IsSplitX)
        {
            Dictionary<double, PointCloud> BasePosition = new Dictionary<double, PointCloud>();

            for (int i = 0; i < Input.Count; i++)
            {
                double RefValue = 0;
                if (IsSplitX) RefValue = Math.Round(Input.Points[i].X, 2);
                else RefValue = Math.Round(Input.Points[i].Y, 2);

                if (BasePosition.ContainsKey(RefValue))
                {
                    BasePosition[RefValue].Add(Input.Points[i]);
                }
                else
                {
                    PointCloud NewCloud = new PointCloud();
                    NewCloud.Add(Input.Points[i]);

                    BasePosition.Add(RefValue, NewCloud);
                }
            }
            foreach (KeyValuePair<double, PointCloud> kvp in BasePosition)
            {
                if (IsSplitX) BasePosition[kvp.Key].Points = kvp.Value.Points.OrderBy(t => t.Y).ToList();
                else BasePosition[kvp.Key].Points = kvp.Value.Points.OrderBy(t => t.X).ToList();
            }
            Dictionary<double, PointCloud> SortPosition = new Dictionary<double, PointCloud>();
            SortPosition = BasePosition.OrderBy(t => t.Key).ToDictionary(t => t.Key, t => t.Value);

            Layers = SortPosition.Values.ToList();
        }

        public LayerPointCloud(PointCloud Input, bool IsSplitX, double Dis)
        {
            double Max, Min = 0.0;
            int SplitCount = 0;
            double UseDis = Math.Abs(Dis);
            double refDis = UseDis < 0.5 ? UseDis * 0.5 : 0.5;
            if (IsSplitX)
            {
                Max = Input.Max.X;
                Min = Input.Min.X;
            }
            else
            {
                Max = Input.Max.Y;
                Min = Input.Min.Y;
            }
            SplitCount = (int)Math.Ceiling((Max - Min) / UseDis);
            Dictionary<double, PointCloud> BasePosition = new Dictionary<double, PointCloud>();
            List<double> BaseIndex = new List<double>();
            for (int i = 0; i < SplitCount; i++)
            {
                double BaseValue = Math.Round(Min + i * UseDis, 1);
                BaseIndex.Add(BaseValue);
                BasePosition.Add(BaseValue, new PointCloud());
            }
            for (int i = 0; i < Input.Count; i++)
            {
                double RefValue = 0;
                if (IsSplitX) RefValue = Input.Points[i].X;
                else RefValue = Input.Points[i].Y;

                for (int j = 0; j < BaseIndex.Count; j++)
                {
                    double BaseValue = BaseIndex[j];
                    double Diff = Math.Abs(BaseValue - RefValue);
                    if (Diff <= refDis)
                    {
                        if (IsSplitX)
                        {
                            Input.Points[i].X = BaseValue;
                        }
                        else
                        {
                            Input.Points[i].Y = BaseValue;
                        }
                        BasePosition[BaseValue].Add(Input.Points[i]);
                    }

                }
            }
            foreach (KeyValuePair<double, PointCloud> kvp in BasePosition)
            {
                if (IsSplitX) BasePosition[kvp.Key].Points = kvp.Value.Points.OrderBy(t => t.Y).ToList();
                else BasePosition[kvp.Key].Points = kvp.Value.Points.OrderBy(t => t.X).ToList();
            }
            Dictionary<double, PointCloud> SortPosition = new Dictionary<double, PointCloud>();
            SortPosition = BasePosition.OrderBy(t => t.Key).ToDictionary(t => t.Key, t => t.Value);

            Layers = SortPosition.Values.ToList();
        }
        /// <summary>
        /// 切分點雲
        /// </summary>
        /// <param name="Input">原始點雲</param>
        /// <param name="SplitMethod">X = 1, Y = 2, Z = 3</param>
        /// <param name="Dis">切分的大小</param>
        public LayerPointCloud(PointCloud Input, int SplitMethod, double Dis, bool IsMax2Min)
        {
            double Max, Min = 0.0;
            int SplitCount = 0;
            double UseDis = Math.Abs(Dis);
            if (SplitMethod == 1)
            {
                Max = Input.Max.X;
                Min = Input.Min.X;
            }
            else if (SplitMethod == 2)
            {
                Max = Input.Max.Y;
                Min = Input.Min.Y;
            }
            else
            {
                Max = Input.Max.Z;
                Min = Input.Min.Z;
            }
            SplitCount = (int)Math.Ceiling((Max - Min) / UseDis);
            Dictionary<double, PointCloud> BasePosition = new Dictionary<double, PointCloud>();
            List<double> BaseIndex = new List<double>();
            for (int i = 0; i < SplitCount; i++)
            {
                double BaseValue = Math.Round(Min + i * UseDis, 1);
                BaseIndex.Add(BaseValue);
                BasePosition.Add(BaseValue, new PointCloud());
            }
            for (int i = 0; i < Input.Count; i++)
            {
                double RefValue = 0;
                if (SplitMethod == 1) RefValue = Input.Points[i].X;
                else if (SplitMethod == 2) RefValue = Input.Points[i].Y;
                else RefValue = Input.Points[i].Z;

                for (int j = 0; j < BaseIndex.Count; j++)
                {
                    double BaseValue = BaseIndex[j];
                    double Diff = Math.Abs(BaseValue - RefValue);
                    if (Diff <= UseDis)
                    {

                        BasePosition[BaseValue].Add(Input.Points[i]);
                    }

                }
            }
            Dictionary<double, PointCloud> SortPosition = new Dictionary<double, PointCloud>();

            if (IsMax2Min) SortPosition = BasePosition.OrderByDescending(t => t.Key).ToDictionary(t => t.Key, t => t.Value);
            else SortPosition = BasePosition.OrderBy(t => t.Key).ToDictionary(t => t.Key, t => t.Value);

            Layers = SortPosition.Values.ToList();
        }

        public LayerPointCloud(string FilePath, int RowDigitCount, bool IsSplitX, DigitFormat Format, char SplitSymbol)
        {
            if (!File.Exists(FilePath)) return;
            //List<string> stringList = ReadAllLines(FilePath);
            Dictionary<double, PointCloud> tempSegment = new Dictionary<double, PointCloud>();

            using (StreamReader sr = new StreamReader(FilePath, Encoding.Default))
            {
                while (!sr.EndOfStream)
                {
                    string s = sr.ReadLine();
                    if (s != "")
                    {
                        string[] SplitData = s.Split(SplitSymbol);
                        if (SplitData.Length == RowDigitCount)
                        {
                            double x = 0;
                            double y = 0;
                            double z = 0;

                            ConvertXYZ(Format, SplitData[0], SplitData[1], SplitData[2], out x, out y, out z);

                            Point3D point = new Point3D(x, y, z);

                            double RefValue = 0;
                            if (IsSplitX) RefValue = x;
                            else RefValue = y;

                            if (tempSegment.ContainsKey(RefValue))
                            {
                                tempSegment[RefValue].Add(point);
                            }
                            else
                            {
                                PointCloud temp = new PointCloud();
                                temp.Add(point);
                                tempSegment.Add(RefValue, temp);
                            }

                        }
                    }
                }
                sr.Close();
            }
            Dictionary<double, PointCloud> Sort = tempSegment.OrderBy(t => t.Key).ToDictionary(d => d.Key, s => s.Value);
            Layers = Sort.Values.ToList();
        }
        public LayerPointCloud(string FilePath, int RowDigitCount, bool IsSplitX, DigitFormat Format, char SplitSymbol, int ResampleCount)
        {
            if (!File.Exists(FilePath)) return;
            Dictionary<double, PointCloud> tempSegment = new Dictionary<double, PointCloud>();

            double minX = double.MaxValue;
            double maxX = double.MinValue;
            double minY = double.MaxValue;
            double maxY = double.MinValue;
            double minZ = double.MaxValue;
            double maxZ = double.MinValue;

            using (StreamReader sr = new StreamReader(FilePath))
            {
                if (ResampleCount <= 0) ResampleCount = 1;
                while (!sr.EndOfStream)
                {
                    string s = sr.ReadLine();
                    if (s != "")
                    {
                        string[] SplitData = s.Split(SplitSymbol);
                        if (SplitData.Length == RowDigitCount)
                        {
                            double x = 0;
                            double y = 0;
                            double z = 0;

                            ConvertXYZ(Format, SplitData[0], SplitData[1], SplitData[2], out x, out y, out z);

                            Point3D point = new Point3D(x, y, z);

                            if (maxX <= x) maxX = x;
                            if (minX >= x) minX = x;

                            if (maxY <= y) maxY = y;
                            if (minY >= y) minY = y;

                            if (maxZ <= z) maxZ = z;
                            if (minZ >= z) minZ = z;


                            double RefValue = 0;
                            if (IsSplitX) RefValue = x;
                            else RefValue = y;

                            if (tempSegment.ContainsKey(RefValue))
                            {
                                tempSegment[RefValue].Add(point);
                            }
                            else
                            {
                                PointCloud temp = new PointCloud();
                                temp.Add(point);
                                tempSegment.Add(RefValue, temp);
                            }

                        }
                    }
                }
                sr.Close();
            }

            Dictionary<double, PointCloud> Sort = tempSegment.OrderBy(t => t.Key).ToDictionary(d => d.Key, s => s.Value);
            List<double> Keys = Sort.Keys.ToList();
            for (int i = 0; i < Keys.Count; i++)
            {
                if (IsSplitX) Sort[Keys[i]].Points = Sort[Keys[i]].Points.OrderByDescending(t => t.Y).ToList();
                else Sort[Keys[i]].Points = Sort[Keys[i]].Points.OrderByDescending(t => t.X).ToList();
                if (i % ResampleCount != 0) continue;
                Layers.Add(Sort[Keys[i]]);
            }
        }
        //public LayerPointCloud(string FilePath, int RowDigitCount, bool IsSplitX, DigitFormat Format, char SplitSymbol, int ResampleCount)
        //{
        //    if (!File.Exists(FilePath)) return;
        //    List<string> stringList = ReadAllLines(FilePath);

        //    if (stringList.Count == 0) return;
        //    if (ResampleCount <= 0) ResampleCount = 1;
        //    Dictionary<double, PointCloud> tempSegment = new Dictionary<double, PointCloud>();
        //    for (int i = 0; i < stringList.Count; i++)
        //    {
        //        if (stringList[i] != "")
        //        {
        //            string[] SplitData = stringList[i].Split(SplitSymbol);
        //            if (SplitData.Length == RowDigitCount)
        //            {
        //                double x = 0;
        //                double y = 0;
        //                double z = 0;

        //                ConvertXYZ(Format, SplitData[0], SplitData[1], SplitData[2], out x, out y, out z);

        //                Point3D point = new Point3D(x, y, z);

        //                double RefValue = 0;
        //                if (IsSplitX) RefValue = x;
        //                else RefValue = y;

        //                if (tempSegment.ContainsKey(RefValue))
        //                {
        //                    tempSegment[RefValue].Add(point);
        //                }
        //                else
        //                {
        //                    PointCloud temp = new PointCloud();
        //                    temp.Add(point);
        //                    tempSegment.Add(RefValue, temp);
        //                }

        //            }
        //        }
        //    }

        //    Dictionary<double, PointCloud> Sort = tempSegment.OrderBy(t => t.Key).ToDictionary(d => d.Key, s => s.Value);
        //    List<double> Keys = Sort.Keys.ToList();
        //    for(int i = 0; i < Keys.Count; i++)
        //    {
        //        if (i % ResampleCount != 0) continue;
        //        Layers.Add(Sort[Keys[i]]);
        //    }
        //}
        public void Clear()
        {
            Layers.Clear();
        }
        public void Add(PointCloud Layer)
        {
            Layers.Add(Layer);
        }
        public void Add(LayerPointCloud Layer)
        {
            Layers.AddRange(Layer.Layers);
        }
        public PointCloud ToPointCloud()
        {
            PointCloud Output = new PointCloud();
            for (int i = 0; i < Layers.Count; i++)
            {
                Output.Add(Layers[i]);
            }
            return Output;
        }
        public PointCloud ToPointCloud(System.Drawing.Color DisplayColor)
        {
            PointCloud Output = new PointCloud();
            for (int i = 0; i < Layers.Count; i++)
            {
                Output.Add(Layers[i]);
            }
            return Output;
        }
        public PointCloud ToPointCloud(int ReduceInt)
        {
            PointCloud Output = new PointCloud();
            for (int i = 0; i < Layers.Count; i++)
            {
                if (i % ReduceInt == 0) Output.Add(Layers[i]);
            }
            return Output;
        }
        public KDTree<int> BuildIndexKDtree()
        {
            KDTree<int> kD = new KDTree<int>(3);

            int Count = 0;
            if (Layers.Count > 0)
            {
                for (int i = 0; i < Layers.Count; i++)
                {
                    for (int j = 0; j < Layers[i].Count; j++)
                    {
                        kD.Add(new double[] { Layers[i].Points[j].X, Layers[i].Points[j].Y, Layers[i].Points[j].Z }, Count);
                        Count++;
                    }
                }
            }
            return kD;
        }

        public void ReduceAboveX(double LimitValue)
        {
            List<PointCloud> Output = new List<PointCloud>();
            for (int i = 0; i < Layers.Count; i++)
            {
                if (Layers[i].Count > 0)
                    if (Layers[i].Points[0].X <= LimitValue) Output.Add(Layers[i]);
            }
            Layers.Clear();
            Layers = Output;
        }
        public void ReduceAboveY(double LimitValue)
        {
            List<PointCloud> Output = new List<PointCloud>();
            for (int i = 0; i < Layers.Count; i++)
            {
                if (Layers[i].Count > 0)
                    if (Layers[i].Points[0].Y <= LimitValue) Output.Add(Layers[i]);
            }
            Layers.Clear();
            Layers = Output;
        }
        public void ReduceAboveZ(double LimitValue)
        {
            List<PointCloud> Output = new List<PointCloud>();
            for (int i = 0; i < Layers.Count; i++)
            {
                if (Layers[i].Count > 0)
                    if (Layers[i].Points[0].Z <= LimitValue) Output.Add(Layers[i]);
            }
            Layers.Clear();
            Layers = Output;
        }
        public void ReduceBelowX(double LimitValue)
        {
            List<PointCloud> Output = new List<PointCloud>();
            for (int i = 0; i < Layers.Count; i++)
            {
                if (Layers[i].Count > 0)
                    if (Layers[i].Points[0].X >= LimitValue) Output.Add(Layers[i]);
            }
            Layers.Clear();
            Layers = Output;
        }
        public void ReduceBelowY(double LimitValue)
        {
            List<PointCloud> Output = new List<PointCloud>();
            for (int i = 0; i < Layers.Count; i++)
            {
                if (Layers[i].Count > 0)
                    if (Layers[i].Points[0].Y >= LimitValue) Output.Add(Layers[i]);
            }
            Layers.Clear();
            Layers = Output;
        }
        public void ReduceBelowZ(double LimitValue)
        {
            List<PointCloud> Output = new List<PointCloud>();
            for (int i = 0; i < Layers.Count; i++)
            {
                if (Layers[i].Count > 0)
                    if (Layers[i].Points[0].Z >= LimitValue) Output.Add(Layers[i]);
            }
            Layers.Clear();
            Layers = Output;
        }

        public LayerPointCloud GetAboveX(double LimitValue)
        {
            LayerPointCloud Output = new LayerPointCloud();
            for (int i = 0; i < Layers.Count; i++)
            {
                if (Layers[i].Count > 0)
                    if (Layers[i].Points[0].X >= LimitValue) Output.Layers.Add(Layers[i]);
            }
            return Output;
        }
        public PointCloud GetAboveXtoPointCloud(double LimitValue)
        {
            PointCloud Output = new PointCloud();
            for (int i = 0; i < Layers.Count; i++)
            {
                if (Layers[i].Count > 0)
                    if (Layers[i].Points[0].X >= LimitValue) Output.Add(Layers[i]);
            }
            return Output;
        }
        public LayerPointCloud GetAboveY(double LimitValue)
        {
            LayerPointCloud Output = new LayerPointCloud();
            for (int i = 0; i < Layers.Count; i++)
            {
                if (Layers[i].Count > 0)
                    if (Layers[i].Points[0].Y >= LimitValue) Output.Layers.Add(Layers[i]);
            }
            return Output;
        }
        public PointCloud GetAboveYtoPointCloud(double LimitValue)
        {
            PointCloud Output = new PointCloud();
            for (int i = 0; i < Layers.Count; i++)
            {
                if (Layers[i].Count > 0)
                    if (Layers[i].Points[0].Y >= LimitValue) Output.Add(Layers[i]);
            }
            return Output;
        }
        public LayerPointCloud GetAboveZ(double LimitValue)
        {
            LayerPointCloud Output = new LayerPointCloud();
            for (int i = 0; i < Layers.Count; i++)
            {
                if (Layers[i].Count > 0)
                    if (Layers[i].Points[0].Z >= LimitValue) Output.Layers.Add(Layers[i]);
            }
            return Output;
        }
        public PointCloud GetAboveZtoPointCloud(double LimitValue)
        {
            PointCloud Output = new PointCloud();
            for (int i = 0; i < Layers.Count; i++)
            {
                if (Layers[i].Count > 0)
                    if (Layers[i].Points[0].Z >= LimitValue) Output.Add(Layers[i]);
            }
            return Output;
        }
        public LayerPointCloud GetBelowX(double LimitValue)
        {
            LayerPointCloud Output = new LayerPointCloud();
            for (int i = 0; i < Layers.Count; i++)
            {
                if (Layers[i].Count > 0)
                    if (Layers[i].Points[0].X <= LimitValue) Output.Layers.Add(Layers[i]);
            }
            return Output;
        }
        public PointCloud GetBelowXtoPointCloud(double LimitValue)
        {
            PointCloud Output = new PointCloud();
            for (int i = 0; i < Layers.Count; i++)
            {
                if (Layers[i].Count > 0)
                    if (Layers[i].Points[0].X <= LimitValue) Output.Add(Layers[i]);
            }
            return Output;
        }
        public LayerPointCloud GetBelowY(double LimitValue)
        {
            LayerPointCloud Output = new LayerPointCloud();
            for (int i = 0; i < Layers.Count; i++)
            {
                if (Layers[i].Count > 0)
                    if (Layers[i].Points[0].Y <= LimitValue) Output.Layers.Add(Layers[i]);
            }
            return Output;
        }
        public PointCloud GetBelowYtoPointCloud(double LimitValue)
        {
            PointCloud Output = new PointCloud();
            for (int i = 0; i < Layers.Count; i++)
            {
                if (Layers[i].Count > 0)
                    if (Layers[i].Points[0].Y <= LimitValue) Output.Add(Layers[i]);
            }
            return Output;
        }
        public LayerPointCloud GetBelowZ(double LimitValue)
        {
            LayerPointCloud Output = new LayerPointCloud();
            for (int i = 0; i < Layers.Count; i++)
            {
                if (Layers[i].Count > 0)
                    if (Layers[i].Points[0].Z <= LimitValue) Output.Layers.Add(Layers[i]);
            }
            return Output;
        }
        public PointCloud GetBelowZtoPointCloud(double LimitValue)
        {
            PointCloud Output = new PointCloud();
            for (int i = 0; i < Layers.Count; i++)
            {
                if (Layers[i].Count > 0)
                    if (Layers[i].Points[0].Z <= LimitValue) Output.Add(Layers[i]);
            }
            return Output;
        }

        public void ReduceInsideRangeX(double MinValue, double MaxValue)
        {
            List<PointCloud> Output = new List<PointCloud>();
            for (int i = 0; i < Layers.Count; i++)
            {
                if (Layers[i].Count > 0)
                {
                    double d = Layers[i].Points[0].X;
                    if (d <= MinValue && d >= MaxValue) Output.Add(Layers[i]);
                }
            }
            Layers.Clear();
            Layers = Output;
        }
        public void ReduceInsideRangeY(double MinValue, double MaxValue)
        {
            List<PointCloud> Output = new List<PointCloud>();
            for (int i = 0; i < Layers.Count; i++)
            {
                if (Layers[i].Count > 0)
                {
                    double d = Layers[i].Points[0].Y;
                    if (d <= MinValue && d >= MaxValue) Output.Add(Layers[i]);
                }
            }
            Layers.Clear();
            Layers = Output;
        }
        public void ReduceInsideRangeZ(double MinValue, double MaxValue)
        {
            List<PointCloud> Output = new List<PointCloud>();
            for (int i = 0; i < Layers.Count; i++)
            {
                if (Layers[i].Count > 0)
                {
                    double d = Layers[i].Points[0].Z;
                    if (d <= MinValue && d >= MaxValue) Output.Add(Layers[i]);
                }
            }
            Layers.Clear();
            Layers = Output;
        }
        public void ReduceOutsideRangeX(double MinValue, double MaxValue)
        {
            List<PointCloud> Output = new List<PointCloud>();
            for (int i = 0; i < Layers.Count; i++)
            {
                if (Layers[i].Count > 0)
                {
                    double d = Layers[i].Points[0].X;
                    if (d >= MinValue && d <= MaxValue) Output.Add(Layers[i]);
                }
            }
            Layers.Clear();
            Layers = Output;
        }
        public void ReduceOutsideRangeY(double MinValue, double MaxValue)
        {
            List<PointCloud> Output = new List<PointCloud>();
            for (int i = 0; i < Layers.Count; i++)
            {
                if (Layers[i].Count > 0)
                {
                    double d = Layers[i].Points[0].Y;
                    if (d >= MinValue && d <= MaxValue) Output.Add(Layers[i]);
                }
            }
            Layers.Clear();
            Layers = Output;
        }

        public void ReduceOutside(Polyline OuterLine)
        {
            KDTree<int> tmp = OuterLine.GetIndexKDtree(true);
            Point3D Center = OuterLine.Average;

            Center.Z = 0;
            LayerPointCloud tmpLayer = new LayerPointCloud();
            for (int j = 0; j < LayerCount; j++)
            {
                PointCloud cloud = new PointCloud();

                for (int i = 0; i < Layers[j].Points.Count; i++)
                {
                    Point3D tempP = new Point3D(Layers[j].Points[i]);
                    tempP.Z = 0;
                    PointCloud NearCloud = PointCloudCommon.GetNearestPointCloud(tmp, tempP, 2);
                    Vector3D tmpV = new Vector3D(NearCloud.Points[1], NearCloud.Points[0]);

                    Point3D CalP = tmpV.ShortestPoint(tempP, NearCloud.Points[1]);
                    //Point3D CalP = NearCloud.Points[0];

                    Vector3D RefV = new Vector3D(CalP, tempP);
                    Vector3D CenterV = new Vector3D(CalP, Center);

                    if (RefV.L != 0.0)
                    {
                        double Dot = Vector3D.Dot(RefV, CenterV);
                        if (Dot >= 0)
                        {
                            cloud.Add(Layers[j].Points[i]);
                        }
                    }
                    else
                    {
                        cloud.Add(Layers[j].Points[i]);
                    }

                }
                tmpLayer.Add(cloud);
            }
            Layers.Clear();
            Layers = tmpLayer.Layers;

        }


        public void ReduceOutsideRangeZ(double MinValue, double MaxValue)
        {
            List<PointCloud> Output = new List<PointCloud>();
            for (int i = 0; i < Layers.Count; i++)
            {
                if (Layers[i].Count > 0)
                {
                    double d = Layers[i].Points[0].Z;
                    if (d >= MinValue && d <= MaxValue) Output.Add(Layers[i]);
                }
            }
            Layers.Clear();
            Layers = Output;
        }

        public LayerPointCloud GetInsideRangeX(double MinValue, double MaxValue)
        {
            LayerPointCloud Output = new LayerPointCloud();
            for (int i = 0; i < Layers.Count; i++)
            {
                if (Layers[i].Count > 0)
                {
                    double d = Layers[i].Points[0].X;
                    if (d >= MinValue && d <= MaxValue) Output.Layers.Add(Layers[i]);
                }
            }
            return Output;
        }
        public PointCloud GetInsideRangeXtoPointCloud(double MinValue, double MaxValue)
        {
            PointCloud Output = new PointCloud();
            for (int i = 0; i < Layers.Count; i++)
            {
                if (Layers[i].Count > 0)
                {
                    double d = Layers[i].Points[0].X;
                    if (d >= MinValue && d <= MaxValue) Output.Add(Layers[i]);
                }
            }
            return Output;
        }
        public LayerPointCloud GetInsideRangeY(double MinValue, double MaxValue)
        {
            LayerPointCloud Output = new LayerPointCloud();
            for (int i = 0; i < Layers.Count; i++)
            {
                if (Layers[i].Count > 0)
                {
                    double d = Layers[i].Points[0].Y;
                    if (d >= MinValue && d <= MaxValue) Output.Layers.Add(Layers[i]);
                }
            }
            return Output;
        }
        public PointCloud GetInsideRangeYtoPointCloud(double MinValue, double MaxValue)
        {
            PointCloud Output = new PointCloud();
            for (int i = 0; i < Layers.Count; i++)
            {
                if (Layers[i].Count > 0)
                {
                    double d = Layers[i].Points[0].Y;
                    if (d >= MinValue && d <= MaxValue) Output.Add(Layers[i]);
                }
            }
            return Output;
        }
        public LayerPointCloud GetInsideRangeZ(double MinValue, double MaxValue)
        {
            LayerPointCloud Output = new LayerPointCloud();
            for (int i = 0; i < Layers.Count; i++)
            {
                if (Layers[i].Count > 0)
                {
                    double d = Layers[i].Points[0].Z;
                    if (d >= MinValue && d <= MaxValue) Output.Layers.Add(Layers[i]);
                }
            }
            return Output;
        }
        public PointCloud GetInsideRangeZtoPointCloud(double MinValue, double MaxValue)
        {
            PointCloud Output = new PointCloud();
            for (int i = 0; i < Layers.Count; i++)
            {
                if (Layers[i].Count > 0)
                {
                    double d = Layers[i].Points[0].Z;
                    if (d >= MinValue && d <= MaxValue) Output.Add(Layers[i]);
                }
            }
            return Output;
        }
        public LayerPointCloud GetOutsideRangeX(double MinValue, double MaxValue)
        {
            LayerPointCloud Output = new LayerPointCloud();
            for (int i = 0; i < Layers.Count; i++)
            {
                if (Layers[i].Count > 0)
                {
                    double d = Layers[i].Points[0].X;
                    if (d <= MinValue && d >= MaxValue) Output.Layers.Add(Layers[i]);
                }
            }
            return Output;
        }
        public PointCloud GetOutsideRangeXtoPointCloud(double MinValue, double MaxValue)
        {
            PointCloud Output = new PointCloud();
            for (int i = 0; i < Layers.Count; i++)
            {
                if (Layers[i].Count > 0)
                {
                    double d = Layers[i].Points[0].X;
                    if (d <= MinValue && d >= MaxValue) Output.Add(Layers[i]);
                }
            }
            return Output;
        }
        public LayerPointCloud GetOutsideRangeY(double MinValue, double MaxValue)
        {
            LayerPointCloud Output = new LayerPointCloud();
            for (int i = 0; i < Layers.Count; i++)
            {
                if (Layers[i].Count > 0)
                {
                    double d = Layers[i].Points[0].Y;
                    if (d <= MinValue && d >= MaxValue) Output.Layers.Add(Layers[i]);
                }
            }
            return Output;
        }
        public PointCloud GetOutsideRangeYtoPointCloud(double MinValue, double MaxValue)
        {
            PointCloud Output = new PointCloud();
            for (int i = 0; i < Layers.Count; i++)
            {
                if (Layers[i].Count > 0)
                {
                    double d = Layers[i].Points[0].Y;
                    if (d <= MinValue && d >= MaxValue) Output.Add(Layers[i]);
                }
            }
            return Output;
        }
        public LayerPointCloud GetOutsideRangeZ(double MinValue, double MaxValue)
        {
            LayerPointCloud Output = new LayerPointCloud();
            for (int i = 0; i < Layers.Count; i++)
            {
                if (Layers[i].Count > 0)
                {
                    double d = Layers[i].Points[0].Z;
                    if (d <= MinValue && d >= MaxValue) Output.Layers.Add(Layers[i]);
                }
            }
            return Output;
        }
        public PointCloud GetOutsideRangeZtoPointCloud(double MinValue, double MaxValue)
        {
            PointCloud Output = new PointCloud();
            for (int i = 0; i < Layers.Count; i++)
            {
                if (Layers[i].Count > 0)
                {
                    double d = Layers[i].Points[0].Z;
                    if (d <= MinValue && d >= MaxValue) Output.Add(Layers[i]);
                }
            }
            return Output;
        }
        public void ReduceEvery(int Count)
        {
            List<PointCloud> Output = new List<PointCloud>();
            if (Count <= 0) Count = 1;
            for (int i = 0; i < Layers.Count; i++)
            {
                if (i % Count == 0) Output.Add(Layers[i]);
            }
            Layers.Clear();
            Layers = Output;
        }

        private Point3D GetMaxPoint()
        {
            double x = double.MinValue;
            double y = double.MinValue;
            double z = double.MinValue;
            for (int i = 0; i < Layers.Count; i++)
            {
                Point3D tmpMax = Layers[i].Max;
                if (tmpMax.X > x) x = tmpMax.X;
                if (tmpMax.Y > y) y = tmpMax.Y;
                if (tmpMax.Z > z) z = tmpMax.Z;
            }
            return new Point3D(x, y, z);
        }
        public Point3D GetMinPoint()
        {
            double x = double.MaxValue;
            double y = double.MaxValue;
            double z = double.MaxValue;
            for (int i = 0; i < Layers.Count; i++)
            {
                Point3D tmpMin = Layers[i].Min;
                if (tmpMin.X < x) x = tmpMin.X;
                if (tmpMin.Y < y) y = tmpMin.Y;
                if (tmpMin.Z < z) z = tmpMin.Z;
            }
            return new Point3D(x, y, z);
        }
        public void Save(string FilePath, bool SaveSplitSection = false)
        {
            using (StreamWriter sw = new StreamWriter(FilePath, false, Encoding.Default))
            {
                for (int i = 0; i < Layers.Count; i++)
                {
                    for (int j = 0; j < Layers[i].Points.Count; j++)
                    {
                        string WriteData = Layers[i].Points[j].ToString(false, false, false);
                        sw.WriteLine(WriteData);
                    }
                    if (SaveSplitSection) sw.WriteLine("");

                }
                sw.Flush();
                sw.Close();
            }
        }
        public void Save(string FilePath, bool WritePointTag, bool WritePointFlag, bool WritePointDt, bool SaveSplitSection = false)
        {
            using (StreamWriter sw = new StreamWriter(FilePath, false, Encoding.Default))
            {
                for (int i = 0; i < Layers.Count; i++)
                {
                    for (int j = 0; j < Layers[i].Points.Count; j++)
                    {
                        string WriteData = Layers[i].Points[j].ToString(WritePointTag, WritePointFlag, WritePointDt);
                        sw.WriteLine(WriteData);
                    }
                    if (SaveSplitSection) sw.WriteLine("");

                }
                sw.Flush();
                sw.Close();
            }
        }

        #region private
        private int TotalPointCount()
        {
            int Count = 0;
            for (int i = 0; i < Layers.Count; i++)
            {
                Count += Layers.Count;
            }
            return Count;
        }
        private bool ConvertXYZ(DigitFormat Format, string str1, string str2, string str3, out double x, out double y, out double z, int RoundDigit = 2)
        {
            string strX, strY, strZ = "";
            x = -9999;
            y = -9999;
            z = -9999;
            switch (Format)
            {
                case DigitFormat.XYZ:
                    strX = str1;
                    strY = str2;
                    strZ = str3;

                    break;

                case DigitFormat.YXZ:
                    strX = str2;
                    strY = str1;
                    strZ = str3;

                    break;

                default:
                    strX = str1;
                    strY = str2;
                    strZ = str3;

                    break;
            }
            if (!double.TryParse(strX, out x)) return false;
            if (!double.TryParse(strY, out y)) return false;
            if (!double.TryParse(strZ, out z)) return false;

            x = Math.Round(x, RoundDigit);
            y = Math.Round(y, RoundDigit);
            z = Math.Round(z, RoundDigit);


            return true;
        }
        private List<string> ReadAllLines(string filePath)
        {
            FileStream fs;
            StreamReader sr;
            List<string> stringList = new List<string>();

            if (!File.Exists(filePath))
                return stringList;

            fs = File.OpenRead(filePath);
            sr = new StreamReader(fs);

            while (sr.Peek() >= 0)
            {
                stringList.Add(sr.ReadLine());
            }

            fs.Flush();
            fs.Close();
            fs.Dispose();
            fs = null;

            sr.Close();
            sr.Dispose();
            sr = null;



            return stringList;
        }


        #endregion
    }
}

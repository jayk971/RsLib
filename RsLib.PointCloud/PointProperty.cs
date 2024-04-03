using Accord.Math;
using Accord.Statistics.Kernels;
using Newtonsoft.Json.Linq;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
namespace RsLib.PointCloudLib
{
    //using CoordTuple = Tuple<List<double>, List<double>, List<double>>;
    //using SelectionTuple = Tuple<Tuple<List<double>, List<double>, List<double>>, Tuple<List<double>, List<double>, List<double>>, Tuple<List<double>, List<double>, List<double>>, Tuple<List<double>, List<double>, List<double>>, List<int>>;

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
    public class DiffOption : ObjectOption
    {
        public Vector3D DiffVector = new Vector3D();
        public double DiffLength => DiffVector.L;
        public Point3D GetEndPoint(Point3D startPt)
        {
            return startPt + DiffVector;
        }
        public Point3D GetEndPoint(Point3D startPt,double distance)
        {
            return startPt + DiffVector.GetUnitVector() * distance;
        }
    }
    public class CompareCloudOption : ObjectOption
    {
        public int _0_20 = 0;
        public int _20_40 = 0;
        public int _40_60 = 0;
        public int _60_80 = 0;
        public int _80_100 = 0;
        public int AcceptCount = 0;

        public double Base20 = 0;
        public double Base40 = 0;
        public double Base60 = 0;
        public double Base80 = 0;
        public double Base100 = 0;
        public double AcceptLimitMin = 0;
        public double AcceptLimitMax = 0;

        public int TotalCount => _0_20 + _20_40 + _40_60 + _60_80 + _80_100;
        public double Ratio_20 => Math.Round((double)_0_20 / (double)TotalCount * 100,1);
        public double Ratio_40 =>Math.Round( (double)_20_40 / (double)TotalCount * 100,1);
        public double Ratio_60 => Math.Round((double)_40_60 / (double)TotalCount * 100,1);
        public double Ratio_80 => Math.Round((double)_60_80 / (double)TotalCount * 100,1);
        public double Ratio_100 => Math.Round((double)_80_100 / (double)TotalCount * 100,1);

        public double Similarity=> Math.Round((double)AcceptCount / (double)TotalCount * 100, 1);


        double _min = double.MinValue;
        double _max = double.MaxValue;
        object _lockObj = new object();
        public CompareCloudOption()
        {

        }
        public CompareCloudOption(double min,double max)
        {
            _min = min;
            _max = max;
        }
        public void Add(double testValue)
        {
            double ratio = (testValue - _min) / (_max - _min) *100;

            lock (_lockObj)
            {
                if (ratio <= 20)
                {
                    _0_20++;
                }
                else if (ratio > 20 && ratio <= 40)
                {
                    _20_40++;
                }
                else if (ratio > 40 && ratio <= 60)
                {
                    _40_60++;
                }
                else if (ratio > 60 && ratio <= 80)
                {
                    _60_80++;
                }
                else
                {
                    _80_100++;
                }
            }
        }
    }
    public class CompareSection10Option : ObjectOption
    {
        public int i_11 = 0;
        public int i_12 = 0;
        public int i_13 = 0;
        public int i_14 = 0;
        public int i_15 = 0;

        public int i_21 = 0;
        public int i_22 = 0;
        public int i_23 = 0;
        public int i_24 = 0;
        public int i_25 = 0;

        public int i_11_Total = 0;
        public int i_12_Total = 0;
        public int i_13_Total = 0;
        public int i_14_Total = 0;
        public int i_15_Total = 0;

        public int i_21_Total = 0;
        public int i_22_Total = 0;
        public int i_23_Total = 0;
        public int i_24_Total = 0;
        public int i_25_Total = 0;

        public double Percent_11 =>Math.Round( (double)i_11 / (double)i_11_Total * 100.0,1);
        public double Percent_12 => Math.Round((double)i_12 / (double)i_12_Total * 100.0, 1);
        public double Percent_13 => Math.Round((double)i_13 / (double)i_13_Total * 100.0, 1);
        public double Percent_14 => Math.Round((double)i_14 / (double)i_14_Total * 100.0, 1);
        public double Percent_15 => Math.Round((double)i_15 / (double)i_15_Total * 100.0, 1);
        public double Percent_21 => Math.Round((double)i_21 / (double)i_21_Total * 100.0, 1);
        public double Percent_22 => Math.Round((double)i_22 / (double)i_22_Total * 100.0, 1);
        public double Percent_23 => Math.Round((double)i_23 / (double)i_23_Total * 100.0, 1);
        public double Percent_24 => Math.Round((double)i_24 / (double)i_24_Total * 100.0, 1);
        public double Percent_25 => Math.Round((double)i_25 / (double)i_25_Total * 100.0, 1);
        public CompareSection10Option()
        {

        }
        public double GetPercent(int index)
        {
            if (index == 0) return Percent_11;
            else if (index == 1) return Percent_12;
            else if (index == 2) return Percent_13;
            else if (index == 3) return Percent_14;
            else if (index == 4) return Percent_15;
            else if (index == 5) return Percent_21;
            else if (index == 6) return Percent_22;
            else if (index == 7) return Percent_23;
            else if (index == 8) return Percent_24;
            else if (index == 9) return Percent_25;
            else return 0.0;
        }
    }

}

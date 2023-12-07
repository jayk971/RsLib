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

}

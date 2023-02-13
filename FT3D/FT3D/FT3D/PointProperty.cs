using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Drawing;
using System.Collections;

namespace RsLib.PointCloud
{
    [Serializable]
    public class ObjectOption
    {

    }
    [Serializable]
    public  abstract class Object3D:IEnumerable<ObjectOption>
    {
        public string Name = "";
        List<ObjectOption> _Options = new List<ObjectOption>();
        public List<ObjectOption> Options => _Options;
        public abstract uint DataCount { get;}

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
                if(t == _Options[i].GetType())
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
                if(t == optionType)
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
    public class ObjectGroup :Object3D
    {
        public Dictionary<string, Object3D> Objects { get; private set; } = new Dictionary<string, Object3D>();
        public List<string> Sequence { get; private set; } = new List<string>();

        public override uint DataCount => (uint)Objects.Count;
        public uint TotalDataCount
        {
            get
            {
                uint sum = 0;
                foreach(KeyValuePair<string, Object3D> kvp in Objects)
                {
                    sum += kvp.Value.DataCount;
                }

                return sum;
            }
        }


        public ObjectGroup(string groupName)
        {
            base.Name = groupName;
        }
        public void Add(string objName,Object3D object3D)
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
        public void Clear()
        {
            Objects.Clear();
            Sequence.Clear();
        }
    }

}

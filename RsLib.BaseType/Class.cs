using System.Collections.Generic;

namespace RsLib.BaseType
{
    public class LockQueue<T> : Queue<T>
    {
        private readonly object _lock = new object();
        public new int Count
        {
            get
            {
                lock (_lock)
                {
                    return base.Count;
                }
            }
        }
        public bool IsEmpty
        {
            get
            {
                lock (_lock)
                {
                    return base.Count == 0;
                }
            }
        }

        public new void Enqueue(T obj)
        {
            lock (_lock)
            {
                base.Enqueue(obj);
            }
        }
        public new T Dequeue()
        {
            lock (_lock)
            {
                return base.Dequeue();
            }
        }
        public T ElementAt(int index)
        {
            lock (_lock)
            {
                T[] lst = base.ToArray();
                if (lst.Length > index)
                {
                    return lst[index];
                }
                else return default;
            }
        }
        public new T Peek()
        {
            lock (_lock)
            {
                return base.Peek();
            }
        }
        public new void Clear()
        {
            lock (_lock)
            {
                base.Clear();
            }
        }
        public new bool Contains(T obj)
        {
            lock (_lock)
            {
                return base.Contains(obj);
            }
        }
        public new T[] ToArray()
        {
            lock (_lock)
            {
                return base.ToArray();
            }
        }
    }

}

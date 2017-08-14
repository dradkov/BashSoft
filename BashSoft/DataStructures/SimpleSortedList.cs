namespace BashSoft.DataStructures
{
    using BashSoft.Interfaces;
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Text;

    public class SimpleSortedList<T> : ISimpleOrderedBag<T> where T : IComparable<T>
    {
        private const int DefaultSize = 16;

        private T[] innerColection;
        private int size;
        private IComparer<T> comparison;

        public SimpleSortedList(int capacity)
            : this(Comparer<T>.Create((x, y) => x.CompareTo(y)), capacity)
        {
            this.size = 0; // Possible Problem
            this.innerColection = new T[capacity];
          
        }

        public SimpleSortedList(IComparer<T> comparer, int capacity)
        {
            this.comparison = comparer;
            this.innerColection = new T[capacity];
        }

        public SimpleSortedList(IComparer<T> comparer)
        {
            this.comparison = comparer;
            this.innerColection = new T[DefaultSize];

        }
        public SimpleSortedList()
        {
            
            this.innerColection = new T[DefaultSize];

        }

        public int Size
        {
            get
            {
                return this.size;

            }

        }

        public bool Remove(T element)
        {
            bool hasBeenRemoved = false;
            int indexOfRemovedElement = 0;

            for (int i = 0; i < this.Size; i++)
            {
                if (this.innerColection[i].Equals(element))
                {
                    indexOfRemovedElement = i;
                    this.innerColection[i] = default(T);
                    hasBeenRemoved = true;
                    break;
                }
            };

            if (hasBeenRemoved)
            {
                for (int i = indexOfRemovedElement; i < this.Size - 1; i++)
                {
                    this.innerColection[i] = this.innerColection[i + 1];
                }
                this.innerColection[this.size - 1] = default(T);
            }

            return hasBeenRemoved;

        }

        public int Capacity
        {
            get { return this.innerColection.Length; }

        }

        public void Add(T element)
        {
            if (element == null)
            {
                throw  new InvalidOperationException("The element Cannot be null");
            }

            if (this.innerColection.Length == this.Size)
            {
                Resize();
            }
            this.innerColection[size] = element;
            this.size++;
            Array.Sort(this.innerColection, 0, size, comparison);
        }

        public void AddAll(ICollection<T> collection)
        {
            if (this.Size + collection.Count >= this.innerColection.Length)
            {
                this.MultiResize(collection);
            }
            foreach (var element in collection)
            {
                this.innerColection[this.Size] = element;
                this.size++;
            }
            Array.Sort(this.innerColection, 0, this.size, this.comparison);
        }

        private void MultiResize(ICollection<T> collection)
        {
            int newSize = this.innerColection.Length * 2;
            while (this.Size + collection.Count >= newSize)
            {
                newSize *= 2;
            }

            T[] newCollection = new T[newSize];
            Array.Copy(this.innerColection, newCollection, this.size);
            this.innerColection = newCollection;
        }

        public string JoinWith(string joiner)
        {
            var builder = new StringBuilder();
            foreach (var element in this)
            {
                builder.Append(element);
                builder.Append(joiner);
            }
            builder.Remove(builder.Length - 1, 1);
            return builder.ToString();
        }


        public IEnumerator<T> GetEnumerator()
        {
            for (int i = 0; i < this.Size; i++)
            {
                yield return this.innerColection[i];
            }
        }


        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }



        private void InitializeInnerCollection(int capacity)
        {
            if (capacity < 0)
            {
                throw new ArgumentException("Capacity cannot be negative!");
            }

            this.innerColection = new T[capacity];
        }
        private void Resize()
        {
            T[] newCollection = new T[this.Size * 2];
            Array.Copy(innerColection, newCollection, Size);
            innerColection = newCollection;
        }
    }
}

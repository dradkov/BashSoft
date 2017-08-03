namespace BashSoft.DataStructures
{
    using BashSoft.Interfaces;
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Text;
    using BashSoft.Contracts;

    public class SimpleSortedList<T> : ISimpleOrderedBag<T>  where T :IComparable<T> 
    {
        private const int DefaultSize = 16;

        private T[] innerColection;
        private int size;
        private IComparer<T> comparison;
        

        public SimpleSortedList(int capacity)
            : this(Comparer<T>.Create((x,y)=>x.CompareTo(y)),capacity)
        {

        }

        public SimpleSortedList(IComparer<T> comparer, int capacity)
        {
            this.comparison = comparer;
            this.size = capacity;
        }

        public SimpleSortedList(IComparer<T> comparer)
        {
            this.comparison = comparer;
            
        }


        public int Size
        {
            get
            {
                return this.size;

            }
        }

        public void Add(T element)
        {
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
            if (this.Size + collection.Count>= this.innerColection.Length)
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
            while (this.Size+collection.Count>=newSize)
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
            if (capacity<0)
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

using System;
using System.Collections;
using System.Collections.Generic;

namespace test
{
    public class Stack<T> : IStack<T>, IEnumerable<T>
    {
        private const int DefaultCapacity = 4;

        private T[] _elements;
        private int _size = 0;

        /// <summary>
        /// Initialize a new instance of Stack class that is empty and has the default initial capacity.
        /// </summary>
        public Stack() : this(DefaultCapacity)
        {
        }

        /// <summary>Initializes a new instance of the Stack class that is empty 
        /// and has the specified initial capacity or the default initial capacity, 
        /// whichever is greater.</summary>
        public Stack(int capacity)
        {
            if (capacity < 0)
            {
                throw new ArgumentOutOfRangeException("capacity");
            }

            this._elements = new T[Math.Max(DefaultCapacity, capacity)];
        }

        /// <summary>Initializes a new instance of the Stack class class that contains 
        /// elements copied from the specified collection.</summary>
        /// <param name="collection">The collection contains elements to seed.</param>
        public Stack(IEnumerable<T> collection)
            : this()
        {
            if (collection == null)
            {
                throw new NullReferenceException("Seed collection cannot be null.");
            }

            foreach (var element in collection)
            {
                this.Push(element);
            }
        }

        /// <summary>
        /// Gets the number of elements contained in the collection.
        /// </summary>
        public int Count
        {
            get
            {
                return this._size;
            }
        }

        /// <summary>
        /// Gets the total number of elements the data structure can hold.
        /// </summary>
        public int Capacity => this._elements.Length;

        /// <summary>
        /// Inserts an objects at the top of the collection.
        /// </summary>
        /// <param name="value">The object to push onto the collection. The value can be null for reference types.</param>
        public void Push(T value)
        {
            this.ExpandCapacity();
            this._elements[this._size++] = value;
        }

        /// <summary>
        /// Returns the object at the top of the collection without removing it.
        /// </summary>
        public T Peek()
        {
            if (this._size == 0)
            {
                throw new ArgumentException("There is no elements in stack.");
            }

            return this._elements[this._size - 1];
        }

        /// <summary>
        /// Removes and returns the object at the top of the collection.
        /// </summary>
        public T Pop()
        {
            if (this._size == 0)
            {
                throw new ArgumentException("There is no elements in stack.");
            }

            var value = this._elements[this._size - 1];
            this._elements[this._size - 1] = default(T);
            this._size--;

            return value;
        }

        /// <summary>
        /// Removes all objects from the collection.
        /// </summary>
        public void Clear()
        {
            Array.Clear(this._elements, 0, this._size);
            this._size = 0;
        }

        /// <summary>
        /// Sets the capacity to the actual number of elements in the Stack,
        /// if that number is less than 90 percent of current capacity.
        /// </summary>
        public void TrimExcess()
        {
            int length = (int)(this._elements.Length * 0.9);

            if (this._size < length)
            {
                T[] resizedArray = new T[this._size];
                Array.Copy(this._elements, 0, resizedArray, 0, this._size);
                this._elements = resizedArray;
            }
        }

        /// <summary>
        /// Returns an enumerator that iterates through the collection.
        /// </summary>
        public IEnumerator<T> GetEnumerator()
        {
            for (int i = 0; i < this.Count; i++)
            {
                yield return this._elements[i];
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }

        private void ExpandCapacity()
        {
            if (this._size == this._elements.Length)
            {
                T[] resizedArray = new T[this._elements.Length == 0 ? DefaultCapacity : 2 * this._elements.Length];
                Array.Copy(this._elements, 0, resizedArray, 0, this._size);
                this._elements = resizedArray;
            }
        }
    }

    public interface IStack<T>
    {
        void Push(T value);

        T Peek();

        T Pop();

        void Clear();
    }
}
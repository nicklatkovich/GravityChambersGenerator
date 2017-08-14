using System.Collections.Generic;

namespace GravityLabChamberGenerator {
    class Vector<T> {
        private T[ ] _arr = new T[1];
        public uint Size {
            get;
            set;
        } = 0;
        public T Last => this[Size - 1];

        public Vector( ) {
        }

        public Vector(ICollection<T> source) {
            Size = (uint)source.Count;
            _arr = new T[Size];
            uint index = 0;
            foreach (T value in source) {
                _arr[index++] = value;
            }
        }

        public void Push(T value) {
            if (Size == _arr.Length) {
                T[ ] newArr = new T[Size * 2];
                for (uint i = 0; i < Size; i++) {
                    newArr[i] = this[i];
                }
                _arr = newArr;
            }
            this[Size++] = value;
        }

        public T this[uint index] {
            get {
                return _arr[index];
            }
            set {
                _arr[index] = value;
            }

        }
    }
}
using System;
using System.Collections;
using System.Collections.Generic;

namespace GravityLabChamberGenerator {
    class Vector<T> : IEnumerable<T> {
        private T[ ] _arr = new T[1];
        private uint _size = 0;
        public uint Size {
            get {
                return _size;
            }
            set {
                if (_size > value) {
                    _size = value;
                    Clean( );
                } else if (_size < value) {
                    throw new NotSupportedException( );
                }
            }
        }
        public T Last => this[Size - 1];

        #region Constructors
        public Vector( ) {
        }

        public Vector(ICollection<T> source) {
            _size = (uint)source.Count;
            _arr = new T[Size];
            uint index = 0;
            foreach (T value in source) {
                _arr[index++] = value;
            }
        }

        public Vector(IEnumerable<T> source) {
            foreach (var a in source) {
                Push(a);
            }
        }
        #endregion

        public void Push(T value) {
            if (Size == _arr.Length) {
                T[ ] newArr = new T[Size * 2];
                for (uint i = 0; i < Size; i++) {
                    newArr[i] = this[i];
                }
                _arr = newArr;
            }
            this[_size++] = value;
        }

        public T this[uint index] {
            get {
                return _arr[index];
            }
            set {
                _arr[index] = value;
            }

        }
        public Vector<T> Clone( ) {
            return new Vector<T>(this as IEnumerable<T>);
        }

        public void Clean( ) {
            Vector<T> res = Clone( );
            _arr = res._arr;
        }

        #region Enumeration
        public class Enumerator : IEnumerator<T> {
            private uint _pos;
            private Vector<T> _v;
            private bool _start = false;

            public object Current {
                get {
                    return _v[_pos];
                }
            }

            T IEnumerator<T>.Current {
                get {
                    return _v[_pos];
                }
            }

            public bool MoveNext( ) {
                if (_start) {
                    _pos++;
                }
                _start = true;
                return _pos < _v.Size;
            }
            public void Reset( ) {
                _pos = 0;
            }

            public void Dispose( ) {
            }

            internal Enumerator(Vector<T> v) {
                _v = v;
                _pos = 0;
            }
        }

        IEnumerator<T> IEnumerable<T>.GetEnumerator( ) {
            return new Enumerator(this);
        }

        public IEnumerator GetEnumerator( ) {
            return new Enumerator(this);
        }
        #endregion
    }
}
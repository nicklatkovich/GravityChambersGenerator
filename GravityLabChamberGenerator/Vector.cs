namespace GravityLabChamberGenerator {
    class Vector<T> {
        private T[ ] _arr = new T[1];
        public uint Size {
            get;
            protected set;
        } = 0;

        public Vector( ) {
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
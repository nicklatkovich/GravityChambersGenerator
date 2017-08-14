namespace GravityLabChamberGenerator {
    class Grid<T> {
        private T[ ][ ] arr;

        public Grid(int width, int height, T defaultValue) {
            arr = new T[width][ ];
            for (uint i = 0; i < width; i++) {
                arr[i] = new T[height];
                for (uint j = 0; j < height; j++) {
                    this[i, j] = defaultValue;
                }
            }
        }

        public T this[Point p] {
            get {
                return this[(uint)p.X, (uint)p.Y];
            }
            set {
                this[(uint)p.X, (uint)p.Y] = value;
            }
        }

        public T this[uint x, uint y] {
            get {
                return arr[x][y];
            }
            set {
                arr[x][y] = value;
            }
        }
    }
}

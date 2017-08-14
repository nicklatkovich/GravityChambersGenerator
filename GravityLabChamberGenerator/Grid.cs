namespace GravityLabChamberGenerator {
    class Grid<T> {
        private T[ ][ ] arr;
        public Point Size {
            get; protected set;
        }
        public uint Width => (uint)Size.X;
        public uint Height => (uint)Size.Y;

        public Grid(int width, int height, T defaultValue) {
            arr = new T[width][ ];
            for (uint i = 0; i < width; i++) {
                arr[i] = new T[height];
                for (uint j = 0; j < height; j++) {
                    this[i, j] = defaultValue;
                }
            }
            Size = new Point(width, height);
        }

        public bool HasCell(Point pos) {
            return pos.X >= 0 && pos.X < Width && pos.Y >= 0 && pos.Y < Height;
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

        public override string ToString( ) {
            string result = "";
            result += Width + '\n';
            result += Height + '\n';
            for (uint y = 0; y < Height; y++) {
                for (uint x = 0; x < Width; x++) {
                    // TODO: make converting for non one-symbols cells
                    result += this[x, y];
                }
                result += '\n';
            }
            return result;
        }
    }
}

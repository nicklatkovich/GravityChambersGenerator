using System;
using System.Collections.Generic;

namespace GravityLabs._utils {
    public class Grid<T> {
        private T[ ][ ] arr;
        public Point Size {
            get; protected set;
        }
        public uint Width => (uint)Size.X;
        public uint Height => (uint)Size.Y;

        public Grid(uint width, uint height, T defaultValue) {
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
            result += Width.ToString( ) + '\n';
            result += Height.ToString( ) + '\n';
            for (uint y = 0; y < Height; y++) {
                for (uint x = 0; x < Width; x++) {
                    // TODO: make converting for non one-symbols cells
                    result += this[x, y];
                }
                result += '\n';
            }
            return result;
        }

        public void Replace(Dictionary<T, T> replaces) {
            for (uint x = 0; x < Width; x++) {
                for (uint y = 0; y < Height; y++) {
                    if (replaces.ContainsKey(this[x, y])) {
                        this[x, y] = replaces[this[x, y]];
                    }
                }
            }
        }

        public Grid<K> Convert<K>(Func<T, K> converter) {
            Grid<K> result = new Grid<K>(Width, Height, converter(this[0, 0]));
            for (uint x = 0; x < Width; x++) {
                for (uint y = 0; y < Height; y++) {
                    result[x, y] = converter(this[x, y]);
                }
            }
            return result;
        }

        public Grid<K> Convert<K>(Dictionary<T, K> converter) {
            Grid<K> result = new Grid<K>(Width, Height, converter[this[0, 0]]);
            for (uint x = 0; x < Width; x++) {
                for (uint y = 0; y < Height; y++) {
                    result[x, y] = converter[this[x, y]];
                }
            }
            return result;
        }
    }
}

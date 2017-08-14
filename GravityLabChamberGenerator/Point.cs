namespace GravityLabChamberGenerator {
    class Point {
        public int X;
        public int Y;

        public Point(int x, int y) {
            X = x;
            Y = y;
        }

        public Point(uint x, uint y) {
            X = (int)x;
            Y = (int)y;
        }

        public override bool Equals(object obj) {
            if (obj is Point) {
                Point other = obj as Point;
                return X == other.X && Y == other.Y;
            }
            return false;
        }

        public override int GetHashCode( ) {
            return X + Y * 65533;
        }

        public Point Clone( ) {
            return new Point(X, Y);
        }

        public void Move(uint direction) {
            uint d = direction % 4;
            X += Utils.DX[d];
            Y += Utils.DY[d];
        }

        public override string ToString( ) {
            return "{" + X + ", " + Y + "}";
        }

        public static bool operator ==(Point p1, Point p2) {
            return p1.Equals(p2);
        }

        public static bool operator !=(Point p1, Point p2) {
            return !p1.Equals(p2);
        }
    }
}

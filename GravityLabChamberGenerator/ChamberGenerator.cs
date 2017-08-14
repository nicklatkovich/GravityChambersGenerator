using System;
using System.Collections.Generic;

namespace GravityLabChamberGenerator {
    static class ChamberGenerator {
        public static Chamber Generate(uint width, uint height) {
            Chamber result = new Chamber( );
            Point startPoint = new Point(
                Utils.URandom(width - 2) + 1,
                Utils.URandom(height - 2) + 1);
            result.StartPoint = startPoint;
            result.Walls = GenerateRoom(width, height, startPoint);
            return result;
        }

        private static bool RoomConverter(uint source) {
            return source == 0 || source == 3;
        }

        public static Grid<bool> GenerateRoom(uint width, uint height, Point startPoint) {
            Grid<uint> map = new Grid<uint>(32, 16, 0);
            map[startPoint] = 2;
            Queue<Point> q = new Queue<Point>( );
            q.Enqueue(startPoint.Clone( ));
            while (q.Count > 0) {
                Point pos = q.Dequeue( );
                map[pos] = 1;
                List<uint> shuffle = Utils.Shuffle(4);
                foreach (uint direction in shuffle) {
                    Vector<Point> v = new Vector<Point>( );
                    Point wallPos = pos.Clone( );
                    Point targetPos = pos.Clone( );
                    while (true) {
                        wallPos.Move(direction);
                        if (!map.HasCell(wallPos)) {
                            break;
                        }
                        if ((map[wallPos] == 0 || map[wallPos] == 3) && map[targetPos] == 0) {
                            v.Push(wallPos.Clone( ));
                        }
                        if (map[wallPos] == 2 || map[wallPos] == 3) {
                            break;
                        }
                        targetPos.Move(direction);
                    }
                    if (v.Size > 0) {
                        wallPos = v[Utils.URandom(v.Size)];
                        targetPos = wallPos.Clone( );
                        targetPos.Move(direction + 2);
                        Point forPos = pos.Clone( );
                        forPos.Move(direction);
                        while (!forPos.Equals(targetPos)) {
                            map[forPos] = 1;
                            forPos.Move(direction);
                        }
                        map[targetPos] = 2;
                        map[wallPos] = 3;
                        q.Enqueue(targetPos);
                    }
                }
            }
            return map.Convert(RoomConverter);
        }

        public static void GenerateGraph(Chamber chamber) {
            Queue<Tuple<Vector<Point>, uint>> queue = new Queue<Tuple<Vector<Point>, uint>>( );
            queue.Enqueue(new Tuple<Vector<Point>, uint>(new Vector<Point>(
                new Point[ ] { chamber.StartPoint.Clone( ) }), 0));
            Vector<Tuple<Vector<Point>, uint>> result = new Vector<Tuple<Vector<Point>, uint>>( );
            Dictionary<Point, Vector<Point>> mostFarCells = new Dictionary<Point, Vector<Point>>( );
            while (queue.Count > 0) {
                Tuple<Vector<Point>, uint> deq = queue.Dequeue( );
                Vector<Point> way = deq.Item1;
                uint cost = deq.Item2;
                if (!mostFarCells.ContainsKey(way.Last)) {

                }
            }
        }
    }
}

using System;
using System.Collections.Generic;

namespace GravityLabChamberGenerator {
    class Program {
        static void Main(string[ ] args) {
            Grid<uint> map = new Grid<uint>(32, 16, 0);
            Point start = new Point(
                Utils.URandom(map.Width - 2) + 1,
                Utils.URandom(map.Height - 2) + 1);
            Console.WriteLine("Start is " + start);
            map[start] = 2;
            Queue<Point> q = new Queue<Point>( );
            q.Enqueue(start.Clone( ));
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
            map.Replace(new Dictionary<uint, uint>( ) {
                { 0, 1 },
                { 3, 1 },
                { 1, 0 },
            });
            Console.WriteLine(map);

#if DEBUG
            Console.ReadKey( );
#endif

        }
    }
}

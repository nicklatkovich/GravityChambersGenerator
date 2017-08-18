using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace GravityLabChamberGenerator {
    public class ChamberGenerator {

        public Chamber Chamber = new Chamber( );
        public Vector<Point> Way = new Vector<Point>( );
        public StreamWriter Logger = null;

        public delegate void ChamberGeneratorEvent( );
        public event ChamberGeneratorEvent WallsCreated;
        public event ChamberGeneratorEvent WayFinded;

        public enum Event {
            ROOM_CREATED,
            WAY_FINDED,
        }

        protected void Generate(uint width, uint height, StreamWriter logger = null, IProgress<Event> progress = null) {
            Logger = logger;
            Point startPoint = new Point(
                Utils.URandom(width - 2) + 1,
                Utils.URandom(height - 2) + 1);
            Chamber.StartPoint = startPoint;

            Logger?.WriteLine("Start Position = " + startPoint);
            Chamber.Walls = GenerateRoom(width, height, startPoint);
            progress?.Report(Event.ROOM_CREATED);
            WallsCreated?.Invoke( );
            Logger?.WriteLine("Room:");
            Logger?.Write(Chamber.WallsGridPresent(Chamber.Walls));

            Vector<Vector<Point>> ways = null;
            ways = GenerateWay(Chamber);
            Way = ways[Utils.URandom(ways.Size)];
            progress?.Report(Event.WAY_FINDED);
            WayFinded?.Invoke( );
            if (Logger != null) {
                foreach (Point wayPoint in Way) {
                    Logger?.WriteLine("\t" + wayPoint);
                }
            }

        }

        public static Grid<bool> GenerateRoom(uint width, uint height, Point startPoint) {
            Grid<uint> map = new Grid<uint>(width, height, 0);
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
            return map.Convert(new Dictionary<uint, bool>( ) {
                { 0, true },
                { 1, false },
                { 3, true },
            });
        }

        public static Tuple<Vector<Point>, uint> GetMostFarCells(Grid<bool> walls, Point from) {
            Grid<int> wayLengthes = new Grid<int>(walls.Width, walls.Height, -1);
            wayLengthes[from] = 0;
            Queue<Point> q = new Queue<Point>( );
            q.Enqueue(from.Clone( ));
            Vector<Point> result = new Vector<Point>( );
            uint maxSteps = 0;
            while (q.Count > 0) {
                Point pos = q.Dequeue( );
                for (uint d = 0; d < 4; d++) {
                    Point dirPos = pos.Clone( );
                    Point prevPos = pos.Clone( );
                    dirPos.Move(d);
                    while (!walls[dirPos]) {
                        dirPos.Move(d);
                        prevPos.Move(d);
                    }
                    if (wayLengthes[prevPos] == -1 || wayLengthes[prevPos] > wayLengthes[pos] + 1) {
                        wayLengthes[prevPos] = wayLengthes[pos] + 1;
                        if (maxSteps < wayLengthes[prevPos]) {
                            maxSteps = (uint)wayLengthes[prevPos];
                            result.Size = 0;
                            result.Push(prevPos.Clone( ));
                        } else if (maxSteps == wayLengthes[prevPos]) {
                            result.Push(prevPos.Clone( ));
                        }
                        q.Enqueue(prevPos.Clone( ));
                    }
                }
            }
            return new Tuple<Vector<Point>, uint>(result, maxSteps);
        }

        public static Vector<Vector<Point>> GenerateWay(Chamber chamber) {
            Queue<Tuple<Vector<Point>, uint>> q = new Queue<Tuple<Vector<Point>, uint>>( );
            q.Enqueue(new Tuple<Vector<Point>, uint>(new Vector<Point>(
                new Point[ ] { chamber.StartPoint.Clone( ) }), 0));
            Vector<Vector<Point>> result = new Vector<Vector<Point>>( );
            uint maxCost = 0;
            Dictionary<Point, Tuple<Vector<Point>, uint>> mostFarCells =
                new Dictionary<Point, Tuple<Vector<Point>, uint>>( );
            while (q.Count > 0) {
                Tuple<Vector<Point>, uint> deq = q.Dequeue( );
                Vector<Point> way = deq.Item1;
                uint cost = deq.Item2;
                if (!mostFarCells.ContainsKey(way.Last)) {
                    mostFarCells[way.Last] = GetMostFarCells(chamber.Walls, way.Last);
                }
                Tuple<Vector<Point>, uint> mfc = mostFarCells[way.Last];
                Vector<Point> cells = mfc.Item1;
                uint length = mfc.Item2;
                uint newCost = cost + length;
                foreach (Point a in cells) {
                    bool needToAdd;
                    if (needToAdd = way[0] != a) {
                        for (uint i = 0; i < way.Size - 1; i++) {
                            if (way[i] == way.Last && way[i + 1] == a) {
                                needToAdd = false;
                                break;
                            }
                        }
                        if (needToAdd) {
                            bool canBeResult = true;
                            for (uint i = 0; i < way.Size; i++) {
                                if (way[i] == a) {
                                    canBeResult = false;
                                    break;
                                }
                            }
                            Vector<Point> newWay = way.Clone( );
                            newWay.Push(a);
                            q.Enqueue(new Tuple<Vector<Point>, uint>(newWay.Clone( ), newCost));
                            if (canBeResult) {
                                if (newCost > maxCost) {
                                    result.Size = 0;
                                    result.Push(newWay.Clone( ));
                                    maxCost = newCost;
                                } else if (newCost == maxCost) {
                                    result.Push(newWay.Clone( ));
                                }
                            }
                        }
                    }
                }
            }
            return result;
        }

        public static ChamberGenerator CreateChamber(uint width, uint height, StreamWriter logger = null, IProgress<Event> progress = null) {
            ChamberGenerator result = new ChamberGenerator( );
            result.Generate(width, height, logger, progress);
            return result;
        }

        protected ChamberGenerator( ) {

        }

    }
}

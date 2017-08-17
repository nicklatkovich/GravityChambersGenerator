﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace GravityLabChamberGenerator {
    public class Chamber {
        public delegate void handler( );

        public Point StartPoint;
        public Grid<bool> Walls = null;
        public Vector<Point> Way = null;
        public TextWriter Logger = null;

        public event handler WallsCreated;

        public Chamber( ) {
        }

        public static void Generate(Chamber chamber, uint width, uint height, TextWriter logger = null) {
            chamber.Logger = logger;
            Point startPoint = new Point(
                Utils.URandom(width - 2) + 1,
                Utils.URandom(height - 2) + 1);
            chamber.StartPoint = startPoint;
            chamber.Logger?.WriteLine("Start Position = " + startPoint);
            chamber.Walls = GenerateRoom(width, height, startPoint);
            chamber.WallsCreated( );
            chamber.Logger?.WriteLine("Room:");
            chamber.Logger?.Write(WallsGridPresent(chamber.Walls));
            Vector<Vector<Point>> ways = GenerateWay(chamber);
            chamber.Way = ways[Utils.URandom(ways.Size)];
            foreach (Point wayPoint in chamber.Way) {
                chamber.Logger?.WriteLine("\t" + wayPoint);
            }
        }

        private static char WallsPresentorTranslator(bool source) {
            return source ? '#' : '-';
        }
        private static string WallsGridPresent(Grid<bool> wallsMap) {
            return wallsMap.Convert(WallsPresentorTranslator).ToString( );
        }

        private static bool RoomConverter(uint source) {
            return source == 0 || source == 3;
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
            return map.Convert(RoomConverter);
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

        public static Grid<bool> LoadWallsMapFromFile(string fileName) {
            Grid<bool> map;
            using (FileStream fs = new FileStream(fileName, FileMode.OpenOrCreate)) {
                using (StreamReader sr = new StreamReader(fs)) {
                    string s;
                    s = sr.ReadLine( );
                    uint width = uint.Parse(s);
                    s = sr.ReadLine( );
                    uint height = uint.Parse(s);
                    map = new Grid<bool>(width, height, false);
                    for (uint y = 0; y < height; y++) {
                        s = sr.ReadLine( );
                        for (uint x = 0; x < width; x++) {
                            map[x, y] = s[(int)x] == '1';
                        }
                    }
                }
            }
            return map;
        }
    }
}

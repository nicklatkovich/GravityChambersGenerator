using GravityLabs._utils;
using System.Collections.Generic;
using System.IO;

namespace GravityLabs {
    public class Chamber {

        public Point StartPoint;
        public Grid<bool> Walls = null;

        public Chamber( ) {

        }

        internal static string WallsGridPresent(Grid<bool> wallsMap) {
            return wallsMap.Convert(new Dictionary<bool, char>( ) {
                { true, '#' },
                { false, '-' },
            }).ToString( );
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

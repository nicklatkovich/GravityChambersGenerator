using System;
using System.Collections.Generic;

namespace GravityLabChamberGenerator {
    class Program {
        private static void Main(string[ ] args) {
            int seed = 0;
            bool work = true;
            while (work) {
                Console.Clear( );
                Utils.RandomSetSeed(seed);
                Console.WriteLine("#" + seed);
                Chamber chamber = Chamber.Generate(16, 8);
                Console.WriteLine("Start position is " + chamber.StartPoint);
                Console.WriteLine(WallsGridPresent(chamber.Walls));
                foreach (var a in chamber.Way) {
                    Console.WriteLine("\t" + a);
                }
                bool hasCommand = false;
                while (!hasCommand) {
                    ConsoleKey key = Console.ReadKey( ).Key;
                    switch (key) {
                    case ConsoleKey.LeftArrow:
                        seed--;
                        if (seed < 0) {
                            seed = 0;
                        }
                        hasCommand = true;
                        break;
                    case ConsoleKey.RightArrow:
                        seed++;
                        hasCommand = true;
                        break;
                    case ConsoleKey.Escape:
                        hasCommand = true;
                        work = false;
                        break;
                    }
                }
            }
        }

        private static char WallsPresentorTranslator(bool source) {
            return source ? '8' : ' ';
        }
        private static string WallsGridPresent(Grid<bool> wallsMap) {
            return wallsMap.Convert(WallsPresentorTranslator).ToString( );
        }
    }
}

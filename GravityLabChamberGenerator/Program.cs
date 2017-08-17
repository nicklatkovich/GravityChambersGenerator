using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace GravityLabChamberGenerator {
    class Program {
        private static Chamber chamber;

        private static void Main(string[ ] args) {
            int seed = 0;
            bool work = true;
            while (work) {
                Console.Clear( );
                Utils.RandomSetSeed(seed);
                Console.WriteLine("#" + seed);
                chamber = new Chamber( );
                Thread thr = new Thread(Generate);
                thr.Start( );
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
                thr.Abort( );
            }
        }

        private static void Generate( ) {
            Chamber.Generate(chamber, 16, 8, Console.Out);
        }
    }
}

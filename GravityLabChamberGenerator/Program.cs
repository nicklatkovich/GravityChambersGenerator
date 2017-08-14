using System;
using System.Collections.Generic;

namespace GravityLabChamberGenerator {
    class Program {
        private static void Main(string[ ] args) {
            Chamber chamber = ChamberGenerator.Generate(32, 16);
            Console.WriteLine("Start position is " + chamber.StartPoint);
            Console.WriteLine(WallsGridPresent(chamber.Walls));

#if DEBUG
            Console.ReadKey( );
#endif

        }

        private static char WallsPresentorTranslator(bool source) {
            return source ? '8' : ' ';
        }
        private static string WallsGridPresent(Grid<bool> wallsMap) {
            return wallsMap.Convert(WallsPresentorTranslator).ToString( );
        }
    }
}

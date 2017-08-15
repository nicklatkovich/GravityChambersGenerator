using System;
using System.Collections.Generic;

namespace GravityLabChamberGenerator {
    static class Utils {
        private static Random _rand = new Random( );

        public static double Random( ) => _rand.NextDouble( );
        public static double Random(double x) => Random( ) * x;
        public static uint URandom(uint x) => (uint)Math.Floor(Random(x));
        public static void RandomSetSeed(int seed) {
            _rand = new Random(seed);
        }

        public static List<uint> Shuffle(uint numbersCount) {
            uint[ ] preresult = new uint[numbersCount];
            for (uint i = 0; i < numbersCount; i++) {
                preresult[i] = i;
            }
            for (uint i = 0; i < numbersCount; i++) {
                uint j = URandom(numbersCount - 1);
                if (j >= i) {
                    j++;
                }
                uint swap = preresult[i];
                preresult[i] = preresult[j];
                preresult[j] = swap;
            }
            return new List<uint>(preresult);
        }

        public static readonly int[ ] DX = { 1, 0, -1, 0 };
        public static readonly int[ ] DY = { 0, -1, 0, 1 };
    }
}

using System;

namespace GravityLabChamberGenerator {
    static class Utils {
        private static Random _rand = new Random( );

        public static double Random( ) {
            return _rand.NextDouble( );
        }

        public static uint URandom(uint x) {
            return (uint)Math.Floor(Random( ) * x);
        }
    }
}

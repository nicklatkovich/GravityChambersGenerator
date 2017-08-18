using System;
using System.IO;
using System.Threading;

namespace GravityLabs.ChambersGenerator {
    public class ChamberGeneratorAsync : ChamberGenerator {

        public Thread GenerationThread = null;

        public event ChamberGeneratorEvent GenerationAborted;

        public void BreakGeneration( ) {
            if (GenerationThread.IsAlive) {
                GenerationAborted?.Invoke( );
                GenerationThread.Abort( );
            }
        }

        public static ChamberGeneratorAsync CreateChamberInThread(uint width, uint height, StreamWriter logger = null, IProgress<Event> progress = null) {
            ChamberGeneratorAsync Generator = new ChamberGeneratorAsync( );
            (Generator.GenerationThread = new Thread(( ) => {
                Generator.Generate(width, height, logger, progress);
            })).Start( );
            return Generator;
        }

        private ChamberGeneratorAsync( ) : base( ) {

        }

    }
}

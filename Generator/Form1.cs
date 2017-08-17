using GravityLabChamberGenerator;
using System;
using System.Drawing;
using System.IO;
using System.Threading;
using System.Windows.Forms;

namespace Generator {
    public partial class Form1 : Form {

        Chamber chamber = null;
        Thread thread = null;
        TextWriter logger;

        public Form1( ) {
            InitializeComponent( );
        }

        private void UpdateMapPresenter( ) {
            while (true) {
                Thread.Sleep(1000);
                if (chamber?.Walls != null) {
                }
            }
        }

        private bool tbMapSeed_Correct = true;

        private void tbMapSeed_TextChanged(object sender, EventArgs e) {
            TextBox tb = sender as TextBox;
            if (tb.Text.Length > 1 && tb.Text[0] == '0') {
                int startIndex = 0;
                for (; startIndex < tb.Text.Length; startIndex++) {
                    if (tb.Text[startIndex] != '0') {
                        tb.Text = tb.Text.Substring(startIndex, tb.Text.Length - startIndex);
                        break;
                    }
                }
                if (tb.Text.Length == 1) {
                    tb.SelectionStart = 1;
                }
            }
            int seed;
            tbMapSeed_Correct = int.TryParse(tb.Text, out seed) && seed >= 0;
            tb.BackColor = tbMapSeed_Correct ? Color.FromArgb(30, 30, 30) : Color.Red;
        }

        private void btnGenerate_Click(object sender, EventArgs e) {
            if (!tbMapSeed_Correct) {
                return;
            }
            if (thread != null) {
                thread.Abort( );
                logger.Dispose( );
            }
            Utils.RandomSetSeed(int.Parse(tbMapSeed.Text));
            chamber = new Chamber( );
            chamber.WallsCreated += ( ) => {
                Bitmap img = new Bitmap(256, 128);
                for (uint i = 0; i < chamber.Walls.Width; i++) {
                    for (uint j = 0; j < chamber.Walls.Height; j++) {
                        if (chamber.Walls[i, j]) {
                            for (uint x = 0; x < 256; x++) {
                                img.SetPixel((int)(i * 16 + x % 16), (int)(j * 16 + x / 16), Color.Red);
                            }
                        }
                    }
                }
                pbRoom.Image?.Dispose( );
                pbRoom.Image = img;
            };
            logger = Console.Out;
            (thread = new Thread(( ) => Chamber.Generate(chamber, 16, 8))).Start( );
        }
    }
}

using GravityLabChamberGenerator;
using System;
using System.Drawing;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Generator {
    public partial class Form1 : Form {

        Chamber chamber = null;

        public Form1( ) {
            InitializeComponent( );
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

        private async void btnGenerate_Click(object sender, EventArgs e) {
            if (!tbMapSeed_Correct) {
                return;
            }

            var progress = new Progress<Chamber.GeneratorEvent>((@event) => {
                switch (@event) {
                case Chamber.GeneratorEvent.ROOM_CREATED:
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
                    break;
                case Chamber.GeneratorEvent.WAY_FINDED:
                    lbWay.Items.Clear( );
                    foreach (var a in chamber.Way) {
                        lbWay.Items.Add(a.ToString( ));
                    }
                    break;
                }
            });

            chamber?.OnBreakGeneration( );

            Utils.RandomSetSeed(int.Parse(tbMapSeed.Text));
            chamber = new Chamber( );
            await Task.Factory.StartNew(( ) => {
                Chamber.GenerateInThread(chamber, 16, 8, null, progress);
            });
        }

        private void Form1_FormClosed(object sender, FormClosedEventArgs e) {
            chamber?.OnBreakGeneration( );
        }
    }
}

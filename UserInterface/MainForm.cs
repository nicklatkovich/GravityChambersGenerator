using GravityLabs._utils;
using System;
using System.Drawing;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GravityLabs.ChambersGenerator.UserInterface {
    public partial class MainForm : Form {

        private ChamberGeneratorAsync Generator = null;

        public MainForm( ) {
            InitializeComponent( );
        }

        private bool tbMapSeed_Correct = true;
        private int newGenerationSeed = 0;
        private int oldGenerationSeed = -1;

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
            tbMapSeed_Correct = int.TryParse(tb.Text, out newGenerationSeed) && newGenerationSeed >= 0;
            tb.BackColor = tbMapSeed_Correct ? Color.FromArgb(30, 30, 30) : Color.Red;
            ButtonSetEnable(btnGenerate, tbMapSeed_Correct && newGenerationSeed != oldGenerationSeed);
        }

        private void ClearResult( ) {
            pbRoom.Image?.Dispose( );
            lbWay.Items.Clear( );
            lblGenMapSeed.Text = "undefined";
        }

        private async void btnGenerate_Click(object sender, EventArgs e) {
            if (!tbMapSeed_Correct) {
                return;
            }

            oldGenerationSeed = newGenerationSeed;
            ButtonSetEnable(btnGenerate, false);
            var progress = new Progress<ChamberGenerator.Event>((@event) => {
                switch (@event) {
                case ChamberGenerator.Event.ROOM_CREATED:
                    Bitmap img = new Bitmap(256, 128);
                    for (uint i = 0; i < Generator.Chamber.Walls.Width; i++) {
                        for (uint j = 0; j < Generator.Chamber.Walls.Height; j++) {
                            if (Generator.Chamber.Walls[i, j]) {
                                for (uint x = 0; x < 256; x++) {
                                    img.SetPixel((int)(i * 16 + x % 16), (int)(j * 16 + x / 16), Color.Red);
                                }
                            }
                        }
                    }
                    pbRoom.Image?.Dispose( );
                    pbRoom.Image = img;
                    break;
                case ChamberGenerator.Event.WAY_FINDED:
                    lbWay.Items.Clear( );
                    foreach (var a in Generator.Way) {
                        lbWay.Items.Add(a.ToString( ));
                    }
                    break;
                }
            });

            Generator?.BreakGeneration( );
            ClearResult( );

            int seed = int.Parse(tbMapSeed.Text);
            lblGenMapSeed.Text = seed.ToString( );

            Utils.RandomSetSeed(seed);
            await Task.Factory.StartNew(( ) => {
                Generator = ChamberGeneratorAsync.CreateChamberInThread(16, 8, null, progress);
            });
        }

        private void ButtonSetEnable(Button btn, bool enable) {
            btn.Enabled = enable;
            btn.ForeColor = enable ? Color.White : Color.Black;

        }

        private void Form1_FormClosed(object sender, FormClosedEventArgs e) {
            Generator?.BreakGeneration( );
        }
    }
}

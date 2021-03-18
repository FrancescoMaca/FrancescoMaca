using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CryptoNS {
    public partial class Crypto_Help : Form {

        private const int borderThickness = 10;

        public Crypto_Help() {
            InitializeComponent();
        }

        private void picClose_Click(object sender, EventArgs e) {
            Close();
        }

        private void pnlTopBar_MouseDown(object sender, MouseEventArgs e) {
            if (e.Button == MouseButtons.Left) {
                CryptoUtil.ReleaseCapture();
                CryptoUtil.SendMessage(Handle, 0xA1, 0x2, 0);
            }
        }

        private void Crypto_Help_Paint(object sender, PaintEventArgs e) {
            CryptoUtil.drawBorder((Form)sender, borderThickness, Color.FromArgb(49, 52, 60));
        }
    }
}

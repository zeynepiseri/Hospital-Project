using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Hastane_Proje
{
    public partial class FrmGirisler : Form
    {
        public FrmGirisler()
        {
            InitializeComponent();
        }

        private void BtnHastaGiris_Click(object sender, EventArgs e)
        {
            FrmHastaGiris fr = new FrmHastaGiris();
            fr.Show();
            this.Hide();      // Bu formu gizleme ve ve fr listesinden türetilen nesneyi getirme
        }

        private void BtnDoktorGiris_Click_1(object sender, EventArgs e)
        {
            DoktorGiris fr = new DoktorGiris();
            fr.Show();
            this.Hide();      // Bu formu gizleme ve ve fr listesinden türetilen nesneyi getirme
        }

        private void BtnSekreterGiris_Click_1(object sender, EventArgs e)
        {
            FrmSekreterGiris fr = new FrmSekreterGiris();
            fr.Show();
            this.Hide();      // Bu formu gizleme ve ve fr listesinden türetilen nesneyi getirme
        }
    }
}

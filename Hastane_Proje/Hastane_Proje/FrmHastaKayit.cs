using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace Hastane_Proje
{
    public partial class FrmHastaKayit : Form
    {
        public FrmHastaKayit()
        {
            InitializeComponent();
        }
        SqlBaglantisi bgl = new SqlBaglantisi();
        private void BtnKayitYap_Click(object sender, EventArgs e)
        {
            SqlCommand komut = new SqlCommand("insert into Tbl_Hastalar (hastaad, hastasoyad, hastatc, hastatelefon,hastasifre, hastacinsiyet) values (@h1,@h2,@h3,@h4,@h5,@h6)", bgl.baglanti());
            komut.Parameters.AddWithValue("@h1", TxtAd.Text);
            komut.Parameters.AddWithValue("@h2", TxtSoyad.Text);
            komut.Parameters.AddWithValue("@h3", MskTC.Text);
            komut.Parameters.AddWithValue("@h4", MskTel.Text);
            komut.Parameters.AddWithValue("@h5", TxtSifre.Text);
            komut.Parameters.AddWithValue("@h6", CmbCinsiyet.Text);
            komut.ExecuteNonQuery();
            bgl.baglanti().Close();
            MessageBox.Show("Kaydınız Başarı ile Gerçekleşti. Şifreniz: " + TxtSifre.Text, "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }
}

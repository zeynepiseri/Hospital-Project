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
    public partial class FrmHastaBilgiDuzenle : Form
    {
        public FrmHastaBilgiDuzenle()
        {
            InitializeComponent();
        }
        SqlBaglantisi bgl = new SqlBaglantisi();

        

        public string TCno;
        private void FrmHastaBilgiDuzenle_Load(object sender, EventArgs e)
        {
            MskTC.Text = TCno;
            SqlCommand komut1 = new SqlCommand("select * from tbl_hastalar where hastatc=@h1", bgl.baglanti());
            komut1.Parameters.AddWithValue("@h1", MskTC.Text);
            SqlDataReader rd = komut1.ExecuteReader();
            while (rd.Read())
            {
                TxtAd.Text = rd[1].ToString();
                TxtSoyad.Text = rd[2].ToString();
                MskTel.Text = rd[4].ToString();
                TxtSifre.Text = rd[5].ToString();
                CmbCinsiyet.Text = rd[6].ToString();
            }
            bgl.baglanti().Close();
        }

        private void BtnBilgiGuncelle_Click(object sender, EventArgs e)
        {
            SqlCommand komut2 = new SqlCommand("update tbl_hastalar set hastaad=@d1,hastasoyad=@d2,hastatelefon=@d3,hastasifre=@d4,hastacinsiyet=@d5 where hastatc=@d6", bgl.baglanti());
            komut2.Parameters.AddWithValue("@d1", TxtAd.Text);
            komut2.Parameters.AddWithValue("@d2", TxtSoyad.Text);
            komut2.Parameters.AddWithValue("@d3", MskTel.Text);
            komut2.Parameters.AddWithValue("@d4", TxtSifre.Text);
            komut2.Parameters.AddWithValue("@d5", CmbCinsiyet.Text);
            komut2.Parameters.AddWithValue("@d6", MskTC.Text);
            komut2.ExecuteNonQuery();
            bgl.baglanti().Close();
            MessageBox.Show("Bilgileriniz Güncellendi. Giriş Ekranına Dönebilirsiniz.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }
    }
}

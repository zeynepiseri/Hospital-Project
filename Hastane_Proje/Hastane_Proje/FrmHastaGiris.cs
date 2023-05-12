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
    public partial class FrmHastaGiris : Form
    {
        public FrmHastaGiris()
        {
            InitializeComponent();
        }

        SqlBaglantisi bgl = new SqlBaglantisi();

        private void BtnGiris_Click(object sender, EventArgs e)
        {
            SqlCommand komut = new SqlCommand("select * from Tbl_Hastalar where hastatc=@h1 and hastasifre=@h2", bgl.baglanti());
            komut.Parameters.AddWithValue("@h1", Msktc.Text);
            komut.Parameters.AddWithValue("@h2", txtsifre.Text);

            SqlDataReader dr = komut.ExecuteReader();
            if (dr.Read())
            {
                FrmHasta_Detay fr = new FrmHasta_Detay();
                fr.Tc = Msktc.Text;
                fr.Show();
                this.Hide();
                bgl.baglanti().Close();

           }
            else
            {
                MessageBox.Show("Hatalı T.C Kimlik No veya Şifre");
            }
            bgl.baglanti().Close();
        }

    }
}

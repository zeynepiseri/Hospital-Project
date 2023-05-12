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
    public partial class FrmDoktorBilgiDuzenle : Form
    {
        public FrmDoktorBilgiDuzenle()
        {
            InitializeComponent();
        }
        SqlBaglantisi bgl = new SqlBaglantisi();
        public string TC;
        private void FrmDoktorBilgiDuzenle_Load(object sender, EventArgs e)
        {
            MskTC.Text = TC;

            SqlCommand cmd1 = new SqlCommand("select * from tbl_doktorlar where doktortc=@d1", bgl.baglanti());
            cmd1.Parameters.AddWithValue("@d1", MskTC.Text);
            SqlDataReader dr = cmd1.ExecuteReader();
            while (dr.Read())
            {
                TxtAd.Text = dr[1].ToString();
                TxtSoyad.Text = dr[2].ToString();
                CmbBrans.Text = dr[3].ToString();
                TxtSifre.Text = dr[5].ToString();
            }
            bgl.baglanti().Close();
        }

        private void BtBilgiGuncelle_Click(object sender, EventArgs e)
        {
            SqlCommand cmd2 = new SqlCommand("update tbl_doktorlar set doktorad=@d1, doktorsoyad=@d2, DoktorBrans=@d3, Doktorsıfre=@d4 where DoktorTC=@d5", bgl.baglanti());
            cmd2.Parameters.AddWithValue("@d1", TxtAd.Text);
            cmd2.Parameters.AddWithValue("@d2", TxtSoyad.Text);
            cmd2.Parameters.AddWithValue("@d3", CmbBrans.Text);
            cmd2.Parameters.AddWithValue("@d4", TxtSifre.Text);
            cmd2.Parameters.AddWithValue("@d5", MskTC.Text);
            cmd2.ExecuteNonQuery();
            bgl.baglanti().Close();
            MessageBox.Show("Bilgiler Güncellendi");
        }
    }
}

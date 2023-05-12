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
    public partial class FrmHasta_Detay : Form
    {
        public FrmHasta_Detay()
        {
            InitializeComponent();
        }
        public string Tc;

        SqlBaglantisi bgl = new SqlBaglantisi();
        private void FrmHasta_Detay_Load(object sender, EventArgs e)
        {
            LblTc.Text = Tc;

            // Ad Soyad Çekme

            SqlCommand komut = new SqlCommand("select hastaAd,hastasoyad from tbl_hastalar where hastatc=@h1", bgl.baglanti());
            komut.Parameters.AddWithValue("@h1", LblTc.Text);
            SqlDataReader dr = komut.ExecuteReader();

            while (dr.Read())
            {
                LblAdSoyad.Text = dr[0] + " " + dr[1];
            }
            bgl.baglanti().Close();

            // Randevu Geçmişi
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter("SELECT * FROM Tbl_Randevu WHERE hastatc= '" + LblTc.Text + "'", bgl.baglanti());
            da.Fill(dt);
            dataGridView1.DataSource = dt;

            //Branşları Çekme

            SqlCommand komut2 = new SqlCommand("select bransAd from Tbl_Branslar", bgl.baglanti());
            SqlDataReader dr2 = komut2.ExecuteReader();
            while (dr2.Read())
            {
                cmbBrans.Items.Add(dr2[0]);
            }
        }

        private void cmbBrans_SelectedIndexChanged(object sender, EventArgs e)
        {
            CmbDoktor.Items.Clear();

            SqlCommand komut3 = new SqlCommand("select doktorAd,doktorSoyad from tbl_doktorlar where doktorBrans=@d1", bgl.baglanti());
            komut3.Parameters.AddWithValue("@d1", cmbBrans.Text);
            SqlDataReader dr3 = komut3.ExecuteReader();
            while (dr3.Read())
            {
                CmbDoktor.Items.Add(dr3[0] + " " + dr3[1]);
            }
            bgl.baglanti().Close();
        }

        private void CmbDoktor_SelectedIndexChanged(object sender, EventArgs e)
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter("select * from tbl_randevu where randevubrans='" + cmbBrans.Text + "'" + "and randevudoktor='" + CmbDoktor.Text + "'", bgl.baglanti());
            da.Fill(dt);
            dataGridView2.DataSource = dt;
        }

        private void LnkBilgiDuzenle_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            FrmHastaBilgiDuzenle fr = new FrmHastaBilgiDuzenle();
            fr.TCno = LblTc.Text;
            fr.Show();
        }

        private void dataGridView2_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int secilen = dataGridView2.SelectedCells[0].RowIndex;
            TxtId.Text = dataGridView2.Rows[secilen].Cells[0].Value.ToString();
        }

        private void BtnRandevu_Click(object sender, EventArgs e)
        {
            SqlCommand komut4 = new SqlCommand("update tbl_randevu set randevudurum=1, hastaTC=@r1, hastasikayet=@r2 where randevuId=@r3", bgl.baglanti());
            komut4.Parameters.AddWithValue("@r1", LblTc.Text);
            komut4.Parameters.AddWithValue("@r2", RchSikayet.Text);
            komut4.Parameters.AddWithValue("@r3", TxtId.Text);
            komut4.ExecuteNonQuery();
            bgl.baglanti().Close();
            MessageBox.Show("Randevu Alındı", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }

}

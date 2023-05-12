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
    public partial class FrmDoktorPaneli : Form
    {
        public FrmDoktorPaneli()
        {
            InitializeComponent();
        }
        SqlBaglantisi bgl = new SqlBaglantisi();
        private void FrmDoktorPaneli_Load(object sender, EventArgs e)
        {
            DataTable dt1 = new DataTable();
            SqlDataAdapter da1 = new SqlDataAdapter("select * from tbl_doktorlar", bgl.baglanti());
            da1.Fill(dt1);
            dataGridView1.DataSource = dt1;

            // Branşları comboboxa ekleme

            SqlCommand komut1 = new SqlCommand("select  BransAd from Tbl_Branslar", bgl.baglanti());
            SqlDataReader rd2 = komut1.ExecuteReader();
            while (rd2.Read())
            {
                CmbBrans.Items.Add(rd2[0]);
            }
            bgl.baglanti().Close();
        }

        private void BtnEkle_Click(object sender, EventArgs e)
        {
            SqlCommand cmd1 = new SqlCommand("insert into tbl_doktorlar (doktorad, doktorsoyad, doktorbrans, doktortc, doktorsıfre) values (@d1,@d2, @d3, @d4, @d5)", bgl.baglanti());
            cmd1.Parameters.AddWithValue("@d1", TxtAd.Text);
            cmd1.Parameters.AddWithValue("@d2", TxtSoyad.Text);
            cmd1.Parameters.AddWithValue("@d3", CmbBrans.Text);
            cmd1.Parameters.AddWithValue("@d4", MskTC.Text);
            cmd1.Parameters.AddWithValue("@d5", TxtSifre.Text);
            cmd1.ExecuteNonQuery();
            bgl.baglanti().Close();
            MessageBox.Show("Doktor Eklendi.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int secilen = dataGridView1.SelectedCells[0].RowIndex;
            TxtAd.Text = dataGridView1.Rows[secilen].Cells[1].Value.ToString();
            TxtSoyad.Text = dataGridView1.Rows[secilen].Cells[2].Value.ToString();
            CmbBrans.Text = dataGridView1.Rows[secilen].Cells[3].Value.ToString();
            MskTC.Text = dataGridView1.Rows[secilen].Cells[4].Value.ToString();
            TxtSifre.Text = dataGridView1.Rows[secilen].Cells[5].Value.ToString();
        }

        private void BtnSil_Click(object sender, EventArgs e)
        {
            SqlCommand cmd2 = new SqlCommand("delete from tbl_doktorlar where doktortc=@d1", bgl.baglanti());
            cmd2.Parameters.AddWithValue("d1", MskTC.Text);
            cmd2.ExecuteNonQuery();
            bgl.baglanti().Close();
            MessageBox.Show("Kayıt Silindi.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }

        private void BtnGuncelle_Click(object sender, EventArgs e)
        {
            SqlCommand cmd3 = new SqlCommand("Update tbl_doktorlar set doktorad=@d1, doktorsoyad=@d2,doktorbrans=@d3, doktorsıfre=@d5 where doktortc=@d4", bgl.baglanti());
            cmd3.Parameters.AddWithValue("@d1", TxtAd.Text);
            cmd3.Parameters.AddWithValue("@d2", TxtSoyad.Text);
            cmd3.Parameters.AddWithValue("@d3", CmbBrans.Text);
            cmd3.Parameters.AddWithValue("@d4", MskTC.Text);
            cmd3.Parameters.AddWithValue("@d5", TxtSifre.Text);
            cmd3.ExecuteNonQuery();
            bgl.baglanti().Close();
            MessageBox.Show("Doktor Güncellendi.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            TxtAd.Clear();
            TxtSoyad.Clear();
            CmbBrans.SelectedIndex = -1;
            MskTC.Clear();
            TxtSifre.Clear();
        }
    }
}

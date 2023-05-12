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
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace Hastane_Proje
{
    public partial class FrmSekreterDetay : Form
    {
        public FrmSekreterDetay()
        {
            InitializeComponent();
        }
        public string TCnumara;

        SqlBaglantisi bgl = new SqlBaglantisi();

        private void FrmSekreterDetay_Load(object sender, EventArgs e)
        {
            lblTC.Text = TCnumara;

            // Ad Soyad

            SqlCommand cmd1 = new SqlCommand("select sekreteradsoyad from tbl_sekreterler where sekretertc=@s1", bgl.baglanti());
            cmd1.Parameters.AddWithValue("@s1", lblTC.Text);
            SqlDataReader rd = cmd1.ExecuteReader();
            if (rd.Read())
            {
                lblAdSoyad.Text = rd[0].ToString();
            }
            bgl.baglanti().Close();

            // Branşları datagrid e aktarma

            DataTable dt1 = new DataTable();
            SqlDataAdapter da1 = new SqlDataAdapter("select * from tbl_branslar", bgl.baglanti());
            da1.Fill(dt1);
            dataGridView1.DataSource = dt1;

            // Doktorları datagrid e aktarma

            DataTable dt2 = new DataTable();
            SqlDataAdapter da2 = new SqlDataAdapter("select (doktorad + ' ' + doktorsoyad) as 'Doktorlar',DoktorBrans as 'Branşlar' from tbl_doktorlar", bgl.baglanti());
            da2.Fill(dt2);
            dataGridView2.DataSource = dt2;


            // Branşı combobox a aktarma

            SqlCommand komut1 = new SqlCommand("select  BransAd from Tbl_Branslar", bgl.baglanti());
            SqlDataReader rd2 = komut1.ExecuteReader();
            while (rd2.Read())
            {
                CmbBrans.Items.Add(rd2[0]);
            }
            bgl.baglanti().Close();

        }


        private void BtnKaydet_Click(object sender, EventArgs e)
        {
            SqlCommand komutkaydet = new SqlCommand("insert into tbl_randevu (RandevuTarih, Randevusaat, Randevubrans, Randevudoktor) values (@r1,@r2,@r3,@r4)", bgl.baglanti());
            komutkaydet.Parameters.AddWithValue("@r1", MskTarih.Text);
            komutkaydet.Parameters.AddWithValue("@r2", MskSaat.Text);
            komutkaydet.Parameters.AddWithValue("@r3", CmbBrans.Text);
            komutkaydet.Parameters.AddWithValue("@r4", CmbDoktor.Text);
            komutkaydet.ExecuteNonQuery();

            bgl.baglanti().Close();

            MessageBox.Show("Randevu Oluşturuldu.");
        }

        private void CmbDoktor_SelectedIndexChanged(object sender, EventArgs e)
        {
            CmbDoktor.Items.Clear();

            SqlCommand komdok = new SqlCommand("select doktorad, doktorsoyad from tbl_doktorlar where doktorbrans=@d1", bgl.baglanti());
            komdok.Parameters.AddWithValue("@d1", CmbBrans.Text);
            SqlDataReader rd4 = komdok.ExecuteReader();
            while (rd4.Read())
            {
                CmbDoktor.Items.Add(rd4[0] + " " + rd4[1]);
            }
            bgl.baglanti().Close();
        }

        private void BtnOluştur_Click(object sender, EventArgs e)
        {
            SqlCommand komduy = new SqlCommand("insert into tbl_Duyurular (duyuru) values (@d1)", bgl.baglanti());
            komduy.Parameters.AddWithValue("@d1", RchDuyuru.Text);
            komduy.ExecuteNonQuery();
            bgl.baglanti().Close();
            MessageBox.Show("Duyuru Oluşturuldu.");
        }

        private void BtnDrPanel_Click(object sender, EventArgs e)
        {
            FrmDoktorPaneli frp = new FrmDoktorPaneli();
            frp.Show();
        }

        private void BtnBransPnel_Click(object sender, EventArgs e)
        {
            FrmBrans frb = new FrmBrans();
            frb.Show();
        }

        private void BtnRandevuListe_Click(object sender, EventArgs e)
        {
            FrmRandevuListesi frr = new FrmRandevuListesi();
            frr.Show();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            FrmDuyurular frd = new FrmDuyurular();
            frd.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            MskTarih.Clear();
            MskSaat.Clear();
            CmbBrans.SelectedIndex = -1;
            CmbDoktor.SelectedIndex = -1;
            MskTC.Clear();
        }
    }
}

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
    public partial class FrmSekreterGiris : Form
    {
        public FrmSekreterGiris()
        {
            InitializeComponent();
        }
        SqlBaglantisi bgl = new SqlBaglantisi();
        private void btngiris_Click(object sender, EventArgs e)
        {
            SqlCommand cmd = new SqlCommand("select * from tbl_sekreterler where sekretertc=@s1 and sekretersıfre=@s2", bgl.baglanti());
            cmd.Parameters.AddWithValue("@s1", Msktc.Text);
            cmd.Parameters.AddWithValue("@s2", txtsifre.Text);
            SqlDataReader rd = cmd.ExecuteReader();
            if (rd.Read())
            {
                FrmSekreterDetay frs = new FrmSekreterDetay();
                frs.TCnumara = Msktc.Text;
                frs.Show();
                this.Hide();
            }
            else
            {
                MessageBox.Show("Hatalı TC No veya şifre");
            }
            bgl.baglanti().Close();
        }
    }
}

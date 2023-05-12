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
    public partial class DoktorGiris : Form
    {
        public DoktorGiris()
        {
            InitializeComponent();
        }
        SqlBaglantisi bgl = new SqlBaglantisi();
        private void btngiris_Click(object sender, EventArgs e)
        {
            SqlCommand komut1 = new SqlCommand("select * from tbl_doktorlar where doktortc=@d1 and doktorsıfre=@d2", bgl.baglanti());
            komut1.Parameters.AddWithValue("@d1", Msktc.Text);
            komut1.Parameters.AddWithValue("@d2", txtsifre.Text);
            SqlDataReader dr = komut1.ExecuteReader();
            if (dr.Read())
            {
                FrmDoktorDetay frd = new FrmDoktorDetay();
                frd.TC = Msktc.Text;
                frd.Show();
                this.Hide();
            }
            else
            {
                MessageBox.Show("Hatalı Şifre veya TC");
            }
            bgl.baglanti().Close();
        }
    }
}

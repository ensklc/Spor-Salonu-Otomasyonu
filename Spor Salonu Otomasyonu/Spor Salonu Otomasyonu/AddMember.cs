using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;//Databese kütüphanesi

namespace Spor_Salonu_Otomasyonu
{
    public partial class AddMember : Form
    {
        public AddMember()
        {
            InitializeComponent();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }
        SqlConnection baglanti = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\tchn\Documents\FitnessProje.mdf;Integrated Security=True;Connect Timeout=30");
        private void Btn_add_Click(object sender, EventArgs e)
        {
            if(Txt_age.Text=="" || Txt_mountlyAmount.Text=="" || Txt_nameSurname.Text=="" || Txt_number.Text == "")
            {
                MessageBox.Show("Eksik bilgi girilmiştir.!");
            }
            else
            {
                baglanti.Open();
                SqlCommand komut = new SqlCommand("insert into Tbl_Member (AdSoyad,Telefon,Cinsiyet,Yas,Ödeme,Zamanlama) values (@p1,@p2,@p3,@p4,@p5,@p6)", baglanti);
                komut.Parameters.AddWithValue("@p1", Txt_nameSurname.Text);
                komut.Parameters.AddWithValue("@p2", Txt_number.Text);
                komut.Parameters.AddWithValue("@p3", Cmb_gender.Text);
                komut.Parameters.AddWithValue("@p4", Txt_age.Text);
                komut.Parameters.AddWithValue("@p5", Txt_mountlyAmount.Text);
                komut.Parameters.AddWithValue("@p6", Cmb_timing.Text);
                komut.ExecuteNonQuery();
                baglanti.Close();
                MessageBox.Show("Kayıt başarıyla gerçekleşmiştir.");
                Txt_nameSurname.Text = "";
                Txt_number.Text = "";
                Cmb_gender.Text = "";
                Txt_age.Text = "";
                Txt_mountlyAmount.Text = "";
                Cmb_timing.Text = "";
            }
        }
        //Bilgileri kaydetmeden silecek.
        private void Btn_reset_Click(object sender, EventArgs e)
        {
            Txt_nameSurname.Text = "";
            Txt_number.Text = "";
            Cmb_gender.Text = "";
            Txt_age.Text = "";
            Txt_mountlyAmount.Text = "";
            Cmb_timing.Text = "";

        }

        private void label10_Click(object sender, EventArgs e)
        {
            Application.Exit(); // Formdan çıkış yapmak için
        }

        private void Btn_back_Click(object sender, EventArgs e)
        {
            Login log = new Login(); //Gitmek istediğimiz form ismiden (class isminden) nesne türetiyoruz ve işlemleri gerçekleştiriyoruz.
            log.Show();
            this.Hide();
        }

        private void Btn_back1_Click(object sender, EventArgs e)
        {
            HomePage homep = new HomePage();
            homep.Show();
            this.Hide();
        }
    }
}

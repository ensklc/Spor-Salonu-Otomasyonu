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

namespace Spor_Salonu_Otomasyonu
{
    public partial class Payment : Form
    {
        public Payment()
        {
            InitializeComponent();
        }
        SqlConnection baglanti = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\tchn\Documents\FitnessProje.mdf;Integrated Security=True;Connect Timeout=30");

        // Ödemeyi isme göre yapacağız. Aşağıya gerekli fonksiyonumuzu yazıyoruz.
        private void FillName()
        {
            baglanti.Open();
            SqlCommand komut = new SqlCommand("select AdSoyad from Tbl_Member", baglanti);
            SqlDataReader rdr;
            rdr = komut.ExecuteReader();
            // DataTable nesnesi, veri tabanından veri çekmek için kullanılan nesnedir.
            DataTable dt = new DataTable();
            dt.Columns.Add("AdSoyad", typeof(string));
            dt.Load(rdr);
            CmbAd_soyad.ValueMember = "AdSoyad"; //Değerleri gelsin.
            CmbAd_soyad.DataSource = dt;
            baglanti.Close();

        }
        //Filtrelediğimiz adın data grid view'de görünmesini sağlayacak.
        private void adFiltrele()
        {
            string query = "select *from Tbl_Payment where PUye='"+Txt_ara.Text+"'";
            //SqlDataAdapter nesnesini kullanmak için select sorgusuna ihtiyaç vardır.
            SqlDataAdapter sda = new SqlDataAdapter(query, baglanti); //Komutumuzu baglanti ile ilişkilendirdik.
            SqlCommandBuilder builder = new SqlCommandBuilder();
            DataSet ds = new DataSet();
            sda.Fill(ds);// verilerimizi sqldataAdapter yardımıyla dataset'e dolduruyoruz.
            dgw_ödeme.DataSource = ds.Tables[0];
            baglanti.Close();
        }
        //Payment veri tabanı tablosundaki verileri data grid view'e getiriyoruz.
        private void uye()
        {
            string query = "select *from Tbl_Payment";
            //SqlDataAdapter nesnesini kullanmak için select sorgusuna ihtiyaç vardır.
            SqlDataAdapter sda = new SqlDataAdapter(query, baglanti); //Komutumuzu baglanti ile ilişkilendirdik.
            SqlCommandBuilder builder = new SqlCommandBuilder();
            DataSet ds = new DataSet();
            sda.Fill(ds);// verilerimizi sqldataAdapter yardımıyla dataset'e dolduruyoruz.
            dgw_ödeme.DataSource = ds.Tables[0];
            baglanti.Close();
        }

        private void Payment_Load(object sender, EventArgs e)
        {
            FillName();
            uye();
        }

        // ödeme işlemini gerçekleştirip Tbl_Payment tablomuza verimizi yerleştiriyoruz.
        private void button5_Click(object sender, EventArgs e)
        {
            if(CmbAd_soyad.Text=="" || textBox1.Text == "")
            {
                MessageBox.Show("Eksik Bilgi!");
            }
            else
            {
                string odemePeriyot = periyot.Value.Month.ToString() + periyot.Value.Year.ToString();
                baglanti.Open();
                SqlDataAdapter sda = new SqlDataAdapter("select count(*) from Tbl_Payment where PUye='" + CmbAd_soyad.SelectedValue.ToString() + "' and PAy='" + odemePeriyot + "'", baglanti);
                DataTable dt = new DataTable();
                sda.Fill(dt);//veritabanından çektiğimiz verilerle dolduruyoruz.
                //Bir ayda iki kere ödeme yapılması engellemk için aşağıdaki if'i kullanıyoruz.
                if (dt.Rows[0][0].ToString() == "1")
                {
                    MessageBox.Show("Zaten ödeme yapılmış!");
                }
                else
                {
                    //sorgumuzu yapıyoruz.
                    string query="insert into Tbl_Payment values('"+odemePeriyot+"','"+CmbAd_soyad.SelectedValue.ToString()+"',"+textBox1.Text+")";
                    //komutumuzu yazıyoruz.
                    SqlCommand komut = new SqlCommand(query, baglanti);
                    komut.ExecuteNonQuery();
                    MessageBox.Show("Ödeme işlemi başarıyla gerçekleşti.");
                }
                baglanti.Close();
                uye();
            }
        }
        //Ana sayfaya gitme
        private void button_Click(object sender, EventArgs e)
        {
            HomePage homep = new HomePage();
            homep.Show();
            this.Hide();
        }
        //AdSoyad yazılı textbox ile ödemenin yazılı olduğu textbox'ı temizleme 
        private void button3_Click(object sender, EventArgs e)
        {
            CmbAd_soyad.Text = "";
            textBox1.Text = "";
        }

        private void dgw_ödeme_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void label6_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
        //Tabloda isim ile arama yapmamızı sağlıyor.
        private void button6_Click(object sender, EventArgs e)
        {
            adFiltrele();
            Txt_ara.Text = "";
        }
        //Arama yaptıktan sonra tablonun refresh yapılmasını sağlıyor.
        private void button7_Click(object sender, EventArgs e)
        {
            uye();
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {

        }
    }
}

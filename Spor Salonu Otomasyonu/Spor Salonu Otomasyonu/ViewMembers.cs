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
    public partial class ViewMembers : Form
    {
        public ViewMembers()
        {
            InitializeComponent();
        }

        private void button4_Click(object sender, EventArgs e)
        {

        }
        SqlConnection baglanti = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\tchn\Documents\FitnessProje.mdf;Integrated Security=True;Connect Timeout=30");

        private void uye()
        {
            string query = "select *from Tbl_Member";
            //SqlDataAdapter nesnesini kullanmak için select sorgusuna ihtiyaç vardır.
            SqlDataAdapter sda = new SqlDataAdapter(query,baglanti); //Komutumuzu baglanti ile ilişkilendirdik.
            SqlCommandBuilder builder = new SqlCommandBuilder();
            DataSet ds = new DataSet();
            sda.Fill(ds);// verilerimizi sqldataAdapter yardımıyla dataset'e dolduruyoruz.
            ViewMember_data.DataSource = ds.Tables[0];
            baglanti.Close();
        }
        private void label8_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void ViewMembers_Load(object sender, EventArgs e)
        {
            uye();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Login log = new Login();
            log.Show();
            this.Hide();
        }
        private void adFiltrele()
        {
            string query = "select *from Tbl_Member where AdSoyad='" + Txt_searchName.Text + "'";
            //SqlDataAdapter nesnesini kullanmak için select sorgusuna ihtiyaç vardır.
            SqlDataAdapter sda = new SqlDataAdapter(query, baglanti); //Komutumuzu baglanti ile ilişkilendirdik.
            SqlCommandBuilder builder = new SqlCommandBuilder();
            DataSet ds = new DataSet();
            sda.Fill(ds);// verilerimizi sqldataAdapter yardımıyla dataset'e dolduruyoruz.
            ViewMember_data.DataSource = ds.Tables[0];
            baglanti.Close();
        }
        //butona basmamızla hangi adı arattıysak veri kaynağımızda o görüntülenecek.
        private void button1_Click(object sender, EventArgs e)
        {
            adFiltrele();
            Txt_searchName.Text = "";
        }
        //Üyeleri tekrardan topluca listeletecek.
        private void button4_Click_1(object sender, EventArgs e)
        {
            uye();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            HomePage homep = new HomePage();
            homep.Show();
            this.Hide();
        }
    }
}
//datasource=veri kaynağı
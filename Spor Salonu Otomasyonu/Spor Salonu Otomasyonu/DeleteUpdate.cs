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
    public partial class DeleteUpdate : Form
    {
        public DeleteUpdate()
        {
            InitializeComponent();
        }

        private void guna2DataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void label10_Click(object sender, EventArgs e)
        {

        }

        private void label7_Click(object sender, EventArgs e)
        {

        }
        SqlConnection baglanti = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\tchn\Documents\FitnessProje.mdf;Integrated Security=True;Connect Timeout=30");

        private void uye()
        {
            string query = "select *from Tbl_Member";
            //SqlDataAdapter nesnesini kullanmak için select sorgusuna ihtiyaç vardır.
            SqlDataAdapter sda = new SqlDataAdapter(query, baglanti); //Komutumuzu baglanti ile ilişkilendirdik.
            SqlCommandBuilder builder = new SqlCommandBuilder();
            DataSet ds = new DataSet();
            sda.Fill(ds);// verilerimizi sqldataAdapter yardımıyla dataset'e dolduruyoruz.
            ViewMember_data.DataSource = ds.Tables[0];
            baglanti.Close();
        }
        private void DeleteUpdate_Load(object sender, EventArgs e)
        {
            uye(); //Form sayfası açıldığında uye() fonksiyonunu çağırsın.
        }
        int key;//int değerimizin dönüştürmesini yapıyoruz.
        private void ViewMember_data_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            key = Convert.ToInt32(ViewMember_data.SelectedRows[0].Cells[0].Value.ToString());// key değerini id yaptık burada
            //Datagrid'e gelen bilgileri textboxlara yazdırıyoruz.
            Txt_name.Text = ViewMember_data.SelectedRows[0].Cells[1].Value.ToString();//sıfırıncı sıra birinci hücredeki değeri Txt_name.text'e yaz.
            Txt_number.Text = ViewMember_data.SelectedRows[0].Cells[2].Value.ToString();
            Cmb_gender.Text = ViewMember_data.SelectedRows[0].Cells[3].Value.ToString();
            Txt_age.Text = ViewMember_data.SelectedRows[0].Cells[4].Value.ToString();
            Txt_mountlyAmount.Text = ViewMember_data.SelectedRows[0].Cells[5].Value.ToString();
            Cmb_timing.Text = ViewMember_data.SelectedRows[0].Cells[6].Value.ToString();

        }
        //Reset
        private void button4_Click(object sender, EventArgs e)
        {
            Txt_name.Text = "";
            Txt_number.Text = "";
            Cmb_gender.Text = "";
            Txt_age.Text = "";
            Txt_mountlyAmount.Text = "";
            Cmb_timing.Text = "";
        }
        //Back
        private void button3_Click(object sender, EventArgs e)
        {
            HomePage homep = new HomePage();
            homep.Show();
            this.Hide();
        }
        //Delete
        private void button2_Click(object sender, EventArgs e)
        {
            if (key == 0) //Eğer id 0 ise yani siilinecek üye seçilmemişse
            {
                MessageBox.Show("Lütfen silinecek üyeyi seçiniz!");
            }
            else
            {
                try
                {
                    baglanti.Open();
                    string query = "delete from Tbl_Member where Id="+key+";";// Tabloda, id ye göre silme işlemini yap.id=key deki değer
                    SqlCommand komut = new SqlCommand(query, baglanti);
                    komut.ExecuteNonQuery();
                    MessageBox.Show("Seçilen üye başarıyla silinmiştir.");
                    baglanti.Close();
                    uye(); //uye() fonk. tekrar çağırarak son halini görüntülüyoruz.
                }catch(Exception Ex)
                {
                    MessageBox.Show(Ex.Message);
                }
            }
        }
        //Update
        private void button1_Click(object sender, EventArgs e)
        {
            //Aşağıdakilerin herhangi birinin text yeri boşsa
            if (key == 0 || Txt_name.Text== "" || Txt_number.Text=="" || Cmb_gender.Text=="" || Txt_age.Text=="" || Txt_mountlyAmount.Text=="" ||Cmb_timing.Text=="") 
            {
                MessageBox.Show("Eksik Bilgi!");
            }
            else
            {
                try
                {
                    baglanti.Open();
                    string query = "update Tbl_Member set AdSoyad='"+Txt_name.Text+"',Telefon='"+Txt_number.Text+"',Cinsiyet='"+Cmb_gender.Text+"',Yas='"+Txt_age.Text+"',Ödeme='"+Txt_mountlyAmount.Text+"',Zamanlama='"+Cmb_timing.Text+"'where Id="+key+";";
                    
                    SqlCommand komut = new SqlCommand(query, baglanti);
                    komut.ExecuteNonQuery();
                    MessageBox.Show("Seçilen üye başarıyla güncellenmiştir.");
                    baglanti.Close();
                    uye(); //uye() fonk. tekrar çağırarak son halini görüntülüyoruz.
                }
                catch (Exception Ex)
                {
                    MessageBox.Show(Ex.Message);
                }
            }
        }

        private void label10_Click_1(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}

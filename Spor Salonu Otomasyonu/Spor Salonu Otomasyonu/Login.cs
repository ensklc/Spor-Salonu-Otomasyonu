using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Spor_Salonu_Otomasyonu
{
    public partial class Login : Form
    {
        public Login()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Txt_member.Text = "";
            Txt_password.Text = "";
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if(Txt_member.Text=="" || Txt_password.Text == "")
            {
                MessageBox.Show("Eksik Bilgi Girildi!");
            }
            else if(Txt_member.Text=="Admin" && Txt_password.Text == "123456")
            {
                HomePage homep = new HomePage();
                homep.Show();
                this.Hide();
            }
            else
            {
                MessageBox.Show("Hatalı Kullanıcı Adı veya Şifre!");
            }
        }
    }
}

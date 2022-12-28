using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Crossword
{
    public partial class Login : Form
    {
        public Login()
        {
            InitializeComponent();
        }
        AccessC.ConectThisAccessWorker con = new AccessC.ConectThisAccessWorker();
        public Label label3 { get; set; }
        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text == "")
            {
                MessageBox.Show("Поля не могут быть пустыми");
            }
            else 
            { 
                con.OpenConnectic();
                string Login;
                string query = "SELECT Password FROM UsersData WHERE Login LIKE \""+ textBox1.Text + "\";";
                string password = con.Select(query).Trim();
                if (textBox2.Text == password)
                {
                    Login = textBox1.Text;
                    label3.Text = "";
                    label3.Text += "Пользователь: " + Login;
                    MessageBox.Show("Добро пожаловать, " + Login +"!");
                }
                else
                {
                    MessageBox.Show("Неверный логин или пароль");
                }
                con.CloseConnect();
                Close();
            }
            
        }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using AccessC;

namespace Crossword
{
    public partial class Login : Form
    {
        public Login()
        {
            InitializeComponent();
        }
        ConectThisAccessWorker con = new ConectThisAccessWorker();
        public Label label3;
        public Label label4;
        public string login;
        public int id_game;
        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text == "")
            {
                MessageBox.Show("Поля не могут быть пустыми");
            }
            else 
            { 
                con.OpenConnectic();
                string query = "SELECT Password FROM UsersData WHERE Login LIKE \""+ textBox1.Text + "\";";
                string password = con.Select(query).Trim();
                if (textBox2.Text == password)
                {
                    login = textBox1.Text;
                    label3.Text = login;
                    MessageBox.Show("Добро пожаловать, " + login + "!");
                    con.InsertAndDeleteAndUpdate("INSERT INTO TopPlayers (`Login`) VALUES (\""+login+"\");");
                    label4.Text = con.Select("SELECT MAX(id_game) FROM TopPlayers;");
                    Close();
                }
                else
                {
                    MessageBox.Show("Неверный логин или пароль");
                }
                con.CloseConnect();
            }
        }
        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (textBox2.UseSystemPasswordChar == true)
                textBox2.UseSystemPasswordChar = false;
            else textBox2.UseSystemPasswordChar = true;
        }
        private void Login_Load(object sender, EventArgs e)
        {
            textBox2.UseSystemPasswordChar = true;
        }
    }
}

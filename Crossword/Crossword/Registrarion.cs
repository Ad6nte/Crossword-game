using System;
using System.Windows.Forms;
using AccessC;

namespace Crossword
{
    public partial class Registrarion : Form
    {
        public Registrarion()
        {
            InitializeComponent();
        }
        readonly ConectThisAccessWorker con = new ConectThisAccessWorker();
        private void button1_Click(object sender, EventArgs e)
        {
            if(textBox1.Text == "")
            {
                MessageBox.Show("Поля не могут быть пустыми");
            }
            else
            {
                con.OpenConnectic();
                string query = "INSERT INTO UsersData (`Login`, `Password`) VALUES (\"";
                query += textBox1.Text + "\",\"" +textBox2.Text + "\");";
                try
                {
                    con.InsertAndDeleteAndUpdate(query);
                    MessageBox.Show("Регистрация прошла успешно!");
                    Close();
                }
                catch 
                {
                    MessageBox.Show("Пользователь с таким логином уже зарегистрирован!");
                }
                con.CloseConnect();
            }
        }
    }
}

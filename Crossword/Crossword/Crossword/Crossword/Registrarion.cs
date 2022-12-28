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
    public partial class Registrarion : Form
    {
        public Registrarion()
        {
            InitializeComponent();
        }
        AccessC.ConectThisAccessWorker con = new AccessC.ConectThisAccessWorker();
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
                }
                catch 
                {
                    MessageBox.Show("Пользователь с таким логином уже зарегистрирован!");
                }
                con.CloseConnect();
                Close();

            }
        }
    }
}

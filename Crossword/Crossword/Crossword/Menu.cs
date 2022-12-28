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
    public partial class Menu : Form
    {
        public Menu()
        {
            InitializeComponent();
        }
        public string login;
        public int id_game;
        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Login log = new Login
            {
                label3 = this.label1,
                login = this.login,
                label4 = this.label3
            };
            log.Show();
        }
        private void linkLabel2_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Registrarion reg = new Registrarion();
            reg.Show();
        }
        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                Cross1 cr = new Cross1
                {
                    X = 15,
                    Y = 18,
                    login = label1.Text,
                    id_game = Convert.ToInt32(label3.Text),
                    VremyaProhozhdenia = "05:00",
                    level = "Level1"
                };
                cr.Show();
            }
            catch (Exception er)
            {
                MessageBox.Show("Вы не авторизованы! Необходимо авторизоваться!");
                MessageBox.Show(er.ToString());
            }
        }
        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                Cross1 cr = new Cross1
                {
                    X = 20,
                    Y = 22,
                    login = label1.Text,
                    id_game = Convert.ToInt32(label3.Text),
                    VremyaProhozhdenia = "07:00",
                    level = "Level2"
                };
                cr.Show();
            }
            catch
            {
                MessageBox.Show("Вы не авторизованы! Необходимо авторизоваться!");
            }
        }
        private void button3_Click(object sender, EventArgs e)
        {

            try
            {
                Cross1 cr = new Cross1
                {
                    X = 20,
                    Y = 27,
                    login = label1.Text,
                    id_game = Convert.ToInt32(label3.Text),
                    VremyaProhozhdenia = "09:59",
                    level = "Level3"
                };
                cr.Show();
            }
            catch
            {
                MessageBox.Show("Вы не авторизованы! Необходимо авторизоваться!");
            }
        }
        public void UpdateTop()
        {
            ConectThisAccessWorker con = new ConectThisAccessWorker();
            con.OpenConnectic();
            dataGridView1.DataSource = con.FillDGV("Select TOP 5 Login as Логин,MINUTE(level1) as минуты,SECOND(level1) as секунды from TopPlayers WHERE Level1 IS NOT NULL order by MINUTE(level1) asc, SECOND(level1) asc;");
            if(dataGridView1.ColumnCount == 4)
            {
                dataGridView1.Columns.Remove(dataGridView1.Columns[0]);
            }
            DataGridViewTextBoxColumn dtgc = new DataGridViewTextBoxColumn()
            {
                HeaderText = "Время",
                DisplayIndex = 3
            };
            dataGridView1.Columns.Insert(3, dtgc);
            for (int i = 0; i < dataGridView1.RowCount; i++)
            {
                if (Convert.ToInt32(dataGridView1.Rows[i].Cells[2].Value) < 10)
                {
                    dataGridView1.Rows[i].Cells[3].Value = dataGridView1.Rows[i].Cells[1].Value.ToString() + ":0" + dataGridView1.Rows[i].Cells[2].Value.ToString();
                }
                else
                    dataGridView1.Rows[i].Cells[3].Value = dataGridView1.Rows[i].Cells[1].Value.ToString() + ":" + dataGridView1.Rows[i].Cells[2].Value.ToString();
            }
            dataGridView1.Columns[0].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dataGridView1.Columns[3].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dataGridView1.Columns[1].Visible = false;
            dataGridView1.Columns[2].Visible = false;
            dataGridView1.Columns[0].ReadOnly = true;
            dataGridView1.Columns[3].ReadOnly = true;
            for (int i = 0; i < dataGridView1.RowCount - 1; i++)
            {
                if (dataGridView1.Rows[i].Cells[1].Value.ToString() == "")
                {
                    dataGridView1.Rows.RemoveAt(i);
                    i--;
                }
            }
            dataGridView2.DataSource = con.FillDGV("Select TOP 5 Login as Логин,MINUTE(level2) as минуты,SECOND(level2) as секунды from TopPlayers WHERE Level2 IS NOT NULL order by MINUTE(level2) asc, SECOND(level2) asc;");
            if (dataGridView2.ColumnCount == 4)
            {
                dataGridView2.Columns.Remove(dataGridView2.Columns[0]);
            }
            dataGridView2.Columns.Add("3", "Время");
            for (int i = 0; i < dataGridView2.RowCount; i++)
            {
                if (Convert.ToInt32(dataGridView2.Rows[i].Cells[2].Value) < 10)
                {
                    dataGridView2.Rows[i].Cells[3].Value = dataGridView2.Rows[i].Cells[1].Value.ToString() + ":0" + dataGridView2.Rows[i].Cells[2].Value.ToString();
                }
                else
                    dataGridView2.Rows[i].Cells[3].Value = dataGridView2.Rows[i].Cells[1].Value.ToString() + ":" + dataGridView2.Rows[i].Cells[2].Value.ToString();
            }
            dataGridView2.Columns[0].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dataGridView2.Columns[3].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dataGridView2.Columns[1].Visible = false;
            dataGridView2.Columns[2].Visible = false;
            dataGridView2.Columns[0].ReadOnly = true;
            dataGridView2.Columns[3].ReadOnly = true;
            for (int i = 0; i < dataGridView2.RowCount - 1; i++)
            {
                if (dataGridView2.Rows[i].Cells[1].Value.ToString() == "")
                {
                    dataGridView2.Rows.RemoveAt(i);
                    i--;
                }
            }
            dataGridView3.DataSource = con.FillDGV("Select TOP 5 Login as Логин,MINUTE(level3) as минуты,SECOND(level3) as секунды from TopPlayers WHERE Level3 IS NOT NULL order by MINUTE(level3) asc, SECOND(level3) asc;");
            if (dataGridView3.ColumnCount == 4)
            {
                dataGridView3.Columns.Remove(dataGridView3.Columns[0]);
            }
            dataGridView3.Columns.Add("3", "Время");
            for (int i = 0; i < dataGridView3.RowCount; i++)
            {
                if (Convert.ToInt32(dataGridView3.Rows[i].Cells[2].Value) < 10)
                {
                    dataGridView3.Rows[i].Cells[3].Value = dataGridView3.Rows[i].Cells[1].Value.ToString() + ":0" + dataGridView3.Rows[i].Cells[2].Value.ToString();
                }
                else
                dataGridView3.Rows[i].Cells[3].Value = dataGridView3.Rows[i].Cells[1].Value.ToString() + ":" + dataGridView3.Rows[i].Cells[2].Value.ToString();
            }
            dataGridView3.Columns[0].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;

            dataGridView3.Columns[1].Visible = false;
            dataGridView3.Columns[2].Visible = false;
            dataGridView3.Columns[0].ReadOnly = true;
            dataGridView3.Columns[3].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dataGridView3.Columns[3].ReadOnly = true;
            for (int i = 0; i < dataGridView3.RowCount - 1; i++)
            {
                if (dataGridView3.Rows[i].Cells[1].Value.ToString() == "")
                {
                    dataGridView3.Rows.RemoveAt(i);
                    i--;
                }
            }
            dataGridView4.DataSource = con.FillDGV("Select TOP 3 Login as Логин,rank as Рейтинг from UsersData WHERE rank IS NOT NULL order by rank desc;");
            dataGridView4.Columns[0].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dataGridView4.Columns[1].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            dataGridView4.Columns[0].ReadOnly = true;
            dataGridView4.Columns[1].ReadOnly = true;
            for (int i = 0; i < dataGridView4.RowCount - 1; i++)
            {
                if (dataGridView4.Rows[i].Cells[1].Value.ToString() == "")
                {
                    dataGridView4.Rows.RemoveAt(i);
                    i--;
                }
            } 
            con.CloseConnect();
        }
        private void Menu_Load(object sender, EventArgs e)
        {
            UpdateTop();
        }
        private void Menu_Activated(object sender, EventArgs e)
        {
            UpdateTop();
        }
    }
}

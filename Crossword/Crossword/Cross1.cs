using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using AccessC;

namespace Crossword
{
    public partial class Cross1 : Form
    {
        readonly ConectThisAccessWorker con = new ConectThisAccessWorker();
        readonly List<id_cells> idc = new List<id_cells>();
        DateTime date1 = new DateTime(0, 0);
        public string level;
        public string login;
        public int X;
        public int Y;
        public string VremyaProhozhdenia;
        public int id_game;
        bool pravilnie = false;
        int intImgNum = 0;
        public Cross1()
        {
            InitializeComponent();
        }
        private void Cross1_Load(object sender, EventArgs e)
        {
            bullWordList();
            InitializeBoard();
            timer1.Interval = 1000;
            pictureBox2.Image = imageList1.Images[0];
            timer1.Start();
            label7.Text = "Пользователь: " + login;
        }
        private void bullWordList()
        {
            con.OpenConnectic();
            string[] l = { "", "", "", "", "", "" };
            dataGridView2.DataSource = con.FillDGV("Select * from " + level + " ;");
            for (int i = 0; i < dataGridView2.Rows.Count; i++)
            {
                for (int j = 0; j < dataGridView2.Rows[i].Cells.Count; j++)
                {
                    l[j] = dataGridView2.Rows[i].Cells[j].Value.ToString();
                }
                idc.Add(new id_cells(Int32.Parse(l[0]), Int32.Parse(l[1]), l[2], l[3], l[4], l[5]));
            }
            dataGridView2.DataSource = con.FillDGV("Select number,direction,help from " + level + " ;");
            dataGridView2.Columns[0].HeaderText = "#";
            dataGridView2.Columns[0].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            dataGridView2.Columns[1].HeaderText = "Направление";
            dataGridView2.Columns[1].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            dataGridView2.Columns[2].HeaderText = "Подсказка";
            dataGridView2.Columns[2].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dataGridView2.DefaultCellStyle.WrapMode = DataGridViewTriState.True;
            dataGridView2.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
            con.CloseConnect();
        }
        private void InitializeBoard()
        {
            dataGridView1.Columns.Clear();
            dataGridView1.Rows.Clear();
            dataGridView1.BackgroundColor = Color.PaleTurquoise;
            dataGridView1.DefaultCellStyle.BackColor = Color.PaleTurquoise;
            for (int i = 0; i < Y; i++)
                dataGridView1.Columns.Add("l", "");
            dataGridView1.Columns[Y-1].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            for (int i = 0; i < X; i++)
                dataGridView1.Rows.Add();
            dataGridView1.Rows[X - 1].Height = 40;
            foreach (DataGridViewColumn c in dataGridView1.Columns)
                c.Width = dataGridView1.Width / dataGridView1.Columns.Count;
            foreach (DataGridViewRow r in dataGridView1.Rows)
                r.Height = dataGridView1.Height / dataGridView1.Rows.Count;
            for (int row = 0; row < dataGridView1.Rows.Count; row++)
            {
                for (int col = 0; col < dataGridView1.Columns.Count; col++)
                {
                    dataGridView1[col, row].ReadOnly = true;
                    dataGridView1[col, row].Style.ForeColor = Color.DarkGreen;
                }
            }
            foreach (id_cells i in idc)
            {
                int start_col = i.X;
                int start_row = i.Y;
                char[] word = i.word.ToCharArray();
                for (int j = 0; j < word.Length; j++)
                {
                    if (i.direction.ToUpper() == "ACROSS")
                        formatCell(start_row, start_col + j, word[j].ToString());
                    if (i.direction.ToUpper() == "DOWN")
                        formatCell(start_row + j, start_col, word[j].ToString());
                }
            }
        }
        private void formatCell(int row, int col, string letter)
        {
            DataGridViewCell c = dataGridView1[col, row];
            c.Style.BackColor = Color.White;
            c.Style.ForeColor = Color.Black;
            c.ReadOnly = false;
            c.Style.SelectionBackColor = Color.Cyan;
            c.Tag = letter;
        }
        private void dataGridView1_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {
            string number = "";
            if (idc.Any(c => (number = c.number) != "" && c.X == e.ColumnIndex && c.Y == e.RowIndex))
            {
                Rectangle r = new Rectangle(e.CellBounds.X, e.CellBounds.Y, e.CellBounds.Width, e.CellBounds.Height);
                e.Graphics.FillRectangle(Brushes.White, r);
                Font f = new Font(e.CellStyle.Font.FontFamily, 7);
                e.Graphics.DrawString(number, f, Brushes.Black, r);
                e.PaintContent(e.ClipBounds);
                e.Handled = true;
            }
        }
        private void dataGridView1_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (timerCgangeImage.Enabled == true) { pictureBox2.Image = imageList1.Images[0];timerCgangeImage.Stop();}
            try
            {
                dataGridView1[e.ColumnIndex, e.RowIndex].Value = dataGridView1[e.ColumnIndex, e.RowIndex].Value.ToString().ToUpper();
            }
            catch { }
            try
            {
                if (dataGridView1[e.ColumnIndex, e.RowIndex].Value.ToString().Length > 1)
                    dataGridView1[e.ColumnIndex, e.RowIndex].Value = dataGridView1[e.ColumnIndex, e.RowIndex].Value.ToString().Substring(0, 1);
            }
            catch { }
            try
            {
                if (dataGridView1[e.ColumnIndex, e.RowIndex].Value.ToString().ToUpper().Equals(dataGridView1[e.ColumnIndex, e.RowIndex].Tag.ToString().ToUpper()))
                {
                    dataGridView1[e.ColumnIndex, e.RowIndex].Style.ForeColor = Color.DarkGreen;
                }
                else
                {
                    dataGridView1[e.ColumnIndex, e.RowIndex].Style.ForeColor = Color.Red;
                }
            }
            catch { }
        }
        private void timer1_Tick(object sender, EventArgs e)
        {
            date1 = date1.AddSeconds(1);
            label1.Text = date1.ToString("mm:ss");
            if (label1.Text == VremyaProhozhdenia)
            {
                timer1.Stop();
                MessageBox.Show("Время закончилось! Уровень не пройден! Попробуйте заново!");
            }
        }
        private void Cross1_FormClosed(object sender, FormClosedEventArgs e)
        {
            timer1.Stop();
            timerCgangeImage.Stop();
        }
        private void button1_Click(object sender, EventArgs e)
        {
            for (int row = 0; row < dataGridView1.Rows.Count; row++)
            {
                for (int col = 0; col < dataGridView1.Columns.Count; col++)
                {
                    if (dataGridView1[col, row].Style.ForeColor == Color.DarkGreen)
                    {
                        pravilnie = true;
                    }
                    else
                    {
                        pravilnie = false;
                        break;
                    }
                }
                if (pravilnie == false) break;
            }
            if (pravilnie == true)
            {
                timer1.Stop();
                MessageBox.Show("Вы прошли уровень!");
                if (login != "")
                {
                    con.OpenConnectic();
                    DateTime RecordTime = DateTime.ParseExact(label1.Text, "mm:ss", null);
                    string query = "Update TopPlayers Set " + level + " = \"00:" + RecordTime.Minute + ":" + RecordTime.Second + "\" Where id_game = " + id_game + ";";
                    try
                    {
                        con.InsertAndDeleteAndUpdate(query);
                    }
                    catch(Exception er)
                    {
                        MessageBox.Show(er.ToString());
                    }
                    int rank = Convert.ToInt32(con.Select("SELECT rank FROM UsersData WHERE Login LIKE \"" + login + "\";"));
                    string[] Minuta = label1.Text.Split(':');
                    string[] MaxMinuta = VremyaProhozhdenia.Split(':');
                    for (int i = Int32.Parse(MaxMinuta[0]); i >= 1; i--)
                    {
                        if (Int32.Parse(Minuta[0]) < i)
                        {
                            rank++;
                        }
                    }
                    try
                    {
                        con.InsertAndDeleteAndUpdate("Update UsersData Set rank = \"" + rank + "\" Where Login = \"" + login + "\";");
                    }
                    catch (Exception er)
                    {
                        MessageBox.Show(er.ToString());
                    }
                    con.CloseConnect();
                }
                else
                {
                    MessageBox.Show("Вы не авторизовались, ваш результат не был сохранён!");
                }
                Close();
            }
            else
            {
                MessageBox.Show("Кроссворд не был решён!");
                intImgNum = 0;

                pictureBox2.Image = imageList1.Images[intImgNum];
                timerCgangeImage.Interval = 250;
                timerCgangeImage.Start();
            }
        }
        private void timerChangeImage_Tick(object sender, EventArgs e)
        {
            pictureBox2.Image = imageList1.Images[intImgNum];
            if (intImgNum == imageList1.Images.Count - 1) 
            {
                intImgNum = 0;
            }
            else
            {
                intImgNum++;
            }
        }
    }
    public class id_cells
    {
        public int X;
        public int Y;
        public string direction;
        public string number;
        public string word;
        public string clue;
        public id_cells(int x, int y, string d, string n, string w, string c)
        {
            this.X = x;
            this.Y = y;
            this.direction = d;
            this.number = n;
            this.word = w;
            this.clue = c;
        }
    }
}

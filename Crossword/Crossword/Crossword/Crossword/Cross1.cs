using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Data.OleDb;

namespace Crossword
{
    public partial class Cross1 : Form
    {
        AccessC.ConectThisAccessWorker con = new AccessC.ConectThisAccessWorker();
        List<id_cells> idc = new List<id_cells>();
        DateTime t1, t2;
        public string level { get; set; }
        public int X { get; set; }
        public int Y { get; set; }

        public int We { get; set; }
        bool Pravilnie = false;
        public Cross1()
        {
            InitializeComponent();
        }
        private void Cross1_Load(object sender, EventArgs e)
        {
            bullWordList();
            InitializeBoard();
            timer1.Interval = 1000;
            timer1.Start();
            t1 = DateTime.Now;
        }
        private void bullWordList()
        {
            con.OpenConnectic();
            string[] l = { "", "", "", "", "", "" };
            dataGridView2.DataSource = con.fillDGV("Select * from " + level + " ;");
            for (int i = 0; i < dataGridView2.Rows.Count; i++)
            {
                for (int j = 0; j < dataGridView2.Rows[i].Cells.Count; j++)
                {
                    l[j] = dataGridView2.Rows[i].Cells[j].Value.ToString();
                }
                idc.Add(new id_cells(Int32.Parse(l[0]), Int32.Parse(l[1]), l[2], l[3], l[4], l[5]));
            }
            dataGridView2.DataSource = con.fillDGV("Select number,direction,help from " + level + " ;");
            dataGridView2.Columns[0].HeaderText = "#";
            dataGridView2.Columns[0].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            dataGridView2.Columns[1].HeaderText = "Направление";
            dataGridView2.Columns[1].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            dataGridView2.Columns[2].HeaderText = "Подсказка";
            dataGridView2.Columns[2].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            dataGridView2.Width = We;
            //AutoSizeMode = AutoSizeMode.
            con.CloseConnect();
        }
        private void InitializeBoard()
        {
            dataGridView1.Columns.Clear();
            dataGridView1.Rows.Clear();
            dataGridView1.BackgroundColor = Color.DarkCyan;
            dataGridView1.DefaultCellStyle.BackColor = Color.DarkCyan;
            for (int i = 0; i < Y; i++)
                dataGridView1.Columns.Add("l", "");
            for (int i = 0; i < X; i++)
                dataGridView1.Rows.Add();
            foreach (DataGridViewColumn c in dataGridView1.Columns)
                c.Width = dataGridView1.Width / dataGridView1.Columns.Count;
            foreach (DataGridViewRow r in dataGridView1.Rows)
                r.Height = dataGridView1.Height / dataGridView1.Rows.Count;
            for (int row = 0; row < dataGridView1.Rows.Count; row++)
            {
                for (int col = 0; col < dataGridView1.Columns.Count; col++)
                {
                    dataGridView1[col, row].ReadOnly = true;
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
            c.ReadOnly = false;
            c.Style.SelectionBackColor = Color.Cyan;
            c.Tag = letter;
        }
        private void dataGridView1_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {
            String number = "";
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
                    Pravilnie = true;
                }
                else
                {
                    dataGridView1[e.ColumnIndex, e.RowIndex].Style.ForeColor = Color.Red;
                    Pravilnie = false;
                }
            }
            catch { }
        }
        private void timer1_Tick(object sender, EventArgs e)
        {
            t2 = DateTime.Now;
            TimeSpan ts = t2 - t1;
            label1.Text =  ts.Minutes.ToString() + ":" + ts.Seconds.ToString();
            if (label1.Text == "1:00")
            {
                timer1.Stop();
                MessageBox.Show("Время закончилось! Уровень не пройден! Попробуйте заново!");
            }
        }
        private void Cross1_FormClosed(object sender, FormClosedEventArgs e)
        {
            timer1.Stop();
        }
        private void button1_Click(object sender, EventArgs e)
        {
            if(Pravilnie)
            {
                timer1.Stop();
                MessageBox.Show("Вы прошли уровень!");
            }
            else
            {
                MessageBox.Show("Кроссворд не был решён!");
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

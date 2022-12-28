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
    public partial class Form1 : Form
    {
        AccessC.ConectThisAccessWorker Conectic = new AccessC.ConectThisAccessWorker();
        public Form1()
        {
            InitializeComponent();
        } 
        private void Form1_Load(object sender, EventArgs e)
        {
            Conectic.OpenConnectic();
        }
        int Rows = 6;//j
        int Columns = 6;//i
        string[] words = {" "," "," "," "," "," "};
        int WordCount = 6;
        char[,] crossword =
        {
            {' ',' ',' ',' ',' ',' '},
            {' ',' ',' ',' ',' ',' '},
            {' ',' ',' ',' ',' ',' '},
            {' ',' ',' ',' ',' ',' '},
            {' ',' ',' ',' ',' ',' '},
            {' ',' ',' ',' ',' ',' '}
        };
        string table = "table1";
        private void button1_Click(object sender, EventArgs e)
        {
            string query = "INSERT INTO " + table + " (t_word) VALUES (\"";
            query += textBox1.Text + "\");";
            Conectic.InsertAndDeleteAndUpdate(query);
        }
        private void button2_Click(object sender, EventArgs e)
        {
            table = textBox2.Text;
            string query = "Create table " + textBox2.Text + "(t_id AUTOINCREMENT PRIMARY KEY , t_word varchar(6));";
            Conectic.InsertAndDeleteAndUpdate(query);
        }
        private void button3_Click(object sender, EventArgs e)
        {
            string query = "Select t_word from " + table + ";";
            textBox7.Text = Conectic.Select(query);
            generate(query);
        }
        void generate(string query)
        {
            words = Conectic.Select(query).Split(' ');
            int max = 0;
            for (int i = 0; i < WordCount; i++)
            {
                if (words[i].Length > words[max].Length) max = i;
            }
            for (int j = 0; j < Columns; j++)
                crossword[3, j] = words[max][j];
            words = words.Except(new string[] { words[max]}).ToArray();
            WordCount -= 1;
            for(int i = 0; i < WordCount; i++)
            {
                for(int r = 0; r < Rows; r++)
                {
                    for (int j = 0; j < Columns; j++)
                    {
                        foreach (char c in words[i])
                        {
                            if (c == crossword[3, j])
                                for (int j1 = 0; j1 < words[i].Length; j1++)
                                    crossword[j1, i] = words[i][j1];
                        }
                    }
                }
            }
            dataGridView1.RowCount = Rows;
            dataGridView1.ColumnCount = Columns;
            for (int i = 0; i < dataGridView1.Rows.Count; i++)
            {
                for (int j = 0; j < dataGridView1.Columns.Count; j++)
                    dataGridView1[i, j].Value = crossword[i, j];// заполненме элемента datagridview1 матрицей(кроссвордом)
            }
        }
    }
}

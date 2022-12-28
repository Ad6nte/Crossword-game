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
    public partial class Menu : Form
    {
        public Menu()
        {
            InitializeComponent();
        }
        public Label l1 = new Label()
        {
            Text = "Пользователь: "
        };
        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Login log = new Login();
            log.label3 = this.label1;
            log.Show();
        }
        private void linkLabel2_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Registrarion reg = new Registrarion();
            reg.Show();
        }
        private void button1_Click(object sender, EventArgs e)
        {
            Cross1 cr = new Cross1();
            cr.X = 15;
            cr.Y = 18;
            cr.We = 520;
            cr.level = "cross1";
            cr.Show();
        }
        private void button2_Click(object sender, EventArgs e)
        {
            Cross1 cr = new Cross1();
            cr.X = 20;
            cr.Y = 18;
            cr.We = 579;
            cr.level = "cross2";
            cr.Show();
        }
        private void button3_Click(object sender, EventArgs e)
        {
            Cross1 cr = new Cross1();
            cr.X = 21;
            cr.Y = 28;
            cr.We = 745;
            cr.level = "cross3";
            cr.Show();
        }
    }
}

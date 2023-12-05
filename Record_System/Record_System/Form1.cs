using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Record_System
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {

            if (tb_username.Text == "admin" && tb_password.Text == "minhs123")
            {
                MessageBox.Show("Login Successfully!");
                this.Hide();
                Dashboard ar = new Dashboard();
                ar.ShowDialog();
                //this.Close();

            }

            else if (tb_username.Text == "" && tb_username.Text == "")
            {
                MessageBox.Show("Please input your your username and password!");
            }

            else if (tb_username.Text == "" || tb_username.Text != "admin")
            {
                MessageBox.Show("Please input your correct username.");
            }
            else if (tb_password.Text == "" || tb_password.Text != "minhs123")
            {
                MessageBox.Show("Please input your correct password.");
            }
            else
            {
                MessageBox.Show("Invalid Login credetials!");
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            tb_username.Clear();
            tb_password.Clear();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            DialogResult res;
            res = MessageBox.Show("Are you sure you want to Exit this Application?", "Exit", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (res == DialogResult.Yes)
            {
                Application.Exit();

            }
            else
            {
                this.Show();

            }
        }
    }
}

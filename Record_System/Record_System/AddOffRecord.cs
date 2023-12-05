using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Record_System
{
    public partial class AddOffRecord : Form
    {
        public AddOffRecord()
        {
            InitializeComponent();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            tb_lname.Clear();
            tb_fname.Clear();
            tb_mname.Clear();
            tb_id.Clear();
            tb_year.Clear();
            tb_section.Clear();
            tb_details.Clear();
        }

        private void AddOffRecord_Load(object sender, EventArgs e)
        {
            List<offense> list = new List<offense>();
            list.Add(new offense() { ID = 1, Name = "Littering" });
            list.Add(new offense() { ID = 2, Name = "Misbehaving" });
            list.Add(new offense() { ID = 3, Name = "Lack of Academic Integrity" });
            list.Add(new offense() { ID = 4, Name = "Smoking" });
            list.Add(new offense() { ID = 5, Name = "Gambling" });
            list.Add(new offense() { ID = 6, Name = "Stealing" });
            list.Add(new offense() { ID = 7, Name = "Vandalism" });
            list.Add(new offense() { ID = 8, Name = "Possession of unnecessary materials\r\n" });
            list.Add(new offense() { ID = 9, Name = "possession of deadly weapons\r\n" });
            list.Add(new offense() { ID = 10, Name = "Harrassment" });
            list.Add(new offense() { ID = 11, Name = "Unauthorizerd used of School's Information\r\n" });
            list.Add(new offense() { ID = 12, Name = "OTHERS" });
            comboBox1.DataSource = list;
            comboBox1.ValueMember = "ID";
            comboBox1.DisplayMember = "Name";
        }

        private void dateTimePicker1_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = true;
        }
        void addToDatabase()
        {
            using (SqlConnection con = new SqlConnection(databaseHelper.conString))
            {
                con.Open();
                String query = @"INSERT INTO studentOffenceRecord (LRN, Name, Year, Section, Date, Time, OffenceType, Details) 
                                VALUES (@lrn, @name, @year, @section, @date, @time, @offenceType, @details)";
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("lrn", tb_id.Text);
                    cmd.Parameters.AddWithValue("name", $"{tb_lname.Text}, {tb_fname.Text} {tb_mname.Text}");
                    cmd.Parameters.AddWithValue("year", tb_year.Text);
                    cmd.Parameters.AddWithValue("section", tb_section.Text);
                    cmd.Parameters.AddWithValue("@date", dtp_date.Value.ToString("yyyy-MM-dd"));
                    cmd.Parameters.AddWithValue("@time", dtp_time.Value.ToString("HH:mm:ss"));
                    cmd.Parameters.AddWithValue("offenceType", comboBox1.Text);
                    cmd.Parameters.AddWithValue("details", tb_details.Text);

                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Added successfully");
                    this.Close();
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (validationHelper.isEmptyTB(tb_fname, "First Name"))
                return;
            if (validationHelper.isEmptyTB(tb_lname, "Last Name"))
                return;
            if (validationHelper.isEmptyTB(tb_mname, "Middle Name"))
                return;
            if (validationHelper.isEmptyTB(tb_id, "LRN/Student ID Number"))
                return;
            if (validationHelper.isEmptyTB(tb_year, "Year"))
                return;
            if (validationHelper.isEmptyTB(tb_section, "Section"))
                return;
            if (validationHelper.isEmptyTB(tb_details, "Details"))
                return;

            addToDatabase();
        }
    }
}

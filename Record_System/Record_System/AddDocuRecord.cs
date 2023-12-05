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
    public partial class AddDocuRecord : Form
    {
        public AddDocuRecord()
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

        private void AddDocuRecord_Load(object sender, EventArgs e)
        {
            List<document> list = new List<document>();
            list.Add(new document() { ID = 1, Name = "Good Moral Certificate" });
            list.Add(new document() { ID =2, Name = "Form 137" });
            list.Add(new document() { ID = 2, Name = "Report Card/Form138" });
            list.Add(new document() { ID = 3, Name = "OTHERS" });
            comboBox1.DataSource = list;
            comboBox1.ValueMember = "ID";
            comboBox1.DisplayMember = "Name";
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

        void addToDatabase()
        {
            using (SqlConnection con = new SqlConnection(databaseHelper.conString))
            {
                con.Open();
                String query = @"INSERT INTO studentDocumentRequest (LRN, Name, Year, Section, Date, Time, DocumentType, Details) 
                                VALUES (@lrn, @name, @year, @section, @date, @time, @docType, @details)";
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("lrn", tb_id.Text);
                    cmd.Parameters.AddWithValue("name", $"{tb_lname.Text}, {tb_fname.Text} {tb_mname.Text}");
                    cmd.Parameters.AddWithValue("year", tb_year.Text);
                    cmd.Parameters.AddWithValue("section", tb_section.Text);
                    cmd.Parameters.AddWithValue("@date", dtp_date.Value.ToString("yyyy-MM-dd"));
                    cmd.Parameters.AddWithValue("@time", dtp_time.Value.ToString("HH:mm:ss"));
                    cmd.Parameters.AddWithValue("docType", comboBox1.Text);
                    cmd.Parameters.AddWithValue("details", tb_details.Text);

                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Added successfully");
                    this.Close();
                }
            }
        }

        private void comboBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = true;
        }

        private void dtp_date_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = true;
        }

        private void dtp_time_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = true;
        }
    }
}

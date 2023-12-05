using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Record_System
{
    public partial class Dashboard : Form
    {
        bool isLogout = false;
        public Dashboard()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            groupBox2.Hide();
            groupBox3.Hide();
        }

        private void Dashboard_Load(object sender, EventArgs e)
        {
            dataGridView2.CellEndEdit += dataGridView2_CellEndEdit;
            dataGridView2.UserDeletingRow += dataGridView2_UserDeletingRow;
            dataGridView2.AllowUserToDeleteRows = true;
            dataGridView2.ReadOnly = false;

            dataGridView1.CellEndEdit += dataGridView1_CellEndEdit;
            dataGridView1.UserDeletingRow += dataGridView1_UserDeletingRow;
            dataGridView1.AllowUserToDeleteRows = true;
            dataGridView1.ReadOnly = false;


            groupBox2.Hide();
            groupBox3.Hide();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            AddDocuRecord adr = new AddDocuRecord();
            adr.ShowDialog();
            loadDocument();
        }

        private void button4_Click(object sender, EventArgs e)
        {
           AddOffRecord aor = new AddOffRecord();
            aor.ShowDialog();
            loadOffence();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            DialogResult res;
            res = MessageBox.Show("Are you sure you want to Logout?", "Logout", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (res == DialogResult.Yes)
            {
                isLogout = true;
                Form1 frm = new Form1();
                this.Close();
                frm.Show();
                //Application.Exit();
                //Form1 f1 = new Form1();
                //f1.ShowDialog();
            }
            else
            {
                this.Show();

            }
        }

        private void pictureBox4_Click(object sender, EventArgs e)
        {
            DialogResult res;
            res = MessageBox.Show("Are you sure you want to Logout?", "Logout", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (res == DialogResult.Yes)
            {
                Form1 frm = new Form1();
                this.Close();
                frm.Show();
                //Application.Exit();
                //Form1 f1 = new Form1();
                //f1.ShowDialog();
            }
            else
            {
                this.Show();

            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            loadOffence();
            groupBox2.Show();
            groupBox3.Hide();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            loadDocument();
            groupBox2.Hide();
            groupBox3.Show();

        }

        void loadOffence()
        {
            using (SqlConnection con = new SqlConnection(databaseHelper.conString))
            {
                String query = "SELECT Lrn, Name, Year, Section, Date, Time, OffenceType, Details FROM studentOffenceRecord";
                if (!string.IsNullOrEmpty(textBox1.Text))
                    query += $" WHERE Name LIKE '%{textBox1.Text}%'";

                using (SqlCommand cmd = new SqlCommand(query, con))
                {

                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    dataGridView1.DataSource = dt;
                }


                // Modify column headers
                dataGridView1.Columns["Lrn"].HeaderText = "LRN";
                dataGridView1.Columns["Name"].HeaderText = "NAME";
                dataGridView1.Columns["Year"].HeaderText = "YEAR";
                dataGridView1.Columns["Section"].HeaderText = "SECTION";
                dataGridView1.Columns["Date"].HeaderText = "DATE";
                dataGridView1.Columns["Time"].HeaderText = "TIME";
                dataGridView1.Columns["OffenceType"].HeaderText = "OFFENCE TYPE";
                dataGridView1.Columns["Details"].HeaderText = "DETAILS";
                dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            }
        }

        void loadDocument()
        {
            using (SqlConnection con = new SqlConnection(databaseHelper.conString))
            {
                String query = "SELECT Lrn, Name, Year, Section, Date, Time, DocumentType, Details FROM studentDocumentRequest";
                if (!string.IsNullOrEmpty(docuTB.Text))
                    query += $" WHERE Name LIKE '%{docuTB.Text}%'";

                using (SqlCommand cmd = new SqlCommand(query, con))
                {

                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    dataGridView2.DataSource = dt;
                }


                // Modify column headers
                dataGridView2.Columns["Lrn"].HeaderText = "LRN";
                dataGridView2.Columns["Name"].HeaderText = "NAME";
                dataGridView2.Columns["Year"].HeaderText = "YEAR";
                dataGridView2.Columns["Section"].HeaderText = "SECTION";
                dataGridView2.Columns["Date"].HeaderText = "DATE";
                dataGridView2.Columns["Time"].HeaderText = "TIME";
                dataGridView2.Columns["DocumentType"].HeaderText = "DOCUMENT TYPE";
                dataGridView2.Columns["Details"].HeaderText = "DETAILS";
                dataGridView2.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            }
        }
        private void dataGridView2_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.RowIndex >= 0)
                {
                    DataGridViewRow editedRow = dataGridView2.Rows[e.RowIndex];
                    DataRowView rowView = editedRow.DataBoundItem as DataRowView;

                    if (rowView != null)
                    {
                        DataRow editedDataRow = rowView.Row;

                        using (SqlConnection con = new SqlConnection(databaseHelper.conString))
                        {
                            con.Open();
                            string updateQuery = "UPDATE studentDocumentRequest SET Lrn = @Lrn, Name = @Name, Year = @Year, Section = @Section, Date = @Date, Time = @Time, DocumentType = @DocumentType, Details = @Details WHERE Lrn = @OriginalLrn";

                            SqlCommand cmd = new SqlCommand(updateQuery, con);
                            cmd.Parameters.AddWithValue("@Lrn", editedDataRow["LRN"]);
                            cmd.Parameters.AddWithValue("@Name", editedDataRow["Name"]);
                            cmd.Parameters.AddWithValue("@Year", editedDataRow["Year"]);
                            cmd.Parameters.AddWithValue("@Section", editedDataRow["Section"]);
                            cmd.Parameters.AddWithValue("@Date", editedDataRow["Date"]);
                            cmd.Parameters.AddWithValue("@Time", editedDataRow["Time"]);
                            cmd.Parameters.AddWithValue("@DocumentType", editedDataRow["DocumentType"]);
                            cmd.Parameters.AddWithValue("@Details", editedDataRow["Details"]);
                            cmd.Parameters.AddWithValue("@OriginalLrn", editedDataRow["LRN", DataRowVersion.Original]);

                            cmd.ExecuteNonQuery();

                            MessageBox.Show("Successfully edited a data.");
                        }
                    }
                }
            } catch(Exception ex)
            {
                MessageBox.Show("Something went wrong.");
            }
            
        }

        private void dataGridView2_UserDeletingRow(object sender, DataGridViewRowCancelEventArgs e)
        {
            try
            {
                DialogResult result = MessageBox.Show("Are you sure you want to delete this row?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (result == DialogResult.Yes && e.Row.Index >= 0)
                {
                    DataGridViewRow deletedRow = dataGridView2.Rows[e.Row.Index];
                    DataRowView rowView = deletedRow.DataBoundItem as DataRowView;

                    if (rowView != null)
                    {
                        DataRow deletedDataRow = rowView.Row;

                        using (SqlConnection con = new SqlConnection(databaseHelper.conString))
                        {
                            con.Open();
                            string deleteQuery = "DELETE FROM studentDocumentRequest WHERE Lrn = @Lrn";

                            SqlCommand cmd = new SqlCommand(deleteQuery, con);
                            cmd.Parameters.AddWithValue("@Lrn", deletedDataRow["LRN"]);

                            cmd.ExecuteNonQuery();
                        }
                    }
                }
                else
                {
                    e.Cancel = true; // Cancel deletion if the user clicks 'No'
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Something went wrong.");
            }
        }
        private void dataGridView1_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.RowIndex >= 0)
                {
                    DataGridViewRow editedRow = dataGridView1.Rows[e.RowIndex];
                    DataRowView rowView = editedRow.DataBoundItem as DataRowView;

                    if (rowView != null)
                    {
                        DataRow editedDataRow = rowView.Row;

                        using (SqlConnection con = new SqlConnection(databaseHelper.conString))
                        {
                            con.Open();
                            string updateQuery = "UPDATE studentOffenceRecord SET Lrn = @Lrn, Name = @Name, Year = @Year, Section = @Section, Date = @Date, Time = @Time, OffenceType = @DocumentType, Details = @Details WHERE Lrn = @OriginalLrn";

                            SqlCommand cmd = new SqlCommand(updateQuery, con);
                            cmd.Parameters.AddWithValue("@Lrn", editedDataRow["LRN"]);
                            cmd.Parameters.AddWithValue("@Name", editedDataRow["Name"]);
                            cmd.Parameters.AddWithValue("@Year", editedDataRow["Year"]);
                            cmd.Parameters.AddWithValue("@Section", editedDataRow["Section"]);
                            cmd.Parameters.AddWithValue("@Date", editedDataRow["Date"]);
                            cmd.Parameters.AddWithValue("@Time", editedDataRow["Time"]);
                            cmd.Parameters.AddWithValue("@DocumentType", editedDataRow["OffenceType"]);
                            cmd.Parameters.AddWithValue("@Details", editedDataRow["Details"]);
                            cmd.Parameters.AddWithValue("@OriginalLrn", editedDataRow["LRN", DataRowVersion.Original]);

                            cmd.ExecuteNonQuery();

                            MessageBox.Show("Successfully edited a data.");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Something went wrong.");
                loadOffence();
            }
            
        }

        private void dataGridView1_UserDeletingRow(object sender, DataGridViewRowCancelEventArgs e)
        {
            try
            {
                DialogResult result = MessageBox.Show("Are you sure you want to delete this row?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (result == DialogResult.Yes && e.Row.Index >= 0)
                {
                    DataGridViewRow deletedRow = dataGridView1.Rows[e.Row.Index];
                    DataRowView rowView = deletedRow.DataBoundItem as DataRowView;

                    if (rowView != null)
                    {
                        DataRow deletedDataRow = rowView.Row;

                        using (SqlConnection con = new SqlConnection(databaseHelper.conString))
                        {
                            con.Open();
                            string deleteQuery = "DELETE FROM studentOffenceRecord WHERE Lrn = @Lrn";

                            SqlCommand cmd = new SqlCommand(deleteQuery, con);
                            cmd.Parameters.AddWithValue("@Lrn", deletedDataRow["LRN"]);

                            cmd.ExecuteNonQuery();
                        }
                    }
                }
                else
                {
                    e.Cancel = true; // Cancel deletion if the user clicks 'No'
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Something went wrong.");
                loadDocument();
            }
            
        }
        private void Dashboard_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (isLogout) return;
            Environment.Exit(0);
            Application.Exit();
        }

        private void docuTB_TextChanged(object sender, EventArgs e)
        {
            loadDocument();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            loadOffence();
        }

        private void reqDelBtn_Click(object sender, EventArgs e)
        {
            try
            {
                String selectedID;
                if (dataGridView2.Rows.Count > 0)
                {
                    DataGridViewRow selectedRow = dataGridView2.SelectedRows[0];
                    selectedID = selectedRow.Cells[0].Value.ToString();

                    DialogResult result = MessageBox.Show("Are you sure you want to delete this row?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (result == DialogResult.Yes)
                    {
                        using (SqlConnection con = new SqlConnection(databaseHelper.conString))
                        {
                            con.Open();
                            string deleteQuery = "DELETE FROM studentDocumentRequest WHERE Lrn = @Lrn";

                            SqlCommand cmd = new SqlCommand(deleteQuery, con);
                            cmd.Parameters.AddWithValue("@Lrn", selectedID);

                            cmd.ExecuteNonQuery();
                            MessageBox.Show("Successfully deleted " + selectedID);
                            loadDocument();
                        }
                    }
                }
            } catch (Exception ex)
            {
                MessageBox.Show("Please click the whole row.");
            }
            
        }

        private void offDelBtn_Click(object sender, EventArgs e)
        {
            try
            {
                String selectedID;
                if (dataGridView1.Rows.Count > 0)
                {
                    DataGridViewRow selectedRow = dataGridView1.SelectedRows[0];
                    selectedID = selectedRow.Cells[0].Value.ToString();

                    DialogResult result = MessageBox.Show("Are you sure you want to delete this row?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (result == DialogResult.Yes)
                    {
                        using (SqlConnection con = new SqlConnection(databaseHelper.conString))
                        {
                            con.Open();
                            string deleteQuery = "DELETE FROM studentOffenseRecord WHERE Lrn = @Lrn";

                            SqlCommand cmd = new SqlCommand(deleteQuery, con);
                            cmd.Parameters.AddWithValue("@Lrn", selectedID);

                            cmd.ExecuteNonQuery();
                            MessageBox.Show("Successfully deleted " + selectedID);
                            loadOffence();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Please click the whole row.");
            }
            
        }

    }
}

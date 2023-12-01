using System;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Data;

namespace Grifindo_Toys
{
    public partial class Settings : Form
    {
        public Settings()
        {
            InitializeComponent();
        }

        MyConnection db = new MyConnection();
        SqlDataAdapter SqlDa = new SqlDataAdapter();
        SqlCommand cmd = new SqlCommand();

        private void button2_Click(object sender, EventArgs e)
        {
            this.Hide();
            Admin_Dashboard admin_Dashboard = new Admin_Dashboard();
            admin_Dashboard.Show();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            DateTime date1 = dateTimePicker1.Value.Date;
            DateTime date2 = dateTimePicker2.Value.Date;

            int dateiff = ((TimeSpan)(date2 - date1)).Days;
            if(dateiff < 0 || dateiff <= 29)
            {
                MessageBox.Show("Error with saving.. Salary Cycle require 30 days or more than 30 days", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                txtDate.Text = dateiff.ToString();
            }

            if(textBox1.Text == "")
            {
                MessageBox.Show("Please Enter no of Leaves first", "Error.", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
            else
            {
                try
                {
                    string queSave = "UPDATE Positions SET No_Leaves='" + textBox1.Text + "' WHERE Position='" + comboBox1.Text + "'";
                    db.conn.Open();
                    cmd = new SqlCommand(queSave, db.conn);
                    cmd.ExecuteNonQuery();
                    db.conn.Close();
                    MessageBox.Show("Record Name:" + comboBox1.Text + " successfully SAVE to the Database!", "SAVE!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    textBox1.Text = "";
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error" + ex.ToString(), "Error getting Data...", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void Settings_Load(object sender, EventArgs e)
        {
            try
            {
                String getDetails = "SELECT Position FROM Positions";
                db.conn.Open();
                SqlDa = new SqlDataAdapter(getDetails, db.conn);
                DataTable DtCou = new DataTable();
                SqlDa.Fill(DtCou);
                comboBox1.Items.Clear();
                comboBox1.Items.Add("--SELECT--");
                foreach (DataRow row in DtCou.Rows)
                {
                    comboBox1.Items.Add(row["Position"]);
                }
                comboBox1.SelectedIndex = 0;
                db.conn.Close();

            }
            catch (Exception getdetails)
            {
                MessageBox.Show("Error.." + getdetails, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            textBox1.Visible = false;
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            textBox1.Visible = true;
            try
            {
                if (comboBox1.SelectedIndex > 0)
                {
                    string id = comboBox1.SelectedItem.ToString();
                    string queGetDetails = "SELECT * FROM Positions WHERE Position='" + id +"'";
                    db.conn.Open();
                    cmd = new SqlCommand(queGetDetails, db.conn);
                    SqlDataReader R = cmd.ExecuteReader();
                    while (R.Read())
                    {
                        textBox1.Text = R.GetValue(2).ToString();
                        
                    }
                    db.conn.Close();
                }
                else
                {
                    textBox1.Visible = false;
                }
            }
            catch (Exception DataErr)
            {
                MessageBox.Show("Error while Loading Data..." +
               Environment.NewLine + DataErr);
            }

        }
    }
}

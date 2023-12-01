using System;
using System.Data;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace Grifindo_Toys
{
    public partial class ChangeSalary : Form
    {
        public ChangeSalary()
        {
            InitializeComponent();
        }

        MyConnection db = new MyConnection();
        SqlCommand cmd = new SqlCommand();
        SqlDataAdapter SqlDa = new SqlDataAdapter();

        private void button3_Click(object sender, EventArgs e)
        {
            Admin_Dashboard admin_Dashboard = new Admin_Dashboard();
            admin_Dashboard.Show();
            this.Hide();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (comboBox1.SelectedIndex > 0)
                {
                    string id = comboBox1.SelectedItem.ToString();
                    string queGetDetails = "SELECT * FROM Salaries WHERE Position='" + id + "'";
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
                    ClearAll();
                }
            }
            catch (Exception DataErr)
            {
                MessageBox.Show("Error while Loading Student Data..." +
               Environment.NewLine + DataErr);
            }
        }

        private void ClearAll()
        {
            comboBox1.SelectedIndex = 0;
            textBox1.Text = "";
        }
        private void ChangeSalary_Load(object sender, EventArgs e)
        {
            try
            {
                String getDetails = "SELECT Position FROM Salaries";
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
        }

        private void button2_Click(object sender, EventArgs e)
        {
            ClearAll();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text == "")
            {
                MessageBox.Show("Please Select the Salary Range first", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                try
                {
                    string queUp = "UPDATE Salaries SET Salary='" + textBox1.Text + "' WHERE Position = '" + comboBox1.SelectedItem + "'";
                    db.conn.Open();
                    cmd = new SqlCommand(queUp, db.conn);
                    cmd.ExecuteNonQuery();
                    db.conn.Close();
                    MessageBox.Show("Position Is:" + comboBox1.Text + " record Successfully UPDATED!", "UPDATE!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    ClearAll();
                }
                catch (Exception UpErr)
                {
                    MessageBox.Show("Error while Update..." + "\n" + UpErr);
                }
            }
        }
    }
}

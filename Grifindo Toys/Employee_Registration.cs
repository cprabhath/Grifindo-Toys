using System;
using System.Data;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Drawing;

namespace Grifindo_Toys
{
    public partial class Employee_Registration : Form
    {
        public Employee_Registration()
        {
            InitializeComponent();
        }

        SqlDataAdapter SqlDa = new SqlDataAdapter();
        MyConnection db = new MyConnection();
        SqlCommand cmd = new SqlCommand();

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void Employee_Registration_Load(object sender, EventArgs e)
        {
            NumGen();
            try
            {
                if (MyConnection.type == "A")
                {
                    String q = "SELECT * FROM Users";
                    db.conn.Open();
                    cmd = new SqlCommand(q, db.conn);
                    SqlDataReader sqlDataReader = cmd.ExecuteReader();
                    if (sqlDataReader.HasRows)
                    {
                        sqlDataReader.Read();
                        label2.Text = sqlDataReader[1].ToString();
                    }
                    db.conn.Close();
                }

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
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Hide();
            Admin_Dashboard admin_Dashboard = new Admin_Dashboard();
            admin_Dashboard.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            ClearAll();
        }

        private void ClearAll()
        {
            textBox2.Text = "";
            textBox3.Text = "";
            textBox5.Text = "";
            textBox6.Text = "";
        }


        private void NumGen()
        {
            try
            {
                string queDataCheck = "SELECT * FROM Employees";
                db.conn.Open();
                SqlDa = new SqlDataAdapter(queDataCheck, db.conn);
                DataTable Dt = new DataTable();
                SqlDa.Fill(Dt);
                int id = 0;
                String EmpId = "";
                if (Dt.Rows.Count > 0)
                {
                    string getMax = "SELECT MAX(EmpID) FROM Employees";
                    cmd = new SqlCommand(getMax, db.conn);
                    SqlDataReader R = cmd.ExecuteReader();
                    while (R.Read())
                    {
                        id = int.Parse(R.GetValue(0).ToString());
                    }
                    id += 1;
                }
                else
                {
                    id = 1;
                }
                EmpId = "GT-" + id;
                db.conn.Close();
                textBox1.Text = EmpId;
            }
            catch (Exception NumGenError)
            {
                MessageBox.Show("Error while Number Generate..." + Environment.NewLine +
               NumGenError);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                string queSave = "INSERT INTO Employees(EmpID, EmpName, Designation, NICorPassport, ContactNo, Address, DOB) VALUES('" + textBox1.Text + "', '" + textBox2.Text + "','" + comboBox1.Text + "','" + textBox3.Text + "','" + textBox5.Text + "','" + textBox6.Text + "','" + dateTimePicker1.Text + "')";
                db.conn.Open();
                cmd = new SqlCommand(queSave, db.conn);
                cmd.ExecuteNonQuery();
                db. conn.Close();
                MessageBox.Show("Employee ID:" + textBox1.Text + " successfully SAVE to the Database!", "SAVE!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                ClearAll();
                NumGen();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error saving data" + ex.ToString(), "ERROR!", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {
            if (textBox3.TextLength < 12 || textBox3.TextLength < 9)
            {
                textBox3.ForeColor = Color.Red;
            }
            else
            {
                textBox3.ForeColor = Color.DarkGreen;
            }
        }

        private void textBox5_TextChanged(object sender, EventArgs e)
        {
            if (textBox5.TextLength < 10 )
            {
                textBox5.ForeColor = Color.Red;
            }
            else
            {
                textBox5.ForeColor = Color.DarkGreen;
            }
        }
    }
}

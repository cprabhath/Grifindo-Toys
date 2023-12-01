using System;
using System.Data;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace Grifindo_Toys
{
    public partial class Admin_Dashboard : Form
    {
        public Admin_Dashboard()
        {
            InitializeComponent();
        }

        MyConnection db = new MyConnection();
        SqlDataAdapter SqlDa = new SqlDataAdapter();
        SqlCommand cmd = new SqlCommand();

        private void Admin_Dashboard_Load(object sender, EventArgs e)
        {
            label4.Text = DateTime.Now.ToString("dd/MM/yyyy");
            label3.Text = DateTime.Now.ToString("hh:mm:ss tt");


            try
            {
        
                    String q = "SELECT * FROM admin";
                    db.conn.Open();
                    cmd = new SqlCommand(q, db.conn);
                    SqlDataReader sqlDataReader = cmd.ExecuteReader();
                    if (sqlDataReader.HasRows)
                    {
                        sqlDataReader.Read();
                        lebal7.Text = sqlDataReader[1].ToString();
                    }
                    db.conn.Close();
                
                String Query = "SELECT * FROM admin";
                db.conn.Open();
                SqlDa = new SqlDataAdapter(Query, db.conn);
                DataTable Dt = new DataTable();
                SqlDa.Fill(Dt);
                int eid = 0;
                if (Dt.Rows.Count > 0)
                {
                    string getMax = "SELECT COUNT(id) FROM admin WHERE realName='Active'";
                    cmd = new SqlCommand(getMax, db.conn);
                    SqlDataReader R = cmd.ExecuteReader();
                    while (R.Read())
                    {
                        eid = int.Parse(R.GetValue(0).ToString());
                    }
                }
                db.conn.Close();
                Txt_name.Text = eid.ToString();
            }
            catch (Exception SelectingError)
            {
                MessageBox.Show("Error" + SelectingError, "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Hide();
            Employee_Registration employee_Registration = new Employee_Registration();
            employee_Registration.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Hide();
            Salary salary = new Salary();
            salary.Show();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Hide();
            Settings settings = new Settings();
            settings.Show();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            this.Hide();
            ChangeSalary changeSalary = new ChangeSalary();
            changeSalary.Show();

        }

        private void button6_Click(object sender, EventArgs e)
        {
            this.Hide();
            Form1 form1 = new Form1();
            form1.Show();
        }
    }
}

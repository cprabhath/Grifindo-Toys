using System;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace Grifindo_Toys
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        MyConnection db = new MyConnection();
        SqlCommand cmd = new SqlCommand();

        private void btn_Exit_Click(object sender, EventArgs e)
        {
            DialogResult dialogResult = MessageBox.Show("Are you sure want to exit?", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if(dialogResult == DialogResult.Yes)
            {
                Application.Exit();
            }
        }

        private void btn_Login_Click(object sender, EventArgs e)
        {
            if (textBox1.Text == "" || textBox2.Text == "")
            {
                MessageBox.Show("Username or Password is Empty", "Error", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                String query1 = "SELECT * FROM admin WHERE username='" + textBox1.Text + "' AND password='" + textBox2.Text + "'";

                db.conn.Open();
                cmd = new SqlCommand(query1, db.conn);
                SqlDataReader sqlDataReader = cmd.ExecuteReader();

                if (sqlDataReader.HasRows)
                {
                    sqlDataReader.Read();
                    this.Hide();
                    Admin_Dashboard admin_Dashboard1 = new Admin_Dashboard();
                    admin_Dashboard1.Show();
                }
                else
                {
                    MessageBox.Show("Invalid username or password", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                db.conn.Close();
            }
           
        }
    }
}

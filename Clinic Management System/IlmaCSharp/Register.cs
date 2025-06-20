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

namespace IlmaCSharp
{
    public partial class Register : Form
    {
        private string connectionString = @"Data Source=AmjadAzward\SQLEXPRESS;Initial Catalog=clinic;Integrated Security=True";

        public Register()
        {
            InitializeComponent();
        }

        private void Register_Load(object sender, EventArgs e)
        {
            guna2TextBox2.Text = "Enter Name";
            guna2TextBox2.ForeColor = Color.Gray;
            guna2TextBox2.GotFocus += guna2TextBox2_GotFocus;
            guna2TextBox2.LostFocus += guna2TextBox2_LostFocus;
            
            guna2TextBox1.Text = "Enter Email";
            guna2TextBox1.ForeColor = Color.Gray;
            guna2TextBox1.GotFocus += guna2TextBox1_GotFocus;
            guna2TextBox1.LostFocus += guna2TextBox1_LostFocus;

            guna2TextBox3.Text = "Enter Username";
            guna2TextBox3.ForeColor = Color.Gray;
            guna2TextBox3.GotFocus += guna2TextBox3_GotFocus;
            guna2TextBox3.LostFocus += guna2TextBox3_LostFocus;

            guna2TextBox4.Text = "Create Password";
            guna2TextBox4.ForeColor = Color.Gray;
            guna2TextBox4.GotFocus += guna2TextBox4_GotFocus;
            guna2TextBox4.LostFocus += guna2TextBox4_LostFocus;

            guna2TextBox5.Text = "Re-enter Password";
            guna2TextBox5.ForeColor = Color.Gray;
            guna2TextBox5.GotFocus += guna2TextBox5_GotFocus;
            guna2TextBox5.LostFocus += guna2TextBox5_LostFocus;
        }

        private void guna2TextBox2_GotFocus(object sender, EventArgs e)
        {
            if (guna2TextBox2.Text == "Enter Name")
            {
                guna2TextBox2.Text = "";
                guna2TextBox2.ForeColor = Color.Black;
            }
        }

        private void guna2TextBox2_LostFocus(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(guna2TextBox2.Text))
            {
                guna2TextBox2.Text = "Enter Name";
                guna2TextBox2.ForeColor = Color.Gray;
            }
        }

        private void guna2TextBox1_GotFocus(object sender, EventArgs e)
        {
            if (guna2TextBox1.Text == "Enter Email")
            {
                guna2TextBox1.Text = "";
                guna2TextBox1.ForeColor = Color.Black;
            }
        }

        private void guna2TextBox1_LostFocus(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(guna2TextBox1.Text))
            {
                guna2TextBox1.Text = "Enter Email";
                guna2TextBox1.ForeColor = Color.Gray;
            }
        }

        private void guna2TextBox3_GotFocus(object sender, EventArgs e)
        {
            if (guna2TextBox3.Text == "Enter Username")
            {
                guna2TextBox3.Text = "";
                guna2TextBox3.ForeColor = Color.Black;
            }
        }

        private void guna2TextBox3_LostFocus(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(guna2TextBox3.Text))
            {
                guna2TextBox3.Text = "Enter Username";
                guna2TextBox3.ForeColor = Color.Gray;
            }
        }

        private void guna2TextBox4_GotFocus(object sender, EventArgs e)
        {
            if (guna2TextBox4.Text == "Create Password")
            {
                guna2TextBox4.Text = "";
                guna2TextBox4.ForeColor = Color.Black;
            }
        }

        private void guna2TextBox4_LostFocus(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(guna2TextBox4.Text))
            { 
                guna2TextBox4.Text = "Create Password";
                guna2TextBox4.ForeColor = Color.Gray;
            }
        }

        private void guna2TextBox5_GotFocus(object sender, EventArgs e)
        {
            if (guna2TextBox5.Text == "Re-enter Password")
            {
                guna2TextBox5.Text = "";
                guna2TextBox5.ForeColor = Color.Black;
            }
        }

        private void guna2TextBox5_LostFocus(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(guna2TextBox5.Text))
            {
                guna2TextBox5.Text = "Re-enter Password";
                guna2TextBox5.ForeColor = Color.Gray;
            }
        }



        private void guna2HtmlLabel5_Click(object sender, EventArgs e)
        {
            Login my = new Login();
            my.Show();
            this.Hide();
        }

        private void guna2CircleButton1_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            string username = guna2TextBox3.Text.Trim();
            string password = guna2TextBox4.Text.Trim();
            string confirmPassword = guna2TextBox5.Text.Trim();
            string email = guna2TextBox1.Text.Trim();
            string fullName = guna2TextBox2.Text.Trim();

            if (username == "Enter Username" || password == "Create Password" ||
              confirmPassword == "Re-enter Password" || email == "Enter Email" || fullName == "Enter Name" ||
              string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(password) ||
              string.IsNullOrWhiteSpace(confirmPassword) || string.IsNullOrWhiteSpace(email) ||
              string.IsNullOrWhiteSpace(fullName))
            {
                MessageBox.Show("All fields are required.", "Registration Failed", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (password != confirmPassword)
            {
                MessageBox.Show("Passwords do not match.", "Registration Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            try
            {
                if (IsUsernameTaken(username))
                {
                    MessageBox.Show("Username is already taken. Please choose another.", "Registration Failed", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                RegisterUser(username, password, email, fullName);
                MessageBox.Show("Registration successful!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

                Login login = new Login();
                login.Show();
                this.Hide();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private bool IsUsernameTaken(string username)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = "SELECT COUNT(1) FROM Users WHERE Username = @Username";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@Username", username);

                conn.Open();
                int count = (int)cmd.ExecuteScalar();
                return count > 0;
            }
        }

        private void RegisterUser(string username, string password, string email, string fullName)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = "INSERT INTO Users (Username, UPassword, UEmail, UName) VALUES (@Username, @UPassword, @UEmail, @UName)";
                SqlCommand cmd = new SqlCommand(query, conn);

                cmd.Parameters.AddWithValue("@Username", username);
                cmd.Parameters.AddWithValue("@UPassword", password);  // Use hashing for real-world scenarios
                cmd.Parameters.AddWithValue("@UEmail", email);
                cmd.Parameters.AddWithValue("@UName", fullName);

                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }

        private void guna2PictureBox3_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void guna2TextBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void guna2TextBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void guna2TextBox3_TextChanged(object sender, EventArgs e)
        {

        }

        private void guna2TextBox5_TextChanged(object sender, EventArgs e)
        {

        }

        private void guna2TextBox4_TextChanged(object sender, EventArgs e)
        {

        }
    }
}

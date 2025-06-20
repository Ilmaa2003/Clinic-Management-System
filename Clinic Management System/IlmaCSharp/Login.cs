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
    public partial class Login : Form
    {
        private string connectionString = @"Data Source=AmjadAzward\SQLEXPRESS;Initial Catalog=clinic;Integrated Security=True";

        public Login()
        {
            InitializeComponent();
        }

        private void Login_Load(object sender, EventArgs e)
        {
            guna2TextBox3.Text = "Enter Username";
            guna2TextBox3.ForeColor = Color.Gray;
            guna2TextBox3.GotFocus += guna2TextBox3_GotFocus;
            guna2TextBox3.LostFocus += guna2TextBox3_LostFocus;

            guna2TextBox5.Text = "Enter Password";
            guna2TextBox5.ForeColor = Color.Gray;
            guna2TextBox5.GotFocus += guna2TextBox5_GotFocus;
            guna2TextBox5.LostFocus += guna2TextBox5_LostFocus;
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

        private void guna2TextBox5_GotFocus(object sender, EventArgs e)
        {
            if (guna2TextBox5.Text == "Enter Password")
            {
                guna2TextBox5.Text = "";
                guna2TextBox5.ForeColor = Color.Black;
            }
        }

        private void guna2TextBox5_LostFocus(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(guna2TextBox5.Text))
            {
                guna2TextBox5.Text = "Enter Password";
                guna2TextBox5.ForeColor = Color.Gray;
            }
        }

        private void guna2HtmlLabel5_Click(object sender, EventArgs e)
        {
            Register my = new Register();
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
            string password = guna2TextBox5.Text.Trim();

            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
            {
                MessageBox.Show("Please enter both Username and Password.", "Login Failed", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    string query = "SELECT * FROM Users WHERE Username = @Username AND UPassword = @Password";
                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@Username", username);
                    cmd.Parameters.AddWithValue("@Password", password);

                    SqlDataReader reader = cmd.ExecuteReader();
                    if (reader.Read())
                    {
                        GlobalUserData.Username = reader["Username"].ToString();
                        GlobalUserData.FullName = reader["UName"].ToString();
                        GlobalUserData.Email = reader["UEmail"].ToString();
                        GlobalUserData.Password = reader["UPassword"].ToString();
                        GlobalUserData.ConfirmPassword = reader["UPassword"].ToString(); 
                        Dashboard dashboard = new Dashboard();
                        dashboard.Show();
                        this.Hide(); 
                    }
                    else
                    {
                        MessageBox.Show("Invalid Username or Password.", "Login Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public static class GlobalUserData
        {
            public static string Username { get; set; }
            public static string FullName { get; set; }
            public static string Email { get; set; }
            public static string Password { get; set; }
            public static string ConfirmPassword { get; set; }
        }

        private bool LoginUser(string username, string password)
        {
            bool isAuthenticated = false;

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = "SELECT COUNT(1) FROM Users WHERE Username = @Username AND UPassword = @UPassword";
                SqlCommand cmd = new SqlCommand(query, conn);

                cmd.Parameters.AddWithValue("@Username", username);
                cmd.Parameters.AddWithValue("@UPassword", password);  // Hash password in real applications

                conn.Open();
                int count = (int)cmd.ExecuteScalar();
                isAuthenticated = (count == 1);
            }

            return isAuthenticated;
        }

        private void guna2PictureBox3_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

      

        private void guna2TextBox3_TextChanged(object sender, EventArgs e)
        {

        }

        private void guna2TextBox5_TextChanged(object sender, EventArgs e)
        {

        }
    }
}

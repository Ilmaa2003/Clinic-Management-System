using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ListView;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;
using static IlmaCSharp.Login;

namespace IlmaCSharp
{
    public partial class User : Form
    {
        private string connectionString = @"Data Source=AmjadAzward\SQLEXPRESS;Initial Catalog=clinic;Integrated Security=True";


        public User()
        {
            InitializeComponent();
        }

        private void guna2Button6_Click(object sender, EventArgs e)
        {
            DialogResult confirmResult = MessageBox.Show
               (
                "Are you sure you want to log out?",
                "Confirm Logout",
                 MessageBoxButtons.YesNo,
                 MessageBoxIcon.Question);

            if (confirmResult == DialogResult.Yes)
            {
                Login my = new Login();
                my.Show();
                this.Hide();
            }
        }

        private void guna2PictureBox4_Click(object sender, EventArgs e)
        {
            Dashboard my = new Dashboard();
            my.Show();
            this.Hide();
        }

        private void guna2PictureBox3_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void User_Load(object sender, EventArgs e)
        {
            txtUsername.Text = GlobalUserData.Username;
            txtFullName.Text = GlobalUserData.FullName;
            txtEmail.Text = GlobalUserData.Email;
            txtPassword.Text = GlobalUserData.Password;
            txtConfirmPassword.Text = GlobalUserData.ConfirmPassword;

            string saveFolder = @"C:\Users\USER\AMJAD\amjudata";
            string userImagePath = Path.Combine(saveFolder, $"{GlobalUserData.Username}.jpg");

            if (File.Exists(userImagePath))
            {
                // Load image safely using FileStream
                using (FileStream fs = new FileStream(userImagePath, FileMode.Open, FileAccess.Read))
                {
                    guna2PictureBox5.Image = Image.FromStream(fs);
                }
            }
            else
            {
                guna2PictureBox5.Image = null;
            }
        }

        private void guna2Button8_Click(object sender, EventArgs e)
        {
            string username = txtUsername.Text.Trim();
            string fullName = txtFullName.Text.Trim();
            string email = txtEmail.Text.Trim();
            string password = txtPassword.Text.Trim();
            string confirmPassword = txtConfirmPassword.Text.Trim();

            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(fullName) || string.IsNullOrEmpty(email) || string.IsNullOrEmpty(password) || string.IsNullOrEmpty(confirmPassword))
            {
                MessageBox.Show("Please fill in all the fields.", "Update Failed", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (password != confirmPassword)
            {
                MessageBox.Show("Passwords do not match.", "Update Failed", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                if (UpdateUserDetails(username, fullName, email, password))
                {
                    MessageBox.Show("User details updated successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    GlobalUserData.Username = username;
                    GlobalUserData.FullName = fullName;
                    GlobalUserData.Email = email;
                    GlobalUserData.Password = password;
                    GlobalUserData.ConfirmPassword = confirmPassword;

                    string saveFolder = @"C:\Users\USER\AMJAD\amjudata";

                    string oldImagePath = Path.Combine(saveFolder, $"{GlobalUserData.Username}.jpg");
                    string newImagePath = Path.Combine(saveFolder, $"{username}.jpg");

                    if (File.Exists(oldImagePath))
                    {
                        try
                        {
                            File.Move(oldImagePath, newImagePath);

                            using (FileStream fs = new FileStream(newImagePath, FileMode.Open, FileAccess.Read))
                            {
                                guna2PictureBox5.Image = Image.FromStream(fs);
                            }

                            //MessageBox.Show("Profile image updated and saved successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show($"An error occurred while renaming and saving the image: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                    else
                    {
                        MessageBox.Show("No existing profile image found to rename.", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }          
                else
                {
                    MessageBox.Show("Failed to update user details.", "Update Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private bool UpdateUserDetails(string username, string fullName, string email, string password)
        {
            bool isUpdated = false;

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = "UPDATE Users SET Username = @NewUsername, UName = @FullName, UEmail = @Email, UPassword = @Password WHERE Username = @OldUsername";

                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@NewUsername", username);  
                cmd.Parameters.AddWithValue("@FullName", fullName);
                cmd.Parameters.AddWithValue("@Email", email);
                cmd.Parameters.AddWithValue("@Password", password);  
                cmd.Parameters.AddWithValue("@OldUsername", GlobalUserData.Username);  

                try
                {
                    conn.Open();
                    int rowsAffected = cmd.ExecuteNonQuery();
                    isUpdated = (rowsAffected > 0);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"An error occurred while updating: {ex.Message}", "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }

            return isUpdated;
        }

        private void guna2Button1_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Image Files|*.jpg;*.png";

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                string imagePath = openFileDialog.FileName;
                string saveFolder = @"C:\Users\USER\AMJAD\amjudata";
                Directory.CreateDirectory(saveFolder);

                string newImagePath = Path.Combine(saveFolder, $"{GlobalUserData.Username}.jpg");

                try
                {
                    guna2PictureBox5.Image = null;

                    if (File.Exists(newImagePath))
                    {
                        File.Delete(newImagePath);
                    }


                    using (FileStream fs = new FileStream(imagePath, FileMode.Open, FileAccess.Read))
                    {
                        using (FileStream destStream = new FileStream(newImagePath, FileMode.Create, FileAccess.Write))
                        {
                            fs.CopyTo(destStream);
                        }
                    }

                    using (FileStream fs = new FileStream(newImagePath, FileMode.Open, FileAccess.Read))
                    {
                        guna2PictureBox5.Image = Image.FromStream(fs);
                    }

                    MessageBox.Show("New image uploaded and replaced successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"An error occurred: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }

        }

     


        private void guna2PictureBox5_Click(object sender, EventArgs e)
        {

        }

        private void guna2Panel1_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
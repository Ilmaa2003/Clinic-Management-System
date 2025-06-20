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
using System.Xml.Linq;
using Guna.UI2.WinForms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ListView;
using static IlmaCSharp.Doctor;

namespace IlmaCSharp
{
    public partial class Doctor : Form
    {
        private Doctors doctor = new Doctors();

        public class Doctors
        {
            private string connectionString = @"Data Source=AmjadAzward\SQLEXPRESS;Initial Catalog=clinic;Integrated Security=True";


            public int DoctorID { get; set; }
            public string DName { get; set; }
            public string Specialization { get; set; }
            public string Contact { get; set; }
            public string Email { get; set; }

            public void AddDoctor()
            {
                try
                {
                    using (SqlConnection connection = new SqlConnection(connectionString))
                    {
                        connection.Open();
                        string query = "INSERT INTO Doctors (DName, Specialization, Contact, Email) VALUES (@DName, @Specialization, @Contact, @Email)";
                        using (SqlCommand cmd = new SqlCommand(query, connection))
                        {
                            cmd.Parameters.AddWithValue("@DName", DName);
                            cmd.Parameters.AddWithValue("@Specialization", Specialization);
                            cmd.Parameters.AddWithValue("@Contact", Contact);
                            cmd.Parameters.AddWithValue("@Email", Email);
                            cmd.ExecuteNonQuery();
                        }
                    }
                    MessageBox.Show("Doctor added successfully.");
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error: {ex.Message}");
                }
            }


            public void UpdateDoctor()
            {
                try
                {
                  


                    using (SqlConnection connection = new SqlConnection(connectionString))
                    {
                        connection.Open();
                        string query = "UPDATE Doctors SET DName = @DName, Specialization = @Specialization, Contact = @Contact, Email = @Email WHERE DoctorID = @DoctorID";
                        using (SqlCommand cmd = new SqlCommand(query, connection))
                        {
                            cmd.Parameters.AddWithValue("@DoctorID", DoctorID);
                            cmd.Parameters.AddWithValue("@DName", DName);
                            cmd.Parameters.AddWithValue("@Specialization", Specialization);
                            cmd.Parameters.AddWithValue("@Contact", Contact);
                            cmd.Parameters.AddWithValue("@Email", Email);
                            cmd.ExecuteNonQuery();
                        }
                    }
                    MessageBox.Show("Doctor updated successfully.");
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error: {ex.Message}");
                }
            }

            public void DeleteDoctor()
            {
                DialogResult dialogResult = MessageBox.Show("Are you sure you want to delete this doctor and all related data?", "Confirm Delete", MessageBoxButtons.YesNo);
                if (dialogResult == DialogResult.Yes)
                {
                    try
                    {
                        using (SqlConnection conn = new SqlConnection(connectionString))
                        {
                            string deleteAppointmentsQuery = "DELETE FROM Appointments WHERE DoctorID = @DoctorID";
                            SqlCommand cmdAppointments = new SqlCommand(deleteAppointmentsQuery, conn);
                            cmdAppointments.Parameters.AddWithValue("@DoctorID", DoctorID);

                            string deletePrescriptionsQuery = "DELETE FROM Prescriptions WHERE DoctorID = @DoctorID";
                            SqlCommand cmdPrescriptions = new SqlCommand(deletePrescriptionsQuery, conn);
                            cmdPrescriptions.Parameters.AddWithValue("@DoctorID", DoctorID);

                            conn.Open();
                            cmdAppointments.ExecuteNonQuery(); 
                            cmdPrescriptions.ExecuteNonQuery(); 

                            string deleteDoctorQuery = "DELETE FROM Doctors WHERE DoctorID = @DoctorID";
                            SqlCommand cmdDoctor = new SqlCommand(deleteDoctorQuery, conn);
                            cmdDoctor.Parameters.AddWithValue("@DoctorID", DoctorID);

                            cmdDoctor.ExecuteNonQuery(); 
                            conn.Close();
                        }

                        MessageBox.Show("Doctor and all related data deleted successfully.");
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Error: {ex.Message}");
                    }
                }
            }


            public DataTable LoadDoctors()
            {
                DataTable dt = new DataTable();
                try
                {
                    using (SqlConnection connection = new SqlConnection(connectionString))
                    {
                        connection.Open();
                        string query = "SELECT * FROM Doctors";
                        using (SqlDataAdapter da = new SqlDataAdapter(query, connection))
                        {
                            da.Fill(dt);
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error: {ex.Message}");
                }
                return dt;
            }
        }


        public Doctor()
        {
            InitializeComponent();
            LoadDoctors();
        }

        private void LoadDoctors()
        {
            dgvDoctors.DataSource = doctor.LoadDoctors();
        }


        private void guna2Button5_Click(object sender, EventArgs e)
        {
            Prescription my = new Prescription();
            my.Show();
            this.Hide();
        }

        private void guna2Button4_Click(object sender, EventArgs e)
        {
            Payment my = new Payment();
            my.Show();
            this.Hide();
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

        private void guna2Button2_Click(object sender, EventArgs e)
        {
            Appointment my = new Appointment();
            my.Show();
            this.Hide();
        }

        private void guna2Button3_Click(object sender, EventArgs e)
        {
            Patient my = new Patient();
            my.Show();
            this.Hide();
        }

        private void guna2Button1_Click(object sender, EventArgs e)
        {

        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            Dashboard my = new Dashboard();
            my.Show();
            this.Hide();
        }

        private void guna2PictureBox4_Click(object sender, EventArgs e)
        {
            Dashboard my = new Dashboard();
            my.Show();
            this.Hide();
        }

        private void guna2PictureBox2_Click(object sender, EventArgs e)
        {
            User my = new User();
            my.Show();
            this.Hide();
        }

        private void guna2PictureBox3_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void guna2TextBox5_TextChanged(object sender, EventArgs e)
        {

        }

        private void guna2TextBox3_TextChanged(object sender, EventArgs e)
        {

        }

        private void guna2TextBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void guna2TextBox4_TextChanged(object sender, EventArgs e)
        {

        }

        
     
        private void Doctor_Load(object sender, EventArgs e)
        {
            dgvDoctors.Columns[0].Width = 60; 
            dgvDoctors.Columns[1].Width = 70;
            dgvDoctors.Columns[2].Width = 80;
            dgvDoctors.Columns[3].Width = 70;
            dgvDoctors.Columns[4].Width = 120;

            InitializeSearchBox();
            SetWatermarks();

            dgvDoctors.ClearSelection();

        }

        private void SetWatermarks()
        {

            guna2TextBox5.Text = "Enter Doctor Name";
            guna2TextBox5.ForeColor = Color.Gray;
            guna2TextBox5.GotFocus += guna2TextBox5_GotFocus;
            guna2TextBox5.LostFocus += guna2TextBox5_LostFocus;


            guna2TextBox2.Text = "Enter Contact Number";
            guna2TextBox2.ForeColor = Color.Gray;
            guna2TextBox2.GotFocus += guna2TextBox2_GotFocus;
            guna2TextBox2.LostFocus += guna2TextBox2_LostFocus;

            guna2TextBox4.Text = "Enter Email";
            guna2TextBox4.ForeColor = Color.Gray;
            guna2TextBox4.GotFocus += guna2TextBox4_GotFocus;
            guna2TextBox4.LostFocus += guna2TextBox4_LostFocus;

            guna2TextBox1.Text = "Search or type a command";
            guna2TextBox1.ForeColor = Color.Gray;
            guna2TextBox1.GotFocus += guna2TextBox1_GotFocus;
            guna2TextBox1.LostFocus += guna2TextBox1_LostFocus;
        }

        private void guna2TextBox5_GotFocus(object sender, EventArgs e)
        {
            if (guna2TextBox5.Text == "Enter Doctor Name")
            {
                guna2TextBox5.Text = "";
                guna2TextBox5.ForeColor = Color.Black;
            }
        }

        private void guna2TextBox2_GotFocus(object sender, EventArgs e)
        {
            if (guna2TextBox2.Text == "Enter Contact Number")
            {
                guna2TextBox2.Text = "";
                guna2TextBox2.ForeColor = Color.Black;
            }
        }

        private void guna2TextBox4_GotFocus(object sender, EventArgs e)
        {
            if (guna2TextBox4.Text == "Enter Email")
            {
                guna2TextBox4.Text = "";
                guna2TextBox4.ForeColor = Color.Black;
            }
        }

        private void guna2TextBox5_LostFocus(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(guna2TextBox5.Text))
            {
                guna2TextBox5.Text = "Enter Doctor Name";
                guna2TextBox5.ForeColor = Color.Gray;
            }
        }

        private void guna2TextBox2_LostFocus(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(guna2TextBox2.Text))
            {
                guna2TextBox2.Text = "Enter Contact Number";
                guna2TextBox2.ForeColor = Color.Gray;
            }
        }

        private void guna2TextBox4_LostFocus(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(guna2TextBox4.Text))
            {
                guna2TextBox4.Text = "Enter Email";
                guna2TextBox4.ForeColor = Color.Gray;
            }
        }

        private void guna2TextBox1_GotFocus(object sender, EventArgs e)
        {
            if (guna2TextBox1.Text == "Search or type a command")
            {
                guna2TextBox1.Text = "";
                guna2TextBox1.ForeColor = Color.Black;
            }
        }

        private void guna2TextBox1_LostFocus(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(guna2TextBox1.Text))
            {
                guna2TextBox1.Text = "Search or type a command";
                guna2TextBox1.ForeColor = Color.Gray;
            }
        }
        private void guna2TextBox1_TextChanged(object sender, EventArgs e)
        {

        }


        private void guna2Button7_Click(object sender, EventArgs e)
        {
           
        }

        private void guna2Panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void upddoc_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(doctor.DName) || doctor.DName == "Enter Doctor Name")
            {
                MessageBox.Show("Please enter a valid doctor name.");
                return;
            }

            if (string.IsNullOrWhiteSpace(doctor.Specialization) || doctor.Specialization == "Select Specialization")
            {
                MessageBox.Show("Please select a specialization.");
                return;
            }

            if (string.IsNullOrWhiteSpace(guna2TextBox2.Text) || !guna2TextBox2.Text.All(char.IsDigit) || guna2TextBox2.Text.Length != 10 || guna2TextBox2.Text == "Enter Contact Number")
            {
                MessageBox.Show("Please enter a valid contact number.");
                return;
            }

            if (IsContactNumberExists(doctor.Contact))
            {
                MessageBox.Show("This contact number is already in use. Please enter a different contact number.");
                return;
            }

            if (string.IsNullOrWhiteSpace(doctor.Email) || doctor.Email == "Enter Email")
            {
                MessageBox.Show("Please enter a valid email.");
                return;
            }

            if (IsEmailExists(doctor.Email))
            {
                MessageBox.Show("This email is already in use. Please enter a different email.");
                return;
            }



            if (dgvDoctors.SelectedRows.Count > 0)
            {
                doctor.DoctorID = Convert.ToInt32(dgvDoctors.SelectedRows[0].Cells[0].Value);
                doctor.DName = guna2TextBox5.Text;
                doctor.Specialization = guna2ComboBox1.SelectedItem?.ToString(); 
                doctor.Contact = guna2TextBox2.Text;
                doctor.Email = guna2TextBox4.Text;

                doctor.UpdateDoctor(); 
                LoadDoctors();
           
            }
            else
            {
                MessageBox.Show("Please select a doctor to update.");
            }
            ClearFields();
            SetWatermarks();

        }

        private void deldoc_Click(object sender, EventArgs e)
        {
            if (dgvDoctors.SelectedRows.Count > 0)
            {
                doctor.DoctorID = Convert.ToInt32(dgvDoctors.SelectedRows[0].Cells[0].Value);
                doctor.DeleteDoctor();
                LoadDoctors(); 
                ClearFields();
                SetWatermarks();
            }
            else
            {
                MessageBox.Show("Please select a doctor to delete.");
            }
        }

        private void adddoc_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(guna2TextBox5.Text) || guna2TextBox5.Text == "Enter Doctor Name")
            {
                MessageBox.Show("Please enter a valid Doctor Name.");
                return;
            }
            doctor.DName = guna2TextBox5.Text;

            doctor.Specialization = guna2ComboBox1.Text;
            if (string.IsNullOrWhiteSpace(doctor.Specialization) || doctor.Specialization == "Select Specialization")
            {
                MessageBox.Show("Please select a valid Specialization.");
                return;
            }

            if (string.IsNullOrWhiteSpace(guna2TextBox2.Text) || !guna2TextBox2.Text.All(char.IsDigit) || guna2TextBox2.Text.Length != 10 || guna2TextBox2.Text == "Enter Contact Number")
            {
                MessageBox.Show("Please enter a valid number.");
                return;
            }
            doctor.Contact = guna2TextBox2.Text;

            if (IsContactNumberExists(doctor.Contact))
            {
                MessageBox.Show("This contact number is already in use. Please enter a different contact number.");
                return;
            }


            if (string.IsNullOrWhiteSpace(guna2TextBox4.Text) || guna2TextBox4.Text == "Enter Email")
            {
                MessageBox.Show("Please enter a valid Email.");
                return;
            }

            if (IsEmailExists(doctor.Email))
            {
                MessageBox.Show("This email is already in use. Please enter a different email.");
                return;
            }
          


            doctor.Email = guna2TextBox4.Text;

            doctor.AddDoctor();

            LoadDoctors();

            ClearFields();

            SetWatermarks();
        }

        private string connectionString = @"Data Source=AmjadAzward\SQLEXPRESS;Initial Catalog=clinic;Integrated Security=True";

        private bool IsContactNumberExists(string contact)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string query = "SELECT COUNT(*) FROM Doctors WHERE Contact = @Contact";
                using (SqlCommand cmd = new SqlCommand(query, connection))
                {
                    cmd.Parameters.AddWithValue("@Contact", contact);
                    int count = (int)cmd.ExecuteScalar();
                    return count > 0; // Returns true if contact exists
                }
            }
        }

        private bool IsEmailExists(string email)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string query = "SELECT COUNT(*) FROM Doctors WHERE Email = @Email";
                using (SqlCommand cmd = new SqlCommand(query, connection))
                {
                    cmd.Parameters.AddWithValue("@Email", email);
                    int count = (int)cmd.ExecuteScalar();
                    return count > 0; // Returns true if email exists
                }
            }
        }


        private void ClearFields()
        {
            guna2TextBox5.Clear();
            guna2TextBox2.Clear();
            guna2TextBox4.Clear();
            guna2ComboBox1.SelectedIndex = -1;
        }

        private void dgvDoctors_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0) 
            {
                DataGridViewRow row = dgvDoctors.Rows[e.RowIndex];
                guna2TextBox5.Text = row.Cells["DName"].Value?.ToString() ?? ""; 
                guna2ComboBox1.SelectedItem = row.Cells["Specialization"].Value?.ToString() ?? ""; 
                guna2TextBox2.Text = row.Cells["Contact"].Value?.ToString() ?? ""; 
                guna2TextBox4.Text = row.Cells["Email"].Value?.ToString() ?? "";
            }
        }

        private void guna2Button7_Click_1(object sender, EventArgs e)
        {
            NavigateToForm(guna2TextBox1.Text.Trim());
        }

        private void NavigateToForm(string formName)
        {
            if (guna2TextBox1.Text == "Search or type a command" || string.IsNullOrEmpty(formName))
            {
                MessageBox.Show("Please enter a form name.", "Search Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            Form targetForm = null;

            switch (formName.ToLower())
            {
                case "dashboard":
                    targetForm = new Dashboard();
                    break;
                case "user":
                    targetForm = new User();
                    break;
                case "appointment":
                    targetForm = new Appointment();
                    break;
                case "patient":
                    targetForm = new Patient();
                    break;
                case "doctor":
                    targetForm = new Doctor();
                    break;
                case "payment":
                    targetForm = new Payment();
                    break;
                case "prescription":
                    targetForm = new Prescription();
                    break;

                default:
                    MessageBox.Show($"Form '{formName}' not found.", "Search Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    guna2TextBox1.Clear();
                    return;
            }

            if (targetForm != null)
            {
                targetForm.Show();
                this.Hide();
            }
        }

        private void InitializeSearchBox()
        {
            var availableForms = new List<string>
          {
                "Dashboard",
                "User",
                "Appointment",
                "Patient",
                "Doctor",
                "Payment",
                "Prescription"
           };

            guna2TextBox1.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            guna2TextBox1.AutoCompleteSource = AutoCompleteSource.CustomSource;

            var autoCompleteCollection = new AutoCompleteStringCollection();
            autoCompleteCollection.AddRange(availableForms.ToArray());
            guna2TextBox1.AutoCompleteCustomSource = autoCompleteCollection;

            guna2TextBox1.KeyDown += guna2TextBox1_KeyDown;
        }

        private void guna2TextBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;
                NavigateToForm(guna2TextBox1.Text.Trim());
            }
        }

    }
    
}

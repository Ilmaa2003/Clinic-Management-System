using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;
using Guna.UI2.WinForms;
using static IlmaCSharp.Doctor;
using static IlmaCSharp.Patient;

namespace IlmaCSharp
{

    public partial class Patient : Form
    {
        private Patients patient = new Patients();


        public class Patients
        {
            private string connectionString = @"Data Source=AmjadAzward\SQLEXPRESS;Initial Catalog=clinic;Integrated Security=True";


            public int PatientID { get; set; }
            public string PName { get; set; }
            public int Age { get; set; }
            public string Contact { get; set; }
            public string PAddress { get; set; }
            public string Gender { get; set; }


            public void Add()
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    string query = "INSERT INTO Patients (PName, Age, Contact, PAddress, Gender) VALUES (@PName, @Age, @Contact, @PAddress, @Gender)";
                    SqlCommand cmd = new SqlCommand(query, conn);

                    cmd.Parameters.AddWithValue("@PName", PName);
                    cmd.Parameters.AddWithValue("@Age", Age);
                    cmd.Parameters.AddWithValue("@Contact", Contact);
                    cmd.Parameters.AddWithValue("@PAddress", PAddress);
                    cmd.Parameters.AddWithValue("@Gender", Gender);

                    conn.Open();
                    cmd.ExecuteNonQuery();
                    conn.Close();
                }

                MessageBox.Show("Patient added successfully.");
            }

            public void Update()
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {



                    string query = "UPDATE Patients SET PName = @PName, Age = @Age, Contact = @Contact, PAddress = @PAddress, Gender = @Gender WHERE PatientID = @PatientID";
                    SqlCommand cmd = new SqlCommand(query, conn);

                    cmd.Parameters.AddWithValue("@PatientID", PatientID);
                    cmd.Parameters.AddWithValue("@PName", PName);
                    cmd.Parameters.AddWithValue("@Age", Age);
                    cmd.Parameters.AddWithValue("@Contact", Contact);
                    cmd.Parameters.AddWithValue("@PAddress", PAddress);
                    cmd.Parameters.AddWithValue("@Gender", Gender);

                    conn.Open();
                    cmd.ExecuteNonQuery();
                    conn.Close();

                }
                    MessageBox.Show("Patient updated successfully.");
                

            }


            public void Delete()
            {
                DialogResult dialogResult = MessageBox.Show("Are you sure you want to delete this patient?", "Confirm Delete", MessageBoxButtons.YesNo);
                if (dialogResult == DialogResult.Yes)
                {
                    try
                    {
                        using (SqlConnection conn = new SqlConnection(connectionString))
                        {
                            string deleteAppointmentsQuery = "DELETE FROM Appointments WHERE PatientID = @PatientID";
                            SqlCommand cmdAppointments = new SqlCommand(deleteAppointmentsQuery, conn);
                            cmdAppointments.Parameters.AddWithValue("@PatientID", PatientID);

                            string deletePaymentsQuery = "DELETE FROM Payments WHERE PatientID = @PatientID";
                            SqlCommand cmdPayments = new SqlCommand(deletePaymentsQuery, conn);
                            cmdPayments.Parameters.AddWithValue("@PatientID", PatientID);

                            string deletePrescriptionsQuery = "DELETE FROM Prescriptions WHERE PatientID = @PatientID";
                            SqlCommand cmdPrescriptions = new SqlCommand(deletePrescriptionsQuery, conn);
                            cmdPrescriptions.Parameters.AddWithValue("@PatientID", PatientID);

                            conn.Open();
                            cmdAppointments.ExecuteNonQuery();
                            cmdPayments.ExecuteNonQuery();
                            cmdPrescriptions.ExecuteNonQuery();

                            string deletePatientQuery = "DELETE FROM Patients WHERE PatientID = @PatientID";
                            SqlCommand cmdPatient = new SqlCommand(deletePatientQuery, conn);
                            cmdPatient.Parameters.AddWithValue("@PatientID", PatientID);

                            cmdPatient.ExecuteNonQuery();
                            conn.Close();
                        }

                        MessageBox.Show("Patient and all related records deleted successfully.");
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Error: {ex.Message}");
                    }
                }
            }




            public static DataTable GetAllPatients()
            {
                DataTable dt = new DataTable();
                try
                {
                    using (SqlConnection connection = new SqlConnection(@"Data Source=AmjadAzward\SQLEXPRESS;Initial Catalog=clinic;Integrated Security=True"))
                    {
                        connection.Open();
                        string query = "SELECT * FROM Patients";
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

        private string GetSelectedGender()
        {
            if (RadioButtonMale.Checked)
                return "Male";
            else if (RadioButtonFemale.Checked)
                return "Female";
            else
                return "";
        }

        private void LoadPatients()
        {
            dgvPatients.DataSource = Patients.GetAllPatients();
        }


        public Patient()
        {
            InitializeComponent();
        }

        private void Patient_Load(object sender, EventArgs e)
        {
            LoadPatients();
            dgvPatients.Columns[0].Width = 60;
            dgvPatients.Columns[1].Width = 70;
            dgvPatients.Columns[2].Width = 30;
            dgvPatients.Columns[3].Width = 70;
            dgvPatients.Columns[4].Width = 120;

            dgvPatients.Columns[1].HeaderText = "Name";
            dgvPatients.Columns[4].HeaderText = "Address";
            SetWatermarks();
            InitializeSearchBox();

            dgvPatients.ClearSelection();


        }

        private void SetWatermarks()
        {
            guna2TextBox1.Text = "Search or type a command";
            guna2TextBox1.ForeColor = Color.Gray;
            guna2TextBox1.GotFocus += guna2TextBox1_GotFocus;
            guna2TextBox1.LostFocus += guna2TextBox1_LostFocus;

            txtPName.Text = "Enter Patient Name";
            txtPName.ForeColor = Color.Gray;
            txtPName.GotFocus += txtPName_GotFocus;
            txtPName.LostFocus += txtPName_LostFocus;


            txtAge.Text = "Enter Age";
            txtAge.ForeColor = Color.Gray;
            txtAge.GotFocus += txtAge_GotFocus;
            txtAge.LostFocus += txtAge_LostFocus;

            txtPAddress.Text = "Enter Address";
            txtPAddress.ForeColor = Color.Gray;
            txtPAddress.GotFocus += txtPAddress_GotFocus;
            txtPAddress.LostFocus += txtPAddress_LostFocus;


            txtContact.Text = "Enter Contact";
            txtContact.ForeColor = Color.Gray;
            txtContact.GotFocus += txtContact_GotFocus;
            txtContact.LostFocus += txtContact_LostFocus;
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

        private void txtPName_GotFocus(object sender, EventArgs e)
        {
            if (txtPName.Text == "Enter Patient Name")
            {
                txtPName.Text = ""; 
                txtPName.ForeColor = Color.Black; 
            }
        }

        private void txtPName_LostFocus(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtPName.Text)) 
            {
                txtPName.Text = "Enter Patient Name"; 
                txtPName.ForeColor = Color.Gray; 
            }
        }

        private void txtAge_GotFocus(object sender, EventArgs e)
        {
            if (txtAge.Text == "Enter Age")
            {
                txtAge.Text = "";
                txtAge.ForeColor = Color.Black;
            }
        }

        private void txtAge_LostFocus(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtAge.Text))
            {
                txtAge.Text = "Enter Age";
                txtAge.ForeColor = Color.Gray;
            }
        }

        private void txtPAddress_GotFocus(object sender, EventArgs e)
        {
            if (txtPAddress.Text == "Enter Address")
            {
                txtPAddress.Text = "";
                txtPAddress.ForeColor = Color.Black;
            }
        }

        private void txtPAddress_LostFocus(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtPAddress.Text))
            {
                txtPAddress.Text = "Enter Address";
                txtPAddress.ForeColor = Color.Gray;
            }
        }

        private void txtContact_GotFocus(object sender, EventArgs e)
        {
            if (txtContact.Text == "Enter Contact")
            {
                txtContact.Text = "";
                txtContact.ForeColor = Color.Black;
            }
        }

        private void txtContact_LostFocus(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtContact.Text))
            {
                txtContact.Text = "Enter Contact";
                txtContact.ForeColor = Color.Gray;
            }
        }



        private void guna2PictureBox3_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void guna2PictureBox2_Click(object sender, EventArgs e)
        {
            User my = new User();
            my.Show();
            this.Hide();
        }

        private void guna2PictureBox4_Click(object sender, EventArgs e)
        {
            Dashboard my = new Dashboard();
            my.Show();
            this.Hide();
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            Dashboard my = new Dashboard();
            my.Show();
            this.Hide();
        }

        private void guna2Button1_Click(object sender, EventArgs e)
        {
            Doctor my = new Doctor();
            my.Show();
            this.Hide();
        }

        private void guna2Button2_Click(object sender, EventArgs e)
        {
            Appointment my = new Appointment();
            my.Show();
            this.Hide();
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

        private void guna2TextBox1_TextChanged(object sender, EventArgs e)
        {

        }


        private void updppat_Click(object sender, EventArgs e)
        {
            if (dgvPatients.SelectedRows.Count > 0)
            {
                patient.PatientID = Convert.ToInt32(dgvPatients.SelectedRows[0].Cells[0].Value);

                if (string.IsNullOrWhiteSpace(txtPName.Text) || txtPName.Text == "Enter Patient Name")
                {
                    MessageBox.Show("Please enter a valid Patient Name.");
                    return;
                }
                patient.PName = txtPName.Text;

                if (!int.TryParse(txtAge.Text, out int age) || age <= 0)
                {
                    MessageBox.Show("Please enter a valid Age.");
                    return;
                }
                patient.Age = age;

                patient.Gender = GetSelectedGender();
                if (string.IsNullOrWhiteSpace(patient.Gender))
                {
                    MessageBox.Show("Please select a valid Gender.");
                    return;
                }

                if (string.IsNullOrWhiteSpace(txtContact.Text) || !txtContact.Text.All(char.IsDigit) || txtContact.Text.Length != 10 || txtContact.Text == "Enter Contact")
                {
                    MessageBox.Show("Please enter a valid Contact Number.");
                    return;
                }

                patient.Contact = txtContact.Text;

                if (IsContactNumberExists(patient.Contact))
                {
                    MessageBox.Show("This contact number is already in use. Please enter a different contact number.");
                    return;
                }

                if (string.IsNullOrWhiteSpace(txtPAddress.Text) || txtPAddress.Text == "Enter Address")
                {
                    MessageBox.Show("Please enter a valid Address.");
                    return;
                }
                patient.PAddress = txtPAddress.Text;

                patient.Update();
                LoadPatients();
                ClearFields();
                SetWatermarks();
            }
            else
            {
                MessageBox.Show("Please select a patient to update.");
            }
        }

        private void addpat_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtPName.Text) || txtPName.Text == "Enter Patient Name")
            {
                MessageBox.Show("Please enter a valid Patient Name.");
                return;
            }
            patient.PName = txtPName.Text;

            if (!int.TryParse(txtAge.Text, out int age) || age <= 0)
            {
                MessageBox.Show("Please enter a valid Age.");
                return;
            }
            patient.Age = age;

            patient.Gender = GetSelectedGender();
            if (string.IsNullOrWhiteSpace(patient.Gender))
            {
                MessageBox.Show("Please select a valid Gender.");
                return;
            }

            if (string.IsNullOrWhiteSpace(txtContact.Text) || !txtContact.Text.All(char.IsDigit) || txtContact.Text.Length != 10 || txtContact.Text == "Enter Contact")
            {
                MessageBox.Show("Please enter a valid Contact Number.");
                return;
            }
            patient.Contact = txtContact.Text;

            if (IsContactNumberExists(patient.Contact))
            {
                MessageBox.Show("This contact number is already in use. Please enter a different contact number.");
                return;
            }


            if (string.IsNullOrWhiteSpace(txtPAddress.Text) || txtPAddress.Text == "Enter Address")
            {
                MessageBox.Show("Please enter a valid Address.");
                return;
            }
            patient.PAddress = txtPAddress.Text;

            patient.Add();
            LoadPatients();
            ClearFields();
            SetWatermarks();
        }

        private string connectionString = @"Data Source=AmjadAzward\SQLEXPRESS;Initial Catalog=clinic;Integrated Security=True";


        private bool IsContactNumberExists(string contactNumber)
        {

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string checkQuery = "SELECT COUNT(*) FROM Patients WHERE Contact = @Contact";
                SqlCommand checkCmd = new SqlCommand(checkQuery, conn);
                checkCmd.Parameters.AddWithValue("@Contact", contactNumber);

                conn.Open();
                int count = (int)checkCmd.ExecuteScalar();
                conn.Close();

                return count > 0;
            }
        }

        private void delpat_Click(object sender, EventArgs e)
        {
            if (dgvPatients.SelectedRows.Count > 0)
            {
                patient.PatientID = Convert.ToInt32(dgvPatients.SelectedRows[0].Cells[0].Value);
                patient.Delete();
                LoadPatients();
                ClearFields();
            }
            else
            {
                MessageBox.Show("Please select a patient to delete.");
            }
        }

        private void dgvPatients_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
           
        }
        private void ClearFields()
        {
            txtPName.Clear(); 
            RadioButtonMale.Checked = false;
            RadioButtonFemale.Checked = false; 
            txtAge.Clear(); 
            txtContact.Clear(); 
            txtPAddress.Clear(); 
        }

        private void dgvPatients_CellContentClick_1(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dgvPatients.Rows[e.RowIndex];

                txtPName.Text = row.Cells["PName"].Value?.ToString() ?? "";
                string gender = row.Cells["Gender"].Value?.ToString() ?? "";
                if (gender == "Male")
                {
                    RadioButtonMale.Checked = true;
                    RadioButtonFemale.Checked = false;
                }
                else if (gender == "Female")
                {
                    RadioButtonFemale.Checked = true;
                    RadioButtonMale.Checked = false;
                }
                else
                {
                    RadioButtonMale.Checked = false;
                    RadioButtonFemale.Checked = false;
                }

                txtAge.Text = row.Cells["Age"].Value?.ToString() ?? "";
                txtContact.Text = row.Cells["Contact"].Value?.ToString() ?? "";
                txtPAddress.Text = row.Cells["PAddress"].Value?.ToString() ?? "";
            }
        }

        private void guna2Button7_Click(object sender, EventArgs e)
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

        private void txtPName_TextChanged(object sender, EventArgs e)
        {

        }
    }
}

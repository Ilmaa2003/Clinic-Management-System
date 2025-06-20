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
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using CrystalDecisions.Windows.Forms;

namespace IlmaCSharp
{
    public partial class Prescription : Form
    {
        private string connectionString = @"Data Source=AmjadAzward\SQLEXPRESS;Initial Catalog=clinic;Integrated Security=True";

        public Prescription()
        {
            InitializeComponent();
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

        private void guna2Button3_Click(object sender, EventArgs e)
        {
            Patient my = new Patient();
            my.Show();
            this.Hide();
        }

        private void guna2Button2_Click(object sender, EventArgs e)
        {
            Appointment my = new Appointment();
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

        private void guna2PictureBox3_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void guna2Button5_Click(object sender, EventArgs e)
        {

        }

        private void guna2Button4_Click(object sender, EventArgs e)
        {
            Payment my = new Payment();
            my.Show();
            this.Hide();
        }

        private void Prescription_Load(object sender, EventArgs e)
        {
            LoadPatientsIntoComboBox();
            LoadDoctorsIntoComboBox();
            LoadPrescriptionsIntoGridView();
            ClearFields();


            dgvPrescriptions.Columns[0].Width = 80;
            dgvPrescriptions.Columns[1].Width = 30;
            dgvPrescriptions.Columns[2].Width = 30;
            dgvPrescriptions.Columns[3].Width = 70;
            dgvPrescriptions.Columns[4].Width = 50;
            dgvPrescriptions.Columns[5].Width = 60;


            dgvPrescriptions.Columns[1].HeaderText = "PID";
            dgvPrescriptions.Columns[2].HeaderText = "DID";
            dgvPrescriptions.Columns[3].HeaderText = "Medicine";
            dgvPrescriptions.Columns[6].HeaderText = "Date";


            guna2TextBox1.Text = "Search or type a command";
            guna2TextBox1.ForeColor = Color.Gray;
            guna2TextBox1.GotFocus += guna2TextBox1_GotFocus;
            guna2TextBox1.LostFocus += guna2TextBox1_LostFocus;

            InitializeSearchBox();

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

        private void LoadPatientsIntoComboBox()
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = "SELECT PatientID, PName FROM Patients";
                SqlDataAdapter da = new SqlDataAdapter(query, conn);
                DataTable dt = new DataTable();
                da.Fill(dt);

                cmbpat.DataSource = dt;
                cmbpat.DisplayMember = "PName";
                cmbpat.ValueMember = "PatientID";
            }
        }

        private void LoadDoctorsIntoComboBox()
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = "SELECT DoctorID, DName FROM Doctors";
                SqlDataAdapter da = new SqlDataAdapter(query, conn);
                DataTable dt = new DataTable();
                da.Fill(dt);

                cmbdat.DataSource = dt;
                cmbdat.DisplayMember = "DName";
                cmbdat.ValueMember = "DoctorID";
            }
        }

        private void LoadPrescriptionsIntoGridView()
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = "SELECT PrescriptionID, PatientID, DoctorID, MedicationName, Dosage, Duration, PrescriptionDate " +
                               "FROM Prescriptions";
                SqlDataAdapter da = new SqlDataAdapter(query, conn);
                DataTable dt = new DataTable();
                da.Fill(dt);

                dgvPrescriptions.DataSource = dt;
            }
        }


        private void ClearFields()
        {
            cmbpat.SelectedIndex = -1;
            cmbdat.SelectedIndex = -1;
            cmbMedication.SelectedIndex = -1;
            numDosage.Value = 0;
            cmbDuration.SelectedIndex = -1;
            dtpPrescriptionDate.Value = DateTime.Now;
        }


        private void AddPrescription()
        {
            // Validate each field separately
            if (cmbpat.SelectedIndex == -1)
            {
                MessageBox.Show("Please select a valid patient.");
                return;
            }

            if (cmbdat.SelectedIndex == -1)
            {
                MessageBox.Show("Please select a doctor.");
                return;
            }

            if (cmbMedication.SelectedIndex == -1)
            {
                MessageBox.Show("Please select a medication.");
                return;
            }

            if (cmbDuration.SelectedIndex == -1)
            {
                MessageBox.Show("Please select a duration.");
                return;
            }

            // Ensure dosage is valid (greater than 0)
            if (numDosage.Value <= 0)
            {
                MessageBox.Show("Please enter a valid dosage.");
                return;
            }

            // Ensure the prescription date is valid (not in the past)
            if (dtpPrescriptionDate.Value.Date < DateTime.Now.Date)
            {
                MessageBox.Show("Prescription date cannot be in the past.");
                return;
            }

            // Proceed with adding the prescription if all fields are valid
            int patientID = Convert.ToInt32(cmbpat.SelectedValue);
            int doctorID = Convert.ToInt32(cmbdat.SelectedValue);
            string medicationName = cmbMedication.SelectedItem.ToString();
            decimal dosage = numDosage.Value;
            string duration = cmbDuration.SelectedItem.ToString();
            DateTime prescriptionDate = dtpPrescriptionDate.Value;

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    string query = "INSERT INTO Prescriptions (PatientID, DoctorID, MedicationName, Dosage, Duration, PrescriptionDate) " +
                                   "VALUES (@PatientID, @DoctorID, @MedicationName, @Dosage, @Duration, @PrescriptionDate)";
                    SqlCommand cmd = new SqlCommand(query, conn);

                    cmd.Parameters.AddWithValue("@PatientID", patientID);
                    cmd.Parameters.AddWithValue("@DoctorID", doctorID);
                    cmd.Parameters.AddWithValue("@MedicationName", medicationName);
                    cmd.Parameters.AddWithValue("@Dosage", dosage);
                    cmd.Parameters.AddWithValue("@Duration", duration);
                    cmd.Parameters.AddWithValue("@PrescriptionDate", prescriptionDate);

                    conn.Open();
                    cmd.ExecuteNonQuery();
                    conn.Close();
                }

                MessageBox.Show("Prescription added successfully.");
                LoadPrescriptionsIntoGridView(); // Reload prescriptions after adding
                ClearFields(); // Clear fields after adding
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred while adding the prescription: {ex.Message}");
            }
        }

        private void addpr_Click(object sender, EventArgs e)
        {
            AddPrescription();
        }


        private void UpdatePrescription()
        {
            // Validate that all fields are selected
            if (cmbpat.SelectedIndex == -1)
            {
                MessageBox.Show("Please select a patient.");
                return;
            }

            if (cmbdat.SelectedIndex == -1)
            {
                MessageBox.Show("Please select a doctor.");
                return;
            }

            if (cmbMedication.SelectedIndex == -1)
            {
                MessageBox.Show("Please select a medication.");
                return;
            }

            if (cmbDuration.SelectedIndex == -1)
            {
                MessageBox.Show("Please select a duration.");
                return;
            }

            // Validate dosage
            if (numDosage.Value <= 0)
            {
                MessageBox.Show("Please enter a valid dosage.");
                return;
            }

            // Validate prescription date
            if (dtpPrescriptionDate.Value.Date < DateTime.Now.Date)
            {
                MessageBox.Show("Prescription date cannot be in the past.");
                return;
            }

            // Ensure a row is selected in the DataGridView
            if (dgvPrescriptions.SelectedRows.Count == 0)
            {
                MessageBox.Show("Please select a prescription to update.");
                return;
            }

            // Retrieve Prescription ID from the selected row
            int prescriptionID = Convert.ToInt32(dgvPrescriptions.SelectedRows[0].Cells["PrescriptionID"].Value);
            int patientID = Convert.ToInt32(cmbpat.SelectedValue);
            int doctorID = Convert.ToInt32(cmbdat.SelectedValue);
            string medicationName = cmbMedication.SelectedItem.ToString();
            decimal dosage = numDosage.Value;
            string duration = cmbDuration.SelectedItem.ToString();
            DateTime prescriptionDate = dtpPrescriptionDate.Value;

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    string query = "UPDATE Prescriptions SET PatientID = @PatientID, DoctorID = @DoctorID, " +
                                   "MedicationName = @MedicationName, Dosage = @Dosage, Duration = @Duration, " +
                                   "PrescriptionDate = @PrescriptionDate WHERE PrescriptionID = @PrescriptionID";

                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@PatientID", patientID);
                    cmd.Parameters.AddWithValue("@DoctorID", doctorID);
                    cmd.Parameters.AddWithValue("@MedicationName", medicationName);
                    cmd.Parameters.AddWithValue("@Dosage", dosage);
                    cmd.Parameters.AddWithValue("@Duration", duration);
                    cmd.Parameters.AddWithValue("@PrescriptionDate", prescriptionDate);
                    cmd.Parameters.AddWithValue("@PrescriptionID", prescriptionID);

                    conn.Open();
                    cmd.ExecuteNonQuery();
                    conn.Close();
                }

                MessageBox.Show("Prescription updated successfully.");
                LoadPrescriptionsIntoGridView(); // Reload prescriptions after updating
                ClearFields(); // Clear fields after updating
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred while updating the prescription: {ex.Message}");
            }
        }

        private void updpr_Click(object sender, EventArgs e)
        {
            UpdatePrescription();
        }


        private void DeletePrescription()
        {
            // Ensure a row is selected in the DataGridView
            if (dgvPrescriptions.SelectedRows.Count == 0)
            {
                MessageBox.Show("Please select a prescription to delete.");
                return;
            }

            // Retrieve Prescription ID from the selected row
            int prescriptionID = Convert.ToInt32(dgvPrescriptions.SelectedRows[0].Cells["PrescriptionID"].Value);

            DialogResult result = MessageBox.Show("Are you sure you want to delete this prescription?",
                                                  "Confirm Deletion", MessageBoxButtons.YesNo);

            if (result == DialogResult.Yes)
            {
                try
                {
                    using (SqlConnection conn = new SqlConnection(connectionString))
                    {
                        string query = "DELETE FROM Prescriptions WHERE PrescriptionID = @PrescriptionID";
                        SqlCommand cmd = new SqlCommand(query, conn);
                        cmd.Parameters.AddWithValue("@PrescriptionID", prescriptionID);

                        conn.Open();
                        cmd.ExecuteNonQuery();
                        conn.Close();
                    }

                    MessageBox.Show("Prescription deleted successfully.");
                    LoadPrescriptionsIntoGridView(); // Reload prescriptions after deleting
                    ClearFields(); // Clear fields after deletion
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"An error occurred while deleting the prescription: {ex.Message}");
                }
            }
        }

        private void delpr_Click(object sender, EventArgs e)
        {
            DeletePrescription();
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

        private void dgvPrescriptions_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                int prescriptionID = Convert.ToInt32(dgvPrescriptions.Rows[e.RowIndex].Cells["PrescriptionID"].Value);

                cmbpat.SelectedValue = dgvPrescriptions.Rows[e.RowIndex].Cells["PatientID"].Value;
                cmbdat.SelectedValue = dgvPrescriptions.Rows[e.RowIndex].Cells["DoctorID"].Value;
                cmbMedication.SelectedItem = dgvPrescriptions.Rows[e.RowIndex].Cells["MedicationName"].Value.ToString();
                numDosage.Value = Convert.ToDecimal(dgvPrescriptions.Rows[e.RowIndex].Cells["Dosage"].Value);
                cmbDuration.SelectedItem = dgvPrescriptions.Rows[e.RowIndex].Cells["Duration"].Value.ToString();
                dtpPrescriptionDate.Value = Convert.ToDateTime(dgvPrescriptions.Rows[e.RowIndex].Cells["PrescriptionDate"].Value);
            }
        }



        private void guna2Button10_Click(object sender, EventArgs e)
        {
            if (dgvPrescriptions.SelectedRows.Count > 0)
            {
                int prescriptionId = Convert.ToInt32(dgvPrescriptions.SelectedRows[0].Cells[0].Value);

              //  MessageBox.Show($"Selected Prescription ID: {prescriptionId}");

                Form2 reportViewer = new Form2(prescriptionId);
                reportViewer.Show();
            }
            else
            {
                MessageBox.Show("Please select a Prescription ID from the list.");
            }
        }
    }

        
}

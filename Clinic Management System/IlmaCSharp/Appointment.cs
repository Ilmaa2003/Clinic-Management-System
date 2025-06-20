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
using static IlmaCSharp.Doctor;
using static IlmaCSharp.Patient;

namespace IlmaCSharp
{
    public partial class Appointment : Form
    {
        private string connectionString = @"Data Source=AmjadAzward\SQLEXPRESS;Initial Catalog=clinic;Integrated Security=True";

        public Appointment()
        {
            InitializeComponent();
            LoadPatientsIntoComboBox();
            LoadDoctorsIntoComboBox();
        }
        private void LoadPatientsIntoComboBox()
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = "SELECT PatientID, PName FROM Patients";
                SqlDataAdapter da = new SqlDataAdapter(query, conn);
                DataTable dt = new DataTable();
                da.Fill(dt);

                cmbPatients.DataSource = dt;
                cmbPatients.DisplayMember = "PatientID";  
                cmbPatients.ValueMember = "PatientID";   
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

                cmbDoctors.DataSource = dt;
                cmbDoctors.DisplayMember = "DoctorID";  // Display doctor name
                cmbDoctors.ValueMember = "DoctorID"; // Keep the value as doctor ID
            }
        }

        private void cmbPatients_SelectedIndexChanged(object sender, EventArgs e)
        {
           
        }

        private void cmbDoctors_SelectedIndexChanged(object sender, EventArgs e)
        {
           
        }


        private string GetPatientNameByID(int patientID)
        {
            string patientName = "";
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = "SELECT PName FROM Patients WHERE PatientID = @PatientID";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@PatientID", patientID);

                conn.Open();
                patientName = cmd.ExecuteScalar()?.ToString() ?? ""; 
                conn.Close();
            }
            return patientName;
        }

       
        private string GetDoctorNameByID(int doctorID)
        {
            string doctorName = "";
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = "SELECT DName FROM Doctors WHERE DoctorID = @DoctorID";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@DoctorID", doctorID);

                conn.Open();
                doctorName = cmd.ExecuteScalar()?.ToString() ?? ""; 
                conn.Close();
            }
            return doctorName;
        }

       


       


  


        public DataTable LoadAppointments()
        {
            DataTable dt = new DataTable();
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    string query = "SELECT a.AppointmentID, a.PatientID, a.DoctorID, a.AppointmentDate, a.AppointmentTime " +
                                   "FROM Appointments a";
                    SqlDataAdapter da = new SqlDataAdapter(query, conn);
                    da.Fill(dt);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}");
            }
            return dt;
        }



        private void LoadAppointmentsIntoDGV()
        {
            dgvAppointments.DataSource = LoadAppointments();
            foreach (DataGridViewRow row in dgvAppointments.Rows)
            {
                if (row.Cells["AppointmentTime"].Value is TimeSpan time)
                {
                    row.Cells["AppointmentTime"].Value = time.ToString(@"hh\:mm\:ss");
                }
            }
        }


        private void guna2HtmlLabel3_Click(object sender, EventArgs e)
        {

        }

        private void guna2Button4_Click(object sender, EventArgs e)
        {
            Payment my = new Payment();
            my.Show();
            this.Hide();
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

        private void guna2Button3_Click(object sender, EventArgs e)
        {
            Patient my = new Patient();
            my.Show();
            this.Hide();
        }

        private void guna2Button2_Click(object sender, EventArgs e)
        {

        }

        private void guna2Button5_Click(object sender, EventArgs e)
        {
            Prescription my = new Prescription();
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

        private void guna2TextBox3_TextChanged(object sender, EventArgs e)
        {

        }

        private void guna2TextBox4_TextChanged(object sender, EventArgs e)
        {

        }

        private void guna2TextBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void Appointment_Load(object sender, EventArgs e)
        {
            ClearFields();
            LoadAppointmentsIntoDGV();

            guna2TextBox1.Text = "Search or type a command";
            guna2TextBox1.ForeColor = Color.Gray;
            guna2TextBox1.GotFocus += guna2TextBox1_GotFocus;
            guna2TextBox1.LostFocus += guna2TextBox1_LostFocus;

            dgvAppointments.Columns[0].Width = 100;
           // dgvAppointments.Columns[1].Width = 70;
           // dgvAppointments.Columns[2].Width = 30;
            dgvAppointments.Columns[3].Width = 100;
            dgvAppointments.Columns[4].Width = 100;

            dgvAppointments.Columns[3].HeaderText = "Date";
            dgvAppointments.Columns[4].HeaderText = "Time";

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

        private void guna2Panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void delapp_Click(object sender, EventArgs e)
        {
            if (dgvAppointments.SelectedRows.Count == 0)
            {
                MessageBox.Show("Please select an appointment to delete");
                return;
            }

            int appointmentID = Convert.ToInt32(dgvAppointments.SelectedRows[0].Cells[0].Value);

            DialogResult result = MessageBox.Show(
                "Are you sure you want to delete this appointment?",
                "Confirm Delete", MessageBoxButtons.YesNo);

            if (result == DialogResult.Yes)
            {
                try
                {
                    DeleteAppointment(appointmentID);
                    LoadAppointmentsIntoDGV();
                    ClearFields();
                    MessageBox.Show("Appointment successfully deleted");
                }
                catch (Exception ex)
                {
                    MessageBox.Show("An error occurred while deleting the appointment: " + ex.Message,
                                    "Error",
                                    MessageBoxButtons.OK,
                                    MessageBoxIcon.Error);
                }
            }
        }

        public void DeleteAppointment(int appointmentID)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    string query = "DELETE FROM Appointments WHERE AppointmentID = @AppointmentID";
                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@AppointmentID", appointmentID);
                    conn.Open();
                    cmd.ExecuteNonQuery();
                    conn.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred while deleting the appointment: " + ex.Message,
                                "Database Error",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Error);
            }
        }
        private void updapp_Click(object sender, EventArgs e)
        {
            try
            {
                if (dgvAppointments.SelectedRows.Count == 0)
                {
                    MessageBox.Show("Please select an appointment to update.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                if (cmbPatients.SelectedValue == null)
                {
                    MessageBox.Show("Please select a valid Patient.");
                    return;
                }

                if (cmbDoctors.SelectedValue == null)
                {
                    MessageBox.Show("Please select a valid Doctor.");
                    return;
                }

                if (dtpAppointmentDate.Value.Date < DateTime.Today)
                {
                    MessageBox.Show("Appointment date cannot be in the past.");
                    return;
                }

                TimeSpan appointmentTime = dtpAppointmentTime.Value.TimeOfDay;

                if (appointmentTime <= DateTime.Now.TimeOfDay)
                {
                    MessageBox.Show("Appointment time cannot be the current time or in the past.");
                    return;
                }

                int appointmentID = Convert.ToInt32(dgvAppointments.SelectedRows[0].Cells["AppointmentID"].Value);

                int patientID = Convert.ToInt32(cmbPatients.SelectedValue);
                int doctorID = Convert.ToInt32(cmbDoctors.SelectedValue);
                DateTime appointmentDate = dtpAppointmentDate.Value.Date;
               // TimeSpan appointmentTimeValue = dtpAppointmentTime.Value.TimeOfDay;


                if (IsAppointmentExist(patientID, doctorID, appointmentDate, appointmentTime))
                {
                    MessageBox.Show("This appointment already exists at the selected time. Please choose a different time.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                UpdateAppointment(appointmentID, patientID, doctorID, appointmentDate, appointmentTime);
                LoadAppointmentsIntoDGV();
                ClearFields();

                MessageBox.Show("Appointment successfully updated.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (SqlException ex)
            {
                MessageBox.Show("Database error: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show("An unexpected error occurred: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public void UpdateAppointment(int appointmentID, int patientID, int doctorID, DateTime appointmentDate, TimeSpan appointmentTime)
        {
            try
            {


                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    string query = @"
                UPDATE Appointments
                SET PatientID = @PatientID,
                    DoctorID = @DoctorID,
                    AppointmentDate = @AppointmentDate,
                    AppointmentTime = @AppointmentTime
                WHERE AppointmentID = @AppointmentID";

                    SqlCommand cmd = new SqlCommand(query, conn);

                    cmd.Parameters.AddRange(new[]
                    {
                new SqlParameter("@AppointmentID", appointmentID),
                new SqlParameter("@PatientID", patientID),
                new SqlParameter("@DoctorID", doctorID),
                new SqlParameter("@AppointmentDate", appointmentDate.Date),
                new SqlParameter("@AppointmentTime", appointmentTime)
            });

                    conn.Open();
                    cmd.ExecuteNonQuery();
                }
            }
            catch (SqlException ex)
            {
                MessageBox.Show("Database error: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show("An unexpected error occurred: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }







        public void AddAppointment(int patientID, int doctorID, DateTime appointmentDate, TimeSpan appointmentTime)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    string query = @"
                       INSERT INTO Appointments (PatientID, DoctorID, AppointmentDate, AppointmentTime) 
                       VALUES (@PatientID, @DoctorID, @AppointmentDate, @AppointmentTime)";

                    SqlCommand cmd = new SqlCommand(query, conn);

                    cmd.Parameters.AddRange(new[]
                    {
                       new SqlParameter("@PatientID", patientID),
                       new SqlParameter("@DoctorID", doctorID),
                       new SqlParameter("@AppointmentDate", appointmentDate.Date),
                       new SqlParameter("@AppointmentTime", appointmentTime)
                      });

                    conn.Open();
                    cmd.ExecuteNonQuery();
                }

            }
            catch (SqlException ex)
            {
                MessageBox.Show("Database error: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show("An unexpected error occurred: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void addapp_Click(object sender, EventArgs e)
        {
            try
            {


                if (cmbPatients.SelectedValue == null)
                {
                    MessageBox.Show("Please select a valid Patient.");
                    return;
                }

                if (cmbDoctors.SelectedValue == null)
                {
                    MessageBox.Show("Please select a valid Doctor.");
                    return;
                }

                if (dtpAppointmentDate.Value.Date < DateTime.Today)
                {
                    MessageBox.Show("Appointment date cannot be in the past.");
                    return;
                }

                TimeSpan appointmentTime = dtpAppointmentTime.Value.TimeOfDay;

                if (appointmentTime <= DateTime.Now.TimeOfDay)
                {
                    MessageBox.Show("Appointment time cannot be the current time or in the past.");
                    return;
                }

                int patientID = Convert.ToInt32(cmbPatients.SelectedValue);
                int doctorID = Convert.ToInt32(cmbDoctors.SelectedValue);
                DateTime appointmentDate = dtpAppointmentDate.Value.Date;
                TimeSpan appointmentTimeValue = dtpAppointmentTime.Value.TimeOfDay;

                // Check if appointment already exists
                if (IsAppointmentExist(patientID, doctorID, appointmentDate, appointmentTimeValue))
                {
                    MessageBox.Show("This appointment already exists at the selected time. Please choose a different time.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                AddAppointment(patientID, doctorID, appointmentDate, appointmentTime);
                LoadAppointmentsIntoDGV();
                ClearFields();
                cmbDoctors.SelectedItem = null;

                MessageBox.Show("Appointment successfully added.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (SqlException ex)
            {
                MessageBox.Show("Database error: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show("An unexpected error occurred: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private bool IsAppointmentExist(int patientID, int doctorID, DateTime appointmentDate, TimeSpan appointmentTime)
        {
            bool exists = false;
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    string query = @"
                    SELECT COUNT(*) 
                    FROM Appointments 
                    WHERE PatientID = @PatientID 
                    AND DoctorID = @DoctorID 
                    AND AppointmentDate = @AppointmentDate 
                    AND AppointmentTime = @AppointmentTime";

                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddRange(new[]
                    {
                        new SqlParameter("@PatientID", patientID),
                        new SqlParameter("@DoctorID", doctorID),
                        new SqlParameter("@AppointmentDate", appointmentDate.Date),
                        new SqlParameter("@AppointmentTime", appointmentTime)
                    });

                    conn.Open();
                    int count = Convert.ToInt32(cmd.ExecuteScalar());
                    if (count > 0)
                    {
                        exists = true;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error checking for existing appointment: " + ex.Message);
            }
            return exists;
        }


        private void ClearFields()
        {
            //cmbPatients.SelectedIndex = -1;
            cmbPatients.SelectedItem = null;
            //cmbDoctors.SelectedIndex = -1;
            cmbDoctors.SelectedItem = null;
            txtPatientName.Clear();
            txtDoctorName.Clear();
            dtpAppointmentDate.Value = DateTime.Now.Date;
            dtpAppointmentTime.Value = DateTime.Today.Add(DateTime.Now.TimeOfDay);
        }

        private void dgvAppointments_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dgvAppointments.Rows[e.RowIndex];
                cmbPatients.SelectedValue = row.Cells["PatientID"].Value?.ToString();
                cmbDoctors.SelectedValue = row.Cells["DoctorID"].Value?.ToString();
                dtpAppointmentDate.Value = Convert.ToDateTime(row.Cells["AppointmentDate"].Value);

                TimeSpan appointmentTime = (TimeSpan)row.Cells["AppointmentTime"].Value;
                dtpAppointmentTime.Value = dtpAppointmentDate.Value.Date.Add(appointmentTime);
            }
        }

        private void cmbDoctors_SelectedIndexChanged_1(object sender, EventArgs e)
        {
            if (cmbDoctors.SelectedItem != null)
            {
                DataRowView selectedRow = (DataRowView)cmbDoctors.SelectedItem;
                int doctorID = Convert.ToInt32(selectedRow["DoctorID"]); 
                string doctorName = GetDoctorNameByID(doctorID);
                txtDoctorName.Text = doctorName;
            }
        }

        private void cmbPatients_SelectedIndexChanged_1(object sender, EventArgs e)
        {
            if (cmbPatients.SelectedItem != null)
            {
                DataRowView selectedRow = (DataRowView)cmbPatients.SelectedItem;
                int patientID = Convert.ToInt32(selectedRow["PatientID"]); 
                string patientName = GetPatientNameByID(patientID);
                txtPatientName.Text = patientName;
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

        private void guna2Button10_Click(object sender, EventArgs e)
        {
            rep01 reportViewer = new rep01();
            reportViewer.Show();
        }
    }
}

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
    public partial class Dashboard : Form
    {
        private string connectionString = @"Data Source=AmjadAzward\SQLEXPRESS;Initial Catalog=clinic;Integrated Security=True";

        public Dashboard()
        {
            InitializeComponent();
            HighlightAllAppointments();
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            
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

        private void guna2PictureBox4_Click(object sender, EventArgs e)
        {
            Dashboard userForm = new Dashboard();
            userForm.Show();
            this.Hide();
        }

        private void guna2PictureBox2_Click(object sender, EventArgs e)
        {
            User userForm = new User();
            userForm.Show();
            this.Hide();
        }

        private void guna2PictureBox3_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void guna2TextBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void Dashboard_Load(object sender, EventArgs e)
        {
            guna2TextBox1.Text = "Search or type a command";
            guna2TextBox1.ForeColor = Color.Gray;
            guna2TextBox1.GotFocus += guna2TextBox1_GotFocus;
            guna2TextBox1.LostFocus += guna2TextBox1_LostFocus;

            LoadNextAppointment();
            LoadNextAppointments();
            HighlightAllAppointments();

            int doctorCount = GetDoctorCount();
            labdoc.Text = doctorCount.ToString();

            int patientCount = GetPatientCount();
            labpat.Text = patientCount.ToString();

            decimal totalAmount = GetTotalAmount();
            //labpay.Text = totalAmount.ToString("C");
            labpay.Text = $"Rs {totalAmount:N0}";

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

        private void guna2HtmlLabel1_Click(object sender, EventArgs e)
        {
           
        }
        private void LoadNextAppointment()
        {
            var nextAppointmentDetails = GetNextAppointmentDetails();

            if (nextAppointmentDetails != null)
            {
                label2.Text = $" Appointment Date: {nextAppointmentDetails.AppointmentDate:dd-MM-yyyy}" +
                                       $"\n Doctor: {nextAppointmentDetails.DoctorName}";
            }
            else
            {
                label2.Text = "No upcoming appointments.";
            }
        }

        private AppointmentDetails GetNextAppointmentDetails()
        {
            AppointmentDetails appointmentDetails = null;

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = "SELECT TOP 1 a.AppointmentDate, d.DName " +
                               "FROM Appointments a " +
                               "JOIN Doctors d ON a.DoctorID = d.DoctorID " +
                               "WHERE a.AppointmentDate > @CurrentDate " +
                               "ORDER BY a.AppointmentDate ASC";

                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@CurrentDate", DateTime.Today);

                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();

                if (reader.Read())
                {
                    appointmentDetails = new AppointmentDetails
                    {
                        AppointmentDate = Convert.ToDateTime(reader["AppointmentDate"]),
                        DoctorName = reader["DName"].ToString()
                    };
                }
                conn.Close();
            }

            return appointmentDetails;
        }

        private void LoadNextAppointments()
        {
            var nextAppointmentDetails = GetNextAppointmentDetail();

            if (nextAppointmentDetails != null)
            {
                label3.Text = $" Appointment Date: {nextAppointmentDetails.AppointmentDate:dd-MM-yyyy}" +
                                         $"\n Doctor: {nextAppointmentDetails.DoctorName}";
            }
            else
            {
                label3.Text = "No upcoming appointments.";
            }
        }

        private AppointmentDetails GetNextAppointmentDetail()
        {
            AppointmentDetails appointmentDetails = null;

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = "SELECT TOP 1 a.AppointmentDate, d.DName " +
                               "FROM Appointments a " +
                               "JOIN Doctors d ON a.DoctorID = d.DoctorID " +
                               "WHERE a.AppointmentDate > @CurrentDate " +
                               "AND a.AppointmentDate NOT IN (SELECT TOP 1 AppointmentDate FROM Appointments WHERE AppointmentDate > @CurrentDate ORDER BY AppointmentDate ASC) " +
                               "ORDER BY a.AppointmentDate ASC";

                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@CurrentDate", DateTime.Today);

                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();

                if (reader.Read())
                {
                    appointmentDetails = new AppointmentDetails
                    {
                        AppointmentDate = Convert.ToDateTime(reader["AppointmentDate"]),
                        DoctorName = reader["DName"].ToString()
                    };
                }
                conn.Close();
            }

            return appointmentDetails;
        }



        public class AppointmentDetails
        {
            public DateTime AppointmentDate { get; set; }
            public string DoctorName { get; set; }
        }
        private void HighlightAllAppointments()
        {
            monthCalendar1.RemoveAllBoldedDates();
            List<DateTime> appointmentDates = GetAllAppointmentDates();

            if (appointmentDates.Count > 0)
            {
                foreach (var date in appointmentDates)
                {
                    monthCalendar1.AddBoldedDate(date);
                }
                monthCalendar1.Refresh(); 
            }
            else
            {
               // MessageBox.Show("No upcoming appointments.");
            }
        }

        private List<DateTime> GetAllAppointmentDates()
        {
            List<DateTime> appointmentDates = new List<DateTime>();

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = "SELECT AppointmentDate FROM Appointments WHERE AppointmentDate >= @CurrentDate ORDER BY AppointmentDate ASC";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@CurrentDate", DateTime.Today);

                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    // Add each appointment date to the list
                    appointmentDates.Add(Convert.ToDateTime(reader["AppointmentDate"]).Date);
                }

                conn.Close();
            }

            return appointmentDates;
        }

        private void guna2HtmlLabel1_Click_1(object sender, EventArgs e)
        {

        }

        private void guna2Panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void guna2CircleProgressBar1_ValueChanged(object sender, EventArgs e)
        {

        }

        private void labdoc_Click(object sender, EventArgs e)
        {

        }

        private int GetDoctorCount()
        {
            {
                int doctorCount = 0;

                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    string query = "SELECT COUNT(*) FROM Doctors";  // Get count of doctors from Doctors table

                    SqlCommand cmd = new SqlCommand(query, conn);

                    conn.Open();
                    doctorCount = (int)cmd.ExecuteScalar();  // Get the count as an integer
                    conn.Close();
                }

                return doctorCount;
            }
        }
        private int GetPatientCount()
        {
            int patientCount = 0;

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = "SELECT COUNT(*) FROM Patients";  // Get count of patients from Patients table

                SqlCommand cmd = new SqlCommand(query, conn);

                conn.Open();
                patientCount = (int)cmd.ExecuteScalar();  // Get the count as an integer
                conn.Close();
            }

            return patientCount;
        }

        private decimal GetTotalAmount()
        {
            decimal totalAmount = 0;

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = "SELECT SUM(Amount) FROM Payments";
                SqlCommand cmd = new SqlCommand(query, conn);

                conn.Open();
                object result = cmd.ExecuteScalar();

                if (result != DBNull.Value)
                {
                    totalAmount = Convert.ToDecimal(result);
                }

                conn.Close();
            }

            return totalAmount;
        }

        private void labpay_Click(object sender, EventArgs e)
        {

        }

        private void monthCalendar1_DateChanged(object sender, DateRangeEventArgs e)
        {

        }

        private void guna2HtmlLabel4_Click(object sender, EventArgs e)
        {
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

    }







}



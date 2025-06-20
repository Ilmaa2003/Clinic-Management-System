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
using IlmaCSharp.Properties;

namespace IlmaCSharp
{
    public partial class Payment : Form
    {
        private string connectionString = @"Data Source=AmjadAzward\SQLEXPRESS;Initial Catalog=clinic;Integrated Security=True";

        public Payment()
        {
            InitializeComponent();
        }

        private void guna2HtmlLabel11_Click(object sender, EventArgs e)
        {

        }

        private void guna2Button7_Click(object sender, EventArgs e)
        {

        }

        private void guna2TextBox1_TextChanged(object sender, EventArgs e)
        {

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


        private void guna2TextBox2_GotFocus(object sender, EventArgs e)
        {
            if (Amount.Text == "Enter Amount")
            {
                Amount.Text = "";
                Amount.ForeColor = Color.Black;
            }
        }

        private void guna2TextBox2_LostFocus(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(Amount.Text))
            {
                Amount.Text = "Enter Amount";
                Amount.ForeColor = Color.Gray;
            }
        }


    


        private void Payment_Load(object sender, EventArgs e)
        {
            LoadPatientsIntoComboBox();
            LoadPaymentsIntoGridView();
            ClearFields();

         
            InitializeSearchBox();
            SetWatermarks();
        }

        private void SetWatermarks()
        {
            guna2TextBox1.Text = "Search or type a command";
            guna2TextBox1.ForeColor = Color.Gray;
            guna2TextBox1.GotFocus += guna2TextBox1_GotFocus;
            guna2TextBox1.LostFocus += guna2TextBox1_LostFocus;

            Amount.Text = "Enter Amount";
            Amount.ForeColor = Color.Gray;
            Amount.GotFocus += guna2TextBox2_GotFocus;
            Amount.LostFocus += guna2TextBox2_LostFocus;
        }

        private void LoadPatientsIntoComboBox()
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = "SELECT PatientID, PName FROM Patients";
                SqlDataAdapter da = new SqlDataAdapter(query, conn);
                DataTable dt = new DataTable();
                da.Fill(dt);

                cmbPats.DataSource = dt;
                cmbPats.DisplayMember = "PatientID";
                cmbPats.ValueMember = "PatientID";
            }
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

        private void LoadPaymentsIntoGridView()
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = "SELECT PaymentID, PatientID, Amount, PaymentDate FROM Payments";
                SqlDataAdapter da = new SqlDataAdapter(query, conn);
                DataTable dt = new DataTable();
                da.Fill(dt);

                dgvPayments.DataSource = dt;
            }
        }


        private void guna2HtmlLabel1_Click(object sender, EventArgs e)
        {

        }

        private void guna2Button8_Click(object sender, EventArgs e)
        {

        }

        private void guna2HtmlLabel5_Click(object sender, EventArgs e)
        {

        }

        private void guna2TextBox6_TextChanged(object sender, EventArgs e)
        {

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

        private void guna2Button6_Click(object sender, EventArgs e)
        {
            DialogResult confirmResult = MessageBox.Show
                (
                 "Are you sure you want to log out?",
                 "Confirm Logout",
                  MessageBoxButtons.YesNo,
                  MessageBoxIcon.Question );

            if (confirmResult == DialogResult.Yes)
            {
                Login my = new Login();
                my.Show();
                this.Hide();
            }
        }

        private void guna2Button4_Click(object sender, EventArgs e)
        {

        }

        private void cmbPats_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbPats.SelectedItem != null)
            {
                DataRowView selectedRow = (DataRowView)cmbPats.SelectedItem;
                int patientID = Convert.ToInt32(selectedRow["PatientID"]);
                string patientName = GetPatientNameByID(patientID);
                txtPatName.Text = patientName;
            }
        }

        private void guna2Panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void dgvPayments_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dgvPayments.Rows[e.RowIndex];

                cmbPats.SelectedValue = row.Cells["PatientID"].Value;
                Amount.Text = row.Cells["Amount"].Value.ToString(); 
                dtpPaymentDate.Value = Convert.ToDateTime(row.Cells["PaymentDate"].Value);
            }
        }

        private void ClearFields()
        {
            cmbPats.SelectedItem = null;
            Amount.Clear(); 
            dtpPaymentDate.Value = DateTime.Today;
            txtPatName.Clear();
        }

      
            private void AddPayment()
            {
            try
            {
                if (cmbPats.SelectedIndex == -1)
                {
                    MessageBox.Show("Please select a valid patient");
                    return;
                }

                if (Amount.Text == "Enter Amount" || !decimal.TryParse(Amount.Text, out decimal amount) || amount <= 0)
                {
                    MessageBox.Show("Please enter a valid amount");
                    return;
                }

                int patientID = Convert.ToInt32(cmbPats.SelectedValue);
                DateTime paymentDate = dtpPaymentDate.Value;

                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    string query = "INSERT INTO Payments (PatientID, Amount, PaymentDate) VALUES (@PatientID, @Amount, @PaymentDate)";
                    SqlCommand cmd = new SqlCommand(query, conn);

                    cmd.Parameters.AddWithValue("@PatientID", patientID);
                    cmd.Parameters.AddWithValue("@Amount", amount);
                    cmd.Parameters.AddWithValue("@PaymentDate", paymentDate);

                    conn.Open();
                    cmd.ExecuteNonQuery();
                    conn.Close();
                }

                MessageBox.Show("Payment added successfully.");
                LoadPaymentsIntoGridView();
                ClearFields();
                SetWatermarks();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error adding payment: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void UpdatePayment()
        {
            try
            {
                if (dgvPayments.SelectedRows.Count == 0)
                {
                    MessageBox.Show("Please select a payment to update");
                    return;
                }
                if (cmbPats.SelectedValue == null)
                {
                    MessageBox.Show("Please select a patient.");
                    return;
                }

                if (Amount.Text == "Enter Amount" || !decimal.TryParse(Amount.Text, out decimal amount) || amount <= 0)
                {
                    MessageBox.Show("Please enter a valid amount");
                    return;
                }

                int paymentID = Convert.ToInt32(dgvPayments.SelectedRows[0].Cells["PaymentID"].Value);
                int patientID = Convert.ToInt32(cmbPats.SelectedValue);
                DateTime paymentDate = dtpPaymentDate.Value;

                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    string query = "UPDATE Payments SET PatientID = @PatientID, Amount = @Amount, PaymentDate = @PaymentDate WHERE PaymentID = @PaymentID";
                    SqlCommand cmd = new SqlCommand(query, conn);

                    cmd.Parameters.AddWithValue("@PaymentID", paymentID);
                    cmd.Parameters.AddWithValue("@PatientID", patientID);
                    cmd.Parameters.AddWithValue("@Amount", amount);
                    cmd.Parameters.AddWithValue("@PaymentDate", paymentDate);

                    conn.Open();
                    cmd.ExecuteNonQuery();
                    conn.Close();
                }

                MessageBox.Show("Payment updated successfully.");
                LoadPaymentsIntoGridView();
                ClearFields();
                SetWatermarks();

                cmbPats.SelectedItem = null;

            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error updating payment: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void DeletePayment()
        {
            if (dgvPayments.SelectedRows.Count == 0)
            {
                MessageBox.Show("Please select a payment to delete.");
                return;
            }

            if (cmbPats.SelectedValue == null)
            {
                MessageBox.Show("Please select a valid patient.");
                return;
            }

            DialogResult confirmResult = MessageBox.Show(
                "Are you sure you want to delete this payment?",
                "Confirm Delete",
                MessageBoxButtons.YesNo
            );

            if (confirmResult == DialogResult.Yes)
            {
                int paymentID = Convert.ToInt32(dgvPayments.SelectedRows[0].Cells["PaymentID"].Value);

                try
                {
                    using (SqlConnection conn = new SqlConnection(connectionString))
                    {
                        string query = "DELETE FROM Payments WHERE PaymentID = @PaymentID";
                        SqlCommand cmd = new SqlCommand(query, conn);

                        cmd.Parameters.AddWithValue("@PaymentID", paymentID);

                        conn.Open();
                        cmd.ExecuteNonQuery();
                        conn.Close();
                    }

                    MessageBox.Show("Payment deleted successfully");

                    LoadPaymentsIntoGridView();
                    ClearFields();
                    SetWatermarks();
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error deleting payment: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void addpay_Click(object sender, EventArgs e)
        {
            AddPayment();

        }

        private void delpay_Click(object sender, EventArgs e)
        {
            DeletePayment();
        }

        private void updpay_Click(object sender, EventArgs e)
        {
            UpdatePayment();
        }

        private void guna2Button7_Click_1(object sender, EventArgs e)
        {
            NavigateToForm(guna2TextBox1.Text.Trim());
        }

        private void NavigateToForm(string formName)
        {
            if (guna2TextBox1.Text == "Search or type a command" || string.IsNullOrEmpty(formName) )
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
            Payrep reportViewer = new Payrep();
            reportViewer.Show();
        }
    }
}

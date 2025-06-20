using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Windows.Forms;

namespace IlmaCSharp
{
    public partial class Form2 : Form
    {
        public Form2(int prescriptionId)
        {
            InitializeComponent();
            crystalReportViewer1 = new CrystalReportViewer();
            this.Controls.Add(crystalReportViewer1);
            PrintPrescriptionReport(prescriptionId);
        }

        public void PrintPrescriptionReport(int prescriptionId)
        {
            ReportDocument reportDocument = new ReportDocument();

            try
            {
                string reportPath = @"C:\Users\USER\source\repos\IlmaCSharp\IlmaCSharp\CrystalReport2.rpt";
                reportDocument.Load(reportPath);

                bool parameterExists = false;
                foreach (ParameterFieldDefinition parameter in reportDocument.DataDefinition.ParameterFields)
                {
                    Console.WriteLine("Parameter Name: " + parameter.Name);
                    if (parameter.Name == "PrescriptionID")
                    {
                        parameterExists = true;
                    }
                }

                if (!parameterExists)
                {
                    throw new Exception("The parameter 'PrescriptionID' is not defined in the report.");
                }

                reportDocument.SetParameterValue("PrescriptionID", prescriptionId);

                Console.WriteLine($"Parameter 'PrescriptionID' set to: {prescriptionId}");
              //  MessageBox.Show($"Loading report for PrescriptionID: {prescriptionId}");

                reportDocument.VerifyDatabase();
                crystalReportViewer1.ReportSource = reportDocument;
                crystalReportViewer1.Refresh();
            }
            catch (Exception ex)
            {
                // Show error if any
                MessageBox.Show($"Error loading the report or setting the parameter: {ex.Message}");
            }
        }


        private void crystalReportViewer1_Load(object sender, EventArgs e)
        {
            crystalReportViewer1.Refresh();
        }
    }
}

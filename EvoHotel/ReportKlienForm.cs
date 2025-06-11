using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;
using Microsoft.Reporting.WinForms;

namespace EvoHotel
{
    public partial class ReportKlienForm : Form
    {
        // Connection string to your database
        private string connectionString = "Data Source=LAPTOP-SALMAARA\\SALMALUVIRZA;Initial Catalog=ProjekEvoHotel;Integrated Security=True";

        public ReportKlienForm()
        {
            InitializeComponent();
        }

        private void ReportKlienForm_Load(object sender, EventArgs e)
        {
            // Setup ReportViewer data
            SetupReportViewer();
            // Refresh report to display data
            this.reportViewer1.RefreshReport();
        }

        private void SetupReportViewer()
        {
            try
            {
                string query = @"SELECT KlienID, NamaKlien, Email, NoTelp, Alamat FROM Klien";

                DataTable dt = new DataTable();

                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    SqlDataAdapter da = new SqlDataAdapter(query, conn);
                    da.Fill(dt);
                }

                // Debugging: Menampilkan jumlah baris yang diambil
                Console.WriteLine("Data Klien yang diambil: " + dt.Rows.Count);

                // Debugging: Menampilkan setiap ID Klien yang diambil
                foreach (DataRow row in dt.Rows)
                {
                    Console.WriteLine("ID Klien: " + row["KlienID"].ToString());
                }

                // Pastikan DataTable di-bind dengan benar
                ReportDataSource rds = new ReportDataSource("DataSet1", dt);

                reportViewer1.LocalReport.DataSources.Clear();
                reportViewer1.LocalReport.DataSources.Add(rds);

                // Pastikan path report .rdlc sesuai
                reportViewer1.LocalReport.ReportPath = @"D:\PRAKTIKUM PABD\EvoHotel(CRUD)\EvoHotel\EvoHotel\KlienReport.rdlc";

                reportViewer1.RefreshReport();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading report: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        // Optional: Method to refresh report data
        private void RefreshReportData()
        {
            SetupReportViewer();
        }

        // Optional: Method to export report
        private void ExportReport(string format)
        {
            try
            {
                Warning[] warnings;
                string[] streamids;
                string mimeType;
                string encoding;
                string extension;

                byte[] bytes = reportViewer1.LocalReport.Render(
                    format, null, out mimeType, out encoding, out extension,
                    out streamids, out warnings);

                SaveFileDialog saveDialog = new SaveFileDialog();
                saveDialog.Filter = $"{format.ToUpper()} files (.{extension})|.{extension}";
                saveDialog.DefaultExt = extension;

                if (saveDialog.ShowDialog() == DialogResult.OK)
                {
                    System.IO.File.WriteAllBytes(saveDialog.FileName, bytes);
                    MessageBox.Show($"Report exported successfully to {saveDialog.FileName}");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error exporting report: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Event handler for exporting report to PDF
        private void btnExportPDF_Click(object sender, EventArgs e)
        {
            ExportReport("PDF");
        }

        // Event handler for exporting report to Excel
        private void btnExportExcel_Click(object sender, EventArgs e)
        {
            ExportReport("EXCELOPENXML");
        }

        // Event handler for refreshing the report data
        private void btnRefresh_Click(object sender, EventArgs e)
        {
            RefreshReportData();
        }

        // Event handler for going back to the main form or previous page
        private void btnBack_Click(object sender, EventArgs e)
        {
            FormClient formClient = new FormClient();
            formClient.Show();
            this.Hide();
        }
    }
}

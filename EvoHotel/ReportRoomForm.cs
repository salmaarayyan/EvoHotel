using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;
using Microsoft.Reporting.WinForms;

namespace EvoHotel
{
    public partial class ReportRoomForm : Form
    {
        // Connection string to your database
        Koneksi kn = new Koneksi();
        string strKonek = "";
        //private string connectionString = "Data Source=LAPTOP-SALMAARA\\SALMALUVIRZA;Initial Catalog=ProjekEvoHotel;Integrated Security=True";

        public ReportRoomForm()
        {
            InitializeComponent();
        }

        private void ReportRoomForm_Load(object sender, EventArgs e)
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
                // SQL query to retrieve the required data
                string query = @"
                    SELECT Ruangan.NamaRuangan, Ruangan.Kapasitas, Ruangan.HargaPerJam, Ruangan.Fasilitas, Ruangan.Status,
       Acara.NamaAcara, Klien.NamaKlien
FROM Ruangan
JOIN Acara ON Ruangan.RuanganID = Acara.RuanganID
JOIN Klien ON Acara.KlienID = Klien.KlienID;";

                // Create a DataTable to store the data
                DataTable dt = new DataTable();

                // Use SqlDataAdapter to fill the DataTable with data from the database
                using (SqlConnection conn = new SqlConnection(kn.connectionString()))
                {
                    SqlDataAdapter da = new SqlDataAdapter(query, conn);
                    da.Fill(dt);
                }

                // Create a ReportDataSource
                ReportDataSource rds = new ReportDataSource("DataSet1", dt); // "DataSet1" should match your RDLC dataset name

                // Clear any existing data sources and add the new data source
                reportViewer1.LocalReport.DataSources.Clear();
                reportViewer1.LocalReport.DataSources.Add(rds);

                // Set the path to the report (.rdlc file)
                reportViewer1.LocalReport.ReportPath = @"D:\PRAKTIKUM PABD\EvoHotel(CRUD)\EvoHotel\EvoHotel\RoomReport.rdlc"; // Update the path accordingly

                // Refresh the ReportViewer to show the updated report
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
            FormRoom formRoom = new FormRoom();
            formRoom.Show();
            this.Hide();
        }
    }
}

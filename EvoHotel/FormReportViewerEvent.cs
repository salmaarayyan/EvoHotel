using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;
using Microsoft.Reporting.WinForms;

namespace EvoHotel
{
    public partial class FormReportViewerEvent : Form
    {
        private int? selectedKlienId;

        // Constructor with optional client ID
        public FormReportViewerEvent(int? klienId = null)
        {
            InitializeComponent();
            selectedKlienId = klienId;
        }

        private void FormReportViewerEvent_Load(object sender, EventArgs e)
        {
            SetupReportViewer();
            this.reportViewer1.RefreshReport();
        }

        private void SetupReportViewer()
        {
            string connectionString = "Data Source=LAPTOP-SALMAARA\\SALMALUVIRZA;Initial Catalog=ProjekEvoHotel;Integrated Security=True";
            string query = @"SELECT Acara.AcaraID, 
                                    Acara.NamaAcara,
                                    Acara.Deskripsi,
                                    Acara.JumlahPeserta, 
                                    Acara.TanggalMulai, 
                                    Acara.TanggalSelesai, 
                                    Acara.Status, 
                                    Klien.KlienID, 
                                    Ruangan.RuanganID
                             FROM   Acara 
                             INNER JOIN Klien ON Acara.KlienID = Klien.KlienID 
                             INNER JOIN Ruangan ON Acara.RuanganID = Ruangan.RuanganID";

            if (selectedKlienId.HasValue)
            {
                query += " WHERE Acara.KlienID = @KlienID";
            }

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand(query, conn);
                if (selectedKlienId.HasValue)
                {
                    cmd.Parameters.AddWithValue("@KlienID", selectedKlienId.Value);
                }

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);

                ReportDataSource rds = new ReportDataSource("DataSet1", dt);
                reportViewer1.LocalReport.DataSources.Clear();
                reportViewer1.LocalReport.DataSources.Add(rds);
                reportViewer1.LocalReport.Refresh();
            }
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            FormDashboard formMenu = new FormDashboard();
            formMenu.Show();
            this.Hide();
        }
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

        private void btnExport_Click(object sender, EventArgs e)
        {
            ExportReport("PDF");
        }
    }
}
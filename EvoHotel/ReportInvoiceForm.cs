using Microsoft.Reporting.WinForms;
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

namespace EvoHotel
{
    public partial class ReportInvoiceForm : Form
    {
        private string connectionString = "Data Source=LAPTOP-SALMAARA\\SALMALUVIRZA;Initial Catalog=ProjekEvoHotel;Integrated Security=True";

        public ReportInvoiceForm()
        {
            InitializeComponent();
        }

        private void ReportInvoiceForm_Load(object sender, EventArgs e)
        {
            SetupReportViewer();
            this.reportViewer1.RefreshReport();
        }

        private void SetupReportViewer()
        {
            try
            {
                string query = @"
                SELECT 
                    dp.DataPembayaranID,
                    dp.Total,
                    dp.MetodePembayaran,
                    dp.Tanggal,
                    dp.Status,
                    k.NamaKlien,
                    a.NamaAcara
                FROM DataPembayaran dp
                INNER JOIN Klien k ON dp.KlienID = k.KlienID
                INNER JOIN Pemesanan p ON dp.PemesananID = p.PemesananID
                INNER JOIN Acara a ON p.AcaraID = a.AcaraID;";

                DataTable dt = new DataTable();

                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    SqlDataAdapter da = new SqlDataAdapter(query, conn);
                    da.Fill(dt);
                }

                ReportDataSource rds = new ReportDataSource("InvoiceDataSet", dt);

                reportViewer1.LocalReport.DataSources.Clear();
                reportViewer1.LocalReport.DataSources.Add(rds);
                reportViewer1.LocalReport.ReportPath = @"D:\PRAKTIKUM PABD\EvoHotel(CRUD)\EvoHotel\EvoHotel\InvoiceReport.rdlc";

                reportViewer1.RefreshReport();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading report: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void RefreshReportData()
        {
            SetupReportViewer();
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

        private void btnExportPDF_Click(object sender, EventArgs e)
        {
            ExportReport("PDF");
        }

        private void btnExportExcel_Click(object sender, EventArgs e)
        {
            ExportReport("EXCELOPENXML");
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            RefreshReportData();
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            FormInvoice formInvoice = new FormInvoice();
            formInvoice.Show();
            this.Hide();
        }
    }
}

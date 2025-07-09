using Microsoft.Reporting.Map.WebForms.BingMaps;
using Microsoft.Reporting.WinForms;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Reflection;
using System.Runtime.Remoting.Contexts;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace EvoHotel
{
    public partial class FormReport : Form
    {
        Koneksi kn = new Koneksi();
        string strKonek = "";
        public FormReport()
        {
            InitializeComponent();
            EnsureIndexes(); 
            LoadClients();
            cmbKlien.SelectedIndexChanged += cmbKlien_SelectedIndexChanged;
        }

        private void FormOrder_Load(object sender, EventArgs e)
        {
            SetupReportViewer();
            this.reportViewer.RefreshReport();
        }

        private void cmbKlien_SelectedIndexChanged(object sender, EventArgs e)
        {
            SetupReportViewer();
        }

        // Load all clients into the ComboBox
        private void LoadClients()
        {
            strKonek = kn.connectionString();
            //string connectionString = "Data Source=LAPTOP-SALMAARA\\SALMALUVIRZA;Initial Catalog=ProjekEvoHotel;Integrated Security=True";
            string query = "SELECT KlienID, NamaKlien FROM Klien";

            DataTable dt = new DataTable();
            using (SqlConnection conn = new SqlConnection(kn.connectionString()))
            {
                SqlDataAdapter da = new SqlDataAdapter(query, conn);
                da.Fill(dt);
            }

            cmbKlien.DataSource = dt;
            cmbKlien.DisplayMember = "NamaKlien";
            cmbKlien.ValueMember = "KlienID";
            cmbKlien.SelectedIndex = -1; // No selection by default
        }

        private void EnsureIndexes()
        {
            strKonek = kn.connectionString();
            //string connectionString = "Data Source=LAPTOP-SALMAARA\\SALMALUVIRZA;Initial Catalog=ProjekEvoHotel;Integrated Security=True";
            using (var conn = new SqlConnection(kn.connectionString()))
            {
                conn.Open();
                var indexScript = @"
            IF OBJECT_ID('dbo.Pemesanan', 'U') IS NOT NULL
            BEGIN
                IF NOT EXISTS (SELECT 1 FROM sys.indexes WHERE name = 'idx_Pemesanan_KlienID')
                BEGIN
                    CREATE NONCLUSTERED INDEX idx_Pemesanan_KlienID ON dbo.Pemesanan(KlienID);
                END;

                IF NOT EXISTS (SELECT 1 FROM sys.indexes WHERE name = 'idx_Pemesanan_Tanggal')
                BEGIN
                    CREATE NONCLUSTERED INDEX idx_Pemesanan_Tanggal ON dbo.Pemesanan(Tanggal);
                END;
            END";
                using (var cmd = new SqlCommand(indexScript, conn))
                {
                    cmd.ExecuteNonQuery();
                }
            }
        }

        private void AnaylzeQuery(string sqlquery)
        {
            strKonek = kn.connectionString();
            //string connectionString = "Data Source=LAPTOP-SALMAARA\\SALMALUVIRZA;Initial Catalog=ProjekEvoHotel;Integrated Security=True";
            using (var conn = new SqlConnection(kn.connectionString()))
            {
                conn.InfoMessage += (s, e) => MessageBox.Show(e.Message, "STATISTICS INFO");
                conn.Open();
                var wrapped = $@"
                    SET STATISTICS IO ON;
                    SET STATISTICS TIME ON;
                    {sqlquery};
                    SET STATISTICS IO OFF;
                    SET STATISTICS TIME OFF;";
                using (var cmd = new SqlCommand(wrapped, conn))
                {
                    cmd.ExecuteNonQuery();
                }
            }
        }

        private void AnaylzeQuery_Click(object sender, EventArgs e)
        {
            var heavyQuery = @"SELECT 
                p.PemesananID,
                p.TotalHarga,
                p.Tanggal as TanggalPemesanan,
                p.Status,
                a.NamaAcara,
                a.TanggalMulai,
                a.TanggalSelesai,
                k.NamaKlien,
                k.Email,
                k.NoTelp,
                r.NamaRuangan
            FROM Pemesanan p
            INNER JOIN Acara a ON p.AcaraID = a.AcaraID
            INNER JOIN Klien k ON p.KlienID = k.KlienID
            INNER JOIN Ruangan r ON a.RuanganID = r.RuanganID
            WHERE p.Status = 'Menunggu Konfirmasi'";
            AnaylzeQuery(heavyQuery);
        }

        private void SetupReportViewer()
        {
            strKonek = kn.connectionString();
            //string connectionString = "Data Source=LAPTOP-SALMAARA\\SALMALUVIRZA;Initial Catalog=ProjekEvoHotel;Integrated Security=True";

            string query = @"SELECT 
        p.PemesananID,
        p.TotalHarga,
        p.Tanggal as TanggalPemesanan,
        p.Status,
        a.NamaAcara,
        a.TanggalMulai,
        a.TanggalSelesai,
        k.NamaKlien,
        k.Email,
        k.NoTelp,
        r.NamaRuangan
    FROM Pemesanan p
    INNER JOIN Acara a ON p.AcaraID = a.AcaraID
    INNER JOIN Klien k ON p.KlienID = k.KlienID
    INNER JOIN Ruangan r ON a.RuanganID = r.RuanganID
    WHERE p.Status = 'Menunggu Konfirmasi'";

            DataTable dt = new DataTable();
            using (SqlConnection conn = new SqlConnection(kn.connectionString()))
            {
                SqlCommand cmd = new SqlCommand(query, conn);

                if (cmbKlien.SelectedIndex != -1 && cmbKlien.SelectedValue != null && int.TryParse(cmbKlien.SelectedValue.ToString(), out int klienId))
                {
                    query += " AND p.KlienID = @KlienID";
                    cmd.CommandText = query;
                    cmd.Parameters.AddWithValue("@KlienID", klienId);
                }
                else
                {
                    cmd.CommandText = query;
                }

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dt);
            }

            ReportDataSource rds = new ReportDataSource("DataSet1", dt);

            reportViewer.LocalReport.DataSources.Clear();
            reportViewer.LocalReport.DataSources.Add(rds);

            reportViewer.LocalReport.ReportPath = @"D:\PRAKTIKUM PABD\EvoHotel(CRUD)\EvoHotel\EvoHotel\OrderReport.rdlc";
            reportViewer.RefreshReport();
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            FormOrder formOrder = new FormOrder();
            formOrder.Show();
            this.Hide(); // Hide the current form instead of closing it
        }
    }
}
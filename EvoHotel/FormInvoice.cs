using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;

namespace EvoHotel
{
    public partial class FormInvoice : Form
    {
        private string connectionString = "Data Source=LAPTOP-SALMAARA\\SALMALUVIRZA;Initial Catalog=ProjekEvoHotel;Integrated Security=True";
        public FormInvoice()
        {
            InitializeComponent();
            InitializeComboBox(); // Inisialisasi ComboBox di constructor
            LoadDropdowns();
            LoadData(); // Memuat data saat form dimuat
            comboBoxKlien.SelectedIndexChanged += comboBoxKlien_SelectedIndexChanged;
            comboBoxStatus.SelectedIndexChanged += comboBoxStatus_SelectedIndexChanged;

        }

        private void InitializeComboBox()
        {
            // Inisialisasi ComboBox Status hanya sekali
            comboBoxStatus.Items.Clear();
            comboBoxStatus.Items.Add("Diterima");
            comboBoxStatus.Items.Add("Dibatalkan");
            comboBoxStatus.Items.Add("Menunggu Konfirmasi");
            comboBoxStatus.DropDownStyle = ComboBoxStyle.DropDownList;

            comboBoxMetodePembayaran.Items.Clear();
            comboBoxMetodePembayaran.Items.Add("Cash");
            comboBoxMetodePembayaran.Items.Add("Transfer Bank");
            comboBoxMetodePembayaran.Items.Add("E-Wallet");
            comboBoxMetodePembayaran.Items.Add("Kartu Kredit");
            comboBoxMetodePembayaran.DropDownStyle = ComboBoxStyle.DropDownList;
        }

        private void ClearForm()
        {
            comboBoxPemesanan.SelectedIndex = -1;
            comboBoxKlien.SelectedIndex = -1;
            txtTotal.Clear();
            comboBoxMetodePembayaran.SelectedIndex = -1;
            comboBoxStatus.SelectedIndex = -1;
        }

        private void LoadData()
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                try
                {
                    conn.Open();
                    string query = @"
                SELECT 
                    dp.DataPembayaranID,
                    p.PemesananID,       
                    dp.Total,
                    dp.MetodePembayaran,
                    dp.Tanggal,
                    dp.Status,
                    k.KlienID,
                    k.NamaKlien,
                    a.AcaraID,
                    a.NamaAcara
                    
                FROM DataPembayaran dp
                INNER JOIN Klien k ON dp.KlienID = k.KlienID
                INNER JOIN Pemesanan p ON dp.PemesananID = p.PemesananID
                INNER JOIN Acara a ON p.AcaraID = a.AcaraID";

                    SqlDataAdapter da = new SqlDataAdapter(query, conn);
                    DataTable dt = new DataTable();
                    da.Fill(dt);

                    dgvDataPembayaran.AutoGenerateColumns = true;
                    dgvDataPembayaran.DataSource = dt;

                    ClearForm();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: " + ex.Message, "Kesalahan", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void dgvDataPembayaran_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dgvDataPembayaran.Rows[e.RowIndex];

                txtTotal.Text = row.Cells["Total"].Value?.ToString() ?? "";
                txtTotal.ReadOnly = true;
                string PembayaranValue = row.Cells["MetodePembayaran"].Value?.ToString();

                comboBoxKlien.SelectedValue = row.Cells["KlienID"].Value;
                comboBoxPemesanan.SelectedValue = row.Cells["PemesananID"].Value;

                if (!string.IsNullOrEmpty(PembayaranValue))
                {
                    int index = comboBoxMetodePembayaran.Items.IndexOf(PembayaranValue);
                    if (index >= 0)
                    {
                        comboBoxMetodePembayaran.SelectedIndex = index;
                    }
                }

                // Handle tanggal dengan aman menggunakan DateTimePicker
                // Set ComboBox status
                string statusValue = row.Cells["Status"].Value?.ToString();
                if (!string.IsNullOrEmpty(statusValue))
                {
                    int index = comboBoxStatus.Items.IndexOf(statusValue);
                    if (index >= 0)
                    {
                        comboBoxStatus.SelectedIndex = index;
                    }
                }
            }
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {

            if (dgvDataPembayaran.CurrentRow == null)
            {
                MessageBox.Show("Pilih data yang akan diedit terlebih dahulu!", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // ✅ Ambil nilai dari input form
            string statusPembayaran = comboBoxStatus.SelectedItem?.ToString();
            string metodePembayaran = comboBoxMetodePembayaran.SelectedItem?.ToString();

            // ✅ Validasi: pastikan semua data penting diisi
            if (string.IsNullOrWhiteSpace(txtTotal.Text) || comboBoxStatus.SelectedItem == null)
            {
                MessageBox.Show("Harap isi semua data!", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // ✅ Validasi: jika status 'Diterima', metode harus diisi
            if (statusPembayaran == "Diterima" && string.IsNullOrEmpty(metodePembayaran))
            {
                MessageBox.Show("Silakan pilih metode pembayaran jika status adalah 'Diterima'.", "Validasi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }


            if (statusPembayaran == "Menunggu Konfirmasi" && !string.IsNullOrEmpty(metodePembayaran))
            {
                MessageBox.Show("Metode pembayaran tidak boleh diisi jika status 'Menunggu Konfirmasi'.", "Validasi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // ✅ Validasi: cek total harus berupa angka desimal
            if (!decimal.TryParse(txtTotal.Text.Trim(), out decimal total))
            {
                MessageBox.Show("Total harus berupa angka!", "Input Tidak Valid", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // ✅ Ambil ID dari baris yang dipilih di DataGridView
            int dataPembayaranID = Convert.ToInt32(dgvDataPembayaran.CurrentRow.Cells["DataPembayaranID"].Value);

            // ✅ Koneksi ke database dan mulai transaksi
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                SqlTransaction transaction = conn.BeginTransaction(); // <-- Mulai transaksi

                try
                {
                    // 🔄 LANGKAH 1: Update kolom Total dan MetodePembayaran
                    string updateQuery = @"UPDATE DataPembayaran
                                   SET Total = @Total,
                                       MetodePembayaran = @MetodePembayaran
                                   WHERE DataPembayaranID = @DataPembayaranID";

                    using (SqlCommand cmd = new SqlCommand(updateQuery, conn, transaction))
                    {
                        cmd.Parameters.AddWithValue("@Total", total);
                        cmd.Parameters.AddWithValue("@MetodePembayaran", (object)metodePembayaran ?? DBNull.Value);
                        cmd.Parameters.AddWithValue("@DataPembayaranID", dataPembayaranID);

                        int rowsAffected = cmd.ExecuteNonQuery();
                        if (rowsAffected == 0)
                        {
                            transaction.Rollback(); // ⛔ Batalkan transaksi jika gagal
                            MessageBox.Show("Data pembayaran gagal diperbarui!", "Kesalahan", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }
                    }

                    // 🔄 LANGKAH 2: Panggil Stored Procedure untuk update kolom Status
                    using (SqlCommand spCmd = new SqlCommand("spUpdateStatusPembayaran", conn, transaction))
                    {
                        spCmd.CommandType = CommandType.StoredProcedure;
                        spCmd.Parameters.AddWithValue("@DataPembayaranID", dataPembayaranID);
                        spCmd.Parameters.AddWithValue("@StatusBaru", statusPembayaran);
                        spCmd.ExecuteNonQuery(); // <-- Jalankan SP
                    }

                    // ✅ Jika semua berhasil, simpan perubahan
                    transaction.Commit();
                    MessageBox.Show("Data pembayaran berhasil diperbarui!", "Sukses", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    // 🔃 Refresh tabel dan kosongkan input
                    LoadData();
                    ClearForm();
                }
                catch (Exception ex)
                {
                    // ⛔ Jika ada error, batalkan semua perubahan
                    transaction.Rollback();
                    MessageBox.Show("Error: " + ex.Message, "Kesalahan", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }

        }


        private void btnDelete_Click(object sender, EventArgs e)
        {
            // Validasi apakah ada baris yang dipilih
            if (dgvDataPembayaran.CurrentRow == null)
            {
                MessageBox.Show("Pilih data yang akan dihapus terlebih dahulu!", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Konfirmasi penghapusan
            DialogResult result = MessageBox.Show("Anda yakin ingin menghapus data pembayaran ini?", "Konfirmasi",
                                                 MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.No)
            {
                return;
            }

            // Ambil PembayaranID dari baris yang dipilih
            int DatapembayaranID = Convert.ToInt32(dgvDataPembayaran.CurrentRow.Cells["DataPembayaranID"].Value);

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                try
                {
                    conn.Open();
                    string query = "DELETE FROM DataPembayaran WHERE DataPembayaranID = @DataPembayaranID";

                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@DataPembayaranID", DatapembayaranID);

                        int rowsAffected = cmd.ExecuteNonQuery();
                        if (rowsAffected > 0)
                        {
                            MessageBox.Show("Data pembayaran berhasil dihapus!", "Sukses", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            LoadData();
                            ClearForm();
                        }
                        else
                        {
                            MessageBox.Show("Data pembayaran tidak berhasil dihapus!", "Kesalahan", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: " + ex.Message, "Kesalahan", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            LoadData();
            MessageBox.Show("Data berhasil direfresh!", "Informasi", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void btnInsert_Click(object sender, EventArgs e)
        {
            string statusPembayaran = comboBoxStatus.SelectedItem?.ToString();
            string metodePembayaran = comboBoxMetodePembayaran.SelectedItem?.ToString();

            if (string.IsNullOrWhiteSpace(txtTotal.Text) ||
                comboBoxStatus.SelectedItem == null)
            {
                MessageBox.Show("Harap isi semua data!", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Validasi status vs metode pembayaran
            if (statusPembayaran == "Diterima" && string.IsNullOrEmpty(metodePembayaran))
            {
                MessageBox.Show("Silakan pilih metode pembayaran jika status pembayaran adalah 'Diterima'.", "Validasi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (statusPembayaran == "Menunggu Konfirmasi" && !string.IsNullOrEmpty(metodePembayaran))
            {
                MessageBox.Show("Metode pembayaran tidak boleh diisi jika status pembayaran adalah 'Menunggu Konfirmasi'.", "Validasi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (!decimal.TryParse(txtTotal.Text.Trim(), out decimal total))
            {
                MessageBox.Show("Total harus berupa angka!", "Input Tidak Valid", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                try
                {
                    conn.Open();
                    string query = "INSERT INTO DataPembayaran (Total, MetodePembayaran, Status, PemesananID, KlienID) " +
                                   "VALUES (@Total, @MetodePembayaran, @Status, @PemesananID, @KlienID)";

                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@Total", total);
                        cmd.Parameters.AddWithValue("@MetodePembayaran", (object)metodePembayaran ?? DBNull.Value);
                        cmd.Parameters.AddWithValue("@PemesananID", comboBoxPemesanan.SelectedValue);
                        cmd.Parameters.AddWithValue("@KlienID", comboBoxKlien.SelectedValue);
                        cmd.Parameters.AddWithValue("@Status", statusPembayaran);

                        int rowsAffected = cmd.ExecuteNonQuery();
                        if (rowsAffected > 0)
                        {
                            MessageBox.Show("Data pembayaran berhasil ditambahkan!", "Sukses", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            LoadData();
                            ClearForm();
                        }
                        else
                        {
                            MessageBox.Show("Data pembayaran tidak berhasil ditambahkan!", "Kesalahan", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: " + ex.Message, "Kesalahan", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }


        private void LoadPemesananAcaraByKlien(int klienId)  //menampilkan acara sesuai yang dimiliki oleh kliennya
        {
            comboBoxPemesanan.SelectedIndexChanged -= comboBoxPemesanan_SelectedIndexChanged;

            comboBoxPemesanan.DataSource = null;
            comboBoxPemesanan.Items.Clear();

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                string query = @"
        SELECT P.PemesananID, A.NamaAcara
        FROM Pemesanan P
        INNER JOIN Acara A ON P.AcaraID = A.AcaraID
        WHERE P.KlienID = @KlienID";

                SqlDataAdapter da = new SqlDataAdapter(query, conn);
                da.SelectCommand.Parameters.AddWithValue("@KlienID", klienId);
                DataTable dt = new DataTable();
                da.Fill(dt);

                comboBoxPemesanan.DataSource = dt;
                comboBoxPemesanan.DisplayMember = "NamaAcara";
                comboBoxPemesanan.ValueMember = "PemesananID";
                comboBoxPemesanan.SelectedIndex = -1;
            }

            comboBoxPemesanan.SelectedIndexChanged += comboBoxPemesanan_SelectedIndexChanged;
        }

        private void LoadDropdowns()
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();

                // Dropdown Klien
                using (SqlCommand cmd = new SqlCommand("SELECT KlienID, NamaKlien FROM Klien", conn))
                {
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    comboBoxKlien.DataSource = dt;
                    comboBoxKlien.DisplayMember = "NamaKlien";
                    comboBoxKlien.ValueMember = "KlienID";
                    comboBoxKlien.SelectedIndex = -1; // Set tidak ada pilihan awal
                }

                // Dropdown Pemesanan
                using (SqlCommand cmd = new SqlCommand(
                    @"SELECT P.PemesananID, A.NamaAcara 
              FROM Pemesanan P 
              JOIN Acara A ON P.AcaraID = A.AcaraID", conn))
                {
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    comboBoxPemesanan.DataSource = dt;
                    comboBoxPemesanan.DisplayMember = "NamaAcara";
                    comboBoxPemesanan.ValueMember = "PemesananID";
                    comboBoxPemesanan.SelectedIndex = -1; // Set tidak ada pilihan awal
                }
            }
        }

        private void comboBoxStatus_SelectedIndexChanged(object sender, EventArgs e)
        {
            string status = comboBoxStatus.SelectedItem?.ToString();

            if (status == "Menunggu Konfirmasi")
            {
                comboBoxMetodePembayaran.SelectedIndex = -1; // Kosongkan
                comboBoxMetodePembayaran.Enabled = false;     // Disable
            }
            else if (status == "Diterima")
            {
                comboBoxMetodePembayaran.Enabled = true;      // Enable kembali
            }
        }

        private void comboBoxKlien_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBoxKlien.SelectedValue != null && int.TryParse(comboBoxKlien.SelectedValue.ToString(), out int klienId))
            {
                LoadPemesananAcaraByKlien(klienId);
            }
            else
            {
                comboBoxPemesanan.DataSource = null;
                comboBoxPemesanan.Items.Clear();
            }
        }
        private void HitungTotalOtomatis()
        {
            if (comboBoxPemesanan.SelectedValue == null) return;

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                string query = @"
            SELECT r.HargaPerJam, a.TanggalMulai, a.TanggalSelesai
            FROM Pemesanan p
            JOIN Acara a ON p.AcaraID = a.AcaraID
            JOIN Ruangan r ON a.RuanganID = r.RuanganID
            WHERE p.PemesananID = @PemesananID";

                SqlCommand cmd = new SqlCommand(query, conn);
                object selected = comboBoxPemesanan.SelectedValue;
                if (selected is DataRowView rowView)
                    selected = rowView["PemesananID"];

                cmd.Parameters.AddWithValue("@PemesananID", selected);

                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        decimal hargaPerJam = reader.GetDecimal(0);
                        DateTime mulai = reader.GetDateTime(1);
                        DateTime selesai = reader.GetDateTime(2);
                        double jam = (selesai - mulai).TotalHours;
                        decimal total = hargaPerJam * (decimal)jam;

                        txtTotal.Text = total.ToString("F0");
                    }
                }
            }
        }
        private void comboBoxPemesanan_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBoxPemesanan.SelectedIndex != -1)
            {
                HitungTotalOtomatis();
            }
        }



        private void btnBack_Click(object sender, EventArgs e)
        {
            FormDashboard formMenu = new FormDashboard();
            formMenu.Show();
            this.Hide();
        }

        private void BtnAnalisis_Click(object sender, EventArgs e)
        {
            // Tambahkan ini di paling awal:
            SetupQueryOptimization();
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                try
                {
                    conn.Open();

                    // Aktifkan analisis statistik IO dan waktu eksekusi (efeknya hanya terlihat di SSMS jika dijalankan manual)
                    SqlCommand statCmd = new SqlCommand("SET STATISTICS IO ON; SET STATISTICS TIME ON;", conn);
                    statCmd.ExecuteNonQuery();

                    // Query analisis (contoh: ambil data pembayaran dengan status 'Diterima')
                    string query = @"
                SELECT dp.DataPembayaranID, dp.Total, dp.MetodePembayaran, dp.Status, dp.Tanggal,
                       p.PemesananID, k.KlienID, k.NamaKlien
                FROM DataPembayaran dp
                JOIN Pemesanan p ON dp.PemesananID = p.PemesananID
                JOIN Klien k ON dp.KlienID = k.KlienID
                WHERE dp.Status = @Status";

                    SqlCommand dataCmd = new SqlCommand(query, conn);
                    dataCmd.Parameters.AddWithValue("@Status", "Diterima");

                    using (SqlDataReader reader = dataCmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            // Data bisa diproses jika ingin, tapi di sini kita hanya menganalisis performa
                        }
                        reader.Close();
                    }

                    MessageBox.Show("Analisis query selesai.\nSilakan cek tab 'Messages' di SQL Server Management Studio (SSMS) untuk statistik I/O dan waktu.",
                                    "Analisis Selesai", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Terjadi kesalahan saat analisis: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void Report_Click(object sender, EventArgs e)
        {
            ReportInvoiceForm reportInvoiceForm = new ReportInvoiceForm();
            reportInvoiceForm.Show();
            this.Hide();
        }

        private void SetupQueryOptimization()
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();

                string indexPemesanan = @"
        IF NOT EXISTS (SELECT name FROM sys.indexes WHERE name = 'IDX_DataPembayaran_PemesananID')
        BEGIN
            CREATE NONCLUSTERED INDEX IDX_DataPembayaran_PemesananID ON DataPembayaran (PemesananID);
        END";

                string indexStatus = @"
        IF NOT EXISTS (SELECT name FROM sys.indexes WHERE name = 'IDX_DataPembayaran_Status')
        BEGIN
            CREATE NONCLUSTERED INDEX IDX_DataPembayaran_Status ON DataPembayaran (Status);
        END";

                using (SqlCommand cmd1 = new SqlCommand(indexPemesanan, conn))
                {
                    cmd1.ExecuteNonQuery();
                }

                using (SqlCommand cmd2 = new SqlCommand(indexStatus, conn))
                {
                    cmd2.ExecuteNonQuery();
                }
            }
        }

    }
}
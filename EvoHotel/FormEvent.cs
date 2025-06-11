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
    public partial class FormEvent : Form
    {
        private string connectionString = "Data Source=LAPTOP-SALMAARA\\SALMALUVIRZA;Initial Catalog=ProjekEvoHotel;Integrated Security=True";
        private int selectedAcaraID = -1;
        public FormEvent()
        {
            InitializeComponent();
            InitializeComboBox();
            LoadData();
            LoadKlienComboBox();
            LoadRuanganComboBox();
            EnsureIndexes();

            // Format dan jam manual untuk DateTimePicker
            dtpMulai.Format = DateTimePickerFormat.Custom;
            dtpMulai.CustomFormat = "yyyy-MM-dd HH:mm";
            dtpMulai.ShowUpDown = true;

            dtpSelesai.Format = DateTimePickerFormat.Custom;
            dtpSelesai.CustomFormat = "yyyy-MM-dd HH:mm";
            dtpSelesai.ShowUpDown = true;
        }

        private void InitializeComboBox()
        {
            cmbStatus.Items.Clear();
            cmbStatus.Items.Add("Direncanakan");
            cmbStatus.Items.Add("Berlangsung");
            cmbStatus.Items.Add("Selesai");
            cmbStatus.Items.Add("Dibatalkan");
            cmbStatus.DropDownStyle = ComboBoxStyle.DropDownList;
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
                    a.AcaraID,
                    k.KlienID,
                    r.RuanganID,
                    a.NamaAcara,
                    a.Deskripsi,
                    a.JumlahPeserta,
                    a.TanggalMulai,
                    a.TanggalSelesai,
                    a.Status,
                    k.NamaKlien,
                    r.NamaRuangan
                FROM Acara a
                INNER JOIN Klien k ON a.KlienID = k.KlienID
                INNER JOIN Ruangan r ON a.RuanganID = r.RuanganID";
                    SqlDataAdapter adapter = new SqlDataAdapter(query, conn);
                    DataTable dt = new DataTable();
                    adapter.Fill(dt);

                    dgvAcara.DataSource = dt;
                    ClearForm();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Gagal memuat data: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void ClearForm()
        {
            txtNama.Clear();
            txtDeskripsi.Clear();
            txtJumlah.Clear();
            dtpMulai.Value = DateTime.Now;
            dtpSelesai.Value = DateTime.Now;
            cmbStatus.SelectedIndex = -1;
            comboBoxKlien.SelectedIndex = -1;
            comboBoxRuangan.SelectedIndex = -1;
        }

        private bool ValidInput()
        {
            if (string.IsNullOrWhiteSpace(txtNama.Text) ||
                string.IsNullOrWhiteSpace(txtDeskripsi.Text) ||
                string.IsNullOrWhiteSpace(txtJumlah.Text) ||
                cmbStatus.SelectedIndex == -1 ||
                comboBoxKlien.SelectedIndex == -1 ||
                comboBoxRuangan.SelectedIndex == -1)
            {
                MessageBox.Show("Semua field harus diisi!");
                return false;
            }

            if (!int.TryParse(txtJumlah.Text, out int jumlah) || jumlah <= 0)
            {
                MessageBox.Show("Jumlah peserta harus angka positif!");
                return false;
            }

            if (dtpSelesai.Value < dtpMulai.Value)
            {
                MessageBox.Show("Tanggal selesai tidak boleh sebelum tanggal mulai!");
                return false;
            }

            return true;
        }

        private void dgvAcara_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dgvAcara.Rows[e.RowIndex];
                selectedAcaraID = Convert.ToInt32(row.Cells["AcaraID"].Value);

                // Ambil RuanganID dari baris yang dipilih
                int currentRuanganId = Convert.ToInt32(row.Cells["RuanganID"].Value);

                // Panggil LoadRuanganComboBox dengan RuanganID saat ini
                LoadRuanganComboBox(currentRuanganId);

                txtNama.Text = row.Cells["NamaAcara"].Value?.ToString();
                txtDeskripsi.Text = row.Cells["Deskripsi"].Value?.ToString();
                txtJumlah.Text = row.Cells["JumlahPeserta"].Value?.ToString();
                dtpMulai.Value = Convert.ToDateTime(row.Cells["TanggalMulai"].Value);
                dtpSelesai.Value = Convert.ToDateTime(row.Cells["TanggalSelesai"].Value);
                cmbStatus.SelectedItem = row.Cells["Status"].Value?.ToString();
                comboBoxKlien.SelectedValue = row.Cells["KlienID"].Value;
                comboBoxRuangan.SelectedValue = currentRuanganId;
            }
        }
        private void cmbStatus_SelectionChangeCommitted(object sender, EventArgs e)
        {
            if (cmbStatus.SelectedItem.ToString() == "Dibatalkan" && selectedAcaraID != -1)
            {
                DialogResult confirm = MessageBox.Show(
                    "Yakin ingin membatalkan acara ini? Semua status terkait akan ikut dibatalkan.",
                    "Konfirmasi Pembatalan",
                    MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

                if (confirm == DialogResult.Yes)
                {
                    using (SqlConnection conn = new SqlConnection(connectionString))
                    {
                        try
                        {
                            conn.Open();
                            SqlCommand cmd = new SqlCommand("spBatalkanAcara", conn);
                            cmd.CommandType = CommandType.StoredProcedure;
                            cmd.Parameters.AddWithValue("@AcaraID", selectedAcaraID);

                            cmd.ExecuteNonQuery();
                            MessageBox.Show("Acara dan data terkait berhasil dibatalkan.");
                            LoadData(); // Refresh DataGridView
                        }
                        catch (SqlException ex)
                        {
                            MessageBox.Show("Gagal membatalkan acara: " + ex.Message);
                        }
                    }
                }
                else
                {
                    LoadData(); // Balikin ke status sebelumnya jika user batal
                }
            }
        }

        private void btnInsert_Click(object sender, EventArgs e)
        {
            if (!ValidInput()) return;

            // Tambahkan validasi tanggal di sini
            if (dtpSelesai.Value < dtpMulai.Value)
            {
                MessageBox.Show("Tanggal selesai tidak boleh lebih awal dari tanggal mulai!",
                    "Validasi Tanggal", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                SqlTransaction transaction = null; // Inisialisasi transaksi
                try
                {
                    transaction = conn.BeginTransaction(); // Mulai transaksi
                                                           // 1. Sisipkan data ke tabel Acara
                    string query = @"INSERT INTO Acara
                     (NamaAcara, Deskripsi, JumlahPeserta, TanggalMulai, TanggalSelesai, Status, KlienID, RuanganID)
                     VALUES (@Nama, @Deskripsi, @Jumlah, @Mulai, @Selesai, @Status, @KlienID, @RuanganID);
                     SELECT SCOPE_IDENTITY();"; // Dapatkan ID acara yang baru disisipkan
                    SqlCommand cmd = new SqlCommand(query, conn, transaction); // Tambahkan transaksi
                    cmd.Parameters.AddWithValue("@Nama", txtNama.Text);
                    cmd.Parameters.AddWithValue("@Deskripsi", txtDeskripsi.Text);
                    cmd.Parameters.AddWithValue("@Jumlah", int.Parse(txtJumlah.Text));
                    cmd.Parameters.AddWithValue("@Mulai", dtpMulai.Value);
                    cmd.Parameters.AddWithValue("@Selesai", dtpSelesai.Value);
                    cmd.Parameters.AddWithValue("@Status", cmbStatus.SelectedItem.ToString());
                    cmd.Parameters.AddWithValue("@KlienID", comboBoxKlien.SelectedValue ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@RuanganID", comboBoxRuangan.SelectedValue ?? DBNull.Value);
                    // Dapatkan ID acara yang baru disisipkan
                    int acaraID = Convert.ToInt32(cmd.ExecuteScalar());

                    transaction.Commit();
                    MessageBox.Show("Data acara berhasil ditambahkan!");
                    LoadData();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Gagal menambahkan acara: " + ex.Message);
                }
            }
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if (!ValidInput()) return;

            if (dtpSelesai.Value < dtpMulai.Value)
            {
                MessageBox.Show("Tanggal selesai tidak boleh lebih awal dari tanggal mulai!",
                    "Validasi Tanggal", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (selectedAcaraID == -1)
            {
                MessageBox.Show("Pilih acara yang akan diubah!");
                return;
            }

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                SqlTransaction transaction = null;

                try
                {
                    transaction = conn.BeginTransaction();

                    // Update data acara
                    string query = @"UPDATE Acara
                     SET NamaAcara = @Nama, Deskripsi = @Deskripsi, JumlahPeserta = @Jumlah,
                         TanggalMulai = @Mulai, TanggalSelesai = @Selesai, Status = @Status,
                         KlienID = @KlienID, RuanganID = @RuanganID
                     WHERE AcaraID = @ID";

                    SqlCommand cmd = new SqlCommand(query, conn, transaction);
                    cmd.Parameters.AddWithValue("@Nama", txtNama.Text);
                    cmd.Parameters.AddWithValue("@Deskripsi", txtDeskripsi.Text);
                    cmd.Parameters.AddWithValue("@Jumlah", int.Parse(txtJumlah.Text));
                    cmd.Parameters.AddWithValue("@Mulai", dtpMulai.Value);
                    cmd.Parameters.AddWithValue("@Selesai", dtpSelesai.Value);
                    cmd.Parameters.AddWithValue("@Status", cmbStatus.SelectedItem.ToString());
                    cmd.Parameters.AddWithValue("@ID", selectedAcaraID);
                    cmd.Parameters.AddWithValue("@KlienID", comboBoxKlien.SelectedValue ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@RuanganID", comboBoxRuangan.SelectedValue ?? DBNull.Value);

                    int rowsAffected = cmd.ExecuteNonQuery();

                    if (rowsAffected > 0)
                    {
                        // Jika status berubah menjadi 'Selesai', update status ruangan
                        if (cmbStatus.SelectedItem.ToString() == "Selesai")
                        {
                            // Update status ruangan menjadi 'Tersedia'
                            string updateRuanganQuery = "UPDATE Ruangan SET Status = 'Tersedia' WHERE RuanganID = @RuanganID";
                            using (SqlCommand updateRuanganCmd = new SqlCommand(updateRuanganQuery, conn, transaction))
                            {
                                updateRuanganCmd.Parameters.AddWithValue("@RuanganID", comboBoxRuangan.SelectedValue ?? DBNull.Value);
                                updateRuanganCmd.ExecuteNonQuery();
                            }

                            // (opsional) Jalankan prosedur jika ada
                            try
                            {
                                string getPemesananIDQuery = "SELECT TOP 1 PemesananID FROM Pemesanan WHERE AcaraID = @AcaraID";
                                using (SqlCommand getCmd = new SqlCommand(getPemesananIDQuery, conn, transaction))
                                {
                                    getCmd.Parameters.AddWithValue("@AcaraID", selectedAcaraID);
                                    object result = getCmd.ExecuteScalar();

                                    if (result != null)
                                    {
                                        int pemesananID = Convert.ToInt32(result);

                                        using (SqlCommand spCmd = new SqlCommand("spUpdateStatusAcara", conn, transaction))
                                        {
                                            spCmd.CommandType = CommandType.StoredProcedure;
                                            spCmd.Parameters.AddWithValue("@PemesananID", pemesananID);
                                            spCmd.ExecuteNonQuery();
                                        }
                                    }
                                }
                            }
                            catch (Exception exSP)
                            {
                                MessageBox.Show("Gagal menjalankan prosedur status acara: " + exSP.Message);
                                transaction.Rollback();
                                return;
                            }
                        }

                        transaction.Commit();
                        MessageBox.Show("Data acara berhasil diperbarui!");
                        LoadData();
                    }
                    else
                    {
                        transaction.Rollback();
                        MessageBox.Show("Data acara gagal diperbarui!");
                    }
                }
                catch (Exception ex)
                {
                    if (transaction != null)
                        transaction.Rollback();
                    MessageBox.Show("Error: " + ex.Message);
                }
                finally
                {
                    if (transaction != null)
                        transaction.Dispose();
                }
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (selectedAcaraID == -1)
            {
                MessageBox.Show("Pilih acara yang ingin dihapus!");
                return;
            }

            DialogResult result = MessageBox.Show("Yakin ingin menghapus acara ini?", "Konfirmasi", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.No) return;

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                try
                {
                    conn.Open();
                    string query = "DELETE FROM Acara WHERE AcaraID = @ID";
                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@ID", selectedAcaraID);
                    cmd.ExecuteNonQuery();

                    MessageBox.Show("Acara berhasil dihapus!");
                    LoadData();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Gagal menghapus acara: " + ex.Message);
                }
            }
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            LoadData();
            MessageBox.Show("Data berhasil direfresh!", "Informasi", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            FormDashboard formMenu = new FormDashboard();
            formMenu.Show();
            this.Hide();
        }

        private void LoadKlienComboBox()
        {
            comboBoxKlien.Items.Clear();
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                string query = "SELECT KlienID, NamaKlien FROM Klien";
                SqlCommand cmd = new SqlCommand(query, conn);
                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                adapter.Fill(dt);

                comboBoxKlien.DataSource = dt;
                comboBoxKlien.DisplayMember = "NamaKlien";
                comboBoxKlien.ValueMember = "KlienID";
                comboBoxKlien.SelectedIndex = -1;
            }
        }

        private void LoadRuanganComboBox(int? currentRuanganId = null)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                string query = @"
            SELECT RuanganID, NamaRuangan 
            FROM Ruangan
            WHERE Status = 'Tersedia'
            " + (currentRuanganId != null ? "OR RuanganID = @CurrentRuanganID" : "");

                SqlCommand cmd = new SqlCommand(query, conn);
                if (currentRuanganId != null)
                    cmd.Parameters.AddWithValue("@CurrentRuanganID", currentRuanganId);

                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                adapter.Fill(dt);

                comboBoxRuangan.DataSource = dt;
                comboBoxRuangan.DisplayMember = "NamaRuangan";
                comboBoxRuangan.ValueMember = "RuanganID";
                comboBoxRuangan.SelectedIndex = -1;
            }
        }
        private void btnReport_Click(object sender, EventArgs e)
        {
            try
            {
                FormReportViewerEvent formReport = new FormReportViewerEvent();
                formReport.Show();
                this.Hide();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error navigating to report: {ex.Message}",
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void AnalyzeQuery(string sqlquery)
        {
            using (var conn = new SqlConnection(connectionString))
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
        private void EnsureIndexes()
        {
            using (var conn = new SqlConnection(connectionString))
            {
                conn.Open();
                var indexScript = @"
        -- Cek dan buat index pada Acara.KlienID
        IF NOT EXISTS (SELECT 1 FROM sys.indexes WHERE name = 'idx_Acara_KlienID')
        BEGIN
            CREATE NONCLUSTERED INDEX idx_Acara_KlienID ON Acara(KlienID);
        END;

        -- Cek dan buat index pada Acara.RuanganID
        IF NOT EXISTS (SELECT 1 FROM sys.indexes WHERE name = 'idx_Acara_RuanganID')
        BEGIN
            CREATE NONCLUSTERED INDEX idx_Acara_RuanganID ON Acara(RuanganID);
        END;

        -- Cek dan buat index pada Acara.Status
        IF NOT EXISTS (SELECT 1 FROM sys.indexes WHERE name = 'idx_Acara_Status')
        BEGIN
            CREATE NONCLUSTERED INDEX idx_Acara_Status ON Acara(Status);
        END;

        -- Cek dan buat index pada Acara.TanggalMulai
        IF NOT EXISTS (SELECT 1 FROM sys.indexes WHERE name = 'idx_Acara_TanggalMulai')
        BEGIN
            CREATE NONCLUSTERED INDEX idx_Acara_TanggalMulai ON Acara(TanggalMulai);
        END;";

                using (var cmd = new SqlCommand(indexScript, conn))
                {
                    cmd.ExecuteNonQuery();
                }
            }
        }

        private void AnaylzeQuery_Click(object sender, EventArgs e)
        {
            var heavyQuery = @"
                SELECT 
                    a.AcaraID,
                    k.KlienID,
                    r.RuanganID,
                    a.NamaAcara,
                    a.Deskripsi,
                    a.JumlahPeserta,
                    a.TanggalMulai,
                    a.TanggalSelesai,
                    a.Status,
                    k.NamaKlien,
                    r.NamaRuangan
                FROM Acara a
                INNER JOIN Klien k ON a.KlienID = k.KlienID
                INNER JOIN Ruangan r ON a.RuanganID = r.RuanganID;";
            AnalyzeQuery(heavyQuery);

        }
    }
}

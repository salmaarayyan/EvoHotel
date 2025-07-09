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
    public partial class FormOrder : Form
    {
        Koneksi kn = new Koneksi();
        string strKonek = "";
        //private string connectionString = "Data Source=LAPTOP-SALMAARA\\SALMALUVIRZA;Initial Catalog=ProjekEvoHotel;Integrated Security=True";
        public FormOrder()
        {
            InitializeComponent();
            InitializeComboBox(); // Inisialisasi ComboBox di constructor
            LoadData();
            LoadKlienComboBox(); // Load Klien ComboBox
                                 // Tidak perlu memanggil LoadAcaraComboBox di sini
            comboBoxKlien.SelectedIndexChanged += comboBoxKlien_SelectedIndexChanged; // Pastikan event handler terhubung
        }

        private void InitializeComboBox()
        {
            // Inisialisasi ComboBox Status
            comboBoxStatus.Items.Clear();
            comboBoxStatus.Items.Add("Menunggu Konfirmasi");
            comboBoxStatus.Items.Add("Dikonfirmasi");
            comboBoxStatus.Items.Add("Dibatalkan");
            comboBoxStatus.Items.Add("Selesai");
            comboBoxStatus.DropDownStyle = ComboBoxStyle.DropDownList;
        }

        private void ClearForm()
        {
            txtTotalHarga.Clear();
            comboBoxStatus.SelectedIndex = -1;
            // Reset ComboBox Acara dan Klien jika diperlukan
            comboBoxAcara.SelectedIndex = -1; // Reset Acara ComboBox
            comboBoxKlien.SelectedIndex = -1; // Reset Klien ComboBox
        }

        private void LoadData()
        {
            using (SqlConnection conn = new SqlConnection(kn.connectionString()))
            {
                try
                {
                    conn.Open();
                    string query = @"
                    SELECT 
                        p.PemesananID,
                        p.TotalHarga,
                        p.Tanggal,
                        p.Status,
                        a.AcaraID,
                        a.NamaAcara,
                        k.KlienID,
                        k.NamaKlien,
                        r.NamaRuangan
                    FROM Pemesanan p
                    INNER JOIN Acara a ON p.AcaraID = a.AcaraID
                    INNER JOIN Klien k ON p.KlienID = k.KlienID
                    INNER JOIN Ruangan r ON a.RuanganID = r.RuanganID";
                    ;
                    SqlDataAdapter da = new SqlDataAdapter(query, conn);
                    DataTable dt = new DataTable();
                    da.Fill(dt);

                    dgvDataPemesanan.AutoGenerateColumns = true;
                    dgvDataPemesanan.DataSource = dt;

                    ClearForm(); // Auto Clear setelah LoadData
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: " + ex.Message, "Kesalahan", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void dgvDataPemesanan_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                try
                {
                    DataGridViewRow row = dgvDataPemesanan.Rows[e.RowIndex];

                    txtNamaRuangan.Text = row.Cells["NamaRuangan"].Value?.ToString() ?? "";
                    txtTotalHarga.Text = row.Cells["TotalHarga"].Value?.ToString() ?? "";

                    string statusValue = row.Cells["Status"].Value?.ToString();
                    if (!string.IsNullOrEmpty(statusValue))
                    {
                        int index = comboBoxStatus.Items.IndexOf(statusValue);
                        if (index >= 0)
                        {
                            comboBoxStatus.SelectedIndex = index;
                        }
                    }

                    // Safely set ComboBox values
                    if (row.Cells["KlienID"].Value != null && row.Cells["KlienID"].Value != DBNull.Value)
                    {
                        comboBoxKlien.SelectedValue = row.Cells["KlienID"].Value;
                    }

                    if (row.Cells["AcaraID"].Value != null && row.Cells["AcaraID"].Value != DBNull.Value)
                    {
                        comboBoxAcara.SelectedValue = row.Cells["AcaraID"].Value;
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error selecting row: {ex.Message}",
                        "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
        }
        // Enhanced validation method
        private bool ValidatePemesananData(out string errorMessage)
        {
            errorMessage = "";

            if (string.IsNullOrWhiteSpace(txtTotalHarga.Text))
            {
                errorMessage = "Total harga tidak boleh kosong!";
                return false;
            }

            if (!decimal.TryParse(txtTotalHarga.Text.Trim(), out decimal total) || total <= 0)
            {
                errorMessage = "Total harga harus berupa angka positif!";
                return false;
            }

            if (comboBoxStatus.SelectedItem == null)
            {
                errorMessage = "Status pemesanan harus dipilih!";
                return false;
            }

            if (comboBoxKlien.SelectedValue == null)
            {
                errorMessage = "Klien harus dipilih!";
                return false;
            }

            if (comboBoxAcara.SelectedValue == null)
            {
                errorMessage = "Acara harus dipilih!";
                return false;
            }

            return true;
        }

        // Enhanced validation for order status changes
        private bool ValidateStatusChange(string currentStatus, string newStatus, out string errorMessage)
        {
            errorMessage = "";

            // Define valid status transitions
            Dictionary<string, List<string>> validTransitions = new Dictionary<string, List<string>>
            {
                ["Menunggu Konfirmasi"] = new List<string> { "Dikonfirmasi", "Dibatalkan" },
                ["Dikonfirmasi"] = new List<string> { "Selesai", "Dibatalkan" },
                ["Dibatalkan"] = new List<string>(), // Cannot change from cancelled
                ["Selesai"] = new List<string>() // Cannot change from completed
            };

            if (currentStatus == "Dibatalkan")
            {
                errorMessage = "Pemesanan yang sudah dibatalkan tidak dapat diubah!";
                return false;
            }

            if (currentStatus == "Selesai")
            {
                errorMessage = "Pemesanan yang sudah selesai tidak dapat diubah!";
                return false;
            }

            if (validTransitions.ContainsKey(currentStatus) &&
                !validTransitions[currentStatus].Contains(newStatus))
            {
                errorMessage = $"Status tidak dapat diubah dari '{currentStatus}' menjadi '{newStatus}'!";
                return false;
            }

            return true;
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            if (dgvDataPemesanan.CurrentRow == null)
            {
                MessageBox.Show("Pilih data yang akan diedit terlebih dahulu!", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Validasi input data
            if (!ValidatePemesananData(out string validationError))
            {
                MessageBox.Show(validationError, "Validasi Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            int pemesananID = Convert.ToInt32(dgvDataPemesanan.CurrentRow.Cells["PemesananID"].Value);
            string currentStatus = dgvDataPemesanan.CurrentRow.Cells["Status"].Value?.ToString() ?? "";
            string newStatus = comboBoxStatus.SelectedItem.ToString();

            // Validasi perubahan status
            if (!ValidateStatusChange(currentStatus, newStatus, out string statusError))
            {
                MessageBox.Show(statusError, "Status Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Validasi format TotalHarga
            if (!decimal.TryParse(txtTotalHarga.Text.Trim(), out decimal total))
            {
                MessageBox.Show("TotalHarga harus berupa angka!", "Input Tidak Valid", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Gunakan transaksi untuk memastikan integritas data
            using (SqlConnection conn = new SqlConnection(kn.connectionString()))
            {
                SqlTransaction transaction = null;
                try
                {
                    conn.Open();
                    transaction = conn.BeginTransaction();

                    // Cek apakah pemesanan masih ada dan belum diubah oleh pengguna lain
                    string checkQuery = "SELECT Status FROM Pemesanan WHERE PemesananID = @PemesananID";
                    using (SqlCommand checkCmd = new SqlCommand(checkQuery, conn, transaction))
                    {
                        checkCmd.Parameters.AddWithValue("@PemesananID", pemesananID);
                        object result = checkCmd.ExecuteScalar();

                        if (result == null)
                        {
                            throw new InvalidOperationException("Pemesanan tidak ditemukan!");
                        }

                        string dbStatus = result.ToString();
                        if (dbStatus != currentStatus)
                        {
                            throw new InvalidOperationException("Status pemesanan telah diubah oleh pengguna lain. Silakan refresh data!");
                        }
                    }

                    // Update data pemesanan
                    string updateQuery = @"UPDATE Pemesanan 
                                 SET TotalHarga = @TotalHarga, Status = @Status, AcaraID = @AcaraID, KlienID = @KlienID 
                                 WHERE PemesananID = @PemesananID";

                    using (SqlCommand cmd = new SqlCommand(updateQuery, conn, transaction))
                    {
                        cmd.Parameters.AddWithValue("@TotalHarga", total);
                        cmd.Parameters.AddWithValue("@Status", newStatus);
                        cmd.Parameters.AddWithValue("@AcaraID", comboBoxAcara.SelectedValue ?? DBNull.Value);
                        cmd.Parameters.AddWithValue("@KlienID", comboBoxKlien.SelectedValue ?? DBNull.Value);
                        cmd.Parameters.AddWithValue("@PemesananID", pemesananID);

                        int rowsAffected = cmd.ExecuteNonQuery();
                        if (rowsAffected == 0)
                        {
                            throw new InvalidOperationException("Gagal memperbarui pemesanan!");
                        }
                    }

                    // Jika status diubah menjadi "Dikonfirmasi", jalankan SP untuk update status ruangan
                    if (newStatus == "Dikonfirmasi")
                    {
                        try
                        {
                            using (SqlCommand spCmd = new SqlCommand("UpdateStatusRuanganJikaDikonfirmasi", conn, transaction))
                            {
                                spCmd.CommandType = CommandType.StoredProcedure;
                                spCmd.Parameters.AddWithValue("@PemesananID", pemesananID);
                                spCmd.ExecuteNonQuery();
                            }
                        }
                        catch (SqlException ex)
                        {
                            throw new InvalidOperationException("Gagal update status ruangan: " + ex.Message);
                        }
                    }

                    // Jika status diubah menjadi "Dibatalkan", lakukan tindakan pembersihan jika perlu
                    if (newStatus == "Dibatalkan")
                    {
                        // Tambahkan logika pembatalan (misalnya, lepas sumber daya, kirim pemberitahuan, dll.)
                        LogStatusChange(pemesananID, currentStatus, newStatus, conn, transaction);
                    }

                    // Komit transaksi
                    transaction.Commit();

                    MessageBox.Show("Data pemesanan berhasil diperbarui!", "Sukses", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LoadData();
                    ClearForm();
                }
                catch (SqlException sqlEx)
                {
                    transaction?.Rollback();
                    MessageBox.Show($"Database Error: {sqlEx.Message}\nError Number: {sqlEx.Number}", "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                catch (InvalidOperationException ex)
                {
                    transaction?.Rollback();
                    MessageBox.Show(ex.Message, "Operation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                catch (Exception ex)
                {
                    transaction?.Rollback();
                    MessageBox.Show($"Unexpected error: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        // Method to log status changes for audit trail
        private void LogStatusChange(int pemesananID, string oldStatus, string newStatus,
            SqlConnection conn, SqlTransaction transaction)
        {
            try
            {
                string logQuery = @"INSERT INTO StatusLog (PemesananID, OldStatus, NewStatus, ChangeDate, ChangedBy) 
                                  VALUES (@PemesananID, @OldStatus, @NewStatus, @ChangeDate, @ChangedBy)";

                using (SqlCommand logCmd = new SqlCommand(logQuery, conn, transaction))
                {
                    logCmd.Parameters.AddWithValue("@PemesananID", pemesananID);
                    logCmd.Parameters.AddWithValue("@OldStatus", oldStatus);
                    logCmd.Parameters.AddWithValue("@NewStatus", newStatus);
                    logCmd.Parameters.AddWithValue("@ChangeDate", DateTime.Now);
                    logCmd.Parameters.AddWithValue("@ChangedBy", Environment.UserName);

                    logCmd.ExecuteNonQuery();
                }
            }
            catch (SqlException)
            {
                // Log table might not exist, continue without logging
                // In production, you might want to create the table or handle this differently
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (dgvDataPemesanan.CurrentRow == null)
            {
                MessageBox.Show("Pilih data yang akan dihapus terlebih dahulu!", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            int pemesananID = Convert.ToInt32(dgvDataPemesanan.CurrentRow.Cells["PemesananID"].Value);
            string currentStatus = dgvDataPemesanan.CurrentRow.Cells["Status"].Value?.ToString() ?? "";

            // Validasi status sebelum penghapusan
            if (currentStatus == "Dikonfirmasi" || currentStatus == "Selesai")
            {
                DialogResult confirmResult = MessageBox.Show(
                    $"Pemesanan dengan status '{currentStatus}' akan dihapus. Apakah Anda yakin?",
                    "Konfirmasi Penghapusan", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

                if (confirmResult == DialogResult.No)
                    return;
            }
            else
            {
                DialogResult result = MessageBox.Show("Anda yakin ingin menghapus data pemesanan ini?",
                    "Konfirmasi", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (result == DialogResult.No)
                    return;
            }

            using (SqlConnection conn = new SqlConnection(kn.connectionString()))
            {
                SqlTransaction transaction = null;
                try
                {
                    conn.Open();
                    transaction = conn.BeginTransaction();

                    // Cek apakah pemesanan masih ada dan dapat dihapus
                    string checkQuery = "SELECT Status FROM Pemesanan WHERE PemesananID = @PemesananID";
                    using (SqlCommand checkCmd = new SqlCommand(checkQuery, conn, transaction))
                    {
                        checkCmd.Parameters.AddWithValue("@PemesananID", pemesananID);
                        object result = checkCmd.ExecuteScalar();

                        if (result == null)
                        {
                            throw new InvalidOperationException("Pemesanan tidak ditemukan!");
                        }
                    }

                    // Hapus data pemesanan
                    string deleteQuery = "DELETE FROM Pemesanan WHERE PemesananID = @PemesananID";
                    using (SqlCommand cmd = new SqlCommand(deleteQuery, conn, transaction))
                    {
                        cmd.Parameters.AddWithValue("@PemesananID", pemesananID);

                        int rowsAffected = cmd.ExecuteNonQuery();
                        if (rowsAffected == 0)
                        {
                            throw new InvalidOperationException("Gagal menghapus pemesanan!");
                        }
                    }

                    // Commit transaksi jika penghapusan berhasil
                    transaction.Commit();
                    MessageBox.Show("Data pemesanan berhasil dihapus!", "Sukses", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LoadData();
                    ClearForm();
                }
                catch (SqlException sqlEx)
                {
                    transaction?.Rollback();
                    if (sqlEx.Number == 547) // Foreign key constraint error
                    {
                        MessageBox.Show("Tidak dapat menghapus pemesanan karena masih terkait dengan data lain!",
                            "Constraint Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                    else
                    {
                        MessageBox.Show($"Database Error: {sqlEx.Message}\nError Number: {sqlEx.Number}",
                            "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                catch (InvalidOperationException ex)
                {
                    transaction?.Rollback();
                    MessageBox.Show(ex.Message, "Operation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                catch (Exception ex)
                {
                    transaction?.Rollback();
                    MessageBox.Show($"Unexpected error: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            try
            {
                LoadData();
                MessageBox.Show("Data berhasil direfresh!",
                    "Informasi", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error refreshing data: {ex.Message}",
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnInsert_Click(object sender, EventArgs e)
        {
            // Validasi input
            if (!ValidatePemesananData(out string validationError))
            {
                MessageBox.Show(validationError, "Validasi Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            decimal total = decimal.Parse(txtTotalHarga.Text.Trim());
            int acaraId = (int)comboBoxAcara.SelectedValue;
            int klienId = (int)comboBoxKlien.SelectedValue;

            using (SqlConnection conn = new SqlConnection(kn.connectionString()))
            {
                SqlTransaction transaction = null;
                try
                {
                    conn.Open();
                    transaction = conn.BeginTransaction();

                    // 1. Verifikasi bahwa Acara dan Klien ada
                    string checkQuery = @"SELECT COUNT(*) FROM Acara WHERE AcaraID = @AcaraID AND KlienID = @KlienID";
                    using (SqlCommand checkCmd = new SqlCommand(checkQuery, conn, transaction))
                    {
                        checkCmd.Parameters.AddWithValue("@AcaraID", acaraId);
                        checkCmd.Parameters.AddWithValue("@KlienID", klienId);

                        int count = (int)checkCmd.ExecuteScalar();
                        if (count == 0)
                        {
                            throw new InvalidOperationException("Acara dan Klien yang dipilih tidak valid!");
                        }
                    }

                    // 2. Validasi Ketersediaan Ruangan
                    string availabilityQuery = @"
            SELECT COUNT(*)
            FROM Acara
            WHERE RuanganID = (SELECT RuanganID FROM Acara WHERE AcaraID = @AcaraID)
              AND AcaraID != @AcaraID  -- Exclude the current event being created
              AND (
                    (SELECT TanggalMulai FROM Acara WHERE AcaraID = @AcaraID) < TanggalSelesai
                    AND (SELECT TanggalSelesai FROM Acara WHERE AcaraID = @AcaraID) > TanggalMulai
                  )";

                    using (SqlCommand availabilityCmd = new SqlCommand(availabilityQuery, conn, transaction))
                    {
                        availabilityCmd.Parameters.AddWithValue("@AcaraID", acaraId);
                        int overlapCount = (int)availabilityCmd.ExecuteScalar();

                        if (overlapCount > 0)
                        {
                            throw new InvalidOperationException("Ruangan tidak tersedia pada tanggal dan waktu yang dipilih!");
                        }
                    }

                    // 3. Insert pemesanan baru
                    string insertQuery = @"INSERT INTO Pemesanan (TotalHarga, Status, AcaraID, KlienID, Tanggal)
                             VALUES (@TotalHarga, @Status, @AcaraID, @KlienID, @Tanggal);
                          SELECT SCOPE_IDENTITY();"; // Get the new PemesananID

                    int pemesananId;
                    using (SqlCommand cmd = new SqlCommand(insertQuery, conn, transaction))
                    {
                        cmd.Parameters.AddWithValue("@TotalHarga", total);
                        cmd.Parameters.AddWithValue("@Status", comboBoxStatus.SelectedItem.ToString());
                        cmd.Parameters.AddWithValue("@AcaraID", acaraId);
                        cmd.Parameters.AddWithValue("@KlienID", klienId);
                        cmd.Parameters.AddWithValue("@Tanggal", DateTime.Now);

                        object result = cmd.ExecuteScalar();
                        if (result == null || result == DBNull.Value)
                        {
                            throw new InvalidOperationException("Gagal menambahkan pemesanan!");
                        }
                        pemesananId = Convert.ToInt32(result);
                    }

                    // 4. Insert Data Pembayaran (Opsional)
                    string insertPembayaranQuery = @"INSERT INTO DataPembayaran (Total, MetodePembayaran, Status, PemesananID, KlienID)
                             VALUES (@TotalPembayaran, @MetodePembayaran, @StatusPembayaran, @PemesananID, @KlienID)";

                    using (SqlCommand cmd = new SqlCommand(insertPembayaranQuery, conn, transaction))
                    {
                        cmd.Parameters.AddWithValue("@TotalPembayaran", total);
                        cmd.Parameters.AddWithValue("@MetodePembayaran", "Menunggu Konfirmasi");
                        cmd.Parameters.AddWithValue("@StatusPembayaran", "Menunggu Konfirmasi");
                        cmd.Parameters.AddWithValue("@PemesananID", pemesananId);
                        cmd.Parameters.AddWithValue("@KlienID", klienId);

                        int rowsAffected = cmd.ExecuteNonQuery();
                        if (rowsAffected == 0)
                        {
                            throw new InvalidOperationException("Gagal menambahkan data pembayaran!");
                        }
                    }

                    // 5. Update status ruangan menjadi Tidak Tersedia
                    int ruanganId = 0;
                    string getRuanganQuery = "SELECT RuanganID FROM Acara WHERE AcaraID = @AcaraID";
                    using (SqlCommand getRuanganCmd = new SqlCommand(getRuanganQuery, conn, transaction))
                    {
                        getRuanganCmd.Parameters.AddWithValue("@AcaraID", acaraId);
                        object result = getRuanganCmd.ExecuteScalar();
                        if (result != null)
                            ruanganId = Convert.ToInt32(result);
                    }

                    if (ruanganId > 0)
                    {
                        string updateRuanganQuery = "UPDATE Ruangan SET Status = 'Tidak Tersedia' WHERE RuanganID = @RuanganID";
                        using (SqlCommand updateCmd = new SqlCommand(updateRuanganQuery, conn, transaction))
                        {
                            updateCmd.Parameters.AddWithValue("@RuanganID", ruanganId);
                            updateCmd.ExecuteNonQuery();
                        }
                    }

                    // Commit transaksi jika semua berhasil
                    transaction.Commit();
                    MessageBox.Show("Data pemesanan berhasil ditambahkan!", "Sukses", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LoadData();
                    ClearForm();
                }
                catch (SqlException sqlEx)
                {
                    transaction?.Rollback();
                    MessageBox.Show($"Database Error: {sqlEx.Message}\nError Number: {sqlEx.Number}",
                        "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                catch (InvalidOperationException ex)
                {
                    transaction?.Rollback();
                    MessageBox.Show(ex.Message, "Operation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                catch (Exception ex)
                {
                    transaction?.Rollback();
                    MessageBox.Show($"Unexpected error: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }


        private void LoadKlienComboBox()
        {
            comboBoxKlien.Items.Clear();
            using (SqlConnection conn = new SqlConnection(kn.connectionString()))
            {
                try
                {
                    conn.Open();
                    string query = "SELECT KlienID, NamaKlien FROM Klien ORDER BY NamaKlien";
                    SqlDataAdapter da = new SqlDataAdapter(query, conn);
                    DataTable dt = new DataTable();
                    da.Fill(dt);

                    comboBoxKlien.DataSource = dt;
                    comboBoxKlien.DisplayMember = "NamaKlien";
                    comboBoxKlien.ValueMember = "KlienID";
                    comboBoxKlien.SelectedIndex = -1;
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error loading klien data: {ex.Message}",
                        "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void comboBoxKlien_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBoxKlien.SelectedValue != null && int.TryParse(comboBoxKlien.SelectedValue.ToString(), out int klienId))
            {
                LoadAcaraComboBox(klienId);
                // Jangan panggil fungsi hitung harga atau tampil ruangan di sini
                txtTotalHarga.Clear();
                txtNamaRuangan.Clear();
            }
            else
            {
                comboBoxAcara.DataSource = null;
                comboBoxAcara.Items.Clear();
                txtTotalHarga.Clear();
                txtNamaRuangan.Clear();
            }
        }

        private void LoadAcaraComboBox(int klienId)
        {
            comboBoxAcara.DataSource = null;
            comboBoxAcara.Items.Clear();
            using (SqlConnection conn = new SqlConnection(kn.connectionString()))
            {
                try
                {
                    conn.Open();
                    string query = "SELECT AcaraID, NamaAcara FROM Acara WHERE KlienID = @KlienID ORDER BY NamaAcara";
                    SqlDataAdapter da = new SqlDataAdapter(query, conn);
                    da.SelectCommand.Parameters.AddWithValue("@KlienID", klienId);
                    DataTable dt = new DataTable();
                    da.Fill(dt);

                    comboBoxAcara.DataSource = dt;
                    comboBoxAcara.DisplayMember = "NamaAcara";
                    comboBoxAcara.ValueMember = "AcaraID";
                    comboBoxAcara.SelectedIndex = -1;
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error loading acara data: {ex.Message}",
                        "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void comboBoxStatus_SelectedIndexChanged(object sender, EventArgs e)
        {
            comboBoxStatus.BackColor = SystemColors.Window;
        }
        private void HitungTotalHargaOtomatis()
        {
            if (comboBoxAcara.SelectedValue == null) return;

            using (SqlConnection conn = new SqlConnection(kn.connectionString()))
            {
                conn.Open();
                string query = @"
            SELECT r.HargaPerJam, a.TanggalMulai, a.TanggalSelesai
            FROM Acara a
            JOIN Ruangan r ON a.RuanganID = r.RuanganID
            WHERE a.AcaraID = @AcaraID";

                SqlCommand cmd = new SqlCommand(query, conn);
                object selected = comboBoxAcara.SelectedValue;
                if (selected is DataRowView rowView)
                    selected = rowView["AcaraID"];
                cmd.Parameters.AddWithValue("@AcaraID", selected);

                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        decimal hargaPerJam = reader.IsDBNull(0) ? 0 : reader.GetDecimal(0);
                        DateTime mulai = reader.IsDBNull(1) ? DateTime.MinValue : reader.GetDateTime(1);
                        DateTime selesai = reader.IsDBNull(2) ? DateTime.MinValue : reader.GetDateTime(2);

                        if (mulai != DateTime.MinValue && selesai != DateTime.MinValue && selesai > mulai && hargaPerJam > 0)
                        {
                            double totalJam = (selesai - mulai).TotalHours;
                            decimal total = hargaPerJam * (decimal)totalJam;
                            txtTotalHarga.Text = total.ToString("F0"); // tanpa desimal
                        }
                        else
                        {
                            txtTotalHarga.Text = "0";
                        }
                    }
                    else
                    {
                        txtTotalHarga.Text = "0";
                    }
                }
            }
        }

        private void comboBoxAcara_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (comboBoxAcara.SelectedIndex != -1)
                {
                    TampilkanNamaRuangan();
                    HitungTotalHargaOtomatis();
                }
                else
                {
                    txtNamaRuangan.Clear();
                    txtTotalHarga.Clear();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error in acara selection: {ex.Message}",
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void TampilkanNamaRuangan()
        {
            if (comboBoxAcara.SelectedValue == null || comboBoxAcara.SelectedIndex == -1)
            {
                txtNamaRuangan.Text = "";
                return;
            }

            using (SqlConnection conn = new SqlConnection(kn.connectionString()))
            {
                try
                {
                    conn.Open();
                    string query = @"
                    SELECT r.NamaRuangan
                    FROM Acara a
                    JOIN Ruangan r ON a.RuanganID = r.RuanganID
                    WHERE a.AcaraID = @AcaraID";

                    SqlCommand cmd = new SqlCommand(query, conn);
                    object selected = comboBoxAcara.SelectedValue;

                    if (selected is DataRowView rowView)
                        selected = rowView["AcaraID"];

                    cmd.Parameters.AddWithValue("@AcaraID", selected);

                    object result = cmd.ExecuteScalar();
                    txtNamaRuangan.Text = result?.ToString() ?? "";
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error displaying room name: {ex.Message}",
                        "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtNamaRuangan.Text = "";
                }
            }
        }


        private void btnBack_Click(object sender, EventArgs e)
        {
            FormDashboard formMenu = new FormDashboard();
            formMenu.Show();
            this.Hide();
        }

        private void btnReport_Click(object sender, EventArgs e)
        {
            try
            {
                FormReport formReport = new FormReport();
                formReport.Show();
                this.Hide();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error navigating to report: {ex.Message}",
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


    }
}

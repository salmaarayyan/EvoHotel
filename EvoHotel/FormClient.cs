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
    public partial class FormClient : Form
    {
        private string connectionString = "Data Source=LAPTOP-SALMAARA\\SALMALUVIRZA;Initial Catalog=ProjekEvoHotel;Integrated Security=True";
        private BindingSource bindingSource = new BindingSource();
        public FormClient()
        {
            InitializeComponent();
            LoadData();
        }

        private void ClearForm()
        {
            txtNamaKlien.Clear();
            txtEmail.Clear();
            txtTelepon.Clear();
            txtAlamat.Clear();
        }

        private void LoadData()
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                try
                {
                    conn.Open();
                    string query = "SELECT KlienID AS [ID Klien], NamaKlien AS [Nama Klien], Email, NoTelp AS [No Telepon], Alamat FROM Klien";
                    SqlDataAdapter da = new SqlDataAdapter(query, conn);
                    DataTable dt = new DataTable();
                    da.Fill(dt);

                    dgvFormKlien.AutoGenerateColumns = true;
                    dgvFormKlien.DataSource = dt;

                    ClearForm(); // Auto Clear setelah LoadData
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: " + ex.Message, "Kesalahan", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void btnInsert_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtNamaKlien.Text) ||
                string.IsNullOrWhiteSpace(txtEmail.Text) ||
                string.IsNullOrWhiteSpace(txtTelepon.Text) ||
                string.IsNullOrWhiteSpace(txtAlamat.Text))
            {
                MessageBox.Show("Harap isi semua data!", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Validasi format email
            if (!IsValidEmail(txtEmail.Text.Trim()))
            {
                MessageBox.Show("Format email tidak valid!", "Input Tidak Valid", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Validasi no telepon (hanya angka)
            if (!IsValidPhoneNumber(txtTelepon.Text.Trim()))
            {
                MessageBox.Show("No telepon harus berupa angka dan panjang 10-15 digit!", "Input Tidak Valid", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                SqlTransaction transaction = conn.BeginTransaction();
                try
                {
                    using (SqlCommand cmd = new SqlCommand("sp_SaveKlien", conn, transaction))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        // Kirim DBNull.Value untuk KlienID pada saat INSERT
                        cmd.Parameters.AddWithValue("@KlienID", DBNull.Value); // NULL untuk KlienID baru
                        cmd.Parameters.AddWithValue("@NamaKlien", txtNamaKlien.Text.Trim());
                        cmd.Parameters.AddWithValue("@Email", txtEmail.Text.Trim());
                        cmd.Parameters.AddWithValue("@NoTelp", txtTelepon.Text.Trim());
                        cmd.Parameters.AddWithValue("@Alamat", txtAlamat.Text.Trim());

                        cmd.ExecuteNonQuery();
                    }

                    transaction.Commit(); // Commit transaksi jika berhasil
                    MessageBox.Show("Data klien berhasil ditambahkan!", "Sukses", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LoadData();
                    ClearForm();
                }
                catch (SqlException ex)
                {
                    transaction.Rollback(); // Rollback transaksi jika ada error
                    MessageBox.Show("Transaksi gagal: " + ex.Message, "Kesalahan", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }



        private void btnEdit_Click(object sender, EventArgs e)
        {
            // Validasi apakah ada baris yang dipilih
            if (dgvFormKlien.CurrentRow == null || dgvFormKlien.CurrentRow.Index < 0)
            {
                MessageBox.Show("Pilih data yang akan diedit terlebih dahulu!", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (string.IsNullOrWhiteSpace(txtNamaKlien.Text) ||
                string.IsNullOrWhiteSpace(txtEmail.Text) ||
                string.IsNullOrWhiteSpace(txtTelepon.Text) ||
                string.IsNullOrWhiteSpace(txtAlamat.Text))
            {
                MessageBox.Show("Harap isi semua data sebelum memperbarui!", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Validasi format email
            if (!IsValidEmail(txtEmail.Text.Trim()))
            {
                MessageBox.Show("Format email tidak valid!", "Input Tidak Valid", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Validasi no telepon (hanya angka)
            if (!IsValidPhoneNumber(txtTelepon.Text.Trim()))
            {
                MessageBox.Show("No telepon harus berupa angka dan panjang 10-15 digit!", "Input Tidak Valid", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Mendapatkan ID Klien yang dipilih
            object klienIDValue = dgvFormKlien.CurrentRow.Cells["ID Klien"].Value;
            if (klienIDValue == null || klienIDValue == DBNull.Value)
            {
                MessageBox.Show("ID Klien tidak valid!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            int klienID = Convert.ToInt32(klienIDValue);

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                SqlTransaction transaction = conn.BeginTransaction();
                try
                {
                    using (SqlCommand cmd = new SqlCommand("sp_SaveKlien", conn, transaction))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        // Kirim KlienID yang sudah ada untuk operasi UPDATE
                        cmd.Parameters.AddWithValue("@KlienID", klienID);
                        cmd.Parameters.AddWithValue("@NamaKlien", txtNamaKlien.Text.Trim());
                        cmd.Parameters.AddWithValue("@Email", txtEmail.Text.Trim());
                        cmd.Parameters.AddWithValue("@NoTelp", txtTelepon.Text.Trim());
                        cmd.Parameters.AddWithValue("@Alamat", txtAlamat.Text.Trim());

                        cmd.ExecuteNonQuery();
                    }

                    transaction.Commit(); // Commit transaksi jika berhasil
                    MessageBox.Show("Data klien berhasil diperbarui!", "Sukses", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LoadData();
                    ClearForm();
                }
                catch (SqlException ex)
                {
                    transaction.Rollback(); // Rollback transaksi jika ada error
                    MessageBox.Show("Transaksi gagal: " + ex.Message, "Kesalahan", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }



        private void btnDelete_Click(object sender, EventArgs e)
        {
            // Validasi apakah ada baris yang dipilih
            if (dgvFormKlien.CurrentRow == null || dgvFormKlien.CurrentRow.Index < 0)
            {
                MessageBox.Show("Pilih data yang akan dihapus terlebih dahulu!", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Konfirmasi penghapusan
            DialogResult result = MessageBox.Show("Anda yakin ingin menghapus data klien ini?", "Konfirmasi",
                                                 MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.No)
            {
                return;
            }

            // PERBAIKAN 4: Penanganan null value yang lebih baik untuk delete
            object klienIDValue = dgvFormKlien.CurrentRow.Cells["ID Klien"].Value;
            if (klienIDValue == null || klienIDValue == DBNull.Value)
            {
                MessageBox.Show("ID Klien tidak valid!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            int klienID = Convert.ToInt32(klienIDValue);

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                try
                {
                    conn.Open();
                    string query = "DELETE FROM Klien WHERE KlienID = @KlienID";

                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@KlienID", klienID);

                        int rowsAffected = cmd.ExecuteNonQuery();
                        if (rowsAffected > 0)
                        {
                            MessageBox.Show("Data klien berhasil dihapus!", "Sukses", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            LoadData();
                            ClearForm();
                        }
                        else
                        {
                            MessageBox.Show("Data klien tidak berhasil dihapus!", "Kesalahan", MessageBoxButtons.OK, MessageBoxIcon.Error);
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

        // PERBAIKAN 5: Ganti CellContentClick dengan CellClick untuk seleksi yang lebih baik
        private void dgvFormKlien_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.RowIndex < dgvFormKlien.Rows.Count)
            {
                DataGridViewRow row = dgvFormKlien.Rows[e.RowIndex];

                // PERBAIKAN 6: Penanganan null value yang lebih baik
                txtNamaKlien.Text = row.Cells["Nama Klien"].Value?.ToString() ?? "";
                txtEmail.Text = row.Cells["Email"].Value?.ToString() ?? "";
                txtTelepon.Text = row.Cells["No Telepon"].Value?.ToString() ?? "";
                txtAlamat.Text = row.Cells["Alamat"].Value?.ToString() ?? "";
            }
        }

        // PERBAIKAN 7: Event handler untuk CellContentClick juga (untuk kompatibilitas)


        // PERBAIKAN 8: Validasi email yang lebih robust
        private bool IsValidEmail(string email)
        {
            if (string.IsNullOrWhiteSpace(email))
                return false;

            try
            {
                var addr = new System.Net.Mail.MailAddress(email);
                return addr.Address == email && email.Contains("@") && email.Contains(".");
            }
            catch
            {
                return false;
            }
        }

        // PERBAIKAN 9: Validasi nomor telepon yang lebih ketat
        private bool IsValidPhoneNumber(string phoneNumber)
        {
            if (string.IsNullOrWhiteSpace(phoneNumber))
                return false;

            // Hapus spasi dan karakter non-digit
            string cleanNumber = new string(phoneNumber.Where(char.IsDigit).ToArray());

            return cleanNumber.Length >= 10 && cleanNumber.Length <= 15;
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            FormDashboard formMenu = new FormDashboard();
            formMenu.Show();
            this.Hide();
        }

        private void btnAnalisis_Click(object sender, EventArgs e)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                try
                {
                    conn.Open();

                    // Mengaktifkan analisis statistik untuk I/O dan waktu eksekusi
                    SqlCommand cmd = new SqlCommand("SET STATISTICS IO ON; SET STATISTICS TIME ON;", conn);
                    cmd.ExecuteNonQuery();  // Menjalankan perintah untuk mengaktifkan statistik

                    // Menjalankan query untuk memuat data klien
                    string query = @"SELECT KlienID, NamaKlien, Email, NoTelp, Alamat
                             FROM Klien";  // Query tanpa filter tambahan
                    SqlCommand dataCmd = new SqlCommand(query, conn);

                    SqlDataReader reader = dataCmd.ExecuteReader();
                    DataTable dtKlien = new DataTable();
                    dtKlien.Load(reader);  // Mengisi DataTable dengan hasil query

                    // Mengikat DataTable ke BindingSource
                    bindingSource.DataSource = dtKlien;

                    // Menyegarkan DataGridView
                    dgvFormKlien.DataSource = bindingSource;

                    reader.Close();

                    // Menampilkan hasil analisis
                    MessageBox.Show("Analisis query selesai. Periksa Output SQL Server untuk statistik I/O dan waktu eksekusi.",
                                    "Informasi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: " + ex.Message, "Kesalahan", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
        private void btnReport(object sender, EventArgs e)
        {
            ReportKlienForm reportKlienForm = new ReportKlienForm();
            reportKlienForm.Show();
            this.Hide();
            // Setelah laporan ditutup, kembali ke form ini
        }

    }
}

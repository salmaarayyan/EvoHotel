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
using System.Windows.Forms.VisualStyles;

namespace EvoHotel
{
    public partial class FormRoom : Form
    {
        Koneksi kn = new Koneksi();
        string strKonek = "";
        //private string connectionString = "Data Source=LAPTOP-SALMAARA\\SALMALUVIRZA;Initial Catalog=ProjekEvoHotel;Integrated Security=True";
        private int selectedRoomID = -1;

        public FormRoom()
        {
            InitializeComponent();
            InitializeComboBox();
            LoadData();
        }

        private void InitializeComboBox()
        {
            comboBoxStatus.Items.Clear();
            comboBoxStatus.Items.Add("Tersedia");
            comboBoxStatus.Items.Add("Dalam Perbaikan");
            comboBoxStatus.Items.Add("Tidak Tersedia");
            comboBoxStatus.DropDownStyle = ComboBoxStyle.DropDownList;
        }

        private void LoadData()
        {
            using (SqlConnection conn = new SqlConnection(kn.connectionString()))
            {
                try
                {
                    conn.Open();
                    string query = @"SELECT 
                                    RuanganID, 
                                    NamaRuangan, 
                                    Kapasitas, 
                                    HargaPerJam, 
                                    Fasilitas, 
                                    Status 
                                 FROM Ruangan";
                    SqlDataAdapter da = new SqlDataAdapter(query, conn);
                    DataTable dt = new DataTable();
                    da.Fill(dt);

                    dgvFormRoom.AutoGenerateColumns = true;
                    dgvFormRoom.DataSource = dt;

                    ClearForm();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: " + ex.Message, "Kesalahan", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void ClearForm()
        {
            txtNama.Clear();
            txtKapasitas.Clear();
            txtHargaPerJam.Clear();
            txtFasilitas.Clear();
            comboBoxStatus.SelectedIndex = -1;
            selectedRoomID = -1;
        }

        private bool ValidInput()
        {
            if (string.IsNullOrWhiteSpace(txtNama.Text) ||
                string.IsNullOrWhiteSpace(txtKapasitas.Text) ||
                string.IsNullOrWhiteSpace(txtHargaPerJam.Text) ||
                string.IsNullOrWhiteSpace(txtFasilitas.Text) ||
                comboBoxStatus.SelectedIndex == -1)
            {
                MessageBox.Show("Semua field harus diisi!");
                return false;
            }

            if (!int.TryParse(txtKapasitas.Text, out int kapasitas) || kapasitas <= 0)
            {
                MessageBox.Show("Kapasitas harus berupa angka positif!");
                return false;
            }

            if (!decimal.TryParse(txtHargaPerJam.Text, out decimal harga) || harga <= 99000)
            {
                MessageBox.Show("Harga per jam harus lebih dari 99.000!");
                return false;
            }

            return true;
        }

        private void dgvFormRoom_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dgvFormRoom.Rows[e.RowIndex];
                selectedRoomID = Convert.ToInt32(row.Cells["RuanganID"].Value);

                txtNama.Text = row.Cells["NamaRuangan"].Value?.ToString();
                txtKapasitas.Text = row.Cells["Kapasitas"].Value?.ToString();
                txtHargaPerJam.Text = row.Cells["HargaPerJam"].Value?.ToString();
                txtFasilitas.Text = row.Cells["Fasilitas"].Value?.ToString();

                string status = row.Cells["Status"].Value?.ToString();
                if (!string.IsNullOrEmpty(status))
                {
                    int index = comboBoxStatus.Items.IndexOf(status);
                    if (index >= 0) comboBoxStatus.SelectedIndex = index;
                }
            }
        }

        private void btnInsert_Click(object sender, EventArgs e)
        {
            if (!ValidInput()) return;

            using (SqlConnection conn = new SqlConnection(kn.connectionString()))
            {
                conn.Open();
                SqlTransaction transaction = conn.BeginTransaction();
                try
                {
                    using (SqlCommand cmd = new SqlCommand("sp_SaveRuangan", conn, transaction))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@RuanganID", DBNull.Value); // NULL untuk insert
                        cmd.Parameters.AddWithValue("@NamaRuangan", txtNama.Text);
                        cmd.Parameters.AddWithValue("@Kapasitas", int.Parse(txtKapasitas.Text));
                        cmd.Parameters.AddWithValue("@HargaPerJam", decimal.Parse(txtHargaPerJam.Text));
                        cmd.Parameters.AddWithValue("@Fasilitas", txtFasilitas.Text);
                        cmd.Parameters.AddWithValue("@Status", comboBoxStatus.SelectedItem.ToString());

                        cmd.ExecuteNonQuery();
                    }
                    transaction.Commit();
                    MessageBox.Show("Data ruangan berhasil ditambahkan!", "Sukses", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LoadData();
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    MessageBox.Show("Transaksi gagal: " + ex.Message, "Kesalahan", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if (selectedRoomID == -1)
            {
                MessageBox.Show("Pilih data yang akan diubah!");
                return;
            }

            if (!ValidInput()) return;

            using (SqlConnection conn = new SqlConnection(kn.connectionString()))
            {
                conn.Open();
                SqlTransaction transaction = conn.BeginTransaction();
                try
                {
                    using (SqlCommand cmd = new SqlCommand("sp_SaveRuangan", conn, transaction))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@RuanganID", selectedRoomID); // ID untuk update
                        cmd.Parameters.AddWithValue("@NamaRuangan", txtNama.Text);
                        cmd.Parameters.AddWithValue("@Kapasitas", int.Parse(txtKapasitas.Text));
                        cmd.Parameters.AddWithValue("@HargaPerJam", decimal.Parse(txtHargaPerJam.Text));
                        cmd.Parameters.AddWithValue("@Fasilitas", txtFasilitas.Text);
                        cmd.Parameters.AddWithValue("@Status", comboBoxStatus.SelectedItem.ToString());

                        cmd.ExecuteNonQuery();
                    }
                    transaction.Commit();
                    MessageBox.Show("Data ruangan berhasil diperbarui!", "Sukses", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LoadData();
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    MessageBox.Show("Transaksi gagal: " + ex.Message, "Kesalahan", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (selectedRoomID == -1)
            {
                MessageBox.Show("Pilih data yang akan dihapus!");
                return;
            }

            DialogResult result = MessageBox.Show("Yakin ingin menghapus data ruangan ini?", "Konfirmasi", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.No) return;

            using (SqlConnection conn = new SqlConnection(kn.connectionString()))
            {
                try
                {
                    conn.Open();
                    string query = "DELETE FROM Ruangan WHERE RuanganID = @ID";
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@ID", selectedRoomID);
                        int rowsAffected = cmd.ExecuteNonQuery();

                        if (rowsAffected > 0)
                        {
                            MessageBox.Show("Data berhasil dihapus!", "Sukses", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            LoadData();
                        }
                        else
                        {
                            MessageBox.Show("Data gagal dihapus!", "Kesalahan", MessageBoxButtons.OK, MessageBoxIcon.Error);
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

        private void btnBack_Click(object sender, EventArgs e)
        {
            FormDashboard formMenu = new FormDashboard();
            formMenu.Show();
            this.Hide();
        }

        private void comboBoxStatus_SelectedIndexChanged(object sender, EventArgs e)
        {
            comboBoxStatus.BackColor = SystemColors.Window;
        }

        private void btnAnalisis_Click(object sender, EventArgs e)
        {
            using (SqlConnection conn = new SqlConnection(kn.connectionString()))
            {
                try
                {
                    StringBuilder statistik = new StringBuilder();
                    conn.InfoMessage += (s, args) =>
                    {
                        statistik.AppendLine(args.Message);
                    };

                    conn.Open();

                    // Aktifkan statistik
                    SqlCommand cmd = new SqlCommand("SET STATISTICS IO ON; SET STATISTICS TIME ON;", conn);
                    cmd.ExecuteNonQuery();

                    // Jalankan query yang ingin dianalisis (bisa tanpa filter, atau sesuai kebutuhan)
                    string query = @"SELECT RuanganID, NamaRuangan, Kapasitas, HargaPerJam, Fasilitas, Status FROM Ruangan";
                    SqlCommand dataCmd = new SqlCommand(query, conn);
                    SqlDataReader reader = dataCmd.ExecuteReader();
                    DataTable dtRoom = new DataTable();
                    dtRoom.Load(reader);
                    dgvFormRoom.DataSource = dtRoom;
                    reader.Close();

                    // Tampilkan hasil statistik
                    if (statistik.Length > 0)
                        MessageBox.Show(statistik.ToString(), "STATISTICS INFO");
                    else
                        MessageBox.Show("Tidak ada statistik yang diterima.", "STATISTICS INFO");
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: " + ex.Message, "Kesalahan", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void btnReport(object sender, EventArgs e)
        {
            ReportRoomForm reportForm = new ReportRoomForm();
            reportForm.Show();
            this.Hide();
            // Setelah laporan ditutup, kembali ke form ini
        }
    }

}

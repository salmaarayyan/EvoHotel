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
    public partial class FormDashboard : Form
    {
        public FormDashboard()
        {
            InitializeComponent();
        }

        private void Client_Click(object sender, EventArgs e)
        {
            FormClient formKlien = new FormClient();
            formKlien.Show();
            this.Hide();
        }

        private void Event_Click(object sender, EventArgs e)
        {
            FormEvent formAcara = new FormEvent();
            formAcara.Show();
            this.Hide();
        }

        private void Room_Click(object sender, EventArgs e)
        {
            FormRoom formRuangan = new FormRoom();
            formRuangan.Show();
            this.Hide();
        }

        private void Order_Click(object sender, EventArgs e)
        {
            FormOrder formPemesanan = new FormOrder();
            formPemesanan.Show();
            this.Hide();
        }

        private void Invoice_Click(object sender, EventArgs e)
        {
            FormInvoice formDataPembayaran = new FormInvoice();
            formDataPembayaran.Show();
            this.Hide();
        }
    }
}

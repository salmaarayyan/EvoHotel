namespace EvoHotel
{
    partial class FormInvoice
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormInvoice));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.txtTotal = new System.Windows.Forms.TextBox();
            this.comboBoxStatus = new System.Windows.Forms.ComboBox();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.button4 = new System.Windows.Forms.Button();
            this.Back = new System.Windows.Forms.PictureBox();
            this.comboBoxKlien = new System.Windows.Forms.ComboBox();
            this.comboBoxPemesanan = new System.Windows.Forms.ComboBox();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.comboBoxMetodePembayaran = new System.Windows.Forms.ComboBox();
            this.dgvDataPembayaran = new System.Windows.Forms.DataGridView();
            this.button5 = new System.Windows.Forms.Button();
            this.button6 = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.button8 = new System.Windows.Forms.Button();
            this.label6 = new System.Windows.Forms.Label();
            this.lblBuktiPembayaran = new System.Windows.Forms.Label();
            this.button7 = new System.Windows.Forms.Button();
            this.panelInvoice = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            ((System.ComponentModel.ISupportInitialize)(this.Back)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvDataPembayaran)).BeginInit();
            this.panelInvoice.SuspendLayout();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Font = new System.Drawing.Font("Gill Sans Ultra Bold", 13.875F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.label1.Location = new System.Drawing.Point(159, 33);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(1062, 51);
            this.label1.TabIndex = 0;
            this.label1.Text = "FORMULIR MANAJEMEN DATA PEMBAYARAN";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.Linen;
            this.label2.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.label2.Location = new System.Drawing.Point(124, 241);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(60, 25);
            this.label2.TabIndex = 1;
            this.label2.Text = "Total";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.BackColor = System.Drawing.Color.Linen;
            this.label3.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.label3.Location = new System.Drawing.Point(124, 351);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(211, 25);
            this.label3.TabIndex = 2;
            this.label3.Text = "Metode Pembayaran";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.BackColor = System.Drawing.Color.Linen;
            this.label5.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.label5.Location = new System.Drawing.Point(124, 295);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(73, 25);
            this.label5.TabIndex = 4;
            this.label5.Text = "Status";
            // 
            // txtTotal
            // 
            this.txtTotal.Location = new System.Drawing.Point(357, 238);
            this.txtTotal.Name = "txtTotal";
            this.txtTotal.Size = new System.Drawing.Size(508, 31);
            this.txtTotal.TabIndex = 6;
            // 
            // comboBoxStatus
            // 
            this.comboBoxStatus.FormattingEnabled = true;
            this.comboBoxStatus.Location = new System.Drawing.Point(357, 292);
            this.comboBoxStatus.Name = "comboBoxStatus";
            this.comboBoxStatus.Size = new System.Drawing.Size(508, 33);
            this.comboBoxStatus.TabIndex = 9;
            this.comboBoxStatus.Click += new System.EventHandler(this.comboBoxStatus_SelectedIndexChanged);
            // 
            // button1
            // 
            this.button1.BackColor = System.Drawing.SystemColors.MenuHighlight;
            this.button1.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.button1.Location = new System.Drawing.Point(961, 135);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(137, 59);
            this.button1.TabIndex = 11;
            this.button1.Text = "Simpan";
            this.button1.UseVisualStyleBackColor = false;
            this.button1.Click += new System.EventHandler(this.btnInsert_Click);
            // 
            // button2
            // 
            this.button2.BackColor = System.Drawing.Color.Red;
            this.button2.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.button2.Location = new System.Drawing.Point(1124, 135);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(137, 59);
            this.button2.TabIndex = 12;
            this.button2.Text = "Hapus";
            this.button2.UseVisualStyleBackColor = false;
            this.button2.Click += new System.EventHandler(this.btnDelete_Click);
            // 
            // button3
            // 
            this.button3.BackColor = System.Drawing.Color.Gold;
            this.button3.Location = new System.Drawing.Point(961, 210);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(137, 59);
            this.button3.TabIndex = 13;
            this.button3.Text = "Update";
            this.button3.UseVisualStyleBackColor = false;
            this.button3.Click += new System.EventHandler(this.btnEdit_Click);
            // 
            // button4
            // 
            this.button4.BackColor = System.Drawing.SystemColors.ControlDark;
            this.button4.Location = new System.Drawing.Point(1124, 210);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(137, 59);
            this.button4.TabIndex = 14;
            this.button4.Text = "Refresh";
            this.button4.UseVisualStyleBackColor = false;
            this.button4.Click += new System.EventHandler(this.btnRefresh_Click);
            // 
            // Back
            // 
            this.Back.BackColor = System.Drawing.Color.Linen;
            this.Back.Image = ((System.Drawing.Image)(resources.GetObject("Back.Image")));
            this.Back.Location = new System.Drawing.Point(45, 794);
            this.Back.Name = "Back";
            this.Back.Size = new System.Drawing.Size(135, 124);
            this.Back.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.Back.TabIndex = 15;
            this.Back.TabStop = false;
            this.Back.Click += new System.EventHandler(this.btnBack_Click);
            // 
            // comboBoxKlien
            // 
            this.comboBoxKlien.FormattingEnabled = true;
            this.comboBoxKlien.Location = new System.Drawing.Point(357, 135);
            this.comboBoxKlien.Name = "comboBoxKlien";
            this.comboBoxKlien.Size = new System.Drawing.Size(508, 33);
            this.comboBoxKlien.TabIndex = 18;
            // 
            // comboBoxPemesanan
            // 
            this.comboBoxPemesanan.FormattingEnabled = true;
            this.comboBoxPemesanan.Location = new System.Drawing.Point(357, 184);
            this.comboBoxPemesanan.Name = "comboBoxPemesanan";
            this.comboBoxPemesanan.Size = new System.Drawing.Size(508, 33);
            this.comboBoxPemesanan.TabIndex = 19;
            this.comboBoxPemesanan.SelectedIndexChanged += new System.EventHandler(this.comboBoxPemesanan_SelectedIndexChanged);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.BackColor = System.Drawing.Color.Linen;
            this.label7.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.label7.Location = new System.Drawing.Point(124, 135);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(60, 25);
            this.label7.TabIndex = 20;
            this.label7.Text = "Klien";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.BackColor = System.Drawing.Color.Linen;
            this.label8.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.label8.Location = new System.Drawing.Point(124, 184);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(126, 25);
            this.label8.TabIndex = 21;
            this.label8.Text = "Pemesanan";
            // 
            // comboBoxMetodePembayaran
            // 
            this.comboBoxMetodePembayaran.FormattingEnabled = true;
            this.comboBoxMetodePembayaran.Location = new System.Drawing.Point(357, 345);
            this.comboBoxMetodePembayaran.Name = "comboBoxMetodePembayaran";
            this.comboBoxMetodePembayaran.Size = new System.Drawing.Size(508, 33);
            this.comboBoxMetodePembayaran.TabIndex = 22;
            // 
            // dgvDataPembayaran
            // 
            this.dgvDataPembayaran.BackgroundColor = System.Drawing.Color.AntiqueWhite;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.875F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.GradientInactiveCaption;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvDataPembayaran.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dgvDataPembayaran.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvDataPembayaran.Location = new System.Drawing.Point(45, 498);
            this.dgvDataPembayaran.Name = "dgvDataPembayaran";
            this.dgvDataPembayaran.RowHeadersWidth = 82;
            this.dgvDataPembayaran.RowTemplate.Height = 33;
            this.dgvDataPembayaran.Size = new System.Drawing.Size(1374, 324);
            this.dgvDataPembayaran.TabIndex = 23;
            this.dgvDataPembayaran.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvDataPembayaran_CellContentClick);
            // 
            // button5
            // 
            this.button5.Location = new System.Drawing.Point(961, 289);
            this.button5.Name = "button5";
            this.button5.Size = new System.Drawing.Size(137, 57);
            this.button5.TabIndex = 24;
            this.button5.Text = "Analisis";
            this.button5.UseVisualStyleBackColor = true;
            this.button5.Click += new System.EventHandler(this.BtnAnalisis_Click);
            // 
            // button6
            // 
            this.button6.Location = new System.Drawing.Point(1124, 289);
            this.button6.Name = "button6";
            this.button6.Size = new System.Drawing.Size(137, 55);
            this.button6.TabIndex = 25;
            this.button6.Text = "Report";
            this.button6.UseVisualStyleBackColor = true;
            this.button6.Click += new System.EventHandler(this.Report_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.BackColor = System.Drawing.Color.Linen;
            this.label4.Location = new System.Drawing.Point(124, 403);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(331, 25);
            this.label4.TabIndex = 26;
            this.label4.Text = "Lampiran Foto Bukti Pembayaran";
            // 
            // button8
            // 
            this.button8.Location = new System.Drawing.Point(129, 442);
            this.button8.Name = "button8";
            this.button8.Size = new System.Drawing.Size(135, 46);
            this.button8.TabIndex = 28;
            this.button8.Text = "Browse...";
            this.button8.UseVisualStyleBackColor = true;
            this.button8.Click += new System.EventHandler(this.btnBrowseBukti_Click);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(403, 497);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(0, 25);
            this.label6.TabIndex = 29;
            // 
            // lblBuktiPembayaran
            // 
            this.lblBuktiPembayaran.AutoSize = true;
            this.lblBuktiPembayaran.BackColor = System.Drawing.Color.Linen;
            this.lblBuktiPembayaran.Location = new System.Drawing.Point(270, 453);
            this.lblBuktiPembayaran.Name = "lblBuktiPembayaran";
            this.lblBuktiPembayaran.Size = new System.Drawing.Size(166, 25);
            this.lblBuktiPembayaran.TabIndex = 30;
            this.lblBuktiPembayaran.Text = "No file selected.";
            // 
            // button7
            // 
            this.button7.Location = new System.Drawing.Point(961, 372);
            this.button7.Name = "button7";
            this.button7.Size = new System.Drawing.Size(300, 48);
            this.button7.TabIndex = 31;
            this.button7.Text = "Lihat Bukti Pembayaran";
            this.button7.UseVisualStyleBackColor = true;
            this.button7.Click += new System.EventHandler(this.btnLihatBukti_Click);
            // 
            // panelInvoice
            // 
            this.panelInvoice.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.panelInvoice.BackColor = System.Drawing.Color.Linen;
            this.panelInvoice.Controls.Add(this.lblBuktiPembayaran);
            this.panelInvoice.Controls.Add(this.dgvDataPembayaran);
            this.panelInvoice.Controls.Add(this.button7);
            this.panelInvoice.Controls.Add(this.panel2);
            this.panelInvoice.Controls.Add(this.button8);
            this.panelInvoice.Controls.Add(this.button2);
            this.panelInvoice.Controls.Add(this.label4);
            this.panelInvoice.Controls.Add(this.button1);
            this.panelInvoice.Controls.Add(this.button3);
            this.panelInvoice.Controls.Add(this.comboBoxMetodePembayaran);
            this.panelInvoice.Controls.Add(this.button4);
            this.panelInvoice.Controls.Add(this.label8);
            this.panelInvoice.Controls.Add(this.button6);
            this.panelInvoice.Controls.Add(this.label7);
            this.panelInvoice.Controls.Add(this.button5);
            this.panelInvoice.Controls.Add(this.comboBoxPemesanan);
            this.panelInvoice.Controls.Add(this.Back);
            this.panelInvoice.Controls.Add(this.comboBoxKlien);
            this.panelInvoice.Controls.Add(this.comboBoxStatus);
            this.panelInvoice.Controls.Add(this.label2);
            this.panelInvoice.Controls.Add(this.txtTotal);
            this.panelInvoice.Controls.Add(this.label3);
            this.panelInvoice.Controls.Add(this.label5);
            this.panelInvoice.Location = new System.Drawing.Point(-4, -1);
            this.panelInvoice.Name = "panelInvoice";
            this.panelInvoice.Size = new System.Drawing.Size(1457, 894);
            this.panelInvoice.TabIndex = 32;
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.SaddleBrown;
            this.panel2.Controls.Add(this.label1);
            this.panel2.Location = new System.Drawing.Point(0, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(1457, 100);
            this.panel2.TabIndex = 0;
            // 
            // FormInvoice
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.ClientSize = new System.Drawing.Size(1450, 888);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.panelInvoice);
            this.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.Name = "FormInvoice";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "FormInvoice";
            ((System.ComponentModel.ISupportInitialize)(this.Back)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvDataPembayaran)).EndInit();
            this.panelInvoice.ResumeLayout(false);
            this.panelInvoice.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txtTotal;
        private System.Windows.Forms.ComboBox comboBoxStatus;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.PictureBox Back;
        private System.Windows.Forms.ComboBox comboBoxKlien;
        private System.Windows.Forms.ComboBox comboBoxPemesanan;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.ComboBox comboBoxMetodePembayaran;
        private System.Windows.Forms.DataGridView dgvDataPembayaran;
        private System.Windows.Forms.Button button5;
        private System.Windows.Forms.Button button6;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button button8;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label lblBuktiPembayaran;
        private System.Windows.Forms.Button button7;
        private System.Windows.Forms.Panel panelInvoice;
        private System.Windows.Forms.Panel panel2;
    }
}
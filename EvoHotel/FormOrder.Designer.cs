namespace EvoHotel
{
    partial class FormOrder
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormOrder));
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.txtTotalHarga = new System.Windows.Forms.TextBox();
            this.comboBoxStatus = new System.Windows.Forms.ComboBox();
            this.dgvDataPemesanan = new System.Windows.Forms.DataGridView();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.button4 = new System.Windows.Forms.Button();
            this.Back = new System.Windows.Forms.PictureBox();
            this.comboBoxKlien = new System.Windows.Forms.ComboBox();
            this.comboBoxAcara = new System.Windows.Forms.ComboBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.txtNamaRuangan = new System.Windows.Forms.TextBox();
            this.button5 = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            ((System.ComponentModel.ISupportInitialize)(this.dgvDataPemesanan)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Back)).BeginInit();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Font = new System.Drawing.Font("Gill Sans Ultra Bold", 13.875F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.label1.Location = new System.Drawing.Point(288, 28);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(868, 51);
            this.label1.TabIndex = 0;
            this.label1.Text = "FORMULIR MANAJEMEN PEMESANAN";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.Linen;
            this.label2.Location = new System.Drawing.Point(87, 340);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(124, 25);
            this.label2.TabIndex = 1;
            this.label2.Text = "Total Harga";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.BackColor = System.Drawing.Color.Linen;
            this.label4.Location = new System.Drawing.Point(87, 396);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(73, 25);
            this.label4.TabIndex = 3;
            this.label4.Text = "Status";
            // 
            // txtTotalHarga
            // 
            this.txtTotalHarga.Location = new System.Drawing.Point(287, 337);
            this.txtTotalHarga.Name = "txtTotalHarga";
            this.txtTotalHarga.Size = new System.Drawing.Size(708, 31);
            this.txtTotalHarga.TabIndex = 5;
            // 
            // comboBoxStatus
            // 
            this.comboBoxStatus.FormattingEnabled = true;
            this.comboBoxStatus.Location = new System.Drawing.Point(287, 393);
            this.comboBoxStatus.Name = "comboBoxStatus";
            this.comboBoxStatus.Size = new System.Drawing.Size(708, 33);
            this.comboBoxStatus.TabIndex = 6;
            this.comboBoxStatus.Click += new System.EventHandler(this.comboBoxStatus_SelectedIndexChanged);
            // 
            // dgvDataPemesanan
            // 
            this.dgvDataPemesanan.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.dgvDataPemesanan.BackgroundColor = System.Drawing.Color.AntiqueWhite;
            this.dgvDataPemesanan.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvDataPemesanan.Location = new System.Drawing.Point(75, 457);
            this.dgvDataPemesanan.Name = "dgvDataPemesanan";
            this.dgvDataPemesanan.RowHeadersWidth = 82;
            this.dgvDataPemesanan.RowTemplate.Height = 33;
            this.dgvDataPemesanan.Size = new System.Drawing.Size(1328, 324);
            this.dgvDataPemesanan.TabIndex = 7;
            this.dgvDataPemesanan.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvDataPemesanan_CellContentClick);
            // 
            // button1
            // 
            this.button1.BackColor = System.Drawing.SystemColors.MenuHighlight;
            this.button1.ForeColor = System.Drawing.SystemColors.HighlightText;
            this.button1.Location = new System.Drawing.Point(1067, 182);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(139, 66);
            this.button1.TabIndex = 8;
            this.button1.Text = "Simpan";
            this.button1.UseVisualStyleBackColor = false;
            this.button1.Click += new System.EventHandler(this.btnInsert_Click);
            // 
            // button2
            // 
            this.button2.BackColor = System.Drawing.Color.Red;
            this.button2.ForeColor = System.Drawing.SystemColors.HighlightText;
            this.button2.Location = new System.Drawing.Point(1239, 182);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(139, 66);
            this.button2.TabIndex = 9;
            this.button2.Text = "Hapus";
            this.button2.UseVisualStyleBackColor = false;
            this.button2.Click += new System.EventHandler(this.btnDelete_Click);
            // 
            // button3
            // 
            this.button3.BackColor = System.Drawing.Color.Gold;
            this.button3.ForeColor = System.Drawing.SystemColors.HighlightText;
            this.button3.Location = new System.Drawing.Point(1067, 264);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(139, 66);
            this.button3.TabIndex = 10;
            this.button3.Text = "Update";
            this.button3.UseVisualStyleBackColor = false;
            this.button3.Click += new System.EventHandler(this.btnEdit_Click);
            // 
            // button4
            // 
            this.button4.BackColor = System.Drawing.SystemColors.ControlDark;
            this.button4.ForeColor = System.Drawing.SystemColors.HighlightText;
            this.button4.Location = new System.Drawing.Point(1239, 264);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(139, 66);
            this.button4.TabIndex = 11;
            this.button4.Text = "Refresh";
            this.button4.UseVisualStyleBackColor = false;
            this.button4.Click += new System.EventHandler(this.btnRefresh_Click);
            // 
            // Back
            // 
            this.Back.BackColor = System.Drawing.Color.Linen;
            this.Back.Image = ((System.Drawing.Image)(resources.GetObject("Back.Image")));
            this.Back.Location = new System.Drawing.Point(75, 763);
            this.Back.Name = "Back";
            this.Back.Size = new System.Drawing.Size(147, 137);
            this.Back.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.Back.TabIndex = 12;
            this.Back.TabStop = false;
            this.Back.Click += new System.EventHandler(this.btnBack_Click);
            // 
            // comboBoxKlien
            // 
            this.comboBoxKlien.FormattingEnabled = true;
            this.comboBoxKlien.Location = new System.Drawing.Point(287, 179);
            this.comboBoxKlien.Name = "comboBoxKlien";
            this.comboBoxKlien.Size = new System.Drawing.Size(708, 33);
            this.comboBoxKlien.TabIndex = 13;
            // 
            // comboBoxAcara
            // 
            this.comboBoxAcara.FormattingEnabled = true;
            this.comboBoxAcara.Location = new System.Drawing.Point(287, 236);
            this.comboBoxAcara.Name = "comboBoxAcara";
            this.comboBoxAcara.Size = new System.Drawing.Size(708, 33);
            this.comboBoxAcara.TabIndex = 14;
            this.comboBoxAcara.SelectedIndexChanged += new System.EventHandler(this.comboBoxAcara_SelectedIndexChanged);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.BackColor = System.Drawing.Color.Linen;
            this.label5.Location = new System.Drawing.Point(87, 182);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(60, 25);
            this.label5.TabIndex = 15;
            this.label5.Text = "Klien";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.BackColor = System.Drawing.Color.Linen;
            this.label6.Location = new System.Drawing.Point(87, 236);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(68, 25);
            this.label6.TabIndex = 16;
            this.label6.Text = "Acara";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.BackColor = System.Drawing.Color.Linen;
            this.label3.Location = new System.Drawing.Point(88, 288);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(161, 25);
            this.label3.TabIndex = 17;
            this.label3.Text = "Nama Ruangan";
            // 
            // txtNamaRuangan
            // 
            this.txtNamaRuangan.Location = new System.Drawing.Point(287, 285);
            this.txtNamaRuangan.Name = "txtNamaRuangan";
            this.txtNamaRuangan.Size = new System.Drawing.Size(708, 31);
            this.txtNamaRuangan.TabIndex = 18;
            // 
            // button5
            // 
            this.button5.Location = new System.Drawing.Point(1067, 367);
            this.button5.Name = "button5";
            this.button5.Size = new System.Drawing.Size(311, 59);
            this.button5.TabIndex = 19;
            this.button5.Text = "Lihat Report";
            this.button5.UseVisualStyleBackColor = true;
            this.button5.Click += new System.EventHandler(this.btnReport_Click);
            // 
            // panel1
            // 
            this.panel1.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.panel1.BackColor = System.Drawing.Color.Linen;
            this.panel1.Controls.Add(this.dgvDataPemesanan);
            this.panel1.Controls.Add(this.txtNamaRuangan);
            this.panel1.Controls.Add(this.button5);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.panel2);
            this.panel1.Controls.Add(this.Back);
            this.panel1.Controls.Add(this.label6);
            this.panel1.Controls.Add(this.button2);
            this.panel1.Controls.Add(this.label5);
            this.panel1.Controls.Add(this.button1);
            this.panel1.Controls.Add(this.comboBoxAcara);
            this.panel1.Controls.Add(this.button3);
            this.panel1.Controls.Add(this.comboBoxKlien);
            this.panel1.Controls.Add(this.button4);
            this.panel1.Controls.Add(this.comboBoxStatus);
            this.panel1.Controls.Add(this.txtTotalHarga);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.label4);
            this.panel1.Location = new System.Drawing.Point(2, 1);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1453, 888);
            this.panel1.TabIndex = 20;
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.SaddleBrown;
            this.panel2.Controls.Add(this.label1);
            this.panel2.Location = new System.Drawing.Point(0, -4);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(1455, 108);
            this.panel2.TabIndex = 0;
            // 
            // FormOrder
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.ClientSize = new System.Drawing.Size(1450, 888);
            this.Controls.Add(this.panel1);
            this.Name = "FormOrder";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "FormOrder";
            ((System.ComponentModel.ISupportInitialize)(this.dgvDataPemesanan)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Back)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtTotalHarga;
        private System.Windows.Forms.ComboBox comboBoxStatus;
        private System.Windows.Forms.DataGridView dgvDataPemesanan;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.PictureBox Back;
        private System.Windows.Forms.ComboBox comboBoxKlien;
        private System.Windows.Forms.ComboBox comboBoxAcara;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtNamaRuangan;
        private Microsoft.Reporting.WinForms.ReportViewer reportViewer;
        private System.Windows.Forms.Button button5;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
    }
}
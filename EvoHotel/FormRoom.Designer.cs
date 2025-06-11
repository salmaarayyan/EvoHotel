namespace EvoHotel
{
    partial class FormRoom
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormRoom));
            this.label1 = new System.Windows.Forms.Label();
            this.Nama = new System.Windows.Forms.Label();
            this.Kapasitas = new System.Windows.Forms.Label();
            this.HargaPerJam = new System.Windows.Forms.Label();
            this.Fasilitas = new System.Windows.Forms.Label();
            this.Status = new System.Windows.Forms.Label();
            this.txtNama = new System.Windows.Forms.TextBox();
            this.txtKapasitas = new System.Windows.Forms.TextBox();
            this.txtHargaPerJam = new System.Windows.Forms.TextBox();
            this.txtFasilitas = new System.Windows.Forms.TextBox();
            this.dgvFormRoom = new System.Windows.Forms.DataGridView();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.button4 = new System.Windows.Forms.Button();
            this.Back = new System.Windows.Forms.PictureBox();
            this.comboBoxStatus = new System.Windows.Forms.ComboBox();
            this.button5 = new System.Windows.Forms.Button();
            this.button6 = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dgvFormRoom)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Back)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.label1.Font = new System.Drawing.Font("Century Gothic", 13.875F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.Transparent;
            this.label1.Location = new System.Drawing.Point(418, 46);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(628, 46);
            this.label1.TabIndex = 0;
            this.label1.Text = "FORMULIR MANAJEMEN RUANGAN";
            // 
            // Nama
            // 
            this.Nama.AutoSize = true;
            this.Nama.Location = new System.Drawing.Point(103, 119);
            this.Nama.Name = "Nama";
            this.Nama.Size = new System.Drawing.Size(68, 25);
            this.Nama.TabIndex = 1;
            this.Nama.Text = "Nama";
            // 
            // Kapasitas
            // 
            this.Kapasitas.AutoSize = true;
            this.Kapasitas.Location = new System.Drawing.Point(103, 170);
            this.Kapasitas.Name = "Kapasitas";
            this.Kapasitas.Size = new System.Drawing.Size(107, 25);
            this.Kapasitas.TabIndex = 2;
            this.Kapasitas.Text = "Kapasitas";
            // 
            // HargaPerJam
            // 
            this.HargaPerJam.AutoSize = true;
            this.HargaPerJam.Location = new System.Drawing.Point(103, 228);
            this.HargaPerJam.Name = "HargaPerJam";
            this.HargaPerJam.Size = new System.Drawing.Size(161, 25);
            this.HargaPerJam.TabIndex = 3;
            this.HargaPerJam.Text = "Harga (perJam)";
            // 
            // Fasilitas
            // 
            this.Fasilitas.AutoSize = true;
            this.Fasilitas.Location = new System.Drawing.Point(103, 279);
            this.Fasilitas.Name = "Fasilitas";
            this.Fasilitas.Size = new System.Drawing.Size(92, 25);
            this.Fasilitas.TabIndex = 4;
            this.Fasilitas.Text = "Fasilitas";
            // 
            // Status
            // 
            this.Status.AutoSize = true;
            this.Status.Location = new System.Drawing.Point(103, 331);
            this.Status.Name = "Status";
            this.Status.Size = new System.Drawing.Size(73, 25);
            this.Status.TabIndex = 5;
            this.Status.Text = "Status";
            // 
            // txtNama
            // 
            this.txtNama.Location = new System.Drawing.Point(282, 116);
            this.txtNama.Name = "txtNama";
            this.txtNama.Size = new System.Drawing.Size(595, 31);
            this.txtNama.TabIndex = 6;
            // 
            // txtKapasitas
            // 
            this.txtKapasitas.Location = new System.Drawing.Point(282, 167);
            this.txtKapasitas.Name = "txtKapasitas";
            this.txtKapasitas.Size = new System.Drawing.Size(595, 31);
            this.txtKapasitas.TabIndex = 7;
            // 
            // txtHargaPerJam
            // 
            this.txtHargaPerJam.Location = new System.Drawing.Point(282, 225);
            this.txtHargaPerJam.Name = "txtHargaPerJam";
            this.txtHargaPerJam.Size = new System.Drawing.Size(595, 31);
            this.txtHargaPerJam.TabIndex = 8;
            // 
            // txtFasilitas
            // 
            this.txtFasilitas.Location = new System.Drawing.Point(282, 276);
            this.txtFasilitas.Name = "txtFasilitas";
            this.txtFasilitas.Size = new System.Drawing.Size(595, 31);
            this.txtFasilitas.TabIndex = 9;
            // 
            // dgvFormRoom
            // 
            this.dgvFormRoom.BackgroundColor = System.Drawing.SystemColors.ControlLight;
            this.dgvFormRoom.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvFormRoom.Location = new System.Drawing.Point(88, 395);
            this.dgvFormRoom.Name = "dgvFormRoom";
            this.dgvFormRoom.RowHeadersWidth = 82;
            this.dgvFormRoom.RowTemplate.Height = 33;
            this.dgvFormRoom.Size = new System.Drawing.Size(1190, 357);
            this.dgvFormRoom.TabIndex = 11;
            this.dgvFormRoom.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvFormRoom_CellContentClick);
            // 
            // button1
            // 
            this.button1.BackColor = System.Drawing.SystemColors.MenuHighlight;
            this.button1.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.button1.Location = new System.Drawing.Point(921, 157);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(140, 67);
            this.button1.TabIndex = 12;
            this.button1.Text = "Simpan";
            this.button1.UseVisualStyleBackColor = false;
            this.button1.Click += new System.EventHandler(this.btnInsert_Click);
            // 
            // button2
            // 
            this.button2.BackColor = System.Drawing.Color.Red;
            this.button2.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.button2.Location = new System.Drawing.Point(1101, 157);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(140, 67);
            this.button2.TabIndex = 13;
            this.button2.Text = "Hapus";
            this.button2.UseVisualStyleBackColor = false;
            this.button2.Click += new System.EventHandler(this.btnDelete_Click);
            // 
            // button3
            // 
            this.button3.BackColor = System.Drawing.Color.Gold;
            this.button3.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.button3.Location = new System.Drawing.Point(921, 240);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(140, 67);
            this.button3.TabIndex = 14;
            this.button3.Text = "Update";
            this.button3.UseVisualStyleBackColor = false;
            this.button3.Click += new System.EventHandler(this.btnUpdate_Click);
            // 
            // button4
            // 
            this.button4.BackColor = System.Drawing.SystemColors.ControlDark;
            this.button4.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.button4.Location = new System.Drawing.Point(1101, 240);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(140, 67);
            this.button4.TabIndex = 15;
            this.button4.Text = "Refresh";
            this.button4.UseVisualStyleBackColor = false;
            this.button4.Click += new System.EventHandler(this.btnRefresh_Click);
            // 
            // Back
            // 
            this.Back.Image = ((System.Drawing.Image)(resources.GetObject("Back.Image")));
            this.Back.Location = new System.Drawing.Point(88, 728);
            this.Back.Name = "Back";
            this.Back.Size = new System.Drawing.Size(145, 128);
            this.Back.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.Back.TabIndex = 16;
            this.Back.TabStop = false;
            this.Back.Click += new System.EventHandler(this.btnBack_Click);
            // 
            // comboBoxStatus
            // 
            this.comboBoxStatus.FormattingEnabled = true;
            this.comboBoxStatus.Location = new System.Drawing.Point(282, 328);
            this.comboBoxStatus.Name = "comboBoxStatus";
            this.comboBoxStatus.Size = new System.Drawing.Size(595, 33);
            this.comboBoxStatus.TabIndex = 17;
            this.comboBoxStatus.SelectedIndexChanged += new System.EventHandler(this.comboBoxStatus_SelectedIndexChanged);
            // 
            // button5
            // 
            this.button5.Location = new System.Drawing.Point(1056, 770);
            this.button5.Name = "button5";
            this.button5.Size = new System.Drawing.Size(104, 49);
            this.button5.TabIndex = 18;
            this.button5.Text = "Analisis";
            this.button5.UseVisualStyleBackColor = true;
            this.button5.Click += new System.EventHandler(this.btnAnalisis_Click);
            // 
            // button6
            // 
            this.button6.Location = new System.Drawing.Point(1175, 770);
            this.button6.Name = "button6";
            this.button6.Size = new System.Drawing.Size(103, 49);
            this.button6.TabIndex = 19;
            this.button6.Text = "Report";
            this.button6.UseVisualStyleBackColor = true;
            this.button6.Click += new System.EventHandler(this.btnReport);
            // 
            // FormRoom
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.WhiteSmoke;
            this.ClientSize = new System.Drawing.Size(1350, 859);
            this.Controls.Add(this.button6);
            this.Controls.Add(this.button5);
            this.Controls.Add(this.dgvFormRoom);
            this.Controls.Add(this.comboBoxStatus);
            this.Controls.Add(this.Back);
            this.Controls.Add(this.button4);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.txtFasilitas);
            this.Controls.Add(this.txtHargaPerJam);
            this.Controls.Add(this.txtKapasitas);
            this.Controls.Add(this.txtNama);
            this.Controls.Add(this.Status);
            this.Controls.Add(this.Fasilitas);
            this.Controls.Add(this.HargaPerJam);
            this.Controls.Add(this.Kapasitas);
            this.Controls.Add(this.Nama);
            this.Controls.Add(this.label1);
            this.Name = "FormRoom";
            this.Text = "FormRuangan";
            ((System.ComponentModel.ISupportInitialize)(this.dgvFormRoom)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Back)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label Nama;
        private System.Windows.Forms.Label Kapasitas;
        private System.Windows.Forms.Label HargaPerJam;
        private System.Windows.Forms.Label Fasilitas;
        private System.Windows.Forms.Label Status;
        private System.Windows.Forms.TextBox txtNama;
        private System.Windows.Forms.TextBox txtKapasitas;
        private System.Windows.Forms.TextBox txtHargaPerJam;
        private System.Windows.Forms.TextBox txtFasilitas;
        private System.Windows.Forms.DataGridView dgvFormRoom;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.PictureBox Back;
        private System.Windows.Forms.ComboBox comboBoxStatus;
        private System.Windows.Forms.Button button5;
        private System.Windows.Forms.Button button6;
    }
}
namespace EvoHotel
{
    partial class FormClient
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormClient));
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.txtNamaKlien = new System.Windows.Forms.TextBox();
            this.txtEmail = new System.Windows.Forms.TextBox();
            this.txtTelepon = new System.Windows.Forms.TextBox();
            this.txtAlamat = new System.Windows.Forms.TextBox();
            this.dgvFormKlien = new System.Windows.Forms.DataGridView();
            this.Back = new System.Windows.Forms.PictureBox();
            this.btnInsert = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.button4 = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.button5 = new System.Windows.Forms.Button();
            this.panelKlien = new System.Windows.Forms.Panel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.label5 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dgvFormKlien)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Back)).BeginInit();
            this.panelKlien.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(130, 182);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(68, 25);
            this.label1.TabIndex = 0;
            this.label1.Text = "Nama";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(130, 230);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(65, 25);
            this.label2.TabIndex = 1;
            this.label2.Text = "Email";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(130, 280);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(90, 25);
            this.label3.TabIndex = 2;
            this.label3.Text = "Telepon";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(130, 328);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(78, 25);
            this.label4.TabIndex = 3;
            this.label4.Text = "Alamat";
            // 
            // txtNamaKlien
            // 
            this.txtNamaKlien.Location = new System.Drawing.Point(236, 179);
            this.txtNamaKlien.Name = "txtNamaKlien";
            this.txtNamaKlien.Size = new System.Drawing.Size(817, 31);
            this.txtNamaKlien.TabIndex = 4;
            // 
            // txtEmail
            // 
            this.txtEmail.Location = new System.Drawing.Point(236, 227);
            this.txtEmail.Name = "txtEmail";
            this.txtEmail.Size = new System.Drawing.Size(817, 31);
            this.txtEmail.TabIndex = 5;
            // 
            // txtTelepon
            // 
            this.txtTelepon.Location = new System.Drawing.Point(236, 277);
            this.txtTelepon.Name = "txtTelepon";
            this.txtTelepon.Size = new System.Drawing.Size(817, 31);
            this.txtTelepon.TabIndex = 6;
            // 
            // txtAlamat
            // 
            this.txtAlamat.Location = new System.Drawing.Point(236, 325);
            this.txtAlamat.Name = "txtAlamat";
            this.txtAlamat.Size = new System.Drawing.Size(817, 31);
            this.txtAlamat.TabIndex = 7;
            // 
            // dgvFormKlien
            // 
            this.dgvFormKlien.BackgroundColor = System.Drawing.Color.AntiqueWhite;
            this.dgvFormKlien.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvFormKlien.Location = new System.Drawing.Point(32, 394);
            this.dgvFormKlien.Name = "dgvFormKlien";
            this.dgvFormKlien.RowHeadersWidth = 82;
            this.dgvFormKlien.RowTemplate.Height = 33;
            this.dgvFormKlien.Size = new System.Drawing.Size(1375, 380);
            this.dgvFormKlien.TabIndex = 8;
            this.dgvFormKlien.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvFormKlien_CellClick);
            // 
            // Back
            // 
            this.Back.Image = ((System.Drawing.Image)(resources.GetObject("Back.Image")));
            this.Back.Location = new System.Drawing.Point(32, 761);
            this.Back.Name = "Back";
            this.Back.Size = new System.Drawing.Size(166, 142);
            this.Back.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.Back.TabIndex = 13;
            this.Back.TabStop = false;
            this.Back.Click += new System.EventHandler(this.btnBack_Click);
            // 
            // btnInsert
            // 
            this.btnInsert.BackColor = System.Drawing.SystemColors.HotTrack;
            this.btnInsert.ForeColor = System.Drawing.SystemColors.HighlightText;
            this.btnInsert.Location = new System.Drawing.Point(1082, 179);
            this.btnInsert.Name = "btnInsert";
            this.btnInsert.Size = new System.Drawing.Size(128, 62);
            this.btnInsert.TabIndex = 15;
            this.btnInsert.Text = "Simpan";
            this.btnInsert.UseVisualStyleBackColor = false;
            this.btnInsert.Click += new System.EventHandler(this.btnInsert_Click);
            // 
            // button2
            // 
            this.button2.BackColor = System.Drawing.Color.Red;
            this.button2.ForeColor = System.Drawing.SystemColors.HighlightText;
            this.button2.Location = new System.Drawing.Point(1082, 280);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(128, 62);
            this.button2.TabIndex = 16;
            this.button2.Text = "Hapus";
            this.button2.UseVisualStyleBackColor = false;
            this.button2.Click += new System.EventHandler(this.btnDelete_Click);
            // 
            // button3
            // 
            this.button3.BackColor = System.Drawing.Color.Gold;
            this.button3.ForeColor = System.Drawing.SystemColors.HighlightText;
            this.button3.Location = new System.Drawing.Point(1249, 179);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(128, 62);
            this.button3.TabIndex = 17;
            this.button3.Text = "Update";
            this.button3.UseVisualStyleBackColor = false;
            this.button3.Click += new System.EventHandler(this.btnEdit_Click);
            // 
            // button4
            // 
            this.button4.BackColor = System.Drawing.SystemColors.ControlDark;
            this.button4.ForeColor = System.Drawing.SystemColors.HighlightText;
            this.button4.Location = new System.Drawing.Point(1249, 280);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(128, 62);
            this.button4.TabIndex = 18;
            this.button4.Text = "Refresh";
            this.button4.UseVisualStyleBackColor = false;
            this.button4.Click += new System.EventHandler(this.btnRefresh_Click);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(1275, 801);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(132, 54);
            this.button1.TabIndex = 19;
            this.button1.Text = "Analisis";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.btnAnalisis_Click);
            // 
            // button5
            // 
            this.button5.Location = new System.Drawing.Point(1117, 801);
            this.button5.Name = "button5";
            this.button5.Size = new System.Drawing.Size(132, 53);
            this.button5.TabIndex = 20;
            this.button5.Text = "Report";
            this.button5.UseVisualStyleBackColor = true;
            this.button5.Click += new System.EventHandler(this.btnReport);
            // 
            // panelKlien
            // 
            this.panelKlien.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.panelKlien.BackColor = System.Drawing.Color.Linen;
            this.panelKlien.Controls.Add(this.button5);
            this.panelKlien.Controls.Add(this.button1);
            this.panelKlien.Controls.Add(this.txtAlamat);
            this.panelKlien.Controls.Add(this.dgvFormKlien);
            this.panelKlien.Controls.Add(this.label1);
            this.panelKlien.Controls.Add(this.button4);
            this.panelKlien.Controls.Add(this.label2);
            this.panelKlien.Controls.Add(this.button3);
            this.panelKlien.Controls.Add(this.label3);
            this.panelKlien.Controls.Add(this.button2);
            this.panelKlien.Controls.Add(this.label4);
            this.panelKlien.Controls.Add(this.btnInsert);
            this.panelKlien.Controls.Add(this.txtNamaKlien);
            this.panelKlien.Controls.Add(this.Back);
            this.panelKlien.Controls.Add(this.txtEmail);
            this.panelKlien.Controls.Add(this.txtTelepon);
            this.panelKlien.Controls.Add(this.panel1);
            this.panelKlien.Location = new System.Drawing.Point(-2, -2);
            this.panelKlien.Name = "panelKlien";
            this.panelKlien.Size = new System.Drawing.Size(1452, 894);
            this.panelKlien.TabIndex = 21;
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.SaddleBrown;
            this.panel1.Controls.Add(this.label5);
            this.panel1.Location = new System.Drawing.Point(-7, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1462, 111);
            this.panel1.TabIndex = 21;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.BackColor = System.Drawing.Color.SaddleBrown;
            this.label5.Font = new System.Drawing.Font("Gill Sans Ultra Bold", 13.875F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.label5.Location = new System.Drawing.Point(344, 34);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(716, 51);
            this.label5.TabIndex = 14;
            this.label5.Text = "FORMULIR MANAJEMEN KLIEN";
            // 
            // FormClient
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.GradientActiveCaption;
            this.ClientSize = new System.Drawing.Size(1450, 888);
            this.Controls.Add(this.panelKlien);
            this.Name = "FormClient";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "FormClient";
            ((System.ComponentModel.ISupportInitialize)(this.dgvFormKlien)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Back)).EndInit();
            this.panelKlien.ResumeLayout(false);
            this.panelKlien.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtNamaKlien;
        private System.Windows.Forms.TextBox txtEmail;
        private System.Windows.Forms.TextBox txtTelepon;
        private System.Windows.Forms.TextBox txtAlamat;
        private System.Windows.Forms.DataGridView dgvFormKlien;
        private System.Windows.Forms.PictureBox Back;
        private System.Windows.Forms.Button btnInsert;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button5;
        private System.Windows.Forms.Panel panelKlien;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Panel panel1;
    }
}
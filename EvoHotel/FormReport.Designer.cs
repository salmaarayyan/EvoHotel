namespace EvoHotel
{
    partial class FormReport
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
            this.reportViewer = new Microsoft.Reporting.WinForms.ReportViewer();
            this.cmbKlien = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.btn_analyze = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // reportViewer
            // 
            this.reportViewer.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.reportViewer.Location = new System.Drawing.Point(17, 115);
            this.reportViewer.Margin = new System.Windows.Forms.Padding(4);
            this.reportViewer.Name = "reportViewer";
            this.reportViewer.ServerReport.BearerToken = null;
            this.reportViewer.Size = new System.Drawing.Size(1397, 683);
            this.reportViewer.TabIndex = 0;
            // 
            // cmbKlien
            // 
            this.cmbKlien.FormattingEnabled = true;
            this.cmbKlien.Location = new System.Drawing.Point(457, 51);
            this.cmbKlien.Margin = new System.Windows.Forms.Padding(4);
            this.cmbKlien.Name = "cmbKlien";
            this.cmbKlien.Size = new System.Drawing.Size(462, 33);
            this.cmbKlien.TabIndex = 1;
            this.cmbKlien.SelectedIndexChanged += new System.EventHandler(this.cmbKlien_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Linen;
            this.label1.Location = new System.Drawing.Point(12, 54);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(406, 25);
            this.label1.TabIndex = 2;
            this.label1.Text = "Pilih Data Pemesanan Berdasarkan Klien";
            // 
            // btn_analyze
            // 
            this.btn_analyze.Location = new System.Drawing.Point(1158, 809);
            this.btn_analyze.Margin = new System.Windows.Forms.Padding(4);
            this.btn_analyze.Name = "btn_analyze";
            this.btn_analyze.Size = new System.Drawing.Size(256, 51);
            this.btn_analyze.TabIndex = 3;
            this.btn_analyze.Text = "Analisis";
            this.btn_analyze.UseVisualStyleBackColor = true;
            this.btn_analyze.Click += new System.EventHandler(this.AnaylzeQuery_Click);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(17, 805);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(116, 55);
            this.button1.TabIndex = 4;
            this.button1.Text = "Back";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.btnBack_Click);
            // 
            // panel1
            // 
            this.panel1.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.panel1.BackColor = System.Drawing.Color.Linen;
            this.panel1.Controls.Add(this.reportViewer);
            this.panel1.Controls.Add(this.button1);
            this.panel1.Controls.Add(this.cmbKlien);
            this.panel1.Controls.Add(this.btn_analyze);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Location = new System.Drawing.Point(0, -1);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1451, 895);
            this.panel1.TabIndex = 5;
            // 
            // FormReport
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1450, 888);
            this.Controls.Add(this.panel1);
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "FormReport";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "FormReport";
            this.Load += new System.EventHandler(this.FormOrder_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private Microsoft.Reporting.WinForms.ReportViewer reportViewer;
        private System.Windows.Forms.ComboBox cmbKlien;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btn_analyze;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Panel panel1;
    }
}
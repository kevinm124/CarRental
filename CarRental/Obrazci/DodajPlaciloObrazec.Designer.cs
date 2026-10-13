namespace CarRental.Obrazci
{
    partial class DodajPlaciloObrazec
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
            this.panelNaslov = new System.Windows.Forms.Panel();
            this.panelGumbi = new System.Windows.Forms.Panel();
            this.panelGlavno = new System.Windows.Forms.Panel();
            this.lblNaslovDialoga = new System.Windows.Forms.Label();
            this.lblRezervacijaNaslov = new System.Windows.Forms.Label();
            this.cmbRezervacija = new System.Windows.Forms.ComboBox();
            this.panelInfo = new System.Windows.Forms.Panel();
            this.lblInfoNaslov = new System.Windows.Forms.Label();
            this.lblInfoStrankaNaslov = new System.Windows.Forms.Label();
            this.lblInfoStranka = new System.Windows.Forms.Label();
            this.lblInfoAvtoNaslov = new System.Windows.Forms.Label();
            this.lblInfoAvto = new System.Windows.Forms.Label();
            this.lblInfoCenaNaslov = new System.Windows.Forms.Label();
            this.lblInfoCena = new System.Windows.Forms.Label();
            this.lblZnesekNaslov = new System.Windows.Forms.Label();
            this.numZnesek = new System.Windows.Forms.NumericUpDown();
            this.lblNacinNaslov = new System.Windows.Forms.Label();
            this.cmbNacin = new System.Windows.Forms.ComboBox();
            this.lblStatusNaslov = new System.Windows.Forms.Label();
            this.cmbStatus = new System.Windows.Forms.ComboBox();
            this.lblOpomba = new System.Windows.Forms.Label();
            this.btnShrani = new System.Windows.Forms.Button();
            this.btnPreklici = new System.Windows.Forms.Button();
            this.panelNaslov.SuspendLayout();
            this.panelGumbi.SuspendLayout();
            this.panelGlavno.SuspendLayout();
            this.panelInfo.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numZnesek)).BeginInit();
            this.SuspendLayout();
            // 
            // panelNaslov
            // 
            this.panelNaslov.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(33)))), ((int)(((byte)(150)))), ((int)(((byte)(243)))));
            this.panelNaslov.Controls.Add(this.lblNaslovDialoga);
            this.panelNaslov.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelNaslov.Location = new System.Drawing.Point(0, 0);
            this.panelNaslov.Name = "panelNaslov";
            this.panelNaslov.Size = new System.Drawing.Size(584, 60);
            this.panelNaslov.TabIndex = 0;
            // 
            // panelGumbi
            // 
            this.panelGumbi.BackColor = System.Drawing.Color.WhiteSmoke;
            this.panelGumbi.Controls.Add(this.btnPreklici);
            this.panelGumbi.Controls.Add(this.btnShrani);
            this.panelGumbi.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelGumbi.Location = new System.Drawing.Point(0, 451);
            this.panelGumbi.Name = "panelGumbi";
            this.panelGumbi.Size = new System.Drawing.Size(584, 70);
            this.panelGumbi.TabIndex = 1;
            // 
            // panelGlavno
            // 
            this.panelGlavno.Controls.Add(this.lblOpomba);
            this.panelGlavno.Controls.Add(this.cmbStatus);
            this.panelGlavno.Controls.Add(this.lblStatusNaslov);
            this.panelGlavno.Controls.Add(this.cmbNacin);
            this.panelGlavno.Controls.Add(this.lblNacinNaslov);
            this.panelGlavno.Controls.Add(this.numZnesek);
            this.panelGlavno.Controls.Add(this.lblZnesekNaslov);
            this.panelGlavno.Controls.Add(this.panelInfo);
            this.panelGlavno.Controls.Add(this.cmbRezervacija);
            this.panelGlavno.Controls.Add(this.lblRezervacijaNaslov);
            this.panelGlavno.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelGlavno.Location = new System.Drawing.Point(0, 60);
            this.panelGlavno.Name = "panelGlavno";
            this.panelGlavno.Padding = new System.Windows.Forms.Padding(30, 25, 30, 25);
            this.panelGlavno.Size = new System.Drawing.Size(584, 391);
            this.panelGlavno.TabIndex = 2;
            // 
            // lblNaslovDialoga
            // 
            this.lblNaslovDialoga.BackColor = System.Drawing.Color.Transparent;
            this.lblNaslovDialoga.Font = new System.Drawing.Font("Segoe UI", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblNaslovDialoga.ForeColor = System.Drawing.Color.White;
            this.lblNaslovDialoga.Location = new System.Drawing.Point(20, 15);
            this.lblNaslovDialoga.Name = "lblNaslovDialoga";
            this.lblNaslovDialoga.Size = new System.Drawing.Size(500, 30);
            this.lblNaslovDialoga.TabIndex = 0;
            this.lblNaslovDialoga.Text = "Zabeleži plačilo";
            // 
            // lblRezervacijaNaslov
            // 
            this.lblRezervacijaNaslov.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblRezervacijaNaslov.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(80)))), ((int)(((byte)(80)))), ((int)(((byte)(80)))));
            this.lblRezervacijaNaslov.Location = new System.Drawing.Point(11, 0);
            this.lblRezervacijaNaslov.Name = "lblRezervacijaNaslov";
            this.lblRezervacijaNaslov.Size = new System.Drawing.Size(280, 18);
            this.lblRezervacijaNaslov.TabIndex = 0;
            this.lblRezervacijaNaslov.Text = "Rezervacija *";
            // 
            // cmbRezervacija
            // 
            this.cmbRezervacija.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbRezervacija.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbRezervacija.FormattingEnabled = true;
            this.cmbRezervacija.Location = new System.Drawing.Point(11, 22);
            this.cmbRezervacija.Name = "cmbRezervacija";
            this.cmbRezervacija.Size = new System.Drawing.Size(530, 25);
            this.cmbRezervacija.TabIndex = 1;
            // 
            // panelInfo
            // 
            this.panelInfo.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(250)))), ((int)(((byte)(255)))));
            this.panelInfo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panelInfo.Controls.Add(this.lblInfoCena);
            this.panelInfo.Controls.Add(this.lblInfoCenaNaslov);
            this.panelInfo.Controls.Add(this.lblInfoAvto);
            this.panelInfo.Controls.Add(this.lblInfoAvtoNaslov);
            this.panelInfo.Controls.Add(this.lblInfoStranka);
            this.panelInfo.Controls.Add(this.lblInfoStrankaNaslov);
            this.panelInfo.Controls.Add(this.lblInfoNaslov);
            this.panelInfo.Location = new System.Drawing.Point(11, 70);
            this.panelInfo.Name = "panelInfo";
            this.panelInfo.Size = new System.Drawing.Size(530, 130);
            this.panelInfo.TabIndex = 2;
            // 
            // lblInfoNaslov
            // 
            this.lblInfoNaslov.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblInfoNaslov.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(33)))), ((int)(((byte)(150)))), ((int)(((byte)(243)))));
            this.lblInfoNaslov.Location = new System.Drawing.Point(26, 10);
            this.lblInfoNaslov.Name = "lblInfoNaslov";
            this.lblInfoNaslov.Size = new System.Drawing.Size(300, 20);
            this.lblInfoNaslov.TabIndex = 0;
            this.lblInfoNaslov.Text = "PODATKI O REZERVACIJI";
            // 
            // lblInfoStrankaNaslov
            // 
            this.lblInfoStrankaNaslov.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblInfoStrankaNaslov.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(80)))), ((int)(((byte)(80)))), ((int)(((byte)(80)))));
            this.lblInfoStrankaNaslov.Location = new System.Drawing.Point(26, 40);
            this.lblInfoStrankaNaslov.Name = "lblInfoStrankaNaslov";
            this.lblInfoStrankaNaslov.Size = new System.Drawing.Size(100, 18);
            this.lblInfoStrankaNaslov.TabIndex = 1;
            this.lblInfoStrankaNaslov.Text = "Stranka:";
            // 
            // lblInfoStranka
            // 
            this.lblInfoStranka.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblInfoStranka.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(33)))), ((int)(((byte)(33)))), ((int)(((byte)(33)))));
            this.lblInfoStranka.Location = new System.Drawing.Point(131, 40);
            this.lblInfoStranka.Name = "lblInfoStranka";
            this.lblInfoStranka.Size = new System.Drawing.Size(390, 18);
            this.lblInfoStranka.TabIndex = 2;
            this.lblInfoStranka.Text = "-";
            // 
            // lblInfoAvtoNaslov
            // 
            this.lblInfoAvtoNaslov.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblInfoAvtoNaslov.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(80)))), ((int)(((byte)(80)))), ((int)(((byte)(80)))));
            this.lblInfoAvtoNaslov.Location = new System.Drawing.Point(26, 62);
            this.lblInfoAvtoNaslov.Name = "lblInfoAvtoNaslov";
            this.lblInfoAvtoNaslov.Size = new System.Drawing.Size(100, 18);
            this.lblInfoAvtoNaslov.TabIndex = 3;
            this.lblInfoAvtoNaslov.Text = "Avtomobil:";
            // 
            // lblInfoAvto
            // 
            this.lblInfoAvto.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblInfoAvto.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(33)))), ((int)(((byte)(33)))), ((int)(((byte)(33)))));
            this.lblInfoAvto.Location = new System.Drawing.Point(131, 62);
            this.lblInfoAvto.Name = "lblInfoAvto";
            this.lblInfoAvto.Size = new System.Drawing.Size(390, 18);
            this.lblInfoAvto.TabIndex = 4;
            this.lblInfoAvto.Text = "-";
            // 
            // lblInfoCenaNaslov
            // 
            this.lblInfoCenaNaslov.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblInfoCenaNaslov.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(33)))), ((int)(((byte)(33)))), ((int)(((byte)(33)))));
            this.lblInfoCenaNaslov.Location = new System.Drawing.Point(26, 90);
            this.lblInfoCenaNaslov.Name = "lblInfoCenaNaslov";
            this.lblInfoCenaNaslov.Size = new System.Drawing.Size(100, 25);
            this.lblInfoCenaNaslov.TabIndex = 5;
            this.lblInfoCenaNaslov.Text = "Skupna cena:";
            // 
            // lblInfoCena
            // 
            this.lblInfoCena.Font = new System.Drawing.Font("Segoe UI", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblInfoCena.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(76)))), ((int)(((byte)(175)))), ((int)(((byte)(80)))));
            this.lblInfoCena.Location = new System.Drawing.Point(131, 88);
            this.lblInfoCena.Name = "lblInfoCena";
            this.lblInfoCena.Size = new System.Drawing.Size(390, 30);
            this.lblInfoCena.TabIndex = 6;
            this.lblInfoCena.Text = "0,00 €";
            // 
            // lblZnesekNaslov
            // 
            this.lblZnesekNaslov.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblZnesekNaslov.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(80)))), ((int)(((byte)(80)))), ((int)(((byte)(80)))));
            this.lblZnesekNaslov.Location = new System.Drawing.Point(11, 220);
            this.lblZnesekNaslov.Name = "lblZnesekNaslov";
            this.lblZnesekNaslov.Size = new System.Drawing.Size(280, 18);
            this.lblZnesekNaslov.TabIndex = 3;
            this.lblZnesekNaslov.Text = "Znesek plačila (€) *";
            // 
            // numZnesek
            // 
            this.numZnesek.DecimalPlaces = 2;
            this.numZnesek.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.numZnesek.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(80)))), ((int)(((byte)(80)))), ((int)(((byte)(80)))));
            this.numZnesek.Increment = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.numZnesek.Location = new System.Drawing.Point(11, 242);
            this.numZnesek.Maximum = new decimal(new int[] {
            99999,
            0,
            0,
            0});
            this.numZnesek.Name = "numZnesek";
            this.numZnesek.Size = new System.Drawing.Size(250, 25);
            this.numZnesek.TabIndex = 4;
            // 
            // lblNacinNaslov
            // 
            this.lblNacinNaslov.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblNacinNaslov.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(80)))), ((int)(((byte)(80)))), ((int)(((byte)(80)))));
            this.lblNacinNaslov.Location = new System.Drawing.Point(291, 220);
            this.lblNacinNaslov.Name = "lblNacinNaslov";
            this.lblNacinNaslov.Size = new System.Drawing.Size(250, 18);
            this.lblNacinNaslov.TabIndex = 5;
            this.lblNacinNaslov.Text = "Način plačila *";
            // 
            // cmbNacin
            // 
            this.cmbNacin.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbNacin.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbNacin.FormattingEnabled = true;
            this.cmbNacin.Location = new System.Drawing.Point(291, 242);
            this.cmbNacin.Name = "cmbNacin";
            this.cmbNacin.Size = new System.Drawing.Size(250, 25);
            this.cmbNacin.TabIndex = 6;
            // 
            // lblStatusNaslov
            // 
            this.lblStatusNaslov.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblStatusNaslov.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(80)))), ((int)(((byte)(80)))), ((int)(((byte)(80)))));
            this.lblStatusNaslov.Location = new System.Drawing.Point(11, 290);
            this.lblStatusNaslov.Name = "lblStatusNaslov";
            this.lblStatusNaslov.Size = new System.Drawing.Size(280, 18);
            this.lblStatusNaslov.TabIndex = 7;
            this.lblStatusNaslov.Text = "Status";
            // 
            // cmbStatus
            // 
            this.cmbStatus.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbStatus.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbStatus.FormattingEnabled = true;
            this.cmbStatus.Location = new System.Drawing.Point(11, 312);
            this.cmbStatus.Name = "cmbStatus";
            this.cmbStatus.Size = new System.Drawing.Size(250, 25);
            this.cmbStatus.TabIndex = 8;
            // 
            // lblOpomba
            // 
            this.lblOpomba.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblOpomba.ForeColor = System.Drawing.Color.Gray;
            this.lblOpomba.Location = new System.Drawing.Point(11, 360);
            this.lblOpomba.Name = "lblOpomba";
            this.lblOpomba.Size = new System.Drawing.Size(200, 15);
            this.lblOpomba.TabIndex = 9;
            this.lblOpomba.Text = "* obvezna polja";
            // 
            // btnShrani
            // 
            this.btnShrani.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnShrani.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(76)))), ((int)(((byte)(175)))), ((int)(((byte)(80)))));
            this.btnShrani.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnShrani.FlatAppearance.BorderSize = 0;
            this.btnShrani.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnShrani.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnShrani.ForeColor = System.Drawing.Color.White;
            this.btnShrani.Location = new System.Drawing.Point(361, 15);
            this.btnShrani.Name = "btnShrani";
            this.btnShrani.Size = new System.Drawing.Size(210, 40);
            this.btnShrani.TabIndex = 0;
            this.btnShrani.Text = "ZABELEŽI PLAČILO";
            this.btnShrani.UseVisualStyleBackColor = false;
            this.btnShrani.Click += new System.EventHandler(this.btnShrani_Click);
            // 
            // btnPreklici
            // 
            this.btnPreklici.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnPreklici.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnPreklici.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(200)))), ((int)(((byte)(200)))), ((int)(((byte)(200)))));
            this.btnPreklici.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnPreklici.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnPreklici.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(80)))), ((int)(((byte)(80)))), ((int)(((byte)(80)))));
            this.btnPreklici.Location = new System.Drawing.Point(190, 15);
            this.btnPreklici.Name = "btnPreklici";
            this.btnPreklici.Size = new System.Drawing.Size(150, 40);
            this.btnPreklici.TabIndex = 1;
            this.btnPreklici.Text = "PREKLIČI";
            this.btnPreklici.UseVisualStyleBackColor = true;
            this.btnPreklici.Click += new System.EventHandler(this.btnPreklici_Click);
            // 
            // DodajPlaciloObrazec
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(584, 521);
            this.Controls.Add(this.panelGlavno);
            this.Controls.Add(this.panelGumbi);
            this.Controls.Add(this.panelNaslov);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "DodajPlaciloObrazec";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Novo plačilo";
            this.panelNaslov.ResumeLayout(false);
            this.panelGumbi.ResumeLayout(false);
            this.panelGlavno.ResumeLayout(false);
            this.panelInfo.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.numZnesek)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panelNaslov;
        private System.Windows.Forms.Label lblNaslovDialoga;
        private System.Windows.Forms.Panel panelGumbi;
        private System.Windows.Forms.Panel panelGlavno;
        private System.Windows.Forms.Panel panelInfo;
        private System.Windows.Forms.Label lblInfoStranka;
        private System.Windows.Forms.Label lblInfoStrankaNaslov;
        private System.Windows.Forms.Label lblInfoNaslov;
        private System.Windows.Forms.ComboBox cmbRezervacija;
        private System.Windows.Forms.Label lblRezervacijaNaslov;
        private System.Windows.Forms.Label lblInfoAvto;
        private System.Windows.Forms.Label lblInfoAvtoNaslov;
        private System.Windows.Forms.NumericUpDown numZnesek;
        private System.Windows.Forms.Label lblZnesekNaslov;
        private System.Windows.Forms.Label lblInfoCena;
        private System.Windows.Forms.Label lblInfoCenaNaslov;
        private System.Windows.Forms.Button btnPreklici;
        private System.Windows.Forms.Button btnShrani;
        private System.Windows.Forms.Label lblOpomba;
        private System.Windows.Forms.ComboBox cmbStatus;
        private System.Windows.Forms.Label lblStatusNaslov;
        private System.Windows.Forms.ComboBox cmbNacin;
        private System.Windows.Forms.Label lblNacinNaslov;
    }
}
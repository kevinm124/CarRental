namespace CarRental.Obrazci
{
    partial class AvtomobiliObrazec
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
            this.btnIzvoz = new System.Windows.Forms.Button();
            this.btnGalerija = new System.Windows.Forms.Button();
            this.btnServisi = new System.Windows.Forms.Button();
            this.btnPoskodbe = new System.Windows.Forms.Button();
            this.panelOrodja = new System.Windows.Forms.Panel();
            this.panelGlavno = new System.Windows.Forms.Panel();
            this.splitGlavno = new System.Windows.Forms.SplitContainer();
            this.dgvAvtomobili = new System.Windows.Forms.DataGridView();
            this.panelDetajli = new System.Windows.Forms.Panel();
            this.btnIzbrisi = new System.Windows.Forms.Button();
            this.btbnUredi = new System.Windows.Forms.Button();
            this.lblBarva = new System.Windows.Forms.Label();
            this.lblSedezev = new System.Windows.Forms.Label();
            this.lblLetnik = new System.Windows.Forms.Label();
            this.lblKategorija = new System.Windows.Forms.Label();
            this.lblCena = new System.Windows.Forms.Label();
            this.lblStatus = new System.Windows.Forms.Label();
            this.lblRegistrska = new System.Windows.Forms.Label();
            this.lblImeAvta = new System.Windows.Forms.Label();
            this.lblDetajliNaslov = new System.Windows.Forms.Label();
            this.btnOsvezi = new System.Windows.Forms.Button();
            this.btnDodaj = new System.Windows.Forms.Button();
            this.cmbFilterStatus = new System.Windows.Forms.ComboBox();
            this.cmbFilterKategorija = new System.Windows.Forms.ComboBox();
            this.txtIskanje = new MaterialSkin.Controls.MaterialTextBox2();
            this.picSlika = new System.Windows.Forms.PictureBox();
            this.panelOrodja.SuspendLayout();
            this.panelGlavno.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitGlavno)).BeginInit();
            this.splitGlavno.Panel1.SuspendLayout();
            this.splitGlavno.Panel2.SuspendLayout();
            this.splitGlavno.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvAvtomobili)).BeginInit();
            this.panelDetajli.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picSlika)).BeginInit();
            this.SuspendLayout();
            // 
            // panelOrodja
            // 
            this.panelOrodja.BackColor = System.Drawing.Color.White;
            this.panelOrodja.Controls.Add(this.btnIzvoz);
            this.panelOrodja.Controls.Add(this.txtIskanje);
            this.panelOrodja.Controls.Add(this.btnOsvezi);
            this.panelOrodja.Controls.Add(this.cmbFilterKategorija);
            this.panelOrodja.Controls.Add(this.btnDodaj);
            this.panelOrodja.Controls.Add(this.cmbFilterStatus);
            this.panelOrodja.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelOrodja.Location = new System.Drawing.Point(0, 0);
            this.panelOrodja.Name = "panelOrodja";
            this.panelOrodja.Padding = new System.Windows.Forms.Padding(15);
            this.panelOrodja.Size = new System.Drawing.Size(1024, 70);
            this.panelOrodja.TabIndex = 0;
            // 
            // panelGlavno
            // 
            this.panelGlavno.BackColor = System.Drawing.Color.WhiteSmoke;
            this.panelGlavno.Controls.Add(this.splitGlavno);
            this.panelGlavno.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelGlavno.Location = new System.Drawing.Point(0, 70);
            this.panelGlavno.Name = "panelGlavno";
            this.panelGlavno.Padding = new System.Windows.Forms.Padding(15);
            this.panelGlavno.Size = new System.Drawing.Size(1024, 551);
            this.panelGlavno.TabIndex = 1;
            // 
            // splitGlavno
            // 
            this.splitGlavno.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitGlavno.Location = new System.Drawing.Point(15, 15);
            this.splitGlavno.Name = "splitGlavno";
            // 
            // splitGlavno.Panel1
            // 
            this.splitGlavno.Panel1.Controls.Add(this.dgvAvtomobili);
            // 
            // splitGlavno.Panel2
            // 
            this.splitGlavno.Panel2.Controls.Add(this.panelDetajli);
            this.splitGlavno.Size = new System.Drawing.Size(994, 521);
            this.splitGlavno.SplitterDistance = 680;
            this.splitGlavno.TabIndex = 5;
            // 
            // dgvAvtomobili
            // 
            this.dgvAvtomobili.AllowUserToAddRows = false;
            this.dgvAvtomobili.AllowUserToDeleteRows = false;
            this.dgvAvtomobili.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvAvtomobili.BackgroundColor = System.Drawing.Color.White;
            this.dgvAvtomobili.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dgvAvtomobili.ColumnHeadersHeight = 40;
            this.dgvAvtomobili.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvAvtomobili.Location = new System.Drawing.Point(0, 0);
            this.dgvAvtomobili.MultiSelect = false;
            this.dgvAvtomobili.Name = "dgvAvtomobili";
            this.dgvAvtomobili.ReadOnly = true;
            this.dgvAvtomobili.RowHeadersVisible = false;
            this.dgvAvtomobili.RowTemplate.Height = 35;
            this.dgvAvtomobili.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvAvtomobili.Size = new System.Drawing.Size(680, 521);
            this.dgvAvtomobili.TabIndex = 0;
            // 
            // panelDetajli
            // 
            this.panelDetajli.BackColor = System.Drawing.Color.White;
            this.panelDetajli.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panelDetajli.Controls.Add(this.btnPoskodbe);
            this.panelDetajli.Controls.Add(this.btnServisi);
            this.panelDetajli.Controls.Add(this.btnGalerija);
            this.panelDetajli.Controls.Add(this.btnIzbrisi);
            this.panelDetajli.Controls.Add(this.btbnUredi);
            this.panelDetajli.Controls.Add(this.lblBarva);
            this.panelDetajli.Controls.Add(this.lblSedezev);
            this.panelDetajli.Controls.Add(this.lblLetnik);
            this.panelDetajli.Controls.Add(this.lblKategorija);
            this.panelDetajli.Controls.Add(this.lblCena);
            this.panelDetajli.Controls.Add(this.lblStatus);
            this.panelDetajli.Controls.Add(this.lblRegistrska);
            this.panelDetajli.Controls.Add(this.lblImeAvta);
            this.panelDetajli.Controls.Add(this.picSlika);
            this.panelDetajli.Controls.Add(this.lblDetajliNaslov);
            this.panelDetajli.AutoScroll = true;
            this.panelDetajli.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelDetajli.Location = new System.Drawing.Point(0, 0);
            this.panelDetajli.Name = "panelDetajli";
            this.panelDetajli.Padding = new System.Windows.Forms.Padding(20);
            this.panelDetajli.Size = new System.Drawing.Size(310, 521);
            this.panelDetajli.TabIndex = 0;
            // 
            // btnIzbrisi
            // 
            this.btnIzbrisi.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(211)))), ((int)(((byte)(47)))), ((int)(((byte)(47)))));
            this.btnIzbrisi.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnIzbrisi.FlatAppearance.BorderSize = 0;
            this.btnIzbrisi.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnIzbrisi.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnIzbrisi.ForeColor = System.Drawing.Color.White;
            this.btnIzbrisi.Location = new System.Drawing.Point(165, 510);
            this.btnIzbrisi.Name = "btnIzbrisi";
            this.btnIzbrisi.Size = new System.Drawing.Size(130, 40);
            this.btnIzbrisi.TabIndex = 11;
            this.btnIzbrisi.Text = "IZBRIŠI";
            this.btnIzbrisi.UseVisualStyleBackColor = false;
            this.btnIzbrisi.Click += new System.EventHandler(this.btnIzbrisi_Click);
            // 
            // btbnUredi
            // 
            this.btbnUredi.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(152)))), ((int)(((byte)(0)))));
            this.btbnUredi.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btbnUredi.FlatAppearance.BorderSize = 0;
            this.btbnUredi.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btbnUredi.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btbnUredi.ForeColor = System.Drawing.Color.White;
            this.btbnUredi.Location = new System.Drawing.Point(20, 510);
            this.btbnUredi.Name = "btbnUredi";
            this.btbnUredi.Size = new System.Drawing.Size(130, 40);
            this.btbnUredi.TabIndex = 10;
            this.btbnUredi.Text = "UREDI";
            this.btbnUredi.UseVisualStyleBackColor = false;
            this.btbnUredi.Click += new System.EventHandler(this.btnUredi_Click);
            // 
            // lblBarva
            // 
            this.lblBarva.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblBarva.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(80)))), ((int)(((byte)(80)))), ((int)(((byte)(80)))), ((int)(((byte)(80)))));
            this.lblBarva.Location = new System.Drawing.Point(20, 465);
            this.lblBarva.Name = "lblBarva";
            this.lblBarva.Size = new System.Drawing.Size(280, 20);
            this.lblBarva.TabIndex = 9;
            this.lblBarva.Text = "Barva: -";
            // 
            // lblSedezev
            // 
            this.lblSedezev.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(80)))), ((int)(((byte)(80)))), ((int)(((byte)(80)))));
            this.lblSedezev.Location = new System.Drawing.Point(20, 440);
            this.lblSedezev.Name = "lblSedezev";
            this.lblSedezev.Size = new System.Drawing.Size(280, 20);
            this.lblSedezev.TabIndex = 8;
            this.lblSedezev.Text = "Št. sedežev: -";
            // 
            // lblLetnik
            // 
            this.lblLetnik.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblLetnik.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(80)))), ((int)(((byte)(80)))), ((int)(((byte)(80)))));
            this.lblLetnik.Location = new System.Drawing.Point(20, 415);
            this.lblLetnik.Name = "lblLetnik";
            this.lblLetnik.Size = new System.Drawing.Size(280, 20);
            this.lblLetnik.TabIndex = 7;
            this.lblLetnik.Text = "Letnik: -";
            // 
            // lblKategorija
            // 
            this.lblKategorija.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblKategorija.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(80)))), ((int)(((byte)(80)))), ((int)(((byte)(80)))));
            this.lblKategorija.Location = new System.Drawing.Point(20, 390);
            this.lblKategorija.Name = "lblKategorija";
            this.lblKategorija.Size = new System.Drawing.Size(280, 20);
            this.lblKategorija.TabIndex = 6;
            this.lblKategorija.Text = "Kategorija: -";
            // 
            // lblCena
            // 
            this.lblCena.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCena.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(33)))), ((int)(((byte)(33)))), ((int)(((byte)(33)))));
            this.lblCena.Location = new System.Drawing.Point(20, 360);
            this.lblCena.Name = "lblCena";
            this.lblCena.Size = new System.Drawing.Size(280, 25);
            this.lblCena.TabIndex = 5;
            this.lblCena.Text = "Cena na dan: -";
            // 
            // lblStatus
            // 
            this.lblStatus.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(76)))), ((int)(((byte)(175)))), ((int)(((byte)(80)))));
            this.lblStatus.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblStatus.ForeColor = System.Drawing.Color.White;
            this.lblStatus.Location = new System.Drawing.Point(20, 315);
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(100, 28);
            this.lblStatus.TabIndex = 4;
            this.lblStatus.Text = "PROSTO";
            this.lblStatus.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblRegistrska
            // 
            this.lblRegistrska.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblRegistrska.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(120)))), ((int)(((byte)(120)))), ((int)(((byte)(120)))));
            this.lblRegistrska.Location = new System.Drawing.Point(20, 285);
            this.lblRegistrska.Name = "lblRegistrska";
            this.lblRegistrska.Size = new System.Drawing.Size(280, 20);
            this.lblRegistrska.TabIndex = 3;
            this.lblRegistrska.Text = "-";
            // 
            // lblImeAvta
            // 
            this.lblImeAvta.Font = new System.Drawing.Font("Segoe UI", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblImeAvta.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(33)))), ((int)(((byte)(33)))), ((int)(((byte)(33)))));
            this.lblImeAvta.Location = new System.Drawing.Point(20, 250);
            this.lblImeAvta.Name = "lblImeAvta";
            this.lblImeAvta.Size = new System.Drawing.Size(280, 30);
            this.lblImeAvta.TabIndex = 2;
            this.lblImeAvta.Text = "Izberite avtomobil";
            // 
            // lblDetajliNaslov
            // 
            this.lblDetajliNaslov.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDetajliNaslov.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(33)))), ((int)(((byte)(33)))), ((int)(((byte)(33)))));
            this.lblDetajliNaslov.Location = new System.Drawing.Point(20, 15);
            this.lblDetajliNaslov.Name = "lblDetajliNaslov";
            this.lblDetajliNaslov.Size = new System.Drawing.Size(280, 25);
            this.lblDetajliNaslov.TabIndex = 0;
            this.lblDetajliNaslov.Text = "DETAJLI AVTOMOBILA";
            // 
            // btnOsvezi
            // 
            this.btnOsvezi.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnOsvezi.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(200)))), ((int)(((byte)(200)))), ((int)(((byte)(200)))));
            this.btnOsvezi.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnOsvezi.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnOsvezi.Location = new System.Drawing.Point(870, 17);
            this.btnOsvezi.Name = "btnOsvezi";
            this.btnOsvezi.Size = new System.Drawing.Size(60, 38);
            this.btnOsvezi.TabIndex = 4;
            this.btnOsvezi.Text = "🔄";
            this.btnOsvezi.UseVisualStyleBackColor = true;
            this.btnOsvezi.Click += new System.EventHandler(this.btnOsvezi_Click);
            // 
            // btnDodaj
            // 
            this.btnDodaj.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(33)))), ((int)(((byte)(150)))), ((int)(((byte)(243)))));
            this.btnDodaj.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnDodaj.FlatAppearance.BorderSize = 0;
            this.btnDodaj.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnDodaj.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnDodaj.ForeColor = System.Drawing.Color.White;
            this.btnDodaj.Location = new System.Drawing.Point(710, 17);
            this.btnDodaj.Name = "btnDodaj";
            this.btnDodaj.Size = new System.Drawing.Size(150, 38);
            this.btnDodaj.TabIndex = 3;
            this.btnDodaj.Text = "DODAJ AVTO";
            this.btnDodaj.UseVisualStyleBackColor = false;
            this.btnDodaj.Click += new System.EventHandler(this.btnDodaj_Click);
            // 
            // cmbFilterStatus
            // 
            this.cmbFilterStatus.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbFilterStatus.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cmbFilterStatus.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbFilterStatus.FormattingEnabled = true;
            this.cmbFilterStatus.Location = new System.Drawing.Point(525, 22);
            this.cmbFilterStatus.Name = "cmbFilterStatus";
            this.cmbFilterStatus.Size = new System.Drawing.Size(150, 25);
            this.cmbFilterStatus.TabIndex = 2;
            // 
            // cmbFilterKategorija
            // 
            this.cmbFilterKategorija.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbFilterKategorija.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cmbFilterKategorija.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbFilterKategorija.FormattingEnabled = true;
            this.cmbFilterKategorija.Location = new System.Drawing.Point(330, 22);
            this.cmbFilterKategorija.Name = "cmbFilterKategorija";
            this.cmbFilterKategorija.Size = new System.Drawing.Size(121, 25);
            this.cmbFilterKategorija.TabIndex = 1;
            // 
            // txtIskanje
            // 
            this.txtIskanje.AnimateReadOnly = false;
            this.txtIskanje.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.txtIskanje.CharacterCasing = System.Windows.Forms.CharacterCasing.Normal;
            this.txtIskanje.Depth = 0;
            this.txtIskanje.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.txtIskanje.HideSelection = true;
            this.txtIskanje.Hint = "Išči po znamki, modelu...";
            this.txtIskanje.LeadingIcon = null;
            this.txtIskanje.Location = new System.Drawing.Point(15, 8);
            this.txtIskanje.MaxLength = 32767;
            this.txtIskanje.MouseState = MaterialSkin.MouseState.OUT;
            this.txtIskanje.Name = "txtIskanje";
            this.txtIskanje.PasswordChar = '\0';
            this.txtIskanje.PrefixSuffixText = null;
            this.txtIskanje.ReadOnly = false;
            this.txtIskanje.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.txtIskanje.SelectedText = "";
            this.txtIskanje.SelectionLength = 0;
            this.txtIskanje.SelectionStart = 0;
            this.txtIskanje.ShortcutsEnabled = true;
            this.txtIskanje.Size = new System.Drawing.Size(300, 48);
            this.txtIskanje.TabIndex = 0;
            this.txtIskanje.TabStop = false;
            this.txtIskanje.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.txtIskanje.TrailingIcon = null;
            this.txtIskanje.UseSystemPasswordChar = false;
            // 
            // picSlika
            // 
            this.picSlika.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
            this.picSlika.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.picSlika.Location = new System.Drawing.Point(20, 55);
            this.picSlika.Name = "picSlika";
            this.picSlika.Size = new System.Drawing.Size(280, 180);
            this.picSlika.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.picSlika.TabIndex = 1;
            this.picSlika.TabStop = false;
            //
            // btnIzvoz
            //
            this.btnIzvoz.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(76)))), ((int)(((byte)(175)))), ((int)(((byte)(80)))));
            this.btnIzvoz.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnIzvoz.FlatAppearance.BorderSize = 0;
            this.btnIzvoz.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnIzvoz.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnIzvoz.ForeColor = System.Drawing.Color.White;
            this.btnIzvoz.Location = new System.Drawing.Point(940, 17);
            this.btnIzvoz.Name = "btnIzvoz";
            this.btnIzvoz.Size = new System.Drawing.Size(110, 38);
            this.btnIzvoz.TabIndex = 20;
            this.btnIzvoz.Text = "📊 EXCEL";
            this.btnIzvoz.UseVisualStyleBackColor = false;
            this.btnIzvoz.Click += new System.EventHandler(this.btnIzvoz_Click);
            //
            // btnGalerija
            //
            this.btnGalerija.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(33)))), ((int)(((byte)(150)))), ((int)(((byte)(243)))));
            this.btnGalerija.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnGalerija.FlatAppearance.BorderSize = 0;
            this.btnGalerija.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnGalerija.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnGalerija.ForeColor = System.Drawing.Color.White;
            this.btnGalerija.Location = new System.Drawing.Point(20, 560);
            this.btnGalerija.Name = "btnGalerija";
            this.btnGalerija.Size = new System.Drawing.Size(130, 40);
            this.btnGalerija.TabIndex = 21;
            this.btnGalerija.Text = "📷 GALERIJA";
            this.btnGalerija.UseVisualStyleBackColor = false;
            this.btnGalerija.Click += new System.EventHandler(this.btnGalerija_Click);
            //
            // btnServisi
            //
            this.btnServisi.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(126)))), ((int)(((byte)(87)))), ((int)(((byte)(194)))));
            this.btnServisi.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnServisi.FlatAppearance.BorderSize = 0;
            this.btnServisi.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnServisi.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnServisi.ForeColor = System.Drawing.Color.White;
            this.btnServisi.Location = new System.Drawing.Point(165, 560);
            this.btnServisi.Name = "btnServisi";
            this.btnServisi.Size = new System.Drawing.Size(130, 40);
            this.btnServisi.TabIndex = 22;
            this.btnServisi.Text = "🔧 SERVISI";
            this.btnServisi.UseVisualStyleBackColor = false;
            this.btnServisi.Click += new System.EventHandler(this.btnServisi_Click);
            //
            // btnPoskodbe
            //
            this.btnPoskodbe.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(96)))), ((int)(((byte)(125)))), ((int)(((byte)(139)))));
            this.btnPoskodbe.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnPoskodbe.FlatAppearance.BorderSize = 0;
            this.btnPoskodbe.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnPoskodbe.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnPoskodbe.ForeColor = System.Drawing.Color.White;
            this.btnPoskodbe.Location = new System.Drawing.Point(20, 610);
            this.btnPoskodbe.Name = "btnPoskodbe";
            this.btnPoskodbe.Size = new System.Drawing.Size(275, 40);
            this.btnPoskodbe.TabIndex = 23;
            this.btnPoskodbe.Text = "⚠ POŠKODBE";
            this.btnPoskodbe.UseVisualStyleBackColor = false;
            this.btnPoskodbe.Click += new System.EventHandler(this.btnPoskodbe_Click);
            // 
            // AvtomobiliObrazec
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1024, 621);
            this.Controls.Add(this.panelGlavno);
            this.Controls.Add(this.panelOrodja);
            this.Name = "AvtomobiliObrazec";
            this.Text = "AvtomobiliObrazec";
            this.panelOrodja.ResumeLayout(false);
            this.panelGlavno.ResumeLayout(false);
            this.splitGlavno.Panel1.ResumeLayout(false);
            this.splitGlavno.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitGlavno)).EndInit();
            this.splitGlavno.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvAvtomobili)).EndInit();
            this.panelDetajli.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.picSlika)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panelOrodja;
        private System.Windows.Forms.Panel panelGlavno;
        private System.Windows.Forms.ComboBox cmbFilterStatus;
        private System.Windows.Forms.ComboBox cmbFilterKategorija;
        private MaterialSkin.Controls.MaterialTextBox2 txtIskanje;
        private System.Windows.Forms.Button btnOsvezi;
        private System.Windows.Forms.Button btnDodaj;
        private System.Windows.Forms.SplitContainer splitGlavno;
        private System.Windows.Forms.DataGridView dgvAvtomobili;
        private System.Windows.Forms.Panel panelDetajli;
        private System.Windows.Forms.Label lblRegistrska;
        private System.Windows.Forms.Label lblImeAvta;
        private System.Windows.Forms.PictureBox picSlika;
        private System.Windows.Forms.Label lblDetajliNaslov;
        private System.Windows.Forms.Label lblSedezev;
        private System.Windows.Forms.Label lblLetnik;
        private System.Windows.Forms.Label lblKategorija;
        private System.Windows.Forms.Label lblCena;
        private System.Windows.Forms.Label lblStatus;
        private System.Windows.Forms.Label lblBarva;
        private System.Windows.Forms.Button btnIzbrisi;
        private System.Windows.Forms.Button btbnUredi;
        private System.Windows.Forms.Button btnIzvoz;
        private System.Windows.Forms.Button btnGalerija;
        private System.Windows.Forms.Button btnServisi;
        private System.Windows.Forms.Button btnPoskodbe;
    }
}
namespace CarRental.Obrazci
{
    partial class StrankeObrazec
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
            this.lblZvestobaNaslov = new System.Windows.Forms.Label();
            this.lblZvestoba = new System.Windows.Forms.Label();
            this.lblZvestobaNapredek = new System.Windows.Forms.Label();
            this.panelOrodja = new System.Windows.Forms.Panel();
            this.btnOsvezi = new System.Windows.Forms.Button();
            this.btnDodaj = new System.Windows.Forms.Button();
            this.txtIskanje = new MaterialSkin.Controls.MaterialTextBox2();
            this.panelGlavno = new System.Windows.Forms.Panel();
            this.splitGlavno = new System.Windows.Forms.SplitContainer();
            this.dgvStranke = new System.Windows.Forms.DataGridView();
            this.panelDetajli = new System.Windows.Forms.Panel();
            this.btnIzbrisi = new System.Windows.Forms.Button();
            this.btnUredi = new System.Windows.Forms.Button();
            this.lblVozniska = new System.Windows.Forms.Label();
            this.lblVozniskaNaslov = new System.Windows.Forms.Label();
            this.lblNaslov = new System.Windows.Forms.Label();
            this.lblNaslovNaslov = new System.Windows.Forms.Label();
            this.lblTelefon = new System.Windows.Forms.Label();
            this.lblTelefonNaslov = new System.Windows.Forms.Label();
            this.lblEmail = new System.Windows.Forms.Label();
            this.lblEmailNaslov = new System.Windows.Forms.Label();
            this.panelSeperator1 = new System.Windows.Forms.Panel();
            this.lblPolnoIme = new System.Windows.Forms.Label();
            this.lblInicialke = new System.Windows.Forms.Label();
            this.lblDetajliNaslov = new System.Windows.Forms.Label();
            this.panelOrodja.SuspendLayout();
            this.panelGlavno.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitGlavno)).BeginInit();
            this.splitGlavno.Panel1.SuspendLayout();
            this.splitGlavno.Panel2.SuspendLayout();
            this.splitGlavno.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvStranke)).BeginInit();
            this.panelDetajli.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelOrodja
            // 
            this.panelOrodja.BackColor = System.Drawing.Color.White;
            this.panelOrodja.Controls.Add(this.btnIzvoz);
            this.panelOrodja.Controls.Add(this.btnOsvezi);
            this.panelOrodja.Controls.Add(this.btnDodaj);
            this.panelOrodja.Controls.Add(this.txtIskanje);
            this.panelOrodja.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelOrodja.Location = new System.Drawing.Point(0, 0);
            this.panelOrodja.Name = "panelOrodja";
            this.panelOrodja.Padding = new System.Windows.Forms.Padding(15);
            this.panelOrodja.Size = new System.Drawing.Size(1024, 70);
            this.panelOrodja.TabIndex = 0;
            // 
            // btnOsvezi
            // 
            this.btnOsvezi.BackColor = System.Drawing.Color.WhiteSmoke;
            this.btnOsvezi.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnOsvezi.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(200)))), ((int)(((byte)(200)))), ((int)(((byte)(200)))));
            this.btnOsvezi.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnOsvezi.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnOsvezi.Location = new System.Drawing.Point(900, 17);
            this.btnOsvezi.Name = "btnOsvezi";
            this.btnOsvezi.Size = new System.Drawing.Size(60, 38);
            this.btnOsvezi.TabIndex = 2;
            this.btnOsvezi.Text = "🔄";
            this.btnOsvezi.UseVisualStyleBackColor = false;
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
            this.btnDodaj.Size = new System.Drawing.Size(180, 38);
            this.btnDodaj.TabIndex = 1;
            this.btnDodaj.Text = "DODAJ STRANKO";
            this.btnDodaj.UseVisualStyleBackColor = false;
            this.btnDodaj.Click += new System.EventHandler(this.btnDodaj_Click);
            // 
            // txtIskanje
            // 
            this.txtIskanje.AnimateReadOnly = false;
            this.txtIskanje.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.txtIskanje.CharacterCasing = System.Windows.Forms.CharacterCasing.Normal;
            this.txtIskanje.Depth = 0;
            this.txtIskanje.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.txtIskanje.HideSelection = true;
            this.txtIskanje.Hint = "Išči po imenu, priimku, emailu...";
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
            this.txtIskanje.Size = new System.Drawing.Size(400, 48);
            this.txtIskanje.TabIndex = 0;
            this.txtIskanje.TabStop = false;
            this.txtIskanje.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.txtIskanje.TrailingIcon = null;
            this.txtIskanje.UseSystemPasswordChar = false;
            // 
            // panelGlavno
            // 
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
            this.splitGlavno.Panel1.Controls.Add(this.dgvStranke);
            // 
            // splitGlavno.Panel2
            // 
            this.splitGlavno.Panel2.Controls.Add(this.panelDetajli);
            this.splitGlavno.Size = new System.Drawing.Size(994, 521);
            this.splitGlavno.SplitterDistance = 680;
            this.splitGlavno.TabIndex = 0;
            // 
            // dgvStranke
            // 
            this.dgvStranke.AllowUserToAddRows = false;
            this.dgvStranke.AllowUserToDeleteRows = false;
            this.dgvStranke.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvStranke.BackgroundColor = System.Drawing.Color.White;
            this.dgvStranke.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dgvStranke.ColumnHeadersHeight = 40;
            this.dgvStranke.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvStranke.Location = new System.Drawing.Point(0, 0);
            this.dgvStranke.MultiSelect = false;
            this.dgvStranke.Name = "dgvStranke";
            this.dgvStranke.ReadOnly = true;
            this.dgvStranke.RowHeadersVisible = false;
            this.dgvStranke.RowTemplate.Height = 35;
            this.dgvStranke.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvStranke.Size = new System.Drawing.Size(680, 521);
            this.dgvStranke.TabIndex = 0;
            // 
            // panelDetajli
            // 
            this.panelDetajli.BackColor = System.Drawing.Color.White;
            this.panelDetajli.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panelDetajli.Controls.Add(this.lblZvestobaNapredek);
            this.panelDetajli.Controls.Add(this.lblZvestoba);
            this.panelDetajli.Controls.Add(this.lblZvestobaNaslov);
            this.panelDetajli.Controls.Add(this.btnIzbrisi);
            this.panelDetajli.Controls.Add(this.btnUredi);
            this.panelDetajli.Controls.Add(this.lblVozniska);
            this.panelDetajli.Controls.Add(this.lblVozniskaNaslov);
            this.panelDetajli.Controls.Add(this.lblNaslov);
            this.panelDetajli.Controls.Add(this.lblNaslovNaslov);
            this.panelDetajli.Controls.Add(this.lblTelefon);
            this.panelDetajli.Controls.Add(this.lblTelefonNaslov);
            this.panelDetajli.Controls.Add(this.lblEmail);
            this.panelDetajli.Controls.Add(this.lblEmailNaslov);
            this.panelDetajli.Controls.Add(this.panelSeperator1);
            this.panelDetajli.Controls.Add(this.lblPolnoIme);
            this.panelDetajli.Controls.Add(this.lblInicialke);
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
            this.btnIzbrisi.TabIndex = 13;
            this.btnIzbrisi.Text = "IZBRIŠI";
            this.btnIzbrisi.UseVisualStyleBackColor = false;
            this.btnIzbrisi.Click += new System.EventHandler(this.btnIzbrisi_Click);
            // 
            // btnUredi
            // 
            this.btnUredi.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(152)))), ((int)(((byte)(0)))));
            this.btnUredi.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnUredi.FlatAppearance.BorderSize = 0;
            this.btnUredi.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnUredi.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnUredi.ForeColor = System.Drawing.Color.White;
            this.btnUredi.Location = new System.Drawing.Point(20, 510);
            this.btnUredi.Name = "btnUredi";
            this.btnUredi.Size = new System.Drawing.Size(130, 40);
            this.btnUredi.TabIndex = 12;
            this.btnUredi.Text = "UREDI";
            this.btnUredi.UseVisualStyleBackColor = false;
            this.btnUredi.Click += new System.EventHandler(this.btnUredi_Click);
            // 
            // lblVozniska
            // 
            this.lblVozniska.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblVozniska.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(33)))), ((int)(((byte)(33)))), ((int)(((byte)(33)))));
            this.lblVozniska.Location = new System.Drawing.Point(20, 390);
            this.lblVozniska.Name = "lblVozniska";
            this.lblVozniska.Size = new System.Drawing.Size(280, 20);
            this.lblVozniska.TabIndex = 11;
            this.lblVozniska.Text = "-";
            // 
            // lblVozniskaNaslov
            // 
            this.lblVozniskaNaslov.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblVozniskaNaslov.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(120)))), ((int)(((byte)(120)))), ((int)(((byte)(120)))));
            this.lblVozniskaNaslov.Location = new System.Drawing.Point(20, 370);
            this.lblVozniskaNaslov.Name = "lblVozniskaNaslov";
            this.lblVozniskaNaslov.Size = new System.Drawing.Size(150, 18);
            this.lblVozniskaNaslov.TabIndex = 10;
            this.lblVozniskaNaslov.Text = "ŠT. VOZNIŠKE";
            // 
            // lblNaslov
            // 
            this.lblNaslov.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblNaslov.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(33)))), ((int)(((byte)(33)))), ((int)(((byte)(33)))));
            this.lblNaslov.Location = new System.Drawing.Point(20, 320);
            this.lblNaslov.Name = "lblNaslov";
            this.lblNaslov.Size = new System.Drawing.Size(280, 40);
            this.lblNaslov.TabIndex = 9;
            this.lblNaslov.Text = "-";
            // 
            // lblNaslovNaslov
            // 
            this.lblNaslovNaslov.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblNaslovNaslov.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(120)))), ((int)(((byte)(120)))), ((int)(((byte)(120)))));
            this.lblNaslovNaslov.Location = new System.Drawing.Point(20, 300);
            this.lblNaslovNaslov.Name = "lblNaslovNaslov";
            this.lblNaslovNaslov.Size = new System.Drawing.Size(150, 18);
            this.lblNaslovNaslov.TabIndex = 8;
            this.lblNaslovNaslov.Text = "NASLOV";
            // 
            // lblTelefon
            // 
            this.lblTelefon.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTelefon.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(33)))), ((int)(((byte)(33)))), ((int)(((byte)(33)))));
            this.lblTelefon.Location = new System.Drawing.Point(20, 270);
            this.lblTelefon.Name = "lblTelefon";
            this.lblTelefon.Size = new System.Drawing.Size(280, 20);
            this.lblTelefon.TabIndex = 7;
            this.lblTelefon.Text = "-";
            // 
            // lblTelefonNaslov
            // 
            this.lblTelefonNaslov.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTelefonNaslov.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(120)))), ((int)(((byte)(120)))), ((int)(((byte)(120)))));
            this.lblTelefonNaslov.Location = new System.Drawing.Point(20, 250);
            this.lblTelefonNaslov.Name = "lblTelefonNaslov";
            this.lblTelefonNaslov.Size = new System.Drawing.Size(150, 18);
            this.lblTelefonNaslov.TabIndex = 6;
            this.lblTelefonNaslov.Text = "TELEFON";
            // 
            // lblEmail
            // 
            this.lblEmail.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblEmail.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(33)))), ((int)(((byte)(33)))), ((int)(((byte)(33)))));
            this.lblEmail.Location = new System.Drawing.Point(20, 220);
            this.lblEmail.Name = "lblEmail";
            this.lblEmail.Size = new System.Drawing.Size(280, 20);
            this.lblEmail.TabIndex = 5;
            this.lblEmail.Text = "-";
            // 
            // lblEmailNaslov
            // 
            this.lblEmailNaslov.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblEmailNaslov.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(120)))), ((int)(((byte)(120)))), ((int)(((byte)(120)))));
            this.lblEmailNaslov.Location = new System.Drawing.Point(20, 200);
            this.lblEmailNaslov.Name = "lblEmailNaslov";
            this.lblEmailNaslov.Size = new System.Drawing.Size(150, 18);
            this.lblEmailNaslov.TabIndex = 4;
            this.lblEmailNaslov.Text = "EMAIL";
            // 
            // panelSeperator1
            // 
            this.panelSeperator1.BackColor = System.Drawing.Color.Gainsboro;
            this.panelSeperator1.Location = new System.Drawing.Point(20, 185);
            this.panelSeperator1.Name = "panelSeperator1";
            this.panelSeperator1.Size = new System.Drawing.Size(280, 1);
            this.panelSeperator1.TabIndex = 3;
            // 
            // lblPolnoIme
            // 
            this.lblPolnoIme.Font = new System.Drawing.Font("Segoe UI", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPolnoIme.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(33)))), ((int)(((byte)(33)))), ((int)(((byte)(33)))));
            this.lblPolnoIme.Location = new System.Drawing.Point(20, 145);
            this.lblPolnoIme.Name = "lblPolnoIme";
            this.lblPolnoIme.Size = new System.Drawing.Size(280, 30);
            this.lblPolnoIme.TabIndex = 2;
            this.lblPolnoIme.Text = "Izberite stranko";
            this.lblPolnoIme.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblInicialke
            // 
            this.lblInicialke.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(33)))), ((int)(((byte)(150)))), ((int)(((byte)(243)))));
            this.lblInicialke.Font = new System.Drawing.Font("Segoe UI", 24F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblInicialke.ForeColor = System.Drawing.Color.White;
            this.lblInicialke.Location = new System.Drawing.Point(110, 55);
            this.lblInicialke.Name = "lblInicialke";
            this.lblInicialke.Size = new System.Drawing.Size(80, 80);
            this.lblInicialke.TabIndex = 1;
            this.lblInicialke.Text = "?";
            this.lblInicialke.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblDetajliNaslov
            // 
            this.lblDetajliNaslov.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDetajliNaslov.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(33)))), ((int)(((byte)(33)))), ((int)(((byte)(33)))));
            this.lblDetajliNaslov.Location = new System.Drawing.Point(20, 15);
            this.lblDetajliNaslov.Name = "lblDetajliNaslov";
            this.lblDetajliNaslov.Size = new System.Drawing.Size(280, 25);
            this.lblDetajliNaslov.TabIndex = 0;
            this.lblDetajliNaslov.Text = "DETAJLI STRANKE";
            //
            // btnIzvoz
            //
            this.btnIzvoz.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(76)))), ((int)(((byte)(175)))), ((int)(((byte)(80)))));
            this.btnIzvoz.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnIzvoz.FlatAppearance.BorderSize = 0;
            this.btnIzvoz.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnIzvoz.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnIzvoz.ForeColor = System.Drawing.Color.White;
            this.btnIzvoz.Location = new System.Drawing.Point(970, 17);
            this.btnIzvoz.Name = "btnIzvoz";
            this.btnIzvoz.Size = new System.Drawing.Size(110, 38);
            this.btnIzvoz.TabIndex = 20;
            this.btnIzvoz.Text = "📊 EXCEL";
            this.btnIzvoz.UseVisualStyleBackColor = false;
            this.btnIzvoz.Click += new System.EventHandler(this.btnIzvoz_Click);
            //
            // lblZvestobaNaslov
            //
            this.lblZvestobaNaslov.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblZvestobaNaslov.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(120)))), ((int)(((byte)(120)))), ((int)(((byte)(120)))));
            this.lblZvestobaNaslov.Location = new System.Drawing.Point(20, 425);
            this.lblZvestobaNaslov.Name = "lblZvestobaNaslov";
            this.lblZvestobaNaslov.Size = new System.Drawing.Size(150, 18);
            this.lblZvestobaNaslov.TabIndex = 21;
            this.lblZvestobaNaslov.Text = "ZVESTOBA";
            //
            // lblZvestoba
            //
            this.lblZvestoba.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblZvestoba.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(33)))), ((int)(((byte)(33)))), ((int)(((byte)(33)))));
            this.lblZvestoba.Location = new System.Drawing.Point(20, 445);
            this.lblZvestoba.Name = "lblZvestoba";
            this.lblZvestoba.Size = new System.Drawing.Size(280, 20);
            this.lblZvestoba.TabIndex = 22;
            this.lblZvestoba.Text = "-";
            //
            // lblZvestobaNapredek
            //
            this.lblZvestobaNapredek.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblZvestobaNapredek.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(120)))), ((int)(((byte)(120)))), ((int)(((byte)(120)))));
            this.lblZvestobaNapredek.Location = new System.Drawing.Point(20, 467);
            this.lblZvestobaNapredek.Name = "lblZvestobaNapredek";
            this.lblZvestobaNapredek.Size = new System.Drawing.Size(280, 36);
            this.lblZvestobaNapredek.TabIndex = 23;
            this.lblZvestobaNapredek.Text = "";
            // 
            // StrankeObrazec
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.WhiteSmoke;
            this.ClientSize = new System.Drawing.Size(1024, 621);
            this.Controls.Add(this.panelGlavno);
            this.Controls.Add(this.panelOrodja);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "StrankeObrazec";
            this.Text = "StrankeObrazec";
            this.panelOrodja.ResumeLayout(false);
            this.panelGlavno.ResumeLayout(false);
            this.splitGlavno.Panel1.ResumeLayout(false);
            this.splitGlavno.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitGlavno)).EndInit();
            this.splitGlavno.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvStranke)).EndInit();
            this.panelDetajli.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panelOrodja;
        private MaterialSkin.Controls.MaterialTextBox2 txtIskanje;
        private System.Windows.Forms.Panel panelGlavno;
        private System.Windows.Forms.Button btnOsvezi;
        private System.Windows.Forms.Button btnDodaj;
        private System.Windows.Forms.SplitContainer splitGlavno;
        private System.Windows.Forms.DataGridView dgvStranke;
        private System.Windows.Forms.Panel panelDetajli;
        private System.Windows.Forms.Label lblEmail;
        private System.Windows.Forms.Label lblEmailNaslov;
        private System.Windows.Forms.Panel panelSeperator1;
        private System.Windows.Forms.Label lblPolnoIme;
        private System.Windows.Forms.Label lblInicialke;
        private System.Windows.Forms.Label lblDetajliNaslov;
        private System.Windows.Forms.Button btnUredi;
        private System.Windows.Forms.Label lblVozniska;
        private System.Windows.Forms.Label lblVozniskaNaslov;
        private System.Windows.Forms.Label lblNaslov;
        private System.Windows.Forms.Label lblNaslovNaslov;
        private System.Windows.Forms.Label lblTelefon;
        private System.Windows.Forms.Label lblTelefonNaslov;
        private System.Windows.Forms.Button btnIzbrisi;
        private System.Windows.Forms.Button btnIzvoz;
        private System.Windows.Forms.Label lblZvestobaNaslov;
        private System.Windows.Forms.Label lblZvestoba;
        private System.Windows.Forms.Label lblZvestobaNapredek;
    }
}
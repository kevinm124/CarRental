namespace CarRental.Obrazci
{
    partial class RezervacijeObrazec
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
            this.btnPogodba = new System.Windows.Forms.Button();
            this.btnRacun = new System.Windows.Forms.Button();
            this.btnEmail = new System.Windows.Forms.Button();
            this.btnPoskodba = new System.Windows.Forms.Button();
            this.panelOrodja = new System.Windows.Forms.Panel();
            this.btnOsvezi = new System.Windows.Forms.Button();
            this.btnNovaRezervacija = new System.Windows.Forms.Button();
            this.cmbFilterStatus = new System.Windows.Forms.ComboBox();
            this.txtIskanje = new MaterialSkin.Controls.MaterialTextBox2();
            this.panelGlavno = new System.Windows.Forms.Panel();
            this.splitGlavno = new System.Windows.Forms.SplitContainer();
            this.dgvRezervacije = new System.Windows.Forms.DataGridView();
            this.panelDetajli = new System.Windows.Forms.Panel();
            this.btnPrekliciRez = new System.Windows.Forms.Button();
            this.btnZakljuci = new System.Windows.Forms.Button();
            this.lblCena = new System.Windows.Forms.Label();
            this.lblCenaNaslov = new System.Windows.Forms.Label();
            this.panelSep2 = new System.Windows.Forms.Panel();
            this.lblSteviloDni = new System.Windows.Forms.Label();
            this.lblObdobje = new System.Windows.Forms.Label();
            this.lblObdobjeNaslov = new System.Windows.Forms.Label();
            this.lblAvto = new System.Windows.Forms.Label();
            this.lblAvtoNaslov = new System.Windows.Forms.Label();
            this.lblStranka = new System.Windows.Forms.Label();
            this.lblStrankaNaslov = new System.Windows.Forms.Label();
            this.panelSep1 = new System.Windows.Forms.Panel();
            this.lblStatus = new System.Windows.Forms.Label();
            this.lblIdRezervacije = new System.Windows.Forms.Label();
            this.lblDetajliNaslov = new System.Windows.Forms.Label();
            this.panelOrodja.SuspendLayout();
            this.panelGlavno.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitGlavno)).BeginInit();
            this.splitGlavno.Panel1.SuspendLayout();
            this.splitGlavno.Panel2.SuspendLayout();
            this.splitGlavno.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvRezervacije)).BeginInit();
            this.panelDetajli.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelOrodja
            // 
            this.panelOrodja.BackColor = System.Drawing.Color.White;
            this.panelOrodja.Controls.Add(this.btnIzvoz);
            this.panelOrodja.Controls.Add(this.btnOsvezi);
            this.panelOrodja.Controls.Add(this.btnNovaRezervacija);
            this.panelOrodja.Controls.Add(this.cmbFilterStatus);
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
            this.btnOsvezi.TabIndex = 3;
            this.btnOsvezi.Text = "🔄";
            this.btnOsvezi.UseVisualStyleBackColor = false;
            this.btnOsvezi.Click += new System.EventHandler(this.btnOsvezi_Click);
            // 
            // btnNovaRezervacija
            // 
            this.btnNovaRezervacija.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(33)))), ((int)(((byte)(150)))), ((int)(((byte)(243)))));
            this.btnNovaRezervacija.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnNovaRezervacija.FlatAppearance.BorderSize = 0;
            this.btnNovaRezervacija.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnNovaRezervacija.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnNovaRezervacija.ForeColor = System.Drawing.Color.White;
            this.btnNovaRezervacija.Location = new System.Drawing.Point(690, 17);
            this.btnNovaRezervacija.Name = "btnNovaRezervacija";
            this.btnNovaRezervacija.Size = new System.Drawing.Size(200, 38);
            this.btnNovaRezervacija.TabIndex = 2;
            this.btnNovaRezervacija.Text = "NOVA REZERVACIJA";
            this.btnNovaRezervacija.UseVisualStyleBackColor = false;
            this.btnNovaRezervacija.Click += new System.EventHandler(this.btnNovaRezervacija_Click);
            // 
            // cmbFilterStatus
            // 
            this.cmbFilterStatus.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbFilterStatus.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cmbFilterStatus.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbFilterStatus.FormattingEnabled = true;
            this.cmbFilterStatus.Location = new System.Drawing.Point(350, 22);
            this.cmbFilterStatus.Name = "cmbFilterStatus";
            this.cmbFilterStatus.Size = new System.Drawing.Size(180, 25);
            this.cmbFilterStatus.TabIndex = 1;
            // 
            // txtIskanje
            // 
            this.txtIskanje.AnimateReadOnly = false;
            this.txtIskanje.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.txtIskanje.CharacterCasing = System.Windows.Forms.CharacterCasing.Normal;
            this.txtIskanje.Depth = 0;
            this.txtIskanje.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.txtIskanje.HideSelection = true;
            this.txtIskanje.Hint = "Išči po stranki, avtu...";
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
            this.txtIskanje.Size = new System.Drawing.Size(320, 48);
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
            this.splitGlavno.Panel1.Controls.Add(this.dgvRezervacije);
            // 
            // splitGlavno.Panel2
            // 
            this.splitGlavno.Panel2.Controls.Add(this.panelDetajli);
            this.splitGlavno.Size = new System.Drawing.Size(994, 521);
            this.splitGlavno.SplitterDistance = 680;
            this.splitGlavno.TabIndex = 0;
            // 
            // dgvRezervacije
            // 
            this.dgvRezervacije.AllowUserToAddRows = false;
            this.dgvRezervacije.AllowUserToDeleteRows = false;
            this.dgvRezervacije.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvRezervacije.BackgroundColor = System.Drawing.Color.White;
            this.dgvRezervacije.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dgvRezervacije.ColumnHeadersHeight = 40;
            this.dgvRezervacije.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.dgvRezervacije.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvRezervacije.Location = new System.Drawing.Point(0, 0);
            this.dgvRezervacije.MultiSelect = false;
            this.dgvRezervacije.Name = "dgvRezervacije";
            this.dgvRezervacije.ReadOnly = true;
            this.dgvRezervacije.RowHeadersVisible = false;
            this.dgvRezervacije.RowTemplate.Height = 35;
            this.dgvRezervacije.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvRezervacije.Size = new System.Drawing.Size(680, 521);
            this.dgvRezervacije.TabIndex = 0;
            // 
            // panelDetajli
            // 
            this.panelDetajli.BackColor = System.Drawing.Color.White;
            this.panelDetajli.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panelDetajli.Controls.Add(this.btnPoskodba);
            this.panelDetajli.Controls.Add(this.btnEmail);
            this.panelDetajli.Controls.Add(this.btnRacun);
            this.panelDetajli.Controls.Add(this.btnPogodba);
            this.panelDetajli.Controls.Add(this.btnPrekliciRez);
            this.panelDetajli.Controls.Add(this.btnZakljuci);
            this.panelDetajli.Controls.Add(this.lblCena);
            this.panelDetajli.Controls.Add(this.lblCenaNaslov);
            this.panelDetajli.Controls.Add(this.panelSep2);
            this.panelDetajli.Controls.Add(this.lblSteviloDni);
            this.panelDetajli.Controls.Add(this.lblObdobje);
            this.panelDetajli.Controls.Add(this.lblObdobjeNaslov);
            this.panelDetajli.Controls.Add(this.lblAvto);
            this.panelDetajli.Controls.Add(this.lblAvtoNaslov);
            this.panelDetajli.Controls.Add(this.lblStranka);
            this.panelDetajli.Controls.Add(this.lblStrankaNaslov);
            this.panelDetajli.Controls.Add(this.panelSep1);
            this.panelDetajli.Controls.Add(this.lblStatus);
            this.panelDetajli.Controls.Add(this.lblIdRezervacije);
            this.panelDetajli.Controls.Add(this.lblDetajliNaslov);
            this.panelDetajli.AutoScroll = true;
            this.panelDetajli.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelDetajli.Location = new System.Drawing.Point(0, 0);
            this.panelDetajli.Name = "panelDetajli";
            this.panelDetajli.Padding = new System.Windows.Forms.Padding(20);
            this.panelDetajli.Size = new System.Drawing.Size(310, 521);
            this.panelDetajli.TabIndex = 0;
            // 
            // btnPrekliciRez
            // 
            this.btnPrekliciRez.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(211)))), ((int)(((byte)(47)))), ((int)(((byte)(47)))));
            this.btnPrekliciRez.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnPrekliciRez.FlatAppearance.BorderSize = 0;
            this.btnPrekliciRez.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnPrekliciRez.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnPrekliciRez.ForeColor = System.Drawing.Color.White;
            this.btnPrekliciRez.Location = new System.Drawing.Point(165, 450);
            this.btnPrekliciRez.Name = "btnPrekliciRez";
            this.btnPrekliciRez.Size = new System.Drawing.Size(130, 40);
            this.btnPrekliciRez.TabIndex = 15;
            this.btnPrekliciRez.Text = "PREKLIČI";
            this.btnPrekliciRez.UseVisualStyleBackColor = false;
            this.btnPrekliciRez.Click += new System.EventHandler(this.btnPrekliciRez_Click);
            // 
            // btnZakljuci
            // 
            this.btnZakljuci.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(76)))), ((int)(((byte)(175)))), ((int)(((byte)(80)))));
            this.btnZakljuci.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnZakljuci.FlatAppearance.BorderSize = 0;
            this.btnZakljuci.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnZakljuci.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnZakljuci.ForeColor = System.Drawing.Color.White;
            this.btnZakljuci.Location = new System.Drawing.Point(20, 450);
            this.btnZakljuci.Name = "btnZakljuci";
            this.btnZakljuci.Size = new System.Drawing.Size(130, 40);
            this.btnZakljuci.TabIndex = 14;
            this.btnZakljuci.Text = "ZAKLJUČI";
            this.btnZakljuci.UseVisualStyleBackColor = false;
            this.btnZakljuci.Click += new System.EventHandler(this.btnZakljuci_Click);
            // 
            // lblCena
            // 
            this.lblCena.Font = new System.Drawing.Font("Segoe UI", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCena.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(76)))), ((int)(((byte)(175)))), ((int)(((byte)(80)))));
            this.lblCena.Location = new System.Drawing.Point(20, 360);
            this.lblCena.Name = "lblCena";
            this.lblCena.Size = new System.Drawing.Size(280, 35);
            this.lblCena.TabIndex = 13;
            this.lblCena.Text = "- €";
            // 
            // lblCenaNaslov
            // 
            this.lblCenaNaslov.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCenaNaslov.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(120)))), ((int)(((byte)(120)))), ((int)(((byte)(120)))));
            this.lblCenaNaslov.Location = new System.Drawing.Point(20, 340);
            this.lblCenaNaslov.Name = "lblCenaNaslov";
            this.lblCenaNaslov.Size = new System.Drawing.Size(150, 18);
            this.lblCenaNaslov.TabIndex = 12;
            this.lblCenaNaslov.Text = "SKUPNA CENA";
            // 
            // panelSep2
            // 
            this.panelSep2.BackColor = System.Drawing.Color.Gainsboro;
            this.panelSep2.Location = new System.Drawing.Point(20, 325);
            this.panelSep2.Name = "panelSep2";
            this.panelSep2.Size = new System.Drawing.Size(280, 1);
            this.panelSep2.TabIndex = 11;
            // 
            // lblSteviloDni
            // 
            this.lblSteviloDni.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSteviloDni.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(120)))), ((int)(((byte)(120)))), ((int)(((byte)(120)))));
            this.lblSteviloDni.Location = new System.Drawing.Point(20, 290);
            this.lblSteviloDni.Name = "lblSteviloDni";
            this.lblSteviloDni.Size = new System.Drawing.Size(280, 20);
            this.lblSteviloDni.TabIndex = 10;
            this.lblSteviloDni.Text = "-";
            // 
            // lblObdobje
            // 
            this.lblObdobje.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblObdobje.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(33)))), ((int)(((byte)(33)))), ((int)(((byte)(33)))));
            this.lblObdobje.Location = new System.Drawing.Point(20, 270);
            this.lblObdobje.Name = "lblObdobje";
            this.lblObdobje.Size = new System.Drawing.Size(280, 20);
            this.lblObdobje.TabIndex = 9;
            this.lblObdobje.Text = "-";
            // 
            // lblObdobjeNaslov
            // 
            this.lblObdobjeNaslov.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblObdobjeNaslov.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(120)))), ((int)(((byte)(120)))), ((int)(((byte)(120)))));
            this.lblObdobjeNaslov.Location = new System.Drawing.Point(20, 250);
            this.lblObdobjeNaslov.Name = "lblObdobjeNaslov";
            this.lblObdobjeNaslov.Size = new System.Drawing.Size(150, 18);
            this.lblObdobjeNaslov.TabIndex = 8;
            this.lblObdobjeNaslov.Text = "OBDOBJE";
            // 
            // lblAvto
            // 
            this.lblAvto.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblAvto.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(33)))), ((int)(((byte)(33)))), ((int)(((byte)(33)))));
            this.lblAvto.Location = new System.Drawing.Point(20, 215);
            this.lblAvto.Name = "lblAvto";
            this.lblAvto.Size = new System.Drawing.Size(280, 20);
            this.lblAvto.TabIndex = 7;
            this.lblAvto.Text = "-";
            // 
            // lblAvtoNaslov
            // 
            this.lblAvtoNaslov.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblAvtoNaslov.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(120)))), ((int)(((byte)(120)))), ((int)(((byte)(120)))));
            this.lblAvtoNaslov.Location = new System.Drawing.Point(20, 195);
            this.lblAvtoNaslov.Name = "lblAvtoNaslov";
            this.lblAvtoNaslov.Size = new System.Drawing.Size(150, 18);
            this.lblAvtoNaslov.TabIndex = 6;
            this.lblAvtoNaslov.Text = "AVTOMOBIL";
            // 
            // lblStranka
            // 
            this.lblStranka.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblStranka.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(33)))), ((int)(((byte)(33)))), ((int)(((byte)(33)))));
            this.lblStranka.Location = new System.Drawing.Point(20, 160);
            this.lblStranka.Name = "lblStranka";
            this.lblStranka.Size = new System.Drawing.Size(280, 20);
            this.lblStranka.TabIndex = 5;
            this.lblStranka.Text = "-";
            // 
            // lblStrankaNaslov
            // 
            this.lblStrankaNaslov.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblStrankaNaslov.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(120)))), ((int)(((byte)(120)))), ((int)(((byte)(120)))));
            this.lblStrankaNaslov.Location = new System.Drawing.Point(20, 140);
            this.lblStrankaNaslov.Name = "lblStrankaNaslov";
            this.lblStrankaNaslov.Size = new System.Drawing.Size(150, 18);
            this.lblStrankaNaslov.TabIndex = 4;
            this.lblStrankaNaslov.Text = "STRANKA";
            // 
            // panelSep1
            // 
            this.panelSep1.BackColor = System.Drawing.Color.Gainsboro;
            this.panelSep1.Location = new System.Drawing.Point(20, 125);
            this.panelSep1.Name = "panelSep1";
            this.panelSep1.Size = new System.Drawing.Size(280, 1);
            this.panelSep1.TabIndex = 3;
            // 
            // lblStatus
            // 
            this.lblStatus.AutoSize = true;
            this.lblStatus.BackColor = System.Drawing.Color.Gray;
            this.lblStatus.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblStatus.ForeColor = System.Drawing.Color.White;
            this.lblStatus.Location = new System.Drawing.Point(20, 85);
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(12, 15);
            this.lblStatus.TabIndex = 2;
            this.lblStatus.Text = "-";
            this.lblStatus.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblIdRezervacije
            // 
            this.lblIdRezervacije.Font = new System.Drawing.Font("Segoe UI", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblIdRezervacije.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(33)))), ((int)(((byte)(150)))), ((int)(((byte)(243)))));
            this.lblIdRezervacije.Location = new System.Drawing.Point(20, 50);
            this.lblIdRezervacije.Name = "lblIdRezervacije";
            this.lblIdRezervacije.Size = new System.Drawing.Size(280, 25);
            this.lblIdRezervacije.TabIndex = 1;
            this.lblIdRezervacije.Text = "Rezervacija #-";
            // 
            // lblDetajliNaslov
            // 
            this.lblDetajliNaslov.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDetajliNaslov.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(33)))), ((int)(((byte)(33)))), ((int)(((byte)(33)))));
            this.lblDetajliNaslov.Location = new System.Drawing.Point(20, 15);
            this.lblDetajliNaslov.Name = "lblDetajliNaslov";
            this.lblDetajliNaslov.Size = new System.Drawing.Size(280, 25);
            this.lblDetajliNaslov.TabIndex = 0;
            this.lblDetajliNaslov.Text = "DETAJLI REZERVACIJE";
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
            // btnPogodba
            //
            this.btnPogodba.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(33)))), ((int)(((byte)(150)))), ((int)(((byte)(243)))));
            this.btnPogodba.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnPogodba.FlatAppearance.BorderSize = 0;
            this.btnPogodba.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnPogodba.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnPogodba.ForeColor = System.Drawing.Color.White;
            this.btnPogodba.Location = new System.Drawing.Point(20, 500);
            this.btnPogodba.Name = "btnPogodba";
            this.btnPogodba.Size = new System.Drawing.Size(130, 40);
            this.btnPogodba.TabIndex = 21;
            this.btnPogodba.Text = "📄 POGODBA";
            this.btnPogodba.UseVisualStyleBackColor = false;
            this.btnPogodba.Click += new System.EventHandler(this.btnPogodba_Click);
            //
            // btnRacun
            //
            this.btnRacun.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(126)))), ((int)(((byte)(87)))), ((int)(((byte)(194)))));
            this.btnRacun.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnRacun.FlatAppearance.BorderSize = 0;
            this.btnRacun.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnRacun.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnRacun.ForeColor = System.Drawing.Color.White;
            this.btnRacun.Location = new System.Drawing.Point(165, 500);
            this.btnRacun.Name = "btnRacun";
            this.btnRacun.Size = new System.Drawing.Size(130, 40);
            this.btnRacun.TabIndex = 22;
            this.btnRacun.Text = "🧾 RAČUN";
            this.btnRacun.UseVisualStyleBackColor = false;
            this.btnRacun.Click += new System.EventHandler(this.btnRacun_Click);
            //
            // btnEmail
            //
            this.btnEmail.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(150)))), ((int)(((byte)(136)))));
            this.btnEmail.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnEmail.FlatAppearance.BorderSize = 0;
            this.btnEmail.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnEmail.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnEmail.ForeColor = System.Drawing.Color.White;
            this.btnEmail.Location = new System.Drawing.Point(20, 550);
            this.btnEmail.Name = "btnEmail";
            this.btnEmail.Size = new System.Drawing.Size(275, 40);
            this.btnEmail.TabIndex = 23;
            this.btnEmail.Text = "✉ POŠLJI POTRDILO";
            this.btnEmail.UseVisualStyleBackColor = false;
            this.btnEmail.Click += new System.EventHandler(this.btnEmail_Click);
            //
            // btnPoskodba
            //
            this.btnPoskodba.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(96)))), ((int)(((byte)(125)))), ((int)(((byte)(139)))));
            this.btnPoskodba.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnPoskodba.FlatAppearance.BorderSize = 0;
            this.btnPoskodba.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnPoskodba.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnPoskodba.ForeColor = System.Drawing.Color.White;
            this.btnPoskodba.Location = new System.Drawing.Point(20, 600);
            this.btnPoskodba.Name = "btnPoskodba";
            this.btnPoskodba.Size = new System.Drawing.Size(275, 40);
            this.btnPoskodba.TabIndex = 24;
            this.btnPoskodba.Text = "⚠ PRIJAVI POŠKODBO";
            this.btnPoskodba.UseVisualStyleBackColor = false;
            this.btnPoskodba.Click += new System.EventHandler(this.btnPoskodba_Click);
            // 
            // RezervacijeObrazec
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.WhiteSmoke;
            this.ClientSize = new System.Drawing.Size(1024, 621);
            this.Controls.Add(this.panelGlavno);
            this.Controls.Add(this.panelOrodja);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "RezervacijeObrazec";
            this.Text = "Rezervacije";
            this.panelOrodja.ResumeLayout(false);
            this.panelGlavno.ResumeLayout(false);
            this.splitGlavno.Panel1.ResumeLayout(false);
            this.splitGlavno.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitGlavno)).EndInit();
            this.splitGlavno.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvRezervacije)).EndInit();
            this.panelDetajli.ResumeLayout(false);
            this.panelDetajli.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panelOrodja;
        private MaterialSkin.Controls.MaterialTextBox2 txtIskanje;
        private System.Windows.Forms.Panel panelGlavno;
        private System.Windows.Forms.Button btnNovaRezervacija;
        private System.Windows.Forms.ComboBox cmbFilterStatus;
        private System.Windows.Forms.Button btnOsvezi;
        private System.Windows.Forms.SplitContainer splitGlavno;
        private System.Windows.Forms.DataGridView dgvRezervacije;
        private System.Windows.Forms.Panel panelDetajli;
        private System.Windows.Forms.Label lblStrankaNaslov;
        private System.Windows.Forms.Panel panelSep1;
        private System.Windows.Forms.Label lblStatus;
        private System.Windows.Forms.Label lblIdRezervacije;
        private System.Windows.Forms.Label lblDetajliNaslov;
        private System.Windows.Forms.Label lblSteviloDni;
        private System.Windows.Forms.Label lblObdobje;
        private System.Windows.Forms.Label lblObdobjeNaslov;
        private System.Windows.Forms.Label lblAvto;
        private System.Windows.Forms.Label lblAvtoNaslov;
        private System.Windows.Forms.Label lblStranka;
        private System.Windows.Forms.Button btnZakljuci;
        private System.Windows.Forms.Label lblCena;
        private System.Windows.Forms.Label lblCenaNaslov;
        private System.Windows.Forms.Panel panelSep2;
        private System.Windows.Forms.Button btnPrekliciRez;
        private System.Windows.Forms.Button btnIzvoz;
        private System.Windows.Forms.Button btnPogodba;
        private System.Windows.Forms.Button btnRacun;
        private System.Windows.Forms.Button btnEmail;
        private System.Windows.Forms.Button btnPoskodba;
    }
}
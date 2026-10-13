namespace CarRental.Obrazci
{
    partial class NovaRezervacijaObrazec
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
            this.lblStrankaNaslov = new System.Windows.Forms.Label();
            this.lblAvtoNaslov = new System.Windows.Forms.Label();
            this.lblDatumOd = new System.Windows.Forms.Label();
            this.lblDatumDo = new System.Windows.Forms.Label();
            this.lblStatusNaslov = new System.Windows.Forms.Label();
            this.cmbStranka = new System.Windows.Forms.ComboBox();
            this.cmbAvto = new System.Windows.Forms.ComboBox();
            this.dtpDatumOd = new System.Windows.Forms.DateTimePicker();
            this.dtpDatumDo = new System.Windows.Forms.DateTimePicker();
            this.cmbStatus = new System.Windows.Forms.ComboBox();
            this.panelPovzetek = new System.Windows.Forms.Panel();
            this.lblPovzetekNaslov = new System.Windows.Forms.Label();
            this.lblDniNaslov = new System.Windows.Forms.Label();
            this.lblSteviloDni = new System.Windows.Forms.Label();
            this.lblCenaDan = new System.Windows.Forms.Label();
            this.panelPovzSep = new System.Windows.Forms.Panel();
            this.lblSkupajNaslov = new System.Windows.Forms.Label();
            this.lblSkupaj = new System.Windows.Forms.Label();
            this.lblOpomba = new System.Windows.Forms.Label();
            this.btnShrani = new System.Windows.Forms.Button();
            this.btnPreklici = new System.Windows.Forms.Button();
            this.panelNaslov.SuspendLayout();
            this.panelGumbi.SuspendLayout();
            this.panelGlavno.SuspendLayout();
            this.panelPovzetek.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelNaslov
            // 
            this.panelNaslov.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(33)))), ((int)(((byte)(150)))), ((int)(((byte)(243)))));
            this.panelNaslov.Controls.Add(this.lblNaslovDialoga);
            this.panelNaslov.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelNaslov.Location = new System.Drawing.Point(0, 0);
            this.panelNaslov.Name = "panelNaslov";
            this.panelNaslov.Size = new System.Drawing.Size(684, 60);
            this.panelNaslov.TabIndex = 0;
            // 
            // panelGumbi
            // 
            this.panelGumbi.BackColor = System.Drawing.Color.WhiteSmoke;
            this.panelGumbi.Controls.Add(this.btnPreklici);
            this.panelGumbi.Controls.Add(this.btnShrani);
            this.panelGumbi.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelGumbi.Location = new System.Drawing.Point(0, 541);
            this.panelGumbi.Name = "panelGumbi";
            this.panelGumbi.Size = new System.Drawing.Size(684, 70);
            this.panelGumbi.TabIndex = 1;
            // 
            // panelGlavno
            // 
            this.panelGlavno.BackColor = System.Drawing.Color.White;
            this.panelGlavno.Controls.Add(this.lblOpomba);
            this.panelGlavno.Controls.Add(this.panelPovzetek);
            this.panelGlavno.Controls.Add(this.cmbStatus);
            this.panelGlavno.Controls.Add(this.dtpDatumDo);
            this.panelGlavno.Controls.Add(this.dtpDatumOd);
            this.panelGlavno.Controls.Add(this.cmbAvto);
            this.panelGlavno.Controls.Add(this.cmbStranka);
            this.panelGlavno.Controls.Add(this.lblStatusNaslov);
            this.panelGlavno.Controls.Add(this.lblDatumDo);
            this.panelGlavno.Controls.Add(this.lblDatumOd);
            this.panelGlavno.Controls.Add(this.lblAvtoNaslov);
            this.panelGlavno.Controls.Add(this.lblStrankaNaslov);
            this.panelGlavno.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelGlavno.Location = new System.Drawing.Point(0, 60);
            this.panelGlavno.Name = "panelGlavno";
            this.panelGlavno.Padding = new System.Windows.Forms.Padding(30, 25, 30, 25);
            this.panelGlavno.Size = new System.Drawing.Size(684, 481);
            this.panelGlavno.TabIndex = 2;
            // 
            // lblNaslovDialoga
            // 
            this.lblNaslovDialoga.AutoSize = true;
            this.lblNaslovDialoga.BackColor = System.Drawing.Color.Transparent;
            this.lblNaslovDialoga.Font = new System.Drawing.Font("Segoe UI", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblNaslovDialoga.ForeColor = System.Drawing.Color.White;
            this.lblNaslovDialoga.Location = new System.Drawing.Point(20, 15);
            this.lblNaslovDialoga.Name = "lblNaslovDialoga";
            this.lblNaslovDialoga.Size = new System.Drawing.Size(159, 25);
            this.lblNaslovDialoga.TabIndex = 0;
            this.lblNaslovDialoga.Text = "Nova rezervacija";
            // 
            // lblStrankaNaslov
            // 
            this.lblStrankaNaslov.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblStrankaNaslov.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(80)))), ((int)(((byte)(80)))), ((int)(((byte)(80)))));
            this.lblStrankaNaslov.Location = new System.Drawing.Point(12, 3);
            this.lblStrankaNaslov.Name = "lblStrankaNaslov";
            this.lblStrankaNaslov.Size = new System.Drawing.Size(280, 18);
            this.lblStrankaNaslov.TabIndex = 0;
            this.lblStrankaNaslov.Text = "Stranka *";
            // 
            // lblAvtoNaslov
            // 
            this.lblAvtoNaslov.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblAvtoNaslov.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(80)))), ((int)(((byte)(80)))), ((int)(((byte)(80)))));
            this.lblAvtoNaslov.Location = new System.Drawing.Point(12, 68);
            this.lblAvtoNaslov.Name = "lblAvtoNaslov";
            this.lblAvtoNaslov.Size = new System.Drawing.Size(280, 18);
            this.lblAvtoNaslov.TabIndex = 1;
            this.lblAvtoNaslov.Text = "Avtomobil *";
            // 
            // lblDatumOd
            // 
            this.lblDatumOd.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDatumOd.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(80)))), ((int)(((byte)(80)))), ((int)(((byte)(80)))));
            this.lblDatumOd.Location = new System.Drawing.Point(12, 138);
            this.lblDatumOd.Name = "lblDatumOd";
            this.lblDatumOd.Size = new System.Drawing.Size(280, 18);
            this.lblDatumOd.TabIndex = 2;
            this.lblDatumOd.Text = "Datum od *";
            // 
            // lblDatumDo
            // 
            this.lblDatumDo.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDatumDo.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(80)))), ((int)(((byte)(80)))), ((int)(((byte)(80)))));
            this.lblDatumDo.Location = new System.Drawing.Point(332, 138);
            this.lblDatumDo.Name = "lblDatumDo";
            this.lblDatumDo.Size = new System.Drawing.Size(280, 18);
            this.lblDatumDo.TabIndex = 3;
            this.lblDatumDo.Text = "Datum do *";
            // 
            // lblStatusNaslov
            // 
            this.lblStatusNaslov.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblStatusNaslov.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(80)))), ((int)(((byte)(80)))), ((int)(((byte)(80)))));
            this.lblStatusNaslov.Location = new System.Drawing.Point(12, 208);
            this.lblStatusNaslov.Name = "lblStatusNaslov";
            this.lblStatusNaslov.Size = new System.Drawing.Size(280, 18);
            this.lblStatusNaslov.TabIndex = 4;
            this.lblStatusNaslov.Text = "Status";
            // 
            // cmbStranka
            // 
            this.cmbStranka.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbStranka.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbStranka.FormattingEnabled = true;
            this.cmbStranka.Location = new System.Drawing.Point(12, 25);
            this.cmbStranka.Name = "cmbStranka";
            this.cmbStranka.Size = new System.Drawing.Size(620, 25);
            this.cmbStranka.TabIndex = 12;
            // 
            // cmbAvto
            // 
            this.cmbAvto.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbAvto.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbAvto.FormattingEnabled = true;
            this.cmbAvto.Location = new System.Drawing.Point(12, 90);
            this.cmbAvto.Name = "cmbAvto";
            this.cmbAvto.Size = new System.Drawing.Size(620, 25);
            this.cmbAvto.TabIndex = 13;
            // 
            // dtpDatumOd
            // 
            this.dtpDatumOd.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtpDatumOd.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpDatumOd.Location = new System.Drawing.Point(12, 160);
            this.dtpDatumOd.Name = "dtpDatumOd";
            this.dtpDatumOd.Size = new System.Drawing.Size(300, 25);
            this.dtpDatumOd.TabIndex = 14;
            // 
            // dtpDatumDo
            // 
            this.dtpDatumDo.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtpDatumDo.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpDatumDo.Location = new System.Drawing.Point(332, 160);
            this.dtpDatumDo.Name = "dtpDatumDo";
            this.dtpDatumDo.Size = new System.Drawing.Size(300, 25);
            this.dtpDatumDo.TabIndex = 15;
            // 
            // cmbStatus
            // 
            this.cmbStatus.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbStatus.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbStatus.FormattingEnabled = true;
            this.cmbStatus.Location = new System.Drawing.Point(12, 230);
            this.cmbStatus.Name = "cmbStatus";
            this.cmbStatus.Size = new System.Drawing.Size(300, 25);
            this.cmbStatus.TabIndex = 16;
            // 
            // panelPovzetek
            // 
            this.panelPovzetek.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(250)))), ((int)(((byte)(255)))));
            this.panelPovzetek.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panelPovzetek.Controls.Add(this.lblSkupaj);
            this.panelPovzetek.Controls.Add(this.lblSkupajNaslov);
            this.panelPovzetek.Controls.Add(this.panelPovzSep);
            this.panelPovzetek.Controls.Add(this.lblCenaDan);
            this.panelPovzetek.Controls.Add(this.lblSteviloDni);
            this.panelPovzetek.Controls.Add(this.lblDniNaslov);
            this.panelPovzetek.Controls.Add(this.lblPovzetekNaslov);
            this.panelPovzetek.Location = new System.Drawing.Point(0, 290);
            this.panelPovzetek.Name = "panelPovzetek";
            this.panelPovzetek.Size = new System.Drawing.Size(620, 170);
            this.panelPovzetek.TabIndex = 17;
            // 
            // lblPovzetekNaslov
            // 
            this.lblPovzetekNaslov.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPovzetekNaslov.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(33)))), ((int)(((byte)(150)))), ((int)(((byte)(243)))));
            this.lblPovzetekNaslov.Location = new System.Drawing.Point(15, 10);
            this.lblPovzetekNaslov.Name = "lblPovzetekNaslov";
            this.lblPovzetekNaslov.Size = new System.Drawing.Size(300, 20);
            this.lblPovzetekNaslov.TabIndex = 0;
            this.lblPovzetekNaslov.Text = "POVZETEK REZERVACIJE";
            // 
            // lblDniNaslov
            // 
            this.lblDniNaslov.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDniNaslov.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(80)))), ((int)(((byte)(80)))), ((int)(((byte)(80)))));
            this.lblDniNaslov.Location = new System.Drawing.Point(15, 45);
            this.lblDniNaslov.Name = "lblDniNaslov";
            this.lblDniNaslov.Size = new System.Drawing.Size(150, 18);
            this.lblDniNaslov.TabIndex = 1;
            this.lblDniNaslov.Text = "Število dni:";
            // 
            // lblSteviloDni
            // 
            this.lblSteviloDni.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSteviloDni.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(33)))), ((int)(((byte)(33)))), ((int)(((byte)(33)))));
            this.lblSteviloDni.Location = new System.Drawing.Point(170, 45);
            this.lblSteviloDni.Name = "lblSteviloDni";
            this.lblSteviloDni.Size = new System.Drawing.Size(200, 18);
            this.lblSteviloDni.TabIndex = 2;
            this.lblSteviloDni.Text = "0 dni";
            // 
            // lblCenaDan
            // 
            this.lblCenaDan.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCenaDan.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(33)))), ((int)(((byte)(33)))), ((int)(((byte)(33)))));
            this.lblCenaDan.Location = new System.Drawing.Point(170, 70);
            this.lblCenaDan.Name = "lblCenaDan";
            this.lblCenaDan.Size = new System.Drawing.Size(200, 18);
            this.lblCenaDan.TabIndex = 3;
            this.lblCenaDan.Text = "0,00 €";
            // 
            // panelPovzSep
            // 
            this.panelPovzSep.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(200)))), ((int)(((byte)(200)))), ((int)(((byte)(200)))));
            this.panelPovzSep.Location = new System.Drawing.Point(15, 105);
            this.panelPovzSep.Name = "panelPovzSep";
            this.panelPovzSep.Size = new System.Drawing.Size(590, 1);
            this.panelPovzSep.TabIndex = 4;
            // 
            // lblSkupajNaslov
            // 
            this.lblSkupajNaslov.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSkupajNaslov.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(33)))), ((int)(((byte)(33)))), ((int)(((byte)(33)))));
            this.lblSkupajNaslov.Location = new System.Drawing.Point(15, 120);
            this.lblSkupajNaslov.Name = "lblSkupajNaslov";
            this.lblSkupajNaslov.Size = new System.Drawing.Size(200, 25);
            this.lblSkupajNaslov.TabIndex = 5;
            this.lblSkupajNaslov.Text = "SKUPAJ ZA PLAČILO:";
            // 
            // lblSkupaj
            // 
            this.lblSkupaj.Font = new System.Drawing.Font("Segoe UI", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSkupaj.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(76)))), ((int)(((byte)(175)))), ((int)(((byte)(80)))));
            this.lblSkupaj.Location = new System.Drawing.Point(380, 115);
            this.lblSkupaj.Name = "lblSkupaj";
            this.lblSkupaj.Size = new System.Drawing.Size(225, 35);
            this.lblSkupaj.TabIndex = 6;
            this.lblSkupaj.Text = "0,00 €";
            this.lblSkupaj.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblOpomba
            // 
            this.lblOpomba.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblOpomba.ForeColor = System.Drawing.Color.Gray;
            this.lblOpomba.Location = new System.Drawing.Point(12, 464);
            this.lblOpomba.Name = "lblOpomba";
            this.lblOpomba.Size = new System.Drawing.Size(200, 15);
            this.lblOpomba.TabIndex = 18;
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
            this.btnShrani.Location = new System.Drawing.Point(430, 15);
            this.btnShrani.Name = "btnShrani";
            this.btnShrani.Size = new System.Drawing.Size(230, 40);
            this.btnShrani.TabIndex = 0;
            this.btnShrani.Text = "POTRDI REZERVACIJO";
            this.btnShrani.UseVisualStyleBackColor = false;
            this.btnShrani.Click += new System.EventHandler(this.btnShrani_Click);
            // 
            // btnPreklici
            // 
            this.btnPreklici.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnPreklici.BackColor = System.Drawing.Color.WhiteSmoke;
            this.btnPreklici.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnPreklici.FlatAppearance.BorderSize = 0;
            this.btnPreklici.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnPreklici.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnPreklici.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(80)))), ((int)(((byte)(80)))), ((int)(((byte)(80)))));
            this.btnPreklici.Location = new System.Drawing.Point(270, 15);
            this.btnPreklici.Name = "btnPreklici";
            this.btnPreklici.Size = new System.Drawing.Size(150, 40);
            this.btnPreklici.TabIndex = 1;
            this.btnPreklici.Text = "PREKLIČI";
            this.btnPreklici.UseVisualStyleBackColor = false;
            this.btnPreklici.Click += new System.EventHandler(this.btnPreklici_Click);
            // 
            // NovaRezervacijaObrazec
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(684, 611);
            this.Controls.Add(this.panelGlavno);
            this.Controls.Add(this.panelGumbi);
            this.Controls.Add(this.panelNaslov);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "NovaRezervacijaObrazec";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Nova rezervacija";
            this.panelNaslov.ResumeLayout(false);
            this.panelNaslov.PerformLayout();
            this.panelGumbi.ResumeLayout(false);
            this.panelGlavno.ResumeLayout(false);
            this.panelPovzetek.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panelNaslov;
        private System.Windows.Forms.Label lblNaslovDialoga;
        private System.Windows.Forms.Panel panelGumbi;
        private System.Windows.Forms.Panel panelGlavno;
        private System.Windows.Forms.ComboBox cmbStranka;
        private System.Windows.Forms.Label lblStatusNaslov;
        private System.Windows.Forms.Label lblDatumDo;
        private System.Windows.Forms.Label lblDatumOd;
        private System.Windows.Forms.Label lblAvtoNaslov;
        private System.Windows.Forms.Label lblStrankaNaslov;
        private System.Windows.Forms.ComboBox cmbAvto;
        private System.Windows.Forms.DateTimePicker dtpDatumOd;
        private System.Windows.Forms.DateTimePicker dtpDatumDo;
        private System.Windows.Forms.Panel panelPovzetek;
        private System.Windows.Forms.ComboBox cmbStatus;
        private System.Windows.Forms.Label lblPovzetekNaslov;
        private System.Windows.Forms.Label lblOpomba;
        private System.Windows.Forms.Label lblSkupaj;
        private System.Windows.Forms.Label lblSkupajNaslov;
        private System.Windows.Forms.Panel panelPovzSep;
        private System.Windows.Forms.Label lblCenaDan;
        private System.Windows.Forms.Label lblSteviloDni;
        private System.Windows.Forms.Label lblDniNaslov;
        private System.Windows.Forms.Button btnPreklici;
        private System.Windows.Forms.Button btnShrani;
    }
}
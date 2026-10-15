namespace CarRental.Obrazci
{
    partial class PlacilaObrazec
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
            this.panelStatistike = new System.Windows.Forms.Panel();
            this.panelOrodja = new System.Windows.Forms.Panel();
            this.panelGlavno = new System.Windows.Forms.Panel();
            this.panelKartica1 = new System.Windows.Forms.Panel();
            this.lblIkona1 = new System.Windows.Forms.Label();
            this.lblNaslov1 = new System.Windows.Forms.Label();
            this.lblVrednost1 = new System.Windows.Forms.Label();
            this.panelKartica2 = new System.Windows.Forms.Panel();
            this.lblIkona2 = new System.Windows.Forms.Label();
            this.lblNaslov2 = new System.Windows.Forms.Label();
            this.lblVrednost2 = new System.Windows.Forms.Label();
            this.panelKartica3 = new System.Windows.Forms.Panel();
            this.lblIkona3 = new System.Windows.Forms.Label();
            this.lblNaslov3 = new System.Windows.Forms.Label();
            this.lblVrednost3 = new System.Windows.Forms.Label();
            this.txtIskanje = new MaterialSkin.Controls.MaterialTextBox2();
            this.cmbFilterNacin = new System.Windows.Forms.ComboBox();
            this.btnNovoPlacilo = new System.Windows.Forms.Button();
            this.btnOsvezi = new System.Windows.Forms.Button();
            this.dgvPlacila = new System.Windows.Forms.DataGridView();
            this.panelStatistike.SuspendLayout();
            this.panelOrodja.SuspendLayout();
            this.panelGlavno.SuspendLayout();
            this.panelKartica1.SuspendLayout();
            this.panelKartica2.SuspendLayout();
            this.panelKartica3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvPlacila)).BeginInit();
            this.SuspendLayout();
            // 
            // panelStatistike
            // 
            this.panelStatistike.Controls.Add(this.panelKartica3);
            this.panelStatistike.Controls.Add(this.panelKartica2);
            this.panelStatistike.Controls.Add(this.panelKartica1);
            this.panelStatistike.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelStatistike.Location = new System.Drawing.Point(0, 0);
            this.panelStatistike.Name = "panelStatistike";
            this.panelStatistike.Padding = new System.Windows.Forms.Padding(15);
            this.panelStatistike.Size = new System.Drawing.Size(1024, 120);
            this.panelStatistike.TabIndex = 0;
            // 
            // panelOrodja
            // 
            this.panelOrodja.BackColor = System.Drawing.Color.White;
            this.panelOrodja.Controls.Add(this.btnIzvoz);
            this.panelOrodja.Controls.Add(this.btnOsvezi);
            this.panelOrodja.Controls.Add(this.btnNovoPlacilo);
            this.panelOrodja.Controls.Add(this.cmbFilterNacin);
            this.panelOrodja.Controls.Add(this.txtIskanje);
            this.panelOrodja.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelOrodja.Location = new System.Drawing.Point(0, 120);
            this.panelOrodja.Name = "panelOrodja";
            this.panelOrodja.Padding = new System.Windows.Forms.Padding(15);
            this.panelOrodja.Size = new System.Drawing.Size(1024, 70);
            this.panelOrodja.TabIndex = 1;
            // 
            // panelGlavno
            // 
            this.panelGlavno.Controls.Add(this.dgvPlacila);
            this.panelGlavno.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelGlavno.Location = new System.Drawing.Point(0, 190);
            this.panelGlavno.Name = "panelGlavno";
            this.panelGlavno.Padding = new System.Windows.Forms.Padding(15);
            this.panelGlavno.Size = new System.Drawing.Size(1024, 431);
            this.panelGlavno.TabIndex = 2;
            // 
            // panelKartica1
            // 
            this.panelKartica1.BackColor = System.Drawing.Color.White;
            this.panelKartica1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panelKartica1.Controls.Add(this.lblVrednost1);
            this.panelKartica1.Controls.Add(this.lblNaslov1);
            this.panelKartica1.Controls.Add(this.lblIkona1);
            this.panelKartica1.Location = new System.Drawing.Point(15, 15);
            this.panelKartica1.Name = "panelKartica1";
            this.panelKartica1.Size = new System.Drawing.Size(320, 90);
            this.panelKartica1.TabIndex = 0;
            // 
            // lblIkona1
            // 
            this.lblIkona1.Font = new System.Drawing.Font("Segoe UI", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblIkona1.Location = new System.Drawing.Point(20, 20);
            this.lblIkona1.Name = "lblIkona1";
            this.lblIkona1.Size = new System.Drawing.Size(50, 50);
            this.lblIkona1.TabIndex = 0;
            this.lblIkona1.Text = "💰";
            // 
            // lblNaslov1
            // 
            this.lblNaslov1.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblNaslov1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(120)))), ((int)(((byte)(120)))), ((int)(((byte)(120)))));
            this.lblNaslov1.Location = new System.Drawing.Point(80, 18);
            this.lblNaslov1.Name = "lblNaslov1";
            this.lblNaslov1.Size = new System.Drawing.Size(220, 18);
            this.lblNaslov1.TabIndex = 1;
            this.lblNaslov1.Text = "SKUPNO PRIHODKOV";
            // 
            // lblVrednost1
            // 
            this.lblVrednost1.Font = new System.Drawing.Font("Segoe UI", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblVrednost1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(76)))), ((int)(((byte)(175)))), ((int)(((byte)(80)))));
            this.lblVrednost1.Location = new System.Drawing.Point(80, 38);
            this.lblVrednost1.Name = "lblVrednost1";
            this.lblVrednost1.Size = new System.Drawing.Size(220, 35);
            this.lblVrednost1.TabIndex = 2;
            this.lblVrednost1.Text = "0,00 €";
            // 
            // panelKartica2
            // 
            this.panelKartica2.BackColor = System.Drawing.Color.White;
            this.panelKartica2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panelKartica2.Controls.Add(this.lblVrednost2);
            this.panelKartica2.Controls.Add(this.lblNaslov2);
            this.panelKartica2.Controls.Add(this.lblIkona2);
            this.panelKartica2.Location = new System.Drawing.Point(345, 15);
            this.panelKartica2.Name = "panelKartica2";
            this.panelKartica2.Size = new System.Drawing.Size(320, 90);
            this.panelKartica2.TabIndex = 1;
            // 
            // lblIkona2
            // 
            this.lblIkona2.Font = new System.Drawing.Font("Segoe UI", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblIkona2.Location = new System.Drawing.Point(20, 20);
            this.lblIkona2.Name = "lblIkona2";
            this.lblIkona2.Size = new System.Drawing.Size(50, 50);
            this.lblIkona2.TabIndex = 0;
            this.lblIkona2.Text = "📊";
            // 
            // lblNaslov2
            // 
            this.lblNaslov2.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblNaslov2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(120)))), ((int)(((byte)(120)))), ((int)(((byte)(120)))));
            this.lblNaslov2.Location = new System.Drawing.Point(80, 18);
            this.lblNaslov2.Name = "lblNaslov2";
            this.lblNaslov2.Size = new System.Drawing.Size(220, 18);
            this.lblNaslov2.TabIndex = 1;
            this.lblNaslov2.Text = "ŠTEVILO PLAČIL";
            // 
            // lblVrednost2
            // 
            this.lblVrednost2.Font = new System.Drawing.Font("Segoe UI", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblVrednost2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(33)))), ((int)(((byte)(150)))), ((int)(((byte)(243)))));
            this.lblVrednost2.Location = new System.Drawing.Point(80, 38);
            this.lblVrednost2.Name = "lblVrednost2";
            this.lblVrednost2.Size = new System.Drawing.Size(220, 35);
            this.lblVrednost2.TabIndex = 2;
            this.lblVrednost2.Text = "0";
            // 
            // panelKartica3
            // 
            this.panelKartica3.BackColor = System.Drawing.Color.White;
            this.panelKartica3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panelKartica3.Controls.Add(this.lblVrednost3);
            this.panelKartica3.Controls.Add(this.lblNaslov3);
            this.panelKartica3.Controls.Add(this.lblIkona3);
            this.panelKartica3.Location = new System.Drawing.Point(675, 15);
            this.panelKartica3.Name = "panelKartica3";
            this.panelKartica3.Size = new System.Drawing.Size(320, 90);
            this.panelKartica3.TabIndex = 2;
            // 
            // lblIkona3
            // 
            this.lblIkona3.Font = new System.Drawing.Font("Segoe UI", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblIkona3.Location = new System.Drawing.Point(20, 20);
            this.lblIkona3.Name = "lblIkona3";
            this.lblIkona3.Size = new System.Drawing.Size(50, 50);
            this.lblIkona3.TabIndex = 0;
            this.lblIkona3.Text = "📈";
            // 
            // lblNaslov3
            // 
            this.lblNaslov3.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblNaslov3.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(120)))), ((int)(((byte)(120)))), ((int)(((byte)(120)))));
            this.lblNaslov3.Location = new System.Drawing.Point(80, 18);
            this.lblNaslov3.Name = "lblNaslov3";
            this.lblNaslov3.Size = new System.Drawing.Size(220, 18);
            this.lblNaslov3.TabIndex = 1;
            this.lblNaslov3.Text = "POVPREČNO PLAČILO";
            // 
            // lblVrednost3
            // 
            this.lblVrednost3.Font = new System.Drawing.Font("Segoe UI", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblVrednost3.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(152)))), ((int)(((byte)(0)))));
            this.lblVrednost3.Location = new System.Drawing.Point(80, 38);
            this.lblVrednost3.Name = "lblVrednost3";
            this.lblVrednost3.Size = new System.Drawing.Size(220, 35);
            this.lblVrednost3.TabIndex = 2;
            this.lblVrednost3.Text = "0,00 €";
            // 
            // txtIskanje
            // 
            this.txtIskanje.AnimateReadOnly = false;
            this.txtIskanje.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.txtIskanje.CharacterCasing = System.Windows.Forms.CharacterCasing.Normal;
            this.txtIskanje.Depth = 0;
            this.txtIskanje.Font = new System.Drawing.Font("Roboto", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
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
            // cmbFilterNacin
            // 
            this.cmbFilterNacin.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbFilterNacin.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbFilterNacin.FormattingEnabled = true;
            this.cmbFilterNacin.Location = new System.Drawing.Point(350, 22);
            this.cmbFilterNacin.Name = "cmbFilterNacin";
            this.cmbFilterNacin.Size = new System.Drawing.Size(200, 25);
            this.cmbFilterNacin.TabIndex = 1;
            // 
            // btnNovoPlacilo
            // 
            this.btnNovoPlacilo.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(33)))), ((int)(((byte)(150)))), ((int)(((byte)(243)))));
            this.btnNovoPlacilo.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnNovoPlacilo.FlatAppearance.BorderSize = 0;
            this.btnNovoPlacilo.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnNovoPlacilo.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnNovoPlacilo.ForeColor = System.Drawing.Color.White;
            this.btnNovoPlacilo.Location = new System.Drawing.Point(720, 17);
            this.btnNovoPlacilo.Name = "btnNovoPlacilo";
            this.btnNovoPlacilo.Size = new System.Drawing.Size(180, 38);
            this.btnNovoPlacilo.TabIndex = 2;
            this.btnNovoPlacilo.Text = "NOVO PLAČILO";
            this.btnNovoPlacilo.UseVisualStyleBackColor = false;
            this.btnNovoPlacilo.Click += new System.EventHandler(this.btnNovoPlacilo_Click);
            // 
            // btnOsvezi
            // 
            this.btnOsvezi.BackColor = System.Drawing.Color.WhiteSmoke;
            this.btnOsvezi.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnOsvezi.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(200)))), ((int)(((byte)(200)))), ((int)(((byte)(200)))));
            this.btnOsvezi.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnOsvezi.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnOsvezi.Location = new System.Drawing.Point(910, 17);
            this.btnOsvezi.Name = "btnOsvezi";
            this.btnOsvezi.Size = new System.Drawing.Size(60, 38);
            this.btnOsvezi.TabIndex = 3;
            this.btnOsvezi.Text = "🔄";
            this.btnOsvezi.UseVisualStyleBackColor = false;
            this.btnOsvezi.Click += new System.EventHandler(this.btnOsvezi_Click);
            // 
            // dgvPlacila
            // 
            this.dgvPlacila.AllowUserToAddRows = false;
            this.dgvPlacila.AllowUserToDeleteRows = false;
            this.dgvPlacila.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvPlacila.BackgroundColor = System.Drawing.Color.White;
            this.dgvPlacila.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dgvPlacila.ColumnHeadersHeight = 40;
            this.dgvPlacila.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.dgvPlacila.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvPlacila.Location = new System.Drawing.Point(15, 15);
            this.dgvPlacila.MultiSelect = false;
            this.dgvPlacila.Name = "dgvPlacila";
            this.dgvPlacila.ReadOnly = true;
            this.dgvPlacila.RowHeadersVisible = false;
            this.dgvPlacila.RowTemplate.Height = 35;
            this.dgvPlacila.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvPlacila.Size = new System.Drawing.Size(994, 401);
            this.dgvPlacila.TabIndex = 0;
            //
            // btnIzvoz
            //
            this.btnIzvoz.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(76)))), ((int)(((byte)(175)))), ((int)(((byte)(80)))));
            this.btnIzvoz.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnIzvoz.FlatAppearance.BorderSize = 0;
            this.btnIzvoz.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnIzvoz.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnIzvoz.ForeColor = System.Drawing.Color.White;
            this.btnIzvoz.Location = new System.Drawing.Point(980, 17);
            this.btnIzvoz.Name = "btnIzvoz";
            this.btnIzvoz.Size = new System.Drawing.Size(110, 38);
            this.btnIzvoz.TabIndex = 20;
            this.btnIzvoz.Text = "📊 EXCEL";
            this.btnIzvoz.UseVisualStyleBackColor = false;
            this.btnIzvoz.Click += new System.EventHandler(this.btnIzvoz_Click);
            // 
            // PlacilaObrazec
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.WhiteSmoke;
            this.ClientSize = new System.Drawing.Size(1024, 621);
            this.Controls.Add(this.panelGlavno);
            this.Controls.Add(this.panelOrodja);
            this.Controls.Add(this.panelStatistike);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "PlacilaObrazec";
            this.Text = "Plačila";
            this.panelStatistike.ResumeLayout(false);
            this.panelOrodja.ResumeLayout(false);
            this.panelGlavno.ResumeLayout(false);
            this.panelKartica1.ResumeLayout(false);
            this.panelKartica2.ResumeLayout(false);
            this.panelKartica3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvPlacila)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panelStatistike;
        private System.Windows.Forms.Panel panelOrodja;
        private System.Windows.Forms.Panel panelGlavno;
        private System.Windows.Forms.Panel panelKartica2;
        private System.Windows.Forms.Label lblNaslov2;
        private System.Windows.Forms.Label lblIkona2;
        private System.Windows.Forms.Panel panelKartica1;
        private System.Windows.Forms.Label lblVrednost1;
        private System.Windows.Forms.Label lblNaslov1;
        private System.Windows.Forms.Label lblIkona1;
        private System.Windows.Forms.Panel panelKartica3;
        private System.Windows.Forms.Label lblVrednost2;
        private System.Windows.Forms.Label lblVrednost3;
        private System.Windows.Forms.Label lblNaslov3;
        private System.Windows.Forms.Label lblIkona3;
        private MaterialSkin.Controls.MaterialTextBox2 txtIskanje;
        private System.Windows.Forms.Button btnNovoPlacilo;
        private System.Windows.Forms.ComboBox cmbFilterNacin;
        private System.Windows.Forms.Button btnOsvezi;
        private System.Windows.Forms.DataGridView dgvPlacila;
        private System.Windows.Forms.Button btnIzvoz;
    }
}
namespace CarRental.Obrazci
{
    partial class PrijavaObrazec
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
            this.picLogo = new System.Windows.Forms.PictureBox();
            this.lblNaslov = new MaterialSkin.Controls.MaterialLabel();
            this.lblPodnaslov = new MaterialSkin.Controls.MaterialLabel();
            this.txtEmail = new MaterialSkin.Controls.MaterialTextBox2();
            this.txtGeslo = new MaterialSkin.Controls.MaterialTextBox2();
            this.btnPrijava = new MaterialSkin.Controls.MaterialButton();
            this.btnPreklici = new MaterialSkin.Controls.MaterialButton();
            this.lblVerzija = new MaterialSkin.Controls.MaterialLabel();
            ((System.ComponentModel.ISupportInitialize)(this.picLogo)).BeginInit();
            this.SuspendLayout();
            // 
            // picLogo
            // 
            this.picLogo.Image = global::CarRental.Properties.Resources.Gemini_Generated_Image_7tt8ae7tt8ae7tt8;
            this.picLogo.Location = new System.Drawing.Point(168, 90);
            this.picLogo.Name = "picLogo";
            this.picLogo.Size = new System.Drawing.Size(64, 64);
            this.picLogo.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.picLogo.TabIndex = 0;
            this.picLogo.TabStop = false;
            // 
            // lblNaslov
            // 
            this.lblNaslov.Depth = 0;
            this.lblNaslov.Font = new System.Drawing.Font("Roboto", 24F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Pixel);
            this.lblNaslov.FontType = MaterialSkin.MaterialSkinManager.fontType.H5;
            this.lblNaslov.Location = new System.Drawing.Point(50, 165);
            this.lblNaslov.MouseState = MaterialSkin.MouseState.HOVER;
            this.lblNaslov.Name = "lblNaslov";
            this.lblNaslov.Size = new System.Drawing.Size(300, 30);
            this.lblNaslov.TabIndex = 10;
            this.lblNaslov.Text = "Izposoja avtomobilov";
            this.lblNaslov.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblPodnaslov
            // 
            this.lblPodnaslov.Depth = 0;
            this.lblPodnaslov.Font = new System.Drawing.Font("Roboto", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.lblPodnaslov.FontType = MaterialSkin.MaterialSkinManager.fontType.Body2;
            this.lblPodnaslov.Location = new System.Drawing.Point(50, 200);
            this.lblPodnaslov.MouseState = MaterialSkin.MouseState.HOVER;
            this.lblPodnaslov.Name = "lblPodnaslov";
            this.lblPodnaslov.Size = new System.Drawing.Size(300, 20);
            this.lblPodnaslov.TabIndex = 11;
            this.lblPodnaslov.Text = "Prijavite se v sistem";
            this.lblPodnaslov.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // txtEmail
            // 
            this.txtEmail.AnimateReadOnly = false;
            this.txtEmail.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.txtEmail.CharacterCasing = System.Windows.Forms.CharacterCasing.Normal;
            this.txtEmail.Depth = 0;
            this.txtEmail.Font = new System.Drawing.Font("Roboto", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.txtEmail.HideSelection = true;
            this.txtEmail.Hint = "Email";
            this.txtEmail.LeadingIcon = null;
            this.txtEmail.Location = new System.Drawing.Point(50, 245);
            this.txtEmail.MaxLength = 32767;
            this.txtEmail.MouseState = MaterialSkin.MouseState.OUT;
            this.txtEmail.Name = "txtEmail";
            this.txtEmail.PasswordChar = '\0';
            this.txtEmail.PrefixSuffixText = null;
            this.txtEmail.ReadOnly = false;
            this.txtEmail.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.txtEmail.SelectedText = "";
            this.txtEmail.SelectionLength = 0;
            this.txtEmail.SelectionStart = 0;
            this.txtEmail.ShortcutsEnabled = true;
            this.txtEmail.Size = new System.Drawing.Size(300, 48);
            this.txtEmail.TabIndex = 12;
            this.txtEmail.TabStop = false;
            this.txtEmail.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.txtEmail.TrailingIcon = null;
            this.txtEmail.UseSystemPasswordChar = false;
            // 
            // txtGeslo
            // 
            this.txtGeslo.AnimateReadOnly = false;
            this.txtGeslo.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.txtGeslo.CharacterCasing = System.Windows.Forms.CharacterCasing.Normal;
            this.txtGeslo.Depth = 0;
            this.txtGeslo.Font = new System.Drawing.Font("Roboto", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.txtGeslo.HideSelection = true;
            this.txtGeslo.Hint = "Geslo";
            this.txtGeslo.LeadingIcon = null;
            this.txtGeslo.Location = new System.Drawing.Point(50, 305);
            this.txtGeslo.MaxLength = 32767;
            this.txtGeslo.MouseState = MaterialSkin.MouseState.OUT;
            this.txtGeslo.Name = "txtGeslo";
            this.txtGeslo.PasswordChar = '*';
            this.txtGeslo.PrefixSuffixText = null;
            this.txtGeslo.ReadOnly = false;
            this.txtGeslo.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.txtGeslo.SelectedText = "";
            this.txtGeslo.SelectionLength = 0;
            this.txtGeslo.SelectionStart = 0;
            this.txtGeslo.ShortcutsEnabled = true;
            this.txtGeslo.Size = new System.Drawing.Size(300, 48);
            this.txtGeslo.TabIndex = 13;
            this.txtGeslo.TabStop = false;
            this.txtGeslo.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.txtGeslo.TrailingIcon = null;
            this.txtGeslo.UseSystemPasswordChar = false;
            // 
            // btnPrijava
            // 
            this.btnPrijava.AutoSize = false;
            this.btnPrijava.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.btnPrijava.Density = MaterialSkin.Controls.MaterialButton.MaterialButtonDensity.Default;
            this.btnPrijava.Depth = 0;
            this.btnPrijava.HighEmphasis = true;
            this.btnPrijava.Icon = null;
            this.btnPrijava.Location = new System.Drawing.Point(50, 375);
            this.btnPrijava.Margin = new System.Windows.Forms.Padding(4, 6, 4, 6);
            this.btnPrijava.MouseState = MaterialSkin.MouseState.HOVER;
            this.btnPrijava.Name = "btnPrijava";
            this.btnPrijava.NoAccentTextColor = System.Drawing.Color.Empty;
            this.btnPrijava.Size = new System.Drawing.Size(300, 40);
            this.btnPrijava.TabIndex = 14;
            this.btnPrijava.Text = "PRIJAVA";
            this.btnPrijava.Type = MaterialSkin.Controls.MaterialButton.MaterialButtonType.Contained;
            this.btnPrijava.UseAccentColor = false;
            this.btnPrijava.UseVisualStyleBackColor = true;
            this.btnPrijava.Click += new System.EventHandler(this.btnPrijava_Click);
            // 
            // btnPreklici
            // 
            this.btnPreklici.AutoSize = false;
            this.btnPreklici.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.btnPreklici.Density = MaterialSkin.Controls.MaterialButton.MaterialButtonDensity.Default;
            this.btnPreklici.Depth = 0;
            this.btnPreklici.HighEmphasis = true;
            this.btnPreklici.Icon = null;
            this.btnPreklici.Location = new System.Drawing.Point(50, 425);
            this.btnPreklici.Margin = new System.Windows.Forms.Padding(4, 6, 4, 6);
            this.btnPreklici.MouseState = MaterialSkin.MouseState.HOVER;
            this.btnPreklici.Name = "btnPreklici";
            this.btnPreklici.NoAccentTextColor = System.Drawing.Color.Empty;
            this.btnPreklici.Size = new System.Drawing.Size(300, 40);
            this.btnPreklici.TabIndex = 15;
            this.btnPreklici.Text = "PREKLIČI";
            this.btnPreklici.Type = MaterialSkin.Controls.MaterialButton.MaterialButtonType.Outlined;
            this.btnPreklici.UseAccentColor = false;
            this.btnPreklici.UseVisualStyleBackColor = true;
            this.btnPreklici.Click += new System.EventHandler(this.btnPreklici_Click);
            // 
            // lblVerzija
            // 
            this.lblVerzija.Depth = 0;
            this.lblVerzija.Font = new System.Drawing.Font("Roboto", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.lblVerzija.Location = new System.Drawing.Point(170, 481);
            this.lblVerzija.MouseState = MaterialSkin.MouseState.HOVER;
            this.lblVerzija.Name = "lblVerzija";
            this.lblVerzija.Size = new System.Drawing.Size(60, 15);
            this.lblVerzija.TabIndex = 16;
            this.lblVerzija.Text = "v1.0.1";
            this.lblVerzija.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // PrijavaObrazec
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(400, 500);
            this.Controls.Add(this.lblVerzija);
            this.Controls.Add(this.btnPreklici);
            this.Controls.Add(this.btnPrijava);
            this.Controls.Add(this.txtGeslo);
            this.Controls.Add(this.txtEmail);
            this.Controls.Add(this.lblPodnaslov);
            this.Controls.Add(this.lblNaslov);
            this.Controls.Add(this.picLogo);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "PrijavaObrazec";
            this.Sizable = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Prijava";
            ((System.ComponentModel.ISupportInitialize)(this.picLogo)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox picLogo;
        private MaterialSkin.Controls.MaterialLabel lblNaslov;
        private MaterialSkin.Controls.MaterialLabel lblPodnaslov;
        private MaterialSkin.Controls.MaterialTextBox2 txtEmail;
        private MaterialSkin.Controls.MaterialTextBox2 txtGeslo;
        private MaterialSkin.Controls.MaterialButton btnPrijava;
        private MaterialSkin.Controls.MaterialButton btnPreklici;
        private MaterialSkin.Controls.MaterialLabel lblVerzija;
    }
}
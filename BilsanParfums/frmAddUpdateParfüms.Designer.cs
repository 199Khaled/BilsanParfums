namespace BilsanParfums
{
    partial class frmAddUpdateParfüms
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
            this.components = new System.ComponentModel.Container();
            this.btnBearbeiten = new System.Windows.Forms.Button();
            this.chbInBestellung = new System.Windows.Forms.CheckBox();
            this.chbIstVorhanden = new System.Windows.Forms.CheckBox();
            this.btnAbbrechen = new System.Windows.Forms.Button();
            this.btnspeichern = new System.Windows.Forms.Button();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.txtParfümNummer = new Guna.UI2.WinForms.Guna2TextBox();
            this.txtMarke = new Guna.UI2.WinForms.Guna2TextBox();
            this.txtName = new Guna.UI2.WinForms.Guna2TextBox();
            this.txtKategorie = new Guna.UI2.WinForms.Guna2TextBox();
            this.txtDuftrichtung = new Guna.UI2.WinForms.Guna2TextBox();
            this.txtBasisnote = new Guna.UI2.WinForms.Guna2TextBox();
            this.errorProvider1 = new System.Windows.Forms.ErrorProvider(this.components);
            this.panel1 = new System.Windows.Forms.Panel();
            this.label7 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).BeginInit();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnBearbeiten
            // 
            this.btnBearbeiten.BackColor = System.Drawing.Color.Teal;
            this.btnBearbeiten.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnBearbeiten.ForeColor = System.Drawing.Color.White;
            this.btnBearbeiten.Location = new System.Drawing.Point(48, 387);
            this.btnBearbeiten.Margin = new System.Windows.Forms.Padding(2);
            this.btnBearbeiten.Name = "btnBearbeiten";
            this.btnBearbeiten.Size = new System.Drawing.Size(111, 45);
            this.btnBearbeiten.TabIndex = 34;
            this.btnBearbeiten.Text = "Korregieren";
            this.btnBearbeiten.UseVisualStyleBackColor = false;
            this.btnBearbeiten.Click += new System.EventHandler(this.btnBearbeiten_Click);
            // 
            // chbInBestellung
            // 
            this.chbInBestellung.AutoSize = true;
            this.chbInBestellung.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chbInBestellung.Location = new System.Drawing.Point(338, 452);
            this.chbInBestellung.Margin = new System.Windows.Forms.Padding(2);
            this.chbInBestellung.Name = "chbInBestellung";
            this.chbInBestellung.Size = new System.Drawing.Size(134, 24);
            this.chbInBestellung.TabIndex = 33;
            this.chbInBestellung.Text = "In Bestellung";
            this.chbInBestellung.UseVisualStyleBackColor = true;
            // 
            // chbIstVorhanden
            // 
            this.chbIstVorhanden.AutoSize = true;
            this.chbIstVorhanden.Checked = true;
            this.chbIstVorhanden.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chbIstVorhanden.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chbIstVorhanden.Location = new System.Drawing.Point(169, 452);
            this.chbIstVorhanden.Margin = new System.Windows.Forms.Padding(2);
            this.chbIstVorhanden.Name = "chbIstVorhanden";
            this.chbIstVorhanden.Size = new System.Drawing.Size(138, 24);
            this.chbIstVorhanden.TabIndex = 32;
            this.chbIstVorhanden.Text = "Ist vorhanden";
            this.chbIstVorhanden.UseVisualStyleBackColor = true;
            // 
            // btnAbbrechen
            // 
            this.btnAbbrechen.BackColor = System.Drawing.Color.DarkSlateGray;
            this.btnAbbrechen.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAbbrechen.ForeColor = System.Drawing.Color.White;
            this.btnAbbrechen.Location = new System.Drawing.Point(344, 500);
            this.btnAbbrechen.Margin = new System.Windows.Forms.Padding(2);
            this.btnAbbrechen.Name = "btnAbbrechen";
            this.btnAbbrechen.Size = new System.Drawing.Size(128, 41);
            this.btnAbbrechen.TabIndex = 31;
            this.btnAbbrechen.Text = "Abbrechen";
            this.btnAbbrechen.UseVisualStyleBackColor = false;
            this.btnAbbrechen.Click += new System.EventHandler(this.btnAbbrechen_Click);
            // 
            // btnspeichern
            // 
            this.btnspeichern.BackColor = System.Drawing.Color.DarkSlateGray;
            this.btnspeichern.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnspeichern.ForeColor = System.Drawing.Color.White;
            this.btnspeichern.Location = new System.Drawing.Point(179, 500);
            this.btnspeichern.Margin = new System.Windows.Forms.Padding(2);
            this.btnspeichern.Name = "btnspeichern";
            this.btnspeichern.Size = new System.Drawing.Size(128, 41);
            this.btnspeichern.TabIndex = 30;
            this.btnspeichern.Text = "Speichern";
            this.btnspeichern.UseVisualStyleBackColor = false;
            this.btnspeichern.Click += new System.EventHandler(this.btnspeichern_Click);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(55, 361);
            this.label6.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(96, 24);
            this.label6.TabIndex = 23;
            this.label6.Text = "Basisnote:";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(41, 306);
            this.label5.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(115, 24);
            this.label5.TabIndex = 22;
            this.label5.Text = "Duftrichtung:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(56, 251);
            this.label4.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(102, 24);
            this.label4.TabIndex = 21;
            this.label4.Text = "Kategorie:*";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(86, 195);
            this.label3.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(73, 24);
            this.label3.TabIndex = 20;
            this.label3.Text = "Name:*";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(86, 140);
            this.label2.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(74, 24);
            this.label2.TabIndex = 19;
            this.label2.Text = "Marke:*";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(11, 79);
            this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(155, 24);
            this.label1.TabIndex = 18;
            this.label1.Text = "ParfümNummer:*";
            // 
            // txtParfümNummer
            // 
            this.txtParfümNummer.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.txtParfümNummer.DefaultText = "";
            this.txtParfümNummer.DisabledState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(208)))), ((int)(((byte)(208)))));
            this.txtParfümNummer.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(226)))), ((int)(((byte)(226)))));
            this.txtParfümNummer.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.txtParfümNummer.DisabledState.Parent = this.txtParfümNummer;
            this.txtParfümNummer.DisabledState.PlaceholderForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.txtParfümNummer.FocusedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.txtParfümNummer.FocusedState.Parent = this.txtParfümNummer;
            this.txtParfümNummer.Font = new System.Drawing.Font("Segoe UI", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtParfümNummer.ForeColor = System.Drawing.Color.Black;
            this.txtParfümNummer.HoverState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.txtParfümNummer.HoverState.Parent = this.txtParfümNummer;
            this.txtParfümNummer.Location = new System.Drawing.Point(179, 69);
            this.txtParfümNummer.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.txtParfümNummer.Name = "txtParfümNummer";
            this.txtParfümNummer.PasswordChar = '\0';
            this.txtParfümNummer.PlaceholderText = "";
            this.txtParfümNummer.SelectedText = "";
            this.txtParfümNummer.ShadowDecoration.Parent = this.txtParfümNummer;
            this.txtParfümNummer.Size = new System.Drawing.Size(293, 44);
            this.txtParfümNummer.TabIndex = 35;
            // 
            // txtMarke
            // 
            this.txtMarke.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.txtMarke.DefaultText = "";
            this.txtMarke.DisabledState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(208)))), ((int)(((byte)(208)))));
            this.txtMarke.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(226)))), ((int)(((byte)(226)))));
            this.txtMarke.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.txtMarke.DisabledState.Parent = this.txtMarke;
            this.txtMarke.DisabledState.PlaceholderForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.txtMarke.FocusedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.txtMarke.FocusedState.Parent = this.txtMarke;
            this.txtMarke.Font = new System.Drawing.Font("Segoe UI", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtMarke.ForeColor = System.Drawing.Color.Black;
            this.txtMarke.HoverState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.txtMarke.HoverState.Parent = this.txtMarke;
            this.txtMarke.Location = new System.Drawing.Point(179, 129);
            this.txtMarke.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.txtMarke.Name = "txtMarke";
            this.txtMarke.PasswordChar = '\0';
            this.txtMarke.PlaceholderText = "";
            this.txtMarke.SelectedText = "";
            this.txtMarke.ShadowDecoration.Parent = this.txtMarke;
            this.txtMarke.Size = new System.Drawing.Size(293, 44);
            this.txtMarke.TabIndex = 36;
            // 
            // txtName
            // 
            this.txtName.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.txtName.DefaultText = "";
            this.txtName.DisabledState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(208)))), ((int)(((byte)(208)))));
            this.txtName.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(226)))), ((int)(((byte)(226)))));
            this.txtName.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.txtName.DisabledState.Parent = this.txtName;
            this.txtName.DisabledState.PlaceholderForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.txtName.FocusedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.txtName.FocusedState.Parent = this.txtName;
            this.txtName.Font = new System.Drawing.Font("Segoe UI", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtName.ForeColor = System.Drawing.Color.Black;
            this.txtName.HoverState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.txtName.HoverState.Parent = this.txtName;
            this.txtName.Location = new System.Drawing.Point(179, 186);
            this.txtName.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.txtName.Name = "txtName";
            this.txtName.PasswordChar = '\0';
            this.txtName.PlaceholderText = "";
            this.txtName.SelectedText = "";
            this.txtName.ShadowDecoration.Parent = this.txtName;
            this.txtName.Size = new System.Drawing.Size(293, 44);
            this.txtName.TabIndex = 37;
            // 
            // txtKategorie
            // 
            this.txtKategorie.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.txtKategorie.DefaultText = "";
            this.txtKategorie.DisabledState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(208)))), ((int)(((byte)(208)))));
            this.txtKategorie.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(226)))), ((int)(((byte)(226)))));
            this.txtKategorie.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.txtKategorie.DisabledState.Parent = this.txtKategorie;
            this.txtKategorie.DisabledState.PlaceholderForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.txtKategorie.FocusedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.txtKategorie.FocusedState.Parent = this.txtKategorie;
            this.txtKategorie.Font = new System.Drawing.Font("Segoe UI", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtKategorie.ForeColor = System.Drawing.Color.Black;
            this.txtKategorie.HoverState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.txtKategorie.HoverState.Parent = this.txtKategorie;
            this.txtKategorie.Location = new System.Drawing.Point(179, 240);
            this.txtKategorie.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.txtKategorie.Name = "txtKategorie";
            this.txtKategorie.PasswordChar = '\0';
            this.txtKategorie.PlaceholderText = "";
            this.txtKategorie.SelectedText = "";
            this.txtKategorie.ShadowDecoration.Parent = this.txtKategorie;
            this.txtKategorie.Size = new System.Drawing.Size(293, 44);
            this.txtKategorie.TabIndex = 38;
            // 
            // txtDuftrichtung
            // 
            this.txtDuftrichtung.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.txtDuftrichtung.DefaultText = "";
            this.txtDuftrichtung.DisabledState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(208)))), ((int)(((byte)(208)))));
            this.txtDuftrichtung.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(226)))), ((int)(((byte)(226)))));
            this.txtDuftrichtung.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.txtDuftrichtung.DisabledState.Parent = this.txtDuftrichtung;
            this.txtDuftrichtung.DisabledState.PlaceholderForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.txtDuftrichtung.FocusedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.txtDuftrichtung.FocusedState.Parent = this.txtDuftrichtung;
            this.txtDuftrichtung.Font = new System.Drawing.Font("Segoe UI", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtDuftrichtung.ForeColor = System.Drawing.Color.Black;
            this.txtDuftrichtung.HoverState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.txtDuftrichtung.HoverState.Parent = this.txtDuftrichtung;
            this.txtDuftrichtung.Location = new System.Drawing.Point(179, 297);
            this.txtDuftrichtung.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.txtDuftrichtung.Name = "txtDuftrichtung";
            this.txtDuftrichtung.PasswordChar = '\0';
            this.txtDuftrichtung.PlaceholderText = "";
            this.txtDuftrichtung.SelectedText = "";
            this.txtDuftrichtung.ShadowDecoration.Parent = this.txtDuftrichtung;
            this.txtDuftrichtung.Size = new System.Drawing.Size(293, 44);
            this.txtDuftrichtung.TabIndex = 39;
            // 
            // txtBasisnote
            // 
            this.txtBasisnote.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.txtBasisnote.DefaultText = "";
            this.txtBasisnote.DisabledState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(208)))), ((int)(((byte)(208)))));
            this.txtBasisnote.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(226)))), ((int)(((byte)(226)))));
            this.txtBasisnote.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.txtBasisnote.DisabledState.Parent = this.txtBasisnote;
            this.txtBasisnote.DisabledState.PlaceholderForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.txtBasisnote.FocusedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.txtBasisnote.FocusedState.Parent = this.txtBasisnote;
            this.txtBasisnote.Font = new System.Drawing.Font("Segoe UI", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtBasisnote.ForeColor = System.Drawing.Color.Black;
            this.txtBasisnote.HoverState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.txtBasisnote.HoverState.Parent = this.txtBasisnote;
            this.txtBasisnote.Location = new System.Drawing.Point(179, 354);
            this.txtBasisnote.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.txtBasisnote.Name = "txtBasisnote";
            this.txtBasisnote.PasswordChar = '\0';
            this.txtBasisnote.PlaceholderText = "";
            this.txtBasisnote.SelectedText = "";
            this.txtBasisnote.ShadowDecoration.Parent = this.txtBasisnote;
            this.txtBasisnote.Size = new System.Drawing.Size(293, 75);
            this.txtBasisnote.TabIndex = 40;
            // 
            // errorProvider1
            // 
            this.errorProvider1.ContainerControl = this;
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.DarkSlateGray;
            this.panel1.Controls.Add(this.label7);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Margin = new System.Windows.Forms.Padding(2);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(501, 46);
            this.panel1.TabIndex = 41;
            // 
            // label7
            // 
            this.label7.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.ForeColor = System.Drawing.Color.White;
            this.label7.Location = new System.Drawing.Point(156, 10);
            this.label7.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(191, 24);
            this.label7.TabIndex = 0;
            this.label7.Text = "Add/Update Parfüm";
            // 
            // frmAddUpdateParfüms
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.LightGray;
            this.ClientSize = new System.Drawing.Size(501, 552);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.txtBasisnote);
            this.Controls.Add(this.txtDuftrichtung);
            this.Controls.Add(this.txtKategorie);
            this.Controls.Add(this.txtName);
            this.Controls.Add(this.txtMarke);
            this.Controls.Add(this.txtParfümNummer);
            this.Controls.Add(this.btnBearbeiten);
            this.Controls.Add(this.chbInBestellung);
            this.Controls.Add(this.chbIstVorhanden);
            this.Controls.Add(this.btnAbbrechen);
            this.Controls.Add(this.btnspeichern);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmAddUpdateParfüms";
            this.Text = "frmAddUpdateParfüms";
            this.Load += new System.EventHandler(this.frmAddUpdateParfüms_Load);
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnBearbeiten;
        private System.Windows.Forms.CheckBox chbInBestellung;
        private System.Windows.Forms.CheckBox chbIstVorhanden;
        private System.Windows.Forms.Button btnAbbrechen;
        private System.Windows.Forms.Button btnspeichern;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private Guna.UI2.WinForms.Guna2TextBox txtParfümNummer;
        private Guna.UI2.WinForms.Guna2TextBox txtMarke;
        private Guna.UI2.WinForms.Guna2TextBox txtName;
        private Guna.UI2.WinForms.Guna2TextBox txtKategorie;
        private Guna.UI2.WinForms.Guna2TextBox txtDuftrichtung;
        private Guna.UI2.WinForms.Guna2TextBox txtBasisnote;
        private System.Windows.Forms.ErrorProvider errorProvider1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label7;
    }
}
namespace BilsanParfums
{
    partial class frmFlakons
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
            this.cbAlleParfümStatus = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.gbFlakonsInfo = new System.Windows.Forms.GroupBox();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.txtFlakonsProkarton = new Guna.UI2.WinForms.Guna2TextBox();
            this.txtKarfonLager = new Guna.UI2.WinForms.Guna2TextBox();
            this.txtVerbleibendeMenge = new Guna.UI2.WinForms.Guna2TextBox();
            this.cbFlakonsMengeInMl = new Guna.UI2.WinForms.Guna2ComboBox();
            this.cbFarbe = new Guna.UI2.WinForms.Guna2ComboBox();
            this.cbForm = new Guna.UI2.WinForms.Guna2ComboBox();
            this.btnSave = new Guna.UI2.WinForms.Guna2Button();
            this.btnCancel = new Guna.UI2.WinForms.Guna2Button();
            this.btnDelete = new Guna.UI2.WinForms.Guna2Button();
            this.dgvFlakons = new System.Windows.Forms.DataGridView();
            this.label8 = new System.Windows.Forms.Label();
            this.cbVerschlussart = new Guna.UI2.WinForms.Guna2ComboBox();
            this.cbAlleParfümStatus.SuspendLayout();
            this.gbFlakonsInfo.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvFlakons)).BeginInit();
            this.SuspendLayout();
            // 
            // cbAlleParfümStatus
            // 
            this.cbAlleParfümStatus.BackColor = System.Drawing.Color.DarkCyan;
            this.cbAlleParfümStatus.Controls.Add(this.label1);
            this.cbAlleParfümStatus.Dock = System.Windows.Forms.DockStyle.Top;
            this.cbAlleParfümStatus.Location = new System.Drawing.Point(0, 0);
            this.cbAlleParfümStatus.Name = "cbAlleParfümStatus";
            this.cbAlleParfümStatus.Size = new System.Drawing.Size(1182, 51);
            this.cbAlleParfümStatus.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.Gold;
            this.label1.Location = new System.Drawing.Point(546, 14);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(154, 25);
            this.label1.TabIndex = 0;
            this.label1.Text = "Flakonsdaten";
            // 
            // gbFlakonsInfo
            // 
            this.gbFlakonsInfo.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.gbFlakonsInfo.BackColor = System.Drawing.Color.Gainsboro;
            this.gbFlakonsInfo.Controls.Add(this.cbVerschlussart);
            this.gbFlakonsInfo.Controls.Add(this.label8);
            this.gbFlakonsInfo.Controls.Add(this.btnDelete);
            this.gbFlakonsInfo.Controls.Add(this.btnCancel);
            this.gbFlakonsInfo.Controls.Add(this.btnSave);
            this.gbFlakonsInfo.Controls.Add(this.cbForm);
            this.gbFlakonsInfo.Controls.Add(this.cbFarbe);
            this.gbFlakonsInfo.Controls.Add(this.cbFlakonsMengeInMl);
            this.gbFlakonsInfo.Controls.Add(this.txtVerbleibendeMenge);
            this.gbFlakonsInfo.Controls.Add(this.txtKarfonLager);
            this.gbFlakonsInfo.Controls.Add(this.txtFlakonsProkarton);
            this.gbFlakonsInfo.Controls.Add(this.label7);
            this.gbFlakonsInfo.Controls.Add(this.label6);
            this.gbFlakonsInfo.Controls.Add(this.label5);
            this.gbFlakonsInfo.Controls.Add(this.label4);
            this.gbFlakonsInfo.Controls.Add(this.label3);
            this.gbFlakonsInfo.Controls.Add(this.label2);
            this.gbFlakonsInfo.Controls.Add(this.pictureBox1);
            this.gbFlakonsInfo.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gbFlakonsInfo.Location = new System.Drawing.Point(0, 57);
            this.gbFlakonsInfo.Name = "gbFlakonsInfo";
            this.gbFlakonsInfo.Size = new System.Drawing.Size(1182, 320);
            this.gbFlakonsInfo.TabIndex = 2;
            this.gbFlakonsInfo.TabStop = false;
            this.gbFlakonsInfo.Text = "Flakonsinfo";
            // 
            // pictureBox1
            // 
            this.pictureBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.pictureBox1.Image = global::BilsanParfums.Properties.Resources.Flakons;
            this.pictureBox1.Location = new System.Drawing.Point(823, 0);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(353, 286);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(547, 48);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(50, 20);
            this.label2.TabIndex = 1;
            this.label2.Text = "Form:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(542, 101);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(55, 20);
            this.label3.TabIndex = 2;
            this.label3.Text = "Farbe:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(42, 48);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(148, 20);
            this.label4.TabIndex = 3;
            this.label4.Text = "FlakonsMengeInMl:";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(440, 151);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(157, 20);
            this.label5.TabIndex = 4;
            this.label5.Text = "Flakons_pro_Karton:";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(70, 151);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(118, 20);
            this.label6.TabIndex = 5;
            this.label6.Text = "Kartons_Lager:";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(16, 197);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(172, 20);
            this.label7.TabIndex = 6;
            this.label7.Text = "Verbleibende_Flakons:";
            // 
            // txtFlakonsProkarton
            // 
            this.txtFlakonsProkarton.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.txtFlakonsProkarton.DefaultText = "";
            this.txtFlakonsProkarton.DisabledState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(208)))), ((int)(((byte)(208)))));
            this.txtFlakonsProkarton.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(226)))), ((int)(((byte)(226)))));
            this.txtFlakonsProkarton.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.txtFlakonsProkarton.DisabledState.Parent = this.txtFlakonsProkarton;
            this.txtFlakonsProkarton.DisabledState.PlaceholderForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.txtFlakonsProkarton.FocusedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.txtFlakonsProkarton.FocusedState.Parent = this.txtFlakonsProkarton;
            this.txtFlakonsProkarton.HoverState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.txtFlakonsProkarton.HoverState.Parent = this.txtFlakonsProkarton;
            this.txtFlakonsProkarton.Location = new System.Drawing.Point(613, 141);
            this.txtFlakonsProkarton.Margin = new System.Windows.Forms.Padding(9, 12, 9, 12);
            this.txtFlakonsProkarton.Name = "txtFlakonsProkarton";
            this.txtFlakonsProkarton.PasswordChar = '\0';
            this.txtFlakonsProkarton.PlaceholderText = "";
            this.txtFlakonsProkarton.SelectedText = "";
            this.txtFlakonsProkarton.ShadowDecoration.Parent = this.txtFlakonsProkarton;
            this.txtFlakonsProkarton.Size = new System.Drawing.Size(188, 36);
            this.txtFlakonsProkarton.TabIndex = 11;
            this.txtFlakonsProkarton.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtFlakonsProkarton_KeyPress);
            // 
            // txtKarfonLager
            // 
            this.txtKarfonLager.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.txtKarfonLager.DefaultText = "";
            this.txtKarfonLager.DisabledState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(208)))), ((int)(((byte)(208)))));
            this.txtKarfonLager.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(226)))), ((int)(((byte)(226)))));
            this.txtKarfonLager.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.txtKarfonLager.DisabledState.Parent = this.txtKarfonLager;
            this.txtKarfonLager.DisabledState.PlaceholderForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.txtKarfonLager.FocusedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.txtKarfonLager.FocusedState.Parent = this.txtKarfonLager;
            this.txtKarfonLager.HoverState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.txtKarfonLager.HoverState.Parent = this.txtKarfonLager;
            this.txtKarfonLager.Location = new System.Drawing.Point(199, 141);
            this.txtKarfonLager.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.txtKarfonLager.Name = "txtKarfonLager";
            this.txtKarfonLager.PasswordChar = '\0';
            this.txtKarfonLager.PlaceholderText = "";
            this.txtKarfonLager.SelectedText = "";
            this.txtKarfonLager.ShadowDecoration.Parent = this.txtKarfonLager;
            this.txtKarfonLager.Size = new System.Drawing.Size(188, 36);
            this.txtKarfonLager.TabIndex = 12;
            this.txtKarfonLager.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtKarfonLager_KeyPress);
            // 
            // txtVerbleibendeMenge
            // 
            this.txtVerbleibendeMenge.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.txtVerbleibendeMenge.DefaultText = "";
            this.txtVerbleibendeMenge.DisabledState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(208)))), ((int)(((byte)(208)))));
            this.txtVerbleibendeMenge.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(226)))), ((int)(((byte)(226)))));
            this.txtVerbleibendeMenge.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.txtVerbleibendeMenge.DisabledState.Parent = this.txtVerbleibendeMenge;
            this.txtVerbleibendeMenge.DisabledState.PlaceholderForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.txtVerbleibendeMenge.FocusedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.txtVerbleibendeMenge.FocusedState.Parent = this.txtVerbleibendeMenge;
            this.txtVerbleibendeMenge.HoverState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.txtVerbleibendeMenge.HoverState.Parent = this.txtVerbleibendeMenge;
            this.txtVerbleibendeMenge.Location = new System.Drawing.Point(199, 190);
            this.txtVerbleibendeMenge.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.txtVerbleibendeMenge.Name = "txtVerbleibendeMenge";
            this.txtVerbleibendeMenge.PasswordChar = '\0';
            this.txtVerbleibendeMenge.PlaceholderText = "";
            this.txtVerbleibendeMenge.SelectedText = "";
            this.txtVerbleibendeMenge.ShadowDecoration.Parent = this.txtVerbleibendeMenge;
            this.txtVerbleibendeMenge.Size = new System.Drawing.Size(188, 36);
            this.txtVerbleibendeMenge.TabIndex = 13;
            // 
            // cbFlakonsMengeInMl
            // 
            this.cbFlakonsMengeInMl.BackColor = System.Drawing.Color.Transparent;
            this.cbFlakonsMengeInMl.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cbFlakonsMengeInMl.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbFlakonsMengeInMl.FocusedColor = System.Drawing.Color.Empty;
            this.cbFlakonsMengeInMl.FocusedState.Parent = this.cbFlakonsMengeInMl;
            this.cbFlakonsMengeInMl.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbFlakonsMengeInMl.ForeColor = System.Drawing.Color.Black;
            this.cbFlakonsMengeInMl.FormattingEnabled = true;
            this.cbFlakonsMengeInMl.HoverState.Parent = this.cbFlakonsMengeInMl;
            this.cbFlakonsMengeInMl.ItemHeight = 30;
            this.cbFlakonsMengeInMl.Items.AddRange(new object[] {
            "30ml",
            "50ml",
            "100ml"});
            this.cbFlakonsMengeInMl.ItemsAppearance.Parent = this.cbFlakonsMengeInMl;
            this.cbFlakonsMengeInMl.Location = new System.Drawing.Point(199, 43);
            this.cbFlakonsMengeInMl.Name = "cbFlakonsMengeInMl";
            this.cbFlakonsMengeInMl.ShadowDecoration.Parent = this.cbFlakonsMengeInMl;
            this.cbFlakonsMengeInMl.Size = new System.Drawing.Size(188, 36);
            this.cbFlakonsMengeInMl.TabIndex = 14;
            // 
            // cbFarbe
            // 
            this.cbFarbe.BackColor = System.Drawing.Color.Transparent;
            this.cbFarbe.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cbFarbe.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbFarbe.FocusedColor = System.Drawing.Color.Empty;
            this.cbFarbe.FocusedState.Parent = this.cbFarbe;
            this.cbFarbe.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbFarbe.ForeColor = System.Drawing.Color.Black;
            this.cbFarbe.FormattingEnabled = true;
            this.cbFarbe.HoverState.Parent = this.cbFarbe;
            this.cbFarbe.ItemHeight = 30;
            this.cbFarbe.Items.AddRange(new object[] {
            "Transparent",
            "Blau transparent",
            "Grün transparent",
            "Pink transparent"});
            this.cbFarbe.ItemsAppearance.Parent = this.cbFarbe;
            this.cbFarbe.Location = new System.Drawing.Point(613, 92);
            this.cbFarbe.Name = "cbFarbe";
            this.cbFarbe.ShadowDecoration.Parent = this.cbFarbe;
            this.cbFarbe.Size = new System.Drawing.Size(188, 36);
            this.cbFarbe.TabIndex = 15;
            // 
            // cbForm
            // 
            this.cbForm.BackColor = System.Drawing.Color.Transparent;
            this.cbForm.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cbForm.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbForm.FocusedColor = System.Drawing.Color.Empty;
            this.cbForm.FocusedState.Parent = this.cbForm;
            this.cbForm.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbForm.ForeColor = System.Drawing.Color.Black;
            this.cbForm.FormattingEnabled = true;
            this.cbForm.HoverState.Parent = this.cbForm;
            this.cbForm.ItemHeight = 30;
            this.cbForm.Items.AddRange(new object[] {
            "Rundflaschen",
            "Eckige Flaschen"});
            this.cbForm.ItemsAppearance.Parent = this.cbForm;
            this.cbForm.Location = new System.Drawing.Point(613, 43);
            this.cbForm.Name = "cbForm";
            this.cbForm.ShadowDecoration.Parent = this.cbForm;
            this.cbForm.Size = new System.Drawing.Size(188, 36);
            this.cbForm.TabIndex = 16;
            // 
            // btnSave
            // 
            this.btnSave.CheckedState.Parent = this.btnSave;
            this.btnSave.CustomImages.Parent = this.btnSave;
            this.btnSave.FillColor = System.Drawing.Color.DarkCyan;
            this.btnSave.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSave.ForeColor = System.Drawing.Color.Gold;
            this.btnSave.HoverState.Parent = this.btnSave;
            this.btnSave.Location = new System.Drawing.Point(207, 255);
            this.btnSave.Name = "btnSave";
            this.btnSave.ShadowDecoration.Parent = this.btnSave;
            this.btnSave.Size = new System.Drawing.Size(120, 45);
            this.btnSave.TabIndex = 17;
            this.btnSave.Text = "Speichern";
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.CheckedState.Parent = this.btnCancel;
            this.btnCancel.CustomImages.Parent = this.btnCancel;
            this.btnCancel.FillColor = System.Drawing.Color.DarkCyan;
            this.btnCancel.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCancel.ForeColor = System.Drawing.Color.Gold;
            this.btnCancel.HoverState.Parent = this.btnCancel;
            this.btnCancel.Location = new System.Drawing.Point(347, 255);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.ShadowDecoration.Parent = this.btnCancel;
            this.btnCancel.Size = new System.Drawing.Size(120, 45);
            this.btnCancel.TabIndex = 18;
            this.btnCancel.Text = "Abbrechen";
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnDelete
            // 
            this.btnDelete.CheckedState.Parent = this.btnDelete;
            this.btnDelete.CustomImages.Parent = this.btnDelete;
            this.btnDelete.FillColor = System.Drawing.Color.DarkCyan;
            this.btnDelete.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnDelete.ForeColor = System.Drawing.Color.Gold;
            this.btnDelete.HoverState.Parent = this.btnDelete;
            this.btnDelete.Location = new System.Drawing.Point(482, 255);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.ShadowDecoration.Parent = this.btnDelete;
            this.btnDelete.Size = new System.Drawing.Size(120, 45);
            this.btnDelete.TabIndex = 19;
            this.btnDelete.Text = "Löschen";
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
            // 
            // dgvFlakons
            // 
            this.dgvFlakons.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvFlakons.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvFlakons.Location = new System.Drawing.Point(12, 422);
            this.dgvFlakons.Name = "dgvFlakons";
            this.dgvFlakons.Size = new System.Drawing.Size(1158, 277);
            this.dgvFlakons.TabIndex = 3;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.Location = new System.Drawing.Point(80, 101);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(110, 20);
            this.label8.TabIndex = 20;
            this.label8.Text = "Verschlussart:";
            // 
            // cbVerschlussart
            // 
            this.cbVerschlussart.BackColor = System.Drawing.Color.Transparent;
            this.cbVerschlussart.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cbVerschlussart.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbVerschlussart.FocusedColor = System.Drawing.Color.Empty;
            this.cbVerschlussart.FocusedState.Parent = this.cbVerschlussart;
            this.cbVerschlussart.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbVerschlussart.ForeColor = System.Drawing.Color.Black;
            this.cbVerschlussart.FormattingEnabled = true;
            this.cbVerschlussart.HoverState.Parent = this.cbVerschlussart;
            this.cbVerschlussart.ItemHeight = 30;
            this.cbVerschlussart.Items.AddRange(new object[] {
            "Schraubverschluss",
            "Crimpverschluss"});
            this.cbVerschlussart.ItemsAppearance.Parent = this.cbVerschlussart;
            this.cbVerschlussart.Location = new System.Drawing.Point(199, 92);
            this.cbVerschlussart.Name = "cbVerschlussart";
            this.cbVerschlussart.ShadowDecoration.Parent = this.cbVerschlussart;
            this.cbVerschlussart.Size = new System.Drawing.Size(188, 36);
            this.cbVerschlussart.TabIndex = 21;
            // 
            // frmFlakons
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1182, 702);
            this.Controls.Add(this.dgvFlakons);
            this.Controls.Add(this.gbFlakonsInfo);
            this.Controls.Add(this.cbAlleParfümStatus);
            this.Name = "frmFlakons";
            this.Text = "Flakons";
            this.Load += new System.EventHandler(this.frmFlakons_Load);
            this.cbAlleParfümStatus.ResumeLayout(false);
            this.cbAlleParfümStatus.PerformLayout();
            this.gbFlakonsInfo.ResumeLayout(false);
            this.gbFlakonsInfo.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvFlakons)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel cbAlleParfümStatus;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox gbFlakonsInfo;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private Guna.UI2.WinForms.Guna2TextBox txtFlakonsProkarton;
        private Guna.UI2.WinForms.Guna2TextBox txtVerbleibendeMenge;
        private Guna.UI2.WinForms.Guna2TextBox txtKarfonLager;
        private Guna.UI2.WinForms.Guna2ComboBox cbForm;
        private Guna.UI2.WinForms.Guna2ComboBox cbFarbe;
        private Guna.UI2.WinForms.Guna2ComboBox cbFlakonsMengeInMl;
        private Guna.UI2.WinForms.Guna2Button btnDelete;
        private Guna.UI2.WinForms.Guna2Button btnCancel;
        private Guna.UI2.WinForms.Guna2Button btnSave;
        private System.Windows.Forms.DataGridView dgvFlakons;
        private Guna.UI2.WinForms.Guna2ComboBox cbVerschlussart;
        private System.Windows.Forms.Label label8;
    }
}
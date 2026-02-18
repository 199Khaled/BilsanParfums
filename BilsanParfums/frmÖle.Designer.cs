namespace BilsanParfums
{
    partial class frmÖle
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle10 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle11 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle12 = new System.Windows.Forms.DataGridViewCellStyle();
            this.dgvDuftÖle = new System.Windows.Forms.DataGridView();
            this.gbParfümöleInfo = new System.Windows.Forms.GroupBox();
            this.cbÖltype = new Guna.UI2.WinForms.Guna2ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.rbNachgefüllteÖlmenge = new System.Windows.Forms.RadioButton();
            this.rbGelieferteÖlmenge = new System.Windows.Forms.RadioButton();
            this.txtGelieferteMenge = new Guna.UI2.WinForms.Guna2TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtParfümCode = new Guna.UI2.WinForms.Guna2TextBox();
            this.txtAlteNummer = new Guna.UI2.WinForms.Guna2TextBox();
            this.txtAltuelleMenge = new Guna.UI2.WinForms.Guna2TextBox();
            this.txtNachgefüllteMenge = new Guna.UI2.WinForms.Guna2TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.btnDelete = new Guna.UI2.WinForms.Guna2Button();
            this.btnCancel = new Guna.UI2.WinForms.Guna2Button();
            this.btnSave = new Guna.UI2.WinForms.Guna2Button();
            this.label4 = new System.Windows.Forms.Label();
            this.cbAlleParfümStatus = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.errorProvider1 = new System.Windows.Forms.ErrorProvider(this.components);
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.txtFilterwert = new Guna.UI2.WinForms.Guna2TextBox();
            this.cbFilterby = new Guna.UI2.WinForms.Guna2ComboBox();
            this.label5 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dgvDuftÖle)).BeginInit();
            this.gbParfümöleInfo.SuspendLayout();
            this.cbAlleParfümStatus.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // dgvDuftÖle
            // 
            this.dgvDuftÖle.AllowUserToAddRows = false;
            this.dgvDuftÖle.AllowUserToDeleteRows = false;
            this.dgvDuftÖle.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvDuftÖle.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvDuftÖle.BackgroundColor = System.Drawing.SystemColors.ActiveBorder;
            dataGridViewCellStyle10.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle10.BackColor = System.Drawing.Color.Gainsboro;
            dataGridViewCellStyle10.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle10.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle10.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle10.SelectionForeColor = System.Drawing.SystemColors.ControlDark;
            dataGridViewCellStyle10.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvDuftÖle.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle10;
            this.dgvDuftÖle.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle11.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle11.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle11.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle11.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle11.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle11.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle11.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvDuftÖle.DefaultCellStyle = dataGridViewCellStyle11;
            this.dgvDuftÖle.EnableHeadersVisualStyles = false;
            this.dgvDuftÖle.Location = new System.Drawing.Point(12, 432);
            this.dgvDuftÖle.MultiSelect = false;
            this.dgvDuftÖle.Name = "dgvDuftÖle";
            this.dgvDuftÖle.ReadOnly = true;
            dataGridViewCellStyle12.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.dgvDuftÖle.RowsDefaultCellStyle = dataGridViewCellStyle12;
            this.dgvDuftÖle.RowTemplate.Height = 35;
            this.dgvDuftÖle.Size = new System.Drawing.Size(939, 404);
            this.dgvDuftÖle.TabIndex = 6;
            this.dgvDuftÖle.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvDuftÖle_CellDoubleClick);
            this.dgvDuftÖle.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(this.dgvDuftÖle_CellFormatting);
            // 
            // gbParfümöleInfo
            // 
            this.gbParfümöleInfo.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.gbParfümöleInfo.BackColor = System.Drawing.Color.Gainsboro;
            this.gbParfümöleInfo.Controls.Add(this.cbÖltype);
            this.gbParfümöleInfo.Controls.Add(this.label3);
            this.gbParfümöleInfo.Controls.Add(this.rbNachgefüllteÖlmenge);
            this.gbParfümöleInfo.Controls.Add(this.rbGelieferteÖlmenge);
            this.gbParfümöleInfo.Controls.Add(this.txtGelieferteMenge);
            this.gbParfümöleInfo.Controls.Add(this.label2);
            this.gbParfümöleInfo.Controls.Add(this.txtParfümCode);
            this.gbParfümöleInfo.Controls.Add(this.txtAlteNummer);
            this.gbParfümöleInfo.Controls.Add(this.txtAltuelleMenge);
            this.gbParfümöleInfo.Controls.Add(this.txtNachgefüllteMenge);
            this.gbParfümöleInfo.Controls.Add(this.label8);
            this.gbParfümöleInfo.Controls.Add(this.btnDelete);
            this.gbParfümöleInfo.Controls.Add(this.btnCancel);
            this.gbParfümöleInfo.Controls.Add(this.btnSave);
            this.gbParfümöleInfo.Controls.Add(this.label4);
            this.gbParfümöleInfo.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gbParfümöleInfo.Location = new System.Drawing.Point(12, 62);
            this.gbParfümöleInfo.Name = "gbParfümöleInfo";
            this.gbParfümöleInfo.Size = new System.Drawing.Size(924, 299);
            this.gbParfümöleInfo.TabIndex = 5;
            this.gbParfümöleInfo.TabStop = false;
            this.gbParfümöleInfo.Text = "Parfümöle info";
            // 
            // cbÖltype
            // 
            this.cbÖltype.BackColor = System.Drawing.Color.Transparent;
            this.cbÖltype.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cbÖltype.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbÖltype.FocusedColor = System.Drawing.Color.Empty;
            this.cbÖltype.FocusedState.Parent = this.cbÖltype;
            this.cbÖltype.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbÖltype.ForeColor = System.Drawing.Color.Black;
            this.cbÖltype.FormattingEnabled = true;
            this.cbÖltype.HoverState.Parent = this.cbÖltype;
            this.cbÖltype.ItemHeight = 30;
            this.cbÖltype.Items.AddRange(new object[] {
            "Designer Duft",
            "Nische Duft",
            "Ultranische Duft"});
            this.cbÖltype.ItemsAppearance.Parent = this.cbÖltype;
            this.cbÖltype.Location = new System.Drawing.Point(211, 110);
            this.cbÖltype.Name = "cbÖltype";
            this.cbÖltype.ShadowDecoration.Parent = this.cbÖltype;
            this.cbÖltype.Size = new System.Drawing.Size(175, 36);
            this.cbÖltype.TabIndex = 32;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(130, 117);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(66, 20);
            this.label3.TabIndex = 31;
            this.label3.Text = "Öl Type:";
            // 
            // rbNachgefüllteÖlmenge
            // 
            this.rbNachgefüllteÖlmenge.AutoSize = true;
            this.rbNachgefüllteÖlmenge.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rbNachgefüllteÖlmenge.Location = new System.Drawing.Point(422, 161);
            this.rbNachgefüllteÖlmenge.Name = "rbNachgefüllteÖlmenge";
            this.rbNachgefüllteÖlmenge.Size = new System.Drawing.Size(188, 24);
            this.rbNachgefüllteÖlmenge.TabIndex = 30;
            this.rbNachgefüllteÖlmenge.TabStop = true;
            this.rbNachgefüllteÖlmenge.Text = "Nachgefüllte Ölmenge:";
            this.rbNachgefüllteÖlmenge.UseVisualStyleBackColor = true;
            this.rbNachgefüllteÖlmenge.CheckedChanged += new System.EventHandler(this.rbNachgefüllteÖlmenge_CheckedChanged);
            // 
            // rbGelieferteÖlmenge
            // 
            this.rbGelieferteÖlmenge.AutoSize = true;
            this.rbGelieferteÖlmenge.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rbGelieferteÖlmenge.Location = new System.Drawing.Point(28, 161);
            this.rbGelieferteÖlmenge.Name = "rbGelieferteÖlmenge";
            this.rbGelieferteÖlmenge.Size = new System.Drawing.Size(169, 24);
            this.rbGelieferteÖlmenge.TabIndex = 29;
            this.rbGelieferteÖlmenge.TabStop = true;
            this.rbGelieferteÖlmenge.Text = "Gelieferte Ölmenge:";
            this.rbGelieferteÖlmenge.UseVisualStyleBackColor = true;
            this.rbGelieferteÖlmenge.CheckedChanged += new System.EventHandler(this.rbGelieferteÖlmenge_CheckedChanged);
            // 
            // txtGelieferteMenge
            // 
            this.txtGelieferteMenge.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.txtGelieferteMenge.DefaultText = "";
            this.txtGelieferteMenge.DisabledState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(208)))), ((int)(((byte)(208)))));
            this.txtGelieferteMenge.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(226)))), ((int)(((byte)(226)))));
            this.txtGelieferteMenge.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.txtGelieferteMenge.DisabledState.Parent = this.txtGelieferteMenge;
            this.txtGelieferteMenge.DisabledState.PlaceholderForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.txtGelieferteMenge.FocusedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.txtGelieferteMenge.FocusedState.Parent = this.txtGelieferteMenge;
            this.txtGelieferteMenge.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtGelieferteMenge.ForeColor = System.Drawing.Color.Black;
            this.txtGelieferteMenge.HoverState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.txtGelieferteMenge.HoverState.Parent = this.txtGelieferteMenge;
            this.txtGelieferteMenge.Location = new System.Drawing.Point(211, 154);
            this.txtGelieferteMenge.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.txtGelieferteMenge.Name = "txtGelieferteMenge";
            this.txtGelieferteMenge.PasswordChar = '\0';
            this.txtGelieferteMenge.PlaceholderText = "";
            this.txtGelieferteMenge.SelectedText = "";
            this.txtGelieferteMenge.ShadowDecoration.Parent = this.txtGelieferteMenge;
            this.txtGelieferteMenge.Size = new System.Drawing.Size(175, 36);
            this.txtGelieferteMenge.TabIndex = 3;
            this.txtGelieferteMenge.Visible = false;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(503, 63);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(106, 20);
            this.label2.TabIndex = 27;
            this.label2.Text = "Parfüm Code:";
            // 
            // txtParfümCode
            // 
            this.txtParfümCode.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtParfümCode.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.txtParfümCode.DefaultText = "";
            this.txtParfümCode.DisabledState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(208)))), ((int)(((byte)(208)))));
            this.txtParfümCode.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(226)))), ((int)(((byte)(226)))));
            this.txtParfümCode.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.txtParfümCode.DisabledState.Parent = this.txtParfümCode;
            this.txtParfümCode.DisabledState.PlaceholderForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.txtParfümCode.FocusedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.txtParfümCode.FocusedState.Parent = this.txtParfümCode;
            this.txtParfümCode.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtParfümCode.ForeColor = System.Drawing.Color.Black;
            this.txtParfümCode.HoverState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.txtParfümCode.HoverState.Parent = this.txtParfümCode;
            this.txtParfümCode.Location = new System.Drawing.Point(616, 56);
            this.txtParfümCode.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.txtParfümCode.Name = "txtParfümCode";
            this.txtParfümCode.PasswordChar = '\0';
            this.txtParfümCode.PlaceholderText = "";
            this.txtParfümCode.SelectedText = "";
            this.txtParfümCode.ShadowDecoration.Parent = this.txtParfümCode;
            this.txtParfümCode.Size = new System.Drawing.Size(175, 36);
            this.txtParfümCode.TabIndex = 1;
            // 
            // txtAlteNummer
            // 
            this.txtAlteNummer.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.txtAlteNummer.DefaultText = "";
            this.txtAlteNummer.DisabledState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(208)))), ((int)(((byte)(208)))));
            this.txtAlteNummer.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(226)))), ((int)(((byte)(226)))));
            this.txtAlteNummer.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.txtAlteNummer.DisabledState.Parent = this.txtAlteNummer;
            this.txtAlteNummer.DisabledState.PlaceholderForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.txtAlteNummer.FocusedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.txtAlteNummer.FocusedState.Parent = this.txtAlteNummer;
            this.txtAlteNummer.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtAlteNummer.ForeColor = System.Drawing.Color.Black;
            this.txtAlteNummer.HoverState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.txtAlteNummer.HoverState.Parent = this.txtAlteNummer;
            this.txtAlteNummer.Location = new System.Drawing.Point(211, 56);
            this.txtAlteNummer.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.txtAlteNummer.Name = "txtAlteNummer";
            this.txtAlteNummer.PasswordChar = '\0';
            this.txtAlteNummer.PlaceholderText = "";
            this.txtAlteNummer.SelectedText = "";
            this.txtAlteNummer.ShadowDecoration.Parent = this.txtAlteNummer;
            this.txtAlteNummer.Size = new System.Drawing.Size(175, 36);
            this.txtAlteNummer.TabIndex = 0;
            // 
            // txtAltuelleMenge
            // 
            this.txtAltuelleMenge.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtAltuelleMenge.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.txtAltuelleMenge.DefaultText = "";
            this.txtAltuelleMenge.DisabledState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(208)))), ((int)(((byte)(208)))));
            this.txtAltuelleMenge.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(226)))), ((int)(((byte)(226)))));
            this.txtAltuelleMenge.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.txtAltuelleMenge.DisabledState.Parent = this.txtAltuelleMenge;
            this.txtAltuelleMenge.DisabledState.PlaceholderForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.txtAltuelleMenge.FocusedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.txtAltuelleMenge.FocusedState.Parent = this.txtAltuelleMenge;
            this.txtAltuelleMenge.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtAltuelleMenge.ForeColor = System.Drawing.Color.Black;
            this.txtAltuelleMenge.HoverState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.txtAltuelleMenge.HoverState.Parent = this.txtAltuelleMenge;
            this.txtAltuelleMenge.Location = new System.Drawing.Point(616, 108);
            this.txtAltuelleMenge.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.txtAltuelleMenge.Name = "txtAltuelleMenge";
            this.txtAltuelleMenge.PasswordChar = '\0';
            this.txtAltuelleMenge.PlaceholderText = "";
            this.txtAltuelleMenge.ReadOnly = true;
            this.txtAltuelleMenge.SelectedText = "";
            this.txtAltuelleMenge.ShadowDecoration.Parent = this.txtAltuelleMenge;
            this.txtAltuelleMenge.Size = new System.Drawing.Size(175, 36);
            this.txtAltuelleMenge.TabIndex = 2;
            // 
            // txtNachgefüllteMenge
            // 
            this.txtNachgefüllteMenge.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtNachgefüllteMenge.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.txtNachgefüllteMenge.DefaultText = "";
            this.txtNachgefüllteMenge.DisabledState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(208)))), ((int)(((byte)(208)))));
            this.txtNachgefüllteMenge.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(226)))), ((int)(((byte)(226)))));
            this.txtNachgefüllteMenge.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.txtNachgefüllteMenge.DisabledState.Parent = this.txtNachgefüllteMenge;
            this.txtNachgefüllteMenge.DisabledState.PlaceholderForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.txtNachgefüllteMenge.FocusedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.txtNachgefüllteMenge.FocusedState.Parent = this.txtNachgefüllteMenge;
            this.txtNachgefüllteMenge.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtNachgefüllteMenge.ForeColor = System.Drawing.Color.Black;
            this.txtNachgefüllteMenge.HoverState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.txtNachgefüllteMenge.HoverState.Parent = this.txtNachgefüllteMenge;
            this.txtNachgefüllteMenge.Location = new System.Drawing.Point(616, 154);
            this.txtNachgefüllteMenge.Margin = new System.Windows.Forms.Padding(7, 8, 7, 8);
            this.txtNachgefüllteMenge.Name = "txtNachgefüllteMenge";
            this.txtNachgefüllteMenge.PasswordChar = '\0';
            this.txtNachgefüllteMenge.PlaceholderText = "";
            this.txtNachgefüllteMenge.SelectedText = "";
            this.txtNachgefüllteMenge.ShadowDecoration.Parent = this.txtNachgefüllteMenge;
            this.txtNachgefüllteMenge.Size = new System.Drawing.Size(175, 36);
            this.txtNachgefüllteMenge.TabIndex = 4;
            this.txtNachgefüllteMenge.Visible = false;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.Location = new System.Drawing.Point(471, 116);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(138, 20);
            this.label8.TabIndex = 20;
            this.label8.Text = "Aktuelle Ölmenge:";
            // 
            // btnDelete
            // 
            this.btnDelete.CheckedState.Parent = this.btnDelete;
            this.btnDelete.CustomImages.Parent = this.btnDelete;
            this.btnDelete.FillColor = System.Drawing.Color.DarkCyan;
            this.btnDelete.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnDelete.ForeColor = System.Drawing.Color.Gold;
            this.btnDelete.HoverState.Parent = this.btnDelete;
            this.btnDelete.Location = new System.Drawing.Point(480, 239);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.ShadowDecoration.Parent = this.btnDelete;
            this.btnDelete.Size = new System.Drawing.Size(120, 45);
            this.btnDelete.TabIndex = 7;
            this.btnDelete.Text = "Löschen";
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.CheckedState.Parent = this.btnCancel;
            this.btnCancel.CustomImages.Parent = this.btnCancel;
            this.btnCancel.FillColor = System.Drawing.Color.DarkCyan;
            this.btnCancel.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCancel.ForeColor = System.Drawing.Color.Gold;
            this.btnCancel.HoverState.Parent = this.btnCancel;
            this.btnCancel.Location = new System.Drawing.Point(345, 239);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.ShadowDecoration.Parent = this.btnCancel;
            this.btnCancel.Size = new System.Drawing.Size(120, 45);
            this.btnCancel.TabIndex = 6;
            this.btnCancel.Text = "Abbrechen";
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnSave
            // 
            this.btnSave.CheckedState.Parent = this.btnSave;
            this.btnSave.CustomImages.Parent = this.btnSave;
            this.btnSave.FillColor = System.Drawing.Color.DarkCyan;
            this.btnSave.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSave.ForeColor = System.Drawing.Color.Gold;
            this.btnSave.HoverState.Parent = this.btnSave;
            this.btnSave.Location = new System.Drawing.Point(206, 239);
            this.btnSave.Name = "btnSave";
            this.btnSave.ShadowDecoration.Parent = this.btnSave;
            this.btnSave.Size = new System.Drawing.Size(120, 45);
            this.btnSave.TabIndex = 5;
            this.btnSave.Text = "Speichern";
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(92, 63);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(105, 20);
            this.label4.TabIndex = 3;
            this.label4.Text = "Alte Nummer:";
            // 
            // cbAlleParfümStatus
            // 
            this.cbAlleParfümStatus.BackColor = System.Drawing.Color.DarkCyan;
            this.cbAlleParfümStatus.Controls.Add(this.label1);
            this.cbAlleParfümStatus.Dock = System.Windows.Forms.DockStyle.Top;
            this.cbAlleParfümStatus.Location = new System.Drawing.Point(0, 0);
            this.cbAlleParfümStatus.Name = "cbAlleParfümStatus";
            this.cbAlleParfümStatus.Size = new System.Drawing.Size(1450, 51);
            this.cbAlleParfümStatus.TabIndex = 4;
            // 
            // label1
            // 
            this.label1.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.Gold;
            this.label1.Location = new System.Drawing.Point(680, 14);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(129, 25);
            this.label1.TabIndex = 0;
            this.label1.Text = "Parfüm Öle";
            // 
            // errorProvider1
            // 
            this.errorProvider1.ContainerControl = this;
            // 
            // pictureBox1
            // 
            this.pictureBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pictureBox1.Image = global::BilsanParfums.Properties.Resources.für_Programm;
            this.pictureBox1.Location = new System.Drawing.Point(969, 57);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(469, 779);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            // 
            // txtFilterwert
            // 
            this.txtFilterwert.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtFilterwert.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.txtFilterwert.DefaultText = "";
            this.txtFilterwert.DisabledState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(208)))), ((int)(((byte)(208)))));
            this.txtFilterwert.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(226)))), ((int)(((byte)(226)))));
            this.txtFilterwert.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.txtFilterwert.DisabledState.Parent = this.txtFilterwert;
            this.txtFilterwert.DisabledState.PlaceholderForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.txtFilterwert.FocusedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.txtFilterwert.FocusedState.Parent = this.txtFilterwert;
            this.txtFilterwert.Font = new System.Drawing.Font("Segoe UI Semibold", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtFilterwert.ForeColor = System.Drawing.Color.Black;
            this.txtFilterwert.HoverState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.txtFilterwert.HoverState.Parent = this.txtFilterwert;
            this.txtFilterwert.Location = new System.Drawing.Point(330, 388);
            this.txtFilterwert.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.txtFilterwert.Name = "txtFilterwert";
            this.txtFilterwert.PasswordChar = '\0';
            this.txtFilterwert.PlaceholderText = "";
            this.txtFilterwert.SelectedText = "";
            this.txtFilterwert.ShadowDecoration.Parent = this.txtFilterwert;
            this.txtFilterwert.Size = new System.Drawing.Size(282, 36);
            this.txtFilterwert.TabIndex = 71;
            this.txtFilterwert.TextChanged += new System.EventHandler(this.txtFilterwert_TextChanged);
            this.txtFilterwert.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtFilterwert_KeyPress);
            // 
            // cbFilterby
            // 
            this.cbFilterby.BackColor = System.Drawing.Color.Transparent;
            this.cbFilterby.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cbFilterby.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbFilterby.FocusedColor = System.Drawing.Color.Empty;
            this.cbFilterby.FocusedState.Parent = this.cbFilterby;
            this.cbFilterby.Font = new System.Drawing.Font("Arial", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbFilterby.ForeColor = System.Drawing.Color.Black;
            this.cbFilterby.FormattingEnabled = true;
            this.cbFilterby.HoverState.Parent = this.cbFilterby;
            this.cbFilterby.ItemHeight = 30;
            this.cbFilterby.Items.AddRange(new object[] {
            "AlteNummer",
            "ParfümCode",
            "Öltype"});
            this.cbFilterby.ItemsAppearance.Parent = this.cbFilterby;
            this.cbFilterby.Location = new System.Drawing.Point(131, 386);
            this.cbFilterby.Name = "cbFilterby";
            this.cbFilterby.ShadowDecoration.Parent = this.cbFilterby;
            this.cbFilterby.Size = new System.Drawing.Size(190, 36);
            this.cbFilterby.TabIndex = 70;
            this.cbFilterby.SelectedIndexChanged += new System.EventHandler(this.cbFilterby_SelectedIndexChanged);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.ForeColor = System.Drawing.Color.Black;
            this.label5.Location = new System.Drawing.Point(11, 392);
            this.label5.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(98, 24);
            this.label5.TabIndex = 69;
            this.label5.Text = "Filter bei:";
            // 
            // frmÖle
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1450, 848);
            this.Controls.Add(this.txtFilterwert);
            this.Controls.Add(this.cbFilterby);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.dgvDuftÖle);
            this.Controls.Add(this.gbParfümöleInfo);
            this.Controls.Add(this.cbAlleParfümStatus);
            this.Controls.Add(this.pictureBox1);
            this.Name = "frmÖle";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "frmÖle";
            this.Load += new System.EventHandler(this.frmÖle_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvDuftÖle)).EndInit();
            this.gbParfümöleInfo.ResumeLayout(false);
            this.gbParfümöleInfo.PerformLayout();
            this.cbAlleParfümStatus.ResumeLayout(false);
            this.cbAlleParfümStatus.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dgvDuftÖle;
        private System.Windows.Forms.GroupBox gbParfümöleInfo;
        private Guna.UI2.WinForms.Guna2TextBox txtNachgefüllteMenge;
        private System.Windows.Forms.Label label8;
        private Guna.UI2.WinForms.Guna2Button btnDelete;
        private Guna.UI2.WinForms.Guna2Button btnCancel;
        private Guna.UI2.WinForms.Guna2Button btnSave;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Panel cbAlleParfümStatus;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private Guna.UI2.WinForms.Guna2TextBox txtParfümCode;
        private Guna.UI2.WinForms.Guna2TextBox txtAlteNummer;
        private Guna.UI2.WinForms.Guna2TextBox txtAltuelleMenge;
        private Guna.UI2.WinForms.Guna2TextBox txtGelieferteMenge;
        private System.Windows.Forms.RadioButton rbGelieferteÖlmenge;
        private System.Windows.Forms.RadioButton rbNachgefüllteÖlmenge;
        private System.Windows.Forms.ErrorProvider errorProvider1;
        private System.Windows.Forms.Label label3;
        private Guna.UI2.WinForms.Guna2ComboBox cbÖltype;
        private Guna.UI2.WinForms.Guna2TextBox txtFilterwert;
        private Guna.UI2.WinForms.Guna2ComboBox cbFilterby;
        private System.Windows.Forms.Label label5;
    }
}
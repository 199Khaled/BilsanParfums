namespace BilsanParfums
{
    partial class frmHauptForm
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
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.btmParfüm = new Guna.UI2.WinForms.Guna2Button();
            this.btnDuftÖle = new Guna.UI2.WinForms.Guna2Button();
            this.btnFlaschen = new Guna.UI2.WinForms.Guna2Button();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = global::BilsanParfums.Properties.Resources.für_Programm;
            this.pictureBox1.Location = new System.Drawing.Point(12, 3);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(562, 832);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            // 
            // btmParfüm
            // 
            this.btmParfüm.CheckedState.Parent = this.btmParfüm;
            this.btmParfüm.CustomImages.Parent = this.btmParfüm;
            this.btmParfüm.FillColor = System.Drawing.Color.DarkGoldenrod;
            this.btmParfüm.Font = new System.Drawing.Font("Segoe UI", 27.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btmParfüm.ForeColor = System.Drawing.Color.White;
            this.btmParfüm.HoverState.Parent = this.btmParfüm;
            this.btmParfüm.Location = new System.Drawing.Point(635, 166);
            this.btmParfüm.Name = "btmParfüm";
            this.btmParfüm.ShadowDecoration.Parent = this.btmParfüm;
            this.btmParfüm.Size = new System.Drawing.Size(374, 132);
            this.btmParfüm.TabIndex = 1;
            this.btmParfüm.Text = "Parfüm";
            this.btmParfüm.Click += new System.EventHandler(this.btmParfüm_Click);
            // 
            // btnDuftÖle
            // 
            this.btnDuftÖle.ButtonMode = Guna.UI2.WinForms.Enums.ButtonMode.RadioButton;
            this.btnDuftÖle.CheckedState.Parent = this.btnDuftÖle;
            this.btnDuftÖle.CustomImages.Parent = this.btnDuftÖle;
            this.btnDuftÖle.FillColor = System.Drawing.Color.DarkGoldenrod;
            this.btnDuftÖle.Font = new System.Drawing.Font("Segoe UI", 27.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnDuftÖle.ForeColor = System.Drawing.Color.White;
            this.btnDuftÖle.HoverState.Parent = this.btnDuftÖle;
            this.btnDuftÖle.Location = new System.Drawing.Point(635, 337);
            this.btnDuftÖle.Name = "btnDuftÖle";
            this.btnDuftÖle.ShadowDecoration.Parent = this.btnDuftÖle;
            this.btnDuftÖle.Size = new System.Drawing.Size(374, 132);
            this.btnDuftÖle.TabIndex = 2;
            this.btnDuftÖle.Text = "Duft Öle";
            this.btnDuftÖle.Click += new System.EventHandler(this.btnDuftÖle_Click);
            // 
            // btnFlaschen
            // 
            this.btnFlaschen.ButtonMode = Guna.UI2.WinForms.Enums.ButtonMode.RadioButton;
            this.btnFlaschen.CheckedState.Parent = this.btnFlaschen;
            this.btnFlaschen.CustomImages.Parent = this.btnFlaschen;
            this.btnFlaschen.FillColor = System.Drawing.Color.DarkGoldenrod;
            this.btnFlaschen.Font = new System.Drawing.Font("Segoe UI", 27.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnFlaschen.ForeColor = System.Drawing.Color.White;
            this.btnFlaschen.HoverState.Parent = this.btnFlaschen;
            this.btnFlaschen.Location = new System.Drawing.Point(635, 510);
            this.btnFlaschen.Name = "btnFlaschen";
            this.btnFlaschen.ShadowDecoration.Parent = this.btnFlaschen;
            this.btnFlaschen.Size = new System.Drawing.Size(374, 132);
            this.btnFlaschen.TabIndex = 3;
            this.btnFlaschen.Text = "Flaschen";
            this.btnFlaschen.Click += new System.EventHandler(this.btnFlaschen_Click);
            // 
            // frmHauptForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1035, 847);
            this.Controls.Add(this.btnFlaschen);
            this.Controls.Add(this.btnDuftÖle);
            this.Controls.Add(this.btmParfüm);
            this.Controls.Add(this.pictureBox1);
            this.Name = "frmHauptForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "frmHauptForm";
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox pictureBox1;
        private Guna.UI2.WinForms.Guna2Button btmParfüm;
        private Guna.UI2.WinForms.Guna2Button btnDuftÖle;
        private Guna.UI2.WinForms.Guna2Button btnFlaschen;
    }
}
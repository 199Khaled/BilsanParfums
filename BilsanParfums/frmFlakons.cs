using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing; // Wichtig für Color
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using BilsanDb_BusinessLayer; // Stellen Sie sicher, dass Ihre Business Layer Namespaces korrekt sind
using Busnisse_Layer;        // Falls dies auch ein relevanter Namespace ist

namespace BilsanParfums
{
    public partial class frmFlakons : Form
    {
        enum enMode { addnew = 0, update = 1 }
        enMode _mode = enMode.addnew;

        DataTable _dtFlakons;
        clsFlakons _flakons;
        BindingSource _bindingSource;
       

        public frmFlakons()
        {
            InitializeComponent();
            _bindingSource = new BindingSource();
            // WICHTIG: Abonnieren Sie das CellFormatting-Ereignis hier
            this.dgvFlakons.CellFormatting += new DataGridViewCellFormattingEventHandler(this.dgvFlakons_CellFormatting);
        }

        private void frmFlakons_Load(object sender, EventArgs e)
        {
            _LadeFlakonsdatenFromDatabase();
        }

        private void _LadeFlakonsdatenFromDatabase()
        {
           
                _dtFlakons = clsFlakons.GetAllFlakons(); // Angenommen, diese Methode gibt DataTable zurück
                if (_dtFlakons != null && _dtFlakons.Rows.Count > 0)
                {
                    _bindingSource.DataSource = _dtFlakons;
                    dgvFlakons.DataSource = _bindingSource;
                    _PasseDataGridViewSchriftAn(dgvFlakons);
                    // _MarkiereParfümZeilen(dgvFlakons); // DIESE ZEILE WIRD ENTFERNT, da CellFormatting es dynamisch macht
                }
            
        }

        private void _PasseDataGridViewSchriftAn(DataGridView dgv)
        {
            dgv.DefaultCellStyle.Font = new System.Drawing.Font("Segoe UI", 13, FontStyle.Regular);
            dgv.ColumnHeadersDefaultCellStyle.Font = new System.Drawing.Font("Segoe UI", 14, FontStyle.Italic);
        }

        /// <summary>
        /// Färbt die "RestFlakons"-Zelle orange, wenn der Wert unter 1000 liegt.
        /// Dies ist ein Ereignishandler für das CellFormatting-Ereignis des DataGridView.
        /// </summary>
        private void dgvFlakons_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            // Stellen Sie sicher, dass dies nicht die Kopfzeile oder eine leere Zeile ist
            if (e.RowIndex < 0 || e.RowIndex == ((DataGridView)sender).NewRowIndex)
            {
                return;
            }

            // Verwenden Sie den ColumnIndex, um auf die Spalte zuzugreifen
            // Überprüfen, ob die Spalte vorhanden ist, bevor Sie auf ihren Namen zugreifen
            if (e.ColumnIndex >= 0 && ((DataGridView)sender).Columns.Count > e.ColumnIndex)
            {
                // Überprüfen, ob die aktuelle Zelle zur Spalte "Rest" gehört
                if (((DataGridView)sender).Columns[e.ColumnIndex].Name == "Rest")
                {
                    // Versuchen Sie, den Wert sicher in eine Ganzzahl umzuwandeln
                    if (e.Value != null && int.TryParse(e.Value.ToString(), out int restFlakons))
                    {
                        // Prüfen Sie, ob der Restbestand unter 1000 liegt
                        if (restFlakons < 1000)
                        {
                            // Setzen Sie die Hintergrundfarbe der aktuellen Zelle auf Orange
                            e.CellStyle.BackColor = Color.Orange;
                            // Sie können auch die Vordergrundfarbe anpassen, um die Lesbarkeit zu verbessern
                            e.CellStyle.ForeColor = Color.Black;
                        }
                        else
                        {
                            // Wenn der Wert 1000 oder mehr ist, setzen Sie die Standardfarben zurück
                            e.CellStyle.BackColor = Color.White;
                            e.CellStyle.ForeColor = Color.Black;
                        }
                    }
                }
            }
        }

        private void _ResetDefaultValues()
        {
            _flakons = null;
            _mode = enMode.addnew;

            rbBenötigt.Checked = false;
            rbGeliefert.Checked = false;

            txtBenötigteFlakons.Visible = false;
            txtGelieferteFlakons.Visible = false;

            cbFlakonsMengeInMl.SelectedIndex = -1;
            errorProvider1.SetError(cbFlakonsMengeInMl, null);
            cbFlakonsMengeInMl.FillColor = Color.White;

            cbForm.SelectedIndex = -1;
            errorProvider1.SetError(cbForm, null);
            cbForm.FillColor = Color.White;

            cbVerschlussart.SelectedIndex = -1;
            errorProvider1.SetError(cbVerschlussart, null);
            cbVerschlussart.FillColor = Color.White;

            cbFarbe.SelectedIndex = -1;
            errorProvider1.SetError(cbFarbe, null);
            cbFarbe.FillColor = Color.White;

            txtBenötigteFlakons.Clear();
            txtGelieferteFlakons.Clear();
            txtVerbleibendeMenge.Clear();

        }

        private bool _TextFelderValidierung(Guna.UI2.WinForms.Guna2TextBox textBox, string fieldName)
        {
            if (string.IsNullOrEmpty(textBox.Text))
            {
                errorProvider1.SetError(textBox, fieldName + " Feld darf nicht leer sein!");
                textBox.FillColor = Color.LightPink;
                return false;
            }
            else
            {
                errorProvider1.SetError(textBox, null);
                textBox.FillColor = Color.White;
                return true;
            }
        }

        private bool _ComboBoxValidierung(Guna.UI2.WinForms.Guna2ComboBox comboBox, string fieldName)
        {
            if (string.IsNullOrEmpty(comboBox.Text))
            {
                errorProvider1.SetError(comboBox, fieldName + " darf nicht leer sein!");
                comboBox.FillColor = Color.LightPink;
                return false;
            }
            else
            {
                errorProvider1.SetError(comboBox, null);
                comboBox.FillColor = Color.White;
                return true;
            }
        }

        private bool _SindEingabenValidiert()
        {
            bool isValid = true;
            isValid = _ComboBoxValidierung(cbFlakonsMengeInMl, "Flakonsmenge");
            isValid &= _ComboBoxValidierung(cbForm, "Form");
            isValid &= _ComboBoxValidierung(cbVerschlussart, "Verschlussart");
            isValid &= _ComboBoxValidierung(cbFarbe, "Farbe");
            if (txtGelieferteFlakons.Visible)
                isValid &= _TextFelderValidierung(txtGelieferteFlakons, "Gelieferte Flakons");
            if (txtBenötigteFlakons.Visible)
                isValid &= _TextFelderValidierung(txtBenötigteFlakons, "Benötigte Flakons");

            return isValid;
        }

        private void _LadenFlakonsdaten(int flakonID)
        {

            _flakons = clsFlakons.FindByFlakonID(flakonID);
            if (_flakons != null)
            {
                cbFlakonsMengeInMl.SelectedItem = _flakons.FlakonsMengeInMl;
                cbForm.SelectedItem = _flakons.Form;
                cbFarbe.SelectedItem = _flakons.Farbe;
                cbVerschlussart.SelectedItem = _flakons.Verschlussart;
                txtVerbleibendeMenge.Text = _flakons.Verbleibende_Flakons.ToString();
            }
            else
            {
                MessageBox.Show("Kein Flakon mit dieser ID gefunden.",
                                "Nicht gefunden",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Information);

                _flakons = null;
            }
        }

        private bool _FillFlakonsdaten()
        {
            _flakons.FlakonsMengeInMl = cbFlakonsMengeInMl.SelectedItem.ToString();
            _flakons.Form = cbForm.SelectedItem.ToString();
            _flakons.Verschlussart = cbVerschlussart.SelectedItem.ToString();
            _flakons.Farbe = cbFarbe.SelectedItem.ToString();

            // 🔹 Aktuelle Flakons (wenn leer → 0)
            int aktuelleFlakonsMenge = 0;
            if (!string.IsNullOrWhiteSpace(txtVerbleibendeMenge.Text))
            {
                if (!int.TryParse(txtVerbleibendeMenge.Text.Trim(), out aktuelleFlakonsMenge))
                {
                    MessageBox.Show("Ungültige aktuelle Menge!");
                    return false;
                }
            }

            int neueFlakonsmenge = aktuelleFlakonsMenge;
            // 🔹 Gelieferte Menge
            if (rbGeliefert.Checked)
            {
                if (!int.TryParse(txtGelieferteFlakons.Text.Trim(), out int geliefert))
                {
                    MessageBox.Show("Ungültige gelieferte Menge!");
                    return false;
                }

                neueFlakonsmenge += geliefert;
            }

            // 🔹 Benötigte Menge (Abzug)
            else if (rbBenötigt.Checked)
            {
                if (!int.TryParse(txtBenötigteFlakons.Text.Trim(), out int benötigt))
                {
                    MessageBox.Show("Ungültige benötigte Menge!");
                    return false;
                }

                neueFlakonsmenge -= benötigt;
            }

            // 🔹 Negativen Bestand verhindern
            if (neueFlakonsmenge < 0)
            {
                MessageBox.Show("Bestand darf nicht negativ sein!");
                return false;
            }

            _flakons.Verbleibende_Flakons = neueFlakonsmenge;
            _flakons.Aktivierungsdatum = DateTime.Now.Date;

            return true;
        }

        private bool _FlakonsDatenSpeichern()
        {
            if (!_SindEingabenValidiert())
                return false;

            //sicherstellen, dass Object existiert.
            if (_flakons == null)
            {
                _flakons = new clsFlakons();
            }

            if (!_FillFlakonsdaten())
            {
                return false;
            }

            string statusMessage;
            if (_mode == enMode.addnew)
                statusMessage = "hinzugefügt";
            else
                statusMessage = "aktualisiert";

            if (!_flakons.Save())
            {
                MessageBox.Show($"Fehler beim {statusMessage} ist aufgetreten.", "Fehler",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            _LadeFlakonsdatenFromDatabase();
            _ResetDefaultValues();

            return true;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            _FlakonsDatenSpeichern();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            _ResetDefaultValues();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (dgvFlakons.CurrentRow != null)
            {
                int? flakonID = (int)dgvFlakons.CurrentRow.Cells[0].Value;
                _EntferneParfüm(flakonID);
            }
        }
        /// <summary>
        /// Entfernt ein Parfüm aus der Datenbank.
        /// </summary>
        private void _EntferneParfüm(int? flakonID)
        {
            bool result = (MessageBox.Show("Sind Sie sicher, Sie möchten diesen Vorgang durchführen?", "Hinweis", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2) == DialogResult.OK);

            if (!result) return;

            clsFlakons flakonDaten = clsFlakons.FindByFlakonID(flakonID);

            if (flakonDaten != null && flakonDaten.DeleteFlakons())
            {
                _LadeFlakonsdatenFromDatabase();
            }
            else
            {
                MessageBox.Show("Flakonsdaten wurden nicht gefunden, \nbitte versuchen Sie es erneut.", "Entfernung", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void txtKarfonLager_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar);
        }

        private void txtFlakonsProkarton_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar);
        }

        private void txtBenötigteFlakons_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar);
        }

        private void dgvFlakons_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && dgvFlakons.Rows[e.RowIndex].Cells[0].Value != null)
            {
                int flakonID = (int)dgvFlakons.Rows[e.RowIndex].Cells[0].Value;
                _LadenFlakonsdaten(flakonID);
                _mode = enMode.update; // Setzen Sie den Modus auf Update
            }
        }

        private void rbGeliefert_CheckedChanged(object sender, EventArgs e)
        {
            txtBenötigteFlakons.Visible = false;
            txtGelieferteFlakons.Visible = true;
        }

        private void rbBenötigt_CheckedChanged(object sender, EventArgs e)
        {
            txtGelieferteFlakons.Visible = false;
            txtBenötigteFlakons.Visible = true;
        }
    }
}
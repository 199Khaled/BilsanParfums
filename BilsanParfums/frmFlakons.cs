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
        private readonly object _dataloadLock = new object();

        DataTable _dtFlakons;
        clsFlakons _flakons;
        BindingSource _bindingSource;
        private int? _initialVerbleibendeMenge; // Neue Variable, um den initialen Wert zu speichern

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
            lock (_dataloadLock)
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

            txtKarfonLager.Clear();
            errorProvider1.SetError(txtKarfonLager, null);
            txtKarfonLager.FillColor = Color.White;

            txtFlakonsProkarton.Clear();
            errorProvider1.SetError(txtFlakonsProkarton, null);
            txtFlakonsProkarton.FillColor = Color.White;

            txtBenötigteFlakons.Clear();
            txtVerbleibendeMenge.Clear();

            lblBenötigteFlakons.Visible = false;
            txtBenötigteFlakons.Visible = false;
            lblVerbleibendeFlakons.Visible = false;
            txtVerbleibendeMenge.Visible = false;

            _initialVerbleibendeMenge = 0; // Beim Zurücksetzen auch den Initialwert löschen
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
            isValid &= _TextFelderValidierung(txtKarfonLager, "Kartonslager");
            isValid &= _TextFelderValidierung(txtFlakonsProkarton, "Flakon pro Karton");
            return isValid;
        }

        private void _LadenFlakonsdaten(int flakonID)
        {
            lblBenötigteFlakons.Visible = true;
            txtBenötigteFlakons.Visible = true;
            lblVerbleibendeFlakons.Visible = true;
            txtVerbleibendeMenge.Visible = true;

            _flakons = clsFlakons.FindByFlakonID(flakonID);
            if (_flakons != null)
            {
                cbFlakonsMengeInMl.SelectedItem = _flakons.FlakonsMengeInMl;
                cbForm.SelectedItem = _flakons.Form;
                cbFarbe.SelectedItem = _flakons.Farbe;
                cbVerschlussart.SelectedItem = _flakons.Verschlussart;
                txtKarfonLager.Text = _flakons.Kartons_Lager.ToString();
                txtFlakonsProkarton.Text = _flakons.Flakons_pro_Karton.ToString();

                // HIER WIRD DER INITIALE WERT GESPEICHERT
                _initialVerbleibendeMenge = _flakons.Verbleibende_Flakons;
                txtVerbleibendeMenge.Text = _initialVerbleibendeMenge.ToString();
            }
            else
            {
                _initialVerbleibendeMenge = 0; // Wenn Flakon nicht gefunden, Initialwert auf 0 setzen
                txtVerbleibendeMenge.Text = "0";
            }
            // Beim Laden der Daten, txtBenötigteFlakons zurücksetzen
            txtBenötigteFlakons.Clear();
        }

        private void _FillFlakonsdaten()
        {
            _flakons.FlakonsMengeInMl = cbFlakonsMengeInMl.SelectedItem.ToString();
            _flakons.Form = cbForm.SelectedItem.ToString();
            _flakons.Verschlussart = cbVerschlussart.SelectedItem.ToString();
            _flakons.Farbe = cbFarbe.SelectedItem.ToString();
            _flakons.Kartons_Lager = Convert.ToInt32(txtKarfonLager.Text.Trim());
            _flakons.Flakons_pro_Karton = Convert.ToInt32(txtFlakonsProkarton.Text.Trim());

            if (_mode == enMode.addnew)
            {
                int verbleibendeFlakons;
                int kartonsLager = Convert.ToInt32(txtKarfonLager.Text.Trim());
                int flakonsProKarton = Convert.ToInt32(txtFlakonsProkarton.Text.Trim());
                verbleibendeFlakons = kartonsLager * flakonsProKarton;
                _flakons.Verbleibende_Flakons = verbleibendeFlakons;
            }
            // Die Logik hier wurde vereinfacht, da txtVerbleibendeMenge nun eher ein Ausgabefeld ist
            // und der Wert primär aus _initialVerbleibendeMenge und txtBenötigteFlakons abgeleitet wird.
            // Wenn txtVerbleibendeMenge in _FillFlakonsdaten noch manuell überschrieben werden soll,
            // wäre eine spezifischere Logik nötig, die aber nicht empfehlenswert wäre, wenn es berechnet wird.
            // Ich gehe davon aus, dass _flakons.Verbleibende_Flakons den Endwert nach Abzug repräsentiert,
            // der dann in der Datenbank gespeichert wird.

            // Wenn die Form im Update-Modus ist und txtBenötigteFlakons gefüllt wurde,
            // dann sollte der verbleibende Wert der sein, der im txtVerbleibendeMenge steht.
            if (_mode == enMode.update && !string.IsNullOrEmpty(txtVerbleibendeMenge.Text))
            {
                if (int.TryParse(txtVerbleibendeMenge.Text.Trim(), out int finalRemaining))
                {
                    _flakons.Verbleibende_Flakons = finalRemaining;
                }
            }


            _flakons.Erstellungsdatum = DateTime.Now.Date;
        }

        private bool _FlakonsDatenSpeichern()
        {
            if (!_SindEingabenValidiert())
                return false;

            if (_flakons == null)
                _flakons = new clsFlakons();

            _FillFlakonsdaten();

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

        private void txtBenötigteFlakons_TextChanged(object sender, EventArgs e)
        {
            // Wenn txtBenötigteFlakons leer ist, den txtVerbleibendeMenge auf den Initialwert zurücksetzen
            if (string.IsNullOrEmpty(txtBenötigteFlakons.Text.Trim()))
            {
                txtVerbleibendeMenge.Text = _initialVerbleibendeMenge.ToString();
                return; // Beenden der Methode
            }

            // Sicherstellen, dass _initialVerbleibendeMenge geladen wurde (z.B. nach CellDoubleClick)
            if (_initialVerbleibendeMenge == 0 && string.IsNullOrEmpty(txtVerbleibendeMenge.Text.Trim()))
            {
                // Dies sollte nur passieren, wenn keine Daten geladen wurden und der Benutzer direkt eingibt
                // Hier könnten Sie eine Meldung anzeigen, dass zuerst ein Flakon ausgewählt werden muss
                MessageBox.Show("Bitte wählen Sie zuerst einen Flakon aus oder geben Sie den initialen Bestand an.", "Fehler", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtBenötigteFlakons.Text = string.Empty;
                txtVerbleibendeMenge.Text = "0"; // Setzen Sie den Wert zurück
                return;
            }


            int benötigteFlakons;
            // Versuchen Sie, den Wert von txtBenötigteFlakons in eine Ganzzahl umzuwandeln
            if (!int.TryParse(txtBenötigteFlakons.Text.Trim(), out benötigteFlakons))
            {
                // Fehlerbehandlung, falls der Text keine gültige Zahl ist (z.B. wenn der Benutzer Text eingibt)
                // Setzen Sie den Verbleibenden Bestand auf den Initialwert zurück und löschen Sie die fehlerhafte Eingabe
                txtVerbleibendeMenge.Text = _initialVerbleibendeMenge.ToString();
                errorProvider1.SetError(txtBenötigteFlakons, "Bitte geben Sie eine gültige Zahl ein!");
                return;
            }
            else
            {
                errorProvider1.SetError(txtBenötigteFlakons, null); // Fehlermeldung entfernen
            }


            // Berechnung des neuen verbleibenden Bestands basierend auf dem Initialwert
            int? neuerVerbleibenderBestand = _initialVerbleibendeMenge - benötigteFlakons;

            if (neuerVerbleibenderBestand < 0)
            {
                MessageBox.Show("Die benötigte Menge darf den verbleibenden Bestand nicht überschreiten!", "Mengenprüfung", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtBenötigteFlakons.Text = string.Empty; // Eingabe löschen
                txtVerbleibendeMenge.Text = _initialVerbleibendeMenge.ToString(); // Auf Initialwert zurücksetzen
                return;
            }

            txtVerbleibendeMenge.Text = neuerVerbleibenderBestand.ToString();
        }
    }
}
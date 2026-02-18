using BilsanDb_BusinessLayer;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BilsanParfums
{
    public partial class frmÖle : Form
    {
        enum enMode { addnew = 0, update = 1 }
        enMode _mode = enMode.addnew;
        private readonly object _dataloadLock = new object();


        DataTable _dtÖlmenge;
        clsDuftÖle _duftÖle;
        BindingSource _bindingSource;

        private BindingSource _bindingSourceAlleParfüms;
        public frmÖle()
        {
            InitializeComponent();
            _bindingSource = new BindingSource();
            // WICHTIG: Abonnieren Sie das CellFormatting-Ereignis hier
            this.dgvDuftÖle.CellFormatting += new DataGridViewCellFormattingEventHandler(this.dgvDuftÖle_CellFormatting);
        }

        private void frmÖle_Load(object sender, EventArgs e)
        {
            _LadeDuftÖledatenFromDatabase();
        }
        
        private void _LadeDuftÖledatenFromDatabase()
        {
            lock (_dataloadLock)
            {
                _dtÖlmenge = clsDuftÖle.GetAllDuftÖle(); // Angenommen, diese Methode gibt DataTable zurück
                if (_dtÖlmenge != null && _dtÖlmenge.Rows.Count > 0)
                {
                    _bindingSource.DataSource = _dtÖlmenge;
                    dgvDuftÖle.DataSource = _bindingSource;
                    _PasseDataGridViewSchriftAn(dgvDuftÖle);
                    // _MarkiereParfümZeilen(dgvFlakons); // DIESE ZEILE WIRD ENTFERNT, da CellFormatting es dynamisch macht
                }
            }
        }

        private void _PasseDataGridViewSchriftAn(DataGridView dgv)
        {
            dgv.DefaultCellStyle.Font = new System.Drawing.Font("Segoe UI", 13, FontStyle.Regular);
            dgv.ColumnHeadersDefaultCellStyle.Font = new System.Drawing.Font("Segoe UI", 14, FontStyle.Italic);
        }

        private void dgvDuftÖle_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
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
                if (((DataGridView)sender).Columns[e.ColumnIndex].Name == "Ölmenge")
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
            _duftÖle = null;
            _mode = enMode.addnew;

            txtAlteNummer.ReadOnly = false;
            txtParfümCode.ReadOnly = false;


            rbGelieferteÖlmenge.Checked = false;
            rbNachgefüllteÖlmenge.Checked = false;

            txtGelieferteMenge.Visible = false;
            txtNachgefüllteMenge.Visible = false;

            txtAlteNummer.Clear();
            errorProvider1.SetError(txtAlteNummer, null);
            txtAlteNummer.FillColor = Color.White;

            txtParfümCode.Clear();
            errorProvider1.SetError(txtParfümCode, null);
            txtParfümCode.FillColor = Color.White;

            cbÖltype.SelectedIndex = -1;
            errorProvider1.SetError(cbÖltype, null);
            cbÖltype.FillColor = Color.White;

            txtAltuelleMenge.Clear();
            errorProvider1.SetError(txtAltuelleMenge, null);
            txtAltuelleMenge.FillColor = Color.White;

            txtNachgefüllteMenge.Clear();
            errorProvider1.SetError(txtNachgefüllteMenge, null);
            txtNachgefüllteMenge.FillColor = Color.White;

            txtGelieferteMenge.Clear();
            errorProvider1.SetError(txtGelieferteMenge, null);
            txtGelieferteMenge.FillColor = Color.White;

        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            _ResetDefaultValues();
        }

        private void rbGelieferteÖlmenge_CheckedChanged(object sender, EventArgs e)
        {
            txtNachgefüllteMenge.Visible = false;
            txtGelieferteMenge.Visible = true;
        }

        private void rbNachgefüllteÖlmenge_CheckedChanged(object sender, EventArgs e)
        {
            txtGelieferteMenge.Visible = false;
            txtNachgefüllteMenge.Visible = true;
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
            isValid = _TextFelderValidierung(txtAlteNummer, "Alte Nummer");
            isValid &= _TextFelderValidierung(txtParfümCode, "Parfümcode");
            isValid &= _ComboBoxValidierung(cbÖltype, "Öltype");
           // isValid &= _TextFelderValidierung(txtAltuelleMenge, "Aktuelle Menge");
            if (txtGelieferteMenge.Visible)
                isValid &= _TextFelderValidierung(txtGelieferteMenge, "Gelieferte Menge");
            if(txtNachgefüllteMenge.Visible)
                isValid &= _TextFelderValidierung(txtNachgefüllteMenge, "Nachgefüllte Menge");

            return isValid;
        }
        private void _LadenFlakonsdaten(int ID)
        {
         
            _duftÖle = clsDuftÖle.FindByID(ID);
            if (_duftÖle != null)
            {
                txtAlteNummer.Text = _duftÖle.AlteNummer.ToString();
                txtParfümCode.Text = _duftÖle.ParfümCode;
                txtAltuelleMenge.Text = _duftÖle.Ölmenge.ToString();
                cbÖltype.SelectedItem = _duftÖle.Öltype;
            }
        }

        private void dgvDuftÖle_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && dgvDuftÖle.Rows[e.RowIndex].Cells[0].Value != null)
            {
                txtAlteNummer.ReadOnly = true;
                txtParfümCode.ReadOnly = true;

                int ID = (int)dgvDuftÖle.Rows[e.RowIndex].Cells[0].Value;
                _LadenFlakonsdaten(ID);
                _mode = enMode.update; // Setzen Sie den Modus auf Update
            }
        }
        private bool _FillÖlMengedaten()
        {
          
            // 🔹 AlteNummer prüfen
            if (!int.TryParse(txtAlteNummer.Text.Trim(), out int alteNummer))
            {
                MessageBox.Show("Ungültige AlteNummer!");
                return false;
            }

            // 🔹 Öltype prüfen
            if (cbÖltype.SelectedItem == null)
            {
                MessageBox.Show("Bitte einen Öltyp auswählen!");
                return false;
            }

            // 🔹 Aktuelle Menge (wenn leer → 0)
            int aktuelleMenge = 0;
            if (!string.IsNullOrWhiteSpace(txtAltuelleMenge.Text))
            {
                if (!int.TryParse(txtAltuelleMenge.Text.Trim(), out aktuelleMenge))
                {
                    MessageBox.Show("Ungültige aktuelle Menge!");
                    return false;
                }
            }

            int neueMenge = aktuelleMenge;

            // 🔹 Gelieferte Menge
            if (rbGelieferteÖlmenge.Checked)
            {
                if (!int.TryParse(txtGelieferteMenge.Text.Trim(), out int geliefert))
                {
                    MessageBox.Show("Ungültige gelieferte Menge!");
                    return false;
                }

                neueMenge += geliefert;
            }

            // 🔹 Nachgefüllte Menge (Abzug)
            else if (rbNachgefüllteÖlmenge.Checked)
            {
                if (!int.TryParse(txtNachgefüllteMenge.Text.Trim(), out int nachgefüllt))
                {
                    MessageBox.Show("Ungültige nachgefüllte Menge!");
                    return false;
                }

                neueMenge -= nachgefüllt;
            }

            // 🔹 Negativen Bestand verhindern
            if (neueMenge < 0)
            {
                MessageBox.Show("Bestand darf nicht negativ sein!");
                return false;
            }

            // 🔹 Objekt füllen
            _duftÖle.AlteNummer = alteNummer;
            _duftÖle.ParfümCode = txtParfümCode.Text.Trim();
            _duftÖle.Öltype = cbÖltype.SelectedItem.ToString();
            _duftÖle.Ölmenge = neueMenge;
            _duftÖle.Aktivierungsdatum = DateTime.Now;

            return true;
        }
        private bool _DuftÖleDatenSpeichern()
        {
            if (!_SindEingabenValidiert())
                return false;

            // Sicherstellen, dass Objekt existiert
            if (_duftÖle == null)
                _duftÖle = new clsDuftÖle();

            if (!_FillÖlMengedaten())
                return false; // WICHTIG!

            if (_duftÖle.Ölmenge < 0)
            {
                MessageBox.Show("Bestand darf nicht negativ sein!");
                return false;
            }


            string statusMessage;
            if (_mode == enMode.addnew)
                statusMessage = "hinzugefügt";
            else
                statusMessage = "aktualisiert";

            if (!_duftÖle.Save())
            {
                MessageBox.Show($"Fehler beim {statusMessage} ist aufgetreten.", "Fehler",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);

                return false;
            }

            _LadeDuftÖledatenFromDatabase();
            _ResetDefaultValues();

            return true;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            _DuftÖleDatenSpeichern();
        }
        private void _EntferneParfüm(int? ID)
        {
            bool result = (MessageBox.Show("Sind Sie sicher, Sie möchten diesen Vorgang durchführen?", "Hinweis", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2) == DialogResult.OK);

            if (!result) return;

            clsDuftÖle DuftÖlDaten = clsDuftÖle.FindByID(ID);

            if (DuftÖlDaten != null && DuftÖlDaten.DeleteDuftÖle())
            {
                _LadeDuftÖledatenFromDatabase();
            }
            else
            {
                MessageBox.Show("DuftÖldaten wurden nicht gefunden, \nbitte versuchen Sie es erneut.", "Entfernung", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (dgvDuftÖle.CurrentRow != null)
            {
                int? flakonID = (int)dgvDuftÖle.CurrentRow.Cells[0].Value;
                _EntferneParfüm(flakonID);
            }
        }
        private void cbFilterby_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbFilterby.SelectedIndex != -1)
            {
                txtFilterwert.Clear();
                txtFilterwert.Focus();
            }
            else
            {
                txtFilterwert.Clear();
                _bindingSource.RemoveFilter();
            }
        }
        private void txtFilterwert_TextChanged(object sender, EventArgs e)
        {
            if (cbFilterby.SelectedIndex == -1)
                return;

            string filterSpalte = cbFilterby.SelectedItem.ToString();
            string filterWert = txtFilterwert.Text.Trim();

            if (string.IsNullOrEmpty(filterWert))
            {
                _bindingSource.RemoveFilter();
                return;
            }

            switch (filterSpalte)
            {
                case "AlteNummer":
                    if (int.TryParse(filterWert, out _))
                        _bindingSource.Filter = $"Convert(AlteNummer, 'System.String') LIKE '%{filterWert}%'";
                    break;

                case "ParfümCode":
                    _bindingSource.Filter = $"ParfümCode LIKE '%{filterWert}%'";
                    break;

                case "Öltype":
                    _bindingSource.Filter = $"Öltype LIKE '%{filterWert}%'";
                    break;
            }
        }
        private void txtFilterwert_KeyPress(object sender, KeyPressEventArgs e)
        {

            // Diese Methode wird aufgerufen, wenn eine Taste in der Textbox für Unisexdüfte gedrückt wird.
            if (cbFilterby.SelectedItem == null)
            {
                return;
            }

            string selectedItem = cbFilterby.SelectedItem.ToString();

            if (selectedItem == "AlteNummer")
            {
                e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
            }
        }
    }
}

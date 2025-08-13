using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Management.Instrumentation;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Busnisse_Layer;
using clsHilfsMethoden;
using Guna.UI2.WinForms;
using static clsHilfsMethoden.AutoComplete;

namespace BilsanParfums
{
    public partial class frmParfüms : Form
    {
        // Private Felder
        private DataTable _dtParfüms;
        private DataTable _dtHerrenParfüms;
        private DataTable _dtDamenParfüms;
        private DataTable _dtUnisexParfüms;
        private DataTable _dtKinderParfüms;

        private readonly object _dataloadLock = new object();
        private readonly object _filterLock = new object();

        private BindingSource _bindingSourceAlleParfüms;
        private BindingSource _bindingSourceHerrenParfüms;
        private BindingSource _bindingSourceDamenParfüms;
        private BindingSource _bindingSourceUnisexParfüms;
        private BindingSource _bindingSourceKinderParfüms;

        private readonly object _autoComplateLock = new object();
        // Deklariere den AVL-Baum als privates Feld
        private AVLTree _autoCompleteTree;

        public frmParfüms()
        {
            InitializeComponent();
            _bindingSourceAlleParfüms = new BindingSource();
            _bindingSourceHerrenParfüms = new BindingSource();
            _bindingSourceDamenParfüms = new BindingSource();
            _bindingSourceUnisexParfüms = new BindingSource();
            _bindingSourceKinderParfüms = new BindingSource();
        }

        private void frmParfüms_Load(object sender, EventArgs e)
        {
            // Setze den DrawMode des TabControls einmalig beim Laden
            tabControl1.DrawMode = TabDrawMode.OwnerDrawFixed;
            _LadeAlleParfümDaten();
            //_LadeFrauenParfümDaten();
            //_LadeHerrenParfümDaten();
            //_LadeKinderParfümDaten();
            //_LadeUnisexParfümDaten();
            // Initialisiere den AVL-Baum nur einmal beim Laden der Form
            _InitialisiereAvlBaum();
        }
        /// <summary>
        /// Lädt alle Parfüm-Daten und aktualisiert das DataGridView.
        /// </summary>
        private void _LadeAlleParfümDaten()
        {
            lock (_dataloadLock)
            {
                _dtParfüms = clsParfüms.GetAllParfüms();
                if (_dtParfüms != null && _dtParfüms.Rows.Count > 0)
                {
                    _bindingSourceAlleParfüms.DataSource = _dtParfüms;
                    dgvAlleParfüms.DataSource = _bindingSourceAlleParfüms;
                    _PasseDataGridViewSchriftAn(dgvAlleParfüms);
                    _MarkiereParfümZeilen(dgvAlleParfüms);
                }
            }
        }
        private void _LadeHerrenParfümDaten()
        {
            lock (_dataloadLock)
            {
                _dtHerrenParfüms = clsParfüms.GetAllHerrenParfüms();
                if (_dtHerrenParfüms != null && _dtHerrenParfüms.Rows.Count > 0)
                {
                    _bindingSourceHerrenParfüms.DataSource = _dtHerrenParfüms;
                    dgvHerrenParfüms.DataSource = _bindingSourceHerrenParfüms;
                    _PasseDataGridViewSchriftAn(dgvHerrenParfüms);
                    _MarkiereParfümZeilen(dgvHerrenParfüms);
                }
            }
        }
        private void _LadeFrauenParfümDaten()
        {
            lock (_dataloadLock)
            {
                // Annahme: Die Methode GetAllParfüms() lädt alle Parfüms.
                // Du benötigst eine Logik, um nur Frauenparfüms zu filtern.
                // Dies könnte eine separate Abfrage oder ein Filter auf dem bestehenden DataTable sein.
                _dtDamenParfüms = clsParfüms.GetAllDamenParfüms(); // Hier sollte eine Filter-Methode stehen.

                if (_dtDamenParfüms != null && _dtDamenParfüms.Rows.Count > 0)
                {
                    _bindingSourceDamenParfüms.DataSource = _dtDamenParfüms;
                    dgvDamenParfüms.DataSource = _bindingSourceDamenParfüms;
                    _PasseDataGridViewSchriftAn(dgvDamenParfüms);
                    _MarkiereParfümZeilen(dgvDamenParfüms);
                }
            }
        }

        private void _LadeUnisexParfümDaten()
        {
            lock (_dataloadLock)
            {
                // Filterlogik für Unisex-Parfüms
                _dtUnisexParfüms = clsParfüms.GetAllUnisexParfüms();

                if (_dtUnisexParfüms != null && _dtUnisexParfüms.Rows.Count > 0)
                {
                    _bindingSourceUnisexParfüms.DataSource = _dtUnisexParfüms;
                    dgvUnisexParfüms.DataSource = _bindingSourceUnisexParfüms;
                    _PasseDataGridViewSchriftAn(dgvUnisexParfüms);
                    _MarkiereParfümZeilen(dgvUnisexParfüms);
                }
            }
        }

        private void _LadeKinderParfümDaten()
        {
            lock (_dataloadLock)
            {
                // Filterlogik für Kinder-Parfüms
                _dtKinderParfüms = clsParfüms.GetAllKinderParfüms();

                if (_dtKinderParfüms != null && _dtKinderParfüms.Rows.Count > 0)
                {
                    _bindingSourceKinderParfüms.DataSource = _dtKinderParfüms;
                    dgvKinderParfüms.DataSource = _bindingSourceKinderParfüms;
                    _PasseDataGridViewSchriftAn(dgvKinderParfüms);
                    _MarkiereParfümZeilen(dgvKinderParfüms);
                }
            }
        }

        // --- Event-Handler ---

        private void tabControl1_DrawItem(object sender, DrawItemEventArgs e)
        {
            Color selectedColor = Color.DarkGray;
            Color unselectedColor = Color.LightGray;

            bool isSelected = e.Index == tabControl1.SelectedIndex;

            e.Graphics.FillRectangle(new SolidBrush(isSelected ? selectedColor : unselectedColor), e.Bounds);

            string tabText = tabControl1.TabPages[e.Index].Text;
            TextRenderer.DrawText(e.Graphics, tabText, tabControl1.Font, e.Bounds, Color.Black, TextFormatFlags.HorizontalCenter | TextFormatFlags.VerticalCenter);
        }

        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (tabControl1.SelectedTab == tabAllgemein)
            {
                _LadeAlleParfümDaten();
               // _MarkiereParfümZeilen(dgvAlleParfüms);
            }
            else if (tabControl1.SelectedTab == tabHerrendüfte)
            {
                _LadeHerrenParfümDaten();
                //_MarkiereParfümZeilen(dgvHerrenParfüms);
            }
            else if (tabControl1.SelectedTab == tabDamendüfte)
            {
                _LadeFrauenParfümDaten();
               // _MarkiereParfümZeilen(dgvDamenParfüms);
            }
            else if (tabControl1.SelectedTab == tabUnisexdüfte)
            {
                _LadeUnisexParfümDaten();
                //_MarkiereParfümZeilen(dgvUnisexParfüms);
            }
            else if (tabControl1.SelectedTab == tabKinderdüfte)
            {
                _LadeKinderParfümDaten();
               // _MarkiereParfümZeilen(dgvKinderParfüms);
            }
        }
        private void cbFilterby_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbFilterby.SelectedIndex != -1)
            {
                txtFilterwert.Clear();
                txtFilterwert.Focus();
            }
        }

        private void btnNeuesParfümHinzufügen_Click(object sender, EventArgs e)
        {
            _ÖffneAddUpdateForm(-1);
        }

        private void neueParfümHinzufügenToolStripMenuItem_Click(object sender, EventArgs e)
        {
            _ÖffneAddUpdateForm(-1);
        }

        private void aktualisiereBestehendesParfümToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (dgvAlleParfüms.CurrentRow != null)
            {
                int parfümNummer = (int)dgvAlleParfüms.CurrentRow.Cells[0].Value;
                _ÖffneAddUpdateForm(parfümNummer);
            }
        }

        private void entferneParfümToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (dgvAlleParfüms.CurrentRow != null)
            {
                _EntferneParfüm();
            }
        }

        private void dgvAlleParfüms_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && dgvAlleParfüms.Rows[e.RowIndex].Cells[2].Value != null)
            {
                string currentName = dgvAlleParfüms.Rows[e.RowIndex].Cells[2].Value.ToString();
                _ÖffneParfumoWebseite(currentName);
            }
        }

        // --- Private Methoden (Hilfsmethoden) ---

        /// <summary>
        /// Wendet den Filter basierend auf der Auswahl an.
        /// </summary>
        private void _FilterAnwenden(Guna2ComboBox filterComboBox, Guna2TextBox filterTextBox, 
            BindingSource bindingSource, DataGridView dgv)
        {
            lock (_filterLock)
            {
                string filterwert = filterTextBox.Text.Trim();

                if (filterComboBox.SelectedIndex == -1 || string.IsNullOrEmpty(filterwert))
                {
                    bindingSource.Filter = string.Empty;
                    return;
                }

                string spalteName = filterComboBox.SelectedItem.ToString();
                string filterString = "";

                switch (spalteName)
                {
                    case "ParfümNummer":
                        filterString = $"{spalteName} = {filterwert}";
                        break;
                    case "Name":
                    case "Marke":
                    case "Kategorie":
                    case "Basisnote":
                    case "Duftrichtung":
                        filterString = $"{spalteName} LIKE '%{filterwert}%'";
                        break;
                    default:
                        filterString = null;
                        break;
                }
                bindingSource.Filter = filterString;
            }
            // Call the highlighting method here, passing the correct DataGridView
            _MarkiereParfümZeilen(dgv);
        }

        /// <summary>
        /// Formatiert das DataGridView, um Spaltenbreite und Schriftart anzupassen.
        /// </summary>
        private void _PasseDataGridViewSchriftAn(DataGridView dgv)
        {
            // AutoSizeColumnsMode auf AllCells setzen, um die Spaltenbreite automatisch anzupassen
            //dgv.AutoResizeColumns(DataGridViewAutoSizeColumnsMode.AllCells);
            // Beispiel mit Segoe UI
            dgv.DefaultCellStyle.Font = new Font("Segoe UI", 13, FontStyle.Regular);
            dgv.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 14, FontStyle.Italic);

            // Beispiel mit Calibri
            //dgv.DefaultCellStyle.Font = new Font("Calibri", 13, FontStyle.Regular);
            //dgv.ColumnHeadersDefaultCellStyle.Font = new Font("Calibri", 14, FontStyle.Bold);
        }

        /// <summary>
        /// Markiert Zeilen im DataGridView basierend auf dem Wert "IstVorhanden".
        /// </summary>
        private void _MarkiereParfümZeilen(DataGridView dgv)
        {
            foreach (DataGridViewRow row in dgv.Rows)
            {
                if (row.Cells["IstVorhanden"].Value != null && Convert.ToBoolean(row.Cells["IstVorhanden"].Value) == true)
                {
                    row.DefaultCellStyle.BackColor = Color.PaleGreen;
                    row.DefaultCellStyle.ForeColor = Color.Black;
                }
            }

        }
        
        /// <summary>
        /// Öffnet die Hinzufügen-/Aktualisieren-Form.
        /// </summary>
        private void _ÖffneAddUpdateForm(int parfümNummer)
        {
            using (frmAddUpdateParfüms frm = new frmAddUpdateParfüms(parfümNummer))
            {
                frm.ShowDialog();
            }
            // Daten nach der Rückkehr aus dem Dialog neu laden
            _LadeAlleParfümDaten();
        }

        /// <summary>
        /// Entfernt ein Parfüm aus der Datenbank.
        /// </summary>
        private void _EntferneParfüm()
        {
            bool result = (MessageBox.Show("Sind Sie sicher, Sie möchten diesen Vorgang durchführen?", "Hinweis", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2) == DialogResult.OK);

            if (!result) return;

            int parfümNummer = (int)dgvAlleParfüms.CurrentRow.Cells[0].Value;
            clsParfüms parfuemDaten = clsParfüms.FindByParfümNummer(parfümNummer);

            if (parfuemDaten != null && parfuemDaten.Delete())
            {
                // TODO: Korrekte Handhabung des AVL-Baums prüfen, falls nötig
                // AutoComplete.AVLTree tree = new AutoComplete.AVLTree();
                // tree.Delete(parfuemDaten.Name);

                MessageBox.Show("Parfümdaten wurden erfolgreich entfernt", "Entfernung", MessageBoxButtons.OK, MessageBoxIcon.Information);
                _LadeAlleParfümDaten();
            }
            else
            {
                MessageBox.Show("Parfümdaten wurden nicht gefunden, \nbitte versuchen Sie es erneut.", "Entfernung", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        /// <summary>
        /// Öffnet die Parfumo-Webseite in einem neuen Browser-Tab.
        /// </summary>
        private void _ÖffneParfumoWebseite(string parfümName)
        {
            string url = $"https://www.parfumo.de/s_perfumes_x.php?in=1&filter={parfümName}";
            try
            {
                Process.Start("msedge", $"--new-tab \"{url}\"");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Fehler beim Öffnen des Browsers: " + ex.Message);
            }
        }

        // --- Neue Methode zur Initialisierung des Baumes ---
        private void _InitialisiereAvlBaum()
        {
            // Erstelle einen neuen Baum
            _autoCompleteTree = new AVLTree();

            // Füge alle Namen nur einmal in den Baum ein, falls die Daten vorhanden sind
            if (_dtParfüms != null)
            {
                foreach (DataRow row in _dtParfüms.Rows)
                {
                    _autoCompleteTree.Insert(row["Name"].ToString());
                }
            }
        }
        private void _FühreAutoCompleteAus(Guna2TextBox filterTextBox, ListBox suggestionsListBox)
        {
            string prefix = filterTextBox.Text.Trim();

            if (_autoCompleteTree != null)
            {
                var completions = _autoCompleteTree.AutoComplete(prefix);
                _UpdateAutoCompleteListeBox(completions, filterTextBox, suggestionsListBox);
            }
        }
        private void _UpdateAutoCompleteListeBox(IEnumerable<string> completions, Guna2TextBox filterTextBox, ListBox suggestionsListBox)
        {
            lock (_autoComplateLock)
            {
                suggestionsListBox.Items.Clear();

                foreach (var suggestion in completions)
                {
                    suggestionsListBox.Items.Add(suggestion.ToString());
                }

                if (!string.IsNullOrEmpty(filterTextBox.Text.Trim()))
                {
                    suggestionsListBox.Visible = suggestionsListBox.Items.Count > 0;
                    if (suggestionsListBox.Visible)
                    {
                       suggestionsListBox.SelectedIndex = -1; // Wähle den ersten Eintrag aus
                        filterTextBox.Focus(); // Setze den Fokus auf die ListBox
                    }
                }
                else
                {
                    suggestionsListBox.Items.Clear();
                    suggestionsListBox.Visible = false;
                }
            }
        }
        private void _WähleVorschlagAus(Guna2ComboBox filterComboBox, Guna2TextBox filterTextBox, ListBox suggestionsListBox,
            BindingSource bindingSource, DataGridView dgv)
        {
            if (suggestionsListBox.SelectedItem != null)
            {
                filterTextBox.Text = suggestionsListBox.SelectedItem.ToString();
                suggestionsListBox.Visible = false;

                // Hier wird die ComboBox korrekt übergeben
                _FilterAnwenden(filterComboBox, filterTextBox, bindingSource,dgv);
            }
            else
            {
                MessageBox.Show("Kein gültiger Eintrag ausgewählt,\nBitte wählen Sie einen gültigen Eintrag aus", "Hinweis", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

            filterTextBox.SelectionStart = filterTextBox.Text.Length;
            filterTextBox.SelectionLength = 0;
            filterTextBox.Focus();
        }

        // --- Evtent Handler für alle Parfüms (Haupt-Tab) ---
        private void txtFilterwert_TextChanged(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtFilterwert.Text))
            {
                _bindingSourceAlleParfüms.Filter = string.Empty;
                // WICHTIG: Rufe _MarkiereParfümZeilen direkt hier auf, wenn der Filter leer ist.
                _MarkiereParfümZeilen(dgvAlleParfüms);
            }

            if (cbFilterby.SelectedItem?.ToString() == "Name")
            {
                _FühreAutoCompleteAus(txtFilterwert, lbVorschläge);
            }
            else
            {
                lbVorschläge.Visible = false;
                _FilterAnwenden(cbFilterby, txtFilterwert, _bindingSourceAlleParfüms,dgvAlleParfüms);
            }
            // Entferne diesen doppelten Aufruf, da er jetzt in den if/else-Blöcken passiert.
            // _MarkiereParfümZeilen(dgvAlleParfüms);
        }

        private void lbVorschläge_Click(object sender, EventArgs e)
        {
            _WähleVorschlagAus(cbFilterby,txtFilterwert, lbVorschläge, _bindingSourceAlleParfüms,dgvAlleParfüms);
        }

        private void txtFilterwert_KeyPress(object sender, KeyPressEventArgs e)
        {
            string selectedItem = cbFilterby.SelectedItem.ToString();

            if (selectedItem == "ParfümNummer")
                e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }

        private void lbVorschläge_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                _WähleVorschlagAus(cbFilterby, txtFilterwert, lbVorschläge, _bindingSourceAlleParfüms, dgvAlleParfüms);
                e.Handled = true;
            }
            else if (e.KeyCode == Keys.Escape)
            {
                lbVorschläge.Visible = false;
                txtFilterwert.Focus();
                e.Handled = true;
            }
        }

        private void txtFilterwert_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Down && lbVorschläge.Visible && lbVorschläge.Items.Count > 0)
            {
                lbVorschläge.Focus();
                e.Handled = true;
            }
        }

        // --- Event Handler für Damendüfte ---

        private void cbDamenFilterby_KeyPress(object sender, KeyPressEventArgs e)
        {
            string selectedItem = cbDamenFilterby.SelectedItem.ToString();

            if (selectedItem == "ParfümNummer")
                e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }
        private void cbDamenFilterby_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbDamenFilterby.SelectedIndex != -1)
            {
                txtDamenFilterwert.Clear();
                txtDamenFilterwert.Focus();
            }
        }
        private void txtDamenFilterwert_TextChanged(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtDamenFilterwert.Text))
            {
                _bindingSourceDamenParfüms.Filter = string.Empty;
                _MarkiereParfümZeilen(dgvDamenParfüms);
            }

            // Ich nehme an, dass es eine lbVorschlägeFürDamen gibt
            if (cbDamenFilterby.SelectedItem?.ToString() == "Name")
            {
                _FühreAutoCompleteAus(txtDamenFilterwert, lbVorschlägeFürDamen);
            }
            else
            {
                lbVorschlägeFürDamen.Visible = false;
                _FilterAnwenden(cbDamenFilterby, txtDamenFilterwert, _bindingSourceDamenParfüms,dgvDamenParfüms);
            }
           // _MarkiereParfümZeilen(dgvDamenParfüms);
        }
        private void lbVorschlägeFürDamen_Click(object sender, EventArgs e)
        {
            _WähleVorschlagAus(cbDamenFilterby, txtDamenFilterwert, lbVorschlägeFürDamen, _bindingSourceDamenParfüms,dgvDamenParfüms);
        }

        private void lbVorschlägeFürDamen_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                _WähleVorschlagAus(cbDamenFilterby, txtDamenFilterwert, lbVorschlägeFürDamen, _bindingSourceDamenParfüms,dgvDamenParfüms);
                e.Handled = true;
            }
            else if (e.KeyCode == Keys.Escape)
            {
                lbVorschlägeFürDamen.Visible = false;
                txtDamenFilterwert.Focus();
                e.Handled = true;
            }
        }

        private void txtDamenFilterwert_KeyDown(object sender, KeyEventArgs e)
        {
            // Die Methode wird ausgelöst, sobald eine Taste in der Textbox gedrückt wird.
            // Das 'e' Objekt enthält Informationen über die gedrückte Taste.

            // Wir prüfen, ob der Benutzer die Pfeil-nach-unten-Taste drückt UND
            // ob die Liste der Vorschläge sichtbar ist UND
            // ob die Liste mindestens ein Element enthält.
            // Nur wenn alle drei Bedingungen erfüllt sind, wird der Codeblock ausgeführt.
            if (e.KeyCode == Keys.Down && lbVorschlägeFürDamen.Visible && lbVorschlägeFürDamen.Items.Count > 0)
            {
                // Wenn die Bedingungen oben wahr sind, setzen wir den Fokus von der Textbox
                // auf die Listbox. Der Benutzer kann jetzt mit den Pfeiltasten
                // in der Vorschlagsliste navigieren.
                lbVorschlägeFürDamen.Focus();

                // Wir setzen 'e.Handled' auf true. Das signalisiert, dass wir das Tastendruck-Ereignis
                // bereits verarbeitet haben. Dies verhindert, dass das Standardverhalten der Textbox
                // (z. B. das Bewegen des Cursors) ausgelöst wird.
                e.Handled = true;
            }
        }

        // --- Event Handler für Herrendüfte ---
        private void txtHerrenFilterwert_KeyPress(object sender, KeyPressEventArgs e)
        {
            string selectedItem = cbHerrenFilterby.SelectedItem.ToString();

            if (selectedItem == "ParfümNummer")
                e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }
        private void cbHerrenFilterby_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbHerrenFilterby.SelectedIndex != -1)
            {
                txtHerrenFilterwert.Clear();
                txtHerrenFilterwert.Focus();
            }
        }
        private void txtHerrenFilterwert_TextChanged(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtHerrenFilterwert.Text))
            {
                _bindingSourceHerrenParfüms.Filter = string.Empty;
                _MarkiereParfümZeilen(dgvHerrenParfüms);
            }

            // Ich nehme an, dass es eine lbVorschlägeFürHerren gibt
            if (cbHerrenFilterby.SelectedItem?.ToString() == "Name")
            {
                _FühreAutoCompleteAus(txtHerrenFilterwert, lbVorschälgeFürHerrendüfte);
            }
            else
            {
                lbVorschälgeFürHerrendüfte.Visible = false;
                _FilterAnwenden(cbHerrenFilterby, txtHerrenFilterwert, _bindingSourceHerrenParfüms,dgvHerrenParfüms);
            }
            //_MarkiereParfümZeilen(dgvHerrenParfüms);
        }
        private void lbVorschälgeFürHerrendüfte_Click(object sender, EventArgs e)
        {
            _WähleVorschlagAus(cbHerrenFilterby, txtHerrenFilterwert, lbVorschälgeFürHerrendüfte, _bindingSourceHerrenParfüms,dgvHerrenParfüms);
        }

        private void lbVorschälgeFürHerrendüfte_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                _WähleVorschlagAus(cbHerrenFilterby, txtHerrenFilterwert, lbVorschälgeFürHerrendüfte, _bindingSourceHerrenParfüms,dgvHerrenParfüms);
                e.Handled = true;
            }
            else if (e.KeyCode == Keys.Escape)
            {
                lbVorschälgeFürHerrendüfte.Visible = false;
                txtHerrenFilterwert.Focus();
                e.Handled = true;
            }
        }

        private void txtHerrenFilterwert_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Down && lbVorschälgeFürHerrendüfte.Visible && lbVorschälgeFürHerrendüfte.Items.Count > 0)
            {
                // Wenn die Bedingungen oben wahr sind, setzen wir den Fokus von der Textbox
                // auf die Listbox. Der Benutzer kann jetzt mit den Pfeiltasten
                // in der Vorschlagsliste navigieren.
                lbVorschälgeFürHerrendüfte.Focus();

                // Wir setzen 'e.Handled' auf true. Das signalisiert, dass wir das Tastendruck-Ereignis
                // bereits verarbeitet haben. Dies verhindert, dass das Standardverhalten der Textbox
                // (z. B. das Bewegen des Cursors) ausgelöst wird.
                e.Handled = true;
            }
        }



        // --- Event Handler für Unisexdüfte ---
        private void txtUnisexFilterwert_KeyPress(object sender, KeyPressEventArgs e)
        {
            string selectedItem = cbUnisexFilterby.SelectedItem.ToString();

            if (selectedItem == "ParfümNummer")
                e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }
        private void cbUnisexFilterby_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbUnisexFilterby.SelectedIndex != -1)
            {
                txtUnisexFilterwert.Clear();
                txtUnisexFilterwert.Focus();
            }
        }
        private void txtUnisexFilterwert_TextChanged(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtUnisexFilterwert.Text))
            {
                _bindingSourceUnisexParfüms.Filter = string.Empty;
                _MarkiereParfümZeilen(dgvUnisexParfüms);
            }

            // Ich nehme an, dass es eine lbVorschlägeFürUnisex gibt
            if (cbUnisexFilterby.SelectedItem?.ToString() == "Name")
            {
                _FühreAutoCompleteAus(txtUnisexFilterwert, lbVorschlägeFürUnisexdüfte);
            }
            else
            {
                lbVorschlägeFürUnisexdüfte.Visible = false;
                _FilterAnwenden(cbUnisexFilterby, txtUnisexFilterwert, _bindingSourceUnisexParfüms,dgvUnisexParfüms);
            }
           // _MarkiereParfümZeilen(dgvUnisexParfüms);
        }

        private void txtUnisexFilterwert_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Down && lbVorschlägeFürUnisexdüfte.Visible && lbVorschlägeFürUnisexdüfte.Items.Count > 0)
            {
                // Wenn die Bedingungen oben wahr sind, setzen wir den Fokus von der Textbox
                // auf die Listbox. Der Benutzer kann jetzt mit den Pfeiltasten
                // in der Vorschlagsliste navigieren.
                lbVorschlägeFürUnisexdüfte.Focus();

                // Wir setzen 'e.Handled' auf true. Das signalisiert, dass wir das Tastendruck-Ereignis
                // bereits verarbeitet haben. Dies verhindert, dass das Standardverhalten der Textbox
                // (z. B. das Bewegen des Cursors) ausgelöst wird.
                e.Handled = true;
            }
        }
    }
}
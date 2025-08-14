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
        private DataTable _dtOrientalischeParfüms;

        private readonly object _dataloadLock = new object();
        private readonly object _filterLock = new object();

        private BindingSource _bindingSourceAlleParfüms;
        private BindingSource _bindingSourceHerrenParfüms;
        private BindingSource _bindingSourceDamenParfüms;
        private BindingSource _bindingSourceUnisexParfüms;
        private BindingSource _bindingSourceKinderParfüms;
        private BindingSource _bindingSourceOrientalischeParfüms;

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
            _bindingSourceOrientalischeParfüms = new BindingSource();
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
                    _AktualisiereAlleParfümdatenAnzahl(_bindingSourceAlleParfüms);
                    _PasseDataGridViewSchriftAn(dgvAlleParfüms);
                    _MarkiereParfümZeilen(dgvAlleParfüms);
                }
            }
        }
        private void _AktualisiereAlleParfümdatenAnzahl(BindingSource bgs)
        {
            lblAlleParfümsnazahl.Text = bgs.Count.ToString();
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
                    _AktualisiereHerrenParfümdatenAnzahl(_bindingSourceHerrenParfüms);
                    _PasseDataGridViewSchriftAn(dgvHerrenParfüms);
                    _MarkiereParfümZeilen(dgvHerrenParfüms);
                }
            }
        }
        private void _AktualisiereHerrenParfümdatenAnzahl(BindingSource bgs)
        {
            lblHerrenParfümsnazahl.Text = bgs.Count.ToString();
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
                    _AktualisiereFrauenParfümdatenAnzahl(_bindingSourceDamenParfüms);
                    _PasseDataGridViewSchriftAn(dgvDamenParfüms);
                    _MarkiereParfümZeilen(dgvDamenParfüms);
                }
            }
        }
        private void _AktualisiereFrauenParfümdatenAnzahl(BindingSource bgs)
        {
            lblDamenParfümsnazahl.Text = bgs.Count.ToString();
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
                    _AktualisiereUnisexParfümdatenAnzahl(_bindingSourceUnisexParfüms);
                    _PasseDataGridViewSchriftAn(dgvUnisexParfüms);
                    _MarkiereParfümZeilen(dgvUnisexParfüms);
                }
            }
        }
        private void _AktualisiereUnisexParfümdatenAnzahl(BindingSource bgs)
        {
            lblUnisexParfümsnazahl.Text = bgs.Count.ToString();
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
                    _AktualisiereKinderParfümdatenAnzahl(_bindingSourceKinderParfüms);
                    _PasseDataGridViewSchriftAn(dgvKinderParfüms);
                    _MarkiereParfümZeilen(dgvKinderParfüms);
                }
            }
        }
        private void _AktualisiereKinderParfümdatenAnzahl(BindingSource bgs)
        {
            lblKinderParfümanzahl.Text = bgs.Count.ToString();
        }
        private void _LadeOrientalischeParfümDaten()
        {
            lock (_dataloadLock)
            {
                // Filterlogik für Kinder-Parfüms
                _dtOrientalischeParfüms = clsParfüms.GetAllOrientalischeParfüms();

                if (_dtOrientalischeParfüms != null && _dtOrientalischeParfüms.Rows.Count > 0)
                {
                    _bindingSourceOrientalischeParfüms.DataSource = _dtOrientalischeParfüms;
                    dgvOrientalischeParfüms.DataSource = _bindingSourceOrientalischeParfüms;
                    _AktualisiereOrientalischParfümdatenAnzahl(_bindingSourceOrientalischeParfüms);
                    _PasseDataGridViewSchriftAn(dgvOrientalischeParfüms);
                    _MarkiereParfümZeilen(dgvOrientalischeParfüms);
                }
            }
        }
        private void _AktualisiereOrientalischParfümdatenAnzahl(BindingSource bgs)
        {
            lblOrientalischParfümsnazahl.Text = bgs.Count.ToString();
        }

        private void _AktualisiereParfümAnzahlFüeSelectedTabpage(BindingSource bgs)
        {
            if (tabControl1.SelectedTab == tabAllgemein)
            {
                _AktualisiereAlleParfümdatenAnzahl(bgs);
            }
            else if (tabControl1.SelectedTab == tabHerrendüfte)
            {
                _AktualisiereHerrenParfümdatenAnzahl(bgs);
            }
            else if (tabControl1.SelectedTab == tabDamendüfte)
            {
                _AktualisiereFrauenParfümdatenAnzahl(bgs);
            }
            else if (tabControl1.SelectedTab == tabUnisexdüfte)
            {
                _AktualisiereUnisexParfümdatenAnzahl(bgs);
            }
            else if (tabControl1.SelectedTab == tabKinderdüfte)
            {
                _AktualisiereKinderParfümdatenAnzahl(bgs);
            }
            else if (tabControl1.SelectedTab == tabOrientalischedüfte)
            {
                _AktualisiereOrientalischParfümdatenAnzahl(bgs);
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
            else if (tabControl1.SelectedTab == tabOrientalischedüfte)
            {
                _LadeOrientalischeParfümDaten();
                // _MarkiereParfümZeilen(dgvKinderParfüms);
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
                int parfümNummer = (int)dgvAlleParfüms.CurrentRow.Cells[0].Value;
                _EntferneParfüm(parfümNummer);
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
            _AktualisiereParfümAnzahlFüeSelectedTabpage(bindingSource);
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
            // Rufen Sie die Methode auf, um den ausgewählten Tab zu aktualisieren
            _AktualisiereDatenNachTab();
        }
        private void _AktualisiereDatenNachTab()
        {
            if (tabControl1.SelectedTab == tabAllgemein)
            {
                _LadeAlleParfümDaten();
            }
            else if (tabControl1.SelectedTab == tabHerrendüfte)
            {
                _LadeHerrenParfümDaten();
            }
            else if (tabControl1.SelectedTab == tabDamendüfte)
            {
                _LadeFrauenParfümDaten();
            }
            else if (tabControl1.SelectedTab == tabUnisexdüfte)
            {
                _LadeUnisexParfümDaten();
            }
            else if (tabControl1.SelectedTab == tabKinderdüfte)
            {
                _LadeKinderParfümDaten();
            }
            else if (tabControl1.SelectedTab == tabOrientalischedüfte)
            {
                _LadeOrientalischeParfümDaten();
            }
        }
        /// <summary>
        /// Entfernt ein Parfüm aus der Datenbank.
        /// </summary>
        private void _EntferneParfüm(int parfümNummer)
        {
            bool result = (MessageBox.Show("Sind Sie sicher, Sie möchten diesen Vorgang durchführen?", "Hinweis", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2) == DialogResult.OK);

            if (!result) return;

            clsParfüms parfuemDaten = clsParfüms.FindByParfümNummer(parfümNummer);

            if (parfuemDaten != null && parfuemDaten.Delete())
            {
                // TODO: Korrekte Handhabung des AVL-Baums prüfen, falls nötig
                // AutoComplete.AVLTree tree = new AutoComplete.AVLTree();
                // tree.Delete(parfuemDaten.Name);

                MessageBox.Show("Parfümdaten wurden erfolgreich entfernt", "Entfernung", MessageBoxButtons.OK, MessageBoxIcon.Information);
                _AktualisiereDatenNachTab();
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



        //#############################################################//
        // --- Evtent Handler für alle Parfüms (Haupt-Tab) ---
        private void cbFilterby_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbFilterby.SelectedIndex != -1)
            {
                txtFilterwert.Clear();
                txtFilterwert.Focus();
            }
        }
        private void txtFilterwert_TextChanged(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtFilterwert.Text))
            {
                _bindingSourceAlleParfüms.Filter = string.Empty;
                // WICHTIG: Die Markierung sollte hier nicht doppelt sein
                _MarkiereParfümZeilen(dgvAlleParfüms);
                _AktualisiereParfümAnzahlFüeSelectedTabpage(_bindingSourceAlleParfüms);
            }

            // Prüfen, ob der "Name"-Filter ausgewählt ist
            if (cbFilterby.SelectedItem?.ToString() == "Name")
            {
                // Nur wenn der "Name"-Filter aktiv ist, die AutoComplete-Logik ausführen
                _FühreAutoCompleteAus(txtFilterwert, lbVorschlägeFürAlleParfüms);
                // Die Sichtbarkeit wird in _FühreAutoCompleteAus gesteuert
            }
            else
            {
                // Wenn ein anderer Filter ausgewählt ist, die Vorschläge ausblenden
                lbVorschlägeFürAlleParfüms.Visible = false;
                // Den Filter für die anderen Spalten anwenden
                _FilterAnwenden(cbFilterby, txtFilterwert, _bindingSourceAlleParfüms, dgvAlleParfüms);
            }
        }
        private void lbVorschlägeFürAlleParfüms_Click(object sender, EventArgs e)
        {
            _WähleVorschlagAus(cbFilterby, txtFilterwert, lbVorschlägeFürAlleParfüms, _bindingSourceAlleParfüms, dgvAlleParfüms);
        }
        private void lbVorschlägeFürAlleParfüms_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                _WähleVorschlagAus(cbFilterby, txtFilterwert, lbVorschlägeFürAlleParfüms, _bindingSourceAlleParfüms, dgvAlleParfüms);
                e.Handled = true;
            }
            else if (e.KeyCode == Keys.Escape)
            {
                lbVorschlägeFürAlleParfüms.Visible = false;
                txtFilterwert.Focus();
                e.Handled = true;
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

            if (selectedItem == "ParfümNummer")
            {
                e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
            }
        }

        private void txtFilterwert_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Down && lbVorschlägeFürAlleParfüms.Visible && lbVorschlägeFürAlleParfüms.Items.Count > 0)
            {
                lbVorschlägeFürAlleParfüms.Focus();
                e.Handled = true;
            }
        }
        private void dgvAlleParfüms_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            _MarkiereParfümZeilen(dgvAlleParfüms);
        }


        //#############################################################//
        // --- Event Handler für Damendüfte ---

        private void cbDamenFilterby_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Diese Methode wird aufgerufen, wenn eine Taste in der Textbox für Damendüfte gedrückt wird.
            if (cbDamenFilterby.SelectedItem == null)
            {
                return;
            }

            string selectedItem = cbDamenFilterby.SelectedItem.ToString();

            if (selectedItem == "ParfümNummer")
            {
                e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
            }
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
                _AktualisiereParfümAnzahlFüeSelectedTabpage(_bindingSourceDamenParfüms);
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
        private void btnParfümhinzufügen_Click(object sender, EventArgs e)
        {
            _ÖffneAddUpdateForm(-1);
        }
        private void neuesParfümHinzufügenToolStripMenuItem_Click(object sender, EventArgs e)
        {
            _ÖffneAddUpdateForm(-1);
        }

        private void bestehendesParfümAktualisierenToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (dgvDamenParfüms.CurrentRow != null)
            {
                int parfümNummer = (int)dgvDamenParfüms.CurrentRow.Cells[0].Value;
                _ÖffneAddUpdateForm(parfümNummer);
            }
        }

        private void entferneParfümToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            if (dgvDamenParfüms.CurrentRow != null)
            {
                int parfümNummer = (int)dgvDamenParfüms.CurrentRow.Cells[0].Value;
                _EntferneParfüm(parfümNummer);
            }
        }
        private void dgvDamenParfüms_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            _MarkiereParfümZeilen(dgvDamenParfüms);
        }
        private void dgvDamenParfüms_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && dgvDamenParfüms.Rows[e.RowIndex].Cells[2].Value != null)
            {
                string currentName = dgvDamenParfüms.Rows[e.RowIndex].Cells[2].Value.ToString();
                _ÖffneParfumoWebseite(currentName);
            }
        }



        //#############################################################//
        // --- Event Handler für Herrendüfte ---
        private void txtHerrenFilterwert_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Diese Methode wird aufgerufen, wenn eine Taste in der Textbox für Herrendüfte gedrückt wird.
            // Sie verhindert, dass Nicht-Zahlen eingegeben werden, wenn der Filter "ParfümNummer" ist.

            // Wichtig: Prüfen, ob überhaupt ein Element in der ComboBox ausgewählt ist,
            // bevor SelectedItem.ToString() aufgerufen wird, um NullReferenceException zu vermeiden.
            if (cbHerrenFilterby.SelectedItem == null)
            {
                // Wenn nichts ausgewählt ist, wird die Eingabe nicht eingeschränkt.
                // Sie könnten hier auch 'e.Handled = true;' setzen, um jegliche Eingabe zu blockieren,
                // wenn kein Filter ausgewählt wurde – abhängig von Ihrem gewünschten Verhalten.
                return;
            }

            string selectedItem = cbHerrenFilterby.SelectedItem.ToString();

            // Wenn der ausgewählte Filter "ParfümNummer" ist, nur Ziffern und Steuertasten zulassen.
            if (selectedItem == "ParfümNummer")
            {
                e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
            }
            // Andernfalls (bei anderen Filtern wie "Name"), keine Einschränkung der Eingabe.
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
                _AktualisiereParfümAnzahlFüeSelectedTabpage(_bindingSourceHerrenParfüms);
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
        private void btnParfünhinzufügen_Click(object sender, EventArgs e)
        {
            _ÖffneAddUpdateForm(-1);
        }
        private void neuesParfümsHinzufügenToolStripMenuItem_Click(object sender, EventArgs e)
        {
            _ÖffneAddUpdateForm(-1);
        }

        private void aktualisiereBestehendesParfümToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            if (dgvHerrenParfüms.CurrentRow != null)
            {
                int parfümNummer = (int)dgvHerrenParfüms.CurrentRow.Cells[0].Value;
                _ÖffneAddUpdateForm(parfümNummer);
            }
        }

        private void entfernToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (dgvHerrenParfüms.CurrentRow != null)
            {
                int parfümNummer = (int)dgvHerrenParfüms.CurrentRow.Cells[0].Value;
                _EntferneParfüm(parfümNummer);
            }
        }
        private void dgvHerrenParfüms_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            _MarkiereParfümZeilen(dgvHerrenParfüms);
        }
        private void dgvHerrenParfüms_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && dgvHerrenParfüms.Rows[e.RowIndex].Cells[2].Value != null)
            {
                string currentName = dgvHerrenParfüms.Rows[e.RowIndex].Cells[2].Value.ToString();
                _ÖffneParfumoWebseite(currentName);
            }
        }


        //#############################################################//
        // --- Event Handler für Unisexdüfte ---
        private void txtUnisexFilterwert_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Diese Methode wird aufgerufen, wenn eine Taste in der Textbox für Unisexdüfte gedrückt wird.
            if (cbUnisexFilterby.SelectedItem == null)
            {
                return;
            }

            string selectedItem = cbUnisexFilterby.SelectedItem.ToString();

            if (selectedItem == "ParfümNummer")
            {
                e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
            }
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
                _AktualisiereParfümAnzahlFüeSelectedTabpage(_bindingSourceUnisexParfüms);
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
        private void lbVorschlägeFürUnisexdüfte_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                _WähleVorschlagAus(cbUnisexFilterby, txtUnisexFilterwert, lbVorschlägeFürUnisexdüfte, _bindingSourceUnisexParfüms, dgvUnisexParfüms);
                e.Handled = true;
            }
            else if (e.KeyCode == Keys.Escape)
            {
                lbVorschlägeFürUnisexdüfte.Visible = false;
                txtUnisexFilterwert.Focus();
                e.Handled = true;
            }
        }

        private void lbVorschlägeFürUnisexdüfte_Click(object sender, EventArgs e)
        {
            _WähleVorschlagAus(cbUnisexFilterby, txtUnisexFilterwert, lbVorschlägeFürUnisexdüfte, _bindingSourceUnisexParfüms, dgvUnisexParfüms);
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
        private void btnHinzufügen_Click(object sender, EventArgs e)
        {
            _ÖffneAddUpdateForm(-1);
        }
        private void neuesParfümHinzufügenToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            _ÖffneAddUpdateForm(-1);
        }

        private void aktualisiereBestehendesParfümToolStripMenuItem2_Click(object sender, EventArgs e)
        {
            if (dgvUnisexParfüms.CurrentRow != null)
            {
                int parfümNummer = (int)dgvUnisexParfüms.CurrentRow.Cells[0].Value;
                _ÖffneAddUpdateForm(parfümNummer);
            }
        }

        private void entferneParfümToolStripMenuItem2_Click(object sender, EventArgs e)
        {
            if (dgvUnisexParfüms.CurrentRow != null)
            {
                int parfümNummer = (int)dgvUnisexParfüms.CurrentRow.Cells[0].Value;
                _EntferneParfüm(parfümNummer);
            }
        }

        private void dgvUnisexParfüms_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            _MarkiereParfümZeilen(dgvUnisexParfüms);
        }

        private void dgvUnisexParfüms_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && dgvUnisexParfüms.Rows[e.RowIndex].Cells[2].Value != null)
            {
                string currentName = dgvUnisexParfüms.Rows[e.RowIndex].Cells[2].Value.ToString();
                _ÖffneParfumoWebseite(currentName);
            }
        }



        //#############################################################//
        // --- Event Handler für Orientalischdüfte ---
        private void cbOrientalischFilterby_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (txtOrientalischFilterwert == null) return; // Zusätzliche Absicherung

            if (cbOrientalischFilterby.SelectedIndex != -1)
            {
                txtOrientalischFilterwert.Clear();
                txtOrientalischFilterwert.ReadOnly = false;
                txtOrientalischFilterwert.Focus();
            }
            else
            {
                txtOrientalischFilterwert.Clear();
                txtOrientalischFilterwert.ReadOnly = true;
            }
        }

        private void txtOrientalischFilterwert_TextChanged(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtOrientalischFilterwert.Text))
            {
                _bindingSourceOrientalischeParfüms.Filter = string.Empty;
                _MarkiereParfümZeilen(dgvOrientalischeParfüms);
                _AktualisiereParfümAnzahlFüeSelectedTabpage(_bindingSourceOrientalischeParfüms);
            }

            // Ich nehme an, dass es eine lbVorschlägeFürUnisex gibt
            if (cbOrientalischFilterby.SelectedItem?.ToString() == "Name")
            {
                _FühreAutoCompleteAus(txtOrientalischFilterwert, lbVorschlägeFürOrientalischedüfte);
            }
            else
            {
                lbVorschlägeFürOrientalischedüfte.Visible = false;
                _FilterAnwenden(cbOrientalischFilterby, txtOrientalischFilterwert, _bindingSourceOrientalischeParfüms, dgvOrientalischeParfüms);
            }
            // _MarkiereParfümZeilen(dgvUnisexParfüms);
        }

        private void txtOrientalischFilterwert_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Prüfen, ob überhaupt ein Element in der ComboBox ausgewählt ist
            if (cbOrientalischFilterby.SelectedItem == null)
            {
                // Wenn nichts ausgewählt ist, wird die Eingabe nicht eingeschränkt.
                // Sie können hier auch 'e.Handled = true;' setzen, um jegliche Eingabe zu blockieren,
                // wenn kein Filter ausgewählt wurde. Das hängt von Ihrem gewünschten Verhalten ab.
                return; // Methode beenden, da keine Filterregel angewendet werden kann
            }

            // Jetzt wissen wir, dass SelectedItem nicht null ist und können ToString() aufrufen
            string selectedItem = cbOrientalischFilterby.SelectedItem.ToString();

            if (selectedItem == "ParfümNummer")
            {
                // Nur Zahlen, Backspace, Delete etc. zulassen
                e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
            }
            // Wichtig: Wenn 'selectedItem' nicht "ParfümNummer" ist, sollte 'e.Handled' NICHT gesetzt werden,
            // damit normale Texteingabe (Buchstaben, Symbole) möglich ist.
        }

        private void lbVorschlägeFürOrientalischedüfte_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                _WähleVorschlagAus(cbOrientalischFilterby, txtOrientalischFilterwert, lbVorschlägeFürOrientalischedüfte, _bindingSourceOrientalischeParfüms, dgvOrientalischeParfüms);
                e.Handled = true;
            }
            else if (e.KeyCode == Keys.Escape)
            {
                lbVorschlägeFürOrientalischedüfte.Visible = false;
                txtOrientalischFilterwert.Focus();
                e.Handled = true;
            }
        }

        private void lbVorschlägeFürOrientalischedüfte_Click(object sender, EventArgs e)
        {
            _WähleVorschlagAus(cbOrientalischFilterby, txtOrientalischFilterwert, lbVorschlägeFürOrientalischedüfte, _bindingSourceOrientalischeParfüms, dgvOrientalischeParfüms);
        }

        private void txtOrientalischFilterwert_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Down && lbVorschlägeFürOrientalischedüfte.Visible && lbVorschlägeFürOrientalischedüfte.Items.Count > 0)
            {
                // Wenn die Bedingungen oben wahr sind, setzen wir den Fokus von der Textbox
                // auf die Listbox. Der Benutzer kann jetzt mit den Pfeiltasten
                // in der Vorschlagsliste navigieren.
                lbVorschlägeFürOrientalischedüfte.Focus();

                // Wir setzen 'e.Handled' auf true. Das signalisiert, dass wir das Tastendruck-Ereignis
                // bereits verarbeitet haben. Dies verhindert, dass das Standardverhalten der Textbox
                // (z. B. das Bewegen des Cursors) ausgelöst wird.
                e.Handled = true;
            }
        }

        private void btnOrientalischeduftHinzufügen_Click(object sender, EventArgs e)
        {
            _ÖffneAddUpdateForm(-1);
        }

        private void neuesParfümHinzufügenToolStripMenuItem2_Click(object sender, EventArgs e)
        {
            _ÖffneAddUpdateForm(-1);
        }

        private void aktualisiereBestehendesParfümToolStripMenuItem3_Click(object sender, EventArgs e)
        {
            if (dgvOrientalischeParfüms.CurrentRow != null)
            {
                int parfümNummer = (int)dgvOrientalischeParfüms.CurrentRow.Cells[0].Value;
                _ÖffneAddUpdateForm(parfümNummer);
            }
        }

        private void entferneParfümToolStripMenuItem3_Click(object sender, EventArgs e)
        {
            if (dgvOrientalischeParfüms.CurrentRow != null)
            {
                int parfümNummer = (int)dgvOrientalischeParfüms.CurrentRow.Cells[0].Value;
                _EntferneParfüm(parfümNummer);
            }
        }

        private void dgvOrientalischeParfüms_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && dgvOrientalischeParfüms.Rows[e.RowIndex].Cells[2].Value != null)
            {
                string currentName = dgvOrientalischeParfüms.Rows[e.RowIndex].Cells[2].Value.ToString();
                _ÖffneParfumoWebseite(currentName);
            }
        }

        private void dgvOrientalischeParfüms_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            _MarkiereParfümZeilen(dgvOrientalischeParfüms);
        }
    }
}
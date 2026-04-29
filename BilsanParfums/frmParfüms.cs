using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Management.Instrumentation;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;
using Busnisse_Layer;
using clsHilfsMethoden;
using Guna.UI2.WinForms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using iTextSharp.text;
using iTextSharp.text.pdf;
using static clsHilfsMethoden.AutoComplete;
using ClosedXML;
using ClosedXML.Excel;

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
        // In Ihrer Hauptform (z.B. frmParfüms.cs)
        public static AutoComplete.AVLTree GlobalParfumeNameTree = new AutoComplete.AVLTree();

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

            // Baum einmalig beim Start befüllen
            _InitialisiereUndFülleAutoCompleteBaum(); // Methode, die Sie selbst definieren
        }

        private void _InitialisiereUndFülleAutoCompleteBaum()
        {
            GlobalParfumeNameTree = new AutoComplete.AVLTree(); // Leeren Baum erstellen

            // Alle Parfümnamen aus der Datenbank holen
            DataTable allParfumsDt = clsNeueParfümDaten.GetAllParfüms();
            if (allParfumsDt != null)
            {
                foreach (DataRow row in allParfumsDt.Rows)
                {
                    if (row["Name"] != DBNull.Value)
                    {
                        GlobalParfumeNameTree.Insert(row["Name"].ToString());
                    }
                }
            }
        }
        /// <summary>
        /// Lädt alle Parfüm-Daten und aktualisiert das DataGridView.
        /// </summary>
        private void _LadeAlleParfümDaten()
        {
            lock (_dataloadLock)
            {
                _dtParfüms = clsNeueParfümDaten.GetAllParfüms();
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
                _dtHerrenParfüms = clsNeueParfümDaten.GetAllHerrenParfüms();
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
                _dtDamenParfüms = clsNeueParfümDaten.GetAllDamenParfüms(); // Hier sollte eine Filter-Methode stehen.

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
                _dtUnisexParfüms = clsNeueParfümDaten.GetAllUnisexParfüms();

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
                _dtKinderParfüms = clsNeueParfümDaten.GetAllKinderParfüms();

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
                _dtOrientalischeParfüms = clsNeueParfümDaten.GetAllOrientalischeParfüms();

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
            _ÖffneAddUpdateForm(null);
        }

        private void neueParfümHinzufügenToolStripMenuItem_Click(object sender, EventArgs e)
        {
            _ÖffneAddUpdateForm(null);
        }

        private void aktualisiereBestehendesParfümToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (dgvAlleParfüms.CurrentRow != null)
            {
                string ParfümCode = dgvAlleParfüms.CurrentRow.Cells[1].Value.ToString();
                _ÖffneAddUpdateForm(ParfümCode);
            }
        }

        private void entferneParfümToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (dgvAlleParfüms.CurrentRow != null)
            {
                string ParfümCode = dgvAlleParfüms.CurrentRow.Cells[1].Value.ToString();
                _EntferneParfüm(ParfümCode);
            }
        }

        private void dgvAlleParfüms_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && dgvAlleParfüms.Rows[e.RowIndex].Cells[3].Value != null)
            {
                string currentName = dgvAlleParfüms.Rows[e.RowIndex].Cells[3].Value.ToString();
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

                // Spezialzeichen behandeln
                // Das Apostroph ' muss verdoppelt werden, um es zu escapen
                filterwert = filterwert.Replace("'", "''");

                // Das Backtick ` wird oft als Anführungszeichen verwendet
                // Manchmal muss es auch entfernt oder escapet werden,
                // um Konflikte zu vermeiden. Hier ersetzen wir es sicherheitshalber.
                filterwert = filterwert.Replace("`", "");

                switch (spalteName)
                {
                    case "AlteNummer":
                        filterString = $"{spalteName} = {filterwert}";
                        break;
                    //case "ParfümCode":
                    //    filterString = $"{spalteName} = '{filterwert}'";
                    //    break;
                    case "ParfümCode":
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

        private string _StatusFilterAnwenden(Guna2ComboBox stockComboBox)
        {
            // Stellt sicher, dass stockComboBox.SelectedItem nicht null ist, bevor ToString() aufgerufen wird.
            // Wenn es null ist, wird es als "Alle" behandelt, um keinen Filter anzuwenden.
            string selectedOption = stockComboBox.SelectedItem?.ToString() ?? "Alle";

            switch (selectedOption)
            {
                case "Vorhanden":
                    // Gibt den Filterstring zurück, um nur vorhandene Parfüms anzuzeigen.
                    return "IstVorhanden = TRUE";
                case "In Bestellung":
                    // Gibt den Filterstring zurück, um nur Parfüms in Bestellung anzuzeigen.
                    return "InBestellung = TRUE";
                case "Alle":
                    // Bei Auswahl von "Alle" soll kein Filter angewendet werden.
                    return string.Empty;
                default:
                    // Bei unerwarteten Werten (falls die ComboBox andere Einträge hat) soll ebenfalls kein Filter angewendet werden.
                    return string.Empty;
            }
        }
        /// <summary>
        /// Formatiert das DataGridView, um Spaltenbreite und Schriftart anzupassen.
        /// </summary>
        private void _PasseDataGridViewSchriftAn(DataGridView dgv)
        {
            // AutoSizeColumnsMode auf AllCells setzen, um die Spaltenbreite automatisch anzupassen
            //dgv.AutoResizeColumns(DataGridViewAutoSizeColumnsMode.AllCells);
            // Beispiel mit Segoe UI
            dgv.DefaultCellStyle.Font = new System.Drawing.Font("Segoe UI", 13, FontStyle.Regular);
            dgv.ColumnHeadersDefaultCellStyle.Font = new System.Drawing.Font("Segoe UI", 14, FontStyle.Italic);

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
                // Überspringe die leere Zeile am Ende
                if (row.IsNewRow) continue;

                // Setze die Standardstile der Zeile zurück
                row.DefaultCellStyle.BackColor = System.Drawing.Color.White;
                row.DefaultCellStyle.ForeColor = System.Drawing.Color.Black;

                row.Cells["AlteNummer"].Style.BackColor = Color.LightGray;
                // Werte aus den Status-Spalten abrufen
                bool istVorhanden = row.Cells["IstVorhanden"].Value != null && Convert.ToBoolean(row.Cells["IstVorhanden"].Value);
                bool isInBestellung = row.Cells["InBestellung"].Value != null && Convert.ToBoolean(row.Cells["InBestellung"].Value);
                bool istNeu = row.Cells["IstNeu"].Value != null && Convert.ToBoolean(row.Cells["IstNeu"].Value);

                if(istNeu)
                {
                    // Färbe die Zelle "InBestellung" zusätzlich Orange
                    row.Cells["IstNeu"].Style.BackColor = System.Drawing.Color.LightGreen;
                }
                else if(!istNeu)
                {
                    foreach (DataGridViewCell cell in row.Cells)
                    {
                        if (cell.OwningColumn.Name == "IstNeu")
                        {
                            cell.Style.BackColor = System.Drawing.Color.White;
                        }
                    }
                }
                // --- Logik für die Markierung ---

                // 1. Fall: Das Parfüm ist gleichzeitig vorhanden UND in Bestellung
                if (istVorhanden && isInBestellung)
                {
                    // Färbe die gesamte Zeile hellgrün
                    row.DefaultCellStyle.BackColor = System.Drawing.Color.LightBlue;

                    // Färbe die Zelle "InBestellung" zusätzlich Orange
                    row.Cells["InBestellung"].Style.BackColor = System.Drawing.Color.Orange;
                }
                // 2. Fall: Das Parfüm ist NUR vorhanden (nicht in Bestellung)
                else if (istVorhanden)
                {
                    // Färbe die gesamte Zeile Hellgrün
                    row.DefaultCellStyle.BackColor = System.Drawing.Color.LightBlue;
                    // Färbe die restlichen Zellen der Zeile Hellgrün
                    foreach (DataGridViewCell cell in row.Cells)
                    {
                        if (cell.OwningColumn.Name == "InBestellung")
                        {
                            cell.Style.BackColor = System.Drawing.Color.White;
                        }
                    }
                }
                // 3. Fall: Das Parfüm ist NUR in Bestellung (nicht vorhanden)
                else if (isInBestellung)
                {
                    // Färbe nur die Zelle "InBestellung" Orange
                    row.Cells["InBestellung"].Style.BackColor = System.Drawing.Color.Orange;
                }
            }

        }
        /// <summary>
        /// Öffnet die Hinzufügen-/Aktualisieren-Form.
        /// </summary>
        private void _ÖffneAddUpdateForm(string ParfümCode)
        {
            using (frmAddUpdateParfüms frm = new frmAddUpdateParfüms(ParfümCode))
            {
                if (frm.ShowDialog() == DialogResult.OK)
                {
                    // Rufen Sie die Methode auf, um den ausgewählten Tab zu aktualisieren
                    _AktualisiereDatenNachTab();

                    // UND HIER bauen Sie den Autovervollständigungsbaum neu auf:
                    _InitialisiereUndFülleAutoCompleteBaum();
                }
            }

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
        private void _EntferneParfüm(string ParfümCode)
        {
            bool result = (MessageBox.Show("Sind Sie sicher, Sie möchten diesen Vorgang durchführen?", "Hinweis", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2) == DialogResult.OK);

            if (!result) return;

            clsNeueParfümDaten parfuemDaten = clsNeueParfümDaten.FindByParfümNummer(ParfümCode);

            if (parfuemDaten != null && parfuemDaten.Delete())
            {
                // TODO: Korrekte Handhabung des AVL-Baums prüfen, falls nötig
                GlobalParfumeNameTree.Delete(parfuemDaten.Name);

                MessageBox.Show("Parfümdaten wurden erfolgreich entfernt", "Entfernung", MessageBoxButtons.OK, MessageBoxIcon.Information);
                _AktualisiereDatenNachTab();

                // UND HIER bauen Sie den Autovervollständigungsbaum neu auf:
                _InitialisiereUndFülleAutoCompleteBaum();
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
        private void _FühreAutoCompleteAus(Guna2TextBox filterTextBox, ListBox suggestionsListBox)
        {
            string prefix = filterTextBox.Text.Trim();

            if (GlobalParfumeNameTree != null)
            {
                var completions = GlobalParfumeNameTree.AutoComplete(prefix);
                _UpdateAutoCompleteListeBox(completions, filterTextBox, suggestionsListBox);
            }
        }
        private void _UpdateAutoCompleteListeBox(IEnumerable<string> completions, Guna2TextBox filterTextBox, ListBox suggestionsListBox)
        {
            lock (GlobalParfumeNameTree)
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
                        suggestionsListBox.BringToFront();
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
                _FilterAnwenden(filterComboBox, filterTextBox, bindingSource, dgv);
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
            // Dies ist eine gute Stelle, um den Filter temporär zu leeren,
            // bevor die neue Filterlogik angewendet wird.
            _bindingSourceAlleParfüms.Filter = string.Empty;

            // Steuerung der Sichtbarkeit der Filter-Steuerelemente
            if (cbFilterby.SelectedItem?.ToString() == "Status") // Verwenden Sie '?.ToString()' für Null-Sicherheit
            {
                cbAlleParfümsStatus.Visible = true; // Zeigt die Status-ComboBox
                cbAlleParfümsStatus.SelectedIndex = 0; // Optional: Setzt die Auswahl zurück auf den ersten Eintrag (z.B. "Alle")
                txtFilterwert.Visible = false;     // Blendet das Textfeld aus
                txtFilterwert.Clear();             // Leert das Textfeld sicherheitshalber
            }
            else // Für alle anderen Filtertypen (Name, Marke, Nummer etc.)
            {
                cbAlleParfümsStatus.Visible = false; // Blendet die Status-ComboBox aus
                                                     // Optional: cbIsVorhandenOderInBestellung.SelectedIndex = -1; // Setzt die Auswahl zurück
                txtFilterwert.Visible = true;     // Zeigt das Textfeld an
            }

            // Zusätzliche Absicherung (meist nicht nötig, wenn Controls im Designer erstellt sind)
            // if (txtOrientalischFilterwert == null) return; 

            // Steuerung des ReadOnly-Zustands und des Fokus des Textfeldes
            if (cbFilterby.SelectedIndex != -1)
            {
                txtFilterwert.Clear();        // Textfeld leeren
                //txtHerrenFilterwert.ReadOnly = false; // Eingabe erlauben
                txtFilterwert.Focus();        // Fokus auf das Textfeld setzen
            }
            else // Wenn kein Element ausgewählt ist (SelectedIndex ist -1)
            {
                txtFilterwert.Clear();        // Textfeld leeren
                                              //  txtHerrenFilterwert.ReadOnly = true;  // Eingabe verhindern                                                           // Optional: Den Fokus von der TextBox entfernen, wenn sie nicht nutzbar is                                                     // this.ActiveControl = cbOrientalischFilterby;
            }
            _MarkiereParfümZeilen(dgvAlleParfüms);
        }
        private void cbAlleParfümsStatus_SelectedIndexChanged(object sender, EventArgs e)
        {
            List<string> activeFilters = new List<string>();
            // 2. Filter vom Status-ComboBox (Vorhanden, In Bestellung) hinzufügen
            string stockStatusFilter = _StatusFilterAnwenden(cbAlleParfümsStatus);

            if (!string.IsNullOrEmpty(stockStatusFilter))
            {
                activeFilters.Add(stockStatusFilter);
            }

            // Kombiniere alle aktiven Filter mit " AND "
            string combinedFilter = string.Empty;
            if (activeFilters.Any())
            {
                combinedFilter = string.Join(" AND ", activeFilters);
            }

            // Wende den kombinierten Filter auf die BindingSource an
            _bindingSourceAlleParfüms.Filter = combinedFilter;

            // Aktualisiere die Zähler und die visuellen Markierungen nach dem Filtern
            _AktualisiereAlleParfümdatenAnzahl(_bindingSourceAlleParfüms);
            _MarkiereParfümZeilen(dgvAlleParfüms);
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
        private void lbVorschlägeFürAlleParfüms_Click_1(object sender, EventArgs e)
        {
            _WähleVorschlagAus(cbFilterby, txtFilterwert, lbVorschlägeFürAlleParfüms, _bindingSourceAlleParfüms, dgvAlleParfüms);
        }
        private void lbVorschlägeFürAlleParfüms_KeyDown_1(object sender, KeyEventArgs e)
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

            if (selectedItem == "AlteNummer")
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

            if (selectedItem == "AlteNummer")
            {
                e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
            }
        }
        private void cbDamenFilterby_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Dies ist eine gute Stelle, um den Filter temporär zu leeren,
            // bevor die neue Filterlogik angewendet wird.
            _bindingSourceDamenParfüms.Filter = string.Empty;

            // Steuerung der Sichtbarkeit der Filter-Steuerelemente
            if (cbDamenFilterby.SelectedItem?.ToString() == "Status") // Verwenden Sie '?.ToString()' für Null-Sicherheit
            {
                cbDamenParfümStatus.Visible = true; // Zeigt die Status-ComboBox
                cbDamenParfümStatus.SelectedIndex = 0; // Optional: Setzt die Auswahl zurück auf den ersten Eintrag (z.B. "Alle")
                txtDamenFilterwert.Visible = false;     // Blendet das Textfeld aus
                txtDamenFilterwert.Clear();             // Leert das Textfeld sicherheitshalber
            }
            else // Für alle anderen Filtertypen (Name, Marke, Nummer etc.)
            {
                cbDamenParfümStatus.Visible = false; // Blendet die Status-ComboBox aus
                                                     // Optional: cbIsVorhandenOderInBestellung.SelectedIndex = -1; // Setzt die Auswahl zurück
                txtDamenFilterwert.Visible = true;     // Zeigt das Textfeld an
            }

            // Zusätzliche Absicherung (meist nicht nötig, wenn Controls im Designer erstellt sind)
            // if (txtOrientalischFilterwert == null) return; 

            // Steuerung des ReadOnly-Zustands und des Fokus des Textfeldes
            if (cbDamenFilterby.SelectedIndex != -1)
            {
                txtDamenFilterwert.Clear();        // Textfeld leeren
                //txtHerrenFilterwert.ReadOnly = false; // Eingabe erlauben
                txtDamenFilterwert.Focus();        // Fokus auf das Textfeld setzen
            }
            else // Wenn kein Element ausgewählt ist (SelectedIndex ist -1)
            {
                txtDamenFilterwert.Clear();        // Textfeld leeren
                                                   //  txtHerrenFilterwert.ReadOnly = true;  // Eingabe verhindern                                                           // Optional: Den Fokus von der TextBox entfernen, wenn sie nicht nutzbar is                                                     // this.ActiveControl = cbOrientalischFilterby;
            }
            _MarkiereParfümZeilen(dgvDamenParfüms);
        }
        private void cbDamenParfümStatus_SelectedIndexChanged(object sender, EventArgs e)
        {
            List<string> activeFilters = new List<string>();
            // 2. Filter vom Status-ComboBox (Vorhanden, In Bestellung) hinzufügen
            string stockStatusFilter = _StatusFilterAnwenden(cbDamenParfümStatus);

            if (!string.IsNullOrEmpty(stockStatusFilter))
            {
                activeFilters.Add(stockStatusFilter);
            }

            // Kombiniere alle aktiven Filter mit " AND "
            string combinedFilter = string.Empty;
            if (activeFilters.Any())
            {
                combinedFilter = string.Join(" AND ", activeFilters);
            }

            // Wende den kombinierten Filter auf die BindingSource an
            _bindingSourceDamenParfüms.Filter = combinedFilter;

            // Aktualisiere die Zähler und die visuellen Markierungen nach dem Filtern
            _AktualisiereFrauenParfümdatenAnzahl(_bindingSourceDamenParfüms);
            _MarkiereParfümZeilen(dgvDamenParfüms);
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
                _FilterAnwenden(cbDamenFilterby, txtDamenFilterwert, _bindingSourceDamenParfüms, dgvDamenParfüms);
            }
            // _MarkiereParfümZeilen(dgvDamenParfüms);
        }
        private void lbVorschlägeFürDamen_Click(object sender, EventArgs e)
        {
            _WähleVorschlagAus(cbDamenFilterby, txtDamenFilterwert, lbVorschlägeFürDamen, _bindingSourceDamenParfüms, dgvDamenParfüms);
        }
        private void lbVorschlägeFürDamen_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                _WähleVorschlagAus(cbDamenFilterby, txtDamenFilterwert, lbVorschlägeFürDamen, _bindingSourceDamenParfüms, dgvDamenParfüms);
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
            _ÖffneAddUpdateForm(null);
        }
        private void neuesParfümHinzufügenToolStripMenuItem_Click(object sender, EventArgs e)
        {
            _ÖffneAddUpdateForm(null);
        }

        private void bestehendesParfümAktualisierenToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (dgvDamenParfüms.CurrentRow != null)
            {
                string ParfümCode = dgvDamenParfüms.CurrentRow.Cells[1].Value.ToString();
                _ÖffneAddUpdateForm(ParfümCode);
            }
        }

        private void entferneParfümToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            if (dgvDamenParfüms.CurrentRow != null)
            {
                string ParfümCode = dgvDamenParfüms.CurrentRow.Cells[1].Value.ToString();
                _EntferneParfüm(ParfümCode);
            }
        }
        private void dgvDamenParfüms_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            _MarkiereParfümZeilen(dgvDamenParfüms);
        }
        private void dgvDamenParfüms_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && dgvDamenParfüms.Rows[e.RowIndex].Cells[3].Value != null)
            {
                string currentName = dgvDamenParfüms.Rows[e.RowIndex].Cells[3].Value.ToString();
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
            if (selectedItem == "AlteNummer")
            {
                e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
            }
            // Andernfalls (bei anderen Filtern wie "Name"), keine Einschränkung der Eingabe.
        }
        private void cbHerrenFilterby_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Dies ist eine gute Stelle, um den Filter temporär zu leeren,
            // bevor die neue Filterlogik angewendet wird.
            _bindingSourceHerrenParfüms.Filter = string.Empty;

            // Steuerung der Sichtbarkeit der Filter-Steuerelemente
            if (cbHerrenFilterby.SelectedItem?.ToString() == "Status") // Verwenden Sie '?.ToString()' für Null-Sicherheit
            {
                cbHerrenParfümStatus.Visible = true; // Zeigt die Status-ComboBox
                cbHerrenParfümStatus.SelectedIndex = 0; // Optional: Setzt die Auswahl zurück auf den ersten Eintrag (z.B. "Alle")
                txtHerrenFilterwert.Visible = false;     // Blendet das Textfeld aus
                txtHerrenFilterwert.Clear();             // Leert das Textfeld sicherheitshalber
            }
            else // Für alle anderen Filtertypen (Name, Marke, Nummer etc.)
            {
                cbHerrenParfümStatus.Visible = false; // Blendet die Status-ComboBox aus
                                                      // Optional: cbIsVorhandenOderInBestellung.SelectedIndex = -1; // Setzt die Auswahl zurück
                txtHerrenFilterwert.Visible = true;     // Zeigt das Textfeld an
            }

            // Zusätzliche Absicherung (meist nicht nötig, wenn Controls im Designer erstellt sind)
            // if (txtOrientalischFilterwert == null) return; 

            // Steuerung des ReadOnly-Zustands und des Fokus des Textfeldes
            if (cbHerrenFilterby.SelectedIndex != -1)
            {
                txtHerrenFilterwert.Clear();        // Textfeld leeren
                //txtHerrenFilterwert.ReadOnly = false; // Eingabe erlauben
                txtHerrenFilterwert.Focus();        // Fokus auf das Textfeld setzen
            }
            else // Wenn kein Element ausgewählt ist (SelectedIndex ist -1)
            {
                txtHerrenFilterwert.Clear();        // Textfeld leeren
                                                    // txtHerrenFilterwert.ReadOnly = true;  // Eingabe verhindern                                                           // Optional: Den Fokus von der TextBox entfernen, wenn sie nicht nutzbar is                                                     // this.ActiveControl = cbOrientalischFilterby;
            }
            _MarkiereParfümZeilen(dgvHerrenParfüms);
        }
        private void cbHerrenParfümStatus_SelectedIndexChanged(object sender, EventArgs e)
        {
            List<string> activeFilters = new List<string>();
            // 2. Filter vom Status-ComboBox (Vorhanden, In Bestellung) hinzufügen
            string stockStatusFilter = _StatusFilterAnwenden(cbHerrenParfümStatus);

            if (!string.IsNullOrEmpty(stockStatusFilter))
            {
                activeFilters.Add(stockStatusFilter);
            }

            // Kombiniere alle aktiven Filter mit " AND "
            string combinedFilter = string.Empty;
            if (activeFilters.Any())
            {
                combinedFilter = string.Join(" AND ", activeFilters);
            }

            // Wende den kombinierten Filter auf die BindingSource an
            _bindingSourceHerrenParfüms.Filter = combinedFilter;

            // Aktualisiere die Zähler und die visuellen Markierungen nach dem Filtern
            _AktualisiereHerrenParfümdatenAnzahl(_bindingSourceHerrenParfüms);
            _MarkiereParfümZeilen(dgvHerrenParfüms);
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
                _FilterAnwenden(cbHerrenFilterby, txtHerrenFilterwert, _bindingSourceHerrenParfüms, dgvHerrenParfüms);
            }
            //_MarkiereParfümZeilen(dgvHerrenParfüms);
        }
        private void lbVorschälgeFürHerrendüfte_Click(object sender, EventArgs e)
        {
            _WähleVorschlagAus(cbHerrenFilterby, txtHerrenFilterwert, lbVorschälgeFürHerrendüfte, _bindingSourceHerrenParfüms, dgvHerrenParfüms);
        }

        private void lbVorschälgeFürHerrendüfte_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                _WähleVorschlagAus(cbHerrenFilterby, txtHerrenFilterwert, lbVorschälgeFürHerrendüfte, _bindingSourceHerrenParfüms, dgvHerrenParfüms);
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
            _ÖffneAddUpdateForm(null);
        }
        private void neuesParfümsHinzufügenToolStripMenuItem_Click(object sender, EventArgs e)
        {
            _ÖffneAddUpdateForm(null);
        }

        private void aktualisiereBestehendesParfümToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            if (dgvHerrenParfüms.CurrentRow != null)
            {
                string ParfümCode = dgvHerrenParfüms.CurrentRow.Cells[1].Value.ToString();
                _ÖffneAddUpdateForm(ParfümCode);
            }
        }

        private void entfernToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (dgvHerrenParfüms.CurrentRow != null)
            {
                string ParfümCode = dgvDamenParfüms.CurrentRow.Cells[1].Value.ToString();
                _EntferneParfüm(ParfümCode);
            }
        }
        private void dgvHerrenParfüms_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            _MarkiereParfümZeilen(dgvHerrenParfüms);
        }
        private void dgvHerrenParfüms_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && dgvHerrenParfüms.Rows[e.RowIndex].Cells[3].Value != null)
            {
                string currentName = dgvHerrenParfüms.Rows[e.RowIndex].Cells[3].Value.ToString();
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

            if (selectedItem == "AlteNummer")
            {
                e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
            }
        }
        private void cbUnisexFilterby_SelectedIndexChanged(object sender, EventArgs e)
        {

            // Dies ist eine gute Stelle, um den Filter temporär zu leeren,
            // bevor die neue Filterlogik angewendet wird.
            _bindingSourceUnisexParfüms.Filter = string.Empty;

            // Steuerung der Sichtbarkeit der Filter-Steuerelemente
            if (cbUnisexFilterby.SelectedItem?.ToString() == "Status") // Verwenden Sie '?.ToString()' für Null-Sicherheit
            {
                cbUnisexParfümsStatus.Visible = true; // Zeigt die Status-ComboBox
                cbUnisexParfümsStatus.SelectedIndex = 0; // Optional: Setzt die Auswahl zurück auf den ersten Eintrag (z.B. "Alle")
                txtUnisexFilterwert.Visible = false;     // Blendet das Textfeld aus
                txtUnisexFilterwert.Clear();             // Leert das Textfeld sicherheitshalber
            }
            else // Für alle anderen Filtertypen (Name, Marke, Nummer etc.)
            {
                cbUnisexParfümsStatus.Visible = false; // Blendet die Status-ComboBox aus
                                                       // Optional: cbIsVorhandenOderInBestellung.SelectedIndex = -1; // Setzt die Auswahl zurück
                txtUnisexFilterwert.Visible = true;     // Zeigt das Textfeld an
            }

            // Zusätzliche Absicherung (meist nicht nötig, wenn Controls im Designer erstellt sind)
            // if (txtOrientalischFilterwert == null) return; 

            // Steuerung des ReadOnly-Zustands und des Fokus des Textfeldes
            if (cbUnisexFilterby.SelectedIndex != -1)
            {
                txtUnisexFilterwert.Clear();        // Textfeld leeren
                                                    // txtUnisexFilterwert.ReadOnly = false; // Eingabe erlauben
                txtUnisexFilterwert.Focus();        // Fokus auf das Textfeld setzen
            }
            else // Wenn kein Element ausgewählt ist (SelectedIndex ist -1)
            {
                txtUnisexFilterwert.Clear();        // Textfeld leeren
                                                    // txtUnisexFilterwert.ReadOnly = true;  // Eingabe verhindern                                                           // Optional: Den Fokus von der TextBox entfernen, wenn sie nicht nutzbar is                                                     // this.ActiveControl = cbOrientalischFilterby;
            }
            _MarkiereParfümZeilen(dgvUnisexParfüms);
        }
        private void cbUnisexParfümsStatus_SelectedIndexChanged(object sender, EventArgs e)
        {
            List<string> activeFilters = new List<string>();
            // 2. Filter vom Status-ComboBox (Vorhanden, In Bestellung) hinzufügen
            string stockStatusFilter = _StatusFilterAnwenden(cbUnisexParfümsStatus);

            if (!string.IsNullOrEmpty(stockStatusFilter))
            {
                activeFilters.Add(stockStatusFilter);
            }

            // Kombiniere alle aktiven Filter mit " AND "
            string combinedFilter = string.Empty;
            if (activeFilters.Any())
            {
                combinedFilter = string.Join(" AND ", activeFilters);
            }

            // Wende den kombinierten Filter auf die BindingSource an
            _bindingSourceUnisexParfüms.Filter = combinedFilter;

            // Aktualisiere die Zähler und die visuellen Markierungen nach dem Filtern
            _AktualisiereUnisexParfümdatenAnzahl(_bindingSourceUnisexParfüms);
            _MarkiereParfümZeilen(dgvUnisexParfüms);
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
                _FilterAnwenden(cbUnisexFilterby, txtUnisexFilterwert, _bindingSourceUnisexParfüms, dgvUnisexParfüms);
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
            _ÖffneAddUpdateForm(null);
        }
        private void neuesParfümHinzufügenToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            _ÖffneAddUpdateForm(null);
        }

        private void aktualisiereBestehendesParfümToolStripMenuItem2_Click(object sender, EventArgs e)
        {
            if (dgvUnisexParfüms.CurrentRow != null)
            {
                string ParfümCode = dgvUnisexParfüms.CurrentRow.Cells[1].Value.ToString();
                _ÖffneAddUpdateForm(ParfümCode);
            }
        }

        private void entferneParfümToolStripMenuItem2_Click(object sender, EventArgs e)
        {
            if (dgvUnisexParfüms.CurrentRow != null)
            {
                string ParfümCode = dgvDamenParfüms.CurrentRow.Cells[1].Value.ToString();
                _EntferneParfüm(ParfümCode);
            }
        }

        private void dgvUnisexParfüms_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            _MarkiereParfümZeilen(dgvUnisexParfüms);
        }

        private void dgvUnisexParfüms_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && dgvUnisexParfüms.Rows[e.RowIndex].Cells[3].Value != null)
            {
                string currentName = dgvUnisexParfüms.Rows[e.RowIndex].Cells[3].Value.ToString();
                _ÖffneParfumoWebseite(currentName);
            }
        }



        //#############################################################//
        // --- Event Handler für Orientalischdüfte ---
        private void cbOrientalischFilterby_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Dies ist eine gute Stelle, um den Filter temporär zu leeren,
            // bevor die neue Filterlogik angewendet wird.
            _bindingSourceOrientalischeParfüms.Filter = string.Empty;

            // Steuerung der Sichtbarkeit der Filter-Steuerelemente
            if (cbOrientalischFilterby.SelectedItem?.ToString() == "Status") // Verwenden Sie '?.ToString()' für Null-Sicherheit
            {
                cbOrientalischeParfümsStatus.Visible = true; // Zeigt die Status-ComboBox
                cbOrientalischeParfümsStatus.SelectedIndex = 0; // Optional: Setzt die Auswahl zurück auf den ersten Eintrag (z.B. "Alle")
                txtOrientalischFilterwert.Visible = false;     // Blendet das Textfeld aus
                txtOrientalischFilterwert.Clear();             // Leert das Textfeld sicherheitshalber
            }
            else // Für alle anderen Filtertypen (Name, Marke, Nummer etc.)
            {
                cbOrientalischeParfümsStatus.Visible = false; // Blendet die Status-ComboBox aus
                                                              // Optional: cbIsVorhandenOderInBestellung.SelectedIndex = -1; // Setzt die Auswahl zurück
                txtOrientalischFilterwert.Visible = true;     // Zeigt das Textfeld an
            }

            // Zusätzliche Absicherung (meist nicht nötig, wenn Controls im Designer erstellt sind)
            // if (txtOrientalischFilterwert == null) return; 

            // Steuerung des ReadOnly-Zustands und des Fokus des Textfeldes
            if (cbOrientalischFilterby.SelectedIndex != -1)
            {
                txtOrientalischFilterwert.Clear();        // Textfeld leeren
                //txtOrientalischFilterwert.ReadOnly = false; // Eingabe erlauben
                txtOrientalischFilterwert.Focus();        // Fokus auf das Textfeld setzen
            }
            else // Wenn kein Element ausgewählt ist (SelectedIndex ist -1)
            {
                txtOrientalischFilterwert.Clear();        // Textfeld leeren
                                                          // txtOrientalischFilterwert.ReadOnly = true;  // Eingabe verhindern
                                                          // Optional: Den Fokus von der TextBox entfernen, wenn sie nicht nutzbar ist
                                                          // this.ActiveControl = cbOrientalischFilterby;
            }
            _MarkiereParfümZeilen(dgvOrientalischeParfüms);

        }
        private void cbOrientalischeParfümsStatus_SelectedIndexChanged(object sender, EventArgs e)
        {
            List<string> activeFilters = new List<string>();
            // 2. Filter vom Status-ComboBox (Vorhanden, In Bestellung) hinzufügen
            string stockStatusFilter = _StatusFilterAnwenden(cbOrientalischeParfümsStatus);

            if (!string.IsNullOrEmpty(stockStatusFilter))
            {
                activeFilters.Add(stockStatusFilter);
            }

            // Kombiniere alle aktiven Filter mit " AND "
            string combinedFilter = string.Empty;
            if (activeFilters.Any())
            {
                combinedFilter = string.Join(" AND ", activeFilters);
            }

            // Wende den kombinierten Filter auf die BindingSource an
            _bindingSourceOrientalischeParfüms.Filter = combinedFilter;

            // Aktualisiere die Zähler und die visuellen Markierungen nach dem Filtern
            _AktualisiereOrientalischParfümdatenAnzahl(_bindingSourceOrientalischeParfüms);
            _MarkiereParfümZeilen(dgvOrientalischeParfüms);
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

            if (selectedItem == "AlteNummer")
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
            _ÖffneAddUpdateForm(null);
        }

        private void neuesParfümHinzufügenToolStripMenuItem2_Click(object sender, EventArgs e)
        {
            _ÖffneAddUpdateForm(null);
        }

        private void aktualisiereBestehendesParfümToolStripMenuItem3_Click(object sender, EventArgs e)
        {
            if (dgvOrientalischeParfüms.CurrentRow != null)
            {
                string ParfümCode = dgvOrientalischeParfüms.CurrentRow.Cells[1].Value.ToString();
                _ÖffneAddUpdateForm(ParfümCode);
            }
        }

        private void entferneParfümToolStripMenuItem3_Click(object sender, EventArgs e)
        {
            if (dgvOrientalischeParfüms.CurrentRow != null)
            {
                string ParfümCode = dgvDamenParfüms.CurrentRow.Cells[1].Value.ToString();
                _EntferneParfüm(ParfümCode);
            }
        }

        private void dgvOrientalischeParfüms_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && dgvOrientalischeParfüms.Rows[e.RowIndex].Cells[3].Value != null)
            {
                string currentName = dgvOrientalischeParfüms.Rows[e.RowIndex].Cells[3].Value.ToString();
                _ÖffneParfumoWebseite(currentName);
            }
        }

        private void dgvOrientalischeParfüms_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            _MarkiereParfümZeilen(dgvOrientalischeParfüms);
        }







        //####################    pdf erstellen     ################################
        private void btnPdfParfümsliste_Click(object sender, EventArgs e)
        {
            string filterType = "Vorhanden";
            string pdfTitle = "Alle vorhandene Parfüms";

            // Rufen Sie die Methode mit dem Titel und dem Filter-Typ auf
            _ErstellePdfVonParfuem(dgvAlleParfüms, pdfTitle, filterType);
            //_ErstelleExcelVonParfuem(dgvAlleParfüms, pdfTitle, filterType);
        }
     

        private void btnParfümlisteFürDamen_Click(object sender, EventArgs e)
        {
            // Bestimmen, welcher Filter-Typ ausgewählt ist
            string filterType = "Alle";
            string pdfTitle = "Alle Damenparfüms";

            if (cbDamenFilterby.SelectedIndex != -1 && cbDamenFilterby.SelectedItem.ToString() == "Status")
            {
                if (cbDamenParfümStatus.SelectedItem.ToString() == "Vorhanden")
                {
                    filterType = "Vorhanden";
                    pdfTitle = "Vorhandene Damenparfüms";
                }
                else if (cbDamenParfümStatus.SelectedItem.ToString() == "In Bestellung")
                {
                    filterType = "In Bestellung";
                    pdfTitle = "Bestellte Damenparfüms";
                }
            }

            // Rufen Sie die Methode mit dem Titel und dem Filter-Typ auf
            _ErstellePdfVonParfuem(dgvDamenParfüms, pdfTitle, filterType);
        }

        private void btnParümslisteFürHerren_Click(object sender, EventArgs e)
        {
            // Bestimmen, welcher Filter-Typ ausgewählt ist
            string filterType = "Alle";
            string pdfTitle = "Alle Herrenparfüms";

            if (cbHerrenFilterby.SelectedIndex != -1 && cbHerrenFilterby.SelectedItem.ToString() == "Status")
            {
                if (cbHerrenParfümStatus.SelectedItem.ToString() == "Vorhanden")
                {
                    filterType = "Vorhanden";
                    pdfTitle = "Vorhandene Herrenparfüms";
                }
                else if (cbHerrenParfümStatus.SelectedItem.ToString() == "In Bestellung")
                {
                    filterType = "In Bestellung";
                    pdfTitle = "Bestellte Herrenparfüms";
                }
            }

            // Rufen Sie die Methode mit dem Titel und dem Filter-Typ auf
            _ErstellePdfVonParfuem(dgvHerrenParfüms, pdfTitle, filterType);
        }

        private void btnParfümslisteFürUnisex_Click(object sender, EventArgs e)
        {
            // Bestimmen, welcher Filter-Typ ausgewählt ist
            string filterType = "Alle";
            string pdfTitle = "Alle Unisexparfüms";

            if (cbUnisexFilterby.SelectedIndex != -1 && cbUnisexFilterby.SelectedItem.ToString() == "Status")
            {
                if (cbUnisexParfümsStatus.SelectedItem.ToString() == "Vorhanden")
                {
                    filterType = "Vorhanden";
                    pdfTitle = "Vorhandene Unisexparfüms";
                }
                else if (cbUnisexParfümsStatus.SelectedItem.ToString() == "In Bestellung")
                {
                    filterType = "In Bestellung";
                    pdfTitle = "Bestellte Unisexparfüms";
                }
            }

            // Rufen Sie die Methode mit dem Titel und dem Filter-Typ auf
            _ErstellePdfVonParfuem(dgvUnisexParfüms, pdfTitle, filterType);
        }

        private void _ErstellePdfVonParfuem(DataGridView dgv, string pdfTitle, string filterType)
        {
            string desktopPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            string fileName = pdfTitle + "-" + DateTime.Now.ToString("dd.MM.yyyy") + ".pdf";
            string filePath = Path.Combine(desktopPath, fileName);

            try
            {
                dgv.CommitEdit(DataGridViewDataErrorContexts.Commit);
                dgv.EndEdit();

                // Farben
                BaseColor goldColor = new BaseColor(212, 175, 55);
                BaseColor darkGreenColor = new BaseColor(44, 85, 48);
                BaseColor champagneColor = new BaseColor(245, 240, 230);
                BaseColor whiteColor = BaseColor.WHITE;
                BaseColor blackColor = BaseColor.BLACK;

                // Schriftarten
                iTextSharp.text.Font titleFont = new iTextSharp.text.Font(
                    iTextSharp.text.Font.FontFamily.HELVETICA, 16f, iTextSharp.text.Font.BOLD, blackColor);

                iTextSharp.text.Font headerFont = new iTextSharp.text.Font(
                    iTextSharp.text.Font.FontFamily.HELVETICA, 11f, iTextSharp.text.Font.BOLD, BaseColor.WHITE);

                iTextSharp.text.Font categoryFont = new iTextSharp.text.Font(
                    iTextSharp.text.Font.FontFamily.HELVETICA, 12f, iTextSharp.text.Font.BOLD, BaseColor.WHITE);

                iTextSharp.text.Font normalFont = new iTextSharp.text.Font(
                    iTextSharp.text.Font.FontFamily.HELVETICA, 10f, iTextSharp.text.Font.NORMAL, blackColor);

                iTextSharp.text.Font highlightFont = new iTextSharp.text.Font(
                    iTextSharp.text.Font.FontFamily.HELVETICA, 11.5f, iTextSharp.text.Font.NORMAL, blackColor);

                using (FileStream stream = new FileStream(filePath, FileMode.Create, FileAccess.Write, FileShare.None))
                using (Document pdfDoc = new Document(PageSize.A4, 25f, 25f, 25f, 25f))
                {
                    PdfWriter.GetInstance(pdfDoc, stream);
                    pdfDoc.Open();

                    // Titel
                    Paragraph title = new Paragraph(pdfTitle, titleFont)
                    {
                        Alignment = Element.ALIGN_CENTER,
                        SpacingAfter = 12f
                    };
                    pdfDoc.Add(title);

                    // Hinweis
                    Paragraph disclaimer = new Paragraph(
                        "Hinweis: Alle Markennamen dienen nur zur Orientierung am Duftcharakter und stehen in keiner Verbindung zu den jeweiligen Herstellern.",
                        normalFont)
                    {
                        Alignment = Element.ALIGN_CENTER,
                        SpacingAfter = 10f
                    };
                    pdfDoc.Add(disclaimer);

                    // 🔥 Nur noch 3 Spalten
                    PdfPTable table = new PdfPTable(3);
                    table.WidthPercentage = 100;
                    table.SetWidths(new float[] { 2f, 3f, 3f });
                    table.SpacingBefore = 5f;

                    // Header
                    table.AddCell(new PdfPCell(new Phrase("Marke", headerFont))
                    {
                        BackgroundColor = goldColor,
                        HorizontalAlignment = Element.ALIGN_CENTER,
                        VerticalAlignment = Element.ALIGN_MIDDLE,
                        Padding = 6f
                    });

                    table.AddCell(new PdfPCell(new Phrase("Name", headerFont))
                    {
                        BackgroundColor = goldColor,
                        HorizontalAlignment = Element.ALIGN_CENTER,
                        VerticalAlignment = Element.ALIGN_MIDDLE,
                        Padding = 6f
                    });

                    table.AddCell(new PdfPCell(new Phrase("Duftrichtung", headerFont))
                    {
                        BackgroundColor = goldColor,
                        HorizontalAlignment = Element.ALIGN_CENTER,
                        VerticalAlignment = Element.ALIGN_MIDDLE,
                        Padding = 6f
                    });

                    var alleZeilen = dgv.Rows
                        .Cast<DataGridViewRow>()
                        .Where(row => !row.IsNewRow)
                        .OrderBy(row => row.Cells["Kategorie"]?.Value?.ToString())
                        .ThenBy(row => row.Cells["Marke"]?.Value?.ToString())
                        .ThenBy(row => row.Cells["Name"]?.Value?.ToString())
                        .ToList();

                    string aktuelleKategorie = "";
                    int zeilenIndex = 0;
                    int gedruckteZeilen = 0;

                    foreach (DataGridViewRow row in alleZeilen)
                    {
                        bool istVorhanden = HoleBoolWert(row, "IstVorhanden");
                        bool inBestellung = HoleBoolWert(row, "InBestellung");
                        bool istNeu = HoleBoolWert(row, "IstNeu");

                        bool sollGedrucktWerden = false;

                        if (filterType == "Vorhanden" && istVorhanden)
                            sollGedrucktWerden = true;
                        else if (filterType == "InBestellung" && inBestellung)
                            sollGedrucktWerden = true;
                        else if (filterType == "IstNeu" && istNeu)
                            sollGedrucktWerden = true;
                        else if (filterType == "Alle")
                            sollGedrucktWerden = true;

                        if (!sollGedrucktWerden)
                            continue;

                        string kategorie = row.Cells["Kategorie"]?.Value?.ToString() ?? "";
                        string marke = row.Cells["Marke"]?.Value?.ToString() ?? "";
                        string name = row.Cells["Name"]?.Value?.ToString() ?? "";
                        string duftrichtung = row.Cells["Duftrichtung"]?.Value?.ToString() ?? "";

                        if (string.IsNullOrWhiteSpace(name))
                            continue;

                        // Kategorie-Zeile
                        if (kategorie != aktuelleKategorie)
                        {
                            PdfPCell categoryCell = new PdfPCell(new Phrase(" " + kategorie + " ", categoryFont))
                            {
                                Colspan = 3,
                                HorizontalAlignment = Element.ALIGN_CENTER,
                                VerticalAlignment = Element.ALIGN_MIDDLE,
                                BackgroundColor = darkGreenColor,
                                Padding = 6f
                            };

                            table.AddCell(categoryCell);
                            aktuelleKategorie = kategorie;
                            zeilenIndex = 0;
                        }

                        bool isHighlightRow = (zeilenIndex % 2 != 0);
                        BaseColor rowColor = isHighlightRow ? champagneColor : whiteColor;
                        iTextSharp.text.Font currentFont = highlightFont;

                        table.AddCell(new PdfPCell(new Phrase(marke, currentFont))
                        {
                            BackgroundColor = rowColor,
                            Padding = 5f,
                            VerticalAlignment = Element.ALIGN_MIDDLE
                        });

                        table.AddCell(new PdfPCell(new Phrase(name, currentFont))
                        {
                            BackgroundColor = rowColor,
                            Padding = 5f,
                            VerticalAlignment = Element.ALIGN_MIDDLE
                        });

                        table.AddCell(new PdfPCell(new Phrase(duftrichtung, currentFont))
                        {
                            BackgroundColor = rowColor,
                            Padding = 5f,
                            VerticalAlignment = Element.ALIGN_MIDDLE
                        });

                        zeilenIndex++;
                        gedruckteZeilen++;
                    }

                    if (gedruckteZeilen == 0)
                    {
                        pdfDoc.Add(new Paragraph("Keine Daten für den gewählten Filter gefunden.", normalFont));
                    }
                    else
                    {
                        pdfDoc.Add(table);
                    }

                    pdfDoc.Close();
                }

                MessageBox.Show(
                    $"Die Datei wurde erfolgreich auf dem Desktop gespeichert:\n{filePath}",
                    "Erfolg",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    $"Fehler beim Speichern der Datei: {ex.Message}",
                    "Fehler",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }
        private bool HoleBoolWert(DataGridViewRow row, string spaltenName)
        {
            if (!row.DataGridView.Columns.Contains(spaltenName))
                return false;

            var value = row.Cells[spaltenName].Value;

            if (value == null || value == DBNull.Value)
                return false;

            if (value is bool b)
                return b;

            if (bool.TryParse(value.ToString(), out bool result))
                return result;

            if (value.ToString() == "1")
                return true;

            return false;
        }
        private void btnBestellListeDrucken_Click(object sender, EventArgs e)
        {
            string filterType = "InBestellung";
            string pdfTitle = "Bestellte parfüms";

            _ErstellePdfVonParfuem(dgvAlleParfüms, pdfTitle, filterType);
        }

        private void btnNeuEingetroffen_Click(object sender, EventArgs e)
        {
            string filterType = "IstNeu";
            string pdfTitle = "Neu eingetroffen";

            _ErstellePdfVonParfuem(dgvAlleParfüms, pdfTitle, filterType);
        }
    }
}
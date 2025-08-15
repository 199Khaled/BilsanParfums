using Busnisse_Layer; // Business-Logik-Schicht
using clsHilfsMethoden; // Hilfsmethoden, falls hier verwendet
using Guna.UI2.WinForms; // Guna.UI2-Komponenten
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static Busnisse_Layer.clsParfüms; // Ermöglicht direkten Zugriff auf statische Mitglieder von clsParfüms

namespace BilsanParfums
{
    public partial class frmAddUpdateParfüms : Form
    {
        // Enum zur Definition des Formularmodus: Hinzufügen eines neuen Parfüms oder Aktualisieren eines bestehenden
        private enum enMode { addnew = 0, update = 1 }

        // Aktueller Betriebsmodus des Formulars
        private enMode _mode = enMode.addnew;

        // Instanz der clsParfüms-Klasse, die die aktuellen Parfümdaten speichert und verwaltet
        clsParfüms _parfüms;

        // Die ParfümNummer, die beim Initialisieren des Formulars übergeben wird.
        // -1 bedeutet, dass ein neues Parfüm hinzugefügt werden soll (addnew).
        // Eine positive Zahl bedeutet, dass ein bestehendes Parfüm aktualisiert werden soll (update).
        int _parfümNummer;

        // Öffentliche Eigenschaft, um den Namen des erfolgreich gespeicherten Parfüms an die aufrufende Form zurückzugeben.
        // Nützlich, um z.B. einen Autovervollständigungsbaum in der Hauptform zu aktualisieren.
        public string SavedParfumName { get; private set; }

        // Öffentliche Eigenschaft, um die Kategorie des erfolgreich gespeicherten Parfüms an die aufrufende Form zurückzugeben.
        // Nützlich, um z.B. den korrekten Tab in der Hauptform zu aktualisieren.
        public string SavedParfumKategorie { get; private set; }

        /// <summary>
        /// Konstruktor für das frmAddUpdateParfüms-Formular.
        /// </summary>
        /// <param name="parfümNummer">Die ParfümNummer des zu bearbeitenden Parfüms, oder -1 für ein neues Parfüm.</param>
        public frmAddUpdateParfüms(int parfümNummer)
        {
            InitializeComponent(); // Initialisiert die Komponenten des Formulars (UI-Elemente)
            this._parfümNummer = parfümNummer; // Speichert die übergebene ParfümNummer
        }

        /// <summary>
        /// Event-Handler für den Klick auf den "Abbrechen"-Button.
        /// Schließt das Formular und setzt den DialogResult auf Cancel.
        /// </summary>
        private void btnAbbrechen_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel; // Setzt das Ergebnis des Dialogs auf "Abgebrochen"
            this.Close(); // Schließt das Formular
        }

        /// <summary>
        /// Event-Handler für den Klick auf den "Speichern"-Button.
        /// Ruft die Methode zum Speichern der Parfümdaten auf.
        /// </summary>
        private void btnspeichern_Click(object sender, EventArgs e)
        {
            _parfümDatenSpeichern(); // Startet den Speichervorgang
        }

        /// <summary>
        /// Validiert ein einzelnes Textfeld auf leeren Inhalt.
        /// Zeigt bei Fehlern eine Fehlermeldung mit errorProvider und färbt das Feld rot ein.
        /// </summary>
        /// <param name="textBox">Das zu validierende Guna2TextBox-Steuerelement.</param>
        /// <param name="fieldName">Der Name des Feldes für die Fehlermeldung.</param>
        /// <returns>True, wenn das Feld gültig ist (nicht leer); False, wenn es leer ist.</returns>
        private bool _TextFelderValidierung(Guna2TextBox textBox, string fieldName)
        {
            if (string.IsNullOrEmpty(textBox.Text))
            {
                errorProvider1.SetError(textBox, fieldName + " darf nicht leer sein!"); // Fehlermeldung setzen
                textBox.FillColor = Color.LightPink; // Hintergrundfarbe ändern
                return false;
            }
            else
            {
                errorProvider1.SetError(textBox, null); // Fehlermeldung entfernen
                textBox.FillColor = Color.White; // Hintergrundfarbe zurücksetzen
                return true;
            }
        }

        /// <summary>
        /// Überprüft die Gültigkeit aller notwendigen Eingabefelder im Formular.
        /// </summary>
        /// <returns>True, wenn alle Felder gültig sind; False, wenn mindestens ein Feld ungültig ist.</returns>
        private bool _istValidiert()
        {
            bool isValid = true;

            // Führt die Validierung für jedes Pflichtfeld aus.
            // Der '&=' Operator stellt sicher, dass alle Validierungsfunktionen aufgerufen werden,
            // auch wenn eine vorherige Validierung fehlschlägt.
            isValid = _TextFelderValidierung(txtParfümNummer, "ParfümNummer");
            isValid &= _TextFelderValidierung(txtMarke, "Marke");
            isValid &= _TextFelderValidierung(txtName, "Name");
            isValid &= _TextFelderValidierung(txtKategorie, "Kategorie");
            // Fügen Sie hier bei Bedarf weitere Validierungen für andere Felder hinzu (z.B. Duftrichtung, Basisnote).

            return isValid;
        }

        /// <summary>
        /// Ruft die Details eines Parfüms aus der Datenbank ab und füllt die Formularfelder.
        /// Wird im Update-Modus verwendet.
        /// </summary>
        private void _holeParfümDatenFromDatenbank()
        {
            _parfüms = clsParfüms.FindByParfümNummer(_parfümNummer); // Parfümdaten abrufen

            if (_parfüms != null)
            {
                // Daten aus dem geladenen _parfüms-Objekt in die UI-Felder übertragen
                txtParfümNummer.Text = _parfüms.parfümNummer.ToString();
                txtMarke.Text = _parfüms.Marke;
                txtName.Text = _parfüms.Name;
                txtKategorie.Text = _parfüms.Kategorie;
                txtDuftrichtung.Text = _parfüms.Duftrichtung;
                txtBasisnote.Text = _parfüms.Basisnote;

                // Checkboxen basierend auf den Booleschen Werten setzen
                chbIstVorhanden.Checked = _parfüms.IstVorhanden;
                chbInBestellung.Checked = _parfüms.InBestellung;
            }
            else
            {
                // Fehlermeldung anzeigen und Formular schließen, falls das Parfüm nicht gefunden wurde.
                MessageBox.Show("Fehler beim Laden der Parfümdaten ist aufgetreten.", "Fehlermeldung",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.DialogResult = DialogResult.Abort; // Setzt das Ergebnis auf "Abbruch" bei Ladefehler
                this.Close(); // Schließt das Formular
            }
        }

        /// <summary>
        /// Prüft im Update-Modus, ob die ParfümNummer im Textfeld mit der ursprünglichen Nummer übereinstimmt.
        /// </summary>
        /// <returns>True, wenn die Nummern gleich sind; False, wenn sie sich unterscheiden oder nicht im Update-Modus.</returns>
        private bool _IstAlteParfümNummerGleichWieNeue()
        {
            // Diese Prüfung ist nur relevant, wenn sich das Formular im Update-Modus befindet
            // und das _parfüms-Objekt bereits initialisiert wurde.
            if (_mode == enMode.update && _parfüms != null)
            {
                int alteParfümNummer = _parfüms.parfümNummer;
                int AktuelleParfümNummer = Convert.ToInt32(txtParfümNummer.Text);

                return alteParfümNummer == AktuelleParfümNummer;
            }
            return false; // Gibt false zurück, wenn nicht im Update-Modus oder _parfüms ist null.
        }

        /// <summary>
        /// Füllt das interne _parfüms-Objekt mit den aktuellen Werten aus den Formularfeldern.
        /// </summary>
        private void _fülleParfümDaten()
        {
            // Setzt die ParfümNummer. Im Update-Modus wird 'neuParfümNummer' verwendet (falls in clsParfüms definiert),
            // ansonsten 'parfümNummer' für neue Einträge.
            if (_mode == enMode.update)
                _parfüms.neuParfümNummer = Convert.ToInt32(txtParfümNummer.Text);
            else
                _parfüms.parfümNummer = Convert.ToInt32(txtParfümNummer.Text);

            // Zuweisung der Textfeldwerte zu den Eigenschaften des Parfüm-Objekts
            _parfüms.Marke = txtMarke.Text;
            _parfüms.Name = txtName.Text;
            _parfüms.Kategorie = txtKategorie.Text;
            _parfüms.Duftrichtung = txtDuftrichtung.Text;
            _parfüms.Basisnote = txtBasisnote.Text;

            // Direkte Zuweisung der Checked-Eigenschaft von Checkboxen
            _parfüms.IstVorhanden = chbIstVorhanden.Checked;
            _parfüms.InBestellung = chbInBestellung.Checked;
        }

        /// <summary>
        /// Prüft, ob die im Textfeld eingegebene ParfümNummer bereits in der Datenbank existiert
        /// und nicht die ursprüngliche Nummer im Update-Modus ist.
        /// </summary>
        /// <returns>True, wenn die ParfümNummer vergeben ist und geändert wurde; False sonst.</returns>
        private bool _IstParfümNummerVergeben()
        {
            int parfümNummer = Convert.ToInt32(txtParfümNummer.Text);

            // Prüft, ob die Nummer bereits existiert UND ob es sich NICHT um die alte Nummer im Update-Modus handelt.
            // Dies verhindert eine Kollision, wenn die Nummer im Update-Modus unverändert bleibt.
            if (clsParfüms.IstParfümNummerVergeben(parfümNummer) && !_IstAlteParfümNummerGleichWieNeue())
            {
                MessageBox.Show("Diese Nummer ist bereits vergeben!\nBitte versuchen Sie eine andere Nummer.", "Hinweis",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// Speichert oder aktualisiert die Parfümdaten in der Datenbank.
        /// Führt Validierungen durch und kommuniziert mit der Business-Schicht.
        /// </summary>
        private void _parfümDatenSpeichern()
        {
            // 1. Überprüfung der Textfeld-Validierung
            if (!_istValidiert())
                return;

            // 2. Überprüfung, ob die ParfümNummer bereits vergeben ist (bei neuen oder geänderten Nummern)
            if (_IstParfümNummerVergeben())
                return;

            // Stellt sicher, dass das _parfüms-Objekt im addnew-Modus initialisiert wird,
            // falls es beim Load-Ereignis nicht passierte.
            if (_parfüms == null)
            {
                _parfüms = new clsParfüms();
            }

            // 3. Füllen des _parfüms-Objekts mit den aktuellen Daten aus dem Formular
            _fülleParfümDaten();

            string statusMessage;
            if (_mode == enMode.addnew)
                statusMessage = "hinzugefügt"; // Statusmeldung für das Hinzufügen
            else
                statusMessage = "aktualisiert"; // Statusmeldung für das Aktualisieren

            // 4. Daten in der Business-Schicht speichern
            if (_parfüms.Save())
            {
                // Bei erfolgreichem Speichern die öffentlichen Eigenschaften für die Rückgabe an die aufrufende Form setzen
                this.SavedParfumName = _parfüms.Name;
                this.SavedParfumKategorie = _parfüms.Kategorie;

                // WICHTIG: Die Aktualisierung des Autovervollständigungsbaums sollte NICHT HIER erfolgen.
                // Der Baum (AVLTree) sollte eine globale/statische Instanz in der Hauptform sein
                // und DORT aktualisiert werden, nachdem dieses Dialogfeld geschlossen wurde.
                // Ein lokaler Baum hier wäre ineffizient und nutzlos.

                // Signalisiert der aufrufenden Form, dass die Operation erfolgreich war.
                this.DialogResult = DialogResult.OK;
                this.Close(); // Schließt das Formular
            }
            else
            {
                // Fehlermeldung anzeigen, falls der Speichervorgang fehlschlägt
                MessageBox.Show($"Fehler beim {statusMessage} ist aufgetreten.", "Fehler",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.DialogResult = DialogResult.None; // Setzt das Ergebnis auf "Keine Aktion" bei Fehler
            }
        }

        /// <summary>
        /// Event-Handler für den "Bearbeiten"-Button.
        /// Diese Methode scheint dazu gedacht zu sein, die Basisnoten-Eingabe zu formatieren/bereinigen.
        /// </summary>
        private void btnBearbeiten_Click(object sender, EventArgs e)
        {
            string input = txtBasisnote.Text;
            List<string> scentNotes = new List<string>();

            // Die Eingabe in Wörter aufteilen, basierend auf Leerzeichen, Kommas und Semikolons.
            // Leere Einträge werden entfernt.
            string[] words = input.Split(new char[] { ' ', ',', ';' }, StringSplitOptions.RemoveEmptyEntries);

            foreach (string word in words)
            {
                // Halbierung der Wörter (z.B. "ZederZeder" wird zu "Zeder").
                // Dies ist eine sehr spezifische Logik; stellen Sie sicher, dass dies das gewünschte Verhalten ist.
                int half = word.Length / 2;
                string halvedWord = word.Substring(0, half);

                // Fügt das halbierte Wort zur Liste hinzu, wenn es noch nicht enthalten ist, um Duplikate zu vermeiden.
                if (!scentNotes.Contains(halvedWord))
                {
                    scentNotes.Add(halvedWord);
                }
            }

            // Die bereinigten Duftnoten werden in einem durch Kommas getrennten String formatiert.
            string result = string.Join(", ", scentNotes);
            txtBasisnote.Text = result; // Aktualisiert das Textfeld mit dem formatierten Ergebnis
        }

        /// <summary>
        /// Event-Handler für das Laden des Formulars.
        /// Setzt den Modus (Add/Update) und lädt bei Bedarf Daten.
        /// </summary>
        private void frmAddUpdateParfüms_Load(object sender, EventArgs e)
        {
            // Wenn _parfümNummer nicht -1 ist, bedeutet dies, dass ein bestehendes Parfüm geladen werden soll (Update-Modus).
            if (_parfümNummer != -1)
            {
                _mode = enMode.update; // Setzt den Modus auf "Update"
                _holeParfümDatenFromDatenbank(); // Lädt die Daten des Parfüms aus der Datenbank
                // Optional: txtParfümNummer im Update-Modus schreibgeschützt machen,
                // wenn die ParfümNummer nicht geändert werden soll.
                // txtParfümNummer.ReadOnly = true; 
            }
            else
            {
                // Wenn _parfümNummer -1 ist, wird ein neues Parfüm hinzugefügt (AddNew-Modus).
                _parfüms = new clsParfüms(); // Erstellt ein neues, leeres Parfüm-Objekt
                _mode = enMode.addnew; // Setzt den Modus auf "AddNew"
            }
            // Optional: Sicherstellen, dass txtParfümNummer im Add-Modus editierbar ist,
            // falls es im Designer standardmäßig auf ReadOnly steht.
            // if (_mode == enMode.addnew)
            // {
            //     txtParfümNummer.ReadOnly = false;
            // }
        }
    }
}

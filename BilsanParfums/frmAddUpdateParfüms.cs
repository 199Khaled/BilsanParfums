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
using System.IO;

namespace BilsanParfums
{
    public partial class frmAddUpdateParfüms : Form
    {
        // Enum zur Definition des Formularmodus: Hinzufügen eines neuen Parfüms oder Aktualisieren eines bestehenden
        private enum enMode { addnew = 0, update = 1 }

        // Aktueller Betriebsmodus des Formulars
        private enMode _mode = enMode.addnew;

        // Instanz der clsParfüms-Klasse, die die aktuellen Parfümdaten speichert und verwaltet
        clsNeueParfümDaten _parfüms;

        // Die ParfümNummer, die beim Initialisieren des Formulars übergeben wird.
        // -1 bedeutet, dass ein neues Parfüm hinzugefügt werden soll (addnew).
        // Eine positive Zahl bedeutet, dass ein bestehendes Parfüm aktualisiert werden soll (update).
        string _ParfümCode;
        private string _ausgewählterBildPfad = null;
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
        public frmAddUpdateParfüms(string ParfümCode)
        {
            InitializeComponent(); // Initialisiert die Komponenten des Formulars (UI-Elemente)
            this._ParfümCode = ParfümCode; // Speichert die übergebene ParfümNummer
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
            isValid = _TextFelderValidierung(txtParfümNummer, "ParfümCode");
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
            _parfüms = clsNeueParfümDaten.FindByParfümNummer(_ParfümCode); // Parfümdaten abrufen

            if (_parfüms != null)
            {
                // Daten aus dem geladenen _parfüms-Objekt in die UI-Felder übertragen
                txtAlteNummer.Text = _parfüms.AlteNummer.ToString();
                txtParfümNummer.Text = _parfüms.ParfümCode;
                txtMarke.Text = _parfüms.Marke;
                txtName.Text = _parfüms.Name;
                txtKategorie.Text = _parfüms.Kategorie;
                txtDuftrichtung.Text = _parfüms.Duftrichtung;
    
                // Checkboxen basierend auf den Booleschen Werten setzen
                chbIstVorhanden.Checked = _parfüms.IstVorhanden;
                chbInBestellung.Checked = _parfüms.InBestellung;
                cbIstNeu.Checked = _parfüms.IstNeu;

                // NEU
                _LadeParfümBild(_parfüms.ParfümCode);
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
                string alteParfümCode = _parfüms.ParfümCode;
                string AktuelleParfümCode = txtParfümNummer.Text.Trim();

                return alteParfümCode == AktuelleParfümCode;
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
                _parfüms.neuParfümCode = txtParfümNummer.Text.Trim();
            else
                _parfüms.ParfümCode = txtParfümNummer.Text.Trim();

            // Zuweisung der Textfeldwerte zu den Eigenschaften des Parfüm-Objekts
            if (!string.IsNullOrEmpty(txtAlteNummer.Text))
                _parfüms.AlteNummer = Convert.ToInt32(txtAlteNummer.Text);
            else
                _parfüms.AlteNummer = null;

            _parfüms.Marke = txtMarke.Text.Trim();
            _parfüms.Name = txtName.Text.Trim();
            _parfüms.Kategorie = txtKategorie.Text.Trim();
            _parfüms.Duftrichtung = txtDuftrichtung.Text.Trim();
          
            // Direkte Zuweisung der Checked-Eigenschaft von Checkboxen
            _parfüms.IstVorhanden = chbIstVorhanden.Checked;
            _parfüms.InBestellung = chbInBestellung.Checked;
            _parfüms.IstNeu = cbIstNeu.Checked;
        }

        /// <summary>
        /// Prüft, ob die im Textfeld eingegebene ParfümNummer bereits in der Datenbank existiert
        /// und nicht die ursprüngliche Nummer im Update-Modus ist.
        /// </summary>
        /// <returns>True, wenn die ParfümNummer vergeben ist und geändert wurde; False sonst.</returns>
        private bool _IstParfümNummerVergeben()
        {
            string ParfümCode = txtParfümNummer.Text.Trim();

            // Prüft, ob die Nummer bereits existiert UND ob es sich NICHT um die alte Nummer im Update-Modus handelt.
            // Dies verhindert eine Kollision, wenn die Nummer im Update-Modus unverändert bleibt.
            if (clsNeueParfümDaten.IstParfümNummerVergeben(ParfümCode) && !_IstAlteParfümNummerGleichWieNeue())
            {
                MessageBox.Show("Dieser ParfümCode ist bereits vergeben!\nBitte versuchen Sie einen anderen ParfümCode.", "Hinweis",
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
            if (!_istValidiert())
                return;

            if (_IstParfümNummerVergeben())
                return;

            if (_parfüms == null)
                _parfüms = new clsNeueParfümDaten();

            // Alten Code merken, bevor _fülleParfümDaten() läuft
            string alterParfümCode =
                _mode == enMode.update
                    ? _parfüms.ParfümCode
                    : null;

            _fülleParfümDaten();

            string neuerParfümCode =
                txtParfümNummer.Text.Trim();

            string statusMessage =
                _mode == enMode.addnew
                    ? "hinzugefügt"
                    : "aktualisiert";

            if (_parfüms.Save())
            {
                // Falls Code beim Update geändert wurde:
                if (_mode == enMode.update &&
                    alterParfümCode != neuerParfümCode)
                {
                    _BenenneParfümBildUm(
                        alterParfümCode,
                        neuerParfümCode);
                }

                // Falls ein neues Bild ausgewählt wurde:
                _SpeichereParfümBild(neuerParfümCode);

                this.SavedParfumName = _parfüms.Name;
                this.SavedParfumKategorie = _parfüms.Kategorie;

                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            else
            {
                MessageBox.Show(
                    $"Fehler beim {statusMessage} ist aufgetreten.",
                    "Fehler",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);

                this.DialogResult = DialogResult.None;
            }
        }

        /// <summary>
        /// Event-Handler für den "Bearbeiten"-Button.
        /// Diese Methode scheint dazu gedacht zu sein, die Basisnoten-Eingabe zu formatieren/bereinigen.
        /// </summary>


        /// <summary>
        /// Event-Handler für das Laden des Formulars.
        /// Setzt den Modus (Add/Update) und lädt bei Bedarf Daten.
        /// </summary>
        private void frmAddUpdateParfüms_Load(object sender, EventArgs e)
        {
            // Wenn _parfümNummer nicht -1 ist, bedeutet dies, dass ein bestehendes Parfüm geladen werden soll (Update-Modus).
            if (!string.IsNullOrEmpty(  _ParfümCode))
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
                _parfüms = new clsNeueParfümDaten(); // Erstellt ein neues, leeres Parfüm-Objekt
                _mode = enMode.addnew; // Setzt den Modus auf "AddNew"
            }
            // Optional: Sicherstellen, dass txtParfümNummer im Add-Modus editierbar ist,
            // falls es im Designer standardmäßig auf ReadOnly steht.
            // if (_mode == enMode.addnew)
            // {
            //     txtParfümNummer.ReadOnly = false;
            // }
        }

        private void btnBildauswählen_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog dialog = new OpenFileDialog())
            {
                dialog.Title = "Parfümbild auswählen";
                dialog.Filter =
                    "Bilddateien|*.jpg;*.jpeg;*.png;*.bmp|Alle Dateien|*.*";

                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    _ausgewählterBildPfad = dialog.FileName;

                    // Bild anzeigen, ohne die Originaldatei dauerhaft zu sperren
                    using (Image temp = Image.FromFile(dialog.FileName))
                    {
                        pbParfümbild.Image = new Bitmap(temp);
                    }

                    pbParfümbild.SizeMode = PictureBoxSizeMode.Zoom;
                }
            }
        }
        private void _SpeichereParfümBild(string parfümCode)
        {
            if (string.IsNullOrWhiteSpace(_ausgewählterBildPfad))
                return;

            if (string.IsNullOrWhiteSpace(parfümCode))
                return;

            string bilderOrdner =
                Path.Combine(Application.StartupPath, "Bilder");

            Directory.CreateDirectory(bilderOrdner);

            string zielPfad =
                Path.Combine(bilderOrdner, parfümCode + ".jpg");

            try
            {
                using (Image original = Image.FromFile(_ausgewählterBildPfad))
                using (Bitmap bitmap = new Bitmap(original))
                {
                    bitmap.Save(
                        zielPfad,
                        System.Drawing.Imaging.ImageFormat.Jpeg);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Das Parfümbild konnte nicht gespeichert werden.\n\n" +
                    ex.Message,
                    "Fehler",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }
        private void _LadeParfümBild(string parfümCode)
        {
            string bilderOrdner =
                Path.Combine(Application.StartupPath, "Bilder");

            string bildPfad =
                Path.Combine(bilderOrdner, parfümCode + ".jpg");

            if (!File.Exists(bildPfad))
            {
               pbParfümbild.Image = null;
                return;
            }

            try
            {
                using (Image temp = Image.FromFile(bildPfad))
                {
                    pbParfümbild.Image = new Bitmap(temp);
                }

                pbParfümbild.SizeMode = PictureBoxSizeMode.Zoom;
            }
            catch
            {
                pbParfümbild.Image = null;
            }
        }
        private void _BenenneParfümBildUm(
         string alterCode,
          string neuerCode)
        {
            if (string.IsNullOrWhiteSpace(alterCode) ||
                string.IsNullOrWhiteSpace(neuerCode) ||
                alterCode == neuerCode)
                return;

            string bilderOrdner =
                Path.Combine(Application.StartupPath, "Bilder");

            string alterPfad =
                Path.Combine(bilderOrdner, alterCode + ".jpg");

            string neuerPfad =
                Path.Combine(bilderOrdner, neuerCode + ".jpg");

            if (!File.Exists(alterPfad))
                return;

            if (File.Exists(neuerPfad))
                File.Delete(neuerPfad);

            File.Move(alterPfad, neuerPfad);
        }
    }
}

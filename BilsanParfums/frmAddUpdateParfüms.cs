using Busnisse_Layer;
using clsHilfsMethoden;
using Guna.UI2.WinForms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static Busnisse_Layer.clsParfüms;

namespace BilsanParfums
{
    public partial class frmAddUpdateParfüms : Form
    {
        private enum enMode { addnew = 0, update = 1 }
        enMode _mode = enMode.addnew;
        clsParfüms _parfüms;
        int _parfümNummer;
        public frmAddUpdateParfüms(int parfümNummer)
        {
            InitializeComponent();
            this._parfümNummer = parfümNummer;
        }
        private void btnAbbrechen_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnspeichern_Click(object sender, EventArgs e)
        {
            _parfümDatenSpeichern();
        }
        private bool _TextFelderValidierung(Guna2TextBox name, string feldname)
        {
            if (string.IsNullOrEmpty(name.Text))
            {
                errorProvider1.SetError(name, feldname + " darf nicht leer sein!");
                name.FillColor = Color.LightPink;
                return false;
            }
            else
            {
                errorProvider1.SetError(name, null);
                name.FillColor = Color.White;
                return true;
            }
        }
        private bool _istValidiert()
        {
            bool istValid = true;

            istValid = _TextFelderValidierung(txtParfümNummer, "pafrümNummer");
            istValid &= _TextFelderValidierung(txtMarke, "Marke");
            istValid &= _TextFelderValidierung(txtName, "Name");
            istValid &= _TextFelderValidierung(txtKategorie, "Kategorie");

            return istValid;
        }
        private void _holeParfümDatenFromDatenbank()
        {
            _parfüms = clsParfüms.FindByParfümNummer(_parfümNummer);

            if (_parfüms != null)
            {
                txtParfümNummer.Text = _parfüms.parfümNummer.ToString();
                txtMarke.Text = _parfüms.Marke;
                txtName.Text = _parfüms.Name;
                txtKategorie.Text = _parfüms.Kategorie;
                txtDuftrichtung.Text = _parfüms.Duftrichtung;
                txtBasisnote.Text = _parfüms.Basisnote;

                chbIstVorhanden.Checked = _parfüms.IstVorhanden;
                chbInBestellung.Checked = _parfüms.InBestellung;
            }
            else
            {
                MessageBox.Show("Fehler beim Laden der Parfümdaten ist aufgetreten", "fehlermeldung",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
        }
        private bool _IstAlteParfümNummerGleichWieNeue()
        {
            int alteParfümNummer = 0;
            int AktuelleParfümNummer = 0;
            if (_mode == enMode.update)
            {
                alteParfümNummer = _parfüms.parfümNummer;
                AktuelleParfümNummer = Convert.ToInt32(txtParfümNummer.Text);

                if (alteParfümNummer == AktuelleParfümNummer)
                    return true;
            }
            return false;
        }
        private void _fülleParfümDaten()
        {
            if (_mode == enMode.update)
                _parfüms.neuParfümNummer = Convert.ToInt32(txtParfümNummer.Text);
            else
                _parfüms.parfümNummer = Convert.ToInt32(txtParfümNummer.Text);

            _parfüms.Marke = txtMarke.Text;
            _parfüms.Name = txtName.Text;
            _parfüms.Kategorie = txtKategorie.Text;
            _parfüms.Duftrichtung = txtDuftrichtung.Text;
            _parfüms.Basisnote = txtBasisnote.Text;
            _parfüms.IstVorhanden = chbIstVorhanden.Checked == true ? true : false;
            _parfüms.InBestellung = chbInBestellung.Checked == true ? true : false;
        }
        private bool _IstParfümNummerVergeben()
        {
            int parfümNummer = Convert.ToInt32(txtParfümNummer.Text);
            if (clsParfüms.IstParfümNummerVergeben(parfümNummer) && !_IstAlteParfümNummerGleichWieNeue())
            {
                MessageBox.Show("Diese Nummer ist vergeben\nVersuchen Sie bitte " +
                    "mit anderer Nummer erneut", "Hinweis", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return true;
            }
            else { return false; }
        }
        private void _parfümDatenSpeichern()
        {
            if (!_istValidiert())  //wir überprüfen, ob bestimmte felder leer sind.
                return;

            if (_IstParfümNummerVergeben()) // wir überprüfen, ob die Nummer bereits vorhanden ist.
                return;

            _fülleParfümDaten();

            string status;
            if (_mode == enMode.addnew)
                status = "hinzugefügt";
            else
                status = "aktualisiert";

            AutoComplete.AVLTree tree = new AutoComplete.AVLTree();
            if (_parfüms.Save())
            {
                tree.Insert(_parfüms.Name); // Hier addieren wir jeden neuen Parfüm in AVL Tree.
                this.Close();
            }
            else
                MessageBox.Show($"Fehler beim {status} ist aufgetreten.", "Erorr",
                   MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void btnBearbeiten_Click(object sender, EventArgs e)
        {

            string eingabe = txtBasisnote.Text;
            List<string> duftnoten = new List<string>();

            // Eingabe in Wörter aufteilen
            string[] wörter = eingabe.Split(new char[] { ' ', ',', ';' }, StringSplitOptions.RemoveEmptyEntries);

            foreach (string wort in wörter)
            {
                // Jeden Eintrag halbieren (doppelte Worte wie "ZederZeder")
                int halb = wort.Length / 2;
                string halbiertesWort = wort.Substring(0, halb);

                // Nur hinzufügen, wenn noch nicht in der Liste
                if (!duftnoten.Contains(halbiertesWort))
                {
                    duftnoten.Add(halbiertesWort);
                }
            }

            // Ausgabe in einem schönen Format
            string ergebnis = string.Join(", ", duftnoten);
            txtBasisnote.Text = ergebnis; // oder irgendwo speichern
        }

        private void frmAddUpdateParfüms_Load(object sender, EventArgs e)
        {
            if (_parfümNummer != -1)
            {
                _mode = enMode.update;
                _holeParfümDatenFromDatenbank();
            }
            else
            {
                _parfüms = new clsParfüms();
                _mode = enMode.addnew;
            }
        }
    }
}

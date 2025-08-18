using Data_Layer;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Busnisse_Layer
{
    public class clsNeueParfümDaten
    {
        public enum enMode { addnew = 0, update = 1 }
        private enMode _mode = enMode.addnew;
        public int? AlteNummer { get; set; }
        public int? neuAlteNummer { get; set; }
        public int parfümNummer { get; set; }
        public int neuParfümNummer { get; set; }
        public string Marke { get; set; }
        public string Name { get; set; }
        public string Kategorie { get; set; }
        public string Duftrichtung { get; set; }
        public string Basisnote { get; set; }

        public bool IstVorhanden { get; set; }
        public bool InBestellung { get; set; }

        private clsNeueParfümDaten(int? alteNummer, int parfümNummer, string marke, string name, string kategorie,
          string duftrichtung, string Basisnote,
          bool istVorhande, bool InBestellung)
        {
            this.AlteNummer = alteNummer;
            this.parfümNummer = parfümNummer;
            this.Marke = marke;
            this.Name = name;
            this.Kategorie = kategorie;
            this.Duftrichtung = duftrichtung;
            this.Basisnote = Basisnote;

            this.IstVorhanden = istVorhande;
            this.InBestellung = InBestellung;

            _mode = enMode.update;
        }

        public clsNeueParfümDaten()
        {
            this.AlteNummer = null;
            this.parfümNummer = -1;
            this.Marke = string.Empty;
            this.Name = string.Empty;
            this.Kategorie = string.Empty;
            this.Duftrichtung = string.Empty;
            this.Basisnote = string.Empty;

            this.IstVorhanden = true;
            this.InBestellung = false;

            _mode = enMode.addnew;
        }

        public static clsNeueParfümDaten FindByParfümNummer(int parfümNummer)
        {
            int? alteNummer = null;
            string marke = string.Empty; string name = string.Empty; string kategorie = string.Empty;
            string duftrichtung = string.Empty; string Basisnote = string.Empty;
            ; bool IstVorhanden = false;
            bool InBestellung = false;

            if (clsNeueParfümDatenzugriff.Find(ref alteNummer, parfümNummer, ref marke, ref name, ref kategorie,
                ref duftrichtung, ref Basisnote, ref IstVorhanden, ref InBestellung))
            {
                return new clsNeueParfümDaten(alteNummer, parfümNummer, marke, name, kategorie, duftrichtung, Basisnote,
                                     IstVorhanden, InBestellung);
            }
            else
                return null;
        }

        private bool _Addnew()
        {
            if (clsNeueParfümDatenzugriff.AddNewPerfum(this.AlteNummer,
                this.parfümNummer, this.Marke, this.Name,
                 this.Kategorie, this.Duftrichtung, this.Basisnote
                , this.IstVorhanden, this.InBestellung))
            {
                return true;
            }
            else
                return false;
        }

        private bool _Update()
        {
            return clsNeueParfümDatenzugriff.UpdatePerfum(this.AlteNummer,
                this.neuParfümNummer, this.parfümNummer, this.Marke, this.Name,
                this.Kategorie, this.Duftrichtung, this.Basisnote
                      , this.IstVorhanden, this.InBestellung);
        }

        public static bool Delete(int parfümNummer)
        {
            return clsNeueParfümDatenzugriff.Delete(parfümNummer);
        }

        public bool Delete()
        {
            return clsNeueParfümDatenzugriff.Delete(this.parfümNummer);
        }
        public bool Save()
        {
            switch (_mode)
            {
                case enMode.addnew:
                    if (_Addnew())
                    {
                        _mode = enMode.update;
                        return true;
                    }
                    else
                        return false;

                case enMode.update:
                    return _Update();
            }
            return false;
        }
        public static DataTable GetAllParfüms()
        {
            return clsNeueParfümDatenzugriff.GetAllParfüms();
        }

        public static DataTable GetAllHerrenParfüms()
        {
            return clsNeueParfümDatenzugriff.GetAllHerrenParfüms();
        }

        public static DataTable GetAllDamenParfüms()
        {
            return clsNeueParfümDatenzugriff.GetAllDamenParfüms();
        }
        public static DataTable GetAllUnisexParfüms()
        {
            return clsNeueParfümDatenzugriff.GetAllUnisexParfüms();
        }
        public static DataTable GetAllKinderParfüms()
        {
            return clsNeueParfümDatenzugriff.GetAllKinderParfüms();
        }
        public static DataTable GetAllOrientalischeParfüms()
        {
            return clsNeueParfümDatenzugriff.GetAllOrientalischeParfüms();
        }


        public static bool IstParfümNummerVergeben(int parfümNummer)
        {
            return clsNeueParfümDatenzugriff._IstParfümNummerVergeben(parfümNummer);
        }
        public static DataTable GetAllParfuemByName(string filterdName)
        {
            return clsNeueParfümDatenzugriff.GetAllParfuemByName(filterdName);
        }
        public static DataTable GetAllParfuemByMarke(string filterdMarke)
        {
            return clsNeueParfümDatenzugriff.GetAllParfuemByMarke(filterdMarke);
        }
    }
}


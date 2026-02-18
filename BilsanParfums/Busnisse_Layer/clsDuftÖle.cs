
using System;
using System.Data;
using BilsanDb_DataLayer;

namespace BilsanDb_BusinessLayer
{
    public class clsDuftÖle
    {
        //#nullable enable

        public enum enMode { AddNew = 0, Update = 1 };
        public enMode Mode = enMode.AddNew;

        public int? ID { get; set; }
        public int? AlteNummer { get; set; } = null;
        public string ParfümCode { get; set; }
        public int? Ölmenge { get; set; } = null;
        public string Öltype { get; set; }
        public DateTime? Aktivierungsdatum { get; set; } = null;


        public clsDuftÖle()
        {
            this.ID = null;
            this.AlteNummer = 0;
            this.ParfümCode = null;
            this.Ölmenge = 0;
            this.Öltype = null;
            this.Aktivierungsdatum = DateTime.Now;


            Mode = enMode.AddNew;
        }


        private clsDuftÖle(
int? ID, int? AlteNummer, int? Ölmenge, DateTime? Aktivierungsdatum, string ParfümCode = null, string Öltype = null)        {
            this.ID = ID;
            this.AlteNummer = AlteNummer;
            this.ParfümCode = ParfümCode;
            this.Ölmenge = Ölmenge;
            this.Öltype = Öltype;
            this.Aktivierungsdatum = Aktivierungsdatum;
            Mode = enMode.Update;
        }


       private bool _AddNewDuftÖle()
       {
        this.ID = clsDuftÖleData.AddNewDuftÖle(
this.AlteNummer, this.Ölmenge, this.Aktivierungsdatum, this.ParfümCode, this.Öltype);
        return (this.ID != null);
       }


       public static bool AddNewDuftÖle(
ref int? ID, int? AlteNummer, int? Ölmenge, DateTime? Aktivierungsdatum, string ParfümCode = null, string Öltype = null)        {
        ID = clsDuftÖleData.AddNewDuftÖle(
AlteNummer, Ölmenge, Aktivierungsdatum, ParfümCode, Öltype);

        return (ID != null);

       }


       private bool _UpdateDuftÖle()
       {
        return clsDuftÖleData.UpdateDuftÖleByID(
ID, AlteNummer, Ölmenge, Aktivierungsdatum, ParfümCode, Öltype);
       }


       public static bool UpdateDuftÖleByID(
int? ID, int? AlteNummer, int? Ölmenge, DateTime? Aktivierungsdatum, string ParfümCode = null, string Öltype = null)        {
        return clsDuftÖleData.UpdateDuftÖleByID(
ID, AlteNummer, Ölmenge, Aktivierungsdatum, ParfümCode, Öltype);

        }


       public static clsDuftÖle FindByID(int? ID)

        {
            if (ID == null)
            {
                return null;
            }
            int? AlteNummer = 0;
            string ParfümCode = "";
            int? Ölmenge = 0;
            string Öltype = "";
            DateTime? Aktivierungsdatum = DateTime.Now;
            bool IsFound = clsDuftÖleData.GetDuftÖleInfoByID(ID,
 ref AlteNummer,  ref ParfümCode,  ref Ölmenge,  ref Öltype,  ref Aktivierungsdatum);

           if (IsFound)
               return new clsDuftÖle(
ID, AlteNummer, Ölmenge, Aktivierungsdatum, ParfümCode, Öltype);
            else
                return null;
            }


       public static DataTable GetAllDuftÖle()
       {

        return clsDuftÖleData.GetAllDuftÖle();

       }



        public bool Save()
        {
            switch (Mode)
            {
                case enMode.AddNew:
                    if(_AddNewDuftÖle())
                    {
                        Mode = enMode.Update;
                         return true;
                    }
                    else
                    {
                        return false;
                    }
                case enMode.Update:
                    return _UpdateDuftÖle();

            }
        
            return false;
        }



       public bool DeleteDuftÖle()
       {

        return clsDuftÖleData.DeleteDuftÖle(this.ID);

       }


        public enum DuftÖleColumn
         {
            ID,
            AlteNummer,
            ParfümCode,
            Ölmenge,
            Öltype,
            Aktivierungsdatum
         }


        public enum SearchMode
        {
            Anywhere,
            StartsWith,
            EndsWith,
            ExactMatch
        }
    

        public static DataTable SearchData(DuftÖleColumn ChosenColumn, string SearchValue, SearchMode Mode = SearchMode.Anywhere)
        {
            if (string.IsNullOrWhiteSpace(SearchValue) || !SqlHelper.IsSafeInput(SearchValue))
                return new DataTable();

            string modeValue = Mode.ToString(); // Get the mode as string for passing to the stored procedure

            return clsDuftÖleData.SearchData(ChosenColumn.ToString(), SearchValue, modeValue);
        }        



    }
}

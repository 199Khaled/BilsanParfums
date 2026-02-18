
using System;
using System.Data;
using BilsanDb_DataLayer;

namespace BilsanDb_BusinessLayer
{
    public class clsFlakons
    {
        //#nullable enable

        public enum enMode { AddNew = 0, Update = 1 };
        public enMode Mode = enMode.AddNew;

        public int? FlakonID { get; set; }
        public string Form { get; set; }
        public string Verschlussart { get; set; }
        public string Farbe { get; set; }
        public string FlakonsMengeInMl { get; set; }
        public int? Verbleibende_Flakons { get; set; } = null;
        public DateTime? Aktivierungsdatum { get; set; } = null;


        public clsFlakons()
        {
            this.FlakonID = null;
            this.Form = null;
            this.Verschlussart = null;
            this.Farbe = null;
            this.FlakonsMengeInMl = null;
            this.Verbleibende_Flakons = 0;
            this.Aktivierungsdatum = DateTime.Now;


            Mode = enMode.AddNew;
        }


        private clsFlakons(
int? FlakonID, string FlakonsMengeInMl,int? Verbleibende_Flakons, DateTime? Aktivierungsdatum, string Form = null, string Verschlussart = null, string Farbe = null)        {
            this.FlakonID = FlakonID;
            this.Form = Form;
            this.Verschlussart = Verschlussart;
            this.Farbe = Farbe;
            this.FlakonsMengeInMl = FlakonsMengeInMl;
            this.Verbleibende_Flakons = Verbleibende_Flakons;
            this.Aktivierungsdatum = Aktivierungsdatum;
            Mode = enMode.Update;
        }


       private bool _AddNewFlakons()
       {
        this.FlakonID = clsFlakonsDatenzugriff.AddNewFlakons(
this.FlakonsMengeInMl, this.Verbleibende_Flakons, this.Aktivierungsdatum, this.Form, this.Verschlussart, this.Farbe);
        return (this.FlakonID != null);
       }


       public static bool AddNewFlakons(
ref int? FlakonID, string FlakonsMengeInMl, int? Verbleibende_Flakons, DateTime? Aktivierungsdatum, string Form = null, string Verschlussart = null, string Farbe = null)        {
        FlakonID = clsFlakonsDatenzugriff.AddNewFlakons(
FlakonsMengeInMl,  Verbleibende_Flakons, Aktivierungsdatum, Form, Verschlussart, Farbe);

        return (FlakonID != null);

       }


       private bool _UpdateFlakons()
       {
        return clsFlakonsDatenzugriff.UpdateFlakonsByID(
FlakonID, FlakonsMengeInMl, Verbleibende_Flakons, Aktivierungsdatum, Form, Verschlussart, Farbe);
       }

        
       public static bool UpdateFlakonsByID(
int? FlakonID, string FlakonsMengeInMl,  int? Verbleibende_Flakons, DateTime? Aktivierungsdatum, string Form = null, string Verschlussart = null, string Farbe = null)        {
        return clsFlakonsDatenzugriff.UpdateFlakonsByID(
FlakonID, FlakonsMengeInMl,  Verbleibende_Flakons, Aktivierungsdatum, Form, Verschlussart, Farbe);

        }


       public static clsFlakons FindByFlakonID(int? FlakonID)

        {
            if (FlakonID == null)
            {
                return null;
            }
            string Form = "";
            string Verschlussart = "";
            string Farbe = "";
            string FlakonsMengeInMl = "";
            int? Verbleibende_Flakons = 0;
            DateTime? Aktivierungsdatum = DateTime.Now;
            bool IsFound = clsFlakonsDatenzugriff.GetFlakonsInfoByID(FlakonID,
 ref Form,  ref Verschlussart,  ref Farbe,  ref FlakonsMengeInMl,   ref Verbleibende_Flakons,  ref Aktivierungsdatum);

           if (IsFound)
               return new clsFlakons(
FlakonID, FlakonsMengeInMl, Verbleibende_Flakons, Aktivierungsdatum, Form, Verschlussart, Farbe);
            else
                return null;
            }


       public static DataTable GetAllFlakons()
       {

        return clsFlakonsDatenzugriff.GetAllFlakons();

       }



        public bool Save()
        {
            switch (Mode)
            {
                case enMode.AddNew:
                    if(_AddNewFlakons())
                    {
                        Mode = enMode.Update;
                         return true;
                    }
                    else
                    {
                        return false;
                    }
                case enMode.Update:
                    return _UpdateFlakons();

            }
        
            return false;
        }



       public bool DeleteFlakons()
       {

        return clsFlakonsDatenzugriff.DeleteFlakons(this.FlakonID);

       }


        public enum FlakonsColumn
         {
            FlakonID,
            Form,
            Verschlussart,
            Farbe,
            FlakonsMengeInMl,
            Verbleibende_Flakons,
            Aktivierungsdatum
        }


        public enum SearchMode
        {
            Anywhere,
            StartsWith,
            EndsWith,
            ExactMatch
        }
    

        public static DataTable SearchData(FlakonsColumn ChosenColumn, string SearchValue, SearchMode Mode = SearchMode.Anywhere)
        {
            if (string.IsNullOrWhiteSpace(SearchValue) || !SqlHelper.IsSafeInput(SearchValue))
                return new DataTable();

            string modeValue = Mode.ToString(); // Get the mode as string for passing to the stored procedure

            return clsFlakonsDatenzugriff.SearchData(ChosenColumn.ToString(), SearchValue, modeValue);
        }        



    }
}

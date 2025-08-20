
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
        public int FlakonsMengeInMl { get; set; }
        public int Flakons_pro_Karton { get; set; }
        public int Kartons_Lager { get; set; }
        public int? Verbleibende_Flakons { get; set; } = null;
        public DateTime? Erstellungsdatum { get; set; } = null;


        public clsFlakons()
        {
            this.FlakonID = null;
            this.Form = null;
            this.Verschlussart = null;
            this.Farbe = null;
            this.FlakonsMengeInMl = 0;
            this.Flakons_pro_Karton = 0;
            this.Kartons_Lager = 0;
            this.Verbleibende_Flakons = 0;
            this.Erstellungsdatum = DateTime.Now;


            Mode = enMode.AddNew;
        }


        private clsFlakons(
int? FlakonID, int FlakonsMengeInMl, int Flakons_pro_Karton, int Kartons_Lager, int? Verbleibende_Flakons, DateTime? Erstellungsdatum, string Form = null, string Verschlussart = null, string Farbe = null)        {
            this.FlakonID = FlakonID;
            this.Form = Form;
            this.Verschlussart = Verschlussart;
            this.Farbe = Farbe;
            this.FlakonsMengeInMl = FlakonsMengeInMl;
            this.Flakons_pro_Karton = Flakons_pro_Karton;
            this.Kartons_Lager = Kartons_Lager;
            this.Verbleibende_Flakons = Verbleibende_Flakons;
            this.Erstellungsdatum = Erstellungsdatum;
            Mode = enMode.Update;
        }


       private bool _AddNewFlakons()
       {
        this.FlakonID = clsFlakonsData.AddNewFlakons(
this.FlakonsMengeInMl, this.Flakons_pro_Karton, this.Kartons_Lager, this.Verbleibende_Flakons, this.Erstellungsdatum, this.Form, this.Verschlussart, this.Farbe);
        return (this.FlakonID != null);
       }


       public static bool AddNewFlakons(
ref int? FlakonID, int FlakonsMengeInMl, int Flakons_pro_Karton, int Kartons_Lager, int? Verbleibende_Flakons, DateTime? Erstellungsdatum, string Form = null, string Verschlussart = null, string Farbe = null)        {
        FlakonID = clsFlakonsData.AddNewFlakons(
FlakonsMengeInMl, Flakons_pro_Karton, Kartons_Lager, Verbleibende_Flakons, Erstellungsdatum, Form, Verschlussart, Farbe);

        return (FlakonID != null);

       }


       private bool _UpdateFlakons()
       {
        return clsFlakonsData.UpdateFlakonsByID(
FlakonID, FlakonsMengeInMl, Flakons_pro_Karton, Kartons_Lager, Verbleibende_Flakons, Erstellungsdatum, Form, Verschlussart, Farbe);
       }


       public static bool UpdateFlakonsByID(
int? FlakonID, int FlakonsMengeInMl, int Flakons_pro_Karton, int Kartons_Lager, int? Verbleibende_Flakons, DateTime? Erstellungsdatum, string Form = null, string Verschlussart = null, string Farbe = null)        {
        return clsFlakonsData.UpdateFlakonsByID(
FlakonID, FlakonsMengeInMl, Flakons_pro_Karton, Kartons_Lager, Verbleibende_Flakons, Erstellungsdatum, Form, Verschlussart, Farbe);

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
            int FlakonsMengeInMl = 0;
            int Flakons_pro_Karton = 0;
            int Kartons_Lager = 0;
            int? Verbleibende_Flakons = 0;
            DateTime? Erstellungsdatum = DateTime.Now;
            bool IsFound = clsFlakonsData.GetFlakonsInfoByID(FlakonID,
 ref Form,  ref Verschlussart,  ref Farbe,  ref FlakonsMengeInMl,  ref Flakons_pro_Karton,  ref Kartons_Lager,  ref Verbleibende_Flakons,  ref Erstellungsdatum);

           if (IsFound)
               return new clsFlakons(
FlakonID, FlakonsMengeInMl, Flakons_pro_Karton, Kartons_Lager, Verbleibende_Flakons, Erstellungsdatum, Form, Verschlussart, Farbe);
            else
                return null;
            }


       public static DataTable GetAllFlakons()
       {

        return clsFlakonsData.GetAllFlakons();

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



       public static bool DeleteFlakons(int FlakonID)
       {

        return clsFlakonsData.DeleteFlakons(FlakonID);

       }


        public enum FlakonsColumn
         {
            FlakonID,
            Form,
            Verschlussart,
            Farbe,
            FlakonsMengeInMl,
            Flakons_pro_Karton,
            Kartons_Lager,
            Verbleibende_Flakons,
            Erstellungsdatum
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

            return clsFlakonsData.SearchData(ChosenColumn.ToString(), SearchValue, modeValue);
        }        



    }
}

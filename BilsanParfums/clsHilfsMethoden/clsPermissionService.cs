using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace clsHilfsMethoden
{
    public class clsPermissionService
    {
        // Bitmasken-Konstanten für Berechtigungen
        public const int ParfuemVerwaltung = 2;      // Bit 1
        public const int KundenVerwaltung = 4;       // Bit 2
        public const int MitarbeiterVerwaltung = 8; // Bit 3
        public const int LagerVerwaltung = 16;      // Bit 4
        public const int UserVerwaltung = 32;  //Bit 5
        public const int Admin = -1;                // Alle Berechtigungen

        // Prüft, ob der Benutzer Admin ist
        public static bool IstAdmin(int permissions)
        {
            return permissions == Admin;
        }

        // Fügt eine Berechtigung hinzu
        public static int BerechtigungHinzufuegen(int permissions, int berechtigung)
        {
            if (IstAdmin(permissions))
                return Admin; // Admin hat automatisch alle Rechte

            return permissions | berechtigung;
        }

        // Entfernt eine Berechtigung
        public static int BerechtigungEntfernen(int permissions, int berechtigung)
        {
            if (IstAdmin(permissions))
                return Admin; // Admin-Berechtigungen bleiben unangetastet
            /*
             * permissions:       00010110 (dezimal: 22)
               ~berechtigung:     11111011
               Ergebnis:          00010010 (dezimal: 18)
             */
            return permissions & ~berechtigung; //Negation (~): Um die Berechtigung zu entfernen,
                                                // invertieren wir zuerst die Bits des Wertes berechtigung.
        }

        // Prüft, ob eine Berechtigung vorhanden ist
        public static bool HatBerechtigung(int permissions, int berechtigung)
        {
            if (IstAdmin(permissions))
                return true; // Admin hat immer alle Rechte

            return (permissions & berechtigung) == berechtigung;
        }
    }
}

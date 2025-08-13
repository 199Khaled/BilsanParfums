using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;

namespace clsHilfsMethoden
{
    public class clsBackupDatenbank
    {

        private static string ConnectionString = ConfigurationManager.ConnectionStrings["MyDbConnection"].ConnectionString;

        public static bool IsBackupSuccessfulByHostname()
        {
            return BackupDatabaseByHostname();
        }

        public static bool IsBackupSuccessfulByMacAdresse()
        {
            return BackupDatabaseByMacAdresse();
        }
        private static bool BackupDatabaseByHostname()
        {

            _BackupDatabaseByHostnameInOntherPath(); // Sichere die datei in anderem order.

            //string allowedHostName = "HAMSSA"; // Der Name des Computers, der das Backup durchführen darf
            //string currentHostName = Environment.MachineName;  // Der aktuelle Hostname des Computers

            //// Überprüfen, ob der Hostname übereinstimmt
            //if (currentHostName != allowedHostName)
            //{
            //    return false;
            //}

            string backupPath;

            try
            {
                string verzeichnis = @"C:\ParfuemDb";
                string dateiname = "ParfuemDb.bak";
                if (!Directory.Exists(verzeichnis))
                {
                    Directory.CreateDirectory(verzeichnis);
                }

                backupPath = Path.Combine(verzeichnis, dateiname);
            }
            catch (Exception ex)
            {
                // Werfe eine präzisere Fehlermeldung
                throw new Exception($"Fehler beim Erstellen des Backup-Pfades: {ex.Message}", ex);
            }
            // Die Abfrage für das Backup
            string query = $@"
                          BACKUP DATABASE ParfümDb 
                             TO DISK = '{backupPath}' 
                               WITH FORMAT, INIT;";

            // Ergebnisvariable
            bool isBackupSuccessful = false;

            try
            {
                using (SqlConnection connection = new SqlConnection(ConnectionString))
                {
                    connection.Open();

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        // Ausführung der Abfrage
                        command.ExecuteNonQuery();
                        isBackupSuccessful = true;
                    }
                }
            }
            catch (Exception ex)
            {
                isBackupSuccessful = false;
                // Exception wird geloggt oder behandelt
                Console.WriteLine($"Backup fehlgeschlagen: {ex.Message}");
                // Optional: Loggen in einer Datei oder Datenbank
            }

            return isBackupSuccessful;
        }

        private static bool _BackupDatabaseByHostnameInOntherPath()
        {

            //string allowedHostName = "HAMSSA"; // Der Name des Computers, der das Backup durchführen darf
            //string currentHostName = Environment.MachineName;  // Der aktuelle Hostname des Computers

            //// Überprüfen, ob der Hostname übereinstimmt
            //if (currentHostName != allowedHostName)
            //{
            //    return false;
            //}

            string backupPath;
            try
            {
                string dateiname = "ParfuemDb.bak";
                string verzeichnis = @"C:\Sql_Datei";
                if (!Directory.Exists(verzeichnis))
                {
                    Directory.CreateDirectory(verzeichnis);
                }

                // Pfad sicher kombinieren
                backupPath = Path.Combine(verzeichnis, dateiname);
            }
            catch (Exception ex)
            {
                // Werfe eine präzisere Fehlermeldung
                throw new Exception($"Fehler beim Erstellen des Backup-Pfades: {ex.Message}", ex);
            }

            // Die Abfrage für das Backup
            string query = $@"
                          BACKUP DATABASE ParfümDb 
                             TO DISK = '{backupPath}' 
                               WITH FORMAT, INIT;";

            // Ergebnisvariable
            bool isBackupSuccessful = false;

            try
            {
                using (SqlConnection connection = new SqlConnection(ConnectionString))
                {
                    connection.Open();

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        // Ausführung der Abfrage
                        command.ExecuteNonQuery();
                        isBackupSuccessful = true;
                    }
                }
            }
            catch (Exception ex)
            {
                isBackupSuccessful = false;
                // Exception wird geloggt oder behandelt
                Console.WriteLine($"Backup fehlgeschlagen: {ex.Message}");
                // Optional: Loggen in einer Datei oder Datenbank
            }

            return isBackupSuccessful;
        }
        public static bool BackupDatabaseByMacAdresse()
        {
            _BackupDatabaseByMacAdresseInOntherPath(); //Sichere die Datei in einem anderen Order.

            //string allowedMacAdresse = "E8-6F-38-BD-13-E9"; // Der MacAdresse des Computers, der das Backup durchführen darf
            //string currentMacAdresse = _GetMacAddress();  // Der aktuelle Hostname des Computers

            //// Überprüfen, ob der Hostname übereinstimmt
            //if (currentMacAdresse != allowedMacAdresse)
            //{
            //    return false;
            //}

            string backupPath;
            try
            {
                string verzeichnis = @"C:\ParfuemDb";
                string dateiname = "ParfuemDb.bak";
                if (!Directory.Exists(verzeichnis))
                {
                    Directory.CreateDirectory(verzeichnis);
                }

                backupPath = Path.Combine(verzeichnis, dateiname);
            }
            catch (Exception ex)
            {
                // Werfe eine präzisere Fehlermeldung
                throw new Exception($"Fehler beim Erstellen des Backup-Pfades: {ex.Message}", ex);
            }

            // Die Abfrage für das Backup
            string query = $@"
                          BACKUP DATABASE ParfümDb 
                             TO DISK = '{backupPath}' 
                               WITH FORMAT, INIT;";

            // Ergebnisvariable
            bool isBackupSuccessful = false;

            try
            {
                using (SqlConnection connection = new SqlConnection(ConnectionString))
                {
                    connection.Open();

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        // Ausführung der Abfrage
                        command.ExecuteNonQuery();
                        isBackupSuccessful = true;
                    }
                }
            }
            catch (Exception ex)
            {
                isBackupSuccessful = false;
                // Exception wird geloggt oder behandelt
                Console.WriteLine($"Backup fehlgeschlagen: {ex.Message}");
                // Optional: Loggen in einer Datei oder Datenbank
            }

            return isBackupSuccessful;
        }

        private static bool _BackupDatabaseByMacAdresseInOntherPath()
        {

            //string allowedMacAdresse = "E8-6F-38-BD-13-E9"; // Der MacAdresse des Computers, der das Backup durchführen darf
            //string currentMacAdresse = _GetMacAddress();  // Der aktuelle Hostname des Computers

            //// Überprüfen, ob der Hostname übereinstimmt
            //if (currentMacAdresse != allowedMacAdresse)
            //{
            //    return false;
            //}

            string backupPath;
            try
            {
                string dateiname = "ParfuemDb.bak";
                string verzeichnis = @"C:\Sql_Datei";
                if (!Directory.Exists(verzeichnis))
                {
                    Directory.CreateDirectory(verzeichnis);
                }

                backupPath = Path.Combine(verzeichnis, dateiname);
            }
            catch (Exception ex)
            {
                // Werfe eine präzisere Fehlermeldung
                throw new Exception($"Fehler beim Erstellen des Backup-Pfades: {ex.Message}", ex);
            }

            // Die Abfrage für das Backup
            string query = $@"
                          BACKUP DATABASE ParfümDb 
                             TO DISK = '{backupPath}' 
                               WITH FORMAT, INIT;";

            // Ergebnisvariable
            bool isBackupSuccessful = false;

            try
            {
                using (SqlConnection connection = new SqlConnection(ConnectionString))
                {
                    connection.Open();

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        // Ausführung der Abfrage
                        command.ExecuteNonQuery();
                        isBackupSuccessful = true;
                    }
                }
            }
            catch (Exception ex)
            {
                isBackupSuccessful = false;
                // Exception wird geloggt oder behandelt
                Console.WriteLine($"Backup fehlgeschlagen: {ex.Message}");
                // Optional: Loggen in einer Datei oder Datenbank
            }

            return isBackupSuccessful;
        }
        // Methode zur Ermittlung der MAC-Adresse des Computers
        private static string _GetMacAddress()
        {
            foreach (NetworkInterface nic in NetworkInterface.GetAllNetworkInterfaces())
            {
                if (nic.OperationalStatus == OperationalStatus.Up)
                {
                    return string.Join("-", nic.GetPhysicalAddress().GetAddressBytes().Select(b => b.ToString("X2")));
                }
            }
            return string.Empty; // Falls keine MAC-Adresse gefunden wurde
        }
    }
}

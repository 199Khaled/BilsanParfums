using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using Windows.UI.Composition.Interactions;
using System.Collections;
using Windows.ApplicationModel.VoiceCommands;


namespace clsHilfsMethoden
{
    public class clsProduktSchluessel
    {
        private static string ConnectionString = ConfigurationManager.ConnectionStrings["MyDbConnection"].ConnectionString;

        private  static bool AddnewSchluessl(string Key)
        {
            int ProduktID = -1;

            string verschüsseltesKey = clsVerschlüsselungHelfer.Encrypt(Key);

            // SQL-Abfrage zum Einfügen des verschlüsselten Schlüssels
            string query = @"INSERT INTO Produktschluessel (Schluessel,verwendet)
                                      VALUES (@Schluessel, @verwendet)
                                Select Scope_Identity() ";

            try
            {
                using (SqlConnection connection = new SqlConnection(ConnectionString))
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Schluessel", verschüsseltesKey);
                    command.Parameters.AddWithValue("@verwendet",false);

                    connection.Open();

                    object result = command.ExecuteScalar();
                    if (result != null && int.TryParse(result.ToString(), out int ID))
                        ProduktID = ID;

                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return (ProduktID != -1);
        }

        public static bool AktiviereProduktSchluesselDatum(string schluessel)
        {
            int rowAffected = 0;
            string verschlüseltesKey = clsVerschlüsselungHelfer.Encrypt(schluessel);
            int totalMonate = _GetTotalMonateFuerProduktSchluessel(verschlüseltesKey); // wir geben den veschlüsselten Schlüssel weiter.

            string query = @"Update ProduktSchluessel  Set
                                                            erstellungsdatum = @erstellungsdatum,
                                                            ablaufdatum  = @ablaufdatum,
                                                           verwendet = @verwendet
                                                    Where Schluessel = @schluessel";
            try
            {
                using (SqlConnection connection = new SqlConnection(ConnectionString))
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Schluessel", verschlüseltesKey);
                    command.Parameters.AddWithValue("@erstellungsdatum", DateTime.Now);
                    command.Parameters.AddWithValue("@ablaufdatum", DateTime.Now.AddMonths(totalMonate));
                    command.Parameters.AddWithValue("@verwendet", true);

                    connection.Open();

                    rowAffected = command.ExecuteNonQuery();

                }
            }
            catch (Exception ex)
            {
                return false;
                throw new Exception(ex.Message);
            }
            return (rowAffected > 0);
        }

        private static int _GetTotalMonateFuerProduktSchluessel(string schluessel)
        {
            int totalMonate = 0;

            string query = @"Select totalMonate From ProduktSchluessel
                                                    Where Schluessel = @schluessel";
            try
            {
                using (SqlConnection connection = new SqlConnection(ConnectionString))
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Schluessel", schluessel);
                    connection.Open();

                    object result = command.ExecuteScalar();
                    if (result != null)
                        totalMonate = (int)result;

                }
            }
            catch (Exception ex)
            {
        
                throw new Exception(ex.Message);
            }
            return totalMonate;
        }
        private static DataTable GetDatumUndStatusByProduktSchluessel(string ProduktSchluessel)
        {
            DataTable dt = new DataTable();

            string VerschlüsselProduktSchluessel = clsVerschlüsselungHelfer.Encrypt(ProduktSchluessel);
            string abfrage = @"Select * From ProduktSchluessel Where Schluessel = @Schluessel";
            try
            {
                using (SqlConnection connection = new SqlConnection(ConnectionString))
                {
                    using (SqlCommand command = new SqlCommand(abfrage, connection))
                    {
                        command.Parameters.AddWithValue("@Schluessel", VerschlüsselProduktSchluessel);

                        connection.Open();
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.HasRows)
                                dt.Load(reader);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return dt;
        }

        public static bool AddnewProduktKay(string key)
        {
            return AddnewSchluessl(key);
        }

        public static DataTable GetDatumAndAblaufdatumBySchluessel(string key)
        {
            return GetDatumUndStatusByProduktSchluessel(key);
        }
    }
}


using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data_Layer
{
    public class clsNeueParfümDatenzugriff
    {
        private static string ConnectionString = ConfigurationManager.ConnectionStrings["MyDbConnection"].ConnectionString;

        public static DataTable GetAllParfüms()
        {
            DataTable dt = new DataTable();
            string abfrage = @"Select * from NeueParfümsdaten";

            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                try
                {
                    using (SqlCommand command = new SqlCommand(abfrage, connection))
                    {
                        connection.Open();
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.HasRows)
                                dt.Load(reader);
                        }
                    }
                }
                catch (Exception ex)
                {
                    // Fehlerbehandlung
                    throw new ApplicationException("Fehler beim Abrufen aller Parfüms.", ex);
                }
            }
            return dt;
        }

        public static DataTable GetAllParfuemByName(string filteredName)
        {
            DataTable dt = new DataTable();
            string abfrage = @"Select * from NeueParfümsdaten Where Name Like @ParfuemFullTextCatalog";

            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                try
                {
                    using (SqlCommand command = new SqlCommand(abfrage, connection))
                    {
                        command.Parameters.AddWithValue("@ParfuemFullTextCatalog", "%" + filteredName + "%");
                        connection.Open();
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.HasRows)
                                dt.Load(reader);
                        }
                    }
                }
                catch (Exception ex)
                {
                    // Fehlerbehandlung
                    throw new ApplicationException("Fehler beim Abrufen von Parfüms nach Name.", ex);
                }
            }
            return dt;
        }

        public static DataTable GetAllParfuemByMarke(string filteredMarke)
        {
            DataTable dt = new DataTable();
            string abfrage = @"Select * from NeueParfümsdaten Where Marke Like @ParfuemFullTextCatalog";

            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                try
                {
                    using (SqlCommand command = new SqlCommand(abfrage, connection))
                    {
                        command.Parameters.AddWithValue("@ParfuemFullTextCatalog", "%" + filteredMarke + "%");
                        connection.Open();
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.HasRows)
                                dt.Load(reader);
                        }
                    }
                }
                catch (Exception ex)
                {
                    // Fehlerbehandlung
                    throw new ApplicationException("Fehler beim Abrufen von Parfüms nach Marke.", ex);
                }
            }
            return dt;
        }
        public static DataTable GetAllHerrenParfüms()
        {
            DataTable dt = new DataTable(); ;

            string abfrage = @"
                              Select * from NeueParfümsdaten where Kategorie Like 'Herrenduft%' 
                                ";

            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                try
                {
                    using (SqlCommand command = new SqlCommand(abfrage, connection))
                    {
                        connection.Open();
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.HasRows)
                                dt.Load(reader);
                        }
                    }
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }

            return dt;
        }

        public static DataTable GetAllDamenParfüms()
        {
            DataTable dt = new DataTable(); ;

            string abfrage = @"Select *
                             from NeueParfümsdaten  where Kategorie Like 'Damenduft%'
                                 ";

            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                try
                {
                    using (SqlCommand command = new SqlCommand(abfrage, connection))
                    {
                        connection.Open();
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.HasRows)
                                dt.Load(reader);
                        }
                    }
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }

            return dt;
        }

        public static DataTable GetAllUnisexParfüms()
        {
            DataTable dt = new DataTable(); ;

            string abfrage = @"Select *
                             from NeueParfümsdaten  where Kategorie Like 'Unisexduft%'
                                 ";

            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                try
                {
                    using (SqlCommand command = new SqlCommand(abfrage, connection))
                    {
                        connection.Open();
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.HasRows)
                                dt.Load(reader);
                        }
                    }
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }

            return dt;
        }
        public static DataTable GetAllKinderParfüms()
        {
            DataTable dt = new DataTable(); ;

            string abfrage = @"Select *
                             from NeueParfümsdaten  where Kategorie Like 'Kinderduft%'
                                 ";

            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                try
                {
                    using (SqlCommand command = new SqlCommand(abfrage, connection))
                    {
                        connection.Open();
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.HasRows)
                                dt.Load(reader);
                        }
                    }
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }

            return dt;
        }

        public static DataTable GetAllOrientalischeParfüms()
        {
            DataTable dt = new DataTable(); ;

            string abfrage = @"Select *
                             from NeueParfümsdaten  where Kategorie Like 'Orientalisch%'
                                 ";

            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                try
                {
                    using (SqlCommand command = new SqlCommand(abfrage, connection))
                    {
                        connection.Open();
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.HasRows)
                                dt.Load(reader);
                        }
                    }
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }

            return dt;
        }


        public static bool AddNewPerfum(int? alteNummer, int parfümNummer, string marke, string name, string kategorie,
              string duftrichtung, string basisnote, bool istVorhanden, bool inBestellung)
        {
            int rowAffected = 0;
            string abfrage = @"Insert into NeueParfümsdaten (AlteNummer, parfümNummer, Marke, Name, Kategorie, Duftrichtung, Basisnote, IstVorhanden, InBestellung)
                           Values(@alteNummer, @parfümNummer, @marke, @name, @kategorie, @duftrichtung, @basisnote, @istVorhanden, @inBestellung)";

            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                try
                {
                    using (SqlCommand command = new SqlCommand(abfrage, connection))
                    {
                        if (alteNummer == null)
                        {
                            command.Parameters.AddWithValue("@alteNummer", DBNull.Value);
                        }
                        else
                        {
                            command.Parameters.AddWithValue("@alteNummer", alteNummer);
                        }
                        command.Parameters.AddWithValue("@parfümNummer", parfümNummer);
                        command.Parameters.AddWithValue("@marke", marke);
                        command.Parameters.AddWithValue("@name", name);
                        command.Parameters.AddWithValue("@kategorie", kategorie);

                        if (string.IsNullOrEmpty(duftrichtung))
                            command.Parameters.AddWithValue("@duftrichtung", DBNull.Value);
                        else
                            command.Parameters.AddWithValue("@duftrichtung", duftrichtung);

                        if (string.IsNullOrEmpty(basisnote))
                            command.Parameters.AddWithValue("@basisnote", DBNull.Value);
                        else
                            command.Parameters.AddWithValue("@basisnote", basisnote);

                        command.Parameters.AddWithValue("@istVorhanden", istVorhanden);
                        command.Parameters.AddWithValue("@inBestellung", inBestellung);
                        connection.Open();
                        rowAffected = command.ExecuteNonQuery();
                    }
                }
                catch (Exception ex)
                {
                    // Fehlerbehandlung
                    throw new ApplicationException("Fehler beim Hinzufügen eines neuen Parfüms.", ex);
                }
            }
            return (rowAffected > 0);
        }

        public static bool UpdatePerfum(int? neuAlteNummer, int neuParfümNummer, int parfümNummer,
            string marke, string name, string kategorie,
              string duftrichtung, string basisnote, bool istVorhanden, bool inBestellung)
        {
            int rowAffected = 0;
            string abfrage = @"Update NeueParfümsdaten
                           Set AlteNummer = @neuAlteNummer,
                               parfümNummer = @neuparfümNummer,
                               Marke = @marke,
                               Name = @name,
                               Kategorie = @kategorie,
                               Duftrichtung = @duftrichtung,
                               Basisnote = @basisnote,
                               IstVorhanden = @istVorhanden,
                               InBestellung = @inBestellung
                           Where parfümNummer = @parfümNummer";

            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                try
                {
                    using (SqlCommand command = new SqlCommand(abfrage, connection))
                    {
                        command.Parameters.AddWithValue("@parfümNummer", parfümNummer);
                        if (neuAlteNummer == null)
                        {
                            command.Parameters.AddWithValue("@neuAlteNummer", DBNull.Value);
                        }
                        else
                        {
                            command.Parameters.AddWithValue("@neuAlteNummer", neuAlteNummer);
                        }
                        command.Parameters.AddWithValue("@neuParfümNummer", neuParfümNummer);
                        command.Parameters.AddWithValue("@marke", marke);
                        command.Parameters.AddWithValue("@name", name);
                        command.Parameters.AddWithValue("@kategorie", kategorie);

                        if (string.IsNullOrEmpty(duftrichtung))
                            command.Parameters.AddWithValue("@duftrichtung", DBNull.Value);
                        else
                            command.Parameters.AddWithValue("@duftrichtung", duftrichtung);

                        if (string.IsNullOrEmpty(basisnote))
                            command.Parameters.AddWithValue("@basisnote", DBNull.Value);
                        else
                            command.Parameters.AddWithValue("@basisnote", basisnote);

                        command.Parameters.AddWithValue("@istVorhanden", istVorhanden);
                        command.Parameters.AddWithValue("@inBestellung", inBestellung);
                        connection.Open();
                        rowAffected = command.ExecuteNonQuery();
                    }
                }
                catch (Exception ex)
                {
                    // Fehlerbehandlung
                    throw new ApplicationException("Fehler beim Aktualisieren des Parfüms.", ex);
                }
            }
            return (rowAffected > 0);
        }

        public static bool Delete(int parfümNummer)
        {
            int rowAffected = 0;
            string abfrage = @"Delete From NeueParfümsdaten Where parfümNummer = @parfümNummer";

            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                try
                {
                    using (SqlCommand command = new SqlCommand(abfrage, connection))
                    {
                        command.Parameters.AddWithValue("@parfümNummer", parfümNummer);
                        connection.Open();
                        rowAffected = command.ExecuteNonQuery();
                    }
                }
                catch (Exception ex)
                {
                    // Fehlerbehandlung
                    throw new ApplicationException("Fehler beim Löschen des Parfüms.", ex);
                }
            }
            return (rowAffected > 0);
        }

        public static bool Find(ref int? alteNummer, int parfümNummer, ref string marke, ref string name,
              ref string kategorie, ref string duftrichtung, ref string basisnote,
              ref bool istVorhanden, ref bool inBestellung)
        {
            bool isFound = false;
            string abfrage = @"Select * From NeueParfümsdaten Where parfümNummer = @parfümNummer";

            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                try
                {
                    using (SqlCommand command = new SqlCommand(abfrage, connection))
                    {
                        command.Parameters.AddWithValue("@parfümNummer", parfümNummer);
                        connection.Open();
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                isFound = true;

                                alteNummer = reader["AlteNummer"] != DBNull.Value ? (int)reader["AlteNummer"] : (int?)null;
                                marke = (string)reader["Marke"];
                                name = (string)reader["Name"];
                                kategorie = (string)reader["Kategorie"];
                                duftrichtung = reader["Duftrichtung"] != DBNull.Value ? (string)reader["Duftrichtung"] : null;
                                basisnote = reader["Basisnote"] != DBNull.Value ? (string)reader["Basisnote"] : null;
                                istVorhanden = (bool)reader["IstVorhanden"];
                                inBestellung = (bool)reader["InBestellung"];
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    // Fehlerbehandlung
                    throw new ApplicationException("Fehler beim Suchen eines Parfüms.", ex);
                }
            }
            return isFound;
        }

        public static bool _IstParfümNummerVergeben(int parfümNummer)
        {
            string abfrage = @"Select parfümNummer From NeueParfümsdaten Where parfümNummer = @parfümNummer";
            bool isVergeben = false;

            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                try
                {
                    using (SqlCommand command = new SqlCommand(abfrage, connection))
                    {
                        command.Parameters.AddWithValue("@parfümNummer", parfümNummer);
                        connection.Open();
                        object result = command.ExecuteScalar();
                        isVergeben = (result != null && result != DBNull.Value);
                    }
                }
                catch (Exception ex)
                {
                    // Fehlerbehandlung
                    throw new ApplicationException("Fehler bei der Prüfung der ParfümNummer.", ex);
                }
            }
            return isVergeben;
        }
    }
}

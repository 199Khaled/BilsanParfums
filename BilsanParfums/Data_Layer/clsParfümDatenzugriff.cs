using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;

namespace Data_Layer
{
   
    public class clsParfümDatenzugriff
    {
        private static string ConnectionString = ConfigurationManager.ConnectionStrings["MyDbConnection"].ConnectionString;


        public static DataTable GetAllParfüms()
        {
            DataTable dt = new DataTable(); ;

            string abfrage = @"Select *
                                from Parfüms";

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
        public static DataTable GetAllParfuemByName(string filterdName)
        {
            DataTable dt = new DataTable(); ;

            string abfrage = @"Select * from Parfüms Where Name = @filterdName";
            //Where Name Like @ParfuemFullTextCatalog OR Freetext(Name, @filterdName) ";

            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                try
                {
                    using (SqlCommand command = new SqlCommand(abfrage, connection))
                    {
                        command.Parameters.AddWithValue("@filterdName", filterdName);
                        command.Parameters.AddWithValue("@ParfuemFullTextCatalog", "%" + filterdName + "%");
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
        public static DataTable GetAllParfuemByMarke(string filterdMarke)
        {
            DataTable dt = new DataTable(); ;

            string abfrage = @"Select * from Parfüms
                              Where Marke Like @ParfuemFullTextCatalog OR Freetext(Marke, @filterdMarke)";

            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                try
                {
                    using (SqlCommand command = new SqlCommand(abfrage, connection))
                    {
                        command.Parameters.AddWithValue("@filterdMarke", filterdMarke);
                        command.Parameters.AddWithValue("@ParfuemFullTextCatalog", "%" + filterdMarke + "%");

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
        public static DataTable GetAllHerrenParfüms()
        {
            DataTable dt = new DataTable(); ;

            string abfrage = @"
                              Select * from Parfüms where Kategorie Like 'Herrenduft%' 
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
                             from Parfüms  where Kategorie Like 'Damenduft%'
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
                             from Parfüms  where Kategorie Like 'Unisexduft%'
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
                             from Parfüms  where Kategorie Like 'Kinderduft%'
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
                             from Parfüms  where Kategorie Like 'Orientalisch%'
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

        public static bool AddNewPerfum(int parfümNummer, string marke, string name, string kategorie,
              string duftrichtung, string Basisnote, bool IstVorhanden, bool InBestellung)
        {
            int rowAffected = 0;

            string abfrage = @"Insert into Parfüms (parfümNummer,Marke, Name, Kategorie, Duftrichtung, Basisnote,IstVorhanden,InBestellung)
                                          Values(@parfümNummer, @marke, @name, @kategorie, @duftrichtung, @Basisnote, @IstVorhanden, @InBestellung)";


            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                try
                {
                    using (SqlCommand command = new SqlCommand(abfrage, connection))
                    {
                        command.Parameters.AddWithValue("@parfümNummer", parfümNummer);
                        command.Parameters.AddWithValue("@marke", marke);
                        command.Parameters.AddWithValue("@name", name);
                        command.Parameters.AddWithValue("@kategorie", kategorie);

                        if (string.IsNullOrEmpty(duftrichtung))
                            command.Parameters.AddWithValue("@duftrichtung", DBNull.Value);
                        else
                            command.Parameters.AddWithValue("@duftrichtung", duftrichtung);

                        if (string.IsNullOrEmpty(Basisnote))
                            command.Parameters.AddWithValue("@Basisnote", DBNull.Value);
                        else
                            command.Parameters.AddWithValue("@Basisnote", Basisnote);


                        command.Parameters.AddWithValue("@IstVorhanden", IstVorhanden);
                        command.Parameters.AddWithValue("@InBestellung", InBestellung);
                        connection.Open();

                        rowAffected = command.ExecuteNonQuery();
                    }
                }
                catch (Exception ex)
                {
                    throw;
                }
            }
            return (rowAffected > 0);
        }

        public static bool UpdatePerfum(int neuParfümNummer, int parfümNummer, string marke, string name, string kategorie,
              string duftrichtung, string Basisnote, bool IstVorhanden, bool InBestellung)
        {
            int rowAffected = 0;

            string abfrage = @"Update Parfüms
                                        Set  parfümNummer = @neuParfümNummer,
                                             Marke        = @marke,
                                             Name         = @name,
                                             Kategorie    = @kategorie,
                                             Duftrichtung = @duftrichtung,
                                             Basisnote = @Basisnote,
                                             
                                             IstVorhanden = @IstVorhanden,
                                             InBestellung = @InBestellung
                                       Where parfümNummer = @parfümNummer";


            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                try
                {
                    using (SqlCommand command = new SqlCommand(abfrage, connection))
                    {

                        command.Parameters.AddWithValue("@parfümNummer", parfümNummer);

                        //die neue ParfümNummer.
                        command.Parameters.AddWithValue("@neuParfümNummer", neuParfümNummer);
                        command.Parameters.AddWithValue("@marke", marke);
                        command.Parameters.AddWithValue("@name", name);
                        command.Parameters.AddWithValue("@kategorie", kategorie);

                        if (string.IsNullOrEmpty(duftrichtung))
                            command.Parameters.AddWithValue("@duftrichtung", DBNull.Value);
                        else
                            command.Parameters.AddWithValue("@duftrichtung", duftrichtung);

                        if (string.IsNullOrEmpty(Basisnote))
                            command.Parameters.AddWithValue("@Basisnote", DBNull.Value);
                        else
                            command.Parameters.AddWithValue("@Basisnote", Basisnote);



                        command.Parameters.AddWithValue("@IstVorhanden", IstVorhanden);
                        command.Parameters.AddWithValue("@InBestellung", InBestellung);
                        connection.Open();

                        rowAffected = command.ExecuteNonQuery();
                    }
                }
                catch (Exception ex)
                {
                    throw;
                }
            }
            return (rowAffected > 0);
        }
        public static bool Delete(int parfümNummer)
        {
            int rowAffected = 0;
            string abfrage = @"Delete From Parfüms Where parfümNummer = @parfümNummer";
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
                    throw;
                }
            }
            return (rowAffected > 0);
        }

        public static bool Find(int parfümNummer, ref string marke, ref string name,
              ref string kategorie, ref string duftrichtung, ref string Basisnote,
              ref bool IstVorhanden, ref bool InBestellung)
        {
            bool isfound = false;
            string abfrage = @"Select * From Parfüms Where parfümNummer = @parfümNummer";

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
                                isfound = true;

                                marke = (string)reader["Marke"];
                                name = (string)reader["Name"];
                                kategorie = (string)reader["Kategorie"];
                                duftrichtung = (string)reader["Duftrichtung"].ToString();
                                Basisnote = (string)reader["Basisnote"].ToString();
                                IstVorhanden = (bool)reader["IstVorhanden"];
                                InBestellung = reader["InBestellung"] != DBNull.Value ? (bool)reader["InBestellung"] : false;
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    throw;
                }
            }
            return isfound;
        }

        public static bool _IstParfümNummerVergeben(int parfümNummer)
        {
            int rowAffected = 0;
            string abfrage = @"Select Find = 1 From Parfüms  Where parfümNummer = @parfümNummer";

            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                try
                {
                    using (SqlCommand command = new SqlCommand(abfrage, connection))
                    {
                        command.Parameters.AddWithValue("@parfümNummer", parfümNummer);

                        connection.Open();

                        object result = command.ExecuteScalar();
                        if (result != null)
                        {
                            rowAffected = (int)result;
                        }
                    }
                }
                catch (Exception ex)
                {
                    throw;
                }
            }
            return (rowAffected > 0);
        }
    }
}


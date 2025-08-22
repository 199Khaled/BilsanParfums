
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using BilsanDb_DataAccess;
using Newtonsoft.Json;

namespace BilsanDb_DataLayer
{
    public class clsFlakonsDatenzugriff
    {
        //#nullable enable

        public static bool GetFlakonsInfoByID(int? FlakonID , ref string Form, ref string Verschlussart, ref string Farbe, ref string FlakonsMengeInMl, ref int Flakons_pro_Karton, ref int Kartons_Lager, ref int? Verbleibende_Flakons, ref DateTime? Erstellungsdatum)
{
    bool isFound = false;

    try
    {
        using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
        {
            string query = "SP_Get_Flakons_ByID";

            using (SqlCommand command = new SqlCommand(query, connection))
            {
                command.CommandType = CommandType.StoredProcedure;

                // Ensure correct parameter assignment
                command.Parameters.AddWithValue("@FlakonID", FlakonID ?? (object)DBNull.Value);

                connection.Open();
                using (SqlDataReader reader = command.ExecuteReader())
                { 
                    if (reader.Read())
                    {
                        // The record was found
                        isFound = true;

                                Form = reader["Form"] != DBNull.Value ? reader["Form"].ToString() : null;
                                Verschlussart = reader["Verschlussart"] != DBNull.Value ? reader["Verschlussart"].ToString() : null;
                                Farbe = reader["Farbe"] != DBNull.Value ? reader["Farbe"].ToString() : null;
                                FlakonsMengeInMl = (string)reader["FlakonsMengeInMl"];
                                Flakons_pro_Karton = (int)reader["Flakons_pro_Karton"];
                                Kartons_Lager = (int)reader["Kartons_Lager"];
                                Verbleibende_Flakons = reader["Verbleibende_Flakons"] != DBNull.Value ? (int?)reader["Verbleibende_Flakons"] : null;
                                Erstellungsdatum = reader["Erstellungsdatum"] != DBNull.Value ? (DateTime?)reader["Erstellungsdatum"] : null;

                    }
                }
            }
        }
    }
    catch (Exception ex)
    {
        // Handle all exceptions in a general way
        ErrorHandler.HandleException(ex, nameof(GetFlakonsInfoByID), $"Parameter: FlakonID = " + FlakonID);
    }

    return isFound;
}

        //        public static DataTable GetAllFlakons()
        //{
        //    DataTable dt = new DataTable();

        //    try
        //    {
        //        using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
        //        {
        //            string query = "SP_Get_All_Flakons";

        //            using (SqlCommand command = new SqlCommand(query, connection))
        //            {
        //                command.CommandType = CommandType.StoredProcedure; 

        //                connection.Open();

        //                using (SqlDataReader reader = command.ExecuteReader())
        //                {
        //                    if (reader.HasRows)
        //                    {
        //                        dt.Load(reader);
        //                    }
        //                }
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        // Handle all exceptions in a general way
        //        ErrorHandler.HandleException(ex, nameof(GetAllFlakons), "No parameters for this method.");
        //    }

        //    return dt;
        //}

        public static DataTable GetAllFlakons()
        {
            DataTable dt = new DataTable();

            try
            {
                using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
                {
                    string query = @"
                SELECT 
                    FlakonID AS ID,
                    Form AS Form,
                    Verschlussart As Verschlussart,
                    Farbe AS Farbe,
                    FlakonsMengeInMl AS inML,
                    Flakons_pro_Karton AS proKarton,
                    Kartons_Lager AS Kartons,
                    Verbleibende_Flakons AS Rest,
                    Erstellungsdatum AS Datum
                FROM dbo.Flakons";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    { 
                        connection.Open();

                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.HasRows)
                            {
                                dt.Load(reader);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                // Handle all exceptions in a general way
                ErrorHandler.HandleException(ex, nameof(GetAllFlakons), "No parameters for this method.");
            }

            return dt;
        }
        public static int? AddNewFlakons(string FlakonsMengeInMl, int Flakons_pro_Karton, int Kartons_Lager, int? Verbleibende_Flakons, DateTime? Erstellungsdatum, string Form = null, string Verschlussart = null, string Farbe = null)
    {
        int? FlakonID = null;

        try
        {
            using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
            {
                string query = @"SP_Add_Flakons";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.AddWithValue("@Form", Form ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@Verschlussart", Verschlussart ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@Farbe", Farbe ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@FlakonsMengeInMl", FlakonsMengeInMl);
                    command.Parameters.AddWithValue("@Flakons_pro_Karton", Flakons_pro_Karton);
                    command.Parameters.AddWithValue("@Kartons_Lager", Kartons_Lager);
                    command.Parameters.AddWithValue("@Verbleibende_Flakons", Verbleibende_Flakons ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@Erstellungsdatum", Erstellungsdatum ?? (object)DBNull.Value);


                    SqlParameter outputIdParam = new SqlParameter("@NewID", SqlDbType.Int)
                    {
                        Direction = ParameterDirection.Output
                    };
                    command.Parameters.Add(outputIdParam);

                    connection.Open();
                    command.ExecuteNonQuery();

                    // Bring added value
                    if (outputIdParam.Value != DBNull.Value)
                    {
                        FlakonID = (int)outputIdParam.Value;
                    }

                }
            }
        }
        catch (Exception ex)
        {
            // Handle all exceptions in a general way
            ErrorHandler.HandleException(ex, nameof(AddNewFlakons), $"Parameters: int FlakonsMengeInMl, int Flakons_pro_Karton, int Kartons_Lager, int? Verbleibende_Flakons, DateTime? Erstellungsdatum, string Form = null, string Verschlussart = null, string Farbe = null");
        }

        return FlakonID;
    }

        public static bool UpdateFlakonsByID(int? FlakonID,string FlakonsMengeInMl, int Flakons_pro_Karton, int Kartons_Lager, int? Verbleibende_Flakons, DateTime? Erstellungsdatum, string Form = null, string Verschlussart = null, string Farbe = null)
{
    int rowsAffected = 0;

    try
    {
        using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
        {
            string query = $@"SP_Update_Flakons_ByID"; 

            using (SqlCommand command = new SqlCommand(query, connection))
            {
                command.CommandType = CommandType.StoredProcedure;

                // Create the parameters for the stored procedure
                    command.Parameters.AddWithValue("@FlakonID", FlakonID);
                    command.Parameters.AddWithValue("@Form", Form ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@Verschlussart", Verschlussart ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@Farbe", Farbe ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@FlakonsMengeInMl", FlakonsMengeInMl);
                    command.Parameters.AddWithValue("@Flakons_pro_Karton", Flakons_pro_Karton);
                    command.Parameters.AddWithValue("@Kartons_Lager", Kartons_Lager);
                    command.Parameters.AddWithValue("@Verbleibende_Flakons", Verbleibende_Flakons ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@Erstellungsdatum", Erstellungsdatum ?? (object)DBNull.Value);


                // Open the connection and execute the update
                connection.Open();
                rowsAffected = command.ExecuteNonQuery();
            }
        }
    }
    catch (Exception ex)
    {
        // Handle exceptions
        ErrorHandler.HandleException(ex, nameof(UpdateFlakonsByID), $"Parameter: FlakonID = " + FlakonID);
    }

    return (rowsAffected > 0);
}

        public static bool DeleteFlakons(int? FlakonID)
{
    int rowsAffected = 0;

    try
    {
        using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
        {
            string query = $@"SP_Delete_Flakons_ByID";  

            using (SqlCommand command = new SqlCommand(query, connection))
            {
                command.CommandType = CommandType.StoredProcedure;

                command.Parameters.AddWithValue("@FlakonID", FlakonID);

                connection.Open();

                rowsAffected = command.ExecuteNonQuery();
            }
        }
    }
    catch (Exception ex)
    {
        // Handle all exceptions in a general way, this includes errors from SP_HandleError if any
        ErrorHandler.HandleException(ex, nameof(DeleteFlakons), $"Parameter: FlakonID = " + FlakonID);
    }

    return (rowsAffected > 0);
}
        
        public static DataTable SearchData(string ColumnName, string SearchValue, string Mode = "Anywhere")
{
    DataTable dt = new DataTable();

    try
    {
        using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
        {
            string query = $@"SP_Search_Flakons_ByColumn";

            using (SqlCommand command = new SqlCommand(query, connection))
            {
                command.CommandType = CommandType.StoredProcedure;

                command.Parameters.AddWithValue("@ColumnName", ColumnName);
                command.Parameters.AddWithValue("@SearchValue", SearchValue);
                command.Parameters.AddWithValue("@Mode", Mode);  // Added Mode parameter

                connection.Open();

                using (SqlDataReader reader = command.ExecuteReader())
                {
                    if (reader.HasRows)
                    {
                        dt.Load(reader);
                    }

                    reader.Close();
                }
            }
        }
    }
    catch (Exception ex)
    {
        // Handle all exceptions in a general way
        ErrorHandler.HandleException(ex, nameof(SearchData), $"ColumnName: {ColumnName}, SearchValue: {SearchValue}, Mode: {Mode}");
    }

    return dt;
}
    }
}

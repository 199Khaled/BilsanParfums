
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using BilsanDb_DataAccess;
using Newtonsoft.Json;

namespace BilsanDb_DataLayer
{
    public class clsDuftÖleData
    {
        //#nullable enable

        public static bool GetDuftÖleInfoByID(int? ID , ref int? AlteNummer, ref string ParfümCode, ref int? Ölmenge, ref string Öltype, ref DateTime? Aktivierungsdatum)
{
    bool isFound = false;

    try
    {
        using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
        {
            string query = "SP_Get_DuftÖle_ByID";

            using (SqlCommand command = new SqlCommand(query, connection))
            {
                command.CommandType = CommandType.StoredProcedure;

                // Ensure correct parameter assignment
                command.Parameters.AddWithValue("@ID", ID ?? (object)DBNull.Value);

                connection.Open();
                using (SqlDataReader reader = command.ExecuteReader())
                { 
                    if (reader.Read())
                    {
                        // The record was found
                        isFound = true;

                                AlteNummer = reader["AlteNummer"] != DBNull.Value ? (int?)reader["AlteNummer"] : null;
                                ParfümCode = reader["ParfümCode"] != DBNull.Value ? reader["ParfümCode"].ToString() : null;
                                Ölmenge = reader["Ölmenge"] != DBNull.Value ? (int?)reader["Ölmenge"] : null;
                                Öltype = reader["Öltype"] != DBNull.Value ? reader["Öltype"].ToString() : null;
                                Aktivierungsdatum = reader["Aktivierungsdatum"] != DBNull.Value ? (DateTime?)reader["Aktivierungsdatum"] : null;

                    }
                }
            }
        }
    }
    catch (Exception ex)
    {
        // Handle all exceptions in a general way
        ErrorHandler.HandleException(ex, nameof(GetDuftÖleInfoByID), $"Parameter: ID = " + ID);
    }

    return isFound;
}

//        public static DataTable GetAllDuftÖle()
//{
//    DataTable dt = new DataTable();

//    try
//    {
//        using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
//        {
//            string query = "SP_Get_All_DuftÖle";

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
//        ErrorHandler.HandleException(ex, nameof(GetAllDuftÖle), "No parameters for this method.");
//    }

//    return dt;
//}
        public static DataTable GetAllDuftÖle()
        {
            DataTable dt = new DataTable();

            try
            {
                using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
                {
                    string query = @"
                SELECT 
                    ID AS ID,
                   AlteNummer AS AlteNummer,
                   ParfümCode As ParfümCode,
                   Ölmenge AS ÖlMengeInGram,
                   Öltype AS Öltype,
                   Aktivierungsdatum AS Aktivierungsdatum
                 
                FROM dbo.DuftÖle";

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
                ErrorHandler.HandleException(ex, nameof(GetAllDuftÖle), "No parameters for this method.");
            }

            return dt;
        }

        public static int? AddNewDuftÖle(int? AlteNummer, int? Ölmenge, DateTime? Aktivierungsdatum, string ParfümCode = null, string Öltype = null)
    {
        int? ID = null;

        try
        {
            using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
            {
                string query = @"SP_Add_DuftÖle";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.AddWithValue("@AlteNummer", AlteNummer ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@ParfümCode", ParfümCode ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@Ölmenge", Ölmenge ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@Öltype", Öltype ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@Aktivierungsdatum", Aktivierungsdatum ?? (object)DBNull.Value);


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
                        ID = (int)outputIdParam.Value;
                    }

                }
            }
        }
        catch (Exception ex)
        {
            // Handle all exceptions in a general way
            ErrorHandler.HandleException(ex, nameof(AddNewDuftÖle), $"Parameters: int? AlteNummer, int? Ölmenge, DateTime? Aktivierungsdatum, string ParfümCode = null, string Öltype = null");
        }

        return ID;
    }

        public static bool UpdateDuftÖleByID(int? ID, int? AlteNummer, int? Ölmenge, DateTime? Aktivierungsdatum, string ParfümCode = null, string Öltype = null)
{
    int rowsAffected = 0;

    try
    {
        using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
        {
            string query = $@"SP_Update_DuftÖle_ByID"; 

            using (SqlCommand command = new SqlCommand(query, connection))
            {
                command.CommandType = CommandType.StoredProcedure;

                // Create the parameters for the stored procedure
                    command.Parameters.AddWithValue("@ID", ID);
                    command.Parameters.AddWithValue("@AlteNummer", AlteNummer ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@ParfümCode", ParfümCode ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@Ölmenge", Ölmenge ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@Öltype", Öltype ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@Aktivierungsdatum", Aktivierungsdatum ?? (object)DBNull.Value);


                // Open the connection and execute the update
                connection.Open();
                rowsAffected = command.ExecuteNonQuery();
            }
        }
    }
    catch (Exception ex)
    {
        // Handle exceptions
        ErrorHandler.HandleException(ex, nameof(UpdateDuftÖleByID), $"Parameter: ID = " + ID);
    }

    return (rowsAffected > 0);
}

        public static bool DeleteDuftÖle(int? ID)
{
    int rowsAffected = 0;

    try
    {
        using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
        {
            string query = $@"SP_Delete_DuftÖle_ByID";  

            using (SqlCommand command = new SqlCommand(query, connection))
            {
                command.CommandType = CommandType.StoredProcedure;

                command.Parameters.AddWithValue("@ID", ID);

                connection.Open();

                rowsAffected = command.ExecuteNonQuery();
            }
        }
    }
    catch (Exception ex)
    {
        // Handle all exceptions in a general way, this includes errors from SP_HandleError if any
        ErrorHandler.HandleException(ex, nameof(DeleteDuftÖle), $"Parameter: ID = " + ID);
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
            string query = $@"SP_Search_DuftÖle_ByColumn";

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

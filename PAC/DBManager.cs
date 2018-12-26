using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace PAC
{
    public class DBManager
    {
        

        public static string[] getColumnName(string tableName)
        {
            SqlConnection sqlConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["SQLConnectionString"].ConnectionString);
            sqlConnection.Open();
            string query = "SELECT COLUMN_NAME FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = N'" + tableName +"'";
            SqlCommand cmd = new SqlCommand(query, sqlConnection);

            List<string> columnList = new List<string>();
            using (SqlDataReader reader = cmd.ExecuteReader()) {
                while (reader.Read())
                {
                    columnList.Add(reader.GetString(0));
                }
                reader.Close();
            }
            sqlConnection.Close();
            return columnList.ToArray();
        }

        public static void ListManager(List<string> listManager, string selectedValue)
        {
            if (selectedValue == "Others")
            {
                listManager.Add("'Dog', 'Cat'");
            }
            else
                listManager.Add(selectedValue);
            
        }
        

    }
}
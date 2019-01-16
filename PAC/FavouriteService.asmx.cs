using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Services;

namespace PAC
{
    /// <summary>
    /// Summary description for FavouriteService
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    [System.Web.Script.Services.ScriptService]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    // [System.Web.Script.Services.ScriptService]
    public class FavouriteService : System.Web.Services.WebService
    {

        [WebMethod]
        public void AddFavourite(Favourite favourite)
        {
            SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["SQLConnectionString"].ConnectionString);

            SqlCommand cmd = new SqlCommand("spAddFavourite", conn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add(new SqlParameter()
            {
                ParameterName = "@MemberListID",
                Value = favourite.MemberListID
            });
            cmd.Parameters.Add(new SqlParameter()
            {
                ParameterName = "@AnimalListID",
                Value = favourite.AnimalListID
            });
            conn.Open();
            cmd.ExecuteNonQuery();
        }
    }
}

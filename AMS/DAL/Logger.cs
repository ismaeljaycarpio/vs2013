using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using System.Data.SqlClient;
using System.Data;


namespace AMS.DAL
{
    public class Logger
    {
        SqlConnection conn;
        SqlCommand comm;
        SqlDataAdapter adp;
        DataTable dt;
        public static string CONN_STRING = WebConfigurationManager.ConnectionStrings["dbAMS"].ConnectionString;

        public void transactionLog(Guid userId,
            string action)
        {
            string strSql = "INSERT INTO TransactionLog VALUES(@UserId, @Action, @ActionDate)";
            using(conn = new SqlConnection(CONN_STRING))
            {
                comm = new SqlCommand(strSql, conn);
                comm.Parameters.AddWithValue("@UserId", userId);
                comm.Parameters.AddWithValue("@Action", action);
                comm.Parameters.AddWithValue("@ActionDate", DateTime.Now);
                conn.Open();
                comm.ExecuteNonQuery();
                conn.Close();
            }
        }

        public void errorLog(Guid userId, string error)
        {
            string strSql = "INSERT INTO ErroLog VALUES(@UserId, @Action, @ActionDate)";
            using (conn = new SqlConnection(CONN_STRING))
            {
                comm = new SqlCommand(strSql, conn);
                comm.Parameters.AddWithValue("@UserId", userId);
                comm.Parameters.AddWithValue("@Action", error);
                comm.Parameters.AddWithValue("@ActionDate", DateTime.Now);
                conn.Open();
                comm.ExecuteNonQuery();
                conn.Close();
            }
        }
    }
}
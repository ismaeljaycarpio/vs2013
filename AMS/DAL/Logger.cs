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
        string strSql = String.Empty;

        public static string CONN_STRING = WebConfigurationManager.ConnectionStrings["dbAMS"].ConnectionString;


        public DataTable displayAuditTrail()
        {
            strSql = "SELECT * FROM TrannsactionLog ORDER BY Id DESC";
            using(conn = new SqlConnection(CONN_STRING))
            {
                dt = new DataTable();
                comm = new SqlCommand(strSql, conn);
                adp = new SqlDataAdapter(comm);

                conn.Open();
                adp.Fill(dt);
                conn.Close();

                return dt;
            }
        }

        public void transactionLog(Guid userId,
            string action)
        {
            strSql = "INSERT INTO TransactionLog VALUES(@UserId, @Action, @ActionDate)";
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
            strSql = "INSERT INTO ErroLog VALUES(@UserId, @Action, @ActionDate)";
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
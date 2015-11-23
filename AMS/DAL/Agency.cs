using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using System.Data.SqlClient;
using System.Data;

namespace AMS.DAL
{
    public class Agency
    {
        //init
        SqlConnection conn;
        SqlCommand comm;
        SqlDataAdapter adp;
        DataTable dt;
        string strSql = "";

        public DataTable DisplayAgency()
        {
            strSql = "SELECT * FROM AGENCY";

            conn = new SqlConnection();
            conn.ConnectionString = WebConfigurationManager.ConnectionStrings["dbAMS"].ConnectionString;
            comm = new SqlCommand(strSql, conn);
            dt = new DataTable();
            adp = new SqlDataAdapter(comm);

            conn.Open();
            adp.Fill(dt);
            conn.Close();

            return dt;
        }

        public DataTable GetAgencyById(int rowId)
        {
            strSql = "SELECT * FROM AGENCY WHERE Id = @Id";

            conn = new SqlConnection();
            conn.ConnectionString = WebConfigurationManager.ConnectionStrings["dbAMS"].ConnectionString;
            comm = new SqlCommand(strSql, conn);
            comm.Parameters.AddWithValue("@Id", rowId);
            dt = new DataTable();
            adp = new SqlDataAdapter(comm);

            conn.Open();
            adp.Fill(dt);
            conn.Close();

            return dt;
        }

        public void AddAgency(
            string agencyName,
            string modifiedBy)
        {
            strSql = "INSERT INTO AGENCY(Agency,ModifiedBy) " +
                "VALUES(@Agency,@ModifiedBy)";

            conn = new SqlConnection();
            conn.ConnectionString = WebConfigurationManager.ConnectionStrings["dbAMS"].ConnectionString;

            using (comm = new SqlCommand(strSql, conn))
            {
                conn.Open();
                comm.Parameters.AddWithValue("@Agency", agencyName);
                comm.Parameters.AddWithValue("@ModifiedBy", modifiedBy);
                comm.ExecuteNonQuery();
                conn.Close();
            }
            comm.Dispose();
            conn.Dispose();
        }

        public void UpdateAgency(
            string agencyName,
            string modifiedBy,
            string rowId)
        {
            strSql = "UPDATE AGENCY SET " +
                "Agency = @Agency, " +
                "ModifiedDate = @ModifiedDate, " +
                "ModifiedBy = @ModifiedBy " +
                "WHERE Id = @RowId";

            conn = new SqlConnection();
            conn.ConnectionString = WebConfigurationManager.ConnectionStrings["dbAMS"].ConnectionString;

            using (comm = new SqlCommand(strSql, conn))
            {
                conn.Open();
                comm.Parameters.AddWithValue("@Agency", agencyName);
                comm.Parameters.AddWithValue("@ModifiedDate", DateTime.Now);
                comm.Parameters.AddWithValue("@ModifiedBy", modifiedBy);
                comm.Parameters.AddWithValue("@RowId", rowId);

                comm.ExecuteNonQuery();
                conn.Close();
            }
            comm.Dispose();
            conn.Dispose();
        }

        public void DeleteAgency(string rowId)
        {
            strSql = "DELETE FROM AGENCY WHERE Id = @Id";

            conn = new SqlConnection();
            conn.ConnectionString = WebConfigurationManager.ConnectionStrings["dbAMS"].ConnectionString;

            using (comm = new SqlCommand(strSql, conn))
            {
                conn.Open();
                comm.Parameters.AddWithValue("@Id", rowId);

                comm.ExecuteNonQuery();
                conn.Close();
            }
            comm.Dispose();
            conn.Dispose();
        }

        public bool CheckIfDuplicate(string agencyName)
        {
            strSql = "SELECT Agency FROM AGENCY WHERE Agency = @Agency";

            conn = new SqlConnection();
            conn.ConnectionString = WebConfigurationManager.ConnectionStrings["dbAMS"].ConnectionString;
            comm = new SqlCommand(strSql, conn);
            comm.Parameters.AddWithValue("@Agency", agencyName);
            dt = new DataTable();
            adp = new SqlDataAdapter(comm);

            conn.Open();
            adp.Fill(dt);
            conn.Close();

            if (dt.Rows.Count > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

    }
}
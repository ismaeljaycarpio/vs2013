using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using System.Data.SqlClient;
using System.Data;

namespace AMS.DAL
{
    public class Leaves
    {
        #region FileMaintenance
        //init
        SqlConnection conn;
        SqlCommand comm;
        SqlDataAdapter adp;
        DataTable dt;
        string strSql = "";

        public DataTable displayLeaves()
        {
            strSql = "SELECT * FROM LeaveType";

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

        public DataTable getLeaveId(int rowId)
        {
            strSql = "SELECT * FROM LeaveType WHERE Id = @Id";

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

        public void addLeaveType(
            string leaveName,
            string noOfDays
            )
        {
            strSql = "INSERT INTO LeaveType(LeaveName,NoOfDays) " +
                "VALUES(@LeaveName,@NoOfDays)";

            conn = new SqlConnection();
            conn.ConnectionString = WebConfigurationManager.ConnectionStrings["dbAMS"].ConnectionString;

            using (comm = new SqlCommand(strSql, conn))
            {
                conn.Open();
                comm.Parameters.AddWithValue("@LeaveName", leaveName);
                comm.Parameters.AddWithValue("@NoOfDays", noOfDays);
                comm.ExecuteNonQuery();
                conn.Close();
            }
            comm.Dispose();
            conn.Dispose();
        }

        public void updateLeave(
            string leaveName,
            string noOfDays,
            string rowId)
        {
            strSql = "UPDATE LeaveType SET " +
                "LeaveName = @Agency, " +
                "NoOfDays = @NoOfDays," +
                "ModifiedDate = @ModifiedDate " +
                "WHERE Id = @RowId";

            conn = new SqlConnection();
            conn.ConnectionString = WebConfigurationManager.ConnectionStrings["dbAMS"].ConnectionString;

            using (comm = new SqlCommand(strSql, conn))
            {
                conn.Open();
                comm.Parameters.AddWithValue("@Agency", leaveName);
                comm.Parameters.AddWithValue("@NoOfDays", noOfDays);
                comm.Parameters.AddWithValue("@ModifiedDate", DateTime.Now);
                comm.Parameters.AddWithValue("@RowId", rowId);

                comm.ExecuteNonQuery();
                conn.Close();
            }
            comm.Dispose();
            conn.Dispose();
        }

        public void deleteLeave(string rowId)
        {
            strSql = "DELETE FROM LeaveType WHERE Id = @Id";

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
        #endregion
    }
}
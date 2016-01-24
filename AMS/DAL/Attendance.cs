using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using System.Data.SqlClient;
using System.Data;
using System.Text;
using System.Security.Permissions;

namespace AMS.DAL
{
    public class Attendance
    {
        SqlConnection conn;
        SqlCommand comm;
        SqlDataAdapter adp;
        DataTable dt;
        string strSql = "";

        public void TimeIn(
            Guid UserId,
            DateTime timeIn,
            DateTime timeOut,
            string remarks,
            string totalHours)
        {
            strSql = "INSERT INTO TimeInTimeOut(UserId,TimeIn,TimeOut,Remarks,TotalHours) " +
                "VALUES(@UserId, @TimeIn, @TimeOut, @Remarks, @TotalHours)";

            conn = new SqlConnection();
            conn.ConnectionString = WebConfigurationManager.ConnectionStrings["dbAMS"].ConnectionString;

            using (comm = new SqlCommand(strSql, conn))
            {
                conn.Open();
                comm.Parameters.AddWithValue("@UserId", UserId);
                comm.Parameters.AddWithValue("@TimeIn", timeIn);
                comm.Parameters.AddWithValue("@TimeOut", timeOut);
                comm.Parameters.AddWithValue("@Remarks", remarks);
                comm.Parameters.AddWithValue("@TotalHours", totalHours);

                comm.ExecuteNonQuery();
                conn.Close();
            }
            comm.Dispose();
            conn.Dispose();
        }

        public void TimeOut(
            int attendaceId,
            DateTime timeOut)
        {
            strSql = "UDPATE TimeInTimeOut SET TimeOut = @TimeOut WHERE Id = @Id";

            conn = new SqlConnection();
            conn.ConnectionString = WebConfigurationManager.ConnectionStrings["dbAMS"].ConnectionString;

            using (comm = new SqlCommand(strSql, conn))
            {
                conn.Open();
                comm.Parameters.AddWithValue("@TimeOut", timeOut);
                comm.ExecuteNonQuery();
                conn.Close();
            }
            comm.Dispose();
            conn.Dispose();
        }

        public string getMaxId(Guid UserId)
        {
            strSql = "SELECT MAX(Id) FROM TimeInTimeOut WHERE UserId = @UserId";
            string strMaxId = String.Empty;

            conn = new SqlConnection();
            conn.ConnectionString = WebConfigurationManager.ConnectionStrings["dbAMS"].ConnectionString;
            comm = new SqlCommand(strSql, conn);
            comm.Parameters.AddWithValue("@UserId", UserId);
            dt = new DataTable();
            adp = new SqlDataAdapter(comm);

            conn.Open();
            adp.Fill(dt);
            conn.Close();

            if(dt.Rows.Count > 0)
            {
                strMaxId = dt.Rows[0]["Id"].ToString();
                return strMaxId;
            }

            return strMaxId;
        }
    }
}
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
            string description,
            string venue,
            string date,
            string details)
        {
            strSql = "INSERT INTO TRAININGS(UserId,Description,Venue,Date,Details) " +
                "VALUES(@UserId, @Description, @Venue, @Date, @Details)";

            conn = new SqlConnection();
            conn.ConnectionString = WebConfigurationManager.ConnectionStrings["dbAMS"].ConnectionString;

            using (comm = new SqlCommand(strSql, conn))
            {
                conn.Open();
                comm.Parameters.AddWithValue("@UserId", UserId);
                comm.Parameters.AddWithValue("@Description", description);
                comm.Parameters.AddWithValue("@Venue", venue);
                comm.Parameters.AddWithValue("@Date", date);
                comm.Parameters.AddWithValue("@Details", details);

                comm.ExecuteNonQuery();
                conn.Close();
            }
            comm.Dispose();
            conn.Dispose();
        }
    }
}
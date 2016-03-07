using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using System.Data.SqlClient;
using System.Data;

namespace AMS.DAL
{
    public class Schedule
    {
        //init
        SqlConnection conn;
        SqlCommand comm;
        SqlDataAdapter adp;
        DataTable dt;
        string strSql = "";


        public DataTable GetScheduleById(Guid userId)
        {
            strSql = "SELECT * FROM Schedule WHERE UserId = @UserId AND Id = (SELECT MAX(Id) FROM Schedule WHERE UserId = @UserId)";

            conn = new SqlConnection();
            conn.ConnectionString = WebConfigurationManager.ConnectionStrings["dbAMS"].ConnectionString;
            comm = new SqlCommand(strSql, conn);
            comm.Parameters.AddWithValue("@UserId", userId);
            dt = new DataTable();
            adp = new SqlDataAdapter(comm);

            conn.Open();
            adp.Fill(dt);
            conn.Close();

            return dt;
        }

        public void addSchedule(
            Guid userId,
            string timeStart,
            string timeEnd)
        {
            strSql = "INSERT INTO Schedule(UserId,TimeStart,TimeEnd) " +
                "VALUES(@UserId,@TimeStart,@TimeEnd)";

            conn = new SqlConnection();
            conn.ConnectionString = WebConfigurationManager.ConnectionStrings["dbAMS"].ConnectionString;

            using (comm = new SqlCommand(strSql, conn))
            {
                conn.Open();
                comm.Parameters.AddWithValue("@UserId", userId);
                comm.Parameters.AddWithValue("@TimeStart", timeStart);
                comm.Parameters.AddWithValue("@TimeEnd", timeEnd);
                comm.ExecuteNonQuery();
                conn.Close();
            }
            comm.Dispose();
            conn.Dispose();
        }
    }
}
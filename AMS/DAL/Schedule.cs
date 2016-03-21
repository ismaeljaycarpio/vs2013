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


        public DataTable getScheduleToday(Guid userId)
        {
            strSql = "SELECT * FROM Schedule WHERE UserId = @UserId ORDER BY TimeStart DESC";

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

        public DataTable GetScheduleById(Guid userId)
        {
            strSql = "SELECT * FROM Schedule WHERE UserId = @UserId ORDER BY TimeStart DESC";

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

        //get assigned attendace
        //limit the record for 2 days only
        //good for dayshift, midshift only
        //need to rework for night shifg because of adjustment of day
        public DataTable getScheduleTodayAttendance(Guid userId)
        {
            strSql = "SELECT * FROM Schedule " +
                "WHERE UserId = @UserId " +
                "AND TimeStart IS NOT NULL " +
                "AND TimeEnd IS NOT NULL " +
                "AND (TimeStart >= @thisDay AND TimeStart < @this2Day)  " +
                "ORDER BY TimeStart DESC";

            conn = new SqlConnection();
            conn.ConnectionString = WebConfigurationManager.ConnectionStrings["dbAMS"].ConnectionString;
            comm = new SqlCommand(strSql, conn);
            comm.Parameters.AddWithValue("@UserId", userId);
            comm.Parameters.AddWithValue("@thisDay", DateTime.Today);
            comm.Parameters.AddWithValue("@this2Day", DateTime.Today.AddDays(1));
            dt = new DataTable();
            adp = new SqlDataAdapter(comm);

            conn.Open();
            adp.Fill(dt);
            conn.Close();

            return dt;
        }

        public DataTable GetScheduleById(int rowId)
        {
            strSql = "SELECT * FROM Schedule WHERE Id = @Id";

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

        public void addSchedule(
            Guid userId,
            string timeStart,
            string timeEnd,
            string status)
        {
            strSql = "INSERT INTO Schedule(UserId,TimeStart,TimeEnd,Status) " +
                "VALUES(@UserId,@TimeStart,@TimeEnd,@Status)";

            conn = new SqlConnection();
            conn.ConnectionString = WebConfigurationManager.ConnectionStrings["dbAMS"].ConnectionString;

            using (comm = new SqlCommand(strSql, conn))
            {
                conn.Open();
                comm.Parameters.AddWithValue("@UserId", userId);
                comm.Parameters.AddWithValue("@TimeStart", Convert.ToDateTime(timeStart));
                comm.Parameters.AddWithValue("@TimeEnd", Convert.ToDateTime(timeEnd));
                comm.Parameters.AddWithValue("@Status", status);
                comm.ExecuteNonQuery();
                conn.Close();
            }
            comm.Dispose();
            conn.Dispose();
        }

        public void updateSchedule(
            string timeStart,
            string timeEnd,
            string status,
            string rowId)
        {
            strSql = "UPDATE Schedule SET TimeStart=@TimeStart, TimeEnd=@TimeEnd, Status=@Status WHERE Id=@Id";

            conn = new SqlConnection();
            conn.ConnectionString = WebConfigurationManager.ConnectionStrings["dbAMS"].ConnectionString;

            using (comm = new SqlCommand(strSql, conn))
            {
                conn.Open();
                comm.Parameters.AddWithValue("@Id", rowId);
                comm.Parameters.AddWithValue("@TimeStart", Convert.ToDateTime(timeStart));
                comm.Parameters.AddWithValue("@TimeEnd", Convert.ToDateTime(timeEnd));
                comm.Parameters.AddWithValue("@Status", status);
                comm.ExecuteNonQuery();
                conn.Close();
            }
            comm.Dispose();
            conn.Dispose();
        }

        public void deleteSchedule(string rowId)
        {
            strSql = "DELETE FROM Schedule WHERE Id = @Id";

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

        public void updateTimeIn(string rowId, string remarks)
        {
            strSql = "UPDATE Schedule SET TimeIn=@TimeIn, Remarks=(Remarks + @Remarks) WHERE Id=@Id";

            conn = new SqlConnection();
            conn.ConnectionString = WebConfigurationManager.ConnectionStrings["dbAMS"].ConnectionString;

            using (comm = new SqlCommand(strSql, conn))
            {
                conn.Open();
                comm.Parameters.AddWithValue("@Id", rowId);
                comm.Parameters.AddWithValue("@TimeIn", DateTime.Now);
                comm.Parameters.AddWithValue("@Remarks", remarks);
                comm.ExecuteNonQuery();
                conn.Close();
            }
            comm.Dispose();
            conn.Dispose();
        }

        public void updateTimeOut(string rowId,string remarks)
        {
            strSql = "UPDATE Schedule SET TimeOut=@TimeOut, Remarks=(Remarks + @Remarks) WHERE Id=@Id";

            conn = new SqlConnection();
            conn.ConnectionString = WebConfigurationManager.ConnectionStrings["dbAMS"].ConnectionString;

            using (comm = new SqlCommand(strSql, conn))
            {
                conn.Open();
                comm.Parameters.AddWithValue("@Id", rowId);
                comm.Parameters.AddWithValue("@TimeOut", DateTime.Now);
                comm.Parameters.AddWithValue("@Remarks", remarks);
                comm.ExecuteNonQuery();
                conn.Close();
            }
            comm.Dispose();
            conn.Dispose();
        }

        public void insertSchedByStaff_timeIn(Guid userId, string remarks)
        {
            strSql = "INSERT INTO Schedule(UserId,TimeIn,Remarks) VALUES(@UserId,@TimeIn,@Remarks)";

            conn = new SqlConnection();
            conn.ConnectionString = WebConfigurationManager.ConnectionStrings["dbAMS"].ConnectionString;

            using (comm = new SqlCommand(strSql, conn))
            {
                conn.Open();
                comm.Parameters.AddWithValue("@TimeIn", DateTime.Now);
                comm.Parameters.AddWithValue("@UserId", userId);
                comm.Parameters.AddWithValue("@Remarks", remarks);
                comm.ExecuteNonQuery();
                conn.Close();
            }
            comm.Dispose();
            conn.Dispose();
        }

        public void insertSchedByStaff_timeOut(Guid userId, string remarks)
        {
            strSql = "INSERT INTO Schedule(UserId,TimeOut,Remarks) VALUES(@UserId,@TimeOut,@Remarks)";

            conn = new SqlConnection();
            conn.ConnectionString = WebConfigurationManager.ConnectionStrings["dbAMS"].ConnectionString;

            using (comm = new SqlCommand(strSql, conn))
            {
                conn.Open();
                comm.Parameters.AddWithValue("@TimeOut", DateTime.Now);
                comm.Parameters.AddWithValue("@Remarks", remarks);
                comm.Parameters.AddWithValue("@UserId", userId);
                comm.ExecuteNonQuery();
                conn.Close();
            }
            comm.Dispose();
            conn.Dispose();
        }
    }
}
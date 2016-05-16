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
            string remarks)
        {
            strSql = "INSERT INTO TimeInTimeOut(UserId,TimeIn,Remarks) " +
                "VALUES(@UserId, @TimeIn, @Remarks)";

            conn = new SqlConnection();
            conn.ConnectionString = WebConfigurationManager.ConnectionStrings["dbAMS"].ConnectionString;

            using (comm = new SqlCommand(strSql, conn))
            {
                conn.Open();
                comm.Parameters.AddWithValue("@UserId", UserId);
                comm.Parameters.AddWithValue("@TimeIn", timeIn);
                comm.Parameters.AddWithValue("@Remarks", remarks);

                comm.ExecuteNonQuery();
                conn.Close();
            }
            comm.Dispose();
            conn.Dispose();
        }

        public void TimeOut(
            Guid UserId,
            DateTime timeOut,
            string remarks)
        {
            strSql = "INSERT INTO TimeInTimeOut(UserId,TimeOut,Remarks) " +
                "VALUES(@UserId, @TimeOut, @Remarks)";

            conn = new SqlConnection();
            conn.ConnectionString = WebConfigurationManager.ConnectionStrings["dbAMS"].ConnectionString;

            using (comm = new SqlCommand(strSql, conn))
            {
                conn.Open();
                comm.Parameters.AddWithValue("@UserId", UserId);
                comm.Parameters.AddWithValue("@TimeOut", timeOut);
                comm.Parameters.AddWithValue("@Remarks", remarks);

                comm.ExecuteNonQuery();
                conn.Close();
            }
            comm.Dispose();
            conn.Dispose();
        }

        public void updateTimeOut(
            string rowId,
            DateTime timeOut)
        {
            strSql = "UPDATE TimeInTimeOut SET TimeOut = @TimeOut WHERE Id = @Id";

            conn = new SqlConnection();
            conn.ConnectionString = WebConfigurationManager.ConnectionStrings["dbAMS"].ConnectionString;

            using (comm = new SqlCommand(strSql, conn))
            {
                conn.Open();
                comm.Parameters.AddWithValue("@Id", rowId);
                comm.Parameters.AddWithValue("@TimeOut", timeOut);

                comm.ExecuteNonQuery();
                conn.Close();
            }
            comm.Dispose();
            conn.Dispose();
        }


        public DataTable getlastTimeIn(Guid UserId)
        {
            strSql = "SELECT Id,TimeIn FROM TimeInTimeOut WHERE UserId = @UserId AND Id = (SELECT MAX(Id) FROM TimeInTimeOut)";
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

            return dt;
        }

        public DataTable DisplayAttendance(Guid userId)
        {
            strSql = "SELECT * FROM Schedule " +
                "WHERE UserId = @UserId " +
                "AND TimeIn IS NOT NULL " +
                "AND TimeOut IS NOT NULL " +
                "ORDER BY Id DESC";

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

        //active only
        public DataTable DisplayAttendance()
        {
            strSql = "SELECT EMPLOYEE.UserId, EMPLOYEE.Emp_Id, (EMPLOYEE.LastName + ',' + EMPLOYEE.FirstName + ' ' + EMPLOYEE.MiddleName) AS [FullName]," +
                "TimeIn, TimeOut, Remarks, Status, TimeStart, TimeEnd " +
                "FROM EMPLOYEE INNER JOIN Schedule " +
                "ON EMPLOYEE.UserId = Schedule.UserId " +
                "AND EMPLOYEE.AccountStatusId = 1 " +
                "AND (TimeIn IS NOT NULL OR TimeOut IS NOT NULL OR Status = 'DayOff') " +
                "ORDER BY Schedule.TimeIn DESC, Schedule.TimeOut DESC";

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

        public DataTable DisplayAttendance(string startDate)
        {
            strSql = "SELECT EMPLOYEE.UserId, EMPLOYEE.Emp_Id, (EMPLOYEE.LastName + ',' + EMPLOYEE.FirstName + ' ' + EMPLOYEE.MiddleName) AS [FullName]," +
                "TimeIn, TimeOut, Remarks, Status, TimeStart, TimeEnd " +
                "FROM EMPLOYEE INNER JOIN Schedule " +
                "ON EMPLOYEE.UserId = Schedule.UserId " +
                "AND EMPLOYEE.AccountStatusId = 1 " +
                "AND (TimeIn IS NOT NULL OR TimeOut IS NOT NULL) " +
                "AND ((Schedule.TimeIn >= @startDate AND Schedule.TimeIn < DATEADD(DAY,1,@startDate)) " +
                "OR (Schedule.TimeOut >= @startDate AND Schedule.TimeOut < DATEADD(DAY,1,@startDate))) " +
                "ORDER BY Schedule.TimeStart DESC";

            conn = new SqlConnection();
            conn.ConnectionString = WebConfigurationManager.ConnectionStrings["dbAMS"].ConnectionString;
            comm = new SqlCommand(strSql, conn);
            comm.Parameters.AddWithValue("@startDate", startDate);
            dt = new DataTable();
            adp = new SqlDataAdapter(comm);

            conn.Open();
            adp.Fill(dt);
            conn.Close();

            return dt;
        }

        public DataTable DisplayAttendanceOfUser(Guid UserId, string startDate)
        {
            strSql = "SELECT EMPLOYEE.UserId, EMPLOYEE.Emp_Id, (EMPLOYEE.LastName + ',' + EMPLOYEE.FirstName + ' ' + EMPLOYEE.MiddleName) AS [FullName]," +
                "TimeIn, TimeOut, Remarks, Status, TimeStart, TimeEnd " +
                "FROM EMPLOYEE INNER JOIN Schedule " +
                "ON EMPLOYEE.UserId = Schedule.UserId " +
                "AND EMPLOYEE.AccountStatusId = 1 " +
                "AND (TimeIn IS NOT NULL OR TimeOut IS NOT NULL) " +
                "AND EMPLOYEE.UserId = @UserId " +
                "AND ((Schedule.TimeIn >= @startDate AND Schedule.TimeIn < DATEADD(DAY,1,@startDate)) " +
                "OR (Schedule.TimeOut >= @startDate AND Schedule.TimeOut < DATEADD(DAY,1,@startDate))) " +
                "ORDER BY Schedule.TimeStart DESC";

            conn = new SqlConnection();
            conn.ConnectionString = WebConfigurationManager.ConnectionStrings["dbAMS"].ConnectionString;
            comm = new SqlCommand(strSql, conn);
            comm.Parameters.AddWithValue("@UserId", UserId);
            comm.Parameters.AddWithValue("@startDate", startDate);
            dt = new DataTable();
            adp = new SqlDataAdapter(comm);

            conn.Open();
            adp.Fill(dt);
            conn.Close();

            return dt;
        }

        public DataTable DisplayAttendanceOfUser(Guid UserId, string startDate, bool someValue)
        {
            strSql = "SELECT EMPLOYEE.UserId, EMPLOYEE.Emp_Id, (EMPLOYEE.LastName + ',' + EMPLOYEE.FirstName + ' ' + EMPLOYEE.MiddleName) AS [FullName]," +
                "TimeIn, TimeOut, Remarks, Status, TimeStart, TimeEnd " +
                "FROM EMPLOYEE INNER JOIN Schedule " +
                "ON EMPLOYEE.UserId = Schedule.UserId " +
                "AND EMPLOYEE.AccountStatusId = 1 " +
                "AND EMPLOYEE.UserId = @UserId " +
                "AND ((Schedule.TimeIn >= @startDate AND Schedule.TimeIn < DATEADD(DAY,1,@startDate)) " +
                "OR (Schedule.TimeOut >= @startDate AND Schedule.TimeOut < DATEADD(DAY,1,@startDate))) " +
                "ORDER BY Schedule.TimeStart DESC";

            conn = new SqlConnection();
            conn.ConnectionString = WebConfigurationManager.ConnectionStrings["dbAMS"].ConnectionString;
            comm = new SqlCommand(strSql, conn);
            comm.Parameters.AddWithValue("@UserId", UserId);
            comm.Parameters.AddWithValue("@startDate", startDate);
            dt = new DataTable();
            adp = new SqlDataAdapter(comm);

            conn.Open();
            adp.Fill(dt);
            conn.Close();

            return dt;
        }

        public DataTable DisplayAttendance(string startDate, string endDate)
        {
            strSql = "SELECT EMPLOYEE.UserId, EMPLOYEE.Emp_Id, (EMPLOYEE.LastName + ',' + EMPLOYEE.FirstName + ' ' + EMPLOYEE.MiddleName) AS [FullName]," +
                "TimeIn, TimeOut, Remarks, Status, TimeStart, TimeEnd " +
                "FROM EMPLOYEE INNER JOIN Schedule " +
                "ON EMPLOYEE.UserId = Schedule.UserId " +
                "AND EMPLOYEE.AccountStatusId = 1 " +
                "AND (TimeIn IS NOT NULL OR TimeOut IS NOT NULL) " +
                "AND (Schedule.TimeIn BETWEEN @startDate AND @endDate OR Schedule.TimeOut BETWEEN @startDate AND @endDate) " +
                "ORDER BY Schedule.TimeStart DESC";

            conn = new SqlConnection();
            conn.ConnectionString = WebConfigurationManager.ConnectionStrings["dbAMS"].ConnectionString;
            comm = new SqlCommand(strSql, conn);
            comm.Parameters.AddWithValue("@startDate", startDate);
            comm.Parameters.AddWithValue("@endDate", endDate);
            dt = new DataTable();
            adp = new SqlDataAdapter(comm);

            conn.Open();
            adp.Fill(dt);
            conn.Close();

            return dt;
        }

        //active only
        public DataTable DisplayAttendanceOfUser(Guid UserId)
        {
            strSql = "SELECT EMPLOYEE.UserId, EMPLOYEE.Emp_Id, (EMPLOYEE.LastName + ',' + EMPLOYEE.FirstName + ' ' + EMPLOYEE.MiddleName) AS [FullName]," +
                "TimeIn, TimeOut, Remarks, Status, TimeStart, TimeEnd " +
                "FROM EMPLOYEE INNER JOIN Schedule " +
                "ON EMPLOYEE.UserId = Schedule.UserId " +
                "AND EMPLOYEE.AccountStatusId = 1 " +
                "AND (TimeIn IS NOT NULL OR TimeOut IS NOT NULL) " +
                "AND EMPLOYEE.UserId = @UserId " +
                "ORDER BY Schedule.TimeStart DESC";

            conn = new SqlConnection();
            conn.ConnectionString = WebConfigurationManager.ConnectionStrings["dbAMS"].ConnectionString;
            comm = new SqlCommand(strSql, conn);
            comm.Parameters.AddWithValue("@UserId", UserId);
            dt = new DataTable();
            adp = new SqlDataAdapter(comm);

            conn.Open();
            adp.Fill(dt);
            conn.Close();

            return dt;
        }

        public DataTable DisplayAttendanceOfUser(Guid UserId, bool someValue)
        {
            strSql = "SELECT EMPLOYEE.UserId, EMPLOYEE.Emp_Id, (EMPLOYEE.LastName + ',' + EMPLOYEE.FirstName + ' ' + EMPLOYEE.MiddleName) AS [FullName]," +
                "TimeIn, TimeOut, Remarks, Status, TimeStart, TimeEnd " +
                "FROM EMPLOYEE INNER JOIN Schedule " +
                "ON EMPLOYEE.UserId = Schedule.UserId " +
                "AND EMPLOYEE.AccountStatusId = 1 " +
                "AND EMPLOYEE.UserId = @UserId " +
                "ORDER BY Schedule.TimeStart DESC";

            conn = new SqlConnection();
            conn.ConnectionString = WebConfigurationManager.ConnectionStrings["dbAMS"].ConnectionString;
            comm = new SqlCommand(strSql, conn);
            comm.Parameters.AddWithValue("@UserId", UserId);
            dt = new DataTable();
            adp = new SqlDataAdapter(comm);

            conn.Open();
            adp.Fill(dt);
            conn.Close();

            return dt;
        }

        public DataTable DisplayAttendanceOfUser(Guid UserId, string startDate, string endDate)
        {
            strSql = "SELECT EMPLOYEE.UserId, EMPLOYEE.Emp_Id, (EMPLOYEE.LastName + ',' + EMPLOYEE.FirstName + ' ' + EMPLOYEE.MiddleName) AS [FullName]," +
                "TimeIn, TimeOut, Remarks, Status, TimeStart, TimeEnd " +
                "FROM EMPLOYEE INNER JOIN Schedule " +
                "ON EMPLOYEE.UserId = Schedule.UserId " +
                "AND EMPLOYEE.AccountStatusId = 1 " +
                "AND (TimeIn IS NOT NULL OR TimeOut IS NOT NULL) " +
                "AND EMPLOYEE.UserId = @UserId " +
                "AND (Schedule.TimeIn BETWEEN @startDate AND @endDate " +
                "OR Schedule.TimeOut BETWEEN @startDate AND @endDate) " +
                "ORDER BY Schedule.TimeStart DESC";

            conn = new SqlConnection();
            conn.ConnectionString = WebConfigurationManager.ConnectionStrings["dbAMS"].ConnectionString;
            comm = new SqlCommand(strSql, conn);
            comm.Parameters.AddWithValue("@UserId", UserId);
            comm.Parameters.AddWithValue("@startDate", startDate);
            comm.Parameters.AddWithValue("@endDate", endDate);
            dt = new DataTable();
            adp = new SqlDataAdapter(comm);

            conn.Open();
            adp.Fill(dt);
            conn.Close();

            return dt;
        }

        public DataTable DisplayAttendanceOfUser(Guid UserId, string startDate, string endDate, bool someValue)
        {
            strSql = "SELECT EMPLOYEE.UserId, EMPLOYEE.Emp_Id, (EMPLOYEE.LastName + ',' + EMPLOYEE.FirstName + ' ' + EMPLOYEE.MiddleName) AS [FullName]," +
                "TimeIn, TimeOut, Remarks, Status, TimeStart, TimeEnd " +
                "FROM EMPLOYEE INNER JOIN Schedule " +
                "ON EMPLOYEE.UserId = Schedule.UserId " +
                "AND EMPLOYEE.AccountStatusId = 1 " +
                "AND EMPLOYEE.UserId = @UserId " +
                "AND (Schedule.TimeIn BETWEEN @startDate AND @endDate " +
                "OR Schedule.TimeOut BETWEEN @startDate AND @endDate) " +
                "ORDER BY Schedule.TimeStart DESC";

            conn = new SqlConnection();
            conn.ConnectionString = WebConfigurationManager.ConnectionStrings["dbAMS"].ConnectionString;
            comm = new SqlCommand(strSql, conn);
            comm.Parameters.AddWithValue("@UserId", UserId);
            comm.Parameters.AddWithValue("@startDate", startDate);
            comm.Parameters.AddWithValue("@endDate", endDate);
            dt = new DataTable();
            adp = new SqlDataAdapter(comm);

            conn.Open();
            adp.Fill(dt);
            conn.Close();

            return dt;
        }

        public DataTable displayEmployee()
        {
            strSql = "SELECT UserId, LastName + ',' + FirstName + ' ' + MiddleName AS [FullName] " +
                "FROM EMPLOYEE ORDER BY FullName";

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

    }
}
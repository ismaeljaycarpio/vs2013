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

        public DataTable DisplayAttendance(Guid userId)
        {
            strSql = "SELECT * FROM TimeInTimeOut WHERE UserId = @UserId ORDER BY Id DESC";

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
        public DataTable DisplayAttendance(string keyWord)
        {
            strSql = "SELECT EMPLOYEE.UserId, EMPLOYEE.Emp_Id, (EMPLOYEE.LastName + ',' + EMPLOYEE.FirstName + ' ' + EMPLOYEE.MiddleName) AS [FullName]," +
                "TimeIn, TimeOut, Remarks " +
                "FROM EMPLOYEE INNER JOIN TimeInTimeOut " +
                "ON EMPLOYEE.UserId = TimeInTimeOut.UserId " +
                "AND " +
                "(EMPLOYEE.Emp_Id LIKE '%' + @searchKeyWord + '%' " +
                "OR EMPLOYEE.FirstName LIKE '%' + @searchKeyWord + '%' " +
                "OR EMPLOYEE.MiddleName LIKE '%' + @searchKeyWord + '%' " +
                "OR EMPLOYEE.LastName LIKE '%' + @searchKeyWord + '%') " +
                "AND EMPLOYEE.AccountStatusId = 1 " + 
                "ORDER BY TimeInTimeOut.Id DESC";

            conn = new SqlConnection();
            conn.ConnectionString = WebConfigurationManager.ConnectionStrings["dbAMS"].ConnectionString;
            comm = new SqlCommand(strSql, conn);
            comm.Parameters.AddWithValue("@searchKeyWord", keyWord);
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
                "TimeIn, TimeOut, Remarks " +
                "FROM EMPLOYEE INNER JOIN TimeInTimeOut " +
                "ON EMPLOYEE.UserId = TimeInTimeOut.UserId " +
                "AND EMPLOYEE.AccountStatusId = 1 " +
                "AND EMPLOYEE.UserId = @UserId " + 
                "ORDER BY TimeInTimeOut.Id DESC";

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
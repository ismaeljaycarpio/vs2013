using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using System.Data.SqlClient;
using System.Data;
using System.Text;
using System.Web.Security;

namespace AMS.DAL
{
    public class Account
    {
        SqlConnection conn;
        SqlCommand comm;
        SqlDataAdapter adp;
        DataTable dt;
        string strSql = String.Empty;

        public DataTable DisplayUserAccounts(string searchKeyWord)
        {
            strSql = "SELECT Memberships.UserId, Memberships.IsApproved, " +
                "EMPLOYEE.Emp_Id, " +
                "(EMPLOYEE.LastName + ', ' + EMPLOYEE.FirstName + ' ' + EMPLOYEE.MiddleName) AS [FullName] " +
                "FROM Memberships, EMPLOYEE " +
                "WHERE " +
                "Memberships.UserId = EMPLOYEE.UserId " +
                "AND (EMPLOYEE.Emp_Id LIKE '%' + @searchKeyWord + '%' OR " +
                "EMPLOYEE.FirstName LIKE '%' + @searchKeyWord + '%' OR " +
                "EMPLOYEE.MiddleName LIKE '%' + @searchKeyWord + '%' OR " +
                "EMPLOYEE.LastName LIKE '%' + @searchKeyWord + '%') " +
                "ORDER BY EMPLOYEE.Emp_Id ASC";

            conn = new SqlConnection();
            conn.ConnectionString = WebConfigurationManager.ConnectionStrings["dbAMS"].ConnectionString;
            comm = new SqlCommand(strSql, conn);
            comm.Parameters.AddWithValue("@searchKeyWord", searchKeyWord);
            dt = new DataTable();
            adp = new SqlDataAdapter(comm);

            conn.Open();
            adp.Fill(dt);
            conn.Close();

            return dt;
        }

        public void DeactivateUser(Guid UserId)
        {
            strSql = "UPDATE Memberships SET IsApproved = 'False' WHERE UserId = @UserId";

            conn = new SqlConnection();
            conn.ConnectionString = WebConfigurationManager.ConnectionStrings["dbAMS"].ConnectionString;

            using (comm = new SqlCommand(strSql, conn))
            {
                conn.Open();
                comm.Parameters.AddWithValue("@UserId", UserId);

                comm.ExecuteNonQuery();
                conn.Close();
            }
            comm.Dispose();
            conn.Dispose();
        }

        public void ActivateUser(Guid UserId)
        {
            strSql = "UPDATE Memberships SET IsApproved = 'True' WHERE UserId = @UserId";

            conn = new SqlConnection();
            conn.ConnectionString = WebConfigurationManager.ConnectionStrings["dbAMS"].ConnectionString;

            using (comm = new SqlCommand(strSql, conn))
            {
                conn.Open();
                comm.Parameters.AddWithValue("@UserId", UserId);

                comm.ExecuteNonQuery();
                conn.Close();
            }
            comm.Dispose();
            conn.Dispose();
        }

        public void ResetPassword(Guid UserId)
        {
            MembershipUser mu = Membership.GetUser(UserId);
            string userName = mu.UserName;

            mu.ChangePassword(mu.ResetPassword(), "pass123");
        }
    }
}
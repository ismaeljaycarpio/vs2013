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
            strSql = "SELECT Memberships.UserId, Memberships.IsApproved, Memberships.IsLockedOut, " +
                "EMPLOYEE.Emp_Id, Roles.RoleName, " +
                "(EMPLOYEE.LastName + ', ' + EMPLOYEE.FirstName + ' ' + EMPLOYEE.MiddleName) AS [FullName] " +
                ", AGENCY.Agency AS [Agency], POSITION.Position AS [Position], DEPARTMENT.Department AS [Department] " +
                "FROM Memberships " +
                "LEFT JOIN UsersInRoles " +
                "ON Memberships.UserId = UsersInRoles.UserId " +
                "LEFT JOIN Roles " +
                "ON Roles.RoleId = UsersInRoles.RoleId " +
                "LEFT JOIN EMPLOYEE " +
                "ON Memberships.UserId = EMPLOYEE.UserId " +
                "LEFT JOIN AGENCY " +
                "ON EMPLOYEE.AgencyId = AGENCY.Id " +
                "LEFT JOIN POSITION " +
                "ON EMPLOYEE.PositionId = POSITION.Id " +
                "LEFT JOIN DEPARTMENT " +
                "ON POSITION.DepartmentId = DEPARTMENT.Id " +
                "WHERE " +
                "(EMPLOYEE.Emp_Id LIKE '%' + @searchKeyWord + '%' OR " +
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

            comm.Dispose();
            adp.Dispose();
            conn.Close();

            return dt;
        }

        public DataTable SelectUserAccounts(Guid UserId)
        {
            strSql = "SELECT Memberships.UserId, Memberships.IsApproved, " +
                "EMPLOYEE.Emp_Id, Roles.RoleName, Roles.RoleId," +
                "(EMPLOYEE.LastName + ', ' + EMPLOYEE.FirstName + ' ' + EMPLOYEE.MiddleName) AS [FullName] " +
                "FROM Memberships, UsersInRoles, Roles,EMPLOYEE " +
                "WHERE " +
                "Memberships.UserId = EMPLOYEE.UserId " +
                "AND Memberships.UserId = UsersInRoles.UserId " +
                "AND UsersInRoles.RoleId = Roles.RoleId " +
                "AND Memberships.UserId = @UserId";

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

            mu.ChangePassword(mu.ResetPassword(), userName);
        }

        public void ChangeRole(Guid UserId, string roleName)
        {
            //get user
            MembershipUser _user = Membership.GetUser(UserId);

            //remove user from all his/her roles
            foreach(string role in Roles.GetRolesForUser(_user.UserName))
            {
                Roles.RemoveUserFromRole(_user.UserName, role);
            }

            //assign user to new role
            if(!Roles.IsUserInRole(_user.UserName, roleName))
            {
                Roles.AddUserToRole(_user.UserName, roleName);
            }
        }

        public void LockUser(Guid UserId)
        {
            strSql = "UPDATE Memberships SET IsLockedOut = 'True' WHERE UserId = @UserId";

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

        public void changeUsername(Guid UserId, string userName)
        {
            strSql = "UPDATE Users SET UserName = @UserName WHERE UserId = @UserId";

            conn = new SqlConnection();
            conn.ConnectionString = WebConfigurationManager.ConnectionStrings["dbAMS"].ConnectionString;

            using (comm = new SqlCommand(strSql, conn))
            {
                conn.Open();
                comm.Parameters.AddWithValue("@UserName", userName);
                comm.Parameters.AddWithValue("@UserId", UserId);
                comm.ExecuteNonQuery();
                conn.Close();
            }
            comm.Dispose();
            conn.Dispose();
        }
    }
}
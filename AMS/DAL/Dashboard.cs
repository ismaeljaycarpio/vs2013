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
    public class Dashboard
    {
        SqlConnection conn;
        SqlCommand comm;
        SqlDataAdapter adp;
        DataTable dt;
        string strSql = "";

        public int CountBday()
        {
            strSql = "SELECT COUNT(Emp_Id) AS BdayCeleb FROM EMPLOYEE WHERE DATEPART(mm,BirthDate) = @MonthNumber";

            conn = new SqlConnection();
            conn.ConnectionString = WebConfigurationManager.ConnectionStrings["dbAMS"].ConnectionString;
            comm = new SqlCommand(strSql, conn);
            int monthNumber = DateTime.Now.Month;
            comm.Parameters.AddWithValue("@MonthNumber", monthNumber);
            conn.Open();
            int _count = (int)comm.ExecuteScalar();
            conn.Close();

            return _count;
        }
        public DataTable DisplayBirthDayCeleb(string searchkeyWord, string MonthNumber)
        {
            strSql = "SELECT EMPLOYEE.UserId, EMPLOYEE.Emp_Id, " +
                "(EMPLOYEE.LastName + ', ' + EMPLOYEE.FirstName + ' ' + EMPLOYEE.MiddleName) AS FullName, " +
                "EMPLOYEE.BirthDate, " +
                "POSITION.Position AS [POSITION], DEPARTMENT.Department AS [DEPARTMENT] " +
                "FROM Memberships, EMPLOYEE, POSITION, DEPARTMENT, UsersInRoles, Roles WHERE " +
                "Memberships.UserId = EMPLOYEE.UserId AND " +
                "EMPLOYEE.PositionId = POSITION.Id AND " +
                "POSITION.DepartmentId = DEPARTMENT.Id AND " +
                "EMPLOYEE.UserId = UsersInRoles.UserId AND " +
                "UsersInRoles.RoleId = Roles.RoleId AND " +
                "Roles.RoleName != 'Admin' AND " +
                "(EMPLOYEE.Emp_Id LIKE '%' + @searchKeyWord + '%' " +
                "OR EMPLOYEE.FirstName LIKE '%' + @searchKeyWord + '%' " +
                "OR EMPLOYEE.MiddleName LIKE '%' + @searchKeyWord + '%' " +
                "OR EMPLOYEE.LastName LIKE '%' + @searchKeyWord + '%' " +
                "OR POSITION.Position LIKE '%' + @searchKeyWord + '%' " +
                "OR DEPARTMENT.Department LIKE '%' + @searchKeyWord + '%') AND " +
                "DATEPART(mm,EMPLOYEE.BirthDate) = @MonthNumber AND " +
                "EMPLOYEE.AccountStatusId = 1 " +
                "ORDER BY Employee.Emp_Id ASC";

            conn = new SqlConnection();
            conn.ConnectionString = WebConfigurationManager.ConnectionStrings["dbAMS"].ConnectionString;
            comm = new SqlCommand(strSql, conn);
            comm.Parameters.AddWithValue("@MonthNumber", MonthNumber);
            comm.Parameters.AddWithValue("@searchKeyWord", searchkeyWord);
            dt = new DataTable();
            adp = new SqlDataAdapter(comm);

            conn.Open();
            adp.Fill(dt);
            conn.Close();

            return dt;
        }

        public DataTable DisplayMasterList()
        {
            strSql = "SELECT ACCOUNT_STATUS.Id, ACCOUNT_STATUS.AccountStatus," +
                "COUNT(EMPLOYEE.UserId) AS [MasterListCount]" +
                "FROM ACCOUNT_STATUS, EMPLOYEE " +
                "WHERE ACCOUNT_STATUS.Id = EMPLOYEE.AccountStatusId " +
                "GROUP BY ACCOUNT_STATUS.Id, " +
                "ACCOUNT_STATUS.AccountStatus";

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

        public DataTable DisplayMasterList(string searchkeyWord, string accountStatusId)
        {
            strSql = "SELECT EMPLOYEE.UserId, EMPLOYEE.Emp_Id, " +
                "(EMPLOYEE.LastName + ', ' + EMPLOYEE.FirstName + ' ' + EMPLOYEE.MiddleName) AS FullName, " +
                "EMPLOYEE.BirthDate, " +
                "POSITION.Position AS [POSITION], DEPARTMENT.Department AS [DEPARTMENT], " +
                "ACCOUNT_STATUS.AccountStatus " +
                "FROM Memberships, EMPLOYEE, POSITION, DEPARTMENT, UsersInRoles, Roles, ACCOUNT_STATUS WHERE " +
                "Memberships.UserId = EMPLOYEE.UserId AND " +
                "EMPLOYEE.PositionId = POSITION.Id AND " +
                "POSITION.DepartmentId = DEPARTMENT.Id AND " +
                "EMPLOYEE.UserId = UsersInRoles.UserId AND " +
                "UsersInRoles.RoleId = Roles.RoleId AND " +
                "Roles.RoleName != 'Admin' AND " +
                "ACCOUNT_STATUS.Id = EMPLOYEE.AccountStatusId AND " +
                "(EMPLOYEE.Emp_Id LIKE '%' + @searchKeyWord + '%' " +
                "OR EMPLOYEE.FirstName LIKE '%' + @searchKeyWord + '%' " +
                "OR EMPLOYEE.MiddleName LIKE '%' + @searchKeyWord + '%' " +
                "OR EMPLOYEE.LastName LIKE '%' + @searchKeyWord + '%' " +
                "OR POSITION.Position LIKE '%' + @searchKeyWord + '%' " +
                "OR DEPARTMENT.Department LIKE '%' + @searchKeyWord + '%') AND " +
                "EMPLOYEE.AccountStatusId = @AccountStatusId " +
                "ORDER BY Employee.Emp_Id ASC";

            conn = new SqlConnection();
            conn.ConnectionString = WebConfigurationManager.ConnectionStrings["dbAMS"].ConnectionString;
            comm = new SqlCommand(strSql, conn);
            comm.Parameters.AddWithValue("@AccountStatusId", accountStatusId);
            comm.Parameters.AddWithValue("@searchKeyWord", searchkeyWord);
            dt = new DataTable();
            adp = new SqlDataAdapter(comm);

            conn.Open();
            adp.Fill(dt);
            conn.Close();

            return dt;
        }

        public int CountNewlyHired()
        {
            strSql = "SELECT COUNT(Emp_Id) AS NewlyHired FROM EMPLOYEE WHERE DATEPART(mm,JoinDate) = @MonthNumber";

            conn = new SqlConnection();
            conn.ConnectionString = WebConfigurationManager.ConnectionStrings["dbAMS"].ConnectionString;
            comm = new SqlCommand(strSql, conn);
            int monthNumber = DateTime.Now.Month;
            comm.Parameters.AddWithValue("@MonthNumber", monthNumber);
            conn.Open();
            int _count = (int)comm.ExecuteScalar();
            conn.Close();

            return _count;
        }

        public DataTable DisplayNewlyHired(string searchkeyWord, string startdate, string enddate)
        {
            if(startdate == String.Empty)
            {
                strSql = "SELECT EMPLOYEE.UserId, EMPLOYEE.Emp_Id, " +
                                "(EMPLOYEE.LastName + ', ' + EMPLOYEE.FirstName + ' ' + EMPLOYEE.MiddleName) AS FullName, " +
                                "EMPLOYEE.JoinDate, " +
                                "POSITION.Position AS [POSITION], DEPARTMENT.Department AS [DEPARTMENT], " +
                                "FROM Memberships, EMPLOYEE, POSITION, DEPARTMENT, UsersInRoles, Roles WHERE " +
                                "Memberships.UserId = EMPLOYEE.UserId AND " +
                                "EMPLOYEE.PositionId = POSITION.Id AND " +
                                "POSITION.DepartmentId = DEPARTMENT.Id AND " +
                                "EMPLOYEE.UserId = UsersInRoles.UserId AND " +
                                "UsersInRoles.RoleId = Roles.RoleId AND " +
                                "Roles.RoleName != 'Admin' AND " +
                                "(EMPLOYEE.Emp_Id LIKE '%' + @searchKeyWord + '%' " +
                                "OR EMPLOYEE.FirstName LIKE '%' + @searchKeyWord + '%' " +
                                "OR EMPLOYEE.MiddleName LIKE '%' + @searchKeyWord + '%' " +
                                "OR EMPLOYEE.LastName LIKE '%' + @searchKeyWord + '%' " +
                                "OR POSITION.Position LIKE '%' + @searchKeyWord + '%' " +
                                "OR DEPARTMENT.Department LIKE '%' + @searchKeyWord + '%') " +
                                "ORDER BY Employee.Emp_Id ASC";
            }
            else if(startdate != String.Empty && enddate == String.Empty)
            strSql = "SELECT EMPLOYEE.UserId, EMPLOYEE.Emp_Id, " +
                "(EMPLOYEE.LastName + ', ' + EMPLOYEE.FirstName + ' ' + EMPLOYEE.MiddleName) AS FullName, " +
                "EMPLOYEE.JoinDate, " +
                "POSITION.Position AS [POSITION], DEPARTMENT.Department AS [DEPARTMENT], " +
                "FROM Memberships, EMPLOYEE, POSITION, DEPARTMENT, UsersInRoles, Roles WHERE " +
                "Memberships.UserId = EMPLOYEE.UserId AND " +
                "EMPLOYEE.PositionId = POSITION.Id AND " +
                "POSITION.DepartmentId = DEPARTMENT.Id AND " +
                "EMPLOYEE.UserId = UsersInRoles.UserId AND " +
                "UsersInRoles.RoleId = Roles.RoleId AND " +
                "Roles.RoleName != 'Admin' AND " +
                "(EMPLOYEE.Emp_Id LIKE '%' + @searchKeyWord + '%' " +
                "OR EMPLOYEE.FirstName LIKE '%' + @searchKeyWord + '%' " +
                "OR EMPLOYEE.MiddleName LIKE '%' + @searchKeyWord + '%' " +
                "OR EMPLOYEE.LastName LIKE '%' + @searchKeyWord + '%' " +
                "OR POSITION.Position LIKE '%' + @searchKeyWord + '%' " +
                "OR DEPARTMENT.Department LIKE '%' + @searchKeyWord + '%') AND " +
                "EMPLOYEE.JOINDATE = @StartDate " +
                "ORDER BY Employee.Emp_Id ASC";

            conn = new SqlConnection();
            conn.ConnectionString = WebConfigurationManager.ConnectionStrings["dbAMS"].ConnectionString;
            comm = new SqlCommand(strSql, conn);
            comm.Parameters.AddWithValue("@AccountStatusId", accountStatusId);
            comm.Parameters.AddWithValue("@searchKeyWord", searchkeyWord);
            dt = new DataTable();
            adp = new SqlDataAdapter(comm);

            conn.Open();
            adp.Fill(dt);
            conn.Close();

            return dt;
        }
    }
}
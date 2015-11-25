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
        public DataTable DisplayBirthDayCeleb(string MonthNumber)
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
                "DATEPART(mm,EMPLOYEE.BirthDate) = @MonthNumber AND " +
                "Memberships.IsApproved = 'True' " +
                "ORDER BY Employee.Emp_Id ASC";

            conn = new SqlConnection();
            conn.ConnectionString = WebConfigurationManager.ConnectionStrings["dbAMS"].ConnectionString;
            comm = new SqlCommand(strSql, conn);
            comm.Parameters.AddWithValue("@MonthNumber", MonthNumber);
            dt = new DataTable();
            adp = new SqlDataAdapter(comm);

            conn.Open();
            adp.Fill(dt);
            conn.Close();

            return dt;
        }
    }
}
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

        public int CountBday(string deptId)
        {
            strSql = "SELECT COUNT(EMPLOYEE.Emp_Id) AS BdayCeleb FROM " +
                "EMPLOYEE INNER JOIN POSITION ON EMPLOYEE.PositionId = POSITION.Id " +
                "INNER JOIN DEPARTMENT ON POSITION.DepartmentId = DEPARTMENT.Id " +
                "WHERE DEPARTMENT.Id = @DepartmentId " +
                "AND DATEPART(mm,BirthDate) = @MonthNumber";

            conn = new SqlConnection();
            conn.ConnectionString = WebConfigurationManager.ConnectionStrings["dbAMS"].ConnectionString;
            comm = new SqlCommand(strSql, conn);
            int monthNumber = DateTime.Now.Month;
            comm.Parameters.AddWithValue("@MonthNumber", monthNumber);
            comm.Parameters.AddWithValue("@DepartmentId", deptId);
            conn.Open();
            int _count = (int)comm.ExecuteScalar();
            conn.Close();

            return _count;
        }

        //active accounts only
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

        public DataTable DisplayBirthDayCeleb(string searchkeyWord, string MonthNumber, string deptId)
        {
            strSql = "SELECT EMPLOYEE.UserId, EMPLOYEE.Emp_Id, " +
                "(EMPLOYEE.LastName + ', ' + EMPLOYEE.FirstName + ' ' + EMPLOYEE.MiddleName) AS FullName, " +
                "EMPLOYEE.BirthDate, " +
                "POSITION.Position AS [POSITION], DEPARTMENT.Department AS [DEPARTMENT] " +
                "FROM Memberships, EMPLOYEE, POSITION, DEPARTMENT, UsersInRoles, Roles " +
                "WHERE " +
                "Memberships.UserId = EMPLOYEE.UserId AND " +
                "EMPLOYEE.PositionId = POSITION.Id AND " +
                "POSITION.DepartmentId = DEPARTMENT.Id AND " +
                "DEPARTMENT.Id = @DepartmentId AND " +
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
            comm.Parameters.AddWithValue("@DepartmentId", deptId);
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
                "FROM ACCOUNT_STATUS LEFT JOIN EMPLOYEE " +
                "ON ACCOUNT_STATUS.Id = EMPLOYEE.AccountStatusId " +
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

        public DataTable DisplayMasterList(string deptId)
        {
            strSql = "SELECT ACCOUNT_STATUS.Id, ACCOUNT_STATUS.AccountStatus, " +
                "COUNT(EMPLOYEE.UserId) AS [MasterListCount] " +
                "FROM ACCOUNT_STATUS " +
                "LEFT JOIN " +
                "( " +
                    "SELECT EMPLOYEE.UserId,EMPLOYEE.AccountStatusId " +
                    "FROM EMPLOYEE " +
                    "INNER JOIN POSITION " +
                    "ON EMPLOYEE.PositionId = POSITION.Id " +
                    "INNER JOIN DEPARTMENT " +
                    "ON POSITION.DepartmentId = DEPARTMENT.Id " +
                    "WHERE DEPARTMENT.Id = @DepartmentId " +
                ") " +
                "EMPLOYEE " +
                "ON EMPLOYEE.AccountStatusId = ACCOUNT_STATUS.Id " +
                "GROUP BY ACCOUNT_STATUS.Id, " +
                "ACCOUNT_STATUS.AccountStatus";

            conn = new SqlConnection();
            conn.ConnectionString = WebConfigurationManager.ConnectionStrings["dbAMS"].ConnectionString;
            comm = new SqlCommand(strSql, conn);
            comm.Parameters.AddWithValue("@DepartmentId", deptId);
            dt = new DataTable();
            adp = new SqlDataAdapter(comm);

            conn.Open();
            adp.Fill(dt);
            conn.Close();

            return dt;
        }

        public DataTable DisplayMasterList(string searchkeyWord, string accountStatusId)
        {
            conn = new SqlConnection();
            conn.ConnectionString = WebConfigurationManager.ConnectionStrings["dbAMS"].ConnectionString;
            comm = new SqlCommand();
            comm.Connection = conn;

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

            comm.Parameters.AddWithValue("@AccountStatusId", accountStatusId);
            comm.Parameters.AddWithValue("@searchKeyWord", searchkeyWord);
            comm.CommandText = strSql;
            dt = new DataTable();
            adp = new SqlDataAdapter(comm);

            conn.Open();
            adp.Fill(dt);
            conn.Close();

            return dt;
        }

        public DataTable DisplayMasterList(string searchkeyWord, string accountStatusId, string deptId)
        {
            conn = new SqlConnection();
            conn.ConnectionString = WebConfigurationManager.ConnectionStrings["dbAMS"].ConnectionString;
            comm = new SqlCommand();
            comm.Connection = conn;

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
                "DEPARTMENT.Id = @DepartmentId AND " +
                "ACCOUNT_STATUS.Id = EMPLOYEE.AccountStatusId AND " +
                "(EMPLOYEE.Emp_Id LIKE '%' + @searchKeyWord + '%' " +
                "OR EMPLOYEE.FirstName LIKE '%' + @searchKeyWord + '%' " +
                "OR EMPLOYEE.MiddleName LIKE '%' + @searchKeyWord + '%' " +
                "OR EMPLOYEE.LastName LIKE '%' + @searchKeyWord + '%' " +
                "OR POSITION.Position LIKE '%' + @searchKeyWord + '%' " +
                "OR DEPARTMENT.Department LIKE '%' + @searchKeyWord + '%') AND " +
                "EMPLOYEE.AccountStatusId = @AccountStatusId " +
                "ORDER BY Employee.Emp_Id ASC";
            comm.Parameters.AddWithValue("@AccountStatusId", accountStatusId);

            comm.Parameters.AddWithValue("@AccountStatusId", accountStatusId);
            comm.Parameters.AddWithValue("@DepartmentId", deptId);
            comm.Parameters.AddWithValue("@searchKeyWord", searchkeyWord);
            comm.CommandText = strSql;
            dt = new DataTable();
            adp = new SqlDataAdapter(comm);

            conn.Open();
            adp.Fill(dt);
            conn.Close();

            return dt;
        }

        //expiring contracts in 2 weeks
        public int CountExpiringContracts()
        {
            strSql = "SELECT COUNT(EMPLOYEE.Emp_Id) " +
                    "FROM EMPLOYEE " +
                    "where ((DATEDIFF(day,GETDATE(),Contract_ED) <= 14) AND (DATEDIFF(day,GETDATE(),Contract_ED) >= 0))" +
                    "AND EMPLOYEE.AccountStatusId = 1";

            conn = new SqlConnection();
            conn.ConnectionString = WebConfigurationManager.ConnectionStrings["dbAMS"].ConnectionString;
            comm = new SqlCommand(strSql, conn);

            conn.Open();
            int _count = (int)comm.ExecuteScalar();
            conn.Close();

            return _count;
        }

        public int CountExpiringContracts(string deptId)
        {
            strSql = "SELECT COUNT(EMPLOYEE.Emp_Id) " +
                    "FROM EMPLOYEE, POSITION, DEPARTMENT " +
                    "WHERE " +
                    "EMPLOYEE.PositionId = POSITION.Id " +
                    "AND POSITION.DepartmentId = DEPARTMENT.Id AND " +
                    "((DATEDIFF(day,GETDATE(),Contract_ED) <= 14) AND (DATEDIFF(day,GETDATE(),Contract_ED) >= 0)) " +
                    "AND DEPARTMENT.Id = @DepartmentId " +
                    "AND EMPLOYEE.AccountStatusId = 1";

            conn = new SqlConnection();
            conn.ConnectionString = WebConfigurationManager.ConnectionStrings["dbAMS"].ConnectionString;
            comm = new SqlCommand(strSql, conn);
            comm.Parameters.AddWithValue("@DepartmentId", deptId);
            conn.Open();
            int _count = (int)comm.ExecuteScalar();
            conn.Close();

            return _count;
        }

        //employee who can evaluate
        public int CountCanEvaluate()
        {
            strSql = "SELECT COUNT(EMPLOYEE.Emp_Id) " +
                    "FROM EMPLOYEE " +
                    "where ((DATEDIFF(day,GETDATE(),Contract_ED) <= 14) AND (DATEDIFF(day,GETDATE(),Contract_ED) >= 0))" +
                    "AND EMPLOYEE.AccountStatusId = 1";

            conn = new SqlConnection();
            conn.ConnectionString = WebConfigurationManager.ConnectionStrings["dbAMS"].ConnectionString;
            comm = new SqlCommand(strSql, conn);
            int monthNumber = DateTime.Now.Month;
            conn.Open();
            int _count = (int)comm.ExecuteScalar();
            conn.Close();

            return _count;
        }

        
        public DataTable DisplayExpiringContract()
        {
            conn = new SqlConnection();
            conn.ConnectionString = WebConfigurationManager.ConnectionStrings["dbAMS"].ConnectionString;
            comm = new SqlCommand();
            comm.Connection = conn;

                //get expiring contracts w/in 2 weeks
                strSql = "SELECT EMPLOYEE.UserId, EMPLOYEE.Emp_Id, " +
                    "(EMPLOYEE.LastName + ', ' + EMPLOYEE.FirstName + ' ' + EMPLOYEE.MiddleName) AS FullName, " +
                    "EMPLOYEE.BirthDate, " +
                    "POSITION.Position AS [POSITION], DEPARTMENT.Department AS [DEPARTMENT], " +
                    "EMPLOYEE.Contract_SD, EMPLOYEE.CONTRACT_ED " +
                    "FROM Memberships, EMPLOYEE, POSITION, DEPARTMENT, UsersInRoles, Roles " +
                    "WHERE " +
                    "Memberships.UserId = EMPLOYEE.UserId AND " +
                    "EMPLOYEE.PositionId = POSITION.Id AND " +
                    "POSITION.DepartmentId = DEPARTMENT.Id AND " +
                    "EMPLOYEE.UserId = UsersInRoles.UserId AND " +
                    "UsersInRoles.RoleId = Roles.RoleId AND " +
                    "Roles.RoleName != 'Admin' AND " +
                    "((DATEDIFF(day,GETDATE(),Contract_ED) <= 14) AND (DATEDIFF(day,GETDATE(),Contract_ED) >= 0)) " +
                    "AND EMPLOYEE.AccountStatusId = 1 " +
                    "ORDER BY Employee.Emp_Id ASC";

            comm.CommandText = strSql;
            dt = new DataTable();
            adp = new SqlDataAdapter(comm);

            conn.Open();
            adp.Fill(dt);
            conn.Close();

            return dt;
        }

        public DataTable DisplayExpiringContract(string deptId)
        {
            conn = new SqlConnection();
            conn.ConnectionString = WebConfigurationManager.ConnectionStrings["dbAMS"].ConnectionString;
            comm = new SqlCommand();
            comm.Connection = conn;

            //get expiring contracts w/in 2 weeks
            strSql = "SELECT EMPLOYEE.UserId, EMPLOYEE.Emp_Id, " +
                "(EMPLOYEE.LastName + ', ' + EMPLOYEE.FirstName + ' ' + EMPLOYEE.MiddleName) AS FullName, " +
                "EMPLOYEE.BirthDate, " +
                "POSITION.Position AS [POSITION], DEPARTMENT.Department AS [DEPARTMENT], " +
                "EMPLOYEE.Contract_SD, EMPLOYEE.CONTRACT_ED " +
                "FROM Memberships, EMPLOYEE, POSITION, DEPARTMENT, UsersInRoles, Roles " +
                "WHERE " +
                "Memberships.UserId = EMPLOYEE.UserId AND " +
                "EMPLOYEE.PositionId = POSITION.Id AND " +
                "POSITION.DepartmentId = DEPARTMENT.Id AND " +
                "EMPLOYEE.UserId = UsersInRoles.UserId AND " +
                "UsersInRoles.RoleId = Roles.RoleId AND " +
                "Roles.RoleName != 'Admin' AND " +
                "DEPARTMENT.Id = @DepartmentId AND " +
                "((DATEDIFF(day,GETDATE(),Contract_ED) <= 14) AND (DATEDIFF(day,GETDATE(),Contract_ED) >= 0)) " +
                "AND EMPLOYEE.AccountStatusId = 1 " +
                "ORDER BY Employee.Emp_Id ASC";

            comm.CommandText = strSql;
            comm.Parameters.AddWithValue("@DepartmentId", deptId);
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

        public int CountNewlyHired(string deptId)
        {
            strSql = "SELECT COUNT(Emp_Id) AS NewlyHired " +
                "FROM EMPLOYEE " +
                "INNER JOIN POSITION " +
                "ON EMPLOYEE.PositionId = POSITION.Id " +
                "INNER JOIN DEPARTMENT " +
                "ON POSITION.DepartmentId = DEPARTMENT.Id " +
                "WHERE DEPARTMENT.Id = @DepartmentId AND " +
                "DATEPART(mm,JoinDate) = @MonthNumber";

            conn = new SqlConnection();
            conn.ConnectionString = WebConfigurationManager.ConnectionStrings["dbAMS"].ConnectionString;
            comm = new SqlCommand(strSql, conn);
            int monthNumber = DateTime.Now.Month;
            comm.Parameters.AddWithValue("@MonthNumber", monthNumber);
            comm.Parameters.AddWithValue("@DepartmentId", deptId);
            conn.Open();
            int _count = (int)comm.ExecuteScalar();
            conn.Close();

            return _count;
        }

        public DataTable DisplayNewlyHired(string searchkeyWord, string startdate, string enddate)
        {
            conn = new SqlConnection();
            conn.ConnectionString = WebConfigurationManager.ConnectionStrings["dbAMS"].ConnectionString;
            comm = new SqlCommand();
            comm.Connection = conn;
            if(startdate == String.Empty)
            {
                strSql = "SELECT EMPLOYEE.UserId, EMPLOYEE.Emp_Id, " +
                                "(EMPLOYEE.LastName + ', ' + EMPLOYEE.FirstName + ' ' + EMPLOYEE.MiddleName) AS FullName, " +
                                "EMPLOYEE.JoinDate, " +
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
                                "OR DEPARTMENT.Department LIKE '%' + @searchKeyWord + '%') " +
                                "ORDER BY Employee.Emp_Id ASC";
            }
            else if(startdate != String.Empty && enddate == String.Empty)
            {
                strSql = "SELECT EMPLOYEE.UserId, EMPLOYEE.Emp_Id, " +
                                "(EMPLOYEE.LastName + ', ' + EMPLOYEE.FirstName + ' ' + EMPLOYEE.MiddleName) AS FullName, " +
                                "EMPLOYEE.JoinDate, " +
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
                                "OR DEPARTMENT.Department LIKE '%' + @searchKeyWord + '%') " +
                                "AND EMPLOYEE.JoinDate = @StartDate " +
                                "ORDER BY Employee.Emp_Id ASC";

                comm.Parameters.AddWithValue("@StartDate", startdate);
            }
            else if(startdate != String.Empty && enddate != String.Empty)
            {
                strSql = "SELECT EMPLOYEE.UserId, EMPLOYEE.Emp_Id, " +
                                "(EMPLOYEE.LastName + ', ' + EMPLOYEE.FirstName + ' ' + EMPLOYEE.MiddleName) AS FullName, " +
                                "EMPLOYEE.JoinDate, " +
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
                                "OR DEPARTMENT.Department LIKE '%' + @searchKeyWord + '%') " +
                                "AND EMPLOYEE.JoinDate BETWEEN @StartDate AND @EndDate " +
                                "ORDER BY Employee.Emp_Id ASC";
                comm.Parameters.AddWithValue("@StartDate", startdate);
                comm.Parameters.AddWithValue("@EndDate", enddate);
            }
           
            comm.Parameters.AddWithValue("@searchKeyWord", searchkeyWord);
            comm.CommandText = strSql;
            dt = new DataTable();
            adp = new SqlDataAdapter(comm);

            conn.Open();
            adp.Fill(dt);
            conn.Close();

            return dt;
        }

        public DataTable DisplayDurationOfContract(string searchkeyWord, string startdate, string enddate)
        {
            conn = new SqlConnection();
            conn.ConnectionString = WebConfigurationManager.ConnectionStrings["dbAMS"].ConnectionString;
            comm = new SqlCommand();
            comm.Connection = conn;
            if (startdate == String.Empty)
            {
                strSql = "SELECT EMPLOYEE.UserId, EMPLOYEE.Emp_Id, " +
                                "(EMPLOYEE.LastName + ', ' + EMPLOYEE.FirstName + ' ' + EMPLOYEE.MiddleName) AS FullName, " +
                                "EMPLOYEE.Contract_SD, EMPLOYEE.Contract_ED," +
                                "DATEDIFF(MM,Contract_SD,Contract_ED)/12 AS [Years]," +
                                "DATEDIFF(MM,Contract_SD,Contract_ED)%12 AS [Months]," +
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
                                "OR DEPARTMENT.Department LIKE '%' + @searchKeyWord + '%') " +
                                "ORDER BY Employee.Emp_Id ASC";
            }
            else if (startdate != String.Empty && enddate == String.Empty)
            {
                strSql = "SELECT EMPLOYEE.UserId, EMPLOYEE.Emp_Id, " +
                                "(EMPLOYEE.LastName + ', ' + EMPLOYEE.FirstName + ' ' + EMPLOYEE.MiddleName) AS FullName, " +
                                "EMPLOYEE.Contract_SD, EMPLOYEE.Contract_ED," +
                                "DATEDIFF(MM,Contract_SD,Contract_ED)/12 AS [Years]," +
                                "DATEDIFF(MM,Contract_SD,Contract_ED)%12 AS [Months]," +
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
                                "OR DEPARTMENT.Department LIKE '%' + @searchKeyWord + '%') " +
                                "AND EMPLOYEE.Contract_SD = @StartDate " +
                                "ORDER BY Employee.Emp_Id ASC";

                comm.Parameters.AddWithValue("@StartDate", startdate);
            }
            else if (startdate != String.Empty && enddate != String.Empty)
            {
                strSql = "SELECT EMPLOYEE.UserId, EMPLOYEE.Emp_Id, " +
                                "(EMPLOYEE.LastName + ', ' + EMPLOYEE.FirstName + ' ' + EMPLOYEE.MiddleName) AS FullName, " +
                                "EMPLOYEE.Contract_SD, EMPLOYEE.Contract_ED," +
                                "DATEDIFF(MM,Contract_SD,Contract_ED)/12 AS [Years]," +
                                "DATEDIFF(MM,Contract_SD,Contract_ED)%12 AS [Months]," +
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
                                "OR DEPARTMENT.Department LIKE '%' + @searchKeyWord + '%') " +
                                "AND EMPLOYEE.Contract_SD BETWEEN @StartDate AND @EndDate " +
                                "ORDER BY Employee.Emp_Id ASC";
                comm.Parameters.AddWithValue("@StartDate", startdate);
                comm.Parameters.AddWithValue("@EndDate", enddate);
            }

            comm.Parameters.AddWithValue("@searchKeyWord", searchkeyWord);
            comm.CommandText = strSql;
            dt = new DataTable();
            adp = new SqlDataAdapter(comm);

            conn.Open();
            adp.Fill(dt);
            conn.Close();

            return dt;
        }

        //employee age -> active only
        public DataTable DisplayAge(string searchkeyWord)
        {
            conn = new SqlConnection();
            conn.ConnectionString = WebConfigurationManager.ConnectionStrings["dbAMS"].ConnectionString;
            comm = new SqlCommand();
            comm.Connection = conn;

            strSql = "SELECT EMPLOYEE.UserId, EMPLOYEE.Emp_Id, " +
                "(EMPLOYEE.LastName + ', ' + EMPLOYEE.FirstName + ' ' + EMPLOYEE.MiddleName) AS FullName, " +
                "EMPLOYEE.BirthDate, " +
                "POSITION.Position AS [POSITION], DEPARTMENT.Department AS [DEPARTMENT], " +
                "GETDATE() AS [TODAY] " +
	                ",DATEDIFF (YY,BirthDate,GETDATE()) - " +
	                "CASE " +
		                "WHEN DATEADD(YY,DATEDIFF(YY,BirthDate,GETDATE()),BirthDate) " +
		                "> GETDATE() THEN 1 " +
		                "ELSE 0 " +
	                "END AS [Age] " +
                "FROM Memberships, EMPLOYEE, POSITION, DEPARTMENT, UsersInRoles, Roles " +
                "WHERE " +
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

            comm.Parameters.AddWithValue("@searchKeyWord", searchkeyWord);
            comm.CommandText = strSql;
            dt = new DataTable();
            adp = new SqlDataAdapter(comm);

            conn.Open();
            adp.Fill(dt);
            conn.Close();

            return dt;
        }
    }
}
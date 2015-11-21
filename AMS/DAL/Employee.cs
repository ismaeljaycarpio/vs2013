using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using System.Data.SqlClient;
using System.Data;
using System.Text;

namespace AMS.DAL
{
    public class Employee
    {
        SqlConnection conn;
        SqlCommand comm;
        SqlDataAdapter adp;
        DataTable dt;
        string strSql = "";

        #region NEW IMPLEMENTATION
        //display all employee->user by HR manager, HR staff, GM
        public DataTable DisplayEmployee(string strSearch)
        {
            strSql = "SELECT EMPLOYEE.UserId, EMPLOYEE.Emp_Id, " +
                "(EMPLOYEE.LastName + ', ' + EMPLOYEE.FirstName, + ' ' + EMPLOYEE.MiddleName) AS FullName, " +
                "POSITION.Position AS [POSITION], DEPARTMENT.Department AS [DEPARTMENT] " +
                "FROM EMPLOYEE, POSITION, DEPARTMENT WHERE " +
                "EMPLOYEE.PositionId = POSITION.Id AND " +
                "POSITION.DepartmentId = DEPARTMENT.Id AND " +
                "(EMPLOYEE.Emp_Id LIKE '%' + @searchKeyWord + '%' " +
                "OR EMPLOYEE.FirstName LIKE '%' + @searchKeyWord + '%' " +
                "OR EMPLOYEE.MiddleName LIKE '%' + @searchKeyWord + '%' " +
                "OR EMPLOYEE.LastName LIKE '%' + @searchKeyWord + '%' " +
                "OR POSITION.Position LIKE '%' + @searchKeyWord + '%' " +
                "OR DEPARTMENT.Department LIKE '%' + @searchKeyWord + '%'65 ) ";

            conn = new SqlConnection();
            conn.ConnectionString = WebConfigurationManager.ConnectionStrings["dbAMS"].ConnectionString;
            comm = new SqlCommand(strSql, conn);
            comm.Parameters.AddWithValue("@searchKeyWord", strSearch);
            dt = new DataTable();
            adp = new SqlDataAdapter(comm);

            conn.Open();
            adp.Fill(dt);
            conn.Close();

            return dt;
        }

        //display supervisor and staff
        public DataTable DisplayEmployeeOfManager(string strSearch, string deptId)
        {
            //strSql = "SELECT JOB.Emp_ID, PERSONAL.UserId, PERSONAL.FName, PERSONAL.MName," +
            //    "PERSONAL.LName, POSITION.Position AS [POSITION], DEPARTMENT.Department AS [DEPARTMENT] " +
            //    "FROM PERSONAL, JOB, POSITION, DEPARTMENT, UsersInRoles, Roles  WHERE " +
            //    "PERSONAL.UserId = JOB.UserId AND " +
            //    "JOB.PositionId = POSITION.Id AND " +
            //    "POSITION.DepartmentId = DEPARTMENT.Id AND " +
            //    "DEPARTMENT.Id = @DepartmentId AND " +
            //    "PERSONAL.UserId = UsersInRoles.UserId AND " +
            //    "UsersInRoles.RoleId = Roles.RoleId AND " +
            //    "(Roles.RoleName = 'Supervisor' OR Roles.RoleName = 'Staff') AND " +
            //    "(JOB.Emp_ID LIKE '%' + @searchKeyWord + '%' " +
            //    "OR PERSONAL.FName LIKE '%' + @searchKeyWord + '%' " +
            //    "OR PERSONAL.MName LIKE '%' + @searchKeyWord + '%' " +
            //    "OR PERSONAL.LName LIKE '%' + @searchKeyWord + '%' " +
            //    "OR POSITION.Position LIKE '%' + @searchKeyWord + '%' " +
            //    "OR DEPARTMENT.Department LIKE '%' + @searchKeyWord + '%' ) ";

            strSql = "SELECT EMPLOYEE.UserId, EMPLOYEE.Emp_Id, " +
                "(EMPLOYEE.LastName + ', ' + EMPLOYEE.FirstName, + ' ' + EMPLOYEE.MiddleName) AS FullName, " +
                "POSITION.Position AS [POSITION], DEPARTMENT.Department AS [DEPARTMENT] " +
                "FROM EMPLOYEE, POSITION, DEPARTMENT, UsersInRoles, Roles WHERE " +
                "EMPLOYEE.PositionId = POSITION.Id AND " +
                "POSITION.DepartmentId = DEPARTMENT.Id AND " +
                "DEPARTMENT.Id = @DepartmentId AND " +
                "EMPLOYEE.UserId = UsersInRoles.UserId AND " +
                "UsersInRoles.RoleId = Roles.RoleId AND " +
                "(Roles.RoleName = 'Supervisor' OR Roles.RoleName = 'Staff') AND " +
                "(EMPLOYEE.Emp_Id LIKE '%' + @searchKeyWord + '%' " +
                "OR EMPLOYEE.FirstName LIKE '%' + @searchKeyWord + '%' " +
                "OR EMPLOYEE.MiddleName LIKE '%' + @searchKeyWord + '%' " +
                "OR EMPLOYEE.LastName LIKE '%' + @searchKeyWord + '%' " +
                "OR POSITION.Position LIKE '%' + @searchKeyWord + '%' " +
                "OR DEPARTMENT.Department LIKE '%' + @searchKeyWord + '%' ) ";

            conn = new SqlConnection();
            conn.ConnectionString = WebConfigurationManager.ConnectionStrings["dbAMS"].ConnectionString;
            comm = new SqlCommand(strSql, conn);
            comm.Parameters.AddWithValue("@searchKeyWord", strSearch);
            comm.Parameters.AddWithValue("@DepartmentId", deptId);
            dt = new DataTable();
            adp = new SqlDataAdapter(comm);

            conn.Open();
            adp.Fill(dt);
            conn.Close();

            return dt;
        }

        //display staff
        public DataTable DisplayEmployeeOfSupervisor(string strSearch, string deptId)
        {
            //strSql = "SELECT JOB.Emp_ID, PERSONAL.UserId, PERSONAL.FName, PERSONAL.MName," +
            //    "PERSONAL.LName, POSITION.Position AS [POSITION], DEPARTMENT.Department AS [DEPARTMENT] " +
            //    "FROM PERSONAL, JOB, POSITION, DEPARTMENT, UsersInRoles, Roles  WHERE " +
            //    "PERSONAL.UserId = JOB.UserId AND " +
            //    "JOB.PositionId = POSITION.Id AND " +
            //    "POSITION.DepartmentId = DEPARTMENT.Id AND " +
            //    "DEPARTMENT.Id = @DepartmentId AND " +
            //    "PERSONAL.UserId = UsersInRoles.UserId AND " +
            //    "UsersInRoles.RoleId = Roles.RoleId AND " +
            //    "(Roles.RoleName = 'Staff') AND " +
            //    "(JOB.Emp_ID LIKE '%' + @searchKeyWord + '%' " +
            //    "OR PERSONAL.FName LIKE '%' + @searchKeyWord + '%' " +
            //    "OR PERSONAL.MName LIKE '%' + @searchKeyWord + '%' " +
            //    "OR PERSONAL.LName LIKE '%' + @searchKeyWord + '%' " +
            //    "OR POSITION.Position LIKE '%' + @searchKeyWord + '%' " +
            //    "OR DEPARTMENT.Department LIKE '%' + @searchKeyWord + '%' ) ";

            strSql = "SELECT EMPLOYEE.UserId, EMPLOYEE.Emp_Id, " +
                "(EMPLOYEE.LastName + ', ' + EMPLOYEE.FirstName, + ' ' + EMPLOYEE.MiddleName) AS FullName, " +
                "POSITION.Position AS [POSITION], DEPARTMENT.Department AS [DEPARTMENT] " +
                "FROM EMPLOYEE, POSITION, DEPARTMENT, UsersInRoles, Roles WHERE " +
                "EMPLOYEE.PositionId = POSITION.Id AND " +
                "POSITION.DepartmentId = DEPARTMENT.Id AND " +
                "DEPARTMENT.Id = @DepartmentId AND " +
                "EMPLOYEE.UserId = UsersInRoles.UserId AND " +
                "UsersInRoles.RoleId = Roles.RoleId AND " +
                "(Roles.RoleName = 'Staff') AND " +
                "(EMPLOYEE.Emp_Id LIKE '%' + @searchKeyWord + '%' " +
                "OR EMPLOYEE.FirstName LIKE '%' + @searchKeyWord + '%' " +
                "OR EMPLOYEE.MiddleName LIKE '%' + @searchKeyWord + '%' " +
                "OR EMPLOYEE.LastName LIKE '%' + @searchKeyWord + '%' " +
                "OR POSITION.Position LIKE '%' + @searchKeyWord + '%' " +
                "OR DEPARTMENT.Department LIKE '%' + @searchKeyWord + '%' ) ";

            conn = new SqlConnection();
            conn.ConnectionString = WebConfigurationManager.ConnectionStrings["dbAMS"].ConnectionString;
            comm = new SqlCommand(strSql, conn);
            comm.Parameters.AddWithValue("@searchKeyWord", strSearch);
            comm.Parameters.AddWithValue("@DepartmentId", deptId);
            dt = new DataTable();
            adp = new SqlDataAdapter(comm);

            conn.Open();
            adp.Fill(dt);
            conn.Close();

            return dt;
        }
        #endregion

        #region Profile
        public DataTable GetEmployee(Guid UserId)
        {
            strSql = "SELECT * FROM EMPLOYEE WHERE UserId= @UserId";

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

        public DataTable DisplayProfile(Guid UserId)
        {
            strSql = "SELECT EMPLOYEE.Emp_Id, " +
                "EMPLOYEE.FirstName, EMPLOYEE.MiddleName, EMPLOYEE.LastName, " +
                "EMPLOYEE.M_Status, EMPLOYEE.ContactNo, " +
                "Agency.Agency, " +
                "POSITION.Position AS [POSITION], DEPARTMENT.Department AS [DEPARTMENT] " +
                "FROM EMPLOYEE, POSITION, DEPARTMENT, AGENCY " +
                "WHERE " +
                "EMPLOYEE.UserId = JOB.UserId " +
                "AND POSITION.DepartmentId = DEPARTMENT.Id " +
                "AND JOB.PositionId = POSITION.Id " +
                "AND JOB.AgencyId = Agency.Id " +
                "AND EMPLOYEE.UserId = @UserId";
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

        public DataTable GetProfileBy(Guid UserId)
        {
            strSql = "SELECT * FROM EMPLOYEE WHERE UserId= @UserId";

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

        public string GetFullName(Guid UserId)
        {
            strSql = "SELECT (LastName + ', ' + FirstName + ' ' + MiddleName) AS FullName FROM EMPLOYEE " +
                "WHERE UserId= @UserId";

            conn = new SqlConnection();
            conn.ConnectionString = WebConfigurationManager.ConnectionStrings["dbAMS"].ConnectionString;
            comm = new SqlCommand(strSql, conn);
            comm.Parameters.AddWithValue("@UserId", UserId);

            dt = new DataTable();
            adp = new SqlDataAdapter(comm);

            conn.Open();
            adp.Fill(dt);
            conn.Close();

            return dt.Rows[0]["FullName"].ToString();
        }

        public void RegisterUser(
            Guid UserId,
            string firstName,
            string middleName,
            string lastName)
        {
            strSql = "INSERT INTO [EMPLOYEE] (UserId, FirstName, MiddleName, LastName) " +
                        "VALUES(@UserId,@FirstName,@MiddleName,@LastName)";

            conn = new SqlConnection();
            conn.ConnectionString = WebConfigurationManager.ConnectionStrings["dbAMS"].ConnectionString;

            using (comm = new SqlCommand(strSql, conn))
            {
                conn.Open();
                comm.Parameters.AddWithValue("@UserId", UserId);
                comm.Parameters.AddWithValue("@FirstName", firstName);
                comm.Parameters.AddWithValue("@MiddleName", middleName);
                comm.Parameters.AddWithValue("@LastName", lastName);

                comm.ExecuteNonQuery();
                conn.Close();
            }
            comm.Dispose();
            conn.Dispose();
        }

        public void UpdateProfile(
            string firstName,
            string middleName,
            string lastName,
            string phoneNo,
            string m_status,
            string gender,
            string nationality,
            string birthdate,
            string bloodtype,
            string language,
            Guid UserId)
        {
            strSql = "UPDATE EMPLOYEE " +
                "SET FirstName=@FName, " +
                "MiddleName=@MName, " +
                "LastName=@LName, " +
                "PhoneNo=@PhoneNo, " +
                "M_Status=@M_Status, " +
                "Gender=@Gender, " +
                "NationalityId=@NationalityId, " +
                "Bdate=@Bdate, " +
                "BloodType=@BloodType, " +
                "Language=@Language " + 
                "WHERE UserId=@UserId";

            conn = new SqlConnection();
            conn.ConnectionString = WebConfigurationManager.ConnectionStrings["dbAMS"].ConnectionString;

            using (comm = new SqlCommand(strSql, conn))
            {
                conn.Open();
                comm.Parameters.AddWithValue("@FName", firstName);
                comm.Parameters.AddWithValue("@MName", middleName);
                comm.Parameters.AddWithValue("@LName", lastName);
                comm.Parameters.AddWithValue("@PhoneNo", phoneNo);
                comm.Parameters.AddWithValue("@M_Status", m_status);
                comm.Parameters.AddWithValue("@Gender", gender);
                comm.Parameters.AddWithValue("@NationalityId", nationality);
                comm.Parameters.AddWithValue("@Bdate", birthdate);
                comm.Parameters.AddWithValue("@BloodType", bloodtype);
                comm.Parameters.AddWithValue("@Language", language);
                comm.Parameters.AddWithValue("@UserId", UserId);
                comm.ExecuteNonQuery();
                conn.Close();
            }
            comm.Dispose();
            conn.Dispose();
        }
        #endregion

        #region JOB
        public string GetRoleNameBypPosition(string PositionId)
        {
            strSql = "SELECT Roles.RoleName FROM Roles,POSITION " +
                "WHERE POSITION.RoleId = Roles.RoleId AND " +
                "POSITION.Id = @PositionId";

            conn = new SqlConnection();
            conn.ConnectionString = WebConfigurationManager.ConnectionStrings["dbAMS"].ConnectionString;
            comm = new SqlCommand(strSql, conn);
            comm.Parameters.AddWithValue("@PositionId", PositionId);
            dt = new DataTable();
            adp = new SqlDataAdapter(comm);

            conn.Open();
            adp.Fill(dt);
            conn.Close();

            return dt.Rows[0]["RoleName"].ToString();
        }


        public string GetRoleNameBypPositionName(string PositionName)
        {
            strSql = "SELECT Roles.RoleName FROM Roles,POSITION " +
                "WHERE POSITION.RoleId = Roles.RoleId AND " +
                "POSITION.Position = @Position";

            conn = new SqlConnection();
            conn.ConnectionString = WebConfigurationManager.ConnectionStrings["dbAMS"].ConnectionString;
            comm = new SqlCommand(strSql, conn);
            comm.Parameters.AddWithValue("@Position", PositionName);
            dt = new DataTable();
            adp = new SqlDataAdapter(comm);

            conn.Open();
            adp.Fill(dt);
            conn.Close();

            return dt.Rows[0]["RoleName"].ToString();
        }

        public string GetDepartment(Guid UserId)
        {
            strSql = "SELECT DEPARTMENT.Department FROM JOB, POSITION, DEPARTMENT  " +
                "WHERE JOB.PositionId = POSITION.Id AND " +
                "POSITION.DepartmentId = DEPARTMENT.Id AND " +
                "JOB.UserId = @UserId";

            conn = new SqlConnection();
            conn.ConnectionString = WebConfigurationManager.ConnectionStrings["dbAMS"].ConnectionString;
            comm = new SqlCommand(strSql, conn);
            comm.Parameters.AddWithValue("@UserId", UserId);
            dt = new DataTable();
            adp = new SqlDataAdapter(comm);

            conn.Open();
            adp.Fill(dt);
            conn.Close();

            return dt.Rows[0]["Department"].ToString();
        }

        public string GetDepartmentId(Guid UserId)
        {
            strSql = "SELECT POSITION.DepartmentId FROM JOB,POSITION,DEPARTMENT " +
                "WHERE JOB.PositionId = POSITION.Id " +
                "AND POSITION.DepartmentId = DEPARTMENT.Id " +
                "AND JOB.UserId = @UserId";

            conn = new SqlConnection();
            conn.ConnectionString = WebConfigurationManager.ConnectionStrings["dbAMS"].ConnectionString;
            comm = new SqlCommand(strSql, conn);
            comm.Parameters.AddWithValue("@UserId", UserId);
            dt = new DataTable();
            adp = new SqlDataAdapter(comm);

            conn.Open();
            adp.Fill(dt);
            conn.Close();

            return dt.Rows[0]["DepartmentId"].ToString();
        }

        public string getManagerName(string deptId)
        {
            strSql = "SELECT PERSONAL.LNAME + ', ' + PERSONAL.FNAME + ' ' + PERSONAL.MNAME AS [FullName] " +
                "FROM PERSONAL, JOB, POSITION, DEPARTMENT, UsersInRoles, Roles " +
                "WHERE " +
                "PERSONAL.UserId = JOB.UserId " +
                "AND JOB.PositionId = POSITION.Id " +
                "AND POSITION.DepartmentId = DEPARTMENT.Id " +
                "AND POSITION.DepartmentId = @DepartmentId " +
                "AND POSITION.RoleId = UsersInRoles.RoleId " +
                "AND UsersInRoles.RoleId = Roles.RoleId " +
                "AND PERSONAL.UserId = UsersInRoles.UserId " +
                "AND Roles.RoleName = 'Manager'";

            conn = new SqlConnection();
            conn.ConnectionString = WebConfigurationManager.ConnectionStrings["dbAMS"].ConnectionString;
            comm = new SqlCommand(strSql, conn);
            comm.Parameters.AddWithValue("@DepartmentId", deptId);
            dt = new DataTable();
            adp = new SqlDataAdapter(comm);

            conn.Open();
            adp.Fill(dt);
            conn.Close();

            StringBuilder sb = new StringBuilder();
            if (dt.Rows.Count > 1)
            {
                foreach (DataRow row in dt.Rows)
                {
                    sb.AppendLine(row["FullName"].ToString());
                    sb.AppendLine("<br>");
                }
                return sb.ToString();
            }
            else if (dt.Rows.Count == 1)
            {
                return dt.Rows[0]["FullName"].ToString();
            }

            return "N/A";
        }

        public string getSupervisorName(string deptId)
        {
            strSql = "SELECT PERSONAL.LNAME + ', ' + PERSONAL.FNAME + ' ' + PERSONAL.MNAME AS [FullName] " +
                "FROM PERSONAL, JOB, POSITION, DEPARTMENT, UsersInRoles, Roles " +
                "WHERE " +
                "PERSONAL.UserId = JOB.UserId " +
                "AND JOB.PositionId = POSITION.Id " +
                "AND POSITION.DepartmentId = DEPARTMENT.Id " +
                "AND POSITION.DepartmentId = @DepartmentId " +
                "AND POSITION.RoleId = UsersInRoles.RoleId " +
                "AND UsersInRoles.RoleId = Roles.RoleId " +
                "AND PERSONAL.UserId = UsersInRoles.UserId " +
                "AND Roles.RoleName = 'Supervisor'";

            conn = new SqlConnection();
            conn.ConnectionString = WebConfigurationManager.ConnectionStrings["dbAMS"].ConnectionString;
            comm = new SqlCommand(strSql, conn);
            comm.Parameters.AddWithValue("@DepartmentId", deptId);
            dt = new DataTable();
            adp = new SqlDataAdapter(comm);

            conn.Open();
            adp.Fill(dt);
            conn.Close();

            StringBuilder sb = new StringBuilder();
            if (dt.Rows.Count > 1)
            {
                foreach (DataRow row in dt.Rows)
                {
                    sb.AppendLine(row["FullName"].ToString());
                    sb.AppendLine("\r\n");
                }
                return sb.ToString();
            }
            else if (dt.Rows.Count == 1)
            {
                return dt.Rows[0]["FullName"].ToString();
            }

            return "N/A";
        }

        public string getHiredDate(Guid UserId)
        {
            strSql = "SELECT JOB.JoinDate FROM JOB WHERE JOB.UserId = @UserId";

            conn = new SqlConnection();
            conn.ConnectionString = WebConfigurationManager.ConnectionStrings["dbAMS"].ConnectionString;
            comm = new SqlCommand(strSql, conn);
            comm.Parameters.AddWithValue("@UserId", UserId);
            dt = new DataTable();
            adp = new SqlDataAdapter(comm);

            conn.Open();
            adp.Fill(dt);
            conn.Close();

            return dt.Rows[0]["JoinDate"].ToString();
        }

        public string getRoleName(Guid UserId)
        {
            strSql = "SELECT Roles.RoleName FROM Roles, UsersInRoles,Users " +
                "WHERE Roles.RoleId = UsersInRoles.RoleId AND " +
                "UsersInRoles.UserId = Users.UserId AND " +
                "Users.UserId = @UserId";

            conn = new SqlConnection();
            conn.ConnectionString = WebConfigurationManager.ConnectionStrings["dbAMS"].ConnectionString;
            comm = new SqlCommand(strSql, conn);
            comm.Parameters.AddWithValue("@UserId", UserId);
            dt = new DataTable();
            adp = new SqlDataAdapter(comm);

            conn.Open();
            adp.Fill(dt);
            conn.Close();

            return dt.Rows[0]["RoleName"].ToString();
        }

        public string getPosition(Guid UserId)
        {
            strSql = "SELECT POSITION.Position FROM POSITION, JOB WHERE " +
                "JOB.PositionId = POSITION.Id AND " +
                "JOB.UserId = @UserId";

            conn = new SqlConnection();
            conn.ConnectionString = WebConfigurationManager.ConnectionStrings["dbAMS"].ConnectionString;
            comm = new SqlCommand(strSql, conn);
            comm.Parameters.AddWithValue("@UserId", UserId);
            dt = new DataTable();
            adp = new SqlDataAdapter(comm);

            conn.Open();
            adp.Fill(dt);
            conn.Close();

            return dt.Rows[0]["Position"].ToString();
        }

        public string getAgencyName(Guid UserId)
        {
            strSql = "SELECT Agency.Agency FROM Agency, JOB WHERE " +
                "JOB.AgencyId = Agency.Id AND " +
                "JOB.UserId = @UserId";

            conn = new SqlConnection();
            conn.ConnectionString = WebConfigurationManager.ConnectionStrings["dbAMS"].ConnectionString;
            comm = new SqlCommand(strSql, conn);
            comm.Parameters.AddWithValue("@UserId", UserId);
            dt = new DataTable();
            adp = new SqlDataAdapter(comm);

            conn.Open();
            adp.Fill(dt);
            conn.Close();

            return dt.Rows[0]["Agency"].ToString();
        }

        public void AddJobDetails(
            Guid UserId,
            string emp_id,
            string positionId)
        {
            strSql = "INSERT INTO JOB(UserId,Emp_ID,PositionId,AgencyId) " +
                "VALUES(@UserId, @Emp_ID, @PositionId,@AgencyId)";

            conn = new SqlConnection();
            conn.ConnectionString = WebConfigurationManager.ConnectionStrings["dbAMS"].ConnectionString;

            using (comm = new SqlCommand(strSql, conn))
            {
                conn.Open();
                comm.Parameters.AddWithValue("@UserId", UserId);
                comm.Parameters.AddWithValue("@Emp_ID", emp_id);
                comm.Parameters.AddWithValue("@PositionId", positionId);
                // value '3' as None in Agency
                comm.Parameters.AddWithValue("@AgencyId", 3);

                comm.ExecuteNonQuery();
                conn.Close();
            }
            comm.Dispose();
            conn.Dispose();
        }

        public void UpdateJobDetails(
            string emp_Id,
            string position,
            string emp_status,
            string subUnit,
            string joinDate,
            string contract_sd,
            string contract_ed,
            string agencyId,
            string emovement,
            Guid UserId)
        {
            strSql = "UPDATE JOB SET " +
                "Emp_ID = @Emp_ID, " +
                "PositionId = @PositionId, " +
                "Emp_Status = @Emp_Status, " +
                "SubUnit = @SubUnit, " +
                "JoinDate = @JoinDate, " +
                "Contract_SD = @Contract_SD, " +
                "Contract_ED = @Contract_ED, " +
                "AgencyId = @AgencyId, " +
                "EMovement = @EMovement " +
                "WHERE " +
                "UserId = @UserId";

            conn = new SqlConnection();
            conn.ConnectionString = WebConfigurationManager.ConnectionStrings["dbAMS"].ConnectionString;

            using (comm = new SqlCommand(strSql, conn))
            {
                conn.Open();
                comm.Parameters.AddWithValue("@Emp_ID", emp_Id);
                comm.Parameters.AddWithValue("@PositionId", position);
                comm.Parameters.AddWithValue("@Emp_Status", emp_status);
                comm.Parameters.AddWithValue("@SubUnit", subUnit);
                comm.Parameters.AddWithValue("@JoinDate", joinDate);
                comm.Parameters.AddWithValue("@Contract_SD", contract_sd);
                comm.Parameters.AddWithValue("@Contract_ED", contract_ed);
                comm.Parameters.AddWithValue("@AgencyId", agencyId);
                comm.Parameters.AddWithValue("@EMovement", emovement);
                comm.Parameters.AddWithValue("@UserId", UserId);

                comm.ExecuteNonQuery();
                conn.Close();
            }
            comm.Dispose();
            conn.Dispose();
        }


        public void updateRole(
            Guid RoleId,
            Guid UserId)
        {
            strSql = "UPDATE UsersInRoles SET " +
                "RoleId = @RoleId " +
                "WHERE " +
                "UserId = @UserId";

            conn = new SqlConnection();
            conn.ConnectionString = WebConfigurationManager.ConnectionStrings["dbAMS"].ConnectionString;

            using (comm = new SqlCommand(strSql, conn))
            {
                conn.Open();
                comm.Parameters.AddWithValue("@RoleId", RoleId);
                comm.Parameters.AddWithValue("@UserId", UserId);

                comm.ExecuteNonQuery();
                conn.Close();
            }
            comm.Dispose();
            conn.Dispose();
        }

        public void deleteJob(string rowId)
        {
            strSql = "DELETE FROM JOB WHERE Id = @Id";

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
        #endregion
    }
}
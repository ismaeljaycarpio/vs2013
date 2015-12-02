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
        //display active employees
        //display all employee->user by HR manager, HR staff, GM
        //cant see admin 
        public DataTable DisplayEmployee(string strSearch)
        {
            strSql = "SELECT EMPLOYEE.UserId, EMPLOYEE.Emp_Id, " +
                "(EMPLOYEE.LastName + ', ' + EMPLOYEE.FirstName + ' ' + EMPLOYEE.MiddleName) AS FullName, " +
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
                "AND EMPLOYEE.AccountStatusId = 1 " + 
                "ORDER BY Employee.Emp_Id ASC";

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
            strSql = "SELECT EMPLOYEE.UserId, EMPLOYEE.Emp_Id, " +
                "(EMPLOYEE.LastName + ', ' + EMPLOYEE.FirstName + ' ' + EMPLOYEE.MiddleName) AS FullName, " +
                "POSITION.Position AS [POSITION], DEPARTMENT.Department AS [DEPARTMENT] " +
                "FROM Memberships, EMPLOYEE, POSITION, DEPARTMENT, UsersInRoles, Roles WHERE " +
                "Memberships.UserId = EMPLOYEE.UserId AND " +
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
                "OR DEPARTMENT.Department LIKE '%' + @searchKeyWord + '%' ) " +
                "AND EMPLOYEE.AccountStatusId = 1 " + 
                "ORDER BY Employee.Emp_Id ASC";

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
            strSql = "SELECT EMPLOYEE.UserId, EMPLOYEE.Emp_Id, " +
                "(EMPLOYEE.LastName + ', ' + EMPLOYEE.FirstName + ' ' + EMPLOYEE.MiddleName) AS FullName, " +
                "POSITION.Position AS [POSITION], DEPARTMENT.Department AS [DEPARTMENT] " +
                "FROM Memberships, EMPLOYEE, POSITION, DEPARTMENT, UsersInRoles, Roles WHERE " +
                "Memberships.UserId = EMPLOYEE.UserId AND " +
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
                "OR DEPARTMENT.Department LIKE '%' + @searchKeyWord + '%' ) " +
                "AND EMPLOYEE.AccountStatusId = 1 " +  
                "ORDER BY Employee.Emp_Id ASC";

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
                "EMPLOYEE.PositionId = POSITION.Id " +
                "AND POSITION.DepartmentId = DEPARTMENT.Id " +
                "AND EMPLOYEE.AgencyId = Agency.Id " +
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

        public DataTable GetProfileById(Guid UserId)
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

            if(dt.Rows.Count > 0)
            {
                return dt.Rows[0]["FullName"].ToString();
            }
            else
            {
                return "";
            }
            
        }

        //use only for sampling
        public void SeedUser(
            Guid UserId,
            string emp_Id,
            string firstName,
            string middleName,
            string lastName,
            string m_status,
            string gender,
            string nationalityId,
            string birthdate,
            string age,
            string bloodtype,
            string language,
            string positionId,
            string agencyId)
        {
            strSql = "INSERT INTO [EMPLOYEE] " +
                "(UserId, Emp_Id,FirstName, MiddleName, LastName, M_Status, Gender, NationalityId, BirthDate, Age, BloodType, Language, PositionId, AgencyId) " +
                "VALUES(@UserId,@Emp_Id,@FirstName,@MiddleName,@LastName,@M_Status,@Gender,@NationalityId,@BirthDate,@Age,@BloodType,@Language, @PositionId, @AgencyId)";

            conn = new SqlConnection();
            conn.ConnectionString = WebConfigurationManager.ConnectionStrings["dbAMS"].ConnectionString;

            using (comm = new SqlCommand(strSql, conn))
            {
                conn.Open();
                comm.Parameters.AddWithValue("@UserId", UserId);
                comm.Parameters.AddWithValue("@Emp_Id", emp_Id);
                comm.Parameters.AddWithValue("@FirstName", firstName);
                comm.Parameters.AddWithValue("@MiddleName", middleName);
                comm.Parameters.AddWithValue("@LastName", lastName);
                comm.Parameters.AddWithValue("@M_Status", m_status);
                comm.Parameters.AddWithValue("@Gender", gender);
                comm.Parameters.AddWithValue("@nationalityId", nationalityId);
                comm.Parameters.AddWithValue("@BirthDate", birthdate);
                comm.Parameters.AddWithValue("@Age", age);
                comm.Parameters.AddWithValue("@BloodType", bloodtype);
                comm.Parameters.AddWithValue("@Language", language);
                comm.Parameters.AddWithValue("@PositionId", positionId);
                comm.Parameters.AddWithValue("@AgencyId", agencyId);
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
            string m_status,
            string gender,
            string nationality,
            string birthdate,
            string bloodtype,
            string language,
            string contactNo,
            Guid UserId)
        {
            strSql = "UPDATE EMPLOYEE " +
                "SET FirstName=@FirstName, " +
                "MiddleName=@MiddleName, " +
                "LastName=@LastName, " +
                "M_Status=@M_Status, " +
                "Gender=@Gender, " +
                "NationalityId=@NationalityId, " +
                "BirthDate=@BirthDate, " +
                "BloodType=@BloodType, " +
                "Language=@Language, " +
                "ContactNo=@ContactNo " +
                "WHERE UserId=@UserId";

            conn = new SqlConnection();
            conn.ConnectionString = WebConfigurationManager.ConnectionStrings["dbAMS"].ConnectionString;

            using (comm = new SqlCommand(strSql, conn))
            {
                conn.Open();
                comm.Parameters.AddWithValue("@FirstName", firstName);
                comm.Parameters.AddWithValue("@MiddleName", middleName);
                comm.Parameters.AddWithValue("@LastName", lastName);
                comm.Parameters.AddWithValue("@M_Status", m_status);
                comm.Parameters.AddWithValue("@Gender", gender);
                comm.Parameters.AddWithValue("@NationalityId", nationality);
                comm.Parameters.AddWithValue("@BirthDate", birthdate);
                comm.Parameters.AddWithValue("@BloodType", bloodtype);
                comm.Parameters.AddWithValue("@Language", language);
                comm.Parameters.AddWithValue("@ContactNo", contactNo);
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


        public string GetRoleNameByPositionName(string PositionName)
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
            strSql = "SELECT DEPARTMENT.Department FROM EMPLOYEE, POSITION, DEPARTMENT  " +
                "WHERE EMPLOYEE.PositionId = POSITION.Id AND " +
                "POSITION.DepartmentId = DEPARTMENT.Id AND " +
                "EMPLOYEE.UserId = @UserId";

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
            strSql = "SELECT POSITION.DepartmentId FROM EMPLOYEE,POSITION,DEPARTMENT " +
                "WHERE EMPLOYEE.PositionId = POSITION.Id " +
                "AND POSITION.DepartmentId = DEPARTMENT.Id " +
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

            return dt.Rows[0]["DepartmentId"].ToString();
        }

        public string GetManagerName(string deptId)
        {
            strSql = "SELECT EMPLOYEE.LastName + ', ' + EMPLOYEE.FirstName + ' ' + EMPLOYEE.MiddleName AS [FullName] " +
                "FROM EMPLOYEE, POSITION, DEPARTMENT, UsersInRoles, Roles " +
                "WHERE " +
                "EMPLOYEE.PositionId = POSITION.Id " +
                "AND POSITION.DepartmentId = DEPARTMENT.Id " +
                "AND POSITION.DepartmentId = @DepartmentId " +
                "AND POSITION.RoleId = UsersInRoles.RoleId " +
                "AND UsersInRoles.RoleId = Roles.RoleId " +
                "AND EMPLOYEE.UserId = UsersInRoles.UserId " +
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

        public string GetSupervisorName(string deptId)
        {
            strSql = "SELECT EMPLOYEE.FirstName + ', ' + EMPLOYEE.MiddleName + ' ' + EMPLOYEE.MiddleName AS [FullName] " +
                "FROM EMPLOYEE, POSITION, DEPARTMENT, UsersInRoles, Roles " +
                "WHERE " +
                "EMPLOYEE.PositionId = POSITION.Id " +
                "AND POSITION.DepartmentId = DEPARTMENT.Id " +
                "AND POSITION.DepartmentId = @DepartmentId " +
                "AND POSITION.RoleId = UsersInRoles.RoleId " +
                "AND UsersInRoles.RoleId = Roles.RoleId " +
                "AND EMPLOYEE.UserId = UsersInRoles.UserId " +
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

        public string GetHiredDate(Guid UserId)
        {
            strSql = "SELECT EMPLOYEE.JoinDate FROM EMPLOYEE WHERE EMPLOYEE.UserId = @UserId";

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

        public string GetLastEvaluationDate(Guid UserId)
        {
            strSql = "SELECT MAX(CONVERT(DATE,DateEvaluated,101)) AS [LastEvaluationDate] FROM Evaluation WHERE Evaluation.UserId = @UserId";

            conn = new SqlConnection();
            conn.ConnectionString = WebConfigurationManager.ConnectionStrings["dbAMS"].ConnectionString;
            comm = new SqlCommand(strSql, conn);
            comm.Parameters.AddWithValue("@UserId", UserId);
            dt = new DataTable();
            adp = new SqlDataAdapter(comm);

            conn.Open();
            adp.Fill(dt);
            conn.Close();

            return dt.Rows[0]["LastEvaluationDate"].ToString();
        }

        public string GetNextEvaluationDate(Guid UserId)
        {
            strSql = "SELECT MAX(CONVERT(DATE,NextEvaluationDate,101)) AS [NextEvaluationDate] FROM Evaluation WHERE Evaluation.UserId = @UserId";
            conn = new SqlConnection();
            conn.ConnectionString = WebConfigurationManager.ConnectionStrings["dbAMS"].ConnectionString;
            comm = new SqlCommand(strSql, conn);
            comm.Parameters.AddWithValue("@UserId", UserId);
            dt = new DataTable();
            adp = new SqlDataAdapter(comm);

            conn.Open();
            adp.Fill(dt);
            conn.Close();

            return dt.Rows[0]["NextEvaluationDate"].ToString();
        }

        public string GetRoleName(Guid UserId)
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

        public string GetPosition(Guid UserId)
        {
            strSql = "SELECT POSITION.Position FROM POSITION, EMPLOYEE WHERE " +
                "EMPLOYEE.PositionId = POSITION.Id AND " +
                "EMPLOYEE.UserId = @UserId";

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

        public string GetPositionId(Guid UserId)
        {
            strSql = "SELECT PositionId FROM EMPLOYEE WHERE UserId = @UserId";

            conn = new SqlConnection();
            conn.ConnectionString = WebConfigurationManager.ConnectionStrings["dbAMS"].ConnectionString;
            comm = new SqlCommand(strSql, conn);
            comm.Parameters.AddWithValue("@UserId", UserId);
            dt = new DataTable();
            adp = new SqlDataAdapter(comm);

            conn.Open();
            adp.Fill(dt);
            conn.Close();

            return dt.Rows[0]["PositionId"].ToString();
        }

        public string GetAgencyName(Guid UserId)
        {
            strSql = "SELECT Agency.Agency FROM Agency, EMPLOYEE WHERE " +
                "EMPLOYEE.AgencyId = Agency.Id AND " +
                "EMPLOYEE.UserId = @UserId";

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
            strSql = "INSERT INTO EMPLOYEE(UserId,Emp_ID,PositionId,AgencyId) " +
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
            string account_status_id,
            Guid UserId)
        {
            strSql = "UPDATE EMPLOYEE SET " +
                "Emp_ID = @Emp_ID, " +
                "PositionId = @PositionId, " +
                "Emp_Status = @Emp_Status, " +
                "SubUnit = @SubUnit, " +
                "JoinDate = @JoinDate, " +
                "Contract_SD = @Contract_SD, " +
                "Contract_ED = @Contract_ED, " +
                "AgencyId = @AgencyId, " +
                "AccountStatusId = @AccountStatusId, " +
                "DateAccountStatusModified = @DateAccountStatusModified " +
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
                comm.Parameters.AddWithValue("@AccountStatusId", account_status_id);
                comm.Parameters.AddWithValue("@DateAccountStatusModified", DateTime.Now.ToShortDateString());
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

        public int UpdateStatusExpiredContracts()
        {
            int affectedRows = 0;

            strSql = "UPDATE EMPLOYEE SET AccountStatusId = 4 " +
                        "WHERE UserId IN (SELECT UserId FROM EMPLOYEE WHERE Contract_ED = CONVERT(DATE,GETDATE(),101) AND AccountStatusId != 4)";

            conn = new SqlConnection();
            conn.ConnectionString = WebConfigurationManager.ConnectionStrings["dbAMS"].ConnectionString;
            dt = new DataTable();
            using (comm = new SqlCommand(strSql, conn))
            {
                conn.Open();
                affectedRows = comm.ExecuteNonQuery();
                conn.Close();
            }
            comm.Dispose();
            conn.Dispose();

            return affectedRows;
        }
        #endregion

        #region User Membership
        //fill EMPLOYEE tbl
        public void RegisterUser(
            Guid UserId,
            string emp_Id,
            string firstName,
            string middleName,
            string lastName,
            string positionId)
        {
            strSql = "INSERT INTO [EMPLOYEE] " +
                "(UserId, Emp_Id,FirstName, MiddleName, LastName, M_Status, Gender, NationalityId, PositionId,AgencyId) " +
                "VALUES(@UserId,@Emp_Id,@FirstName,@MiddleName,@LastName,@M_Status,@Gender,@NationalityId,@PositionId,@AgencyId)";

            conn = new SqlConnection();
            conn.ConnectionString = WebConfigurationManager.ConnectionStrings["dbAMS"].ConnectionString;

            using (comm = new SqlCommand(strSql, conn))
            {
                conn.Open();
                comm.Parameters.AddWithValue("@UserId", UserId);
                comm.Parameters.AddWithValue("@Emp_Id", emp_Id);
                comm.Parameters.AddWithValue("@FirstName", firstName);
                comm.Parameters.AddWithValue("@MiddleName", middleName);
                comm.Parameters.AddWithValue("@LastName", lastName);
                comm.Parameters.AddWithValue("@M_Status", "Single");
                comm.Parameters.AddWithValue("@Gender", "MALE");
                comm.Parameters.AddWithValue("@NationalityId", 67); //67 -> Filipino
                comm.Parameters.AddWithValue("@PositionId", positionId);
                comm.Parameters.AddWithValue("@AgencyId", 3);
                comm.ExecuteNonQuery();
                conn.Close();
            }
            comm.Dispose();
            conn.Dispose();
        }

        public string GetGeneratedUserName()
        {
            strSql = "SELECT MAX(RowId) AS RowId FROM EMPLOYEE";

            conn = new SqlConnection();
            conn.ConnectionString = WebConfigurationManager.ConnectionStrings["dbAMS"].ConnectionString;
            comm = new SqlCommand(strSql, conn);
            dt = new DataTable();
            adp = new SqlDataAdapter(comm);

            conn.Open();
            adp.Fill(dt);
            conn.Close();

            return dt.Rows[0]["RowId"].ToString();
        }
        #endregion
    }
}
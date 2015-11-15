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
    public class Job
    {
        //init
        SqlConnection conn;
        SqlCommand comm;
        SqlDataAdapter adp;
        DataTable dt;
        string strSql = "";

        public DataTable displayJob()
        {
            strSql = "SELECT * FROM JOB";

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

        public DataTable getJobDetailsById(Guid UserId)
        {
            strSql = "SELECT * FROM JOB WHERE UserId = @UserId";

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

        public string getRoleNameBypPosition(string PositionId)
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

        public string getRoleNameBypPositionName(string PositionName)
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

        public string getDepartment(Guid UserId)
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

        public string getDepartmentId(Guid UserId)
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


        public DataTable getJobDetailsByRowId(int rowId)
        {
            strSql = "SELECT * FROM JOB WHERE Id = @Id";

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
        
        //get manager by dept
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

        //get supervisor by supervisor
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
            if(dt.Rows.Count > 1)
            {
                foreach(DataRow row in dt.Rows)
                {
                    sb.AppendLine(row["FullName"].ToString());
                    sb.AppendLine("\r\n");
                }
                return sb.ToString();
            }
            else if(dt.Rows.Count == 1)
            {
                return dt.Rows[0]["FullName"].ToString();
            }

            return "N/A";
        }

        //get hired date/join date
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

        //get rolename
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

        //get position
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

        //get agency
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

        public void addJobDetails(
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


        public void updateJobDetails(
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
    }
}
﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using System.Data.SqlClient;
using System.Data;

namespace AMS.DAL
{
    public class Leaves
    {
        #region FileMaintenance
        //init
        SqlConnection conn;
        SqlCommand comm;
        SqlDataAdapter adp;
        DataTable dt;
        string strSql = "";

        public DataTable displayLeaves()
        {
            strSql = "SELECT LeaveType.Id, LeaveType.LeaveName, LeaveType.DefaultDays, " +
                "AGENCY.Agency " +
                "FROM LeaveType, AGENCY " +
                "WHERE " +
                "LeaveType.AgencyId = AGENCY.Id";

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

        public DataTable getLeaveId(int rowId)
        {
            strSql = "SELECT * FROM LeaveType WHERE Id = @Id";

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

        public void addLeaveType(
            string leaveName,
            string noOfDays,
            string agencyId
            )
        {
            strSql = "INSERT INTO LeaveType(LeaveName,DefaultDays,AgencyId) " +
                "VALUES(@LeaveName,@DefaultDays,@AgencyId)";

            conn = new SqlConnection();
            conn.ConnectionString = WebConfigurationManager.ConnectionStrings["dbAMS"].ConnectionString;

            using (comm = new SqlCommand(strSql, conn))
            {
                conn.Open();
                comm.Parameters.AddWithValue("@LeaveName", leaveName);
                comm.Parameters.AddWithValue("@DefaultDays", noOfDays);
                comm.Parameters.AddWithValue("@AgencyId", agencyId);
                comm.ExecuteNonQuery();
                conn.Close();
            }
            comm.Dispose();
            conn.Dispose();
        }

        public void updateLeave(
            string leaveName,
            string noOfDays,
            string agencyId,
            string rowId)
        {
            strSql = "UPDATE LeaveType SET " +
                "LeaveName = @LeaveName, " +
                "DefaultDays = @DefaultDays," +
                "ModifiedDate = @ModifiedDate, " +
                "AgencyId = @AgencyId " +
                "WHERE Id = @RowId";

            conn = new SqlConnection();
            conn.ConnectionString = WebConfigurationManager.ConnectionStrings["dbAMS"].ConnectionString;

            using (comm = new SqlCommand(strSql, conn))
            {
                conn.Open();
                comm.Parameters.AddWithValue("@LeaveName", leaveName);
                comm.Parameters.AddWithValue("@DefaultDays", noOfDays);
                comm.Parameters.AddWithValue("@ModifiedDate", DateTime.Now);
                comm.Parameters.AddWithValue("@AgencyId", agencyId);
                comm.Parameters.AddWithValue("@RowId", rowId);

                comm.ExecuteNonQuery();
                conn.Close();
            }
            comm.Dispose();
            conn.Dispose();
        }

        public void deleteLeave(string rowId)
        {
            strSql = "DELETE FROM LeaveType WHERE Id = @Id";

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


        #region AssignLeaves
        public DataTable getLeaves(Guid userId, string agencyId)
        {
            strSql = "SELECT LeaveType.Id, LeaveType.LeaveName,  " +
                "LeaveType.DefaultDays, LeaveTypeUser.NoOfDays FROM LeaveType " +
                "LEFT JOIN LeaveTypeUser " +
                "ON LeaveType.Id = LeaveTypeId " +
                "AND LeaveTypeUser.UserId = @UserId " +
                "WHERE LeaveType.AgencyId = @AgencyId";

            conn = new SqlConnection();
            conn.ConnectionString = WebConfigurationManager.ConnectionStrings["dbAMS"].ConnectionString;
            comm = new SqlCommand(strSql, conn);
            comm.Parameters.AddWithValue("@UserId", userId);
            comm.Parameters.AddWithValue("@AgencyId", agencyId);
            dt = new DataTable();
            adp = new SqlDataAdapter(comm);

            conn.Open();
            adp.Fill(dt);
            conn.Close();

            return dt;
        }

        public void addLeaveTypeToUser(
            string leaveTypeId,
            string noOfDays,
            Guid userId)
        {
            //add leave to user
            strSql = "INSERT INTO LeaveTypeUser(UserId,LeaveTypeId,NoOfDays) " +
                    "VALUES(@UserId,@LeaveTypeId,@NoOfDays)";

            conn = new SqlConnection();
            conn.ConnectionString = WebConfigurationManager.ConnectionStrings["dbAMS"].ConnectionString;

            using (comm = new SqlCommand(strSql, conn))
            {
                conn.Open();
                comm.Parameters.AddWithValue("@UserId", userId);
                comm.Parameters.AddWithValue("@LeaveTypeId", leaveTypeId);
                comm.Parameters.AddWithValue("@NoOfDays", noOfDays);
                comm.ExecuteNonQuery();
                conn.Close();
            }
            comm.Dispose();
            conn.Dispose();
        }

        public void deleteLeavesForUser(Guid userId)
        {
            //delete leave for user
            strSql = "DELETE FROM LeaveTypeUser WHERE UserId = @UserId";
            conn = new SqlConnection();
            conn.ConnectionString = WebConfigurationManager.ConnectionStrings["dbAMS"].ConnectionString;
            using (comm = new SqlCommand(strSql, conn))
            {
                conn.Open();
                comm.Parameters.AddWithValue("@UserId", userId);
                comm.ExecuteNonQuery();
                conn.Close();
            }
            comm.Dispose();
            conn.Dispose();
        }
        #endregion

        #region LeaveTransaction
        public DataTable getMyLeaves(Guid userId)
        {
            strSql = "SELECT LeaveTransaction.Id, LeaveType.LeaveName, " +
                "LeaveTransaction.NumberOfDays, LeaveTransaction.FromDate, " +
                "LeaveTransaction.ToDate, LeaveTransaction.DepartmentHeadApproval, " +
                "LeaveTransaction.HRApproval, LeaveTransaction.FiledDate " +
                "FROM LeaveTransaction, LeaveType, LeaveTypeUser " +
                "WHERE " +
                "LeaveTransaction.UserId = LeaveTypeUser.UserId " +
                "AND LeaveTransaction.LeaveTypeUserId = LeaveTypeUser.Id " +
                "AND LeaveTypeUser.LeaveTypeId = LeaveType.Id " +
                "AND LeaveTypeUser.UserId = @UserId";

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

        public DataTable getMyLeaves(int rowId)
        {
            strSql = "SELECT LeaveTransaction.Id, LeaveTransaction.LeaveTypeUserId,LeaveType.LeaveName, " +
                "LeaveTransaction.NumberOfDays, LeaveTransaction.FromDate, " +
                "LeaveTransaction.ToDate, LeaveTransaction.DepartmentHeadApproval, " +
                "LeaveTransaction.HRApproval " +
                "FROM LeaveTransaction, LeaveType, LeaveTypeUser " +
                "WHERE " +
                "LeaveTransaction.UserId = LeaveTypeUser.UserId " +
                "AND LeaveTransaction.LeaveTypeUserId = LeaveTypeUser.Id " +
                "AND LeaveTypeUser.LeaveTypeId = LeaveType.Id " +
                "AND LeaveTransaction.Id = @Id";

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

        public DataTable fillMyLeaves(Guid userId)
        {
            strSql = "SELECT LeaveTypeUser.Id, LeaveType.LeaveName, " +
                "LeaveTypeUser.NoOfDays " +
                "FROM LeaveTypeUser, LeaveType " +
                "WHERE " +
                "LeaveTypeUser.LeaveTypeId = LeaveType.Id " +
                "AND LeaveTypeUser.UserId = @UserId";

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

        public string getRemainingDays(Guid userId, string Id)
        {
            strSql = "SELECT NoOfDays FROM LeaveTypeUser WHERE UserId=@UserId AND Id=@Id";

            conn = new SqlConnection();
            conn.ConnectionString = WebConfigurationManager.ConnectionStrings["dbAMS"].ConnectionString;
            comm = new SqlCommand(strSql, conn);
            comm.Parameters.AddWithValue("@UserId", userId);
            comm.Parameters.AddWithValue("@Id", Id);
            dt = new DataTable();
            adp = new SqlDataAdapter(comm);

            conn.Open();
            adp.Fill(dt);
            conn.Close();

            return dt.Rows[0]["NoOfDays"].ToString();
        }

        public void fileLeave(
            string leaveTypeId,
            string noOfDays,
            string fromDate,
            string toDate,
            Guid userId)
        {
            //add leave to user
            strSql = "INSERT INTO LeaveTransaction(LeaveTypeUserId,NumberOfDays,FromDate,ToDate,UserId,FiledDate,Status) " +
                    "VALUES(@LeaveTypeUserId,@NumberOfDays,@FromDate,@ToDate,@UserId,@FiledDate,@Status)";

            conn = new SqlConnection();
            conn.ConnectionString = WebConfigurationManager.ConnectionStrings["dbAMS"].ConnectionString;

            using (comm = new SqlCommand(strSql, conn))
            {
                conn.Open();
                comm.Parameters.AddWithValue("@UserId", userId);
                comm.Parameters.AddWithValue("@LeaveTypeUserId", leaveTypeId);
                comm.Parameters.AddWithValue("@NumberOfDays", noOfDays);
                comm.Parameters.AddWithValue("@FromDate", Convert.ToDateTime(fromDate));
                comm.Parameters.AddWithValue("@ToDate", Convert.ToDateTime(toDate));
                comm.Parameters.AddWithValue("@FiledDate", DateTime.Now);
                comm.Parameters.AddWithValue("@Status", "Pending");
                comm.ExecuteNonQuery();
                conn.Close();
            }
            comm.Dispose();
            conn.Dispose();
        }

        public void editfiledLeave(
            string leaveTypeUserId,
            string noOfDays,
            string fromDate,
            string toDate,
            string rowId)
        {
            strSql = "UPDATE LeaveTransaction SET " +
                "LeaveTypeUserId = @LeaveTypeUserId, " +
                "NumberOfDays = @NumberOfDays," +
                "FromDate = @FromDate, " +
                "ToDate = @ToDate, " +
                "FiledDate = @FiledDate " +
                "WHERE Id = @Id";

            conn = new SqlConnection();
            conn.ConnectionString = WebConfigurationManager.ConnectionStrings["dbAMS"].ConnectionString;

            using (comm = new SqlCommand(strSql, conn))
            {
                conn.Open();
                comm.Parameters.AddWithValue("@Id", rowId);
                comm.Parameters.AddWithValue("@LeaveTypeUserId", leaveTypeUserId);
                comm.Parameters.AddWithValue("@NumberOfDays", noOfDays);
                comm.Parameters.AddWithValue("@FromDate", Convert.ToDateTime(fromDate));
                comm.Parameters.AddWithValue("@ToDate", Convert.ToDateTime(toDate));
                comm.Parameters.AddWithValue("@FiledDate", DateTime.Now);
                comm.ExecuteNonQuery();
                conn.Close();
            }
            comm.Dispose();
            conn.Dispose();
        }

        public void deleteLeaveTransaction(string rowId)
        {
            strSql = "DELETE FROM LeaveTransaction WHERE Id = @Id";

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

        #region LeaveApproval

        //for admin only
        //edit 5-16-2016 >> show all roles
        public DataTable DisplayPendingLeaveApproval()
        {
            strSql = "SELECT LeaveTransaction.Id, LeaveTransaction.UserId, EMPLOYEE.Emp_Id, " +
                "(EMPLOYEE.LastName + ', ' + EMPLOYEE.FirstName + ' ' + EMPLOYEE.MiddleName) AS FullName, " +
                "POSITION.Position AS [POSITION], DEPARTMENT.Department AS [DEPARTMENT], " +
                "AGENCY.Agency AS [Agency], " +
                "LeaveTransaction.NumberOfDays, LeaveTransaction.DepartmentHeadApproval, " +
                "LeaveTransaction.HRApproval," +
                "LeaveTransaction.FromDate," +
                "LeaveTransaction.ToDate, " +
                "LeaveTransaction.FiledDate, " +
                "LeaveType.LeaveName " +
                "FROM Memberships, EMPLOYEE, POSITION, DEPARTMENT, UsersInRoles, Roles, Agency, " +
                "LeaveTransaction, LeaveTypeUser, LeaveType " +
                "WHERE " +
                "LeaveTransaction.UserId = Memberships.UserId AND " +
                "Memberships.UserId = EMPLOYEE.UserId AND " +
                "EMPLOYEE.PositionId = POSITION.Id AND " +
                "POSITION.DepartmentId = DEPARTMENT.Id AND " +
                "EMPLOYEE.UserId = UsersInRoles.UserId AND " +
                "UsersInRoles.RoleId = Roles.RoleId AND " +
                "EMPLOYEE.AgencyId = AGENCY.Id AND " +
                "Roles.RoleName != 'Admin' AND " +
                "EMPLOYEE.AccountStatusId = 1 " +
                "AND LeaveTransaction.LeaveTypeUserId = LeaveTypeUser.Id " +
                "AND LeaveTypeUser.LeaveTypeId = LeaveType.Id " +
                "AND LeaveTransaction.DepartmentHeadApproval IS NULL " +
                "AND LeaveTransaction.HRApproval IS NULL " +
                "ORDER BY LeaveTransaction.Id DESC";

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

        //display leave report
        public DataTable DisplayLeaveApproval_Admin(string status)
        {
            strSql = "SELECT LeaveTransaction.Id, LeaveTransaction.UserId, EMPLOYEE.Emp_Id, " +
                "(EMPLOYEE.LastName + ', ' + EMPLOYEE.FirstName + ' ' + EMPLOYEE.MiddleName) AS FullName, " +
                "POSITION.Position AS [POSITION], DEPARTMENT.Department AS [DEPARTMENT], " +
                "AGENCY.Agency AS [Agency], " +
                "LeaveTransaction.NumberOfDays, LeaveTransaction.DepartmentHeadApproval, " +
                "LeaveTransaction.HRApproval," +
                "LeaveTransaction.FromDate," +
                "LeaveTransaction.ToDate, " +
                "LeaveTransaction.FiledDate, " +
                "LeaveType.LeaveName " +
                "FROM Memberships, EMPLOYEE, POSITION, DEPARTMENT, UsersInRoles, Roles, Agency, " +
                "LeaveTransaction, LeaveTypeUser, LeaveType WHERE " +
                "LeaveTransaction.UserId = Memberships.UserId AND " +
                "Memberships.UserId = EMPLOYEE.UserId AND " +
                "EMPLOYEE.PositionId = POSITION.Id AND " +
                "POSITION.DepartmentId = DEPARTMENT.Id AND " +
                "EMPLOYEE.UserId = UsersInRoles.UserId AND " +
                "UsersInRoles.RoleId = Roles.RoleId AND " +
                "EMPLOYEE.AgencyId = AGENCY.Id AND " +
                "Roles.RoleName != 'Admin' AND " +
                "EMPLOYEE.AccountStatusId = 1 " +
                "AND LeaveTransaction.LeaveTypeUserId = LeaveTypeUser.Id " +
                "AND LeaveTypeUser.LeaveTypeId = LeaveType.Id " +
                "AND LeaveTransaction.Status = @Status " +
                "ORDER BY LeaveTransaction.Id DESC";

            conn = new SqlConnection();
            conn.ConnectionString = WebConfigurationManager.ConnectionStrings["dbAMS"].ConnectionString;
            comm = new SqlCommand(strSql, conn);
            comm.Parameters.AddWithValue("@Status", status);
            dt = new DataTable();
            adp = new SqlDataAdapter(comm);

            conn.Open();
            adp.Fill(dt);
            conn.Close();

            return dt;
        }

        //display leave report
        public DataTable DisplayLeaveApproval_Admin(string status, string startDate)
        {
            strSql = "SELECT LeaveTransaction.Id, LeaveTransaction.UserId, EMPLOYEE.Emp_Id, " +
                "(EMPLOYEE.LastName + ', ' + EMPLOYEE.FirstName + ' ' + EMPLOYEE.MiddleName) AS FullName, " +
                "POSITION.Position AS [POSITION], DEPARTMENT.Department AS [DEPARTMENT], " +
                "AGENCY.Agency AS [Agency], " +
                "LeaveTransaction.NumberOfDays, LeaveTransaction.DepartmentHeadApproval, " +
                "LeaveTransaction.HRApproval," +
                "LeaveTransaction.FromDate," +
                "LeaveTransaction.ToDate, " +
                "LeaveTransaction.FiledDate, " +
                "LeaveType.LeaveName " +
                "FROM Memberships, EMPLOYEE, POSITION, DEPARTMENT, UsersInRoles, Roles, Agency, " +
                "LeaveTransaction, LeaveTypeUser, LeaveType WHERE " +
                "LeaveTransaction.UserId = Memberships.UserId AND " +
                "Memberships.UserId = EMPLOYEE.UserId AND " +
                "EMPLOYEE.PositionId = POSITION.Id AND " +
                "POSITION.DepartmentId = DEPARTMENT.Id AND " +
                "EMPLOYEE.UserId = UsersInRoles.UserId AND " +
                "UsersInRoles.RoleId = Roles.RoleId AND " +
                "EMPLOYEE.AgencyId = AGENCY.Id AND " +
                "Roles.RoleName != 'Admin' AND " +
                "EMPLOYEE.AccountStatusId = 1 " +
                "AND LeaveTransaction.LeaveTypeUserId = LeaveTypeUser.Id " +
                "AND LeaveTypeUser.LeaveTypeId = LeaveType.Id " +
                "AND LeaveTransaction.Status = @Status " +
                "AND (LeaveTransaction.FiledDate = @StartDate OR LeaveTransaction.FromDate = @StartDate OR LeaveTransaction.ToDate = @StartDate) " + 
                "ORDER BY LeaveTransaction.Id DESC";

            conn = new SqlConnection();
            conn.ConnectionString = WebConfigurationManager.ConnectionStrings["dbAMS"].ConnectionString;
            comm = new SqlCommand(strSql, conn);
            comm.Parameters.AddWithValue("@Status", status);
            comm.Parameters.AddWithValue("@StartDate", Convert.ToDateTime(startDate));
            dt = new DataTable();
            adp = new SqlDataAdapter(comm);

            conn.Open();
            adp.Fill(dt);
            conn.Close();

            return dt;
        }

        //display leave report
        public DataTable DisplayLeaveApproval_Admin(string status, string startDate, string endDate)
        {
            strSql = "SELECT LeaveTransaction.Id, LeaveTransaction.UserId, EMPLOYEE.Emp_Id, " +
                "(EMPLOYEE.LastName + ', ' + EMPLOYEE.FirstName + ' ' + EMPLOYEE.MiddleName) AS FullName, " +
                "POSITION.Position AS [POSITION], DEPARTMENT.Department AS [DEPARTMENT], " +
                "AGENCY.Agency AS [Agency], " +
                "LeaveTransaction.NumberOfDays, LeaveTransaction.DepartmentHeadApproval, " +
                "LeaveTransaction.HRApproval," +
                "LeaveTransaction.FromDate," +
                "LeaveTransaction.ToDate, " +
                "LeaveTransaction.FiledDate, " +
                "LeaveType.LeaveName " +
                "FROM Memberships, EMPLOYEE, POSITION, DEPARTMENT, UsersInRoles, Roles, Agency, " +
                "LeaveTransaction, LeaveTypeUser, LeaveType WHERE " +
                "LeaveTransaction.UserId = Memberships.UserId AND " +
                "Memberships.UserId = EMPLOYEE.UserId AND " +
                "EMPLOYEE.PositionId = POSITION.Id AND " +
                "POSITION.DepartmentId = DEPARTMENT.Id AND " +
                "EMPLOYEE.UserId = UsersInRoles.UserId AND " +
                "UsersInRoles.RoleId = Roles.RoleId AND " +
                "EMPLOYEE.AgencyId = AGENCY.Id AND " +
                "Roles.RoleName != 'Admin' AND " +
                "EMPLOYEE.AccountStatusId = 1 " +
                "AND LeaveTransaction.LeaveTypeUserId = LeaveTypeUser.Id " +
                "AND LeaveTypeUser.LeaveTypeId = LeaveType.Id " +
                "AND LeaveTransaction.Status = @Status " +
                "AND (LeaveTransaction.FiledDate = @StartDate OR LeaveTransaction.FromDate = @StartDate OR LeaveTransaction.ToDate = @StartDate) " +
                "ORDER BY LeaveTransaction.Id DESC";

            conn = new SqlConnection();
            conn.ConnectionString = WebConfigurationManager.ConnectionStrings["dbAMS"].ConnectionString;
            comm = new SqlCommand(strSql, conn);
            comm.Parameters.AddWithValue("@Status", status);
            comm.Parameters.AddWithValue("@StartDate", Convert.ToDateTime(startDate));
            dt = new DataTable();
            adp = new SqlDataAdapter(comm);

            conn.Open();
            adp.Fill(dt);
            conn.Close();

            return dt;
        }

        //for hr manager only
        public DataTable DisplayPendingLeaveApproval_HR()
        {
            strSql = "SELECT LeaveTransaction.Id, LeaveTransaction.UserId, EMPLOYEE.Emp_Id, " +
                "(EMPLOYEE.LastName + ', ' + EMPLOYEE.FirstName + ' ' + EMPLOYEE.MiddleName) AS FullName, " +
                "POSITION.Position AS [POSITION], DEPARTMENT.Department AS [DEPARTMENT], " +
                "AGENCY.Agency AS [Agency], " +
                "LeaveTransaction.NumberOfDays, LeaveTransaction.DepartmentHeadApproval, " +
                "LeaveTransaction.HRApproval," +
                "LeaveTransaction.FromDate," +
                "LeaveTransaction.ToDate, " +
                "LeaveTransaction.FiledDate, " +
                "LeaveType.LeaveName " +
                "FROM Memberships, EMPLOYEE, POSITION, DEPARTMENT, UsersInRoles, Roles, Agency, " +
                "LeaveTransaction, LeaveTypeUser, LeaveType WHERE " +
                "LeaveTransaction.UserId = Memberships.UserId AND " +
                "Memberships.UserId = EMPLOYEE.UserId AND " +
                "EMPLOYEE.PositionId = POSITION.Id AND " +
                "POSITION.DepartmentId = DEPARTMENT.Id AND " +
                "EMPLOYEE.UserId = UsersInRoles.UserId AND " +
                "UsersInRoles.RoleId = Roles.RoleId AND " +
                "EMPLOYEE.AgencyId = AGENCY.Id AND " +
                "Roles.RoleName != 'Admin' AND " +
                "EMPLOYEE.AccountStatusId = 1 " +
                "AND LeaveTransaction.LeaveTypeUserId = LeaveTypeUser.Id " +
                "AND LeaveTypeUser.LeaveTypeId = LeaveType.Id " +
                "AND LeaveTransaction.DepartmentHeadApproval = 'Approved' " +
                "AND LeaveTransaction.HRApproval IS NULL " +
                "ORDER BY LeaveTransaction.Id DESC";

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

        //for manager only
        public DataTable DisplayPendingLeaveApproval_Mang(string deptId)
        {
            strSql = "SELECT LeaveTransaction.Id, LeaveTransaction.UserId, EMPLOYEE.Emp_Id, " +
                "(EMPLOYEE.LastName + ', ' + EMPLOYEE.FirstName + ' ' + EMPLOYEE.MiddleName) AS FullName, " +
                "POSITION.Position AS [POSITION], DEPARTMENT.Department AS [DEPARTMENT], " +
                "AGENCY.Agency AS [Agency], " +
                "LeaveTransaction.NumberOfDays, LeaveTransaction.DepartmentHeadApproval, " +
                "LeaveTransaction.HRApproval," +
                "LeaveTransaction.FromDate," +
                "LeaveTransaction.ToDate, " +
                "LeaveTransaction.FiledDate, " +
                "LeaveType.LeaveName " +
                "FROM Memberships, EMPLOYEE, POSITION, DEPARTMENT, UsersInRoles, Roles, Agency, " +
                "LeaveTransaction, LeaveTypeUser, LeaveType WHERE " +
                "LeaveTransaction.UserId = Memberships.UserId AND " +
                "Memberships.UserId = EMPLOYEE.UserId AND " +
                "EMPLOYEE.PositionId = POSITION.Id AND " +
                "POSITION.DepartmentId = DEPARTMENT.Id AND " +
                "EMPLOYEE.UserId = UsersInRoles.UserId AND " +
                "UsersInRoles.RoleId = Roles.RoleId AND " +
                "(Roles.RoleName = 'Supervisor' OR Roles.RoleName = 'Staff') AND " +
                "EMPLOYEE.AgencyId = AGENCY.Id AND " +
                "Roles.RoleName != 'Admin' AND " +
                "EMPLOYEE.AccountStatusId = 1 " +
                "AND DEPARTMENT.Id = @DepartmentId " +
                "AND LeaveTransaction.LeaveTypeUserId = LeaveTypeUser.Id " +
                "AND LeaveTypeUser.LeaveTypeId = LeaveType.Id " +
                "AND LeaveTransaction.DepartmentHeadApproval IS NULL " +
                "AND LeaveTransaction.HRApproval IS NULL " +
                "ORDER BY LeaveTransaction.Id DESC";

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

        //for manager only
        public DataTable DisplayLeaveApproval_Mang(string deptId, string status)
        {
            strSql = "SELECT LeaveTransaction.Id, LeaveTransaction.UserId, EMPLOYEE.Emp_Id, " +
                "(EMPLOYEE.LastName + ', ' + EMPLOYEE.FirstName + ' ' + EMPLOYEE.MiddleName) AS FullName, " +
                "POSITION.Position AS [POSITION], DEPARTMENT.Department AS [DEPARTMENT], " +
                "AGENCY.Agency AS [Agency], " +
                "LeaveTransaction.NumberOfDays, LeaveTransaction.DepartmentHeadApproval, " +
                "LeaveTransaction.HRApproval," +
                "LeaveTransaction.FromDate," +
                "LeaveTransaction.ToDate, " +
                "LeaveTransaction.FiledDate, " +
                "LeaveType.LeaveName " +
                "FROM Memberships, EMPLOYEE, POSITION, DEPARTMENT, UsersInRoles, Roles, Agency, " +
                "LeaveTransaction, LeaveTypeUser, LeaveType WHERE " +
                "LeaveTransaction.UserId = Memberships.UserId AND " +
                "Memberships.UserId = EMPLOYEE.UserId AND " +
                "EMPLOYEE.PositionId = POSITION.Id AND " +
                "POSITION.DepartmentId = DEPARTMENT.Id AND " +
                "EMPLOYEE.UserId = UsersInRoles.UserId AND " +
                "UsersInRoles.RoleId = Roles.RoleId AND " +
                "(Roles.RoleName = 'Supervisor' OR Roles.RoleName = 'Staff') AND " +
                "EMPLOYEE.AgencyId = AGENCY.Id AND " +
                "Roles.RoleName != 'Admin' AND " +
                "EMPLOYEE.AccountStatusId = 1 " +
                "AND DEPARTMENT.Id = @DepartmentId " +
                "AND LeaveTransaction.LeaveTypeUserId = LeaveTypeUser.Id " +
                "AND LeaveTypeUser.LeaveTypeId = LeaveType.Id " +
                "AND LeaveTransaction.Status = @Status " +
                "ORDER BY LeaveTransaction.Id DESC";

            conn = new SqlConnection();
            conn.ConnectionString = WebConfigurationManager.ConnectionStrings["dbAMS"].ConnectionString;
            comm = new SqlCommand(strSql, conn);
            comm.Parameters.AddWithValue("@DepartmentId", deptId);
            comm.Parameters.AddWithValue("@Status", status);
            dt = new DataTable();
            adp = new SqlDataAdapter(comm);

            conn.Open();
            adp.Fill(dt);
            conn.Close();

            return dt;
        }

        //for manager only
        public DataTable DisplayLeaveApproval_Mang(string deptId, string status, string startDate)
        {
            strSql = "SELECT LeaveTransaction.Id, LeaveTransaction.UserId, EMPLOYEE.Emp_Id, " +
                "(EMPLOYEE.LastName + ', ' + EMPLOYEE.FirstName + ' ' + EMPLOYEE.MiddleName) AS FullName, " +
                "POSITION.Position AS [POSITION], DEPARTMENT.Department AS [DEPARTMENT], " +
                "AGENCY.Agency AS [Agency], " +
                "LeaveTransaction.NumberOfDays, LeaveTransaction.DepartmentHeadApproval, " +
                "LeaveTransaction.HRApproval," +
                "LeaveTransaction.FromDate," +
                "LeaveTransaction.ToDate, " +
                "LeaveTransaction.FiledDate, " +
                "LeaveType.LeaveName " +
                "FROM Memberships, EMPLOYEE, POSITION, DEPARTMENT, UsersInRoles, Roles, Agency, " +
                "LeaveTransaction, LeaveTypeUser, LeaveType WHERE " +
                "LeaveTransaction.UserId = Memberships.UserId AND " +
                "Memberships.UserId = EMPLOYEE.UserId AND " +
                "EMPLOYEE.PositionId = POSITION.Id AND " +
                "POSITION.DepartmentId = DEPARTMENT.Id AND " +
                "EMPLOYEE.UserId = UsersInRoles.UserId AND " +
                "UsersInRoles.RoleId = Roles.RoleId AND " +
                "(Roles.RoleName = 'Supervisor' OR Roles.RoleName = 'Staff') AND " +
                "EMPLOYEE.AgencyId = AGENCY.Id AND " +
                "Roles.RoleName != 'Admin' AND " +
                "EMPLOYEE.AccountStatusId = 1 " +
                "AND DEPARTMENT.Id = @DepartmentId " +
                "AND LeaveTransaction.LeaveTypeUserId = LeaveTypeUser.Id " +
                "AND LeaveTypeUser.LeaveTypeId = LeaveType.Id " +
                "AND LeaveTransaction.Status = @Status " +
                "AND (LeaveTransaction.FiledDate = @StartDate OR LeaveTransaction.FromDate = @StartDate OR LeaveTransaction.ToDate = @StartDate) " + 
                "ORDER BY LeaveTransaction.Id DESC";

            conn = new SqlConnection();
            conn.ConnectionString = WebConfigurationManager.ConnectionStrings["dbAMS"].ConnectionString;
            comm = new SqlCommand(strSql, conn);
            comm.Parameters.AddWithValue("@DepartmentId", deptId);
            comm.Parameters.AddWithValue("@Status", status);
            comm.Parameters.AddWithValue("@StartDate", Convert.ToDateTime(startDate));
            dt = new DataTable();
            adp = new SqlDataAdapter(comm);

            conn.Open();
            adp.Fill(dt);
            conn.Close();

            return dt;
        }

        //for manager only
        public DataTable DisplayLeaveApproval_Mang(string deptId, string status, string startDate, string endDate)
        {
            strSql = "SELECT LeaveTransaction.Id, LeaveTransaction.UserId, EMPLOYEE.Emp_Id, " +
                "(EMPLOYEE.LastName + ', ' + EMPLOYEE.FirstName + ' ' + EMPLOYEE.MiddleName) AS FullName, " +
                "POSITION.Position AS [POSITION], DEPARTMENT.Department AS [DEPARTMENT], " +
                "AGENCY.Agency AS [Agency], " +
                "LeaveTransaction.NumberOfDays, LeaveTransaction.DepartmentHeadApproval, " +
                "LeaveTransaction.HRApproval," +
                "LeaveTransaction.FromDate," +
                "LeaveTransaction.ToDate, " +
                "LeaveTransaction.FiledDate, " +
                "LeaveType.LeaveName " +
                "FROM Memberships, EMPLOYEE, POSITION, DEPARTMENT, UsersInRoles, Roles, Agency, " +
                "LeaveTransaction, LeaveTypeUser, LeaveType WHERE " +
                "LeaveTransaction.UserId = Memberships.UserId AND " +
                "Memberships.UserId = EMPLOYEE.UserId AND " +
                "EMPLOYEE.PositionId = POSITION.Id AND " +
                "POSITION.DepartmentId = DEPARTMENT.Id AND " +
                "EMPLOYEE.UserId = UsersInRoles.UserId AND " +
                "UsersInRoles.RoleId = Roles.RoleId AND " +
                "(Roles.RoleName = 'Supervisor' OR Roles.RoleName = 'Staff') AND " +
                "EMPLOYEE.AgencyId = AGENCY.Id AND " +
                "Roles.RoleName != 'Admin' AND " +
                "EMPLOYEE.AccountStatusId = 1 " +
                "AND DEPARTMENT.Id = @DepartmentId " +
                "AND LeaveTransaction.LeaveTypeUserId = LeaveTypeUser.Id " +
                "AND LeaveTypeUser.LeaveTypeId = LeaveType.Id " +
                "AND LeaveTransaction.Status = @Status " +
                "AND (LeaveTransaction.FiledDate = @StartDate OR LeaveTransaction.FromDate = @StartDate OR LeaveTransaction.ToDate = @StartDate) " +
                "ORDER BY LeaveTransaction.Id DESC";

            conn = new SqlConnection();
            conn.ConnectionString = WebConfigurationManager.ConnectionStrings["dbAMS"].ConnectionString;
            comm = new SqlCommand(strSql, conn);
            comm.Parameters.AddWithValue("@DepartmentId", deptId);
            comm.Parameters.AddWithValue("@Status", status);
            comm.Parameters.AddWithValue("@StartDate", Convert.ToDateTime(startDate));
            dt = new DataTable();
            adp = new SqlDataAdapter(comm);

            conn.Open();
            adp.Fill(dt);
            conn.Close();

            return dt;
        }

        //for sup
        public DataTable DisplayPendingLeaveApproval_Sup(string deptId)
        {
            strSql = "SELECT LeaveTransaction.Id, LeaveTransaction.UserId, EMPLOYEE.Emp_Id, " +
                "(EMPLOYEE.LastName + ', ' + EMPLOYEE.FirstName + ' ' + EMPLOYEE.MiddleName) AS FullName, " +
                "POSITION.Position AS [POSITION], DEPARTMENT.Department AS [DEPARTMENT], " +
                "AGENCY.Agency AS [Agency], " +
                "LeaveTransaction.NumberOfDays, LeaveTransaction.DepartmentHeadApproval, " +
                "LeaveTransaction.HRApproval," +
                "LeaveTransaction.FromDate," +
                "LeaveTransaction.ToDate, " +
                "LeaveTransaction.FiledDate, " +
                "LeaveType.LeaveName " +
                "FROM Memberships, EMPLOYEE, POSITION, DEPARTMENT, UsersInRoles, Roles, Agency, " +
                "LeaveTransaction, LeaveTypeUser, LeaveType WHERE " +
                "LeaveTransaction.UserId = Memberships.UserId AND " +
                "Memberships.UserId = EMPLOYEE.UserId AND " +
                "EMPLOYEE.PositionId = POSITION.Id AND " +
                "POSITION.DepartmentId = DEPARTMENT.Id AND " +
                "EMPLOYEE.UserId = UsersInRoles.UserId AND " +
                "UsersInRoles.RoleId = Roles.RoleId AND " +
                "Roles.RoleName = 'Staff' AND " +
                "EMPLOYEE.AgencyId = AGENCY.Id AND " +
                "Roles.RoleName != 'Admin' AND " +
                "EMPLOYEE.AccountStatusId = 1 " +
                "AND DEPARTMENT.Id = @DepartmentId " +
                "AND LeaveTransaction.LeaveTypeUserId = LeaveTypeUser.Id " +
                "AND LeaveTypeUser.LeaveTypeId = LeaveType.Id " +
                "AND LeaveTransaction.DepartmentHeadApproval IS NULL " +
                "AND LeaveTransaction.HRApproval IS NULL " +
                "ORDER BY LeaveTransaction.Id DESC";

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

        //for sup
        public DataTable DisplayLeaveApproval_Sup(string deptId, string status)
        {
            strSql = "SELECT LeaveTransaction.Id, LeaveTransaction.UserId, EMPLOYEE.Emp_Id, " +
                "(EMPLOYEE.LastName + ', ' + EMPLOYEE.FirstName + ' ' + EMPLOYEE.MiddleName) AS FullName, " +
                "POSITION.Position AS [POSITION], DEPARTMENT.Department AS [DEPARTMENT], " +
                "AGENCY.Agency AS [Agency], " +
                "LeaveTransaction.NumberOfDays, LeaveTransaction.DepartmentHeadApproval, " +
                "LeaveTransaction.HRApproval," +
                "LeaveTransaction.FromDate," +
                "LeaveTransaction.ToDate, " +
                "LeaveTransaction.FiledDate, " +
                "LeaveType.LeaveName " +
                "FROM Memberships, EMPLOYEE, POSITION, DEPARTMENT, UsersInRoles, Roles, Agency, " +
                "LeaveTransaction, LeaveTypeUser, LeaveType WHERE " +
                "LeaveTransaction.UserId = Memberships.UserId AND " +
                "Memberships.UserId = EMPLOYEE.UserId AND " +
                "EMPLOYEE.PositionId = POSITION.Id AND " +
                "POSITION.DepartmentId = DEPARTMENT.Id AND " +
                "EMPLOYEE.UserId = UsersInRoles.UserId AND " +
                "UsersInRoles.RoleId = Roles.RoleId AND " +
                "Roles.RoleName = 'Staff' AND " +
                "EMPLOYEE.AgencyId = AGENCY.Id AND " +
                "Roles.RoleName != 'Admin' AND " +
                "EMPLOYEE.AccountStatusId = 1 " +
                "AND DEPARTMENT.Id = @DepartmentId " +
                "AND LeaveTransaction.LeaveTypeUserId = LeaveTypeUser.Id " +
                "AND LeaveTypeUser.LeaveTypeId = LeaveType.Id " +
                "AND LeaveTransaction.Status = @Status " +
                "ORDER BY LeaveTransaction.Id DESC";

            conn = new SqlConnection();
            conn.ConnectionString = WebConfigurationManager.ConnectionStrings["dbAMS"].ConnectionString;
            comm = new SqlCommand(strSql, conn);
            comm.Parameters.AddWithValue("@DepartmentId", deptId);
            comm.Parameters.AddWithValue("@Status", status);
            dt = new DataTable();
            adp = new SqlDataAdapter(comm);

            conn.Open();
            adp.Fill(dt);
            conn.Close();

            return dt;
        }

        //for sup
        public DataTable DisplayLeaveApproval_Sup(string deptId, string status, string startDate)
        {
            strSql = "SELECT LeaveTransaction.Id, LeaveTransaction.UserId, EMPLOYEE.Emp_Id, " +
                "(EMPLOYEE.LastName + ', ' + EMPLOYEE.FirstName + ' ' + EMPLOYEE.MiddleName) AS FullName, " +
                "POSITION.Position AS [POSITION], DEPARTMENT.Department AS [DEPARTMENT], " +
                "AGENCY.Agency AS [Agency], " +
                "LeaveTransaction.NumberOfDays, LeaveTransaction.DepartmentHeadApproval, " +
                "LeaveTransaction.HRApproval," +
                "LeaveTransaction.FromDate," +
                "LeaveTransaction.ToDate, " +
                "LeaveTransaction.FiledDate, " +
                "LeaveType.LeaveName " +
                "FROM Memberships, EMPLOYEE, POSITION, DEPARTMENT, UsersInRoles, Roles, Agency, " +
                "LeaveTransaction, LeaveTypeUser, LeaveType WHERE " +
                "LeaveTransaction.UserId = Memberships.UserId AND " +
                "Memberships.UserId = EMPLOYEE.UserId AND " +
                "EMPLOYEE.PositionId = POSITION.Id AND " +
                "POSITION.DepartmentId = DEPARTMENT.Id AND " +
                "EMPLOYEE.UserId = UsersInRoles.UserId AND " +
                "UsersInRoles.RoleId = Roles.RoleId AND " +
                "Roles.RoleName = 'Staff' AND " +
                "EMPLOYEE.AgencyId = AGENCY.Id AND " +
                "Roles.RoleName != 'Admin' AND " +
                "EMPLOYEE.AccountStatusId = 1 " +
                "AND DEPARTMENT.Id = @DepartmentId " +
                "AND LeaveTransaction.LeaveTypeUserId = LeaveTypeUser.Id " +
                "AND LeaveTypeUser.LeaveTypeId = LeaveType.Id " +
                "AND LeaveTransaction.Status = @Status " +
                "AND (LeaveTransaction.FiledDate = @StartDate OR LeaveTransaction.FromDate = @StartDate OR LeaveTransaction.ToDate = @StartDate) " + 
                "ORDER BY LeaveTransaction.Id DESC";

            conn = new SqlConnection();
            conn.ConnectionString = WebConfigurationManager.ConnectionStrings["dbAMS"].ConnectionString;
            comm = new SqlCommand(strSql, conn);
            comm.Parameters.AddWithValue("@DepartmentId", deptId);
            comm.Parameters.AddWithValue("@Status", status);
            comm.Parameters.AddWithValue("@StartDate", Convert.ToDateTime(startDate));
            dt = new DataTable();
            adp = new SqlDataAdapter(comm);

            conn.Open();
            adp.Fill(dt);
            conn.Close();

            return dt;
        }

        //for sup
        public DataTable DisplayLeaveApproval_Sup(string deptId, string status, string startDate, string endDate)
        {
            strSql = "SELECT LeaveTransaction.Id, LeaveTransaction.UserId, EMPLOYEE.Emp_Id, " +
                "(EMPLOYEE.LastName + ', ' + EMPLOYEE.FirstName + ' ' + EMPLOYEE.MiddleName) AS FullName, " +
                "POSITION.Position AS [POSITION], DEPARTMENT.Department AS [DEPARTMENT], " +
                "AGENCY.Agency AS [Agency], " +
                "LeaveTransaction.NumberOfDays, LeaveTransaction.DepartmentHeadApproval, " +
                "LeaveTransaction.HRApproval," +
                "LeaveTransaction.FromDate," +
                "LeaveTransaction.ToDate, " +
                "LeaveTransaction.FiledDate, " +
                "LeaveType.LeaveName " +
                "FROM Memberships, EMPLOYEE, POSITION, DEPARTMENT, UsersInRoles, Roles, Agency, " +
                "LeaveTransaction, LeaveTypeUser, LeaveType WHERE " +
                "LeaveTransaction.UserId = Memberships.UserId AND " +
                "Memberships.UserId = EMPLOYEE.UserId AND " +
                "EMPLOYEE.PositionId = POSITION.Id AND " +
                "POSITION.DepartmentId = DEPARTMENT.Id AND " +
                "EMPLOYEE.UserId = UsersInRoles.UserId AND " +
                "UsersInRoles.RoleId = Roles.RoleId AND " +
                "Roles.RoleName = 'Staff' AND " +
                "EMPLOYEE.AgencyId = AGENCY.Id AND " +
                "Roles.RoleName != 'Admin' AND " +
                "EMPLOYEE.AccountStatusId = 1 " +
                "AND DEPARTMENT.Id = @DepartmentId " +
                "AND LeaveTransaction.LeaveTypeUserId = LeaveTypeUser.Id " +
                "AND LeaveTypeUser.LeaveTypeId = LeaveType.Id " +
                "AND LeaveTransaction.Status = @Status " +
                "AND (LeaveTransaction.FiledDate = @StartDate OR LeaveTransaction.FromDate = @StartDate OR LeaveTransaction.ToDate = @StartDate) " +
                "ORDER BY LeaveTransaction.Id DESC";

            conn = new SqlConnection();
            conn.ConnectionString = WebConfigurationManager.ConnectionStrings["dbAMS"].ConnectionString;
            comm = new SqlCommand(strSql, conn);
            comm.Parameters.AddWithValue("@DepartmentId", deptId);
            comm.Parameters.AddWithValue("@Status", status);
            comm.Parameters.AddWithValue("@StartDate", Convert.ToDateTime(startDate));
            dt = new DataTable();
            adp = new SqlDataAdapter(comm);

            conn.Open();
            adp.Fill(dt);
            conn.Close();

            return dt;
        }

        public void approve_Pending_leave(bool deptHead, bool Hr, string rowId, string approvalDesc)
        {
            if(deptHead == true)
            {
                strSql = "UPDATE LeaveTransaction SET DepartmentHeadApproval = @Approval WHERE Id = @Id";
                conn = new SqlConnection();
                conn.ConnectionString = WebConfigurationManager.ConnectionStrings["dbAMS"].ConnectionString;
                using (comm = new SqlCommand(strSql, conn))
                {
                    conn.Open();
                    comm.Parameters.AddWithValue("@Id", rowId);
                    comm.Parameters.AddWithValue("@Approval", approvalDesc);
                    comm.ExecuteNonQuery();
                    conn.Close();
                }
                comm.Dispose();
                conn.Dispose();
            }

            if (Hr == true)
            {
                strSql = "UPDATE LeaveTransaction SET HRApproval = @Approval, Status=@Status WHERE Id = @Id";
                conn = new SqlConnection();
                conn.ConnectionString = WebConfigurationManager.ConnectionStrings["dbAMS"].ConnectionString;
                using (comm = new SqlCommand(strSql, conn))
                {
                    conn.Open();
                    comm.Parameters.AddWithValue("@Id", rowId);
                    comm.Parameters.AddWithValue("@Approval", approvalDesc);
                    comm.Parameters.AddWithValue("@Status", approvalDesc);
                    comm.ExecuteNonQuery();
                    conn.Close();
                }
                comm.Dispose();
                conn.Dispose();
            }
        }
        #endregion


        #region ApprovedLeave_approvals

        public DataTable DisplayApprovedLeaveApproval()
        {
            strSql = "SELECT LeaveTransaction.Id, LeaveTransaction.UserId, EMPLOYEE.Emp_Id, " +
                "(EMPLOYEE.LastName + ', ' + EMPLOYEE.FirstName + ' ' + EMPLOYEE.MiddleName) AS FullName, " +
                "POSITION.Position AS [POSITION], DEPARTMENT.Department AS [DEPARTMENT], " +
                "AGENCY.Agency AS [Agency], " +
                "LeaveTransaction.NumberOfDays, LeaveTransaction.DepartmentHeadApproval, " +
                "LeaveTransaction.HRApproval," +
                "LeaveTransaction.FromDate," +
                "LeaveTransaction.ToDate, " +
                "LeaveTransaction.FiledDate, " +
                "LeaveType.LeaveName " +
                "FROM Memberships, EMPLOYEE, POSITION, DEPARTMENT, UsersInRoles, Roles, Agency, " +
                "LeaveTransaction, LeaveTypeUser, LeaveType WHERE " +
                "LeaveTransaction.UserId = Memberships.UserId AND " +
                "Memberships.UserId = EMPLOYEE.UserId AND " +
                "EMPLOYEE.PositionId = POSITION.Id AND " +
                "POSITION.DepartmentId = DEPARTMENT.Id AND " +
                "EMPLOYEE.UserId = UsersInRoles.UserId AND " +
                "UsersInRoles.RoleId = Roles.RoleId AND " +
                "EMPLOYEE.AgencyId = AGENCY.Id AND " +
                "Roles.RoleName != 'Admin' AND " +
                "EMPLOYEE.AccountStatusId = 1 " +
                "AND LeaveTransaction.LeaveTypeUserId = LeaveTypeUser.Id " +
                "AND LeaveTypeUser.LeaveTypeId = LeaveType.Id " +
                "AND LeaveTransaction.DepartmentHeadApproval = 'Approved' " +
                "AND LeaveTransaction.HRApproval = 'Approved' " +
                "ORDER BY LeaveTransaction.Id DESC";

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

        //for hr manager only
        public DataTable DisplayApprovedLeaveApproval_HR()
        {
            strSql = "SELECT LeaveTransaction.Id, LeaveTransaction.UserId, EMPLOYEE.Emp_Id, " +
                "(EMPLOYEE.LastName + ', ' + EMPLOYEE.FirstName + ' ' + EMPLOYEE.MiddleName) AS FullName, " +
                "POSITION.Position AS [POSITION], DEPARTMENT.Department AS [DEPARTMENT], " +
                "AGENCY.Agency AS [Agency], " +
                "LeaveTransaction.NumberOfDays, LeaveTransaction.DepartmentHeadApproval, " +
                "LeaveTransaction.HRApproval," +
                "LeaveTransaction.FromDate," +
                "LeaveTransaction.ToDate, " +
                "LeaveTransaction.FiledDate, " +
                "LeaveType.LeaveName " +
                "FROM Memberships, EMPLOYEE, POSITION, DEPARTMENT, UsersInRoles, Roles, Agency, " +
                "LeaveTransaction, LeaveTypeUser, LeaveType WHERE " +
                "LeaveTransaction.UserId = Memberships.UserId AND " +
                "Memberships.UserId = EMPLOYEE.UserId AND " +
                "EMPLOYEE.PositionId = POSITION.Id AND " +
                "POSITION.DepartmentId = DEPARTMENT.Id AND " +
                "EMPLOYEE.UserId = UsersInRoles.UserId AND " +
                "UsersInRoles.RoleId = Roles.RoleId AND " +
                "EMPLOYEE.AgencyId = AGENCY.Id AND " +
                "Roles.RoleName != 'Admin' AND " +
                "EMPLOYEE.AccountStatusId = 1 " +
                "AND LeaveTransaction.LeaveTypeUserId = LeaveTypeUser.Id " +
                "AND LeaveTypeUser.LeaveTypeId = LeaveType.Id " +
                "AND LeaveTransaction.DepartmentHeadApproval = 'Approved' " +
                "AND LeaveTransaction.HRApproval = 'Approved' " +
                "ORDER BY LeaveTransaction.Id DESC";

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

        public DataTable DisplayApprovedLeaveApproval_Mang(string deptId)
        {
            strSql = "SELECT LeaveTransaction.Id, LeaveTransaction.UserId, EMPLOYEE.Emp_Id, " +
                "(EMPLOYEE.LastName + ', ' + EMPLOYEE.FirstName + ' ' + EMPLOYEE.MiddleName) AS FullName, " +
                "POSITION.Position AS [POSITION], DEPARTMENT.Department AS [DEPARTMENT], " +
                "AGENCY.Agency AS [Agency], " +
                "LeaveTransaction.NumberOfDays, LeaveTransaction.DepartmentHeadApproval, " +
                "LeaveTransaction.HRApproval," +
                "LeaveTransaction.FromDate," +
                "LeaveTransaction.ToDate, " +
                "LeaveTransaction.FiledDate, " +
                "LeaveType.LeaveName " +
                "FROM Memberships, EMPLOYEE, POSITION, DEPARTMENT, UsersInRoles, Roles, Agency, " +
                "LeaveTransaction, LeaveTypeUser, LeaveType WHERE " +
                "LeaveTransaction.UserId = Memberships.UserId AND " +
                "Memberships.UserId = EMPLOYEE.UserId AND " +
                "EMPLOYEE.PositionId = POSITION.Id AND " +
                "POSITION.DepartmentId = DEPARTMENT.Id AND " +
                "EMPLOYEE.UserId = UsersInRoles.UserId AND " +
                "UsersInRoles.RoleId = Roles.RoleId AND " +
                "(Roles.RoleName = 'Supervisor' OR Roles.RoleName = 'Staff') AND " +
                "EMPLOYEE.AgencyId = AGENCY.Id AND " +
                "Roles.RoleName != 'Admin' AND " +
                "EMPLOYEE.AccountStatusId = 1 " +
                "AND DEPARTMENT.Id = @DepartmentId " +
                "AND LeaveTransaction.LeaveTypeUserId = LeaveTypeUser.Id " +
                "AND LeaveTypeUser.LeaveTypeId = LeaveType.Id " +
                "AND LeaveTransaction.DepartmentHeadApproval = 'Approved' " +
                "AND LeaveTransaction.HRApproval = 'Approved' " +
                "ORDER BY LeaveTransaction.Id DESC";

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

        public DataTable DisplayApprovedLeaveApproval_Sup(string deptId)
        {
            strSql = "SELECT LeaveTransaction.Id, LeaveTransaction.UserId, EMPLOYEE.Emp_Id, " +
                "(EMPLOYEE.LastName + ', ' + EMPLOYEE.FirstName + ' ' + EMPLOYEE.MiddleName) AS FullName, " +
                "POSITION.Position AS [POSITION], DEPARTMENT.Department AS [DEPARTMENT], " +
                "AGENCY.Agency AS [Agency], " +
                "LeaveTransaction.NumberOfDays, LeaveTransaction.DepartmentHeadApproval, " +
                "LeaveTransaction.HRApproval," +
                "LeaveTransaction.FromDate," +
                "LeaveTransaction.ToDate, " +
                "LeaveTransaction.FiledDate, " +
                "LeaveType.LeaveName " +
                "FROM Memberships, EMPLOYEE, POSITION, DEPARTMENT, UsersInRoles, Roles, Agency, " +
                "LeaveTransaction, LeaveTypeUser, LeaveType WHERE " +
                "LeaveTransaction.UserId = Memberships.UserId AND " +
                "Memberships.UserId = EMPLOYEE.UserId AND " +
                "EMPLOYEE.PositionId = POSITION.Id AND " +
                "POSITION.DepartmentId = DEPARTMENT.Id AND " +
                "EMPLOYEE.UserId = UsersInRoles.UserId AND " +
                "UsersInRoles.RoleId = Roles.RoleId AND " +
                "Roles.RoleName = 'Staff' AND " +
                "EMPLOYEE.AgencyId = AGENCY.Id AND " +
                "Roles.RoleName != 'Admin' AND " +
                "EMPLOYEE.AccountStatusId = 1 " +
                "AND DEPARTMENT.Id = @DepartmentId " +
                "AND LeaveTransaction.LeaveTypeUserId = LeaveTypeUser.Id " +
                "AND LeaveTypeUser.LeaveTypeId = LeaveType.Id " +
                "AND LeaveTransaction.DepartmentHeadApproval = 'Approved' " +
                "AND LeaveTransaction.HRApproval = 'Approved' " +
                "ORDER BY LeaveTransaction.Id DESC";

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
        #endregion

        #region DisApproveLeaves
        public DataTable DisplayRejectedLeaveApproval()
        {
            strSql = "SELECT LeaveTransaction.Id, LeaveTransaction.UserId, EMPLOYEE.Emp_Id, " +
                "(EMPLOYEE.LastName + ', ' + EMPLOYEE.FirstName + ' ' + EMPLOYEE.MiddleName) AS FullName, " +
                "POSITION.Position AS [POSITION], DEPARTMENT.Department AS [DEPARTMENT], " +
                "AGENCY.Agency AS [Agency], " +
                "LeaveTransaction.NumberOfDays, LeaveTransaction.DepartmentHeadApproval, " +
                "LeaveTransaction.HRApproval," +
                "LeaveTransaction.FromDate," +
                "LeaveTransaction.ToDate, " +
                "LeaveTransaction.FiledDate, " +
                "LeaveType.LeaveName " +
                "FROM Memberships, EMPLOYEE, POSITION, DEPARTMENT, UsersInRoles, Roles, Agency, " +
                "LeaveTransaction, LeaveTypeUser, LeaveType WHERE " +
                "LeaveTransaction.UserId = Memberships.UserId AND " +
                "Memberships.UserId = EMPLOYEE.UserId AND " +
                "EMPLOYEE.PositionId = POSITION.Id AND " +
                "POSITION.DepartmentId = DEPARTMENT.Id AND " +
                "EMPLOYEE.UserId = UsersInRoles.UserId AND " +
                "UsersInRoles.RoleId = Roles.RoleId AND " +
                "EMPLOYEE.AgencyId = AGENCY.Id AND " +
                "Roles.RoleName != 'Admin' AND " +
                "EMPLOYEE.AccountStatusId = 1 " +
                "AND LeaveTransaction.LeaveTypeUserId = LeaveTypeUser.Id " +
                "AND LeaveTypeUser.LeaveTypeId = LeaveType.Id " +
                "AND (LeaveTransaction.DepartmentHeadApproval = 'Disapproved' " +
                "OR LeaveTransaction.HRApproval = 'Disapproved') " +
                "ORDER BY LeaveTransaction.Id DESC";

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

        //for hr manager only
        public DataTable DisplayRejectedLeaveApproval_HR()
        {
            strSql = "SELECT LeaveTransaction.Id, LeaveTransaction.UserId, EMPLOYEE.Emp_Id, " +
                "(EMPLOYEE.LastName + ', ' + EMPLOYEE.FirstName + ' ' + EMPLOYEE.MiddleName) AS FullName, " +
                "POSITION.Position AS [POSITION], DEPARTMENT.Department AS [DEPARTMENT], " +
                "AGENCY.Agency AS [Agency], " +
                "LeaveTransaction.NumberOfDays, LeaveTransaction.DepartmentHeadApproval, " +
                "LeaveTransaction.HRApproval," +
                "LeaveTransaction.FromDate," +
                "LeaveTransaction.ToDate, " +
                "LeaveTransaction.FiledDate, " +
                "LeaveType.LeaveName " +
                "FROM Memberships, EMPLOYEE, POSITION, DEPARTMENT, UsersInRoles, Roles, Agency, " +
                "LeaveTransaction, LeaveTypeUser, LeaveType WHERE " +
                "LeaveTransaction.UserId = Memberships.UserId AND " +
                "Memberships.UserId = EMPLOYEE.UserId AND " +
                "EMPLOYEE.PositionId = POSITION.Id AND " +
                "POSITION.DepartmentId = DEPARTMENT.Id AND " +
                "EMPLOYEE.UserId = UsersInRoles.UserId AND " +
                "UsersInRoles.RoleId = Roles.RoleId AND " +
                "EMPLOYEE.AgencyId = AGENCY.Id AND " +
                "Roles.RoleName != 'Admin' AND " +
                "EMPLOYEE.AccountStatusId = 1 " +
                "AND LeaveTransaction.LeaveTypeUserId = LeaveTypeUser.Id " +
                "AND LeaveTypeUser.LeaveTypeId = LeaveType.Id " +
                "AND (LeaveTransaction.DepartmentHeadApproval = 'Disapproved' " +
                "OR LeaveTransaction.HRApproval = 'Disapproved') " +
                "ORDER BY LeaveTransaction.Id DESC";

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

        public DataTable DisplayRejectedLeaveApproval_Mang(string deptId)
        {
            strSql = "SELECT LeaveTransaction.Id, LeaveTransaction.UserId, EMPLOYEE.Emp_Id, " +
                "(EMPLOYEE.LastName + ', ' + EMPLOYEE.FirstName + ' ' + EMPLOYEE.MiddleName) AS FullName, " +
                "POSITION.Position AS [POSITION], DEPARTMENT.Department AS [DEPARTMENT], " +
                "AGENCY.Agency AS [Agency], " +
                "LeaveTransaction.NumberOfDays, LeaveTransaction.DepartmentHeadApproval, " +
                "LeaveTransaction.HRApproval," +
                "LeaveTransaction.FromDate," +
                "LeaveTransaction.ToDate, " +
                "LeaveTransaction.FiledDate, " +
                "LeaveType.LeaveName " +
                "FROM Memberships, EMPLOYEE, POSITION, DEPARTMENT, UsersInRoles, Roles, Agency, " +
                "LeaveTransaction, LeaveTypeUser, LeaveType WHERE " +
                "LeaveTransaction.UserId = Memberships.UserId AND " +
                "Memberships.UserId = EMPLOYEE.UserId AND " +
                "EMPLOYEE.PositionId = POSITION.Id AND " +
                "POSITION.DepartmentId = DEPARTMENT.Id AND " +
                "EMPLOYEE.UserId = UsersInRoles.UserId AND " +
                "UsersInRoles.RoleId = Roles.RoleId AND " +
                "(Roles.RoleName = 'Supervisor' OR Roles.RoleName = 'Staff') AND " +
                "EMPLOYEE.AgencyId = AGENCY.Id AND " +
                "Roles.RoleName != 'Admin' AND " +
                "EMPLOYEE.AccountStatusId = 1 " +
                "AND DEPARTMENT.Id = @DepartmentId " +
                "AND LeaveTransaction.LeaveTypeUserId = LeaveTypeUser.Id " +
                "AND LeaveTypeUser.LeaveTypeId = LeaveType.Id " +
                "AND (LeaveTransaction.DepartmentHeadApproval = 'Disapproved' " +
                "OR LeaveTransaction.HRApproval = 'Disapproved') " +
                "ORDER BY LeaveTransaction.Id DESC";

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

        public DataTable DisplayRejectedLeaveApproval_Sup(string deptId)
        {
            strSql = "SELECT LeaveTransaction.Id, LeaveTransaction.UserId, EMPLOYEE.Emp_Id, " +
                "(EMPLOYEE.LastName + ', ' + EMPLOYEE.FirstName + ' ' + EMPLOYEE.MiddleName) AS FullName, " +
                "POSITION.Position AS [POSITION], DEPARTMENT.Department AS [DEPARTMENT], " +
                "AGENCY.Agency AS [Agency], " +
                "LeaveTransaction.NumberOfDays, LeaveTransaction.DepartmentHeadApproval, " +
                "LeaveTransaction.HRApproval," +
                "LeaveTransaction.FromDate," +
                "LeaveTransaction.ToDate, " +
                "LeaveTransaction.FiledDate, " +
                "LeaveType.LeaveName " +
                "FROM Memberships, EMPLOYEE, POSITION, DEPARTMENT, UsersInRoles, Roles, Agency, " +
                "LeaveTransaction, LeaveTypeUser, LeaveType WHERE " +
                "LeaveTransaction.UserId = Memberships.UserId AND " +
                "Memberships.UserId = EMPLOYEE.UserId AND " +
                "EMPLOYEE.PositionId = POSITION.Id AND " +
                "POSITION.DepartmentId = DEPARTMENT.Id AND " +
                "EMPLOYEE.UserId = UsersInRoles.UserId AND " +
                "UsersInRoles.RoleId = Roles.RoleId AND " +
                "Roles.RoleName = 'Staff' AND " +
                "EMPLOYEE.AgencyId = AGENCY.Id AND " +
                "Roles.RoleName != 'Admin' AND " +
                "EMPLOYEE.AccountStatusId = 1 " +
                "AND DEPARTMENT.Id = @DepartmentId " +
                "AND LeaveTransaction.LeaveTypeUserId = LeaveTypeUser.Id " +
                "AND LeaveTypeUser.LeaveTypeId = LeaveType.Id " +
                "AND (LeaveTransaction.DepartmentHeadApproval = 'Disapproved' " +
                "OR LeaveTransaction.HRApproval = 'Disapproved') " +
                "ORDER BY LeaveTransaction.Id DESC";

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
        #endregion

        public void subtract_valid_days(string NoOfDays, string rowId)
        {
            strSql = "UPDATE LeaveTypeUser SET " +
                "NoOfDays = (NoOfDays - @NoOfDays) " +
                "WHERE Id = @Id";

            conn = new SqlConnection();
            conn.ConnectionString = WebConfigurationManager.ConnectionStrings["dbAMS"].ConnectionString;

            using (comm = new SqlCommand(strSql, conn))
            {
                conn.Open();
                comm.Parameters.AddWithValue("@NoOfDays", NoOfDays);
                comm.Parameters.AddWithValue("@Id", rowId);
                comm.ExecuteNonQuery();
                conn.Close();
            }
            comm.Dispose();
            conn.Dispose();
        }

        public DataTable getuserId(string rowId)
        {
            strSql = "SELECT LeaveTypeUserId, UserId FROM LeaveTransaction WHERE Id = @Id";

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

        
    }
}
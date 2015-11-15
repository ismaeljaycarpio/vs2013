using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using System.Data.SqlClient;
using System.Data;

namespace AMS.DAL
{
    public class PositionManagement
    {
        //init
        SqlConnection conn;
        SqlCommand comm;
        SqlDataAdapter adp;
        DataTable dt;
        string strSql = "";


        //get managerial position
        public DataTable displayPositions(string strSearch)
        {
            strSql = "SELECT POSITION.Id, POSITION.Position, DEPARTMENT.Department, Roles.RoleName FROM POSITION, DEPARTMENT,Roles " +
                "WHERE DEPARTMENT.Id = POSITION.DepartmentId AND POSITION.RoleId = Roles.RoleId " +
                "AND (DEPARTMENT.Department LIKE '%' + @searchKeyWork + '%' OR POSITION.Position LIKE '%' + @searchKeyWork + '%')";

            conn = new SqlConnection();
            conn.ConnectionString = WebConfigurationManager.ConnectionStrings["dbAMS"].ConnectionString;
            comm = new SqlCommand(strSql, conn);
            comm.Parameters.AddWithValue("@searchKeyWork", strSearch);
            dt = new DataTable();
            adp = new SqlDataAdapter(comm);

            conn.Open();
            adp.Fill(dt);
            conn.Close();

            return dt;
        }

        public DataTable fillPositionByRoleId(Guid RoleId)
        {
            conn = new SqlConnection();
            conn.ConnectionString = WebConfigurationManager.ConnectionStrings["dbAMS"].ConnectionString;
            string strSql = "SELECT * FROM Position WHERE RoleId = @RoleId";
            comm = new SqlCommand(strSql, conn);
            comm.Parameters.AddWithValue("@RoleId", RoleId);
            dt = new DataTable();
            adp = new SqlDataAdapter(comm);

            conn.Open();
            adp.Fill(dt);
            conn.Close();

            return dt;
        }

        public DataTable getPositionByRowId(int rowId)
        {
            strSql = "SELECT * FROM POSITION WHERE Id = @Id";

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

        public string getDepartmentIdBypPosition(string PositionId)
        {
            strSql = "SELECT DepartmentId FROM POSITION WHERE Id = @Id";

            conn = new SqlConnection();
            conn.ConnectionString = WebConfigurationManager.ConnectionStrings["dbAMS"].ConnectionString;
            comm = new SqlCommand(strSql, conn);
            comm.Parameters.AddWithValue("@Id", PositionId);
            dt = new DataTable();
            adp = new SqlDataAdapter(comm);

            conn.Open();
            adp.Fill(dt);
            conn.Close();

            return dt.Rows[0]["DepartmentId"].ToString();
        }

        public void addPosition(
            string position,
            Guid RoleId,
            string deptId)
        {
            strSql = "INSERT INTO POSITION(Position,RoleId,DepartmentId) " +
                "VALUES(@Position,@RoleId,@DepartmentId) ";

            conn = new SqlConnection();
            conn.ConnectionString = WebConfigurationManager.ConnectionStrings["dbAMS"].ConnectionString;

            using (comm = new SqlCommand(strSql, conn))
            {
                conn.Open();
                comm.Parameters.AddWithValue("@Position", position);
                comm.Parameters.AddWithValue("@RoleId", RoleId);
                comm.Parameters.AddWithValue("@DepartmentId", deptId);

                comm.ExecuteNonQuery();
                conn.Close();
            }
            comm.Dispose();
            conn.Dispose();
        }

        public void updatePosition(
            string position,
            Guid RoleId,
            string deptId,
            string rowId)
        {
            strSql = "UPDATE POSITION SET Position=@Position, RoleId=@RoleId, DepartmentId=@DepartmentId WHERE Id=@RowId";

            conn = new SqlConnection();
            conn.ConnectionString = WebConfigurationManager.ConnectionStrings["dbAMS"].ConnectionString;

            using (comm = new SqlCommand(strSql, conn))
            {
                conn.Open();
                comm.Parameters.AddWithValue("@Position", position);
                comm.Parameters.AddWithValue("@RoleId", RoleId);
                comm.Parameters.AddWithValue("@DepartmentId", deptId);
                comm.Parameters.AddWithValue("@RowId", rowId);

                comm.ExecuteNonQuery();

                //update users in role


                conn.Close();
            }
            comm.Dispose();
            conn.Dispose();
        }
    }
}
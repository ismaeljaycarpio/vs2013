using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using System.Data.SqlClient;
using System.Data;


namespace AMS.DAL
{
    public class Department
    {
        //init
        SqlConnection conn;
        SqlCommand comm;
        SqlDataAdapter adp;
        DataTable dt;
        string strSql = "";

        public DataTable DisplayDepartment()
        {
            strSql = "SELECT * FROM DEPARTMENT";

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

        public DataTable GetDepartment(int rowId)
        {
            strSql = "SELECT * FROM DEPARTMENT WHERE Id = @Id";

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

        public void AddDepartment(
            string departmentName,
            string modifiedBy)
        {
            strSql = "INSERT INTO DEPARTMENT(Department,ModifiedBy) " +
                "VALUES(@Department,@ModifiedBy)";

            conn = new SqlConnection();
            conn.ConnectionString = WebConfigurationManager.ConnectionStrings["dbAMS"].ConnectionString;

            using (comm = new SqlCommand(strSql, conn))
            {
                conn.Open();
                comm.Parameters.AddWithValue("@Department", departmentName);
                comm.Parameters.AddWithValue("@ModifiedBy", modifiedBy);
                comm.ExecuteNonQuery();
                conn.Close();
            }
            comm.Dispose();
            conn.Dispose();
        }

        public void UpdateDepartment(
            string departmentName,
            string modifiedBy,
            string rowId)
        {
            strSql = "UPDATE DEPARTMENT SET " +
                "Department = @Department, " +
                "ModifiedDate = @ModifiedDate, " +
                "ModifiedBy = @ModifiedBy " +
                "WHERE Id = @RowId";

            conn = new SqlConnection();
            conn.ConnectionString = WebConfigurationManager.ConnectionStrings["dbAMS"].ConnectionString;

            using (comm = new SqlCommand(strSql, conn))
            {
                conn.Open();
                comm.Parameters.AddWithValue("@Department", departmentName);
                comm.Parameters.AddWithValue("@ModifiedDate", DateTime.Now);
                comm.Parameters.AddWithValue("@ModifiedBy", modifiedBy);
                comm.Parameters.AddWithValue("@RowId", rowId);

                comm.ExecuteNonQuery();
                conn.Close();
            }
            comm.Dispose();
            conn.Dispose();
        }

        public void DeleteDepartment(string rowId)
        {
            strSql = "DELETE FROM DEPARTMENT WHERE Id = @Id";

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

        public bool CheckIfDuplicate(string deptName)
        {
            strSql = "SELECT Department FROM DEPARTMENT WHERE Department = @Department";

            conn = new SqlConnection();
            conn.ConnectionString = WebConfigurationManager.ConnectionStrings["dbAMS"].ConnectionString;
            comm = new SqlCommand(strSql, conn);
            comm.Parameters.AddWithValue("@Department", deptName);
            dt = new DataTable();
            adp = new SqlDataAdapter(comm);

            conn.Open();
            adp.Fill(dt);
            conn.Close();

            if(dt.Rows.Count > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
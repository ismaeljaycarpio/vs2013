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
        public DataTable DisplayPositions(string strSearch)
        {
            strSql = "SELECT POSITION.Id, POSITION.Position, DEPARTMENT.Department " +
                "FROM POSITION INNER JOIN DEPARTMENT " +
                "ON DEPARTMENT.Id = POSITION.DepartmentId " +
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


        public DataTable GetPositionByRowId(int rowId)
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

        public string GetDepartmentIdBypPosition(string PositionId)
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

        public void AddPosition(
            string position,
            string deptId)
        {
            strSql = "INSERT INTO POSITION(Position,DepartmentId) " +
                "VALUES(@Position,@DepartmentId) ";

            conn = new SqlConnection();
            conn.ConnectionString = WebConfigurationManager.ConnectionStrings["dbAMS"].ConnectionString;

            using (comm = new SqlCommand(strSql, conn))
            {
                conn.Open();
                comm.Parameters.AddWithValue("@Position", position);
                comm.Parameters.AddWithValue("@DepartmentId", deptId);

                comm.ExecuteNonQuery();
                conn.Close();
            }
            comm.Dispose();
            conn.Dispose();
        }

        public void UpdatePosition(
            string position,
            string deptId,
            string rowId)
        {
            strSql = "UPDATE POSITION SET Position=@Position,DepartmentId=@DepartmentId WHERE Id=@RowId";

            conn = new SqlConnection();
            conn.ConnectionString = WebConfigurationManager.ConnectionStrings["dbAMS"].ConnectionString;

            using (comm = new SqlCommand(strSql, conn))
            {
                conn.Open();
                comm.Parameters.AddWithValue("@Position", position);
                comm.Parameters.AddWithValue("@DepartmentId", deptId);
                comm.Parameters.AddWithValue("@RowId", rowId);

                comm.ExecuteNonQuery();

                //update users in role


                conn.Close();
            }
            comm.Dispose();
            conn.Dispose();
        }

        public bool CheckIfDuplicate(string position)
        {
            strSql = "SELECT Position FROM POSITION WHERE Position = @Position";

            conn = new SqlConnection();
            conn.ConnectionString = WebConfigurationManager.ConnectionStrings["dbAMS"].ConnectionString;
            comm = new SqlCommand(strSql, conn);
            comm.Parameters.AddWithValue("@Position", position);
            dt = new DataTable();
            adp = new SqlDataAdapter(comm);

            conn.Open();
            adp.Fill(dt);
            conn.Close();

            if (dt.Rows.Count > 0)
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
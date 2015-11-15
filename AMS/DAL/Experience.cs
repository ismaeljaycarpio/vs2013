using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using System.Data.SqlClient;
using System.Data;

namespace AMS.DAL
{
    public class Experience
    {
        //init
        SqlConnection conn;
        SqlCommand comm;
        SqlDataAdapter adp;
        DataTable dt;
        string strSql = "";

        public DataTable displayExperience()
        {
            strSql = "SELECT * FROM EXPERIENCE";

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

        public DataTable getExperienceById(Guid UserId)
        {
            strSql = "SELECT * FROM EXPERIENCE WHERE UserId = @UserId";

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

        public DataTable getExperienceByRowId(int rowId)
        {
            strSql = "SELECT * FROM EXPERIENCE WHERE Id = @Id";

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

        
        public void addExperience(
            Guid UserId, 
            string company, 
            string job,
            string from, 
            string to, 
            string reason, 
            string description)
        {
            strSql = "INSERT INTO EXPERIENCE(UserId,Company,Job,FromDate,ToDate,Reason,Description) " +
                "VALUES(@UserId, @Company, @Job, @FromDate, @ToDate, @Reason, @Description)";

            conn = new SqlConnection();
            conn.ConnectionString = WebConfigurationManager.ConnectionStrings["dbAMS"].ConnectionString;

            using (comm = new SqlCommand(strSql, conn))
            {
                conn.Open();
                comm.Parameters.AddWithValue("@UserId", UserId);
                comm.Parameters.AddWithValue("@Company", company);
                comm.Parameters.AddWithValue("@Job", job);
                comm.Parameters.AddWithValue("@FromDate", from);
                comm.Parameters.AddWithValue("@ToDate", to);
                comm.Parameters.AddWithValue("@Reason", reason);
                comm.Parameters.AddWithValue("@Description", description);

                comm.ExecuteNonQuery();
                conn.Close();
            }
            comm.Dispose();
            conn.Dispose();
        }

        public void updateExperience(
            string company, 
            string job,
            string from, 
            string to, 
            string reason, 
            string description, 
            string rowId)
        {
            strSql = "UPDATE EXPERIENCE SET Company = @Company, " +
                "Job = @Job, [FromDate] = @FromDate, [ToDate] = @ToDate, Reason = @Reason, Description = @Description " +
                "WHERE Id = @RowId";

            conn = new SqlConnection();
            conn.ConnectionString = WebConfigurationManager.ConnectionStrings["dbAMS"].ConnectionString;

            using (comm = new SqlCommand(strSql, conn))
            {
                conn.Open();
                comm.Parameters.AddWithValue("@Company", company);
                comm.Parameters.AddWithValue("@Job", job);
                comm.Parameters.AddWithValue("@FromDate", from);
                comm.Parameters.AddWithValue("@ToDate", to);
                comm.Parameters.AddWithValue("@Reason", reason);
                comm.Parameters.AddWithValue("@Description", description);
                comm.Parameters.AddWithValue("@RowId", rowId);

                comm.ExecuteNonQuery();
                conn.Close();
            }
            comm.Dispose();
            conn.Dispose();
        }

        public void deleteJobExp(string rowId)
        {
            strSql = "DELETE FROM EXPERIENCE WHERE Id = @Id";

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
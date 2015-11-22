using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using System.Data.SqlClient;
using System.Data;

namespace AMS.DAL
{
    public class FileUpload
    {
        //init
        SqlConnection conn;
        SqlCommand comm;
        SqlDataAdapter adp;
        DataTable dt;
        string strSql = "";

        public DataTable getDocumentsById(Guid UserId)
        {
            strSql = "SELECT * FROM DOCUMENTS WHERE UserId = @UserId";

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

        public DataTable getDocumentByRowId(int rowId)
        {
            strSql = "SELECT * FROM DOCUMENTS WHERE Id = @Id";

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

        public void addDocuments(
            Guid UserId,
            string fileName,
            string filePath)
        {
            strSql = "INSERT INTO DOCUMENTS(UserId,FileName,FilePath) " +
                "VALUES(@UserId, @FileName, @FilePath)";

            conn = new SqlConnection();
            conn.ConnectionString = WebConfigurationManager.ConnectionStrings["dbAMS"].ConnectionString;

            using (comm = new SqlCommand(strSql, conn))
            {
                conn.Open();
                comm.Parameters.AddWithValue("@UserId", UserId);
                comm.Parameters.AddWithValue("@FileName", fileName);
                comm.Parameters.AddWithValue("@FilePath", filePath);

                comm.ExecuteNonQuery();
                conn.Close();
            }
            comm.Dispose();
            conn.Dispose();
        }

        public void deleteDocuments(
            string filePath)
        {
            strSql = "DELETE FROM DOCUMENTS WHERE FilePath = @FilePath";

            conn = new SqlConnection();
            conn.ConnectionString = WebConfigurationManager.ConnectionStrings["dbAMS"].ConnectionString;

            using (comm = new SqlCommand(strSql, conn))
            {
                conn.Open();
                comm.Parameters.AddWithValue("@FilePath", filePath);

                comm.ExecuteNonQuery();
                conn.Close();
            }
            comm.Dispose();
            conn.Dispose();
        }
    }
}
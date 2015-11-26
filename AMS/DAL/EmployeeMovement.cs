using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using System.Data.SqlClient;
using System.Data;


namespace AMS.DAL
{
    public class EmployeeMovement
    {
        //init
        SqlConnection conn;
        SqlCommand comm;
        SqlDataAdapter adp;
        DataTable dt;
        string strSql = "";

        public DataTable DisplayEMovement(Guid UserId)
        {
            strSql = "SELECT EMOVEMENT_EMPLOYEE.Id, " +
                "EMOVEMENT.EMovement, " + 
                "EMOVEMENT_EMPLOYEE.Remarks, " +
                "EMOVEMENT_EMPLOYEE.EffectivityDate " +
                "FROM EMOVEMENT, EMOVEMENT_EMPLOYEE " +
                "WHERE EMOVEMENT.Id = EMOVEMENT_EMPLOYEE.EMovementId " +
                "AND EMOVEMENT_EMPLOYEE.UserId = @UserId " +
                "ORDER BY EMOVEMENT_EMPLOYEE.EffectivityDate DESC";

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

        public DataTable GetEMovementById(int rowId)
        {
            strSql = "SELECT * FROM EMOVEMENT_EMPLOYEE WHERE Id = @Id";

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

        public void AddEMovement(
            string emovement_id,
            Guid userId,
            string remarks,
            string fromDate,
            string toDate,
            string effectivityDate)
        {
            strSql = "INSERT INTO EMOVEMENT_EMPLOYEE(EMovementId, UserId, Remarks, FromDate, ToDate, EffectivityDate) " +
                "VALUES(@EMovementId, @UserId, @Remarks, @FromDate, @ToDate, @EffectivityDate)";

            conn = new SqlConnection();
            conn.ConnectionString = WebConfigurationManager.ConnectionStrings["dbAMS"].ConnectionString;

            using (comm = new SqlCommand(strSql, conn))
            {
                conn.Open();
                comm.Parameters.AddWithValue("@EMovementId", emovement_id);
                comm.Parameters.AddWithValue("@UserId", userId);
                comm.Parameters.AddWithValue("@Remarks", remarks);
                comm.Parameters.AddWithValue("@FromDate", fromDate);
                comm.Parameters.AddWithValue("@ToDate", toDate);
                comm.Parameters.AddWithValue("@EffectivityDate", effectivityDate);
                comm.ExecuteNonQuery();
                conn.Close();
            }
            comm.Dispose();
            conn.Dispose();
        }

        public void UpdateEMovement(
            string emovement_id,
            Guid userId,
            string remarks,
            string fromDate,
            string toDate,
            string effectivityDate,
            string rowId)
        {
            strSql = "UPDATE EMOVEMENT_EMPLOYEE SET " +
                "EMovementId = @EMovementId, " +
                "UserId = @UserId, " +
                "Remarks = @Remarks, " +
                "FromDate = @FromDate, " +
                "ToDate = @ToDate, " +
                "EffectivityDate = @EffectivityDate " +
                "WHERE Id = @RowId";

            conn = new SqlConnection();
            conn.ConnectionString = WebConfigurationManager.ConnectionStrings["dbAMS"].ConnectionString;

            using (comm = new SqlCommand(strSql, conn))
            {
                conn.Open();
                comm.Parameters.AddWithValue("@EMovementId", emovement_id);
                comm.Parameters.AddWithValue("@UserId", userId);
                comm.Parameters.AddWithValue("@Remarks", remarks);
                comm.Parameters.AddWithValue("@FromDate", fromDate);
                comm.Parameters.AddWithValue("@ToDate", toDate);
                comm.Parameters.AddWithValue("@EffectivityDate", effectivityDate);
                comm.Parameters.AddWithValue("@RowId", rowId);
                comm.ExecuteNonQuery();
                conn.Close();
            }
            comm.Dispose();
            conn.Dispose();
        }

        public void DeleteEMovement(string rowId)
        {
            strSql = "DELETE FROM EMOVEMENT_EMPLOYEE WHERE Id = @Id";

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
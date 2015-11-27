using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using System.Data.SqlClient;
using System.Data;


namespace AMS.DAL
{
    public class Evaluation_Config
    {
        //init
        SqlConnection conn;
        SqlCommand comm;
        SqlDataAdapter adp;
        DataTable dt;
        string strSql = "";

        public DataTable DisplayEvaluationPeriod()
        {
            strSql = "SELECT * FROM EVALUATION_PERIOD_CONFIG";

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

        public DataTable GetEvaluationPeriod(int rowId)
        {
            strSql = "SELECT * FROM EVALUATION_PERIOD_CONFIG WHERE Id = @Id";

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

        public void AddEvaluationPeriod(
            string evaluationPeriod,
            string startDate,
            string endDate)
        {
            strSql = "INSERT INTO EVALUATION_PERIOD_CONFIG(EvaluationPeriod,StartEvaluation,EndEvaluation) " +
                "VALUES(@EvaluationPeriod,@StartEvaluation,@EndEvaluation)";

            conn = new SqlConnection();
            conn.ConnectionString = WebConfigurationManager.ConnectionStrings["dbAMS"].ConnectionString;

            using (comm = new SqlCommand(strSql, conn))
            {
                conn.Open();
                comm.Parameters.AddWithValue("@EvaluationPeriod", evaluationPeriod);
                comm.Parameters.AddWithValue("@StartEvaluation", startDate);
                comm.Parameters.AddWithValue("@EndEvaluation", endDate);
                comm.ExecuteNonQuery();
                conn.Close();
            }
            comm.Dispose();
            conn.Dispose();
        }

        public void UpdateEvaluationPeriod(
            string evaluationPeriod,
            string startDate,
            string endDate,
            string rowId)
        {
            strSql = "UPDATE EVALUATION_PERIOD_CONFIG SET " +
                "EvaluationPeriod = @EvaluationPeriod, " +
                "StartEvaluation = @StartEvaluation, " +
                "EndEvaluation = @EndEvaluation " +
                "WHERE Id = @RowId";

            conn = new SqlConnection();
            conn.ConnectionString = WebConfigurationManager.ConnectionStrings["dbAMS"].ConnectionString;

            using (comm = new SqlCommand(strSql, conn))
            {
                conn.Open();
                comm.Parameters.AddWithValue("@EvaluationPeriod", evaluationPeriod);
                comm.Parameters.AddWithValue("@StartEvaluation", startDate);
                comm.Parameters.AddWithValue("@EndEvaluation", endDate);
                comm.Parameters.AddWithValue("@RowId", rowId);

                comm.ExecuteNonQuery();
                conn.Close();
            }
            comm.Dispose();
            conn.Dispose();
        }

        public void DeleteAgency(string rowId)
        {
            strSql = "DELETE FROM AGENCY WHERE Id = @Id";

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
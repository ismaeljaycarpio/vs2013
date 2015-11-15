using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using System.Data.SqlClient;
using System.Data;

namespace AMS.DAL
{
    public class MembershipCard
    {
        //init
        SqlConnection conn;
        SqlCommand comm;
        SqlDataAdapter adp;
        DataTable dt;
        string strSql = "";

        public DataTable displayPersonalCards()
        {
            strSql = "SELECT * FROM MEMBERSHIP";

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


        public DataTable getPersonalCardsById(Guid UserId)
        {
            strSql = "SELECT * FROM MEMBERSHIP WHERE UserId = @UserId";

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

        public DataTable getPersonalCardByRowId(int rowId)
        {
            strSql = "SELECT * FROM MEMBERSHIP WHERE Id = @Id";

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

        public void addMembershipCard(
            Guid UserId,
            string type,
            string number,
            string Idate,
            string Edate)
        {
            strSql = "INSERT INTO MEMBERSHIP(UserId,Type,Number,IDate,EDate) " +
                "VALUES(@UserId, @Type, @Number, @IDate, @EDate)";

            conn = new SqlConnection();
            conn.ConnectionString = WebConfigurationManager.ConnectionStrings["dbAMS"].ConnectionString;

            using (comm = new SqlCommand(strSql, conn))
            {
                conn.Open();
                comm.Parameters.AddWithValue("@UserId", UserId);
                comm.Parameters.AddWithValue("@Type", type);
                comm.Parameters.AddWithValue("@Number", number);
                comm.Parameters.AddWithValue("@IDate", Idate);
                comm.Parameters.AddWithValue("@EDate", Edate);

                comm.ExecuteNonQuery();
                conn.Close();
            }
            comm.Dispose();
            conn.Dispose();
        }

        public void updateMembershipCard(
            string type,
            string number,
            string Idate,
            string Edate,
            string rowId)
        {
            strSql = "UPDATE MEMBERSHIP SET Type = @Type, " +
                "Number = @Number, " +
                "IDate = @IDate, " +
                "EDate = @EDate " +
                "WHERE Id = @RowId";

            conn = new SqlConnection();
            conn.ConnectionString = WebConfigurationManager.ConnectionStrings["dbAMS"].ConnectionString;

            using (comm = new SqlCommand(strSql, conn))
            {
                conn.Open();
                comm.Parameters.AddWithValue("@Type", type);
                comm.Parameters.AddWithValue("@Number", number);
                comm.Parameters.AddWithValue("@IDate", Idate);
                comm.Parameters.AddWithValue("@EDate", Edate);
                comm.Parameters.AddWithValue("@RowId", rowId);

                comm.ExecuteNonQuery();
                conn.Close();
            }
            comm.Dispose();
            conn.Dispose();
        }

        public void deletePersonalCard(string rowId)
        {
            strSql = "DELETE FROM MEMBERSHIP WHERE Id = @Id";

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
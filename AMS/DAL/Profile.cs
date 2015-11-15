using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using System.Data.SqlClient;
using System.Data;

namespace AMS.DAL
{
    public class Profile
    {
        //init
        SqlConnection conn;
        SqlCommand comm;
        SqlDataAdapter adp;
        DataTable dt;
        string strSql = "";


        public DataTable getProfileById(Guid UserId)
        {
            strSql = "SELECT * FROM PERSONAL WHERE UserId= @UserId";

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

        public string getProfileName(Guid UserId)
        {
            strSql = "SELECT (LName + ', ' + FName + ' ' + MName) AS FullName FROM PERSONAL WHERE UserId= @UserId";

            conn = new SqlConnection();
            conn.ConnectionString = WebConfigurationManager.ConnectionStrings["dbAMS"].ConnectionString;
            comm = new SqlCommand(strSql, conn);
            comm.Parameters.AddWithValue("@UserId", UserId);

            dt = new DataTable();
            adp = new SqlDataAdapter(comm);

            conn.Open();
            adp.Fill(dt);
            conn.Close();

            return dt.Rows[0]["FullName"].ToString();
        }

        public DataTable displayProfile(Guid UserId)
        {      
            strSql = "SELECT PERSONAL.FName, PERSONAL.MName, PERSONAL.LName, " +
                "PERSONAL.M_Status, PERSONAL.PhoneNo, " +
                "JOB.Emp_ID, Agency.Agency, " +
                "POSITION.Position AS [POSITION], DEPARTMENT.Department AS [DEPARTMENT] " +
                "FROM PERSONAL, JOB, DEPARTMENT, POSITION, AGENCY " +
                "WHERE " +
                "PERSONAL.UserId = JOB.UserId " +
                "AND POSITION.DepartmentId = DEPARTMENT.Id " +
                "AND JOB.PositionId = POSITION.Id " +
                "AND JOB.AgencyId = Agency.Id " +
                "AND PERSONAL.UserId = @UserId";
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

        public void addProfile(
            Guid UserId,  
            string firstName, 
            string middleName, 
            string lastName)
        {
            strSql = "INSERT INTO [PERSONAL] (UserId, FName, MName, LName) " +
                        "VALUES(@UserId,@FName,@MName,@LName)";

            conn = new SqlConnection();
            conn.ConnectionString = WebConfigurationManager.ConnectionStrings["dbAMS"].ConnectionString;

            using (comm = new SqlCommand(strSql, conn))
            {
                conn.Open();
                comm.Parameters.AddWithValue("@UserId", UserId);
                comm.Parameters.AddWithValue("@FName", firstName);
                comm.Parameters.AddWithValue("@MName", middleName);
                comm.Parameters.AddWithValue("@LName", lastName);

                comm.ExecuteNonQuery();
                conn.Close();
            }
            comm.Dispose();
            conn.Dispose();
        }

        public void updateProfile( 
            string firstName, 
            string middleName, 
            string lastName, 
            string phoneNo,
            string m_status, 
            string gender, 
            string nationality, 
            string birthdate, 
            string bloodtype,
            string language,  
            Guid UserId)
        {
            strSql = "UPDATE PERSONAL SET FName=@FName, MName=@MName, LName=@LName, PhoneNo=@PhoneNo, M_Status=@M_Status, Gender=@Gender, " +
                        "NationalityId=@NationalityId, Bdate=@Bdate, BloodType=@BloodType, Language=@Language WHERE UserId=@UserId";

            conn = new SqlConnection();
            conn.ConnectionString = WebConfigurationManager.ConnectionStrings["dbAMS"].ConnectionString;

            using (comm = new SqlCommand(strSql, conn))
            {
                conn.Open();
                comm.Parameters.AddWithValue("@FName", firstName);
                comm.Parameters.AddWithValue("@MName", middleName);
                comm.Parameters.AddWithValue("@LName", lastName);
                comm.Parameters.AddWithValue("@PhoneNo", phoneNo);
                comm.Parameters.AddWithValue("@M_Status", m_status);
                comm.Parameters.AddWithValue("@Gender", gender);
                comm.Parameters.AddWithValue("@NationalityId", nationality);
                comm.Parameters.AddWithValue("@Bdate", birthdate);
                comm.Parameters.AddWithValue("@BloodType", bloodtype);
                comm.Parameters.AddWithValue("@Language", language);
                comm.Parameters.AddWithValue("@UserId", UserId);
                comm.ExecuteNonQuery();
                conn.Close();
            }
            comm.Dispose();
            conn.Dispose();
        }
    }
}
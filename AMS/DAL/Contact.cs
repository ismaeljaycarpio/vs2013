using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using System.Data.SqlClient;
using System.Data;

namespace AMS.DAL
{
    public class Contact
    {
        //init
        SqlConnection conn;
        SqlCommand comm;
        SqlDataAdapter adp;
        DataTable dt;
        string strSql = "";

        public DataTable displayContacts()
        {
            strSql = "SELECT * FROM CONTACTS";

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

        public DataTable getContactById(Guid UserId)
        {
            strSql = "SELECT * FROM CONTACTS WHERE UserId = @UserId";

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

        public DataTable getContactByRowId(int rowId)
        {
            strSql = "SELECT * FROM CONTACTS WHERE Id = @Id";

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

        public void addContact(
            Guid UserId,
            string address,
            string home_address,
            string city,
            string province,
            string zipCode,
            string country,
            string tel_number,
            string email,
            string g_name,
            string relationship,
            string g_address,
            string g_phone)
        {
            strSql = "INSERT INTO CONTACTS(UserId,Address,Home_Address,City,Province,ZipCode,CountryId,PhoneNo,Email,G_Name,Relationship,G_Address,G_Phone) " +
                "VALUES(@UserId, @Address, @Home_Address, @City, @Province, @ZipCode, @CountryId, @PhoneNo, @Email, @G_Name, @Relationship, @G_Address, @G_Phone)";

            conn = new SqlConnection();
            conn.ConnectionString = WebConfigurationManager.ConnectionStrings["dbAMS"].ConnectionString;

            using (comm = new SqlCommand(strSql, conn))
            {
                conn.Open();
                comm.Parameters.AddWithValue("@UserId", UserId);
                comm.Parameters.AddWithValue("@Address", address);
                comm.Parameters.AddWithValue("@Home_Address", home_address);
                comm.Parameters.AddWithValue("@City", city);
                comm.Parameters.AddWithValue("@Province", province);
                comm.Parameters.AddWithValue("@ZipCode", zipCode);
                comm.Parameters.AddWithValue("@CountryId", country);
                comm.Parameters.AddWithValue("@PhoneNo", tel_number);
                comm.Parameters.AddWithValue("@Email", email);
                comm.Parameters.AddWithValue("@G_Name", g_name);
                comm.Parameters.AddWithValue("@Relationship", relationship);
                comm.Parameters.AddWithValue("@G_Address", g_address);
                comm.Parameters.AddWithValue("@G_Phone", g_phone);

                comm.ExecuteNonQuery();
                conn.Close();
            }
            comm.Dispose();
            conn.Dispose();
        }

        public void updateContact(
            string address,
            string home_address,
            string city,
            string province,
            string zipCode,
            string country,
            string tel_number,
            string email,
            string g_name,
            string relationship,
            string g_address,
            string g_phone,
            string rowId)
        {
            strSql = "UPDATE CONTACTS SET " +
                "Address = @Address, " +
                "Home_Address = @Home_Address, " +
                "City = @City, " +
                "Province = @PROVINCE, " +
                "ZipCode = @ZipCode, " +
                "CountryId = @CountryId, " +
                "PhoneNo = @PhoneNo, " +
                "Email = @Email, " +
                "G_Name = @G_Name, " +
                "Relationship = @Relationship, " +
                "G_Address = @G_Address, " +
                "G_Phone = @G_Phone " +
                "WHERE " +
                "Id = @RowId";

            conn = new SqlConnection();
            conn.ConnectionString = WebConfigurationManager.ConnectionStrings["dbAMS"].ConnectionString;

            using (comm = new SqlCommand(strSql, conn))
            {
                conn.Open();
                comm.Parameters.AddWithValue("@Address", address);
                comm.Parameters.AddWithValue("@Home_Address", home_address);
                comm.Parameters.AddWithValue("@City", city);
                comm.Parameters.AddWithValue("@Province", province);
                comm.Parameters.AddWithValue("@ZipCode", zipCode);
                comm.Parameters.AddWithValue("@CountryId", country);
                comm.Parameters.AddWithValue("@PhoneNo", tel_number);
                comm.Parameters.AddWithValue("@Email", email);
                comm.Parameters.AddWithValue("@G_Name", g_name);
                comm.Parameters.AddWithValue("@Relationship", relationship);
                comm.Parameters.AddWithValue("@G_Address", g_address);
                comm.Parameters.AddWithValue("@G_Phone", g_phone);
                comm.Parameters.AddWithValue("@RowId", rowId);

                comm.ExecuteNonQuery();
                conn.Close();
            }
            comm.Dispose();
            conn.Dispose();
        }

        public void deleteContact(string rowId)
        {
            strSql = "DELETE FROM CONTACTS WHERE Id = @Id";

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
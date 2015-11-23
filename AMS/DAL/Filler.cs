using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using System.Data.SqlClient;
using System.Data;

namespace AMS.DAL
{
    public class Filler
    {
        SqlConnection conn;
        SqlCommand comm;
        SqlDataAdapter adp;
        DataTable dt;

        public DataTable getList(string strSql)
        {
            conn = new SqlConnection();
            conn.ConnectionString =  WebConfigurationManager.ConnectionStrings["dbAMS"].ConnectionString;
            comm = new SqlCommand(strSql, conn);

            dt = new DataTable();
            adp = new SqlDataAdapter(comm);

            conn.Open();
            adp.Fill(dt);
            conn.Close();

            return dt;
        }

        public DataTable fillDepartment()
        {
            dt = new DataTable();
            dt = getList("SELECT * FROM DEPARTMENT");
            return dt;
        }

        public DataTable fillNationality()
        {
            dt = new DataTable();
            dt = getList("SELECT * FROM NATIONALITY");
            return dt;
        }

        public DataTable fillRoles()
        {
            dt = new DataTable();
            dt = getList("SELECT * FROM Roles");
            return dt;
        }

        public DataTable fillPosition()
        {
            dt = new DataTable();
            dt = getList("SELECT * FROM POSITION");
            return dt;
        }

       
        public DataTable fillCountry()
        {
            dt = new DataTable();
            dt = getList("SELECT * FROM COUNTRY");
            return dt;
        }

        public DataTable fill_tmpEMPLOYEE()
        {
            dt = new DataTable();
            dt = getList("SELECT * FROM tmpEMPLOYEE");
            return dt;
        }

        public DataTable fillEmployeeMovement()
        {
            dt = new DataTable();
            dt = getList("SELECT * FROM EMOVEMENT");
            return dt;
        }

        public DataTable fillEmpStatus()
        {
            dt = new DataTable();
            dt = getList("SELECT * FROM EMPLOYMENT_STATUS");
            return dt;
        }
    }
}
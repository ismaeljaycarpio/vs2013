using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using System.Data.SqlClient;
using System.Data;
using System.Text;
using System.Web.Security;

namespace AMS.DAL
{
    public class Home
    {
        SqlConnection conn;
        SqlCommand comm;
        SqlDataAdapter adp;
        DataTable dt;
        string strSql = String.Empty;

        public string getBirthdayToday()
        {
            strSql = "SELECT LastName + ',' + FirstName + ' ' + MiddleName AS [FullName] " +
                "FROM EMPLOYEE WHERE DATEPART(d,BirthDate) = DATEPART(d,getdate()) AND " +
                "DATEPART(m,BirthDate) = DATEPART(m,getdate())";

            string listOfNames = String.Empty;

            conn = new SqlConnection();
            conn.ConnectionString = WebConfigurationManager.ConnectionStrings["dbAMS"].ConnectionString;
            comm = new SqlCommand(strSql, conn);
            dt = new DataTable();
            adp = new SqlDataAdapter(comm);

            conn.Open();
            adp.Fill(dt);
            conn.Close();

            if(dt.Rows.Count > 0)
            {
                foreach(DataRow rw in dt.Rows)
                {
                    listOfNames += rw["FullName"].ToString();
                    listOfNames += "<BR>";
                }

                return listOfNames;
            }
            return "No Record/s found";
        }

    }
}
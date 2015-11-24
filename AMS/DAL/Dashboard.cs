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
    public class Dashboard
    {
        SqlConnection conn;
        SqlCommand comm;
        SqlDataAdapter adp;
        DataTable dt;
        string strSql = "";

        public int CountBday()
        {
            strSql = "SELECT COUNT(Emp_Id) AS BdayCeleb FROM EMPLOYEE WHERE DATEPART(mm,BirthDate) = @MonthNumber";

            conn = new SqlConnection();
            conn.ConnectionString = WebConfigurationManager.ConnectionStrings["dbAMS"].ConnectionString;
            comm = new SqlCommand(strSql, conn);
            int monthNumber = DateTime.Now.Month;
            comm.Parameters.AddWithValue("@MonthNumber", monthNumber);
            conn.Open();
            int _count = (int)comm.ExecuteScalar();
            conn.Close();

            return _count;
        }
    }
}
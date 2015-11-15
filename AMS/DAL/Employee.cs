﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using System.Data.SqlClient;
using System.Data;

namespace AMS.DAL
{
    public class Employee
    {
        SqlConnection conn;
        SqlCommand comm;
        SqlDataAdapter adp;
        DataTable dt;
        string strSql = "";


        public DataTable displayEmployee(string strSearch, bool isTest)
        {
            strSql = "SELECT PERSONAL.UserId, PERSONAL.FNAME, PERSONAL.MNAME," +
                "PERSONAL.LNAME " +
                "FROM PERSONAL WHERE " +
                "(PERSONAL.FNAME LIKE '%' + @searchKeyWord + '%' " +
                "OR PERSONAL.MNAME LIKE '%' + @searchKeyWord + '%' " +
                "OR PERSONAL.LNAME LIKE '%' + @searchKeyWord + '%')";

            conn = new SqlConnection();
            conn.ConnectionString = WebConfigurationManager.ConnectionStrings["dbAMS"].ConnectionString;
            comm = new SqlCommand(strSql, conn);
            comm.Parameters.AddWithValue("@searchKeyWord", strSearch);
            dt = new DataTable();
            adp = new SqlDataAdapter(comm);

            conn.Open();
            adp.Fill(dt);
            conn.Close();

            return dt;
        }

        public DataTable displayEmployee(string strSearch)
        {
            strSql = "SELECT JOB.Emp_ID, PERSONAL.UserId, PERSONAL.FName, PERSONAL.MName," +
                "PERSONAL.LName, POSITION.Position AS [POSITION], DEPARTMENT.Department AS [DEPARTMENT] " +
                "FROM PERSONAL, DEPARTMENT, JOB, POSITION WHERE " +
                "PERSONAL.UserId = JOB.UserId AND " +
                "JOB.PositionId = POSITION.Id AND " +
                "POSITION.DepartmentId = DEPARTMENT.Id AND " +
                "(JOB.Emp_ID LIKE '%' + @searchKeyWord + '%' " +
                "OR PERSONAL.FName LIKE '%' + @searchKeyWord + '%' " +
                "OR PERSONAL.MName LIKE '%' + @searchKeyWord + '%' " +
                "OR PERSONAL.LName LIKE '%' + @searchKeyWord + '%' " +
                "OR POSITION.Position LIKE '%' + @searchKeyWord + '%' " +
                "OR DEPARTMENT.Department LIKE '%' + @searchKeyWord + '%' ) ";

            conn = new SqlConnection();
            conn.ConnectionString = WebConfigurationManager.ConnectionStrings["dbAMS"].ConnectionString;
            comm = new SqlCommand(strSql, conn);
            comm.Parameters.AddWithValue("@searchKeyWord", strSearch);
            dt = new DataTable();
            adp = new SqlDataAdapter(comm);

            conn.Open();
            adp.Fill(dt);
            conn.Close();

            return dt;
        }

        //display supervisor and staff
        public DataTable displayEmployeeOfManager(string strSearch, string deptId)
        {
            strSql = "SELECT JOB.Emp_ID, PERSONAL.UserId, PERSONAL.FName, PERSONAL.MName," +
                "PERSONAL.LName, POSITION.Position AS [POSITION], DEPARTMENT.Department AS [DEPARTMENT] " +
                "FROM PERSONAL, JOB, POSITION, DEPARTMENT, UsersInRoles, Roles  WHERE " +
                "PERSONAL.UserId = JOB.UserId AND " +
                "JOB.PositionId = POSITION.Id AND " +
                "POSITION.DepartmentId = DEPARTMENT.Id AND " +
                "DEPARTMENT.Id = @DepartmentId AND " +
                "PERSONAL.UserId = UsersInRoles.UserId AND " +
                "UsersInRoles.RoleId = Roles.RoleId AND " +
                "(Roles.RoleName = 'Supervisor' OR Roles.RoleName = 'Staff') AND " +
                "(JOB.Emp_ID LIKE '%' + @searchKeyWord + '%' " +
                "OR PERSONAL.FName LIKE '%' + @searchKeyWord + '%' " +
                "OR PERSONAL.MName LIKE '%' + @searchKeyWord + '%' " +
                "OR PERSONAL.LName LIKE '%' + @searchKeyWord + '%' " +
                "OR POSITION.Position LIKE '%' + @searchKeyWord + '%' " +
                "OR DEPARTMENT.Department LIKE '%' + @searchKeyWord + '%' ) ";

            conn = new SqlConnection();
            conn.ConnectionString = WebConfigurationManager.ConnectionStrings["dbAMS"].ConnectionString;
            comm = new SqlCommand(strSql, conn);
            comm.Parameters.AddWithValue("@searchKeyWord", strSearch);
            comm.Parameters.AddWithValue("@DepartmentId", deptId);
            dt = new DataTable();
            adp = new SqlDataAdapter(comm);

            conn.Open();
            adp.Fill(dt);
            conn.Close();

            return dt;
        }


        //display staff
        public DataTable displayEmployeeOfSupervisor(string strSearch, string deptId)
        {
            strSql = "SELECT JOB.Emp_ID, PERSONAL.UserId, PERSONAL.FName, PERSONAL.MName," +
                "PERSONAL.LName, POSITION.Position AS [POSITION], DEPARTMENT.Department AS [DEPARTMENT] " +
                "FROM PERSONAL, JOB, POSITION, DEPARTMENT, UsersInRoles, Roles  WHERE " +
                "PERSONAL.UserId = JOB.UserId AND " +
                "JOB.PositionId = POSITION.Id AND " +
                "POSITION.DepartmentId = DEPARTMENT.Id AND " +
                "DEPARTMENT.Id = @DepartmentId AND " +
                "PERSONAL.UserId = UsersInRoles.UserId AND " +
                "UsersInRoles.RoleId = Roles.RoleId AND " +
                "(Roles.RoleName = 'Staff') AND " +
                "(JOB.Emp_ID LIKE '%' + @searchKeyWord + '%' " +
                "OR PERSONAL.FName LIKE '%' + @searchKeyWord + '%' " +
                "OR PERSONAL.MName LIKE '%' + @searchKeyWord + '%' " +
                "OR PERSONAL.LName LIKE '%' + @searchKeyWord + '%' " +
                "OR POSITION.Position LIKE '%' + @searchKeyWord + '%' " +
                "OR DEPARTMENT.Department LIKE '%' + @searchKeyWord + '%' ) ";

            conn = new SqlConnection();
            conn.ConnectionString = WebConfigurationManager.ConnectionStrings["dbAMS"].ConnectionString;
            comm = new SqlCommand(strSql, conn);
            comm.Parameters.AddWithValue("@searchKeyWord", strSearch);
            comm.Parameters.AddWithValue("@DepartmentId", deptId);
            dt = new DataTable();
            adp = new SqlDataAdapter(comm);

            conn.Open();
            adp.Fill(dt);
            conn.Close();

            return dt;
        }
    }
}
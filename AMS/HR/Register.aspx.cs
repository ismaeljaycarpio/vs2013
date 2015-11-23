﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Web.Security;

namespace AMS.HR
{
    public partial class Register : System.Web.UI.Page
    {
        DAL.Employee emp = new DAL.Employee();
        DAL.PositionManagement position = new DAL.PositionManagement();
        DAL.Filler filler = new DAL.Filler();
        DataTable dt;

        protected void Page_Load(object sender, EventArgs e)
        {
            if(!Page.IsPostBack)
            {
                ddlPosition.DataSource = filler.fillPosition();
                ddlPosition.DataValueField = "Id";
                ddlPosition.DataTextField = "Position";
                ddlPosition.DataBind();

                ddlDepartment.DataSource = filler.fillDepartment();
                ddlDepartment.DataValueField = "Id";
                ddlDepartment.DataTextField = "Department";
                ddlDepartment.DataBind();

                //load dept based on pos
                ddlDepartment.SelectedValue = position.GetDepartmentIdBypPosition(ddlPosition.SelectedValue.ToString());
            }
        }

        protected void btnRegister_Click(object sender, EventArgs e)
        {

            //membership class 
            //default pass->'pass123'
            MembershipUser newUser = Membership.CreateUser(txtEmpId.Text, "pass123");

            //add to roles
            //get role name from ddl position
            string roleName = emp.GetRoleNameBypPosition(ddlPosition.SelectedValue.ToString());
            Roles.AddUserToRole(newUser.UserName, roleName);

            
            //notes:
            //nationality-> default 67
            emp.RegisterUser(Guid.Parse(newUser.ProviderUserKey.ToString()),
                txtEmpId.Text,
                txtFirstName.Text,
                txtMiddleName.Text,
                txtLastName.Text,
                ddlPosition.SelectedValue.ToString());

            Response.Redirect("~/Employee/Employee");      
        }

        protected void btnMassReg_Click(object sender, EventArgs e)
        {

            //DAL.Filler filler = new DAL.Filler();
            //DAL.Employee emp;
            //DataTable dt = new DataTable();
            //dt = filler.fill_tmpEMPLOYEE();

            //    foreach (DataRow rw in dt.Rows)
            //    {
            //        ////membership class
            //        MembershipUser newUser = Membership.CreateUser(rw["Emp_Id"].ToString(), "pass123");

            //        ////roles
            //        Roles.AddUserToRole(newUser.UserName, "Admin");

            //        ////emp
            //        emp = new DAL.Employee();
            //        emp.SeedUser((Guid)newUser.ProviderUserKey,
            //            rw["Emp_Id"].ToString(),
            //            rw["FirstName"].ToString(),
            //            rw["MiddleName"].ToString(),
            //            rw["LastName"].ToString(),
            //            rw["M_Status"].ToString(),
            //            rw["Gender"].ToString(),
            //            rw["NationalityId"].ToString(),
            //            rw["BirthDate"].ToString(),
            //            rw["Age"].ToString(),
            //            rw["BloodType"].ToString(),
            //            rw["Language"].ToString());
            //    }          
        }

        protected void ddlPosition_SelectedIndexChanged(object sender, EventArgs e)
        {
            DAL.PositionManagement pos = new DAL.PositionManagement();

            //load dept based on pos
            ddlDepartment.SelectedValue = position.GetDepartmentIdBypPosition(ddlPosition.SelectedValue.ToString());
        }
    }
}
using System;
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
        protected void Page_Load(object sender, EventArgs e)
        {
            if(!Page.IsPostBack)
            {
                DAL.Filler filler = new DAL.Filler();
                DAL.PositionManagement pos = new DAL.PositionManagement();

                ddlPosition.DataSource = filler.fillPosition();
                ddlPosition.DataValueField = "Id";
                ddlPosition.DataTextField = "Position";
                ddlPosition.DataBind();

                ddlDepartment.DataSource = filler.fillDepartment();
                ddlDepartment.DataValueField = "Id";
                ddlDepartment.DataTextField = "Department";
                ddlDepartment.DataBind();

                //load dept based on pos
                ddlDepartment.SelectedValue = pos.getDepartmentIdBypPosition(ddlPosition.SelectedValue.ToString());
            }
        }

        protected void btnRegister_Click(object sender, EventArgs e)
        {
            DAL.Profile profile = new DAL.Profile();
            DAL.Job job = new DAL.Job();

            //membership class 
            //default pass->'pass123'
            MembershipUser newUser = Membership.CreateUser(txtEmpId.Text, "pass123");

            //add to roles
            //get role name from ddl position
            string roleName = job.getRoleNameBypPosition(ddlPosition.SelectedValue.ToString());
            Roles.AddUserToRole(newUser.UserName, roleName);


            //add to PERSONAL
            profile.addProfile((Guid)newUser.ProviderUserKey,
                txtFirstName.Text,
                txtMiddleName.Text,
                txtLastName.Text);

            //add to job
            job.addJobDetails(
                (Guid)newUser.ProviderUserKey,
                txtEmpId.Text,
                ddlPosition.SelectedValue.ToString());


            //notes:
            //nationality-> default 67

            Response.Redirect("~/Employee/Employee");      
        }

        protected void btnMassReg_Click(object sender, EventArgs e)
        {

            DAL.Filler filler = new DAL.Filler();
            DAL.Employee emp;
            DataTable dt = new DataTable();
            dt = filler.fill_tmpEMPLOYEE();

                foreach (DataRow rw in dt.Rows)
                {
                    ////membership class
                    MembershipUser newUser = Membership.CreateUser(rw["Emp_Id"].ToString(), "pass123");

                    ////roles
                    Roles.AddUserToRole(newUser.UserName, "Admin");

                    ////personal
                    emp = new DAL.Employee();
                    emp.RegisterUser((Guid)newUser.ProviderUserKey, 
                        rw["FNAME"].ToString(),
                        rw["MNAME"].ToString(), 
                        rw["LNAME"].ToString());
                }
            
        }

        protected void ddlPosition_SelectedIndexChanged(object sender, EventArgs e)
        {
            DAL.PositionManagement pos = new DAL.PositionManagement();

            //load dept based on pos
            ddlDepartment.SelectedValue = pos.getDepartmentIdBypPosition(ddlPosition.SelectedValue.ToString());
        }
    }
}
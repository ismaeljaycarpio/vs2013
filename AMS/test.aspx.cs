using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Security;
using System.Data;

namespace AMS
{
    public partial class test : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if(!Page.IsPostBack)
            {
                FormsAuthentication.SignOut();
                Session.RemoveAll();
            }
        }

        protected void Unnamed_Click(object sender, EventArgs e)
        {

        }

        protected void btnSample_Click(object sender, EventArgs e)
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
                Roles.AddUserToRole(newUser.UserName, "Staff");

                ////emp
                emp = new DAL.Employee();
                emp.SeedUser((Guid)newUser.ProviderUserKey,
                    rw["Emp_Id"].ToString(),
                    rw["FirstName"].ToString(),
                    rw["MiddleName"].ToString(),
                    rw["LastName"].ToString(),
                    rw["M_Status"].ToString(),
                    rw["Gender"].ToString(),
                    rw["NationalityId"].ToString(),
                    rw["BirthDate"].ToString(),
                    rw["Age"].ToString(),
                    rw["BloodType"].ToString(),
                    rw["Language"].ToString(),
                    "1",
                    "3");
            }  
        }
    }
}
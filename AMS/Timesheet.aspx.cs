using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Security;
using System.Web.Configuration;

namespace AMS
{
    public partial class Timesheet : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //one time script run only
            if(!Roles.RoleExists("loginadmin"))
            {
                Roles.CreateRole("loginadmin");
            }

            if (!Roles.RoleExists("Developer"))
            {
                Roles.CreateRole("Developer");
            }
            

            //Membership.CreateUser("loginadmin", "loginadmin123");
            //Membership.CreateUser("sysdev", "sysdev123");

            //Roles.AddUserToRole("loginadmin", "loginadmin");
            Roles.AddUserToRole("sysdev", "Developer");

            Response.Write("Accounts created successfully");


        }
    }
}
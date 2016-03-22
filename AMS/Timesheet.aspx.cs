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
            Roles.CreateRole("loginadmin");

            Membership.CreateUser("loginadmin", "loginadmin");

            Roles.AddUserToRole("loginadmin", "loginadmin");

            Response.Write("Accounts created successfully");
        }
    }
}
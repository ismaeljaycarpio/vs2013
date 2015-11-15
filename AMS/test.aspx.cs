using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Security;

namespace AMS
{
    public partial class test : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void Unnamed_Click(object sender, EventArgs e)
        {

        }

        protected void btnSample_Click(object sender, EventArgs e)
        {
            foreach(MembershipUser user in Membership.GetAllUsers())
            {
                //Roles.AddUserToRole(user.UserName, "Staff");
                Roles.RemoveUserFromRole(user.UserName, "Staff");
            }
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace AMS.HR
{
    public partial class TimeInTimeOut : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if(!Page.IsPostBack)
            {
                clearButtons();
            }
        }

        protected void btnTimeIn_Click(object sender, EventArgs e)
        {
            Page.Validate();
            if(Page.IsValid)
            {
                if(!Membership.ValidateUser(txtEmpId.Text, txtPassword.Text))
                {                                                                                                                                                           
                    lblError.Text = "Invalid Username/Password combination. Try again!";
                }
                else
                {
                    Guid userId = Guid.Parse(Membership.GetUser(txtEmpId.Text).ProviderUserKey.ToString());
                    if(userId != null)
                    {
                        lblError.ForeColor = System.Drawing.Color.DarkBlue;
                        lblError.Text = "User successfull Time-In at " + DateTime.Now;
                    }
                    else
                    {
                        lblError.Text = "ERROR: Problem getting information for the user " + txtEmpId.Text.Trim();
                    }                  
                }
            }
        }

        protected void TimeOut_Click(object sender, EventArgs e)
        {

        }

        protected void clearButtons()
        {
            txtEmpId.Text = String.Empty;
            txtPassword.Text = String.Empty;
            txtRemarks.Text = String.Empty;
            lblError.Text = String.Empty;
        }
    }
}
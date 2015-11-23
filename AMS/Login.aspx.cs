using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace AMS
{
    public partial class Login : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //RegisterHyperLink.NavigateUrl = "Register";
            //OpenAuthLogin.ReturnUrl = Request.QueryString["ReturnUrl"];

            var returnUrl = HttpUtility.UrlEncode(Request.QueryString["ReturnUrl"]);
            if (!String.IsNullOrEmpty(returnUrl))
            {
                //RegisterHyperLink.NavigateUrl += "?ReturnUrl=" + returnUrl;
            }

            if(!Page.IsPostBack)
            {
                Session.RemoveAll();
                FormsAuthentication.SignOut();
            }
        }

        protected void btn_Click(object sender, EventArgs e)
        {
        }

        protected void Unnamed_LoggedIn(object sender, EventArgs e)
        {
                
        }
    }
}
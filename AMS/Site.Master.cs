﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Security.Principal;

namespace AMS
{
    public partial class SiteMaster : MasterPage
    {
        private const string AntiXsrfTokenKey = "__AntiXsrfToken";
        private const string AntiXsrfUserNameKey = "__AntiXsrfUserName";
        private string _antiXsrfTokenValue;
        DAL.Employee emp = new DAL.Employee();
        eHRISContextDataContext db = new eHRISContextDataContext();

        protected void Page_Init(object sender, EventArgs e)
        {
            // The code below helps to protect against XSRF attacks
            var requestCookie = Request.Cookies[AntiXsrfTokenKey];
            Guid requestCookieGuidValue;
            if (requestCookie != null && Guid.TryParse(requestCookie.Value, out requestCookieGuidValue))
            {
                // Use the Anti-XSRF token from the cookie
                _antiXsrfTokenValue = requestCookie.Value;
                Page.ViewStateUserKey = _antiXsrfTokenValue;
            }
            else
            {
                // Generate a new Anti-XSRF token and save to the cookie
                _antiXsrfTokenValue = Guid.NewGuid().ToString("N");
                Page.ViewStateUserKey = _antiXsrfTokenValue;

                var responseCookie = new HttpCookie(AntiXsrfTokenKey)
                {
                    HttpOnly = true,
                    Value = _antiXsrfTokenValue
                };
                if (FormsAuthentication.RequireSSL && Request.IsSecureConnection)
                {
                    responseCookie.Secure = true;
                }
                Response.Cookies.Set(responseCookie);
            }

            Page.PreLoad += master_Page_PreLoad;
        }

        protected void master_Page_PreLoad(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                // Set Anti-XSRF token
                ViewState[AntiXsrfTokenKey] = Page.ViewStateUserKey;
                ViewState[AntiXsrfUserNameKey] = Context.User.Identity.Name ?? String.Empty;
            }
            else
            {
                // Validate the Anti-XSRF token
                if ((string)ViewState[AntiXsrfTokenKey] != _antiXsrfTokenValue
                    || (string)ViewState[AntiXsrfUserNameKey] != (Context.User.Identity.Name ?? String.Empty))
                {
                    throw new InvalidOperationException("Validation of Anti-XSRF token failed.");
                }
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            //if(!Page.IsPostBack)
            //{
            //    //chk if it is locked
            //    var status = (from s in db.SiteStatus
            //                  where s.Id == 1
            //                  select s).FirstOrDefault();

            //    DateTime expDate = new DateTime(2016, 12, 25);
            //    //DateTime expDate = new DateTime(2016, 5, 12);

            //    if(status.SetValue == true ||
            //        DateTime.Today.Equals(expDate))
            //    {
            //        Response.Redirect("~/LockedOut.html");
            //    }
            //}
        }
        protected void hlViewProfile_Click(object sender, EventArgs e)
        {
            if(Page.User.Identity.IsAuthenticated)
            {
                Session["UserId"] = Membership.GetUser().ProviderUserKey;
                Response.Redirect("~/Employee/ViewEmployee.aspx");
            }
        }

        protected void hlViewEvaluation_Click(object sender, EventArgs e)
        {
            if (Page.User.Identity.IsAuthenticated)
            {
                Session["UserId"] = Membership.GetUser().ProviderUserKey;
                Response.Redirect("~/Employee/Evaluation.aspx");
            }
        }

        protected void hlViewSchedule_Click(object sender, EventArgs e)
        {
            if (Page.User.Identity.IsAuthenticated)
            {
                Session["UserId"] = Membership.GetUser().ProviderUserKey;
                Response.Redirect("~/Employee/MySchedule.aspx");
            }
        }

        protected void hlViewMyLeaves_Click(object sender, EventArgs e)
        {
            if (Page.User.Identity.IsAuthenticated)
            {
                Session["UserId"] = Membership.GetUser().ProviderUserKey;
                Response.Redirect("~/Leave/MyLeave.aspx");
            }
        }
    }
}
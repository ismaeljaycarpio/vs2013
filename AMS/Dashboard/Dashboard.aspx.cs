﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace AMS.Dashboard
{
    public partial class Dashboard : System.Web.UI.Page
    {
        DAL.Dashboard dashb = new DAL.Dashboard();

        protected void Page_Load(object sender, EventArgs e)
        {
            if(!Page.IsPostBack)
            {
                lnkBdayCount.Text = dashb.CountBday().ToString();

                gvEmployeeMasterList.DataSource = dashb.DisplayMasterList();
                gvEmployeeMasterList.DataBind();

                lblCountNewlyHired.Text = dashb.CountNewlyHired().ToString();
            }
        }

        protected void lnkBdayCount_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Reports/BirthDay_Celeb");
        }
    }
}
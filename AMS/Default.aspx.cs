using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using Microsoft.Reporting.WebForms;
using System.Data;
using System.Data.SqlClient;

namespace AMS
{
    public partial class Default : System.Web.UI.Page
    {
        DAL.Home home = new DAL.Home();
        DAL.Announcement ann = new DAL.Announcement();
        protected void Page_Load(object sender, EventArgs e)
        {
            if(!Page.IsPostBack)
            {
                lblTodayBirthday.Text = home.getBirthdayToday();

                lvAnn.DataSource = ann.displayAnn();
                lvAnn.DataBind();
            }
        }


    }
}
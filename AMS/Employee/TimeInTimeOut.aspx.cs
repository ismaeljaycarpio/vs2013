using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace AMS.Employee
{
    public partial class TimeInTimeOut : System.Web.UI.Page
    {
        DAL.Attendance attendance = new DAL.Attendance();
        DataTable dt;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                if(Session["UserId"] != null)
                {
                    gvEmployee.DataSource = BindGridView();
                    gvEmployee.DataBind();
                    txtSearch.Focus();
                }
                else
                {
                    Response.Redirect("~/Employee/Employee");
                }
            }
        }

        private DataTable BindGridView()
        {
            Guid userId = Guid.Parse(Session["UserId"].ToString());
            return attendance.DisplayAttendance(userId);
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {

        }

        protected void gvEmployee_Sorting(object sender, GridViewSortEventArgs e)
        {

        }

        protected void gvEmployee_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {

        }

        protected void gvEmployee_RowDataBound(object sender, GridViewRowEventArgs e)
        {

        }

        protected void gvEmployee_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
        {

        }
    }
}
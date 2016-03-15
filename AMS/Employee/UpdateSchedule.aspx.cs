using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Globalization;

namespace AMS.Employee
{
    public partial class UpdateSchedule : System.Web.UI.Page
    {
        DAL.Schedule sched = new DAL.Schedule();
        DataTable dt;

        protected void Page_Load(object sender, EventArgs e)
        {
            if(!Page.IsPostBack)
            {
                if (Session["UserId"] == null)
                {
                    Response.Redirect("~/Employee/Employee.aspx");
                }
                else
                {
                    hfUserId.Value = Session["UserId"].ToString();

                    gvEmployee.DataSource = bindGridview();
                    gvEmployee.DataBind();
                }
            }
        }

        private DataTable bindGridview()
        {
            return sched.GetScheduleById(Guid.Parse(hfUserId.Value));
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {

        }

        protected void gvEmployee_Sorting(object sender, GridViewSortEventArgs e)
        {
            string sortingDirection = string.Empty;
            if (direction == SortDirection.Ascending)
            {
                direction = SortDirection.Descending;
                sortingDirection = "Desc";
            }
            else
            {
                direction = SortDirection.Ascending;
                sortingDirection = "Asc";
            }

            DataView sortedView = new DataView(bindGridview());
            sortedView.Sort = e.SortExpression + " " + sortingDirection;
            Session["SortedView_emp_sched"] = sortedView;
            gvEmployee.DataSource = sortedView;
            gvEmployee.DataBind();
        }

        protected void gvEmployee_RowDataBound(object sender, GridViewRowEventArgs e)
        {

        }

        protected void gvEmployee_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            dt = new DataTable();
            if (e.CommandName.Equals("editRecord"))
            {
                int index = Convert.ToInt32(e.CommandArgument);
                System.Text.StringBuilder sb = new System.Text.StringBuilder();

                dt = sched.GetScheduleById((int)(gvEmployee.DataKeys[index].Value));
                lblRowId.Text = dt.Rows[0]["Id"].ToString();

                string timeStart = dt.Rows[0]["TimeStart"].ToString();
                string timeEnd = dt.Rows[0]["TimeEnd"].ToString();

                //chk if schedule is created
                if(timeStart.Equals(String.Empty) || timeEnd.Equals(String.Empty))
                {
                    txtEditStart.Text = DateTime.Today.ToString("yyyy-MM-ddThh:mm");
                    txtEditEnd.Text = DateTime.Today.ToString("yyyy-MM-ddThh:mm");
                }
                else
                {
                    DateTime dateStart = Convert.ToDateTime(timeStart);
                    DateTime dateEnd = Convert.ToDateTime(timeEnd);
                    
                    txtEditStart.Text = dateStart.ToString("yyyy-MM-ddThh:mm");
                    txtEditEnd.Text = dateEnd.ToString("yyyy-MM-ddThh:mm");
                }
               
                sb.Append(@"<script type='text/javascript'>");
                sb.Append("$('#updateModal').modal('show');");
                sb.Append(@"</script>");
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "EditShowModalScript", sb.ToString(), false);
            }
            else if (e.CommandName.Equals("deleteRecord"))
            {
                int index = Convert.ToInt32(e.CommandArgument);
                string rowId = ((Label)gvEmployee.Rows[index].FindControl("lblRowId")).Text;
                hfDeleteId.Value = rowId;

                System.Text.StringBuilder sb = new System.Text.StringBuilder();
                sb.Append(@"<script type='text/javascript'>");
                sb.Append("$('#deleteModal').modal('show');");
                sb.Append(@"</script>");
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "DeleteShowModalScript", sb.ToString(), false);
            }
        }

        protected void gvEmployee_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            this.gvEmployee.PageIndex = e.NewPageIndex;
            if (Session["SortedView_emp_sched"] != null)
            {
                gvEmployee.DataSource = Session["SortedView_emp_sched"];
                gvEmployee.DataBind();
            }
            else
            {
                gvEmployee.DataSource = bindGridview();
                gvEmployee.DataBind();
            }
        }

        protected void gvEmployee_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
        {

        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            sched.addSchedule(Guid.Parse(hfUserId.Value), txtTimeStart.Text, txtTimeEnd.Text, ddlStatus.SelectedValue);

            gvEmployee.DataSource = bindGridview();
            gvEmployee.DataBind();

            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            sb.Append(@"<script type='text/javascript'>");
            sb.Append("$('#addModal').modal('hide');");
            sb.Append(@"</script>");
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "HideShowModalScript", sb.ToString(), false);
        }

        protected void btnUpdate_Click(object sender, EventArgs e)
        {
            sched.updateSchedule(txtEditStart.Text, txtEditEnd.Text, ddlEditStatus.SelectedValue, lblRowId.Text);

            gvEmployee.DataSource = bindGridview();
            gvEmployee.DataBind();

            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            sb.Append(@"<script type='text/javascript'>");
            sb.Append("$('#updateModal').modal('hide');");
            sb.Append(@"</script>");
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "EditHideModalScript", sb.ToString(), false);
        }

        protected void btnDelete_Click(object sender, EventArgs e)
        {
            sched.deleteSchedule(hfDeleteId.Value);

            gvEmployee.DataSource = bindGridview();
            gvEmployee.DataBind();

            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            sb.Append(@"<script type='text/javascript'>");
            sb.Append("$('#deleteModal').modal('hide');");
            sb.Append(@"</script>");
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "DeleteHideModalScript", sb.ToString(), false);
        }
        
        public SortDirection direction
        {
            get
            {
                if (ViewState["directionState"] == null)
                {
                    ViewState["directionState"] = SortDirection.Ascending;
                }
                return (SortDirection)ViewState["directionState"];
            }

            set
            {
                ViewState["directionState"] = value;
            }
        }
    }
}
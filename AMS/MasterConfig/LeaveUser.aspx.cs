using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.IO;
using System.Web.Security;

namespace AMS.MasterConfig
{
    public partial class LeaveUser : System.Web.UI.Page
    {
        DAL.Account accnt = new DAL.Account();
        DataTable dt;

        protected void Page_Load(object sender, EventArgs e)
        {
            if(!Page.IsPostBack)
            {
                gvEmployee.DataSource = BindGridView();
                gvEmployee.DataBind();
            }
        }

        protected DataTable BindGridView()
        {
            return accnt.DisplayUserAccounts(txtSearch.Text.Trim());
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

            DataView sortedView = new DataView(BindGridView());
            sortedView.Sort = e.SortExpression + " " + sortingDirection;
            Session["SortedView_mas_acc"] = sortedView;
            gvEmployee.DataSource = sortedView;
            gvEmployee.DataBind();
        }

        protected void gvEmployee_RowDataBound(object sender, GridViewRowEventArgs e)
        {

        }

        protected void gvEmployee_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            dt = new DataTable();
            if(e.CommandName.Equals("addRecord"))
            {
                int index = Convert.ToInt32(e.CommandArgument);
                System.Text.StringBuilder sb = new System.Text.StringBuilder();

                dt = accnt.SelectUserAccounts((Guid)(gvEmployee.DataKeys[index].Value));
                lblRowId.Text = dt.Rows[0]["UserId"].ToString();
                txtEditName.Text = dt.Rows[0]["FullName"].ToString();
                DDLRole.SelectedValue = dt.Rows[0]["RoleId"].ToString();

                sb.Append(@"<script type='text/javascript'>");
                sb.Append("$('#updateModal').modal('show');");
                sb.Append(@"</script>");
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "EditShowModalScript", sb.ToString(), false);
            }
            else if(e.CommandName.Equals("editRecord"))
            {
                int index = Convert.ToInt32(e.CommandArgument);
                System.Text.StringBuilder sb = new System.Text.StringBuilder();

                dt = accnt.SelectUserAccounts((Guid)(gvEmployee.DataKeys[index].Value));
                lblRowId.Text = dt.Rows[0]["UserId"].ToString();
                txtEditName.Text = dt.Rows[0]["FullName"].ToString();
                DDLRole.SelectedValue = dt.Rows[0]["RoleId"].ToString();

                sb.Append(@"<script type='text/javascript'>");
                sb.Append("$('#updateModal').modal('show');");
                sb.Append(@"</script>");
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "EditShowModalScript", sb.ToString(), false);
            }
            else if(e.CommandName.Equals("deleteRecord"))
            {
                int index = Convert.ToInt32(e.CommandArgument);
                string rowId = ((Label)gvEmployee.Rows[index].FindControl("lblEmp_Id")).Text;
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
            if (Session["SortedView_leave_acc"] != null)
            {
                gvEmployee.DataSource = Session["SortedView_leave_acc"];
                gvEmployee.DataBind();
            }
            else
            {
                gvEmployee.DataSource = BindGridView();
                gvEmployee.DataBind();
            }
        }

        protected void gvEmployee_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
        {

        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            gvEmployee.DataSource = BindGridView();
            gvEmployee.DataBind();
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

        protected void btnUpdate_Click(object sender, EventArgs e)
        {
            accnt.ChangeRole(Guid.Parse(lblRowId.Text), DDLRole.SelectedItem.Text);

            gvEmployee.DataSource = BindGridView();
            gvEmployee.DataBind();

            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            sb.Append(@"<script type='text/javascript'>");
            sb.Append("$('#updateModal').modal('hide');");
            sb.Append(@"</script>");
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "EditHideModalScript", sb.ToString(), false);
        }

        protected void btnDelete_Click(object sender, EventArgs e)
        {
            Membership.DeleteUser(hfDeleteId.Value);

            gvEmployee.DataSource = BindGridView();
            gvEmployee.DataBind();

            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            sb.Append(@"<script type='text/javascript'>");
            sb.Append("$('#deleteModal').modal('hide');");
            sb.Append(@"</script>");
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "DeleteHideModalScript", sb.ToString(), false);
        }
    }
}
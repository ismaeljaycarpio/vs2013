using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Security;
using System.Data;

namespace AMS.MasterConfig
{
    public partial class RoleManager : System.Web.UI.Page
    {
        DAL.PositionManagement position = new DAL.PositionManagement();
        DAL.Filler filler = new DAL.Filler();
        DataTable dt;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                gvRoles.DataSource = BindGridView();
                gvRoles.DataBind();
                fillAddDropDowns();
            }
        }

        private DataTable BindGridView()
        {
            return position.DisplayPositions(txtSearch.Text);
        }

        protected void btnAddRole_Click(object sender, EventArgs e)
        {
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            gvRoles.DataSource = BindGridView();
            gvRoles.DataBind();
        }

        protected void gvRoles_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            this.gvRoles.PageIndex = e.NewPageIndex;
            if (Session["SortedView_roles"] != null)
            {
                gvRoles.DataSource = Session["SortedView_roles"];
                gvRoles.DataBind();
            }
            else
            {
                gvRoles.DataSource = BindGridView();
                gvRoles.DataBind();
            }
        }


        protected void btnSave_Click(object sender, EventArgs e)
        {
            //chk duplicate
            if (!position.CheckIfDuplicate(txtAddPosition.Text))
            {
                position.AddPosition(txtAddPosition.Text,
                ddlAddDepartment.SelectedValue.ToString());
            }


            gvRoles.DataSource = BindGridView();
            gvRoles.DataBind();

            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            sb.Append(@"<script type='text/javascript'>");
            sb.Append("$('#addModal').modal('hide');");
            sb.Append(@"</script>");
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "HideShowModalScript", sb.ToString(), false);
        }

        protected void btnUpdate_Click(object sender, EventArgs e)
        {

                position.UpdatePosition(
                    txtEditPosition.Text,
                    ddlEditDepartment.SelectedValue.ToString(),
                    lblRowId.Text);

        
            gvRoles.DataSource = BindGridView();
            gvRoles.DataBind();

            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            sb.Append(@"<script type='text/javascript'>");
            sb.Append("$('#updateModal').modal('hide');");
            sb.Append(@"</script>");
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "EditHideModalScript", sb.ToString(), false);
        }

        protected void gvRoles_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName.Equals("editRecord"))
            {
                dt = new DataTable();

                //load ddl
                fillEditDropDowns();

                int index = Convert.ToInt32(e.CommandArgument);

                System.Text.StringBuilder sb = new System.Text.StringBuilder();

                DAL.PositionManagement position = new DAL.PositionManagement();

                dt = position.GetPositionByRowId((int)gvRoles.DataKeys[index].Value);
                lblRowId.Text = dt.Rows[0]["Id"].ToString();
                ddlEditDepartment.SelectedValue = dt.Rows[0]["DepartmentId"].ToString();
                txtEditPosition.Text = dt.Rows[0]["Position"].ToString();

                sb.Append(@"<script type='text/javascript'>");
                sb.Append("$('#updateModal').modal('show');");
                sb.Append(@"</script>");
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "EditShowModalScript", sb.ToString(), false);
            }
        }

        protected void fillAddDropDowns()
        {
            //load dept
            ddlAddDepartment.DataSource = filler.fillDepartment();
            ddlAddDepartment.DataTextField = "Department";
            ddlAddDepartment.DataValueField = "Id";
            ddlAddDepartment.DataBind();
        }

        protected void fillEditDropDowns()
        {            
            //load dept
            ddlEditDepartment.DataSource = filler.fillDepartment();
            ddlEditDepartment.DataTextField = "Department";
            ddlEditDepartment.DataValueField = "Id";
            ddlEditDepartment.DataBind();
        }

        protected void gvRoles_Sorting(object sender, GridViewSortEventArgs e)
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
            Session["SortedView_roles"] = sortedView;
            gvRoles.DataSource = sortedView;
            gvRoles.DataBind();
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
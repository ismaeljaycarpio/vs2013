using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Web.Security;

namespace AMS.MasterConfig
{
    public partial class Department_Master : System.Web.UI.Page
    {
        DAL.Department dept = new DAL.Department();
        DataTable dt;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                gvDepartment.DataSource = BindGridView();
                gvDepartment.DataBind();

                //show/hide controls based on role
                if (!User.IsInRole("Admin") && !User.IsInRole("HR"))
                {
                    btnOpenModal.Visible = false;
                    gvDepartment.Columns[1].Visible = false;
                    gvDepartment.Columns[3].Visible = false;
                }
            }
        }

        private DataTable BindGridView()
        {
            return dept.DisplayDepartment();
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            //get user empid
            MembershipUser loggedUser = Membership.GetUser();
            string loggedUser_empId = loggedUser.UserName;

            if(!dept.CheckIfDuplicate(txtAddDepartment.Text))
            {
                dept.AddDepartment(txtAddDepartment.Text, loggedUser_empId);
            }
            else{

            }

            gvDepartment.DataSource = BindGridView();
            gvDepartment.DataBind();

            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            sb.Append(@"<script type='text/javascript'>");
            sb.Append("$('#addModal').modal('hide');");
            sb.Append(@"</script>");
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "HideShowModalScript", sb.ToString(), false);
        }

        protected void btnUpdate_Click(object sender, EventArgs e)
        {
            //get user empid
            MembershipUser loggedUser = Membership.GetUser();
            string loggedUser_empId = loggedUser.UserName;

            if(!dept.CheckIfDuplicate(txtEditDepartment.Text))
            {
                dept.UpdateDepartment(txtEditDepartment.Text,
                    loggedUser_empId,
                    lblRowId.Text);
            }


            gvDepartment.DataSource = BindGridView();
            gvDepartment.DataBind();

            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            sb.Append(@"<script type='text/javascript'>");
            sb.Append("$('#updateModal').modal('hide');");
            sb.Append(@"</script>");
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "EditHideModalScript", sb.ToString(), false);
        }

        protected void gvDepartment_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            string rowId = ((Label)gvDepartment.Rows[e.RowIndex].FindControl("lblRowId")).Text;
            dept.DeleteDepartment(rowId);

            gvDepartment.DataSource = BindGridView();
            gvDepartment.DataBind();
        }

        protected void gvDepartment_RowCommand(object sender, GridViewCommandEventArgs e)
        {           
            if (e.CommandName.Equals("editRecord"))
            {
                dt = new DataTable();
                int index = Convert.ToInt32(e.CommandArgument);

                System.Text.StringBuilder sb = new System.Text.StringBuilder();

                dt = dept.GetDepartment((int)(gvDepartment.DataKeys[index].Value));
                lblRowId.Text = dt.Rows[0]["Id"].ToString();
                txtEditDepartment.Text = dt.Rows[0]["Department"].ToString();

                sb.Append(@"<script type='text/javascript'>");
                sb.Append("$('#updateModal').modal('show');");
                sb.Append(@"</script>");
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "EditShowModalScript", sb.ToString(), false);
            }
        }

        protected void btnOpenModal_Click(object sender, EventArgs e)
        {
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            sb.Append(@"<script type='text/javascript'>");
            sb.Append("$('#addModal').modal('show');");
            sb.Append(@"</script>");
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "AddShowModalScript", sb.ToString(), false);
        }

        protected void gvDepartment_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            this.gvDepartment.PageIndex = e.NewPageIndex;
            if (Session["SortedView_dep"] != null)
            {
                gvDepartment.DataSource = Session["SortedView_dep"];
                gvDepartment.DataBind();
            }
            else
            {
                gvDepartment.DataSource = BindGridView();
                gvDepartment.DataBind();
            }
        }

        protected void gvDepartment_Sorting(object sender, GridViewSortEventArgs e)
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
            Session["SortedView_dep"] = sortedView;
            gvDepartment.DataSource = sortedView;
            gvDepartment.DataBind();
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
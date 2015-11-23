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
                BindData();

                //show/hide controls based on role
                if (!User.IsInRole("Admin") && !User.IsInRole("HR"))
                {
                    btnOpenModal.Visible = false;
                    gvDepartment.Columns[1].Visible = false;
                    gvDepartment.Columns[3].Visible = false;
                }
            }
        }

        private void BindData()
        {
            dt = new DataTable();
            dt = dept.DisplayDepartment();

            gvDepartment.DataSource = dt;
            gvDepartment.DataBind();
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

            BindData();

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


            BindData();

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

            BindData();
        }

        protected void gvDepartment_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            dt = new DataTable();
            int index = Convert.ToInt32(e.CommandArgument);
            if (e.CommandName.Equals("editRecord"))
            {
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
    }
}
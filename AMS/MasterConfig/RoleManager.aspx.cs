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
                BindData();
                fillAddDropDowns();
            }
        }

        private void BindData()
        {
            gvRoles.DataSource = position.DisplayPositions(txtSearch.Text);
            gvRoles.DataBind();
        }

        protected void btnAddRole_Click(object sender, EventArgs e)
        {
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            BindData();
        }

        protected void gvRoles_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            this.gvRoles.PageIndex = e.NewPageIndex;
            BindData();
        }


        protected void btnSave_Click(object sender, EventArgs e)
        {
            //save
            Guid RoleId = Guid.Parse(ddlAddRole.SelectedValue.ToString());

            //chk duplicate
            if (!position.CheckIfDuplicate(txtAddPosition.Text))
            {
                position.AddPosition(txtAddPosition.Text,
                RoleId,
                ddlAddDepartment.SelectedValue.ToString());
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
            if (!position.CheckIfDuplicate(txtEditPosition.Text))
            {
                position.UpdatePosition(
                    txtEditPosition.Text,
                    Guid.Parse(ddlEditRole.SelectedValue.ToString()),
                    ddlEditDepartment.SelectedValue.ToString(),
                    lblRowId.Text);
            }
         
            //!IMPORTANT -> should update users roles
            //not yet implemented
            //update users ->update roleId whose userId->@UserId


            BindData();

            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            sb.Append(@"<script type='text/javascript'>");
            sb.Append("$('#updateModal').modal('hide');");
            sb.Append(@"</script>");
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "EditHideModalScript", sb.ToString(), false);
        }

        protected void gvRoles_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            dt = new DataTable();

            //load ddl
            fillEditDropDowns();

            int index = Convert.ToInt32(e.CommandArgument);
            if (e.CommandName.Equals("editRecord"))
            {
                System.Text.StringBuilder sb = new System.Text.StringBuilder();

                DAL.PositionManagement position = new DAL.PositionManagement();

                dt = position.GetPositionByRowId((int)gvRoles.DataKeys[index].Value);
                lblRowId.Text = dt.Rows[0]["Id"].ToString();
                ddlEditRole.SelectedValue = dt.Rows[0]["RoleId"].ToString();
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
            //load role
            ddlAddRole.DataSource = filler.fillRoles();
            ddlAddRole.DataTextField = "RoleName";
            ddlAddRole.DataValueField = "RoleId";
            ddlAddRole.DataBind();

            //load dept
            ddlAddDepartment.DataSource = filler.fillDepartment();
            ddlAddDepartment.DataTextField = "Department";
            ddlAddDepartment.DataValueField = "Id";
            ddlAddDepartment.DataBind();
        }

        protected void fillEditDropDowns()
        {
            //load role
            ddlEditRole.DataSource = filler.fillRoles();
            ddlEditRole.DataTextField = "RoleName";
            ddlEditRole.DataValueField = "RoleId";
            ddlEditRole.DataBind();
            
            //load dept
            ddlEditDepartment.DataSource = filler.fillDepartment();
            ddlEditDepartment.DataTextField = "Department";
            ddlEditDepartment.DataValueField = "Id";
            ddlEditDepartment.DataBind();
        }
    }
}
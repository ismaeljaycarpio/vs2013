using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Security;
using System.Data;

namespace AMS.dev
{
    public partial class _default : System.Web.UI.Page
    {
        eHRISContextDataContext db = new eHRISContextDataContext();
        DataTable dt;
        protected void Page_Load(object sender, EventArgs e)
        {
            if(!Page.IsPostBack)
            {
                bindGridview();
            }
        }

        protected void gvRoles_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName.Equals("editRecord"))
            {
                dt = new DataTable();
                int index = Convert.ToInt32(e.CommandArgument);
                System.Text.StringBuilder sb = new System.Text.StringBuilder();

                var q = (from role in db.Roles
                     where role.RoleId == Guid.Parse(gvRoles.DataKeys[index].Value.ToString())
                     select role).FirstOrDefault();

                lblRowId.Text = q.RoleId.ToString();
                txtEditRole.Text = q.RoleName;

                sb.Append(@"<script type='text/javascript'>");
                sb.Append("$('#updateModal').modal('show');");
                sb.Append(@"</script>");
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "EditShowModalScript", sb.ToString(), false);
            }
            else if (e.CommandName.Equals("deleteRecord"))
            {
                int index = Convert.ToInt32(e.CommandArgument);
                string rowId = ((Label)gvRoles.Rows[index].FindControl("lblRowId")).Text;
                hfDeleteId.Value = rowId;

                System.Text.StringBuilder sb = new System.Text.StringBuilder();
                sb.Append(@"<script type='text/javascript'>");
                sb.Append("$('#deleteModal').modal('show');");
                sb.Append(@"</script>");
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "DeleteShowModalScript", sb.ToString(), false);
            }
        }

        protected void gvRoles_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvRoles.PageIndex = e.NewPageIndex;
            bindGridview();
        }

        protected void bindGridview()
        {
            gvRoles.DataSource = db.Roles.ToList();
            gvRoles.DataBind();
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            if(!Roles.RoleExists(txtAddRole.Text))
            {
                Roles.CreateRole(txtAddRole.Text);

                bindGridview();

                System.Text.StringBuilder sb = new System.Text.StringBuilder();
                sb.Append(@"<script type='text/javascript'>");
                sb.Append("$('#addModal').modal('hide');");
                sb.Append(@"</script>");
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "HideShowModalScript", sb.ToString(), false);
            }
        }

        protected void btnUpdate_Click(object sender, EventArgs e)
        {
            var q = (from role in db.Roles
                     where role.RoleId == Guid.Parse(lblRowId.Text)
                     select role).FirstOrDefault();

            q.RoleName = txtEditRole.Text;
            db.SubmitChanges();

            bindGridview();

            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            sb.Append(@"<script type='text/javascript'>");
            sb.Append("$('#updateModal').modal('hide');");
            sb.Append(@"</script>");
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "EditHideModalScript", sb.ToString(), false);
        }

        protected void btnDelete_Click(object sender, EventArgs e)
        {
            Role role = db.Roles.Single(r => r.RoleId.Equals(Guid.Parse(hfDeleteId.Value)));
            db.Roles.DeleteOnSubmit(role);
            db.SubmitChanges();

            bindGridview();

            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            sb.Append(@"<script type='text/javascript'>");
            sb.Append("$('#deleteModal').modal('hide');");
            sb.Append(@"</script>");
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "DeleteHideModalScript", sb.ToString(), false);
        }
    }
}
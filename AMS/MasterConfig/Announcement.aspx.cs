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
    public partial class Announcement : System.Web.UI.Page
    {
        DAL.Announcement ann = new DAL.Announcement();
        DataTable dt;

        protected void Page_Load(object sender, EventArgs e)
        {
            if(!Page.IsPostBack)
            {
                BindData();
            }
        }

        private void BindData()
        {
            gvAnn.DataSource = ann.displayAnn();
            gvAnn.DataBind();
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            ann.addAnn(ddlType.SelectedItem.Text, txtTitle.Text, txtContent.Text);

            BindData();

            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            sb.Append(@"<script type='text/javascript'>");
            sb.Append("$('#addModal').modal('hide');");
            sb.Append(@"</script>");
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "HideShowModalScript", sb.ToString(), false);
        }

        protected void btnUpdate_Click(object sender, EventArgs e)
        {
            ann.updateAnn(ddlEditType.SelectedItem.Text, 
                txtEditTitle.Text, 
                txtEditContent.Text, lblRowId.Text);

            BindData();

            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            sb.Append(@"<script type='text/javascript'>");
            sb.Append("$('#updateModal').modal('hide');");
            sb.Append(@"</script>");
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "EditHideModalScript", sb.ToString(), false);
        }

        protected void btnDelete_Click(object sender, EventArgs e)
        {
            ann.deleteAnn(hfDeleteId.Value);

            BindData();

            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            sb.Append(@"<script type='text/javascript'>");
            sb.Append("$('#deleteModal').modal('hide');");
            sb.Append(@"</script>");
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "DeleteHideModalScript", sb.ToString(), false);
        }

        protected void gvAnn_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName.Equals("editRecord"))
            {
                dt = new DataTable();
                int index = Convert.ToInt32(e.CommandArgument);
                System.Text.StringBuilder sb = new System.Text.StringBuilder();

                dt = ann.getAnnById((int)(gvAnn.DataKeys[index].Value));
                lblRowId.Text = dt.Rows[0]["Id"].ToString();
                ddlEditType.SelectedItem.Text = dt.Rows[0]["Type"].ToString();
                txtEditTitle.Text = dt.Rows[0]["Title"].ToString();
                txtEditContent.Text = dt.Rows[0]["Content"].ToString();

                sb.Append(@"<script type='text/javascript'>");
                sb.Append("$('#updateModal').modal('show');");
                sb.Append(@"</script>");
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "EditShowModalScript", sb.ToString(), false);
            }
            else if (e.CommandName.Equals("deleteRecord"))
            {
                int index = Convert.ToInt32(e.CommandArgument);
                string rowId = ((Label)gvAnn.Rows[index].FindControl("lblRowId")).Text;
                hfDeleteId.Value = rowId;

                System.Text.StringBuilder sb = new System.Text.StringBuilder();
                sb.Append(@"<script type='text/javascript'>");
                sb.Append("$('#deleteModal').modal('show');");
                sb.Append(@"</script>");
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "DeleteShowModalScript", sb.ToString(), false);
            }
        }

        protected void gvAnn_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {

        }
    }
}
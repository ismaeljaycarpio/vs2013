using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

namespace AMS.Employee
{
    public partial class Awards : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                if (Session["UserId"] == null)
                    Response.Redirect("~/Employee/Employee");

                hfUserId.Value = Session["UserId"].ToString();
                BindData();

                //show/hide controls based on role
                if (!User.IsInRole("Admin") && !User.IsInRole("HR"))
                {
                    btnOpenModal.Visible = false;
                    gvAwards.Columns[1].Visible = false;
                    gvAwards.Columns[5].Visible = false;
                }                
            }
        }

        private void BindData()
        {
            DAL.Award awards = new DAL.Award();
            DataTable dt = new DataTable();

            Guid UserId = Guid.Parse(hfUserId.Value);
            dt = awards.getAwardsById(UserId);

            gvAwards.DataSource = dt;
            gvAwards.DataBind();
        }

        protected void btnOpenModal_Click(object sender, EventArgs e)
        {
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            sb.Append(@"<script type='text/javascript'>");
            sb.Append("$('#addModal').modal('show');");
            sb.Append(@"</script>");
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "AddShowModalScript", sb.ToString(), false);
        }

        protected void gvAwards_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            string rowId = ((Label)gvAwards.Rows[e.RowIndex].FindControl("lblRowId")).Text;
            DAL.Award awards = new DAL.Award();
            awards.deleteAward(rowId);

            BindData();
        }

        protected void gvAwards_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            DataTable dt = new DataTable();

            int index = Convert.ToInt32(e.CommandArgument);
            if (e.CommandName.Equals("editRecord"))
            {
                System.Text.StringBuilder sb = new System.Text.StringBuilder();

                DAL.Award awards = new DAL.Award();
                dt = awards.getAwardExByRowId((int)(gvAwards.DataKeys[index].Value));
                lblRowId.Text = dt.Rows[0]["Id"].ToString();
                txtEditDescription.Text = dt.Rows[0]["Description"].ToString();
                txtEditVenue.Text = dt.Rows[0]["Venue"].ToString();
                txtEditDate.Text = dt.Rows[0]["Date"].ToString();

                sb.Append(@"<script type='text/javascript'>");
                sb.Append("$('#updateModal').modal('show');");
                sb.Append(@"</script>");
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "EditShowModalScript", sb.ToString(), false);
            }
        }

        protected void btnUpdate_Click(object sender, EventArgs e)
        {
            DAL.Award awards = new DAL.Award();
            awards.updateAward(txtEditDescription.Text,
                txtEditVenue.Text,
                txtEditDate.Text,
                lblRowId.Text);

            BindData();

            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            sb.Append(@"<script type='text/javascript'>");
            sb.Append("$('#updateModal').modal('hide');");
            sb.Append(@"</script>");
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "EditHideModalScript", sb.ToString(), false);
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            DAL.Award awards = new DAL.Award();
            awards.addAward(Guid.Parse(hfUserId.Value),
                txtAddDescription.Text,
                txtAddVenue.Text,
                txtAddDate.Text);

            BindData();

            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            sb.Append(@"<script type='text/javascript'>");
            sb.Append("$('#addModal').modal('hide');");
            sb.Append(@"</script>");
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "HideShowModalScript", sb.ToString(), false);
        }
    }
}
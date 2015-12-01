using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;


namespace AMS.Employee
{
    public partial class EmployeeMovement : System.Web.UI.Page
    {
        //init
        DAL.EmployeeMovement emov = new DAL.EmployeeMovement();
        DAL.Filler filler = new DAL.Filler();
        DataTable dt;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                if (Session["UserId"] == null)
                {
                    Response.Redirect("~/Employee/Employee");
                }

                hfUserId.Value = Session["UserId"].ToString();

                BindData();

                if (!User.IsInRole("Admin") && !User.IsInRole("HR"))
                {
                    btnOpenModal.Visible = false;
                    gvEMovement.Columns[1].Visible = false;
                    gvEMovement.Columns[5].Visible = false;
                }

                ddlEMovement.DataSource = filler.fillEmployeeMovement();
                ddlEMovement.DataTextField = "EMovement";
                ddlEMovement.DataValueField = "Id";
                ddlEMovement.DataBind();

                ddlEditMovement.DataSource = filler.fillEmployeeMovement();
                ddlEditMovement.DataTextField = "EMovement";
                ddlEditMovement.DataValueField = "Id";
                ddlEditMovement.DataBind();
            }
        }

        private void BindData()
        {
            Guid UserId = Guid.Parse(hfUserId.Value);
            gvEMovement.DataSource = emov.DisplayEMovement(UserId);
            gvEMovement.DataBind();
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            emov.AddEMovement(
                ddlEMovement.SelectedValue.ToString(),
                Guid.Parse(hfUserId.Value),
                txtRemarks.Text,
                txtAddFromDate.Text,
                txtAddToDate.Text,
                txtEffectivityDate.Text);

            BindData();

            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            sb.Append(@"<script type='text/javascript'>");
            sb.Append("$('#addModal').modal('hide');");
            sb.Append(@"</script>");
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "HideShowModalScript", sb.ToString(), false);
        }

        protected void btnUpdate_Click(object sender, EventArgs e)
        {
            emov.UpdateEMovement(ddlEditMovement.SelectedValue.ToString(),
                Guid.Parse(hfUserId.Value),
                txtEditRemarks.Text,
                txtEditFromDate.Text,
                txtEditToDate.Text,
                txtEditEffectivityDate.Text,
                lblRowId.Text);

            BindData();

            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            sb.Append(@"<script type='text/javascript'>");
            sb.Append("$('#updateModal').modal('hide');");
            sb.Append(@"</script>");
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "EditHideModalScript", sb.ToString(), false);
        }

        protected void gvEMovement_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            string rowId = ((Label)gvEMovement.Rows[e.RowIndex].FindControl("lblRowId")).Text;
            emov.DeleteEMovement(rowId);

            BindData();
        }

        protected void gvEMovement_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            dt = new DataTable();

            int index = Convert.ToInt32(e.CommandArgument);
            if (e.CommandName.Equals("editRecord"))
            {
                System.Text.StringBuilder sb = new System.Text.StringBuilder();
                dt = emov.GetEMovementById((int)(gvEMovement.DataKeys[index].Value));
                lblRowId.Text = dt.Rows[0]["Id"].ToString();
                ddlEditMovement.SelectedValue = dt.Rows[0]["EMovementId"].ToString();
                txtEditRemarks.Text = dt.Rows[0]["Remarks"].ToString();
                txtEditFromDate.Text = dt.Rows[0]["ToDate"].ToString();
                txtEditToDate.Text = dt.Rows[0]["FromDate"].ToString();
                txtEditEffectivityDate.Text = dt.Rows[0]["EffectivityDate"].ToString();

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
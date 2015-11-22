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
                    gvEMovement.Columns[8].Visible = false;
                }

                DataTable dtEmp = new DataTable();
                dtEmp = filler.fillEmployeeMovement();

                ddlEMovement.DataSource = dtEmp;
                ddlEMovement.DataTextField = "EMovement";
                ddlEMovement.DataValueField = "Id";
                ddlEMovement.DataBind();

                ddlEditMovement.DataSource = dtEmp;
                ddlEditMovement.DataTextField = "EMovement";
                ddlEditMovement.DataValueField = "Id";
                ddlEditMovement.DataBind();
            }
        }

        private void BindData()
        {
            DataTable dt = new DataTable();
            Guid UserId = Guid.Parse(hfUserId.Value);
            dt = emov.displayEMovement(UserId);
            gvEMovement.DataSource = dt;
            gvEMovement.DataBind();
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            emov.addEMovement(
                ddlEMovement.SelectedValue.ToString(),
                Guid.Parse(hfUserId.Value),
                txtRemarks.Text,
                DateTime.Now.ToShortDateString());

            BindData();

            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            sb.Append(@"<script type='text/javascript'>");
            sb.Append("$('#addModal').modal('hide');");
            sb.Append(@"</script>");
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "HideShowModalScript", sb.ToString(), false);
        }

        protected void btnUpdate_Click(object sender, EventArgs e)
        {
            emov.updateEMovement(ddlEditMovement.SelectedValue.ToString(),
                Guid.Parse(hfUserId.Value),
                txtEditRemarks.Text,
                DateTime.Now.ToShortDateString(),
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
            emov.deleteEMovement(rowId);

            BindData();
        }

        protected void gvEMovement_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            DataTable dt = new DataTable();

            int index = Convert.ToInt32(e.CommandArgument);
            if (e.CommandName.Equals("editRecord"))
            {
                System.Text.StringBuilder sb = new System.Text.StringBuilder();
                dt = emov.getEMovementById((int)(gvEMovement.DataKeys[index].Value));
                lblRowId.Text = dt.Rows[0]["Id"].ToString();
                ddlEditMovement.SelectedValue = dt.Rows[0]["EMovementId"].ToString();
                txtEditRemarks.Text = dt.Rows[0]["Remarks"].ToString();

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
            ScriptManager.RegisterStartupScript(this, this.GetType(), "AddShowModalScript", sb.ToString(), false);
        }
    }
}
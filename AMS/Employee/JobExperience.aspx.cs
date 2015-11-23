using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

namespace AMS.Employee
{
    public partial class JobExperience : System.Web.UI.Page
    {
        DAL.Experience exp = new DAL.Experience();
        DataTable dt;
        System.Text.StringBuilder sb;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                if(Session["UserId"] == null)
                {
                    Response.Redirect("~/Employee/Employee");
                }

                hfUserId.Value = Session["UserId"].ToString();

                BindData();

                if(!User.IsInRole("Admin") && !User.IsInRole("HR"))
                {
                    btnOpenModal.Visible = false;
                    gvJobExp.Columns[1].Visible = false;
                    gvJobExp.Columns[8].Visible = false;
                }
            }
        }

        private void BindData()
        {
            Guid UserId = Guid.Parse(hfUserId.Value);
            dt = new DataTable();         
            dt = exp.getExperienceById(UserId);

            gvJobExp.DataSource = dt;
            gvJobExp.DataBind();
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            DAL.Experience exp = new DAL.Experience();
            exp.addExperience(Guid.Parse(hfUserId.Value),
                txtAddCompany.Text,
                txtAddJob.Text,
                txtAddFrom.Text,
                txtAddTo.Text,
                txtAddReason.Text,
                txtAddDescription.Text);

            BindData();

            sb = new System.Text.StringBuilder();
            sb.Append(@"<script type='text/javascript'>");
            sb.Append("$('#addModal').modal('hide');");
            sb.Append(@"</script>");
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "HideShowModalScript", sb.ToString(), false);
        }

        protected void btnUpdate_Click(object sender, EventArgs e)
        {
            exp.updateExperience(txtEditCompany.Text,
                txtEditJob.Text,
                txtEditFromDate.Text,
                txtEditToDate.Text,
                txtEditReason.Text,
                txtEditDescription.Text,
                lblRowId.Text);

            BindData();

            sb = new System.Text.StringBuilder();
            sb.Append(@"<script type='text/javascript'>");
            sb.Append("$('#updateModal').modal('hide');");
            sb.Append(@"</script>");
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "EditHideModalScript", sb.ToString(), false);
        }

        protected void gvJobExp_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            string rowId = ((Label)gvJobExp.Rows[e.RowIndex].FindControl("lblRowId")).Text;
            exp.deleteJobExp(rowId);
            BindData();
        }

        protected void gvJobExp_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            dt = new DataTable();
            int index = Convert.ToInt32(e.CommandArgument);

            if (e.CommandName.Equals("editRecord"))
            {
                sb = new System.Text.StringBuilder();

                dt = exp.getExperienceByRowId((int)(gvJobExp.DataKeys[index].Value));
                lblRowId.Text = dt.Rows[0]["Id"].ToString();
                txtEditCompany.Text = dt.Rows[0]["Company"].ToString();
                txtEditJob.Text = dt.Rows[0]["Job"].ToString();
                txtEditFromDate.Text = dt.Rows[0]["FromDate"].ToString();
                txtEditToDate.Text = dt.Rows[0]["ToDate"].ToString();
                txtEditReason.Text = dt.Rows[0]["Reason"].ToString();
                txtEditDescription.Text = dt.Rows[0]["Description"].ToString();

                sb.Append(@"<script type='text/javascript'>");
                sb.Append("$('#updateModal').modal('show');");
                sb.Append(@"</script>");
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "EditShowModalScript", sb.ToString(), false);
            }
        }

        protected void btnOpenModal_Click(object sender, EventArgs e)
        {
            sb = new System.Text.StringBuilder();
            sb.Append(@"<script type='text/javascript'>");
            sb.Append("$('#addModal').modal('show');");
            sb.Append(@"</script>");
            ScriptManager.RegisterStartupScript(this, this.GetType(), "AddShowModalScript", sb.ToString(), false);
        }
    }
}
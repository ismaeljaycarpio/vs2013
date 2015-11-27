using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Web.Security;

namespace AMS.Evaluation
{
    public partial class Config_Evaluation_Periond : System.Web.UI.Page
    {
        DAL.Evaluation_Config evalConfig = new DAL.Evaluation_Config();
        DAL.Employee emp = new DAL.Employee();
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
                    gvEvaluationPeriod.Columns[1].Visible = false;
                }
            }
        }

        private void BindData()
        {
            Guid loggedUserId = Guid.Parse(Membership.GetUser().ProviderUserKey.ToString());
            if(emp.GetPosition(loggedUserId) != "HR")
            {
                Response.Redirect("~/UnauthorizedAccess");
            }
            else
            {
                gvEvaluationPeriod.DataSource = evalConfig.DisplayEvaluationPeriod();
                gvEvaluationPeriod.DataBind();
            }
            
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            evalConfig.AddEvaluationPeriod(txtAddEvaluationPeriod.Text,
                txtStartDate.Text,
                txtEndDate.Text);

            BindData();

            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            sb.Append(@"<script type='text/javascript'>");
            sb.Append("$('#addModal').modal('hide');");
            sb.Append(@"</script>");
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "HideShowModalScript", sb.ToString(), false);
        }

        protected void btnUpdate_Click(object sender, EventArgs e)
        {
            evalConfig.UpdateEvaluationPeriod(
                txtEditEvaluationPeriod.Text,
                txtEditStartDate.Text,
                txtEditEndDate.Text,
                lblRowId.Text);

            BindData();

            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            sb.Append(@"<script type='text/javascript'>");
            sb.Append("$('#updateModal').modal('hide');");
            sb.Append(@"</script>");
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "EditHideModalScript", sb.ToString(), false);
        }

        protected void gvEvaluationPeriod_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {

        }

        protected void gvEvaluationPeriod_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            dt = new DataTable();
            int index = Convert.ToInt32(e.CommandArgument);
            if (e.CommandName.Equals("editRecord"))
            {
                System.Text.StringBuilder sb = new System.Text.StringBuilder();

                dt = evalConfig.GetEvaluationPeriod((int)(gvEvaluationPeriod.DataKeys[index].Value));
                lblRowId.Text = dt.Rows[0]["Id"].ToString();
                txtEditEvaluationPeriod.Text = dt.Rows[0]["EvaluationPeriod"].ToString();
                txtEditStartDate.Text = dt.Rows[0]["StartEvaluation"].ToString();
                txtEditEndDate.Text = dt.Rows[0]["EndEvaluation"].ToString();

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
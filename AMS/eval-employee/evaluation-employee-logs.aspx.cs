using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Security;

namespace AMS.eval_employee
{
    public partial class evaluation_employee_logs : System.Web.UI.Page
    {
        eHRISContextDataContext db = new eHRISContextDataContext();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["UserId"] == null)
            {
                Session["UserId"] = Membership.GetUser().ProviderUserKey.ToString();
            }

            hfUserId.Value = Session["UserId"].ToString();
        }

        protected void gvEmployeeEvaluation_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName.Equals("deleteRecord"))
            {
                int index = Convert.ToInt32(e.CommandArgument);
                string rowId = ((Label)gvEmployeeEvaluation.Rows[index].FindControl("lblRowId")).Text;
                hfDeleteId.Value = rowId;

                System.Text.StringBuilder sb = new System.Text.StringBuilder();
                sb.Append(@"<script type='text/javascript'>");
                sb.Append("$('#deleteModal').modal('show');");
                sb.Append(@"</script>");
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "DeleteShowModalScript", sb.ToString(), false);
            }
        }

        protected void gvEmployeeEvaluation_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
        {
            gvEmployeeEvaluation.SelectedIndex = Convert.ToInt32(e.NewSelectedIndex);
            Session["SelfEvaluationId"] = gvEmployeeEvaluation.SelectedDataKey.Value;
            Response.Redirect("~/eval-employee/evaluation-employee-form-view.aspx");
        }

        protected void EmployeeEvaluationDataSource_Selecting(object sender, LinqDataSourceSelectEventArgs e)
        {
            e.Result = (from se in db.SELF_EVALUATIONs
                        join emp in db.EMPLOYEEs
                        on se.EvaluatedBy equals emp.UserId
                        where
                        (se.UserId == Guid.Parse(hfUserId.Value)) &&
                        (se.Type == "Employee")
                        select new
                        {
                            Id = se.Id,
                            UserId = se.UserId,
                            DateEvaluated = se.DateEvaluated,
                            EvaluatedBy = emp.LastName + ", " + emp.FirstName + " " + emp.MiddleName
                        }).ToList();
        }

        protected void btnDelete_Click(object sender, EventArgs e)
        {
            var eval = db.SELF_EVALUATIONs.Single(i => i.Id == Convert.ToInt32(hfDeleteId.Value));
            db.SELF_EVALUATIONs.DeleteOnSubmit(eval);
            db.SubmitChanges();

            this.gvEmployeeEvaluation.DataBind();

            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            sb.Append(@"<script type='text/javascript'>");
            sb.Append("$('#deleteModal').modal('hide');");
            sb.Append(@"</script>");
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "DeleteShowModalScript", sb.ToString(), false);
        }
    }
}
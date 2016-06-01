using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Security;

namespace AMS.score_sheet
{
    public partial class score_sheet_logs : System.Web.UI.Page
    {
        eHRISContextDataContext db = new eHRISContextDataContext();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                if (Session["UserId"] == null)
                {
                    Session["UserId"] = Membership.GetUser().ProviderUserKey.ToString();
                }

                hfUserId.Value = Session["UserId"].ToString();

                if (Membership.GetUser().ProviderUserKey.ToString() == Session["UserId"].ToString())
                {
                    //hlScoresheet.Visible = false;
                }

                if(!User.IsInRole("Admin") && !User.IsInRole("HR"))
                {
                    hlScoresheet.Visible = false;
                }
            }
        }

        protected void gvScoresheet_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName.Equals("deleteRecord"))
            {
                int index = Convert.ToInt32(e.CommandArgument);
                string rowId = ((Label)gvScoresheet.Rows[index].FindControl("lblRowId")).Text;
                hfDeleteId.Value = rowId;

                System.Text.StringBuilder sb = new System.Text.StringBuilder();
                sb.Append(@"<script type='text/javascript'>");
                sb.Append("$('#deleteModal').modal('show');");
                sb.Append(@"</script>");
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "DeleteShowModalScript", sb.ToString(), false);
            }
        }

        protected void gvScoresheet_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
        {
            gvScoresheet.SelectedIndex = Convert.ToInt32(e.NewSelectedIndex);
            Session["SelfEvaluationId"] = gvScoresheet.SelectedDataKey.Value;
            Response.Redirect("~/score-sheet/score-sheet-view.aspx");
        }

        protected void btnDelete_Click(object sender, EventArgs e)
        {
            var eval = db.SELF_EVALUATIONs.Single(i => i.Id == Convert.ToInt32(hfDeleteId.Value));
            db.SELF_EVALUATIONs.DeleteOnSubmit(eval);
            db.SubmitChanges();

            this.gvScoresheet.DataBind();

            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            sb.Append(@"<script type='text/javascript'>");
            sb.Append("$('#deleteModal').modal('hide');");
            sb.Append(@"</script>");
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "DeleteShowModalScript", sb.ToString(), false);
        }

        protected void ScoresheetDataSource_Selecting(object sender, LinqDataSourceSelectEventArgs e)
        {
            e.Result = (from se in db.SELF_EVALUATIONs
                        where
                        (se.UserId == Guid.Parse(hfUserId.Value)) &&
                        (se.Type == "SCORE")
                        select new
                        {
                            Id = se.Id,
                            UserId = se.UserId,
                            DateEvaluated = se.DateEvaluated,
                            FullName = se.EMPLOYEE.LastName + ", " + se.EMPLOYEE.FirstName + " " + se.EMPLOYEE.MiddleName
                        }).ToList();
        }
    }
}
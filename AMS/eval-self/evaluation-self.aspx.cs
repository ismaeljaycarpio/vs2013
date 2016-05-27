using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Security;

namespace AMS.eval_self
{
    public partial class evaluation_self : System.Web.UI.Page
    {
        eHRISContextDataContext db = new eHRISContextDataContext();

        protected void Page_Load(object sender, EventArgs e)
        {
            if(!Page.IsPostBack)
            {
                if (Session["UserId"] == null)
                {
                    Session["UserId"] = Membership.GetUser().ProviderUserKey.ToString();
                }

                hfUserId.Value = Session["UserId"].ToString();

                if(Membership.GetUser().ProviderUserKey.ToString() != Session["UserId"].ToString())
                {
                    hlSelfEvaluation.Visible = false;
                }
            }
        }


        protected void gvSelfEvaluation_RowCommand(object sender, GridViewCommandEventArgs e)
        {

        }

        protected void SelfEvaluationDataSource_Selecting(object sender, LinqDataSourceSelectEventArgs e)
        {
            e.Result = (from se in db.SELF_EVALUATIONs
                        where
                        (se.UserId == Guid.Parse(hfUserId.Value)) &&
                        (se.Type == "Self")
                        select new
                        {
                            Id = se.Id,
                            UserId = se.UserId,
                            DateEvaluated = se.DateEvaluated,
                            FullName = se.EMPLOYEE.LastName + ", " + se.EMPLOYEE.FirstName + " " + se.EMPLOYEE.MiddleName
                        }).ToList();
        }

        protected void gvSelfEvaluation_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
        {
            gvSelfEvaluation.SelectedIndex = Convert.ToInt32(e.NewSelectedIndex);
            Session["SelfEvaluationId"] = gvSelfEvaluation.SelectedDataKey.Value;
            Response.Redirect("~/eval-self/evaluation-self-form-view.aspx");
        }
    }
}
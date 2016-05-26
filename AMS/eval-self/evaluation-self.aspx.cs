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

        }


        protected void gvSelfEvaluation_RowCommand(object sender, GridViewCommandEventArgs e)
        {

        }

        protected void SelfEvaluationDataSource_Selecting(object sender, LinqDataSourceSelectEventArgs e)
        {
            e.Result = (from se in db.SELF_EVALUATIONs
                        where 
                        (se.UserId == Guid.Parse(Membership.GetUser().ProviderUserKey.ToString())) &&
                        (se.Type == "Self")
                        select se).ToList();
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Collections;
using System.Web.Security;
using System.Text;

namespace AMS.Evaluation
{
    public partial class PendingApprovals : System.Web.UI.Page
    {
        DAL.Evaluation eval = new DAL.Evaluation();
        DAL.Employee emp = new DAL.Employee();
        DataTable dt;

        protected void Page_Load(object sender, EventArgs e)
        {
            if(!Page.IsPostBack)
            {
                MembershipUser mu = Membership.GetUser();
                Guid logged_UserId = Guid.Parse(mu.ProviderUserKey.ToString());

                //avoid HR Assistant evaluating
                if(emp.GetPosition(logged_UserId) == "HR Assistant")
                {
                    Response.Redirect("~/UnauthorizedAccess.aspx");
                }

                bindGridView();
            }
        }

        private void bindGridView()
        {
            //get current user
            MembershipUser _loggedUser = Membership.GetUser();
            Guid loggedUserId = Guid.Parse(_loggedUser.ProviderUserKey.ToString());
            string deptId = emp.GetDepartmentId(loggedUserId);
            dt = new DataTable();

            if(User.IsInRole("General Manager"))
            {
                //show director, division head, hr
                dt = eval.GetPendingApprovalGM();
            }
            else if(User.IsInRole("Director"))
            {
                //show division head and manager only
                dt = eval.GetPendingApprovalDirector();
            }
            else if(User.IsInRole("Division Head"))
            {
                //show manager only
                dt = eval.GetPendingApprovalDivisionHead();
            }
            else if(User.IsInRole("HR") || 
                User.IsInRole("Admin"))
            {
                dt = eval.GetPendingApprovalHR();
            }
            else if(User.IsInRole("Manager"))
            {
                dt = eval.GetPendingApprovalManager(deptId);
            }
            else if(User.IsInRole("Supervisor"))
            {
                dt = eval.GetPendingApprovalSupervisor(deptId);
            }
            else
            {
                dt = null;
            }
            

            gvPendingApprovals.DataSource = dt;
            gvPendingApprovals.DataBind();
        }

        protected void gvPendingApprovals_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvPendingApprovals.PageIndex = e.NewPageIndex;
            bindGridView();

        }
        protected void gvPendingApprovals_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
        {
            gvPendingApprovals.SelectedIndex = Convert.ToInt32(e.NewSelectedIndex);
            Session["EvaluationId"] = Convert.ToInt32(gvPendingApprovals.SelectedDataKey.Values["Id"]);
            Session["UserId"] = gvPendingApprovals.SelectedDataKey.Values["UserId"];
            Response.Redirect("~/Employee/vPerformanceEvaluation.aspx");
        }

        protected void gvPendingApprovals_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName.Equals("approveRecord"))
            {
                int index = Convert.ToInt32(e.CommandArgument);
                string rowId = ((Label)gvPendingApprovals.Rows[index].FindControl("lblRowId")).Text;

                hfApproveId.Value = rowId;

                System.Text.StringBuilder sb = new System.Text.StringBuilder();
                sb.Append(@"<script type='text/javascript'>");
                sb.Append("$('#approveModal').modal('show');");
                sb.Append(@"</script>");
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "DeleteShowModalScript", sb.ToString(), false);
            }
        }

        protected void gvPendingApprovals_Sorting(object sender, GridViewSortEventArgs e)
        {

        }

        protected void btnApprove_Click(object sender, EventArgs e)
        {
            if(User.IsInRole("HR"))
            {
                eval.ApprovePendingApprovalHR(hfApproveId.Value, Guid.Parse(Membership.GetUser().ProviderUserKey.ToString()));
            }
            else
            {
                eval.ApprovePendingApprovalManager(hfApproveId.Value, Guid.Parse(Membership.GetUser().ProviderUserKey.ToString()));
            }

            bindGridView();

            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            sb.Append(@"<script type='text/javascript'>");
            sb.Append("$('#approveModal').modal('hide');");
            sb.Append(@"</script>");
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "DeleteHideModalScript", sb.ToString(), false);
        }
    }
}
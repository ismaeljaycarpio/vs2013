using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Security;
using System.Data;
using System.IO;

namespace AMS.Employee
{
    public partial class Evaluation : System.Web.UI.Page
    {
        DAL.Evaluation eval = new DAL.Evaluation();
        DAL.Job job = new DAL.Job();
        DataTable dt;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                if (Session["UserId"] == null)
                {
                    Response.Redirect("~/Employee/Employee");
                }

                Guid UserId = Guid.Parse(Session["UserId"].ToString());
                BindGridView(UserId);

                hfAgency.Value = job.getAgencyName(UserId);


                //check ids
                MembershipUser loggedInUser = Membership.GetUser();
                Guid loggedUserId = Guid.Parse(loggedInUser.ProviderUserKey.ToString());

                //chk if user is evaluating itself
                if (loggedUserId.Equals(UserId))
                {
                    if (hfAgency.Value.Equals("TOPLIS Solutions Inc."))
                    {
                        btnPerfEval.Visible = false;
                        btnPerfEval.Enabled = false;
                    }
                    else
                    {
                        btnPerfEval.Visible = true;
                        btnPerfEval.Enabled = true;
                    }
                }

                //evaluate managers only ->for GM,HR
                if (User.IsInRole("General Manager") ||
                    User.IsInRole("HR"))
                {
                    //show if managers only
                    if (!job.getRoleName(UserId).Equals("Manager"))
                    {
                        btnPerfEval.Enabled = false;
                        btnPerfEval.Visible = false;
                    }
                }
            }
        }

        protected void BindGridView(Guid UserId)
        {
            gvEvaluation.DataSource = eval.displayMyEvaluation(UserId);
            gvEvaluation.DataBind();
        }

        protected void gvEvaluation_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {

        }

        protected void gvEvaluation_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
        {
            //Session["EvaluationId"] = gvEvaluation.SelectedValue.ToString();
            //Response.Redirect("~/Employee/vPerformanceEvaluation");
        }

        protected void btnPerfEval_Click(object sender, EventArgs e)
        {
            if (hfAgency.Value.Equals("TOPLIS Solutions Inc."))
            {
                Response.Redirect("~/Employee/PerformanceEvaluation");
            }
            else if (hfAgency.Value.Equals("PrimePower"))
            {
                Response.Redirect("~/Employee/Prime_Performance_Evaluation");
            }
            else
            {
                Response.Redirect("~/Employee/No_Agency");
            }
        }

        protected void btnSelfEval_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Employee/Self_Evaluation");
        }

        protected void gvEvaluation_SelectedIndexChanged(object sender, EventArgs e)
        {
            Session["EvaluationId"] = gvEvaluation.SelectedValue.ToString();
            Response.Redirect("~/Employee/vPerformanceEvaluation");
        }
    }
}
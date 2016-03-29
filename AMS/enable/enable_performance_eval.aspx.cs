using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using System.Web.Security;
using System.Collections;
using System.Text;

namespace AMS.enable
{
    public partial class enable_performance_eval : System.Web.UI.Page
    {
        DAL.Evaluation eval = new DAL.Evaluation();
        DAL.Employee emp = new DAL.Employee();
        eHRISContextDataContext db = new eHRISContextDataContext();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                if (Session["UserId"] == null)
                {
                    Response.Redirect("~/Employee/Employee.aspx");
                }

                //get selected user
                hfUserId.Value = Session["UserId"].ToString();
                Guid UserId = Guid.Parse(hfUserId.Value);

                lblEmpName.Text = emp.GetFullName(UserId);
                lblDepartment.Text = emp.GetDepartment(UserId);
                lblDateHired.Text = emp.GetHiredDate(UserId);
                lblEvalDate.Text = DateTime.Now.ToShortDateString();
                lblAgency.Text = emp.GetAgencyName(UserId);
                lblPosition.Text = emp.GetPosition(UserId);
                lblDateLastEvaluation.Text = emp.GetLastEvaluationDate(UserId);

                gvOrientation.DataSource = eval.getOrientation();
                gvOrientation.DataBind();

                gvBehavior.DataSource = eval.getBehavior();
                gvBehavior.DataBind();

                gvManagement.DataSource = eval.getManagement();
                gvManagement.DataBind();


                //check ids
                MembershipUser loggedInUser = Membership.GetUser();
                Guid loggedUserId = Guid.Parse(loggedInUser.ProviderUserKey.ToString());

                //chk if user is evaluating itself
                if (loggedUserId.Equals(UserId))
                {
                    //hide evaluator rating
                    gvOrientation.Columns[3].Visible = false;
                    gvBehavior.Columns[3].Visible = false;
                    gvManagement.Columns[3].Visible = false;


                    pnlEvaluatorsOnly.Visible = false;
                    txtNextEvaluationDate.Enabled = false;
                    RequiredFieldValidator3.Enabled = false;

                    pnlStaffOnly.Visible = true;
                }
                else
                {
                    //hide staff rating
                    gvOrientation.Columns[2].Visible = false;
                    gvBehavior.Columns[2].Visible = false;
                    gvManagement.Columns[2].Visible = false;

                    pnlEvaluatorsOnly.Visible = true;
                }
            }
        }

        protected void btnSumbit_Click(object sender, EventArgs e)
        {
            Page.Validate();
            //get loggeduserid
            Guid myUserId = Guid.Parse(Membership.GetUser().ProviderUserKey.ToString());
            Guid userId = Guid.Parse(hfUserId.Value);

            if (!myUserId.Equals(userId))
            {
                //evaluator
                EvaluationLINQ ev = new EvaluationLINQ();
                ev.UserId = userId;
                ev.EvaluationType = "Performance Evaluation";
                ev.EvaluatedById = myUserId;
                ev.DateEvaluated = DateTime.Now.ToShortDateString();
                ev.ApprovedByManagerId = Guid.Empty;
                ev.ApprovedByHRId = Guid.Empty;
                ev.Agency = lblAgency.Text;
                ev.NextEvaluationDate = txtNextEvaluationDate.Text;
                ev.EnableManagerStrength = txtEnableManagerStength.Text;
                ev.EnableNeedImprovement = txtEnableNeedImprovement.Text;
                ev.EnableRemarks = txtEnableRemarks.Text;

                db.EvaluationLINQs.InsertOnSubmit(ev);
                db.SubmitChanges();

                int evId = ev.Id;

                //insert scores

                //orientation
                foreach (GridViewRow row in gvOrientation.Rows)
                {
                    if (row.RowType == DataControlRowType.DataRow)
                    {
                        int competenceCatQId = int.Parse((row.FindControl("lblCompetenceCatQId") as Label).Text);
                        decimal evaluatorRating = decimal.Parse((row.FindControl("txtEvaluatorRating") as TextBox).Text);
                        string strRemarks = (row.FindControl("txtRemarks") as TextBox).Text;

                        Evaluation_Score sc = new Evaluation_Score();
                        sc.EvaluationId = evId;
                        sc.CompetenceCatQId = competenceCatQId;
                        sc.EvaluatorRating = evaluatorRating;
                        sc.Remarks = strRemarks;

                        db.Evaluation_Scores.InsertOnSubmit(sc);
                        db.SubmitChanges();
                    }
                }

                foreach (GridViewRow row in gvBehavior.Rows)
                {
                    if (row.RowType == DataControlRowType.DataRow)
                    {
                        int competenceCatQId = int.Parse((row.FindControl("lblCompetenceCatQId") as Label).Text);
                        decimal evaluatorRating = decimal.Parse((row.FindControl("txtEvaluatorRating") as TextBox).Text);
                        string strRemarks = (row.FindControl("txtRemarks") as TextBox).Text;

                        Evaluation_Score sc = new Evaluation_Score();
                        sc.EvaluationId = evId;
                        sc.CompetenceCatQId = competenceCatQId;
                        sc.EvaluatorRating = evaluatorRating;
                        sc.Remarks = strRemarks;

                        db.Evaluation_Scores.InsertOnSubmit(sc);
                        db.SubmitChanges();
                    }
                }

                foreach (GridViewRow row in gvManagement.Rows)
                {
                    if (row.RowType == DataControlRowType.DataRow)
                    {
                        int competenceCatQId = int.Parse((row.FindControl("lblCompetenceCatQId") as Label).Text);
                        decimal evaluatorRating = decimal.Parse((row.FindControl("txtEvaluatorRating") as TextBox).Text);
                        string strRemarks = (row.FindControl("txtRemarks") as TextBox).Text;

                        Evaluation_Score sc = new Evaluation_Score();
                        sc.EvaluationId = evId;
                        sc.CompetenceCatQId = competenceCatQId;
                        sc.EvaluatorRating = evaluatorRating;
                        sc.Remarks = strRemarks;

                        db.Evaluation_Scores.InsertOnSubmit(sc);
                        db.SubmitChanges();
                    }
                }
            }
            Response.Redirect("~/Employee/Evaluation.aspx");
        }
    }
}
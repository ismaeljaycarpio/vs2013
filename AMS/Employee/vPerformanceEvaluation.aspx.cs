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

namespace AMS.Employee
{
    public partial class vPerformanceEvaluation : System.Web.UI.Page
    {
        DAL.Evaluation eval = new DAL.Evaluation();
        DAL.Profile profile = new DAL.Profile();
        DAL.Job job = new DAL.Job();
        DataTable dt;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                if (Session["UserId"] == null)
                    Response.Redirect("~/Employee/Employee");

                //get selected user
                Guid UserId = Guid.Parse(Session["UserId"].ToString());

                //Get selected evaluation id
                int evaluationId = Convert.ToInt32(Session["EvaluationId"]);

                //get evaluation details
                dt = new DataTable();
                dt = eval.getEvaluated(evaluationId);

                if(dt.Rows[0]["EvaluationType"].ToString().Equals("Self Evaluation"))
                {
                    Response.Redirect("~/Employee/vSelf_Evaluation");
                }

                if (dt.Rows[0]["Agency"].ToString().Equals("PrimePower"))
                {
                    Response.Redirect("~/Employee/vPrime_Performance_Evaluation");
                }

                lblEmpName.Text = profile.getProfileName(UserId);

                lblAgency.Text = "TOPLIS Solutions Inc.";
                //lblAgency.Text = job.getAgencyName(UserId);
                lblDateHired.Text = job.getHiredDate(UserId);
                lblPosition.Text = job.getPosition(UserId);


                hfEvaluationId.Value = dt.Rows[0]["Id"].ToString();
                lblRemarksName.Text = dt.Rows[0]["RemarksName"].ToString();
                txtUnacceptable.Text = dt.Rows[0]["ImpUnacceptable"].ToString();
                txtFallShort.Text = dt.Rows[0]["ImpFallShort"].ToString();
                txtEffective.Text = dt.Rows[0]["ImpEffective"].ToString();
                txtHighlyEffective.Text = dt.Rows[0]["ImpHighlyEffective"].ToString();
                txtExceptional.Text = dt.Rows[0]["ImpExceptional"].ToString();
                txtRecommendation.Text = dt.Rows[0]["Recommendation"].ToString();
                txtNeedImpro.Text = dt.Rows[0]["NeedImprovement"].ToString();
                lblDateEvaluated.Text = dt.Rows[0]["DateEvaluated"].ToString();

                //approvals
                lblEvaluatedBy.Text = dt.Rows[0]["EvaluatedBy"].ToString();
                lblApprovedByManager.Text = dt.Rows[0]["ApprovedByManager"].ToString();
                lblApprovedByHRManager.Text = dt.Rows[0]["ApprovedByHR"].ToString();
                lblAckBy.Text = dt.Rows[0]["AcknowledgedBy"].ToString();

                //populate gridview
                BindData();
            }
        }

        private void BindData()
        {
            //get selected user
            Guid UserId = Guid.Parse(Session["UserId"].ToString());

            dt = new DataTable();
            dt = eval.display_filled_TSIQuestions(UserId);
            gvEvaluation.DataSource = dt;
            gvEvaluation.DataBind();
        }


        protected void btnUpdate_Click(object sender, EventArgs e)
        {
            decimal _scores = 0;
            decimal totalScore = 0;
            decimal formattedScores = 0;

            //insert to evaluation
            Guid UserId = Guid.Parse(Session["UserId"].ToString());
            MembershipUser _evaluatedBy = Membership.GetUser();
            Guid evaluatedById = ((Guid)_evaluatedBy.ProviderUserKey);
            string remarksName = "";
            string impUnacceptable = txtUnacceptable.Text;
            string impFallShort = txtFallShort.Text;
            string impEffective = txtEffective.Text;
            string impHighlyEffective = txtHighlyEffective.Text;
            string impExceptional = txtExceptional.Text;
            string reccomendation = txtRecommendation.Text;
            string needImprovement = txtNeedImpro.Text;
            string evaluatedBy = ""; //get who
            string approvedByManager = ""; //get who
            string approvedByHR = ""; //get who
            string AcknowledgedBy = ""; //get who

            //get approvals
            evaluatedBy = profile.getProfileName(evaluatedById);
            AcknowledgedBy = profile.getProfileName(UserId);

            //compute for scores
            foreach (GridViewRow row in gvEvaluation.Rows)
            {
                if (row.RowType == DataControlRowType.DataRow)
                {
                    _scores += decimal.Parse((row.FindControl("txtRating") as TextBox).Text);
                }
            }
            totalScore = _scores / gvEvaluation.Rows.Count;
            formattedScores = Decimal.Ceiling(totalScore);
            if (formattedScores == 1)
            {
                remarksName = "Unacceptable";
            }
            else if (formattedScores == 2)
            {
                remarksName = "Fall Short of Objectives";
            }
            else if (formattedScores == 3)
            {
                remarksName = "Effective";
            }
            else if (formattedScores == 4)
            {
                remarksName = "Highly Effective";
            }
            else if (formattedScores == 5)
            {
                remarksName = "Exceptional";
            }
            else
            {
                remarksName = "ERROR";
            }


            eval.updateEvaluation(
                evaluatedById,
                formattedScores, //gets ceiling
                remarksName,
                impUnacceptable,
                impFallShort,
                impEffective,
                impHighlyEffective,
                impExceptional,
                reccomendation,
                needImprovement,
                evaluatedBy,
                approvedByManager,
                approvedByHR,
                AcknowledgedBy,
                hfEvaluationId.Value.ToString());


            //get grid values
            foreach (GridViewRow row in gvEvaluation.Rows)
            {
                if (row.RowType == DataControlRowType.DataRow)
                {
                    int eval_score_id = int.Parse((row.FindControl("lblEvaluation_Score_Id") as Label).Text);
                    decimal rating = decimal.Parse((row.FindControl("txtRating") as TextBox).Text);

                    //edit 
                    eval.updateEvaluation_Scores(
                        eval_score_id,
                        rating);
                }
            }
            Response.Redirect("~/Employee/vPerformanceEvaluation");
        }

        protected void gvEvaluation_DataBound(object sender, EventArgs e)
        {

        }

        decimal _score = 0;
        protected void gvEvaluation_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                _score += decimal.Parse((e.Row.FindControl("txtRating") as TextBox).Text);
            }

            if (e.Row.RowType == DataControlRowType.Footer)
            {
                Label lblTotalRating = (Label)e.Row.FindControl("lblRating");
                lblTotalRating.Text = _score.ToString();
            }
        }
    }
}
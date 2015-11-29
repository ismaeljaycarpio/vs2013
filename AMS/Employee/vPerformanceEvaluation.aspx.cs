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
        DAL.Employee emp = new DAL.Employee();
        DataTable dt;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                if (Session["UserId"] == null)
                {
                    Response.Redirect("~/Employee/Employee");
                }
                    
                //get selected user
                hfUserId.Value = Session["UserId"].ToString();
                Guid UserId = Guid.Parse(hfUserId.Value);

                //Get selected evaluation id
                int evaluationId = Convert.ToInt32(Session["EvaluationId"]);

                //get evaluation details
                dt = new DataTable();
                dt = eval.getEvaluated(evaluationId);

                //chk evaluation type
                if(dt.Rows[0]["EvaluationType"].ToString().Equals("Self Evaluation"))
                {
                    Response.Redirect("~/Employee/vSelf_Evaluation");
                }

                if (dt.Rows[0]["Agency"].ToString().Equals("PrimePower"))
                {
                    Response.Redirect("~/Employee/vPrime_Performance_Evaluation");
                }

                lblEmpName.Text = emp.GetFullName(UserId);
                lblAgency.Text = emp.GetAgencyName(UserId);
                lblDateHired.Text = emp.GetHiredDate(UserId);
                lblPosition.Text = emp.GetPosition(UserId);
                lblDepartment.Text = emp.GetDepartment(UserId);
                lblDateLastEvaluation.Text = emp.GetLastEvaluationDate(UserId);
                txtNextEvaluationDate.Text = dt.Rows[0]["NextEvaluationDate"].ToString();

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
                lblEvalDate.Text = dt.Rows[0]["DateEvaluated"].ToString();

                //approvals
                lblEvaluatedBy.Text = emp.GetFullName(Guid.Parse(dt.Rows[0]["EvaluatedById"].ToString()));
                lblApprovedByManager.Text = dt.Rows[0]["ApprovedByManager"].ToString();
                lblApprovedByHRManager.Text = dt.Rows[0]["ApprovedByHR"].ToString();
                lblAckBy.Text = lblEmpName.Text;
                lblDateEvaluated.Text = dt.Rows[0]["DateEvaluated"].ToString();
                

                //populate gridview
                BindData();

                //check ids
                MembershipUser loggedInUser = Membership.GetUser();
                Guid loggedUserId = Guid.Parse(loggedInUser.ProviderUserKey.ToString());

                //hide evaluator
                if(loggedUserId.Equals(UserId))
                {
                    pnlEvaluator.Visible = false;
                    gvEvaluation.Columns[5].Visible = false;
                }
                else
                {
                    gvEvaluation.Columns[4].Visible = false;
                }
            }
        }

        private void BindData()
        {
            //get selected user
            Guid UserId = Guid.Parse(hfUserId.Value);

            dt = new DataTable();
            dt = eval.display_filled_TSIQuestions(UserId);
            gvEvaluation.DataSource = dt;
            gvEvaluation.DataBind();

            decimal total_staff = eval.display_filled_TSIQuestions(UserId).AsEnumerable().Sum(row => row.Field<decimal?>("StaffRating") == null ? 0 : row.Field<decimal>("StaffRating"));
            decimal total_evaluator = eval.display_filled_TSIQuestions(UserId).AsEnumerable().Sum(row => row.Field<decimal?>("EvaluatorRating") == null ? 0 : row.Field<decimal>("EvaluatorRating"));
            gvEvaluation.FooterRow.Cells[4].Text = total_staff.ToString();
            gvEvaluation.FooterRow.Cells[5].Text = total_evaluator.ToString();
        }


        protected void btnUpdate_Click(object sender, EventArgs e)
        {
            decimal _scores = 0;
            decimal totalScore = 0;
            decimal formattedScores = 0;

            //insert to evaluation
            Guid UserId = Guid.Parse(hfUserId.Value);
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
            string ApprovedByHR = ""; //get who
            string AcknowledgedBy = ""; //get who
            string ApprovedByManager = "";

            //get approvals
            AcknowledgedBy = emp.GetFullName(UserId);

            //check ids
            MembershipUser loggedInUser = Membership.GetUser();
            Guid loggedUserId = Guid.Parse(loggedInUser.ProviderUserKey.ToString());

            //evaluator
            if(!loggedUserId.Equals(UserId))
            {
                //compute for scores
                foreach (GridViewRow row in gvEvaluation.Rows)
                {
                    if (row.RowType == DataControlRowType.DataRow)
                    {
                        _scores += decimal.Parse((row.FindControl("txtEvaluatorRating") as TextBox).Text);
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

                //chk evaluator's role ->auto-approve
                if(User.IsInRole("HR"))
                {
                    //auto-approve HR
                    ApprovedByHR = loggedUserId.ToString();
                }
                else if(User.IsInRole("Manager"))
                {
                    //auto-approve Manager
                    ApprovedByManager = loggedUserId.ToString();
                }
                else if(User.IsInRole("General Manager"))
                {
                    ApprovedByManager = loggedUserId.ToString();
                    ApprovedByHR = loggedUserId.ToString();
                }
            }
            else
            {
                evaluatedById = Guid.Empty;
            }

            
            eval.UpdateEvaluation(
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
                ApprovedByManager,
                ApprovedByHR,
                hfEvaluationId.Value.ToString());

            //get grid values
            //evaluator
            if(!loggedUserId.Equals(UserId))
            {
                foreach (GridViewRow row in gvEvaluation.Rows)
                {
                    if (row.RowType == DataControlRowType.DataRow)
                    {
                        int eval_score_id = int.Parse((row.FindControl("lblEvaluation_Score_Id") as Label).Text);
                        decimal rating = decimal.Parse((row.FindControl("txtEvaluatorRating") as TextBox).Text);

                        //edit 
                        eval.updateEvaluation_Scores_Evaluator(
                            eval_score_id,
                            rating);
                    }
                }
            }
            else
            {
                foreach (GridViewRow row in gvEvaluation.Rows)
                {
                    if (row.RowType == DataControlRowType.DataRow)
                    {
                        int eval_score_id = int.Parse((row.FindControl("lblEvaluation_Score_Id") as Label).Text);
                        decimal rating = decimal.Parse((row.FindControl("txtStaffRating") as TextBox).Text);

                        //edit 
                        eval.updateEvaluation_Scores_Staff(
                            eval_score_id,
                            rating);
                    }
                }
            }
            
            Response.Redirect("~/Employee/vPerformanceEvaluation");
        }

        protected void gvEvaluation_DataBound(object sender, EventArgs e)
        {

        }

        //decimal _score = 0;
        //decimal _score_staff = 0;
        protected void gvEvaluation_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            //if (e.Row.RowType == DataControlRowType.DataRow)
            //{
            //    _score += decimal.Parse((e.Row.FindControl("txtEvaluatorRating") as TextBox).Text);
            //    _score_staff += decimal.Parse((e.Row.FindControl("txtStaffRating") as TextBox).Text);
            //}

            //if (e.Row.RowType == DataControlRowType.Footer)
            //{
            //    Label lblTotalRating = (Label)e.Row.FindControl("lblRating");
            //    lblTotalRating.Text = _score.ToString();

            //    Label lblRatingStaff = (Label)e.Row.FindControl("lblRatingStaff");
            //    lblRatingStaff.Text = _score_staff.ToString();
            //}
        }
    }
}

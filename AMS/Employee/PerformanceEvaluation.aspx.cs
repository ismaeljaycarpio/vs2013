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
    //designed for TOPLIS only
    public partial class PerformanceEvaluation : System.Web.UI.Page
    {
        DAL.Evaluation eval = new DAL.Evaluation();
        DAL.Employee emp = new DAL.Employee();
        DataTable dt;

        protected void Page_Load(object sender, EventArgs e)
        {
            if(!Page.IsPostBack)
            {
                if (Session["UserId"] == null)
                {
                    Response.Redirect("~/Employee/Employee");
                }

                //get selected user
                hfUserId.Value = Session["UserId"].ToString();
                Guid UserId = Guid.Parse(hfUserId.Value);

                lblEmpName.Text = emp.GetFullName(UserId);
                lblAgency.Text = emp.GetAgencyName(UserId);
                lblDateHired.Text = emp.GetHiredDate(UserId);
                lblPosition.Text = emp.GetPosition(UserId);
                lblDepartment.Text = emp.GetDepartment(UserId);
                lblEvalDate.Text = DateTime.Now.ToShortDateString();

                //get last eval date
                lblDateLastEvaluation.Text = emp.GetLastEvaluationDate(UserId);

                //populate gridview
                BindData();

                //chk id
                MembershipUser loggedInUser = Membership.GetUser();
                Guid loggedUserId = Guid.Parse(loggedInUser.ProviderUserKey.ToString());

                //chk if user is evaluating itself
                if(loggedUserId.Equals(UserId))
                {
                    //hide evaluator rating
                    gvEvaluation.Columns[5].Visible = false;
                }
                else
                {
                    //show evaluator panel
                    pnlEvaluatorOnly.Visible = true;
                    gvEvaluation.Columns[4].Visible = false;                  
                }
            }
        }

        private void BindData()
        {
            gvEvaluation.DataSource = eval.displayTSIQuestions();
            gvEvaluation.DataBind();
        }


        protected void btnSumbit_Click(object sender, EventArgs e)
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
            Guid ApprovedByManagerId = Guid.Empty;
            Guid ApprovedByHRId = Guid.Empty;


            //check ids
            MembershipUser loggedInUser = Membership.GetUser();
            Guid loggedUserId = Guid.Parse(loggedInUser.ProviderUserKey.ToString());

            //evaluator
            if(!loggedUserId.Equals(UserId))
            {
                //compute for scores for evaluator
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
                    ApprovedByHRId = loggedUserId;
                }
                else if(User.IsInRole("Manager"))
                {
                    //auto-approve Manager
                    ApprovedByManagerId = loggedUserId;
                }
                else if(User.IsInRole("General Manager"))
                {
                    ApprovedByManagerId = loggedUserId;
                    ApprovedByHRId = ApprovedByManagerId;
                }
            }
                //self
            else
            {
                evaluatedById = Guid.Empty;
            }

            
            int evaluationId = eval.InsertEvaluation(
                UserId,
                "Performance Evaluation",
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
                ApprovedByManagerId,
                ApprovedByHRId,
                lblAgency.Text,
                txtNextEvaluationDate.Text);

            //get grid values
            //evaluators
            if(!loggedUserId.Equals(UserId))
            {
                foreach (GridViewRow row in gvEvaluation.Rows)
                {
                    if (row.RowType == DataControlRowType.DataRow)
                    {
                        int competenceCatId = int.Parse((row.FindControl("lblCompetenceCatId") as Label).Text);
                        decimal rating = decimal.Parse((row.FindControl("txtEvaluatorRating") as TextBox).Text);

                        eval.addEvaluation_Scores_Evaluator(
                            evaluationId,
                            competenceCatId,
                            rating);
                    }
                }
            }
                //staff
            else
            {
                foreach (GridViewRow row in gvEvaluation.Rows)
                {
                    if (row.RowType == DataControlRowType.DataRow)
                    {
                        int competenceCatId = int.Parse((row.FindControl("lblCompetenceCatId") as Label).Text);
                        decimal rating = decimal.Parse((row.FindControl("txtStaffRating") as TextBox).Text);

                        eval.addEvaluation_Scores_Staff(
                            evaluationId,
                            competenceCatId,
                            rating);
                    }
                }
            }            
            Response.Redirect("~/Employee/Evaluation");
        }
    }
}
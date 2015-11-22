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

                //if (eval.IsEvaluated(UserId, DateTime.Now.Year))
                //{
                //    //redirect to update form
                //    Response.Redirect("~/Employee/vPerformanceEvaluation");
                //}

                lblEmpName.Text = emp.GetFullName(UserId);
                lblAgency.Text = emp.GetAgencyName(UserId);
                lblDateHired.Text = emp.GetHiredDate(UserId);
                lblPosition.Text = emp.GetPosition(UserId);

                //populate gridview
                BindData();
            }
        }

        private void BindData()
        {
            dt = new DataTable();

            dt = eval.displayTSIQuestions();
            gvEvaluation.DataSource = dt;
            gvEvaluation.DataBind();
        }


        protected void gvEvaluation_DataBound(object sender, EventArgs e)
        {
        }

        protected void gvEvaluation_RowDataBound(object sender, GridViewRowEventArgs e)
        {
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
            string evaluatedBy = ""; //get who
            string approvedByManager = ""; //get who
            string approvedByHR = ""; //get who
            string AcknowledgedBy = ""; //get who

            evaluatedBy = emp.GetFullName(UserId);
            AcknowledgedBy = emp.GetFullName(UserId);

            //compute for scores
            foreach(GridViewRow row in gvEvaluation.Rows)
            {
                if(row.RowType == DataControlRowType.DataRow)
                {
                    _scores += decimal.Parse((row.FindControl("txtRating") as TextBox).Text);
                }
            }
            totalScore = _scores / gvEvaluation.Rows.Count;
            formattedScores = Decimal.Ceiling(totalScore);
            if(formattedScores == 1)
            {
                remarksName = "Unacceptable";
            }
            else if(formattedScores == 2)
            {
                remarksName = "Fall Short of Objectives";
            }
            else if(formattedScores == 3)
            {
                remarksName = "Effective";
            }
            else if(formattedScores ==4)
            {
                remarksName = "Highly Effective";
            }
            else if(formattedScores == 5)
            {
                remarksName = "Exceptional";
            }
            else
            {
                remarksName = "ERROR";
            }


            int evaluationId = eval.insertEvaluation(
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
                evaluatedBy,
                approvedByManager,
                approvedByHR,
                AcknowledgedBy,
                lblAgency.Text);


            //get grid values
            foreach(GridViewRow row in gvEvaluation.Rows)
            {
                if(row.RowType == DataControlRowType.DataRow)
                {                
                    int competenceCatId = int.Parse((row.FindControl("lblCompetenceCatId") as Label).Text);
                    decimal rating = decimal.Parse((row.FindControl("txtRating") as TextBox).Text);

                    eval.addEvaluation_Scores(
                        evaluationId,
                        competenceCatId,
                        rating);
                }
            }
            Response.Redirect("~/Employee/Evaluation");
        }
    }
}
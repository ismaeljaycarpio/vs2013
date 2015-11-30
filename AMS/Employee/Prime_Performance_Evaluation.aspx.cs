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
    public partial class Prime_Performance_Evaluation : System.Web.UI.Page
    {
        DAL.Evaluation eval = new DAL.Evaluation();
        DAL.Employee emp = new DAL.Employee();

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

                lblEmpName.Text = emp.GetFullName(UserId);
                lblDepartment.Text = emp.GetDepartment(UserId);
                lblDateHired.Text = emp.GetHiredDate(UserId);
                lblEvalDate.Text = DateTime.Now.ToShortDateString();
                lblAgency.Text = emp.GetAgencyName(UserId);
                lblPosition.Text = emp.GetPosition(UserId);
                lblDateLastEvaluation.Text = emp.GetLastEvaluationDate(UserId);

                gvCooperation.DataSource = eval.getCooperation();
                gvCooperation.DataBind();

                gvAttendanceAndPunctuality.DataSource = eval.getAttendanceAndPunctuality();
                gvAttendanceAndPunctuality.DataBind();

                gvInterpersonalRelationship.DataSource = eval.getInterpersonalRelationship();
                gvInterpersonalRelationship.DataBind();

                gvAttitude.DataSource = eval.getAttitude();
                gvAttitude.DataBind();

                gvInitiative.DataSource = eval.getIniatitve();
                gvInitiative.DataBind();

                gvJudgement.DataSource = eval.getJudgement();
                gvJudgement.DataBind();

                gvCommunication.DataSource = eval.getCommunication();
                gvCommunication.DataBind();

                gvSafety.DataSource = eval.getSafety();
                gvSafety.DataBind();

                gvDependability.DataSource = eval.getDependability();
                gvDependability.DataBind();

                gvSpecificJobSkills.DataSource = eval.getSecificJobSkills();
                gvSpecificJobSkills.DataBind();

                gvProductivity.DataSource = eval.getProductivity();
                gvProductivity.DataBind();

                gvOrganizationalSkills.DataSource = eval.getOrganizationalSkill();
                gvOrganizationalSkills.DataBind();

                //check ids
                MembershipUser loggedInUser = Membership.GetUser();
                Guid loggedUserId = Guid.Parse(loggedInUser.ProviderUserKey.ToString());

                //chk if user is evaluating itself
                if (loggedUserId.Equals(UserId))
                {
                    //hide evaluator rating
                    gvCooperation.Columns[3].Visible = false;
                    gvAttendanceAndPunctuality.Columns[3].Visible = false;
                    gvInterpersonalRelationship.Columns[3].Visible = false;
                    gvAttitude.Columns[3].Visible = false;
                    gvInitiative.Columns[3].Visible = false;
                    gvJudgement.Columns[3].Visible = false;
                    gvCommunication.Columns[3].Visible = false;
                    gvSafety.Columns[3].Visible = false;
                    gvDependability.Columns[3].Visible = false;
                    gvSpecificJobSkills.Columns[3].Visible = false;
                    gvProductivity.Columns[3].Visible = false;
                    gvOrganizationalSkills.Columns[3].Visible = false;

                    pnlEvaluatorsOnly.Visible = false;
                    txtNextEvaluationDate.Enabled = false;
                    RequiredFieldValidator3.Enabled = false;
                }
                else
                {
                    //hide staff rating
                    gvCooperation.Columns[2].Visible = false;
                    gvAttendanceAndPunctuality.Columns[2].Visible = false;
                    gvInterpersonalRelationship.Columns[2].Visible = false;
                    gvAttitude.Columns[2].Visible = false;
                    gvInitiative.Columns[2].Visible = false;
                    gvJudgement.Columns[2].Visible = false;
                    gvCommunication.Columns[2].Visible = false;
                    gvSafety.Columns[2].Visible = false;
                    gvDependability.Columns[2].Visible = false;
                    gvSpecificJobSkills.Columns[2].Visible = false;
                    gvProductivity.Columns[2].Visible = false;
                    gvOrganizationalSkills.Columns[2].Visible = false;

                }

                if (!User.IsInRole("HR"))
                {
                    pnlHROnly.Visible = false;
                }
            }
        }

        protected void btnSumbit_Click(object sender, EventArgs e)
        {
            decimal section1a = 0;
            decimal section1b = 0;
            decimal section1c = 0;

            decimal section2a = 0;
            decimal section2b = 0;
            decimal section2c = 0;

            decimal section3a = 0;
            decimal section3b = 0;
            decimal section3c = 0;
            decimal section3d = 0;
            decimal section3e = 0;
            decimal section3f = 0;

            decimal total_section1 = 0;
            decimal total_section2 = 0;
            decimal total_section3 = 0;

            //get selected user
            Guid UserId = Guid.Parse(hfUserId.Value);

            //check ids
            MembershipUser loggedInUser = Membership.GetUser();
            Guid loggedUserId = Guid.Parse(loggedInUser.ProviderUserKey.ToString());

            //chk if evaluating itself
            if (!loggedUserId.Equals(UserId))
            {
                //compute score
                //Cooperation
                foreach (GridViewRow row in gvCooperation.Rows)
                {
                    if (row.RowType == DataControlRowType.DataRow)
                    {
                        section1a += decimal.Parse((row.FindControl("txtEvaluatorRating") as TextBox).Text);
                    }
                }

                //Attendance
                foreach (GridViewRow row in gvAttendanceAndPunctuality.Rows)
                {
                    if (row.RowType == DataControlRowType.DataRow)
                    {
                        section1b += decimal.Parse((row.FindControl("txtEvaluatorRating") as TextBox).Text);
                    }
                }

                //Interpersonal
                foreach (GridViewRow row in gvInterpersonalRelationship.Rows)
                {
                    if (row.RowType == DataControlRowType.DataRow)
                    {
                        section1c += decimal.Parse((row.FindControl("txtEvaluatorRating") as TextBox).Text);
                    }
                }

                //Attitde
                foreach (GridViewRow row in gvAttitude.Rows)
                {
                    if (row.RowType == DataControlRowType.DataRow)
                    {
                        section2a += decimal.Parse((row.FindControl("txtEvaluatorRating") as TextBox).Text);
                    }
                }

                //Initiative
                foreach (GridViewRow row in gvInitiative.Rows)
                {
                    if (row.RowType == DataControlRowType.DataRow)
                    {
                        section2b += decimal.Parse((row.FindControl("txtEvaluatorRating") as TextBox).Text);
                    }
                }

                //Judgement
                foreach (GridViewRow row in gvJudgement.Rows)
                {
                    if (row.RowType == DataControlRowType.DataRow)
                    {
                        section2c += decimal.Parse((row.FindControl("txtEvaluatorRating") as TextBox).Text);
                    }
                }

                //Communication
                foreach (GridViewRow row in gvCommunication.Rows)
                {
                    if (row.RowType == DataControlRowType.DataRow)
                    {
                        section3a += decimal.Parse((row.FindControl("txtEvaluatorRating") as TextBox).Text);
                    }
                }

                //Safety
                foreach (GridViewRow row in gvSafety.Rows)
                {
                    if (row.RowType == DataControlRowType.DataRow)
                    {
                        section3b += decimal.Parse((row.FindControl("txtEvaluatorRating") as TextBox).Text);
                    }
                }

                //Dependability
                foreach (GridViewRow row in gvDependability.Rows)
                {
                    if (row.RowType == DataControlRowType.DataRow)
                    {
                        section3c += decimal.Parse((row.FindControl("txtEvaluatorRating") as TextBox).Text);
                    }
                }

                //Specific job Skill
                foreach (GridViewRow row in gvSpecificJobSkills.Rows)
                {
                    if (row.RowType == DataControlRowType.DataRow)
                    {
                        section3d += decimal.Parse((row.FindControl("txtEvaluatorRating") as TextBox).Text);
                    }
                }

                //productivity
                foreach (GridViewRow row in gvProductivity.Rows)
                {
                    if (row.RowType == DataControlRowType.DataRow)
                    {
                        section3e += decimal.Parse((row.FindControl("txtEvaluatorRating") as TextBox).Text);
                    }
                }

                //Specific job Skill
                foreach (GridViewRow row in gvOrganizationalSkills.Rows)
                {
                    if (row.RowType == DataControlRowType.DataRow)
                    {
                        section3f += decimal.Parse((row.FindControl("txtEvaluatorRating") as TextBox).Text);
                    }
                }
            }

            total_section1 = (section1a + section1b + section1c);
            total_section2 = (section2a + section2b + section2c);
            total_section3 = (section3a + section3b + section3c + section3d + section3e + section3f);

            MembershipUser _evaluatedBy = Membership.GetUser();
            Guid evaluatedById = ((Guid)_evaluatedBy.ProviderUserKey);
            string AcknowledgedBy = emp.GetFullName(UserId);
            Guid ApprovedByManagerId = Guid.Empty;
            Guid ApprovedByHRId = Guid.Empty;
            int evaluationId = 0;

            //chk if user is evaluating itself
            if (loggedUserId.Equals(UserId))
            {
                evaluationId = eval.InsertEvaluation_Prime(
                        UserId,
                        "Performance Evaluation",
                        lblAgency.Text,
                        txtCommentSection1A.Text,
                        txtCommentSection1B.Text,
                        txtCommentSection1C.Text,
                        txtCommentSection2A.Text,
                        txtCommentSection2B.Text,
                        txtCommentSection2C.Text,
                        txtCommentSection3A.Text,
                        txtCommentSection3B.Text,
                        txtCommentSection3C.Text,
                        txtCommentSection3D.Text,
                        txtCommentSection3E.Text,
                        txtCommentSection3F.Text,
                        section1a,
                        section1b,
                        section1c,
                        section2a,
                        section2b,
                        section2c,
                        section3a,
                        section3b,
                        section3c,
                        section3d,
                        section3e,
                        section3f,
                        txtDaysSick.Text,
                        txtDaysTardy.Text,
                        txtCommentsNNotes.Text,
                        txtNextEvaluationDate.Text,
                        txtCreativeContribution.Text,
                        txtNewSkill.Text,
                        txtEmployeesStrength.Text,
                        txtImprovement.Text,
                        txtChanges.Text,
                        txtPersonalGoals.Text,
                        txtRecommendation.Text);
            }
            else
            {
                //chk evaluator's role ->auto-approve
                if (User.IsInRole("HR"))
                {
                    //auto-approve HR
                    ApprovedByHRId = loggedUserId;
                }
                else if (User.IsInRole("Manager"))
                {
                    //auto-approve Manager
                    ApprovedByManagerId = loggedUserId;
                }
                else if (User.IsInRole("General Manager"))
                {
                    ApprovedByManagerId = loggedUserId;
                    ApprovedByHRId = ApprovedByManagerId;
                }
                evaluationId = eval.InsertEvaluation_Prime(
                    UserId,
                    "Performance Evaluation",
                    evaluatedById,
                    ApprovedByManagerId,
                    ApprovedByHRId,
                    lblAgency.Text,
                    txtCommentSection1A.Text,
                    txtCommentSection1B.Text,
                    txtCommentSection1C.Text,
                    txtCommentSection2A.Text,
                    txtCommentSection2B.Text,
                    txtCommentSection2C.Text,
                    txtCommentSection3A.Text,
                    txtCommentSection3B.Text,
                    txtCommentSection3C.Text,
                    txtCommentSection3D.Text,
                    txtCommentSection3E.Text,
                    txtCommentSection3F.Text,
                    section1a,
                    section1b,
                    section1c,
                    section2a,
                    section2b,
                    section2c,
                    section3a,
                    section3b,
                    section3c,
                    section3d,
                    section3e,
                    section3f,
                    txtDaysSick.Text,
                    txtDaysTardy.Text,
                    txtCommentsNNotes.Text,
                    txtNextEvaluationDate.Text,
                    txtCreativeContribution.Text,
                    txtNewSkill.Text,
                    txtEmployeesStrength.Text,
                    txtImprovement.Text,
                    txtChanges.Text,
                    txtPersonalGoals.Text,
                    txtRecommendation.Text);
            }

             
            //Evaluators
            if (!loggedUserId.Equals(UserId))
            {
                //Cooperation
                foreach (GridViewRow row in gvCooperation.Rows)
                {
                    if (row.RowType == DataControlRowType.DataRow)
                    {
                        int competenceCatQId = int.Parse((row.FindControl("lblCompetenceCatQId") as Label).Text);
                        decimal rating = decimal.Parse((row.FindControl("txtEvaluatorRating") as TextBox).Text);

                        eval.addEvaluation_Scores_Prime_Evaluator(
                            evaluationId,
                            competenceCatQId,
                            rating);
                    }
                }

                //Attendance
                foreach (GridViewRow row in gvAttendanceAndPunctuality.Rows)
                {
                    if (row.RowType == DataControlRowType.DataRow)
                    {
                        int competenceCatQId = int.Parse((row.FindControl("lblCompetenceCatQId") as Label).Text);
                        decimal rating = decimal.Parse((row.FindControl("txtEvaluatorRating") as TextBox).Text);

                        eval.addEvaluation_Scores_Prime_Evaluator(
                            evaluationId,
                            competenceCatQId,
                            rating);
                    }
                }

                //Interpersonal
                foreach (GridViewRow row in gvInterpersonalRelationship.Rows)
                {
                    if (row.RowType == DataControlRowType.DataRow)
                    {
                        int competenceCatQId = int.Parse((row.FindControl("lblCompetenceCatQId") as Label).Text);
                        decimal rating = decimal.Parse((row.FindControl("txtEvaluatorRating") as TextBox).Text);

                        eval.addEvaluation_Scores_Prime_Evaluator(
                            evaluationId,
                            competenceCatQId,
                            rating);
                    }
                }

                //Attitde
                foreach (GridViewRow row in gvAttitude.Rows)
                {
                    if (row.RowType == DataControlRowType.DataRow)
                    {
                        int competenceCatQId = int.Parse((row.FindControl("lblCompetenceCatQId") as Label).Text);
                        decimal rating = decimal.Parse((row.FindControl("txtEvaluatorRating") as TextBox).Text);

                        eval.addEvaluation_Scores_Prime_Evaluator(
                            evaluationId,
                            competenceCatQId,
                            rating);
                    }
                }

                //Initiative
                foreach (GridViewRow row in gvInitiative.Rows)
                {
                    if (row.RowType == DataControlRowType.DataRow)
                    {
                        int competenceCatQId = int.Parse((row.FindControl("lblCompetenceCatQId") as Label).Text);
                        decimal rating = decimal.Parse((row.FindControl("txtEvaluatorRating") as TextBox).Text);

                        eval.addEvaluation_Scores_Prime_Evaluator(
                            evaluationId,
                            competenceCatQId,
                            rating);
                    }
                }

                //Judgement
                foreach (GridViewRow row in gvJudgement.Rows)
                {
                    if (row.RowType == DataControlRowType.DataRow)
                    {
                        int competenceCatQId = int.Parse((row.FindControl("lblCompetenceCatQId") as Label).Text);
                        decimal rating = decimal.Parse((row.FindControl("txtEvaluatorRating") as TextBox).Text);

                        eval.addEvaluation_Scores_Prime_Evaluator(
                            evaluationId,
                            competenceCatQId,
                            rating);
                    }
                }

                //Communication
                foreach (GridViewRow row in gvCommunication.Rows)
                {
                    if (row.RowType == DataControlRowType.DataRow)
                    {
                        int competenceCatQId = int.Parse((row.FindControl("lblCompetenceCatQId") as Label).Text);
                        decimal rating = decimal.Parse((row.FindControl("txtEvaluatorRating") as TextBox).Text);

                        eval.addEvaluation_Scores_Prime_Evaluator(
                            evaluationId,
                            competenceCatQId,
                            rating);
                    }
                }

                //Safety
                foreach (GridViewRow row in gvSafety.Rows)
                {
                    if (row.RowType == DataControlRowType.DataRow)
                    {
                        int competenceCatQId = int.Parse((row.FindControl("lblCompetenceCatQId") as Label).Text);
                        decimal rating = decimal.Parse((row.FindControl("txtEvaluatorRating") as TextBox).Text);

                        eval.addEvaluation_Scores_Prime_Evaluator(
                            evaluationId,
                            competenceCatQId,
                            rating);
                    }
                }

                //Dependability
                foreach (GridViewRow row in gvDependability.Rows)
                {
                    if (row.RowType == DataControlRowType.DataRow)
                    {
                        int competenceCatQId = int.Parse((row.FindControl("lblCompetenceCatQId") as Label).Text);
                        decimal rating = decimal.Parse((row.FindControl("txtEvaluatorRating") as TextBox).Text);

                        eval.addEvaluation_Scores_Prime_Evaluator(
                            evaluationId,
                            competenceCatQId,
                            rating);
                    }
                }

                //Specific job Skill
                foreach (GridViewRow row in gvSpecificJobSkills.Rows)
                {
                    if (row.RowType == DataControlRowType.DataRow)
                    {
                        int competenceCatQId = int.Parse((row.FindControl("lblCompetenceCatQId") as Label).Text);
                        decimal rating = decimal.Parse((row.FindControl("txtEvaluatorRating") as TextBox).Text);

                        eval.addEvaluation_Scores_Prime_Evaluator(
                            evaluationId,
                            competenceCatQId,
                            rating);
                    }
                }

                //productivity
                foreach (GridViewRow row in gvProductivity.Rows)
                {
                    if (row.RowType == DataControlRowType.DataRow)
                    {
                        int competenceCatQId = int.Parse((row.FindControl("lblCompetenceCatQId") as Label).Text);
                        decimal rating = decimal.Parse((row.FindControl("txtEvaluatorRating") as TextBox).Text);

                        eval.addEvaluation_Scores_Prime_Evaluator(
                            evaluationId,
                            competenceCatQId,
                            rating);
                    }
                }

                //Specific job Skill
                foreach (GridViewRow row in gvOrganizationalSkills.Rows)
                {
                    if (row.RowType == DataControlRowType.DataRow)
                    {
                        int competenceCatQId = int.Parse((row.FindControl("lblCompetenceCatQId") as Label).Text);
                        decimal rating = decimal.Parse((row.FindControl("txtEvaluatorRating") as TextBox).Text);

                        eval.addEvaluation_Scores_Prime_Evaluator(
                            evaluationId,
                            competenceCatQId,
                            rating);
                    }
                }
            }
            //User is first to evaluate
            else
            {
                //Cooperation
                foreach (GridViewRow row in gvCooperation.Rows)
                {
                    if (row.RowType == DataControlRowType.DataRow)
                    {
                        int competenceCatQId = int.Parse((row.FindControl("lblCompetenceCatQId") as Label).Text);
                        decimal rating = decimal.Parse((row.FindControl("txtStaffRating") as TextBox).Text);

                        eval.addEvaluation_Scores_Prime_Staff(
                            evaluationId,
                            competenceCatQId,
                            rating);
                    }
                }

                //Attendance
                foreach (GridViewRow row in gvAttendanceAndPunctuality.Rows)
                {
                    if (row.RowType == DataControlRowType.DataRow)
                    {
                        int competenceCatQId = int.Parse((row.FindControl("lblCompetenceCatQId") as Label).Text);
                        decimal rating = decimal.Parse((row.FindControl("txtStaffRating") as TextBox).Text);

                        eval.addEvaluation_Scores_Prime_Staff(
                            evaluationId,
                            competenceCatQId,
                            rating);
                    }
                }

                //Interpersonal
                foreach (GridViewRow row in gvInterpersonalRelationship.Rows)
                {
                    if (row.RowType == DataControlRowType.DataRow)
                    {
                        int competenceCatQId = int.Parse((row.FindControl("lblCompetenceCatQId") as Label).Text);
                        decimal rating = decimal.Parse((row.FindControl("txtStaffRating") as TextBox).Text);

                        eval.addEvaluation_Scores_Prime_Staff(
                            evaluationId,
                            competenceCatQId,
                            rating);
                    }
                }

                //Attitde
                foreach (GridViewRow row in gvAttitude.Rows)
                {
                    if (row.RowType == DataControlRowType.DataRow)
                    {
                        int competenceCatQId = int.Parse((row.FindControl("lblCompetenceCatQId") as Label).Text);
                        decimal rating = decimal.Parse((row.FindControl("txtStaffRating") as TextBox).Text);

                        eval.addEvaluation_Scores_Prime_Staff(
                            evaluationId,
                            competenceCatQId,
                            rating);
                    }
                }

                //Initiative
                foreach (GridViewRow row in gvInitiative.Rows)
                {
                    if (row.RowType == DataControlRowType.DataRow)
                    {
                        int competenceCatQId = int.Parse((row.FindControl("lblCompetenceCatQId") as Label).Text);
                        decimal rating = decimal.Parse((row.FindControl("txtStaffRating") as TextBox).Text);

                        eval.addEvaluation_Scores_Prime_Staff(
                            evaluationId,
                            competenceCatQId,
                            rating);
                    }
                }

                //Judgement
                foreach (GridViewRow row in gvJudgement.Rows)
                {
                    if (row.RowType == DataControlRowType.DataRow)
                    {
                        int competenceCatQId = int.Parse((row.FindControl("lblCompetenceCatQId") as Label).Text);
                        decimal rating = decimal.Parse((row.FindControl("txtStaffRating") as TextBox).Text);

                        eval.addEvaluation_Scores_Prime_Staff(
                            evaluationId,
                            competenceCatQId,
                            rating);
                    }
                }

                //Communication
                foreach (GridViewRow row in gvCommunication.Rows)
                {
                    if (row.RowType == DataControlRowType.DataRow)
                    {
                        int competenceCatQId = int.Parse((row.FindControl("lblCompetenceCatQId") as Label).Text);
                        decimal rating = decimal.Parse((row.FindControl("txtStaffRating") as TextBox).Text);

                        eval.addEvaluation_Scores_Prime_Staff(
                            evaluationId,
                            competenceCatQId,
                            rating);
                    }
                }

                //Safety
                foreach (GridViewRow row in gvSafety.Rows)
                {
                    if (row.RowType == DataControlRowType.DataRow)
                    {
                        int competenceCatQId = int.Parse((row.FindControl("lblCompetenceCatQId") as Label).Text);
                        decimal rating = decimal.Parse((row.FindControl("txtStaffRating") as TextBox).Text);

                        eval.addEvaluation_Scores_Prime_Staff(
                            evaluationId,
                            competenceCatQId,
                            rating);
                    }
                }

                //Dependability
                foreach (GridViewRow row in gvDependability.Rows)
                {
                    if (row.RowType == DataControlRowType.DataRow)
                    {
                        int competenceCatQId = int.Parse((row.FindControl("lblCompetenceCatQId") as Label).Text);
                        decimal rating = decimal.Parse((row.FindControl("txtStaffRating") as TextBox).Text);

                        eval.addEvaluation_Scores_Prime_Staff(
                            evaluationId,
                            competenceCatQId,
                            rating);
                    }
                }

                //Specific job Skill
                foreach (GridViewRow row in gvSpecificJobSkills.Rows)
                {
                    if (row.RowType == DataControlRowType.DataRow)
                    {
                        int competenceCatQId = int.Parse((row.FindControl("lblCompetenceCatQId") as Label).Text);
                        decimal rating = decimal.Parse((row.FindControl("txtStaffRating") as TextBox).Text);

                        eval.addEvaluation_Scores_Prime_Staff(
                            evaluationId,
                            competenceCatQId,
                            rating);
                    }
                }

                //productivity
                foreach (GridViewRow row in gvProductivity.Rows)
                {
                    if (row.RowType == DataControlRowType.DataRow)
                    {
                        int competenceCatQId = int.Parse((row.FindControl("lblCompetenceCatQId") as Label).Text);
                        decimal rating = decimal.Parse((row.FindControl("txtStaffRating") as TextBox).Text);

                        eval.addEvaluation_Scores_Prime_Staff(
                            evaluationId,
                            competenceCatQId,
                            rating);
                    }
                }

                //Specific job Skill
                foreach (GridViewRow row in gvOrganizationalSkills.Rows)
                {
                    if (row.RowType == DataControlRowType.DataRow)
                    {
                        int competenceCatQId = int.Parse((row.FindControl("lblCompetenceCatQId") as Label).Text);
                        decimal rating = decimal.Parse((row.FindControl("txtStaffRating") as TextBox).Text);

                        eval.addEvaluation_Scores_Prime_Staff(
                            evaluationId,
                            competenceCatQId,
                            rating);
                    }
                }
            }
            Session["EvaluationId"] = evaluationId;
            Response.Redirect("~/Employee/vPrime_Performance_Evaluation");
        }

        protected void gvCooperation_RowDataBound(object sender, GridViewRowEventArgs e)
        {
        }
    }
}
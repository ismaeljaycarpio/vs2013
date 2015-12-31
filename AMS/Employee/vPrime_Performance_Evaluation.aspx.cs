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
    public partial class vPrime_Performance_Evaluation : System.Web.UI.Page
    {
        DAL.Evaluation eval = new DAL.Evaluation();
        DAL.Employee emp = new DAL.Employee();
        DataTable dt;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                if(Session["UserId"] == null)
                {
                    Response.Redirect("~/Employee/Employee");
                }

                //get selected user
                hfUserId.Value = Session["UserId"].ToString();
                Guid UserId = Guid.Parse(hfUserId.Value);

                //Get selected evaluation id
                int evaluationId = Convert.ToInt32(Session["EvaluationId"]);

                lblEmpName.Text = emp.GetFullName(UserId);
                lblDepartment.Text = emp.GetDepartment(UserId);
                lblDateHired.Text = emp.GetHiredDate(UserId);
                lblAgency.Text = emp.GetAgencyName(UserId);
                lblPosition.Text = emp.GetPosition(UserId);
                lblDateLastEvaluation.Text = emp.GetLastEvaluationDate(UserId);

                //get evaluation details
                dt = new DataTable();
                dt = eval.GetEvaluated(evaluationId);
        
                //fill approval
                //approvals
                lblEvaluatedBy.Text = emp.GetFullName(Guid.Parse(dt.Rows[0]["EvaluatedById"].ToString()));
                if (Guid.Parse(dt.Rows[0]["ApprovedByManagerId"].ToString()).Equals(Guid.Empty))
                {
                    lblApprovedByManager.Text = "";
                }
                else
                {
                    lblApprovedByManager.Text = emp.GetFullName(Guid.Parse(dt.Rows[0]["ApprovedByManagerId"].ToString()));
                }
                if (Guid.Parse(dt.Rows[0]["ApprovedByHRId"].ToString()).Equals(Guid.Empty))
                {
                    lblApprovedByHR.Text = "";
                }
                else
                {
                    lblApprovedByHR.Text = emp.GetFullName(Guid.Parse(dt.Rows[0]["ApprovedByHRId"].ToString()));
                }
                lblAcknowledgeBy.Text = lblEmpName.Text;
                lblDateEvaluated.Text = dt.Rows[0]["DateEvaluated"].ToString();
                lblEvalDate.Text = dt.Rows[0]["DateEvaluated"].ToString();

                txtCommentSection1A.Text = dt.Rows[0]["CommentSection1A"].ToString();
                txtCommentSection1B.Text = dt.Rows[0]["CommentSection1B"].ToString();
                txtCommentSection1C.Text = dt.Rows[0]["CommentSection1C"].ToString();

                txtCommentSection2A.Text = dt.Rows[0]["CommentSection2A"].ToString();
                txtCommentSection2B.Text = dt.Rows[0]["CommentSection2B"].ToString();
                txtCommentSection2C.Text = dt.Rows[0]["CommentSection2C"].ToString();

                txtCommentSection3A.Text = dt.Rows[0]["CommentSection3A"].ToString();
                txtCommentSection3B.Text = dt.Rows[0]["CommentSection3B"].ToString();
                txtCommentSection3C.Text = dt.Rows[0]["CommentSection3C"].ToString();
                txtCommentSection3D.Text = dt.Rows[0]["CommentSection3D"].ToString();
                txtCommentSection3E.Text = dt.Rows[0]["CommentSection3E"].ToString();
                txtCommentSection3F.Text = dt.Rows[0]["CommentSection3F"].ToString();

                txtCreativeContribution.Text = dt.Rows[0]["EmployeesCreativeContribution"].ToString();
                txtNewSkill.Text = dt.Rows[0]["EmployeesNewSkills"].ToString();
                txtEmployeesStrength.Text = dt.Rows[0]["EmployeesStrength"].ToString();
                txtImprovement.Text = dt.Rows[0]["EmployeesImprovement"].ToString();
                txtChanges.Text = dt.Rows[0]["EmployeesChanges"].ToString();
                txtPersonalGoals.Text = dt.Rows[0]["EmployeesPersonalGoals"].ToString();
                txtRecommendation.Text = dt.Rows[0]["EmployeesRecommendation"].ToString();

                txtDaysSick.Text = dt.Rows[0]["DaysSick"].ToString();
                txtDaysTardy.Text = dt.Rows[0]["DaysTardy"].ToString();

                txtCommentsNNotes.Text = dt.Rows[0]["primeComments"].ToString();
                txtNextEvaluationDate.Text = dt.Rows[0]["NextEvaluationDate"].ToString();

                //display filled scores
                gvCooperation.DataSource = eval.getCooperation_filled(evaluationId);
                gvCooperation.DataBind();
                decimal total_coop_staff = eval.getCooperation_filled(evaluationId).AsEnumerable().Sum(row => row.Field<decimal?>("StaffRating") == null ? 0 : row.Field<decimal>("StaffRating"));
                decimal total_coop_evaluator = eval.getCooperation_filled(evaluationId).AsEnumerable().Sum(row => row.Field<decimal?>("EvaluatorRating") == null ? 0 : row.Field<decimal>("EvaluatorRating"));
                gvCooperation.FooterRow.Cells[2].Text = total_coop_staff.ToString();
                gvCooperation.FooterRow.Cells[3].Text = total_coop_evaluator.ToString();

                gvAttendanceAndPunctuality.DataSource = eval.getAttendanceAndPunctuality_filled(evaluationId);
                gvAttendanceAndPunctuality.DataBind();
                decimal total_attendance_staff = eval.getAttendanceAndPunctuality_filled(evaluationId).AsEnumerable().Sum(row => row.Field<decimal?>("StaffRating") == null ? 0 : row.Field<decimal>("StaffRating"));
                decimal total_attendance_evaluator = eval.getAttendanceAndPunctuality_filled(evaluationId).AsEnumerable().Sum(row => row.Field<decimal?>("EvaluatorRating") == null ? 0 : row.Field<decimal>("EvaluatorRating"));
                gvAttendanceAndPunctuality.FooterRow.Cells[2].Text = total_attendance_staff.ToString();
                gvAttendanceAndPunctuality.FooterRow.Cells[3].Text = total_attendance_evaluator.ToString();

                gvInterpersonalRelationship.DataSource = eval.getInterpersonalRelationship_filled(evaluationId);
                gvInterpersonalRelationship.DataBind();
                decimal total_interpersonal_staff = eval.getInterpersonalRelationship_filled(evaluationId).AsEnumerable().Sum(row => row.Field<decimal?>("StaffRating") == null ? 0 : row.Field<decimal>("StaffRating"));
                decimal total_interpersonal_evaluator = eval.getInterpersonalRelationship_filled(evaluationId).AsEnumerable().Sum(row => row.Field<decimal?>("EvaluatorRating") == null ? 0 : row.Field<decimal>("EvaluatorRating"));
                gvInterpersonalRelationship.FooterRow.Cells[2].Text = total_interpersonal_staff.ToString();
                gvInterpersonalRelationship.FooterRow.Cells[3].Text = total_interpersonal_evaluator.ToString();

                gvAttitude.DataSource = eval.getAttitude_filled(evaluationId);
                gvAttitude.DataBind();
                decimal total_attitude_staff = eval.getAttitude_filled(evaluationId).AsEnumerable().Sum(row => row.Field<decimal?>("StaffRating") == null ? 0 : row.Field<decimal>("StaffRating"));
                decimal total_attitude_evaluator = eval.getAttitude_filled(evaluationId).AsEnumerable().Sum(row => row.Field<decimal?>("EvaluatorRating") == null ? 0 : row.Field<decimal>("EvaluatorRating"));
                gvAttitude.FooterRow.Cells[2].Text = total_attitude_staff.ToString();
                gvAttitude.FooterRow.Cells[3].Text = total_attitude_evaluator.ToString();

                gvInitiative.DataSource = eval.getIniatitve_filled(evaluationId);
                gvInitiative.DataBind();
                decimal total_initiative_staff = eval.getIniatitve_filled(evaluationId).AsEnumerable().Sum(row => row.Field<decimal?>("StaffRating") == null ? 0 : row.Field<decimal>("StaffRating"));
                decimal total_initiative_evaluator = eval.getIniatitve_filled(evaluationId).AsEnumerable().Sum(row => row.Field<decimal?>("EvaluatorRating") == null ? 0 : row.Field<decimal>("EvaluatorRating"));
                gvInitiative.FooterRow.Cells[2].Text = total_initiative_staff.ToString();
                gvInitiative.FooterRow.Cells[3].Text = total_initiative_evaluator.ToString();

                gvJudgement.DataSource = eval.getJudgement_filled(evaluationId);
                gvJudgement.DataBind();
                decimal total_judgement_staff = eval.getJudgement_filled(evaluationId).AsEnumerable().Sum(row => row.Field<decimal?>("StaffRating") == null ? 0 : row.Field<decimal>("StaffRating"));
                decimal total_judgement_evaluator = eval.getJudgement_filled(evaluationId).AsEnumerable().Sum(row => row.Field<decimal?>("EvaluatorRating") == null ? 0 : row.Field<decimal>("EvaluatorRating"));
                gvJudgement.FooterRow.Cells[2].Text = total_judgement_staff.ToString();
                gvJudgement.FooterRow.Cells[3].Text = total_judgement_evaluator.ToString();

                gvCommunication.DataSource = eval.getCommunication_filled(evaluationId);
                gvCommunication.DataBind();
                decimal total_communication_staff = eval.getCommunication_filled(evaluationId).AsEnumerable().Sum(row => row.Field<decimal?>("StaffRating") == null ? 0 : row.Field<decimal>("StaffRating"));
                decimal total_communication_evaluator = eval.getCommunication_filled(evaluationId).AsEnumerable().Sum(row => row.Field<decimal?>("EvaluatorRating") == null ? 0 : row.Field<decimal>("EvaluatorRating"));
                gvCommunication.FooterRow.Cells[2].Text = total_communication_staff.ToString();
                gvCommunication.FooterRow.Cells[3].Text = total_communication_evaluator.ToString();

                gvSafety.DataSource = eval.getSafety_filled(evaluationId);
                gvSafety.DataBind();
                decimal total_safety_staff = eval.getSafety_filled(evaluationId).AsEnumerable().Sum(row => row.Field<decimal?>("StaffRating") == null ? 0 : row.Field<decimal>("StaffRating"));
                decimal total_safety_evaluator = eval.getSafety_filled(evaluationId).AsEnumerable().Sum(row => row.Field<decimal?>("EvaluatorRating") == null ? 0 : row.Field<decimal>("EvaluatorRating"));
                gvSafety.FooterRow.Cells[2].Text = total_safety_staff.ToString();
                gvSafety.FooterRow.Cells[3].Text = total_safety_evaluator.ToString();

                gvDependability.DataSource = eval.getDependability_filled(evaluationId);
                gvDependability.DataBind();
                decimal total_dependability_staff = eval.getDependability_filled(evaluationId).AsEnumerable().Sum(row => row.Field<decimal?>("StaffRating") == null ? 0 : row.Field<decimal>("StaffRating"));
                decimal total_dependability_evaluator = eval.getDependability_filled(evaluationId).AsEnumerable().Sum(row => row.Field<decimal?>("EvaluatorRating") == null ? 0 : row.Field<decimal>("EvaluatorRating"));
                gvDependability.FooterRow.Cells[2].Text = total_dependability_staff.ToString();
                gvDependability.FooterRow.Cells[3].Text = total_dependability_evaluator.ToString();

                gvSpecificJobSkills.DataSource = eval.getSecificJobSkills_filled(evaluationId);
                gvSpecificJobSkills.DataBind();
                decimal total_specificskill_staff = eval.getSecificJobSkills_filled(evaluationId).AsEnumerable().Sum(row => row.Field<decimal?>("StaffRating") == null ? 0 : row.Field<decimal>("StaffRating"));
                decimal total_specificskill_evaluator = eval.getSecificJobSkills_filled(evaluationId).AsEnumerable().Sum(row => row.Field<decimal?>("EvaluatorRating") == null ? 0 : row.Field<decimal>("EvaluatorRating"));
                gvSpecificJobSkills.FooterRow.Cells[2].Text = total_specificskill_staff.ToString();
                gvSpecificJobSkills.FooterRow.Cells[3].Text = total_specificskill_evaluator.ToString();

                gvProductivity.DataSource = eval.getProductivity_filled(evaluationId);
                gvProductivity.DataBind();
                decimal total_productivity_staff = eval.getProductivity_filled(evaluationId).AsEnumerable().Sum(row => row.Field<decimal?>("StaffRating") == null ? 0 : row.Field<decimal>("StaffRating"));
                decimal total_productivity_evaluator = eval.getProductivity_filled(evaluationId).AsEnumerable().Sum(row => row.Field<decimal?>("EvaluatorRating") == null ? 0 : row.Field<decimal>("EvaluatorRating"));
                gvProductivity.FooterRow.Cells[2].Text = total_productivity_staff.ToString();
                gvProductivity.FooterRow.Cells[3].Text = total_productivity_evaluator.ToString();

                gvOrganizationalSkills.DataSource = eval.getOrganizationalSkill_filled(evaluationId);
                gvOrganizationalSkills.DataBind();
                decimal total_orgskill_staff = eval.getOrganizationalSkill_filled(evaluationId).AsEnumerable().Sum(row => row.Field<decimal?>("StaffRating") == null ? 0 : row.Field<decimal>("StaffRating"));
                decimal total_orgskill_evaluator = eval.getOrganizationalSkill_filled(evaluationId).AsEnumerable().Sum(row => row.Field<decimal?>("EvaluatorRating") == null ? 0 : row.Field<decimal>("EvaluatorRating"));
                gvOrganizationalSkills.FooterRow.Cells[2].Text = total_orgskill_staff.ToString();
                gvOrganizationalSkills.FooterRow.Cells[3].Text = total_orgskill_evaluator.ToString();


                //fill hr use only
                lblSection1A.Text = dt.Rows[0]["Section1A"].ToString();
                lblSection1B.Text = dt.Rows[0]["Section1B"].ToString();
                lblSection1C.Text = dt.Rows[0]["Section1C"].ToString();
                decimal section1a = decimal.Parse(lblSection1A.Text);
                decimal section1b = decimal.Parse(lblSection1B.Text);
                decimal section1c = decimal.Parse(lblSection1C.Text);


                lblSection2a.Text = dt.Rows[0]["Section2A"].ToString();
                lblSection2b.Text = dt.Rows[0]["Section2B"].ToString();
                lblSection2c.Text = dt.Rows[0]["Section2C"].ToString();
                decimal section2a = decimal.Parse(lblSection2a.Text);
                decimal section2b = decimal.Parse(lblSection2b.Text);
                decimal section2c = decimal.Parse(lblSection2c.Text);

                lblSection3A.Text = dt.Rows[0]["Section3A"].ToString();
                lblSection3B.Text = dt.Rows[0]["Section3B"].ToString();
                lblSection3C.Text = dt.Rows[0]["Section3C"].ToString();
                lblSection3D.Text = dt.Rows[0]["Section3D"].ToString();
                lblSection3E.Text = dt.Rows[0]["Section3E"].ToString();
                lblSection3F.Text = dt.Rows[0]["Section3F"].ToString();
                decimal section3a = decimal.Parse(lblSection3A.Text);
                decimal section3b = decimal.Parse(lblSection3B.Text);
                decimal section3c = decimal.Parse(lblSection3C.Text);
                decimal section3d = decimal.Parse(lblSection3D.Text);
                decimal section3e = decimal.Parse(lblSection3E.Text);
                decimal section3f = decimal.Parse(lblSection3F.Text);

                lblTotalSection1.Text = (section1a + section1b + section1c).ToString();
                lblDivTotalSection1.Text = (Math.Round((decimal.Parse(lblTotalSection1.Text) / 3), 1)).ToString();

                lblTotalSection2.Text = (section2a + section2b + section2c).ToString();
                lblDivTotalSection2.Text = (Math.Round((decimal.Parse(lblTotalSection2.Text) / 3), 1)).ToString();

                lblTotalSection3.Text = (section3a + section3b + section3c +
                    section3d + section3e + section3f).ToString();
                lblDivTotalSection3.Text = (Math.Round((decimal.Parse(lblTotalSection3.Text) / 6), 1)).ToString();

                lblDivDivTotalSection1.Text = lblDivTotalSection1.Text;
                lblDivDivTotalSection2.Text = lblDivTotalSection2.Text;
                lblDivDivTotalSection3.Text = lblDivTotalSection3.Text;

                lblSection1P.Text = (decimal.Parse(lblDivDivTotalSection1.Text) * 0.10m).ToString();
                lblSection2P.Text = (decimal.Parse(lblDivDivTotalSection2.Text) * 0.30m).ToString();
                lblSection3P.Text = (decimal.Parse(lblDivDivTotalSection3.Text) * 0.60m).ToString();

                lblSectionTotalP.Text = (decimal.Parse(lblSection1P.Text) +
                    decimal.Parse(lblSection2P.Text) +
                    decimal.Parse(lblSection3P.Text)).ToString();

                //check ids
                MembershipUser loggedInUser = Membership.GetUser();
                Guid loggedUserId = Guid.Parse(loggedInUser.ProviderUserKey.ToString());

                //chk if user is updating itself
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

        protected void btnUpdate_Click(object sender, EventArgs e)
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
            Guid ApprovedByManagerId = Guid.Empty;
            Guid ApprovedByHRId = Guid.Empty;

            //Get selected evaluation id
            int evaluationId = Convert.ToInt32(Session["EvaluationId"]);

            //chk if user is evaluating itself
            if (loggedUserId.Equals(UserId))
            {

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
                eval.UpdateEvaluation_Prime(
                    evaluatedById,
                    ApprovedByManagerId,
                    ApprovedByHRId,
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
                    txtRecommendation.Text,
                    evaluationId);
            }

            //Evaluators
            if (!loggedUserId.Equals(UserId))
            {
                //Cooperation
                foreach (GridViewRow row in gvCooperation.Rows)
                {
                    if (row.RowType == DataControlRowType.DataRow)
                    {
                        int id = int.Parse((row.FindControl("lblCompetenceCatQId") as Label).Text);
                        decimal rating = decimal.Parse((row.FindControl("txtEvaluatorRating") as TextBox).Text);

                        eval.updateEvaluation_Scores_Prime_Evaluator(
                            id,
                            rating);
                    }
                }

                //Attendance
                foreach (GridViewRow row in gvAttendanceAndPunctuality.Rows)
                {
                    if (row.RowType == DataControlRowType.DataRow)
                    {
                        int id = int.Parse((row.FindControl("lblCompetenceCatQId") as Label).Text);
                        decimal rating = decimal.Parse((row.FindControl("txtEvaluatorRating") as TextBox).Text);

                        eval.updateEvaluation_Scores_Prime_Evaluator(
                            id,
                            rating);
                    }
                }

                //Interpersonal
                foreach (GridViewRow row in gvInterpersonalRelationship.Rows)
                {
                    if (row.RowType == DataControlRowType.DataRow)
                    {
                        int id = int.Parse((row.FindControl("lblCompetenceCatQId") as Label).Text);
                        decimal rating = decimal.Parse((row.FindControl("txtEvaluatorRating") as TextBox).Text);

                        eval.updateEvaluation_Scores_Prime_Evaluator(
                            id,
                            rating);
                    }
                }

                //Attitde
                foreach (GridViewRow row in gvAttitude.Rows)
                {
                    if (row.RowType == DataControlRowType.DataRow)
                    {
                        int id = int.Parse((row.FindControl("lblCompetenceCatQId") as Label).Text);
                        decimal rating = decimal.Parse((row.FindControl("txtEvaluatorRating") as TextBox).Text);

                        eval.updateEvaluation_Scores_Prime_Evaluator(
                            id,
                            rating);
                    }
                }

                //Initiative
                foreach (GridViewRow row in gvInitiative.Rows)
                {
                    if (row.RowType == DataControlRowType.DataRow)
                    {
                        int id = int.Parse((row.FindControl("lblCompetenceCatQId") as Label).Text);
                        decimal rating = decimal.Parse((row.FindControl("txtEvaluatorRating") as TextBox).Text);

                        eval.updateEvaluation_Scores_Prime_Evaluator(
                            id,
                            rating);
                    }
                }

                //Judgement
                foreach (GridViewRow row in gvJudgement.Rows)
                {
                    if (row.RowType == DataControlRowType.DataRow)
                    {
                        int id = int.Parse((row.FindControl("lblCompetenceCatQId") as Label).Text);
                        decimal rating = decimal.Parse((row.FindControl("txtEvaluatorRating") as TextBox).Text);

                        eval.updateEvaluation_Scores_Prime_Evaluator(
                            id,
                            rating);
                    }
                }

                //Communication
                foreach (GridViewRow row in gvCommunication.Rows)
                {
                    if (row.RowType == DataControlRowType.DataRow)
                    {
                        int id = int.Parse((row.FindControl("lblCompetenceCatQId") as Label).Text);
                        decimal rating = decimal.Parse((row.FindControl("txtEvaluatorRating") as TextBox).Text);

                        eval.updateEvaluation_Scores_Prime_Evaluator(
                            id,
                            rating);
                    }
                }

                //Safety
                foreach (GridViewRow row in gvSafety.Rows)
                {
                    if (row.RowType == DataControlRowType.DataRow)
                    {
                        int id = int.Parse((row.FindControl("lblCompetenceCatQId") as Label).Text);
                        decimal rating = decimal.Parse((row.FindControl("txtEvaluatorRating") as TextBox).Text);

                        eval.updateEvaluation_Scores_Prime_Evaluator(
                            id,
                            rating);
                    }
                }

                //Dependability
                foreach (GridViewRow row in gvDependability.Rows)
                {
                    if (row.RowType == DataControlRowType.DataRow)
                    {
                        int id = int.Parse((row.FindControl("lblCompetenceCatQId") as Label).Text);
                        decimal rating = decimal.Parse((row.FindControl("txtEvaluatorRating") as TextBox).Text);

                        eval.updateEvaluation_Scores_Prime_Evaluator(
                            id,
                            rating);
                    }
                }

                //Specific job Skill
                foreach (GridViewRow row in gvSpecificJobSkills.Rows)
                {
                    if (row.RowType == DataControlRowType.DataRow)
                    {
                        int id = int.Parse((row.FindControl("lblCompetenceCatQId") as Label).Text);
                        decimal rating = decimal.Parse((row.FindControl("txtEvaluatorRating") as TextBox).Text);

                        eval.updateEvaluation_Scores_Prime_Evaluator(
                            id,
                            rating);
                    }
                }

                //productivity
                foreach (GridViewRow row in gvProductivity.Rows)
                {
                    if (row.RowType == DataControlRowType.DataRow)
                    {
                        int id = int.Parse((row.FindControl("lblCompetenceCatQId") as Label).Text);
                        decimal rating = decimal.Parse((row.FindControl("txtEvaluatorRating") as TextBox).Text);

                        eval.updateEvaluation_Scores_Prime_Evaluator(
                            id,
                            rating);
                    }
                }

                //Specific job Skill
                foreach (GridViewRow row in gvOrganizationalSkills.Rows)
                {
                    if (row.RowType == DataControlRowType.DataRow)
                    {
                        int id = int.Parse((row.FindControl("lblCompetenceCatQId") as Label).Text);
                        decimal rating = decimal.Parse((row.FindControl("txtEvaluatorRating") as TextBox).Text);

                        eval.updateEvaluation_Scores_Prime_Evaluator(
                            id,
                            rating);
                    }
                }
            }
            //staff is editing 
            else
            {
                //Cooperation
                foreach (GridViewRow row in gvCooperation.Rows)
                {
                    if (row.RowType == DataControlRowType.DataRow)
                    {
                        int id = int.Parse((row.FindControl("lblCompetenceCatQId") as Label).Text);
                        decimal rating = decimal.Parse((row.FindControl("txtStaffRating") as TextBox).Text);

                        eval.updateEvaluation_Scores_Prime_Staff(
                            id,
                            rating);
                    }
                }

                //Attendance
                foreach (GridViewRow row in gvAttendanceAndPunctuality.Rows)
                {
                    if (row.RowType == DataControlRowType.DataRow)
                    {
                        int id = int.Parse((row.FindControl("lblCompetenceCatQId") as Label).Text);
                        decimal rating = decimal.Parse((row.FindControl("txtStaffRating") as TextBox).Text);

                        eval.updateEvaluation_Scores_Prime_Staff(
                            id,
                            rating);
                    }
                }

                //Interpersonal
                foreach (GridViewRow row in gvInterpersonalRelationship.Rows)
                {
                    if (row.RowType == DataControlRowType.DataRow)
                    {
                        int id = int.Parse((row.FindControl("lblCompetenceCatQId") as Label).Text);
                        decimal rating = decimal.Parse((row.FindControl("txtStaffRating") as TextBox).Text);

                        eval.updateEvaluation_Scores_Prime_Staff(
                            id,
                            rating);
                    }
                }

                //Attitde
                foreach (GridViewRow row in gvAttitude.Rows)
                {
                    if (row.RowType == DataControlRowType.DataRow)
                    {
                        int id = int.Parse((row.FindControl("lblCompetenceCatQId") as Label).Text);
                        decimal rating = decimal.Parse((row.FindControl("txtStaffRating") as TextBox).Text);

                        eval.updateEvaluation_Scores_Prime_Staff(
                            id,
                            rating);
                    }
                }

                //Initiative
                foreach (GridViewRow row in gvInitiative.Rows)
                {
                    if (row.RowType == DataControlRowType.DataRow)
                    {
                        int id = int.Parse((row.FindControl("lblCompetenceCatQId") as Label).Text);
                        decimal rating = decimal.Parse((row.FindControl("txtStaffRating") as TextBox).Text);

                        eval.updateEvaluation_Scores_Prime_Staff(
                            id,
                            rating);
                    }
                }

                //Judgement
                foreach (GridViewRow row in gvJudgement.Rows)
                {
                    if (row.RowType == DataControlRowType.DataRow)
                    {
                        int id = int.Parse((row.FindControl("lblCompetenceCatQId") as Label).Text);
                        decimal rating = decimal.Parse((row.FindControl("txtStaffRating") as TextBox).Text);

                        eval.updateEvaluation_Scores_Prime_Staff(
                            id,
                            rating);
                    }
                }

                //Communication
                foreach (GridViewRow row in gvCommunication.Rows)
                {
                    if (row.RowType == DataControlRowType.DataRow)
                    {
                        int id = int.Parse((row.FindControl("lblCompetenceCatQId") as Label).Text);
                        decimal rating = decimal.Parse((row.FindControl("txtStaffRating") as TextBox).Text);

                        eval.updateEvaluation_Scores_Prime_Staff(
                            id,
                            rating);
                    }
                }

                //Safety
                foreach (GridViewRow row in gvSafety.Rows)
                {
                    if (row.RowType == DataControlRowType.DataRow)
                    {
                        int id = int.Parse((row.FindControl("lblCompetenceCatQId") as Label).Text);
                        decimal rating = decimal.Parse((row.FindControl("txtStaffRating") as TextBox).Text);

                        eval.updateEvaluation_Scores_Prime_Staff(
                            id,
                            rating);
                    }
                }

                //Dependability
                foreach (GridViewRow row in gvDependability.Rows)
                {
                    if (row.RowType == DataControlRowType.DataRow)
                    {
                        int id = int.Parse((row.FindControl("lblCompetenceCatQId") as Label).Text);
                        decimal rating = decimal.Parse((row.FindControl("txtStaffRating") as TextBox).Text);

                        eval.updateEvaluation_Scores_Prime_Staff(
                            id,
                            rating);
                    }
                }

                //Specific job Skill
                foreach (GridViewRow row in gvSpecificJobSkills.Rows)
                {
                    if (row.RowType == DataControlRowType.DataRow)
                    {
                        int id = int.Parse((row.FindControl("lblCompetenceCatQId") as Label).Text);
                        decimal rating = decimal.Parse((row.FindControl("txtStaffRating") as TextBox).Text);

                        eval.updateEvaluation_Scores_Prime_Staff(
                            id,
                            rating);
                    }
                }

                //productivity
                foreach (GridViewRow row in gvProductivity.Rows)
                {
                    if (row.RowType == DataControlRowType.DataRow)
                    {
                        int id = int.Parse((row.FindControl("lblCompetenceCatQId") as Label).Text);
                        decimal rating = decimal.Parse((row.FindControl("txtStaffRating") as TextBox).Text);

                        eval.updateEvaluation_Scores_Prime_Staff(
                            id,
                            rating);
                    }
                }

                //Specific job Skill
                foreach (GridViewRow row in gvOrganizationalSkills.Rows)
                {
                    if (row.RowType == DataControlRowType.DataRow)
                    {
                        int id = int.Parse((row.FindControl("lblCompetenceCatQId") as Label).Text);
                        decimal rating = decimal.Parse((row.FindControl("txtStaffRating") as TextBox).Text);

                        eval.updateEvaluation_Scores_Prime_Staff(
                            id,
                            rating);
                    }
                }
            }
            //Response.Redirect("~/Employee/vPrime_Performance_Evaluation");
            //Response.Redirect(Request.RawUrl);
            Response.Redirect("~/Employee/post_back");
        }

        protected void gvCooperation_RowDataBound(object sender, GridViewRowEventArgs e)
        {

        }
    }
}
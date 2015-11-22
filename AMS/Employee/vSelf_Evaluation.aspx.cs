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
    public partial class vSelf_Evaluation : System.Web.UI.Page
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

                //Get selected evaluation id
                int evaluationId = Convert.ToInt32(Session["EvaluationId"]);

                lblEmpName.Text = emp.GetFullName(UserId);
                lblDesignation.Text = emp.GetDepartment(UserId);
                txtHiredDate.Text = emp.GetHiredDate(UserId);

                //get evaluation details
                dt = new DataTable();
                dt = eval.getEvaluated(evaluationId);
                txtPeriodCovered.Text = dt.Rows[0]["PeriodCovered"].ToString();

                //load grids values
                gvSocialSkills.DataSource = eval.getSelf_SocialSkill_filled(evaluationId);
                gvSocialSkills.DataBind();

                gvCustomerService.DataSource = eval.getSelf_CustomerService_filled(evaluationId);
                gvCustomerService.DataBind();

                gvOriginality.DataSource = eval.getSelf_Originality_filled(evaluationId);
                gvOriginality.DataBind();

                gvResponsibility.DataSource = eval.getSelf_Responsibility_filled(evaluationId);
                gvResponsibility.DataBind();

                gvExcellent.DataSource = eval.getSelf_Excellent_filled(evaluationId);
                gvExcellent.DataBind();
            }
        }

        protected void btnUpdate_Click(object sender, EventArgs e)
        {
            Page.Validate();
            if(Page.IsValid)
            {
                //get selected user
                Guid UserId = Guid.Parse(hfUserId.Value);

                //Get selected evaluation id
                int evaluationId = Convert.ToInt32(Session["EvaluationId"]);

                string agency = emp.GetAgencyName(UserId);
                //update eval
                eval.updateEvaluation_Self(agency, txtPeriodCovered.Text, evaluationId);

                //get grid values
                foreach (GridViewRow row in gvSocialSkills.Rows)
                {
                    if (row.RowType == DataControlRowType.DataRow)
                    {
                        int Id = int.Parse((row.FindControl("lblId") as Label).Text);
                        int rating = Int32.Parse((row.FindControl("txtRating") as TextBox).Text);
                        string remarks = (row.FindControl("txtRemarks") as TextBox).Text;

                        eval.updateSelf_Evaluation_Rating(rating, remarks, Id);
                    }
                }

                foreach (GridViewRow row in gvCustomerService.Rows)
                {
                    if (row.RowType == DataControlRowType.DataRow)
                    {
                        int Id = int.Parse((row.FindControl("lblId") as Label).Text);
                        int rating = Int32.Parse((row.FindControl("txtRating") as TextBox).Text);
                        string remarks = (row.FindControl("txtRemarks") as TextBox).Text;

                        eval.updateSelf_Evaluation_Rating(rating, remarks, Id);
                    }
                }

                foreach (GridViewRow row in gvOriginality.Rows)
                {
                    if (row.RowType == DataControlRowType.DataRow)
                    {
                        int Id = int.Parse((row.FindControl("lblId") as Label).Text);
                        int rating = Int32.Parse((row.FindControl("txtRating") as TextBox).Text);
                        string remarks = (row.FindControl("txtRemarks") as TextBox).Text;

                        eval.updateSelf_Evaluation_Rating(rating, remarks, Id);
                    }
                }

                foreach (GridViewRow row in gvResponsibility.Rows)
                {
                    if (row.RowType == DataControlRowType.DataRow)
                    {
                        int Id = int.Parse((row.FindControl("lblId") as Label).Text);
                        int rating = Int32.Parse((row.FindControl("txtRating") as TextBox).Text);
                        string remarks = (row.FindControl("txtRemarks") as TextBox).Text;

                        eval.updateSelf_Evaluation_Rating(rating, remarks, Id);
                    }
                }

                foreach (GridViewRow row in gvExcellent.Rows)
                {
                    if (row.RowType == DataControlRowType.DataRow)
                    {
                        int Id = int.Parse((row.FindControl("lblId") as Label).Text);
                        int rating = Int32.Parse((row.FindControl("txtRating") as TextBox).Text);
                        string remarks = (row.FindControl("txtRemarks") as TextBox).Text;

                        eval.updateSelf_Evaluation_Rating(rating, remarks, Id);
                    }
                }

                Response.Redirect("~/Employee/vSelf_Evaluation");
            }
        }
    }
}
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

namespace AMS.eval_colleague
{
    public partial class evaluation_colleague_form_view : System.Web.UI.Page
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
                    Response.Redirect("~/eval-colleague/evaluation-colleague.aspx");
                }

                //get selected user
                hfUserId.Value = Session["UserId"].ToString();
                Guid UserId = Guid.Parse(hfUserId.Value);

                //Get selected evaluation id
                int evaluationId = Convert.ToInt32(Session["SelfEvaluationId"]);

                lblEmpName.Text = emp.GetFullName(UserId);
                lblDepartment.Text = emp.GetDepartment(UserId);
                lblDateHired.Text = emp.GetHiredDate(UserId);
                lblPosition.Text = emp.GetPosition(UserId);


                //get evaluation details
                dt = new DataTable();
                dt = eval.Get_Self_Evaluated(evaluationId);

                lblEvalDate.Text = dt.Rows[0]["DateEvaluated"].ToString();
                lblEvaluatedBy.Text = emp.GetFullName(Guid.Parse(dt.Rows[0]["EvaluatedBy"].ToString()));

                //load grids values
                gvSocialSkills.DataSource = eval.getSelf_SocialSkill_filled(evaluationId);
                gvSocialSkills.DataBind();
            }
        }

        protected void btnUpdate_Click(object sender, EventArgs e)
        {
            Page.Validate();
            if (Page.IsValid)
            {
                //get selected user
                Guid UserId = Guid.Parse(hfUserId.Value);

                //Get selected evaluation id
                int evaluationId = Convert.ToInt32(Session["EvaluationId"]);

                string agency = emp.GetAgencyName(UserId);
                //update eval
                //eval.updateEvaluation_Self(agency, evaluationId);

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

                Response.Redirect(Request.Url.AbsoluteUri);
            }
        }
    }
}
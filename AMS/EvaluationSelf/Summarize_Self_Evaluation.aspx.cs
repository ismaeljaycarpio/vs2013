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

namespace AMS.EvaluationSelf
{
    public partial class Summarize_Self_Evaluation : System.Web.UI.Page
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
                    Response.Redirect("~/EvaluationSelf/Score_Sheet");
                }

                //get selected user
                hfUserId.Value = Session["UserId"].ToString();
                Guid UserId = Guid.Parse(hfUserId.Value);

                lblEmpName.Text = emp.GetFullName(UserId);
                lblDepartment.Text = emp.GetDepartment(UserId);
                lblDateHired.Text = emp.GetHiredDate(UserId);
                lblPosition.Text = emp.GetPosition(UserId);
            }
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            lblEvalDate.Text = txtStartDate.Text;

            //load grids values
            gvSocialSkills.DataSource = eval.getSelf_SocialSkill_filled_Summarize(txtStartDate.Text, Guid.Parse(hfUserId.Value));
            gvSocialSkills.DataBind();

            gvCustomerService.DataSource = eval.getSelf_CustomerService_filled_Summarize(txtStartDate.Text, Guid.Parse(hfUserId.Value));
            gvCustomerService.DataBind();

            gvOriginality.DataSource = eval.getSelf_Originality_filled_Summarize(txtStartDate.Text, Guid.Parse(hfUserId.Value));
            gvOriginality.DataBind();

            gvResponsibility.DataSource = eval.getSelf_Responsibility_filled_Summarize(txtStartDate.Text, Guid.Parse(hfUserId.Value));
            gvResponsibility.DataBind();

            gvExcellent.DataSource = eval.getSelf_Excellent_filled_Summarize(txtStartDate.Text, Guid.Parse(hfUserId.Value));
            gvExcellent.DataBind();

        }

        protected void gvSocialSkills_DataBound(object sender, EventArgs e)
        {
        }
    }
}
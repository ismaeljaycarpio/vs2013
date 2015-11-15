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
    public partial class Self_Evaluation : System.Web.UI.Page
    {
        DAL.Evaluation eval = new DAL.Evaluation();
        DAL.Profile profile = new DAL.Profile();
        DAL.Job job = new DAL.Job();
        DataTable dt;

        protected void Page_Load(object sender, EventArgs e)
        {
            if(!Page.IsPostBack)
            {
                if (Session["UserId"] == null)
                    Response.Redirect("~/Employee/Employee");

                //get selected user
                Guid UserId = Guid.Parse(Session["UserId"].ToString());

                lblEmpName.Text = profile.getProfileName(UserId);
                lblDesignation.Text = job.getDepartment(UserId);
                txtHiredDate.Text = job.getHiredDate(UserId);

                gvSocialSkills.DataSource = eval.getSelf_SocialSkill();
                gvSocialSkills.DataBind();

                gvCustomerService.DataSource = eval.getSelf_CustomerService();
                gvCustomerService.DataBind();

                gvOriginality.DataSource = eval.getSelf_Originality();
                gvOriginality.DataBind();

                gvResponsibility.DataSource = eval.getSelf_Responsibility();
                gvResponsibility.DataBind();

                gvExcellent.DataSource = eval.getSelf_Excellent();
                gvExcellent.DataBind();
            }
        }

        protected void btnSumbit_Click(object sender, EventArgs e)
        {
            Page.Validate();
            if(Page.IsValid)
            {
                //get selected user
                Guid UserId = Guid.Parse(Session["UserId"].ToString());
                string agency = job.getAgencyName(UserId);

                int evaluationId = eval.insertEvaluation_Self(
                    UserId, "Self Evaluation", agency, txtPeriodCovered.Text);
            }
        }
    }
}
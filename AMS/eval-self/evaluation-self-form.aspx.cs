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

namespace AMS.eval_self
{
    public partial class evaluation_self_form : System.Web.UI.Page
    {
        DAL.Evaluation eval = new DAL.Evaluation();
        DAL.Employee emp = new DAL.Employee();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                Session["UserId"] = Guid.Parse(Membership.GetUser().ProviderUserKey.ToString());

                //get selected user
                hfUserId.Value = Session["UserId"].ToString();
                Guid UserId = Guid.Parse(hfUserId.Value);
                Guid loggedUserId = Guid.Parse(Membership.GetUser().ProviderUserKey.ToString());

                //chk if logged user already evaluated selected user
                if (eval.IfUserIsAlreadyEvaluated(UserId, loggedUserId, DateTime.Now.ToShortDateString()).Rows.Count > 0)
                {
                    //redirect to edit
                    Session["SelfEvaluationId"] = eval.Get_Self_Evaluation(UserId, loggedUserId, DateTime.Now.ToShortDateString());
                    Response.Redirect("~/eval-self/evaluation-self-form-view.aspx");
                }

                lblEmpName.Text = emp.GetFullName(UserId);
                lblDepartment.Text = emp.GetDepartment(UserId);
                lblDateHired.Text = emp.GetHiredDate(UserId);
                lblPosition.Text = emp.GetPosition(UserId);
                lblEvalDate.Text = DateTime.Now.ToShortDateString();



                gvCustomerService.DataSource = eval.getSelf_CustomerService();
                gvCustomerService.DataBind();
            }
        }

        protected void btnSumbit_Click(object sender, EventArgs e)
        {
            Page.Validate();
            if (Page.IsValid)
            {
                //get selected user
                Guid UserId = Guid.Parse(hfUserId.Value);
                Guid loggedUserId = (Guid)Membership.GetUser().ProviderUserKey;
                string agency = emp.GetAgencyName(UserId);

                int evaluationId = eval.InsertEvaluation_Self(
                    UserId,
                    loggedUserId,
                    DateTime.Now.ToShortDateString());

                foreach (GridViewRow row in gvCustomerService.Rows)
                {
                    if (row.RowType == DataControlRowType.DataRow)
                    {
                        int Id = int.Parse((row.FindControl("lblId") as Label).Text);
                        string rating = (row.FindControl("txtRating") as TextBox).Text;
                        string remarks = (row.FindControl("txtRemarks") as TextBox).Text;

                        eval.addSelf_Evaluation_Rating(evaluationId, Id, rating, remarks);
                    }
                }

                Session["SelfEvaluationId"] = evaluationId;
                Response.Redirect("~/eval-self/evaluation-self-form-view.aspx");
            }
        }
    }
}
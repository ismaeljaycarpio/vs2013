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
    public partial class Evaluation_Self : System.Web.UI.Page
    {
        DAL.Evaluation eval = new DAL.Evaluation();
        DAL.Employee emp = new DAL.Employee();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
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
                lblEvalDate.Text = DateTime.Now.ToShortDateString();

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

                if(!User.IsInRole("Manager"))
                {
                    gvOriginality.Visible = false;
                    gvResponsibility.Visible = false;
                    gvExcellent.Visible = false;
                }

                if(User.IsInRole("Manager") || 
                    User.IsInRole("Staff"))
                {
                    //gvCus
                    var list = new List<int> { 63, 64, 65, 66, 67 };
                    foreach (GridViewRow row in gvCustomerService.Rows)
                    {
                        if (row.RowType == DataControlRowType.DataRow)
                        {
                            int Id = int.Parse((row.FindControl("lblId") as Label).Text);
                            if (!list.Contains(Id))
                            {
                                row.Visible = false;
                            }
                        }
                    }
                }
                else if(User.IsInRole("Supervisor"))
                {
                    //gvCus
                    var list = new List<int> { 63, 64, 65, 66, 67 };
                    foreach (GridViewRow row in gvCustomerService.Rows)
                    {
                        if (row.RowType == DataControlRowType.DataRow)
                        {
                            int Id = int.Parse((row.FindControl("lblId") as Label).Text);
                            if (list.Contains(Id))
                            {
                                row.Visible = false;
                            }
                        }
                    }
                }
            }
        }

        protected void btnSumbit_Click(object sender, EventArgs e)
        {
            Page.Validate();
            if(Page.IsValid)
            {
                //get selected user
                Guid UserId = Guid.Parse(hfUserId.Value);
                string agency = emp.GetAgencyName(UserId);

                int evaluationId = eval.insertEvaluation_Self(
                    UserId, 
                    "Self Evaluation", 
                    agency);

                //get grid values
                foreach(GridViewRow row in gvSocialSkills.Rows)
                {
                    if(row.RowType == DataControlRowType.DataRow)
                    {
                        int Id = int.Parse((row.FindControl("lblId") as Label).Text);
                        string rating = (row.FindControl("txtRating") as TextBox).Text;
                        string remarks = (row.FindControl("txtRemarks") as TextBox).Text;

                        eval.addSelf_Evaluation_Rating(evaluationId, Id, rating, remarks);
                    }
                }

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

                foreach (GridViewRow row in gvOriginality.Rows)
                {
                    if (row.RowType == DataControlRowType.DataRow)
                    {
                        int Id = int.Parse((row.FindControl("lblId") as Label).Text);
                        string rating = (row.FindControl("txtRating") as TextBox).Text;
                        string remarks = (row.FindControl("txtRemarks") as TextBox).Text;

                        eval.addSelf_Evaluation_Rating(evaluationId, Id, rating, remarks);
                    }
                }

                foreach (GridViewRow row in gvResponsibility.Rows)
                {
                    if (row.RowType == DataControlRowType.DataRow)
                    {
                        int Id = int.Parse((row.FindControl("lblId") as Label).Text);
                        string rating = (row.FindControl("txtRating") as TextBox).Text;
                        string remarks = (row.FindControl("txtRemarks") as TextBox).Text;

                        eval.addSelf_Evaluation_Rating(evaluationId, Id, rating, remarks);
                    }
                }

                foreach (GridViewRow row in gvExcellent.Rows)
                {
                    if (row.RowType == DataControlRowType.DataRow)
                    {
                        int Id = int.Parse((row.FindControl("lblId") as Label).Text);
                        string rating = (row.FindControl("txtRating") as TextBox).Text;
                        string remarks = (row.FindControl("txtRemarks") as TextBox).Text;

                        eval.addSelf_Evaluation_Rating(evaluationId, Id, rating, remarks);
                    }
                }
                Session["EvaluationId"] = evaluationId;
                Response.Redirect("~/EvaluationSelf/vEvaluation_Self");
            }        
        }

        protected void gvCustomerService_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if(e.Row.RowType == DataControlRowType.DataRow)
            {
                if(User.IsInRole("Manager") || User.IsInRole("Staff"))
                {
                    //show row

                }
                else
                {
                    //hide
                }
            }
        }
    }
}
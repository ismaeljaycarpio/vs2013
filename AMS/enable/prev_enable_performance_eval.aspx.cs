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

namespace AMS.enable
{
    public partial class prev_enable_performance_eval : System.Web.UI.Page
    {
        DAL.Evaluation eval = new DAL.Evaluation();
        DAL.Employee emp = new DAL.Employee();
        eHRISContextDataContext db = new eHRISContextDataContext();
        DataTable dt;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                if (Session["UserId"] == null)
                {
                    Response.Redirect("~/Employee/Employee.aspx");
                }

                //get selected user
                hfUserId.Value = Session["UserId"].ToString();
                Guid UserId = Guid.Parse(hfUserId.Value);

                //Get selected evaluation id
                int evaluationId = Convert.ToInt32(Session["EvaluationId"]);
                hfEvaluationId.Value = evaluationId.ToString();

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
                    lblApprovedByHRManager.Text = "";
                }
                else
                {
                    lblApprovedByHRManager.Text = emp.GetFullName(Guid.Parse(dt.Rows[0]["ApprovedByHRId"].ToString()));
                }

                txtEnableManagerStength.Text = dt.Rows[0]["EnableManagerStrength"].ToString();
                txtEnableNeedImprovement.Text = dt.Rows[0]["EnableNeedImprovement"].ToString();
                txtEnableRemarks.Text = dt.Rows[0]["EnableRemarks"].ToString();

                lblAckBy.Text = lblEmpName.Text;
                lblEvalDate.Text = dt.Rows[0]["DateEvaluated"].ToString();
                txtNextEvaluationDate.Text = dt.Rows[0]["NextEvaluationDate"].ToString();

                //display filled scores
                gvOrientation.DataSource = eval.getOrientation_filled(evaluationId);
                gvOrientation.DataBind();

                gvBehavior.DataSource = eval.getBehavior_filled(evaluationId);
                gvBehavior.DataBind();

                gvManagement.DataSource = eval.getManagement_filled(evaluationId);
                gvManagement.DataBind();


                //check ids
                Guid myUserId = Guid.Parse(Membership.GetUser().ProviderUserKey.ToString());

                //chk if user is updating itself
                if (myUserId.Equals(UserId))
                {
                    //hide evaluator rating
                    gvOrientation.Columns[3].Visible = false;
                    gvBehavior.Columns[3].Visible = false;
                    gvManagement.Columns[3].Visible = false;

                    gvOrientation.Columns[5].Visible = false;
                    gvBehavior.Columns[5].Visible = false;
                    gvManagement.Columns[5].Visible = false;

                    txtNextEvaluationDate.Enabled = false;
                    RequiredFieldValidator3.Enabled = false;
                    pnlStaffOnly.Visible = true;
                }
                else
                {
                    //hide staff rating
                    //gvOrientation.Columns[2].Visible = false;
                    //gvBehavior.Columns[2].Visible = false;
                    //gvManagement.Columns[2].Visible = false;
                    pnlEvaluatorsOnly.Visible = true;
                    pnlStaffOnly.Visible = true;
                    pnlStaffOnly.Enabled = false;
                }
            }
        }

        protected void btnUpdate_Click(object sender, EventArgs e)
        {
            Page.Validate();

            Guid myUserId = Guid.Parse(Membership.GetUser().ProviderUserKey.ToString());
            Guid userId = Guid.Parse(hfUserId.Value);

            var ev = (from eval in db.EvaluationLINQs
                      where eval.Id == Convert.ToInt32(hfEvaluationId.Value)
                      select eval).FirstOrDefault();
            
            int evId = ev.Id;

            if(!myUserId.Equals(userId))
            {
                //rater
                ev.NextEvaluationDate = txtNextEvaluationDate.Text;
                ev.EnableRemarks = txtEnableRemarks.Text;

                db.SubmitChanges();

                //insert scores
                foreach (GridViewRow row in gvOrientation.Rows)
                {
                    if (row.RowType == DataControlRowType.DataRow)
                    {
                        int Id = int.Parse((row.FindControl("lblId") as Label).Text);
                        decimal rating = decimal.Parse((row.FindControl("txtEvaluatorRating") as TextBox).Text);
                        string strRemarks = (row.FindControl("txtRemarks") as TextBox).Text;

                        var sc = (from s in db.Evaluation_Scores
                                  where s.Id == Id
                                  select s).FirstOrDefault();

                        sc.EvaluatorRating = rating;
                        sc.Remarks = strRemarks;
                        db.SubmitChanges();
                    }
                }

                foreach (GridViewRow row in gvBehavior.Rows)
                {
                    if (row.RowType == DataControlRowType.DataRow)
                    {
                        int Id = int.Parse((row.FindControl("lblId") as Label).Text);
                        decimal rating = decimal.Parse((row.FindControl("txtEvaluatorRating") as TextBox).Text);
                        string strRemarks = (row.FindControl("txtRemarks") as TextBox).Text;

                        var sc = (from s in db.Evaluation_Scores
                                  where s.Id == Id
                                  select s).FirstOrDefault();

                        sc.EvaluatorRating = rating;
                        sc.Remarks = strRemarks;
                        db.SubmitChanges();
                    }
                }

                foreach (GridViewRow row in gvManagement.Rows)
                {
                    if (row.RowType == DataControlRowType.DataRow)
                    {
                        int Id = int.Parse((row.FindControl("lblId") as Label).Text);
                        decimal rating = decimal.Parse((row.FindControl("txtEvaluatorRating") as TextBox).Text);
                        string strRemarks = (row.FindControl("txtRemarks") as TextBox).Text;

                        var sc = (from s in db.Evaluation_Scores
                                  where s.Id == Id
                                  select s).FirstOrDefault();

                        sc.EvaluatorRating = rating;
                        sc.Remarks = strRemarks;
                        db.SubmitChanges();
                    }
                }
            }
            else
            {
                //ratee
                ev.EnableManagerStrength = txtEnableManagerStength.Text;
                ev.EnableNeedImprovement = txtEnableNeedImprovement.Text;

                db.SubmitChanges();

                //insert scores
                foreach (GridViewRow row in gvOrientation.Rows)
                {
                    if (row.RowType == DataControlRowType.DataRow)
                    {
                        int Id = int.Parse((row.FindControl("lblId") as Label).Text);
                        decimal rating = decimal.Parse((row.FindControl("txtStaffRating") as TextBox).Text);
                        //string strRemarks = (row.FindControl("txtRemarks") as TextBox).Text;

                        var sc = (from s in db.Evaluation_Scores
                                  where s.Id == Id
                                  select s).FirstOrDefault();

                        sc.StaffRating = rating;
                        db.SubmitChanges();
                    }
                }

                foreach (GridViewRow row in gvBehavior.Rows)
                {
                    if (row.RowType == DataControlRowType.DataRow)
                    {
                        int Id = int.Parse((row.FindControl("lblId") as Label).Text);
                        decimal rating = decimal.Parse((row.FindControl("txtStaffRating") as TextBox).Text);
                        //string strRemarks = (row.FindControl("txtRemarks") as TextBox).Text;

                        var sc = (from s in db.Evaluation_Scores
                                  where s.Id == Id
                                  select s).FirstOrDefault();

                        sc.StaffRating = rating;
                        db.SubmitChanges();
                    }
                }

                foreach (GridViewRow row in gvManagement.Rows)
                {
                    if (row.RowType == DataControlRowType.DataRow)
                    {
                        int Id = int.Parse((row.FindControl("lblId") as Label).Text);
                        decimal rating = decimal.Parse((row.FindControl("txtStaffRating") as TextBox).Text);
                        //string strRemarks = (row.FindControl("txtRemarks") as TextBox).Text;

                        var sc = (from s in db.Evaluation_Scores
                                  where s.Id == Id
                                  select s).FirstOrDefault();

                        sc.StaffRating = rating;
                        db.SubmitChanges();
                    }
                }
            }
            Response.Redirect("~/Employee/Evaluation.aspx");
        }

        protected void gvOrientation_RowDataBound(object sender, GridViewRowEventArgs e)
        {

        }

        protected void gvBehavior_RowDataBound(object sender, GridViewRowEventArgs e)
        {

        }

        protected void gvManagement_RowDataBound(object sender, GridViewRowEventArgs e)
        {

        }
    }
}
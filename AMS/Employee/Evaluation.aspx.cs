using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Security;
using System.Data;
using System.IO;

namespace AMS.Employee
{
    public partial class Evaluation : System.Web.UI.Page
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
                    Response.Redirect("~/Employee/Employee.aspx");
                }

                hfUserId.Value = Session["UserId"].ToString();
                Guid UserId = Guid.Parse(hfUserId.Value);

                BindGridView();
                //BindSelfEvaluation();

                hfAgency.Value = emp.GetAgencyName(UserId);
                lblLastEvaluationDate.Text = emp.GetLastEvaluationDate(UserId);
                lblNextEvaluationDate.Text = emp.GetNextEvaluationDate(UserId);

                //check ids
                Guid loggedUserId = Guid.Parse(Membership.GetUser().ProviderUserKey.ToString());

                //disable controls
                btnPerfEval.Enabled = false;
                btnPerfEval.Visible = false;

                //evaluator
                if (!loggedUserId.Equals(UserId))
                {
                    //GM-> evaluate Managers and HR/Manager                   
                    if (User.IsInRole("General Manager"))
                    {
                        //show managers/hr only
                        if (emp.GetRoleName(UserId).Equals("Manager") ||
                            emp.GetRoleName(UserId).Equals("HR"))
                        {
                            btnPerfEval.Enabled = true;
                            btnPerfEval.Visible = true;
                        }
                    }
                    //HR-> evaluate Managers only
                    else if (User.IsInRole("HR"))
                    {
                        //chk if HR Assistant
                        if(emp.GetPosition(loggedUserId) == "HR Assistant")
                        {
                            btnPerfEval.Enabled = false;
                            btnPerfEval.Visible = false;
                        }
                        else
                        {
                            //show if managers/supervisor/staff only
                            if (emp.GetRoleName(UserId).Equals("Manager") ||
                                emp.GetRoleName(UserId).Equals("Supervisor") ||
                                emp.GetRoleName(UserId).Equals("Staff"))
                            {
                                btnPerfEval.Enabled = true;
                                btnPerfEval.Visible = true;
                            }
                        }                     
                    }
                    else
                    {
                        btnPerfEval.Visible = true;
                        btnPerfEval.Enabled = true;
                    }
                }
                    //self eval
                else
                {
                    btnPerfEval.Visible = false;
                    btnPerfEval.Enabled = false;

                    //dont show self eval list
                    //gvSelfEvaluation.Visible = false;
                    //pnlInfo.Visible = true;
                }
            }
        }

        protected void BindGridView()
        {
            Guid UserId = Guid.Parse(hfUserId.Value);
            gvEvaluation.DataSource = eval.DisplayMyEvaluation(UserId);
            gvEvaluation.DataBind();
        }

        protected void BindSelfEvaluation()
        {
            Guid UserId = Guid.Parse(hfUserId.Value);
            //gvSelfEvaluation.DataSource = eval.DisplayMySelf_Evaluation(UserId);
            //gvSelfEvaluation.DataBind();
        }

        protected void gvEvaluation_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvEvaluation.PageIndex = e.NewPageIndex;
            BindGridView();
        }

        protected void btnPerfEval_Click(object sender, EventArgs e)
        {
            if (hfAgency.Value.Equals("TOPLIS Solutions Inc."))
            {
                Response.Redirect("~/Employee/PerformanceEvaluation.aspx");
            }
            else if (hfAgency.Value.Equals("PrimePower"))
            {
                Response.Redirect("~/Employee/Prime_Performance_Evaluation.aspx");
            }
            else
            {
                //no agency specified
                Response.Redirect("~/Employee/ErrorPage.aspx");
            }
        }

        protected void btnSelfEval_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Employee/Self_Evaluation.aspx");
        }

        protected void gvEvaluation_SelectedIndexChanged(object sender, EventArgs e)
        {
            Session["EvaluationId"] = gvEvaluation.SelectedValue.ToString();
            Response.Redirect("~/Employee/vPerformanceEvaluation.aspx");
        }

        protected void gvSelfEvaluation_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            //gvSelfEvaluation.PageIndex = e.NewPageIndex;
            //BindSelfEvaluation();
        }

        protected void gvSelfEvaluation_SelectedIndexChanged(object sender, EventArgs e)
        {
            //Session["SelfEvaluationId"] = gvSelfEvaluation.SelectedValue.ToString();
            //Response.Redirect("~/EvaluationSelf/vEvaluation_Self.aspx");
        }

        protected void gvSelfEvaluation_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            //if (e.Row.RowType == DataControlRowType.Footer)
            //{
            //    int _TotalRecs = ((DataTable)(gvSelfEvaluation.DataSource)).Rows.Count;
            //    int _CurrentRecStart = gvSelfEvaluation.PageIndex * gvSelfEvaluation.PageSize + 1;
            //    int _CurrentRecEnd = gvSelfEvaluation.PageIndex * gvSelfEvaluation.PageSize + gvSelfEvaluation.Rows.Count;

            //    e.Row.Cells[0].ColumnSpan = 2;
            //    e.Row.Cells[0].Text = string.Format("Displaying <b style=color:red>{0}</b> to <b style=color:red>{1}</b> of {2} records found", _CurrentRecStart, _CurrentRecEnd, _TotalRecs);
            //}
        }

        protected void lnkSummarize_Click(object sender, EventArgs e)
        {
            Session["UserId"] = hfUserId.Value;
            Response.Redirect("~/EvaluationSelf/Summarize_Self_Evaluation.aspx");
        }

        protected void btnDelete_Click(object sender, EventArgs e)
        {
            eval.deleteEvaluation(hfDeleteId.Value);

            BindGridView();

            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            sb.Append(@"<script type='text/javascript'>");
            sb.Append("$('#deleteModal').modal('hide');");
            sb.Append(@"</script>");
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "DeleteHideModalScript", sb.ToString(), false);
        }

        protected void gvEvaluation_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName.Equals("deleteRecord"))
            {
                dt = new DataTable();
                int index = Convert.ToInt32(e.CommandArgument);

                string rowId = ((Label)gvEvaluation.Rows[index].FindControl("lblRowId")).Text;
                hfDeleteId.Value = rowId;

                System.Text.StringBuilder sb = new System.Text.StringBuilder();
                sb.Append(@"<script type='text/javascript'>");
                sb.Append("$('#deleteModal').modal('show');");
                sb.Append(@"</script>");
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "DeleteShowModalScript", sb.ToString(), false);
            }
            else if(e.CommandName.Equals("selectRecord"))
            {
                int index = Convert.ToInt32(e.CommandArgument);
                Session["EvaluationId"] = ((Label)gvEvaluation.Rows[index].FindControl("lblRowId")).Text;
                Response.Redirect("~/Employee/vPerformanceEvaluation.aspx");
            }
        }
    }
}
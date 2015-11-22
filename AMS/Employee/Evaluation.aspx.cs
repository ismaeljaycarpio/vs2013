﻿using System;
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
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                if (Session["UserId"] == null)
                {
                    Response.Redirect("~/Employee/Employee");
                }

                hfUserId.Value = Session["UserId"].ToString();
                Guid UserId = Guid.Parse(hfUserId.Value);
                BindGridView(UserId);

                hfAgency.Value = emp.GetAgencyName(UserId);


                //check ids
                MembershipUser loggedInUser = Membership.GetUser();
                Guid loggedUserId = Guid.Parse(loggedInUser.ProviderUserKey.ToString());

                //disable controls
                btnPerfEval.Enabled = false;
                btnPerfEval.Visible = false;

                //chk if user is evaluating itself
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
                        //show if managers only
                        if (emp.GetRoleName(UserId).Equals("Manager"))
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
        }

        protected void BindGridView(Guid UserId)
        {
            gvEvaluation.DataSource = eval.displayMyEvaluation(UserId);
            gvEvaluation.DataBind();
        }

        protected void gvEvaluation_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {

        }

        protected void gvEvaluation_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
        {
            //Session["EvaluationId"] = gvEvaluation.SelectedValue.ToString();
            //Response.Redirect("~/Employee/vPerformanceEvaluation");
        }

        protected void btnPerfEval_Click(object sender, EventArgs e)
        {
            if (hfAgency.Value.Equals("TOPLIS Solutions Inc."))
            {
                Response.Redirect("~/Employee/PerformanceEvaluation");
            }
            else if (hfAgency.Value.Equals("PrimePower"))
            {
                Response.Redirect("~/Employee/Prime_Performance_Evaluation");
            }
            else
            {
                Response.Redirect("~/Employee/No_Agency");
            }
        }

        protected void btnSelfEval_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Employee/Self_Evaluation");
        }

        protected void gvEvaluation_SelectedIndexChanged(object sender, EventArgs e)
        {
            Session["EvaluationId"] = gvEvaluation.SelectedValue.ToString();
            Response.Redirect("~/Employee/vPerformanceEvaluation");
        }
    }
}
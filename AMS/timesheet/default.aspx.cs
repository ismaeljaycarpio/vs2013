using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace AMS.timesheet
{
    public partial class _default : System.Web.UI.Page
    {
        DAL.Attendance attendance = new DAL.Attendance();
        DAL.Schedule sched = new DAL.Schedule();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                clearButtons();

                //dont remember username, security reasons
                txtEmpId.Attributes.Add("autocomplete", "off");
            }
        }

        protected void btnConfirmTimeout_Click(object sender, EventArgs e)
        {
            Guid userId = Guid.Parse(Membership.GetUser(txtEmpId.Text).ProviderUserKey.ToString());

            if (ddlTimeOutSched.Items.Count == 0)
            {
                //create sched
                //15hrs
                sched.insertSchedByStaff_timeOut(userId, txtRemarks.Text);
            }
            else
            {
                //update timeout
                sched.updateTimeOut(ddlTimeOutSched.SelectedValue, txtRemarks.Text);
            }

            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            sb.Append(@"<script type='text/javascript'>");
            sb.Append("$('#timeoutModal').modal('hide');");
            sb.Append(@"</script>");
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "DeleteHideModalScript", sb.ToString(), false);
        }

        protected void btnConfirmTimeIn_Click(object sender, EventArgs e)
        {
            Guid userId = Guid.Parse(Membership.GetUser(txtEmpId.Text).ProviderUserKey.ToString());

            //chk ddl if no sched created
            if (ddlSchedule.Items.Count == 0)
            {
                //create new sched
                //15hrs
                sched.insertSchedByStaff_timeIn(userId, txtRemarks.Text);
            }
            else
            {
                //update sched
                sched.updateTimeIn(ddlSchedule.SelectedValue, txtRemarks.Text);
            }

            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            sb.Append(@"<script type='text/javascript'>");
            sb.Append("$('#timeinModal').modal('hide');");
            sb.Append(@"</script>");
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "HideConfirmTimeInModalScript", sb.ToString(), false);
        }

        protected void btnTimeIn_Click(object sender, EventArgs e)
        {
            lblError.Text = String.Empty;

            Page.Validate();
            if (Page.IsValid)
            {
                if (!Membership.ValidateUser(txtEmpId.Text, txtPassword.Text))
                {
                    lblError.Text = "Invalid Username/Password combination. Try again!";
                }
                else
                {
                    Guid userId = Guid.Parse(Membership.GetUser(txtEmpId.Text).ProviderUserKey.ToString());
                    if (userId != null)
                    {
                        ddlSchedule.DataSource = sched.getScheduleTodayAttendance(userId);
                        ddlSchedule.DataTextField = "TimeStart";
                        ddlSchedule.DataValueField = "Id";
                        ddlSchedule.DataBind();

                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myTimeInModal", "$('#timeinModal').modal();", true);
                        upConfirmTimeIn.Update();
                    }
                    else
                    {
                        lblError.Text = "ERROR: Problem getting information for the user " + txtEmpId.Text.Trim();
                    }
                }
            }
        }

        protected void btnTimeOut_Click(object sender, EventArgs e)
        {
            lblError.Text = String.Empty;

            Page.Validate();
            if (Page.IsValid)
            {
                if (!Membership.ValidateUser(txtEmpId.Text, txtPassword.Text))
                {
                    lblError.Text = "Invalid Username/Password combination. Try again!";
                }
                else
                {
                    Guid userId = Guid.Parse(Membership.GetUser(txtEmpId.Text).ProviderUserKey.ToString());
                    if (userId != null)
                    {
                        ddlTimeOutSched.DataSource = sched.getScheduleTodayAttendance(userId);
                        ddlTimeOutSched.DataTextField = "TimeEnd";
                        ddlTimeOutSched.DataValueField = "Id";
                        ddlTimeOutSched.DataBind();
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#timeoutModal').modal();", true);
                        upConfirmTimeOut.Update();
                    }
                    else
                    {
                        lblError.Text = "ERROR: Problem getting information for the user " + txtEmpId.Text.Trim();
                    }
                }
            }
        }

        protected void clearButtons()
        {
            txtEmpId.Text = String.Empty;
            txtPassword.Text = String.Empty;
            txtRemarks.Text = String.Empty;
            lblError.Text = String.Empty;
        }
    }
}
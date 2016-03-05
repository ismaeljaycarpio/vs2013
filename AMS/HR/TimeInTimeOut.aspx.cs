using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace AMS.HR
{
    public partial class TimeInTimeOut : System.Web.UI.Page
    {
        DAL.Attendance attendance = new DAL.Attendance();
        DataTable dt;

        protected void Page_Load(object sender, EventArgs e)
        {
            if(!Page.IsPostBack)
            {
                clearButtons();
            }
        }

        protected void btnTimeIn_Click(object sender, EventArgs e)
        {
            Page.Validate();
            if(Page.IsValid)
            {
                if(!Membership.ValidateUser(txtEmpId.Text, txtPassword.Text))
                {                                                                                                                                                           
                    lblError.Text = "Invalid Username/Password combination. Try again!";
                }
                else
                {
                    Guid userId = Guid.Parse(Membership.GetUser(txtEmpId.Text).ProviderUserKey.ToString());
                    if(userId != null)
                    {
                        attendance.TimeIn(userId, DateTime.Now, txtRemarks.Text);
                        lblModalTitle.Text = "Successfull Time-In";
                        lblModalBody.Text = "User " + txtEmpId.Text + " successfully time-in at : " + DateTime.Now;
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#myModal').modal();", true);
                        upModal.Update();
                    }
                    else
                    {
                        lblError.Text = "ERROR: Problem getting information for the user " + txtEmpId.Text.Trim();
                    }
                }
            }
        }

        protected void TimeOut_Click(object sender, EventArgs e)
        {
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
                        ddlLastTimeIn.DataSource = attendance.getlastTimeIn(userId);
                        ddlLastTimeIn.DataTextField = "TimeIn";
                        ddlLastTimeIn.DataValueField = "Id";
                        ddlLastTimeIn.DataBind();

                        if(ddlLastTimeIn.Items.Count > 0)
                        {
                            ddlLastTimeIn.Items.Insert(1, new ListItem("No Time-In", "0"));

                            string strLastTimein = ddlLastTimeIn.SelectedItem.Text;
                            DateTime dtTimein = Convert.ToDateTime(strLastTimein);
                            TimeSpan diff = DateTime.Now.Subtract(dtTimein);

                            lblLastTimeIn.Text = String.Format("{0} day/s, {1} hours, {2} minutes ago", diff.Days, diff.Hours, diff.Minutes);
                        }
                        else if(ddlLastTimeIn.Items.Count == 0)
                        {
                            ddlLastTimeIn.Items.Insert(0, new ListItem("", "0"));
                            ddlLastTimeIn.Items.Insert(1, new ListItem("No Login", "-1"));

                            lblLastTimeIn.Text = "no records found";
                            lblLastTimeIn.ForeColor = System.Drawing.Color.DarkOrange;
                        }
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

        protected void btnConfirmTimeout_Click(object sender, EventArgs e)
        {
            Guid userId = Guid.Parse(Membership.GetUser(txtEmpId.Text).ProviderUserKey.ToString());

            //chk
            if(ddlLastTimeIn.SelectedValue == "0" || ddlLastTimeIn.SelectedValue == "-1")
            {
                attendance.TimeOut(userId, DateTime.Now, txtReasons.Text);
            }
            else
            {
                attendance.updateTimeOut(ddlLastTimeIn.SelectedValue, DateTime.Now);
            }

            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            sb.Append(@"<script type='text/javascript'>");
            sb.Append("$('#timeoutModal').modal('hide');");
            sb.Append(@"</script>");
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "DeleteHideModalScript", sb.ToString(), false);
        }
    }
}
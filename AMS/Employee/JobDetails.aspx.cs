using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Web.Security;
using System.IO;

namespace AMS.Employee
{
    public partial class JobDetails : System.Web.UI.Page
    {
        DAL.Employee emp = new DAL.Employee();
        DAL.PositionManagement pos = new DAL.PositionManagement();
        DAL.Agency agency = new DAL.Agency();
        DAL.Filler fill = new DAL.Filler();
        DataTable dt;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                if(Session["UserId"] == null)
                {
                    Response.Redirect("~/Employee/Employee");
                }

                hfUserId.Value = Session["UserId"].ToString();
                //load ddls
                fillPosition();
                fillDept();
                fillAgency();
                fillEmpStatus();

                dt = new DataTable();
                dt = emp.GetEmployee(Guid.Parse(hfUserId.Value));

                //load job details
                txtEmpId.Text = dt.Rows[0]["Emp_ID"].ToString();
                ddlPosition.SelectedValue = dt.Rows[0]["PositionId"].ToString();
                ddlDepartment.SelectedValue = emp.GetDepartmentId(Guid.Parse(hfUserId.Value));
                txtSubUnit.Text = dt.Rows[0]["SubUnit"].ToString();

                ddlAgency.SelectedValue = dt.Rows[0]["AgencyId"].ToString();
                ddlEmpStatus.SelectedValue = dt.Rows[0]["Emp_Status"].ToString();
                txtJoinDate.Text = dt.Rows[0]["JoinDate"].ToString();
                txtContractStartingDate.Text = dt.Rows[0]["Contract_SD"].ToString();
                txtContractEndingDate.Text = dt.Rows[0]["Contract_ED"].ToString();

                lblRole.Text = emp.GetRoleNameBypPosition(ddlPosition.SelectedValue.ToString());

                //get list of supervisor and manager
                lblManager.Text = emp.GetManagerName(ddlDepartment.SelectedValue.ToString());
                lblSupervisor.Text = emp.GetSupervisorName(ddlDepartment.SelectedValue.ToString());

                if(!User.IsInRole("Admin") && !User.IsInRole("HR"))
                {
                    disableControls();
                    hideControls();
                }
            }
        }

        private void hideControls()
        {
            btnUpdateJob.Visible = false;
        }

        private void disableControls()
        {
            txtEmpId.Enabled = false;
            ddlPosition.Enabled = false;
            txtSubUnit.Enabled = false;
            ddlAgency.Enabled = false;
            ddlEmpStatus.Enabled = false;
            txtJoinDate.Enabled = false;
            txtContractStartingDate.Enabled = false;
            txtContractEndingDate.Enabled = false;
        }

        protected void btnUpdateJob_Click(object sender, EventArgs e)
        {
                emp.UpdateJobDetails(
                    txtEmpId.Text,
                    ddlPosition.SelectedValue.ToString(),
                    ddlEmpStatus.SelectedValue.ToString(),
                    txtSubUnit.Text,
                    txtJoinDate.Text,
                    txtContractStartingDate.Text,
                    txtContractEndingDate.Text,
                    ddlAgency.SelectedValue,
                    Guid.Parse(hfUserId.Value));

            //get user
            MembershipUser _user = Membership.GetUser(Guid.Parse(hfUserId.Value));

            //remove user from role membership
            foreach (string role in Roles.GetRolesForUser(_user.UserName))
            {
                Roles.RemoveUserFromRole(_user.UserName, role);
            }

            //asign user to new role
            if(!Roles.IsUserInRole(_user.UserName, lblRole.Text))
            {
                Roles.AddUserToRole(_user.UserName, lblRole.Text);
            }
        }

        public void fillPosition()
        {
            if(User.IsInRole("Admin"))
            {
                //show admin value
                ddlPosition.DataSource = fill.fillPosition(true);
                ddlPosition.DataTextField = "Position";
                ddlPosition.DataValueField = "Id";
                ddlPosition.DataBind();
            }
            else
            {
                ddlPosition.DataSource = fill.fillPosition(false);
                ddlPosition.DataTextField = "Position";
                ddlPosition.DataValueField = "Id";
                ddlPosition.DataBind();
            }
        }

        public void fillDept()
        {
            ddlDepartment.DataSource = fill.fillDepartment();
            ddlDepartment.DataTextField = "Department";
            ddlDepartment.DataValueField = "Id";
            ddlDepartment.DataBind();
        }

        public void fillAgency()
        {       
            ddlAgency.DataSource = agency.DisplayAgency();
            ddlAgency.DataTextField = "Agency";
            ddlAgency.DataValueField = "Id";
            ddlAgency.DataBind();
        }

        public void fillEmpStatus()
        {
            ddlEmpStatus.DataSource = fill.fillEmpStatus();
            ddlEmpStatus.DataTextField = "Status";
            ddlEmpStatus.DataValueField = "Id";
            ddlEmpStatus.DataBind();
        }

        protected void ddlPosition_SelectedIndexChanged(object sender, EventArgs e)
        {
            lblRole.Text = emp.GetRoleNameBypPosition(ddlPosition.SelectedValue.ToString());
            ddlDepartment.SelectedValue = pos.GetDepartmentIdBypPosition(ddlPosition.SelectedValue.ToString());
            lblManager.Text = emp.GetManagerName(ddlDepartment.SelectedValue.ToString());
            lblSupervisor.Text = emp.GetSupervisorName(ddlDepartment.SelectedValue.ToString());
        }
    }
}
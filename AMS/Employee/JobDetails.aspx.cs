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

                DAL.Job job = new DAL.Job();
                DataTable dt = new DataTable();
                dt = job.getJobDetailsById(Guid.Parse(hfUserId.Value));

                //load job details
                txtEmpId.Text = dt.Rows[0]["Emp_ID"].ToString();
                ddlPosition.SelectedValue = dt.Rows[0]["PositionId"].ToString();
                ddlDepartment.SelectedValue = job.getDepartmentId(Guid.Parse(hfUserId.Value));
                txtSubUnit.Text = dt.Rows[0]["SubUnit"].ToString();

                ddlAgency.SelectedValue = dt.Rows[0]["AgencyId"].ToString();
                txtEmpStatus.Text = dt.Rows[0]["Emp_Status"].ToString();
                txtJoinDate.Text = dt.Rows[0]["JoinDate"].ToString();
                txtContractStartingDate.Text = dt.Rows[0]["Contract_SD"].ToString();
                txtContractEndingDate.Text = dt.Rows[0]["Contract_ED"].ToString();
                txtEMovement.Text = dt.Rows[0]["EMovement"].ToString();

                lblRole.Text = job.getRoleNameBypPosition(ddlPosition.SelectedValue.ToString());

                //txtManager.Text = dt.Rows[0]["ManagerId"].ToString();
                //txtSupervisor.Text = dt.Rows[0]["SupervisorId"].ToString();
                //txtManager.Text = job.getManagerName(ddlDepartment.SelectedValue.ToString());

                lblManager.Text = job.getManagerName(ddlDepartment.SelectedValue.ToString());
                lblSupervisor.Text = job.getSupervisorName(ddlDepartment.SelectedValue.ToString());

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
            txtEmpStatus.Enabled = false;
            txtJoinDate.Enabled = false;
            txtContractStartingDate.Enabled = false;
            txtContractEndingDate.Enabled = false;
            txtEMovement.Enabled = false;
        }

        protected void btnUpdateJob_Click(object sender, EventArgs e)
        {
                DAL.Job job = new DAL.Job();
                job.updateJobDetails(
                    txtEmpId.Text,
                    ddlPosition.SelectedValue.ToString(),
                    txtEmpStatus.Text,
                    txtSubUnit.Text,
                    txtJoinDate.Text,
                    txtContractStartingDate.Text,
                    txtContractEndingDate.Text,
                    ddlAgency.SelectedValue,
                    txtEMovement.Text,
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
            DAL.Filler fill = new DAL.Filler();
            ddlPosition.DataSource = fill.fillPosition();
            ddlPosition.DataTextField = "Position";
            ddlPosition.DataValueField = "Id";
            ddlPosition.DataBind();
            
        }

        public void fillDept()
        {
            DAL.Filler fill = new DAL.Filler();
            ddlDepartment.DataSource = fill.fillDepartment();
            ddlDepartment.DataTextField = "Department";
            ddlDepartment.DataValueField = "Id";
            ddlDepartment.DataBind();
        }

        public void fillAgency()
        {
            DAL.Agency agency = new DAL.Agency();
            ddlAgency.DataSource = agency.displayAgency();
            ddlAgency.DataTextField = "Agency";
            ddlAgency.DataValueField = "Id";
            ddlAgency.DataBind();
        }

        protected void ddlPosition_SelectedIndexChanged(object sender, EventArgs e)
        {
            DAL.Job job = new DAL.Job();
            DAL.PositionManagement pos = new DAL.PositionManagement();
            lblRole.Text = job.getRoleNameBypPosition(ddlPosition.SelectedValue.ToString());
            ddlDepartment.SelectedValue = pos.getDepartmentIdBypPosition(ddlPosition.SelectedValue.ToString());
            lblManager.Text = job.getManagerName(ddlDepartment.SelectedValue.ToString());
            lblSupervisor.Text = job.getSupervisorName(ddlDepartment.SelectedValue.ToString());
        }

    }
}
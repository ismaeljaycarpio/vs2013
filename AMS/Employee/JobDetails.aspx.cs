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
                fillAccountStatus();

                dt = new DataTable();
                dt = emp.GetEmployee(Guid.Parse(hfUserId.Value));

                //load job details
                txtEmpId.Text = dt.Rows[0]["Emp_ID"].ToString();
                ddlPosition.SelectedValue = dt.Rows[0]["PositionId"].ToString();
                ddlDepartment.SelectedValue = emp.GetDepartmentId(Guid.Parse(hfUserId.Value));
                ddlAccountStatus.SelectedValue = dt.Rows[0]["AccountStatusId"].ToString();
                txtSubUnit.Text = dt.Rows[0]["SubUnit"].ToString();

                ddlAgency.SelectedValue = dt.Rows[0]["AgencyId"].ToString();
                ddlEmpStatus.SelectedValue = dt.Rows[0]["Emp_Status"].ToString();
                txtJoinDate.Text = dt.Rows[0]["JoinDate"].ToString();
                txtContractStartingDate.Text = dt.Rows[0]["Contract_SD"].ToString();
                txtContractEndingDate.Text = dt.Rows[0]["Contract_ED"].ToString();

                //lblRole.Text = emp.GetRoleNameBypPosition(ddlPosition.SelectedValue.ToString());

                //get list of supervisor and manager
                lblManager.Text = emp.GetManagerName(ddlDepartment.SelectedValue.ToString());
                lblSupervisor.Text = emp.GetSupervisorName(ddlDepartment.SelectedValue.ToString());

                //chk user account
                if (dt.Rows[0]["Contract_ED"].ToString() != String.Empty)
                {
                    DateTime contract_end_date = Convert.ToDateTime(dt.Rows[0]["Contract_ED"].ToString());

                    if (contract_end_date < DateTime.Now)
                    {
                        pnlAccountStatus.Visible = true;
                        ddlAccountStatus.ClearSelection();
                        ddlAccountStatus.Items.FindByText("Expired").Selected = true;
                    }                    
                }

                if (!User.IsInRole("Admin") && !User.IsInRole("HR"))
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
            ddlAccountStatus.Enabled = false;
        }

        protected void btnUpdateJob_Click(object sender, EventArgs e)
        {
                emp.UpdateJobDetails(
                    txtEmpId.Text,
                    ddlPosition.SelectedValue.ToString(),
                    ddlEmpStatus.SelectedValue.ToString(),
                    txtSubUnit.Text,
                    Request.Form[txtJoinDate.UniqueID],
                    Request.Form[txtContractStartingDate.UniqueID],
                    Request.Form[txtContractEndingDate.UniqueID],
                    ddlAgency.SelectedValue,
                    ddlAccountStatus.SelectedValue,
                    Guid.Parse(hfUserId.Value));

            Response.Redirect(Request.Url.AbsoluteUri);
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

        public void fillAccountStatus()
        {
            ddlAccountStatus.DataSource = fill.fillAccountStatus();
            ddlAccountStatus.DataTextField = "AccountStatus";
            ddlAccountStatus.DataValueField = "Id";
            ddlAccountStatus.DataBind();
        }

        protected void ddlPosition_SelectedIndexChanged(object sender, EventArgs e)
        {
            //lblRole.Text = emp.GetRoleNameBypPosition(ddlPosition.SelectedValue.ToString());
            ddlDepartment.SelectedValue = pos.GetDepartmentIdBypPosition(ddlPosition.SelectedValue.ToString());
            lblManager.Text = emp.GetManagerName(ddlDepartment.SelectedValue.ToString());
            lblSupervisor.Text = emp.GetSupervisorName(ddlDepartment.SelectedValue.ToString());
        }
    }
}
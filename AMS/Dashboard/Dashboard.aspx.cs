using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Security;

namespace AMS.Dashboard
{
    public partial class Dashboard : System.Web.UI.Page
    {
        DAL.Dashboard dashb = new DAL.Dashboard();
        DAL.Employee emp = new DAL.Employee();

        protected void Page_Load(object sender, EventArgs e)
        {
            if(!Page.IsPostBack)
            {
                //get deptId
                Guid UserId = Guid.Parse(Membership.GetUser().ProviderUserKey.ToString());
                string deptId = emp.GetDepartmentId(UserId);

                if(!User.IsInRole("Admin") && 
                    !User.IsInRole("HR") && 
                    !User.IsInRole("General Manager"))
                {
                    string deptName = emp.GetDepartment(UserId);
                    lblDepartment.Text = deptName;
                    pnlNotification.Visible = true;

                    //display dept based emp
                    lnkBdayCount.Text = dashb.CountBday(deptId).ToString();

                    gvEmployeeMasterList.DataSource = dashb.DisplayMasterList(deptId);
                    gvEmployeeMasterList.DataBind();

                    lnkCountExpiringContracts.Text = dashb.CountExpiringContracts(deptId).ToString();
                    //lblCountNewlyHired.Text = dashb.CountNewlyHired(deptId).ToString();
                    lblCountPendingEvaluation.Text = dashb.CountPendingEvaluation(deptId).ToString();
                }
                else
                {
                    lnkBdayCount.Text = dashb.CountBday().ToString();

                    gvEmployeeMasterList.DataSource = dashb.DisplayMasterList();
                    gvEmployeeMasterList.DataBind();

                    lnkCountExpiringContracts.Text = dashb.CountExpiringContracts().ToString();
                    //lblCountNewlyHired.Text = dashb.CountNewlyHired().ToString();
                    lblCountPendingEvaluation.Text = dashb.CountPendingEvaluate().ToString();
                }
                
            }
        }

        protected void lnkBdayCount_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Reports/BirthDay_Celeb.aspx");
        }
    }
}
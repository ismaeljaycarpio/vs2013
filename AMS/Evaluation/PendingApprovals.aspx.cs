using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Collections;
using System.Web.Security;
using System.Text;

namespace AMS.Evaluation
{
    public partial class PendingApprovals : System.Web.UI.Page
    {
        DAL.Evaluation eval = new DAL.Evaluation();
        DAL.Employee emp = new DAL.Employee();
        DataTable dt;

        protected void Page_Load(object sender, EventArgs e)
        {
            if(Page.IsPostBack)
                GetData();  
                BindData();
            if(!Page.IsPostBack)
            {
                MembershipUser mu = Membership.GetUser();
                Guid logged_UserId = Guid.Parse(mu.ProviderUserKey.ToString());

                if(emp.GetPosition(logged_UserId) == "HR Assistant")
                {
                    Response.Redirect("~/UnauthorizedAccess");
                }
            }
        }

        private void BindData()
        {
            //get current user
            MembershipUser _loggedUser = Membership.GetUser();
            Guid loggedUserId = Guid.Parse(_loggedUser.ProviderUserKey.ToString());
            string deptId = emp.GetDepartmentId(loggedUserId);
            dt = new DataTable();

            if(User.IsInRole("General Manager"))
            {
                //display approval for managers only
                dt = eval.getPendingApprovalGM();
            }
            else if(User.IsInRole("HR"))
            {
                dt = eval.getPendingApprovalHR();
            }
            else if(User.IsInRole("Manager"))
            {
                dt = eval.getPendingApprovalManager(deptId);
            }
            else if(User.IsInRole("Supervisor"))
            {

            }
            else
            {
                dt = null;
            }
            

            gvPendingApprovals.DataSource = dt;
            gvPendingApprovals.DataBind();

            if(dt != null)
            {
                if (dt.Rows.Count < 1)
                {
                    btnApprove.Visible = false;
                }
            }
        }

        protected void btnApprove_Click(object sender, EventArgs e)
        {
            //get current user
            MembershipUser _loggedUser = Membership.GetUser();
            Guid loggedUserId = Guid.Parse(_loggedUser.ProviderUserKey.ToString());
            string signatory = emp.GetFullName(loggedUserId);

            int count = 0;
            SetData();
            gvPendingApprovals.AllowPaging = false;
            gvPendingApprovals.DataBind();
            ArrayList arr = (ArrayList)ViewState["SelectedRecords"];
            count = arr.Count;
            for (int i = 0; i < gvPendingApprovals.Rows.Count; i++)
            {
                if (arr.Contains(gvPendingApprovals.DataKeys[i].Value))
                {
                    if(User.IsInRole("HR"))
                    {
                        eval.ApprovePendingApprovalHR(gvPendingApprovals.DataKeys[i].Value.ToString(), signatory);
                        arr.Remove(gvPendingApprovals.DataKeys[i].Value);
                    }
                    else if(User.IsInRole("Manager") || User.IsInRole("General Manager"))
                    {
                        eval.ApprovePendingApprovalManager(gvPendingApprovals.DataKeys[i].Value.ToString(), signatory);
                        arr.Remove(gvPendingApprovals.DataKeys[i].Value);
                    }
                }
            }
            ViewState["SelectedRecords"] = arr;
            hfCount.Value = "0";
            gvPendingApprovals.AllowPaging = true;
            BindData();
            ShowMessage(count);
        }

        private void ShowMessage(int count)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("<script type = 'text/javascript'>");
            sb.Append("alert('");
            sb.Append(count.ToString());
            sb.Append(" records approved.');");
            sb.Append("</script>");
            ClientScript.RegisterClientScriptBlock(this.GetType(),
                            "script", sb.ToString());
        }


        protected void gvPendingApprovals_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvPendingApprovals.PageIndex = e.NewPageIndex;
            gvPendingApprovals.DataBind();
            SetData();
        }

        private void SetData()
        {
            int currentCount = 0;
            CheckBox chkAll = (CheckBox)gvPendingApprovals.HeaderRow
                                    .Cells[0].FindControl("chkAll");
            chkAll.Checked = true;
            ArrayList arr = (ArrayList)ViewState["SelectedRecords"];
            for (int i = 0; i < gvPendingApprovals.Rows.Count; i++)
            {
                CheckBox chk = (CheckBox)gvPendingApprovals.Rows[i]
                                .Cells[0].FindControl("chk");
                if (chk != null)
                {
                    chk.Checked = arr.Contains(gvPendingApprovals.DataKeys[i].Value);
                    if (!chk.Checked)
                        chkAll.Checked = false;
                    else
                        currentCount++;
                }
            }
            hfCount.Value = (arr.Count - currentCount).ToString(); 
        }

        private void GetData()
        {
            ArrayList arr;
            if (ViewState["SelectedRecords"] != null)
                arr = (ArrayList)ViewState["SelectedRecords"];
            else
                arr = new ArrayList();
            CheckBox chkAll = (CheckBox)gvPendingApprovals.HeaderRow
                                .Cells[0].FindControl("chkAll");
            for (int i = 0; i < gvPendingApprovals.Rows.Count; i++)
            {
                if (chkAll.Checked)
                {
                    if (!arr.Contains(gvPendingApprovals.DataKeys[i].Value))
                    {
                        arr.Add(gvPendingApprovals.DataKeys[i].Value);
                    }
                }
                else
                {
                    CheckBox chk = (CheckBox)gvPendingApprovals.Rows[i]
                                       .Cells[0].FindControl("chk");
                    if (chk.Checked)
                    {
                        if (!arr.Contains(gvPendingApprovals.DataKeys[i].Value))
                        {
                            arr.Add(gvPendingApprovals.DataKeys[i].Value);
                        }
                    }
                    else
                    {
                        if (arr.Contains(gvPendingApprovals.DataKeys[i].Value))
                        {
                            arr.Remove(gvPendingApprovals.DataKeys[i].Value);
                        }
                    }
                }
            }
            ViewState["SelectedRecords"] = arr;
        }
    }
}
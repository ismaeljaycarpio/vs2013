using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace AMS.Leave
{
    public partial class Leave : System.Web.UI.Page
    {
        DAL.Leaves leaves = new DAL.Leaves();
        DAL.Employee emp = new DAL.Employee();
        DataTable dt;

        protected void Page_Load(object sender, EventArgs e)
        {
            if(!Page.IsPostBack)
            {
                if(Membership.GetUser() == null)
                {
                    Response.Redirect("~/Login.aspx");
                }

                gvPendingLeaveApprovals.DataSource = bindGridview_Pending();
                gvPendingLeaveApprovals.DataBind();
                lblPendingCount.Text = gvPendingLeaveApprovals.Rows.Count.ToString();

                gvApproved.DataSource = bindGridview_Approved();
                gvApproved.DataBind();
                lblApprovedCount.Text = gvApproved.Rows.Count.ToString();

                gvRejected.DataSource = bindGridview_Rejected();
                gvRejected.DataBind();
                lblDisapprovedCount.Text = gvRejected.Rows.Count.ToString();

                gvLeaves.DataSource = bind_generateGrid();
                gvLeaves.DataBind();

                TabName.Value = Request.Form[TabName.UniqueID];
            }
        }

        public DataTable bindGridview_Pending()
        {
            //get user from membership
            Guid _userId = Guid.Parse(Membership.GetUser().ProviderUserKey.ToString());

            //get departmentId
            string deptId = emp.GetDepartmentId(_userId);

            dt = new DataTable();

            //check logged-in user's role and dept
            if (User.IsInRole("Admin"))
            {
                //display all employee - HR/Manager/GM/Supervisor/Staff
                return dt = leaves.DisplayPendingLeaveApproval();
            }
            else if (User.IsInRole("HR"))
            {
                //display leaves whos hrApproval is empty but departmenthead approval is signed
                return dt = leaves.DisplayPendingLeaveApproval_HR();
            }
            else if (User.IsInRole("Manager"))
            {
                //display supervisors and staff by dept
                return dt = leaves.DisplayPendingLeaveApproval_Mang(deptId);
            }
            else if(User.IsInRole("Supervisor"))
            {
                return dt = leaves.DisplayPendingLeaveApproval_Sup(deptId);
            }
            return dt = null;
        }

        public DataTable bindGridview_Approved()
        {
            //get user from membership
            Guid _userId = Guid.Parse(Membership.GetUser().ProviderUserKey.ToString());

            //get departmentId
            string deptId = emp.GetDepartmentId(_userId);

            dt = new DataTable();

            //check logged-in user's role and dept
            if (User.IsInRole("Admin"))
            {
                //display all employee - HR/Manager/GM/Supervisor/Staff
                return dt = leaves.DisplayApprovedLeaveApproval();
            }
            else if (User.IsInRole("HR"))
            {
                //display leaves whos hrApproval is empty but departmenthead approval is signed
                return dt = leaves.DisplayApprovedLeaveApproval_HR();
            }
            else if (User.IsInRole("Manager"))
            {
                //display supervisors and staff by dept
                return dt = leaves.DisplayApprovedLeaveApproval_Mang(deptId);
            }
            else if (User.IsInRole("Supervisor"))
            {
                return dt = leaves.DisplayApprovedLeaveApproval_Sup(deptId);
            }
            return dt = null;
        }

        public DataTable bindGridview_Rejected()
        {
            //get user from membership
            Guid _userId = Guid.Parse(Membership.GetUser().ProviderUserKey.ToString());

            //get departmentId
            string deptId = emp.GetDepartmentId(_userId);

            dt = new DataTable();

            //check logged-in user's role and dept
            if (User.IsInRole("Admin"))
            {
                //display all employee - HR/Manager/GM/Supervisor/Staff
                return dt = leaves.DisplayRejectedLeaveApproval();
            }
            else if (User.IsInRole("HR"))
            {
                //display leaves whos hrApproval is empty but departmenthead approval is signed
                return dt = leaves.DisplayRejectedLeaveApproval_HR();
            }
            else if (User.IsInRole("Manager"))
            {
                //display supervisors and staff by dept
                return dt = leaves.DisplayRejectedLeaveApproval_Mang(deptId);
            }
            else if (User.IsInRole("Supervisor"))
            {
                return dt = leaves.DisplayRejectedLeaveApproval_Sup(deptId);
            }
            return dt = null;
        }

        protected void gvPendingLeaveApprovals_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName.Equals("approveRecord"))
            {
                int index = Convert.ToInt32(e.CommandArgument);
                string rowId = ((Label)gvPendingLeaveApprovals.Rows[index].FindControl("lblRowId")).Text;
                string noOfDays = ((Label)gvPendingLeaveApprovals.Rows[index].FindControl("lblNoOfDays")).Text;
                
                hfApproveId.Value = rowId;
                hfNoOfDays.Value = noOfDays;

                //get userid
                dt = new DataTable();
                dt = leaves.getuserId(rowId);

                hfLeaveTypeUserId.Value = dt.Rows[0]["LeaveTypeUserId"].ToString();
                hfUserId.Value = dt.Rows[0]["UserId"].ToString();

                System.Text.StringBuilder sb = new System.Text.StringBuilder();
                sb.Append(@"<script type='text/javascript'>");
                sb.Append("$('#approveModal').modal('show');");
                sb.Append(@"</script>");
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "DeleteShowModalScript", sb.ToString(), false);
            }
        }

        protected void btnApprove_Click(object sender, EventArgs e)
        {
            if(User.IsInRole("Manager") || User.IsInRole("Supervisor"))
            {
                leaves.approve_Pending_leave(true, false, hfApproveId.Value, "Approved");
            }
            else if(User.IsInRole("HR"))
            {
                //subtract no of days to his leave counts
                leaves.subtract_valid_days(hfNoOfDays.Value, hfLeaveTypeUserId.Value);

                leaves.approve_Pending_leave(false, true, hfApproveId.Value, "Approved");
    
                //should reflect in timesheet
            }

            gvPendingLeaveApprovals.DataSource = bindGridview_Pending();
            gvPendingLeaveApprovals.DataBind();
            lblPendingCount.Text = gvPendingLeaveApprovals.Rows.Count.ToString();

            gvApproved.DataSource = bindGridview_Approved();
            gvApproved.DataBind();
            lblApprovedCount.Text = gvApproved.Rows.Count.ToString();

            gvRejected.DataSource = bindGridview_Rejected();
            gvRejected.DataBind();
            lblDisapprovedCount.Text = gvRejected.Rows.Count.ToString();

            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            sb.Append(@"<script type='text/javascript'>");
            sb.Append("$('#approveModal').modal('hide');");
            sb.Append(@"</script>");
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "DeleteHideModalScript", sb.ToString(), false);
        }

        protected void btnDisapprove_Click(object sender, EventArgs e)
        {
            if (User.IsInRole("Manager") || User.IsInRole("Supervisor"))
            {
                leaves.approve_Pending_leave(true, false, hfApproveId.Value, "Disapproved");
            }
            else if (User.IsInRole("HR"))
            {
                leaves.approve_Pending_leave(false, true, hfApproveId.Value, "Disapproved");
            }

            gvPendingLeaveApprovals.DataSource = bindGridview_Pending();
            gvPendingLeaveApprovals.DataBind();
            lblPendingCount.Text = gvPendingLeaveApprovals.Rows.Count.ToString();

            gvApproved.DataSource = bindGridview_Approved();
            gvApproved.DataBind();
            lblApprovedCount.Text = gvApproved.Rows.Count.ToString();

            gvRejected.DataSource = bindGridview_Rejected();
            gvRejected.DataBind();
            lblDisapprovedCount.Text = gvRejected.Rows.Count.ToString();

            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            sb.Append(@"<script type='text/javascript'>");
            sb.Append("$('#approveModal').modal('hide');");
            sb.Append(@"</script>");
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "DeleteHideModalScript", sb.ToString(), false);
        }

        protected void gvPendingLeaveApprovals_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvPendingLeaveApprovals.PageIndex = e.NewPageIndex;
            gvPendingLeaveApprovals.DataSource = bindGridview_Pending();
            gvPendingLeaveApprovals.DataBind();
        }
        protected void gvApproved_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvApproved.PageIndex = e.NewPageIndex;
            gvApproved.DataSource = bindGridview_Approved();
            gvApproved.DataBind();
        }
        protected void gvRejected_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvRejected.PageIndex = e.NewPageIndex;
            gvRejected.DataSource = bindGridview_Rejected();
            gvRejected.DataBind();
        }
        
        protected void gvApproved_RowCommand(object sender, GridViewCommandEventArgs e)
        {

        }

        protected void gvRejected_RowCommand(object sender, GridViewCommandEventArgs e)
        {
        }

        protected void gvLeaves_Sorting(object sender, GridViewSortEventArgs e)
        {

        }

        protected void gvLeaves_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {

        }

        protected void gvLeaves_RowDataBound(object sender, GridViewRowEventArgs e)
        {

        }

        protected void gvLeaves_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
        {

        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            gvLeaves.DataSource = bind_generateGrid();
            gvLeaves.DataBind();
        }

        protected void btnExcel_Click(object sender, EventArgs e)
        {
            //Create a dummy GridView
            GridView GridView1 = new GridView();
            GridView1.AllowPaging = false;
            GridView1.DataSource = bind_generateGrid();
            GridView1.DataBind();

            Response.Clear();
            Response.Buffer = true;
            Response.AddHeader("content-disposition",
             "attachment;filename=Leave_Report.xls");
            Response.Charset = "";
            Response.ContentType = "application/vnd.ms-excel";
            StringWriter sw = new StringWriter();
            HtmlTextWriter hw = new HtmlTextWriter(sw);

            for (int i = 0; i < GridView1.Rows.Count; i++)
            {
                //Apply text style to each Row
                GridView1.Rows[i].Attributes.Add("class", "textmode");
            }
            GridView1.RenderControl(hw);

            //style to format numbers to string
            string style = @"<style> .textmode { mso-number-format:\@; } </style>";
            Response.Write(style);
            Response.Output.Write(sw.ToString());
            Response.Flush();
            Response.End();

            //var leaves = bind_generateGrid();
            //ExcelPackage excel = new ExcelPackage();
            //var workSheet = excel.Workbook.Worksheets.Add("Leaves");
            //var totalCols = leaves.Columns.Count;
            //var totalRows = leaves.Rows.Count;

            //for (var col = 1; col <= totalCols; col++)
            //{
            //    workSheet.Cells[1, col].Value = leaves.Columns[col - 1].ColumnName;
            //}
            //for (var row = 1; row <= totalRows; row++)
            //{
            //    for (var col = 0; col < totalCols; col++)
            //    {
            //        workSheet.Cells[row + 1, col + 1].Value = leaves.Rows[row - 1][col];
            //    }
            //}
            //using (var memoryStream = new MemoryStream())
            //{
            //    Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
            //    Response.AddHeader("content-disposition", "attachment;  filename=Leave_Report.xlsx");
            //    excel.SaveAs(memoryStream);
            //    memoryStream.WriteTo(Response.OutputStream);
            //    Response.Flush();
            //    Response.End();
            //}
        }

        protected DataTable bind_generateGrid()
        {
            //get user from membership
            Guid _userId = Guid.Parse(Membership.GetUser().ProviderUserKey.ToString());

            //get departmentId
            string deptId = emp.GetDepartmentId(_userId);
            string leaveStatus = ddlStatus.SelectedItem.Text;
            dt = new DataTable();

            //no date filter
            if(txtStartDate.Text.Equals(String.Empty) && txtEndDate.Text.Equals(String.Empty))
            {
                if(User.IsInRole("Admin") || User.IsInRole("HR"))
                {
                    //display all leaves
                    return dt = leaves.DisplayLeaveApproval_Admin(leaveStatus);
                }
                else if (User.IsInRole("Manager"))
                {
                    return dt = leaves.DisplayLeaveApproval_Mang(deptId, leaveStatus);
                }
                else if(User.IsInRole("Supervisor"))
                {
                    return dt = leaves.DisplayLeaveApproval_Sup(deptId, leaveStatus);
                }
            }
                //start date
            else if(!txtStartDate.Text.Equals(String.Empty) && txtEndDate.Text.Equals(String.Empty))
            {
                if (User.IsInRole("Admin") || User.IsInRole("HR"))
                {
                    //display all leaves
                    return dt = leaves.DisplayLeaveApproval_Admin(leaveStatus, txtStartDate.Text);
                }
                else if (User.IsInRole("Manager"))
                {
                    return dt = leaves.DisplayLeaveApproval_Mang(deptId, leaveStatus, txtStartDate.Text);
                }
                else if (User.IsInRole("Supervisor"))
                {
                    return dt = leaves.DisplayLeaveApproval_Sup(deptId, leaveStatus, txtStartDate.Text);
                }
            }
                //date range
            else if (!txtStartDate.Text.Equals(String.Empty) && !txtEndDate.Text.Equals(String.Empty))
            {
                if (User.IsInRole("Admin") || User.IsInRole("HR"))
                {
                    //display all leaves
                    return dt = leaves.DisplayLeaveApproval_Admin(leaveStatus, txtStartDate.Text, txtEndDate.Text);
                }
                else if (User.IsInRole("Manager"))
                {
                    return dt = leaves.DisplayLeaveApproval_Mang(deptId, leaveStatus, txtStartDate.Text, txtEndDate.Text);
                }
                else if (User.IsInRole("Supervisor"))
                {
                    return dt = leaves.DisplayLeaveApproval_Sup(deptId, leaveStatus, txtStartDate.Text, txtEndDate.Text);
                }
            }

            return dt = null;
        }

    }
}
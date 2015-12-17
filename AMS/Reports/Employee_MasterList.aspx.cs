using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Security;
using System.Data;
using System.IO;


namespace AMS.Reports
{
    public partial class Employee_MasterList : System.Web.UI.Page
    {
        DAL.Filler filler = new DAL.Filler();
        DAL.Dashboard dashb = new DAL.Dashboard();
        DAL.Employee emp = new DAL.Employee();
        DataTable dt;
        
        protected void Page_Load(object sender, EventArgs e)
        {
            if(!Page.IsPostBack)
            {
                ddlStatus.DataSource = filler.fillAccountStatus(true);
                ddlStatus.DataValueField = "Id";
                ddlStatus.DataTextField = "AccountStatus";
                ddlStatus.DataBind();

                //chk if expiring contract is selected
                if (Request.QueryString["Exp"] != null)
                {
                    ddlStatus.SelectedValue = "5";
                }

                gvEmployee.DataSource = BindGridView();               
                gvEmployee.DataBind();

                gvExpiringContract.DataSource = BindExpiringContracts();
                gvExpiringContract.DataBind();
            }
        }

        private DataTable BindExpiringContracts()
        {
            dt = new DataTable();

            //get deptId
            Guid UserId = Guid.Parse(Membership.GetUser().ProviderUserKey.ToString());
            string deptId = emp.GetDepartmentId(UserId);

            if (!User.IsInRole("Admin") &&
                !User.IsInRole("General Manager") &&
                !User.IsInRole("HR"))
            {
                dt = dashb.DisplayExpiringContract(deptId);
            }
            else
            {
                dt = dashb.DisplayExpiringContract();
            }

            return dt;
        }

        private DataTable BindGridView()
        {
            dt = new DataTable();

            //get deptId
            Guid UserId = Guid.Parse(Membership.GetUser().ProviderUserKey.ToString());
            string deptId = emp.GetDepartmentId(UserId);

            if(!User.IsInRole("Admin") &&
                !User.IsInRole("General Manager") &&
                !User.IsInRole("HR"))
            {
                dt = dashb.DisplayMasterList(txtSearch.Text,
                    ddlStatus.SelectedValue,
                    deptId);
            }
            else
            {
                dt = dashb.DisplayMasterList(txtSearch.Text, ddlStatus.SelectedValue);
            }
            
            return dt;
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            gvEmployee.DataSource = BindGridView();
            gvEmployee.DataBind();
        }

        protected void gvEmployee_Sorting(object sender, GridViewSortEventArgs e)
        {
            string sortingDirection = string.Empty;
            if (direction == SortDirection.Ascending)
            {
                direction = SortDirection.Descending;
                sortingDirection = "Desc";
            }
            else
            {
                direction = SortDirection.Ascending;
                sortingDirection = "Asc";
            }

            DataView sortedView = new DataView(BindGridView());
            sortedView.Sort = e.SortExpression + " " + sortingDirection;
            Session["SortedView_ml"] = sortedView;
            gvEmployee.DataSource = sortedView;
            gvEmployee.DataBind();
        }

        protected void gvEmployee_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if(e.Row.RowType == DataControlRowType.Footer)
            {
                int _TotalRecs = BindGridView().Rows.Count;
                int _CurrentRecStart = gvEmployee.PageIndex * gvEmployee.PageSize + 1;
                int _CurrentRecEnd = gvEmployee.PageIndex * gvEmployee.PageSize + gvEmployee.Rows.Count;

                e.Row.Cells[0].ColumnSpan = 2;
                e.Row.Cells[0].Text = string.Format("Displaying {0} to {1} of {2} records found", _CurrentRecStart, _CurrentRecEnd, _TotalRecs);
            }
        }

        protected void gvEmployee_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            this.gvEmployee.PageIndex = e.NewPageIndex;
            if (Session["SortedView_ml"] != null)
            {
                gvEmployee.DataSource = Session["SortedView_ml"];
                gvEmployee.DataBind();
            }
            else
            {
                gvEmployee.DataSource = BindGridView();
                gvEmployee.DataBind();
            }
        }

        protected void btnExportToPDF_Click(object sender, EventArgs e)
        {
            //Create a dummy GridView
            GridView GridView1 = new GridView();
            GridView1.AllowPaging = false;
            GridView1.DataSource = BindGridView();
            GridView1.DataBind();

            Response.Clear();
            Response.Buffer = true;
            Response.AddHeader("content-disposition",
                "attachment;filename=" + DateTime.Now.Year  + "Employee_MasterList" + ".doc");
            Response.Charset = "";
            Response.ContentType = "application/vnd.ms-word ";
            StringWriter sw = new StringWriter();
            HtmlTextWriter hw = new HtmlTextWriter(sw);
            GridView1.RenderControl(hw);
            Response.Output.Write(sw.ToString());
            Response.Flush();
            Response.End();
        }

        protected void btnExcel_Click(object sender, EventArgs e)
        {
            //Create a dummy GridView
            GridView GridView1 = new GridView();
            GridView1.AllowPaging = false;
            GridView1.DataSource = BindGridView();
            GridView1.DataBind();

            Response.Clear();
            Response.Buffer = true;
            Response.AddHeader("content-disposition",
             "attachment;filename=EmployeeList.xls");
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
        }

        protected void btnExpiraingContract_Word_Click(object sender, EventArgs e)
        {
            //Create a dummy GridView
            GridView GridView1 = new GridView();
            GridView1.AllowPaging = false;
            GridView1.DataSource = BindExpiringContracts();
            GridView1.DataBind();

            Response.Clear();
            Response.Buffer = true;
            Response.AddHeader("content-disposition",
                "attachment;filename=" + DateTime.Now.Year + "Employee_MasterList_Expiring_Contracts" + ".doc");
            Response.Charset = "";
            Response.ContentType = "application/vnd.ms-word ";
            StringWriter sw = new StringWriter();
            HtmlTextWriter hw = new HtmlTextWriter(sw);
            GridView1.RenderControl(hw);
            Response.Output.Write(sw.ToString());
            Response.Flush();
            Response.End();
        }

        protected void btnExpiringContract_Excel_Click(object sender, EventArgs e)
        {
            //Create a dummy GridView
            GridView GridView1 = new GridView();
            GridView1.AllowPaging = false;
            GridView1.DataSource = BindExpiringContracts();
            GridView1.DataBind();

            Response.Clear();
            Response.Buffer = true;
            Response.AddHeader("content-disposition",
             "attachment;filename=EmployeeList.xls");
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
        }

        public SortDirection direction
        {
            get
            {
                if (ViewState["directionState"] == null)
                {
                    ViewState["directionState"] = SortDirection.Ascending;
                }
                return (SortDirection)ViewState["directionState"];
            }

            set
            {
                ViewState["directionState"] = value;
            }
        }

        protected void gvExpiringContract_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
        {
            gvExpiringContract.SelectedIndex = Convert.ToInt32(e.NewSelectedIndex);
            Session["UserId"] = gvExpiringContract.SelectedDataKey.Value;
            Response.Redirect("~/Employee/JobDetails");
        }

        protected void gvEmployee_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
        {
            gvEmployee.SelectedIndex = Convert.ToInt32(e.NewSelectedIndex);
            Session["UserId"] = gvEmployee.SelectedDataKey.Value;
            Response.Redirect("~/Employee/JobDetails");
        }
    }
}
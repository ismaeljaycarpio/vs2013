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

        private object BindExpiringContracts()
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

        }

        protected void gvEmployee_RowDataBound(object sender, GridViewRowEventArgs e)
        {

        }

        protected void gvEmployee_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            this.gvEmployee.PageIndex = e.NewPageIndex;
            if (Session["SortedView"] != null)
            {
                gvEmployee.DataSource = Session["SortedView"];
                gvEmployee.DataBind();
            }
            else
            {
                gvEmployee.DataSource = BindGridView();
                gvEmployee.DataBind();
            }
        }

        protected void gvEmployee_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
        {

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

        }

        protected void btnExpiringContract_Excel_Click(object sender, EventArgs e)
        {

        }
    }
}
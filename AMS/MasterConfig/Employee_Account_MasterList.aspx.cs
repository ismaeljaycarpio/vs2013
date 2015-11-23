using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace AMS.MasterConfig
{
    public partial class Employee_Account_MasterList : System.Web.UI.Page
    {
        DAL.Account accnt = new DAL.Account();
        protected void Page_Load(object sender, EventArgs e)
        {
            if(!Page.IsPostBack)
            {
                BindGridView();
            }
        }

        protected void BindGridView()
        {
            gvEmployee.DataSource = accnt.DisplayUserAccounts(txtSearch.Text.Trim());
            gvEmployee.DataBind();
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            BindGridView();
        }

        protected void gvEmployee_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            this.gvEmployee.PageIndex = e.NewPageIndex;
            BindGridView();
        }

        protected void btnExportToPDF_Click(object sender, EventArgs e)
        {

        }

        protected void btnExcel_Click(object sender, EventArgs e)
        {

        }


        protected void gvEmployee_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
        {

        }

        protected void lblStatus_Click(object sender, EventArgs e)
        {
            LinkButton lnkStatus = sender as LinkButton;
            GridViewRow gvrow = lnkStatus.NamingContainer as GridViewRow;
            Guid UserId = Guid.Parse(gvEmployee.DataKeys[gvrow.RowIndex].Value.ToString());

            if(lnkStatus.Text == "Active")
            {
                accnt.DeactivateUser(UserId);
            }
            else
            {
                accnt.ActivateUser(UserId);
            }
            BindGridView();
        }

        protected void gvEmployee_Sorting(object sender, GridViewSortEventArgs e)
        {

        }

        protected void lblReset_Click(object sender, EventArgs e)
        {
            LinkButton lnkReset = sender as LinkButton;
            GridViewRow gvrow = lnkReset.NamingContainer as GridViewRow;
            Guid UserId = Guid.Parse(gvEmployee.DataKeys[gvrow.RowIndex].Value.ToString());

            accnt.ResetPassword(UserId);
            BindGridView();
        }

        protected void gvEmployee_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                LinkButton lnkStatus = (LinkButton)e.Row.FindControl("lblStatus");
                LinkButton lnkReset = (LinkButton)e.Row.FindControl("lblReset");

                if (lnkStatus.Text == "Active")
                {
                    lnkStatus.Attributes.Add("onclick", "return confirm('Do you want to deactivate this user ? ');");
                }
                else
                {
                    lnkStatus.Attributes.Add("onclick", "return confirm('Do you want to activate this user ? ');");
                }
                
                lnkReset.Attributes.Add("onclick", "return confirm('Do you want to reset the password of this user ? ');");
            }
        }
    }
}
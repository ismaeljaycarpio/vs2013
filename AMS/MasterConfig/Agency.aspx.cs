using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Web.Security;

namespace AMS.MasterConfig
{
    public partial class Agency : System.Web.UI.Page
    {
        DAL.Agency agency = new DAL.Agency();
        DataTable dt;

        protected void Page_Load(object sender, EventArgs e)
        {
            if(!Page.IsPostBack)
            {
                BindData();

                //show/hide controls based on role
                if (!User.IsInRole("Admin") && !User.IsInRole("HR"))
                {
                    gvAgency.Columns[1].Visible = false;
                    gvAgency.Columns[3].Visible = false;
                }
            }
        }

        private void BindData()
        {
            gvAgency.DataSource = agency.DisplayAgency();
            gvAgency.DataBind();
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            //get user empid
            MembershipUser loggedUser = Membership.GetUser();
            string loggedUser_empId = loggedUser.UserName;

            if (!agency.CheckIfDuplicate(txtAddAgency.Text))
            {
                agency.AddAgency(txtAddAgency.Text, 
                    loggedUser_empId);
            }

            BindData();

            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            sb.Append(@"<script type='text/javascript'>");
            sb.Append("$('#addModal').modal('hide');");
            sb.Append(@"</script>");
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "HideShowModalScript", sb.ToString(), false);
        }

        protected void btnUpdate_Click(object sender, EventArgs e)
        {
            //get user empid
            MembershipUser loggedUser = Membership.GetUser();
            string loggedUser_empId = loggedUser.UserName;

            if (!agency.CheckIfDuplicate(txtEditAgency.Text))
            {
                agency.UpdateAgency(txtEditAgency.Text,
                    loggedUser_empId,
                    lblRowId.Text);
            }


            BindData();

            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            sb.Append(@"<script type='text/javascript'>");
            sb.Append("$('#updateModal').modal('hide');");
            sb.Append(@"</script>");
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "EditHideModalScript", sb.ToString(), false);
        }

        protected void gvAgency_RowCommand(object sender, GridViewCommandEventArgs e)
        {            
            if (e.CommandName.Equals("editRecord"))
            {
                dt = new DataTable();
                int index = Convert.ToInt32(e.CommandArgument);
                System.Text.StringBuilder sb = new System.Text.StringBuilder();

                dt = agency.GetAgencyById((int)(gvAgency.DataKeys[index].Value));
                lblRowId.Text = dt.Rows[0]["Id"].ToString();
                txtEditAgency.Text = dt.Rows[0]["Agency"].ToString();

                sb.Append(@"<script type='text/javascript'>");
                sb.Append("$('#updateModal').modal('show');");
                sb.Append(@"</script>");
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "EditShowModalScript", sb.ToString(), false);
            }
        }

        protected void gvAgency_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            string rowId = ((Label)gvAgency.Rows[e.RowIndex].FindControl("lblRowId")).Text;
            agency.DeleteAgency(rowId);

            BindData();
        }
    }
}
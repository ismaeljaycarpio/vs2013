using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

namespace AMS.Employee
{
    public partial class PersonalCards : System.Web.UI.Page
    {
        DAL.MembershipCard memCard = new DAL.MembershipCard();
        DataTable dt;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                if(Session["UserId"] == null)
                {
                    Response.Redirect("~/Employee/Employee");
                }

                //get selected user
                hfUserId.Value = Session["UserId"].ToString();

                BindData();

                if (!User.IsInRole("Admin") && !User.IsInRole("HR"))
                {
                    btnOpenModal.Visible = false;
                    gvMembership.Columns[1].Visible = false;
                    gvMembership.Columns[6].Visible = false;
                }
            }
        }

        private void BindData()
        {
            Guid UserId = Guid.Parse(hfUserId.Value);
            gvMembership.DataSource = memCard.getPersonalCardsById(UserId);
            gvMembership.DataBind();
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            memCard.addMembershipCard(Guid.Parse(hfUserId.Value),
                txtAddType.Text,
                txtAddNumber.Text,
                txtAddIssuedDate.Text,
                txtAddExpirationDate.Text);

            BindData();

            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            sb.Append(@"<script type='text/javascript'>");
            sb.Append("$('#addModal').modal('hide');");
            sb.Append(@"</script>");
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "HideShowModalScript", sb.ToString(), false);
        }

        protected void btnUpdate_Click(object sender, EventArgs e)
        {
            memCard.updateMembershipCard(
                txtEditType.Text,
                txtEditNumber.Text,
                txtEditIssuedDate.Text,
                txtEditExpirationDate.Text,
                lblRowId.Text);

            BindData();

            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            sb.Append(@"<script type='text/javascript'>");
            sb.Append("$('#updateModal').modal('hide');");
            sb.Append(@"</script>");
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "EditHideModalScript", sb.ToString(), false);
        }

        protected void gvMembership_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            string rowId = ((Label)gvMembership.Rows[e.RowIndex].FindControl("lblRowId")).Text;
            memCard.deletePersonalCard(rowId);

            BindData();
        }

        protected void gvMembership_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            dt = new DataTable();

            int index = Convert.ToInt32(e.CommandArgument);
            if (e.CommandName.Equals("editRecord"))
            {
                System.Text.StringBuilder sb = new System.Text.StringBuilder();

                dt = memCard.getPersonalCardByRowId((int)(gvMembership.DataKeys[index].Value));
                lblRowId.Text = dt.Rows[0]["Id"].ToString();
                txtEditType.Text = dt.Rows[0]["TYPE"].ToString();
                txtEditNumber.Text = dt.Rows[0]["NUMBER"].ToString();
                txtEditIssuedDate.Text = dt.Rows[0]["IDATE"].ToString();
                txtEditExpirationDate.Text = dt.Rows[0]["EDATE"].ToString();

                sb.Append(@"<script type='text/javascript'>");
                sb.Append("$('#updateModal').modal('show');");
                sb.Append(@"</script>");
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "EditShowModalScript", sb.ToString(), false);
            }
        }

        protected void btnOpenModal_Click(object sender, EventArgs e)
        {
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            sb.Append(@"<script type='text/javascript'>");
            sb.Append("$('#addModal').modal('show');");
            sb.Append(@"</script>");
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "AddShowModalScript", sb.ToString(), false);
        }
    }
}
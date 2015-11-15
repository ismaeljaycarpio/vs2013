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
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
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
            DAL.MembershipCard memCard = new DAL.MembershipCard();
            DataTable dt = new DataTable();
            Guid UserId = Guid.Parse(Session["UserId"].ToString());
            dt = memCard.getPersonalCardsById(UserId);

            gvMembership.DataSource = dt;
            gvMembership.DataBind();
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            DAL.MembershipCard memCard = new DAL.MembershipCard();
            memCard.addMembershipCard(Guid.Parse(Session["UserId"].ToString()),
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
            DAL.MembershipCard memCard = new DAL.MembershipCard();
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
            DAL.MembershipCard memCard = new DAL.MembershipCard();
            memCard.deletePersonalCard(rowId);

            BindData();
        }

        protected void gvMembership_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            DataTable dt = new DataTable();

            int index = Convert.ToInt32(e.CommandArgument);
            if (e.CommandName.Equals("editRecord"))
            {
                System.Text.StringBuilder sb = new System.Text.StringBuilder();

                DAL.MembershipCard memCard = new DAL.MembershipCard();
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
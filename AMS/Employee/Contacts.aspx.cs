using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

namespace AMS.Employee
{
    public partial class Contacts : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                if(Session["UserId"] == null)
                {
                    Response.Redirect("~/Employee/Employee");
                }

                hfUserId.Value = Session["UserId"].ToString();
                BindData();

                if (!User.IsInRole("Admin") && !User.IsInRole("HR"))
                {
                    btnOpenModal.Visible = false;
                    gvContacts.Columns[1].Visible = false;
                    gvContacts.Columns[6].Visible = false;
                }
            }
        }

        private void BindData()
        {
            DAL.Contact contact = new DAL.Contact();
            DataTable dt = new DataTable();
            Guid UserId = Guid.Parse(hfUserId.Value);
            dt = contact.getContactById(UserId);

            gvContacts.DataSource = dt;
            gvContacts.DataBind();
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            DAL.Contact contact = new DAL.Contact();
            contact.addContact(
                Guid.Parse(hfUserId.Value),
                txtAddAddress.Text,
                txtAddHomeAddress.Text,
                txtAddCity.Text,
                txtAddProvince.Text,
                txtAddZipCode.Text,
                txtAddCountry.Text,
                txtAddPhoneNumber.Text,
                txtAddEmail.Text,
                txtAddGuardianName.Text,
                txtAddRelationship.Text,
                txtAddGuardianAddress.Text,
                txtAddGuardianPhone.Text);

            BindData();

            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            sb.Append(@"<script type='text/javascript'>");
            sb.Append("$('#addModal').modal('hide');");
            sb.Append(@"</script>");
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "HideShowModalScript", sb.ToString(), false);
        }

        protected void btnUpdate_Click(object sender, EventArgs e)
        {
            DAL.Contact contact = new DAL.Contact();
            contact.updateContact(
                txtEditAddress.Text,
                txtEditHomeAddress.Text,
                txtEditCity.Text,
                txtEditProvince.Text,
                txtEditZipCode.Text,
                txtEditCountry.Text,
                txtEditPhoneNumber.Text,
                txtEditEmail.Text,
                txtEditGuardianName.Text,
                txtEditRelationship.Text,
                txtEditGuardianAddress.Text,
                txtEditGuardianPhone.Text,
                lblRowId.Text);

            BindData();

            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            sb.Append(@"<script type='text/javascript'>");
            sb.Append("$('#updateModal').modal('hide');");
            sb.Append(@"</script>");
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "EditHideModalScript", sb.ToString(), false);
        }

        protected void gvContacts_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            string rowId = ((Label)gvContacts.Rows[e.RowIndex].FindControl("lblRowId")).Text;
            DAL.Contact contact = new DAL.Contact();
            contact.deleteContact(rowId);

            BindData();
        }

        protected void gvContacts_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            DataTable dt = new DataTable();

            int index = Convert.ToInt32(e.CommandArgument);
            if (e.CommandName.Equals("editRecord"))
            {
                System.Text.StringBuilder sb = new System.Text.StringBuilder();

                DAL.Contact contact = new DAL.Contact();
                dt = contact.getContactByRowId((int)(gvContacts.DataKeys[index].Value));
                lblRowId.Text = dt.Rows[0]["Id"].ToString();

                txtEditAddress.Text = dt.Rows[0]["Address"].ToString();
                txtEditHomeAddress.Text = dt.Rows[0]["Home_Address"].ToString();
                txtEditCity.Text = dt.Rows[0]["City"].ToString();
                txtEditProvince.Text = dt.Rows[0]["Province"].ToString();
                txtEditZipCode.Text = dt.Rows[0]["ZipCode"].ToString();
                txtEditCountry.Text = dt.Rows[0]["CountryId"].ToString();
                txtEditPhoneNumber.Text = dt.Rows[0]["PhoneNo"].ToString();
                txtEditEmail.Text = dt.Rows[0]["Email"].ToString();
                txtEditGuardianName.Text = dt.Rows[0]["G_Name"].ToString();
                txtEditRelationship.Text = dt.Rows[0]["Relationship"].ToString();
                txtEditGuardianAddress.Text = dt.Rows[0]["G_Address"].ToString();
                txtEditGuardianPhone.Text = dt.Rows[0]["G_Phone"].ToString();



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
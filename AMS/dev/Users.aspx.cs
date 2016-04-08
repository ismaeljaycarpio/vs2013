using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace AMS.dev
{
    public partial class Users : System.Web.UI.Page
    {
        eHRISContextDataContext db = new eHRISContextDataContext();
        DAL.Account accnt = new DAL.Account();
        DAL.Filler filler = new DAL.Filler();
        DataTable dt;

        protected void Page_Load(object sender, EventArgs e)
        {
            if(!Page.IsPostBack)
            {
                bindGridview();
                bindDropdown();
            }
        }
        protected void lblStatus_Click(object sender, EventArgs e)
        {
            LinkButton lnkStatus = sender as LinkButton;
            GridViewRow gvrow = lnkStatus.NamingContainer as GridViewRow;
            Guid UserId = Guid.Parse(gvSpecUsers.DataKeys[gvrow.RowIndex].Value.ToString());

            if (lnkStatus.Text == "Active")
            {
                accnt.DeactivateUser(UserId);
            }
            else
            {
                accnt.ActivateUser(UserId);
            }
            bindGridview();
        }

        protected void lbtnLockedOut_Click(object sender, EventArgs e)
        {
            LinkButton lbtnLockedOut_Click = sender as LinkButton;
            GridViewRow gvrow = lbtnLockedOut_Click.NamingContainer as GridViewRow;
            Guid UserId = Guid.Parse(gvSpecUsers.DataKeys[gvrow.RowIndex].Value.ToString());

            MembershipUser getUser = Membership.GetUser(UserId);

            if (lbtnLockedOut_Click.Text == "Yes")
            {
                //unlock
                getUser.UnlockUser();
            }
            else
            {
                //lock
                accnt.LockUser(UserId);
            }
            bindGridview();
        }

        protected void lblReset_Click(object sender, EventArgs e)
        {
            LinkButton lnkReset = sender as LinkButton;
            GridViewRow gvrow = lnkReset.NamingContainer as GridViewRow;
            Guid UserId = Guid.Parse(gvSpecUsers.DataKeys[gvrow.RowIndex].Value.ToString());

            accnt.ResetPassword(UserId);
            bindGridview();
        }

        protected void bindGridview()
        {
            var users = from m in db.MembershipLINQs
                        join u in db.Users
                        on m.UserId equals u.UserId
                        join uir in db.UsersInRoles
                        on u.UserId equals uir.UserId
                        join r in db.Roles
                        on uir.RoleId equals r.RoleId
                        where
                        (r.RoleName == "Developer" || r.RoleName == "loginadmin")
                        select new
                        {
                            UserId = m.UserId,
                            UserName = u.UserName,
                            RoleName = r.RoleName,
                            IsApproved = m.IsApproved,
                            IsLockedOut = m.IsLockedOut,
                            FailedPasswordAttemptCount = m.FailedPasswordAttemptCount
                        };

            gvSpecUsers.DataSource = users.ToList();
            gvSpecUsers.DataBind();
        }

        protected void gvSpecUsers_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                LinkButton lnkStatus = (LinkButton)e.Row.FindControl("lblStatus");
                LinkButton lnkReset = (LinkButton)e.Row.FindControl("lblReset");
                LinkButton lbtnLockedOut = (LinkButton)e.Row.FindControl("lbtnLockedOut");

                if (lnkStatus.Text == "Active")
                {
                    lnkStatus.Attributes.Add("onclick", "return confirm('Do you want to deactivate this user ? ');");
                }
                else
                {
                    lnkStatus.Attributes.Add("onclick", "return confirm('Do you want to activate this user ? ');");
                }

                lnkReset.Attributes.Add("onclick", "return confirm('Do you want to reset the password of this user ? ');");

                if (lbtnLockedOut.Text == "Yes")
                {
                    lbtnLockedOut.Attributes.Add("onclick", "return confirm('Do you want to Unlock this user ? ');");
                }
                else
                {
                    lbtnLockedOut.Attributes.Add("onclick", "return confirm('Do you want to Lock this user ? ');");
                }
            }
        }

        protected void gvSpecUsers_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if(e.CommandName.Equals("changePassword"))
            {
                int index = Convert.ToInt32(e.CommandArgument);
                Guid userId = Guid.Parse(gvSpecUsers.DataKeys[index].Value.ToString());
                hfUserId.Value = userId.ToString();

                System.Text.StringBuilder sb = new System.Text.StringBuilder();
                sb.Append(@"<script type='text/javascript'>");
                sb.Append("$('#changePasswordModal').modal('show');");
                sb.Append(@"</script>");
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "EditShowModalScript", sb.ToString(), false);
            }
            else if(e.CommandName.Equals("editRole"))
            {
                int index = Convert.ToInt32(e.CommandArgument);
                Guid userId = Guid.Parse(gvSpecUsers.DataKeys[index].Value.ToString());
                hfRoleId.Value = userId.ToString();

                var user = (from u in db.Users
                           join uir in db.UsersInRoles
                           on u.UserId equals uir.UserId
                           join r in db.Roles
                           on uir.RoleId equals r.RoleId
                           where
                           u.UserId == userId
                           select new
                           {
                               RoleId = r.RoleId
                           }).FirstOrDefault();

                ddlRole.SelectedValue = user.RoleId.ToString();

                System.Text.StringBuilder sb = new System.Text.StringBuilder();
                sb.Append(@"<script type='text/javascript'>");
                sb.Append("$('#editRoleModal').modal('show');");
                sb.Append(@"</script>");
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "EditShowModalScript", sb.ToString(), false);
            }
            else if(e.CommandName.Equals("deleteRecord"))
            {
                int index = Convert.ToInt32(e.CommandArgument);
                Guid userId = Guid.Parse(gvSpecUsers.DataKeys[index].Value.ToString());
                hfDeleteId.Value = userId.ToString();

                System.Text.StringBuilder sb = new System.Text.StringBuilder();
                sb.Append(@"<script type='text/javascript'>");
                sb.Append("$('#deleteModal').modal('show');");
                sb.Append(@"</script>");
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "EditShowModalScript", sb.ToString(), false);
            }
        }

        protected void btnChangePassword_Click(object sender, EventArgs e)
        {
            MembershipUser mu = Membership.GetUser(Guid.Parse(hfUserId.Value));
            mu.ChangePassword(mu.ResetPassword(), txtNewPassword.Text);

            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            sb.Append(@"<script type='text/javascript'>");
            sb.Append("$('#changePasswordModal').modal('hide');");
            sb.Append(@"</script>");
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "EditShowModalScript", sb.ToString(), false);
        }

        protected void btnEditRole_Click(object sender, EventArgs e)
        {
            accnt.ChangeRole(Guid.Parse(hfRoleId.Value), ddlRole.SelectedItem.Text);
            bindGridview();

            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            sb.Append(@"<script type='text/javascript'>");
            sb.Append("$('#editRoleModal').modal('hide');");
            sb.Append(@"</script>");
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "EditShowModalScript", sb.ToString(), false);
        }

        protected void btnDelete_Click(object sender, EventArgs e)
        {
            string userName = Membership.GetUser(Guid.Parse(hfDeleteId.Value)).UserName;
            Membership.DeleteUser(userName, true);

            bindGridview();

            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            sb.Append(@"<script type='text/javascript'>");
            sb.Append("$('#deleteModal').modal('hide');");
            sb.Append(@"</script>");
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "EditShowModalScript", sb.ToString(), false);
        }

        protected void bindDropdown()
        {
            //load role in here
            var roles = from r in db.Roles
                        where
                        (r.RoleName == "loginadmin" || r.RoleName == "Developer")
                        select r;

            ddlRole.DataSource = roles.ToList();
            ddlRole.DataTextField = "RoleName";
            ddlRole.DataValueField = "RoleId";
            ddlRole.DataBind();
        }
    }
}
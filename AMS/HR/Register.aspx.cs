using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Web.Security;

namespace AMS.HR
{
    public partial class Register : System.Web.UI.Page
    {
        DAL.Employee emp = new DAL.Employee();
        DAL.PositionManagement position = new DAL.PositionManagement();
        DAL.Filler filler = new DAL.Filler();

        protected void Page_Load(object sender, EventArgs e)
        {
            if(!Page.IsPostBack)
            {
                if(User.IsInRole("Admin"))
                {
                    ddlPosition.DataSource = filler.fillPosition(true);
                    ddlPosition.DataValueField = "Id";
                    ddlPosition.DataTextField = "Position";
                    ddlPosition.DataBind();
                }
                else
                {
                    ddlPosition.DataSource = filler.fillPosition(false);
                    ddlPosition.DataValueField = "Id";
                    ddlPosition.DataTextField = "Position";
                    ddlPosition.DataBind();
                }
                

                ddlDepartment.DataSource = filler.fillDepartment();
                ddlDepartment.DataValueField = "Id";
                ddlDepartment.DataTextField = "Department";
                ddlDepartment.DataBind();

                ddlRole.DataSource = filler.fillRoles();
                ddlRole.DataValueField = "RoleId";
                ddlRole.DataTextField = "RoleName";
                ddlRole.DataBind();

                //load dept based on pos
                ddlDepartment.SelectedValue = position.GetDepartmentIdBypPosition(ddlPosition.SelectedValue.ToString());
            }
        }

        protected void btnRegister_Click(object sender, EventArgs e)
        {
            lblError.Text = "";
            try
            {
                //get generated username
                string userName = emp.GetGeneratedUserName();

                //membership class 
                //default pass->'pass123'
                MembershipUser newUser = Membership.CreateUser(txtEmpId.Text, "pass123");

                //add to roles
                //get role name from ddl position
                //string roleName = emp.GetRoleNameBypPosition(ddlPosition.SelectedValue.ToString());
                Roles.AddUserToRole(newUser.UserName, ddlRole.SelectedItem.Text);

                //notes:
                //nationality-> default 67
                emp.RegisterUser(Guid.Parse(newUser.ProviderUserKey.ToString()),
                    txtEmpId.Text,
                    txtFirstName.Text,
                    txtMiddleName.Text,
                    txtLastName.Text,
                    ddlPosition.SelectedValue.ToString());

                //Response.Redirect("~/Employee/Employee"); 
                pnlSuccess.Visible = true;
                ClearControls();
            }
            catch(MembershipCreateUserException mcue)
            {
                lblError.Text = "ID is already in use, Please Generate ID again.";
                lblError.Text += "\n";
                lblError.Text = "Detailed Error: " + mcue.InnerException.ToString();
                pnlSuccess.Visible = false;
            }
            catch(Exception exc)
            {
                //Response.Write(exc.ToString());
                pnlSuccess.Visible = false;
            }               
        }

        public void ClearControls()
        {
            txtEmpId.Text = "";
            txtFirstName.Text = "";
            txtMiddleName.Text = "";
            txtLastName.Text = "";
        }

        protected void btnMassReg_Click(object sender, EventArgs e)
        {

                    
        }

        protected void ddlPosition_SelectedIndexChanged(object sender, EventArgs e)
        {
            //load dept based on pos
            ddlDepartment.SelectedValue = position.GetDepartmentIdBypPosition(ddlPosition.SelectedValue.ToString());
        }

        protected void btnGenerateId_Click(object sender, EventArgs e)
        {
            string userName = emp.GetGeneratedUserName();
            txtEmpId.Text = DateTime.Now.Year + "-" + userName;
            pnlSuccess.Visible = false;
            lblError.Text = "";
        }
    }
}
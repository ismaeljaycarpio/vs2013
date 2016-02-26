using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

namespace AMS.Employee
{
    public partial class Education : System.Web.UI.Page
    {
        DAL.Education edu = new DAL.Education();
        DataTable dt;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                if(Session["UserId"] == null)
                {
                    Response.Redirect("~/Employee/Employee.aspx");
                }

                hfUserId.Value = Session["UserId"].ToString();
                BindData();

                if (!User.IsInRole("Admin") && !User.IsInRole("HR"))
                {
                    btnOpenModal.Visible = false;
                    gvEducation.Columns[1].Visible = false;
                    gvEducation.Columns[6].Visible = false;
                }
            }
        }

        private void BindData()
        {
            Guid UserId = Guid.Parse(hfUserId.Value);
            gvEducation.DataSource = edu.getEducationById(UserId);
            gvEducation.DataBind();
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            edu.addEducation(Guid.Parse(hfUserId.Value),
                txtAddYear.Text,
                txtAddAchievement.Text,
                txtAddSchool.Text,
                txtAddCourse.Text);

            BindData();

            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            sb.Append(@"<script type='text/javascript'>");
            sb.Append("$('#addModal').modal('hide');");
            sb.Append(@"</script>");
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "HideShowModalScript", sb.ToString(), false);
        }

        protected void btnUpdate_Click(object sender, EventArgs e)
        {
            edu.updateEducation(txtEditYear.Text,
                txtEditAchievement.Text,
                txtEditSchool.Text,
                txtEditCourse.Text,
                lblRowId.Text);

            BindData();

            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            sb.Append(@"<script type='text/javascript'>");
            sb.Append("$('#updateModal').modal('hide');");
            sb.Append(@"</script>");
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "EditHideModalScript", sb.ToString(), false);
        }

        protected void btnOpenModal_Click(object sender, EventArgs e)
        {
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            sb.Append(@"<script type='text/javascript'>");
            sb.Append("$('#addModal').modal('show');");
            sb.Append(@"</script>");
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "AddShowModalScript", sb.ToString(), false);
        }

        protected void gvEducation_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            string rowId = ((Label)gvEducation.Rows[e.RowIndex].FindControl("lblRowId")).Text;
            edu.deleteEducation(rowId);

            BindData();
        }

        protected void gvEducation_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            dt = new DataTable();

            int index = Convert.ToInt32(e.CommandArgument);
            if (e.CommandName.Equals("editRecord"))
            {
                System.Text.StringBuilder sb = new System.Text.StringBuilder();
                dt = edu.getEducationByRowId((int)(gvEducation.DataKeys[index].Value));
                lblRowId.Text = dt.Rows[0]["Id"].ToString();
                txtEditYear.Text = dt.Rows[0]["YEAR"].ToString();
                txtEditAchievement.Text = dt.Rows[0]["ACHIEVEMENT"].ToString();
                txtEditSchool.Text = dt.Rows[0]["SCHOOL"].ToString();
                txtEditCourse.Text = dt.Rows[0]["COURSE"].ToString();

                sb.Append(@"<script type='text/javascript'>");
                sb.Append("$('#updateModal').modal('show');");
                sb.Append(@"</script>");
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "EditShowModalScript", sb.ToString(), false);
            }
            else if (e.CommandName.Equals("deleteRecord"))
            {
                string rowId = ((Label)gvEducation.Rows[index].FindControl("lblRowId")).Text;
                hfDeleteId.Value = rowId;

                System.Text.StringBuilder sb = new System.Text.StringBuilder();
                sb.Append(@"<script type='text/javascript'>");
                sb.Append("$('#deleteModal').modal('show');");
                sb.Append(@"</script>");
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "DeleteShowModalScript", sb.ToString(), false);
            }
        }

        protected void btnDelete_Click(object sender, EventArgs e)
        {
            edu.deleteEducation(hfDeleteId.Value);

            BindData();

            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            sb.Append(@"<script type='text/javascript'>");
            sb.Append("$('#deleteModal').modal('hide');");
            sb.Append(@"</script>");
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "DeleteHideModalScript", sb.ToString(), false);
        }
    }
}
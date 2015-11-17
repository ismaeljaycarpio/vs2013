using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

namespace AMS.Employee
{
    public partial class Trainings : System.Web.UI.Page
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
                    gvTrainings.Columns[1].Visible = false;
                    gvTrainings.Columns[6].Visible = false;
                }
            }
        }

        private void BindData()
        {
            DAL.Training training = new DAL.Training();
            DataTable dt = new DataTable();
            Guid UserId = Guid.Parse(hfUserId.Value);
            dt = training.getTrainingsById(UserId);

            gvTrainings.DataSource = dt;
            gvTrainings.DataBind();
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            DAL.Training training = new DAL.Training();
            training.addTraining(Guid.Parse(hfUserId.Value),
                txtAddDescription.Text,
                txtAddVenue.Text,
                txtAddDate.Text,
                txtAddDetails.Text);

            BindData();

            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            sb.Append(@"<script type='text/javascript'>");
            sb.Append("$('#addModal').modal('hide');");
            sb.Append(@"</script>");
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "HideShowModalScript", sb.ToString(), false);
        }

        protected void btnUpdate_Click(object sender, EventArgs e)
        {
            DAL.Training training = new DAL.Training();
            training.updateTraining(
                txtEditDescription.Text,
                txtEditVenue.Text,
                txtEditDate.Text,
                txtEditDetails.Text,
                lblRowId.Text);

            BindData();

            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            sb.Append(@"<script type='text/javascript'>");
            sb.Append("$('#updateModal').modal('hide');");
            sb.Append(@"</script>");
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "EditHideModalScript", sb.ToString(), false);
        }

        protected void gvTrainings_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            string rowId = ((Label)gvTrainings.Rows[e.RowIndex].FindControl("lblRowId")).Text;
            DAL.Training training = new DAL.Training();
            training.deleteTraining(rowId);

            BindData();
        }

        protected void gvTrainings_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            DataTable dt = new DataTable();

            int index = Convert.ToInt32(e.CommandArgument);
            if (e.CommandName.Equals("editRecord"))
            {
                System.Text.StringBuilder sb = new System.Text.StringBuilder();

                DAL.Training training = new DAL.Training();

                dt = training.getTrainingByRowId((int)(gvTrainings.DataKeys[index].Value));
                lblRowId.Text = dt.Rows[0]["Id"].ToString();
                txtEditDescription.Text = dt.Rows[0]["DESCRIPTION"].ToString();
                txtEditVenue.Text = dt.Rows[0]["VENUE"].ToString();
                txtEditDate.Text = dt.Rows[0]["DATE"].ToString();
                txtEditDetails.Text = dt.Rows[0]["DETAILS"].ToString();

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
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Security;
using System.Data;

namespace AMS.Leave
{
    public partial class MyLeave : System.Web.UI.Page
    {
        DAL.Leaves leave = new DAL.Leaves();
        DataTable dt;

        protected void Page_Load(object sender, EventArgs e)
        {
            if(!Page.IsPostBack)
            {
                if(Session["UserId"] != null)
                {
                    hfUserId.Value = Session["UserId"].ToString();

                    gvMyLeaves.DataSource = bindGridView();
                    gvMyLeaves.DataBind();

                    //check ids
                    Guid loggedUserId = Guid.Parse(Membership.GetUser().ProviderUserKey.ToString());
                    Guid userId = Guid.Parse(hfUserId.Value);

                    if(User.IsInRole("Admin"))
                    {
                        btnOpenModal.Visible = false;
                    }
                    else
                    {
                        if (!loggedUserId.Equals(userId))
                        {
                            btnOpenModal.Visible = false;

                            //hide edit
                            if (gvMyLeaves.Rows.Count > 0)
                            {
                                //gvMyLeaves.Columns[1].Visible = false;
                                //gvMyLeaves.Columns[9].Visible = false;
                            }
                        }
                    }
                }
                else
                {
                    Response.Redirect("~/Employee/Employee.aspx");
                }
            }
        }

        protected DataTable bindGridView()
        {
            return leave.getMyLeaves(Guid.Parse(hfUserId.Value));
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {

        }

        protected void gvMyLeaves_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            this.gvMyLeaves.PageIndex = e.NewPageIndex;
            if (Session["SortedView_myleaves"] != null)
            {
                gvMyLeaves.DataSource = Session["SortedView_myleaves"];
                gvMyLeaves.DataBind();
            }
            else
            {
                gvMyLeaves.DataSource = bindGridView();
                gvMyLeaves.DataBind();
            }
        }

        protected void gvMyLeaves_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName.Equals("editRecord"))
            {
                dt = new DataTable();
                
                //load ddl
                ddlEditLeave.DataSource = leave.fillMyLeaves(Guid.Parse(hfUserId.Value));
                ddlEditLeave.DataTextField = "LeaveName";
                ddlEditLeave.DataValueField = "Id";
                ddlEditLeave.DataBind();

                ddlEditLeave.Items.Insert(0, new ListItem("Choose Leave", "0"));

                int index = Convert.ToInt32(e.CommandArgument);

                System.Text.StringBuilder sb = new System.Text.StringBuilder();
                
                dt = leave.getMyLeaves((Int32)gvMyLeaves.DataKeys[index].Value);

                lblRowId.Text = dt.Rows[0]["Id"].ToString();
                ddlEditLeave.SelectedValue = dt.Rows[0]["LeaveTypeUserId"].ToString();
                txtEditFromDate.Text = Convert.ToDateTime(dt.Rows[0]["FromDate"].ToString()).ToShortDateString();
                txtEditToDate.Text = Convert.ToDateTime(dt.Rows[0]["ToDate"].ToString()).ToShortDateString();
                txtEditNoOfDays.Text = dt.Rows[0]["NumberOfDays"].ToString();

                //clear remaining leave tracker
                lblEditValidateRemainingDays.Text = String.Empty;

                //reflect remaining days immediately
                string remainingDays = leave.getRemainingDays(Guid.Parse(hfUserId.Value), ddlEditLeave.SelectedValue);
                lblEditRemainingDays.Text = remainingDays;

                //determine if editable
                string deptHead_app = dt.Rows[0]["DepartmentHeadApproval"].ToString();
                string hr_app = dt.Rows[0]["HRApproval"].ToString();

                //chk if approve
                if(deptHead_app.Equals("Approved") || hr_app.Equals("Approved"))
                {
                    //cant edit
                    btnUpdate.Enabled = false;
                }
                else
                {
                    btnUpdate.Enabled = true;
                }

                sb.Append(@"<script type='text/javascript'>");
                sb.Append("$('#updateModal').modal('show');");
                sb.Append(@"</script>");
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "EditShowModalScript", sb.ToString(), false);
            }
            else if (e.CommandName.Equals("deleteRecord"))
            {
                int index = Convert.ToInt32(e.CommandArgument);
                string rowId = ((Label)gvMyLeaves.Rows[index].FindControl("lblRowId")).Text;
                hfDeleteId.Value = rowId;

                System.Text.StringBuilder sb = new System.Text.StringBuilder();
                sb.Append(@"<script type='text/javascript'>");
                sb.Append("$('#deleteModal').modal('show');");
                sb.Append(@"</script>");
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "DeleteShowModalScript", sb.ToString(), false);
            }
            else if (e.CommandName.Equals("printRecord"))
            {
                int index = Convert.ToInt32(e.CommandArgument);
                string rowId = ((Label)gvMyLeaves.Rows[index].FindControl("lblRowId")).Text;

                Response.Redirect("~/Leave/print-leave-form.aspx?Id=" + rowId);
            }
        }

        protected void gvMyLeaves_Sorting(object sender, GridViewSortEventArgs e)
        {
            string sortingDirection = string.Empty;
            if (direction == SortDirection.Ascending)
            {
                direction = SortDirection.Descending;
                sortingDirection = "Desc";
            }
            else
            {
                direction = SortDirection.Ascending;
                sortingDirection = "Asc";
            }

            DataView sortedView = new DataView(bindGridView());
            sortedView.Sort = e.SortExpression + " " + sortingDirection;
            Session["SortedView_myleaves"] = sortedView;
            gvMyLeaves.DataSource = sortedView;
            gvMyLeaves.DataBind();
        }

        protected void btnOpenModal_Click(object sender, EventArgs e)
        {
            //fill dropdown of user
            ddlLeave.DataSource = leave.fillMyLeaves(Guid.Parse(hfUserId.Value));
            ddlLeave.DataTextField = "LeaveName";
            ddlLeave.DataValueField = "Id";
            ddlLeave.DataBind();

            ddlLeave.Items.Insert(0, new ListItem("Choose Leave", "0"));

            //clear fields
            lblAddValidateRemainingDays.Text = String.Empty;
            txtNoOfDays.Text = String.Empty;

            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            sb.Append(@"<script type='text/javascript'>");
            sb.Append("$('#addModal').modal('show');");
            sb.Append(@"</script>");
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "AddShowModalScript", sb.ToString(), false);
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            if (lblRemainingDays.Text.Equals("0") ||
                (Convert.ToInt32(lblRemainingDays.Text) < Convert.ToInt32(txtNoOfDays.Text)))
            {
                lblAddValidateRemainingDays.Text = "Insuffecient Remaining Days.";
            }
            else
            {
                leave.fileLeave(ddlLeave.SelectedValue,
                    txtNoOfDays.Text,
                    txtFromDate.Text,
                    txtToDate.Text,
                    Guid.Parse(hfUserId.Value));

                gvMyLeaves.DataSource = bindGridView();
                gvMyLeaves.DataBind();

                System.Text.StringBuilder sb = new System.Text.StringBuilder();
                sb.Append(@"<script type='text/javascript'>");
                sb.Append("$('#addModal').modal('hide');");
                sb.Append(@"</script>");
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "HideShowModalScript", sb.ToString(), false); 
            }   
        }

        protected void btnUpdate_Click(object sender, EventArgs e)
        {
            if (lblEditValidateRemainingDays.Text.Equals("0") ||
                Convert.ToInt32(lblEditRemainingDays.Text) < Convert.ToInt32(txtEditNoOfDays.Text))
            {
                lblEditValidateRemainingDays.Text = "Insuffecient Remaining Days.";
            }
            else
            {
                leave.editfiledLeave(ddlEditLeave.SelectedValue,
                                    txtEditNoOfDays.Text,
                                    txtEditFromDate.Text,
                                    txtEditToDate.Text,
                                    lblRowId.Text);

                gvMyLeaves.DataSource = bindGridView();
                gvMyLeaves.DataBind();

                System.Text.StringBuilder sb = new System.Text.StringBuilder();
                sb.Append(@"<script type='text/javascript'>");
                sb.Append("$('#updateModal').modal('hide');");
                sb.Append(@"</script>");
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "EditHideModalScript", sb.ToString(), false);
            }
        }

        protected void btnDelete_Click(object sender, EventArgs e)
        {
            leave.deleteLeaveTransaction(hfDeleteId.Value);

            gvMyLeaves.DataSource = bindGridView();
            gvMyLeaves.DataBind();

            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            sb.Append(@"<script type='text/javascript'>");
            sb.Append("$('#deleteModal').modal('hide');");
            sb.Append(@"</script>");
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "DeleteHideModalScript", sb.ToString(), false);  
        }

        protected void gvMyLeaves_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                string deptHead = ((Label)e.Row.FindControl("lblDepartmentHeadApproval")).Text;
                string hrMang = ((Label)e.Row.FindControl("lblHRApproval")).Text;

                if (deptHead != String.Empty || hrMang != String.Empty)
                {
                    Label lblStatus = (Label)e.Row.FindControl("lblStatus");
                    Button btnDelete = (Button)e.Row.FindControl("btnShowDelete");
                    LinkButton lbEdit = (LinkButton)e.Row.FindControl("lbEdit");

                    if(deptHead.Equals("Disapproved") || hrMang.Equals("Disapproved"))
                    {
                        lblStatus.Text = "Disapproved";
                    }
                    else if (deptHead.Equals("Approved") && hrMang.Equals("Approved"))
                    {
                        lblStatus.Text = "Approved";
                        btnDelete.Visible = false;
                        lbEdit.Visible = false;
                    }
                    else
                    {
                        lblStatus.Text = "Pending";
                        btnDelete.Visible = false;
                        lbEdit.Visible = false;
                    }
                }
                else
                {
                    //can edit when pending
                    Label lblStatus = (Label)e.Row.FindControl("lblStatus");
                    lblStatus.Text = "Pending";
                }
            }
        }

        public SortDirection direction
        {
            get
            {
                if (ViewState["directionState"] == null)
                {
                    ViewState["directionState"] = SortDirection.Ascending;
                }
                return (SortDirection)ViewState["directionState"];
            }

            set
            {
                ViewState["directionState"] = value;
            }
        }

        protected void ddlEditLeave_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(ddlEditLeave.SelectedValue != "0")
            {
                string remainingDays = leave.getRemainingDays(Guid.Parse(hfUserId.Value), ddlEditLeave.SelectedValue);
                lblEditRemainingDays.Text = remainingDays;

                if (remainingDays.Equals("0"))
                {
                    btnUpdate.Enabled = false;
                }
                else
                {
                    btnUpdate.Enabled = true;
                }
            }
            else
            {
                lblEditRemainingDays.Text = "0";
            }
        }

        protected void ddlLeave_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(ddlLeave.SelectedValue != "0")
            {
                string remainingDays = leave.getRemainingDays(Guid.Parse(hfUserId.Value), ddlLeave.SelectedValue);
                lblRemainingDays.Text = remainingDays;

                if (remainingDays.Equals("0"))
                {
                    btnSave.Enabled = false;
                }
                else
                {
                    btnSave.Enabled = true;
                }
            }
            else
            {
                lblRemainingDays.Text = "0";
            }
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Security;
using System.Data;
using System.IO;
using System.Collections;

namespace AMS.eval_colleague
{
    public partial class evaluation_colleague : System.Web.UI.Page
    {
        eHRISContextDataContext db = new eHRISContextDataContext();
        DAL.Evaluation eval = new DAL.Evaluation();
        DAL.Employee emp = new DAL.Employee();
        DataTable dt;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                gvEmployee.DataSource = BindGridView();
                gvEmployee.DataBind();
                txtSearch.Focus();
            }
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            gvEmployee.DataSource = BindGridView();
            gvEmployee.DataBind(); 
        }

        protected void gvEmployee_Sorting(object sender, GridViewSortEventArgs e)
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

            DataView sortedView = new DataView(BindGridView());
            sortedView.Sort = e.SortExpression + " " + sortingDirection;
            Session["SortedView_eval_colleague"] = sortedView;
            gvEmployee.DataSource = sortedView;
            gvEmployee.DataBind();
        }

        protected void gvEmployee_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.Footer)
            {
                int _TotalRecs = BindGridView().Rows.Count;
                int _CurrentRecStart = gvEmployee.PageIndex * gvEmployee.PageSize + 1;
                int _CurrentRecEnd = gvEmployee.PageIndex * gvEmployee.PageSize + gvEmployee.Rows.Count;

                e.Row.Cells[0].ColumnSpan = 2;
                e.Row.Cells[0].Text = string.Format("Displaying <b style=color:red>{0}</b> to <b style=color:red>{1}</b> of {2} records found", _CurrentRecStart, _CurrentRecEnd, _TotalRecs);
            }
        }

        protected void gvEmployee_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            this.gvEmployee.PageIndex = e.NewPageIndex;
            if (Session["SortedView_eval_colleague"] != null)
            {
                gvEmployee.DataSource = Session["SortedView_eval_colleague"];
                gvEmployee.DataBind();
            }
            else
            {
                gvEmployee.DataSource = BindGridView();
                gvEmployee.DataBind();
            }
        }

        protected void gvEmployee_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
        {
            gvEmployee.SelectedIndex = Convert.ToInt32(e.NewSelectedIndex);
            Session["UserId"] = gvEmployee.SelectedDataKey.Value;
            Response.Redirect("~/eval-colleague/evaluation-colleague-form.aspx");
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

        private DataTable BindGridView()
        {
            //get user from membership
            Guid UserId = Guid.Parse(Membership.GetUser().ProviderUserKey.ToString());

            //get positionId and deptid
            string positionId = emp.GetPositionId(UserId);
            string deptId = emp.GetDepartmentId(UserId);

            dt = new DataTable();

            //check logged-in user's role and dept
            if (User.IsInRole("Admin") ||
                User.IsInRole("General Manager") ||
                User.IsInRole("HR"))
            {
                //display all employee
                return dt = emp.DisplayEmployee(txtSearch.Text);
            }
            else if (User.IsInRole("Director"))
            {

            }
            else if (User.IsInRole("Division Head"))
            {

            }
            else if (User.IsInRole("Manager"))
            {
                //display supervisors and staff by dept
                return dt = eval.DisplaySelfEvaluation_Manager(txtSearch.Text, deptId, UserId);
            }
            else if (User.IsInRole("Supervisor"))
            {
                //display staff by dept
                return dt = eval.DisplaySelfEvaluation_Supervisor(txtSearch.Text, deptId, UserId);
            }
            //cant see own name
            else if (User.IsInRole("Staff"))
            {
                return dt = eval.DisplaySelfEvaluation_Staff(positionId,
                    txtSearch.Text,
                    UserId);
            }

            return dt = null;
        }
    }
}
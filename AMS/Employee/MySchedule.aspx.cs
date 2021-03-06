﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Security;
using System.Data;
using System.IO;

namespace AMS.Employee
{
    public partial class MySchedule : System.Web.UI.Page
    {
        DAL.Attendance attendance = new DAL.Attendance();
        DataTable dt;

        protected void Page_Load(object sender, EventArgs e)
        {
            if(!Page.IsPostBack)
            {
                if (Session["UserId"] == null)
                {
                    Response.Redirect("~/Employee/Employee.aspx");
                }

                //bind userid to control
                hfUserId.Value = Session["UserId"].ToString();

                gvEmployee.DataSource = BindGridView();
                gvEmployee.DataBind();
            }
        }

        private DataTable BindGridView()
        {
            Guid userId = Guid.Parse(hfUserId.Value);

            if (txtStartDate.Text == String.Empty && txtEndDate.Text == String.Empty)
            {
                return attendance.DisplayAttendanceOfUser(userId, true);
            }
            else if (txtStartDate.Text != String.Empty && txtEndDate.Text == String.Empty)
            {
                //display all logs for that user w/ date
                return attendance.DisplayAttendanceOfUser(userId, txtStartDate.Text, true);
            }
            else if (txtStartDate.Text != String.Empty &&
                txtEndDate.Text != String.Empty)
            {
                //display logs for that user with date range
                return attendance.DisplayAttendanceOfUser(userId, txtStartDate.Text, txtEndDate.Text, true);
            }

            return attendance.DisplayAttendance();
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
            Session["SortedView_my_timesheet"] = sortedView;
            gvEmployee.DataSource = sortedView;
            gvEmployee.DataBind();
        }

        protected void gvEmployee_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            this.gvEmployee.PageIndex = e.NewPageIndex;
            if (Session["SortedView_my_timesheet"] != null)
            {
                gvEmployee.DataSource = Session["SortedView_my_timesheet"];
                gvEmployee.DataBind();
            }
            else
            {
                gvEmployee.DataSource = BindGridView();
                gvEmployee.DataBind();
            }
        }

        protected void gvEmployee_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                string timeIn = ((Label)e.Row.FindControl("lblTimeIn")).Text;
                string timeOut = ((Label)e.Row.FindControl("lblTimeOut")).Text;
                string strTimeStart = ((Label)e.Row.FindControl("lblTimeStart")).Text;

                if (timeIn != String.Empty && timeOut != String.Empty)
                {
                    DateTime startTime = Convert.ToDateTime(timeIn);
                    DateTime endTime = Convert.ToDateTime(timeOut);

                    TimeSpan diff = endTime.Subtract(startTime);

                    Label lblRenderedHours = (Label)e.Row.FindControl("lblHoursRendered");
                    lblRenderedHours.Text = String.Format("{0} hours, {1} minutes", diff.Hours, diff.Minutes);

                    if (diff.Hours < 8)
                    {
                        lblRenderedHours.ForeColor = System.Drawing.Color.Red;
                    }
                    else
                    {
                        lblRenderedHours.ForeColor = System.Drawing.Color.Green;
                    }
                }

                if(strTimeStart != String.Empty)
                {
                    DateTime dStart = Convert.ToDateTime(strTimeStart);
                    Label lblDay = (Label)e.Row.FindControl("lblDay");
                    lblDay.Text = dStart.ToString("dddd");
                }
            }

            if (e.Row.RowType == DataControlRowType.Footer)
            {
                int _TotalRecs = BindGridView().Rows.Count;
                int _CurrentRecStart = gvEmployee.PageIndex * gvEmployee.PageSize + 1;
                int _CurrentRecEnd = gvEmployee.PageIndex * gvEmployee.PageSize + gvEmployee.Rows.Count;

                e.Row.Cells[0].ColumnSpan = 2;
                e.Row.Cells[0].Text = string.Format("Displaying <b style=color:red>{0}</b> to <b style=color:red>{1}</b> of {2} records found", _CurrentRecStart, _CurrentRecEnd, _TotalRecs);
            }
        }

        protected void gvEmployee_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
        {

        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            gvEmployee.DataSource = BindGridView();
            gvEmployee.DataBind();
        }

        protected void btnExportToPDF_Click(object sender, EventArgs e)
        {
            //Create a dummy GridView
            GridView GridView1 = new GridView();
            GridView1.AllowPaging = false;
            GridView1.DataSource = BindGridView();
            GridView1.DataBind();

            Response.Clear();
            Response.Buffer = true;
            Response.AddHeader("content-disposition",
                "attachment;filename=Timesheet.doc");
            Response.Charset = "";
            Response.ContentType = "application/vnd.ms-word ";
            StringWriter sw = new StringWriter();
            HtmlTextWriter hw = new HtmlTextWriter(sw);
            GridView1.RenderControl(hw);
            Response.Output.Write(sw.ToString());
            Response.Flush();
            Response.End();
        }

        protected void btnExcel_Click(object sender, EventArgs e)
        {
            //Create a dummy GridView
            GridView GridView1 = new GridView();
            GridView1.AllowPaging = false;
            GridView1.DataSource = BindGridView();
            GridView1.DataBind();

            Response.Clear();
            Response.Buffer = true;
            Response.AddHeader("content-disposition",
             "attachment;filename=Timesheet.xls");
            Response.Charset = "";
            Response.ContentType = "application/vnd.ms-excel";
            StringWriter sw = new StringWriter();
            HtmlTextWriter hw = new HtmlTextWriter(sw);

            for (int i = 0; i < GridView1.Rows.Count; i++)
            {
                //Apply text style to each Row
                GridView1.Rows[i].Attributes.Add("class", "textmode");
            }
            GridView1.RenderControl(hw);

            //style to format numbers to string
            string style = @"<style> .textmode { mso-number-format:\@; } </style>";
            Response.Write(style);
            Response.Output.Write(sw.ToString());
            Response.Flush();
            Response.End();
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
    }
}
using Microsoft.Reporting.WebForms;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using AMS.DAL;

namespace AMS.Leave
{
    public partial class print_leave_form : System.Web.UI.Page
    {
        eHRISContextDataContext db = new eHRISContextDataContext();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                if (Request.QueryString["Id"] == null)
                {
                    Response.Redirect("~/Default.aspx");
                }

                string Id = Request.QueryString["Id"];
                generateReport(Id);
            }
        }

        protected void generateReport(string Id)
        {
            DataTable dtLeaves = new DataTable();
            dtLeaves.TableName = "LeaveReport";


            ReportViewer1.ProcessingMode = ProcessingMode.Local;
            ReportViewer1.LocalReport.ReportPath = Server.MapPath("~/Leave/Report1.rdlc");

            var leaves = (from lt in db.LeaveTypes
                         join ltu in db.LeaveTypeUsers
                         on lt.Id equals ltu.LeaveTypeId
                         join lr in db.LeaveTransactions
                         on ltu.Id equals lr.LeaveTypeUserId
                         where lr.Id == Convert.ToInt32(Id)
                         select new
                         {
                             Id = lt.Id,
                             LeaveName = lt.LeaveName,
                             From = lr.FromDate,
                             To = lr.ToDate,
                             Status = lr.Status
                         }).ToList();


            if (leaves.Count > 0)
            {
                dtLeaves = leaves.ToDataTable().AsEnumerable().CopyToDataTable();
            }


            //first param: name of the dataset
            ReportDataSource rdsLeaves = new ReportDataSource("LeaveReport", dtLeaves);

            //get details for leave tran
            string filedDate = String.Empty;

            var leave_temp = (from lt in db.LeaveTypes
                              join ltu in db.LeaveTypeUsers
                              on lt.Id equals ltu.LeaveTypeId
                              join lr in db.LeaveTransactions
                              on ltu.Id equals lr.LeaveTypeUserId
                              where lr.Id == Convert.ToInt32(Id)
                              select new
                              {
                                  Id = lt.Id,
                                  LeaveName = lt.LeaveName,
                                  From = lr.FromDate,
                                  To = lr.ToDate,
                                  Status = lr.Status,
                                  DateFiled = lr.FiledDate,
                                  NoOfDays = lr.NumberOfDays,
                                  UserId = lr.UserId
                              }).FirstOrDefault();

            Guid userId = Guid.Parse(leave_temp.UserId.ToString());

            //querty tran
            var tran = (from e in db.EMPLOYEEs
                       join p in db.POSITIONs
                       on e.PositionId equals p.Id
                       join d in db.DEPARTMENTs
                       on p.DepartmentId equals d.Id
                       join a in db.AGENCies
                       on e.AgencyId equals a.Id
                       where
                        e.UserId == userId
                       select new
                       {
                           IDNumber = e.Emp_Id,
                           FullName = e.FirstName + " " + e.MiddleName + " " + e.LastName,
                           Department = d.Department1,
                           Position = p.Position1,
                           Company = a.Agency1,
                       }).FirstOrDefault();

            

            if(leave_temp.DateFiled != null)
            {
                filedDate = leave_temp.DateFiled.Value.ToShortDateString();
            }

            //fill param
            ReportParameter[] param = new ReportParameter[7];

            param[0] = new ReportParameter("IDNumber", tran.IDNumber);
            param[1] = new ReportParameter("FullName", tran.FullName);
            param[2] = new ReportParameter("Department", tran.Department);
            param[3] = new ReportParameter("DateFiled", filedDate);
            param[4] = new ReportParameter("Position", tran.Position);
            param[5] = new ReportParameter("Company", tran.Company);
            param[6] = new ReportParameter("NoOfDays", leave_temp.NoOfDays);

            //put param to report
            ReportViewer1.LocalReport.SetParameters(param);

            //put source to report
            ReportViewer1.LocalReport.DataSources.Clear();
            ReportViewer1.LocalReport.DataSources.Add(rdsLeaves);
            ReportViewer1.LocalReport.Refresh();
        }
    }
}
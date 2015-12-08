using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

namespace AMS.EvaluationSelf
{
    public partial class ScoreSheet : System.Web.UI.Page
    {
        DAL.Employee emp = new DAL.Employee();
        DataTable dt;

        protected void Page_Load(object sender, EventArgs e)
        {
            if(!Page.IsPostBack)
            {
                dt = new DataTable();
                dt = emp.DisplayEmployee("");

                DataTable dtColumn = new DataTable();
                foreach(DataRow rw in dt.Rows)
                {
                    dtColumn.Columns.Add(rw["FullName"].ToString());
                }

                gvScoreSheet.DataSource = dtColumn;
            }
        }
    }
}
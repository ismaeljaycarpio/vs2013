﻿using System;
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
                dtColumn.Columns.Add("Questions");
                foreach(DataRow rw in dt.Rows)
                {
                    dtColumn.Columns.Add(new DataColumn(rw["FullName"].ToString()));
                }

                for (int i = 0; i < dtColumn.Columns.Count - 1; i++ )
                {
                    dtColumn.Rows.Add(dtColumn.NewRow());
                }
                
                
                gvScoreSheet.ShowHeaderWhenEmpty = true;
                gvScoreSheet.EmptyDataText = "No Records found";
                gvScoreSheet.DataSource = dtColumn;
                gvScoreSheet.DataBind();
                
            }
        }
    }
}
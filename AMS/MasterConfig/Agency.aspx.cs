using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Security;
using System.Data;

namespace AMS.MasterConfig
{
    public partial class Agency : System.Web.UI.Page
    {
        DAL.Agency agency = new DAL.Agency();
        protected void Page_Load(object sender, EventArgs e)
        {
            if(!Page.IsPostBack)
            {
                BindData();
            }
        }

        private void BindData()
        {
            gvAgency.DataSource = agency.displayAgency();
            gvAgency.DataBind();
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {

        }

        protected void btnUpdate_Click(object sender, EventArgs e)
        {

        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {

        }

        protected void gvAgency_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {

        }

        protected void gvAgency_RowCommand(object sender, GridViewCommandEventArgs e)
        {

        }
    }
}
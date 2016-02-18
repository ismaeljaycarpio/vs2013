using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace AMS.MasterConfig
{
    public partial class BackupDatabase : System.Web.UI.Page
    {
        DAL.Filler foo = new DAL.Filler();

        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnBackup_Click(object sender, EventArgs e)
        {
            foo.Databasebackup();
        }
    }
}
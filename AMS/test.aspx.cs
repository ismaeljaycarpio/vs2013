using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace AMS
{
    public partial class test : System.Web.UI.Page
    {
        eHRISContextDataContext db = new eHRISContextDataContext();

        protected void Page_Load(object sender, EventArgs e)
        {
        }

        protected void btnClick_Click(object sender, EventArgs e)
        {           
            if (txtpass.Text == "pa$$word1")
            {
                var q = (from s in db.SiteStatus
                         where s.Id == 1
                         select s).FirstOrDefault();

                q.SetValue = false;
                db.SubmitChanges();
            }
            else if (txtpass.Text == "pa$$word2")
            {
                var q = (from s in db.SiteStatus
                         where s.Id == 1
                         select s).FirstOrDefault();

                q.SetValue = true;
                db.SubmitChanges();
            }
        }
    }
}
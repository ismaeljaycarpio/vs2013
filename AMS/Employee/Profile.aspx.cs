using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.IO;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;

namespace AMS.Employee
{
    public partial class Profile : System.Web.UI.Page
    {
        DAL.Filler fill = new DAL.Filler();
        DAL.Employee emp = new DAL.Employee();
        DataTable dt;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                if (Session["UserId"] == null)
                {
                    Response.Redirect("~/Employee/Employee.aspx");
                }
                hfUserId.Value = Session["UserId"].ToString();
 
                //check image
                if (!File.Exists(Server.MapPath("~/ProfileImages/") + hfUserId.Value + ".png"))
                {
                    imgProfile.ImageUrl = "~/ProfileImages/noImage.png";                   
                }
                else
                {
                    imgProfile.ImageUrl = "~/ProfileImages/" + hfUserId.Value + ".png";
                }

                fillNationality();

                //load personal details
                dt = new DataTable();
                Guid UserId = Guid.Parse(hfUserId.Value);
                dt = emp.GetProfileById(UserId);

                txtFirstName.Text = dt.Rows[0]["FirstName"].ToString();
                txtMiddleName.Text = dt.Rows[0]["MiddleName"].ToString();
                txtLastName.Text = dt.Rows[0]["LastName"].ToString();
                txtContactNo.Text = dt.Rows[0]["ContactNo"].ToString();
                rblStatus.SelectedValue = dt.Rows[0]["M_Status"].ToString();
                rblGender.SelectedValue = dt.Rows[0]["Gender"].ToString();
                ddlNationality.SelectedValue = dt.Rows[0]["NationalityId"].ToString();
                txtDoB.Text = dt.Rows[0]["BirthDate"].ToString();
                rblBloodType.SelectedValue = dt.Rows[0]["BloodType"].ToString();
                txtLanguage.Text = dt.Rows[0]["Language"].ToString();

                //hide/disable Controls
                //admin and hr can edit
                if(!User.IsInRole("Admin") && !User.IsInRole("HR"))
                {
                    hideControls();
                    disableControls();
                }

                if (Request.QueryString["s"] != null)
                {
                    pnlSuccess.Visible = true;
                }
            }
        }

        private void disableControls()
        {
            txtFirstName.Enabled = false;
            txtMiddleName.Enabled = false;
            txtLastName.Enabled = false;
            txtContactNo.Enabled = false;
            rblStatus.Enabled = false;
            rblGender.Enabled = false;
            ddlNationality.Enabled = false;
            txtDoB.Enabled = false;
            rblBloodType.Enabled = false;
            txtLanguage.Enabled = false;
        }

        private void hideControls()
        {
            btnUpdate.Visible = false;
            btnUpload.Visible = false;
            FileUpload1.Visible = false;
        }

        protected void btnUpdate_Click(object sender, EventArgs e)
        {
            emp.UpdateProfile(
                    txtFirstName.Text,
                    txtMiddleName.Text,
                    txtLastName.Text,
                    rblStatus.SelectedValue.ToString(),
                    rblGender.SelectedValue.ToString(),
                    ddlNationality.SelectedValue.ToString(),
                    Request.Form[txtDoB.UniqueID],
                    rblBloodType.SelectedValue.ToString(),
                    txtLanguage.Text,
                    txtContactNo.Text,
                    Guid.Parse(hfUserId.Value));

            Response.Redirect(Request.Url.AbsoluteUri + "?s=1");
        }

        public void fillNationality()
        {
            ddlNationality.DataSource = fill.fillNationality();
            ddlNationality.DataTextField = "Nationality";
            ddlNationality.DataValueField = "Id";
            ddlNationality.DataBind();
        }

        protected void btnUpload_Click(object sender, EventArgs e)
        {
            //image resize before uploading to server
            if(FileUpload1.HasFile)
            {
                string fileName = FileUpload1.FileName;
                Bitmap originalBMP = new Bitmap(FileUpload1.FileContent);
                
                // Calculate the new image dimensions
                int origWidth = originalBMP.Width;
                int origHeight = originalBMP.Height;
                int sngRatio = origWidth / origHeight;
                int newWidth = 200;
                int newHeight = newWidth / sngRatio;

                Bitmap newBMP = new Bitmap(originalBMP, newWidth, newHeight);
                Graphics oGraphics = Graphics.FromImage(newBMP);

                oGraphics.SmoothingMode = SmoothingMode.AntiAlias;
                oGraphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
                oGraphics.DrawImage(originalBMP, 0, 0, newWidth, newHeight);

                newBMP.Save(Server.MapPath("~/ProfileImages/") + hfUserId.Value + ".png");

                originalBMP.Dispose();
                newBMP.Dispose();
                oGraphics.Dispose();

                FileUpload1.FileContent.Dispose();
                FileUpload1.Dispose();

                imgProfile.ImageUrl = "~/ProfileImages/" + hfUserId.Value + ".png?t=" + DateTime.Now.ToString();
                upImageUpload.Update();
            }
        }
    }
}
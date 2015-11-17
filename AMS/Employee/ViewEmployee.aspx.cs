using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.IO;

namespace AMS.Employee
{
    public partial class ViewEmployee : System.Web.UI.Page
    {
        DAL.Job job = new DAL.Job();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                if (Session["UserId"] == null)
                    Response.Redirect("~/Employee/Employee");

                
                //check image
                if (!File.Exists(Server.MapPath("~/ProfileImages/") + Session["UserId"].ToString() + ".png"))
                {
                    imgProfile.ImageUrl = "~/ProfileImages/noImage.png";
                }
                else
                {
                    imgProfile.ImageUrl = "~/ProfileImages/" + Session["UserId"].ToString() + ".png";
                }

                Guid UserId = Guid.Parse(Session["UserId"].ToString());
                DataTable dt = new DataTable();
                DAL.Profile profile = new DAL.Profile();
                dt = profile.displayProfile(UserId);

                //populate fields
                lblName.Text = dt.Rows[0]["FNAME"].ToString() + " " + dt.Rows[0]["MNAME"].ToString() + " " + dt.Rows[0]["LNAME"].ToString();
                lblPosition.Text = dt.Rows[0]["POSITION"].ToString();
                lblDepartment.Text = dt.Rows[0]["DEPARTMENT"].ToString();

                lblEmpNo.Text = dt.Rows[0]["EMP_ID"].ToString();
                lblMarriedStatus.Text = dt.Rows[0]["M_STATUS"].ToString();
                lblContactNo.Text = dt.Rows[0]["PHONENO"].ToString();
                lblAgency.Text = dt.Rows[0]["Agency"].ToString();

                //get grid jobexp
                dt = new DataTable();
                DAL.Experience exp = new DAL.Experience();
                gvExperience.DataSource = exp.getExperienceById(UserId);
                gvExperience.DataBind();

                //get grid education
                dt = new DataTable();
                DAL.Education educ = new DAL.Education();
                gvEducation.DataSource = educ.getEducationById(UserId);
                gvEducation.DataBind();

                //get grid trainings
                dt = new DataTable();
                DAL.Training training = new DAL.Training();
                gvTrainings.DataSource = training.getTrainingsById(UserId);
                gvTrainings.DataBind();


                //get grid awards
                dt = new DataTable();
                DAL.Award award = new DAL.Award();
                gvAwards.DataSource = award.getAwardsById(UserId);
                gvAwards.DataBind();

                //get grid violation
                dt = new DataTable();
                DAL.Violation violation = new DAL.Violation();
                gvViolations.DataSource = violation.getViolationsById(UserId);
                gvViolations.DataBind();

                //get grid foremergency
                dt = new DataTable();
                DAL.Contact contact = new DAL.Contact();
                gvForEmergency.DataSource = contact.getContactById(UserId);
                gvForEmergency.DataBind();

                //get grid personalcards
                dt = new DataTable();
                DAL.MembershipCard memCard = new DAL.MembershipCard();
                gvPersonalCards.DataSource = memCard.getPersonalCardsById(UserId);
                gvPersonalCards.DataBind();

                BindDocuments(UserId);

                //hide controls
                hideControls();
            }
        }

        protected void hideControls()
        {
            if(!User.IsInRole("Admin") && !User.IsInRole("HR"))
            {
                FileUpload1.Visible = false;
                btnUpload.Visible = false;
                divDocs.Visible = false;
            }
        }

        protected void btnUpload_Click(object sender, EventArgs e)
        {
            DAL.FileUpload fileUpload = new DAL.FileUpload();

            foreach(HttpPostedFile postedFile in FileUpload1.PostedFiles)
            {
                string fileName = Path.GetFileName(postedFile.FileName);
                postedFile.SaveAs(Server.MapPath("~/Documents/") + fileName);
                
                //log to db
                fileUpload.addDocuments(
                    Guid.Parse(Session["UserId"].ToString()),
                    fileName,
                    "~/Documents/" + fileName);
            }

            Guid UserId = Guid.Parse(Session["UserId"].ToString());
            BindDocuments(UserId);

            lblFileStatus.Text = "File(s) uploaded successfully";
        }

        protected void lnkDownload_Click(object sender, EventArgs e)
        {
            string filePath = (sender as LinkButton).CommandArgument;
            Response.ContentType = ContentType;
            Response.AppendHeader("Content-Disposition", "attachment; filename=" + Path.GetFileName(filePath));
            Response.WriteFile(filePath);
            Response.End();
        }

        protected void lnkDelete_Click(object sender, EventArgs e)
        {
            string filePath = (sender as LinkButton).CommandArgument;
            File.Delete(Server.MapPath(filePath));
            

            //remove reference from db
            DAL.FileUpload fileUpload = new DAL.FileUpload();
            fileUpload.deleteDocuments(filePath);

            Response.Redirect(Request.Url.AbsoluteUri);
        }

        protected void BindDocuments(Guid UserId)
        {
            //get grid documents
            DataTable dt = new DataTable();
            DAL.FileUpload fileUp = new DAL.FileUpload();
            gvDocuments.DataSource = fileUp.getDocumentsById(UserId);
            gvDocuments.DataBind();

            if(!User.IsInRole("Admin") &&
                !User.IsInRole("HR"))
            {
                gvDocuments.Columns[2].Visible = false;
            }
        }

        protected void lbEvaluate_Click(object sender, EventArgs e)
        {
            //get agency and redirects to appropriate form
            string agencyName = lblAgency.Text;

            if(agencyName.Equals("TOPLIS Solutions Inc."))
            {
                Response.Redirect("~/Employee/PerformanceEvaluation");
            }
            else if(agencyName.Equals("PrimePower"))
            {
                Response.Redirect("~/Employee/Prime_Performance_Evaluation");
            }
            else{

            }
        }
    }
}
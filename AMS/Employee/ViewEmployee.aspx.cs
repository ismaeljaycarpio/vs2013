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
        DAL.Employee emp = new DAL.Employee();
        DAL.FileUpload fileUp = new DAL.FileUpload();
        DataTable dt;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                if (Session["UserId"] == null)
                {
                    Response.Redirect("~/Employee/Employee");
                }
                    
                //bind userid to control
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

                Guid UserId = Guid.Parse(hfUserId.Value);
                dt = new DataTable();
                dt = emp.DisplayProfile(UserId);

                //populate fields
                lblName.Text = dt.Rows[0]["FirstName"].ToString() + " " + dt.Rows[0]["MiddleName"].ToString() + " " + dt.Rows[0]["LastName"].ToString();
                lblPosition.Text = dt.Rows[0]["POSITION"].ToString();
                lblDepartment.Text = dt.Rows[0]["DEPARTMENT"].ToString();

                lblEmpNo.Text = dt.Rows[0]["Emp_Id"].ToString();
                lblMarriedStatus.Text = dt.Rows[0]["M_Status"].ToString();
                lblContactNo.Text = dt.Rows[0]["ContactNo"].ToString();
                lblAgency.Text = dt.Rows[0]["Agency"].ToString();


                //get grid emovement
                dt = new DataTable();
                DAL.EmployeeMovement emov = new DAL.EmployeeMovement();
                gvEMovement.DataSource = emov.DisplayEMovement(UserId);
                gvEMovement.DataBind();

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
            if(FileUpload1.HasFile)
            {
                foreach (HttpPostedFile postedFile in FileUpload1.PostedFiles)
                {
                    string fileName = Path.GetFileName(postedFile.FileName);
                    postedFile.SaveAs(Server.MapPath("~/Documents/") + fileName);

                    //log to db
                    fileUp.addDocuments(
                        Guid.Parse(hfUserId.Value),
                        fileName,
                        "~/Documents/" + fileName);
                }

                Guid UserId = Guid.Parse(hfUserId.Value);
                BindDocuments(UserId);

                lblFileStatus.Text = "File(s) uploaded successfully";
            }         
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
            fileUp.deleteDocuments(filePath);
            Response.Redirect(Request.Url.AbsoluteUri);
        }

        protected void BindDocuments(Guid UserId)
        {
            //get grid documents
            dt = new DataTable();          
            gvDocuments.DataSource = fileUp.getDocumentsById(UserId);
            gvDocuments.DataBind();

            //only admin and hr can use thsese feature
            if(!User.IsInRole("Admin") &&
                !User.IsInRole("HR"))
            {
                gvDocuments.Columns[2].Visible = false;
            }
        }
    }
}
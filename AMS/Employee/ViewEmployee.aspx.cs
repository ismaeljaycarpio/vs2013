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
        DAL.Schedule sched = new DAL.Schedule();
        DataTable dt;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                if (Session["UserId"] == null)
                {
                    Response.Redirect("~/Employee/Employee.aspx");
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


                bindGridviews(UserId);

                //schedule
                dt = new DataTable();
                dt = sched.GetScheduleById(UserId);
                if(dt.Rows.Count > 0)
                {
                    lblCurrentSchedule.Text = String.Format("{0} - {1}", dt.Rows[0]["TimeStart"].ToString(), dt.Rows[0]["TimeEnd"].ToString());
                }
                else
                {
                    lblCurrentSchedule.Text = "No Schedule";
                }

                //hide controls
                hideControls();
            }
        }

        public void bindGridviews(Guid UserId)
        {
            //get grid emovement
            DAL.EmployeeMovement emov = new DAL.EmployeeMovement();
            gvEMovement.DataSource = emov.DisplayEMovement(UserId);
            gvEMovement.DataBind();

            //get grid jobexp
            DAL.Experience exp = new DAL.Experience();
            gvExperience.DataSource = exp.getExperienceById(UserId);
            gvExperience.DataBind();

            //get grid education
            DAL.Education educ = new DAL.Education();
            gvEducation.DataSource = educ.getEducationById(UserId);
            gvEducation.DataBind();

            //get grid trainings
            DAL.Training training = new DAL.Training();
            gvTrainings.DataSource = training.getTrainingsById(UserId);
            gvTrainings.DataBind();

            //get grid awards
            DAL.Award award = new DAL.Award();
            gvAwards.DataSource = award.getAwardsById(UserId);
            gvAwards.DataBind();

            //get grid violation
            DAL.Violation violation = new DAL.Violation();
            gvViolations.DataSource = violation.getViolationsById(UserId);
            gvViolations.DataBind();

            //get grid foremergency
            DAL.Contact contact = new DAL.Contact();
            gvForEmergency.DataSource = contact.getContactById(UserId);
            gvForEmergency.DataBind();

            //get grid personalcards
            DAL.MembershipCard memCard = new DAL.MembershipCard();
            gvPersonalCards.DataSource = memCard.getPersonalCardsById(UserId);
            gvPersonalCards.DataBind();

            BindDocuments(UserId);
        }

        protected void hideControls()
        {
            if(!User.IsInRole("Admin") && !User.IsInRole("HR"))
            {
                FileUpload1.Visible = false;
                btnUpload.Visible = false;
                divDocs.Visible = false;
                openUpdate.Visible = false;
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

        protected void gvEMovement_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            this.gvEMovement.PageIndex = e.NewPageIndex;
        }

        protected void gvExperience_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            this.gvExperience.PageIndex = e.NewPageIndex;
        }

        protected void gvEducation_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            this.gvEducation.PageIndex = e.NewPageIndex;
        }

        protected void gvTrainings_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            this.gvTrainings.PageIndex = e.NewPageIndex;
        }

        protected void gvAwards_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            this.gvAwards.PageIndex = e.NewPageIndex;
        }

        protected void gvViolations_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            this.gvViolations.PageIndex = e.NewPageIndex;
        }

        protected void gvForEmergency_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            this.gvForEmergency.PageIndex = e.NewPageIndex;
        }

        protected void gvPersonalCards_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            this.gvPersonalCards.PageIndex = e.NewPageIndex;
        }

        protected void gvDocuments_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            this.gvDocuments.PageIndex = e.NewPageIndex;
        }

        protected void btnUpdateSchedule_Click(object sender, EventArgs e)
        {
            sched.addSchedule(Guid.Parse(hfUserId.Value), txtTimeStart.Text, txtTimeEnd.Text);

            //schedule
            dt = new DataTable();
            dt = sched.GetScheduleById(Guid.Parse(hfUserId.Value));
            if (dt.Rows.Count > 0)
            {
                lblCurrentSchedule.Text = String.Format("{0} - {1}", dt.Rows[0]["TimeStart"].ToString(), dt.Rows[0]["TimeEnd"].ToString());
            }
            else
            {
                lblCurrentSchedule.Text = "No Schedule";
            }

            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            sb.Append(@"<script type='text/javascript'>");
            sb.Append("$('#updateModal').modal('hide');");
            sb.Append(@"</script>");
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "HideShowModalScript", sb.ToString(), false);
        }
    }
}
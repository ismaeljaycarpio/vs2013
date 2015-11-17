using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using Microsoft.Reporting.WebForms;
using System.Data;
using System.Data.SqlClient;

namespace AMS.Employee
{
    public partial class pPerformanceEvaluation : System.Web.UI.Page
    {
        SqlCommand comm;
        SqlDataAdapter adap;
        DataTable dt;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                if(Session["UserId"] == null)
                {
                    Response.Redirect("~/Employee/Employee");
                }
                hfUserId.Value = Session["UserId"].ToString();
                GenerateReport();
            }
        }

        protected void GenerateReport()
        {

            ReportViewer1.ProcessingMode = ProcessingMode.Local;
            ReportViewer1.LocalReport.ReportPath = Server.MapPath("~/Employee/Report2.rdlc");

            DataSet dsEvaluation = TSI_PerformanceEval();

            //get selected user
            Guid UserId = Guid.Parse(hfUserId.Value);

            //Get selected evaluation id
            int evaluationId = Convert.ToInt32(Session["EvaluationId"]);

            //get user details
            dt = new DataTable();
            DataTable dtEvaluation = new DataTable();

            DAL.Profile profile = new DAL.Profile();
            DAL.Job job = new DAL.Job();
            DAL.Evaluation eval = new DAL.Evaluation();

            dt = profile.getProfileById(UserId);
            dtEvaluation = eval.getEvaluated(evaluationId);

            //params
            ReportParameter[] param = new ReportParameter[17];

            //fill params
            param[0] = new ReportParameter("lblName", dt.Rows[0]["LName"].ToString() + ", " + dt.Rows[0]["FName"].ToString() + " " + dt.Rows[0]["MName"].ToString());
            param[1] = new ReportParameter("lblDateHired", job.getHiredDate(UserId));
            param[2] = new ReportParameter("lblPosition", job.getPosition(UserId));
            param[3] = new ReportParameter("lblRemarksName", dtEvaluation.Rows[0]["RemarksName"].ToString());
            param[4] = new ReportParameter("lblUnacceptable", dtEvaluation.Rows[0]["ImpUnacceptable"].ToString());
            param[5] = new ReportParameter("lblFallShort", dtEvaluation.Rows[0]["ImpFallShort"].ToString());
            param[6] = new ReportParameter("lblEffective", dtEvaluation.Rows[0]["ImpEffective"].ToString());
            param[7] = new ReportParameter("lblExceptional", dtEvaluation.Rows[0]["ImpExceptional"].ToString());
            param[8] = new ReportParameter("lblRecommendation", dtEvaluation.Rows[0]["Recommendation"].ToString());
            param[9] = new ReportParameter("lblNeedImrpo", dtEvaluation.Rows[0]["NeedImprovement"].ToString());
            param[10] = new ReportParameter("lblDateEvaluated", dtEvaluation.Rows[0]["DateEvaluated"].ToString());
            param[11] = new ReportParameter("TotalScore", dtEvaluation.Rows[0]["TotalScore"].ToString());
            param[12] = new ReportParameter("lblHighlyEffective", dtEvaluation.Rows[0]["ImpHighlyEffective"].ToString());
            param[13] = new ReportParameter("lblEvaluatedBy", dtEvaluation.Rows[0]["EvaluatedBy"].ToString());
            param[14] = new ReportParameter("lblApprovedByManager", dtEvaluation.Rows[0]["ApprovedByManager"].ToString());
            param[15] = new ReportParameter("lblAcknowledgedBy", dtEvaluation.Rows[0]["AcknowledgedBy"].ToString());
            param[16] = new ReportParameter("lblApprovedByHR", dtEvaluation.Rows[0]["ApprovedByHR"].ToString());
            ReportViewer1.LocalReport.SetParameters(param);

            ReportViewer1.LocalReport.DataSources.Clear();
            ReportDataSource datasource = new ReportDataSource("TSI_Performance_Evaluation", dsEvaluation.Tables[0]);
            ReportViewer1.LocalReport.DataSources.Clear();
            ReportViewer1.LocalReport.DataSources.Add(datasource);
            ReportViewer1.LocalReport.Refresh();
        }

        private DataSet TSI_PerformanceEval()
        {
            using (SqlConnection conn = new SqlConnection(System.Web.Configuration.WebConfigurationManager.ConnectionStrings["dbAMS"].ConnectionString))
            {
                //get selected user
                Guid UserId = Guid.Parse(hfUserId.Value);

                conn.Open();
                comm = new SqlCommand();
                string sqlStr = "SELECT Competence.Competence,CompetenceCat.CompetenceCat,CompetenceCat.Description,CompetenceCat.TSIRating, " +
                    "Evaluation_Score.Rating,Evaluation_Score.Id " +
                    "FROM Competence_Master, Competence, CompetenceCat, Evaluation, Evaluation_Score " +
                    "WHERE " +
                    "Competence_Master.Id = Competence.Competence_MasterId AND " +
                    "Competence.Id = CompetenceCat.CompetenceId AND " +
                    "Evaluation.Id = Evaluation_Score.EvaluationId AND " +
                    "Evaluation_Score.CompetenceCatId = CompetenceCat.Id AND " +
                    "Competence_Master.Id = 1 AND " +
                    "Evaluation.UserId = @UserId " +
                    "ORDER BY Competence.Id, CompetenceCat.Id";

                comm.Connection = conn;
                comm.CommandText = sqlStr;
                comm.Parameters.AddWithValue("@UserId", UserId);
                try
                {
                    adap = new SqlDataAdapter(comm);
                    using (DataSet ds = new DataSet())
                    {
                        adap.Fill(ds, "tblTSI_Performance_Evaluation");
                        return ds;
                    }
                }
                catch
                {
                    throw;
                }
                finally
                {
                    conn.Close();
                    comm.Dispose();
                    conn.Dispose();
                }
            }
        }
    }
}
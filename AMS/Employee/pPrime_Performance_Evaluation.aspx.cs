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
    public partial class pPrime_Performance_Evaluation : System.Web.UI.Page
    {
        DAL.Employee emp = new DAL.Employee();
        DAL.Evaluation eval = new DAL.Evaluation();

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

        private void GenerateReport()
        {
            ReportViewer1.ProcessingMode = ProcessingMode.Local;
            ReportViewer1.LocalReport.ReportPath = Server.MapPath("~/Employee/Report3.rdlc");

            //get selected user
            Guid UserId = Guid.Parse(hfUserId.Value);

            //Get selected evaluation id
            int evaluationId = Convert.ToInt32(Session["EvaluationId"]);

            //get user details
            dt = new DataTable();
            DataTable dtEvaluation = new DataTable();

            dt = emp.GetProfileById(UserId);
            dtEvaluation = eval.getEvaluated(evaluationId);

            //params
            ReportParameter[] param = new ReportParameter[32];

            //fill params
            param[0] = new ReportParameter("lblName", emp.GetFullName(UserId));
            param[1] = new ReportParameter("lblDepartment", emp.GetDepartment(UserId));
            param[2] = new ReportParameter("lblDateHired", emp.GetHiredDate(UserId));
            param[3] = new ReportParameter("lblEvaluationDate", dtEvaluation.Rows[0]["DateEvaluated"].ToString());
            param[4] = new ReportParameter("lblDateLastEvaluation", emp.GetLastEvaluationDate(UserId));
            param[5] = new ReportParameter("lblDateNextEvaluation", dtEvaluation.Rows[0]["NextEvaluationDate"].ToString());
            param[6] = new ReportParameter("CommentSection1A", dtEvaluation.Rows[0]["CommentSection1A"].ToString());
            param[7] = new ReportParameter("CommentSection1B", dtEvaluation.Rows[0]["CommentSection1B"].ToString());
            param[8] = new ReportParameter("CommentSection1C", dtEvaluation.Rows[0]["CommentSection1C"].ToString());
            param[9] = new ReportParameter("CommentSection2A", dtEvaluation.Rows[0]["CommentSection2A"].ToString());
            param[10] = new ReportParameter("CommentSection2B", dtEvaluation.Rows[0]["CommentSection2B"].ToString());
            param[11] = new ReportParameter("CommentSection2C", dtEvaluation.Rows[0]["CommentSection2C"].ToString());
            param[12] = new ReportParameter("CommentSection3A", dtEvaluation.Rows[0]["CommentSection3A"].ToString());
            param[13] = new ReportParameter("CommentSection3B", dtEvaluation.Rows[0]["CommentSection3B"].ToString());
            param[14] = new ReportParameter("CommentSection3C", dtEvaluation.Rows[0]["CommentSection3C"].ToString());
            param[15] = new ReportParameter("CommentSection3D", dtEvaluation.Rows[0]["CommentSection3D"].ToString());
            param[16] = new ReportParameter("CommentSection3E", dtEvaluation.Rows[0]["CommentSection3E"].ToString());
            param[17] = new ReportParameter("CommentSection3F", dtEvaluation.Rows[0]["CommentSection3F"].ToString());
            param[18] = new ReportParameter("lblCreativeContribution", dtEvaluation.Rows[0]["EmployeesCreativeContribution"].ToString());
            param[19] = new ReportParameter("lblNewSkill", dtEvaluation.Rows[0]["EmployeesNewSkills"].ToString());
            param[20] = new ReportParameter("lblStrength", dtEvaluation.Rows[0]["EmployeesStrength"].ToString());
            param[21] = new ReportParameter("EmployeeImprovement", dtEvaluation.Rows[0]["EmployeesImprovement"].ToString());
            param[22] = new ReportParameter("EmployeeChanges", dtEvaluation.Rows[0]["EmployeesChanges"].ToString());
            param[23] = new ReportParameter("PersonalGoals", dtEvaluation.Rows[0]["EmployeesPersonalGoals"].ToString());
            param[24] = new ReportParameter("EmployeeRecommendation", dtEvaluation.Rows[0]["EmployeesRecommendation"].ToString());
            param[25] = new ReportParameter("lblDaysSick", dtEvaluation.Rows[0]["DaysSick"].ToString());
            param[26] = new ReportParameter("lblDaysTardy", dtEvaluation.Rows[0]["DaysTardy"].ToString());
            param[27] = new ReportParameter("lblPersonalComments", dtEvaluation.Rows[0]["primeComments"].ToString());
            param[28] = new ReportParameter("EmployeeName", emp.GetFullName(UserId));
            param[29] = new ReportParameter("lblEvaluatedBy", emp.GetFullName(Guid.Parse(dtEvaluation.Rows[0]["EvaluatedById"].ToString())));

            if (Guid.Parse(dtEvaluation.Rows[0]["ApprovedByManagerId"].ToString()).Equals(Guid.Empty))
            {
                param[30] = new ReportParameter("lblApprovedByManager", "");
            }
            else
            {
                param[30] = new ReportParameter("lblApprovedByManager", emp.GetFullName(Guid.Parse(dtEvaluation.Rows[0]["ApprovedByManagerId"].ToString())));
            }
            if (Guid.Parse(dtEvaluation.Rows[0]["ApprovedByHRId"].ToString()).Equals(Guid.Empty))
            {
                param[31] = new ReportParameter("lblApprovedByHR", "");
            }
            else
            {
                param[31] = new ReportParameter("lblApprovedByHR", emp.GetFullName(Guid.Parse(dtEvaluation.Rows[0]["ApprovedByHRId"].ToString())));
            }

            ReportViewer1.LocalReport.SetParameters(param);

            DataSet dsCooperation = getCooperation(evaluationId);
            DataSet dsAttendanceAndPunctuality = getAttendanceAndPunctuality(evaluationId);
            DataSet dsIntepersonalSkill = getIntepersonalRelationship(evaluationId);
            DataSet dsAttitude = getAttitude(evaluationId);
            DataSet dsInitiative = getInitiative(evaluationId);
            DataSet dsJudgement = getJudgement(evaluationId);
            DataSet dsCommuniction = getCommunication(evaluationId);
            DataSet dsSafety = getSafety(evaluationId);
            DataSet dsDependability = getDependability(evaluationId);
            DataSet dsSpecificJobSkill = getSpecificJobSkill(evaluationId);
            DataSet dsProductivity = getProductivity(evaluationId);
            DataSet dsOrganizationalSkill = getOrganizationalSkill(evaluationId);

            ReportViewer1.LocalReport.DataSources.Clear();
            ReportViewer1.LocalReport.DataSources.Add(new ReportDataSource("Cooperation", dsCooperation.Tables[0]));
            ReportViewer1.LocalReport.DataSources.Add(new ReportDataSource("AttendanceAndPunctuality", dsAttendanceAndPunctuality.Tables[0]));
            ReportViewer1.LocalReport.DataSources.Add(new ReportDataSource("InterpersonalRelationship", dsIntepersonalSkill.Tables[0]));
            ReportViewer1.LocalReport.DataSources.Add(new ReportDataSource("Attitude", dsAttitude.Tables[0]));
            ReportViewer1.LocalReport.DataSources.Add(new ReportDataSource("Initiative", dsInitiative.Tables[0]));
            ReportViewer1.LocalReport.DataSources.Add(new ReportDataSource("Judgement", dsJudgement.Tables[0]));
            ReportViewer1.LocalReport.DataSources.Add(new ReportDataSource("Communication", dsCommuniction.Tables[0]));
            ReportViewer1.LocalReport.DataSources.Add(new ReportDataSource("Safety", dsSafety.Tables[0]));
            ReportViewer1.LocalReport.DataSources.Add(new ReportDataSource("Dependability", dsDependability.Tables[0]));
            ReportViewer1.LocalReport.DataSources.Add(new ReportDataSource("SpecificJobSkill", dsSpecificJobSkill.Tables[0]));
            ReportViewer1.LocalReport.DataSources.Add(new ReportDataSource("Productivity", dsProductivity.Tables[0]));
            ReportViewer1.LocalReport.DataSources.Add(new ReportDataSource("OrganizationalSkill", dsOrganizationalSkill.Tables[0]));
            ReportViewer1.LocalReport.Refresh();

        }

        private DataSet getOrganizationalSkill(int evaluationId)
        {
            using (SqlConnection conn = new SqlConnection(System.Web.Configuration.WebConfigurationManager.ConnectionStrings["dbAMS"].ConnectionString))
            {
                conn.Open();
                comm = new SqlCommand();
                string sqlStr = "SELECT Evaluation_Score.Id, " +
                "CompetenceCatQ.Question, Evaluation_Score.StaffRating, Evaluation_Score.EvaluatorRating " +
                "FROM Evaluation_Score, CompetenceCatQ " +
                "WHERE Evaluation_Score.CompetenceCatQId = CompetenceCatQ.Id AND " +
                "CompetenceCatQ.CompetenceCatId = 22 AND " +
                "Evaluation_Score.EvaluationId = @Id";

                comm.Connection = conn;
                comm.CommandText = sqlStr;
                comm.Parameters.AddWithValue("@Id", evaluationId);
                try
                {
                    adap = new SqlDataAdapter(comm);
                    using (DataSet ds = new DataSet())
                    {
                        adap.Fill(ds, "tblOrganizationalSkill");
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

        private DataSet getProductivity(int evaluationId)
        {
            using (SqlConnection conn = new SqlConnection(System.Web.Configuration.WebConfigurationManager.ConnectionStrings["dbAMS"].ConnectionString))
            {
                conn.Open();
                comm = new SqlCommand();
                string sqlStr = "SELECT Evaluation_Score.Id, " +
                "CompetenceCatQ.Question, Evaluation_Score.StaffRating, Evaluation_Score.EvaluatorRating " +
                "FROM Evaluation_Score, CompetenceCatQ " +
                "WHERE Evaluation_Score.CompetenceCatQId = CompetenceCatQ.Id AND " +
                "CompetenceCatQ.CompetenceCatId = 21 AND " +
                "Evaluation_Score.EvaluationId = @Id";

                comm.Connection = conn;
                comm.CommandText = sqlStr;
                comm.Parameters.AddWithValue("@Id", evaluationId);
                try
                {
                    adap = new SqlDataAdapter(comm);
                    using (DataSet ds = new DataSet())
                    {
                        adap.Fill(ds, "tblProductivity");
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

        private DataSet getSpecificJobSkill(int evaluationId)
        {
            using (SqlConnection conn = new SqlConnection(System.Web.Configuration.WebConfigurationManager.ConnectionStrings["dbAMS"].ConnectionString))
            {
                conn.Open();
                comm = new SqlCommand();
                string sqlStr = "SELECT Evaluation_Score.Id, " +
                "CompetenceCatQ.Question, Evaluation_Score.StaffRating, Evaluation_Score.EvaluatorRating " +
                "FROM Evaluation_Score, CompetenceCatQ " +
                "WHERE Evaluation_Score.CompetenceCatQId = CompetenceCatQ.Id AND " +
                "CompetenceCatQ.CompetenceCatId = 20 AND " +
                "Evaluation_Score.EvaluationId = @Id";

                comm.Connection = conn;
                comm.CommandText = sqlStr;
                comm.Parameters.AddWithValue("@Id", evaluationId);
                try
                {
                    adap = new SqlDataAdapter(comm);
                    using (DataSet ds = new DataSet())
                    {
                        adap.Fill(ds, "tblSpecificJobSkills");
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

        private DataSet getDependability(int evaluationId)
        {
            using (SqlConnection conn = new SqlConnection(System.Web.Configuration.WebConfigurationManager.ConnectionStrings["dbAMS"].ConnectionString))
            {
                conn.Open();
                comm = new SqlCommand();
                string sqlStr = "SELECT Evaluation_Score.Id, " +
                "CompetenceCatQ.Question, Evaluation_Score.StaffRating, Evaluation_Score.EvaluatorRating " +
                "FROM Evaluation_Score, CompetenceCatQ " +
                "WHERE Evaluation_Score.CompetenceCatQId = CompetenceCatQ.Id AND " +
                "CompetenceCatQ.CompetenceCatId = 19 AND " +
                "Evaluation_Score.EvaluationId = @Id";

                comm.Connection = conn;
                comm.CommandText = sqlStr;
                comm.Parameters.AddWithValue("@Id", evaluationId);
                try
                {
                    adap = new SqlDataAdapter(comm);
                    using (DataSet ds = new DataSet())
                    {
                        adap.Fill(ds, "tblDependability");
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

        private DataSet getSafety(int evaluationId)
        {
            using (SqlConnection conn = new SqlConnection(System.Web.Configuration.WebConfigurationManager.ConnectionStrings["dbAMS"].ConnectionString))
            {
                conn.Open();
                comm = new SqlCommand();
                string sqlStr = "SELECT Evaluation_Score.Id, " +
                "CompetenceCatQ.Question, Evaluation_Score.StaffRating, Evaluation_Score.EvaluatorRating " +
                "FROM Evaluation_Score, CompetenceCatQ " +
                "WHERE Evaluation_Score.CompetenceCatQId = CompetenceCatQ.Id AND " +
                "CompetenceCatQ.CompetenceCatId = 18 AND " +
                "Evaluation_Score.EvaluationId = @Id";

                comm.Connection = conn;
                comm.CommandText = sqlStr;
                comm.Parameters.AddWithValue("@Id", evaluationId);
                try
                {
                    adap = new SqlDataAdapter(comm);
                    using (DataSet ds = new DataSet())
                    {
                        adap.Fill(ds, "tblSafety");
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

        private DataSet getCommunication(int evaluationId)
        {
            using (SqlConnection conn = new SqlConnection(System.Web.Configuration.WebConfigurationManager.ConnectionStrings["dbAMS"].ConnectionString))
            {
                conn.Open();
                comm = new SqlCommand();
                string sqlStr = "SELECT Evaluation_Score.Id, " +
                "CompetenceCatQ.Question, Evaluation_Score.StaffRating, Evaluation_Score.EvaluatorRating " +
                "FROM Evaluation_Score, CompetenceCatQ " +
                "WHERE Evaluation_Score.CompetenceCatQId = CompetenceCatQ.Id AND " +
                "CompetenceCatQ.CompetenceCatId = 17 AND " +
                "Evaluation_Score.EvaluationId = @Id";

                comm.Connection = conn;
                comm.CommandText = sqlStr;
                comm.Parameters.AddWithValue("@Id", evaluationId);
                try
                {
                    adap = new SqlDataAdapter(comm);
                    using (DataSet ds = new DataSet())
                    {
                        adap.Fill(ds, "tblCommunication");
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

        private DataSet getJudgement(int evaluationId)
        {
            using (SqlConnection conn = new SqlConnection(System.Web.Configuration.WebConfigurationManager.ConnectionStrings["dbAMS"].ConnectionString))
            {
                conn.Open();
                comm = new SqlCommand();
                string sqlStr = "SELECT Evaluation_Score.Id, " +
                "CompetenceCatQ.Question, Evaluation_Score.StaffRating, Evaluation_Score.EvaluatorRating " +
                "FROM Evaluation_Score, CompetenceCatQ " +
                "WHERE Evaluation_Score.CompetenceCatQId = CompetenceCatQ.Id AND " +
                "CompetenceCatQ.CompetenceCatId = 16 AND " +
                "Evaluation_Score.EvaluationId = @Id";

                comm.Connection = conn;
                comm.CommandText = sqlStr;
                comm.Parameters.AddWithValue("@Id", evaluationId);
                try
                {
                    adap = new SqlDataAdapter(comm);
                    using (DataSet ds = new DataSet())
                    {
                        adap.Fill(ds, "tblJudgement");
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

        private DataSet getInitiative(int evaluationId)
        {
            using (SqlConnection conn = new SqlConnection(System.Web.Configuration.WebConfigurationManager.ConnectionStrings["dbAMS"].ConnectionString))
            {
                conn.Open();
                comm = new SqlCommand();
                string sqlStr = "SELECT Evaluation_Score.Id, " +
                "CompetenceCatQ.Question, Evaluation_Score.StaffRating, Evaluation_Score.EvaluatorRating " +
                "FROM Evaluation_Score, CompetenceCatQ " +
                "WHERE Evaluation_Score.CompetenceCatQId = CompetenceCatQ.Id AND " +
                "CompetenceCatQ.CompetenceCatId = 15 AND " +
                "Evaluation_Score.EvaluationId = @Id";

                comm.Connection = conn;
                comm.CommandText = sqlStr;
                comm.Parameters.AddWithValue("@Id", evaluationId);
                try
                {
                    adap = new SqlDataAdapter(comm);
                    using (DataSet ds = new DataSet())
                    {
                        adap.Fill(ds, "tblInitiative");
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

        private DataSet getAttitude(int evaluationId)
        {
            using (SqlConnection conn = new SqlConnection(System.Web.Configuration.WebConfigurationManager.ConnectionStrings["dbAMS"].ConnectionString))
            {
                conn.Open();
                comm = new SqlCommand();
                string sqlStr = "SELECT Evaluation_Score.Id, " +
                "CompetenceCatQ.Question, Evaluation_Score.StaffRating, Evaluation_Score.EvaluatorRating " +
                "FROM Evaluation_Score, CompetenceCatQ " +
                "WHERE Evaluation_Score.CompetenceCatQId = CompetenceCatQ.Id AND " +
                "CompetenceCatQ.CompetenceCatId = 14 AND " +
                "Evaluation_Score.EvaluationId = @Id";

                comm.Connection = conn;
                comm.CommandText = sqlStr;
                comm.Parameters.AddWithValue("@Id", evaluationId);
                try
                {
                    adap = new SqlDataAdapter(comm);
                    using (DataSet ds = new DataSet())
                    {
                        adap.Fill(ds, "tblAttitude");
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

        private DataSet getIntepersonalRelationship(int evaluationId)
        {
            using (SqlConnection conn = new SqlConnection(System.Web.Configuration.WebConfigurationManager.ConnectionStrings["dbAMS"].ConnectionString))
            {
                conn.Open();
                comm = new SqlCommand();
                string sqlStr = "SELECT Evaluation_Score.Id, " +
                "CompetenceCatQ.Question, Evaluation_Score.StaffRating, Evaluation_Score.EvaluatorRating " +
                "FROM Evaluation_Score, CompetenceCatQ " +
                "WHERE Evaluation_Score.CompetenceCatQId = CompetenceCatQ.Id AND " +
                "CompetenceCatQ.CompetenceCatId = 13 AND " +
                "Evaluation_Score.EvaluationId = @Id";

                comm.Connection = conn;
                comm.CommandText = sqlStr;
                comm.Parameters.AddWithValue("@Id", evaluationId);
                try
                {
                    adap = new SqlDataAdapter(comm);
                    using (DataSet ds = new DataSet())
                    {
                        adap.Fill(ds, "tblInterpersonalRelationship");
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

        private DataSet getAttendanceAndPunctuality(int evaluationId)
        {
            using (SqlConnection conn = new SqlConnection(System.Web.Configuration.WebConfigurationManager.ConnectionStrings["dbAMS"].ConnectionString))
            {
                conn.Open();
                comm = new SqlCommand();
                string sqlStr = "SELECT Evaluation_Score.Id, " +
                "CompetenceCatQ.Question, Evaluation_Score.StaffRating, Evaluation_Score.EvaluatorRating " +
                "FROM Evaluation_Score, CompetenceCatQ " +
                "WHERE Evaluation_Score.CompetenceCatQId = CompetenceCatQ.Id AND " +
                "CompetenceCatQ.CompetenceCatId = 12 AND " +
                "Evaluation_Score.EvaluationId = @Id";

                comm.Connection = conn;
                comm.CommandText = sqlStr;
                comm.Parameters.AddWithValue("@Id", evaluationId);
                try
                {
                    adap = new SqlDataAdapter(comm);
                    using (DataSet ds = new DataSet())
                    {
                        adap.Fill(ds, "tblAttendanceAndPunctuality");
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

        private DataSet getCooperation(int evaluationId)
        {
            using (SqlConnection conn = new SqlConnection(System.Web.Configuration.WebConfigurationManager.ConnectionStrings["dbAMS"].ConnectionString))
            {
                conn.Open();
                comm = new SqlCommand();
                string sqlStr = "SELECT Evaluation_Score.Id, " +
                "CompetenceCatQ.Question, Evaluation_Score.StaffRating, Evaluation_Score.EvaluatorRating " +
                "FROM Evaluation_Score, CompetenceCatQ " +
                "WHERE Evaluation_Score.CompetenceCatQId = CompetenceCatQ.Id AND " +
                "CompetenceCatQ.CompetenceCatId = 11 AND " +
                "Evaluation_Score.EvaluationId = @Id";

                comm.Connection = conn;
                comm.CommandText = sqlStr;
                comm.Parameters.AddWithValue("@Id", evaluationId);
                try
                {
                    adap = new SqlDataAdapter(comm);
                    using (DataSet ds = new DataSet())
                    {
                        adap.Fill(ds, "tblCooperation");
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
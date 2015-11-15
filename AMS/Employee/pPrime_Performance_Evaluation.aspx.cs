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
        SqlCommand comm;
        SqlDataAdapter adap;
        DataTable dt;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                GenerateReport();
            }
        }

        private void GenerateReport()
        {
            ReportViewer1.ProcessingMode = ProcessingMode.Local;
            ReportViewer1.LocalReport.ReportPath = Server.MapPath("~/Employee/Report3.rdlc");

            //get selected user
            Guid UserId = Guid.Parse(Session["UserId"].ToString());

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
            //ReportParameter[] param = new ReportParameter[17];

            //fill params
            //param[0] = new ReportParameter("lblName", dt.Rows[0]["LName"].ToString() + ", " + dt.Rows[0]["FName"].ToString() + " " + dt.Rows[0]["MName"].ToString());
            //param[1] = new ReportParameter("lblDateHired", job.getHiredDate(UserId));
            //param[2] = new ReportParameter("lblPosition", job.getPosition(UserId));
            //param[3] = new ReportParameter("lblRemarksName", dtEvaluation.Rows[0]["RemarksName"].ToString());
            //param[4] = new ReportParameter("lblUnacceptable", dtEvaluation.Rows[0]["ImpUnacceptable"].ToString());
            //param[5] = new ReportParameter("lblFallShort", dtEvaluation.Rows[0]["ImpFallShort"].ToString());
            //param[6] = new ReportParameter("lblEffective", dtEvaluation.Rows[0]["ImpEffective"].ToString());
            //param[7] = new ReportParameter("lblExceptional", dtEvaluation.Rows[0]["ImpExceptional"].ToString());
            //param[8] = new ReportParameter("lblRecommendation", dtEvaluation.Rows[0]["Recommendation"].ToString());
            //param[9] = new ReportParameter("lblNeedImrpo", dtEvaluation.Rows[0]["NeedImprovement"].ToString());
            //param[10] = new ReportParameter("lblDateEvaluated", dtEvaluation.Rows[0]["DateEvaluated"].ToString());
            //param[11] = new ReportParameter("TotalScore", dtEvaluation.Rows[0]["TotalScore"].ToString());
            //param[12] = new ReportParameter("lblHighlyEffective", dtEvaluation.Rows[0]["ImpHighlyEffective"].ToString());
            //param[13] = new ReportParameter("lblEvaluatedBy", dtEvaluation.Rows[0]["EvaluatedBy"].ToString());
            //param[14] = new ReportParameter("lblApprovedByManager", dtEvaluation.Rows[0]["ApprovedByManager"].ToString());
            //param[15] = new ReportParameter("lblAcknowledgedBy", dtEvaluation.Rows[0]["AcknowledgedBy"].ToString());
            //param[16] = new ReportParameter("lblApprovedByHR", dtEvaluation.Rows[0]["ApprovedByHR"].ToString());
            //ReportViewer1.LocalReport.SetParameters(param);

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
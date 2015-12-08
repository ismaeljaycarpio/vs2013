using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using System.Data.SqlClient;
using System.Data;

namespace AMS.DAL
{
    public class Evaluation
    {
        //init
        SqlConnection conn;
        SqlCommand comm;
        SqlDataAdapter adp;
        DataTable dt;
        string strSql = "";


        #region TOPLIS
        public DataTable displayTSIQuestions()
        {
            strSql = "SELECT Competence.Competence,CompetenceCat.CompetenceCat,CompetenceCat.Description,CompetenceCat.TSIRating,CompetenceCat.Id " +
                    "FROM Competence_Master, Competence, CompetenceCat " +
                    "WHERE " +
                    "Competence_Master.Id = Competence.Competence_MasterId AND " +
                    "Competence.Id = CompetenceCat.CompetenceId AND " +
                    "Competence_Master.Id = 1 " +
                    "ORDER BY Competence.Id, CompetenceCat.Id";

            conn = new SqlConnection();
            conn.ConnectionString = WebConfigurationManager.ConnectionStrings["dbAMS"].ConnectionString;
            comm = new SqlCommand(strSql, conn);
            dt = new DataTable();
            adp = new SqlDataAdapter(comm);

            conn.Open();
            adp.Fill(dt);
            conn.Close();

            return dt;
        }

        public DataTable display_filled_TSIQuestions(Guid UserId, int evaluationId)
        {
            strSql = "SELECT Competence.Competence,CompetenceCat.CompetenceCat,CompetenceCat.Description,CompetenceCat.TSIRating, " +
                "Evaluation_Score.StaffRating,Evaluation_Score.EvaluatorRating, Evaluation_Score.Id " +
                    "FROM Competence_Master, Competence, CompetenceCat, Evaluation, Evaluation_Score " +
                    "WHERE " +
                    "Competence_Master.Id = Competence.Competence_MasterId AND " +
                    "Competence.Id = CompetenceCat.CompetenceId AND " +
                    "Evaluation.Id = Evaluation_Score.EvaluationId AND " +
                    "Evaluation_Score.CompetenceCatId = CompetenceCat.Id AND " +
                    "Competence_Master.Id = 1 AND " +
                    "Evaluation.UserId = @UserId " +
                    "AND Evaluation.Id = @EvaluationId " +
                    "ORDER BY Competence.Id, CompetenceCat.Id";

            conn = new SqlConnection();
            conn.ConnectionString = WebConfigurationManager.ConnectionStrings["dbAMS"].ConnectionString;
            comm = new SqlCommand(strSql, conn);
            comm.Parameters.AddWithValue("@UserId", UserId);
            comm.Parameters.AddWithValue("@EvaluationId", evaluationId);
            dt = new DataTable();
            adp = new SqlDataAdapter(comm);

            conn.Open();
            adp.Fill(dt);
            conn.Close();

            return dt;
        }

        public int InsertEvaluation(
            Guid UserId,
            string EvaluationType,
            Guid EvaluatedById,
            decimal TotalScore,
            string RemarksName,
            string ImpUnacceptable,
            string ImpFallShort,
            string ImpEffective,
            string ImpHighlyEffective,
            string ImpExceptional,
            string Recommendation,
            string NeedImprovement,
            Guid ApprovedByManagerId,
            Guid ApprovedByHRId,
            string agency,
            string next_eval_date
            )
        {
            int _newlyInsertedId = 0;

            strSql = "INSERT INTO Evaluation(UserId,EvaluationType,EvaluatedById,DateEvaluated,TotalScore,RemarksName," +
                "ImpUnacceptable,ImpFallShort,ImpEffective,ImpHighlyEffective, " +
                "ImpExceptional, Recommendation, NeedImprovement, ApprovedByManagerId, " +
                "ApprovedByHRId,Agency, NextEvaluationDate)" +
                "VALUES(@UserId, @EvaluationType, @EvaluatedById, @DateEvaluated, @TotalScore,@RemarksName, " +
                "@ImpUnacceptable, @ImpFallShort, @ImpEffective, @ImpHighlyEffective, " +
                "@ImpExceptional, @Recommendation, @NeedImprovement, " +
                "@ApprovedByManagerId, @ApprovedByHRId,@Agency,@NextEvaluationDate);" +
                "SELECT SCOPE_IDENTITY()";

            conn = new SqlConnection();
            conn.ConnectionString = WebConfigurationManager.ConnectionStrings["dbAMS"].ConnectionString;

            using (comm = new SqlCommand(strSql, conn))
            {
                conn.Open();
                comm.Parameters.AddWithValue("@UserId", UserId);
                comm.Parameters.AddWithValue("@EvaluationType", EvaluationType);
                comm.Parameters.AddWithValue("@EvaluatedById", EvaluatedById);
                comm.Parameters.AddWithValue("@DateEvaluated", DateTime.Now.ToShortDateString());
                comm.Parameters.AddWithValue("@TotalScore", TotalScore);
                comm.Parameters.AddWithValue("@RemarksName", RemarksName);
                comm.Parameters.AddWithValue("@ImpUnacceptable", ImpUnacceptable);
                comm.Parameters.AddWithValue("@ImpFallShort", ImpFallShort);
                comm.Parameters.AddWithValue("@ImpEffective", ImpEffective);
                comm.Parameters.AddWithValue("@ImpHighlyEffective", ImpHighlyEffective);
                comm.Parameters.AddWithValue("@ImpExceptional", ImpExceptional);
                comm.Parameters.AddWithValue("@Recommendation", Recommendation);
                comm.Parameters.AddWithValue("@NeedImprovement", NeedImprovement);
                comm.Parameters.AddWithValue("@ApprovedByManagerId", ApprovedByManagerId);
                comm.Parameters.AddWithValue("@ApprovedByHRId", ApprovedByHRId);
                comm.Parameters.AddWithValue("@NextEvaluationDate", next_eval_date);
                comm.Parameters.AddWithValue("@Agency", agency);

                object exScalar = comm.ExecuteScalar();
                _newlyInsertedId = (exScalar == null ? -1 : Convert.ToInt32(exScalar.ToString()));
                conn.Close();
            }
            comm.Dispose();
            conn.Dispose();

            return _newlyInsertedId;
        }

        //for self evaluating
        //dont include dateevaluated
        public int InsertEvaluation(
            Guid UserId,
            string EvaluationType,
            string agency
            )
        {
            int _newlyInsertedId = 0;

            strSql = "INSERT INTO Evaluation(UserId,EvaluationType,EvaluatedById,ApprovedByManagerId,ApprovedByHRId,Agency) " +
                "VALUES(@UserId, @EvaluationType,@EvaluatedById,@ApprovedByManagerId,@ApprovedByHRId,@Agency); " +
                "SELECT SCOPE_IDENTITY()";

            conn = new SqlConnection();
            conn.ConnectionString = WebConfigurationManager.ConnectionStrings["dbAMS"].ConnectionString;

            using (comm = new SqlCommand(strSql, conn))
            {
                conn.Open();
                comm.Parameters.AddWithValue("@UserId", UserId);
                comm.Parameters.AddWithValue("@EvaluationType", EvaluationType);
                comm.Parameters.AddWithValue("@EvaluatedById", Guid.Empty);
                comm.Parameters.AddWithValue("@ApprovedByManagerId", Guid.Empty);
                comm.Parameters.AddWithValue("@ApprovedByHRId", Guid.Empty);
                comm.Parameters.AddWithValue("@Agency", agency);

                object exScalar = comm.ExecuteScalar();
                _newlyInsertedId = (exScalar == null ? -1 : Convert.ToInt32(exScalar.ToString()));
                conn.Close();
            }
            comm.Dispose();
            conn.Dispose();

            return _newlyInsertedId;
        }

        public void UpdateEvaluation(
            Guid EvaludatedById,
            decimal TotalScore,
            string RemarksName,
            string ImpUnacceptable,
            string ImpFallShort,
            string ImpEffective,
            string ImpHighlyEffective,
            string ImpExceptional,
            string Recommendation,
            string NeedImprovement,
            Guid ApprovedByManagerId,
            Guid ApprovedByHRId,
            string next_eval_date,
            string evaluationId)
        {
            strSql = "UPDATE Evaluation SET " +
                "EvaluatedById = @EvaluatedById, " +
                "DateEvaluated = @DateEvaluated, " +
                "TotalScore = @TotalScore, " +
                "RemarksName = @RemarksName, " +
                "ImpUnacceptable = @ImpUnacceptable, " +
                "ImpFallShort = @ImpFallShort, " +
                "ImpEffective = @ImpEffective, " +
                "ImpHighlyEffective = @ImpHighlyEffective, " +
                "ImpExceptional = @ImpExceptional, " +
                "Recommendation = @Recommendation, " +
                "NeedImprovement = @NeedImprovement, " +
                "ApprovedByManagerId = @ApprovedByManagerId, " +
                "ApprovedByHRId = @ApprovedByHRId, " +
                "NextEvaluationDate = @NextEvaluationDate " +
                "WHERE Id = @Id";

            conn = new SqlConnection();
            conn.ConnectionString = WebConfigurationManager.ConnectionStrings["dbAMS"].ConnectionString;

            using (comm = new SqlCommand(strSql, conn))
            {
                conn.Open();
                comm.Parameters.AddWithValue("@EvaluatedById", EvaludatedById);
                comm.Parameters.AddWithValue("@DateEvaluated", DateTime.Now.ToShortDateString());
                comm.Parameters.AddWithValue("@TotalScore", TotalScore);
                comm.Parameters.AddWithValue("@RemarksName", RemarksName);
                comm.Parameters.AddWithValue("@ImpUnacceptable", ImpUnacceptable);
                comm.Parameters.AddWithValue("@ImpFallShort", ImpFallShort);
                comm.Parameters.AddWithValue("@ImpEffective", ImpEffective);
                comm.Parameters.AddWithValue("@ImpHighlyEffective", ImpHighlyEffective);
                comm.Parameters.AddWithValue("@ImpExceptional", ImpExceptional);
                comm.Parameters.AddWithValue("@Recommendation", Recommendation);
                comm.Parameters.AddWithValue("@NeedImprovement", NeedImprovement);
                comm.Parameters.AddWithValue("@ApprovedByManagerId", ApprovedByManagerId);
                comm.Parameters.AddWithValue("@ApprovedByHRId", ApprovedByHRId);
                comm.Parameters.AddWithValue("@NextEvaluationDate", next_eval_date);
                comm.Parameters.AddWithValue("@Id", evaluationId);
                comm.ExecuteNonQuery();
                conn.Close();
            }
            comm.Dispose();
            conn.Dispose();
        }



        public void addEvaluation_Scores_Evaluator(
            int evaluationId,
            int competencyId,
            decimal evaluator_rating)
        {
            strSql = "INSERT INTO Evaluation_Score(EvaluationId, CompetenceCatId, EvaluatorRating) " +
                "VALUES(@Agency, @CompetenceCatId, @EvaluatorRating)";

            conn = new SqlConnection();
            conn.ConnectionString = WebConfigurationManager.ConnectionStrings["dbAMS"].ConnectionString;

            using (comm = new SqlCommand(strSql, conn))
            {
                conn.Open();
                comm.Parameters.AddWithValue("@Agency", evaluationId);
                comm.Parameters.AddWithValue("@CompetenceCatId", competencyId);
                comm.Parameters.AddWithValue("@EvaluatorRating", evaluator_rating);
                comm.ExecuteNonQuery();
                conn.Close();
            }
            comm.Dispose();
            conn.Dispose();
        }

        public void addEvaluation_Scores_Staff(
            int evaluationId,
            int competencyId,
            decimal rating_staff)
        {
            strSql = "INSERT INTO Evaluation_Score(EvaluationId, CompetenceCatId, StaffRating) " +
                "VALUES(@Agency, @CompetenceCatId, @StaffRating)";

            conn = new SqlConnection();
            conn.ConnectionString = WebConfigurationManager.ConnectionStrings["dbAMS"].ConnectionString;

            using (comm = new SqlCommand(strSql, conn))
            {
                conn.Open();
                comm.Parameters.AddWithValue("@Agency", evaluationId);
                comm.Parameters.AddWithValue("@CompetenceCatId", competencyId);
                comm.Parameters.AddWithValue("@StaffRating", rating_staff);
                comm.ExecuteNonQuery();
                conn.Close();
            }
            comm.Dispose();
            conn.Dispose();
        }

        public void updateEvaluation_Scores_Evaluator(
            int eva_score_id,
            decimal rating)
        {
            strSql = "UPDATE Evaluation_Score SET EvaluatorRating=@Rating WHERE " +
                "Id = @Id";

            conn = new SqlConnection();
            conn.ConnectionString = WebConfigurationManager.ConnectionStrings["dbAMS"].ConnectionString;

            using (comm = new SqlCommand(strSql, conn))
            {
                conn.Open();
                comm.Parameters.AddWithValue("@Rating", rating);
                comm.Parameters.AddWithValue("@Id", eva_score_id);
                comm.ExecuteNonQuery();
                conn.Close();
            }
            comm.Dispose();
            conn.Dispose();
        }

        public void updateEvaluation_Scores_Staff(
            int eva_score_id,
            decimal rating)
        {
            strSql = "UPDATE Evaluation_Score SET StaffRating=@Rating WHERE " +
                "Id = @Id";

            conn = new SqlConnection();
            conn.ConnectionString = WebConfigurationManager.ConnectionStrings["dbAMS"].ConnectionString;

            using (comm = new SqlCommand(strSql, conn))
            {
                conn.Open();
                comm.Parameters.AddWithValue("@Rating", rating);
                comm.Parameters.AddWithValue("@Id", eva_score_id);
                comm.ExecuteNonQuery();
                conn.Close();
            }
            comm.Dispose();
            conn.Dispose();
        }




        #endregion

        #region PrimePower
        //Prime
        public DataTable getCooperation()
        {
            //11 -> Cooperation
            strSql = "SELECT Id, Question FROM CompetenceCatQ WHERE CompetenceCatId = 11";

            conn = new SqlConnection();
            conn.ConnectionString = WebConfigurationManager.ConnectionStrings["dbAMS"].ConnectionString;
            comm = new SqlCommand(strSql, conn);
            dt = new DataTable();
            adp = new SqlDataAdapter(comm);

            conn.Open();
            adp.Fill(dt);
            conn.Close();

            return dt;
        }

        public DataTable getAttendanceAndPunctuality()
        {
            //11 -> Attendance and Punctuality
            strSql = "SELECT Id, Question FROM CompetenceCatQ WHERE CompetenceCatId = 12";

            conn = new SqlConnection();
            conn.ConnectionString = WebConfigurationManager.ConnectionStrings["dbAMS"].ConnectionString;
            comm = new SqlCommand(strSql, conn);
            dt = new DataTable();
            adp = new SqlDataAdapter(comm);

            conn.Open();
            adp.Fill(dt);
            conn.Close();

            return dt;
        }


        public DataTable getInterpersonalRelationship()
        {
            //11 -> Interpersonal Relationship
            strSql = "SELECT Id, Question FROM CompetenceCatQ WHERE CompetenceCatId = 13";

            conn = new SqlConnection();
            conn.ConnectionString = WebConfigurationManager.ConnectionStrings["dbAMS"].ConnectionString;
            comm = new SqlCommand(strSql, conn);
            dt = new DataTable();
            adp = new SqlDataAdapter(comm);

            conn.Open();
            adp.Fill(dt);
            conn.Close();

            return dt;
        }

        public DataTable getAttitude()
        {
            //11 -> Attitude
            strSql = "SELECT Id, Question FROM CompetenceCatQ WHERE CompetenceCatId = 14";

            conn = new SqlConnection();
            conn.ConnectionString = WebConfigurationManager.ConnectionStrings["dbAMS"].ConnectionString;
            comm = new SqlCommand(strSql, conn);
            dt = new DataTable();
            adp = new SqlDataAdapter(comm);

            conn.Open();
            adp.Fill(dt);
            conn.Close();

            return dt;
        }

        public DataTable getIniatitve()
        {
            //11 -> Initiative
            strSql = "SELECT Id, Question FROM CompetenceCatQ WHERE CompetenceCatId = 15";

            conn = new SqlConnection();
            conn.ConnectionString = WebConfigurationManager.ConnectionStrings["dbAMS"].ConnectionString;
            comm = new SqlCommand(strSql, conn);
            dt = new DataTable();
            adp = new SqlDataAdapter(comm);

            conn.Open();
            adp.Fill(dt);
            conn.Close();

            return dt;
        }

        public DataTable getJudgement()
        {
            //11 -> Initiative
            strSql = "SELECT Id, Question FROM CompetenceCatQ WHERE CompetenceCatId = 16";

            conn = new SqlConnection();
            conn.ConnectionString = WebConfigurationManager.ConnectionStrings["dbAMS"].ConnectionString;
            comm = new SqlCommand(strSql, conn);
            dt = new DataTable();
            adp = new SqlDataAdapter(comm);

            conn.Open();
            adp.Fill(dt);
            conn.Close();

            return dt;
        }

        public DataTable getCommunication()
        {
            //11 -> Initiative
            strSql = "SELECT Id, Question FROM CompetenceCatQ WHERE CompetenceCatId = 17";

            conn = new SqlConnection();
            conn.ConnectionString = WebConfigurationManager.ConnectionStrings["dbAMS"].ConnectionString;
            comm = new SqlCommand(strSql, conn);
            dt = new DataTable();
            adp = new SqlDataAdapter(comm);

            conn.Open();
            adp.Fill(dt);
            conn.Close();

            return dt;
        }

        public DataTable getSafety()
        {
            //11 -> Initiative
            strSql = "SELECT Id, Question FROM CompetenceCatQ WHERE CompetenceCatId = 18";

            conn = new SqlConnection();
            conn.ConnectionString = WebConfigurationManager.ConnectionStrings["dbAMS"].ConnectionString;
            comm = new SqlCommand(strSql, conn);
            dt = new DataTable();
            adp = new SqlDataAdapter(comm);

            conn.Open();
            adp.Fill(dt);
            conn.Close();

            return dt;
        }

        public DataTable getDependability()
        {
            //11 -> Initiative
            strSql = "SELECT Id, Question FROM CompetenceCatQ WHERE CompetenceCatId = 19";

            conn = new SqlConnection();
            conn.ConnectionString = WebConfigurationManager.ConnectionStrings["dbAMS"].ConnectionString;
            comm = new SqlCommand(strSql, conn);
            dt = new DataTable();
            adp = new SqlDataAdapter(comm);

            conn.Open();
            adp.Fill(dt);
            conn.Close();

            return dt;
        }

        public DataTable getSecificJobSkills()
        {
            //11 -> Initiative
            strSql = "SELECT Id, Question FROM CompetenceCatQ WHERE CompetenceCatId = 20";

            conn = new SqlConnection();
            conn.ConnectionString = WebConfigurationManager.ConnectionStrings["dbAMS"].ConnectionString;
            comm = new SqlCommand(strSql, conn);
            dt = new DataTable();
            adp = new SqlDataAdapter(comm);

            conn.Open();
            adp.Fill(dt);
            conn.Close();

            return dt;
        }

        public DataTable getProductivity()
        {
            //11 -> Initiative
            strSql = "SELECT Id, Question FROM CompetenceCatQ WHERE CompetenceCatId = 21";

            conn = new SqlConnection();
            conn.ConnectionString = WebConfigurationManager.ConnectionStrings["dbAMS"].ConnectionString;
            comm = new SqlCommand(strSql, conn);
            dt = new DataTable();
            adp = new SqlDataAdapter(comm);

            conn.Open();
            adp.Fill(dt);
            conn.Close();

            return dt;
        }

        public DataTable getOrganizationalSkill()
        {
            //11 -> Initiative
            strSql = "SELECT Id, Question FROM CompetenceCatQ WHERE CompetenceCatId = 22";

            conn = new SqlConnection();
            conn.ConnectionString = WebConfigurationManager.ConnectionStrings["dbAMS"].ConnectionString;
            comm = new SqlCommand(strSql, conn);
            dt = new DataTable();
            adp = new SqlDataAdapter(comm);

            conn.Open();
            adp.Fill(dt);
            conn.Close();

            return dt;
        }

        public DataTable DisplayMyEvaluation(Guid UserId)
        {
            strSql = "SELECT Id, EvaluationType, EvaluatedById, DateEvaluated, ApprovedByManagerId, ApprovedByHRId FROM Evaluation WHERE UserId = @UserId " +
                "ORDER BY DateEvaluated DESC";

            conn = new SqlConnection();
            conn.ConnectionString = WebConfigurationManager.ConnectionStrings["dbAMS"].ConnectionString;
            comm = new SqlCommand(strSql, conn);
            comm.Parameters.AddWithValue("@UserId", UserId);
            dt = new DataTable();
            adp = new SqlDataAdapter(comm);

            conn.Open();
            adp.Fill(dt);
            conn.Close();

            return dt;
        }

        //Prime only
        public int InsertEvaluation_Prime(
            Guid UserId,
            string EvaluationType,
            Guid EvaluatedById,
            Guid ApprovedByManagerId,
            Guid ApprovedByHRId,
            string agency,
            string commentSection1A,
            string commentSection1B,
            string commentSection1C,
            string commentSection2A,
            string commentSection2B,
            string commentSection2C,
            string commentSection3A,
            string commentSection3B,
            string commentSection3C,
            string commentSection3D,
            string commentSection3E,
            string commentSection3F,
            decimal section1A,
            decimal section1B,
            decimal section1C,
            decimal section2A,
            decimal section2B,
            decimal section2C,
            decimal section3A,
            decimal section3B,
            decimal section3C,
            decimal section3D,
            decimal section3E,
            decimal section3F,
            string daysSick,
            string daysTardy,
            string primeComment,
            string next_eval_date,
            string employeescreativeContribution,
            string employeesnewSkills,
            string empployeesStrength,
            string employeesImprovement,
            string employeesChanges,
            string employeesPersonalGoals,
            string employeesRecommendation
            )
        {
            int _newlyInsertedId = 0;

            strSql = "INSERT INTO Evaluation(UserId,EvaluationType,EvaluatedById, DateEvaluated, ApprovedByManagerId, " +
                "ApprovedByHRId,Agency, " +
                "Section1A, Section1B, Section1C, " +
                "Section2A, Section2B, Section2C, " +
                "Section3A, Section3B, Section3C, Section3D, Section3E, Section3F," +
                "CommentSection1A, CommentSection1B, CommentSection1C, " +
                "CommentSection2A, CommentSection2B, CommentSection2C, " +
                "CommentSection3A, CommentSection3B, CommentSection3C,CommentSection3D,CommentSection3E,CommentSection3F, " +
                "DaysSick, DaysTardy, primeComments, NextEvaluationDate, " +
                "EmployeesCreativeContribution, EmployeesNewSkills, EmployeesStrength, " +
                "EmployeesImprovement, EmployeesChanges, EmployeesPersonalGoals, " +
                "EmployeesRecommendation) " +
                "VALUES(@UserId, @EvaluationType, @EvaluatedById, @DateEvaluated, " +
                "@ApprovedByManagerId, @ApprovedByHRId,@Agency, " +
                "@Section1A, @Section1B, @Section1C, " +
                "@Section2A, @Section2B, @Section2C, " +
                "@Section3A, @Section3B, @Section3C, @Section3D, @Section3E, @Section3F, " +
                "@CommentSection1A, @CommentSection1B, @CommentSection1C, " +
                "@CommentSection2A, @CommentSection2B, @CommentSection2C, " +
                "@CommentSection3A, @CommentSection3B, @CommentSection3C, @CommentSection3D, @CommentSection3E,@CommentSection3F, " +
                "@DaysSick, @DaysTardy, @primeComments, @NextEvaluationDate, " +
                "@EmployeesCreativeContribution, @EmployeesNewSkills, @EmployeesStrength, " +
                "@EmployeesImprovement, @EmployeesChanges, @EmployeesPersonalGoals, " +
                "@EmployeesRecommendation);" +
                "SELECT SCOPE_IDENTITY()";

            conn = new SqlConnection();
            conn.ConnectionString = WebConfigurationManager.ConnectionStrings["dbAMS"].ConnectionString;

            using (comm = new SqlCommand(strSql, conn))
            {
                conn.Open();
                comm.Parameters.AddWithValue("@UserId", UserId);
                comm.Parameters.AddWithValue("@EvaluationType", EvaluationType);
                comm.Parameters.AddWithValue("@EvaluatedById", EvaluatedById);
                comm.Parameters.AddWithValue("@DateEvaluated", DateTime.Now.ToShortDateString());
                comm.Parameters.AddWithValue("@ApprovedByManagerId", ApprovedByManagerId);
                comm.Parameters.AddWithValue("@ApprovedByHRId", ApprovedByHRId);
                comm.Parameters.AddWithValue("@Agency", agency);
                comm.Parameters.AddWithValue("@Section1A", section1A);
                comm.Parameters.AddWithValue("@Section1B", section1B);
                comm.Parameters.AddWithValue("@Section1C", section1C);
                comm.Parameters.AddWithValue("@Section2A", section2A);
                comm.Parameters.AddWithValue("@Section2B", section2B);
                comm.Parameters.AddWithValue("@Section2C", section2C);
                comm.Parameters.AddWithValue("@Section3A", section3A);
                comm.Parameters.AddWithValue("@Section3B", section3B);
                comm.Parameters.AddWithValue("@Section3C", section3C);
                comm.Parameters.AddWithValue("@Section3D", section3D);
                comm.Parameters.AddWithValue("@Section3E", section3E);
                comm.Parameters.AddWithValue("@Section3F", section3F);
                comm.Parameters.AddWithValue("@CommentSection1A", commentSection1A);
                comm.Parameters.AddWithValue("@CommentSection1B", commentSection1B);
                comm.Parameters.AddWithValue("@CommentSection1C", commentSection1C);
                comm.Parameters.AddWithValue("@CommentSection2A", commentSection2A);
                comm.Parameters.AddWithValue("@CommentSection2B", commentSection2B);
                comm.Parameters.AddWithValue("@CommentSection2C", commentSection2C);
                comm.Parameters.AddWithValue("@CommentSection3A", commentSection3A);
                comm.Parameters.AddWithValue("@CommentSection3B", commentSection3B);
                comm.Parameters.AddWithValue("@CommentSection3C", commentSection3C);
                comm.Parameters.AddWithValue("@CommentSection3D", commentSection3D);
                comm.Parameters.AddWithValue("@CommentSection3E", commentSection3E);
                comm.Parameters.AddWithValue("@CommentSection3F", commentSection3F);
                comm.Parameters.AddWithValue("@DaysSick", daysSick);
                comm.Parameters.AddWithValue("@DaysTardy", daysTardy);
                comm.Parameters.AddWithValue("@primeComments", primeComment);
                comm.Parameters.AddWithValue("@NextEvaluationDate", next_eval_date);
                comm.Parameters.AddWithValue("@EmployeesCreativeContribution", employeescreativeContribution);
                comm.Parameters.AddWithValue("@EmployeesNewSkills", employeesnewSkills);
                comm.Parameters.AddWithValue("@EmployeesStrength", empployeesStrength);
                comm.Parameters.AddWithValue("@EmployeesImprovement", employeesImprovement);
                comm.Parameters.AddWithValue("@EmployeesChanges", employeesChanges);
                comm.Parameters.AddWithValue("@EmployeesPersonalGoals", employeesPersonalGoals);
                comm.Parameters.AddWithValue("@EmployeesRecommendation", employeesRecommendation);

                object exScalar = comm.ExecuteScalar();
                _newlyInsertedId = (exScalar == null ? -1 : Convert.ToInt32(exScalar.ToString()));
                conn.Close();
            }
            comm.Dispose();
            conn.Dispose();

            return _newlyInsertedId;
        }

        //self-evaluate prime
        public int InsertEvaluation_Prime(
           Guid UserId,
           string EvaluationType,
           string agency,
           string commentSection1A,
           string commentSection1B,
           string commentSection1C,
           string commentSection2A,
           string commentSection2B,
           string commentSection2C,
           string commentSection3A,
           string commentSection3B,
           string commentSection3C,
           string commentSection3D,
           string commentSection3E,
           string commentSection3F,
           decimal section1A,
           decimal section1B,
           decimal section1C,
           decimal section2A,
           decimal section2B,
           decimal section2C,
           decimal section3A,
           decimal section3B,
           decimal section3C,
           decimal section3D,
           decimal section3E,
           decimal section3F,
           string daysSick,
           string daysTardy,
           string primeComment,
           string next_eval_date,
           string employeescreativeContribution,
           string employeesnewSkills,
           string empployeesStrength,
           string employeesImprovement,
           string employeesChanges,
           string employeesPersonalGoals,
           string employeesRecommendation
           )
        {
            int _newlyInsertedId = 0;

            strSql = "INSERT INTO Evaluation(UserId,EvaluationType,EvaluatedById, DateEvaluated, ApprovedByManagerId, " +
                "ApprovedByHRId,Agency, " +
                "Section1A, Section1B, Section1C, " +
                "Section2A, Section2B, Section2C, " +
                "Section3A, Section3B, Section3C, Section3D, Section3E, Section3F," +
                "CommentSection1A, CommentSection1B, CommentSection1C, " +
                "CommentSection2A, CommentSection2B, CommentSection2C, " +
                "CommentSection3A, CommentSection3B, CommentSection3C,CommentSection3D,CommentSection3E,CommentSection3F, " +
                "DaysSick, DaysTardy, primeComments, NextEvaluationDate, " +
                "EmployeesCreativeContribution, EmployeesNewSkills, EmployeesStrength, " +
                "EmployeesImprovement, EmployeesChanges, EmployeesPersonalGoals, " +
                "EmployeesRecommendation) " +
                "VALUES(@UserId, @EvaluationType, @EvaluatedById, @DateEvaluated, " +
                "@ApprovedByManagerId, @ApprovedByHRId,@Agency, " +
                "@Section1A, @Section1B, @Section1C, " +
                "@Section2A, @Section2B, @Section2C, " +
                "@Section3A, @Section3B, @Section3C, @Section3D, @Section3E, @Section3F, " +
                "@CommentSection1A, @CommentSection1B, @CommentSection1C, " +
                "@CommentSection2A, @CommentSection2B, @CommentSection2C, " +
                "@CommentSection3A, @CommentSection3B, @CommentSection3C, @CommentSection3D, @CommentSection3E,@CommentSection3F, " +
                "@DaysSick, @DaysTardy, @primeComments, @NextEvaluationDate, " +
                "@EmployeesCreativeContribution, @EmployeesNewSkills, @EmployeesStrength, " +
                "@EmployeesImprovement, @EmployeesChanges, @EmployeesPersonalGoals, " +
                "@EmployeesRecommendation);" +
                "SELECT SCOPE_IDENTITY()";

            conn = new SqlConnection();
            conn.ConnectionString = WebConfigurationManager.ConnectionStrings["dbAMS"].ConnectionString;

            using (comm = new SqlCommand(strSql, conn))
            {
                conn.Open();
                comm.Parameters.AddWithValue("@UserId", UserId);
                comm.Parameters.AddWithValue("@EvaluationType", EvaluationType);
                comm.Parameters.AddWithValue("@EvaluatedById", Guid.Empty);
                comm.Parameters.AddWithValue("@DateEvaluated", "");
                comm.Parameters.AddWithValue("@ApprovedByManagerId", Guid.Empty);
                comm.Parameters.AddWithValue("@ApprovedByHRId", Guid.Empty);
                comm.Parameters.AddWithValue("@Agency", agency);
                comm.Parameters.AddWithValue("@Section1A", section1A);
                comm.Parameters.AddWithValue("@Section1B", section1B);
                comm.Parameters.AddWithValue("@Section1C", section1C);
                comm.Parameters.AddWithValue("@Section2A", section2A);
                comm.Parameters.AddWithValue("@Section2B", section2B);
                comm.Parameters.AddWithValue("@Section2C", section2C);
                comm.Parameters.AddWithValue("@Section3A", section3A);
                comm.Parameters.AddWithValue("@Section3B", section3B);
                comm.Parameters.AddWithValue("@Section3C", section3C);
                comm.Parameters.AddWithValue("@Section3D", section3D);
                comm.Parameters.AddWithValue("@Section3E", section3E);
                comm.Parameters.AddWithValue("@Section3F", section3F);
                comm.Parameters.AddWithValue("@CommentSection1A", commentSection1A);
                comm.Parameters.AddWithValue("@CommentSection1B", commentSection1B);
                comm.Parameters.AddWithValue("@CommentSection1C", commentSection1C);
                comm.Parameters.AddWithValue("@CommentSection2A", commentSection2A);
                comm.Parameters.AddWithValue("@CommentSection2B", commentSection2B);
                comm.Parameters.AddWithValue("@CommentSection2C", commentSection2C);
                comm.Parameters.AddWithValue("@CommentSection3A", commentSection3A);
                comm.Parameters.AddWithValue("@CommentSection3B", commentSection3B);
                comm.Parameters.AddWithValue("@CommentSection3C", commentSection3C);
                comm.Parameters.AddWithValue("@CommentSection3D", commentSection3D);
                comm.Parameters.AddWithValue("@CommentSection3E", commentSection3E);
                comm.Parameters.AddWithValue("@CommentSection3F", commentSection3F);
                comm.Parameters.AddWithValue("@DaysSick", daysSick);
                comm.Parameters.AddWithValue("@DaysTardy", daysTardy);
                comm.Parameters.AddWithValue("@primeComments", primeComment);
                comm.Parameters.AddWithValue("@NextEvaluationDate", next_eval_date);
                comm.Parameters.AddWithValue("@EmployeesCreativeContribution", employeescreativeContribution);
                comm.Parameters.AddWithValue("@EmployeesNewSkills", employeesnewSkills);
                comm.Parameters.AddWithValue("@EmployeesStrength", empployeesStrength);
                comm.Parameters.AddWithValue("@EmployeesImprovement", employeesImprovement);
                comm.Parameters.AddWithValue("@EmployeesChanges", employeesChanges);
                comm.Parameters.AddWithValue("@EmployeesPersonalGoals", employeesPersonalGoals);
                comm.Parameters.AddWithValue("@EmployeesRecommendation", employeesRecommendation);

                object exScalar = comm.ExecuteScalar();
                _newlyInsertedId = (exScalar == null ? -1 : Convert.ToInt32(exScalar.ToString()));
                conn.Close();
            }
            comm.Dispose();
            conn.Dispose();

            return _newlyInsertedId;
        }

        public void UpdateEvaluation_Prime(
            Guid EvaluatedById,
            Guid ApprovedByManagerId,
            Guid ApprovedByHRId,
            string commentSection1A,
            string commentSection1B,
            string commentSection1C,
            string commentSection2A,
            string commentSection2B,
            string commentSection2C,
            string commentSection3A,
            string commentSection3B,
            string commentSection3C,
            string commentSection3D,
            string commentSection3E,
            string commentSection3F,
            decimal section1A,
            decimal section1B,
            decimal section1C,
            decimal section2A,
            decimal section2B,
            decimal section2C,
            decimal section3A,
            decimal section3B,
            decimal section3C,
            decimal section3D,
            decimal section3E,
            decimal section3F,
            string daysSick,
            string daysTardy,
            string primeComment,
            string next_eval_date,
            string employeescreativeContribution,
            string employeesnewSkills,
            string empployeesStrength,
            string employeesImprovement,
            string employeesChanges,
            string employeesPersonalGoals,
            string employeesRecommendation,
            int evaluationId
            )
        {
            strSql = "UPDATE Evaluation SET " +
                "EvaluatedById = @EvaluatedById, " +
                "DateEvaluated = @DateEvaluated, " +
                "ApprovedByManagerId=@ApprovedByManagerId, " +
                "ApprovedByHRId=@ApprovedByHRId, " +
                "EmployeesCreativeContribution=@EmployeesCreativeContribution, " +
                "EmployeesNewSkills=@EmployeesNewSkills, " +
                "EmployeesStrength=@EmployeesStrength, " +
                "EmployeesImprovement=@EmployeesImprovement, " +
                "EmployeesChanges=@EmployeesChanges, " +
                "EmployeesPersonalGoals=@EmployeesPersonalGoals, " +
                "EmployeesRecommendation=@EmployeesRecommendation, " +
                "Section1A=@Section1A, Section1B=@Section1B, Section1C=@Section1C, " +
                "Section2A=@Section2A, Section2B=@Section2B, Section2C=@Section2C, " +
                "Section3A=@Section3A, Section3B=@Section3B, Section3C=@Section3C, Section3D=@Section3D, Section3E=@Section3E, Section3F=@Section3F, " +
                "CommentSection1A=@CommentSection1A, " +
                "CommentSection1B=@CommentSection1B, " +
                "CommentSection1C=@CommentSection1C, " +
                "CommentSection2A=@CommentSection2A, " +
                "CommentSection2B=@CommentSection2B, " +
                "CommentSection2C=@CommentSection2C, " +
                "CommentSection3A=@CommentSection3A, CommentSection3B=@CommentSection3B, CommentSection3C=@CommentSection3C, " +
                "CommentSection3D=@CommentSection3D, CommentSection3E=@CommentSection3E, CommentSection3F=@CommentSection3F, " +
                "DaysSick=@DaysSick, DaysTardy=@DaysTardy, primeComments=@primeComments, " +
                "NextEvaluationDate=@NextEvaluationDate " +
                "WHERE Id = @Id";
            conn = new SqlConnection();
            conn.ConnectionString = WebConfigurationManager.ConnectionStrings["dbAMS"].ConnectionString;

            using (comm = new SqlCommand(strSql, conn))
            {
                conn.Open();
                comm.Parameters.AddWithValue("@EvaluatedById", EvaluatedById);
                comm.Parameters.AddWithValue("@DateEvaluated", DateTime.Now.ToShortDateString());
                comm.Parameters.AddWithValue("@ApprovedByManagerId", ApprovedByManagerId);
                comm.Parameters.AddWithValue("@ApprovedByHRId", ApprovedByHRId);
                comm.Parameters.AddWithValue("@Section1A", section1A);
                comm.Parameters.AddWithValue("@Section1B", section1B);
                comm.Parameters.AddWithValue("@Section1C", section1C);
                comm.Parameters.AddWithValue("@Section2A", section2A);
                comm.Parameters.AddWithValue("@Section2B", section2B);
                comm.Parameters.AddWithValue("@Section2C", section2C);
                comm.Parameters.AddWithValue("@Section3A", section3A);
                comm.Parameters.AddWithValue("@Section3B", section3B);
                comm.Parameters.AddWithValue("@Section3C", section3C);
                comm.Parameters.AddWithValue("@Section3D", section3D);
                comm.Parameters.AddWithValue("@Section3E", section3E);
                comm.Parameters.AddWithValue("@Section3F", section3F);
                comm.Parameters.AddWithValue("@CommentSection1A", commentSection1A);
                comm.Parameters.AddWithValue("@CommentSection1B", commentSection1B);
                comm.Parameters.AddWithValue("@CommentSection1C", commentSection1C);
                comm.Parameters.AddWithValue("@CommentSection2A", commentSection2A);
                comm.Parameters.AddWithValue("@CommentSection2B", commentSection2B);
                comm.Parameters.AddWithValue("@CommentSection2C", commentSection2C);
                comm.Parameters.AddWithValue("@CommentSection3A", commentSection3A);
                comm.Parameters.AddWithValue("@CommentSection3B", commentSection3B);
                comm.Parameters.AddWithValue("@CommentSection3C", commentSection3C);
                comm.Parameters.AddWithValue("@CommentSection3D", commentSection3D);
                comm.Parameters.AddWithValue("@CommentSection3E", commentSection3E);
                comm.Parameters.AddWithValue("@CommentSection3F", commentSection3F);
                comm.Parameters.AddWithValue("@DaysSick", daysSick);
                comm.Parameters.AddWithValue("@DaysTardy", daysTardy);
                comm.Parameters.AddWithValue("@primeComments", primeComment);
                comm.Parameters.AddWithValue("@NextEvaluationDate", next_eval_date);
                comm.Parameters.AddWithValue("@EmployeesCreativeContribution", employeescreativeContribution);
                comm.Parameters.AddWithValue("@EmployeesNewSkills", employeesnewSkills);
                comm.Parameters.AddWithValue("@EmployeesStrength", empployeesStrength);
                comm.Parameters.AddWithValue("@EmployeesImprovement", employeesImprovement);
                comm.Parameters.AddWithValue("@EmployeesChanges", employeesChanges);
                comm.Parameters.AddWithValue("@EmployeesPersonalGoals", employeesPersonalGoals);
                comm.Parameters.AddWithValue("@EmployeesRecommendation", employeesRecommendation);
                comm.Parameters.AddWithValue("@Id", evaluationId);
                comm.ExecuteNonQuery();
                conn.Close();
            }
            comm.Dispose();
            conn.Dispose();
        }

        //for Prime only
        public void addEvaluation_Scores_Prime_Evaluator(
            int evaluationId,
            int competencyCatQId,
            decimal evaluatorRating)
        {
            strSql = "INSERT INTO Evaluation_Score(EvaluationId, CompetenceCatQId, EvaluatorRating) " +
                "VALUES(@Agency, @CompetenceCatQId, @EvaluatorRating)";

            conn = new SqlConnection();
            conn.ConnectionString = WebConfigurationManager.ConnectionStrings["dbAMS"].ConnectionString;

            using (comm = new SqlCommand(strSql, conn))
            {
                conn.Open();
                comm.Parameters.AddWithValue("@Agency", evaluationId);
                comm.Parameters.AddWithValue("@CompetenceCatQId", competencyCatQId);
                comm.Parameters.AddWithValue("@EvaluatorRating", evaluatorRating);
                comm.ExecuteNonQuery();
                conn.Close();
            }
            comm.Dispose();
            conn.Dispose();
        }

        //staff
        public void addEvaluation_Scores_Prime_Staff(
            int evaluationId,
            int competencyCatQId,
            decimal evaluatorRating)
        {
            strSql = "INSERT INTO Evaluation_Score(EvaluationId, CompetenceCatQId, StaffRating) " +
                "VALUES(@Agency, @CompetenceCatQId, @StaffRating)";

            conn = new SqlConnection();
            conn.ConnectionString = WebConfigurationManager.ConnectionStrings["dbAMS"].ConnectionString;

            using (comm = new SqlCommand(strSql, conn))
            {
                conn.Open();
                comm.Parameters.AddWithValue("@Agency", evaluationId);
                comm.Parameters.AddWithValue("@CompetenceCatQId", competencyCatQId);
                comm.Parameters.AddWithValue("@StaffRating", evaluatorRating);
                comm.ExecuteNonQuery();
                conn.Close();
            }
            comm.Dispose();
            conn.Dispose();
        }

        public void updateEvaluation_Scores_Prime_Evaluator(
            int eva_score_id,
            decimal rating)
        {
            strSql = "UPDATE Evaluation_Score SET EvaluatorRating=@Rating WHERE " +
                "Id = @Id";

            conn = new SqlConnection();
            conn.ConnectionString = WebConfigurationManager.ConnectionStrings["dbAMS"].ConnectionString;

            using (comm = new SqlCommand(strSql, conn))
            {
                conn.Open();
                comm.Parameters.AddWithValue("@Rating", rating);
                comm.Parameters.AddWithValue("@Id", eva_score_id);
                comm.ExecuteNonQuery();
                conn.Close();
            }
            comm.Dispose();
            conn.Dispose();
        }

        public void updateEvaluation_Scores_Prime_Staff(
            int eva_score_id,
            decimal rating)
        {
            strSql = "UPDATE Evaluation_Score SET StaffRating=@Rating WHERE " +
                "Id = @Id";

            conn = new SqlConnection();
            conn.ConnectionString = WebConfigurationManager.ConnectionStrings["dbAMS"].ConnectionString;

            using (comm = new SqlCommand(strSql, conn))
            {
                conn.Open();
                comm.Parameters.AddWithValue("@Rating", rating);
                comm.Parameters.AddWithValue("@Id", eva_score_id);
                comm.ExecuteNonQuery();
                conn.Close();
            }
            comm.Dispose();
            conn.Dispose();
        }

        //filled gridview
        public DataTable getCooperation_filled(int evaluationId)
        {
            //11 -> Cooperation
            //strSql = "SELECT Id, Question FROM CompetenceCatQ WHERE CompetenceCatId = 11";
            strSql = "SELECT Evaluation_Score.Id, " +
                "CompetenceCatQ.Question, Evaluation_Score.StaffRating, Evaluation_Score.EvaluatorRating " +
                "FROM Evaluation_Score, CompetenceCatQ " +
                "WHERE Evaluation_Score.CompetenceCatQId = CompetenceCatQ.Id AND " +
                "CompetenceCatQ.CompetenceCatId = 11 AND " +
                "Evaluation_Score.EvaluationId = @Id";

            conn = new SqlConnection();
            conn.ConnectionString = WebConfigurationManager.ConnectionStrings["dbAMS"].ConnectionString;
            comm = new SqlCommand(strSql, conn);
            comm.Parameters.AddWithValue("@Id", evaluationId);
            dt = new DataTable();
            adp = new SqlDataAdapter(comm);

            conn.Open();
            adp.Fill(dt);
            conn.Close();

            return dt;
        }

        public DataTable getAttendanceAndPunctuality_filled(int evaluationId)
        {
            //11 -> Attendance and Punctuality
            strSql = "SELECT Evaluation_Score.Id, " +
                "CompetenceCatQ.Question, Evaluation_Score.StaffRating, Evaluation_Score.EvaluatorRating " +
                "FROM Evaluation_Score, CompetenceCatQ " +
                "WHERE Evaluation_Score.CompetenceCatQId = CompetenceCatQ.Id AND " +
                "CompetenceCatQ.CompetenceCatId = 12 AND " +
                "Evaluation_Score.EvaluationId = @Id";

            conn = new SqlConnection();
            conn.ConnectionString = WebConfigurationManager.ConnectionStrings["dbAMS"].ConnectionString;
            comm = new SqlCommand(strSql, conn);
            comm.Parameters.AddWithValue("@Id", evaluationId);
            dt = new DataTable();
            adp = new SqlDataAdapter(comm);

            conn.Open();
            adp.Fill(dt);
            conn.Close();

            return dt;
        }


        public DataTable getInterpersonalRelationship_filled(int evaluationId)
        {
            //11 -> Interpersonal Relationship
            strSql = "SELECT Evaluation_Score.Id, " +
                "CompetenceCatQ.Question, Evaluation_Score.StaffRating, Evaluation_Score.EvaluatorRating " +
                "FROM Evaluation_Score, CompetenceCatQ " +
                "WHERE Evaluation_Score.CompetenceCatQId = CompetenceCatQ.Id AND " +
                "CompetenceCatQ.CompetenceCatId = 13 AND " +
                "Evaluation_Score.EvaluationId = @Id";

            conn = new SqlConnection();
            conn.ConnectionString = WebConfigurationManager.ConnectionStrings["dbAMS"].ConnectionString;
            comm = new SqlCommand(strSql, conn);
            comm.Parameters.AddWithValue("@Id", evaluationId);
            dt = new DataTable();
            adp = new SqlDataAdapter(comm);

            conn.Open();
            adp.Fill(dt);
            conn.Close();

            return dt;
        }

        public DataTable getAttitude_filled(int evaluationId)
        {
            //11 -> Attitude
            strSql = "SELECT Evaluation_Score.Id, " +
                "CompetenceCatQ.Question, Evaluation_Score.StaffRating, Evaluation_Score.EvaluatorRating " +
                "FROM Evaluation_Score, CompetenceCatQ " +
                "WHERE Evaluation_Score.CompetenceCatQId = CompetenceCatQ.Id AND " +
                "CompetenceCatQ.CompetenceCatId = 14 AND " +
                "Evaluation_Score.EvaluationId = @Id";

            conn = new SqlConnection();
            conn.ConnectionString = WebConfigurationManager.ConnectionStrings["dbAMS"].ConnectionString;
            comm = new SqlCommand(strSql, conn);
            comm.Parameters.AddWithValue("@Id", evaluationId);
            dt = new DataTable();
            adp = new SqlDataAdapter(comm);

            conn.Open();
            adp.Fill(dt);
            conn.Close();

            return dt;
        }

        public DataTable getIniatitve_filled(int evaluationId)
        {
            //11 -> Initiative
            strSql = "SELECT Evaluation_Score.Id, " +
                "CompetenceCatQ.Question, Evaluation_Score.StaffRating, Evaluation_Score.EvaluatorRating " +
                "FROM Evaluation_Score, CompetenceCatQ " +
                "WHERE Evaluation_Score.CompetenceCatQId = CompetenceCatQ.Id AND " +
                "CompetenceCatQ.CompetenceCatId = 15 AND " +
                "Evaluation_Score.EvaluationId = @Id";

            conn = new SqlConnection();
            conn.ConnectionString = WebConfigurationManager.ConnectionStrings["dbAMS"].ConnectionString;
            comm = new SqlCommand(strSql, conn);
            comm.Parameters.AddWithValue("@Id", evaluationId);
            dt = new DataTable();
            adp = new SqlDataAdapter(comm);

            conn.Open();
            adp.Fill(dt);
            conn.Close();

            return dt;
        }

        public DataTable getJudgement_filled(int evaluationId)
        {
            //11 -> Initiative
            strSql = "SELECT Evaluation_Score.Id, " +
                "CompetenceCatQ.Question, Evaluation_Score.StaffRating, Evaluation_Score.EvaluatorRating " +
                "FROM Evaluation_Score, CompetenceCatQ " +
                "WHERE Evaluation_Score.CompetenceCatQId = CompetenceCatQ.Id AND " +
                "CompetenceCatQ.CompetenceCatId = 16 AND " +
                "Evaluation_Score.EvaluationId = @Id";

            conn = new SqlConnection();
            conn.ConnectionString = WebConfigurationManager.ConnectionStrings["dbAMS"].ConnectionString;
            comm = new SqlCommand(strSql, conn);
            comm.Parameters.AddWithValue("@Id", evaluationId);
            dt = new DataTable();
            adp = new SqlDataAdapter(comm);

            conn.Open();
            adp.Fill(dt);
            conn.Close();

            return dt;
        }

        public DataTable getCommunication_filled(int evaluationId)
        {
            //11 -> Initiative
            strSql = "SELECT Evaluation_Score.Id, " +
                "CompetenceCatQ.Question, Evaluation_Score.StaffRating, Evaluation_Score.EvaluatorRating " +
                "FROM Evaluation_Score, CompetenceCatQ " +
                "WHERE Evaluation_Score.CompetenceCatQId = CompetenceCatQ.Id AND " +
                "CompetenceCatQ.CompetenceCatId = 17 AND " +
                "Evaluation_Score.EvaluationId = @Id";

            conn = new SqlConnection();
            conn.ConnectionString = WebConfigurationManager.ConnectionStrings["dbAMS"].ConnectionString;
            comm = new SqlCommand(strSql, conn);
            comm.Parameters.AddWithValue("@Id", evaluationId);
            dt = new DataTable();
            adp = new SqlDataAdapter(comm);

            conn.Open();
            adp.Fill(dt);
            conn.Close();

            return dt;
        }

        public DataTable getSafety_filled(int evaluationId)
        {
            //11 -> Initiative
            strSql = "SELECT Evaluation_Score.Id, " +
                "CompetenceCatQ.Question, Evaluation_Score.StaffRating, Evaluation_Score.EvaluatorRating " +
                "FROM Evaluation_Score, CompetenceCatQ " +
                "WHERE Evaluation_Score.CompetenceCatQId = CompetenceCatQ.Id AND " +
                "CompetenceCatQ.CompetenceCatId = 18 AND " +
                "Evaluation_Score.EvaluationId = @Id";

            conn = new SqlConnection();
            conn.ConnectionString = WebConfigurationManager.ConnectionStrings["dbAMS"].ConnectionString;
            comm = new SqlCommand(strSql, conn);
            comm.Parameters.AddWithValue("@Id", evaluationId);
            dt = new DataTable();
            adp = new SqlDataAdapter(comm);

            conn.Open();
            adp.Fill(dt);
            conn.Close();

            return dt;
        }

        public DataTable getDependability_filled(int evaluationId)
        {
            //11 -> Initiative
            strSql = "SELECT Evaluation_Score.Id, " +
                "CompetenceCatQ.Question, Evaluation_Score.StaffRating, Evaluation_Score.EvaluatorRating " +
                "FROM Evaluation_Score, CompetenceCatQ " +
                "WHERE Evaluation_Score.CompetenceCatQId = CompetenceCatQ.Id AND " +
                "CompetenceCatQ.CompetenceCatId = 19 AND " +
                "Evaluation_Score.EvaluationId = @Id";

            conn = new SqlConnection();
            conn.ConnectionString = WebConfigurationManager.ConnectionStrings["dbAMS"].ConnectionString;
            comm = new SqlCommand(strSql, conn);
            comm.Parameters.AddWithValue("@Id", evaluationId);
            dt = new DataTable();
            adp = new SqlDataAdapter(comm);

            conn.Open();
            adp.Fill(dt);
            conn.Close();

            return dt;
        }

        public DataTable getSecificJobSkills_filled(int evaluationId)
        {
            //11 -> Initiative
            strSql = "SELECT Evaluation_Score.Id, " +
                "CompetenceCatQ.Question, Evaluation_Score.StaffRating, Evaluation_Score.EvaluatorRating " +
                "FROM Evaluation_Score, CompetenceCatQ " +
                "WHERE Evaluation_Score.CompetenceCatQId = CompetenceCatQ.Id AND " +
                "CompetenceCatQ.CompetenceCatId = 20 AND " +
                "Evaluation_Score.EvaluationId = @Id";

            conn = new SqlConnection();
            conn.ConnectionString = WebConfigurationManager.ConnectionStrings["dbAMS"].ConnectionString;
            comm = new SqlCommand(strSql, conn);
            comm.Parameters.AddWithValue("@Id", evaluationId);
            dt = new DataTable();
            adp = new SqlDataAdapter(comm);

            conn.Open();
            adp.Fill(dt);
            conn.Close();

            return dt;
        }

        public DataTable getProductivity_filled(int evaluationId)
        {
            //11 -> Initiative
            strSql = "SELECT Evaluation_Score.Id, " +
                "CompetenceCatQ.Question, Evaluation_Score.StaffRating, Evaluation_Score.EvaluatorRating " +
                "FROM Evaluation_Score, CompetenceCatQ " +
                "WHERE Evaluation_Score.CompetenceCatQId = CompetenceCatQ.Id AND " +
                "CompetenceCatQ.CompetenceCatId = 21 AND " +
                "Evaluation_Score.EvaluationId = @Id";

            conn = new SqlConnection();
            conn.ConnectionString = WebConfigurationManager.ConnectionStrings["dbAMS"].ConnectionString;
            comm = new SqlCommand(strSql, conn);
            comm.Parameters.AddWithValue("@Id", evaluationId);
            dt = new DataTable();
            adp = new SqlDataAdapter(comm);

            conn.Open();
            adp.Fill(dt);
            conn.Close();

            return dt;
        }

        public DataTable getOrganizationalSkill_filled(int evaluationId)
        {
            //11 -> Initiative
            strSql = "SELECT Evaluation_Score.Id, " +
                "CompetenceCatQ.Question, Evaluation_Score.StaffRating, Evaluation_Score.EvaluatorRating " +
                "FROM Evaluation_Score, CompetenceCatQ " +
                "WHERE Evaluation_Score.CompetenceCatQId = CompetenceCatQ.Id AND " +
                "CompetenceCatQ.CompetenceCatId = 22 AND " +
                "Evaluation_Score.EvaluationId = @Id";

            conn = new SqlConnection();
            conn.ConnectionString = WebConfigurationManager.ConnectionStrings["dbAMS"].ConnectionString;
            comm = new SqlCommand(strSql, conn);
            comm.Parameters.AddWithValue("@Id", evaluationId);
            dt = new DataTable();
            adp = new SqlDataAdapter(comm);

            conn.Open();
            adp.Fill(dt);
            conn.Close();

            return dt;
        }
        #endregion

        #region Approval
        public DataTable GetEvaluated(int evaluationId)
        {
            strSql = "SELECT * FROM Evaluation WHERE Id = @Id";

            conn = new SqlConnection();
            conn.ConnectionString = WebConfigurationManager.ConnectionStrings["dbAMS"].ConnectionString;
            comm = new SqlCommand(strSql, conn);
            comm.Parameters.AddWithValue("@Id", evaluationId);
            dt = new DataTable();
            adp = new SqlDataAdapter(comm);

            conn.Open();
            adp.Fill(dt);
            conn.Close();

            return dt;
        }

        //Pending Approval List by HR
        //all roles -> approvals
        public DataTable GetPendingApprovalHR()
        {
            strSql = "SELECT Evaluation.Id,EMPLOYEE.LastName + ', ' + EMPLOYEE.FirstName + ' ' + EMPLOYEE.MiddleName AS [FullName], " +
                "Evaluation.DateEvaluated, " +
                "Evaluation.ApprovedByHRId, " +
                "Evaluation.ApprovedByManagerId " +
                "FROM EMPLOYEE " +
                "INNER JOIN Evaluation " +
                "ON EMPLOYEE.UserId = Evaluation.UserId " +
                "WHERE " +
                "Evaluation.ApprovedByHRId = @HRId";

            conn = new SqlConnection();
            conn.ConnectionString = WebConfigurationManager.ConnectionStrings["dbAMS"].ConnectionString;
            comm = new SqlCommand(strSql, conn);
            comm.Parameters.AddWithValue("@HRId", Guid.Empty);
            dt = new DataTable();
            adp = new SqlDataAdapter(comm);

            conn.Open();
            adp.Fill(dt);
            conn.Close();

            return dt;
        }

        //Pending Approval List by GM ->only managers and HR
        //GM the signatory of Managers/HR only
        //disregard HR assistant since it has a role of HR but limited
        public DataTable GetPendingApprovalGM()
        {
            strSql = "SELECT Evaluation.Id,(EMPLOYEE.LastName + ', ' + EMPLOYEE.FirstName + ' ' + EMPLOYEE.MiddleName) AS [FullName], " +
                "Evaluation.DateEvaluated, " +
                "Evaluation.ApprovedByHRId, " +
                "Evaluation.ApprovedByManagerId " +
                "FROM EMPLOYEE INNER JOIN Evaluation ON EMPLOYEE.UserId = Evaluation.UserId " +
                "INNER JOIN UsersInRoles ON Evaluation.UserId = UsersInRoles.UserId " +
                "INNER JOIN Roles ON UsersInRoles.RoleId = Roles.RoleId " +
                "WHERE " +
                "(Roles.RoleName = 'Manager' OR Roles.RoleName = 'HR') AND " +
                "POSITION.Position != 'HR Assistant' AND " +
                "Evaluation.ApprovedByManagerId = @ManagerId";
            conn = new SqlConnection();
            conn.ConnectionString = WebConfigurationManager.ConnectionStrings["dbAMS"].ConnectionString;
            comm = new SqlCommand(strSql, conn);
            comm.Parameters.AddWithValue("@ManagerId", Guid.Empty);
            dt = new DataTable();
            adp = new SqlDataAdapter(comm);

            conn.Open();
            adp.Fill(dt);
            conn.Close();

            return dt;
        }

        //Pending Approval List by Manager ->
        public DataTable GetPendingApprovalManager(string deptId)
        {
            strSql = strSql = "SELECT Evaluation.Id,(EMPLOYEE.LastName + ', ' + EMPLOYEE.FirstName + ' ' + EMPLOYEE.MiddleName) AS [FullName], " +
                "Evaluation.DateEvaluated, " +
                "Evaluation.ApprovedByHRId, " +
                "Evaluation.ApprovedByManagerId " +
                "FROM EMPLOYEE INNER JOIN Evaluation ON EMPLOYEE.UserId = Evaluation.UserId " +
                "INNER JOIN UsersInRoles ON Evaluation.UserId = UsersInRoles.UserId " +
                "INNER JOIN Roles ON UsersInRoles.RoleId = Roles.RoleId " +
                "INNER JOIN POSITION ON EMPLOYEE.PositionId = POSITION.Id " +
                "INNER JOIN DEPARTMENT ON POSITION.DepartmentId = DEPARTMENT.Id " +
                "WHERE " +
                "(Roles.RoleName = 'Supervisor' OR Roles.RoleName = 'Staff') AND " +
                "DEPARTMENT.Id = @DepartmentId AND " +
                "Evaluation.ApprovedByManagerId = @ManagerId";

            conn = new SqlConnection();
            conn.ConnectionString = WebConfigurationManager.ConnectionStrings["dbAMS"].ConnectionString;
            comm = new SqlCommand(strSql, conn);
            comm.Parameters.AddWithValue("@DepartmentId", deptId);
            comm.Parameters.AddWithValue("@ManagerId", Guid.Empty);
            dt = new DataTable();
            adp = new SqlDataAdapter(comm);

            conn.Open();
            adp.Fill(dt);
            conn.Close();

            return dt;
        }

        //Pending Approval List by Supervisor
        public DataTable GetPendingApprovalSupervisor(string deptId)
        {
            strSql = strSql = "SELECT Evaluation.Id,(EMPLOYEE.LastName + ', ' + EMPLOYEE.FirstName + ' ' + EMPLOYEE.MiddleName) AS [FullName], " +
                "Evaluation.DateEvaluated, " +
                "Evaluation.ApprovedByHRId, " +
                "Evaluation.ApprovedByManagerId " +
                "FROM EMPLOYEE INNER JOIN Evaluation ON EMPLOYEE.UserId = Evaluation.UserId " +
                "INNER JOIN UsersInRoles ON Evaluation.UserId = UsersInRoles.UserId " +
                "INNER JOIN Roles ON UsersInRoles.RoleId = Roles.RoleId " +
                "INNER JOIN POSITION ON EMPLOYEE.PositionId = POSITION.Id " +
                "INNER JOIN DEPARTMENT ON POSITION.DepartmentId = DEPARTMENT.Id " +
                "WHERE " +
                "(Roles.RoleName = 'Staff') AND " +
                "DEPARTMENT.Id = @DepartmentId AND " +
                "Evaluation.ApprovedByManagerId = @ManagerId";

            conn = new SqlConnection();
            conn.ConnectionString = WebConfigurationManager.ConnectionStrings["dbAMS"].ConnectionString;
            comm = new SqlCommand(strSql, conn);
            comm.Parameters.AddWithValue("@DepartmentId", deptId);
            comm.Parameters.AddWithValue("@ManagerId", Guid.Empty);
            dt = new DataTable();
            adp = new SqlDataAdapter(comm);

            conn.Open();
            adp.Fill(dt);
            conn.Close();

            return dt;
        }

        //Approval by HR
        public void ApprovePendingApprovalHR(string evaluationId, Guid signatory)
        {
            strSql = "UPDATE Evaluation SET ApprovedByHRId = @ApprovedByHRId WHERE Id = @Id";

            conn = new SqlConnection();
            conn.ConnectionString = WebConfigurationManager.ConnectionStrings["dbAMS"].ConnectionString;
            comm = new SqlCommand(strSql, conn);
            comm.Parameters.AddWithValue("@ApprovedByHRId", signatory);
            comm.Parameters.AddWithValue("@Id", evaluationId);
            conn.Open();
            comm.ExecuteNonQuery();
            comm.Dispose();
            conn.Close();
        }
        public void ApprovePendingApprovalManager(string evaluationId, Guid signatory)
        {
            strSql = "UPDATE Evaluation SET ApprovedByManagerId = @ApprovedByManagerId WHERE Id = @Id";

            conn = new SqlConnection();
            conn.ConnectionString = WebConfigurationManager.ConnectionStrings["dbAMS"].ConnectionString;
            comm = new SqlCommand(strSql, conn);
            comm.Parameters.AddWithValue("@ApprovedByManagerId", signatory);
            comm.Parameters.AddWithValue("@Id", evaluationId);
            conn.Open();
            comm.ExecuteNonQuery();
            comm.Dispose();
            conn.Close();
        }
        #endregion

        #region SELF EVALUATION
        ////////////////////////////////////////////////////////////////////////////////
        /////////////////////////////// SELF EVALUATION ////////////////////////////////
        ////////////////////////////////////////////////////////////////////////////////

        //can self evaluate by manager -> supervisor and staff, same dept
        public DataTable DisplaySelfEvaluation_Manager(string strSearch, string deptId, Guid UserId)
        {
            strSql = "SELECT EMPLOYEE.UserId, EMPLOYEE.Emp_Id, " +
                "(EMPLOYEE.LastName + ', ' + EMPLOYEE.FirstName + ' ' + EMPLOYEE.MiddleName) AS FullName, " +
                "POSITION.Position AS [POSITION], DEPARTMENT.Department AS [DEPARTMENT] " +
                "FROM Memberships, EMPLOYEE, POSITION, DEPARTMENT, UsersInRoles, Roles WHERE " +
                "Memberships.UserId = EMPLOYEE.UserId AND " +
                "EMPLOYEE.PositionId = POSITION.Id AND " +
                "POSITION.DepartmentId = DEPARTMENT.Id AND " +
                "DEPARTMENT.Id = @DepartmentId AND " +
                "EMPLOYEE.UserId = UsersInRoles.UserId AND " +
                "UsersInRoles.RoleId = Roles.RoleId AND " +
                "(Roles.RoleName = 'Staff' OR Roles.RoleName = 'Supervisor') AND " +
                "EMPLOYEE.UserId != @UserId AND " +
                "(EMPLOYEE.Emp_Id LIKE '%' + @searchKeyWord + '%' " +
                "OR EMPLOYEE.FirstName LIKE '%' + @searchKeyWord + '%' " +
                "OR EMPLOYEE.MiddleName LIKE '%' + @searchKeyWord + '%' " +
                "OR EMPLOYEE.LastName LIKE '%' + @searchKeyWord + '%' " +
                "OR POSITION.Position LIKE '%' + @searchKeyWord + '%' " +
                "OR DEPARTMENT.Department LIKE '%' + @searchKeyWord + '%' ) " +
                "AND EMPLOYEE.AccountStatusId = 1 " +
                "ORDER BY Employee.Emp_Id ASC";

            conn = new SqlConnection();
            conn.ConnectionString = WebConfigurationManager.ConnectionStrings["dbAMS"].ConnectionString;
            comm = new SqlCommand(strSql, conn);
            comm.Parameters.AddWithValue("@searchKeyWord", strSearch);
            comm.Parameters.AddWithValue("@DepartmentId", deptId);
            comm.Parameters.AddWithValue("@UserId", UserId);
            dt = new DataTable();
            adp = new SqlDataAdapter(comm);

            conn.Open();
            adp.Fill(dt);
            conn.Close();

            return dt;
        }

        public DataTable DisplaySelfEvaluation_Supervisor(string strSearch, string deptId, Guid UserId)
        {
            strSql = "SELECT EMPLOYEE.UserId, EMPLOYEE.Emp_Id, " +
                "(EMPLOYEE.LastName + ', ' + EMPLOYEE.FirstName + ' ' + EMPLOYEE.MiddleName) AS FullName, " +
                "POSITION.Position AS [POSITION], DEPARTMENT.Department AS [DEPARTMENT] " +
                "FROM Memberships, EMPLOYEE, POSITION, DEPARTMENT, UsersInRoles, Roles WHERE " +
                "Memberships.UserId = EMPLOYEE.UserId AND " +
                "EMPLOYEE.PositionId = POSITION.Id AND " +
                "POSITION.DepartmentId = DEPARTMENT.Id AND " +
                "DEPARTMENT.Id = @DepartmentId AND " +
                "EMPLOYEE.UserId = UsersInRoles.UserId AND " +
                "UsersInRoles.RoleId = Roles.RoleId AND " +
                "(Roles.RoleName = 'Staff') AND " +
                "EMPLOYEE.UserId != @UserId AND " +
                "(EMPLOYEE.Emp_Id LIKE '%' + @searchKeyWord + '%' " +
                "OR EMPLOYEE.FirstName LIKE '%' + @searchKeyWord + '%' " +
                "OR EMPLOYEE.MiddleName LIKE '%' + @searchKeyWord + '%' " +
                "OR EMPLOYEE.LastName LIKE '%' + @searchKeyWord + '%' " +
                "OR POSITION.Position LIKE '%' + @searchKeyWord + '%' " +
                "OR DEPARTMENT.Department LIKE '%' + @searchKeyWord + '%' ) " +
                "AND EMPLOYEE.AccountStatusId = 1 " +
                "ORDER BY Employee.Emp_Id ASC";

            conn = new SqlConnection();
            conn.ConnectionString = WebConfigurationManager.ConnectionStrings["dbAMS"].ConnectionString;
            comm = new SqlCommand(strSql, conn);
            comm.Parameters.AddWithValue("@searchKeyWord", strSearch);
            comm.Parameters.AddWithValue("@DepartmentId", deptId);
            comm.Parameters.AddWithValue("@UserId", UserId);
            dt = new DataTable();
            adp = new SqlDataAdapter(comm);

            conn.Open();
            adp.Fill(dt);
            conn.Close();

            return dt;
        }

        //display can self evaulautby staff
        public DataTable DisplaySelfEvaluation_Staff(string positionId, string strSearch, Guid UserId)
        {
            strSql = "SELECT EMPLOYEE.UserId, EMPLOYEE.Emp_Id, " +
                "(EMPLOYEE.LastName + ', ' + EMPLOYEE.FirstName + ' ' + EMPLOYEE.MiddleName) AS FullName, " +
                "POSITION.Position AS [POSITION], DEPARTMENT.Department AS [DEPARTMENT] " +
                "FROM Memberships, EMPLOYEE, POSITION, DEPARTMENT, UsersInRoles, Roles WHERE " +
                "Memberships.UserId = EMPLOYEE.UserId AND " +
                "EMPLOYEE.PositionId = POSITION.Id AND " +
                "POSITION.DepartmentId = DEPARTMENT.Id AND " +
                "EMPLOYEE.PositionId = @PositionId AND " +
                "EMPLOYEE.UserId = UsersInRoles.UserId AND " +
                "UsersInRoles.RoleId = Roles.RoleId AND " +
                "(Roles.RoleName = 'Staff') AND " +
                "EMPLOYEE.UserId != @UserId AND " + 
                "(EMPLOYEE.Emp_Id LIKE '%' + @searchKeyWord + '%' " +
                "OR EMPLOYEE.FirstName LIKE '%' + @searchKeyWord + '%' " +
                "OR EMPLOYEE.MiddleName LIKE '%' + @searchKeyWord + '%' " +
                "OR EMPLOYEE.LastName LIKE '%' + @searchKeyWord + '%' " +
                "OR POSITION.Position LIKE '%' + @searchKeyWord + '%' " +
                "OR DEPARTMENT.Department LIKE '%' + @searchKeyWord + '%' ) " +
                "AND EMPLOYEE.AccountStatusId = 1 " +
                "ORDER BY Employee.Emp_Id ASC";

            conn = new SqlConnection();
            conn.ConnectionString = WebConfigurationManager.ConnectionStrings["dbAMS"].ConnectionString;
            comm = new SqlCommand(strSql, conn);
            comm.Parameters.AddWithValue("@searchKeyWord", strSearch);
            comm.Parameters.AddWithValue("@PositionId", positionId);
            comm.Parameters.AddWithValue("@UserId", UserId);
            dt = new DataTable();
            adp = new SqlDataAdapter(comm);

            conn.Open();
            adp.Fill(dt);
            conn.Close();

            return dt;
        }


        public DataTable getSelf_SocialSkill()
        {
            //11 -> Initiative
            strSql = "SELECT Id, Question FROM CompetenceCatQ WHERE CompetenceCatId = 23";

            conn = new SqlConnection();
            conn.ConnectionString = WebConfigurationManager.ConnectionStrings["dbAMS"].ConnectionString;
            comm = new SqlCommand(strSql, conn);
            dt = new DataTable();
            adp = new SqlDataAdapter(comm);

            conn.Open();
            adp.Fill(dt);
            conn.Close();

            return dt;
        }

        public DataTable getSelf_CustomerService()
        {
            //11 -> Initiative
            strSql = "SELECT Id, Question FROM CompetenceCatQ WHERE CompetenceCatId = 24";

            conn = new SqlConnection();
            conn.ConnectionString = WebConfigurationManager.ConnectionStrings["dbAMS"].ConnectionString;
            comm = new SqlCommand(strSql, conn);
            dt = new DataTable();
            adp = new SqlDataAdapter(comm);

            conn.Open();
            adp.Fill(dt);
            conn.Close();

            return dt;
        }

        public DataTable getSelf_Originality()
        {
            //11 -> Initiative
            strSql = "SELECT Id, Question FROM CompetenceCatQ WHERE CompetenceCatId = 25";

            conn = new SqlConnection();
            conn.ConnectionString = WebConfigurationManager.ConnectionStrings["dbAMS"].ConnectionString;
            comm = new SqlCommand(strSql, conn);
            dt = new DataTable();
            adp = new SqlDataAdapter(comm);

            conn.Open();
            adp.Fill(dt);
            conn.Close();

            return dt;
        }

        public DataTable getSelf_Responsibility()
        {
            //11 -> Initiative
            strSql = "SELECT Id, Question FROM CompetenceCatQ WHERE CompetenceCatId = 26";

            conn = new SqlConnection();
            conn.ConnectionString = WebConfigurationManager.ConnectionStrings["dbAMS"].ConnectionString;
            comm = new SqlCommand(strSql, conn);
            dt = new DataTable();
            adp = new SqlDataAdapter(comm);

            conn.Open();
            adp.Fill(dt);
            conn.Close();

            return dt;
        }

        public DataTable getSelf_Excellent()
        {
            //11 -> Initiative
            strSql = "SELECT Id, Question FROM CompetenceCatQ WHERE CompetenceCatId = 27";

            conn = new SqlConnection();
            conn.ConnectionString = WebConfigurationManager.ConnectionStrings["dbAMS"].ConnectionString;
            comm = new SqlCommand(strSql, conn);
            dt = new DataTable();
            adp = new SqlDataAdapter(comm);

            conn.Open();
            adp.Fill(dt);
            conn.Close();

            return dt;
        }

        public int insertEvaluation_Self(
            Guid UserId,
            string evaluationType,
            string agency,
            string PeriodCovered
            )
        {
            int _newlyInsertedId = 0;

            strSql = "INSERT INTO Evaluation(UserId,EvaluationType,Agency,PeriodCovered) " +
                "VALUES(@UserId,@EvaluationType,@Agency,@PeriodCovered);" +
                "SELECT SCOPE_IDENTITY()";

            conn = new SqlConnection();
            conn.ConnectionString = WebConfigurationManager.ConnectionStrings["dbAMS"].ConnectionString;

            using (comm = new SqlCommand(strSql, conn))
            {
                conn.Open();
                comm.Parameters.AddWithValue("@UserId", UserId);
                comm.Parameters.AddWithValue("@EvaluationType", evaluationType);
                comm.Parameters.AddWithValue("@Agency", agency);
                comm.Parameters.AddWithValue("@PeriodCovered", PeriodCovered);

                object exScalar = comm.ExecuteScalar();
                _newlyInsertedId = (exScalar == null ? -1 : Convert.ToInt32(exScalar.ToString()));
                conn.Close();
            }
            comm.Dispose();
            conn.Dispose();

            return _newlyInsertedId;
        }

        public void updateEvaluation_Self(
            string agency,
            string PeriodCovered,
            int evaluationId
            )
        {
            strSql = "UPDATE Evaluation SET Agency=@Agency, PeriodCovered=@PeriodCovered WHERE Id=@Id";
            conn = new SqlConnection();
            conn.ConnectionString = WebConfigurationManager.ConnectionStrings["dbAMS"].ConnectionString;

            using (comm = new SqlCommand(strSql, conn))
            {
                conn.Open();
                comm.Parameters.AddWithValue("@Agency", agency);
                comm.Parameters.AddWithValue("@PeriodCovered", PeriodCovered);
                comm.Parameters.AddWithValue("@Id", evaluationId);
                comm.ExecuteNonQuery();
                conn.Close();
            }
            comm.Dispose();
            conn.Dispose();
        }
        
        public void addSelf_Evaluation_Rating(
            int evaluationId,
            int competencyCatQId,
            string rating,
            string remarks)
        {
            strSql = "INSERT INTO Evaluation_Self(EvaluationId, CompetenceCatQId, Rating,Remarks) " +
                "VALUES(@Agency, @CompetenceCatId, @Rating, @Remarks)";

            conn = new SqlConnection();
            conn.ConnectionString = WebConfigurationManager.ConnectionStrings["dbAMS"].ConnectionString;

            using (comm = new SqlCommand(strSql, conn))
            {
                conn.Open();
                comm.Parameters.AddWithValue("@Agency", evaluationId);
                comm.Parameters.AddWithValue("@CompetenceCatId", competencyCatQId);
                comm.Parameters.AddWithValue("@Rating", rating);
                comm.Parameters.AddWithValue("@Remarks", remarks);
                comm.ExecuteNonQuery();
                conn.Close();
            }
            comm.Dispose();
            conn.Dispose();
        }

        public DataTable getSelf_SocialSkill_filled(int evaluationId)
        {
            strSql = "SELECT Evaluation_Self.Id, CompetenceCatQ.Question, " +
                "Evaluation_Self.Rating, Evaluation_Self.Remarks " +
                "FROM Evaluation_Self, CompetenceCatQ " +
                "WHERE Evaluation_Self.CompetenceCatQId = CompetenceCatQ.Id " +
                "AND CompetenceCatQ.CompetenceCatId = 23 " +
                "AND Evaluation_Self.EvaluationId = @EvaluationId";
            conn = new SqlConnection();
            conn.ConnectionString = WebConfigurationManager.ConnectionStrings["dbAMS"].ConnectionString;
            comm = new SqlCommand(strSql, conn);
            comm.Parameters.AddWithValue("@EvaluationId", evaluationId);
            dt = new DataTable();
            adp = new SqlDataAdapter(comm);

            conn.Open();
            adp.Fill(dt);
            conn.Close();

            return dt;
        }

        public DataTable getSelf_CustomerService_filled(int evaluationId)
        {
            strSql = "SELECT Evaluation_Self.Id, CompetenceCatQ.Question, " +
                "Evaluation_Self.Rating, Evaluation_Self.Remarks " +
                "FROM Evaluation_Self, CompetenceCatQ " +
                "WHERE Evaluation_Self.CompetenceCatQId = CompetenceCatQ.Id " +
                "AND CompetenceCatQ.CompetenceCatId  = 24 " +
                "AND Evaluation_Self.EvaluationId = @EvaluationId";

            conn = new SqlConnection();
            conn.ConnectionString = WebConfigurationManager.ConnectionStrings["dbAMS"].ConnectionString;
            comm = new SqlCommand(strSql, conn);
            comm.Parameters.AddWithValue("@EvaluationId", evaluationId);
            dt = new DataTable();
            adp = new SqlDataAdapter(comm);

            conn.Open();
            adp.Fill(dt);
            conn.Close();

            return dt;
        }

        public DataTable getSelf_Originality_filled(int evaluationId)
        {
            strSql = "SELECT Evaluation_Self.Id, CompetenceCatQ.Question, " +
                "Evaluation_Self.Rating, Evaluation_Self.Remarks " +
                "FROM Evaluation_Self, CompetenceCatQ " +
                "WHERE Evaluation_Self.CompetenceCatQId = CompetenceCatQ.Id " +
                "AND CompetenceCatQ.CompetenceCatId  = 25 " +
                "AND Evaluation_Self.EvaluationId = @EvaluationId";

            conn = new SqlConnection();
            conn.ConnectionString = WebConfigurationManager.ConnectionStrings["dbAMS"].ConnectionString;
            comm = new SqlCommand(strSql, conn);
            comm.Parameters.AddWithValue("@EvaluationId", evaluationId);
            dt = new DataTable();
            adp = new SqlDataAdapter(comm);

            conn.Open();
            adp.Fill(dt);
            conn.Close();

            return dt;
        }

        public DataTable getSelf_Responsibility_filled(int evaluationId)
        {
            strSql = "SELECT Evaluation_Self.Id, CompetenceCatQ.Question, " +
                "Evaluation_Self.Rating, Evaluation_Self.Remarks " +
                "FROM Evaluation_Self, CompetenceCatQ " +
                "WHERE Evaluation_Self.CompetenceCatQId = CompetenceCatQ.Id " +
                "AND CompetenceCatQ.CompetenceCatId  = 26 " +
                "AND Evaluation_Self.EvaluationId = @EvaluationId";

            conn = new SqlConnection();
            conn.ConnectionString = WebConfigurationManager.ConnectionStrings["dbAMS"].ConnectionString;
            comm = new SqlCommand(strSql, conn);
            comm.Parameters.AddWithValue("@EvaluationId", evaluationId);
            dt = new DataTable();
            adp = new SqlDataAdapter(comm);

            conn.Open();
            adp.Fill(dt);
            conn.Close();

            return dt;
        }

        public DataTable getSelf_Excellent_filled(int evaluationId)
        {
            strSql = "SELECT Evaluation_Self.Id, CompetenceCatQ.Question, " +
                "Evaluation_Self.Rating, Evaluation_Self.Remarks " +
                "FROM Evaluation_Self, CompetenceCatQ " +
                "WHERE Evaluation_Self.CompetenceCatQId = CompetenceCatQ.Id " +
                "AND CompetenceCatQ.CompetenceCatId  = 27 " +
                "AND Evaluation_Self.EvaluationId = @EvaluationId";

            conn = new SqlConnection();
            conn.ConnectionString = WebConfigurationManager.ConnectionStrings["dbAMS"].ConnectionString;
            comm = new SqlCommand(strSql, conn);
            comm.Parameters.AddWithValue("@EvaluationId", evaluationId);
            dt = new DataTable();
            adp = new SqlDataAdapter(comm);

            conn.Open();
            adp.Fill(dt);
            conn.Close();

            return dt;
        }

        public void updateSelf_Evaluation_Rating(
            int rating,
            string remarks,
            int Id)
        {
            strSql = "UPDATE Evaluation_Self SET Rating=@Rating, Remarks=@Remarks " +
                "WHERE Id = @Id";
            conn = new SqlConnection();
            conn.ConnectionString = WebConfigurationManager.ConnectionStrings["dbAMS"].ConnectionString;

            using (comm = new SqlCommand(strSql, conn))
            {
                conn.Open();
                comm.Parameters.AddWithValue("@Rating", rating);
                comm.Parameters.AddWithValue("@Remarks", remarks);
                comm.Parameters.AddWithValue("@Id", Id);
                comm.ExecuteNonQuery();
                conn.Close();
            }
            comm.Dispose();
            conn.Dispose();
        }
        #endregion

        #region CONFIG EVALUATION PERIOD

        public DataTable DispalyEvaluationPeriod()
        {
            strSql = "SELECT * FROM EVALUATION_PERIOD";

            conn = new SqlConnection();
            conn.ConnectionString = WebConfigurationManager.ConnectionStrings["dbAMS"].ConnectionString;
            comm = new SqlCommand(strSql, conn);
            dt = new DataTable();
            adp = new SqlDataAdapter(comm);

            conn.Open();
            adp.Fill(dt);
            conn.Close();

            return dt;
        }

        public void SetEvaluationPeriod(string fromDate,
            string toDate,
            string Id)
        {
            strSql = "UPDATE EVALUATION_PERIOD SET FromDate=@FromDate, " +
                "ToDate=@ToDate WHERE Id = @Id";

            conn = new SqlConnection();
            conn.ConnectionString = WebConfigurationManager.ConnectionStrings["dbAMS"].ConnectionString;

            using (comm = new SqlCommand(strSql, conn))
            {
                conn.Open();
                comm.Parameters.AddWithValue("@FromDate", fromDate);
                comm.Parameters.AddWithValue("@ToDate", toDate);
                comm.Parameters.AddWithValue("@Id", Id);
                comm.ExecuteNonQuery();
                conn.Close();
            }
            comm.Dispose();
            conn.Dispose();
        }
        #endregion

        #region ChkEvaluationPeriod
        public DataTable ChkEvaluationPeriod()
        {
            strSql = "SELECT EvaluationPeriod FROM EVALUATION_PERIOD_CONFIG WHERE GETDATE() BETWEEN StartEvaluation AND EndEvaluation";

            conn = new SqlConnection();
            conn.ConnectionString = WebConfigurationManager.ConnectionStrings["dbAMS"].ConnectionString;
            comm = new SqlCommand(strSql, conn);
            dt = new DataTable();
            adp = new SqlDataAdapter(comm);

            conn.Open();
            adp.Fill(dt);
            conn.Close();

            return dt;
        }
        
        #endregion
    }
}
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

        public DataTable display_filled_TSIQuestions(Guid UserId)
        {
            strSql = "SELECT Competence.Competence,CompetenceCat.CompetenceCat,CompetenceCat.Description,CompetenceCat.TSIRating, " +
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

        public int insertEvaluation(
            Guid UserId,
            string EvaluationType,
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
            string EvaluatedBy,
            string ApprovedByManager,
            string ApprovedByHR,
            string AcknowledgedBy,
            string agency
            )
        {
            int _newlyInsertedId = 0;

            strSql = "INSERT INTO Evaluation(UserId,EvaluationType,EvaluatedById,TotalScore,RemarksName," +
                "ImpUnacceptable,ImpFallShort,ImpEffective,ImpHighlyEffective, " +
                "ImpExceptional, Recommendation, NeedImprovement,  EvaluatedBy, ApprovedByManager, " +
                "ApprovedByHR, AcknowledgedBy,Agency)" +
                "VALUES(@UserId, @EvaluationType, @EvaluatedById, @TotalScore,@RemarksName, " +
                "@ImpUnacceptable, @ImpFallShort, @ImpEffective, @ImpHighlyEffective, " +
                "@ImpExceptional, @Recommendation, @NeedImprovement, @EvaluatedBy, " +
                "@ApprovedByManager, @ApprovedByHR, @AcknowledgedBy,@Agency);" +
                "SELECT SCOPE_IDENTITY()";

            conn = new SqlConnection();
            conn.ConnectionString = WebConfigurationManager.ConnectionStrings["dbAMS"].ConnectionString;

            using (comm = new SqlCommand(strSql, conn))
            {
                conn.Open();
                comm.Parameters.AddWithValue("@UserId", UserId);
                comm.Parameters.AddWithValue("@EvaluationType", EvaluationType);
                comm.Parameters.AddWithValue("@EvaluatedById", EvaludatedById);
                comm.Parameters.AddWithValue("@TotalScore", TotalScore);
                comm.Parameters.AddWithValue("@RemarksName", RemarksName);
                comm.Parameters.AddWithValue("@ImpUnacceptable", ImpUnacceptable);
                comm.Parameters.AddWithValue("@ImpFallShort", ImpFallShort);
                comm.Parameters.AddWithValue("@ImpEffective", ImpEffective);
                comm.Parameters.AddWithValue("@ImpHighlyEffective", ImpHighlyEffective);
                comm.Parameters.AddWithValue("@ImpExceptional", ImpExceptional);
                comm.Parameters.AddWithValue("@Recommendation", Recommendation);
                comm.Parameters.AddWithValue("@NeedImprovement", NeedImprovement);
                comm.Parameters.AddWithValue("@EvaluatedBy", EvaluatedBy);
                comm.Parameters.AddWithValue("@ApprovedByManager", ApprovedByManager);
                comm.Parameters.AddWithValue("@ApprovedByHR", ApprovedByHR);
                comm.Parameters.AddWithValue("@AcknowledgedBy", AcknowledgedBy);
                comm.Parameters.AddWithValue("@Agency", agency);

                object exScalar = comm.ExecuteScalar();
                _newlyInsertedId = (exScalar == null ? -1 : Convert.ToInt32(exScalar.ToString()));
                conn.Close();
            }
            comm.Dispose();
            conn.Dispose();

            return _newlyInsertedId;
        }

        public void updateEvaluation(
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
            string EvaluatedBy,
            string ApprovedByManager,
            string ApprovedByHR,
            string AcknowledgedBy,
            string evaluationId)
        {
            strSql = "UPDATE Evaluation SET " +
                "EvaluatedById = @EvaluatedById, " +
                "TotalScore = @TotalScore, " +
                "RemarksName = @RemarksName, " +
                "ImpUnacceptable = @ImpUnacceptable, " +
                "ImpFallShort = @ImpFallShort, " +
                "ImpEffective = @ImpEffective, " +
                "ImpHighlyEffective = @ImpHighlyEffective, " +
                "ImpExceptional = @ImpExceptional, " +
                "Recommendation = @Recommendation, " +
                "NeedImprovement = @NeedImprovement, " +
                "EvaluatedBy = @EvaluatedBy, " +
                "ApprovedByManager = @ApprovedByManager, " +
                "ApprovedByHR = @ApprovedByHR, " +
                "AcknowledgedBy = @AcknowledgedBy " +
                "WHERE Id = @Id";

            conn = new SqlConnection();
            conn.ConnectionString = WebConfigurationManager.ConnectionStrings["dbAMS"].ConnectionString;

            using (comm = new SqlCommand(strSql, conn))
            {
                conn.Open();
                comm.Parameters.AddWithValue("@EvaluatedById", EvaludatedById);
                comm.Parameters.AddWithValue("@TotalScore", TotalScore);
                comm.Parameters.AddWithValue("@RemarksName", RemarksName);
                comm.Parameters.AddWithValue("@ImpUnacceptable", ImpUnacceptable);
                comm.Parameters.AddWithValue("@ImpFallShort", ImpFallShort);
                comm.Parameters.AddWithValue("@ImpEffective", ImpEffective);
                comm.Parameters.AddWithValue("@ImpHighlyEffective", ImpHighlyEffective);
                comm.Parameters.AddWithValue("@ImpExceptional", ImpExceptional);
                comm.Parameters.AddWithValue("@Recommendation", Recommendation);
                comm.Parameters.AddWithValue("@NeedImprovement", NeedImprovement);
                comm.Parameters.AddWithValue("@EvaluatedBy", EvaluatedBy);
                comm.Parameters.AddWithValue("@ApprovedByManager", ApprovedByManager);
                comm.Parameters.AddWithValue("@ApprovedByHR", ApprovedByHR);
                comm.Parameters.AddWithValue("@AcknowledgedBy", AcknowledgedBy);
                comm.Parameters.AddWithValue("@Id", evaluationId);
                comm.ExecuteNonQuery();
                conn.Close();
            }
            comm.Dispose();
            conn.Dispose();
        }

        public void addEvaluation_Scores(
            int evaluationId,
            int competencyId,
            decimal rating)
        {
            strSql = "INSERT INTO Evaluation_Score(EvaluationId, CompetenceCatId, Rating) " +
                "VALUES(@Agency, @CompetenceCatId, @Rating)";

            conn = new SqlConnection();
            conn.ConnectionString = WebConfigurationManager.ConnectionStrings["dbAMS"].ConnectionString;

            using (comm = new SqlCommand(strSql, conn))
            {
                conn.Open();
                comm.Parameters.AddWithValue("@Agency", evaluationId);
                comm.Parameters.AddWithValue("@CompetenceCatId", competencyId);
                comm.Parameters.AddWithValue("@Rating", rating);
                comm.ExecuteNonQuery();
                conn.Close();
            }
            comm.Dispose();
            conn.Dispose();
        }

        public void updateEvaluation_Scores(
            int eva_score_id,
            decimal rating)
        {
            strSql = "UPDATE Evaluation_Score SET Rating=@Rating WHERE " +
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

        public DataTable displayMyEvaluation(Guid UserId)
        {
            strSql = "SELECT Id,EvaluationType,DateEvaluated,EvaluatedBy,ApprovedByManager,ApprovedByHR " +
                "FROM Evaluation WHERE UserId = @UserId";

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
        public int insertEvaluation_Prime(
            Guid UserId,
            string EvaluationType,
            Guid EvaludatedById,
            string EvaluatedBy,
            string ApprovedByManager,
            string ApprovedByHR,
            string AcknowledgedBy,
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
            string last_date_of_eval,
            string next_date_eval,
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

            strSql = "INSERT INTO Evaluation(UserId,EvaluationType,EvaluatedById, EvaluatedBy, ApprovedByManager, " +
                "ApprovedByHR, AcknowledgedBy,Agency, " +
                "Section1A, Section1B, Section1C, " +
                "Section2A, Section2B, Section2C, " +
                "Section3A, Section3B, Section3C, Section3D, Section3E, Section3F," +
                "CommentSection1A, CommentSection1B, CommentSection1C, " +
                "CommentSection2A, CommentSection2B, CommentSection2C, " +
                "CommentSection3A, CommentSection3B, CommentSection3C,CommentSection3D,CommentSection3E,CommentSection3F, " +
                "DaysSick, DaysTardy, primeComments, LastDateEvaluation, DateNextEvaluation, " +
                "EmployeesCreativeContribution, EmployeesNewSkills, EmployeesStrength, " +
                "EmployeesImprovement, EmployeesChanges, EmployeesPersonalGoals, " +
                "EmployeesRecommendation) " +
                "VALUES(@UserId, @EvaluationType, @EvaluatedById,  @EvaluatedBy, " +
                "@ApprovedByManager, @ApprovedByHR, @AcknowledgedBy,@Agency, " +
                "@Section1A, @Section1B, @Section1C, " +
                "@Section2A, @Section2B, @Section2C, " +
                "@Section3A, @Section3B, @Section3C, @Section3D, @Section3E, @Section3F, " +
                "@CommentSection1A, @CommentSection1B, @CommentSection1C, " +
                "@CommentSection2A, @CommentSection2B, @CommentSection2C, " +
                "@CommentSection3A, @CommentSection3B, @CommentSection3C, @CommentSection3D, @CommentSection3E,@CommentSection3F, " +
                "@DaysSick, @DaysTardy, @primeComments, @LastDateEvaluation, @DateNextEvaluation, " +
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
                comm.Parameters.AddWithValue("@EvaluatedById", EvaludatedById);
                comm.Parameters.AddWithValue("@EvaluatedBy", EvaluatedBy);
                comm.Parameters.AddWithValue("@ApprovedByManager", ApprovedByManager);
                comm.Parameters.AddWithValue("@ApprovedByHR", ApprovedByHR);
                comm.Parameters.AddWithValue("@AcknowledgedBy", AcknowledgedBy);
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
                comm.Parameters.AddWithValue("@LastDateEvaluation", last_date_of_eval);
                comm.Parameters.AddWithValue("@DateNextEvaluation", next_date_eval);
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

        public void updateEvaluation_Prime(
            Guid EvaluatedById,
            string dateEvaluated,
            string EvaluatedBy,
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
            string last_date_of_eval,
            string next_date_eval,
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
                "DateEvaluated=@DateEvaluated, " +
                "EvaluatedBy=@EvaluatedBy, " +
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
                "DaysSick=@DaysSick, DaysTardy=@DaysTardy, primeComments=@primeComments, LastDateEvaluation=@LastDateEvaluation, " +
                "DateNextEvaluation=@DateNextEvaluation " +
                "WHERE Id = @Id";
            conn = new SqlConnection();
            conn.ConnectionString = WebConfigurationManager.ConnectionStrings["dbAMS"].ConnectionString;

            using (comm = new SqlCommand(strSql, conn))
            {
                conn.Open();
                comm.Parameters.AddWithValue("@EvaluatedById", EvaluatedById);
                comm.Parameters.AddWithValue("@DateEvaluated", dateEvaluated);
                comm.Parameters.AddWithValue("@EvaluatedBy", EvaluatedBy);
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
                comm.Parameters.AddWithValue("@LastDateEvaluation", last_date_of_eval);
                comm.Parameters.AddWithValue("@DateNextEvaluation", next_date_eval);
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
        public DataTable getEvaluated(int evaluationId)
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
        public DataTable getPendingApprovalHR()
        {
            strSql = "SELECT Id,RemarksName,EvaluatedBy,AcknowledgedBy FROM Evaluation WHERE " +
                    "(ApprovedByHR = '')";

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

        //Pending Approval List by GM ->only managers
        //GM the signatory of Managers
        public DataTable getPendingApprovalGM()
        {
            strSql = "SELECT Evaluation.Id,Evaluation.RemarksName,Evaluation.EvaluatedBy, " +
                "Evaluation.AcknowledgedBy, Evaluation.UserId " +
                "FROM Evaluation, UsersInRoles, Roles WHERE " +
                "Evaluation.UserId = UsersInRoles.UserId AND " +
                "UsersInRoles.RoleId = Roles.RoleId AND " +
                "Roles.RoleName = 'Manager'";

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

        //Pending Approval List by Manager ->
        public DataTable getPendingApprovalManager()
        {
            strSql = "SELECT Evaluation.Id,Evaluation.RemarksName,Evaluation.EvaluatedBy, " +
                "Evaluation.AcknowledgedBy, Evaluation.UserId " +
                "FROM Evaluation, UsersInRoles, Roles WHERE " +
                "Evaluation.UserId = UsersInRoles.UserId AND " +
                "UsersInRoles.RoleId = Roles.RoleId AND " +
                "((Roles.RoleName = 'Supervisor') OR (Roles.RoleName = 'Staff'))";

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

        //Pending Approval List by Manager
        public DataTable getPendingApprovalSupervisor()
        {
            strSql = "SELECT Id,RemarksName,EvaluatedBy,AcknowledgedBy FROM Evaluation WHERE " +
                    "(ApprovedByManager = '')";

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

        //Approval by HR
        public void ApprovePendingApprovalHR(string evaluationId, string signatory)
        {
            strSql = "UPDATE Evaluation SET ApprovedByHR = @ApprovedByHR WHERE Id = @Id";

            conn = new SqlConnection();
            conn.ConnectionString = WebConfigurationManager.ConnectionStrings["dbAMS"].ConnectionString;
            comm = new SqlCommand(strSql, conn);
            comm.Parameters.AddWithValue("@ApprovedByHR", signatory);
            comm.Parameters.AddWithValue("@Id", evaluationId);
            conn.Open();
            comm.ExecuteNonQuery();
            comm.Dispose();
            conn.Close();
        }


        //Pending Approval List by Manager
        public DataTable getPendingApprovalManager(string year, string department)
        {
            strSql = "SELECT RemarksName,EvaluatedBy,AcknowledgedBy FROM Evaluation WHERE " +
                    "DATEPART(yyyy,DateEvaluated) = @YearEvaluated AND " +
                    "(ApprovedByManager = '')";

            conn = new SqlConnection();
            conn.ConnectionString = WebConfigurationManager.ConnectionStrings["dbAMS"].ConnectionString;
            comm = new SqlCommand(strSql, conn);
            comm.Parameters.AddWithValue("@YearEvaluated", year);
            dt = new DataTable();
            adp = new SqlDataAdapter(comm);

            conn.Open();
            adp.Fill(dt);
            conn.Close();

            return dt;
        }

        public DataTable getPendingEvaluationManager(string year)
        {
            strSql = "SELECT * FROM Evaluation WHERE " +
                    "DATEPART(yyyy,DateEvaluated) = @YearEvaluated AND " +
                    "(ApprovedByManager = '')";

            conn = new SqlConnection();
            conn.ConnectionString = WebConfigurationManager.ConnectionStrings["dbAMS"].ConnectionString;
            comm = new SqlCommand(strSql, conn);
            comm.Parameters.AddWithValue("@YearEvaluated", year);
            dt = new DataTable();
            adp = new SqlDataAdapter(comm);

            conn.Open();
            adp.Fill(dt);
            conn.Close();

            return dt;
        }
        #endregion

        #region SELF EVALUATION
        ////////////////////////////////////////////////////////////////////////////////
        /////////////////////////////// SELF EVALUATION ////////////////////////////////
        ////////////////////////////////////////////////////////////////////////////////
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
            int rating,
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
    }
}
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using TuDuyWeb.Models;

namespace TuDuyWeb.Services
{
    public class MemoryRepository
    {
        private static readonly (int No, string Type, string Text)[] DefaultQuestions =
        {
            (1, "reflection", "Hom nay dieu gi lam ban thay co y nghia nhat?"),
            (2, "decision", "Ban da ra mot quyet dinh nao dang nho? Vi sao?"),
            (3, "emotion", "Cam xuc noi bat nhat trong ngay la gi? Dieu gi kich hoat no?"),
            (4, "lesson", "Ban hoc duoc bai hoc nao tu cong viec, con nguoi, hoac chinh minh?"),
            (5, "next_step", "Ngay mai ban muon giu lai dieu gi va cai thien dieu gi?")
        };

        private readonly string _connectionString;

        public MemoryRepository()
        {
            _connectionString = ConfigurationManager.ConnectionStrings["MemoryDb"]?.ConnectionString;
            if (string.IsNullOrWhiteSpace(_connectionString))
            {
                throw new InvalidOperationException("Missing connection string 'MemoryDb' in Web.config.");
            }
        }

        public HomeIndexViewModel GetOrCreateTodaySession(DateTime sessionDate)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                var sessionId = EnsureSession(connection, sessionDate.Date);
                EnsureDefaultQuestions(connection, sessionId);

                return new HomeIndexViewModel
                {
                    SessionId = sessionId,
                    SessionDate = sessionDate.Date,
                    InterviewStatus = GetSessionStatus(connection, sessionId),
                    Summary = GetSessionSummary(connection, sessionId),
                    Questions = GetQuestions(connection, sessionId)
                };
            }
        }

        public void SaveSession(HomeIndexViewModel model)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                using (var transaction = connection.BeginTransaction())
                {
                    foreach (var question in model.Questions)
                    {
                        var answer = Normalize(question.AnswerText);
                        var isAnswered = !string.IsNullOrWhiteSpace(answer);

                        using (var command = new SqlCommand(@"
UPDATE memory_questions
SET answer_text = @answer_text,
    is_answered = @is_answered,
    answered_at = CASE WHEN @is_answered = 1 THEN SYSUTCDATETIME() ELSE NULL END
WHERE session_id = @session_id AND question_no = @question_no;", connection, transaction))
                        {
                            command.Parameters.AddWithValue("@answer_text", (object)answer ?? DBNull.Value);
                            command.Parameters.AddWithValue("@is_answered", isAnswered);
                            command.Parameters.AddWithValue("@session_id", model.SessionId);
                            command.Parameters.AddWithValue("@question_no", question.QuestionNo);
                            command.ExecuteNonQuery();
                        }
                    }

                    var answeredCount = 0;
                    foreach (var question in model.Questions)
                    {
                        if (!string.IsNullOrWhiteSpace(question.AnswerText))
                        {
                            answeredCount++;
                        }
                    }

                    var status = answeredCount == model.Questions.Count && model.Questions.Count > 0
                        ? "completed"
                        : "in_progress";

                    using (var command = new SqlCommand(@"
UPDATE memory_sessions
SET summary = @summary,
    interview_status = @interview_status,
    question_count = @question_count,
    source = COALESCE(source, 'manual_web'),
    updated_at = SYSUTCDATETIME()
WHERE id = @id;", connection, transaction))
                    {
                        command.Parameters.AddWithValue("@summary", (object)Normalize(model.Summary) ?? DBNull.Value);
                        command.Parameters.AddWithValue("@interview_status", status);
                        command.Parameters.AddWithValue("@question_count", model.Questions.Count);
                        command.Parameters.AddWithValue("@id", model.SessionId);
                        command.ExecuteNonQuery();
                    }

                    transaction.Commit();
                }
            }
        }

        public List<MemorySessionSummaryViewModel> GetRecentSessions(int take = 30)
        {
            var sessions = new List<MemorySessionSummaryViewModel>();

            using (var connection = new SqlConnection(_connectionString))
            using (var command = new SqlCommand(@"
SELECT TOP (@take)
    id,
    session_date,
    interview_status,
    question_count,
    summary,
    processing_status,
    updated_at
FROM memory_sessions
ORDER BY session_date DESC;", connection))
            {
                command.Parameters.AddWithValue("@take", take);
                connection.Open();

                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        sessions.Add(new MemorySessionSummaryViewModel
                        {
                            Id = reader.GetInt64(0),
                            SessionDate = reader.GetDateTime(1),
                            InterviewStatus = reader.GetString(2),
                            QuestionCount = reader.GetInt32(3),
                            Summary = reader.IsDBNull(4) ? null : reader.GetString(4),
                            ProcessingStatus = reader.GetString(5),
                            UpdatedAt = reader.GetDateTime(6)
                        });
                    }
                }
            }

            return sessions;
        }

        public MemorySessionDetailViewModel GetSessionDetail(long sessionId)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                MemorySessionDetailViewModel session = null;
                using (var command = new SqlCommand(@"
SELECT
    id,
    session_date,
    interview_status,
    question_count,
    summary,
    processing_status,
    updated_at
FROM memory_sessions
WHERE id = @id;", connection))
                {
                    command.Parameters.AddWithValue("@id", sessionId);

                    using (var reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            session = new MemorySessionDetailViewModel
                            {
                                Id = reader.GetInt64(0),
                                SessionDate = reader.GetDateTime(1),
                                InterviewStatus = reader.GetString(2),
                                QuestionCount = reader.GetInt32(3),
                                Summary = reader.IsDBNull(4) ? null : reader.GetString(4),
                                ProcessingStatus = reader.GetString(5),
                                UpdatedAt = reader.GetDateTime(6)
                            };
                        }
                    }
                }

                if (session == null)
                {
                    return null;
                }

                session.Questions = GetQuestions(connection, sessionId);
                return session;
            }
        }

        private static string Normalize(string value)
        {
            return string.IsNullOrWhiteSpace(value) ? null : value.Trim();
        }

        private static long EnsureSession(SqlConnection connection, DateTime sessionDate)
        {
            using (var selectCommand = new SqlCommand(
                "SELECT id FROM memory_sessions WHERE session_date = @session_date;",
                connection))
            {
                selectCommand.Parameters.AddWithValue("@session_date", sessionDate);
                var existingId = selectCommand.ExecuteScalar();
                if (existingId != null && existingId != DBNull.Value)
                {
                    return Convert.ToInt64(existingId);
                }
            }

            using (var insertCommand = new SqlCommand(@"
INSERT INTO memory_sessions (session_date, interview_status, question_count, source, processing_status)
VALUES (@session_date, 'in_progress', 0, 'manual_web', 'pending');
SELECT CAST(SCOPE_IDENTITY() AS BIGINT);", connection))
            {
                insertCommand.Parameters.AddWithValue("@session_date", sessionDate);
                return Convert.ToInt64(insertCommand.ExecuteScalar());
            }
        }

        private static void EnsureDefaultQuestions(SqlConnection connection, long sessionId)
        {
            using (var countCommand = new SqlCommand(
                "SELECT COUNT(1) FROM memory_questions WHERE session_id = @session_id;",
                connection))
            {
                countCommand.Parameters.AddWithValue("@session_id", sessionId);
                var count = Convert.ToInt32(countCommand.ExecuteScalar());
                if (count > 0)
                {
                    return;
                }
            }

            foreach (var question in DefaultQuestions)
            {
                using (var insertCommand = new SqlCommand(@"
INSERT INTO memory_questions (session_id, question_no, question_type, question_text, answer_text, is_answered)
VALUES (@session_id, @question_no, @question_type, @question_text, NULL, 0);", connection))
                {
                    insertCommand.Parameters.AddWithValue("@session_id", sessionId);
                    insertCommand.Parameters.AddWithValue("@question_no", question.No);
                    insertCommand.Parameters.AddWithValue("@question_type", question.Type);
                    insertCommand.Parameters.AddWithValue("@question_text", question.Text);
                    insertCommand.ExecuteNonQuery();
                }
            }

            using (var updateCommand = new SqlCommand(@"
UPDATE memory_sessions
SET question_count = @question_count,
    updated_at = SYSUTCDATETIME()
WHERE id = @id;", connection))
            {
                updateCommand.Parameters.AddWithValue("@question_count", DefaultQuestions.Length);
                updateCommand.Parameters.AddWithValue("@id", sessionId);
                updateCommand.ExecuteNonQuery();
            }
        }

        private static string GetSessionStatus(SqlConnection connection, long sessionId)
        {
            using (var command = new SqlCommand(
                "SELECT interview_status FROM memory_sessions WHERE id = @id;",
                connection))
            {
                command.Parameters.AddWithValue("@id", sessionId);
                return Convert.ToString(command.ExecuteScalar());
            }
        }

        private static string GetSessionSummary(SqlConnection connection, long sessionId)
        {
            using (var command = new SqlCommand(
                "SELECT summary FROM memory_sessions WHERE id = @id;",
                connection))
            {
                command.Parameters.AddWithValue("@id", sessionId);
                var value = command.ExecuteScalar();
                return value == DBNull.Value || value == null ? null : Convert.ToString(value);
            }
        }

        private static List<MemoryQuestionInputViewModel> GetQuestions(SqlConnection connection, long sessionId)
        {
            var questions = new List<MemoryQuestionInputViewModel>();

            using (var command = new SqlCommand(@"
SELECT question_no, question_type, question_text, answer_text, is_answered
FROM memory_questions
WHERE session_id = @session_id
ORDER BY question_no;", connection))
            {
                command.Parameters.AddWithValue("@session_id", sessionId);

                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        questions.Add(new MemoryQuestionInputViewModel
                        {
                            QuestionNo = reader.GetInt32(0),
                            QuestionType = reader.GetString(1),
                            QuestionText = reader.GetString(2),
                            AnswerText = reader.IsDBNull(3) ? null : reader.GetString(3),
                            IsAnswered = reader.GetBoolean(4)
                        });
                    }
                }
            }

            return questions;
        }
    }
}

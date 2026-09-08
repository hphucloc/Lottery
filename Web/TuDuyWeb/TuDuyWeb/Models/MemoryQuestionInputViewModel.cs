namespace TuDuyWeb.Models
{
    public class MemoryQuestionInputViewModel
    {
        public int QuestionNo { get; set; }

        public string QuestionType { get; set; }

        public string QuestionText { get; set; }

        public string AnswerText { get; set; }

        public bool IsAnswered { get; set; }
    }
}

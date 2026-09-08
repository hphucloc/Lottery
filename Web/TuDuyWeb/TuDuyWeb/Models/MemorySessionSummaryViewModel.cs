using System;

namespace TuDuyWeb.Models
{
    public class MemorySessionSummaryViewModel
    {
        public long Id { get; set; }

        public DateTime SessionDate { get; set; }

        public string InterviewStatus { get; set; }

        public int QuestionCount { get; set; }

        public string Summary { get; set; }

        public string ProcessingStatus { get; set; }

        public DateTime UpdatedAt { get; set; }
    }
}

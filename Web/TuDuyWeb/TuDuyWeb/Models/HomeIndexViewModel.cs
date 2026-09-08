using System;
using System.Collections.Generic;

namespace TuDuyWeb.Models
{
    public class HomeIndexViewModel
    {
        public HomeIndexViewModel()
        {
            Questions = new List<MemoryQuestionInputViewModel>();
        }

        public long SessionId { get; set; }

        public DateTime SessionDate { get; set; }

        public string InterviewStatus { get; set; }

        public string Summary { get; set; }

        public string StatusMessage { get; set; }

        public List<MemoryQuestionInputViewModel> Questions { get; set; }
    }
}

using System.Collections.Generic;

namespace TuDuyWeb.Models
{
    public class MemorySessionDetailViewModel : MemorySessionSummaryViewModel
    {
        public MemorySessionDetailViewModel()
        {
            Questions = new List<MemoryQuestionInputViewModel>();
        }

        public List<MemoryQuestionInputViewModel> Questions { get; set; }
    }
}

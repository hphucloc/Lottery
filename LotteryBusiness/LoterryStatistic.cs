using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LotteryBusiness
{
    public class LoterryStatistic
    {
        public Int16 NumberTypeId { get; set; }
        public Int16 NumberWinLevelId { get; set; }
        public string LotNumber { get; set; }
        
        public DatePublishList2 DatePublishList = new DatePublishList2();

        public List<DateTime> AllDatePublishList = new List<DateTime>();      
        public DateTime DateCreated { get; set; }
        public DateTime DatePublish { get; set; }
        public System.DateTime DatePublishMax { get; set; }
        public System.DateTime DatePublishMin { get; set; }
        public int TotalNumberAppear { get; set; }
        public int TotalNumberAppearInRange { get; set; }
        public int? KyQuay { get; set; }
    }

    public class DatePublishList2
    {
        public List<DateTime> DatePublishList1 = new List<DateTime>();
        public List<DateTime> DateBoughtList = new List<DateTime>();
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LotteryBusiness
{
    public partial class LotteryNumber
    {
        public long NumberId { get; set; }
        public short NumberTypeId { get; set; }
        public short NumberWinLevelId { get; set; }
        public string LotNumber { get; set; }
        public System.DateTime DatePublish { get; set; }
        public System.DateTime DateCreated { get; set; }
        public Nullable<System.DateTime> DateUpdated { get; set; }   
        public int TotalNumberAppear { get; set; }
        public int TotalNumberAppearInRange { get; set; }
        public int? KyQuay { get; set; } 

    }
}

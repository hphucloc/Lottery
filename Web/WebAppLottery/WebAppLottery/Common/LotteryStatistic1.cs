using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebAppLottery.Models
{
    public class LotteryStatistic1
    {
        //public Int16 NumberTypeId { get; set; }
        //public Int16 NumberWinLevelId { get; set; }
        public int LotNumber { get; set; }       

        public List<DateTime> AllDatePublishList = new List<DateTime>();

        public LotteryBusiness.DatePublishList2 DatePublishList = new LotteryBusiness.DatePublishList2();
        //public DateTime DateCreated { get; set; }
        public DateTime DatePublish { get; set; }
        //public System.DateTime DatePublishMax { get; set; }
        //public System.DateTime DatePublishMin { get; set; }
        //public int TotalNumberAppear { get; set; }
        public int TotalNumberAppearInRange { get; set; }       
    }
}
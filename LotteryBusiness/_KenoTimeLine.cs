using LotteryDAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LotteryBusiness
{
    public class _KenoTimeLine
    {
        public static List<LoterryStatistic> GetKeno(DateTime datePublishFrom, DateTime datePublishTo)
        {
            IQueryable<LotteryNumber> number = Common.GetNumber((Int16)Enum_NumberType._Keno, datePublishFrom, datePublishTo);
           // return Common.GetLotNumberStatistic(number);
        }

        public static List<LoterryStatistic> GetKenoChanLe(DateTime datePublishFrom, DateTime datePublishTo)
        {
            IQueryable<LotteryNumber> number = Common.GetNumber((Int16)Enum_NumberWinLevel.GiaiNhi, (Int16)Enum_NumberType._3DMax, datePublishFrom, datePublishTo);
            return Common.GetLotNumberStatistic(number);
        }

        public static List<LoterryStatistic> GetKenoLonNho(DateTime datePublishFrom, DateTime datePublishTo)
        {
            IQueryable<LotteryNumber> number = Common.GetNumber((Int16)Enum_NumberWinLevel.GiaiBa, (Int16)Enum_NumberType._3DMax, datePublishFrom, datePublishTo);
            return Common.GetLotNumberStatistic(number);
        }        
    }
}

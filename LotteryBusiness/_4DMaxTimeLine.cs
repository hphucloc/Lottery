using LotteryDAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LotteryBusiness
{
    public class _4DMaxTimeLine
    {
        public static List<LoterryStatistic> Get6Over45NumberGiaiNhat(DateTime datePublishFrom, DateTime datePublishTo)
        {
            IQueryable<LotteryNumber> number = Common.GetNumber((Int16)Enum_NumberWinLevel.GiaiNhat, (Int16)Enum_NumberType._3DMax, datePublishFrom, datePublishTo);
            return Common.GetLotNumberStatistic(number);
        }

        public static List<LoterryStatistic> Get6Over45NumberGiaiNhi(DateTime datePublishFrom, DateTime datePublishTo)
        {
            IQueryable<LotteryNumber> number = Common.GetNumber((Int16)Enum_NumberWinLevel.GiaiNhi, (Int16)Enum_NumberType._3DMax, datePublishFrom, datePublishTo);
            return Common.GetLotNumberStatistic(number);
        }

        public static List<LoterryStatistic> Get6Over45NumberGiaiBa(DateTime datePublishFrom, DateTime datePublishTo)
        {
            IQueryable<LotteryNumber> number = Common.GetNumber((Int16)Enum_NumberWinLevel.GiaiBa, (Int16)Enum_NumberType._3DMax, datePublishFrom, datePublishTo);
            return Common.GetLotNumberStatistic(number);
        }        
    }
}

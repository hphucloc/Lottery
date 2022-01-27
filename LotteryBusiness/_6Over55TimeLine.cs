using LotteryDAL;
using System;
using System.Collections.Generic;
using System.Linq;

namespace LotteryBusiness
{
    public class _6Over55TimeLine
    {
        public static List<LoterryStatistic> Get6Over55Number(DateTime datePublishFrom, DateTime datePublishTo)
        {
            IQueryable<LotteryNumber> number = Common.GetNumber((Int16)Enum_NumberWinLevel.DacBiet, (Int16)Enum_NumberType._6Over55, datePublishFrom, datePublishTo);
            return Common.GetLotNumberStatistic(number);
        }

        public static List<LoterryStatistic> Get6Over55Number(string lotNumber, DateTime datePublishFrom, DateTime datePublishTo)
        {
            IQueryable<LotteryNumber> number = Common.GetNumber(lotNumber, (Int16)Enum_NumberWinLevel.DacBiet, (Int16)Enum_NumberType._6Over55, datePublishFrom, datePublishTo);
            return Common.GetLotNumberStatistic(number);
        }

        public static void NewNumber(DateTime publishDdate, List<string> number)
        {
            Common.NewNumber(publishDdate, number, (Int16)Enum_NumberType._6Over55, (Int16)Enum_NumberWinLevel.DacBiet);
        }

        //public static List<int> GetNumberNextAppear(SortedSet<DateTime> dateAppear)
        //{
        //    return Common.GetNumberNextAppear(dateAppear, (Int16)Enum_NumberType._6Over55, (Int16)Enum_NumberWinLevel.DacBiet);
        //}

        public static void CreateBoughtNumber(List<int> number, DateTime dateBought, short numberType, short numberWinLevel)
        {
            Common.CreateBoughtNumber(number, dateBought, (Int16)Enum_NumberType._6Over55, (Int16)Enum_NumberWinLevel.DacBiet);
        }
    }
}

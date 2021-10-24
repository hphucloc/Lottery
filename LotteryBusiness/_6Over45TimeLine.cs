using LotteryDAL;
using System;
using System.Collections.Generic;
using System.Linq;


namespace LotteryBusiness
{
    public class _6Over45TimeLine
    {
        public static List<LoterryStatistic> Get6Over45Number(DateTime datePublishFrom, DateTime datePublishTo)
        {
            IQueryable<LotteryNumber> number = Common.GetNumber((Int16)Enum_NumberWinLevel.DacBiet, (Int16)Enum_NumberType._6Over45, datePublishFrom, datePublishTo);
            return Common.GetLotNumberStatistic(number);            
        }

        public static List<LoterryStatistic> Get6Over45Number(byte lotNumber,DateTime datePublishFrom, DateTime datePublishTo)
        {
            IQueryable<LotteryNumber> number = Common.GetNumber(lotNumber, (Int16)Enum_NumberWinLevel.DacBiet, (Int16)Enum_NumberType._6Over45, datePublishFrom, datePublishTo);
            return Common.GetLotNumberStatistic(number);
        }

        public static SortedSet<int> Get6Over45BoughtNumberPublishedInDate(DateTime datePublish)
        {
            var data = Common.GetBoughtNumberPublishedInDate((Int16)Enum_NumberWinLevel.DacBiet, (Int16)Enum_NumberType._6Over45, datePublish);
            SortedSet<int> rel = new SortedSet<int>();

            foreach (var i in data)
                rel.Add(i.LotNumber);
            return rel;
        }

        public static List<LotteryNumber> Get6Over45BoughtNumber(DateTime datePublishFrom, DateTime datePublishTo)
        {
            return Common.GetBoughtNumber((Int16)Enum_NumberWinLevel.DacBiet, (Int16)Enum_NumberType._6Over45, datePublishFrom, datePublishTo)
                .OrderBy(x=>x.DatePublish)
                .ToList();
        }

        public static List<int> GetNumberNotAppearInTime(int number, List<LoterryStatistic> _6Over45No)
        {
            List<int> numberNotAppear = new List<int>();

            return numberNotAppear;
        }

        public static void NewNumber(DateTime publishDdate, List<int> number)
        {
            Common.NewNumber(publishDdate, number, (Int16)Enum_NumberType._6Over45, (Int16)Enum_NumberWinLevel.DacBiet);
        }

        public static List<int> GetNumberNextAppear(SortedSet<DateTime> dateAppear)
        {
            return Common.GetNumberNextAppear(dateAppear, (Int16)Enum_NumberType._6Over45, (Int16)Enum_NumberWinLevel.DacBiet);
        }

        public static void CreateBoughtNumber(List<int> number, DateTime dateBought, short numberType, short numberWinLevel)
        {
            Common.CreateBoughtNumber(number, dateBought, (Int16)Enum_NumberType._6Over45, (Int16)Enum_NumberWinLevel.DacBiet);
        }
    }
}

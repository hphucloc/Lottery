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

        public static List<LoterryStatistic> Get6Over45Number(string lotNumber,DateTime datePublishFrom, DateTime datePublishTo)
        {
            IQueryable<LotteryNumber> number = Common.GetNumber(lotNumber, (Int16)Enum_NumberWinLevel.DacBiet, (Int16)Enum_NumberType._6Over45, datePublishFrom, datePublishTo);
            return Common.GetLotNumberStatistic(number);
        }          

        public static List<int> GetNumberNotAppearInTime(int number, List<LoterryStatistic> _6Over45No)
        {
            List<int> numberNotAppear = new List<int>();

            return numberNotAppear;
        }

        public static void NewNumber(DateTime publishDdate, List<string> number)
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

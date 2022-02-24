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

        public static List<LoterryStatistic> GetLatest6Over45Number()
        {
            IQueryable<LotteryNumber> number = Common.GetNumber((Int16)Enum_NumberWinLevel.DacBiet, (Int16)Enum_NumberType._6Over45, DateTime.Now.AddDays(-7), DateTime.Now);          
            return Common.GetLotNumberStatistic(number.OrderByDescending(x=>x.DatePublish).Take(6));
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

        public static List<string> GetNumberNextAppear(string number)
        {
            List<string> rdata = new List<string>();
            var data = Common.GetNumbersNextAppear(6, 1, 1);

            var lstDateNextAppear = data.Where(x => x.LotNumber == number && x.NextPublishDate != null).Select(x => x.NextPublishDate);

            //rdata = data.Where(x => lstDateNextAppear.Contains(x.DatePublish)).Select(x => x.LotNumber).ToList();
            foreach (var i in lstDateNextAppear)
            {
                rdata.AddRange(data.Where(x => x.DatePublish.Date == i.Value.Date).Select(x => x.LotNumber));
            }

            return rdata.OrderBy(x => Convert.ToInt32(x)).ToList();
        }

        public static List<string> GetColorNextAppear()
        {
            var data = Common.GetNumbersNextAppear(6, 1, 1);
            var latestNumber = GetLatest6Over45Number();
            var lstDateNextAppear = data.Where(x => x.LotNumber ==  && x.NextPublishDate != null).Select(x => x.NextPublishDate);

        }

        public static void CreateBoughtNumber(List<int> number, DateTime dateBought, short numberType, short numberWinLevel)
        {
            Common.CreateBoughtNumber(number, dateBought, (Int16)Enum_NumberType._6Over45, (Int16)Enum_NumberWinLevel.DacBiet);
        }
    }
}

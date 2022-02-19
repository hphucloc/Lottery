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

        public static List<LoterryStatistic> GetLatest6Over55Number()
        {
            IQueryable<LotteryNumber> number = Common.GetNumber((Int16)Enum_NumberWinLevel.DacBiet, (Int16)Enum_NumberType._6Over55, DateTime.Now.AddDays(-7), DateTime.Now);
            return Common.GetLotNumberStatistic(number.OrderByDescending(x => x.DatePublish).Take(7));
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

        public static List<string> GetNumberNextAppear(string number)
        {
            List<string> rdata = new List<string>();
            var data = Common.GetNumbersNextAppear(7, 3, 1);

            //Get NextPublishDate of LotNumber
            var lstDateNextAppear = data.Where(x => x.LotNumber == number && x.NextPublishDate != null).Select(x => x.NextPublishDate);

            //rdata = data.Where(x => lstDateNextAppear.Contains(x.DatePublish)).Select(x => x.LotNumber).ToList();
            foreach(var i in lstDateNextAppear)
            {
                rdata.AddRange(data.Where(x => x.DatePublish.Date == i.Value.Date).Select(x => x.LotNumber));
            }

            return rdata.OrderBy(x => Convert.ToInt32(x)).ToList();
        }

        public static void CreateBoughtNumber(List<int> number, DateTime dateBought, short numberType, short numberWinLevel)
        {
            Common.CreateBoughtNumber(number, dateBought, (Int16)Enum_NumberType._6Over55, (Int16)Enum_NumberWinLevel.DacBiet);
        }
    }
}

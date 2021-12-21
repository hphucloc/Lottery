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
        public static List<LoterryStatistic> GetKenoNumberStatistic(List<LotteryNumber> numbers, DateTime datePublishFrom, DateTime datePublishTo)
        {                       
            return Common.GetLotNumberStatisticKeno(numbers.AsQueryable());
        }

        public static List<LotteryNumber> GetKenoNumber(DateTime datePublishFrom, DateTime datePublishTo)
        {
            IQueryable<LotteryNumber> number = Common.GetNumber((int)Enum_NumberWinLevel.DacBiet, (Int16)Enum_NumberType._Keno, datePublishFrom, datePublishTo);

            List<LotteryNumber> numbers = new List<LotteryNumber>();
            foreach (LotteryNumber no in number)
            {
                if (no.LotNumber.Length == 40)
                {
                    for (int i = 0; i <= 38; i += 2)
                    {
                        LotteryNumber n = new LotteryNumber();
                        n.DateCreated = no.DateCreated;
                        n.DatePublish = no.DatePublish;
                        n.DateUpdated = no.DateUpdated;
                        n.LotNumber = no.LotNumber.Substring(i, 2);
                        n.NumberTypeId = no.NumberTypeId;
                        n.NumberWinLevelId = no.NumberWinLevelId;
                        n.KyQuay = no.KyQuay;
                        numbers.Add(n);
                    }
                }
            }

            return numbers;            
        }

        public static SortedDictionary<int?, SortedSet<int>> GetKenoNumberKyquay(List<LotteryNumber> numbers, DateTime datePublishFrom, DateTime datePublishTo)
        {
            SortedDictionary<int?, SortedSet<int>> kyQuays = new SortedDictionary<int?, SortedSet<int>>();
            var kyquay = numbers.GroupBy(x => x.KyQuay).Select(x => x.First().KyQuay);
            foreach (var i in kyquay) 
            {
                SortedSet<int> kenoNumbers = new SortedSet<int>();
                foreach (var j in numbers)
                {
                    if(i == j.KyQuay)
                    {
                        kenoNumbers.Add(Convert.ToInt32(j.LotNumber));
                    }
                }
                kyQuays.Add(i, kenoNumbers);
            }

            return kyQuays;
        }

        public static SortedDictionary<int?, string[]> GetKenoChanleLonNho(DateTime datePublishFrom, DateTime datePublishTo)
        {
            SortedDictionary<int?, string[]> lstChanLeLonNho = new SortedDictionary<int?, string[]>();

            IQueryable<LotteryNumber> lstChanle = Common.GetNumber((int)Enum_NumberWinLevel.GiaiChanLe, (Int16)Enum_NumberType._Keno, datePublishFrom, datePublishTo);
            IQueryable<LotteryNumber> lstLonNho = Common.GetNumber((int)Enum_NumberWinLevel.GiaiLonNho, (Int16)Enum_NumberType._Keno, datePublishFrom, datePublishTo);

            foreach (var i in lstChanle)
            {
                string[] chanlelonnho = new string[2];
                chanlelonnho[0] = i.LotNumber;
                lstChanLeLonNho.Add(i.KyQuay, chanlelonnho);
            }

            foreach (var i in lstLonNho)
            {
                var val = lstChanLeLonNho[i.KyQuay];
                val[1] = i.LotNumber;
            }

            return lstChanLeLonNho;
        }
    }
}

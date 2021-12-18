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
            List<LoterryStatistic> lst = new List<LoterryStatistic>();
            IQueryable<LotteryNumber> number = Common.GetNumber((Int16)Enum_NumberType._Keno, datePublishFrom, datePublishTo);

            List<string> numbers = new List<string>();
            int c = 0;
            foreach (LotteryNumber no in number)
            {               
                if (no.LotNumber.Length == 40)
                {                    
                    for (int i = 2; i <= 40; i += 2)
                    {
                        numbers.Add(no.LotNumber.Substring(c, i));
                        c = i;
                    }                 
                }
                else
                {
                    numbers.Add(no.LotNumber);
                }
            }

            foreach (var a in numbers)
                Console.WriteLine(a);

            return lst;
        }       
    }
}

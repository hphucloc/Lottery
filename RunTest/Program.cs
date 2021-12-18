using LotteryBusiness;
using LotteryDAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RunTest
{
    class Program
    {
        static void Main(string[] args)
        {
            List<LoterryStatistic> lst = new List<LoterryStatistic>();
            IQueryable<LotteryNumber> number = Common.GetNumber((Int16)Enum_NumberType._Keno, DateTime.Now.AddMonths(-3), DateTime.Now);

            //List<string> numbers = new List<string>();         
            foreach (LotteryNumber no in number)
            {
                LotteryNumber n = new LotteryNumber();
                if (no.LotNumber.Length == 40)
                {
                    for (int i = 0; i <= 38; i += 2)
                    {
                        numbers.Add(no.LotNumber.Substring(i, 2));
                        
                    }
                }
                else
                {
                    numbers.Add(no.LotNumber);
                }
            }

            foreach (var a in numbers)
                Console.WriteLine(a);

            Console.ReadLine();
        }
    }
}

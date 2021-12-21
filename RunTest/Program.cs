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

            var a = _KenoTimeLine.GetKenoChanleLonNho
                (DateTime.Now.AddMonths(-3), DateTime.Now);
            foreach (var i in a)
            {
                Console.WriteLine(i.Key);                
                foreach(var j in a[i.Key])
                {
                    Console.Write(j[0] + "\t" +j[1]);
                }               
            }

            Console.ReadLine();
        }
    }
}

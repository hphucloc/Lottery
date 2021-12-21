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
            //var a = _KenoTimeLine.GetKenoChanleLonNho
            //    (DateTime.Now.AddMonths(-3), DateTime.Now);
            //foreach (var i in a)
            //{
            //    Console.WriteLine(i.Key);                
            //    foreach(var j in a[i.Key])
            //    {
            //        Console.Write(j[0] + "\t" +j[1]);
            //    }               
            //}

            var a = _KenoTimeLine.GetKenoNumber(DateTime.Now.AddMonths(-3), DateTime.Now);
            var b = _KenoTimeLine.GetKenoNumberKyquay(a);

            foreach (var i in b.Keys)                
            {
                var c = b[i];
                foreach (var j in c)
                {
                    Console.Write(i + ": " + j + "\t");                    
                }
                Console.WriteLine();
            }

            Console.ReadLine();
        }
    }
}

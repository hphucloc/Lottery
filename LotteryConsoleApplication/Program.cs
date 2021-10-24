using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LotteryApplication
{
    class Program
    {       
        static void Main(string[] args)
        {
            try
            {
                Console.WriteLine("Dang lay 6/45.....");
                _6Over45.Insert(Common.ReadAppConfig("6Over45URL"));

                Console.WriteLine("Dang lay 6/55.....");
                _6Over55.Insert(Common.ReadAppConfig("6Over55URL"));

                //Console.WriteLine("Dang lay 4dMax.....");
                //Max4d.Insert(Common.ReadAppConfig("4dMaxURL"));

                Console.Write("Hoan tat. Nhan phim bat ky de dong.");
                Console.ReadKey();
            }catch(Exception E)
            {
                Console.WriteLine("ERROR: " + E.Message);
                Console.WriteLine("ERROR: " + E.InnerException.Message);
            }
        }
    }

   
}

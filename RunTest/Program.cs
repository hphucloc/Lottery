using LotteryBusiness;
using LotteryDAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RunTest
{
    public class LotteryStatistic1
    {
        //public Int16 NumberTypeId { get; set; }
        //public Int16 NumberWinLevelId { get; set; }
        public int LotNumber { get; set; }

        public List<DateTime> AllDatePublishList = new List<DateTime>();

        public LotteryBusiness.DatePublishList2 DatePublishList = new LotteryBusiness.DatePublishList2();
        //public DateTime DateCreated { get; set; }
        public DateTime DatePublish { get; set; }
        //public System.DateTime DatePublishMax { get; set; }
        //public System.DateTime DatePublishMin { get; set; }
        //public int TotalNumberAppear { get; set; }
        public int TotalNumberAppearInRange { get; set; }
    }

    class Program
    {
        static void Main(string[] args)
        {
            //var a = _KenoTimeLine.GetKenoChanleLonNho
            //    (DateTime.Now.AddMonths(-3), DateTime.Now);
            //foreach (var i in a)
            //{
            //    Console.WriteLine(i.Key);
            //    foreach (var j in a[i.Key])
            //    {
            //        Console.Write(j[0] + "\t" + j[1]);
            //    }
            //}

            //var a = _KenoTimeLine.GetKenoNumber(DateTime.Now.AddMonths(-3), DateTime.Now);
            //var b = _KenoTimeLine.GetKenoNumberKyquay(a);

            //foreach (var i in b.Keys)                
            //{
            //    var c = b[i];
            //    foreach (var j in c)
            //    {
            //        Console.Write(i + ": " + j + "\t");                    
            //    }
            //    Console.WriteLine();
            //}

            //var a = _6Over45TimeLine.Get6Over45Number(DateTime.Now.AddMonths(-3), DateTime.Now).Select(x => new LotteryStatistic1
            //{
            //    LotNumber = Convert.ToInt32(x.LotNumber),
            //    AllDatePublishList = x.AllDatePublishList,
            //    DatePublish = x.DatePublish,
            //    TotalNumberAppearInRange = x.TotalNumberAppearInRange,
            //    DatePublishList = x.DatePublishList
            //}).OrderBy(x => x.LotNumber).ToList();

            //var a = _3DMaxProTimeLine.Get3DMaxProNumberDacBiet("825", DateTime.MinValue, DateTime.MaxValue);

            //foreach (var i in a)
            // Console.WriteLine(i.LotNumber);

            //var a = Common.GetNumbersNextAppear(6, "2", 1, 1);

            //foreach (var i in a)
            //    Console.WriteLine(i.LotNumber + ", " + i.NextPublishDate);

            var a = _6Over45TimeLine.GetNumberNextAppear("2");

            foreach (var i in a)
                Console.WriteLine(i);

            Console.ReadLine();
        }
    }
}

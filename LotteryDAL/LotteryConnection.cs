using System;

namespace LotteryDAL
{
    public class LotteryConnection
    {
        private static LotteryEntities lazy = new LotteryEntities();

        public static LotteryEntities Instance { get; set; }

        private LotteryConnection()
        {
        }
    }
}

using System;

namespace LotteryDAL
{
    public class LotteryConnection
    {
        private static Lazy<LotteryEntities> lazy = new Lazy<LotteryEntities>(() => new LotteryEntities());

        public static LotteryEntities Instance { get { return lazy.Value; } }

        private LotteryConnection()
        {
        }
    }
}

namespace LotteryDAL
{
    public class LotteryConnection
    {
        public static LotteryEntities Create()
        {
            return new LotteryEntities();
        }

        private LotteryConnection()
        {
        }
    }
}

using LotteryDAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LotteryBusiness
{
    public class _3DMaxTimeLine
    {
        public static List<LoterryStatistic> Get3DMaxNumberDacBiet(DateTime datePublishFrom, DateTime datePublishTo)
        {
            IQueryable<LotteryNumber> number = Common.GetNumber((Int16)Enum_NumberWinLevel.DacBiet, (Int16)Enum_NumberType._3DMax, datePublishFrom, datePublishTo);

            List<LotteryNumber> numbers = new List<LotteryNumber>();
            foreach (LotteryNumber no in number)
            {
                if (no.LotNumber.Length == 6)
                {
                    for (int i = 0; i <= 3; i += 3)
                    {
                        LotteryNumber n = new LotteryNumber();
                        n.DateCreated = no.DateCreated;
                        n.DatePublish = no.DatePublish;
                        n.DateUpdated = no.DateUpdated;
                        n.LotNumber = no.LotNumber.Substring(i, 3);
                        n.NumberTypeId = no.NumberTypeId;
                        n.NumberWinLevelId = no.NumberWinLevelId;
                        n.KyQuay = no.KyQuay;
                        if(n.LotNumber.Length==3)
                            numbers.Add(n);
                    }
                }
            }

            return Common.GetLotNumberStatisticKeno(numbers.AsQueryable());
        }

        public static List<LoterryStatistic> Get3DMaxNumberDacBiet(string getNumber, DateTime datePublishFrom, DateTime datePublishTo)
        {
            List<LoterryStatistic> number = Get3DMaxNumberDacBiet(datePublishFrom, datePublishTo).Where(x => x.LotNumber == getNumber).ToList();

            List<LotteryNumber> numbers = new List<LotteryNumber>();
            foreach (LoterryStatistic no in number)
            {
                
            }

            return Common.GetLotNumberStatisticKeno(numbers.AsQueryable());
        }

        public static List<LoterryStatistic> Get3DMaxNumberGiaiNhat(DateTime datePublishFrom, DateTime datePublishTo)
        {
            IQueryable<LotteryNumber> number = Common.GetNumber((Int16)Enum_NumberWinLevel.GiaiNhat, (Int16)Enum_NumberType._3DMax, datePublishFrom, datePublishTo);
            List<LotteryNumber> numbers = new List<LotteryNumber>();
            foreach (LotteryNumber no in number)
            {
                if (no.LotNumber.Length == 12)
                {
                    for (int i = 0; i <= 9; i += 3)
                    {
                        LotteryNumber n = new LotteryNumber();
                        n.DateCreated = no.DateCreated;
                        n.DatePublish = no.DatePublish;
                        n.DateUpdated = no.DateUpdated;
                        n.LotNumber = no.LotNumber.Substring(i, 3);
                        n.NumberTypeId = no.NumberTypeId;
                        n.NumberWinLevelId = no.NumberWinLevelId;
                        n.KyQuay = no.KyQuay;
                        if (n.LotNumber.Length == 3)
                            numbers.Add(n);
                    }
                }
            }

            return Common.GetLotNumberStatisticKeno(numbers.AsQueryable());
        }

        public static List<LoterryStatistic> Get3DMaxNumberGiaiNhat(string getNumber, DateTime datePublishFrom, DateTime datePublishTo)
        {
            IQueryable<LotteryNumber> number = Common.GetNumber((Int16)Enum_NumberWinLevel.GiaiNhat, 
                (Int16)Enum_NumberType._3DMax, datePublishFrom, datePublishTo).Where(x => x.LotNumber == getNumber);
            List<LotteryNumber> numbers = new List<LotteryNumber>();
            foreach (LotteryNumber no in number)
            {
                if (no.LotNumber.Length == 12)
                {
                    for (int i = 0; i <= 9; i += 3)
                    {
                        LotteryNumber n = new LotteryNumber();
                        n.DateCreated = no.DateCreated;
                        n.DatePublish = no.DatePublish;
                        n.DateUpdated = no.DateUpdated;
                        n.LotNumber = no.LotNumber.Substring(i, 3);
                        n.NumberTypeId = no.NumberTypeId;
                        n.NumberWinLevelId = no.NumberWinLevelId;
                        n.KyQuay = no.KyQuay;
                        if (n.LotNumber.Length == 3)
                            numbers.Add(n);
                    }
                }
            }

            return Common.GetLotNumberStatisticKeno(numbers.AsQueryable());
        }

        public static List<LoterryStatistic> Get3DMaxNumberGiaiNhi(DateTime datePublishFrom, DateTime datePublishTo)
        {
            IQueryable<LotteryNumber> number = Common.GetNumber((Int16)Enum_NumberWinLevel.GiaiNhi, (Int16)Enum_NumberType._3DMax, datePublishFrom, datePublishTo);
            List<LotteryNumber> numbers = new List<LotteryNumber>();
            foreach (LotteryNumber no in number)
            {
                if (no.LotNumber.Length == 18)
                {
                    for (int i = 0; i <= 15; i += 3)
                    {
                        LotteryNumber n = new LotteryNumber();
                        n.DateCreated = no.DateCreated;
                        n.DatePublish = no.DatePublish;
                        n.DateUpdated = no.DateUpdated;
                        n.LotNumber = no.LotNumber.Substring(i, 3);
                        n.NumberTypeId = no.NumberTypeId;
                        n.NumberWinLevelId = no.NumberWinLevelId;
                        n.KyQuay = no.KyQuay;
                        if (n.LotNumber.Length == 3)
                            numbers.Add(n);
                    }
                }
            }

            return Common.GetLotNumberStatisticKeno(numbers.AsQueryable());           
        }

        public static List<LoterryStatistic> Get3DMaxNumberGiaiNhi(string getNumber, DateTime datePublishFrom, DateTime datePublishTo)
        {
            IQueryable<LotteryNumber> number = Common.GetNumber((Int16)Enum_NumberWinLevel.GiaiNhi,
                (Int16)Enum_NumberType._3DMax, datePublishFrom, datePublishTo).Where(x => x.LotNumber == getNumber);
            List<LotteryNumber> numbers = new List<LotteryNumber>();
            foreach (LotteryNumber no in number)
            {
                if (no.LotNumber.Length == 18)
                {
                    for (int i = 0; i <= 15; i += 3)
                    {
                        LotteryNumber n = new LotteryNumber();
                        n.DateCreated = no.DateCreated;
                        n.DatePublish = no.DatePublish;
                        n.DateUpdated = no.DateUpdated;
                        n.LotNumber = no.LotNumber.Substring(i, 3);
                        n.NumberTypeId = no.NumberTypeId;
                        n.NumberWinLevelId = no.NumberWinLevelId;
                        n.KyQuay = no.KyQuay;
                        if (n.LotNumber.Length == 3)
                            numbers.Add(n);
                    }
                }
            }

            return Common.GetLotNumberStatisticKeno(numbers.AsQueryable());
        }

        public static List<LoterryStatistic> Get3DMaxNumberGiaiBa(DateTime datePublishFrom, DateTime datePublishTo)
        {
            IQueryable<LotteryNumber> number = Common.GetNumber((Int16)Enum_NumberWinLevel.GiaiBa, (Int16)Enum_NumberType._3DMax, datePublishFrom, datePublishTo);

            List<LotteryNumber> numbers = new List<LotteryNumber>();
            foreach (LotteryNumber no in number)
            {
                if (no.LotNumber.Length == 24)
                {
                    for (int i = 0; i <= 21; i += 3)
                    {
                        LotteryNumber n = new LotteryNumber();
                        n.DateCreated = no.DateCreated;
                        n.DatePublish = no.DatePublish;
                        n.DateUpdated = no.DateUpdated;
                        n.LotNumber = no.LotNumber.Substring(i, 3);
                        n.NumberTypeId = no.NumberTypeId;
                        n.NumberWinLevelId = no.NumberWinLevelId;
                        n.KyQuay = no.KyQuay;
                        if (n.LotNumber.Length == 3)
                            numbers.Add(n);
                    }
                }
            }

            return Common.GetLotNumberStatisticKeno(numbers.AsQueryable());
        }

        public static List<LoterryStatistic> Get3DMaxNumberGiaiBa(string getNumber, DateTime datePublishFrom, DateTime datePublishTo)
        {
            IQueryable<LotteryNumber> number = Common.GetNumber((Int16)Enum_NumberWinLevel.GiaiBa,
                (Int16)Enum_NumberType._3DMax, datePublishFrom, datePublishTo).Where(x => x.LotNumber == getNumber);

            List<LotteryNumber> numbers = new List<LotteryNumber>();
            foreach (LotteryNumber no in number)
            {
                if (no.LotNumber.Length == 24)
                {
                    for (int i = 0; i <= 21; i += 3)
                    {
                        LotteryNumber n = new LotteryNumber();
                        n.DateCreated = no.DateCreated;
                        n.DatePublish = no.DatePublish;
                        n.DateUpdated = no.DateUpdated;
                        n.LotNumber = no.LotNumber.Substring(i, 3);
                        n.NumberTypeId = no.NumberTypeId;
                        n.NumberWinLevelId = no.NumberWinLevelId;
                        n.KyQuay = no.KyQuay;
                        if (n.LotNumber.Length == 3)
                            numbers.Add(n);
                    }
                }
            }

            return Common.GetLotNumberStatisticKeno(numbers.AsQueryable());
        }
    }
}

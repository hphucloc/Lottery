using LotteryDAL;
using System;
using System.Collections.Generic;
using System.Linq;


namespace LotteryBusiness
{
    public class _6Over45TimeLine
    {
        public static List<LoterryStatistic> Get6Over45Number(DateTime datePublishFrom, DateTime datePublishTo)
        {
            IQueryable<LotteryNumber> number = Common.GetNumber((Int16)Enum_NumberWinLevel.DacBiet, (Int16)Enum_NumberType._6Over45, datePublishFrom, datePublishTo);
            return Common.GetLotNumberStatistic(number);            
        }

        public static List<LoterryStatistic> GetLatest6Over45Number()
        {
            IQueryable<LotteryNumber> number = Common.GetNumber((Int16)Enum_NumberWinLevel.DacBiet, (Int16)Enum_NumberType._6Over45, DateTime.Now.AddDays(-7), DateTime.Now);          
            return Common.GetLotNumberStatistic(number.OrderByDescending(x=>x.DatePublish).Take(6));
        }

        public static List<LoterryStatistic> Get6Over45Number(string lotNumber,DateTime datePublishFrom, DateTime datePublishTo)
        {
            IQueryable<LotteryNumber> number = Common.GetNumber(lotNumber, (Int16)Enum_NumberWinLevel.DacBiet, (Int16)Enum_NumberType._6Over45, datePublishFrom, datePublishTo);
            return Common.GetLotNumberStatistic(number);
        }          

        public static List<int> GetNumberNotAppearInTime(int number, List<LoterryStatistic> _6Over45No)
        {
            List<int> numberNotAppear = new List<int>();

            return numberNotAppear;
        }

        public static void NewNumber(DateTime publishDdate, List<string> number)
        {
            Common.NewNumber(publishDdate, number, (Int16)Enum_NumberType._6Over45, (Int16)Enum_NumberWinLevel.DacBiet);
        }

        public static List<string> GetNumberNextAppear(string number)
        {
            List<string> rdata = new List<string>();
            var data = Common.GetNumbersNextAppear(6, 1, 1);

            var lstDateNextAppear = data.Where(x => x.LotNumber == number && x.NextPublishDate != null).Select(x => x.NextPublishDate);

            //rdata = data.Where(x => lstDateNextAppear.Contains(x.DatePublish)).Select(x => x.LotNumber).ToList();
            foreach (var i in lstDateNextAppear)
            {
                rdata.AddRange(data.Where(x => x.DatePublish.Date == i.Value.Date).Select(x => x.LotNumber));
            }

            return rdata.OrderBy(x => Convert.ToInt32(x)).ToList();
        }

        public static Dictionary<DateTime, List<string>> GetColorNextAppear()
        {
            /* 1/ Xac dinh mau cua lastestNumber
             * 2/ Loop qua moi 6 so cua data
               3/ Kiem tra 6 sodo co thuoc mau do ko 
               4/ neu thuocmaudo -> lay nextDateAppear luu vao List<Date>
               5/ Convert cac so do ra List<Color>
            */

            List<DateTime> lstNextDateAppear = new List<DateTime>();
            var data = Common.GetNumbersNextAppear(6, 1, 1);
            var latestNumber = GetLatest6Over45Number().OrderBy(x => Convert.ToInt32(x.LotNumber)).ToList();

            string s1 = null, s2 = null, s3 = null, s4 = null, s5 = null, s6 = null;
            for (var i = 0; i < latestNumber.Count(); i++)
            {
                switch (i)
                {
                    case 0:
                        if(Convert.ToInt32(latestNumber[i].LotNumber) > 0 && Convert.ToInt32(latestNumber[i].LotNumber) <= 7)
                        {
                            s1 = "0-7";
                        }
                        if (Convert.ToInt32(latestNumber[i].LotNumber) > 7 && Convert.ToInt32(latestNumber[i].LotNumber) <= 15)
                        {
                            s1 = "7-15";
                        }
                        if (Convert.ToInt32(latestNumber[i].LotNumber) > 15 && Convert.ToInt32(latestNumber[i].LotNumber) <= 23)
                        {
                            s1 = "15-23";
                        }
                        if (Convert.ToInt32(latestNumber[i].LotNumber) > 23 && Convert.ToInt32(latestNumber[i].LotNumber) <= 31)
                        {
                            s1 = "23-31";
                        }
                        if (Convert.ToInt32(latestNumber[i].LotNumber) > 31 && Convert.ToInt32(latestNumber[i].LotNumber) <= 39)
                        {
                            s1 = "31-39";
                        }
                        if (Convert.ToInt32(latestNumber[i].LotNumber) > 39 && Convert.ToInt32(latestNumber[i].LotNumber) <= 47)
                        {
                            s1 = "39-47";
                        }
                        
                        break;
                    case 1:
                        if (Convert.ToInt32(latestNumber[i].LotNumber) > 0 && Convert.ToInt32(latestNumber[i].LotNumber) <= 7)
                        {
                            s2 = "0-7";
                        }
                        if (Convert.ToInt32(latestNumber[i].LotNumber) > 7 && Convert.ToInt32(latestNumber[i].LotNumber) <= 15)
                        {
                            s2 = "7-15";
                        }
                        if (Convert.ToInt32(latestNumber[i].LotNumber) > 15 && Convert.ToInt32(latestNumber[i].LotNumber) <= 23)
                        {
                            s2 = "15-23";
                        }
                        if (Convert.ToInt32(latestNumber[i].LotNumber) > 23 && Convert.ToInt32(latestNumber[i].LotNumber) <= 31)
                        {
                            s2 = "23-31";
                        }
                        if (Convert.ToInt32(latestNumber[i].LotNumber) > 31 && Convert.ToInt32(latestNumber[i].LotNumber) <= 39)
                        {
                            s2 = "31-39";
                        }
                        if (Convert.ToInt32(latestNumber[i].LotNumber) > 39 && Convert.ToInt32(latestNumber[i].LotNumber) <= 47)
                        {
                            s2 = "39-47";
                        }
                        break;
                    case 2:
                        if (Convert.ToInt32(latestNumber[i].LotNumber) > 0 && Convert.ToInt32(latestNumber[i].LotNumber) <= 7)
                        {
                            s3 = "0-7";
                        }
                        if (Convert.ToInt32(latestNumber[i].LotNumber) > 7 && Convert.ToInt32(latestNumber[i].LotNumber) <= 15)
                        {
                            s3 = "7-15";
                        }
                        if (Convert.ToInt32(latestNumber[i].LotNumber) > 15 && Convert.ToInt32(latestNumber[i].LotNumber) <= 23)
                        {
                            s3 = "15-23";
                        }
                        if (Convert.ToInt32(latestNumber[i].LotNumber) > 23 && Convert.ToInt32(latestNumber[i].LotNumber) <= 31)
                        {
                            s3 = "23-31";
                        }
                        if (Convert.ToInt32(latestNumber[i].LotNumber) > 31 && Convert.ToInt32(latestNumber[i].LotNumber) <= 39)
                        {
                            s3 = "31-39";
                        }
                        if (Convert.ToInt32(latestNumber[i].LotNumber) > 39 && Convert.ToInt32(latestNumber[i].LotNumber) <= 47)
                        {
                            s3 = "39-47";
                        }
                        break;
                    case 3:
                        if (Convert.ToInt32(latestNumber[i].LotNumber) > 0 && Convert.ToInt32(latestNumber[i].LotNumber) <= 7)
                        {
                            s4 = "0-7";
                        }
                        if (Convert.ToInt32(latestNumber[i].LotNumber) > 7 && Convert.ToInt32(latestNumber[i].LotNumber) <= 15)
                        {
                            s4 = "7-15";
                        }
                        if (Convert.ToInt32(latestNumber[i].LotNumber) > 15 && Convert.ToInt32(latestNumber[i].LotNumber) <= 23)
                        {
                            s4 = "15-23";
                        }
                        if (Convert.ToInt32(latestNumber[i].LotNumber) > 23 && Convert.ToInt32(latestNumber[i].LotNumber) <= 31)
                        {
                            s4 = "23-31";
                        }
                        if (Convert.ToInt32(latestNumber[i].LotNumber) > 31 && Convert.ToInt32(latestNumber[i].LotNumber) <= 39)
                        {
                            s4 = "31-39";
                        }
                        if (Convert.ToInt32(latestNumber[i].LotNumber) > 39 && Convert.ToInt32(latestNumber[i].LotNumber) <= 47)
                        {
                            s4 = "39-47";
                        }
                        break;
                    case 4:
                        if (Convert.ToInt32(latestNumber[i].LotNumber) > 0 && Convert.ToInt32(latestNumber[i].LotNumber) <= 7)
                        {
                            s5 = "0-7";
                        }
                        if (Convert.ToInt32(latestNumber[i].LotNumber) > 7 && Convert.ToInt32(latestNumber[i].LotNumber) <= 15)
                        {
                            s5 = "7-15";
                        }
                        if (Convert.ToInt32(latestNumber[i].LotNumber) > 15 && Convert.ToInt32(latestNumber[i].LotNumber) <= 23)
                        {
                            s5 = "15-23";
                        }
                        if (Convert.ToInt32(latestNumber[i].LotNumber) > 23 && Convert.ToInt32(latestNumber[i].LotNumber) <= 31)
                        {
                            s5 = "23-31";
                        }
                        if (Convert.ToInt32(latestNumber[i].LotNumber) > 31 && Convert.ToInt32(latestNumber[i].LotNumber) <= 39)
                        {
                            s5 = "31-39";
                        }
                        if (Convert.ToInt32(latestNumber[i].LotNumber) > 39 && Convert.ToInt32(latestNumber[i].LotNumber) <= 47)
                        {
                            s5 = "39-47";
                        }
                        break;
                    case 5:
                        if (Convert.ToInt32(latestNumber[i].LotNumber) > 0 && Convert.ToInt32(latestNumber[i].LotNumber) <= 7)
                        {
                            s6 = "0-7";
                        }
                        if (Convert.ToInt32(latestNumber[i].LotNumber) > 7 && Convert.ToInt32(latestNumber[i].LotNumber) <= 15)
                        {
                            s6 = "7-15";
                        }
                        if (Convert.ToInt32(latestNumber[i].LotNumber) > 15 && Convert.ToInt32(latestNumber[i].LotNumber) <= 23)
                        {
                            s6 = "15-23";
                        }
                        if (Convert.ToInt32(latestNumber[i].LotNumber) > 23 && Convert.ToInt32(latestNumber[i].LotNumber) <= 31)
                        {
                            s6 = "23-31";
                        }
                        if (Convert.ToInt32(latestNumber[i].LotNumber) > 31 && Convert.ToInt32(latestNumber[i].LotNumber) <= 39)
                        {
                            s6 = "31-39";
                        }
                        if (Convert.ToInt32(latestNumber[i].LotNumber) > 39 && Convert.ToInt32(latestNumber[i].LotNumber) <= 47)
                        {
                            s6 = "39-47";
                        }
                        break;
                    default:
                        break;
                }            
            }
            string pattern1 = string.Format("{0}/{1}/{2}/{3}/{4}/{5}", s1, s2, s3, s4, s5, s6);
          
            string a1 = null, a2 = null, a3 = null, a4 = null, a5 = null, a6 = null;
            for (var i = 0; i < data.Count(); i += 6)
            {
                if (i + 6 < data.Count)
                {
                    //a1
                    if (Convert.ToInt32(data[i].LotNumber) > 0 && Convert.ToInt32(data[i].LotNumber) <= 7)
                    {
                        a1 = "0-7";
                    }
                    else
                if (Convert.ToInt32(data[i].LotNumber) > 7 && Convert.ToInt32(data[i].LotNumber) <= 15)
                    {
                        a1 = "7-15";
                    }
                    else
                if (Convert.ToInt32(data[i].LotNumber) > 15 && Convert.ToInt32(data[i].LotNumber) <= 23)
                    {
                        a1 = "15-23";
                    }
                    else
                if (Convert.ToInt32(data[i].LotNumber) > 23 && Convert.ToInt32(data[i].LotNumber) <= 31)
                    {
                        a1 = "23-31";
                    }
                    else
                if (Convert.ToInt32(data[i].LotNumber) > 31 && Convert.ToInt32(data[i].LotNumber) <= 39)
                    {
                        a1 = "31-39";
                    }
                    else
                if (Convert.ToInt32(data[i].LotNumber) > 39 && Convert.ToInt32(data[i].LotNumber) <= 47)
                    {
                        a1 = "39-47";
                    }

                    //a2
                    if (Convert.ToInt32(data[i + 1].LotNumber) > 0 && Convert.ToInt32(data[i + 1].LotNumber) <= 7)
                    {
                        a2 = "0-7";
                    }
                    else
                    if (Convert.ToInt32(data[i + 1].LotNumber) > 7 && Convert.ToInt32(data[i + 1].LotNumber) <= 15)
                    {
                        a2 = "7-15";
                    }
                    else
                    if (Convert.ToInt32(data[i + 1].LotNumber) > 15 && Convert.ToInt32(data[i + 1].LotNumber) <= 23)
                    {
                        a2 = "15-23";
                    }
                    else
                    if (Convert.ToInt32(data[i + 1].LotNumber) > 23 && Convert.ToInt32(data[i + 1].LotNumber) <= 31)
                    {
                        a2 = "23-31";
                    }
                    else
                    if (Convert.ToInt32(data[i + 1].LotNumber) > 31 && Convert.ToInt32(data[i + 1].LotNumber) <= 39)
                    {
                        a2 = "31-39";
                    }
                    else
                    if (Convert.ToInt32(data[i + 1].LotNumber) > 39 && Convert.ToInt32(data[i + 1].LotNumber) <= 47)
                    {
                        a2 = "39-47";
                    }
                    //a3
                    if (Convert.ToInt32(data[i + 2].LotNumber) > 0 && Convert.ToInt32(data[i + 2].LotNumber) <= 7)
                    {
                        a3 = "0-7";
                    }
                    else
                    if (Convert.ToInt32(data[i + 2].LotNumber) > 7 && Convert.ToInt32(data[i + 2].LotNumber) <= 15)
                    {
                        a3 = "7-15";
                    }
                    else
                    if (Convert.ToInt32(data[i + 2].LotNumber) > 15 && Convert.ToInt32(data[i + 2].LotNumber) <= 23)
                    {
                        a3 = "15-23";
                    }
                    else
                    if (Convert.ToInt32(data[i + 2].LotNumber) > 23 && Convert.ToInt32(data[i + 2].LotNumber) <= 31)
                    {
                        a3 = "23-31";
                    }
                    else
                    if (Convert.ToInt32(data[i + 2].LotNumber) > 31 && Convert.ToInt32(data[i + 2].LotNumber) <= 39)
                    {
                        a3 = "31-39";
                    }
                    else
                    if (Convert.ToInt32(data[i + 2].LotNumber) > 39 && Convert.ToInt32(data[i + 2].LotNumber) <= 47)
                    {
                        a3 = "39-47";
                    }

                    //a4
                    if (Convert.ToInt32(data[i + 3].LotNumber) > 0 && Convert.ToInt32(data[i + 3].LotNumber) <= 7)
                    {
                        a4 = "0-7";
                    }
                    else
                    if (Convert.ToInt32(data[i + 3].LotNumber) > 7 && Convert.ToInt32(data[i + 3].LotNumber) <= 15)
                    {
                        a4 = "7-15";
                    }
                    else
                    if (Convert.ToInt32(data[i + 3].LotNumber) > 15 && Convert.ToInt32(data[i + 3].LotNumber) <= 23)
                    {
                        a4 = "15-23";
                    }
                    else
                    if (Convert.ToInt32(data[i + 3].LotNumber) > 23 && Convert.ToInt32(data[i + 3].LotNumber) <= 31)
                    {
                        a4 = "23-31";
                    }
                    else
                    if (Convert.ToInt32(data[i + 3].LotNumber) > 31 && Convert.ToInt32(data[i + 3].LotNumber) <= 39)
                    {
                        a4 = "31-39";
                    }
                    else
                    if (Convert.ToInt32(data[i + 3].LotNumber) > 39 && Convert.ToInt32(data[i + 3].LotNumber) <= 47)
                    {
                        a4 = "39-47";
                    }

                    //a5
                    if (Convert.ToInt32(data[i + 4].LotNumber) > 0 && Convert.ToInt32(data[i + 4].LotNumber) <= 7)
                    {
                        a5 = "0-7";
                    }
                    else
                    if (Convert.ToInt32(data[i + 4].LotNumber) > 7 && Convert.ToInt32(data[i + 4].LotNumber) <= 15)
                    {
                        a5 = "7-15";
                    }
                    else
                    if (Convert.ToInt32(data[i + 4].LotNumber) > 15 && Convert.ToInt32(data[i + 4].LotNumber) <= 23)
                    {
                        a5 = "15-23";
                    }
                    else
                    if (Convert.ToInt32(data[i + 4].LotNumber) > 23 && Convert.ToInt32(data[i + 4].LotNumber) <= 31)
                    {
                        a5 = "23-31";
                    }
                    else
                    if (Convert.ToInt32(data[i + 4].LotNumber) > 31 && Convert.ToInt32(data[i + 4].LotNumber) <= 39)
                    {
                        a5 = "31-39";
                    }
                    else
                    if (Convert.ToInt32(data[i + 4].LotNumber) > 39 && Convert.ToInt32(data[i + 4].LotNumber) <= 47)
                    {
                        a5 = "39-47";
                    }

                    //a6
                    if (Convert.ToInt32(data[i + 5].LotNumber) > 0 && Convert.ToInt32(data[i + 5].LotNumber) <= 7)
                    {
                        a6 = "0-7";
                    }
                    else
                    if (Convert.ToInt32(data[i + 5].LotNumber) > 7 && Convert.ToInt32(data[i + 5].LotNumber) <= 15)
                    {
                        a6 = "7-15";
                    }
                    else
                    if (Convert.ToInt32(data[i + 5].LotNumber) > 15 && Convert.ToInt32(data[i + 5].LotNumber) <= 23)
                    {
                        a6 = "15-23";
                    }
                    else
                    if (Convert.ToInt32(data[i + 5].LotNumber) > 23 && Convert.ToInt32(data[i + 5].LotNumber) <= 31)
                    {
                        a6 = "23-31";
                    }
                    else
                    if (Convert.ToInt32(data[i + 5].LotNumber) > 31 && Convert.ToInt32(data[i + 5].LotNumber) <= 39)
                    {
                        a6 = "31-39";
                    }
                    else
                    if (Convert.ToInt32(data[i + 5].LotNumber) > 39 && Convert.ToInt32(data[i + 5].LotNumber) <= 47)
                    {
                        a6 = "39-47";
                    }

                    //Get NextPublishDate
                    string pattern2 = string.Format("{0}/{1}/{2}/{3}/{4}/{5}", a1, a2, a3, a4, a5, a6);
                    if (pattern1 == pattern2 && data[i].NextPublishDate != null)
                    {
                        lstNextDateAppear.Add(data[i].NextPublishDate.Value.Date);
                    }
                }
            }

            Dictionary<DateTime, List<int>> dicNumberNextAppearExactly = new Dictionary<DateTime, List<int>>();
            foreach (var i in lstNextDateAppear)
                dicNumberNextAppearExactly.Add(i.Date,
                    data.Where(x => x.DatePublish.Date == i.Date).OrderBy(x => Convert.ToInt32(x.LotNumber))
                        .Select(x => Convert.ToInt32(x.LotNumber)).ToList());

            Dictionary<DateTime, List<string>> dicColorNextAppear = new Dictionary<DateTime, List<string>>();         
            foreach (var i in dicNumberNextAppearExactly.Keys)
            {
                List<string> lstColor = new List<string>();
                foreach (var j in dicNumberNextAppearExactly[i])
                {
                    if (j > 0 && j <= 7)
                    {
                        lstColor.Add("Do");
                    }
                    else
                    if (j > 7 && j <= 15)
                    {
                        lstColor.Add("Cam");
                    }
                    else
                    if (j > 15 && j <= 23)
                    {
                        lstColor.Add("Vang");
                    }
                    else
                    if (j > 23 && j <= 31)
                    {
                        lstColor.Add("Luc");
                    }
                    else
                    if (j > 31 && j <= 39)
                    {
                        lstColor.Add("Lam");
                    }
                    else
                    if (j > 39 && j <= 47)
                    {
                        lstColor.Add("Cham");
                    }
                }

                dicColorNextAppear.Add(i, lstColor);
            }

            return dicColorNextAppear;
        }

        public static Dictionary<DateTime, List<int>> GetNumberNextAppearExactly()
        {            
            List<DateTime> lstNextDateAppear = new List<DateTime>();
            var data = Common.GetNumbersNextAppear(6, 1, 1);
            var latestNumber = GetLatest6Over45Number().OrderBy(x => x.LotNumber).ToList();
            
            for (var i = 0; i < data.Count(); i += 6)
            {                                          
                if (Convert.ToInt32(data[i].LotNumber) == Convert.ToInt32(latestNumber[0].LotNumber) &&
                    Convert.ToInt32(data[i + 1].LotNumber) == Convert.ToInt32(latestNumber[1].LotNumber) &&
                    Convert.ToInt32(data[i + 2].LotNumber) == Convert.ToInt32(latestNumber[2].LotNumber) &&
                    Convert.ToInt32(data[i + 3].LotNumber) == Convert.ToInt32(latestNumber[3].LotNumber) &&
                    Convert.ToInt32(data[i + 4].LotNumber) == Convert.ToInt32(latestNumber[4].LotNumber) &&
                    Convert.ToInt32(data[i + 5].LotNumber) == Convert.ToInt32(latestNumber[5].LotNumber) &&
                    data[i].NextPublishDate != null)
                {
                    lstNextDateAppear.Add(data[i].NextPublishDate.Value.Date);
                }                              
            }

            Dictionary<DateTime, List<int>> dicNumberNextAppearExactly = new Dictionary<DateTime, List<int>>();
            foreach (var i in lstNextDateAppear)
                dicNumberNextAppearExactly.Add(i.Date, 
                    data.Where(x => x.DatePublish.Date == i.Date).OrderBy(x => Convert.ToInt32(x.LotNumber))
                        .Select(x => Convert.ToInt32(x.LotNumber)).ToList());
                

            return dicNumberNextAppearExactly;
        }

        public static void CreateBoughtNumber(List<int> number, DateTime dateBought, short numberType, short numberWinLevel)
        {
            Common.CreateBoughtNumber(number, dateBought, (Int16)Enum_NumberType._6Over45, (Int16)Enum_NumberWinLevel.DacBiet);
        }
    }
}

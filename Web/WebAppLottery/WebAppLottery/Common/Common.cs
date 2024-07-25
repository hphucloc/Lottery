using HtmlAgilityPack;
using LotteryDAL;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace DataVietlott
{
    class Common
    {
        private static LotteryEntities Db = new LotteryEntities();
        public static string Content(string url)
        {
            //url = "https://vietlott.vn/vi/trung-thuong/ket-qua-trung-thuong/winning-number-keno";
            HtmlWeb web = new HtmlWeb();
            HtmlDocument doc = web.Load(url);

            if (doc.DocumentNode.SelectNodes("//table[@class='table table-hover']") != null)
                foreach (HtmlNode link in doc.DocumentNode.SelectNodes("//table[@class='table table-hover']"))
                {
                    return link.InnerText;
                }
            return null;
        }
        public static List<string> RemoveEmptyElement(string[] input)
        {
            var val = new List<string>();

            val = input.Where(s => !string.IsNullOrWhiteSpace(s)).ToList();

            return val;
        }
        public static bool CheckDateExisted(DateTime date)
        {
            return Db.Numbers.Any(a => a.DatePublish == date.Date);
        }
        public static bool CheckDateExisted(DateTime date, Int16 numberTypeId)
        {
            return Db.Numbers.Any(a => DbFunctions.TruncateTime(a.DatePublish) == date.Date && a.NumberTypeId == numberTypeId);
        }
        public static string ReadAppConfig(string key)
        {
            return System.Configuration.ConfigurationManager.AppSettings[key];
        }
        public static bool CheckKyQuayExisted(int kyQuay, int numberTypeID)
        {
            return Db.Numbers.Any(x=>x.KyQuay == kyQuay);
        }
    }
}

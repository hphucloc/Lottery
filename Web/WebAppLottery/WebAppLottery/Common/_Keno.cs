using HtmlAgilityPack;
using LotteryDAL;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace DataVietlott
{
    public class _Keno
    {
        private static LotteryEntities Db = LotteryDAL.LotteryConnection.Instance;       
        private static List<Number> ConvertListStringToListNumber(List<string> input)
        {
            List<Number> val = new List<Number>();
            CultureInfo cul = new CultureInfo("vi-VN");
            for (int no = 0; no < input.Count; no++)
            {
                if (!Common.CheckDateExisted(Convert.ToDateTime(input[no].Substring(0,10), cul), (Int16)Enum_NumberType._Keno))
                {                   
                    Number aNo = new Number();
                    aNo.DatePublish = Convert.ToDateTime(input[no].Substring(0, 10), cul);
                    aNo.NumberTypeId = (Int16)Enum_NumberType._Keno;
                    aNo.NumberWinLevelId = (Int16)Enum_NumberWinLevel.DacBiet;
                    for(int j = 1; j <= 20; j++)
                    {
                        aNo.LotNumber += input[no + j];
                    }                   
                    aNo.DateCreated = DateTime.Now;
                    val.Add(aNo);

                    aNo = new Number();
                    aNo.DatePublish = Convert.ToDateTime(input[no].Substring(0, 10), cul);
                    aNo.NumberTypeId = (Int16)Enum_NumberType._Keno;
                    aNo.NumberWinLevelId = (Int16)Enum_NumberWinLevel.GiaiChanLe;
                    aNo.LotNumber = input[no + 21] + input[no + 22];
                    aNo.DateCreated = DateTime.Now;
                    val.Add(aNo);

                    aNo = new Number();
                    aNo.DatePublish = Convert.ToDateTime(input[no].Substring(0, 10), cul);
                    aNo.NumberTypeId = (Int16)Enum_NumberType._Keno;
                    aNo.NumberWinLevelId = (Int16)Enum_NumberWinLevel.GiaiLonNho;
                    aNo.LotNumber = input[no + 23] + input[no + 24];
                    aNo.DateCreated = DateTime.Now;
                    val.Add(aNo);
                }
                no += 24;
            }

            return val;
        }        
        public static string Insert(string url)
        {
            string value = null;

            string content = Common.Content(url);
            var array = content.Split("\r\n        ".ToCharArray());
            var list = Common.RemoveEmptyElement(array);

            //Remove 4 first items in list
            for (byte i = 0; i <= 5; i++)
            {
                list.RemoveAt(0);
            }

            int c = 0;
            List<Number> ListNumber = ConvertListStringToListNumber(list);
            foreach (Number lotNumber in ListNumber)
            {
                c++;
                value += lotNumber.LotNumber + " ";
                Db.Numbers.Add(lotNumber);
                Db.Entry(lotNumber).State = System.Data.Entity.EntityState.Added;
               // Db.SaveChanges();

                if (c == 4)
                {
                    value += "\t(" + lotNumber.DatePublish.ToShortDateString() + ")\n\t";
                    c = 0;
                }
            }

            
            return "+ " + DateTime.Now + ", Đã Lấy Keno thành công các số: \n\t" + (string.IsNullOrEmpty(value) ? "none" : value);
        }
    }
}

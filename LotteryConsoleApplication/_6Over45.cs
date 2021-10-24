using HtmlAgilityPack;
using LotteryDAL;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace LotteryApplication
{

    public class _6Over45
    {
        private static LotteryEntities Db = LotteryDAL.LotteryConnection.Instance;       
        private static List<Number> ConvertListStringToListNumber(List<string> input)
        {
            List<Number> val = new List<Number>();
            CultureInfo cul = new CultureInfo("vi-VN");
            for (int no = 0; no < input.Count; no++)
            {
                if (!Common.CheckDateExisted(Convert.ToDateTime(input[no], cul), (short)Enum_NumberType._6Over45))
                {
                    Console.WriteLine("Lay so ngay: " + input[no]);

                    Number aNo = new Number();
                    aNo.DatePublish = Convert.ToDateTime(input[no],cul);
                    aNo.NumberTypeId = (Int16)Enum_NumberType._6Over45;
                    aNo.NumberWinLevelId = (Int16)Enum_NumberWinLevel.DacBiet;
                    aNo.DateCreated = DateTime.Now;
                    aNo.LotNumber = Convert.ToInt32(input[no + 2].Substring(0,2));      
                                
                    val.Add(aNo);
                   
                    aNo = new Number();
                    aNo.DatePublish = Convert.ToDateTime(input[no],cul);
                    aNo.NumberTypeId = (Int16)Enum_NumberType._6Over45;
                    aNo.NumberWinLevelId = (Int16)Enum_NumberWinLevel.DacBiet;
                    aNo.LotNumber = Convert.ToInt32(input[no + 2].Substring(2,2));
                    aNo.DateCreated = DateTime.Now;
                    val.Add(aNo);                   

                    aNo = new Number();
                    aNo.DatePublish = Convert.ToDateTime(input[no],cul);
                    aNo.NumberTypeId = (Int16)Enum_NumberType._6Over45;
                    aNo.NumberWinLevelId = (Int16)Enum_NumberWinLevel.DacBiet;
                    aNo.LotNumber = Convert.ToInt32(input[no + 2].Substring(4,2));
                    aNo.DateCreated = DateTime.Now;
                    val.Add(aNo);

                    aNo = new Number();
                    aNo.DatePublish = Convert.ToDateTime(input[no],cul);
                    aNo.NumberTypeId = (Int16)Enum_NumberType._6Over45;
                    aNo.NumberWinLevelId = (Int16)Enum_NumberWinLevel.DacBiet;
                    aNo.LotNumber = Convert.ToInt32(input[no + 2].Substring(6,2));
                    aNo.DateCreated = DateTime.Now;
                    val.Add(aNo);

                    aNo = new Number();
                    aNo.DatePublish = Convert.ToDateTime(input[no],cul);
                    aNo.NumberTypeId = (Int16)Enum_NumberType._6Over45;
                    aNo.NumberWinLevelId = (Int16)Enum_NumberWinLevel.DacBiet;
                    aNo.LotNumber = Convert.ToInt32(input[no + 2].Substring(8, 2));
                    aNo.DateCreated = DateTime.Now;
                    val.Add(aNo);

                    aNo = new Number();
                    aNo.DatePublish = Convert.ToDateTime(input[no],cul);
                    aNo.NumberTypeId = (Int16)Enum_NumberType._6Over45;
                    aNo.NumberWinLevelId = (Int16)Enum_NumberWinLevel.DacBiet;
                    aNo.LotNumber = Convert.ToInt32(input[no + 2].Substring(10, 2));
                    aNo.DateCreated = DateTime.Now;
                    val.Add(aNo);                    
                }

                no += 5;
            }

            return val;
        }        
        public static void Insert(string url)
        {
            string content = Common.Content(url);
            var array = content.Split("\r\n        ".ToCharArray());
            var list = Common.RemoveEmptyElement(array);

            //Remove 3 items in list
            for (byte i = 0; i <= 3; i++)
            {
                list.RemoveAt(0);
            }

            List<Number> ListNumber = ConvertListStringToListNumber(list);
            foreach (Number lotNumber in ListNumber)
            {
                Db.Numbers.Add(lotNumber);
                Db.Entry(lotNumber).State = System.Data.Entity.EntityState.Added;
            }

            Db.SaveChanges();

        }
    }
}

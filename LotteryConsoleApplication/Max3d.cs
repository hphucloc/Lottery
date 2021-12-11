using HtmlAgilityPack;
using LotteryDAL;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace LotteryApplication
{
    class Max4d
    {
        private static LotteryEntities Db = LotteryDAL.LotteryConnection.Instance;
        private static List<Number> ConvertListStringToListNumber(List<string> input)
        {
            List<Number> val = new List<Number>();
            CultureInfo cul = new CultureInfo("vi-VN");
            for (int no = 0; no < input.Count; no++)
            {
                //if (!Common.CheckDateExisted(Convert.ToDateTime(input[no],cul), (Int16) Enum_NumberType._3DMax))
                //{
                //    Console.WriteLine(input[no]);

                //    Number aNo = new Number();
                //    aNo.DatePublish = Convert.ToDateTime(input[no], cul);
                //    aNo.NumberTypeId = (Int16)Enum_NumberType._3DMax;
                //    aNo.NumberWinLevelId = (Int16)Enum_NumberWinLevel.DacBiet;
                //    aNo.LotNumber = Convert.ToInt32(input[no + 4] + input[no + 8]);
                //    aNo.DateCreated = DateTime.Now;
                //    val.Add(aNo);

                //    aNo = new Number();
                //    aNo.DatePublish = Convert.ToDateTime(input[no],cul);
                //    aNo.NumberTypeId = (Int16)Enum_NumberType._3DMax;
                //    aNo.NumberWinLevelId = (Int16)Enum_NumberWinLevel.GiaiNhat;
                //    aNo.LotNumber = Convert.ToInt32(input[no + 14] + input[no + 18] + input[no + 22] + input[no + 26]);
                //    aNo.DateCreated = DateTime.Now;
                //    val.Add(aNo);

                //    aNo = new Number();
                //    aNo.DatePublish = Convert.ToDateTime(input[no],cul);
                //    aNo.NumberTypeId = (Int16)Enum_NumberType._3DMax;
                //    aNo.NumberWinLevelId = (Int16)Enum_NumberWinLevel.GiaiNhi;
                //    aNo.LotNumber = Convert.ToInt32(input[no + 32] + input[no + 36] + input[no + 40] + input[no + 44] + input[no + 48] + input[no + 52]);
                //    aNo.DateCreated = DateTime.Now;
                //    val.Add(aNo);

                //    aNo = new Number();
                //    aNo.DatePublish = Convert.ToDateTime(input[no], cul);
                //    aNo.NumberTypeId = (Int16)Enum_NumberType._3DMax;
                //    aNo.NumberWinLevelId = (Int16)Enum_NumberWinLevel.GiaiBa;
                //    aNo.LotNumber = Convert.ToInt32(input[no + 58] + input[no + 62] + input[no + 66] + input[no + 70] + input[no + 74] + input[no + 78] + input[no + 82] + input[no + 86]);
                //    aNo.DateCreated = DateTime.Now;
                //    val.Add(aNo);

                    
                //}

                no += 12;
            }

            return val;
        }
        public static void Insert(string url)
        {
            string content = Common.Content(url);
            var array = content.Split("\r\n        ".ToCharArray());
            var list = Common.RemoveEmptyElement(array);

            //Remove 4 items in list
            for (byte i = 0; i <= 4; i++)
            {
                list.RemoveAt(0);
            }

            List<Number> ListNumber = ConvertListStringToListNumber(list);
            foreach (Number lotNumber in ListNumber)
            {
                Db.Numbers.Add(lotNumber);
                Db.Entry(lotNumber).State = System.Data.Entity.EntityState.Added;
                Console.WriteLine(lotNumber + "\t");
            }

          //  Db.SaveChanges();

        }
    }
}

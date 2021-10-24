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
                if (!Common.CheckDateExisted(Convert.ToDateTime(input[no],cul), (Int16) Enum_NumberType._4DMax))
                {
                    Console.WriteLine(input[no]);

                    Number aNo = new Number();
                    aNo.DatePublish = Convert.ToDateTime(input[no], cul);
                    aNo.NumberTypeId = (Int16)Enum_NumberType._4DMax;
                    aNo.NumberWinLevelId = (Int16)Enum_NumberWinLevel.GiaiNhat;
                    aNo.LotNumber = Convert.ToInt32(input[no + 5] + input[no + 6] + input[no + 7] + input[no + 8]);
                    aNo.DateCreated = DateTime.Now;
                    val.Add(aNo);

                    aNo = new Number();
                    aNo.DatePublish = Convert.ToDateTime(input[no],cul);
                    aNo.NumberTypeId = (Int16)Enum_NumberType._4DMax;
                    aNo.NumberWinLevelId = (Int16)Enum_NumberWinLevel.GiaiNhi;
                    aNo.LotNumber = Convert.ToInt32(input[no + 11] + input[no + 12] + input[no + 13] + input[no + 14]);
                    aNo.DateCreated = DateTime.Now;
                    val.Add(aNo);
                    aNo = new Number();
                    aNo.DatePublish = Convert.ToDateTime(input[no],cul);
                    aNo.NumberTypeId = (Int16)Enum_NumberType._4DMax;
                    aNo.NumberWinLevelId = (Int16)Enum_NumberWinLevel.GiaiNhi;
                    aNo.LotNumber = Convert.ToInt32(input[no + 15] + input[no + 16] + input[no + 17] + input[no + 18]);
                    aNo.DateCreated = DateTime.Now;
                    val.Add(aNo);

                    aNo = new Number();
                    aNo.DatePublish = Convert.ToDateTime(input[no], cul);
                    aNo.NumberTypeId = (Int16)Enum_NumberType._4DMax;
                    aNo.NumberWinLevelId = (Int16)Enum_NumberWinLevel.GiaiBa;
                    aNo.LotNumber = Convert.ToInt32(input[no + 21] + input[no + 22] + input[no + 23] + input[no + 24]);
                    aNo.DateCreated = DateTime.Now;
                    val.Add(aNo);
                    aNo = new Number();
                    aNo.DatePublish = Convert.ToDateTime(input[no], cul);
                    aNo.NumberTypeId = (Int16)Enum_NumberType._4DMax;
                    aNo.NumberWinLevelId = (Int16)Enum_NumberWinLevel.GiaiBa;
                    aNo.LotNumber = Convert.ToInt32(input[no + 25] + input[no + 26] + input[no + 27] + input[no + 28]);
                    aNo.DateCreated = DateTime.Now;
                    val.Add(aNo);
                    aNo = new Number();
                    aNo.DatePublish = Convert.ToDateTime(input[no], cul);
                    aNo.NumberTypeId = (Int16)Enum_NumberType._4DMax;
                    aNo.NumberWinLevelId = (Int16)Enum_NumberWinLevel.GiaiBa;
                    aNo.LotNumber = Convert.ToInt32(input[no + 29] + input[no + 30] + input[no + 31] + input[no + 32]);
                    aNo.DateCreated = DateTime.Now;
                    val.Add(aNo);

                    aNo = new Number();
                    aNo.DatePublish = Convert.ToDateTime(input[no], cul);
                    aNo.NumberTypeId = (Int16)Enum_NumberType._4DMax;
                    aNo.NumberWinLevelId = (Int16)Enum_NumberWinLevel.GiaiKhuyenKhich1;
                    aNo.LotNumber = Convert.ToInt32(input[no + 38] + input[no + 39] + input[no + 40]);
                    aNo.DateCreated = DateTime.Now;
                    val.Add(aNo);

                    aNo = new Number();
                    aNo.DatePublish = Convert.ToDateTime(input[no], cul);
                    aNo.NumberTypeId = (Int16)Enum_NumberType._4DMax;
                    aNo.NumberWinLevelId = (Int16)Enum_NumberWinLevel.GiaiKhuyenKhich2;
                    aNo.LotNumber = Convert.ToInt32(input[no + 47] + input[no + 48]);
                    aNo.DateCreated = DateTime.Now;
                    val.Add(aNo);
                }

                no += 48;
            }

            return val;
        }
        public static void Insert(string url)
        {
            string content = Common.Content(url);
            var array = content.Split("\r\n        ".ToCharArray());
            var list = Common.RemoveEmptyElement(array);           

            //List<Number> ListNumber = ConvertListStringToListNumber(list);
            //foreach (Number lotNumber in ListNumber)
            //{
            //    Db.Numbers.Add(lotNumber);
            //    Db.Entry(lotNumber).State = System.Data.Entity.EntityState.Added;
            //}

            //Db.SaveChanges();

        }
    }
}

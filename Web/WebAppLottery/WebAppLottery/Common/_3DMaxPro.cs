using HtmlAgilityPack;
using LotteryDAL;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace DataVietlott
{
    public class _3DMaxPro
    {
        private static LotteryEntities Db = LotteryDAL.LotteryConnection.Instance;       
        private static List<Number> ConvertListStringToListNumber(List<string> input)
        {
            List<Number> val = new List<Number>();
            CultureInfo cul = new CultureInfo("vi-VN");
            for (int no = 0; no < input.Count; no++)
            {
                if (!Common.CheckDateExisted(Convert.ToDateTime(input[no], cul), (Int16)Enum_NumberType._3DMaxPro))
                {
                    Console.WriteLine(input[no]);

                    Number aNo = new Number();
                    aNo.DatePublish = Convert.ToDateTime(input[no], cul);
                    aNo.NumberTypeId = (Int16)Enum_NumberType._3DMaxPro;
                    aNo.NumberWinLevelId = (Int16)Enum_NumberWinLevel.DacBiet;
                    aNo.LotNumber = input[no + 4] + input[no + 8];
                    aNo.DateCreated = DateTime.Now;
                    val.Add(aNo);

                    aNo = new Number();
                    aNo.DatePublish = Convert.ToDateTime(input[no], cul);
                    aNo.NumberTypeId = (Int16)Enum_NumberType._3DMaxPro;
                    aNo.NumberWinLevelId = (Int16)Enum_NumberWinLevel.GiaiNhat;
                    aNo.LotNumber = input[no + 14] + input[no + 18] + input[no + 22] + input[no + 26];
                    aNo.DateCreated = DateTime.Now;
                    val.Add(aNo);

                    aNo = new Number();
                    aNo.DatePublish = Convert.ToDateTime(input[no], cul);
                    aNo.NumberTypeId = (Int16)Enum_NumberType._3DMaxPro;
                    aNo.NumberWinLevelId = (Int16)Enum_NumberWinLevel.GiaiNhi;
                    aNo.LotNumber = input[no + 32] + input[no + 36] + input[no + 40] + input[no + 44] + input[no + 48] + input[no + 52];
                    aNo.DateCreated = DateTime.Now;
                    val.Add(aNo);

                    aNo = new Number();
                    aNo.DatePublish = Convert.ToDateTime(input[no], cul);
                    aNo.NumberTypeId = (Int16)Enum_NumberType._3DMaxPro;
                    aNo.NumberWinLevelId = (Int16)Enum_NumberWinLevel.GiaiBa;
                    aNo.LotNumber = input[no + 58] + input[no + 62] + input[no + 66] + input[no + 70] + input[no + 74] + input[no + 78] + input[no + 82] + input[no + 86];
                    aNo.DateCreated = DateTime.Now;
                    val.Add(aNo);
                }
                no += 97;
            }

            return val;
        }        
        public static string Insert(string url)
        {
            string value = null;

            string content = Common.Content(url);

            if (string.IsNullOrEmpty(content))
            {
                return "Error to get Data 3DMaxPro";
            }

            var array = content.Split("\r\n        ".ToCharArray());
            var list = Common.RemoveEmptyElement(array);

            //Remove 4 first items in list
            for (byte i = 0; i <= 4; i++)
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
                Db.SaveChanges();

                if (c == 4)
                {
                    value += "\t(" + lotNumber.DatePublish.ToShortDateString() + ")\n\t";
                    c = 0;
                }
            }

            
            return "+ " + DateTime.Now + ", Đã Lấy 3D MAX PRO thành công các số: \n\t" + (string.IsNullOrEmpty(value) ? "none" : value);
        }
    }
}

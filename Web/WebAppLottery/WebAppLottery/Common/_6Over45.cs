﻿using HtmlAgilityPack;
using LotteryDAL;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace DataVietlott
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
                    aNo.LotNumber = input[no + 2].Substring(0,2);      
                                
                    val.Add(aNo);
                   
                    aNo = new Number();
                    aNo.DatePublish = Convert.ToDateTime(input[no],cul);
                    aNo.NumberTypeId = (Int16)Enum_NumberType._6Over45;
                    aNo.NumberWinLevelId = (Int16)Enum_NumberWinLevel.DacBiet;
                    aNo.LotNumber = input[no + 2].Substring(2,2);
                    aNo.DateCreated = DateTime.Now;
                    val.Add(aNo);                   

                    aNo = new Number();
                    aNo.DatePublish = Convert.ToDateTime(input[no],cul);
                    aNo.NumberTypeId = (Int16)Enum_NumberType._6Over45;
                    aNo.NumberWinLevelId = (Int16)Enum_NumberWinLevel.DacBiet;
                    aNo.LotNumber = input[no + 2].Substring(4,2);
                    aNo.DateCreated = DateTime.Now;
                    val.Add(aNo);

                    aNo = new Number();
                    aNo.DatePublish = Convert.ToDateTime(input[no],cul);
                    aNo.NumberTypeId = (Int16)Enum_NumberType._6Over45;
                    aNo.NumberWinLevelId = (Int16)Enum_NumberWinLevel.DacBiet;
                    aNo.LotNumber = input[no + 2].Substring(6,2);
                    aNo.DateCreated = DateTime.Now;
                    val.Add(aNo);

                    aNo = new Number();
                    aNo.DatePublish = Convert.ToDateTime(input[no],cul);
                    aNo.NumberTypeId = (Int16)Enum_NumberType._6Over45;
                    aNo.NumberWinLevelId = (Int16)Enum_NumberWinLevel.DacBiet;
                    aNo.LotNumber = input[no + 2].Substring(8, 2);
                    aNo.DateCreated = DateTime.Now;
                    val.Add(aNo);

                    aNo = new Number();
                    aNo.DatePublish = Convert.ToDateTime(input[no],cul);
                    aNo.NumberTypeId = (Int16)Enum_NumberType._6Over45;
                    aNo.NumberWinLevelId = (Int16)Enum_NumberWinLevel.DacBiet;
                    aNo.LotNumber = input[no + 2].Substring(10, 2);
                    aNo.DateCreated = DateTime.Now;
                    val.Add(aNo);                    
                }

                no += 5;
            }

            return val;
        }        
        public static string Insert(string url)
        {
            string value = null;

            string content = Common.Content(url);
            if (string.IsNullOrEmpty(content))
            {
                return "Error to get Data 6/45";
            }
            var array = content.Split("\r\n        ".ToCharArray());
            var list = Common.RemoveEmptyElement(array);

            //Remove 3 items in list
            for (byte i = 0; i <= 3; i++)
            {
                list.RemoveAt(0);
            }

            int c = 0;
            List<Number> ListNumber = ConvertListStringToListNumber(list);
            foreach (Number lotNumber in ListNumber)
            {
                c++;
                value += lotNumber.LotNumber + " ";
                lotNumber.LotNumber = (Convert.ToInt32(lotNumber.LotNumber)).ToString();
                Db.Numbers.Add(lotNumber);
                Db.Entry(lotNumber).State = System.Data.Entity.EntityState.Added;
                Db.SaveChanges();

                if (c == 6)
                {
                    value += "\t(" + lotNumber.DatePublish.ToShortDateString() + ")\n\t";
                    c = 0;
                }
            }

            
            return "+ " + DateTime.Now + ", Đã Lấy 6/45 thành công các số: \n\t" + (string.IsNullOrEmpty(value) ? "none" : value);
        }
    }
}

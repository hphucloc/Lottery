﻿using LotteryDAL;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace LotteryBusiness
{
    public class Common
    {
        private static LotteryEntities Db = LotteryDAL.LotteryConnection.Instance;       
        public static IQueryable<LotteryNumber> GetNumber(Int16 numberWinLevelId, Int16 numberTypeId, DateTime datePublishFrom, DateTime datePublishTo)
        {           
            var t = Db.Numbers.Where(n => n.NumberWinLevelId == numberWinLevelId && n.NumberTypeId == numberTypeId &&
                n.DatePublish >= datePublishFrom && n.DatePublish <= datePublishTo).OrderBy(p=>p.LotNumber).Select(p => new LotteryNumber()
                {
                    DateCreated = p.DateCreated,
                    DatePublish = p.DatePublish,
                    DateUpdated = p.DateUpdated,
                    LotNumber = p.LotNumber,
                    NumberId = p.NumberId,
                    NumberTypeId = p.NumberTypeId,
                    NumberWinLevelId = p.NumberWinLevelId,                  
                });           

            return t;
        }
        public static IQueryable<LotteryNumber> GetNumber(byte number, Int16 numberWinLevelId, Int16 numberTypeId, DateTime datePublishFrom, DateTime datePublishTo)
        {
            var t = Db.Numbers.Where(n => n.NumberWinLevelId == numberWinLevelId && n.NumberTypeId == numberTypeId && n.LotNumber == number &&
                n.DatePublish >= datePublishFrom && n.DatePublish <= datePublishTo).OrderBy(p => p.LotNumber).Select(p => new LotteryNumber()
                {
                    DateCreated = p.DateCreated,
                    DatePublish = p.DatePublish,
                    DateUpdated = p.DateUpdated,
                    LotNumber = p.LotNumber,
                    NumberId = p.NumberId,
                    NumberTypeId = p.NumberTypeId,
                    NumberWinLevelId = p.NumberWinLevelId,
                });

            return t;
        }
        public static IQueryable<LotteryNumber> GetBoughtNumberPublishedInDate(Int16 numberWinLevelId, Int16 numberTypeId, DateTime datePublish)
        {
            var t = Db.NumberBoughts.Where(n => n.NumberWinLevelId == numberWinLevelId && n.NumberTypeId == numberTypeId &&
                n.DateBought == datePublish.Date).OrderBy(p => p.LotNumber).Select(p => new LotteryNumber()
                {                   
                    DatePublish = p.DateBought,                  
                    LotNumber = p.LotNumber,
                    NumberId = p.Id,
                    NumberTypeId = p.NumberTypeId,
                    NumberWinLevelId = p.NumberWinLevelId,
                });

            return t;
        }
        public static IQueryable<LotteryNumber> GetBoughtNumber(Int16 numberWinLevelId, Int16 numberTypeId, DateTime datePublishFrom, DateTime datePublishTo)
        {
            var t = Db.NumberBoughts.Where(n => n.NumberWinLevelId == numberWinLevelId && n.NumberTypeId == numberTypeId &&
                n.DateBought >= datePublishFrom && n.DateBought <= datePublishTo).OrderBy(p => p.LotNumber).Select(p => new LotteryNumber()
                {
                    DatePublish = p.DateBought,
                    LotNumber = p.LotNumber,
                    NumberId = p.Id,
                    NumberTypeId = p.NumberTypeId,
                    NumberWinLevelId = p.NumberWinLevelId,
                });

            return t;
        }
        public static List<FullLotteryStatistic> GetFullLotNumberStatistic(IQueryable<LotteryNumber> Number)
        {
            List<FullLotteryStatistic> lst = new List<FullLotteryStatistic>();

            int prvLotNumber = -1;
            int curLotNumber = -1;

            //Bo so trung
            foreach (LotteryNumber no in Number)
            {
                curLotNumber = no.LotNumber;
                if (curLotNumber != prvLotNumber)
                {
                    //if (lst.Count > 0)
                    //    lst[lst.Count - 1].DatePublishList.DatePublishList1.Sort();

                    //LoterryStatistic sta = new LoterryStatistic();
                    //sta.NumberTypeId = no.NumberTypeId;
                    //sta.NumberWinLevelId = no.NumberWinLevelId;
                    //sta.LotNumber = no.LotNumber;
                    //sta.DateCreated = no.DateCreated;
                    //sta.DatePublish = no.DatePublish;
                    //sta.DatePublishList.DatePublishList1.Add(no.DatePublish);
                    //sta.TotalNumberAppear = no.TotalNumberAppear;
                    //lst.Add(sta);
                }
                else
                {
                    //lst[lst.Count - 1].DatePublishList.DatePublishList1.Add(no.DatePublish);
                }
                prvLotNumber = curLotNumber;

            }

            return lst;
        }
        public static List<LoterryStatistic> GetLotNumberStatistic(IQueryable<LotteryNumber> Number)
        {
            List<LoterryStatistic> lst = new List<LoterryStatistic>();

            int prvLotNumber = -1;
            int curLotNumber = -1;

            //Bo so trung
            foreach (LotteryNumber no in Number)
            {
                curLotNumber = no.LotNumber;
                if (curLotNumber != prvLotNumber)
                {
                    if (lst.Count > 0)
                        lst[lst.Count - 1].DatePublishList.DatePublishList1.Sort();

                    LoterryStatistic sta = new LoterryStatistic();
                    sta.NumberTypeId = no.NumberTypeId;
                    sta.NumberWinLevelId = no.NumberWinLevelId;
                    sta.LotNumber = no.LotNumber;
                    sta.DateCreated = no.DateCreated;
                    sta.DatePublish = no.DatePublish;
                    sta.DatePublishList.DatePublishList1.Add(no.DatePublish);
                    sta.TotalNumberAppear = no.TotalNumberAppear;
                    lst.Add(sta);
                }
                else
                {
                    lst[lst.Count - 1].DatePublishList.DatePublishList1.Add(no.DatePublish);
                }
                prvLotNumber = curLotNumber;

            }

            //Lay ngay lon nhat va nho nhat
            foreach(LoterryStatistic no in lst)
            {
                no.DatePublishMax = Number.Max(a => a.DatePublish);
                no.DatePublishMin = Number.Min(a => a.DatePublish);
                no.TotalNumberAppear = GetTotalNumberAppear(no.LotNumber, no.NumberTypeId);
            }

            //Lay tat ca ngay publish
            foreach (LoterryStatistic no in lst)
            {
                var a = Number.GroupBy(n => n.DatePublish).ToList();
                foreach (var state in a)
                {
                    no.AllDatePublishList.Add(state.First().DatePublish);
                }
            }          

            return lst;
        }
        public static List<NumberTipBet> GetNumberTipBet()
        {
            return Db.NumberTipBets.ToList();
        }
        public static void AddTipBet(string tipBet)
        {
            NumberTipBet t = new NumberTipBet();
            t.Label = tipBet;

            Db.NumberTipBets.Add(t);
            Db.SaveChanges();
        }
        public static void AddWinnedAmount(string input)
        {
            Match t = new Match();
            t.Date = DateTime.Now;
            t.FullNumber = input.Split('~')[2];
            t.MatchNumber = input.Split('~')[1];
            t.Amount = Convert.ToInt64(input.Split('~')[0]);
            t.Comment = input.Split('~')[3];

            Db.Matches.Add(t);
            Db.SaveChanges();
        }
        public static void AddBettedAmount(string input)
        {
            AmountBuyLottery t = new AmountBuyLottery();
            t.Date = DateTime.Now;
            t.Amount = Convert.ToInt64(input.Split('~')[0]);
            t.Comment = input.Split('~')[1];

            Db.AmountBuyLotteries.Add(t);
            Db.SaveChanges();
        }
        public static int Wined()
        {
            return (int)Db.Matches.Sum(m => m.Amount);           
        }
        public static int Bet()
        {
            return (int)Db.AmountBuyLotteries.Sum(m => m.Amount);
        }
        public static int GetTotalNumberAppear(int No, short numberType)
        {
            return Db.Numbers.Count(x => x.LotNumber == No && x.NumberTypeId == numberType);
        }
        public static void NewNumber(DateTime publishDdate, List<int> number, short numberType, short numberWinLevel)
        {
            Number n = null;
            foreach (var no in number)
            {
                n = new Number();
                n.DateCreated = DateTime.Now;
                n.DatePublish = publishDdate;
                n.LotNumber = no;
                n.NumberTypeId = numberType;
                n.NumberWinLevelId = numberWinLevel;
                Db.Entry(n).State = System.Data.Entity.EntityState.Added;
                Db.Numbers.Add(n);
            }
            try
            {              
                Db.SaveChanges();
            }
            catch (Exception)
            {
                foreach (byte i in number) {
                    Db.Database.ExecuteSqlCommand("INSERT INTO dbo.Number(NumberTypeId, NumberWinLevelId, LotNumber, DatePublish, DateCreated) VALUES({0}, {1}, {2}, {3}, {4})",
                        numberType, numberWinLevel, i, publishDdate, publishDdate);
                }
            }
        }
        public static List<int> GetNumberNextAppear(SortedSet<DateTime> dateAppear, short numberType, short numberWinLevel)
        {
            List<int> numberNextAppear = new List<int>();

            foreach(DateTime i in dateAppear)
            {
                DateTime dayOfWeek = i;
                if (numberType == (short)Enum_NumberType._6Over45)
                {
                    if (i.DayOfWeek == DayOfWeek.Wednesday || i.DayOfWeek == DayOfWeek.Friday)
                        dayOfWeek = dayOfWeek.AddDays(2).Date;
                    else if (i.DayOfWeek == DayOfWeek.Sunday)
                        dayOfWeek = dayOfWeek.AddDays(3).Date;
                }
                else if (numberType == (short)Enum_NumberType._6Over55)
                {
                    if (i.DayOfWeek == DayOfWeek.Thursday || i.DayOfWeek == DayOfWeek.Tuesday)
                        dayOfWeek = dayOfWeek.AddDays(2).Date;
                    else if (i.DayOfWeek == DayOfWeek.Saturday)
                        dayOfWeek = dayOfWeek.AddDays(3).Date;
                }

                var number = Db.Numbers.Where(x => DbFunctions.TruncateTime(x.DatePublish) == dayOfWeek
                    && x.NumberTypeId == numberType && x.NumberWinLevelId== numberWinLevel).Select(x => x.LotNumber).ToList();
                foreach(byte j in number)
                {
                    numberNextAppear.Add(j);
                }
            }

            return numberNextAppear;
        }
        public static void CreateBoughtNumber(List<int> number, DateTime dateBought, short numberType, short numberWinLevel)
        {
            foreach (byte i in number)
            {
                Db.Database.ExecuteSqlCommand("INSERT INTO dbo.NumberBought(DateBought, LotNumber, NumberTypeId, NumberWinLevelId, CreatedDate) VALUES({0}, {1}, {2}, {3}, {4})",
                    dateBought, i, numberType, numberWinLevel, DateTime.Now);
            }
        }       
    }
}
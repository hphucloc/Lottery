using LotteryDAL;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Linq;

namespace LotteryBusiness
{
    public class NumbersNextAppear
    {
        public Int64 NumberId { get; set; }
        public Int16 NumberTypeID { get; set; }
        public Int16 NumberWinLevelID { get; set; }
        public string LotNumber { get; set; }
        public DateTime DatePublish { get; set; }
        public DateTime? NextPublishDate { get; set; }
    }

    public class Common
    {
        private static LotteryEntities Db = new LotteryEntities();

        public static IQueryable<LotteryNumber> GetNumber(Int16 numberWinLevelId, Int16 numberTypeId, DateTime datePublishFrom, DateTime datePublishTo)
        {
            return Db.Numbers.Where(n => n.NumberWinLevelId == numberWinLevelId && n.NumberTypeId == numberTypeId &&
                n.DatePublish >= datePublishFrom && n.DatePublish <= datePublishTo).OrderBy(p => p.LotNumber).Select(p => new LotteryNumber()
                {
                    DateCreated = p.DateCreated,
                    DatePublish = p.DatePublish,
                    DateUpdated = p.DateUpdated,
                    LotNumber = p.LotNumber,
                    NumberId = p.NumberId,
                    NumberTypeId = p.NumberTypeId,
                    NumberWinLevelId = p.NumberWinLevelId,
                    KyQuay = p.KyQuay
                });
        }

        public static IQueryable<LotteryNumber> GetNumber(string number, Int16 numberWinLevelId, Int16 numberTypeId, DateTime datePublishFrom, DateTime datePublishTo)
        {
            return Db.Numbers.Where(n => n.NumberWinLevelId == numberWinLevelId && n.NumberTypeId == numberTypeId && n.LotNumber == number &&
                n.DatePublish >= datePublishFrom && n.DatePublish <= datePublishTo).OrderBy(p => p.LotNumber).Select(p => new LotteryNumber()
                {
                    DateCreated = p.DateCreated,
                    DatePublish = p.DatePublish,
                    DateUpdated = p.DateUpdated,
                    LotNumber = p.LotNumber,
                    NumberId = p.NumberId,
                    NumberTypeId = p.NumberTypeId,
                    NumberWinLevelId = p.NumberWinLevelId,
                    KyQuay = p.KyQuay
                });
            
        }

        public static IQueryable<LotteryNumber> GetNumber(Int16 numberTypeId, DateTime datePublishFrom, DateTime datePublishTo)
        {
            return Db.Numbers.Where(n => n.NumberTypeId == numberTypeId &&
                n.DatePublish >= datePublishFrom && n.DatePublish <= datePublishTo).OrderByDescending(x => x.KyQuay).Select(p => new LotteryNumber()
                {
                    DateCreated = p.DateCreated,
                    DatePublish = p.DatePublish,
                    DateUpdated = p.DateUpdated,
                    LotNumber = p.LotNumber,
                    NumberId = p.NumberId,
                    NumberTypeId = p.NumberTypeId,
                    NumberWinLevelId = p.NumberWinLevelId,
                    KyQuay = p.KyQuay
                });
        }       

        public static List<LoterryStatistic> GetLotNumberStatistic(IQueryable<LotteryNumber> Number)
        {
            List<LoterryStatistic> lst = new List<LoterryStatistic>();

            int prvLotNumber = 0;
            int curLotNumber = 0;

            List<LotteryNumber> lstData = new List<LotteryNumber>();
            foreach (LotteryNumber i in Number)
            {
                LotteryNumber a = new LotteryNumber();
                a.DateCreated = i.DateCreated;
                a.DatePublish = i.DatePublish;
                a.DateUpdated = i.DateUpdated;
                a.LotNumber = Convert.ToInt32(i.LotNumber).ToString();
                a.NumberId = i.NumberId;
                a.NumberTypeId = i.NumberTypeId;
                a.NumberWinLevelId = i.NumberWinLevelId;
                a.KyQuay = i.KyQuay;
                lstData.Add(a);
            }           

            //Bo so trung
            foreach (LotteryNumber no in lstData.OrderBy(x=>x.LotNumber))
            {
                curLotNumber = Convert.ToInt32(no.LotNumber);
                if (curLotNumber != Convert.ToInt32(prvLotNumber))
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
                    sta.DatePublishList.LstKyQuay.Add(no.KyQuay);
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
                no.TotalNumberAppearInRange = no.DatePublishList.DatePublishList1.Count;
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

        public static List<LoterryStatistic> GetLotNumberStatisticKeno(IQueryable<LotteryNumber> Number)
        {
            List<LoterryStatistic> lst = new List<LoterryStatistic>();
                      
            foreach (LotteryNumber no in Number)
            {
                if(lst.FirstOrDefault(x=>x.LotNumber == no.LotNumber) == null)
                {                      
                    LoterryStatistic sta = new LoterryStatistic();
                    sta.NumberTypeId = no.NumberTypeId;
                    sta.NumberWinLevelId = no.NumberWinLevelId;
                    sta.LotNumber = no.LotNumber;
                    sta.DateCreated = no.DateCreated;
                    sta.DatePublish = no.DatePublish;
                    sta.DatePublishList.DatePublishList1.Add(no.DatePublish);
                    sta.DatePublishList.LstKyQuay.Add(no.KyQuay);
                    sta.TotalNumberAppear = no.TotalNumberAppear;                    
                    lst.Add(sta);
                }
                else
                {
                    lst.FirstOrDefault(x => x.LotNumber == no.LotNumber).DatePublishList.DatePublishList1.Add(no.DatePublish);
                    lst.FirstOrDefault(x => x.LotNumber == no.LotNumber).DatePublishList.LstKyQuay.Add(no.KyQuay);
                }               
            }
            

            //Lay ngay lon nhat va nho nhat
            foreach (LoterryStatistic no in lst)
            {
                no.DatePublishMax = Number.Max(a => a.DatePublish);
                no.DatePublishMin = Number.Min(a => a.DatePublish);
                no.TotalNumberAppear = GetTotalNumberAppear(no.LotNumber, no.NumberTypeId);
                no.TotalNumberAppearInRange = no.DatePublishList.DatePublishList1.Count;
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

            return lst.OrderBy(x=> Convert.ToInt32(x.LotNumber)).ToList();
        }

        public static int GetTotalNumberAppear(string No, short numberType)
        {
            return Db.Numbers.Count(x => x.LotNumber == No && x.NumberTypeId == numberType);
        }

        public static List<NumbersNextAppear> GetNumbersNextAppear(int leadOffset, int @numberTypeId, int numberWinLevelId)
        {
            Dictionary<DateTime, List<int>> numberNextAppear = new Dictionary<DateTime, List<int>>();            

            var pLeadOffset = new SqlParameter("@leadOffset", leadOffset);         
            var pNumberTypeId = new SqlParameter("@numberTypeId", numberTypeId);
            var pNumberWinLevelId = new SqlParameter("@NumberWinLevelId", numberWinLevelId);

            var result = Db.Database.SqlQuery<NumbersNextAppear>("GetNextAppear @leadOffset, @numberTypeId, @numberWinLevelId ",
                    pLeadOffset, pNumberTypeId, pNumberWinLevelId)
                .ToList();

            return result;
        }        
        public static void NewNumber(DateTime publishDdate, List<string> number, short numberType, short numberWinLevel)
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

                foreach (var i in number)
                {
                    Db.Database.ExecuteSqlCommand("INSERT INTO dbo.Number(NumberTypeId, NumberWinLevelId, LotNumber, DatePublish, DateCreated) VALUES({0}, {1}, {2}, {3}, {4})",
                        numberType, numberWinLevel, i, publishDdate, publishDdate);
                }
                //throw new Exception();
            }
        }        
        public static void CreateBoughtNumber(List<int> number, DateTime dateBought, short numberType, short numberWinLevel)
        {
            foreach (byte i in number)
            {
                Db.Database.ExecuteSqlCommand("INSERT INTO dbo.NumberBought(DateBought, LotNumber, NumberTypeId, NumberWinLevelId, CreatedDate) VALUES({0}, {1}, {2}, {3}, {4})",
                    dateBought, i, numberType, numberWinLevel, DateTime.Now);
            }
        }

        public static List<FullLotteryStatistic> ConvertLotNumberToListFullLotteryStatistic(List<LoterryStatistic> data)
        {
            var allPublishDate = data[0].AllDatePublishList.OrderBy(x => x.Date);
            List<FullLotteryStatistic> lstItem = new List<FullLotteryStatistic>();
            foreach (var i in allPublishDate)
            {
                FullLotteryStatistic item = new FullLotteryStatistic()
                {
                    PublishDate = i,
                    Numbers = new List<byte>()
                };
                foreach (var j in data)
                {
                    foreach (var k in j.DatePublishList.DatePublishList1)
                    {
                        if (i.Date == k.Date)
                        {
                            item.Numbers.Add(Convert.ToByte(j.LotNumber));
                        }
                    }
                }
                lstItem.Add(item);

            }
            return lstItem.OrderByDescending(x => x.PublishDate).ToList();
        }
    }
}

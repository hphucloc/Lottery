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
        public static List<LotteryNumber> GetNumber(Int16 numberWinLevelId, Int16 numberTypeId, DateTime datePublishFrom, DateTime datePublishTo)
        {
            using (var db = new LotteryEntities())
            {
                return db.Numbers.Where(n => n.NumberWinLevelId == numberWinLevelId && n.NumberTypeId == numberTypeId &&
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
                    }).ToList();
            }
        }

        public static List<LotteryNumber> GetNumber(string number, Int16 numberWinLevelId, Int16 numberTypeId, DateTime datePublishFrom, DateTime datePublishTo)
        {
            using (var db = new LotteryEntities())
            {
                return db.Numbers.Where(n => n.NumberWinLevelId == numberWinLevelId && n.NumberTypeId == numberTypeId && n.LotNumber == number &&
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
                    }).ToList();
            }
        }

        public static List<LotteryNumber> GetNumber(Int16 numberTypeId, DateTime datePublishFrom, DateTime datePublishTo)
        {
            using (var db = new LotteryEntities())
            {
                return db.Numbers.Where(n => n.NumberTypeId == numberTypeId &&
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
                    }).ToList();
            }
        }

        public static List<LoterryStatistic> GetLotNumberStatistic(IEnumerable<LotteryNumber> Number)
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

            var numberList = Number.ToList();

            //Lay ngay lon nhat va nho nhat
            foreach(LoterryStatistic no in lst)
            {
                no.DatePublishMax = numberList.Max(a => a.DatePublish);
                no.DatePublishMin = numberList.Min(a => a.DatePublish);
                no.TotalNumberAppear = GetTotalNumberAppear(no.LotNumber, no.NumberTypeId);
                no.TotalNumberAppearInRange = no.DatePublishList.DatePublishList1.Count;
            }

            //Lay tat ca ngay publish
            foreach (LoterryStatistic no in lst)
            {
                var a = numberList.GroupBy(n => n.DatePublish).ToList();
                foreach (var state in a)
                {
                    no.AllDatePublishList.Add(state.First().DatePublish);
                }
            }

            return lst;
        }

        public static List<LoterryStatistic> GetLotNumberStatisticKeno(IEnumerable<LotteryNumber> Number)
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

            var numberList = Number.ToList();

            //Lay ngay lon nhat va nho nhat
            foreach (LoterryStatistic no in lst)
            {
                no.DatePublishMax = numberList.Max(a => a.DatePublish);
                no.DatePublishMin = numberList.Min(a => a.DatePublish);
                no.TotalNumberAppear = GetTotalNumberAppear(no.LotNumber, no.NumberTypeId);
                no.TotalNumberAppearInRange = no.DatePublishList.DatePublishList1.Count;
            }

            //Lay tat ca ngay publish
            foreach (LoterryStatistic no in lst)
            {
                var a = numberList.GroupBy(n => n.DatePublish).ToList();
                foreach (var state in a)
                {
                    no.AllDatePublishList.Add(state.First().DatePublish);
                }
            }

            return lst.OrderBy(x=> Convert.ToInt32(x.LotNumber)).ToList();
        }

        public static int GetTotalNumberAppear(string No, short numberType)
        {
            using (var db = new LotteryEntities())
            {
                return db.Numbers.Count(x => x.LotNumber == No && x.NumberTypeId == numberType);
            }
        }

        public static string ExecuteSql(string sql)
        {
            using (var db = new LotteryEntities())
            {
                var conn = db.Database.Connection;
                if (conn.State != System.Data.ConnectionState.Open)
                    conn.Open();

                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = sql;
                    cmd.CommandTimeout = 60;

                    var upper = sql.TrimStart().ToUpper();
                    if (upper.StartsWith("SELECT") || upper.StartsWith("WITH"))
                    {
                        using (var reader = cmd.ExecuteReader())
                        {
                            var sb = new System.Text.StringBuilder();
                            var cols = Enumerable.Range(0, reader.FieldCount)
                                                .Select(i => reader.GetName(i));
                            sb.AppendLine(string.Join("\t", cols));
                            sb.AppendLine(new string('-', 60));
                            int rows = 0;
                            while (reader.Read())
                            {
                                var vals = Enumerable.Range(0, reader.FieldCount)
                                                     .Select(i => reader.IsDBNull(i) ? "NULL" : reader[i].ToString());
                                sb.AppendLine(string.Join("\t", vals));
                                rows++;
                            }
                            sb.AppendLine(string.Format("-- {0} row(s) returned", rows));
                            return sb.ToString();
                        }
                    }
                    else
                    {
                        int affected = cmd.ExecuteNonQuery();
                        return string.Format("-- {0} row(s) affected", affected);
                    }
                }
            }
        }

        public static List<NumbersNextAppear> GetNumbersNextAppear(int leadOffset, int @numberTypeId, int numberWinLevelId)
        {
            using (var db = new LotteryEntities())
            {
                var pLeadOffset = new SqlParameter("@leadOffset", leadOffset);
                var pNumberTypeId = new SqlParameter("@numberTypeId", numberTypeId);
                var pNumberWinLevelId = new SqlParameter("@NumberWinLevelId", numberWinLevelId);

                return db.Database.SqlQuery<NumbersNextAppear>("GetNextAppear @leadOffset, @numberTypeId, @numberWinLevelId ",
                        pLeadOffset, pNumberTypeId, pNumberWinLevelId)
                    .ToList();
            }
        }

        public static void NewNumber(DateTime publishDdate, List<string> number, short numberType, short numberWinLevel)
        {
            using (var db = new LotteryEntities())
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
                    db.Entry(n).State = EntityState.Added;
                    db.Numbers.Add(n);
                }
                try
                {
                    db.SaveChanges();
                }
                catch (Exception)
                {
                    foreach (var i in number)
                    {
                        db.Database.ExecuteSqlCommand("INSERT INTO dbo.Number(NumberTypeId, NumberWinLevelId, LotNumber, DatePublish, DateCreated) VALUES({0}, {1}, {2}, {3}, {4})",
                            numberType, numberWinLevel, i, publishDdate, publishDdate);
                    }
                }
            }
        }

        public static void CreateBoughtNumber(List<int> number, DateTime dateBought, short numberType, short numberWinLevel)
        {
            using (var db = new LotteryEntities())
            {
                foreach (byte i in number)
                {
                    db.Database.ExecuteSqlCommand("INSERT INTO dbo.NumberBought(DateBought, LotNumber, NumberTypeId, NumberWinLevelId, CreatedDate) VALUES({0}, {1}, {2}, {3}, {4})",
                        dateBought, i, numberType, numberWinLevel, DateTime.Now);
                }
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

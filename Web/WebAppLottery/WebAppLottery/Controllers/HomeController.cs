using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using LotteryBusiness;
using WebAppLottery.Models;

namespace WebAppLottery.Controllers
{
    public class HomeController : Controller
    {
        private List<LoterryStatistic> _6Over45NoData { get; set; }
        private List<LoterryStatistic> _6Over55NoData { get; set; }
        [HttpGet]
        public ActionResult Index(IndexPageModel m)
        {            
            m.ListLoaiVe = IndexPageModel.LoaiVe._6Over45;
            m.From = DateTime.Now.AddMonths(-3);
            m.HiddenFrom = string.Format("{0:yyyy-MM-dd}", m.From);
            m.To = DateTime.Now;
            m.HiddenTo = string.Format("{0:yyyy-MM-dd}", m.To);
            m.ErrorMessage = "";

            return View(m);
        }

        [HttpPost]
        public new ActionResult Request(IndexPageModel m)
        {
            if (ModelState.IsValid)
            {
                //************************Render Header*************************//
                int count1_7 = 0;
                int count8_15 = 0;
                int count16_23 = 0;
                int count24_31 = 0;
                int count32_39 = 0;
                int count40_47 = 0;
                int countUpper47 = 0;
                
                int loaiVe = (int)m.ListLoaiVe;
                DateTime from, to;                
                string sysFormat = CultureInfo.CurrentCulture.DateTimeFormat.ShortDatePattern;
                if (sysFormat == "d/M/yyyy")
                {
                    from = DateTime.ParseExact(m.From.ToShortDateString(), "d/M/yyyy", CultureInfo.InvariantCulture);
                    to = DateTime.ParseExact(m.To.ToShortDateString(), "d/M/yyyy", CultureInfo.InvariantCulture);
                    m.ErrorMessage = "";
                }
                else if (sysFormat == "M/d/yyyy")
                {

                    from = DateTime.ParseExact(m.From.ToShortDateString(), "M/d/yyyy", CultureInfo.InvariantCulture);
                    to = DateTime.ParseExact(m.To.ToShortDateString(), "M/d/yyyy", CultureInfo.InvariantCulture);
                    m.ErrorMessage = "";
                }
                else
                {
                    from = DateTime.Now.AddMonths(-3);
                    to = DateTime.Now;
                    m.ErrorMessage = "Unsuppoted System DateTime Format";
                }

                m.From = from;
                m.To = to;
                if (m.ListLoaiVe == IndexPageModel.LoaiVe._6Over45)
                {
                    _6Over45NoData = _6Over45TimeLine.Get6Over45Number(from, to);
                    m.Data = _6Over45NoData.Select(x => new LotteryStatistic1
                    {
                        LotNumber = Convert.ToInt32(x.LotNumber),
                        AllDatePublishList =  x.AllDatePublishList,
                        DatePublish = x.DatePublish,
                        TotalNumberAppearInRange = x.TotalNumberAppearInRange,
                        DatePublishList = x.DatePublishList
                    }).OrderBy(x => x.LotNumber).ToList();
                }
                else if (m.ListLoaiVe == IndexPageModel.LoaiVe._6Over55)
                {
                    _6Over55NoData = _6Over55TimeLine.Get6Over55Number(from, to);
                    m.Data = _6Over55NoData.Select(x => new LotteryStatistic1
                    {
                        LotNumber = Convert.ToInt32(x.LotNumber),
                        AllDatePublishList = x.AllDatePublishList,
                        DatePublish = x.DatePublish,
                        TotalNumberAppearInRange = x.TotalNumberAppearInRange,
                        DatePublishList = x.DatePublishList
                    }).OrderBy(x=>x.LotNumber).ToList();
                }

                //************************Render Body*************************//    
                m.AllDatePublist = m.Data[0].AllDatePublishList;

                //************************Render GroupNumberStatistic*************************//
                SortedDictionary<DateTime, SortedSet<int>> hitNumberByDate = 
                    new SortedDictionary<DateTime, SortedSet<int>>();
                foreach (var i in m.Data)
                {
                    if (Convert.ToInt32(i.LotNumber) >= 1 && Convert.ToInt32(i.LotNumber) <= 7)
                    {
                        count1_7 += i.TotalNumberAppearInRange;
                    }
                    else
                        if (Convert.ToInt32(i.LotNumber) >= 8 && Convert.ToInt32(i.LotNumber) <= 15)
                    {
                        count8_15 += i.TotalNumberAppearInRange;
                    }
                    else
                        if (Convert.ToInt32(i.LotNumber) >= 6 && Convert.ToInt32(i.LotNumber) <= 23)
                    {
                        count16_23 += i.TotalNumberAppearInRange;
                    }
                    else
                        if (Convert.ToInt32(i.LotNumber) >= 24 && Convert.ToInt32(i.LotNumber) <= 31)
                    {
                        count24_31 += i.TotalNumberAppearInRange;
                    }
                    else
                        if (Convert.ToInt32(i.LotNumber) >= 32 && Convert.ToInt32(i.LotNumber) <= 39)
                    {
                        count32_39 += i.TotalNumberAppearInRange;
                    }
                    else
                        if (Convert.ToInt32(i.LotNumber) >= 40 && Convert.ToInt32(i.LotNumber) <= 47)
                    {
                        count40_47 += i.TotalNumberAppearInRange;
                    }
                    else
                    {
                        countUpper47 += i.TotalNumberAppearInRange;
                    }

                    //Get all publih Date               
                    foreach (var date in i.AllDatePublishList)
                    {
                        if (!hitNumberByDate.ContainsKey(date))
                        {
                            hitNumberByDate.Add(date, new SortedSet<int>());
                        }
                    }
                }

                foreach (var item in hitNumberByDate)
                    foreach (var aNo in m.Data)
                        foreach (var date in aNo.DatePublishList.DatePublishList1)
                            if (item.Key == date)
                                item.Value.Add(Convert.ToInt32(aNo.LotNumber));

                m.groupNumberStatistic = hitNumberByDate;
                m.NoAppear1To7 = count1_7;
                m.NoAppear8To15 = count8_15;
                m.NoAppear16To23 = count16_23;
                m.NoAppear24To31 = count24_31;
                m.NoAppear32To39 = count32_39;
                m.NoAppear40To47 = count40_47;
                m.NoAppear48To55 = countUpper47;             
            }
            return View("Index", m);            
        }

        [HttpGet]
        public ActionResult Data(DataPageModel m)
        {
            string val;
            try
            {
                val = DataVietlott._6Over45.Insert(DataVietlott.Common.ReadAppConfig("6Over45URL"));
                m.Status = val + "\n";

                val = DataVietlott._6Over55.Insert(DataVietlott.Common.ReadAppConfig("6Over55URL"));
                m.Status += val + "\n";

                val = DataVietlott._3DMax.Insert(DataVietlott.Common.ReadAppConfig("3dMaxURL"));
                m.Status += val + "\n";

                val = DataVietlott._3DMaxPro.Insert(DataVietlott.Common.ReadAppConfig("3dMaxProURL"));
                m.Status += val + "\n";

                m.ErrorMessage = "";
                m.IsGet6Over45 = true;
                m.IsGet6Over55 = true;
                m.IsGet3DMax = true;
                m.IsGet3DMaxPro = true;
                m.IsGetKeno = true;
                ModelState.Clear();
            }
            catch (Exception e)
            {
                m.ErrorMessage = e.Message;
            }

            return View(m);
        }

        [HttpPost]
        public ActionResult DataWithFilter(DataPageModel m)
        {
            string val;          
            try
            {
                if (m.IsGet6Over45) {
                    val = DataVietlott._6Over45.Insert(DataVietlott.Common.ReadAppConfig("6Over45URL"));
                    m.Status = val + "\n";
                }
                if (m.IsGet6Over55)
                {
                    val = DataVietlott._6Over55.Insert(DataVietlott.Common.ReadAppConfig("6Over55URL"));
                    m.Status += val + "\n";
                }
                if (m.IsGet3DMax)
                {
                    val = DataVietlott._3DMax.Insert(DataVietlott.Common.ReadAppConfig("3dMaxURL"));
                    m.Status += val + "\n";
                }
                if (m.IsGet3DMaxPro)
                {
                    val = DataVietlott._3DMaxPro.Insert(DataVietlott.Common.ReadAppConfig("3dMaxProURL"));
                    m.Status += val + "\n";
                }
                if (string.IsNullOrEmpty(m.ErrorMessage))
                    m.ErrorMessage = "";

                ModelState.Clear();
            }
            catch (Exception e)
            {
                m.ErrorMessage = e.Message;
            }
         
            return View("Data", m);
        }

        public ActionResult Admin()
        {            
            return View();
        }
    }
}
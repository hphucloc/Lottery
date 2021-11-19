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
            m.From = DateTime.Now.AddMonths(-1);
            m.HiddenFrom = string.Format("{0:yyyy-M-d}", m.From);
            m.To = DateTime.Now;
            m.HiddenTo = string.Format("{0:yyyy-M-d}", m.To);
            
            return View(m);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
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
                DateTime from = DateTime.ParseExact(m.From.ToShortDateString(), "M/d/yyyy", CultureInfo.InvariantCulture);
                DateTime to = DateTime.ParseExact(m.To.ToShortDateString(), "M/d/yyyy", CultureInfo.InvariantCulture);                
                if (m.ListLoaiVe == IndexPageModel.LoaiVe._6Over45)
                    _6Over45NoData = m.Data = _6Over45TimeLine.Get6Over45Number(from, to);
                else if (m.ListLoaiVe == IndexPageModel.LoaiVe._6Over55)
                    _6Over55NoData = m.Data = _6Over55TimeLine.Get6Over55Number(from, to);

                foreach (var i in m.Data)
                {                   
                    if (i.LotNumber >= 1 && i.LotNumber <= 7)
                    {
                        count1_7 += i.TotalNumberAppearInRange;
                    }
                    else
                        if (i.LotNumber >= 8 && i.LotNumber <= 15)
                    {
                        count8_15 += i.TotalNumberAppearInRange;
                    }
                    else
                        if (i.LotNumber >= 6 && i.LotNumber <= 23)
                    {
                        count16_23 += i.TotalNumberAppearInRange;
                    }
                    else
                        if (i.LotNumber >= 24 && i.LotNumber <= 31)
                    {
                        count24_31 += i.TotalNumberAppearInRange;
                    }
                    else
                        if (i.LotNumber >= 32 && i.LotNumber <= 39)
                    {
                        count32_39 += i.TotalNumberAppearInRange;
                    }
                    else
                        if (i.LotNumber >= 40 && i.LotNumber <= 47)
                    {
                        count40_47 += i.TotalNumberAppearInRange;
                    }
                    else
                    {
                        countUpper47 += i.TotalNumberAppearInRange;
                    }
                }
                
                m.NoAppear1To7   = count1_7;
                m.NoAppear8To15  = count8_15;
                m.NoAppear16To23 = count16_23;
                m.NoAppear24To31 = count24_31;
                m.NoAppear32To39 = count32_39;
                m.NoAppear40To47 = count40_47;
                m.NoAppear48To55 = countUpper47;

                //************************Render Body*************************//    
                m.AllDatePublist = m.Data[0].AllDatePublishList;
            }
            return View("Index", m);            
        }        
    }
}
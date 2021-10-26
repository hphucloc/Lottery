using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using LotteryBusiness;

namespace WebAppLottery.Controllers
{
    public class HomeController : Controller
    {
        private List<LoterryStatistic> _6Over45No { get; set; }
        public ActionResult Index()
        {
            _6Over45No = LotteryBusiness._6Over45TimeLine.Get6Over45Number(DateTime.MinValue, DateTime.MaxValue);
            ViewBag.Message = _6Over45No[0].

            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}
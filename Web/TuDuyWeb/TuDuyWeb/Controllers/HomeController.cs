using System;
using System.Web.Mvc;
using TuDuyWeb.Services;

namespace TuDuyWeb.Controllers
{
    public class HomeController : Controller
    {
        private readonly MemoryRepository _repository = new MemoryRepository();

        [HttpGet]
        public ActionResult Index()
        {
            var model = _repository.GetOrCreateTodaySession(DateTime.Today);
            model.StatusMessage = TempData["StatusMessage"] as string;
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Save(TuDuyWeb.Models.HomeIndexViewModel model)
        {
            _repository.SaveSession(model);
            TempData["StatusMessage"] = "Da luu buoi tu duy hom nay.";
            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult History()
        {
            var sessions = _repository.GetRecentSessions();
            return View(sessions);
        }

        [HttpGet]
        public ActionResult Details(long id)
        {
            var model = _repository.GetSessionDetail(id);
            if (model == null)
            {
                return HttpNotFound();
            }

            return View(model);
        }
    }
}

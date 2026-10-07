using System;
using System.Configuration;
using System.Data.Entity;
using System.Globalization;
using System.Linq;
using System.Web.Mvc;
using LotteryDAL;
using Newtonsoft.Json;
using WebAppLottery.Models;

namespace WebAppLottery.Controllers
{
    // Read-only JSON endpoints; fetching history never invokes the scraper.
    public class LotteryApiController : Controller
    {
        private ActionResult JsonResponse(object data, int status = 200)
        {
            Response.StatusCode = status;
            Response.TrySkipIisCustomErrors = true;
            Response.SuppressFormsAuthenticationRedirect = true;
            Response.Cache.SetCacheability(System.Web.HttpCacheability.NoCache);
            return Content(JsonConvert.SerializeObject(data), "application/json", System.Text.Encoding.UTF8);
        }

        private bool Authorized()
        {
            var key = ConfigurationManager.AppSettings["LotteryApiKey"];
            return string.IsNullOrWhiteSpace(key) ||
                string.Equals(Request.Headers["Authorization"], "Bearer " + key, StringComparison.Ordinal);
        }

        [HttpGet]
        public ActionResult Draws(string game, string from = null, string to = null,
            string limit = null, string offset = "0")
        {
            if (!Authorized()) return JsonResponse(new { error = "unauthorized" }, 401);
            short type;
            switch ((game ?? "").ToLowerInvariant())
            {
                case "645": type = (short)Enum_NumberType._6Over45; break;
                case "655": type = (short)Enum_NumberType._6Over55; break;
                case "3dmax": type = (short)Enum_NumberType._3DMax; break;
                case "3dmaxpro": type = (short)Enum_NumberType._3DMaxPro; break;
                default: return JsonResponse(new { error = "invalid_game", message = "Use 645, 655, 3dmax, or 3dmaxpro." }, 400);
            }
            int take = int.MaxValue, skip;
            if ((limit != null && (!int.TryParse(limit, NumberStyles.None, CultureInfo.InvariantCulture, out take) || take < 1 || take > 100)) ||
                !int.TryParse(offset, NumberStyles.None, CultureInfo.InvariantCulture, out skip) || skip < 0 || skip > 1000000)
                return JsonResponse(new { error = "invalid_pagination", message = "Omit limit for full history; explicit limit: 1..100; offset: 0..1000000." }, 400);
            DateTime first = DateTime.MinValue, last = DateTime.MaxValue;
            if ((from != null && !DateTime.TryParseExact(from, "yyyy-MM-dd", CultureInfo.InvariantCulture, DateTimeStyles.None, out first)) ||
                (to != null && !DateTime.TryParseExact(to, "yyyy-MM-dd", CultureInfo.InvariantCulture, DateTimeStyles.None, out last)) || first > last)
                return JsonResponse(new { error = "invalid_dates", message = "Use yyyy-MM-dd and from <= to." }, 400);
            game = game.ToLowerInvariant();
            try
            {
                using (var db = new LotteryEntities())
                {
                    db.Database.CommandTimeout = 20;
                    var all = db.Numbers.AsNoTracking().Where(x => x.NumberTypeId == type);
                    var latest = all.Select(x => (DateTime?)x.DatePublish).Max();
                    var filtered = all;
                    if (from != null) filtered = filtered.Where(x => x.DatePublish >= first);
                    if (to != null && last < DateTime.MaxValue.Date)
                    {
                        var exclusiveEnd = last.AddDays(1);
                        filtered = filtered.Where(x => x.DatePublish < exclusiveEnd);
                    }
                    var dates = filtered.Select(x => DbFunctions.TruncateTime(x.DatePublish)).Distinct();
                    var total = dates.Count();
                    var page = dates.OrderByDescending(x => x).Skip(skip).Take(take).ToList();
                    var rowQuery = limit == null ? filtered :
                        filtered.Where(x => page.Contains(DbFunctions.TruncateTime(x.DatePublish)));
                    var rows = page.Count == 0 ? new System.Collections.Generic.List<Number>() :
                        rowQuery.OrderBy(x => x.NumberId).ToList();
                    var rowsByDate = rows.ToLookup(x => x.DatePublish.Date);
                    var draws = page.Select(date => LotteryDrawFormatter.Format(game,
                        date.Value, rowsByDate[date.Value].ToList())).ToList();
                    return JsonResponse(new
                    {
                        game, source = "local_database", timezone = "Asia/Ho_Chi_Minh",
                        fetchedAtUtc = DateTime.UtcNow.ToString("o"),
                        latestAvailableDrawDate = latest.HasValue ? latest.Value.ToString("yyyy-MM-dd") : null,
                        totalDraws = total, limit = limit == null ? (int?)null : take, offset = skip,
                        hasMore = (long)skip + draws.Count < total,
                        nextOffset = skip + draws.Count < total ? (int?)(skip + draws.Count) : null,
                        draws
                    });
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Trace.TraceError("Lottery API: {0}", ex);
                return JsonResponse(new { error = "data_unavailable", message = "Cannot read lottery history. Try again later." }, 503);
            }
        }

        [HttpGet]
        public ActionResult All()
        {
            if (!Authorized()) return JsonResponse(new { error = "unauthorized" }, 401);
            try
            {
                using (var db = new LotteryEntities())
                {
                    db.Database.CommandTimeout = 30;
                    var types = new short[] { (short)Enum_NumberType._6Over45, (short)Enum_NumberType._6Over55,
                        (short)Enum_NumberType._3DMax, (short)Enum_NumberType._3DMaxPro };
                    var rows = db.Numbers.AsNoTracking().Where(x => types.Contains(x.NumberTypeId))
                        .OrderBy(x => x.NumberId).ToList();
                    var games = LotteryDrawFormatter.FormatAll(rows);
                    return JsonResponse(new { source = "local_database", timezone = "Asia/Ho_Chi_Minh",
                        fetchedAtUtc = DateTime.UtcNow.ToString("o"), fullHistory = true, games });
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Trace.TraceError("Lottery full history API: {0}", ex);
                return JsonResponse(new { error = "data_unavailable", message = "Cannot read complete lottery history. Try again later." }, 503);
            }
        }

        [HttpGet]
        public ActionResult OpenApi()
        {
            return Content(System.IO.File.ReadAllText(Server.MapPath("~/api-docs/openapi.json")),
                "application/json", System.Text.Encoding.UTF8);
        }
    }
}

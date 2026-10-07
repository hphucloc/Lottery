using System;
using System.Collections.Generic;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using LotteryDAL;
using WebAppLottery.Controllers;
using WebAppLottery.Models;

class FakeCache : HttpCachePolicyBase { public override void SetCacheability(HttpCacheability value) {} }
class FakeResponse : HttpResponseBase
{
    public override int StatusCode { get; set; }
    public override bool TrySkipIisCustomErrors { get; set; }
    public override bool SuppressFormsAuthenticationRedirect { get; set; }
    public override HttpCachePolicyBase Cache { get { return new FakeCache(); } }
}
class FakeRequest : HttpRequestBase
{
    public override System.Collections.Specialized.NameValueCollection Headers { get { return new System.Collections.Specialized.NameValueCollection(); } }
}
class FakeContext : HttpContextBase
{
    private readonly FakeResponse response = new FakeResponse();
    public override HttpResponseBase Response { get { return response; } }
    public override HttpRequestBase Request { get { return new FakeRequest(); } }
}
class ApiTests
{
    static void Check(bool condition, string message) { if (!condition) throw new Exception(message); }
    static Number Row(long id, short level, string number) { return new Number { NumberId=id, NumberWinLevelId=level, LotNumber=number }; }
    static void BadRequest(string game, string from=null, string to=null, string limit="50", string offset="0")
    {
        var context = new FakeContext();
        var controller = new LotteryApiController();
        controller.ControllerContext = new ControllerContext(context, new RouteData(), controller);
        var result = controller.Draws(game, from, to, limit, offset) as ContentResult;
        Check(context.Response.StatusCode == 400, "Expected 400");
        Check(result.ContentType == "application/json" && result.Content.Contains("error"), "Expected JSON error");
    }
    static void Main()
    {
        var date = new DateTime(2026,10,7);
        var d = LotteryDrawFormatter.Format("3dmax", date, new List<Number> {
            Row(1,1,"007042"), Row(2,2,"001002003004"),
            Row(3,3,"005006007008009010"), Row(4,4,"011012013014015016017018") });
        Check(d.date == "2026-10-07" && d.prizes[0].numbers[0] == "007", "Leading zeros / date lost");
        Check(d.prizes[0].numbers[1] == "042" && d.warnings.Count == 0, "Prize decoding failed");
        var bad = LotteryDrawFormatter.Format("3dmax",date,new List<Number>{Row(1,1,"12X"),Row(2,2,"12")});
        Check(bad.warnings.Count >= 4 && bad.prizes[0].numbers.Count == 0,"Malformed data accepted");
        var lotto = LotteryDrawFormatter.Format("645",date,new List<Number>{Row(6,1,"45"),Row(5,1,"5"),Row(4,1,"4"),Row(3,1,"3"),Row(2,1,"2"),Row(1,1,"1")});
        Check(lotto.warnings.Count == 0 && lotto.prizes[0].numbers[0] == "01", "Stored order or padding incorrect");
        var duplicate=LotteryDrawFormatter.Format("645",date,new List<Number>{Row(1,1,"1"),Row(2,1,"1"),Row(3,1,"46")});
        Check(duplicate.warnings.Count == 3, "Invalid range / duplicates / incomplete draw not flagged");
        var power=LotteryDrawFormatter.Format("655",date,new List<Number>{Row(1,1,"55")});
        Check(power.warnings.Exists(x=>x.Contains("main/bonus")), "Missing Power 655 ambiguity warning");
        BadRequest("unknown"); BadRequest("645",limit:"0"); BadRequest("645",limit:"101");
        BadRequest("645",offset:"-1"); BadRequest("645",limit:"oops");
        BadRequest("645",from:"07/10/2026"); BadRequest("645",to:"2026-02-30");
        BadRequest("645",from:"2026-10-08",to:"2026-10-07");
        var historyRows = new List<Number>();
        foreach (short type in new short[] {1,2,3,4})
            for (int i=0; i<150; i++)
                historyRows.Add(new Number { NumberId=i, NumberTypeId=type, NumberWinLevelId=1,
                    LotNumber=type==2 || type==4 ? "007042" : "1", DatePublish=date.AddDays(-i) });
        var history=LotteryDrawFormatter.FormatAll(historyRows);
        Check(history.Count==4, "Full export must include all four games");
        foreach (var gameHistory in history)
            Check(gameHistory.totalDraws==150 && gameHistory.draws.Count==150 &&
                gameHistory.draws[0].date=="2026-10-07" && gameHistory.draws[149].date==date.AddDays(-149).ToString("yyyy-MM-dd"),
                "Full export truncated history or sorted incorrectly");
        var emptyHistory=LotteryDrawFormatter.FormatAll(new List<Number>());
        Check(emptyHistory.Count==4 && emptyHistory[0].totalDraws==0 && emptyHistory[0].latestAvailableDrawDate==null,
            "Empty games must remain present");
        var limitParameter=typeof(LotteryApiController).GetMethod("Draws").GetParameters()[3];
        Check(limitParameter.DefaultValue==null,"Per-game history must default to unlimited");
        Console.WriteLine("PASS: formatting, data-quality warnings, and HTTP input validation.");
    }
}

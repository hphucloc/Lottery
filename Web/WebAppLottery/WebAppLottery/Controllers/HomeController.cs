using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;

using LotteryBusiness;
using WebAppLottery.Models;

namespace WebAppLottery.Controllers
{
    public class HomeController : Controller
    {     
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
                int count48_55 = 0;
                int count56_63 = 0;
                int count64_71 = 0;
                int count72_80 = 0;

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
                    m.Data = _6Over45TimeLine.Get6Over45Number(from, to).Select(x => new LotteryStatistic1
                    {
                        LotNumber = Convert.ToInt32(x.LotNumber),
                        AllDatePublishList = x.AllDatePublishList,
                        DatePublish = x.DatePublish,
                        TotalNumberAppearInRange = x.TotalNumberAppearInRange,
                        DatePublishList = x.DatePublishList
                    }).OrderBy(x => x.LotNumber).ToList();
                }                
                else if (m.ListLoaiVe == IndexPageModel.LoaiVe._6Over55)
                {                                
                    m.Data = _6Over55TimeLine.Get6Over55Number(from, to).Select(x => new LotteryStatistic1
                    {
                        LotNumber = Convert.ToInt32(x.LotNumber),
                        AllDatePublishList = x.AllDatePublishList,
                        DatePublish = x.DatePublish,
                        TotalNumberAppearInRange = x.TotalNumberAppearInRange,
                        DatePublishList = x.DatePublishList
                    }).OrderBy(x => x.LotNumber).ToList();
                }
                else if (m.ListLoaiVe == IndexPageModel.LoaiVe._Keno)
                {
                    if (to.Date.Subtract(from.Date) >= new TimeSpan(7, 0, 0, 0, 0))
                    {
                        from = DateTime.Now.AddDays(-7);
                        m.ErrorMessage = "Keno chỉ hổ trợ dữ liệu cách hiện tại 7 ngày";
                    }

                    var numbers = _KenoTimeLine.GetKenoNumber(from, to);
                    var _KenoNoData = _KenoTimeLine.GetKenoNumberStatistic(numbers);
                    m.Data = _KenoNoData.Select(x => new LotteryStatistic1
                    {
                        LotNumber = Convert.ToInt32(x.LotNumber),
                        AllDatePublishList = x.AllDatePublishList,
                        DatePublish = x.DatePublish,
                        TotalNumberAppearInRange = x.TotalNumberAppearInRange,
                        DatePublishList = x.DatePublishList,
                    }).OrderBy(x => Convert.ToInt32(x.LotNumber)).ToList();
                    m.KyQuays = _KenoTimeLine.GetKenoNumberKyquay(numbers);
                    m.ChanleLonNhos = _KenoTimeLine.GetKenoChanleLonNho(from, to);
                    m.HoaChanLeNo = m.ChanleLonNhos.Count(x => x.Value[0].ToLower().Contains("Hòa".ToLower()));
                    m.ChanNo= m.ChanleLonNhos.Count(x => x.Value[0].ToLower().Contains("Chẵn".ToLower()));
                    m.LeNo = m.ChanleLonNhos.Count(x => x.Value[0].ToLower().Contains("Lẻ".ToLower()));
                    m.HoaLonNhoNo = m.ChanleLonNhos.Count(x => x.Value[1].ToLower().Contains("Hòa".ToLower()));
                    m.LonNo = m.ChanleLonNhos.Count(x => x.Value[1].ToLower().Contains("Lớn".ToLower()));
                    m.NhoNo = m.ChanleLonNhos.Count(x => x.Value[1].ToLower().Contains("Nhỏ".ToLower()));
                }
                else if (m.ListLoaiVe == IndexPageModel.LoaiVe._3DMax)
                {
                    m.OriginalData = _3DMaxTimeLine.Get3DMaxNumberDacBiet(from, to).OrderByDescending(x => x.DatePublish).ToList();
                    m.GiaiNhat = _3DMaxTimeLine.Get3DMaxNumberGiaiNhat(from, to).OrderByDescending(x => x.DatePublish).ToList();
                    m.GiaiNhi = _3DMaxTimeLine.Get3DMaxNumberGiaiNhi(from, to).OrderByDescending(x => x.DatePublish).ToList();
                    m.GiaiBa = _3DMaxTimeLine.Get3DMaxNumberGiaiBa(from, to).OrderByDescending(x => x.DatePublish).ToList();
                }
                else if (m.ListLoaiVe == IndexPageModel.LoaiVe._3DMaxPro)
                {
                    m.OriginalData = _3DMaxProTimeLine.Get3DMaxProNumberDacBiet(from, to).OrderByDescending(x => x.DatePublish).ToList();
                    m.GiaiNhat = _3DMaxProTimeLine.Get3DMaxProNumberGiaiNhat(from, to).OrderByDescending(x => x.DatePublish).ToList();
                    m.GiaiNhi = _3DMaxProTimeLine.Get3DMaxProNumberGiaiNhi(from, to).OrderByDescending(x => x.DatePublish).ToList();
                    m.GiaiBa = _3DMaxProTimeLine.Get3DMaxProNumberGiaiBa(from, to).OrderByDescending(x => x.DatePublish).ToList();
                }

                //************************Render Body*************************//    
                if (m.Data != null && m.Data.Count > 0)
                {
                    m.AllDatePublist = m.Data[0].AllDatePublishList;
                }
                else if (m.OriginalData != null && m.OriginalData.Count > 0)
                {
                    m.AllDatePublist = m.OriginalData[0].AllDatePublishList;
                }

                //************************Render GroupNumberStatistic*************************//
                if (m.Data != null && m.Data.Count > 0) //6/45, 6/55, Keno
                {
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
                        else if (Convert.ToInt32(i.LotNumber) >= 48 && Convert.ToInt32(i.LotNumber) <= 55)
                        {
                            count48_55 += i.TotalNumberAppearInRange;
                        }
                        else if (Convert.ToInt32(i.LotNumber) >= 56 && Convert.ToInt32(i.LotNumber) <= 63)
                        {
                            count56_63 += i.TotalNumberAppearInRange;
                        }
                        else if (Convert.ToInt32(i.LotNumber) >= 64 && Convert.ToInt32(i.LotNumber) <= 71)
                        {
                            count64_71 += i.TotalNumberAppearInRange;
                        }
                        else if (Convert.ToInt32(i.LotNumber) >= 72 && Convert.ToInt32(i.LotNumber) <= 80)
                        {
                            count72_80 += i.TotalNumberAppearInRange;
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

                    if (m.ListLoaiVe != IndexPageModel.LoaiVe._Keno)
                    {
                        //Prepare data for Color timeline
                        foreach (var item in hitNumberByDate)
                        {
                            foreach (var aNo in m.Data)
                                foreach (var date in aNo.DatePublishList.DatePublishList1)
                                    if (item.Key == date)
                                        item.Value.Add(Convert.ToInt32(aNo.LotNumber));                            
                        }
                    }
                    m.groupNumberStatistic = hitNumberByDate;
                    m.NoAppear1To7 = count1_7;
                    m.NoAppear8To15 = count8_15;
                    m.NoAppear16To23 = count16_23;
                    m.NoAppear24To31 = count24_31;
                    m.NoAppear32To39 = count32_39;
                    m.NoAppear40To47 = count40_47;
                    m.NoAppear48To55 = count48_55;
                    m.NoAppear56To63 = count56_63;
                    m.NoAppear64To71 = count64_71;
                    m.NoAppear72To80 = count72_80;
                } 
                else if (m.OriginalData != null && m.OriginalData.Count > 0 && 
                    m.ListLoaiVe == IndexPageModel.LoaiVe._3DMax) //3DMax
                {
                    #region GiaiDacBiet
                    SortedDictionary<DateTime, SortedSet<string>> hitNumberByDate =
                        new SortedDictionary<DateTime, SortedSet<string>>();
                    foreach (var i in m.OriginalData)
                    {
                        if (Convert.ToInt32(i.LotNumber) >= 1 && Convert.ToInt32(i.LotNumber) <= 100)
                        {
                            count1_7 += i.TotalNumberAppearInRange;
                        }
                        else
                            if (Convert.ToInt32(i.LotNumber) >= 101 && Convert.ToInt32(i.LotNumber) <= 200)
                        {
                            count8_15 += i.TotalNumberAppearInRange;
                        }
                        else
                            if (Convert.ToInt32(i.LotNumber) >= 201 && Convert.ToInt32(i.LotNumber) <= 300)
                        {
                            count16_23 += i.TotalNumberAppearInRange;
                        }
                        else
                            if (Convert.ToInt32(i.LotNumber) >= 301 && Convert.ToInt32(i.LotNumber) <= 400)
                        {
                            count24_31 += i.TotalNumberAppearInRange;
                        }
                        else
                            if (Convert.ToInt32(i.LotNumber) >= 401 && Convert.ToInt32(i.LotNumber) <= 500)
                        {
                            count32_39 += i.TotalNumberAppearInRange;
                        }
                        else
                            if (Convert.ToInt32(i.LotNumber) >= 501 && Convert.ToInt32(i.LotNumber) <= 600)
                        {
                            count40_47 += i.TotalNumberAppearInRange;
                        }
                        else if (Convert.ToInt32(i.LotNumber) >= 601 && Convert.ToInt32(i.LotNumber) <= 700)
                        {
                            count48_55 += i.TotalNumberAppearInRange;
                        }
                        else if (Convert.ToInt32(i.LotNumber) >= 701 && Convert.ToInt32(i.LotNumber) <= 800)
                        {
                            count56_63 += i.TotalNumberAppearInRange;
                        }
                        else if (Convert.ToInt32(i.LotNumber) >= 801 && Convert.ToInt32(i.LotNumber) <= 900)
                        {
                            count64_71 += i.TotalNumberAppearInRange;
                        }
                        else if (Convert.ToInt32(i.LotNumber) >= 901 && Convert.ToInt32(i.LotNumber) <= 999)
                        {
                            count72_80 += i.TotalNumberAppearInRange;
                        }

                        //Get all publih Date               
                        foreach (var date in i.AllDatePublishList)
                        {
                            if (!hitNumberByDate.ContainsKey(date))
                            {
                                hitNumberByDate.Add(date, new SortedSet<string>());
                            }
                        }
                    }

                    //Prepare data for Color timeline
                    foreach (var item in hitNumberByDate)
                    {
                        foreach (var aNo in m.OriginalData)
                            foreach (var date in aNo.DatePublishList.DatePublishList1)
                                if (item.Key == date)
                                    item.Value.Add(aNo.LotNumber);                       
                    }

                    //Count number appear
                    List<int> temp = new List<int>();
                    foreach (var i in hitNumberByDate.Keys)
                    {
                        foreach (var j in hitNumberByDate[i])
                        {
                            temp.Add(Convert.ToInt32(j));
                        }
                    }
                    SortedDictionary<DateTime, SortedSet<string>> hitNumberByDateDB =
                       new SortedDictionary<DateTime, SortedSet<string>>();
                    foreach (var i in hitNumberByDate.Keys)
                    {
                        SortedSet<string> k = new SortedSet<string>();
                        foreach (var j in hitNumberByDate[i])
                        {                            
                            k.Add(j + " (" + temp.Count(x => x == Convert.ToInt32(j)) + ")");                            
                        }
                        if (k.Count == 2)
                            hitNumberByDateDB.Add(i, k);
                    }

                    m.groupNumberStatistic3DMax = hitNumberByDateDB;
                    m.NoAppear1To7 = count1_7;
                    m.NoAppear8To15 = count8_15;
                    m.NoAppear16To23 = count16_23;
                    m.NoAppear24To31 = count24_31;
                    m.NoAppear32To39 = count32_39;
                    m.NoAppear40To47 = count40_47;
                    m.NoAppear48To55 = count48_55;
                    m.NoAppear56To63 = count56_63;
                    m.NoAppear64To71 = count64_71;
                    m.NoAppear72To80 = count72_80;
                    #endregion

                    count1_7 = count8_15 = count16_23 = count24_31 = 
                        count32_39 = count40_47 = count48_55 =
                        count56_63 = count64_71 = count72_80 = 0;                    

                    #region GiaiNhat
                    hitNumberByDate = new SortedDictionary<DateTime, SortedSet<string>>();
                    foreach (var i in m.GiaiNhat)
                    {
                        if (Convert.ToInt32(i.LotNumber) >= 1 && Convert.ToInt32(i.LotNumber) <= 100)
                        {
                            count1_7 += i.TotalNumberAppearInRange;
                        }
                        else
                            if (Convert.ToInt32(i.LotNumber) >= 101 && Convert.ToInt32(i.LotNumber) <= 200)
                        {
                            count8_15 += i.TotalNumberAppearInRange;
                        }
                        else
                            if (Convert.ToInt32(i.LotNumber) >= 201 && Convert.ToInt32(i.LotNumber) <= 300)
                        {
                            count16_23 += i.TotalNumberAppearInRange;
                        }
                        else
                            if (Convert.ToInt32(i.LotNumber) >= 301 && Convert.ToInt32(i.LotNumber) <= 400)
                        {
                            count24_31 += i.TotalNumberAppearInRange;
                        }
                        else
                            if (Convert.ToInt32(i.LotNumber) >= 401 && Convert.ToInt32(i.LotNumber) <= 500)
                        {
                            count32_39 += i.TotalNumberAppearInRange;
                        }
                        else
                            if (Convert.ToInt32(i.LotNumber) >= 501 && Convert.ToInt32(i.LotNumber) <= 600)
                        {
                            count40_47 += i.TotalNumberAppearInRange;
                        }
                        else if (Convert.ToInt32(i.LotNumber) >= 601 && Convert.ToInt32(i.LotNumber) <= 700)
                        {
                            count48_55 += i.TotalNumberAppearInRange;
                        }
                        else if (Convert.ToInt32(i.LotNumber) >= 701 && Convert.ToInt32(i.LotNumber) <= 800)
                        {
                            count56_63 += i.TotalNumberAppearInRange;
                        }
                        else if (Convert.ToInt32(i.LotNumber) >= 801 && Convert.ToInt32(i.LotNumber) <= 900)
                        {
                            count64_71 += i.TotalNumberAppearInRange;
                        }
                        else if (Convert.ToInt32(i.LotNumber) >= 901 && Convert.ToInt32(i.LotNumber) <= 999)
                        {
                            count72_80 += i.TotalNumberAppearInRange;
                        }

                        //Get all publih Date               
                        foreach (var date in i.AllDatePublishList)
                        {
                            if (!hitNumberByDate.ContainsKey(date))
                            {
                                hitNumberByDate.Add(date, new SortedSet<string>());
                            }
                        }
                    }

                    //Prepare data for Color timeline
                    foreach (var item in hitNumberByDate)
                    {
                        foreach (var aNo in m.GiaiNhat)
                            foreach (var date in aNo.DatePublishList.DatePublishList1)
                                if (item.Key == date)
                                    item.Value.Add(aNo.LotNumber);
                    }

                    //Count number appear
                    temp = new List<int>();
                    foreach (var i in hitNumberByDate.Keys)
                    {
                        foreach (var j in hitNumberByDate[i])
                        {
                            temp.Add(Convert.ToInt32(j));
                        }
                    }

                    SortedDictionary<DateTime, SortedSet<string>> hitNumberByDateG1 =
                       new SortedDictionary<DateTime, SortedSet<string>>();
                    foreach (var i in hitNumberByDate.Keys)
                    {
                        SortedSet<string> k = new SortedSet<string>();
                        foreach (var j in hitNumberByDate[i])
                        {
                            k.Add(j + " (" + temp.Count(x => x == Convert.ToInt32(j)) + ")");
                        }
                        if (k.Count == 4)
                            hitNumberByDateG1.Add(i, k);
                    }

                    m.groupNumberStatistic3DMaxGiaiNhat = hitNumberByDateG1;
                    m.NoAppear1To7G1 = count1_7;
                    m.NoAppear8To15G1 = count8_15;
                    m.NoAppear16To23G1 = count16_23;
                    m.NoAppear24To31G1 = count24_31;
                    m.NoAppear32To39G1 = count32_39;
                    m.NoAppear40To47G1 = count40_47;
                    m.NoAppear48To55G1 = count48_55;
                    m.NoAppear56To63G1 = count56_63;
                    m.NoAppear64To71G1 = count64_71;
                    m.NoAppear72To80G1 = count72_80;
                    #endregion                   

                    count1_7 = count8_15 = count16_23 = count24_31 =
                        count32_39 = count40_47 = count48_55 =
                        count56_63 = count64_71 = count72_80 = 0;

                    #region GiaiNhi
                    hitNumberByDate = new SortedDictionary<DateTime, SortedSet<string>>();
                    foreach (var i in m.GiaiNhi)
                    {
                        if (Convert.ToInt32(i.LotNumber) >= 1 && Convert.ToInt32(i.LotNumber) <= 100)
                        {
                            count1_7 += i.TotalNumberAppearInRange;
                        }
                        else
                            if (Convert.ToInt32(i.LotNumber) >= 101 && Convert.ToInt32(i.LotNumber) <= 200)
                        {
                            count8_15 += i.TotalNumberAppearInRange;
                        }
                        else
                            if (Convert.ToInt32(i.LotNumber) >= 201 && Convert.ToInt32(i.LotNumber) <= 300)
                        {
                            count16_23 += i.TotalNumberAppearInRange;
                        }
                        else
                            if (Convert.ToInt32(i.LotNumber) >= 301 && Convert.ToInt32(i.LotNumber) <= 400)
                        {
                            count24_31 += i.TotalNumberAppearInRange;
                        }
                        else
                            if (Convert.ToInt32(i.LotNumber) >= 401 && Convert.ToInt32(i.LotNumber) <= 500)
                        {
                            count32_39 += i.TotalNumberAppearInRange;
                        }
                        else
                            if (Convert.ToInt32(i.LotNumber) >= 501 && Convert.ToInt32(i.LotNumber) <= 600)
                        {
                            count40_47 += i.TotalNumberAppearInRange;
                        }
                        else if (Convert.ToInt32(i.LotNumber) >= 601 && Convert.ToInt32(i.LotNumber) <= 700)
                        {
                            count48_55 += i.TotalNumberAppearInRange;
                        }
                        else if (Convert.ToInt32(i.LotNumber) >= 701 && Convert.ToInt32(i.LotNumber) <= 800)
                        {
                            count56_63 += i.TotalNumberAppearInRange;
                        }
                        else if (Convert.ToInt32(i.LotNumber) >= 801 && Convert.ToInt32(i.LotNumber) <= 900)
                        {
                            count64_71 += i.TotalNumberAppearInRange;
                        }
                        else if (Convert.ToInt32(i.LotNumber) >= 901 && Convert.ToInt32(i.LotNumber) <= 999)
                        {
                            count72_80 += i.TotalNumberAppearInRange;
                        }

                        //Get all publih Date               
                        foreach (var date in i.AllDatePublishList)
                        {
                            if (!hitNumberByDate.ContainsKey(date))
                            {
                                hitNumberByDate.Add(date, new SortedSet<string>());
                            }
                        }
                    }

                    //Prepare data for Color timeline
                    foreach (var item in hitNumberByDate)
                    {
                        foreach (var aNo in m.GiaiNhi)
                            foreach (var date in aNo.DatePublishList.DatePublishList1)
                                if (item.Key == date)
                                    item.Value.Add(aNo.LotNumber);
                    }

                    //Count number appear
                    temp = new List<int>();
                    foreach (var i in hitNumberByDate.Keys)
                    {
                        foreach (var j in hitNumberByDate[i])
                        {
                            temp.Add(Convert.ToInt32(j));
                        }
                    }

                    SortedDictionary<DateTime, SortedSet<string>> hitNumberByDateG2 =
                       new SortedDictionary<DateTime, SortedSet<string>>();
                    foreach (var i in hitNumberByDate.Keys)
                    {
                        SortedSet<string> k = new SortedSet<string>();
                        foreach (var j in hitNumberByDate[i])
                        {
                            k.Add(j + " (" + temp.Count(x => x == Convert.ToInt32(j)) + ")");
                        }
                        if(k.Count==6)
                            hitNumberByDateG2.Add(i, k);
                    }

                    m.groupNumberStatistic3DMaxGiaiNhi = hitNumberByDateG2;
                    m.NoAppear1To7G2 = count1_7;
                    m.NoAppear8To15G2 = count8_15;
                    m.NoAppear16To23G2 = count16_23;
                    m.NoAppear24To31G2 = count24_31;
                    m.NoAppear32To39G2 = count32_39;
                    m.NoAppear40To47G2 = count40_47;
                    m.NoAppear48To55G2 = count48_55;
                    m.NoAppear56To63G2 = count56_63;
                    m.NoAppear64To71G2 = count64_71;
                    m.NoAppear72To80G2 = count72_80;
                    #endregion                   

                    count1_7 = count8_15 = count16_23 = count24_31 =
                        count32_39 = count40_47 = count48_55 =
                        count56_63 = count64_71 = count72_80 = 0;

                    #region GiaiBa
                    hitNumberByDate = new SortedDictionary<DateTime, SortedSet<string>>();
                    foreach (var i in m.GiaiBa)
                    {
                        if (Convert.ToInt32(i.LotNumber) >= 1 && Convert.ToInt32(i.LotNumber) <= 100)
                        {
                            count1_7 += i.TotalNumberAppearInRange;
                        }
                        else
                            if (Convert.ToInt32(i.LotNumber) >= 101 && Convert.ToInt32(i.LotNumber) <= 200)
                        {
                            count8_15 += i.TotalNumberAppearInRange;
                        }
                        else
                            if (Convert.ToInt32(i.LotNumber) >= 201 && Convert.ToInt32(i.LotNumber) <= 300)
                        {
                            count16_23 += i.TotalNumberAppearInRange;
                        }
                        else
                            if (Convert.ToInt32(i.LotNumber) >= 301 && Convert.ToInt32(i.LotNumber) <= 400)
                        {
                            count24_31 += i.TotalNumberAppearInRange;
                        }
                        else
                            if (Convert.ToInt32(i.LotNumber) >= 401 && Convert.ToInt32(i.LotNumber) <= 500)
                        {
                            count32_39 += i.TotalNumberAppearInRange;
                        }
                        else
                            if (Convert.ToInt32(i.LotNumber) >= 501 && Convert.ToInt32(i.LotNumber) <= 600)
                        {
                            count40_47 += i.TotalNumberAppearInRange;
                        }
                        else if (Convert.ToInt32(i.LotNumber) >= 601 && Convert.ToInt32(i.LotNumber) <= 700)
                        {
                            count48_55 += i.TotalNumberAppearInRange;
                        }
                        else if (Convert.ToInt32(i.LotNumber) >= 701 && Convert.ToInt32(i.LotNumber) <= 800)
                        {
                            count56_63 += i.TotalNumberAppearInRange;
                        }
                        else if (Convert.ToInt32(i.LotNumber) >= 801 && Convert.ToInt32(i.LotNumber) <= 900)
                        {
                            count64_71 += i.TotalNumberAppearInRange;
                        }
                        else if (Convert.ToInt32(i.LotNumber) >= 901 && Convert.ToInt32(i.LotNumber) <= 999)
                        {
                            count72_80 += i.TotalNumberAppearInRange;
                        }

                        //Get all publih Date               
                        foreach (var date in i.AllDatePublishList)
                        {
                            if (!hitNumberByDate.ContainsKey(date))
                            {
                                hitNumberByDate.Add(date, new SortedSet<string>());
                            }
                        }
                    }

                    //Prepare data for Color timeline
                    foreach (var item in hitNumberByDate)
                    {
                        foreach (var aNo in m.GiaiBa)
                            foreach (var date in aNo.DatePublishList.DatePublishList1)
                                if (item.Key == date)
                                    item.Value.Add(aNo.LotNumber);
                    }

                    //Count number appear
                    temp = new List<int>();
                    foreach (var i in hitNumberByDate.Keys)
                    {
                        foreach (var j in hitNumberByDate[i])
                        {
                            temp.Add(Convert.ToInt32(j));
                        }
                    }

                    SortedDictionary<DateTime, SortedSet<string>> hitNumberByDateG3 =
                       new SortedDictionary<DateTime, SortedSet<string>>();
                    foreach (var i in hitNumberByDate.Keys)
                    {
                        SortedSet<string> k = new SortedSet<string>();
                        foreach (var j in hitNumberByDate[i])
                        {
                            k.Add(j + " (" + temp.Count(x => x == Convert.ToInt32(j)) + ")");
                        }
                        if (k.Count == 8)
                            hitNumberByDateG3.Add(i, k);
                    }

                    m.groupNumberStatistic3DMaxGiaiBa = hitNumberByDateG3;
                    m.NoAppear1To7G3 = count1_7;
                    m.NoAppear8To15G3 = count8_15;
                    m.NoAppear16To23G3 = count16_23;
                    m.NoAppear24To31G3 = count24_31;
                    m.NoAppear32To39G3 = count32_39;
                    m.NoAppear40To47G3 = count40_47;
                    m.NoAppear48To55G3 = count48_55;
                    m.NoAppear56To63G3 = count56_63;
                    m.NoAppear64To71G3 = count64_71;
                    m.NoAppear72To80G3 = count72_80;
                    #endregion                   
                }
                else if (m.OriginalData != null && m.OriginalData.Count > 0 &&
                   m.ListLoaiVe == IndexPageModel.LoaiVe._3DMaxPro) //3DMaxPro
                {
                    #region GiaiDacBiet
                    SortedDictionary<DateTime, SortedSet<string>> hitNumberByDate =
                        new SortedDictionary<DateTime, SortedSet<string>>();
                    foreach (var i in m.OriginalData)
                    {
                        if (Convert.ToInt32(i.LotNumber) >= 1 && Convert.ToInt32(i.LotNumber) <= 100)
                        {
                            count1_7 += i.TotalNumberAppearInRange;
                        }
                        else
                            if (Convert.ToInt32(i.LotNumber) >= 101 && Convert.ToInt32(i.LotNumber) <= 200)
                        {
                            count8_15 += i.TotalNumberAppearInRange;
                        }
                        else
                            if (Convert.ToInt32(i.LotNumber) >= 201 && Convert.ToInt32(i.LotNumber) <= 300)
                        {
                            count16_23 += i.TotalNumberAppearInRange;
                        }
                        else
                            if (Convert.ToInt32(i.LotNumber) >= 301 && Convert.ToInt32(i.LotNumber) <= 400)
                        {
                            count24_31 += i.TotalNumberAppearInRange;
                        }
                        else
                            if (Convert.ToInt32(i.LotNumber) >= 401 && Convert.ToInt32(i.LotNumber) <= 500)
                        {
                            count32_39 += i.TotalNumberAppearInRange;
                        }
                        else
                            if (Convert.ToInt32(i.LotNumber) >= 501 && Convert.ToInt32(i.LotNumber) <= 600)
                        {
                            count40_47 += i.TotalNumberAppearInRange;
                        }
                        else if (Convert.ToInt32(i.LotNumber) >= 601 && Convert.ToInt32(i.LotNumber) <= 700)
                        {
                            count48_55 += i.TotalNumberAppearInRange;
                        }
                        else if (Convert.ToInt32(i.LotNumber) >= 701 && Convert.ToInt32(i.LotNumber) <= 800)
                        {
                            count56_63 += i.TotalNumberAppearInRange;
                        }
                        else if (Convert.ToInt32(i.LotNumber) >= 801 && Convert.ToInt32(i.LotNumber) <= 900)
                        {
                            count64_71 += i.TotalNumberAppearInRange;
                        }
                        else if (Convert.ToInt32(i.LotNumber) >= 901 && Convert.ToInt32(i.LotNumber) <= 999)
                        {
                            count72_80 += i.TotalNumberAppearInRange;
                        }

                        //Get all publih Date               
                        foreach (var date in i.AllDatePublishList)
                        {
                            if (!hitNumberByDate.ContainsKey(date))
                            {
                                hitNumberByDate.Add(date, new SortedSet<string>());
                            }
                        }
                    }

                    //Prepare data for Color timeline
                    foreach (var item in hitNumberByDate)
                    {
                        foreach (var aNo in m.OriginalData)
                            foreach (var date in aNo.DatePublishList.DatePublishList1)
                                if (item.Key == date)
                                    item.Value.Add(aNo.LotNumber);
                    }

                    //Count number appear
                    List<int> temp = new List<int>();
                    foreach (var i in hitNumberByDate.Keys)
                    {
                        foreach (var j in hitNumberByDate[i])
                        {
                            temp.Add(Convert.ToInt32(j));
                        }
                    }
                    SortedDictionary<DateTime, SortedSet<string>> hitNumberByDateDB =
                       new SortedDictionary<DateTime, SortedSet<string>>();
                    foreach (var i in hitNumberByDate.Keys)
                    {
                        SortedSet<string> k = new SortedSet<string>();
                        foreach (var j in hitNumberByDate[i])
                        {
                            k.Add(j + " (" + temp.Count(x => x == Convert.ToInt32(j)) + ")");
                        }
                        if (k.Count == 2)
                            hitNumberByDateDB.Add(i, k);
                    }

                    m.groupNumberStatistic3DMax = hitNumberByDateDB;
                    m.NoAppear1To7 = count1_7;
                    m.NoAppear8To15 = count8_15;
                    m.NoAppear16To23 = count16_23;
                    m.NoAppear24To31 = count24_31;
                    m.NoAppear32To39 = count32_39;
                    m.NoAppear40To47 = count40_47;
                    m.NoAppear48To55 = count48_55;
                    m.NoAppear56To63 = count56_63;
                    m.NoAppear64To71 = count64_71;
                    m.NoAppear72To80 = count72_80;
                    #endregion

                    count1_7 = count8_15 = count16_23 = count24_31 =
                        count32_39 = count40_47 = count48_55 =
                        count56_63 = count64_71 = count72_80 = 0;

                    #region GiaiNhat
                    hitNumberByDate = new SortedDictionary<DateTime, SortedSet<string>>();
                    foreach (var i in m.GiaiNhat)
                    {
                        if (Convert.ToInt32(i.LotNumber) >= 1 && Convert.ToInt32(i.LotNumber) <= 100)
                        {
                            count1_7 += i.TotalNumberAppearInRange;
                        }
                        else
                            if (Convert.ToInt32(i.LotNumber) >= 101 && Convert.ToInt32(i.LotNumber) <= 200)
                        {
                            count8_15 += i.TotalNumberAppearInRange;
                        }
                        else
                            if (Convert.ToInt32(i.LotNumber) >= 201 && Convert.ToInt32(i.LotNumber) <= 300)
                        {
                            count16_23 += i.TotalNumberAppearInRange;
                        }
                        else
                            if (Convert.ToInt32(i.LotNumber) >= 301 && Convert.ToInt32(i.LotNumber) <= 400)
                        {
                            count24_31 += i.TotalNumberAppearInRange;
                        }
                        else
                            if (Convert.ToInt32(i.LotNumber) >= 401 && Convert.ToInt32(i.LotNumber) <= 500)
                        {
                            count32_39 += i.TotalNumberAppearInRange;
                        }
                        else
                            if (Convert.ToInt32(i.LotNumber) >= 501 && Convert.ToInt32(i.LotNumber) <= 600)
                        {
                            count40_47 += i.TotalNumberAppearInRange;
                        }
                        else if (Convert.ToInt32(i.LotNumber) >= 601 && Convert.ToInt32(i.LotNumber) <= 700)
                        {
                            count48_55 += i.TotalNumberAppearInRange;
                        }
                        else if (Convert.ToInt32(i.LotNumber) >= 701 && Convert.ToInt32(i.LotNumber) <= 800)
                        {
                            count56_63 += i.TotalNumberAppearInRange;
                        }
                        else if (Convert.ToInt32(i.LotNumber) >= 801 && Convert.ToInt32(i.LotNumber) <= 900)
                        {
                            count64_71 += i.TotalNumberAppearInRange;
                        }
                        else if (Convert.ToInt32(i.LotNumber) >= 901 && Convert.ToInt32(i.LotNumber) <= 999)
                        {
                            count72_80 += i.TotalNumberAppearInRange;
                        }

                        //Get all publih Date               
                        foreach (var date in i.AllDatePublishList)
                        {
                            if (!hitNumberByDate.ContainsKey(date))
                            {
                                hitNumberByDate.Add(date, new SortedSet<string>());
                            }
                        }
                    }

                    //Prepare data for Color timeline
                    foreach (var item in hitNumberByDate)
                    {
                        foreach (var aNo in m.GiaiNhat)
                            foreach (var date in aNo.DatePublishList.DatePublishList1)
                                if (item.Key == date)
                                    item.Value.Add(aNo.LotNumber);
                    }

                    //Count number appear
                    temp = new List<int>();
                    foreach (var i in hitNumberByDate.Keys)
                    {
                        foreach (var j in hitNumberByDate[i])
                        {
                            temp.Add(Convert.ToInt32(j));
                        }
                    }

                    SortedDictionary<DateTime, SortedSet<string>> hitNumberByDateG1 =
                       new SortedDictionary<DateTime, SortedSet<string>>();
                    foreach (var i in hitNumberByDate.Keys)
                    {
                        SortedSet<string> k = new SortedSet<string>();
                        foreach (var j in hitNumberByDate[i])
                        {
                            k.Add(j + " (" + temp.Count(x => x == Convert.ToInt32(j)) + ")");
                        }
                        if (k.Count == 4)
                            hitNumberByDateG1.Add(i, k);
                    }

                    m.groupNumberStatistic3DMaxGiaiNhat = hitNumberByDateG1;
                    m.NoAppear1To7G1 = count1_7;
                    m.NoAppear8To15G1 = count8_15;
                    m.NoAppear16To23G1 = count16_23;
                    m.NoAppear24To31G1 = count24_31;
                    m.NoAppear32To39G1 = count32_39;
                    m.NoAppear40To47G1 = count40_47;
                    m.NoAppear48To55G1 = count48_55;
                    m.NoAppear56To63G1 = count56_63;
                    m.NoAppear64To71G1 = count64_71;
                    m.NoAppear72To80G1 = count72_80;
                    #endregion                   

                    count1_7 = count8_15 = count16_23 = count24_31 =
                        count32_39 = count40_47 = count48_55 =
                        count56_63 = count64_71 = count72_80 = 0;

                    #region GiaiNhi
                    hitNumberByDate = new SortedDictionary<DateTime, SortedSet<string>>();
                    foreach (var i in m.GiaiNhi)
                    {
                        if (Convert.ToInt32(i.LotNumber) >= 1 && Convert.ToInt32(i.LotNumber) <= 100)
                        {
                            count1_7 += i.TotalNumberAppearInRange;
                        }
                        else
                            if (Convert.ToInt32(i.LotNumber) >= 101 && Convert.ToInt32(i.LotNumber) <= 200)
                        {
                            count8_15 += i.TotalNumberAppearInRange;
                        }
                        else
                            if (Convert.ToInt32(i.LotNumber) >= 201 && Convert.ToInt32(i.LotNumber) <= 300)
                        {
                            count16_23 += i.TotalNumberAppearInRange;
                        }
                        else
                            if (Convert.ToInt32(i.LotNumber) >= 301 && Convert.ToInt32(i.LotNumber) <= 400)
                        {
                            count24_31 += i.TotalNumberAppearInRange;
                        }
                        else
                            if (Convert.ToInt32(i.LotNumber) >= 401 && Convert.ToInt32(i.LotNumber) <= 500)
                        {
                            count32_39 += i.TotalNumberAppearInRange;
                        }
                        else
                            if (Convert.ToInt32(i.LotNumber) >= 501 && Convert.ToInt32(i.LotNumber) <= 600)
                        {
                            count40_47 += i.TotalNumberAppearInRange;
                        }
                        else if (Convert.ToInt32(i.LotNumber) >= 601 && Convert.ToInt32(i.LotNumber) <= 700)
                        {
                            count48_55 += i.TotalNumberAppearInRange;
                        }
                        else if (Convert.ToInt32(i.LotNumber) >= 701 && Convert.ToInt32(i.LotNumber) <= 800)
                        {
                            count56_63 += i.TotalNumberAppearInRange;
                        }
                        else if (Convert.ToInt32(i.LotNumber) >= 801 && Convert.ToInt32(i.LotNumber) <= 900)
                        {
                            count64_71 += i.TotalNumberAppearInRange;
                        }
                        else if (Convert.ToInt32(i.LotNumber) >= 901 && Convert.ToInt32(i.LotNumber) <= 999)
                        {
                            count72_80 += i.TotalNumberAppearInRange;
                        }

                        //Get all publih Date               
                        foreach (var date in i.AllDatePublishList)
                        {
                            if (!hitNumberByDate.ContainsKey(date))
                            {
                                hitNumberByDate.Add(date, new SortedSet<string>());
                            }
                        }
                    }

                    //Prepare data for Color timeline
                    foreach (var item in hitNumberByDate)
                    {
                        foreach (var aNo in m.GiaiNhi)
                            foreach (var date in aNo.DatePublishList.DatePublishList1)
                                if (item.Key == date)
                                    item.Value.Add(aNo.LotNumber);
                    }

                    //Count number appear
                    temp = new List<int>();
                    foreach (var i in hitNumberByDate.Keys)
                    {
                        foreach (var j in hitNumberByDate[i])
                        {
                            temp.Add(Convert.ToInt32(j));
                        }
                    }

                    SortedDictionary<DateTime, SortedSet<string>> hitNumberByDateG2 =
                       new SortedDictionary<DateTime, SortedSet<string>>();
                    foreach (var i in hitNumberByDate.Keys)
                    {
                        SortedSet<string> k = new SortedSet<string>();
                        foreach (var j in hitNumberByDate[i])
                        {
                            k.Add(j + " (" + temp.Count(x => x == Convert.ToInt32(j)) + ")");
                        }
                        if (k.Count == 6)
                            hitNumberByDateG2.Add(i, k);
                    }

                    m.groupNumberStatistic3DMaxGiaiNhi = hitNumberByDateG2;
                    m.NoAppear1To7G2 = count1_7;
                    m.NoAppear8To15G2 = count8_15;
                    m.NoAppear16To23G2 = count16_23;
                    m.NoAppear24To31G2 = count24_31;
                    m.NoAppear32To39G2 = count32_39;
                    m.NoAppear40To47G2 = count40_47;
                    m.NoAppear48To55G2 = count48_55;
                    m.NoAppear56To63G2 = count56_63;
                    m.NoAppear64To71G2 = count64_71;
                    m.NoAppear72To80G2 = count72_80;
                    #endregion                   

                    count1_7 = count8_15 = count16_23 = count24_31 =
                        count32_39 = count40_47 = count48_55 =
                        count56_63 = count64_71 = count72_80 = 0;

                    #region GiaiBa
                    hitNumberByDate = new SortedDictionary<DateTime, SortedSet<string>>();
                    foreach (var i in m.GiaiBa)
                    {
                        if (Convert.ToInt32(i.LotNumber) >= 1 && Convert.ToInt32(i.LotNumber) <= 100)
                        {
                            count1_7 += i.TotalNumberAppearInRange;
                        }
                        else
                            if (Convert.ToInt32(i.LotNumber) >= 101 && Convert.ToInt32(i.LotNumber) <= 200)
                        {
                            count8_15 += i.TotalNumberAppearInRange;
                        }
                        else
                            if (Convert.ToInt32(i.LotNumber) >= 201 && Convert.ToInt32(i.LotNumber) <= 300)
                        {
                            count16_23 += i.TotalNumberAppearInRange;
                        }
                        else
                            if (Convert.ToInt32(i.LotNumber) >= 301 && Convert.ToInt32(i.LotNumber) <= 400)
                        {
                            count24_31 += i.TotalNumberAppearInRange;
                        }
                        else
                            if (Convert.ToInt32(i.LotNumber) >= 401 && Convert.ToInt32(i.LotNumber) <= 500)
                        {
                            count32_39 += i.TotalNumberAppearInRange;
                        }
                        else
                            if (Convert.ToInt32(i.LotNumber) >= 501 && Convert.ToInt32(i.LotNumber) <= 600)
                        {
                            count40_47 += i.TotalNumberAppearInRange;
                        }
                        else if (Convert.ToInt32(i.LotNumber) >= 601 && Convert.ToInt32(i.LotNumber) <= 700)
                        {
                            count48_55 += i.TotalNumberAppearInRange;
                        }
                        else if (Convert.ToInt32(i.LotNumber) >= 701 && Convert.ToInt32(i.LotNumber) <= 800)
                        {
                            count56_63 += i.TotalNumberAppearInRange;
                        }
                        else if (Convert.ToInt32(i.LotNumber) >= 801 && Convert.ToInt32(i.LotNumber) <= 900)
                        {
                            count64_71 += i.TotalNumberAppearInRange;
                        }
                        else if (Convert.ToInt32(i.LotNumber) >= 901 && Convert.ToInt32(i.LotNumber) <= 999)
                        {
                            count72_80 += i.TotalNumberAppearInRange;
                        }

                        //Get all publih Date               
                        foreach (var date in i.AllDatePublishList)
                        {
                            if (!hitNumberByDate.ContainsKey(date))
                            {
                                hitNumberByDate.Add(date, new SortedSet<string>());
                            }
                        }
                    }

                    //Prepare data for Color timeline
                    foreach (var item in hitNumberByDate)
                    {
                        foreach (var aNo in m.GiaiBa)
                            foreach (var date in aNo.DatePublishList.DatePublishList1)
                                if (item.Key == date)
                                    item.Value.Add(aNo.LotNumber);
                    }

                    //Count number appear
                    temp = new List<int>();
                    foreach (var i in hitNumberByDate.Keys)
                    {
                        foreach (var j in hitNumberByDate[i])
                        {
                            temp.Add(Convert.ToInt32(j));
                        }
                    }

                    SortedDictionary<DateTime, SortedSet<string>> hitNumberByDateG3 =
                       new SortedDictionary<DateTime, SortedSet<string>>();
                    foreach (var i in hitNumberByDate.Keys)
                    {
                        SortedSet<string> k = new SortedSet<string>();
                        foreach (var j in hitNumberByDate[i])
                        {
                            k.Add(j + " (" + temp.Count(x => x == Convert.ToInt32(j)) + ")");
                        }
                        if (k.Count == 8)
                            hitNumberByDateG3.Add(i, k);
                    }

                    m.groupNumberStatistic3DMaxGiaiBa = hitNumberByDateG3;
                    m.NoAppear1To7G3 = count1_7;
                    m.NoAppear8To15G3 = count8_15;
                    m.NoAppear16To23G3 = count16_23;
                    m.NoAppear24To31G3 = count24_31;
                    m.NoAppear32To39G3 = count32_39;
                    m.NoAppear40To47G3 = count40_47;
                    m.NoAppear48To55G3 = count48_55;
                    m.NoAppear56To63G3 = count56_63;
                    m.NoAppear64To71G3 = count64_71;
                    m.NoAppear72To80G3 = count72_80;
                    #endregion                   
                }
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

                m.IsGet6Over45 = true;
                m.IsGet6Over55 = true;
                m.IsGet3DMax = true;
                m.IsGet3DMaxPro = true;
                ModelState.Clear();
            }
            catch (Exception e)
            {
                m.ErrorMessage = e.Message;
            }
         
            return View("Data", m);
        }

        private ActionResult SubmitKeno(DataPageModel m)
        {
            string val;
            try
            {
                val = DataVietlott._Keno.Insert(DataVietlott.Common.ReadAppConfig("KenoURL"));
                m.Status = val + "\n";

                m.ErrorMessage = "";
                ModelState.Clear();
            }
            catch (Exception e)
            {
                m.ErrorMessage = e.Message;
            }

            return View("Keno", m);
        }

        [HttpGet]
        public ActionResult Keno(DataPageModel m)
        {
            return SubmitKeno(m);
        }

        [HttpPost]
        public ActionResult KenoSubmit(DataPageModel m)
        {
            return SubmitKeno(m);
        }

        public ActionResult Admin()
        {            
            return View();
        }

        private StringBuilder FullChuKyDisplay645(string no, string loaive)
        {
            StringBuilder sb = new StringBuilder();

            var data = _6Over45TimeLine.Get6Over45Number(no, DateTime.MinValue, DateTime.MaxValue).Select(x => new LotteryStatistic1
            {
                LotNumber = Convert.ToInt32(x.LotNumber),
                AllDatePublishList = x.AllDatePublishList,
                DatePublish = x.DatePublish,
                TotalNumberAppearInRange = x.TotalNumberAppearInRange,
                DatePublishList = x.DatePublishList
            }).OrderBy(x => x.LotNumber).ToList();

            List<DateTime> allDatePublist = new List<DateTime>();
            allDatePublist = data[0].AllDatePublishList;

            sb.Append("<div class='container-fluid'>");
            sb.Append("<div class='col-12'><h6>6Over45, CHU KỲ XUẤT HIỆN ĐẦY ĐỦ CỦA SỐ: " + no + "</h6>");
            sb.Append("<table class='table table-secondary table-striped table-bordered TableData'>" +
                "<thead class='table-dark'><tr class='text-info'><td>Xuất hiện</td><td>Ngày</td></tr></thead><tbody>");
            sb.Append("<tr>");
            foreach (var i in data)
            {
                sb.Append("<td class='align-middle'><h6>");
                sb.Append(i.TotalNumberAppearInRange);
                sb.Append("</h6></td>");
                foreach (var j in allDatePublist.OrderByDescending(x => x.Date))
                {
                    sb.Append("<td class='align-middle'>");
                    foreach (var k in i.DatePublishList.DatePublishList1.OrderByDescending(x => x.Date))
                    {
                        if (j.Date == k.Date)
                        {
                            sb.Append("<div class='MauNenSoTrung font-italic fs-5 text-secondary align-items-lg-center align-middle text-center'>");
                            sb.Append("<p class='SoTrungFontSize'>");
                            sb.Append(string.Format("{0:d/M}", k.Date));
                            sb.Append("</p>");
                            sb.Append("</div>");
                        }
                    }
                    sb.Append("</td>");
                }
            }
            sb.Append("</tr>");
            sb.Append("<tr>");
            sb.Append("<td>Chu kỳ (ngày)</td>");
            foreach (var i in data)
            {
                var t = i.DatePublishList.DatePublishList1.OrderByDescending(x => x.Date).ToList();
                for (int j = 0; j < t.Count() - 1; j++)
                {
                    sb.Append("<td class='align-middle'>");
                    sb.Append("<div class='font-italic fs-6 text-secondary align-items-lg-center align-middle text-center'>");
                    sb.Append(string.Format("{0}", (t[j].Date - t[j + 1].Date).TotalDays));
                    sb.Append("</div>");
                    sb.Append("</td>");
                }
            }
            sb.Append("</tr>");
            sb.Append("</body>");
            sb.Append("</div>");
            sb.Append("</div>");

            return sb;
        }

        private StringBuilder FullChuKyDisplay655(string no, string loaive)
        {
            StringBuilder sb = new StringBuilder();

            var data = _6Over55TimeLine.Get6Over55Number(no, DateTime.MinValue, DateTime.MaxValue).Select(x => new LotteryStatistic1
            {
                LotNumber = Convert.ToInt32(x.LotNumber),
                AllDatePublishList = x.AllDatePublishList,
                DatePublish = x.DatePublish,
                TotalNumberAppearInRange = x.TotalNumberAppearInRange,
                DatePublishList = x.DatePublishList
            }).OrderBy(x => x.LotNumber).ToList();

            List<DateTime> allDatePublist = new List<DateTime>();
            allDatePublist = data[0].AllDatePublishList;

            sb.Append("<div class='container-fluid'>");
            sb.Append("<div class='col-12'><h6>6Over55, CHU KỲ XUẤT HIỆN ĐẦY ĐỦ CỦA SỐ: " + no + "</h6>");
            sb.Append("<table class='table table-secondary table-striped table-bordered TableData'>" +
                "<thead class='table-dark'><tr class='text-info'><td>Xuất hiện</td><td>Ngày</td></tr></thead><tbody>");
            sb.Append("<tr>");
            foreach (var i in data)
            {
                sb.Append("<td class='align-middle'><h6>");
                sb.Append(i.TotalNumberAppearInRange);
                sb.Append("</h6></td>");
                foreach (var j in allDatePublist.OrderByDescending(x => x.Date))
                {
                    sb.Append("<td class='align-middle'>");
                    foreach (var k in i.DatePublishList.DatePublishList1.OrderByDescending(x => x.Date))
                    {
                        if (j.Date == k.Date)
                        {
                            sb.Append("<div class='MauNenSoTrung font-italic fs-5 text-secondary align-items-lg-center align-middle text-center'>");
                            sb.Append("<p class='SoTrungFontSize'>");
                            sb.Append(string.Format("{0:d/M}", k.Date));
                            sb.Append("</p>");
                            sb.Append("</div>");
                        }
                    }
                    sb.Append("</td>");
                }
            }
            sb.Append("</tr>");
            sb.Append("<tr>");
            sb.Append("<td>Chu kỳ (ngày)</td>");
            foreach (var i in data)
            {
                var t = i.DatePublishList.DatePublishList1.OrderByDescending(x => x.Date).ToList();
                for (int j = 0; j < t.Count() - 1; j++)
                {
                    sb.Append("<td class='align-middle'>");
                    sb.Append("<div class='font-italic fs-6 text-secondary align-items-lg-center align-middle text-center'>");
                    sb.Append(string.Format("{0}", (t[j].Date - t[j + 1].Date).TotalDays));
                    sb.Append("</div>");
                    sb.Append("</td>");
                }
            }
            sb.Append("</tr>");
            sb.Append("</body>");
            sb.Append("</div>");
            sb.Append("</div>");

            return sb;
        }

        private StringBuilder FullChuKyDisplayKeno(string no, string loaive)
        {
            StringBuilder sb = new StringBuilder();
            var numbers = _KenoTimeLine.GetKenoNumber(no, DateTime.MinValue, DateTime.MaxValue);
            var _KenoNoData = _KenoTimeLine.GetKenoNumberStatistic(numbers);
            var data = _KenoNoData.Select(x => new LotteryStatistic1
            {
                LotNumber = Convert.ToInt32(x.LotNumber),
                AllDatePublishList = x.AllDatePublishList,
                DatePublish = x.DatePublish,
                TotalNumberAppearInRange = x.TotalNumberAppearInRange,
                DatePublishList = x.DatePublishList,
            }).OrderBy(x => Convert.ToInt32(x.LotNumber)).ToList();

            List<DateTime> allDatePublist = new List<DateTime>();
            allDatePublist = data[0].AllDatePublishList;

            sb.Append("<div class='container-fluid'>");
            sb.Append("<div class='col-12'><h6>Keno, CHU KỲ XUẤT HIỆN ĐẦY ĐỦ CỦA SỐ: " + no + "</h6>");
            sb.Append("<table class='table table-secondary table-striped table-bordered TableData'>" +
                "<thead class='table-dark'><tr class='text-info'><td>Xuất hiện</td><td>Ngày</td></tr></thead><tbody>");
            sb.Append("<tr>");
            foreach (var i in data)
            {
                sb.Append("<td class='align-middle'><h6>");
                sb.Append(i.TotalNumberAppearInRange);
                sb.Append("</h6></td>");
                foreach (var j in allDatePublist.OrderByDescending(x => x.Date))
                {
                    sb.Append("<td class='align-middle'>");
                    foreach (var k in i.DatePublishList.DatePublishList1.OrderByDescending(x => x.Date))
                    {
                        if (j.Date == k.Date)
                        {
                            sb.Append("<div class='MauNenSoTrung font-italic fs-5 text-secondary align-items-lg-center align-middle text-center'>");
                            sb.Append("<p class='SoTrungFontSize'>");
                            sb.Append(string.Format("{0:d/M}", k.Date));
                            sb.Append("</p>");
                            sb.Append("</div>");
                        }
                    }
                    sb.Append("</td>");
                }
            }
            sb.Append("</tr>");
            sb.Append("<tr>");
            sb.Append("<td>Chu kỳ (ngày)</td>");
            foreach (var i in data)
            {
                var t = i.DatePublishList.DatePublishList1.OrderByDescending(x => x.Date).ToList();
                for (int j = 0; j < t.Count() - 1; j++)
                {
                    sb.Append("<td class='align-middle'>");
                    sb.Append("<div class='font-italic fs-6 text-secondary align-items-lg-center align-middle text-center'>");
                    sb.Append(string.Format("{0}", (t[j].Date - t[j + 1].Date).TotalDays));
                    sb.Append("</div>");
                    sb.Append("</td>");
                }
            }
            sb.Append("</tr>");
            sb.Append("</body>");
            sb.Append("</div>");
            sb.Append("</div>");

            return sb;
        }

        [HttpGet]
        public ActionResult DuDoan(string no, string loaive)
        {
            StringBuilder sb = new StringBuilder();
            switch (Convert.ToInt32(loaive))
            {
                case 1://6/45
                    sb = FullChuKyDisplay645(no, loaive);
                    break;
                case 3://6/55
                    sb = FullChuKyDisplay655(no, loaive);
                    break;
                case 5: //Keno
                    sb = FullChuKyDisplayKeno(no, loaive);
                    break;
                default:
                    break;
            }

            return Json(sb.ToString(), JsonRequestBehavior.AllowGet);
        }
    }
}
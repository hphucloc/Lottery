using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebAppLottery.Models
{
    public class IndexPageModel
    {
        public enum LoaiVe
        {
            [Description("6/45")]
            _6Over45 = 1,
            [Description("6/55")]
            _6Over55 = 3,
            [Description("Keno")]
            _Keno = 5,
            [Description("3DMAX")]
            _3DMax = 2,
        }

        //request header       
        public LoaiVe ListLoaiVe { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:d/M/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime From { get; set; }
        public string HiddenFrom { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:d/M/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime To { get; set; }
        public string HiddenTo { get; set; }
        public string ErrorMessage { get; set; }

        //numberGroupStatistic
        public int? NoAppear1To7 { get; set; }
        public int? NoAppear8To15 { get; set; }
        public int? NoAppear16To23 { get; set; }
        public int? NoAppear24To31 { get; set; }
        public int? NoAppear32To39 { get; set; }
        public int? NoAppear40To47 { get; set; }
        public int? NoAppear48To55 { get; set; }
        public int? NoAppear56To63 { get; set; }
        public int? NoAppear64To71 { get; set; }
        public int? NoAppear72To80 { get; set; }
        public SortedDictionary<DateTime, SortedSet<int>> groupNumberStatistic { get; set; }
        public SortedDictionary<int?, SortedSet<int>> KyQuays { get; set; }
        public SortedDictionary<int?, string[]> ChanleLonNhos { get; set; }
        //Render body
        public List<WebAppLottery.Models.LotteryStatistic1> Data { get; set; }

        //3DMAX
        public SortedDictionary<DateTime, SortedSet<string>> groupNumberStatistic3DMax { get; set; }
        public List<LotteryBusiness.LoterryStatistic> OriginalData { get; set; }
        public int? NoAppear1To7G1 { get; set; }
        public int? NoAppear8To15G1 { get; set; }
        public int? NoAppear16To23G1 { get; set; }
        public int? NoAppear24To31G1 { get; set; }
        public int? NoAppear32To39G1 { get; set; }
        public int? NoAppear40To47G1 { get; set; }
        public int? NoAppear48To55G1 { get; set; }
        public int? NoAppear56To63G1 { get; set; }
        public int? NoAppear64To71G1 { get; set; }
        public int? NoAppear72To80G1 { get; set; }
        public List<LotteryBusiness.LoterryStatistic> GiaiNhat { get; set; }
        public SortedDictionary<DateTime, SortedSet<string>> groupNumberStatistic3DMaxGiaiNhat { get; set; }
        public int? NoAppear1To7G2 { get; set; }
        public int? NoAppear8To15G2 { get; set; }
        public int? NoAppear16To23G2 { get; set; }
        public int? NoAppear24To31G2 { get; set; }
        public int? NoAppear32To39G2 { get; set; }
        public int? NoAppear40To47G2 { get; set; }
        public int? NoAppear48To55G2 { get; set; }
        public int? NoAppear56To63G2 { get; set; }
        public int? NoAppear64To71G2 { get; set; }
        public int? NoAppear72To80G2 { get; set; }
        public List<LotteryBusiness.LoterryStatistic> GiaiNhi { get; set; }
        public SortedDictionary<DateTime, SortedSet<string>> groupNumberStatistic3DMaxGiaiNhi { get; set; }
        public int? NoAppear1To7G3 { get; set; }
        public int? NoAppear8To15G3 { get; set; }
        public int? NoAppear16To23G3 { get; set; }
        public int? NoAppear24To31G3 { get; set; }
        public int? NoAppear32To39G3 { get; set; }
        public int? NoAppear40To47G3 { get; set; }
        public int? NoAppear48To55G3 { get; set; }
        public int? NoAppear56To63G3 { get; set; }
        public int? NoAppear64To71G3 { get; set; }
        public int? NoAppear72To80G3 { get; set; }
        public List<LotteryBusiness.LoterryStatistic> GiaiBa { get; set; }
        public SortedDictionary<DateTime, SortedSet<string>> groupNumberStatistic3DMaxGiaiBa { get; set; }

        public List<DateTime> AllDatePublist { get; set; }        

        public int? HoaChanLeNo { get; set; }
        public int? ChanNo { get; set; }
        public int? LeNo { get; set; }
        public int? HoaLonNhoNo { get; set; }
        public int? LonNo { get; set; }
        public int? NhoNo { get; set; }
    }
}
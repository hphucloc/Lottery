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
        public SortedDictionary<DateTime, SortedSet<int>> groupNumberStatistic { get; set; }

        //Render body
        public List<WebAppLottery.Models.LotteryStatistic1> Data { get; set; }      

        public List<DateTime> AllDatePublist { get; set; }
    }
}
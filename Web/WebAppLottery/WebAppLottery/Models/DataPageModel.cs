using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebAppLottery.Models
{

    public class DataPageModel
    {
        public enum LoaiVe
        {
            [Description("6/45")]
            _6Over45 = 1,
            [Description("6/55")]
            _6Over55 = 3,
            //[Description("Keno")]
            //_Keno = 5,
            //[Description("3DMAX")]
            //_3DMax = 2,
            //[Description("3DMAXPro")]
            //_3DMaxPro = 4,
        }
        public string Status { get; set; }
        public string ErrorMessage { get; set; }
        public bool IsGet6Over45 { get; set; }
        public bool IsGet6Over55 { get; set; }
        public bool IsGet3DMax { get; set; }
        public bool IsGet3DMaxPro { get; set; }
        public bool IsGetKeno { get; set; }


        //For INsert 
        public LoaiVe ListLoaiVe { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:d/M/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime? PublishDate { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:d/M/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime? CreatedDate { get; set; }
        public string No1 { get; set; }
        public string No2 { get; set; }
        public string No3 { get; set; }
        public string No4 { get; set; }
        public string No5 { get; set; }
        public string No6 { get; set; }
        public string No7 { get; set; }
        public string PasswordAddDataManualy { get; set; }
        public string AddMultipleDataManualy { get; set; }
    }
}
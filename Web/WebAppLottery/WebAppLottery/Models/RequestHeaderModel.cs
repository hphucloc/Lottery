using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebAppLottery.Models
{    
    public class RequestHeaderModel
    {       
        public enum LoaiVe
        {
            [Description("6/45")]
            _6Over45 = 1,
            [Description("6/55")]
            _6Over55 = 3,
        }
        [Range(1, int.MaxValue, ErrorMessage = "Chọn loại vé")]
        public LoaiVe ListLoaiVe { get; set; }
       
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime From { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime To { get; set; }      
    }
}
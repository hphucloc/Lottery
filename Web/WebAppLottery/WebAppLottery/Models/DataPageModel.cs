using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebAppLottery.Models
{
    public class DataPageModel
    {
        public string Status { get; set; }
        public string ErrorMessage { get; set; }
        public bool IsGet6Over45 { get; set; }
        public bool IsGet6Over55 { get; set; }
        public bool IsGet3DMax { get; set; }
        public bool IsGet3DMaxPro { get; set; }
        public bool IsGetKeno { get; set; }
    }
}
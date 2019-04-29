using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KLTN_QLKHACHHANG.Models
{
    public class SAN_PHAM
    {
        public int id { get; set; }
        public string tensp { get; set; }
        public int dongia { get; set; }
        public string donvitinh { get; set; }
        public int giamgia { get; set; }
        public string tenloai { get; set; }
        public string ncc { get; set; }
    }
}
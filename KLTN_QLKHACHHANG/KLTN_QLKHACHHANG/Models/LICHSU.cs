using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KLTN_QLKHACHHANG.Models
{
    public class LICHSU
    {
        public int id { get; set; }
        public int idhoadon { get; set; }
        public string idkh { get; set; }
        public int idnv { get; set; }
        public double tongtien { get; set; }
        public double tiengiam { get; set; }
        public double tienkhachtra { get; set; }
        public string ngaymua { get; set; }
        public List<ChiTiet> listchitiet { get; set; }
    }
}
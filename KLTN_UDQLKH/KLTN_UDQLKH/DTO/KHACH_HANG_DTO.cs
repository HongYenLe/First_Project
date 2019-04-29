using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KLTN_UDQLKH.DTO
{
    class KHACH_HANG_DTO
    {
        public int id { get; set; }
        public string tenkh { get; set; }
        public string gioitinh { get; set; }
        public string ngaysinh { get; set; }
        public string cmnd { get; set; }
        public string diachi { get; set; }
        public string dienthoai { get; set; }
        //public string email { get; set; }
        public int diemtichluy { get; set; }
        public int diemconlai { get; set; }
        public int diemthuong { get; set; }
        public int diemnamtruoc { get; set;}
        public int idloaikh { get; set; }
        public int idquyen { get; set; }
        public string tenloai { get; set; }
        public string tenquyen { get; set; }
        public string tendn { get; set; }
        public string matkhau { get; set; }
    }
}

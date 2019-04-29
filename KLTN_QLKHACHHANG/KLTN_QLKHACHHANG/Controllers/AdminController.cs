using KLTN_QLKHACHHANG.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Xml;
using System.Xml.Linq;

namespace KLTN_QLKHACHHANG.Controllers
{
    public class AdminController : Controller
    {

        //================= ĐĂNG NHẬP ======================
        public ActionResult DangNhap()
        {
            return View();
        }

        
        // GET: Admin
        public ActionResult ManageCustomer()
        {
            try
            {
                XmlDocument doc = new XmlDocument();
                List<Models.KHACH_HANG> KH = new List<Models.KHACH_HANG>();
            //List<Models.LOAI_KHACH_HANG> loaiKH = new List<Models.LOAI_KHACH_HANG>();
            //doc.Load(Server.MapPath("C:/Users/Administrator/Desktop/KLTN_UDQLKH/KLTN_UDQLKH/Data/KHACHHANG.xml"));
            //int id = int.Parse(Session["id"].ToString());
            
                XDocument testXML = XDocument.Load("C:/Users/Administrator/Desktop/KLTN_UDQLKH/KLTN_UDQLKH/KLTN_UDQLKH/Data/KH_LOAIKH.xml");
                var Node = (from kh in testXML.Descendants("Join")
                            where (int)kh.Element("id") == int.Parse(Session["id"].ToString())
                            select new
                            {
                                id = (int)kh.Element("id"),
                                tenkh = (string)kh.Element("tenkh"),
                                gioitinh = (string)kh.Element("gioitinh"),

                                ngaysinh = (string)kh.Element("ngaysinh"),
                                cmnd = (string)kh.Element("cmnd"),
                                diachi = (string)kh.Element("diachi"),
                                dienthoai = (string)kh.Element("dienthoai"),
                                diemtichluy = (int)kh.Element("diemtichluy"),
                                diemconlai = (int)kh.Element("diemconlai"),
                                diemthuong = (int)kh.Element("diemthuong"),
                                diemnamtruoc = (int)kh.Element("diemnamtruoc"),
                                tenloai = (string)kh.Element("tenloai")
                            }).ToList();
                foreach (var node in Node)
                {
                    //var kh = testXML.Descendants("khachhang").Where(c => c.Attributes("idloaikh").Equals());
                    KHACH_HANG temp = new KHACH_HANG();
                    temp.id = Convert.ToInt32(node.id);
                    temp.tenkh = node.tenkh;
                    temp.gioitinh = node.gioitinh;
                    temp.ngaysinh = node.ngaysinh;
                    temp.cmnd = node.cmnd;
                    temp.diachi = node.diachi;
                    temp.dienthoai = node.dienthoai;
                    temp.diemtichluy = Convert.ToInt32(node.diemtichluy);
                    temp.diemconlai = Convert.ToInt32(node.diemconlai);
                    temp.diemthuong = Convert.ToInt32(node.diemthuong);
                    temp.diemnamtruoc = Convert.ToInt32(node.diemnamtruoc);
                    temp.tenloai = node.tenloai;
                    //temp.tenquyen = node["tenquyen"].InnerText;
                    KH.Add(temp);


                }
                return View(KH);
            }
            catch(Exception ex)
            {
                return RedirectToAction("DangNhap", "Admin");
            }
            

        }

        public ActionResult DoiMatKhau()
        {
            
            return View();
        }
        public ActionResult DMK(string mkcu, string mkmoi)
        {try { 
            XDocument xmlDoc = XDocument.Load("C:/Users/Administrator/Desktop/KLTN_UDQLKH/KLTN_UDQLKH/KLTN_UDQLKH/Data/KHACH_HANG.xml");
            var items = xmlDoc.Descendants("khachhang").ToList().Count;
            string t = Session["id"].ToString();
            XElement selected = xmlDoc.Descendants("khachhang").Where(p => p.Attribute("id").Value.ToString().Equals(t)).FirstOrDefault();
            if (mkcu == Session["mk"].ToString())
            {
                selected.Element("matkhau").Value = mkmoi;

                xmlDoc.Save("C:/Users/Administrator/Desktop/KLTN_UDQLKH/KLTN_UDQLKH/KLTN_UDQLKH/Data/KHACH_HANG.xml");
                return RedirectToAction("ManageCustomer", "Admin");
            }
            
            else
                return RedirectToAction("XemUuDai", "Admin");
            }
            catch (Exception ex)
            {
                return RedirectToAction("DangNhap", "Admin");
            }
        }

        //----------------Lịch sử----------------------------
        public ActionResult ViewHistory()
        {
            try
            {

                XmlDocument doc = new XmlDocument();
                List<Models.LICHSU> LS = new List<Models.LICHSU>();


                XDocument testXML_LS = XDocument.Load("C:/Users/Administrator/Desktop/KLTN_UDQLKH/KLTN_UDQLKH/KLTN_UDQLKH/Data/LICH_SU.xml");
                string test = Session["id"].ToString();
                int n = testXML_LS.Descendants("lichsu").Count();

                var Node = (from ls in testXML_LS.Descendants("lichsu")
                            where (string)ls.Element("idkh") == test
                            select new
                            {
                                id = (int)ls.Attribute("id"),
                                idhoadon = (int)ls.Element("idhoadon"),
                                idkh = (string)ls.Element("idkh"),

                                idnv = (int)ls.Element("idnv"),
                                tongtien = (double)ls.Element("tongtien"),
                                tiengiam = (double)ls.Element("tiengiam"),
                                tienkhachtra = (double)ls.Element("tienkhachtra"),
                                ngaymua = (string)ls.Element("ngaymua")

                            }).ToList();
                foreach (var node in Node)
                {
                    //var kh = testXML.Descendants("khachhang").Where(c => c.Attributes("idloaikh").Equals());
                    LICHSU temp = new LICHSU();
                    temp.id = Convert.ToInt32(node.id);
                    temp.idhoadon = Convert.ToInt32(node.idhoadon);
                    temp.idkh = node.idkh;
                    temp.idnv = Convert.ToInt32(node.idnv);
                    temp.tongtien = Convert.ToDouble(node.tongtien);
                    temp.tiengiam = Convert.ToDouble(node.tiengiam);
                    temp.tienkhachtra = Convert.ToDouble(node.tienkhachtra);
                    temp.ngaymua = node.ngaymua;
                    temp.listchitiet = ChiTietLichSu(temp.idhoadon.ToString());
                    LS.Add(temp);
                }
                return View(LS);
            }
            catch (Exception ex)
            {
                return RedirectToAction("DangNhap", "Admin");
            }
        }

        //=============Xem chi tiết LS====================
        public List<ChiTiet> ChiTietLichSu(string id)
        {
            List<Models.ChiTiet> chitiet = new List<Models.ChiTiet>();
            XDocument xmlDoc = XDocument.Load("C:/Users/Administrator/Desktop/KLTN_UDQLKH/KLTN_UDQLKH/KLTN_UDQLKH/Data/LICH_SU.xml");
            XDocument xmlDocCT = XDocument.Load("C:/Users/Administrator/Desktop/KLTN_UDQLKH/KLTN_UDQLKH/KLTN_UDQLKH/Data/CT_HOADON.xml");
            XDocument xmlDocSP = XDocument.Load("C:/Users/Administrator/Desktop/KLTN_UDQLKH/KLTN_UDQLKH/KLTN_UDQLKH/Data/SAN_PHAM.xml");
            var Node = (from ls in xmlDoc.Descendants("lichsu")
                        join cthd in xmlDocCT.Descendants("chitiet")
                        on (string)ls.Element("idhoadon") equals (string)cthd.Attribute("idhoadon")
                        join sp in xmlDocSP.Descendants("sanpham")
                        on (string)cthd.Attribute("idsp") equals (string)sp.Attribute("id")
                        where (string)ls.Element("idhoadon").Value == id
                        select new
                        {
                            id=(string)cthd.Attribute("id"),
                            tensp = (string)sp.Element("tensp"),
                            dongia = (int)sp.Element("dongia"),
                            soluongmua = (int)cthd.Element("soluongmua")
                        }).ToList();
            foreach(var i in Node)
            {
                ChiTiet ct = new ChiTiet();
                ct.id = i.id;
                ct.tensp = i.tensp;
                ct.dongia = i.dongia;
                ct.soluongmua = i.soluongmua;
                chitiet.Add(ct);
            }
            return chitiet;
        }

        public ActionResult XemUuDai()
        {
            try { 
            XmlDocument doc = new XmlDocument();
            List<Models.QuaTangvaUuDai> QT = new List<Models.QuaTangvaUuDai>();


            XDocument testXML_QT = XDocument.Load("C:/Users/Administrator/Desktop/KLTN_UDQLKH/KLTN_UDQLKH/KLTN_UDQLKH/Data/QUATANG_UUDAI.xml");
            //string test = Session["id"].ToString();
            int n = testXML_QT.Descendants("quatang_uudai").Count();

            var Node = (from qt in testXML_QT.Descendants("quatang_uudai")
                        where (string)qt.Attribute("idkh") == Session["id"].ToString()
                        select new
                        {
                            id = (int)qt.Attribute("id"),
                            tenqt = (string)qt.Element("tenqt"),
                            noidung = (string)qt.Element("noidung"),

                            trangthai = (string)qt.Element("trangthai")
                            

                        }).ToList();
            foreach (var node in Node)
            {
                //var kh = testXML.Descendants("khachhang").Where(c => c.Attributes("idloaikh").Equals());
                QuaTangvaUuDai temp = new QuaTangvaUuDai();
                temp.id = Convert.ToInt32(node.id);
                temp.tenqt = node.tenqt;
                temp.noidung = node.noidung;
                temp.trangthai = node.trangthai;
                QT.Add(temp);
            }
            return View(QT);
            }
            catch (Exception ex)
            {
                return RedirectToAction("DangNhap", "Admin");
            }
        }
        public ActionResult LoaiKhachHang()
        {
            XmlDocument doc = new XmlDocument();
            List<Models.LOAI_KHACH_HANG> loaiKH = new List<Models.LOAI_KHACH_HANG>();
            doc.Load(Server.MapPath("~/Data/LOAIKHACHHANG.xml"));
            int i = 1;
            foreach (XmlNode node in doc.SelectNodes("/LOAIKHACHHANG/loaikh"))
            {
                loaiKH.Add(new Models.LOAI_KHACH_HANG
                {
                    maloai = i,
                    tenloai = node["tenloai"].InnerText
                });
                i = i + 1;
            }
            return View(loaiKH);
        }

        public ActionResult ManageProductKind()
        {
            XmlDocument doc = new XmlDocument();
            List<Models.LOAI_SAN_PHAM> loaiSP = new List<Models.LOAI_SAN_PHAM>();
            doc.Load(Server.MapPath("~/Data/LoaiSanPham.xml"));
            int i = 1;
            foreach (XmlNode node in doc.SelectNodes("/LOAISANPHAM/loaisp"))
            {
                loaiSP.Add(new Models.LOAI_SAN_PHAM
                {
                    maloai = i,
                    tenloai = node["tenloai"].InnerText
                });
                i = i + 1;
            }
            //ViewBag.loaisp=
            return View(loaiSP);
        }

        //---------------Xử lý đối tượng loại khách hàng-----------
        Models.LOAI_KHACH_HANG model = new Models.LOAI_KHACH_HANG();
        public ActionResult AddEditProject(int? id)
        {
            int Id = Convert.ToInt32(id);
            if (Id > 0)
            {
                GetDetailsById(Id);
                model.IsEdit = true;
                return View(model);
            }
            else
            {
                model.IsEdit = false;
                return View(model);
            }
        }
        [HttpPost]

        public ActionResult ThemLoaiKH(string tenloai)
        {
            XmlDocument oXmlDocument = new XmlDocument();
            oXmlDocument.Load(Server.MapPath("~/Data/LOAIKHACHHANG.xml"));
            XmlNodeList nodelist = oXmlDocument.GetElementsByTagName("loaikh");
            var x = oXmlDocument.GetElementsByTagName("id");
            //var count = oXmlDocument.Descendants("loaikh").Count();
            int Max = 0;
            foreach (XmlElement item in x)
            {
                int EId = Convert.ToInt32(item.InnerText.ToString());
                if (EId > Max)
                {
                    Max = EId;
                }
            }
            Max = Max + 1;
            XDocument xmlDoc = XDocument.Load(Server.MapPath("~/Data/LOAIKHACHHANG.xml"));
            xmlDoc.Element("LOAIKHACHHANG").Add(new XElement("loaikh",
                new XAttribute("id", Max),

                new XElement("tenloai", tenloai)
            ));
            xmlDoc.Save(Server.MapPath("~/Data/LOAIKHACHHANG.xml"));
            return RedirectToAction("LoaiKhachHang", "Admin");
        }

        public ActionResult AddEditProject(Models.LOAI_KHACH_HANG mdl)
        {

            if (mdl.maloai > 0)
            {
                XDocument xmlDoc = XDocument.Load(Server.MapPath("~/Data/LOAIKHACHHANG.xml"));
                var items = (from item in xmlDoc.Descendants("loaikh") select item).ToList();
                XElement selected = items.Where(p => p.Attribute("Id").Value == mdl.maloai.ToString()).FirstOrDefault();

                selected.Remove();
                xmlDoc.Save(Server.MapPath("~/Data/LOAIKHACHHANG.xml"));
                xmlDoc.Element("LOAIKHACHHANG").Add(new XElement("loaikh",
                    new XElement("id", mdl.maloai),
                    new XElement("tenloai", mdl.tenloai)
                ));
                xmlDoc.Save(Server.MapPath("~/Data/LOAIKHACHHANG.xml"));

                return RedirectToAction("LoaiKhachHang", "Admin");
            }
            else
            {
                XmlDocument oXmlDocument = new XmlDocument();
                oXmlDocument.Load(Server.MapPath("~/Data/LOAIKHACHHANG.xml"));
                XmlNodeList nodelist = oXmlDocument.GetElementsByTagName("loaikh");
                var x = oXmlDocument.GetElementsByTagName("id");
                int Max = 0;
                foreach (XmlElement item in x)
                {
                    int EId = Convert.ToInt32(item.InnerText.ToString());
                    if (EId > Max)
                    {
                        Max = EId;
                    }
                }
                Max = Max + 1;
                XDocument xmlDoc = XDocument.Load(Server.MapPath("~/Data/LOAIKHACHHANG.xml"));
                xmlDoc.Element("LOAIKHACHHANG").Add(new XElement("loaikh",
                    new XElement("id", Max),

                    new XElement("tenloai", mdl.tenloai)
                ));
                xmlDoc.Save(Server.MapPath("~/Data/LOAIKHACHHANG.xml"));
                return RedirectToAction("LoaiKhachHang", "Admin");
            }

        }

        public ActionResult Delete(int Id)
        {
            ViewBag.ErrorMsg = "Error deleting record. " ;
            if (Id > 0)
            {
                XDocument xmlDoc = XDocument.Load(Server.MapPath("~/Data/LOAIKHACHHANG.xml"));
                var items = (from item in xmlDoc.Descendants("loaikh") select item).ToList();
                XElement selected = items.Where(p => p.Attribute("id").Value == Id.ToString()).FirstOrDefault();
                selected.Remove();
                xmlDoc.Save(Server.MapPath("~/Data/LOAIKHACHHANG.xml"));
            }
            return RedirectToAction("LoaiKhachHang", "Admin");
            
        }


        public void GetDetailsById(int Id)
        {
            XDocument oXmlDocument = XDocument.Load(Server.MapPath("~/Data/LOAIKHACHHANG.xml"));
            var items = (from item in oXmlDocument.Descendants("loaikh")
                         where Convert.ToInt32(item.Element("id").Value) == Id
                         select new projectItems
                         {
                             Id = Convert.ToInt32(item.Element("id").Value),

                             Tenloai = item.Element("tenloai").Value
                         }).SingleOrDefault();
            if (items != null)
            {
                model.maloai = items.Id;

                model.tenloai = items.Tenloai;
            }
        }
        public class projectItems
        {
            public int Id
            {
                get;
                set;
            }

            public string Tenloai
            {
                get;
                set;
            }
            public projectItems() { }
        }

        //-----------Xử lý sản phẩm-----------
        public ActionResult ManageProduct()
        {
            XmlDocument doc = new XmlDocument();
            List<Models.SAN_PHAM> sp = new List<Models.SAN_PHAM>();
            doc.Load(Server.MapPath("~/Data/SP_LoaiSP.xml"));
            int i = 1;
            foreach (XmlNode node in doc.SelectNodes("/SP_LOAISP/sp_loaisp"))
            {
                sp.Add(new Models.SAN_PHAM
                {
                    id = i,
                    tensp = node["tensp"].InnerText,
                    dongia = Convert.ToInt32(node["dongia"].InnerText),
                    donvitinh = node["donvitinh"].InnerText,
                    giamgia = Convert.ToInt32(node["giamgia"].InnerText),
                    tenloai = node["tenloai"].InnerText,
                    ncc = node["ncc"].InnerText
                });
                i = i + 1;
            }

            //-------hiển thị loại sản phẩm lên combobox------
            List<Models.LOAI_SAN_PHAM> loaiSP = new List<Models.LOAI_SAN_PHAM>();
            doc.Load(Server.MapPath("~/Data/LoaiSanPham.xml"));
             i = 1;
            foreach (XmlNode node in doc.SelectNodes("/LOAISANPHAM/loaisp"))
            {
                loaiSP.Add(new Models.LOAI_SAN_PHAM
                {
                    maloai = i,
                    tenloai = node["tenloai"].InnerText
                });
                i = i + 1;
            }
            ViewBag.loaisp = loaiSP;

            //--------hiển thị loại sản phẩm lên combobox---------
            
            List<Models.NHA_CUNG_CAP> ncc = new List<Models.NHA_CUNG_CAP>();
            doc.Load(Server.MapPath("~/Data/NhaCungCap.xml"));
            i = 1;
            foreach (XmlNode node in doc.SelectNodes("/NHACUNGCAP/nhacungcap"))
            {
                ncc.Add(new Models.NHA_CUNG_CAP
                {
                    id = i,
                    tenncc = node["tenncc"].InnerText,
                    dienthoai = node["dienthoai"].InnerText,
                    diachi = node["diachi"].InnerText
                });
                i = i + 1;
            }
            ViewBag.ncc = ncc;
            return View(sp);
        }

        //------------Thêm sp------------------
        public ActionResult ThemSanPham(string tensp, int dongia, string donvitinh, int giamgia, string tenloai, string ncc)
        {
            XmlDocument oXmlDocument = new XmlDocument();
            oXmlDocument.Load(Server.MapPath("~/Data/SP_LoaiSP.xml"));
            XmlNodeList nodelist = oXmlDocument.GetElementsByTagName("sp_loaisp");
            var x = oXmlDocument.GetElementsByTagName("id");
            int Max = 0;
            foreach (XmlElement item in x)
            {
                int EId = Convert.ToInt32(item.InnerText.ToString());
                if (EId > Max)
                {
                    Max = EId;
                }
            }
            Max = Max + 1;
            XDocument xmlDoc = XDocument.Load(Server.MapPath("~/Data/SP_LoaiSP.xml"));
            xmlDoc.Element("SP_LOAISP").Add(new XElement("sp_loaisp",
                new XAttribute("id", Max),

                new XElement("tensp", tensp),
                new XElement("dongia", dongia),
                new XElement("donvitinh", donvitinh),
                new XElement("giamgia", giamgia),
                new XElement("tenloai", tenloai),
                new XElement("ncc", ncc)
            ));
            xmlDoc.Save(Server.MapPath("~/Data/SP_LoaiSP.xml"));
            return RedirectToAction("ManageProduct", "Admin");
        }

        public ActionResult XoaSP(int Id)
        {
            ViewBag.ErrorMsg = "Error deleting record. ";
            if (Id > 0)
            {
                XDocument xmlDoc = XDocument.Load(Server.MapPath("~/Data/SP_LoaiSP.xml"));
                var items = (from item in xmlDoc.Descendants("sp_loaisp") select item).ToList();
                XElement selected = items.Where(p => p.Attribute("id").Value == Id.ToString()).FirstOrDefault();
                selected.Remove();
                xmlDoc.Save(Server.MapPath("~/Data/SP_LoaiSP.xml"));
            }

            
            return RedirectToAction("ManageProduct", "Admin");

        }

        public ActionResult Login(string ten, string mk)
        {
            XDocument testXML = XDocument.Load("C:/Users/Administrator/Desktop/KLTN_UDQLKH/KLTN_UDQLKH/KLTN_UDQLKH/Data/KHACH_HANG.xml");
            var node = testXML.Descendants("khachhang").Where(x => x.Element("tendn").Value.Equals(ten.ToString().Trim()) && x.Element("matkhau").Value.Equals(mk.ToString().Trim())).FirstOrDefault();
            if (node != null)
            {
                //if(ten == node.Element("tendn").Value && mk == node.Element("matkhau").Value)
                //{
                Session["id"] = node.Attribute("id").Value;
                Session["ten"] = node.Element("tendn").Value;
                Session["mk"] = node.Element("matkhau").Value;
                return RedirectToAction("ManageCustomer", "Admin");
            }
             else   
           
            return RedirectToAction("DangNhap", "Admin");

        }

        
        public ActionResult Edit(string id,string tenkh,string gioitinh, string ngaysinh,string cmnd,string diachi,string dienthoai)
        {
            XDocument xmlDoc = XDocument.Load("C:/Users/Administrator/Desktop/KLTN_UDQLKH/KLTN_UDQLKH/KLTN_UDQLKH/Data/KH_LoaiKH.xml");
            //var items = (from item in xmlDoc.Descendants("kh_loaikh") select item).ToList();
            var selected = xmlDoc.Descendants("Join").Where(p => p.Element("id").Value == id.ToString()).FirstOrDefault();
            selected.Element("id").Value = id;
            selected.Element("tenkh").Value = tenkh;
            selected.Element("gioitinh").Value = gioitinh;
            selected.Element("ngaysinh").Value = ngaysinh;
            selected.Element("cmnd").Value = cmnd;
            selected.Element("diachi").Value = diachi;
            selected.Element("dienthoai").Value = dienthoai;
            //selected.Remove();
            //xmlDoc.Save("C:/Users/Administrator/Desktop/KLTN_UDQLKH/KLTN_UDQLKH/Data/KH_LoaiKH.xml");
            //xmlDoc.Element("Root").Add(new XElement("Join",
            //    new XElement("id", id),
            //    new XElement("tenkh", tenkh),
            //    new XElement("gioitinh", gioitinh),
            //    new XElement("ngaysinh", ngaysinh),
            //    new XElement("cmnd", cmnd),
            //    new XElement("diachi", diachi),
            //    new XElement("dienthoai", dienthoai)
            //    //new XElement("diemtichluy", diemtichluy),
            //    //new XElement("diemmuahang", diemmuahang),
            //    //new XElement("diemthuong", diemthuong)
            //));
            xmlDoc.Save("C:/Users/Administrator/Desktop/KLTN_UDQLKH/KLTN_UDQLKH/KLTN_UDQLKH/Data/KH_LoaiKH.xml");

            return RedirectToAction("ManageCustomer", "Admin");
            
        }

        //--------------------Nhà cung cấp---------------
        public ActionResult QL_NhaCungCap()
        {
            XmlDocument doc = new XmlDocument();
            List<Models.NHA_CUNG_CAP> ncc = new List<Models.NHA_CUNG_CAP>();
            doc.Load(Server.MapPath("~/Data/NhaCungCap.xml"));
            int i = 1;
            foreach (XmlNode node in doc.SelectNodes("/NHACUNGCAP/nhacungcap"))
            {
                ncc.Add(new Models.NHA_CUNG_CAP
                {
                    id = i,
                    tenncc = node["tenncc"].InnerText,
                    dienthoai = node["dienthoai"].InnerText,
                    diachi = node["diachi"].InnerText
                });
                i = i + 1;
            }
            //ViewBag.loaisp=
            return View(ncc);
        }
    }
}
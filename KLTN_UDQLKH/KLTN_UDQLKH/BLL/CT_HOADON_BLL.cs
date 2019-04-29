 using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KLTN_UDQLKH.DTO;
using System.Xml.Linq;
using System.Data;
using System.Windows.Forms;
using System.Xml;
using System.Globalization;

namespace KLTN_UDQLKH.BLL
{
    class CT_HOADON_BLL
    {
        public void LoadComboBoxKH(ComboBox cbx) //LoadCBX(tên loại khách hàng)
        { 
            try
            {
                XmlDocument doc = new XmlDocument();
                doc.Load("..\\..\\Data\\SAN_PHAM.xml");
                DataSet dataset = new DataSet();
                DataTable dt = new DataTable();
                dataset.ReadXml("..\\..\\Data\\SAN_PHAM.xml");
                dt = dataset.Tables["sanpham"];
                cbx.DataSource = dt;
                cbx.DisplayMember = "tensp";
                cbx.ValueMember = "id";
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
        } 
        public string path = "..\\..\\Data\\CT_HOADON.xml";
        public string path2 = "..\\..\\Data\\HOA_DON.xml";
        public string path3 = "..\\..\\Data\\KHACH_HANG.xml";
        public string path4 = "..\\..\\Data\\LICH_SU.xml";
        public string path5 = "..\\..\\Data\\SAN_PHAM.xml";
        XDocument testXML = XDocument.Load("..\\..\\Data\\CT_HOADON.xml");
        XDocument testXML_SP = XDocument.Load("..\\..\\Data\\SAN_PHAM.xml");
        XDocument testXML_HD = XDocument.Load("..\\..\\Data\\HOA_DON.xml");
        XDocument testXML_KH = XDocument.Load("..\\..\\Data\\KHACH_HANG.xml");
        XDocument testXML_NV = XDocument.Load("..\\..\\Data\\NHAN_VIEN.xml");
        XDocument testXML_LS = XDocument.Load("..\\..\\Data\\LICH_SU.xml");
        public string path6 = "..\\..\\Data\\SP_LOAISP.xml";
        XDocument testXML_SPLOAISP = XDocument.Load("..\\..\\Data\\SP_LOAISP.xml");
        public void LoadDB(int maCanTim, ListView lv)  // tìm theo ID
        {
            try
            {
                lv.Items.Clear();
                DataSet dataSet = new DataSet();
                DataTable dt = new DataTable();
                dataSet.ReadXml("..\\..\\Data\\CT_HOADON.xml");
                dt = dataSet.Tables["chitiet"];
                int co = 0;
                int i = 0;
                foreach (DataRow dr in dt.Rows)
                {
                    if (dr["idhoadon"].ToString() == maCanTim.ToString())
                    {
                        lv.Items.Add(dr["id"].ToString());
                        lv.Items[i].SubItems.Add(dr["idhoadon"].ToString());
                        lv.Items[i].SubItems.Add(dr["idsp"].ToString());
                        lv.Items[i].SubItems.Add(dr["soluongmua"].ToString());
                        co++;
                        i++;
                    }

                }
                if (co == 0)
                {
                    //MessageBox.Show("Không tìm thấy chi tiết mã hóa đơn trên");
                }
                
            }
            catch (Exception e)
            {
                MessageBox.Show("Lỗi: " + e.Message);
            }
        }

        public void ThemHD(CT_HOADON_DTO cthd, ListView lv, string idsp, int sl)  // hàm thêm từ lớp kh sang file xmlthêm chi tiết hóa đơn
        {
            try
            {
                var count = testXML.Descendants("chitiet").Count();
                MessageBox.Show("" + count);
                XElement newStudent = new XElement("chitiet",
                    new XElement("soluongmua", cthd.soluongmua));
                var lastStudent = testXML.Descendants("chitiet").Last();
                newStudent.SetAttributeValue("id", count + 1);
                newStudent.SetAttributeValue("idhoadon", cthd.idhoadon);
                newStudent.SetAttributeValue("idsp", cthd.idsp);
                var Node = (from sp in testXML_SP.Descendants("sanpham")
                            where (string)sp.Attribute("id") == idsp.ToString()
                            select new
                            {
                                soluong = (int)sp.Element("soluong")
                            }).FirstOrDefault();

                if (Node.soluong>0 && sl<=Node.soluong)
                {
                    testXML.Element("CHI_TIET").Add(newStudent);

                    testXML.Save(path);


                    var node = testXML_SP.Descendants("sanpham").Where(x => x.Attribute("id").Value.Equals(idsp)).FirstOrDefault();
                    node.Element("soluong").Value = (int.Parse(node.Element("soluong").Value) - sl).ToString();
                    testXML_SP.Save(path5);

                    var node1 = testXML_SPLOAISP.Descendants("Join").Where(x => x.Element("id").Value.Equals(idsp)).FirstOrDefault();
                    node1.Element("soluong").Value = node.Element("soluong").Value;
                    testXML_SPLOAISP.Save(path6);
                }
                else
                {
                    MessageBox.Show("Hiện tại sản phẩm này còn "+Node.soluong+" sản phẩm!");
                }
            } 
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }

        //-----------------------------Thanh toán k sử dụng PMH----------------------------------------------
        public void ThanhToan(int id,int tientra,double tiengiam,int thanhtoan)
        {
            var Node = (from c in testXML.Descendants("chitiet")
                        join k in testXML_SP.Descendants("sanpham")
                        on (string)c.Attribute("idsp") equals (string)k.Attribute("id")
                        where (string)c.Attribute("idhoadon") == id.ToString()
                        select new
                        {
                            mahoadon = (int)c.Attribute("idhoadon"),
                            masp = (int)c.Attribute("idsp"),
                            sl = (int)c.Element("soluongmua"),
                            gia = (int)k.Element("dongia"),
                            giamgia = (int)k.Element("giamgia")

                        }).ToList();

            //MessageBox.Show("bla bla" + id);
            var hd = testXML_HD.Descendants("hoadon").Where(x => x.Attribute("id").Value.Equals(id.ToString())).FirstOrDefault();
            hd.Element("tongtienmua").Value = thanhtoan.ToString();
            hd.Element("tiengiam").Value = tiengiam.ToString();
            hd.Element("tienkhachtra").Value = tientra.ToString();
            hd.Element("trangthaiTT").Value = "1";
            testXML_HD.Save(path2);
        }

        XDocument testQT = XDocument.Load("..\\..\\Data\\QUATANG_UUDAI.xml");
        string pathQT = "..\\..\\Data\\QUATANG_UUDAI.xml";
        //-----------------------------Thanh toán sử dụng PMH----------------------------------------------
        //public void ThanhToanPMH(string id,string idhd, int tientra, double tiengiam, int thanhtoan)
        //{
        //    var Node = (from qt in testQT.Descendants("quatang_uudai")
        //                //join kh in testXML_KH.Descendants("khachhang")
        //                //on (string)qt.Attribute("idkh") equals (string)kh.Attribute("id")
        //                where (string)qt.Attribute("idkh").Value == id.ToString()
        //                select new
        //                {
        //                    sl = (int)qt.Element("soluong"),
        //                    tien = (int)qt.Element("tientuongung")
        //                }).ToList();
        //    var node1 = testQT.Descendants("quatang_uudai").Where(x => x.Attribute("idkh").Value.Equals(id)).FirstOrDefault();
        //    int tienPMH = 0;
        //    int soluong = 0;
        //    int d = 0;
        //    foreach(var i in Node)
        //    {
        //        tienPMH = tienPMH + (i.sl * i.tien);
        //    }
        //    MessageBox.Show("tien PMH"+tienPMH);
        //    var node = testXML_KH.Descendants("khachhang").Where(x => x.Attribute("id").Value.Equals(id)).FirstOrDefault();
        //    MessageBox.Show(""+node.Attribute("loaikh").Value);
        //    //Than thiet
        //    if (node.Attribute("loaikh").Value == "1")
        //    {
        //        foreach (var i in Node)
        //        {
        //            if (tienPMH <= thanhtoan)
        //            {
        //                node1.Remove();
        //                testQT.Save(pathQT);

        //                var hd = testXML_HD.Descendants("hoadon").Where(x => x.Attribute("id").Value.Equals(idhd)).FirstOrDefault();
        //                hd.Element("tongtienmua").Value = thanhtoan.ToString();
        //                hd.Element("tiengiam").Value = tiengiam.ToString();
        //                hd.Element("tienkhachtra").Value = tientra.ToString();
        //                hd.Element("trangthaiTT").Value = "1";
        //                testXML_HD.Save(path2);

        //            }
        //            if (tienPMH > thanhtoan)
        //            {
        //                while (tienPMH > thanhtoan)
        //                {
        //                    tienPMH = tienPMH - 20000;
        //                    soluong = i.sl - 1;
        //                    d = d + 1;
        //                }
        //                //MessageBox.Show("tien PMH" + soluong);
        //                MessageBox.Show("tien PMHjjgtrjgj" + (thanhtoan-(d*20000)));
        //                node1.Element("soluong").Value = soluong.ToString();
        //                testQT.Save(pathQT);
        //            }
        //        } 
        //    }

        //    //Thanh vien va VIP
        //    var sophieu100 = testQT.Descendants("quatang_uudai").Where(x => x.Attribute("idkh").Value.Equals(id) && x.Element("tientuongung").Value.Equals("100000")).FirstOrDefault();
        //    var sophieu20 = testQT.Descendants("quatang_uudai").Where(x => x.Attribute("idkh").Value.Equals(id) && x.Element("tientuongung").Value.Equals("20000")).FirstOrDefault();
        //    int sp100 = int.Parse(sophieu100.Element("soluong").Value);
        //    int sp20 = int.Parse(sophieu20.Element("soluong").Value);
        //    int tt = thanhtoan;
        //    if (node.Attribute("loaikh").Value != "1")
        //    {
        //        //int tt = thanhtoan;
        //        if (tienPMH > thanhtoan)
        //        {
        //            foreach (var i in Node)
        //            {
        //                while (tt >= 20000)
        //                {
        //                    if (tt >= 100000 && int.Parse(sophieu100.Element("soluong").Value) > 0)
        //                    {
        //                        //số phiếu mua hàng hiện tại -1
        //                        //diem = diem - 500;
        //                        sp100 = sp100 - 1;
        //                        tt = tt - 100000;

        //                    }
        //                    if (tt < 100000 && int.Parse(sophieu20.Element("soluong").Value) > 0)
        //                    {
        //                        //số phiếu mua hàng hiện tại -1
        //                        //diem = diem - 100;
        //                        sp20 = sp20 - 1;
        //                        tt = tt - 20000;
        //                    }
        //                }
        //                //tt còn lại là số tiền khách hàng cần trả

        //            }
        //            MessageBox.Show("tien tra" + tt + "sp100" + sp100 + "sp20" + sp20);
        //            if (sp100 == 0)
        //                sophieu100.Remove();
        //            if(sp100>0)
        //                sophieu100.Element("soluong").Value = sp100.ToString();
        //            if (sp20 == 0)
        //                sophieu20.Remove();
        //            if(sp20>0)
        //            sophieu20.Element("soluong").Value = sp20.ToString();
        //            testQT.Save(pathQT);
        //        }

        //        if (tienPMH <= thanhtoan)
        //        {
        //            node1.Remove();
        //            testQT.Save(pathQT);

        //            var hd = testXML_HD.Descendants("hoadon").Where(x => x.Attribute("id").Value.Equals(idhd)).FirstOrDefault();
        //            hd.Element("tongtienmua").Value = thanhtoan.ToString();
        //            hd.Element("tiengiam").Value = tiengiam.ToString();
        //            hd.Element("tienkhachtra").Value = tientra.ToString();
        //            hd.Element("trangthaiTT").Value = "1";
        //            testXML_HD.Save(path2);

        //        }
        //    }
        //    //var Node = (from c in testXML.Descendants("chitiet")
        //    //            join k in testXML_SP.Descendants("sanpham")
        //    //            on (string)c.Attribute("idsp") equals (string)k.Attribute("id")
        //    //            where (string)c.Attribute("idhoadon") == id.ToString()
        //    //            select new
        //    //            {
        //    //                mahoadon = (int)c.Attribute("idhoadon"),
        //    //                masp = (int)c.Attribute("idsp"),
        //    //                sl = (int)c.Element("soluongmua"),
        //    //                gia = (int)k.Element("dongia"),
        //    //                giamgia = (int)k.Element("giamgia")

        //    //            }).ToList();

        //    //MessageBox.Show("bla bla" + id);
        //    //var hd = testXML_HD.Descendants("hoadon").Where(x => x.Attribute("id").Value.Equals(id.ToString())).FirstOrDefault();
        //    //hd.Element("tongtienmua").Value = thanhtoan.ToString();
        //    //hd.Element("tiengiam").Value = tiengiam.ToString();
        //    //hd.Element("tienkhachtra").Value = tientra.ToString();
        //    //hd.Element("trangthaiTT").Value = "1";
        //    //testXML_HD.Save(path2);
        //}


        public void ThanhToanPMH(string id, string idhd, int tientra, double tiengiam, int thanhtoan,int tienPMH)
        {
            var Node = (from kh in testXML_KH.Descendants("khachhang")
                        
                        where (string)kh.Attribute("id").Value == id.ToString()
                        select new
                        {
                            
                            id = (string)kh.Attribute("id")
                        }).ToList();
            var node1 = testQT.Descendants("quatang_uudai").Where(x => x.Attribute("idkh").Value.Equals(id)).FirstOrDefault();
            
            var node = testXML_KH.Descendants("khachhang").Where(x => x.Attribute("id").Value.Equals(id)).FirstOrDefault();
            //MessageBox.Show("" + node.Attribute("loaikh").Value);

            //Than thiet
            if (node.Attribute("loaikh").Value == "1")
            {
                //MessageBox.Show("bdsaahdh");
                int tt = thanhtoan;
                int diem = int.Parse(node.Element("diemconlai").Value);
                int j = 0;
                var sl = testQT.Descendants("quatang_uudai").Count();
                foreach (var i in Node)
                {
                    while (diem >= 100 && tt >= 20000)
                    {
                        tt = tt - 20000;
                        diem = diem - 100;
                        j++;
                    }
                    XElement newStudent = new XElement("quatang_uudai",
                    new XElement("tenqt", "Phiếu chiết khấu"),
                    new XElement("noidung", "Khách hàng đã nhận phiếu chiết khấu 20000"),
                    new XElement("soluong", j.ToString()),
                    new XElement("tientuongung", "20000"),
                    new XElement("trangthai", "1"));
                    var lastStudent = testQT.Descendants("quatang_uudai").Last();
                    newStudent.SetAttributeValue("id", sl + 1);
                    newStudent.SetAttributeValue("idkh", id);
                    testQT.Element("QUATANG_UUDAI").Add(newStudent);
                    testQT.Save(pathQT);

                    node.Element("diemconlai").Value = diem.ToString();
                    testXML_KH.Save(path3);
                    var hd = testXML_HD.Descendants("hoadon").Where(x => x.Attribute("id").Value.Equals(idhd)).FirstOrDefault();
                    hd.Element("tongtienmua").Value = thanhtoan.ToString();
                    hd.Element("tiengiam").Value = tiengiam.ToString();
                    hd.Element("tienkhachtra").Value = tientra.ToString();
                    hd.Element("trangthaiTT").Value = "1";
                    hd.Element("sudungPMH").Value = tienPMH.ToString();
                    testXML_HD.Save(path2);

                }
            }

            //thành viên
            if (node.Attribute("loaikh").Value == "2")
            {
                int diemtl = int.Parse(node.Element("diemtichluy").Value);
                int tt = thanhtoan;
                int diem = int.Parse(node.Element("diemconlai").Value);
                //MessageBox.Show(diemtl.ToString());

                foreach (var i in Node)
                {
                    int j = 0;
                    int a = 0;
                    int k = 0;
                    int h = 0;
                    if (diem >= 500 && diemtl>=diem)
                    {
                        var sl = testQT.Descendants("quatang_uudai").Count();
                        while (diem >= 500 && tt >= 100000)
                        {
                            tt = tt - 100000;
                            diem = diem - 500;
                            j++;
                        }
                        XElement newStudent = new XElement("quatang_uudai",
                        new XElement("tenqt", "Phiếu chiết khấu"),
                        new XElement("noidung", "Khách hàng đã nhận phiếu chiết khấu 100000"),
                        new XElement("soluong", j.ToString()),
                        new XElement("tientuongung", "100000"),
                        new XElement("trangthai", "1"));
                        var lastStudent = testQT.Descendants("quatang_uudai").Last();
                        newStudent.SetAttributeValue("id", sl + 1);
                        newStudent.SetAttributeValue("idkh", id);
                        testQT.Element("QUATANG_UUDAI").Add(newStudent);
                        testQT.Save(pathQT);


                        
                        var sl1 = testQT.Descendants("quatang_uudai").Count();
                        while (diem >= 100 && tt >= 20000)
                        {
                            tt = tt - 20000;
                            diem = diem - 100;
                            k++;
                           // MessageBox.Show("hfyyfhyen");
                        }

                        XElement newStudent1 = new XElement("quatang_uudai",
                        new XElement("tenqt", "Phiếu chiết khấu"),
                        new XElement("noidung", "Khách hàng đã nhận phiếu chiết khấu 20000"),
                        new XElement("soluong", k.ToString()),
                        new XElement("tientuongung", "20000"),
                        new XElement("trangthai", "1"));
                        var lastStudent1 = testQT.Descendants("quatang_uudai").Last();
                        newStudent1.SetAttributeValue("id", sl1 + 1);
                        newStudent1.SetAttributeValue("idkh", id);
                        testQT.Element("QUATANG_UUDAI").Add(newStudent1);
                        testQT.Save(pathQT);

                        node.Element("diemconlai").Value = diem.ToString();
                        testXML_KH.Save(path3);
                        var hd = testXML_HD.Descendants("hoadon").Where(x => x.Attribute("id").Value.Equals(idhd)).FirstOrDefault();
                        hd.Element("tongtienmua").Value = thanhtoan.ToString();
                        hd.Element("tiengiam").Value = tiengiam.ToString();
                        hd.Element("tienkhachtra").Value = tientra.ToString();
                        hd.Element("trangthaiTT").Value = "1";
                        hd.Element("sudungPMH").Value = tienPMH.ToString();
                        testXML_HD.Save(path2);
                    }

                    //thân thiết + thành viên
                    if (diem >= 500 && diemtl < diem)
                    {
                        //MessageBox.Show("dggdsgffbabaiu");
                        var sl = testQT.Descendants("quatang_uudai").Count();
                        while (diemtl<diem && tt>=20000)
                        {
                            tt = tt - 20000;
                            diem = diem - 100;
                            h++;
                            //MessageBox.Show("dggdsgffbabaiu"+diemtl+" "+tt);
                        }

                        
                        XElement newStudent = new XElement("quatang_uudai",
                        new XElement("tenqt", "Phiếu chiết khấu"),
                        new XElement("noidung", "Khách hàng đã nhận phiếu chiết khấu 20000"),
                        new XElement("soluong", h.ToString()),
                        new XElement("tientuongung", "20000"),
                        new XElement("trangthai", "1"));
                        var lastStudent = testQT.Descendants("quatang_uudai").Last();
                        newStudent.SetAttributeValue("id", sl + 1);
                        newStudent.SetAttributeValue("idkh", id);
                        testQT.Element("QUATANG_UUDAI").Add(newStudent);
                        testQT.Save(pathQT);
                        node.Element("diemconlai").Value = diem.ToString();
                        testXML_KH.Save(path3);

                        if(diem >= 500 && tt >= 100000)
                        {
                            while (diem >= 500 && tt >= 100000)
                            {
                                //MessageBox.Show("yen yen");
                                tt = tt - 100000;
                                diem = diem - 500;
                                a++;
                            }
                            XElement newStudent2 = new XElement("quatang_uudai",
                            new XElement("tenqt", "Phiếu chiết khấu"),
                            new XElement("noidung", "Khách hàng đã nhận phiếu chiết khấu 100000"),
                            new XElement("soluong", a.ToString()),
                            new XElement("tientuongung", "100000"),
                            new XElement("trangthai", "1"));
                            var lastStudent2 = testQT.Descendants("quatang_uudai").Last();
                            newStudent2.SetAttributeValue("id", sl + 1);
                            newStudent2.SetAttributeValue("idkh", id);
                            testQT.Element("QUATANG_UUDAI").Add(newStudent2);
                            testQT.Save(pathQT);
                            node.Element("diemconlai").Value = diem.ToString();
                            testXML_KH.Save(path3);



                            if (diem >= 100 && tt >= 20000)
                            {
                                var sl1 = testQT.Descendants("quatang_uudai").Count();
                                while (diem >= 100 && tt >= 20000)
                                {
                                    tt = tt - 20000;
                                    diem = diem - 100;
                                    k++;
                                }

                                XElement newStudent1 = new XElement("quatang_uudai",
                                new XElement("tenqt", "Phiếu chiết khấu"),
                                new XElement("noidung", "Khách hàng đã nhận phiếu chiết khấu 20000"),
                                new XElement("soluong", k.ToString()),
                                new XElement("tientuongung", "20000"),
                                new XElement("trangthai", "1"));
                                var lastStudent1 = testQT.Descendants("quatang_uudai").Last();
                                newStudent1.SetAttributeValue("id", sl1 + 1);
                                newStudent1.SetAttributeValue("idkh", id);
                                testQT.Element("QUATANG_UUDAI").Add(newStudent1);
                                testQT.Save(pathQT);

                                node.Element("diemconlai").Value = diem.ToString();
                                testXML_KH.Save(path3);
                            }
                        }
                        var hd = testXML_HD.Descendants("hoadon").Where(x => x.Attribute("id").Value.Equals(idhd)).FirstOrDefault();
                        hd.Element("tongtienmua").Value = thanhtoan.ToString();
                        hd.Element("tiengiam").Value = tiengiam.ToString();
                        hd.Element("tienkhachtra").Value = tientra.ToString();
                        hd.Element("trangthaiTT").Value = "1";
                        hd.Element("sudungPMH").Value = tienPMH.ToString();
                        testXML_HD.Save(path2);

                    }
                }
            }


            if (node.Attribute("loaikh").Value == "3" && node.Attribute("loaikh").Value == "4")
            {
                int tt = thanhtoan;
                int diem = int.Parse(node.Element("diemconlai").Value);
                int j = 0;
                
                foreach (var i in Node)
                {
                    if (diem >= 500)
                    {
                        var sl = testQT.Descendants("quatang_uudai").Count();
                        while (diem >= 500 && tt >= 100000)
                        {
                            tt = tt - 100000;
                            diem = diem - 500;
                            j++;
                        }
                        XElement newStudent = new XElement("quatang_uudai",
                        new XElement("tenqt", "Phiếu chiết khấu"),
                        new XElement("noidung", "Khách hàng đã nhận phiếu chiết khấu 100000"),
                        new XElement("soluong", j.ToString()),
                        new XElement("tientuongung", "100000"),
                        new XElement("trangthai", "1"));
                        var lastStudent = testQT.Descendants("quatang_uudai").Last();
                        newStudent.SetAttributeValue("id", sl + 1);
                        newStudent.SetAttributeValue("idkh", id);
                        testQT.Element("QUATANG_UUDAI").Add(newStudent);
                        testQT.Save(pathQT);

                       
                        int k = 0;
                        var sl1 = testQT.Descendants("quatang_uudai").Count();
                        while (diem >= 100 && tt >= 20000)
                        {
                            tt = tt - 20000;
                            diem = diem - 100;
                            k++;
                        }

                        XElement newStudent1 = new XElement("quatang_uudai",
                        new XElement("tenqt", "Phiếu chiết khấu"),
                        new XElement("noidung", "Khách hàng đã nhận phiếu chiết khấu 20000"),
                        new XElement("soluong", k.ToString()),
                        new XElement("tientuongung", "20000"),
                        new XElement("trangthai", "1"));
                        var lastStudent1 = testQT.Descendants("quatang_uudai").Last();
                        newStudent1.SetAttributeValue("id", sl1 + 1);
                        newStudent1.SetAttributeValue("idkh", id);
                        testQT.Element("QUATANG_UUDAI").Add(newStudent1);
                        testQT.Save(pathQT);

                        node.Element("diemconlai").Value = diem.ToString();
                        testXML_KH.Save(path3);
                        var hd = testXML_HD.Descendants("hoadon").Where(x => x.Attribute("id").Value.Equals(idhd)).FirstOrDefault();
                        hd.Element("tongtienmua").Value = thanhtoan.ToString();
                        hd.Element("tiengiam").Value = tiengiam.ToString();
                        hd.Element("tienkhachtra").Value = tientra.ToString();
                        hd.Element("trangthaiTT").Value = "1";
                        hd.Element("sudungPMH").Value = tienPMH.ToString();
                        testXML_HD.Save(path2);
                    }  
                }
            }
        }

        
        public int PMH(string id, string idhd, int tientra, double tiengiam, int thanhtoan)
        {
            var Node = (from kh in testXML_KH.Descendants("khachhang")
                      
                        where (string)kh.Attribute("id").Value == id
                        select new
                        {
                            id = (string)kh.Attribute("id")
                        }).ToList();
            var node1 = testQT.Descendants("quatang_uudai").Where(x => x.Attribute("idkh").Value.Equals(id)).FirstOrDefault();

            var node = testXML_KH.Descendants("khachhang").Where(x => x.Attribute("id").Value.Equals(id)).FirstOrDefault();
            //MessageBox.Show("" + node.Attribute("loaikh").Value);
            int t = 0;
            //Than thiet
            if (node.Attribute("loaikh").Value == "1")
            {
                int tt = thanhtoan;
                int diem = int.Parse(node.Element("diemconlai").Value);
                int j = 0;
                var sl = testQT.Descendants("quatang_uudai").Count();
                foreach (var i in Node)
                {
                    while (diem >= 100 && tt >= 20000)
                    {
                        tt = tt - 20000;
                        diem = diem - 100;
                        j++;
                    }
                    
                }
                t= j * 20000;
            }

            if (node.Attribute("loaikh").Value == "2")
            {
                int diemtl = int.Parse(node.Element("diemtichluy").Value);
                int tt = thanhtoan;
                int diem = int.Parse(node.Element("diemconlai").Value);
               
                int j = 0;
                int a = 0;
                int k = 0;
                int h = 0;
                int k1 = 0;
                foreach (var i in Node)
                {
                    
                    
                    //=========================thành viên lâu 
                    if (diem >= 500 && diemtl >= diem)
                    {
                        
                        while (diem >= 500 && tt >= 100000)
                        {
                            tt = tt - 100000;
                            diem = diem - 500;
                            j++;
                        }
                        while (diem >= 100 && tt >= 20000)
                        {
                            tt = tt - 20000;
                            diem = diem - 100;
                            k++;
                            //MessageBox.Show("hfyyfhyen");
                        }
                        
                    }
                    
                    //================ mới lên thành viên

                    if (diem >= 500 && diemtl < diem)
                        {
                            //MessageBox.Show("dggdsgffbabaiu");

                            while (diemtl < diem && tt >= 20000)
                            {
                                tt = tt - 20000;
                                diem = diem - 100;
                                h++;
                                //MessageBox.Show("dggdsgffbabaiu" + diemtl + " " + tt);
                            }
                        if (diem >= 500 && tt >= 100000)
                        {
                            while (diem >= 500 && tt >= 100000)
                            {
                                //MessageBox.Show("yen yen");
                                tt = tt - 100000;
                                diem = diem - 500;
                                a++;
                            }


                            if (diem >= 100 && tt >= 20000)
                            {

                                while (diem >= 100 && tt >= 20000)
                                {
                                    tt = tt - 20000;
                                    diem = diem - 100;
                                    k1++;
                                }
                            }
                        }             
                    }
                   
                }
                t = (j * 100000) + (k * 20000)+ (h * 20000) + (a * 100000) + (k1 * 20000);
            }

            int j1 = 0;
            int j2 = 0;
            if (node.Attribute("loaikh").Value == "3" && node.Attribute("loaikh").Value == "4")
                        {
                            int tt = thanhtoan;
                            int diem = int.Parse(node.Element("diemconlai").Value);
                            

                            foreach (var i in Node)
                            {
                                if (diem >= 500)
                                {

                                    while (diem >= 500 && tt >= 100000)
                                    {
                                        tt = tt - 100000;
                                        diem = diem - 500;
                            j1++;
                                    }
                                    while (diem >= 100 && tt >= 20000)
                                    {
                                        tt = tt - 20000;
                                        diem = diem - 100;
                            j2++;
                                    }
                                }
                            }
                t = j1 * 100000 + j2 * 20000;
            }
            return t;
        }
        public double TinhTien(int id)
        {
            var Node = (from ct in testXML.Descendants("chitiet")
                        join sp in testXML_SP.Descendants("sanpham")
                        on (string)ct.Attribute("idsp") equals (string)sp.Attribute("id")
                        where (string)ct.Attribute("idhoadon") == id.ToString()
                        select new
                        {
                            mahoadon = int.Parse((string)ct.Attribute("idhoadon")),
                            masp = int.Parse((string)ct.Attribute("idsp")),
                            sl = double.Parse((string)ct.Element("soluongmua")),
                            gia = double.Parse((string)sp.Element("dongia")),
                            giamgia = double.Parse((string)sp.Element("giamgia"))

                        }).ToList();
            double tiengiam = 0;
            double thanhtoan =0;
            //int diem = 0;
            foreach (var i in Node)
            {
                
                tiengiam = tiengiam + i.sl * i.gia * (i.giamgia / 100);
                thanhtoan = thanhtoan + i.gia * ((100 - i.giamgia) / 100) * i.sl;
            
            }
            //diem = (int)(thanhtoan / 10000);
            //var count = testXML.Descendants("chitiet").Where(x=>x.Attribute("idhoadon").Equals(id)).Count();
            //MessageBox.Show("" + thanhtoan);
            return thanhtoan;
        }

        
        public double TinhTiengiam(int id)
        {
            var Node = (from ct in testXML.Descendants("chitiet")
                        join sp in testXML_SP.Descendants("sanpham")
                        on (string)ct.Attribute("idsp") equals (string)sp.Attribute("id")
                        where (string)ct.Attribute("idhoadon") == id.ToString()
                        select new
                        {
                            mahoadon = int.Parse((string)ct.Attribute("idhoadon")),
                            masp = int.Parse((string)ct.Attribute("idsp")),
                            sl = double.Parse((string)ct.Element("soluongmua")),
                            gia = double.Parse((string)sp.Element("dongia")),
                            giamgia = double.Parse((string)sp.Element("giamgia"))

                        }).ToList();
            double tiengiam = 0;
            //double thanhtoan = 0;
            
            foreach (var i in Node)
            {
                tiengiam = tiengiam + i.sl * i.gia * (i.giamgia / 100);       
            }
            //MessageBox.Show("" + tiengiam);
            return tiengiam;
        }

        //------------------------------ In hóa đơn----------------------------------
        public List<InHoaDon_DTO> InHoaDon(int id,double tienkhachtra, int sl)
        {
            
            var Node = (from hd in testXML_HD.Descendants("hoadon")
                        join ct in testXML.Descendants("chitiet")
                        on (string)hd.Attribute("id") equals (string)ct.Attribute("idhoadon")
                        join sp in testXML_SP.Descendants("sanpham")
                        on (string)ct.Attribute("idsp") equals (string)sp.Attribute("id")
                        join kh in testXML_KH.Descendants("khachhang")
                        on (string)hd.Attribute("idkh") equals (string)kh.Attribute("id")
                        join nv in testXML_NV.Descendants("nhanvien")
                        on (string)hd.Attribute("idnv") equals (string)nv.Attribute("id")
                        where (string)hd.Attribute("id") == id.ToString()
                        select new InHoaDon_DTO
                        {
                            id = int.Parse((string)hd.Attribute("id")),
                            ngayin_hoadon = (string)hd.Element("ngaymua"),
                            tennv = (string)nv.Element("tennv"),
                            idsp = int.Parse((string)sp.Attribute("id")),
                            tensp = (string)sp.Element("tensp"),
                            soluongmua = int.Parse((string)ct.Element("soluongmua")),
                            dongia = double.Parse((string)sp.Element("dongia")),
                            dongia_dagiamgia = double.Parse((string)sp.Element("dongia")) * (1-double.Parse((string)sp.Element("giamgia")) / 100),
                            tongtiensp = double.Parse((string)sp.Element("dongia")) * (1 - double.Parse((string)sp.Element("giamgia")) / 100) * int.Parse((string)ct.Element("soluongmua")),
                            tongtienmua = double.Parse((string)hd.Element("tongtienmua")),
                            tienkhachtra = tienkhachtra,
                            tienthoi = double.Parse(tienkhachtra.ToString())+ double.Parse((string)hd.Element("sudungPMH")) - double.Parse((string)hd.Element("tongtienmua")),
                            idkhachhang = int.Parse((string)kh.Attribute("id")),
                            tenkh = (string)kh.Element("tenkh"),
                            tienthue = double.Parse((string)hd.Element("tongtienmua")) * 10 / 100,
                            tienchu = DocTienBangChu((long)hd.Element("tongtienmua"))+" đồng",
                            tongspmua = sl
                        }).ToList();
            return Node;
        }

        XDocument testXMLk = XDocument.Load("..\\..\\Data\\KH_LOAIKH.xml");
        //=====================================In the===========================
        public List<TheKH> Print(string id, string ten, string link, string loai)
        {
            var Node = (from kh in testXMLk.Descendants("Join")
                        where (string)kh.Element("id") == id
                        select new TheKH
                        {
                            tenkh = ten,
                            mathe = link,
                            loaithe = loai
                        }).ToList();
            //MessageBox.Show("slll"+Node.Count);
            return Node;
        }
        //--------------------------đọc số thành chữ----------------------------
        private string[] ChuSo = new string[10] { " không", " một", " hai", " ba", " bốn", " năm", " sáu", " bảy", " tám", " chín" };
        private string[] Tien = new string[6] { "", " nghìn", " triệu", " tỷ", " nghìn tỷ", " triệu tỷ" };
        // Hàm đọc số thành chữ
        public string DocTienBangChu(long SoTien)
        {
            int lan, i;
            long so;
            string KetQua = "", tmp = "";
            int[] ViTri = new int[6];
            if (SoTien < 0) return "Số tiền âm !";
            if (SoTien == 0) return "Không đồng !";
            if (SoTien > 0)
            {
                so = SoTien;
            }
            else
            {
                so = -SoTien;
            }
            //Kiểm tra số quá lớn
            if (SoTien > 8999999999999999)
            {
                SoTien = 0;
                return "";
            }
            ViTri[5] = (int)(so / 1000000000000000);
            so = so - long.Parse(ViTri[5].ToString()) * 1000000000000000;
            ViTri[4] = (int)(so / 1000000000000);
            so = so - long.Parse(ViTri[4].ToString()) * +1000000000000;
            ViTri[3] = (int)(so / 1000000000);
            so = so - long.Parse(ViTri[3].ToString()) * 1000000000;
            ViTri[2] = (int)(so / 1000000);
            ViTri[1] = (int)((so % 1000000) / 1000);
            ViTri[0] = (int)(so % 1000);
            if (ViTri[5] > 0)
            {
                lan = 5;
            }
            else if (ViTri[4] > 0)
            {
                lan = 4;
            }
            else if (ViTri[3] > 0)
            {
                lan = 3;
            }
            else if (ViTri[2] > 0)
            {
                lan = 2;
            }
            else if (ViTri[1] > 0)
            {
                lan = 1;
            }
            else
            {
                lan = 0;
            }
            for (i = lan; i >= 0; i--)
            {
                tmp = DocSo3ChuSo(ViTri[i]);
                KetQua += tmp;
                if (ViTri[i] != 0) KetQua += Tien[i];
                if ((i > 0) && (!string.IsNullOrEmpty(tmp))) KetQua += ",";//&& (!string.IsNullOrEmpty(tmp))
            }
            if (KetQua.Substring(KetQua.Length - 1, 1) == ",") KetQua = KetQua.Substring(0, KetQua.Length - 1);
            KetQua = KetQua.Trim();
            return KetQua.Substring(0, 1).ToUpper() + KetQua.Substring(1);
        }
        // Hàm đọc số có 3 chữ số
        private string DocSo3ChuSo(int baso)
        {
            int tram, chuc, donvi;
            string KetQua = "";
            tram = (int)(baso / 100);
            chuc = (int)((baso % 100) / 10);
            donvi = baso % 10;
            if ((tram == 0) && (chuc == 0) && (donvi == 0)) return "";
            if (tram != 0)
            {
                KetQua += ChuSo[tram] + " trăm";
                if ((chuc == 0) && (donvi != 0)) KetQua += " linh";
            }
            if ((chuc != 0) && (chuc != 1))
            {
                KetQua += ChuSo[chuc] + " mươi";
                if ((chuc == 0) && (donvi != 0)) KetQua = KetQua + " linh";
            }
            if (chuc == 1) KetQua += " mười";
            switch (donvi)
            {
                case 1:
                    if ((chuc != 0) && (chuc != 1))
                {
                    KetQua += " mốt";
                }
                else
                {
                    KetQua += ChuSo[donvi];
                }
                break;
                case 5:
                    if (chuc == 0)
                {
                    KetQua += ChuSo[donvi];
                }
                else
                {
                    KetQua += " lăm";
                }
                break;
                default:
                    if (donvi != 0)
                {
                    KetQua += ChuSo[donvi];
                }
                break;
            }
            return KetQua;
        }

        //-=================================================================
        public int soluong(int id)
        {
            var Node = (from hd in testXML_HD.Descendants("hoadon")
                        join ct in testXML.Descendants("chitiet")
                        on (string)hd.Attribute("id") equals (string)ct.Attribute("idhoadon")
                        where (string)hd.Attribute("id") == id.ToString()
                        select new
                        {
                            sl = int.Parse((string)ct.Element("soluongmua"))
                        }).ToList();
            int solg = 0;
            
            foreach (var i in Node)
            {
                solg = solg + i.sl;                
            }
            return solg;
        }

        //---------------------Trả sp------------------
        public void TraSP(CT_HOADON_DTO cthd,ListView lv, string idsp,int sl)
        {
            try
            {
                XElement Node = testXML.Descendants("chitiet").Where(c => c.Attribute("id").Value.Equals(cthd.id.ToString())).FirstOrDefault();
                Node.Remove();
                //Node.Attribute("idsp").Value = cthd.idsp.ToString();
                //Node.Element("soluongmua").Value = cthd.soluongmua.ToString();
                testXML.Save(path);

                var node = testXML_SP.Descendants("sanpham").Where(x => x.Attribute("id").Value.Equals(idsp)).FirstOrDefault();
                node.Element("soluong").Value = (int.Parse(node.Element("soluong").Value) + sl).ToString();
                testXML_SP.Save(path5);

                var node1 = testXML_SPLOAISP.Descendants("Join").Where(x => x.Element("id").Value.Equals(idsp)).FirstOrDefault();
                node1.Element("soluong").Value = node.Element("soluong").Value;
                testXML_SPLOAISP.Save(path6);
            }
            catch (Exception err)
            {
                MessageBox.Show("Lỗi sửa thông tin:" + err.Message);
            }
        }

        //---------------------------đổi sản phẩm--------------------------------
        public void DoiSP(CT_HOADON_DTO cthd, ListView lv, string idsp, int sl,int sl_kdoi)
        {
            try
            {
                

                var Node1 = (from sp in testXML_SP.Descendants("sanpham")
                            where (string)sp.Attribute("id") == idsp.ToString()
                            select new
                            {
                                soluong = (int)sp.Element("soluong")
                            }).FirstOrDefault();
                if (Node1.soluong > 0 && Math.Abs(sl_kdoi-sl) <= Node1.soluong)
                {

                    XElement Node = testXML.Descendants("chitiet").Where(c => c.Attribute("id").Value.Equals(cthd.id.ToString())).FirstOrDefault();

                    Node.Attribute("idsp").Value = cthd.idsp.ToString();
                    Node.Element("soluongmua").Value = cthd.soluongmua.ToString();
                    testXML.Save(path);
                    var node = testXML_SP.Descendants("sanpham").Where(x => x.Attribute("id").Value.Equals(idsp)).FirstOrDefault();
                    node.Element("soluong").Value = (int.Parse(node.Element("soluong").Value) + (sl_kdoi- sl)).ToString();
                    testXML_SP.Save(path5);

                    var node1 = testXML_SPLOAISP.Descendants("Join").Where(x => x.Element("id").Value.Equals(idsp)).FirstOrDefault();
                    node1.Element("soluong").Value = node.Element("soluong").Value;
                    testXML_SPLOAISP.Save(path6);
                }
                else
                {
                    MessageBox.Show("Hiện tại sản phẩm này còn " + Node1.soluong + " sản phẩm!");
                }
            }
            catch (Exception err)
            {
                MessageBox.Show("Lỗi sửa thông tin:" + err.Message);
            }
        }

        //===========================Xem lịch sử=========================================
        public void LoadLS(ListView lv)  // tìm theo ID
        {
            try
            {
                //lv.Items.Clear();
                DataSet dataSet = new DataSet();
                DataTable dt = new DataTable();
                dataSet.ReadXml("..\\..\\Data\\HOA_DON.xml");
                dt = dataSet.Tables["hoadon"];
                int co = 0;
                int i = 0;
                foreach (DataRow dr in dt.Rows)
                {
                    if (int.Parse(dr["tongtienmua"].ToString()) > 0)
                    {
                        lv.Items.Add((i + 1).ToString());
                        lv.Items[i].SubItems.Add(dr["idkh"].ToString());
                        lv.Items[i].SubItems.Add(dr["idnv"].ToString());
                        lv.Items[i].SubItems.Add(dr["id"].ToString());
                        lv.Items[i].SubItems.Add(dr["tongtienmua"].ToString());
                        lv.Items[i].SubItems.Add(dr["tienkhachtra"].ToString());
                        lv.Items[i].SubItems.Add(dr["tiengiam"].ToString());

                        lv.Items[i].SubItems.Add(dr["ngaymua"].ToString());
                        if (dr["trangthai"].ToString() == "0")
                        {
                            lv.Items[i].SubItems.Add("Chưa cập nhật điểm");
                        }
                        if (dr["trangthai"].ToString() == "1")
                        {
                            lv.Items[i].SubItems.Add("Đã cập nhật điểm");
                        }
                        if (dr["trangthaiTT"].ToString() == "0")
                        {
                            lv.Items[i].SubItems.Add("Chưa thanh toán");
                        }
                        if (dr["trangthaiTT"].ToString() == "1")
                        {
                            lv.Items[i].SubItems.Add("Đã thanh toán");
                        }
                        co++;
                        i++;
                    }

                }

            }
            catch (Exception e)
            {
                MessageBox.Show("Lỗi: " + e.Message);
            }
        }

        public void Loaduudai(ListView lv, string tendn)
        {
            //----------------------xem uu đãi-------------------------------

            try
            {
                //XElement node = testXML_KH.Descendants("khachhang").Where(x => x.Element("tendn").Value.Equals(tendn.ToString())).FirstOrDefault();
                //MessageBox.Show("ffffff" + node.Element("id").Value);
                DataSet dataSet = new DataSet();
                DataTable dt = new DataTable();
                dataSet.ReadXml("..\\..\\Data\\QUATANG_UUDAI.xml");
                dt = dataSet.Tables["quatang_uudai"];
                int co = 0;
                int j = 0;
                foreach (DataRow dr in dt.Rows)
                {
                    //foreach (var k in node)
                    //{
                    if (dr["idkh"].ToString() == "2")
                    {
                        lv.Items.Add(dr["id"].ToString());
                        lv.Items[j].SubItems.Add(dr["idkh"].ToString());
                        lv.Items[j].SubItems.Add(dr["tenqt"].ToString());
                        lv.Items[j].SubItems.Add(dr["noidung"].ToString());
                        if (dr["trangthai"].ToString() == "1")
                        {
                            lv.Items[j].SubItems.Add("Đã nhận quà tặng");
                        }
                        if (dr["trangthai"].ToString() == "0")
                        {
                            lv.Items[j].SubItems.Add("Chưa nhận quà tặng");
                        }
                        //MessageBox.Show("fff" + dr["tenqt"]);
                        co++;
                        j++;
                    }
                }
            }
            catch (Exception e)
            {
                MessageBox.Show("Lỗi: " + e.Message);
            }
            //}
        }

        //====================================tìm kiếm lịch sử theo mã kh==============================
        public int TimkiemLS(int id)
        {
            var Node = (from kh in testXML_HD.Descendants("hoadon")
                        where (string)kh.Attribute("idkh") == id.ToString()
                        select new
                        {
                            makh = int.Parse((string)kh.Attribute("idkh")),
                        }).ToList();

            int kq = Node.Count;
            return kq;
        }

        public void Hienthi_TimkiemLS(ListView lv, int maCanTim)
        {
            try
            {
                lv.Items.Clear();
                DataSet dataSet = new DataSet();
                DataTable dt = new DataTable();
                dataSet.ReadXml("..\\..\\Data\\HOA_DON.xml");
                dt = dataSet.Tables["hoadon"];
                int i = 0;
                foreach (DataRow dr in dt.Rows)
                {
                    if (dr["idkh"].ToString() == maCanTim.ToString())
                    {
                        lv.Items.Add((i + 1).ToString());
                        lv.Items[i].SubItems.Add(dr["idkh"].ToString());
                        lv.Items[i].SubItems.Add(dr["idnv"].ToString());
                        lv.Items[i].SubItems.Add(dr["id"].ToString());
                        lv.Items[i].SubItems.Add(dr["tongtienmua"].ToString());
                        lv.Items[i].SubItems.Add(dr["tienkhachtra"].ToString());
                        lv.Items[i].SubItems.Add(dr["tiengiam"].ToString());

                        lv.Items[i].SubItems.Add(dr["ngaymua"].ToString());
                        if (dr["trangthai"].ToString() == "0")
                        {
                            lv.Items[i].SubItems.Add("Chưa cập nhật điểm");
                        }
                        if (dr["trangthai"].ToString() == "1")
                        {
                            lv.Items[i].SubItems.Add("Đã cập nhật điểm");
                        }
                        if (dr["trangthaiTT"].ToString() == "0")
                        {
                            lv.Items[i].SubItems.Add("Chưa thanh toán");
                        }
                        if (dr["trangthaiTT"].ToString() == "1")
                        {
                            lv.Items[i].SubItems.Add("Đã thanh toán");
                        }
                        i++;
                    }

                }
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }

        public void CapNhatDiem()
        {
            var Node = (from hd in testXML_HD.Descendants("hoadon")
                        join kh in testXML_KH.Descendants("khachhang")
                        on (int)hd.Attribute("idkh") equals (int)kh.Attribute("id")
                        where (string)hd.Element("trangthai") == 0.ToString() 
                        select new
                        {
                            ma = (int)hd.Attribute("id"),
                            makh = (int)kh.Attribute("id"),
                            manv = (int)hd.Attribute("idnv"),
                            tong = (int)hd.Element("tongtienmua"),
                            tienkt = (int)hd.Element("tienkhachtra"),
                            tiengiam = (int)hd.Element("tiengiam"),
                            ngay = (string)hd.Element("ngaymua"),
                            trangthai = (string)hd.Element("trangthai"),
                            trangthaitt = (string)hd.Element("trangthaiTT")
                        }).ToList();
            
            DateTime a;
            DateTime dt = DateTime.Now;
            TimeSpan time;
            foreach (var i in Node)
            {
                a=DateTime.Parse(i.ngay);
                time = (a).Subtract(dt);
                int diem = 0;
                var node = (from hdd in testXML_HD.Descendants("hoadon")
                            where (string)hdd.Attribute("idkh").Value == i.makh.ToString() && (string)hdd.Element("trangthaiTT").Value == "1" && (string)hdd.Element("trangthai").Value == "0"
                            select new
                            {
                                tongtien = (int)hdd.Element("tongtienmua")
                            }).ToList();
                //MessageBox.Show("soluong" + node.Count);
                foreach (var j in node)
                {
                    diem = diem + j.tongtien / 10000;
                    //MessageBox.Show("diem: " + i.ma + "ff" + diem);
                }
                
                    if (Math.Abs(time.Days) >= 1)
                    {
                        XElement Node1 = testXML_KH.Descendants("khachhang").Where(x => x.Attribute("id").Value.Equals(i.makh.ToString())).FirstOrDefault();
                        Node1.Element("diemtichluy").Value = (int.Parse(Node1.Element("diemtichluy").Value) + diem).ToString();
                        Node1.Element("diemconlai").Value = (int.Parse(Node1.Element("diemconlai").Value) + diem).ToString();
                        XElement node2 = testXML_HD.Descendants("hoadon").Where(x => x.Attribute("id").Value.Equals(i.ma.ToString())).FirstOrDefault();
                        node2.Element("trangthai").Value = 1.ToString();
                        testXML_KH.Save(path3);
                        testXML_HD.Save(path2);
                        //XElement node3 = testXML_LS.Descendants("lic")
                        var count = testXML_LS.Descendants("lichsu").Count();
                        //MessageBox.Show("" + count);
                        XElement newStudent = new XElement("lichsu",
                            new XElement("idhoadon", i.ma),
                            new XElement("idkh", i.makh),
                            new XElement("idnv", i.manv),
                            new XElement("tongtien", i.tong),
                            new XElement("tiengiam", i.tiengiam),
                            new XElement("tienkhachtra", i.tienkt),
                            new XElement("ngaymua", i.ngay)
                            );
                        var lastStudent = testXML_LS.Descendants("lichsu").Last();
                        newStudent.SetAttributeValue("id", count + 1);
                        testXML_LS.Element("LICHSU").Add(newStudent);
                        testXML_LS.Save(path4);
                    }
                
            }   
        }

        public void CapNhatDiemNgay(int diemcong)
        {
            var Node = (from kh in testXML_KH.Descendants("khachhang")
                        join hd in testXML_HD.Descendants("hoadon")
                        on(int)kh.Attribute("id") equals(int)hd.Attribute("idkh")
                        where(string)hd.Element("trangthai") == 0.ToString()
                        select new
                        {
                            ma = (int)kh.Attribute("id"),
                            mahd = (int)hd.Attribute("id")
                            
                        }).ToList();
            //MessageBox.Show("soluong" + Node.Count);
            foreach (var i in Node)
            {

                int diem = 0;
                var node = (from hdd in testXML_HD.Descendants("hoadon")
                            where (string)hdd.Attribute("idkh").Value == i.ma.ToString() && (string)hdd.Element("trangthaiTT").Value == "1" && (string)hdd.Element("trangthai").Value == "0"
                            select new
                            {
                                tongtien= (int)hdd.Element("tongtienmua"), 
                                pmh = (int)hdd.Element("sudungPMH")
                            }).ToList();

                foreach(var j in node)
                {
                    //diem = diem + (j.tongtien-j.pmh )/ 10000;
                    //MessageBox.Show("diem: " + i.ma + "ff" + diem);
                }
                //MessageBox.Show("diem: " + i.ma+"fhvyf"+diem);
                XElement Node1 = testXML_KH.Descendants("khachhang").Where(x => x.Attribute("id").Value.Equals(i.ma.ToString())).FirstOrDefault();
                Node1.Element("diemtichluy").Value = (int.Parse(Node1.Element("diemtichluy").Value) + diemcong).ToString();
                Node1.Element("diemconlai").Value = (int.Parse(Node1.Element("diemconlai").Value) + diemcong).ToString();
                XElement node2 = testXML_HD.Descendants("hoadon").Where(x => x.Attribute("id").Value.Equals(i.mahd.ToString())).FirstOrDefault();
                node2.Element("trangthai").Value = 1.ToString();
                testXML_KH.Save(path3);
                testXML_HD.Save(path2);
            }
        }

        public void NangCapThe()
        {
            var Node = (from kh in testXML_KH.Descendants("khachhang")
                        //where (string)kh.Element("trangthai") == 0.ToString()
                        select new
                        {
                            ma = (int)kh.Attribute("id"),
                            loai = (int)kh.Attribute("loaikh"),
                            diem = (int)kh.Element("diemtichluy")

                        }).ToList();
            //MessageBox.Show("soluong" + Node.Count);
            foreach (var i in Node)
            {
                //MessageBox.Show("Tiền" + i.diem);
                XElement node2 = testXML_KH.Descendants("khachhang").Where(x => x.Attribute("id").Value.Equals(i.ma.ToString())).FirstOrDefault();
                int diem = int.Parse(node2.Element("diemtichluy").Value);
                if (diem >= 1000 && node2.Attribute("loaikh").Value=="1")
                {
                    node2.Attribute("loaikh").Value = "2";
                    diem = diem - 1000;

                    if (diem >= 2000)
                    {
                        node2.Attribute("loaikh").Value = "3";
                        diem = diem - 2000;
                    }
                }
                node2.Element("diemtichluy").Value = diem.ToString();
                testXML_KH.Save(path3);
            }
        }

        public void NangCapKimCuong()
        {
            var Node = (from kh in testXML_KH.Descendants("khachhang")
                            //where (string)kh.Element("trangthai") == 0.ToString()
                        select new
                        {
                            ma = (int)kh.Attribute("id"),
                            loai = (int)kh.Attribute("loaikh")
                            //diem = (int)kh.Element("diemtichluy")

                        }).ToList();
            //MessageBox.Show("soluong" + Node.Count);
            foreach (var i in Node)
            {
                //MessageBox.Show("Tiền" + i.diem);
                var node1 = (from ls in testXML_LS.Descendants("lichsu")
                             where (string)ls.Element("idkh") == i.ma.ToString()
                             select new
                             {
                                 //loai = (int)kh.Element("idloai"),
                                 tien = (int)ls.Element("tongtien"),
                                 ngay = (string)ls.Element("ngaymua")
                             }).ToList();
                //MessageBox.Show("sl: " + node1.Count);
                DateTime ngayHD, ngayQK;
                TimeSpan time1, time2;
                DateTime ngayHT = DateTime.Now;
                string day = (DateTime.Now.Day).ToString();
                string month = (DateTime.Now.Month).ToString();
                int year = int.Parse((DateTime.Now.Year).ToString()) - 2;
                string ngay = day + "-" + month + "-" + year.ToString();
                int tien = 0;
                foreach (var j in node1)
                {
                    ngayHD = DateTime.Parse(j.ngay);
                    ngayQK = DateTime.Parse(ngay);
                    time1 = (ngayHD).Subtract(ngayQK);
                    time2 = ngayHT.Subtract(ngayHD);
                    if (time1.Days > 0 && time2.Days > 0)
                    {
                        tien = tien + j.tien;
                    }

                }
                //MessageBox.Show("tiền: " + tien);
                XElement node2 = testXML_KH.Descendants("khachhang").Where(x => x.Attribute("id").Value.Equals(i.ma.ToString())).FirstOrDefault();
                if (i.loai == 3 && tien >= 1000000)
                {
                    node2.Attribute("loaikh").Value = "4";
                }
                testXML_KH.Save(path3);
            }
        }
    }
}

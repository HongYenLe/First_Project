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
    class DoiQuaBLL
    {
        XDocument testXML = XDocument.Load("..\\..\\Data\\KHACH_HANG.xml");
        XDocument testXML_QT = XDocument.Load("..\\..\\Data\\QUATANG_UUDAI.xml");
        string path = "..\\..\\Data\\QUATANG_UUDAI.xml";
        string path1 = "..\\..\\Data\\KHACH_HANG.xml";
        public void DoiQua()
        {
            var node = (from kh in testXML.Descendants("khachhang")
                        //where (string)kh.Attribute("id") == makh.ToString()
                        select new
                        {
                            ma = (int)kh.Attribute("id"),
                            diem = (int)kh.Element("diemconlai"),
                            diemNT = (int)kh.Element("diemnamtruoc"),
                            //diemsd = (int)kh.Element("diemsudung"),
                            loaikh = (int)kh.Attribute("loaikh")
                        }).ToList();
             

            int t = testXML_QT.Descendants("quatang_uudai").Count();
           // MessageBox.Show("HONGYEN" + node.Count);
            foreach (var i in node)
            {
                //MessageBox.Show("HONGYEN" + i.ma+" "+i.loaikh+" "+i.diem);
                if (i.loaikh == 1)
                {
                    //var node1 = testXML.Descendants("khachhang").Where(x => x.Attribute("id").Value.Equals(makh)).FirstOrDefault();
                    if (i.diem >= 100)
                    {

                        XElement newStudent = new XElement("quatang_uudai",
                            new XElement("tenqt", "Phiếu mua hàng"),
                            new XElement("noidung", "Chúc mừng quý khách nhận được " + (i.diem / 100) + " PMH 20000"),
                            new XElement("trangthai", 0)
                            );
                        var lastStudent = testXML_QT.Descendants("quatang_uudai").Last();
                        newStudent.SetAttributeValue("id", t + 1);
                        newStudent.SetAttributeValue("idkh", i.ma);
                        testXML_QT.Element("QUATANG_UUDAI").Add(newStudent);
                        testXML_QT.Save(path);

                        var node1 = testXML.Descendants("khachhang").Where(x => x.Attribute("id").Value.Equals(i.ma.ToString())).FirstOrDefault();
                        node1.Element("diemconlai").Value = (i.diem - ((i.diem / 100) * 100)).ToString();
                        testXML.Save(path1);
                    }
                }
            }
        }

        //========================Đổi quà PMH 100k==================================
        public void DoiQua100()
        {
            var node = (from kh in testXML.Descendants("khachhang")
                        where (string)kh.Attribute("id") =="100000"
                        select new
                        {
                            ma = (int)kh.Attribute("id"),
                            diemNT = (int)kh.Element("diemnamtruoc"),
                            diemconlai = (int)kh.Element("diemconlai"),
                            loaikh = (int)kh.Attribute("loaikh")
                        }).ToList();

            int t = testXML_QT.Descendants("quatang_uudai").Count();
           // MessageBox.Show("HONGYEN" + t);
            int diem = 0;
            int diem1 = 0;
            foreach (var i in node)
            {
                if (i.loaikh != 1)
                {
                    
                    diem = i.diemNT + i.diemconlai;
                    MessageBox.Show(diem.ToString());
                    if (diem >= 500)
                    {
                        XElement newStudent = new XElement("quatang_uudai",
                                new XElement("tenqt", "Phiếu mua hàng"),
                                new XElement("noidung", "Chúc mừng quý khách nhận được " + (diem / 500).ToString() + " PMH 100.000"),
                                new XElement("soluong", (diem / 500).ToString()),
                                new XElement("tientuongung",100000),
                                new XElement("trangthai", 0)
                                );
                        var lastStudent = testXML_QT.Descendants("quatang_uudai").Last();
                        newStudent.SetAttributeValue("id", t + 1);
                        newStudent.SetAttributeValue("idkh", i.ma);
                        testXML_QT.Element("QUATANG_UUDAI").Add(newStudent);
                        testXML_QT.Save(path);
                        
                        var node1 = (from kh in testXML.Descendants("khachhang")
                                         //where (string)kh.Attribute("id").Value==makh
                                     select new
                                     {
                                         diemconlai = (int)kh.Element("diemconlai"),
                                         //diemsudung = (int)kh.Element("diemsudung"),
                                         diemnamtruoc = (int)kh.Element("diemnamtruoc")
                                     }).FirstOrDefault();

                        var node2 = testXML.Descendants("khachhang").FirstOrDefault();
                        node2.Element("diemnamtruoc").Value = "0";
                        node2.Element("diemconlai").Value = ((node1.diemconlai + node1.diemnamtruoc) - ((node1.diemconlai + node1.diemnamtruoc) / 100) * 100).ToString();

                        testXML.Save(path1);

                        
                    }
                    diem = diem - (diem / 500) * 500;
                    MessageBox.Show("diem1:" + diem);
                    if (diem >= 100)
                    {
                        XElement newStudent1 = new XElement("quatang_uudai",
                            new XElement("tenqt", "Phiếu mua hàng"),
                            new XElement("noidung", "Chúc mừng quý khách nhận được " + (diem / 100).ToString() + "  PMH 20.000"),
                            new XElement("soluong", (diem / 100).ToString()),
                            new XElement("tientuongung", 20000),
                            new XElement("trangthai", 0)
                            );
                        var lastStudent1 = testXML_QT.Descendants("quatang_uudai").Last();
                        newStudent1.SetAttributeValue("id", t + 1);
                        newStudent1.SetAttributeValue("idkh", i.ma);
                        testXML_QT.Element("QUATANG_UUDAI").Add(newStudent1);
                        testXML_QT.Save(path);

                        var node3 = (from kh in testXML.Descendants("khachhang")
                                         //where (string)kh.Attribute("id").Value==makh
                                     select new
                                     {
                                         diemconlai = (int)kh.Element("diemconlai"),
                                         //diemsudung = (int)kh.Element("diemsudung"),
                                         diemnamtruoc = (int)kh.Element("diemnamtruoc")
                                     }).FirstOrDefault();

                        var node4 = testXML.Descendants("khachhang").FirstOrDefault();
                        node4.Element("diemnamtruoc").Value = "0";
                        node4.Element("diemconlai").Value = ((node3.diemconlai + node3.diemnamtruoc) - ((node3.diemconlai + node3.diemnamtruoc) / 500) * 100).ToString();

                        testXML.Save(path1);
                    }
                }
                else
                {
                    diem = i.diemNT + i.diemconlai;
                    if (diem >= 100)
                    {
                        XElement newStudent = new XElement("quatang_uudai",
                            new XElement("tenqt", "Phiếu mua hàng"),
                            new XElement("noidung", "Chúc mừng quý khách nhận được PMH " + ((diem / 100) * 20000).ToString()),
                            new XElement("trangthai", 0)
                            );
                        var lastStudent = testXML_QT.Descendants("quatang_uudai").Last();
                        newStudent.SetAttributeValue("id", t + 1);
                        newStudent.SetAttributeValue("idkh", i.ma);
                        testXML_QT.Element("QUATANG_UUDAI").Add(newStudent);
                        testXML_QT.Save(path);

                        var node1 = (from kh in testXML.Descendants("khachhang")
                                         //where (string)kh.Attribute("id").Value==makh
                                     select new
                                     {
                                         diemconlai = (int)kh.Element("diemconlai"),
                                         //diemsudung = (int)kh.Element("diemsudung"),
                                         diemnamtruoc = (int)kh.Element("diemnamtruoc")
                                     }).FirstOrDefault();

                        var node2 = testXML.Descendants("khachhang").FirstOrDefault();
                        node2.Element("diemnamtruoc").Value = "0";
                        node2.Element("diemconlai").Value = ((node1.diemconlai + node1.diemnamtruoc) - ((node1.diemconlai + node1.diemnamtruoc) / 100) * 100).ToString();

                        testXML.Save(path1);
                    }
                }
                
            }
        }

        //=============================luu ddiem 2 nam========================
        public void LuuDiem()
        {
            string ngaythang = (DateTime.Now.Day.ToString()) + "-" + (DateTime.Now.Month.ToString());
            var Node = (from kh in testXML.Descendants("khachhang")
                        select new
                        {
                            id = (int)kh.Attribute("id"),
                            diem = (int)kh.Element("diemtichluy"),
                            diemcon = (int)kh.Element("diemconlai"),
                            diemthg = (int)kh.Element("diemthuong"),
                            diemNT = (int)kh.Element("diemnamtruoc")
                        }).ToList();
            //MessageBox.Show("Node: "+Node.Count);
            //MessageBox.Show("Node:htfhft " + ngaythang);
            if (ngaythang == "1-1")
            {
                foreach(var i in Node)
                {
                    //MessageBox.Show("Node: " + i.diemcon);
                    XElement node = testXML.Descendants("khachhang").Where(x => x.Attribute("id").Value.Equals(i.id.ToString())).FirstOrDefault();
                    node.Element("diemtichluy").Value = "0";
                    
                    node.Element("diemthuong").Value = "0";
                    node.Element("diemnamtruoc").Value = i.diemcon.ToString();
                    node.Element("diemconlai").Value = "0";
                    testXML.Save(path1);
                }
            }
        }
        public void DoiMatKhau(string mathe,string mkcu, string mkmoi)
        {
            var Node = testXML.Descendants("khachhang").Where(x => x.Attribute("id").Value.Equals(mathe)).FirstOrDefault();
            if (Node.Element("matkhau").Value == mkcu)
            {
                Node.Element("matkhau").Value = mkmoi;
                testXML.Save(path1);
            }
            else
                MessageBox.Show("Mật khẩu không đúng");
        }
    }
}

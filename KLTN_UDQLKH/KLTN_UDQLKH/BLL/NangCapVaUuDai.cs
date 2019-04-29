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
    class NangCapVaUuDai
    {
        XDocument testXML = XDocument.Load("..\\..\\Data\\KHACH_HANG.xml");
        XDocument testXML_QT = XDocument.Load("..\\..\\Data\\QUATANG_UUDAI.xml");
        XDocument testXML_HD = XDocument.Load("..\\..\\Data\\HOA_DON.xml");
        string path = "..\\..\\Data\\QUATANG_UUDAI.xml";
        public void NangCapThe()
        {
            var Node = (from kh in testXML.Descendants("khachhang")
                        select new
                        {
                            diem=int.Parse((string)kh.Element("diemtichluy"))
                        }).ToList();
            //int diemTL;
            //string loaikh="";
            //foreach(var i in Node)
            //{
            //    diemTL = i.diem;
            //    Console.WriteLine("Diem "+diemTL);

            //    if(diemTL >= 0 && diemTL <= 599)
            //    {
            //        loaikh = "ffdf";
            //    }
            //}
        }

        //---------------------quà sinh nhật------------------------------
        public void QuaSinhNhat()
        {
            //MessageBox.Show("đygdgy" + DateTime.Now.ToString("dd-MM-yyyy").Substring(3,2));
            int ngaysinh = DateTime.Now.Day;
            int thangsinh = DateTime.Now.Month;
            var Node = (from kh in testXML.Descendants("khachhang")
                        where int.Parse(kh.Element("ngaysinh").Value.Substring(0,2)) <= ngaysinh && int.Parse(kh.Element("ngaysinh").Value.Substring(3,2)) == thangsinh && kh.Attribute("loaikh").Value!="1"
                        select new
                        {
                            makh = (int)kh.Attribute("id")
                        }).ToList();
            
            
            foreach(var i in Node)
            {
                var node1 = (from qt in testXML_QT.Descendants("quatang_uudai")
                             where (string)qt.Attribute("idkh").Value == i.makh.ToString() && (string)qt.Element("tenqt").Value == "Quà sinh nhật"
                             select new
                             {
                                 id = (int)qt.Attribute("id")
                             }).ToList();
                int k = node1.Count;

                int t = testXML_QT.Descendants("quatang_uudai").Count();
                    if (k == 0)
                    {
                        XElement newStudent = new XElement("quatang_uudai",
                        new XElement("tenqt", "Quà sinh nhật"),
                        new XElement("noidung", "Happy Birthday"),
                        new XElement("soluong", DateTime.Now.ToString("dd-MM-yyyy", CultureInfo.InvariantCulture)),
                        new XElement("tientuongung",DateTime.Now.AddMonths(6).ToString("dd-MM-yyyy", CultureInfo.InvariantCulture)),
                        new XElement("trangthai", 0)

                        );
                        var lastStudent = testXML_QT.Descendants("quatang_uudai").Last();
                        newStudent.SetAttributeValue("id", t+1);
                        newStudent.SetAttributeValue("idkh", i.makh);
                        testXML_QT.Element("QUATANG_UUDAI").Add(newStudent);
                        testXML_QT.Save(path);
                    }   
                //MessageBox.Show("gvfdxbh" + i.makh+"dshd"+k);
            }
        }

        //----------------------------- QUÀ TẾT------------------------------------------
        public void QuaTet()
        {
            string yearStart = "01-1-" + (int.Parse(DateTime.Now.ToString("dd-M-yyyy").ToString().Substring(5, 4)) - 1).ToString();
            //MessageBox.Show("" + yearStart);
            string yearEnd = "01-1-" + DateTime.Now.ToString("dd-M-yyyy").ToString().Substring(5, 4);
            //MessageBox.Show("" + yearEnd);
            DateTime dtStart = DateTime.Parse(yearStart);
            DateTime dtEnd = DateTime.Parse(yearEnd);

            string ngaysinh = DateTime.Now.ToString("dd-M-yyyy");


            /*if (yearEnd == DateTime.Now.ToString("dd-M-yyyy"))*/ //khi ngày truy cập là ngày đầu năm sau thì mới thực hiện kiểm tra dk
            if (DateTime.Now.ToString("dd-M-yyyy")=="30-3-2019")
            {
                var Node = (from hd in testXML_HD.Descendants("hoadon")
                            //join hd in testXML_HD.Descendants("hoadon")
                            //on (string)kh.Attribute("id") equals (string)hd.Attribute("idkh")
                            where int.Parse(hd.Element("ngaymua").Value.ToString().Substring(5, 4)) >= 2019 && int.Parse(hd.Element("ngaymua").Value.ToString().Substring(5, 4)) < 2020
                            select new
                            {
                                makh = (int)hd.Attribute("idkh"),
                                //diem = (int)kh.Element("diemtichluy"),
                                tongtien = (int)hd.Element("tongtienmua"),
                                ngay = (string)hd.Element("ngaymua")
                            }).ToList();
                //MessageBox.Show("đygdgy" + Node.Count);
                int t = testXML_QT.Descendants("quatang_uudai").Count();
                int diem = 0;
                foreach (var i in Node)
                {
                    diem = i.tongtien/10000;
                    //MessageBox.Show("aaaaa" + diem);
                    //MessageBox.Show("aaaaa" + i.ngay);
                    if (diem >= 600)
                    {
                       
                        XElement newStudent = new XElement("quatang_uudai",
                        new XElement("tenqt", "Quà tết"),
                        new XElement("noidung", "Chúc mừng năm mới"),
                        new XElement("trangthai", 0)
                        );
                        var lastStudent = testXML_QT.Descendants("quatang_uudai").Last();
                        newStudent.SetAttributeValue("id", t + 1);
                        newStudent.SetAttributeValue("idkh", i.makh);
                        testXML_QT.Element("QUATANG_UUDAI").Add(newStudent);
                        testXML_QT.Save(path);
                    }
                }

                //foreach (var i in Node)
                //{
                //var node1 = (from qt in testXML_QT.Descendants("quatang_uudai")
                //             where (string)qt.Attribute("idkh").Value == i.makh.ToString() && (string)qt.Element("tenqt").Value == "Quà tết"
                //             select new
                //             {
                //                 id = (int)qt.Attribute("id")
                //             }).ToList();
                //int k = node1.Count;

                //int t = testXML_QT.Descendants("quatang_uudai").Count();
                //if (k == 0)
                //{
                //XElement newStudent = new XElement("quatang_uudai",
                //new XElement("tenqt", "Quà tết"),
                //new XElement("noidung", "Chúc mừng năm mới"),
                //new XElement("trangthai", 0)
                //);
                //var lastStudent = testXML_QT.Descendants("quatang_uudai").Last();
                //newStudent.SetAttributeValue("id", t + 1);
                //newStudent.SetAttributeValue("idkh", i.makh);
                //testXML_QT.Element("QUATANG_UUDAI").Add(newStudent);
                //testXML_QT.Save(path);
                //}
                //MessageBox.Show("gvfdxbh" + i.makh + "dshd" + k);
                //}
            }
        }

        public void XemUuDai(ListView lv)
        {
            try
            {
                lv.Items.Clear();
                DataSet dataSet = new DataSet();
                DataTable dt = new DataTable();
                dataSet.ReadXml("..\\..\\Data\\QUATANG_UUDAI.xml");
                dt = dataSet.Tables["quatang_uudai"];
                int i = 0;
                foreach (DataRow dr in dt.Rows)
                {
                    lv.Items.Add(dr["id"].ToString());
                    lv.Items[i].SubItems.Add(dr["idkh"].ToString());
                    lv.Items[i].SubItems.Add(dr["tenqt"].ToString());
                    lv.Items[i].SubItems.Add(dr["noidung"].ToString());
                    lv.Items[i].SubItems.Add(dr["soluong"].ToString());
                    lv.Items[i].SubItems.Add(dr["tientuongung"].ToString());
                    lv.Items[i].SubItems.Add(dr["trangthai"].ToString());
                    
                    i++;
                }
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }
    }
}

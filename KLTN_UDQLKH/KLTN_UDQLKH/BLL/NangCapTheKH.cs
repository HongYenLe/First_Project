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


namespace KLTN_UDQLKH.BLL
{
    class NangCapTheKH
    {

        public string path = "..\\..\\Data\\KHACH_HANG.xml";
        XDocument testXML_KH = XDocument.Load("..\\..\\Data\\KHACH_HANG.xml");

        public void nangcap()
        {
            var Node = (from kh in testXML_KH.Descendants("khachhang")
                        select new
                        {
                            ten = (string)kh.Element("tendn"),
                            diem = (int)kh.Element("diemtichluy")
                        }).ToList();
            string _tendn = "";
            int _diemtl = 0;
            int j = 2;
            foreach (var i in Node)
            {
                
                _tendn = i.ten;
                _diemtl = i.diem;

                XElement Node1 = testXML_KH.Descendants("khachhang").Where(x => x.Element("tendn").Value.Equals(_tendn)).FirstOrDefault();
                
                if (_diemtl>=600 && _diemtl <1600)
                {
                    Node1.Attribute("loaikh").Value = 2.ToString();
                }
                if (_diemtl >= 1600)
                {
                    Node1.Attribute("loaikh").Value = 2.ToString();
                }
                if (_diemtl >= 5000)
                {
                    Node1.Attribute("loaikh").Value = 1.ToString();
                }
                testXML_KH.Save(path);
            }
        }

        public void CapNhatDiem()
        {

        }
    }
}

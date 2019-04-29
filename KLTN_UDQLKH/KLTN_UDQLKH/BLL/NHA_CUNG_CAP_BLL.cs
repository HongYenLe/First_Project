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
    class NHA_CUNG_CAP_BLL
    {
        string path = "..\\..\\Data\\NHA_CUNG_CAP.xml";
        XDocument testXML = XDocument.Load("..\\..\\Data\\NHA_CUNG_CAP.xml");
        public void Load(ListView lv)  // load dữ liệu từ file vào listView
        {
            //getNamefromID();
            try
            {
                lv.Items.Clear();
                DataSet dataSet = new DataSet();
                DataTable dt = new DataTable();
                dataSet.ReadXml("..\\..\\Data\\NHA_CUNG_CAP.xml");
                dt = dataSet.Tables["nhacungcap"];
                int i = 0;
                foreach (DataRow dr in dt.Rows)
                {
                    lv.Items.Add(dr["id"].ToString());
                    lv.Items[i].SubItems.Add(dr["tenncc"].ToString());
                    lv.Items[i].SubItems.Add(dr["dienthoai"].ToString());
                    lv.Items[i].SubItems.Add(dr["diachi"].ToString());
                    i++;
                }
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }

        //-------------------Thêm nhà cung cấp---------------------------
        public void ThemNCC(NHA_CUNG_CAP_DTO ncc, ListView lv)  // hàm thêm từ lớp kh sang file xml
        {
            try
            {
                var count = testXML.Descendants("nhacungcap").Count();
                XElement newStudent = new XElement("nhacungcap",
                    new XElement("tenncc", ncc.tenncc),
                    new XElement("dienthoai", ncc.dienthoai),
                    new XElement("diachi", ncc.diachi)
                    );
                var lastStudent = testXML.Descendants("nhacungcap").Last();
                newStudent.SetAttributeValue("id", count + 1);
                testXML.Element("NHACUNGCAP").Add(newStudent);
                testXML.Save(path);
                Load(lv);
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }

        //=---------------------------sửa----------------------------------
        public void SuaNCC(NHA_CUNG_CAP_DTO ncc, ListView lv) //sửa thông tin
        {

            try
            {
                XElement Node = testXML.Descendants("nhacungcap").Where(c => c.Attribute("id").Value.Equals(ncc.id.ToString())).FirstOrDefault();
                Node.Element("tenncc").Value = ncc.tenncc;
                Node.Element("dienthoai").Value = ncc.dienthoai;
                Node.Element("diachi").Value = ncc.diachi;
                testXML.Save(path);
                Load(lv);
            }
            catch (Exception err)
            {
                MessageBox.Show("Lỗi sửa thông tin:" + err.Message);
            }
        }

        //--------------------------------xóa------------------------------------
        public void XoaNCC(int Id, ListView lv) // xóa node tại id="Id"
        {
            try
            {
                XElement Node = testXML.Descendants("nhacungcap")
                    .Where(c => c.Attribute("id").Value.Equals(Id.ToString())).FirstOrDefault();
                Node.Remove();
                testXML.Save(path);
                Load(lv);
            }
            catch (Exception err)
            {
                MessageBox.Show(err.Message);
            }
        }
    }
}

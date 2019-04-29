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
    class SAN_PHAM_BLL
    {
        public string path = "..\\..\\Data\\SAN_PHAM.xml";
        XDocument testXML = XDocument.Load("..\\..\\Data\\SAN_PHAM.xml");
        XDocument testXML_LoaiSP = XDocument.Load("..\\..\\Data\\LOAI_SAN_PHAM.xml");
        XDocument testXML_NCC = XDocument.Load("..\\..\\Data\\NHA_CUNG_CAP.xml");
        XDocument Join = XDocument.Load("..\\..\\Data\\SP_LOAISP.xml");
        //--------------------------------------------------------
        public void Load(ListView lv)  // load dữ liệu từ file vào listView
        {
            getNamefromID();
            try
            {
                lv.Items.Clear();
                DataSet dataSet = new DataSet();
                DataTable dt = new DataTable();
                dataSet.ReadXml("..\\..\\Data\\SP_LOAISP.xml");
                dt = dataSet.Tables["Join"];
                int i = 0;
                foreach (DataRow dr in dt.Rows)
                {
                    lv.Items.Add(dr["id"].ToString());
                    lv.Items[i].SubItems.Add(dr["tensp"].ToString());
                    lv.Items[i].SubItems.Add(dr["dongia"].ToString());
                    lv.Items[i].SubItems.Add(dr["donvitinh"].ToString());
                    lv.Items[i].SubItems.Add(dr["giamgia"].ToString());
                    lv.Items[i].SubItems.Add(dr["soluong"].ToString());
                    lv.Items[i].SubItems.Add(dr["tenloai"].ToString());
                    lv.Items[i].SubItems.Add(dr["ncc"].ToString());
                    i++;
                }
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }

        //-------------------Thêm sản phẩm---------------------------
        public void ThemSp(SAN_PHAM_DTO sp, ListView lv)  // hàm thêm từ lớp kh sang file xml
        {
            try
            {
                var count = testXML.Descendants("sanpham").Count();
                XElement newStudent = new XElement("sanpham",
                    new XElement("tensp", sp.tensp),
                    new XElement("dongia", sp.dongia),
                    new XElement("donvitinh", sp.donvitinh),
                    new XElement("giamgia", sp.giamgia),
                    new XElement("soluong", sp.soluong)
                    );
                var lastStudent = testXML.Descendants("sanpham").Last();
                newStudent.SetAttributeValue("id", count + 1);
                newStudent.SetAttributeValue("idloai", sp.idloai);
                newStudent.SetAttributeValue("idncc", sp.idncc);
                testXML.Element("SANPHAM").Add(newStudent);
                testXML.Save(path);
                Load(lv);
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }

        //-------------------------------Sửa sản phẩm----------------------------------
        public void SuaSP(SAN_PHAM_DTO sp, ListView lv) //sửa thông tin
        {
            try
            {
                XElement Node = testXML.Descendants("sanpham").Where(c => c.Attribute("id").Value.Equals(sp.id.ToString())).FirstOrDefault();
                Node.Element("tensp").Value = sp.tensp;
                Node.Element("dongia").Value = sp.dongia.ToString();
                Node.Element("donvitinh").Value = sp.donvitinh;
                Node.Element("giamgia").Value = sp.giamgia.ToString();
                Node.Element("soluong").Value = sp.soluong.ToString();
                Node.Attribute("idloai").Value = sp.idloai.ToString();
                Node.Attribute("idncc").Value = sp.idncc.ToString(); 
                testXML.Save(path);
                Load(lv);
            }
            catch (Exception err)
            {
                MessageBox.Show("Lỗi sửa thông tin:" + err.Message);
            }
        }

        //------------------------Xóa sản phẩm-------------------------------------
        public void XoaSP(int Id, ListView lv) // xóa node tại id="Id"
        {
            try
            {
                XElement Node = testXML.Descendants("sanpham")
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

        //---------------------load du lieu len combobox----------------------------------------
        public void LoadComboBoxloaiSP(ComboBox cbx) //LoadCBX(tên loại sp)
        {
            try
            {
                XmlDocument doc = new XmlDocument();
                doc.Load("..\\..\\Data\\LOAI_SAN_PHAM.xml");
                DataSet dataset = new DataSet();
                DataTable dt = new DataTable();
                dataset.ReadXml("..\\..\\Data\\LOAI_SAN_PHAM.xml");
                dt = dataset.Tables["loaisp"];
                cbx.DataSource = dt;
                cbx.DisplayMember = "tenloai";
                cbx.ValueMember = "id";
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }

        public void LoadComboBoxNCC(ComboBox cbx) //LoadCBX
        {
            try
            {
                XmlDocument doc = new XmlDocument();
                doc.Load("..\\..\\Data\\NHA_CUNG_CAP.xml");
                DataSet dataset = new DataSet();
                DataTable dt = new DataTable();
                dataset.ReadXml("..\\..\\Data\\NHA_CUNG_CAP.xml");
                dt = dataset.Tables["nhacungcap"];
                cbx.DataSource = dt;
                cbx.DisplayMember = "tenncc";
                cbx.ValueMember = "id";
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }

        //---------------------lien ket du lieu----------------------------------------
        public void getNamefromID()// tạo liên kết 2 bảng
        {
            var root = new XElement("Root",
                        from sp in testXML.Descendants("sanpham")
                        join loai in testXML_LoaiSP.Descendants("loaisp")
                        on (string)sp.Attribute("idloai") equals (string)loai.Attribute("id")
                        join ncc in testXML_NCC.Descendants("nhacungcap")
                        on (string)sp.Attribute("idncc") equals (string)ncc.Attribute("id")
                        select new XElement(
                             "Join",
                             new XElement("id", (int)sp.Attribute("id")),
                             new XElement("tensp", (string)sp.Element("tensp")),
                             new XElement("dongia", (int)sp.Element("dongia")),
                             new XElement("donvitinh", (string)sp.Element("donvitinh")),
                             new XElement("giamgia", (int)sp.Element("giamgia")),
                             new XElement("soluong", (int)sp.Element("soluong")),
                             new XElement("tenloai", (string)loai.Element("tenloai")),
                             new XElement("ncc", (string)ncc.Element("tenncc"))
                        ));
            root.Save("..\\..\\Data\\SP_LOAISP.xml");

        }

        public void TimTheoID(string maCanTim, ListView lv)  // tìm theo ID
        {
            try
            {
                lv.Items.Clear();
                DataSet dataSet = new DataSet();
                DataTable dt = new DataTable();
                dataSet.ReadXml("..\\..\\Data\\SP_LOAISP.xml");
                dt = dataSet.Tables["Join"];
                int co = 0;
                int i = 0;
                foreach (DataRow dr in dt.Rows)
                {
                    if (dr["id"].ToString() == maCanTim)
                    {
                        lv.Items.Add(dr["id"].ToString());
                        lv.Items[i].SubItems.Add(dr["tensp"].ToString());
                        lv.Items[i].SubItems.Add(dr["dongia"].ToString());
                        lv.Items[i].SubItems.Add(dr["donvitinh"].ToString());
                        lv.Items[i].SubItems.Add(dr["giamgia"].ToString());
                        lv.Items[i].SubItems.Add(dr["soluong"].ToString());
                        lv.Items[i].SubItems.Add(dr["tenloai"].ToString());
                        lv.Items[i].SubItems.Add(dr["ncc"].ToString());
                        co++;
                        i++;
                    }

                }
                if (co == 0)
                {
                    MessageBox.Show("Không tìm thấy khách hàng với mã khách hàng trên");
                }
            }
            catch (Exception e)
            {
                MessageBox.Show("Lỗi: " + e.Message);
            }
        }

        public void TimTheoTen(string ten, ListView lv)
        {
            try
            {
                lv.Items.Clear();
                DataSet dataSet = new DataSet();
                DataTable dt = new DataTable();
                dataSet.ReadXml("..\\..\\Data\\SP_LOAISP.xml");
                dt = dataSet.Tables["Join"];
                int co = 0;
                int i = 0;
                foreach (DataRow dr in dt.Rows)
                {
                    if (dr["tensp"].ToString().Contains(ten))
                    {
                        lv.Items.Add(dr["id"].ToString());
                        lv.Items[i].SubItems.Add(dr["tensp"].ToString());
                        lv.Items[i].SubItems.Add(dr["dongia"].ToString());
                        lv.Items[i].SubItems.Add(dr["donvitinh"].ToString());
                        lv.Items[i].SubItems.Add(dr["giamgia"].ToString());
                        lv.Items[i].SubItems.Add(dr["soluong"].ToString());
                        lv.Items[i].SubItems.Add(dr["tenloai"].ToString());
                        lv.Items[i].SubItems.Add(dr["ncc"].ToString());
                        co++;
                        i++;
                    }

                }
                if (co == 0)
                    MessageBox.Show("Không tìm thấy");
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }

        public void TimTheoLoai(string loai, ListView lv)
        {
            try
            {
                lv.Items.Clear();
                DataSet dataSet = new DataSet();
                DataTable dt = new DataTable();
                dataSet.ReadXml("..\\..\\Data\\SP_LOAISP.xml");
                dt = dataSet.Tables["Join"];
                int co = 0;
                int i = 0;
                foreach (DataRow dr in dt.Rows)
                {
                    if (dr["tenloai"].ToString().Contains(loai))
                    {
                        lv.Items.Add(dr["id"].ToString());
                        lv.Items[i].SubItems.Add(dr["tensp"].ToString());
                        lv.Items[i].SubItems.Add(dr["dongia"].ToString());
                        lv.Items[i].SubItems.Add(dr["donvitinh"].ToString());
                        lv.Items[i].SubItems.Add(dr["giamgia"].ToString());
                        lv.Items[i].SubItems.Add(dr["soluong"].ToString());
                        lv.Items[i].SubItems.Add(dr["tenloai"].ToString());
                        lv.Items[i].SubItems.Add(dr["ncc"].ToString());
                        co++;
                        i++;
                    }

                }
                if (co == 0)
                    MessageBox.Show("Không tìm thấy");
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }
    }
}

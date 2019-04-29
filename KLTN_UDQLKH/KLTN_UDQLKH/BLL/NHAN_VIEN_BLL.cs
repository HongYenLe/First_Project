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
    class NHAN_VIEN_BLL
    {
        public string path = "..\\..\\Data\\NHAN_VIEN.xml";
        XDocument testXML = XDocument.Load("..\\..\\Data\\NHAN_VIEN.xml");
        //XDocument testXML_LoaiKH = XDocument.Load("..\\..\\Data\\LOAI_KHACH_HANG.xml");
        //XDocument Join = XDocument.Load("..\\..\\Data\\KH_LOAIKH.xml");
        //--------------------------------------------------------
        public void Load(ListView lv)  // load dữ liệu từ file vào listView
        {
            getNamefromID();
            try
            {
                lv.Items.Clear();
                DataSet dataSet = new DataSet();
                DataTable dt = new DataTable();
                dataSet.ReadXml("..\\..\\Data\\NHAN_VIEN.xml");
                dt = dataSet.Tables["nhanvien"];
                int i = 0;
                foreach (DataRow dr in dt.Rows)
                {
                    lv.Items.Add(dr["id"].ToString());
                    lv.Items[i].SubItems.Add(dr["tennv"].ToString());
                    lv.Items[i].SubItems.Add(dr["gioitinh"].ToString());
                    lv.Items[i].SubItems.Add(dr["ngaysinh"].ToString());
                    lv.Items[i].SubItems.Add(dr["cmnd"].ToString());
                    lv.Items[i].SubItems.Add(dr["diachi"].ToString());
                    lv.Items[i].SubItems.Add(dr["dienthoai"].ToString());
                    i++;
                }
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }

        public void Them(NHAN_VIEN_DTO nv, ListView lv)  // hàm thêm từ lớp kh sang file xml
        {
            try
            {
                var count = testXML.Descendants("nhanvien").Count();
                MessageBox.Show(""+count);
                XElement newStudent = new XElement("nhanvien",
                    new XElement("tennv", nv.tennv),
                    new XElement("gioitinh", nv.gioitinh),
                    new XElement("ngaysinh", nv.ngaysinh),
                    new XElement("cmnd", nv.cmnd),
                    new XElement("diachi", nv.diachi),
                    new XElement("dienthoai", nv.dienthoai),
                    new XElement("tendn", nv.tendn),
                    new XElement("matkhau", nv.matkhau)
                    );
                var lastStudent = testXML.Descendants("nhanvien").Last();
                newStudent.SetAttributeValue("id", count + 1);
                newStudent.SetAttributeValue("idquyen", 1);
                testXML.Element("NHANVIEN").Add(newStudent);
                testXML.Save(path);
                Load(lv);
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }

        public void Sua(NHAN_VIEN_DTO nv, ListView lv) //sửa thông tin
        {

            try
            {
                XElement Node = testXML.Descendants("nhanvien").Where(c => c.Attribute("id").Value.Equals(nv.id.ToString())).FirstOrDefault();
                Node.Element("tennv").Value = nv.tennv;
                Node.Element("gioitinh").Value = nv.gioitinh;
                Node.Element("ngaysinh").Value = nv.ngaysinh;
                Node.Element("cmnd").Value = nv.cmnd;
                Node.Element("diachi").Value = nv.diachi;
                Node.Element("dienthoai").Value = nv.dienthoai;
                //Node.Element("tendn").Value = kh.tendn;
                //Node.Element("matkhau").Value = kh.matkhau;

                testXML.Save(path);
                Load(lv);
            }
            catch (Exception err)
            {
                MessageBox.Show("Lỗi sửa thông tin:" + err.Message);
            }
        }

        public void Xoa(int Id, ListView lv) // xóa node tại id="Id"
        {
            try
            {
                XElement Node = testXML.Descendants("nhanvien")
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

        public void TimTheoID(string maCanTim, ListView lv)  // tìm theo ID
        {
            try
            {
                lv.Items.Clear();
                DataSet dataSet = new DataSet();
                DataTable dt = new DataTable();
                dataSet.ReadXml("..\\..\\Data\\NHAN_VIEN.xml");
                dt = dataSet.Tables["NHANVIEN"];
                int co = 0;
                int i = 0;
                foreach (DataRow dr in dt.Rows)
                {
                    if (dr["id"].ToString().Contains(maCanTim))
                    {
                        lv.Items.Add(dr["id"].ToString());
                        lv.Items[i].SubItems.Add(dr["tennv"].ToString());
                        lv.Items[i].SubItems.Add(dr["gioitinh"].ToString());
                        lv.Items[i].SubItems.Add(dr["ngaysinh"].ToString());
                        lv.Items[i].SubItems.Add(dr["cmnd"].ToString());
                        lv.Items[i].SubItems.Add(dr["diachi"].ToString());
                        lv.Items[i].SubItems.Add(dr["dienthoai"].ToString());
                        co++;
                        i++;
                    }

                }
                if (co == 0)
                {
                    MessageBox.Show("Không tìm thấy nhân viên với mã nhân viên trên");
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
                dataSet.ReadXml("..\\..\\Data\\NHAN_VIEN.xml");
                dt = dataSet.Tables["NHANVIEN"];
                int co = 0;
                int i = 0;
                foreach (DataRow dr in dt.Rows)
                {
                    if (dr["tennv"].ToString().Contains(ten))
                    {
                        lv.Items.Add(dr["id"].ToString());
                        lv.Items[i].SubItems.Add(dr["tennv"].ToString());
                        lv.Items[i].SubItems.Add(dr["gioitinh"].ToString());
                        lv.Items[i].SubItems.Add(dr["ngaysinh"].ToString());
                        lv.Items[i].SubItems.Add(dr["cmnd"].ToString());
                        lv.Items[i].SubItems.Add(dr["diachi"].ToString());
                        lv.Items[i].SubItems.Add(dr["dienthoai"].ToString());
                        co++;
                        i++;
                    }

                }
                if (co == 0)
                    MessageBox.Show("Không tìm thấy nhân viên với tên nhân viên trên");
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }


        public bool IsExist(int ma)  // kiểm tra mã nhập vào có tồn tại?  
        {
            XElement cStudent = testXML.Descendants("nhanvien").Where(c => c.Attribute("id").Value.Equals(ma)).FirstOrDefault();
            if (cStudent != null)
                return true;
            return false;
        }

       
        public void getNamefromID()// tạo liên kết 2 bảng
        {
            //var root = new XElement("Root",
            //            from c in testXML.Descendants("khachhang")
            //            join k in testXML_LoaiKH.Descendants("loaikh")
            //            on (string)c.Attribute("loaikh") equals (string)k.Attribute("id")
            //            select new XElement(
            //                 "Join",
            //                 new XElement("id", (int)c.Attribute("id")),
            //                 new XElement("tenkh", (string)c.Element("tenkh")),
            //                 new XElement("gioitinh", (string)c.Element("gioitinh")),
            //                 new XElement("ngaysinh", (string)c.Element("ngaysinh")),
            //                 new XElement("cmnd", (string)c.Element("cmnd")),
            //                 new XElement("diachi", (string)c.Element("diachi")),
            //                 new XElement("dienthoai", (string)c.Element("dienthoai")),
            //                 new XElement("diemtichluy", (int)c.Element("diemtichluy")),
            //                 new XElement("diemconlai", (int)c.Element("diemconlai")),
            //                 new XElement("diemthuong", (int)c.Element("diemthuong")),
            //                 new XElement("tenloai", (string)k.Element("tenloai"))
            //            ));
            //root.Save("..\\..\\Data\\KH_LOAIKH.xml");

        }
    }
}

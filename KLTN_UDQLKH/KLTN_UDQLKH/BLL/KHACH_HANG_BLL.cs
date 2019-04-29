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
    class KHACH_HANG_BLL
    {
        public string path = "..\\..\\Data\\KHACH_HANG.xml";
        XDocument testXML = XDocument.Load("..\\..\\Data\\KHACH_HANG.xml");
        XDocument testXML_LoaiKH = XDocument.Load("..\\..\\Data\\LOAI_KHACH_HANG.xml");
        XDocument Join = XDocument.Load("..\\..\\Data\\KH_LOAIKH.xml");
        //--------------------------------------------------------
        public void Load(ListView lv)  // load dữ liệu từ file vào listView
        {
            getNamefromID();
            try
            {
                lv.Items.Clear();
                DataSet dataSet = new DataSet();
                DataTable dt = new DataTable();
                dataSet.ReadXml("..\\..\\Data\\KH_LOAIKH.xml");
                dt = dataSet.Tables["Join"];
                int i = 0;
                foreach (DataRow dr in dt.Rows)
                {
                    lv.Items.Add(dr["id"].ToString());
                    lv.Items[i].SubItems.Add(dr["tenkh"].ToString());
                    lv.Items[i].SubItems.Add(dr["gioitinh"].ToString());
                    lv.Items[i].SubItems.Add(dr["ngaysinh"].ToString());
                    lv.Items[i].SubItems.Add(dr["cmnd"].ToString());
                    lv.Items[i].SubItems.Add(dr["diachi"].ToString());
                    lv.Items[i].SubItems.Add(dr["dienthoai"].ToString());
                    lv.Items[i].SubItems.Add(dr["diemtichluy"].ToString());
                    lv.Items[i].SubItems.Add(dr["diemconlai"].ToString());
                    lv.Items[i].SubItems.Add(dr["diemthuong"].ToString());
                    lv.Items[i].SubItems.Add(dr["diemnamtruoc"].ToString());
                    //lv.Items[i].SubItems.Add(dr["matkhau"].ToString());
                    lv.Items[i].SubItems.Add(dr["tenloai"].ToString());
                    i++;
                }
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }

        public void Them(KHACH_HANG_DTO kh, ListView lv)  // hàm thêm từ lớp kh sang file xml
        {
            try
            {
                var count = testXML.Descendants("khachhang").Count();
                XElement newStudent = new XElement("khachhang",
                    new XElement("tenkh", kh.tenkh),
                    new XElement("gioitinh", kh.gioitinh),
                    new XElement("ngaysinh", kh.ngaysinh),
                    new XElement("cmnd", kh.cmnd),
                    new XElement("diachi", kh.diachi),
                    new XElement("dienthoai", kh.dienthoai),
                    new XElement("diemtichluy", 0),
                    new XElement("diemconlai", 0),
                    new XElement("diemthuong", 0),
                    new XElement("diemnamtruoc", 0),
                    new XElement("tendn", kh.tendn),
                    new XElement("matkhau", kh.matkhau)
                    );
                var lastStudent = testXML.Descendants("khachhang").Last();
                newStudent.SetAttributeValue("id", count+100000);
                newStudent.SetAttributeValue("loaikh", 1);
                newStudent.SetAttributeValue("idquyen", 2);
                testXML.Element("KHACHHANG").Add(newStudent);
                testXML.Save(path);
                Load(lv);
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }

        public void Sua(KHACH_HANG_DTO kh, ListView lv) //sửa thông tin
        {

            try
            {
                XElement Node = testXML.Descendants("khachhang").Where(c => c.Attribute("id").Value.Equals(kh.id.ToString())).FirstOrDefault();
                Node.Element("tenkh").Value = kh.tenkh;
                Node.Element("gioitinh").Value = kh.gioitinh;
                Node.Element("ngaysinh").Value = kh.ngaysinh;  
                Node.Element("cmnd").Value = kh.cmnd;
                Node.Element("diachi").Value = kh.diachi;
                Node.Element("dienthoai").Value = kh.dienthoai;
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
        public void SuaKH(KHACH_HANG_DTO kh)
        {
            try
            {
                XElement Node = testXML.Descendants("khachhang").Where(c => c.Attribute("id").Value.Equals(kh.id.ToString())).FirstOrDefault();
                Node.Element("tenkh").Value = kh.tenkh;
                Node.Element("gioitinh").Value = kh.gioitinh;
                Node.Element("ngaysinh").Value = kh.ngaysinh;
                Node.Element("cmnd").Value = kh.cmnd;
                Node.Element("diachi").Value = kh.diachi;
                Node.Element("dienthoai").Value = kh.dienthoai;

                testXML.Save(path);

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
                XElement Node = testXML.Descendants("khachhang")
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
                dataSet.ReadXml("..\\..\\Data\\KH_LOAIKH.xml");
                dt = dataSet.Tables["Join"];
                int co = 0;
                int i = 0;
                foreach (DataRow dr in dt.Rows)
                {
                    if (dr["id"].ToString().Contains(maCanTim))
                    {
                        lv.Items.Add(dr["id"].ToString());
                        lv.Items[i].SubItems.Add(dr["tenkh"].ToString());
                        lv.Items[i].SubItems.Add(dr["gioitinh"].ToString());
                        lv.Items[i].SubItems.Add(dr["ngaysinh"].ToString());
                        lv.Items[i].SubItems.Add(dr["cmnd"].ToString());
                        lv.Items[i].SubItems.Add(dr["diachi"].ToString());
                        lv.Items[i].SubItems.Add(dr["dienthoai"].ToString());
                        lv.Items[i].SubItems.Add(dr["diemtichluy"].ToString());
                        lv.Items[i].SubItems.Add(dr["diemconlai"].ToString());
                        lv.Items[i].SubItems.Add(dr["diemthuong"].ToString());
                        lv.Items[i].SubItems.Add(dr["diemnamtruoc"].ToString());
                        lv.Items[i].SubItems.Add(dr["tenloai"].ToString());
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
                MessageBox.Show("Lỗi: "+e.Message);
            }
        }

        public void TimTheoTen(string ten, ListView lv)
        {
            try
            {
                lv.Items.Clear();
                DataSet dataSet = new DataSet();
                DataTable dt = new DataTable();
                dataSet.ReadXml("..\\..\\Data\\KH_LOAIKH.xml");
                dt = dataSet.Tables["Join"];
                int co = 0;
                int i = 0;
                foreach (DataRow dr in dt.Rows)
                {
                    if (dr["tenkh"].ToString().Contains(ten))
                    {
                        lv.Items.Add(dr["id"].ToString());
                        lv.Items[i].SubItems.Add(dr["tenkh"].ToString());
                        lv.Items[i].SubItems.Add(dr["gioitinh"].ToString());
                        lv.Items[i].SubItems.Add(dr["ngaysinh"].ToString());
                        lv.Items[i].SubItems.Add(dr["cmnd"].ToString());
                        lv.Items[i].SubItems.Add(dr["diachi"].ToString());
                        lv.Items[i].SubItems.Add(dr["dienthoai"].ToString());
                        lv.Items[i].SubItems.Add(dr["diemtichluy"].ToString());
                        lv.Items[i].SubItems.Add(dr["diemconlai"].ToString());
                        lv.Items[i].SubItems.Add(dr["diemthuong"].ToString());
                        lv.Items[i].SubItems.Add(dr["diemnamtruoc"].ToString());
                        lv.Items[i].SubItems.Add(dr["tenloai"].ToString());
                        co++;
                        i++;
                    }

                }
                if (co == 0)
                    MessageBox.Show("Không tìm thấy khách hàng với tên khách hàng trên");
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }

        //public void TimTheoDiem(string dk, int so, ListView lv)
        //{
        //    int co = 0;
        //    lv.Items.Clear();
        //    DataSet dataSet = new DataSet();
        //    DataTable dt = new DataTable();
        //    dataSet.ReadXml("..\\..\\Data\\KH_LOAIKH.xml");
        //    dt = dataSet.Tables["Join"];
        //    int i = 0;
        //    foreach (DataRow dr in dt.Rows)
        //    {
        //        if (dk == ">")
        //        {
        //            if (int.Parse(dr["diemtichluy"].ToString()) > so)
        //            {
        //                lv.Items.Add(dr["id"].ToString());
        //                lv.Items[i].SubItems.Add(dr["tenkh"].ToString());
        //                lv.Items[i].SubItems.Add(dr["gioitinh"].ToString());
        //                lv.Items[i].SubItems.Add(dr["ngaysinh"].ToString());
        //                lv.Items[i].SubItems.Add(dr["cmnd"].ToString());
        //                lv.Items[i].SubItems.Add(dr["diachi"].ToString());
        //                lv.Items[i].SubItems.Add(dr["dienthoai"].ToString());
        //                lv.Items[i].SubItems.Add(dr["diemtichluy"].ToString());
        //                lv.Items[i].SubItems.Add(dr["diemconlai"].ToString());
        //                lv.Items[i].SubItems.Add(dr["diemthuong"].ToString());
        //                lv.Items[i].SubItems.Add(dr["diemnamtruoc"].ToString());
        //                lv.Items[i].SubItems.Add(dr["tenloai"].ToString());
        //                co++;
        //                i++;
        //            }
        //        }
        //        if (dk == "<")
        //        {
        //            if (int.Parse(dr["diemtichluy"].ToString()) < so)
        //            {
        //                lv.Items.Add(dr["id"].ToString());
        //                lv.Items[i].SubItems.Add(dr["tenkh"].ToString());
        //                lv.Items[i].SubItems.Add(dr["gioitinh"].ToString());
        //                lv.Items[i].SubItems.Add(dr["ngaysinh"].ToString());
        //                lv.Items[i].SubItems.Add(dr["cmnd"].ToString());
        //                lv.Items[i].SubItems.Add(dr["diachi"].ToString());
        //                lv.Items[i].SubItems.Add(dr["dienthoai"].ToString());
        //                lv.Items[i].SubItems.Add(dr["diemtichluy"].ToString());
        //                lv.Items[i].SubItems.Add(dr["diemconlai"].ToString());
        //                lv.Items[i].SubItems.Add(dr["diemthuong"].ToString());
        //                lv.Items[i].SubItems.Add(dr["diemnamtruoc"].ToString());
        //                lv.Items[i].SubItems.Add(dr["tenloai"].ToString());
        //                co++;
        //                i++;
        //            }
        //        }
        //        if (dk == "=")
        //        {
        //            if (int.Parse(dr["diemtichluy"].ToString()) == so)
        //            {
        //                lv.Items.Add(dr["id"].ToString());
        //                lv.Items[i].SubItems.Add(dr["tenkh"].ToString());
        //                lv.Items[i].SubItems.Add(dr["gioitinh"].ToString());
        //                lv.Items[i].SubItems.Add(dr["ngaysinh"].ToString());
        //                lv.Items[i].SubItems.Add(dr["cmnd"].ToString());
        //                lv.Items[i].SubItems.Add(dr["diachi"].ToString());
        //                lv.Items[i].SubItems.Add(dr["dienthoai"].ToString());
        //                lv.Items[i].SubItems.Add(dr["diemtichluy"].ToString());
        //                lv.Items[i].SubItems.Add(dr["diemconlai"].ToString());
        //                lv.Items[i].SubItems.Add(dr["diemthuong"].ToString());
        //                lv.Items[i].SubItems.Add(dr["diemnamtruoc"].ToString());
        //                lv.Items[i].SubItems.Add(dr["tenloai"].ToString());
        //                co++;
        //                i++;
        //            }
        //        }
        //    }
        //    if (co == 0)
        //    {
        //        MessageBox.Show("Không tìm thấy!");
        //        return;
        //    }
        //}

        public void TimTheoLoai(string loai, ListView lv)
        {
            try
            {
                lv.Items.Clear();
                DataSet dataSet = new DataSet();
                DataTable dt = new DataTable();
                dataSet.ReadXml("..\\..\\Data\\KH_LOAIKH.xml");
                dt = dataSet.Tables["Join"];
                int co = 0;
                int i = 0;
                foreach (DataRow dr in dt.Rows)
                {
                    if (dr["tenloai"].ToString().Contains(loai))
                    {
                        lv.Items.Add(dr["id"].ToString());
                        lv.Items[i].SubItems.Add(dr["tenkh"].ToString());
                        lv.Items[i].SubItems.Add(dr["gioitinh"].ToString());
                        lv.Items[i].SubItems.Add(dr["ngaysinh"].ToString());
                        lv.Items[i].SubItems.Add(dr["cmnd"].ToString());
                        lv.Items[i].SubItems.Add(dr["diachi"].ToString());
                        lv.Items[i].SubItems.Add(dr["dienthoai"].ToString());
                        lv.Items[i].SubItems.Add(dr["diemtichluy"].ToString());
                        lv.Items[i].SubItems.Add(dr["diemconlai"].ToString());
                        lv.Items[i].SubItems.Add(dr["diemthuong"].ToString());
                        lv.Items[i].SubItems.Add(dr["diemnamtruoc"].ToString());
                        lv.Items[i].SubItems.Add(dr["tenloai"].ToString());
                        co++;
                        i++;
                    }

                }
                if (co == 0)
                    MessageBox.Show("Không tìm thấy khách hàng với loại khách hàng trên");
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }

        public bool IsExist(int ma)  // kiểm tra mã nhập vào có tồn tại?  
        {
            XElement cStudent = testXML.Descendants("khachhang").Where(c => c.Attribute("id").Value.Equals(ma)).FirstOrDefault();
            if (cStudent != null)
                return true;
            return false;
        }

        //public void LoadDataToCollection(TextBox txtName1)
        //{

        //    try
        //    {
        //        AutoCompleteStringCollection auto = new AutoCompleteStringCollection();
        //        XmlDocument doc = new XmlDocument();
        //        doc.Load("..\\..\\Customer.xml");
        //        XmlNodeList nameList = doc.SelectNodes("Customers/Customer/Name");
        //        foreach (XmlNode Name in nameList)  //duyện qua các nút con
        //        {
        //            auto.Add(Name.InnerText);  // load vào cbx
        //        }
        //        txtName1.AutoCompleteMode = AutoCompleteMode.Suggest;
        //        txtName1.AutoCompleteSource = AutoCompleteSource.CustomSource;
        //        txtName1.AutoCompleteCustomSource = auto;
        //    }
        //    catch (Exception e)
        //    {
        //        MessageBox.Show(e.Message);
        //    }
        //}

        public void LoadComboBox(ComboBox cbx) //LoadCBX(tên loại khách hàng)
        {
            try
            {
                XmlDocument doc = new XmlDocument();
                doc.Load("..\\..\\Data\\LOAI_KHACH_HANG.xml");
                DataSet dataset = new DataSet();
                DataTable dt = new DataTable();
                dataset.ReadXml("..\\..\\Data\\LOAI_KHACH_HANG.xml");
                dt = dataset.Tables["loaikh"];
                cbx.DataSource = dt;
                cbx.DisplayMember = "tenloai";
                cbx.ValueMember = "id";
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }


        public void getNamefromID()// tạo liên kết 2 bảng
        {
            var root = new XElement("Root",
                        from c in testXML.Descendants("khachhang")
                        join k in testXML_LoaiKH.Descendants("loaikh")
                        on (string)c.Attribute("loaikh") equals (string)k.Attribute("id")
                        select new XElement(
                             "Join",
                             new XElement("id", (int)c.Attribute("id")),
                             new XElement("tenkh", (string)c.Element("tenkh")),
                             new XElement("gioitinh", (string)c.Element("gioitinh")),
                             new XElement("ngaysinh", (string)c.Element("ngaysinh")),
                             new XElement("cmnd", (string)c.Element("cmnd")),
                             new XElement("diachi", (string)c.Element("diachi")),
                             new XElement("dienthoai", (string)c.Element("dienthoai")),
                             new XElement("diemtichluy", (int)c.Element("diemtichluy")),
                             new XElement("diemconlai", (int)c.Element("diemconlai")),
                             new XElement("diemthuong", (int)c.Element("diemthuong")),
                             new XElement("diemnamtruoc", (int)c.Element("diemnamtruoc")),
                             new XElement("tenloai", (string)k.Element("tenloai"))
                        ));
            root.Save("..\\..\\Data\\KH_LOAIKH.xml");

        }

        
    }
}

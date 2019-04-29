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
    class ThongKe
    {
        public void LoadComboBoxLoaiKH(ComboBox cbx) //LoadCBX(tên loại khách hàng)
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

        XDocument testXML = XDocument.Load("..\\..\\Data\\LOAI_KHACH_HANG.xml");
        XDocument testXML_KH = XDocument.Load("..\\..\\Data\\KHACH_HANG.xml");
        XDocument testTKLoai = XDocument.Load("..\\..\\Data\\thongkeloai.xml");
        string pathtkloai = "..\\..\\Data\\thongkeloai.xml";
        public int Thongke_theoLoaiKH(int id)
        {
            var Node = (from loai in testXML.Descendants("loaikh")
                        join kh in testXML_KH.Descendants("khachhang")
                        on (string)loai.Attribute("id") equals (string)kh.Attribute("loaikh")
                        where (string)loai.Attribute("id") == id.ToString()
                        select new
                        {
                            makh = int.Parse((string)kh.Attribute("id")),
                        }).ToList();
            
            int kq = Node.Count;
            return kq;
        }


        public void Hienthi_TKLoaiKH(ListView lv,int maCanTim)
        {
            try
            {
                lv.Items.Clear();
                DataSet dataSet = new DataSet();
                DataTable dt = new DataTable();
                dataSet.ReadXml("..\\..\\Data\\KHACH_HANG.xml");
                dt = dataSet.Tables["KHACHHANG"];
                int i = 0;
                foreach (DataRow dr in dt.Rows)
                {
                    if (dr["loaikh"].ToString() == maCanTim.ToString())
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
                        i++;
                    }
                    
                }
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }
        

        //===============================Cập nhật bảng thống kê loại======================================
        public void CapNhatLoai()
        {
            var node1 = testTKLoai.Descendants("tkloai").Where(x=>x.Attribute("idloai").Value.Equals("1")).FirstOrDefault();
            node1.Element("soluong").Value = Thongke_theoLoaiKH(1).ToString();

            var node2 = testTKLoai.Descendants("tkloai").Where(x => x.Attribute("idloai").Value.Equals("2")).FirstOrDefault();
            node2.Element("soluong").Value = Thongke_theoLoaiKH(2).ToString();

            var node3 = testTKLoai.Descendants("tkloai").Where(x => x.Attribute("idloai").Value.Equals("3")).FirstOrDefault();
            node3.Element("soluong").Value = Thongke_theoLoaiKH(3).ToString();

            var node4 = testTKLoai.Descendants("tkloai").Where(x => x.Attribute("idloai").Value.Equals("4")).FirstOrDefault();
            node4.Element("soluong").Value = Thongke_theoLoaiKH(4).ToString();
            testTKLoai.Save(pathtkloai);
        }
        //-------------------------------thong ke theo ngay sinh-------------------------------------

        public int Thongke_theoNgaysinh(string ngay)
        {
            var Node = (from kh in testXML_KH.Descendants("khachhang")
                        where (string)("Tháng "+DateTime.Parse(kh.Element("ngaysinh").Value).Month) == ngay
                        select new
                        {
                            makh = int.Parse((string)kh.Attribute("id")),
                        }).ToList();

            int kq = Node.Count;
            return kq;
        }

        public void Hienthi_TKtheoNgaySinh(ListView lv, string ngay)
        {
            try
            {
                lv.Items.Clear();
                DataSet dataSet = new DataSet();
                DataTable dt = new DataTable();
                dataSet.ReadXml("..\\..\\Data\\KHACH_HANG.xml");
                dt = dataSet.Tables["KHACHHANG"];
                int i = 0;
                foreach (DataRow dr in dt.Rows)
                {
                    //DateTime ns = DateTime.Parse(dr["ngaysinh"].ToString());
                    string ngay1 = "Tháng "+(DateTime.Parse(dr["ngaysinh"].ToString()).Month).ToString();
                    if (ngay1 == ngay)
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
                        i++;
                    }

                }
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }

        //-------------------------------thong ke theo giới tính-------------------------------------

        public int Thongke_theogt(string gt)
        {
            var Node = (from kh in testXML_KH.Descendants("khachhang")
                        where (string)kh.Element("gioitinh") == gt.ToString()
                        select new
                        {
                            makh = int.Parse((string)kh.Attribute("id")),
                        }).ToList();

            int kq = Node.Count;
            return kq;
        }

        public void Hienthi_TKtheogt(ListView lv, string gt)
        {
            try
            {
                lv.Items.Clear();
                DataSet dataSet = new DataSet();
                DataTable dt = new DataTable();
                dataSet.ReadXml("..\\..\\Data\\KHACH_HANG.xml");
                dt = dataSet.Tables["KHACHHANG"];
                int i = 0;
                foreach (DataRow dr in dt.Rows)
                {
                    if (dr["gioitinh"].ToString() == gt)
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
                        i++;
                    }

                }
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }

        //-------------------------------thong ke theo địa chỉ-------------------------------------

        public int Thongke_theoDC(string dc)
        {
            var Node = (from kh in testXML_KH.Descendants("khachhang")
                        where ((string)kh.Element("diachi")).Contains(dc.ToString())
                        select new
                        {
                            makh = int.Parse((string)kh.Attribute("id")),
                        }).ToList();

            int kq = Node.Count;
            return kq;
        }

        public void Hienthi_TKtheoDC(ListView lv, string dc)
        {
            try
            {
                lv.Items.Clear();
                DataSet dataSet = new DataSet();
                DataTable dt = new DataTable();
                dataSet.ReadXml("..\\..\\Data\\KHACH_HANG.xml");
                dt = dataSet.Tables["KHACHHANG"];
                int i = 0;
                foreach (DataRow dr in dt.Rows)
                {
                    if (dr["diachi"].ToString().Contains(dc))
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
                        i++;
                    }

                }
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }
    }
}

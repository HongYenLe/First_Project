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
    class HOA_DON_BLL
    {
            public void LoadComboBoxKH(ComboBox cbx) //LoadCBX(tên loại khách hàng)
            {
                try
                {
                    XmlDocument doc = new XmlDocument();
                    doc.Load("..\\..\\Data\\KHACH_HANG.xml");
                    DataSet dataset = new DataSet();
                    DataTable dt = new DataTable();
                    dataset.ReadXml("..\\..\\Data\\KHACH_HANG.xml");
                    dt = dataset.Tables["khachhang"];
                    cbx.DataSource = dt;
                    cbx.DisplayMember = "tenkh";
                    cbx.ValueMember = "id";
                }
                catch (Exception e)
                {
                    MessageBox.Show(e.Message);
                }
            }

            public void LoadComboBoxNV(ComboBox cbx) //Load cmb nhân viên
            {
                try
                {
                    XmlDocument doc = new XmlDocument();
                    doc.Load("..\\..\\Data\\NHAN_VIEN.xml");
                    DataSet dataset = new DataSet();
                    DataTable dt = new DataTable();
                    dataset.ReadXml("..\\..\\Data\\NHAN_VIEN.xml");
                    dt = dataset.Tables["nhanvien"];
                    cbx.DataSource = dt;
                    cbx.DisplayMember = "tennv";
                    cbx.ValueMember = "id";
                }
                catch (Exception e)
                {
                    MessageBox.Show(e.Message);
                }
            }

            public string path = "..\\..\\Data\\HOA_DON.xml";
            XDocument testXML = XDocument.Load("..\\..\\Data\\HOA_DON.xml");
            //XDocument testXML_LoaiKH = XDocument.Load("..\\..\\Data\\LOAI_KHACH_HANG.xml");
            //XDocument Join = XDocument.Load("..\\..\\Data\\KH_LOAIKH.xml");
            //--------------------------------------------------------
            public void Load(ListView lv)  // load dữ liệu từ file vào listView
            {
                //getNamefromID();
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
                        lv.Items.Add(dr["id"].ToString());
                        lv.Items[i].SubItems.Add(dr["idkh"].ToString());
                        lv.Items[i].SubItems.Add(dr["tongtienmua"].ToString());
                        lv.Items[i].SubItems.Add(dr["ngaymua"].ToString());
                        i++;
                    }
                }
                catch (Exception e)
                {
                    MessageBox.Show(e.Message);
                }
            }
        public void TimTheoID(int maCanTim, ListView lv)  // tìm theo ID
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
                    if (dr["id"].ToString() == maCanTim.ToString())
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
                    MessageBox.Show("Không tìm thấy chi tiết mã hóa đơn trên");
                }
            }
            catch (Exception e)
            {
                MessageBox.Show("Lỗi: " + e.Message);
            }
        }

        public void ThemHD(HOA_DON_DTO hd, ListView lv)  // hàm thêm từ lớp kh sang file xml
        {
            try
            {
                var count = testXML.Descendants("hoadon").Count();
                MessageBox.Show("" + count);
                XElement newStudent = new XElement("hoadon",
                    new XElement("tongtienmua", 0),
                    new XElement("ngaymua", hd.ngaymua),
                    new XElement("tienkhachtra", 0),
                    new XElement("tiengiam", 0),
                    new XElement("trangthai", 0),
                    new XElement("trangthaiTT", 0),
                    new XElement("sudungPMH",0)
                    );
                var lastStudent = testXML.Descendants("hoadon").Last();
                newStudent.SetAttributeValue("id", count + 1);
                newStudent.SetAttributeValue("idkh", hd.idkh);
                newStudent.SetAttributeValue("idnv", hd.idnv);
                testXML.Element("HOADON").Add(newStudent);
                testXML.Save(path);
                Load(lv);
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }

        public void XoaHD(int Id, ListView lv)
        {
            try
            {
                XElement Node = testXML.Descendants("hoadon")
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

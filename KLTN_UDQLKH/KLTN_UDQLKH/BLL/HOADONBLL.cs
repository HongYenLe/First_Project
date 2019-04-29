using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KLTN_UDQLKH.BLL
{
    class HOADONBLL
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

        public string path = "..\\..\\Data\\NHAN_VIEN.xml";
        XDocument testXML = XDocument.Load("..\\..\\Data\\NHAN_VIEN.xml");
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
    }
}

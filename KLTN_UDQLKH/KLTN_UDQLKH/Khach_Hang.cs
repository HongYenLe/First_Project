using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;
using KLTN_UDQLKH.BLL;
using KLTN_UDQLKH.DTO;
using System.Globalization;

namespace KLTN_UDQLKH
{
    public partial class Khach_Hang : Form
    {
        string tendn;
        public Khach_Hang(string _tendn)
        {
            InitializeComponent();
            tendn = _tendn;

            ColumnHeader columnheader;
            lvuudai.View = View.Details;
            lvuudai.GridLines = true;
            //lvhoadon.FullRowSelect = true;

            columnheader = new ColumnHeader(); // create id column
            columnheader.Width = 100;
            columnheader.Text = "Mã ưu đãi";
            lvuudai.Columns.Add(columnheader);

            columnheader = new ColumnHeader(); // create id column
            columnheader.Width = 100;
            columnheader.Text = "Mã khách hàng";
            lvuudai.Columns.Add(columnheader);

            columnheader = new ColumnHeader(); // create id column
            columnheader.Width = 100;
            columnheader.Text = "Tên ưu đãi";
            lvuudai.Columns.Add(columnheader);

            columnheader = new ColumnHeader(); // create id column
            columnheader.Width = 100;
            columnheader.Text = "Nội dung";
            lvuudai.Columns.Add(columnheader);

            columnheader = new ColumnHeader(); // create id column
            columnheader.Width = 100;
            columnheader.Text = "Trạng thái";
            lvuudai.Columns.Add(columnheader);


            ColumnHeader columnheader5;
            lvLS.View = View.Details;
            lvLS.GridLines = true;
            //lvkhachhang.FullRowSelect = true;

            columnheader5 = new ColumnHeader(); // create id column
            columnheader5.Width = 100;
            columnheader5.Text = "Mã lịch sử";
            lvLS.Columns.Add(columnheader5);

            columnheader5 = new ColumnHeader(); // create id column
            columnheader5.Width = 100;
            columnheader5.Text = "Mã khách hàng";
            lvLS.Columns.Add(columnheader5);

            columnheader5 = new ColumnHeader(); // create id column
            columnheader5.Width = 100;
            columnheader5.Text = "Mã nhân viên";
            lvLS.Columns.Add(columnheader5);

            columnheader5 = new ColumnHeader(); // create id column
            columnheader5.Width = 100;
            columnheader5.Text = "Mã hóa đơn";
            lvLS.Columns.Add(columnheader5);

            columnheader5 = new ColumnHeader(); // create id column
            columnheader5.Width = 150;
            columnheader5.Text = "Tổng thanh toán";
            lvLS.Columns.Add(columnheader5);

            columnheader5 = new ColumnHeader(); // create id column
            columnheader5.Width = 150;
            columnheader5.Text = "Ngày mua hàng";
            lvLS.Columns.Add(columnheader5);

            ctBLL.Loaduudai(lvuudai, tendn);
            //ctBLL.LoadLS(lvLS);

            var node = (from kh in testXML.Descendants("khachhang")
                        where (string)kh.Element("tendn").Value == tendn
                        select new
                        {
                            id = (string)kh.Attribute("id")
                        }).ToList();
            MessageBox.Show("đfff" + node.Count);
            foreach(var i in node)
            {
                LoadLS(lvLS,i.id);
            }
            
        }
        XDocument testLS = XDocument.Load("..\\..\\Data\\LICH_SU.xml");
        XDocument testXML_loai = XDocument.Load("..\\..\\Data\\LOAI_KHACH_HANG.xml");
        XDocument testXML = XDocument.Load("..\\..\\Data\\KHACH_HANG.xml");
        string path = "..\\..\\Data\\KHACH_HANG.xml";
        CT_HOADON_BLL ctBLL = new CT_HOADON_BLL();

        public void LoadLS(ListView lv,string id)  // tìm theo ID
        {
            try
            {
                //lv.Items.Clear();
                DataSet dataSet = new DataSet();
                DataTable dt = new DataTable();
                dataSet.ReadXml("..\\..\\Data\\HOA_DON.xml");
                dt = dataSet.Tables["hoadon"];
                int co = 0;
                int i = 0;
                foreach (DataRow dr in dt.Rows)
                {
                    if (int.Parse(dr["tongtienmua"].ToString()) > 0 && dr["idkh"].ToString() == id)
                    {
                        lv.Items.Add((i + 1).ToString());
                        lv.Items[i].SubItems.Add(dr["idkh"].ToString());
                        lv.Items[i].SubItems.Add(dr["idnv"].ToString());
                        lv.Items[i].SubItems.Add(dr["id"].ToString());
                        lv.Items[i].SubItems.Add(dr["tongtienmua"].ToString());
                        lv.Items[i].SubItems.Add(dr["tienkhachtra"].ToString());
                        lv.Items[i].SubItems.Add(dr["tiengiam"].ToString());

                        lv.Items[i].SubItems.Add(dr["ngaymua"].ToString());
                        if (dr["trangthai"].ToString() == "0")
                        {
                            lv.Items[i].SubItems.Add("Chưa cập nhật điểm");
                        }
                        if (dr["trangthai"].ToString() == "1")
                        {
                            lv.Items[i].SubItems.Add("Đã cập nhật điểm");
                        }
                        if (dr["trangthaiTT"].ToString() == "0")
                        {
                            lv.Items[i].SubItems.Add("Chưa thanh toán");
                        }
                        if (dr["trangthaiTT"].ToString() == "1")
                        {
                            lv.Items[i].SubItems.Add("Đã thanh toán");
                        }
                        co++;
                        i++;
                    }

                }

            }
            catch (Exception e)
            {
                MessageBox.Show("Lỗi: " + e.Message);
            }
        }

        private void tabControl1_Click(object sender, EventArgs e)
        {
            
        }

        private void Khach_Hang_Load(object sender, EventArgs e)
        {
            var Node = (from kh in testXML.Descendants("khachhang")
                        join loai in testXML_loai.Descendants("loaikh")
                        on (string)kh.Attribute("loaikh") equals (string)loai.Attribute("id")
                        where (string)kh.Element("tendn") == (tendn.ToString())
                        select new
                        {
                            id = (int)kh.Attribute("id"),
                            ten = (string)kh.Element("tenkh"),
                            gioitinh = (string)kh.Element("gioitinh"),
                            ngaysinh = (string)kh.Element("ngaysinh"),
                            cmnd = (string)kh.Element("cmnd"),
                            dienthoai = (string)kh.Element("dienthoai"),
                            diachi = (string)kh.Element("diachi"),
                            loaithe = (string)loai.Element("tenloai"),
                            diemconlai = (int)kh.Element("diemconlai"),
                            diemthg = (int)kh.Element("diemthuong"),
                            diemtl = (int)kh.Element("diemtichluy")
                        }).ToList();
            //int _id;
            //string _ten = ""; string  _gioitinh= ""; string _ngaysinh = ""; string _cmnd = "";
            //string _diachi = ""; string _dienthoai = ""; string _loaithe = "";
            foreach (var i in Node)
            {
                lbid.Text = i.id.ToString();
                lbten.Text = i.ten;
                lbgt.Text = i.gioitinh;
                lbngaysinh.Text = i.ngaysinh;
                lbcmnd.Text = i.cmnd;
                lbdiachi.Text = i.diachi;
                lbdt.Text = i.dienthoai;
                lbloaithe.Text = i.loaithe;

                lbdiemconlai.Text = i.diemconlai.ToString();
                lbdiemthuong.Text = i.diemthg.ToString();
                lbdiemtichluy.Text = i.diemtl.ToString();

                // cập nhật thông tin
                lbidkh.Text = i.id.ToString();
                textBoxtenkh.Text = i.ten;
                if(i.gioitinh == "Nam")
                {
                    rb_nam.Checked = true;
                }
                if(i.gioitinh == "Nữ")
                {
                    rb_nu.Checked = true;
                }
                if(i.gioitinh=="Khác")
                {
                    rb_khac.Checked = true;
                }
                dtp_ngaysinh.Value = Convert.ToDateTime(i.ngaysinh);
                textBox3cmnd.Text = i.cmnd;
                textBox4diachi.Text = i.diachi;
                textBox5dienthoai.Text = i.dienthoai;
            }
        }

        KHACH_HANG_DTO khDTO = new KHACH_HANG_DTO();
        KHACH_HANG_BLL khBLL = new KHACH_HANG_BLL();
        public bool layDL_suaKH() //Lấy dữ liệu từ txt
        {
            khDTO.id = Int32.Parse(lbidkh.Text);
            khDTO.tenkh = textBoxtenkh.Text;
            if (DateTime.Now.Year - dtp_ngaysinh.Value.Year >= 6)
            {
                if (DateTime.Now.Year - dtp_ngaysinh.Value.Year < 80)
                {
                    khDTO.ngaysinh = dtp_ngaysinh.Value.Date.ToString("dd-MM-yyyy", CultureInfo.InvariantCulture);  //chuyển datetimepicker values sang string
                }
                else
                {
                    MessageBox.Show("Tuổi của khách hàng phải lớn nhỏ hơn 80!", "THÔNG BÁO", MessageBoxButtons.OK);
                    return false;
                }
            }
            else
            {
                MessageBox.Show("Tuổi của khách hàng phải lớn hơn hoặc bằng 6!", "THÔNG BÁO", MessageBoxButtons.OK);
                return false;
            }
            if (rb_nu.Checked == true)
                khDTO.gioitinh = rb_nu.Text;
            else
            {
                if (rb_nam.Checked == true)
                    khDTO.gioitinh = rb_nam.Text;
                else
                    khDTO.gioitinh = rb_khac.Text;
            }
            khDTO.dienthoai = textBox5dienthoai.Text;
            khDTO.diachi = textBox4diachi.Text;
            khDTO.cmnd = textBox3cmnd.Text;
            return true;
        }

        private void tabItem7_Click(object sender, EventArgs e)
        {
            DOI_MAT_KHAU dmk = new DOI_MAT_KHAU(tendn);
            this.Hide();
            dmk.ShowDialog();
            this.Show();
        }

        private void tabItem8_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void tabControlPanel7_Click(object sender, EventArgs e)
        {

        }
        
        private void bt_sua_Click(object sender, EventArgs e)
        {

            layDL_suaKH();
            khBLL.SuaKH(khDTO);
        }
    }
}

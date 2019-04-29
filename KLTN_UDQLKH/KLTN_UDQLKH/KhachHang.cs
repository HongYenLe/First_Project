using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using KLTN_UDQLKH.BLL;
using KLTN_UDQLKH.DTO;
using System.Globalization;
using System.Xml;
using System.Xml.Linq;
using System.IO;

using Microsoft.Office.Interop.Excel;
using System.Text.RegularExpressions;


namespace KLTN_UDQLKH
{
    public partial class KhachHang : Form
    {
        string tendn;
        public KhachHang(string _tendn)
        {
            InitializeComponent();
            tendn = _tendn;

            var node = (from nv in testXML.Descendants("khachhang")
                        where (string)nv.Element("tendn").Value == tendn
                        select new
                        {
                            ten = (string)nv.Element("tenkh")
                        }).FirstOrDefault();
            label1.Text = node.ten;
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
        }

        private void ribbonTabItem9_Click(object sender, EventArgs e)
        {
            panel1ttkh.BringToFront();
        }
        XDocument testXML_loai = XDocument.Load("..\\..\\Data\\LOAI_KHACH_HANG.xml");
        XDocument testXML = XDocument.Load("..\\..\\Data\\KHACH_HANG.xml");
        private void KhachHang_Load(object sender, EventArgs e)
        {
            var Node = (from kh in testXML.Descendants("khachhang")
                        join loai in testXML_loai.Descendants("loaikh")
                        on (string)kh.Attribute("loaikh") equals (string)loai.Attribute("id")
                        where (string)kh.Element("tendn")==(tendn.ToString())
                        select new
                        {
                            id = (int)kh.Attribute("id"),
                            ten =(string)kh.Element("tenkh"),
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
            }

            
            
        }

       

        private void ribbonTabItemTroGiup_Click(object sender, EventArgs e)
        {
            panelxemdiem.BringToFront();
        }
        CT_HOADON_BLL ctBLL = new CT_HOADON_BLL();
        private void ribbonTabItem10_Click(object sender, EventArgs e)
        {
            panel1_UUDAI.BringToFront();
            ctBLL.Loaduudai(lvuudai,tendn);

        }
        
        private void ribbonTabItemXemLS_Click(object sender, EventArgs e)
        {
            panel1LS.BringToFront();
            ctBLL.LoadLS(lvLS);
        }

        private void ribbonTabItem11doimatkhau_Click(object sender, EventArgs e)
        {
            DOI_MAT_KHAU dmk = new DOI_MAT_KHAU(tendn);
            this.Hide();
            dmk.ShowDialog();
            this.Show();
        }
    }
}

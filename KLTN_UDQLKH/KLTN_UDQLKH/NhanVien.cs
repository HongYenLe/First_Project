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
using System.Xml.Linq;
using System.IO;
//using System.Text;
using Microsoft.Office.Interop.Excel;
using System.Text.RegularExpressions;

namespace KLTN_UDQLKH
{
    public partial class NhanVien : Form
    {
        string tendn;
        public NhanVien(string _tendn)
        {
            InitializeComponent();
            tendn = _tendn;
            var node = (from nv in testXML.Descendants("nhanvien")
                        where (string)nv.Element("tendn").Value == tendn
                        select new
                        {
                            ten = (string)nv.Element("tennv")
                        }).FirstOrDefault();
            label3.Text = node.ten;
            //MessageBox.Show(node.ten);
            
            ColumnHeader columnheader;
            lvkhachhang.View = View.Details;
            lvkhachhang.GridLines = true;
            //lvkhachhang.FullRowSelect = true;

            columnheader = new ColumnHeader(); // create id column
            columnheader.Width = 50;
            columnheader.Text = "Mã KH";
            lvkhachhang.Columns.Add(columnheader);

            columnheader = new ColumnHeader(); //create name customer column
            columnheader.Width = 120;
            columnheader.Text = "Họ tên KH";
            lvkhachhang.Columns.Add(columnheader);

            columnheader = new ColumnHeader(); // create gender customer column
            columnheader.Width = 60;
            columnheader.Text = "Giới tính";
            lvkhachhang.Columns.Add(columnheader);

            columnheader = new ColumnHeader(); //create birthday column
            columnheader.Width = 80;
            columnheader.Text = "Ngày sinh";
            lvkhachhang.Columns.Add(columnheader);

            columnheader = new ColumnHeader(); //create cmnd column
            columnheader.Width = 100;
            columnheader.Text = "Số CMND";
            lvkhachhang.Columns.Add(columnheader);

            columnheader = new ColumnHeader(); //create address column
            columnheader.Width = 150;
            columnheader.Text = "Địa chỉ";
            lvkhachhang.Columns.Add(columnheader);


            columnheader = new ColumnHeader(); //create phone column
            columnheader.Width = 100;
            columnheader.Text = "Số điện thoại";
            lvkhachhang.Columns.Add(columnheader);

            columnheader = new ColumnHeader(); // điểm tích lũy
            columnheader.Width = 100;
            columnheader.Text = "Điểm tích lũy";
            lvkhachhang.Columns.Add(columnheader);

            columnheader = new ColumnHeader(); // điểm mua hàng
            columnheader.Width = 100;
            columnheader.Text = "Điểm mua hàng";
            lvkhachhang.Columns.Add(columnheader);

            columnheader = new ColumnHeader(); // điểm thưởng
            columnheader.Width = 90;
            columnheader.Text = "Điểm thưởng";
            lvkhachhang.Columns.Add(columnheader);

            columnheader = new ColumnHeader(); // điểm năm trước sử dụng chưa hết
            columnheader.Width = 90;
            columnheader.Text = "Điểm còn lại năm trước";
            lvkhachhang.Columns.Add(columnheader);

            columnheader = new ColumnHeader(); // loại khách hàng
            columnheader.Width = 100;
            columnheader.Text = "Loại khách hàng";
            lvkhachhang.Columns.Add(columnheader);

            khBLL.Load(lvkhachhang);  //load file xml


            ColumnHeader columnheader1;
            lvnhanvien.View = View.Details;
            lvnhanvien.GridLines = true;
            //lvkhachhang.FullRowSelect = true;

            columnheader1 = new ColumnHeader(); // create id column
            columnheader1.Width = 50;
            columnheader1.Text = "Mã NV";
            lvnhanvien.Columns.Add(columnheader1);

            columnheader1 = new ColumnHeader(); //create name customer column
            columnheader1.Width = 120;
            columnheader1.Text = "Họ tên NV";
            lvnhanvien.Columns.Add(columnheader1);

            columnheader1 = new ColumnHeader(); // create gender customer column
            columnheader1.Width = 60;
            columnheader1.Text = "Giới tính";
            lvnhanvien.Columns.Add(columnheader1);

            columnheader1 = new ColumnHeader(); //create birthday column
            columnheader1.Width = 80;
            columnheader1.Text = "Ngày sinh";
            lvnhanvien.Columns.Add(columnheader1);

            columnheader1 = new ColumnHeader(); //create cmnd column
            columnheader1.Width = 100;
            columnheader1.Text = "Số CMND";
            lvnhanvien.Columns.Add(columnheader1);

            columnheader1 = new ColumnHeader(); //create address column
            columnheader1.Width = 150;
            columnheader1.Text = "Địa chỉ";
            lvnhanvien.Columns.Add(columnheader1);

            columnheader1 = new ColumnHeader(); //create phone column
            columnheader1.Width = 100;
            columnheader1.Text = "Số điện thoại";
            lvnhanvien.Columns.Add(columnheader1);
            
            nvBLL.Load(lvnhanvien);  //load file xml
            
        }

        KHACH_HANG_BLL khBLL = new KHACH_HANG_BLL();
        KHACH_HANG_DTO khDTO = new KHACH_HANG_DTO();
        SAN_PHAM_BLL spBLL = new SAN_PHAM_BLL();
        SAN_PHAM_DTO spDTO = new SAN_PHAM_DTO();
        NHA_CUNG_CAP_DTO nccDTO = new NHA_CUNG_CAP_DTO();
        NHA_CUNG_CAP_BLL nccBLL = new NHA_CUNG_CAP_BLL();
        //txt_maKH.ReadOnly = false;
        //    txttendn.ReadOnly = false;
        //    txtmk.ReadOnly = false;
        //    khBLL.Load(lvkhachhang);  //load file 

        //    txt_maKH.Clear();
        //    txt_tenKH.Clear();
        //    txt_soDT.Clear();
        //    txt_diachi.Clear();
        //    txt_cmnd.Clear();
        //    rb_nam.Checked = true;
        //    dtp_ngaysinh.Value = DateTime.Now;
        public bool layDL_themKH() //Lấy dữ liệu từ txt
        {
            //khDTO.id = Int32.Parse(txt_maKH.Text);
            khDTO.tenkh = txt_tenKH.Text;
            if (DateTime.Now.Year - dtp_ngaysinh.Value.Year >= 16)
            {
                if (DateTime.Now.Year - dtp_ngaysinh.Value.Year < 120)
                {
                    khDTO.ngaysinh = dtp_ngaysinh.Value.Date.ToString("dd-MM-yyyy", CultureInfo.InvariantCulture);  //chuyển datetimepicker values sang string
                }
                else
                {
                    MessageBox.Show("Tuổi của khách hàng phải nhỏ hơn 120!", "THÔNG BÁO", MessageBoxButtons.OK);
                    return false;
                }
            }
            else
            {
                MessageBox.Show("Tuổi của khách hàng phải lớn hơn hoặc bằng 16!", "THÔNG BÁO", MessageBoxButtons.OK);
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
            khDTO.dienthoai = txt_soDT.Text;
            khDTO.diachi = txt_diachi.Text;
           
            khDTO.cmnd = txt_cmnd.Text;
            khDTO.tendn = txttendn.Text;
            khDTO.matkhau = txtmk.Text;
            return true;
        }

        public bool layDL_suaKH() //Lấy dữ liệu từ txt
        {
            khDTO.id = Int32.Parse(txt_maKH.Text);
            khDTO.tenkh = txt_tenKH.Text;
            if (DateTime.Now.Year - dtp_ngaysinh.Value.Year >= 16)
            {
                if (DateTime.Now.Year - dtp_ngaysinh.Value.Year < 120)
                {
                    khDTO.ngaysinh = dtp_ngaysinh.Value.Date.ToString("dd-MM-yyyy", CultureInfo.InvariantCulture);  //chuyển datetimepicker values sang string
                }
                else
                {
                    MessageBox.Show("Tuổi của khách hàng phải nhỏ hơn 120!", "THÔNG BÁO", MessageBoxButtons.OK);
                    return false;
                }
            }
            else
            {
                MessageBox.Show("Tuổi của khách hàng phải lớn hơn hoặc bằng 16!", "THÔNG BÁO", MessageBoxButtons.OK);
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
            khDTO.dienthoai = txt_soDT.Text;
            
            khDTO.diachi = txt_diachi.Text;
            khDTO.cmnd = txt_cmnd.Text;
            khDTO.tendn = txttendn.Text;
            khDTO.matkhau = txtmk.Text;
            return true;
        }

        private bool IsNull() // kiểm tra thông tin trong textbox 
        {

            if (txt_tenKH.Text == "")
            {
                MessageBox.Show("Bạn chưa nhập tên khách hàng!");
                return false;
            }

            if (txt_diachi.Text == "")
            {
                MessageBox.Show("Bạn chưa nhập địa chỉ khách hàng!");
                return false;
            }
            
            if (txt_soDT.Text == "")
            {
                MessageBox.Show("Bạn chưa nhập số điện thoại!");
                return false;
            }

            return true;
        }
        private void bt_them_Click(object sender, EventArgs e)
        {
            
            if (IsNull() == false)
                return;
            if (!layDL_themKH())
                return;
            if (khBLL.IsExist(khDTO.id) == true)
            {
                MessageBox.Show("Mã khách hàng đã tồn tại!");
                return;
            }
            khBLL.Them(khDTO, lvkhachhang);
            txt_maKH.ReadOnly = true;
            txttendn.ReadOnly = true;
            txtmk.ReadOnly = true;
        }

        private void lvkhachhang_Click(object sender, EventArgs e)
        {
            txt_maKH.ReadOnly = true;
            txttendn.ReadOnly = true;
            txtmk.ReadOnly = true;
            foreach (ListViewItem item in lvkhachhang.SelectedItems)
            {
                txt_maKH.Text = item.SubItems[0].Text;
                txt_tenKH.Text = item.SubItems[1].Text;
                dtp_ngaysinh.Value = Convert.ToDateTime(item.SubItems[3].Text);
                if (item.SubItems[2].Text == "Nam")
                    rb_nam.Checked = true;
                if (item.SubItems[2].Text == "Nữ")
                    rb_nu.Checked = true;
                if (item.SubItems[2].Text == "Khác")
                    rb_khac.Checked = true;
                txt_soDT.Text = item.SubItems[6].Text;
                txt_diachi.Text = item.SubItems[5].Text;
                txt_cmnd.Text = item.SubItems[4].Text;
                //textBoxmail.Text = item.SubItems[7].Text;
                //DateTime dt = DateTime.ParseExact(item.SubItems[7].Text, "MM/dd/yyyy", CultureInfo.InvariantCulture);
                //dtp_ngaymua.Value = dt;

            }
        }

        private void bt_sua_Click(object sender, EventArgs e)
        {
            if (txt_maKH.Text != "")
            {
                if (IsNull() == false)
                    return;
                if (DateTime.Now.Year - dtp_ngaysinh.Value.Year >= 16)
                {
                    if (DateTime.Now.Year - dtp_ngaysinh.Value.Year < 120)
                    {
                        khDTO.ngaysinh = dtp_ngaysinh.Value.Date.ToString("dd-MM-yyyy", CultureInfo.InvariantCulture);  //chuyển datetimepicker values sang string
                    }
                    else
                    {
                        MessageBox.Show("Tuổi của khách hàng phải nhỏ hơn 120!", "THÔNG BÁO", MessageBoxButtons.OK);
                        return;
                    }
                }
                else
                {
                    MessageBox.Show("Tuổi của khách hàng phải lớn hơn hoặc bằng 16!", "THÔNG BÁO", MessageBoxButtons.OK);
                    return;
                }
                layDL_suaKH();
                khBLL.Sua(khDTO, lvkhachhang);

            }
        }

        private void bt_xuat_Click(object sender, EventArgs e)
        {
            try
            {
                using (SaveFileDialog sfd = new SaveFileDialog() { Filter = "Excel Workbook|*.xls", ValidateNames = true })
                {
                    if (sfd.ShowDialog() == DialogResult.OK)
                    {
                        Microsoft.Office.Interop.Excel.Application app = new Microsoft.Office.Interop.Excel.Application();
                        Workbook wb = app.Workbooks.Add(XlSheetType.xlWorksheet);
                        Worksheet ws = (Worksheet)app.ActiveSheet;
                        app.Visible = false;
                        ws.Cells[1, 1] = "Mã KH";
                        ws.Cells[1, 2] = "Tên KH";
                        ws.Cells[1, 3] = "Giới tính";
                        ws.Cells[1, 4] = "Ngày sinh";
                        ws.Cells[1, 5] = "CMND";
                        ws.Cells[1, 6] = "Địa chỉ";
                        ws.Cells[1, 7] = "Điện thoại";
                        //ws.Cells[1, 8] = "Email";
                        ws.Cells[1, 8] = "Điểm tích lũy";
                        ws.Cells[1, 9] = "Điểm mua hàng";
                        ws.Cells[1, 10] = "Điểm thưởng";
                        ws.Cells[1, 11] = "Điểm năm trước";
                        ws.Cells[1, 12] = "Loại khách hàng";
                        int i = 2;
                        foreach (ListViewItem item in lvkhachhang.Items)
                        {
                            ws.Cells[i, 1] = item.SubItems[0].Text;
                            ws.Cells[i, 2] = item.SubItems[1].Text;
                            ws.Cells[i, 3] = item.SubItems[2].Text;
                            ws.Cells[i, 4] = item.SubItems[3].Text;
                            ws.Cells[i, 5] = item.SubItems[4].Text;
                            ws.Cells[i, 6] = item.SubItems[5].Text;
                            ws.Cells[i, 7] = item.SubItems[6].Text;
                            ws.Cells[i, 8] = item.SubItems[7].Text;
                            ws.Cells[i, 9] = item.SubItems[8].Text;
                            ws.Cells[i, 10] = item.SubItems[9].Text;
                            ws.Cells[i, 11] = item.SubItems[10].Text;
                            ws.Cells[i, 12] = item.SubItems[11].Text;
                            //ws.Cells[i, 13] = item.SubItems[12].Text;
                            i++;

                        }
                        ws.SaveAs(sfd.FileName, XlFileFormat.xlWorkbookDefault, Type.Missing, Type.Missing, false, XlSaveAsAccessMode.xlNoChange, XlSaveConflictResolution.xlLocalSessionChanges, Type.Missing, Type.Missing);
                        app.Quit();
                        MessageBox.Show("Xuất file thành công!");
                    }
                }
            }
            catch (Exception err)
            {
                MessageBox.Show(err.Message);
            }
        }

        private void buttonItemTopSaoLuuDuLieu_Click(object sender, EventArgs e)
        {
            panelQUANLYKH.BringToFront();
            panelNHANVIEN.SendToBack();
            
        }

        NHAN_VIEN_BLL nvBLL = new NHAN_VIEN_BLL();
        NHAN_VIEN_DTO nvDTO = new NHAN_VIEN_DTO();
        private void btQLNHANVIEN_Click(object sender, EventArgs e)
        {
            panelNHANVIEN.BringToFront();
            panelQUANLYKH.SendToBack();
            
        }

        public bool layDL_themNV() //Lấy dữ liệu từ txt
        {
            //nvDTO.id = int.Parse(txtmanv.Text);
            nvDTO.tennv = txttennv.Text;
            if (DateTime.Now.Year - dtpngaysinh.Value.Year >= 18)
            {
                if (DateTime.Now.Year - dtpngaysinh.Value.Year < 65)
                {
                    nvDTO.ngaysinh = dtpngaysinh.Value.Date.ToString("dd-MM-yyyy", CultureInfo.InvariantCulture);  //chuyển datetimepicker values sang string
                }
                else
                {
                    MessageBox.Show("Tuổi của khách hàng phải nhỏ hơn 65!", "THÔNG BÁO", MessageBoxButtons.OK);
                    return false;
                }
            }
            else
            {
                MessageBox.Show("Tuổi của khách hàng phải lớn hơn hoặc bằng 18!", "THÔNG BÁO", MessageBoxButtons.OK);
                return false;
            }
            if (rbnu.Checked == true)
                nvDTO.gioitinh = rbnu.Text;
            else
            {
                if (rbnam.Checked == true)
                    nvDTO.gioitinh = rbnam.Text;
                else
                    khDTO.gioitinh = rbkhac.Text;
            }
            nvDTO.dienthoai = txtdt.Text;
            nvDTO.diachi = txtdc.Text;
            nvDTO.cmnd = txtcmnd.Text;
            nvDTO.tendn = txttendangnhap.Text;
            nvDTO.matkhau = textBoxmatkhau.Text;
            return true;
        }
        public bool layDL_suaNV() //Lấy dữ liệu từ txt
        {
            nvDTO.id = int.Parse(txtmanv.Text);
            nvDTO.tennv = txttennv.Text;
            if (DateTime.Now.Year - dtpngaysinh.Value.Year >= 18)
            {
                if (DateTime.Now.Year - dtpngaysinh.Value.Year < 65)
                {
                    nvDTO.ngaysinh = dtpngaysinh.Value.Date.ToString("dd-MM-yyyy", CultureInfo.InvariantCulture);  //chuyển datetimepicker values sang string
                }
                else
                {
                    MessageBox.Show("Tuổi của khách hàng phải nhỏ hơn 65!", "THÔNG BÁO", MessageBoxButtons.OK);
                    return false;
                }
            }
            else
            {
                MessageBox.Show("Tuổi của khách hàng phải lớn hơn hoặc bằng 18!", "THÔNG BÁO", MessageBoxButtons.OK);
                return false;
            }
            if (rbnu.Checked == true)
                nvDTO.gioitinh = rbnu.Text;
            else
            {
                if (rbnam.Checked == true)
                    nvDTO.gioitinh = rbnam.Text;
                else
                    khDTO.gioitinh = rbkhac.Text;
            }
            nvDTO.dienthoai = txtdt.Text;
            nvDTO.diachi = txtdc.Text;
            nvDTO.cmnd = txtcmnd.Text;
            nvDTO.tendn = txttendangnhap.Text;
            nvDTO.matkhau = textBoxmatkhau.Text;
            return true;
        }
        private bool IsNull_NHANVIEN() // kiểm tra thông tin trong textbox 
        {

            if (txttennv.Text == "")
            {
                MessageBox.Show("Bạn chưa nhập tên nhân viên!");
                return false;
            }

            if (txtdc.Text == "")
            {
                MessageBox.Show("Bạn chưa nhập địa chỉ nhân viên!");
                return false;
            }
            if (txtdt.Text == "")
            {
                MessageBox.Show("Bạn chưa nhập số điện thoại!");
                return false;
            }

            return true;
        }
        private void btthem_Click(object sender, EventArgs e)
        {
            
            if (IsNull_NHANVIEN() == false)
                return;
            if (!layDL_themNV())
                return;
            if (nvBLL.IsExist(nvDTO.id) == true)
            {
                MessageBox.Show("Mã nhân viên đã tồn tại!");
              
                return;
            }
            nvBLL.Them(nvDTO, lvnhanvien);

            //txtmanv.ReadOnly = true;
            //txttendangnhap.ReadOnly = true;
            //textBoxmatkhau.ReadOnly = true;
        }

        private void lvnhanvien_Click(object sender, EventArgs e)
        {
            txtmanv.ReadOnly = true;
            txttendangnhap.ReadOnly = true;
            textBoxmatkhau.ReadOnly = true;
            foreach (ListViewItem item in lvnhanvien.SelectedItems)
            {
                txtmanv.Text = item.SubItems[0].Text;
                txttennv.Text = item.SubItems[1].Text;
                dtpngaysinh.Value = Convert.ToDateTime(item.SubItems[3].Text);
                if (item.SubItems[2].Text == "Nam")
                    rbnam.Checked = true;
                if (item.SubItems[2].Text == "Nữ")
                    rbnu.Checked = true;
                if (item.SubItems[2].Text == "Khác")
                    rbkhac.Checked = true;
                txtdc.Text = item.SubItems[5].Text;
                txtdt.Text = item.SubItems[6].Text;
                txtcmnd.Text = item.SubItems[4].Text;
            }
        }

        private void btsua_Click(object sender, EventArgs e)
        {
            if (txtmanv.Text != "")
            {
                if (IsNull_NHANVIEN() == false)
                    return;
                if (DateTime.Now.Year - dtpngaysinh.Value.Year >= 18)
                {
                    if (DateTime.Now.Year - dtpngaysinh.Value.Year < 65)
                    {
                        nvDTO.ngaysinh = dtpngaysinh.Value.Date.ToString("dd-MM-yyyy", CultureInfo.InvariantCulture);  //chuyển datetimepicker values sang string
                    }
                    else
                    {
                        MessageBox.Show("Tuổi của khách hàng phải nhỏ hơn 65!", "THÔNG BÁO", MessageBoxButtons.OK);
                        return;
                    }
                }
                else
                {
                    MessageBox.Show("Tuổi của khách hàng phải lớn hơn hoặc bằng 18!", "THÔNG BÁO", MessageBoxButtons.OK);
                    return;
                }
                layDL_suaNV();
                nvBLL.Sua(nvDTO, lvnhanvien);

            }
        }

        private void btxoa_Click(object sender, EventArgs e)
        {
            DialogResult rs = MessageBox.Show("Xác nhận xóa nhân viên có mã " + txtmanv.Text, "Xác nhận", MessageBoxButtons.OKCancel);
            if (rs == DialogResult.OK)
            {
                layDL_suaNV();
                nvBLL.Xoa(nvDTO.id, lvnhanvien);

                //set default
                txtmanv.Clear();
                txttennv.Clear();
                txtcmnd.Clear();
                txtdc.Clear();
                txtdt.Clear();
                rbnam.Checked = true;
                dtpngaysinh.Value = DateTime.Now;

            }
        }
       
        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                using (SaveFileDialog sfd = new SaveFileDialog() { Filter = "Excel Workbook|*.xls", ValidateNames = true })
                {
                    if (sfd.ShowDialog() == DialogResult.OK)
                    {
                        Microsoft.Office.Interop.Excel.Application app = new Microsoft.Office.Interop.Excel.Application();
                        Workbook wb = app.Workbooks.Add(XlSheetType.xlWorksheet);
                        Worksheet ws = (Worksheet)app.ActiveSheet;
                        app.Visible = false;
                        ws.Cells[1, 1] = "Mã nhân viên";
                        ws.Cells[1, 2] = "Tên nhân viên";
                        ws.Cells[1, 3] = "Giới tính";
                        ws.Cells[1, 4] = "Ngày sinh";
                        ws.Cells[1, 5] = "CMND";
                        ws.Cells[1, 6] = "Địa chỉ";
                        ws.Cells[1, 7] = "Điện thoại";
                        int i = 2;
                        foreach (ListViewItem item in lvkhachhang.Items)
                        {
                            ws.Cells[i, 1] = item.SubItems[0].Text;
                            ws.Cells[i, 2] = item.SubItems[1].Text;
                            ws.Cells[i, 3] = item.SubItems[2].Text;
                            ws.Cells[i, 4] = item.SubItems[3].Text;
                            ws.Cells[i, 5] = item.SubItems[4].Text;
                            ws.Cells[i, 6] = item.SubItems[5].Text;
                            ws.Cells[i, 7] = item.SubItems[6].Text;
                            i++;

                        }
                        ws.SaveAs(sfd.FileName, XlFileFormat.xlWorkbookDefault, Type.Missing, Type.Missing, false, XlSaveAsAccessMode.xlNoChange, XlSaveConflictResolution.xlLocalSessionChanges, Type.Missing, Type.Missing);
                        app.Quit();
                        MessageBox.Show("Successful");
                    }
                }
            }
            catch (Exception err)
            {
                MessageBox.Show(err.Message);
            }
        }

        XDocument testXML = XDocument.Load("..\\..\\Data\\NHAN_VIEN.xml");
        private void NhanVien_Load(object sender, EventArgs e)
        {
            
        }

        private void btmuahang_Click(object sender, EventArgs e)
        {
            MuaHang mh = new MuaHang(tendn);
            this.Hide();
            mh.ShowDialog();
            this.Show();
        }

        private void label27_Click(object sender, EventArgs e)
        {

        }

        //========================= NHÀ CUNG CẤP ===========================================
        private void lvnhacungcap_Click(object sender, EventArgs e)
        {
           
        }

        private void button14themmoi_Click(object sender, EventArgs e)
        {
           
        }

        private void button9them_Click(object sender, EventArgs e)
        {
            
        }

        
        private void button13sua_Click(object sender, EventArgs e)
        {
            
        }

        private void button12xoa_Click(object sender, EventArgs e)
        {
            
        }

        private void button10xuatexcel_Click(object sender, EventArgs e)
        {
           
        }

        //=============================SẢN PHẨM==================================
        private void button8_Click(object sender, EventArgs e)
        {
            
        }
        
        private void button1_Click(object sender, EventArgs e)
        {
            
        }

        private void buttonItem1_Click(object sender, EventArgs e)
        {
            SanPham sp = new SanPham();
            this.Hide();
            sp.ShowDialog();
            this.Show();
        }

        private void lvsanpham_Click(object sender, EventArgs e)
        {
           
        }


        
        private void button7suasp_Click(object sender, EventArgs e)
        {
        }

        private void button6xoasp_Click(object sender, EventArgs e)
        {
            
        }

        private void button4xuatexcel_Click(object sender, EventArgs e)
        {
        }

        CT_HOADON_DTO ctDTO = new CT_HOADON_DTO();
        CT_HOADON_BLL ctBLL = new CT_HOADON_BLL();
        private void buttonItemTopXemlichsu_Click(object sender, EventArgs e)
        {
            LichSu ls = new LichSu();
            this.Hide();
            ls.ShowDialog();
            this.Show();
        }

        private void button4timkiemlichsu_Click(object sender, EventArgs e)
        {
            
        }

        private void panelSP_Paint(object sender, PaintEventArgs e)
        {

        }

        private void buttonItem3_Click(object sender, EventArgs e)
        {
            NhaCungCap ncc = new NhaCungCap();
            this.Hide();
            ncc.ShowDialog();
            this.Show();
        }
        DoiQuaBLL dqBLL = new DoiQuaBLL();
        private void buttonItemTopDoiQua_Click(object sender, EventArgs e)
        {
            //dqBLL.DoiQua100();
            XemQT xqt = new XemQT();
            this.Hide();
            xqt.ShowDialog();
            this.Show();
        }

        private void bt_thoat_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void buttonItem2DoiMK_Click(object sender, EventArgs e)
        {
            DOI_MAT_KHAU dmk = new DOI_MAT_KHAU(tendn);
            this.Hide();
            dmk.ShowDialog();
            this.Show();
        }

        private void txt_soDT_TextChanged(object sender, EventArgs e)
        {
            if (IsNumber(txt_soDT.Text) != true)
            {
                MessageBox.Show("Dữ liệu nhập không hợp lệ, không được nhập ký tự", "Thông báo");
                txt_soDT.Text = "";

            }
        }

        private bool IsNumber(string val)
        {
            if (val != "")
                return Regex.IsMatch(val, @"^[0-9]\d*\.?[0]*$");
            else return true;
        }

        private void txt_cmnd_TextChanged(object sender, EventArgs e)
        {
            if (IsNumber(txt_cmnd.Text) != true)
            {
                MessageBox.Show("Dữ liệu nhập không hợp lệ, không được nhập ký tự", "Thông báo");
                txt_cmnd.Text = "";

            }
        }

        private void ribbonControl1_Click(object sender, EventArgs e)
        {

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(comboBox1.SelectedItem.Equals("Mã thẻ"))
            {
                //lvkhachhang.Clear();
                khBLL.TimTheoID(textBox1timkiem.Text, lvkhachhang);
            }

            if (comboBox1.SelectedItem.Equals("Tên"))
            {
                //lvkhachhang.Clear();
                khBLL.TimTheoTen(textBox1timkiem.Text, lvkhachhang);
            }

            if (comboBox1.SelectedItem.Equals("Loại thẻ"))
            {
                //lvkhachhang.Clear();
                khBLL.TimTheoLoai(textBox1timkiem.Text, lvkhachhang);
            }
        }

        private void textBox1timkiem_Click(object sender, EventArgs e)
        {
            textBox1timkiem.Clear();
        }

        private void ribbonTabItemQuanLyNhanSu_Click(object sender, EventArgs e)
        {

        }

        private void buttonItemMenuThoat_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void textBox1_Click(object sender, EventArgs e)
        {
            textBox1.Clear();
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox2.SelectedItem.Equals("Mã nhân viên"))
            {
                //lvkhachhang.Clear();
                nvBLL.TimTheoID(textBox1.Text, lvnhanvien);
            }

            if (comboBox2.SelectedItem.Equals("Tên nhân viên"))
            {
                //lvkhachhang.Clear();
                nvBLL.TimTheoTen(textBox1.Text, lvnhanvien);
            }
        }

        private void btThongKe_Click(object sender, EventArgs e)
        {
            ThongkeKH tk = new ThongkeKH();
            this.Hide();
            tk.ShowDialog();
            this.Show();
        }
    }
}

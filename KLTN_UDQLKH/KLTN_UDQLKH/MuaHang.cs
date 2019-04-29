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
    
    public partial class MuaHang : Form
    {
        string ten;
        public MuaHang(string _tendn)
        {
            InitializeComponent();
            ten = _tendn;

            ColumnHeader columnheader;
            lvhoadon.View = View.Details;
            lvhoadon.GridLines = true;
            //lvhoadon.FullRowSelect = true;

            columnheader = new ColumnHeader(); // create id column
            columnheader.Width = 100;
            columnheader.Text = "Mã hóa đơn";
            lvhoadon.Columns.Add(columnheader);

            columnheader = new ColumnHeader(); //create name customer column
            columnheader.Width = 200;
            columnheader.Text = "Họ tên KH";
            lvhoadon.Columns.Add(columnheader);

            columnheader = new ColumnHeader(); // create gender customer column
            columnheader.Width = 100;
            columnheader.Text = "Tổng tiền";
            lvhoadon.Columns.Add(columnheader);

            columnheader = new ColumnHeader(); //create birthday column
            columnheader.Width = 100;
            columnheader.Text = "Ngày mua";
            lvhoadon.Columns.Add(columnheader);

            hdBLL.Load(lvhoadon);

            // hiển thị dữ liệu chi tiết hóa đơn
            ColumnHeader columnheader1;
            lv_cthoadon.View = View.Details;
            lv_cthoadon.GridLines = true;
            //lvhoadon.FullRowSelect = true;

            columnheader1 = new ColumnHeader(); 
            columnheader1.Width = 100;
            columnheader1.Text = "Mã chi tiết";
            lv_cthoadon.Columns.Add(columnheader1);

            columnheader1 = new ColumnHeader();
            columnheader1.Width = 200;
            columnheader1.Text = "Mã hóa đơn";
            lv_cthoadon.Columns.Add(columnheader1);

            columnheader1 = new ColumnHeader(); 
            columnheader1.Width = 200;
            columnheader1.Text = "Sản phẩm";
            lv_cthoadon.Columns.Add(columnheader1);

            columnheader1 = new ColumnHeader(); 
            columnheader1.Width = 100;
            columnheader1.Text = "Số lượng mua";
            lv_cthoadon.Columns.Add(columnheader1);
            cthdBLL.LoadComboBoxKH(cmbsanpham);

        }
        NHAN_VIEN_DTO nv = new NHAN_VIEN_DTO();
        HOA_DON_DTO hdDTO = new HOA_DON_DTO();
        HOA_DON_BLL hdBLL = new HOA_DON_BLL();
        NangCapVaUuDai nc = new NangCapVaUuDai();
        XDocument testXML = XDocument.Load("..//..//Data//NHAN_VIEN.xml");
        XDocument testXMLKH = XDocument.Load("..//..//Data//KHACH_HANG.xml");
        private void MuaHang_Load(object sender, EventArgs e)
        {
            //hdBLL.LoadComboBoxKH(cmbkhachhang);
            //hdBLL.LoadComboBoxNV(cmbnhanvien);
            var node = testXML.Descendants("nhanvien").Where(x => x.Element("tendn").Value.Equals(ten)).FirstOrDefault();
            lbmanv.Text = node.Attribute("id").Value;
            cthdBLL.LoadComboBoxKH(cmbsanpham);
            nc.QuaSinhNhat();
            nc.QuaTet();
        }

        private void lvhoadon_Click(object sender, EventArgs e)
        {
           

                foreach (ListViewItem item in lvhoadon.SelectedItems)
            {
                txtmahd.Text = item.SubItems[0].Text;
                textBox1makh.Text = item.SubItems[1].Text;
            }

            //hdBLL.TimTheoID(int.Parse(txtmahd.Text.Trim()),lv_cthoadon);
            cthdBLL.LoadDB(int.Parse(txtmahd.Text), lv_cthoadon);
            //MessageBox.Show(textBox1makh.Text);
            var node = testXMLKH.Descendants("khachhang").Where(x => x.Attribute("id").Value.Equals(textBox1makh.Text.Trim())).FirstOrDefault();
            int i = int.Parse(node.Element("diemconlai").Value);
            if (i > 100)
            {
                checkBox1.Enabled = true;
            }
            else
            {
                checkBox1.Enabled = false;
            }
        }

        //---------------------------LẤY DỮ LIỆU THÊM HÓA ĐƠN---------------------------

        public bool layDL_themKH() //Lấy dữ liệu từ txt
        {
            hdDTO.idkh = int.Parse(textBox1makh.Text.Trim());
            hdDTO.idnv = int.Parse(lbmanv.Text.Trim());
            hdDTO.tongtienmua = 0;
            DateTime dt = DateTime.Now;
            hdDTO.ngaymua = (dt).ToString("dd-MM-yyyy,hh:mm:ss", CultureInfo.InvariantCulture);
            //hdDTO.ngaymua = dtpngaymua.Value.Date.ToString(" dddd,dd-MM-yyyy,hh:mm:ss tt", CultureInfo.InvariantCulture);  //chuyển datetimepicker values sang string
            return true;
        }
        private void btthemhoadon_Click(object sender, EventArgs e)
        {
            if (!layDL_themKH())
                return;
            hdBLL.ThemHD(hdDTO, lvhoadon);
            MuaHang Child = new MuaHang(ten);
            this.Hide();
            Child.ShowDialog();
            this.Show();
        }


        CT_HOADON_DTO cthdDTO = new CT_HOADON_DTO();
        CT_HOADON_BLL cthdBLL = new CT_HOADON_BLL();
        public bool layDL_themCTHD() //Lấy dữ liệu từ txt
        {
            cthdDTO.idsp = int.Parse(cmbsanpham.SelectedValue.ToString());
            cthdDTO.idhoadon = int.Parse(txtmahd.Text);
            cthdDTO.soluongmua = int.Parse(txtsoluongmua.Text);
            return true;
        }
        private void button2_Click(object sender, EventArgs e)
        {
            if (!layDL_themCTHD())
                return;
            cthdBLL.ThemHD(cthdDTO,lv_cthoadon,cmbsanpham.SelectedValue.ToString(),int.Parse(txtsoluongmua.Text.Trim()));
            cthdBLL.LoadDB(int.Parse(txtmahd.Text),lv_cthoadon);

            layDL1();
            txttongtienmua.Text = cthdBLL.TinhTien(int.Parse(txtmahd.Text)).ToString();
            txtsl.Text = cthdBLL.soluong(int.Parse(txtmahd.Text)).ToString();
            txttieng.Text = cthdBLL.TinhTiengiam(int.Parse(txtmahd.Text)).ToString();

            
        }

        //--------------lay DL---------------------
        public bool layDL() //Lấy dữ liệu từ txt
        {
            hdDTO.id = int.Parse(txtmahd.Text);
            hdDTO.tiennhan = int.Parse(txttiennhan.Text);
            hdDTO.tientra = int.Parse(txttientra.Text);
            hdDTO.tongtienmua = int.Parse(txttongtienmua.Text);
            return true;
        }

        public bool layDL1() //Lấy dữ liệu từ txt
        {
            hdDTO.id = int.Parse(txtmahd.Text);
            return true;
        }
        private void btthanhtoan_Click(object sender, EventArgs e)
        {
            
            if (checkBox1.Checked == false)
            {
                layDL();
                cthdBLL.ThanhToan(int.Parse(txtmahd.Text), int.Parse(txttiennhan.Text), double.Parse(txttieng.Text), int.Parse(txttongtienmua.Text));
                //MessageBox.Show("tien chu: " + cthdBLL.DocTienBangChu(long.Parse(txttongtienmua.Text.Trim())));
            }
            if (checkBox1.Checked == true)
            {
                layDL();
                int pmh = cthdBLL.PMH(textBox1makh.Text, txtmahd.Text, int.Parse(txttiennhan.Text), double.Parse(txttieng.Text), int.Parse(txttongtienmua.Text));
                cthdBLL.ThanhToanPMH(textBox1makh.Text, txtmahd.Text, int.Parse(txttiennhan.Text), double.Parse(txttieng.Text), int.Parse(txttongtienmua.Text),pmh);
            }

            Xemhoadon xhd = new Xemhoadon(int.Parse(txtmahd.Text), double.Parse(txttiennhan.Text), cthdBLL.soluong(int.Parse(txtmahd.Text)));
            xhd.Show();

            
        }


        private void txttiennhan_TextChanged(object sender, EventArgs e)
        {
            

            int pmh = cthdBLL.PMH(textBox1makh.Text, txtmahd.Text, int.Parse(txttiennhan.Text), double.Parse(txttieng.Text), int.Parse(txttongtienmua.Text));
            try
            {
                if (IsNumber(txttiennhan.Text) != true)
                {
                    MessageBox.Show("Dữ liệu nhập không hợp lệ, không được nhập ký tự", "Thông báo");
                    txttiennhan.Text = "";

                }
                else
                {
                    if (checkBox1.Checked == true)
                    {
                        int tienthoi = int.Parse(txttiennhan.Text) + pmh - int.Parse(txttongtienmua.Text);
                        txttientra.Text = tienthoi.ToString();
                    }
                    else
                    {
                        int tienthoi = int.Parse(txttiennhan.Text) - int.Parse(txttongtienmua.Text);
                        txttientra.Text = tienthoi.ToString();
                    }
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi: "+ex);
            }
        }
        private static bool IsNumber(string val)
        {
            if (val != "")
                return Regex.IsMatch(val, @"^[0-9]\d*\.?[0]*$");
            else return true;
        }

        private void txttongtienmua_TextChanged(object sender, EventArgs e)
        {
            try
            {
                int diemtichluy = (Int32.Parse(txttongtienmua.Text)) / 10000;
                txtdiem.Text = diemtichluy.ToString();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi: " + ex);
            }
        }

        private void bttinhtien_Click(object sender, EventArgs e)
        {
            layDL1();
            txttongtienmua.Text = cthdBLL.TinhTien(int.Parse(txtmahd.Text)).ToString();
            txtsl.Text = cthdBLL.soluong(int.Parse(txtmahd.Text)).ToString();
            txttieng.Text = cthdBLL.TinhTiengiam(int.Parse(txtmahd.Text)).ToString();
        }

        private void txtmahd_TextChanged(object sender, EventArgs e)
        {

        }

        private void button5_Click(object sender, EventArgs e)
        {
            

        }

        private void lv_cthoadon_Click(object sender, EventArgs e)
        {
            foreach (ListViewItem item in lv_cthoadon.SelectedItems)
            {
                txtmact.Text = item.SubItems[0].Text;
                cmbsanpham.SelectedValue = item.SubItems[2].Text;
                txtsoluongmua.Text = item.SubItems[3].Text;
                txtsl_kdoi.Text = item.SubItems[3].Text;
            }
        }

        public bool layDLcthd() //Lấy dữ liệu từ txt
        {
            cthdDTO.id = int.Parse(txtmact.Text);
            cthdDTO.idsp = int.Parse(cmbsanpham.SelectedValue.ToString());
            cthdDTO.soluongmua = int.Parse(txtsoluongmua.Text);
            return true;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            layDLcthd();
            cthdBLL.TraSP(cthdDTO,lv_cthoadon, cmbsanpham.SelectedValue.ToString(), int.Parse(txtsoluongmua.Text.Trim()));
            cthdBLL.LoadDB(int.Parse(txtmahd.Text), lv_cthoadon);
        }

        private void btdoisp_Click(object sender, EventArgs e)
        {
            layDLcthd();
            cthdBLL.DoiSP(cthdDTO, lv_cthoadon, cmbsanpham.SelectedValue.ToString(), int.Parse(txtsoluongmua.Text.Trim()), int.Parse(txtsl_kdoi.Text.Trim()));
            cthdBLL.LoadDB(int.Parse(txtmahd.Text), lv_cthoadon);
        }

        private void btcapnhatdiem_Click(object sender, EventArgs e)
        {
            cthdBLL.CapNhatDiemNgay(int.Parse(txtdiem.Text));

            cthdBLL.NangCapThe();
            
            cthdBLL.NangCapKimCuong();
        }

        DoiQuaBLL dqBLL = new DoiQuaBLL();

        private void button4kt_Click(object sender, EventArgs e)
        {
            
        }

        XDocument testXML_HD = XDocument.Load("..\\..\\Data\\HOA_DON.xml");

        private void button1_Click(object sender, EventArgs e)
        {
            var Node = testXML_HD.Descendants("hoadon").Where(x => x.Attribute("id").Value.Equals(txtmahd.Text) && x.Element("trangthaiTT").Value.Equals("1")).ToList();
            //MessageBox.Show("kc nè"+Node.Count);
            if (Node.Count == 0)
            {
                hdBLL.XoaHD(int.Parse(txtmahd.Text), lvhoadon);
            }
            else
                MessageBox.Show("Hóa đơn đã thanh toán, không thể xóa!");
        }

        private void button4_Click(object sender, EventArgs e)
        {
            MuaHang Child = new MuaHang(ten);
            this.Hide();
            Child.ShowDialog();
            this.Show();
        }

        private void txtsoluongmua_TextChanged(object sender, EventArgs e)
        {
            //try
            //{
                if (IsNumber(txtsoluongmua.Text) != true)
                {
                    MessageBox.Show("Dữ liệu nhập không hợp lệ, không được nhập ký tự", "Thông báo");
                    txtsoluongmua.Text = "";

                }
                //else
                //{
                    
                //}

            //}
            //catch (Exception ex)
            //{
            //    MessageBox.Show("Lỗi: " + ex);
            //}
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            int pmh = cthdBLL.PMH(textBox1makh.Text, txtmahd.Text, int.Parse(txttiennhan.Text), double.Parse(txttieng.Text), int.Parse(txttongtienmua.Text));
            MessageBox.Show("QUÝ KHÁCH ĐÃ SỬ DỤNG PHIẾU CHIẾT KHẤU TRỊ GIÁ " + pmh+" CHO HÓA ĐƠN NÀY!");
            if (checkBox1.Checked == true)
            {
                if (pmh >= int.Parse(txttongtienmua.Text))
                {
                    txtdiem.Text = "0";
                    txttientra.Text = (int.Parse(txttiennhan.Text) + pmh - int.Parse(txttongtienmua.Text)).ToString();
                }
                    
                if(pmh< int.Parse(txttongtienmua.Text))
                {
                    txtdiem.Text = ((int.Parse(txttongtienmua.Text) - pmh) / 10000).ToString();
                    txttientra.Text = (int.Parse(txttiennhan.Text) + pmh - int.Parse(txttongtienmua.Text)).ToString();
                }
                    

                
            }

            if (checkBox1.Checked == false)
            {
                if (pmh >= int.Parse(txttongtienmua.Text))
                {
                    txtdiem.Text = "0";
                    txttientra.Text = (int.Parse(txttiennhan.Text) - int.Parse(txttongtienmua.Text)).ToString();
                }

                if (pmh < int.Parse(txttongtienmua.Text))
                {
                    txtdiem.Text = (int.Parse(txttongtienmua.Text)/ 10000).ToString();
                    txttientra.Text = (int.Parse(txttiennhan.Text) + pmh - int.Parse(txttongtienmua.Text)).ToString();
                }



            }
        }

        private void label10gia_Click(object sender, EventArgs e)
        {

        }

        XDocument testSP = XDocument.Load("..//..//Data//SAN_PHAM.xml");
        int k = 0;
        private void cmbsanpham_SelectedIndexChanged(object sender, EventArgs e)
        {
            //MessageBox.Show("yen" + cmbsanpham.SelectedValue.ToString());
            var Node = testSP.Descendants("sanpham").Where(x => x.Attribute("id").Value.Equals(cmbsanpham.SelectedValue.ToString())).FirstOrDefault();
            if (k == 0)
                label10gia.Text = "6500";
            if (k == 1)
                label10gia.Text = Node.Element("dongia").Value;
        }

        private void cmbsanpham_Click(object sender, EventArgs e)
        {
            k = 1;
            
        }

        private void txttiennhan_Leave(object sender, EventArgs e)
        {
            
           

        }

        private void txttiennhan_MouseHover(object sender, EventArgs e)
        {
            System.Globalization.CultureInfo culture = new System.Globalization.CultureInfo("en-US");
            decimal value = decimal.Parse(txttiennhan.Text, System.Globalization.NumberStyles.AllowThousands);
            txttiennhan.Text = String.Format(culture, "{0:N0}", value);
            txttiennhan.Select(txttiennhan.Text.Length, 0);
        }
    }
}
                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                 
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
using System.Xml;
using KLTN_UDQLKH.DTO;
using KLTN_UDQLKH.BLL;
namespace KLTN_UDQLKH
{
    public partial class DangNhap : Form
    {

        //public string Message { get; internal set; }
        public DangNhap()
        {
            InitializeComponent();
            doc.Load(path);
            root = doc.DocumentElement;

            doc1.Load(path1);
            root1 = doc1.DocumentElement;
            //cthd.CapNhatDiem();

        }
        public string Message { get; internal set; }
        //NangCapTheKH ncBLL = new NangCapTheKH();
        CT_HOADON_BLL cthd = new CT_HOADON_BLL();
        DoiQuaBLL dq = new DoiQuaBLL();
        NangCapVaUuDai nc = new NangCapVaUuDai();
        
        private void DangNhap_Load(object sender, EventArgs e)
        {
            //ncBLL.nangcap();
            cthd.CapNhatDiem();
            //MessageBox.Show("egfydwe");
            cthd.NangCapThe();
            //MessageBox.Show("kc nè");
            cthd.NangCapKimCuong();
            dq.LuuDiem();
            nc.QuaSinhNhat();
            
            //tắt chút, xíu mở lại nhá nha nha
            //dq.DoiQua100();

        }
        public static string path = "..\\..\\Data\\KHACH_HANG.xml";
        public static string path1 = "..\\..\\Data\\NHAN_VIEN.xml";
        XmlDocument doc = new XmlDocument();
        XmlDocument doc1 = new XmlDocument();
        XmlElement root;
        XmlElement root1;
        XDocument testXML = XDocument.Load("..\\..\\Data\\KHACH_HANG.xml");
        XDocument testXML_NV = XDocument.Load("..\\..\\Data\\NHAN_VIEN.xml");
        KHACH_HANG_DTO acc = new KHACH_HANG_DTO();
        //AccountBLL ac = new AccountBLL();
        private void btlogin_Click(object sender, EventArgs e)
        {
            string ten = txt_username.Text.Trim();
            var test = (from kh in testXML.Descendants("khachhang")
                        where (string)kh.Attribute("id").Value == ten
                        select new
                        {
                            name = (string)kh.Element("tendn")
                        }).ToList();
            int sltest = test.Count;
        
            var test2 = (from nv in testXML_NV.Descendants("nhanvien")
                        where (string)nv.Attribute("id").Value == ten
                        select new
                        {
                            name = (string)nv.Element("tendn")
                        }).ToList();
            int sltest2 = test2.Count;
            
            string mk = txt_password.Text.Trim();
            if (txt_username.Text == null || txt_password.Text == null)
            {
                MessageBox.Show("Ban chua nhap ten va mat khau!");
                return;
            }
            if (sltest > 0)
            {
                foreach (var j in test)
                {
                    XmlNode kiemtra = root.SelectSingleNode("khachhang[tendn='" + j.name + "']");

                    if (kiemtra != null)
                    {
                        XmlNode matkhau = root.SelectSingleNode("khachhang[matkhau='" + txt_password.Text.Trim() + "']");

                        if (matkhau != null)
                        {
                            var Node = (from c in testXML.Descendants("khachhang")
                                        where (string)c.Element("tendn") == j.name && (string)c.Element("matkhau") == mk
                                        select new
                                        {
                                            loai = (string)c.Attribute("idquyen")
                                        }).ToList();
                            string loaitk = "";
                            foreach (var i in Node)
                            {
                                loaitk = i.loai;
                            }
                            //MessageBox.Show(" " + ten);
                            if (loaitk == "2")
                            {
                                Khach_Hang Child = new Khach_Hang(j.name);
                                this.Hide();
                                Child.ShowDialog();
                                this.Show();
                                txt_username.Text = "";
                                txt_password.Text = "";
                            }
                        }
                    }
                }
            }
                if (sltest2 > 0)
                {
                    foreach (var k in test2)
                    {
                        XmlNode kiemtra1 = root1.SelectSingleNode("nhanvien[tendn='" + k.name +"']");
                        if (kiemtra1 != null)
                        {
                            //-----------------------------ktra tai khoan nhan vien--------------------------------
                            XmlNode matkhau1 = root1.SelectSingleNode("nhanvien[matkhau='" + txt_password.Text.Trim() + "']");
                            if (matkhau1 != null)
                            {
                                var Node1 = (from c in testXML_NV.Descendants("nhanvien")
                                             where (string)c.Element("tendn") == k.name && (string)c.Element("matkhau") == mk
                                             select new
                                             {
                                                 loai = (string)c.Attribute("idquyen")
                                             }).ToList();

                                string loaitk = "";
                                foreach (var i in Node1)
                                {
                                    loaitk = i.loai;
                                }
                                //MessageBox.Show(" " + ten);
                                if (loaitk == "1")
                                {
                                    NhanVien b = new NhanVien(k.name);
                                    //b.Activate();
                                    this.Hide();
                                    b.ShowDialog();
                                    this.Show();
                                    txt_username.Text = "";
                                    txt_password.Text = "";
                                    
                                }
                            }
                            else
                            {
                                MessageBox.Show("Mật khẩu không đúng");
                                DangNhap dn = new DangNhap();
                                this.Hide();
                                dn.ShowDialog();
                                this.Show();
                            }

                        }
                        else
                        {
                            MessageBox.Show("Tài khoản không tồn tại. Vui lòng đăng ký tài khoản.");
                        }
                    }
                }
            
        }

        private void txt_password_OnValueChanged(object sender, EventArgs e)
        {
            txt_password.isPassword = true;
        }

        private void btthoat_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void btinthe_Click(object sender, EventArgs e)
        {
            InTheKhachHang inthe = new InTheKhachHang();
            this.Hide();
            inthe.ShowDialog();
            this.Show();
        }

        private void content_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}

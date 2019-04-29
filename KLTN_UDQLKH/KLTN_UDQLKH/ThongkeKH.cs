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
using System.Windows.Forms.DataVisualization.Charting;

namespace KLTN_UDQLKH
{
    public partial class ThongkeKH : Form
    {
        public ThongkeKH()
        {
            InitializeComponent();

            //-----------------------thong ke-------------------
            ColumnHeader columnheader2;
            lvthongkeloaikh.View = View.Details;
            lvthongkeloaikh.GridLines = true;
            //lvkhachhang.FullRowSelect = true;

            columnheader2 = new ColumnHeader(); // create id column
            columnheader2.Width = 50;
            columnheader2.Text = "Mã KH";
            lvthongkeloaikh.Columns.Add(columnheader2);

            columnheader2 = new ColumnHeader(); //create name customer column
            columnheader2.Width = 120;
            columnheader2.Text = "Họ tên KH";
            lvthongkeloaikh.Columns.Add(columnheader2);

            columnheader2 = new ColumnHeader(); // create gender customer column
            columnheader2.Width = 60;
            columnheader2.Text = "Giới tính";
            lvthongkeloaikh.Columns.Add(columnheader2);

            columnheader2 = new ColumnHeader(); //create birthday column
            columnheader2.Width = 80;
            columnheader2.Text = "Ngày sinh";
            lvthongkeloaikh.Columns.Add(columnheader2);

            columnheader2 = new ColumnHeader(); //create cmnd column
            columnheader2.Width = 100;
            columnheader2.Text = "Số CMND";
            lvthongkeloaikh.Columns.Add(columnheader2);

            columnheader2 = new ColumnHeader(); //create address column
            columnheader2.Width = 150;
            columnheader2.Text = "Địa chỉ";
            lvthongkeloaikh.Columns.Add(columnheader2);


            columnheader2 = new ColumnHeader(); //create phone column
            columnheader2.Width = 100;
            columnheader2.Text = "Số điện thoại";
            lvthongkeloaikh.Columns.Add(columnheader2);

            columnheader2 = new ColumnHeader(); // điểm tích lũy
            columnheader2.Width = 100;
            columnheader2.Text = "Điểm tích lũy";
            lvthongkeloaikh.Columns.Add(columnheader2);

            columnheader2 = new ColumnHeader(); // điểm mua hàng
            columnheader2.Width = 100;
            columnheader2.Text = "Điểm mua hàng";
            lvthongkeloaikh.Columns.Add(columnheader2);

            columnheader2 = new ColumnHeader(); // điểm thưởng
            columnheader2.Width = 90;
            columnheader2.Text = "Điểm thưởng";
            lvthongkeloaikh.Columns.Add(columnheader2);

            tkBLL.LoadComboBoxLoaiKH(cmbloaikh1);
        }
        ThongKe tkBLL = new ThongKe();
        int ck = 1;
        private void cmbloaikh1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ck == 0)
            {
                txtthongkeloaikh.Text = tkBLL.Thongke_theoLoaiKH(int.Parse(cmbloaikh1.SelectedValue.ToString())).ToString();
                tkBLL.Hienthi_TKLoaiKH(lvthongkeloaikh, int.Parse(cmbloaikh1.SelectedValue.ToString()));
            }
            if (ck == 1)
            {
                txtthongkeloaikh.Text = tkBLL.Thongke_theoLoaiKH(1).ToString();
                tkBLL.Hienthi_TKLoaiKH(lvthongkeloaikh, 1);
            }
        }

        private void cmbloaikh1_Click(object sender, EventArgs e)
        {
            ck = 0;
        }

        int a = 1;
        private void cmbgt_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (a == 0)
            {
                textBoxthongketheoGT.Text = tkBLL.Thongke_theogt(cmbgt.Text.Trim()).ToString();
                tkBLL.Hienthi_TKtheogt(lvthongkeloaikh, cmbgt.Text.Trim());
            }
        }

        private void cmbgt_Click(object sender, EventArgs e)
        {
            a = 0;
        }

        private void textBoxdiachi_TextChanged(object sender, EventArgs e)
        {
            textBoxTKtheoDC.Text = tkBLL.Thongke_theoDC(textBoxdiachi.Text).ToString();
            tkBLL.Hienthi_TKtheoDC(lvthongkeloaikh, textBoxdiachi.Text);
        }

        private void comboBoxEx1_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtthongkeNgaySinh.Text = tkBLL.Thongke_theoNgaysinh(comboBoxTK.SelectedItem.ToString()).ToString();
            tkBLL.Hienthi_TKtheoNgaySinh(lvthongkeloaikh, comboBoxTK.SelectedItem.ToString());
        }

        XDocument testloaikh = XDocument.Load("..\\..\\Data\\LOAI_KHACH_HANG.xml");
        XDocument testtk = XDocument.Load("..\\..\\Data\\thongkeloai.xml");
        private void ThongkeKH_Load(object sender, EventArgs e)
        {
           
            tkBLL.CapNhatLoai();
            label1.Text = "/"+testkh.Descendants("khachhang").Count().ToString();
            DataSet ds = new DataSet();

            ds.ReadXml("..\\..\\Data\\thongkeloai.xml");
            chartLoaiKH.Series["LoaiKH"].XValueMember = "ten";
            chartLoaiKH.Series["LoaiKH"].YValueMembers = "soluong";

            chartLoaiKH.DataSource = ds;
            chartLoaiKH.DataBind();

            chartLoaiKH.Series[0].IsValueShownAsLabel = true;
            chartLoaiKH.Series[0].Label = "#PERCENT";
            chartLoaiKH.Series[0].LegendText = "#AXISLABEL";

            this.chartLoaiKH.Legends.Add("Legend1");
            this.chartLoaiKH.Legends[0].Enabled = true;
            this.chartLoaiKH.Legends[0].Docking = Docking.Bottom;
            this.chartLoaiKH.Legends[0].Alignment = System.Drawing.StringAlignment.Center;
            this.chartLoaiKH.Series[0].LegendText = "#VALX (#PERCENT)";
        }

        private void lvthongkeloaikh_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void lvthongkeloaikh_Click(object sender, EventArgs e)
        {
            
            foreach (ListViewItem item in lvthongkeloaikh.SelectedItems)
            {
                
                textBox1makh.Text = item.SubItems[0].Text;
                label2makh.Text = item.SubItems[0].Text;
                label3tenkh.Text = item.SubItems[1].Text;
                label4ngaysinh.Text = item.SubItems[3].Text;
                try
                {
                    var Node = testqt.Descendants("quatang_uudai").Where(x => x.Attribute("idkh").Value.Equals(textBox1makh.Text.Trim()) && x.Element("tenqt").Value.Equals("Quà sinh nhật")).FirstOrDefault();
                    if (Node.Element("trangthai").Value == "1")
                    {
                        checkBoxquasn.Checked = true;
                        checkBoxquasn.Enabled = false;
                        label3trangthai.Text = "Đã nhận";
                    }
                    if (Node.Element("trangthai").Value == "0")
                    {
                        checkBoxquasn.Checked = false;
                        checkBoxquasn.Enabled = true;
                        label3trangthai.Text = "Chưa nhận";
                    }
                    if (Node.Element("trangthai").Value == "2")
                    {
                        checkBoxquasn.Checked = false;
                        checkBoxquasn.Enabled = false;
                        label3trangthai.Text = "Quá hạn nhận";
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Khong ton tai");
                    checkBoxquasn.Enabled = false;
                }
            }
        }

        XDocument testqt = XDocument.Load("..\\..\\Data\\QUATANG_UUDAI.xml");
        string path = "..\\..\\Data\\QUATANG_UUDAI.xml";
        XDocument testkh = XDocument.Load("..\\..\\Data\\KHACH_HANG.xml");
        private void checkBoxquasn_CheckedChanged(object sender, EventArgs e)
        {
            var node = testkh.Descendants("khachhang").Where(x => x.Attribute("id").Value.Equals(textBox1makh.Text)).FirstOrDefault();
            var node1 = testqt.Descendants("quatang_uudai").Where(x => x.Attribute("idkh").Value.Equals(textBox1makh.Text) && x.Element("tenqt").Value.Equals("Quà sinh nhật")).Count();
            //DateTime ns = DateTime.Parse(node1.Element("soluong").Value); // ngay bt
            //DateTime ns1 = DateTime.Parse(node1.Element("tientuongung").Value); // ngay kt
            ////string ngayhethan = node.Element("ngaysinh").Value.Substring(0, 1)+ "-" + node.Element("ngaysinh").Value.Substring(3, 5)+"-" + DateTime.Now.Year.ToString()
            //DateTime nht = DateTime.Now.Date;

            //TimeSpan time = nht - ns;
            //TimeSpan time2 = ns1-nht;

            //MessageBox.Show(time.Days.ToString());
            //MessageBox.Show(time2.Days.ToString());
            //if (time.Days <= time2.Days)
            //{
            MessageBox.Show("dfdyfgy"+node1);
                if (checkBoxquasn.Checked == true)
            {
                
                    try
                    {
                        var Node = testqt.Descendants("quatang_uudai").Where(x => x.Attribute("idkh").Value.Equals(textBox1makh.Text) && x.Element("tenqt").Value.Equals("Quà sinh nhật")).FirstOrDefault();
                        Node.Element("trangthai").Value = "1";
                        testqt.Save(path);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Chưa đến ngày sinh nhật của khách hàng" + ex);
                    }
                

            }
            
            //}
            //else
            //{
            //    MessageBox.Show("qua 6 thang");
            //    checkBoxquasn.Checked = false;
            //    if (checkBoxquasn.Checked == false)
            //    {
            //        try
            //        {
            //            var Node = testqt.Descendants("quatang_uudai").Where(x => x.Attribute("idkh").Value.Equals(textBox1makh.Text) && x.Element("tenqt").Value.Equals("Quà sinh nhật")).FirstOrDefault();
            //            Node.Element("trangthai").Value = "0";
            //            testqt.Save(path);
            //        }
            //        catch (Exception ex)
            //        {
            //            MessageBox.Show("Lỗi: " + ex);
            //        }

            //    }
            //}
                
        }
    }
}

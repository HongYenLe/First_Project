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
using Microsoft.Office.Interop.Excel;


namespace KLTN_UDQLKH
{
    public partial class SanPham : Form
    {
        public SanPham()
        {
            InitializeComponent();

            ColumnHeader columnheader3;
            lvsanpham.View = View.Details;
            lvsanpham.GridLines = true;

            columnheader3 = new ColumnHeader(); // create id column
            columnheader3.Width = 100;
            columnheader3.Text = "Mã sản phẩm";
            lvsanpham.Columns.Add(columnheader3);

            columnheader3 = new ColumnHeader(); //create name customer column
            columnheader3.Width = 200;
            columnheader3.Text = "Tên sản phẩm";
            lvsanpham.Columns.Add(columnheader3);

            columnheader3 = new ColumnHeader(); // create gender customer column
            columnheader3.Width = 100;
            columnheader3.Text = "Đơn giá";
            lvsanpham.Columns.Add(columnheader3);

            columnheader3 = new ColumnHeader(); //create birthday column
            columnheader3.Width = 100;
            columnheader3.Text = "Đơn vị tính";
            lvsanpham.Columns.Add(columnheader3);

            columnheader3 = new ColumnHeader(); //create birthday column
            columnheader3.Width = 100;
            columnheader3.Text = "Giảm giá";
            lvsanpham.Columns.Add(columnheader3);

            columnheader3 = new ColumnHeader(); //create birthday column
            columnheader3.Width = 100;
            columnheader3.Text = "Số lượng";
            lvsanpham.Columns.Add(columnheader3);

            columnheader3 = new ColumnHeader(); //create cmnd column
            columnheader3.Width = 100;
            columnheader3.Text = "Tên loại SP";
            lvsanpham.Columns.Add(columnheader3);

            columnheader3 = new ColumnHeader(); //create address column
            columnheader3.Width = 200;
            columnheader3.Text = "Nhà cung cấp";
            lvsanpham.Columns.Add(columnheader3);
            spBLL.Load(lvsanpham);
            spBLL.LoadComboBoxNCC(comboBoxNCC);
            spBLL.LoadComboBoxloaiSP(comboBoxloaisp);
        }
        SAN_PHAM_DTO spDTO = new SAN_PHAM_DTO();
        SAN_PHAM_BLL spBLL = new SAN_PHAM_BLL();
        private void button8_Click(object sender, EventArgs e)
        {
            txtmasp.Clear();
            textBoxtensp.Clear();
            textBoxdongia.Clear();
            textBoxdvt.Clear();
            textBoxgiamgia.Clear();
        }

        public bool layDL_themSP() //Lấy dữ liệu từ txt
        {
            //nccDTO.id = int.Parse(txtmancc.Text);
            spDTO.tensp = textBoxtensp.Text;
            spDTO.dongia = int.Parse(textBoxdongia.Text);
            spDTO.donvitinh = textBoxdvt.Text;
            spDTO.giamgia = int.Parse(textBoxgiamgia.Text);
            spDTO.idloai = int.Parse(comboBoxloaisp.SelectedValue.ToString());
            spDTO.idncc = int.Parse(comboBoxNCC.SelectedValue.ToString());
            return true;
        }

        private bool IsNull_SP() // kiểm tra thông tin trong textbox 
        {

            if (textBoxtensp.Text == "")
            {
                MessageBox.Show("Bạn chưa nhập tên sản phẩm!");
                return false;
            }

            if (textBoxdongia.Text == "")
            {
                MessageBox.Show("Bạn chưa nhập đơn giá!");
                return false;
            }
            if (textBoxdvt.Text == "")
            {
                MessageBox.Show("Bạn chưa nhập đơn vị tính!");
                return false;
            }
            if (textBoxgiamgia.Text == "")
            {
                MessageBox.Show("Bạn chưa nhập mức giảm giá!");
                return false;
            }
            if (txtsl.Text == "")
            {
                MessageBox.Show("Bạn chưa nhập số lượng sản phẩm!");
                return false;
            }
            return true;
        }

        private void button1themsp_Click(object sender, EventArgs e)
        {
            if (IsNull_SP() == false)
                return;
            if (!layDL_themSP())
                return;
            spBLL.ThemSp(spDTO, lvsanpham);
        }

        private void lvsanpham_Click(object sender, EventArgs e)
        {
            foreach (ListViewItem item in lvsanpham.SelectedItems)
            {
                txtmasp.Text = item.SubItems[0].Text;
                textBoxtensp.Text = item.SubItems[1].Text;
                textBoxdongia.Text = item.SubItems[2].Text;
                textBoxdvt.Text = item.SubItems[3].Text;
                textBoxgiamgia.Text = item.SubItems[4].Text;
                txtsl.Text = item.SubItems[5].Text;
                comboBoxloaisp.Text = item.SubItems[6].Text;
                comboBoxNCC.Text = item.SubItems[7].Text;
            }
        }

        public bool layDL_suaSP() //Lấy dữ liệu từ txt
        {
            spDTO.id = int.Parse(txtmasp.Text);
            spDTO.tensp = textBoxtensp.Text;
            spDTO.dongia = int.Parse(textBoxdongia.Text);
            spDTO.donvitinh = textBoxdvt.Text;
            spDTO.giamgia = int.Parse(textBoxgiamgia.Text);
            spDTO.soluong = int.Parse(txtsl.Text);
            spDTO.idloai = int.Parse(comboBoxloaisp.SelectedValue.ToString());
            spDTO.idncc = int.Parse(comboBoxNCC.SelectedValue.ToString());
            return true;
        }
        private void button7suasp_Click(object sender, EventArgs e)
        {
            if (IsNull_SP() == false)
                return;
            if (!layDL_suaSP())
                return;
            spBLL.SuaSP(spDTO, lvsanpham);
        }

        private void button6xoasp_Click(object sender, EventArgs e)
        {
            DialogResult rs = MessageBox.Show("Xác nhận xóa sản phẩm có mã " + txtmasp.Text, "Xác nhận", MessageBoxButtons.OKCancel);
            if (rs == DialogResult.OK)
            {
                layDL_suaSP();
                spBLL.XoaSP(spDTO.id, lvsanpham);

                //set default
                txtmasp.Clear();
                textBoxtensp.Clear();
                textBoxdongia.Clear();
                textBoxdvt.Clear();
                txtsl.Clear();
                textBoxgiamgia.Clear();
            }
        }

        private void button4xuatexcel_Click(object sender, EventArgs e)
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
                        ws.Cells[1, 1] = "Mã sản phẩm";
                        ws.Cells[1, 2] = "Tên sản phẩm";
                        ws.Cells[1, 3] = "Đơn giá";
                        ws.Cells[1, 4] = "Đơn vị tính";
                        ws.Cells[1, 5] = "Mức giảm giá";
                        ws.Cells[1, 6] = "Loại sản phẩm";
                        ws.Cells[1, 7] = "Nhà cung cấp";
                        int i = 2;
                        foreach (ListViewItem item in lvsanpham.Items)
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
                        MessageBox.Show("Đã xuất file");
                    }
                }
            }
            catch (Exception err)
            {
                MessageBox.Show(err.Message);
            }
        }

        private void textBox1timkiem_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox1timkiem_Click(object sender, EventArgs e)
        {
            textBox1timkiem.Clear();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox1.SelectedItem.Equals("Mã sản phẩm"))
            {
                //lvkhachhang.Clear();
                spBLL.TimTheoID(textBox1timkiem.Text, lvsanpham);
            }

            if (comboBox1.SelectedItem.Equals("Tên sản phẩm"))
            {
                //lvkhachhang.Clear();
                spBLL.TimTheoTen(textBox1timkiem.Text, lvsanpham);
            }

            if (comboBox1.SelectedItem.Equals("Loại sản phẩm"))
            {
                //lvkhachhang.Clear();
                spBLL.TimTheoLoai(textBox1timkiem.Text, lvsanpham);
            }
        }
    }
}

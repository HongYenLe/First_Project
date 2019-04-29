using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using KLTN_UDQLKH.DTO;
using KLTN_UDQLKH.BLL;
using Microsoft.Office.Interop.Excel;

namespace KLTN_UDQLKH
{
    public partial class NhaCungCap : Form
    {
        public NhaCungCap()
        {
            InitializeComponent();

            ColumnHeader columnheader4;
            lvnhacungcap.View = View.Details;
            lvnhacungcap.GridLines = true;

            columnheader4 = new ColumnHeader(); // create id column
            columnheader4.Width = 130;
            columnheader4.Text = "Mã nhà cung cấp";
            lvnhacungcap.Columns.Add(columnheader4);

            columnheader4 = new ColumnHeader(); //create name customer column
            columnheader4.Width = 250;
            columnheader4.Text = "Tên nhà cung cấp";
            lvnhacungcap.Columns.Add(columnheader4);

            columnheader4 = new ColumnHeader(); // create gender customer column
            columnheader4.Width = 140;
            columnheader4.Text = "Điện thoại";
            lvnhacungcap.Columns.Add(columnheader4);

            columnheader4 = new ColumnHeader(); //create birthday column
            columnheader4.Width = 500;
            columnheader4.Text = "Địa chỉ";
            lvnhacungcap.Columns.Add(columnheader4);
            nccBLL.Load(lvnhacungcap);
        }

        NHA_CUNG_CAP_BLL nccBLL = new NHA_CUNG_CAP_BLL();
        NHA_CUNG_CAP_DTO nccDTO = new NHA_CUNG_CAP_DTO();

        private void button14themmoi_Click(object sender, EventArgs e)
        {
            txtmancc.Clear();
            txttenncc.Clear();
            textBox1dienthoai.Clear();
            textBox3diachi.Clear();
        }

        public bool layDL_themNCC() //Lấy dữ liệu từ txt
        {

            nccDTO.tenncc = txttenncc.Text;
            nccDTO.dienthoai = textBox1dienthoai.Text;
            nccDTO.diachi = textBox3diachi.Text;
            
            return true;
        }
        private bool IsNull_NCC() // kiểm tra thông tin trong textbox 
        {

            if (txttenncc.Text == "")
            {
                MessageBox.Show("Bạn chưa nhập tên nhà cung cấp!");
                return false;
            }

            if (textBox1dienthoai.Text == "")
            {
                MessageBox.Show("Bạn chưa nhập điện thoại!");
                return false;
            }
            if (textBox3diachi.Text == "")
            {
                MessageBox.Show("Bạn chưa nhập địa chỉ!");
                return false;
            }
            
            return true;
        }

        private void button9them_Click(object sender, EventArgs e)
        {
            if (IsNull_NCC() == false)
                return;
            if (!layDL_themNCC())
                return;
            nccBLL.ThemNCC(nccDTO, lvnhacungcap);
        }

        private void lvnhacungcap_SelectedIndexChanged(object sender, EventArgs e)
        {
            foreach (ListViewItem item in lvnhacungcap.SelectedItems)
            {
                txtmancc.Text = item.SubItems[0].Text;
                txttenncc.Text = item.SubItems[1].Text;
                textBox1dienthoai.Text = item.SubItems[2].Text;
                textBox3diachi.Text = item.SubItems[3].Text;
               
            }
        }

        public bool layDL_suaNCC() //Lấy dữ liệu từ txt
        {
            nccDTO.id = int.Parse(txtmancc.Text);
            nccDTO.tenncc = txttenncc.Text;
            nccDTO.dienthoai = textBox1dienthoai.Text;
            nccDTO.diachi = textBox3diachi.Text;
            return true;
        }

        private void button13sua_Click(object sender, EventArgs e)
        {
            if (IsNull_NCC() == false)
                return;
            if (!layDL_suaNCC())
                return;
            nccBLL.SuaNCC(nccDTO, lvnhacungcap);
        }

        private void button12xoa_Click(object sender, EventArgs e)
        {
            DialogResult rs = MessageBox.Show("Xác nhận xóa sản phẩm có mã " + txtmancc.Text, "Xác nhận", MessageBoxButtons.OKCancel);
            if (rs == DialogResult.OK)
            {
                layDL_suaNCC();
                nccBLL.XoaNCC(nccDTO.id, lvnhacungcap);

                //set default
                txtmancc.Clear();
                txttenncc.Clear();
                textBox1dienthoai.Clear();
                textBox3diachi.Clear();
            }
        }

        private void button10xuatexcel_Click(object sender, EventArgs e)
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
                        ws.Cells[1, 1] = "Mã nhà cung cấp";
                        ws.Cells[1, 2] = "Tên nhà cung cấp";
                        ws.Cells[1, 3] = "Điện thoại";
                        ws.Cells[1, 4] = "Địa chỉ";
                       
                        int i = 2;
                        foreach (ListViewItem item in lvnhacungcap.Items)
                        {
                            ws.Cells[i, 1] = item.SubItems[0].Text;
                            ws.Cells[i, 2] = item.SubItems[1].Text;
                            ws.Cells[i, 3] = item.SubItems[2].Text;
                            ws.Cells[i, 4] = item.SubItems[3].Text;
                           
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

        private void button11thoat_Click(object sender, EventArgs e)
        {
            //NhanVien ncc = new nh();
            //this.Hide();
            //ncc.ShowDialog();
            //this.Show();
            this.Close();
        }
    }
}

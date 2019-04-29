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
        

namespace KLTN_UDQLKH
{
    public partial class LichSu : Form
    {
        public LichSu()
        {
            InitializeComponent();

            ColumnHeader columnheader5;
            lvlichsu.View = View.Details;
            lvlichsu.GridLines = true;
            //lvkhachhang.FullRowSelect = true;

            columnheader5 = new ColumnHeader(); // create id column
            columnheader5.Width = 100;
            columnheader5.Text = "Mã lịch sử";
            lvlichsu.Columns.Add(columnheader5);

            columnheader5 = new ColumnHeader(); // create id column
            columnheader5.Width = 100;
            columnheader5.Text = "Mã khách hàng";
            lvlichsu.Columns.Add(columnheader5);

            columnheader5 = new ColumnHeader(); // create id column
            columnheader5.Width = 100;
            columnheader5.Text = "Mã nhân viên";
            lvlichsu.Columns.Add(columnheader5);

            columnheader5 = new ColumnHeader(); // create id column
            columnheader5.Width = 100;
            columnheader5.Text = "Mã hóa đơn";
            lvlichsu.Columns.Add(columnheader5);

            columnheader5 = new ColumnHeader(); // create id column
            columnheader5.Width = 150;
            columnheader5.Text = "Tổng thanh toán";
            lvlichsu.Columns.Add(columnheader5);

            columnheader5 = new ColumnHeader(); // create id column
            columnheader5.Width = 150;
            columnheader5.Text = "Ngày mua hàng";
            lvlichsu.Columns.Add(columnheader5);
        }

        CT_HOADON_BLL ctBLL = new CT_HOADON_BLL();
        private void LichSu_Load(object sender, EventArgs e)
        {
            ctBLL.LoadLS(lvlichsu);
        }

        private void textBox2makh_TextChanged(object sender, EventArgs e)
        {
            if (textBox2makh.Text != "")
            {
                textBox1soluong.Text = ctBLL.TimkiemLS(int.Parse(textBox2makh.Text.Trim())).ToString();
                ctBLL.Hienthi_TimkiemLS(lvlichsu, int.Parse(textBox2makh.Text.Trim()));
            }
            else
            {
                ctBLL.LoadLS(lvlichsu);
            }
            
        }
    }
}

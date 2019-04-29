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
namespace KLTN_UDQLKH
{
    public partial class XemQT : Form
    {
        public XemQT()
        {
            InitializeComponent();

            ColumnHeader columnheader;
            lvquatang.View = View.Details;
            lvquatang.GridLines = true;
            
            columnheader = new ColumnHeader(); 
            columnheader.Width = 50;
            columnheader.Text = "Mã quà tặng";
            lvquatang.Columns.Add(columnheader);

            columnheader = new ColumnHeader();
            columnheader.Width = 50;
            columnheader.Text = "Mã khách hàng";
            lvquatang.Columns.Add(columnheader);

            columnheader = new ColumnHeader();
            columnheader.Width = 150;
            columnheader.Text = "Tên quà tặng";
            lvquatang.Columns.Add(columnheader);

            columnheader = new ColumnHeader();
            columnheader.Width = 350;
            columnheader.Text = "Nội dung";
            lvquatang.Columns.Add(columnheader);

            columnheader = new ColumnHeader();
            columnheader.Width = 80;
            columnheader.Text = "Số lượng";
            lvquatang.Columns.Add(columnheader);

            columnheader = new ColumnHeader();
            columnheader.Width = 80;
            columnheader.Text = "Tiền tương ứng";
            lvquatang.Columns.Add(columnheader);

            columnheader = new ColumnHeader();
            columnheader.Width = 100;
            columnheader.Text = "Trạng thái";
            lvquatang.Columns.Add(columnheader);
        }

        NangCapVaUuDai nc = new NangCapVaUuDai();

        private void XemQT_Load(object sender, EventArgs e)
        {
            nc.XemUuDai(lvquatang);
        }
    }
}

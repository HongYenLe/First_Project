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
using System.Xml.Linq;

namespace KLTN_UDQLKH
{
    public partial class DOI_MAT_KHAU : Form
    {
        string tendn;
        public DOI_MAT_KHAU(string _tendn)
        {
            InitializeComponent();
            tendn = _tendn;
        }

        private void txtmk_OnValueChanged(object sender, EventArgs e)
        {
            txtmk.isPassword = true;
        }

        private void txt_passwordnew_OnValueChanged(object sender, EventArgs e)
        {
            txt_passwordnew.isPassword = true;
        }

        private void txtxacnhan_OnValueChanged(object sender, EventArgs e)
        {
            txtxacnhan.isPassword = true;
        }

        private void label1hien_Click(object sender, EventArgs e)
        {
            txtmk.isPassword = false;
            label1hien.Visible = false;
            label1an.Visible = true;
        }

        private void label1an_Click(object sender, EventArgs e)
        {
            txtmk.isPassword = true;
            label1hien.Visible = true;
            label1an.Visible = false;
        }

        private void label2an_Click(object sender, EventArgs e)
        {
            txt_passwordnew.isPassword = true;
            label2hien.Visible = true;
            label2an.Visible = false;
        }

        private void label2hien_Click(object sender, EventArgs e)
        {
            txt_passwordnew.isPassword = false;
            label2hien.Visible = false;
            label2an.Visible = true;
        }

        private void label7hien_Click(object sender, EventArgs e)
        {
            txtxacnhan.isPassword = false;
            label7hien.Visible = false;
            label3an.Visible = true;
        }

        private void label3an_Click(object sender, EventArgs e)
        {
            txtxacnhan.isPassword = true;
            label7hien.Visible = true;
            label3an.Visible = false;
        }

        DoiQuaBLL dq = new DoiQuaBLL();
        XDocument test = XDocument.Load("..\\..\\Data\\KHACH_HANG.xml");
        private void btlogin_Click(object sender, EventArgs e)
        {
            var node = (from kh in test.Descendants("khachhang")
                        where (string)kh.Element("tendn").Value == tendn
                        select new
                        {
                            id = (string)kh.Attribute("id")
                        }).ToList();
                       
            foreach(var i in node)
            {
                if (txt_passwordnew.Text == txtxacnhan.Text)
                {
                    dq.DoiMatKhau(i.id, txtmk.Text, txt_passwordnew.Text);
                }
                else
                    MessageBox.Show("sai mật khẩu");
            }
            
        }
    }
}

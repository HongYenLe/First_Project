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
namespace KLTN_UDQLKH
{
    public partial class Xemhoadon : Form
    {
        int ma;
        double tiennhan;
        int soluongsp;
        public Xemhoadon(int _mahd, double _tiennhan,int _soluongsp)
        {
            InitializeComponent();
            ma = _mahd;
            tiennhan = _tiennhan;
            soluongsp = _soluongsp;
        }

        private void Xemhoadon_Load(object sender, EventArgs e)
        {
            List<InHoaDon_DTO> data = new List<InHoaDon_DTO>();
            CT_HOADON_BLL bll = new CT_HOADON_BLL();
            data = bll.InHoaDon(ma,tiennhan,soluongsp);
            InHoaDon rpt = new InHoaDon();
            rpt.SetDataSource (data);
            
            crystalReportViewer1.ReportSource = rpt;

        }
    }
}

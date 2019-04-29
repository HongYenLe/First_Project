using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing.Imaging;
using OnBarcode.Barcode;
using OnBarcode.Barcode.BarcodeScanner;
using System.IO;
using ZXing;
using KLTN_UDQLKH.DTO;
using KLTN_UDQLKH.BLL;
using System.Xml.Linq;
using System.Xml;

namespace KLTN_UDQLKH
{
    public partial class InTheKhachHang : Form
    {

        private readonly IBarcodeReader barcodeReader;
        public InTheKhachHang()
        {
            InitializeComponent();
            

            barcodeReader = new BarcodeReader();

            ColumnHeader columnheader;
            lvttkh.View = View.Details;
            lvttkh.GridLines = true;
            
            columnheader = new ColumnHeader(); // create id column
            columnheader.Width = 70;
            columnheader.Text = "Mã KH";
            lvttkh.Columns.Add(columnheader);

            columnheader = new ColumnHeader(); 
            columnheader.Width = 200;
            columnheader.Text = "Tên KH";
            lvttkh.Columns.Add(columnheader);

            columnheader = new ColumnHeader(); 
            columnheader.Width = 100;
            columnheader.Text = "Điện thoại";
            lvttkh.Columns.Add(columnheader);

            columnheader = new ColumnHeader(); 
            columnheader.Width = 200;
            columnheader.Text = "Địa chỉ";
            lvttkh.Columns.Add(columnheader);

            columnheader = new ColumnHeader();
            columnheader.Width = 170;
            columnheader.Text = "Loại thẻ";
            lvttkh.Columns.Add(columnheader);

        }

        private void GenerateQrcode(string _data, string _filename)
        {
            QRCode qrcode = new QRCode();
            qrcode.Data = _data;
            qrcode.DataMode = QRCodeDataMode.Byte;
            qrcode.UOM = UnitOfMeasure.PIXEL;
            qrcode.X = 3;
            qrcode.LeftMargin = 0;
            qrcode.RightMargin = 0;
            qrcode.TopMargin = 0;
            qrcode.BottomMargin = 0;
            qrcode.Resolution = 72;
            qrcode.Rotate = Rotate.Rotate0;
            qrcode.ImageFormat = ImageFormat.Jpeg;
            qrcode.drawBarcode(_filename);
        }
        private void GenerateBacode(string _data, string _filename)
        {
            Linear barcode = new Linear();
            barcode.Type = OnBarcode.Barcode.BarcodeType.CODE39;
            barcode.Data = _data;
            //barcode.SupData = textBox6tenkh.Text;
            //barcode.Data = textBox6tenkh.Text;
            barcode.drawBarcode(_filename);

        }

        
       
        
        private void button3_Click(object sender, EventArgs e)
        {
            Random rd = new Random();
            textBox2.Text = "C:\\Users\\Administrator\\Desktop\\KLTN_UDQLKH\\KLTN_UDQLKH\\icon\\" + rd.Next(1, 100).ToString() + ".jpg";
            GenerateQrcode(textBox1.Text, textBox2.Text);

            List<TheKH> data = new List<TheKH>();
            CT_HOADON_BLL bll = new CT_HOADON_BLL();
            data = bll.Print(textBox1.Text, textBox6tenkh.Text, textBox2.Text, textBox6loai.Text);
            InTheKH rpt1 = new InTheKH();
            rpt1.SetDataSource(data);

            crystalReportViewer1.ReportSource = rpt1;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Random rd = new Random();
            textBox2.Text = "C:\\Users\\Administrator\\Desktop\\KLTN_UDQLKH\\KLTN_UDQLKH\\icon\\" + rd.Next(1, 100).ToString() + ".gif";
            //MessageBox.Show("textBox1.Text.Trim()"+ textBox1.Text.Trim()+ "textBox2.Text.Trim()"+ textBox2.Text.Trim());
            GenerateBacode(textBox1.Text.Trim(), textBox2.Text.Trim());

            List<TheKH> data = new List<TheKH>();
            CT_HOADON_BLL bll = new CT_HOADON_BLL();
            data = bll.Print(textBox1.Text, textBox6tenkh.Text, textBox2.Text, textBox6loai.Text);
            InTheKH rpt = new InTheKH();
            rpt.SetDataSource(data);

            crystalReportViewer1.ReportSource = rpt;

        }

        private void button4_Click(object sender, EventArgs e)
        {
            try
            {
                if (textBox3.Text != "")
                {
                    string text = "";
                    //Can Be use to get data from Barcode Image File.
                    String[] Data = ReadBarcodeFromFile(textBox3.Text);
                    foreach (var item in Data)
                    {
                        text += item.ToString();
                    }
                    textBox4.Text = text;

                    //Can Be Use to get data from bitmap barcode images.

                    //String[] Data = ReadBarcodeFromBitmap((Bitmap)pictureBox1.Image);
                    //foreach (var item in Data)
                    //{
                    //    text += item.ToString();
                    //}
                    //textBox2.Text = text;
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        private String[] ReadBarcodeFromFile(string _Filepath)
        {
            String[] barcodes = BarcodeScanner.Scan(_Filepath, OnBarcode.Barcode.BarcodeScanner.BarcodeType.Code39);
            return barcodes;
        }
        private String[] ReadBarcodeFromBitmap(Bitmap _bimapimage)
        {
            System.Drawing.Bitmap objImage = _bimapimage;
            String[] barcodes = BarcodeScanner.Scan(objImage, OnBarcode.Barcode.BarcodeScanner.BarcodeType.Code39);
            return barcodes;
        }

        private void button5_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.ShowDialog();
            textBox3.Text = ofd.FileName.ToString();
            pictureBox1.Load(ofd.FileName);
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void button6_Click(object sender, EventArgs e)
        {
            using (var openDlg = new OpenFileDialog())
            {
                openDlg.Multiselect = false;
                if (openDlg.ShowDialog(this) == DialogResult.OK)
                {
                    string fileName = openDlg.FileName;
                    if (!File.Exists(fileName))
                    {
                        return;
                    }

                    var image = (Bitmap)Bitmap.FromFile(fileName);
                    pictureBox1.Image = image;

                    var result = barcodeReader.Decode(image);
                    if (result == null)
                    {
                        result = barcodeReader.Decode(image);
                    }

                    if (result == null)
                    {
                        MessageBox.Show("Không nhận diện được mã QR Code",
                        "QR Code Generator");
                    }
                    else
                    {
                        textBox5.Text = result.Text;
                    }
                }
            }
        }

        private void button8tim_Click(object sender, EventArgs e)
        {

            try
            {
                lvttkh.Items.Clear();
                DataSet dataSet = new DataSet();
                DataTable dt = new DataTable();
                dataSet.ReadXml("..\\..\\Data\\KH_LOAIKH.xml");
                dt = dataSet.Tables["Join"];
                int co = 0;
                int i = 0;
                foreach (DataRow dr in dt.Rows)
                {
                    if (dr["dienthoai"].ToString() == textBox6sdt.Text.Trim())
                    {
                        //MessageBox.Show(dr["id"].ToString());
                        textBox1.Text = dr["id"].ToString();
                        textBox6tenkh.Text = dr["tenkh"].ToString();
                        textBox6loai.Text = dr["tenloai"].ToString();
                        lvttkh.Items.Add(dr["id"].ToString());
                        lvttkh.Items[i].SubItems.Add(dr["tenkh"].ToString());
                        
                        lvttkh.Items[i].SubItems.Add(dr["dienthoai"].ToString());
                        lvttkh.Items[i].SubItems.Add(dr["diachi"].ToString());
                        lvttkh.Items[i].SubItems.Add(dr["tenloai"].ToString());
                        co++;
                        i++;
                    }

                }
                if (co == 0)
                {
                    MessageBox.Show("Không tìm thấy khách hàng ");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi: " + ex.Message);
            }
        }

        private void crystalReportViewer1_Load(object sender, EventArgs e)
        {
            
        }
    }
}

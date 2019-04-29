using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net.Mail;
using System.Net;

namespace KLTN_UDQLKH
{
    public partial class GuiMail : Form
    {
        public GuiMail()
        {
            InitializeComponent();
        }

        private void btgui_Click(object sender, EventArgs e)
        {
            SmtpClient client = new SmtpClient();
            client.Port = 587;
            client.Host = "smtp.gmail.com";
            client.EnableSsl = true;
            client.Timeout = 10000;
            client.DeliveryMethod = SmtpDeliveryMethod.Network;
            client.UseDefaultCredentials = false;
            client.Credentials = new System.Net.NetworkCredential(txtmailnguoigui.Text.Trim(),txtpass.Text.Trim());
            string addresses = "lethihongyen777@gmail.com;110115061@sv.tvu.edu.vn;110115005@sv.tvu.edu.vn;nguyenkhanhduy1311@gmail.com";
            MailMessage mailMessage = new MailMessage();
            foreach (var address in addresses.Split(new[] { ";" }, StringSplitOptions.RemoveEmptyEntries))
            {
                //mailMessage.To.Add(address);

                MailMessage mm = new MailMessage(txtmailnguoigui.Text.Trim(), address, txttieude.Text, txtnoidung.Text);
                mm.BodyEncoding = UTF8Encoding.UTF8;
                mm.DeliveryNotificationOptions = DeliveryNotificationOptions.OnFailure;

                client.Send(mm);
            }
        }

        private void GuiMail_Load(object sender, EventArgs e)
        {

        }
    }
}

namespace KLTN_UDQLKH
{
    partial class DangNhap
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DangNhap));
            this.panel1 = new System.Windows.Forms.Panel();
            this.content = new System.Windows.Forms.Panel();
            this.txt_password = new Bunifu.Framework.UI.BunifuMaterialTextbox();
            this.txt_username = new Bunifu.Framework.UI.BunifuMaterialTextbox();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.btlogin = new System.Windows.Forms.Button();
            this.label6 = new System.Windows.Forms.Label();
            this.panel3 = new System.Windows.Forms.Panel();
            this.label2 = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.panel4 = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.label5 = new System.Windows.Forms.Label();
            this.btthoat = new Bunifu.Framework.UI.BunifuFlatButton();
            this.btinthe = new Bunifu.Framework.UI.BunifuFlatButton();
            this.bt_dangnhap = new Bunifu.Framework.UI.BunifuFlatButton();
            this.panel1.SuspendLayout();
            this.content.SuspendLayout();
            this.panel3.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.content);
            this.panel1.Controls.Add(this.panel3);
            this.panel1.Controls.Add(this.panel2);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(930, 448);
            this.panel1.TabIndex = 10;
            // 
            // content
            // 
            this.content.BackColor = System.Drawing.Color.White;
            this.content.Controls.Add(this.pictureBox1);
            this.content.Controls.Add(this.txt_password);
            this.content.Controls.Add(this.txt_username);
            this.content.Controls.Add(this.label4);
            this.content.Controls.Add(this.label3);
            this.content.Controls.Add(this.btlogin);
            this.content.Controls.Add(this.label6);
            this.content.Dock = System.Windows.Forms.DockStyle.Fill;
            this.content.Location = new System.Drawing.Point(200, 100);
            this.content.Name = "content";
            this.content.Size = new System.Drawing.Size(730, 348);
            this.content.TabIndex = 13;
            this.content.Paint += new System.Windows.Forms.PaintEventHandler(this.content_Paint);
            // 
            // txt_password
            // 
            this.txt_password.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.txt_password.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F);
            this.txt_password.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.txt_password.HintForeColor = System.Drawing.Color.Empty;
            this.txt_password.HintText = "";
            this.txt_password.isPassword = true;
            this.txt_password.LineFocusedColor = System.Drawing.Color.Blue;
            this.txt_password.LineIdleColor = System.Drawing.Color.Gray;
            this.txt_password.LineMouseHoverColor = System.Drawing.Color.Blue;
            this.txt_password.LineThickness = 3;
            this.txt_password.Location = new System.Drawing.Point(390, 164);
            this.txt_password.Margin = new System.Windows.Forms.Padding(4);
            this.txt_password.Name = "txt_password";
            this.txt_password.Size = new System.Drawing.Size(308, 37);
            this.txt_password.TabIndex = 21;
            this.txt_password.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.txt_password.OnValueChanged += new System.EventHandler(this.txt_password_OnValueChanged);
            // 
            // txt_username
            // 
            this.txt_username.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.txt_username.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F);
            this.txt_username.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.txt_username.HintForeColor = System.Drawing.Color.Empty;
            this.txt_username.HintText = "";
            this.txt_username.isPassword = false;
            this.txt_username.LineFocusedColor = System.Drawing.Color.Blue;
            this.txt_username.LineIdleColor = System.Drawing.Color.Gray;
            this.txt_username.LineMouseHoverColor = System.Drawing.Color.Blue;
            this.txt_username.LineThickness = 3;
            this.txt_username.Location = new System.Drawing.Point(390, 106);
            this.txt_username.Margin = new System.Windows.Forms.Padding(4);
            this.txt_username.Name = "txt_username";
            this.txt_username.Size = new System.Drawing.Size(308, 35);
            this.txt_username.TabIndex = 20;
            this.txt_username.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Times New Roman", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(237, 120);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(100, 21);
            this.label4.TabIndex = 19;
            this.label4.Text = "Nhập mã số";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Times New Roman", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(237, 180);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(79, 21);
            this.label3.TabIndex = 17;
            this.label3.Text = "Mật khẩu";
            // 
            // btlogin
            // 
            this.btlogin.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.btlogin.Font = new System.Drawing.Font("Times New Roman", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btlogin.ForeColor = System.Drawing.Color.White;
            this.btlogin.Location = new System.Drawing.Point(241, 257);
            this.btlogin.Name = "btlogin";
            this.btlogin.Size = new System.Drawing.Size(457, 38);
            this.btlogin.TabIndex = 13;
            this.btlogin.Text = "Đăng nhập";
            this.btlogin.UseVisualStyleBackColor = false;
            this.btlogin.Click += new System.EventHandler(this.btlogin_Click);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.BackColor = System.Drawing.Color.White;
            this.label6.Font = new System.Drawing.Font("Times New Roman", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.ForeColor = System.Drawing.Color.DodgerBlue;
            this.label6.Location = new System.Drawing.Point(278, 37);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(378, 31);
            this.label6.TabIndex = 10;
            this.label6.Text = "Đăng nhập vào tài khoản của bạn";
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.label5);
            this.panel3.Controls.Add(this.label2);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel3.Location = new System.Drawing.Point(200, 0);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(730, 100);
            this.panel3.TabIndex = 12;
            // 
            // label2
            // 
            this.label2.Font = new System.Drawing.Font("Times New Roman", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(121, 35);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(163, 41);
            this.label2.TabIndex = 1;
            this.label2.Text = "Đăng nhập";
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(41)))), ((int)(((byte)(53)))), ((int)(((byte)(65)))));
            this.panel2.Controls.Add(this.btthoat);
            this.panel2.Controls.Add(this.btinthe);
            this.panel2.Controls.Add(this.bt_dangnhap);
            this.panel2.Controls.Add(this.panel4);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel2.Location = new System.Drawing.Point(0, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(200, 448);
            this.panel2.TabIndex = 11;
            // 
            // panel4
            // 
            this.panel4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(229)))), ((int)(((byte)(126)))), ((int)(((byte)(48)))));
            this.panel4.Controls.Add(this.label1);
            this.panel4.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel4.Location = new System.Drawing.Point(0, 0);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(200, 100);
            this.panel4.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Times New Roman", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(1, 37);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(200, 26);
            this.label1.TabIndex = 1;
            this.label1.Text = "Co.opmart System";
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackgroundImage = global::KLTN_UDQLKH.Properties.Resources.user_login_icon;
            this.pictureBox1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pictureBox1.Location = new System.Drawing.Point(15, 63);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(195, 190);
            this.pictureBox1.TabIndex = 22;
            this.pictureBox1.TabStop = false;
            // 
            // label5
            // 
            this.label5.Font = new System.Drawing.Font("Times New Roman", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Image = global::KLTN_UDQLKH.Properties.Resources.Login_icon;
            this.label5.Location = new System.Drawing.Point(57, 22);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(62, 54);
            this.label5.TabIndex = 2;
            this.label5.Text = "              ";
            // 
            // btthoat
            // 
            this.btthoat.Activecolor = System.Drawing.Color.FromArgb(((int)(((byte)(46)))), ((int)(((byte)(139)))), ((int)(((byte)(87)))));
            this.btthoat.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(41)))), ((int)(((byte)(53)))), ((int)(((byte)(65)))));
            this.btthoat.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btthoat.BorderRadius = 0;
            this.btthoat.ButtonText = "Thoát";
            this.btthoat.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btthoat.DisabledColor = System.Drawing.Color.Gray;
            this.btthoat.Dock = System.Windows.Forms.DockStyle.Top;
            this.btthoat.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btthoat.Iconcolor = System.Drawing.Color.Transparent;
            this.btthoat.Iconimage = ((System.Drawing.Image)(resources.GetObject("btthoat.Iconimage")));
            this.btthoat.Iconimage_right = null;
            this.btthoat.Iconimage_right_Selected = null;
            this.btthoat.Iconimage_Selected = null;
            this.btthoat.IconMarginLeft = 0;
            this.btthoat.IconMarginRight = 0;
            this.btthoat.IconRightVisible = true;
            this.btthoat.IconRightZoom = 0D;
            this.btthoat.IconVisible = true;
            this.btthoat.IconZoom = 90D;
            this.btthoat.IsTab = false;
            this.btthoat.Location = new System.Drawing.Point(0, 196);
            this.btthoat.Name = "btthoat";
            this.btthoat.Normalcolor = System.Drawing.Color.FromArgb(((int)(((byte)(41)))), ((int)(((byte)(53)))), ((int)(((byte)(65)))));
            this.btthoat.OnHovercolor = System.Drawing.Color.FromArgb(((int)(((byte)(36)))), ((int)(((byte)(129)))), ((int)(((byte)(77)))));
            this.btthoat.OnHoverTextColor = System.Drawing.Color.White;
            this.btthoat.selected = false;
            this.btthoat.Size = new System.Drawing.Size(200, 48);
            this.btthoat.TabIndex = 3;
            this.btthoat.Text = "Thoát";
            this.btthoat.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btthoat.Textcolor = System.Drawing.Color.White;
            this.btthoat.TextFont = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btthoat.Click += new System.EventHandler(this.btthoat_Click);
            // 
            // btinthe
            // 
            this.btinthe.Activecolor = System.Drawing.Color.FromArgb(((int)(((byte)(46)))), ((int)(((byte)(139)))), ((int)(((byte)(87)))));
            this.btinthe.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(41)))), ((int)(((byte)(53)))), ((int)(((byte)(65)))));
            this.btinthe.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btinthe.BorderRadius = 0;
            this.btinthe.ButtonText = "In thẻ KH";
            this.btinthe.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btinthe.DisabledColor = System.Drawing.Color.Gray;
            this.btinthe.Dock = System.Windows.Forms.DockStyle.Top;
            this.btinthe.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btinthe.Iconcolor = System.Drawing.Color.Transparent;
            this.btinthe.Iconimage = ((System.Drawing.Image)(resources.GetObject("btinthe.Iconimage")));
            this.btinthe.Iconimage_right = null;
            this.btinthe.Iconimage_right_Selected = null;
            this.btinthe.Iconimage_Selected = null;
            this.btinthe.IconMarginLeft = 0;
            this.btinthe.IconMarginRight = 0;
            this.btinthe.IconRightVisible = true;
            this.btinthe.IconRightZoom = 0D;
            this.btinthe.IconVisible = true;
            this.btinthe.IconZoom = 90D;
            this.btinthe.IsTab = false;
            this.btinthe.Location = new System.Drawing.Point(0, 148);
            this.btinthe.Name = "btinthe";
            this.btinthe.Normalcolor = System.Drawing.Color.FromArgb(((int)(((byte)(41)))), ((int)(((byte)(53)))), ((int)(((byte)(65)))));
            this.btinthe.OnHovercolor = System.Drawing.Color.FromArgb(((int)(((byte)(36)))), ((int)(((byte)(129)))), ((int)(((byte)(77)))));
            this.btinthe.OnHoverTextColor = System.Drawing.Color.White;
            this.btinthe.selected = false;
            this.btinthe.Size = new System.Drawing.Size(200, 48);
            this.btinthe.TabIndex = 2;
            this.btinthe.Text = "In thẻ KH";
            this.btinthe.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btinthe.Textcolor = System.Drawing.Color.White;
            this.btinthe.TextFont = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btinthe.Click += new System.EventHandler(this.btinthe_Click);
            // 
            // bt_dangnhap
            // 
            this.bt_dangnhap.Activecolor = System.Drawing.Color.FromArgb(((int)(((byte)(46)))), ((int)(((byte)(139)))), ((int)(((byte)(87)))));
            this.bt_dangnhap.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(41)))), ((int)(((byte)(53)))), ((int)(((byte)(65)))));
            this.bt_dangnhap.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.bt_dangnhap.BorderRadius = 0;
            this.bt_dangnhap.ButtonText = "Đăng nhập";
            this.bt_dangnhap.Cursor = System.Windows.Forms.Cursors.Hand;
            this.bt_dangnhap.DisabledColor = System.Drawing.Color.Gray;
            this.bt_dangnhap.Dock = System.Windows.Forms.DockStyle.Top;
            this.bt_dangnhap.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.bt_dangnhap.Iconcolor = System.Drawing.Color.Transparent;
            this.bt_dangnhap.Iconimage = ((System.Drawing.Image)(resources.GetObject("bt_dangnhap.Iconimage")));
            this.bt_dangnhap.Iconimage_right = null;
            this.bt_dangnhap.Iconimage_right_Selected = null;
            this.bt_dangnhap.Iconimage_Selected = null;
            this.bt_dangnhap.IconMarginLeft = 0;
            this.bt_dangnhap.IconMarginRight = 0;
            this.bt_dangnhap.IconRightVisible = true;
            this.bt_dangnhap.IconRightZoom = 0D;
            this.bt_dangnhap.IconVisible = true;
            this.bt_dangnhap.IconZoom = 90D;
            this.bt_dangnhap.IsTab = false;
            this.bt_dangnhap.Location = new System.Drawing.Point(0, 100);
            this.bt_dangnhap.Name = "bt_dangnhap";
            this.bt_dangnhap.Normalcolor = System.Drawing.Color.FromArgb(((int)(((byte)(41)))), ((int)(((byte)(53)))), ((int)(((byte)(65)))));
            this.bt_dangnhap.OnHovercolor = System.Drawing.Color.FromArgb(((int)(((byte)(36)))), ((int)(((byte)(129)))), ((int)(((byte)(77)))));
            this.bt_dangnhap.OnHoverTextColor = System.Drawing.Color.White;
            this.bt_dangnhap.selected = false;
            this.bt_dangnhap.Size = new System.Drawing.Size(200, 48);
            this.bt_dangnhap.TabIndex = 1;
            this.bt_dangnhap.Text = "Đăng nhập";
            this.bt_dangnhap.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.bt_dangnhap.Textcolor = System.Drawing.Color.White;
            this.bt_dangnhap.TextFont = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            // 
            // DangNhap
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(930, 448);
            this.Controls.Add(this.panel1);
            this.Name = "DangNhap";
            this.Text = "DangNhap";
            this.Load += new System.EventHandler(this.DangNhap_Load);
            this.panel1.ResumeLayout(false);
            this.content.ResumeLayout(false);
            this.content.PerformLayout();
            this.panel3.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.panel4.ResumeLayout(false);
            this.panel4.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel content;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button btlogin;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Panel panel2;
        private Bunifu.Framework.UI.BunifuFlatButton btinthe;
        private Bunifu.Framework.UI.BunifuFlatButton bt_dangnhap;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.Label label1;
        private Bunifu.Framework.UI.BunifuMaterialTextbox txt_password;
        private Bunifu.Framework.UI.BunifuMaterialTextbox txt_username;
        private System.Windows.Forms.Label label2;
        private Bunifu.Framework.UI.BunifuFlatButton btthoat;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Label label5;
    }
}
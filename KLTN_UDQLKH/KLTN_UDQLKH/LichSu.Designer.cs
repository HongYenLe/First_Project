namespace KLTN_UDQLKH
{
    partial class LichSu
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
            this.groupBox7 = new System.Windows.Forms.GroupBox();
            this.textBox2makh = new System.Windows.Forms.TextBox();
            this.label38 = new System.Windows.Forms.Label();
            this.textBox1soluong = new System.Windows.Forms.TextBox();
            this.label39 = new System.Windows.Forms.Label();
            this.lvlichsu = new DevComponents.DotNetBar.Controls.ListViewEx();
            this.button4timkiemlichsu = new System.Windows.Forms.Button();
            this.groupBox7.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox7
            // 
            this.groupBox7.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox7.BackColor = System.Drawing.Color.PowderBlue;
            this.groupBox7.Controls.Add(this.textBox2makh);
            this.groupBox7.Controls.Add(this.button4timkiemlichsu);
            this.groupBox7.Controls.Add(this.label38);
            this.groupBox7.Controls.Add(this.textBox1soluong);
            this.groupBox7.Controls.Add(this.label39);
            this.groupBox7.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.groupBox7.Font = new System.Drawing.Font("Times New Roman", 12F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox7.Location = new System.Drawing.Point(12, 10);
            this.groupBox7.Name = "groupBox7";
            this.groupBox7.Size = new System.Drawing.Size(981, 59);
            this.groupBox7.TabIndex = 86;
            this.groupBox7.TabStop = false;
            this.groupBox7.Text = "Thống kê theo";
            // 
            // textBox2makh
            // 
            this.textBox2makh.Font = new System.Drawing.Font("Palatino Linotype", 12F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.textBox2makh.Location = new System.Drawing.Point(205, 18);
            this.textBox2makh.Name = "textBox2makh";
            this.textBox2makh.Size = new System.Drawing.Size(195, 29);
            this.textBox2makh.TabIndex = 87;
            this.textBox2makh.TextChanged += new System.EventHandler(this.textBox2makh_TextChanged);
            // 
            // label38
            // 
            this.label38.AutoSize = true;
            this.label38.Font = new System.Drawing.Font("Palatino Linotype", 12.75F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label38.ForeColor = System.Drawing.Color.Blue;
            this.label38.Location = new System.Drawing.Point(588, 22);
            this.label38.Name = "label38";
            this.label38.Size = new System.Drawing.Size(136, 23);
            this.label38.TabIndex = 84;
            this.label38.Text = "Số lượng hóa đơn";
            // 
            // textBox1soluong
            // 
            this.textBox1soluong.Font = new System.Drawing.Font("Palatino Linotype", 12F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.textBox1soluong.Location = new System.Drawing.Point(765, 18);
            this.textBox1soluong.Name = "textBox1soluong";
            this.textBox1soluong.Size = new System.Drawing.Size(195, 29);
            this.textBox1soluong.TabIndex = 6;
            // 
            // label39
            // 
            this.label39.AutoSize = true;
            this.label39.Font = new System.Drawing.Font("Palatino Linotype", 12.75F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label39.ForeColor = System.Drawing.Color.Blue;
            this.label39.Location = new System.Drawing.Point(51, 22);
            this.label39.Name = "label39";
            this.label39.Size = new System.Drawing.Size(123, 23);
            this.label39.TabIndex = 69;
            this.label39.Text = "Mã khách hàng";
            // 
            // lvlichsu
            // 
            // 
            // 
            // 
            this.lvlichsu.Border.Class = "ListViewBorder";
            this.lvlichsu.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.lvlichsu.DisabledBackColor = System.Drawing.Color.Empty;
            this.lvlichsu.Location = new System.Drawing.Point(12, 75);
            this.lvlichsu.Name = "lvlichsu";
            this.lvlichsu.Size = new System.Drawing.Size(970, 364);
            this.lvlichsu.TabIndex = 0;
            this.lvlichsu.UseCompatibleStateImageBehavior = false;
            // 
            // button4timkiemlichsu
            // 
            this.button4timkiemlichsu.BackColor = System.Drawing.Color.Azure;
            this.button4timkiemlichsu.Font = new System.Drawing.Font("Palatino Linotype", 12.75F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button4timkiemlichsu.Location = new System.Drawing.Point(436, 15);
            this.button4timkiemlichsu.Name = "button4timkiemlichsu";
            this.button4timkiemlichsu.Size = new System.Drawing.Size(126, 35);
            this.button4timkiemlichsu.TabIndex = 86;
            this.button4timkiemlichsu.Text = "Tìm kiếm";
            this.button4timkiemlichsu.UseVisualStyleBackColor = false;
            // 
            // LichSu
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1041, 446);
            this.Controls.Add(this.groupBox7);
            this.Controls.Add(this.lvlichsu);
            this.Name = "LichSu";
            this.Text = "LichSu";
            this.Load += new System.EventHandler(this.LichSu_Load);
            this.groupBox7.ResumeLayout(false);
            this.groupBox7.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.GroupBox groupBox7;
        private System.Windows.Forms.TextBox textBox2makh;
        private System.Windows.Forms.Label label38;
        private System.Windows.Forms.TextBox textBox1soluong;
        private System.Windows.Forms.Label label39;
        private DevComponents.DotNetBar.Controls.ListViewEx lvlichsu;
        private System.Windows.Forms.Button button4timkiemlichsu;
    }
}
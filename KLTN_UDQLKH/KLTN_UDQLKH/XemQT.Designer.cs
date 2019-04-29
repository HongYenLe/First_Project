namespace KLTN_UDQLKH
{
    partial class XemQT
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
            this.label39 = new System.Windows.Forms.Label();
            this.lvquatang = new DevComponents.DotNetBar.Controls.ListViewEx();
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
            this.groupBox7.Controls.Add(this.label39);
            this.groupBox7.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.groupBox7.Font = new System.Drawing.Font("Times New Roman", 12F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox7.Location = new System.Drawing.Point(12, 9);
            this.groupBox7.Name = "groupBox7";
            this.groupBox7.Size = new System.Drawing.Size(969, 60);
            this.groupBox7.TabIndex = 88;
            this.groupBox7.TabStop = false;
            this.groupBox7.Text = "Tìm kiếm theo";
            // 
            // textBox2makh
            // 
            this.textBox2makh.Font = new System.Drawing.Font("Palatino Linotype", 12F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.textBox2makh.Location = new System.Drawing.Point(205, 18);
            this.textBox2makh.Name = "textBox2makh";
            this.textBox2makh.Size = new System.Drawing.Size(195, 29);
            this.textBox2makh.TabIndex = 87;
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
            // lvquatang
            // 
            // 
            // 
            // 
            this.lvquatang.Border.Class = "ListViewBorder";
            this.lvquatang.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.lvquatang.DisabledBackColor = System.Drawing.Color.Empty;
            this.lvquatang.Location = new System.Drawing.Point(12, 74);
            this.lvquatang.Name = "lvquatang";
            this.lvquatang.Size = new System.Drawing.Size(970, 364);
            this.lvquatang.TabIndex = 87;
            this.lvquatang.UseCompatibleStateImageBehavior = false;
            // 
            // XemQT
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(993, 445);
            this.Controls.Add(this.groupBox7);
            this.Controls.Add(this.lvquatang);
            this.Name = "XemQT";
            this.Text = "XemQT";
            this.Load += new System.EventHandler(this.XemQT_Load);
            this.groupBox7.ResumeLayout(false);
            this.groupBox7.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox7;
        private System.Windows.Forms.TextBox textBox2makh;
        private System.Windows.Forms.Label label39;
        private DevComponents.DotNetBar.Controls.ListViewEx lvquatang;
    }
}
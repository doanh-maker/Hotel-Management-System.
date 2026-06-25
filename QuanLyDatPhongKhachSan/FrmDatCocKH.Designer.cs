namespace QuanLyDatPhongKhachSan
{
    partial class FrmDatCocKH
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
            this.label1 = new System.Windows.Forms.Label();
            this.lblDatCoc = new System.Windows.Forms.Label();
            this.btnDatCoc = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(112, 102);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(82, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Tiền đặt cọc là:";
            // 
            // lblDatCoc
            // 
            this.lblDatCoc.AutoSize = true;
            this.lblDatCoc.Location = new System.Drawing.Point(226, 102);
            this.lblDatCoc.Name = "lblDatCoc";
            this.lblDatCoc.Size = new System.Drawing.Size(35, 13);
            this.lblDatCoc.TabIndex = 1;
            this.lblDatCoc.Text = "label2";
            // 
            // btnDatCoc
            // 
            this.btnDatCoc.Location = new System.Drawing.Point(115, 260);
            this.btnDatCoc.Name = "btnDatCoc";
            this.btnDatCoc.Size = new System.Drawing.Size(75, 23);
            this.btnDatCoc.TabIndex = 2;
            this.btnDatCoc.Text = "Đặt cọc";
            this.btnDatCoc.UseVisualStyleBackColor = true;
            this.btnDatCoc.Click += new System.EventHandler(this.btnDatCoc_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(112, 145);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(151, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "Vui lòng đặt cọc để giữ phòng";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(0, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(35, 13);
            this.label3.TabIndex = 4;
            this.label3.Text = "label3";
            // 
            // FrmDatCocKH
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.btnDatCoc);
            this.Controls.Add(this.lblDatCoc);
            this.Controls.Add(this.label1);
            this.Name = "FrmDatCocKH";
            this.Text = " FrmDatCoc";
            this.Load += new System.EventHandler(this.FrmDatCoc_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lblDatCoc;
        private System.Windows.Forms.Button btnDatCoc;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
    }
}
namespace QuanLyDatPhongKhachSan
{
    partial class FrmDanhSachDatPhongKH
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
            this.lblDanhSachDatPhong = new System.Windows.Forms.Label();
            this.dgvDanhSachDatPhong = new System.Windows.Forms.DataGridView();
            this.btnYeuCauHuy = new System.Windows.Forms.DataGridViewButtonColumn();
            this.btnXemChiTiet = new System.Windows.Forms.DataGridViewButtonColumn();
            ((System.ComponentModel.ISupportInitialize)(this.dgvDanhSachDatPhong)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(50, 23);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(111, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Danh sách đặt phòng";
            // 
            // lblDanhSachDatPhong
            // 
            this.lblDanhSachDatPhong.AutoSize = true;
            this.lblDanhSachDatPhong.Location = new System.Drawing.Point(53, 63);
            this.lblDanhSachDatPhong.Name = "lblDanhSachDatPhong";
            this.lblDanhSachDatPhong.Size = new System.Drawing.Size(35, 13);
            this.lblDanhSachDatPhong.TabIndex = 1;
            this.lblDanhSachDatPhong.Text = "label2";
            // 
            // dgvDanhSachDatPhong
            // 
            this.dgvDanhSachDatPhong.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvDanhSachDatPhong.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.btnYeuCauHuy,
            this.btnXemChiTiet});
            this.dgvDanhSachDatPhong.Location = new System.Drawing.Point(12, 107);
            this.dgvDanhSachDatPhong.Name = "dgvDanhSachDatPhong";
            this.dgvDanhSachDatPhong.Size = new System.Drawing.Size(1106, 320);
            this.dgvDanhSachDatPhong.TabIndex = 2;
            this.dgvDanhSachDatPhong.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvDanhSachDatPhong_CellContentClick);
            // 
            // btnYeuCauHuy
            // 
            this.btnYeuCauHuy.HeaderText = "Yêu cầu huỷ";
            this.btnYeuCauHuy.Name = "btnYeuCauHuy";
            this.btnYeuCauHuy.Text = "Yêu cầu huỷ";
            this.btnYeuCauHuy.UseColumnTextForButtonValue = true;
            // 
            // btnXemChiTiet
            // 
            this.btnXemChiTiet.HeaderText = "Xem chi tiết";
            this.btnXemChiTiet.Name = "btnXemChiTiet";
            this.btnXemChiTiet.Text = "Xem chi tiết";
            this.btnXemChiTiet.UseColumnTextForButtonValue = true;
            // 
            // FrmDanhSachDatPhongKH
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.dgvDanhSachDatPhong);
            this.Controls.Add(this.lblDanhSachDatPhong);
            this.Controls.Add(this.label1);
            this.Name = "FrmDanhSachDatPhongKH";
            this.Text = "FrmDanhSachDatPhong";
            this.Load += new System.EventHandler(this.FrmDanhSachDatPhong_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvDanhSachDatPhong)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lblDanhSachDatPhong;
        private System.Windows.Forms.DataGridView dgvDanhSachDatPhong;
        private System.Windows.Forms.DataGridViewButtonColumn btnYeuCauHuy;
        private System.Windows.Forms.DataGridViewButtonColumn btnXemChiTiet;
    }
}
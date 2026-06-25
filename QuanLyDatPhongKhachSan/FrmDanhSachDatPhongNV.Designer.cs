namespace QuanLyDatPhongKhachSan
{
    partial class FrmDanhSachDatPhongNV
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
            this.dgvDanhSachDatPhong = new System.Windows.Forms.DataGridView();
            this.btnChiTiet = new System.Windows.Forms.DataGridViewButtonColumn();
            ((System.ComponentModel.ISupportInitialize)(this.dgvDanhSachDatPhong)).BeginInit();
            this.SuspendLayout();
            // 
            // dgvDanhSachDatPhong
            // 
            this.dgvDanhSachDatPhong.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvDanhSachDatPhong.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.btnChiTiet});
            this.dgvDanhSachDatPhong.Location = new System.Drawing.Point(12, 77);
            this.dgvDanhSachDatPhong.Name = "dgvDanhSachDatPhong";
            this.dgvDanhSachDatPhong.Size = new System.Drawing.Size(1139, 361);
            this.dgvDanhSachDatPhong.TabIndex = 0;
            this.dgvDanhSachDatPhong.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvDanhSachDatPhong_CellContentClick);
            // 
            // btnChiTiet
            // 
            this.btnChiTiet.HeaderText = "Chi tiết";
            this.btnChiTiet.Name = "btnChiTiet";
            this.btnChiTiet.Text = "Xem";
            this.btnChiTiet.UseColumnTextForButtonValue = true;
            // 
            // FrmDanhSachDatPhongNV
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.dgvDanhSachDatPhong);
            this.Name = "FrmDanhSachDatPhongNV";
            this.Text = "FrmDanhSachDatPhongNV";
            this.Load += new System.EventHandler(this.FrmDanhSachDatPhongNV_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvDanhSachDatPhong)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dgvDanhSachDatPhong;
        private System.Windows.Forms.DataGridViewButtonColumn btnChiTiet;
    }
}
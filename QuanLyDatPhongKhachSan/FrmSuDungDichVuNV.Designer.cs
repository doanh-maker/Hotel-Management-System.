namespace QuanLyDatPhongKhachSan
{
    partial class FrmSuDungDichVuNV
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
            this.Lable1 = new System.Windows.Forms.Label();
            this.txtTimKiem = new System.Windows.Forms.TextBox();
            this.btnTimKiem = new System.Windows.Forms.Button();
            this.dgvDanhSachDonDatCanSuDungDV = new System.Windows.Forms.DataGridView();
            ((System.ComponentModel.ISupportInitialize)(this.dgvDanhSachDonDatCanSuDungDV)).BeginInit();
            this.SuspendLayout();
            // 
            // Lable1
            // 
            this.Lable1.AutoSize = true;
            this.Lable1.Location = new System.Drawing.Point(100, 32);
            this.Lable1.Name = "Lable1";
            this.Lable1.Size = new System.Drawing.Size(119, 13);
            this.Lable1.TabIndex = 0;
            this.Lable1.Text = "Nhập(họ tên, sdt,cccd):";
            // 
            // txtTimKiem
            // 
            this.txtTimKiem.Location = new System.Drawing.Point(225, 28);
            this.txtTimKiem.Name = "txtTimKiem";
            this.txtTimKiem.Size = new System.Drawing.Size(100, 20);
            this.txtTimKiem.TabIndex = 1;
            // 
            // btnTimKiem
            // 
            this.btnTimKiem.Location = new System.Drawing.Point(377, 27);
            this.btnTimKiem.Name = "btnTimKiem";
            this.btnTimKiem.Size = new System.Drawing.Size(75, 23);
            this.btnTimKiem.TabIndex = 2;
            this.btnTimKiem.Text = "Tìm kiếm";
            this.btnTimKiem.UseVisualStyleBackColor = true;
            this.btnTimKiem.Click += new System.EventHandler(this.btnTimKiem_Click);
            // 
            // dgvDanhSachDonDatCanSuDungDV
            // 
            this.dgvDanhSachDonDatCanSuDungDV.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvDanhSachDonDatCanSuDungDV.Location = new System.Drawing.Point(26, 91);
            this.dgvDanhSachDonDatCanSuDungDV.Name = "dgvDanhSachDonDatCanSuDungDV";
            this.dgvDanhSachDonDatCanSuDungDV.Size = new System.Drawing.Size(740, 314);
            this.dgvDanhSachDonDatCanSuDungDV.TabIndex = 3;
            this.dgvDanhSachDonDatCanSuDungDV.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvDanhSachDonDatCanSuDungDV_CellContentClick);
            // 
            // FrmSuDungDichVuNV
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.dgvDanhSachDonDatCanSuDungDV);
            this.Controls.Add(this.btnTimKiem);
            this.Controls.Add(this.txtTimKiem);
            this.Controls.Add(this.Lable1);
            this.Name = "FrmSuDungDichVuNV";
            this.Text = "FrmSuDungDichVuNV";
            this.Load += new System.EventHandler(this.FrmSuDungDichVuNV_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvDanhSachDonDatCanSuDungDV)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label Lable1;
        private System.Windows.Forms.TextBox txtTimKiem;
        private System.Windows.Forms.Button btnTimKiem;
        private System.Windows.Forms.DataGridView dgvDanhSachDonDatCanSuDungDV;
    }
}
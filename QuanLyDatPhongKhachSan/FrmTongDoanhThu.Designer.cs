using System;

namespace QuanLyDatPhongKhachSan
{
    partial class FrmTongDoanhThu
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
            this.dtpTuNgay = new System.Windows.Forms.DateTimePicker();
            this.dtpDenNgay = new System.Windows.Forms.DateTimePicker();
            this.crystalReportViewer1 = new CrystalDecisions.Windows.Forms.CrystalReportViewer();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.btnTimKiem = new System.Windows.Forms.Button();
            this.radKhoangNgay = new System.Windows.Forms.RadioButton();
            this.radTrongNgay = new System.Windows.Forms.RadioButton();
            this.dtpTrongNgay = new System.Windows.Forms.DateTimePicker();
            this.SuspendLayout();
            // 
            // dtpTuNgay
            // 
            this.dtpTuNgay.Location = new System.Drawing.Point(112, 45);
            this.dtpTuNgay.Name = "dtpTuNgay";
            this.dtpTuNgay.Size = new System.Drawing.Size(200, 20);
            this.dtpTuNgay.TabIndex = 0;
            // 
            // dtpDenNgay
            // 
            this.dtpDenNgay.Location = new System.Drawing.Point(112, 71);
            this.dtpDenNgay.Name = "dtpDenNgay";
            this.dtpDenNgay.Size = new System.Drawing.Size(200, 20);
            this.dtpDenNgay.TabIndex = 1;
            // 
            // crystalReportViewer1
            // 
            this.crystalReportViewer1.ActiveViewIndex = -1;
            this.crystalReportViewer1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.crystalReportViewer1.Cursor = System.Windows.Forms.Cursors.Default;
            this.crystalReportViewer1.Location = new System.Drawing.Point(-4, 97);
            this.crystalReportViewer1.Name = "crystalReportViewer1";
            this.crystalReportViewer1.Size = new System.Drawing.Size(1535, 815);
            this.crystalReportViewer1.TabIndex = 2;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(53, 45);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(49, 13);
            this.label1.TabIndex = 3;
            this.label1.Text = "Từ ngày:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(53, 77);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(56, 13);
            this.label2.TabIndex = 4;
            this.label2.Text = "Đến ngày:";
            // 
            // btnTimKiem
            // 
            this.btnTimKiem.Location = new System.Drawing.Point(786, 67);
            this.btnTimKiem.Name = "btnTimKiem";
            this.btnTimKiem.Size = new System.Drawing.Size(75, 23);
            this.btnTimKiem.TabIndex = 5;
            this.btnTimKiem.Text = "Tìm kiếm";
            this.btnTimKiem.UseVisualStyleBackColor = true;
            this.btnTimKiem.Click += new System.EventHandler(this.btnTimKiem_Click);
            // 
            // radKhoangNgay
            // 
            this.radKhoangNgay.AutoSize = true;
            this.radKhoangNgay.Location = new System.Drawing.Point(56, 13);
            this.radKhoangNgay.Name = "radKhoangNgay";
            this.radKhoangNgay.Size = new System.Drawing.Size(88, 17);
            this.radKhoangNgay.TabIndex = 6;
            this.radKhoangNgay.TabStop = true;
            this.radKhoangNgay.Text = "Khoảng ngày";
            this.radKhoangNgay.UseVisualStyleBackColor = true;
            this.radKhoangNgay.CheckedChanged += new System.EventHandler(this.radKhoangNgay_CheckedChanged);
            // 
            // radTrongNgay
            // 
            this.radTrongNgay.AutoSize = true;
            this.radTrongNgay.Location = new System.Drawing.Point(410, 13);
            this.radTrongNgay.Name = "radTrongNgay";
            this.radTrongNgay.Size = new System.Drawing.Size(79, 17);
            this.radTrongNgay.TabIndex = 7;
            this.radTrongNgay.TabStop = true;
            this.radTrongNgay.Text = "Trong ngày";
            this.radTrongNgay.UseVisualStyleBackColor = true;
            this.radTrongNgay.CheckedChanged += new System.EventHandler(this.radTrongNgay_CheckedChanged);
            // 
            // dtpTrongNgay
            // 
            this.dtpTrongNgay.Location = new System.Drawing.Point(410, 45);
            this.dtpTrongNgay.Name = "dtpTrongNgay";
            this.dtpTrongNgay.Size = new System.Drawing.Size(200, 20);
            this.dtpTrongNgay.TabIndex = 8;
            // 
            // FrmTongDoanhThu
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1543, 853);
            this.Controls.Add(this.dtpTrongNgay);
            this.Controls.Add(this.radTrongNgay);
            this.Controls.Add(this.radKhoangNgay);
            this.Controls.Add(this.btnTimKiem);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.crystalReportViewer1);
            this.Controls.Add(this.dtpDenNgay);
            this.Controls.Add(this.dtpTuNgay);
            this.Name = "FrmTongDoanhThu";
            this.Text = "FrmTongDoanhThu";
            this.Load += new System.EventHandler(this.FrmTongDoanhThu_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DateTimePicker dtpTuNgay;
        private System.Windows.Forms.DateTimePicker dtpDenNgay;
        private CrystalDecisions.Windows.Forms.CrystalReportViewer crystalReportViewer1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btnTimKiem;
        private System.Windows.Forms.RadioButton radKhoangNgay;
        private System.Windows.Forms.RadioButton radTrongNgay;
        private System.Windows.Forms.DateTimePicker dtpTrongNgay;
        private EventHandler dtpTrongNgay_ValueChanged;
        private EventHandler crystalReportViewer1_Load;
    }
}
using System;
using System.Windows.Forms;

namespace QuanLyDatPhongKhachSan
{
    partial class frmMain
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
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.hệThốngToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.mnvDangNhap = new System.Windows.Forms.ToolStripMenuItem();
            this.mnvThoat = new System.Windows.Forms.ToolStripMenuItem();
            this.quảnLýĐặtPhòngToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.đặtPhòngMớiToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.danhSáchĐặtPhòngToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.nghiệpVụLễTânToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.checkInToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.checkOutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.sửDụngDịchVụToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.xửLýYêuCầuHuỷToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.xemPhòngToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.giaHạnThêmNgàyỞToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.chiPhíPhátSinhToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.báoCáoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.báoCáoDoanhThuToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.hoáĐơnToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.xemVàInDanhSáchHoáĐơnToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.quảnLýDịchVụToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.hệThốngToolStripMenuItem,
            this.quảnLýĐặtPhòngToolStripMenuItem,
            this.nghiệpVụLễTânToolStripMenuItem,
            this.báoCáoToolStripMenuItem,
            this.hoáĐơnToolStripMenuItem,
            this.quảnLýDịchVụToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(800, 24);
            this.menuStrip1.TabIndex = 1;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // hệThốngToolStripMenuItem
            // 
            this.hệThốngToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnvDangNhap,
            this.mnvThoat});
            this.hệThốngToolStripMenuItem.Name = "hệThốngToolStripMenuItem";
            this.hệThốngToolStripMenuItem.Size = new System.Drawing.Size(69, 20);
            this.hệThốngToolStripMenuItem.Text = "Hệ thống";
            this.hệThốngToolStripMenuItem.Click += new System.EventHandler(this.hệThốngToolStripMenuItem_Click);
            // 
            // mnvDangNhap
            // 
            this.mnvDangNhap.Name = "mnvDangNhap";
            this.mnvDangNhap.Size = new System.Drawing.Size(132, 22);
            this.mnvDangNhap.Text = "Đăng nhập";
            this.mnvDangNhap.Click += new System.EventHandler(this.mnvDangNhap_Click);
            // 
            // mnvThoat
            // 
            this.mnvThoat.Name = "mnvThoat";
            this.mnvThoat.Size = new System.Drawing.Size(132, 22);
            this.mnvThoat.Text = "Thoát";
            // 
            // quảnLýĐặtPhòngToolStripMenuItem
            // 
            this.quảnLýĐặtPhòngToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.đặtPhòngMớiToolStripMenuItem,
            this.danhSáchĐặtPhòngToolStripMenuItem});
            this.quảnLýĐặtPhòngToolStripMenuItem.Name = "quảnLýĐặtPhòngToolStripMenuItem";
            this.quảnLýĐặtPhòngToolStripMenuItem.Size = new System.Drawing.Size(118, 20);
            this.quảnLýĐặtPhòngToolStripMenuItem.Text = "Quản lý đặt phòng";
            // 
            // đặtPhòngMớiToolStripMenuItem
            // 
            this.đặtPhòngMớiToolStripMenuItem.Name = "đặtPhòngMớiToolStripMenuItem";
            this.đặtPhòngMớiToolStripMenuItem.Size = new System.Drawing.Size(187, 22);
            this.đặtPhòngMớiToolStripMenuItem.Text = "Đặt phòng mới";
            this.đặtPhòngMớiToolStripMenuItem.Click += new System.EventHandler(this.đặtPhòngMớiToolStripMenuItem_Click);
            // 
            // danhSáchĐặtPhòngToolStripMenuItem
            // 
            this.danhSáchĐặtPhòngToolStripMenuItem.Name = "danhSáchĐặtPhòngToolStripMenuItem";
            this.danhSáchĐặtPhòngToolStripMenuItem.Size = new System.Drawing.Size(187, 22);
            this.danhSáchĐặtPhòngToolStripMenuItem.Text = "Danh sách đặt phòng";
            this.danhSáchĐặtPhòngToolStripMenuItem.Click += new System.EventHandler(this.danhSáchĐặtPhòngToolStripMenuItem_Click);
            // 
            // nghiệpVụLễTânToolStripMenuItem
            // 
            this.nghiệpVụLễTânToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.checkInToolStripMenuItem,
            this.checkOutToolStripMenuItem,
            this.sửDụngDịchVụToolStripMenuItem,
            this.xửLýYêuCầuHuỷToolStripMenuItem,
            this.xemPhòngToolStripMenuItem1,
            this.giaHạnThêmNgàyỞToolStripMenuItem,
            this.chiPhíPhátSinhToolStripMenuItem});
            this.nghiệpVụLễTânToolStripMenuItem.Name = "nghiệpVụLễTânToolStripMenuItem";
            this.nghiệpVụLễTânToolStripMenuItem.Size = new System.Drawing.Size(106, 20);
            this.nghiệpVụLễTânToolStripMenuItem.Text = "Nghiệp vụ lễ tân";
            // 
            // checkInToolStripMenuItem
            // 
            this.checkInToolStripMenuItem.Name = "checkInToolStripMenuItem";
            this.checkInToolStripMenuItem.Size = new System.Drawing.Size(184, 22);
            this.checkInToolStripMenuItem.Text = "CheckIn";
            this.checkInToolStripMenuItem.Click += new System.EventHandler(this.checkInToolStripMenuItem_Click);
            // 
            // checkOutToolStripMenuItem
            // 
            this.checkOutToolStripMenuItem.Name = "checkOutToolStripMenuItem";
            this.checkOutToolStripMenuItem.Size = new System.Drawing.Size(184, 22);
            this.checkOutToolStripMenuItem.Text = "CheckOut";
            this.checkOutToolStripMenuItem.Click += new System.EventHandler(this.checkOutToolStripMenuItem_Click);
            // 
            // sửDụngDịchVụToolStripMenuItem
            // 
            this.sửDụngDịchVụToolStripMenuItem.Name = "sửDụngDịchVụToolStripMenuItem";
            this.sửDụngDịchVụToolStripMenuItem.Size = new System.Drawing.Size(184, 22);
            this.sửDụngDịchVụToolStripMenuItem.Text = "Sử dụng dịch vụ";
            this.sửDụngDịchVụToolStripMenuItem.Click += new System.EventHandler(this.sửDụngDịchVụToolStripMenuItem_Click);
            // 
            // xửLýYêuCầuHuỷToolStripMenuItem
            // 
            this.xửLýYêuCầuHuỷToolStripMenuItem.Name = "xửLýYêuCầuHuỷToolStripMenuItem";
            this.xửLýYêuCầuHuỷToolStripMenuItem.Size = new System.Drawing.Size(184, 22);
            this.xửLýYêuCầuHuỷToolStripMenuItem.Text = "Xử lý yêu cầu huỷ";
            this.xửLýYêuCầuHuỷToolStripMenuItem.Click += new System.EventHandler(this.xửLýYêuCầuHuỷToolStripMenuItem_Click);
            // 
            // xemPhòngToolStripMenuItem1
            // 
            this.xemPhòngToolStripMenuItem1.Name = "xemPhòngToolStripMenuItem1";
            this.xemPhòngToolStripMenuItem1.Size = new System.Drawing.Size(184, 22);
            this.xemPhòngToolStripMenuItem1.Text = "Xem phòng ";
            this.xemPhòngToolStripMenuItem1.Click += new System.EventHandler(this.xemPhòngToolStripMenuItem1_Click);
            // 
            // giaHạnThêmNgàyỞToolStripMenuItem
            // 
            this.giaHạnThêmNgàyỞToolStripMenuItem.Name = "giaHạnThêmNgàyỞToolStripMenuItem";
            this.giaHạnThêmNgàyỞToolStripMenuItem.Size = new System.Drawing.Size(184, 22);
            this.giaHạnThêmNgàyỞToolStripMenuItem.Text = "Gia hạn thêm ngày ở";
            this.giaHạnThêmNgàyỞToolStripMenuItem.Click += new System.EventHandler(this.giaHạnThêmNgàyỞToolStripMenuItem_Click);
            // 
            // chiPhíPhátSinhToolStripMenuItem
            // 
            this.chiPhíPhátSinhToolStripMenuItem.Name = "chiPhíPhátSinhToolStripMenuItem";
            this.chiPhíPhátSinhToolStripMenuItem.Size = new System.Drawing.Size(184, 22);
            this.chiPhíPhátSinhToolStripMenuItem.Text = "Chi phí phát sinh";
            this.chiPhíPhátSinhToolStripMenuItem.Click += new System.EventHandler(this.chiPhíPhátSinhToolStripMenuItem_Click);
            // 
            // báoCáoToolStripMenuItem
            // 
            this.báoCáoToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.báoCáoDoanhThuToolStripMenuItem});
            this.báoCáoToolStripMenuItem.Name = "báoCáoToolStripMenuItem";
            this.báoCáoToolStripMenuItem.Size = new System.Drawing.Size(61, 20);
            this.báoCáoToolStripMenuItem.Text = "Báo cáo";
            // 
            // báoCáoDoanhThuToolStripMenuItem
            // 
            this.báoCáoDoanhThuToolStripMenuItem.Name = "báoCáoDoanhThuToolStripMenuItem";
            this.báoCáoDoanhThuToolStripMenuItem.Size = new System.Drawing.Size(174, 22);
            this.báoCáoDoanhThuToolStripMenuItem.Text = "Báo cáo doanh thu";
            this.báoCáoDoanhThuToolStripMenuItem.Click += new System.EventHandler(this.báoCáoDoanhThuToolStripMenuItem_Click);
            // 
            // hoáĐơnToolStripMenuItem
            // 
            this.hoáĐơnToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.xemVàInDanhSáchHoáĐơnToolStripMenuItem});
            this.hoáĐơnToolStripMenuItem.Name = "hoáĐơnToolStripMenuItem";
            this.hoáĐơnToolStripMenuItem.Size = new System.Drawing.Size(65, 20);
            this.hoáĐơnToolStripMenuItem.Text = "Hoá đơn";
            // 
            // xemVàInDanhSáchHoáĐơnToolStripMenuItem
            // 
            this.xemVàInDanhSáchHoáĐơnToolStripMenuItem.Name = "xemVàInDanhSáchHoáĐơnToolStripMenuItem";
            this.xemVàInDanhSáchHoáĐơnToolStripMenuItem.Size = new System.Drawing.Size(230, 22);
            this.xemVàInDanhSáchHoáĐơnToolStripMenuItem.Text = "Xem và in danh sách hoá đơn";
            this.xemVàInDanhSáchHoáĐơnToolStripMenuItem.Click += new System.EventHandler(this.xemVàInDanhSáchHoáĐơnToolStripMenuItem_Click);
            // 
            // quảnLýDịchVụToolStripMenuItem
            // 
            this.quảnLýDịchVụToolStripMenuItem.Name = "quảnLýDịchVụToolStripMenuItem";
            this.quảnLýDịchVụToolStripMenuItem.Size = new System.Drawing.Size(105, 20);
            this.quảnLýDịchVụToolStripMenuItem.Text = "Quản lý dịch vụ ";
            this.quảnLýDịchVụToolStripMenuItem.Click += new System.EventHandler(this.quảnLýDịchVụToolStripMenuItem_Click_1);
            // 
            // frmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.menuStrip1);
            this.IsMdiContainer = true;
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "frmMain";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.frmMain_Load);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

      
        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem hệThốngToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem mnvDangNhap;
        private System.Windows.Forms.ToolStripMenuItem mnvThoat;
        private System.Windows.Forms.ToolStripMenuItem quảnLýĐặtPhòngToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem đặtPhòngMớiToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem danhSáchĐặtPhòngToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem báoCáoToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem báoCáoDoanhThuToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem nghiệpVụLễTânToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem checkInToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem checkOutToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem sửDụngDịchVụToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem xửLýYêuCầuHuỷToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem xemPhòngToolStripMenuItem1;
        private ToolStripMenuItem hoáĐơnToolStripMenuItem;
        private ToolStripMenuItem xemVàInDanhSáchHoáĐơnToolStripMenuItem;
        private ToolStripMenuItem giaHạnThêmNgàyỞToolStripMenuItem;
        private ToolStripMenuItem chiPhíPhátSinhToolStripMenuItem;
        private ToolStripMenuItem quảnLýDịchVụToolStripMenuItem;
    }
}


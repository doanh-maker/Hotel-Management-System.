using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Collections.Generic;
using System.Windows.Forms;

namespace QuanLyDatPhongKhachSan
{
    public partial class FrmChiTietPhongKH : Form
    {
        private int maPhong;
        private int soDem;
        private int soNguoi;
        private decimal tongTien;
        private DateTime tuNgay;
        private DateTime denNgay;
        private int maTK;
        private string tenPhong;
        private string tenLoai;
        private decimal donGia;
        // ===== SCALE FONT =====
        private float originalWidth;
        private float originalHeight;
        private Dictionary<Control, float> originalFonts = new Dictionary<Control, float>();
        private int soLuongNguoi;
        private string moTa;
        private string trangThai;

        public FrmChiTietPhongKH(int maPhong, int soDem, int soNguoi, decimal tongTien,
                                DateTime tuNgay, DateTime denNgay, int maTK,string tenPhong,string tenLoai,decimal donGia,string moTa,string trangThai,int soLuongNguoi)
        {
            InitializeComponent();

            // ===== GÁN BIẾN =====
            this.maPhong = maPhong;
            this.soDem = soDem;
            this.soNguoi = soNguoi;
            this.tongTien = tongTien;
            this.tuNgay = tuNgay;
            this.denNgay = denNgay;
            this.maTK = maTK;
            this.moTa = moTa;
            this.soLuongNguoi = soLuongNguoi;
            this.trangThai = trangThai;

            // ===== HIỂN THỊ =====
            lblMaPhong.Text = tenPhong.ToString();
            lblLoaiPhong.Text = tenLoai.ToString();
            lblGia.Text =donGia.ToString("N0") + " VNĐ/ngày";

            lblSoNguoi.Text = soNguoi.ToString();
            lblSoDem.Text = soDem.ToString();
            lblTuNgay.Text = tuNgay.ToString("dd/MM/yyyy");
            lblDenNgay.Text = denNgay.ToString("dd/MM/yyyy");
            lblTongTien.Text = tongTien.ToString("N0") + " VNĐ";
            lblTrangThai.Text = trangThai.ToString();
            lblSoLuongNguoiToiDa.Text = soLuongNguoi.ToString();
            lblMoTa.Text = moTa.ToString();

            // Highlight tổng tiền
            lblTongTien.ForeColor = Color.Red;
            lblTongTien.Font = new Font(lblTongTien.Font.FontFamily, 14, FontStyle.Bold);

            // ===== UI =====
            SetupUI();

            // ===== SCALE =====
            SaveOriginalFont(this);
            originalWidth = this.Width;
            originalHeight = this.Height;

            // ===== EVENT =====
            this.Load += FrmChiTietPhongKH_Load;
            this.Resize += FrmChiTietPhongKH_Resize;
        }

      

        // ================= UI =================
        private void SetupUI()
        {
            // Tiện nghi
            chkTienNghi.Dock = DockStyle.Fill;
            chkTienNghi.Enabled = false;

            // Button
            btnDatPhong.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;

            // Auto size label
            foreach (Control c in this.Controls)
            {
                if (c is Label lbl)
                    lbl.AutoSize = true;
            }
        }

        // ================= LOAD =================
        private void FrmChiTietPhongKH_Load(object sender, EventArgs e)
        {
            LoadTienNghi();
            LoadThongTinPhong();
        }

        // ================= LOAD PHÒNG =================
        private void LoadThongTinPhong()
        {
            //try
            //{
            //    SqlParameter[] parameters =
            //    {
            //        new SqlParameter("@MaPhong", SqlDbType.Int) { Value = maPhong }
            //    };

            //    DataTable dt = DatabaseHelper.ExecuteStoredProcedure(
            //        "sp_LayThongTinPhong", parameters);

            //    if (dt != null && dt.Rows.Count > 0)
            //    {
            //        DataRow row = dt.Rows[0];

            //        lblLoaiPhong.Text = row["TenLoai"].ToString();
            //        lblGia.Text = Convert.ToDecimal(row["DonGia"])
            //                        .ToString("N0") + " VNĐ";
            //    }
            //}
            //catch (Exception ex)
            //{
            //    MessageBox.Show("Lỗi load phòng:\n" + ex.Message);
            //}
        }

        // ================= LOAD TIỆN NGHI =================
        private void LoadTienNghi()
        {
            try
            {
                SqlParameter[] parameters =
                {
                    new SqlParameter("@MaPhong", SqlDbType.Int) { Value = maPhong }
                };

                DataTable dt = DatabaseHelper.ExecuteStoredProcedure(
                    "sp_HienThiTienNghiTheoMaPhong", parameters);

                chkTienNghi.Items.Clear();

                if (dt != null && dt.Rows.Count > 0)
                {
                    foreach (DataRow row in dt.Rows)
                    {
                        chkTienNghi.Items.Add("✔ " + row["MoTa"].ToString());
                    }
                }
                else
                {
                    chkTienNghi.Items.Add("Không có tiện nghi");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi tải tiện nghi:\n" + ex.Message,
                                "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // ================= SCALE FONT =================
        private void FrmChiTietPhongKH_Resize(object sender, EventArgs e)
        {
            float scaleX = this.Width / originalWidth;
            float scaleY = this.Height / originalHeight;
            float scale = Math.Min(scaleX, scaleY);

            ResizeFont(this, scale);
        }

        private void SaveOriginalFont(Control parent)
        {
            foreach (Control c in parent.Controls)
            {
                if (!originalFonts.ContainsKey(c))
                    originalFonts[c] = c.Font.Size;

                if (c.Controls.Count > 0)
                    SaveOriginalFont(c);
            }
        }

        private void ResizeFont(Control parent, float scale)
        {
            foreach (Control c in parent.Controls)
            {
                if (!originalFonts.ContainsKey(c)) continue;

                float baseSize = originalFonts[c];
                float newSize = baseSize * scale;

                if (newSize < 8) newSize = 8;
                if (newSize > 28) newSize = 28;

                c.Font = new Font(c.Font.FontFamily, newSize, c.Font.Style);

                if (c.Controls.Count > 0)
                    ResizeFont(c, scale);
            }
        }

        // ================= ĐẶT PHÒNG =================
        private void btnDatPhong_Click(object sender, EventArgs e)
        {
            try
            {
                
                if (this.MdiParent is frmMain main)
                {
                    main.OpenForm(new FrmThongTinKhachHangKH(
                    maPhong, tuNgay, denNgay, maTK, tongTien));
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi mở form khách hàng:\n" + ex.Message,
                                "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void chkTienNghi_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void tableLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void btnDatPhong_Click_1(object sender, EventArgs e)
        {

        }

        private void chkTienNghi_SelectedIndexChanged_1(object sender, EventArgs e)
        {

        }

        private void tableLayoutPanel2_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
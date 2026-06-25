using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;

namespace QuanLyDatPhongKhachSan
{
    public partial class FrmTaoDatPhongKH : Form
    {
        private int maTK;

        public FrmTaoDatPhongKH() { }

        public FrmTaoDatPhongKH(int maTK)
        {
            InitializeComponent();
            this.maTK = maTK;

            // Form đẹp hơn
            this.FormBorderStyle = FormBorderStyle.None;
            this.Dock = DockStyle.Fill;
            this.BackColor = Color.White;
        }

        private void FrmTaoDatPhong_Load(object sender, EventArgs e)
        {
            nudSoNguoi.Value = 2;
            dtpTuNgay.Value = DateTime.Now;
          dtpDenNgay.Value = DateTime.Now.AddDays(1);
            //dgvPhong.Columns["TongTien"].DefaultCellStyle.Format = "N0 'VNĐ'";
            //dgvPhong.Columns["DonGiaTheoNgay"].DefaultCellStyle.Format = "N0 'VNĐ'";
            // Tự động tìm kiếm lần đầu
            btnTimKiem.PerformClick();
           
            StyleDataGridView();
            StyleControls();
            LoadLoaiPhong();
        }

        // ================= STYLE =================
        private void StyleDataGridView()
        {
            dgvPhong.BorderStyle = BorderStyle.None;
            dgvPhong.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(240, 240, 240);
            dgvPhong.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            dgvPhong.DefaultCellStyle.SelectionBackColor = Color.FromArgb(0, 120, 215);
            dgvPhong.DefaultCellStyle.SelectionForeColor = Color.White;
            dgvPhong.BackgroundColor = Color.White;

            dgvPhong.EnableHeadersVisualStyles = false;
            dgvPhong.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;
            dgvPhong.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(0, 120, 215);
            dgvPhong.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            dgvPhong.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 10, FontStyle.Bold);

            dgvPhong.DefaultCellStyle.Font = new Font("Segoe UI", 10);
            dgvPhong.RowTemplate.Height = 35;
            dgvPhong.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        }

        private void StyleControls()
        {
            btnTimKiem.BackColor = Color.FromArgb(0, 120, 215);
            btnTimKiem.ForeColor = Color.White;
            btnTimKiem.FlatStyle = FlatStyle.Flat;
            btnTimKiem.FlatAppearance.BorderSize = 0;
            btnTimKiem.Font = new Font("Segoe UI", 10, FontStyle.Bold);

           nudSoNguoi.Font = new Font("Segoe UI", 10);

            dtpTuNgay.Font = new Font("Segoe UI", 10);
            dtpDenNgay.Font = new Font("Segoe UI", 10);
        }

        // ================= TÌM KIẾM =================
        private void btnTimKiem_Click(object sender, EventArgs e)
        {
            DateTime tuNgay = dtpTuNgay.Value.Date;
            DateTime denNgay = dtpDenNgay.Value.Date;
            object selected = cboLoaiPhong.SelectedValue;

            object maLoaiParam = (selected == null || selected == DBNull.Value)
                ? (object)DBNull.Value
                : Convert.ToInt32(selected);
            int soNguoi = nudSoNguoi.Text.Trim() != "" ? int.Parse(nudSoNguoi.Text.Trim()) : 0;
            if (nudSoNguoi.Value < 1)
            {
                MessageBox.Show("Số người phải lớn hơn 0", "Lỗi nhập liệu", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            string loi = "";

            if (tuNgay > denNgay)
                loi += "- Ngày bắt đầu phải <= ngày kết thúc\n";

            if (tuNgay < DateTime.Now.Date)
                loi += "- Ngày bắt đầu phải >= hôm nay\n";

            if (denNgay < DateTime.Now.Date)
                loi += "- Ngày kết thúc phải >= hôm nay\n";

            if (tuNgay == denNgay)
                loi += "- Phải chọn ít nhất 1 đêm\n";

            if (loi != "")
            {
                MessageBox.Show(loi, "Lỗi nhập liệu", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@NgayDat", SqlDbType.Date){Value=tuNgay},
                new SqlParameter("@NgayTra", SqlDbType.Date){Value=denNgay},
                new SqlParameter("@SoNguoi", SqlDbType.Int){Value=soNguoi},
                new SqlParameter("@MaLoai", SqlDbType.Int){Value=maLoaiParam}
            };

            DataTable dt = DatabaseHelper.ExecuteStoredProcedure("sp_TimKiemPhongTheoNgayVaSoNguoi", parameters);

            dgvPhong.DataSource = dt;
            if (dgvPhong.Columns.Contains("DonGiaTheoNgay"))
            {
                dgvPhong.Columns["DonGiaTheoNgay"].DefaultCellStyle.Format = "N0";
                dgvPhong.Columns["DonGiaTheoNgay"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
            }

            if (dgvPhong.Columns.Contains("TongTien"))
            {
                dgvPhong.Columns["TongTien"].DefaultCellStyle.Format = "N0";
                dgvPhong.Columns["TongTien"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
            }

            if (!dgvPhong.Columns.Contains("btnXem"))
            {
                DataGridViewButtonColumn btn = new DataGridViewButtonColumn();
                btn.Name = "btnXem";
                btn.HeaderText = "Chi tiết";
                btn.Text = "Xem";
                btn.UseColumnTextForButtonValue = true;
                dgvPhong.Columns.Add(btn);
            }

            dgvPhong.Columns["btnXem"].DisplayIndex = dgvPhong.Columns.Count - 1;
            dgvPhong.AllowUserToAddRows = false;

            if (dgvPhong.Columns.Contains("MaPhong")) { 
            dgvPhong.Columns["MaPhong"].Visible = false; 
            }

            if (dgvPhong.Columns.Contains("SoLuongNguoi"))
            {
                dgvPhong.Columns["SoLuongNguoi"].Visible = false;
            }
            if (dgvPhong.Columns.Contains("MoTa"))
            {
                dgvPhong.Columns["MoTa"].Visible = false;
            }

        }

        // ================= CLICK XEM =================
        private void dgvPhong_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dgvPhong.Columns[e.ColumnIndex].Name == "btnXem" && e.RowIndex >= 0)
            {
                int maPhong = Convert.ToInt32(dgvPhong.Rows[e.RowIndex].Cells["MaPhong"].Value);
                int soDem = Convert.ToInt32(dgvPhong.Rows[e.RowIndex].Cells["SoDem"].Value);
                int soNguoi = nudSoNguoi.Text.Trim() != "" ? int.Parse(nudSoNguoi.Text.Trim()) : 0;
                decimal tongTien = Convert.ToDecimal(dgvPhong.Rows[e.RowIndex].Cells["TongTien"].Value);
                string tenPhong = dgvPhong.Rows[e.RowIndex].Cells["TenPhong"].Value.ToString();
                string tenLoai = dgvPhong.Rows[e.RowIndex].Cells["TenLoai"].Value.ToString();
                decimal donGia = Convert.ToDecimal(dgvPhong.Rows[e.RowIndex].Cells["DonGiaTheoNgay"].Value);
                DateTime tuNgay = dtpTuNgay.Value.Date;
                DateTime denNgay = dtpDenNgay.Value.Date;
                string moTa = dgvPhong.Rows[e.RowIndex].Cells["MoTa"].Value.ToString();
                int soLuongNguoi = Convert.ToInt32(dgvPhong.Rows[e.RowIndex].Cells["SoLuongNguoi"].Value);
                string trangThai = dgvPhong.Rows[e.RowIndex].Cells["TrangThai"].Value.ToString();

                // 👉 Mở popup chi tiết (đúng UX)
                if (this.MdiParent is frmMain main)
                {
                    main.OpenForm(new FrmChiTietPhongKH(maPhong, soDem, soNguoi, tongTien, tuNgay, denNgay, maTK,tenPhong,tenLoai,donGia,moTa,trangThai,soLuongNguoi));
                }

                // 👉 Nếu muốn chuyển full màn hình thì dùng cái này thay thế:
                /*
                if (this.MdiParent is frmMain main)
                {
                    main.OpenForm(new FrmChiTietPhongKH(maPhong, soDem, soNguoi, tongTien, tuNgay, denNgay, maTK));
                }
                */
            }
        }

        private void txtSoNguoi_TextChanged(object sender, EventArgs e)
        {
            // Có thể validate số ở đây nếu muốn
        }
        private void LoadLoaiPhong()
        {
            string query = "SELECT MaLoai, TenLoai FROM LoaiPhong";
            DataTable dtLoai = DatabaseHelper.ExecuteQuery(query);

            // thêm dòng "Tất cả"
            DataRow row = dtLoai.NewRow();
            row["MaLoai"] = DBNull.Value;
            row["TenLoai"] = "-- Tất cả --";
            dtLoai.Rows.InsertAt(row, 0);

            cboLoaiPhong.DataSource = dtLoai;
            cboLoaiPhong.DisplayMember = "TenLoai";
            cboLoaiPhong.ValueMember = "MaLoai";
            cboLoaiPhong.SelectedIndex = 0;
        }
    }
}
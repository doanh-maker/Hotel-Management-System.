using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace QuanLyDatPhongKhachSan
{
    public partial class FrmGiaHanPhong : Form
    {
        private int maDP = 0;
        private DateTime ngayTra;

        public FrmGiaHanPhong()
        {
            InitializeComponent();
        }

        private void FrmGiaHanPhong_Load(object sender, EventArgs e)
        {
            LoadDanhSach();
        }

        // ================= LOAD DANH SÁCH =================
        private void LoadDanhSach()
        {
            SqlParameter[] p = {
                new SqlParameter("@TuKhoa",
                    string.IsNullOrEmpty(txtTimKiem.Text)
                    ? (object)DBNull.Value
                    : txtTimKiem.Text.Trim())
            };

            DataTable dt = DatabaseHelper.ExecuteStoredProcedure("sp_TimKiemDonDangThue", p);

            dgvDanhSach.DataSource = dt;
            dgvDanhSach.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvDanhSach.ReadOnly = true;

            // Thêm nút Gia hạn
            if (dgvDanhSach.Columns["btnGiaHan"] == null)
            {
                DataGridViewButtonColumn btn = new DataGridViewButtonColumn();
                btn.Name = "btnGiaHan";
                btn.HeaderText = "Thao tác";
                btn.Text = "Gia hạn";
                btn.UseColumnTextForButtonValue = true;
                dgvDanhSach.Columns.Add(btn);
            }
        }

        // ================= CLICK BUTTON TRONG GRID =================
        private void dgvDanhSach_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;

            if (dgvDanhSach.Columns[e.ColumnIndex].Name == "btnGiaHan")
            {
                var value = dgvDanhSach.Rows[e.RowIndex].Cells["MaDP"].Value;
                if (value == null || value == DBNull.Value)
                    return;

                maDP = Convert.ToInt32(value);
                LoadThongTinGiaHan(maDP);
            }
        }

        // ================= LOAD NGÀY TRẢ =================
        private void LoadThongTinGiaHan(int maDP)
        {
            string sql = "SELECT NgayTra FROM DatPhong WHERE MaDP = @MaDP";
            SqlParameter[] p = { new SqlParameter("@MaDP", maDP) };

            DataTable dt = DatabaseHelper.ExecuteQuery(sql, p);

            if (dt.Rows.Count > 0)
            {
                ngayTra = Convert.ToDateTime(dt.Rows[0]["NgayTra"]);
                dtpGiaHan.Value = ngayTra;
            }
        }

        // ================= XÁC NHẬN GIA HẠN =================
        private void btnXacNhan_Click(object sender, EventArgs e)
        {
            if (maDP == 0)
            {
                MessageBox.Show("Vui lòng chọn đơn đặt phòng cần gia hạn.");
                return;
            }

            DateTime ngayTraMoi = dtpGiaHan.Value.Date;

            if (ngayTraMoi <= DateTime.Now.Date)
            {
                MessageBox.Show("Ngày trả mới phải lớn hơn hôm nay.");
                return;
            }

            try
            {
                SqlParameter[] p = {
                    new SqlParameter("@MaDP", maDP),
                    new SqlParameter("@NgayTraMoi", ngayTraMoi),
                    new SqlParameter("@MaNV", CurrentUser.MaNV),
                    new SqlParameter("@SoTienTang", SqlDbType.Decimal)
                    {
                        Direction = ParameterDirection.Output
                    }
                };

                // ✅ GỌI SP bằng ExecuteNonQuery
                DatabaseHelper.ExecuteNonQuery(
                    "sp_GiaHanPhong",
                    p,
                    CommandType.StoredProcedure
                );

                // ✅ Lấy tiền phát sinh từ OUTPUT
                decimal soTienTang = 0;
                if (p[3].Value != DBNull.Value)
                    soTienTang = Convert.ToDecimal(p[3].Value);

                MessageBox.Show(
                    $"Gia hạn phòng thành công!\nTiền phát sinh: {soTienTang:N0} VNĐ",
                    "Thành công",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information
                );

                LoadDanhSach(); // reload lại danh sách
                maDP = 0; // reset
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi: " + ex.Message);
            }
        }

        private void btnTimKiem_Click(object sender, EventArgs e)
        {
            LoadDanhSach();
        }
        private void dtpGiaHan_ValueChanged(object sender, EventArgs e)
        {

        }

    }
}


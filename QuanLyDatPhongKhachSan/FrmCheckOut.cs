using System;

using System.Data;
using System.Data.SqlClient;

using System.Windows.Forms;

namespace QuanLyDatPhongKhachSan
{
    public partial class FrmCheckOut : Form
    {
        private int selectedMaDP = 0;
        private decimal tongCong = 0, daCoc = 0;
        public FrmCheckOut()
        {
            InitializeComponent();
            LoadDanhSach();
        }
        private void LoadDanhSach(string tuKhoa = null)
        {

          
            SqlParameter[] p = { new SqlParameter("@TuKhoa", tuKhoa ?? (object)DBNull.Value) };
            DataTable dt = DatabaseHelper.ExecuteStoredProcedure("sp_TimKiemDonDangO", p);
            dgvKetQua.DataSource = dt;
            dgvKetQua.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvKetQua.ReadOnly = true;
            // Thêm cột nút Check‑out nếu chưa có
            if (dgvKetQua.Columns["btnCheckOut"] == null)
            {
                DataGridViewButtonColumn btn = new DataGridViewButtonColumn();
                btn.Name = "btnCheckOut";
                btn.HeaderText = "Check‑out";
                btn.Text = "Chọn";
                btn.UseColumnTextForButtonValue = true;
                dgvKetQua.Columns.Add(btn);
            }

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;

            if (dgvKetQua.Columns[e.ColumnIndex].Name == "btnCheckOut")
            {
                var value = dgvKetQua.Rows[e.RowIndex].Cells["MaDP"].Value;

                if (value == null || value == DBNull.Value)
                    return;

                // ✅ Gán vào biến toàn cục
                selectedMaDP = Convert.ToInt32(value);

                // Gọi load thông tin thanh toán
                LoadThongTinThanhToan(selectedMaDP);
            }
        }
       
        private void FrmCheckOut_Load(object sender, EventArgs e)
        {
            cboPhuongThuc.Items.Add("Chuyển khoản");
            cboPhuongThuc.Items.Add("Tiền mặt");
            LoadDanhSach();
        }

        private void btnTimKiem_Click(object sender, EventArgs e)
        {
            LoadDanhSach(txtTimKiem.Text.Trim());
        }
        private void LoadThongTinThanhToan(int maDP)
        {
            // Tính tổng tiền, đã cọc, còn nợ (có thể dùng sp riêng hoặc tính trực tiếp)
            string sql = @"
          SELECT 
    ISNULL(SUM(ct.DonGia * DATEDIFF(DAY, dp.NgayNhan, dp.NgayTra)), 0) AS TongTienPhong,
    ISNULL((
        SELECT SUM(sd.SoLuong * dv.DonGia)
        FROM SuDungDichVu sd
        JOIN DichVu dv ON sd.MaDV = dv.MaDV
        WHERE sd.MaDP = dp.MaDP
    ), 0) AS TongTienDichVu,
    ISNULL((
        SELECT SUM(SoTien) 
        FROM ChiPhiPhatSinh 
        WHERE MaDP = dp.MaDP
    ), 0) AS TongChiPhi,
    ISNULL((
        SELECT SUM(SoTienCoc) 
        FROM DatCoc 
        WHERE MaDP = dp.MaDP AND TrangThaiCoc = N'Đã đặt cọc'
    ), 0) AS DaCoc
FROM DatPhong dp
LEFT JOIN ChiTietDatPhong ct ON ct.MaDP = dp.MaDP
WHERE dp.MaDP = @MaDP
GROUP BY dp.MaDP, dp.NgayNhan, dp.NgayTra;"; 
            SqlParameter[] param = { new SqlParameter("@MaDP", maDP) };
            DataTable dt = DatabaseHelper.ExecuteQuery(sql, param);
            if (dt.Rows.Count > 0)
            {
                decimal tongPhong = Convert.ToDecimal(dt.Rows[0]["TongTienPhong"]);
                decimal tongDV = Convert.ToDecimal(dt.Rows[0]["TongTienDichVu"]);
                decimal tongChiPhi = Convert.ToDecimal(dt.Rows[0]["TongChiPhi"]);
                tongCong = tongPhong + tongDV + tongChiPhi;
                daCoc = Convert.ToDecimal(dt.Rows[0]["DaCoc"]);
                decimal conNo = tongCong - daCoc;

                lblTongCong.Text = tongCong.ToString("N0") + " VNĐ";
                lblDatCoc.Text = daCoc.ToString("N0") + " VNĐ";
                lblConNo.Text = conNo.ToString("N0") + " VNĐ";
                nudSoTienTra.Value = conNo > 0 ? conNo : 0;
            }
        }

        private void btnThanhToan_Click(object sender, EventArgs e)
        {
            if (selectedMaDP == 0)
            {
                MessageBox.Show("Chọn một đơn để check-out.", "Thông báo");
                return;
            }

            decimal soTienTra = nudSoTienTra.Value;
            string phuongThuc = cboPhuongThuc.Text;

            if (string.IsNullOrEmpty(phuongThuc))
            {
                MessageBox.Show("Chọn phương thức thanh toán.", "Thông báo");
                return;
            }

            try
            {
                SqlParameter[] p = {
            new SqlParameter("@MaDP", selectedMaDP),
            new SqlParameter("@SoTienTra", soTienTra),
            new SqlParameter("@PhuongThuc", phuongThuc),
            new SqlParameter("@MaNV", CurrentUser.MaNV)
        };

                DataTable dt = DatabaseHelper.ExecuteStoredProcedure("sp_CheckOut", p);

                if (dt.Rows.Count > 0)
                {
                    int maHD = Convert.ToInt32(dt.Rows[0]["MaHD"]);

                    MessageBox.Show("Check-out thành công!", "Thành công");

                    // 🔥 GỌI REPORT
                    FrmReportHoaDon frm = new FrmReportHoaDon(maHD);
                    frm.ShowDialog();

                    // reload
                    LoadDanhSach(txtTimKiem.Text.Trim());

                    selectedMaDP = 0;

                    lblTongCong.Text = "0";
                    lblDatCoc.Text = "0";
                    lblConNo.Text = "0";
                }
                else
                {
                    MessageBox.Show("Check-out thất bại: " + dt.Rows[0]["KetQua"], "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi: " + ex.Message);
            }
        }
    }
}

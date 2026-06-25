using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.VisualBasic; // Thư viện cho InputBox

namespace QuanLyDatPhongKhachSan
{
    public partial class FrmDanhSachDatPhongKH : Form
    {
        private int maTK;

        public object Interaction { get; private set; }

        public FrmDanhSachDatPhongKH(int maTK)
        {
            InitializeComponent();
            this.maTK = maTK;
        }

        private void FrmDanhSachDatPhong_Load(object sender, EventArgs e)
        {
            // Gọi hàm tải dữ liệu ngay khi mở Form
            LoadDanhSach();

            // Cấu hình giao diện DataGridView
            ConfigDataGridView();
        }

        // Hàm nạp dữ liệu (Dùng chung cho Load và sau khi cập nhật)
        private void LoadDanhSach()
        {
            try
            {
                SqlParameter[] parameters = new SqlParameter[]
                {
                    new SqlParameter("@MaTK", SqlDbType.Int) { Value = maTK },
                };

                DataTable dt = DatabaseHelper.ExecuteStoredProcedure("pr_hienthidanhsachdatphong", parameters);
                dgvDanhSachDatPhong.DataSource = dt;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi tải danh sách: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ConfigDataGridView()
        {
            dgvDanhSachDatPhong.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvDanhSachDatPhong.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
            dgvDanhSachDatPhong.DefaultCellStyle.WrapMode = DataGridViewTriState.True;
            dgvDanhSachDatPhong.ReadOnly = true;
            dgvDanhSachDatPhong.AllowUserToDeleteRows = false;
            dgvDanhSachDatPhong.AllowUserToAddRows = false;

            // Sắp xếp thứ tự các cột nút bấm ở cuối bảng
            if (dgvDanhSachDatPhong.Columns.Contains("btnYeuCauHuy") && dgvDanhSachDatPhong.Columns.Contains("btnXemChiTiet"))
            {
                dgvDanhSachDatPhong.Columns["btnYeuCauHuy"].DisplayIndex = dgvDanhSachDatPhong.Columns.Count - 1;
                dgvDanhSachDatPhong.Columns["btnXemChiTiet"].DisplayIndex = dgvDanhSachDatPhong.Columns.Count - 2;
            }
        }

        private void dgvDanhSachDatPhong_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;
            string columnName = dgvDanhSachDatPhong.Columns[e.ColumnIndex].Name;

            // XỬ LÝ NÚT YÊU CẦU HỦY
            if (columnName == "btnYeuCauHuy")
            {
                ThucHienYeuCauHuy(e.RowIndex);
            }
            // XỬ LÝ NÚT XEM CHI TIẾT
            else if (columnName == "btnXemChiTiet")
            {
                int maDP = Convert.ToInt32(dgvDanhSachDatPhong.Rows[e.RowIndex].Cells["MaDP"].Value);
                string trangThai = dgvDanhSachDatPhong.Rows[e.RowIndex].Cells["TrangThaiDat"].Value.ToString();
                if (trangThai == "Đã trả phòng")
                {
                   
                    FrmChiTietDatPhongDaThanhToan frmChiTietDatPhongDaThanhToan = new FrmChiTietDatPhongDaThanhToan(maDP);
                    frmChiTietDatPhongDaThanhToan.ShowDialog();
                }
                else
                {
                   
                    // Mở Form chi tiết (Lưu ý: Bạn nên tạo constructor nhận MaDP bên FrmChiTietDatPhongNV)
                    FrmChiTietDatPhong frm = new FrmChiTietDatPhong(maDP);
                    frm.ShowDialog();
                }
            }
        }
        private void ThucHienYeuCauHuy(int rowIndex)
        {
            try
            {
                int maDP = Convert.ToInt32(dgvDanhSachDatPhong.Rows[rowIndex].Cells["MaDP"].Value);
                string trangThai = dgvDanhSachDatPhong.Rows[rowIndex].Cells["TrangThaiDat"].Value.ToString();

                // Xử lý giá trị DateTime để tránh lỗi DBNull
                object objNgayNhan = dgvDanhSachDatPhong.Rows[rowIndex].Cells["NgayNhan"].Value;
                DateTime ngayNhan = (objNgayNhan != DBNull.Value) ? Convert.ToDateTime(objNgayNhan) : DateTime.MinValue;

                // 1. Kiểm tra điều kiện hủy
                if (trangThai == "Hủy" || trangThai == "Đã trả phòng" || trangThai == "Đã nhận phòng" || ngayNhan <= DateTime.Now.Date)
                {
                    MessageBox.Show("Đơn đặt phòng này hiện tại không thể yêu cầu hủy (Đã nhận/trả phòng hoặc quá hạn hủy).",
                                    "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // 2. Hiển thị InputBox lấy lý do (Dùng thư viện Microsoft.VisualBasic)
                // Thay dòng bị gạch đỏ bằng dòng này:
                string lyDo = Microsoft.VisualBasic.Interaction.InputBox("Vui lòng nhập lý do hủy phòng:", "Xác nhận yêu cầu hủy", "");

                if (string.IsNullOrWhiteSpace(lyDo))
                {
                    MessageBox.Show("Bạn chưa nhập lý do. Yêu cầu hủy đã bị hủy bỏ.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // 3. Gọi Stored Procedure để lưu yêu cầu
                SqlParameter[] parameters = {
                    new SqlParameter("@MaDP", maDP),
                    new SqlParameter("@LyDo", lyDo)
                };

                DataTable dt = DatabaseHelper.ExecuteStoredProcedure("sp_KhachHangYeuCauHuy", parameters);

                if (dt != null && dt.Rows.Count > 0)
                {
                    string maYeuCau = dt.Rows[0]["MaYeuCau"].ToString();
                    MessageBox.Show($"Yêu cầu hủy đã được gửi thành công!\nMã yêu cầu: {maYeuCau}\nNhân viên sẽ kiểm tra và phản hồi sớm nhất.",
                                    "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    // Tải lại danh sách để cập nhật trạng thái mới nhất
                    LoadDanhSach();
                }
                else
                {
                    MessageBox.Show("Hệ thống không thể ghi nhận yêu cầu lúc này. Vui lòng thử lại sau.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Đã xảy ra lỗi: " + ex.Message, "Lỗi hệ thống", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
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

namespace QuanLyDatPhongKhachSan
{
    public partial class FrmDanhSachYeuCauHuy : Form
    {
        public FrmDanhSachYeuCauHuy()
        {
            InitializeComponent();
        }
        private void LoadData()
        {
            // Lấy mục đang chọn, nếu chưa chọn gì thì mặc định là "Tất cả"
            string selected = cboTrangThai.SelectedItem?.ToString() ?? "Tất cả";

            // Chuyển "Tất cả" thành null để SQL xử lý lấy toàn bộ danh sách
            string thamSoTrangThai = (selected == "Tất cả") ? null : selected;

            // Truyền tham số vào Store Procedure
            SqlParameter[] p = {
        new SqlParameter("@TrangThai", (object)thamSoTrangThai ?? DBNull.Value)
    };

            try
            {
                DataTable dt = DatabaseHelper.ExecuteStoredProcedure("sp_LayDanhSachYeuCauHuy", p);
                dgvYeuCau.DataSource = dt;

                // Cấu hình lại Grid sau khi nạp dữ liệu
                dgvYeuCau.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                AddButtonColumns();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi: " + ex.Message);
            }
        }
        private void FrmDanhSachYeuCauHuy_Load(object sender, EventArgs e)
        {

            cboTrangThai.Items.Clear();
            cboTrangThai.Items.Add("Tất cả");
            cboTrangThai.Items.Add("Đã duyệt");
            cboTrangThai.Items.Add("Đã hủy");
            LoadData();
        }
        private void AddButtonColumns()
        {
            // Chỉ thêm nút nếu chưa có
            if (dgvYeuCau.Columns["btnDuyet"] == null)
            {
                DataGridViewButtonColumn btnDuyet = new DataGridViewButtonColumn();
                btnDuyet.Name = "btnDuyet";
                btnDuyet.HeaderText = "Duyệt";
                btnDuyet.Text = "Duyệt hủy";
                btnDuyet.UseColumnTextForButtonValue = true;
                dgvYeuCau.Columns.Add(btnDuyet);
            }
            if (dgvYeuCau.Columns["btnTuChoi"] == null)
            {
                DataGridViewButtonColumn btnTuChoi = new DataGridViewButtonColumn();
                btnTuChoi.Name = "btnTuChoi";
                btnTuChoi.HeaderText = "Từ chối";
                btnTuChoi.Text = "Từ chối";
                btnTuChoi.UseColumnTextForButtonValue = true;
                dgvYeuCau.Columns.Add(btnTuChoi);
            }
        }

        private void dgvYeuCau_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;
            string columnName = dgvYeuCau.Columns[e.ColumnIndex].Name;
            int maYeuCau = Convert.ToInt32(dgvYeuCau.Rows[e.RowIndex].Cells["MaYeuCau"].Value);
            string trangThaiYC = dgvYeuCau.Rows[e.RowIndex].Cells["TrangThaiYeuCau"].Value.ToString();

            if (trangThaiYC != "Chờ duyệt")
            {
                MessageBox.Show("Yêu cầu này đã được xử lý.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            if (columnName == "btnDuyet")
            {
                DialogResult dr = MessageBox.Show("Xác nhận duyệt hủy đơn đặt phòng này?", "Duyệt hủy", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (dr == DialogResult.Yes)
                {
                    try
                    {
                        SqlParameter[] p = {
                            new SqlParameter("@MaYeuCau", maYeuCau),
                            new SqlParameter("@Duyet", 1),
                            new SqlParameter("@MaNV", CurrentUser.MaNV)
                        };
                        DatabaseHelper.ExecuteStoredProcedure("sp_XuLyYeuCauHuy", p);
                        MessageBox.Show("Đã duyệt hủy đơn thành công.", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        LoadData(); // reload danh sách
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Lỗi: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            else if (columnName == "btnTuChoi")
            {
                string lyDo = Microsoft.VisualBasic.Interaction.InputBox("Nhập lý do từ chối:", "Từ chối yêu cầu hủy", "");
                if (string.IsNullOrWhiteSpace(lyDo))
                {
                    MessageBox.Show("Vui lòng nhập lý do từ chối.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                try
                {
                    SqlParameter[] p = {
                        new SqlParameter("@MaYeuCau", maYeuCau),
                        new SqlParameter("@Duyet", 0),
                        new SqlParameter("@LyDoTuChoi", lyDo),
                        new SqlParameter("@MaNV", CurrentUser.MaNV)
                    };
                    DatabaseHelper.ExecuteStoredProcedure("sp_XuLyYeuCauHuy", p);
                    MessageBox.Show("Đã từ chối yêu cầu hủy.", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LoadData();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
        private void btnLamMoi_Click(object sender, EventArgs e)
        {
            // Đưa ComboBox về mục đầu tiên (Tất cả)
            if (cboTrangThai.Items.Count > 0)
            {
                cboTrangThai.SelectedIndex = 0;
            }

            // Gọi load lại dữ liệu
            LoadData();

            // Thông báo nhỏ để biết nó đã chạy xong
            // MessageBox.Show("Đã làm mới danh sách!"); 
        }

        private void cboTrangThai_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadData();
        }
    }
}
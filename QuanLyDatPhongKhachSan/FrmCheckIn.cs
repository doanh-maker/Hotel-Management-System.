using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;

namespace QuanLyDatPhongKhachSan
{
    public partial class FrmCheckIn : Form
    {
        public FrmCheckIn()
        {
            InitializeComponent();
            // Form đẹp hơn
            this.FormBorderStyle = FormBorderStyle.None;
            this.Dock = DockStyle.Fill;
            this.BackColor = Color.White;
        }

        private void FrmCheckIn_Load(object sender, EventArgs e)
        {
            LoadDanhSach();
            StyleDataGridView();
            
        }

        private void LoadDanhSach(string tuKhoa = null)
        {
            SqlParameter[] p = { new SqlParameter("@TuKhoa", tuKhoa ?? (object)DBNull.Value) };
            DataTable dt = DatabaseHelper.ExecuteStoredProcedure("sp_TimKiemDonDatPhong", p);
            dgvKetQua.DataSource = dt;
            dgvKetQua.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvKetQua.ReadOnly = true;

            if (dgvKetQua.Columns["btnCheckIn"] == null)
            {
                DataGridViewButtonColumn btn = new DataGridViewButtonColumn();
                btn.Name = "btnCheckIn";
                btn.HeaderText = "Thao tác";
                btn.Text = "Check-in";
                btn.UseColumnTextForButtonValue = true;
                dgvKetQua.Columns.Add(btn);
            }
        }
        private void StyleDataGridView()
        {
            dgvKetQua.BorderStyle = BorderStyle.None;
            dgvKetQua.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(240, 240, 240);
            dgvKetQua.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            dgvKetQua.DefaultCellStyle.SelectionBackColor = Color.FromArgb(0, 120, 215);
            dgvKetQua.DefaultCellStyle.SelectionForeColor = Color.White;
            dgvKetQua.BackgroundColor = Color.White;

            dgvKetQua.EnableHeadersVisualStyles = false;
            dgvKetQua.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;
            dgvKetQua.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(0, 120, 215);
            dgvKetQua.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            dgvKetQua.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 10, FontStyle.Bold);

            dgvKetQua.DefaultCellStyle.Font = new Font("Segoe UI", 10);
            dgvKetQua.RowTemplate.Height = 35;
            dgvKetQua.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        }

        private void dgvKetQua_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;
            if (dgvKetQua.Columns[e.ColumnIndex].Name == "btnCheckIn")
            {
                
                var cellValue = dgvKetQua.Rows[e.RowIndex].Cells["MaDP"].Value;
                if (cellValue == null || cellValue == DBNull.Value)
                    return;
                if (!int.TryParse(cellValue.ToString(), out int maDP))
                    return;
                string trangThai = dgvKetQua.Rows[e.RowIndex].Cells["TrangThaiDat"].Value.ToString();
                DateTime ngayNhan = Convert.ToDateTime(dgvKetQua.Rows[e.RowIndex].Cells["NgayNhan"].Value);

                // Kiểm tra điều kiện trước khi gọi
                if (trangThai != "Đã đặt cọc" && trangThai != "Đã đặt")
                {
                    MessageBox.Show($"Đơn {maDP} không thể check-in vì trạng thái '{trangThai}'.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                if (ngayNhan.Date > DateTime.Now.Date)
                {
                    MessageBox.Show($"Chưa đến ngày nhận phòng ({ngayNhan:dd/MM/yyyy}).", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
              
                
                DialogResult dr = MessageBox.Show($"Xác nhận check‑in cho đơn #{maDP}?", "Check‑in", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (dr == DialogResult.Yes)
                {
                    try
                    {
                        SqlParameter[] parameters = {
                            new SqlParameter("@MaDP", maDP),
                            new SqlParameter("@MaNV", CurrentUser.MaNV)
                        };
                        object result = DatabaseHelper.ExecuteScalar("sp_CheckIn", parameters, CommandType.StoredProcedure);
                        if (result !=null && Convert.ToInt32(result)==1)
                        {
                           
                            MessageBox.Show("Check‑in thành công!", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            string tuKhoa = string.IsNullOrWhiteSpace(txtTimKiem.Text) ? null : txtTimKiem.Text.Trim();
                            LoadDanhSach(tuKhoa);
                            DialogResult di = MessageBox.Show(
    $"Bạn có muốn sử dụng dịch vụ cho đơn #{maDP} không?",
    "Dịch vụ",
    MessageBoxButtons.YesNo,
    MessageBoxIcon.Question);

                            if (di == DialogResult.Yes)
                            {
                                FrmSuDungDichVu frmSuDungDichVu = new FrmSuDungDichVu(maDP);

                                frmSuDungDichVu.ShowDialog();
                            }
                            else
                            {
                                // bỏ qua
                            }
                        }
                        else
                        {
                            MessageBox.Show("Check‑in thất bại. Vui lòng thử lại.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Lỗi: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }

            }
        }

        private void btnTimKiem_Click(object sender, EventArgs e)
        {
            string tuKhoa = string.IsNullOrWhiteSpace(txtTimKiem.Text) ? null : txtTimKiem.Text.Trim();
            LoadDanhSach(tuKhoa);
        }

        private void txtTimKiem_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
                btnTimKiem.PerformClick();
        }
    }
}
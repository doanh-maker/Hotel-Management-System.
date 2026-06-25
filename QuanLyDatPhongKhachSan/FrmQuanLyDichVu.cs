using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace QuanLyDatPhongKhachSan
{
    public partial class FrmQuanLyDichVu : Form
    {
        private int selectedMaDV = 0; 

        public FrmQuanLyDichVu()
        {
            InitializeComponent();
        }

        private void dgvDanhSachDichVu_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            
        }

        private void FrmQuanLyDichVu_Load(object sender, EventArgs e)
        {
            LoadDanhSachDichVu();     
            dtpNgayHetHan.Value = DateTime.Now; 
        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {
            
        }

        private void LoadDanhSachDichVu()
        {
            string sql = @"
                SELECT MaDV, TenDV, DonGia, DonViTinh, NgayHetHan,
                    CASE 
                        WHEN NgayHetHan IS NULL THEN N'Không hạn'
                        WHEN NgayHetHan < CAST(GETDATE() AS DATE) THEN N'Đã hết hạn'
                        ELSE CAST(DATEDIFF(DAY, GETDATE(), NgayHetHan) AS NVARCHAR) + N' ngày'
                    END AS SoNgayConHan
                FROM DichVu
                ORDER BY MaDV";
            DataTable dt = DatabaseHelper.ExecuteQuery(sql);
            dgvDanhSachDichVu.DataSource = dt;
            dgvDanhSachDichVu.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            
            if (dgvDanhSachDichVu.Columns.Contains("MaDV"))
                dgvDanhSachDichVu.Columns["MaDV"].Visible = false;
        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
           
            if (selectedMaDV == 0)
            {
                MessageBox.Show("Vui lòng chọn một dịch vụ để cập nhật.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

           
            if (string.IsNullOrWhiteSpace(txtTenDV.Text))
            {
                MessageBox.Show("Tên dịch vụ không được để trống.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            
            if (!decimal.TryParse(txtDonGia.Text, out decimal donGia) || donGia <= 0)
            {
                MessageBox.Show("Đơn giá phải là số lớn hơn 0.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

       
            DateTime ngayHetHan = dtpNgayHetHan.Value.Date;
            if (ngayHetHan < DateTime.Now.Date)
            {
                MessageBox.Show("Ngày hết hạn phải từ hôm nay trở đi.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

           
            string sql = "UPDATE DichVu SET TenDV = @Ten, DonGia = @Gia, DonViTinh = @DonVi, NgayHetHan = @Han WHERE MaDV = @MaDV";
            SqlParameter[] parameters = {
                new SqlParameter("@Ten", txtTenDV.Text.Trim()),
                new SqlParameter("@Gia", donGia),
                new SqlParameter("@DonVi", txtDonViTinh.Text.Trim()),
                new SqlParameter("@Han", ngayHetHan),
                new SqlParameter("@MaDV", selectedMaDV)
            };

            try
            {
                int result = DatabaseHelper.ExecuteNonQuery(sql, parameters);
                if (result > 0)
                {
                    MessageBox.Show("Cập nhật dịch vụ thành công.", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LoadDanhSachDichVu(); 
                }
                else
                {
                    MessageBox.Show("Không có thay đổi nào.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void dgvDanhSachDichVu_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvDanhSachDichVu.CurrentRow != null)
            {
                selectedMaDV = Convert.ToInt32(dgvDanhSachDichVu.CurrentRow.Cells["MaDV"].Value);
                txtTenDV.Text = dgvDanhSachDichVu.CurrentRow.Cells["TenDV"].Value?.ToString();
                txtDonGia.Text = dgvDanhSachDichVu.CurrentRow.Cells["DonGia"].Value?.ToString();
                txtDonViTinh.Text = dgvDanhSachDichVu.CurrentRow.Cells["DonViTinh"].Value?.ToString();
                dtpNgayHetHan.Value = Convert.ToDateTime(dgvDanhSachDichVu.CurrentRow.Cells["NgayHetHan"].Value);
            }
        }

        private void txtTenDV_TextChanged(object sender, EventArgs e)
        {
           
        }

        private void txtDonGia_TextChanged(object sender, EventArgs e)
        {
            
        }

        private void txtDonViTinh_TextChanged(object sender, EventArgs e)
        {
            
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            
            if (string.IsNullOrWhiteSpace(txtTenDV.Text))
            {
                MessageBox.Show("Tên dịch vụ không được để trống.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

           
            if (!decimal.TryParse(txtDonGia.Text, out decimal donGia) || donGia <= 0)
            {
                MessageBox.Show("Đơn giá phải là số lớn hơn 0.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            
            DateTime ngayHetHan = dtpNgayHetHan.Value.Date;
            if (ngayHetHan < DateTime.Now.Date)
            {
                MessageBox.Show("Ngày hết hạn phải từ hôm nay trở đi.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

          
            string sql = @"INSERT INTO DichVu (TenDV, DonGia, DonViTinh, NgayHetHan)
                           VALUES (@Ten, @Gia, @DonVi, @Han)";
            SqlParameter[] parameters = {
                new SqlParameter("@Ten", txtTenDV.Text.Trim()),
                new SqlParameter("@Gia", donGia),
                new SqlParameter("@DonVi", txtDonViTinh.Text.Trim()),
                new SqlParameter("@Han", ngayHetHan)
            };

            try
            {
                int result = DatabaseHelper.ExecuteNonQuery(sql, parameters);
                if (result > 0)
                {
                    MessageBox.Show("Thêm dịch vụ mới thành công.", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LoadDanhSachDichVu(); 

                   
                    txtTenDV.Text = "";
                    txtDonGia.Text = "";
                    txtDonViTinh.Text = "";
                    dtpNgayHetHan.Value = DateTime.Now;
                    selectedMaDV = 0; 
                }
                else
                {
                    MessageBox.Show("Thêm thất bại.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void txtTimKiem_TextChanged(object sender, EventArgs e)
        {

        }

        private void btnTimKiem_Click(object sender, EventArgs e)
        {
            DateTime ngayTim = dtpNgayHetHan.Value.Date;

            string sql = @"
                SELECT MaDV, TenDV, DonGia, DonViTinh, NgayHetHan,
                    CASE 
                        WHEN NgayHetHan IS NULL THEN N'Không hạn'
                        WHEN NgayHetHan < CAST(GETDATE() AS DATE) THEN N'Đã hết hạn'
                        ELSE CAST(DATEDIFF(DAY, GETDATE(), NgayHetHan) AS NVARCHAR) + N' ngày'
                    END AS SoNgayConHan
                FROM DichVu
                WHERE NgayHetHan = @NgayTim
                ORDER BY MaDV";
            SqlParameter[] p = { new SqlParameter("@NgayTim", ngayTim) };
            DataTable dt = DatabaseHelper.ExecuteQuery(sql, p);
            dgvDanhSachDichVu.DataSource = dt;
            if (dgvDanhSachDichVu.Columns.Contains("MaDV"))
                dgvDanhSachDichVu.Columns["MaDV"].Visible = false;
            txtSoLuong.Text = dt.Rows.Count.ToString();

            if (dt.Rows.Count == 0)
                MessageBox.Show($"Không có dịch vụ nào hết hạn vào ngày {ngayTim:dd/MM/yyyy}.", "Kết quả tìm kiếm");
        }

        private void txtSoLuong_TextChanged(object sender, EventArgs e)
        {

        }

        private void btnInDanhSach_Click(object sender, EventArgs e)
        {
            DateTime ngayTim = dtpNgayHetHan.Value.Date; 
            FrmInDV frm = new FrmInDV(ngayTim);
            frm.ShowDialog();
        }

        private void btnLoad_Click(object sender, EventArgs e)
        {
            LoadDanhSachDichVu();
        }
    }
}
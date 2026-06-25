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
    public partial class FrmChiPhiPhatSinh : Form
    {
        private int maDP = 0;
        public FrmChiPhiPhatSinh()
        {
            InitializeComponent();
        }

        private void dgvDanhSach_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if(e.RowIndex < 0) return;
            if(dgvDanhSach.Columns[e.ColumnIndex].Name == "btnThemChiPhi")
            {
                var value = dgvDanhSach.Rows[e.RowIndex].Cells["MaDP"].Value;
                if(value == null || value == DBNull.Value)
                    return;
                 maDP = Convert.ToInt32(value);
                
               
                LoadDanhSach();
            }
        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            if (maDP == 0)
            {
                MessageBox.Show("Vui lòng chọn đơn đặt phòng để thêm chi phí phát sinh.");
                return;
            }
            if (string.IsNullOrEmpty(txtTenChiPhi.Text.Trim()))
            {
                MessageBox.Show("Tên chi phí không được để trống.");
                return;
            }
            if (nudSoTien.Value <= 0)
            {
                MessageBox.Show("Số tiền phải lớn hơn 0.");
                return;
            }
            try
            {
                SqlParameter[] p = {
                    new SqlParameter("@MaDP", maDP),
                    new SqlParameter("@TenChiPhi", txtTenChiPhi.Text.Trim()),
                    new SqlParameter("@SoTien", nudSoTien.Value),
                    new SqlParameter("@GhiChu",txtGhiChu.Text.Trim())
                };
                int result = DatabaseHelper.ExecuteNonQuery("sp_ThemChiPhiPhatSinh", p, CommandType.StoredProcedure);
                if (result > 0)
                {
                    MessageBox.Show("Thêm chi phí phát sinh thành công!");
                    LoadDanhSach();
                }
                else
                {
                    MessageBox.Show("Thêm chi phí phát sinh thất bại.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi thêm chi phí phát sinh:\n" + ex.Message);

            }
        }

        private void btnTimKiem_Click(object sender, EventArgs e)
        {
            LoadDanhSach();
        }

        private void FrmChiPhiPhatSinh_Load(object sender, EventArgs e)
        {
            LoadDanhSach();
        }
        private void LoadDanhSach()
        {
            SqlParameter[] p = { new SqlParameter("@TuKhoa", string.IsNullOrEmpty(txtTimKiem.Text)?(object)DBNull.Value:txtTimKiem.Text.Trim()) };
            DataTable dt = DatabaseHelper.ExecuteStoredProcedure("sp_TimKiemDonDangThue", p);
            dgvDanhSach.DataSource = dt;
                dgvDanhSach.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                dgvDanhSach.ReadOnly = true;
            if(dgvDanhSach.Columns["btnThemChiPhi"] == null)
            {
                DataGridViewButtonColumn btn = new DataGridViewButtonColumn();
                btn.Name = "btnThemChiPhi";
                btn.HeaderText = "Thao tác";
                btn.Text = "Thêm chi phí";
                btn.UseColumnTextForButtonValue = true;
                dgvDanhSach.Columns.Add(btn);
            }

        }
    }
}
